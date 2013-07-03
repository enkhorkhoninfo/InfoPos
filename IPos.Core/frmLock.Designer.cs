namespace ISM.CUser
{
    partial class frmLock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLock));
            this.password = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.userName = new System.Windows.Forms.TextBox();
            this.passwordC = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.userNameC = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(197, 146);
            this.password.MaxLength = 30;
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(155, 20);
            this.password.TabIndex = 0;
            this.password.UseSystemPasswordChar = true;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(364, 183);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 13;
            this.closeButton.Text = "Гарах";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click_1);
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(197, 122);
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            this.userName.Size = new System.Drawing.Size(155, 20);
            this.userName.TabIndex = 15;
            // 
            // passwordC
            // 
            this.passwordC.AutoSize = true;
            this.passwordC.BackColor = System.Drawing.Color.White;
            this.passwordC.Location = new System.Drawing.Point(129, 150);
            this.passwordC.Name = "passwordC";
            this.passwordC.Size = new System.Drawing.Size(44, 13);
            this.passwordC.TabIndex = 12;
            this.passwordC.Text = "Нууц үг";
            // 
            // loginButton
            // 
            this.loginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loginButton.Location = new System.Drawing.Point(283, 183);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 11;
            this.loginButton.Text = "Нэвтрэх";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click_1);
            // 
            // userNameC
            // 
            this.userNameC.AutoSize = true;
            this.userNameC.BackColor = System.Drawing.Color.White;
            this.userNameC.Location = new System.Drawing.Point(129, 126);
            this.userNameC.Name = "userNameC";
            this.userNameC.Size = new System.Drawing.Size(59, 13);
            this.userNameC.TabIndex = 14;
            this.userNameC.Text = "Хэрэглэгч";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, -8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 279);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 218);
            this.Controls.Add(this.password);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.passwordC);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.userNameC);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLock";
            this.Activated += new System.EventHandler(this.frmLock_Activated);
            this.Load += new System.EventHandler(this.frmLock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label passwordC;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label userNameC;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}