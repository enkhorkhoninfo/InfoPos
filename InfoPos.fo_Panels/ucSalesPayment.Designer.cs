namespace InfoPos.fo_panels
{
    partial class ucSalesPayment
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.numPaidAmount = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkISVAT = new DevExpress.XtraEditors.CheckEdit();
            this.btnRegisterPayment = new DevExpress.XtraEditors.SimpleButton();
            this.txtChargeAmount = new DevExpress.XtraEditors.CalcEdit();
            this.txtPayAmount = new DevExpress.XtraEditors.CalcEdit();
            this.txtTotalAmount = new DevExpress.XtraEditors.CalcEdit();
            this.txtRebateAmount = new DevExpress.XtraEditors.CalcEdit();
            this.txtSalesAmount = new DevExpress.XtraEditors.CalcEdit();
            this.lblChargeAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblPayAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblRebateAmount = new DevExpress.XtraEditors.LabelControl();
            this.btnRebateCalc = new DevExpress.XtraEditors.SimpleButton();
            this.btnOtherPayment = new DevExpress.XtraEditors.SimpleButton();
            this.btnCardPayment = new DevExpress.XtraEditors.SimpleButton();
            this.lblSalesAmount = new DevExpress.XtraEditors.LabelControl();
            this.btnCashPayment = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaidAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkISVAT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargeAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRebateAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesAmount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.groupControl1.Controls.Add(this.numPaidAmount);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.chkISVAT);
            this.groupControl1.Controls.Add(this.btnRegisterPayment);
            this.groupControl1.Controls.Add(this.txtChargeAmount);
            this.groupControl1.Controls.Add(this.txtTotalAmount);
            this.groupControl1.Controls.Add(this.txtRebateAmount);
            this.groupControl1.Controls.Add(this.txtSalesAmount);
            this.groupControl1.Controls.Add(this.lblChargeAmount);
            this.groupControl1.Controls.Add(this.lblPayAmount);
            this.groupControl1.Controls.Add(this.lblTotalAmount);
            this.groupControl1.Controls.Add(this.lblRebateAmount);
            this.groupControl1.Controls.Add(this.btnRebateCalc);
            this.groupControl1.Controls.Add(this.btnOtherPayment);
            this.groupControl1.Controls.Add(this.btnCardPayment);
            this.groupControl1.Controls.Add(this.lblSalesAmount);
            this.groupControl1.Controls.Add(this.btnCashPayment);
            this.groupControl1.Controls.Add(this.txtPayAmount);
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(424, 226);
            this.groupControl1.TabIndex = 33;
            this.groupControl1.Text = "Төлбөрийн мэдээлэл";
            // 
            // numPaidAmount
            // 
            this.numPaidAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numPaidAmount.Location = new System.Drawing.Point(267, 175);
            this.numPaidAmount.Name = "numPaidAmount";
            this.numPaidAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPaidAmount.Properties.Appearance.Options.UseFont = true;
            this.numPaidAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numPaidAmount.Properties.ReadOnly = true;
            this.numPaidAmount.Size = new System.Drawing.Size(141, 26);
            this.numPaidAmount.TabIndex = 51;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Location = new System.Drawing.Point(164, 183);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 50;
            this.labelControl1.Text = "Төлөгдсөн дүн:";
            // 
            // chkISVAT
            // 
            this.chkISVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkISVAT.EditValue = true;
            this.chkISVAT.Enabled = false;
            this.chkISVAT.Location = new System.Drawing.Point(154, 28);
            this.chkISVAT.Name = "chkISVAT";
            this.chkISVAT.Properties.Caption = "НӨАТ";
            this.chkISVAT.Size = new System.Drawing.Size(75, 19);
            this.chkISVAT.TabIndex = 49;
            this.chkISVAT.Visible = false;
            // 
            // btnRegisterPayment
            // 
            this.btnRegisterPayment.Enabled = false;
            this.btnRegisterPayment.Location = new System.Drawing.Point(7, 65);
            this.btnRegisterPayment.Name = "btnRegisterPayment";
            this.btnRegisterPayment.Size = new System.Drawing.Size(130, 34);
            this.btnRegisterPayment.TabIndex = 48;
            this.btnRegisterPayment.Text = "Төлөх";
            this.btnRegisterPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // txtChargeAmount
            // 
            this.txtChargeAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChargeAmount.Location = new System.Drawing.Point(267, 145);
            this.txtChargeAmount.Name = "txtChargeAmount";
            this.txtChargeAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChargeAmount.Properties.Appearance.Options.UseFont = true;
            this.txtChargeAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtChargeAmount.Properties.ReadOnly = true;
            this.txtChargeAmount.Size = new System.Drawing.Size(141, 26);
            this.txtChargeAmount.TabIndex = 47;
            // 
            // txtPayAmount
            // 
            this.txtPayAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPayAmount.Location = new System.Drawing.Point(267, 115);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayAmount.Properties.Appearance.Options.UseFont = true;
            this.txtPayAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPayAmount.Properties.ReadOnly = true;
            this.txtPayAmount.Size = new System.Drawing.Size(141, 26);
            this.txtPayAmount.TabIndex = 46;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmount.Location = new System.Drawing.Point(267, 85);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Properties.Appearance.Options.UseFont = true;
            this.txtTotalAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTotalAmount.Properties.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(141, 26);
            this.txtTotalAmount.TabIndex = 45;
            // 
            // txtRebateAmount
            // 
            this.txtRebateAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRebateAmount.Location = new System.Drawing.Point(267, 57);
            this.txtRebateAmount.Name = "txtRebateAmount";
            this.txtRebateAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRebateAmount.Properties.Appearance.Options.UseFont = true;
            this.txtRebateAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtRebateAmount.Properties.ReadOnly = true;
            this.txtRebateAmount.Size = new System.Drawing.Size(141, 26);
            this.txtRebateAmount.TabIndex = 43;
            // 
            // txtSalesAmount
            // 
            this.txtSalesAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSalesAmount.Location = new System.Drawing.Point(267, 29);
            this.txtSalesAmount.Name = "txtSalesAmount";
            this.txtSalesAmount.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtSalesAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesAmount.Properties.Appearance.Options.UseFont = true;
            this.txtSalesAmount.Properties.DisplayFormat.FormatString = "#,##0.00";
            this.txtSalesAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSalesAmount.Properties.EditFormat.FormatString = "#,##0.00";
            this.txtSalesAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSalesAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSalesAmount.Properties.MaxLength = 999999;
            this.txtSalesAmount.Properties.ReadOnly = true;
            this.txtSalesAmount.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.txtSalesAmount.Size = new System.Drawing.Size(141, 26);
            this.txtSalesAmount.TabIndex = 42;
            // 
            // lblChargeAmount
            // 
            this.lblChargeAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChargeAmount.Location = new System.Drawing.Point(164, 153);
            this.lblChargeAmount.Name = "lblChargeAmount";
            this.lblChargeAmount.Size = new System.Drawing.Size(45, 13);
            this.lblChargeAmount.TabIndex = 41;
            this.lblChargeAmount.Text = "Хариулт:";
            // 
            // lblPayAmount
            // 
            this.lblPayAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayAmount.Location = new System.Drawing.Point(163, 123);
            this.lblPayAmount.Name = "lblPayAmount";
            this.lblPayAmount.Size = new System.Drawing.Size(71, 13);
            this.lblPayAmount.TabIndex = 40;
            this.lblPayAmount.Text = "Төлж буй дүн:";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmount.Location = new System.Drawing.Point(164, 93);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(61, 13);
            this.lblTotalAmount.TabIndex = 39;
            this.lblTotalAmount.Text = "Төлбөр дүн:";
            // 
            // lblRebateAmount
            // 
            this.lblRebateAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRebateAmount.Location = new System.Drawing.Point(164, 65);
            this.lblRebateAmount.Name = "lblRebateAmount";
            this.lblRebateAmount.Size = new System.Drawing.Size(78, 13);
            this.lblRebateAmount.TabIndex = 37;
            this.lblRebateAmount.Text = "Хөнгөлөлт дүн:";
            // 
            // btnRebateCalc
            // 
            this.btnRebateCalc.Location = new System.Drawing.Point(8, 25);
            this.btnRebateCalc.Name = "btnRebateCalc";
            this.btnRebateCalc.Size = new System.Drawing.Size(129, 34);
            this.btnRebateCalc.TabIndex = 36;
            this.btnRebateCalc.Text = "Хөнгөлөлт тооцох";
            this.btnRebateCalc.Click += new System.EventHandler(this.btnRebateCalc_Click);
            // 
            // btnOtherPayment
            // 
            this.btnOtherPayment.Enabled = false;
            this.btnOtherPayment.Location = new System.Drawing.Point(7, 185);
            this.btnOtherPayment.Name = "btnOtherPayment";
            this.btnOtherPayment.Size = new System.Drawing.Size(130, 34);
            this.btnOtherPayment.TabIndex = 35;
            this.btnOtherPayment.Text = "Бусад төлбөр";
            this.btnOtherPayment.Click += new System.EventHandler(this.btnOtherPayment_Click);
            // 
            // btnCardPayment
            // 
            this.btnCardPayment.Enabled = false;
            this.btnCardPayment.Location = new System.Drawing.Point(7, 145);
            this.btnCardPayment.Name = "btnCardPayment";
            this.btnCardPayment.Size = new System.Drawing.Size(130, 34);
            this.btnCardPayment.TabIndex = 34;
            this.btnCardPayment.Text = "Картаар төлөх";
            this.btnCardPayment.Click += new System.EventHandler(this.btnCardPayment_Click);
            // 
            // lblSalesAmount
            // 
            this.lblSalesAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSalesAmount.Location = new System.Drawing.Point(164, 37);
            this.lblSalesAmount.Name = "lblSalesAmount";
            this.lblSalesAmount.Size = new System.Drawing.Size(97, 13);
            this.lblSalesAmount.TabIndex = 33;
            this.lblSalesAmount.Text = "Борлуулалтын дүн:";
            // 
            // btnCashPayment
            // 
            this.btnCashPayment.Enabled = false;
            this.btnCashPayment.Location = new System.Drawing.Point(7, 105);
            this.btnCashPayment.Name = "btnCashPayment";
            this.btnCashPayment.Size = new System.Drawing.Size(130, 34);
            this.btnCashPayment.TabIndex = 32;
            this.btnCashPayment.Text = "Кассаар төлөх";
            this.btnCashPayment.Click += new System.EventHandler(this.btnCashPayment_Click);
            // 
            // ucSalesPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ucSalesPayment";
            this.Size = new System.Drawing.Size(424, 226);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPaidAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkISVAT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChargeAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRebateAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesAmount.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CalcEdit numPaidAmount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkISVAT;
        private DevExpress.XtraEditors.SimpleButton btnRegisterPayment;
        private DevExpress.XtraEditors.CalcEdit txtChargeAmount;
        private DevExpress.XtraEditors.CalcEdit txtPayAmount;
        private DevExpress.XtraEditors.CalcEdit txtTotalAmount;
        private DevExpress.XtraEditors.CalcEdit txtRebateAmount;
        private DevExpress.XtraEditors.CalcEdit txtSalesAmount;
        private DevExpress.XtraEditors.LabelControl lblChargeAmount;
        private DevExpress.XtraEditors.LabelControl lblPayAmount;
        private DevExpress.XtraEditors.LabelControl lblTotalAmount;
        private DevExpress.XtraEditors.LabelControl lblRebateAmount;
        private DevExpress.XtraEditors.SimpleButton btnRebateCalc;
        private DevExpress.XtraEditors.SimpleButton btnOtherPayment;
        private DevExpress.XtraEditors.SimpleButton btnCardPayment;
        private DevExpress.XtraEditors.LabelControl lblSalesAmount;
        private DevExpress.XtraEditors.SimpleButton btnCashPayment;
    }
}
