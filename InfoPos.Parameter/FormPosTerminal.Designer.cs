namespace InfoPos.Parameter
{
    partial class FormPosTerminal
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPOSNo = new DevExpress.XtraEditors.TextEdit();
            this.txtPOSName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtPOSIP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPOSMAC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cboPOSType = new DevExpress.XtraEditors.LookUpEdit();
            this.txtPOSDesc = new DevExpress.XtraEditors.MemoEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnPayTypeRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayTypeAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayTypeDelete = new DevExpress.XtraEditors.SimpleButton();
            this.cboPayTypeID = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl40 = new DevExpress.XtraEditors.LabelControl();
            this.grdPayType = new DevExpress.XtraGrid.GridControl();
            this.gvwPayType = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSMAC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPOSType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPayTypeID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwPayType)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Size = new System.Drawing.Size(358, 421);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 443);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 443);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 443);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 443);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 443);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(670, 425);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(303, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 425);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(303, 425);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(308, 0);
            this.panelControl3.Size = new System.Drawing.Size(362, 425);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ПОС ын дугаар ";
            // 
            // txtPOSNo
            // 
            this.txtPOSNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOSNo.Location = new System.Drawing.Point(98, 14);
            this.txtPOSNo.MinimumSize = new System.Drawing.Size(177, 20);
            this.txtPOSNo.Name = "txtPOSNo";
            this.txtPOSNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPOSNo.Properties.MaxLength = 20;
            this.txtPOSNo.Size = new System.Drawing.Size(212, 20);
            this.txtPOSNo.TabIndex = 1;
            this.txtPOSNo.ToolTipTitle = "Пос ын дугаар оруулна уу.";
            // 
            // txtPOSName
            // 
            this.txtPOSName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOSName.Location = new System.Drawing.Point(98, 40);
            this.txtPOSName.MinimumSize = new System.Drawing.Size(177, 20);
            this.txtPOSName.Name = "txtPOSName";
            this.txtPOSName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPOSName.Properties.MaxLength = 100;
            this.txtPOSName.Size = new System.Drawing.Size(212, 20);
            this.txtPOSName.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "ПОС ын нэр ";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 69);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "ПОС ын тайлбар";
            // 
            // txtPOSIP
            // 
            this.txtPOSIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOSIP.Location = new System.Drawing.Point(98, 129);
            this.txtPOSIP.MinimumSize = new System.Drawing.Size(177, 20);
            this.txtPOSIP.Name = "txtPOSIP";
            this.txtPOSIP.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPOSIP.Properties.MaxLength = 20;
            this.txtPOSIP.Size = new System.Drawing.Size(212, 20);
            this.txtPOSIP.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 132);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "ПОС ын IP";
            // 
            // txtPOSMAC
            // 
            this.txtPOSMAC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOSMAC.Location = new System.Drawing.Point(98, 155);
            this.txtPOSMAC.MinimumSize = new System.Drawing.Size(177, 20);
            this.txtPOSMAC.Name = "txtPOSMAC";
            this.txtPOSMAC.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPOSMAC.Properties.MaxLength = 20;
            this.txtPOSMAC.Size = new System.Drawing.Size(212, 20);
            this.txtPOSMAC.TabIndex = 9;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(11, 158);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "ПОС ын MAC";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(11, 184);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "ПОС ын төрөл";
            // 
            // cboPOSType
            // 
            this.cboPOSType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPOSType.Location = new System.Drawing.Point(98, 181);
            this.cboPOSType.MinimumSize = new System.Drawing.Size(177, 20);
            this.cboPOSType.Name = "cboPOSType";
            this.cboPOSType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPOSType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboPOSType.Size = new System.Drawing.Size(212, 20);
            this.cboPOSType.TabIndex = 11;
            this.cboPOSType.ToolTipTitle = "Пос ын төрөл оруулна уу.";
            // 
            // txtPOSDesc
            // 
            this.txtPOSDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOSDesc.Location = new System.Drawing.Point(98, 68);
            this.txtPOSDesc.MinimumSize = new System.Drawing.Size(177, 55);
            this.txtPOSDesc.Name = "txtPOSDesc";
            this.txtPOSDesc.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPOSDesc.Properties.MaxLength = 500;
            this.txtPOSDesc.Size = new System.Drawing.Size(212, 55);
            this.txtPOSDesc.TabIndex = 4;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 22);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(354, 397);
            this.xtraTabControl1.TabIndex = 12;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.Selected += new DevExpress.XtraTab.TabPageEventHandler(this.xtraTabControl1_Selected);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.txtPOSNo);
            this.xtraTabPage1.Controls.Add(this.txtPOSDesc);
            this.xtraTabPage1.Controls.Add(this.labelControl1);
            this.xtraTabPage1.Controls.Add(this.cboPOSType);
            this.xtraTabPage1.Controls.Add(this.labelControl2);
            this.xtraTabPage1.Controls.Add(this.labelControl6);
            this.xtraTabPage1.Controls.Add(this.txtPOSName);
            this.xtraTabPage1.Controls.Add(this.txtPOSMAC);
            this.xtraTabPage1.Controls.Add(this.labelControl3);
            this.xtraTabPage1.Controls.Add(this.labelControl5);
            this.xtraTabPage1.Controls.Add(this.labelControl4);
            this.xtraTabPage1.Controls.Add(this.txtPOSIP);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(348, 370);
            this.xtraTabPage1.Text = "POS ын өгөгдөл";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.panelControl4);
            this.xtraTabPage2.Controls.Add(this.grdPayType);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(348, 370);
            this.xtraTabPage2.Text = "POS дээрх төлбөрийн төрлийн өгөгдөл";
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.btnPayTypeRefresh);
            this.panelControl4.Controls.Add(this.btnPayTypeAdd);
            this.panelControl4.Controls.Add(this.btnPayTypeDelete);
            this.panelControl4.Controls.Add(this.cboPayTypeID);
            this.panelControl4.Controls.Add(this.labelControl40);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 236);
            this.panelControl4.MaximumSize = new System.Drawing.Size(344, 206);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(344, 134);
            this.panelControl4.TabIndex = 360;
            // 
            // btnPayTypeRefresh
            // 
            this.btnPayTypeRefresh.Location = new System.Drawing.Point(5, 32);
            this.btnPayTypeRefresh.Name = "btnPayTypeRefresh";
            this.btnPayTypeRefresh.Size = new System.Drawing.Size(67, 26);
            this.btnPayTypeRefresh.TabIndex = 348;
            this.btnPayTypeRefresh.Text = "Сэргээх";
            this.btnPayTypeRefresh.Click += new System.EventHandler(this.btnPayTypeRefresh_Click);
            // 
            // btnPayTypeAdd
            // 
            this.btnPayTypeAdd.Location = new System.Drawing.Point(91, 32);
            this.btnPayTypeAdd.Name = "btnPayTypeAdd";
            this.btnPayTypeAdd.Size = new System.Drawing.Size(67, 26);
            this.btnPayTypeAdd.TabIndex = 6;
            this.btnPayTypeAdd.Text = "Нэмэх";
            this.btnPayTypeAdd.Click += new System.EventHandler(this.btnPayTypeAdd_Click);
            // 
            // btnPayTypeDelete
            // 
            this.btnPayTypeDelete.Location = new System.Drawing.Point(177, 32);
            this.btnPayTypeDelete.Name = "btnPayTypeDelete";
            this.btnPayTypeDelete.Size = new System.Drawing.Size(67, 26);
            this.btnPayTypeDelete.TabIndex = 8;
            this.btnPayTypeDelete.Text = "Устгах";
            this.btnPayTypeDelete.Click += new System.EventHandler(this.btnPayTypeDelete_Click);
            // 
            // cboPayTypeID
            // 
            this.cboPayTypeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPayTypeID.Location = new System.Drawing.Point(98, 6);
            this.cboPayTypeID.Name = "cboPayTypeID";
            this.cboPayTypeID.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboPayTypeID.Properties.Appearance.Options.UseBackColor = true;
            this.cboPayTypeID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPayTypeID.Properties.DisplayFormat.FormatString = "d";
            this.cboPayTypeID.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cboPayTypeID.Properties.EditFormat.FormatString = "d";
            this.cboPayTypeID.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cboPayTypeID.Properties.NullText = "";
            this.cboPayTypeID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboPayTypeID.Size = new System.Drawing.Size(241, 20);
            this.cboPayTypeID.TabIndex = 3;
            this.cboPayTypeID.ToolTipTitle = "Эрсдэлийн үнэлгээний хэмжээ оруулна уу.";
            // 
            // labelControl40
            // 
            this.labelControl40.Location = new System.Drawing.Point(5, 13);
            this.labelControl40.Name = "labelControl40";
            this.labelControl40.Size = new System.Drawing.Size(87, 13);
            this.labelControl40.TabIndex = 347;
            this.labelControl40.Text = "Төлбөрийн төрөл";
            // 
            // grdPayType
            // 
            this.grdPayType.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdPayType.Location = new System.Drawing.Point(0, 0);
            this.grdPayType.MainView = this.gvwPayType;
            this.grdPayType.Name = "grdPayType";
            this.grdPayType.Size = new System.Drawing.Size(348, 236);
            this.grdPayType.TabIndex = 2;
            this.grdPayType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwPayType});
            // 
            // gvwPayType
            // 
            this.gvwPayType.GridControl = this.grdPayType;
            this.gvwPayType.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.gvwPayType.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwPayType.Name = "gvwPayType";
            // 
            // FormPosTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 476);
            this.MinimumSize = new System.Drawing.Size(664, 417);
            this.Name = "FormPosTerminal";
            this.Text = "POS ын бүртгэл";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSMAC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPOSType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOSDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPayTypeID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwPayType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtPOSNo;
        private DevExpress.XtraEditors.LookUpEdit cboPOSType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtPOSMAC;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtPOSIP;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtPOSName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit txtPOSDesc;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnPayTypeRefresh;
        private DevExpress.XtraEditors.SimpleButton btnPayTypeAdd;
        private DevExpress.XtraEditors.SimpleButton btnPayTypeDelete;
        private DevExpress.XtraEditors.LookUpEdit cboPayTypeID;
        private DevExpress.XtraEditors.LabelControl labelControl40;
        private DevExpress.XtraGrid.GridControl grdPayType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwPayType;
    }
}