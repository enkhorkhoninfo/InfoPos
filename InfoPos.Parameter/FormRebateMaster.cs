using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormRebateMaster : ISM.Template.frmTempProp
    {
        #region[Variable]
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        #endregion[]
        #region[Construction]
        public FormRebateMaster(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion[]
        #region[Init]
        private void Init()
        {
            this.EventRefresh += new delegateEventRefresh(FormRebateMaster_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormRebateMaster_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormRebateMaster_EventSave);
            this.EventEdit += new delegateEventEdit(FormRebateMaster_EventEdit);
            this.EventDelete += new delegateEventDelete(FormRebateMaster_EventDelete);

            this.FieldLinkAdd("numMasterID", "MasterID", "", false, true);
            this.FieldLinkAdd("cboMasterType", "MasterType", "", true, false);
            this.FieldLinkAdd("numName", "Name", "", true, false);
            this.FieldLinkAdd("txtName2", "Name2", "", true, false);
            this.FieldLinkAdd("numListOrder", "ListOrder", "", true, false);


            FormUtility.LookUpEdit_SetList(ref cboMasterType, 0, "Хөнгөлөлт");
            FormUtility.LookUpEdit_SetList(ref cboMasterType, 1, "Урамшуулал");
            FormUtility.LookUpEdit_SetList(ref cboMasterType, 2, "Оноо");

            
        }
        #endregion[]
        #region[Event]
        void FormRebateMaster_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140340, 140340, new object[] { numMasterID.EditValue });
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
        void FormRebateMaster_EventEdit(ref bool cancel)
        {
            try
            {
                object[] Value = { numMasterID.EditValue, cboMasterType.EditValue, numName.EditValue, txtName2.EditValue, numListOrder.EditValue };
                OldValue = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormRebateMaster_EventSave(bool isnew, ref bool cancel)
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


                object[] NewValue = { numMasterID.EditValue, cboMasterType.EditValue, numName.EditValue, txtName2.EditValue, numListOrder.EditValue };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140339, 140339, new object[] { NewValue });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140338, 140338, new object[] { NewValue });
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
        }
        void FormRebateMaster_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "ID дугаар ");
            this.FieldLinkSetColumnCaption(1, "Хөнгөлөлт, Оноо, Урамшуулал эсэх");
            this.FieldLinkSetColumnCaption(2, "Нэр");
            this.FieldLinkSetColumnCaption(3, "Нэр 2");
            this.FieldLinkSetColumnCaption(4, "Жагсаалтын эрэмбэ");
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
        void FormRebateMaster_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140336, 140336, null);
                if (r.ResultNo != 0)
                {
                    if (r.Data == null)
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