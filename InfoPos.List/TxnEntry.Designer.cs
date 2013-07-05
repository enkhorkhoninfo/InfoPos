namespace InfoPos.List
{
    partial class TxnEntry
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
            this.ucTxnEntry = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucTxnEntry
            // 
            this.ucTxnEntry.Browsable = false;
            this.ucTxnEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTxnEntry.Location = new System.Drawing.Point(0, 0);
            this.ucTxnEntry.Name = "ucTxnEntry";
            this.ucTxnEntry.PageRows = 100;
            this.ucTxnEntry.Size = new System.Drawing.Size(624, 369);
            this.ucTxnEntry.TabIndex = 0;
            this.ucTxnEntry.VisibleFilter = false;
            this.ucTxnEntry.VisibleFind = true;
            this.ucTxnEntry.Load += new System.EventHandler(this.ucTxnEntry_Load);
            this.ucTxnEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxnEntry_KeyDown);
            // 
            // TxnEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 369);
            this.Controls.Add(this.ucTxnEntry);
            this.KeyPreview = true;
            this.Name = "TxnEntry";
            this.Text = "Гүйлгээний оролтуудын жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TxnEntry_FormClosing);
            this.Load += new System.EventHandler(this.TxnEntry_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxnEntry_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucTxnEntry;

    }
}