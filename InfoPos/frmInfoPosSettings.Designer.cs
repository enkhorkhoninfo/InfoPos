namespace InfoPos
{
    partial class frmInfoPosSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfoPosSettings));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.btnTagReaderInit = new DevExpress.XtraEditors.SimpleButton();
            this.btnParamUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.pbConfPicture = new System.Windows.Forms.PictureBox();
            this.IsTouch = new DevExpress.XtraEditors.CheckEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.picPos = new System.Windows.Forms.PictureBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbConfPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsTouch.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPos)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(1, 1);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(454, 203);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.btnTagReaderInit);
            this.xtraTabPage1.Controls.Add(this.btnParamUpdate);
            this.xtraTabPage1.Controls.Add(this.pbConfPicture);
            this.xtraTabPage1.Controls.Add(this.IsTouch);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(448, 176);
            this.xtraTabPage1.Text = "Тохиргоо";
            // 
            // btnTagReaderInit
            // 
            this.btnTagReaderInit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTagReaderInit.Appearance.Options.UseFont = true;
            this.btnTagReaderInit.Location = new System.Drawing.Point(168, 109);
            this.btnTagReaderInit.Name = "btnTagReaderInit";
            this.btnTagReaderInit.Size = new System.Drawing.Size(262, 40);
            this.btnTagReaderInit.TabIndex = 8;
            this.btnTagReaderInit.Text = "Таг уншигч ачааллах";
            this.btnTagReaderInit.Click += new System.EventHandler(this.btnTagReaderInit_Click);
            // 
            // btnParamUpdate
            // 
            this.btnParamUpdate.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParamUpdate.Appearance.Options.UseFont = true;
            this.btnParamUpdate.Location = new System.Drawing.Point(168, 63);
            this.btnParamUpdate.Name = "btnParamUpdate";
            this.btnParamUpdate.Size = new System.Drawing.Size(262, 40);
            this.btnParamUpdate.TabIndex = 7;
            this.btnParamUpdate.Text = "Параметр шинэчлэх";
            this.btnParamUpdate.Click += new System.EventHandler(this.btnParamUpdate_Click);
            // 
            // pbConfPicture
            // 
            this.pbConfPicture.Image = ((System.Drawing.Image)(resources.GetObject("pbConfPicture.Image")));
            this.pbConfPicture.Location = new System.Drawing.Point(17, 35);
            this.pbConfPicture.Name = "pbConfPicture";
            this.pbConfPicture.Size = new System.Drawing.Size(117, 110);
            this.pbConfPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbConfPicture.TabIndex = 6;
            this.pbConfPicture.TabStop = false;
            // 
            // IsTouch
            // 
            this.IsTouch.EditValue = true;
            this.IsTouch.Location = new System.Drawing.Point(166, 33);
            this.IsTouch.Name = "IsTouch";
            this.IsTouch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsTouch.Properties.Appearance.Options.UseFont = true;
            this.IsTouch.Properties.Appearance.Options.UseTextOptions = true;
            this.IsTouch.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.IsTouch.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.IsTouch.Properties.Caption = "Виртуал товчлуур идэвхжүүлэх";
            this.IsTouch.Properties.FullFocusRect = true;
            this.IsTouch.Size = new System.Drawing.Size(277, 24);
            this.IsTouch.TabIndex = 1;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.picPos);
            this.xtraTabPage2.Controls.Add(this.labelControl4);
            this.xtraTabPage2.Controls.Add(this.labelControl3);
            this.xtraTabPage2.Controls.Add(this.labelControl2);
            this.xtraTabPage2.Controls.Add(this.labelControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(448, 176);
            this.xtraTabPage2.Text = "Програмын тухай";
            // 
            // picPos
            // 
            this.picPos.Image = ((System.Drawing.Image)(resources.GetObject("picPos.Image")));
            this.picPos.Location = new System.Drawing.Point(10, 12);
            this.picPos.Name = "picPos";
            this.picPos.Size = new System.Drawing.Size(161, 151);
            this.picPos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPos.TabIndex = 10;
            this.picPos.TabStop = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(177, 64);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(77, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "ПОС-ын систем";
            this.labelControl4.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(177, 150);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(171, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Copyright (c): 2012 Инфософт ХХК";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(177, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(94, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Огноо: 2012.08.31";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(177, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Хувилбар: 1.0.0";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(314, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(137, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Болих";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(160, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(137, 40);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmInfoPosSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(457, 256);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.xtraTabControl1);
            this.MaximumSize = new System.Drawing.Size(473, 294);
            this.MinimumSize = new System.Drawing.Size(473, 294);
            this.Name = "frmInfoPosSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Цэс";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbConfPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsTouch.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.CheckEdit IsTouch;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.PictureBox pbConfPicture;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox picPos;
        private DevExpress.XtraEditors.SimpleButton btnParamUpdate;
        private DevExpress.XtraEditors.SimpleButton btnTagReaderInit;
    }
}