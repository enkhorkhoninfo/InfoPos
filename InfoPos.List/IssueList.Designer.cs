namespace InfoPos.List
{
    partial class Issue
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
            this.ucIssueList = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(12, 376);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(106, 26);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Лавлагаа";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ucIssueList
            // 
            this.ucIssueList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucIssueList.Browsable = false;
            this.ucIssueList.Location = new System.Drawing.Point(-2, 0);
            this.ucIssueList.Name = "ucIssueList";
            this.ucIssueList.PageRows = 100;
            this.ucIssueList.Size = new System.Drawing.Size(614, 370);
            this.ucIssueList.TabIndex = 1;
            this.ucIssueList.VisibleFilter = false;
            this.ucIssueList.VisibleFind = true;
            this.ucIssueList.Load += new System.EventHandler(this.ucIssueList_Load);
            // 
            // Issue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 414);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ucIssueList);
            this.KeyPreview = true;
            this.Name = "Issue";
            this.Text = "Харилцагчийн асуудлын жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Issue_FormClosing);
            this.Load += new System.EventHandler(this.IssueList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Issue_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucIssueList;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;

    }
}