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
    public partial class ucSaleProdList : UserControl
    {
        #region Internal variables

        Image imgExport = null;
        Image imgImport = null;
        Image imgDone = null;

        byte _lastmode = 0;

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

        public ucSaleProdList()
        {
            InitializeComponent();
            this.ResizeRedraw = true;

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);

            // зурган багана нэмж оруулахад энэ үзэгдлийг зарлаж дотор нь зургаа set хийнэ.
            gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
            gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView1_RowCellClick);
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
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
                    int rentalstatus = Static.ToInt(row["rentalstatus"]);

                    Result res = null;

                    //switch (rentalstatus)
                    //{
                    //    case 1: res = Deliver(); break;
                    //    case 2: res = Receive(); break;
                    //}
                    //ISM.Template.FormUtility.ValidateQuery(res);
                }
            }
            catch
            { }
        }
        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                switch (e.Column.FieldName)
                {
                    case "PIC":
                        DataRow row = gridView1.GetDataRow(e.RowHandle);
                        int rentalstatus = Static.ToInt(row["rentalstatus"]);

                        switch (rentalstatus)
                        {
                            case 1: e.Value = imgExport; break;
                            case 2: e.Value = imgImport; break;
                            case 3: e.Value = imgDone; break;
                        }
                        break;
                }
            }
        }
        private void ucSaleProdList_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                imgExport = _resource.GetImage("image_forwardmail");
                imgImport = _resource.GetImage("image_replymail");
                imgDone = _resource.GetImage("image_ok");
            }
            if (_touchkeyboard != null)
            {
                // there is no touch controls...
            }
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Хэрэгсэл №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Хэрэгслийн нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Баркод");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Торгууль");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Олгосон");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Авсан");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Төлөв", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Үйлдэл");

        }
        public Result DataRefresh(int pageno)
        {
            #region Validation

            if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters


            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                object[] param = new object[] { _salesno };
                res = _remote.Connection.Call(_remote.User.UserNo, 500, 500004, 500004, pageno - 1, _pagerows, param);
                if (res.ResultNo == 0)
                {
                    PageSet(pageno, res);
                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layoutfilename);

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
            res = DataRefresh(1);

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
