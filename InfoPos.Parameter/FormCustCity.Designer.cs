namespace InfoPos.Parameter
{
    partial class FormCustCity
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
            this.numCityCode = new DevExpress.XtraEditors.TextEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.numCityCode.Properties)).BeginInit();
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
            this.groupControl1.Controls.Add(this.numCityCode);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Size = new System.Drawing.Size(528, 312);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 334);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 334);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 334);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 334);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 334);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(793, 316);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(256, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 316);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(256, 316);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(261, 0);
            this.panelControl3.Size = new System.Drawing.Size(532, 316);
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(197, 116);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Size = new System.Drawing.Size(268, 20);
            this.numOrderNo.TabIndex = 3;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // numCityCode
            // 
            this.numCityCode.Location = new System.Drawing.Point(197, 38);
            this.numCityCode.Name = "numCityCode";
            this.numCityCode.Properties.Mask.BeepOnError = true;
            this.numCityCode.Properties.Mask.EditMask = "\\d{0,4}";
            this.numCityCode.Properties.Mask.IgnoreMaskBlank = false;
            this.numCityCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numCityCode.Properties.Mask.SaveLiteral = false;
            this.numCityCode.Size = new System.Drawing.Size(268, 20);
            this.numCityCode.TabIndex = 0;
            this.numCityCode.ToolTipTitle = "Аймаг хотын код оруулна уу";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(197, 64);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 1;
            this.txtName.ToolTipTitle = "Аймаг хотын нэр оруулна уу";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(197, 90);
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
            this.labelControl1.Size = new System.Drawing.Size(94, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Аймаг хотын код :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(92, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Аймаг хотын нэр :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 93);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(98, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Аймаг хотын нэр 2:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 119);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(107, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Жагсаалтын эрэмбэ :";
            // 
            // FormCustCity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 367);
            this.KeyPreview = true;
            this.Name = "FormCustCity";
            this.Text = "Аймаг хотын бүртгэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCustCity_FormClosing);
            this.Load += new System.EventHandler(this.FormCustCity_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCustCity_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCityCode.Properties)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit numCityCode;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
    }
}