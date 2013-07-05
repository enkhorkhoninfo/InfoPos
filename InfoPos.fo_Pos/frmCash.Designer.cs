namespace InfoPos.Pos
{
    partial class frmCash
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
            this.pnlTop = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblWarning = new DevExpress.XtraEditors.LabelControl();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.pnlTop.Panel1.Controls.Add(this.groupControl4);
            this.pnlTop.Panel1.Controls.Add(this.groupControl3);
            this.pnlTop.Panel2.Controls.Add(this.groupControl1);
            this.pnlTop.Panel2.Text = "Кассын дүн:";
            this.pnlTop.Size = new System.Drawing.Size(989, 512);
            this.pnlTop.SplitterPosition = 301;
            this.pnlTop.TabIndex = 1;
            this.pnlTop.Text = "splitContainerControl1";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.gridControl2);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl4.Location = new System.Drawing.Point(0, 316);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(297, 192);
            this.groupControl4.TabIndex = 8;
            this.groupControl4.Text = "Ээлжийн хөдөлгөөн:";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 21);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(293, 169);
            this.gridControl2.TabIndex = 6;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsCustomization.AllowGroup = false;
            this.gridView2.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.RowHeight = 28;
            this.gridView2.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.gridControl1);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(297, 508);
            this.groupControl3.TabIndex = 7;
            this.groupControl3.Text = "Мөнгөн дэвсгэртүүд:";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(2, 23);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(357, 291);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.RowHeight = 28;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblWarning);
            this.groupControl1.Controls.Add(this.gridControl3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(683, 512);
            this.groupControl1.TabIndex = 33;
            this.groupControl1.Text = "Кассын мэдээ:";
            // 
            // lblWarning
            // 
            this.lblWarning.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblWarning.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblWarning.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblWarning.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblWarning.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblWarning.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblWarning.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblWarning.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblWarning.Location = new System.Drawing.Point(20, 42);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(317, 75);
            this.lblWarning.TabIndex = 34;
            this.lblWarning.Text = "Одоогийн ээлж өөр хэрэглэгч дээр байгаа тул, ээлж хаасны дараа ээлжийн хөдөлгөөн " +
    "хийх боломжтой болно!";
            this.lblWarning.Visible = false;
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.Location = new System.Drawing.Point(2, 2);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(679, 508);
            this.gridControl3.TabIndex = 7;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsBehavior.ReadOnly = true;
            this.gridView3.OptionsCustomization.AllowGroup = false;
            this.gridView3.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.gridView3.OptionsView.ColumnAutoWidth = false;
            this.gridView3.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            this.gridView3.RowHeight = 28;
            this.gridView3.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            // 
            // frmCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 512);
            this.Controls.Add(this.pnlTop);
            this.Name = "frmCash";
            this.Text = "Кассын нээлт хаалт";
            this.Load += new System.EventHandler(this.frmCash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl pnlTop;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl lblWarning;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
    }
}