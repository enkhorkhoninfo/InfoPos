namespace InfoPos.sales
{
    partial class frmPayment
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
            this.components = new System.ComponentModel.Container();
            this.numPayment = new DevExpress.XtraEditors.CalcEdit();
            this.lblChange = new DevExpress.XtraEditors.LabelControl();
            this.numChange = new DevExpress.XtraEditors.CalcEdit();
            this.txtRegNo = new DevExpress.XtraEditors.TextEdit();
            this.lblRegNo = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnPayment = new DevExpress.XtraEditors.SimpleButton();
            this.numRemain = new DevExpress.XtraEditors.CalcEdit();
            this.numPrepaid = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.numTotal = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtSalesNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.ucNumpad1 = new InfoPos.Panels.ucNumpad();
            this.dropDownButton1 = new DevExpress.XtraEditors.DropDownButton();
            this.popupContainer1 = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barLargeButtonItem1 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.txtDetail = new DevExpress.XtraEditors.TextEdit();
            this.lblDetail = new DevExpress.XtraEditors.LabelControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.numPaid = new DevExpress.XtraEditors.CalcEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.numPayment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRemain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrepaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainer1)).BeginInit();
            this.popupContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // numPayment
            // 
            this.numPayment.Location = new System.Drawing.Point(112, 82);
            this.numPayment.Name = "numPayment";
            this.numPayment.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.numPayment.Properties.Appearance.BorderColor = System.Drawing.Color.Green;
            this.numPayment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPayment.Properties.Appearance.Options.UseBackColor = true;
            this.numPayment.Properties.Appearance.Options.UseBorderColor = true;
            this.numPayment.Properties.Appearance.Options.UseFont = true;
            this.numPayment.Properties.AutoHeight = false;
            this.numPayment.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numPayment.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numPayment.Properties.Mask.EditMask = "#,###,###,###";
            this.numPayment.Size = new System.Drawing.Size(162, 33);
            this.numPayment.TabIndex = 3;
            this.numPayment.EditValueChanged += new System.EventHandler(this.numPayment_EditValueChanged);
            this.numPayment.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.numPayment_EditValueChanging);
            // 
            // lblChange
            // 
            this.lblChange.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblChange.Location = new System.Drawing.Point(21, 49);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(75, 19);
            this.lblChange.TabIndex = 0;
            this.lblChange.Text = "Хариулт:";
            // 
            // numChange
            // 
            this.numChange.Location = new System.Drawing.Point(112, 43);
            this.numChange.Name = "numChange";
            this.numChange.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.numChange.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.numChange.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numChange.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.numChange.Properties.Appearance.Options.UseBackColor = true;
            this.numChange.Properties.Appearance.Options.UseBorderColor = true;
            this.numChange.Properties.Appearance.Options.UseFont = true;
            this.numChange.Properties.Appearance.Options.UseForeColor = true;
            this.numChange.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.numChange.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.numChange.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.numChange.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.numChange.Properties.AutoHeight = false;
            this.numChange.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numChange.Properties.Mask.EditMask = "#,###,###,###";
            this.numChange.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numChange.Properties.NullText = "-- хариулт --";
            this.numChange.Properties.Precision = 2;
            this.numChange.Properties.ReadOnly = true;
            this.numChange.Size = new System.Drawing.Size(162, 33);
            this.numChange.TabIndex = 1;
            // 
            // txtRegNo
            // 
            this.txtRegNo.Location = new System.Drawing.Point(14, 172);
            this.txtRegNo.Name = "txtRegNo";
            this.txtRegNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtRegNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtRegNo.Properties.Appearance.Options.UseFont = true;
            this.txtRegNo.Properties.AutoHeight = false;
            this.txtRegNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRegNo.Size = new System.Drawing.Size(173, 33);
            this.txtRegNo.TabIndex = 10;
            // 
            // lblRegNo
            // 
            this.lblRegNo.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegNo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblRegNo.Location = new System.Drawing.Point(16, 149);
            this.lblRegNo.Name = "lblRegNo";
            this.lblRegNo.Size = new System.Drawing.Size(118, 19);
            this.lblRegNo.TabIndex = 9;
            this.lblRegNo.Text = "Бүртгэл дугаар:";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 27);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(474, 146);
            this.gridControl1.TabIndex = 0;
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
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // btnPayment
            // 
            this.btnPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPayment.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayment.Appearance.Options.UseFont = true;
            this.btnPayment.Location = new System.Drawing.Point(489, 413);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(142, 54);
            this.btnPayment.TabIndex = 5;
            this.btnPayment.Text = "Төлөх";
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // numRemain
            // 
            this.numRemain.Location = new System.Drawing.Point(331, 67);
            this.numRemain.Name = "numRemain";
            this.numRemain.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRemain.Properties.Appearance.Options.UseFont = true;
            this.numRemain.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Maroon;
            this.numRemain.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.numRemain.Properties.AutoHeight = false;
            this.numRemain.Properties.Mask.EditMask = "#,###,###,###";
            this.numRemain.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numRemain.Properties.ReadOnly = true;
            this.numRemain.Size = new System.Drawing.Size(124, 28);
            this.numRemain.TabIndex = 7;
            this.numRemain.TabStop = false;
            // 
            // numPrepaid
            // 
            this.numPrepaid.Location = new System.Drawing.Point(331, 35);
            this.numPrepaid.Name = "numPrepaid";
            this.numPrepaid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrepaid.Properties.Appearance.Options.UseFont = true;
            this.numPrepaid.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Maroon;
            this.numPrepaid.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.numPrepaid.Properties.AutoHeight = false;
            this.numPrepaid.Properties.Mask.EditMask = "#,###,###,###";
            this.numPrepaid.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numPrepaid.Properties.ReadOnly = true;
            this.numPrepaid.Size = new System.Drawing.Size(124, 28);
            this.numPrepaid.TabIndex = 5;
            this.numPrepaid.TabStop = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl7.Location = new System.Drawing.Point(278, 40);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(47, 16);
            this.labelControl7.TabIndex = 4;
            this.labelControl7.Text = "Төлсөн:";
            // 
            // numTotal
            // 
            this.numTotal.Location = new System.Drawing.Point(112, 67);
            this.numTotal.Name = "numTotal";
            this.numTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotal.Properties.Appearance.Options.UseFont = true;
            this.numTotal.Properties.AutoHeight = false;
            this.numTotal.Properties.Mask.EditMask = "#,###,###,###";
            this.numTotal.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numTotal.Properties.ReadOnly = true;
            this.numTotal.Size = new System.Drawing.Size(124, 28);
            this.numTotal.TabIndex = 3;
            this.numTotal.TabStop = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl4.Location = new System.Drawing.Point(268, 71);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(57, 16);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Үлдэгдэл:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl1.Location = new System.Drawing.Point(28, 71);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Нийт төлбөр:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(639, 413);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(142, 54);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Буцах";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtSalesNo
            // 
            this.txtSalesNo.Location = new System.Drawing.Point(112, 35);
            this.txtSalesNo.Name = "txtSalesNo";
            this.txtSalesNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesNo.Properties.Appearance.Options.UseFont = true;
            this.txtSalesNo.Properties.AutoHeight = false;
            this.txtSalesNo.Properties.ReadOnly = true;
            this.txtSalesNo.Size = new System.Drawing.Size(124, 28);
            this.txtSalesNo.TabIndex = 1;
            this.txtSalesNo.TabStop = false;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl14.Location = new System.Drawing.Point(14, 40);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(92, 16);
            this.labelControl14.TabIndex = 0;
            this.labelControl14.Text = "Борлуулалт №:";
            // 
            // ucNumpad1
            // 
            this.ucNumpad1.ExtraShow1 = true;
            this.ucNumpad1.ExtraShow2 = true;
            this.ucNumpad1.ExtraShow3 = true;
            this.ucNumpad1.ExtraText1 = "";
            this.ucNumpad1.ExtraText2 = "Ж+";
            this.ucNumpad1.ExtraText3 = "Ж‒";
            this.ucNumpad1.Location = new System.Drawing.Point(13, 128);
            this.ucNumpad1.Name = "ucNumpad1";
            this.ucNumpad1.Size = new System.Drawing.Size(262, 265);
            this.ucNumpad1.TabIndex = 4;
            this.ucNumpad1.EventClickExtraButton += new InfoPos.Panels.ucNumpad.DelegateEvenClickExtraButton(this.ucNumpad1_EventClickExtraButton);
            // 
            // dropDownButton1
            // 
            this.dropDownButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownButton1.Appearance.Options.UseFont = true;
            //this.dropDownButton1.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Show;
            this.dropDownButton1.DropDownControl = this.popupContainer1;
            this.dropDownButton1.Location = new System.Drawing.Point(14, 106);
            this.dropDownButton1.MenuManager = this.barManager1;
            this.dropDownButton1.Name = "dropDownButton1";
            this.dropDownButton1.Size = new System.Drawing.Size(441, 41);
            this.dropDownButton1.TabIndex = 8;
            this.dropDownButton1.Text = "Төлбөрийн төрөл сонгоно уу...";
            // 
            // popupContainer1
            // 
            this.popupContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.popupContainer1.Controls.Add(this.gridControl2);
            this.popupContainer1.Location = new System.Drawing.Point(854, 7);
            this.popupContainer1.Name = "popupContainer1";
            this.popupContainer1.Size = new System.Drawing.Size(214, 302);
            this.popupContainer1.TabIndex = 10;
            this.popupContainer1.Visible = false;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(214, 302);
            this.gridControl2.TabIndex = 11;
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
            this.gridView2.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsCustomization.AllowColumnMoving = false;
            this.gridView2.OptionsCustomization.AllowColumnResizing = false;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView2.OptionsView.ShowColumnHeaders = false;
            this.gridView2.OptionsView.ShowDetailButtons = false;
            this.gridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barToolbarsListItem1,
            this.barCheckItem1,
            this.barLargeButtonItem1});
            this.barManager1.MaxItemId = 3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(787, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 474);
            this.barDockControlBottom.Size = new System.Drawing.Size(787, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 474);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(787, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 474);
            // 
            // barToolbarsListItem1
            // 
            this.barToolbarsListItem1.Caption = "hhhh";
            this.barToolbarsListItem1.Id = 0;
            this.barToolbarsListItem1.Name = "barToolbarsListItem1";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "aaaa";
            this.barCheckItem1.Checked = true;
            this.barCheckItem1.Id = 1;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barLargeButtonItem1
            // 
            this.barLargeButtonItem1.Caption = "hhhhhhhhhh";
            this.barLargeButtonItem1.Id = 2;
            this.barLargeButtonItem1.Name = "barLargeButtonItem1";
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl3.Controls.Add(this.txtDetail);
            this.groupControl3.Controls.Add(this.txtSalesNo);
            this.groupControl3.Controls.Add(this.lblDetail);
            this.groupControl3.Controls.Add(this.numTotal);
            this.groupControl3.Controls.Add(this.labelControl14);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.numPrepaid);
            this.groupControl3.Controls.Add(this.labelControl7);
            this.groupControl3.Controls.Add(this.numRemain);
            this.groupControl3.Controls.Add(this.txtRegNo);
            this.groupControl3.Controls.Add(this.lblRegNo);
            this.groupControl3.Controls.Add(this.dropDownButton1);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Location = new System.Drawing.Point(6, 6);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(478, 222);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "Төлбөрийн мэдээлэл:";
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(193, 172);
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetail.Properties.Appearance.Options.UseFont = true;
            this.txtDetail.Properties.AutoHeight = false;
            this.txtDetail.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDetail.Size = new System.Drawing.Size(262, 33);
            this.txtDetail.TabIndex = 12;
            // 
            // lblDetail
            // 
            this.lblDetail.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetail.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblDetail.Location = new System.Drawing.Point(193, 149);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(121, 19);
            this.lblDetail.TabIndex = 11;
            this.lblDetail.Text = "Бусад мэдээлэл:";
            // 
            // groupControl4
            // 
            this.groupControl4.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl4.AppearanceCaption.Options.UseFont = true;
            this.groupControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl4.Controls.Add(this.gridControl1);
            this.groupControl4.Location = new System.Drawing.Point(6, 234);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(478, 175);
            this.groupControl4.TabIndex = 1;
            this.groupControl4.Text = "Төлбөрийн жагсаалт:";
            // 
            // groupControl5
            // 
            this.groupControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl5.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl5.AppearanceCaption.Options.UseFont = true;
            this.groupControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.groupControl5.Controls.Add(this.numChange);
            this.groupControl5.Controls.Add(this.labelControl10);
            this.groupControl5.Controls.Add(this.numPayment);
            this.groupControl5.Controls.Add(this.lblChange);
            this.groupControl5.Controls.Add(this.ucNumpad1);
            this.groupControl5.Location = new System.Drawing.Point(489, 6);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(292, 401);
            this.groupControl5.TabIndex = 4;
            this.groupControl5.Text = "Төлбөрийн дүн оруулах:";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControl10.Location = new System.Drawing.Point(21, 88);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(57, 19);
            this.labelControl10.TabIndex = 2;
            this.labelControl10.Text = "Төлөх:";
            // 
            // numPaid
            // 
            this.numPaid.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPaid.Location = new System.Drawing.Point(44, 413);
            this.numPaid.Name = "numPaid";
            this.numPaid.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.numPaid.Properties.Appearance.BorderColor = System.Drawing.Color.Green;
            this.numPaid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPaid.Properties.Appearance.ForeColor = System.Drawing.Color.Green;
            this.numPaid.Properties.Appearance.Options.UseBackColor = true;
            this.numPaid.Properties.Appearance.Options.UseBorderColor = true;
            this.numPaid.Properties.Appearance.Options.UseFont = true;
            this.numPaid.Properties.Appearance.Options.UseForeColor = true;
            this.numPaid.Properties.Appearance.Options.UseTextOptions = true;
            this.numPaid.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.numPaid.Properties.AutoHeight = false;
            this.numPaid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numPaid.Properties.Mask.EditMask = "#,###,###,###";
            this.numPaid.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numPaid.Properties.Precision = 2;
            this.numPaid.Properties.ReadOnly = true;
            this.numPaid.Size = new System.Drawing.Size(191, 54);
            this.numPaid.TabIndex = 3;
            this.numPaid.TabStop = false;
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "å";
            this.textEdit1.Location = new System.Drawing.Point(8, 413);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textEdit1.Properties.Appearance.BorderColor = System.Drawing.Color.Green;
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Symbol", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.textEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.Green;
            this.textEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.textEdit1.Properties.Appearance.Options.UseBorderColor = true;
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.textEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.textEdit1.Properties.AutoHeight = false;
            this.textEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(37, 54);
            this.textEdit1.TabIndex = 2;
            this.textEdit1.TabStop = false;
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(787, 474);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.numPaid);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.popupContainer1);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.groupControl5);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(840, 512);
            this.MinimizeBox = false;
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Борлуулалтын төлбөр";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPayment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRemain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrepaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainer1)).EndInit();
            this.popupContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CalcEdit numPayment;
        private DevExpress.XtraEditors.LabelControl lblChange;
        private DevExpress.XtraEditors.CalcEdit numChange;
        public DevExpress.XtraEditors.TextEdit txtRegNo;
        private DevExpress.XtraEditors.SimpleButton btnPayment;
        private DevExpress.XtraEditors.LabelControl lblRegNo;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.CalcEdit numRemain;
        private DevExpress.XtraEditors.CalcEdit numPrepaid;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CalcEdit numTotal;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtSalesNo;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private Panels.ucNumpad ucNumpad1;
        private DevExpress.XtraEditors.DropDownButton dropDownButton1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarToolbarsListItem barToolbarsListItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraBars.PopupControlContainer popupContainer1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        public DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.CalcEdit numPaid;
        public DevExpress.XtraEditors.TextEdit txtDetail;
        private DevExpress.XtraEditors.LabelControl lblDetail;

    }
}