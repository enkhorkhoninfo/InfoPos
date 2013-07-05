using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ;
using EServ.Shared;
using ISM.Template;
using InfoPos.Core;
namespace InfoPos.Admin
{
    public partial class frmCallTxn : DevExpress.XtraEditors.XtraForm
    {
        private readonly InfoPos.Core.Core _core;
        DataTable _DT;
        public frmCallTxn(InfoPos.Core.Core core, DataTable DT)
        {
            InitializeComponent();
            _core = core;
            _DT = DT;
            InfoPos.Resource asd = new InfoPos.Resource();
            simpleButton1.Image=asd.GetImage("button_ok");
            //simpleButton1.Image = _core.Resource.GetImage("button_ok");
            simpleButton2.Image = _core.Resource.GetImage("navigate_cancel");
        }
        private void frmOption_Load(object sender, EventArgs e)
        {
            Result res = new Result();
            if (_DT == null)
            {
                MessageBox.Show("Гүйлгээний эрх байхгүй байна : " + res.ResultDesc);
            }
            else
            {
                cboTxns.Properties.DataSource = null;
                FormUtility.LookUpEdit_SetList(ref cboTxns, _DT, "TxnCode", "TxnName");
                cboTxns.Properties.Columns[0].AllowSort = DevExpress.Utils.DefaultBoolean.True;
                cboTxns.Properties.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                cboTxns.Properties.SortColumnIndex = 0;
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Static.ToInt(cboTxns.EditValue) != 0)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void frmCallTxn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
