namespace InfoPos.Issue
{
    partial class FormAssign
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
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlData = new DevExpress.XtraEditors.GroupControl();
            this.lblComment = new DevExpress.XtraEditors.LabelControl();
            this.lblTxnType = new DevExpress.XtraEditors.LabelControl();
            this.cboUser = new DevExpress.XtraEditors.LookUpEdit();
            this.mmeComment = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlData)).BeginInit();
            this.groupControlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeComment.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(243, 111);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(102, 23);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.Text = "Хадгалах";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(351, 111);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Болих";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupControlData
            // 
            this.groupControlData.Controls.Add(this.lblComment);
            this.groupControlData.Controls.Add(this.lblTxnType);
            this.groupControlData.Controls.Add(this.cboUser);
            this.groupControlData.Controls.Add(this.mmeComment);
            this.groupControlData.Location = new System.Drawing.Point(12, 12);
            this.groupControlData.Name = "groupControlData";
            this.groupControlData.Size = new System.Drawing.Size(441, 94);
            this.groupControlData.TabIndex = 3;
            this.groupControlData.Text = "Өгөгдөл";
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(15, 49);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(108, 13);
            this.lblComment.TabIndex = 3;
            this.lblComment.Text = "Гүйлгээний тайлбар :";
            // 
            // lblTxnType
            // 
            this.lblTxnType.Location = new System.Drawing.Point(15, 28);
            this.lblTxnType.Name = "lblTxnType";
            this.lblTxnType.Size = new System.Drawing.Size(116, 13);
            this.lblTxnType.TabIndex = 1;
            this.lblTxnType.Text = "Шилжүүлэх хэрэглэгч :";
            // 
            // cboUser
            // 
            this.cboUser.Location = new System.Drawing.Point(188, 25);
            this.cboUser.Name = "cboUser";
            this.cboUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUser.Properties.NullText = "";
            this.cboUser.Size = new System.Drawing.Size(235, 20);
            this.cboUser.TabIndex = 0;
            this.cboUser.ToolTipTitle = "Гүйлгээний төрөл оруулна уу";
            this.cboUser.EditValueChanged += new System.EventHandler(this.cboUser_EditValueChanged);
            // 
            // mmeComment
            // 
            this.mmeComment.Location = new System.Drawing.Point(188, 46);
            this.mmeComment.Name = "mmeComment";
            this.mmeComment.Size = new System.Drawing.Size(235, 41);
            this.mmeComment.TabIndex = 1;
            this.mmeComment.ToolTipTitle = "Гүйлгээний тайлбар оруулна уу";
            // 
            // FormAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 138);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupControlData);
            this.MaximumSize = new System.Drawing.Size(481, 176);
            this.MinimumSize = new System.Drawing.Size(473, 165);
            this.Name = "FormAssign";
            this.Text = "Шилжүүлэх";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAssign_FormClosing);
            this.Load += new System.EventHandler(this.FormAssign_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlData)).EndInit();
            this.groupControlData.ResumeLayout(false);
            this.groupControlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeComment.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.GroupControl groupControlData;
        private DevExpress.XtraEditors.LabelControl lblComment;
        private DevExpress.XtraEditors.LabelControl lblTxnType;
        private DevExpress.XtraEditors.LookUpEdit cboUser;
        private DevExpress.XtraEditors.MemoEdit mmeComment;
    }
}