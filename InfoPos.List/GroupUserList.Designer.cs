namespace InfoPos.List
{
    partial class GroupUserList
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
            this.ucGroupUserList = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucGroupUserList
            // 
            this.ucGroupUserList.Browsable = false;
            this.ucGroupUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGroupUserList.Location = new System.Drawing.Point(0, 0);
            this.ucGroupUserList.Name = "ucGroupUserList";
            this.ucGroupUserList.PageRows = 100;
            this.ucGroupUserList.Size = new System.Drawing.Size(724, 440);
            this.ucGroupUserList.TabIndex = 0;
            this.ucGroupUserList.VisibleFilter = false;
            this.ucGroupUserList.VisibleFind = true;
            // 
            // GroupUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 440);
            this.Controls.Add(this.ucGroupUserList);
            this.KeyPreview = true;
            this.Name = "GroupUserList";
            this.Text = "Хэрэглэгчийн бүлгийн жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GroupUserList_FormClosing);
            this.Load += new System.EventHandler(this.GroupUserList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GroupUserList_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucGroupUserList;

    }
}