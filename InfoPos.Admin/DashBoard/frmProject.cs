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
    public partial class frmProject : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmProject(InfoPos.Core.Core core)
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
                    //Static.RegisterSave(_core.ApplicationPath, "Dashboard\\Project", "ReadDay", Static.ToInt(numReadDay.EditValue));
                    //Static.RegisterSave(_core.ApplicationPath, "Dashboard\\Project", "UnReadDay", Static.ToInt(numUnReadDay.EditValue));
                    //Static.RegisterSave(_core.ApplicationPath, "Dashboard\\Project", "RefreshTime", Static.ToInt(numRefreshTime.EditValue));
                    _core.CacheSet(this.Name + "_PROJECTREADDAY", Static.ToInt(numReadDay.EditValue));
                    _core.CacheSet(this.Name + "_PROJECTUNREADDAY", Static.ToInt(numUnReadDay.EditValue));
                    _core.CacheSet(this.Name + "_PROJECTRefreshTime", Static.ToInt(numRefreshTime.EditValue));
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
            numUserno.EditValue = _core.RemoteObject.User.UserNo;
            numReadDay.EditValue = _core.CacheGetInt(this.Name + "_PROJECTREADDAY", 9);
            numUnReadDay.EditValue = _core.CacheGetInt(this.Name + "_PROJECTREADDAY", 99);
        }

        private void frmIssue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}