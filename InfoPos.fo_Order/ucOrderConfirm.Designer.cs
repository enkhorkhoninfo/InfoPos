namespace InfoPos.Order
{
    partial class ucOrderConfirm
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.btnConfrim = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtOrderNo);
            this.groupControl1.Location = new System.Drawing.Point(10, 10);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(458, 74);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Мэдээлэл";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Location = new System.Drawing.Point(19, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Захиалгын №:";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Location = new System.Drawing.Point(127, 31);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtOrderNo.Properties.Appearance.Options.UseFont = true;
            this.txtOrderNo.Properties.AutoHeight = false;
            this.txtOrderNo.Properties.ReadOnly = true;
            this.txtOrderNo.Size = new System.Drawing.Size(193, 28);
            this.txtOrderNo.TabIndex = 0;
            // 
            // btnConfrim
            // 
            this.btnConfrim.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnConfrim.Appearance.Options.UseFont = true;
            this.btnConfrim.Location = new System.Drawing.Point(313, 90);
            this.btnConfrim.Name = "btnConfrim";
            this.btnConfrim.Size = new System.Drawing.Size(155, 41);
            this.btnConfrim.TabIndex = 8;
            this.btnConfrim.Text = "Баталгаажуулах";
            this.btnConfrim.Click += new System.EventHandler(this.btnConfrim_Click);
            // 
            // ucOrderConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConfrim);
            this.Controls.Add(this.groupControl1);
            this.Name = "ucOrderConfirm";
            this.Size = new System.Drawing.Size(478, 141);
            this.Load += new System.EventHandler(this.ucStatusChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtOrderNo;
        private DevExpress.XtraEditors.SimpleButton btnConfrim;
    }
}
