namespace InfoPos.Contract
{
    partial class frmContractBulk
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.grdContractMain = new DevExpress.XtraGrid.GridControl();
            this.gvwContractMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.txtRowNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboType = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.txtAutoNum = new DevExpress.XtraEditors.LabelControl();
            this.txtAutoNumPrefix = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdContractMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwContractMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoNumPrefix.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdContractMain);
            this.panelControl1.Controls.Add(this.panelControl5);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(896, 477);
            this.panelControl1.TabIndex = 1;
            // 
            // grdContractMain
            // 
            this.grdContractMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdContractMain.Location = new System.Drawing.Point(2, 85);
            this.grdContractMain.MainView = this.gvwContractMain;
            this.grdContractMain.Name = "grdContractMain";
            this.grdContractMain.Size = new System.Drawing.Size(892, 390);
            this.grdContractMain.TabIndex = 1;
            this.grdContractMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwContractMain});
            // 
            // gvwContractMain
            // 
            this.gvwContractMain.GridControl = this.grdContractMain;
            this.gvwContractMain.Name = "gvwContractMain";
            // 
            // panelControl5
            // 
            this.panelControl5.Controls.Add(this.txtAutoNum);
            this.panelControl5.Controls.Add(this.txtAutoNumPrefix);
            this.panelControl5.Controls.Add(this.txtRowNo);
            this.panelControl5.Controls.Add(this.labelControl2);
            this.panelControl5.Controls.Add(this.cboType);
            this.panelControl5.Controls.Add(this.btnSave);
            this.panelControl5.Controls.Add(this.btnBrowse);
            this.panelControl5.Controls.Add(this.labelControl1);
            this.panelControl5.Controls.Add(this.labelControl17);
            this.panelControl5.Controls.Add(this.txtFileName);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(2, 2);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(892, 83);
            this.panelControl5.TabIndex = 2;
            // 
            // txtRowNo
            // 
            this.txtRowNo.EditValue = "1";
            this.txtRowNo.Location = new System.Drawing.Point(645, 5);
            this.txtRowNo.Name = "txtRowNo";
            this.txtRowNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRowNo.Size = new System.Drawing.Size(59, 20);
            this.txtRowNo.TabIndex = 14;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(534, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(108, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Эхлэх мөрийн дугаар";
            // 
            // cboType
            // 
            this.cboType.Location = new System.Drawing.Point(108, 5);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.Size = new System.Drawing.Size(420, 20);
            this.cboType.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(791, 27);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(710, 27);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = ". . .";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Төрлөө сонгоно уу";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(5, 33);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(96, 13);
            this.labelControl17.TabIndex = 3;
            this.labelControl17.Text = "Файлаа сонгоно уу";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(108, 29);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(596, 20);
            this.txtFileName.TabIndex = 2;
            // 
            // txtAutoNum
            // 
            this.txtAutoNum.Location = new System.Drawing.Point(5, 59);
            this.txtAutoNum.Name = "txtAutoNum";
            this.txtAutoNum.Size = new System.Drawing.Size(102, 13);
            this.txtAutoNum.TabIndex = 16;
            this.txtAutoNum.Text = "Дугаарлалт тэмдэг:";
            // 
            // txtAutoNumPrefix
            // 
            this.txtAutoNumPrefix.Location = new System.Drawing.Point(108, 55);
            this.txtAutoNumPrefix.Name = "txtAutoNumPrefix";
            this.txtAutoNumPrefix.Size = new System.Drawing.Size(53, 20);
            this.txtAutoNumPrefix.TabIndex = 15;
            // 
            // frmContractBulk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 477);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmContractBulk";
            this.Text = "Гэрээг xls ээс оруулах";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmContractBulk_FormClosed);
            this.Load += new System.EventHandler(this.frmContractBulk_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdContractMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwContractMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            this.panelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRowNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAutoNumPrefix.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grdContractMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwContractMain;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.LookUpEdit cboType;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBrowse;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txtFileName;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtRowNo;
        private DevExpress.XtraEditors.LabelControl txtAutoNum;
        private DevExpress.XtraEditors.TextEdit txtAutoNumPrefix;

    }
}

