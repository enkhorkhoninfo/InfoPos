namespace ISM.CUser
{
    partial class frmChangePass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePass));
            this.lblNewPassAgain = new System.Windows.Forms.Label();
            this.lblNewPass = new System.Windows.Forms.Label();
            this.lblOldPass = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblMask = new DevExpress.XtraEditors.LabelControl();
            this.txtNewPassAgain = new DevExpress.XtraEditors.TextEdit();
            this.txtNewPass = new DevExpress.XtraEditors.TextEdit();
            this.txtOldPass = new DevExpress.XtraEditors.TextEdit();
            this.pictureKey = new DevExpress.XtraEditors.PictureEdit();
            this.btnSaveD = new DevExpress.XtraEditors.SimpleButton();
            this.btnExitD = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassAgain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureKey.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNewPassAgain
            // 
            this.lblNewPassAgain.AutoSize = true;
            this.lblNewPassAgain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPassAgain.Location = new System.Drawing.Point(154, 105);
            this.lblNewPassAgain.Name = "lblNewPassAgain";
            this.lblNewPassAgain.Size = new System.Drawing.Size(123, 13);
            this.lblNewPassAgain.TabIndex = 13;
            this.lblNewPassAgain.Text = "Шинэ нууц үг давтаж :";
            // 
            // lblNewPass
            // 
            this.lblNewPass.AutoSize = true;
            this.lblNewPass.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPass.Location = new System.Drawing.Point(154, 83);
            this.lblNewPass.Name = "lblNewPass";
            this.lblNewPass.Size = new System.Drawing.Size(81, 13);
            this.lblNewPass.TabIndex = 12;
            this.lblNewPass.Text = "Шинэ нууц үг :";
            // 
            // lblOldPass
            // 
            this.lblOldPass.AutoSize = true;
            this.lblOldPass.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldPass.Location = new System.Drawing.Point(154, 35);
            this.lblOldPass.Name = "lblOldPass";
            this.lblOldPass.Size = new System.Drawing.Size(90, 13);
            this.lblOldPass.TabIndex = 9;
            this.lblOldPass.Text = "Хуучин нууц үг :";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.pictureKey);
            this.groupControl1.Controls.Add(this.lblMask);
            this.groupControl1.Controls.Add(this.txtNewPassAgain);
            this.groupControl1.Controls.Add(this.txtNewPass);
            this.groupControl1.Controls.Add(this.txtOldPass);
            this.groupControl1.Controls.Add(this.lblNewPass);
            this.groupControl1.Controls.Add(this.lblOldPass);
            this.groupControl1.Controls.Add(this.lblNewPassAgain);
            this.groupControl1.Location = new System.Drawing.Point(12, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(494, 142);
            this.groupControl1.TabIndex = 14;
            // 
            // lblMask
            // 
            this.lblMask.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMask.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMask.Location = new System.Drawing.Point(157, 59);
            this.lblMask.Name = "lblMask";
            this.lblMask.Size = new System.Drawing.Size(65, 13);
            this.lblMask.TabIndex = 17;
            this.lblMask.Text = "Маскны утга";
            // 
            // txtNewPassAgain
            // 
            this.txtNewPassAgain.Location = new System.Drawing.Point(278, 102);
            this.txtNewPassAgain.Name = "txtNewPassAgain";
            this.txtNewPassAgain.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNewPassAgain.Properties.Mask.ShowPlaceHolders = false;
            this.txtNewPassAgain.Properties.UseSystemPasswordChar = true;
            this.txtNewPassAgain.Size = new System.Drawing.Size(202, 20);
            this.txtNewPassAgain.TabIndex = 16;
            // 
            // txtNewPass
            // 
            this.txtNewPass.Location = new System.Drawing.Point(278, 80);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNewPass.Properties.Mask.ShowPlaceHolders = false;
            this.txtNewPass.Properties.UseSystemPasswordChar = true;
            this.txtNewPass.Size = new System.Drawing.Size(202, 20);
            this.txtNewPass.TabIndex = 15;
            // 
            // txtOldPass
            // 
            this.txtOldPass.Location = new System.Drawing.Point(278, 32);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.Properties.Mask.ShowPlaceHolders = false;
            this.txtOldPass.Properties.UseSystemPasswordChar = true;
            this.txtOldPass.Size = new System.Drawing.Size(202, 20);
            this.txtOldPass.TabIndex = 14;
            // 
            // pictureKey
            // 
            this.pictureKey.EditValue = ((object)(resources.GetObject("pictureKey.EditValue")));
            this.pictureKey.Location = new System.Drawing.Point(25, 26);
            this.pictureKey.Name = "pictureKey";
            this.pictureKey.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureKey.Properties.Appearance.Options.UseBackColor = true;
            this.pictureKey.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureKey.Size = new System.Drawing.Size(96, 96);
            this.pictureKey.TabIndex = 18;
            // 
            // btnSaveD
            // 
            this.btnSaveD.Location = new System.Drawing.Point(324, 148);
            this.btnSaveD.Name = "btnSaveD";
            this.btnSaveD.Size = new System.Drawing.Size(88, 23);
            this.btnSaveD.TabIndex = 15;
            this.btnSaveD.Text = "Хадгалах";
            this.btnSaveD.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExitD
            // 
            this.btnExitD.Location = new System.Drawing.Point(418, 148);
            this.btnExitD.Name = "btnExitD";
            this.btnExitD.Size = new System.Drawing.Size(88, 23);
            this.btnExitD.TabIndex = 16;
            this.btnExitD.Text = "Гарах";
            this.btnExitD.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 174);
            this.Controls.Add(this.btnExitD);
            this.Controls.Add(this.btnSaveD);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Нууц үг солих";
            this.Activated += new System.EventHandler(this.frmChangePass_Activated);
            this.Load += new System.EventHandler(this.frmChangePass_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChangePass_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassAgain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureKey.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblNewPassAgain;
        internal System.Windows.Forms.Label lblNewPass;
        internal System.Windows.Forms.Label lblOldPass;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtNewPassAgain;
        private DevExpress.XtraEditors.TextEdit txtNewPass;
        private DevExpress.XtraEditors.TextEdit txtOldPass;
        private DevExpress.XtraEditors.LabelControl lblMask;
        private DevExpress.XtraEditors.PictureEdit pictureKey;
        private DevExpress.XtraEditors.SimpleButton btnSaveD;
        private DevExpress.XtraEditors.SimpleButton btnExitD;
    }
}