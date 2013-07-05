namespace InfoPos.Admin
{
    partial class frmParameterUpdate
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
            this.grdParameter = new DevExpress.XtraGrid.GridControl();
            this.gvwParameter = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.progressbar = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.grdParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressbar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdParameter
            // 
            this.grdParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdParameter.Location = new System.Drawing.Point(2, 38);
            this.grdParameter.MainView = this.gvwParameter;
            this.grdParameter.Name = "grdParameter";
            this.grdParameter.Size = new System.Drawing.Size(419, 410);
            this.grdParameter.TabIndex = 0;
            this.grdParameter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwParameter});
            // 
            // gvwParameter
            // 
            this.gvwParameter.GridControl = this.grdParameter;
            this.gvwParameter.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwParameter.Name = "gvwParameter";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(248, 37);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(82, 22);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Шинэчлэх";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(336, 37);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 22);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Гарах";
            this.btnClose.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdParameter);
            this.groupControl1.Controls.Add(this.panel2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(423, 450);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Параметрүүд";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 17);
            this.panel2.TabIndex = 13;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Location = new System.Drawing.Point(4, 0);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(103, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Бүгдийг сонгох";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.progressbar);
            this.panelControl1.Controls.Add(this.btnUpdate);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 450);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(423, 66);
            this.panelControl1.TabIndex = 4;
            // 
            // progressbar
            // 
            this.progressbar.Location = new System.Drawing.Point(6, 7);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(412, 23);
            this.progressbar.TabIndex = 3;
            // 
            // frmParameterUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 516);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(431, 543);
            this.Name = "frmParameterUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметр шинэчлэх";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmParameterUpdate_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressbar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdParameter;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwParameter;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox2;
        public DevExpress.XtraEditors.ProgressBarControl progressbar;

    }
}