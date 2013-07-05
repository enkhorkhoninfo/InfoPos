using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmSearchPledge : DevExpress.XtraEditors.XtraForm
    {
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
                }
            }
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

        #endregion
        #region User Functions

        public void Init()
        {
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

            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);

        }
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Барьцаа №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Харилцагч №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Нэр, овог");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Утас");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Төрөл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "ББДугаар");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Ажилтан");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Огноо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Төлөв");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Таг");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Багц");

            ISM.Template.FormUtility.Column_SetList(ref gridView1, 8, new string[] { "БАРЬЦААЛСАН", "ЧӨЛӨӨЛСӨН" });

            gridView1.BestFitColumns();
        }
        public Result DataRefresh(int pageno)
        {
            #region Validation

            if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters

            object[] param = new object[]{
                 txtDocNo.EditValue
                ,txtPhone.EditValue
                ,txtCustName.EditValue
                ,txtPledgeNo.EditValue
                ,txtCustNo.EditValue
                ,txtSerialNo.EditValue
                ,radioGroup1.SelectedIndex
            };

            #endregion
            #region Call server
            Result res = null;
            if (_core != null && _core.RemoteObject != null)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601003, 601003, pageno - 1, _pagerows, param);
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
        public Result Find(string custno, string pledgeno, string docno, string custname, string phone, string serialno)
        {
            Result res = null;

            txtCustNo.EditValue = custno;
            txtPledgeNo.EditValue = pledgeno;

            txtDocNo.EditValue = docno;
            txtCustName.EditValue = custname;
            txtPhone.EditValue = phone;
            txtSerialNo.EditValue = serialno;

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
        public void OnEventChoose(DataRow currentrow)
        {
            try
            {
                if (EventChoose != null) EventChoose(currentrow);
            }
            catch
            { }
        }

        #endregion
        #region Control Events
        public frmSearchPledge(InfoPos.Core.Core core)
        {
            InitializeComponent();
            Init();

            _core = core;
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(txtCustNo);
                _touchkeyboard.AddToKeyboard(txtPledgeNo);
                _touchkeyboard.AddToKeyboard(txtPhone);
                _touchkeyboard.AddToKeyboard(txtCustName);
                _touchkeyboard.AddToKeyboard(txtDocNo);
                _touchkeyboard.AddToKeyboard(txtSerialNo);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _pagecount = 0;
            Result res = DataRefresh(1);
            if (res.ResultNo != 0)
                MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            DataRow currentrow = gridView1.GetFocusedDataRow();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            //OnEventChoose(currentrow);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
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
    }
}
