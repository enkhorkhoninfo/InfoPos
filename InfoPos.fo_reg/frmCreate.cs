using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors.Repository;
using EServ.Shared;

namespace InfoPos.Reg
{
    public partial class frmCreate : Form
    {
        #region Properpies

        private bool _isnew = true;

        private string _pledgeno = null;
        public string PledgeNo
        {
            get { return _pledgeno; }
            set { _pledgeno = value; }
        }

        private int _typeid = 0;
        public int TypeId
        {
            get { return _typeid; }
            set { _typeid = value; }
        }

        private int _typeidcurrent = 0;
        public int TypeIdCurrent
        {
            get { return _typeidcurrent; }
            set { _typeidcurrent = value; }
        }

        private bool _selected = false;
        public bool Selected
        {
            get { return _selected; }
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
                    //if (_remote == null) _remote = _core.RemoteObject;
                    //if (_resource == null) _resource = _core.Resource;
                }
            }
        }
        #endregion
        #region Business functions
        public void InitTypeList()
        {
            try
            {
                DataTable dt = new DataTable();
                Result res = ISM.Template.DictUtility.Get(_core.RemoteObject, "PLEDGETYPE", 601003, ref dt);
                if (res != null && res.ResultNo == 0)
                {
                    DataTable d = new DataTable();
                    d.Columns.Add("checked", typeof(bool));
                    d.Columns.Add("typename", typeof(string));
                    d.Columns.Add("typeid", typeof(string));

                    int index =0;
                    int i=0;
                    foreach (DataRow r in dt.Rows)
                    {
                        bool check = false;
                        int typeid = Static.ToInt(r["typeid"]);
                        if (r["typeid"] != null && r["typeid"] != DBNull.Value)
                        {
                            //int typeid = Static.ToInt(r["typeid"]);
                            check = !string.IsNullOrEmpty(_pledgeno) && _typeid == typeid;
                        }
                        if (check) index = i;

                        d.Rows.Add( check, r["typename"], typeid);

                        i++;
                    }

                    gridControl1.DataSource = d;
                    DataColumnRefresh();
                    gridView1.FocusedRowHandle = gridView1.GetRowHandle(index);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void DataColumnRefresh()
        {
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "", false);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "", false);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "", true);

            gridView1.Columns[0].Width = 32;
            gridView1.Columns[1].Width = gridControl1.Width - 32 - 8;

            gridView1.RowHeight = 28;
        }
        public void Save()
        {
            Result res = null;
            #region Validation

            if (!_selected)
            {
                res = new Result(9, "Баримт бичгийн төрөл сонгогдоогүй байна!");
                goto OnExit;
            }

            string docno = Static.ToStr(txtNo.EditValue);
            if (string.IsNullOrEmpty(docno))
            {
                res = new Result(9, "Баримт бичгийн дугаараа оруулаагүй байна!");
                goto OnExit;
            }

            string custno = Static.ToStr(txtCustNo.EditValue);
            string custname = Static.ToStr(txtName.EditValue);
            if (string.IsNullOrEmpty(custno))
            {
                res = new Result(9, "Барьцаалагч сонгогдоогүй байна!");
                goto OnExit;
            }
            string phone = Static.ToStr(txtPhone.EditValue);
            if (string.IsNullOrEmpty(phone))
            {
                res = new Result(9, "Холбоо барих утасны дугаарыг оруулна уу!");
                goto OnExit;
            }
            string memo = Static.ToStr(txtMemo.EditValue);

            #endregion
            #region Prepare parameters

            object[] param = new object[] { _pledgeno, _typeidcurrent, docno, custno, custname, phone, memo };

            #endregion
            #region Сервер рүү хүсэлт илгээх
            if (string.IsNullOrEmpty(_pledgeno))
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                    , 601, 601001, 601001, param);
                if (res.ResultNo == 0) _pledgeno = res.ResultDesc;
            }
            else
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                    , 601, 601002, 601002, param);
            }
            #endregion

        OnExit:
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        public Result Read(string pledgeno)
        {
            Result res = null;
            #region Validation

            if (string.IsNullOrEmpty(pledgeno))
            {
                res = new Result(9, "Барьцаа сонгогдоогүй байна!");
                goto OnExit;
            }
            #endregion
            #region Prepare parameters

            _pledgeno = pledgeno;

            object[] param = new object[] { null, null, null, pledgeno, 0, null, 0, null /*tagframeno*/ };

            #endregion
            #region Сервер рүү хүсэлт илгээх
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601003, 601003, param);
            if (res.ResultNo != 0) goto OnExit;
            #endregion

            if (res.AffectedRows > 0)
            {
                DataTable dt = res.Data.Tables[0];
                DataRow r = dt.Rows[0];

                txtCustNo.EditValue = r["CUSTNO"];
                txtName.EditValue = r["CUSTNAME"];
                txtPhone.EditValue = r["CONTACT"];
                txtMemo.EditValue = r["MEMO"];
                txtNo.EditValue = r["DOCNO"];

                _typeid = Static.ToInt(r["DOCTYPE"]);
                _typeidcurrent = _typeid;                

                //DataTable docs = (DataTable)gridControl1.DataSource;
                //DataRow[] rows = docs.Select(string.Format("TYPEID={0}", doctype));
                //if (rows != null && rows.Length > 0)
                //{
                //    rows[0]["checked"] = true;
                //    rows[0].AcceptChanges();

                //    _selected = true;
                //    _typeidcurrent = doctype;
                //}
            }

        OnExit:
            if (!ISM.Template.FormUtility.ValidateQuery(res))
            {
                //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                //this.Close();
            }
            return res;
        }
        #endregion
        #region Constructors
        public frmCreate(InfoPos.Core.Core core, string pledgeno)
        {
            InitializeComponent();

            _core = core;
            _pledgeno = pledgeno;


            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridView1.OptionsView.ShowColumnHeaders = false;
            gridView1.OptionsView.ShowVertLines = false;

            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            //gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);

            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            // зурган багана нэмж оруулахад энэ үзэгдлийг зарлаж дотор нь зургаа set хийнэ.
            //gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
            //gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView1_RowCellClick);
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);

        }
        #endregion
        #region Control Events

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(e.PrevFocusedRowHandle);
            if (row != null) row["checked"] = false;

            row = gridView1.GetDataRow(e.FocusedRowHandle);
            if (row != null)
            {
                row["checked"] = true;
                _selected = true;
                _typeidcurrent = Static.ToInt(row["typeid"]);
            }
        }
        private void frmCreate_Load(object sender, EventArgs e)
        {
            InitTypeList();
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            Save();
            //else SaveEdit();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fo_Customer.frmCustSearch frm = new fo_Customer.frmCustSearch(_core);
            DialogResult res = frm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                DataRow r = frm.CurrentRow;
                if (r != null)
                {
                    txtCustNo.EditValue = Static.ToStr(r["customerno"]);
                    txtName.EditValue = Static.ToStr(r["custname"]);
                    txtPhone.EditValue = Static.ToStr(r["mobile"]);
                    txtNo.EditValue = Static.ToStr(r["registerno"]);
                }
            }
        }

        #endregion
    }
}
