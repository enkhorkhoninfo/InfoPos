namespace ISM.Template
{
    partial class ucXDataPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.vGridControl1 = new DevExpress.XtraVerticalGrid.VGridControl();
            this.riTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.riCalcEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.riDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.riButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.riButtonEditReadOnly = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.btnInput = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCalcEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateEdit.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riButtonEditReadOnly)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(332, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 26);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Хадгал";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Caramel";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(332, 35);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 26);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Болих";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // vGridControl1
            // 
            this.vGridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl1.Location = new System.Drawing.Point(3, 3);
            this.vGridControl1.Name = "vGridControl1";
            this.vGridControl1.OptionsView.AutoScaleBands = true;
            this.vGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riTextEdit,
            this.riCalcEdit,
            this.riDateEdit,
            this.riButtonEdit,
            this.riButtonEditReadOnly});
            this.vGridControl1.ShowButtonMode = DevExpress.XtraVerticalGrid.ShowButtonModeEnum.ShowAlways;
            this.vGridControl1.Size = new System.Drawing.Size(323, 281);
            this.vGridControl1.TabIndex = 6;
            // 
            // riTextEdit
            // 
            this.riTextEdit.AutoHeight = false;
            this.riTextEdit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.riTextEdit.Name = "riTextEdit";
            // 
            // riCalcEdit
            // 
            this.riCalcEdit.AutoHeight = false;
            this.riCalcEdit.Name = "riCalcEdit";
            // 
            // riDateEdit
            // 
            this.riDateEdit.AutoHeight = false;
            this.riDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riDateEdit.DisplayFormat.FormatString = "yyyy.MM.dd";
            this.riDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.riDateEdit.EditFormat.FormatString = "yyyy.MM.dd";
            this.riDateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.riDateEdit.HideSelection = false;
            this.riDateEdit.Name = "riDateEdit";
            this.riDateEdit.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // riButtonEdit
            // 
            this.riButtonEdit.AutoHeight = false;
            this.riButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riButtonEdit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.riButtonEdit.Name = "riButtonEdit";
            // 
            // riButtonEditReadOnly
            // 
            this.riButtonEditReadOnly.AutoHeight = false;
            this.riButtonEditReadOnly.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.riButtonEditReadOnly.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.riButtonEditReadOnly.Name = "riButtonEditReadOnly";
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(332, 93);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(78, 26);
            this.btnView.TabIndex = 9;
            this.btnView.Text = "Харах";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnInput
            // 
            this.btnInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInput.Location = new System.Drawing.Point(332, 125);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(78, 26);
            this.btnInput.TabIndex = 10;
            this.btnInput.Text = "Оруулах";
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // ucXDataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.vGridControl1);
            this.Name = "ucXDataPanel";
            this.Size = new System.Drawing.Size(413, 287);
            this.Load += new System.EventHandler(this.ucXDataPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCalcEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateEdit.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riButtonEditReadOnly)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnSave;
        public DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        public DevExpress.XtraEditors.SimpleButton btnCancel;
        public DevExpress.XtraVerticalGrid.VGridControl vGridControl1;
        public DevExpress.XtraEditors.SimpleButton btnView;
        public DevExpress.XtraEditors.SimpleButton btnInput;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit riTextEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit riCalcEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riDateEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riButtonEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit riButtonEditReadOnly;
    }
}
