namespace InfoPos.Parameter
{
    partial class FormCustContactType
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
            this.numTypeCode = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTypeCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.numTypeCode);
            this.groupControl1.Size = new System.Drawing.Size(451, 324);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(799, 328);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(339, 0);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(339, 328);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(344, 0);
            this.panelControl3.Size = new System.Drawing.Size(455, 328);
            // 
            // numTypeCode
            // 
            this.numTypeCode.Location = new System.Drawing.Point(254, 39);
            this.numTypeCode.Name = "numTypeCode";
            this.numTypeCode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numTypeCode.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.numTypeCode.Properties.Mask.EditMask = "[0-9]{1,16}";
            this.numTypeCode.Properties.Mask.IgnoreMaskBlank = false;
            this.numTypeCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numTypeCode.Properties.Mask.ShowPlaceHolders = false;
            this.numTypeCode.Properties.MaxLength = 4;
            this.numTypeCode.Size = new System.Drawing.Size(177, 20);
            this.numTypeCode.TabIndex = 2;
            this.numTypeCode.ToolTipTitle = "Харилцагчтай холбоо барисан төрлийн код оруулна уу.";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(254, 61);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.Size = new System.Drawing.Size(177, 20);
            this.txtName.TabIndex = 3;
            this.txtName.ToolTipTitle = "Нэр оруулна уу.";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(254, 84);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 50;
            this.txtName2.ShowToolTips = false;
            this.txtName2.Size = new System.Drawing.Size(177, 20);
            this.txtName2.TabIndex = 4;
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(254, 107);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numOrderNo.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.numOrderNo.Properties.Mask.EditMask = "[0-9]{1,4}";
            this.numOrderNo.Properties.Mask.IgnoreMaskBlank = false;
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Properties.Mask.ShowPlaceHolders = false;
            this.numOrderNo.Properties.MaxLength = 4;
            this.numOrderNo.Size = new System.Drawing.Size(177, 20);
            this.numOrderNo.TabIndex = 5;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу.";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(225, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Харилцагчтай холбоо барисан төрлийн код:";
            this.labelControl1.ToolTipTitle = "Бүлэг дугаар оруулна уу .";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 64);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(22, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Нэр:";
            this.labelControl2.ToolTipTitle = "Бүлэг дугаар оруулна уу .";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Нэр 2:";
            this.labelControl3.ToolTipTitle = "Бүлэг дугаар оруулна уу .";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(24, 110);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(104, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Жагсаалтын эрэмбэ:";
            this.labelControl4.ToolTipTitle = "Бүлэг дугаар оруулна уу .";
            // 
            // FormCustContactType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 379);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(839, 417);
            this.Name = "FormCustContactType";
            this.Text = "Харилцагчтай холбоо барисан төрөл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCustContactType_FormClosing);
            this.Load += new System.EventHandler(this.FormCustContactType_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCustContactType_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTypeCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit numTypeCode;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}