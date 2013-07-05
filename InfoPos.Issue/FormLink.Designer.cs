namespace InfoPos.Issue
{
    partial class FormLink
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
            this.components = new System.ComponentModel.Container();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlData = new DevExpress.XtraEditors.GroupControl();
            this.btnDestIssueID = new DevExpress.XtraEditors.SimpleButton();
            this.btnSourceIssueID = new DevExpress.XtraEditors.SimpleButton();
            this.lblTxnType = new DevExpress.XtraEditors.LabelControl();
            this.cboType = new DevExpress.XtraEditors.LookUpEdit();
            this.lblLinkeTypeID = new DevExpress.XtraEditors.LabelControl();
            this.cboLinkType = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDestIssueID = new DevExpress.XtraEditors.LabelControl();
            this.lblComment = new DevExpress.XtraEditors.LabelControl();
            this.lblSourceIssueID = new DevExpress.XtraEditors.LabelControl();
            this.mmeComment = new DevExpress.XtraEditors.MemoEdit();
            this.txtDestIssueID = new DevExpress.XtraEditors.TextEdit();
            this.txtSourceIssueID = new DevExpress.XtraEditors.TextEdit();
            this.ErrorChecker = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlData)).BeginInit();
            this.groupControlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLinkType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestIssueID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceIssueID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(428, 177);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(102, 23);
            this.btnEnter.TabIndex = 5;
            this.btnEnter.Text = "Хадгалах";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(536, 177);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Болих";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupControlData
            // 
            this.groupControlData.Controls.Add(this.btnDestIssueID);
            this.groupControlData.Controls.Add(this.btnSourceIssueID);
            this.groupControlData.Controls.Add(this.lblTxnType);
            this.groupControlData.Controls.Add(this.cboType);
            this.groupControlData.Controls.Add(this.lblLinkeTypeID);
            this.groupControlData.Controls.Add(this.cboLinkType);
            this.groupControlData.Controls.Add(this.lblDestIssueID);
            this.groupControlData.Controls.Add(this.lblComment);
            this.groupControlData.Controls.Add(this.lblSourceIssueID);
            this.groupControlData.Controls.Add(this.mmeComment);
            this.groupControlData.Controls.Add(this.txtDestIssueID);
            this.groupControlData.Controls.Add(this.txtSourceIssueID);
            this.groupControlData.Location = new System.Drawing.Point(12, 12);
            this.groupControlData.Name = "groupControlData";
            this.groupControlData.Size = new System.Drawing.Size(627, 161);
            this.groupControlData.TabIndex = 6;
            this.groupControlData.Text = "Өгөгдөл";
            // 
            // btnDestIssueID
            // 
            this.btnDestIssueID.Location = new System.Drawing.Point(524, 68);
            this.btnDestIssueID.Name = "btnDestIssueID";
            this.btnDestIssueID.Size = new System.Drawing.Size(89, 22);
            this.btnDestIssueID.TabIndex = 8;
            this.btnDestIssueID.Text = "Хайх";
            this.btnDestIssueID.Click += new System.EventHandler(this.btnDestIssueID_Click);
            // 
            // btnSourceIssueID
            // 
            this.btnSourceIssueID.Location = new System.Drawing.Point(524, 45);
            this.btnSourceIssueID.Name = "btnSourceIssueID";
            this.btnSourceIssueID.Size = new System.Drawing.Size(89, 22);
            this.btnSourceIssueID.TabIndex = 7;
            this.btnSourceIssueID.Text = "Хайх";
            this.btnSourceIssueID.Click += new System.EventHandler(this.btnSourceIssueID_Click);
            // 
            // lblTxnType
            // 
            this.lblTxnType.Location = new System.Drawing.Point(15, 28);
            this.lblTxnType.Name = "lblTxnType";
            this.lblTxnType.Size = new System.Drawing.Size(96, 13);
            this.lblTxnType.TabIndex = 9;
            this.lblTxnType.Text = "Гүйлгээний төрөл :";
            // 
            // cboType
            // 
            this.cboType.Location = new System.Drawing.Point(188, 25);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cboType.Properties.Appearance.Options.UseBackColor = true;
            this.cboType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.Properties.NullText = "";
            this.cboType.Size = new System.Drawing.Size(330, 20);
            this.cboType.TabIndex = 0;
            this.cboType.ToolTipTitle = "Гүйлгээний төрөл оруулна уу";
            this.cboType.EditValueChanged += new System.EventHandler(this.cboType_EditValueChanged);
            // 
            // lblLinkeTypeID
            // 
            this.lblLinkeTypeID.Location = new System.Drawing.Point(15, 91);
            this.lblLinkeTypeID.Name = "lblLinkeTypeID";
            this.lblLinkeTypeID.Size = new System.Drawing.Size(96, 13);
            this.lblLinkeTypeID.TabIndex = 7;
            this.lblLinkeTypeID.Text = "Холболтын төрөл :";
            this.lblLinkeTypeID.ToolTipTitle = "Холболтын төрөл оруулна уу";
            // 
            // cboLinkType
            // 
            this.cboLinkType.Location = new System.Drawing.Point(188, 88);
            this.cboLinkType.Name = "cboLinkType";
            this.cboLinkType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cboLinkType.Properties.Appearance.Options.UseBackColor = true;
            this.cboLinkType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLinkType.Properties.NullText = "";
            this.cboLinkType.Size = new System.Drawing.Size(330, 20);
            this.cboLinkType.TabIndex = 3;
            this.cboLinkType.ToolTipTitle = "Холболтын төрөл оруулна уу";
            // 
            // lblDestIssueID
            // 
            this.lblDestIssueID.Location = new System.Drawing.Point(15, 70);
            this.lblDestIssueID.Name = "lblDestIssueID";
            this.lblDestIssueID.Size = new System.Drawing.Size(158, 13);
            this.lblDestIssueID.TabIndex = 5;
            this.lblDestIssueID.Text = "Холбогдсон асуудлын дугаар :";
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(15, 112);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(108, 13);
            this.lblComment.TabIndex = 3;
            this.lblComment.Text = "Гүйлгээний тайлбар :";
            this.lblComment.ToolTipTitle = "Гүйлгээний тайлбар оруулна уу";
            // 
            // lblSourceIssueID
            // 
            this.lblSourceIssueID.Location = new System.Drawing.Point(15, 49);
            this.lblSourceIssueID.Name = "lblSourceIssueID";
            this.lblSourceIssueID.Size = new System.Drawing.Size(112, 13);
            this.lblSourceIssueID.TabIndex = 1;
            this.lblSourceIssueID.Text = "Эх асуудлын дугаар :";
            // 
            // mmeComment
            // 
            this.mmeComment.Location = new System.Drawing.Point(188, 109);
            this.mmeComment.Name = "mmeComment";
            this.mmeComment.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.mmeComment.Properties.Appearance.Options.UseBackColor = true;
            this.mmeComment.Size = new System.Drawing.Size(330, 41);
            this.mmeComment.TabIndex = 4;
            this.mmeComment.ToolTipTitle = "Гүйлгээний тайлбар оруулна уу";
            // 
            // txtDestIssueID
            // 
            this.txtDestIssueID.Location = new System.Drawing.Point(188, 67);
            this.txtDestIssueID.Name = "txtDestIssueID";
            this.txtDestIssueID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDestIssueID.Properties.Appearance.Options.UseBackColor = true;
            this.txtDestIssueID.Properties.ReadOnly = true;
            this.txtDestIssueID.Size = new System.Drawing.Size(330, 20);
            this.txtDestIssueID.TabIndex = 2;
            this.txtDestIssueID.ToolTipTitle = "Холбогдсон асуудлын дугаар оруулна уу";
            // 
            // txtSourceIssueID
            // 
            this.txtSourceIssueID.Location = new System.Drawing.Point(188, 46);
            this.txtSourceIssueID.Name = "txtSourceIssueID";
            this.txtSourceIssueID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSourceIssueID.Properties.Appearance.Options.UseBackColor = true;
            this.txtSourceIssueID.Properties.ReadOnly = true;
            this.txtSourceIssueID.Size = new System.Drawing.Size(330, 20);
            this.txtSourceIssueID.TabIndex = 1;
            this.txtSourceIssueID.ToolTipTitle = "Эх асуудлын дугаар оруулна уу";
            // 
            // ErrorChecker
            // 
            this.ErrorChecker.ContainerControl = this;
            // 
            // FormLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 203);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupControlData);
            this.MaximumSize = new System.Drawing.Size(667, 241);
            this.MinimumSize = new System.Drawing.Size(667, 241);
            this.Name = "FormLink";
            this.Text = "Гүйлгээ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLink_FormClosing);
            this.Load += new System.EventHandler(this.FormLink_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlData)).EndInit();
            this.groupControlData.ResumeLayout(false);
            this.groupControlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLinkType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestIssueID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceIssueID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.GroupControl groupControlData;
        private DevExpress.XtraEditors.LabelControl lblComment;
        private DevExpress.XtraEditors.LabelControl lblSourceIssueID;
        private DevExpress.XtraEditors.MemoEdit mmeComment;
        private DevExpress.XtraEditors.LabelControl lblDestIssueID;
        private DevExpress.XtraEditors.LabelControl lblLinkeTypeID;
        private DevExpress.XtraEditors.LookUpEdit cboLinkType;
        private DevExpress.XtraEditors.SimpleButton btnDestIssueID;
        private DevExpress.XtraEditors.SimpleButton btnSourceIssueID;
        private DevExpress.XtraEditors.LabelControl lblTxnType;
        private DevExpress.XtraEditors.LookUpEdit cboType;
        private DevExpress.XtraEditors.TextEdit txtDestIssueID;
        private DevExpress.XtraEditors.TextEdit txtSourceIssueID;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorChecker;
    }
}