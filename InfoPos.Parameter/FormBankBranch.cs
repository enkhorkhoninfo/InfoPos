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
    public partial class FormBankBranch :ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;
        FunctionParam fp = new FunctionParam();

        int SelectTxnCode = 140096;
        int AddTxnCode = 140098;
        int EditTxnCode = 140099;
        int DeleteTxnCode = 140100;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormBankBranch(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            this.Resource = _core.Resource;
            Init();
            InitCombo();   
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init Function]
        private void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormBankBranch_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormBankBranch_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormBankBranch_EventSave);
                this.EventEdit += new delegateEventEdit(FormBankBranch_EventEdit);
                this.EventDelete += new delegateEventDelete(FormBankBranch_EventDelete);

                this.FieldLinkAdd("cboBankID", "BankID", "", true, true);
                this.FieldLinkAdd("txtBranchID", "BranchID", "", true, false);
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
            fp.SetCombo(_core, "BANK", "BANKID", "NAME", SelectTxnCode, cboBankID, "", null);
        }
        #endregion
        #region[Event]

        void FormBankBranch_EventSave(bool isnew, ref bool cancel)
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
                Result r;
                try
                {
                    object[] NewValue = { cboBankID.EditValue, txtBranchID.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
                    if (!isnew)
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, EditTxnCode, EditTxnCode, new object[] { NewValue, OldValue, FieldName });
                        if (r.ResultNo == 0)
                        {
                            MessageBox.Show("Амжилттай засварлалаа.");
                        }
                    }
                    else
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, AddTxnCode, AddTxnCode, new object[] { NewValue,FieldName});
                        if (r.ResultNo == 0)
                        {
                            MessageBox.Show("Амжилттай нэмлээ .");
                        }
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
        void FormBankBranch_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, DeleteTxnCode, DeleteTxnCode, new object[] { cboBankID.EditValue, txtBranchID.EditValue });
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
        void FormBankBranch_EventEdit(ref bool cancel)
        {
            object[] Value = { cboBankID.EditValue, txtBranchID.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }
        void FormBankBranch_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Банкны дугаар");
            this.FieldLinkSetColumnCaption(1, "Салбарын дугаар");
            this.FieldLinkSetColumnCaption(2, "Салбарын нэр");
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
        void FormBankBranch_EventRefresh(ref DataTable dt)
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
        private void FormBankBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormBankBranch_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormBankBranch_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion
    }
}