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
    public partial class PATagSetup : ISM.Template.frmTempProp
    {        
        Core.Core _core = null;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        public PATagSetup(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();            
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }        
        private void Init() 
        {
            this.EventRefresh += new delegateEventRefresh(PATagSetup_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(PATagSetup_EventRefreshAfter);
            this.EventSave += new delegateEventSave(PATagSetup_EventSave);
            this.EventEdit += new delegateEventEdit(PATagSetup_EventEdit);
            this.EventDelete += new delegateEventDelete(PATagSetup_EventDelete);


            this.FieldLinkAdd("txtTagType", "TagType", "", true, true);
            this.FieldLinkAdd("txtName", "Name", "", true, false);
            this.FieldLinkAdd("numOffset", "Offset", "", false, false);
            this.FieldLinkAdd("numLength", "Length", "", false, false);
            this.FieldLinkAdd("cboFormat", "Format", "", true, false);            
        }
        private void InitCombo()
        {
            FormUtility.LookUpEdit_SetList(ref cboFormat, 0, "Текст");
            FormUtility.LookUpEdit_SetList(ref cboFormat, 1, "Дижит тэмдэгтээр");
            FormUtility.LookUpEdit_SetList(ref cboFormat, 2, "Байт тоон утга");
            FormUtility.LookUpEdit_SetList(ref cboFormat, 3, "Огноо");
        }
        #region[Үзэгдлүүд]
        void PATagSetup_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140175, 140175, new object[] { txtTagType.EditValue });
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
        void PATagSetup_EventEdit(ref bool cancel)
        {
            object[] Value = { txtTagType.EditValue, txtName.EditValue };
            OldValue = Value;
        }
        void PATagSetup_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(txtTagType.EditValue), 
                                      Static.ToStr(txtName.EditValue),
                                      Static.ToInt(numOffset.EditValue),
                                      Static.ToInt(numLength.EditValue),
                                      Static.ToInt(cboFormat.EditValue)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140174, 140174, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140173, 140173, new object[] { NewValue, FieldName });
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
        void PATagSetup_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Тагийн төрлийн ");
            this.FieldLinkSetColumnCaption(1, "Тагийн төрлийн нэр ");
            this.FieldLinkSetColumnCaption(2, "Бичилт хийгдэх байрлал ");
            this.FieldLinkSetColumnCaption(3, "Бичилтийн байт хэмжээ ");
            this.FieldLinkSetColumnCaption(4, "Мэдээллийн хэлбэр");            
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
        void PATagSetup_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140171, 140171, null);
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