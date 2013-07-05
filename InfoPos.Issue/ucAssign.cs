using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace InfoPos.Issue
{
    public partial class ucAssign : DevExpress.XtraEditors.XtraUserControl
    {
        int _focusedUser = 0;
        int _UserNo=0;
        string _UserName = "";
        public int UserNo
        {
            get { return _UserNo; }
            set { _UserNo = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public int FocusedUserNo
        {
            get { return _focusedUser; }
        }
        public DataTable DataSource
        {
            get 
            {
                if ((DataTable)grdData.DataSource == null) return null;
                else return (DataTable)grdData.DataSource;
            }
            set { grdData.DataSource = value; }
        }
        string oldName = "";

        public delegate void dlgAssign();
        public event dlgAssign UserAssign;

        public ucAssign()
        {
            InitializeComponent();
        }
        public void ChangeName(int PUserNo, string PUserName)
        {
            UserNo = PUserNo;
            UserName = PUserName;
            if (_UserNo == 0) lblAssignUser.Text = "";
            else lblAssignUser.Text = UserNo.ToString() + " - " + UserName;
        }
        private void btnExCo_Click(object sender, EventArgs e)
        {
            if (_UserNo != 0)
            {
                if (this.Size.Height > 20)
                {
                    this.Size = new Size(this.Width, 20);
                    btnExCo.Image = Issue.Resource.down_icon;
                    lblAssignUser.Text=oldName;
                }
                else
                {
                    this.Size = new Size(this.Width, 250);
                    btnExCo.Image = Issue.Resource.up_icon;
                    oldName = lblAssignUser.Text;
                }
            }
            else
            {
                this.Size = new Size(this.Width, 20);
                btnExCo.Image = Issue.Resource.down_icon;
            }
        }

        private void ucAssign_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Width, 20);
            btnExCo.Image = Issue.Resource.down_icon;
            if (_UserNo == 0) lblAssignUser.Text = "";
            else lblAssignUser.Text = _UserNo.ToString() + " - " + _UserName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Size = new Size(this.Width, 20);
            lblAssignUser.Text = oldName;
            btnExCo.Image = Issue.Resource.down_icon;
        }

        private void gvwData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvwData.GetFocusedDataRow() != null)
            {
                DataRow rw = gvwData.GetFocusedDataRow();
                lblAssignUser.Text = rw[0].ToString() + " - " + rw["USERLNAME"].ToString();
                _focusedUser = ISM.Lib.Static.ToInt(rw[0]);
            }
        }

        private void ucAssign_Leave(object sender, EventArgs e)
        {
            if (this.Size.Height > 20)
            {
                this.Size = new Size(this.Width, 20);
                btnExCo.Image = Issue.Resource.down_icon;
                lblAssignUser.Text = oldName;
            }
        }

        private void DoAssign_Click(object sender, EventArgs e)
        {
            UserAssign();
            if (this.Size.Height > 20)
            {
                this.Size = new Size(this.Width, 20);
                btnExCo.Image = Issue.Resource.down_icon;
                lblAssignUser.Text = oldName;
            }
            else
            {
                btnExCo.Image = Issue.Resource.down_icon;
            }
        }
    }
}
