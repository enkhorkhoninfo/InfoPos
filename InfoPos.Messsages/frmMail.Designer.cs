namespace InfoPos.Messages
{
    partial class frmMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMail));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grdInbox = new DevExpress.XtraGrid.GridControl();
            this.gvwInbox = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imagecombo = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imagecol = new DevExpress.Utils.ImageCollection();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grdSent = new DevExpress.XtraGrid.GridControl();
            this.gvwSent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.mmoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.lblto = new DevExpress.XtraEditors.LabelControl();
            this.lblsent = new DevExpress.XtraEditors.LabelControl();
            this.lblfrom = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.btnForward = new DevExpress.XtraEditors.SimpleButton();
            this.btnReply = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwInbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagecombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagecol)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwSent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(442, 457);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.Selected += new DevExpress.XtraTab.TabPageEventHandler(this.xtraTabControl1_Selected);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdInbox);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(437, 431);
            this.xtraTabPage1.Text = "Ирсэн";
            // 
            // grdInbox
            // 
            this.grdInbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInbox.Location = new System.Drawing.Point(0, 0);
            this.grdInbox.MainView = this.gvwInbox;
            this.grdInbox.Name = "grdInbox";
            this.grdInbox.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.imagecombo});
            this.grdInbox.Size = new System.Drawing.Size(437, 431);
            this.grdInbox.TabIndex = 0;
            this.grdInbox.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwInbox});
            // 
            // gvwInbox
            // 
            this.gvwInbox.GridControl = this.grdInbox;
            this.gvwInbox.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "TOUSERNO", null, "({0} items)")});
            this.gvwInbox.Name = "gvwInbox";
            this.gvwInbox.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            this.gvwInbox.OptionsView.ShowFooter = true;
            this.gvwInbox.OptionsView.ShowGroupedColumns = true;
            this.gvwInbox.OptionsView.ShowGroupPanel = false;
            this.gvwInbox.OptionsView.ShowIndicator = false;
            this.gvwInbox.OptionsView.ShowVertLines = false;
            this.gvwInbox.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvwInbox_RowClick);
            this.gvwInbox.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvwInbox_FocusedRowChanged);
            // 
            // imagecombo
            // 
            this.imagecombo.AutoHeight = false;
            this.imagecombo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imagecombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.imagecombo.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ((short)(1)), 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ((short)(0)), 2)});
            this.imagecombo.Name = "imagecombo";
            this.imagecombo.SmallImages = this.imagecol;
            // 
            // imagecol
            // 
            this.imagecol.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imagecol.ImageStream")));
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdSent);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(437, 431);
            this.xtraTabPage2.Text = "Илгээсэн";
            // 
            // grdSent
            // 
            this.grdSent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSent.Location = new System.Drawing.Point(0, 0);
            this.grdSent.MainView = this.gvwSent;
            this.grdSent.Name = "grdSent";
            this.grdSent.Size = new System.Drawing.Size(437, 431);
            this.grdSent.TabIndex = 1;
            this.grdSent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwSent});
            // 
            // gvwSent
            // 
            this.gvwSent.GridControl = this.grdSent;
            this.gvwSent.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "POSTDATE", null, "({0} items)")});
            this.gvwSent.Name = "gvwSent";
            this.gvwSent.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            this.gvwSent.OptionsView.ShowFooter = true;
            this.gvwSent.OptionsView.ShowGroupedColumns = true;
            this.gvwSent.OptionsView.ShowGroupPanel = false;
            this.gvwSent.OptionsView.ShowIndicator = false;
            this.gvwSent.OptionsView.ShowVertLines = false;
            this.gvwSent.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvwSent_FocusedRowChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.mmoDescription);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(442, 0);
            this.panelControl1.MinimumSize = new System.Drawing.Size(189, 457);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(398, 457);
            this.panelControl1.TabIndex = 1;
            // 
            // mmoDescription
            // 
            this.mmoDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mmoDescription.Location = new System.Drawing.Point(2, 97);
            this.mmoDescription.Name = "mmoDescription";
            this.mmoDescription.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.mmoDescription.Properties.Appearance.Options.UseFont = true;
            this.mmoDescription.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.mmoDescription.Properties.ReadOnly = true;
            this.mmoDescription.Size = new System.Drawing.Size(394, 358);
            this.mmoDescription.TabIndex = 4;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnRefresh);
            this.panelControl2.Controls.Add(this.lblto);
            this.panelControl2.Controls.Add(this.lblsent);
            this.panelControl2.Controls.Add(this.lblfrom);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.btnDelete);
            this.panelControl2.Controls.Add(this.btnSend);
            this.panelControl2.Controls.Add(this.btnForward);
            this.panelControl2.Controls.Add(this.btnReply);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(394, 95);
            this.panelControl2.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRefresh.Location = new System.Drawing.Point(8, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(29, 24);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.ToolTip = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblto
            // 
            this.lblto.Location = new System.Drawing.Point(42, 73);
            this.lblto.Name = "lblto";
            this.lblto.Size = new System.Drawing.Size(12, 13);
            this.lblto.TabIndex = 16;
            this.lblto.Text = "To";
            // 
            // lblsent
            // 
            this.lblsent.Location = new System.Drawing.Point(42, 54);
            this.lblsent.Name = "lblsent";
            this.lblsent.Size = new System.Drawing.Size(22, 13);
            this.lblsent.TabIndex = 15;
            this.lblsent.Text = "Sent";
            // 
            // lblfrom
            // 
            this.lblfrom.Location = new System.Drawing.Point(42, 35);
            this.lblfrom.Name = "lblfrom";
            this.lblfrom.Size = new System.Drawing.Size(24, 13);
            this.lblfrom.TabIndex = 14;
            this.lblfrom.Text = "From";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 73);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(16, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "To:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(26, 13);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "Sent:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "From:";
            // 
            // btnDelete
            // 
            this.btnDelete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDelete.Location = new System.Drawing.Point(148, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(29, 24);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.ToolTip = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSend
            // 
            this.btnSend.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSend.Location = new System.Drawing.Point(43, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(29, 24);
            this.btnSend.TabIndex = 5;
            this.btnSend.ToolTip = "New E-Mail";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnForward
            // 
            this.btnForward.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnForward.Location = new System.Drawing.Point(113, 5);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(29, 24);
            this.btnForward.TabIndex = 4;
            this.btnForward.ToolTip = "Forward";
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnReply
            // 
            this.btnReply.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnReply.Location = new System.Drawing.Point(78, 5);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(29, 24);
            this.btnReply.TabIndex = 3;
            this.btnReply.ToolTip = "Reply";
            this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl1.Location = new System.Drawing.Point(436, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 457);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // frmMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 457);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.Name = "frmMail";
            this.Text = "Захиа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMail_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMail_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdInbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwInbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagecombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagecol)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwSent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mmoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.GridControl grdInbox;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwInbox;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grdSent;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwSent;
        private DevExpress.XtraEditors.MemoEdit mmoDescription;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.SimpleButton btnForward;
        private DevExpress.XtraEditors.SimpleButton btnReply;
        private DevExpress.XtraEditors.LabelControl lblto;
        private DevExpress.XtraEditors.LabelControl lblsent;
        private DevExpress.XtraEditors.LabelControl lblfrom;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imagecombo;
        private DevExpress.Utils.ImageCollection imagecol;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
    }
}