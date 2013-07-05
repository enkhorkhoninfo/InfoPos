namespace InfoPos.Parameter
{
    partial class FormRebateMaster
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.numMasterID = new DevExpress.XtraEditors.TextEdit();
            this.numName = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.numListOrder = new DevExpress.XtraEditors.TextEdit();
            this.cboMasterType = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMasterID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numListOrder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMasterType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboMasterType);
            this.groupControl1.Controls.Add(this.numListOrder);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.numName);
            this.groupControl1.Controls.Add(this.numMasterID);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Size = new System.Drawing.Size(358, 323);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 345);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 345);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 345);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 345);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 345);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(687, 327);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(320, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 327);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(320, 327);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(325, 0);
            this.panelControl3.Size = new System.Drawing.Size(362, 327);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 45);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ID дугаар";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(87, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Урамшуулал эсэх";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 89);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(18, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Нэр";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(24, 112);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(27, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Нэр 2";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(24, 137);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(100, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Жагсаалтын эрэмбэ";
            // 
            // numMasterID
            // 
            this.numMasterID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMasterID.Location = new System.Drawing.Point(129, 42);
            this.numMasterID.MinimumSize = new System.Drawing.Size(177, 20);
            this.numMasterID.Name = "numMasterID";
            this.numMasterID.Properties.Mask.EditMask = "[0-9]{2,10}";
            this.numMasterID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numMasterID.Properties.MaxLength = 10;
            this.numMasterID.Size = new System.Drawing.Size(177, 20);
            this.numMasterID.TabIndex = 5;
            // 
            // numName
            // 
            this.numName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numName.Location = new System.Drawing.Point(129, 86);
            this.numName.MinimumSize = new System.Drawing.Size(177, 20);
            this.numName.Name = "numName";
            this.numName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numName.Properties.Mask.IgnoreMaskBlank = false;
            this.numName.Properties.Mask.ShowPlaceHolders = false;
            this.numName.Properties.MaxLength = 16;
            this.numName.Size = new System.Drawing.Size(177, 20);
            this.numName.TabIndex = 6;
            // 
            // txtName2
            // 
            this.txtName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName2.Location = new System.Drawing.Point(129, 109);
            this.txtName2.MinimumSize = new System.Drawing.Size(177, 20);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 16;
            this.txtName2.Size = new System.Drawing.Size(177, 20);
            this.txtName2.TabIndex = 7;
            // 
            // numListOrder
            // 
            this.numListOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numListOrder.Location = new System.Drawing.Point(129, 134);
            this.numListOrder.MinimumSize = new System.Drawing.Size(177, 20);
            this.numListOrder.Name = "numListOrder";
            this.numListOrder.Properties.Mask.EditMask = "[0-9]{1,2}";
            this.numListOrder.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numListOrder.Size = new System.Drawing.Size(177, 20);
            this.numListOrder.TabIndex = 8;
            // 
            // cboMasterType
            // 
            this.cboMasterType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMasterType.Location = new System.Drawing.Point(129, 64);
            this.cboMasterType.MinimumSize = new System.Drawing.Size(177, 20);
            this.cboMasterType.Name = "cboMasterType";
            this.cboMasterType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMasterType.Size = new System.Drawing.Size(177, 20);
            this.cboMasterType.TabIndex = 9;
            // 
            // FormRebateMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 378);
            this.Name = "FormRebateMaster";
            this.Text = "Тооцоолол хийх матриц";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMasterID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numListOrder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMasterType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cboMasterType;
        private DevExpress.XtraEditors.TextEdit numListOrder;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.TextEdit numName;
        private DevExpress.XtraEditors.TextEdit numMasterID;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}