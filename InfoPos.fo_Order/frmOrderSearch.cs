using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace InfoPos.Order
{
    public partial class frmOrderSearch : DevExpress.XtraEditors.XtraForm
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
            get { return _resource; }
            set { _resource = value; }
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

        private string _orderno;
        public string OrderNo
        {
            get { return _orderno; }
        }

        private decimal _custno;
        public decimal CustNo
        {
            get { return _custno; }
        }
        
        private string _custname;
        public string CustName
        {
            get { return _custname; }
        }

        private string _custreg;
        public string CustReg
        {
            get { return _custreg; }
        }

        private bool _selected = false;
        public bool Selected
        {
            get { return _selected; }
        }


        #endregion
        #region Constructors

        public frmOrderSearch(InfoPos.Core.Core core)
        {
            InitializeComponent();

            this.Core = core;
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
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
        }

        #endregion
        #region Control Events

        private void frmOrderSearch_Load(object sender, EventArgs e)
        {
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(txtCustomerNo);
                _touchkeyboard.AddToKeyboard(txtOrderNo);
                _touchkeyboard.AddToKeyboard(txtRegNo);
                _touchkeyboard.AddToKeyboard(txtName1);
                _touchkeyboard.AddToKeyboard(txtName2);
                _touchkeyboard.AddToKeyboard(txtCorp);
            }
            if (_core != null && _core.Resource != null)
            {
                btnSearch.Image = _core.Resource.GetImage("button_find");
                btnNext.Image = _core.Resource.GetImage("paging_next");
                btnPrev.Image = _core.Resource.GetImage("paging_prev");
                btnChoose.Image = _core.Resource.GetImage("button_ok");
            }
            //DataRefresh(1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _pagecount = 0;
            Result res = DataRefresh(1);
            if (res != null && res.ResultNo != 0)
                MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            DataRow r = gridView1.GetFocusedDataRow();
            if (r != null)
            {
                _selected = true;
                _orderno = Static.ToStr(r["orderno"]);
                _custno = Static.ToDecimal(r["custno"]);
                _custname = Static.ToStr(r["custname"]);
                _custreg = Static.ToStr(r["registerno"]);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Захиалгын №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Харилцагчийн №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Харилцагчийн нэр, овог");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Регистр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Баталгаажуулах хугацаа");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Үнийн дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Урьдчилж төлсөн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Валют");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Эхлэх өдөр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Дуусах өдөр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Хамрагдах хүний тоо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "Төлөв");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 12, "Төлвийн нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 13, "Үүсгэсэн огноо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 14, "Үүсгэсэн хэрэглэгч");
        }
        public Result DataRefresh(int pageno)
        {
            #region Validation

            if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters

            object[] param = new object[6];
            param[0] = txtOrderNo.EditValue;
            param[1] = txtCustomerNo.EditValue;
            param[2] = txtName1.EditValue;
            param[3] = txtName2.EditValue;
            param[4] = txtCorp.EditValue;
            param[5] = txtRegNo.EditValue;

            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 603, 603001, 603001, pageno - 1, _pagerows, param);
                if (res != null && res.ResultNo == 0)
                {
                    PageSet(pageno, res);
                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layoutfilename);
                    //gridControl1.DataSource = res.Data.Tables[0];
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

        public Result Find(string orderno,string custno, string contractno, string reg, string fname, string lname, string cname)
        {
            Result res = null;
            txtOrderNo.EditValue = orderno;
            txtCustomerNo.EditValue = custno;
            txtRegNo.EditValue = reg;
            txtName1.EditValue = fname;
            txtName2.EditValue = lname;
            txtCorp.EditValue = cname;

            res = DataRefresh(1);

            return res;
        }

        #endregion
    }
}
