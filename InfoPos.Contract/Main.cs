using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Shared;
namespace InfoPos.Contract
{
    public class Main
    {
        #region [ Init ]
        private Core.Core _core;
        public Main()
        {
        }
        #endregion
        #region [ Contract ]
        public void CallContract(object[] param)
        {
            _core = (Core.Core)param[0];
            string contractid = "";
            try
            {
                contractid = Static.ToStr(param[1]);
            }
            catch { contractid = ""; }
            InfoPos.Contract.frmContract frm = new InfoPos.Contract.frmContract(_core, contractid);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallContractBulk(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Contract.frmContractBulk frm = new InfoPos.Contract.frmContractBulk(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        #endregion
        #region [ WorkArea ]
        public void CallWorkArea(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Contract.frmWorkArea frm = new InfoPos.Contract.frmWorkArea(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        #endregion
    }
}
