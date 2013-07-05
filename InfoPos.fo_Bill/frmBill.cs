using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Touch;
using InfoPos.Core;
using ISM.Template;
using System.Drawing.Printing;

using System.IO.Ports;

namespace InfoPos.bill
{
    public partial class frmBill : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;

        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _kb = new TouchKeyboard();
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;

                ucSaleSearchToBill.Remote = _core.RemoteObject;
                ucSaleSearchToBill.Core = _core;
                ucSaleSearchToBill.Resource = _core.Resource;

                ucBill.remote = _core.RemoteObject;
                ucBill.kb = _kb;
                ucBill.core = _core;                
                               
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
                    case "fo_repeat_bill":
                        if (ucBill.SalesNo != null) {
                            if (_core.BillPrinterType == 1)
                            {
                                ucBill.PrindReport();
                            }
                            else
                            {
                                ucBill.PrindReportSerial();
                            }
                            }
                        else { MessageBox.Show("Борлуулалт сонгоогүй байна:"); return; }
                        break;
                    case "fo_cash_bill_exit":
                        item.IsClose = 1;
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
        public frmBill()
        {
            InitializeComponent();

            tabMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            this.ucSaleSearchToBill.EventChoose += new Panels.ucSaleSearch.delegateEventChoose(ucSaleSearchToBill_EventChoose);

        }

        #region[bill]
        void ucSaleSearchToBill_EventChoose(DataRow currentrow)
        {
            Result res = new Result();            
            try
            {
                tabMain.SelectedTabPageIndex = 1;

                if (currentrow == null)
                {
                    MessageBox.Show("Борлуулалт хийгдээгүй байна.");
                }
                else
                {
                    string BatchNo = Static.ToStr(currentrow["batchno"]);
                    ucBill.SalesNo = Static.ToStr(currentrow["salesno"]);

                    string lname = Static.ToStr(currentrow["lastname"]);
                    string fname = Static.ToStr(currentrow["firstname"]);
                    ucBill._billno = Static.ToStr(currentrow["PAYMENTNO"]); 

                    _core.MainForm_HeaderSet(0, null, fname);
                    _core.MainForm_HeaderSet(1, null, lname);
                    _core.MainForm_HeaderSet(2, null, Static.ToStr(currentrow["salesno"]));

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510017, 510017, new object[] { BatchNo });
                    if (ISM.Template.FormUtility.ValidateQuery(res))
                    {
                        ucBill._changeAmount = Static.ToDecimal(res.Data.Tables[0].Rows[0]["CHARGEAMOUNT"]);
                        ucBill.sumamount = Static.ToDecimal(res.Data.Tables[0].Rows[0]["TOTALAMOUNT"]);
                        ucBill.vat = Static.ToDecimal(res.Data.Tables[0].Rows[0]["VAT"]);
                        ucBill._paydamount = Static.ToDecimal(res.Data.Tables[0].Rows[0]["SALESAMOUNT"]) + Static.ToDecimal(res.Data.Tables[0].Rows[0]["CHARGEAMOUNT"]);
                    }            

                    ucBill.refresh(BatchNo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion[]

        private void frmBill_Load(object sender, EventArgs e)
        {
            ucSaleSearchToBill.DataRefresh(1);
        }
    }
}