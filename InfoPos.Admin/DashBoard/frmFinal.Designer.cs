namespace InfoPos.Admin.DashBoard
{
    partial class frmFinal
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
            this.components = new System.ComponentModel.Container();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.numUnReadDay = new DevExpress.XtraEditors.TextEdit();
            this.numReadDay = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtPanelName = new DevExpress.XtraEditors.TextEdit();
            this.LabelCOntrol5 = new DevExpress.XtraEditors.LabelControl();
            this.txtReportNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cboViewType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dteTxnDate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numRefreshTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnReadDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPanelName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboViewType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTxnDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(249, 266);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 26);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textEdit1);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.numUnReadDay);
            this.groupControl1.Controls.Add(this.numReadDay);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtPanelName);
            this.groupControl1.Controls.Add(this.LabelCOntrol5);
            this.groupControl1.Controls.Add(this.txtReportNo);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.cboViewType);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.dteTxnDate);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.numRefreshTime);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(7, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(332, 253);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Өгөгдөл";
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "0";
            this.textEdit1.Location = new System.Drawing.Point(230, 220);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.textEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.textEdit1.Properties.Mask.BeepOnError = true;
            this.textEdit1.Properties.Mask.EditMask = "f0";
            this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit1.Properties.Mask.ShowPlaceHolders = false;
            this.textEdit1.Size = new System.Drawing.Size(92, 20);
            this.textEdit1.TabIndex = 22;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(20, 225);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(131, 13);
            this.labelControl7.TabIndex = 21;
            this.labelControl7.Text = "Сэргээх хугацаа [минут] :";
            // 
            // numUnReadDay
            // 
            this.numUnReadDay.EditValue = "0";
            this.numUnReadDay.Location = new System.Drawing.Point(230, 194);
            this.numUnReadDay.Name = "numUnReadDay";
            this.numUnReadDay.Properties.Mask.EditMask = "\\d{0,2}";
            this.numUnReadDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numUnReadDay.Properties.MaxLength = 2;
            this.numUnReadDay.Size = new System.Drawing.Size(92, 20);
            this.numUnReadDay.TabIndex = 20;
            // 
            // numReadDay
            // 
            this.numReadDay.EditValue = "0";
            this.numReadDay.Location = new System.Drawing.Point(230, 168);
            this.numReadDay.Name = "numReadDay";
            this.numReadDay.Properties.Mask.EditMask = "\\d{0,1}";
            this.numReadDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numReadDay.Properties.MaxLength = 1;
            this.numReadDay.Size = new System.Drawing.Size(92, 20);
            this.numReadDay.TabIndex = 19;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(19, 171);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(188, 13);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "Уншсан мэдээллийг харуулах хоног :";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(19, 197);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(199, 13);
            this.labelControl8.TabIndex = 17;
            this.labelControl8.Text = "Уншаагүй мэдээллийг харуулах хоног :";
            // 
            // txtPanelName
            // 
            this.txtPanelName.EditValue = "Final Statement";
            this.txtPanelName.Location = new System.Drawing.Point(159, 27);
            this.txtPanelName.Name = "txtPanelName";
            this.txtPanelName.Properties.MaxLength = 30;
            this.txtPanelName.Size = new System.Drawing.Size(163, 20);
            this.txtPanelName.TabIndex = 0;
            // 
            // LabelCOntrol5
            // 
            this.LabelCOntrol5.Location = new System.Drawing.Point(19, 30);
            this.LabelCOntrol5.Name = "LabelCOntrol5";
            this.LabelCOntrol5.Size = new System.Drawing.Size(69, 13);
            this.LabelCOntrol5.TabIndex = 10;
            this.LabelCOntrol5.Text = "Панелын нэр:";
            // 
            // txtReportNo
            // 
            this.txtReportNo.EditValue = "0";
            this.txtReportNo.Location = new System.Drawing.Point(159, 81);
            this.txtReportNo.Name = "txtReportNo";
            this.txtReportNo.Properties.Mask.EditMask = "\\d{0,10}";
            this.txtReportNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtReportNo.Size = new System.Drawing.Size(163, 20);
            this.txtReportNo.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(19, 84);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(98, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Тайлангын дугаар:";
            // 
            // cboViewType
            // 
            this.cboViewType.Location = new System.Drawing.Point(159, 137);
            this.cboViewType.Name = "cboViewType";
            this.cboViewType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboViewType.Size = new System.Drawing.Size(163, 20);
            this.cboViewType.TabIndex = 15;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 139);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Харах төрөл:";
            // 
            // dteTxnDate
            // 
            this.dteTxnDate.EditValue = "0";
            this.dteTxnDate.Enabled = false;
            this.dteTxnDate.Location = new System.Drawing.Point(159, 110);
            this.dteTxnDate.Name = "dteTxnDate";
            this.dteTxnDate.Properties.Mask.EditMask = "\\d{0,10}";
            this.dteTxnDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.dteTxnDate.Size = new System.Drawing.Size(163, 20);
            this.dteTxnDate.TabIndex = 12;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 113);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(132, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Тайлан гаргаж буй огноо:";
            // 
            // numRefreshTime
            // 
            this.numRefreshTime.EditValue = "0";
            this.numRefreshTime.Location = new System.Drawing.Point(159, 53);
            this.numRefreshTime.Name = "numRefreshTime";
            this.numRefreshTime.Properties.Mask.EditMask = "\\d{0,10}";
            this.numRefreshTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numRefreshTime.Size = new System.Drawing.Size(163, 20);
            this.numRefreshTime.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Шинэчлэх хугацаа:";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // frmFinal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 295);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupControl1);
            this.MaximumSize = new System.Drawing.Size(352, 322);
            this.MinimumSize = new System.Drawing.Size(352, 322);
            this.Name = "frmFinal";
            this.Text = "Final Statement";
            this.Load += new System.EventHandler(this.frmFinal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnReadDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPanelName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboViewType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTxnDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LookUpEdit cboViewType;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit dteTxnDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit numRefreshTime;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtReportNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtPanelName;
        private DevExpress.XtraEditors.LabelControl LabelCOntrol5;
        private DevExpress.XtraEditors.TextEdit numUnReadDay;
        private DevExpress.XtraEditors.TextEdit numReadDay;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorProvider;
    }
}