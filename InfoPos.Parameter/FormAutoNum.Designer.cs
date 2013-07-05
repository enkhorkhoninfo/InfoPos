namespace InfoPos.Parameter
{
    partial class FormAutoNum
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
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.cboType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtMask = new DevExpress.XtraEditors.TextEdit();
            this.txtKey = new DevExpress.XtraEditors.TextEdit();
            this.btncheck = new DevExpress.XtraEditors.SimpleButton();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.lblNote = new DevExpress.XtraEditors.LabelControl();
            this.mmError = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMask.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmError.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.mmError);
            this.groupControl1.Controls.Add(this.lblNote);
            this.groupControl1.Controls.Add(this.btncheck);
            this.groupControl1.Controls.Add(this.txtKey);
            this.groupControl1.Controls.Add(this.txtMask);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.cboType);
            this.groupControl1.Size = new System.Drawing.Size(502, 312);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 334);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 334);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 334);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 334);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 334);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(875, 316);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(364, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 316);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(364, 316);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(369, 0);
            this.panelControl3.Size = new System.Drawing.Size(506, 316);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(151, 60);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 100;
            this.txtName.Size = new System.Drawing.Size(327, 20);
            this.txtName.TabIndex = 1;
            this.txtName.ToolTipTitle = "Нэр оруулна уу .";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(151, 81);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 100;
            this.txtName2.Size = new System.Drawing.Size(327, 20);
            this.txtName2.TabIndex = 2;
            // 
            // cboType
            // 
            this.cboType.Location = new System.Drawing.Point(151, 38);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboType.Properties.NullText = "";
            this.cboType.Size = new System.Drawing.Size(327, 20);
            this.cboType.TabIndex = 0;
            this.cboType.ToolTipTitle = "Дугаарлалтын төрөл оруулна уу";
            this.cboType.EditValueChanged += new System.EventHandler(this.cboType_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 84);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "Нэр 2:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 63);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(25, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Нэр :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(115, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Дугаарлалтын төрөл :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 149);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 13);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "Маскны утга:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(30, 221);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Түлхүүр үг:";
            // 
            // txtMask
            // 
            this.txtMask.Location = new System.Drawing.Point(151, 146);
            this.txtMask.Name = "txtMask";
            this.txtMask.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMask.Properties.MaxLength = 100;
            this.txtMask.Size = new System.Drawing.Size(327, 20);
            this.txtMask.TabIndex = 3;
            this.txtMask.ToolTipTitle = "Маскны утга оруулна уу .";
            this.txtMask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMask_KeyPress);
            this.txtMask.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMask_KeyUp);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(151, 218);
            this.txtKey.Name = "txtKey";
            this.txtKey.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKey.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.txtKey.Properties.Mask.BeepOnError = true;
            this.txtKey.Properties.Mask.EditMask = "\\p{Lu}+";
            this.txtKey.Properties.MaxLength = 100;
            this.txtKey.Size = new System.Drawing.Size(327, 20);
            this.txtKey.TabIndex = 5;
            this.txtKey.ToolTipTitle = "Та түлхүүр үг авна уу .";
            // 
            // btncheck
            // 
            this.btncheck.Location = new System.Drawing.Point(367, 169);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(111, 26);
            this.btncheck.TabIndex = 4;
            this.btncheck.Text = "Түлхүүр үг авах";
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(899, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 367);
            this.barDockControlBottom.Size = new System.Drawing.Size(899, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 367);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(899, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 367);
            // 
            // lblNote
            // 
            this.lblNote.Location = new System.Drawing.Point(151, 103);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(296, 39);
            this.lblNote.TabIndex = 16;
            this.lblNote.Text = "Маскны үсгийн формат: B, C, G, P, S ,Y \r\nМаскны тооны урт: Нийт тооны урт 16-с ба" +
                "га байх ёстой.\r\nЖишээ нь: 1N3[B01]13[G03]0[C01]12A";
            // 
            // mmError
            // 
            this.mmError.Location = new System.Drawing.Point(151, 169);
            this.mmError.MenuManager = this.barManager1;
            this.mmError.Name = "mmError";
            this.mmError.Size = new System.Drawing.Size(210, 46);
            this.mmError.TabIndex = 17;
            this.mmError.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mmError_KeyPress);
            // 
            // FormAutoNum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 367);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.KeyPreview = true;
            this.Name = "FormAutoNum";
            this.Text = "Автомат дугаарлалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAutoNum_FormClosing);
            this.Load += new System.EventHandler(this.FormAutoNum_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAutoNum_KeyDown);
            this.Controls.SetChildIndex(this.barDockControlTop, 0);
            this.Controls.SetChildIndex(this.barDockControlBottom, 0);
            this.Controls.SetChildIndex(this.barDockControlRight, 0);
            this.Controls.SetChildIndex(this.barDockControlLeft, 0);
            this.Controls.SetChildIndex(this.btnRefresh, 0);
            this.Controls.SetChildIndex(this.btnAdd, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnRemove, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMask.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmError.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.LookUpEdit cboType;
        private DevExpress.XtraEditors.TextEdit txtKey;
        private DevExpress.XtraEditors.TextEdit txtMask;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btncheck;
        private DevExpress.XtraEditors.LabelControl lblNote;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.MemoEdit mmError;
    }
}