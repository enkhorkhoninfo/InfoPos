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
    public partial class CRMIssueMemberPurp : frmTempProp
    {
        #region [ Хувьсагчууд ]
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        int rowhandle = 0;
        int AddTxn = 310032;
        int SelectTxn = 310030;
        int EditTxn = 310033;
        int DeleteTxn = 310034;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        FunctionParam fp = new FunctionParam();
        #endregion
        #region [ Байгуулагч функц ]
        public CRMIssueMemberPurp(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(CRMIssueMemberPurp_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(CRMIssueMemberPurp_EventRefreshAfter);
                this.EventSave += new delegateEventSave(CRMIssueMemberPurp_EventSave);
                this.EventEdit += new delegateEventEdit(CRMIssueMemberPurp_EventEdit);
                this.EventDelete += new delegateEventDelete(CRMIssueMemberPurp_EventDelete);

                this.FieldLinkAdd("numPurpID", "IssuePurpID", "", true, true);
                this.FieldLinkAdd("txtName", "Name", "", true, false);
                this.FieldLinkAdd("txtName2", "Name2", "", false, false);
                this.FieldLinkAdd("numOrderNo", "OrderNo", "", true, false);
                this.FieldLinkAdd("cboGroupID", "GroupID", "", true, false);

                this.Resource = _core.Resource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitCombo()
        {
            try
            {
                fp.SetCombo(_core, "TXNGROUP", "GROUPID", "Name", SelectTxn, cboGroupID, "", null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region [ Events ]
        void CRMIssueMemberPurp_EventDelete()
        {
            DialogResult DR = MessageBox.Show(MSG.Messages(_core.Lang, 10007), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxn, DeleteTxn, new object[] { Static.ToInt(numPurpID.EditValue) });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                    else
                    {
                        MessageBox.Show(MSG.Messages(_core.Lang, 10003));
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void CRMIssueMemberPurp_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToInt(numPurpID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue), Static.ToInt(cboGroupID.EditValue) };
            OldValue = Value;
        }
        void CRMIssueMemberPurp_EventSave(bool isnew, ref bool cancel)
        {
            Result r;
            string err = "";
            Control cont = null;
            if (!FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
            else
            {
                try
                {
                    if (!isnew)
                    {
                        object[] NewValue = { Static.ToInt(numPurpID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue), Static.ToInt(cboGroupID.EditValue) };
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxn, EditTxn, new object[] { NewValue, OldValue, FieldName });
                        MessageBox.Show(MSG.Messages(_core.Lang, 10001));
                    }

                    else
                    {
                        object[] NewValue = { Static.ToInt(numPurpID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue), Static.ToInt(cboGroupID.EditValue) };
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxn, AddTxn, new object[] { NewValue, FieldName });
                        MessageBox.Show(MSG.Messages(_core.Lang, 10000));
                    }
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void CRMIssueMemberPurp_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Үүргийн дугаар");
            this.FieldLinkSetColumnCaption(1, "Нэр");
            this.FieldLinkSetColumnCaption(2, "Нэр2");
            this.FieldLinkSetColumnCaption(3, "Жагсаалтын эрэмбэ");
            this.FieldLinkSetColumnCaption(4, "Бүлгийн дугаар");
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }
        void CRMIssueMemberPurp_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, SelectTxn, SelectTxn, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                }
                else
                {
                    dt = r.Data.Tables[0];
                    int index = 0;
                    object[] Value = new object[dt.Columns.Count];
                    foreach (DataColumn col in dt.Columns)
                    {
                        Value[index] = col.ColumnName;
                        index++;
                    }
                    FieldName = Value;
                    switch (btn)
                    {
                        case 0: gridView1.FocusedRowHandle = rowhandle; break;
                        case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[FormEvent]
        private void CRMIssueMemberPurp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        private void CRMIssueMemberPurp_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void CRMIssueMemberPurp_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion 
    }
}