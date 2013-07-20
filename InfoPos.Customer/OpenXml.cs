using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;

using ISM.Template;

namespace InfoPos.Customer
{
    public partial class OpenXml : Form
    {
        #region[variables]
        InfoPos.Core.Core _core;
        #endregion

        #region[Functions]
        public OpenXml(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
        }
        #endregion
        private void OpenXml_Load(object sender, EventArgs e)
        {

        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    txtFileName.EditValue = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
