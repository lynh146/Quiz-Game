using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;



namespace Client
{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream stream;
        int scoreP1 = 0, scoreP2 = 0;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void btnRock_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5000);
                stream = client.GetStream();
                MessageBox.Show("Connected to Server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message);
            }
        }
    }
}

