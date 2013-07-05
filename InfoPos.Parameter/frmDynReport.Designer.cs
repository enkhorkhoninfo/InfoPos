namespace InfoPos.Parameter
{
    partial class frmDynReport
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
            this.Dynamic = new DevExpress.XtraEditors.GroupControl();
            this.Slips = new DevExpress.XtraEditors.GroupControl();
            this.txtSlipsname = new DevExpress.XtraEditors.TextEdit();
            this.btnChoose1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.btnChoose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboTxnCode = new DevExpress.XtraEditors.LookUpEdit();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.cbotype = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.Dynamic)).BeginInit();
            this.Dynamic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Slips)).BeginInit();
            this.Slips.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSlipsname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTxnCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbotype.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Dynamic
            // 
            this.Dynamic.Controls.Add(this.Slips);
            this.Dynamic.Controls.Add(this.btnChoose);
            this.Dynamic.Controls.Add(this.labelControl2);
            this.Dynamic.Controls.Add(this.labelControl1);
            this.Dynamic.Controls.Add(this.cboTxnCode);
            this.Dynamic.Controls.Add(this.txtFilePath);
            this.Dynamic.Location = new System.Drawing.Point(12, 27);
            this.Dynamic.Name = "Dynamic";
            this.Dynamic.Size = new System.Drawing.Size(560, 90);
            this.Dynamic.TabIndex = 0;
            // 
            // Slips
            // 
            this.Slips.Controls.Add(this.txtSlipsname);
            this.Slips.Controls.Add(this.btnChoose1);
            this.Slips.Controls.Add(this.labelControl4);
            this.Slips.Controls.Add(this.labelControl5);
            this.Slips.Controls.Add(this.txtPath);
            this.Slips.Location = new System.Drawing.Point(0, 0);
            this.Slips.Name = "Slips";
            this.Slips.Size = new System.Drawing.Size(560, 90);
            this.Slips.TabIndex = 8;
            // 
            // txtSlipsname
            // 
            this.txtSlipsname.EditValue = "";
            this.txtSlipsname.Location = new System.Drawing.Point(188, 25);
            this.txtSlipsname.Name = "txtSlipsname";
            this.txtSlipsname.Size = new System.Drawing.Size(260, 20);
            this.txtSlipsname.TabIndex = 1;
            // 
            // btnChoose1
            // 
            this.btnChoose1.Location = new System.Drawing.Point(457, 57);
            this.btnChoose1.Name = "btnChoose1";
            this.btnChoose1.Size = new System.Drawing.Size(85, 24);
            this.btnChoose1.TabIndex = 4;
            this.btnChoose1.Text = "Сонгох";
            this.btnChoose1.Click += new System.EventHandler(this.btnChoose1_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 62);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(53, 13);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "*.rpt файл";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 28);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(103, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Slips тайлангын нэр:";
            // 
            // txtPath
            // 
            this.txtPath.EditValue = "";
            this.txtPath.Location = new System.Drawing.Point(188, 59);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(260, 20);
            this.txtPath.TabIndex = 3;
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(457, 57);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(85, 24);
            this.btnChoose.TabIndex = 7;
            this.btnChoose.Text = "Сонгох";
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(154, 26);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Тайлангын *.rpt,*.ini файлууд\r\n байрлаж буй хавтас";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Гүйлгээний код:";
            // 
            // cboTxnCode
            // 
            this.cboTxnCode.Location = new System.Drawing.Point(188, 25);
            this.cboTxnCode.Name = "cboTxnCode";
            this.cboTxnCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTxnCode.Size = new System.Drawing.Size(260, 20);
            this.cboTxnCode.TabIndex = 2;
            // 
            // txtFilePath
            // 
            this.txtFilePath.EditValue = "";
            this.txtFilePath.Location = new System.Drawing.Point(188, 59);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(260, 20);
            this.txtFilePath.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(487, 124);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 24);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Гарах";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(394, 124);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(85, 24);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "Үүсгэх";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // cbotype
            // 
            this.cbotype.Location = new System.Drawing.Point(200, 2);
            this.cbotype.Name = "cbotype";
            this.cbotype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbotype.Size = new System.Drawing.Size(260, 20);
            this.cbotype.TabIndex = 0;
            this.cbotype.EditValueChanged += new System.EventHandler(this.cbotype_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(24, 5);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(95, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Тайлангын төрөл :";
            // 
            // frmDynReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 157);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.cbotype);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.Dynamic);
            this.Controls.Add(this.btnCreate);
            this.MaximumSize = new System.Drawing.Size(593, 184);
            this.Name = "frmDynReport";
            this.Text = "Тайлангын файл үүсгэх";
            this.Load += new System.EventHandler(this.frmDynReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Dynamic)).EndInit();
            this.Dynamic.ResumeLayout(false);
            this.Dynamic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Slips)).EndInit();
            this.Slips.ResumeLayout(false);
            this.Slips.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSlipsname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTxnCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbotype.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl Dynamic;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraEditors.SimpleButton btnChoose;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.LookUpEdit cboTxnCode;
        private DevExpress.XtraEditors.LookUpEdit cbotype;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GroupControl Slips;
        private DevExpress.XtraEditors.TextEdit txtSlipsname;
        private DevExpress.XtraEditors.SimpleButton btnChoose1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtPath;
    }
}