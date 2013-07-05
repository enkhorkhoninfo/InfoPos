namespace InfoPos.List
{
    partial class SalesMoreBO
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
            this.ucSalesSearch = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucSalesSearch
            // 
            this.ucSalesSearch.Browsable = false;
            this.ucSalesSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSalesSearch.Location = new System.Drawing.Point(0, 0);
            this.ucSalesSearch.Name = "ucSalesSearch";
            this.ucSalesSearch.PageRows = 10;
            this.ucSalesSearch.Size = new System.Drawing.Size(722, 421);
            this.ucSalesSearch.TabIndex = 0;
            this.ucSalesSearch.VisibleFilter = false;
            this.ucSalesSearch.VisibleFind = true;
            // 
            // SalesSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 421);
            this.Controls.Add(this.ucSalesSearch);
            this.KeyPreview = true;
            this.Name = "SalesSearch";
            this.Text = "Борлуулалтын дэлгэрэнгүй жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesSearch_FormClosing);
            this.Load += new System.EventHandler(this.SalesSearch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SalesSearch_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Template.ucGridPanel ucSalesSearch;
    }
}