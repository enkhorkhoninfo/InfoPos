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
    public partial class frmClaimRepEnq : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        int _days = 0;
        public frmClaimRepEnq(InfoPos.Core.Core core, int days)
        {
            InitializeComponent();    
            _core = core;
            _days = days;
            if (_core.Resource != null)
            {
                btnExit.Image = _core.Resource.GetImage("navigate_home");
            }
        }

        private void frmDealRepEnq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void frmDealRepEnq_Load(object sender, EventArgs e)
        {
            RefreshClaimEnq();
        }

        private void RefreshClaimEnq()
        {
            try
            {
                Result res = new Result();
                grdClaimRepEnq.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300011, 300011, new object[]{Static.ToDate(_core.TxnDate),Static.ToInt(_days)});
                if (res.ResultNo == 0)
                {
                    grdClaimRepEnq.DataSource = res.Data.Tables[0];
                    SetClaimEnq();
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
        private void SetClaimEnq()
        {
            gvwClaimRepEnq.Columns[0].Caption = "Дугаар";
            gvwClaimRepEnq.Columns[1].Caption = "Хэрэглэгчийн нэр";
            gvwClaimRepEnq.Columns[2].Caption = "Нийт дүн";
            gvwClaimRepEnq.Columns[3].Caption = "Нөхөн төлбөрийн тоо";

            gvwClaimRepEnq.Columns[0].OptionsColumn.AllowEdit = false;
            gvwClaimRepEnq.Columns[1].OptionsColumn.AllowEdit = false;
            gvwClaimRepEnq.Columns[2].OptionsColumn.AllowEdit = false;
            gvwClaimRepEnq.Columns[3].OptionsColumn.AllowEdit = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}