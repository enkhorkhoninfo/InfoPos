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

namespace InfoPos.Parameter
{
    public partial class FormRebateFormula : ISM.Template.frmTempProp
    {
        #region[Variable]
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        #endregion[]
        #region[Construction]
        public FormRebateFormula(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion[]
        #region[Init]
        private void Init()
        {
            this.EventRefresh += new delegateEventRefresh(FormRebateFormula_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormRebateFormula_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormRebateFormula_EventSave);
            this.EventEdit += new delegateEventEdit(FormRebateFormula_EventEdit);
            this.EventDelete += new delegateEventDelete(FormRebateFormula_EventDelete);

            this.FieldLinkAdd("numFormulaID", "FormulaID", "",true, true);
            this.FieldLinkAdd("cboStatus", "Status", "", false, false);
            this.FieldLinkAdd("dtBeginDate", "BeginDate", "", false, false);
            this.FieldLinkAdd("dtEndDate", "EndDate", "", false, false);
            this.FieldLinkAdd("mmSQLFunction", "SQLFunction", "", false, false);


            FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Идэвхтэй");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 9, "Идэвхгүй");


        }        
        #endregion[]
        #region[Event]
        void FormRebateFormula_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140340, 140340, new object[] { numFormulaID.EditValue });
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
        void FormRebateFormula_EventEdit(ref bool cancel)
        {
            try
            {
                object[] Value = { numFormulaID.EditValue, cboStatus.EditValue, dtBeginDate.EditValue, dtEndDate.EditValue, mmSQLFunction.EditValue };
                OldValue = Value;

                cboStatus.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormRebateFormula_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            string msj = "";
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


                object[] NewValue = { numFormulaID.EditValue, cboStatus.EditValue, dtBeginDate.EditValue, dtEndDate.EditValue, mmSQLFunction.EditValue };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140349, 140349, new object[] { NewValue });
                    msj = "Амжилттай засварлалаа.";                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140348, 140348, new object[] { NewValue });
                    msj = "Амжилттай нэмлээ .";
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    cancel = true;
                }
                else if (r.ResultNo == 0) 
                {
                    MessageBox.Show(msj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormRebateFormula_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "ID дугаар");
            this.FieldLinkSetColumnCaption(1, "Төлөв");
            this.FieldLinkSetColumnCaption(2, "Идэвхтэй эхлэх хугацаа");
            this.FieldLinkSetColumnCaption(3, "Идэвхтэй дуусах хугацаа");
            this.FieldLinkSetColumnCaption(4, "SQL функцийн нэр");
            appname = _core.ApplicationName;
            //formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, "Parameter." + this.Name, ref gridView1);
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }
        void FormRebateFormula_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140346, 140346, null);
                if (r.ResultNo != 0)
                {
                    if (r.Data == null)
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
        #endregion[]
    }
}