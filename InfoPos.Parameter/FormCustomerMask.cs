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
    public partial class FormCustomerMask :ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;

        int SelectTxnCode = 140126;
        int AddTxnCode = 140128;
        int EditTxnCode = 140129;
        int DeleteTxnCode=140130;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
#endregion
        #region[Байгуулагч функц]
        public FormCustomerMask(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init]
        private void Init() 
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormCustomerMask_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormCustomerMask_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormCustomerMask_EventSave);
                this.EventEdit += new delegateEventEdit(FormCustomerMask_EventEdit);
                this.EventDelete += new delegateEventDelete(FormCustomerMask_EventDelete);

                this.FieldLinkAdd("numMaskID", "MaskID", "", true, true);
                this.FieldLinkAdd("txtMaskName", "MaskName", "", true, false);
                this.FieldLinkAdd("txtMaskValue", "MaskValue", "", true, false);
                this.FieldLinkAdd("cboMaskType", "MaskType", "", true, false);
                this.FieldLinkAdd("cboCustType", "CustType", "", true, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void InitCombo() 
        {
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboMaskType, 0, "Регистерийн дугаар");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboMaskType, 1, "Пасспортын болон улсын бүртгэлийн дугаар");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboMaskType, 2, "Жолоочийн үнэмлэхний дугаар");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboCustType, 0, "Иргэн");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboCustType, 1, "Байгууллага");
        }
#endregion
        #region[Event]

        void FormCustomerMask_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { numMaskID.EditValue, txtMaskName.EditValue, txtMaskValue.EditValue, cboMaskType.EditValue, cboCustType.EditValue, numOrderNo.EditValue };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxnCode, EditTxnCode , new object[] { NewValue, OldValue, FieldName });
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

        void FormCustomerMask_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { numMaskID.EditValue });
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

        void FormCustomerMask_EventEdit(ref bool cancel)
        {
            object[] Value = { numMaskID.EditValue, txtMaskName.EditValue, txtMaskValue.EditValue, cboMaskType.EditValue,cboCustType.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }

        void FormCustomerMask_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Маскны дугаар");
            this.FieldLinkSetColumnCaption(1, "Маскны нэр");
            this.FieldLinkSetColumnCaption(2, "", true);
            this.FieldLinkSetColumnCaption(3, "", true);
            this.FieldLinkSetColumnCaption(4, "", true);
            this.FieldLinkSetColumnCaption(5, "Жагсаалтын эрэмбэ");
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }

        void FormCustomerMask_EventRefresh(ref DataTable dt)
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
        #region[FormEvent]
        private void FormCustomerMask_Load(object sender, EventArgs e)
        {
            object[] Value = { numMaskID.EditValue, txtMaskName.EditValue, txtMaskValue.EditValue, cboMaskType.EditValue,cboCustType.EditValue, numOrderNo.EditValue };
            OldValue = Value;

            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        private void FormCustomerMask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormCustomerMask_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        #endregion
    }
}