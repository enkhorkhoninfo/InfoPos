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
using DevExpress.XtraBars.Ribbon;

using ISM.Touch;
using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmSalesEdit : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        #region Comment!
        /***************************************
         * Борлуулалт эхлэхийн өмнө үйлчлүүлэгч заавал Бүртгэл дээр
         * очиж барьцааны бичиг баримтаа тавиж, таг авсан байна.
         * 
         * Дараах мэдээллүүдийг бөөнд явуулж хадгална.
         * 1. Бараануудын мэдээлэл. _carts тэйблийг явуулах
         * 2. Хамаарах үйлчлүүлэгчдийн мэдээлэл. Сервер дээр барьцааны мэдээлэлд оруулах.
         * 
         * Таг уншуулахад борлуулалтын мэдээллийг шууд гаргаж харуулах.
         * 
         ***************************************/
        #endregion
        #region Internal events

        public event Core.Tag.delegateEventOnCard EventOnCard;

        #endregion
        #region Internal variables

        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;

        object[] _arraySearchForm = null;

        DateTime _trandate = DateTime.MinValue;
        decimal _totalamount = 0;
        decimal _salesamount = 0;
        decimal _discount = 0;
        decimal _vat = 0;
        decimal _vat_extend = 0;
        decimal _prepaid = 0;
        bool _isvat = true;

        string _contractno = null;
        string _orderno = null;
        string _salesno = null;
        string _pledgeno = null;
        //int _status = 0;

        decimal _custno = 0;  //pledge owner
        string _custname = null;
        decimal _selected_custno = 0;
        string _selected_custname = null;

        private string _layoutfilename = "";

        ArrayList _tables = null;

        DataSet _ds = new DataSet();
        DataTable _cart = new DataTable();
        DataTable _paymentdetail = null;
        
        frmSalesInv _frmInv = null;

        Image _imgExtend = null;

        #endregion

        #region Menu functions

        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;
                _kb = new TouchKeyboard();
                if (_core.IsTouch == true)
                    _kb.Enable = true;
                else
                    _kb.Enable = false;


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
            try
            {
                switch (buttonkey)
                {
                    case "fo_sales_edit_exit":
                        item.IsClose = 1;
                        this.Close();
                        break;
                    case "fo_sales_edit_search":
                        SubMenu_Search();
                        break;
                    case "fo_sales_edit_bill":
                        SubMenu_Bill();
                        break;
                    case "fo_sales_edit_payment":
                        SubMenu_Payment();
                        break;

                    case "fo_sales_edit_refund1":
                        SubMenu_Refund1();
                        break;
                    case "fo_sales_edit_refund2":
                        SubMenu_Refund2();
                        break;
                    case "fo_sales_edit_refund3":
                        SubMenu_Refund3();
                        break;
                    case "fo_sales_edit_move":
                        SubMenu_Rent();
                        break;

                    case "fo_sales_edit_extendtxn":
                        SubMenu_ExtendTxn();
                        break;

                    case "fo_sales_edit_correction":
                        SubMenu_Correction();
                        break;

                    case "call_tagreader":
                        string tagno = item.Text;
                        Core.TagEventData tagdata = (Core.TagEventData)item.Tag;

                        if (_frmInv == null)
                        {
                            SubMenu_TagReader(tagno);
                        }
                        else
                        {
                            _frmInv.EventOnCard(tagdata);
                        }

                        //if (EventOnCard != null)
                        //{
                        //    EventOnCard(tagdata);
                        //}
                        //else
                        //{
                        //    SubMenu_TagReader(tagno);
                        //}
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
            SelectSales();
        }
        public void SubMenu_Payment()
        {
            PaymentForm();
        }
        public void SubMenu_Refund1()
        {
            RefundSales();
        }
        public void SubMenu_Refund2()
        {
            RefundProduct();
        }
        public void SubMenu_Refund3()
        {
            RefundPayment();
        }
        private void SubMenu_TagReader(string serialno)
        {
            SelectSalesByTag(serialno);
            //_serialno = serialno;
            //Result res = ReadRecordByTag(serialno);
            //ISM.Template.FormUtility.ValidateQuery(res);
        }
        public void SubMenu_Rent()
        {
            RentForm(_salesno);
        }
        public void SubMenu_Bill()
        {
            BillPrint();
        }

        //public void SubMenu_Extend()
        //{
        //    SalesExtendOrCorrectionValue();
        //}
        public void SubMenu_ExtendTxn()
        {
            SalesExtendTxn();
        }
        public void SubMenu_Correction()
        {
            SalesCorrectionTxn();
        }

        #endregion
        #region Business functions

        public void InitCart()
        {
            _ds.DataSetName = "_ds";

            /***********************************************
             * Сагсны дэлгэцэнд харуулах талбарууд
             ***********************************************/
            _cart.TableName = "CART";
            _cart.Columns.Add("CUSTNO", typeof(decimal));
            _cart.Columns.Add("CUSTNAME", typeof(string));
            _cart.Columns.Add("PRODNO", typeof(string));
            _cart.Columns.Add("PRODNAME", typeof(string));
            _cart.Columns.Add("PRODTYPE", typeof(int));
            _cart.Columns.Add("PRICE", typeof(decimal));
            _cart.Columns.Add("QTY", typeof(decimal));
            _cart.Columns.Add("DISCOUNT", typeof(decimal));

            /***********************************************
             * Тооцоололд ашиглах далд талбарууд
             ***********************************************/
            _cart.Columns.Add("UNITPRICE", typeof(decimal));
            _cart.Columns.Add("SALEPRICE", typeof(decimal));

            _cart.Columns.Add("SALESAMOUNT", typeof(decimal));
            _cart.Columns.Add("DISCOUNTPROD", typeof(decimal));
            _cart.Columns.Add("DISCOUNTSALES", typeof(decimal));
            _cart.Columns.Add("TOTALAMOUNT", typeof(decimal));

            _cart.Columns.Add("SAVAT", typeof(decimal));
            _cart.Columns.Add("DPVAT", typeof(decimal));
            _cart.Columns.Add("DSVAT", typeof(decimal));

            _cart.Columns.Add("SALETYPE", typeof(int));
            _cart.Columns.Add("PACKID", typeof(string));
            _cart.Columns.Add("CATEGORY", typeof(string));
            _cart.Columns.Add("CONTRACTNO", typeof(string));

            _cart.Columns.Add("EXTEND", typeof(int));

            gridControl1.DataSource = _cart;
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "CustNo", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Үйлчлүүлэгч");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "ProdNo", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Үйлчилгээний нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Төрөл", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Нэгж үнэ");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Тоо/ш");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Хөн.");

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 12, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 13, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 14, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 15, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 16, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 17, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 18, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 19, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 20, "", true);

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 21, "Сунгах");

            gridView1.Columns[1].GroupIndex = 0;
            gridView1.GroupFormat = "{1}";

            gridView1.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[5].DisplayFormat.FormatString = "#,##0.00";
            gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            gridView2.OptionsBehavior.FocusLeaveOnTab = true;

            gridView1.RowHeight = 28;
            gridView2.RowHeight = 28;

            #region Add picture column

            RepositoryItemPictureEdit ri = new RepositoryItemPictureEdit();
            gridControl1.RepositoryItems.Add(ri);

            DevExpress.XtraGrid.Columns.GridColumn delcol = new DevExpress.XtraGrid.Columns.GridColumn();
            delcol.VisibleIndex = gridView1.Columns.Count;
            delcol.Caption = "...";
            delcol.FieldName = string.Format("PIC");
            delcol.ColumnEdit = ri;
            delcol.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            delcol.OptionsColumn.ReadOnly = true;
            delcol.Width = 32;
            gridView1.Columns.Add(delcol);

            #endregion

        }
        public Result InitDict()
        {
            string[] dictid = new string[] { "INVMAIN", "SERVMAIN", "PACKMAIN", "PACKITEM", "PRODPRICE", "PRODTREE", "PACALENDAR" };
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject, dictid, 601003, ref _tables);
            ISM.Template.FormUtility.ValidateQuery(res);
            return res;
        }

        public structProdInfo GetProdInfo(string prodno, int type)
        {
            structProdInfo info = new structProdInfo();
            info.found = false;

            try
            {
                if (type == 0)
                {
                    #region Бараа материал
                    DataTable dt = (DataTable)_tables[0];
                    DataRow[] rows = dt.Select(string.Format("invid='{0}'", prodno));
                    if (rows != null && rows.Length > 0)
                    {
                        info.prodno = prodno;
                        info.prodname = Static.ToStr(rows[0]["name"]);
                        info.subtype = Static.ToStr(rows[0]["invtype"]);
                        info.price = Static.ToDecimal(rows[0]["priceamount"]);
                        info.found = true;
                    }
                    #endregion
                }
                else if (type == 1)
                {
                    #region Үйлчилгээ
                    DataTable dt = (DataTable)_tables[1];
                    DataRow[] rows = dt.Select(string.Format("servid='{0}'", prodno));
                    if (rows != null && rows.Length > 0)
                    {
                        info.prodno = prodno;
                        info.prodname = Static.ToStr(rows[0]["name"]);
                        info.subtype = Static.ToStr(rows[0]["servtype"]);
                        info.price = Static.ToDecimal(rows[0]["priceamount"]);
                        info.schedule = Static.ToStr(rows[0]["scheduletype"]);
                        info.found = true;
                    }
                    #endregion
                }
                else if (type == 2)
                {
                    #region Багц
                    DataTable dt = (DataTable)_tables[2];
                    DataRow[] rows = dt.Select(string.Format("packid='{0}'", prodno));
                    if (rows != null && rows.Length > 0)
                    {
                        info.prodno = prodno;
                        info.prodname = Static.ToStr(rows[0]["name"]);
                        info.subtype = "";
                        info.price = Static.ToDecimal(rows[0]["price"]);
                        info.found = true;
                    }
                    #endregion
                }
            }
            catch
            { }

            return info;
        }
        public void AddToCart(decimal custno, string custname, string prodno, int type, decimal qty)
        {
            Result res = null;
            decimal newqty = qty;
            int rowindex = 0;

            #region Validation1 - Is customer selected

            if (_selected_custno == 0)
            {
                res = new Result(9, "Үйлчлүүлэгч сонгогдоогүй байна!");
                goto OnExit;
            }

            #endregion

            #region Get product information
            structProdInfo info = GetProdInfo(prodno, type);
            if (!info.found) return;
            #endregion
            #region Edit cart list
            DataRow[] rows = _cart.Select(string.Format("CUSTNO={0} AND PRODNO='{1}' AND PRODTYPE={2}", custno, prodno, type));
            if (rows != null && rows.Length > 0)
            {
                newqty = ((decimal)rows[0]["QTY"]) + 1;
                rows[0]["QTY"] = newqty;

                if (newqty <= 0)
                {
                    rows[0].Delete();
                    rows[0].Table.AcceptChanges();

                    rowindex = _cart.Rows.Count;
                }
                else
                {
                    rowindex = _cart.Rows.IndexOf(rows[0]);
                }
            }
            else
            {
                _cart.Rows.Add(custno, custname, info.prodno, info.prodname, type, 1, "", info.price, 0, 0);
                rowindex = _cart.Rows.Count - 1;
            }
            gridView1.FocusedRowHandle = gridView1.GetRowHandle(rowindex);
            
            #endregion
            #region Calc total amount

            RefreshBoardValues();

            #endregion

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }

        public bool SelectSales()
        {
            Result res = null;
            bool success = false;
            try
            {
                InfoPos.sales.frmSalesSearch frm = new sales.frmSalesSearch(_core);
                frm.StartPosition = FormStartPosition.CenterScreen;
                DialogResult dr = frm.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    SalesClear();
                                        
                    if (frm.CurrentRow != null)
                    {
                        _arraySearchForm = frm.CurrentRow.ItemArray;

                        //_status = Static.ToInt(frm.CurrentRow["STATUS"]);
                        _salesno = Static.ToStr(frm.CurrentRow["SALESNO"]);
                        _contractno = Static.ToStr(frm.CurrentRow["CONTRACTNO"]);
                        _orderno = Static.ToStr(frm.CurrentRow["ORDERNO"]);
                        _pledgeno = Static.ToStr(frm.CurrentRow["PLEDGENO"]);
                        _custno = Static.ToDecimal(frm.CurrentRow["CUSTNO"]);
                        _custname = Static.ToStr(frm.CurrentRow["CUSTNAME"]);

                        _selected_custno = _custno;
                        _selected_custname = _custname;

                        _totalamount = Static.ToDecimal(frm.CurrentRow["TOTALAMOUNT"]);
                        _salesamount = Static.ToDecimal(frm.CurrentRow["SALESAMOUNT"]);
                        _discount = Static.ToDecimal(frm.CurrentRow["DISCOUNT"]);
                        _vat = Static.ToDecimal(frm.CurrentRow["VAT"]);
                        _prepaid = Static.ToDecimal(frm.CurrentRow["PAID"]);
                        _trandate = Static.ToDate(frm.CurrentRow["TRANDATE"]);

                        _isvat = _vat > 0;                        

                        _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                        _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                        _core.MainForm_HeaderSet(2, "Гэрээ", _contractno);
                        _core.MainForm_HeaderSet(3, "Захиалга", _orderno);
                        _core.MainForm_HeaderSet(5, "Борлуулалт", _salesno);
                        
                        frm.Dispose();

                        res = ReadProdFromSales(_trandate, _salesno);
                        if (res != null && res.ResultNo != 0) goto OnExit;

                        res = ReadPaymentFromSales(_trandate, _salesno);
                        if (res != null && res.ResultNo != 0) goto OnExit;

                        RefreshBoardValues();

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
            return success;
        }
        public bool SelectSalesByTag(string tagno)
        {
            Result res = null;
            bool success = false;
            try
            {
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                object[] param = new object[] { _core.TxnDate, tagno };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605007, 605001, param);
                if (res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows <= 0)
                {
                    res = new Result(9, string.Format("[{0}] тагийн дугаараар борлуулалт олдсонгүй.", tagno));
                    goto OnExit;
                }
                #endregion

                SalesClear();

                DataRow r = res.Data.Tables[0].Rows[0];

                //_status = Static.ToInt(frm.CurrentRow["STATUS"]);
                _salesno = Static.ToStr(r["SALESNO"]);
                _contractno = Static.ToStr(r["CONTRACTNO"]);
                _orderno = Static.ToStr(r["ORDERNO"]);
                _custno = Static.ToDecimal(r["CUSTNO"]);
                _custname = Static.ToStr(r["CUSTNAME"]);

                _selected_custno = _custno;
                _selected_custname = _custname;

                _totalamount = Static.ToDecimal(r["TOTALAMOUNT"]);
                _salesamount = Static.ToDecimal(r["SALESAMOUNT"]);
                _discount = Static.ToDecimal(r["DISCOUNT"]);
                _vat = Static.ToDecimal(r["VAT"]);
                _prepaid = Static.ToDecimal(r["PAID"]);

                _trandate = Static.ToDate(r["TRANDATE"]);

                _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                _core.MainForm_HeaderSet(2, "Гэрээ", _contractno);
                _core.MainForm_HeaderSet(3, "Захиалга", _orderno);
                _core.MainForm_HeaderSet(5, "Борлуулалт", _salesno);

                res = ReadProdFromSales(_trandate, _salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                res = ReadPaymentFromSales(_trandate, _salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                RefreshBoardValues();

                success = true;

            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
            return success;
        }
        public Result ReadProdFromSales(DateTime trandate, string salesno)
        {
            Result res = null;
            try
            {
                #region Validation
                #endregion
                #region Prepare parameters
                object[] param = new object[] { trandate, salesno };
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605004, 605001, param);
                if (res.ResultNo != 0) goto OnExit;

                #endregion
                #region Build cart data grid
                DataTable dt = res.Data.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    decimal uprice = Static.ToDecimal(r["BASEPRICE"]);
                    decimal sprice = Static.ToDecimal(r["PRICE"]);

                    decimal sa = Static.ToDecimal(r["SALESAMOUNT"]);
                    decimal dp = Static.ToDecimal(r["DISCOUNTPROD"]);
                    decimal ds = Static.ToDecimal(r["DISCOUNTSALES"]);
                    decimal ta = sa - dp - ds;

                    _cart.Rows.Add(
                        r["CUSTNO"]
                        , r["CUSTNAME"]
                        , r["PRODNO"]
                        , r["PRODNAME"]
                        , r["PRODTYPE"]
                    , r["BASEPRICE"]    /*price*/
                    , r["QTY"]             /*qty*/
                    , uprice - sprice + dp + ds      /*discount*/
                    , uprice    /*unit price*/
                    , sprice         /*sale price*/
                    , r["SALESAMOUNT"]         /*sale amount*/
                    , dp             /*discountprod*/
                    , ds             /*discountsale*/
                    , ta             /*totalamount*/
                    , 0              /*SAVAT*/
                    , 0              /*DPVAT*/
                    , 0              /*DSVAT*/
                    , r["SALESTYPE"]             /*saletype*/
                    , r["PACKID"]        /*packid*/
                    , r["SUBTYPE"]  /*cat*/
                    , r["CONTRACTNO"]
                    );
                }

                gridView1.ExpandAllGroups();
                
                //string custreg = Static.ToStr(r["registerno"]);
                //string custname = Static.ToStr(r["custname"]);
                //decimal custno = Static.ToDecimal(r["custno"]);
                //CustomerAdd(custno, custname, custreg);

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(601901, ex.ToString());
            }
        OnExit:
            //ISM.Template.FormUtility.ValidateQuery(res);
            return res;
        }
        public Result ReadPaymentFromSales(DateTime trandate, string salesno)
        {
            Result res = null;
            try
            {
                #region Validation
                #endregion
                #region Prepare parameters
                object[] param = new object[] { trandate, salesno };
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605013, 605013, param);
                if (res.ResultNo != 0) goto OnExit;

                #endregion
                #region Build payment grid
                
                DataTable dt = res.Data.Tables[0];
                _paymentdetail = dt;
                ISM.Template.FormUtility.GridLayoutGet(gridView2, dt, _layoutfilename);

                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 0, "Борлуулалт №", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 1, "Огноо");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 2, "Төлбөр №");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 3, "ТТөрөл",true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 4, "ТТөрөл");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 5, "Дүн");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 6, "Ялгац",true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 7, "Ялгац");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 8, "Ажилтан");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 9, "SourceNo", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 10, "PaymentFlag", true);

                gridView2.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView2.Columns[1].DisplayFormat.FormatString = "yy.MM.dd HH:mm";

                gridView2.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView2.Columns[5].DisplayFormat.FormatString = "#,##0.00";

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(601901, ex.ToString());
            }
        OnExit:
            //ISM.Template.FormUtility.ValidateQuery(res);
            return res;
        }

        public void RefreshBoardValues()
        {
            try
            {
                #region Нийт дүнгүүдийг тооцоолох

                decimal sa = 0;
                decimal dp = 0;
                decimal ds = 0;
                decimal up = 0;

                if (_cart.Rows.Count > 0)
                {
                    var qtotal = from r in _cart.AsEnumerable()
                                 where string.IsNullOrEmpty(Static.ToStr(r["CONTRACTNO"]))
                                 group r by 1
                                     into agg
                                     select new
                                     {
                                         up = agg.Sum(rr => (Static.ToDecimal(rr["QTY"]) + Static.ToDecimal(rr["EXTEND"])) * Static.ToDecimal(rr["UNITPRICE"]))
                                         ,
                                         sa = agg.Sum(rr => Static.ToDecimal(rr["SALESAMOUNT"]))
                                         ,
                                         tdp = agg.Sum(rr => Static.ToDecimal(rr["DISCOUNT"]))
                                         ,
                                         dp = agg.Sum(rr => Static.ToDecimal(rr["DISCOUNTPROD"]))
                                         ,
                                         ds = agg.Sum(rr => Static.ToDecimal(rr["DISCOUNTSALES"]))
                                     };
                    if (qtotal != null && qtotal.Count() > 0)
                    {
                        var v = qtotal.First();
                        sa = v.sa;
                        ds = v.ds;
                        up = v.up;

                        decimal dd = v.up - v.sa;
                        if (dd < 0) dd = 0;
                        dp = v.dp + dd;

                        if (dp > sa) dp = sa;
                    }
                }
                decimal ta = (sa - dp - ds);

                numSales.EditValue = sa;    // НӨАТ орсон дүнгээрээ
                numDiscount.EditValue = dp + ds; // НӨАТ орсон дүнгээрээ
                //numRebate.EditValue = ds;

                if (_isvat)
                {
                    numTotal.EditValue = ta;
                    _vat_extend = ta * _core.Vat / 100;
                    numVat.EditValue = ta * _core.Vat / 100;
                }
                else
                {
                    _vat_extend = -ta * _core.Vat / 100;
                    numVat.EditValue = -ta * _core.Vat / 100;

                    ta = ta * (1 - _core.Vat / 100);
                    numTotal.EditValue = ta;
                }

                numPaid.EditValue = _prepaid;
                numRemain.EditValue = ta - _prepaid;

                if (ta - _prepaid > 0) numRemain.ForeColor = Color.Red;
                else numRemain.ForeColor = Color.DarkGreen;

                #endregion
            }
            catch
            { }
        }
        public void RefreshBoardValues1()
        {
            try
            {
                /******************************************
                 * хайтлийн формоос дараах утгууд ирсэн бгаа.
                 * 
                 * TotalAmount нь бүх хөнгөлөлт, НӨАТийн дүн
                 * хасагдаж ирнэ.
                 * 
                 * VAT нь хэрэв НӨАТ тооцох бол (+) дүнтэй
                 * хэрэв тооцохгүй бол (-) дүнтэйгээр орж ирнэ.
                 * Тэг дүнтэйгээр хэзээ ч бичигдэхгүй.
                 * 
                 ******************************************/
                numSales.EditValue = _salesamount;
                numDiscount.EditValue = _discount;
                numPaid.EditValue = _prepaid;
                numRemain.EditValue = _totalamount - _prepaid;
                numVat.EditValue = _vat;
                numTotal.EditValue = _totalamount;
                                
                if (_totalamount - _prepaid > 0) numRemain.ForeColor = Color.Red;
                else numRemain.ForeColor = Color.DarkGreen;

                RefreshExtendValues();
            }
            catch
            { }
        }
        public void RefreshExtendValues()
        {
            try
            {
                decimal extendamount = _cart.AsEnumerable().Sum(r =>
                    (
                        Static.ToDecimal(r["PRICE"]) - Static.ToDecimal(r["DISCOUNTPROD"]) / Static.ToDecimal(r["QTY"])
                    ) * Static.ToDecimal(r["EXTEND"])
                    );

                if (_vat < 0)
                {
                    extendamount = extendamount * (1 - _core.Vat / 100);
                }

                numExtendAmount.EditValue = extendamount;
                
                //if (_salesamount - _prepaid > 0) numRemain.ForeColor = Color.Red;
                //else numRemain.ForeColor = Color.DarkGreen;
            }
            catch
            { }
        }

        public void SalesClear()
        {
            _contractno = null;
            _salesno = null;
            _discount = 0;

            _custno = 0;  //pledge owner
            _custname = null;
            _selected_custno = 0;
            _selected_custname = null;

            _cart.Clear();
            gridControl2.DataSource = null;

            RefreshBoardValues();
        }
        public Result SalesValidation()
        {
            Result res = new Result();

            #region Validation

            decimal sales = Static.ToDecimal(numTotal.EditValue);
            decimal nowpaid = Static.ToDecimal(numPaid.EditValue);
            decimal diff = sales - nowpaid;

            if (_cart.Rows.Count <=0 )
            {
                res = new Result(9, "Сагс хоосон байна, борлуулалтаа оруулна уу.");
                goto OnExit;
            }
            if (sales > 0 && nowpaid <= 0)
            {
                res = new Result(9, "Төлөх дүнгээ оруулна уу.");
                goto OnExit;
            }

            #endregion
        OnExit:
            return res;
        }
        public Result SalesPreparation()
        {
            Result res = null;
                        
            #region Prepare parameters
            object[] param = new object[] { 
                _core.TxnDate
                , _core.RemoteObject.User.PosNo
                , _core.RemoteObject.User.AreaCode
                , _salesno
                , _custno
                , _contractno
                , _orderno
                , 0
                , _vat_extend - _vat
                , _pledgeno
                , _cart
                };
            res = new Result();
            res.Param = param;
            #endregion
            
            OnExit:
            return res;
        }
        public Result SalesSend()
        {
            Result res = null;
            try
            {
                #region Prepare sales data
                res = SalesPreparation();
                if (res.ResultNo != 0) goto OnExit;
                object[] paramSales = res.Param;
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605002, 605012, paramSales);
                if (res.ResultNo != 0) goto OnExit;

                #endregion
                #region Clear data

                SalesClear();

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            OnExit:
            //ISM.Template.FormUtility.ValidateQuery(res, success_text);
            return res;
        }

        public void SalesExtendOrCorrectionValue()
        {
            Result res = null;
            string success_text = "";
            try
            {
                #region Validation

                decimal total = Static.ToDecimal(numSales.EditValue);
                decimal remain = Static.ToDecimal(numRemain.EditValue);
                if (total <= 0)
                {
                    res = new Result(9, "Борлуулалт бүрэн буцаагдсан байна.");
                    goto OnExit;
                }
                DataRow r = gridView1.GetFocusedDataRow();
                if (r == null)
                {
                    res = new Result(9, "Сунгалт хийх үйлчилгээ сонгогдоогүй байна.");
                    goto OnExit;
                }
                #endregion

                decimal custno = Static.ToDecimal(r["CUSTNO"]);
                string prodno = Static.ToStr(r["PRODNO"]);
                string prodname = Static.ToStr(r["PRODNAME"]);
                int prodtype = Static.ToInt(r["PRODTYPE"]);
                decimal qty = Static.ToDecimal(r["QTY"]);
                decimal price = Static.ToDecimal(r["PRICE"]);
                decimal discount = Static.ToDecimal(r["DISCOUNT"]);

                frmExtendService frm = new frmExtendService(_core, prodname);
                frm.StartPosition = FormStartPosition.CenterScreen;
                DialogResult dlg = frm.ShowDialog();
                if (dlg != System.Windows.Forms.DialogResult.OK) return;

                decimal extend = frm.Qty;
                decimal discountprod = Static.ToDecimal(r["DISCOUNTPROD"]);

                r["EXTEND"] = extend;
                r["DISCOUNTPROD"] = discountprod + discountprod / qty * extend;
                r["SALESAMOUNT"] = (qty + extend) * price;

                RefreshBoardValues();
                //RefreshExtendValues();

                //success_text = string.Format("Үйлчилгээ сунгалт амжилттай хийгдлээ.");
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        public void SalesExtendTxn()
        {
            Result res = null;
            string success_text = "";
            try
            {
                decimal extended = _cart.AsEnumerable().Sum(x => Static.ToDecimal(x["EXTEND"]));
                if (extended <= 0)
                {
                    Alert("Үйлчилгээний тоо хэмжээгээ сунгаж оруулна уу.", "Үйлчилгээ сунгах");
                    return;
                }
                ExtendPaymentCash();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        public void SalesCorrectionTxn()
        {
            Result res = null;
            string success_text = "";
            try
            {
                decimal extended = _cart.AsEnumerable().Sum(x => Static.ToDecimal(x["EXTEND"]));
                if (extended <= 0)
                {
                    Alert("Засварлах тоо хэмжээгээ оруулна уу.", "Үйлчилгээ засварлах");
                    return;
                }
                ExtendPaymentCash();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }

        public void RefundSales()
        {
            Result res = null;
            string success_text = "";
            try
            {
                #region Validation

                decimal total = Static.ToDecimal(numSales.EditValue);
                decimal remain = Static.ToDecimal(numRemain.EditValue);
                if (total <= 0)
                {
                    res = new Result(9, "Борлуулалт буцаагдсан байна.");
                    goto OnExit;
                }

                #endregion
                frmRefundSales frm = new frmRefundSales(_core, _trandate, _salesno);
                res = frm.Find(_trandate, _salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                DialogResult dlg = frm.ShowDialog();
                if (dlg != System.Windows.Forms.DialogResult.OK) return;

                res = frm._result;
                if (res.ResultNo != 0) goto OnExit;

                string newsalesno = (string)res.Param[0];
                string newpaymentno = (string)res.Param[1];
                success_text = string.Format("Буцаалтын гүйлгээ амжилттай хийгдлээ.\r\nБорлуулалт No : {0}\r\nТөлбөр No     : {1}", newsalesno, newpaymentno);
                SalesClear();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        public void RefundProduct()
        {
            Result res = null;
            string success_text = "";
            try
            {
                #region Validation

                decimal total = Static.ToDecimal(numSales.EditValue);
                decimal remain = Static.ToDecimal(numRemain.EditValue);
                if (total <= 0)
                {
                    res = new Result(9, "Борлуулалт бүрэн буцаагдсан байна.");
                    goto OnExit;
                }
                //if (remain <= 0)
                //{
                //    res = new Result(9, "Дутуу төлбөр алга байна.");
                //    goto OnExit;
                //}
                DataRow r = gridView1.GetFocusedDataRow();
                if (r == null)
                {
                    res = new Result(9, "Буцаалт хийх бүтээгдэхүүн сонгогдоогүй байна.");
                    goto OnExit;
                }
                #endregion
                
                decimal custno = Static.ToDecimal(r["CUSTNO"]);
                string prodno = Static.ToStr(r["PRODNO"]);
                string prodname = Static.ToStr(r["PRODNAME"]);
                int prodtype = Static.ToInt(r["PRODTYPE"]);
                decimal qty = Static.ToDecimal(r["QTY"]);
                decimal price = Static.ToDecimal(r["PRICE"]);
                decimal discount = Static.ToDecimal(r["DISCOUNT"]);

                frmRefundProduct frm = new frmRefundProduct(_core, _salesno, custno, prodno, prodtype, prodname, price, qty, discount);
                frm.Find(_trandate, _salesno);
                DialogResult dlg = frm.ShowDialog();
                if (dlg != System.Windows.Forms.DialogResult.OK) return;

                res = frm._result;
                if (res.ResultNo != 0) goto OnExit;

                string newsalesno = (string)res.Param[0];
                string newpaymentno = (string)res.Param[1];
                success_text = string.Format("Буцаалтын гүйлгээ амжилттай хийгдлээ.\r\nБорлуулалт No : {0}\r\nТөлбөр No     : {1}", newsalesno, newpaymentno);
                SalesClear();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        public void RefundPayment()
        {
            Result res = null;
            string success_text = "";
            try
            {
                frmRefundPayment frm = new frmRefundPayment(_core, _trandate, _salesno);
                res = frm.Find(_trandate, _salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                DialogResult dlg = frm.ShowDialog();
                if (dlg != System.Windows.Forms.DialogResult.OK) return;

                res = frm._result;
                if (res.ResultNo != 0) goto OnExit;

                string newsalesno = (string)res.Param[0];
                string newpaymentno = (string)res.Param[1];
                success_text = string.Format("Буцаалтын гүйлгээ амжилттай хийгдлээ.\r\nБорлуулалт No : {0}\r\nТөлбөр No     : {1}", newsalesno, newpaymentno);
                SalesClear();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }

        public void PaymentForm()
        {
            Result res = null;
            string success_text = "";
            try
            {
                #region Validation

                decimal total = Static.ToDecimal(numSales.EditValue);
                decimal remain = Static.ToDecimal(numRemain.EditValue);
                if (total <= 0)
                {
                    res = new Result(9, "Борлуулалт буцаагдсан байна.");
                    goto OnExit;
                }
                if (remain <= 0)
                {
                    res = new Result(9, "Дутуу төлбөр алга байна.");
                    goto OnExit;
                }

                #endregion
                //res = SalesPreparation();
                //if (res.ResultNo != 0) goto OnExit;

                frmPayment formPayment = new frmPayment(_core, _salesno);
                formPayment.SalesParam = null;
                res = formPayment.Find(_salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                //formPayment.InitValues(
                //    Static.ToDecimal(numTotal.EditValue)
                //    , Static.ToDecimal(numSales.EditValue)
                //    , Static.ToDecimal(numDiscount.EditValue)
                //    , Static.ToDecimal(numVat.EditValue)
                //    , 0
                //    );

                DialogResult dlg = formPayment.ShowDialog();
                if (dlg != System.Windows.Forms.DialogResult.OK) return;

                res = formPayment._result;
                if (res.ResultNo != 0) goto OnExit;

                //string salesno = Static.ToStr(res.Param[0]);
                //string paymentno = Static.ToStr(res.Param[1]);
                string paymentno = res.ResultDesc;
                success_text = string.Format("Төлбөрийн гүйлгээ амжилттай хийгдлээ.\r\nТөлбөр No    : {0}", paymentno);
                SalesClear();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        public void RentForm(string salesno)
        {
            using (_frmInv = new frmSalesInv(_core, salesno))
            {
                _frmInv.StartPosition = FormStartPosition.CenterScreen;
                DialogResult dlg = _frmInv.ShowDialog();
            }
            _frmInv = null;
        }
        public void BillPrint()
        {
            Result res = null;
            try
            {
                #region Validation
                if (string.IsNullOrEmpty(_salesno))
                {
                    res = new Result(9, "Борлуулалт сонгоогдоогүй байна.");
                    goto OnExit;
                }
                #endregion
                
                //_core.Printer.Print(sb.ToString());
                //string filename = string.Format("c:\\Bill-{0}.txt", _salesno);
                //System.IO.File.WriteAllText(filename, sb.ToString());

                string posno = null;
                int shiftno = 0;
                string cashiername = null;

                if (_arraySearchForm != null)
                {
                    posno = Static.ToStr(_arraySearchForm[9]);
                    shiftno = Static.ToInt(_arraySearchForm[14]);
                    cashiername = Static.ToStr(_arraySearchForm[16]);
                }
                
                frmBillShow frm = new frmBillShow(_core);
                frm.StartPosition = FormStartPosition.CenterScreen;
                //frm.BillReprintShow(_salesno, _vat > 0, posno, shiftno, cashiername, _cart, _paymentdetail);
                frm.PrepareBillContents(_salesno);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            Alert(res, "Билл хэвлэх");
            //ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        
        public void ExtendPaymentCash()
        {
            string success_text = null;
            string salesno = null;
            string paymentno = null;

            Result res = SalesValidation();
            if (res.ResultNo != 0) goto OnExit;

            res = SalesPreparation();
            if (res.ResultNo != 0) goto OnExit;
            object[] param = res.Param;

            frmPayment frm = new frmPayment(_core, _salesno);
            frm.TransactionType = frmPayment.SalesTransactionTypes.Extend; //сунгалтын гүйлгээ гэдгийг тэмдэглэх
            frm.SalesParam = res.Param;
            frm.Find("");
            frm.SetPaymentType(0);
            
            frm.InitValues(
                Static.ToDecimal(numTotal.EditValue)
                , Static.ToDecimal(numSales.EditValue)
                , 0
                , Static.ToDecimal(param[8]) /*vat of extended amount*/
                , _prepaid
                );

            DialogResult dlg = frm.ShowDialog();
            if (dlg != System.Windows.Forms.DialogResult.OK) return;

            res = frm._result;
            if (res.ResultNo != 0) goto OnExit;

            salesno = Static.ToStr(res.Param[0]);
            paymentno = Static.ToStr(res.Param[1]);

            _salesno = salesno;

            numExtendAmount.EditValue = 0;

            //BillPrint(_salesno, _vat>0, _cart, frm.PaymentParam);

        OnExit:
            if (ISM.Template.FormUtility.ValidateQuery(res, success_text))
            {
                //Бүх юм амжилттай бол борлуулалтыг шинээр эхлүүлэх
                SalesClear();

                if (!string.IsNullOrEmpty(salesno)) RentForm(salesno);
            }
        }
        public void CorrectionPaymentCash()
        {
            string success_text = null;
            string salesno = null;
            string paymentno = null;

            Result res = SalesValidation();
            if (res.ResultNo != 0) goto OnExit;

            res = SalesPreparation();
            if (res.ResultNo != 0) goto OnExit;
            object[] param = res.Param;

            frmPayment frm = new frmPayment(_core, _salesno);
            frm.TransactionType = frmPayment.SalesTransactionTypes.Correction; //засварын гүйлгээ гэдгийг тэмдэглэх

            frm.SalesParam = res.Param;
            frm.Find("");
            frm.SetPaymentType(0);

            frm.InitValues(
                Static.ToDecimal(numTotal.EditValue)
                , Static.ToDecimal(numSales.EditValue)
                , 0
                , Static.ToDecimal(param[8]) /*vat of extended amount*/
                , _prepaid
                );

            DialogResult dlg = frm.ShowDialog();
            if (dlg != System.Windows.Forms.DialogResult.OK) return;

            res = frm._result;
            if (res.ResultNo != 0) goto OnExit;

            salesno = Static.ToStr(res.Param[0]);
            paymentno = Static.ToStr(res.Param[1]);

            _salesno = salesno;

            numExtendAmount.EditValue = 0;

            //BillPrint(_salesno, _vat>0, _cart, frm.PaymentParam);

        OnExit:
            if (ISM.Template.FormUtility.ValidateQuery(res, success_text))
            {
                //Бүх юм амжилттай бол борлуулалтыг шинээр эхлүүлэх
                SalesClear();

                if (!string.IsNullOrEmpty(salesno)) RentForm(salesno);
            }
        }

        
        static public DataTable MakeGroupTree(DataTable src, string itemid, out int count)
        {
            int cntParent = 0;

            #region Create output table
            DataTable tmp = new DataTable();
            tmp.Columns.Add("LEVEL", typeof(int));
            tmp.Columns.Add("ITEMID", typeof(string));
            tmp.Columns.Add("ITEMNAME", typeof(string));
            #endregion
            #region Collect parent heirarchy groups
            string startwith = itemid;
            while (true)
            {
                var q1 = from row in src.AsEnumerable()
                         where row.Field<string>("ITEMID") == startwith &&
                         row.Field<Int16>("ITEMTYPE") == 3
                         select row;
                if (q1.Count() <= 0) break;

                DataRow r = q1.First();
                tmp.LoadDataRow(new object[] { cntParent++, r["ITEMID"], r["ITEMNAME"] }, true);
                string parent = Static.ToStr(r["PARENTID"]);
                startwith = parent;
                if (parent == null) break;
                if (parent == "ROOT")
                {
                    tmp.LoadDataRow(new object[] { cntParent++, parent, "ЦЭС" }, true);
                }
                //if (parent == null || (startwith = parent) == 0) break;
            }
            #endregion
            count = cntParent;
            return tmp;
        }
        static public DataTable GetGroupTree(DataTable src, string itemid, out int count)
        {
            DataTable dt = MakeGroupTree(src, itemid, out count);
            if (dt != null && dt.Rows.Count > 0)
            {
                var sorted = from row in dt.AsEnumerable()
                             orderby row.Field<int>(0) descending
                             select row;
                sorted.Count();
                dt = sorted.CopyToDataTable();
            }
            return dt;
        }

        public void Alert(Result res, string caption)
        {
            if (res != null && res.ResultNo != 0)
            {
                if (_core != null)
                {
                    _core.AlertShow(caption, res.ResultDesc);
                }
            }
        }
        public void Alert(string text, string caption)
        {
            if (_core != null)
            {
                _core.AlertShow(caption, text);
            }
        }

        #endregion
        #region Constructors
        public frmSalesEdit()
        {
            InitializeComponent();
            #region Events
            this.FormClosing += frmSales_FormClosing;
            this.gridControl1.Resize += gridControl1_Resize;
            this.gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            this.gridView1.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
            this.gridView1.RowCellClick += gridView1_RowCellClick;
            #endregion

            gridControl1.Dock = DockStyle.Fill;
            gridControl2.Dock = DockStyle.Fill;
            splitProd.Dock = DockStyle.Fill;

            tabMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
            gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            gridView2.OptionsBehavior.FocusLeaveOnTab = true;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView2.OptionsSelection.EnableAppearanceHideSelection = false;


            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
        }

        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "PIC")
            {
                SalesExtendOrCorrectionValue();
            }
        }

        void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                try
                {
                    DataRow row = null;
                    switch (e.Column.FieldName)
                    {
                        case "PIC":
                            e.Value = _imgExtend;
                            break;
                    }
                }
                catch
                { }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetFocusedDataRow();
        }
        private void gridControl1_Resize(object sender, EventArgs e)
        {
            gridView1.Columns[3].Width = gridControl1.Width - (50 + 70 + 80 + 10 + 50 + 32);
            gridView1.Columns[5].Width = 80;
            gridView1.Columns[6].Width = 50;
            gridView1.Columns[7].Width = 70;
            gridView1.Columns[7].Width = 70;
            gridView1.Columns["EXTEND"].Width = 50;
            gridView1.Columns["PIC"].Width = 32;
        }

        #endregion
        #region Control events

        private void frmSales_Load(object sender, EventArgs e)
        {
            _imgExtend = _resource.GetImage("object_add");

            _core.MainForm_HeaderClear();
            _core.MainForm_HeaderSet(0, "Харилцагч №", "");
            _core.MainForm_HeaderSet(1, "Овог нэр", "");
            _core.MainForm_HeaderSet(2, "Гэрээ", "");
            _core.MainForm_HeaderSet(3, "Захиалга", "");
            _core.MainForm_HeaderSet(4, "Барьцаа", "");
            _core.MainForm_HeaderSet(5, "Борлуулалт", "");

            InitCart();
            //InitDict();

            gridControl1_Resize(null, null);
        }
        private void frmSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView2, _layoutfilename);
        }
                
        #endregion
              

    }
}
