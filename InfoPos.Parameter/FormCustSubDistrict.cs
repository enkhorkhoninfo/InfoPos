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
    public partial class FormCustSubDistrict : ISM.Template.frmTempProp
    {
        #region
        object[] OldValue;
        object[] FieldName;

        FunctionParam fp = new FunctionParam();
        int SelectTxnCode = 140066;
        int AddTxnCode = 140068;
        int EditTxnCode = 140069;
        int DeleteTxnCode = 140070;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormCustSubDistrict(InfoPos.Core.Core core)
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
                this.EventRefresh += new delegateEventRefresh(FormCustSubDistrict_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormCustSubDistrict_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormCustSubDistrict_EventSave);
                this.EventEdit += new delegateEventEdit(FormCustSubDistrict_EventEdit);
                this.EventDelete += new delegateEventDelete(FormCustSubDistrict_EventDelete);

                this.FieldLinkAdd("cboCityCode", "CityCode", "", true, true);
                this.FieldLinkAdd("cboDistCode", "DistCode", "", true, true);
                this.FieldLinkAdd("numSubDistCode", "SubDistCode", "", true, true);
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
            fp.SetCombo(_core,"CUSTDISTRICT", "DISTCODE", "NAME", SelectTxnCode,cboDistCode, "", new int[]{1});
        }
#endregion
        #region[Event]

        void FormCustSubDistrict_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { numSubDistCode.EditValue, cboDistCode.EditValue,cboCityCode.EditValue });
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

        void FormCustSubDistrict_EventEdit(ref bool cancel)
        {
            object[] Value = { numSubDistCode.EditValue, cboDistCode.EditValue, cboCityCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }

        void FormCustSubDistrict_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { numSubDistCode.EditValue, cboDistCode.EditValue, cboCityCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
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

        void FormCustSubDistrict_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Баг хорооны код");
            this.FieldLinkSetColumnCaption(1, "", true);
            this.FieldLinkSetColumnCaption(2, "", true);
            this.FieldLinkSetColumnCaption(3, "Баг хорооны нэр");
            this.FieldLinkSetColumnCaption(4, "", true);
            this.FieldLinkSetColumnCaption(5, "Жагсаалтын эрэмбэ");
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }

        void FormCustSubDistrict_EventRefresh(ref DataTable dt)
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

        #endregion
        #region[FormEvent]
        private void cboCityCode_EditValueChanged(object sender, EventArgs e)
        {
            if (Static.ToStr(cboCityCode.EditValue) != "")
            {
                fp.SetCombo(_core, "CUSTDISTRICT", "DISTCODE", "NAME", SelectTxnCode, cboDistCode, "citycode=" + cboCityCode.EditValue + "", new int[] { 1 });
            }
            else
            {
                fp.SetCombo(_core, "CUSTDISTRICT", "DISTCODE", "NAME", SelectTxnCode, cboDistCode, "", new int[] { 1 });
            }
        }
        private void FormCustSubDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormCustSubDistrict_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormCustSubDistrict_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }

        #endregion
    }
}