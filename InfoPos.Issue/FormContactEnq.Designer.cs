namespace InfoPos.Issue
{
    partial class FormContactEnq
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
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PiePointOptions piePointOptions1 = new DevExpress.XtraCharts.PiePointOptions();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel2 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            this.IssueImageList = new System.Windows.Forms.ImageList();
            this.ErrorChecker = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            this.xtraTabPageIssue = new DevExpress.XtraTab.XtraTabPage();
            this.panelTxnBtn = new DevExpress.XtraEditors.PanelControl();
            this.btnDeleteIssue = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditIssue = new DevExpress.XtraEditors.SimpleButton();
            this.btnLink = new DevExpress.XtraEditors.SimpleButton();
            this.btnAssignee = new DevExpress.XtraEditors.SimpleButton();
            this.btnComment = new DevExpress.XtraEditors.SimpleButton();
            this.groupDataIssue = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControlData = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarIssue = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainerTxn = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.grdSourceIssue = new DevExpress.XtraGrid.GridControl();
            this.gvwSourceIssue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SOURCEISSUEID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SOURCESUBJECT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SOURCEASSIGNEEUSER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.navBarGroupControlContainerIssue = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.groupDataAll = new DevExpress.XtraEditors.GroupControl();
            this.groupData = new DevExpress.XtraEditors.PanelControl();
            this.lblAssigneeUserName = new DevExpress.XtraEditors.LabelControl();
            this.lblASSIGNEEUSER = new DevExpress.XtraEditors.LabelControl();
            this.lblETrackID = new DevExpress.XtraEditors.LabelControl();
            this.lblTrackID = new DevExpress.XtraEditors.LabelControl();
            this.lblVote = new DevExpress.XtraEditors.LabelControl();
            this.btnAddVote = new DevExpress.XtraEditors.SimpleButton();
            this.lblEStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnProgress = new DevExpress.XtraEditors.SimpleButton();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.mmeDescription = new DevExpress.XtraEditors.MemoEdit();
            this.lblECreateUserName = new DevExpress.XtraEditors.LabelControl();
            this.lblCreateUserName = new DevExpress.XtraEditors.LabelControl();
            this.lblSubject = new DevExpress.XtraEditors.LabelControl();
            this.lblECreateDate = new DevExpress.XtraEditors.LabelControl();
            this.lblCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.navBarGroupControlContainerAttach = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.grdAttach = new DevExpress.XtraGrid.GridControl();
            this.gvwAttach = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.FILENAME = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_FILENAME = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.FILETYPE = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.PictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.layoutViewField_FILETYPE = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.ATTACHCREATEDATE = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_ATTACHCREATEDATE = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.ATTACHDESCRIPTION = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.layoutViewField_ATTACHDESCRIPTION = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.ImageEdit = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.ImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.splitContainerControlChartAndTxn = new DevExpress.XtraEditors.SplitContainerControl();
            this.ChartIssue = new DevExpress.XtraCharts.ChartControl();
            this.grdTxn = new DevExpress.XtraGrid.GridControl();
            this.gvwTxn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.USERNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TXNSUBJECT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.STATUSNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ACTIONTYPENAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TXNDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TXNTRACKNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.JRNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TXNDATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CONTACTTYPENAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupCustomerIssue = new DevExpress.XtraEditors.GroupControl();
            this.btnIssueECustomer = new DevExpress.XtraEditors.SimpleButton();
            this.txtIssueCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.lblIssueCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.txtIssueCustomerNo = new DevExpress.XtraEditors.TextEdit();
            this.lblIssueCustomerNo = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPageNote = new DevExpress.XtraTab.XtraTabPage();
            this.groupDataNote = new DevExpress.XtraEditors.PanelControl();
            this.ucToggleCustomer = new ISM.Template.ucTogglePanel();
            this.groupCustomerData = new DevExpress.XtraEditors.GroupControl();
            this.lblNote = new DevExpress.XtraEditors.LabelControl();
            this.txtBriefDesc = new DevExpress.XtraEditors.TextEdit();
            this.lblBriefDesc = new DevExpress.XtraEditors.LabelControl();
            this.lblContactType = new DevExpress.XtraEditors.LabelControl();
            this.lblContactDate = new DevExpress.XtraEditors.LabelControl();
            this.txtSeqNo = new DevExpress.XtraEditors.TextEdit();
            this.lblSeqNo = new DevExpress.XtraEditors.LabelControl();
            this.mmeNote = new DevExpress.XtraEditors.MemoEdit();
            this.dteContactDate = new DevExpress.XtraEditors.DateEdit();
            this.cboContactType = new DevExpress.XtraEditors.LookUpEdit();
            this.grdCustomer = new DevExpress.XtraGrid.GridControl();
            this.gvwCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupCustomerNote = new DevExpress.XtraEditors.GroupControl();
            this.btnECustomer = new DevExpress.XtraEditors.SimpleButton();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerNo = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomerNo = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabIssue = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).BeginInit();
            this.xtraTabPageIssue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTxnBtn)).BeginInit();
            this.panelTxnBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupDataIssue)).BeginInit();
            this.groupDataIssue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlData)).BeginInit();
            this.splitContainerControlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarIssue)).BeginInit();
            this.navBarIssue.SuspendLayout();
            this.navBarGroupControlContainerTxn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSourceIssue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwSourceIssue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.navBarGroupControlContainerIssue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupDataAll)).BeginInit();
            this.groupDataAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupData)).BeginInit();
            this.groupData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmeDescription.Properties)).BeginInit();
            this.navBarGroupControlContainerAttach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwAttach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_FILENAME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_FILETYPE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_ATTACHCREATEDATE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_ATTACHDESCRIPTION)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlChartAndTxn)).BeginInit();
            this.splitContainerControlChartAndTxn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartIssue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTxn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwTxn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomerIssue)).BeginInit();
            this.groupCustomerIssue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueCustomerNo.Properties)).BeginInit();
            this.xtraTabPageNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupDataNote)).BeginInit();
            this.groupDataNote.SuspendLayout();
            this.ucToggleCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomerData)).BeginInit();
            this.groupCustomerData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBriefDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeqNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteContactDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteContactDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboContactType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomerNote)).BeginInit();
            this.groupCustomerNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabIssue)).BeginInit();
            this.xtraTabIssue.SuspendLayout();
            this.SuspendLayout();
            // 
            // IssueImageList
            // 
            this.IssueImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.IssueImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.IssueImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ErrorChecker
            // 
            this.ErrorChecker.ContainerControl = this;
            // 
            // xtraTabPageIssue
            // 
            this.xtraTabPageIssue.Controls.Add(this.panelTxnBtn);
            this.xtraTabPageIssue.Controls.Add(this.groupDataIssue);
            this.xtraTabPageIssue.Controls.Add(this.groupCustomerIssue);
            this.xtraTabPageIssue.Name = "xtraTabPageIssue";
            this.xtraTabPageIssue.Size = new System.Drawing.Size(946, 600);
            this.xtraTabPageIssue.Text = "Issue";
            // 
            // panelTxnBtn
            // 
            this.panelTxnBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTxnBtn.Controls.Add(this.btnDeleteIssue);
            this.panelTxnBtn.Controls.Add(this.btnEditIssue);
            this.panelTxnBtn.Controls.Add(this.btnLink);
            this.panelTxnBtn.Controls.Add(this.btnAssignee);
            this.panelTxnBtn.Controls.Add(this.btnComment);
            this.panelTxnBtn.Location = new System.Drawing.Point(4, 560);
            this.panelTxnBtn.Name = "panelTxnBtn";
            this.panelTxnBtn.Size = new System.Drawing.Size(938, 37);
            this.panelTxnBtn.TabIndex = 3;
            // 
            // btnDeleteIssue
            // 
            this.btnDeleteIssue.Location = new System.Drawing.Point(144, 8);
            this.btnDeleteIssue.Name = "btnDeleteIssue";
            this.btnDeleteIssue.Size = new System.Drawing.Size(129, 23);
            this.btnDeleteIssue.TabIndex = 5;
            this.btnDeleteIssue.Text = "Асуудал устгах";
            this.btnDeleteIssue.Click += new System.EventHandler(this.btnDeleteIssue_Click);
            // 
            // btnEditIssue
            // 
            this.btnEditIssue.Location = new System.Drawing.Point(9, 8);
            this.btnEditIssue.Name = "btnEditIssue";
            this.btnEditIssue.Size = new System.Drawing.Size(129, 23);
            this.btnEditIssue.TabIndex = 4;
            this.btnEditIssue.Text = "Асуудал засах";
            this.btnEditIssue.Click += new System.EventHandler(this.btnEditIssue_Click);
            // 
            // btnLink
            // 
            this.btnLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLink.Location = new System.Drawing.Point(603, 8);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(105, 23);
            this.btnLink.TabIndex = 2;
            this.btnLink.Text = "Link";
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // btnAssignee
            // 
            this.btnAssignee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAssignee.Location = new System.Drawing.Point(714, 8);
            this.btnAssignee.Name = "btnAssignee";
            this.btnAssignee.Size = new System.Drawing.Size(105, 23);
            this.btnAssignee.TabIndex = 1;
            this.btnAssignee.Text = "Шилжүүлэх";
            this.btnAssignee.Click += new System.EventHandler(this.btnAssignee_Click);
            // 
            // btnComment
            // 
            this.btnComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComment.Location = new System.Drawing.Point(825, 8);
            this.btnComment.Name = "btnComment";
            this.btnComment.Size = new System.Drawing.Size(105, 23);
            this.btnComment.TabIndex = 0;
            this.btnComment.Text = "Comment";
            this.btnComment.Click += new System.EventHandler(this.btnComment_Click);
            // 
            // groupDataIssue
            // 
            this.groupDataIssue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDataIssue.Controls.Add(this.splitContainerControlData);
            this.groupDataIssue.Location = new System.Drawing.Point(4, 66);
            this.groupDataIssue.Name = "groupDataIssue";
            this.groupDataIssue.Size = new System.Drawing.Size(938, 492);
            this.groupDataIssue.TabIndex = 2;
            this.groupDataIssue.Text = "Issue";
            // 
            // splitContainerControlData
            // 
            this.splitContainerControlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControlData.Location = new System.Drawing.Point(2, 22);
            this.splitContainerControlData.Name = "splitContainerControlData";
            this.splitContainerControlData.Panel1.Controls.Add(this.navBarIssue);
            this.splitContainerControlData.Panel1.Text = "Panel1";
            this.splitContainerControlData.Panel2.Controls.Add(this.splitContainerControlChartAndTxn);
            this.splitContainerControlData.Panel2.Text = "Panel2";
            this.splitContainerControlData.Size = new System.Drawing.Size(934, 468);
            this.splitContainerControlData.SplitterPosition = 476;
            this.splitContainerControlData.TabIndex = 5;
            this.splitContainerControlData.Text = "splitContainerControl1";
            // 
            // navBarIssue
            // 
            this.navBarIssue.ActiveGroup = this.navBarGroup3;
            this.navBarIssue.Appearance.GroupHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.navBarIssue.Appearance.GroupHeader.Options.UseFont = true;
            this.navBarIssue.Controls.Add(this.navBarGroupControlContainerIssue);
            this.navBarIssue.Controls.Add(this.navBarGroupControlContainerAttach);
            this.navBarIssue.Controls.Add(this.navBarGroupControlContainerTxn);
            this.navBarIssue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarIssue.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2,
            this.navBarGroup3});
            this.navBarIssue.Location = new System.Drawing.Point(0, 0);
            this.navBarIssue.Name = "navBarIssue";
            this.navBarIssue.OptionsNavPane.ExpandedWidth = 236;
            this.navBarIssue.Size = new System.Drawing.Size(476, 468);
            this.navBarIssue.StoreDefaultPaintStyleName = true;
            this.navBarIssue.TabIndex = 4;
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "Асуудлын холбоос";
            this.navBarGroup3.ControlContainer = this.navBarGroupControlContainerTxn;
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.GroupClientHeight = 213;
            this.navBarGroup3.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // navBarGroupControlContainerTxn
            // 
            this.navBarGroupControlContainerTxn.Controls.Add(this.grdSourceIssue);
            this.navBarGroupControlContainerTxn.Name = "navBarGroupControlContainerTxn";
            this.navBarGroupControlContainerTxn.Size = new System.Drawing.Size(474, 212);
            this.navBarGroupControlContainerTxn.TabIndex = 3;
            // 
            // grdSourceIssue
            // 
            this.grdSourceIssue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSourceIssue.Location = new System.Drawing.Point(0, 0);
            this.grdSourceIssue.MainView = this.gvwSourceIssue;
            this.grdSourceIssue.Name = "grdSourceIssue";
            this.grdSourceIssue.Size = new System.Drawing.Size(474, 212);
            this.grdSourceIssue.TabIndex = 4;
            this.grdSourceIssue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwSourceIssue,
            this.gridView1});
            // 
            // gvwSourceIssue
            // 
            this.gvwSourceIssue.Appearance.OddRow.BackColor = System.Drawing.Color.Wheat;
            this.gvwSourceIssue.Appearance.OddRow.Options.UseBackColor = true;
            this.gvwSourceIssue.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.SOURCEISSUEID,
            this.SOURCESUBJECT,
            this.SOURCEASSIGNEEUSER});
            this.gvwSourceIssue.GridControl = this.grdSourceIssue;
            this.gvwSourceIssue.Images = this.IssueImageList;
            this.gvwSourceIssue.Name = "gvwSourceIssue";
            this.gvwSourceIssue.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateFocusedItem;
            this.gvwSourceIssue.OptionsView.ShowGroupPanel = false;
            this.gvwSourceIssue.OptionsView.ShowHorzLines = false;
            // 
            // SOURCEISSUEID
            // 
            this.SOURCEISSUEID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.SOURCEISSUEID.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(163)))));
            this.SOURCEISSUEID.AppearanceCell.Options.UseFont = true;
            this.SOURCEISSUEID.AppearanceCell.Options.UseForeColor = true;
            this.SOURCEISSUEID.Caption = "Эх асуудлын дугаар";
            this.SOURCEISSUEID.FieldName = "SOURCEISSUEID";
            this.SOURCEISSUEID.MaxWidth = 200;
            this.SOURCEISSUEID.Name = "SOURCEISSUEID";
            this.SOURCEISSUEID.OptionsColumn.AllowEdit = false;
            this.SOURCEISSUEID.Visible = true;
            this.SOURCEISSUEID.VisibleIndex = 0;
            // 
            // SOURCESUBJECT
            // 
            this.SOURCESUBJECT.Caption = "Товч утга";
            this.SOURCESUBJECT.FieldName = "SUBJECT";
            this.SOURCESUBJECT.Name = "SOURCESUBJECT";
            this.SOURCESUBJECT.OptionsColumn.AllowEdit = false;
            this.SOURCESUBJECT.Visible = true;
            this.SOURCESUBJECT.VisibleIndex = 1;
            // 
            // SOURCEASSIGNEEUSER
            // 
            this.SOURCEASSIGNEEUSER.Caption = "Яг одоо хариуцаж байгаа хэрэглэгч";
            this.SOURCEASSIGNEEUSER.FieldName = "ASSIGNEEUSERNAME";
            this.SOURCEASSIGNEEUSER.ImageIndex = 0;
            this.SOURCEASSIGNEEUSER.Name = "SOURCEASSIGNEEUSER";
            this.SOURCEASSIGNEEUSER.OptionsColumn.AllowEdit = false;
            this.SOURCEASSIGNEEUSER.Visible = true;
            this.SOURCEASSIGNEEUSER.VisibleIndex = 2;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdSourceIssue;
            this.gridView1.Name = "gridView1";
            // 
            // navBarGroupControlContainerIssue
            // 
            this.navBarGroupControlContainerIssue.Controls.Add(this.groupDataAll);
            this.navBarGroupControlContainerIssue.Name = "navBarGroupControlContainerIssue";
            this.navBarGroupControlContainerIssue.Size = new System.Drawing.Size(474, 299);
            this.navBarGroupControlContainerIssue.TabIndex = 0;
            // 
            // groupDataAll
            // 
            this.groupDataAll.Controls.Add(this.groupData);
            this.groupDataAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDataAll.Location = new System.Drawing.Point(0, 0);
            this.groupDataAll.Name = "groupDataAll";
            this.groupDataAll.Size = new System.Drawing.Size(474, 299);
            this.groupDataAll.TabIndex = 2;
            // 
            // groupData
            // 
            this.groupData.Appearance.BackColor = System.Drawing.Color.White;
            this.groupData.Appearance.Options.UseBackColor = true;
            this.groupData.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupData.Controls.Add(this.lblAssigneeUserName);
            this.groupData.Controls.Add(this.lblASSIGNEEUSER);
            this.groupData.Controls.Add(this.lblETrackID);
            this.groupData.Controls.Add(this.lblTrackID);
            this.groupData.Controls.Add(this.lblVote);
            this.groupData.Controls.Add(this.btnAddVote);
            this.groupData.Controls.Add(this.lblEStatus);
            this.groupData.Controls.Add(this.btnProgress);
            this.groupData.Controls.Add(this.lblStatus);
            this.groupData.Controls.Add(this.mmeDescription);
            this.groupData.Controls.Add(this.lblECreateUserName);
            this.groupData.Controls.Add(this.lblCreateUserName);
            this.groupData.Controls.Add(this.lblSubject);
            this.groupData.Controls.Add(this.lblECreateDate);
            this.groupData.Controls.Add(this.lblCreateDate);
            this.groupData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupData.Location = new System.Drawing.Point(2, 22);
            this.groupData.Name = "groupData";
            this.groupData.Size = new System.Drawing.Size(470, 275);
            this.groupData.TabIndex = 1;
            // 
            // lblAssigneeUserName
            // 
            this.lblAssigneeUserName.Location = new System.Drawing.Point(203, 59);
            this.lblAssigneeUserName.Name = "lblAssigneeUserName";
            this.lblAssigneeUserName.Size = new System.Drawing.Size(18, 13);
            this.lblAssigneeUserName.TabIndex = 14;
            this.lblAssigneeUserName.Text = "Нэр";
            // 
            // lblASSIGNEEUSER
            // 
            this.lblASSIGNEEUSER.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(163)))));
            this.lblASSIGNEEUSER.Location = new System.Drawing.Point(7, 59);
            this.lblASSIGNEEUSER.Name = "lblASSIGNEEUSER";
            this.lblASSIGNEEUSER.Size = new System.Drawing.Size(190, 13);
            this.lblASSIGNEEUSER.TabIndex = 13;
            this.lblASSIGNEEUSER.Text = "Яг одоо хариуцаж байгаа хэрэглэгч :";
            // 
            // lblETrackID
            // 
            this.lblETrackID.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblETrackID.Location = new System.Drawing.Point(414, 43);
            this.lblETrackID.Name = "lblETrackID";
            this.lblETrackID.Size = new System.Drawing.Size(101, 13);
            this.lblETrackID.TabIndex = 12;
            this.lblETrackID.Text = "Шатлалын төлөв";
            // 
            // lblTrackID
            // 
            this.lblTrackID.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(163)))));
            this.lblTrackID.Location = new System.Drawing.Point(361, 43);
            this.lblTrackID.Name = "lblTrackID";
            this.lblTrackID.Size = new System.Drawing.Size(47, 13);
            this.lblTrackID.TabIndex = 11;
            this.lblTrackID.Text = "Шатлал :";
            // 
            // lblVote
            // 
            this.lblVote.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(163)))));
            this.lblVote.Location = new System.Drawing.Point(361, 59);
            this.lblVote.Name = "lblVote";
            this.lblVote.Size = new System.Drawing.Size(22, 13);
            this.lblVote.TabIndex = 9;
            this.lblVote.Text = "Vote";
            this.lblVote.ToolTipTitle = "Санал өгсөн хүмүүс";
            // 
            // btnAddVote
            // 
            this.btnAddVote.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnAddVote.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAddVote.Location = new System.Drawing.Point(330, 52);
            this.btnAddVote.Name = "btnAddVote";
            this.btnAddVote.Size = new System.Drawing.Size(24, 24);
            this.btnAddVote.TabIndex = 8;
            this.btnAddVote.Click += new System.EventHandler(this.btnAddVote_Click);
            // 
            // lblEStatus
            // 
            this.lblEStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblEStatus.Location = new System.Drawing.Point(414, 26);
            this.lblEStatus.Name = "lblEStatus";
            this.lblEStatus.Size = new System.Drawing.Size(81, 13);
            this.lblEStatus.TabIndex = 7;
            this.lblEStatus.Text = "Төлөв байдал";
            // 
            // btnProgress
            // 
            this.btnProgress.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnProgress.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnProgress.Location = new System.Drawing.Point(330, 19);
            this.btnProgress.Name = "btnProgress";
            this.btnProgress.Size = new System.Drawing.Size(25, 25);
            this.btnProgress.TabIndex = 10;
            this.btnProgress.Click += new System.EventHandler(this.btnProgress_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(163)))));
            this.lblStatus.Location = new System.Drawing.Point(361, 26);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Төлөв :";
            // 
            // mmeDescription
            // 
            this.mmeDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mmeDescription.Location = new System.Drawing.Point(3, 78);
            this.mmeDescription.Name = "mmeDescription";
            this.mmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mmeDescription.Properties.Appearance.Options.UseBackColor = true;
            this.mmeDescription.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.mmeDescription.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.mmeDescription.Properties.ReadOnly = true;
            this.mmeDescription.Size = new System.Drawing.Size(464, 194);
            this.mmeDescription.TabIndex = 5;
            // 
            // lblECreateUserName
            // 
            this.lblECreateUserName.Location = new System.Drawing.Point(114, 43);
            this.lblECreateUserName.Name = "lblECreateUserName";
            this.lblECreateUserName.Size = new System.Drawing.Size(18, 13);
            this.lblECreateUserName.TabIndex = 4;
            this.lblECreateUserName.Text = "Нэр";
            // 
            // lblCreateUserName
            // 
            this.lblCreateUserName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(163)))));
            this.lblCreateUserName.Location = new System.Drawing.Point(7, 43);
            this.lblCreateUserName.Name = "lblCreateUserName";
            this.lblCreateUserName.Size = new System.Drawing.Size(101, 13);
            this.lblCreateUserName.TabIndex = 3;
            this.lblCreateUserName.Text = "Үүсгэсэн хэрэглэгч :";
            // 
            // lblSubject
            // 
            this.lblSubject.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(27, 3);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(44, 16);
            this.lblSubject.TabIndex = 2;
            this.lblSubject.Text = "Гарчиг";
            // 
            // lblECreateDate
            // 
            this.lblECreateDate.Location = new System.Drawing.Point(114, 26);
            this.lblECreateDate.Name = "lblECreateDate";
            this.lblECreateDate.Size = new System.Drawing.Size(31, 13);
            this.lblECreateDate.TabIndex = 1;
            this.lblECreateDate.Text = "Огноо";
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(163)))));
            this.lblCreateDate.Location = new System.Drawing.Point(7, 26);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(71, 13);
            this.lblCreateDate.TabIndex = 0;
            this.lblCreateDate.Text = "Үүссэн огноо :";
            // 
            // navBarGroupControlContainerAttach
            // 
            this.navBarGroupControlContainerAttach.Controls.Add(this.grdAttach);
            this.navBarGroupControlContainerAttach.Name = "navBarGroupControlContainerAttach";
            this.navBarGroupControlContainerAttach.Size = new System.Drawing.Size(781, 175);
            this.navBarGroupControlContainerAttach.TabIndex = 2;
            // 
            // grdAttach
            // 
            this.grdAttach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAttach.Location = new System.Drawing.Point(0, 0);
            this.grdAttach.MainView = this.gvwAttach;
            this.grdAttach.Name = "grdAttach";
            this.grdAttach.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ImageEdit,
            this.PictureEdit,
            this.ImageComboBox});
            this.grdAttach.Size = new System.Drawing.Size(781, 175);
            this.grdAttach.TabIndex = 3;
            this.grdAttach.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwAttach,
            this.gridView2});
            // 
            // gvwAttach
            // 
            this.gvwAttach.CardHorzInterval = 0;
            this.gvwAttach.CardMinSize = new System.Drawing.Size(241, 86);
            this.gvwAttach.CardVertInterval = 0;
            this.gvwAttach.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.FILENAME,
            this.FILETYPE,
            this.ATTACHCREATEDATE,
            this.ATTACHDESCRIPTION});
            this.gvwAttach.GridControl = this.grdAttach;
            this.gvwAttach.Name = "gvwAttach";
            this.gvwAttach.OptionsItemText.TextToControlDistance = 7;
            this.gvwAttach.OptionsView.ShowCardExpandButton = false;
            this.gvwAttach.OptionsView.ShowHeaderPanel = false;
            this.gvwAttach.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Row;
            this.gvwAttach.TemplateCard = this.layoutViewCard1;
            // 
            // FILENAME
            // 
            this.FILENAME.Caption = "Файлын нэр";
            this.FILENAME.FieldName = "FILENAME";
            this.FILENAME.LayoutViewField = this.layoutViewField_FILENAME;
            this.FILENAME.Name = "FILENAME";
            this.FILENAME.OptionsColumn.AllowEdit = false;
            this.FILENAME.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.FILENAME.OptionsFilter.AllowFilter = false;
            // 
            // layoutViewField_FILENAME
            // 
            this.layoutViewField_FILENAME.EditorPreferredWidth = 106;
            this.layoutViewField_FILENAME.Location = new System.Drawing.Point(40, 0);
            this.layoutViewField_FILENAME.Name = "layoutViewField_FILENAME";
            this.layoutViewField_FILENAME.Size = new System.Drawing.Size(195, 20);
            this.layoutViewField_FILENAME.TextSize = new System.Drawing.Size(78, 13);
            // 
            // FILETYPE
            // 
            this.FILETYPE.AppearanceCell.Options.UseTextOptions = true;
            this.FILETYPE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FILETYPE.ColumnEdit = this.PictureEdit;
            this.FILETYPE.FieldName = "FILETYPE";
            this.FILETYPE.LayoutViewField = this.layoutViewField_FILETYPE;
            this.FILETYPE.Name = "FILETYPE";
            this.FILETYPE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.FILETYPE.OptionsFilter.AllowFilter = false;
            this.FILETYPE.Tag = "";
            // 
            // PictureEdit
            // 
            this.PictureEdit.Name = "PictureEdit";
            this.PictureEdit.ReadOnly = true;
            this.PictureEdit.DoubleClick += new System.EventHandler(this.PictureEdit_DoubleClick);
            // 
            // layoutViewField_FILETYPE
            // 
            this.layoutViewField_FILETYPE.EditorPreferredWidth = 36;
            this.layoutViewField_FILETYPE.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutViewField_FILETYPE.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_FILETYPE.MaxSize = new System.Drawing.Size(40, 40);
            this.layoutViewField_FILETYPE.MinSize = new System.Drawing.Size(40, 40);
            this.layoutViewField_FILETYPE.Name = "layoutViewField_FILETYPE";
            this.layoutViewField_FILETYPE.Size = new System.Drawing.Size(40, 40);
            this.layoutViewField_FILETYPE.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutViewField_FILETYPE.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_FILETYPE.TextToControlDistance = 0;
            this.layoutViewField_FILETYPE.TextVisible = false;
            // 
            // ATTACHCREATEDATE
            // 
            this.ATTACHCREATEDATE.Caption = "Үүсгэсэн огноо";
            this.ATTACHCREATEDATE.FieldName = "CREATEDATE";
            this.ATTACHCREATEDATE.LayoutViewField = this.layoutViewField_ATTACHCREATEDATE;
            this.ATTACHCREATEDATE.Name = "ATTACHCREATEDATE";
            this.ATTACHCREATEDATE.OptionsColumn.AllowEdit = false;
            this.ATTACHCREATEDATE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ATTACHCREATEDATE.OptionsFilter.AllowFilter = false;
            // 
            // layoutViewField_ATTACHCREATEDATE
            // 
            this.layoutViewField_ATTACHCREATEDATE.EditorPreferredWidth = 106;
            this.layoutViewField_ATTACHCREATEDATE.Location = new System.Drawing.Point(40, 20);
            this.layoutViewField_ATTACHCREATEDATE.Name = "layoutViewField_ATTACHCREATEDATE";
            this.layoutViewField_ATTACHCREATEDATE.Size = new System.Drawing.Size(195, 20);
            this.layoutViewField_ATTACHCREATEDATE.TextSize = new System.Drawing.Size(78, 13);
            // 
            // ATTACHDESCRIPTION
            // 
            this.ATTACHDESCRIPTION.AppearanceCell.Options.UseTextOptions = true;
            this.ATTACHDESCRIPTION.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ATTACHDESCRIPTION.FieldName = "DESCRIPTION";
            this.ATTACHDESCRIPTION.LayoutViewField = this.layoutViewField_ATTACHDESCRIPTION;
            this.ATTACHDESCRIPTION.Name = "ATTACHDESCRIPTION";
            this.ATTACHDESCRIPTION.OptionsColumn.AllowEdit = false;
            this.ATTACHDESCRIPTION.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ATTACHDESCRIPTION.OptionsFilter.AllowFilter = false;
            // 
            // layoutViewField_ATTACHDESCRIPTION
            // 
            this.layoutViewField_ATTACHDESCRIPTION.EditorPreferredWidth = 231;
            this.layoutViewField_ATTACHDESCRIPTION.Location = new System.Drawing.Point(0, 40);
            this.layoutViewField_ATTACHDESCRIPTION.Name = "layoutViewField_ATTACHDESCRIPTION";
            this.layoutViewField_ATTACHDESCRIPTION.Size = new System.Drawing.Size(235, 20);
            this.layoutViewField_ATTACHDESCRIPTION.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_ATTACHDESCRIPTION.TextToControlDistance = 0;
            this.layoutViewField_ATTACHDESCRIPTION.TextVisible = false;
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "TemplateCard";
            this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_FILENAME,
            this.layoutViewField_FILETYPE,
            this.layoutViewField_ATTACHCREATEDATE,
            this.layoutViewField_ATTACHDESCRIPTION});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 7;
            this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // ImageEdit
            // 
            this.ImageEdit.AutoHeight = false;
            this.ImageEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ImageEdit.Images = this.IssueImageList;
            this.ImageEdit.Name = "ImageEdit";
            // 
            // ImageComboBox
            // 
            this.ImageComboBox.AutoHeight = false;
            this.ImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ImageComboBox.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ((short)(0)), 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ((short)(1)), 5),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ((short)(2)), 6)});
            this.ImageComboBox.LargeImages = this.IssueImageList;
            this.ImageComboBox.Name = "ImageComboBox";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdAttach;
            this.gridView2.Name = "gridView2";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Дэлгэрэнгүй мэдээлэл";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainerIssue;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 300;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "Хавсралт файл";
            this.navBarGroup2.ControlContainer = this.navBarGroupControlContainerAttach;
            this.navBarGroup2.GroupClientHeight = 182;
            this.navBarGroup2.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // splitContainerControlChartAndTxn
            // 
            this.splitContainerControlChartAndTxn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControlChartAndTxn.Horizontal = false;
            this.splitContainerControlChartAndTxn.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControlChartAndTxn.Name = "splitContainerControlChartAndTxn";
            this.splitContainerControlChartAndTxn.Panel1.Controls.Add(this.ChartIssue);
            this.splitContainerControlChartAndTxn.Panel1.Text = "Panel1";
            this.splitContainerControlChartAndTxn.Panel2.Controls.Add(this.grdTxn);
            this.splitContainerControlChartAndTxn.Panel2.Text = "Panel2";
            this.splitContainerControlChartAndTxn.Size = new System.Drawing.Size(452, 468);
            this.splitContainerControlChartAndTxn.SplitterPosition = 263;
            this.splitContainerControlChartAndTxn.TabIndex = 0;
            this.splitContainerControlChartAndTxn.Text = "splitContainerControl1";
            // 
            // ChartIssue
            // 
            this.ChartIssue.AppearanceName = "Pastel Kit";
            this.ChartIssue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartIssue.Location = new System.Drawing.Point(0, 0);
            this.ChartIssue.Name = "ChartIssue";
            pieSeriesLabel1.LineVisible = true;
            series1.Label = pieSeriesLabel1;
            series1.Name = "pieSeries";
            piePointOptions1.PercentOptions.ValueAsPercent = false;
            piePointOptions1.PointView = DevExpress.XtraCharts.PointView.ArgumentAndValues;
            piePointOptions1.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number;
            piePointOptions1.ValueNumericOptions.Precision = 0;
            series1.PointOptions = piePointOptions1;
            series1.TopNOptions.Count = 6;
            series1.TopNOptions.Enabled = true;
            pieSeriesView1.RuntimeExploding = false;
            series1.View = pieSeriesView1;
            this.ChartIssue.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            pieSeriesLabel2.LineVisible = true;
            this.ChartIssue.SeriesTemplate.Label = pieSeriesLabel2;
            pieSeriesView2.RuntimeExploding = false;
            this.ChartIssue.SeriesTemplate.View = pieSeriesView2;
            this.ChartIssue.Size = new System.Drawing.Size(452, 263);
            this.ChartIssue.TabIndex = 213;
            // 
            // grdTxn
            // 
            this.grdTxn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTxn.Location = new System.Drawing.Point(0, 0);
            this.grdTxn.MainView = this.gvwTxn;
            this.grdTxn.Name = "grdTxn";
            this.grdTxn.Size = new System.Drawing.Size(452, 199);
            this.grdTxn.TabIndex = 1;
            this.grdTxn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwTxn,
            this.gridView3});
            this.grdTxn.DataSourceChanged += new System.EventHandler(this.grdTxn_DataSourceChanged);
            // 
            // gvwTxn
            // 
            this.gvwTxn.Appearance.EvenRow.BackColor = System.Drawing.Color.Linen;
            this.gvwTxn.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvwTxn.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.USERNAME,
            this.TXNSUBJECT,
            this.STATUSNAME,
            this.ACTIONTYPENAME,
            this.TXNDESCRIPTION,
            this.TXNTRACKNAME,
            this.JRNO,
            this.TXNDATE,
            this.CONTACTTYPENAME});
            this.gvwTxn.GridControl = this.grdTxn;
            this.gvwTxn.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwTxn.Images = this.IssueImageList;
            this.gvwTxn.Name = "gvwTxn";
            this.gvwTxn.OptionsView.ShowAutoFilterRow = true;
            this.gvwTxn.OptionsView.ShowGroupPanel = false;
            this.gvwTxn.OptionsView.ShowHorzLines = false;
            // 
            // USERNAME
            // 
            this.USERNAME.Caption = "Гүйлгээ хийсэн хэрэглэгч ";
            this.USERNAME.FieldName = "USERNAME";
            this.USERNAME.ImageIndex = 0;
            this.USERNAME.MinWidth = 100;
            this.USERNAME.Name = "USERNAME";
            this.USERNAME.OptionsColumn.AllowEdit = false;
            this.USERNAME.Visible = true;
            this.USERNAME.VisibleIndex = 1;
            this.USERNAME.Width = 100;
            // 
            // TXNSUBJECT
            // 
            this.TXNSUBJECT.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.TXNSUBJECT.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(71)))), ((int)(((byte)(163)))));
            this.TXNSUBJECT.AppearanceCell.Options.UseFont = true;
            this.TXNSUBJECT.AppearanceCell.Options.UseForeColor = true;
            this.TXNSUBJECT.Caption = "Гарчиг";
            this.TXNSUBJECT.FieldName = "SUBJECT";
            this.TXNSUBJECT.MinWidth = 100;
            this.TXNSUBJECT.Name = "TXNSUBJECT";
            this.TXNSUBJECT.OptionsColumn.AllowEdit = false;
            this.TXNSUBJECT.Visible = true;
            this.TXNSUBJECT.VisibleIndex = 2;
            this.TXNSUBJECT.Width = 100;
            // 
            // STATUSNAME
            // 
            this.STATUSNAME.Caption = "Гүйлгээний төрөл";
            this.STATUSNAME.FieldName = "STATUSNAME";
            this.STATUSNAME.Name = "STATUSNAME";
            this.STATUSNAME.OptionsColumn.AllowEdit = false;
            this.STATUSNAME.Width = 71;
            // 
            // ACTIONTYPENAME
            // 
            this.ACTIONTYPENAME.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ACTIONTYPENAME.AppearanceCell.Options.UseFont = true;
            this.ACTIONTYPENAME.Caption = "Гүйлгээний зорилго";
            this.ACTIONTYPENAME.FieldName = "ACTIONTYPENAME";
            this.ACTIONTYPENAME.MinWidth = 100;
            this.ACTIONTYPENAME.Name = "ACTIONTYPENAME";
            this.ACTIONTYPENAME.OptionsColumn.AllowEdit = false;
            this.ACTIONTYPENAME.Visible = true;
            this.ACTIONTYPENAME.VisibleIndex = 4;
            this.ACTIONTYPENAME.Width = 100;
            // 
            // TXNDESCRIPTION
            // 
            this.TXNDESCRIPTION.Caption = "Гүйлгээний тайлбар";
            this.TXNDESCRIPTION.FieldName = "DESCRIPTION";
            this.TXNDESCRIPTION.MinWidth = 100;
            this.TXNDESCRIPTION.Name = "TXNDESCRIPTION";
            this.TXNDESCRIPTION.OptionsColumn.AllowEdit = false;
            this.TXNDESCRIPTION.Visible = true;
            this.TXNDESCRIPTION.VisibleIndex = 3;
            this.TXNDESCRIPTION.Width = 100;
            // 
            // TXNTRACKNAME
            // 
            this.TXNTRACKNAME.Caption = "Шатлалын байдал";
            this.TXNTRACKNAME.FieldName = "TRACKNAME";
            this.TXNTRACKNAME.Name = "TXNTRACKNAME";
            this.TXNTRACKNAME.OptionsColumn.AllowEdit = false;
            this.TXNTRACKNAME.Width = 82;
            // 
            // JRNO
            // 
            this.JRNO.Caption = "JRNO";
            this.JRNO.FieldName = "JRNO";
            this.JRNO.Name = "JRNO";
            // 
            // TXNDATE
            // 
            this.TXNDATE.Caption = "Огноо";
            this.TXNDATE.FieldName = "TXNDATE";
            this.TXNDATE.MinWidth = 100;
            this.TXNDATE.Name = "TXNDATE";
            this.TXNDATE.OptionsColumn.AllowEdit = false;
            this.TXNDATE.Visible = true;
            this.TXNDATE.VisibleIndex = 0;
            this.TXNDATE.Width = 100;
            // 
            // CONTACTTYPENAME
            // 
            this.CONTACTTYPENAME.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CONTACTTYPENAME.AppearanceCell.Options.UseBackColor = true;
            this.CONTACTTYPENAME.Caption = "Холбоо барьсан төрөл";
            this.CONTACTTYPENAME.FieldName = "CONTACTTYPENAME";
            this.CONTACTTYPENAME.MinWidth = 100;
            this.CONTACTTYPENAME.Name = "CONTACTTYPENAME";
            this.CONTACTTYPENAME.OptionsColumn.AllowEdit = false;
            this.CONTACTTYPENAME.Visible = true;
            this.CONTACTTYPENAME.VisibleIndex = 5;
            this.CONTACTTYPENAME.Width = 100;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.grdTxn;
            this.gridView3.Name = "gridView3";
            // 
            // groupCustomerIssue
            // 
            this.groupCustomerIssue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupCustomerIssue.Controls.Add(this.btnIssueECustomer);
            this.groupCustomerIssue.Controls.Add(this.txtIssueCustomerName);
            this.groupCustomerIssue.Controls.Add(this.lblIssueCustomerName);
            this.groupCustomerIssue.Controls.Add(this.txtIssueCustomerNo);
            this.groupCustomerIssue.Controls.Add(this.lblIssueCustomerNo);
            this.groupCustomerIssue.Location = new System.Drawing.Point(3, 3);
            this.groupCustomerIssue.Name = "groupCustomerIssue";
            this.groupCustomerIssue.Size = new System.Drawing.Size(939, 57);
            this.groupCustomerIssue.TabIndex = 1;
            this.groupCustomerIssue.Text = "Харилцагч";
            // 
            // btnIssueECustomer
            // 
            this.btnIssueECustomer.Location = new System.Drawing.Point(659, 26);
            this.btnIssueECustomer.Name = "btnIssueECustomer";
            this.btnIssueECustomer.Size = new System.Drawing.Size(83, 23);
            this.btnIssueECustomer.TabIndex = 11;
            this.btnIssueECustomer.Text = "Лавлах";
            this.btnIssueECustomer.Click += new System.EventHandler(this.btnIssueECustomer_Click);
            // 
            // txtIssueCustomerName
            // 
            this.txtIssueCustomerName.Location = new System.Drawing.Point(453, 28);
            this.txtIssueCustomerName.Name = "txtIssueCustomerName";
            this.txtIssueCustomerName.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtIssueCustomerName.Properties.Appearance.Options.UseBackColor = true;
            this.txtIssueCustomerName.Properties.ReadOnly = true;
            this.txtIssueCustomerName.Size = new System.Drawing.Size(200, 20);
            this.txtIssueCustomerName.TabIndex = 10;
            // 
            // lblIssueCustomerName
            // 
            this.lblIssueCustomerName.Location = new System.Drawing.Point(349, 31);
            this.lblIssueCustomerName.Name = "lblIssueCustomerName";
            this.lblIssueCustomerName.Size = new System.Drawing.Size(98, 13);
            this.lblIssueCustomerName.TabIndex = 9;
            this.lblIssueCustomerName.Text = "Харилцагчийн нэр :";
            // 
            // txtIssueCustomerNo
            // 
            this.txtIssueCustomerNo.Location = new System.Drawing.Point(142, 28);
            this.txtIssueCustomerNo.Name = "txtIssueCustomerNo";
            this.txtIssueCustomerNo.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtIssueCustomerNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtIssueCustomerNo.Properties.ReadOnly = true;
            this.txtIssueCustomerNo.Size = new System.Drawing.Size(200, 20);
            this.txtIssueCustomerNo.TabIndex = 8;
            // 
            // lblIssueCustomerNo
            // 
            this.lblIssueCustomerNo.Location = new System.Drawing.Point(19, 31);
            this.lblIssueCustomerNo.Name = "lblIssueCustomerNo";
            this.lblIssueCustomerNo.Size = new System.Drawing.Size(117, 13);
            this.lblIssueCustomerNo.TabIndex = 7;
            this.lblIssueCustomerNo.Text = "Харилцагчийн дугаар :";
            // 
            // xtraTabPageNote
            // 
            this.xtraTabPageNote.Controls.Add(this.groupDataNote);
            this.xtraTabPageNote.Controls.Add(this.groupCustomerNote);
            this.xtraTabPageNote.Name = "xtraTabPageNote";
            this.xtraTabPageNote.Size = new System.Drawing.Size(946, 600);
            this.xtraTabPageNote.Text = "Тэмдэглэл";
            // 
            // groupDataNote
            // 
            this.groupDataNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDataNote.Controls.Add(this.ucToggleCustomer);
            this.groupDataNote.Location = new System.Drawing.Point(4, 64);
            this.groupDataNote.Name = "groupDataNote";
            this.groupDataNote.Size = new System.Drawing.Size(938, 532);
            this.groupDataNote.TabIndex = 1;
            // 
            // ucToggleCustomer
            // 
            this.ucToggleCustomer.Controls.Add(this.groupCustomerData);
            this.ucToggleCustomer.Controls.Add(this.grdCustomer);
            this.ucToggleCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucToggleCustomer.Location = new System.Drawing.Point(2, 2);
            this.ucToggleCustomer.Name = "ucToggleCustomer";
            this.ucToggleCustomer.Size = new System.Drawing.Size(934, 528);
            this.ucToggleCustomer.TabIndex = 2;
            this.ucToggleCustomer.ToggleShowDelete = false;
            this.ucToggleCustomer.ToggleShowEdit = false;
            this.ucToggleCustomer.ToggleShowExit = false;
            this.ucToggleCustomer.ToggleShowNew = false;
            this.ucToggleCustomer.ToggleShowReject = false;
            this.ucToggleCustomer.ToggleShowSave = false;
            // 
            // groupCustomerData
            // 
            this.groupCustomerData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupCustomerData.Controls.Add(this.lblNote);
            this.groupCustomerData.Controls.Add(this.txtBriefDesc);
            this.groupCustomerData.Controls.Add(this.lblBriefDesc);
            this.groupCustomerData.Controls.Add(this.lblContactType);
            this.groupCustomerData.Controls.Add(this.lblContactDate);
            this.groupCustomerData.Controls.Add(this.txtSeqNo);
            this.groupCustomerData.Controls.Add(this.lblSeqNo);
            this.groupCustomerData.Controls.Add(this.mmeNote);
            this.groupCustomerData.Controls.Add(this.dteContactDate);
            this.groupCustomerData.Controls.Add(this.cboContactType);
            this.groupCustomerData.Location = new System.Drawing.Point(554, 3);
            this.groupCustomerData.Name = "groupCustomerData";
            this.groupCustomerData.Size = new System.Drawing.Size(377, 492);
            this.groupCustomerData.TabIndex = 1;
            this.groupCustomerData.Text = "Өгөгдөл";
            // 
            // lblNote
            // 
            this.lblNote.Location = new System.Drawing.Point(23, 123);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(192, 13);
            this.lblNote.TabIndex = 8;
            this.lblNote.Text = "Холбоо барьсан дэлгэрэнгүй мэдээлэл";
            // 
            // txtBriefDesc
            // 
            this.txtBriefDesc.Location = new System.Drawing.Point(147, 98);
            this.txtBriefDesc.Name = "txtBriefDesc";
            this.txtBriefDesc.Size = new System.Drawing.Size(204, 20);
            this.txtBriefDesc.TabIndex = 7;
            this.txtBriefDesc.ToolTipTitle = "Товч утга оруулна уу";
            // 
            // lblBriefDesc
            // 
            this.lblBriefDesc.Location = new System.Drawing.Point(23, 101);
            this.lblBriefDesc.Name = "lblBriefDesc";
            this.lblBriefDesc.Size = new System.Drawing.Size(57, 13);
            this.lblBriefDesc.TabIndex = 6;
            this.lblBriefDesc.Text = "Товч утга :";
            // 
            // lblContactType
            // 
            this.lblContactType.Location = new System.Drawing.Point(23, 79);
            this.lblContactType.Name = "lblContactType";
            this.lblContactType.Size = new System.Drawing.Size(120, 13);
            this.lblContactType.TabIndex = 4;
            this.lblContactType.Text = "Холбоо барьсан төрөл :";
            // 
            // lblContactDate
            // 
            this.lblContactDate.Location = new System.Drawing.Point(23, 57);
            this.lblContactDate.Name = "lblContactDate";
            this.lblContactDate.Size = new System.Drawing.Size(119, 13);
            this.lblContactDate.TabIndex = 2;
            this.lblContactDate.Text = "Холбоо барьсан огноо :";
            this.lblContactDate.ToolTipTitle = "Холбоо барьсан огноо оруулна уу";
            // 
            // txtSeqNo
            // 
            this.txtSeqNo.Location = new System.Drawing.Point(147, 32);
            this.txtSeqNo.Name = "txtSeqNo";
            this.txtSeqNo.Size = new System.Drawing.Size(204, 20);
            this.txtSeqNo.TabIndex = 1;
            // 
            // lblSeqNo
            // 
            this.lblSeqNo.Location = new System.Drawing.Point(23, 35);
            this.lblSeqNo.Name = "lblSeqNo";
            this.lblSeqNo.Size = new System.Drawing.Size(64, 13);
            this.lblSeqNo.TabIndex = 0;
            this.lblSeqNo.Text = "Дэс дугаар :";
            // 
            // mmeNote
            // 
            this.mmeNote.Location = new System.Drawing.Point(23, 142);
            this.mmeNote.Name = "mmeNote";
            this.mmeNote.Size = new System.Drawing.Size(328, 148);
            this.mmeNote.TabIndex = 9;
            this.mmeNote.ToolTipTitle = "Холбоо барьсан дэлгэрэнгүй мэдээлэл оруулна уу";
            // 
            // dteContactDate
            // 
            this.dteContactDate.EditValue = null;
            this.dteContactDate.Location = new System.Drawing.Point(147, 54);
            this.dteContactDate.Name = "dteContactDate";
            this.dteContactDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteContactDate.Properties.Mask.EditMask = "";
            this.dteContactDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteContactDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteContactDate.Size = new System.Drawing.Size(204, 20);
            this.dteContactDate.TabIndex = 3;
            this.dteContactDate.ToolTipTitle = "Холбоо барьсан огноо оруулна уу";
            // 
            // cboContactType
            // 
            this.cboContactType.Location = new System.Drawing.Point(147, 76);
            this.cboContactType.Name = "cboContactType";
            this.cboContactType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboContactType.Properties.NullText = "";
            this.cboContactType.Size = new System.Drawing.Size(204, 20);
            this.cboContactType.TabIndex = 5;
            this.cboContactType.ToolTipTitle = "Холбоо барьсан төрөл сонгоно уу";
            // 
            // grdCustomer
            // 
            this.grdCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdCustomer.Location = new System.Drawing.Point(3, 3);
            this.grdCustomer.MainView = this.gvwCustomer;
            this.grdCustomer.Name = "grdCustomer";
            this.grdCustomer.Size = new System.Drawing.Size(545, 492);
            this.grdCustomer.TabIndex = 0;
            this.grdCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwCustomer,
            this.gridView5});
            // 
            // gvwCustomer
            // 
            this.gvwCustomer.GridControl = this.grdCustomer;
            this.gvwCustomer.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwCustomer.Name = "gvwCustomer";
            this.gvwCustomer.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvwCustomer_FocusedRowChanged);
            // 
            // gridView5
            // 
            this.gridView5.GridControl = this.grdCustomer;
            this.gridView5.Name = "gridView5";
            // 
            // groupCustomerNote
            // 
            this.groupCustomerNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupCustomerNote.Controls.Add(this.btnECustomer);
            this.groupCustomerNote.Controls.Add(this.txtCustomerName);
            this.groupCustomerNote.Controls.Add(this.lblCustomerName);
            this.groupCustomerNote.Controls.Add(this.txtCustomerNo);
            this.groupCustomerNote.Controls.Add(this.lblCustomerNo);
            this.groupCustomerNote.Location = new System.Drawing.Point(4, 4);
            this.groupCustomerNote.Name = "groupCustomerNote";
            this.groupCustomerNote.Size = new System.Drawing.Size(938, 54);
            this.groupCustomerNote.TabIndex = 0;
            this.groupCustomerNote.Text = "Харилцагч";
            // 
            // btnECustomer
            // 
            this.btnECustomer.Location = new System.Drawing.Point(648, 25);
            this.btnECustomer.Name = "btnECustomer";
            this.btnECustomer.Size = new System.Drawing.Size(83, 23);
            this.btnECustomer.TabIndex = 6;
            this.btnECustomer.Text = "Лавлах";
            this.btnECustomer.Click += new System.EventHandler(this.btnECustomer_Click);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(442, 27);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCustomerName.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerName.Properties.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(200, 20);
            this.txtCustomerName.TabIndex = 5;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Location = new System.Drawing.Point(338, 30);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(98, 13);
            this.lblCustomerName.TabIndex = 4;
            this.lblCustomerName.Text = "Харилцагчийн нэр :";
            // 
            // txtCustomerNo
            // 
            this.txtCustomerNo.Location = new System.Drawing.Point(131, 27);
            this.txtCustomerNo.Name = "txtCustomerNo";
            this.txtCustomerNo.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCustomerNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerNo.Properties.ReadOnly = true;
            this.txtCustomerNo.Size = new System.Drawing.Size(200, 20);
            this.txtCustomerNo.TabIndex = 3;
            // 
            // lblCustomerNo
            // 
            this.lblCustomerNo.Location = new System.Drawing.Point(8, 30);
            this.lblCustomerNo.Name = "lblCustomerNo";
            this.lblCustomerNo.Size = new System.Drawing.Size(117, 13);
            this.lblCustomerNo.TabIndex = 2;
            this.lblCustomerNo.Text = "Харилцагчийн дугаар :";
            // 
            // xtraTabIssue
            // 
            this.xtraTabIssue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabIssue.Location = new System.Drawing.Point(0, 0);
            this.xtraTabIssue.Name = "xtraTabIssue";
            this.xtraTabIssue.SelectedTabPage = this.xtraTabPageIssue;
            this.xtraTabIssue.Size = new System.Drawing.Size(951, 626);
            this.xtraTabIssue.TabIndex = 1;
            this.xtraTabIssue.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageNote,
            this.xtraTabPageIssue});
            this.xtraTabIssue.Selected += new DevExpress.XtraTab.TabPageEventHandler(this.xtraTabIssue_Selected);
            // 
            // FormContactEnq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 626);
            this.Controls.Add(this.xtraTabIssue);
            this.Name = "FormContactEnq";
            this.Text = "CRM & Issue Tracking";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).EndInit();
            this.xtraTabPageIssue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelTxnBtn)).EndInit();
            this.panelTxnBtn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupDataIssue)).EndInit();
            this.groupDataIssue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlData)).EndInit();
            this.splitContainerControlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarIssue)).EndInit();
            this.navBarIssue.ResumeLayout(false);
            this.navBarGroupControlContainerTxn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSourceIssue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwSourceIssue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.navBarGroupControlContainerIssue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupDataAll)).EndInit();
            this.groupDataAll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupData)).EndInit();
            this.groupData.ResumeLayout(false);
            this.groupData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmeDescription.Properties)).EndInit();
            this.navBarGroupControlContainerAttach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAttach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwAttach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_FILENAME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_FILETYPE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_ATTACHCREATEDATE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_ATTACHDESCRIPTION)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlChartAndTxn)).EndInit();
            this.splitContainerControlChartAndTxn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartIssue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTxn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwTxn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomerIssue)).EndInit();
            this.groupCustomerIssue.ResumeLayout(false);
            this.groupCustomerIssue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueCustomerNo.Properties)).EndInit();
            this.xtraTabPageNote.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupDataNote)).EndInit();
            this.groupDataNote.ResumeLayout(false);
            this.ucToggleCustomer.ResumeLayout(false);
            this.ucToggleCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomerData)).EndInit();
            this.groupCustomerData.ResumeLayout(false);
            this.groupCustomerData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBriefDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeqNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteContactDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteContactDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboContactType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomerNote)).EndInit();
            this.groupCustomerNote.ResumeLayout(false);
            this.groupCustomerNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabIssue)).EndInit();
            this.xtraTabIssue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorChecker;
        private System.Windows.Forms.ImageList IssueImageList;
        private DevExpress.XtraTab.XtraTabControl xtraTabIssue;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageNote;
        private DevExpress.XtraEditors.PanelControl groupDataNote;
        private ISM.Template.ucTogglePanel ucToggleCustomer;
        private DevExpress.XtraEditors.GroupControl groupCustomerData;
        private DevExpress.XtraEditors.LabelControl lblNote;
        private DevExpress.XtraEditors.TextEdit txtBriefDesc;
        private DevExpress.XtraEditors.LabelControl lblBriefDesc;
        private DevExpress.XtraEditors.LabelControl lblContactType;
        private DevExpress.XtraEditors.LabelControl lblContactDate;
        private DevExpress.XtraEditors.TextEdit txtSeqNo;
        private DevExpress.XtraEditors.LabelControl lblSeqNo;
        private DevExpress.XtraEditors.MemoEdit mmeNote;
        private DevExpress.XtraEditors.DateEdit dteContactDate;
        private DevExpress.XtraEditors.LookUpEdit cboContactType;
        private DevExpress.XtraGrid.GridControl grdCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwCustomer;
        private DevExpress.XtraEditors.GroupControl groupCustomerNote;
        private DevExpress.XtraEditors.SimpleButton btnECustomer;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.LabelControl lblCustomerName;
        private DevExpress.XtraEditors.TextEdit txtCustomerNo;
        private DevExpress.XtraEditors.LabelControl lblCustomerNo;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageIssue;
        private DevExpress.XtraEditors.PanelControl panelTxnBtn;
        private DevExpress.XtraEditors.SimpleButton btnLink;
        private DevExpress.XtraEditors.SimpleButton btnAssignee;
        private DevExpress.XtraEditors.SimpleButton btnComment;
        private DevExpress.XtraEditors.GroupControl groupDataIssue;
        private DevExpress.XtraNavBar.NavBarControl navBarIssue;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerTxn;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerIssue;
        private DevExpress.XtraEditors.GroupControl groupDataAll;
        private DevExpress.XtraEditors.PanelControl groupData;
        private DevExpress.XtraEditors.LabelControl lblETrackID;
        private DevExpress.XtraEditors.LabelControl lblTrackID;
        private DevExpress.XtraEditors.LabelControl lblVote;
        private DevExpress.XtraEditors.SimpleButton btnAddVote;
        private DevExpress.XtraEditors.LabelControl lblEStatus;
        private DevExpress.XtraEditors.SimpleButton btnProgress;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.MemoEdit mmeDescription;
        private DevExpress.XtraEditors.LabelControl lblECreateUserName;
        private DevExpress.XtraEditors.LabelControl lblCreateUserName;
        private DevExpress.XtraEditors.LabelControl lblSubject;
        private DevExpress.XtraEditors.LabelControl lblECreateDate;
        private DevExpress.XtraEditors.LabelControl lblCreateDate;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerAttach;
        private DevExpress.XtraGrid.GridControl grdAttach;
        private DevExpress.XtraGrid.Views.Layout.LayoutView gvwAttach;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn FILENAME;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_FILENAME;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn FILETYPE;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit PictureEdit;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_FILETYPE;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn ATTACHCREATEDATE;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_ATTACHCREATEDATE;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn ATTACHDESCRIPTION;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_ATTACHDESCRIPTION;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit ImageEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ImageComboBox;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraEditors.GroupControl groupCustomerIssue;
        private DevExpress.XtraEditors.SimpleButton btnIssueECustomer;
        private DevExpress.XtraEditors.TextEdit txtIssueCustomerName;
        private DevExpress.XtraEditors.LabelControl lblIssueCustomerName;
        private DevExpress.XtraEditors.TextEdit txtIssueCustomerNo;
        private DevExpress.XtraEditors.LabelControl lblIssueCustomerNo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.SimpleButton btnDeleteIssue;
        private DevExpress.XtraEditors.SimpleButton btnEditIssue;
        private DevExpress.XtraEditors.LabelControl lblAssigneeUserName;
        private DevExpress.XtraEditors.LabelControl lblASSIGNEEUSER;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlData;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlChartAndTxn;
        private DevExpress.XtraGrid.GridControl grdSourceIssue;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwSourceIssue;
        private DevExpress.XtraGrid.Columns.GridColumn SOURCEISSUEID;
        private DevExpress.XtraGrid.Columns.GridColumn SOURCESUBJECT;
        private DevExpress.XtraGrid.Columns.GridColumn SOURCEASSIGNEEUSER;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraCharts.ChartControl ChartIssue;
        private DevExpress.XtraGrid.GridControl grdTxn;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwTxn;
        private DevExpress.XtraGrid.Columns.GridColumn USERNAME;
        private DevExpress.XtraGrid.Columns.GridColumn TXNSUBJECT;
        private DevExpress.XtraGrid.Columns.GridColumn STATUSNAME;
        private DevExpress.XtraGrid.Columns.GridColumn ACTIONTYPENAME;
        private DevExpress.XtraGrid.Columns.GridColumn TXNDESCRIPTION;
        private DevExpress.XtraGrid.Columns.GridColumn TXNTRACKNAME;
        private DevExpress.XtraGrid.Columns.GridColumn JRNO;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn TXNDATE;
        private DevExpress.XtraGrid.Columns.GridColumn CONTACTTYPENAME;
    }
}