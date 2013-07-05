using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EServ.Shared;

namespace InfoPos.Schedule
{
    public class Main
    {
        private Core.Core _core;
        public Main()
        {
        }
        public void CallCalendar(object[] param)
        {
            _core = (Core.Core)param[0];
            frmCalendar frm = new frmCalendar(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
    }
}
