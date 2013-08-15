using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoPos.PreSale
{
    public class Main
    {
        Core.Core _core;
        #region[PreSale]
        public void CallPreSale(object[] param)
        {
            string presaleno;
            try
            {
                _core = (Core.Core)param[0];
                presaleno = Convert.ToString(param[1]);
                InfoPos.PreSale.frmPreSale frm = new InfoPos.PreSale.frmPreSale(_core, presaleno);
                frm.MdiParent = _core.MainForm;
                frm.Show();
            }
            catch
            {
                InfoPos.PreSale.frmPreSale frm = new InfoPos.PreSale.frmPreSale(_core, "");
                frm.MdiParent = _core.MainForm;
                frm.Show();
            }
        }
        public void CallPreSaleMain(object[] param)
        {
            try
            {
                _core = (Core.Core)param[0];
                //orderno = Convert.ToString(param[1]);
                InfoPos.PreSale.frmPreSaleMain frm = new InfoPos.PreSale.frmPreSaleMain(_core);
                frm.MdiParent = _core.MainForm;
                frm.Show();
            }
            catch
            {
                InfoPos.PreSale.frmPreSaleMain frm = new InfoPos.PreSale.frmPreSaleMain(_core);
                frm.MdiParent = _core.MainForm;
                frm.Show();
            }
        }
        public void CallPreSaleConform(object[] param)
        {
            string orderno;
            try
            {
                _core = (Core.Core)param[0];
                orderno = Convert.ToString(param[1]);
                //InfoPos.Order.frmOrderConfirm frm = new InfoPos.Order.frmOrderConfirm(_core, orderno);
                //frm.MdiParent = _core.MainForm;
                //frm.Show();
            }
            catch
            {
                //InfoPos.Order.frmPreSale frm = new InfoPos.Order.frmPreSale(_core, "");
                //frm.MdiParent = _core.MainForm;
                //frm.Show();
            }
        }
        public void CallPreSaleCancel(object[] param)
        {
            string orderno;
            try
            {
                _core = (Core.Core)param[0];
                orderno = Convert.ToString(param[1]);
                //InfoPos.Order.frmOrderCancel frm = new InfoPos.Order.frmOrderCancel(_core, orderno);
                //frm.MdiParent = _core.MainForm;
                //frm.Show();
            }
            catch
            {
                //InfoPos.Order.frmPreSale frm = new InfoPos.Order.frmPreSale(_core, "");
                //frm.MdiParent = _core.MainForm;
                //frm.Show();
            }
        }
        public void CallPreSaleRecovery(object[] param)
        {
            string orderno;
            try
            {
                _core = (Core.Core)param[0];
                orderno = Convert.ToString(param[1]);
                //InfoPos.Order.frmOrderRecovery frm = new InfoPos.Order.frmOrderRecovery(_core, orderno);
                //frm.MdiParent = _core.MainForm;
                //frm.Show();
            }
            catch
            {
                //InfoPos.Order.frmPreSale frm = new InfoPos.Order.frmPreSale(_core, "");
                //frm.MdiParent = _core.MainForm;
                //frm.Show();
            }
        }
        #endregion
    }

}
