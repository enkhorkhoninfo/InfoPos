namespace InfoPos.Parameter
{
    partial class FormObject
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
            this.ucObject = new ISM.Template.ucTogglePanel();
            this.pnlObject = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl34 = new DevExpress.XtraEditors.LabelControl();
            this.btnSubObject = new DevExpress.XtraEditors.SimpleButton();
            this.btnItem = new DevExpress.XtraEditors.SimpleButton();
            this.txtObjectID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl44 = new DevExpress.XtraEditors.LabelControl();
            this.txtOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl45 = new DevExpress.XtraEditors.LabelControl();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl46 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.cboClassID = new DevExpress.XtraEditors.LookUpEdit();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.grdObject = new DevExpress.XtraGrid.GridControl();
            this.gvwObject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ucObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlObject)).BeginInit();
            this.pnlObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtObjectID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClassID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwObject)).BeginInit();
            this.SuspendLayout();
            // 
            // ucObject
            // 
            this.ucObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucObject.Controls.Add(this.pnlObject);
            this.ucObject.Location = new System.Drawing.Point(4, 4);
            this.ucObject.Name = "ucObject";
            this.ucObject.Size = new System.Drawing.Size(953, 555);
            this.ucObject.TabIndex = 0;
            this.ucObject.ToggleShowDelete = false;
            this.ucObject.ToggleShowEdit = false;
            this.ucObject.ToggleShowExit = false;
            this.ucObject.ToggleShowNew = false;
            this.ucObject.ToggleShowReject = false;
            this.ucObject.ToggleShowSave = false;
            // 
            // pnlObject
            // 
            this.pnlObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlObject.Controls.Add(this.panelControl1);
            this.pnlObject.Controls.Add(this.splitterControl3);
            this.pnlObject.Controls.Add(this.grdObject);
            this.pnlObject.Location = new System.Drawing.Point(0, 3);
            this.pnlObject.Name = "pnlObject";
            this.pnlObject.Size = new System.Drawing.Size(950, 515);
            this.pnlObject.TabIndex = 20;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl34);
            this.panelControl1.Controls.Add(this.btnSubObject);
            this.panelControl1.Controls.Add(this.btnItem);
            this.panelControl1.Controls.Add(this.txtObjectID);
            this.panelControl1.Controls.Add(this.labelControl26);
            this.panelControl1.Controls.Add(this.labelControl44);
            this.panelControl1.Controls.Add(this.txtOrderNo);
            this.panelControl1.Controls.Add(this.labelControl45);
            this.panelControl1.Controls.Add(this.txtName2);
            this.panelControl1.Controls.Add(this.labelControl46);
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.cboClassID);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(538, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(410, 511);
            this.panelControl1.TabIndex = 207;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // labelControl34
            // 
            this.labelControl34.Location = new System.Drawing.Point(20, 18);
            this.labelControl34.Name = "labelControl34";
            this.labelControl34.Size = new System.Drawing.Size(84, 13);
            this.labelControl34.TabIndex = 214;
            this.labelControl34.Text = "Зүйлийн дугаар:";
            // 
            // btnSubObject
            // 
            this.btnSubObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubObject.Location = new System.Drawing.Point(17, 438);
            this.btnSubObject.Name = "btnSubObject";
            this.btnSubObject.Size = new System.Drawing.Size(164, 23);
            this.btnSubObject.TabIndex = 206;
            this.btnSubObject.Text = "Зүйлийн зүйл холбох";
            this.btnSubObject.Click += new System.EventHandler(this.btnSubObject_Click);
            // 
            // btnItem
            // 
            this.btnItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnItem.Location = new System.Drawing.Point(17, 467);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(164, 23);
            this.btnItem.TabIndex = 205;
            this.btnItem.Text = "Зүйлийн үзүүлэлтүүд холбох";
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // txtObjectID
            // 
            this.txtObjectID.Location = new System.Drawing.Point(136, 15);
            this.txtObjectID.Name = "txtObjectID";
            this.txtObjectID.Properties.Mask.EditMask = "\\d{0,4}";
            this.txtObjectID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtObjectID.Size = new System.Drawing.Size(103, 20);
            this.txtObjectID.TabIndex = 205;
            this.txtObjectID.ToolTipTitle = "Зүйлийн дугаар оруулаагүй байна";
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(20, 44);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(71, 13);
            this.labelControl26.TabIndex = 213;
            this.labelControl26.Text = "Зүйлийн бүлэг";
            // 
            // labelControl44
            // 
            this.labelControl44.Location = new System.Drawing.Point(20, 122);
            this.labelControl44.Name = "labelControl44";
            this.labelControl44.Size = new System.Drawing.Size(100, 13);
            this.labelControl44.TabIndex = 212;
            this.labelControl44.Text = "Жагсаалтын эрэмбэ";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Location = new System.Drawing.Point(136, 119);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.txtOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtOrderNo.Size = new System.Drawing.Size(103, 20);
            this.txtOrderNo.TabIndex = 209;
            // 
            // labelControl45
            // 
            this.labelControl45.Location = new System.Drawing.Point(19, 96);
            this.labelControl45.Name = "labelControl45";
            this.labelControl45.Size = new System.Drawing.Size(27, 13);
            this.labelControl45.TabIndex = 211;
            this.labelControl45.Text = "Нэр 2";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(136, 93);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Size = new System.Drawing.Size(252, 20);
            this.txtName2.TabIndex = 208;
            // 
            // labelControl46
            // 
            this.labelControl46.Location = new System.Drawing.Point(19, 70);
            this.labelControl46.Name = "labelControl46";
            this.labelControl46.Size = new System.Drawing.Size(18, 13);
            this.labelControl46.TabIndex = 210;
            this.labelControl46.Text = "Нэр";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(136, 67);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Size = new System.Drawing.Size(252, 20);
            this.txtName.TabIndex = 207;
            this.txtName.ToolTipTitle = "Зүйлийн нэр оруулаагүй байна";
            // 
            // cboClassID
            // 
            this.cboClassID.Location = new System.Drawing.Point(136, 41);
            this.cboClassID.Name = "cboClassID";
            this.cboClassID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboClassID.Properties.DisplayFormat.FormatString = "d";
            this.cboClassID.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cboClassID.Properties.EditFormat.FormatString = "d";
            this.cboClassID.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cboClassID.Properties.NullText = "";
            this.cboClassID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboClassID.Size = new System.Drawing.Size(103, 20);
            this.cboClassID.TabIndex = 206;
            this.cboClassID.ToolTipTitle = "Зүйлийн төрөл сонгоогүй байна";
            // 
            // splitterControl3
            // 
            this.splitterControl3.Location = new System.Drawing.Point(533, 2);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(5, 511);
            this.splitterControl3.TabIndex = 1;
            this.splitterControl3.TabStop = false;
            // 
            // grdObject
            // 
            this.grdObject.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdObject.Location = new System.Drawing.Point(2, 2);
            this.grdObject.MainView = this.gvwObject;
            this.grdObject.Name = "grdObject";
            this.grdObject.Size = new System.Drawing.Size(531, 511);
            this.grdObject.TabIndex = 0;
            this.grdObject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwObject});
            // 
            // gvwObject
            // 
            this.gvwObject.GridControl = this.grdObject;
            this.gvwObject.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwObject.Name = "gvwObject";
            this.gvwObject.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvwObject_FocusedRowChanged);
            // 
            // FormObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 562);
            this.Controls.Add(this.ucObject);
            this.KeyPreview = true;
            this.Name = "FormObject";
            this.Text = "Зүйлийн бүртгэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormObject_FormClosing);
            this.Load += new System.EventHandler(this.FormObject_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormObject_KeyDown);
            this.ucObject.ResumeLayout(false);
            this.ucObject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlObject)).EndInit();
            this.pnlObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtObjectID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClassID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlObject;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private DevExpress.XtraGrid.GridControl grdObject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwObject;
        private DevExpress.XtraEditors.SimpleButton btnItem;
        private DevExpress.XtraEditors.SimpleButton btnSubObject;
        public ISM.Template.ucTogglePanel ucObject;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl34;
        private DevExpress.XtraEditors.TextEdit txtObjectID;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private DevExpress.XtraEditors.LabelControl labelControl44;
        private DevExpress.XtraEditors.TextEdit txtOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl45;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.LabelControl labelControl46;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LookUpEdit cboClassID;
    }
}