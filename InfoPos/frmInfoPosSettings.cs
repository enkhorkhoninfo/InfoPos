using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISM.Touch;

namespace InfoPos
{
    public partial class frmInfoPosSettings : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmInfoPosSettings(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsTouch.Checked)
                {
                    Program.Core.IsTouch = true;
                    Program.Core.CacheSet("IsTouch", 1);
                }
                else
                {
                    Program.Core.IsTouch = false;
                    Program.Core.CacheSet("IsTouch", 0);
                }
                Program.Core.CacheSave();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            try
            {
                int _IsTouch = EServ.Shared.Static.ToInt(Program.Core.CacheGet("IsTouch"));

                if (_IsTouch == 1) IsTouch.Checked = true;
                else IsTouch.Checked = false;

                if (Program.Core.Resource != null)
                {
                    btnSave.Image = Program.Core.Resource.GetImage("navigate_save");
                    btnCancel.Image = Program.Core.Resource.GetImage("image_exit");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnParamUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnTagReaderInit_Click(object sender, EventArgs e)
        {
            if (_core.Tag.InitReader() == 0)
                MessageBox.Show("Таг уншигч холбогдоогүй байна.");
        }
    }
}
