namespace ISM.Template.UserControls
{
    partial class ucAttachView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAttachView));
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup2 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navAll = new DevExpress.XtraNavBar.NavBarItem();
            this.navImage = new DevExpress.XtraNavBar.NavBarItem();
            this.navDoc = new DevExpress.XtraNavBar.NavBarItem();
            this.navOther = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navFileView = new DevExpress.XtraNavBar.NavBarItem();
            this.navFileAdd = new DevExpress.XtraNavBar.NavBarItem();
            this.navFileRemove = new DevExpress.XtraNavBar.NavBarItem();
            this.navFileDownload = new DevExpress.XtraNavBar.NavBarItem();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.imageCollection2 = new DevExpress.Utils.ImageCollection(this.components);
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblAttachId = new DevExpress.XtraEditors.LabelControl();
            this.lblAttachDate = new DevExpress.XtraEditors.LabelControl();
            this.lblUserNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.lblFilename = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblAttachSize = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblAttachDesc = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2,
            this.navBarGroup3});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navImage,
            this.navDoc,
            this.navOther,
            this.navAll,
            this.navFileAdd,
            this.navFileRemove,
            this.navFileDownload,
            this.navFileView});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 126;
            this.navBarControl1.OptionsNavPane.ShowOverflowButton = false;
            this.navBarControl1.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl1.OptionsNavPane.ShowSplitter = false;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar;
            this.navBarControl1.Size = new System.Drawing.Size(126, 439);
            this.navBarControl1.SmallImages = this.imageCollection1;
            this.navBarControl1.StoreDefaultPaintStyleName = true;
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Файлын төрөл";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navAll),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navImage),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navDoc),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navOther)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navAll
            // 
            this.navAll.Caption = "Бүгд";
            this.navAll.Name = "navAll";
            this.navAll.SmallImageIndex = 3;
            this.navAll.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navAll_LinkClicked);
            // 
            // navImage
            // 
            this.navImage.Caption = "Зураг";
            this.navImage.Name = "navImage";
            this.navImage.SmallImageIndex = 0;
            this.navImage.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navImage_LinkClicked);
            // 
            // navDoc
            // 
            this.navDoc.Caption = "Документ";
            this.navDoc.Name = "navDoc";
            this.navDoc.SmallImageIndex = 1;
            this.navDoc.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navDoc_LinkClicked);
            // 
            // navOther
            // 
            this.navOther.Caption = "Бусад";
            this.navOther.Name = "navOther";
            this.navOther.SmallImageIndex = 2;
            this.navOther.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navOther_LinkClicked);
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.radioGroup1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(118, 52);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = 0;
            this.radioGroup1.Location = new System.Drawing.Point(3, 1);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Зургаар"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Жагсаалтаар")});
            this.radioGroup1.Properties.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_Properties_SelectedIndexChanged);
            this.radioGroup1.Size = new System.Drawing.Size(112, 51);
            this.radioGroup1.TabIndex = 0;
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "Харагдац";
            this.navBarGroup2.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.GroupClientHeight = 59;
            this.navBarGroup2.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "Ажилбар";
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navFileView),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navFileAdd),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navFileRemove),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navFileDownload)});
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // navFileView
            // 
            this.navFileView.Caption = "Харах";
            this.navFileView.Name = "navFileView";
            this.navFileView.SmallImageIndex = 7;
            this.navFileView.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navFileView_LinkClicked);
            // 
            // navFileAdd
            // 
            this.navFileAdd.Caption = "Файл нэмэх";
            this.navFileAdd.Name = "navFileAdd";
            this.navFileAdd.SmallImageIndex = 4;
            this.navFileAdd.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navFileAdd_LinkClicked);
            // 
            // navFileRemove
            // 
            this.navFileRemove.Caption = "Файл хасах";
            this.navFileRemove.Name = "navFileRemove";
            this.navFileRemove.SmallImageIndex = 5;
            this.navFileRemove.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navFileRemove_LinkClicked);
            // 
            // navFileDownload
            // 
            this.navFileDownload.Caption = "Татаж авах";
            this.navFileDownload.Name = "navFileDownload";
            this.navFileDownload.SmallImageIndex = 6;
            this.navFileDownload.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navFileDownload_LinkClicked);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Icon_371.ico");
            this.imageCollection1.Images.SetKeyName(1, "Icon_370.ico");
            this.imageCollection1.Images.SetKeyName(2, "My eBooks XP.ico");
            this.imageCollection1.Images.SetKeyName(3, "Icon_39.ico");
            this.imageCollection1.Images.SetKeyName(4, "id_card_add.png");
            this.imageCollection1.Images.SetKeyName(5, "id_card_error.png");
            this.imageCollection1.Images.SetKeyName(6, "import1.png");
            this.imageCollection1.Images.SetKeyName(7, "id_card_view.png");
            // 
            // galleryControl1
            // 
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.DesignGalleryGroupIndex = 0;
            this.galleryControl1.DesignGalleryItemIndex = 0;
            // 
            // galleryControlGallery1
            // 
            this.galleryControl1.Gallery.AllowFilter = false;
            galleryItemGroup2.Caption = "Group1";
            this.galleryControl1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup2});
            this.galleryControl1.Gallery.ImageSize = new System.Drawing.Size(64, 64);
            this.galleryControl1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleCheck;
            this.galleryControl1.Gallery.ShowGroupCaption = false;
            this.galleryControl1.Gallery.ShowItemText = true;
            this.galleryControl1.Gallery.UseMaxImageSize = true;
            this.galleryControl1.Gallery.ItemDoubleClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.galleryControlGallery1_ItemDoubleClick);
            this.galleryControl1.Location = new System.Drawing.Point(126, 0);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(473, 132);
            this.galleryControl1.TabIndex = 1;
            this.galleryControl1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(452, 128);
            // 
            // imageCollection2
            // 
            this.imageCollection2.ImageSize = new System.Drawing.Size(64, 64);
            this.imageCollection2.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection2.ImageStream")));
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(126, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 439);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblAttachDesc);
            this.panelControl1.Controls.Add(this.lblAttachId);
            this.panelControl1.Controls.Add(this.lblAttachDate);
            this.panelControl1.Controls.Add(this.lblUserNo);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.lblFilename);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.lblAttachSize);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(131, 373);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(584, 66);
            this.panelControl1.TabIndex = 3;
            // 
            // lblAttachId
            // 
            this.lblAttachId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAttachId.Location = new System.Drawing.Point(338, 24);
            this.lblAttachId.Name = "lblAttachId";
            this.lblAttachId.Size = new System.Drawing.Size(110, 13);
            this.lblAttachId.TabIndex = 10;
            this.lblAttachId.Text = "...";
            // 
            // lblAttachDate
            // 
            this.lblAttachDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAttachDate.Location = new System.Drawing.Point(132, 24);
            this.lblAttachDate.Name = "lblAttachDate";
            this.lblAttachDate.Size = new System.Drawing.Size(147, 13);
            this.lblAttachDate.TabIndex = 8;
            this.lblAttachDate.Text = "...";
            // 
            // lblUserNo
            // 
            this.lblUserNo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblUserNo.Location = new System.Drawing.Point(132, 43);
            this.lblUserNo.Name = "lblUserNo";
            this.lblUserNo.Size = new System.Drawing.Size(147, 13);
            this.lblUserNo.TabIndex = 7;
            this.lblUserNo.Text = "...";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Location = new System.Drawing.Point(284, 24);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(47, 13);
            this.labelControl9.TabIndex = 9;
            this.labelControl9.Text = "Дугаар:";
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFilename.Location = new System.Drawing.Point(132, 5);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(147, 13);
            this.lblFilename.TabIndex = 5;
            this.lblFilename.Text = "...";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(6, 43);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(120, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Оруулсан хэрэглэгч:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(6, 24);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Оруулсан огноо:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(6, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Файлын нэр:";
            // 
            // lblAttachSize
            // 
            this.lblAttachSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAttachSize.Location = new System.Drawing.Point(338, 5);
            this.lblAttachSize.Name = "lblAttachSize";
            this.lblAttachSize.Size = new System.Drawing.Size(110, 13);
            this.lblAttachSize.TabIndex = 6;
            this.lblAttachSize.Text = "...";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(284, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(47, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Хэмжээ:";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(137, 136);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(462, 183);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // lblAttachDesc
            // 
            this.lblAttachDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAttachDesc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAttachDesc.LineLocation = DevExpress.XtraEditors.LineLocation.Left;
            this.lblAttachDesc.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Vertical;
            this.lblAttachDesc.LineVisible = true;
            this.lblAttachDesc.Location = new System.Drawing.Point(469, 5);
            this.lblAttachDesc.Name = "lblAttachDesc";
            this.lblAttachDesc.Size = new System.Drawing.Size(110, 56);
            this.lblAttachDesc.TabIndex = 11;
            this.lblAttachDesc.Text = "...";
            // 
            // ucAttachView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.galleryControl1);
            this.Controls.Add(this.navBarControl1);
            this.Name = "ucAttachView";
            this.Size = new System.Drawing.Size(715, 439);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraNavBar.NavBarItem navImage;
        private DevExpress.XtraNavBar.NavBarItem navDoc;
        private DevExpress.XtraNavBar.NavBarItem navOther;
        private DevExpress.XtraNavBar.NavBarItem navAll;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.Utils.ImageCollection imageCollection2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblAttachId;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl lblAttachDate;
        private DevExpress.XtraEditors.LabelControl lblUserNo;
        private DevExpress.XtraEditors.LabelControl lblAttachSize;
        private DevExpress.XtraEditors.LabelControl lblFilename;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarItem navFileAdd;
        private DevExpress.XtraNavBar.NavBarItem navFileRemove;
        private DevExpress.XtraNavBar.NavBarItem navFileDownload;
        private DevExpress.XtraNavBar.NavBarItem navFileView;
        private DevExpress.XtraEditors.LabelControl lblAttachDesc;
    }
}
