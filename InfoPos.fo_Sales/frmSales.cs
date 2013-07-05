using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
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
    public partial class frmSales : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        #region Comment!
        /***************************************
         * - Борлуулалт эхлэхийн өмнө үйлчлүүлэгч заавал Бүртгэл дээр
         *   очиж барьцааны бичиг баримтаа тавиж, таг авсан байна.
         * 
         * - Дараах мэдээллүүдийг бөөнд явуулж хадгална.
         *   1. Бараануудын мэдээлэл. _carts тэйблийг явуулах
         *   2. Хамаарах үйлчлүүлэгчдийн мэдээлэл. Сервер дээр барьцааны мэдээлэлд оруулах.
         * 
         * - Таг уншуулахад борлуулалтын мэдээллийг шууд гаргаж харуулах.
         * - Өдрийн төрлөөс хамаарсан үнээр тооцно. ProdPrice тэйбэл
         * - Гэрээнд заагдсан бүтээгдэхүүн бол борлуулалтын үнэд оруулахгүй.
         *   Үүнийг тайлангаар мэдээ гаргаж, гэрээний үлдэгдлийг хорогдуулах бдлаар явна.
         * 
         ***************************************/
        #endregion

        //#region Internal events

        //public event Core.Tag.delegateEventOnCard EventOnCard;

        //#endregion
        #region Internal variables

        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;

        Image _imgHome = null;
        Image _imgFolder = null;
        Image _imgPackage = null;
        Image _imgService = null;
        Image _imgInverntory = null;

        string _contractno = null;
        string _orderno = null;
        string _salesno = null;
        decimal _discountsales = 0;
        bool _isvat = true;
        DateTime _corrtxndate = DateTime.MinValue;
        string _flag = null;

        string _pledgeno = null;
        decimal _custno = 0;  //pledge owner
        string _custname = null;
        string _custregno = null;
        decimal _selected_custno = 0;
        string _selected_custname = null;
        string _selected_custregno = null;

        string _serialno = null;
        string _tagno = null;
        string _batchno = null;


        private string _layoutfilename = "";

        ArrayList _tables = null;

        DataSet _ds = new DataSet();
        DataTable _cart = new DataTable();
        frmSalesInv _frmInv = null;

        /**********************************************
         * Гэрээнд заагдсан бүтээгдэхүүний жагсаалтыг
         * гэрээг сонгох үед авч хадгална. Борлуулалт
         * бараа нь гэрээний бараа бол борлуулалтаар
         * тооцохгүйгээр хийх ёстой.
         **********************************************/
        DataTable _dtContractProd = null; 


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
                    case "fo_sales_exit":
                        item.IsClose = 1;
                        this.Close();
                        break;
                    case "fo_sales_custadd":
                        SubMenu_CustomerAdd();
                        break;
                    case "fo_sales_custdel":
                        SubMenu_CustomerDel();
                        break;
                    case "fo_sales_contract":
                        SubMenu_Contract();
                        break;
                    case "fo_sales_order":
                        SubMenu_Order();
                        break;
                    case "fo_sales_pledge":
                        SubMenu_Pledge();
                        break;
                    case "fo_sales_company":
                        SubMenu_Company();
                        break;

                    case "fo_sales_payment":
                        PaymentForm();
                        break;
                    case "fo_sales_move":
                        SubMenu_Move();
                        break;
                    case "fo_sales_unpack":
                        SubMenu_UnPack();
                        break;
                    case "fo_sales_proddel":
                        SubMenu_ProductDel();
                        break;

                    case "fo_sales_discount":
                        SubMenu_Discount();
                        break;

                    case "fo_sales_correction":
                        SubMenu_Correction();
                        break;

                    case "fo_sales_clear":
                        SubMenu_Clear();
                        break;

                    case "call_tagreader":
                        string tagno = item.Text;
                        Core.TagEventData tagdata = (Core.TagEventData)item.Tag;

                        if (_frmInv != null)
                        {
                            /*****************************************
                             * Хэрэв Түрээсийн хэрэгсэл бичих Форм нээгдсэн
                             * бол уншигдсан тагийн мэдээллийг тийш нь дамжуулна.
                             *****************************************/
                            _frmInv.EventOnCard(tagdata);
                        }
                        else
                        {
                            /*****************************************
                             * Хэрэв борлуулалтын үндсэн Форм дээр таг
                             * уншигдаж бгаа Барьцааны мэдээллээс тагыг
                             * хайж гаргана.
                             *****************************************/
                            SelectPledgeByTag(tagno);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SubMenu_CustomerAdd()
        {
            //InfoPos.fo_Customer.frmCustSearch frm = new fo_Customer.frmCustSearch(_core);
            InfoPos.fo_Customer.frmIndividualSearch frm = new fo_Customer.frmIndividualSearch(_core);
            frm.StartPosition = FormStartPosition.CenterScreen;
            DialogResult res = frm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (frm.Selected)
                {
                    string contractno = Static.ToStr(frm.CurrentRow["MEMBERCONTRACTNO"]);
                    if (string.IsNullOrEmpty(contractno))
                    {
                        CustomerAdd(frm.CustNo, frm.CustName, frm.CustReg);
                    }
                    else
                    {
                        if (_custno == 0)
                        {
                            _custno = frm.CustNo;
                            _custname = frm.CustName;
                        }
                    }

                    _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                    _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                }

            }
            frm.Dispose();
        }
        public void SubMenu_CustomerDel()
        {
            try
            {
                CustomerDel(_selected_custno);
            }
            catch (Exception ex)
            {
            }
        }

        public void SubMenu_Contract()
        {
            try
            {
                bool success = SelectContract();
                if (success)
                {
                    _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                    _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                    _core.MainForm_HeaderSet(2, "Гэрээ", _contractno);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void SubMenu_Order()
        {
            try
            {
                bool success = SelectOrder();
                if (success)
                {
                    _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                    _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                    _core.MainForm_HeaderSet(3, "Захиалга", _orderno);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void SubMenu_Pledge()
        {
            try
            {
                bool success = SelectPledge();
                if (success)
                {
                    _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                    _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                    _core.MainForm_HeaderSet(4, "Барьцаа", _pledgeno);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void SubMenu_Company()
        {
            try
            {
                bool success = SelectCompany();
                if (success)
                {
                    _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                    _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void SubMenu_Move()
        {
            DataRow r = gridView1.GetFocusedDataRow();
            if (r == null) return;

            if (_selected_custno <= 0) return;

            decimal src_custno = Static.ToDecimal(r["CUSTNO"]);
            string prodno = Static.ToStr(r["PRODNO"]);
            string prodname = Static.ToStr(r["PRODNAME"]);
            int prodtype = Static.ToInt(r["PRODTYPE"]);
            decimal qty = Static.ToDecimal(r["QTY"]);

            if (src_custno != _selected_custno)
            {
                string confirm = string.Format("[{0}] Бүтээгдэхүүнийг [{1}] үйлчлүүлэгч рүү шилжүүлэх үү?", prodname, _selected_custname);
                if (ISM.Template.FormUtility.ValidateConfirm(confirm))
                {
                    AddToCart(_selected_custno, _selected_custname, _selected_custregno, prodno, prodtype, qty);
                    r.Delete();
                    r.Table.AcceptChanges();
                }
            }
        }
        public void SubMenu_UnPack()
        {
            /********************************
             * Сонгосон багцыг доторх бараа үйлчилгээгээр
             * нь задлах. Үйлчлүүлэгчид хуваарилах үед
             * хэрэглэгдэнэ.
             ********************************/
            Result res = null;
            DataRow r = gridView1.GetFocusedDataRow();
            #region Validation
            if (r == null || _selected_custno <= 0)
            {
                res = new Result(9, string.Format("Үйлчлүүлэгч болон бүтээгдэхүүн сонгогдоогүй байна."));
                goto OnExit;
            }
            #endregion
            res = UnPackItems(r);

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        public void SubMenu_ProductDel()
        {
            Result res = null;
            DataRow r = gridView1.GetFocusedDataRow();
            #region Validation
            if (r == null)
            {
                res = new Result(9, string.Format("Бүтээгдэхүүн сонгогдоогүй байна."));
                goto OnExit;
            }

            string prodno = Static.ToStr(r["PRODNO"]);
            string prodname = Static.ToStr(r["PRODNAME"]);
            string confirm = string.Format("[{0}-{1}] Барааны бичилтыг бүхэлд нь хасахдаа итгэлтэй байна уу?",prodno, prodname);
            if (!ISM.Template.FormUtility.ValidateConfirm(confirm)) goto OnExit;

            r.Delete();
            RefreshBoardValues();

            #endregion
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }

        public void SubMenu_Discount()
        {
            try
            {
                DiscountCalc(_cart);

                RefreshBoardValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void SubMenu_Correction()
        {
            BrowseSales();
        }

        public void SubMenu_Clear()
        {
            _salesno = null;
            _contractno = null;
            _orderno = null;

            _discountsales = 0;
            _isvat = true;

            _pledgeno = null;
            _custno = 0;  //pledge owner
            _custname = null;
            _custregno = null;
            _selected_custno = 0;
            _selected_custname = null;
            _selected_custregno = null;

            _cart.Rows.Clear();

            GalleryItemGroup group = null;
            if (galleryCust.Gallery.Groups.Count > 0)
            {
                group = galleryCust.Gallery.Groups[0];
                group.Items.Clear();
            }

            chkVat.Checked = _isvat;

            RefreshBoardValues();

            _core.MainForm_HeaderClear();
            _core.MainForm_HeaderSet(0, "Харилцагч №", "");
            _core.MainForm_HeaderSet(1, "Овог нэр", "");
            _core.MainForm_HeaderSet(2, "Гэрээ", "");
            _core.MainForm_HeaderSet(3, "Захиалга", "");
            _core.MainForm_HeaderSet(4, "Барьцаа", "");
        }

        #endregion
        #region Business functions

        #region Init хийх функцүүд
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
            _cart.Columns.Add("SALESPRICE", typeof(decimal));

            _cart.Columns.Add("SALESAMOUNT", typeof(decimal));
            _cart.Columns.Add("DISCOUNTPROD", typeof(decimal));
            _cart.Columns.Add("DISCOUNTSALES", typeof(decimal));

            _cart.Columns.Add("SALETYPE", typeof(int));
            _cart.Columns.Add("PACKID", typeof(string));
            _cart.Columns.Add("SUBTYPE", typeof(string));
            _cart.Columns.Add("CONTRACTNO", typeof(string));
            _cart.Columns.Add("REGNO", typeof(string));
            _cart.Columns.Add("BIRTHDATE", typeof(DateTime));
            
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "SA", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 12, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 13, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 14, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 15, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 16, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 17, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 18, "", true);

            gridView1.Columns[1].GroupIndex = 0;
            gridView1.GroupFormat = "{1}";

            gridView1.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[5].DisplayFormat.FormatString = "#,##0.00";
            gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            //gridView2.OptionsBehavior.FocusLeaveOnTab = true;

            //gridView1.RowHeight = 28;
            //gridView2.RowHeight = 28;

        }
        public Result InitDict()
        {
            string[] dictid = new string[] { "INVMAIN", "SERVMAIN", "PACKMAIN", "PACKITEM", "PRODPRICE", "PACALENDAR" };
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject, dictid, 601003, ref _tables);
            if (res != null && res.ResultNo != 0) goto OnExit;

            //object[] param = new object[] { _core.TxnDate, _core.POSNo, _core.AreaCode };
            //res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605008, 601003, param);
            //if (res != null && res.ResultNo != 0) goto OnExit;

            //_dtProdTree = res.Data.Tables[0];


        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);

            return res;
        }
        public Result InitProd(string groupid)
        {
            Result res = null;
            try
            {
                DataTable dtProd = _core._dtProdInfo;
                DataTable dtItems = null;

                if (dtProd == null || dtProd.Rows.Count <= 0)
                {
                    res = new Result(9, "Бүтээгдэхүүн тохируулагдаагүй байна!");
                    goto OnExit;
                }

                if (string.IsNullOrEmpty(groupid)) groupid = "ROOT";
                #region Тэйблээ шүүж бэлдэх


                var query = from row in dtProd.AsEnumerable()
                            where row.Field<string>("PARENTID") == groupid
                            orderby row["ITEMTYPE"] descending
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dtItems = query.CopyToDataTable();
                }
                #endregion
                #region Галарейд бараануудаа оруулах

                //galleryProd.BeginInit();
                //galleryProd.SuspendLayout();  --Энэ 2 дээр алдаа өгөөд бгаа!

                galleryProd.Gallery.Groups.Clear();
                DevExpress.XtraBars.Ribbon.GalleryItemGroup group = new GalleryItemGroup();
                galleryProd.Gallery.Groups.Add(group);

                #region Дамжсан хавтаснуудаа эхэнд оруулах

                int cnt = 0;
                DataTable folders = GetGroupTree(dtProd, groupid, out cnt);
                if (folders != null && folders.Rows.Count > 0)
                {
                    foreach (DataRow r in folders.Rows)
                    {
                        string parent = Static.ToStr(r["ITEMID"]);
                        string id = Static.ToStr(r["ITEMID"]);
                        string caption = Static.ToStr(r["ITEMNAME"]);
                        int type = 3;

                        GalleryItem item = new GalleryItem();
                        item.Tag = new object[] { type, id };
                        item.Caption = caption;
                        item.Hint = "";
                        item.Checked = true;
                        item.Image = id == "ROOT" ? _imgHome : _imgFolder;

                        group.Items.Add(item);
                    }
                }

                #endregion

                #region Хавтас доторх бараануудаа оруулах
                if (dtItems != null)
                {
                    foreach (DataRow r in dtItems.Rows)
                    {
                        string parent = Static.ToStr(r["PARENTID"]);
                        string id = Static.ToStr(r["ITEMID"]);
                        string caption = Static.ToStr(r["ITEMNAME"]);
                        int type = Static.ToInt(r["ITEMTYPE"]);

                        GalleryItem item = new GalleryItem();
                        item.Tag = new object[] { type, id };
                        item.Caption = caption;
                        item.Hint = "";
                        object pic = null;
                        switch (type)
                        {
                            case 0:
                                //item.Description = "";
                                pic = r["ITEMPICTURE"];
                                if (pic != null && pic is byte[])
                                {
                                    item.Image = Static.ImageFromByte((byte[])pic);
                                }
                                else
                                {
                                    item.Image = _imgInverntory;
                                }
                                break;
                            case 1:
                                //item.Description = "";
                                if (pic != null && pic is byte[])
                                {
                                    item.Image = Static.ImageFromByte((byte[])pic);
                                }
                                else
                                {
                                    item.Image = _imgService;
                                }
                                break;
                            case 2:
                                //item.Description = "";
                                item.Image = _imgPackage;
                                break;
                            case 3:
                                //item.Description = "";
                                item.Image = _imgFolder;
                                break;
                        }
                        group.Items.Add(item);
                    }
                }
                #endregion

                //galleryProd.ResumeLayout(true);
                //galleryProd.EndInit();
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
            return res;
        }
        #endregion
        #region Бараа үйлчлүүлэгч цуглуулах
        public void CustomerAdd(decimal custno, string custname, string register)
        {
            #region Get group
            GalleryItemGroup group = null;
            if (galleryCust.Gallery.Groups.Count > 0)
            {
                group = galleryCust.Gallery.Groups[0];
            }
            else
            {
                group = new GalleryItemGroup();
                galleryCust.Gallery.Groups.Add(group);
            }
            #endregion
            #region Check item is exists
            GalleryItem item = null;
            for (int i = 0; i < group.Items.Count; i++)
            {
                item = group.Items[i];
                if (item.Tag != null)
                {
                    decimal d = Static.ToDecimal(item.Tag);
                    if (custno == d)
                    {
                        return; // already exists
                    }
                }
            }
            #endregion
            #region Add new item
            item = new GalleryItem();
            item.Caption = custname;
            item.Description = register;
            item.Tag = custno;

            group.Items.Add(item);
            #endregion
            if (_custno <= 0)
            {
                //_custno = _selected_custno = custno;
                //_custname = _selected_custname = custname;
                //_custregno = _selected_custregno = register;

                _selected_custno = custno;
                _selected_custname = custname;
                _selected_custregno = register;
            }
        }
        public void CustomerDel(decimal custno)
        {
            #region Get group
            GalleryItemGroup group = null;
            if (galleryCust.Gallery.Groups.Count > 0)
            {
                group = galleryCust.Gallery.Groups[0];
            }
            else
            {
                group = new GalleryItemGroup();
            }
            #endregion
            #region Check item is exists
            for (int i = 0; i < group.Items.Count; i++)
            {
                GalleryItem item = group.Items[0];
                if (item.Tag != null)
                {
                    decimal d = Static.ToDecimal(item.Tag);
                    if (custno == d)
                    {
                        group.Items.Remove(item);
                    }
                }
            }
            #endregion
        }

        public structProdInfo GetProdInfo(string prodno, int type)
        {
            structProdInfo info = new structProdInfo();
            info.found = false;

            try
            {
                #region Бүтээгдэхүүний үндсэн мэдээлэл авах
                if (type == 0)
                {
                    #region Бараа материал
                    DataTable dt = (DataTable)_tables[0];
                    DataRow[] rows = dt.Select(string.Format("INVID='{0}'", prodno));
                    if (rows != null && rows.Length > 0)
                    {
                        info.prodno = prodno;
                        info.prodname = Static.ToStr(rows[0]["NAME"]);
                        info.subtype = Static.ToStr(rows[0]["INVTYPE"]);
                        info.price = Static.ToDecimal(rows[0]["PRICEAMOUNT"]);
                        info.found = true;
                    }
                    #endregion
                }
                else if (type == 1)
                {
                    #region Үйлчилгээ
                    DataTable dt = (DataTable)_tables[1];
                    DataRow[] rows = dt.Select(string.Format("SERVID='{0}'", prodno));
                    if (rows != null && rows.Length > 0)
                    {
                        info.prodno = prodno;
                        info.prodname = Static.ToStr(rows[0]["NAME"]);
                        info.subtype = Static.ToStr(rows[0]["SERVTYPE"]);
                        info.price = Static.ToDecimal(rows[0]["PRICEAMOUNT"]);
                        info.schedule = Static.ToStr(rows[0]["SCHEDULETYPE"]);
                        info.found = true;
                    }
                    #endregion
                }
                else if (type == 2)
                {
                    #region Багц
                    DataTable dt = (DataTable)_tables[2];
                    DataRow[] rows = dt.Select(string.Format("PACKID='{0}'", prodno));
                    if (rows != null && rows.Length > 0)
                    {
                        info.prodno = prodno;
                        info.prodname = Static.ToStr(rows[0]["NAME"]);
                        info.subtype = "";
                        info.price = Static.ToDecimal(rows[0]["PRICE"]);
                        info.found = true;
                    }
                    #endregion
                }
                #endregion
                #region Гэрээнд орсон бараа эсэхийг олох

                if (!string.IsNullOrEmpty(_contractno) && _dtContractProd != null)
                {
                    var query = from r in _dtContractProd.AsEnumerable()
                                where Static.ToStr(r["PRODNO"]) == prodno && Static.ToInt(r["PRODTYPE"]) == type
                                select r;

                    if (query != null && query.Count() > 0)
                    {
                        DataRow row = query.First();
                        info.contractno = _contractno;
                        //info.price = Static.ToDecimal(row["PRICE"]);
                    }
                }

                #endregion
                #region Өдөр гаригаас хамаарсан үнэ олох

                DataTable dtPrice = (DataTable)_tables[4];

                var qprice = from r in dtPrice.AsEnumerable()
                            where Static.ToStr (r["PRODID"]) == prodno 
                            && Static.ToInt(r["PRODTYPE"]) == type 
                            && Static.ToStr(r["DAYTYPEID"]) == _core.DayType
                            && ToTime(r["STARTTIME"]) <= ToTime( DateTime.Now)
                            && ToTime(r["ENDTIME"]) >= ToTime(DateTime.Now)
                            select r;
                if (qprice != null && qprice.Count() > 0)
                {
                    info.price = Static.ToDecimal(qprice.First()["PRICE"]);
                }

                #endregion
            }
            catch
            { }

            return info;
        }
        public void AddToCart(decimal custno, string custname, string regno, string prodno, int type, decimal qty, decimal price = 0, string packid="")
        {
            Result res = null;
            decimal newqty = qty;
            int rowindex = 0;
            DataRow r = null;

            #region Validation1 - Is pledge selected
            //if (string.IsNullOrEmpty(_pledgeno))
            //{
            //    res = new Result(9, "Барьцаа сонгогдоогүй байна!");
            //    goto OnExit;
            //}
            #endregion
            #region Validation2 - Is customer selected
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

            if (price <= 0) price = info.price;
            decimal discountprice = info.price > price ? info.price - price : 0;

            DataRow[] rows = _cart.Select(string.Format("CUSTNO={0} AND PRODNO='{1}' AND PRODTYPE={2} AND SALESPRICE={3}", custno, prodno, type, price));

            if (rows != null && rows.Length > 0)
            {
                r = rows[0];
                rowindex = _cart.Rows.IndexOf(rows[0]);

                #region Бараа байвал тоог нэмэх
                newqty = ((decimal)rows[0]["QTY"]) + qty;
                
                rows[0]["QTY"] = newqty;
                rows[0]["SALESAMOUNT"] = newqty * info.price;
                rows[0]["DISCOUNT"] = newqty * discountprice;

                #endregion
                #region Бараа хасагдаж дууссан бол бичлэгийг хасах
                if (newqty <= 0)
                {
                    //rows[0].Delete();
                    //rows[0].Table.AcceptChanges();
                    //rowindex = _cart.Rows.Count;
                }
                #endregion
            }
            else
            {
                #region Шинэ барааг карт руу нэмэх
                r = _cart.Rows.Add(custno, custname, info.prodno, info.prodname, type
                    , info.price    /*price*/
                    , 1             /*qty*/
                    , discountprice      /*discount*/

                    , info.price    /*unit price*/
                    , price         /*sale price*/
                    , info.price    /*sale amount*/
                    , 0             /*discountprod*/
                    , 0             /*discountsale*/
                    , 0             /*saletype*/
                    , packid        /*packid*/
                    , info.subtype  /*cat*/
                    , info.contractno
                    , regno
                    );
                rowindex = _cart.Rows.Count - 1;
                #endregion
            }
            //CalcRow(r);
            
            gridView1.FocusedRowHandle = gridView1.GetRowHandle(rowindex);
            gridView1.ExpandAllGroups();

            #endregion
            #region Calc total amount

            numQty.EditValue = newqty;
            RefreshBoardValues();

            #endregion

        OnExit:
            Alert(res, "Борлуулалт");
            //ISM.Template.FormUtility.ValidateQuery(res);
        }
        public void CalcTotal()
        {
            /****************************************************
             * Хөнгөлөлтийг тооцоолох.
             * 1. Нэгж бараа бүр дээрх хөнгөлөлт.
             * 2. Нийт үнийн дүнгээс хөнгөлөх хөнгөлөлт
             ****************************************************/
            //decimal discountsales = 0;

            /****************************************************
             * Нийт борлуулалтын TotalSalesAmount дүнг олох.
             * DiscountSales дүнг бараа бүрд жигнэж хуваарилахад 
             * ашиглагдана.
             ****************************************************/
            decimal tsa = _cart.AsEnumerable().Sum(x => Static.ToDecimal(x.Field<object>("SALESAMOUNT")));

            foreach (DataRow r in _cart.Rows)
            {
                decimal up = Static.ToDecimal(r["UNITPRICE"]);
                decimal sp = Static.ToDecimal(r["SALESPRICE"]);

                decimal sa = Static.ToDecimal(r["SALESAMOUNT"]);
                decimal dp = Static.ToDecimal(r["DISCOUNTPROD"]);

                decimal ds = 0; // Math.Ceiling(discountsales * sa / tsa);
                decimal ta = sa - dp - ds;

                r["DISCOUNT"] = (up - sp) + dp;
                r["DISCOUNTPROD"] = 0; //Хөнгөлөлтийн тооцооллоос олж авах.
                r["DISCOUNTSALES"] = ds;
                r["TOTALAMOUNT"] = ta;
            }
        }

        public void RefreshBoardValues()
        {
            try
            {
                #region Хөнгөлөлтийг тооцоолох

                //DiscountCalc(_cart);

                #endregion
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
                                     up = agg.Sum(rr=> Static.ToDecimal(rr["QTY"]) * Static.ToDecimal(rr["UNITPRICE"]))
                                     ,sa= agg.Sum(rr=> Static.ToDecimal(rr["SALESAMOUNT"]))
                                     ,tdp= agg.Sum(rr=> Static.ToDecimal(rr["DISCOUNT"]))
                                     ,dp= agg.Sum(rr=> Static.ToDecimal(rr["DISCOUNTPROD"]))
                                     ,ds= agg.Sum(rr=> Static.ToDecimal(rr["DISCOUNTSALES"]))
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
                //decimal paid = Static.ToDecimal(numPaid.EditValue);  // paid amount

                numSales.EditValue = sa;    // НӨАТ орсон дүнгээрээ
                numDiscount.EditValue = dp; // НӨАТ орсон дүнгээрээ
                numRebate.EditValue = ds;

                if (_isvat)
                {
                    numTotal.EditValue = ta;
                    numVat.EditValue = ta * _core.Vat / 100;
                }
                else
                {
                    numTotal.EditValue = ta * (1 - _core.Vat / 100);
                    numVat.EditValue = -ta * _core.Vat / 100;
                }
                //numDiff.EditValue = ta - paid;

                //if (ta - paid > 0) numDiff.ForeColor = Color.Red;
                //else numDiff.ForeColor = Color.DarkGreen;
                #endregion

                #region Засвар хийх мэдээлэл гаргах
                if (string.IsNullOrEmpty(_salesno))
                {
                    lblCorrTitle.Visible = false;
                    lblCorrSalesNo.Visible = false;
                }
                else
                {
                    lblCorrTitle.Visible = true;
                    lblCorrSalesNo.Visible = true;

                    lblCorrSalesNo.Text = string.Format("Борл. #{0}", _salesno);
                }
                #endregion
            }
            catch
            { }
        }
        #endregion
        #region Туслах хайлтын дэлгэц дуудах функцүүд
        public bool SelectPledgeByTag(string tagno)
        {
            Result res = null;
            bool success = false;
            try
            {
                #region Prepare parameters
                object[] param = new object[] { tagno };
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601012, 605001, param);
                if (res.ResultNo != 0) goto OnExit;

                #endregion

                if (res.AffectedRows > 0)
                {
                    DataTable dt = res.Data.Tables[0];
                    DataRow row = dt.Rows[0];

                    _pledgeno = Static.ToStr(row["pledgeno"]);
                    _custno = Static.ToDecimal(row["custno"]);
                    _custname = Static.ToStr(row["custname"]);

                    _selected_custno = _custno;
                    _selected_custname = _custname;

                    res = ReadCustFromPledge(_pledgeno);
                    if (res.ResultNo == 0)
                    {
                        if (galleryCust.Gallery.Groups.Count > 0 && galleryCust.Gallery.Groups[0].Items.Count > 0)
                        {
                            GalleryItem item = galleryCust.Gallery.Groups[0].Items[0];
                            galleryCust.Gallery.SetItemCheck(item, true);
                        }
                    }

                    _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                    _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                    _core.MainForm_HeaderSet(4, "Барьцаа", _pledgeno);

                    success = true;
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
        public bool SelectPledge()
        {
            Result res = null;
            bool success = false;
            try
            {
                InfoPos.Reg.frmRegSearch frm = new Reg.frmRegSearch(_core);
                frm.radioPledgeType.SelectedIndex = 1;
                frm.radioPledgeType.Properties.Items[0].Enabled = false;
                frm.radioPledgeType.Properties.Items[2].Enabled = false;

                //frmSearchPledge frm = new frmSearchPledge(_core);
                DialogResult dr = frm.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.CurrentRow != null)
                    {
                        _pledgeno = Static.ToStr(frm.CurrentRow["pledgeno"]);
                        //_custno = Static.ToDecimal(frm.CurrentRow["custno"]);
                        //_custname = Static.ToStr(frm.CurrentRow["custname"]);

                        //_selected_custno = _custno;
                        //_selected_custname = _custname;

                        res = ReadCustFromPledge(_pledgeno);
                        if (res.ResultNo == 0)
                        {
                            if (galleryCust.Gallery.Groups.Count > 0 && galleryCust.Gallery.Groups[0].Items.Count > 0)
                            {
                                GalleryItem item = galleryCust.Gallery.Groups[0].Items[0];
                                galleryCust.Gallery.SetItemCheck(item, true);
                            }
                        }


                        success = true;
                    }
                }
                frm.Dispose();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
            return success;
        }
        public bool SelectOrder()
        {
            Result res = null;
            bool success = false;
            try
            {
                #region Validation1 - Is pledge selected

                //if (string.IsNullOrEmpty(_pledgeno))
                //{
                //    res = new Result(9, "Барьцаа сонгогдоогүй байна!");
                //    goto OnExit;
                //}

                #endregion

                InfoPos.Order.frmOrderSearch frm = new Order.frmOrderSearch(_core);
                DialogResult dr = frm.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.Selected)
                    {
                        _orderno = frm.OrderNo;
                        if (_custno <= 0)
                        {
                            //_custno = frm.CustNo;
                            //_custname = frm.CustName;

                            _selected_custno = frm.CustNo;
                            _selected_custname = frm.CustName;

                            CustomerAdd(frm.CustNo, frm.CustName, frm.CustReg);
                        }
                        res = ReadCustFromOrder(_orderno);
                        if (res.ResultNo == 0)
                        {
                            if (galleryCust.Gallery.Groups.Count > 0 && galleryCust.Gallery.Groups[0].Items.Count > 0)
                            {
                                GalleryItem item = galleryCust.Gallery.Groups[0].Items[0];
                                galleryCust.Gallery.SetItemCheck(item, true);
                            }
                        }

                        success = true;
                    }
                }
                frm.Dispose();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

        OnExit:
            Alert(res, "Борлуулалт");
            //ISM.Template.FormUtility.ValidateQuery(res);
            return success;
        }
        public bool SelectContract()
        {
            Result res = null;
            bool success = false;
            try
            {
                InfoPos.Order.frmContractSearch frm = new Order.frmContractSearch(_core);
                DialogResult dr = frm.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.CurrentRow != null)
                    {
                        _contractno = frm.ContractNo;
                        _isvat = frm.IsVat;

                        chkVat.Checked = _isvat;

                        if (_custno <= 0)
                        {
                            _custno = frm.CustNo;
                            _custname = frm.CustName;

                            //_selected_custno = _custno;
                            //_selected_custname = _custname;
                            
                            //string regno = frm.CustReg;
                            //CustomerAdd(_custno, _custname, regno);
                        }
                        res = ReadProdFromContract(_contractno);
                        success = true;
                    }
                }
                frm.Dispose();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
            return success;
        }
        public bool SelectCompany()
        {
            Result res = null;
            bool success = false;
            try
            {
                InfoPos.fo_Customer.frmCompanySearch frm = new fo_Customer.frmCompanySearch(_core);
                frm.StartPosition = FormStartPosition.CenterScreen;
                DialogResult dr = frm.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.Selected && frm.CurrentRow != null)
                    {
                        _custno = Static.ToDecimal(frm.CurrentRow["CUSTOMERNO"]);
                        _custname = Static.ToStr(frm.CurrentRow["CUSTNAME"]);
                        success = true;
                    }
                }
                frm.Dispose();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
            return success;
        }

        #endregion
        #region Туслах хайлтын дэлгэцээр сонгосон бичлэгийн дата авах
        public Result ReadCustFromPledge(string pledgeno)
        {
            Result res = null;
            try
            {
                #region Validation
                #endregion
                #region Prepare parameters
                object[] param = new object[] { pledgeno };
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601011, 605001, param);
                if (res.ResultNo != 0) goto OnExit;

                #endregion
                #region Build customers gallery control
                DataTable dt = res.Data.Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    string custreg = Static.ToStr(r["registerno"]);
                    string custname = Static.ToStr(r["custname"]);
                    decimal custno = Static.ToDecimal(r["custno"]);
                    CustomerAdd(custno, custname, custreg);
                }
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
        public Result ReadCustFromOrder(string orderno)
        {
            Result res = null;
            try
            {
                #region Validation
                #endregion
                #region Prepare parameters
                object[] param = new object[] { orderno };
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 603, 603002, 603001, param);
                if (res.ResultNo != 0) goto OnExit;

                #endregion
                #region Build customers gallery control
                DataTable dt = res.Data.Tables[0];

                GalleryItemGroup group = new GalleryItemGroup();
                foreach (DataRow r in dt.Rows)
                {
                    string custreg = Static.ToStr(r["registerno"]);
                    string custname = Static.ToStr(r["custname"]);
                    decimal custno = Static.ToDecimal(r["custno"]);

                    _selected_custno = _selected_custno<=0 ? custno : 0;
                    _selected_custname= _selected_custno<=0 ? custname : "";
                    _selected_custregno = _selected_custno <= 0 ? custreg : "";

                    CustomerAdd(custno, custname, custreg);
                }
                #endregion

                #region Call server for Products of order
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 603, 603003, 603001, param);
                if (res.ResultNo != 0) goto OnExit;

                #endregion
                #region Build products
                dt = res.Data.Tables[0];
                _cart.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    AddToCart(_selected_custno, _selected_custname, _selected_custregno
                        , Static.ToStr(r["PRODNO"])
                        , Static.ToInt(r["PRODTYPE"])
                        , Static.ToDecimal(r["QTY"]));
                }
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
        public Result ReadProdFromContract(string contractno)
        {
            Result res = null;
            try
            {
                #region Validation
                #endregion
                #region Prepare parameters
                object[] param = new object[] { contractno };
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 604, 604004, 604001, param);
                if (res.ResultNo != 0) goto OnExit;

                _dtContractProd = res.Data.Tables[0];
                RefreshBoardValues();

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
        #endregion
        
        #region Борлуулалт илгээх
        public void SalesClear()
        {
            _salesno = null;
            _contractno = null;
            _orderno = null;
            _pledgeno = null;
            _discountsales = 0;

            _custno = 0;  //pledge owner
            _custname = null;
            _selected_custno = 0;
            _selected_custname = null;

            _cart.Clear();
            galleryCust.Gallery.Groups.Clear();
            RefreshBoardValues();

            //numPaid.EditValue = 0;
        }
        public Result SalesValidation()
        {
            Result res = new Result();

            #region Validation

            decimal sales = Static.ToDecimal(numSales.EditValue);
            //decimal nowpaid = Static.ToDecimal(numPaid.EditValue);
            //decimal diff = sales - nowpaid;

            if (_cart.Rows.Count <= 0)
            {
                res = new Result(9, "Сагс хоосон байна, борлуулалтаа оруулна уу.");
                goto OnExit;
            }
            //if (sales > 0 && nowpaid <= 0)
            //{
            //    res = new Result(9, "Төлөх дүнгээ оруулна уу.");
            //    goto OnExit;
            //}

            #endregion
        OnExit:
            return res;
        }
        public Result SalesPreparation()
        {
            Result res = null;
            _cart.AcceptChanges();

            //decimal vat = Static.ToDecimal(numVat.EditValue);
            //if (vat < 0) vat = 0;
            DateTime txndate = string.IsNullOrEmpty(_salesno) ? _core.TxnDate : _corrtxndate;

            #region Prepare parameters
            object[] param = new object[] { 
                txndate
                , _core.RemoteObject.User.PosNo
                , _core.RemoteObject.User.AreaCode
                , _salesno  /* 1. empty for new sales, 2. corrected salesno for correciton */
                , _custno
                , _contractno
                , _orderno
                , _discountsales
                , Static.ToDecimal (numVat.EditValue)
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
        #endregion
        #region Төлбөрийн функцүүд

        public void PaymentCash()
        {
            string success_text = null;
            string salesno = null;
            string paymentno = null;

            Result res = SalesValidation();
            if (res.ResultNo != 0) goto OnExit;

            res = SalesPreparation();
            if (res.ResultNo != 0) goto OnExit;
            object[] param = res.Param;

            frmPayment frm = new frmPayment(_core, "");
            frm.TransactionType = string.IsNullOrEmpty(_salesno) ? frmPayment.SalesTransactionTypes.Normal : frmPayment.SalesTransactionTypes.Correction;
            frm.SalesParam = res.Param;
            frm.Find("");
            frm.SetPaymentType(0);
            frm.InitValues(
                Static.ToDecimal(numTotal.EditValue)
                , Static.ToDecimal(numSales.EditValue)
                , Static.ToDecimal(numDiscount.EditValue)
                , Static.ToDecimal(numVat.EditValue)
                , 0
                );
            
            DialogResult dlg = frm.ShowDialog();
            if (dlg != System.Windows.Forms.DialogResult.OK) return;

            res = frm._result;
            if (res.ResultNo != 0) goto OnExit;

            salesno = Static.ToStr(res.Param[0]);
            paymentno = Static.ToStr(res.Param[1]);

            _salesno = salesno;

            BillPrint(_salesno, _isvat, _cart, frm.PaymentParam);
            
        OnExit:
            if (ISM.Template.FormUtility.ValidateQuery(res, success_text))
            {
                //Бүх юм амжилттай бол борлуулалтыг шинээр эхлүүлэх
                SalesClear();

                if (!string.IsNullOrEmpty(salesno)) RentForm(salesno);
            }
        }
        public void PaymentCard()
        {
            string success_text = null;
            string salesno = null;
            string paymentno = null;

            Result res = SalesValidation();
            if (res != null && res.ResultNo != 0) goto OnExit;

            res = SalesPreparation();
            if (res != null && res.ResultNo != 0) goto OnExit;
            object[] param = res.Param;

            frmPayment frm = new frmPayment(_core, "");
            frm.TransactionType = string.IsNullOrEmpty(_salesno) ? frmPayment.SalesTransactionTypes.Normal : frmPayment.SalesTransactionTypes.Correction;
            frm.SalesParam = res.Param;
            frm.Find("");
            frm.SetPaymentType(1);
            frm.InitValues(
                Static.ToDecimal(numTotal.EditValue)
                , Static.ToDecimal(numSales.EditValue)
                , Static.ToDecimal(numDiscount.EditValue)
                , Static.ToDecimal(numVat.EditValue)
                , 0
                );

            DialogResult dlg = frm.ShowDialog();
            if (dlg != System.Windows.Forms.DialogResult.OK) return;

            res = frm._result;
            if (res.ResultNo != 0) goto OnExit;

            salesno = Static.ToStr(res.Param[0]);
            paymentno = Static.ToStr(res.Param[1]);
            //success_text = string.Format("Борлуулалт амжилттай хийгдлээ.\r\nБорлуулалт No: {0}\r\nТөлбөр No    : {1}", salesno, paymentno);

            _salesno = salesno;

            BillPrint(_salesno, _isvat, _cart, frm.PaymentParam);

        OnExit:
            if (ISM.Template.FormUtility.ValidateQuery(res, success_text))
            {
                //Бүх юм амжилттай бол борлуулалтыг шинээр эхлүүлэх
                SalesClear();
            }
        }
        public void PaymentForm()
        {
            Result res = null;
            string success_text = "";
            try
            {
                res = SalesValidation();
                if (res != null && res.ResultNo != 0) goto OnExit;

                res = SalesPreparation();
                if (res.ResultNo != 0) goto OnExit;

                frmPayment frm = new frmPayment(_core, "");
                frm.TransactionType = string.IsNullOrEmpty(_salesno) ? frmPayment.SalesTransactionTypes.Normal : frmPayment.SalesTransactionTypes.Correction;
                frm.SalesParam = res.Param;
                frm.Find("");

                frm.InitValues(
                    Static.ToDecimal(numTotal.EditValue)
                    , Static.ToDecimal(numSales.EditValue)
                    , Static.ToDecimal(numDiscount.EditValue)
                    , Static.ToDecimal(numVat.EditValue)
                    , 0
                    );

                DialogResult dlg = frm.ShowDialog();
                if (dlg != System.Windows.Forms.DialogResult.OK) return;

                res = frm._result;
                if (res.ResultNo != 0) goto OnExit;

                string salesno = Static.ToStr(res.Param[0]);
                string paymentno = Static.ToStr(res.Param[1]);
                //success_text = string.Format("Борлуулалт амжилттай хийгдлээ.\r\nБорлуулалт No: {0}\r\nТөлбөр No    : {1}", salesno, paymentno);

                _salesno = salesno;
                
                BillPrint(_salesno, _isvat, _cart, frm.PaymentParam);

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

        #endregion
        #region Багц доторх бараануудыг задлах функц
        public Result UnPackItems(DataRow cartrow)
        {
            Result res = null;
            try
            {
                gridControl1.BeginUpdate();
                gridControl1.SuspendLayout();

                #region Validation
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                #endregion
                #region Prepare cart product info
                DataRow r = cartrow;
                decimal custno = Static.ToDecimal(r["CUSTNO"]);
                string custname = Static.ToStr(r["CUSTNAME"]);
                string packid = Static.ToStr(r["PRODNO"]);
                string prodname = Static.ToStr(r["PRODNAME"]);
                int prodtype = Static.ToInt(r["PRODTYPE"]);
                decimal qty = Static.ToDecimal(r["QTY"]);
                decimal price = Static.ToDecimal(r["PRICE"]);
                decimal discountprod = Static.ToDecimal(r["DISCOUNTPROD"]);
                decimal discountsales = Static.ToDecimal(r["DISCOUNTSALES"]);

                decimal discount = Static.ToDecimal(r["DISCOUNT"]);

                string regno = Static.ToStr(r["REGNO"]);

                #endregion
                #region Багц доторх бараануудыг серверээс авчрах
                if (prodtype != 2)
                {
                    res = new Result(9, "Багц үйлчилгээ биш байна.");
                    goto OnExit;
                }
                object[] param = new object[] { packid };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605005, 605001, param);
                if (res.ResultNo != 0) goto OnExit;
                #endregion
                #region Багцын бараануудыг картруу оруулах
                DataTable dt = res.Data.Tables[0];
                foreach (DataRow i in dt.Rows)
                {
                    string prodno = Static.ToStr(i["PRODNO"]);
                    prodtype = Static.ToInt(i["PRODTYPE"]);
                    price = Static.ToDecimal(i["PRICE"]);
                    decimal q = qty * Static.ToDecimal(i["QTY"]);
                    string subtype = Static.ToStr(i["SUBTYPE"]);

                    AddToCart(custno, custname, regno, packid, prodtype, q, price, prodno);
                }
                #endregion
                #region Сонгогдсон багцын бичлэгийг хасах
                cartrow.Delete();
                #endregion
                res.Data = null;
                res.Param = null;
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            gridControl1.ResumeLayout();
            gridControl1.EndUpdate();

            return res;
        }
        #endregion

        #region НӨАТын Эрх шалгах функц

        public Result CheckPriv()
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605019, 605019, null);
            return res;
        }

        #endregion

        #region Модны мөчир дэх барааны жагсаалт буцаах функц
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
                    tmp.LoadDataRow(new object[] { cntParent++, parent, "ҮНДСЭН ЦЭС" }, true);
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
        #endregion
        #region Convertion functions
        static public TimeSpan ToTime(object datetimevalue)
        {
            DateTime time = Static.ToDateTime(datetimevalue );
            TimeSpan ts = new TimeSpan(time.Hour, time.Minute, time.Second);
            return ts;
        }
        #endregion

        #endregion
        #region Discount functions

        public ArrayList DiscountGetCustSales(DataTable cartTable)
        {
            /*********************************************************
             * Борлуулалтын тэйблийг үйлчлүүлэгч бүрээр JSON обект болгох.
             *********************************************************/

            ArrayList cust_all = new ArrayList();

            #region Үйлчлүүлэгч бүрээр бүлэглэх
            var query = from p in cartTable.AsEnumerable()
                        group p by p["CUSTNO"] into g
                        select new
                        {
                            CustNo = g.Key,
                            CustName = g.Max(p => Static.ToStr(p["CUSTNAME"])),
                            CustYear = g.Max(p => Static.ToInt(Static.SubStr(Static.ToStr(p["REGNO"]) + "00000000", 2, 2))),
                            CustMonth = g.Max(p => Static.ToInt(Static.SubStr(Static.ToStr(p["REGNO"]) + "00000000", 4, 2))),
                            CustDay = g.Max(p => Static.ToInt(Static.SubStr(Static.ToStr(p["REGNO"]) + "00000000", 6, 2))),
                            TotalSales = g.Sum(p => Static.ToDecimal(p["SALESAMOUNT"])),
                            Products = g
                        };
            #endregion
            #region Бүлэг доторх барааны жагсаалт
            if (query != null && query.Count() > 0)
            {
                foreach (var v in query)
                {
                    Hashtable cust = new Hashtable();
                    Core.Core.JPathSet(cust, @"id", "new");
                    Core.Core.JPathSet(cust, @"cid", v.CustNo);
                    Core.Core.JPathSet(cust, @"condition\sales_date", DateTime.Today.ToString("yyyy/MM/dd"));
                    Core.Core.JPathSet(cust, @"condition\sales_hour", DateTime.Now.Hour);
                    Core.Core.JPathSet(cust, @"condition\sales_week", (int)DateTime.Today.DayOfWeek);
                    Core.Core.JPathSet(cust, @"condition\sales_amt", v.TotalSales);
                    Core.Core.JPathSet(cust, @"condition\score_amt", 0);
                    Core.Core.JPathSet(cust, @"condition\cust_type", "");
                    Core.Core.JPathSet(cust, @"condition\cust_year", v.CustYear + (v.CustMonth > 20 ? 2000 : 1900));
                    Core.Core.JPathSet(cust, @"condition\cust_month", v.CustMonth > 20 ? v.CustMonth - 10 : v.CustMonth);
                    Core.Core.JPathSet(cust, @"condition\cust_day", v.CustDay);
                    Core.Core.JPathSet(cust, @"condition\cust_name", v.CustName);

                    #region Үйлчлүүлэгч доторх барааны

                    ArrayList custprods = new ArrayList();
                    foreach (DataRow r in v.Products)
                    {
                        //{"id":"22","cat":"91","pck":"p1","qty":3,"up":23000,"sp":21000}
                        Hashtable p = new Hashtable();
                        p["id"] = r["PRODNO"];
                        p["cat"] = r["SUBTYPE"];
                        p["pck"] = r["PACKID"];
                        p["qty"] = r["QTY"];
                        p["up"] = r["UNITPRICE"];
                        p["sp"] = r["SALESPRICE"];
                        custprods.Add(p);

                        r["DISCOUNT"] = 0;
                        r["DISCOUNTPROD"] = 0;
                        r["DISCOUNTSALES"] = 0;
                    }

                    #endregion

                    Core.Core.JPathSet(cust, @"condition\sales_list", custprods);

                    cust_all.Add(cust);
                }
            }
            //foreach(var v in query)
            #endregion
            return cust_all;
        }
        public void DiscountCalc_bak(DataTable cart)
        {
            #region Багц доторх барааг задалж оруулах

            /***************************************************
             * Анхнаас нь задалж оруулах хэрэгтэй юм бн даа. 
             * Тэгж байж багц доторх бараанд хөнгөлөлт тооцож чадна!
             ***************************************************/


            #endregion

            /***************************************************
             * Тухайн борлуулалтын бүх дүнг гаргах. Үүнийг 
             * борлуулалтын дүнгээс тооцох хөнгөлөлтийн дүнг 
             * бараа бүрд ноогдуулж хуваарилахад хэрэглэнэ.
             ***************************************************/
            decimal tsa = cart.AsEnumerable().Sum(x => Static.ToDecimal(x["SALESAMOUNT"]));


            // Үйлчлүүлэгч бүрээр барааны жагсаалт гарч ирнэ.
            ArrayList arr = DiscountGetCustSales(cart);
            for (int i = 0; i < arr.Count; i++)
            {
                /***************************************************
                 * Тухайн үйлчүүлэгчийн хувьд үндсэн дүнгээс тооцох
                 * хамгийн их хөнгөлөлтийн хувь/дүнг хадгалана.
                 ***************************************************/
                decimal max_discount = 0;

                Hashtable cust = (Hashtable)arr[i];

                string custno = Core.Core.JPathGetStr(cust, @"cid");
                DateTime sales_date = Core.Core.JPathGetDate(cust, @"condition\sales_date");
                double sales_hour = Core.Core.JPathGetDbl(cust, @"condition\sales_hour");
                double sales_week = Core.Core.JPathGetDbl(cust, @"condition\sales_week");
                double sales_amt = Core.Core.JPathGetDbl(cust, @"condition\sales_amt");
                double score_amt = Core.Core.JPathGetDbl(cust, @"condition\score_amt");
                string cust_type = Core.Core.JPathGetStr(cust, @"condition\cust_type");
                double cust_year = Core.Core.JPathGetDbl(cust, @"condition\cust_year");
                double cust_month = Core.Core.JPathGetDbl(cust, @"condition\cust_month");
                double cust_day = Core.Core.JPathGetDbl(cust, @"condition\cust_day");
                string cust_name = Core.Core.JPathGetStr(cust, @"condition\cust_name");

                ArrayList cust_plist = (ArrayList)Core.Core.JPathGet(cust, @"condition\sales_list");

                //Бүх хөнгөлөлтөөр гүйнэ.
                for (int j = 0; j < _core._Discounts.Count; j++)
                {
                    Hashtable _d = (Hashtable)_core._Discounts.GetByIndex(j);

                    #region Хөнгөлөлтийн үндсэн нөхцөл шалгах
                    /***************************************************
                     * Хөнгөлөлтийн үндсэн нөхцлүүдийг шалгах
                     ***************************************************/
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\sales_date"), sales_date)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\sales_hour"), sales_hour)) goto OnNext;
                    if (!_in_((ArrayList)Core.Core.JPathGet(_d, @"condition\sales_week"), sales_week)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\sales_amt"), sales_amt)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\score_amt"), score_amt)) goto OnNext;
                    if (!_in_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_type"), cust_type)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_year"), cust_year)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_month"), cust_month)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_day"), cust_day)) goto OnNext;
                    if (!_contain_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_name"), cust_name)) goto OnNext;
                    #endregion
                    #region Нөхцөлт бараанууд агуулагдаж буй эсхийг шалгах
                    /***************************************************
                     * Хөнгөлөлтийн нөхцөл тооцох бараанууд агуулагдаж буй эсхийг шалгах.
                     ***************************************************/
                    ArrayList csl = (ArrayList)Core.Core.JPathGet(_d, @"condition\sales_list");
                    if (csl != null)
                    {
                        for (int k = 0; k < csl.Count; k++)
                        {
                            Hashtable sales_row = (Hashtable)csl[k];
                            object value = Core.Core.JPathGet(sales_row, @"id");
                            if (!_in_(cust_plist, @"id", value)) goto OnNext;
                        }
                    }
                    #endregion

                    #region Хөнгөлөлт тооцоолох

                    /***************************************************
                     * Шалгах бүх нөхцөл тохирсон бол ийшээ орж ирнэ.
                     * Хөнгөлөлт тооцоолох процесс.
                     ***************************************************/

                    int calc_level = Core.Core.JPathGetInt(_d, @"method\calc_level");
                    int disc_prc = Core.Core.JPathGetInt(_d, @"method\disc_prc");
                    decimal disc_amt = Core.Core.JPathGetDec(_d, @"method\disc_amt");

                    int score_dest = Core.Core.JPathGetInt(_d, @"method\score_dest");
                    int score_add = Core.Core.JPathGetInt(_d, @"method\score_add");
                    int score_flag = Core.Core.JPathGetInt(_d, @"method\score_flag");

                    ArrayList msl = (ArrayList)Core.Core.JPathGet(_d, @"method\sales_list");

                    #region Нийт дүнгээс тооцох хөнгөлөлтийн дүнг олох
                    decimal disc = disc_amt + (decimal)sales_amt * disc_prc / 100;
                    if (disc > max_discount) max_discount = disc;
                    #endregion


                    /***********************************************
                     * Тухайн үйлчлүүлэгчийн сагсан доторх бүх
                     * бараагаар гүйж хөнгөлтөнд орж буйг шалгах
                     ***********************************************/
                    DataRow[] rows = cart.Select(string.Format("CUSTNO='{0}'", custno));
                    if (rows != null && rows.Length > 0)
                    {
                        foreach (DataRow r in rows)
                        {
                            decimal sa = Static.ToDecimal(r["SALESAMOUNT"]);
                            string prodno = Static.ToStr(r["PRODNO"]);
                            string contractno = Static.ToStr(r["CONTRACTNO"]);
                            string packid = Static.ToStr(r["PACKID"]); // Багц доторх барааг задалж оруулж ирэх!!!
                            decimal qty = Static.ToDecimal(r["QTY"]);
                            decimal dp = Static.ToDecimal(r["DISCOUNTPROD"]);
                            decimal discount = Static.ToDecimal(r["DISCOUNT"]);

                            decimal price = Static.ToDecimal(r["PRICE"]);
                            decimal uprice = Static.ToDecimal(r["UNITPRICE"]);

                            // Нийт дүнгээс тооцох хөнгөллтийг бараа бүрт шингээх
                            decimal dp1 = 0;
                            decimal ds1 = Math.Round(disc * sa / tsa, 0);

                            // 1. Бараанд 1 л удаа хөнгөлөлт тооцно.
                            // 2. Гэрээгээр орж ирсэн барааны хувьд хөнгөлөлт тооцохгүй.
                            if (dp == 0 && string.IsNullOrEmpty(contractno))
                            {
                                if (msl != null)
                                {
                                    //QTY ширхэгийг агуулж бгаа хөнгөлөлтийг олох
                                    Hashtable h = _qty_(msl, prodno, qty);
                                    if (h != null)
                                    {
                                        decimal dpa = Core.Core.JPathGetDec(h, "val");
                                        string flg = Core.Core.JPathGetStr(h, "flg");

                                        switch (flg)
                                        {
                                            case "P":
                                                dp1 = sa * dpa / 100; //хувиар хөнгөлөх
                                                break;
                                            case "A":
                                                dp1 = dpa; //шууд дүнгээр хөнгөлөх
                                                break;
                                            case "Q":
                                                dp1 = dpa * price; //тоо ширхэгээр нь хөнгөлөх
                                                break;
                                        }
                                    }
                                } //if msl
                            } // if dp

                            r["DISCOUNTPROD"] = dp1;
                            r["DISCOUNTSALES"] = ds1;

                            decimal pack_discount = uprice - price;
                            if (pack_discount > 0) pack_discount = 0;

                            r["DISCOUNT"] = dp1 + qty * pack_discount; //багцын хөнгөлөлт дээр нэмэх нь барааны хөнгөлөлт

                        } //foreach
                    }

                    /*******************************************
                     * нэг бараан дээр 1 л удаа хөнгөлөлт тооцно.
                     *******************************************/
                    //goto OnNextCustomer;

                    #endregion

                OnNext:
                    ;
                } //for discounts

            OnNextCustomer:
                ;
            }
        }
        public void DiscountCalc(DataTable cart)
        {
            /***************************************************
             * Сагсны бараа бүр дээр таарсан хөнгөлөлтийн мэдээллийг
             * энд цуглуулна.
             ***************************************************/
            Hashtable _discounts = new Hashtable();
            Hashtable _colision = new Hashtable();

            #region Багц доторх барааг задалж оруулах

            /***************************************************
             * Анхнаас нь задалж оруулах хэрэгтэй юм бн даа. 
             * Тэгж байж багц доторх бараанд хөнгөлөлт тооцож чадна!
             ***************************************************/


            #endregion

            /***************************************************
             * Тухайн борлуулалтын бүх дүнг гаргах. Үүнийг 
             * борлуулалтын дүнгээс тооцох хөнгөлөлтийн дүнг 
             * бараа бүрд ноогдуулж хуваарилахад хэрэглэнэ.
             * 
             * Сагсны барааг циклэдэж борлуулалтын хөнгөлөлт тооцох үед
             * тухайн бараанд ноогдвор утгыг олгох юм.
             ***************************************************/
            decimal tsa = cart.AsEnumerable().Sum(x => Static.ToDecimal(x["SALESAMOUNT"]));


            // Үйлчлүүлэгч бүрээр барааны жагсаалт гарч ирнэ.
            ArrayList arr = DiscountGetCustSales(cart);
            for (int i = 0; i < arr.Count; i++)
            {
                /***************************************************
                 * Тухайн үйлчүүлэгчийн хувьд үндсэн дүнгээс тооцох
                 * хамгийн их хөнгөлөлтийн хувь/дүнг хадгалана.
                 ***************************************************/
                decimal max_discount = 0;

                #region Үйлчлүүлэгчийн борлуулалтын үндсэн нөхцлийг авах
                Hashtable cust = (Hashtable)arr[i];
                string custno = Core.Core.JPathGetStr(cust, @"cid");
                DateTime sales_date = Core.Core.JPathGetDate(cust, @"condition\sales_date");
                double sales_hour = Core.Core.JPathGetDbl(cust, @"condition\sales_hour");
                double sales_week = Core.Core.JPathGetDbl(cust, @"condition\sales_week");
                double sales_amt = Core.Core.JPathGetDbl(cust, @"condition\sales_amt");
                double score_amt = Core.Core.JPathGetDbl(cust, @"condition\score_amt");
                string cust_type = Core.Core.JPathGetStr(cust, @"condition\cust_type");
                double cust_year = Core.Core.JPathGetDbl(cust, @"condition\cust_year");
                double cust_month = Core.Core.JPathGetDbl(cust, @"condition\cust_month");
                double cust_day = Core.Core.JPathGetDbl(cust, @"condition\cust_day");
                string cust_name = Core.Core.JPathGetStr(cust, @"condition\cust_name");
                ArrayList cust_plist = (ArrayList)Core.Core.JPathGet(cust, @"condition\sales_list");
                #endregion

                /***************************************************
                 * Системийн бүх хөнгөлөлтийг шалгана.
                 ***************************************************/
                for (int j = 0; j < _core._Discounts.Count; j++)
                {
                    Hashtable _d = (Hashtable)_core._Discounts.GetByIndex(j);

                    string disc_id = Core.Core.JPathGetStr(_d, @"id");
                    string disc_name = Core.Core.JPathGetStr(_d, @"desc");

                    #region Хөнгөлөлтийн үндсэн нөхцөл шалгах
                    /***************************************************
                     * Хөнгөлөлтийн үндсэн нөхцлүүдийг шалгах
                     ***************************************************/
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\sales_date"), sales_date)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\sales_hour"), sales_hour)) goto OnNext;
                    if (!_in_((ArrayList)Core.Core.JPathGet(_d, @"condition\sales_week"), sales_week)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\sales_amt"), sales_amt)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\score_amt"), score_amt)) goto OnNext;
                    if (!_in_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_type"), cust_type)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_year"), cust_year)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_month"), cust_month)) goto OnNext;
                    if (!_between_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_day"), cust_day)) goto OnNext;
                    if (!_contain_((ArrayList)Core.Core.JPathGet(_d, @"condition\cust_name"), cust_name)) goto OnNext;
                    #endregion
                    #region Нөхцөлт бараанууд агуулагдаж буй эсхийг шалгах
                    /***************************************************
                     * Хөнгөлөлтийн нөхцөл тооцох бараанууд агуулагдаж буй эсхийг шалгах.
                     ***************************************************/
                    ArrayList csl = (ArrayList)Core.Core.JPathGet(_d, @"condition\sales_list");
                    if (csl != null)
                    {
                        for (int k = 0; k < csl.Count; k++)
                        {
                            Hashtable sales_row = (Hashtable)csl[k];
                            object value = Core.Core.JPathGet(sales_row, @"id");
                            if (!_in_(cust_plist, @"id", value)) goto OnNext;
                        }
                    }
                    #endregion

                    /***************************************************
                     * Шалгах бүх нөхцөл тохирсон бол ийшээ орж ирнэ.
                     * Хөнгөлөлт тооцоолох процесс.
                     ***************************************************/
                    #region Шалгарсан хөнгөлөлтийн мэдээллийг авах

                    int calc_level = Core.Core.JPathGetInt(_d, @"method\calc_level");
                    int disc_prc = Core.Core.JPathGetInt(_d, @"method\disc_prc");
                    decimal disc_amt = Core.Core.JPathGetDec(_d, @"method\disc_amt");

                    int score_dest = Core.Core.JPathGetInt(_d, @"method\score_dest");
                    int score_add = Core.Core.JPathGetInt(_d, @"method\score_add");
                    int score_flag = Core.Core.JPathGetInt(_d, @"method\score_flag");

                    ArrayList msl = (ArrayList)Core.Core.JPathGet(_d, @"method\sales_list");

                    #endregion
                    #region Нийт дүнгээс тооцох хөнгөлөлтийн дүнг олох
                    decimal disc = disc_amt + (decimal)sales_amt * disc_prc / 100;
                    if (disc > max_discount) max_discount = disc;
                    #endregion
                    
                    /***********************************************
                     * Тухайн үйлчлүүлэгчийн сагсан доторх бүх
                     * бараагаар гүйж хөнгөлтөнд орж буйг шалгах
                     ***********************************************/
                    DataRow[] rows = cart.Select(string.Format("CUSTNO='{0}'", custno));
                    if (rows != null && rows.Length > 0)
                    {
                        foreach (DataRow r in rows)
                        {
                            #region Сагсны борлуулалтын бичлэгийн утгуудыг авах
                            decimal sa = Static.ToDecimal(r["SALESAMOUNT"]);
                            string prodno = Static.ToStr(r["PRODNO"]);
                            string prodname = Static.ToStr(r["PRODNAME"]);
                            string contractno = Static.ToStr(r["CONTRACTNO"]);
                            string packid = Static.ToStr(r["PACKID"]); // Багц доторх барааг задалж оруулж ирэх эсэх!!!
                            decimal qty = Static.ToDecimal(r["QTY"]);
                            decimal dp = Static.ToDecimal(r["DISCOUNTPROD"]);
                            decimal discount = Static.ToDecimal(r["DISCOUNT"]);

                            decimal price = Static.ToDecimal(r["PRICE"]);
                            decimal uprice = Static.ToDecimal(r["UNITPRICE"]);
                            #endregion

                            // Нийт дүнгээс тооцох хөнгөллтийг бараа бүрт шингээх
                            decimal dp1 = 0;
                            decimal ds1 = Math.Round(disc * sa / tsa, 0);

                            // Гэрээгээр орж ирсэн барааны хувьд хөнгөлөлт тооцохгүй.
                            if (string.IsNullOrEmpty(contractno))
                            {
                                if (msl != null)
                                {
                                    //QTY ширхэгийг агуулж бгаа хөнгөлөлтийг олох
                                    Hashtable h = _qty_(msl, prodno, qty);
                                    if (h != null)
                                    {
                                        #region Бараанын хөнгөлөлтийн дүнг бодох
                                        decimal dpv = Core.Core.JPathGetDec(h, "val");
                                        string dpf = Core.Core.JPathGetStr(h, "flg");

                                        switch (dpf)
                                        {
                                            case "P":
                                                dp1 = sa * dpv / 100; //хувиар хөнгөлөх
                                                break;
                                            case "A":
                                                dp1 = dpv; //шууд дүнгээр хөнгөлөх
                                                break;
                                            case "Q":
                                                dp1 = dpv * price; //тоо ширхэгээр нь хөнгөлөх
                                                break;
                                        }
                                        #endregion
                                        #region Бараанд таарсан хөнгөлөлтийг бүртгэх
                                        string discount_key = string.Format("{0}_{1}", custno, disc_id);
                                        RowDiscountInfo di = (RowDiscountInfo)_discounts[discount_key];
                                        if (di == null)
                                        {
                                            di = new RowDiscountInfo();
                                            di.d = _d;
                                            di.custno = Static.ToDecimal(custno);
                                            di.custname = cust_name;

                                            _discounts.Add(discount_key, di);
                                        }
                                        di.rows.Add(r);  //Хөнгөлөлт тооцсон мөрийг хадгалах
                                        di.amt.Add(dp1); //Харгалзах хөнгөлөлтийн дүнг хадгалах
                                        di.total += dp1;
                                        #endregion
                                        #region Барааны түвшинд давхцаж бгаа хөнгөлөлтүүдийг бүртгэх

                                        string colision_key = string.Format("c{0}_p{1}", custno, prodno);
                                        object colision_obj = _colision[colision_key];
                                        int colision_count = 0;
                                        if (colision_obj != null)
                                        {
                                            colision_count = (int)colision_obj;
                                        }
                                        colision_count++;
                                        _colision[colision_key] = colision_count;

                                        #endregion

                                        #region Давхар хөнгөлөлт бгаа бол нүдний өнгийг өөр болгох

                                        string msg = string.Format("'{0}' бараанд '{1}' хөнгөлөлт тооцогдлоо...", prodname, disc_name);
                                        Alert(msg, "Хөнгөлөлт", 0);

                                        #endregion
                                    }
                                } //if msl
                            } // if dp

                            decimal pack_discount = uprice - price;
                            if (pack_discount < 0) pack_discount = 0;

                            r["DISCOUNT"] = qty * pack_discount + dp1; //багцын хөнгөлөлт дээр нэмэх нь барааны хөнгөлөлт
                            r["DISCOUNTSALES"] = ds1;
                            r["DISCOUNTPROD"] = dp1;
                            
                        } //foreach
                    }

                    /*******************************************
                     * нэг бараан дээр 1 л удаа хөнгөлөлт тооцно.
                     *******************************************/
                //goto OnNextCustomer;

                    //#endregion

                OnNext:
                    ;
                } //for discounts

            OnNextCustomer:
                ;
            }

            if (_discounts.Count > 0)
            {
                #region Сонголт дэлгэцийг үүсгэх
                frmDiscountChoice frm = new frmDiscountChoice();
                frm.StartPosition = FormStartPosition.CenterScreen;
                bool needtochoice = false;
                #endregion
                #region Сонголт хийлгэх хөнгөлөлтийн мэдээллийг бэлтгэх
                ICollection keys = _discounts.Keys;
                foreach (string key in keys)
                {
                    Static.WriteToLogFile("ipos_debug_discount.txt", "*******************************************************");
                    Static.WriteToLogFile("ipos_debug_discount.txt", string.Format("{0}", key));

                    RowDiscountInfo di = (RowDiscountInfo)_discounts[key];
                    string did = Core.Core.JPathGetStr(di.d, @"id");
                    string ddesc = Core.Core.JPathGetStr(di.d, @"desc");
                    
                    if (di != null /*&& di.rows.Count > 1*/)
                    {
                        for (int ii = 0; ii < di.rows.Count; ii++ )
                        {
                            DataRow r = di.rows[ii];
                            string prodno = Static.ToStr(r["PRODNO"]);
                            string prodname = Static.ToStr(r["PRODNAME"]);

                            Static.WriteToLogFile("ipos_debug_discount.txt", string.Format("   {0}-{1}, {2}", prodno, prodname, di.amt[ii]));

                            string colision_key = string.Format("c{0}_p{1}", di.custno, prodno);
                            int colision_count = Static.ToInt(_colision[colision_key]);
                            if (colision_count > 1)
                            {
                                string choice_desc = string.Format("{0}, {1} төг, {2} бараа", ddesc, di.total.ToString("#,##0"), di.rows.Count);
                                frm.Add(di.custno, di.custname, "", "", did, choice_desc);
                                needtochoice = true;
                            }
                        }
                    }
                    Static.WriteToLogFile("ipos_debug_discount.txt", "*******************************************************");
                }
                #endregion
                #region Сонголт хийх хөнгөлөлт байгаа бол сонголт хийх дэлгэц дуудах
                if (needtochoice)
                {
                    DialogResult dlg = frm.ShowDialog();
                    if (dlg == System.Windows.Forms.DialogResult.OK)
                    {
                        foreach (DataRow r in frm._cart.Rows)
                        {
                            bool selected = Static.ToBool(r["CHECKED"]);
                            string custno = Static.ToStr(r["CUSTNO"]);
                            string did = Static.ToStr(r["ITEMKEY"]);

                            if (selected)
                            {
                                string discount_key = string.Format("{0}_{1}", custno, did);
                                RowDiscountInfo di = (RowDiscountInfo)_discounts[discount_key];
                                if (di != null)
                                {
                                    di.selected = true;
                                }
                            }
                        }
                    }
                    frm.Dispose();
                }
                #endregion
                #region Хөнгөлөлтүүдийг зоох

                foreach (RowDiscountInfo di in _discounts.Values)
                {
                    if (di != null)
                    {
                        for (int i = 0; i < di.rows.Count; i++)
                        {
                            DataRow rr = di.rows[i];
                            decimal d = Static.ToDecimal(rr["DISCOUNT"]);
                            decimal price = Static.ToDecimal(rr["PRICE"]);
                            decimal uprice = Static.ToDecimal(rr["UNITPRICE"]);
                            decimal qty = Static.ToDecimal(rr["QTY"]);

                            decimal pack_discount = uprice - price;
                            if (pack_discount < 0) pack_discount = 0;

                            if (di.rows.Count == 1 || di.selected)
                            {
                                rr["DISCOUNT"] = qty * pack_discount + di.amt[i];
                                rr["DISCOUNTPROD"] = di.amt[i];
                            }
                            else
                            {
                                rr["DISCOUNT"] = qty * pack_discount;
                                rr["DISCOUNTPROD"] = 0;
                            }
                        }
                    }
                }

                #endregion
            }

            _discounts.Clear();
            _colision.Clear();

            RefreshBoardValues();
        }

        #region Discount condition operands

        private bool _between_(ArrayList list, int value)
        {
            if (list == null || list.Count == 0) return true;
            int v1 = Static.ToInt(list[0]);
            int v2 = v1;
            if (list.Count >= 2) v2 = Static.ToInt(list[1]);
            return (value >= v1 && value <= v2);
        }
        private bool _between_(ArrayList list, double value)
        {
            if (list == null || list.Count == 0) return true;
            double v1 = Static.ToDouble(list[0]);
            double v2 = v1;
            if (list.Count >= 2) v2 = Static.ToDouble(list[1]);
            return (value >= v1 && value <= v2);
        }
        private bool _between_(ArrayList list, decimal value)
        {
            if (list == null || list.Count == 0) return true;
            decimal v1 = Static.ToDecimal(list[0]);
            decimal v2 = v1;
            if (list.Count >= 2) v2 = Static.ToDecimal(list[1]);
            return (value >= v1 && value <= v2);
        }
        private bool _between_(ArrayList list, DateTime value)
        {
            if (list == null || list.Count == 0) return true;
            DateTime v1 = Static.ToDate(list[0]);
            DateTime v2 = v1;
            if (list.Count >= 2) v2 = Static.ToDate(list[1]);
            return (value >= v1 && value <= v2);
        }

        private bool _contain_(ArrayList list, string value)
        {
            if (list == null || list.Count == 0) return true;
            for (int i = 0; i < list.Count; i++)
            {
                string s = Static.ToStr(list[i]);
                if (value.Contains(s)) return true;
            }
            return false;
        }

        private bool _in_(ArrayList list, object value)
        {
            if (list == null || list.Count == 0) return true;
            return list.Contains(value);
        }
        private bool _in_(ArrayList jlist, string jkey, object value)
        {
            if (jlist == null || jlist.Count <= 0) return true;

            bool success = false;
            for (int i = 0; i < jlist.Count; i++)
            {
                object jobj = jlist[i];
                if (jobj != null && jobj is Hashtable)
                {
                    Hashtable json = (Hashtable)jlist[i];
                    object jvalue = Core.Core.JPathGet(json, jkey);
                    if (jvalue != null && jvalue.Equals(value)) return true;
                }
            }
            return success;
        }
        private object _exist_(ArrayList jlist, string jkey, object value)
        {
            if (jlist == null || jlist.Count <= 0) return null;

            for (int i = 0; i < jlist.Count; i++)
            {
                object jobj = jlist[i];
                if (jobj != null && jobj is Hashtable)
                {
                    Hashtable json = (Hashtable)jlist[i];
                    object jvalue = Core.Core.JPathGet(json, jkey);
                    if (jvalue != null && jvalue.Equals(value)) return json;
                }
            }
            return null;
        }

        private Hashtable _qty_(ArrayList jlist, string id, decimal qty)
        {
            //Өгөгдсөн QTY тоог агуулсан хамгийн бага утга бүхий бичлэгийг буцаах
            if (jlist == null || jlist.Count <= 0) return null;

            Hashtable h = null;
            decimal min = 9999;
            for (int i = 0; i < jlist.Count; i++)
            {
                object jobj = jlist[i];
                if (jobj != null && jobj is Hashtable)
                {
                    Hashtable json = (Hashtable)jlist[i];
                    string jid = Core.Core.JPathGetStr(json, "id");
                    string jflg = Core.Core.JPathGetStr(json, "flg");
                    decimal jqty = Core.Core.JPathGetDec(json, "qty");
                    if (jid != null && jid == id && jflg != "B")
                    {
                        if (qty <= jqty && min > jqty) //хамгийн багаар агуулж бгаа эсэх
                        {
                            h = json;
                            min = jqty;
                        }
                    }
                }
            }
            return h;
        }

        #endregion

        #endregion

        #region Correction functions

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
                    decimal qty = Static.ToDecimal(r["QTY"]);

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
                    , qty*(uprice - sprice) + dp + ds      /*discount*/
                    , uprice    /*unit price*/
                    , sprice         /*sale price*/
                    , r["SALESAMOUNT"]         /*sale amount*/
                    , dp             /*discountprod*/
                    , ds             /*discountsale*/

                    , r["SALESTYPE"]             /*saletype*/
                    , r["PACKID"]        /*packid*/
                    , r["SUBTYPE"]  /*cat*/
                    , r["CONTRACTNO"]
                    , r["REGISTERNO"]
                    , DateTime.MinValue
                    );

                    string custreg = Static.ToStr(r["registerno"]);
                    string custname = Static.ToStr(r["custname"]);
                    decimal custno = Static.ToDecimal(r["custno"]);
                    CustomerAdd(custno, custname, custreg);
                }

                gridView1.ExpandAllGroups();

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
        public bool BrowseSales()
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
                        //_arraySearchForm = frm.CurrentRow.ItemArray;

                        _corrtxndate = Static.ToDate(frm.CurrentRow["TRANDATE"]);

                        //_status = Static.ToInt(frm.CurrentRow["STATUS"]);
                        _salesno = Static.ToStr(frm.CurrentRow["SALESNO"]);
                        _contractno = Static.ToStr(frm.CurrentRow["CONTRACTNO"]);
                        _orderno = Static.ToStr(frm.CurrentRow["ORDERNO"]);
                        _pledgeno = Static.ToStr(frm.CurrentRow["PLEDGENO"]);
                        _custno = Static.ToDecimal(frm.CurrentRow["CUSTNO"]);
                        _custname = Static.ToStr(frm.CurrentRow["CUSTNAME"]);
                        _flag = Static.ToStr(frm.CurrentRow["FLAG"]);

                        _selected_custno = _custno;
                        _selected_custname = _custname;
                        
                        decimal vat = Static.ToDecimal(frm.CurrentRow["VAT"]);
                        _isvat = vat > 0;

                        if (_flag != "N")
                        {
                            Alert("Засвар болон буцаалт хийгдээгүй үндсэн гүйлгээ сонгоно уу.", "Борлуулалтын засвар", 2);
                            return false;
                        }


                        //_totalamount = Static.ToDecimal(frm.CurrentRow["TOTALAMOUNT"]);
                        //_salesamount = Static.ToDecimal(frm.CurrentRow["SALESAMOUNT"]);
                        //_discount = Static.ToDecimal(frm.CurrentRow["DISCOUNT"]);
                        //_prepaid = Static.ToDecimal(frm.CurrentRow["PAID"]);
                        
                        _core.MainForm_HeaderSet(0, "Харилцагч №", _custno.ToString());
                        _core.MainForm_HeaderSet(1, "Овог нэр", _custname);
                        _core.MainForm_HeaderSet(2, "Гэрээ", _contractno);
                        _core.MainForm_HeaderSet(3, "Захиалга", _orderno);
                        _core.MainForm_HeaderSet(4, "Барьцаа", _pledgeno);

                        frm.Dispose();

                        res = ReadProdFromSales(_corrtxndate, _salesno);
                        if (res != null && res.ResultNo != 0) goto OnExit;

                        //res = ReadPaymentFromSales(_trandate, _salesno);
                        //if (res != null && res.ResultNo != 0) goto OnExit;

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

        #endregion

        #region Bill functions
        public void BillPrint(string salesno, bool isvat, DataTable cart, object[] payments)
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

                string userfullname = (string.IsNullOrEmpty(_core.RemoteObject.User.UserFName)
    ? ""
    : Static.SubStr(_core.RemoteObject.User.UserLName, 0, 1).ToUpper() + ".")
    + Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(_core.RemoteObject.User.UserLName.ToLower());

                frmBillShow frm = new frmBillShow(_core);
                frm.StartPosition = FormStartPosition.CenterScreen;
                //frm.BillShow(salesno, isvat, _core.POSNo, _core.ShiftNo, userfullname, cart, payments);
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

        #endregion
        #region Constructors
        public frmSales()
        {
            InitializeComponent();
            #region Events
            this.FormClosing += frmSales_FormClosing;
            this.galleryCust.Gallery.ItemCheckedChanged += Gallery_ItemCheckedChanged;
            this.galleryProd.Gallery.ItemClick += Gallery_ItemClick;
            this.gridControl1.Resize += gridControl1_Resize;
            this.gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            this.gridView1.KeyDown += gridView1_KeyDown;
            this.numQty.EditValueChanged += numQty_EditValueChanged;

            this.ucNumpad1.EventClickExtraButton += ucNumpad1_EventClickExtraButton;

            #endregion

            this.tabMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            gridControl1.Dock = DockStyle.Fill;
            tabProd.Dock = DockStyle.Fill;
            splitProd.Dock = DockStyle.Fill;
            galleryProd.Dock = DockStyle.Fill;

            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
            gridView1.OptionsBehavior.FocusLeaveOnTab = true;

            DevExpress.XtraGrid.StyleFormatCondition condition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            //condition1.Appearance.BackColor = Color.SeaShell;
            //condition1.Appearance.Options.UseBackColor = true;
            condition1.Appearance.ForeColor = Color.Brown;
            condition1.Appearance.Options.UseForeColor = true;
            condition1.ApplyToRow = true;
            
            condition1.Expression = "[CONTRACTNO] != ?"; //not equal to null
            condition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            
            gridView1.FormatConditions.Add(condition1);



            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);

            ucNumpad1.ExtraText2 = "$\r\nCash";
            ucNumpad1.ExtraText3 = "ﬦ\r\nCard";
            ucNumpad1.EditControl = numQty;
        }

        private void ucNumpad1_EventClickExtraButton(int num)
        {
            switch (num)
            {
                case 2:
                    PaymentCash();
                    break;
                case 3:
                    PaymentCard();
                    break;
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) gridControl1.SelectNextControl(gridControl1, true, true, true, true);
        }

        private void numQty_EditValueChanged(object sender, EventArgs e)
        {
            DataRow r = gridView1.GetFocusedDataRow();
            if (r != null)
            {
                decimal qt = Static.ToDecimal(numQty.EditValue);
                decimal pr = Static.ToDecimal(r["PRICE"]);
                r["QTY"] = qt < 0 ? 0 : qt;
                r["SALESAMOUNT"] = qt < 0 ? 0 : qt * pr;
                if (qt <= 0)
                {
                    //r.Delete();
                    //r.Table.AcceptChanges();
                }

                RefreshBoardValues();
            }
            else
            {
                numQty.EditValue = 0;
            }
        }
        private void numPaid_EditValueChanged(object sender, EventArgs e)
        {
            //RefreshBoardValues();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetFocusedDataRow();
            numQty.EditValue = row["QTY"];
        }
        private void gridControl1_Resize(object sender, EventArgs e)
        {
            gridView1.Columns[3].Width = gridControl1.Width - (50 + 70 + 80 + 10);
            gridView1.Columns[5].Width = 80;
            gridView1.Columns[6].Width = 50;
            gridView1.Columns[7].Width = 70;
        }

        private void Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            if (e.Item == null) return;
            try
            {
                decimal custno = _custno;

                #region Get product
                object[] tag = (object[])e.Item.Tag;
                if (tag != null)
                {
                    int type = (int)tag[0];
                    string id = (string)tag[1];

                    if (type == 3)
                    {
                        InitProd(id);
                    }
                    else
                    {
                        AddToCart(_selected_custno, _selected_custname, _selected_custregno, id, type, 1);
                        //MessageBox.Show(string.Format("Selected product is {0}", id));
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Gallery_ItemCheckedChanged(object sender, DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e)
        {
            /**********************
             * Gallery.Tag дээр идэвхтэй item обектийг хадгална.
             * Үүнийг сонгогдсон гэсэн зураг гүйлгэхэд ашигласан.
             **********************/
            //e.Item.ImageIndex = 1;
            //if (e.Gallery.Tag != null)
            //{
            //    DevExpress.XtraBars.Ribbon.GalleryItem old = (DevExpress.XtraBars.Ribbon.GalleryItem)e.Gallery.Tag;
            //    old.ImageIndex = 0;
            //}
            //e.Gallery.Tag = e.Item;


            if (e.Item.Tag != null)
            {
                _selected_custno = Static.ToDecimal(e.Item.Tag);
                _selected_custname = e.Item.Caption;
                _selected_custregno = e.Item.Description;
            }
        }

        #endregion
        #region Control events
        private void frmSales_Load(object sender, EventArgs e)
        {
            _core.MainForm_HeaderClear();
            _core.MainForm_HeaderSet(0, "Харилцагч №", "");
            _core.MainForm_HeaderSet(1, "Овог нэр", "");
            _core.MainForm_HeaderSet(2, "Гэрээ", "");
            _core.MainForm_HeaderSet(3, "Захиалга", "");
            _core.MainForm_HeaderSet(4, "Барьцаа", "");
            //_core.MainForm_HeaderSet(5, "Борлуулалт", "");

            _imgHome = _core.Resource.GetImage("prodmenu_home");
            _imgFolder = _core.Resource.GetImage("prodmenu_folder");
            _imgPackage = _core.Resource.GetImage("prodmenu_package");
            _imgService = _core.Resource.GetImage("prodmenu_serv");
            _imgInverntory = _core.Resource.GetImage("prodmenu_inv");

            InitCart();
            InitDict();
            InitProd("");

            gridControl1_Resize(null, null);
        }
        private void frmSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }

        private void btnTab1Cust_Click(object sender, EventArgs e)
        {
            SubMenu_Pledge();
            tabMain.SelectedTabPageIndex = 1;
        }
        private void btnTab1Contract_Click(object sender, EventArgs e)
        {
            SubMenu_Contract();
            tabMain.SelectedTabPageIndex = 1;
        }
        private void btnTab1Order_Click(object sender, EventArgs e)
        {
            SubMenu_Order();
            tabMain.SelectedTabPageIndex = 1;
        }
        
        private void btnCash_Click(object sender, EventArgs e)
        {
            PaymentCash();
        }
        private void btnCard_Click(object sender, EventArgs e)
        {
            PaymentCard();
        }
        private void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentForm();
        }
        
        private void chkVat_CheckedChanged(object sender, EventArgs e)
        {
            /*********************************************
             * Хэрэв ямар нэгэн бараа аваад борлуулалт эхэлсэн
             * бол НӨАТ-ийн төлвийг өөрчлүүлэхгүй.
             *********************************************/
            if (chkVat.Checked != _isvat)
            {
                if (_cart != null && _cart.Rows.Count > 0)
                {
                    _core.AlertShow("НӨАТ Тооцох", "Борлуулалт эхэлсэн тул НӨАТ өөрчлөх боломжгүй.", 2);
                    goto OnExit;
                }
            }

            Result res = CheckPriv();
            if (res != null && res.ResultNo == 0)
            {
                _isvat = chkVat.Checked;
            }
            else
            {
                _core.AlertShow("НӨАТ Тооцох", res.ResultDesc, 2);
                goto OnExit;
            }

        OnExit:
            chkVat.Checked = _isvat; // тэмдэглэгээг буцаах
            chkVat.Text = (chkVat.Checked ? " НӨАТтэй:" : " НӨАТгүй:");
        }

        #endregion
        #region Unusable

        //private void btnQtyP1_Click(object sender, EventArgs e)
        //{
        //    decimal d = Static.ToDecimal(numQty.EditValue);
        //    numQty.EditValue = ++d;
        //}
        //private void btnQtyP3_Click(object sender, EventArgs e)
        //{
        //    decimal d = Static.ToDecimal(numQty.EditValue);
        //    numQty.EditValue = d + 3;
        //}
        //private void btnQtyP5_Click(object sender, EventArgs e)
        //{
        //    decimal d = Static.ToDecimal(numQty.EditValue);
        //    numQty.EditValue = d + 5;
        //}

        //private void btnQtyM1_Click(object sender, EventArgs e)
        //{
        //    decimal d = Static.ToDecimal(numQty.EditValue);
        //    if (d > 0) numQty.EditValue = --d;
        //}
        //private void btnQtyM3_Click(object sender, EventArgs e)
        //{
        //    decimal d = Static.ToDecimal(numQty.EditValue);
        //    if (d >= 3) numQty.EditValue = d - 3;
        //}
        //private void btnQtyM5_Click(object sender, EventArgs e)
        //{
        //    decimal d = Static.ToDecimal(numQty.EditValue);
        //    if (d >= 5) numQty.EditValue = d - 5;
        //}

        //private void btnQtyDetail_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("...хийгдэж дуусаагүй байна...");
        //}
        
        #endregion

        public void Alert(Result res, string caption, int image = 0)
        {
            if (res != null && res.ResultNo != 0)
            {
                if (_core != null)
                {
                    _core.AlertShow(caption, res.ResultDesc, image);
                }
            }
        }
        public void Alert(string text, string caption, int image = 0)
        {
            if (_core != null)
            {
                _core.AlertShow(caption, text, image);
            }
        }

    }

    public struct structProdInfo
    {
        #region Fields

        public bool found;
        public string prodno;
        public string prodname;
        public decimal price;
        public decimal price2;

        public string subtype;
        public string schedule;

        public string contractno;

        #endregion
    }

    public class RowDiscountInfo
    {
        #region Fields

        public string dkey = null;
        public List<DataRow> rows = new List<DataRow>();
        public List<decimal> amt = new List<decimal>();
        public Hashtable d = null; //discount json object
        public decimal total = 0;
        public bool selected = false;

        //public DataRow row = null;
        //public string rowkey = null;
        //public Dictionary<string, Hashtable> dp = new Dictionary<string, Hashtable>();
        //public Dictionary<string, decimal> amt = new Dictionary<string, decimal>();

        public decimal custno = 0;
        public string custname = null;
        //public string prodno = null;
        //public string prodname = null;

        #endregion
    }
}
