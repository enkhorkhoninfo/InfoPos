namespace InfoPos.Order
{
    partial class frmProductList
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
            this.ucProductList1 = new InfoPos.fo_panels.ucProductList();
            this.SuspendLayout();
            // 
            // ucProductList1
            // 
            this.ucProductList1.Core = null;
            this.ucProductList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucProductList1.Location = new System.Drawing.Point(0, 0);
            this.ucProductList1.Name = "ucProductList1";
            this.ucProductList1.PageRows = 20;
            this.ucProductList1.Remote = null;
            this.ucProductList1.Resource = null;
            this.ucProductList1.Size = new System.Drawing.Size(723, 410);
            this.ucProductList1.TabIndex = 0;
            this.ucProductList1.TouchKeyboard = null;
            // 
            // frmProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 410);
            this.Controls.Add(this.ucProductList1);
            this.Name = "frmProductList";
            this.Text = "Бараа үйлчилгээний жагсаалт";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public fo_panels.ucProductList ucProductList1;
    }
}