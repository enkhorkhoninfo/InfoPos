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
    public partial class FormAccountCode :ISM.Template.frmTempProp
    {
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        int rowhandle = 0;
        int SelectTxnCode = 140026;
        int AddTxnCode = 140028;
        int EditTxnCode = 140029;
        int DeleteTxnCode = 140030;
        string appname = "", formname = "";
        Form FormName = null;
        FunctionParam fp=new FunctionParam();
        InfoPos.Core.Core _core = null;

        public FormAccountCode(InfoPos.Core.Core core)
        {
            try
            {
                InitializeComponent();
                _core = core;
                Init();
                InitCombo();
                this.Resource = _core.Resource;
                this.FieldLinkSetSaveState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void Init() 
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormAccountCode_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormAccountCode_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormAccountCode_EventSave);
                this.EventEdit += new delegateEventEdit(FormAccountCode_EventEdit);
                this.EventDelete += new delegateEventDelete(FormAccountCode_EventDelete);

                this.FieldLinkAdd("cboBranch", "branch", "", true, false);
                this.FieldLinkAdd("txtCode", "Code", "", true, true);
                this.FieldLinkAdd("cboCurrency", "Currency", "", true, false);
                this.FieldLinkAdd("numAccountNo", "AccountNo", "", true, false,true);
                this.FieldLinkAdd("mmeNote", "Note", "", false, false);
                this.FieldLinkAdd("cboCodeType", "CodeType", "", false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InitCombo() 
        {
           fp.SetCombo(_core,"CURRENCY", "CURRENCY", "NAME", SelectTxnCode, cboCurrency, "", null);
           fp.SetCombo(_core,"BRANCH", "BRANCH", "NAME", SelectTxnCode, cboBranch, "", new int[] { 2, 3, 4 });

           ISM.Template.FormUtility.LookUpEdit_SetList(ref cboCodeType, 0, "Байгууллагын данс");
           ISM.Template.FormUtility.LookUpEdit_SetList(ref cboCodeType, 1, "Балансын гадуурх данс");
        }

        #region[Event]

        void FormAccountCode_EventSave(bool isnew, ref bool cancel)
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
                    object[] NewValue = { txtCode.EditValue, cboCurrency.EditValue, cboBranch.EditValue, numAccountNo.EditValue, mmeNote.EditValue, cboCodeType.EditValue };
                    if (!isnew)
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxnCode, EditTxnCode, new object[] { NewValue, OldValue, FieldName });
                        MessageBox.Show("Амжилттай засварлалаа.");
                    }
                    else
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxnCode, AddTxnCode, new object[] { NewValue,FieldName});
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
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }

        void FormAccountCode_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { txtCode.EditValue });
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

        void FormAccountCode_EventEdit(ref bool cancel)
        {
            object[] Value = { txtCode.EditValue, cboCurrency.EditValue, cboBranch.EditValue, numAccountNo.EditValue, mmeNote.EditValue,cboCodeType.EditValue };
            OldValue = Value;
        }

        void FormAccountCode_EventRefreshAfter()
        {
            this.FieldLinkSetColumnCaption(0, "Дугаар");
            this.FieldLinkSetColumnCaption(1, "",true);
            this.FieldLinkSetColumnCaption(2, "",true);
            this.FieldLinkSetColumnCaption(3, "Дансны дугаар");
            this.FieldLinkSetColumnCaption(4, "",true);
            this.FieldLinkSetColumnCaption(5, "",true);
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

        void FormAccountCode_EventRefresh(ref DataTable dt)
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
       
        private void FormAccountCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FormAccountCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }

        private void FormAccountCode_Load(object sender, EventArgs e)
        {
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
          }

    }
}