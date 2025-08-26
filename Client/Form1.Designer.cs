namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.btnAnswerA = new System.Windows.Forms.Button();
            this.btnAnswerB = new System.Windows.Forms.Button();
            this.btnAnswerC = new System.Windows.Forms.Button();
            this.btnAnswerD = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.quizTimer = new System.Windows.Forms.Timer(this.components);
            this.lblTimer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.PowderBlue;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(376, 65);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUIZ GAME 🎉";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblScore
            // 
            this.lblScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblScore.Location = new System.Drawing.Point(50, 87);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(119, 38);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Điểm: 0";
            // 
            // lblQuestion
            // 
            this.lblQuestion.BackColor = System.Drawing.Color.LightCyan;
            this.lblQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblQuestion.Location = new System.Drawing.Point(57, 139);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(850, 100);
            this.lblQuestion.TabIndex = 2;
            this.lblQuestion.Text = "Câu hỏi sẽ hiển thị tại đây";
            this.lblQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAnswerA
            // 
            this.btnAnswerA.BackColor = System.Drawing.Color.Moccasin;
            this.btnAnswerA.FlatAppearance.BorderSize = 0;
            this.btnAnswerA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnswerA.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnswerA.Location = new System.Drawing.Point(57, 301);
            this.btnAnswerA.Name = "btnAnswerA";
            this.btnAnswerA.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnAnswerA.Size = new System.Drawing.Size(350, 70);
            this.btnAnswerA.TabIndex = 3;
            this.btnAnswerA.Text = "A";
            this.btnAnswerA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnswerA.UseVisualStyleBackColor = false;
            this.btnAnswerA.Click += new System.EventHandler(this.btnAnswerA_Click);
            this.btnAnswerA.MouseEnter += new System.EventHandler(this.btnAnswerA_MouseEnter);
            this.btnAnswerA.MouseLeave += new System.EventHandler(this.btnAnswerA_MouseLeave);
            // 
            // btnAnswerB
            // 
            this.btnAnswerB.BackColor = System.Drawing.Color.Moccasin;
            this.btnAnswerB.FlatAppearance.BorderSize = 0;
            this.btnAnswerB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnswerB.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnswerB.Location = new System.Drawing.Point(57, 462);
            this.btnAnswerB.Name = "btnAnswerB";
            this.btnAnswerB.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnAnswerB.Size = new System.Drawing.Size(350, 70);
            this.btnAnswerB.TabIndex = 4;
            this.btnAnswerB.Text = "B";
            this.btnAnswerB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnswerB.UseVisualStyleBackColor = false;
            this.btnAnswerB.Click += new System.EventHandler(this.btnAnswerB_Click);
            this.btnAnswerB.MouseEnter += new System.EventHandler(this.btnAnswerB_MouseEnter);
            this.btnAnswerB.MouseLeave += new System.EventHandler(this.btnAnswerB_MouseLeave);
            // 
            // btnAnswerC
            // 
            this.btnAnswerC.BackColor = System.Drawing.Color.Moccasin;
            this.btnAnswerC.FlatAppearance.BorderSize = 0;
            this.btnAnswerC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnswerC.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnswerC.Location = new System.Drawing.Point(557, 301);
            this.btnAnswerC.Name = "btnAnswerC";
            this.btnAnswerC.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnAnswerC.Size = new System.Drawing.Size(350, 70);
            this.btnAnswerC.TabIndex = 5;
            this.btnAnswerC.Text = "C";
            this.btnAnswerC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnswerC.UseVisualStyleBackColor = false;
            this.btnAnswerC.Click += new System.EventHandler(this.btnAnswerC_Click);
            this.btnAnswerC.MouseEnter += new System.EventHandler(this.btnAnswerC_MouseEnter);
            this.btnAnswerC.MouseLeave += new System.EventHandler(this.btnAnswerC_MouseLeave);
            // 
            // btnAnswerD
            // 
            this.btnAnswerD.BackColor = System.Drawing.Color.Moccasin;
            this.btnAnswerD.FlatAppearance.BorderSize = 0;
            this.btnAnswerD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnswerD.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnswerD.Location = new System.Drawing.Point(557, 462);
            this.btnAnswerD.Name = "btnAnswerD";
            this.btnAnswerD.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnAnswerD.Size = new System.Drawing.Size(350, 70);
            this.btnAnswerD.TabIndex = 6;
            this.btnAnswerD.Text = "D";
            this.btnAnswerD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnswerD.UseVisualStyleBackColor = false;
            this.btnAnswerD.Click += new System.EventHandler(this.btnAnswerD_Click);
            this.btnAnswerD.MouseEnter += new System.EventHandler(this.btnAnswerD_MouseEnter);
            this.btnAnswerD.MouseLeave += new System.EventHandler(this.btnAnswerD_MouseLeave);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.SkyBlue;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(57, 575);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 50);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.SkyBlue;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnNext.Location = new System.Drawing.Point(757, 575);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(150, 50);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Next ➡";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // quizTimer
            // 
            this.quizTimer.Interval = 1000;
            this.quizTimer.Tick += new System.EventHandler(this.quizTimer_Tick);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(817, 239);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(90, 32);
            this.lblTimer.TabIndex = 9;
            this.lblTimer.Text = "⏱ 30s";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(978, 744);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAnswerD);
            this.Controls.Add(this.btnAnswerC);
            this.Controls.Add(this.btnAnswerB);
            this.Controls.Add(this.btnAnswerA);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form1";
            this.Text = "Quiz Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button btnAnswerA;
        private System.Windows.Forms.Button btnAnswerB;
        private System.Windows.Forms.Button btnAnswerC;
        private System.Windows.Forms.Button btnAnswerD;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Timer quizTimer;
        private System.Windows.Forms.Label lblTimer;
    }
}

