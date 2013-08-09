namespace InfoPos.Parameter
{
    partial class FormPaBill
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numBankNote = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.txtDesc = new DevExpress.XtraEditors.TextEdit();
            this.cboCurrency = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBankNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrency.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboCurrency);
            this.groupControl1.Controls.Add(this.txtDesc);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.numBankNote);
            this.groupControl1.Controls.Add(this.labelControl1);
            // 
            // btnRemove
            // 
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(33, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Валют";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(120, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Дэвсгэртийн тоон утга ";
            // 
            // numBankNote
            // 
            this.numBankNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numBankNote.Location = new System.Drawing.Point(156, 57);
            this.numBankNote.MinimumSize = new System.Drawing.Size(171, 20);
            this.numBankNote.Name = "numBankNote";
            this.numBankNote.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numBankNote.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.numBankNote.Properties.Mask.EditMask = "[0-9]{1,6}";
            this.numBankNote.Properties.Mask.IgnoreMaskBlank = false;
            this.numBankNote.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numBankNote.Properties.Mask.ShowPlaceHolders = false;
            this.numBankNote.Properties.MaxLength = 6;
            this.numBankNote.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numBankNote.Size = new System.Drawing.Size(171, 20);
            this.numBankNote.TabIndex = 2;
            this.numBankNote.ToolTipTitle = "Дэвсгэртийн тоон утгаа оруулна уу";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(14, 84);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(136, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Дэвсгэртийн нэр, тайлбар ";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(14, 109);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(87, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Эрэмбийн дугаар";
            // 
            // numOrderNo
            // 
            this.numOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numOrderNo.Location = new System.Drawing.Point(156, 105);
            this.numOrderNo.MinimumSize = new System.Drawing.Size(171, 20);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numOrderNo.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.numOrderNo.Properties.Mask.EditMask = "[0-9]{1,5}";
            this.numOrderNo.Properties.Mask.IgnoreMaskBlank = false;
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Properties.Mask.ShowPlaceHolders = false;
            this.numOrderNo.Properties.MaxLength = 5;
            this.numOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numOrderNo.Size = new System.Drawing.Size(171, 20);
            this.numOrderNo.TabIndex = 4;
            this.numOrderNo.ToolTipTitle = "Эрэмбийн дугаараа оруулна уу";
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Location = new System.Drawing.Point(156, 81);
            this.txtDesc.MinimumSize = new System.Drawing.Size(171, 20);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDesc.Properties.MaxLength = 200;
            this.txtDesc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDesc.Size = new System.Drawing.Size(171, 20);
            this.txtDesc.TabIndex = 3;
            this.txtDesc.ToolTipTitle = "Дэвсгэртийн нэр, тайлбараа оруулна уу";
            // 
            // cboCurrency
            // 
            this.cboCurrency.Location = new System.Drawing.Point(156, 35);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCurrency.Size = new System.Drawing.Size(171, 20);
            this.cboCurrency.TabIndex = 8;
            this.cboCurrency.ToolTipTitle = "Валютаа сонгоно уу";
            // 
            // FormPaBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 390);
            this.MaximumSize = new System.Drawing.Size(1024, 720);
            this.MinimumSize = new System.Drawing.Size(664, 417);
            this.Name = "FormPaBill";
            this.Text = "Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBankNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCurrency.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit numBankNote;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDesc;
        private DevExpress.XtraEditors.LookUpEdit cboCurrency;
    }
}