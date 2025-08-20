using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        var server = new QuizServer(IPAddress.Any, 5000);
        Console.WriteLine("Starting Quiz Server on :5000 ...");
        await server.StartAsync();
    }
}

public class QuizServer
{
    private readonly TcpListener _listener;

    // Bảng đáp án đúng theo qnum (khớp với askQuestion ở Form1.cs)
    // qnum: correctIndex (1..4 giống client)
    private readonly Dictionary<int, int> _answerKey = new()
    {
        {1, 2}, // Network
        {2, 3}, // Transport
        {3, 1}, // 80
        {4, 2}, // UDP
        {5, 1}, // 32
        {6, 2}, // TcpClient
        {7, 3}, // ICMP
        {8, 3}, // 443
        {9, 2}, // TcpListener accept
        {10, 1}, // FTP  (bạn tiếp tục điền nếu còn)
        // … thêm nếu bạn có nhiều câu hơn
    };

    public QuizServer(IPAddress ip, int port)
    {
        _listener = new TcpListener(ip, port);
    }

    public async Task StartAsync(CancellationToken ct = default)
    {
        _listener.Start();
        Console.WriteLine("Server listening...");
        while (!ct.IsCancellationRequested)
        {
            var client = await _listener.AcceptTcpClientAsync(ct);
            _ = HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        var ep = client.Client.RemoteEndPoint;
        Console.WriteLine($"Client connected: {ep}");
        using var ns = client.GetStream();

        int expectedTotal = 0;
        int answered = 0;
        int score = 0;

        try
        {
            // Chờ client gửi StartQuiz trước
            var startMsg = await ReceiveAsync(ns);
            if (startMsg?.Type != "StartQuiz")
            {
                await SendAsync(ns, new NetMsg("Error", new { message = "Expect StartQuiz" }));
                return;
            }

            var startPayload = startMsg.Payload.Deserialize<StartQuiz>();
            expectedTotal = Math.Max(0, startPayload?.Total ?? 0);
            await SendAsync(ns, new NetMsg("AckStart", new { ok = true, total = expectedTotal }));

            // Nhận lần lượt trả lời
            while (true)
            {
                var msg = await ReceiveAsync(ns);
                if (msg == null) break;

                if (msg.Type == "Answer")
                {
                    var ans = msg.Payload.Deserialize<AnswerDto>();
                    if (ans == null) continue;

                    answered++;
                    bool correct = false;
                    if (_answerKey.TryGetValue(ans.Qnum, out var correctIndex))
                        correct = (ans.SelectedIndex == correctIndex);

                    if (correct) score++;

                    await SendAsync(ns, new NetMsg("AnswerResult", new
                    {
                        qnum = ans.Qnum,
                        correct,
                        correctIndex
                    }));

                    // Nếu client không gửi Finish mà ta đã nhận đủ số câu khai báo lúc Start
                    if (expectedTotal > 0 && answered >= expectedTotal)
                        break;
                }
                else if (msg.Type == "Finish")
                {
                    break;
                }
                else
                {
                    await SendAsync(ns, new NetMsg("Error", new { message = "Unknown message" }));
                }
            }

            double pct = expectedTotal > 0 ? (score * 100.0 / expectedTotal) : 0;
            await SendAsync(ns, new NetMsg("FinalResult", new
            {
                score,
                total = expectedTotal,
                percentage = Math.Round(pct, 2)
            }));

            Console.WriteLine($"Client {ep} result {score}/{expectedTotal}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Client {ep} error: {ex.Message}");
            try { await SendAsync(ns, new NetMsg("Error", new { message = ex.Message })); } catch { }
        }
        finally
        {
            client.Close();
            Console.WriteLine($"Client disconnected: {ep}");
        }
    }

    // ===== Framing: [4 byte length big-endian] + JSON UTF-8 =====

    private static async Task SendAsync(NetworkStream ns, NetMsg msg, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(msg, JsonOpts);
        var data = Encoding.UTF8.GetBytes(json);
        var len = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data.Length));
        await ns.WriteAsync(len, ct);
        await ns.WriteAsync(data, ct);
        await ns.FlushAsync(ct);
    }

    private static async Task<NetMsg?> ReceiveAsync(NetworkStream ns, CancellationToken ct = default)
    {
        var lenBuf = new byte[4];
        await ReadExactAsync(ns, lenBuf, ct);
        int len = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(lenBuf, 0));
        if (len <= 0 || len > 10_000_000) throw new IOException("Invalid frame length");

        var buf = new byte[len];
        await ReadExactAsync(ns, buf, ct);
        var json = Encoding.UTF8.GetString(buf);
        return JsonSerializer.Deserialize<NetMsg>(json, JsonOpts);
    }

    private static async Task ReadExactAsync(NetworkStream ns, byte[] buffer, CancellationToken ct = default)
    {
        int read = 0;
        while (read < buffer.Length)
        {
            int r = await ns.ReadAsync(buffer.AsMemory(read, buffer.Length - read), ct);
            if (r == 0) throw new IOException("Connection closed");
            read += r;
        }
    }

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}

// ======= Models & helper =======
public record NetMsg(string Type, object? Payload)
{
    public string Type { get; set; } = Type;
    public object? Payload { get; set; } = Payload;
}

public class StartQuiz { public int Total { get; set; } }

public class AnswerDto
{
    public int Qnum { get; set; }          // số câu hiện tại ở client
    public int SelectedIndex { get; set; } // 1..4 giống client
}

static class JsonExt
{
    public static T? Deserialize<T>(this object? payload)
    {
        if (payload is JsonElement je) return JsonSerializer.Deserialize<T>(je.GetRawText());
        if (payload is null) return default;
        // fallback
        var json = JsonSerializer.Serialize(payload);
        return JsonSerializer.Deserialize<T>(json);
    }
}
