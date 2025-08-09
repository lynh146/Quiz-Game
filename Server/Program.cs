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
                Console.WriteLine($"[SERVER] Đang nghe cong {port}...");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("[SERVER] Client da ket noi!");

              
                    NetworkStream stream = client.GetStream();

        
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("[CLIENT]: " + message);

              
                    string response = "Server da nhan: " + message;
                    byte[] data = Encoding.UTF8.GetBytes(response);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("[SERVER] da gui phan hoi cho client");

             
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
