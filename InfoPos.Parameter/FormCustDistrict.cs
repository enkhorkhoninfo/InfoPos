using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;
using DevExpress.XtraEditors;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class FormCustDistrict : ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;
        FunctionParam fp = new FunctionParam();

        int SelectTxnCode = 140061;
        int AddTxnCode = 140063;
        int EditTxnCode = 140064;
        int DeleteTxnCode = 140065;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormCustDistrict(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            this.Resource = _core.Resource;
            Init();
            InitCombo();
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init]
        private void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormCustDistrict_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormCustDistrict_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormCustDistrict_EventSave);
                this.EventEdit += new delegateEventEdit(FormCustDistrict_EventEdit);
                this.EventDelete += new delegateEventDelete(FormCustDistrict_EventDelete);

                this.FieldLinkAdd("cboCityCode", "CITYCODE", "", true, true);
                this.FieldLinkAdd("numDistCode", "DISTCODE", "", true, true);
                this.FieldLinkAdd("txtName", "name", "", true, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitCombo() 
        {
           fp.SetCombo(_core,"CUSTCITY", "CITYCODE", "NAME", SelectTxnCode,cboCityCode , "", null);
        }
        #endregion
        #region[Event]
        void FormCustDistrict_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (!FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
                return;
            }

            Result r;
            try
            {
                object[] NewValue = { numDistCode.EditValue, cboCityCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxnCode, EditTxnCode, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxnCode, AddTxnCode, new object[] {NewValue,FieldName});
                    MessageBox.Show("Амжилттай нэмлээ .");
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void FormCustDistrict_EventEdit(ref bool cancel)
        {
            object[] Value = { numDistCode.EditValue, cboCityCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }
        void FormCustDistrict_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Сум дүүрэгийн код");
            this.FieldLinkSetColumnCaption(1, "", true);
            this.FieldLinkSetColumnCaption(2, "Сум дүүрэгийн нэр");
            this.FieldLinkSetColumnCaption(3, "", true);
            this.FieldLinkSetColumnCaption(4, "Жагсаалтын эрэмбэ");
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridView1);
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }
        void FormCustDistrict_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, SelectTxnCode, SelectTxnCode, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
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
        void FormCustDistrict_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { numDistCode.EditValue, cboCityCode.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion
        #region[FormEvent]
        private void FormCustDistrict_Load(object sender, EventArgs e)
        {
            object[] Value = { numDistCode.EditValue, cboCityCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        private void FormCustDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormCustDistrict_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        #endregion
    }
}