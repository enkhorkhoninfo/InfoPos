namespace InfoPos.List
{
    partial class EmployeeList
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
            this.ucEmployeeResourceFa = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucEmployeeResourceFa
            // 
            this.ucEmployeeResourceFa.Browsable = false;
            this.ucEmployeeResourceFa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEmployeeResourceFa.Location = new System.Drawing.Point(0, 0);
            this.ucEmployeeResourceFa.Name = "ucEmployeeResourceFa";
            this.ucEmployeeResourceFa.PageRows = 100;
            this.ucEmployeeResourceFa.Size = new System.Drawing.Size(824, 432);
            this.ucEmployeeResourceFa.TabIndex = 0;
            this.ucEmployeeResourceFa.VisibleFilter = false;
            this.ucEmployeeResourceFa.VisibleFind = true;
            this.ucEmployeeResourceFa.Load += new System.EventHandler(this.EmployeeList_Load);
            this.ucEmployeeResourceFa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EmployeeList_KeyDown);
            // 
            // EmployeeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 432);
            this.Controls.Add(this.ucEmployeeResourceFa);
            this.KeyPreview = true;
            this.Name = "EmployeeList";
            this.Text = "Ажилчдын нэрсийн жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EmployeeList_FormClosing);
            this.Load += new System.EventHandler(this.EmployeeList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EmployeeList_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucEmployeeResourceFa;

    }
}