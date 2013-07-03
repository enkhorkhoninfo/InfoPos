namespace HeavenPro.Image
{
    partial class FormZoom
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
            this.groupCtrl = new DevExpress.XtraEditors.GroupControl();
            this.ZoomHelp = new DevExpress.XtraEditors.XtraScrollableControl();
            this.zoomPicture = new DevExpress.XtraEditors.PictureEdit();
            this.zoomTrack = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnRotRight = new DevExpress.XtraEditors.SimpleButton();
            this.btnRotLeft = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrl)).BeginInit();
            this.groupCtrl.SuspendLayout();
            this.ZoomHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrack.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupCtrl
            // 
            this.groupCtrl.Controls.Add(this.ZoomHelp);
            this.groupCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCtrl.Location = new System.Drawing.Point(0, 0);
            this.groupCtrl.Name = "groupCtrl";
            this.groupCtrl.Size = new System.Drawing.Size(720, 347);
            this.groupCtrl.TabIndex = 0;
            this.groupCtrl.Text = "Зураг";
            // 
            // ZoomHelp
            // 
            this.ZoomHelp.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.ZoomHelp.Appearance.Options.UseBackColor = true;
            this.ZoomHelp.Controls.Add(this.zoomPicture);
            this.ZoomHelp.Location = new System.Drawing.Point(2, 22);
            this.ZoomHelp.Name = "ZoomHelp";
            this.ZoomHelp.Size = new System.Drawing.Size(531, 323);
            this.ZoomHelp.TabIndex = 1;
            this.ZoomHelp.Click += new System.EventHandler(this.ZoomHelp_Click);
            // 
            // zoomPicture
            // 
            this.zoomPicture.Location = new System.Drawing.Point(10, 3);
            this.zoomPicture.Name = "zoomPicture";
            this.zoomPicture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.zoomPicture.Size = new System.Drawing.Size(417, 316);
            this.zoomPicture.TabIndex = 0;
            // 
            // zoomTrack
            // 
            this.zoomTrack.EditValue = 25;
            this.zoomTrack.Location = new System.Drawing.Point(12, 8);
            this.zoomTrack.Name = "zoomTrack";
            this.zoomTrack.Properties.Maximum = 50;
            this.zoomTrack.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.zoomTrack.Size = new System.Drawing.Size(399, 18);
            this.zoomTrack.TabIndex = 1;
            this.zoomTrack.Value = 25;
            this.zoomTrack.ValueChanged += new System.EventHandler(this.zoomTrack_ValueChanged);
            this.zoomTrack.EditValueChanged += new System.EventHandler(this.zoomTrack_EditValueChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnRotRight);
            this.panelControl1.Controls.Add(this.btnRotLeft);
            this.panelControl1.Controls.Add(this.zoomTrack);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 347);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(720, 37);
            this.panelControl1.TabIndex = 2;
            // 
            // btnRotRight
            // 
            this.btnRotRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRotRight.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRotRight.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btnRotRight.Location = new System.Drawing.Point(657, 5);
            this.btnRotRight.Name = "btnRotRight";
            this.btnRotRight.Size = new System.Drawing.Size(26, 26);
            this.btnRotRight.TabIndex = 3;
            this.btnRotRight.Click += new System.EventHandler(this.RotRight_Click);
            // 
            // btnRotLeft
            // 
            this.btnRotLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRotLeft.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRotLeft.Location = new System.Drawing.Point(689, 5);
            this.btnRotLeft.Name = "btnRotLeft";
            this.btnRotLeft.Size = new System.Drawing.Size(26, 26);
            this.btnRotLeft.TabIndex = 2;
            this.btnRotLeft.Click += new System.EventHandler(this.RotLeft_Click);
            // 
            // FormZoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 384);
            this.Controls.Add(this.groupCtrl);
            this.Controls.Add(this.panelControl1);
            this.Name = "FormZoom";
            this.Load += new System.EventHandler(this.FormZoom_Load);
            this.Resize += new System.EventHandler(this.FormZoom_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.groupCtrl)).EndInit();
            this.groupCtrl.ResumeLayout(false);
            this.ZoomHelp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrack.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupCtrl;
        private DevExpress.XtraEditors.XtraScrollableControl ZoomHelp;
        public DevExpress.XtraEditors.PictureEdit zoomPicture;
        private DevExpress.XtraEditors.ZoomTrackBarControl zoomTrack;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnRotRight;
        private DevExpress.XtraEditors.SimpleButton btnRotLeft;
    }
}