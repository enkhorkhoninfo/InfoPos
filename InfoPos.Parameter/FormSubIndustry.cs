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
    public partial class FormSubIndustry : ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;
        FunctionParam fp = new FunctionParam();

        int SelectTxnCode = 140051;
        int AddTxnCode = 140053;
        int EditTxnCode = 140054;
        int DeleteTxnCode = 140055;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormSubIndustry(InfoPos.Core.Core core)
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
                this.EventRefresh += new delegateEventRefresh(FormSubIndustry_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormSubIndustry_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormSubIndustry_EventSave);
                this.EventEdit += new delegateEventEdit(FormSubIndustry_EventEdit);
                this.EventDelete += new delegateEventDelete(FormSubIndustry_EventDelete);

                this.FieldLinkAdd("cboTypeCode", "TypeCode", "",true,true);
                this.FieldLinkAdd("numSubTypeCode", "SubTypeCode", "", true, true);
                this.FieldLinkAdd("cboClassCode", "ClassCode", "", true, false);
                this.FieldLinkAdd("txtName", "Name", "", true, false);
                this.FieldLinkAdd("txtName2", "Name2", "", false, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void InitCombo()
        {
            fp.SetCombo(_core,"INDUSTRY", "TYPECODE", "NAME", SelectTxnCode,cboTypeCode, "", null);
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboClassCode, 0, "Иргэн");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboClassCode, 1, "Байгууллага");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboClassCode, 2, "Хоёуланд нь");
        }
        #endregion
        #region[Event]
        void FormSubIndustry_EventRefresh(ref DataTable dt)
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
        void FormSubIndustry_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Төрлийн дугаар");
            this.FieldLinkSetColumnCaption(1, "Дэд Төрлийн дугаар");
            this.FieldLinkSetColumnCaption(2, "Класс код");
            this.FieldLinkSetColumnCaption(3, "Төрлийн нэр");
            this.FieldLinkSetColumnCaption(4, "Төрлийн 2-р нэр");
            this.FieldLinkSetColumnCaption(5, "Жагсаалтын эрэмбэ");
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
        void FormSubIndustry_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { cboTypeCode.EditValue, numSubTypeCode.EditValue, cboClassCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
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
        void FormSubIndustry_EventEdit(ref bool cancel)
        {
            object[] Value = { cboTypeCode.EditValue, numSubTypeCode.EditValue, cboClassCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }
        void FormSubIndustry_EventDelete()
        {
            rowhandle = gridView1.FocusedRowHandle;
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { cboTypeCode.EditValue, numSubTypeCode.EditValue});
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
        #endregion
        #region[SetCombo]
        void SetCombo(string ID, string IDFieldName, int PrivNo, DevExpress.XtraEditors.LookUpEdit LEdit, string Filter, int[] Hide)
        {
            Result res = new Result();
            DataTable DT = null;
            string msg = "";

            ISM.Template.DictUtility.PrivNo = PrivNo;

            res = ISM.Template.DictUtility.Get(_core.RemoteObject, ID, ref DT);

            if (res.ResultNo == 0)
            {
                ISM.Template.FormUtility.LookUpEdit_SetList(ref LEdit, DT, IDFieldName, Filter, Hide);
            }
            else
            {
                msg = "Dictionary-д " + ID + " оруулаагүй байна-" + res.ResultDesc;
                MessageBox.Show(msg);
            }
        }
        void SetComboSub(string ID, string IDFieldName, int PrivNo, DevExpress.XtraEditors.LookUpEdit LEdit, string Filter, int[] Hide)
        {
            DataTable DT = null;
            string msg = "";
            Result res = new Result();
            ISM.Template.DictUtility.PrivNo = PrivNo;

            res = ISM.Template.DictUtility.Get(_core.RemoteObject, ID, ref DT);
            if (res.ResultNo == 0)
            {
                ISM.Template.FormUtility.LookUpEdit_SetList(ref LEdit, DT, IDFieldName, Filter, Hide);
            }
            else
                msg = "Dictionary-д " + ID + " оруулаагүй байна-" + res.ResultDesc;
        }
        #endregion
        #region[FormEvent]
        private void FormSubIndustry_Load(object sender, EventArgs e)
        {
            object[] Value = { cboTypeCode.EditValue, numSubTypeCode.EditValue, cboClassCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        private void FormSubIndustry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormSubIndustry_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        #endregion
    }
}