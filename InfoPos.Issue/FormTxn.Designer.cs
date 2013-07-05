namespace InfoPos.Issue
{
    partial class FormTxn
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
            this.groupControlData = new DevExpress.XtraEditors.GroupControl();
            this.lblContactType = new DevExpress.XtraEditors.LabelControl();
            this.cboContactType = new DevExpress.XtraEditors.LookUpEdit();
            this.btnFile = new DevExpress.XtraEditors.SimpleButton();
            this.lblResolutionTypeID = new DevExpress.XtraEditors.LabelControl();
            this.cboResolutionTypeID = new DevExpress.XtraEditors.LookUpEdit();
            this.lblSubject = new DevExpress.XtraEditors.LabelControl();
            this.lblNextDate = new DevExpress.XtraEditors.LabelControl();
            this.lblNextPurpose = new DevExpress.XtraEditors.LabelControl();
            this.mmeNextPurpose = new DevExpress.XtraEditors.MemoEdit();
            this.checkReport = new DevExpress.XtraEditors.CheckEdit();
            this.lblTrackID = new DevExpress.XtraEditors.LabelControl();
            this.cboTrackID = new DevExpress.XtraEditors.LookUpEdit();
            this.lblComment = new DevExpress.XtraEditors.LabelControl();
            this.lblTxnType = new DevExpress.XtraEditors.LabelControl();
            this.cboType = new DevExpress.XtraEditors.LookUpEdit();
            this.mmeComment = new DevExpress.XtraEditors.MemoEdit();
            this.dteNextDate = new DevExpress.XtraEditors.DateEdit();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.ErrorChecker = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlData)).BeginInit();
            this.groupControlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboContactType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResolutionTypeID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNextPurpose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrackID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNextDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNextDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControlData
            // 
            this.groupControlData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControlData.Controls.Add(this.lblContactType);
            this.groupControlData.Controls.Add(this.cboContactType);
            this.groupControlData.Controls.Add(this.btnFile);
            this.groupControlData.Controls.Add(this.lblResolutionTypeID);
            this.groupControlData.Controls.Add(this.cboResolutionTypeID);
            this.groupControlData.Controls.Add(this.lblSubject);
            this.groupControlData.Controls.Add(this.lblNextDate);
            this.groupControlData.Controls.Add(this.lblNextPurpose);
            this.groupControlData.Controls.Add(this.mmeNextPurpose);
            this.groupControlData.Controls.Add(this.checkReport);
            this.groupControlData.Controls.Add(this.lblTrackID);
            this.groupControlData.Controls.Add(this.cboTrackID);
            this.groupControlData.Controls.Add(this.lblComment);
            this.groupControlData.Controls.Add(this.lblTxnType);
            this.groupControlData.Controls.Add(this.cboType);
            this.groupControlData.Controls.Add(this.mmeComment);
            this.groupControlData.Controls.Add(this.dteNextDate);
            this.groupControlData.Controls.Add(this.txtSubject);
            this.groupControlData.Location = new System.Drawing.Point(12, 12);
            this.groupControlData.Name = "groupControlData";
            this.groupControlData.Size = new System.Drawing.Size(484, 518);
            this.groupControlData.TabIndex = 0;
            this.groupControlData.Text = "Өгөгдөл";
            // 
            // lblContactType
            // 
            this.lblContactType.Location = new System.Drawing.Point(15, 212);
            this.lblContactType.Name = "lblContactType";
            this.lblContactType.Size = new System.Drawing.Size(123, 13);
            this.lblContactType.TabIndex = 32;
            this.lblContactType.Text = "Холбоо барисан төрөл  :";
            // 
            // cboContactType
            // 
            this.cboContactType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboContactType.Location = new System.Drawing.Point(188, 209);
            this.cboContactType.Name = "cboContactType";
            this.cboContactType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboContactType.Properties.NullText = "";
            this.cboContactType.Size = new System.Drawing.Size(279, 20);
            this.cboContactType.TabIndex = 31;
            this.cboContactType.ToolTipTitle = "Холбоо барисан оруулна уу";
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFile.Location = new System.Drawing.Point(188, 242);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(118, 21);
            this.btnFile.TabIndex = 3;
            this.btnFile.Text = "Файл оруулах";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblResolutionTypeID
            // 
            this.lblResolutionTypeID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResolutionTypeID.Location = new System.Drawing.Point(15, 293);
            this.lblResolutionTypeID.Name = "lblResolutionTypeID";
            this.lblResolutionTypeID.Size = new System.Drawing.Size(133, 13);
            this.lblResolutionTypeID.TabIndex = 30;
            this.lblResolutionTypeID.Text = "Асуудал хаагдсан төлөв :";
            // 
            // cboResolutionTypeID
            // 
            this.cboResolutionTypeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboResolutionTypeID.Location = new System.Drawing.Point(188, 290);
            this.cboResolutionTypeID.Name = "cboResolutionTypeID";
            this.cboResolutionTypeID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboResolutionTypeID.Properties.NullText = "";
            this.cboResolutionTypeID.Size = new System.Drawing.Size(279, 20);
            this.cboResolutionTypeID.TabIndex = 5;
            this.cboResolutionTypeID.ToolTipTitle = "Асуудал хаагдсан төлөв оруулна уу";
            // 
            // lblSubject
            // 
            this.lblSubject.Location = new System.Drawing.Point(15, 49);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(116, 13);
            this.lblSubject.TabIndex = 29;
            this.lblSubject.Text = "Гүйлгээний товч утга :";
            // 
            // lblNextDate
            // 
            this.lblNextDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNextDate.Location = new System.Drawing.Point(15, 485);
            this.lblNextDate.Name = "lblNextDate";
            this.lblNextDate.Size = new System.Drawing.Size(139, 13);
            this.lblNextDate.TabIndex = 23;
            this.lblNextDate.Text = "Дараагийн ярилцах огноо :";
            // 
            // lblNextPurpose
            // 
            this.lblNextPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNextPurpose.Location = new System.Drawing.Point(15, 344);
            this.lblNextPurpose.Name = "lblNextPurpose";
            this.lblNextPurpose.Size = new System.Drawing.Size(165, 13);
            this.lblNextPurpose.TabIndex = 22;
            this.lblNextPurpose.Text = "Дараа хийх ажлын төлөвлөгөө :";
            // 
            // mmeNextPurpose
            // 
            this.mmeNextPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mmeNextPurpose.Enabled = false;
            this.mmeNextPurpose.Location = new System.Drawing.Point(188, 341);
            this.mmeNextPurpose.Name = "mmeNextPurpose";
            this.mmeNextPurpose.Size = new System.Drawing.Size(279, 135);
            this.mmeNextPurpose.TabIndex = 7;
            this.mmeNextPurpose.ToolTipTitle = "Дараа хийх ажлын төлөвлөгөө оруулна уу";
            // 
            // checkReport
            // 
            this.checkReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkReport.Location = new System.Drawing.Point(186, 316);
            this.checkReport.Name = "checkReport";
            this.checkReport.Properties.Caption = "Ажлын төлөвлөгөөг оруулах эсэх";
            this.checkReport.Size = new System.Drawing.Size(199, 19);
            this.checkReport.TabIndex = 6;
            this.checkReport.CheckedChanged += new System.EventHandler(this.checkReport_CheckedChanged);
            // 
            // lblTrackID
            // 
            this.lblTrackID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrackID.Location = new System.Drawing.Point(15, 272);
            this.lblTrackID.Name = "lblTrackID";
            this.lblTrackID.Size = new System.Drawing.Size(101, 13);
            this.lblTrackID.TabIndex = 18;
            this.lblTrackID.Text = "Шатлалын байдал :";
            // 
            // cboTrackID
            // 
            this.cboTrackID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTrackID.Location = new System.Drawing.Point(188, 269);
            this.cboTrackID.Name = "cboTrackID";
            this.cboTrackID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTrackID.Properties.NullText = "";
            this.cboTrackID.Size = new System.Drawing.Size(279, 20);
            this.cboTrackID.TabIndex = 4;
            this.cboTrackID.ToolTipTitle = "Шатлалын байдал оруулна уу";
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(15, 70);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(108, 13);
            this.lblComment.TabIndex = 3;
            this.lblComment.Text = "Гүйлгээний тайлбар :";
            // 
            // lblTxnType
            // 
            this.lblTxnType.Location = new System.Drawing.Point(15, 28);
            this.lblTxnType.Name = "lblTxnType";
            this.lblTxnType.Size = new System.Drawing.Size(96, 13);
            this.lblTxnType.TabIndex = 1;
            this.lblTxnType.Text = "Гүйлгээний төрөл :";
            // 
            // cboType
            // 
            this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboType.Location = new System.Drawing.Point(188, 25);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.Properties.NullText = "";
            this.cboType.Size = new System.Drawing.Size(279, 20);
            this.cboType.TabIndex = 0;
            this.cboType.ToolTipTitle = "Гүйлгээний төрөл оруулна уу";
            this.cboType.EditValueChanged += new System.EventHandler(this.cboType_EditValueChanged);
            // 
            // mmeComment
            // 
            this.mmeComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mmeComment.Location = new System.Drawing.Point(188, 67);
            this.mmeComment.Name = "mmeComment";
            this.mmeComment.Size = new System.Drawing.Size(279, 141);
            this.mmeComment.TabIndex = 2;
            this.mmeComment.ToolTipTitle = "Гүйлгээний тайлбар оруулна уу";
            // 
            // dteNextDate
            // 
            this.dteNextDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteNextDate.EditValue = null;
            this.dteNextDate.Enabled = false;
            this.dteNextDate.Location = new System.Drawing.Point(188, 482);
            this.dteNextDate.Name = "dteNextDate";
            this.dteNextDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteNextDate.Properties.Mask.EditMask = "";
            this.dteNextDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteNextDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteNextDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteNextDate.Size = new System.Drawing.Size(279, 20);
            this.dteNextDate.TabIndex = 8;
            this.dteNextDate.ToolTipTitle = "Дараагийн ярилцах огноо оруулна уу";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(188, 46);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(279, 20);
            this.txtSubject.TabIndex = 1;
            this.txtSubject.ToolTipTitle = "Гүйлгээний товч утга оруулна уу";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(394, 535);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Болих";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.Location = new System.Drawing.Point(286, 535);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(102, 23);
            this.btnEnter.TabIndex = 9;
            this.btnEnter.Text = "Хадгалах";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // ErrorChecker
            // 
            this.ErrorChecker.ContainerControl = this;
            // 
            // FormTxn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 563);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupControlData);
            this.MinimumSize = new System.Drawing.Size(524, 515);
            this.Name = "FormTxn";
            this.Text = "Асуудлын гүйлгээ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTxn_FormClosing);
            this.Load += new System.EventHandler(this.FormTxn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlData)).EndInit();
            this.groupControlData.ResumeLayout(false);
            this.groupControlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboContactType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResolutionTypeID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNextPurpose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrackID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNextDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNextDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControlData;
        private DevExpress.XtraEditors.LookUpEdit cboType;
        private DevExpress.XtraEditors.LabelControl lblComment;
        private DevExpress.XtraEditors.LabelControl lblTxnType;
        private DevExpress.XtraEditors.MemoEdit mmeComment;
        private DevExpress.XtraEditors.LabelControl lblNextDate;
        private DevExpress.XtraEditors.LabelControl lblNextPurpose;
        private DevExpress.XtraEditors.MemoEdit mmeNextPurpose;
        private DevExpress.XtraEditors.CheckEdit checkReport;
        private DevExpress.XtraEditors.LabelControl lblTrackID;
        private DevExpress.XtraEditors.LookUpEdit cboTrackID;
        private DevExpress.XtraEditors.DateEdit dteNextDate;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.LabelControl lblSubject;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.LabelControl lblResolutionTypeID;
        private DevExpress.XtraEditors.LookUpEdit cboResolutionTypeID;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorChecker;
        private DevExpress.XtraEditors.SimpleButton btnFile;
        private DevExpress.XtraEditors.LabelControl lblContactType;
        private DevExpress.XtraEditors.LookUpEdit cboContactType;
    }
}