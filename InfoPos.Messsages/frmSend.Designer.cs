namespace InfoPos.Messages
{
    partial class frmSend
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.mmoDesc = new DevExpress.XtraEditors.MemoEdit();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboUserNo = new DevExpress.XtraEditors.LookUpEdit();
            this.cboGroupID = new DevExpress.XtraEditors.LookUpEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmoDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroupID.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.mmoDesc);
            this.groupControl1.Location = new System.Drawing.Point(12, 95);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(444, 221);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Илгээх мэдээлэл";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // mmoDesc
            // 
            this.mmoDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mmoDesc.Location = new System.Drawing.Point(2, 22);
            this.mmoDesc.Name = "mmoDesc";
            this.mmoDesc.Properties.MaxLength = 500;
            this.mmoDesc.Size = new System.Drawing.Size(440, 197);
            this.mmoDesc.TabIndex = 0;
            this.mmoDesc.EditValueChanged += new System.EventHandler(this.mmoDesc_EditValueChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(276, 322);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(86, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Илгээх";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(368, 322);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Гарах";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Хэрэглэгч:";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(101, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Хэрэглэгчийн бүлэг:";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // cboUserNo
            // 
            this.cboUserNo.Location = new System.Drawing.Point(149, 44);
            this.cboUserNo.Name = "cboUserNo";
            this.cboUserNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUserNo.Size = new System.Drawing.Size(238, 20);
            this.cboUserNo.TabIndex = 1;
            // 
            // cboGroupID
            // 
            this.cboGroupID.Location = new System.Drawing.Point(149, 18);
            this.cboGroupID.Name = "cboGroupID";
            this.cboGroupID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGroupID.Size = new System.Drawing.Size(238, 20);
            this.cboGroupID.TabIndex = 0;
            this.cboGroupID.EditValueChanged += new System.EventHandler(this.cboGroupID_EditValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboGroupID);
            this.groupBox1.Controls.Add(this.cboUserNo);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 77);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // frmSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 356);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClose);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(484, 383);
            this.MinimumSize = new System.Drawing.Size(476, 372);
            this.Name = "frmSend";
            this.Text = "Захидал илгээх";
            this.Load += new System.EventHandler(this.frmSend_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSend_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mmoDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUserNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroupID.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cboUserNo;
        private DevExpress.XtraEditors.LookUpEdit cboGroupID;
        private DevExpress.XtraEditors.MemoEdit mmoDesc;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}