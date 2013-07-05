namespace InfoPos.bo_Reports
{
    partial class frmInputParam
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
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.ucExcelReports = new ISM.Template.ucParameterPanel();
            this.SuspendLayout();
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(320, 6);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 24);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "Харах";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(320, 36);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 24);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Буцах";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ucExcelReports
            // 
            this.ucExcelReports.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucExcelReports.Location = new System.Drawing.Point(0, 0);
            this.ucExcelReports.Name = "ucExcelReports";
            this.ucExcelReports.ShowDescription = true;
            this.ucExcelReports.Size = new System.Drawing.Size(314, 253);
            this.ucExcelReports.TabIndex = 0;
            // 
            // frmInputParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 253);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.ucExcelReports);
            this.MaximumSize = new System.Drawing.Size(408, 280);
            this.MinimumSize = new System.Drawing.Size(408, 280);
            this.Name = "frmInputParam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmInputParam";
            this.Load += new System.EventHandler(this.frmInputParam_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Template.ucParameterPanel ucExcelReports;
        private DevExpress.XtraEditors.SimpleButton btnView;
        private DevExpress.XtraEditors.SimpleButton btnExit;
    }
}