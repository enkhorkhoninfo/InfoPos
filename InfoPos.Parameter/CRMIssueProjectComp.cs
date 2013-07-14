using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;
using InfoPos.Messages;

namespace InfoPos.Parameter
{
    public partial class CRMIssueProjectComp : frmTempProp
    {
        #region [ Хувьсагчууд ]
        object[] OldValue;
        int btn = 0;
        int rowhandle = 0;
        int AddTxn = 310076;
        int SelectTxn = 310074;
        int EditTxn = 310077;
        int DeleteTxn = 310078;
        FunctionParam fp = new FunctionParam();
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public CRMIssueProjectComp(Core.Core core)
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
                this.EventRefresh += new delegateEventRefresh(CRMIssueProjectComp_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(CRMIssueProjectComp_EventRefreshAfter);
                this.EventSave += new delegateEventSave(CRMIssueProjectComp_EventSave);
                this.EventEdit += new delegateEventEdit(CRMIssueProjectComp_EventEdit);
                this.EventDelete += new delegateEventDelete(CRMIssueProjectComp_EventDelete);

                this.FieldLinkAdd("numProjectCompID", "ProjectCompID", "", true, true);
                this.FieldLinkAdd("cboProjectID", "ProjectID", "", true, false);
                this.FieldLinkAdd("txtName", "Name", "", true, false);
                this.FieldLinkAdd("txtName2", "Name2", "", false, false);
                this.FieldLinkAdd("txtShortName", "ShortName", "", true, false);
                this.FieldLinkAdd("txtShortName2", "ShortName2", "", false, false);
                this.FieldLinkAdd("txtDescription", "Description", "", false, false);
                this.FieldLinkAdd("cboOwnerUser", "OwnerUser", "", true, false);
                this.FieldLinkAdd("numOrderNo", "OrderNo", "", true, false);

                this.Resource = _core.Resource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitCombo()
        {
            fp.SetCombo(_core, "ISSUEPROJECT", "PROJECTID", "NAME", SelectTxn, cboProjectID, "", null);
            fp.SetCombo(_core, "USERS", "USERNO", "USERFNAME", SelectTxn, cboOwnerUser, "", null);
        }
        #endregion
        #region [ Events ]
        void CRMIssueProjectComp_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Устгахдаа итгэлтэй байна уу", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxn, DeleteTxn, new object[] { Static.ToInt(numProjectCompID.EditValue),Static.ToLong(cboProjectID.EditValue) });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа.");
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void CRMIssueProjectComp_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToInt(numProjectCompID.EditValue), Static.ToLong(cboProjectID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToStr(txtShortName.EditValue), Static.ToStr(txtShortName2.EditValue),Static.ToStr(txtDescription.EditValue),Static.ToInt(cboOwnerUser.EditValue), Static.ToInt(numOrderNo.EditValue) };
            OldValue = Value;
        }
        void CRMIssueProjectComp_EventSave(bool isnew, ref bool cancel)
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
                    object[] NewValue = { Static.ToInt(numProjectCompID.EditValue), Static.ToLong(cboProjectID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToStr(txtShortName.EditValue), Static.ToStr(txtShortName2.EditValue), Static.ToStr(txtDescription.EditValue), Static.ToInt(cboOwnerUser.EditValue), Static.ToInt(numOrderNo.EditValue) };
                    object[] FieldName={"PROJECTCOMPID","PROJECTID","NAME","NAME2","SHORTNAME","SHORTNAME2","DESCRIPTION","OWNERUSER","ORDERNO"};
                    if (!isnew)
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxn, EditTxn, new object[] { NewValue, OldValue, FieldName });
                        MessageBox.Show("Амжилттай хадгаллаа.");
                    }

                    else
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxn, AddTxn, new object[] { NewValue, FieldName });
                        MessageBox.Show("Амжилттай хадгаллаа.");
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
        void CRMIssueProjectComp_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Дэд төрлийн дугаар");
            this.FieldLinkSetColumnCaption(1, "Төслийн дугаар");
            this.FieldLinkSetColumnCaption(2, "Төслийн нэр");
            this.FieldLinkSetColumnCaption(3, "Нэр");
            this.FieldLinkSetColumnCaption(4, "Нэр 2");
            this.FieldLinkSetColumnCaption(5, "Товч нэр");
            this.FieldLinkSetColumnCaption(6, "Товч нэр 2");
            this.FieldLinkSetColumnCaption(7, "Төслийн дэлгэрэнгүй мэдээлэл");
            this.FieldLinkSetColumnCaption(8, "Төслийн хариуцагчийн дугаар");
            this.FieldLinkSetColumnCaption(9, "Төслийн хариуцагч");
            this.FieldLinkSetColumnCaption(10, "Жагсаалтын эрэмбэ");

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
        void CRMIssueProjectComp_EventRefresh(ref DataTable dt)
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
        private void CRMIssueProjectComp_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void CRMIssueProjectComp_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void CRMIssueProjectComp_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion
    }
}