using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
namespace InfoPos.Admin.DashBoard
{
    public partial class frmDealRepEnq : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        int _days = 0;
        public frmDealRepEnq(InfoPos.Core.Core core)
        {
            InitializeComponent();    
            _core = core;
            if (_core.Resource != null) btnExit.Image = _core.Resource.GetImage("navigate_home");
        }

        private void frmDealRepEnq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void frmDealRepEnq_Load(object sender, EventArgs e)
        {
            RefreshDealEnq();
        }
        
        private void RefreshDealEnq()
        {
            try
            {
                Result res = new Result();
                grdDealRepEnq.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300010, 300010, null);
                if (res.ResultNo == 0)
                {
                    grdDealRepEnq.DataSource = res.Data.Tables[0];
                    SetDealEnq();
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }         
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetDealEnq()
        {
            gvwDealRepEnq.Columns[0].Caption = "Дугаар";
            gvwDealRepEnq.Columns[1].Caption = "Хэрэглэгчийн нэр";
            gvwDealRepEnq.Columns[2].Caption = "Хэлцлийн тоо";
            gvwDealRepEnq.Columns[3].Caption = "Нийт дүн";

            gvwDealRepEnq.Columns[0].OptionsColumn.AllowEdit=false;
            gvwDealRepEnq.Columns[1].OptionsColumn.AllowEdit=false;
            gvwDealRepEnq.Columns[2].OptionsColumn.AllowEdit=false;
            gvwDealRepEnq.Columns[3].OptionsColumn.AllowEdit = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}