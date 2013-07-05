using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using EServ.Shared;

namespace InfoPos.Panels
{
    public partial class ucMaster : UserControl
    {
        #region[Properties]
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

        private string _layoutfilename = "";
        public string LayoutFileName
        {
            get { return _layoutfilename; }
        }
        #endregion
        public ucMaster()
        {
            try
            {
                InitializeComponent();
                this.ResizeRedraw = true;
                gridView1.RowHeight = 30;
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
        #region[Function]
        public void DataColumnRefresh()
        {
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Барааны №");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Төрлийн код", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Төрөл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Барааны тайлбар");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Бар код");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Төлөв");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Сүүлийн олгосон хэрэглэгч", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Сүүлийн олгосон огноо", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Тэмдэглэл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Сонголт");
            gridView1.Columns[10].ColumnEdit = CreateRepositoryCheckEdit();
            gridView1.Columns[10].VisibleIndex = 0;
            for (int i = 0; i <= 9; i++)
            {
                if (i != 4 && i != 9)
                {
                    gridView1.Columns[i].BestFit();
                }
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
        }
        public Result DataRefresh()
        {
            #region Prepare parameters

            object[] param = new object[]
            {
                txtInvID.EditValue
                ,txtName.EditValue
                ,txtBarCode.EditValue
                ,rdgStatus.EditValue
            };

            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 501, 600041, 600041, param);
                if (res.ResultNo == 0)
                {
                    DataTable dt = res.Data.Tables[0];
                    dt.Columns.Add("SELECT", typeof(int));
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["SELECT"] = 0;
                    }
                    gridControl1.DataSource = dt;
                    DataColumnRefresh();
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
            else
            {
                res = new Result(1000, "Internal Error: Remote object not set.");
            }
            #endregion

            return res;
        }
        public Result Find(string invid, string name, string barcode, string status)
        {
            Result res = null;

            txtInvID.EditValue = invid;
            txtName.EditValue = name;
            txtBarCode.EditValue = barcode;
            rdgStatus.EditValue = status;

            res = DataRefresh();

            return res;
        }
        public void SaveLayout()
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }
        void ri_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            string val = "";
            if (e.Value != null)
            {
                val = e.Value.ToString();
            }
            else
            {
                val = "False";
            }
            switch (val)
            {
                case "True":
                    e.CheckState = CheckState.Checked;
                    break;
                case "False":
                    e.CheckState = CheckState.Unchecked;
                    break;
                case "Yes":
                    goto case "True";
                case "No":
                    goto case "False";
                case "1":
                    goto case "True";
                case "0":
                    goto case "False";
                default:
                    e.CheckState = CheckState.Checked;
                    break;
            }
            e.Handled = true;
        }
        public RepositoryItemCheckEdit CreateRepositoryCheckEdit()
        {
            RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(ri)).BeginInit();
            ri.AutoHeight = false;
            ri.AllowFocused = false;
            ri.ValueChecked = 1;
            ri.ValueUnchecked = 0;
            ((System.ComponentModel.ISupportInitialize)(ri)).EndInit();
            ri.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(ri_QueryCheckStateByValue);
            return ri;
        }
        #endregion
        #region[BTN]
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            DataRefresh();
        }
        public void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                Result res = new Result();
                res = _remote.Connection.Call(_remote.User.UserNo, 501, 600042, 600042, new object[] { (DataTable)gridControl1.DataSource });
                if (res.ResultNo == 0)
                {
                    DataRefresh();
                }
                else
                {
                    MessageBox.Show(string.Format("{0} : {1}", res.ResultNo, res.ResultDesc));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void btnInActive_Click(object sender, EventArgs e)
        {
            try
            {
                Result res = new Result();
                res = _remote.Connection.Call(_remote.User.UserNo, 501, 600043, 600043, new object[] { (DataTable)gridControl1.DataSource });
                if (res.ResultNo == 0)
                {
                    DataRefresh();
                }
                else
                {
                    MessageBox.Show(string.Format("{0} : {1}", res.ResultNo, res.ResultDesc));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void ucMaster_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                btnActive.Image = _resource.GetImage("button_ok");
                btnInActive.Image = _resource.GetImage("image_exit");
                btnSearch.Image = _resource.GetImage("button_find");
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt=(DataTable)gridControl1.DataSource;
            if (checkEdit1.Checked == true)
            {
                checkEdit1.Text = "Бүгдийг арилгах";
                foreach (DataRow dr in dt.Rows)
                {
                    dr["SELECT"] = 1;
                }
            }
            else
            {
                checkEdit1.Text = "Бүгдийг сонгох";
                foreach (DataRow dr in dt.Rows)
                {
                    dr["SELECT"] = 0;
                }
            }
        }

        private void rdgStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkEdit1.Text = "Бүгдийг сонгох";
            checkEdit1.Checked = false;
        }
        #region User Events

        #endregion
    }
}