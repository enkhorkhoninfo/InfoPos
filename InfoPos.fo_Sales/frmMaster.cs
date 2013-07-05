using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ISM.Touch;
using EServ.Shared;
namespace InfoPos.sales
{
    public partial class frmMaster : DevExpress.XtraEditors.XtraForm,ISM.Touch.ITouchCall
    {
        InfoPos.Core.Core _core = null;
        ISM.Touch.TouchKeyboard _kb = null;
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            _core = (InfoPos.Core.Core)param;
            _kb = new TouchKeyboard();
            _kb.Enable = true;

            ucMaster1.Core = _core;
            ucMaster1.Remote = _core.RemoteObject;
            ucMaster1.Resource = _core.Resource;
            ucMaster1.TouchKeyboard = _kb;

            TabMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.MdiParent = parent;
            this.Show();
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            Result res = new Result();
            try
            {
                switch (buttonkey)
                {
                    case "master_search":
                        TabMain.SelectedTabPageIndex = 0;
                        ucMaster1.DataRefresh();
                        break;
                    case "master_active":
                        ucMaster1.btnActive_Click(null, null);
                        break;
                    case "master_inactive":
                        ucMaster1.btnInActive_Click(null, null);
                        break;
                    case "master_exit":
                        this.Close();
                        break;
                }
                    ISM.Template.FormUtility.ValidateQuery(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public frmMaster()
        {
            InitializeComponent();
        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            ucMaster1.DataRefresh();
            //ucMaster1
        }

        private void ucMaster1_Load(object sender, EventArgs e)
        {

        }
    }
}