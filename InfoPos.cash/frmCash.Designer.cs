namespace InfoPos.Cash
{
    partial class frmCash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCash));
            this.TabMAIN = new DevExpress.XtraTab.XtraTabControl();
            this.TabChoice = new DevExpress.XtraTab.XtraTabPage();
            this.TouchChoice = new ISM.Touch.TouchButtonGroup();
            this.TabCustomerSearch = new DevExpress.XtraTab.XtraTabPage();
            this.ucCustSearch1 = new InfoPos.Panels.ucCustSearch();
            this.TabOrderSearch = new DevExpress.XtraTab.XtraTabPage();
            this.ucOrderSearch1 = new InfoPos.Order.ucOrderSearch();
            this.TabRegSearch = new DevExpress.XtraTab.XtraTabPage();
            this.ucContractSearch1 = new InfoPos.Panels.ucContractSearch();
            this.TabSales = new DevExpress.XtraTab.XtraTabPage();
            this.TouchProductMenu = new ISM.Touch.TouchButtonGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ucSalesProd1 = new InfoPos.Panels.ucSalesProd();
            this.ucSalesPayment1 = new InfoPos.fo_panels.ucSalesPayment();
            this.TabOtherPayment = new DevExpress.XtraTab.XtraTabPage();
            this.ucPayment1 = new InfoPos.Panels.ucPayment();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.TabMAIN)).BeginInit();
            this.TabMAIN.SuspendLayout();
            this.TabChoice.SuspendLayout();
            this.TabCustomerSearch.SuspendLayout();
            this.TabOrderSearch.SuspendLayout();
            this.TabRegSearch.SuspendLayout();
            this.TabSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.TabOtherPayment.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabMAIN
            // 
            this.TabMAIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabMAIN.Location = new System.Drawing.Point(0, 0);
            this.TabMAIN.Name = "TabMAIN";
            this.TabMAIN.SelectedTabPage = this.TabChoice;
            this.TabMAIN.Size = new System.Drawing.Size(678, 450);
            this.TabMAIN.TabIndex = 0;
            this.TabMAIN.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.TabChoice,
            this.TabCustomerSearch,
            this.TabOrderSearch,
            this.TabRegSearch,
            this.TabSales,
            this.TabOtherPayment,
            this.xtraTabPage1});
            // 
            // TabChoice
            // 
            this.TabChoice.Controls.Add(this.TouchChoice);
            this.TabChoice.Name = "TabChoice";
            this.TabChoice.Size = new System.Drawing.Size(672, 423);
            this.TabChoice.Text = "Сонголт";
            // 
            // TouchChoice
            // 
            this.TouchChoice.BurronBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TouchChoice.BurronForeColor = System.Drawing.SystemColors.ButtonFace;
            this.TouchChoice.ButtonTrackFont = new System.Drawing.Font("Tahoma", 10F);
            this.TouchChoice.ButtonTrackHeigth = 26;
            this.TouchChoice.ButtonTrackText = "";
            this.TouchChoice.ButtonTrackVisible = true;
            this.TouchChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TouchChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TouchChoice.Location = new System.Drawing.Point(0, 0);
            this.TouchChoice.Margin = new System.Windows.Forms.Padding(5);
            this.TouchChoice.Name = "TouchChoice";
            this.TouchChoice.ParentMDI = null;
            this.TouchChoice.Size = new System.Drawing.Size(672, 423);
            this.TouchChoice.TabIndex = 1;
            // 
            // TabCustomerSearch
            // 
            this.TabCustomerSearch.Controls.Add(this.ucCustSearch1);
            this.TabCustomerSearch.Name = "TabCustomerSearch";
            this.TabCustomerSearch.Size = new System.Drawing.Size(672, 423);
            this.TabCustomerSearch.Text = "Харилцагч сонгох";
            // 
            // ucCustSearch1
            // 
            this.ucCustSearch1.Core = null;
            this.ucCustSearch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCustSearch1.Location = new System.Drawing.Point(0, 0);
            this.ucCustSearch1.Name = "ucCustSearch1";
            this.ucCustSearch1.PageRows = 20;
            this.ucCustSearch1.Remote = null;
            this.ucCustSearch1.Resource = null;
            this.ucCustSearch1.Size = new System.Drawing.Size(672, 423);
            this.ucCustSearch1.TabIndex = 0;
            this.ucCustSearch1.TouchKeyboard = null;
            // 
            // TabOrderSearch
            // 
            this.TabOrderSearch.Controls.Add(this.ucOrderSearch1);
            this.TabOrderSearch.Name = "TabOrderSearch";
            this.TabOrderSearch.Size = new System.Drawing.Size(672, 423);
            this.TabOrderSearch.Text = "Захиалга хайх";
            // 
            // ucOrderSearch1
            // 
            this.ucOrderSearch1.Core = null;
            this.ucOrderSearch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrderSearch1.Location = new System.Drawing.Point(0, 0);
            this.ucOrderSearch1.Name = "ucOrderSearch1";
            this.ucOrderSearch1.PageRows = 20;
            this.ucOrderSearch1.Remote = null;
            this.ucOrderSearch1.Resource = null;
            this.ucOrderSearch1.Size = new System.Drawing.Size(672, 423);
            this.ucOrderSearch1.TabIndex = 0;
            this.ucOrderSearch1.TouchKeyboard = null;
            // 
            // TabRegSearch
            // 
            this.TabRegSearch.Controls.Add(this.ucContractSearch1);
            this.TabRegSearch.Name = "TabRegSearch";
            this.TabRegSearch.Size = new System.Drawing.Size(672, 423);
            this.TabRegSearch.Text = "Бүртгэл хайх";
            // 
            // ucContractSearch1
            // 
            this.ucContractSearch1.Core = null;
            this.ucContractSearch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucContractSearch1.Location = new System.Drawing.Point(0, 0);
            this.ucContractSearch1.Name = "ucContractSearch1";
            this.ucContractSearch1.PageRows = 20;
            this.ucContractSearch1.Remote = null;
            this.ucContractSearch1.Resource = null;
            this.ucContractSearch1.Size = new System.Drawing.Size(672, 423);
            this.ucContractSearch1.TabIndex = 0;
            this.ucContractSearch1.TouchKeyboard = null;
            // 
            // TabSales
            // 
            this.TabSales.Controls.Add(this.TouchProductMenu);
            this.TabSales.Controls.Add(this.panelControl1);
            this.TabSales.Name = "TabSales";
            this.TabSales.Size = new System.Drawing.Size(672, 423);
            this.TabSales.Text = "Сагс";
            // 
            // TouchProductMenu
            // 
            this.TouchProductMenu.BurronBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TouchProductMenu.BurronForeColor = System.Drawing.SystemColors.ButtonFace;
            this.TouchProductMenu.ButtonTrackFont = new System.Drawing.Font("Tahoma", 10F);
            this.TouchProductMenu.ButtonTrackHeigth = 26;
            this.TouchProductMenu.ButtonTrackText = "";
            this.TouchProductMenu.ButtonTrackVisible = true;
            this.TouchProductMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TouchProductMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TouchProductMenu.Location = new System.Drawing.Point(467, 0);
            this.TouchProductMenu.Margin = new System.Windows.Forms.Padding(5);
            this.TouchProductMenu.Name = "TouchProductMenu";
            this.TouchProductMenu.ParentMDI = null;
            this.TouchProductMenu.Size = new System.Drawing.Size(205, 423);
            this.TouchProductMenu.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ucSalesProd1);
            this.panelControl1.Controls.Add(this.ucSalesPayment1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(467, 423);
            this.panelControl1.TabIndex = 1;
            // 
            // ucSalesProd1
            // 
            this.ucSalesProd1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSalesProd1.Location = new System.Drawing.Point(2, 2);
            this.ucSalesProd1.Name = "ucSalesProd1";
            this.ucSalesProd1.Remote = null;
            this.ucSalesProd1.Resource = null;
            this.ucSalesProd1.Size = new System.Drawing.Size(463, 193);
            this.ucSalesProd1.TabIndex = 0;
            this.ucSalesProd1.TouchKeyboard = null;
            // 
            // ucSalesPayment1
            // 
            this.ucSalesPayment1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucSalesPayment1.Location = new System.Drawing.Point(2, 195);
            this.ucSalesPayment1.Name = "ucSalesPayment1";
            this.ucSalesPayment1.Size = new System.Drawing.Size(463, 226);
            this.ucSalesPayment1.TabIndex = 1;
            // 
            // TabOtherPayment
            // 
            this.TabOtherPayment.Controls.Add(this.ucPayment1);
            this.TabOtherPayment.Name = "TabOtherPayment";
            this.TabOtherPayment.Size = new System.Drawing.Size(672, 423);
            this.TabOtherPayment.Text = "Бусад төлбөр";
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
            this.ucPayment1.Size = new System.Drawing.Size(672, 423);
            this.ucPayment1.TabIndex = 0;
            this.ucPayment1.TouchKeyboard = null;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(672, 423);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // frmCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 450);
            this.Controls.Add(this.TabMAIN);
            this.Name = "frmCash";
            this.Text = "frmCash";
            ((System.ComponentModel.ISupportInitialize)(this.TabMAIN)).EndInit();
            this.TabMAIN.ResumeLayout(false);
            this.TabChoice.ResumeLayout(false);
            this.TabCustomerSearch.ResumeLayout(false);
            this.TabOrderSearch.ResumeLayout(false);
            this.TabRegSearch.ResumeLayout(false);
            this.TabSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.TabOtherPayment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl TabMAIN;
        private DevExpress.XtraTab.XtraTabPage TabCustomerSearch;
        private DevExpress.XtraTab.XtraTabPage TabSales;
        private Panels.ucCustSearch ucCustSearch1;
        private ISM.Touch.TouchButtonGroup TouchProductMenu;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Panels.ucSalesProd ucSalesProd1;
        private DevExpress.XtraTab.XtraTabPage TabChoice;
        private DevExpress.XtraTab.XtraTabPage TabOrderSearch;
        private DevExpress.XtraTab.XtraTabPage TabRegSearch;
        private fo_panels.ucSalesPayment ucSalesPayment1;
        private DevExpress.XtraTab.XtraTabPage TabOtherPayment;
        private Panels.ucPayment ucPayment1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private Order.ucOrderSearch ucOrderSearch1;
        private Panels.ucContractSearch ucContractSearch1;
        private ISM.Touch.TouchButtonGroup TouchChoice;
    }
}