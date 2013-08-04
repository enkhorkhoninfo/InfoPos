using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class FormPaPriceType : ISM.Template.frmTempProp
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
        public FormPaPriceType(InfoPos.Core.Core core)
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

            this.FieldLinkAdd("txtPriceTypeID", "PriceTypeID", "", true, true);
            this.FieldLinkAdd("txtName", "Name", "", true, false);
            this.FieldLinkAdd("txtName2", "Name2", "", false, false);
            this.FieldLinkAdd("cboDayType", "DayType", "", false, false);
            this.FieldLinkAdd("dtStartTime", "StartTime", "", true, false);
            this.FieldLinkAdd("dtEndTime", "EndTime", "", true, false);
            this.FieldLinkAdd("txtOrderNo", "OrderNo", "", false, false);

            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboDayType, "0", "Үгүй");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboDayType, "1", "Тийм");

            ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboDayType, 0);
        }
        private void InitCombo()
        {
            try
            {
                Result res = new Result();
                ArrayList Tables = new ArrayList();
                DataTable DT = null;
                string msg = "";

                DictUtility.PrivNo = 140416;
                string[] name = new string[] { "PADAYTYPE" };
                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д PADAYTYPE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboDayType, DT, "DayType", "name");
                }

                if (msg != "")
                    MessageBox.Show(msg);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Өгөгдлийн баазаас Dictionary олдсонгүй.");
            }
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
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140420, 140420, new object[] { txtPriceTypeID.EditValue });
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

            object[] Value = { txtPriceTypeID.EditValue, txtName.EditValue, txtName2.EditValue, cboDayType.EditValue, dtStartTime.EditValue,
                             dtEndTime.EditValue, txtOrderNo.EditValue};
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
                object[] NewValue = { Static.ToStr(txtPriceTypeID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToInt(cboDayType.EditValue), Static.ToDateTime(dtStartTime.EditValue),
                                    Static.ToDateTime(dtEndTime.EditValue), Static.ToInt(txtOrderNo.EditValue)};
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140419, 140419, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140418, 140418, new object[] { NewValue, FieldName });
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
            this.FieldLinkSetColumnCaption(0, "Үнийн төрөлийн код");
            this.FieldLinkSetColumnCaption(1, "Нэр");
            this.FieldLinkSetColumnCaption(2, "Нэр2");
            this.FieldLinkSetColumnCaption(3, "Өдрийн төрөл");
            this.FieldLinkSetColumnCaption(4, "Өдрийн төрөл");
            this.FieldLinkSetColumnCaption(5, "Эхлэх цаг (Зөвхөн цаг)");
            this.FieldLinkSetColumnCaption(6, "Дуусах цаг (Зөвхөн цаг)");
            this.FieldLinkSetColumnCaption(7, "Эрэмбэ");

            this.gridView1.Columns[5].DisplayFormat.FormatString = "{0:T}";
            this.gridView1.Columns[6].DisplayFormat.FormatString = "{0:T}";

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
        void FormPaDaytype_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140416, 140416, null);
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

        private void FormPaDaytype_Load(object sender, EventArgs e)
        {

        }
    }
}
