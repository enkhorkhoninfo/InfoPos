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
    public partial class ucRentTag : UserControl
    {
        #region Internal variables

        Image imgExport = null;
        Image imgImport = null;

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

        private string _serialno = null;
        public string SerialNo
        {
            get { return _serialno; }
            set { _serialno = value; }
        }

        private string _custno = null;
        public string CustNo
        {
            get { return _custno; }
            set { _custno = value; }
        }

        #endregion
        #region Constructors

        public ucRentTag()
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
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
        }

        #endregion
        #region Control Events

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
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
                            DataRow row = gridView1.GetDataRow(e.RowHandle);
                            DateTime started = Static.ToDate(row["servstarttime"]);
                            e.Value = (started > DateTime.MinValue ? imgImport : imgExport);
                            break;
                    }
                }
                catch
                { }
            }
        }
        private void ucRentTag_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                imgExport = _resource.GetImage("image_forwardmail");
                imgImport = _resource.GetImage("image_replymail");
            }
            if (_touchkeyboard != null)
            {
                // there is no touch controls...
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "ББагц",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Борл.");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Сериал");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Овог");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Регистр");
        }
        public Result DataRefresh(int pageno)
        {
            #region Validation

            if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters

            object[] param = new object[]{
                _salesno, _serialno, _custno
            };

            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 500, 500005, 500005, pageno - 1, _pagerows, param);
                if (res.ResultNo == 0)
                {
                    PageSet(pageno, res);
                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layoutfilename);
                    DataColumnRefresh();

                    if (_pageno < _pagecount)
                        lblPage.Text = string.Format("Хуудас: {0}/{1}+", _pageno, _pagecount);
                    else
                        lblPage.Text = string.Format("Хуудас: {0}/{1}", _pageno, _pagecount);
                }
            }
            else
            {
                res = new Result(1000, "Internal Error: Remote object not set.");
            }
            #endregion

            return res;
        }

        public Result Find(string salesno, string serialno, string custno)
        {
            Result res = null;

            _salesno = salesno;
            _serialno = serialno;
            _custno = custno;
            res = DataRefresh(1);

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

        #endregion

    }
}
