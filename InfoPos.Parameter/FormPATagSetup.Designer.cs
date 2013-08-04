namespace InfoPos.Parameter
{
    partial class PATagSetup
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
            this.txtTagType = new DevExpress.XtraEditors.TextEdit();
            this.numOffset = new DevExpress.XtraEditors.TextEdit();
            this.numLength = new DevExpress.XtraEditors.TextEdit();
            this.cboFormat = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtOrderNo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTagType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOffset.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtOrderNo);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.cboFormat);
            this.groupControl1.Controls.Add(this.numLength);
            this.groupControl1.Controls.Add(this.numOffset);
            this.groupControl1.Controls.Add(this.txtTagType);
            // 
            // txtTagType
            // 
            this.txtTagType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTagType.Location = new System.Drawing.Point(146, 36);
            this.txtTagType.MinimumSize = new System.Drawing.Size(179, 20);
            this.txtTagType.Name = "txtTagType";
            this.txtTagType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTagType.Properties.MaxLength = 10;
            this.txtTagType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTagType.Size = new System.Drawing.Size(179, 20);
            this.txtTagType.TabIndex = 0;
            this.txtTagType.ToolTipTitle = "Тагийн төрлийн нэр оруулна уу.";
            // 
            // numOffset
            // 
            this.numOffset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numOffset.Location = new System.Drawing.Point(146, 113);
            this.numOffset.MinimumSize = new System.Drawing.Size(179, 20);
            this.numOffset.Name = "numOffset";
            this.numOffset.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numOffset.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.numOffset.Properties.Mask.EditMask = "[0-9]{1,5}";
            this.numOffset.Properties.Mask.IgnoreMaskBlank = false;
            this.numOffset.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOffset.Properties.Mask.ShowPlaceHolders = false;
            this.numOffset.Properties.MaxLength = 5;
            this.numOffset.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numOffset.Size = new System.Drawing.Size(179, 20);
            this.numOffset.TabIndex = 4;
            // 
            // numLength
            // 
            this.numLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numLength.Location = new System.Drawing.Point(146, 139);
            this.numLength.MinimumSize = new System.Drawing.Size(179, 20);
            this.numLength.Name = "numLength";
            this.numLength.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numLength.Properties.Mask.EditMask = "[0-9]{1,5}";
            this.numLength.Properties.Mask.IgnoreMaskBlank = false;
            this.numLength.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numLength.Properties.Mask.ShowPlaceHolders = false;
            this.numLength.Properties.MaxLength = 5;
            this.numLength.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numLength.Size = new System.Drawing.Size(179, 20);
            this.numLength.TabIndex = 5;
            // 
            // cboFormat
            // 
            this.cboFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFormat.Location = new System.Drawing.Point(146, 165);
            this.cboFormat.MinimumSize = new System.Drawing.Size(179, 20);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboFormat.Properties.NullText = "";
            this.cboFormat.Properties.PopupSizeable = false;
            this.cboFormat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboFormat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cboFormat.Size = new System.Drawing.Size(179, 20);
            this.cboFormat.TabIndex = 6;
            this.cboFormat.ToolTipTitle = "Мэдээллийн хэлбэр сонгоно уу";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(102, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Тагийн төрлийн код";
            this.labelControl1.ToolTipTitle = "Тагийн төрлийн код оруулна уу.";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 64);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(103, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Тагийн төрлийн нэр ";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 115);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(125, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Бичилт хийгдэх байрлал";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 141);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(119, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Бичилтийн байт хэмжээ";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 168);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(97, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Мэдээллийн хэлбэр";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(146, 61);
            this.txtName.MinimumSize = new System.Drawing.Size(179, 20);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 200;
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName.Size = new System.Drawing.Size(179, 20);
            this.txtName.TabIndex = 1;
            this.txtName.ToolTipTitle = "Тагийн төрлийн нэр оруулна уу.";
            // 
            // txtName2
            // 
            this.txtName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName2.Location = new System.Drawing.Point(146, 87);
            this.txtName2.MinimumSize = new System.Drawing.Size(179, 20);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 200;
            this.txtName2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName2.Size = new System.Drawing.Size(179, 20);
            this.txtName2.TabIndex = 3;
            this.txtName2.ToolTipTitle = "Тагийн төрлийн нэр оруулна уу.";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(15, 90);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(109, 13);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Тагийн төрлийн нэр 2";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(15, 193);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(35, 13);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "Эрэмбэ";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNo.Location = new System.Drawing.Point(146, 191);
            this.txtOrderNo.MinimumSize = new System.Drawing.Size(179, 20);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOrderNo.Properties.Mask.EditMask = "[0-9]{5}";
            this.txtOrderNo.Properties.Mask.IgnoreMaskBlank = false;
            this.txtOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtOrderNo.Properties.Mask.ShowPlaceHolders = false;
            this.txtOrderNo.Properties.MaxLength = 5;
            this.txtOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOrderNo.Size = new System.Drawing.Size(179, 20);
            this.txtOrderNo.TabIndex = 13;
            // 
            // PATagSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 390);
            this.MaximumSize = new System.Drawing.Size(1024, 720);
            this.MinimumSize = new System.Drawing.Size(664, 417);
            this.Name = "PATagSetup";
            this.Text = "Тагийн төрлийн бүртгэл";
            this.Load += new System.EventHandler(this.PATagSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTagType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOffset.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtTagType;
        private DevExpress.XtraEditors.TextEdit numOffset;
        private DevExpress.XtraEditors.TextEdit numLength;
        private DevExpress.XtraEditors.LookUpEdit cboFormat;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtOrderNo;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}