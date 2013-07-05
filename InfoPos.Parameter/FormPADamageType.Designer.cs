namespace InfoPos.Parameter
{
    partial class FormPADamageType
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
            this.txtDamageType = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDamageType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtDamageType);
            this.groupControl1.Controls.Add(this.labelControl1);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(139, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Эвдэрлийн төрлийн дугаар";
            // 
            // txtDamageType
            // 
            this.txtDamageType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDamageType.Location = new System.Drawing.Point(164, 41);
            this.txtDamageType.MinimumSize = new System.Drawing.Size(161, 20);
            this.txtDamageType.Name = "txtDamageType";
            this.txtDamageType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDamageType.Properties.MaxLength = 10;
            this.txtDamageType.Size = new System.Drawing.Size(161, 20);
            this.txtDamageType.TabIndex = 1;
            this.txtDamageType.ToolTipTitle = "Эвдэрлийн төрлийн дугаар оруулна уу.";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(164, 65);
            this.txtName.MinimumSize = new System.Drawing.Size(161, 20);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 200;
            this.txtName.Size = new System.Drawing.Size(161, 20);
            this.txtName.TabIndex = 3;
            this.txtName.ToolTipTitle = "Эвдэрлийн төрлийн нэр оруулна уу.";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(120, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Эвдэрлийн төрлийн нэр";
            // 
            // numOrderNo
            // 
            this.numOrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numOrderNo.Location = new System.Drawing.Point(164, 89);
            this.numOrderNo.MinimumSize = new System.Drawing.Size(161, 20);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "[0-9]{1,5}";
            this.numOrderNo.Properties.Mask.IgnoreMaskBlank = false;
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Properties.Mask.ShowPlaceHolders = false;
            this.numOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numOrderNo.Size = new System.Drawing.Size(161, 20);
            this.numOrderNo.TabIndex = 5;
            this.numOrderNo.ToolTipTitle = "Эрэмбийн дугаар оруулна уу.";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 92);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(87, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Эрэмбийн дугаар";
            // 
            // FormPADamageType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 379);
            this.Name = "FormPADamageType";
            this.Text = "Эвдэрлийн төрлийн бүртгэл";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDamageType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDamageType;
        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}