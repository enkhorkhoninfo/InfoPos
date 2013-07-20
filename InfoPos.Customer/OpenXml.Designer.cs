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
            this.grdAccountList = new DevExpress.XtraGrid.GridControl();
            this.gvwAccountList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.BtnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAccountList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwAccountList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.groupControl1.Controls.Add(this.grdAccountList);
            this.groupControl1.Controls.Add(this.BtnClose);
            this.groupControl1.Controls.Add(this.btnBrowse);
            this.groupControl1.Controls.Add(this.BtnOpen);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtFileName);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(776, 371);
            this.groupControl1.TabIndex = 22;
            this.groupControl1.Text = "XML файл унших";
            // 
            // grdAccountList
            // 
            this.grdAccountList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAccountList.Location = new System.Drawing.Point(0, 66);
            this.grdAccountList.MainView = this.gvwAccountList;
            this.grdAccountList.Name = "grdAccountList";
            this.grdAccountList.Size = new System.Drawing.Size(776, 305);
            this.grdAccountList.TabIndex = 21;
            this.grdAccountList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwAccountList});
            // 
            // gvwAccountList
            // 
            this.gvwAccountList.GridControl = this.grdAccountList;
            this.gvwAccountList.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwAccountList.Name = "gvwAccountList";
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(553, 25);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(57, 23);
            this.BtnClose.TabIndex = 17;
            this.BtnClose.Text = "Close";
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
            this.BtnOpen.Location = new System.Drawing.Point(481, 25);
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(57, 23);
            this.BtnOpen.TabIndex = 15;
            this.BtnOpen.Text = "Open";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(10, 31);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(25, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Зам :";
            // 
            // txtFileName
            // 
            this.txtFileName.EditValue = "";
            this.txtFileName.Location = new System.Drawing.Point(109, 28);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Strong;
            this.txtFileName.Properties.Mask.BeepOnError = true;
            this.txtFileName.Properties.Mask.IgnoreMaskBlank = false;
            this.txtFileName.Properties.Mask.ShowPlaceHolders = false;
            this.txtFileName.Properties.MaxLength = 100;
            this.txtFileName.Properties.NullText = "Утгаа оруулна уу.";
            this.txtFileName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtFileName.Properties.ValidateOnEnterKey = true;
            this.txtFileName.Size = new System.Drawing.Size(321, 20);
            this.txtFileName.TabIndex = 4;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OpenXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 371);
            this.Controls.Add(this.groupControl1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "OpenXml";
            this.Text = "XML файл унших";
            this.Load += new System.EventHandler(this.OpenXml_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAccountList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwAccountList)).EndInit();
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
        private DevExpress.XtraGrid.GridControl grdAccountList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwAccountList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

    }
}

