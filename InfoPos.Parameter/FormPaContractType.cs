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
    public partial class FormPaContractType : ISM.Template.frmTempProp
    {
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        public FormPaContractType(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = core.Resource;
            this.FieldLinkSetSaveState();
        }
        private void Init() 
        {
            this.EventRefresh += new delegateEventRefresh(FormPaContractType_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPaContractType_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPaContractType_EventSave);
            this.EventDelete += new delegateEventDelete(FormPaContractType_EventDelete);
            this.EventEdit += new delegateEventEdit(FormPaContractType_EventEdit);

                this.FieldLinkAdd("txtContractType", "ContractType","", true, true);
                this.FieldLinkAdd("txtName", "name", "", false, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
                this.FieldLinkAdd("cboMethod", "Method", "", false, false);


                FormUtility.LookUpEdit_SetList(ref cboMethod, 0, "Үлдэгдэл нь борлуулалтаар буурна");
                FormUtility.LookUpEdit_SetList(ref cboMethod, 1, "Үлдэгдэл нь автоматаар буурна");
                FormUtility.LookUpEdit_SetList(ref cboMethod, 2, "Үлдэгдэл хөтлөхгүй");

        }

        void FormPaContractType_EventEdit(ref bool cancel)
        {
            object[] Value = { txtContractType.EditValue, txtName.EditValue, txtName2.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }

        void FormPaContractType_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140235, 140235, new object[] { txtContractType.EditValue });
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

        void FormPaContractType_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(txtContractType.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(numOrderNo.EditValue), cboMethod.EditValue };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140234, 140234, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140233, 140233, new object[] { NewValue, FieldName });
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

        void FormPaContractType_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Гэрээний төрлийн дугаар");
            this.FieldLinkSetColumnCaption(1, "Нэр ");
            this.FieldLinkSetColumnCaption(2, "Нэр 2");
            this.FieldLinkSetColumnCaption(3, "Жагсаалтын эрэмбэ");
            appname = _core.ApplicationName;            
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

        void FormPaContractType_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140231, 140231, null);
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

        private void FormPaContractType_Load(object sender, EventArgs e)
        {

        }
    }
}