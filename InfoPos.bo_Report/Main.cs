using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EServ.Shared;
namespace InfoPos.bo_Reports
{
    public class Main
    {
        #region [ Init ]
        private Core.Core _core;
        public Main()
        {
        }
        #endregion
        #region [ Excel Report ]
        public void CallReports(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.bo_Reports.frmReport frm = new InfoPos.bo_Reports.frmReport(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        #endregion
    }
}
