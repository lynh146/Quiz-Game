using System;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Địa chỉ IP và cổng server
                string serverIP = "127.0.0.1";
                int port = 5000;

                // Kết nối tới server
                TcpClient client = new TcpClient(serverIP, port);
                NetworkStream stream = client.GetStream();

                // Gửi dữ liệu
                Console.Write("Nhập dữ liệu gửi tới server: ");
                string message = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // Nhận dữ liệu
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Dữ liệu nhận từ server: " + response);

                // Đóng kết nối
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
    }
}
