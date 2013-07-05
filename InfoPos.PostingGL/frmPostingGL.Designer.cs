namespace InfoPos.PostingGL
{
    partial class frmPostingGL
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtProgress = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTxnDate = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 142);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Нууц үг:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(92, 85);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(177, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Text = "sa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Хэрэглэгч:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(92, 63);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(177, 20);
            this.txtUsername.TabIndex = 5;
            this.txtUsername.Text = "sa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Бааз ID:";
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(92, 41);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(177, 20);
            this.txtDB.TabIndex = 3;
            this.txtDB.Text = "demo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Сервер:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(92, 19);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(177, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "192.168.1.2";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(296, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(487, 459);
            this.listBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 35);
            this.button2.TabIndex = 9;
            this.button2.Text = "Харилцагч нэгтгэх";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtProgress
            // 
            this.txtProgress.Location = new System.Drawing.Point(296, 7);
            this.txtProgress.Name = "txtProgress";
            this.txtProgress.Size = new System.Drawing.Size(487, 15);
            this.txtProgress.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 105);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 35);
            this.button3.TabIndex = 11;
            this.button3.Text = "Гүйлгээг татах";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(194, 64);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 35);
            this.button4.TabIndex = 12;
            this.button4.Text = "Цэвэрлэх";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.txtTxnDate);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(3, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 151);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Огноо:";
            // 
            // txtTxnDate
            // 
            this.txtTxnDate.Location = new System.Drawing.Point(194, 19);
            this.txtTxnDate.Name = "txtTxnDate";
            this.txtTxnDate.Size = new System.Drawing.Size(75, 20);
            this.txtTxnDate.TabIndex = 1;
            this.txtTxnDate.Text = "2011.07.07";
            // 
            // frmPostingGL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 495);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtProgress);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPostingGL";
            this.Text = "Ерөнхий дэвтэр лүү пост хийх";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPostingGL_FormClosing);
            this.Load += new System.EventHandler(this.frmPostingGL_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar txtProgress;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTxnDate;
    }
}