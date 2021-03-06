﻿namespace InfoPos.Parameter
{
    partial class FaPosition
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.numTypeCode = new DevExpress.XtraEditors.TextEdit();
            this.txtPosition = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numOrderNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTypeCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.numOrderNo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtPosition);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.numTypeCode);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Size = new System.Drawing.Size(389, 324);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(657, 328);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(259, 0);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(259, 328);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(264, 0);
            this.panelControl3.Size = new System.Drawing.Size(393, 328);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(102, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Байрлалын дугаар :";
            // 
            // numTypeCode
            // 
            this.numTypeCode.Location = new System.Drawing.Point(156, 47);
            this.numTypeCode.Name = "numTypeCode";
            this.numTypeCode.Properties.Mask.EditMask = "\\d{0,4}";
            this.numTypeCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numTypeCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numTypeCode.Size = new System.Drawing.Size(207, 20);
            this.numTypeCode.TabIndex = 0;
            this.numTypeCode.ToolTipTitle = "Байрлалын дугаараа оруулна уу";
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(156, 73);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPosition.Properties.Mask.EditMask = "99999999";
            this.txtPosition.Properties.MaxLength = 50;
            this.txtPosition.Size = new System.Drawing.Size(207, 20);
            this.txtPosition.TabIndex = 1;
            this.txtPosition.ToolTipTitle = "Байршлаа оруулна уу";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 76);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Байршил :";
            // 
            // numOrderNo
            // 
            this.numOrderNo.Location = new System.Drawing.Point(156, 99);
            this.numOrderNo.Name = "numOrderNo";
            this.numOrderNo.Properties.Mask.EditMask = "\\d{0,4}";
            this.numOrderNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numOrderNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.numOrderNo.Size = new System.Drawing.Size(207, 20);
            this.numOrderNo.TabIndex = 2;
            this.numOrderNo.ToolTipTitle = "Жагсаалтын эрэмбээ оруулна уу";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 102);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(107, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Жагсаалтын эрэмбэ :";
            // 
            // FaPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 380);
            this.KeyPreview = true;
            this.Name = "FaPosition";
            this.Text = "Үндсэн хөрөнгө, бараа материалын байршлын бүртгэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaPosition_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FaPosition_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTypeCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPosition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit numOrderNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtPosition;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit numTypeCode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}