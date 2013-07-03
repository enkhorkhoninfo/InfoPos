namespace ISM.Touch
{
    partial class frmDatepad
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
            this.numYear = new DevExpress.XtraEditors.CalcEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnKeyExit = new DevExpress.XtraEditors.LabelControl();
            this.btnKeyComma = new DevExpress.XtraEditors.LabelControl();
            this.btnKey0 = new DevExpress.XtraEditors.LabelControl();
            this.btnKeyDot = new DevExpress.XtraEditors.LabelControl();
            this.btnKeyDone = new DevExpress.XtraEditors.LabelControl();
            this.btnKey9 = new DevExpress.XtraEditors.LabelControl();
            this.btnKey8 = new DevExpress.XtraEditors.LabelControl();
            this.btnKey7 = new DevExpress.XtraEditors.LabelControl();
            this.btnKeyClear = new DevExpress.XtraEditors.LabelControl();
            this.btnKey6 = new DevExpress.XtraEditors.LabelControl();
            this.btnKey5 = new DevExpress.XtraEditors.LabelControl();
            this.btnKey4 = new DevExpress.XtraEditors.LabelControl();
            this.btnKeyBack = new DevExpress.XtraEditors.LabelControl();
            this.btnKey3 = new DevExpress.XtraEditors.LabelControl();
            this.btnKey2 = new DevExpress.XtraEditors.LabelControl();
            this.btnKey1 = new DevExpress.XtraEditors.LabelControl();
            this.numDay = new DevExpress.XtraEditors.CalcEdit();
            this.numMonth = new DevExpress.XtraEditors.CalcEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numYear.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // numYear
            // 
            this.numYear.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numYear.EnterMoveNextControl = true;
            this.numYear.Location = new System.Drawing.Point(40, 10);
            this.numYear.Name = "numYear";
            this.numYear.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.numYear.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numYear.Properties.Appearance.Options.UseFont = true;
            this.numYear.Properties.AutoHeight = false;
            this.numYear.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.numYear.Properties.Mask.SaveLiteral = false;
            this.numYear.Properties.Mask.ShowPlaceHolders = false;
            this.numYear.Properties.MaxLength = 4;
            this.numYear.Size = new System.Drawing.Size(69, 38);
            this.numYear.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnKeyExit);
            this.panel1.Controls.Add(this.btnKeyComma);
            this.panel1.Controls.Add(this.btnKey0);
            this.panel1.Controls.Add(this.btnKeyDot);
            this.panel1.Controls.Add(this.btnKeyDone);
            this.panel1.Controls.Add(this.btnKey9);
            this.panel1.Controls.Add(this.btnKey8);
            this.panel1.Controls.Add(this.btnKey7);
            this.panel1.Controls.Add(this.btnKeyClear);
            this.panel1.Controls.Add(this.btnKey6);
            this.panel1.Controls.Add(this.btnKey5);
            this.panel1.Controls.Add(this.btnKey4);
            this.panel1.Controls.Add(this.btnKeyBack);
            this.panel1.Controls.Add(this.btnKey3);
            this.panel1.Controls.Add(this.btnKey2);
            this.panel1.Controls.Add(this.btnKey1);
            this.panel1.Location = new System.Drawing.Point(7, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(337, 286);
            this.panel1.TabIndex = 44;
            // 
            // btnKeyExit
            // 
            this.btnKeyExit.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKeyExit.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeyExit.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKeyExit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKeyExit.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKeyExit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKeyExit.Location = new System.Drawing.Point(216, 216);
            this.btnKeyExit.Name = "btnKeyExit";
            this.btnKeyExit.Size = new System.Drawing.Size(115, 64);
            this.btnKeyExit.TabIndex = 63;
            this.btnKeyExit.Tag = "Exit";
            this.btnKeyExit.Text = "Гарах";
            // 
            // btnKeyComma
            // 
            this.btnKeyComma.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKeyComma.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeyComma.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKeyComma.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKeyComma.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKeyComma.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKeyComma.Location = new System.Drawing.Point(146, 216);
            this.btnKeyComma.Name = "btnKeyComma";
            this.btnKeyComma.Size = new System.Drawing.Size(64, 64);
            this.btnKeyComma.TabIndex = 62;
            this.btnKeyComma.Text = ",";
            // 
            // btnKey0
            // 
            this.btnKey0.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey0.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey0.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey0.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey0.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey0.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey0.Location = new System.Drawing.Point(76, 216);
            this.btnKey0.Name = "btnKey0";
            this.btnKey0.Size = new System.Drawing.Size(64, 64);
            this.btnKey0.TabIndex = 61;
            this.btnKey0.Text = "0";
            // 
            // btnKeyDot
            // 
            this.btnKeyDot.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKeyDot.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeyDot.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKeyDot.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKeyDot.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKeyDot.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKeyDot.Location = new System.Drawing.Point(6, 216);
            this.btnKeyDot.Name = "btnKeyDot";
            this.btnKeyDot.Size = new System.Drawing.Size(64, 64);
            this.btnKeyDot.TabIndex = 60;
            this.btnKeyDot.Text = ".";
            // 
            // btnKeyDone
            // 
            this.btnKeyDone.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKeyDone.Appearance.Font = new System.Drawing.Font("Wingdings", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnKeyDone.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKeyDone.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKeyDone.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKeyDone.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKeyDone.Location = new System.Drawing.Point(216, 146);
            this.btnKeyDone.Name = "btnKeyDone";
            this.btnKeyDone.Size = new System.Drawing.Size(115, 64);
            this.btnKeyDone.TabIndex = 59;
            this.btnKeyDone.Tag = "Done";
            this.btnKeyDone.Text = "Ã";
            // 
            // btnKey9
            // 
            this.btnKey9.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey9.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey9.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey9.Location = new System.Drawing.Point(146, 146);
            this.btnKey9.Name = "btnKey9";
            this.btnKey9.Size = new System.Drawing.Size(64, 64);
            this.btnKey9.TabIndex = 58;
            this.btnKey9.Text = "9";
            // 
            // btnKey8
            // 
            this.btnKey8.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey8.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey8.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey8.Location = new System.Drawing.Point(76, 146);
            this.btnKey8.Name = "btnKey8";
            this.btnKey8.Size = new System.Drawing.Size(64, 64);
            this.btnKey8.TabIndex = 57;
            this.btnKey8.Text = "8";
            // 
            // btnKey7
            // 
            this.btnKey7.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey7.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey7.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey7.Location = new System.Drawing.Point(6, 146);
            this.btnKey7.Name = "btnKey7";
            this.btnKey7.Size = new System.Drawing.Size(64, 64);
            this.btnKey7.TabIndex = 56;
            this.btnKey7.Text = "7";
            // 
            // btnKeyClear
            // 
            this.btnKeyClear.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKeyClear.Appearance.Font = new System.Drawing.Font("Wingdings", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnKeyClear.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKeyClear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKeyClear.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKeyClear.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKeyClear.Location = new System.Drawing.Point(216, 76);
            this.btnKeyClear.Name = "btnKeyClear";
            this.btnKeyClear.Size = new System.Drawing.Size(115, 64);
            this.btnKeyClear.TabIndex = 55;
            this.btnKeyClear.Tag = "Clear";
            this.btnKeyClear.Text = "x";
            // 
            // btnKey6
            // 
            this.btnKey6.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey6.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey6.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey6.Location = new System.Drawing.Point(146, 76);
            this.btnKey6.Name = "btnKey6";
            this.btnKey6.Size = new System.Drawing.Size(64, 64);
            this.btnKey6.TabIndex = 54;
            this.btnKey6.Text = "6";
            // 
            // btnKey5
            // 
            this.btnKey5.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey5.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey5.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey5.Location = new System.Drawing.Point(76, 76);
            this.btnKey5.Name = "btnKey5";
            this.btnKey5.Size = new System.Drawing.Size(64, 64);
            this.btnKey5.TabIndex = 53;
            this.btnKey5.Text = "5";
            // 
            // btnKey4
            // 
            this.btnKey4.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey4.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey4.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey4.Location = new System.Drawing.Point(6, 76);
            this.btnKey4.Name = "btnKey4";
            this.btnKey4.Size = new System.Drawing.Size(64, 64);
            this.btnKey4.TabIndex = 52;
            this.btnKey4.Text = "4";
            // 
            // btnKeyBack
            // 
            this.btnKeyBack.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKeyBack.Appearance.Font = new System.Drawing.Font("Wingdings", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnKeyBack.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKeyBack.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKeyBack.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKeyBack.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKeyBack.Location = new System.Drawing.Point(216, 6);
            this.btnKeyBack.Name = "btnKeyBack";
            this.btnKeyBack.Size = new System.Drawing.Size(115, 64);
            this.btnKeyBack.TabIndex = 51;
            this.btnKeyBack.Tag = "Back";
            this.btnKeyBack.Text = "Õ";
            // 
            // btnKey3
            // 
            this.btnKey3.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey3.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey3.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey3.Location = new System.Drawing.Point(146, 6);
            this.btnKey3.Name = "btnKey3";
            this.btnKey3.Size = new System.Drawing.Size(64, 64);
            this.btnKey3.TabIndex = 50;
            this.btnKey3.Text = "3";
            // 
            // btnKey2
            // 
            this.btnKey2.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey2.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey2.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey2.Location = new System.Drawing.Point(76, 6);
            this.btnKey2.Name = "btnKey2";
            this.btnKey2.Size = new System.Drawing.Size(64, 64);
            this.btnKey2.TabIndex = 49;
            this.btnKey2.Text = "2";
            // 
            // btnKey1
            // 
            this.btnKey1.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKey1.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKey1.Appearance.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnKey1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnKey1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.btnKey1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnKey1.Location = new System.Drawing.Point(6, 6);
            this.btnKey1.Name = "btnKey1";
            this.btnKey1.Size = new System.Drawing.Size(64, 64);
            this.btnKey1.TabIndex = 48;
            this.btnKey1.Text = "1";
            // 
            // numDay
            // 
            this.numDay.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDay.Location = new System.Drawing.Point(241, 10);
            this.numDay.Name = "numDay";
            this.numDay.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.numDay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDay.Properties.Appearance.Options.UseFont = true;
            this.numDay.Properties.AutoHeight = false;
            this.numDay.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.numDay.Properties.Mask.SaveLiteral = false;
            this.numDay.Properties.Mask.ShowPlaceHolders = false;
            this.numDay.Properties.MaxLength = 2;
            this.numDay.Size = new System.Drawing.Size(69, 38);
            this.numDay.TabIndex = 2;
            // 
            // numMonth
            // 
            this.numMonth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMonth.Location = new System.Drawing.Point(141, 10);
            this.numMonth.Name = "numMonth";
            this.numMonth.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.numMonth.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMonth.Properties.Appearance.Options.UseFont = true;
            this.numMonth.Properties.AutoHeight = false;
            this.numMonth.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.numMonth.Properties.Mask.SaveLiteral = false;
            this.numMonth.Properties.Mask.ShowPlaceHolders = false;
            this.numMonth.Properties.MaxLength = 2;
            this.numMonth.Size = new System.Drawing.Size(69, 38);
            this.numMonth.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(113, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 33);
            this.label1.TabIndex = 45;
            this.label1.Text = "/";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(213, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 33);
            this.label2.TabIndex = 46;
            this.label2.Text = "/";
            // 
            // frmDatepad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(351, 344);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numMonth);
            this.Controls.Add(this.numDay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numYear);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmDatepad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Тоогоо оруулна уу";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDatepad_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numYear.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CalcEdit numYear;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.CalcEdit numDay;
        private DevExpress.XtraEditors.CalcEdit numMonth;
        private DevExpress.XtraEditors.LabelControl btnKey1;
        private DevExpress.XtraEditors.LabelControl btnKeyExit;
        private DevExpress.XtraEditors.LabelControl btnKeyComma;
        private DevExpress.XtraEditors.LabelControl btnKey0;
        private DevExpress.XtraEditors.LabelControl btnKeyDot;
        private DevExpress.XtraEditors.LabelControl btnKeyDone;
        private DevExpress.XtraEditors.LabelControl btnKey9;
        private DevExpress.XtraEditors.LabelControl btnKey8;
        private DevExpress.XtraEditors.LabelControl btnKey7;
        private DevExpress.XtraEditors.LabelControl btnKeyClear;
        private DevExpress.XtraEditors.LabelControl btnKey6;
        private DevExpress.XtraEditors.LabelControl btnKey5;
        private DevExpress.XtraEditors.LabelControl btnKey4;
        private DevExpress.XtraEditors.LabelControl btnKeyBack;
        private DevExpress.XtraEditors.LabelControl btnKey3;
        private DevExpress.XtraEditors.LabelControl btnKey2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}