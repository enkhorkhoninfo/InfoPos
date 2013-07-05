namespace InfoPos.Parameter
{
    partial class FormDynamicList
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
            this.LabelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LabelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.LabelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtKey = new DevExpress.XtraEditors.TextEdit();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.numRate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtFormula = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormula.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtFormula);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.numRate);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.txtID);
            this.groupControl1.Controls.Add(this.txtKey);
            this.groupControl1.Controls.Add(this.LabelControl3);
            this.groupControl1.Controls.Add(this.LabelControl2);
            this.groupControl1.Controls.Add(this.LabelControl1);
            this.groupControl1.Size = new System.Drawing.Size(312, 321);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 343);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 343);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 343);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 343);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(432, 343);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(736, 325);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(415, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 325);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(415, 325);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(420, 0);
            this.panelControl3.Size = new System.Drawing.Size(316, 325);
            // 
            // LabelControl1
            // 
            this.LabelControl1.Location = new System.Drawing.Point(18, 38);
            this.LabelControl1.Name = "LabelControl1";
            this.LabelControl1.Size = new System.Drawing.Size(85, 13);
            this.LabelControl1.TabIndex = 0;
            this.LabelControl1.Text = "Түлхүүр талбар :";
            // 
            // LabelControl2
            // 
            this.LabelControl2.Location = new System.Drawing.Point(18, 61);
            this.LabelControl2.Name = "LabelControl2";
            this.LabelControl2.Size = new System.Drawing.Size(57, 13);
            this.LabelControl2.TabIndex = 1;
            this.LabelControl2.Text = "ID дугаар :";
            // 
            // LabelControl3
            // 
            this.LabelControl3.Location = new System.Drawing.Point(18, 84);
            this.LabelControl3.Name = "LabelControl3";
            this.LabelControl3.Size = new System.Drawing.Size(25, 13);
            this.LabelControl3.TabIndex = 2;
            this.LabelControl3.Text = "Нэр :";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(104, 35);
            this.txtKey.Name = "txtKey";
            this.txtKey.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKey.Properties.MaxLength = 10;
            this.txtKey.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtKey.Size = new System.Drawing.Size(189, 20);
            this.txtKey.TabIndex = 3;
            this.txtKey.ToolTipTitle = "Түлхүүр талбар оруулна уу  :";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(104, 58);
            this.txtID.Name = "txtID";
            this.txtID.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtID.Properties.MaxLength = 200;
            this.txtID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtID.Size = new System.Drawing.Size(189, 20);
            this.txtID.TabIndex = 4;
            this.txtID.ToolTipTitle = "ID дугаар оруулна уу";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(104, 81);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 2000;
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtName.Size = new System.Drawing.Size(189, 20);
            this.txtName.TabIndex = 5;
            this.txtName.ToolTipTitle = "Нэр оруулна уу";
            // 
            // numRate
            // 
            this.numRate.Location = new System.Drawing.Point(104, 203);
            this.numRate.Name = "numRate";
            this.numRate.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.None;
            this.numRate.Properties.Mask.EditMask = "[0-5]{1,5}";
            this.numRate.Properties.Mask.IgnoreMaskBlank = false;
            this.numRate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numRate.Properties.Mask.ShowPlaceHolders = false;
            this.numRate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numRate.Size = new System.Drawing.Size(189, 20);
            this.numRate.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(18, 107);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(41, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Томъёо:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 202);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(49, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Хувь дүн:";
            // 
            // txtFormula
            // 
            this.txtFormula.Location = new System.Drawing.Point(104, 104);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFormula.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFormula.Size = new System.Drawing.Size(189, 96);
            this.txtFormula.TabIndex = 7;
            // 
            // FormDynamicList
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(760, 376);
            this.KeyPreview = true;
            this.Name = "FormDynamicList";
            this.Text = "Динамик жагсаалт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDynamicList_FormClosing);
            this.Load += new System.EventHandler(this.FormDynamicList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDynamicList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormula.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LabelControl1;
        private DevExpress.XtraEditors.LabelControl LabelControl3;
        private DevExpress.XtraEditors.LabelControl LabelControl2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.TextEdit txtKey;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit numRate;
        private DevExpress.XtraEditors.MemoEdit txtFormula;
    }
}