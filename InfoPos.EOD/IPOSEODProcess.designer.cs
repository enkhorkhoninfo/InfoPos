namespace InfoPos.EOD
{
    partial class IPOSEODProcess
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
            this.ggsControl = new DevExpress.XtraGauges.Win.GaugeControl();
            this.ggsGauge = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.ggsBackgroundLayerComponent = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.ggsValue = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.ggsComment = new DevExpress.XtraGauges.Win.Base.LabelComponent();
            this.ggsMarker = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleMarkerComponent();
            this.ggsRangeBar = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent();
            this.grdEODProcess = new DevExpress.XtraGrid.GridControl();
            this.gvwEODProcess = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.picEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.cboImageEdit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GLImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnProcessStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnEmail = new DevExpress.XtraEditors.SimpleButton();
            this.txtTxnDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ggsGauge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsBackgroundLayerComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsMarker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsRangeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEODProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwEODProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboImageEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // ggsControl
            // 
            this.ggsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ggsControl.BackColor = System.Drawing.SystemColors.Control;
            this.ggsControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ggsControl.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.ggsGauge});
            this.ggsControl.Location = new System.Drawing.Point(616, 13);
            this.ggsControl.Name = "ggsControl";
            this.ggsControl.Size = new System.Drawing.Size(260, 260);
            this.ggsControl.TabIndex = 0;
            // 
            // ggsGauge
            // 
            this.ggsGauge.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.ggsBackgroundLayerComponent});
            this.ggsGauge.Bounds = new System.Drawing.Rectangle(6, 6, 248, 248);
            this.ggsGauge.Labels.AddRange(new DevExpress.XtraGauges.Win.Base.LabelComponent[] {
            this.ggsComment});
            this.ggsGauge.Markers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleMarkerComponent[] {
            this.ggsMarker});
            this.ggsGauge.Name = "ggsGauge";
            this.ggsGauge.RangeBars.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent[] {
            this.ggsRangeBar});
            this.ggsGauge.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.ggsValue});
            // 
            // ggsBackgroundLayerComponent
            // 
            this.ggsBackgroundLayerComponent.ArcScale = this.ggsValue;
            this.ggsBackgroundLayerComponent.Name = "arcScaleBackgroundLayerComponent3";
            this.ggsBackgroundLayerComponent.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.77F, 0.77F);
            this.ggsBackgroundLayerComponent.Shader = new DevExpress.XtraGauges.Core.Drawing.StyleShader("Colors[Style1:#80FF80;Style2:SeaGreen]");
            this.ggsBackgroundLayerComponent.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularQuarter_Style1Left;
            this.ggsBackgroundLayerComponent.ZOrder = 1000;
            // 
            // ggsValue
            // 
            this.ggsValue.AppearanceTickmarkText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ggsValue.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(195F, 195F);
            this.ggsValue.EndAngle = -90F;
            this.ggsValue.MajorTickCount = 12;
            this.ggsValue.MajorTickmark.FormatString = "{0:F0}";
            this.ggsValue.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style7_2;
            this.ggsValue.MajorTickmark.TextOffset = 28F;
            this.ggsValue.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.ggsValue.MaxValue = 11F;
            this.ggsValue.MinorTickCount = 4;
            this.ggsValue.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style7_1;
            this.ggsValue.MinValue = 1F;
            this.ggsValue.Name = "arcScaleComponent3";
            this.ggsValue.RadiusX = 127F;
            this.ggsValue.RadiusY = 127F;
            this.ggsValue.StartAngle = -180F;
            this.ggsValue.Value = 1F;
            // 
            // ggsComment
            // 
            this.ggsComment.AppearanceText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ggsComment.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Gainsboro");
            this.ggsComment.Name = "circularGauge1_Label1";
            this.ggsComment.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(102.8F, 218.7F);
            this.ggsComment.Size = new System.Drawing.SizeF(150F, 25F);
            this.ggsComment.Text = "Тайлбар";
            this.ggsComment.ZOrder = -1001;
            // 
            // ggsMarker
            // 
            this.ggsMarker.ArcScale = this.ggsValue;
            this.ggsMarker.Name = "ggsGauge_Marker1";
            this.ggsMarker.Shader = new DevExpress.XtraGauges.Core.Drawing.StyleShader("Colors[Style1:#CBEC5A;Style2:highlighttext]");
            this.ggsMarker.ShapeOffset = 45F;
            this.ggsMarker.ShapeType = DevExpress.XtraGauges.Core.Model.MarkerPointerShapeType.WedgeLeft;
            this.ggsMarker.ZOrder = -100;
            // 
            // ggsRangeBar
            // 
            this.ggsRangeBar.AnchorValue = 1F;
            this.ggsRangeBar.AppearanceRangeBar.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#CBEC5A");
            this.ggsRangeBar.AppearanceRangeBar.BorderWidth = 1F;
            this.ggsRangeBar.AppearanceRangeBar.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#CBEC5A");
            this.ggsRangeBar.ArcScale = this.ggsValue;
            this.ggsRangeBar.EndOffset = 35F;
            this.ggsRangeBar.Name = "circularGauge1_RangeBar1";
            this.ggsRangeBar.Shader = new DevExpress.XtraGauges.Core.Drawing.StyleShader("Colors[Style1:DarkGreen;Style2:#C0FFC0]");
            this.ggsRangeBar.StartOffset = 60F;
            this.ggsRangeBar.ZOrder = -10;
            // 
            // grdEODProcess
            // 
            this.grdEODProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdEODProcess.Location = new System.Drawing.Point(12, 13);
            this.grdEODProcess.MainView = this.gvwEODProcess;
            this.grdEODProcess.Name = "grdEODProcess";
            this.grdEODProcess.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.picEdit,
            this.cboImageEdit});
            this.grdEODProcess.Size = new System.Drawing.Size(598, 259);
            this.grdEODProcess.TabIndex = 1;
            this.grdEODProcess.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwEODProcess});
            // 
            // gvwEODProcess
            // 
            this.gvwEODProcess.Appearance.EvenRow.BackColor = System.Drawing.Color.LightYellow;
            this.gvwEODProcess.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvwEODProcess.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gvwEODProcess.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvwEODProcess.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvwEODProcess.GridControl = this.grdEODProcess;
            this.gvwEODProcess.GroupPanelText = " ";
            this.gvwEODProcess.Name = "gvwEODProcess";
            // 
            // picEdit
            // 
            this.picEdit.Name = "picEdit";
            // 
            // cboImageEdit
            // 
            this.cboImageEdit.AutoHeight = false;
            this.cboImageEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboImageEdit.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cboImageEdit.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ((short)(0)), 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ((short)(1)), 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ((short)(9)), 0)});
            this.cboImageEdit.Name = "cboImageEdit";
            this.cboImageEdit.SmallImages = this.GLImageList;
            // 
            // GLImageList
            // 
            this.GLImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.GLImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.GLImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnProcessStart
            // 
            this.btnProcessStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProcessStart.Location = new System.Drawing.Point(12, 278);
            this.btnProcessStart.Name = "btnProcessStart";
            this.btnProcessStart.Size = new System.Drawing.Size(130, 24);
            this.btnProcessStart.TabIndex = 2;
            this.btnProcessStart.Text = "Процесс эхлүүлэх";
            this.btnProcessStart.Click += new System.EventHandler(this.btnProcessStart_Click);
            // 
            // btnEmail
            // 
            this.btnEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEmail.Location = new System.Drawing.Point(148, 278);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(130, 24);
            this.btnEmail.TabIndex = 3;
            this.btnEmail.Text = "Е-Майл явуулах";
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // txtTxnDate
            // 
            this.txtTxnDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTxnDate.AutoSize = true;
            this.txtTxnDate.Location = new System.Drawing.Point(297, 285);
            this.txtTxnDate.Name = "txtTxnDate";
            this.txtTxnDate.Size = new System.Drawing.Size(0, 13);
            this.txtTxnDate.TabIndex = 4;
            // 
            // IPOSEODProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 307);
            this.Controls.Add(this.txtTxnDate);
            this.Controls.Add(this.btnEmail);
            this.Controls.Add(this.btnProcessStart);
            this.Controls.Add(this.grdEODProcess);
            this.Controls.Add(this.ggsControl);
            this.MinimumSize = new System.Drawing.Size(776, 334);
            this.Name = "IPOSEODProcess";
            this.Text = "Өдөр өндөрлөлтийн процесс";
            this.Load += new System.EventHandler(this.IPOSEODProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ggsGauge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsBackgroundLayerComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsMarker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ggsRangeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEODProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwEODProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboImageEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGauges.Win.GaugeControl ggsControl;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge ggsGauge;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent ggsBackgroundLayerComponent;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent ggsValue;
        private DevExpress.XtraGauges.Win.Base.LabelComponent ggsComment;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent ggsRangeBar;
        private DevExpress.XtraGrid.GridControl grdEODProcess;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwEODProcess;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit picEdit;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleMarkerComponent ggsMarker;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cboImageEdit;
        private System.Windows.Forms.ImageList GLImageList;
        private DevExpress.XtraEditors.SimpleButton btnProcessStart;
        private DevExpress.XtraEditors.SimpleButton btnEmail;
        private System.Windows.Forms.Label txtTxnDate;
    }
}