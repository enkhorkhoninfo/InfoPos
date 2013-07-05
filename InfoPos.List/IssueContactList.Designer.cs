namespace InfoPos.List
{
    partial class IssueContact
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
            this.ucIssueContactList = new ISM.Template.ucGridPanel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // ucIssueContactList
            // 
            this.ucIssueContactList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucIssueContactList.Browsable = false;
            this.ucIssueContactList.Location = new System.Drawing.Point(-2, 0);
            this.ucIssueContactList.Name = "ucIssueContactList";
            this.ucIssueContactList.PageRows = 100;
            this.ucIssueContactList.Size = new System.Drawing.Size(614, 370);
            this.ucIssueContactList.TabIndex = 1;
            this.ucIssueContactList.VisibleFilter = false;
            this.ucIssueContactList.VisibleFind = true;
            this.ucIssueContactList.Load += new System.EventHandler(this.ucIssueList_Load);
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
            // IssueContact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 414);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ucIssueContactList);
            this.KeyPreview = true;
            this.Name = "IssueContact";
            this.Text = "Холбоо барьсан харилцагчийн асуудлын жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Issue_FormClosing);
            this.Load += new System.EventHandler(this.IssueList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Issue_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucIssueContactList;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;

    }
}