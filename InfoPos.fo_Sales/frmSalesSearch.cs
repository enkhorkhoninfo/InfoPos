using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmSalesSearch : DevExpress.XtraEditors.XtraForm
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

        private string _salesno;
        public string SalesNo
        {
            get { return _salesno; }
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

        public frmSalesSearch(InfoPos.Core.Core core)
        {
            InitializeComponent();
            this.Core = core;
            this.ResizeRedraw = true;

            this.FormClosing += frmSalesSearch_FormClosing;

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

        void frmSalesSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }

        #endregion
        #region Control Events

        private void frmSalesSearch_Load(object sender, EventArgs e)
        {
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(txtCustomerNo);
                _touchkeyboard.AddToKeyboard(txtSalesNo);
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
            dteSales.EditValue = _core.TxnDate;
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
                _salesno = Static.ToStr(r["salesno"]);
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Борлуулалт №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Харилцагчийн №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Регистр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Харилцагчийн нэр, овог");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Борлуулалт дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Хөнгөлөлт");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "НӨАТ");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Төлөх дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Төлөгдсөн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "ПОС");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Огноо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "Гэрээ");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 12, "Захиалга");

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 13, "TRANDATE",true);
            //ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 16, "DISCOUNTPROD", true);
            //ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 17, "DISCOUNTSALES",true);
            //ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 18, "DSVAT",true);
            //ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 19, "ISVAT", true);

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 14, "Ээлж");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 15, "Кассчин",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 16, "Кассчин");

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 17, "Таг сериал");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 18, "Таг сери");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 19, "Барьцаа");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 20, "Төлөв");

            gridView1.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[4].DisplayFormat.FormatString = "#,##0.00";

            gridView1.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[5].DisplayFormat.FormatString = "#,##0.00";

            gridView1.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[6].DisplayFormat.FormatString = "#,##0.00";

            gridView1.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[7].DisplayFormat.FormatString = "#,##0.00";

            gridView1.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[8].DisplayFormat.FormatString = "#,##0.00";

            gridView1.RowHeight = 28;
        }
        public Result DataRefresh(int pageno)
        {
            #region Validation

            if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters

            object[] param = new object[12];
            param[0] = txtSalesNo.EditValue;
            param[1] = txtCustomerNo.EditValue;
            param[2] = txtRegNo.EditValue;
            param[3] = txtName1.EditValue;
            param[4] = txtName2.EditValue;
            param[5] = txtCorp.EditValue;
            param[6] = dteSales.EditValue;
            param[7] = chkOriginal.Checked;
            param[8] = chkRefund.Checked;

            param[9] = txtSerialNo.EditValue;
            param[10] = txtTagFrameNo.EditValue;

            param[11] = chkCorrection.Checked;

            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 605, 605001, 605001, pageno - 1, _pagerows, param);
                if (res != null && res.ResultNo == 0)
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

        public Result Find(string salesno,string custno, string reg, string fname, string lname, string cname)
        {
            Result res = null;
            txtSalesNo.EditValue = salesno;
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
