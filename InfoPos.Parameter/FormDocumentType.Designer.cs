namespace HeavenPro.Parameter
{
    partial class FormTxn
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
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numTranCode = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTranCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.numTranCode);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Size = new System.Drawing.Size(575, 313);
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
            this.splitterControl1.Location = new System.Drawing.Point(256, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 317);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(256, 317);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(261, 0);
            this.panelControl3.Size = new System.Drawing.Size(579, 317);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(188, 64);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.Mask.BeepOnError = true;
            this.txtName.Properties.Mask.IgnoreMaskBlank = false;
            this.txtName.Properties.Mask.SaveLiteral = false;
            this.txtName.Properties.MaxLength = 200;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 40;
            this.txtName.ToolTipTitle = "Гүйлгээний нэр оруулна уу";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 13);
            this.labelControl1.TabIndex = 42;
            this.labelControl1.Text = "Гүйлгээний нэр :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(30, 41);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(85, 13);
            this.labelControl6.TabIndex = 44;
            this.labelControl6.Text = "Гүйлгээний код :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 93);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(92, 13);
            this.labelControl2.TabIndex = 43;
            this.labelControl2.Text = "Гүйлгээний нэр 2 :";
            // 
            // numTranCode
            // 
            this.numTranCode.Location = new System.Drawing.Point(188, 38);
            this.numTranCode.Name = "numTranCode";
            this.numTranCode.Properties.Mask.BeepOnError = true;
            this.numTranCode.Properties.Mask.EditMask = "\\d{0,16}";
            this.numTranCode.Properties.Mask.IgnoreMaskBlank = false;
            this.numTranCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numTranCode.Properties.Mask.SaveLiteral = false;
            this.numTranCode.Size = new System.Drawing.Size(268, 20);
            this.numTranCode.TabIndex = 39;
            this.numTranCode.ToolTipTitle = "Гүйлгээний код оруулна уу";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(188, 90);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 200;
            this.txtName2.Size = new System.Drawing.Size(268, 20);
            this.txtName2.TabIndex = 41;
            // 
            // FormTxn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 368);
            this.KeyPreview = true;
            this.Name = "FormTxn";
            this.Text = "Гүйлгээний төрөл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTxn_FormClosing);
            this.Load += new System.EventHandler(this.FormTxn_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormTxn_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTranCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit numTranCode;
        private DevExpress.XtraEditors.TextEdit txtName2;
    }
}