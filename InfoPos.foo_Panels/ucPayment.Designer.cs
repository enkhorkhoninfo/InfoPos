namespace InfoPos.foo_panels
{
    partial class ucPayment
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.numRemain = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.numVat = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.numPrepaid = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.numDiff = new DevExpress.XtraEditors.CalcEdit();
            this.numPaid = new DevExpress.XtraEditors.CalcEdit();
            this.numSales = new DevExpress.XtraEditors.CalcEdit();
            this.numDiscount = new DevExpress.XtraEditors.CalcEdit();
            this.numTotal = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRemain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrepaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(530, 208);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.numRemain);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.numVat);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.numPrepaid);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.numDiff);
            this.panelControl1.Controls.Add(this.numPaid);
            this.panelControl1.Controls.Add(this.numSales);
            this.panelControl1.Controls.Add(this.numDiscount);
            this.panelControl1.Controls.Add(this.numTotal);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(4, 217);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(529, 158);
            this.panelControl1.TabIndex = 4;
            // 
            // numRemain
            // 
            this.numRemain.Location = new System.Drawing.Point(378, 45);
            this.numRemain.Name = "numRemain";
            this.numRemain.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRemain.Properties.Appearance.Options.UseFont = true;
            this.numRemain.Properties.AutoHeight = false;
            this.numRemain.Properties.ReadOnly = true;
            this.numRemain.Size = new System.Drawing.Size(124, 28);
            this.numRemain.TabIndex = 25;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl3.Location = new System.Drawing.Point(12, 116);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 16);
            this.labelControl3.TabIndex = 24;
            this.labelControl3.Text = "Төлөгдөх:";
            // 
            // numVat
            // 
            this.numVat.Location = new System.Drawing.Point(131, 77);
            this.numVat.Name = "numVat";
            this.numVat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numVat.Properties.Appearance.Options.UseFont = true;
            this.numVat.Properties.AutoHeight = false;
            this.numVat.Properties.ReadOnly = true;
            this.numVat.Size = new System.Drawing.Size(124, 28);
            this.numVat.TabIndex = 23;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl8.Location = new System.Drawing.Point(12, 84);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(38, 16);
            this.labelControl8.TabIndex = 22;
            this.labelControl8.Text = "НӨАТ:";
            // 
            // numPrepaid
            // 
            this.numPrepaid.Location = new System.Drawing.Point(378, 13);
            this.numPrepaid.Name = "numPrepaid";
            this.numPrepaid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrepaid.Properties.Appearance.Options.UseFont = true;
            this.numPrepaid.Properties.AutoHeight = false;
            this.numPrepaid.Properties.ReadOnly = true;
            this.numPrepaid.Size = new System.Drawing.Size(124, 28);
            this.numPrepaid.TabIndex = 21;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl7.Location = new System.Drawing.Point(270, 20);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(99, 16);
            this.labelControl7.TabIndex = 20;
            this.labelControl7.Text = "Өмнө төлөгдсөн:";
            // 
            // numDiff
            // 
            this.numDiff.Location = new System.Drawing.Point(378, 109);
            this.numDiff.Name = "numDiff";
            this.numDiff.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDiff.Properties.Appearance.Options.UseFont = true;
            this.numDiff.Properties.AutoHeight = false;
            this.numDiff.Properties.ReadOnly = true;
            this.numDiff.Size = new System.Drawing.Size(124, 28);
            this.numDiff.TabIndex = 19;
            // 
            // numPaid
            // 
            this.numPaid.Location = new System.Drawing.Point(378, 77);
            this.numPaid.Name = "numPaid";
            this.numPaid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPaid.Properties.Appearance.Options.UseFont = true;
            this.numPaid.Properties.AutoHeight = false;
            this.numPaid.Properties.ReadOnly = true;
            this.numPaid.Size = new System.Drawing.Size(124, 28);
            this.numPaid.TabIndex = 18;
            // 
            // numSales
            // 
            this.numSales.Location = new System.Drawing.Point(131, 13);
            this.numSales.Name = "numSales";
            this.numSales.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSales.Properties.Appearance.Options.UseFont = true;
            this.numSales.Properties.AutoHeight = false;
            this.numSales.Properties.ReadOnly = true;
            this.numSales.Size = new System.Drawing.Size(124, 28);
            this.numSales.TabIndex = 17;
            // 
            // numDiscount
            // 
            this.numDiscount.Location = new System.Drawing.Point(131, 45);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDiscount.Properties.Appearance.Options.UseFont = true;
            this.numDiscount.Properties.AutoHeight = false;
            this.numDiscount.Properties.ReadOnly = true;
            this.numDiscount.Size = new System.Drawing.Size(124, 28);
            this.numDiscount.TabIndex = 15;
            // 
            // numTotal
            // 
            this.numTotal.Location = new System.Drawing.Point(131, 109);
            this.numTotal.Name = "numTotal";
            this.numTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotal.Properties.Appearance.Options.UseFont = true;
            this.numTotal.Properties.AutoHeight = false;
            this.numTotal.Properties.ReadOnly = true;
            this.numTotal.Size = new System.Drawing.Size(124, 28);
            this.numTotal.TabIndex = 14;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl6.Location = new System.Drawing.Point(270, 116);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(91, 16);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Зөрүү/Хариулт:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl5.Location = new System.Drawing.Point(270, 84);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(85, 16);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Төлж буй дүн:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl4.Location = new System.Drawing.Point(270, 52);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(102, 16);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Үлдэгдэл төлбөр:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl2.Location = new System.Drawing.Point(12, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 16);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Хөнгөлөлт:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl1.Location = new System.Drawing.Point(12, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(113, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Борлуулалтын дүн:";
            // 
            // ucPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gridControl1);
            this.Name = "ucPayment";
            this.Size = new System.Drawing.Size(536, 378);
            this.Load += new System.EventHandler(this.ucPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRemain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrepaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotal.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CalcEdit numDiff;
        private DevExpress.XtraEditors.CalcEdit numPaid;
        private DevExpress.XtraEditors.CalcEdit numSales;
        private DevExpress.XtraEditors.CalcEdit numDiscount;
        private DevExpress.XtraEditors.CalcEdit numTotal;
        private DevExpress.XtraEditors.CalcEdit numPrepaid;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CalcEdit numVat;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.CalcEdit numRemain;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}
