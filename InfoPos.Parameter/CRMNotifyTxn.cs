using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;
using InfoPos.Messages;
namespace InfoPos.Parameter
{
    public partial class CRMNotifyTxn : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        Core.Core _core;
        string appname = "", formname = "";
        Form FormName = null;
        object[] OldValue;
        object[] OldValueTxn;
        int _id;
        int rowhandle = 0;
        FunctionParam fp = new FunctionParam();
        int btn = 1;
        #endregion
        #region[Байгуулагч функц]
        public CRMNotifyTxn(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            InitCombo();
            RefreshNotifySchema();
        }
        #endregion
        #region[Init]
        private void InitCombo()
        {
            fp.SetCombo(_core,"TXN","TRANCODE","NAME",310047,cboTxn,"",null);
            FormUtility.LookUpEdit_SetList(ref cboGroupID, 0, "Reporter");
            FormUtility.LookUpEdit_SetList(ref cboGroupID, 1, "CurrentAssignee");
            FormUtility.LookUpEdit_SetList(ref cboGroupID, 2, "CurrentUser");
            FormUtility.LookUpEdit_SetList(ref cboGroupID, 3, "Project Owner");
            FormUtility.LookUpEdit_SetList(ref cboGroupID, 4, "Component Owner");
            FormUtility.LookUpEdit_SetList(ref cboGroupID, 5, "User");
            FormUtility.LookUpEdit_SetList(ref cboGroupID, 6, "Group");           
            cboGroupID.ItemIndex = 0;
        }
        #endregion
        #region[Function]
        private void RefreshNotifySchema()
        {
            try
            {
                Result res = new Result();
                grdNotify.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310045, 310045, null);
                if (res.ResultNo == 0)
                {
                    if (res.Data != null && res.Data.Tables[0].Rows.Count != 0)
                    {
                        grdNotify.DataSource = res.Data.Tables[0];
                        SetNotify();
                    }
                    if (gvwNotify.RowCount == 0)
                    {
                        numSchemaID.EditValue = null;
                        txtName.EditValue = null;
                        txtName2.EditValue = null;
                        numOrderNo.EditValue = null;
                        numNotityTxnID.EditValue = null;
                    }
                }
                else
                {
                    MessageBox.Show(res.ResultNo + "  " + res.ResultDesc);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetNotify()
        {
            gvwNotify.Columns[0].Caption = "Мэдэгдэлийн дугаар";
            gvwNotify.Columns[1].Caption = "Нэр";
            gvwNotify.Columns[2].Caption = "Нэр 2";
            gvwNotify.Columns[3].Caption = "Жагсаалтын эрэмбэ";
            gvwNotify.Columns[0].OptionsColumn.AllowEdit = false;
            gvwNotify.Columns[1].OptionsColumn.AllowEdit = false;
            gvwNotify.Columns[2].OptionsColumn.AllowEdit = false;
            gvwNotify.Columns[3].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwNotify);

        }
        private void SetNotifyTxn()
        {

            gvwNotifyTxn.Columns[0].Caption = "Мэдэгдэлийн дугаар";
            gvwNotifyTxn.Columns[1].Caption = "Гүйлгээний код";
            gvwNotifyTxn.Columns[2].Caption = "Төрлийн код";
            gvwNotifyTxn.Columns[2].Visible = false;
            gvwNotifyTxn.Columns[3].Caption = "Төрлийн нэр";
            gvwNotifyTxn.Columns[4].Caption = "Дугаар";
            gvwNotifyTxn.Columns[0].OptionsColumn.AllowEdit = false;
            gvwNotifyTxn.Columns[1].OptionsColumn.AllowEdit = false;
            gvwNotifyTxn.Columns[2].OptionsColumn.AllowEdit = false;
            gvwNotifyTxn.Columns[3].OptionsColumn.AllowEdit = false;
            gvwNotifyTxn.Columns[4].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwNotifyTxn);
        }
        private void RefreshNotifyTxn(int schemaid)
        {
            try
            {
                Result res = new Result();
                grdNotifyTxn.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310050, 310050, new object[] { schemaid });
                if (res.ResultNo == 0)
                {
                    if (res.Data != null && res.Data.Tables[0].Rows.Count != 0)
                    {
                        grdNotifyTxn.DataSource = res.Data.Tables[0];
                        SetNotifyTxn();
                    }
                }
                else
                {
                    MessageBox.Show(res.ResultNo + "  " + res.ResultDesc);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SaveEdit(bool issave)
        {
            rowhandle = gvwNotify.FocusedRowHandle;
            if (check())
            {
                Result res = new Result();
                try
                {
                    string msg = "";
                    object[] FieldName = { "SchemaID", "Name", "Name2", "OrderNo" };
                    object[] NewValue = { Static.ToInt(numSchemaID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue) };
                    if (issave)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310047, 310047, new object[] { NewValue, FieldName });
                        msg = "Амжилттай нэмлээ .";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310048, 310048, new object[] { NewValue, OldValue, FieldName });
                        msg = "Амжилттай засварлалаа .";
                        gvwNotify.FocusedRowHandle = rowhandle;
                    }

                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        RefreshNotifySchema();
                        MessageBox.Show(msg);
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void SaveEditTxn(bool issave)
        {
            rowhandle = gvwNotifyTxn.FocusedRowHandle;
            if (checkTxn())
            {
                Result res = new Result();
                try
                {
                    string msg = "";
                    object[] FieldName = { "SchemaID", "TRANCODE", "TYPEID", "ID" };
                    object[] NewValue = { Static.ToInt(numNotityTxnID.EditValue), Static.ToStr(cboTxn.EditValue), Static.ToStr(cboGroupID.EditValue), Static.ToInt(cboIDD.EditValue) };
                    if (issave)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310052, 310052, new object[] { NewValue, FieldName });
                        msg = "Амжилттай нэмлээ .";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310053, 310053, new object[] { OldValueTxn, NewValue, FieldName });
                        msg = "Амжилттай засварлалаа .";
                        gvwNotifyTxn.FocusedRowHandle = rowhandle;
                    }

                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        RefreshNotifyTxn(Static.ToInt(numNotityTxnID.EditValue));
                        MessageBox.Show(msg);
                        DataRow _dr = gvwNotifyTxn.GetDataRow(gvwNotifyTxn.FocusedRowHandle);
                        if (_dr != null)
                        {
                            OldValueTxn[1] = Static.ToInt(_dr["TRANCODE"]);
                            OldValueTxn[2] = Static.ToInt(_dr["TYPEID"]);
                            OldValueTxn[3] = Static.ToInt(_dr[4]);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        bool check()
        {
            if (numSchemaID.Text == "")
            {
                MessageBox.Show("Мэдэгдэлийн дугаар оруулна уу .");
                numSchemaID.Select();
                return false;
            }
            else
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Нэр оруулна уу .");
                    txtName.Select();
                    return false;
                }
                else
                {
                    if (numOrderNo.Text == "")
                    {
                        MessageBox.Show("Эрэмбэ оруулна уу.");
                        numOrderNo.Select();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        bool checkTxn()
        {
            if (numNotityTxnID.EditValue == null)
            {
                numSchemaID.Select();
                MessageBox.Show("Мэдэгдэлийн схемээ эхэлж үүсгэнэ үү .");
                return false;
            }
            else
            {
                if (cboTxn.EditValue == null)
                {
                    MessageBox.Show("Гүйлгээний код оруулна уу .");
                    cboTxn.Select();
                    return false;
                }
                else
                {
                    if (Static.ToInt(cboGroupID.EditValue) == 5 || Static.ToInt(cboGroupID.EditValue) == 6)
                    {
                        if (cboIDD.Text == "0 - " || cboIDD.Text == "")
                        {
                            MessageBox.Show("Дугаар оруулна уу .");
                            txtName.Select();
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return true;
                }
            }
        }
        void Clear()
        {
            cboGroupID.ItemIndex = 0;
            cboIDD.EditValue = 0;
            cboTxn.EditValue = null;
        }
        #endregion
        #region[BTN]
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SaveEdit(true);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            SaveEdit(false);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show(MSG.Messages(_core.Lang, 10007), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    DataRow dr = gvwNotify.GetFocusedDataRow();
                    if (dr != null)
                    {
                        numSchemaID.EditValue = Static.ToInt(dr["SchemaID"]);
                        txtName.EditValue = Static.ToStr(dr["Name"]);
                        txtName2.EditValue = Static.ToStr(dr["Name2"]);
                        numOrderNo.EditValue = Static.ToInt(dr["OrderNo"]);
                        Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310049, 310049, new object[] { Static.ToInt(numSchemaID.EditValue) });
                        if (r.ResultNo != 0)
                        {
                            MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        }
                        else
                        {
                            MessageBox.Show(MSG.Messages(_core.Lang, 10003));
                            RefreshNotifySchema();
                            btn = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            numSchemaID.EditValue = null;
            txtName.EditValue = null;
            txtName2.EditValue = null;
            numOrderNo.EditValue = null;
        }

        private void btnAddTxn_Click(object sender, EventArgs e)
        {
            SaveEditTxn(true);
        }
        private void btnEditTxn_Click(object sender, EventArgs e)
        {
            if(numNotityTxnID.Text != "")
            SaveEditTxn(false);
        }
        private void btnDeleteTxn_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show(MSG.Messages(_core.Lang, 10007), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                        Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310054, 310054, new object[] { Static.ToInt(numNotityTxnID.EditValue), Static.ToStr(cboTxn.EditValue), Static.ToStr(cboGroupID.EditValue), Static.ToInt(cboIDD.EditValue) });
                        if (r.ResultNo != 0)
                        {
                            MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        }
                        else
                        {
                            MessageBox.Show(MSG.Messages(_core.Lang, 10003));
                            RefreshNotifyTxn(Static.ToInt(numSchemaID.EditValue));
                            DataRow _dr = gvwNotifyTxn.GetDataRow(gvwNotifyTxn.FocusedRowHandle);
                            if (_dr != null)
                            {
                                cboTxn.EditValue = Static.ToInt(_dr["TRANCODE"]);
                                cboGroupID.EditValue = Static.ToInt(_dr["TYPEID"]);
                                FormUtility.LookUpEdit_SetValue(ref cboIDD, Static.ToInt(_dr[4]));
                            }
                            else
                            {
                                Clear();
                            }
                            btn = 1;
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnClearTxn_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #endregion
        #region[FormEvent]
        private void CRMNotifyTxn_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwNotify);
            FormUtility.SaveStateGrid(appname, formname, ref gvwNotifyTxn);
        }
        private void CRMNotifyTxn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        private void gvwNotify_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwNotify.GetFocusedDataRow();
            if (dr != null)
            {
                numSchemaID.EditValue = Static.ToInt(dr["SchemaID"]);
                numNotityTxnID.EditValue = numSchemaID.EditValue;
                txtName.EditValue = Static.ToStr(dr["Name"]);
                txtName2.EditValue = Static.ToStr(dr["Name2"]);
                numOrderNo.EditValue = Static.ToInt(dr["OrderNo"]);
                object[] value = new object[4];
                value[0] = Static.ToInt(numSchemaID.EditValue);
                value[1] = Static.ToStr(txtName.EditValue);
                value[2] = Static.ToStr(txtName2.EditValue);
                value[3] = Static.ToInt(numOrderNo.EditValue);
                OldValue = value;
            }
            RefreshNotifyTxn(Static.ToInt(numSchemaID.EditValue));
        }
        private void gvwNotifyTxn_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DataRow dr = gvwNotifyTxn.GetFocusedDataRow();
                if (dr != null)
                {
                    cboTxn.EditValue = Static.ToInt(dr["TRANCODE"]);
                    cboGroupID.EditValue = Static.ToInt(dr["TYPEID"]);
                    FormUtility.LookUpEdit_SetValue(ref cboIDD, Static.ToInt(dr[4]));
                    object[] value = new object[4];
                    value[0] = Static.ToInt(numNotityTxnID.EditValue);
                    value[1] = Static.ToStr(cboTxn.EditValue);
                    value[3] = Static.ToInt(cboIDD.EditValue);
                    value[2] = Static.ToStr(cboGroupID.EditValue);
                    OldValueTxn = value;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cboGroupID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Static.ToInt(cboGroupID.EditValue) == 5 || Static.ToInt(cboGroupID.EditValue) == 6)
                {
                    cboIDD.Visible = true;
                    labelControl6.Visible = true;
                    if (Static.ToInt(cboGroupID.EditValue) == 5)
                    {
                        fp.SetCombo(_core, "USERS", "USERNO", "USERLNAME", 310047, cboIDD, "", null);
                    }
                    else
                    {
                        fp.SetCombo(_core, "TXNGROUP", "GROUPID", "NAME", 310047, cboIDD, "", null);
                    }
                    DataRow dr = gvwNotifyTxn.GetFocusedDataRow();
                    if (dr != null)
                    {
                        FormUtility.LookUpEdit_SetValue(ref cboIDD, Static.ToInt(dr[4]));
                    }
                }
                else
                {
                    cboIDD.EditValue = 0;
                    cboIDD.Visible = false;
                    labelControl6.Visible = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CRMNotifyTxn_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            if (_core.Resource != null)
            {
                btnAdd.Image = _core.Resource.GetImage("navigate_save");
                btnEdit.Image = _core.Resource.GetImage("navigate_edit");
                btnDelete.Image = _core.Resource.GetImage("navigate_delete");
                btnClear.Image = _core.Resource.GetImage("navigate_clear");
                btnAddTxn.Image = _core.Resource.GetImage("navigate_save");
                btnEditTxn.Image = _core.Resource.GetImage("navigate_edit");
                btnDeleteTxn.Image = _core.Resource.GetImage("navigate_delete");
                btnClearTxn.Image = _core.Resource.GetImage("navigate_clear");
            }
        }
        #endregion
    }
}