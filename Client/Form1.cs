using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    public partial class Form1 : Form
    {
        // ===== SOCKET & STATE =====
        TcpClient _client;
        StreamReader _reader;
        StreamWriter _writer;

        int _qnum = 1;         // câu hiện tại 1..10
        int _total = 10;       // tổng số câu
        int _timeLeft = 30;    // giây/câu
        int _score = 0;        // điểm hiển thị
        bool _answered = false;

        // màu gốc 4 nút để reset
        Color _a0, _b0, _c0, _d0;

        // ===== DỮ LIỆU CÂU HỎI (giữ nguyên như bạn) =====
        class Q { public string Text; public string[] Opt; public int Correct; } // Correct = 0..3
        readonly List<Q> Qs = new List<Q> {
            new Q{ Text="Trong mô hình OSI, tầng nào chịu trách nhiệm định tuyến gói tin?",
                   Opt=new[]{"Data Link","Network","Transport","Application"}, Correct=1},
            new Q{ Text="Giao thức TCP hoạt động ở tầng nào trong mô hình OSI?",
                   Opt=new[]{"Application","Network","Transport","Session"}, Correct=2},
            new Q{ Text="Cổng mặc định của HTTP là số mấy?",
                   Opt=new[]{"80","443","21","25"}, Correct=0},
            new Q{ Text="Giao thức nào là giao thức không kết nối?",
                   Opt=new[]{"TCP","UDP","FTP","SMTP"}, Correct=1},
            new Q{ Text="Địa chỉ IPv4 có bao nhiêu bit?",
                   Opt=new[]{"32","64","128","16"}, Correct=0},
            new Q{ Text="Trong C#, lớp nào thường dùng để tạo TCP Client?",
                   Opt=new[]{"Socket","TcpClient","UdpClient","TcpListener"}, Correct=1},
            new Q{ Text="Lệnh ping sử dụng giao thức nào để kiểm tra kết nối?",
                   Opt=new[]{"TCP","UDP","ICMP","HTTP"}, Correct=2},
            new Q{ Text="Cổng mặc định của HTTPS là số mấy?",
                   Opt=new[]{"80","21","443","25"}, Correct=2},
            new Q{ Text="Trong C#, lớp TcpListener dùng để làm gì?",
                   Opt=new[]{"Gửi email","Nghe và chấp nhận kết nối TCP từ client","Gửi dữ liệu UDP","Mã hóa dữ liệu"}, Correct=1},
            new Q{ Text="Giao thức nào dùng để truyền file qua mạng?",
                   Opt=new[]{"FTP","HTTP","SSH","DNS"}, Correct=0},
        };

        public Form1()
        {
            InitializeComponent();

            // ghi nhớ màu gốc
            _a0 = btnAnswerA.BackColor; _b0 = btnAnswerB.BackColor;
            _c0 = btnAnswerC.BackColor; _d0 = btnAnswerD.BackColor;

            // timer (1s)
            quizTimer.Interval = 1000;

            // kết nối server & nạp câu đầu khi form load
            this.Load += (s, e) =>
            {
                try
                {
                    ConnectServer();
                    LoadQuestion(_qnum);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không kết nối được server: " + ex.Message);
                    Close();
                }
            };
            this.FormClosed += (s, e) => { try { _client?.Close(); } catch { } };
        }

        // ==================== NETWORK HELPERS ====================
        void ConnectServer()
        {
            _client = new TcpClient();
            _client.Connect("127.0.0.1", 5000);
            var ns = _client.GetStream();
            _reader = new StreamReader(ns);
            _writer = new StreamWriter(ns) { AutoFlush = true };

            _writer.WriteLine("START|" + _total);      // báo tổng số câu
            string ack = _reader.ReadLine();           // "ACK|<total>"
            if (ack == null || !ack.StartsWith("ACK|"))
                throw new Exception("Server không phản hồi đúng.");
        }

        void LoadQuestion(int qnum)
        {
            if (qnum > _total)
            {
                // hoàn tất → gửi FINISH và nhận FINAL
                _writer.WriteLine("FINISH");
                string final = _reader.ReadLine(); // FINAL|score|total|pct
                int s = 0, t = 0; double pct = 0;
                if (!string.IsNullOrEmpty(final) && final.StartsWith("FINAL|"))
                {
                    var ps = final.Split('|');
                    if (ps.Length >= 4)
                    {
                        int.TryParse(ps[1], out s);
                        int.TryParse(ps[2], out t);
                        double.TryParse(ps[3], out pct);
                    }
                }
                MessageBox.Show($"Kết thúc!\nĐúng {s}/{t} ({pct}%)", "Result");
                Close();
                return;
            }

            var q = Qs[qnum - 1];
            lblQuestion.Text = q.Text;
            btnAnswerA.Text = "A. " + q.Opt[0];
            btnAnswerB.Text = "B. " + q.Opt[1];
            btnAnswerC.Text = "C. " + q.Opt[2];
            btnAnswerD.Text = "D. " + q.Opt[3];

            // reset trạng thái & UI
            _answered = false;
            _timeLeft = 30;
            lblTimer.Text = $"{_timeLeft}s";
            lblScore.Text = "Điểm: " + _score;

            btnAnswerA.BackColor = _a0; btnAnswerB.BackColor = _b0;
            btnAnswerC.BackColor = _c0; btnAnswerD.BackColor = _d0;
            EnableAnswers(true);

            quizTimer.Start();
        }

        void EnableAnswers(bool en)
        {
            btnAnswerA.Enabled = btnAnswerB.Enabled = btnAnswerC.Enabled = btnAnswerD.Enabled = en;
        }

        void SubmitAnswer(int selectedIndex) // selected: 1..4
        {
            if (_answered) return;
            _answered = true;
            quizTimer.Stop();

            // gửi lên server
            _writer.WriteLine("ANS|" + _qnum + "|" + selectedIndex);

            // nhận: RES|qnum|isCorrect|correctIndex
            string res = _reader.ReadLine();
            bool correct = false; int correctIndex = 0;
            if (!string.IsNullOrEmpty(res) && res.StartsWith("RES|"))
            {
                var ps = res.Split('|');
                if (ps.Length >= 4)
                {
                    correct = (ps[2] == "1");
                    int.TryParse(ps[3], out correctIndex);
                }
            }

            // tô màu
            Button[] arr = { btnAnswerA, btnAnswerB, btnAnswerC, btnAnswerD };
            for (int i = 0; i < 4; i++)
            {
                if (i + 1 == correctIndex) arr[i].BackColor = Color.LightGreen;
                else if (i + 1 == selectedIndex && !correct) arr[i].BackColor = Color.LightGray;
            }

            if (correct) { _score++; lblScore.Text = "Điểm: " + _score; }
            EnableAnswers(false);
        }

        //==================================================================================================
        //Đáp án
        private void btnAnswerA_Click(object sender, EventArgs e) => SubmitAnswer(1);
        private void btnAnswerB_Click(object sender, EventArgs e) => SubmitAnswer(2);
        private void btnAnswerC_Click(object sender, EventArgs e) => SubmitAnswer(3);
        private void btnAnswerD_Click(object sender, EventArgs e) => SubmitAnswer(4);

        //==================================================================================================
        // hover
        private void btnAnswerA_MouseEnter(object sender, EventArgs e)
        {
            if (btnAnswerA.Enabled && !_answered) btnAnswerA.BackColor = ControlPaint.Light(_a0);
        }
        private void btnAnswerB_MouseEnter(object sender, EventArgs e)
        {
            if (btnAnswerB.Enabled && !_answered) btnAnswerB.BackColor = ControlPaint.Light(_b0);
        }
        private void btnAnswerC_MouseEnter(object sender, EventArgs e)
        {
            if (btnAnswerC.Enabled && !_answered) btnAnswerC.BackColor = ControlPaint.Light(_c0);
        }
        private void btnAnswerD_MouseEnter(object sender, EventArgs e)
        {
            if (btnAnswerD.Enabled && !_answered) btnAnswerD.BackColor = ControlPaint.Light(_d0);
        }

        //==================================================================================================
        private void btnAnswerA_MouseLeave(object sender, EventArgs e)
        {
            if (!_answered) btnAnswerA.BackColor = _a0;
        }
        private void btnAnswerB_MouseLeave(object sender, EventArgs e)
        {
            if (!_answered) btnAnswerB.BackColor = _b0;
        }
        private void btnAnswerC_MouseLeave(object sender, EventArgs e)
        {
            if (!_answered) btnAnswerC.BackColor = _c0;
        }
        private void btnAnswerD_MouseLeave(object sender, EventArgs e)
        {
            if (!_answered) btnAnswerD.BackColor = _d0;
        }

        //==================================================================================================
        //Chuyển sang câu hỏi mới
        private void btnNext_Click(object sender, EventArgs e)
        {
            // Nếu người chơi chưa chọn → coi như bỏ lỡ (gửi ANS|q|0 để server tính)
            if (!_answered)
            {
                quizTimer.Stop();
                try
                {
                    _writer.WriteLine("ANS|" + _qnum + "|0");
                    _reader.ReadLine(); // RES|... (bỏ qua)
                }
                catch { }
            }

            _qnum++;
            LoadQuestion(_qnum);
        }

        //==================================================================================================
        // Thoát game
        private void btnExit_Click(object sender, EventArgs e)
        {
            try { _writer?.WriteLine("FINISH"); } catch { }
            Close();
        }

        //==================================================================================================
        private void quizTimer_Tick(object sender, EventArgs e)
        {
            _timeLeft--;
            lblTimer.Text = $"{_timeLeft}s";
            if (_timeLeft <= 0)
            {
                quizTimer.Stop();
                _answered = true;
                EnableAnswers(false);

                // hết giờ: gửi ANS|q|0 để server trả về đáp án đúng, tô màu cho người chơi thấy
                try
                {
                    _writer.WriteLine("ANS|" + _qnum + "|0");
                    string res = _reader.ReadLine(); // RES|qnum|isCorrect|correctIndex
                    int correctIndex = 0;
                    if (!string.IsNullOrEmpty(res) && res.StartsWith("RES|"))
                    {
                        var ps = res.Split('|');
                        if (ps.Length >= 4) int.TryParse(ps[3], out correctIndex);
                    }
                    Button[] arr = { btnAnswerA, btnAnswerB, btnAnswerC, btnAnswerD };
                    for (int i = 0; i < 4; i++)
                        arr[i].BackColor = (i + 1 == correctIndex) ? Color.LightGreen : Color.LightGray;
                }
                catch { /* nếu mất kết nối thì bỏ qua highlight */ }
            }
        }
    }
}