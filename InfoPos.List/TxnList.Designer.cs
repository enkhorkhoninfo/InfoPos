namespace InfoPos.List
{
    partial class TxnList
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
            this.ucTxn = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucTxn
            // 
            this.ucTxn.Browsable = false;
            this.ucTxn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTxn.Location = new System.Drawing.Point(0, 0);
            this.ucTxn.Name = "ucTxn";
            this.ucTxn.PageRows = 100;
            this.ucTxn.Size = new System.Drawing.Size(613, 369);
            this.ucTxn.TabIndex = 0;
            this.ucTxn.VisibleFilter = false;
            this.ucTxn.VisibleFind = true;
            this.ucTxn.Load += new System.EventHandler(this.ucTxn_Load);
            this.ucTxn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxnList_KeyDown);
            // 
            // TxnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 369);
            this.Controls.Add(this.ucTxn);
            this.KeyPreview = true;
            this.Name = "TxnList";
            this.Text = "Гүйлгээний кодын жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TxnList_FormClosing);
            this.Load += new System.EventHandler(this.TxnList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxnList_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucTxn;

    }
}