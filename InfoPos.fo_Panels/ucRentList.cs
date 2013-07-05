using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using EServ.Shared;

namespace InfoPos.Panels
{
    public partial class ucRentList : UserControl
    {
        #region Internal variables

        Image imgExport = null;
        Image imgImport = null;
        Image imgDone = null;
        Image imgBroke = null;
        Image imgReturn = null;
        private string invids = "";
        public string _invids
        {
            get { return invids; }
        }
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
                try
                {
                    if (value != null)
                    {
                        _core = value;
                        if (_remote == null) _remote = _core.RemoteObject;
                        if (_resource == null) _resource = _core.Resource;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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

        public DataTable Data
        {
            get { return (DataTable)gridControl1.DataSource; }
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

        private string _batchno = null;
        public string BatchNo
        {
            get { return _batchno; }
            set { _batchno = value; }
        }

        private string _custno = null;
        public string CustNo
        {
            get { return _custno; }
            set { _custno = value; }
        }

        private string _serialno = null;
        public string SerialNo
        {
            get { return _serialno; }
            set { _serialno = value; }
        }
        private int _userno = 0;
        public int Userno
        {
            get { return _userno; }
            set { _userno = value; }
        }

        private int _userstate = 0;
        public int UserState
        {
            get { return _userstate; }
            set { _userstate = value; }
        }
        #endregion
        #region Constructors

        public ucRentList()
        {
            try
            {
                InitializeComponent();
                this.ResizeRedraw = true;

                gridView1.OptionsBehavior.ReadOnly = true;
                gridView1.OptionsBehavior.Editable = false;
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
                gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView1_RowCellClick);
                _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
        #region Control Events

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            Result res = null;
            try
            {       
                DataRow row = gridView1.GetFocusedDataRow();
                int rentstatus = Static.ToInt(row["rentstatus"]);
                if (e.Column.FieldName == "PIC")
                {
                    switch (rentstatus)
                    {
                        case 0: res = Deliver(); break;
                        case 1: res = Receive(); break;
                    }
                    ISM.Template.FormUtility.ValidateQuery(res);
                }
                else if (e.Column.FieldName == "RETURN")
                {
                    Return();
                }
            }
            catch
            { }
        }
        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.IsGetData)
                {
                    switch (e.Column.FieldName)
                    {
                        case "PIC":
                            DataRow row = gridView1.GetDataRow(e.RowHandle);
                            int rentstatus = Static.ToInt(row["rentstatus"]);

                            switch (rentstatus)
                            {
                                case 0: e.Value = imgExport; break;
                                case 1: e.Value = imgImport; break;
                                case 2: e.Value = imgDone; break;
                                case 9: e.Value = imgBroke; break;
                            }
                            break;
                        case "RETURN":
                            e.Value = imgReturn;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ucRentList_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                imgExport = _resource.GetImage("image_forwardmail");
                imgImport = _resource.GetImage("image_replymail");
                imgDone = _resource.GetImage("image_ok");
                imgReturn = _resource.GetImage("edit_undo");
                imgBroke = _resource.GetImage("object_delete");

            }
            if (_touchkeyboard != null)
            {
                // there is no touch controls...
            }
            gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);
        }

        void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                int deliver = Static.ToInt(View.GetRowCellDisplayText(e.RowHandle, View.Columns["DELIVERUSERSTATE"]));
                int receive = Static.ToInt(View.GetRowCellDisplayText(e.RowHandle, View.Columns["RECEIVEUSERSTATE"]));
                switch(deliver)
                {
                    case 1: e.Appearance.BackColor = Color.DeepSkyBlue; break;
                    case 2: e.Appearance.BackColor = Color.DarkOrange; break;
                    case 3: e.Appearance.BackColor = Color.LimeGreen; break;
                    case 4: e.Appearance.BackColor = Color.HotPink; break;
                }
                switch (receive)
                {
                    case 1: e.Appearance.BackColor2 = Color.DeepSkyBlue; break;
                    case 2: e.Appearance.BackColor2 = Color.DarkOrange; break;
                    case 3: e.Appearance.BackColor2 = Color.LimeGreen; break;
                    case 4: e.Appearance.BackColor2 = Color.HotPink; break;
                }
            }
        }
        public void RegState()
        {
            gridView1.Columns["PIC"].Visible = false;
            gridView1.Columns["RETURN"].Visible = false;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            _pagecount = 0;
            Result res = DataRefresh(1);
            if (res.ResultNo != 0)
                MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
        }
        private void btnNew_Click(object sender, EventArgs e)
        {

        }
        private void btnChoose_Click(object sender, EventArgs e)
        {

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_pageno > 1)
            {
                Result res = DataRefresh(_pageno - 1);
                if (res.ResultNo != 0)
                    MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_pagecount > _pageno)
            {
                Result res = DataRefresh(_pageno + 1);
                if (res.ResultNo != 0)
                    MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
            }
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Харилцагчийн №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Таг №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Дэс №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Хэрэгслийн №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Хэрэгслийн нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Баркод");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Торгууль");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Олгосон");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Авсан");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Төлөв", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Борлуулалтын №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "Олгосон хэрэглэгч", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 12, "Хүлээн авсан хэрэглэгч", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 13, "Олгосон хэрэглэгч байр", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 14, "Хүлээн авсан хэрэглэгч байр", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 15, "Үйлдэл");
            gridView1.BestFitColumns();
        }
        public Result DataRefresh(int pageno)
        {
            Result res = null;
            try
            {
                #region Validation

                if (pageno < 1) pageno = 1;

                #endregion
                #region Prepare parameters


                #endregion
                #region Call server
                if (_remote != null)
                {
                    invids = "";
                    object[] param = new object[] { _serialno, _salesno };
                    gridControl1.DataSource = null;
                    res = _remote.Connection.Call(_remote.User.UserNo, 500, 500003, 500003, pageno - 1, _pagerows, param);
                    if (ISM.Template.FormUtility.ValidateQuery(res))
                    {
                        PageSet(pageno, res);
                        ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layoutfilename);
                        foreach (DataRow dr in res.Data.Tables[0].Rows)
                        {
                            if (invids.Length == 0)
                                invids = Static.ToStr(dr["PRODNO"]);
                            else
                                if (!invids.Contains(Static.ToStr(dr["PRODNO"])))
                                    invids = invids + ";" + Static.ToStr(dr["PRODNO"]);
                        }

                        #region  picture column
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

                        DevExpress.XtraGrid.Columns.GridColumn col1 = new DevExpress.XtraGrid.Columns.GridColumn();
                        col1.VisibleIndex = gridView1.Columns.Count;
                        col1.Caption = "Буцаах";
                        col1.FieldName = string.Format("RETURN");
                        col1.ColumnEdit = ri;
                        col1.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                        col1.OptionsColumn.ReadOnly = true;
                        col1.Width = 32;

                        gridView1.Columns.Add(col1);

                        #endregion

                        DataColumnRefresh();

                        if (_pageno < _pagecount)
                            lblPage.Text = string.Format("Хуудас: {0}/{1}+", _pageno, _pagecount);
                        else
                            lblPage.Text = string.Format("Хуудас: {0}/{1}", _pageno, _pagecount);
                    }
                }
                else
                {
                    res = new Result(99999, "Internal Error: Remote object not set.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
                MessageBox.Show(ex.ToString());
            }
            return res;
        }

        public void RentRemove(string serialno)
        {
            try
            {
                var query = Data.AsEnumerable().Where(x => Static.ToStr(x["SERIALNO"]) != serialno).Select(x => x);
                if (query != null)
                {
                    if (query.Count() != 0)
                    {
                        gridControl1.DataSource = query.CopyToDataTable();
                    }
                    else
                    {
                        gridControl1.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       }
       
        public void Clear()
        {
            if (Data != null)
            {
                gridControl1.DataSource = null;
            }
        }
        public Result Find(string serialno, string salesno)
        {
            Result res = null;

            _serialno = serialno;
            _salesno = salesno;
            _custno = null;
            _batchno = null;
            res = DataRefresh(1);

            return res;
        }
        //public Result Find(string custno, string batchno)
        //{
        //    Result res = null;

        //    _salesno = null;
        //    _custno = custno;
        //    _batchno = batchno;
        //    res = DataRefresh(1);

        //    return res;
        //}
        public Result Deliver()
        {
            Result res = null;
            try
            {
                DataRow currentrow = gridView1.GetFocusedDataRow();
                if (currentrow != null)
                {
                    DateTime started = Static.ToDate(currentrow["rentstarttime"]);
                    DateTime stopped = Static.ToDate(currentrow["rentendtime"]);

                    if (started > DateTime.MinValue)
                    {
                        /* 
                         * InfoPos.Core.dll ээс алдааны тайлбараа авдаг болгох!
                         * 
                         */
                        res = new Result(50000301, "Түрээсийн хэрэгслийг олгосон байна.");
                    }
                    else
                    {
                        string salesno = Static.ToStr(currentrow["salesno"]);
                        int itemno = Static.ToInt(currentrow["itemno"]);
                        string invcode = Static.ToStr(currentrow["prodno"]);
                        string invname = Static.ToStr(currentrow["name"]);

                        frmRentDeliver frm = new frmRentDeliver(_core, _touchkeyboard, salesno, itemno, invcode, invname, _userno, _userstate);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        DialogResult dlg = frm.ShowDialog();
                        if (dlg == DialogResult.OK)
                        {
                            res = DataRefresh(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res = new Result(99999, ex.ToString());
            }
            return res;
        }
        public Result Receive()
        {
            Result res = null;
            try
            {
                DataRow currentrow = gridView1.GetFocusedDataRow();
                if (currentrow != null)
                {
                    DateTime started = Static.ToDate(currentrow["rentstarttime"]);
                    DateTime stopped = Static.ToDate(currentrow["rentendtime"]);

                    if (started <= DateTime.MinValue)
                    {
                        /* 
                         * InfoPos.Core.dll ээс алдааны тайлбараа авдаг болгох!
                         * 
                         */
                        res = new Result(50000302, "Түрээсийн хэрэгслийг олгоогүй байна.");
                    }
                    else
                        if (stopped > DateTime.MinValue)
                        {
                            /* 
                             * InfoPos.Core.dll ээс алдааны тайлбараа авдаг болгох!
                             * 
                             */
                            res = new Result(50000303, "Түрээсийн хэрэгслийг хүлээн авсан байна.");
                        }
                        else
                        {
                            string salesno = Static.ToStr(currentrow["salesno"]);
                            int itemno = Static.ToInt(currentrow["itemno"]);
                            string invcode = Static.ToStr(currentrow["prodno"]);
                            string invname = Static.ToStr(currentrow["name"]);
                            string barcode = Static.ToStr(currentrow["barcode"]);

                            frmRentReceive frm = new frmRentReceive(_core, _touchkeyboard, salesno, itemno, invcode, invname, barcode, _userno, _userstate);
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            DialogResult dlg = frm.ShowDialog();
                            if (dlg == DialogResult.OK)
                            {
                                res = DataRefresh(1);
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                res = new Result(99999, ex.ToString());
            }
            return res;
        }
        public Result Return()
        {
            Result res = new Result();
            try
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {
                    string salesno = Static.ToStr(dr["SALESNO"]);
                    string prodno = Static.ToStr(dr["PRODNO"]);
                    int itemno = Static.ToInt(dr["ITEMNO"]);
                    string barcode = Static.ToStr(dr["BARCODE"]);
                    int status = Static.ToInt(dr["RENTSTATUS"]);

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 500, 500104, 500104, new object[] { salesno, prodno, itemno, barcode, status });
                    if (ISM.Template.FormUtility.ValidateQuery(res))
                        DataRefresh(1);
                }
                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return res;
        }
        public void SaveLayout()
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
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

        public delegate void delegateEventControlChoose();
        public event delegateEventControlChoose EventControlChoose;
        public void OnEventControlChoose()
        {
            try
            {
                if (EventControlChoose != null)
                {
                    EventControlChoose();
                }
            }
            catch
            { }
        }
        #endregion

        private void gridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            OnEventControlChoose();
        }
    }
}
