namespace ISM.Template
{
    partial class ucDynamicDataPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucParameterPanel1 = new ISM.Template.ucParameterPanel();
            this.SuspendLayout();
            // 
            // ucParameterPanel1
            // 
            this.ucParameterPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucParameterPanel1.Location = new System.Drawing.Point(0, 0);
            this.ucParameterPanel1.Name = "ucParameterPanel1";
            this.ucParameterPanel1.ShowDescription = true;
            this.ucParameterPanel1.Size = new System.Drawing.Size(326, 215);
            this.ucParameterPanel1.TabIndex = 11;
            // 
            // ucDynamicDataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucParameterPanel1);
            this.Name = "ucDynamicDataPanel";
            this.Size = new System.Drawing.Size(326, 215);
            this.Load += new System.EventHandler(this.ucDynamicDataPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public ucParameterPanel ucParameterPanel1;
    }
}
