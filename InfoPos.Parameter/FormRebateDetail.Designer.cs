namespace InfoPos.Parameter
{
    partial class FormRebateDetail
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.numMasterID = new DevExpress.XtraEditors.TextEdit();
            this.cboProdType = new DevExpress.XtraEditors.LookUpEdit();
            this.numCalcAmount = new DevExpress.XtraEditors.TextEdit();
            this.cboProdNo = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.cboCalcType = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMasterID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProdType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCalcAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProdNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalcType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboCalcType);
            this.groupControl1.Controls.Add(this.btnSearch);
            this.groupControl1.Controls.Add(this.cboProdNo);
            this.groupControl1.Controls.Add(this.numCalcAmount);
            this.groupControl1.Controls.Add(this.cboProdType);
            this.groupControl1.Controls.Add(this.numMasterID);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ID дугаар";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(134, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Бараа үйлчилгээний төрөл";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(119, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Бараа үйлчилгээний ID.";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(18, 111);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(87, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Тооцоолох төрөл";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 135);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(125, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Хувь эсвэл дүнг оруулна";
            // 
            // numMasterID
            // 
            this.numMasterID.Location = new System.Drawing.Point(158, 33);
            this.numMasterID.Name = "numMasterID";
            this.numMasterID.Size = new System.Drawing.Size(185, 20);
            this.numMasterID.TabIndex = 5;
            // 
            // cboProdType
            // 
            this.cboProdType.Location = new System.Drawing.Point(158, 57);
            this.cboProdType.Name = "cboProdType";
            this.cboProdType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboProdType.Size = new System.Drawing.Size(185, 20);
            this.cboProdType.TabIndex = 6;
            // 
            // numCalcAmount
            // 
            this.numCalcAmount.Location = new System.Drawing.Point(158, 132);
            this.numCalcAmount.Name = "numCalcAmount";
            this.numCalcAmount.Size = new System.Drawing.Size(185, 20);
            this.numCalcAmount.TabIndex = 9;
            // 
            // cboProdNo
            // 
            this.cboProdNo.Location = new System.Drawing.Point(158, 83);
            this.cboProdNo.Name = "cboProdNo";
            this.cboProdNo.Size = new System.Drawing.Size(104, 20);
            this.cboProdNo.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(268, 81);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Хайх";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboCalcType
            // 
            this.cboCalcType.Location = new System.Drawing.Point(158, 108);
            this.cboCalcType.Name = "cboCalcType";
            this.cboCalcType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCalcType.Size = new System.Drawing.Size(185, 20);
            this.cboCalcType.TabIndex = 12;
            // 
            // FormRebateDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 379);
            this.Name = "FormRebateDetail";
            this.Text = "ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMasterID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProdType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCalcAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboProdNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCalcType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit numCalcAmount;
        private DevExpress.XtraEditors.LookUpEdit cboProdType;
        private DevExpress.XtraEditors.TextEdit numMasterID;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.TextEdit cboProdNo;
        private DevExpress.XtraEditors.LookUpEdit cboCalcType;
    }
}