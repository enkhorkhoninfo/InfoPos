using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.PostingGL
{
    public class Main
    {
        private Core.Core _core;

        public Main()
        {
        }
        public void CallPostingGL(object[] param)
        {
            _core = (Core.Core)param[0];
            frmTemp frm = new frmTemp(_core);
            frm.ShowDialog();
        }
    }
}
