namespace InfoPos.Parameter
{
    partial class FormShortcut
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
            this.cboShortCtType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.numID = new DevExpress.XtraEditors.TextEdit();
            this.mmeNote = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cboIDValue = new DevExpress.XtraEditors.LookUpEdit();
            this.txtIDValue = new DevExpress.XtraEditors.MemoEdit();
            this.txtKey = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboShortCtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIDValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIDValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtKey);
            this.groupControl1.Controls.Add(this.cboIDValue);
            this.groupControl1.Controls.Add(this.txtIDValue);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.mmeNote);
            this.groupControl1.Controls.Add(this.numID);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.cboShortCtType);
            this.groupControl1.Size = new System.Drawing.Size(443, 347);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 369);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 369);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 369);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(329, 369);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(434, 369);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(824, 351);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(372, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 351);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(372, 351);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(377, 0);
            this.panelControl3.Size = new System.Drawing.Size(447, 351);
            // 
            // cboShortCtType
            // 
            this.cboShortCtType.Location = new System.Drawing.Point(169, 61);
            this.cboShortCtType.Name = "cboShortCtType";
            this.cboShortCtType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboShortCtType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboShortCtType.Size = new System.Drawing.Size(261, 20);
            this.cboShortCtType.TabIndex = 1;
            this.cboShortCtType.ToolTipTitle = "Төрөл оруулна уу .";
            this.cboShortCtType.EditValueChanged += new System.EventHandler(this.cboShortCtType_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "ID дугаар :";
            // 
            // numID
            // 
            this.numID.Location = new System.Drawing.Point(169, 33);
            this.numID.Name = "numID";
            this.numID.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.numID.Properties.Mask.BeepOnError = true;
            this.numID.Properties.Mask.EditMask = "\\d{0,4}";
            this.numID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numID.Size = new System.Drawing.Size(261, 20);
            this.numID.TabIndex = 0;
            this.numID.ToolTipTitle = "ID дугаар оруулна уу .";
            // 
            // mmeNote
            // 
            this.mmeNote.Location = new System.Drawing.Point(169, 195);
            this.mmeNote.Name = "mmeNote";
            this.mmeNote.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mmeNote.Properties.MaxLength = 500;
            this.mmeNote.Size = new System.Drawing.Size(261, 57);
            this.mmeNote.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(31, 148);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(0, 13);
            this.labelControl2.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 198);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(49, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Тайлбар :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 64);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(37, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Төрөл :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(20, 96);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(143, 13);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "Товчнууд болон хослолууд:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(20, 141);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(83, 13);
            this.labelControl7.TabIndex = 11;
            this.labelControl7.Text = "Талбарын утга :";
            // 
            // cboIDValue
            // 
            this.cboIDValue.Location = new System.Drawing.Point(169, 138);
            this.cboIDValue.Name = "cboIDValue";
            this.cboIDValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboIDValue.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboIDValue.Size = new System.Drawing.Size(261, 20);
            this.cboIDValue.TabIndex = 3;
            this.cboIDValue.ToolTipTitle = "Талбарын утга оруулна уу .";
            // 
            // txtIDValue
            // 
            this.txtIDValue.Location = new System.Drawing.Point(169, 122);
            this.txtIDValue.Name = "txtIDValue";
            this.txtIDValue.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIDValue.Properties.MaxLength = 500;
            this.txtIDValue.Size = new System.Drawing.Size(261, 54);
            this.txtIDValue.TabIndex = 3;
            this.txtIDValue.ToolTipTitle = "Талбарын утга оруулна уу .";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(169, 93);
            this.txtKey.Name = "txtKey";
            this.txtKey.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKey.Properties.MaxLength = 200;
            this.txtKey.Size = new System.Drawing.Size(261, 20);
            this.txtKey.TabIndex = 2;
            this.txtKey.ToolTip = "Гараас товчнууд болон хослолоо дарна уу .";
            this.txtKey.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.txtKey.ToolTipTitle = "Заавар";
            this.txtKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyDown_1);
            this.txtKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKey_KeyPress);
            // 
            // FormShortcut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 398);
            this.KeyPreview = true;
            this.Name = "FormShortcut";
            this.Text = "Shortcut-н бүртгэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormShortcut_FormClosing);
            this.Load += new System.EventHandler(this.FormShortcut_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormShortcut_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboShortCtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIDValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIDValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKey.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit mmeNote;
        private DevExpress.XtraEditors.TextEdit numID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cboShortCtType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LookUpEdit cboIDValue;
        private DevExpress.XtraEditors.MemoEdit txtIDValue;
        private DevExpress.XtraEditors.TextEdit txtKey;
    }
}