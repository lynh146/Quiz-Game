using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace QuizServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new QuizServer(IPAddress.Any, 5000);
            Console.WriteLine("Starting Quiz Server on :5000 ...");
            server.Start();
            Console.WriteLine("Press ENTER to stop.");
            Console.ReadLine();
        }
    }

    public class QuizServer
    {
        private readonly TcpListener _listener;

        // BẢN ĐÁP ÁN KHỚP VỚI CLIENT (qnum: 1..10, đáp án 1..4)
        private readonly Dictionary<int, int> _answerKey = new Dictionary<int, int>
        {
            {1, 2}, {2, 3}, {3, 1}, {4, 2}, {5, 1},
            {6, 2}, {7, 3}, {8, 3}, {9, 2}, {10, 1},
        };

        public QuizServer(IPAddress ip, int port) { _listener = new TcpListener(ip, port); }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Server listening...");

            Task.Run(async () =>
            {
                while (true)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync();
                    Task.Run(() => HandleClient(client));
                }
            });
        }

        private void HandleClient(TcpClient client)
        {
            var ep = client.Client.RemoteEndPoint;
            Console.WriteLine("Client connected: " + ep);

            using (client)
            using (var ns = client.GetStream())
            using (var reader = new StreamReader(ns))
            using (var writer = new StreamWriter(ns) { AutoFlush = true })
            {
                try
                {
                    // START|<total>
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line) || !line.StartsWith("START|"))
                    {
                        writer.WriteLine("ERR|Expect START");
                        return;
                    }

                    int expectedTotal = 0, answered = 0, score = 0;
                    var parts = line.Split('|');
                    if (parts.Length >= 2) int.TryParse(parts[1], out expectedTotal);

                    writer.WriteLine("ACK|" + expectedTotal);

                    while (true)
                    {
                        string msg = reader.ReadLine();
                        if (msg == null) break; // disconnected

                        if (msg.StartsWith("ANS|"))
                        {
                            // ANS|qnum|selected
                            var ps = msg.Split('|');
                            if (ps.Length < 3) { writer.WriteLine("ERR|Bad ANS"); continue; }

                            int qnum, selected;
                            int.TryParse(ps[1], out qnum);
                            int.TryParse(ps[2], out selected);

                            answered++;
                            int correctIndex;
                            bool correct = _answerKey.TryGetValue(qnum, out correctIndex) && selected == correctIndex;
                            if (correct) score++;

                            writer.WriteLine("RES|" + qnum + "|" + (correct ? 1 : 0) + "|" + correctIndex);
                            // KHÔNG tự break khi đủ câu – chờ FINISH từ client
                        }
                        else if (msg == "FINISH")
                        {
                            break;
                        }
                        else
                        {
                            writer.WriteLine("ERR|Unknown");
                        }
                    }

                    double pct = expectedTotal > 0 ? (score * 100.0 / expectedTotal) : 0.0;
                    writer.WriteLine("FINAL|" + score + "|" + expectedTotal + "|" + Math.Round(pct, 2));
                    Console.WriteLine("Client {0} result {1}/{2}", ep, score, expectedTotal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Client {0} error: {1}", ep, ex.Message);
                    try { var w = new StreamWriter(ns) { AutoFlush = true }; w.WriteLine("ERR|" + ex.Message); } catch { }
                }
                finally
                {
                    Console.WriteLine("Client disconnected: " + ep);
                }
            }
        }
    }
}
