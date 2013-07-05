namespace InfoPos.Parameter
{
    partial class CRMNotifyTxn
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
            this.grdNotify = new DevExpress.XtraGrid.GridControl();
            this.gvwNotify = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.numSchemaID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grdNotifyTxn = new DevExpress.XtraGrid.GridControl();
            this.gvwNotifyTxn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.cboIDD = new DevExpress.XtraEditors.LookUpEdit();
            this.btnClearTxn = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteTxn = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditTxn = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddTxn = new DevExpress.XtraEditors.SimpleButton();
            this.cboGroupID = new DevExpress.XtraEditors.LookUpEdit();
            this.cboTxn = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.numNotityTxnID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNotify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwNotify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSchemaID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNotifyTxn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwNotifyTxn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboIDD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroupID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTxn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNotityTxnID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdNotify);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.MinimumSize = new System.Drawing.Size(364, 574);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(691, 574);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Мэдэгдэлийн схем";
            // 
            // grdNotify
            // 
            this.grdNotify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNotify.Location = new System.Drawing.Point(2, 22);
            this.grdNotify.MainView = this.gvwNotify;
            this.grdNotify.Name = "grdNotify";
            this.grdNotify.Size = new System.Drawing.Size(687, 370);
            this.grdNotify.TabIndex = 1;
            this.grdNotify.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwNotify});
            // 
            // gvwNotify
            // 
            this.gvwNotify.GridControl = this.grdNotify;
            this.gvwNotify.Name = "gvwNotify";
            this.gvwNotify.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvwNotify_FocusedRowChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnAdd);
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnEdit);
            this.panelControl2.Controls.Add(this.btnDelete);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.numOrderNo);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.txtName2);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.txtName);
            this.panelControl2.Controls.Add(this.numSchemaID);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(2, 392);
            this.panelControl2.MinimumSize = new System.Drawing.Size(526, 180);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(687, 180);
            this.panelControl2.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(11, 139);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(79, 26);
            this.btnAdd.TabIndex = 89;
            this.btnAdd.Text = "Нэмэх";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(267, 139);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(86, 26);
            this.btnClear.TabIndex = 92;
            this.btnClear.Text = "Цэвэрлэх";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(96, 139);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(79, 26);
            this.btnEdit.TabIndex = 90;
            this.btnEdit.Text = "Засах";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(181, 139);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(79, 26);
            this.btnDelete.TabIndex = 91;
            this.btnDelete.Text = "Устгах";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 13);
            this.labelControl2.TabIndex = 88;
            this.labelControl2.Text = "Нэр :";
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(163, 96);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.OldLace;
            this.numOrderNo.Properties.Appearance.Options.UseBackColor = true;
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Size = new System.Drawing.Size(164, 20);
            this.numOrderNo.TabIndex = 87;
            this.numOrderNo.ToolTip = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(24, 99);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(107, 13);
            this.labelControl4.TabIndex = 86;
            this.labelControl4.Text = "Жагсаалтын эрэмбэ :";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(163, 70);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Size = new System.Drawing.Size(164, 20);
            this.txtName2.TabIndex = 85;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 73);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(34, 13);
            this.labelControl3.TabIndex = 84;
            this.labelControl3.Text = "Нэр 2 :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(163, 44);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BackColor = System.Drawing.Color.OldLace;
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Size = new System.Drawing.Size(164, 20);
            this.txtName.TabIndex = 83;
            this.txtName.ToolTip = "Нэр оруулна уу";
            // 
            // numSchemaID
            // 
            this.numSchemaID.Location = new System.Drawing.Point(163, 18);
            this.numSchemaID.Name = "numSchemaID";
            this.numSchemaID.Properties.Appearance.BackColor = System.Drawing.Color.OldLace;
            this.numSchemaID.Properties.Appearance.Options.UseBackColor = true;
            this.numSchemaID.Properties.Mask.EditMask = "\\d{0,4}";
            this.numSchemaID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numSchemaID.Size = new System.Drawing.Size(164, 20);
            this.numSchemaID.TabIndex = 82;
            this.numSchemaID.ToolTip = "Мэдэгдэлийн дугаар оруулна уу";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(112, 13);
            this.labelControl1.TabIndex = 81;
            this.labelControl1.Text = "Мэдэгдэлийн дугаар :";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grdNotifyTxn);
            this.groupControl2.Controls.Add(this.panelControl3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(693, 2);
            this.groupControl2.MinimumSize = new System.Drawing.Size(453, 574);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(453, 574);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Мэдэгдэлийн схем гүйлгээ";
            // 
            // grdNotifyTxn
            // 
            this.grdNotifyTxn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNotifyTxn.Location = new System.Drawing.Point(2, 22);
            this.grdNotifyTxn.MainView = this.gvwNotifyTxn;
            this.grdNotifyTxn.Name = "grdNotifyTxn";
            this.grdNotifyTxn.Size = new System.Drawing.Size(449, 370);
            this.grdNotifyTxn.TabIndex = 2;
            this.grdNotifyTxn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwNotifyTxn});
            // 
            // gvwNotifyTxn
            // 
            this.gvwNotifyTxn.GridControl = this.grdNotifyTxn;
            this.gvwNotifyTxn.Name = "gvwNotifyTxn";
            this.gvwNotifyTxn.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvwNotifyTxn_FocusedRowChanged);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.cboIDD);
            this.panelControl3.Controls.Add(this.btnClearTxn);
            this.panelControl3.Controls.Add(this.btnDeleteTxn);
            this.panelControl3.Controls.Add(this.btnEditTxn);
            this.panelControl3.Controls.Add(this.btnAddTxn);
            this.panelControl3.Controls.Add(this.cboGroupID);
            this.panelControl3.Controls.Add(this.cboTxn);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Controls.Add(this.labelControl7);
            this.panelControl3.Controls.Add(this.numNotityTxnID);
            this.panelControl3.Controls.Add(this.labelControl8);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 392);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(449, 180);
            this.panelControl3.TabIndex = 3;
            // 
            // cboIDD
            // 
            this.cboIDD.Location = new System.Drawing.Point(141, 95);
            this.cboIDD.Name = "cboIDD";
            this.cboIDD.Properties.Appearance.BackColor = System.Drawing.Color.OldLace;
            this.cboIDD.Properties.Appearance.Options.UseBackColor = true;
            this.cboIDD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboIDD.Size = new System.Drawing.Size(278, 20);
            this.cboIDD.TabIndex = 99;
            this.cboIDD.Visible = false;
            // 
            // btnClearTxn
            // 
            this.btnClearTxn.Location = new System.Drawing.Point(298, 139);
            this.btnClearTxn.Name = "btnClearTxn";
            this.btnClearTxn.Size = new System.Drawing.Size(86, 26);
            this.btnClearTxn.TabIndex = 97;
            this.btnClearTxn.Text = "Цэвэрлэх";
            this.btnClearTxn.Click += new System.EventHandler(this.btnClearTxn_Click);
            // 
            // btnDeleteTxn
            // 
            this.btnDeleteTxn.Location = new System.Drawing.Point(206, 139);
            this.btnDeleteTxn.Name = "btnDeleteTxn";
            this.btnDeleteTxn.Size = new System.Drawing.Size(86, 26);
            this.btnDeleteTxn.TabIndex = 96;
            this.btnDeleteTxn.Text = "Устгах";
            this.btnDeleteTxn.Click += new System.EventHandler(this.btnDeleteTxn_Click);
            // 
            // btnEditTxn
            // 
            this.btnEditTxn.Location = new System.Drawing.Point(114, 139);
            this.btnEditTxn.Name = "btnEditTxn";
            this.btnEditTxn.Size = new System.Drawing.Size(86, 26);
            this.btnEditTxn.TabIndex = 95;
            this.btnEditTxn.Text = "Засах";
            this.btnEditTxn.Click += new System.EventHandler(this.btnEditTxn_Click);
            // 
            // btnAddTxn
            // 
            this.btnAddTxn.Location = new System.Drawing.Point(22, 139);
            this.btnAddTxn.Name = "btnAddTxn";
            this.btnAddTxn.Size = new System.Drawing.Size(86, 26);
            this.btnAddTxn.TabIndex = 93;
            this.btnAddTxn.Text = "Нэмэх";
            this.btnAddTxn.Click += new System.EventHandler(this.btnAddTxn_Click);
            // 
            // cboGroupID
            // 
            this.cboGroupID.Location = new System.Drawing.Point(141, 71);
            this.cboGroupID.Name = "cboGroupID";
            this.cboGroupID.Properties.Appearance.BackColor = System.Drawing.Color.OldLace;
            this.cboGroupID.Properties.Appearance.Options.UseBackColor = true;
            this.cboGroupID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGroupID.Size = new System.Drawing.Size(278, 20);
            this.cboGroupID.TabIndex = 93;
            this.cboGroupID.EditValueChanged += new System.EventHandler(this.cboGroupID_EditValueChanged);
            // 
            // cboTxn
            // 
            this.cboTxn.Location = new System.Drawing.Point(141, 45);
            this.cboTxn.Name = "cboTxn";
            this.cboTxn.Properties.Appearance.BackColor = System.Drawing.Color.OldLace;
            this.cboTxn.Properties.Appearance.Options.UseBackColor = true;
            this.cboTxn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTxn.Size = new System.Drawing.Size(278, 20);
            this.cboTxn.TabIndex = 92;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(23, 46);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(85, 13);
            this.labelControl5.TabIndex = 87;
            this.labelControl5.Text = "Гүйлгээний код :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(23, 98);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 85;
            this.labelControl6.Text = "Дугаар :";
            this.labelControl6.Visible = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(23, 72);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(88, 13);
            this.labelControl7.TabIndex = 83;
            this.labelControl7.Text = "Төрлийн дугаар :";
            // 
            // numNotityTxnID
            // 
            this.numNotityTxnID.Enabled = false;
            this.numNotityTxnID.Location = new System.Drawing.Point(141, 18);
            this.numNotityTxnID.Name = "numNotityTxnID";
            this.numNotityTxnID.Properties.Appearance.BackColor = System.Drawing.Color.OldLace;
            this.numNotityTxnID.Properties.Appearance.Options.UseBackColor = true;
            this.numNotityTxnID.Properties.Mask.EditMask = "\\d{0,4}";
            this.numNotityTxnID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numNotityTxnID.Size = new System.Drawing.Size(278, 20);
            this.numNotityTxnID.TabIndex = 81;
            this.numNotityTxnID.ToolTip = "Мэдэгдэлийн дугаар оруулна уу";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(23, 20);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(112, 13);
            this.labelControl8.TabIndex = 80;
            this.labelControl8.Text = "Мэдэгдэлийн дугаар :";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.splitterControl1);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1148, 578);
            this.panelControl1.TabIndex = 3;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(693, 2);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 574);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // CRMNotifyTxn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 578);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.Name = "CRMNotifyTxn";
            this.Text = "Мэдэгдэлийн схем болон мэдэгдэлийн схемийн гүйлгээ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CRMNotifyTxn_FormClosing);
            this.Load += new System.EventHandler(this.CRMNotifyTxn_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CRMNotifyTxn_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNotify)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwNotify)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSchemaID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNotifyTxn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwNotifyTxn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboIDD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroupID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTxn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNotityTxnID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdNotify;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwNotify;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grdNotifyTxn;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwNotifyTxn;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit numSchemaID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit numNotityTxnID;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit cboGroupID;
        private DevExpress.XtraEditors.LookUpEdit cboTxn;
        private DevExpress.XtraEditors.SimpleButton btnClearTxn;
        private DevExpress.XtraEditors.SimpleButton btnDeleteTxn;
        private DevExpress.XtraEditors.SimpleButton btnEditTxn;
        private DevExpress.XtraEditors.SimpleButton btnAddTxn;
        private DevExpress.XtraEditors.LookUpEdit cboIDD;
    }
}