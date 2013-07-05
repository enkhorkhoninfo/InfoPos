namespace InfoPos.Admin.DashBoard
{
    partial class frmFinal1
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
            this.numRefreshTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.numUnReadDay = new DevExpress.XtraEditors.TextEdit();
            this.numReadDay = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtReportNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cboViewType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dteTxnDate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.ErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnReadDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboViewType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTxnDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(246, 204);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 26);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.numRefreshTime);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.numUnReadDay);
            this.groupControl1.Controls.Add(this.numReadDay);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtReportNo);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.cboViewType);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.dteTxnDate);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Location = new System.Drawing.Point(7, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(329, 194);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Өгөгдөл";
            // 
            // numRefreshTime
            // 
            this.numRefreshTime.EditValue = "0";
            this.numRefreshTime.Location = new System.Drawing.Point(220, 163);
            this.numRefreshTime.Name = "numRefreshTime";
            this.numRefreshTime.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.numRefreshTime.Properties.Appearance.Options.UseBackColor = true;
            this.numRefreshTime.Properties.Mask.BeepOnError = true;
            this.numRefreshTime.Properties.Mask.EditMask = "f0";
            this.numRefreshTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.numRefreshTime.Properties.Mask.ShowPlaceHolders = false;
            this.numRefreshTime.Size = new System.Drawing.Size(86, 20);
            this.numRefreshTime.TabIndex = 21;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(9, 166);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(131, 13);
            this.labelControl5.TabIndex = 20;
            this.labelControl5.Text = "Сэргээх хугацаа [минут] :";
            // 
            // numUnReadDay
            // 
            this.numUnReadDay.EditValue = "0";
            this.numUnReadDay.Location = new System.Drawing.Point(220, 137);
            this.numUnReadDay.Name = "numUnReadDay";
            this.numUnReadDay.Properties.Mask.EditMask = "\\d{0,2}";
            this.numUnReadDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numUnReadDay.Properties.MaxLength = 2;
            this.numUnReadDay.Size = new System.Drawing.Size(102, 20);
            this.numUnReadDay.TabIndex = 16;
            // 
            // numReadDay
            // 
            this.numReadDay.EditValue = "0";
            this.numReadDay.Location = new System.Drawing.Point(220, 111);
            this.numReadDay.Name = "numReadDay";
            this.numReadDay.Properties.Mask.EditMask = "\\d{0,1}";
            this.numReadDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numReadDay.Properties.MaxLength = 1;
            this.numReadDay.Size = new System.Drawing.Size(102, 20);
            this.numReadDay.TabIndex = 15;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 114);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(188, 13);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "Уншсан мэдээллийг харуулах хоног :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(9, 140);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(199, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Уншаагүй мэдээллийг харуулах хоног :";
            // 
            // txtReportNo
            // 
            this.txtReportNo.EditValue = "0";
            this.txtReportNo.Location = new System.Drawing.Point(159, 28);
            this.txtReportNo.Name = "txtReportNo";
            this.txtReportNo.Properties.Mask.EditMask = "\\d{0,10}";
            this.txtReportNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtReportNo.Size = new System.Drawing.Size(163, 20);
            this.txtReportNo.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 31);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(98, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Тайлангын дугаар:";
            // 
            // cboViewType
            // 
            this.cboViewType.Location = new System.Drawing.Point(159, 84);
            this.cboViewType.Name = "cboViewType";
            this.cboViewType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboViewType.Size = new System.Drawing.Size(163, 20);
            this.cboViewType.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Харах төрөл:";
            // 
            // dteTxnDate
            // 
            this.dteTxnDate.EditValue = "0";
            this.dteTxnDate.Location = new System.Drawing.Point(159, 57);
            this.dteTxnDate.Name = "dteTxnDate";
            this.dteTxnDate.Properties.Mask.EditMask = "\\d{0,10}";
            this.dteTxnDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.dteTxnDate.Size = new System.Drawing.Size(163, 20);
            this.dteTxnDate.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(132, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Тайлан гаргаж буй огноо:";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // frmFinal1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 232);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupControl1);
            this.MaximumSize = new System.Drawing.Size(352, 259);
            this.MinimumSize = new System.Drawing.Size(352, 259);
            this.Name = "frmFinal1";
            this.Text = "Final Statement 1";
            this.Load += new System.EventHandler(this.frmFinal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnReadDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboViewType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTxnDate.Properties)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit txtReportNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit numUnReadDay;
        private DevExpress.XtraEditors.TextEdit numReadDay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit numRefreshTime;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorProvider;
    }
}