using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
//==================================================================================================
        //Đáp án
        private void btnAnswerA_Click(object sender, EventArgs e)
        {
            // TODO: Nếu người chơi click A:
            // - Nếu đúng: đổi btnAnswerA.BackColor = xanh lá nhạt (+1 điểm)
            // - Nếu sai: đổi btnAnswerA.BackColor = xám nhạt (không cộng điểm)
            // - Disable tất cả nút còn lại
            // - Dừng timer
        }

        private void btnAnswerB_Click(object sender, EventArgs e)
        {
            //tương tự
        }

        private void btnAnswerC_Click(object sender, EventArgs e)
        {
            //tương tự
        }

        private void btnAnswerD_Click(object sender, EventArgs e)
        {
            //tương tự
        }
//==================================================================================================
        // hover
        private void btnAnswerA_MouseEnter(object sender, EventArgs e)
        {
            //Nếu nút còn Enabled → đổi màu sáng hơn một chút
        }

        private void btnAnswerB_MouseEnter(object sender, EventArgs e)
        {
            //Nếu nút còn Enabled → đổi màu sáng hơn một chút
        }

        private void btnAnswerC_MouseEnter(object sender, EventArgs e)
        {
            //Nếu nút còn Enabled → đổi màu sáng hơn một chút
        }

        private void btnAnswerD_MouseEnter(object sender, EventArgs e)
        {
            //Nếu nút còn Enabled → đổi màu sáng hơn một chút
        }
        //==================================================================================================
        private void btnAnswerA_MouseLeave(object sender, EventArgs e)
        {
            //Nếu chưa chọn → trả lại màu gốc
        }

        private void btnAnswerB_MouseLeave(object sender, EventArgs e)
        {
            //Nếu chưa chọn → trả lại màu gốc
        }

        private void btnAnswerC_MouseLeave(object sender, EventArgs e)
        {
            //Nếu chưa chọn → trả lại màu gốc
        }

        private void btnAnswerD_MouseLeave(object sender, EventArgs e)
        {
            //Nếu chưa chọn → trả lại màu gốc
        }
        //==================================================================================================
        //Chuyển sang câu hỏi mới
        private void btnNext_Click(object sender, EventArgs e)
        {
            // TODO: Sang câu tiếp theo:
            // - Reset lại màu pastel gốc cho 4 nút
            // - Enable lại 4 nút
            // - Load câu hỏi mới
            // - Reset timer = 30s, start lại

        }
        //==================================================================================================
        // Thoát game
        private void btnExit_Click(object sender, EventArgs e)
        {
            // TODO: Thoát game
        }
        //==================================================================================================
        private void quizTimer_Tick(object sender, EventArgs e)
        {
            // TODO:
            // - Mỗi giây giảm biến timeLeft và cập nhật lblTimer
            // - Nếu timeLeft == 0:
            //    + Dừng timer
            //    + Tự highlight đáp án đúng = xanh lá nhạt
            //    + Các đáp án sai = xám nhạt
            //    + Disable các nút (vì hết giờ)
            //    + Không cộng điểm (do chưa chọn)
            //    + Cho phép bấm Next
        }
    }
    
}
