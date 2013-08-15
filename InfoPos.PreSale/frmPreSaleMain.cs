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

namespace InfoPos.PreSale
{
    public partial class frmPreSaleMain :ISM.Template.frmTempProp
    {
        #region[Variables]
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        int rowhandle = 0;
        int SelectTxnCode = 130326;
        int AddTxnCode = 130328;
        int EditTxnCode = 130329;
        int DeleteTxnCode = 130330;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public frmPreSaleMain(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init Function]
        private void Init() 
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormUnitType_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormUnitType_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormUnitType_EventSave);
                this.EventEdit += new delegateEventEdit(FormUnitType_EventEdit);
                this.EventDelete += new delegateEventDelete(FormUnitType_EventDelete);

                this.FieldLinkAdd("txtPreSaleProd", "PreSaleProd", "", true, true);
                this.FieldLinkAdd("txtName", "name", "", true, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("txtCount", "Count", "", true, false);
                this.FieldLinkAdd("cboAutoNumNo", "AutoNumNo", "", true, false);
                this.FieldLinkAdd("txtSalesAccountNo", "SalesAccountNo", "", true, false);
                this.FieldLinkAdd("txtRefundAccountNo", "RefundAccountNo", "", true, false);
                this.FieldLinkAdd("txtDiscountAccountNo", "DiscountAccountNo", "", true, false);
                this.FieldLinkAdd("txtBonusAccountNo", "BonusAccountNo", "", true, false);
                this.FieldLinkAdd("txtBonusExpAccountNo", "BonusExpAccountNo", "", true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void InitCombo()
        {
            #region[General]
            string msg = "";
            ArrayList Tables = new ArrayList();
            string[] names = { "AUTONUM" };
            DictUtility.PrivNo = SelectTxnCode;
            Result res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
            DataTable dt = (DataTable)Tables[0];
            if (dt == null)
            {
                msg = "Dictionary-д CURRENCY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboAutoNumNo, dt, "ID", "NAME", "", null);
                cboAutoNumNo.ItemIndex = 0;
            }
            
            if (msg != "")
                MessageBox.Show(msg);

            #endregion
        }
        #endregion
        #region[Event]
        void FormUnitType_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { 
                                        Static.ToStr(txtPreSaleProd.EditValue), 
                                        Static.ToStr(txtName.EditValue), 
                                        Static.ToStr(txtName2.EditValue), 
                                        Static.ToInt(txtCount.EditValue), 
                                        Static.ToInt(cboAutoNumNo.EditValue), 
                                        Static.ToStr(txtSalesAccountNo.EditValue), 
                                        Static.ToStr(txtRefundAccountNo.EditValue), 
                                        Static.ToStr(txtDiscountAccountNo.EditValue), 
                                        Static.ToStr(txtBonusAccountNo.EditValue), 
                                        Static.ToStr(txtBonusExpAccountNo.EditValue)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, EditTxnCode, EditTxnCode, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, AddTxnCode, AddTxnCode, new object[] { NewValue, FieldName });
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
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void FormUnitType_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, DeleteTxnCode, DeleteTxnCode, new object[] { txtPreSaleProd.EditValue });
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
        void FormUnitType_EventEdit(ref bool cancel)
        {
            object[] Value = { 
                                Static.ToStr(txtPreSaleProd.EditValue), 
                                Static.ToStr(txtName.EditValue), 
                                Static.ToStr(txtName2.EditValue), 
                                Static.ToInt(txtCount.EditValue), 
                                Static.ToInt(cboAutoNumNo.EditValue), 
                                Static.ToStr(txtSalesAccountNo.EditValue), 
                                Static.ToStr(txtRefundAccountNo.EditValue), 
                                Static.ToStr(txtDiscountAccountNo.EditValue), 
                                Static.ToStr(txtBonusAccountNo.EditValue), 
                                Static.ToStr(txtBonusExpAccountNo.EditValue)
                             };
            OldValue = Value;
        }
        void FormUnitType_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Урьчилсан бүтээгдэхүүний код (Зөвхөн ваучерийн хувьд)");
            this.FieldLinkSetColumnCaption(1, "Бүтээгдэхүүний нэр");
            this.FieldLinkSetColumnCaption(2, "Бүтээгдэхүүний нэр 2", true);
            this.FieldLinkSetColumnCaption(3, "Нийт үүсгэх ваучерийн тоо");

            this.FieldLinkSetColumnCaption(4, "Auto дугаарлалтын дугаар");
            this.FieldLinkSetColumnCaption(5, "Auto дугаарлалт");
            this.FieldLinkSetColumnCaption(6, "Борлуулалтын орлогын данс");
            this.FieldLinkSetColumnCaption(7, "Борлуулалтын буцаалт данс");
            this.FieldLinkSetColumnCaption(8, "Борлуулалтын хөнгөлөлтийн данс");
            this.FieldLinkSetColumnCaption(9, "Урамшууллын данс");
            this.FieldLinkSetColumnCaption(10, "Урамшууллын зардлын данс");

            appname = _core.ApplicationName;
            formname = "PreSale." + this.Name;
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
        void FormUnitType_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, SelectTxnCode, SelectTxnCode, null);
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
        #endregion
        #region[FormEvent]
        private void FormUnitType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void FormUnitType_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormUnitType_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion
    }
}
