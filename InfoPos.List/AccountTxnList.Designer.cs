namespace InfoPos.List
{
    partial class AccountTxnList
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
            this.ucAccountTxnList = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucAccountTxnList
            // 
            this.ucAccountTxnList.Browsable = false;
            this.ucAccountTxnList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAccountTxnList.Location = new System.Drawing.Point(0, 0);
            this.ucAccountTxnList.Name = "ucAccountTxnList";
            this.ucAccountTxnList.PageRows = 100;
            this.ucAccountTxnList.Size = new System.Drawing.Size(630, 406);
            this.ucAccountTxnList.TabIndex = 0;
            this.ucAccountTxnList.VisibleFilter = false;
            this.ucAccountTxnList.VisibleFind = true;
            this.ucAccountTxnList.Load += new System.EventHandler(this.ucAccountTxnList_Load);
            this.ucAccountTxnList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ucAccountTxnList_KeyDown);
            // 
            // AccountTxnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 406);
            this.Controls.Add(this.ucAccountTxnList);
            this.KeyPreview = true;
            this.Name = "AccountTxnList";
            this.Text = "Дансны гүйлгээ жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountTxnList_FormClosing);
            this.Load += new System.EventHandler(this.AccountTxnList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AccountTxnList_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucAccountTxnList;
    }
}