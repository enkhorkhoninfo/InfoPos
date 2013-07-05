using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using ISM.Touch;
using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmSaleNew : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        #region [ Variables ]
        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;
        Hashtable productpanel = null;
        Image imgFine;
        Image imgRead;
        DataTable TagList = null;

        /// <summary>
        /// 0 - Таг цэнэглэх
        /// 1 - Таг цэвэрлэх
        /// 2 - таг унших
        /// </summary>
        int tagstatus = 0;

        bool isreturn = false;

        string batchno = "";
        string salesno = "";
        string paymentno = "";
        string contracttype = "";
        string contractno = "";
        string orderno = "";

        long customerno = 0;
        long OldCustNo = 0;
        

        long rebateid = 0;
        decimal point = 0;
        long loyalid = 0;
        long pointid = 0;
        #endregion
        #region[ Constracture ]
        public frmSaleNew()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " : " + ex.StackTrace);
            }
        }
        private void FormLoad(object param, int func)
        {
            try
            {
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                pnlSales.Visible = false;
                #region[Choose]
                ucContractSearch1.EventChoose += new InfoPos.Panels.ucContractSearch.delegateEventChoose(ucContractSearch1_EventChoose);
                ucSalesDetailSearch1.EventChoose += new InfoPos.Panels.ucSalesDetailSearch.delegateEventChoose(ucSalesDetailSearch1_EventChoose);
                ucOrderSearch1.EventChoose += new InfoPos.Order.ucOrderSearch.delegateEventChoose(ucOrderSearch1_EventChoose);
                ucCustSearch1.EventChoose += new InfoPos.Panels.ucCustSearch.delegateEventChoose(ucCustSearch1_EventChoose);
                ucProductList1.EventChoose += new InfoPos.fo_panels.ucProductList.delegateEventChoose(ucProductList1_EventChoose);
                ucSaleCustomer1.EventOnBtnEdit += new InfoPos.Panels.ucSaleCustomer.delegateEventOnBtnEdit(ucSaleCustomer1_EventOnBtnEdit);
                ucSaleCustomer1.EventOnBtnDelete += new InfoPos.Panels.ucSaleCustomer.delegateEventOnBtnDelete(ucSaleCustomer1_EventOnBtnDelete);
                #endregion
                #region[Product]
                ucSalesProd1.ColumnVisible(0, false);
                ucSalesProd1.ColumnVisible(1, false);
                ucSalesProd1.ColumnVisible(2, false);
                ucSalesProd1.ColumnVisible(3, false);
                ucSalesProd1.ColumnVisible(9, false);
                #endregion
                #region[Payment]
                ucCalcSales1.EventOnDiscount += new InfoPos.Panels.ucCalcSales.delegateEventOnOtherPayment(ucCalcSales1_EventOnDiscount);
                ucCalcSales1.EventOnPayment += new InfoPos.Panels.ucCalcSales.delegateEventOnPayment(ucCalcSales1_EventOnPayment);
                ucCalcSales1.EventOnCashPayment += new InfoPos.Panels.ucCalcSales.delegateEventOnCashPayment(ucCalcSales1_EventOnCashPayment);
                ucCalcSales1.EventOnCardPayment += new InfoPos.Panels.ucCalcSales.delegateEventOnCardPayment(ucCalcSales1_EventOnCardPayment);
                ucCalcSales1.EventOnOtherPayment += new InfoPos.Panels.ucCalcSales.delegateEventOnOtherPayment(ucCalcSales1_EventOnOtherPayment);
                #endregion
                InitPackProduct();
                gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
                touchButtonGroup1.EventKeyDown += new TouchButtonGroup.delegateKeyDown(touchButtonGroup1_EventKeyDown);
                touchtagsign.EventKeyDown += new TouchButtonGroup.delegateKeyDown(touchtagsign_EventKeyDown);
                ucSaleCustomer1.EventOnItemClick += new InfoPos.Panels.ucSaleCustomer.delegateEventOnItemClick(ucSaleCustomer1_EventOnItemClick);
                touchButtonGroup1.ParentMDI = this;
                InitTouchButton();
                InitTouchTag();
                touchButtonGroup1.ButtonsDrawChildren("ROOT");
                #region
                #endregion
                switch (func)
                {
                    case 0:
                        xtraTabControl1.SelectedTabPageIndex = 0;
                        break;
                    case 1:
                        xtraTabControl1.SelectedTabPageIndex = 4;
                        ucSalesDetailSearch1.DataRefresh(1);
                        isreturn = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " : " + ex.StackTrace);
            }
        }

        void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.IsGetData)
                {
                    switch (e.Column.FieldName)
                    {
                        case "PIC":
                            DataRow row = gridView1.GetDataRow(e.RowHandle);
                            int rentstatus = Static.ToInt(row["status"]);
                            switch (rentstatus)
                            {
                                case 0: e.Value = imgFine; break;
                                case 1: e.Value = imgRead; break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void touchtagsign_EventKeyDown(Control sender, MouseEventArgs e, TouchLinkItem item, ref bool cancel)
        {
            switch(item.Key)
            {
                case "tagwrite": labelControl7.Text = "Таг цэнэглэх"; tagstatus = 0; break;
                case "tagclean": labelControl7.Text = "Таг цэвэрлэх"; tagstatus = 1; break;
                case "tagread": labelControl7.Text = "Тагаас мэдээлэл унших"; tagstatus = 2; break;
            }
        }
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;
                _kb = new ISM.Touch.TouchKeyboard();
                _kb.Enable = true;

                this.ucCalcSales1.Remote = _core.RemoteObject;

                this.ucCustSearch1.Remote = _core.RemoteObject;
                this.ucCustSearch1.Core = _core;
                this.ucCustSearch1.Resource = _resource;

                this.ucSalesDetailSearch1.Remote = _core.RemoteObject;
                this.ucSalesDetailSearch1.Core = _core;
                this.ucSalesDetailSearch1.Resource = _resource;
                this.ucSalesDetailSearch1.TouchKeyboard = _kb;

                this.ucProductList1.Remote = _core.RemoteObject;
                this.ucProductList1.Core = _core;
                this.ucProductList1.Resource = _resource;

                this.ucContractSearch1.Remote = _core.RemoteObject;
                this.ucContractSearch1.Core = _core;
                this.ucContractSearch1.Resource = _core.Resource;
                this.ucContractSearch1.TouchKeyboard = _kb;

                this.ucOrderSearch1.Remote = _core.RemoteObject;
                this.ucOrderSearch1.Core = _core;
                this.ucOrderSearch1.Resource = _core.Resource;
                this.ucOrderSearch1.TouchKeyboard = _kb;

                this.ucSaleCustomer1.Resource = _core.Resource;
                this.ucSaleCustomer1.Core = _core;
                this.ucSaleCustomer1.Remote = _core.RemoteObject;

                this.ucSalesProd1.Remote = _core.RemoteObject;
                this.ucSalesProd1.Resource = _core.Resource;
                this.ucSalesProd1.TouchKeyboard = _kb;

                ucPayment1.Remote = _core.RemoteObject;
                ucPayment1.Resource = _core.Resource;
                ucPayment1.Core = _core;
                ucPayment1.TouchKeyboard = _kb;

                ucCalcSales1.core = _core;
                ucCalcSales1.TouchKeyboard = _kb;
                
                ucBill1.remote = _core.RemoteObject;
                ucBill1.core = _core;

                if (_core.Resource != null)
                {
                    imgFine = _core.Resource.GetImage("tag_purple_unread");
                    imgRead = _core.Resource.GetImage("tag_purple_read");
                }
                this.MdiParent = _core.MainForm;
                this.Show();
                if (item.Key == "fo_cash_new")
                {
                    FormLoad(param, 0);
                }
                if (item.Key == "fo_salesreturn")
                {
                    FormLoad(param, 1);
                }
                xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            try
            {
                switch (buttonkey)
                {
                    case "fo_cash_new_1.1":
                        xtraTabControl1.SelectedTabPageIndex = 0;
                        break;
                    case "fo_cash_new_2.1":
                        xtraTabControl1.SelectedTabPageIndex = 1;
                        break;
                    case "fo_cash_new_1.2":
                        xtraTabControl1.SelectedTabPageIndex = 2;
                        break;
                    case "fo_cash_new_2.2":
                        xtraTabControl1.SelectedTabPageIndex = 3;
                        ucCalcSales1.DiscountState();
                        break;
                    case "fo_cash_new_1.3":
                        if (xtraTabControl1.SelectedTabPageIndex == 5)
                        {
                            Result res = ucPayment1.PaymentTable(paymentno);
                            if (res.ResultNo == 0)
                            {
                                _core.AlertShow("Мэдээлэл", "Төлбөр амжилттай хийгдлээ.");
                                Tag();
                                ucBill1.SalesPrint(batchno, point, ucCalcSales1.IsVat, Static.ToDecimal(res.Param[0]), Static.ToDecimal(res.Param[1]), Static.ToStr(res.Param[2]), 0);
                                item.IsClose = 1;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(res.ResultDesc);
                            }
                        }
                        else
                        {
                            _core.AlertShow("Мэдээлэл", "Төлбөр хийх цонхноос дахин оролдоно уу.");
                        }
                        break;
                    case "fo_cash_new_2.8":
                        item.IsClose = 1;
                        this.Close();
                        break;
                    case "fo_salesreturn_1.1":
                        xtraTabControl1.SelectedTabPageIndex = 0;
                        break;
                    case "fo_salesreturn_2.1":
                        xtraTabControl1.SelectedTabPageIndex = 1;
                        break;
                    case "fo_salesreturn_1.2":
                        xtraTabControl1.SelectedTabPageIndex = 2;
                        break;
                    case "fo_salesreturn_2.2":
                        xtraTabControl1.SelectedTabPageIndex = 3;
                        break;
                    case "fo_salesreturn_1.3":
                        if (xtraTabControl1.SelectedTabPageIndex == 5)
                        {
                            Result res = ucPayment1.PaymentTable(paymentno);
                            if (res.ResultNo == 0)
                            {
                                _core.AlertShow("Мэдээлэл", "Төлбөр амжилттай хийгдлээ.");
                                ucBill1.SalesPrint(batchno, point, ucCalcSales1.IsVat, Static.ToDecimal(res.Param[0]), Static.ToDecimal(res.Param[1]), Static.ToStr(res.Param[2]), 0);
                                item.IsClose = 1;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(res.ResultDesc);
                            }
                        }
                        else
                        {
                            _core.AlertShow("Мэдээлэл", "Төлбөр хийх цонхноос дахин оролдоно уу.");
                        }
                        break;
                    case "fo_salesreturn_2.8":
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
        #endregion
        #region[ Choose ]
        void ucContractSearch1_EventChoose(DataRow currentrow)
        {
            if (currentrow != null)
            {
                pnlSales.Visible = true;
                Result res = new Result();
                try
                {
                    contractno = Static.ToStr(currentrow["CONTRACTNO"]);
                    rebateid = Static.ToLong(currentrow["rebateid"]);
                    loyalid = Static.ToLong(currentrow["loyalid"]);
                    pointid = Static.ToLong(currentrow["pointid"]);
                    ucCalcSales1.IsVat = Static.ToInt(currentrow["vat"]);
                    contracttype = Static.ToStr(currentrow["contracttype"]);
                    _core.MainForm_HeaderClear();
                    _core.MainForm_HeaderSet(0, "Гэрээний №", contractno);
                    _core.MainForm_HeaderSet(1, "Харилцагчийн №", Static.ToStr(currentrow["CUSTNO"]));
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130006, 130006, new object[] { Static.ToStr(currentrow["CONTRACTNO"]) });
                    if (res.ResultNo == 0)
                    {
                        if (res.Data != null)
                        {
                            ucSaleCustomer1.AddRow("", Static.ToStr(currentrow["CUSTNO"]), 0, 0, "", Static.ToStr(currentrow["LASTNAME"]));
                            xtraTabControl1.SelectedTabPageIndex = 2;
                            customerno = Static.ToLong(currentrow["CUSTNO"]);
                            if (res.Data.Tables[0].Rows.Count != 0)
                            {
                                DataTable productlist = new DataTable();
                                productlist.Columns.Add("SALESNO", typeof(string));
                                productlist.Columns.Add("CUSTOMERNO", typeof(long));
                                productlist.Columns.Add("PRODCODE", typeof(string));
                                productlist.Columns.Add("PRODTYPE", typeof(int));
                                productlist.Columns.Add("PRODNAME", typeof(string));
                                productlist.Columns.Add("PRICE", typeof(decimal));
                                productlist.Columns.Add("DISCOUNT", typeof(decimal));
                                productlist.Columns.Add("SALESAMOUNT", typeof(decimal));
                                productlist.Columns.Add("QUANTITY", typeof(long));
                                productlist.Columns.Add("ISVAT", typeof(int));
                                productlist.Columns.Add("EDIT");
                                productlist.Columns.Add("DELETE");
                                foreach (DataRow dr in res.Data.Tables[0].Rows)
                                {
                                    if (Static.ToInt(dr["PRODTYPE"]) == 0)
                                    {
                                        productlist = GetInv(Static.ToStr(dr["PRODNO"]), productlist, 1);
                                    }
                                    if (Static.ToInt(dr["PRODTYPE"]) == 1)
                                    {
                                        productlist = GetServ(Static.ToStr(dr["PRODNO"]), productlist, 1);
                                    }
                                    foreach (DataRow drow in productlist.Rows)
                                    {
                                        ucSalesProd1.AddProduct(salesno,
                                                                Static.ToLong(drow["CUSTOMERNO"]),
                                                                Static.ToStr(drow["PRODCODE"]),
                                                                Static.ToInt(drow["PRODTYPE"]),
                                                                Static.ToStr(drow["PRODNAME"]),
                                                                Static.ToDecimal(drow["PRICE"]),
                                                                Static.ToDecimal(drow["DISCOUNT"]),
                                                                Static.ToDecimal(drow["SALESAMOUNT"]),
                                                                Static.ToInt(drow["QUANTITY"]),
                                                                0,
                                                                0
                                                                );
                                    }
                                    ucCalcSales1.prodlist = ucSalesProd1.productlist;
                                    ucCalcSales1.SetAmount(productlist, customerno);
                                }
                            }
                        }
                        else
                        {
                            _core.AlertShow("Анхааруулга", "Гэрээн дээр бүтээгдэхүүн байхгүй байна.");
                        }
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucOrderSearch1_EventChoose(DataRow currentrow)
        {
            if (currentrow != null)
            {
                pnlSales.Visible = true;
                Result res = new Result();
                try
                {
                    orderno = Static.ToStr(currentrow["ORDERNO"]);
                    rebateid = Static.ToLong(currentrow["rebateid"]);
                    loyalid = Static.ToLong(currentrow["loyalid"]);
                    pointid = Static.ToLong(currentrow["pointid"]);
                    _core.MainForm_HeaderClear();
                    _core.MainForm_HeaderSet(0, "Захиалгын №", contractno);
                    _core.MainForm_HeaderSet(1, "Харилцагчийн №", Static.ToStr(currentrow["CUSTNO"]));
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 90000, 90000, new object[] { Static.ToStr(currentrow["ORDERNO"]) });
                    if (res.ResultNo == 0)
                    {
                        if (res.Data != null)
                        {
                            if (res.Data.Tables[0].Rows.Count != 0)
                            {
                                xtraTabControl1.SelectedTabPageIndex = 2;
                                customerno = Static.ToLong(currentrow["CUSTNO"]);
                                ucSaleCustomer1.AddRow("", Static.ToStr(currentrow["CUSTNO"]), 0, 0, "", Static.ToStr(currentrow["LASTNAME"]));
                                DataTable productlist = new DataTable();
                                productlist.Columns.Add("SALESNO", typeof(string));
                                productlist.Columns.Add("CUSTOMERNO", typeof(long));
                                productlist.Columns.Add("PRODCODE", typeof(string));
                                productlist.Columns.Add("PRODTYPE", typeof(int));
                                productlist.Columns.Add("PRODNAME", typeof(string));
                                productlist.Columns.Add("PRICE", typeof(decimal));
                                productlist.Columns.Add("DISCOUNT", typeof(decimal));
                                productlist.Columns.Add("SALESAMOUNT", typeof(decimal));
                                productlist.Columns.Add("QUANTITY", typeof(long));
                                productlist.Columns.Add("FLAG", typeof(long));
                                productlist.Columns.Add("EDIT");
                                productlist.Columns.Add("DELETE");
                                foreach (DataRow dr in res.Data.Tables[0].Rows)
                                {
                                    if (Static.ToInt(dr["PRODTYPE"]) == 0)
                                    {
                                        productlist = GetInv(Static.ToStr(dr["PRODNO"]), productlist, Static.ToInt(dr["QTY"]));
                                    }
                                    if (Static.ToInt(dr["PRODTYPE"]) == 1)
                                    {
                                        productlist = GetServ(Static.ToStr(dr["PRODNO"]), productlist, Static.ToInt(dr["QTY"]));
                                    }
                                }
                                foreach (DataRow drow in productlist.Rows)
                                {
                                    ucSalesProd1.AddProduct(salesno,
                                                            Static.ToLong(drow["CUSTOMERNO"]),
                                                            Static.ToStr(drow["PRODCODE"]),
                                                            Static.ToInt(drow["PRODTYPE"]),
                                                            Static.ToStr(drow["PRODNAME"]),
                                                            Static.ToDecimal(drow["PRICE"]),
                                                            Static.ToDecimal(drow["DISCOUNT"]),
                                                            Static.ToDecimal(drow["SALESAMOUNT"]),
                                                            Static.ToInt(drow["QUANTITY"]),
                                                            Static.ToInt(drow["FLAG"]),
                                                            0
                                                            );
                                }
                                ucCalcSales1.prodlist = ucSalesProd1.productlist;
                                ucCalcSales1.SetAmount(productlist, customerno);
                            }
                        }
                        else
                        {
                            _core.AlertShow("Мэдээлэл","Захиалга дээр бүтээгдэхүүн байхгүй байна.");
                        }
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucProductList1_EventChoose(DataTable currentdata)
        {
            if (currentdata != null)
            {
                DataTable productlist = new DataTable();
                productlist.Columns.Add("SALESNO", typeof(string));
                productlist.Columns.Add("CUSTOMERNO", typeof(long));
                productlist.Columns.Add("PRODCODE", typeof(string));
                productlist.Columns.Add("PRODTYPE", typeof(int));
                productlist.Columns.Add("PRODNAME", typeof(string));
                productlist.Columns.Add("PRICE", typeof(decimal));
                productlist.Columns.Add("DISCOUNT", typeof(decimal));
                productlist.Columns.Add("SALESAMOUNT", typeof(decimal));
                productlist.Columns.Add("QUANTITY", typeof(long));
                productlist.Columns.Add("FLAG", typeof(int));
                productlist.Columns.Add("EDIT");
                productlist.Columns.Add("DELETE");

                foreach (DataRow currentrow in currentdata.Rows)
                {
                    switch (Static.ToInt(currentrow["prodtype"]))
                    {
                        case 0:
                            productlist = GetInv(Static.ToStr(currentrow["prodid"]), productlist, 1);
                            break;
                        case 1:
                            productlist = GetServ(Static.ToStr(currentrow["prodid"]), productlist, 1);
                            break;
                    }
                }
                foreach (DataRow dr in productlist.Rows)
                {
                        ucSalesProd1.AddProduct("",
                                                Static.ToLong(dr["CUSTOMERNO"]),
                                                Static.ToStr(dr["PRODCODE"]),
                                                Static.ToInt(dr["PRODTYPE"]),
                                                Static.ToStr(dr["PRODNAME"]),
                                                Static.ToDecimal(dr["PRICE"]),
                                                Static.ToDecimal(dr["DISCOUNT"]),
                                                Static.ToDecimal(dr["SALESAMOUNT"]),
                                                Static.ToInt(dr["QUANTITY"]),
                                                Static.ToInt(dr["FLAG"]),
                                                0);
                }
                ucCalcSales1.prodlist = ucSalesProd1.productlist;
                ucCalcSales1.SetAmount(ucSalesProd1.productlist, customerno);
            }
        }
        void ucCustSearch1_EventChoose(DataRow currentrow)
        {
            if (currentrow != null)
            {
                pnlSales.Visible = true;
                try
                {
                    if (currentrow != null)
                    {
                        Result res = null;
                        customerno = Static.ToLong(currentrow["CUSTOMERNO"]);
                        int sex = Static.ToInt(currentrow["SEX"]);
                        int classcode = Static.ToInt(currentrow["CLASSCODE"]);
                        string custname = Static.ToStr(currentrow["LASTNAME"]);
                        if (OldCustNo != 0)
                        {
                            res = ucSaleCustomer1.EditRow(customerno, custname, sex, classcode);
                            if (ISM.Template.FormUtility.ValidateQuery(res))
                            {
                                ucSalesProd1.UpdateCustomer(OldCustNo, customerno);
                            }
                            else return;
                            OldCustNo = 0;
                        }
                        else
                        {
                            res = ucSaleCustomer1.AddRow("", Static.ToStr(currentrow["CUSTOMERNO"]), classcode, sex, Static.ToStr(currentrow["REGISTERNO"]), custname);
                            if (ISM.Template.FormUtility.ValidateQuery(res))
                            {
                                ucSaleCustomer1.SelectItem(customerno);
                            }
                            else return;
                        }
                        xtraTabControl1.SelectedTabPageIndex = 2;
                        pnlSales.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucSalesDetailSearch1_EventChoose(DataRow currentrow)
        {
            if (currentrow != null)
            {
                salesno = Static.ToStr(currentrow["SALESNO"]);
                batchno = Static.ToStr(currentrow["BATCHNO"]);
                paymentno = Static.ToStr(currentrow["PAYMENTNO"]);

                _core.MainForm_HeaderClear();
                _core.MainForm_HeaderSet(0, "Багцын №", batchno);
                _core.MainForm_HeaderSet(1, "Харилцагчийн №", Static.ToStr(currentrow["CUSTNO"]));

                Result res = ucSalesProd1.RefreshData(batchno);
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    res = ucSaleCustomer1.RefreshData(batchno);
                    if (ISM.Template.FormUtility.ValidateQuery(res)) ;
                    {
                        customerno = Static.ToLong(currentrow["CUSTNO"]);
                        ucSaleCustomer1.SelectItem(Static.ToLong(currentrow["CUSTNO"]));
                        ucCalcSales1.prodlist = ucSalesProd1.productlist;
                        xtraTabControl1.SelectedTabPageIndex = 2;
                        pnlSales.Visible = true;
                    }
                }
            }
        }     

        //Төлбөрийн хэсэг
        void ucCalcSales1_EventOnPayment()
        {
            try
            {
                object[] amount = ucCalcSales1.GetAmount();
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 600013, 600013
                    , new object[] { 
                        ucSalesProd1.productlist
                        , ucCalcSales1.IsVat
                        , contractno
                        , Static.ToDecimal(amount[3])
                        , _core.POSNo
                        , _core.RemoteObject.User.AreaCode
                        , batchno });
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    TagList = res.Data.Tables[0];
                    batchno = Static.ToStr(res.Param[0]);
                    res = ucPayment1.PaymentRegister(contractno, Static.ToDecimal(amount[2]), batchno, contracttype, paymentno);
                    if (ISM.Template.FormUtility.ValidateQuery(res))
                    {
                        Tag();
                        if (paymentno == "")
                        {
                            ucBill1.SalesPrint(batchno, point, ucCalcSales1.IsVat, Static.ToDecimal(amount[2]), 0, Static.ToStr(res.Param[0]), 0);
                        }
                        else
                        {
                            ucBill1.ReturnPrint(batchno, DateTime.Now, Static.ToStr(res.Param[0]),ucCalcSales1.IsVat);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucCalcSales1_EventOnOtherPayment()
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 600013, 600013, new object[] { ucSalesProd1.productlist, ucCalcSales1.IsVat, contractno, _core.POSNo, _core.RemoteObject.User.AreaCode, batchno });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                if ( res.ResultNo != 0)
                {
                    MessageBox.Show(res.ResultDesc);
                }
                else
                {
                    TagList = res.Data.Tables[0];
                    batchno = Static.ToStr(res.Param[0]);
                    xtraTabControl1.SelectedTabPageIndex = 5;
                    ucPayment1.BatchNo = batchno;
                    pnlSales.Visible = false;
                    res = ucPayment1.DataRefresh();
                }
            }
        }
        void ucCalcSales1_EventOnCardPayment()
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 600013, 600013, new object[] { ucSalesProd1.productlist, ucCalcSales1.IsVat, contractno, _core.POSNo, _core.RemoteObject.User.AreaCode,batchno });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                batchno = Static.ToStr(res.Param[0]);
                object[] amount = ucCalcSales1.GetAmount();
                if (amount != null)
                {
                    if (amount.Count() != 0)
                    {
                        res = ucPayment1.PaymentByIPPOS(batchno, _core.CardPayment, Static.ToDecimal(amount[2]));
                        if (ISM.Template.FormUtility.ValidateQuery(res))
                        {
                            Tag();
                            ucBill1.SalesPrint(batchno, point, ucCalcSales1.IsVat, Static.ToDecimal(amount[3]), Static.ToDecimal(amount[4]), Static.ToStr(res.Param[0]), 0);
                        }
                    }
                }
            }
        }
        void DataColumnsRefresh()
        {           
            #region Add picture column

            gridControl1.RepositoryItems.Clear();
            RepositoryItemPictureEdit ri = new RepositoryItemPictureEdit();
            gridControl1.RepositoryItems.Add(ri);
            DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
            col.VisibleIndex = gridView1.Columns.Count;
            col.Caption = "Үйлдэл";
            col.FieldName = string.Format("PIC");
            col.ColumnEdit = ri;
            col.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            col.OptionsColumn.ReadOnly = true;
            col.Width = 32;

            gridView1.Columns.Add(col);

            #endregion

            gridView1.Columns[0].Caption = "Борлуулалтын №";
            gridView1.Columns[1].Caption = "Харилцагчийн №";
            gridView1.Columns[2].Caption = "Үйлчилгээний хугацаа";
            gridView1.Columns[3].Caption = "Эхлэх огноо";
            gridView1.Columns[4].Caption = "Харилцагчийн нэр";
            gridView1.Columns[5].Caption = "Тагын дугаар";
            gridView1.Columns[6].Caption = "Төлөв";

            gridView1.RowHeight = 30;


            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;

            gridView1.Columns[1].Visible = false;
            gridView1.Columns[6].Visible = false;

            gridView1.Columns[4].VisibleIndex = 1;
        }    
        void ucCalcSales1_EventOnCashPayment()
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 600013, 600013
                , new object[] { ucSalesProd1.productlist, ucCalcSales1.IsVat, contractno, _core.POSNo, _core.RemoteObject.User.AreaCode, batchno });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                batchno = Static.ToStr(res.Param[0]);
                TagList = res.Data.Tables[0];
                object[] amount = ucCalcSales1.GetAmount();
                if (amount != null)
                {
                    if (amount.Count() != 0)
                    {
                        res = ucPayment1.PaymentByCash(batchno, _core.CashPayment, Static.ToDecimal(amount[2]), Static.ToDecimal(amount[4]), paymentno);
                        if (ISM.Template.FormUtility.ValidateQuery(res))
                        {
                            Tag();
                            if (paymentno == "")
                                ucBill1.SalesPrint(batchno, point, ucCalcSales1.IsVat, Static.ToDecimal(amount[3]), Static.ToDecimal(amount[4]), Static.ToStr(res.Param[0]), 0);
                            else
                            {
                                ucBill1.ReturnPrint(batchno, DateTime.Now, Static.ToStr(res.Param[0]), ucCalcSales1.IsVat);
                            }
                            if (TagList == null)
                            {
                                this.Close();
                            }
                            if (TagList.Rows.Count == 0)
                                this.Close();
                        }
                    }
                }
            }
        }
        void Tag()
        {
            if (_core.TagWrite == 0)
            {
                if (_core.Tag.tagreader != null)
                {
                    if (TagList != null)
                    {
                        if (TagList.Rows.Count != 0)
                        {
                            xtraTabControl1.SelectedTabPageIndex = 10;
                            pnlSales.Visible = false;
                            tagstatus = 0;
                            labelControl7.Text = "Таг цэнэглэх";
                            gridControl1.DataSource = TagList;
                            DataColumnsRefresh();
                            _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                        }
                    }
                }
                else
                {
                    _core.AlertShow("Мэдээлэл", "Таг уншигч холбогдоогүй байна.");
                }
            }
        }
        void tagreader_OnCardRead(object sender, Sit.onCardEventArgs e)
        {
            string cardid = e.cardID;
            txtSerialNo.EditValue = cardid;
            _core.Tag.tagreader.OnCardRead -= new Sit.Reader.CardEventHandler(tagreader_OnCardRead);

            if (!cardid.Equals("") && !cardid.Equals("47FF"))
            {
                DataTable table=(DataTable)gridControl1.DataSource;

                var query = table.AsEnumerable().Where(x => Static.ToStr(x["SERIALNO"]) == cardid).Select(x => x);
                if (query != null)
                {
                    if (query.Count() != 0)
                    {
                        DataRow dr = query.Single();
                        #region[ Тан цэнэглэх ]
                        if (tagstatus == 0)
                        {
                            DateTime startdate = DateTime.Now;
                            DateTime enddate = DateTime.Now.AddHours(Static.ToDouble(dr["tagtime"]));
                            if (_core.Tag.Reader_WriteData(cardid, startdate, enddate))
                            {
                                dr["status"] = 1;
                                MessageBox.Show("Таг амжилттай цэнэглэгдлээ.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                var confirm = table.AsEnumerable().Where(x => Static.ToInt(x["STATUS"]) == 0).Select(x => x);
                                if (confirm != null)
                                {
                                    if (confirm.Count() == 0)
                                    {
                                        this.Close();
                                    }
                                    else
                                    {
                                        _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                                        _core.Tag.tagreader.lastSelectedCardID = "";
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Таг цэнэглэхэд алдаа гарлаа. Дахин оролдоно уу.");
                                _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                                _core.Tag.tagreader.lastSelectedCardID = "";
                            }
                        }
                        #endregion
                        #region[ Таг цэвэрлэх ]
                        if (tagstatus == 1)
                        {
                            if (_core.Tag.Reader_ClearData(cardid))
                            {
                                dr["status"] = 0;
                                MessageBox.Show("Таг амжилттай цэвэрлэгдлээ.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                                _core.Tag.tagreader.lastSelectedCardID = "";
                            }
                            else
                            {
                                MessageBox.Show("Таг цэвэрлэхэд алдаа гарлаа.");
                                _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                                _core.Tag.tagreader.lastSelectedCardID = "";
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        MessageBox.Show(string.Format("[{0}] дугаартай таг тухайн борлуулалтад алга байна.", cardid));
                        _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                        _core.Tag.tagreader.lastSelectedCardID = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Тагаа дахин уншуулна уу.");
                _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                _core.Tag.tagreader.lastSelectedCardID = "";
            }
        }
        void ucCalcSales1_EventOnDiscount()
        {
            try
            {
                if (ucSalesProd1.productlist != null)
                {
                    if (ucSalesProd1.productlist.Rows.Count > 0 || paymentno != "")
                    {
                        DataTable infodata = new DataTable();
                        infodata.Columns.Add("ITEMNAME", typeof(int));
                        infodata.Columns.Add("ITEMVALUE", typeof(string));
                        DataRow dd = infodata.NewRow();
                        dd["ITEMNAME"] = 1;
                        dd["ITEMVALUE"] = "test";
                        infodata.Rows.Add(dd);

                        Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 502, 500099, 500099, new object[] { contractno, orderno, rebateid, loyalid, pointid, ucSalesProd1.productlist, infodata });
                        if (ISM.Template.FormUtility.ValidateQuery(res))
                        {
                            ///res.Param[0, 1, 2..] гэх мэтчилэн хөнгөлөлтын сонголтууд ирж байгаа
                            ///res.Param дотроо 
                            ///obj[0] - SALESDB буюу хөнгөлсөн шинэ үнэтэй бараа үйлчилгээний жагсаалт
                            ///obj[1] - Урамшуулалын барааны жагсаалт
                            ///obj[2] - POINT - Оноо
                            ///obj[3] - REBATEID - Хөнгөлөлтын дугаар
                            ///obj[4] - LOYALID - Урамшуулалын дугаар
                            ///obj[5] - POINTID - Онооны дугаар
                            ///obj[6] - Хөнгөлөлт урамшуулалын нэр
                            ///гэсэн форматтайгаар мэдээлэл ирж байгаа.
                            object[] obj = null;
                            if (res.Param != null)
                            {
                                if (res.Param.Count() != 0)
                                {
                                    if (res.Param.Count() > 1)
                                    {
                                        int index = 0;
                                        //Сонголтын цонх дуудаж байна. Сонгосон сонголт индекс frm.rebateindex дээр орсон байгаа.
                                        frmRebateOptions frm = new frmRebateOptions(res.Param);
                                        frm.ShowDialog();
                                        obj = (object[])res.Param[Static.ToInt(frm.rebateindex)];
                                        point = Static.ToDecimal(obj[2]);
                                    }
                                    if (res.Param.Count() == 1)
                                    {
                                        obj = (object[])res.Param[0];
                                    }
                                    //Хөнгөлөлт тооцсон бараа болон үйлчилгээний үнийг оруулж байна.
                                    DataTable dt = (DataTable)obj[0];
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        ucSalesProd1.SetSalesAmount(Static.ToLong(dr["CUSTOMERNO"]), Static.ToStr(dr["PRODNO"]), Static.ToInt(dr["PRODTYPE"]), Static.ToDecimal(dr["SALESPRICE"]));
                                    }
                                    //Урамшуулалын барааг нэмэж байна.
                                    ucSalesProd1.LoyalProductRemove();
                                    if (obj[1] != null)
                                    {
                                        DataTable data = (DataTable)obj[1];
                                        foreach (DataRow dr in data.Rows)
                                        {
                                            ucSalesProd1.AddProduct(salesno, customerno, Static.ToStr(dr["PRODNO"]), Static.ToInt(dr["PRODTYPE"]), Static.ToStr(dr["PRODNAME"]), 0, 0, 0, 1, 3, 0);
                                        }
                                    }
                                    point = Static.ToDecimal(obj[2]);
                                }
                                else
                                {
                                    ucSalesProd1.DefaultSalesAmount();
                                }
                            }
                            //Сонгогдсон байсан харилцагчаар мэдээллийг филтерлэж байна.
                            ucSalesProd1.CustomerFilter(customerno);
                            if (contractno != "" && contractno != null)
                                ucCalcSales1.PaymentState();
                            else
                                ucCalcSales1.OtherPaymentState();
                            //Төлбөрийн дүнг харуулж буй хэсгийг дахин шинэчилж байна.
                            ucCalcSales1.SetAmount(ucSalesProd1.productlist);
                        }
                    }
                    else _core.AlertShow("Мэдээлэл", "Бүтээгдэхүүн үйлчилгээ оруулаагүй байна.");
                }
                else _core.AlertShow("Мэдээлэл", "Бүтээгдэхүүн үйлчилгээ оруулаагүй байна.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " : " + ex.StackTrace);
            }
        }

        void ucSaleCustomer1_EventOnItemClick(long CustomerNo,string SalesNo)
        {
            salesno = SalesNo;
            customerno = CustomerNo;
            _core.MainForm_HeaderClear();
            _core.MainForm_HeaderSet(0, "Харилцагчийн №", Static.ToStr(customerno));
            ucSalesProd1.CustomerFilter(CustomerNo);
            ucCalcSales1.SetAmount(ucSalesProd1.productlist);
        }
        void touchButtonGroup1_EventKeyDown(Control sender, MouseEventArgs e, TouchLinkItem item, ref bool cancel)
        {
            try
            {
                if (ucSaleCustomer1.ItemCount() == 0)
                {
                    _core.AlertShow("Мэдээлэл", "Харилцагч сонгогдоогүй байна.");
                    return;
                }
                if (item != null)
                {
                    if (productpanel.ContainsKey(item.parentKey.ToString() + "-" + item.Key.ToString()))
                    {
                        DataTable productlist = new DataTable();
                        productlist.Columns.Add("SALESNO", typeof(string));
                        productlist.Columns.Add("CUSTOMERNO", typeof(long));
                        productlist.Columns.Add("PRODCODE", typeof(string));
                        productlist.Columns.Add("PRODTYPE", typeof(int));
                        productlist.Columns.Add("PRODNAME", typeof(string));
                        productlist.Columns.Add("PRICE", typeof(decimal));
                        productlist.Columns.Add("DISCOUNT", typeof(decimal));
                        productlist.Columns.Add("SALESAMOUNT", typeof(decimal));
                        productlist.Columns.Add("QUANTITY", typeof(long));
                        productlist.Columns.Add("FLAG", typeof(int));
                        productlist.Columns.Add("EDIT");
                        productlist.Columns.Add("DELETE");
                        DataRow drow = (DataRow)productpanel[item.parentKey.ToString() + "-" + item.Key.ToString()];
                        switch (Static.ToInt(drow["NODETYPE"]))
                        {
                            case 1:
                                PACKINPRODUCT.Clear();
                                xtraTabControl1.SelectedTabPageIndex = 11;
                                GetPack(Static.ToStr(drow["NODEID"]));
                                break;
                            case 2:
                                productlist = GetInv(Static.ToStr(drow["NODEID"]), productlist, 1);
                                break;
                            case 3:
                                productlist = GetServ(Static.ToStr(drow["NODEID"]), productlist, 1);
                                break;
                        }
                        foreach (DataRow dr in productlist.Rows)
                        {
                            ucSalesProd1.AddProduct(salesno,
                            Static.ToLong(dr["CUSTOMERNO"]),
                            Static.ToStr(dr["PRODCODE"]),
                            Static.ToInt(dr["PRODTYPE"]),
                            Static.ToStr(dr["PRODNAME"]),
                            Static.ToDecimal(dr["PRICE"]),
                            Static.ToDecimal(dr["DISCOUNT"]),
                            Static.ToDecimal(dr["SALESAMOUNT"]),
                            Static.ToInt(dr["QUANTITY"]),
                            Static.ToInt(dr["FLAG"]),
                            0
                            );
                        }
                        ucCalcSales1.DiscountState();
                        ucCalcSales1.prodlist = ucSalesProd1.productlist;
                        ucCalcSales1.SetAmount(ucSalesProd1.productlist, customerno);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " : " + ex.StackTrace);
            }
        }
        #endregion
        #region[ Function ]
        private void GetPack(string ID)
        {
            try
            {
                DataTable dt1 = new DataTable();
                Result res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PACKITEM", 500101, ref dt1);
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        if (Static.ToStr(dr["PACKID"]) == ID)
                        {
                            PACKINPRODUCT.Rows.Add(ID, dr["PRODTYPE"], dr["PRODID"], dr["NAME"], dr["COUNT"], 1, 0, 0);
                        }
                    }
                    gridControl2.DataSource = PACKINPRODUCT;
                    gridView2.BestFitColumns();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private DataTable GetPack(string ID, DataTable prodlist)
        //{

        //    DataTable dt = new DataTable();
        //    DataTable dt1 = new DataTable();
        //    Result res;
        //    res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PACKMAIN", 500101, ref dt);
        //    if (ISM.Template.FormUtility.ValidateQuery(res))
        //    {
        //        res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PACKITEM", 500101, ref dt1);
        //        if (ISM.Template.FormUtility.ValidateQuery(res))
        //        {
        //            var query = from row in dt1.AsEnumerable()
        //                        where row.Field<string>("PackID") == ID
        //                        select row;
        //            if (query != null && query.Count() > 0)
        //            {
        //                dt = query.CopyToDataTable();
        //                foreach (DataRow drow in dt.Rows)
        //                {
        //                    switch (Static.ToInt(drow["ProdType"]))
        //                    {
        //                        case 0:
        //                            prodlist = GetInv(Static.ToStr(drow["ProdID"]), prodlist, Static.ToInt(drow["count"]));
        //                            break;
        //                        case 1:
        //                            prodlist = GetServ(Static.ToStr(drow["ProdID"]), prodlist, Static.ToInt(drow["count"]));
        //                            break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return prodlist;
        //}
        private DataTable GetInv(string ID, DataTable prodlist,int invcount)
        {
            object[] obj = new object[10];
            DataTable dt = new DataTable();
            decimal tmpamount = 0;
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "INVMAIN", 500101, ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                var query = from row in dt.AsEnumerable()
                            where row.Field<string>("INVID") == ID
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dt = query.CopyToDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        // 0 - SALESNO
                        // 1 - CUSTOMERNO
                        // 2 - PRODCODE
                        // 3 - PRODTYPE
                        // 4 - PRODNAME
                        // 5 - PRICE
                        // 6 - DISCOUNT
                        // 7 - SALESAMOUNT
                        // 8 - QUANTITY
                        // 9 - FLAG 0 - UnFlag, 1 - RentFlag, 9- RebateFlag 
                        obj[0] = salesno.ToString();
                        obj[1] = customerno;
                        obj[2] = Static.ToStr(ID);
                        obj[3] = 0;
                        obj[4] = Static.ToStr(dt.Rows[0]["NAME"]);
                        obj[5] = Static.ToDecimal(dt.Rows[0]["PRICEAMOUNT"]);
                        obj[6] = 0;
                        obj[7] = 0;
                        if (invcount != 0)
                            obj[8] = invcount;
                        else
                            obj[8] = 1;
                        obj[9] = Static.ToInt(dt.Rows[0]["RENTFLAG"]);
                        tmpamount = GetCalendarPrice(0, ID, DateTime.Now);
                        if (tmpamount != 0)
                        {
                            obj[5] = tmpamount;
                        }

                        prodlist.Rows.Add(obj);
                    }
                    else
                    {
                        MessageBox.Show("Энэ үйлчилгээний мэдээлэл байхгүй байна. Системийн администраторт хандана уу!!! " + "Бараа " + ID);
                    }

                }
            }
            return prodlist;
        }
        private DataTable GetServ(string ID, DataTable prodlist,int servcount)
        {
            object[] obj = new object[10];
            DataTable dt = new DataTable();
            decimal tmpamount = 0;
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "SERVMAIN", 500101, ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                var query = from row in dt.AsEnumerable()
                            where row.Field<string>("SERVID") == ID
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dt = query.CopyToDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        // 0 - SALESNO
                        // 1 - CUSTOMERNO
                        // 2 - PRODCODE
                        // 3 - PRODTYPE
                        // 4 - PRODNAME
                        // 5 - PRICE
                        // 6 - DISCOUNT
                        // 7 - SALESAMOUNT
                        // 8 - QUANTITY
                        // 9 - FLAG 0 - UnFlag, 1 - RentFlag, 9- RebateFlag 
                        // 10 - EDIT
                        // 11 - DELETE
                        obj[0] = salesno.ToString();
                        obj[1] = customerno;
                        obj[2] = Static.ToStr(ID);
                        obj[3] = 1;
                        obj[4] = Static.ToStr(dt.Rows[0]["NAME"]);
                        obj[5] = Static.ToDecimal(dt.Rows[0]["PRICEAMOUNT"]);
                        obj[6] = 0; 
                        obj[7] = 0;
                        if (servcount != 0)
                            obj[8] = servcount;
                        else
                            obj[8] = 1;
                        obj[9] = 0;
                        tmpamount = GetCalendarPrice(0, ID, DateTime.Now);
                        if (tmpamount != 0)
                        {
                            obj[5] = tmpamount;
                        }

                        prodlist.Rows.Add(obj);
                    }
                    else
                    {
                        MessageBox.Show("Энэ үйлчилгээний мэдээлэл байхгүй байна. Системийн администраторт хандана уу!!! " + "Бараа " + ID);
                    }
                }
            }
            return prodlist;
        }
        private decimal GetCalendarPrice(int typeid, string id, DateTime datetime)
        {
            DataTable dt = new DataTable();
            decimal result = 0;
            string daytype = "";
            Result res;
            res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PACALENDAR", 500101, ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                var query = from row in dt.AsEnumerable()
                            where row.Field<DateTime>("DAY") == datetime
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dt = query.CopyToDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        daytype = Static.ToStr(dt.Rows[0]["DayType"]);
                    }
                }
            }

            if (daytype == "") return result;

            DataTable dt1 = new DataTable();
            res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PRODPRICE", 500101, ref dt1);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                var query = from row in dt1.AsEnumerable()
                            where row.Field<int>("ProdType") == typeid &&
                                  row.Field<string>("ProdID") == id &&
                                  row.Field<DateTime>("StartTime") >= datetime &&
                                  row.Field<DateTime>("EndTime") <= datetime
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dt1 = query.CopyToDataTable();
                    if (dt1.Rows.Count > 0)
                    {
                        result = Static.ToDecimal(dt1.Rows[0]["Price"]);
                    }
                }
            }
            return result;
        }
        private void InitTouchButton()
        {
            string parent = "";
            string code = "";
            string name = "";
            int col = 0;
            int row = 0;
            int ptype = 0;
            string pid = "";
            try
            {
                productpanel = new Hashtable();

                DataTable dt = new DataTable();
                Result res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PRODUCTPANEL", 500101, ref dt);
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        touchButtonGroup1.Init(3, 3, 2);
                        foreach (DataRow drow in dt.Rows)
                        {
                            parent = Static.ToStr(drow["PARENTCODE"]);
                            code = Static.ToStr(drow["NodeCode"]);
                            if (_core.RemoteObject.User.UserLanguage == "MN")
                                name = Static.ToStr(drow["Name"]);
                            else
                                name = Static.ToStr(drow["Name2"]);
                            col = Static.ToInt(drow["COLINDEX"]);
                            row = Static.ToInt(drow["ROWINDEX"]);
                            ptype = Static.ToInt(drow["NODETYPE"]);
                            pid = Static.ToStr(drow["NODEID"]);

                            touchButtonGroup1.Add(parent, code, row, col, name, name, _core.Resource.GetBitmap("frontmenu_batch_choose"));
                            if (ptype != 0)
                                productpanel.Add(parent.ToString() + "-" + code.ToString(), drow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void InitTouchTag()
        {
            touchtagsign.Init(1, 2, 2);
            touchtagsign.Add("ROOT", "tagclean", 1, 1, "Таг цэвэрлэх", "", _core.Resource.GetBitmap("tag_purple_unread"));
            touchtagsign.Add("ROOT", "tagwrite", 1, 2, "Таг цэнэглэх", "", _core.Resource.GetBitmap("tag_purple_read"));
        }
        public void ShowKeyboard(int rowhandle)
        {
            if (rowhandle < gridView2.RowCount)
            {
                gridView2.FocusedRowHandle = rowhandle;
                DataRow dr = gridView2.GetDataRow(rowhandle);
                DialogResult res = _kb.ShowKeyboard(gridView2, rowhandle, 5);
                if (res == DialogResult.OK)
                {
                    if(Static.ToInt(dr["BALANCEQTY"])<=Static.ToInt(dr["CHOOCEQTY"]))
                    {
                        ucSalesProd1.AddProduct(salesno, customerno, Static.ToStr(dr["PRODNO"]), Static.ToInt(dr["PRODTYPE"]), Static.ToStr(dr["NAME"]), 0, 0, 0, Static.ToInt(dr["BALANCEQTY"]), 4, 1);
                        gridView2.DeleteRow(rowhandle);
                        ShowKeyboard(rowhandle);
                    }
                    else
                    {
                        ucSalesProd1.AddProduct(salesno, customerno, Static.ToStr(dr["PRODNO"]), Static.ToInt(dr["PRODTYPE"]), Static.ToStr(dr["NAME"]), 0, 0, 0, Static.ToInt(dr["CHOOCEQTY"]), 4, 1);
                        dr["BALANCEQTY"] = Static.ToInt(dr["BALANCEQTY"]) - Static.ToInt(dr["CHOOCEQTY"]);
                        if (rowhandle + 1 < gridView2.RowCount)
                            ShowKeyboard(rowhandle + 1);
                    }
                }
            }
        }
        DataTable PACKINPRODUCT = new DataTable();
        public void InitPackProduct()
        {
            try
            {
                gridView2.OptionsBehavior.ReadOnly = true;
                gridView2.OptionsBehavior.Editable = false;
                gridView2.OptionsCustomization.AllowGroup = false;
                gridView2.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
                gridView2.OptionsView.ColumnAutoWidth = false;
                gridView2.OptionsView.ShowGroupPanel = false;
                gridView2.OptionsView.ShowIndicator = false;
                gridView2.Appearance.Row.Font = new Font("Tahoma", 11);
                gridView2.RowHeight = 40;
                gridControl2.DataSource = null;
                PACKINPRODUCT.Columns.Add("PACKID", typeof(string));
                PACKINPRODUCT.Columns.Add("PRODTYPE", typeof(int));
                PACKINPRODUCT.Columns.Add("PRODNO", typeof(string));
                PACKINPRODUCT.Columns.Add("NAME", typeof(string));
                PACKINPRODUCT.Columns.Add("BALANCEQTY", typeof(int));
                PACKINPRODUCT.Columns.Add("CHOOCEQTY", typeof(int));
                PACKINPRODUCT.Columns.Add("EDIT", typeof(Int16));
                PACKINPRODUCT.Columns.Add("CHOOSE", typeof(Int16));
                gridControl2.DataSource = PACKINPRODUCT;
                gridView2.Columns[0].Caption = "Багцын №";
                gridView2.Columns[1].Caption = "Төрөл";
                gridView2.Columns[2].Caption = "Код";
                gridView2.Columns[3].Caption = "Нэр";
                gridView2.Columns[4].Caption = "Үлдэгдэл тоо";
                gridView2.Columns[5].Caption = "Хувиарлах тоо";
                gridView2.Columns[6].Caption = "...";
                gridView2.Columns[7].Caption = "...";

                gridView2.Columns[0].Visible = false;
                gridView2.Columns[1].Visible = false;
                gridView2.Columns[2].Visible = false;

                DevExpress.Utils.ImageCollection imagecollection1 = new DevExpress.Utils.ImageCollection();
                imagecollection1.AddImage(_core.Resource.GetImage("edit-validated-icon"));
                imagecollection1.AddImage(_core.Resource.GetImage("button_ok"));

                repoChooce.LargeImages = imagecollection1;
                repoEditChooce.LargeImages = imagecollection1;

                gridView2.Columns[6].ColumnEdit = repoEditChooce;
                gridView2.Columns[7].ColumnEdit = repoChooce;

                gridView2.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView2_RowCellClick);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int rowhandle = e.RowHandle;
            DataRow dr = gridView2.GetDataRow(rowhandle);
            int QTY = Static.ToInt(dr["BALANCEQTY"]);
            switch (e.Column.FieldName)
            {
                case "EDIT":
                        ShowKeyboard(rowhandle);
                        break;
                case "CHOOSE": 
                        if (QTY == 1)
                        {
                            ucSalesProd1.AddProduct(salesno, customerno, Static.ToStr(dr["PRODNO"]), Static.ToInt(dr["PRODTYPE"]), Static.ToStr(dr["NAME"]), 0, 0, 0, 1, 4, 1);
                            gridView2.DeleteRow(rowhandle);
                        }
                        else
                        {
                            ucSalesProd1.AddProduct(salesno, customerno, Static.ToStr(dr["PRODNO"]), Static.ToInt(dr["PRODTYPE"]), Static.ToStr(dr["NAME"]), 0, 0, 0, 1, 4, 1);
                            dr["BALANCEQTY"] = QTY - 1;
                        }
                    break;
            }
        }
        #endregion
        #region[ BTN ]
        //Сонголт 1 - Харилцагч, 2 - Гэрээ, 3 - Захиалага
        private void button1_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            ucCustSearch1.DataRefresh(1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 7;
            ucContractSearch1.DataRefresh(1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 8;
            ucOrderSearch1.DataRefresh(1);
        }

        //Сонгогдсон харилцагч устгах
        void ucSaleCustomer1_EventOnBtnDelete(long CustomerNo)
        {
            ucSalesProd1.RemoveProduct(CustomerNo);
            if (ucSaleCustomer1.ItemCount() == 0)
            {
                xtraTabControl1.SelectedTabPageIndex = 1;
            }
        }

        //Сонгогдсон харилцагч солих
        void ucSaleCustomer1_EventOnBtnEdit(long CustomerNo)
        {
            OldCustNo = CustomerNo;
            pnlSales.Visible = false;
            xtraTabControl1.SelectedTabPageIndex = 1;
            pnlSales.Visible = true;
        }

        //Багцын барааг харилцагчтай уях
        private void btnProduct_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        private void frmSaleNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_core.Tag.tagreader != null)
                _core.Tag.tagreader.OnCardRead -= new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
        }

        private void gridView2_RowCountChanged(object sender, EventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                xtraTabControl1.SelectedTabPageIndex = 2;
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            frmSales frm = new frmSales();
            bool b = false;
            frm.Init("", null, ParentForm, _core, ref b);
            frm.Show();
        }

        private void frmSaleNew_Load(object sender, EventArgs e)
        {

        }
    }
}