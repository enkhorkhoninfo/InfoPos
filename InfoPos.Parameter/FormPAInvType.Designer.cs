namespace InfoPos.Parameter
{
    partial class FormPAInvType
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
            this.txtInvType = new DevExpress.XtraEditors.TextEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cboClassCode = new DevExpress.XtraEditors.LookUpEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClassCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.cboClassCode);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.txtNote);
            this.groupControl1.Controls.Add(this.txtInvType);
            this.groupControl1.Size = new System.Drawing.Size(367, 421);
            this.groupControl1.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 443);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 443);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 443);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 443);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 443);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(800, 425);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(424, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 425);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(424, 425);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(429, 0);
            this.panelControl3.Size = new System.Drawing.Size(371, 425);
            // 
            // txtInvType
            // 
            this.txtInvType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInvType.Location = new System.Drawing.Point(132, 33);
            this.txtInvType.MinimumSize = new System.Drawing.Size(174, 20);
            this.txtInvType.Name = "txtInvType";
            this.txtInvType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInvType.Properties.MaxLength = 20;
            this.txtInvType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtInvType.Size = new System.Drawing.Size(174, 20);
            this.txtInvType.TabIndex = 0;
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Location = new System.Drawing.Point(132, 125);
            this.txtNote.MinimumSize = new System.Drawing.Size(174, 155);
            this.txtNote.Name = "txtNote";
            this.txtNote.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNote.Size = new System.Drawing.Size(174, 155);
            this.txtNote.TabIndex = 4;
            // 
            // numOrderNo
            // 
            this.numOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numOrderNo.Location = new System.Drawing.Point(132, 283);
            this.numOrderNo.MinimumSize = new System.Drawing.Size(174, 20);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "[0-9]{1,5}";
            this.numOrderNo.Properties.Mask.IgnoreMaskBlank = false;
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Properties.Mask.ShowPlaceHolders = false;
            this.numOrderNo.Properties.MaxLength = 5;
            this.numOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numOrderNo.Size = new System.Drawing.Size(174, 20);
            this.numOrderNo.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(107, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Төрлийн код, дугаар";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Төрлийн нэр";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 81);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Төрлийн нэр 2";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(19, 104);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(42, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Ангилал";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(19, 128);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(42, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Тайлбар";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(19, 286);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(87, 13);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Эрэмбийн дугаар";
            // 
            // cboClassCode
            // 
            this.cboClassCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClassCode.Location = new System.Drawing.Point(132, 101);
            this.cboClassCode.MinimumSize = new System.Drawing.Size(174, 20);
            this.cboClassCode.Name = "cboClassCode";
            this.cboClassCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboClassCode.Properties.NullText = "";
            this.cboClassCode.Properties.PopupSizeable = false;
            this.cboClassCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboClassCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cboClassCode.Size = new System.Drawing.Size(174, 20);
            this.cboClassCode.TabIndex = 3;
            this.cboClassCode.ToolTipTitle = "Төрөл сонгоно уу";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(132, 55);
            this.txtName.MinimumSize = new System.Drawing.Size(174, 20);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 200;
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName.Size = new System.Drawing.Size(174, 20);
            this.txtName.TabIndex = 1;
            // 
            // txtName2
            // 
            this.txtName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName2.Location = new System.Drawing.Point(132, 78);
            this.txtName2.MinimumSize = new System.Drawing.Size(174, 20);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 200;
            this.txtName2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName2.Size = new System.Drawing.Size(174, 20);
            this.txtName2.TabIndex = 2;
            // 
            // FormPAInvType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 476);
            this.MaximumSize = new System.Drawing.Size(1024, 7200);
            this.MinimumSize = new System.Drawing.Size(840, 514);
            this.Name = "FormPAInvType";
            this.Text = "Бараа материалын төрлийн бүртгэл";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtInvType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboClassCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtInvType;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.MemoEdit txtNote;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cboClassCode;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.TextEdit txtName;
    }
}