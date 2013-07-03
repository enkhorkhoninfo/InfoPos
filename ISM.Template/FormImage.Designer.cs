namespace ISM.Template
{
    partial class FormImage
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
            this.picMain = new DevExpress.XtraEditors.PictureEdit();
            this.btnTake = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnScan = new DevExpress.XtraEditors.SimpleButton();
            this.btnCam = new DevExpress.XtraEditors.SimpleButton();
            this.btnFile = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnRotRight = new DevExpress.XtraEditors.SimpleButton();
            this.btnRotLeft = new DevExpress.XtraEditors.SimpleButton();
            this.trkZoom = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.scrZoom = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.picMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom.Properties)).BeginInit();
            this.scrZoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picMain.Location = new System.Drawing.Point(3, 4);
            this.picMain.Name = "picMain";
            this.picMain.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picMain.Size = new System.Drawing.Size(392, 295);
            this.picMain.TabIndex = 0;
            // 
            // btnTake
            // 
            this.btnTake.Enabled = false;
            this.btnTake.Location = new System.Drawing.Point(5, 281);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(34, 34);
            this.btnTake.TabIndex = 4;
            this.btnTake.ToolTip = "Камерийн харуулж буй дүрсийг буулгах.";
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(5, 115);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(34, 34);
            this.btnSave.TabIndex = 6;
            this.btnSave.ToolTip = "Зургийг файл болгож хадгалах.";
            this.btnSave.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(5, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(34, 34);
            this.btnOK.TabIndex = 7;
            this.btnOK.ToolTip = "Зургийг сонгох.";
            this.btnOK.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(5, 207);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(34, 34);
            this.btnScan.TabIndex = 3;
            this.btnScan.ToolTip = "Scanner төхөөрөмжөөс зураг татаж оруулах.";
            this.btnScan.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnCam
            // 
            this.btnCam.Location = new System.Drawing.Point(5, 244);
            this.btnCam.Name = "btnCam";
            this.btnCam.Size = new System.Drawing.Size(34, 34);
            this.btnCam.TabIndex = 2;
            this.btnCam.ToolTip = "Камераас Real-Time дүрс харуулах.";
            this.btnCam.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnCam.Click += new System.EventHandler(this.btnCam_Click);
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(5, 170);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(34, 34);
            this.btnFile.TabIndex = 1;
            this.btnFile.ToolTip = "Диск төхөөрөмжөөс зургийн файл татаж оруулах.";
            this.btnFile.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnFile.ToolTipTitle = "Мэдээлэл";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnRotRight);
            this.panelControl1.Controls.Add(this.btnRotLeft);
            this.panelControl1.Controls.Add(this.btnFile);
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Controls.Add(this.btnCam);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnScan);
            this.panelControl1.Controls.Add(this.btnTake);
            this.panelControl1.Location = new System.Drawing.Point(406, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(45, 322);
            this.panelControl1.TabIndex = 8;
            // 
            // btnRotRight
            // 
            this.btnRotRight.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRotRight.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btnRotRight.Location = new System.Drawing.Point(5, 41);
            this.btnRotRight.Name = "btnRotRight";
            this.btnRotRight.Size = new System.Drawing.Size(34, 34);
            this.btnRotRight.TabIndex = 11;
            this.btnRotRight.ToolTip = "Зургийг баруун эргүүлэх.";
            this.btnRotRight.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnRotRight.Click += new System.EventHandler(this.btnRotRight_Click);
            // 
            // btnRotLeft
            // 
            this.btnRotLeft.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRotLeft.Location = new System.Drawing.Point(5, 78);
            this.btnRotLeft.Name = "btnRotLeft";
            this.btnRotLeft.Size = new System.Drawing.Size(34, 34);
            this.btnRotLeft.TabIndex = 10;
            this.btnRotLeft.ToolTip = "Зургийг зүүн эргүүлэх";
            this.btnRotLeft.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.btnRotLeft.Click += new System.EventHandler(this.btnRotLeft_Click);
            // 
            // trkZoom
            // 
            this.trkZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trkZoom.EditValue = 25;
            this.trkZoom.Location = new System.Drawing.Point(4, 310);
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.Properties.Maximum = 50;
            this.trkZoom.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.trkZoom.Size = new System.Drawing.Size(399, 18);
            this.trkZoom.TabIndex = 9;
            this.trkZoom.Value = 25;
            // 
            // scrZoom
            // 
            this.scrZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scrZoom.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.scrZoom.Appearance.Options.UseBackColor = true;
            this.scrZoom.Controls.Add(this.picMain);
            this.scrZoom.Location = new System.Drawing.Point(5, 5);
            this.scrZoom.Name = "scrZoom";
            this.scrZoom.Size = new System.Drawing.Size(398, 302);
            this.scrZoom.TabIndex = 10;
            // 
            // FormImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 330);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.scrZoom);
            this.Controls.Add(this.trkZoom);
            this.MinimumSize = new System.Drawing.Size(471, 368);
            this.Name = "FormImage";
            this.Load += new System.EventHandler(this.FormImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            this.scrZoom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnFile;
        private DevExpress.XtraEditors.SimpleButton btnCam;
        private DevExpress.XtraEditors.SimpleButton btnScan;
        private DevExpress.XtraEditors.SimpleButton btnTake;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PictureEdit picMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ZoomTrackBarControl trkZoom;
        private DevExpress.XtraEditors.SimpleButton btnRotRight;
        private DevExpress.XtraEditors.SimpleButton btnRotLeft;
        private DevExpress.XtraEditors.XtraScrollableControl scrZoom;

    }
}