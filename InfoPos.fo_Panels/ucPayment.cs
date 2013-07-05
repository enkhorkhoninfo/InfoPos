using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

using EServ.Shared;

namespace InfoPos.Panels
{
    public partial class ucPayment : UserControl
    {
        #region Internal variables

        Image _imgEdit = null;

        decimal _sales = 0;
        decimal _discount = 0;
        decimal _vat = 0;
        decimal _total = 0;
        decimal _prepaid = 0;
        decimal _paidnow = 0;
        decimal _paymentamount = 0;
        int _seqno = 0;
        string contracttype = "";
        DataTable _paymenttable = null;
        DataTable _typetable = null;

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
            get { return null; }
            // gridView1.GetFocusedDataRow(); }
        }

        private string _layoutfilename = "";
        public string LayoutFileName
        {
            get { return _layoutfilename; }
        }

        private string _batchno = null;
        public string BatchNo
        {
            get { return _batchno; }
            set { _batchno = value; }
        }

        #endregion
        #region Constructors

        public ucPayment()
        {
            InitializeComponent();
            this.ResizeRedraw = true;

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
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);


            touchPayType.EventKeyDown +=new ISM.Touch.TouchButtonGroup.delegateKeyDown(touchPayType_EventKeyDown);
            _paymenttable = new DataTable();
            _paymenttable.Columns.Add("PaymentType", typeof(string));
            _paymenttable.Columns.Add("PaymentName", typeof(string));
            _paymenttable.Columns.Add("Amount", typeof(string));
            _paymenttable.Columns.Add("ContractNo", typeof(string));
            _paymenttable.Columns.Add("METHOD", typeof(Int16));
            //_paymenttable.Columns.Add("Төлбөрийн №", typeof(string));
            //_paymenttable.Columns.Add("Дэс дугаар", typeof(string));
            //_paymenttable.Columns.Add("Төлбөрийн төрөл №", typeof(string));
            //_paymenttable.Columns.Add("Төлбөрийн төрөл", typeof(string));
            //_paymenttable.Columns.Add("Төлбөрийн дүн", typeof(string));
            //_paymenttable.Columns.Add("Гэрээ,Ваучир №", typeof(string));
        }
        private void touchPayType_EventKeyDown(Control sender, MouseEventArgs e, ISM.Touch.TouchLinkItem item, ref bool cancel)
        {
            /*
             * Боломжит төлбөрийн төрөл сонгогдоход
             * баруун талд бгаа тект хайрцагт утгыг харуулах.
             */

            txtPayType.EditValue = item.Text;
            txtPayType.Tag = item.Key;
            contracttype = item.Description;
            numCardNo.EditValue = null;
            txtNo.EditValue = null;
            numBalance.EditValue = null;
            numPayment.EditValue = null;

            //2013.02.13 Amaraa
            //if (item.Key == Core.CashPayment)
            //{
            //    disablestate();
            //}
            //else
            //{
            //    if (item.Key == Core.CardPayment)
            //    {
            //        gridControl2.Location = new Point(1, 178);
            //        numCardNo.Visible = true;
            //        numCardNo.Properties.ReadOnly = true;
            //        labelControl13.Visible = true;
            //    }
            //    else
            //    {
            //        if (item.Key == Core.OfflineCardPayment)
            //        {
            //            gridControl2.Location = new Point(1, 178);
            //            numCardNo.Visible = true;
            //            numCardNo.Properties.ReadOnly = false;
            //            labelControl13.Visible = true;
            //        }
            //        else
            //        {
            //            enablestate();
            //        }
            //    }
            //}
        }
        void disablestate()
        {
            labelControl10.Enabled = false;
            txtNo.Enabled = false;
            txtNo.EditValue = null;
            btnCheck.Enabled = false;
            labelControl13.Visible = false;
            numCardNo.Visible = false;
            gridControl2.Location = new Point(1, 157);
        }
        void enablestate()
        {
            labelControl13.Visible = false;
            numCardNo.Visible = false;
            labelControl10.Enabled = true;
            txtNo.Enabled = false;
            btnCheck.Enabled = true;
            gridControl2.Location = new Point(1, 157);
            txtNo.Enabled = true;
        }
        #endregion
        #region Control Events

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.AbsoluteIndex == 3)
            {
                CalcAmount();
            }
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "PIC")
                {
                    ShowKeyboard(e.RowHandle);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //OnEventChoose();

            /*
             * Боломжит төлбөрийн төрөл сонгогдоход
             * баруун талд бгаа тект хайрцагт утгыг харуулах.
             */
        }
        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                try
                {
                    switch (e.Column.FieldName)
                    {
                        case "PIC":
                            e.Value = _imgEdit;
                            break;
                    }
                }
                catch
                { }
            }
        }
        private void ucPayment_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                _imgEdit = _resource.GetImage("navigate_edit");
            }
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(numPayment);
                _touchkeyboard.AddToKeyboard(numCardNo);
                _touchkeyboard.AddToKeyboard(txtNo);
            }
        }
        private void numPaid_EditValueChanged(object sender, EventArgs e)
        {
            numDiff.EditValue = Static.ToDecimal(numPaid.EditValue) - Static.ToDecimal(numRemain.EditValue);
        }
        private void btnPrev_Click_removed(object sender, EventArgs e)
        {
            //if (_pageno > 1)
            //{
            //    Result res = DataRefresh(_pageno - 1);
            //    if (res.ResultNo != 0)
            //        MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
            //}
        }
        private void btnNext_Click_removed(object sender, EventArgs e)
        {
            //if (_pagecount > _pageno)
            //{
            //    Result res = DataRefresh(_pageno + 1);
            //    if (res.ResultNo != 0)
            //        MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
            //}
        }
        
        #endregion
        #region User Functions

        private void PageSet(int pageno, Result res)
        {
            if (res.ResultNo == 0)
            {
                _pageno = res.ResultPageCount;
                if (pageno <= res.ResultPageCount)
                {
                    bool partial = (res.AffectedRows % _pagerows) != 0;
                    _pagecount = partial ? res.ResultPageCount : res.ResultPageCount + 1;
                }
            }
        }
        public void DataColumnRefresh()
        {
            //ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Төрөл", true);
            //ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Төлбөрийн нэр");
            //ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Данс", true);
            ////ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Төлөх", true);

            //gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            //gridView1.Columns[3].OptionsColumn.ReadOnly = false;

            //gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            //gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            //gridView1.Columns[2].OptionsColumn.AllowEdit = false;
            ////gridView1.Columns[4].OptionsColumn.AllowEdit = false;
        }
        public void DataButtonRefresh(DataTable paytypes)
        {
            //pt.typeid,pt.name,pt.suspaccount,0 paid
            touchPayType.Init(paytypes.Rows.Count, 1, 2);
            int rowindex = 1;
            foreach (DataRow row in paytypes.Rows)
            {
                touchPayType.Add(""
                    , Static.ToStr(row["typeid"])
                    , rowindex++
                    , 1
                    , Static.ToStr(row["name"])
                    , Static.ToStr(row["contracttype"])
                    , null, null, null);
            }
            touchPayType.ButtonsDrawChildren("ROOT");
        }
        public Result DataRefresh()
        {
            #region Validation

            //if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters

            object[] param = new object[]{
                _batchno, _remote.User.UserNo
            };

            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 500, 500007, 500007, 0, _pagerows, param);
                if (res.ResultNo == 0)
                {
                    if (res.Data.Tables[1].Rows.Count == 0)
                    {
                        res.ResultNo = 2001;
                        res.ResultDesc = string.Format("{0} дугаартай хэрэглэгч дээр төлбөрийн хэрэгсэл тохируулаагүй байна.\r\n Системийн администратортой холбоо барина уу.", _core.RemoteObject.User.UserNo);
                        return res;
                    }
                    #region Төлбөрийн төрлийн жагсаалт бэлдэх
                    //PageSet(pageno, res);
                    //ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[1], _layoutfilename);
                    _typetable = res.Data.Tables[1];

                    DataColumnRefresh();
                    DataButtonRefresh(_typetable);

                    #endregion
                    #region Борлуулалтын дүнгүүдийг олгох

                    DataTable dtAmounts = res.Data.Tables[0];

                    _sales = Static.ToDecimal(dtAmounts.Rows[0]["totalamount"]);
                    _discount = Static.ToDecimal(dtAmounts.Rows[0]["discount"]);
                    _vat = Static.ToDecimal(dtAmounts.Rows[0]["vat"]);
                    _total = Static.ToDecimal(dtAmounts.Rows[0]["salesamount"]);
                    _prepaid = Static.ToDecimal(dtAmounts.Rows[0]["paid"]);
                    _seqno = Static.ToInt(dtAmounts.Rows[0]["seqno"]);

                    numSales.EditValue = _sales;
                    numDiscount.EditValue = _discount;
                    numVat.EditValue = _vat;

                    numTotal.EditValue = _total;
                    numPrepaid.EditValue = _prepaid;

                    numRemain.EditValue = _total - _prepaid > 0 ? _total - _prepaid : 0;

                    numPaid.EditValue = null;
                    numDiff.EditValue = null;
                    #endregion
                }
                else { MessageBox.Show(res.ResultNo + ":" + res.ResultDesc); }
            }
            else
            {
                res = new Result(1000, "Internal Error: Remote object not set.");
            }
            #endregion

            return res;
        }
        public void Clear()
        {
            //gridControl1.DataSource = null;
        }
        public Result Find(string batchno)
        {
            Result res = null;

            _batchno = batchno;
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
        public void CalcAmount()
        {
            _paidnow = GetTotalPaid();
            numPaid.EditValue = _paidnow;
            numDiff.EditValue = _total - _prepaid - _paidnow;
        }
        /// <summary>
        /// Үнийн жагсаалт
        /// VAT
        /// ТӨЛӨГДӨХ
        /// ӨМНӨ ТӨЛӨГДСӨН
        /// ҮЛДЭГДЭЛ
        /// ТӨЛЖ БУЙ
        /// ХАРИУЛТ
        /// </summary>
        /// <returns></returns>
        public decimal[] GetAmount()
        {
            decimal[] amount=new decimal[6];
            if (numVat.EditValue != null)
                amount[0] = Static.ToDecimal(numVat.EditValue);
            else amount[0] = 0;
            if (numTotal.EditValue != null)
                amount[1] = Static.ToDecimal(numTotal.EditValue);
            else amount[1] = 0;
            if (numPrepaid.EditValue != null)
                amount[2] = Static.ToDecimal(numPrepaid.EditValue);
            else amount[2] = 0;
            if (numRemain.EditValue != null)
                amount[3] = Static.ToDecimal(numRemain.EditValue);
            else amount[3] = 0;
            if (numPaid.EditValue != null)
                amount[4] = Static.ToDecimal(numPaid.EditValue);
            else amount[4] = 0;
            if (numDiff.EditValue != null)
                amount[5] = Static.ToDecimal(numDiff.EditValue);
            else amount[5] = 0;
            return amount;
        }
        public decimal GetTotalPaid()
        {
            decimal paid = 0;
            //for (int r = 0; r < gridView1.RowCount; r++)
            //{
            //    decimal d = Static.ToDecimal(gridView1.GetRowCellValue(r, "PAID"));
            //    paid += d;
            //}
            return paid;
        }
        public void SaveLayout()
        {
            //ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }
        public Result PaymentRegister(string regno, decimal amount, string batchno, string contracttype, string paymentno)
        {
            Result res = new Result();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 500, 500206, 500206, new object[] { batchno, regno, amount, string.Format("{0}-{1}", _core.RemoteObject.User.AreaCode, _core.RemoteObject.User.UserNo), contracttype, paymentno });
            return res;
        }
        public Result Payment()
        {
            Result res = null;

            decimal paid = GetTotalPaid();
            if (paid <= 0)
            {
                res = new Result(9, "Төлбөрийн дүнгээ оруулна уу!");
            }
            else
            {
                DataTable dt = (DataTable)_typetable;
                object[] param = new object[] { _batchno, dt };

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                    , 500, 500201, 500201, param);
            }
            return res;
        }
        public Result PaymentByCash(string batchno, string paytype, decimal amount, decimal chargeamount, string paymentno)
        {
            Result res = null;

            object[] param = new object[] { batchno, paytype, amount, string.Format("{0}-{1}", _core.RemoteObject.User.AreaCode, _core.POSNo), paymentno, chargeamount };

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                , 500, 500203, 500201, param);
            return res;
        }
        public Result PaymentByIPPOS(string batchno, string paytype, decimal amount)
        {
            Result res = null;

            //GCIPPOSF.CTxn t = new GCIPPOSF.CTxn();
            //t.InitAll();

            //string ret = t.Txn1000(Static.ToDouble(amount), "496");
            //MessageBox.Show(ret);

            GCIPPOSF.frmSupervisor frm = new GCIPPOSF.frmSupervisor();
            frm.ShowDialog();

            object[] param = new object[] { batchno, paytype, amount, string.Format("{0}-{1}", _core.RemoteObject.User.AreaCode, _core.POSNo) };

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                , 500, 500203, 500201, param);

            return res;
        }
        public Result PaymentTable(string paymentno)
        {
            Result res = new Result();
            if (_paymenttable != null && _paymenttable.Rows.Count != 0)
            {
                decimal cardpaymentamount = 0;
                foreach (DataRow dr in _paymenttable.Rows)
                {
                    //2013.02.13 Amaraa
                    //if (Static.ToStr(dr["PAYMENTTYPE"]) == _core.CardPayment)
                    //{
                    //    cardpaymentamount = cardpaymentamount + Static.ToDecimal(dr["AMOUNT"]);
                    //}
                }
                try
                {
                    if (cardpaymentamount > 0)
                    {
                        GCIPPOSF.CTxn t = new GCIPPOSF.CTxn();
                        t.InitAll();
                        string ret = t.Txn1000(Static.ToDouble(cardpaymentamount), "496");
                        MessageBox.Show(ret);
                    }
                }
                catch (Exception ex)
                {
                    res.ResultNo = 1;
                    res.ResultDesc = string.Format("{IPPOS оор гүйлгээ хийхэд алдаа гарлаа} : {1}", ex);
                    return res;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 500, 500205, 500205, new object[] { _batchno, _paymenttable, string.Format("{0}-{1}", _core.RemoteObject.User.AreaCode, _core.POSNo), paymentno, Static.ToDecimal(numDiff.EditValue) });
                if (res.ResultNo == 0)
                {
                    object[] obj = new object[3];
                    obj[0] = numPaid.EditValue;
                    obj[1] = numDiff.EditValue;
                    obj[2] = res.Param[0];
                    res.Param = obj;
                    return res;
                }
                else return res;
            }
            else
            {
                res.ResultNo = 1;
                res.ResultDesc = "Төлбөрийн мэдээлэлээ оруулна уу.";
            }

            return res;
        }
        #endregion
        #region User Events

        public delegate void delegateEventChoose(DataRow currentrow);
        public event delegateEventChoose EventChoose;
        public void OnEventChoose()
        {
            try
            {
                if (EventChoose != null)
                {
                    //DataRow currentrow = gridView1.GetFocusedDataRow();
                    //EventChoose(currentrow);
                }
            }
            catch
            { }
        }

        #endregion
        #region[BTN]
        private void btnCheck_Click(object sender, EventArgs e)
        {
            Result res = null;
            try
            {
                string contractno = Static.ToStr(txtNo.EditValue);
                if (string.IsNullOrEmpty(Static.ToStr(txtPayType.EditValue)))
                {
                    res = new Result(9, "Төлбөрийн төрлөө сонгоно уу!");
                    goto OnExit;
                }

                object[] param = new object[] { contractno, contracttype };

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                    , 500, 500204, 500201, param);
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    if (res.Data == null)
                    {
                        _core.AlertShow("Мэдээлэл", string.Format("[{0}] - Ийм дугаартай бүртгэл олдсонгүй.", contractno));
                    }
                    else
                    {
                        if (res.Data.Tables[0].Rows.Count == 0)
                        {
                            _core.AlertShow("Мэдээлэл", string.Format("[{0}] - Ийм дугаартай бүртгэл олдсонгүй.", contractno));
                        }
                    }
                    numBalance.EditValue = res.Data.Tables[0].Rows[0]["BALANCE"];
                    if (res.Data.Tables[0].Rows[0]["METHOD"] != null)
                        numBalance.Tag = res.Data.Tables[0].Rows[0]["METHOD"];
                    if (Static.ToInt(numBalance.EditValue) == 0)
                    {
                        _core.AlertShow("Алдаа гарлаа", "Бүртгэл үлдэгдэлгүй байна.");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Static.ToStr(txtPayType.EditValue)))
            {
                _core.AlertShow("Алдаа гарлаа", "Бүртгэл үлдэгдэлгүй байна.");
                return;
            }
            //2013.02.13 Amaraa
            //if (Static.ToStr(txtPayType.Tag) != Core.CashPayment && Static.ToStr(txtPayType.Tag) != Core.CardPayment && Static.ToStr(txtPayType.Tag) != Core.OfflineCardPayment)
            //{
            //    if (txtNo.EditValue == null || txtNo.Text == "")
            //    {
            //        _core.AlertShow("Алдаа гарлаа", "Бүртгэл үлдэгдэлгүй байна.");
            //        return;
            //    }
            //}
            if (numPayment.EditValue == null || Static.ToDecimal(numPayment.EditValue) == 0)
            {
                _core.AlertShow("Алдаа гарлаа", "Бүртгэл үлдэгдэлгүй байна.");
                return;
            }
            if (numRemain.EditValue == null || Static.ToDecimal(numRemain.EditValue) == 0)
            {
                _core.AlertShow("Мэдээлэл","Төлбөр бүрэн төлөгдсөн байна. Дахин төлбөр хийх боломжгүй.");
                return;
            }
            numPaid.EditValue = Static.ToDecimal(numPaid.EditValue) + Static.ToDecimal(numPayment.EditValue);
            DataRow dr = _paymenttable.NewRow();
            dr["PaymentType"] = txtPayType.Tag;
            dr["PaymentName"] = txtPayType.EditValue;
            dr["Amount"] = numPayment.EditValue;
            dr["ContractNo"] = txtNo.EditValue;
            if (numBalance.Tag != null)
                dr["METHOD"] = numBalance.Tag;
            _paymenttable.Rows.Add(dr);

            

            gridControl2.DataSource = null;
            gridControl2.DataSource = _paymenttable;
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            DataRow dr = gridView2.GetFocusedDataRow();
            if (dr != null)
            {
                numPaid.EditValue = Static.ToDecimal(numPaid.EditValue) - Static.ToDecimal(dr["AMOUNT"]);
                _paymenttable.Rows.Remove(dr);
                gridControl2.DataSource = null;
                gridControl2.DataSource = _paymenttable;
            }
        }
        #endregion
    }
}
