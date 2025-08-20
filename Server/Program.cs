using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QuizServer
{
    class Program
    {
        static void Main(string[] args)
        {
            QuizServer server = new QuizServer(IPAddress.Any, 5000);
            Console.WriteLine("Starting Quiz Server on :5000 ...");
            server.Start();
            Console.WriteLine("Press ENTER to stop.");
            Console.ReadLine();
        }
    }

    public class QuizServer
    {
        private readonly TcpListener _listener;

        // Đáp án đúng theo qnum (1..4) – khớp askQuestion ở client
        private readonly Dictionary<int, int> _answerKey = new Dictionary<int, int>
        {
            {1, 2}, {2, 3}, {3, 1}, {4, 2}, {5, 1},
            {6, 2}, {7, 3}, {8, 3}, {9, 2}, {10, 1},
        };

        public QuizServer(IPAddress ip, int port)
        {
            _listener = new TcpListener(ip, port);
        }

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

            NetworkStream ns = client.GetStream();
            int expectedTotal = 0;
            int answered = 0;
            int score = 0;

            try
            {
                NetMsg start = Receive(ns);
                if (start == null || start.Type != "StartQuiz")
                {
                    Send(ns, new NetMsg("Error", new { message = "Expect StartQuiz" }));
                    client.Close();
                    return;
                }

                expectedTotal = (int)start.Payload["total"];
                Send(ns, new NetMsg("AckStart", new { ok = true, total = expectedTotal }));

                while (true)
                {
                    NetMsg msg = Receive(ns);
                    if (msg == null) break;

                    if (msg.Type == "Answer")
                    {
                        int qnum = (int)msg.Payload["qnum"];
                        int selectedIndex = (int)msg.Payload["selectedIndex"];

                        answered++;
                        int correctIndex;
                        bool correct = _answerKey.TryGetValue(qnum, out correctIndex) && selectedIndex == correctIndex;
                        if (correct) score++;

                        Send(ns, new NetMsg("AnswerResult", new
                        {
                            qnum = qnum,
                            correct = correct,
                            correctIndex = correctIndex
                        }));

                        if (expectedTotal > 0 && answered >= expectedTotal)
                            break;
                    }
                    else if (msg.Type == "Finish")
                    {
                        break;
                    }
                    else
                    {
                        Send(ns, new NetMsg("Error", new { message = "Unknown message" }));
                    }
                }

                double pct = expectedTotal > 0 ? (score * 100.0 / expectedTotal) : 0.0;
                Send(ns, new NetMsg("FinalResult", new
                {
                    score = score,
                    total = expectedTotal,
                    percentage = Math.Round(pct, 2)
                }));

                Console.WriteLine("Client {0} result {1}/{2}", ep, score, expectedTotal);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client {0} error: {1}", ep, ex.Message);
                try { Send(ns, new NetMsg("Error", new { message = ex.Message })); } catch { }
            }
            finally
            {
                client.Close();
                C
