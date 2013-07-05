using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;

namespace InfoPos.fo_Customer
{
    public partial class frmIndividualSearch : Form
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

        private decimal _custno = 0;
        public decimal CustNo
        {
            get { return _custno; }
        }
        private string _custname = null;
        public string CustName
        {
            get { return _custname; }
        }
        private string _custreg = null;
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
        public frmIndividualSearch(InfoPos.Core.Core core)
        {
            try
            {
                InitializeComponent();
                this.Core = core;
                this.FormClosing += frmCustSearch_FormClosing;
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
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion
        #region Control Events

        private void frmCustSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLayout();
        }
        private void frmCustSearch_Load(object sender, EventArgs e)
        {
            try
            {
                if (_touchkeyboard != null)
                {
                    _touchkeyboard.AddToKeyboard(txtCustNo);
                    _touchkeyboard.AddToKeyboard(txtContract);
                    _touchkeyboard.AddToKeyboard(txtRegNo);
                    _touchkeyboard.AddToKeyboard(txtName1);
                    _touchkeyboard.AddToKeyboard(txtName2);
                }
                if (Resource != null)
                {
                    btnSearch.Image = Resource.GetImage("button_find");
                    btnNext.Image = Resource.GetImage("paging_next");
                    btnPrev.Image = Resource.GetImage("paging_prev");
                    btnChoose.Image = Resource.GetImage("button_ok");
                    btnNew.Image = Resource.GetImage("frontmenu_customer_add");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
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
            InfoPos.fo_Customer.frmCustomer frm = new fo_Customer.frmCustomer(_core,
                                                                            Static.ToStr(txtCustNo.EditValue),
                                                                            Static.ToStr(0),
                                                                            Static.ToStr(txtName1.EditValue),
                                                                            Static.ToStr(txtName2.EditValue),
                                                                            "",
                                                                            Static.ToStr(txtRegNo.EditValue)
                                                                            );
            frm.ShowDialog();
            DataRefresh(1);
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            DataRow r = gridView1.GetFocusedDataRow();
            if (r != null)
            {
                _selected = true;
                _custno = Static.ToDecimal(r["customerno"]);
                _custname = Static.ToStr(r["custname"]);
                _custreg = Static.ToStr(r["registerno"]);
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

            //DataRow currentrow = gridView1.GetFocusedDataRow();
            //OnEventChoose(currentrow);
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Харилцагч №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Нэр, овог");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Хүйс");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Регистр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Утас");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Гэрээ №");
            gridView1.RowHeight = 28;
            //gridView1.BestFitColumns();
        }
        public Result DataRefresh(int pageno)
        {
            #region Validation

            if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters

            object[] param = new object[]{
                txtCustNo.EditValue
                ,txtName1.EditValue
                ,txtName2.EditValue
                ,txtRegNo.EditValue
                ,txtContract.EditValue
            };

            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 602, 602004, 120000, pageno - 1, _pagerows, param);
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

        public Result Find(string custno, string fname, string lname, string reg, string contractno)
        {
            Result res = null;

            txtCustNo.EditValue = custno;
            txtContract.EditValue = contractno;
            txtRegNo.EditValue = reg;
            txtName1.EditValue = fname;
            txtName2.EditValue = lname;

            res = DataRefresh(1);

            return res;
        }

        public void SaveLayout()
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }
        #endregion
        #region User Events

        //public delegate void delegateEventChoose(DataRow currentrow);
        //public event delegateEventChoose EventChoose;
        //public void OnEventChoose(DataRow currentrow)
        //{
        //    try
        //    {
        //        if (EventChoose != null) EventChoose(currentrow);
        //    }
        //    catch
        //    { }
        //}

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
               
    }
}
