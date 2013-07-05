namespace InfoPos.List
{
    partial class ContactList
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
            this.ucContactList = new ISM.Template.ucGridPanel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // ucContactList
            // 
            this.ucContactList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucContactList.Browsable = false;
            this.ucContactList.Location = new System.Drawing.Point(0, 0);
            this.ucContactList.Name = "ucContactList";
            this.ucContactList.PageRows = 100;
            this.ucContactList.Size = new System.Drawing.Size(764, 425);
            this.ucContactList.TabIndex = 0;
            this.ucContactList.VisibleFilter = false;
            this.ucContactList.VisibleFind = true;
            this.ucContactList.Load += new System.EventHandler(this.ucContactList_Load_1);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(12, 429);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(101, 28);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Лавлагаа";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton2.Location = new System.Drawing.Point(119, 429);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(101, 28);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "Issue";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // ContactList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 462);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ucContactList);
            this.KeyPreview = true;
            this.Name = "ContactList";
            this.Text = "Холбоо барьсан харилцагчийн жагсаалт авах";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContactList_FormClosing);
            this.Load += new System.EventHandler(this.ContactList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactList_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public ISM.Template.ucGridPanel ucContactList;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;

    }
}