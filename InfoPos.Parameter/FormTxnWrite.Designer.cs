namespace InfoPos.Parameter
{
    partial class FormTxnWrite
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
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.cboEntryCode = new DevExpress.XtraEditors.LookUpEdit();
            this.cboTranCode = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEntryCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTranCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.cboEntryCode);
            this.groupControl1.Controls.Add(this.cboTranCode);
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
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(144, 13);
            this.labelControl1.TabIndex = 42;
            this.labelControl1.Text = "Санхүүгийн бичилтийн код :";
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
            this.labelControl2.Size = new System.Drawing.Size(107, 13);
            this.labelControl2.TabIndex = 43;
            this.labelControl2.Text = "Жагсаалтын эрэмбэ :";
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(188, 90);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Properties.MaxLength = 200;
            this.numOrderNo.Size = new System.Drawing.Size(268, 20);
            this.numOrderNo.TabIndex = 3;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // cboEntryCode
            // 
            this.cboEntryCode.Location = new System.Drawing.Point(188, 64);
            this.cboEntryCode.Name = "cboEntryCode";
            this.cboEntryCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboEntryCode.Properties.MaxLength = 200;
            this.cboEntryCode.Properties.NullText = "";
            this.cboEntryCode.Size = new System.Drawing.Size(268, 20);
            this.cboEntryCode.TabIndex = 2;
            this.cboEntryCode.ToolTipTitle = "Санхүүгийн бичилтийн код оруулна уу";
            // 
            // cboTranCode
            // 
            this.cboTranCode.Location = new System.Drawing.Point(188, 38);
            this.cboTranCode.Name = "cboTranCode";
            this.cboTranCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTranCode.Properties.NullText = "";
            this.cboTranCode.Size = new System.Drawing.Size(268, 20);
            this.cboTranCode.TabIndex = 1;
            this.cboTranCode.ToolTipTitle = "Гүйлгээний код оруулна уу";
            // 
            // FormTxnWrite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 368);
            this.KeyPreview = true;
            this.Name = "FormTxnWrite";
            this.Text = "Гүйлгээний санхүүгийн бичилт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTxnWrite_FormClosing);
            this.Load += new System.EventHandler(this.FormTxnWrite_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormTxnWrite_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEntryCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTranCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.LookUpEdit cboEntryCode;
        private DevExpress.XtraEditors.LookUpEdit cboTranCode;
    }
}