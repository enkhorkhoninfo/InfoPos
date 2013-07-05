namespace InfoPos.List
{
    partial class InventoryList
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
            this.ucInventoryList = new ISM.Template.ucGridPanel();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // ucInventoryList
            // 
            this.ucInventoryList.Browsable = false;
            this.ucInventoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInventoryList.Location = new System.Drawing.Point(0, 0);
            this.ucInventoryList.Name = "ucInventoryList";
            this.ucInventoryList.PageRows = 100;
            this.ucInventoryList.Size = new System.Drawing.Size(992, 435);
            this.ucInventoryList.TabIndex = 0;
            this.ucInventoryList.VisibleFilter = false;
            this.ucInventoryList.VisibleFind = true;
            this.ucInventoryList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InventoryList_KeyDown);
            this.ucInventoryList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InventoryList_KeyUp);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl4.Location = new System.Drawing.Point(772, 408);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Идэвхгүй";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl3.Location = new System.Drawing.Point(596, 408);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 13);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "Идэвхтэй";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl2.Location = new System.Drawing.Point(684, 408);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 13);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "labelControl2";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl1.Location = new System.Drawing.Point(517, 408);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 15);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "labelControl1";
            // 
            // InventoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 435);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.ucInventoryList);
            this.KeyPreview = true;
            this.Name = "InventoryList";
            this.Text = "Бараа материалын жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryList_FormClosing);
            this.Load += new System.EventHandler(this.InventoryList_Load);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.InventoryList_DragEnter);
            this.Enter += new System.EventHandler(this.InventoryList_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InventoryList_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InventoryList_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ISM.Template.ucGridPanel ucInventoryList;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}