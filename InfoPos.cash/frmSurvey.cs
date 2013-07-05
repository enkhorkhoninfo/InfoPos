using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;
using ISM.Touch;

namespace InfoPos.cash
{
    public partial class frmSurvey : Form, ISM.Touch.ITouchCall
    {
        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
           try
            {
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;

                //this.ucSaleSearch1.Remote = _core.RemoteObject;
                //this.ucSaleSearch1.Resource = _resource;
                //this.ucCustSearch1.Remote = _core.RemoteObject;
                //this.ucCustSearch1.Resource = _resource;
                
                //this.ucRentTag1.Core = _core;

                //this.ucRentList1.Remote = _core.RemoteObject;
                //this.ucRentList1.Resource = _resource;

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
                    case "fo_rent_search":
                        tabMain.SelectedTabPageIndex = 0;
                        break;
                    case "fo_rent_tag":
                        ShowTagReader();
                        break;
                    case "fo_rent_deliver":
                        if (tabMain.SelectedTabPageIndex != 1)
                        {
                            //res = Msg.Get(EnumMessage.PRODUCT_NOT_SELECTED);
                        }
                        else
                        {
                            //res = ucRentList1.Deliver();
                        }

                        break;
                    case "fo_rent_receive":
                        if (tabMain.SelectedTabPageIndex != 1)
                        {
                            //res = Msg.Get(EnumMessage.PRODUCT_NOT_SELECTED);
                        }
                        else
                        {
                            //res = ucCustSearch1.Receive();
                        }
                        break;
                }
                ISM.Template.FormUtility.ValidateQuery(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        public void ShowTagReader()
        {
            try
            {
                InfoPos.Panels.frmRentTagReader frm = new InfoPos.Panels.frmRentTagReader(_core);
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    switch (tabMain.SelectedTabPageIndex)
                    {
                        case 0: // Sales search page
                            //ucCustSearch1.Find(null, frm.SerialNo, frm.CustNo, null, null, null, null);
                            break;
                        case 1:
                            //ucRentTag1.Find(null, frm.SerialNo, frm.CustNo);
                           // ucRentTag1.OnEventChoose();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public frmSurvey()
        {
            InitializeComponent();
            //tabMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;

            //this.ucCustSearch1.EventChoose += new Panels.ucCustSearch.delegateEventChoose(ucCustSearch1_EventChoose);            

            try
            {
                _kb = new TouchKeyboard();
                ////_kb.AddToKeyboard(calcEdit1);
                ////_kb.AddToKeyboard(dateEdit1);
                ////_kb.AddToKeyboard(textEdit1);
                //_kb.Enable = chkTouchKeyboard.Checked;

                //ucCustSearch1.TouchKeyboard = _kb;
                //ucCustSearch1.PageRows = 2;

                //ucCustSearch1.PageRows = 20;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void ucCustSearch1_EventChoose(DataRow currentrow)
        {
            throw new NotImplementedException();
        }
    }
}