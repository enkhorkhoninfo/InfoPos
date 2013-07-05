namespace InfoPos.Panels
{
    partial class frmTagClean
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSerialNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(17, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(215, 69);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Уншигч төхөөрөмж ажилд бэлэн.\r\nТа төхөөрөмжөө уншуулна уу...";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(11, 15);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new System.Drawing.Size(100, 87);
            this.pictureEdit1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Location = new System.Drawing.Point(122, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 95);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Анхаар!";
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSerialNo.EditValue = "SRNO-0001";
            this.txtSerialNo.Location = new System.Drawing.Point(153, 118);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNo.Properties.Appearance.Options.UseFont = true;
            this.txtSerialNo.Properties.AutoHeight = false;
            this.txtSerialNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSerialNo.Properties.ReadOnly = true;
            this.txtSerialNo.Size = new System.Drawing.Size(204, 28);
            this.txtSerialNo.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.labelControl2.Location = new System.Drawing.Point(45, 124);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(91, 16);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Сериал дугаар:";
            // 
            // frmTagClean
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 156);
            this.Controls.Add(this.txtSerialNo);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(378, 184);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(378, 181);
            this.Name = "frmTagClean";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Харилцагчийн ID төхөөрөмж уншуулах";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTagClean_FormClosed);
            this.Load += new System.EventHandler(this.frmTagReader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSerialNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.TextEdit txtSerialNo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}