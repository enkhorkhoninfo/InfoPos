using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using ISM.Touch;
using InfoPos.Core;
using EServ.Shared;

namespace infopos.cash
{
    public partial class Main : ISM.Touch.ITouchCall
    {
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                frmSaleNew frm;

                switch ( item.Row*10 + item.Col)
                {
                    case 12:
                        // New
                        frm = new frmSaleNew(param, 0);
                        frm.MdiParent = parent;
                        frm.Show();
                        break;
                    case 22:
                        // Update
                        frm = new frmSaleNew(param, 1);
                        frm.MdiParent = parent;
                        frm.Show();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {

            frmSaleNew frm;
            

            MessageBox.Show(string.Format("CalcSales Form2: Call. key={0} r={1} c={2}", buttonkey, item.Row, item.Col));

            if (item.Row == 2 && item.Col == 2)
            {
                // Борлуулалт хайх
                //xtraTabControl1.SelectedTabPageIndex = 5;
            }
        }
    }
}
