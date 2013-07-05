namespace InfoPos.sales
{
    partial class frmRefundProduct
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
            this.pnlTop = new DevExpress.XtraEditors.SplitContainerControl();
            this.txtProdName = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnRefundSales = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.numTotal = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.numRefund = new DevExpress.XtraEditors.CalcEdit();
            this.numQty = new DevExpress.XtraEditors.CalcEdit();
            this.numPrice = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtProdNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtSalesNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.numEdit = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numDiscount = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.numRefundAmt = new DevExpress.XtraEditors.CalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefund.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefundAmt.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.pnlTop.Horizontal = false;
            this.pnlTop.Location = new System.Drawing.Point(294, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.pnlTop.Panel1.Controls.Add(this.labelControl7);
            this.pnlTop.Panel1.Controls.Add(this.labelControl6);
            this.pnlTop.Panel1.Controls.Add(this.numRefundAmt);
            this.pnlTop.Panel1.Controls.Add(this.numEdit);
            this.pnlTop.Panel1.Controls.Add(this.gridControl1);
            this.pnlTop.Panel1.ShowCaption = true;
            this.pnlTop.Panel1.Text = "Буцаах төлбөр:";
            this.pnlTop.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.pnlTop.Panel2.Controls.Add(this.gridControl2);
            this.pnlTop.Panel2.ShowCaption = true;
            this.pnlTop.Panel2.Text = "Дагалдах хэрэгсэл:";
            this.pnlTop.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.pnlTop.Size = new System.Drawing.Size(496, 420);
            this.pnlTop.SplitterPosition = 226;
            this.pnlTop.TabIndex = 7;
            this.pnlTop.Text = "splitContainerControl1";
            // 
            // txtProdName
            // 
            this.txtProdName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProdName.Location = new System.Drawing.Point(140, 97);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdName.Properties.Appearance.Options.UseFont = true;
            this.txtProdName.Properties.AutoHeight = false;
            this.txtProdName.Properties.ReadOnly = true;
            this.txtProdName.Size = new System.Drawing.Size(133, 28);
            this.txtProdName.TabIndex = 14;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(7, 72);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(478, 317);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // gridControl2
            // 
            this.gridControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl2.Location = new System.Drawing.Point(7, 20);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(0, 0);
            this.gridControl2.TabIndex = 7;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView2.OptionsFilter.AllowFilterEditor = false;
            this.gridView2.OptionsFilter.AllowMRUFilterList = false;
            this.gridView2.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView2.OptionsView.ShowDetailButtons = false;
            this.gridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // btnRefundSales
            // 
            this.btnRefundSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefundSales.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefundSales.Appearance.Options.UseFont = true;
            this.btnRefundSales.Location = new System.Drawing.Point(15, 301);
            this.btnRefundSales.Name = "btnRefundSales";
            this.btnRefundSales.Size = new System.Drawing.Size(252, 54);
            this.btnRefundSales.TabIndex = 30;
            this.btnRefundSales.Text = "Буцаалт хийх";
            this.btnRefundSales.Click += new System.EventHandler(this.btnRefundSales_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl3.Location = new System.Drawing.Point(15, 166);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 16);
            this.labelControl3.TabIndex = 24;
            this.labelControl3.Text = "Тоо ширхэг:";
            // 
            // numTotal
            // 
            this.numTotal.Location = new System.Drawing.Point(140, 190);
            this.numTotal.Name = "numTotal";
            this.numTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotal.Properties.Appearance.Options.UseFont = true;
            this.numTotal.Properties.AutoHeight = false;
            this.numTotal.Properties.ReadOnly = true;
            this.numTotal.Size = new System.Drawing.Size(133, 28);
            this.numTotal.TabIndex = 23;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl8.Location = new System.Drawing.Point(15, 197);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(57, 16);
            this.labelControl8.TabIndex = 22;
            this.labelControl8.Text = "Нийт дүн:";
            // 
            // numRefund
            // 
            this.numRefund.Location = new System.Drawing.Point(140, 252);
            this.numRefund.Name = "numRefund";
            this.numRefund.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRefund.Properties.Appearance.Options.UseFont = true;
            this.numRefund.Properties.AutoHeight = false;
            this.numRefund.Size = new System.Drawing.Size(133, 28);
            this.numRefund.TabIndex = 18;
            // 
            // numQty
            // 
            this.numQty.Location = new System.Drawing.Point(140, 159);
            this.numQty.Name = "numQty";
            this.numQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQty.Properties.Appearance.Options.UseFont = true;
            this.numQty.Properties.AutoHeight = false;
            this.numQty.Properties.ReadOnly = true;
            this.numQty.Size = new System.Drawing.Size(133, 28);
            this.numQty.TabIndex = 17;
            // 
            // numPrice
            // 
            this.numPrice.Location = new System.Drawing.Point(140, 128);
            this.numPrice.Name = "numPrice";
            this.numPrice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrice.Properties.Appearance.Options.UseFont = true;
            this.numPrice.Properties.AutoHeight = false;
            this.numPrice.Properties.ReadOnly = true;
            this.numPrice.Size = new System.Drawing.Size(133, 28);
            this.numPrice.TabIndex = 14;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl5.Location = new System.Drawing.Point(15, 259);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(115, 16);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Буцаах тоо ширхэг:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl1.Location = new System.Drawing.Point(15, 135);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Нэгжийн үнэ:";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.numDiscount);
            this.groupControl1.Controls.Add(this.txtProdNo);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtProdName);
            this.groupControl1.Controls.Add(this.btnCancel);
            this.groupControl1.Controls.Add(this.txtSalesNo);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.btnRefundSales);
            this.groupControl1.Controls.Add(this.numTotal);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.numPrice);
            this.groupControl1.Controls.Add(this.numRefund);
            this.groupControl1.Controls.Add(this.numQty);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(285, 420);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Борлуулалтын мэдээлэл:";
            // 
            // txtProdNo
            // 
            this.txtProdNo.Location = new System.Drawing.Point(140, 66);
            this.txtProdNo.Name = "txtProdNo";
            this.txtProdNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdNo.Properties.Appearance.Options.UseFont = true;
            this.txtProdNo.Properties.AutoHeight = false;
            this.txtProdNo.Properties.ReadOnly = true;
            this.txtProdNo.Size = new System.Drawing.Size(133, 28);
            this.txtProdNo.TabIndex = 15;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelControl4.Location = new System.Drawing.Point(15, 70);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(118, 16);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Бүтээгдэхүүний код:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(15, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(252, 54);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Болих";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtSalesNo
            // 
            this.txtSalesNo.Location = new System.Drawing.Point(140, 35);
            this.txtSalesNo.Name = "txtSalesNo";
            this.txtSalesNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesNo.Properties.Appearance.Options.UseFont = true;
            this.txtSalesNo.Properties.AutoHeight = false;
            this.txtSalesNo.Properties.ReadOnly = true;
            this.txtSalesNo.Size = new System.Drawing.Size(133, 28);
            this.txtSalesNo.TabIndex = 33;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl14.Location = new System.Drawing.Point(15, 42);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(92, 16);
            this.labelControl14.TabIndex = 26;
            this.labelControl14.Text = "Борлуулалт №:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl6.Location = new System.Drawing.Point(26, 46);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(70, 16);
            this.labelControl6.TabIndex = 37;
            this.labelControl6.Text = "Буцаах дүн:";
            // 
            // numEdit
            // 
            this.numEdit.Location = new System.Drawing.Point(136, 39);
            this.numEdit.Name = "numEdit";
            this.numEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numEdit.Properties.Appearance.Options.UseFont = true;
            this.numEdit.Properties.AutoHeight = false;
            this.numEdit.Size = new System.Drawing.Size(182, 28);
            this.numEdit.TabIndex = 36;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl2.Location = new System.Drawing.Point(15, 228);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 16);
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "Хөнгөлөлт:";
            // 
            // numDiscount
            // 
            this.numDiscount.Location = new System.Drawing.Point(140, 221);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDiscount.Properties.Appearance.Options.UseFont = true;
            this.numDiscount.Properties.AutoHeight = false;
            this.numDiscount.Properties.ReadOnly = true;
            this.numDiscount.Size = new System.Drawing.Size(133, 28);
            this.numDiscount.TabIndex = 36;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl7.Location = new System.Drawing.Point(26, 15);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(91, 16);
            this.labelControl7.TabIndex = 37;
            this.labelControl7.Text = "Буцаах төлбөр:";
            // 
            // numRefundAmt
            // 
            this.numRefundAmt.Location = new System.Drawing.Point(136, 8);
            this.numRefundAmt.Name = "numRefundAmt";
            this.numRefundAmt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRefundAmt.Properties.Appearance.Options.UseFont = true;
            this.numRefundAmt.Properties.AutoHeight = false;
            this.numRefundAmt.Properties.ReadOnly = true;
            this.numRefundAmt.Size = new System.Drawing.Size(182, 28);
            this.numRefundAmt.TabIndex = 38;
            // 
            // frmRefundProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(793, 426);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmRefundProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Бараа үйлчилгээ буцаах";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtProdName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefund.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefundAmt.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl pnlTop;
        private DevExpress.XtraEditors.SimpleButton btnRefundSales;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CalcEdit numTotal;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.CalcEdit numRefund;
        private DevExpress.XtraEditors.CalcEdit numQty;
        private DevExpress.XtraEditors.CalcEdit numPrice;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.TextEdit txtSalesNo;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        public DevExpress.XtraEditors.TextEdit txtProdName;
        public DevExpress.XtraEditors.TextEdit txtProdNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CalcEdit numEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CalcEdit numDiscount;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CalcEdit numRefundAmt;

    }
}