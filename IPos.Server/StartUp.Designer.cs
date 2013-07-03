namespace Development
{
    partial class StartUp
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPrepare = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnEODStart = new System.Windows.Forms.Button();
            this.btnEODList = new System.Windows.Forms.Button();
            this.btnEODClear = new System.Windows.Forms.Button();
            this.txtLogProcess = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.AcceptsReturn = true;
            this.txtLog.AcceptsTab = true;
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(519, 310);
            this.txtLog.TabIndex = 0;
            this.txtLog.WordWrap = false;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(527, 74);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(99, 30);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Зогсоох";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(527, 38);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(99, 30);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Эхлүүлэх";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPrepare
            // 
            this.btnPrepare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrepare.Location = new System.Drawing.Point(527, 3);
            this.btnPrepare.Name = "btnPrepare";
            this.btnPrepare.Size = new System.Drawing.Size(99, 30);
            this.btnPrepare.TabIndex = 4;
            this.btnPrepare.Text = "Бэлтгэх";
            this.btnPrepare.UseVisualStyleBackColor = true;
            this.btnPrepare.Click += new System.EventHandler(this.btnPrepare_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(527, 141);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(99, 30);
            this.btnClearLog.TabIndex = 7;
            this.btnClearLog.Text = "Лог цэвэрлэх";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(638, 345);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtLog);
            this.tabPage1.Controls.Add(this.btnClearLog);
            this.tabPage1.Controls.Add(this.btnPrepare);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(630, 319);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Сервер";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnEODStart);
            this.tabPage2.Controls.Add(this.btnEODList);
            this.tabPage2.Controls.Add(this.btnEODClear);
            this.tabPage2.Controls.Add(this.txtLogProcess);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(630, 319);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Өндөрлөлт";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnEODStart
            // 
            this.btnEODStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEODStart.Location = new System.Drawing.Point(527, 39);
            this.btnEODStart.Name = "btnEODStart";
            this.btnEODStart.Size = new System.Drawing.Size(99, 30);
            this.btnEODStart.TabIndex = 10;
            this.btnEODStart.Text = "Өндөрлөх";
            this.btnEODStart.UseVisualStyleBackColor = true;
            this.btnEODStart.Click += new System.EventHandler(this.btnEODStart_Click);
            // 
            // btnEODList
            // 
            this.btnEODList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEODList.Location = new System.Drawing.Point(527, 3);
            this.btnEODList.Name = "btnEODList";
            this.btnEODList.Size = new System.Drawing.Size(99, 30);
            this.btnEODList.TabIndex = 9;
            this.btnEODList.Text = "Жагсаалт";
            this.btnEODList.UseVisualStyleBackColor = true;
            this.btnEODList.Click += new System.EventHandler(this.btnEODList_Click);
            // 
            // btnEODClear
            // 
            this.btnEODClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEODClear.Location = new System.Drawing.Point(527, 75);
            this.btnEODClear.Name = "btnEODClear";
            this.btnEODClear.Size = new System.Drawing.Size(99, 30);
            this.btnEODClear.TabIndex = 8;
            this.btnEODClear.Text = "Лог цэвэрлэх";
            this.btnEODClear.UseVisualStyleBackColor = true;
            this.btnEODClear.Click += new System.EventHandler(this.btnClearLogProcess_Click);
            // 
            // txtLogProcess
            // 
            this.txtLogProcess.AcceptsReturn = true;
            this.txtLogProcess.AcceptsTab = true;
            this.txtLogProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogProcess.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogProcess.Location = new System.Drawing.Point(3, 3);
            this.txtLogProcess.Multiline = true;
            this.txtLogProcess.Name = "txtLogProcess";
            this.txtLogProcess.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogProcess.Size = new System.Drawing.Size(519, 310);
            this.txtLogProcess.TabIndex = 1;
            this.txtLogProcess.WordWrap = false;
            // 
            // StartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 351);
            this.Controls.Add(this.tabControl1);
            this.Name = "StartUp";
            this.Text = "InfoPos Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartUp_FormClosed);
            this.Load += new System.EventHandler(this.StartUp_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPrepare;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtLogProcess;
        private System.Windows.Forms.Button btnEODClear;
        private System.Windows.Forms.Button btnEODList;
        private System.Windows.Forms.Button btnEODStart;
    }
}