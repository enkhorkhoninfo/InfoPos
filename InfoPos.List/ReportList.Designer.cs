namespace InfoPos.List
{
    partial class ReportList
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
            this.ucReportList = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucReportList
            // 
            this.ucReportList.Browsable = false;
            this.ucReportList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucReportList.Location = new System.Drawing.Point(0, 0);
            this.ucReportList.Name = "ucReportList";
            this.ucReportList.PageRows = 100;
            this.ucReportList.Size = new System.Drawing.Size(815, 403);
            this.ucReportList.TabIndex = 0;
            this.ucReportList.VisibleFilter = false;
            this.ucReportList.VisibleFind = true;
            this.ucReportList.EventFindPaging += new ISM.Template.ucGridPanel.delegateEventFindPaging(this.ucReportList_EventFindPaging);
            this.ucReportList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportList_KeyDown);
            // 
            // ReportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 403);
            this.Controls.Add(this.ucReportList);
            this.KeyPreview = true;
            this.Name = "ReportList";
            this.Text = "Динамик тайлангийн жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportList_FormClosing);
            this.Load += new System.EventHandler(this.ReportList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportList_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucReportList;

    }
}