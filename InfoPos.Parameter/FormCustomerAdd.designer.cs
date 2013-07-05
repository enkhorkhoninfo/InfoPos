namespace InfoPos.Parameter
{
    partial class FormCustomerAdd
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
            this.ucDynamicParameter1 = new ISM.Template.UserControls.ucDynamicParameter();
            this.cboCustomerType = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomerType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ucDynamicParameter1
            // 
            this.ucDynamicParameter1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucDynamicParameter1.Location = new System.Drawing.Point(1, 27);
            this.ucDynamicParameter1.Name = "ucDynamicParameter1";
            this.ucDynamicParameter1.Size = new System.Drawing.Size(794, 486);
            this.ucDynamicParameter1.TabIndex = 213;
            this.ucDynamicParameter1.TableTypeId = ((ulong)(0ul));
            // 
            // cboCustomerType
            // 
            this.cboCustomerType.Location = new System.Drawing.Point(153, 3);
            this.cboCustomerType.Name = "cboCustomerType";
            this.cboCustomerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCustomerType.Properties.DisplayFormat.FormatString = "d";
            this.cboCustomerType.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cboCustomerType.Properties.EditFormat.FormatString = "d";
            this.cboCustomerType.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cboCustomerType.Properties.NullText = "";
            this.cboCustomerType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cboCustomerType.Size = new System.Drawing.Size(272, 20);
            this.cboCustomerType.TabIndex = 361;
            this.cboCustomerType.EditValueChanged += new System.EventHandler(this.cboCustomerType_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(6, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(141, 13);
            this.labelControl4.TabIndex = 362;
            this.labelControl4.Text = "Харилцагчийн төрөл сонгох";
            // 
            // FormCustomerAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 514);
            this.Controls.Add(this.cboCustomerType);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.ucDynamicParameter1);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(798, 521);
            this.Name = "FormCustomerAdd";
            this.Text = "Харилцагчийн нэмэлт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCustomerAdd_FormClosing);
            this.Load += new System.EventHandler(this.FormContractAdd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCustomerAdd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.cboCustomerType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ISM.Template.UserControls.ucDynamicParameter ucDynamicParameter1;
        private DevExpress.XtraEditors.LookUpEdit cboCustomerType;
        private DevExpress.XtraEditors.LabelControl labelControl4;

    }
}