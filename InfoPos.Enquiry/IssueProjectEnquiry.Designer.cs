namespace InfoPos.Enquiry
{
    partial class IssueProjectEnquiry
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
            this.grdIssueProject = new DevExpress.XtraGrid.GridControl();
            this.gvwIssueProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnexit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdIssueProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwIssueProject)).BeginInit();
            this.SuspendLayout();
            // 
            // grdIssueProject
            // 
            this.grdIssueProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.grdIssueProject.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.grdIssueProject.Location = new System.Drawing.Point(1, 0);
            this.grdIssueProject.MainView = this.gvwIssueProject;
            this.grdIssueProject.Name = "grdIssueProject";
            this.grdIssueProject.Size = new System.Drawing.Size(332, 524);
            this.grdIssueProject.TabIndex = 2;
            this.grdIssueProject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwIssueProject});
            // 
            // gvwIssueProject
            // 
            this.gvwIssueProject.GridControl = this.grdIssueProject;
            this.gvwIssueProject.Name = "gvwIssueProject";
            // 
            // btnexit
            // 
            this.btnexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnexit.Location = new System.Drawing.Point(238, 530);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(84, 23);
            this.btnexit.TabIndex = 3;
            this.btnexit.Text = "Хаах";
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click_1);
            // 
            // IssueProjectEnquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 562);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.grdIssueProject);
            this.MaximumSize = new System.Drawing.Size(350, 600);
            this.MinimumSize = new System.Drawing.Size(350, 600);
            this.Name = "IssueProjectEnquiry";
            this.Text = "Төслийн лавалгаа";
            this.Load += new System.EventHandler(this.IssueProjectEnquiry_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IssueProjectEnquiry_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdIssueProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwIssueProject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdIssueProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwIssueProject;
        private DevExpress.XtraEditors.SimpleButton btnexit;
    }
}