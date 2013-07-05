namespace InfoPos.Parameter
{
    partial class FormFormula
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numFormulaID = new DevExpress.XtraEditors.TextEdit();
            this.mmeFormula = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFormulaID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeFormula.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.numFormulaID);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.mmeFormula);
            this.groupControl1.Size = new System.Drawing.Size(575, 394);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 416);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(117, 416);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(222, 416);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(327, 416);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(432, 416);
            // 
            // panelControl1
            // 
            this.panelControl1.Size = new System.Drawing.Size(840, 398);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(256, 0);
            this.splitterControl1.Size = new System.Drawing.Size(5, 398);
            // 
            // panelControl2
            // 
            this.panelControl2.Size = new System.Drawing.Size(256, 398);
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(261, 0);
            this.panelControl3.Size = new System.Drawing.Size(579, 398);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(37, 40);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Томъёоны ID :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(37, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Томъёо :";
            // 
            // numFormulaID
            // 
            this.numFormulaID.Location = new System.Drawing.Point(144, 37);
            this.numFormulaID.Name = "numFormulaID";
            this.numFormulaID.Properties.Mask.EditMask = "\\d{0,16}";
            this.numFormulaID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.numFormulaID.Size = new System.Drawing.Size(313, 20);
            this.numFormulaID.TabIndex = 2;
            this.numFormulaID.ToolTipTitle = "Томъёоны ID оруулна уу";
            // 
            // mmeFormula
            // 
            this.mmeFormula.Location = new System.Drawing.Point(144, 65);
            this.mmeFormula.Name = "mmeFormula";
            this.mmeFormula.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.mmeFormula.Properties.MaxLength = 5000;
            this.mmeFormula.Size = new System.Drawing.Size(313, 324);
            this.mmeFormula.TabIndex = 3;
            this.mmeFormula.ToolTipTitle = "Томъёо оруулна уу";
            // 
            // FormFormula
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 449);
            this.KeyPreview = true;
            this.Name = "FormFormula";
            this.Text = "Нөөцийн сангийн томъёо";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFormula_FormClosing);
            this.Load += new System.EventHandler(this.FormFormula_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormFormula_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numFormulaID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmeFormula.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit numFormulaID;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit mmeFormula;
    }
}