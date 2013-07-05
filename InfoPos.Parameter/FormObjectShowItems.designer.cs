namespace InfoPos.Parameter
{
    partial class FormObjectShowItems
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
            this.grdObjectItems = new DevExpress.XtraGrid.GridControl();
            this.gvwObjectItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwObjectItems)).BeginInit();
            this.SuspendLayout();
            // 
            // grdObjectItems
            // 
            this.grdObjectItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdObjectItems.Location = new System.Drawing.Point(1, 3);
            this.grdObjectItems.MainView = this.gvwObjectItems;
            this.grdObjectItems.Name = "grdObjectItems";
            this.grdObjectItems.Size = new System.Drawing.Size(771, 541);
            this.grdObjectItems.TabIndex = 8;
            this.grdObjectItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwObjectItems});
            // 
            // gvwObjectItems
            // 
            this.gvwObjectItems.GridControl = this.grdObjectItems;
            this.gvwObjectItems.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.gvwObjectItems.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwObjectItems.Name = "gvwObjectItems";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnAdd.Location = new System.Drawing.Point(778, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(29, 28);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.ToolTip = "Зүйл дээр үзүүлэлт холбох";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnSave.Location = new System.Drawing.Point(778, 46);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(29, 28);
            this.btnSave.TabIndex = 10;
            this.btnSave.ToolTip = "Зүйл үзүүлэлтийн эрэмбэ хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormObjectShowItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 544);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grdObjectItems);
            this.KeyPreview = true;
            this.Name = "FormObjectShowItems";
            this.Text = "Зүйлийн холбосон үзүүлэлтүүд";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormObjectShowItems_FormClosing);
            this.Load += new System.EventHandler(this.FormObjectShowItems_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormObjectShowItems_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwObjectItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdObjectItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwObjectItems;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}