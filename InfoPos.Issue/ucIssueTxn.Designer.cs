namespace InfoPos.Issue
{
    partial class ucIssueTxn
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
            this.pnlName = new DevExpress.XtraEditors.PanelControl();
            this.btnExCo = new DevExpress.XtraEditors.SimpleButton();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlData = new DevExpress.XtraEditors.PanelControl();
            this.nbcIssueTxn = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgIssueMain = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lblContactType = new DevExpress.XtraEditors.LabelControl();
            this.cboType = new DevExpress.XtraEditors.LookUpEdit();
            this.lblTxnType = new DevExpress.XtraEditors.LabelControl();
            this.cboContactType = new DevExpress.XtraEditors.LookUpEdit();
            this.lblTxnComment = new DevExpress.XtraEditors.LabelControl();
            this.mmeComment = new DevExpress.XtraEditors.MemoEdit();
            this.btnFile = new DevExpress.XtraEditors.SimpleButton();
            this.cboTrackID = new DevExpress.XtraEditors.LookUpEdit();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.lblTrackID = new DevExpress.XtraEditors.LabelControl();
            this.lblSubject = new DevExpress.XtraEditors.LabelControl();
            this.lblResolutionTypeID = new DevExpress.XtraEditors.LabelControl();
            this.cboResolutionTypeID = new DevExpress.XtraEditors.LookUpEdit();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.lblNextDate = new DevExpress.XtraEditors.LabelControl();
            this.mmeNextPurpose = new DevExpress.XtraEditors.MemoEdit();
            this.dteNextDate = new DevExpress.XtraEditors.DateEdit();
            this.lblNextPurpose = new DevExpress.XtraEditors.LabelControl();
            this.nbgNext = new DevExpress.XtraNavBar.NavBarGroup();
            this.checkReport = new DevExpress.XtraEditors.CheckEdit();
            this.ErrorChecker = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.pnlName)).BeginInit();
            this.pnlName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlData)).BeginInit();
            this.pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbcIssueTxn)).BeginInit();
            this.nbcIssueTxn.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboContactType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrackID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResolutionTypeID.Properties)).BeginInit();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNextPurpose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNextDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNextDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlName
            // 
            this.pnlName.Controls.Add(this.btnExCo);
            this.pnlName.Controls.Add(this.lblStatus);
            this.pnlName.Location = new System.Drawing.Point(0, 0);
            this.pnlName.MaximumSize = new System.Drawing.Size(190, 20);
            this.pnlName.MinimumSize = new System.Drawing.Size(190, 20);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(190, 20);
            this.pnlName.TabIndex = 7;
            // 
            // btnExCo
            // 
            this.btnExCo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExCo.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExCo.Location = new System.Drawing.Point(169, 2);
            this.btnExCo.Name = "btnExCo";
            this.btnExCo.Size = new System.Drawing.Size(16, 16);
            this.btnExCo.TabIndex = 2;
            this.btnExCo.Click += new System.EventHandler(this.btnExCo_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(5, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.btnEnter.Appearance.Options.UseFont = true;
            this.btnEnter.Image = global::InfoPos.Issue.Resource.ok;
            this.btnEnter.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnEnter.Location = new System.Drawing.Point(365, 240);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(16, 16);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.ToolTip = "Шилжүүлэх";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Image = global::InfoPos.Issue.Resource.close;
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(387, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(16, 16);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.ToolTip = "Болих";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.nbcIssueTxn);
            this.pnlData.Controls.Add(this.btnEnter);
            this.pnlData.Controls.Add(this.btnCancel);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlData.Location = new System.Drawing.Point(0, 25);
            this.pnlData.MaximumSize = new System.Drawing.Size(408, 260);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(408, 260);
            this.pnlData.TabIndex = 9;
            // 
            // nbcIssueTxn
            // 
            this.nbcIssueTxn.ActiveGroup = this.nbgIssueMain;
            this.nbcIssueTxn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nbcIssueTxn.Controls.Add(this.navBarGroupControlContainer1);
            this.nbcIssueTxn.Controls.Add(this.navBarGroupControlContainer2);
            this.nbcIssueTxn.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbcIssueTxn.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgIssueMain,
            this.nbgNext});
            this.nbcIssueTxn.Location = new System.Drawing.Point(3, 3);
            this.nbcIssueTxn.MaximumSize = new System.Drawing.Size(402, 236);
            this.nbcIssueTxn.Name = "nbcIssueTxn";
            this.nbcIssueTxn.OptionsNavPane.ExpandedWidth = 460;
            this.nbcIssueTxn.Size = new System.Drawing.Size(402, 236);
            this.nbcIssueTxn.TabIndex = 9;
            this.nbcIssueTxn.GroupExpanded += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.nbcIssueTxn_GroupExpanded);
            this.nbcIssueTxn.GroupCollapsed += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.nbcIssueTxn_GroupCollapsed);
            // 
            // nbgIssueMain
            // 
            this.nbgIssueMain.Appearance.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbgIssueMain.Appearance.Options.UseFont = true;
            this.nbgIssueMain.AppearanceBackground.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbgIssueMain.AppearanceBackground.Options.UseFont = true;
            this.nbgIssueMain.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbgIssueMain.AppearanceHotTracked.Options.UseFont = true;
            this.nbgIssueMain.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbgIssueMain.AppearancePressed.Options.UseFont = true;
            this.nbgIssueMain.Caption = "";
            this.nbgIssueMain.ControlContainer = this.navBarGroupControlContainer1;
            this.nbgIssueMain.Expanded = true;
            this.nbgIssueMain.GroupClientHeight = 178;
            this.nbgIssueMain.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbgIssueMain.Name = "nbgIssueMain";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.lblContactType);
            this.navBarGroupControlContainer1.Controls.Add(this.cboType);
            this.navBarGroupControlContainer1.Controls.Add(this.lblTxnType);
            this.navBarGroupControlContainer1.Controls.Add(this.cboContactType);
            this.navBarGroupControlContainer1.Controls.Add(this.lblTxnComment);
            this.navBarGroupControlContainer1.Controls.Add(this.mmeComment);
            this.navBarGroupControlContainer1.Controls.Add(this.btnFile);
            this.navBarGroupControlContainer1.Controls.Add(this.cboTrackID);
            this.navBarGroupControlContainer1.Controls.Add(this.txtSubject);
            this.navBarGroupControlContainer1.Controls.Add(this.lblTrackID);
            this.navBarGroupControlContainer1.Controls.Add(this.lblSubject);
            this.navBarGroupControlContainer1.Controls.Add(this.lblResolutionTypeID);
            this.navBarGroupControlContainer1.Controls.Add(this.cboResolutionTypeID);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(394, 171);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // lblContactType
            // 
            this.lblContactType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblContactType.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lblContactType.Location = new System.Drawing.Point(0, 89);
            this.lblContactType.Name = "lblContactType";
            this.lblContactType.Size = new System.Drawing.Size(105, 11);
            this.lblContactType.TabIndex = 63;
            this.lblContactType.Text = "Холбоо барисан төрөл  :";
            // 
            // cboType
            // 
            this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboType.Location = new System.Drawing.Point(145, 3);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.cboType.Properties.Appearance.Options.UseFont = true;
            this.cboType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.Properties.NullText = "";
            this.cboType.Size = new System.Drawing.Size(249, 18);
            this.cboType.TabIndex = 51;
            this.cboType.ToolTipTitle = "Гүйлгээний төрөл оруулна уу";
            this.cboType.EditValueChanged += new System.EventHandler(this.cboType_EditValueChanged);
            // 
            // lblTxnType
            // 
            this.lblTxnType.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lblTxnType.Location = new System.Drawing.Point(0, 6);
            this.lblTxnType.Name = "lblTxnType";
            this.lblTxnType.Size = new System.Drawing.Size(81, 11);
            this.lblTxnType.TabIndex = 53;
            this.lblTxnType.Text = "Гүйлгээний төрөл :";
            // 
            // cboContactType
            // 
            this.cboContactType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboContactType.Location = new System.Drawing.Point(145, 86);
            this.cboContactType.Name = "cboContactType";
            this.cboContactType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.cboContactType.Properties.Appearance.Options.UseFont = true;
            this.cboContactType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboContactType.Properties.NullText = "";
            this.cboContactType.Size = new System.Drawing.Size(249, 18);
            this.cboContactType.TabIndex = 62;
            this.cboContactType.ToolTipTitle = "Холбоо барисан оруулна уу";
            // 
            // lblTxnComment
            // 
            this.lblTxnComment.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lblTxnComment.Location = new System.Drawing.Point(0, 43);
            this.lblTxnComment.Name = "lblTxnComment";
            this.lblTxnComment.Size = new System.Drawing.Size(91, 11);
            this.lblTxnComment.TabIndex = 55;
            this.lblTxnComment.Text = "Гүйлгээний тайлбар :";
            // 
            // mmeComment
            // 
            this.mmeComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mmeComment.Location = new System.Drawing.Point(145, 40);
            this.mmeComment.Name = "mmeComment";
            this.mmeComment.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.mmeComment.Properties.Appearance.Options.UseFont = true;
            this.mmeComment.Size = new System.Drawing.Size(249, 45);
            this.mmeComment.TabIndex = 54;
            this.mmeComment.ToolTipTitle = "Гүйлгээний тайлбар оруулна уу";
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFile.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.btnFile.Appearance.Options.UseFont = true;
            this.btnFile.Location = new System.Drawing.Point(145, 105);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(118, 21);
            this.btnFile.TabIndex = 56;
            this.btnFile.Text = "Файл оруулах";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // cboTrackID
            // 
            this.cboTrackID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTrackID.Location = new System.Drawing.Point(145, 127);
            this.cboTrackID.Name = "cboTrackID";
            this.cboTrackID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.cboTrackID.Properties.Appearance.Options.UseFont = true;
            this.cboTrackID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTrackID.Properties.NullText = "";
            this.cboTrackID.Size = new System.Drawing.Size(249, 18);
            this.cboTrackID.TabIndex = 57;
            this.cboTrackID.ToolTipTitle = "Шатлалын байдал оруулна уу";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(145, 22);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.txtSubject.Properties.Appearance.Options.UseFont = true;
            this.txtSubject.Size = new System.Drawing.Size(249, 17);
            this.txtSubject.TabIndex = 52;
            this.txtSubject.ToolTipTitle = "Гүйлгээний товч утга оруулна уу";
            // 
            // lblTrackID
            // 
            this.lblTrackID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrackID.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lblTrackID.Location = new System.Drawing.Point(0, 130);
            this.lblTrackID.Name = "lblTrackID";
            this.lblTrackID.Size = new System.Drawing.Size(84, 11);
            this.lblTrackID.TabIndex = 59;
            this.lblTrackID.Text = "Шатлалын байдал :";
            // 
            // lblSubject
            // 
            this.lblSubject.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lblSubject.Location = new System.Drawing.Point(0, 25);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(100, 11);
            this.lblSubject.TabIndex = 60;
            this.lblSubject.Text = "Гүйлгээний товч утга :";
            // 
            // lblResolutionTypeID
            // 
            this.lblResolutionTypeID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResolutionTypeID.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lblResolutionTypeID.Location = new System.Drawing.Point(0, 149);
            this.lblResolutionTypeID.Name = "lblResolutionTypeID";
            this.lblResolutionTypeID.Size = new System.Drawing.Size(113, 11);
            this.lblResolutionTypeID.TabIndex = 61;
            this.lblResolutionTypeID.Text = "Асуудал хаагдсан төлөв :";
            // 
            // cboResolutionTypeID
            // 
            this.cboResolutionTypeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboResolutionTypeID.Location = new System.Drawing.Point(145, 146);
            this.cboResolutionTypeID.Name = "cboResolutionTypeID";
            this.cboResolutionTypeID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.cboResolutionTypeID.Properties.Appearance.Options.UseFont = true;
            this.cboResolutionTypeID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboResolutionTypeID.Properties.NullText = "";
            this.cboResolutionTypeID.Size = new System.Drawing.Size(249, 18);
            this.cboResolutionTypeID.TabIndex = 58;
            this.cboResolutionTypeID.ToolTipTitle = "Асуудал хаагдсан төлөв оруулна уу";
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.lblNextDate);
            this.navBarGroupControlContainer2.Controls.Add(this.mmeNextPurpose);
            this.navBarGroupControlContainer2.Controls.Add(this.dteNextDate);
            this.navBarGroupControlContainer2.Controls.Add(this.lblNextPurpose);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(394, 130);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // lblNextDate
            // 
            this.lblNextDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNextDate.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lblNextDate.Location = new System.Drawing.Point(0, 109);
            this.lblNextDate.Name = "lblNextDate";
            this.lblNextDate.Size = new System.Drawing.Size(117, 11);
            this.lblNextDate.TabIndex = 46;
            this.lblNextDate.Text = "Дараагийн ярилцах огноо :";
            // 
            // mmeNextPurpose
            // 
            this.mmeNextPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mmeNextPurpose.Enabled = false;
            this.mmeNextPurpose.Location = new System.Drawing.Point(145, 3);
            this.mmeNextPurpose.Name = "mmeNextPurpose";
            this.mmeNextPurpose.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.mmeNextPurpose.Properties.Appearance.Options.UseFont = true;
            this.mmeNextPurpose.Size = new System.Drawing.Size(246, 100);
            this.mmeNextPurpose.TabIndex = 42;
            this.mmeNextPurpose.ToolTipTitle = "Дараа хийх ажлын төлөвлөгөө оруулна уу";
            // 
            // dteNextDate
            // 
            this.dteNextDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteNextDate.EditValue = null;
            this.dteNextDate.Enabled = false;
            this.dteNextDate.Location = new System.Drawing.Point(145, 106);
            this.dteNextDate.Name = "dteNextDate";
            this.dteNextDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.dteNextDate.Properties.Appearance.Options.UseFont = true;
            this.dteNextDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteNextDate.Properties.Mask.EditMask = "";
            this.dteNextDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteNextDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteNextDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteNextDate.Size = new System.Drawing.Size(246, 18);
            this.dteNextDate.TabIndex = 43;
            this.dteNextDate.ToolTipTitle = "Дараагийн ярилцах огноо оруулна уу";
            // 
            // lblNextPurpose
            // 
            this.lblNextPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNextPurpose.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lblNextPurpose.Location = new System.Drawing.Point(0, 6);
            this.lblNextPurpose.Name = "lblNextPurpose";
            this.lblNextPurpose.Size = new System.Drawing.Size(137, 11);
            this.lblNextPurpose.TabIndex = 45;
            this.lblNextPurpose.Text = "Дараагийн ажлын төлөвлөгөө :";
            // 
            // nbgNext
            // 
            this.nbgNext.Appearance.Font = new System.Drawing.Font("Tahoma", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbgNext.Appearance.Options.UseFont = true;
            this.nbgNext.Caption = "";
            this.nbgNext.ControlContainer = this.navBarGroupControlContainer2;
            this.nbgNext.GroupClientHeight = 137;
            this.nbgNext.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbgNext.Name = "nbgNext";
            // 
            // checkReport
            // 
            this.checkReport.Location = new System.Drawing.Point(196, 1);
            this.checkReport.Name = "checkReport";
            this.checkReport.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.checkReport.Properties.Appearance.Options.UseFont = true;
            this.checkReport.Properties.Caption = "Ажлын төлөвлөгөөг оруулах эсэх";
            this.checkReport.Size = new System.Drawing.Size(199, 19);
            this.checkReport.TabIndex = 41;
            this.checkReport.CheckedChanged += new System.EventHandler(this.checkReport_CheckedChanged);
            // 
            // ErrorChecker
            // 
            this.ErrorChecker.ContainerControl = this;
            // 
            // ucIssueTxn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlName);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.checkReport);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.MaximumSize = new System.Drawing.Size(408, 285);
            this.MinimumSize = new System.Drawing.Size(190, 20);
            this.Name = "ucIssueTxn";
            this.Size = new System.Drawing.Size(408, 285);
            this.Tag = "";
            this.Load += new System.EventHandler(this.ucIssueTxn_Load);
            this.Leave += new System.EventHandler(this.ucIssueTxn_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.pnlName)).EndInit();
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlData)).EndInit();
            this.pnlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nbcIssueTxn)).EndInit();
            this.nbcIssueTxn.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboContactType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrackID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResolutionTypeID.Properties)).EndInit();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNextPurpose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNextDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNextDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorChecker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlName;
        private DevExpress.XtraEditors.SimpleButton btnExCo;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        public DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl pnlData;
        private DevExpress.XtraNavBar.NavBarControl nbcIssueTxn;
        private DevExpress.XtraNavBar.NavBarGroup nbgIssueMain;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraEditors.LabelControl lblContactType;
        private DevExpress.XtraEditors.LookUpEdit cboType;
        private DevExpress.XtraEditors.LabelControl lblTxnType;
        private DevExpress.XtraEditors.LookUpEdit cboContactType;
        private DevExpress.XtraEditors.LabelControl lblTxnComment;
        private DevExpress.XtraEditors.MemoEdit mmeComment;
        private DevExpress.XtraEditors.SimpleButton btnFile;
        private DevExpress.XtraEditors.LookUpEdit cboTrackID;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.CheckEdit checkReport;
        private DevExpress.XtraEditors.LabelControl lblTrackID;
        private DevExpress.XtraEditors.LabelControl lblSubject;
        private DevExpress.XtraEditors.LabelControl lblResolutionTypeID;
        private DevExpress.XtraEditors.LookUpEdit cboResolutionTypeID;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private DevExpress.XtraEditors.LabelControl lblNextDate;
        private DevExpress.XtraEditors.MemoEdit mmeNextPurpose;
        private DevExpress.XtraEditors.DateEdit dteNextDate;
        private DevExpress.XtraEditors.LabelControl lblNextPurpose;
        private DevExpress.XtraNavBar.NavBarGroup nbgNext;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorChecker;
    }
}
