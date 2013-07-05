namespace InfoPos.cash
{
    partial class frmOtherPayment
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnTxn = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
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
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 22);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(687, 269);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // btnTxn
            // 
            this.btnTxn.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnTxn.Appearance.Options.UseFont = true;
            this.btnTxn.Location = new System.Drawing.Point(466, 465);
            this.btnTxn.Name = "btnTxn";
            this.btnTxn.Size = new System.Drawing.Size(107, 32);
            this.btnTxn.TabIndex = 1;
            this.btnTxn.Text = "Төлөх";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(579, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Буцах";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(691, 293);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Борлуулалтууд";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.numRemain);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.numVat);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.numPrepaid);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.numDiff);
            this.groupControl2.Controls.Add(this.numPaid);
            this.groupControl2.Controls.Add(this.numSales);
            this.groupControl2.Controls.Add(this.numDiscount);
            this.groupControl2.Controls.Add(this.numTotal);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 293);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(691, 166);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "Дүнгийн мэдээлэл";
            // 
            // numRemain
            // 
            this.numRemain.Location = new System.Drawing.Point(378, 59);
            this.numRemain.Name = "numRemain";
            this.numRemain.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRemain.Properties.Appearance.Options.UseFont = true;
            this.numRemain.Properties.AutoHeight = false;
            this.numRemain.Properties.ReadOnly = true;
            this.numRemain.Size = new System.Drawing.Size(124, 28);
            this.numRemain.TabIndex = 41;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl3.Location = new System.Drawing.Point(12, 130);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 16);
            this.labelControl3.TabIndex = 40;
            this.labelControl3.Text = "Төлөгдөх:";
            // 
            // numVat
            // 
            this.numVat.Location = new System.Drawing.Point(131, 91);
            this.numVat.Name = "numVat";
            this.numVat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numVat.Properties.Appearance.Options.UseFont = true;
            this.numVat.Properties.AutoHeight = false;
            this.numVat.Properties.ReadOnly = true;
            this.numVat.Size = new System.Drawing.Size(124, 28);
            this.numVat.TabIndex = 39;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl8.Location = new System.Drawing.Point(12, 98);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(38, 16);
            this.labelControl8.TabIndex = 38;
            this.labelControl8.Text = "НӨАТ:";
            // 
            // numPrepaid
            // 
            this.numPrepaid.Location = new System.Drawing.Point(378, 27);
            this.numPrepaid.Name = "numPrepaid";
            this.numPrepaid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPrepaid.Properties.Appearance.Options.UseFont = true;
            this.numPrepaid.Properties.AutoHeight = false;
            this.numPrepaid.Properties.ReadOnly = true;
            this.numPrepaid.Size = new System.Drawing.Size(124, 28);
            this.numPrepaid.TabIndex = 37;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl7.Location = new System.Drawing.Point(270, 34);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(99, 16);
            this.labelControl7.TabIndex = 36;
            this.labelControl7.Text = "Өмнө төлөгдсөн:";
            // 
            // numDiff
            // 
            this.numDiff.Location = new System.Drawing.Point(378, 123);
            this.numDiff.Name = "numDiff";
            this.numDiff.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDiff.Properties.Appearance.Options.UseFont = true;
            this.numDiff.Properties.AutoHeight = false;
            this.numDiff.Properties.ReadOnly = true;
            this.numDiff.Size = new System.Drawing.Size(124, 28);
            this.numDiff.TabIndex = 35;
            // 
            // numPaid
            // 
            this.numPaid.Location = new System.Drawing.Point(378, 91);
            this.numPaid.Name = "numPaid";
            this.numPaid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPaid.Properties.Appearance.Options.UseFont = true;
            this.numPaid.Properties.AutoHeight = false;
            this.numPaid.Properties.ReadOnly = true;
            this.numPaid.Size = new System.Drawing.Size(124, 28);
            this.numPaid.TabIndex = 34;
            // 
            // numSales
            // 
            this.numSales.Location = new System.Drawing.Point(131, 27);
            this.numSales.Name = "numSales";
            this.numSales.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSales.Properties.Appearance.Options.UseFont = true;
            this.numSales.Properties.AutoHeight = false;
            this.numSales.Properties.ReadOnly = true;
            this.numSales.Size = new System.Drawing.Size(124, 28);
            this.numSales.TabIndex = 33;
            // 
            // numDiscount
            // 
            this.numDiscount.Location = new System.Drawing.Point(131, 59);
            this.numDiscount.Name = "numDiscount";
            this.numDiscount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDiscount.Properties.Appearance.Options.UseFont = true;
            this.numDiscount.Properties.AutoHeight = false;
            this.numDiscount.Properties.ReadOnly = true;
            this.numDiscount.Size = new System.Drawing.Size(124, 28);
            this.numDiscount.TabIndex = 32;
            // 
            // numTotal
            // 
            this.numTotal.Location = new System.Drawing.Point(131, 123);
            this.numTotal.Name = "numTotal";
            this.numTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotal.Properties.Appearance.Options.UseFont = true;
            this.numTotal.Properties.AutoHeight = false;
            this.numTotal.Properties.ReadOnly = true;
            this.numTotal.Size = new System.Drawing.Size(124, 28);
            this.numTotal.TabIndex = 31;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl6.Location = new System.Drawing.Point(270, 130);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(91, 16);
            this.labelControl6.TabIndex = 30;
            this.labelControl6.Text = "Зөрүү/Хариулт:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl5.Location = new System.Drawing.Point(270, 98);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(85, 16);
            this.labelControl5.TabIndex = 29;
            this.labelControl5.Text = "Төлж буй дүн:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl4.Location = new System.Drawing.Point(270, 66);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(102, 16);
            this.labelControl4.TabIndex = 28;
            this.labelControl4.Text = "Үлдэгдэл төлбөр:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl2.Location = new System.Drawing.Point(12, 66);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 16);
            this.labelControl2.TabIndex = 27;
            this.labelControl2.Text = "Хөнгөлөлт:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl1.Location = new System.Drawing.Point(12, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(113, 16);
            this.labelControl1.TabIndex = 26;
            this.labelControl1.Text = "Борлуулалтын дүн:";
            // 
            // frmOtherPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 502);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTxn);
            this.Name = "frmOtherPayment";
            this.Text = "Бусад төлөлт";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
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
        private DevExpress.XtraEditors.SimpleButton btnTxn;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CalcEdit numRemain;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CalcEdit numVat;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.CalcEdit numPrepaid;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.CalcEdit numDiff;
        private DevExpress.XtraEditors.CalcEdit numPaid;
        private DevExpress.XtraEditors.CalcEdit numSales;
        private DevExpress.XtraEditors.CalcEdit numDiscount;
        private DevExpress.XtraEditors.CalcEdit numTotal;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}