namespace InfoPos.Parameter
{
    partial class FormPaProductTree
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
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtItemId = new DevExpress.XtraEditors.TextEdit();
            this.cboItemType = new DevExpress.XtraEditors.LookUpEdit();
            this.cboParentId = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboItemType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboParentId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboParentId);
            this.groupControl1.Controls.Add(this.cboItemType);
            this.groupControl1.Controls.Add(this.txtItemId);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Size = new System.Drawing.Size(358, 370);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 392);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 392);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 392);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 392);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 392);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(758, 374);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(391, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 374);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(391, 374);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(396, 0);
            this.panelControl3.Size = new System.Drawing.Size(362, 374);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Эх бүлгийн код";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 102);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(73, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Бүлгийн төрөл";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(18, 76);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(101, 13);
            this.labelControl7.TabIndex = 6;
            this.labelControl7.Text = "Тухайн бүлгийн код";
            // 
            // txtItemId
            // 
            this.txtItemId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItemId.Location = new System.Drawing.Point(161, 73);
            this.txtItemId.MinimumSize = new System.Drawing.Size(171, 20);
            this.txtItemId.Name = "txtItemId";
            this.txtItemId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtItemId.Properties.MaxLength = 10;
            this.txtItemId.Size = new System.Drawing.Size(171, 20);
            this.txtItemId.TabIndex = 2;
            this.txtItemId.ToolTipTitle = "Эх багын коп оруулна уу.";
            // 
            // cboItemType
            // 
            this.cboItemType.Location = new System.Drawing.Point(161, 99);
            this.cboItemType.Name = "cboItemType";
            this.cboItemType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboItemType.Properties.NullText = "[Сонгох]";
            this.cboItemType.Size = new System.Drawing.Size(171, 20);
            this.cboItemType.TabIndex = 3;
            // 
            // cboParentId
            // 
            this.cboParentId.Location = new System.Drawing.Point(161, 47);
            this.cboParentId.Name = "cboParentId";
            this.cboParentId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboParentId.Properties.NullText = "[Сонгох]";
            this.cboParentId.Size = new System.Drawing.Size(171, 20);
            this.cboParentId.TabIndex = 1;
            // 
            // FormPaProductTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 425);
            this.Name = "FormPaProductTree";
            this.Text = "Бүтээгдэхүүний бүлэглэлийн бүртгэл";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtItemId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboItemType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboParentId.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtItemId;
        private DevExpress.XtraEditors.LookUpEdit cboParentId;
        private DevExpress.XtraEditors.LookUpEdit cboItemType;
    }
}