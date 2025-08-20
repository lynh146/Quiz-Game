using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 5000;
            TcpListener server = new TcpListener(IPAddress.Any, port);

            try
            {
                server.Start();
                Console.WriteLine($"[SERVER] Đang nghe cổng {port}...");

                while (true) // Server luôn chờ client mới
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("[SERVER] Client đã kết nối!");

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    // Nhận dữ liệu từ client
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("[CLIENT]: " + message);

                    // Gửi phản hồi cho client
                    string response = "Server đã nhận: " + message;
                    byte[] data = Encoding.UTF8.GetBytes(response);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("[SERVER] Đã gửi phản hồi!");

                    // Đóng kết nối với client sau khi xử lý xong
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
