namespace InfoPos.Panels
{
    partial class ucCalcSales
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
            this.chkISVAT = new DevExpress.XtraEditors.CheckEdit();
            this.btnPayment = new DevExpress.XtraEditors.SimpleButton();
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
            this.groupControl1.AppearanceCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.groupControl1.Controls.Add(this.chkISVAT);
            this.groupControl1.Controls.Add(this.btnPayment);
            this.groupControl1.Controls.Add(this.txtChargeAmount);
            this.groupControl1.Controls.Add(this.txtPayAmount);
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
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(414, 211);
            this.groupControl1.TabIndex = 32;
            this.groupControl1.Text = "Төлбөрийн дүн";
            // 
            // chkISVAT
            // 
            this.chkISVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkISVAT.EditValue = true;
            this.chkISVAT.Enabled = false;
            this.chkISVAT.Location = new System.Drawing.Point(145, 42);
            this.chkISVAT.Name = "chkISVAT";
            this.chkISVAT.Properties.Caption = "НӨАТ";
            this.chkISVAT.Size = new System.Drawing.Size(75, 19);
            this.chkISVAT.TabIndex = 49;
            this.chkISVAT.Visible = false;
            // 
            // btnPayment
            // 
            this.btnPayment.Enabled = false;
            this.btnPayment.Location = new System.Drawing.Point(5, 34);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(130, 34);
            this.btnPayment.TabIndex = 48;
            this.btnPayment.Text = "ТӨЛӨХ";
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // txtChargeAmount
            // 
            this.txtChargeAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChargeAmount.Location = new System.Drawing.Point(250, 180);
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
            this.txtPayAmount.Location = new System.Drawing.Point(250, 150);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayAmount.Properties.Appearance.Options.UseFont = true;
            this.txtPayAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPayAmount.Size = new System.Drawing.Size(141, 26);
            this.txtPayAmount.TabIndex = 46;
            this.txtPayAmount.EditValueChanged += new System.EventHandler(this.txtPayAmount_EditValueChanged);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmount.Location = new System.Drawing.Point(250, 120);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Properties.Appearance.Options.UseFont = true;
            this.txtTotalAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTotalAmount.Properties.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(141, 26);
            this.txtTotalAmount.TabIndex = 45;
            this.txtTotalAmount.EditValueChanged += new System.EventHandler(this.txtTotalAmount_EditValueChanged);
            // 
            // txtRebateAmount
            // 
            this.txtRebateAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRebateAmount.Location = new System.Drawing.Point(250, 92);
            this.txtRebateAmount.Name = "txtRebateAmount";
            this.txtRebateAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRebateAmount.Properties.Appearance.Options.UseFont = true;
            this.txtRebateAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtRebateAmount.Properties.ReadOnly = true;
            this.txtRebateAmount.Size = new System.Drawing.Size(141, 26);
            this.txtRebateAmount.TabIndex = 43;
            this.txtRebateAmount.EditValueChanged += new System.EventHandler(this.txtRebateAmount_EditValueChanged);
            // 
            // txtSalesAmount
            // 
            this.txtSalesAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSalesAmount.Location = new System.Drawing.Point(250, 64);
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
            this.lblChargeAmount.Location = new System.Drawing.Point(147, 188);
            this.lblChargeAmount.Name = "lblChargeAmount";
            this.lblChargeAmount.Size = new System.Drawing.Size(50, 13);
            this.lblChargeAmount.TabIndex = 41;
            this.lblChargeAmount.Text = "ХАРИУЛТ:";
            // 
            // lblPayAmount
            // 
            this.lblPayAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayAmount.Location = new System.Drawing.Point(147, 158);
            this.lblPayAmount.Name = "lblPayAmount";
            this.lblPayAmount.Size = new System.Drawing.Size(71, 13);
            this.lblPayAmount.TabIndex = 40;
            this.lblPayAmount.Text = "ТӨЛСӨН ДҮН:";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmount.Location = new System.Drawing.Point(147, 128);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(69, 13);
            this.lblTotalAmount.TabIndex = 39;
            this.lblTotalAmount.Text = "ТӨЛБӨР ДҮН:";
            // 
            // lblRebateAmount
            // 
            this.lblRebateAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRebateAmount.Location = new System.Drawing.Point(147, 100);
            this.lblRebateAmount.Name = "lblRebateAmount";
            this.lblRebateAmount.Size = new System.Drawing.Size(91, 13);
            this.lblRebateAmount.TabIndex = 37;
            this.lblRebateAmount.Text = "ХӨНГӨЛӨЛТ ДҮН:";
            // 
            // btnRebateCalc
            // 
            this.btnRebateCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRebateCalc.Location = new System.Drawing.Point(250, 22);
            this.btnRebateCalc.Name = "btnRebateCalc";
            this.btnRebateCalc.Size = new System.Drawing.Size(141, 39);
            this.btnRebateCalc.TabIndex = 36;
            this.btnRebateCalc.Text = "ХӨНГӨЛӨЛТ ТООЦООЛОХ";
            this.btnRebateCalc.Click += new System.EventHandler(this.btnRebateCalc_Click);
            // 
            // btnOtherPayment
            // 
            this.btnOtherPayment.Enabled = false;
            this.btnOtherPayment.Location = new System.Drawing.Point(5, 154);
            this.btnOtherPayment.Name = "btnOtherPayment";
            this.btnOtherPayment.Size = new System.Drawing.Size(130, 34);
            this.btnOtherPayment.TabIndex = 35;
            this.btnOtherPayment.Text = "БУСАД ТӨЛБӨР";
            this.btnOtherPayment.Click += new System.EventHandler(this.btnOtherPayment_Click);
            // 
            // btnCardPayment
            // 
            this.btnCardPayment.Enabled = false;
            this.btnCardPayment.Location = new System.Drawing.Point(5, 114);
            this.btnCardPayment.Name = "btnCardPayment";
            this.btnCardPayment.Size = new System.Drawing.Size(130, 34);
            this.btnCardPayment.TabIndex = 34;
            this.btnCardPayment.Text = "КАРТААР ТӨЛӨХ";
            this.btnCardPayment.Click += new System.EventHandler(this.btnCardPayment_Click);
            // 
            // lblSalesAmount
            // 
            this.lblSalesAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSalesAmount.Location = new System.Drawing.Point(147, 72);
            this.lblSalesAmount.Name = "lblSalesAmount";
            this.lblSalesAmount.Size = new System.Drawing.Size(96, 13);
            this.lblSalesAmount.TabIndex = 33;
            this.lblSalesAmount.Text = "БОРЛУУЛАЛТ ДҮН:";
            // 
            // btnCashPayment
            // 
            this.btnCashPayment.Enabled = false;
            this.btnCashPayment.Location = new System.Drawing.Point(5, 74);
            this.btnCashPayment.Name = "btnCashPayment";
            this.btnCashPayment.Size = new System.Drawing.Size(130, 34);
            this.btnCashPayment.TabIndex = 32;
            this.btnCashPayment.Text = "КАССААР ТӨЛӨХ";
            this.btnCashPayment.Click += new System.EventHandler(this.btnCashPayment_Click);
            // 
            // ucCalcSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ucCalcSales";
            this.Size = new System.Drawing.Size(414, 211);
            this.Load += new System.EventHandler(this.ucCalcSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
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
        private DevExpress.XtraEditors.CalcEdit txtChargeAmount;
        private DevExpress.XtraEditors.CalcEdit txtPayAmount;
        private DevExpress.XtraEditors.CalcEdit txtTotalAmount;
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
        private DevExpress.XtraEditors.CalcEdit txtRebateAmount;
        private DevExpress.XtraEditors.SimpleButton btnPayment;
        private DevExpress.XtraEditors.CheckEdit chkISVAT;

    }
}
