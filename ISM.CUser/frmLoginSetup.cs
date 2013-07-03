using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace ISM.CUser
{
    public partial class frmLoginSetup : DevExpress.XtraEditors.XtraForm
    {
        public frmLoginSetup()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ISM.Lib.Static.RegisterSave(Remote.mstrRegPath, "Login", "Server", txtServer.Text);
            ISM.Lib.Static.RegisterSave(Remote.mstrRegPath, "Login", "PortNo", numPort.Text);
            ISM.Lib.Static.RegisterSave(Remote.mstrRegPath, "Login", "TimeOut", txtTimeOut.Text);

            this.Close();
        }

        private void frmLoginSetup_Load(object sender, EventArgs e)
        {
            txtServer.Text = Static.ToStr(Static.RegisterGet(Remote.mstrRegPath, "Login", "Server", ""));
            numPort.Text = Static.ToStr(Static.RegisterGet(Remote.mstrRegPath, "Login", "PortNo", ""));
            txtTimeOut.Text = Static.ToStr(Static.RegisterGet(Remote.mstrRegPath, "Login", "TimeOut", 1));


            btnCancelD.Image = Resource.navigate_cancel;
            btnConnectD.Image = Resource.button_ok;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLoginSetup_Activated(object sender, EventArgs e)
        {
            txtServer.Focus();
        }

        private void frmLoginSetup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                ISM.Lib.Static.RegisterSave(Remote.mstrRegPath, "Login", "Server", txtServer.Text);
                ISM.Lib.Static.RegisterSave(Remote.mstrRegPath, "Login", "PortNo", numPort.Text);
                ISM.Lib.Static.RegisterSave(Remote.mstrRegPath, "Login", "TimeOut", txtTimeOut.Text);

                this.Close();
            }
        }
    }
}
