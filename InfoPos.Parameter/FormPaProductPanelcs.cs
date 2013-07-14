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
    public partial class FormPaProductPanelcs : ISM.Template.frmTempProp
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
        public FormPaProductPanelcs(Core.Core core)
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

            this.FieldLinkAdd("txtParentCode", "ParentCode", "", true, true);
            this.FieldLinkAdd("cboNodeCode", "NodeCode", "", true, true);
            this.FieldLinkAdd("txtName", "Name", "", false, false);
            this.FieldLinkAdd("txtName2", "Name2", "", false, false);
            this.FieldLinkAdd("numRowIndex", "RowIndex", "", true, false);
            this.FieldLinkAdd("numColIndex", "ColIndex", "", true, false);
            this.FieldLinkAdd("cboNodeType", "NODETYPE", "", false, false);
            this.FieldLinkAdd("cboNodeID", "NODEID", "", false, false);
        }
        private void Initcombo()
        {
            FormUtility.LookUpEdit_SetList(ref cboNodeType, 1, "Багц");
            FormUtility.LookUpEdit_SetList(ref cboNodeType, 2, "Бараа");
            FormUtility.LookUpEdit_SetList(ref cboNodeType, 3, "Үйлчилгээ");            
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
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140300, 140300, new object[] { txtParentCode.EditValue, cboNodeCode.EditValue });
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
                object[] Value = {   txtParentCode.EditValue, cboNodeCode.EditValue, txtName.EditValue, txtName2.EditValue, numRowIndex.EditValue, 
                                     numColIndex.EditValue, cboNodeType.EditValue, cboNodeID.EditValue };
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


                object[] NewValue = { txtParentCode.EditValue, cboNodeCode.EditValue, txtName.EditValue, txtName2.EditValue, numRowIndex.EditValue, numColIndex.EditValue, cboNodeType.EditValue, cboNodeID.EditValue };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140299, 140299, new object[] { NewValue, txtParentCode.EditValue, cboNodeCode.EditValue });
                    msg ="Амжилттай засварлалаа.";
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140298, 140298, new object[] { NewValue });
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
            this.FieldLinkSetColumnCaption(0, "Эх багцын код");
            this.FieldLinkSetColumnCaption(1, "Тухайн багцын код");
            this.FieldLinkSetColumnCaption(2, "Багцын нэр");
            this.FieldLinkSetColumnCaption(3, "Багцын нэр 2");
            this.FieldLinkSetColumnCaption(4, "Тухайн мөрийн дугаар");

            this.FieldLinkSetColumnCaption(5, "Тухайн баганын дугаар");
            this.FieldLinkSetColumnCaption(6, "Багцын төрөл");
            this.FieldLinkSetColumnCaption(7, "Багцын ID");

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
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140296, 140296, null);
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

        private void cboNodeType_EditValueChanged(object sender, EventArgs e)
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";
            DictUtility.PrivNo = 140131;
            string[] name = new string[] { "PACKMAIN", "INVMAIN", "SERVMAIN" };
            res = DictUtility.Get(_core.RemoteObject, name, ref Tables);
            if (Static.ToInt(cboNodeType.EditValue) != 1 && Static.ToInt(cboNodeType.EditValue) != 2 && Static.ToInt(cboNodeType.EditValue) != 3)
            {
                cboNodeID.Properties.ReadOnly = true;
            }

            if (Static.ToInt(cboNodeType.EditValue) == 1) 
            {
                cboNodeID.Properties.ReadOnly = false;
                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д PACKMAIN оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboNodeID, DT, "PACKID", "NAME", "", new int[] { 2, 3, 4 });
                }
            }

            else if (Static.ToInt(cboNodeType.EditValue) == 2)
            {
                cboNodeID.Properties.ReadOnly = false;
                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д INVMAIN оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboNodeID, DT, "INVID", "NAME", "", new int[] { 1, 3, 4, 5, 6, 7 });
                }
            }

            else if (Static.ToInt(cboNodeType.EditValue) == 3)
            {
                cboNodeID.Properties.ReadOnly = false;

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д SERVMAIN оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboNodeID, DT, "SERVID", "NAME", "", new int[] { 1, 3, 4, 5 });                    
                }
            }
        }

        private void FormPaProductPanelcs_Load(object sender, EventArgs e)
        {
            if(txtParentCode.EditValue != null)
            {
            cboNodeType.ItemIndex = 0;
                cboNodeID.ItemIndex = 0;
            }
        }
    }
}