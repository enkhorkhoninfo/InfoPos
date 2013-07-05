namespace InfoPos.Parameter
{
    partial class FormCustRate
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
            this.numRateCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.mmoNote = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.numMinScore = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.numMaxScore = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRateCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmoNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinScore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxScore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.numMaxScore);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.numMinScore);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.numRateCode);
            this.groupControl1.Controls.Add(this.mmoNote);
            this.groupControl1.Size = new System.Drawing.Size(613, 440);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 462);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 462);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 462);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 462);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 462);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(878, 444);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(256, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 444);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(256, 444);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(261, 0);
            this.panelControl3.Size = new System.Drawing.Size(617, 444);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(164, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Харилцагчийн зэрэглэлийн код :";
            // 
            // numRateCode
            // 
            this.numRateCode.Location = new System.Drawing.Point(260, 38);
            this.numRateCode.Name = "numRateCode";
            this.numRateCode.Properties.Mask.BeepOnError = true;
            this.numRateCode.Properties.Mask.EditMask = "\\d{0,4}";
            this.numRateCode.Properties.Mask.IgnoreMaskBlank = false;
            this.numRateCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numRateCode.Properties.Mask.SaveLiteral = false;
            this.numRateCode.Size = new System.Drawing.Size(268, 20);
            this.numRateCode.TabIndex = 0;
            this.numRateCode.ToolTipTitle = "Зэрэглэлийн код оруулна уу";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(89, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Зэрэглэлийн нэр :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(260, 64);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.Size = new System.Drawing.Size(268, 20);
            this.txtName.TabIndex = 1;
            this.txtName.ToolTipTitle = "Зэрэглэлийн нэр оруулна уу";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 93);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(98, 13);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "Зэрэглэлийн нэр 2 :";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(260, 90);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 50;
            this.txtName2.Size = new System.Drawing.Size(268, 20);
            this.txtName2.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(30, 197);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(133, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Зэрэглэлийн тодорхойлт :";
            // 
            // mmoNote
            // 
            this.mmoNote.Location = new System.Drawing.Point(30, 216);
            this.mmoNote.Name = "mmoNote";
            this.mmoNote.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mmoNote.Properties.MaxLength = 1000;
            this.mmoNote.Size = new System.Drawing.Size(498, 186);
            this.mmoNote.TabIndex = 6;
            this.mmoNote.ToolTipTitle = "Зэрэглэлийн тодорхойлт оруулна уу";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 119);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(209, 13);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "Тухайн зэрэглэлд хамрагдах доод оноо :";
            // 
            // numMinScore
            // 
            this.numMinScore.Location = new System.Drawing.Point(260, 116);
            this.numMinScore.Name = "numMinScore";
            this.numMinScore.Properties.Mask.BeepOnError = true;
            this.numMinScore.Properties.Mask.EditMask = "\\d{0,10}";
            this.numMinScore.Properties.Mask.IgnoreMaskBlank = false;
            this.numMinScore.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numMinScore.Properties.Mask.SaveLiteral = false;
            this.numMinScore.Size = new System.Drawing.Size(268, 20);
            this.numMinScore.TabIndex = 3;
            this.numMinScore.ToolTipTitle = "Хамрагдах доод оноо оруулна уу";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(30, 145);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(207, 13);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "Тухайн зэрэглэлд хамрагдах дээд оноо :";
            // 
            // numMaxScore
            // 
            this.numMaxScore.Location = new System.Drawing.Point(260, 142);
            this.numMaxScore.Name = "numMaxScore";
            this.numMaxScore.Properties.Mask.BeepOnError = true;
            this.numMaxScore.Properties.Mask.EditMask = "\\d{0,10}";
            this.numMaxScore.Properties.Mask.IgnoreMaskBlank = false;
            this.numMaxScore.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numMaxScore.Properties.Mask.SaveLiteral = false;
            this.numMaxScore.Size = new System.Drawing.Size(268, 20);
            this.numMaxScore.TabIndex = 4;
            this.numMaxScore.ToolTipTitle = "Хамрагдах дээд оноо оруулна уу";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(30, 171);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(107, 13);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "Жагсаалтын эрэмбэ :";
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(260, 168);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.BeepOnError = true;
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.IgnoreMaskBlank = false;
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.Properties.Mask.SaveLiteral = false;
            this.numOrderNo.Size = new System.Drawing.Size(268, 20);
            this.numOrderNo.TabIndex = 5;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбэ оруулна уу";
            // 
            // FormCustRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 495);
            this.KeyPreview = true;
            this.Name = "FormCustRate";
            this.Text = "Харилцагчийн зэрэглэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCustRate_FormClosing);
            this.Load += new System.EventHandler(this.FormCustRate_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCustRate_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRateCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmoNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinScore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxScore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit numRateCode;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit numMaxScore;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit numMinScore;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.MemoEdit mmoNote;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit numOrderNo;

    }
}