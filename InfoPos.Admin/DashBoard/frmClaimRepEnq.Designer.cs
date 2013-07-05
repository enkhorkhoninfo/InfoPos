namespace InfoPos.Admin.DashBoard
{
    partial class frmClaimRepEnq
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
            this.grdClaimRepEnq = new DevExpress.XtraGrid.GridControl();
            this.gvwClaimRepEnq = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaimRepEnq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwClaimRepEnq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdClaimRepEnq);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(510, 228);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Лавлагаа жагсаалт";
            // 
            // grdClaimRepEnq
            // 
            this.grdClaimRepEnq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClaimRepEnq.Location = new System.Drawing.Point(2, 22);
            this.grdClaimRepEnq.MainView = this.gvwClaimRepEnq;
            this.grdClaimRepEnq.Name = "grdClaimRepEnq";
            this.grdClaimRepEnq.Size = new System.Drawing.Size(506, 204);
            this.grdClaimRepEnq.TabIndex = 0;
            this.grdClaimRepEnq.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwClaimRepEnq});
            // 
            // gvwClaimRepEnq
            // 
            this.gvwClaimRepEnq.GridControl = this.grdClaimRepEnq;
            this.gvwClaimRepEnq.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwClaimRepEnq.Name = "gvwClaimRepEnq";
            this.gvwClaimRepEnq.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 228);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(510, 30);
            this.panelControl1.TabIndex = 1;
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
            // frmClaimRepEnq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 258);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(518, 285);
            this.Name = "frmClaimRepEnq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Хэрэглэгчийн хариуцсан нөхөн төлбөрийн лавлагаа";
            this.Load += new System.EventHandler(this.frmDealRepEnq_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDealRepEnq_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaimRepEnq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwClaimRepEnq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdClaimRepEnq;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwClaimRepEnq;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnExit;
    }
}