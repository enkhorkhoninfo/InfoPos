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
using InfoPos.Core;

namespace InfoPos.Parameter
{
    public partial class FormCustContactType : ISM.Template.frmTempProp
    {
        #region[Variables]
        int rowhandle = 0;
        InfoPos.Core.Core _core;
        object[] FieldName;
        int btn = 0;
        string appname = "", formname = "";
        Form FormName = null;
        object[] OldValue;
        #endregion[]
        public FormCustContactType(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.FieldLinkSetSaveState();
            if (_core.Resource != null)
            {
                this.Resource = _core.Resource;
            }            
        }
        #region[Init]
        private void Init()
        {
            this.EventRefresh += new delegateEventRefresh(FormCustContactType_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormCustContactType_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormCustContactType_EventSave);
            this.EventDelete += new delegateEventDelete(FormCustContactType_EventDelete);
            this.FieldLinkAdd("numTypeCode", "TypeCode", "", true, true);
            this.FieldLinkAdd("txtName", "name", "", true, false);
            this.FieldLinkAdd("txtName2", "name2", "", false, false);
            this.FieldLinkAdd("numOrderNo", "OrderNo", "", true, false);  
        }
#endregion[]    
        #region[Event]
        void FormCustContactType_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 120071, 120071, new object[] { numTypeCode.EditValue });
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
        void FormCustContactType_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Харилцагчтай холбоо барисан төрлийн код");            
            this.FieldLinkSetColumnCaption(1, "Нэр");            
            this.FieldLinkSetColumnCaption(2, "Нэр Англи");            
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
        void FormCustContactType_EventSave(bool isnew, ref bool cancel)
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
                string msj = "";
                object[] NewValue = { Static.ToInt(numTypeCode.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue) };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 120070, 120070, new object[] { NewValue, OldValue });
                    msj = "Амжилттай засварлалаа.";
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 120069, 120069, new object[] { NewValue });
                    msj = "Амжилттай нэмлээ .";
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    cancel = true;
                }
                else
                {
                    MessageBox.Show(msj);
                    this.FieldLinkSetSaveState();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        
        
        
        
        
        
        }
        void FormCustContactType_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 120067, 120067, null);
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
        #region[Form Events]
        private void FormCustContactType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        private void FormCustContactType_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormCustContactType_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);            
        }
        #endregion[]
    }
}