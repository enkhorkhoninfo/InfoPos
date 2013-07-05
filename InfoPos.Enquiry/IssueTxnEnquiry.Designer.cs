namespace InfoPos.Enquiry
{
    partial class IssueTxnEnquiry
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
            this.grdIssueTxn = new DevExpress.XtraGrid.GridControl();
            this.gvwIssueTxn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnclose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdIssueTxn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwIssueTxn)).BeginInit();
            this.SuspendLayout();
            // 
            // grdIssueTxn
            // 
            this.grdIssueTxn.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdIssueTxn.Location = new System.Drawing.Point(0, 0);
            this.grdIssueTxn.MainView = this.gvwIssueTxn;
            this.grdIssueTxn.Name = "grdIssueTxn";
            this.grdIssueTxn.Size = new System.Drawing.Size(334, 534);
            this.grdIssueTxn.TabIndex = 3;
            this.grdIssueTxn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwIssueTxn});
            // 
            // gvwIssueTxn
            // 
            this.gvwIssueTxn.GridControl = this.grdIssueTxn;
            this.gvwIssueTxn.Name = "gvwIssueTxn";
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.Location = new System.Drawing.Point(245, 537);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(84, 23);
            this.btnclose.TabIndex = 4;
            this.btnclose.Text = "Хаах";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click_1);
            // 
            // IssueTxnEnquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 562);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.grdIssueTxn);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(350, 600);
            this.Name = "IssueTxnEnquiry";
            this.Text = "Асуудлын гүйлгээ лавалгаа";
            this.Load += new System.EventHandler(this.IssueTxnEnquiry_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IssueTxnEnquiry_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdIssueTxn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwIssueTxn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdIssueTxn;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwIssueTxn;
        private DevExpress.XtraEditors.SimpleButton btnclose;
    }
}