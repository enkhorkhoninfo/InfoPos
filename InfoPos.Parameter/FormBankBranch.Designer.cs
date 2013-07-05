namespace InfoPos.Parameter
{
    partial class FormBankBranch
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtBranchID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.cboBankID = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranchID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBankID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtBranchID);
            this.groupControl1.Controls.Add(this.cboBankID);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Size = new System.Drawing.Size(572, 313);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 335);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 335);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 335);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 335);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 335);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(840, 317);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(259, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 317);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(259, 317);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(264, 0);
            this.panelControl3.Size = new System.Drawing.Size(576, 317);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(30, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(136, 13);
            this.labelControl4.TabIndex = 21;
            this.labelControl4.Text = "Банкны салбарын дугаар :";
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(179, 64);
            this.txtBranchID.Name = "txtBranchID";
            this.txtBranchID.Properties.Mask.EditMask = "\\d{0,16}";
            this.txtBranchID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtBranchID.Properties.MaxLength = 16;
            this.txtBranchID.Size = new System.Drawing.Size(268, 20);
            this.txtBranchID.TabIndex = 1;
            this.txtBranchID.ToolTipTitle = "Банкны салбарын дугаарыг оруулна уу";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 145);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(107, 13);
            this.labelControl5.TabIndex = 20;
            this.labelControl5.Text = "Жагсаалтын эрэмбэ :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 119);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(110, 13);
            this.labelControl3.TabIndex = 19;
            this.labelControl3.Text = "Салбарын англи нэр :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 93);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 13);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Салбарын нэр :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "Банкны код :";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(179, 116);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 50;
            this.txtName2.Size = new System.Drawing.Size(268, 20);
            this.txtName2.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(179, 90);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 2;
            this.txtName.ToolTipTitle = "Салбарын нэрийг оруулна уу";
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(179, 142);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Size = new System.Drawing.Size(268, 20);
            this.numOrderNo.TabIndex = 4;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // cboBankID
            // 
            this.cboBankID.Location = new System.Drawing.Point(179, 38);
            this.cboBankID.Name = "cboBankID";
            this.cboBankID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboBankID.Properties.NullText = "";
            this.cboBankID.Size = new System.Drawing.Size(268, 20);
            this.cboBankID.TabIndex = 0;
            this.cboBankID.ToolTipTitle = "Банкны кодыг оруулна уу";
            // 
            // FormBankBranch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 368);
            this.KeyPreview = true;
            this.Name = "FormBankBranch";
            this.Text = "Банкны салбар";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBankBranch_FormClosing);
            this.Load += new System.EventHandler(this.FormBankBranch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBankBranch_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBranchID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBankID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtBranchID;
        private DevExpress.XtraEditors.LookUpEdit cboBankID;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtName2;

    }
}