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
    public partial class FormFAType :ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;
        FunctionParam fp = new FunctionParam();
        int SelectTxnCode = 140136;
        int AddTxnCode = 140138;
        int EditTxnCode = 140139;
        int DeleteTxnCode=140140;
        int btn=0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormFAType(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init функц]
        private void Init() 
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormFAType_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormFAType_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormFAType_EventSave);
                this.EventEdit += new delegateEventEdit(FormFAType_EventEdit);
                this.EventDelete += new delegateEventDelete(FormFAType_EventDelete);

                this.FieldLinkAdd("numFATypeID", "FATypeID", "", true, false);
                this.FieldLinkAdd("txtName", "name", "", true, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
                this.FieldLinkAdd("cboCurrency", "Currency", "", true, false);
                this.FieldLinkAdd("numAccountNo", "AccountNo", "", true, false,true);
                this.FieldLinkAdd("numDefAccountNo", "DefAccountNo", "", true, false, true);
                this.FieldLinkAdd("numDefExpAccountNo", "DefExpAccountNo", "", true, false, true);
                this.FieldLinkAdd("numProfitAccountNo", "ProfitAccountNo", "", true, false, true);
                this.FieldLinkAdd("numLossAccountNo", "LossAccountNo", "", true, false, true);
                this.FieldLinkAdd("numWearYear", "WearYear", "", true, false);
                this.FieldLinkAdd("txtDepFormula", "DepFormula", "", false, false);
                this.FieldLinkAdd("txtDays", "Days", "", false, false);
                this.FieldLinkAdd("cboHalfMonthCalc", "HalfMonthCalc", "", true, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void InitCombo()
        {
            fp.SetCombo(_core,"CURRENCY", "CURRENCY", "NAME", SelectTxnCode, cboCurrency, "", null);
            fp.SetCombo(_core, "FORMULA", "FORMULAID", "FORMULA", SelectTxnCode, cboHalfMonthCalc, "", null);

            FormUtility.LookUpEdit_SetList(ref cboHalfMonthCalc, "0", "Тооцохгүй");
            FormUtility.LookUpEdit_SetList(ref cboHalfMonthCalc, "1", "Тооцно");
        }
        #endregion
        #region[Event]
        void FormFAType_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = {  Static.ToLong(numFATypeID.EditValue), 
                                    Static.ToStr(txtName.EditValue), 
                                    Static.ToStr(txtName2.EditValue), 
                                    Static.ToStr(cboCurrency.EditValue), 
                                    Static.ToLong(numAccountNo.EditValue), 
                                    Static.ToLong(numDefAccountNo.EditValue), 
                                    Static.ToLong(numDefExpAccountNo.EditValue), 
                                    Static.ToLong(numLossAccountNo.EditValue),
                                    Static.ToInt(numWearYear.EditValue),
                                    Static.ToStr(txtDepFormula.EditValue),
                                    Static.ToInt(numOrderNo.EditValue),
                                    Static.ToInt(txtDays.EditValue),
                                    Static.ToInt(cboHalfMonthCalc.EditValue),
                                    Static.ToLong(numProfitAccountNo.EditValue)};
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxnCode, EditTxnCode , new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxnCode, AddTxnCode,new object[] {NewValue,FieldName});
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
        void FormFAType_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { Static.ToLong(numFATypeID.EditValue) });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
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
        void FormFAType_EventEdit(ref bool cancel)
        {
            object[] Value = {  Static.ToLong(numFATypeID.EditValue), 
                                    Static.ToStr(txtName.EditValue), 
                                    Static.ToStr(txtName2.EditValue), 
                                    Static.ToStr(cboCurrency.EditValue), 
                                    Static.ToLong(numAccountNo.EditValue), 
                                    Static.ToLong(numDefAccountNo.EditValue), 
                                    Static.ToLong(numDefExpAccountNo.EditValue), 
                                    Static.ToLong(numLossAccountNo.EditValue),
                                    Static.ToInt(numWearYear.EditValue),
                                    Static.ToStr(txtDepFormula.EditValue),
                                    Static.ToInt(numOrderNo.EditValue),
                                    Static.ToInt(txtDays.EditValue),
                                    Static.ToInt(cboHalfMonthCalc.EditValue),
                                    Static.ToLong(numProfitAccountNo.EditValue),};
            OldValue = Value;
        }
        void FormFAType_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Төрлийн дугаар");
            this.FieldLinkSetColumnCaption(1, "Төрлийн нэр");
            this.FieldLinkSetColumnCaption(2, "", true);
            this.FieldLinkSetColumnCaption(3, "", true);
            this.FieldLinkSetColumnCaption(4, "", true);
            this.FieldLinkSetColumnCaption(5, "", true);
            this.FieldLinkSetColumnCaption(6, "", true);
            this.FieldLinkSetColumnCaption(7, "", true);
            this.FieldLinkSetColumnCaption(8, "", true);
            this.FieldLinkSetColumnCaption(9, "", true);
            this.FieldLinkSetColumnCaption(10, "", true);
            this.FieldLinkSetColumnCaption(11, "Жагсаалтын эрэмбэ");
            this.FieldLinkSetColumnCaption(12, "", true);
            this.FieldLinkSetColumnCaption(13, "",true);
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
        void FormFAType_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, SelectTxnCode, SelectTxnCode, null);
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
        private void FormFAType_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        private void FormFAType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormFAType_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        #endregion
    }
}