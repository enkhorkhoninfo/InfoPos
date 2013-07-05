namespace InfoPos.List
{
    partial class SalesPayBO
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
            this.ucSalesPayBO = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucSalesPayBO
            // 
            this.ucSalesPayBO.Browsable = false;
            this.ucSalesPayBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSalesPayBO.Location = new System.Drawing.Point(0, 0);
            this.ucSalesPayBO.Name = "ucSalesPayBO";
            this.ucSalesPayBO.PageRows = 10;
            this.ucSalesPayBO.Size = new System.Drawing.Size(722, 421);
            this.ucSalesPayBO.TabIndex = 0;
            this.ucSalesPayBO.VisibleFilter = false;
            this.ucSalesPayBO.VisibleFind = true;
            // 
            // SalesPayBO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 421);
            this.Controls.Add(this.ucSalesPayBO);
            this.KeyPreview = true;
            this.Name = "SalesPayBO";
            this.Text = "Төлбөрийн жагсаалт BO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesSearch_FormClosing);
            this.Load += new System.EventHandler(this.SalesSearch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SalesSearch_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Template.ucGridPanel ucSalesPayBO;
    }
}