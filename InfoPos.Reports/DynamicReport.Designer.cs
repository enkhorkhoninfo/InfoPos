namespace InfoPos.Reports
{
    partial class DynamicReport
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
            this.ucDynamicReport = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucDynamicReport
            // 
            this.ucDynamicReport.Browsable = false;
            this.ucDynamicReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDynamicReport.Location = new System.Drawing.Point(0, 0);
            this.ucDynamicReport.Name = "ucDynamicReport";
            this.ucDynamicReport.PageRows = 100;
            this.ucDynamicReport.Size = new System.Drawing.Size(564, 378);
            this.ucDynamicReport.TabIndex = 0;
            this.ucDynamicReport.VisibleFilter = false;
            this.ucDynamicReport.VisibleFind = true;
            this.ucDynamicReport.Load += new System.EventHandler(this.ucDynamicReport_Load);
            // 
            // DynamicReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 378);
            this.Controls.Add(this.ucDynamicReport);
            this.KeyPreview = true;
            this.Name = "DynamicReport";
            this.Text = "Динамик тайлан";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DynamicReport_FormClosing);
            this.Load += new System.EventHandler(this.DynamicReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicReport_KeyDown);
            this.ResumeLayout(false);
            //

        }

        #endregion

        public ISM.Template.ucGridPanel ucDynamicReport;
    }
}