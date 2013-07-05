using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EServ.Shared;
namespace InfoPos.cash
{
    public partial class frmOtherPayment : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmOtherPayment(InfoPos.Core.Core core, string batchno)
        {
            InitializeComponent();
            _core = core;
            RefreshData(batchno);
        }
        public Result RefreshData(string batchno)
        { 
            Result res = new Result();
            if (batchno != "")
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 600022, 600022, new object[] { batchno });
                if (res.ResultNo == 0)
                {
                    gridControl1.DataSource = res.Data.Tables[0];
                }
            }
            else
            {
                res.ResultNo = 1;
                res.ResultDesc = "Багцын дугаар оруулна уу.";
            }
            return res;
        }
    }
}