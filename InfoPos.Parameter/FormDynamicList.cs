using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class FormDynamicList : ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] NewValue;
        object[] FieldName;
        int rowhandle;
        int btn=0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormDynamicList(InfoPos.Core.Core core)
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
                this.EventRefresh += new delegateEventRefresh(FormDynamicList_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormDynamicList_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormDynamicList_EventSave);
                this.EventEdit += new delegateEventEdit(FormDynamicList_EventEdit);
                this.EventDelete += new delegateEventDelete(FormDynamicList_EventDelete);

                this.FieldLinkAdd("txtKey", "Key", "", true, false);
                this.FieldLinkAdd("txtID", "ID", "", true, false);
                this.FieldLinkAdd("txtName", "Name", "", true, false);
                this.FieldLinkAdd("txtFormula", "Formula", "", false, false);
                this.FieldLinkAdd("numRate", "Rate", "", false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Event]
        void FormDynamicList_EventSave(bool isnew, ref bool cancel)
        {
            string msj = "";
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
                object[] NewValue = { 
                                        Static.ToStr(txtKey.EditValue), 
                                        Static.ToStr(txtID.EditValue), 
                                        Static.ToStr(txtName.EditValue), 
                                        Static.ToStr(txtFormula.EditValue), 
                                        Static.ToDecimal(numRate.Text)
                                    };
                object[] obj = new object[5];
                                     obj[0] = Static.ToStr(txtKey.EditValue); 
                                     obj[1] = Static.ToStr(txtID.EditValue); 
                                     obj[2] = Static.ToStr(txtName.EditValue); 
                                     obj[3] = Static.ToStr(txtFormula.EditValue);
                                     obj[4] = Static.ToDecimal(numRate.EditValue);                                    
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140309, 140309, new object[] { OldValue, NewValue, FieldName });
                    msj = "Амжилттай засварлалаа .";
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140308, 140308, new object[] { NewValue, FieldName });
                    msj = "Амжилттай нэмлээ .";
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    cancel = true;
                }
                else
                {
                    this.FieldLinkSetNewState();
                    this.FieldLinkSetSaveState();
                    MessageBox.Show(msj);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void FormDynamicList_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140310, 140310, new object[] { txtID.EditValue });
                    if (r.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        btn = 1;
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
        void FormDynamicList_EventEdit(ref bool cancel)
        {
            object[] Value = new object[5];
            
            Value[0] =    Static.ToStr(txtKey.EditValue); 
            Value[1] =    Static.ToStr(txtID.EditValue); 
            Value[2] =    Static.ToStr(txtName.EditValue); 
            Value[3] =    Static.ToStr(txtFormula.EditValue);
            Value[4] =    Static.ToDecimal(numRate.EditValue); 
            OldValue = Value;
        }
        void FormDynamicList_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Түлхүүр талбар", false);
            this.FieldLinkSetColumnCaption(1, "ID дугаар", false);
            this.FieldLinkSetColumnCaption(2, "Нэр", false);
            this.FieldLinkSetColumnCaption(3, "Томъёо", false);
            this.FieldLinkSetColumnCaption(4, "Хувь дүн", false);
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridView1);
            object[] Value = new object[5];

            Value[0] = Static.ToStr(txtKey.EditValue);
            Value[1] = Static.ToStr(txtID.EditValue);
            Value[2] = Static.ToStr(txtName.EditValue);
            Value[3] = Static.ToStr(txtFormula.EditValue);
            Value[4] = Static.ToDecimal(numRate.EditValue);
            OldValue = Value;
            switch(btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle-1; break;
            }
            btn = 0;
        }
        void FormDynamicList_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140306, 140306, null);
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
        private void FormDynamicList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormDynamicList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormDynamicList_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
            //object[] Value = 
            //{ 
            //    Static.ToStr(txtKey.EditValue), 
            //    Static.ToStr(txtID.EditValue), 
            //    Static.ToStr(txtName.EditValue), 
            //    Static.ToStr(txtFormula.EditValue), 
            //    Static.ToDecimal(numRate.EditValue) 
            //};
            //OldValue = Value;
        }
        #endregion
   }
}