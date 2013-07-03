using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace InfoPos
{
    public partial class frmAbout : Form
    {
        InfoPos.Core.Core _core = null;

        public void ShowVersionDetail(InfoPos.Core.Core core)
        {
            if (core == null || core.RemoteObject == null) return;

            _core = core;
            
            Result res =_core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510099, 110000, null);
            if (res.ResultNo == 0)
            {
                memoEdit1.Text = res.ResultDesc;
            }
        }

        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            //pictureEdit1.Image = InfoPos.frmLogin.pictureBox1_Image;
            lblVersion.Text = "Хувилбар " + Application.ProductVersion;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
