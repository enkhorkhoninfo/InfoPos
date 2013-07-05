using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoPos.Order
{
    public class Main
    {
        Core.Core _core;
        #region[Order]
        public void CallOrder(object[] param)
        {
            string orderno;
            try
            {
                _core = (Core.Core)param[0];
                orderno = Convert.ToString(param[1]);
                InfoPos.Order.frmOrder frm = new InfoPos.Order.frmOrder(_core, orderno);
                frm.MdiParent = _core.MainForm;
                frm.Show();
            }
            catch
            {
                InfoPos.Order.frmOrder frm = new InfoPos.Order.frmOrder(_core, "");
                frm.MdiParent = _core.MainForm;
                frm.Show();
            }
        }
        #endregion
    }

}
