using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace InfoPos.Order
{
    public partial class frmProductList : DevExpress.XtraEditors.XtraForm
    {
        public frmProductList(InfoPos.Core.Core core)
        {
            InitializeComponent();
            ucProductList1.Core = core;
            ucProductList1.Remote = core.RemoteObject;
            ucProductList1.Resource = core.Resource;
            ucProductList1.DataRefresh(1);
        }
    }
}