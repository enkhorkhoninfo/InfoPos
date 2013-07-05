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
using InfoPos.Messages;


namespace InfoPos.Parameter
{
    public partial class FormBacProduct : ISM.Template.frmTempProp
    {
       #region[Хувьсагч]
        object[] OldValue;
        object[] FieldName;
        FunctionParam fp = new FunctionParam();       

        int SelectTxnCode = 140111;
        int AddTxnCode = 140113;
        int EditTxnCode = 140114;
        int DeleteTxnCode=140115;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
       #endregion
       #region[Байгуулагч функц]
        public FormBacProduct(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
       #region[Init]
        private void Init() 
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormBacProduct_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormBacProduct_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormBacProduct_EventSave);
                this.EventEdit += new delegateEventEdit(FormBacProduct_EventEdit);
                this.EventDelete += new delegateEventDelete(FormBacProduct_EventDelete);

                this.FieldLinkAdd("numProdCode", "ProdCode", "", true, true);
                this.FieldLinkAdd("txtName", "name", "", true, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("cboCurCode", "CurCode", "", true, false);
                this.FieldLinkAdd("cboGL", "GL", "", true, false);
                this.FieldLinkAdd("cboType", "Type", "", true, false);
                this.FieldLinkAdd("cboBalanceType", "BalanceType", "", true, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void InitCombo() 
        {
            fp.SetCombo(_core,"CURRENCY", "CURRENCY", "NAME", SelectTxnCode,cboCurCode , "", null);
            fp.SetCombo(_core, "CHART", "ACCOUNT", "NAME", SelectTxnCode, cboGL, "", null);

            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboBalanceType, "D", "Debit");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboBalanceType, "C", "Credit");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboBalanceType, "Z", "Debit and Credit");

            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 0, "Бусад");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 1, "Орлого");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 2, "Зарлага");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 3, "Авлага");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 4, "Өглөг");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 5, "Касс");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 6, "Хөрөнгө");            
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 7, "Өмч");

        }
        #endregion
       #region[Event]
        void FormBacProduct_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (!FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
            else
            {
                Result r;
                try
                {
                    object[] FieldName = {"PRODCODE", "NAME", "NAME2", "CURCODE", "GL", "TYPE", "BalanceType", "ORDERNO"};
                    object[] NewValue = { numProdCode.EditValue, txtName.EditValue, txtName2.EditValue, cboCurCode.EditValue, cboGL.EditValue, cboType.EditValue, cboBalanceType.EditValue, numOrderNo.EditValue };
                    if (!isnew)
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxnCode, EditTxnCode, new object[] { NewValue, OldValue, FieldName });
                        MessageBox.Show("Амжилттай засварлалаа.");
                    }
                    else
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxnCode, AddTxnCode, new object[] {NewValue,FieldName});
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
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }

        void FormBacProduct_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { numProdCode.EditValue });
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

        void FormBacProduct_EventEdit(ref bool cancel)
        {
            object[] Value = { numProdCode.EditValue, txtName.EditValue, txtName2.EditValue, cboCurCode.EditValue, cboGL.EditValue, cboType.EditValue, cboBalanceType.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }

        void FormBacProduct_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Бүтээгдэхүүний код");
            this.FieldLinkSetColumnCaption(1, "Нэр");
            this.FieldLinkSetColumnCaption(2, "2-р нэр",true);
            this.FieldLinkSetColumnCaption(3, "Валютын код");
            this.FieldLinkSetColumnCaption(4, "Ерөнхий дэвтрийн дугаар");
            this.FieldLinkSetColumnCaption(5, "Төрлийн дугаар",true);
            this.FieldLinkSetColumnCaption(6, "Төрөл");
            this.FieldLinkSetColumnCaption(7, "Балансын төрлийн дугаар",true);
            this.FieldLinkSetColumnCaption(8, "Балансын төрөл");
            this.FieldLinkSetColumnCaption(9, "Жагсаалтын эрэмбэ");
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
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

        void FormBacProduct_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, SelectTxnCode, SelectTxnCode, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
                else
                {
                    dt = r.Data.Tables[0];
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
        private void FormBacProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormBacProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormBacProduct_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);

        }
       #endregion

   }
}