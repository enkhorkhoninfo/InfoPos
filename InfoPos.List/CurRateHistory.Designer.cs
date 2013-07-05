namespace InfoPos.List
{
    partial class CurRateHistory
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
            this.ucCurRateHistory = new ISM.Template.ucGridPanel();
            this.SuspendLayout();
            // 
            // ucCurRateHistory
            // 
            this.ucCurRateHistory.Browsable = false;
            this.ucCurRateHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCurRateHistory.Location = new System.Drawing.Point(0, 0);
            this.ucCurRateHistory.Name = "ucCurRateHistory";
            this.ucCurRateHistory.PageRows = 100;
            this.ucCurRateHistory.Size = new System.Drawing.Size(600, 393);
            this.ucCurRateHistory.TabIndex = 0;
            this.ucCurRateHistory.VisibleFilter = false;
            this.ucCurRateHistory.VisibleFind = true;
            this.ucCurRateHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ucCurRateHistory_KeyDown);
            // 
            // CurRateHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 393);
            this.Controls.Add(this.ucCurRateHistory);
            this.KeyPreview = true;
            this.Name = "CurRateHistory";
            this.Text = "Ханшийн түүх харах";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CurRateHistory_FormClosing);
            this.Load += new System.EventHandler(this.BacAccount_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurRateHistory_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucCurRateHistory;

    }
}