namespace InfoPos.Reports
{
    partial class BacTxnReport
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtReportPriv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReportName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnView1 = new DevExpress.XtraEditors.SimpleButton();
            this.cboOutType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Тайлан гаргах төрөл : ";
            // 
            // txtReportPriv
            // 
            this.txtReportPriv.Location = new System.Drawing.Point(106, 13);
            this.txtReportPriv.Name = "txtReportPriv";
            this.txtReportPriv.ReadOnly = true;
            this.txtReportPriv.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtReportPriv.Size = new System.Drawing.Size(115, 20);
            this.txtReportPriv.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Гүйлгээ код:";
            // 
            // txtReportName
            // 
            this.txtReportName.Location = new System.Drawing.Point(106, 40);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Properties.ReadOnly = true;
            this.txtReportName.Size = new System.Drawing.Size(337, 20);
            this.txtReportName.TabIndex = 14;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(86, 13);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "Тайлангийн нэр :";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(352, 66);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(90, 31);
            this.simpleButton1.TabIndex = 16;
            this.simpleButton1.Text = "Болих";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnView1
            // 
            this.btnView1.Location = new System.Drawing.Point(256, 66);
            this.btnView1.Name = "btnView1";
            this.btnView1.Size = new System.Drawing.Size(90, 31);
            this.btnView1.TabIndex = 17;
            this.btnView1.Text = "Харах";
            this.btnView1.Click += new System.EventHandler(this.btnView1_Click);
            // 
            // cboOutType
            // 
            this.cboOutType.FormattingEnabled = true;
            this.cboOutType.Items.AddRange(new object[] {
            "Doc",
            "Pdf",
            "Xls"});
            this.cboOutType.Location = new System.Drawing.Point(354, 13);
            this.cboOutType.Name = "cboOutType";
            this.cboOutType.Size = new System.Drawing.Size(89, 21);
            this.cboOutType.TabIndex = 18;
            this.cboOutType.Text = "Xls";
            // 
            // BacTxnReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 102);
            this.Controls.Add(this.cboOutType);
            this.Controls.Add(this.btnView1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtReportName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtReportPriv);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(470, 140);
            this.MinimumSize = new System.Drawing.Size(470, 140);
            this.Name = "BacTxnReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Динамик тайлан харах";
            this.Load += new System.EventHandler(this.BacTxnReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BacTxnReport_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtReportName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReportPriv;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtReportName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnView1;
        private System.Windows.Forms.ComboBox cboOutType;
    }
}