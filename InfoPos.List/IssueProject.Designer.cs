namespace InfoPos.List
{
    partial class IssueProject
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
            this.ucIssueProjectList = new ISM.Template.ucGridPanel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // ucIssueProjectList
            // 
            this.ucIssueProjectList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucIssueProjectList.Browsable = false;
            this.ucIssueProjectList.Location = new System.Drawing.Point(1, 1);
            this.ucIssueProjectList.Name = "ucIssueProjectList";
            this.ucIssueProjectList.PageRows = 100;
            this.ucIssueProjectList.Size = new System.Drawing.Size(569, 341);
            this.ucIssueProjectList.TabIndex = 2;
            this.ucIssueProjectList.VisibleFilter = false;
            this.ucIssueProjectList.VisibleFind = true;
            this.ucIssueProjectList.Load += new System.EventHandler(this.ucIssueProjectList_Load);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(1, 348);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(106, 26);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Лавлагаа";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton2.Location = new System.Drawing.Point(125, 348);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(106, 26);
            this.simpleButton2.TabIndex = 4;
            this.simpleButton2.Text = "Issue";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // IssueProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 387);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ucIssueProjectList);
            this.KeyPreview = true;
            this.Name = "IssueProject";
            this.Text = "Төслийн жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IssueProject_FormClosing);
            this.Load += new System.EventHandler(this.IssueProject_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IssueProject_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucIssueProjectList;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}