namespace InfoPos.Parameter
{
    partial class CRMIssuePriority
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numPriorID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPriorID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.numPriorID);
            this.groupControl1.Controls.Add(this.labelControl1);
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(167, 117);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Size = new System.Drawing.Size(164, 20);
            this.numOrderNo.TabIndex = 39;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(28, 120);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(107, 13);
            this.labelControl4.TabIndex = 38;
            this.labelControl4.Text = "Жагсаалтын эрэмбэ :";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(167, 91);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Size = new System.Drawing.Size(164, 20);
            this.txtName2.TabIndex = 37;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(28, 94);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(34, 13);
            this.labelControl3.TabIndex = 36;
            this.labelControl3.Text = "Нэр 2 :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(167, 65);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Size = new System.Drawing.Size(164, 20);
            this.txtName.TabIndex = 35;
            this.txtName.ToolTipTitle = "Нэр оруулна уу";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(28, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 13);
            this.labelControl2.TabIndex = 34;
            this.labelControl2.Text = "Нэр :";
            // 
            // numPriorID
            // 
            this.numPriorID.Location = new System.Drawing.Point(167, 39);
            this.numPriorID.Name = "numPriorID";
            this.numPriorID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.numPriorID.Properties.Appearance.Options.UseBackColor = true;
            this.numPriorID.Properties.Mask.EditMask = "\\d{0,4}";
            this.numPriorID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numPriorID.Size = new System.Drawing.Size(164, 20);
            this.numPriorID.TabIndex = 33;
            this.numPriorID.ToolTipTitle = "Эрэмбийн дугаар оруулна уу";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 13);
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "Эрэмбэ дугаар :";
            // 
            // CRMIssuePriority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 379);
            this.KeyPreview = true;
            this.Name = "CRMIssuePriority";
            this.Text = "Асуудлын эрэмбэ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CRMIssuePriority_FormClosing);
            this.Load += new System.EventHandler(this.CRMIssuePriority_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CRMIssuePriority_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPriorID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit numPriorID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}