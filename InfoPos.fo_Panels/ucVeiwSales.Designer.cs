namespace InfoPos.foo_panels
{
    partial class ucVeiwSales
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
            this.txtTotalAmount = new DevExpress.XtraEditors.CalcEdit();
            this.txtRebateAmount = new DevExpress.XtraEditors.CalcEdit();
            this.txtSalesAmount = new DevExpress.XtraEditors.CalcEdit();
            this.lblTotalAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblRebateAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblSalesAmount = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRebateAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesAmount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmount.Location = new System.Drawing.Point(107, 59);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Properties.Appearance.Options.UseFont = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(157, 26);
            this.txtTotalAmount.TabIndex = 58;
            // 
            // txtRebateAmount
            // 
            this.txtRebateAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRebateAmount.Location = new System.Drawing.Point(107, 30);
            this.txtRebateAmount.Name = "txtRebateAmount";
            this.txtRebateAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRebateAmount.Properties.Appearance.Options.UseFont = true;
            this.txtRebateAmount.Size = new System.Drawing.Size(157, 26);
            this.txtRebateAmount.TabIndex = 57;
            // 
            // txtSalesAmount
            // 
            this.txtSalesAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSalesAmount.Location = new System.Drawing.Point(107, 3);
            this.txtSalesAmount.Name = "txtSalesAmount";
            this.txtSalesAmount.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtSalesAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalesAmount.Properties.Appearance.Options.UseFont = true;
            this.txtSalesAmount.Properties.DisplayFormat.FormatString = "#,##0.00";
            this.txtSalesAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSalesAmount.Properties.EditFormat.FormatString = "#,##0.00";
            this.txtSalesAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtSalesAmount.Properties.MaxLength = 999999;
            this.txtSalesAmount.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.txtSalesAmount.Size = new System.Drawing.Size(157, 26);
            this.txtSalesAmount.TabIndex = 56;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Location = new System.Drawing.Point(6, 64);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(69, 13);
            this.lblTotalAmount.TabIndex = 55;
            this.lblTotalAmount.Text = "ТӨЛБӨР ДҮН:";
            // 
            // lblRebateAmount
            // 
            this.lblRebateAmount.Location = new System.Drawing.Point(6, 38);
            this.lblRebateAmount.Name = "lblRebateAmount";
            this.lblRebateAmount.Size = new System.Drawing.Size(91, 13);
            this.lblRebateAmount.TabIndex = 54;
            this.lblRebateAmount.Text = "ХӨНГӨЛӨЛТ ДҮН:";
            // 
            // lblSalesAmount
            // 
            this.lblSalesAmount.Location = new System.Drawing.Point(5, 11);
            this.lblSalesAmount.Name = "lblSalesAmount";
            this.lblSalesAmount.Size = new System.Drawing.Size(96, 13);
            this.lblSalesAmount.TabIndex = 53;
            this.lblSalesAmount.Text = "БОРЛУУЛАЛТ ДҮН:";
            // 
            // ucVeiwSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.txtRebateAmount);
            this.Controls.Add(this.txtSalesAmount);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblRebateAmount);
            this.Controls.Add(this.lblSalesAmount);
            this.Name = "ucVeiwSales";
            this.Size = new System.Drawing.Size(271, 92);
            this.Load += new System.EventHandler(this.ucVeiwSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRebateAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesAmount.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CalcEdit txtTotalAmount;
        private DevExpress.XtraEditors.CalcEdit txtRebateAmount;
        private DevExpress.XtraEditors.CalcEdit txtSalesAmount;
        private DevExpress.XtraEditors.LabelControl lblTotalAmount;
        private DevExpress.XtraEditors.LabelControl lblRebateAmount;
        private DevExpress.XtraEditors.LabelControl lblSalesAmount;


    }
}
