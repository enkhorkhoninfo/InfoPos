using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace ISM.Template
{
    public partial class frmTempTabbed : Form
    {
        #region Constractor

        public frmTempTabbed()
        {
            InitializeComponent();

            xtraTabControl1.Deselecting += new DevExpress.XtraTab.TabPageCancelEventHandler(xtraTabControl1_Deselecting);
        }

        void xtraTabControl1_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            Control control = FormUtility.FindControl(e.Page.Controls, typeof(ucTogglePanel));
            if (control != null)
            {
                ucTogglePanel uc = (ucTogglePanel)control;
                if (uc.ToggleFlag != 0) e.Cancel = true;
            }
        }
        #endregion
    }
}
