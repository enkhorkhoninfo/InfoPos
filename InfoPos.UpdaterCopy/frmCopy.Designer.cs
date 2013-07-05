namespace InfoPos.UpdaterCopy
{
    partial class frmCopy
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.mmolog = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmolog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(18, 116);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Visible = false;
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(12, 413);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(371, 28);
            this.progressBarControl1.TabIndex = 1;
            // 
            // mmolog
            // 
            this.mmolog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mmolog.EditValue = "";
            this.mmolog.Location = new System.Drawing.Point(12, 25);
            this.mmolog.Name = "mmolog";
            this.mmolog.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.mmolog.Size = new System.Drawing.Size(371, 382);
            this.mmolog.TabIndex = 2;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(308, 445);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Гарах";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.mmolog);
            this.groupControl1.Controls.Add(this.dateTimePicker1);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.progressBarControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(395, 481);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Шинэчлэгдсэн файлууд";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // frmCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 481);
            this.ControlBox = false;
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(401, 505);
            this.MinimumSize = new System.Drawing.Size(401, 334);
            this.Name = "frmCopy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Хуулах процесс";
            this.Load += new System.EventHandler(this.frmCopy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmolog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.MemoEdit mmolog;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl1;

    }
}