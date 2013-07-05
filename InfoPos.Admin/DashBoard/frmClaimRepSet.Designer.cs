namespace InfoPos.Admin.DashBoard
{
    partial class frmClaimRepSet
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.numRefreshTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.numDays = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(173, 102);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 26);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.numRefreshTime);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.numDays);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(8, 8);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(255, 91);
            this.groupControl1.TabIndex = 11;
            this.groupControl1.Text = "Өгөгдөл";
            // 
            // numRefreshTime
            // 
            this.numRefreshTime.EditValue = "0";
            this.numRefreshTime.Location = new System.Drawing.Point(159, 57);
            this.numRefreshTime.Name = "numRefreshTime";
            this.numRefreshTime.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.numRefreshTime.Properties.Appearance.Options.UseBackColor = true;
            this.numRefreshTime.Properties.Mask.BeepOnError = true;
            this.numRefreshTime.Properties.Mask.EditMask = "f0";
            this.numRefreshTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.numRefreshTime.Properties.Mask.ShowPlaceHolders = false;
            this.numRefreshTime.Size = new System.Drawing.Size(86, 20);
            this.numRefreshTime.TabIndex = 19;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(9, 60);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(131, 13);
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "Сэргээх хугацаа [минут] :";
            // 
            // numDays
            // 
            this.numDays.EditValue = "";
            this.numDays.Location = new System.Drawing.Point(159, 30);
            this.numDays.Name = "numDays";
            this.numDays.Properties.Mask.EditMask = "\\d{0,10}";
            this.numDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numDays.Size = new System.Drawing.Size(86, 20);
            this.numDays.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Хоног :";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // frmClaimRepSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 130);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupControl1);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(277, 157);
            this.MinimumSize = new System.Drawing.Size(277, 157);
            this.Name = "frmClaimRepSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Нөхөн төлбөрийн мэдээлэлийн тохиргоо";
            this.Load += new System.EventHandler(this.frmClaimRepSet_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmClaimRepSet_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit numDays;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit numRefreshTime;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider ErrorProvider;
    }
}