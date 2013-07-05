namespace InfoPos.List
{
    partial class PackMainList
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
            this.ucPackMain = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucPackMain
            // 
            this.ucPackMain.Browsable = false;
            this.ucPackMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPackMain.Location = new System.Drawing.Point(0, 0);
            this.ucPackMain.Name = "ucPackMain";
            this.ucPackMain.PageRows = 100;
            this.ucPackMain.Size = new System.Drawing.Size(824, 432);
            this.ucPackMain.TabIndex = 0;
            this.ucPackMain.VisibleFilter = false;
            this.ucPackMain.VisibleFind = true;
            this.ucPackMain.Load += new System.EventHandler(this.EmployeeList_Load);
            this.ucPackMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EmployeeList_KeyDown);
            // 
            // PackMainList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 432);
            this.Controls.Add(this.ucPackMain);
            this.KeyPreview = true;
            this.Name = "PackMainList";
            this.Text = "Бүтээгдэхүүний багцын жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EmployeeList_FormClosing);
            this.Load += new System.EventHandler(this.EmployeeList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EmployeeList_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucPackMain;

    }
}