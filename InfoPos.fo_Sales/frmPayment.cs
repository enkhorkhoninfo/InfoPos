using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmPayment : DevExpress.XtraEditors.XtraForm
    {
        #region Enums

        public enum SalesTransactionTypes
        {
            Normal = 0,
            Extend = 1,
            Correction = 2
        }

        #endregion
        #region Internal variables

        Image _imgEdit = null;

        string _paymentno = null;
        decimal _totalamount = 0;
        decimal _salesamount = 0;
        decimal _discount = 0;
        decimal _vat = 0;
        decimal _prepaid = 0;
        int _pflag_last = 0;

        DataTable _paymentdetail = null;
        DataTable _paymenttypes = null;

        public Result _result = null;

        #endregion
        #region Properties

        private int _pageno = 0;
        public int PageNo
        {
            get { return _pageno; }
        }
        private int _pagecount = 0;
        public int PageCount
        {
            get { return _pagecount; }
        }
        private int _pagerows = 20;
        public int PageRows
        {
            get { return _pagerows; }
            set
            {
                if (value > 0 && value < 100)
                    _pagerows = value;
            }
        }

        private InfoPos.Core.Core _core = null;
        public InfoPos.Core.Core Core
        {
            get { return _core; }
            set
            {
                if (value != null)
                {
                    _core = value;
                    if (_remote == null) _remote = _core.RemoteObject;
                    if (_resource == null) _resource = _core.Resource;
                }
            }
        }

        private ISM.CUser.Remote _remote = null;
        public ISM.CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }

        private ISM.Template.Resource _resource = null;
        public ISM.Template.Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        private ISM.Touch.TouchKeyboard _touchkeyboard = null;
        public ISM.Touch.TouchKeyboard TouchKeyboard
        {
            get { return _touchkeyboard; }
            set { _touchkeyboard = value; }
        }

        public DataRow CurrentRow
        {
            get { return gridView1.GetFocusedDataRow(); }
        }

        private string _layoutfilename = "";
        public string LayoutFileName
        {
            get { return _layoutfilename; }
        }

        private string _salesno = null;
        public string SalesNo
        {
            get { return _salesno; }
            set { _salesno = value; }
        }

        private object[] _salesParam = null;
        public object[] SalesParam
        {
            get { return _salesParam; }
            set { _salesParam = value; }
        }

        private object[] _paymentParam = null;
        public object[] PaymentParam
        {
            get { return _paymentParam; }
            set { _paymentParam = value; }
        }

        private SalesTransactionTypes _transactiontype =  SalesTransactionTypes.Normal;
        public SalesTransactionTypes TransactionType
        {
            get { return _transactiontype; }
            set { _transactiontype = value; }
        }

        #endregion
        #region Constructors

        public frmPayment(InfoPos.Core.Core core, string salesno)
        {
            InitializeComponent();
            this.Core = core;
            this.SalesNo = salesno;
            this.ResizeRedraw = true;
            this.popupContainer1.Width = dropDownButton1.Width;
            this.ucNumpad1.EditControl = numPayment;

            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
            
            #region Events
            this.FormClosing += frmPayment_FormClosing;
            this.gridView2.FocusedRowChanged += gridView2_FocusedRowChanged;
            #endregion
            #region Numeric field format
                        
            numTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numTotal.Properties.DisplayFormat.FormatString = "#,##0.00";

            numPrepaid.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numPrepaid.Properties.DisplayFormat.FormatString = "#,##0.00";

            numRemain.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numRemain.Properties.DisplayFormat.FormatString = "#,##0.00";

            numChange.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numChange.Properties.DisplayFormat.FormatString = "#,##0.00";

            numPayment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numPayment.Properties.DisplayFormat.FormatString = "#,##0.00";

            #endregion
            #region Gird format

            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            //gridView1.OptionsBehavior.ReadOnly = false;
            //gridView1.OptionsBehavior.Editable = true;
            //gridView1.OptionsCustomization.AllowGroup = false;
            //gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            //gridView1.OptionsView.ColumnAutoWidth = false;
            ////gridView1.OptionsView.ShowAutoFilterRow = false;
            //gridView1.OptionsView.ShowGroupPanel = false;
            //gridView1.OptionsView.ShowIndicator = false;
            //gridView1.OptionsView.RowAutoHeight = true;
            //gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);

            //// зурган багана нэмж оруулахад энэ үзэгдлийг зарлаж дотор нь зургаа set хийнэ.
            //gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
            //gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            //gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView1_RowCellClick);
            //gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanged);
            gridView1.RowHeight = 28;
            #endregion
            #region Init functions
            InitGrid();
            #endregion
        }

        void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView2.GetFocusedDataRow();
            PaymentListSelected(r);
            barManager1.CloseMenus();
        }

        void frmPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }

        #endregion
        #region Control Events

        private void frmPayment_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                _imgEdit = _resource.GetImage("navigate_edit");
            }
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(numPayment);
                _touchkeyboard.AddToKeyboard(txtRegNo);
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            Result res = null;

            decimal change = Static.ToDecimal(numChange.EditValue);
            decimal paid = Static.ToDecimal(numPayment.EditValue);
            string regno = Static.ToStr(txtRegNo.EditValue);
            string detail = Static.ToStr(txtDetail.EditValue);

            if (_pflag_last == 0 && _paymentdetail.Rows.Count == 0)
            {
                /***************************************
                 * Шууд бэлэн төлбөр бол 
                 ***************************************/
                res = PaymentByCash(_salesno,"", paid, change );
            }
            else if (_pflag_last == 1 && _paymentdetail.Rows.Count == 0)
            {
                /***************************************
                 * Шууд онлайн картын төлбөр бол 
                 ***************************************/
                res = PaymentByIPPOS(_salesno, "", paid, regno, detail);
            }
            else
            {
                res = PaymentByTable(_salesno);
            }
            if (res.ResultNo == 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                return;
            }
            else
            {
                ISM.Template.FormUtility.ValidateQuery(res);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region User Functions

        public void InitGrid()
        {
            _paymentdetail = new DataTable();
            _paymentdetail.Columns.Add("PAYMENTTYPE", typeof(string));
            _paymentdetail.Columns.Add("PAYMENTNAME", typeof(string));
            _paymentdetail.Columns.Add("PAYMENTFLAG", typeof(Int16));
            _paymentdetail.Columns.Add("AMOUNT", typeof(decimal));
            _paymentdetail.Columns.Add("CONTRACTNO", typeof(string));
            _paymentdetail.Columns.Add("DETAIL", typeof(string));
            _paymentdetail.Columns.Add("METHOD", typeof(Int16));

            ISM.Template.FormUtility.GridLayoutGet(gridView1, _paymentdetail, _layoutfilename);

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Төрөл", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Төрөл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Флаг", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Төлөх дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Бүртгэлийн дугаар");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Бусад");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Арга", true);

            ISM.Template.FormUtility.Column_SetNumber(ref gridView1, 3, "#,##0.00");
        }
        public void InitTouchButtons(DataTable paytypes)
        {
            /*********************************************
             * Дараах бүтэц бүхий тэйбэл серверээс ирнэ.
             * PAPAYTYPE:
             * pt.typeid,pt.name,pt.suspaccount,0 paid,pt.contracttype,pt.paymentflag
             *********************************************/

            gridView2.OptionsBehavior.Editable = false;
            gridView2.OptionsBehavior.ReadOnly = true;
            gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridView2.OptionsView.ShowColumnHeaders = false;
            gridView2.OptionsView.ShowGroupPanel = false;
            gridView2.OptionsView.ShowIndicator = false;
            //gridView2.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView2.OptionsSelection.UseIndicatorForSelection = false;
            gridView2.RowHeight = 28;

            gridControl2.DataSource = paytypes;
            gridControl2.BindingContext = new System.Windows.Forms.BindingContext();
            gridControl2.ForceInitialize();

            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 0, "Төрөл", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 1, "Нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 2, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 3, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 4, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 5, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 6, "", true);
        }
        public void InitValues(decimal totalamount, decimal salesamount, decimal discount, decimal vat, decimal prepaid)
        {
            _totalamount = totalamount;
            _salesamount = salesamount;
            _discount = discount;
            _vat = vat;
            _prepaid = prepaid;

            txtSalesNo.EditValue = _salesno;
            numTotal.EditValue = _totalamount;
            //numSales.EditValue = _salesamount;
            //numDiscount.EditValue = _discount;
            //numVat.EditValue = _vat;
            numPrepaid.EditValue = _prepaid;
            numRemain.EditValue = _totalamount - _prepaid > 0 ? _totalamount - _prepaid : 0;
        }

        public Result DataRefresh()
        {
            Result res = null;

            #region Prepare parameters

            object[] param = new object[] { _core.TxnDate, _salesno, _remote.User.UserNo, _core.POSNo, _core.AreaCode };

            #endregion
            #region Validation

            if (_remote == null)
            {
                res = new Result(1000, "Internal Error: Remote object not set.");
                goto OnExit;
            }

            #endregion
            #region Call server

            res = _remote.Connection.Call(_remote.User.UserNo, 605, 605010, 605001, param);
            if (res.ResultNo != 0) goto OnExit;

            if (res.Data.Tables[1].Rows.Count == 0)
            {
                res = new Result(2001, string.Format("{0} дугаартай хэрэглэгч дээр төлбөрийн хэрэгсэл тохируулаагүй байна.\r\n Системийн администратортой холбоо барина уу.", _core.RemoteObject.User.UserNo));
                goto OnExit;
            }

            #endregion
            #region Төлбөрийн төрлийн жагсаалт бэлдэх

            _paymenttypes = res.Data.Tables[1];
            InitTouchButtons(_paymenttypes);

            #endregion
            #region Борлуулалтын дүнгүүдийг олгох

            DataTable dt = res.Data.Tables[0];
            if (dt.Rows.Count > 0)
            {
                _totalamount = Static.ToDecimal(dt.Rows[0]["TOTALAMOUNT"]);
                _salesamount = Static.ToDecimal(dt.Rows[0]["SALESAMOUNT"]);
                _discount = Static.ToDecimal(dt.Rows[0]["DISCOUNT"]);
                _vat = Static.ToDecimal(dt.Rows[0]["VAT"]);
                _prepaid = Static.ToDecimal(dt.Rows[0]["PAID"]);

                txtSalesNo.EditValue = _salesno;
                numTotal.EditValue = _totalamount;
                //numSales.EditValue = _salesamount;
                //numDiscount.EditValue = _discount;
                //numVat.EditValue = _vat;
                numPrepaid.EditValue = _prepaid;
                numRemain.EditValue = _salesamount - _prepaid > 0 ? _salesamount - _prepaid : 0;
            }
            #endregion

        OnExit:

            
            return res;
        }
        public Result Find(string salesno)
        {
            Result res = null;
            if (!string.IsNullOrEmpty(salesno))
            {
                _salesno = salesno;
                res = DataRefresh();
            }
            return res;
        }

        public Result ListAdd(string ptype, string pname, int pflag, decimal amount, string regno, int pcheck, string detail)
        {
            /**********************************************************
             * ptype  - төлбөрийн төрлийн код дугаар. Сервер талд, энэ талбар нь хоосон бол pflag аар ptype -ийг олно. Гэхдээ зөвхөн CASH, CARD үед л ашиглана.
             * pname  - төлбөрийн төрлийн нэр
             * pflag  - төлбөрийн ялгац. 0-бэлэн, 1-карт, 2-бусад төлбөрийн төрөл
             * amount - төлбөрийн дүн
             * regno  - Бусад төлбөрийн төрөл дэх төлбөрийн хэрэгслийн сери дугаар.
             * pcheck - төлбөрийн хэрэгслийн серийн дугаарыг шалгах эсэх. 0-Шалгахгүй, 1-Шалгана
             * 
             **********************************************************/
            Result res = new Result();
            try
            {
                #region Validation

                if (string.IsNullOrEmpty(ptype))
                {
                    res = new Result(9, "Төлбөрийн төрөл сонгогдоогүй байна.");
                    goto OnExit;
                }
                if (pflag != 0 && string.IsNullOrEmpty(regno))
                {
                    res = new Result(9, "Төлбөрийн хэрэгслийн дугаараа оруулна уу.");
                    goto OnExit;
                }
                if (amount <= 0)
                {
                    res = new Result(9, "Төлбөрийн дүнгээ оруулна уу.");
                    goto OnExit;
                }

                decimal nowpaid = _paymentdetail.AsEnumerable().Sum(r => r.Field<decimal>("AMOUNT"));
                if (nowpaid + amount > _salesamount - _prepaid)
                {
                    //res = new Result(9, "Төлбөр хийх дүн илүү байна.");
                    //goto OnExit;
                }

                DataRow[] rows = _paymentdetail.Select(string.Format("PAYMENTTYPE='{0}' AND CONTRACTNO='{1}'", ptype, regno));
                if (rows != null && rows.Length > 0)
                {
                    res = new Result(9, string.Format("[{0} {1}] Төлбөрийн хэрэгслээр төлбөр бүртгэгдсэн байна.", dropDownButton1.Text, regno));
                    goto OnExit;
                }

                #endregion
                #region Гэрээний үлдэгдэл ба хорогдуулах аргыг шалгах

                decimal balance = 0;
                int method = 0;

                /*********************************************
                 * pflag: PaymentFlag
                 * 0 - Payment by Cash
                 * 1 - Payment by Card
                 * 2 - Payment by Contracts
                 *********************************************/
                if (pflag == 2)
                {
                    object[] param = new object[] { regno, ptype };
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 604, 604003, 605001, param);
                    if (res.ResultNo != 0) goto OnExit;
                    if (res.AffectedRows <= 0)
                    {
                        res = new Result(9, string.Format("[{0}] Төлбөрийн хэрэгслийн бүртгэлийн дугаар олдсонгүй.", regno));
                        goto OnExit;
                    }

                    DataTable dt = res.Data.Tables[0];
                    balance = Static.ToDecimal(dt.Rows[0]["balance"]);
                    method = Static.ToInt(dt.Rows[0]["method"]);

                    /************************************
                     * Method:
                     * 0 - Үлдэгдэл нь борлуулалтаар буурна
                     * 1 - Үлдэгдэл нь автоматаар буурна
                     * 2 - Үлдэгдэл хөтлөхгүй
                     ************************************/
                    if (method == 0 && balance < amount)
                    {
                        res = new Result(9, string.Format("[{0}] Төлбөрийн хэрэгслийн үлдэгдэл хүрэлцэхгүй байна. Үлдэгдэл = {1}", regno, balance));
                        goto OnExit;
                    }
                }

                #endregion
                #region Жагсаалт руу нэмэх

                DataRow dr = _paymentdetail.NewRow();
                dr["PAYMENTTYPE"] = ptype;
                dr["PAYMENTNAME"] = pname;
                dr["PAYMENTFLAG"] = pflag;
                dr["AMOUNT"] = amount;
                dr["CONTRACTNO"] = regno;
                dr["DETAIL"] = detail;
                dr["METHOD"] = 0; // method;

                _paymentdetail.Rows.Add(dr);

                #endregion
                RefreshTotalValue();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

        OnExit:
            return res;
        }
        public Result ListDel(string ptype, string regno)
        {
            Result res = new Result();

            DataRow[] rows = _paymentdetail.Select(string.Format("PAYMENTTYPE='{0}' AND CONTRACTNO='[{1}]'", ptype, regno));
            if (rows == null || rows.Length <= 0)
            {
                res = new Result(9, string.Format("[{0}] Төлбөрийн хэрэгсэл жагсаалтад бүртгэгдээгүй байна.", regno));
                goto OnExit;
            }

            rows[0].Delete();
            rows[0].Table.AcceptChanges();

            RefreshTotalValue();

        OnExit:
            return res;
        }

        public void RefreshTotalValue()
        {
            decimal sum = _paymentdetail.AsEnumerable().Sum(r => r.Field<decimal>("AMOUNT"));
            numPaid.EditValue = sum;

            sum = Static.ToDecimal(numRemain.EditValue) - sum;
            if (sum > 0) numChange.EditValue = -sum;
            else numChange.EditValue = null;
        }

        public void ShowKeyboard(int rowhandle)
        {
            if (TouchKeyboard == null) return;

            //if (rowhandle < gridView1.RowCount)
            //{
            //    //gridView1.SelectRow(rowhandle);
            //    gridView1.FocusedRowHandle = rowhandle;
            //    DialogResult res = TouchKeyboard.ShowKeyboard(gridView1, rowhandle, 3);
            //    if (res == DialogResult.OK)
            //    {
            //        if (rowhandle + 1 < gridView1.RowCount)
            //            ShowKeyboard(rowhandle + 1);
            //    }
            //}
        }
        public void SaveLayout()
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }

        public void SetPaymentType(int pflag)
        {
            if (_paymenttypes != null)
            {
                DataRow[] rows = _paymenttypes.Select(string.Format("PAYMENTFLAG={0}", pflag));
                if (rows != null && rows.Length > 0)
                {
                    gridView2.FocusedRowHandle = gridView2.GetRowHandle(_paymenttypes.Rows.IndexOf(rows[0]));
                }
            }
        }
        public DataRow GetPaymentType(string typeid)
        {
            DataRow row = null;

            if (_paymenttypes != null)
            {
                DataRow[] rows = _paymenttypes.Select(string.Format("TYPEID='{0}'", typeid));
                if (rows != null) row = rows[0];
            }

            return row;
        }

        public Result PaymentByCash(string salesno , string paymentno, decimal amount, decimal chargeamount)
        {
            Result res = null;
            /****************************************************************************************
             * salesno, paymentno, paymenttype, paymentflag, amount, txnflag, contractno, areacode, posno
             * Дээрх бүтэц бүхий олон бичлэгийг сервер руу явуулна.
             * param = Object[ object[], object[], ...  ] гэсэн байдлаар.
             * 
             * ChargeAmount буюу хариулт мөнгө нь хасах (-) дүнтэй тоо байх ёстой!
             * PaymentFlag = 0 буюу бэлэн гүйлгээ үед PaymentType -ийг явуулах шаардлагагүй.
             * Сервер талд үүнийг өөрөө олчихно.
             ****************************************************************************************/

            ArrayList param = new ArrayList();
            object[] row = null;

            if (amount > 0)
            {
                row = new object[] { salesno, paymentno, "" /*ptype*/, 0 /*cash txn*/, amount, "N" /*flag=0 orig txn*/, "CASH", _core.RemoteObject.User.AreaCode, _core.POSNo, "" /*detail info*/, "CASH" /*paymentname*/ };
                param.Add(row);
            }
            if (chargeamount < 0)
            {
                row = new object[] { salesno, paymentno, "" /*ptype*/, 0 /*cash txn*/, chargeamount, "E" /*flag=1 хариулт txn*/, "CASH", _core.RemoteObject.User.AreaCode, _core.POSNo, "" /*detail info*/, "CASH" /*paymentname*/ };
                param.Add(row);
            }

            _paymentParam = param.ToArray();

            // Илгээх датагаа бүрдүүлж шидэх
            if (_salesParam == null)
            {
                object[] baseinfo = new object[] { _core.TxnDate, _core.POSNo, _core.AreaCode, salesno };
                object[] allparam = new object[] { baseinfo, param.ToArray() };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605011, 605011, allparam);
            }
            else
            {
                object[] allparam = new object[] { _salesParam, param.ToArray() };

                switch (_transactiontype)
                {
                    case SalesTransactionTypes.Extend:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605020, 605020, allparam);
                        break;
                    case SalesTransactionTypes.Correction:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605021, 605021, allparam);
                        break;
                    default:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605012, 605012, allparam);
                        break;
                }
            }
            _result = res;
            return res;
        }
        public Result PaymentByIPPOS(string salesno, string paymentno, decimal amount, string regno, string detail)
        {
            Result res = null;

            #region IPPOS Карт төхөөрөмжөөр эхэлж картын гүйлгээ хийнэ.
            //GCIPPOSF.CTxn t = new GCIPPOSF.CTxn();
            //t.InitAll();

            //string ret = t.Txn1000(Static.ToDouble(amount), "496");
            //MessageBox.Show(ret);
            //goto OnExit;
            #endregion

            /****************************************************************************************
             * salesno, paymentno, paymenttype, paymentflag, amount, contractno, areacode, posno
             * Дээрх бүтэц бүхий олон бичлэгийг сервер руу явуулна.
             * param = Object[ object[], object[], ...  ] гэсэн байдлаар.
             * 
             * PaymentFlag=1 буюу Картын гүйлгээ үед PaymentType -ийг явуулах шаардлагагүй.
             * Сервер талд үүнийг өөрөө олчихно.
             ****************************************************************************************/

            if (string.IsNullOrEmpty(regno)) regno = "CARD";

            ArrayList param = new ArrayList();
            object[] row = null;

            if (amount > 0)
            {
                row = new object[] { salesno, paymentno, "" /*ptype*/, 1 /*card txn*/, amount, "N" /*flag=0 orig txn*/, regno, _core.RemoteObject.User.AreaCode, _core.POSNo, detail /*detail info*/, "CARD" /*paymentname*/};
                param.Add(row);
            }

            _paymentParam = param.ToArray();

            // Илгээх датагаа бүрдүүлж шидэх
            if (_salesParam == null)
            {
                object[] baseinfo = new object[] { _core.TxnDate, _core.POSNo, _core.AreaCode, salesno };
                object[] allparam = new object[] { baseinfo, param.ToArray() };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605011, 605011, allparam);
            }
            else
            {
                object[] allparam = new object[] { _salesParam, param.ToArray() };

                switch (_transactiontype)
                {
                    case SalesTransactionTypes.Extend:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605020, 605020, allparam);
                        break;
                    case SalesTransactionTypes.Correction:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605021, 605021, allparam);
                        break;
                    default:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605012, 605012, allparam);
                        break;
                }
            }
        OnExit:
            _result = res;
            return res;
        }
        public Result PaymentByTable(string salesno)
        {
            Result res = null;

            /***********************************************************
             * Анхан шатны шалгууруудыг шалгана.
             ***********************************************************/
            res = PaymentTableValidation();
            if (res != null && res.ResultNo != 0) goto OnExit;

            /***********************************************************
             * Төлбөрийн жагсаалт доторх гүйлгээний мэдээллүүдийг 
             * цуглуулж буцаана.
             ***********************************************************/
            res = PaymentTablePreparation(salesno);
            if (res != null && res.ResultNo != 0) goto OnExit;
            object[] param = res.Param;

            _paymentParam = res.Param;

            /***********************************************************
             * Хэрэв төлбөрийн хэрэгслүүд дотор картын онлайн гүйлгээ
             * орсон бол түүнийг ялгаж IPPOS гүйлгээг хамгийн эхэнд
             * хийнэ. Тэгээд амжилттай болсон үед төлбөрийн гүйлгээг
             * хийх шаардлагатай.
             ***********************************************************/
            res = PaymentTableCard();
            if (res != null && res.ResultNo != 0) goto OnExit;

            /***********************************************************
             * Төлбөрийн гүйлгээний мэдээллийг сервер рүү илгээж хийлгэх.
             * 2 хэмжээст object[][] байна.
             ***********************************************************/
            if (_salesParam == null)
            {
                // Дан төлбөрийн гүйлгээ. Энэ үед төлбөрийн гүйлгээний дугаар нь
                // res.retdesc талбараар буцана.
                object[] baseinfo = new object[] { _core.TxnDate, _core.POSNo, _core.AreaCode, salesno };
                object[] allparam = new object[] { baseinfo, param.ToArray() };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605011, 605011, allparam);
            }
            else
            {
                // Борлуулалт болон төлбөрийн гүйлгээ хамтдаа. 
                // Энэ үед борлуулалт болон төлбөрийн гүйлгээний дугаар нь
                // res.Param["salesno", "paymentno"] талбараар буцана.
                object[] allparam = new object[] { _salesParam, param };

                switch (_transactiontype)
                {
                    case SalesTransactionTypes.Extend:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605020, 605020, allparam);
                        break;
                    case SalesTransactionTypes.Correction:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605021, 605021, allparam);
                        break;
                    default:
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605012, 605012, allparam);
                        break;
                }
            }

        OnExit:
            _result = res;
            return res;
        }

        public Result PaymentTableValidation()
        {
            Result res = new Result();
            #region Validation

            decimal remain = Static.ToDecimal(numRemain.EditValue);
            decimal nowpaid = Static.ToDecimal(numPaid.EditValue);

            if (remain <= 0)
            {
                res = new Result(9, "Төлбөр бүрэн төлөгдсөн байна.");
                goto OnExit;
            }
            if (nowpaid <= 0)
            {
                res = new Result(9, "Төлбөрийн дүнгээ оруулна уу.");
                goto OnExit;
            }
            if (nowpaid > remain)
            {
                //res = new Result(9, "Төлбөрийн дүн төлөгдөх дүнгээс их байна.");
                //goto OnExit;
            }

            #endregion
        OnExit:
            return res;
        }
        public Result PaymentTablePreparation(string salesno)
        {
            /****************************************************************************************
             * salesno, paymentno, paymenttype, paymentflag, amount, contractno, areacode, posno
             * Дээрх бүтэц бүхий олон бичлэгийг сервер руу явуулна.
             * param = Object[ object[], object[], ...  ] гэсэн байдлаар.
             * 
             * PaymentFlag=0 буюу Бэлэн гүйлгээ үед 
             * PaymentFlag=1 буюу Картын гүйлгээ үед
             * PaymentType -ийг явуулах шаардлагагүй.
             * Сервер талд үүнийг өөрөө олчихно.
             ****************************************************************************************/

            /**********************************************
             * Төлбөрийн жагсаалтанд байгаа төлбөрүүдийн нэг бүрчлэн
             * уншиж гүйлгээг хийх.
             * 1. Хамгийн эхэнд картын гүйлгээ буюу PaymentFlag=1 байх бичлэгийг шалгаж IPPOS оор картын гүйлгээг эхэлж хийнэ.
             * 2. Амжилттай болсны дараа бүх бичлэгүүдийг сервер рүү явуулж гүйлгээг хийлгүүлнэ.
             **********************************************/

            Result res = null;

            #region Validation

            res = PaymentTableValidation();
            if (res != null && res.ResultNo != 0) goto OnExit;

            #endregion
            #region Төлбөрийн бичлэгүүдийг бэлдэх

            ArrayList param = new ArrayList();
            foreach (DataRow dr in _paymentdetail.Rows)
            {
                #region Collect parameters
                object[] row = new object[] { 
                    salesno
                    , ""        /*paymentno*/
                    , dr["PAYMENTTYPE"] /*ptype*/
                    , dr["PAYMENTFLAG"]
                    , dr["AMOUNT"]
                    , "N" /*flag=0 orig txn*/
                    , dr["CONTRACTNO"]
                    , _core.RemoteObject.User.AreaCode
                    , _core.POSNo 
                    , dr["DETAIL"] /*detail info*/
                    , dr["PAYMENTNAME"] /*билл хэвлэхэд зориулав*/
                };
                param.Add(row);
                #endregion
            }
            #endregion
            #region Хариулт байвал бичлэгт нэмж оруулах

            decimal chargeamount = Static.ToDecimal(numChange.EditValue);
            if (chargeamount < 0)
            {
                object[] row = new object[] { salesno, "", "" /*ptype*/, 0 /*cash txn*/, chargeamount, "E" /*flag=1 хариулт txn*/, "CASH", _core.RemoteObject.User.AreaCode, _core.POSNo, "" /*detail info*/, "CASH" };
                param.Add(row);
            }
            
            #endregion
            res.Param = param.ToArray();

        OnExit:
            return res;
        }
        public Result PaymentTableCard()
        {
            /***********************************************************
             * Хэрэв төлбөрийн хэрэгслүүд дотор картын онлайн гүйлгээ
             * орсон бол түүнийг ялгаж IPPOS гүйлгээг хийх функц.
             * 
             * Энэ олон төрлийн төлбөрийн гүйлгээг зэрэг хийх үед буюу
             * PaymentByTable() функцэд хэрэглэгдэнэ.
             ***********************************************************/
            Result res = new Result();
            try
            {
                #region Картын гүйлгээг шалгаж IPPOS хийх

                var query = from alias in _paymentdetail.AsEnumerable()
                            where alias.Field<Int16>("PAYMENTFLAG") == 1
                            select alias;

                if (query.Count() > 0)
                {
                    DataRow r = query.First();
                    #region IPPOS Карт төхөөрөмжөөр эхэлж картын гүйлгээ хийнэ.
                    //GCIPPOSF.CTxn t = new GCIPPOSF.CTxn();
                    //t.InitAll();

                    //string ret = t.Txn1000(Static.ToDouble(amount), "496");
                    //MessageBox.Show(ret);
                    // if (error) goto OnExit;
                    #endregion
                }

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            return res;
        }

        public void PaymentListSelected(DataRow r)
        {
            if (r == null) return;

            #region Төлбөрийн төрлийн утгуудаа авах
            
            string typeid = Static.ToStr(r["TYPEID"]);
            string typename = Static.ToStr(r["NAME"]);
            int pflag = Static.ToInt(r["PAYMENTFLAG"]);
            
            _pflag_last = pflag;

            numPayment.EditValue = null;
            //numChange.EditValue = null;
            txtRegNo.EditValue = null;

            dropDownButton1.ToolTip = typeid;
            dropDownButton1.Text = typename;
            //dropDownButton1.Tag = typeid;

            #endregion
            #region Контролуудыг идэвхжүүлэх

            switch (pflag)
            {
                case 0: // Cash payment
                    lblChange.Visible = true;
                    numChange.Visible = true;
                    lblRegNo.Visible = false;
                    txtRegNo.Visible = false;
                    lblDetail.Visible = false;
                    txtDetail.Visible = false;
                    break;
                case 1: // Online card payment
                    lblChange.Visible = true;
                    numChange.Visible = true;
                    lblRegNo.Visible = true;
                    txtRegNo.Visible = true;
                    lblRegNo.Text = "Картын дугаар:";
                    lblDetail.Visible = true;
                    txtDetail.Visible = true;
                    break;
                default: // Contract, offline card others.
                    lblChange.Visible = true;
                    numChange.Visible = true;
                    lblRegNo.Visible = true;
                    txtRegNo.Visible = true;
                    lblRegNo.Text = "Бүртгэлийн дугаар:";
                    lblDetail.Visible = true;
                    txtDetail.Visible = true;
                    break;
            }

            #endregion
        }
        private void PaymentListAdd()
        {
            Result res = null;

            if (string.IsNullOrEmpty( dropDownButton1.ToolTip))
            {
                res = new Result(9, "Төлбөрийн хэрэгсэл сонгогдоогүй байна.");
                goto OnExit;
            }
            if (txtRegNo.Visible && string.IsNullOrEmpty(txtRegNo.Text))
            {
                res = new Result(9, "Төлбөрийн хэрэгслийн дугаар оруулаагүй байна.");
                goto OnExit;
            }

            DataRow row = GetPaymentType(dropDownButton1.ToolTip);

            int pflag = Static.ToInt(row["PAYMENTFLAG"]);
            int pcheck = Static.ToInt(row["CONTRACTCHECK"]);
            decimal amount = Static.ToDecimal(numPayment.EditValue);
            string detail = Static.ToStr(txtDetail.EditValue);

            /*************************************************
             * pflag = 0 бол бэлэн гүйлгээ ба үүнд гэрээний дугаар,
             * картын дугаар оруулах шаардлагагүй.
             *************************************************/
            string regno = txtRegNo.Text;
            if (pflag == 0) regno = "CASH";
            else if (string.IsNullOrEmpty(regno)) regno = "NONE";

            res = ListAdd(dropDownButton1.ToolTip, dropDownButton1.Text, pflag, amount, regno, pcheck, detail);
            if (res.ResultNo != 0) goto OnExit;
            
            //numBalance.EditValue = null;
            txtRegNo.EditValue = null;
            txtDetail.EditValue = null;
            //numChange.EditValue = null;
            numPayment.EditValue = null;

            numPayment.Select();

        OnExit:
            dropDownButton1.Tag = null;
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        private void PaymentListRemove()
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                dr.Delete();
                dr.Table.AcceptChanges();
            }
            RefreshTotalValue();
        }

        #endregion

        #region Error Messages

        public enum ErrorType
        {
            ZERO_AMOUNT
        }
        public Result Err(ErrorType e)
        {
            Result res = null;
            switch (e)
            {
                case ErrorType.ZERO_AMOUNT: res = new Result(9, "Төлбөрийн дүнгээ оруулна уу!"); break;

                default: res = new Result(9, "Алдаа гарлаа!"); break;
            }
            return res;
        }

        #endregion

        private void ucNumpad1_EventClickExtraButton(int num)
        {
            switch (num)
            {
                case 2:
                    PaymentListAdd();
                    break;
                case 3:
                    PaymentListRemove();
                    break;
            }
        }

        private void numPayment_EditValueChanged(object sender, EventArgs e)
        {
            decimal d = Static.ToDecimal(numRemain.EditValue) 
                - Static.ToDecimal(numPaid.EditValue) 
                - Static.ToDecimal(numPayment.EditValue);

            if (d >= 0) numChange.EditValue = null;
            else numChange.EditValue = d;
        }
        private void numPayment_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal remain = Static.ToDecimal(numRemain.EditValue);
            decimal paid = Static.ToDecimal(numPaid.EditValue);
            decimal now = Static.ToDecimal(e.NewValue);

            //if (_pflag_last != 0 && remain < now + paid)
            //if (remain < paid)
            //{
            //    _core.AlertShow("Төлбөр", "Төлбөрийн дүн төлөх дүнгээс их байх ёсгүй!", 2);
            //    e.Cancel = true;
            //}
        }

    }
}