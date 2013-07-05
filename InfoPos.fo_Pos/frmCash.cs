using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;

using ISM.Template;
using EServ.Shared;
using ISM.CUser;
using ISM.Touch;

namespace InfoPos.Pos
{
    public partial class frmCash : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        #region Internal variables

        private DataTable _cashdata = null;
        private DataTable _cashgrid = null;

        private int _posstatus = 0;
        private string _posdesc = null;
        private int _shiftno = 1;
        private int _shiftuserno = 0;
        private string _shiftusername = null;
        private decimal _debt = 0;
        
        #endregion

        #region Properties

        Image _imgEdit = null;
        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;

        private ISM.CUser.Remote _remote = null;
        public ISM.CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }

        private ISM.Touch.TouchKeyboard _touchkeyboard = null;
        public ISM.Touch.TouchKeyboard TouchKeyboard
        {
            get { return _touchkeyboard; }
            set { _touchkeyboard = value; }
        }

        public DataRow CurrentRow
        {
            get { return null; /* gvwCashOpen.GetFocusedDataRow();*/ }
        }

        private string _layoutfilename = "";
        public string LayoutFileName
        {
            get { return _layoutfilename; }
        }

        #endregion
        #region Menu Functions
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _touchkeyboard = new ISM.Touch.TouchKeyboard();
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;
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
                    case "fo_shift_supply"://Кассын зузаатгал
                        SubMenu_Supply();
                        break;
                    case "fo_shift_draw"://Касс тушаах
                        SubMenu_Draw();
                        break;
                    case "fo_shift_open"://Ээлж нээх
                        SubMenu_Open();
                        break;
                    case "fo_shift_close"://Ээлж хаах
                        SubMenu_Close();
                        break;

                    case "fo_pos_open": //ПОС нээх
                        SubMenu_PosOpen();
                        break;
                    case "fo_pos_close": //ПОС хаах
                        SubMenu_PosClose();
                        break;

                    case "fo_pos_print":
                        SubMenu_PosPrint();
                        break;

                    case "fo_pos_repbrief":
                        SubMenu_ReportBrief();
                        break;
                    case "fo_pos_repcash":
                        SubMenu_CashReport();
                        break;

                    case "fo_shift_exit":
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

        private void SubMenu_Supply()
        {
            string success_text = null;
            Result res = null;
            DataTable dt = (DataTable)gridControl1.DataSource;

            #region Validation

            if (dt == null)
            {
                res = new Result(2110010, "Энэ үйлдлийг хийх боломжгүй байна.");
                goto OnExit;
            }

            var query = from row in dt.AsEnumerable()
                        where Static.ToDecimal(row.Field<object>("QTY2")) < 0
                        select row;

            if (query != null && query.Count() > 0)
            {
                res = new Result(2110011, "Дэвсгэртийн тоо ширхэг хасах утгатай байж болохгүй. Оруулсан дэвсгэртүүдээ шалгана уу.");
                goto OnExit;
            }

            decimal total = dt.AsEnumerable().Sum(r => Static.ToDecimal(r.Field<object>("QTY2")));
            if (total <= 0)
            {
                res = new Result(2110011, "Дэвсгэртүүдээ оруулна уу.");
                goto OnExit;
            }
            #endregion
            #region Серверрүү илгээх

            object[] param = new object[] { _core.POSNo,_shiftno, dt };
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211003, 211003, param);
            if (res.ResultNo != 0) goto OnExit;

            InitPos();

            success_text = string.Format("Дэвсгэртүүдийг амжилттай орууллаа.");

            #endregion

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        private void SubMenu_Draw()
        {
            string success_text = null;
            Result res = null;
            DataTable dt = (DataTable)gridControl1.DataSource;

            #region Validation

            if (dt == null)
            {
                res = new Result(2110010, "Энэ үйлдлийг хийх боломжгүй байна.");
                goto OnExit;
            }

            var query = from row in dt.AsEnumerable()
                        where Static.ToDecimal(row.Field<object>("QTY2")) < 0
                        select row;

            if (query != null && query.Count() > 0)
            {
                res = new Result(2110011, "Дэвсгэртийн тоо ширхэг хасах утгатай байж болохгүй. Оруулсан дэвсгэртүүдээ шалгана уу.");
                goto OnExit;
            }

            decimal total = dt.AsEnumerable().Sum(r => Static.ToDecimal(r.Field<object>("QTY2")));
            if (total <= 0)
            {
                res = new Result(2110011, "Дэвсгэртүүдээ оруулна уу.");
                goto OnExit;
            }
            #endregion

            #region Серверрүү илгээх

            object[] param = new object[] { _core.POSNo, _shiftno, dt };
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211004, 211004, param);
            if (res.ResultNo != 0) goto OnExit;

            InitPos();

            success_text = string.Format("Дэвсгэртүүдийг амжилттай орууллаа.");

            #endregion

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        private void SubMenu_Open()
        {
            string success_text = null;
            Result res = null;
            //DataTable dt = (DataTable)gridControl1.DataSource;

            #region Баталгаажуулалт

            string confirm = string.Format("Та {0} дугаартай шинээр ээлж нээхдээ итгэлтэй байна уу?", _shiftno + 1);
            if (!ISM.Template.FormUtility.ValidateConfirm(confirm)) goto OnExit;

            #endregion

            #region Серверрүү илгээх

            object[] param = new object[] { _core.POSNo };
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211005, 211005, param);
            if (res.ResultNo != 0) goto OnExit;

            _shiftno = Static.ToInt (res.Param[0]);
            _core.ShiftNo = _shiftno;
            _core.ShiftStatus = 1;

            InitPos();

            success_text = string.Format("[{0}] дугаартай ээлж амжилттай нээгдлээ.", _shiftno);

            #endregion

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        private void SubMenu_Close()
        {
            string success_text = null;
            Result res = null;
            //DataTable dt = (DataTable)gridControl1.DataSource;

            #region Баталгаажуулалт

            string confirm = string.Format("Та {0} дугаартай ээлж хаахдаа итгэлтэй байна уу?", _shiftno);
            if (!ISM.Template.FormUtility.ValidateConfirm(confirm)) goto OnExit;

            #endregion

            #region Серверрүү илгээх

            object[] param = new object[] { _core.POSNo, _debt /* numCashRemain.EditValue */ };
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211006, 211006, param);
            if (res.ResultNo != 0) goto OnExit;

            InitPos();

            _core.ShiftStatus = 2;

            success_text = string.Format("[{0}] дугаартай ээлж амжилттай хаагдлаа.", _shiftno);

            #endregion

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        private void SubMenu_PosOpen()
        {
            string success_text = null;
            Result res = null;

            #region Баталгаажуулалт

            string confirm = string.Format("Та {0} өдрийн гүйлгээг нээхдээ итгэлтэй байна уу?", _core.TxnDate.ToString("yyyy-MM-dd"));
            if (!ISM.Template.FormUtility.ValidateConfirm(confirm)) goto OnExit;

            #endregion
            #region Серверрүү илгээх

            object[] param = new object[] { _core.POSNo };
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211007, 211007, param);
            if (res.ResultNo != 0) goto OnExit;

            InitPos();

            _core.ShiftStatus = 0;

            success_text = string.Format("{0} өдрийн гүйлгээг амжилттай нээлээ.", _core.TxnDate.ToString("yyyy-MM-dd"));

            #endregion

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        private void SubMenu_PosClose()
        {
            string success_text = null;
            Result res = null;

            #region Баталгаажуулалт

            string confirm = string.Format("Та {0} өдрийн гүйлгээг хаахдаа итгэлтэй байна уу?", _core.TxnDate.ToString("yyyy-MM-dd"));
            if (!ISM.Template.FormUtility.ValidateConfirm(confirm)) goto OnExit;

            #endregion
            #region Серверрүү илгээх

            object[] param = new object[] { _core.POSNo };
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211008, 211008, param);
            if (res.ResultNo != 0) goto OnExit;

            InitPos();

            _core.ShiftStatus = 3;

            success_text = string.Format("{0} өдрийн гүйлгээг амжилттай хаалаа.", _core.TxnDate.ToString("yyyy-MM-dd"));

            #endregion

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }

        private void SubMenu_PosPrint()
        {
            //_posstatus = 3;
            if (_posstatus != 2 && _posstatus != 3)
            {
                _core.AlertShow("Билл хэвлэх", "Ээлж эсвэл ПОС хаагдсан байх шаардлагатай.", 2);
                return;
            }

            frmPosClosureReport frm = new frmPosClosureReport(_core);
            frm.StartPosition = FormStartPosition.CenterScreen;

            if (_posstatus == 2 /*shift is closed*/) frm.ShiftClosure_PrepareBill(_core.POSNo, _shiftno);
            else if (_posstatus == 3 /*pos is closed*/) frm.PosClosure_PrepareBill(_core.POSNo);
            frm.ShowDialog();

            frm.Dispose();
        }

        private void SubMenu_ReportBrief()
        {
            frmSalesReportBrief frm = new frmSalesReportBrief (_core);
            frm.StartPosition= FormStartPosition.CenterScreen;
            frm.ShowDialog();

        OnExit:
            ;
            //ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }
        private void SubMenu_CashReport()
        {
            frmCashReportBrief frm = new frmCashReportBrief(_core);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        #endregion
        #region Constructor and Events
        public frmCash()
        {
            InitializeComponent();

            InitGrid();
        }
        private void frmCash_Load(object sender, EventArgs e)
        {
            _core.MainForm_HeaderSet(0, "Ээлжийн дугаар", "");
            _core.MainForm_HeaderSet(1, "Ээлжийн төлөв", "");
            _core.MainForm_HeaderSet(2, "Ээлжийн ажилтан", "");

            Result res = InitPos();
            ISM.Template.FormUtility.ValidateQuery(res);

        }
        #endregion
        #region Business Functions

        public void InitGrid()
        {
            _cashgrid = new DataTable();
            _cashgrid.Columns.Add("ID", typeof(string));
            _cashgrid.Columns.Add("GROUP", typeof(string));
            _cashgrid.Columns.Add("NAME", typeof(string));
            _cashgrid.Columns.Add("AMOUNT", typeof(decimal));
            _cashgrid.Columns.Add("COUNT", typeof(int));

            gridControl3.DataSource = _cashgrid;

            ISM.Template.FormUtility.Column_SetCaption(ref gridView3, 0, "Id",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView3, 1, "Бүлэг");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView3, 2, "Нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView3, 3, "Дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView3, 4, "Тоо");

            gridView3.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView3.Columns[3].DisplayFormat.FormatString = "#,##0.00";

            gridView3.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView3.Columns[4].DisplayFormat.FormatString = "#,##0";

            gridView3.RowHeight = 28;
            gridView3.Columns[1].GroupIndex = 0;
            gridView3.Columns[1].GroupFormat.FormatString = "{0}";

        }
        public void BuildGridData(DataTable dt)
        {
            _cashgrid.Rows.Clear();

            if (dt == null || dt.Rows.Count <= 0) return;
            
            DataRow r = dt.Rows[0];

            _cashgrid.Rows.Add("sales_total1", "Борлуулалтын дүн", "Борлуулалт", r["TOTAL_AMOUNT"], r["SALES_COUNT"]);
            _cashgrid.Rows.Add("sales_total2", "Борлуулалтын дүн", "Буцаалт", r["REFUND_AMOUNT"], r["REFUND_COUNT"]);
            _cashgrid.Rows.Add("sales_total3", "Борлуулалтын дүн", "Цэвэр дүн"
                , Static.ToDecimal(r["TOTAL_AMOUNT"]) + Static.ToDecimal(r["REFUND_AMOUNT"])
                , Static.ToDecimal(r["SALES_COUNT"]) + Static.ToDecimal(r["REFUND_COUNT"]));

            _cashgrid.Rows.Add("sales_income1", "Борлуулалтын орлого", "Бэлнээр", r["CASH_AMOUNT"], r["CASH_COUNT"]);
            _cashgrid.Rows.Add("sales_income2", "Борлуулалтын орлого", "Картаар", r["CARD_AMOUNT"], r["CARD_COUNT"]);
            _cashgrid.Rows.Add("sales_income3", "Борлуулалтын орлого", "Бусад", r["OTHER_AMOUNT"], r["OTHER_COUNT"]);
            _cashgrid.Rows.Add("sales_income4", "Борлуулалтын орлого", "Нийт"
                , Static.ToDecimal(r["CASH_AMOUNT"]) + Static.ToDecimal(r["CARD_AMOUNT"]) + Static.ToDecimal(r["OTHER_AMOUNT"])
                , Static.ToDecimal(r["CASH_COUNT"]) + Static.ToDecimal(r["CARD_COUNT"]) + Static.ToDecimal(r["OTHER_COUNT"]));


            _cashgrid.Rows.Add("cash_total1", "Кассын мэдээ", "Зузаатгал", r["CASHIN_AMOUNT"], r["CASHIN_COUNT"]);
            _cashgrid.Rows.Add("cash_total1", "Кассын мэдээ", "Борлуулалт", r["CASH_AMOUNT"], r["CASH_COUNT"]);
            _cashgrid.Rows.Add("cash_total1", "Кассын мэдээ", "Кассын дүн"
                , Static.ToDecimal(r["CASHIN_AMOUNT"]) + Static.ToDecimal(r["CASH_AMOUNT"])
                , Static.ToDecimal(r["CASHIN_COUNT"]) + Static.ToDecimal(r["CASH_COUNT"]));
            _cashgrid.Rows.Add("cash_total1", "Кассын мэдээ", "Тушаасан", -Static.ToDecimal(r["CASHOUT_AMOUNT"]), r["CASHOUT_COUNT"]);
            _cashgrid.Rows.Add("cash_total1", "Кассын мэдээ", "Илүү/Дутуу"
                , Static.ToDecimal(r["CASHIN_AMOUNT"]) + Static.ToDecimal(r["CASH_AMOUNT"]) + Static.ToDecimal(r["CASHOUT_AMOUNT"])
                , Static.ToDecimal(r["CASHIN_COUNT"]) + Static.ToDecimal(r["CASH_COUNT"]) + Static.ToDecimal(r["CASHOUT_COUNT"]));

            _debt = Static.ToDecimal(r["CASHIN_AMOUNT"]) + Static.ToDecimal(r["CASH_AMOUNT"]) + Static.ToDecimal(r["CASHOUT_AMOUNT"]);

            gridView3.Columns[2].Width = 220;
            gridView3.Columns[3].Width = 150;
            gridView3.Columns[4].Width = 60;

            gridView3.ExpandAllGroups();
        }
        public Result InitPos()
        {
            Result res = null;
            try
            {
                //Txn211001
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }

                object[] param = new object[] { _core.POSNo, _core.AreaCode };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211001, 211001, param);
                if (res.ResultNo != 0) goto OnExit;
                
                #endregion
                #region Ээлжтэй хэрэглэгчийг шалгах

                DataTable dt = res.Data.Tables[0];
                _posstatus = Static.ToInt(dt.Rows[0]["STATUS"]);
                _shiftno = Static.ToInt(dt.Rows[0]["SHIFTNO"]);
                _shiftuserno = Static.ToInt(dt.Rows[0]["SHIFTUSERNO"]);
                _shiftusername = Static.ToStr(dt.Rows[0]["USERNAME"]);

                if (_posstatus == 0) _posdesc= "ПОС НЭЭЛТТЭЙ";
                else if (_posstatus == 1) _posdesc= "ЭЭЛЖ НЭЭЛТТЭЙ";
                else if (_posstatus == 2) _posdesc= "ЭЭЛЖ ХААГДСАН";
                else if (_posstatus == 3) _posdesc= "ПОС ХААГДСАН!";

                _core.MainForm_HeaderSet(0, "Ээлжийн дугаар", _shiftno.ToString());
                _core.MainForm_HeaderSet(1, "Ээлжийн төлөв", _posdesc);
                _core.MainForm_HeaderSet(2, "Ээлжийн ажилтан", _shiftusername);
                

                if (_posstatus==1 /*ээлж нээлттэй*/ && _shiftuserno != _core.RemoteObject.User.UserNo /*өөр хэрэглэгч нээсэн*/)
                {
                    //txtShiftNo.EditValue = string.Format("{0} [{1}]", _shiftno, _shiftusername);
                    //txtShiftNo.ForeColor = Color.Red;
                    lblWarning.Visible = true;
                }
                else
                {
                    // Ээлжийн төлвийг харуулах.

                    param = new object[] { _core.POSNo, _shiftno };
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211002, 211002, param);
                    if (res.ResultNo != 0) goto OnExit;

                    #region Дэлгэцийн мэдээллүүдийг дүүргэх

                    _cashdata = res.Data.Tables[0];
                    BuildGridData(_cashdata);

                    dt = res.Data.Tables[1];
                    dt.Columns.Add("QTY2", typeof(decimal));
                    gridControl1.DataSource = dt;

                    RepositoryItemCalcEdit ri = new RepositoryItemCalcEdit();
                    gridView1.Columns["QTY2"].ColumnEdit = ri;
                    ri.Buttons.Clear();
                    ri.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    ri.DisplayFormat.FormatString = "#,##0";

                    ri.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    ri.EditFormat.FormatString = "#,##0";


                    gridView1.OptionsBehavior.Editable = true;
                    gridView1.OptionsBehavior.ReadOnly = false;
                    gridView1.OptionsView.ColumnAutoWidth = true;
                    gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                    gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[1].OptionsColumn.ReadOnly = true;
                    gridView1.Columns[1].OptionsColumn.AllowEdit = false;


                    ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Дэвсгэрт");
                    ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Тоо ширхэг");
                    ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Оруулах утга");

                    gridControl2.DataSource = res.Data.Tables[2];
                    gridView2.OptionsView.ColumnAutoWidth = true;
                    ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 0, "Ээлж №");
                    ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 1, "Хэрэглэгч",true);
                    ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 2, "Хэрэглэгч");
                    ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 3, "Огноо");


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

        #endregion

    }
}
