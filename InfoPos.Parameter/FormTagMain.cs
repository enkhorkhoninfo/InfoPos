using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using  ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormTagMain : ISM.Template.frmTempProp
    {
        #region [ Variable ]
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        int SelectTxnCode = 140356;
        FunctionParam fp = new FunctionParam();
        #endregion
        public FormTagMain(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            this.Resource = core.Resource;
            Init();
            InitCombo();
            this.FieldLinkSetSaveState();
        }
        private void Init() 
        {
            this.EventRefresh += new delegateEventRefresh(FormPosTerminal_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPosTerminal_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPosTerminal_EventSave);
            this.EventDelete += new delegateEventDelete(FormPosTerminal_EventDelete);
            this.EventEdit += new delegateEventEdit(FormPosTerminal_EventEdit);

            this.FieldLinkAdd("txtTagID", "TagID", "", true, true);
            this.FieldLinkAdd("cboTagType", "TagType", "", false, false);
            this.FieldLinkAdd("cboStatus", "Status", "", false, false);
            this.FieldLinkAdd("numOrderNo", "OrderNo", "", false, false);
            this.FieldLinkSetValues();
        }
        void FormPosTerminal_EventEdit(ref bool cancel)
        {
            object[] Value = { txtTagID.EditValue, cboTagType.EditValue, cboStatus.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }
        void FormPosTerminal_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140360, 140360, new object[] { txtTagID.EditValue });
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
        void FormPosTerminal_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(txtTagID.EditValue), 
                                      Static.ToInt(cboTagType.EditValue), 
                                      Static.ToInt(cboStatus.EditValue),
                                      Static.ToInt(numOrderNo.EditValue)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140359, 140079, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140358, 140358, new object[] { NewValue, FieldName });
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
        void FormPosTerminal_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Tag ын дугаар ");
            this.FieldLinkSetColumnCaption(1, "Tag ын төрөл ");
            this.FieldLinkSetColumnCaption(2, "Tag ын төлөв");
            this.FieldLinkSetColumnCaption(3, "Эрэмбэ");
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
        private void InitCombo()
        {
            fp.SetCombo(_core, "TAGSETUP", "TAGTYPE", "NAME", SelectTxnCode, cboTagType, "", null);

            FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Идэвхтэй");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Идэвхгүй");
        }
        void FormPosTerminal_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140356, 140356, null);
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
