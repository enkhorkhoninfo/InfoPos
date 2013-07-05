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
    public partial class frmRefundSales : DevExpress.XtraEditors.XtraForm
    {
        #region Comment!

        /*******************************************************************
         * Энэ дэлгэц нь борлуулалтыг бүхэлд нь эсвэл төлбөрийг хэсэгчилж буцаана.
         * Хэрэв 
         * 
         *******************************************************************/

        #endregion

        #region Internal variables

        Image _imgEdit = null;

        string _paymentno = null;
        decimal _totalamount = 0;
        decimal _salesamount = 0;
        decimal _discount = 0;
        decimal _vat = 0;
        decimal _prepaid = 0;
                
        DataTable _paymentdetail = null; //Борлуулалтын үндсэн мэдээлэл. Sales тэйблээс авна.
        DataTable _paymenttypes = null; //Хэрэглэгчийн боломжит төлбөрийн төрлүүд. Төлбөр буцаах үед төлбөрийн эрхийг шалгахад хэрэглэнэ.
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

        #endregion
        #region Constructors

        public frmRefundSales(InfoPos.Core.Core core, DateTime trandate, string salesno)
        {
            InitializeComponent();
            this.Core = core;
            this.TranDate = trandate;
            this.SalesNo = salesno;
            this.ResizeRedraw = true;

            #region Events
            this.FormClosing += frmPayment_FormClosing;
            #endregion
            #region Numeric field format

            numSales.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numSales.Properties.DisplayFormat.FormatString = "#,##0.00";

            numDiscount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numDiscount.Properties.DisplayFormat.FormatString = "#,##0.00";
            
            numVat.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numVat.Properties.DisplayFormat.FormatString = "#,##0.00";
            
            numTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numTotal.Properties.DisplayFormat.FormatString = "#,##0.00";
            
            numPrepaid.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numPrepaid.Properties.DisplayFormat.FormatString = "#,##0.00";
            
            numRemain.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            numRemain.Properties.DisplayFormat.FormatString = "#,##0.00";
            
            //numDiff.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //numDiff.Properties.DisplayFormat.FormatString = "#,##0.00";

            //numBalance.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //numBalance.Properties.DisplayFormat.FormatString = "#,##0.00";

            //numEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //numEdit.Properties.DisplayFormat.FormatString = "#,##0.00";

            #endregion
            #region Gird format
            
            //gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            //gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            //gridView1.OptionsBehavior.ReadOnly = true;
            //gridView1.OptionsBehavior.Editable = false;
            //gridView1.OptionsCustomization.AllowGroup = false;
            //gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            //gridView1.OptionsView.ColumnAutoWidth = false;
            //gridView1.OptionsView.ShowAutoFilterRow = false;
            //gridView1.OptionsView.ShowGroupPanel = false;
            //gridView1.OptionsView.ShowIndicator = false;
            //gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
            //gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            //gridView1.RowHeight = 28;

            //gridControl2.Dock = DockStyle.Fill;

            #endregion
            #region Init functions
            InitGrid();
            #endregion
        }

        void frmPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLayout();
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
                //_touchkeyboard.AddToKeyboard(numEdit);
            }
        }

        #endregion
        #region Button Events

        private void btnRefundSales_Click(object sender, EventArgs e)
        {
            Result res = null;
            #region Validation

            //decimal totalrefund = Static.ToDecimal(numRefund.EditValue);
            //if (totalrefund <= 0)
            //{
            //    res = new Result(9, "Буцаалт хийх дүн буруу байна.");
            //    goto OnExit;
            //}

            #endregion
            #region Confirmation
            string confirm = "";
            confirm = string.Format("Борлуулалтыг бүхэлд нь буцаахдаа итгэлтэй байна уу?");
            if (!ISM.Template.FormUtility.ValidateConfirm(confirm)) return;
            #endregion

            res = MakeRefundSales(_trandate, _salesno);
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

        }
        public void InitValues(decimal totalamount, decimal salesamount, decimal discount, decimal vat, decimal prepaid)
        {
            _totalamount = totalamount;
            _salesamount = salesamount;
            _discount = discount;
            _vat = vat;
            _prepaid = prepaid;

            numTotal.EditValue = _totalamount;
            numSales.EditValue = _salesamount;
            numDiscount.EditValue = _discount;
            numVat.EditValue = _vat;
            numPrepaid.EditValue = _prepaid;

            numRemain.EditValue = _totalamount - _prepaid > 0 ? _totalamount - _prepaid : 0;
        }

        public Result DataRefresh()
        {
            Result res = null;

            #region Prepare parameters

            object[] param = new object[] { _trandate, _salesno, _remote.User.UserNo, _core.POSNo, _core.AreaCode };

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
                numSales.EditValue = _salesamount;
                numDiscount.EditValue = _discount;
                numVat.EditValue = _vat;
                numPrepaid.EditValue = _prepaid;

                numRemain.EditValue = _salesamount - _prepaid > 0 ? _salesamount - _prepaid : 0;
            }
            #endregion
            #region Хэрэглэгчийн боломжит Төлбөрийн төрлүүд

            _paymenttypes = res.Data.Tables[1];

            #endregion

            //RefreshRefundValue();
            //RefreshTotalValue();

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
            //ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
            //ISM.Template.FormUtility.GridLayoutSave(gridView2, _layoutfilename2);
        }

        public Result MakeRefundSales(DateTime trandate, string salesno)
        {
            Result res = null;
            // Борлуулалтыг бүхэлд нь буцаах
            object[] param = new object[] { trandate, salesno, _remote.User.PosNo, _remote.User.AreaCode };
            res = _remote.Connection.Call(_remote.User.UserNo, 605, 605015, 605015, param);
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