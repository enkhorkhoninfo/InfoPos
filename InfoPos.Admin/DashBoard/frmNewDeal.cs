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
    public partial class frmNewDeal : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmNewDeal(InfoPos.Core.Core core)
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
                    _core.CacheSet("frmNewDeal_Amount", numAmount.EditValue);
                    _core.CacheSet("frmNewDeal_UnReadDay", numUnReadDay.EditValue);
                    _core.CacheSet("frmNewDeal_ReadDay", numAmount.EditValue);
                    _core.CacheSet("frmNewDeal_RefreshTime", numRefreshTime.EditValue);
                    _core.CacheSet("frmNewDeal_Type", cboType.EditValue);
                    _core.CacheSave();
                    MessageBox.Show("Амжилттай хадгалагдлаа");
                    this.Close();
                }
                else
                {
                    ErrorProvider.SetError(numRefreshTime, "5-с их утга оруулна уу.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmNewDeal_Load(object sender, EventArgs e)
        {          
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 1, "Хэлцэл");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 2, "Гэрээт баталгаа");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 3, "Хэлцэл болон гэрээт баталгаа");
            //numAmount.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "Amount", 10000));
            //numReadDay.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "ReadDay", 9));
            //numUnReadDay.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "UnReadDay", 99));
            //numRefreshTime.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "RefreshTime", 5));
            //ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboType, Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "Type", 3)));
        }

        private void frmNewDeal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}