using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;

namespace InfoPos.Admin
{
    public partial class frmMoveReDeal : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmMoveReDeal(InfoPos.Core.Core core)
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
            try
            {
                if (Static.ToLong(numRefreshTime.EditValue) >= 5)
                {
                    _core.CacheSet("frmMoveReDeal_UnReadDay", numUnReadDay.EditValue);
                    _core.CacheSet("frmMoveReDeal_ReadDay", numReadDay.EditValue);
                    _core.CacheSet("frm_MoveReDeal_RefreshTime", numRefreshTime.EditValue);
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

        private void frmNewDeal_Load(object sender, EventArgs e)
        {
            numReadDay.EditValue = _core.CacheGet("frmMoveReDeal_ReadDay", 9);
            numUnReadDay.EditValue = _core.CacheGet("frmMoveReDeal_UnReadDay", 99);
            numRefreshTime.EditValue = _core.CacheGet("frmMoveReDeal_RefreshTime", 5);
        }

        private void frmNewDeal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}