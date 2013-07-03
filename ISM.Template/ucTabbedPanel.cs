using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace ISM.Template
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))] 
    public partial class ucTabbedPanel : UserControl
    {

        public DevExpress.XtraTab.XtraTabPageCollection TabPages
        {
            get { return xtraTabControl1.TabPages; }
        }
        public DevExpress.XtraTab.XtraTabPage SelectedPage
        {
            get { return xtraTabControl1.SelectedTabPage; }
        }

        public ucTabbedPanel()
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

    }
}
