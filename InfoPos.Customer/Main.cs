using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Shared;

namespace InfoPos.Customer
{
    public class Main
    {
        private InfoPos.Core.Core _core;

        public Main()
        {
        }
        public void CallCustomerProp(object[] param)
        {
            _core = (InfoPos.Core.Core)param[0];
            long customerid = 0;
            int classcode=0;
            try
            {
                customerid = Static.ToLong(param[1]);
                classcode = Static.ToInt(param[2]);
            }
            catch { customerid = 0; classcode = 0; }
            InfoPos.Customer.CustomerProp frm = new InfoPos.Customer.CustomerProp(_core, customerid, classcode);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallCustomerInfo(object[] param)
        {
            _core = (Core.Core)param[0];
            long customerid = 0;
            int classcode = 0;
            try
            {
                customerid = Static.ToLong(param[1]);
                classcode = Static.ToInt(param[2]);
            }
            catch { customerid = 0; classcode = 0; }
            InfoPos.Customer.CustomerInfo frm = new InfoPos.Customer.CustomerInfo(_core, customerid, classcode);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
    }
}
