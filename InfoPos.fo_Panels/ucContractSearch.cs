using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EServ.Shared;

namespace InfoPos.Panels
{
    public partial class ucContractSearch : UserControl
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

        #endregion
        #region Constructors

        public ucContractSearch()
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
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsView.ShowIndicator = false;
                gridView1.OptionsView.RowAutoHeight = true;
                gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);

                _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        #region Control Events

        private void ContractSearch_Load(object sender, EventArgs e)
        {
            try
            {
                string msg = "";
                ArrayList Tables = new ArrayList();
                string[] names = { "CONTRACTTYPE" };
                ISM.Template.DictUtility.PrivNo = 130001;
                Result res = ISM.Template.DictUtility.Get(_core.RemoteObject, names, ref Tables);
                DataTable dt = (DataTable)Tables[0];
                if (dt == null)
                {
                    msg = "Dictionary-д CONTRACTTYPE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, dt, "CONTRACTTYPE", "NAME", "", null);
                    cboType.ItemIndex = 0;
                }
                if (_touchkeyboard != null)
                {
                    _touchkeyboard.AddToKeyboard(txtContractNo);
                    _touchkeyboard.AddToKeyboard(txtCustomerNo);
                    _touchkeyboard.AddToKeyboard(txtRegNo);
                    _touchkeyboard.AddToKeyboard(txtName1);
                    _touchkeyboard.AddToKeyboard(txtName2);
                    _touchkeyboard.AddToKeyboard(txtCorp);
                    _touchkeyboard.AddToKeyboard(cboType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboType.EditValue != null)
            {
                _pagecount = 0;
                Result res = DataRefresh(1);
                if (res.ResultNo != 0)
                    MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
            }
            else MessageBox.Show("Төрөл сонгоно уу.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnTag_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    frmRentTagReader frm = new frmRentTagReader(_core);
            //    DialogResult res = frm.ShowDialog();
            //    if (res == DialogResult.OK)
            //    {
            //        this.txtTagNo.EditValue = frm.ReadString;
            //        if (!string.IsNullOrEmpty(frm.ReadString))
            //            DataRefresh(1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            DataRow currentrow = gridView1.GetFocusedDataRow();
            OnEventChoose(currentrow);
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
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Гэрээний №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Төрөл", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Төрөл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Харилцагчийн №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Харилцагчийн овог");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Харилцагчийн нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Компаний нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Эхлэх огноо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Эхлэх цаг");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Дуусах огноо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Дуусах цаг");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "Үнийн дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 12, "Үлдэгдэл үнэ");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 13, "Валют");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 14, "Үйлчлүүлэгчийн тоо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 15, "Дүнг элэгдүүлэх давтамж");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 16, "Дүнг элэгдүүлэх дүн");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 17, "Төлөв", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 18, "Төлөв");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 19, "Үүсгэсэн огноо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 20, "Үүсгэсэн огноо цаг");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 21, "Үүсгэсэн хэрэглэгч");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 22, "Хариуцсан хэрэглэгч");
            gridView1.RowHeight = 30;
            gridView1.BestFitColumns();
        }
        public Result DataRefresh(int pageno)
        {
            #region Validation

            if (pageno < 1) pageno = 1;

            #endregion
            #region Prepare parameters

            object[] param = new object[7];
            param[0] = cboType.EditValue;
            param[1] = txtContractNo.EditValue;
            param[2] = txtCustomerNo.EditValue;
            param[3] = txtName1.EditValue;
            param[4] = txtName2.EditValue;
            param[5] = txtCorp.EditValue;
            param[6] = txtRegNo.EditValue;
            
            #endregion

            #region Call server
            Result res = new Result();
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 207, 130001, 130001, pageno - 1, _pagerows, new object[] { param, 1 });
                if (res.ResultNo == 0)
                {
                    PageSet(pageno, res);
                    //ISM.Template.FormUtility.GridLayout(gridView1, res.Data.Tables[0], _layoutfilename);
                    gridControl1.DataSource = res.Data.Tables[0];
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

        public Result Find(string salesno, string tagno, string reg, string fname, string lname, string cname)
        {
            Result res = null;

            txtContractNo.EditValue = salesno;
            txtCustomerNo.EditValue = tagno;
            txtRegNo.EditValue = reg;
            txtName1.EditValue = fname;
            txtName2.EditValue = lname;
            txtCorp.EditValue = cname;

            res = DataRefresh(1);

            return res;
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
    }
}
