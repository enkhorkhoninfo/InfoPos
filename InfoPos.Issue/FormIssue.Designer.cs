namespace InfoPos.Issue
{
    partial class FormIssue
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
            this.ErrorChecker = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            this.ucToggleIssue = new ISM.Template.ucTogglePanel();
            this.groupData = new DevExpress.XtraEditors.GroupControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.mmeDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtResolutionUser = new DevExpress.XtraEditors.TextEdit();
            this.lblResolutionUser = new DevExpress.XtraEditors.LabelControl();
            this.lblResolutionDate = new DevExpress.XtraEditors.LabelControl();
            this.lblAssigneeUser = new DevExpress.XtraEditors.LabelControl();
            this.dteResolutionDate = new DevExpress.XtraEditors.DateEdit();
            this.lblDueDate = new DevExpress.XtraEditors.LabelControl();
            this.dteDueDate = new DevExpress.XtraEditors.DateEdit();
            this.lblUpdateDate = new DevExpress.XtraEditors.LabelControl();
            this.lblCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.txtCreateUser = new DevExpress.XtraEditors.TextEdit();
            this.lblCreateUser = new DevExpress.XtraEditors.LabelControl();
            this.lblTrackID = new DevExpress.XtraEditors.LabelControl();
            this.lblResolutionStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.lblSubject = new DevExpress.XtraEditors.LabelControl();
            this.mmeSubject = new DevExpress.XtraEditors.MemoEdit();
            this.lblIssuePriorID = new DevExpress.XtraEditors.LabelControl();
            this.cboIssuePriorID = new DevExpress.XtraEditors.LookUpEdit();
            this.lblIssueTypeID = new DevExpress.XtraEditors.LabelControl();
            this.cboIssueTypeID = new DevExpress.XtraEditors.LookUpEdit();
            this.lblProjectCompID = new DevExpress.XtraEditors.LabelControl();
            this.lblProjectID = new DevExpress.XtraEditors.LabelControl();
            this.txtIssueID = new DevExpress.XtraEditors.TextEdit();
            this.lblIssueID = new DevExpress.XtraEditors.LabelControl();
            this.cboProjectCompID = new DevExpress.XtraEditors.LookUpEdit();
            this.cboProjectID = new DevExpress.XtraEditors.LookUpEdit();
            this.dteCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.dteUpdateDate = new DevExpress.XtraEditors.DateEdit();
            this.cboResolutionStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.cboTrackID = new DevExpress.XtraEditors.LookUpEdit();
            this.cboAssigneeUser = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).BeginInit();
            this.ucToggleIssue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupData)).BeginInit();
            this.groupData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmeDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResolutionUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteResolutionDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteResolutionDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDueDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDueDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIssuePriorID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIssueTypeID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectCompID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteUpdateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteUpdateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResolutionStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrackID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAssigneeUser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorChecker
            // 
            this.ErrorChecker.ContainerControl = this;
            // 
            // ucToggleIssue
            // 
            this.ucToggleIssue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucToggleIssue.Controls.Add(this.groupData);
            this.ucToggleIssue.Location = new System.Drawing.Point(8, 2);
            this.ucToggleIssue.Name = "ucToggleIssue";
            this.ucToggleIssue.Size = new System.Drawing.Size(947, 304);
            this.ucToggleIssue.TabIndex = 1;
            this.ucToggleIssue.ToggleShowDelete = false;
            this.ucToggleIssue.ToggleShowEdit = false;
            this.ucToggleIssue.ToggleShowExit = false;
            this.ucToggleIssue.ToggleShowNew = false;
            this.ucToggleIssue.ToggleShowReject = false;
            this.ucToggleIssue.ToggleShowSave = false;
            // 
            // groupData
            // 
            this.groupData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupData.Controls.Add(this.lblDescription);
            this.groupData.Controls.Add(this.mmeDescription);
            this.groupData.Controls.Add(this.txtResolutionUser);
            this.groupData.Controls.Add(this.lblResolutionUser);
            this.groupData.Controls.Add(this.lblResolutionDate);
            this.groupData.Controls.Add(this.lblAssigneeUser);
            this.groupData.Controls.Add(this.dteResolutionDate);
            this.groupData.Controls.Add(this.lblDueDate);
            this.groupData.Controls.Add(this.dteDueDate);
            this.groupData.Controls.Add(this.lblUpdateDate);
            this.groupData.Controls.Add(this.lblCreateDate);
            this.groupData.Controls.Add(this.txtCreateUser);
            this.groupData.Controls.Add(this.lblCreateUser);
            this.groupData.Controls.Add(this.lblTrackID);
            this.groupData.Controls.Add(this.lblResolutionStatus);
            this.groupData.Controls.Add(this.lblStatus);
            this.groupData.Controls.Add(this.cboStatus);
            this.groupData.Controls.Add(this.lblSubject);
            this.groupData.Controls.Add(this.mmeSubject);
            this.groupData.Controls.Add(this.lblIssuePriorID);
            this.groupData.Controls.Add(this.cboIssuePriorID);
            this.groupData.Controls.Add(this.lblIssueTypeID);
            this.groupData.Controls.Add(this.cboIssueTypeID);
            this.groupData.Controls.Add(this.lblProjectCompID);
            this.groupData.Controls.Add(this.lblProjectID);
            this.groupData.Controls.Add(this.txtIssueID);
            this.groupData.Controls.Add(this.lblIssueID);
            this.groupData.Controls.Add(this.cboProjectCompID);
            this.groupData.Controls.Add(this.cboProjectID);
            this.groupData.Controls.Add(this.dteCreateDate);
            this.groupData.Controls.Add(this.dteUpdateDate);
            this.groupData.Controls.Add(this.cboResolutionStatus);
            this.groupData.Controls.Add(this.cboTrackID);
            this.groupData.Controls.Add(this.cboAssigneeUser);
            this.groupData.Location = new System.Drawing.Point(3, 10);
            this.groupData.Name = "groupData";
            this.groupData.Size = new System.Drawing.Size(934, 262);
            this.groupData.TabIndex = 0;
            this.groupData.Text = "Өгөгдөл";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(17, 180);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(122, 13);
            this.lblDescription.TabIndex = 33;
            this.lblDescription.Text = "Асуудлын дэлгэрэнгүй :";
            // 
            // mmeDescription
            // 
            this.mmeDescription.Location = new System.Drawing.Point(163, 177);
            this.mmeDescription.Name = "mmeDescription";
            this.mmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Cornsilk;
            this.mmeDescription.Properties.Appearance.Options.UseBackColor = true;
            this.mmeDescription.Size = new System.Drawing.Size(755, 74);
            this.mmeDescription.TabIndex = 6;
            this.mmeDescription.ToolTipTitle = "Асуудлын дэлгэрэнгүй оруулна уу";
            // 
            // txtResolutionUser
            // 
            this.txtResolutionUser.Location = new System.Drawing.Point(789, 46);
            this.txtResolutionUser.Name = "txtResolutionUser";
            this.txtResolutionUser.Size = new System.Drawing.Size(129, 20);
            this.txtResolutionUser.TabIndex = 13;
            // 
            // lblResolutionUser
            // 
            this.lblResolutionUser.Location = new System.Drawing.Point(634, 49);
            this.lblResolutionUser.Name = "lblResolutionUser";
            this.lblResolutionUser.Size = new System.Drawing.Size(140, 13);
            this.lblResolutionUser.TabIndex = 30;
            this.lblResolutionUser.Text = "Асуудал хаасан хэрэглэгч :";
            // 
            // lblResolutionDate
            // 
            this.lblResolutionDate.Location = new System.Drawing.Point(634, 28);
            this.lblResolutionDate.Name = "lblResolutionDate";
            this.lblResolutionDate.Size = new System.Drawing.Size(132, 13);
            this.lblResolutionDate.TabIndex = 28;
            this.lblResolutionDate.Text = "Асуудал хаагдсан огноо :";
            // 
            // lblAssigneeUser
            // 
            this.lblAssigneeUser.Location = new System.Drawing.Point(309, 70);
            this.lblAssigneeUser.Name = "lblAssigneeUser";
            this.lblAssigneeUser.Size = new System.Drawing.Size(177, 13);
            this.lblAssigneeUser.TabIndex = 26;
            this.lblAssigneeUser.Text = "Одоо хариуцаж байгаа хэрэглэгч :";
            // 
            // dteResolutionDate
            // 
            this.dteResolutionDate.EditValue = null;
            this.dteResolutionDate.Location = new System.Drawing.Point(789, 25);
            this.dteResolutionDate.Name = "dteResolutionDate";
            this.dteResolutionDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteResolutionDate.Properties.Mask.EditMask = "";
            this.dteResolutionDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteResolutionDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteResolutionDate.Size = new System.Drawing.Size(129, 20);
            this.dteResolutionDate.TabIndex = 12;
            // 
            // lblDueDate
            // 
            this.lblDueDate.Location = new System.Drawing.Point(309, 112);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(121, 13);
            this.lblDueDate.TabIndex = 24;
            this.lblDueDate.Text = "Асуудал дуусах огноо :";
            // 
            // dteDueDate
            // 
            this.dteDueDate.EditValue = null;
            this.dteDueDate.Location = new System.Drawing.Point(492, 109);
            this.dteDueDate.Name = "dteDueDate";
            this.dteDueDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDueDate.Properties.Mask.EditMask = "";
            this.dteDueDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteDueDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteDueDate.Size = new System.Drawing.Size(129, 20);
            this.dteDueDate.TabIndex = 11;
            this.dteDueDate.ToolTipTitle = "Асуудал дуусах огноо оруулна уу";
            // 
            // lblUpdateDate
            // 
            this.lblUpdateDate.Location = new System.Drawing.Point(634, 91);
            this.lblUpdateDate.Name = "lblUpdateDate";
            this.lblUpdateDate.Size = new System.Drawing.Size(152, 13);
            this.lblUpdateDate.TabIndex = 22;
            this.lblUpdateDate.Text = "Сүүлд өөрчлөлт орсон огноо :";
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.Location = new System.Drawing.Point(309, 91);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(126, 13);
            this.lblCreateDate.TabIndex = 20;
            this.lblCreateDate.Text = "Асуудал үүсгэсэн огноо :";
            // 
            // txtCreateUser
            // 
            this.txtCreateUser.Location = new System.Drawing.Point(789, 67);
            this.txtCreateUser.Name = "txtCreateUser";
            this.txtCreateUser.Size = new System.Drawing.Size(129, 20);
            this.txtCreateUser.TabIndex = 14;
            this.txtCreateUser.Visible = false;
            // 
            // lblCreateUser
            // 
            this.lblCreateUser.Location = new System.Drawing.Point(634, 70);
            this.lblCreateUser.Name = "lblCreateUser";
            this.lblCreateUser.Size = new System.Drawing.Size(101, 13);
            this.lblCreateUser.TabIndex = 18;
            this.lblCreateUser.Text = "Үүсгэсэн хэрэглэгч :";
            this.lblCreateUser.Visible = false;
            // 
            // lblTrackID
            // 
            this.lblTrackID.Location = new System.Drawing.Point(309, 49);
            this.lblTrackID.Name = "lblTrackID";
            this.lblTrackID.Size = new System.Drawing.Size(101, 13);
            this.lblTrackID.TabIndex = 16;
            this.lblTrackID.Text = "Шатлалын байдал :";
            // 
            // lblResolutionStatus
            // 
            this.lblResolutionStatus.Location = new System.Drawing.Point(634, 112);
            this.lblResolutionStatus.Name = "lblResolutionStatus";
            this.lblResolutionStatus.Size = new System.Drawing.Size(133, 13);
            this.lblResolutionStatus.TabIndex = 14;
            this.lblResolutionStatus.Text = "Асуудал хаагдсан төлөв :";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(309, 28);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Төлөв :";
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(492, 25);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Size = new System.Drawing.Size(129, 20);
            this.cboStatus.TabIndex = 7;
            // 
            // lblSubject
            // 
            this.lblSubject.Location = new System.Drawing.Point(17, 138);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(111, 13);
            this.lblSubject.TabIndex = 11;
            this.lblSubject.Text = "Асуудлын товч утга :";
            // 
            // mmeSubject
            // 
            this.mmeSubject.Location = new System.Drawing.Point(163, 135);
            this.mmeSubject.Name = "mmeSubject";
            this.mmeSubject.Properties.Appearance.BackColor = System.Drawing.Color.Cornsilk;
            this.mmeSubject.Properties.Appearance.Options.UseBackColor = true;
            this.mmeSubject.Size = new System.Drawing.Size(755, 41);
            this.mmeSubject.TabIndex = 5;
            this.mmeSubject.ToolTipTitle = "Асуудлын товч утга оруулна уу";
            // 
            // lblIssuePriorID
            // 
            this.lblIssuePriorID.Location = new System.Drawing.Point(17, 112);
            this.lblIssuePriorID.Name = "lblIssuePriorID";
            this.lblIssuePriorID.Size = new System.Drawing.Size(108, 13);
            this.lblIssuePriorID.TabIndex = 8;
            this.lblIssuePriorID.Text = "Асуудлын эрэмбэ ID :";
            // 
            // cboIssuePriorID
            // 
            this.cboIssuePriorID.Location = new System.Drawing.Point(163, 109);
            this.cboIssuePriorID.Name = "cboIssuePriorID";
            this.cboIssuePriorID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboIssuePriorID.Properties.NullText = "";
            this.cboIssuePriorID.Size = new System.Drawing.Size(129, 20);
            this.cboIssuePriorID.TabIndex = 4;
            this.cboIssuePriorID.ToolTipTitle = "Асуудлын эрэмбэ ID оруулна уу";
            // 
            // lblIssueTypeID
            // 
            this.lblIssueTypeID.Location = new System.Drawing.Point(17, 91);
            this.lblIssueTypeID.Name = "lblIssueTypeID";
            this.lblIssueTypeID.Size = new System.Drawing.Size(142, 13);
            this.lblIssueTypeID.TabIndex = 6;
            this.lblIssueTypeID.Text = "Асуудлын төрлийн дугаар :";
            // 
            // cboIssueTypeID
            // 
            this.cboIssueTypeID.Location = new System.Drawing.Point(163, 88);
            this.cboIssueTypeID.Name = "cboIssueTypeID";
            this.cboIssueTypeID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboIssueTypeID.Properties.NullText = "";
            this.cboIssueTypeID.Size = new System.Drawing.Size(129, 20);
            this.cboIssueTypeID.TabIndex = 3;
            this.cboIssueTypeID.ToolTipTitle = "Асуудлын төрлийн дугаар оруулна уу";
            // 
            // lblProjectCompID
            // 
            this.lblProjectCompID.Location = new System.Drawing.Point(17, 70);
            this.lblProjectCompID.Name = "lblProjectCompID";
            this.lblProjectCompID.Size = new System.Drawing.Size(111, 13);
            this.lblProjectCompID.TabIndex = 4;
            this.lblProjectCompID.Text = "Дэд төрлийн дугаар :";
            // 
            // lblProjectID
            // 
            this.lblProjectID.Location = new System.Drawing.Point(17, 49);
            this.lblProjectID.Name = "lblProjectID";
            this.lblProjectID.Size = new System.Drawing.Size(87, 13);
            this.lblProjectID.TabIndex = 2;
            this.lblProjectID.Text = "Төслийн дугаар :";
            // 
            // txtIssueID
            // 
            this.txtIssueID.Location = new System.Drawing.Point(163, 25);
            this.txtIssueID.Name = "txtIssueID";
            this.txtIssueID.Size = new System.Drawing.Size(129, 20);
            this.txtIssueID.TabIndex = 0;
            // 
            // lblIssueID
            // 
            this.lblIssueID.Location = new System.Drawing.Point(17, 28);
            this.lblIssueID.Name = "lblIssueID";
            this.lblIssueID.Size = new System.Drawing.Size(97, 13);
            this.lblIssueID.TabIndex = 0;
            this.lblIssueID.Text = "Асуудлын дугаар :";
            // 
            // cboProjectCompID
            // 
            this.cboProjectCompID.Location = new System.Drawing.Point(163, 67);
            this.cboProjectCompID.Name = "cboProjectCompID";
            this.cboProjectCompID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProjectCompID.Properties.NullText = "";
            this.cboProjectCompID.Size = new System.Drawing.Size(129, 20);
            this.cboProjectCompID.TabIndex = 2;
            this.cboProjectCompID.ToolTipTitle = "Дэд төрлийн дугаар оруулна уу";
            // 
            // cboProjectID
            // 
            this.cboProjectID.Location = new System.Drawing.Point(163, 46);
            this.cboProjectID.Name = "cboProjectID";
            this.cboProjectID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProjectID.Properties.NullText = "";
            this.cboProjectID.Size = new System.Drawing.Size(129, 20);
            this.cboProjectID.TabIndex = 1;
            this.cboProjectID.ToolTipTitle = "Төслийн дугаар оруулна уу";
            // 
            // dteCreateDate
            // 
            this.dteCreateDate.EditValue = null;
            this.dteCreateDate.Location = new System.Drawing.Point(492, 88);
            this.dteCreateDate.Name = "dteCreateDate";
            this.dteCreateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCreateDate.Properties.Mask.EditMask = "";
            this.dteCreateDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteCreateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCreateDate.Size = new System.Drawing.Size(129, 20);
            this.dteCreateDate.TabIndex = 10;
            this.dteCreateDate.ToolTipTitle = "Асуудал үүсгэсэн огноо оруулна уу";
            // 
            // dteUpdateDate
            // 
            this.dteUpdateDate.EditValue = null;
            this.dteUpdateDate.Location = new System.Drawing.Point(789, 88);
            this.dteUpdateDate.Name = "dteUpdateDate";
            this.dteUpdateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteUpdateDate.Properties.Mask.EditMask = "";
            this.dteUpdateDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteUpdateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteUpdateDate.Size = new System.Drawing.Size(129, 20);
            this.dteUpdateDate.TabIndex = 15;
            // 
            // cboResolutionStatus
            // 
            this.cboResolutionStatus.Location = new System.Drawing.Point(789, 109);
            this.cboResolutionStatus.Name = "cboResolutionStatus";
            this.cboResolutionStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboResolutionStatus.Properties.NullText = "";
            this.cboResolutionStatus.Size = new System.Drawing.Size(129, 20);
            this.cboResolutionStatus.TabIndex = 16;
            // 
            // cboTrackID
            // 
            this.cboTrackID.Location = new System.Drawing.Point(492, 46);
            this.cboTrackID.Name = "cboTrackID";
            this.cboTrackID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTrackID.Properties.NullText = "";
            this.cboTrackID.Size = new System.Drawing.Size(129, 20);
            this.cboTrackID.TabIndex = 8;
            this.cboTrackID.ToolTipTitle = "Шатлалын байдал оруулна уу";
            // 
            // cboAssigneeUser
            // 
            this.cboAssigneeUser.Location = new System.Drawing.Point(492, 67);
            this.cboAssigneeUser.Name = "cboAssigneeUser";
            this.cboAssigneeUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboAssigneeUser.Properties.NullText = "";
            this.cboAssigneeUser.Size = new System.Drawing.Size(129, 20);
            this.cboAssigneeUser.TabIndex = 9;
            this.cboAssigneeUser.ToolTipTitle = "Одоо хариуцаж байгаа хэрэглэгч оруулна уу";
            // 
            // FormIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 307);
            this.Controls.Add(this.ucToggleIssue);
            this.MaximumSize = new System.Drawing.Size(973, 345);
            this.MinimumSize = new System.Drawing.Size(965, 334);
            this.Name = "FormIssue";
            this.Text = "Асуудал";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIssue_FormClosing);
            this.Load += new System.EventHandler(this.FormIssue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).EndInit();
            this.ucToggleIssue.ResumeLayout(false);
            this.ucToggleIssue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupData)).EndInit();
            this.groupData.ResumeLayout(false);
            this.groupData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmeDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResolutionUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteResolutionDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteResolutionDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDueDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDueDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIssuePriorID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIssueTypeID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIssueID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectCompID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProjectID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteUpdateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteUpdateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResolutionStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrackID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAssigneeUser.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupData;
        private DevExpress.XtraEditors.LabelControl lblIssueID;
        private ISM.Template.ucTogglePanel ucToggleIssue;
        private DevExpress.XtraEditors.LabelControl lblUpdateDate;
        private DevExpress.XtraEditors.LabelControl lblCreateDate;
        private DevExpress.XtraEditors.TextEdit txtCreateUser;
        private DevExpress.XtraEditors.LabelControl lblCreateUser;
        private DevExpress.XtraEditors.LabelControl lblTrackID;
        private DevExpress.XtraEditors.LabelControl lblResolutionStatus;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl lblSubject;
        private DevExpress.XtraEditors.MemoEdit mmeSubject;
        private DevExpress.XtraEditors.LabelControl lblIssuePriorID;
        private DevExpress.XtraEditors.LookUpEdit cboIssuePriorID;
        private DevExpress.XtraEditors.LabelControl lblIssueTypeID;
        private DevExpress.XtraEditors.LookUpEdit cboIssueTypeID;
        private DevExpress.XtraEditors.LabelControl lblProjectCompID;
        private DevExpress.XtraEditors.LabelControl lblProjectID;
        private DevExpress.XtraEditors.TextEdit txtIssueID;
        private DevExpress.XtraEditors.LookUpEdit cboProjectCompID;
        private DevExpress.XtraEditors.LookUpEdit cboProjectID;
        private DevExpress.XtraEditors.TextEdit txtResolutionUser;
        private DevExpress.XtraEditors.LabelControl lblResolutionUser;
        private DevExpress.XtraEditors.LabelControl lblResolutionDate;
        private DevExpress.XtraEditors.LabelControl lblAssigneeUser;
        private DevExpress.XtraEditors.DateEdit dteResolutionDate;
        private DevExpress.XtraEditors.LabelControl lblDueDate;
        private DevExpress.XtraEditors.DateEdit dteDueDate;
        private DevExpress.XtraEditors.DateEdit dteCreateDate;
        private DevExpress.XtraEditors.DateEdit dteUpdateDate;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.MemoEdit mmeDescription;
        private DevExpress.XtraEditors.LookUpEdit cboResolutionStatus;
        private DevExpress.XtraEditors.LookUpEdit cboTrackID;
        private DevExpress.XtraEditors.LookUpEdit cboAssigneeUser;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorChecker;
    }
}