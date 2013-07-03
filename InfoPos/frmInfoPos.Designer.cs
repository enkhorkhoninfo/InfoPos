namespace InfoPos
{
    partial class frmInfoPos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfoPos));
            this.pnlTop = new DevExpress.XtraEditors.SplitContainerControl();
            this.picTitle = new DevExpress.XtraEditors.PictureEdit();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.InstName = new DevExpress.XtraBars.BarStaticItem();
            this.UserBranch = new DevExpress.XtraBars.BarStaticItem();
            this.SystemDate = new DevExpress.XtraBars.BarStaticItem();
            this.ServerPort = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem5 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem6 = new DevExpress.XtraBars.BarStaticItem();
            this.Version = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonAbout = new DevExpress.XtraBars.BarButtonItem();
            this.sbarTagReader = new DevExpress.XtraBars.BarStaticItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.touchButtonGroup1 = new ISM.Touch.TouchButtonGroup();
            this.splitterTop = new DevExpress.XtraEditors.SplitterControl();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager();
            this.barLinkContainerItem1 = new DevExpress.XtraBars.BarLinkContainerItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem4 = new DevExpress.XtraBars.BarStaticItem();
            this.SystemMsg = new DevExpress.XtraBars.BarStaticItem();
            this.defaultBarAndDockingController1 = new DevExpress.XtraBars.DefaultBarAndDockingController();
            this.barStaticItem7 = new DevExpress.XtraBars.BarStaticItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController1.Controller)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            resources.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.LookAndFeel.UseWindowsXPTheme = true;
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Panel1.Controls.Add(this.picTitle);
            this.pnlTop.Panel1.Controls.Add(this.lblTitle);
            this.pnlTop.Panel1.Controls.Add(this.gridControl1);
            this.pnlTop.Panel2.Controls.Add(this.touchButtonGroup1);
            this.pnlTop.ShowCaption = true;
            this.pnlTop.SplitterPosition = 297;
            // 
            // picTitle
            // 
            resources.ApplyResources(this.picTitle, "picTitle");
            this.picTitle.MenuManager = this.ribbon;
            this.picTitle.Name = "picTitle";
            this.picTitle.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // ribbon
            // 
            resources.ApplyResources(this.ribbon, "ribbon");
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.InstName,
            this.UserBranch,
            this.SystemDate,
            this.ServerPort,
            this.barStaticItem2,
            this.barStaticItem5,
            this.barStaticItem6,
            this.Version,
            this.barButtonAbout,
            this.sbarTagReader});
            this.ribbon.MaxItemId = 41;
            this.ribbon.Name = "ribbon";
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemRichTextEdit1});
            this.ribbon.ShowCategoryInCaption = false;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            this.ribbon.ApplicationButtonClick += new System.EventHandler(this.ribbon_ApplicationButtonClick);
            // 
            // InstName
            // 
            resources.ApplyResources(this.InstName, "InstName");
            this.InstName.Id = 18;
            this.InstName.Name = "InstName";
            this.InstName.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // UserBranch
            // 
            resources.ApplyResources(this.UserBranch, "UserBranch");
            this.UserBranch.Id = 19;
            this.UserBranch.Name = "UserBranch";
            this.UserBranch.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.UserBranch.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // SystemDate
            // 
            resources.ApplyResources(this.SystemDate, "SystemDate");
            this.SystemDate.Id = 20;
            this.SystemDate.Name = "SystemDate";
            this.SystemDate.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // ServerPort
            // 
            resources.ApplyResources(this.ServerPort, "ServerPort");
            this.ServerPort.Id = 21;
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem2
            // 
            resources.ApplyResources(this.barStaticItem2, "barStaticItem2");
            this.barStaticItem2.Id = 23;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem5
            // 
            resources.ApplyResources(this.barStaticItem5, "barStaticItem5");
            this.barStaticItem5.Id = 24;
            this.barStaticItem5.Name = "barStaticItem5";
            this.barStaticItem5.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem6
            // 
            resources.ApplyResources(this.barStaticItem6, "barStaticItem6");
            this.barStaticItem6.Id = 25;
            this.barStaticItem6.Name = "barStaticItem6";
            this.barStaticItem6.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // Version
            // 
            resources.ApplyResources(this.Version, "Version");
            this.Version.Id = 38;
            this.Version.Name = "Version";
            this.Version.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barButtonAbout
            // 
            resources.ApplyResources(this.barButtonAbout, "barButtonAbout");
            this.barButtonAbout.Id = 39;
            this.barButtonAbout.Name = "barButtonAbout";
            this.barButtonAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonAbout_ItemClick);
            // 
            // sbarTagReader
            // 
            resources.ApplyResources(this.sbarTagReader, "sbarTagReader");
            this.sbarTagReader.Id = 40;
            this.sbarTagReader.Name = "sbarTagReader";
            this.sbarTagReader.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // repositoryItemTextEdit1
            // 
            resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            resources.ApplyResources(this.repositoryItemTextEdit2, "repositoryItemTextEdit2");
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            this.repositoryItemRichTextEdit1.ShowCaretInReadOnly = false;
            // 
            // ribbonStatusBar
            // 
            resources.ApplyResources(this.ribbonStatusBar, "ribbonStatusBar");
            this.ribbonStatusBar.ItemLinks.Add(this.InstName);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem2);
            this.ribbonStatusBar.ItemLinks.Add(this.UserBranch);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem5);
            this.ribbonStatusBar.ItemLinks.Add(this.SystemDate);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem6);
            this.ribbonStatusBar.ItemLinks.Add(this.ServerPort);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem6);
            this.ribbonStatusBar.ItemLinks.Add(this.sbarTagReader);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem2);
            this.ribbonStatusBar.ItemLinks.Add(this.Version);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItem6);
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonAbout);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblTitle.Appearance.Font")));
            this.lblTitle.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblTitle.Appearance.ForeColor")));
            this.lblTitle.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom;
            this.lblTitle.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical;
            this.lblTitle.Name = "lblTitle";
            // 
            // gridControl1
            // 
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.BackColor = ((System.Drawing.Color)(resources.GetObject("gridView1.Appearance.Row.BackColor")));
            this.gridView1.Appearance.Row.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.Row.Font")));
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // touchButtonGroup1
            // 
            resources.ApplyResources(this.touchButtonGroup1, "touchButtonGroup1");
            this.touchButtonGroup1.BurronBackColor = System.Drawing.Color.SteelBlue;
            this.touchButtonGroup1.BurronForeColor = System.Drawing.SystemColors.ButtonFace;
            this.touchButtonGroup1.ButtonTrackFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.touchButtonGroup1.ButtonTrackHeigth = 26;
            this.touchButtonGroup1.ButtonTrackText = "Path1:";
            this.touchButtonGroup1.ButtonTrackVisible = false;
            this.touchButtonGroup1.Name = "touchButtonGroup1";
            this.touchButtonGroup1.ParentMDI = null;
            // 
            // splitterTop
            // 
            resources.ApplyResources(this.splitterTop, "splitterTop");
            this.splitterTop.LookAndFeel.UseDefaultLookAndFeel = false;
            this.splitterTop.LookAndFeel.UseWindowsXPTheme = true;
            this.splitterTop.Name = "splitterTop";
            this.splitterTop.TabStop = false;
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabbedMdiManager1.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabbedMdiManager1.HeaderButtons = DevExpress.XtraTab.TabButtons.Close;
            this.xtraTabbedMdiManager1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabbedMdiManager1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Vertical;
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.None;
            this.xtraTabbedMdiManager1.SetNextMdiChildMode = DevExpress.XtraTabbedMdi.SetNextMdiChildMode.TabControl;
            this.xtraTabbedMdiManager1.ShowFloatingDropHint = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabbedMdiManager1.ShowHeaderFocus = DevExpress.Utils.DefaultBoolean.False;
            // 
            // barLinkContainerItem1
            // 
            resources.ApplyResources(this.barLinkContainerItem1, "barLinkContainerItem1");
            this.barLinkContainerItem1.Id = 12;
            this.barLinkContainerItem1.Name = "barLinkContainerItem1";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Id = -1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem3
            // 
            resources.ApplyResources(this.barStaticItem3, "barStaticItem3");
            this.barStaticItem3.Id = 23;
            this.barStaticItem3.Name = "barStaticItem3";
            this.barStaticItem3.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem4
            // 
            resources.ApplyResources(this.barStaticItem4, "barStaticItem4");
            this.barStaticItem4.Id = 23;
            this.barStaticItem4.Name = "barStaticItem4";
            this.barStaticItem4.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // SystemMsg
            // 
            resources.ApplyResources(this.SystemMsg, "SystemMsg");
            this.SystemMsg.Id = 22;
            this.SystemMsg.Name = "SystemMsg";
            this.SystemMsg.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // defaultBarAndDockingController1
            // 
            // 
            // barStaticItem7
            // 
            resources.ApplyResources(this.barStaticItem7, "barStaticItem7");
            this.barStaticItem7.Id = 35;
            this.barStaticItem7.Name = "barStaticItem7";
            this.barStaticItem7.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // frmInfoPos
            // 
            this.AllowDisplayRibbon = false;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitterTop);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Name = "frmInfoPos";
            this.Ribbon = this.ribbon;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StatusBar = this.ribbonStatusBar;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInfoPos_FormClosing);
            this.Load += new System.EventHandler(this.frmInfoPos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController1.Controller)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitterControl splitterTop;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarLinkContainerItem barLinkContainerItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem InstName;
        private DevExpress.XtraBars.BarStaticItem UserBranch;
        private DevExpress.XtraBars.BarStaticItem SystemDate;
        private DevExpress.XtraBars.BarStaticItem ServerPort;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarStaticItem barStaticItem5;
        private DevExpress.XtraBars.BarStaticItem barStaticItem6;
        private DevExpress.XtraBars.BarStaticItem barStaticItem3;
        private DevExpress.XtraBars.BarStaticItem barStaticItem4;
        private DevExpress.XtraBars.BarStaticItem SystemMsg;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.SplitContainerControl pnlTop;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private ISM.Touch.TouchButtonGroup touchButtonGroup1;
        private DevExpress.XtraBars.DefaultBarAndDockingController defaultBarAndDockingController1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem7;
        private DevExpress.XtraBars.BarStaticItem Version;
        private DevExpress.XtraBars.BarButtonItem barButtonAbout;
        private DevExpress.XtraBars.BarStaticItem sbarTagReader;
        private DevExpress.XtraEditors.PictureEdit picTitle;
        private DevExpress.XtraEditors.LabelControl lblTitle;
    }
}