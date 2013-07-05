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
    public partial class FormBank : ISM.Template.frmTempProp
    {
        #region[Хувьсагч]

        object[] OldValue;
        object[] FieldName;

        int SelectTxnCode = 140021;
        int AddTxnCode = 140023;
        int EditTxnCode = 140024;
        int DeleteTxnCode = 140025;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormBank(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init Function]
        private void Init() 
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormBank_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormBank_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormBank_EventSave);
                this.EventEdit += new delegateEventEdit(FormBank_EventEdit);
                this.EventDelete += new delegateEventDelete(FormBank_EventDelete);

                this.FieldLinkAdd("txtBankID", "bankid", "", true, true);
                this.FieldLinkAdd("txtName", "name", "", true, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Event]

        void FormBank_EventSave(bool isnew, ref bool cancel)
        {
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
                object[] NewValue = { txtBankID.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
                Result r;
                try
                {
                    if (!isnew)
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, EditTxnCode, EditTxnCode, new object[] { NewValue, OldValue, FieldName });
                        MessageBox.Show("Амжилттай засварлалаа.");
                    }
                    else
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, AddTxnCode, AddTxnCode, new object[] {NewValue,FieldName});
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
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void FormBank_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, DeleteTxnCode, DeleteTxnCode, new object[] { txtBankID.EditValue });
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
        void FormBank_EventEdit(ref bool cancel)
        {
            object[] Value = { txtBankID.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }
        void FormBank_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Банкны дугаар");
            this.FieldLinkSetColumnCaption(1, "Банкны нэр");
            this.FieldLinkSetColumnCaption(2, "Банкны 2-р нэр", true);
            this.FieldLinkSetColumnCaption(3, "Жагсаалтын эрэмбэ");
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
        void FormBank_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, SelectTxnCode, SelectTxnCode, null);
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
        private void FormBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormBank_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormBank_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);

        }
        #endregion
    }
}