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
    public partial class CRMIssueTrack : frmTempProp
    {
        #region [ Хувьсагчууд ]
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        int rowhandle = 0;

        int SelectTxn = 310100;
        int AddTxn = 310012;
        int EditTxn = 310013;
        int DeleteTxn = 310014;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public CRMIssueTrack(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(CRMIssueTrack_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(CRMIssueTrack_EventRefreshAfter);
                this.EventSave += new delegateEventSave(CRMIssueTrack_EventSave);
                this.EventEdit += new delegateEventEdit(CRMIssueTrack_EventEdit);
                this.EventDelete += new delegateEventDelete(CRMIssueTrack_EventDelete);

                this.FieldLinkAdd("numIssueTrackID", "ISSUETRACKID", "", true, true);
                this.FieldLinkAdd("txtName", "Name", "", true, false);
                this.FieldLinkAdd("txtName2", "Name2", "", false, false);
                this.FieldLinkAdd("numOrderNo", "OrderNo", "", true, false);

                this.Resource = _core.Resource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region [ Events ]
        void CRMIssueTrack_EventDelete()
        {
            DialogResult DR = MessageBox.Show(MSG.Messages(_core.Lang, 10007), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    if (Static.ToInt(numIssueTrackID.EditValue) > 4)
                    {
                        Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxn, DeleteTxn, new object[] { Static.ToInt(numIssueTrackID.EditValue) });
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
                    else
                    {
                        MessageBox.Show("Энэхүү утга нь тогтмол утга учир устгах боломжгүй байна. ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void CRMIssueTrack_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToInt(numIssueTrackID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue) };
            OldValue = Value;
        }
        void CRMIssueTrack_EventSave(bool isnew, ref bool cancel)
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
                    object[] NewValue = { Static.ToInt(numIssueTrackID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue)};
                    if (!isnew)
                    {
                        if (Static.ToInt(numIssueTrackID.EditValue) > 4)
                        {

                            r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxn, EditTxn, new object[] { NewValue, OldValue, FieldName });
                            if (r.ResultNo == 0)
                            {
                                MessageBox.Show(MSG.Messages(_core.Lang, 10001));
                            }
                            else
                            {
                                MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Энэхүү утга нь тогтмол утга учир засах боломжгүй байна.", "Мэдээлэл");
                        }
                    }

                    else
                    {
                        if (Static.ToInt(numIssueTrackID.EditValue) > 4)
                        {
                            r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxn, AddTxn, new object[] { NewValue, FieldName });
                            if (r.ResultNo == 0)
                            {
                                MessageBox.Show(MSG.Messages(_core.Lang, 10000));
                            }
                            else
                            {
                                MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                            }
                        }
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
        void CRMIssueTrack_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Алхамын дугаар");
            this.FieldLinkSetColumnCaption(1, "Нэр");
            this.FieldLinkSetColumnCaption(2, "Нэр 2");
            this.FieldLinkSetColumnCaption(3, "Жагсаалтын эрэмбэ");

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
        void CRMIssueTrack_EventRefresh(ref DataTable dt)
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
        private void CRMIssueTrack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        private void CRMIssueTrack_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void CRMIssueTrack_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion
    }
}