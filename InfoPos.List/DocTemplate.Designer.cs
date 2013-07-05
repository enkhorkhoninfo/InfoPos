namespace InfoPos.List
{
    partial class DocTemplate
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
            this.ucDocTemplate = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucDocTemplate
            // 
            this.ucDocTemplate.Browsable = false;
            this.ucDocTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDocTemplate.Location = new System.Drawing.Point(0, 0);
            this.ucDocTemplate.Name = "ucDocTemplate";
            this.ucDocTemplate.PageRows = 100;
            this.ucDocTemplate.Size = new System.Drawing.Size(569, 424);
            this.ucDocTemplate.TabIndex = 0;
            this.ucDocTemplate.VisibleFilter = false;
            this.ucDocTemplate.VisibleFind = true;
            this.ucDocTemplate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DocTemplate_KeyDown);
            // 
            // DocTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 424);
            this.Controls.Add(this.ucDocTemplate);
            this.KeyPreview = true;
            this.Name = "DocTemplate";
            this.Text = "Документ загварын жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocTemplate_FormClosing);
            this.Load += new System.EventHandler(this.DocTemplate_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DocTemplate_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucDocTemplate;

    }
}