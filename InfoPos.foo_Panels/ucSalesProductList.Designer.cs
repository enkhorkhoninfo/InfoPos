namespace InfoPos.fo_panels
{
    partial class ucSalesProductList
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
            this.grdSalesProductList = new DevExpress.XtraGrid.GridControl();
            this.grvSalesProductList = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdSalesProductList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSalesProductList)).BeginInit();
            this.SuspendLayout();
            // 
            // grdSalesProductList
            // 
            this.grdSalesProductList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSalesProductList.Location = new System.Drawing.Point(0, 0);
            this.grdSalesProductList.MainView = this.grvSalesProductList;
            this.grdSalesProductList.Name = "grdSalesProductList";
            this.grdSalesProductList.Size = new System.Drawing.Size(302, 350);
            this.grdSalesProductList.TabIndex = 4;
            this.grdSalesProductList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvSalesProductList});
            this.grdSalesProductList.Click += new System.EventHandler(this.grdSalesProductList_Click);
            // 
            // grvSalesProductList
            // 
            this.grvSalesProductList.GridControl = this.grdSalesProductList;
            this.grvSalesProductList.Name = "grvSalesProductList";
            // 
            // ucSalesProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdSalesProductList);
            this.Name = "ucSalesProductList";
            this.Size = new System.Drawing.Size(302, 350);
            ((System.ComponentModel.ISupportInitialize)(this.grdSalesProductList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvSalesProductList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdSalesProductList;
        private DevExpress.XtraGrid.Views.Grid.GridView grvSalesProductList;
    }
}
