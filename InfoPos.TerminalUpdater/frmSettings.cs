using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EServ.Shared;

namespace InfoPos.TerminalUpdater
{
    public partial class frmSettings : DevExpress.XtraEditors.XtraForm
    {
        public frmSettings()
        {
            InitializeComponent();
            txtServer.EditValue = Static.ToStr(Static.RegisterGet(ISM.CUser.Remote.mstrRegPath, "Login", "Server", ""));
            numPort.EditValue = Static.ToStr(Static.RegisterGet(ISM.CUser.Remote.mstrRegPath, "Login", "PortNo", ""));
            numTimeOut.EditValue = Static.ToStr(Static.RegisterGet(ISM.CUser.Remote.mstrRegPath, "Login", "TimeOut", 1));
            numUserNo.EditValue = Static.ToInt(Static.RegisterGet(ISM.CUser.Remote.mstrRegPath, "Login", "UserNo", "")); 
            txtTerminalPath.EditValue=Static.ToStr(Static.RegisterGet(ISM.CUser.Remote.mstrRegPath,"Login","TerminalPath",@"C:\Program Files\InfoPos\InfoPosAdmin"));
            btnsave.Image = Resource.save;
            btnclose.Image = Resource.exit;
            btnBrowse.Image = Resource.find;
            btnBrowse.ImageLocation = ImageLocation.MiddleCenter;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Static.RegisterSave(ISM.CUser.Remote.mstrRegPath, "Login", "Server", txtServer.Text);
                Static.RegisterSave(ISM.CUser.Remote.mstrRegPath, "Login", "PortNo", numPort.Text);
                Static.RegisterSave(ISM.CUser.Remote.mstrRegPath, "Login", "TimeOut", numTimeOut.Text);
                Static.RegisterSave(ISM.CUser.Remote.mstrRegPath, "Login", "UserNo", numUserNo.Text);
                Static.RegisterSave(ISM.CUser.Remote.mstrRegPath, "Login", "TerminalPath", txtTerminalPath.EditValue);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdialog = new FolderBrowserDialog();
            if (fdialog.ShowDialog() == DialogResult.OK)
            {
                txtTerminalPath.EditValue = fdialog.SelectedPath;
            }
        }
    }
}