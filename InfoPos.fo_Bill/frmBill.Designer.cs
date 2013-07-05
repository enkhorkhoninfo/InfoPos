namespace InfoPos.bill
{
    partial class frmBill
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
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.ucSaleSearchToBill = new InfoPos.Panels.ucSaleSearch();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.ucBill = new InfoPos.Bill.ucBill();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.xtraTabPage1;
            this.tabMain.Size = new System.Drawing.Size(705, 451);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.ucSaleSearchToBill);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(699, 424);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // ucSaleSearchToBill
            // 
            this.ucSaleSearchToBill.Core = null;
            this.ucSaleSearchToBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSaleSearchToBill.Location = new System.Drawing.Point(0, 0);
            this.ucSaleSearchToBill.Name = "ucSaleSearchToBill";
            this.ucSaleSearchToBill.PageRows = 20;
            this.ucSaleSearchToBill.Remote = null;
            this.ucSaleSearchToBill.Resource = null;
            this.ucSaleSearchToBill.Size = new System.Drawing.Size(699, 424);
            this.ucSaleSearchToBill.TabIndex = 2;
            this.ucSaleSearchToBill.TouchKeyboard = null;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.ucBill);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(699, 424);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // ucBill
            // 
            this.ucBill.BatchNo = null;
            this.ucBill.core = null;
            this.ucBill.Data = null;
            this.ucBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBill.kb = null;
            this.ucBill.Location = new System.Drawing.Point(0, 0);
            this.ucBill.Name = "ucBill";
            this.ucBill.PageRows = 20;
            this.ucBill.remote = null;
            this.ucBill.SalesNo = null;
            this.ucBill.Size = new System.Drawing.Size(699, 424);
            this.ucBill.TabIndex = 0;
            // 
            // frmBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 451);
            this.Controls.Add(this.tabMain);
            this.Name = "frmBill";
            this.Text = "Билл хэвлэх";
            this.Load += new System.EventHandler(this.frmBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Panels.ucSaleSearch ucSaleSearchToBill;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Bill.ucBill ucBill;

    }
}