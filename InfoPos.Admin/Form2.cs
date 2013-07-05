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

namespace HeavenPro.Terminal
{
    public partial class frmSplash : Form
    {
        private readonly bool fromMenu;
 
        public frmSplash() : this(false)
        {
        }

        public frmSplash(bool FromMenu)
        {
            InitializeComponent();
            this.fromMenu = FromMenu;
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {

        }
        private void SplashScreen_Click(object sender, EventArgs e)
        {
            if (fromMenu)
            {
                this.Close();
            }
        }
    }
}
