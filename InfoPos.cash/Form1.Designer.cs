namespace InfoPos.cash
{
    partial class Form1
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
            this.touchButtonGroup1 = new ISM.Touch.TouchButtonGroup();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // touchButtonGroup1
            // 
            this.touchButtonGroup1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.touchButtonGroup1.BurronBackColor = System.Drawing.Color.SteelBlue;
            this.touchButtonGroup1.BurronForeColor = System.Drawing.SystemColors.ButtonFace;
            this.touchButtonGroup1.ButtonTrackFont = new System.Drawing.Font("Tahoma", 10F);
            this.touchButtonGroup1.ButtonTrackHeigth = 26;
            this.touchButtonGroup1.ButtonTrackText = "";
            this.touchButtonGroup1.ButtonTrackVisible = true;
            this.touchButtonGroup1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.touchButtonGroup1.Location = new System.Drawing.Point(32, 60);
            this.touchButtonGroup1.Margin = new System.Windows.Forms.Padding(5);
            this.touchButtonGroup1.Name = "touchButtonGroup1";
            this.touchButtonGroup1.ParentMDI = null;
            this.touchButtonGroup1.Size = new System.Drawing.Size(298, 130);
            this.touchButtonGroup1.TabIndex = 3;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(190, 12);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(140, 40);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.TabStop = false;
            this.simpleButton2.Text = "Form1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(32, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(141, 40);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.TabStop = false;
            this.simpleButton1.Text = "Form1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 306);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.touchButtonGroup1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Touch.TouchButtonGroup touchButtonGroup1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}

