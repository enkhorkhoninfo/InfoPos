namespace InfoPos.List
{
    partial class LogList
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.ucLogList = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(18, 398);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(119, 28);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Дэлгэрэнгүй";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ucLogList
            // 
            this.ucLogList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLogList.Browsable = false;
            this.ucLogList.Location = new System.Drawing.Point(-2, -3);
            this.ucLogList.Name = "ucLogList";
            this.ucLogList.PageRows = 100;
            this.ucLogList.Size = new System.Drawing.Size(722, 396);
            this.ucLogList.TabIndex = 1;
            this.ucLogList.VisibleFilter = false;
            this.ucLogList.VisibleFind = true;
            // 
            // LogList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 438);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ucLogList);
            this.KeyPreview = true;
            this.Name = "LogList";
            this.Text = "Логийн бүртгэлийн жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogList_FormClosing);
            this.Load += new System.EventHandler(this.LogList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogList_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public ISM.Template.ucGridPanel ucLogList;
    }
}