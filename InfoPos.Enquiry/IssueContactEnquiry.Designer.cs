namespace InfoPos.Enquiry
{
    partial class IssueContactEnquiry
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.grdIssueContact = new DevExpress.XtraGrid.GridControl();
            this.gvwIssueContact = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnexitb = new DevExpress.XtraEditors.SimpleButton();
            this.btnexit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdIssueContact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwIssueContact)).BeginInit();
            this.SuspendLayout();
            // 
            // grdIssueContact
            // 
            this.grdIssueContact.Dock = System.Windows.Forms.DockStyle.Top;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.grdIssueContact.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.grdIssueContact.Location = new System.Drawing.Point(0, 0);
            this.grdIssueContact.MainView = this.gvwIssueContact;
            this.grdIssueContact.Name = "grdIssueContact";
            this.grdIssueContact.Size = new System.Drawing.Size(434, 524);
            this.grdIssueContact.TabIndex = 4;
            this.grdIssueContact.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwIssueContact});
            // 
            // gvwIssueContact
            // 
            this.gvwIssueContact.GridControl = this.grdIssueContact;
            this.gvwIssueContact.Name = "gvwIssueContact";
            // 
            // btnexitb
            // 
            this.btnexitb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexitb.Location = new System.Drawing.Point(456, 1092);
            this.btnexitb.Name = "btnexitb";
            this.btnexitb.Size = new System.Drawing.Size(84, 23);
            this.btnexitb.TabIndex = 5;
            this.btnexitb.Text = "Хаах";
            this.btnexitb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnexit_KeyDown);
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(338, 531);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(84, 23);
            this.btnexit.TabIndex = 6;
            this.btnexit.Text = "Хаах";
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // IssueContactEnquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 562);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.btnexitb);
            this.Controls.Add(this.grdIssueContact);
            this.Name = "IssueContactEnquiry";
            this.Text = "Холбоо барисан харилцагчийн асуудлын лавалгаа";
            this.Load += new System.EventHandler(this.IssueContactEnquiry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdIssueContact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwIssueContact)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdIssueContact;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwIssueContact;
        private DevExpress.XtraEditors.SimpleButton btnexitb;
        private DevExpress.XtraEditors.SimpleButton btnexit;
    }
}