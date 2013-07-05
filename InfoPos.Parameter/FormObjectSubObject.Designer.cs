namespace InfoPos.Parameter
{
    partial class FormObjectSubObject
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
            this.ucObject = new ISM.Template.ucTogglePanel();
            this.pnlObject = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.grdSubObject = new DevExpress.XtraGrid.GridControl();
            this.gvwSubObject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnObjectID = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl34 = new DevExpress.XtraEditors.LabelControl();
            this.txtObjectID = new DevExpress.XtraEditors.TextEdit();
            this.ucObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlObject)).BeginInit();
            this.pnlObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwSubObject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtObjectID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ucObject
            // 
            this.ucObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucObject.Controls.Add(this.pnlObject);
            this.ucObject.Location = new System.Drawing.Point(4, 4);
            this.ucObject.Name = "ucObject";
            this.ucObject.Size = new System.Drawing.Size(656, 515);
            this.ucObject.TabIndex = 0;
            this.ucObject.ToggleShowDelete = false;
            this.ucObject.ToggleShowEdit = false;
            this.ucObject.ToggleShowExit = false;
            this.ucObject.ToggleShowNew = false;
            this.ucObject.ToggleShowReject = false;
            this.ucObject.ToggleShowSave = false;
            // 
            // pnlObject
            // 
            this.pnlObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlObject.Controls.Add(this.splitterControl3);
            this.pnlObject.Controls.Add(this.grdSubObject);
            this.pnlObject.Controls.Add(this.panelControl1);
            this.pnlObject.Location = new System.Drawing.Point(0, 3);
            this.pnlObject.Name = "pnlObject";
            this.pnlObject.Size = new System.Drawing.Size(653, 475);
            this.pnlObject.TabIndex = 20;
            // 
            // splitterControl3
            // 
            this.splitterControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl3.Location = new System.Drawing.Point(417, 2);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(5, 471);
            this.splitterControl3.TabIndex = 1;
            this.splitterControl3.TabStop = false;
            // 
            // grdSubObject
            // 
            this.grdSubObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSubObject.Location = new System.Drawing.Point(2, 2);
            this.grdSubObject.MainView = this.gvwSubObject;
            this.grdSubObject.Name = "grdSubObject";
            this.grdSubObject.Size = new System.Drawing.Size(420, 471);
            this.grdSubObject.TabIndex = 0;
            this.grdSubObject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwSubObject});
            // 
            // gvwSubObject
            // 
            this.gvwSubObject.GridControl = this.grdSubObject;
            this.gvwSubObject.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            this.gvwSubObject.Name = "gvwSubObject";
            this.gvwSubObject.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvwObject_FocusedRowChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnObjectID);
            this.panelControl1.Controls.Add(this.labelControl34);
            this.panelControl1.Controls.Add(this.txtObjectID);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(422, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(229, 471);
            this.panelControl1.TabIndex = 2;
            // 
            // btnObjectID
            // 
            this.btnObjectID.Enabled = false;
            this.btnObjectID.Location = new System.Drawing.Point(115, 29);
            this.btnObjectID.Name = "btnObjectID";
            this.btnObjectID.Size = new System.Drawing.Size(103, 23);
            this.btnObjectID.TabIndex = 208;
            this.btnObjectID.Text = "Зүйл сонгох";
            this.btnObjectID.Click += new System.EventHandler(this.btnObjectID_Click_1);
            // 
            // labelControl34
            // 
            this.labelControl34.Location = new System.Drawing.Point(6, 15);
            this.labelControl34.Name = "labelControl34";
            this.labelControl34.Size = new System.Drawing.Size(121, 13);
            this.labelControl34.TabIndex = 207;
            this.labelControl34.Text = "Зүйлийн зүйлийн төрөл:";
            // 
            // txtObjectID
            // 
            this.txtObjectID.Location = new System.Drawing.Point(6, 32);
            this.txtObjectID.Name = "txtObjectID";
            this.txtObjectID.Properties.Mask.EditMask = "\\d{0,16}";
            this.txtObjectID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtObjectID.Size = new System.Drawing.Size(103, 20);
            this.txtObjectID.TabIndex = 206;
            this.txtObjectID.ToolTipTitle = "Зүйлийн дугаар оруулаагүй байна";
            // 
            // FormObjectSubObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 533);
            this.Controls.Add(this.ucObject);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(678, 560);
            this.Name = "FormObjectSubObject";
            this.Text = "Зүйлийн зүйлийн бүртгэл";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormObjectSubObject_FormClosing);
            this.Load += new System.EventHandler(this.FormObject_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormObjectSubObject_KeyDown);
            this.ucObject.ResumeLayout(false);
            this.ucObject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlObject)).EndInit();
            this.pnlObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSubObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwSubObject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtObjectID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ISM.Template.ucTogglePanel ucObject;
        private DevExpress.XtraEditors.PanelControl pnlObject;
        private DevExpress.XtraEditors.SplitterControl splitterControl3;
        private DevExpress.XtraGrid.GridControl grdSubObject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwSubObject;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnObjectID;
        private DevExpress.XtraEditors.LabelControl labelControl34;
        private DevExpress.XtraEditors.TextEdit txtObjectID;
    }
}