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

namespace InfoPos.foo_panels
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
        int _seqno = 0;

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

        private string _salesno = null;
        public string SalesNo
        {
            get { return _salesno; }
            set { _salesno = value; }
        }

        #endregion
        #region Constructors

        public ucPayment()
        {
            InitializeComponent();
            this.ResizeRedraw = true;

            gridView1.OptionsBehavior.ReadOnly = false;
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            //gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);

            // зурган багана нэмж оруулахад энэ үзэгдлийг зарлаж дотор нь зургаа set хийнэ.
            gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView1_RowCellClick);
            gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanged);
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
        }

        void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.AbsoluteIndex == 3)
            {
                CalcAmount();
            }
        }

        #endregion
        #region Control Events

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
            OnEventChoose();
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
                // there is no touch controls...
            }
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Төрөл", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Төлбөрийн нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Данс", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Төлөх");

            gridView1.Columns[0].OptionsColumn.ReadOnly = true;
            gridView1.Columns[1].OptionsColumn.ReadOnly = true;
            gridView1.Columns[2].OptionsColumn.ReadOnly = true;
            gridView1.Columns[3].OptionsColumn.ReadOnly = false;

            gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            gridView1.Columns[2].OptionsColumn.AllowEdit = false;
            gridView1.Columns[4].OptionsColumn.AllowEdit = false;
        }
        public Result DataRefresh()
        {
            #region Validation

            //if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters

            object[] param = new object[]{
                _salesno, _remote.User.UserNo
            };

            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 500, 500007, 500007, 0, _pagerows, param);
                if (res.ResultNo == 0)
                {
                    #region Төлбөрийн төрлийн жагсаалт бэлдэх
                    //PageSet(pageno, res);
                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[1], _layoutfilename);

                    #region Add picture column

                    RepositoryItemPictureEdit ri = new RepositoryItemPictureEdit();
                    gridControl1.RepositoryItems.Add(ri);

                    DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                    col.VisibleIndex = gridView1.Columns.Count;
                    col.Caption = "...";
                    col.FieldName = string.Format("PIC");
                    col.ColumnEdit = ri;
                    col.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                    col.OptionsColumn.ReadOnly = true;
                    col.Width = 32;

                    gridView1.Columns.Add(col);

                    #endregion

                    DataColumnRefresh();
                    #endregion
                    #region Борлуулалтын дүнгүүдийг олгох

                    DataTable dtAmounts = res.Data.Tables[0];

                    _sales = Static.ToDecimal(dtAmounts.Rows[0]["salesamount"]);
                    _discount = Static.ToDecimal(dtAmounts.Rows[0]["discount"]);
                    _vat = Static.ToDecimal(dtAmounts.Rows[0]["vat"]);
                    _total = Static.ToDecimal(dtAmounts.Rows[0]["totalamount"]);
                    _prepaid = Static.ToDecimal(dtAmounts.Rows[0]["paid"]);
                    _seqno = Static.ToInt(dtAmounts.Rows[0]["seqno"]);

                    numSales.EditValue = _sales;
                    numDiscount.EditValue = _discount;
                    numVat.EditValue = _vat;
                    //numPenalty.EditValue = penalty;

                    numTotal.EditValue = _total;
                    numPrepaid.EditValue = _prepaid;

                    numRemain.EditValue = _total - _prepaid;

                    //if (_pageno < _pagecount)
                    //    lblPage.Text = string.Format("Хуудас: {0}/{1}+", _pageno, _pagecount);
                    //else
                    //    lblPage.Text = string.Format("Хуудас: {0}/{1}", _pageno, _pagecount);
                    #endregion
                }
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
            gridControl1.DataSource = null;
        }
        public Result Find(string salesno)
        {
            Result res = null;

            _salesno = salesno;
            res = DataRefresh();

            return res;
        }

        public void ShowKeyboard(int rowhandle)
        {
            if (TouchKeyboard == null) return;

            if (rowhandle < gridView1.RowCount)
            {
                //gridView1.SelectRow(rowhandle);
                gridView1.FocusedRowHandle = rowhandle;
                DialogResult res = TouchKeyboard.ShowKeyboard(gridView1, rowhandle, 3);
                if (res == DialogResult.OK)
                {
                    if (rowhandle + 1 < gridView1.RowCount)
                        ShowKeyboard(rowhandle + 1);
                }
            }
        }
        public void CalcAmount()
        {
            decimal paid = 0;
            for (int r = 0; r < gridView1.RowCount; r++)
            {
                decimal d = Static.ToDecimal(gridView1.GetRowCellValue(r, "PAID"));
                paid += d;
            }
            numPaid.EditValue = paid;
            numDiff.EditValue = _total - _prepaid - paid;
        }
        public Result Payment()
        {
            Result res = null;

            DataTable dt = (DataTable)gridControl1.DataSource;
            object[] param = new object[] { _salesno, _seqno + 1, dt };

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                , 500, 500201, 500201, param);

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
                    DataRow currentrow = gridView1.GetFocusedDataRow();
                    EventChoose(currentrow);
                }
            }
            catch
            { }
        }

        #endregion
    }
}
