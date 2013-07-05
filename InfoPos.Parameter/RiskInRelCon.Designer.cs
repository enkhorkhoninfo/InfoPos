namespace InfoPos.Parameter
{
    partial class ConInRel
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.grdConInRel = new DevExpress.XtraGrid.GridControl();
            this.gvwConInRel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdConInRel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwConInRel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnSave.Location = new System.Drawing.Point(5, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(29, 28);
            this.btnSave.TabIndex = 13;
            this.btnSave.ToolTip = "Эрсдэлийн бүлэгт үйлчилгээ нэмэх";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdConInRel
            // 
            this.grdConInRel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdConInRel.Location = new System.Drawing.Point(2, 2);
            this.grdConInRel.MainView = this.gvwConInRel;
            this.grdConInRel.Name = "grdConInRel";
            this.grdConInRel.Size = new System.Drawing.Size(632, 502);
            this.grdConInRel.TabIndex = 12;
            this.grdConInRel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwConInRel});
            this.grdConInRel.Click += new System.EventHandler(this.grdConInRel_Click);
            this.grdConInRel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdConInRel_KeyDown);
            // 
            // gvwConInRel
            // 
            this.gvwConInRel.GridControl = this.grdConInRel;
            this.gvwConInRel.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.gvwConInRel.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwConInRel.Name = "gvwConInRel";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(640, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(39, 547);
            this.panelControl1.TabIndex = 14;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Controls.Add(this.panel1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(640, 547);
            this.groupControl1.TabIndex = 15;
            this.groupControl1.Text = "Үйлчилгээ";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.grdConInRel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 39);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(636, 506);
            this.panelControl2.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 17);
            this.panel1.TabIndex = 14;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(4, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Бүгдийг сонгох";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ConInRel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 547);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(687, 574);
            this.Name = "ConInRel";
            this.Text = "Эрсдэлийн бүлэгт холбогдоогүй үйлчилгээний жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConInRel_FormClosing);
            this.Load += new System.EventHandler(this.ConInRel_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConInRel_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdConInRel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwConInRel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl grdConInRel;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwConInRel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}