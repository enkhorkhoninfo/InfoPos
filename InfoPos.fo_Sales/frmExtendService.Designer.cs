namespace InfoPos.sales
{
    partial class frmExtendService
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblProdName = new DevExpress.XtraEditors.LabelControl();
            this.numQty = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ucNumpad1 = new InfoPos.Panels.ucNumpad();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblProdName);
            this.panelControl1.Controls.Add(this.numQty);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 43);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(269, 64);
            this.panelControl1.TabIndex = 3;
            // 
            // lblProdName
            // 
            this.lblProdName.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.lblProdName.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProdName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblProdName.Location = new System.Drawing.Point(7, 3);
            this.lblProdName.Name = "lblProdName";
            this.lblProdName.Size = new System.Drawing.Size(252, 29);
            this.lblProdName.TabIndex = 14;
            // 
            // numQty
            // 
            this.numQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numQty.Location = new System.Drawing.Point(106, 31);
            this.numQty.Name = "numQty";
            this.numQty.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.numQty.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.numQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numQty.Properties.Appearance.Options.UseBorderColor = true;
            this.numQty.Properties.Appearance.Options.UseFont = true;
            this.numQty.Properties.AutoHeight = false;
            this.numQty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.numQty.Properties.DisplayFormat.FormatString = "#,###,##0.00";
            this.numQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numQty.Properties.EditFormat.FormatString = "#,###,##0.00";
            this.numQty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numQty.Size = new System.Drawing.Size(153, 29);
            this.numQty.TabIndex = 13;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl4.Location = new System.Drawing.Point(7, 31);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(102, 29);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = " Тоо ширхэг:";
            // 
            // ucNumpad1
            // 
            this.ucNumpad1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucNumpad1.ExtraShow1 = true;
            this.ucNumpad1.ExtraShow2 = true;
            this.ucNumpad1.ExtraShow3 = true;
            this.ucNumpad1.ExtraText1 = "";
            this.ucNumpad1.ExtraText2 = "";
            this.ucNumpad1.ExtraText3 = "";
            this.ucNumpad1.Location = new System.Drawing.Point(0, 107);
            this.ucNumpad1.Name = "ucNumpad1";
            this.ucNumpad1.Size = new System.Drawing.Size(269, 265);
            this.ucNumpad1.TabIndex = 2;
            this.ucNumpad1.TabStop = false;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControl10.Location = new System.Drawing.Point(5, 7);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(258, 19);
            this.labelControl10.TabIndex = 4;
            this.labelControl10.Text = "Борлуулалт Сунгах/Засварлах";
            // 
            // frmExtendService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 372);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ucNumpad1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExtendService";
            this.Text = "Борлуулалт сунгах засварлах";
            this.Load += new System.EventHandler(this.frmExtendService_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numQty.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CalcEdit numQty;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Panels.ucNumpad ucNumpad1;
        private DevExpress.XtraEditors.LabelControl lblProdName;
        private DevExpress.XtraEditors.LabelControl labelControl10;
    }
}