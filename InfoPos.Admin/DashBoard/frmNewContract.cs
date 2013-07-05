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
    public partial class frmNewContract : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmNewContract(InfoPos.Core.Core core)
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
                    _core.CacheSet("frmNewContract_UserNo", numUserno.EditValue);
                    _core.CacheSet("frmNewContract_ReadDay", numReadDay.EditValue);
                    _core.CacheSet("frmNewContract_UnReadDay", numUnReadDay.EditValue);
                    _core.CacheSet("frmNewContract_RefreshTime", numRefreshTime.EditValue);
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

        private void frmIssue_Load(object sender, EventArgs e)
        {
            numUserno.EditValue = _core.CacheGet("frmNewContract_ReadDay", 9);
            numUserno.EditValue = _core.CacheGet("frmNewContract_UserNo", 1);
            numUserno.EditValue = _core.CacheGet("frmNewContract_UnReadDay", 99);
            numUserno.EditValue = _core.CacheGet("frmNewContract_RefreshTime", 5);
        }

        private void frmIssue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}