namespace InfoPos.Parameter
{
    partial class FormGenParam
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
            this.panelBtn = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.ucGenParam = new ISM.Template.ucParameterPanel();
            ((System.ComponentModel.ISupportInitialize)(this.panelBtn)).BeginInit();
            this.panelBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.btnExit);
            this.panelBtn.Controls.Add(this.btnEdit);
            this.panelBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBtn.Location = new System.Drawing.Point(0, 499);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Size = new System.Drawing.Size(657, 38);
            this.panelBtn.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(117, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(99, 26);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Гарах";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(12, 7);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(99, 26);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "Засах";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // ucGenParam
            // 
            this.ucGenParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGenParam.Location = new System.Drawing.Point(0, 0);
            this.ucGenParam.Name = "ucGenParam";
            this.ucGenParam.ShowDescription = true;
            this.ucGenParam.Size = new System.Drawing.Size(657, 499);
            this.ucGenParam.TabIndex = 0;
            // 
            // FormGenParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 537);
            this.Controls.Add(this.ucGenParam);
            this.Controls.Add(this.panelBtn);
            this.KeyPreview = true;
            this.Name = "FormGenParam";
            this.Text = "Ерөнхий параметр";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGenParam_FormClosing);
            this.Load += new System.EventHandler(this.FormGenParam_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGenParam_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelBtn)).EndInit();
            this.panelBtn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Template.ucParameterPanel ucGenParam;
        private DevExpress.XtraEditors.PanelControl panelBtn;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
    }
}