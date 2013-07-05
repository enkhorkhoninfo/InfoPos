namespace InfoPos.Issue
{
    partial class ucAssign
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAssignUser = new DevExpress.XtraEditors.LabelControl();
            this.pnlData = new DevExpress.XtraEditors.PanelControl();
            this.lblComment = new DevExpress.XtraEditors.LabelControl();
            this.Comment = new DevExpress.XtraEditors.MemoEdit();
            this.DoAssign = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.grdData = new DevExpress.XtraGrid.GridControl();
            this.gvwData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExCo = new DevExpress.XtraEditors.SimpleButton();
            this.pnlName = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlData)).BeginInit();
            this.pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Comment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlName)).BeginInit();
            this.pnlName.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAssignUser
            // 
            this.lblAssignUser.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignUser.Location = new System.Drawing.Point(5, 3);
            this.lblAssignUser.Name = "lblAssignUser";
            this.lblAssignUser.Size = new System.Drawing.Size(69, 13);
            this.lblAssignUser.TabIndex = 0;
            this.lblAssignUser.Text = "AssignName";
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.lblComment);
            this.pnlData.Controls.Add(this.Comment);
            this.pnlData.Controls.Add(this.DoAssign);
            this.pnlData.Controls.Add(this.btnCancel);
            this.pnlData.Controls.Add(this.grdData);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlData.Location = new System.Drawing.Point(0, 23);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(215, 227);
            this.pnlData.TabIndex = 1;
            // 
            // lblComment
            // 
            this.lblComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblComment.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.Location = new System.Drawing.Point(6, 143);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(91, 11);
            this.lblComment.TabIndex = 4;
            this.lblComment.Text = "Гүйлгээний тайлбар :";
            // 
            // Comment
            // 
            this.Comment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Comment.Location = new System.Drawing.Point(3, 160);
            this.Comment.Name = "Comment";
            this.Comment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Comment.Properties.Appearance.Options.UseFont = true;
            this.Comment.Size = new System.Drawing.Size(209, 45);
            this.Comment.TabIndex = 3;
            // 
            // DoAssign
            // 
            this.DoAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoAssign.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoAssign.Appearance.Options.UseFont = true;
            this.DoAssign.Image = global::InfoPos.Issue.Resource.ok;
            this.DoAssign.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.DoAssign.Location = new System.Drawing.Point(174, 207);
            this.DoAssign.Name = "DoAssign";
            this.DoAssign.Size = new System.Drawing.Size(16, 16);
            this.DoAssign.TabIndex = 2;
            this.DoAssign.ToolTip = "Шилжүүлэх";
            this.DoAssign.Click += new System.EventHandler(this.DoAssign_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Image = global::InfoPos.Issue.Resource.close;
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(196, 207);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(16, 16);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.ToolTip = "Болих";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.Location = new System.Drawing.Point(3, 3);
            this.grdData.MainView = this.gvwData;
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(209, 136);
            this.grdData.TabIndex = 0;
            this.grdData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwData});
            // 
            // gvwData
            // 
            this.gvwData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.NAME});
            this.gvwData.GridControl = this.grdData;
            this.gvwData.Name = "gvwData";
            this.gvwData.OptionsView.ShowAutoFilterRow = true;
            this.gvwData.OptionsView.ShowGroupPanel = false;
            this.gvwData.OptionsView.ShowHorzLines = false;
            this.gvwData.OptionsView.ShowIndicator = false;
            this.gvwData.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvwData_FocusedRowChanged);
            // 
            // ID
            // 
            this.ID.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ID.AppearanceCell.Options.UseFont = true;
            this.ID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ID.AppearanceHeader.Options.UseFont = true;
            this.ID.Caption = "Дугаар";
            this.ID.FieldName = "USERNO";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.Visible = true;
            this.ID.VisibleIndex = 0;
            this.ID.Width = 50;
            // 
            // NAME
            // 
            this.NAME.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.NAME.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.NAME.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NAME.AppearanceCell.Options.UseBackColor = true;
            this.NAME.AppearanceCell.Options.UseFont = true;
            this.NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NAME.AppearanceHeader.Options.UseFont = true;
            this.NAME.Caption = "Хэрэглэгчийн нэр";
            this.NAME.FieldName = "USERLNAME";
            this.NAME.Name = "NAME";
            this.NAME.OptionsColumn.AllowEdit = false;
            this.NAME.Visible = true;
            this.NAME.VisibleIndex = 1;
            this.NAME.Width = 141;
            // 
            // btnExCo
            // 
            this.btnExCo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExCo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExCo.Location = new System.Drawing.Point(194, 2);
            this.btnExCo.Name = "btnExCo";
            this.btnExCo.Size = new System.Drawing.Size(16, 16);
            this.btnExCo.TabIndex = 2;
            this.btnExCo.Click += new System.EventHandler(this.btnExCo_Click);
            // 
            // pnlName
            // 
            this.pnlName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlName.Controls.Add(this.btnExCo);
            this.pnlName.Controls.Add(this.lblAssignUser);
            this.pnlName.Location = new System.Drawing.Point(0, 0);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(215, 20);
            this.pnlName.TabIndex = 5;
            // 
            // ucAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlName);
            this.Controls.Add(this.pnlData);
            this.MinimumSize = new System.Drawing.Size(150, 20);
            this.Name = "ucAssign";
            this.Size = new System.Drawing.Size(215, 250);
            this.Load += new System.EventHandler(this.ucAssign_Load);
            this.Leave += new System.EventHandler(this.ucAssign_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.pnlData)).EndInit();
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Comment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlName)).EndInit();
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblAssignUser;
        private DevExpress.XtraEditors.PanelControl pnlData;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.GridControl grdData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwData;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn NAME;
        private DevExpress.XtraEditors.LabelControl lblComment;
        private DevExpress.XtraEditors.SimpleButton btnExCo;
        private DevExpress.XtraEditors.PanelControl pnlName;
        public DevExpress.XtraEditors.SimpleButton DoAssign;
        public DevExpress.XtraEditors.MemoEdit Comment;
    }
}
