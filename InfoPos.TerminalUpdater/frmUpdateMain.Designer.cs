namespace InfoPos.Updater
{
    partial class frmUpdateMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateMain));
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSettings = new DevExpress.XtraEditors.SimpleButton();
            this.mmoreadme = new DevExpress.XtraEditors.MemoEdit();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.mmoreadme.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(321, 278);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(90, 26);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Шинэчлэх";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(417, 278);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Гарах";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 278);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(96, 26);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "Тохиргоо";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // mmoreadme
            // 
            this.mmoreadme.Location = new System.Drawing.Point(12, 12);
            this.mmoreadme.Name = "mmoreadme";
            this.mmoreadme.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.mmoreadme.Properties.Appearance.Options.UseFont = true;
            this.mmoreadme.Size = new System.Drawing.Size(495, 226);
            this.mmoreadme.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(206, 283);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Зөвшөөрч байна";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(12, 244);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Properties.Stopped = true;
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(495, 27);
            this.marqueeProgressBarControl1.TabIndex = 7;
            // 
            // frmUpdateMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 312);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.mmoreadme);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(516, 339);
            this.MinimumSize = new System.Drawing.Size(516, 339);
            this.Name = "frmUpdateMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Терминал шинэчлэх";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUpdateMain_FormClosing);
            this.Load += new System.EventHandler(this.frmUpdateMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mmoreadme.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSettings;
        private DevExpress.XtraEditors.MemoEdit mmoreadme;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;

    }
}