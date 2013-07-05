using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.EOD
{
    public class Main
    {
        private Core.Core _core;

        public Main()
        {
        }
        public void CallEODProcess(object[] param)
        {
            _core = (Core.Core)param[0];
            IPOSEODProcess frm = new IPOSEODProcess(_core);
            frm.ShowDialog();
        }
    }
}
