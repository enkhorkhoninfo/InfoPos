using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ISM.Template;
using EServ.Shared;
using InfoPos.Core;

namespace InfoPos.Parameter
{
    public partial class FormCustomerType : ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;

        int SelectTxnCode = 140036;
        int AddTxnCode = 140038;
        int EditTxnCode = 140039;
        int DeleteTxnCode = 140040;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
#endregion
        #region[Байгуулагч функц]
        public FormCustomerType(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            this.Resource = _core.Resource;
            Init();
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init]
        private void Init() 
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormAccountCode_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormAccountCode_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormAccountCode_EventSave);
                this.EventEdit += new delegateEventEdit(FormAccountCode_EventEdit);
                this.EventDelete += new delegateEventDelete(FormAccountCode_EventDelete);

                this.FieldLinkAdd("numTypeCode", "typecode", "", true, true);
                this.FieldLinkAdd("numClassCode", "classcode", "", true, false);
                this.FieldLinkAdd("txtName", "name", "", true, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("txtAccountNo", "AccountNo", "", false, false);
                this.FieldLinkAdd("txtIncomeAccountNo", "IncomeAccountNo", "", false, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion
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
                return;
            }
            Result r;
            try
            {
                object[] NewValue = { numTypeCode.EditValue, numClassCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue, txtAccountNo.EditValue, txtIncomeAccountNo.EditValue };
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
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void FormAccountCode_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, DeleteTxnCode, DeleteTxnCode, new object[] { numTypeCode.EditValue });
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
            object[] Value = { numTypeCode.EditValue, numClassCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue, txtAccountNo.EditValue, txtIncomeAccountNo.EditValue };
            OldValue = Value;
        }     
        void FormAccountCode_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Төрлийн дугаар");
            this.FieldLinkSetColumnCaption(1, "Класс код",true);
            this.FieldLinkSetColumnCaption(2, "Төрлийн нэр");
            this.FieldLinkSetColumnCaption(3, "Төрлийн 2-р нэр", true);
            this.FieldLinkSetColumnCaption(4, "Авлагын данс", true);
            this.FieldLinkSetColumnCaption(5, "Жагсаалтын эрэмбэ");
            this.FieldLinkSetColumnCaption(6, "Орлогын данс", true);
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
        #region[FormEvnet]
        private void FormCustomerType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormCustomerType_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormCustomerType_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);

            FormUtility.LookUpEdit_SetList(ref numClassCode, 0, "ИРГЭН");
            FormUtility.LookUpEdit_SetList(ref numClassCode, 1, "БАЙГУУЛЛАГА");
            FormUtility.LookUpEdit_SetList(ref numClassCode, 2, "ИРГЭН / БАЙГУУЛЛАГА");

        }
        #endregion
       
    }
}