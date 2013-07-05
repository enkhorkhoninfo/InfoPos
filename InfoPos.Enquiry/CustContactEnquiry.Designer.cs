namespace InfoPos.Enquiry
{
    partial class CustContactEnquiry
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
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grdNote = new DevExpress.XtraGrid.GridControl();
            this.gvwNote = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdPosition = new DevExpress.XtraGrid.GridControl();
            this.gvwPosition = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdCustomer = new DevExpress.XtraGrid.GridControl();
            this.gvwCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ucAdd = new ISM.Template.ucDynamicDataPanel();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1,
            this.navBarItem2,
            this.navBarItem3,
            this.navBarItem4});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 213;
            this.navBarControl1.Size = new System.Drawing.Size(213, 415);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Лавлагаа";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem1),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem2),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem3)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "Үндсэн мэдээлэл";
            this.navBarItem1.Name = "navBarItem1";
            this.navBarItem1.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem1_LinkClicked);
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "Хаягийн жагсаалт";
            this.navBarItem2.Name = "navBarItem2";
            this.navBarItem2.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem2_LinkClicked);
            // 
            // navBarItem3
            // 
            this.navBarItem3.Caption = "Товч дүгнэлт";
            this.navBarItem3.Name = "navBarItem3";
            this.navBarItem3.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem3_LinkClicked);
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "Гарах";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem4)});
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // navBarItem4
            // 
            this.navBarItem4.Caption = "Гарах";
            this.navBarItem4.Name = "navBarItem4";
            this.navBarItem4.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem4_LinkClicked);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.ucAdd);
            this.groupControl1.Controls.Add(this.grdNote);
            this.groupControl1.Controls.Add(this.grdPosition);
            this.groupControl1.Controls.Add(this.grdCustomer);
            this.groupControl1.Location = new System.Drawing.Point(219, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(494, 403);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Жагсаалт лавлагаа";
            // 
            // grdNote
            // 
            this.grdNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNote.Location = new System.Drawing.Point(2, 22);
            this.grdNote.MainView = this.gvwNote;
            this.grdNote.Name = "grdNote";
            this.grdNote.Size = new System.Drawing.Size(490, 379);
            this.grdNote.TabIndex = 9;
            this.grdNote.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwNote});
            this.grdNote.Visible = false;
            // 
            // gvwNote
            // 
            this.gvwNote.GridControl = this.grdNote;
            this.gvwNote.Name = "gvwNote";
            // 
            // grdPosition
            // 
            this.grdPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPosition.Location = new System.Drawing.Point(2, 22);
            this.grdPosition.MainView = this.gvwPosition;
            this.grdPosition.Name = "grdPosition";
            this.grdPosition.Size = new System.Drawing.Size(490, 379);
            this.grdPosition.TabIndex = 1;
            this.grdPosition.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwPosition});
            this.grdPosition.Visible = false;
            // 
            // gvwPosition
            // 
            this.gvwPosition.GridControl = this.grdPosition;
            this.gvwPosition.Name = "gvwPosition";
            // 
            // grdCustomer
            // 
            this.grdCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCustomer.Location = new System.Drawing.Point(2, 22);
            this.grdCustomer.MainView = this.gvwCustomer;
            this.grdCustomer.Name = "grdCustomer";
            this.grdCustomer.Size = new System.Drawing.Size(490, 379);
            this.grdCustomer.TabIndex = 0;
            this.grdCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwCustomer});
            // 
            // gvwCustomer
            // 
            this.gvwCustomer.GridControl = this.grdCustomer;
            this.gvwCustomer.Name = "gvwCustomer";
            // 
            // ucAdd
            // 
            this.ucAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAdd.Location = new System.Drawing.Point(2, 22);
            this.ucAdd.Name = "ucAdd";
            this.ucAdd.Size = new System.Drawing.Size(490, 379);
            this.ucAdd.TabIndex = 10;
            this.ucAdd.TableRowKey = ((ulong)(0ul));
            this.ucAdd.TableTypeId = ((ulong)(0ul));
            this.ucAdd.Visible = false;
            // 
            // CustContactEnquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 415);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.navBarControl1);
            this.Name = "CustContactEnquiry";
            this.Text = "Холбоо барисан харилцагчийн лавалгаа";
            this.Load += new System.EventHandler(this.CustContactEnquiry_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CustContactEnquiry_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwCustomer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem3;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private ISM.Template.ucDynamicDataPanel ucAdd;
        private DevExpress.XtraGrid.GridControl grdNote;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwNote;
        private DevExpress.XtraGrid.GridControl grdPosition;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwPosition;
        private DevExpress.XtraGrid.GridControl grdCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwCustomer;
    }
}