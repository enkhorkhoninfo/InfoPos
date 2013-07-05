namespace InfoPos.Parameter
{
    partial class FormPAWeather
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
            this.txtWeatherId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtIcon = new DevExpress.XtraEditors.PictureEdit();
            this.txtDesc = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeatherId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtDesc);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.txtIcon);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtWeatherId);
            this.groupControl1.Size = new System.Drawing.Size(358, 327);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 349);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 349);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 349);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 349);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 349);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(630, 331);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(263, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 331);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(263, 331);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(268, 0);
            this.panelControl3.Size = new System.Drawing.Size(362, 331);
            // 
            // txtWeatherId
            // 
            this.txtWeatherId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWeatherId.EditValue = "";
            this.txtWeatherId.Location = new System.Drawing.Point(156, 33);
            this.txtWeatherId.MinimumSize = new System.Drawing.Size(185, 20);
            this.txtWeatherId.Name = "txtWeatherId";
            this.txtWeatherId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWeatherId.Size = new System.Drawing.Size(185, 20);
            this.txtWeatherId.TabIndex = 0;
            this.txtWeatherId.ToolTipTitle = "Цаг агаарын төрлийн код оруулна уу.";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(132, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Цаг агаарын төрлийн код";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Тайлбар";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 79);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(89, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Дүрслэгдэх зураг";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.simpleButton1.Location = new System.Drawing.Point(255, 82);
            this.simpleButton1.MaximumSize = new System.Drawing.Size(86, 22);
            this.simpleButton1.MinimumSize = new System.Drawing.Size(86, 22);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(86, 22);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Зураг сонгох";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtIcon
            // 
            this.txtIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIcon.Location = new System.Drawing.Point(156, 82);
            this.txtIcon.MaximumSize = new System.Drawing.Size(93, 65);
            this.txtIcon.MinimumSize = new System.Drawing.Size(93, 65);
            this.txtIcon.Name = "txtIcon";
            this.txtIcon.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.txtIcon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.txtIcon.Size = new System.Drawing.Size(93, 65);
            this.txtIcon.TabIndex = 2;
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.EditValue = "";
            this.txtDesc.Location = new System.Drawing.Point(156, 56);
            this.txtDesc.MinimumSize = new System.Drawing.Size(185, 20);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDesc.Properties.MaxLength = 200;
            this.txtDesc.Size = new System.Drawing.Size(185, 20);
            this.txtDesc.TabIndex = 1;
            this.txtDesc.ToolTipTitle = "Тайлбар бичнэ үү.";
            // 
            // FormPAWeather
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 382);
            this.MaximumSize = new System.Drawing.Size(1024, 720);
            this.MinimumSize = new System.Drawing.Size(670, 420);
            this.Name = "FormPAWeather";
            this.Text = "Цаг агаарын төрлийн бүртгэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPAWeather_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtWeatherId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtWeatherId;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PictureEdit txtIcon;
        private DevExpress.XtraEditors.TextEdit txtDesc;
    }
}