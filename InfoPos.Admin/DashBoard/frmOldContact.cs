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
    public partial class frmOldContact : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        #endregion
        public frmOldContact(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
            }
        }

        private void frmOldContact_Load(object sender, EventArgs e)
        {
            numUserNo.EditValue = _core.CacheGet("frmOldContract_Userno", 1);
            numReadDay.EditValue = _core.CacheGet("frmOldContract_ReadDay", 9);
            numUnReadDay.EditValue = _core.CacheGet("frmOldContract_ReadDay", 99);
            numRefreshTime.EditValue = _core.CacheGet("frmOldContract_RefreshTime", 5);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Static.ToLong(numRefreshTime.EditValue) >= 5)
                {
                    _core.CacheSet("frmOldContract_Userno", numUserNo.EditValue);
                    _core.CacheSet("frmOldContract_UnReadDay", numUnReadDay.EditValue);
                    _core.CacheSet("frmOldContract_ReadDay", numReadDay.EditValue);
                    _core.CacheSet("frmOldContract_RefreshTime", numRefreshTime.EditValue);
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

        private void frmOldContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}