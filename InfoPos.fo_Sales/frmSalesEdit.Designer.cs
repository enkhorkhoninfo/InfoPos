namespace InfoPos.sales
{
    partial class frmSalesEdit
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitProd = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.numExtendAmount = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.numVat = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.numPaid = new DevExpress.XtraEditors.CalcEdit();
            this.numRemain = new DevExpress.XtraEditors.CalcEdit();
            this.numTotal = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.numDiscount = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.numSales = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabMainPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.tabMainPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTab1Cust = new DevExpress.XtraEditors.SimpleButton();
            this.btnTab1Contract = new DevExpress.XtraEditors.SimpleButton();
            this.btnTab1Order = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitProd)).BeginInit();
            this.splitProd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExtendAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRemain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabMainPage2.SuspendLayout();
            this.tabMainPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl1.Panel1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel1.ShowCaption = true;
            this.splitContainerControl1.Panel1.Text = "Борлуулсан бараа үйлчилгээ";
            this.splitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainerControl1.Panel2.Controls.Add(this.splitProd);
            this.splitContainerControl1.Panel2.ShowCaption = true;
            this.splitContainerControl1.Panel2.Text = "Төлбөрийн гүйлгээ";
            this.splitContainerControl1.ShowCaption = true;
            this.splitContainerControl1.Size = new System.Drawing.Size(946, 377);
            this.splitContainerControl1.SplitterPosition = 481;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.DataMember = "_cart";
            this.gridControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(371, 222);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.RowHeight = 28;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;
            // 
            // splitProd
            // 
            this.splitProd.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitProd.Horizontal = false;
            this.splitProd.Location = new System.Drawing.Point(12, 23);
            this.splitProd.LookAndFeel.UseDefaultLookAndFeel = false;
            this.splitProd.Margin = new System.Windows.Forms.Padding(4);
            this.splitProd.Name = "splitProd";
            this.splitProd.Panel1.Controls.Add(this.gridControl2);
            this.splitProd.Panel2.Controls.Add(this.labelControl1);
            this.splitProd.Panel2.Text = "Panel2";
            this.splitProd.Size = new System.Drawing.Size(463, 325);
            this.splitProd.SplitterPosition = 149;
            this.splitProd.TabIndex = 0;
            this.splitProd.Text = "splitContainerControl2";
            // 
            // gridControl2
            // 
            this.gridControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl2.DataMember = "_cart";
            this.gridControl2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl2.Location = new System.Drawing.Point(16, 12);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(381, 147);
            this.gridControl2.TabIndex = 0;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowFilter = false;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsCustomization.AllowSort = false;
            this.gridView2.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowDetailButtons = false;
            this.gridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.RowHeight = 28;
            this.gridView2.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(-49, 226);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(132, 30);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Хуудас: ";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.numExtendAmount);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.numVat);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.numPaid);
            this.panelControl1.Controls.Add(this.numRemain);
            this.panelControl1.Controls.Add(this.numTotal);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.numDiscount);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.numSales);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 377);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(946, 68);
            this.panelControl1.TabIndex = 0;
            // 
            // numExtendAmount
            // 
            this.numExtendAmount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numExtendAmount.Location = new System.Drawing.Point(822, 5);
            this.numExtendAmount.Name = "numExtendAmount";
            this.numExtendAmount.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.numExtendAmount.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.numExtendAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numExtendAmount.Properties.Appearance.Options.UseBackColor = true;
            this.numExtendAmount.Properties.Appearance.Options.UseBorderColor = true;
            this.numExtendAmount.Properties.Appearance.Options.UseFont = true;
            this.numExtendAmount.Properties.AutoHeight = false;
            this.numExtendAmount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numExtendAmount.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numExtendAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numExtendAmount.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numExtendAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numExtendAmount.Properties.ReadOnly = true;
            this.numExtendAmount.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.numExtendAmount.Size = new System.Drawing.Size(141, 29);
            this.numExtendAmount.TabIndex = 13;
            this.numExtendAmount.TabStop = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl4.Location = new System.Drawing.Point(731, 5);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(92, 29);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = " Сунгалт:";
            // 
            // numVat
            // 
            this.numVat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numVat.Location = new System.Drawing.Point(334, 33);
            this.numVat.Name = "numVat";
            this.numVat.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.numVat.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.numVat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numVat.Properties.Appearance.Options.UseBackColor = true;
            this.numVat.Properties.Appearance.Options.UseBorderColor = true;
            this.numVat.Properties.Appearance.Options.UseFont = true;
            this.numVat.Properties.AutoHeight = false;
            this.numVat.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numVat.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numVat.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numVat.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numVat.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numVat.Properties.ReadOnly = true;
            this.numVat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.numVat.Size = new System.Drawing.Size(141, 29);
            this.numVat.TabIndex = 7;
            this.numVat.TabStop = false;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl9.Location = new System.Drawing.Point(243, 33);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(92, 29);
            this.labelControl9.TabIndex = 6;
            this.labelControl9.Text = " НӨАТ:";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl7.Location = new System.Drawing.Point(481, 33);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(92, 29);
            this.labelControl7.TabIndex = 10;
            this.labelControl7.Text = " Үлдэгдэл:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl6.Location = new System.Drawing.Point(481, 5);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(92, 29);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = " Төлөгдсөн:";
            // 
            // numPaid
            // 
            this.numPaid.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPaid.Location = new System.Drawing.Point(572, 5);
            this.numPaid.Name = "numPaid";
            this.numPaid.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.numPaid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPaid.Properties.Appearance.Options.UseBorderColor = true;
            this.numPaid.Properties.Appearance.Options.UseFont = true;
            this.numPaid.Properties.AutoHeight = false;
            this.numPaid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numPaid.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numPaid.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numPaid.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numPaid.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numPaid.Properties.ReadOnly = true;
            this.numPaid.Size = new System.Drawing.Size(141, 29);
            this.numPaid.TabIndex = 9;
            this.numPaid.TabStop = false;
            // 
            // numRemain
            // 
            this.numRemain.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numRemain.Location = new System.Drawing.Point(572, 33);
            this.numRemain.Name = "numRemain";
            this.numRemain.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.numRemain.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.numRemain.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRemain.Properties.Appearance.Options.UseBackColor = true;
            this.numRemain.Properties.Appearance.Options.UseBorderColor = true;
            this.numRemain.Properties.Appearance.Options.UseFont = true;
            this.numRemain.Properties.AutoHeight = false;
            this.numRemain.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numRemain.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numRemain.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numRemain.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numRemain.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numRemain.Properties.ReadOnly = true;
            this.numRemain.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.numRemain.Size = new System.Drawing.Size(141, 29);
            this.numRemain.TabIndex = 11;
            this.numRemain.TabStop = false;
            // 
            // numTotal
            // 
            this.numTotal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTotal.Location = new System.Drawing.Point(334, 5);
            this.numTotal.Name = "numTotal";
            this.numTotal.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.numTotal.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.numTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotal.Properties.Appearance.Options.UseBackColor = true;
            this.numTotal.Properties.Appearance.Options.UseBorderColor = true;
            this.numTotal.Properties.Appearance.Options.UseFont = true;
            this.numTotal.Properties.AutoHeight = false;
            this.numTotal.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numTotal.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numTotal.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numTotal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numTotal.Properties.ReadOnly = true;
            this.numTotal.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.numTotal.Size = new System.Drawing.Size(141, 29);
            this.numTotal.TabIndex = 5;
            this.numTotal.TabStop = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl5.Location = new System.Drawing.Point(243, 5);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(92, 29);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = " Авах:";
            // 
            // numDiscount
            // 
            this.numDiscount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDiscount.Location = new System.Drawing.Point(96, 33);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.numDiscount.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.numDiscount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDiscount.Properties.Appearance.Options.UseBackColor = true;
            this.numDiscount.Properties.Appearance.Options.UseBorderColor = true;
            this.numDiscount.Properties.Appearance.Options.UseFont = true;
            this.numDiscount.Properties.AutoHeight = false;
            this.numDiscount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numDiscount.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numDiscount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numDiscount.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numDiscount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numDiscount.Properties.ReadOnly = true;
            this.numDiscount.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.numDiscount.Size = new System.Drawing.Size(141, 29);
            this.numDiscount.TabIndex = 3;
            this.numDiscount.TabStop = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl3.Location = new System.Drawing.Point(5, 33);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(92, 29);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = " Хям/Хөн:";
            // 
            // numSales
            // 
            this.numSales.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numSales.Location = new System.Drawing.Point(96, 5);
            this.numSales.Name = "numSales";
            this.numSales.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.numSales.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.numSales.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSales.Properties.Appearance.Options.UseBackColor = true;
            this.numSales.Properties.Appearance.Options.UseBorderColor = true;
            this.numSales.Properties.Appearance.Options.UseFont = true;
            this.numSales.Properties.AutoHeight = false;
            this.numSales.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numSales.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numSales.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numSales.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numSales.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numSales.Properties.ReadOnly = true;
            this.numSales.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.numSales.Size = new System.Drawing.Size(141, 29);
            this.numSales.TabIndex = 1;
            this.numSales.TabStop = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl2.Location = new System.Drawing.Point(5, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(92, 29);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = " Борл. дүн:";
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabMainPage2;
            this.tabMain.Size = new System.Drawing.Size(951, 471);
            this.tabMain.TabIndex = 16;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMainPage1,
            this.tabMainPage2});
            // 
            // tabMainPage2
            // 
            this.tabMainPage2.Controls.Add(this.splitContainerControl1);
            this.tabMainPage2.Controls.Add(this.panelControl1);
            this.tabMainPage2.Name = "tabMainPage2";
            this.tabMainPage2.Size = new System.Drawing.Size(946, 445);
            this.tabMainPage2.Text = "Борлуулалт";
            // 
            // tabMainPage1
            // 
            this.tabMainPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabMainPage1.Name = "tabMainPage1";
            this.tabMainPage1.PageVisible = false;
            this.tabMainPage1.Size = new System.Drawing.Size(946, 445);
            this.tabMainPage1.Text = "Үйлчилгээний төрөл";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.21942F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.56116F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.21942F));
            this.tableLayoutPanel1.Controls.Add(this.btnTab1Cust, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTab1Contract, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnTab1Order, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(927, 274);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // btnTab1Cust
            // 
            this.btnTab1Cust.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTab1Cust.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.btnTab1Cust.Appearance.Options.UseFont = true;
            this.btnTab1Cust.Appearance.Options.UseForeColor = true;
            this.btnTab1Cust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTab1Cust.Location = new System.Drawing.Point(162, 3);
            this.btnTab1Cust.Name = "btnTab1Cust";
            this.btnTab1Cust.Size = new System.Drawing.Size(601, 85);
            this.btnTab1Cust.TabIndex = 6;
            this.btnTab1Cust.Text = "Барьцаа сонгох";
            // 
            // btnTab1Contract
            // 
            this.btnTab1Contract.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTab1Contract.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.btnTab1Contract.Appearance.Options.UseFont = true;
            this.btnTab1Contract.Appearance.Options.UseForeColor = true;
            this.btnTab1Contract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTab1Contract.Location = new System.Drawing.Point(162, 94);
            this.btnTab1Contract.Name = "btnTab1Contract";
            this.btnTab1Contract.Size = new System.Drawing.Size(601, 85);
            this.btnTab1Contract.TabIndex = 7;
            this.btnTab1Contract.Text = "Гэрээ";
            // 
            // btnTab1Order
            // 
            this.btnTab1Order.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTab1Order.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.btnTab1Order.Appearance.Options.UseFont = true;
            this.btnTab1Order.Appearance.Options.UseForeColor = true;
            this.btnTab1Order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTab1Order.Location = new System.Drawing.Point(162, 185);
            this.btnTab1Order.Name = "btnTab1Order";
            this.btnTab1Order.Size = new System.Drawing.Size(601, 86);
            this.btnTab1Order.TabIndex = 8;
            this.btnTab1Order.Text = "Захиалга";
            // 
            // frmSalesEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(951, 471);
            this.Controls.Add(this.tabMain);
            this.Name = "frmSalesEdit";
            this.Text = "Борлуулалт";
            this.Load += new System.EventHandler(this.frmSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitProd)).EndInit();
            this.splitProd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numExtendAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRemain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabMainPage2.ResumeLayout(false);
            this.tabMainPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SplitContainerControl splitProd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CalcEdit numSales;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabMainPage1;
        private DevExpress.XtraTab.XtraTabPage tabMainPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btnTab1Cust;
        private DevExpress.XtraEditors.SimpleButton btnTab1Contract;
        private DevExpress.XtraEditors.SimpleButton btnTab1Order;
        private DevExpress.XtraEditors.CalcEdit numTotal;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CalcEdit numDiscount;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CalcEdit numVat;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CalcEdit numPaid;
        private DevExpress.XtraEditors.CalcEdit numRemain;
        private DevExpress.XtraEditors.CalcEdit numExtendAmount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}