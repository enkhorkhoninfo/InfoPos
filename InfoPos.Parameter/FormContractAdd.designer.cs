namespace InfoPos.Parameter
{
    partial class FormContractAdd
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
            this.ucDynamicParameter1.Size = new System.Drawing.Size(790, 494);
            this.ucDynamicParameter1.TabIndex = 213;
            this.ucDynamicParameter1.TableTypeId = ((ulong)(0ul));
            // 
            // FormContractAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 494);
            this.Controls.Add(this.ucDynamicParameter1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(798, 521);
            this.Name = "FormContractAdd";
            this.Text = "Гэрээний нэмэлт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormContractAdd_FormClosing);
            this.Load += new System.EventHandler(this.FormContractAdd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormContractAdd_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Template.UserControls.ucDynamicParameter ucDynamicParameter1;

    }
}