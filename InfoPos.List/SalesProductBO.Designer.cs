namespace InfoPos.List
{
    partial class SalesProductBO
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
            this.ucSalesProductBO = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucSalesProductBO
            // 
            this.ucSalesProductBO.Browsable = false;
            this.ucSalesProductBO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSalesProductBO.Location = new System.Drawing.Point(0, 0);
            this.ucSalesProductBO.Name = "ucSalesProductBO";
            this.ucSalesProductBO.PageRows = 10;
            this.ucSalesProductBO.Size = new System.Drawing.Size(722, 421);
            this.ucSalesProductBO.TabIndex = 0;
            this.ucSalesProductBO.VisibleFilter = false;
            this.ucSalesProductBO.VisibleFind = true;
            // 
            // SalesProductBO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 421);
            this.Controls.Add(this.ucSalesProductBO);
            this.KeyPreview = true;
            this.Name = "SalesProductBO";
            this.Text = "Борлуулсан бараа болон үйлчилгээний жагсаалт BO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesSearch_FormClosing);
            this.Load += new System.EventHandler(this.SalesSearch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SalesSearch_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Template.ucGridPanel ucSalesProductBO;
    }
}