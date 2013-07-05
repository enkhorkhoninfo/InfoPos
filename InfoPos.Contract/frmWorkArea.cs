using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System.Linq;
using EServ.Shared;
using ISM.Template;
namespace InfoPos.Contract
{
    public partial class frmWorkArea : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        string _layout = "";
        string knowbutton = "";
        RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
        #endregion
        #region[Constructure]
        public frmWorkArea(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnGroupAdd.Image = _core.Resource.GetImage("navigate_add");
                btnGroupEdit.Image = _core.Resource.GetImage("navigate_edit");
                btnGroupDelete.Image = _core.Resource.GetImage("navigate_delete");
                btnGroupCancel.Image = _core.Resource.GetImage("image_exit");
                btnposuse.Image = _core.Resource.GetImage("paging_next");
                btnposunuse.Image = _core.Resource.GetImage("paging_prev");
                btnpayuse.Image = _core.Resource.GetImage("paging_next");
                btnpayunuse.Image = _core.Resource.GetImage("paging_prev");
                btnproduse.Image = _core.Resource.GetImage("paging_next");
                btnprodunuse.Image = _core.Resource.GetImage("paging_prev");
                btnPackUse.Image = _core.Resource.GetImage("paging_next");
                btnPackUnUse.Image = _core.Resource.GetImage("paging_prev");

            }
            _layout = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
        }
        private void frmWorkArea_Load(object sender, EventArgs e)
        {
            RefreshArea();
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
            _layout = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
            navBarGroupControlContainer1.Height = this.Height - 170;
            navBarGroupControlContainer2.Height = this.Height - 170;
            navBarGroupControlContainer5.Height = this.Height - 170;
            navBarGroupControlContainer6.Height = this.Height - 170;
            navBarGroupControlContainer1.OwnerGroup.Expanded = true;
        }
        #endregion
        #region[Function]
        void RefreshPos(string areacode)
        {
            try
            {
                grdPos.DataSource = null;
                grdSelectedPos.DataSource = null;
                grdPayType.DataSource = null;
                grdSelectedPayType.DataSource = null;
                grdProd.DataSource = null;
                grdSelectedProd.DataSource = null;
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130135, 130135, new object[] { areacode });
                if (FormUtility.ValidateQuery(res))
                {
                    #region[POS]
                    var query = from row in res.Data.Tables[0].AsEnumerable()
                                where row.Field<decimal>("TYPE") == 1
                                select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            grdPos.DataSource = query.CopyToDataTable();
                            gvwPos.Columns[0].Caption = "Төлөв";
                            gvwPos.Columns[1].Caption = "Дугаар";
                            gvwPos.Columns[2].Caption = "Нэр";
                            gvwPos.Columns[3].Caption = "Төрөл";
                            gvwPos.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                            gvwPos.Columns[3].Visible = false;
                            gvwPos.Columns[1].OptionsColumn.AllowEdit = false;
                            gvwPos.Columns[2].OptionsColumn.AllowEdit = false;
                            gvwPos.GroupPanelText = "Сонгогдоогүй посууд";
                        }
                    }
                    query = null;
                    query = from row in res.Data.Tables[0].AsEnumerable()
                            where row.Field<decimal>("TYPE") == 0
                                select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            grdSelectedPos.DataSource = query.CopyToDataTable();
                            gvwSelectedPos.Columns[0].Caption = "Төлөв";
                            gvwSelectedPos.Columns[1].Caption = "Дугаар";
                            gvwSelectedPos.Columns[2].Caption = "Нэр";
                            gvwSelectedPos.Columns[3].Caption = "Төрөл";
                            gvwSelectedPos.Columns[3].Visible = false;
                            gvwSelectedPos.Columns[1].OptionsColumn.AllowEdit = false;
                            gvwSelectedPos.Columns[2].OptionsColumn.AllowEdit = false;
                            gvwSelectedPos.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                            gvwSelectedPos.GroupPanelText = "Сонгогдсон посууд";
                        }
                    }
                    #endregion
                    #region[PAYMENTTYPE]
                    query = null;
                    query = from row in res.Data.Tables[1].AsEnumerable()
                                where row.Field<decimal>("TYPE") == 1
                                select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            grdPayType.DataSource = query.CopyToDataTable();
                            gvwPayType.Columns[0].Caption = "Төлөв";
                            gvwPayType.Columns[1].Caption = "Дугаар";
                            gvwPayType.Columns[2].Caption = "Нэр";
                            gvwPayType.Columns[3].Caption = "Төрөл";
                            gvwPayType.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                            gvwPayType.Columns[3].Visible = false;
                            gvwPayType.Columns[1].OptionsColumn.AllowEdit = false;
                            gvwPayType.Columns[2].OptionsColumn.AllowEdit = false;
                            gvwPayType.GroupPanelText = "Сонгогдоогүй төлбөрийн хэрэгслүүд";
                        }
                    }
                    query = null;
                    query = from row in res.Data.Tables[1].AsEnumerable()
                            where row.Field<decimal>("TYPE") == 0
                            select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            grdSelectedPayType.DataSource = query.CopyToDataTable();
                            gvwSelectedPayType.Columns[0].Caption = "Төлөв";
                            gvwSelectedPayType.Columns[1].Caption = "Дугаар";
                            gvwSelectedPayType.Columns[2].Caption = "Нэр";
                            gvwSelectedPayType.Columns[3].Caption = "Төрөл";
                            gvwSelectedPayType.Columns[3].Visible = false;
                            gvwSelectedPayType.Columns[1].OptionsColumn.AllowEdit = false;
                            gvwSelectedPayType.Columns[2].OptionsColumn.AllowEdit = false;
                            gvwSelectedPayType.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                            gvwSelectedPayType.GroupPanelText = "Сонгогдсон төлбөрийн хэрэгслүүд";
                        }
                    }
                    #endregion
                    #region[PRODUCT]
                    query = null;
                    query = from row in res.Data.Tables[2].AsEnumerable()
                            where row.Field<decimal>("TYPE") == 1
                            select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            grdProd.DataSource = query.CopyToDataTable();
                            gvwProd.Columns[0].Caption = "Төлөв";
                            gvwProd.Columns[1].Caption = "Дугаар";
                            gvwProd.Columns[2].Caption = "Нэр";
                            gvwProd.Columns[3].Caption = "Төрөл";
                            gvwProd.Columns[4].Caption = "Төрөл";
                            gvwProd.Columns[5].Caption = "Төрөл";
                            gvwProd.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                            gvwProd.Columns[3].Visible = false;
                            gvwProd.Columns[5].Visible = false;
                            gvwProd.Columns[1].OptionsColumn.AllowEdit = false;
                            gvwProd.Columns[2].OptionsColumn.AllowEdit = false;
                            gvwProd.Columns[3].OptionsColumn.AllowEdit = false;
                            gvwProd.Columns[4].OptionsColumn.AllowEdit = false;
                            gvwProd.GroupPanelText = "Сонгогдоогүй бүтээгдэхүүнүүд";
                        }
                    }
                    query = null;
                    query = from row in res.Data.Tables[2].AsEnumerable()
                            where row.Field<decimal>("TYPE") == 0
                            select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            grdSelectedProd.DataSource = query.CopyToDataTable();
                            gvwSelectedProd.Columns[0].Caption = "Төлөв";
                            gvwSelectedProd.Columns[1].Caption = "Дугаар";
                            gvwSelectedProd.Columns[2].Caption = "Нэр";
                            gvwSelectedProd.Columns[3].Caption = "Төрөл";
                            gvwSelectedProd.Columns[4].Caption = "Төрөл";
                            gvwSelectedProd.Columns[5].Caption = "Төрөл";
                            gvwSelectedProd.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                            gvwSelectedProd.Columns[3].Visible = false;
                            gvwSelectedProd.Columns[5].Visible = false;
                            gvwSelectedProd.Columns[1].OptionsColumn.AllowEdit = false;
                            gvwSelectedProd.Columns[2].OptionsColumn.AllowEdit = false;
                            gvwSelectedProd.Columns[3].OptionsColumn.AllowEdit = false;
                            gvwSelectedProd.Columns[4].OptionsColumn.AllowEdit = false;
                            gvwSelectedProd.Columns[5].OptionsColumn.AllowEdit = false;
                            gvwSelectedProd.GroupPanelText = "Сонгогдсон бүтээгдэхүүнүүд";
                        }
                    }
                    #endregion
                    #region[PACKAGE]
                    query = null;
                    query = from row in res.Data.Tables[3].AsEnumerable()
                            where row.Field<decimal>("TYPE") == 1
                            select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            grdPack.DataSource = query.CopyToDataTable();
                            gvwPack.Columns[0].Caption = "Төлөв";
                            gvwPack.Columns[1].Caption = "Дугаар";
                            gvwPack.Columns[2].Caption = "Нэр";
                            gvwPack.Columns[3].Caption = "Төрөл";
                            gvwPack.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                            gvwPack.Columns[3].Visible = false;
                            gvwPack.Columns[1].OptionsColumn.AllowEdit = false;
                            gvwPack.Columns[2].OptionsColumn.AllowEdit = false;
                            gvwPack.GroupPanelText = "Сонгогдоогүй багцууд";
                        }
                    }
                    query = null;
                    query = from row in res.Data.Tables[3].AsEnumerable()
                            where row.Field<decimal>("TYPE") == 0
                            select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            grdSelectedPack.DataSource = query.CopyToDataTable();
                            gvwSelectedPack.Columns[0].Caption = "Төлөв";
                            gvwSelectedPack.Columns[1].Caption = "Дугаар";
                            gvwSelectedPack.Columns[2].Caption = "Нэр";
                            gvwSelectedPack.Columns[3].Caption = "Төрөл";
                            gvwSelectedPack.Columns[3].Visible = false;
                            gvwSelectedPack.Columns[1].OptionsColumn.AllowEdit = false;
                            gvwSelectedPack.Columns[2].OptionsColumn.AllowEdit = false;
                            gvwSelectedPack.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                            gvwSelectedPack.GroupPanelText = "Сонгогдсон багцууд";
                        }
                    }
                    #endregion
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Cancel()
        {
            btnGroupEdit.Text = "Засах";
            btnGroupAdd.Enabled = true;
            btnGroupDelete.Enabled = true;
            btnGroupCancel.Enabled = false;
            knowbutton = "";
            txtAreaCode.Properties.ReadOnly = true;
            txtName.Properties.ReadOnly = true;
            txtOrderNo.Properties.ReadOnly = true;
            txtName2.Properties.ReadOnly = true;
            txtAreaCode.BackColor = Color.Gainsboro;
            txtName.BackColor = Color.Gainsboro;
            txtName2.BackColor = Color.Gainsboro;
            txtOrderNo.BackColor = Color.Gainsboro;
        }
        void RefreshArea()
        {
            try
            {
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130130, 130130, null);
                if (FormUtility.ValidateQuery(res))
                {
                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layout);
                    gridView1.Columns[0].Caption = "Дугаар";
                    gridView1.Columns[1].Caption = "Нэр";
                    gridView1.Columns[2].Caption = "Нэр 2";
                    gridView1.Columns[3].Caption = "Эрэмбэ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            //((System.ComponentModel.ISupportInitialize)(ri)).BeginInit();
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
        private void btnGroupAdd_Click(object sender, EventArgs e)
        {
            btnGroupEdit.Text = "Хадгалах";
            btnGroupAdd.Enabled = false;
            btnGroupDelete.Enabled = false;
            btnGroupCancel.Enabled = true;
            txtAreaCode.Properties.ReadOnly = false;
            txtName.Properties.ReadOnly = false;
            txtOrderNo.Properties.ReadOnly = false;
            txtName2.Properties.ReadOnly = false;
            txtAreaCode.BackColor = Color.LemonChiffon;
            txtName.BackColor = Color.LemonChiffon;
            txtName2.BackColor = Color.White;
            txtOrderNo.BackColor = Color.LemonChiffon;
        }
        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (btnGroupEdit.Text == "Засах")
            {
                btnGroupAdd.Enabled = false;
                btnGroupDelete.Enabled = false;
                btnGroupCancel.Enabled = true;
                btnGroupEdit.Text = "Хадгалах";
                knowbutton = "Засах";
                txtAreaCode.Properties.ReadOnly = false;
                txtName.Properties.ReadOnly = false;
                txtOrderNo.Properties.ReadOnly = false;
                txtName2.Properties.ReadOnly = false;
                txtAreaCode.BackColor = Color.LemonChiffon;
                txtName.BackColor = Color.LemonChiffon;
                txtName2.BackColor = Color.White;
                txtOrderNo.BackColor = Color.LemonChiffon;
                return;
            }
            else
            {
                Result res = new Result();
                string msg = "";
                object[] obj = {
                                   Static.ToStr(txtAreaCode.EditValue),
                                   Static.ToStr(txtName.EditValue),
                                   Static.ToStr(txtName2.EditValue),
                                   Static.ToInt(txtOrderNo.EditValue)
                               };
                if (knowbutton == "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130132, 130132, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130133, 130133, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    RefreshArea();
                    knowbutton = "";
                    Cancel();
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
        }
        private void btnGroupCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        #endregion
        #region[Form Events]
        private void frmWorkArea_SizeChanged(object sender, EventArgs e)
        {
            navBarGroupControlContainer1.Height = this.Height - 170;
            navBarGroupControlContainer2.Height = this.Height - 170;
            navBarGroupControlContainer5.Height = this.Height - 170;
            navBarGroupControlContainer6.Height = this.Height - 170;
        }
        private void navBarControl1_GroupExpanded(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            foreach(DevExpress.XtraNavBar.NavBarGroup nbargroup in navBarControl1.Groups)
            {
                if (e.Group.Name != nbargroup.Name)
                {
                    nbargroup.Expanded = false;
                }
            }
        }
        #endregion
        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130134, 130134, new object[] { Static.ToStr(txtAreaCode.EditValue) });
            if (FormUtility.ValidateQuery(res))
            {
                MessageBox.Show("Амжилттай устгагдлаа.");
                RefreshArea();
                knowbutton = "";
                Cancel();
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                txtAreaCode.EditValue = dr["AREACODE"];
                txtName.EditValue = dr["NAME"];
                txtName2.EditValue = dr["NAME2"];
                txtOrderNo.EditValue = dr["ORDERNO"];
                RefreshPos(txtAreaCode.Text);
            }
        }
        private void btnposuse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt=(DataTable)grdPos.DataSource;
                var query = from row in dt.AsEnumerable()
                                where row.Field<decimal>("status") == 1
                                select row;
                if (query == null || query.Count() == 0)
                {
                    MessageBox.Show("Посуудаа сонгоно уу.");
                    return;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130136, 130136, new object[] { Static.ToStr(txtAreaCode.EditValue), dt, 0 });
                if (FormUtility.ValidateQuery(res))
                {
                    RefreshPos(txtAreaCode.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnposunuse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grdSelectedPos.DataSource;
                var query = from row in dt.AsEnumerable()
                            where row.Field<decimal>("status") == 1
                            select row;
                if (query == null || query.Count() == 0)
                {
                    MessageBox.Show("Посуудаа сонгоно уу.");
                    return;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130137, 130137, new object[] { Static.ToStr(txtAreaCode.EditValue), dt, 0 });
                if (FormUtility.ValidateQuery(res))
                {
                    RefreshPos(txtAreaCode.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnpayunuse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grdSelectedPayType.DataSource;
                var query = from row in dt.AsEnumerable()
                            where row.Field<decimal>("status") == 1
                            select row;
                if (query == null || query.Count() == 0)
                {
                    MessageBox.Show("Төлбөрийн хэрэгсэл сонгоно уу.");
                    return;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130137, 130137, new object[] { Static.ToStr(txtAreaCode.EditValue), grdSelectedPayType.DataSource, 1 });
                if (FormUtility.ValidateQuery(res))
                {
                    RefreshPos(txtAreaCode.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnpayuse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grdPayType.DataSource;
                var query = from row in dt.AsEnumerable()
                            where row.Field<decimal>("status") == 1
                            select row;
                if (query == null || query.Count() == 0)
                {
                    MessageBox.Show("Төлбөрийн хэрэгсэл сонгоно уу.");
                    return;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130136, 130136, new object[] { Static.ToStr(txtAreaCode.EditValue), grdPayType.DataSource, 1 });
                if (FormUtility.ValidateQuery(res))
                {
                    RefreshPos(txtAreaCode.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnprodunuse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grdSelectedProd.DataSource;
                var query = from row in dt.AsEnumerable()
                            where row.Field<decimal>("status") == 1
                            select row;
                if (query == null || query.Count() == 0)
                {
                    MessageBox.Show("Бараа үйлчилгээ сонгоно уу.");
                    return;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130137, 130137, new object[] { Static.ToStr(txtAreaCode.EditValue), grdSelectedProd.DataSource, 2 });
                if (FormUtility.ValidateQuery(res))
                {
                    RefreshPos(txtAreaCode.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnproduse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grdProd.DataSource;
                var query = from row in dt.AsEnumerable()
                            where row.Field<decimal>("status") == 1
                            select row;
                if (query == null || query.Count() == 0)
                {
                    MessageBox.Show("Бараа үйлчилгээ сонгоно уу.");
                    return;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130136, 130136, new object[] { Static.ToStr(txtAreaCode.EditValue), grdProd.DataSource, 2 });
                if (FormUtility.ValidateQuery(res))
                {
                    RefreshPos(txtAreaCode.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPackUnUse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grdSelectedPack.DataSource;
                var query = from row in dt.AsEnumerable()
                            where row.Field<decimal>("status") == 1
                            select row;
                if (query == null || query.Count() == 0)
                {
                    MessageBox.Show("Багц сонгоно уу.");
                    return;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130137, 130137, new object[] { Static.ToStr(txtAreaCode.EditValue), grdSelectedPack.DataSource, 3 });
                if (FormUtility.ValidateQuery(res))
                {
                    RefreshPos(txtAreaCode.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPackUse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grdPack.DataSource;
                var query = from row in dt.AsEnumerable()
                            where row.Field<decimal>("status") == 1
                            select row;
                if (query == null || query.Count() == 0)
                {
                    MessageBox.Show("Багц сонгоно уу.");
                    return;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130136, 130136, new object[] { Static.ToStr(txtAreaCode.EditValue), grdPack.DataSource, 3 });
                if (FormUtility.ValidateQuery(res))
                {
                    RefreshPos(Static.ToStr(txtAreaCode.EditValue));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmWorkArea_FormClosed(object sender, FormClosedEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layout);
        }
    }
}