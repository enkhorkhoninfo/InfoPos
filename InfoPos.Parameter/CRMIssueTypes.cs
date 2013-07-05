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
using ISM.Template;
using InfoPos.Messages;

namespace InfoPos.Parameter
{
    public partial class CRMIssueTypes : XtraForm
    {
        #region [ Хувьсагчууд ]
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        int rowhandle = 0;
        int issueTypeid = 0;
        int AddTxn = 310007;
        int SelectTxn = 310005;
        int EditTxn = 310008;
        int DeleteTxn = 310009;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public CRMIssueTypes(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            RefreshIssueTypes();
            InitCombo();
            if (_core.Resource != null)
            {
                btnRiskGroupAdd.Image = _core.Resource.GetImage("navigate_save");
                btnRiskGroupEdit.Image = _core.Resource.GetImage("navigate_edit");
                btnRiskGroupDelete.Image = _core.Resource.GetImage("navigate_delete");
                bntRiskGroupClear.Image = _core.Resource.GetImage("navigate_clear");
                btnSave.Image = _core.Resource.GetImage("navigate_save");
            }
        }
        #endregion
        #region [ Init ]
        private void RefreshIssueTypes()
        {
            rowhandle = gvwIssueTypes.FocusedRowHandle;
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, SelectTxn, SelectTxn, null);
                if (res.ResultNo == 0 || res.ResultNo == 9110014)
                {
                    grdIssueTypes.DataSource = res.Data.Tables[0];
                    SetIssueType();
                    if (btn == 1)
                    {
                        gvwIssueTypes.FocusedRowHandle = rowhandle - 1;
                        btn = 0;
                    }
                    else
                    {
                        gvwIssueTypes.FocusedRowHandle = rowhandle;
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
        private void InitCombo()
        {
            FormUtility.LookUpEdit_SetList(ref cboVote, 0 ,"Үгүй");
            FormUtility.LookUpEdit_SetList(ref cboVote, 1, "Тийм");
            cboVote.ItemIndex = 0;
        }
        #endregion
        #region [ Events ]
        void SetIssueType()
        {
            gvwIssueTypes.Columns[0].Caption = "Төслийн төрлийн дугаар";
            gvwIssueTypes.Columns[1].Caption = "Нэр";
            gvwIssueTypes.Columns[2].Caption = "Нэр 2";
            gvwIssueTypes.Columns[3].Caption = "Жагсаалтын эрэмбэ";
            gvwIssueTypes.Columns[4].Caption = "Санал асуулгатай эсэх";
            gvwIssueTypes.Columns[0].OptionsColumn.AllowEdit = false;
            gvwIssueTypes.Columns[1].OptionsColumn.AllowEdit = false;
            gvwIssueTypes.Columns[2].OptionsColumn.AllowEdit = false;
            gvwIssueTypes.Columns[3].OptionsColumn.AllowEdit = false;
            gvwIssueTypes.Columns[4].OptionsColumn.AllowEdit = false;
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwIssueTypes);
            switch (btn)
            {
                case 0: gvwIssueTypes.FocusedRowHandle = rowhandle; break;
                case 1: gvwIssueTypes.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }
        #endregion
        #region[FormEvent]
        private void CRMIssueTypes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        private void CRMIssueTypes_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwIssueTypes);
        }
        #endregion
        #region[BTN]
        private void btnRiskGroupAdd_Click(object sender, EventArgs e)
        {
            SaveEdit(true);
        }
        private void btnRiskGroupEdit_Click(object sender, EventArgs e)
        {
            SaveEdit(false);
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwIssueTypes);
        }
        #endregion
        void SaveEdit(bool issave)
        {
            rowhandle = gvwIssueTypes.FocusedRowHandle;
            if (check())
            {
                Result res = new Result();
                try
                {
                    string msg = "";
                    object[] FieldName = { "IssueTypeID", "Name", "Name2", "OrderNo", "Vote"};
                   object[] NewValue = { Static.ToInt(numTypeCode.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue), Static.ToInt(cboVote.EditValue) };
                    if (issave)
                    {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxn, AddTxn, new object[] { NewValue, FieldName });
                        msg = "Амжилттай нэмлээ .";
                    }
                    else
                    {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxn, EditTxn, new object[] { NewValue, OldValue, FieldName });
                        msg = "Амжилттай засварлалаа .";
                        gvwIssueTypes.FocusedRowHandle = rowhandle;
                    }

                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        RefreshIssueTypes();
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
        bool check()
        {
            if (numTypeCode.Text == "")
            {
                MessageBox.Show("Төрлийн дугаар оруулна уу .");
                numTypeCode.Select();
                return false;
            }
            else
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Төрлийн нэр оруулна уу .");
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
                        if (Static.ToStr(cboVote.EditValue) == null)
                        {
                            MessageBox.Show("Санал асуулга оруулна уу.");
                            cboVote.Select();
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        private void btnRiskGroupDelete_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show(MSG.Messages(_core.Lang, 10007), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxn, DeleteTxn, new object[] { Static.ToInt(numTypeCode.EditValue) });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                    else
                    {
                        MessageBox.Show(MSG.Messages(_core.Lang, 10003));
                        RefreshIssueTypes();
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bntRiskGroupClear_Click(object sender, EventArgs e)
        {
            numOrderNo.EditValue = null;
            numOrderNo.Text = "";
            txtName.EditValue = null;
            txtName.Text = "";
            txtName2.EditValue = null;
            txtName2.Text = "";
            numTypeCode.EditValue = null;
            numTypeCode.Text = "";
            cboVote.ItemIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {               
                DataTable dt = (DataTable)grdTracks.DataSource;
                object[] obj = new object[2];
                obj[0] = issueTypeid;
                obj[1] = dt;
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310085, 310085, obj);
                if (r.ResultNo == 0)
                {
                    MessageBox.Show("Амжмлттай хадгалагдлаа .");
                }
                else
                {
                    MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetTrack()
        {
            gvwTracks.Columns[0].Caption = "Төлөв";
            gvwTracks.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwTracks.Columns[1].Caption = "Алхмын дугаар";
            gvwTracks.Columns[2].Caption = "Нэр";
            gvwTracks.Columns[3].Caption = "Нэр 2";
            gvwTracks.Columns[4].Caption = "Жагсаалтын эрэмбэ";
            gvwTracks.Columns[1].OptionsColumn.AllowEdit = false;
            gvwTracks.Columns[2].OptionsColumn.AllowEdit = false;
            gvwTracks.Columns[3].OptionsColumn.AllowEdit = false;
            gvwTracks.Columns[4].OptionsColumn.AllowEdit = false;
        }
        private void gvwIssueTypes_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr=gvwIssueTypes.GetDataRow(gvwIssueTypes.FocusedRowHandle);
            if (dr != null)
            {
                issueTypeid = Static.ToInt(dr[0]);
                numTypeCode.EditValue = issueTypeid;
                txtName.EditValue = Static.ToStr(dr[1]);
                txtName2.EditValue = Static.ToStr(dr[2]);
                numOrderNo.EditValue = Static.ToInt(dr[3]);
                object[] Value = { Static.ToInt(numTypeCode.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue), Static.ToInt(cboVote.EditValue) };
                OldValue = Value;
            }
            RefreshTrack(issueTypeid);
        }
        private void RefreshTrack(int TypeID)
        {
            try
            {
                grdTracks.DataSource = null;
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310086, 310086, new object[] {TypeID});
                if (r.ResultNo == 0)
                {
                    if (r.Data != null)
                    {   
                        grdTracks.DataSource = r.Data.Tables[0];
                        SetTrack();
                    }
                }
                else
                {
                    MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region[Checkbox]
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

        private void CRMIssueTypes_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwIssueTypes);
        }
    }
}