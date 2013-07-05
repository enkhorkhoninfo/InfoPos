using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class FormPaBill : ISM.Template.frmTempProp
    {
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        public FormPaBill(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombos();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        private void Init()
        {
            this.EventRefresh += new delegateEventRefresh(FormPaBill_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPaBill_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPaBill_EventSave);
            this.EventEdit += new delegateEventEdit(FormPaBill_EventEdit);
            this.EventDelete += new delegateEventDelete(FormPaBill_EventDelete);

            this.FieldLinkAdd("cboCurrency", "Currency", "", false, true);
            this.FieldLinkAdd("numBankNote", "BankNote", "", true, false);
            this.FieldLinkAdd("txtDesc", "DESCRIPTION", "", true, false);
            this.FieldLinkAdd("numOrderNo", "OrderNo", "", true, false);

        }
        private void InitCombos()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";

            DictUtility.PrivNo = 111000;

            string[] names = new string[] { "Currency" };
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

            DT = (DataTable)Tables[0];
            if (DT == null)
            {
                msg = "Dictionary-д OBJECTCLASS оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboCurrency, DT, "Currency", "NAME");
            }

            if (msg != "")
                MessageBox.Show(msg);
        }

        void FormPaBill_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140195, 140195, new object[] { cboCurrency.EditValue, numBankNote.Text });
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

        void FormPaBill_EventEdit(ref bool cancel)
        {
            try
            {
                object[] Value = { cboCurrency.EditValue, numBankNote.EditValue, txtDesc.EditValue, numOrderNo.EditValue };
                OldValue = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void FormPaBill_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(cboCurrency.EditValue), 
                                      Static.ToInt(numBankNote.EditValue), 
                                      Static.ToStr(txtDesc.EditValue),                                       
                                      Static.ToInt(numOrderNo.EditValue)
                                    };
                if (Static.ToStr(cboCurrency.EditValue).Trim() == "")
                {
                    MessageBox.Show("Валютыг сонгох шаардлагатай");
                    cont = cboCurrency;
                    cont.Select();
                    cancel = true;
                    return;
                }
                if (Static.ToStr(numBankNote.EditValue).Trim() == "")
                {
                    MessageBox.Show("Валютын дэвсгэртийг сонгох шаардлагатай");
                    cont = numBankNote;
                    cont.Select();
                    cancel = true;
                    return;
                }
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140194, 140194, new object[] { NewValue, OldValue, FieldName });
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140193, 140193, new object[] { NewValue, FieldName });
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    cancel = true;
                }
                else
                {
                    if (!isnew)
                        MessageBox.Show("Амжилттай засварлалаа.");
                    else
                        MessageBox.Show("Амжилттай нэмлээ .");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void FormPaBill_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Currency ");
            this.FieldLinkSetColumnCaption(1, "Дэвсгэртийн тоон утга");
            this.FieldLinkSetColumnCaption(2, "Дэвсгэртийн нэр, тайлбар ");
            this.FieldLinkSetColumnCaption(3, "Эрэмбийн дугаар");
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

        void FormPaBill_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140191, 140191, null);
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

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
    }
}