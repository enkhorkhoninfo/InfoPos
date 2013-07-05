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
    public partial class FormTxnEntry :ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;

        int SelectTxnCode = 140166;
        int AddTxnCode = 140168;
        int EditTxnCode = 140169;
        int DeleteTxnCode = 140170;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormTxnEntry(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init]
        void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormTxnEntry_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormTxnEntry_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormTxnEntry_EventSave);
                this.EventEdit += new delegateEventEdit(FormTxnEntry_EventEdit);
                this.EventDelete += new delegateEventDelete(FormTxnEntry_EventDelete);

                this.FieldLinkAdd("numEntryCode", "EntryCode", "", true, false, false);
                this.FieldLinkAdd("numEntryTxnCode", "EntryTxnCode", "", true, false, false);
                this.FieldLinkAdd("txtDRAcntMod", "DRAcntMod", "", true, false);
                this.FieldLinkAdd("txtDRAcntNo", "DRAcntNo", "", true, false);
                this.FieldLinkAdd("txtDRCurrCode", "DRCurrCode", "", true, false);
                this.FieldLinkAdd("txtDRRate", "DRRate", "", true, false);
                this.FieldLinkAdd("txtDRAmount", "DRAmount", "", true, false);
                this.FieldLinkAdd("txtCRAcntMod", "CRAcntMod", "", true, false);
                this.FieldLinkAdd("txtCRAcntno", "CRAcntno", "", true, false);
                this.FieldLinkAdd("txtCRCurrCode", "CRCurrCode", "", true, false);
                this.FieldLinkAdd("txtCRAmount", "CRAmount", "", true, false);
                this.FieldLinkAdd("txtCRRate", "CRRate", "", true, false);
                this.FieldLinkAdd("txtDescription", "Description", "", true, false);
                this.FieldLinkAdd("mmoCondition", "Condition", "", true, false);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[ Event ]
        void FormTxnEntry_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { numEntryCode.EditValue, numEntryTxnCode.EditValue, txtDRAcntMod.EditValue, txtDRAcntNo.EditValue, txtDRCurrCode.EditValue, txtDRRate.EditValue, txtDRAmount.EditValue, txtCRAcntMod.EditValue, txtCRAcntno.EditValue, txtCRCurrCode.EditValue, txtCRAmount.EditValue, txtCRRate.EditValue, txtDescription.EditValue, mmoCondition.EditValue };
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
        void FormTxnEntry_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { numEntryCode.EditValue });
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
        void FormTxnEntry_EventEdit(ref bool cancel)
        {
            object[] Value = { numEntryCode.EditValue, numEntryTxnCode.EditValue, txtDRAcntMod.EditValue, txtDRAcntNo.EditValue, txtDRCurrCode.EditValue, txtDRRate.EditValue, txtDRAmount.EditValue, txtCRAcntMod.EditValue, txtCRAcntno.EditValue, txtCRCurrCode.EditValue, txtCRAmount.EditValue, txtCRRate.EditValue, txtDescription.EditValue, mmoCondition.EditValue };
            OldValue = Value;
        }
        void FormTxnEntry_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Гүйлгээний код");
            this.FieldLinkSetColumnCaption(1, "Гүйлгээний нэр");
            this.FieldLinkSetColumnCaption(2, "", true);
            this.FieldLinkSetColumnCaption(3, "", true);
            this.FieldLinkSetColumnCaption(4, "", true);
            this.FieldLinkSetColumnCaption(5, "", true);
            this.FieldLinkSetColumnCaption(6, "", true);
            this.FieldLinkSetColumnCaption(7, "", true);
            this.FieldLinkSetColumnCaption(8, "", true);
            this.FieldLinkSetColumnCaption(9, "", true);
            this.FieldLinkSetColumnCaption(10, "", true);
            this.FieldLinkSetColumnCaption(11, "", true);
            this.FieldLinkSetColumnCaption(12, "", true);
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
        void FormTxnEntry_EventRefresh(ref DataTable dt)
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
        #region[BTN]
        private void btnChoose_Click(object sender, EventArgs e)
        {
            //HeavenPro.List.TxnEntry frm = new HeavenPro.List.TxnEntry(_core);
            //frm.ucTxnEntry.Browsable = true;
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    numEntryTxnCode.EditValue = (frm.ucTxnEntry.SelectedRow["TranCode"]).ToString();
            //}
        }
        #endregion
        #region[FormEvent]
        private void FormTxnEntry_Load(object sender, EventArgs e)
        {
            object[] Value = { numEntryCode.EditValue, numEntryTxnCode.EditValue, txtDRAcntMod.EditValue, txtDRAcntNo.EditValue, txtDRCurrCode.EditValue, txtDRRate.EditValue, txtDRAmount.EditValue, txtCRAcntMod.EditValue, txtCRAcntno.EditValue, txtCRCurrCode.EditValue, txtCRAmount.EditValue, txtCRRate.EditValue, txtDescription.EditValue };
            OldValue = Value;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        private void FormTxnEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void FormTxnEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        #endregion
    }
}
