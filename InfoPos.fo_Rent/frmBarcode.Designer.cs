namespace InfoPos.Rent
{
    partial class frmBarcode
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChoose = new System.Windows.Forms.Button();
            this.gridControlDetail = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.lblPage = new DevExpress.XtraEditors.LabelControl();
            this.txtInvName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(280, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 46);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Болих";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChoose
            // 
            this.btnChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChoose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChoose.Location = new System.Drawing.Point(280, 16);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(100, 46);
            this.btnChoose.TabIndex = 8;
            this.btnChoose.Text = "Оруулах";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // gridControlDetail
            // 
            this.gridControlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlDetail.Location = new System.Drawing.Point(12, 84);
            this.gridControlDetail.MainView = this.gridViewDetail;
            this.gridControlDetail.Name = "gridControlDetail";
            this.gridControlDetail.Size = new System.Drawing.Size(262, 217);
            this.gridControlDetail.TabIndex = 11;
            this.gridControlDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetail});
            // 
            // gridViewDetail
            // 
            this.gridViewDetail.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewDetail.Appearance.Row.Options.UseFont = true;
            this.gridViewDetail.GridControl = this.gridControlDetail;
            this.gridViewDetail.Name = "gridViewDetail";
            this.gridViewDetail.OptionsView.RowAutoHeight = true;
            this.gridViewDetail.OptionsView.ShowGroupPanel = false;
            this.gridViewDetail.OptionsView.ShowIndicator = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(138, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Properties.AutoHeight = false;
            this.txtSearch.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearch.Size = new System.Drawing.Size(136, 28);
            this.txtSearch.TabIndex = 10;
            // 
            // lblPage
            // 
            this.lblPage.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPage.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPage.Location = new System.Drawing.Point(12, 21);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(120, 16);
            this.lblPage.TabIndex = 15;
            this.lblPage.Text = "Хэрэгслийн баркод: ";
            // 
            // txtInvName
            // 
            this.txtInvName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInvName.Location = new System.Drawing.Point(138, 50);
            this.txtInvName.Name = "txtInvName";
            this.txtInvName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInvName.Properties.Appearance.Options.UseFont = true;
            this.txtInvName.Properties.AutoHeight = false;
            this.txtInvName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInvName.Properties.ReadOnly = true;
            this.txtInvName.Size = new System.Drawing.Size(136, 28);
            this.txtInvName.TabIndex = 17;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelControl2.Location = new System.Drawing.Point(12, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(95, 16);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "Хэрэгслийн нэр:";
            // 
            // frmBarcode
            // 
            this.AcceptButton = this.btnChoose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(392, 313);
            this.Controls.Add(this.txtInvName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.gridControlDetail);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnChoose);
            this.KeyPreview = true;
            this.Name = "frmBarcode";
            this.Text = "Баркод хайх";
            this.Load += new System.EventHandler(this.frmDeliver_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChoose;
        private DevExpress.XtraGrid.GridControl gridControlDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetail;
        public DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.LabelControl lblPage;
        public DevExpress.XtraEditors.TextEdit txtInvName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}