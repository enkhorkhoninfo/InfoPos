namespace InfoPos.sales
{
    partial class frmSalesInv
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSalesInv));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.galleryCust = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnTag = new DevExpress.XtraEditors.SimpleButton();
            this.btnMove = new DevExpress.XtraEditors.SimpleButton();
            this.imageGallery = new DevExpress.Utils.ImageCollection(this.components);
            this.galleryControlGallery2 = new DevExpress.XtraBars.Ribbon.Gallery.GalleryControlGallery();
            this.btnCustAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnCustRem = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryCust)).BeginInit();
            this.galleryCust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageGallery)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(3, 63);
            this.splitContainerControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.galleryCust);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(764, 342);
            this.splitContainerControl1.SplitterPosition = 287;
            this.splitContainerControl1.TabIndex = 16;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.DataMember = "_cart";
            this.gridControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(390, 214);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.TabStop = false;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView1.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFind.AllowFindPanel = false;
            this.gridView1.OptionsLayout.StoreVisualOptions = false;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            //this.gridView1.OptionsView.ShowPreviewRowLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;
            // 
            // galleryCust
            // 
            this.galleryCust.Controls.Add(this.galleryControlClient1);
            this.galleryCust.DesignGalleryGroupIndex = 0;
            this.galleryCust.DesignGalleryItemIndex = 0;
            // 
            // galleryControlGallery1
            // 
            galleryItemGroup1.Caption = "Үйлчлүүлэгчид";
            galleryItemGroup1.CaptionControlSize = new System.Drawing.Size(119, 56);
            this.galleryCust.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1});
            this.galleryCust.Location = new System.Drawing.Point(0, 0);
            this.galleryCust.Margin = new System.Windows.Forms.Padding(4);
            this.galleryCust.Name = "galleryCust";
            this.galleryCust.Size = new System.Drawing.Size(262, 197);
            this.galleryCust.TabIndex = 0;
            this.galleryCust.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryCust;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.galleryControlClient1.Size = new System.Drawing.Size(241, 193);
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Location = new System.Drawing.Point(515, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(124, 56);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Буцах";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnTag
            // 
            this.btnTag.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTag.Appearance.Options.UseFont = true;
            this.btnTag.Location = new System.Drawing.Point(387, 3);
            this.btnTag.Name = "btnTag";
            this.btnTag.Size = new System.Drawing.Size(124, 56);
            this.btnTag.TabIndex = 2;
            this.btnTag.Text = "Таг дээр \r\nбичих";
            this.btnTag.Click += new System.EventHandler(this.btnTag_Click);
            // 
            // btnMove
            // 
            this.btnMove.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMove.Appearance.Options.UseFont = true;
            this.btnMove.Location = new System.Drawing.Point(259, 3);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(124, 56);
            this.btnMove.TabIndex = 1;
            this.btnMove.Text = "Бараа \r\nшилжүүлэх";
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // imageGallery
            // 
            this.imageGallery.ImageSize = new System.Drawing.Size(1, 1);
            this.imageGallery.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageGallery.ImageStream")));
            this.imageGallery.Images.SetKeyName(0, "xrcontrol.png");
            // 
            // btnCustAdd
            // 
            this.btnCustAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustAdd.Appearance.Options.UseFont = true;
            this.btnCustAdd.Location = new System.Drawing.Point(3, 3);
            this.btnCustAdd.Name = "btnCustAdd";
            this.btnCustAdd.Size = new System.Drawing.Size(124, 56);
            this.btnCustAdd.TabIndex = 5;
            this.btnCustAdd.Text = "Үйлчлүүлэгч \r\nнэмэх";
            this.btnCustAdd.Click += new System.EventHandler(this.btnCustAdd_Click);
            // 
            // btnCustRem
            // 
            this.btnCustRem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustRem.Appearance.Options.UseFont = true;
            this.btnCustRem.Location = new System.Drawing.Point(131, 3);
            this.btnCustRem.Name = "btnCustRem";
            this.btnCustRem.Size = new System.Drawing.Size(124, 56);
            this.btnCustRem.TabIndex = 6;
            this.btnCustRem.Text = "Үйлчлүүлэгч \r\nхасах";
            this.btnCustRem.Click += new System.EventHandler(this.btnCustRem_Click);
            // 
            // frmSalesInv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 409);
            this.Controls.Add(this.btnTag);
            this.Controls.Add(this.btnCustRem);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.btnCustAdd);
            this.Controls.Add(this.btnExit);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(787, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(787, 38);
            this.Name = "frmSalesInv";
            this.Text = "Таг дээр бичих";
            this.Load += new System.EventHandler(this.frmSalesInv_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryCust)).EndInit();
            this.galleryCust.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageGallery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryCust;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraBars.Ribbon.Gallery.GalleryControlGallery galleryControlGallery2;
        private DevExpress.Utils.ImageCollection imageGallery;
        private DevExpress.XtraEditors.SimpleButton btnMove;
        private DevExpress.XtraEditors.SimpleButton btnTag;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnCustAdd;
        private DevExpress.XtraEditors.SimpleButton btnCustRem;
    }
}