using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormPAPayType : ISM.Template.frmTempProp
    {
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        int PrivNo = 140201;
        public FormPAPayType(Core.Core core)
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
            this.EventRefresh += new delegateEventRefresh(FormPAPayType_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPAPayType_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPAPayType_EventSave);
            this.EventEdit += new delegateEventEdit(FormPAPayType_EventEdit);
            this.EventDelete += new delegateEventDelete(FormPAPayType_EventDelete);

            this.FieldLinkAdd("txtTypeId", "TypeId", "", true, true);
            this.FieldLinkAdd("txtName", "Name", "", true, false);
            this.FieldLinkAdd("txtName2", "Name2", "", false, false);
            this.FieldLinkAdd("txtSuspAccount", "SuspAccount", "", false, false);
            this.FieldLinkAdd("numOrderNo", "OrderNo", "", true, false);
            this.FieldLinkAdd("cboContractType", "ContractType", "", false, false);
            this.FieldLinkAdd("cboPaymentFlag", "PaymentFlag", "", false, false);
            this.FieldLinkAdd("chkContractCheck", "ContractCheck", "", false, false);
        }
        private void InitCombo()
        {
            try
            {
                Result res = new Result();
                ArrayList Tables = new ArrayList();
                DataTable DT = null;

                DictUtility.PrivNo = PrivNo;
                string[] name = new string[] { "CONTRACTTYPE"};                    
                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    MessageBox.Show("Dictionary-д CONTRACTTYPE оруулаагүй байна-" + res.ResultDesc);
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboContractType, DT, "contracttype", "name", "", new int[] { 2, 3, 4 });
                }

                FormUtility.LookUpEdit_SetList(ref cboPaymentFlag, 0, "Бэлэн төлбөр");
                FormUtility.LookUpEdit_SetList(ref cboPaymentFlag, 1, "Картын төлбөр");
                FormUtility.LookUpEdit_SetList(ref cboPaymentFlag, 9, "Бусад төлбөр (гэрээ, т/д, ваучир гм)");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Өгөгдлийн баазаас Dictionary олдсонгүй." + ex.Message);
            }
        }
        void FormPAPayType_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140205, 140205, new object[] { txtTypeId.EditValue });
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
        void FormPAPayType_EventEdit(ref bool cancel)
        {
            try
            {
                object[] Value = { txtTypeId.EditValue, txtName.EditValue, txtName2.EditValue, txtSuspAccount.EditValue, numOrderNo.EditValue, cboPaymentFlag.EditValue, cboContractType.EditValue, chkContractCheck.EditValue };
                OldValue = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormPAPayType_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(txtTypeId.EditValue), 
                                      Static.ToStr(txtName.EditValue), 
                                      Static.ToStr(txtName2.EditValue), 
                                      Static.ToStr(txtSuspAccount.EditValue),
                                      Static.ToInt(numOrderNo.EditValue),
                                      Static.ToInt(cboPaymentFlag.EditValue),
                                      Static.ToStr(cboContractType.EditValue),
                                      (chkContractCheck.Checked ? 1 : 0)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140204, 140204, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140203, 140203, new object[] { NewValue, FieldName });
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
        void FormPAPayType_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Төлбөрийн төрлийн код");
            this.FieldLinkSetColumnCaption(1, "Нэр ");
            this.FieldLinkSetColumnCaption(2, "Нэр 2");
            this.FieldLinkSetColumnCaption(3, "Дансны сонголтын код");
            this.FieldLinkSetColumnCaption(4, "Эрэмбийн дугаар");
            this.FieldLinkSetColumnCaption(5, "Төлбөрийн төрөл");
            this.FieldLinkSetColumnCaption(6, "Гэрээний бүлгийн дугаар");
            this.FieldLinkSetColumnCaption(7, "Гэрээний дугаарыг шалгах эсэх");            
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
        void FormPAPayType_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140201, 140201, null);
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
        private void chIsContract_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (chIsContract.Checked == true)
                //    cboContractType.Properties.ReadOnly = false;
                //else
                //{
                //    cboContractType.Properties.ReadOnly = true;
                //}
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                    
        }
        private void FormPAPayType_Load(object sender, EventArgs e)
        {

        }
    }
}