namespace InfoPos.Parameter
{
    partial class FormStepItem
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
            this.numWorkDays = new DevExpress.XtraEditors.TextEdit();
            this.numStepItemID = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.mmeNote = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.mmeNote2 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWorkDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStepItemID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNote2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.mmeNote2);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtName2);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.numStepItemID);
            this.groupControl1.Controls.Add(this.numWorkDays);
            this.groupControl1.Controls.Add(this.mmeNote);
            this.groupControl1.Size = new System.Drawing.Size(559, 312);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 334);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 334);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 334);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 334);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 334);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(824, 316);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(256, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 316);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(256, 316);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(261, 0);
            this.panelControl3.Size = new System.Drawing.Size(563, 316);
            // 
            // numWorkDays
            // 
            this.numWorkDays.Location = new System.Drawing.Point(241, 262);
            this.numWorkDays.Name = "numWorkDays";
            this.numWorkDays.Properties.Mask.EditMask = "\\d{0,4}";
            this.numWorkDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numWorkDays.Size = new System.Drawing.Size(275, 20);
            this.numWorkDays.TabIndex = 5;
            this.numWorkDays.ToolTipTitle = "Ажлын хэдэн өдөрт хийгдэх ажил вэ? ";
            // 
            // numStepItemID
            // 
            this.numStepItemID.Location = new System.Drawing.Point(241, 38);
            this.numStepItemID.Name = "numStepItemID";
            this.numStepItemID.Properties.Mask.BeepOnError = true;
            this.numStepItemID.Properties.Mask.EditMask = "\\d{0,4}";
            this.numStepItemID.Properties.Mask.IgnoreMaskBlank = false;
            this.numStepItemID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numStepItemID.Properties.Mask.SaveLiteral = false;
            this.numStepItemID.Properties.MaxLength = 50;
            this.numStepItemID.Size = new System.Drawing.Size(275, 20);
            this.numStepItemID.TabIndex = 0;
            this.numStepItemID.ToolTipTitle = "Дамжлагын дугаарыг оруулна уу";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(241, 64);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Properties.MaxLength = 100;
            this.txtName.Size = new System.Drawing.Size(275, 20);
            this.txtName.TabIndex = 1;
            this.txtName.ToolTipTitle = "Дамжлагын нэрийг оруулна уу";
            // 
            // txtName2
            // 
            this.txtName2.Location = new System.Drawing.Point(241, 90);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName2.Properties.MaxLength = 100;
            this.txtName2.Size = new System.Drawing.Size(275, 20);
            this.txtName2.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(105, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Дамжлагын дугаар :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(86, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Дамжлагын нэр :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 93);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(118, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Дамжлагын англи нэр :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 265);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(200, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Ажлын хэдэн өдөрт хийгдэх ажил вэ? :";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(30, 119);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(111, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Дамжлагын тайлбар :";
            // 
            // mmeNote
            // 
            this.mmeNote.Location = new System.Drawing.Point(241, 116);
            this.mmeNote.Name = "mmeNote";
            this.mmeNote.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mmeNote.Properties.MaxLength = 2000;
            this.mmeNote.Size = new System.Drawing.Size(275, 67);
            this.mmeNote.TabIndex = 3;
            this.mmeNote.ToolTipTitle = "Тайлбар оруулна уу";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(30, 192);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(120, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Дамжлагын тайлбар 2 :";
            // 
            // mmeNote2
            // 
            this.mmeNote2.Location = new System.Drawing.Point(241, 189);
            this.mmeNote2.Name = "mmeNote2";
            this.mmeNote2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mmeNote2.Properties.MaxLength = 2000;
            this.mmeNote2.Size = new System.Drawing.Size(275, 67);
            this.mmeNote2.TabIndex = 4;
            // 
            // FormStepItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 367);
            this.KeyPreview = true;
            this.Name = "FormStepItem";
            this.Text = "Дамжлагын бүртгэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStepItem_FormClosing);
            this.Load += new System.EventHandler(this.FormStepItem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormStepItem_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numWorkDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStepItemID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNote2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit numStepItemID;
        private DevExpress.XtraEditors.TextEdit numWorkDays;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.MemoEdit mmeNote2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.MemoEdit mmeNote;
    }
}