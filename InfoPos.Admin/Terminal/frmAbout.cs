using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ISM.Template;

namespace InfoPos.Admin
{
    public partial class frmAbout : Form
    {
        private readonly InfoPos.Core.Core _core;
        public frmAbout(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null) btnClose.Image = _core.Resource.GetImage("navigate_home");
        }
        private void frmOption_Activated(object sender, EventArgs e)
        {
        }
        private void frmAbout_Load(object sender, EventArgs e)
        {
           // if (_core.Resource != null)
             //   picCustomer.Image = _core.Resource.GetImage("HPro_Application");
        }

        private void frmAbout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
