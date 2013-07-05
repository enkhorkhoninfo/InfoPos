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
    public partial class FormDictionary : ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;
        int btn=0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormDictionary(InfoPos.Core.Core core)
        {
            try
            {
                InitializeComponent();
                _core = core;
                Init();
                this.Resource = _core.Resource;
                this.FieldLinkSetSaveState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
        #region[Init]
        void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormDictionary_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormDictionary_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormDictionary_EventSave);
                this.EventEdit += new delegateEventEdit(FormDictionary_EventEdit);
                this.EventDelete += new delegateEventDelete(FormDictionary_EventDelete);


                this.FieldLinkAdd("txtID","ID","",true,true);
                this.FieldLinkAdd("txtName", "Name", "", true,false);
                this.FieldLinkAdd("dteLastUpdated", "LastUpdated", "", false, false);
                this.FieldLinkAdd("memoDescription", "Description", "", false, false);
                this.FieldLinkAdd("memoSql", "SQL", "", true, false);
                this.FieldLinkAdd("numRefreshInterval", "REFRESHINTERVAL", "5", true, false);
                this.FieldLinkAdd("txtFieldNames", "FieldNames", "", true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Event]

        void FormDictionary_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(txtID.EditValue), Static.ToStr(txtName.EditValue), Static.ToDate(dteLastUpdated.EditValue), Static.ToStr(memoDescription.EditValue), Static.ToStr(memoSql.EditValue), Static.ToInt(numRefreshInterval.EditValue), Static.ToStr(txtFieldNames.EditValue) };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140304, 140304, new object[] { NewValue, OldValue, FieldName, Static.ToStr(txtID.EditValue) });
                    MessageBox.Show("Амжилттай засварлалаа .");
                    btn = 1;
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140303, 140303, new object[] { NewValue, FieldName, Static.ToStr(txtID.EditValue) });
                    MessageBox.Show("Амжилттай нэмлээ .");
                    btn = 2;
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

        void FormDictionary_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140305, 140305, new object[] { txtID.EditValue });
                    if (r.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        btn = 3;
                    }
                    else 
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void FormDictionary_EventEdit(ref bool cancel)
        {
            object[] Value = { txtID.EditValue, txtName.EditValue, dteLastUpdated.EditValue, memoDescription.EditValue, memoSql.EditValue, numRefreshInterval.EditValue, txtFieldNames.EditValue };
            OldValue = Value;
        }

        void FormDictionary_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0,"Дугаар",false);
            this.FieldLinkSetColumnCaption(1, "Нэр",false);
            this.FieldLinkSetColumnCaption(2, "Сүүлд хийгдсэн өөрчлөлтийн огноо",true);
            this.FieldLinkSetColumnCaption(3, "Тайлбар",true);
            this.FieldLinkSetColumnCaption(4, "SQL", true);
            this.FieldLinkSetColumnCaption(5, "Сэргээх хугацаа", true);
            this.FieldLinkSetColumnCaption(6, "Талбаруудын нэр",false);
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridView1);
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle; break;
                case 2: gridView1.FocusedRowHandle = rowhandle; break;
                case 3: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }

        void FormDictionary_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140301, 140301, null);
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
                        case 1: gridView1.FocusedRowHandle = rowhandle; break;
                        case 2: gridView1.FocusedRowHandle = rowhandle; break;
                        case 3: gridView1.FocusedRowHandle = rowhandle - 1; break;
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
        private void FormDictionary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormDictionary_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormDictionary_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion

   }
}