namespace InfoPos.Schedule
{
    partial class frmDay
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.imgcbDayWheType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.imgcbNightWheType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dteEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.numNightTemp = new DevExpress.XtraEditors.TextEdit();
            this.numDayTemp = new DevExpress.XtraEditors.TextEdit();
            this.cboDayType = new DevExpress.XtraEditors.LookUpEdit();
            this.dteStartDay = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcbDayWheType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcbNightWheType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNightTemp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDayTemp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDayType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(232, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // imgcbDayWheType
            // 
            this.imgcbDayWheType.Location = new System.Drawing.Point(195, 102);
            this.imgcbDayWheType.Name = "imgcbDayWheType";
            this.imgcbDayWheType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.imgcbDayWheType.Properties.Appearance.Options.UseBackColor = true;
            this.imgcbDayWheType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imgcbDayWheType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.imgcbDayWheType.Size = new System.Drawing.Size(194, 20);
            this.imgcbDayWheType.TabIndex = 20;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(329, 231);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Гарах";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imgcbNightWheType
            // 
            this.imgcbNightWheType.Location = new System.Drawing.Point(195, 150);
            this.imgcbNightWheType.Name = "imgcbNightWheType";
            this.imgcbNightWheType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.imgcbNightWheType.Properties.Appearance.Options.UseBackColor = true;
            this.imgcbNightWheType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imgcbNightWheType.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.imgcbNightWheType.Size = new System.Drawing.Size(194, 20);
            this.imgcbNightWheType.TabIndex = 30;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Эхлэх огноо :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 81);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(79, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Өдрийн төрөл :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 129);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(66, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Өдрийн хэм :";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(19, 105);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(145, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Өдрийн цаг агаарын төрөл :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(19, 153);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(146, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Шөнийн цаг агаарын төрөл :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(19, 177);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(67, 13);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "Шөнийн хэм :";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dteEndDate);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.numNightTemp);
            this.groupControl1.Controls.Add(this.numDayTemp);
            this.groupControl1.Controls.Add(this.cboDayType);
            this.groupControl1.Controls.Add(this.dteStartDay);
            this.groupControl1.Controls.Add(this.imgcbNightWheType);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.imgcbDayWheType);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(405, 208);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Өгөгдөл";
            // 
            // dteEndDate
            // 
            this.dteEndDate.EditValue = null;
            this.dteEndDate.Location = new System.Drawing.Point(195, 55);
            this.dteEndDate.Name = "dteEndDate";
            this.dteEndDate.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.dteEndDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteEndDate.Size = new System.Drawing.Size(194, 20);
            this.dteEndDate.TabIndex = 10;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(19, 58);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(76, 13);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "Дуусах огноо :";
            // 
            // numNightTemp
            // 
            this.numNightTemp.Location = new System.Drawing.Point(195, 174);
            this.numNightTemp.Name = "numNightTemp";
            this.numNightTemp.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.numNightTemp.Properties.Appearance.Options.UseBackColor = true;
            this.numNightTemp.Size = new System.Drawing.Size(194, 20);
            this.numNightTemp.TabIndex = 35;
            // 
            // numDayTemp
            // 
            this.numDayTemp.Location = new System.Drawing.Point(195, 126);
            this.numDayTemp.Name = "numDayTemp";
            this.numDayTemp.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.numDayTemp.Properties.Appearance.Options.UseBackColor = true;
            this.numDayTemp.Size = new System.Drawing.Size(194, 20);
            this.numDayTemp.TabIndex = 25;
            // 
            // cboDayType
            // 
            this.cboDayType.Location = new System.Drawing.Point(195, 78);
            this.cboDayType.Name = "cboDayType";
            this.cboDayType.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cboDayType.Properties.Appearance.Options.UseBackColor = true;
            this.cboDayType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboDayType.Size = new System.Drawing.Size(194, 20);
            this.cboDayType.TabIndex = 15;
            // 
            // dteStartDay
            // 
            this.dteStartDay.EditValue = null;
            this.dteStartDay.Location = new System.Drawing.Point(195, 33);
            this.dteStartDay.Name = "dteStartDay";
            this.dteStartDay.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.dteStartDay.Properties.Appearance.Options.UseBackColor = true;
            this.dteStartDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStartDay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteStartDay.Size = new System.Drawing.Size(194, 20);
            this.dteStartDay.TabIndex = 5;
            // 
            // frmDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 262);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(437, 289);
            this.MinimumSize = new System.Drawing.Size(437, 289);
            this.Name = "frmDay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тэмдэглэгээ";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDay_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgcbDayWheType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgcbNightWheType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNightTemp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDayTemp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDayType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDay.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.ImageComboBoxEdit imgcbDayWheType;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.ImageComboBoxEdit imgcbNightWheType;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit numNightTemp;
        private DevExpress.XtraEditors.TextEdit numDayTemp;
        private DevExpress.XtraEditors.LookUpEdit cboDayType;
        private DevExpress.XtraEditors.DateEdit dteStartDay;
        private DevExpress.XtraEditors.DateEdit dteEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}