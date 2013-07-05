using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EServ.Shared;

namespace InfoPos.Admin.DashBoard
{
    public partial class frmDealRepEdt : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmDealRepEdt(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
            }
            numRefreshTime.EditValue = _core.CacheGet("frmDealReport_RefreshTime", 5);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Static.ToLong(numRefreshTime.EditValue) >= 5)
            {
                _core.CacheSet("frmDealReport_RefreshTime", numRefreshTime.EditValue);
                _core.CacheSave();
                this.Close();
            }
            else
            {
                ErrorProvider.SetError(numRefreshTime, "5-с их утга оруулна уу.");
            }
        }

        private void frmDealRepEdt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}