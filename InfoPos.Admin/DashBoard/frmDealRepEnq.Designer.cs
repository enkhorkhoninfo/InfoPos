namespace InfoPos.Admin.DashBoard
{
    partial class frmDealRepEnq
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grdDealRepEnq = new DevExpress.XtraGrid.GridControl();
            this.gvwDealRepEnq = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDealRepEnq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwDealRepEnq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdDealRepEnq);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(510, 228);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Лавлагаа жагсаалт";
            // 
            // grdDealRepEnq
            // 
            this.grdDealRepEnq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDealRepEnq.Location = new System.Drawing.Point(2, 22);
            this.grdDealRepEnq.MainView = this.gvwDealRepEnq;
            this.grdDealRepEnq.Name = "grdDealRepEnq";
            this.grdDealRepEnq.Size = new System.Drawing.Size(506, 204);
            this.grdDealRepEnq.TabIndex = 0;
            this.grdDealRepEnq.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwDealRepEnq});
            // 
            // gvwDealRepEnq
            // 
            this.gvwDealRepEnq.GridControl = this.grdDealRepEnq;
            this.gvwDealRepEnq.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwDealRepEnq.Name = "gvwDealRepEnq";
            this.gvwDealRepEnq.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 228);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(510, 30);
            this.panelControl1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(420, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 24);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Гарах";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmDealRepEnq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 258);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(518, 285);
            this.Name = "frmDealRepEnq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Хэрэглэгчийн хариуцсан хэлцлийн лавлагаа";
            this.Load += new System.EventHandler(this.frmDealRepEnq_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDealRepEnq_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDealRepEnq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwDealRepEnq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdDealRepEnq;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwDealRepEnq;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnExit;
    }
}