namespace ISM.CUser
{
    partial class frmLoginSetup
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtTimeOut = new DevExpress.XtraEditors.TextEdit();
            this.numPort = new DevExpress.XtraEditors.TextEdit();
            this.btnConnectD = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelD = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Хүсэлт хүлээх хугацаа:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtServer
            // 
            this.txtServer.BackColor = System.Drawing.SystemColors.Info;
            this.txtServer.Location = new System.Drawing.Point(92, 16);
            this.txtServer.MaxLength = 30;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(152, 21);
            this.txtServer.TabIndex = 0;
            // 
            // lblServer
            // 
            this.lblServer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(6, 15);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(72, 20);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Сервер:";
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPort
            // 
            this.lblPort.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(6, 43);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(72, 17);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Порт:";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.EditValue = ((short)(9999));
            this.txtTimeOut.EnterMoveNextControl = true;
            this.txtTimeOut.Location = new System.Drawing.Point(181, 68);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Properties.DisplayFormat.FormatString = "9999";
            this.txtTimeOut.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTimeOut.Properties.EditFormat.FormatString = "9999";
            this.txtTimeOut.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTimeOut.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTimeOut.Properties.MaxLength = 4;
            this.txtTimeOut.Properties.NullText = "1";
            this.txtTimeOut.Properties.NullValuePrompt = "1";
            this.txtTimeOut.Size = new System.Drawing.Size(63, 20);
            this.txtTimeOut.TabIndex = 2;
            // 
            // numPort
            // 
            this.numPort.EditValue = ((short)(9999));
            this.numPort.EnterMoveNextControl = true;
            this.numPort.Location = new System.Drawing.Point(181, 42);
            this.numPort.Name = "numPort";
            this.numPort.Properties.DisplayFormat.FormatString = "9999";
            this.numPort.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numPort.Properties.EditFormat.FormatString = "9999";
            this.numPort.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numPort.Properties.MaxLength = 4;
            this.numPort.Properties.NullText = "1";
            this.numPort.Properties.NullValuePrompt = "1";
            this.numPort.Size = new System.Drawing.Size(63, 20);
            this.numPort.TabIndex = 1;
            // 
            // btnConnectD
            // 
            this.btnConnectD.Location = new System.Drawing.Point(83, 114);
            this.btnConnectD.Name = "btnConnectD";
            this.btnConnectD.Size = new System.Drawing.Size(91, 23);
            this.btnConnectD.TabIndex = 7;
            this.btnConnectD.Text = "Хадгалах";
            this.btnConnectD.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancelD
            // 
            this.btnCancelD.Location = new System.Drawing.Point(180, 114);
            this.btnCancelD.Name = "btnCancelD";
            this.btnCancelD.Size = new System.Drawing.Size(92, 23);
            this.btnCancelD.TabIndex = 8;
            this.btnCancelD.Text = "Гарах";
            this.btnCancelD.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtTimeOut);
            this.panelControl1.Controls.Add(this.txtServer);
            this.panelControl1.Controls.Add(this.numPort);
            this.panelControl1.Controls.Add(this.lblServer);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.lblPort);
            this.panelControl1.Location = new System.Drawing.Point(12, 9);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(260, 100);
            this.panelControl1.TabIndex = 9;
            // 
            // frmLoginSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 141);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnCancelD);
            this.Controls.Add(this.btnConnectD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoginSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Холболтын тохиргоо";
            this.Activated += new System.EventHandler(this.frmLoginSetup_Activated);
            this.Load += new System.EventHandler(this.frmLoginSetup_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLoginSetup_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblServer;
        internal System.Windows.Forms.Label lblPort;
        internal System.Windows.Forms.TextBox txtServer;
        internal System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit numPort;
        private DevExpress.XtraEditors.TextEdit txtTimeOut;
        private DevExpress.XtraEditors.SimpleButton btnConnectD;
        private DevExpress.XtraEditors.SimpleButton btnCancelD;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}