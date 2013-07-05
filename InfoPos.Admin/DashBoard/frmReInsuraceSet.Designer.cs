namespace InfoPos.Admin.DashBoard
{
    partial class frmReInsuraceSet
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.numUserNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnReadDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUserNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(268, 141);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 26);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.numRefreshTime);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.numUnReadDay);
            this.groupControl1.Controls.Add(this.numReadDay);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.numUserNo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(6, 6);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(352, 132);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Өгөгдөл";
            // 
            // numRefreshTime
            // 
            this.numRefreshTime.EditValue = "0";
            this.numRefreshTime.Location = new System.Drawing.Point(226, 107);
            this.numRefreshTime.Name = "numRefreshTime";
            this.numRefreshTime.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.numRefreshTime.Properties.Appearance.Options.UseBackColor = true;
            this.numRefreshTime.Properties.Mask.BeepOnError = true;
            this.numRefreshTime.Properties.Mask.EditMask = "f0";
            this.numRefreshTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.numRefreshTime.Properties.Mask.ShowPlaceHolders = false;
            this.numRefreshTime.Size = new System.Drawing.Size(102, 20);
            this.numRefreshTime.TabIndex = 19;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 110);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(131, 13);
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "Сэргээх хугацаа [минут] :";
            // 
            // numUnReadDay
            // 
            this.numUnReadDay.EditValue = "0";
            this.numUnReadDay.Location = new System.Drawing.Point(226, 80);
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
            this.numReadDay.Location = new System.Drawing.Point(226, 54);
            this.numReadDay.Name = "numReadDay";
            this.numReadDay.Properties.Mask.EditMask = "\\d{0,1}";
            this.numReadDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numReadDay.Properties.MaxLength = 1;
            this.numReadDay.Size = new System.Drawing.Size(102, 20);
            this.numReadDay.TabIndex = 15;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(15, 57);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(188, 13);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Уншсан мэдээллийг харуулах хоног :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(15, 83);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(199, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Уншаагүй мэдээллийг харуулах хоног :";
            // 
            // numUserNo
            // 
            this.numUserNo.EditValue = "0";
            this.numUserNo.Location = new System.Drawing.Point(165, 25);
            this.numUserNo.Name = "numUserNo";
            this.numUserNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.numUserNo.Properties.Appearance.Options.UseBackColor = true;
            this.numUserNo.Properties.Mask.EditMask = "d";
            this.numUserNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.numUserNo.Size = new System.Drawing.Size(163, 20);
            this.numUserNo.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 28);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(112, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Хариуцсан хэрэглэгч :";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // frmReInsuraceSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 169);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupControl1);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(370, 196);
            this.MinimumSize = new System.Drawing.Size(370, 196);
            this.Name = "frmReInsuraceSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Давхар даатгалын тохиргоо";
            this.Load += new System.EventHandler(this.frmReInsuraceSet_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReInsuraceSet_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUnReadDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUserNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit numUserNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit numUnReadDay;
        private DevExpress.XtraEditors.TextEdit numReadDay;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit numRefreshTime;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorProvider;
    }
}