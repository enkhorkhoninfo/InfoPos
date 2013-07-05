namespace InfoPos.Parameter
{
    partial class FormPledgetype
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
            this.numTypeID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtTypeName = new DevExpress.XtraEditors.TextEdit();
            this.txtMaskValue = new DevExpress.XtraEditors.TextEdit();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.cboMaskType = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTypeID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTypeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaskValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaskType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboMaskType);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.txtMaskValue);
            this.groupControl1.Controls.Add(this.txtTypeName);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.numTypeID);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Size = new System.Drawing.Size(358, 359);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 381);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 381);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 381);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 381);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 381);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(659, 363);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(292, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 363);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(292, 363);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(297, 0);
            this.panelControl3.Size = new System.Drawing.Size(362, 363);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Төрлийн дугаар:";
            // 
            // numTypeID
            // 
            this.numTypeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numTypeID.Location = new System.Drawing.Point(126, 35);
            this.numTypeID.MinimumSize = new System.Drawing.Size(218, 20);
            this.numTypeID.Name = "numTypeID";
            this.numTypeID.Properties.Mask.EditMask = "\\d{0,4}";
            this.numTypeID.Properties.Mask.IgnoreMaskBlank = false;
            this.numTypeID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numTypeID.Properties.Mask.ShowPlaceHolders = false;
            this.numTypeID.Size = new System.Drawing.Size(218, 20);
            this.numTypeID.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Төрлийн нэр:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Маскны утга:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(16, 112);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(76, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Маскны төрөл:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(16, 137);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(104, 13);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "Жагсаалтын эрэмбэ:";
            // 
            // txtTypeName
            // 
            this.txtTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTypeName.Location = new System.Drawing.Point(126, 60);
            this.txtTypeName.MinimumSize = new System.Drawing.Size(218, 20);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(218, 20);
            this.txtTypeName.TabIndex = 6;
            // 
            // txtMaskValue
            // 
            this.txtMaskValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaskValue.Location = new System.Drawing.Point(126, 84);
            this.txtMaskValue.MinimumSize = new System.Drawing.Size(218, 20);
            this.txtMaskValue.Name = "txtMaskValue";
            this.txtMaskValue.Size = new System.Drawing.Size(218, 20);
            this.txtMaskValue.TabIndex = 7;
            // 
            // numOrderNo
            // 
            this.numOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numOrderNo.Location = new System.Drawing.Point(126, 134);
            this.numOrderNo.MinimumSize = new System.Drawing.Size(218, 20);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.IgnoreMaskBlank = false;
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Properties.Mask.ShowPlaceHolders = false;
            this.numOrderNo.Size = new System.Drawing.Size(218, 20);
            this.numOrderNo.TabIndex = 9;
            // 
            // cboMaskType
            // 
            this.cboMaskType.Location = new System.Drawing.Point(126, 109);
            this.cboMaskType.Name = "cboMaskType";
            this.cboMaskType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.cboMaskType.Properties.Appearance.Options.UseBackColor = true;
            this.cboMaskType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMaskType.Properties.NullText = "";
            this.cboMaskType.Size = new System.Drawing.Size(218, 20);
            this.cboMaskType.TabIndex = 10;
            this.cboMaskType.ToolTipTitle = "Маскны төрөл оруулна уу";
            // 
            // FormPledgetype
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 414);
            this.Name = "FormPledgetype";
            this.Text = "Барьцааны төрөл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCustomerMask_FormClosing);
            this.Load += new System.EventHandler(this.FormCustomerMask_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCustomerMask_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTypeID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTypeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaskValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaskType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit numTypeID;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.TextEdit txtMaskValue;
        private DevExpress.XtraEditors.TextEdit txtTypeName;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit cboMaskType;
    }
}