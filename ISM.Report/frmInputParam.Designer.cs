namespace ISM.Report
{
    partial class frmInputParam
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
            this.btnView = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ucParameterPanel1 = new ISM.Template.ucParameterPanel();
            this.SuspendLayout();
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(336, 5);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(92, 26);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "Харах";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(336, 34);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Буцах";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucParameterPanel1
            // 
            this.ucParameterPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucParameterPanel1.Location = new System.Drawing.Point(5, 5);
            this.ucParameterPanel1.Name = "ucParameterPanel1";
            this.ucParameterPanel1.ShowDescription = true;
            this.ucParameterPanel1.Size = new System.Drawing.Size(325, 338);
            this.ucParameterPanel1.TabIndex = 0;
            // 
            // frmInputParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 347);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.ucParameterPanel1);
            this.KeyPreview = true;
            this.Name = "frmInputParam";
            this.Text = "frmInputParam";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInputParam_FormClosed);
            this.Load += new System.EventHandler(this.frmInputParam_Load);
            this.Click += new System.EventHandler(this.frmInputParam_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInputParam_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Template.ucParameterPanel ucParameterPanel1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnCancel;
    }
}