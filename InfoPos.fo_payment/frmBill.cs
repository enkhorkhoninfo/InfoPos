using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Touch;
using InfoPos.Core;

namespace InfoPos.Payment
{
    public partial class frmBill : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;

        string _custno = null;
        string _fname = null;
        string _lname = null;
        string _batchno = null;
        string _salesno = null;
        string _serialno = null;
        string _paymentno = null;
        int _isvat = 0;

        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;

                this.ucSaleSearch1.Core = _core;
                this.ucSaleSearch1.Remote = _core.RemoteObject;
                this.ucSaleSearch1.Resource = _resource;

                ucBill1.remote = _core.RemoteObject;
                ucBill1.core = _core;

                this.ucPayment1.Core = _core;
                this.ucPayment1.Remote = _core.RemoteObject;
                this.ucPayment1.Resource = _resource;

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
                    case "fo_payment_search":
                        SubMenu_Search();
                        break;
                    case "fo_payment_tag":
                        SubMenu_SearchTag();
                        break;
                    case "fo_payment_set":
                        SubMenu_Payment(item);
                        break;
                    case "fo_payment_exit":
                        item.IsClose = 1;
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SubMenu_Search()
        {
            tabMain.SelectedTabPageIndex = 0;

            _fname = null;
            _lname = null;
            _custno = null;
            _batchno = null;
            _salesno = null;
            _serialno = null;

            _core.MainForm_HeaderSet(0, null, "");
            _core.MainForm_HeaderSet(1, null, "");
            _core.MainForm_HeaderSet(2, null, "");
            _core.MainForm_HeaderSet(3, null, "");
            _core.MainForm_HeaderSet(4, null, "");
            _core.MainForm_HeaderSet(5, null, "");
        }
        public void SubMenu_SearchTag()
        {
            try
            {
                InfoPos.Panels.frmTagReader frm = new InfoPos.Panels.frmTagReader(_core);
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    ucSaleSearch1.Find(null, frm.SerialNo, null, null, null, null, null);
                    SubMenu_Search();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void SubMenu_Payment(TouchLinkItem item)
        {
            Result res = null;
            if (tabMain.SelectedTabPageIndex != 1)
            {
                res = Msg.Get(EnumMessage.SALES_NOT_SELECTED);
            }
            else
            {
                _paymentno = "";
                res = ucPayment1.PaymentTable(_paymentno);
                if (res.ResultNo == 0)
                {
                    _core.AlertShow("Мэдээлэл", "Төлбөр амжилттай хийгдлээ.");
                    decimal[] amount = ucPayment1.GetAmount();
                    ucBill1.SalesPrint(_batchno, 0, _isvat, amount[4], amount[5], Static.ToStr(res.Param[2]), amount[2]);
                    // Төлбөр амжилттай хийгдвэл шууд формоо хаах
                    item.IsClose = 1;
                    this.Close();
                }
                else
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }    
        }
        //public void SubMenu_CreateCustomer()
        //{
        //    InfoPos.fo_Customer.frmCustomer frm = new fo_Customer.frmCustomer(_core, "", "", "", "", "", "");
        //    frm.ShowDialog();
        //}

        public frmBill()
        {
            InitializeComponent();
            tabMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            this.FormClosing += new FormClosingEventHandler(frmBill_FormClosing);
            this.ucSaleSearch1.EventChoose += new Panels.ucSaleSearch.delegateEventChoose(ucSaleSearch1_EventChoose);
            this.ucPayment1.EventChoose += new Panels.ucPayment.delegateEventChoose(ucPayment1_EventChoose);
            //this.ucSalesProductList1.EventProdChanged += new InfoPos.fo_panels.ucSalesProductList.delegateEventProdChanged(ucSalesProductList1_EventProdChanged); 
            //this.ucSalesCheckList1.EventOnRowChanged += new fo_panels.ucSalesCheckList.delegateEventOnRowChanged(ucSalesCheckList1_EventOnRowChanged);

            try
            {
                _kb = new TouchKeyboard();
                _kb.Enable = true; // chkTouchKeyboard.Checked;

                ucSaleSearch1.TouchKeyboard = _kb;
                ucPayment1.TouchKeyboard = _kb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void ucSalesCheckList1_EventOnRowChanged(string SalesNo)
        {
            //ucSalesProd1.SalesFilter(new string[] { SalesNo });
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            

            _core.MainForm_HeaderClear();
            _core.MainForm_HeaderSet(0, "Овог", "");
            _core.MainForm_HeaderSet(1, "Нэр", "");
            _core.MainForm_HeaderSet(2, "Харилцагч №", "");
            _core.MainForm_HeaderSet(3, "Багц №", "");
            _core.MainForm_HeaderSet(4, "Борлуулалт №", "");
            _core.MainForm_HeaderSet(5, "Таг №", "");

            //ucSalesProductList1.InitAll();
            //ucCalcSales1.ViewType = 1;
            //ucCalcSales1.SetViewForm(); 
            
        }
        private void frmBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucPayment1.SaveLayout();
            ucSaleSearch1.SaveLayout();
            //ucSalesProductList1.SaveLayout();
        }

        private void ucPayment1_EventChoose(DataRow currentrow)
        {
            
        }
        private void ucSaleSearch1_EventChoose(DataRow currentrow)
        {
            Result res = new Result();
            try
            {
                if (currentrow == null)
                {
                    res = Msg.Get(EnumMessage.SALES_NOT_SELECTED);
                }
                else
                {
                    tabMain.SelectedTabPageIndex = 1;

                    _batchno = ISM.Lib.Static.ToStr(currentrow["batchno"]);
                    _salesno = ISM.Lib.Static.ToStr(currentrow["salesno"]);
                    _custno = ISM.Lib.Static.ToStr(currentrow["custno"]);
                    _lname = ISM.Lib.Static.ToStr(currentrow["lastname"]);
                    _fname = ISM.Lib.Static.ToStr(currentrow["firstname"]);
                    _serialno = ISM.Lib.Static.ToStr(currentrow["serialno"]);
                    _isvat = Static.ToInt(currentrow["vat"]);
                    //_paymentno = Static.ToStr(currentrow["paymetnno"]);
                    _core.MainForm_HeaderSet(0, null, _fname);
                    _core.MainForm_HeaderSet(1, null, _lname);
                    _core.MainForm_HeaderSet(2, null, _custno);
                    _core.MainForm_HeaderSet(3, null, _batchno);
                    _core.MainForm_HeaderSet(4, null, _salesno);
                    _core.MainForm_HeaderSet(5, null, _serialno);

                    //res = ucSalesProd1.RefreshData(_batchno);
                    //if (!ISM.Template.FormUtility.ValidateQuery(res)) return;

                    //res = ucSalesCheckList1.RefreshData(_batchno);
                    //if (!ISM.Template.FormUtility.ValidateQuery(res)) return;

                    //res = ucPayment1.PaymentByCash(_batchno, "1", 262019720);
                    //if (!ISM.Template.FormUtility.ValidateQuery(res)) return;

                    res = ucPayment1.Find(_batchno);
                    if (ISM.Template.FormUtility.ValidateQuery(res)) return;

                    //{
                    //    tabMain.SelectedTabPageIndex = 1;

                    //    if (res.AffectedRows <= 0) ucPayment1.Clear();
                    //    else
                    //    {
                    //        ucPayment1.OnEventChoose();
                    //        ucPayment1.ShowKeyboard(0);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ucSaleSearch1_Load(object sender, EventArgs e)
        {

        }

    }
}