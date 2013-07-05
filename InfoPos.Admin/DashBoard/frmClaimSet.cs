using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EServ.Shared;
using InfoPos.Core;

namespace InfoPos.Admin.DashBoard
{
    public partial class frmClaimSet : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmClaimSet(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core=core;
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Static.ToLong(numRefreshTime.EditValue) >= 5)
                {
                    _core.CacheSet("frmClaim_Amount", numAmount.EditValue);
                    _core.CacheSet("frmClaim_ReadDay", numReadDay.EditValue);
                    _core.CacheSet("frmClaim_UnReadDay", numUnReadDay.EditValue);
                    _core.CacheSet("frmClaim_RefreshTime", numRefreshTime.EditValue);
                    _core.CacheSave();
                    MessageBox.Show("Амжилттай хадгалагдлаа");
                    this.Close();
                }
                else
                {
                    ErrorProvider.SetError(numRefreshTime, "5-с их утга оруулна уу.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmClaimSet_Load(object sender, EventArgs e)
        {
            numAmount.EditValue = _core.CacheGet("frmClaim_Amount", 100000);
            numReadDay.EditValue = _core.CacheGet("frmClaim_ReadDay", 9);
            numUnReadDay.EditValue = _core.CacheGet("frmClaim_UnReadDay", 9);
            numRefreshTime.EditValue = _core.CacheGet("frmClaim_RefreshTime", 9);
        }

        private void frmClaimSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}