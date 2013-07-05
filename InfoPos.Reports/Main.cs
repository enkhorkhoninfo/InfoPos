using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EServ.Shared;

namespace InfoPos.Reports
{
    public class Main
    {
        private Core.Core _core;
       public void CallDynamicReportTxn(object[] param)              // Динамик тайлангуудын жагсаалт авах
        {

            _core = (Core.Core)param[0];
            InfoPos.Reports.DynamicReport Dynamic = new InfoPos.Reports.DynamicReport(_core);
            Dynamic.MdiParent = _core.MainForm;
            Dynamic.Show();
        }
       public void CallBacTxn(object[] param)              // Тайлангийн form дуудах
       {
           _core = (Core.Core)param[0];
           InfoPos.Reports.BacTxnReport bactxn = new InfoPos.Reports.BacTxnReport(param);
           bactxn.ShowDialog();
       }
    }
}
