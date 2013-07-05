namespace InfoPos.Payment
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ucSaleSearch1 = new InfoPos.Panels.ucSaleSearch();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.ucBill1 = new InfoPos.Bill.ucBill();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ucPayment1 = new InfoPos.Panels.ucPayment();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.xtraTabPage1;
            this.tabMain.Size = new System.Drawing.Size(792, 473);
            this.tabMain.TabIndex = 0;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.labelControl4);
            this.xtraTabPage1.Controls.Add(this.ucSaleSearch1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(786, 446);
            this.xtraTabPage1.Text = "SalesSearch";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelControl4.Location = new System.Drawing.Point(6, 3);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(211, 17);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Төлбөр - Борлуулалт сонгох";
            // 
            // ucSaleSearch1
            // 
            this.ucSaleSearch1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucSaleSearch1.Core = null;
            this.ucSaleSearch1.Location = new System.Drawing.Point(3, 26);
            this.ucSaleSearch1.Name = "ucSaleSearch1";
            this.ucSaleSearch1.PageRows = 20;
            this.ucSaleSearch1.Remote = null;
            this.ucSaleSearch1.Resource = null;
            this.ucSaleSearch1.Size = new System.Drawing.Size(783, 420);
            this.ucSaleSearch1.TabIndex = 0;
            this.ucSaleSearch1.TouchKeyboard = null;
            this.ucSaleSearch1.Load += new System.EventHandler(this.ucSaleSearch1_Load);
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.labelControl5);
            this.xtraTabPage2.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(786, 446);
            this.xtraTabPage2.Text = "Payment";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelControl5.Location = new System.Drawing.Point(6, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(283, 17);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Төлбөр - Борлуулалтын төлбөр төлөх";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Location = new System.Drawing.Point(6, 26);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.ucPayment1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(773, 413);
            this.splitContainerControl1.SplitterPosition = 0;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(0, 0);
            this.splitContainerControl2.SplitterPosition = 0;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // ucPayment1
            // 
            this.ucPayment1.BatchNo = null;
            this.ucPayment1.Core = null;
            this.ucPayment1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPayment1.Location = new System.Drawing.Point(0, 0);
            this.ucPayment1.Name = "ucPayment1";
            this.ucPayment1.PageRows = 20;
            this.ucPayment1.Remote = null;
            this.ucPayment1.Resource = null;
            this.ucPayment1.Size = new System.Drawing.Size(768, 413);
            this.ucPayment1.TabIndex = 0;
            this.ucPayment1.TouchKeyboard = null;
            // 
            // xtraTabPage3
            //
            this.xtraTabPage3.Controls.Add(this.ucBill1);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(786, 446);
            this.xtraTabPage3.Text = "Билл хэвлэх";
            // 
            // ucBill1
            // 
            this.ucBill1.BatchNo = null;
            this.ucBill1.core = null;
            this.ucBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBill1.kb = null;
            this.ucBill1.Location = new System.Drawing.Point(0, 0);
            this.ucBill1.Name = "ucBill1";
            this.ucBill1.PageRows = 20;
            this.ucBill1.remote = null;
            this.ucBill1.SalesNo = null;
            this.ucBill1.Size = new System.Drawing.Size(609, 546);
            this.ucBill1.TabIndex = 0;
            // 
            // frmBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 473);
            this.Controls.Add(this.tabMain);
            this.Name = "frmBill";
            this.Text = "frmPayment";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Panels.ucSaleSearch ucSaleSearch1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Panels.ucPayment ucPayment1;
        private InfoPos.Bill.ucBill ucBill1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
    }
}