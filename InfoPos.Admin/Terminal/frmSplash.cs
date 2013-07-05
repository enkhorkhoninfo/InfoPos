using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Win32;

namespace InfoPos.Admin
{
    public partial class frmSplash : Form
    {
        public frmSplash() : this(false)
        {
        }

        internal void SetStatus(string statusText)
        {
            txtStatus.Text = statusText;
            txtStatus.Refresh();
        }

        public frmSplash(bool FromMenu)
        {
            InitializeComponent();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            SetStatus("Loading ...");
            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
