using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;

using InfoPos.Core;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class FormPaProductTreeDesc : ISM.Template.frmTempProp
    {
        #region[Variables]
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        DataSet DSAgent = null;
        #endregion[]
        #region[Constuction]
        public FormPaProductTreeDesc(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            Initcombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion[]
        #region[Init]
        private void Init()
        {
            FunctionParam fp = new FunctionParam();     

            this.EventRefresh += new delegateEventRefresh(FormPaProductPanelcs_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPaProductPanelcs_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPaProductPanelcs_EventSave);
            this.EventEdit += new delegateEventEdit(FormPaProductPanelcs_EventEdit);
            this.EventDelete += new delegateEventDelete(FormPaProductPanelcs_EventDelete);

            this.FieldLinkAdd("txtItemId", "ItemId", "", true, true);
            this.FieldLinkAdd("txtName", "Name", "", true, false);
            this.FieldLinkAdd("txtName2", "Name2", "", false, false);
        }
        private void Initcombo()
        {
                    
        }
        void SetComboSub(string ValueField, string NameField, DevExpress.XtraEditors.LookUpEdit LEdit, DataTable DT, string Filter, int[] Hide)
        {
            try
            {
                string msg = "";
                if (DT == null)
                {
                    msg = "Dictionary-д утга оруулна ууаагүй байна. ";
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref LEdit, DT, ValueField, NameField, Filter, Hide);
                }
                if (msg != "")
                    MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + Filter);
            }
        }
        void SetCombo(string ValueField, string NameField, DevExpress.XtraEditors.LookUpEdit LEdit, DataTable DT, int[] Hide)
        {
            string msg = "";
            if (DT == null)
            {
                msg = "Dictionary-д утга оруулна уу .";
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref LEdit, DT, ValueField, NameField, "", Hide);
            }
            if (msg != "")
                MessageBox.Show(msg);
        }
        #endregion[]
        #region[Events]
        void FormPaProductPanelcs_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140365, 140365, new object[] { txtItemId.EditValue });
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
        void FormPaProductPanelcs_EventEdit(ref bool cancel)
        {
            try
            {
                object[] Value = {   txtItemId.EditValue, txtName.EditValue, txtName2.EditValue };
                OldValue = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormPaProductPanelcs_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            string msg = "";
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


                object[] NewValue = { txtItemId.EditValue, txtName.EditValue, txtName2.EditValue};
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140364, 140364, new object[] { NewValue });
                    msg ="Амжилттай засварлалаа.";
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140363, 140363, new object[] { NewValue });
                   msg = "Амжилттай нэмлээ .";
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    cancel = true;
                }
                else 
                {
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formName, ref gridView1);
        }
        void FormPaProductPanelcs_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Бүлгийн код");
            this.FieldLinkSetColumnCaption(1, "Бүлгийн нэр");
            this.FieldLinkSetColumnCaption(2, "Бүлгийн нэр");

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
        void FormPaProductPanelcs_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140361, 140361, null);
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