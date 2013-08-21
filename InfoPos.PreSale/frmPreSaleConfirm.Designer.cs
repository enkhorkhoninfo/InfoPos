namespace InfoPos.PreSale
{
    partial class frmPreSaleConfirm
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl40 = new DevExpress.XtraEditors.LabelControl();
            this.btnOrderNo = new DevExpress.XtraEditors.SimpleButton();
            this.txtPreSaleNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreSaleNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtCustomerName);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.btnOk);
            this.groupControl1.Controls.Add(this.btnCancel);
            this.groupControl1.Controls.Add(this.labelControl40);
            this.groupControl1.Controls.Add(this.btnOrderNo);
            this.groupControl1.Controls.Add(this.txtPreSaleNo);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtNote);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(459, 227);
            this.groupControl1.TabIndex = 19;
            this.groupControl1.Text = "Өгөгдөл";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(124, 13);
            this.labelControl2.TabIndex = 96;
            this.labelControl2.Text = "УБ ын харилцагчийн нэр";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.EditValue = "";
            this.txtCustomerName.Enabled = false;
            this.txtCustomerName.Location = new System.Drawing.Point(148, 59);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCustomerName.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerName.Properties.MaxLength = 50;
            this.txtCustomerName.Size = new System.Drawing.Size(299, 20);
            this.txtCustomerName.TabIndex = 95;
            this.txtCustomerName.ToolTipTitle = "Харилцагч оруулна уу";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(392, 30);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(56, 23);
            this.simpleButton1.TabIndex = 94;
            this.simpleButton1.Text = "Лавлах";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(19, 192);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(108, 23);
            this.btnOk.TabIndex = 93;
            this.btnOk.Text = "Баталгаажуулах";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(145, 192);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 23);
            this.btnCancel.TabIndex = 92;
            this.btnCancel.Text = "Болих";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl40
            // 
            this.labelControl40.Location = new System.Drawing.Point(18, 90);
            this.labelControl40.Name = "labelControl40";
            this.labelControl40.Size = new System.Drawing.Size(42, 13);
            this.labelControl40.TabIndex = 91;
            this.labelControl40.Text = "Тайлбар";
            // 
            // btnOrderNo
            // 
            this.btnOrderNo.Location = new System.Drawing.Point(330, 30);
            this.btnOrderNo.Name = "btnOrderNo";
            this.btnOrderNo.Size = new System.Drawing.Size(56, 23);
            this.btnOrderNo.TabIndex = 11;
            this.btnOrderNo.Text = "Хайх";
            this.btnOrderNo.Click += new System.EventHandler(this.btnOrderNo_Click);
            // 
            // txtPreSaleNo
            // 
            this.txtPreSaleNo.Enabled = false;
            this.txtPreSaleNo.Location = new System.Drawing.Point(148, 33);
            this.txtPreSaleNo.Name = "txtPreSaleNo";
            this.txtPreSaleNo.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtPreSaleNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtPreSaleNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPreSaleNo.Size = new System.Drawing.Size(176, 20);
            this.txtPreSaleNo.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "УБ дугаар";
            // 
            // txtNote
            // 
            this.txtNote.EditValue = "";
            this.txtNote.Location = new System.Drawing.Point(19, 109);
            this.txtNote.Name = "txtNote";
            this.txtNote.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtNote.Properties.Appearance.Options.UseBackColor = true;
            this.txtNote.Properties.MaxLength = 50;
            this.txtNote.Size = new System.Drawing.Size(429, 70);
            this.txtNote.TabIndex = 3;
            this.txtNote.ToolTipTitle = "Харилцагч оруулна уу";
            // 
            // frmPreSaleConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 227);
            this.Controls.Add(this.groupControl1);
            this.Name = "frmPreSaleConfirm";
            this.Text = "Урьдчилсан борлуулалт баталгаажуулах";
            this.Load += new System.EventHandler(this.frmOrderConfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreSaleNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl40;
        private DevExpress.XtraEditors.SimpleButton btnOrderNo;
        private DevExpress.XtraEditors.TextEdit txtPreSaleNo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtNote;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
    }
}