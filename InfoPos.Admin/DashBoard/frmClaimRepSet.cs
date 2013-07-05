using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;
namespace InfoPos.Admin.DashBoard
{
    public partial class frmClaimRepSet : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmClaimRepSet(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Static.ToLong(numRefreshTime.EditValue) >= 5)
            {
                _core.CacheSet("frmClaimReport_Days", numDays.EditValue);
                _core.CacheSet("frmClaimReport_RefreshTime", numRefreshTime.EditValue);
                _core.CacheSave();
                this.Close();
            }
            else
            {
                ErrorProvider.SetError(numRefreshTime, "5-с их утга оруулна уу");
            }
        }

        private void frmClaimRepSet_Load(object sender, EventArgs e)
        {
            numDays.EditValue = _core.CacheGet("frmClaimReport_Days", 99); ;
            numRefreshTime.EditValue = _core.CacheGet("frmClaimReport_RefreshTime", 5);
        }

        private void frmClaimRepSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}