namespace InfoPos.Admin
{
    partial class frmDashBSettings
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.grdSettings = new DevExpress.XtraGrid.GridControl();
            this.gvwSettings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clrOddRow2 = new DevExpress.XtraEditors.ColorEdit();
            this.clrEventRow2 = new DevExpress.XtraEditors.ColorEdit();
            this.clrSelectedRow2 = new DevExpress.XtraEditors.ColorEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clrSelectedRow1 = new DevExpress.XtraEditors.ColorEdit();
            this.clrOddRow1 = new DevExpress.XtraEditors.ColorEdit();
            this.clrEventRow1 = new DevExpress.XtraEditors.ColorEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clrOddRow2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrEventRow2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrSelectedRow2.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clrSelectedRow1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrOddRow1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrEventRow1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdSettings);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(360, 349);
            this.panelControl1.TabIndex = 0;
            // 
            // grdSettings
            // 
            this.grdSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSettings.Location = new System.Drawing.Point(2, 2);
            this.grdSettings.MainView = this.gvwSettings;
            this.grdSettings.Name = "grdSettings";
            this.grdSettings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ColorEdit1});
            this.grdSettings.Size = new System.Drawing.Size(356, 345);
            this.grdSettings.TabIndex = 0;
            this.grdSettings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwSettings});
            // 
            // gvwSettings
            // 
            this.gvwSettings.GridControl = this.grdSettings;
            this.gvwSettings.Name = "gvwSettings";
            // 
            // ColorEdit1
            // 
            this.ColorEdit1.AutoHeight = false;
            this.ColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ColorEdit1.Name = "ColorEdit1";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(625, 356);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 26);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(717, 356);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(86, 26);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Гарах";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(362, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(443, 349);
            this.panelControl2.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupBox2);
            this.groupControl1.Controls.Add(this.groupBox1);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(439, 345);
            this.groupControl1.TabIndex = 15;
            this.groupControl1.Text = "Өгөгдөл";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clrOddRow2);
            this.groupBox2.Controls.Add(this.clrEventRow2);
            this.groupBox2.Controls.Add(this.clrSelectedRow2);
            this.groupBox2.Location = new System.Drawing.Point(290, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(137, 100);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Өнгө 2";
            // 
            // clrOddRow2
            // 
            this.clrOddRow2.EditValue = System.Drawing.Color.Empty;
            this.clrOddRow2.Location = new System.Drawing.Point(6, 71);
            this.clrOddRow2.Name = "clrOddRow2";
            this.clrOddRow2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.clrOddRow2.Properties.ShowCustomColors = false;
            this.clrOddRow2.Size = new System.Drawing.Size(116, 20);
            this.clrOddRow2.TabIndex = 8;
            // 
            // clrEventRow2
            // 
            this.clrEventRow2.EditValue = System.Drawing.Color.Empty;
            this.clrEventRow2.Location = new System.Drawing.Point(6, 45);
            this.clrEventRow2.Name = "clrEventRow2";
            this.clrEventRow2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.clrEventRow2.Properties.ShowCustomColors = false;
            this.clrEventRow2.Size = new System.Drawing.Size(116, 20);
            this.clrEventRow2.TabIndex = 6;
            // 
            // clrSelectedRow2
            // 
            this.clrSelectedRow2.EditValue = System.Drawing.Color.Empty;
            this.clrSelectedRow2.Location = new System.Drawing.Point(6, 20);
            this.clrSelectedRow2.Name = "clrSelectedRow2";
            this.clrSelectedRow2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.clrSelectedRow2.Properties.ShowCustomColors = false;
            this.clrSelectedRow2.Size = new System.Drawing.Size(116, 20);
            this.clrSelectedRow2.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clrSelectedRow1);
            this.groupBox1.Controls.Add(this.clrOddRow1);
            this.groupBox1.Controls.Add(this.clrEventRow1);
            this.groupBox1.Location = new System.Drawing.Point(144, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 100);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Өнгө 1";
            // 
            // clrSelectedRow1
            // 
            this.clrSelectedRow1.EditValue = System.Drawing.Color.Empty;
            this.clrSelectedRow1.Location = new System.Drawing.Point(8, 19);
            this.clrSelectedRow1.Name = "clrSelectedRow1";
            this.clrSelectedRow1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.clrSelectedRow1.Properties.ShowCustomColors = false;
            this.clrSelectedRow1.Size = new System.Drawing.Size(116, 20);
            this.clrSelectedRow1.TabIndex = 11;
            // 
            // clrOddRow1
            // 
            this.clrOddRow1.EditValue = System.Drawing.Color.Empty;
            this.clrOddRow1.Location = new System.Drawing.Point(8, 71);
            this.clrOddRow1.Name = "clrOddRow1";
            this.clrOddRow1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.clrOddRow1.Properties.ShowCustomColors = false;
            this.clrOddRow1.Size = new System.Drawing.Size(116, 20);
            this.clrOddRow1.TabIndex = 13;
            // 
            // clrEventRow1
            // 
            this.clrEventRow1.EditValue = System.Drawing.Color.Empty;
            this.clrEventRow1.Location = new System.Drawing.Point(8, 45);
            this.clrEventRow1.Name = "clrEventRow1";
            this.clrEventRow1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.clrEventRow1.Properties.ShowCustomColors = false;
            this.clrEventRow1.Size = new System.Drawing.Size(116, 20);
            this.clrEventRow1.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(123, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Идэвхитэй мөрийн өнгө:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 76);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(93, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Тэгш мөрийн өнгө:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(112, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Сондгой мөрийн өнгө:";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelControl1);
            this.panelControl3.Controls.Add(this.panelControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(807, 353);
            this.panelControl3.TabIndex = 4;
            // 
            // frmDashBSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 386);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelControl3);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(815, 413);
            this.Name = "frmDashBSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ажлын самбарын тохиргоо";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDashBSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmDashBSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDashBSettings_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clrOddRow2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrEventRow2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrSelectedRow2.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clrSelectedRow1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrOddRow1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clrEventRow1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grdSettings;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwSettings;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit ColorEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.ColorEdit clrOddRow2;
        private DevExpress.XtraEditors.ColorEdit clrEventRow2;
        private DevExpress.XtraEditors.ColorEdit clrSelectedRow2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.ColorEdit clrSelectedRow1;
        private DevExpress.XtraEditors.ColorEdit clrOddRow1;
        private DevExpress.XtraEditors.ColorEdit clrEventRow1;
    }
}