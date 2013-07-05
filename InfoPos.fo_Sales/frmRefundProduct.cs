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
    public partial class frmRefundProduct : DevExpress.XtraEditors.XtraForm
    {
        #region Comment!

        /*******************************************************************
         * Энэ дэлгэц нь борлуулалт доторх өгөгдсөн бараа үйлчилгээг буцаана.
         * Бараа үйлчилгээний буцааж буй үнийн дүн тооцоолж
         * Sales тэйблийн бичлэгт байгаа дараах утгуудыг шинэчилнэ. 
         * Үүнд:
         *      SalesAmount = барааны дүнгээр хасна.
         *      Discount    = барааны хөнгөлөлтийг хасна.
         *      TotalAmount = SalesAmount - Discount
         *      VAT         = TotalAmount * 0.1
         * 
         *******************************************************************/

        #endregion

        #region Internal variables

        Image _imgEdit = null;

        string _paymentno = null;
        decimal _totalamount = 0;
        decimal _salesamount = 0;
        decimal _vat = 0;
        decimal _prepaid = 0;
                
        DataTable _paymentdetail = null; //Борлуулалтын үндсэн мэдээлэл. Sales тэйблээс авна.
        DataTable _salesrent = null;
        DataTable _paymenttxn = null;

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
            get{return _resource;}
            set {_resource = value;}
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

        private DateTime _trandate = DateTime.MinValue;
        public DateTime TranDate
        {
            get { return _trandate; }
            set { _trandate = value; }
        }

        private string _salesno = null;
        public string SalesNo
        {
            get { return _salesno; }
            set { _salesno = value; }
        }

        private string _prodno = null;
        public string ProdNo
        {
            get { return _prodno; }
            set { _prodno = value; }
        }

        private int _prodtype = 0;
        public int ProdType
        {
            get { return _prodtype; }
            set { _prodtype = value; }
        }
        
        private decimal _custno = 0;
        public decimal CustNo
        {
            get { return _custno; }
            set { _custno = value; }
        }

        private decimal _price = 0;
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private decimal _qty = 0;
        public decimal Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }
        private decimal _discount = 0;
        public decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        #endregion
        #region Constructors

        public frmRefundProduct(InfoPos.Core.Core core, string salesno, decimal custno, string prodno, int prodtype, string prodname, decimal price, decimal qty, decimal discount)
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            
            this.Core = core;
            this.SalesNo = salesno;
            this.CustNo = custno;
            this.ProdNo = prodno;
            this.ProdType = prodtype;
            this.Price = price;
            this.Qty = qty;
            this.Discount = discount;

            this.txtSalesNo.EditValue = salesno;
            this.txtProdNo.EditValue = prodno;
            this.txtProdName.EditValue = prodname;

            this.numPrice.EditValue = price;
            this.numQty.EditValue = qty;
            this.numTotal.EditValue = price * qty;
            this.numRefund.EditValue = qty;
            this.numDiscount.EditValue = discount;

            this.numRefundAmt.EditValue = price * qty - discount;

            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);

            #region Events
            this.FormClosing += frmPayment_FormClosing;
            this.numEdit.EditValueChanging += numEdit_EditValueChanging;
            this.gridView1.FocusedRowChanged+=gridView1_FocusedRowChanged;
            #endregion
            #region Numeric field format

            numQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numQty.Properties.DisplayFormat.FormatString = "#,##0.00";

            numDiscount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numDiscount.Properties.DisplayFormat.FormatString = "#,##0.00";
            
            numTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numTotal.Properties.DisplayFormat.FormatString = "#,##0.00";
            
            numPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numPrice.Properties.DisplayFormat.FormatString = "#,##0.00";
            
            numRefund.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numRefund.Properties.DisplayFormat.FormatString = "#,##0.00";

            numRefundAmt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numRefundAmt.Properties.DisplayFormat.FormatString = "#,##0.00";

            //numBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //numBalance.Properties.DisplayFormat.FormatString = "#,##0.00";

            #endregion
            #region Gird format
            
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            gridView1.RowHeight = 28;

            #endregion
            #region Init functions
            InitGrid();
            #endregion
        }

        void numEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            DataRow r = gridView1.GetFocusedDataRow();
            if (r != null)
            {
                decimal refund = Static.ToDecimal(e.NewValue);
                decimal amount = Static.ToDecimal(r["AMOUNT"]);

                if (amount >= refund)
                {
                    r["REFUND"] = refund;
                    //RefreshTotalValue();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        void frmPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLayout();
        }
        void frmPayment_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                _imgEdit = _resource.GetImage("navigate_edit");
            }
            if (_touchkeyboard != null)
            {
                //_touchkeyboard.AddToKeyboard(numEdit);
            }
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView1.GetFocusedDataRow();
            if (r != null) numEdit.EditValue = r["REFUND"];
        }
        

        #endregion
        #region Button Events

        private void btnRefundSales_Click(object sender, EventArgs e)
        {
            Result res = null;
            #region Validation

            decimal qty = Static.ToDecimal(numRefund.EditValue);
            if (qty <= 0)
            {
                res = new Result(9, "Буцаалт хийх тоо ширхэг буруу байна.");
                goto OnExit;
            }

            decimal shouldpay = Static.ToDecimal (numQty.EditValue ) * (_price - _discount );
            decimal totalrefund = _paymenttxn.AsEnumerable().Sum(x => Static.ToDecimal(x.Field<object>("REFUND")));
            if (totalrefund < shouldpay)
            {
                if (!ISM.Template.FormUtility.ValidateConfirm(
                string.Format ("Буцаалт хийх барааны төлбөрийн дүн таарахгүй байна, буцаалтыг үргэлжлүүлэх үү?")
                )) goto OnExit;
            }

            #endregion
            #region Confirmation
            string confirm = "";
            #endregion

            //string success_text = null;

            res = MakeRefund(_salesno, _custno, _prodno, _prodtype, _price, qty);
            if (res.ResultNo == 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                return;
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
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
            _paymentdetail.Columns.Add("METHOD", typeof(Int16));

            ISM.Template.FormUtility.GridLayoutGet(gridView1, _paymentdetail, _layoutfilename);

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Төрөл", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Төрөл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Флаг", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Төлөх дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Бүртгэлийн дугаар");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Арга", true);

            ISM.Template.FormUtility.Column_SetNumber(ref gridView1, 3, "#,##0.00");
        }
        public void InitValues(decimal totalamount, decimal salesamount, decimal discount, decimal vat, decimal prepaid)
        {
            //_totalamount = totalamount;
            //_salesamount = salesamount;
            //_discount = discount;
            //_vat = vat;
            //_prepaid = prepaid;

            //numPrice.EditValue = _totalamount;
            //numQty.EditValue = _salesamount;
            ////numDiscount.EditValue = _discount;
            //numTotal.EditValue = _vat;

            //numRefund.EditValue = 0;
        }

        public Result DataRefresh()
        {
            Result res = null;

            #region Prepare parameters

            object[] param = new object[] { _trandate, _salesno, _remote.User.UserNo, txtProdNo.Text };

            #endregion
            #region Validation

            if (_remote == null)
            {
                res = new Result(1000, "Internal Error: Remote object not set.");
                goto OnExit;
            }

            #endregion
            
            #region Төлбөрийн үлдэгдлүүд

            param = new object[] { _core.TxnDate, _salesno };
            res = _remote.Connection.Call(_remote.User.UserNo, 605, 605014, 605001, param);
            if (res.ResultNo != 0) goto OnExit;

            _paymenttxn = res.Data.Tables[0];
            _paymenttxn.Columns.Add("REFUND", typeof(decimal));

            //gridControl1.DataSource = _paymenttxn;
            ISM.Template.FormUtility.GridLayoutGet(gridView1, _paymenttxn, _layoutfilename);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Төлбөрийн төрөл", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Төлбөрийн хэрэгсэл", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Төлбөрийн хэрэгсэл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Төлсөн дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Буцаах дүн");

            gridView1.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[4].DisplayFormat.FormatString = "#,##0.00";

            gridView1.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[3].DisplayFormat.FormatString = "#,##0.00";

            gridView1.BestFitColumns();

            #endregion
            #region Түрээсийн хэрэгслүүдийг авах

            // Одоохондоо энэ онц шаардлагагүй бх.

            #endregion

            RefreshRefundValue();
            RefreshTotalValue();

        OnExit:
            return res;
        }
        public Result Find(DateTime trandate, string salesno)
        {
            Result res = null;

            _trandate = trandate;
            _salesno = salesno;
            
            res = DataRefresh();
            
            return res;
        }

        public void RefreshRefundValue()
        {
            //numRefund.EditValue = 0;
            //if (_paymenttxn == null) return;

        }
        public void RefreshTotalValue()
        {
            //decimal sum = _paymenttxn.AsEnumerable().Sum(r => Static.ToDecimal(r.Field<object>("REFUND")));
            //numRefund.EditValue = sum;
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

        public Result MakeRefund(string salesno, decimal custno, string prodno, int prodtype, decimal price, decimal qty)
        {
            object[] param = new object[] {
                _core.TxnDate, salesno, _core.POSNo, _core.AreaCode
                , custno, prodno, prodtype, price, qty
                , null
            };
            
            // Төлбөр буцаах
            ArrayList rows = new ArrayList();
            foreach (DataRow r in _paymenttxn.Rows)
            {
                decimal refund = Static.ToDecimal(r["REFUND"]);
                if (refund != 0)
                {
                    object[] row = new object[] { 
                    salesno
                    , 0 /*paymentno*/
                    , r["PAYMENTTYPE"]  /*paymenttype*/
                    , 0 /*cash txn*/
                    , -1 * Static.ToDecimal (r["REFUND"])
                    , "R" /*flag=Refund txn*/
                    , r["REGISTERNO"]
                    , _core.RemoteObject.User.AreaCode
                    , _core.POSNo };
                    rows.Add(row);
                }
            }
            param[9] = rows.ToArray();
            Result res = _remote.Connection.Call(_remote.User.UserNo, 605, 605016, 605016, param);
            this._result = res;
            return res;
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

    }
}