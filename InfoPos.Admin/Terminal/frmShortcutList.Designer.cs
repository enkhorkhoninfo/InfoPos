namespace InfoPos.Admin
{
    partial class frmShortcutList
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
            this.grdShortcutList = new DevExpress.XtraGrid.GridControl();
            this.gvwShorcutList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdShortcutList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwShorcutList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdShortcutList);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(643, 487);
            this.panelControl1.TabIndex = 1;
            // 
            // grdShortcutList
            // 
            this.grdShortcutList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShortcutList.Location = new System.Drawing.Point(2, 2);
            this.grdShortcutList.MainView = this.gvwShorcutList;
            this.grdShortcutList.Name = "grdShortcutList";
            this.grdShortcutList.Size = new System.Drawing.Size(639, 483);
            this.grdShortcutList.TabIndex = 0;
            this.grdShortcutList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwShorcutList,
            this.gridView1});
            // 
            // gvwShorcutList
            // 
            this.gvwShorcutList.GridControl = this.grdShortcutList;
            this.gvwShorcutList.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwShorcutList.Name = "gvwShorcutList";
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdShortcutList;
            this.gridView1.Name = "gridView1";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnExit);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 487);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(643, 33);
            this.panelControl2.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(560, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 26);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Гарах";
            this.btnExit.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frmShortcutList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 520);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(659, 558);
            this.MinimumSize = new System.Drawing.Size(659, 558);
            this.Name = "frmShortcutList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shorcut-н мэдээлэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmShortcutList_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmShortcutList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdShortcutList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwShorcutList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraGrid.GridControl grdShortcutList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwShorcutList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}