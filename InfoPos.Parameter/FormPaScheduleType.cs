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
    public partial class PaScheduleType : ISM.Template.frmTempProp
    {
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        public PaScheduleType(Core.Core core)
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
            this.EventRefresh += new delegateEventRefresh(FormPaScheduleType_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPaScheduleType_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPaScheduleType_EventSave);
            this.EventDelete += new delegateEventDelete(FormPaScheduleType_EventDelete);
            this.EventEdit += new delegateEventEdit(FormPaScheduleType_EventEdit);            

            this.FieldLinkAdd("txtScheduleType", "ScheduleType", "", true, true);
            this.FieldLinkAdd("txtName", "Name", "", true, false);
            this.FieldLinkAdd("txtName2", "NAME2", "", false, false); 
            this.FieldLinkAdd("cboUnit", "Unit", "", false, false);
            this.FieldLinkAdd("cboMethod", "Method", "", false, false);
            this.FieldLinkAdd("numDuration", "Duration", "", true, false);            
        }

       
        private void InitCombo() 
        {
            FormUtility.LookUpEdit_SetList(ref cboUnit, "T", "Time based");
            FormUtility.LookUpEdit_SetList(ref cboUnit, "D", "Day based");            
            FormUtility.LookUpEdit_SetList(ref cboMethod, 0, "FIFO");

            cboUnit.ItemIndex = 0;
            cboMethod.ItemIndex = 0;
        }
        void FormPaScheduleType_EventEdit(ref bool cancel)
        {
            object[] Value = {   txtScheduleType.EditValue, 
                                 txtName.EditValue, 
                                 txtName2.EditValue, 
                                 cboUnit.EditValue, 
                                 cboMethod.EditValue, 
                                 numDuration.EditValue };
            OldValue = Value;
            if (txtScheduleType.Text == "")
            {
                cboMethod.ItemIndex = 0;
                cboUnit.ItemIndex = 0;
            }
        }
        void FormPaScheduleType_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140185, 140185, new object[] { txtScheduleType.EditValue });
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
        void FormPaScheduleType_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(txtScheduleType.EditValue), 
                                      Static.ToStr(txtName.EditValue),
                                      Static.ToStr(txtName2.EditValue),
                                      Static.ToStr(cboUnit.EditValue),
                                      Static.ToInt(cboMethod.EditValue),
                                      Static.ToInt(numDuration.EditValue)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140184, 140184, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140183, 140183, new object[] { NewValue, FieldName });
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

        void FormPaScheduleType_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Төрлийн код, дугаар");
            this.FieldLinkSetColumnCaption(1, "Төрлийн нэр ");
            this.FieldLinkSetColumnCaption(2, "Төрлийн нэр 2");
            this.FieldLinkSetColumnCaption(3, "Хэмжигдэх нэгж");
            this.FieldLinkSetColumnCaption(4, "Хуваарь хийх арга ");
            this.FieldLinkSetColumnCaption(5, "Хуваарь дээрх таскын урт ");
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

        void FormPaScheduleType_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140181, 140181, null);
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
    }
}