namespace InfoPos.Parameter
{
    partial class FormExtenseType
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
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.numTypeCode = new DevExpress.XtraEditors.TextEdit();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTypeCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
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
            this.groupControl1.Controls.Add(this.numTypeCode);
            this.groupControl1.Controls.Add(this.numOrderNo);
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
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 119);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(107, 13);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Жагсаалтын эрэмбэ :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 93);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Төрлийн нэр 2 :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "Төрлийн нэр :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Төрлийн код :";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(173, 90);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 50;
            this.txtName2.Size = new System.Drawing.Size(268, 20);
            this.txtName2.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(173, 64);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 1;
            this.txtName.ToolTipTitle = "Төрлийн нэр оруулна уу";
            // 
            // numTypeCode
            // 
            this.numTypeCode.Location = new System.Drawing.Point(173, 38);
            this.numTypeCode.Name = "numTypeCode";
            this.numTypeCode.Properties.Mask.BeepOnError = true;
            this.numTypeCode.Properties.Mask.EditMask = "\\d{0,4}";
            this.numTypeCode.Properties.Mask.IgnoreMaskBlank = false;
            this.numTypeCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numTypeCode.Properties.Mask.SaveLiteral = false;
            this.numTypeCode.Size = new System.Drawing.Size(268, 20);
            this.numTypeCode.TabIndex = 0;
            this.numTypeCode.ToolTipTitle = "Төрлийн код оруулна уу";
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(173, 116);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Size = new System.Drawing.Size(268, 20);
            this.numOrderNo.TabIndex = 3;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // FormExtenseType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 368);
            this.KeyPreview = true;
            this.Name = "FormExtenseType";
            this.Text = "Нөхөн төлбөрийн зардлын төрөл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormExtenseType_FormClosing);
            this.Load += new System.EventHandler(this.FormExtenseType_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormExtenseType_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTypeCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit numTypeCode;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
    }
}