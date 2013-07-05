namespace InfoPos.sales
{
    partial class frmMaster
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
            this.TabMain = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.ucMaster1 = new InfoPos.Panels.ucMaster();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.TabMain)).BeginInit();
            this.TabMain.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabMain
            // 
            this.TabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabMain.Location = new System.Drawing.Point(0, 0);
            this.TabMain.Name = "TabMain";
            this.TabMain.SelectedTabPage = this.xtraTabPage1;
            this.TabMain.Size = new System.Drawing.Size(833, 524);
            this.TabMain.TabIndex = 0;
            this.TabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.ucMaster1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(827, 497);
            this.xtraTabPage1.Text = "Мастерийн бүртгэл";
            // 
            // ucMaster1
            // 
            this.ucMaster1.Core = null;
            this.ucMaster1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMaster1.Location = new System.Drawing.Point(0, 0);
            this.ucMaster1.Name = "ucMaster1";
            this.ucMaster1.Remote = null;
            this.ucMaster1.Resource = null;
            this.ucMaster1.Size = new System.Drawing.Size(827, 497);
            this.ucMaster1.TabIndex = 0;
            this.ucMaster1.TouchKeyboard = null;
            this.ucMaster1.Load += new System.EventHandler(this.ucMaster1_Load);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(827, 497);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // frmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 524);
            this.Controls.Add(this.TabMain);
            this.Name = "frmMaster";
            this.Text = "frmMaster";
            this.Load += new System.EventHandler(this.frmMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TabMain)).EndInit();
            this.TabMain.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl TabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Panels.ucMaster ucMaster1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;

    }
}