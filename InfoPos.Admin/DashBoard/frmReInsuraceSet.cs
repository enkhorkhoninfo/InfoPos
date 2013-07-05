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
    public partial class frmReInsuraceSet : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmReInsuraceSet(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if(_core.Resource!=null)
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
                    _core.CacheSet("frmReInsurance_UserNo", numUserNo.EditValue);
                    _core.CacheSet("frmReInsurance_UnReadDay", numUnReadDay.EditValue);
                    _core.CacheSet("frmReInsurance_ReadDay", numReadDay.EditValue);
                    _core.CacheSet("frmReInsurance_RefreshTime", numRefreshTime.EditValue);
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

        private void frmReInsuraceSet_Load(object sender, EventArgs e)
        {
            numUserNo.EditValue = _core.CacheGet("frmReInsurance_UserNo", 1);
            numReadDay.EditValue = _core.CacheGet("frmReInsurance_ReadDay", 9);
            numUnReadDay.EditValue = _core.CacheGet("frmReInsurance_UnReadDay", 99);
            numRefreshTime.EditValue = _core.CacheGet("frmReInsurance_RefreshTime", 5);
        }

        private void frmReInsuraceSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}