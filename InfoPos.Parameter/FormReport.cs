using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class FormReport : ISM.Template.frmTempProp
    {
        #region[Variable]
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        DataSet DS;            
        #endregion[]
        #region[Construction]
        public FormReport(Core.Core core)
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
            this.EventRefresh += new delegateEventRefresh(FormReport_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormReport_EventRefreshAfter);
            this.EventSave +=new delegateEventSave(FormReport_EventSave);
            this.EventEdit += new delegateEventEdit(FormReport_EventEdit);
            this.EventDelete += new delegateEventDelete(FormReport_EventDelete);
            this.FieldLinkAdd("txtViewName", "ViewName", "", true, true);
            this.FieldLinkAdd("numParamID", "ParamID", "", true, true);
            this.FieldLinkAdd("cboParamType", "ParamType", "", false, false);
            this.FieldLinkAdd("txtFieldName", "FieldName", "", false, false);
            this.FieldLinkAdd("cboDicValueField", "DicValueField", "", false, false);

            this.FieldLinkAdd("cboDicNameField", "DicNameField", "", false, false);
            this.FieldLinkAdd("cboDicID", "DicID", "", false, false);
            this.FieldLinkAdd("txtDefaultValue", "DefaultValue", "", false, false);
            this.FieldLinkAdd("cboCondition", "Condition", "", false, false); 

            this.FieldLinkAdd("mmParamDesc", "ParamDesc", "", false, false);
            this.FieldLinkAdd("mmParamName", "ParamName", "", false, false);


//////////////////////            = Тэнцүү
//////////////////////            > Их
//////////////////////            >= Их буюу тэнцүү
//////////////////////            < Бага
//////////////////////            <= Бага буюу тэнцүү
//////////////////////            like Ойролцоогоор


            FormUtility.LookUpEdit_SetList(ref cboCondition, 0, "= Тэнцүү");
            FormUtility.LookUpEdit_SetList(ref cboCondition, 1, "> Их");
            FormUtility.LookUpEdit_SetList(ref cboCondition, 2, ">= Их буюу тэнцүү");

            FormUtility.LookUpEdit_SetList(ref cboCondition, 3, "< Бага");
            FormUtility.LookUpEdit_SetList(ref cboCondition, 4, "<= Бага буюу тэнцүү");
            FormUtility.LookUpEdit_SetList(ref cboCondition, 5, "like Ойролцоогоор");


            FormUtility.LookUpEdit_SetList(ref cboParamType, 0, "Тоо");
            FormUtility.LookUpEdit_SetList(ref cboParamType, 1, "Текст");
            FormUtility.LookUpEdit_SetList(ref cboParamType, 2, "Огноо");
            FormUtility.LookUpEdit_SetList(ref cboParamType, 3, "Цаг");
            FormUtility.LookUpEdit_SetList(ref cboParamType, 4, "Лист");


            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";

            DictUtility.PrivNo = 110100;
            string[] name = new string[] { "DICTIONARY" };

            res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

            DT = (DataTable)Tables[0];
            if (DT == null)
            {
                msg = "Dictionary-д DICTIONARY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboDicID, DT, "ID", "NAME", "", new int[] { 2, 3, 4 });                
            }

            //DataSet DSTmp = new DataSet();
            //if (msg != "" && DT == null)
            //    MessageBox.Show(msg);
            //for (int i = 0; i < Tables.Count; i++)
            //{
            //    DataTable DTmp = ((DataTable)Tables[i]).Copy();
            //    DTmp.TableName = name[i];
            //    DSTmp.Tables.Add(DTmp);
            //}

            //DS = DSTmp;

            //SetCombo("ID", "NAME", cboDicNameField, DS.Tables["DICTIONARY"], new int[] { 2, 3 });

            //string NameField = "";

            //if (cboDicID.EditValue != null && cboDicID.EditValue != DBNull.Value && cboDicID.EditValue != "")
            //    NameField = "NAME=" + cboDicID.EditValue;

            //if (DS.Tables["DICTIONARY"] != null)
            //{
            //    SetComboSub("ID", "FIELDNAME", cboDicNameField, DS.Tables["DICTIONARY"], NameField, new int[] { 1 });
            //}
            //else
            //{
            //    MessageBox.Show("Dictionary-д DICTIONARY оруулаагүй байна.");
            //}
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
        #region[Event]
        void FormReport_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140355, 140355, new object[] { txtViewName.EditValue });
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
        void FormReport_EventEdit(ref bool cancel)
        {
            try
            {
                object[] Value = { txtViewName.EditValue, numParamID.EditValue, 
                                   cboParamType.EditValue, txtFieldName.EditValue, 
                                   mmParamName.EditValue,mmParamDesc.EditValue,
                                   txtDefaultValue.EditValue, cboCondition.EditValue,
                                   cboDicID.EditValue, cboDicNameField.EditValue,
                                   cboDicValueField.EditValue
                                 };
                OldValue = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormReport_EventSave(bool isnew, ref bool cancel)
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


                object[] NewValue = { txtViewName.EditValue, numParamID.EditValue, 
                                   cboParamType.EditValue, txtFieldName.EditValue, 
                                   mmParamName.EditValue,mmParamDesc.EditValue,
                                   txtDefaultValue.EditValue, cboCondition.EditValue,
                                   cboDicID.EditValue, cboDicNameField.EditValue,
                                   cboDicValueField.EditValue };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140354, 140354, new object[] { NewValue });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140353, 140353, new object[] { NewValue });
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
        void FormReport_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "View болон хүснэгтийн нэр");
            this.FieldLinkSetColumnCaption(1, "Параметрийн дугаар");
            this.FieldLinkSetColumnCaption(2, "Параметрийн төрөл");
            this.FieldLinkSetColumnCaption(3, "Field ийн нэр");
            this.FieldLinkSetColumnCaption(4, "Параметрийн тайлбар");


            this.FieldLinkSetColumnCaption(0, "Параметрийн дэлгэрэнгүй тайлбар");
            this.FieldLinkSetColumnCaption(1, "Field ийн анхны утга");
            this.FieldLinkSetColumnCaption(2, "Нөхцөл");
            this.FieldLinkSetColumnCaption(3, "Жагсаалтын нэр");
            this.FieldLinkSetColumnCaption(4, "Жагсаалтын сонголт хийх талбарын нэр");
            this.FieldLinkSetColumnCaption(4, "Жагсаалтын сонголт хийх талбарын тайлбар");
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
        void FormReport_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140351, 140351, null);
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

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboDicID_EditValueChanged(object sender, EventArgs e)
        {
            string[] values = Static.ToStr(cboDicID.GetColumnValue("FIELDNAMES")).Split(',');
            int index = 0;
            foreach (string fieldnames in values)
            {
                values[index] = fieldnames.Trim();
                index++;
            }
            ISM.Template.FormUtility.ComboEdit_SetList(ref cboDicNameField, values, true);
            ISM.Template.FormUtility.ComboEdit_SetList(ref cboDicValueField, values, true);
        }
        #endregion[]
    }
}
