namespace InfoPos.Reg
{
    partial class frmRegFine
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
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtProdName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFineNo = new DevExpress.XtraEditors.TextEdit();
            this.btnFree = new System.Windows.Forms.Button();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.numOverTime = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtRentStatus = new DevExpress.XtraEditors.TextEdit();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFineNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOverTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRentStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(131, 96);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(65, 16);
            this.labelControl7.TabIndex = 2;
            this.labelControl7.Text = "Тэмдэглэл:";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(339, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(132, 52);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Буцах";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayment.Location = new System.Drawing.Point(193, 6);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(132, 52);
            this.btnPayment.TabIndex = 16;
            this.btnPayment.Text = "Төлөх";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(78, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(118, 16);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "Түрээсийн хэрэгсэл:";
            // 
            // txtProdName
            // 
            this.txtProdName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProdName.Location = new System.Drawing.Point(202, 22);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProdName.Properties.Appearance.Options.UseFont = true;
            this.txtProdName.Properties.AutoHeight = false;
            this.txtProdName.Properties.ReadOnly = true;
            this.txtProdName.Size = new System.Drawing.Size(295, 28);
            this.txtProdName.TabIndex = 19;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(17, 215);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(179, 16);
            this.labelControl3.TabIndex = 22;
            this.labelControl3.Text = "Төлбөрийн гүйлгээний дугаар:";
            // 
            // txtFineNo
            // 
            this.txtFineNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFineNo.EditValue = "";
            this.txtFineNo.Location = new System.Drawing.Point(202, 211);
            this.txtFineNo.Name = "txtFineNo";
            this.txtFineNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFineNo.Properties.Appearance.Options.UseFont = true;
            this.txtFineNo.Properties.AutoHeight = false;
            this.txtFineNo.Size = new System.Drawing.Size(124, 28);
            this.txtFineNo.TabIndex = 23;
            // 
            // btnFree
            // 
            this.btnFree.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFree.Location = new System.Drawing.Point(46, 6);
            this.btnFree.Name = "btnFree";
            this.btnFree.Size = new System.Drawing.Size(132, 52);
            this.btnFree.TabIndex = 24;
            this.btnFree.Text = "Чөлөөлөх!";
            this.btnFree.UseVisualStyleBackColor = true;
            this.btnFree.Click += new System.EventHandler(this.btnFree_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnFree);
            this.panelControl1.Controls.Add(this.btnPayment);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 266);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(516, 64);
            this.panelControl1.TabIndex = 25;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(96, 181);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(100, 16);
            this.labelControl4.TabIndex = 27;
            this.labelControl4.Text = "Хугацаа хэтрэлт:";
            // 
            // numOverTime
            // 
            this.numOverTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numOverTime.Location = new System.Drawing.Point(202, 176);
            this.numOverTime.Name = "numOverTime";
            this.numOverTime.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.numOverTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numOverTime.Properties.Appearance.Options.UseFont = true;
            this.numOverTime.Properties.AutoHeight = false;
            this.numOverTime.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numOverTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numOverTime.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numOverTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numOverTime.Properties.ReadOnly = true;
            this.numOverTime.Size = new System.Drawing.Size(124, 29);
            this.numOverTime.TabIndex = 26;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(90, 62);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(106, 16);
            this.labelControl5.TabIndex = 28;
            this.labelControl5.Text = "Эвдрэлийн төрөл:";
            // 
            // txtRentStatus
            // 
            this.txtRentStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRentStatus.Location = new System.Drawing.Point(202, 56);
            this.txtRentStatus.Name = "txtRentStatus";
            this.txtRentStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRentStatus.Properties.Appearance.Options.UseFont = true;
            this.txtRentStatus.Properties.AutoHeight = false;
            this.txtRentStatus.Properties.ReadOnly = true;
            this.txtRentStatus.Size = new System.Drawing.Size(295, 28);
            this.txtRentStatus.TabIndex = 29;
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Location = new System.Drawing.Point(202, 90);
            this.txtNote.Name = "txtNote";
            this.txtNote.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Properties.Appearance.Options.UseFont = true;
            this.txtNote.Properties.ReadOnly = true;
            this.txtNote.Size = new System.Drawing.Size(295, 80);
            this.txtNote.TabIndex = 30;
            // 
            // frmRegFine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 330);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtRentStatus);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.numOverTime);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtFineNo);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtProdName);
            this.Controls.Add(this.labelControl7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRegFine";
            this.Text = "Торгууль төлөх";
            ((System.ComponentModel.ISupportInitialize)(this.txtProdName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFineNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOverTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRentStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPayment;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtProdName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtFineNo;
        private System.Windows.Forms.Button btnFree;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CalcEdit numOverTime;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtRentStatus;
        private DevExpress.XtraEditors.MemoEdit txtNote;
    }
}