using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
//using DevExpress.XtraBars.Ribbon;

using ISM.Touch;
using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.Order
{
    public partial class frmOrder : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        #region Internal variables

        private InfoPos.Core.Core _core;
        private ISM.Touch.TouchKeyboard _kb;
        private string ProdNo = "";

        private string _layoutfilename = "";

        #endregion

        #region Menu functions

        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _kb = new TouchKeyboard();
                _kb.Enable = true;



                this.MdiParent = parent;
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            Result res = new Result();
            try
            {
                switch (buttonkey)
                {

                    case "fo_customer_search":
                        break;
                    case "fo_order_ordernew":
                        break;
                    case "fo_order_personnew":
                        break;
                    case "fo_order_groupnew":
                        break;
                    case "fo_order_new_exit":
                        this.Close();
                        item.IsClose = 1;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        
        #endregion

        #region Constructors
        public frmOrder()
        {
            InitializeComponent();
        }
        #endregion
        #region Control events

        private void frmOrder_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
