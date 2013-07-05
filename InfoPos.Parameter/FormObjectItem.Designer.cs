namespace InfoPos.Parameter
{
    partial class FormObjectItem
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
            this.ucDynamicParameter1 = new ISM.Template.UserControls.ucDynamicParameter();
            this.SuspendLayout();
            // 
            // ucDynamicParameter1
            // 
            this.ucDynamicParameter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDynamicParameter1.Location = new System.Drawing.Point(0, 0);
            this.ucDynamicParameter1.Name = "ucDynamicParameter1";
            this.ucDynamicParameter1.Size = new System.Drawing.Size(834, 509);
            this.ucDynamicParameter1.TabIndex = 0;
            this.ucDynamicParameter1.TableTypeId = ((ulong)(0ul));
            this.ucDynamicParameter1.Load += new System.EventHandler(this.ucDynamicParameter1_Load);
            // 
            // FormObjectItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 509);
            this.Controls.Add(this.ucDynamicParameter1);
            this.KeyPreview = true;
            this.Name = "FormObjectItem";
            this.Text = "Зүйлийн үзүүлэлтүүдийн бүртгэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormObjectItem_FormClosing);
            this.Load += new System.EventHandler(this.FormObjectItem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormObjectItem_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Template.UserControls.ucDynamicParameter ucDynamicParameter1;
    }
}