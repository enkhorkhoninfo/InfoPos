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
    public partial class FormPaDaytype : ISM.Template.frmTempProp
    {
        #region[Хувьсагчууд]
        InfoPos.Core.Core _core = null;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        #endregion[]
        public FormPaDaytype(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        private void Init()
        {
            this.EventRefresh += new delegateEventRefresh(FormPaDaytype_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPaDaytype_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPaDaytype_EventSave);
            this.EventEdit += new delegateEventEdit(FormPaDaytype_EventEdit);
            this.EventDelete += new delegateEventDelete(FormPaDaytype_EventDelete);

            this.FieldLinkAdd("txtDayType", "DayType", "", true, true);
            this.FieldLinkAdd("txtDesc", "DESCRIPTION", "", true, false);
        }
        #region[Үзэгдэлүүд]
        void FormPaDaytype_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140135, 140135, new object[] { txtDayType.EditValue });
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
        void FormPaDaytype_EventEdit(ref bool cancel)
        {
            object[] Value = { txtDayType.EditValue, txtDesc.EditValue };
            OldValue = Value;
        }
        void FormPaDaytype_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(txtDayType.EditValue), Static.ToStr(txtDesc.EditValue) };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140134, 140134, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140133, 140133, new object[] { NewValue, FieldName });
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
            FormUtility.SaveStateGrid(appname, formName, ref gridView1);
        }
        void FormPaDaytype_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Өдрийн төрлийн код");
            this.FieldLinkSetColumnCaption(1, "Өдрийн тайлбар");
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
        void FormPaDaytype_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140131, 140131, null);
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
        #endregion[]
    }
}