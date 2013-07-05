namespace InfoPos.Parameter
{
    partial class FormSubIndustry
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
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.numSubTypeCode = new DevExpress.XtraEditors.TextEdit();
            this.cboTypeCode = new DevExpress.XtraEditors.LookUpEdit();
            this.cboClassCode = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSubTypeCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTypeCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClassCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboTypeCode);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.numSubTypeCode);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.cboClassCode);
            this.groupControl1.Size = new System.Drawing.Size(560, 334);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 356);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 356);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 356);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 356);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 356);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(825, 338);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(256, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 338);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(256, 338);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(261, 0);
            this.panelControl3.Size = new System.Drawing.Size(564, 338);
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(208, 168);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Size = new System.Drawing.Size(268, 20);
            this.numOrderNo.TabIndex = 5;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(208, 116);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 3;
            this.txtName.ToolTipTitle = "Төрлийн нэр оруулна уу";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(208, 142);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 50;
            this.txtName2.Size = new System.Drawing.Size(268, 20);
            this.txtName2.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(88, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Төрлийн дугаар :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 119);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Төрлийн нэр :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 145);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Төрлийн нэр 2 :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 171);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(107, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Жагсаалтын эрэмбэ :";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(30, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Класс код :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(30, 67);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(111, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Дэд Төрлийн дугаар :";
            // 
            // numSubTypeCode
            // 
            this.numSubTypeCode.Location = new System.Drawing.Point(208, 64);
            this.numSubTypeCode.Name = "numSubTypeCode";
            this.numSubTypeCode.Properties.Mask.BeepOnError = true;
            this.numSubTypeCode.Properties.Mask.EditMask = "\\d{0,4}";
            this.numSubTypeCode.Properties.Mask.IgnoreMaskBlank = false;
            this.numSubTypeCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numSubTypeCode.Properties.Mask.SaveLiteral = false;
            this.numSubTypeCode.Size = new System.Drawing.Size(268, 20);
            this.numSubTypeCode.TabIndex = 1;
            this.numSubTypeCode.ToolTipTitle = "Дэд төрлийн дугаар оруулна уу";
            // 
            // cboTypeCode
            // 
            this.cboTypeCode.Location = new System.Drawing.Point(208, 38);
            this.cboTypeCode.Name = "cboTypeCode";
            this.cboTypeCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTypeCode.Properties.NullText = "";
            this.cboTypeCode.Size = new System.Drawing.Size(268, 20);
            this.cboTypeCode.TabIndex = 0;
            this.cboTypeCode.ToolTipTitle = "Төрлийн дугаар оруулна уу";
            // 
            // cboClassCode
            // 
            this.cboClassCode.Location = new System.Drawing.Point(208, 90);
            this.cboClassCode.Name = "cboClassCode";
            this.cboClassCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboClassCode.Properties.NullText = "";
            this.cboClassCode.Size = new System.Drawing.Size(268, 20);
            this.cboClassCode.TabIndex = 2;
            this.cboClassCode.ToolTipTitle = "Класс код оруулна уу";
            // 
            // FormSubIndustry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 389);
            this.KeyPreview = true;
            this.Name = "FormSubIndustry";
            this.Text = "Үйл ажиллагааны дэд чиглэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSubIndustry_FormClosing);
            this.Load += new System.EventHandler(this.FormSubIndustry_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSubIndustry_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSubTypeCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTypeCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClassCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit numSubTypeCode;
        private DevExpress.XtraEditors.LookUpEdit cboTypeCode;
        private DevExpress.XtraEditors.LookUpEdit cboClassCode;
    }
}