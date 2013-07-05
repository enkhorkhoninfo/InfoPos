namespace InfoPos.Order
{
    partial class ucOrderGroup
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnGroupDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnGroupAdd = new DevExpress.XtraEditors.SimpleButton();
            this.tmeGroupEnd = new DevExpress.XtraEditors.TimeEdit();
            this.tmeGroupStart = new DevExpress.XtraEditors.TimeEdit();
            this.cboTabGroupRunTime = new DevExpress.XtraEditors.LookUpEdit();
            this.txtTabGroupNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.dteTabGroupOrderDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtProdQty = new DevExpress.XtraEditors.CalcEdit();
            this.btnProdFind = new DevExpress.XtraEditors.SimpleButton();
            this.btnProdSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnProdDelete = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl35 = new DevExpress.XtraEditors.LabelControl();
            this.txtProdProdNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tmeGroupEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmeGroupStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTabGroupRunTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTabGroupNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTabGroupOrderDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTabGroupOrderDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdProdNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 22);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(377, 165);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу.";
            this.gridView1.Name = "gridView1";
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnGroupDelete);
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Controls.Add(this.btnGroupAdd);
            this.groupControl1.Controls.Add(this.tmeGroupEnd);
            this.groupControl1.Controls.Add(this.tmeGroupStart);
            this.groupControl1.Controls.Add(this.cboTabGroupRunTime);
            this.groupControl1.Controls.Add(this.txtTabGroupNo);
            this.groupControl1.Controls.Add(this.labelControl16);
            this.groupControl1.Controls.Add(this.dteTabGroupOrderDate);
            this.groupControl1.Controls.Add(this.labelControl17);
            this.groupControl1.Controls.Add(this.labelControl20);
            this.groupControl1.Controls.Add(this.labelControl18);
            this.groupControl1.Controls.Add(this.labelControl19);
            this.groupControl1.Location = new System.Drawing.Point(3, 64);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(381, 189);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Багц";
            // 
            // btnGroupDelete
            // 
            this.btnGroupDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnGroupDelete.Appearance.Options.UseFont = true;
            this.btnGroupDelete.Location = new System.Drawing.Point(158, 155);
            this.btnGroupDelete.Name = "btnGroupDelete";
            this.btnGroupDelete.Size = new System.Drawing.Size(127, 30);
            this.btnGroupDelete.TabIndex = 45;
            this.btnGroupDelete.Text = "Устгах";
            this.btnGroupDelete.Click += new System.EventHandler(this.btnGroupDelete_Click);
            // 
            // btnGroupAdd
            // 
            this.btnGroupAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnGroupAdd.Appearance.Options.UseFont = true;
            this.btnGroupAdd.Location = new System.Drawing.Point(17, 155);
            this.btnGroupAdd.Name = "btnGroupAdd";
            this.btnGroupAdd.Size = new System.Drawing.Size(127, 30);
            this.btnGroupAdd.TabIndex = 44;
            this.btnGroupAdd.Text = "Нэмэх";
            this.btnGroupAdd.Click += new System.EventHandler(this.btnGroupAdd_Click);
            // 
            // tmeGroupEnd
            // 
            this.tmeGroupEnd.EditValue = new System.DateTime(2012, 7, 1, 0, 0, 0, 0);
            this.tmeGroupEnd.Location = new System.Drawing.Point(158, 104);
            this.tmeGroupEnd.Name = "tmeGroupEnd";
            this.tmeGroupEnd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.tmeGroupEnd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tmeGroupEnd.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.tmeGroupEnd.Properties.Appearance.Options.UseBackColor = true;
            this.tmeGroupEnd.Properties.Appearance.Options.UseFont = true;
            this.tmeGroupEnd.Properties.Appearance.Options.UseForeColor = true;
            this.tmeGroupEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tmeGroupEnd.Size = new System.Drawing.Size(156, 21);
            this.tmeGroupEnd.TabIndex = 37;
            // 
            // tmeGroupStart
            // 
            this.tmeGroupStart.EditValue = new System.DateTime(2012, 7, 1, 0, 0, 0, 0);
            this.tmeGroupStart.Location = new System.Drawing.Point(158, 78);
            this.tmeGroupStart.Name = "tmeGroupStart";
            this.tmeGroupStart.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.tmeGroupStart.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tmeGroupStart.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.tmeGroupStart.Properties.Appearance.Options.UseBackColor = true;
            this.tmeGroupStart.Properties.Appearance.Options.UseFont = true;
            this.tmeGroupStart.Properties.Appearance.Options.UseForeColor = true;
            this.tmeGroupStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tmeGroupStart.Size = new System.Drawing.Size(156, 21);
            this.tmeGroupStart.TabIndex = 36;
            // 
            // cboTabGroupRunTime
            // 
            this.cboTabGroupRunTime.Location = new System.Drawing.Point(158, 130);
            this.cboTabGroupRunTime.Name = "cboTabGroupRunTime";
            this.cboTabGroupRunTime.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cboTabGroupRunTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboTabGroupRunTime.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.cboTabGroupRunTime.Properties.Appearance.Options.UseBackColor = true;
            this.cboTabGroupRunTime.Properties.Appearance.Options.UseFont = true;
            this.cboTabGroupRunTime.Properties.Appearance.Options.UseForeColor = true;
            this.cboTabGroupRunTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTabGroupRunTime.Size = new System.Drawing.Size(156, 21);
            this.cboTabGroupRunTime.TabIndex = 38;
            // 
            // txtTabGroupNo
            // 
            this.txtTabGroupNo.EditValue = "";
            this.txtTabGroupNo.Location = new System.Drawing.Point(158, 25);
            this.txtTabGroupNo.Name = "txtTabGroupNo";
            this.txtTabGroupNo.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtTabGroupNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtTabGroupNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtTabGroupNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtTabGroupNo.Properties.Appearance.Options.UseFont = true;
            this.txtTabGroupNo.Properties.Appearance.Options.UseForeColor = true;
            this.txtTabGroupNo.Properties.Mask.EditMask = "\\d{0,20}";
            this.txtTabGroupNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtTabGroupNo.Size = new System.Drawing.Size(156, 21);
            this.txtTabGroupNo.TabIndex = 34;
            this.txtTabGroupNo.ToolTipTitle = "Харилцагч оруулна уу";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl16.Location = new System.Drawing.Point(7, 28);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(91, 14);
            this.labelControl16.TabIndex = 39;
            this.labelControl16.Text = "Багцийн дугаар:";
            // 
            // dteTabGroupOrderDate
            // 
            this.dteTabGroupOrderDate.EditValue = null;
            this.dteTabGroupOrderDate.Location = new System.Drawing.Point(158, 52);
            this.dteTabGroupOrderDate.Name = "dteTabGroupOrderDate";
            this.dteTabGroupOrderDate.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.dteTabGroupOrderDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dteTabGroupOrderDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.dteTabGroupOrderDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteTabGroupOrderDate.Properties.Appearance.Options.UseFont = true;
            this.dteTabGroupOrderDate.Properties.Appearance.Options.UseForeColor = true;
            this.dteTabGroupOrderDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTabGroupOrderDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTabGroupOrderDate.Size = new System.Drawing.Size(156, 21);
            this.dteTabGroupOrderDate.TabIndex = 35;
            // 
            // labelControl17
            // 
            this.labelControl17.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl17.Location = new System.Drawing.Point(7, 55);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(97, 14);
            this.labelControl17.TabIndex = 40;
            this.labelControl17.Text = "Бүртгэсэн огноо:";
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl20.Location = new System.Drawing.Point(7, 133);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(108, 14);
            this.labelControl20.TabIndex = 43;
            this.labelControl20.Text = "Ажиллах давтамж:";
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl18.Location = new System.Drawing.Point(7, 107);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(64, 14);
            this.labelControl18.TabIndex = 41;
            this.labelControl18.Text = "Дуусах цаг:";
            // 
            // labelControl19
            // 
            this.labelControl19.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl19.Location = new System.Drawing.Point(7, 81);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(58, 14);
            this.labelControl19.TabIndex = 42;
            this.labelControl19.Text = "Эхлэх цаг:";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(424, 489);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.DoubleClick += new System.EventHandler(this.gridControl2_DoubleClick);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу.";
            this.gridView2.Name = "gridView2";
            this.gridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView2_FocusedRowChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtProdQty);
            this.groupControl2.Controls.Add(this.btnProdFind);
            this.groupControl2.Controls.Add(this.btnProdSelect);
            this.groupControl2.Controls.Add(this.btnProdDelete);
            this.groupControl2.Controls.Add(this.labelControl35);
            this.groupControl2.Controls.Add(this.txtProdProdNo);
            this.groupControl2.Controls.Add(this.labelControl32);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl2.Location = new System.Drawing.Point(424, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(399, 489);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Өгөгдөл";
            // 
            // txtProdQty
            // 
            this.txtProdQty.Location = new System.Drawing.Point(151, 26);
            this.txtProdQty.Name = "txtProdQty";
            this.txtProdQty.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtProdQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtProdQty.Properties.Appearance.Options.UseBackColor = true;
            this.txtProdQty.Properties.Appearance.Options.UseFont = true;
            this.txtProdQty.Properties.AutoHeight = false;
            this.txtProdQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtProdQty.Size = new System.Drawing.Size(149, 25);
            this.txtProdQty.TabIndex = 1;
            // 
            // btnProdFind
            // 
            this.btnProdFind.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnProdFind.Appearance.Options.UseFont = true;
            this.btnProdFind.Location = new System.Drawing.Point(306, 56);
            this.btnProdFind.Name = "btnProdFind";
            this.btnProdFind.Size = new System.Drawing.Size(85, 30);
            this.btnProdFind.TabIndex = 84;
            this.btnProdFind.Text = "Хайх";
            this.btnProdFind.Click += new System.EventHandler(this.btnProdFind_Click);
            // 
            // btnProdSelect
            // 
            this.btnProdSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProdSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnProdSelect.Appearance.Options.UseFont = true;
            this.btnProdSelect.Location = new System.Drawing.Point(307, 453);
            this.btnProdSelect.Name = "btnProdSelect";
            this.btnProdSelect.Size = new System.Drawing.Size(85, 30);
            this.btnProdSelect.TabIndex = 83;
            this.btnProdSelect.Text = "Сонгох";
            this.btnProdSelect.Click += new System.EventHandler(this.btnProdSelect_Click);
            // 
            // btnProdDelete
            // 
            this.btnProdDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnProdDelete.Appearance.Options.UseFont = true;
            this.btnProdDelete.Location = new System.Drawing.Point(306, 92);
            this.btnProdDelete.Name = "btnProdDelete";
            this.btnProdDelete.Size = new System.Drawing.Size(85, 30);
            this.btnProdDelete.TabIndex = 82;
            this.btnProdDelete.Text = "Устгах";
            this.btnProdDelete.Click += new System.EventHandler(this.btnProdDelete_Click);
            // 
            // labelControl35
            // 
            this.labelControl35.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl35.Location = new System.Drawing.Point(8, 30);
            this.labelControl35.Name = "labelControl35";
            this.labelControl35.Size = new System.Drawing.Size(69, 14);
            this.labelControl35.TabIndex = 76;
            this.labelControl35.Text = "Тоо ширхэг:";
            // 
            // txtProdProdNo
            // 
            this.txtProdProdNo.EditValue = "";
            this.txtProdProdNo.Location = new System.Drawing.Point(151, 58);
            this.txtProdProdNo.Name = "txtProdProdNo";
            this.txtProdProdNo.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtProdProdNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtProdProdNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtProdProdNo.Properties.Appearance.Options.UseFont = true;
            this.txtProdProdNo.Properties.AutoHeight = false;
            this.txtProdProdNo.Properties.ReadOnly = true;
            this.txtProdProdNo.Size = new System.Drawing.Size(149, 25);
            this.txtProdProdNo.TabIndex = 79;
            this.txtProdProdNo.ToolTipTitle = "Харилцагч оруулна уу";
            // 
            // labelControl32
            // 
            this.labelControl32.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl32.Location = new System.Drawing.Point(8, 64);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(133, 14);
            this.labelControl32.TabIndex = 72;
            this.labelControl32.Text = "Бүтээгдэхүүний дугаар:";
            // 
            // ucOrderGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "ucOrderGroup";
            this.Size = new System.Drawing.Size(823, 489);
            this.Load += new System.EventHandler(this.ucOrderGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tmeGroupEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmeGroupStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTabGroupRunTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTabGroupNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTabGroupOrderDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTabGroupOrderDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdProdNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnGroupDelete;
        private DevExpress.XtraEditors.SimpleButton btnGroupAdd;
        private DevExpress.XtraEditors.TimeEdit tmeGroupEnd;
        private DevExpress.XtraEditors.TimeEdit tmeGroupStart;
        private DevExpress.XtraEditors.LookUpEdit cboTabGroupRunTime;
        private DevExpress.XtraEditors.TextEdit txtTabGroupNo;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.DateEdit dteTabGroupOrderDate;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.SimpleButton btnProdDelete;
        private DevExpress.XtraEditors.LabelControl labelControl35;
        private DevExpress.XtraEditors.TextEdit txtProdProdNo;
        private DevExpress.XtraEditors.LabelControl labelControl32;
        private DevExpress.XtraEditors.SimpleButton btnProdSelect;
        private DevExpress.XtraEditors.SimpleButton btnProdFind;
        private DevExpress.XtraEditors.CalcEdit txtProdQty;
    }
}
