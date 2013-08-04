namespace InfoPos.Customer
{
    partial class OpenXml
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnGet = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtvalue = new DevExpress.XtraEditors.TextEdit();
            this.txtRegisterNo = new DevExpress.XtraEditors.TextEdit();
            this.btnHash = new DevExpress.XtraEditors.SimpleButton();
            this.btnMemory = new DevExpress.XtraEditors.SimpleButton();
            this.grdXML = new DevExpress.XtraGrid.GridControl();
            this.gvwXML = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.BtnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSaveAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtvalue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisterNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdXML)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwXML)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.groupControl1.Controls.Add(this.btnSaveAll);
            this.groupControl1.Controls.Add(this.btnGet);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.txtvalue);
            this.groupControl1.Controls.Add(this.txtRegisterNo);
            this.groupControl1.Controls.Add(this.btnHash);
            this.groupControl1.Controls.Add(this.btnMemory);
            this.groupControl1.Controls.Add(this.grdXML);
            this.groupControl1.Controls.Add(this.BtnClose);
            this.groupControl1.Controls.Add(this.btnBrowse);
            this.groupControl1.Controls.Add(this.BtnOpen);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtFileName);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(907, 371);
            this.groupControl1.TabIndex = 22;
            this.groupControl1.Text = "XML файл унших";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(592, 61);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(57, 23);
            this.btnGet.TabIndex = 29;
            this.btnGet.Text = "Get";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(466, 61);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(57, 23);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtvalue
            // 
            this.txtvalue.EditValue = "";
            this.txtvalue.Location = new System.Drawing.Point(167, 64);
            this.txtvalue.Name = "txtvalue";
            this.txtvalue.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Strong;
            this.txtvalue.Properties.Mask.BeepOnError = true;
            this.txtvalue.Properties.Mask.IgnoreMaskBlank = false;
            this.txtvalue.Properties.Mask.ShowPlaceHolders = false;
            this.txtvalue.Properties.MaxLength = 100;
            this.txtvalue.Properties.NullText = "Утгаа оруулна уу.";
            this.txtvalue.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtvalue.Properties.ValidateOnEnterKey = true;
            this.txtvalue.Size = new System.Drawing.Size(293, 20);
            this.txtvalue.TabIndex = 25;
            // 
            // txtRegisterNo
            // 
            this.txtRegisterNo.EditValue = "";
            this.txtRegisterNo.Location = new System.Drawing.Point(52, 64);
            this.txtRegisterNo.Name = "txtRegisterNo";
            this.txtRegisterNo.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Strong;
            this.txtRegisterNo.Properties.Mask.BeepOnError = true;
            this.txtRegisterNo.Properties.Mask.IgnoreMaskBlank = false;
            this.txtRegisterNo.Properties.Mask.ShowPlaceHolders = false;
            this.txtRegisterNo.Properties.MaxLength = 100;
            this.txtRegisterNo.Properties.NullText = "Утгаа оруулна уу.";
            this.txtRegisterNo.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtRegisterNo.Properties.ValidateOnEnterKey = true;
            this.txtRegisterNo.Size = new System.Drawing.Size(96, 20);
            this.txtRegisterNo.TabIndex = 24;
            // 
            // btnHash
            // 
            this.btnHash.Location = new System.Drawing.Point(655, 25);
            this.btnHash.Name = "btnHash";
            this.btnHash.Size = new System.Drawing.Size(57, 23);
            this.btnHash.TabIndex = 23;
            this.btnHash.Text = "Hash";
            // 
            // btnMemory
            // 
            this.btnMemory.Location = new System.Drawing.Point(592, 25);
            this.btnMemory.Name = "btnMemory";
            this.btnMemory.Size = new System.Drawing.Size(57, 23);
            this.btnMemory.TabIndex = 22;
            this.btnMemory.Text = "Memory";
            this.btnMemory.Click += new System.EventHandler(this.btnMemory_Click);
            // 
            // grdXML
            // 
            this.grdXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdXML.Location = new System.Drawing.Point(0, 134);
            this.grdXML.MainView = this.gvwXML;
            this.grdXML.Name = "grdXML";
            this.grdXML.Size = new System.Drawing.Size(907, 237);
            this.grdXML.TabIndex = 21;
            this.grdXML.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwXML});
            // 
            // gvwXML
            // 
            this.gvwXML.GridControl = this.grdXML;
            this.gvwXML.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwXML.Name = "gvwXML";
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(529, 25);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(57, 23);
            this.BtnClose.TabIndex = 17;
            this.BtnClose.Text = "Create";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(436, 25);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 23);
            this.btnBrowse.TabIndex = 16;
            this.btnBrowse.Text = "...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // BtnOpen
            // 
            this.BtnOpen.Location = new System.Drawing.Point(466, 25);
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(57, 23);
            this.BtnOpen.TabIndex = 15;
            this.BtnOpen.Text = "Open";
            this.BtnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 31);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(25, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Зам :";
            // 
            // txtFileName
            // 
            this.txtFileName.EditValue = "";
            this.txtFileName.Location = new System.Drawing.Point(52, 28);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Strong;
            this.txtFileName.Properties.Mask.BeepOnError = true;
            this.txtFileName.Properties.Mask.IgnoreMaskBlank = false;
            this.txtFileName.Properties.Mask.ShowPlaceHolders = false;
            this.txtFileName.Properties.MaxLength = 100;
            this.txtFileName.Properties.NullText = "Утгаа оруулна уу.";
            this.txtFileName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtFileName.Properties.ValidateOnEnterKey = true;
            this.txtFileName.Size = new System.Drawing.Size(378, 20);
            this.txtFileName.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.xml";
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Location = new System.Drawing.Point(529, 61);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(57, 23);
            this.btnSaveAll.TabIndex = 30;
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // OpenXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 371);
            this.Controls.Add(this.groupControl1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "OpenXml";
            this.Text = "XML файл унших";
            this.Load += new System.EventHandler(this.OpenXml_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtvalue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisterNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdXML)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwXML)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnClose;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.SimpleButton BtnOpen;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private DevExpress.XtraGrid.GridControl grdXML;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwXML;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.SimpleButton btnHash;
        private DevExpress.XtraEditors.SimpleButton btnMemory;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtvalue;
        private DevExpress.XtraEditors.TextEdit txtRegisterNo;
        private DevExpress.XtraEditors.SimpleButton btnGet;
        private DevExpress.XtraEditors.SimpleButton btnSaveAll;

    }
}

