namespace InfoPos.Parameter
{
    partial class FormLanguage
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
            this.numLanguageCode = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLanguageCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.numLanguageCode);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Size = new System.Drawing.Size(559, 313);
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
            this.panelControl1.Size = new System.Drawing.Size(824, 317);
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
            this.panelControl3.Size = new System.Drawing.Size(563, 317);
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(176, 116);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Size = new System.Drawing.Size(268, 20);
            this.numOrderNo.TabIndex = 3;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // numLanguageCode
            // 
            this.numLanguageCode.Location = new System.Drawing.Point(176, 38);
            this.numLanguageCode.Name = "numLanguageCode";
            this.numLanguageCode.Properties.Mask.BeepOnError = true;
            this.numLanguageCode.Properties.Mask.EditMask = "\\d{0,4}";
            this.numLanguageCode.Properties.Mask.IgnoreMaskBlank = false;
            this.numLanguageCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numLanguageCode.Properties.Mask.SaveLiteral = false;
            this.numLanguageCode.Size = new System.Drawing.Size(268, 20);
            this.numLanguageCode.TabIndex = 0;
            this.numLanguageCode.ToolTipTitle = "Хэлний код оруулна уу";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(176, 64);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 1;
            this.txtName.ToolTipTitle = "Улсын нэр оруулна уу";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(176, 90);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 50;
            this.txtName2.Size = new System.Drawing.Size(268, 20);
            this.txtName2.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Хэлний код :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Улсын нэр :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 93);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(65, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Улсын нэр 2:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 119);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(107, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Жагсаалтын эрэмбэ :";
            // 
            // FormLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 368);
            this.KeyPreview = true;
            this.Name = "FormLanguage";
            this.Text = "Хэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLanguage_FormClosing);
            this.Load += new System.EventHandler(this.FormLanguage_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormLanguage_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLanguageCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit numLanguageCode;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
    }
}