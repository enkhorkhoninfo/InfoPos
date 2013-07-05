using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class FormAge : ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;

        int SelectTxnCode = 140371;
        int AddTxnCode = 140373;
        int EditTxnCode = 140374;
        int DeleteTxnCode = 140375;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormAge(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            this.Resource = _core.Resource;
            Init();
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init Function]
        public void Init() 
        {
            this.EventRefresh += new delegateEventRefresh(FormAge_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormAge_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormAge_EventSave);
            this.EventEdit += new delegateEventEdit(FormAge_EventEdit);
            this.EventDelete += new delegateEventDelete(FormAge_EventDelete);

            this.FieldLinkAdd("numAgeCode", "Agecode", "", true, true);
            this.FieldLinkAdd("txtName", "name", "", true, false);
            this.FieldLinkAdd("txtName2", "name2", "", false, false);
            this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
        }
        #endregion
        #region[Event]
        void FormAge_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = {numAgeCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
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
        void FormAge_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { numAgeCode.EditValue });
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
        void FormAge_EventEdit(ref bool cancel)
        {
            object[] Value = { numAgeCode.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }
        void FormAge_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Насны код");
            this.FieldLinkSetColumnCaption(1, "Насны нэр");
            this.FieldLinkSetColumnCaption(2, "Насны 2-р нэр", true);
            this.FieldLinkSetColumnCaption(3, "Жагсаалтын эрэмбэ");
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
        void FormAge_EventRefresh(ref DataTable dt)
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
        private void FormAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormAge_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormAge_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion
      }
}