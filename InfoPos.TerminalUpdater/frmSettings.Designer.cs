namespace InfoPos.TerminalUpdater
{
    partial class frmSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.txtTerminalPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.numUserNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.numTimeOut = new DevExpress.XtraEditors.TextEdit();
            this.numPort = new DevExpress.XtraEditors.TextEdit();
            this.txtServer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.btnclose = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTerminalPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUserNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.txtTerminalPath);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.numUserNo);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.numTimeOut);
            this.groupBox1.Controls.Add(this.numPort);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(350, 123);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(36, 23);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtTerminalPath
            // 
            this.txtTerminalPath.Enabled = false;
            this.txtTerminalPath.Location = new System.Drawing.Point(157, 125);
            this.txtTerminalPath.Name = "txtTerminalPath";
            this.txtTerminalPath.Size = new System.Drawing.Size(178, 20);
            this.txtTerminalPath.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(11, 128);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(138, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Терминал байрлах хавтас :";
            // 
            // numUserNo
            // 
            this.numUserNo.Location = new System.Drawing.Point(157, 100);
            this.numUserNo.Name = "numUserNo";
            this.numUserNo.Size = new System.Drawing.Size(178, 20);
            this.numUserNo.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 103);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(113, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Хэрэглэгчийн дугаар :";
            // 
            // numTimeOut
            // 
            this.numTimeOut.Location = new System.Drawing.Point(157, 74);
            this.numTimeOut.Name = "numTimeOut";
            this.numTimeOut.Size = new System.Drawing.Size(178, 20);
            this.numTimeOut.TabIndex = 5;
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(157, 48);
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(178, 20);
            this.numPort.TabIndex = 4;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(157, 22);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(178, 20);
            this.txtServer.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Хүлээх хугацаа :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(32, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Порт :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Сервэр :";
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(212, 164);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(92, 23);
            this.btnsave.TabIndex = 6;
            this.btnsave.Text = "Хадгалах";
            this.btnsave.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(310, 164);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(92, 23);
            this.btnclose.TabIndex = 7;
            this.btnclose.Text = "Буцах";
            this.btnclose.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 191);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тохиргоо";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTerminalPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUserNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit numTimeOut;
        private DevExpress.XtraEditors.TextEdit numPort;
        private DevExpress.XtraEditors.TextEdit txtServer;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.SimpleButton btnclose;
        private DevExpress.XtraEditors.TextEdit numUserNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.TextEdit txtTerminalPath;
        private DevExpress.XtraEditors.LabelControl labelControl5;

    }
}