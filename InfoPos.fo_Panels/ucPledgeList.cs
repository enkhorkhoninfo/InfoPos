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
    public partial class ucPledgeList : UserControl
    {
        #region Internal variables

        Image imgExport = null;
        Image imgImport = null;
        Image imgDelete = null;

        #endregion
        #region Properties
        private DataTable _UserList;
        public DataTable UserList
        {
            get { return (DataTable)gridControl2.DataSource; }
        }
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

        private string _pledgeno = null;
        public string PledgeNo
        {
            get { return _pledgeno; }
            set { _pledgeno = value; }
        }

        private string _custno = null;
        public string CustNo
        {
            get { return _custno; }
            set { _custno = value; }
        }

        #endregion
        #region Constructors

        public ucPledgeList()
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
                gridView2.Appearance.Row.Font = new Font("Tahoma", 10.0F);
                // зурган багана нэмж оруулахад энэ үзэгдлийг зарлаж дотор нь зургаа set хийнэ.
                gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
                gridView2.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView2_CustomUnboundColumnData);

                gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView1_RowCellClick);
                gridView2.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView2_RowCellClick);

                gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
                gridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView2_FocusedRowChanged);
                _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);

                memoEdit1.Validating += new CancelEventHandler(memoEdit1_Validating);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "DELETE")
                {
                    Result res = new Result();
                    DataRow row = gridView2.GetDataRow(e.RowHandle);
                    DataTable dt = (DataTable)gridControl2.DataSource;
                    if (dt.Rows.Count == 1)
                    {
                        MessageBox.Show("Энэ харилцагчийг хасах боломжгүй."); 
                        return;
                    }
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 500, 500014, 500014, new object[] { row["CUSTNO"], _pledgeno });
                    if (ISM.Template.FormUtility.ValidateQuery(res))
                    {
                        DataRefresh(1);
                        OnEventRowChanged();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    DataRow row = gridView1.GetFocusedDataRow();
                    int status = Static.ToInt(row["status"]);

                    if (status == 0)
                    {
                        Result res = Deliver();
                        ISM.Template.FormUtility.ValidateQuery(res);
                    }
                }
            }
            catch
            { }
        }
        void gridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                try
                {
                    switch (e.Column.FieldName)
                    {
                        case "DELETE":
                            e.Value = imgDelete;
                            break;
                    }
                }
                catch
                { }
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            OnEventChoose();
        }
        void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            OnEventCustChoose();
        }
        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                try
                {
                    DataRow row = null;
                    switch (e.Column.FieldName)
                    {
                        case "PIC":
                            row = gridView1.GetDataRow(e.RowHandle);
                            int status = Static.ToInt(row["status"]);
                            e.Value = status == 0 ? imgExport : imgImport;
                            break;
                    }
                }
                catch
                { }
            }
        }
        private void ucPledgeList_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                imgExport = _resource.GetImage("button_next");
                imgImport = _resource.GetImage("button_ok");
                imgDelete = _resource.GetImage("object_delete");
            }
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(memoEdit1);
            }
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
        private void memoEdit1_Validating(object sender, CancelEventArgs e)
        {
            if (memoEdit1.OldEditValue == null) return; // no changes is made.

            if (Static.ToStr(memoEdit1.EditValue) != Static.ToStr(memoEdit1.OldEditValue))
            {
                if (_remote != null)
                {
                    object[] param = new object[] { _custno, _pledgeno, memoEdit1.EditValue };
                    Result res = _remote.Connection.Call(_remote.User.UserNo, 500, 500013, 500010, param);
                }
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "rowid",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "pledgeno",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "doctype",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Төрөл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Дугаар");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Барьцаалсан ажилтан",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Барьцаалсан огноо",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Чөлөөлсөн ажилтан",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Чөлөөлсөн огноо",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Төлөв",true);
        }
        bool firstrefresh = false;
        public Result DataRefresh(int pageno)
        {
            #region Validation
            if (pageno < 1) pageno = 1;
            object[] param = new object[]{
                _custno, _pledgeno
            };
            #endregion

            #region Call server
            Result res = null;
            if (_remote != null)
            {
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;

                gridControl2.RepositoryItems.Clear();
                gridControl1.RepositoryItems.Clear();

                res = _remote.Connection.Call(_remote.User.UserNo, 500, 500010, 500010, pageno - 1, _pagerows, param);
                if (res.ResultNo == 0)
                {
                    PageSet(pageno, res);
                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[1], _layoutfilename);
                    gridControl2.DataSource = res.Data.Tables[2];

                    gridView2.Columns[0].Caption = "Харилцагчийн №";
                    gridView2.Columns[1].Caption = "Овог, Нэр";
                    gridView2.Columns[2].Caption = "Барьцааны №";
                    gridView2.Columns[3].Caption = "Тэмдэглэл";
                    gridView2.Columns[4].Caption = "Огноо";
                    gridView2.Columns[5].Caption = "Таг №";

                    gridView2.Columns[0].Visible = false;
                    gridView2.Columns[2].Visible = false;
                    gridView2.Columns[3].Visible = false;
                    gridView2.Columns[4].Visible = false;
                    gridView2.Columns[4].Visible = false;
                    gridView2.BestFitColumns();
                    if (firstrefresh == false)
                    {
                        #region Add picture column

                        RepositoryItemPictureEdit ri = new RepositoryItemPictureEdit();
                        gridControl1.RepositoryItems.Add(ri);
                        gridControl2.RepositoryItems.Add(ri);

                        DevExpress.XtraGrid.Columns.GridColumn delcol = new DevExpress.XtraGrid.Columns.GridColumn();
                        delcol.VisibleIndex = gridView2.Columns.Count;
                        delcol.Caption = "...";
                        delcol.FieldName = string.Format("DELETE");
                        delcol.ColumnEdit = ri;
                        delcol.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                        delcol.OptionsColumn.ReadOnly = true;
                        delcol.Width = 32;
                        gridView2.Columns.Add(delcol);

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
                        firstrefresh = true;
                    }

                    DataColumnRefresh();

                    if (_pageno < _pagecount)
                        lblPage.Text = string.Format("Хуудас: {0}/{1}+", _pageno, _pagecount);
                    else
                        lblPage.Text = string.Format("Хуудас: {0}/{1}", _pageno, _pagecount);


                    DataTable dt0 = res.Data.Tables[0];
                    if (dt0.Rows.Count > 0)
                    {
                        memoEdit1.EditValue = Static.ToStr(dt0.Rows[0]["note"]);
                    }
                }
            }
            else
            {
                res = new Result(1000, "Internal Error: Remote object not set.");
            }
            #endregion

            return res;
        }

        public Result Find(string custno, string pledgeno)
        {
            Result res = null;

            _custno = custno;
            _pledgeno = pledgeno;
            res = DataRefresh(1);

            return res;
        }

        public void SaveLayout()
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
            ISM.Template.FormUtility.GridLayoutSave(gridView2, _layoutfilename + "2");
        }

        public Result Receive()
        {
            Result res = new Result();

            frmPledgeReceive frm = new frmPledgeReceive(_core, TouchKeyboard, _custno, _pledgeno);
            DialogResult dlg = frm.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(_pledgeno))
                {
                    _pledgeno = frm._pledgeno; // шинэ дугаарлалт буцаж ирнэ.
                }
                res = DataRefresh(1);
            }
            else
            {
                res.ResultNo = 9;
            }
            if (res.ResultNo == 0) res.ResultDesc = _pledgeno;

            return res;
        }
        public Result Deliver()
        {
            Result res = null;
            try
            {
                DataRow currentrow = gridView1.GetFocusedDataRow();
                if (currentrow == null)
                {
                    res = new Result(9, "Барьцаа сонгогдоогүй байна!");
                    ISM.Template.FormUtility.ValidateQuery(res);
                    return res;
                }

                string rowid = Static.ToStr(currentrow["rowid"]);
                int userno = _core.RemoteObject.User.UserNo;
                string docno = Static.ToStr(currentrow["docno"]);
                int status = Static.ToInt(currentrow["status"]);

                #region Validation

                if (status > 0)
                {
                    res = new Result(9, "Чөлөөлөгдсөн бичиг баримт.");
                    ISM.Template.FormUtility.ValidateQuery(res);
                    return res;
                }
                if (!ISM.Template.FormUtility.ValidateConfirm(string.Format("Та [{0}] дугаартай бичиг баримтыг олгохдоо итгэлтэй байна уу?", docno)))
                    return new Result (9, "Үйлдлээ цуцлав.");

                #endregion
                #region Prepare parameters

                object[] param = new object[]{
                    _custno, _pledgeno, rowid,userno 
                };

                #endregion
                #region Call server
                if (_remote != null)
                {
                    res = _remote.Connection.Call(_remote.User.UserNo, 500, 500012, 500012, param);
                    if (res != null && res.ResultNo == 0)
                    {
                        res = DataRefresh(1);
                    }
                }
                else
                {
                    res = new Result(1000, "Internal Error: Remote object not set.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(99999, ex.ToString());
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
                    DataRow currentrow = gridView1.GetFocusedDataRow();
                    EventChoose(currentrow);
                }
            }
            catch
            { }
        }

        public delegate void delegateEventCustChoose(DataRow currentrow);
        public event delegateEventCustChoose EventCustChoose;
        public void OnEventCustChoose()
        {
            try
            {
                if (EventCustChoose != null)
                {
                    DataRow currentrow = gridView2.GetFocusedDataRow();
                    EventCustChoose(currentrow);
                }
            }
            catch
            { }
        }

        public delegate void delegateEventRowChanged();
        public event delegateEventRowChanged EventRowChanged;
        public void OnEventRowChanged()
        {
            try
            {
                if (EventRowChanged != null)
                {
                    EventRowChanged();
                }
            }
            catch
            { }
        }
        #endregion
    }
}
