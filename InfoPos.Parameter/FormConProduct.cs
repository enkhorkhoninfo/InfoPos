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
    public partial class FormConProduct :ISM.Template.frmTempProp
    {
        #region[Хувьсагч]
        object[] OldValue;
        FunctionParam fp = new FunctionParam();

        int SelectTxnCode = 140116;
        int AddTxnCode = 140118;
        int EditTxnCode = 140119;
        int DeleteTxnCode=140120;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormConProduct(InfoPos.Core.Core core)
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
                this.EventRefresh += new delegateEventRefresh(FormConProduct_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormConProduct_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormConProduct_EventSave);
                this.EventEdit += new delegateEventEdit(FormConProduct_EventEdit);
                this.EventDelete += new delegateEventDelete(FormConProduct_EventDelete);
                
                this.FieldLinkAdd("numProdCode", "ProdCode", "", true, true);
                this.FieldLinkAdd("txtName", "name", "", true, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("cboCurCode", "CurCode", "", true, false);
                this.FieldLinkAdd("numGL", "GL", "", true, false);
                this.FieldLinkAdd("cboTypeCode", "TypeCode", "", true, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void InitCombo() 
        {
            fp.SetCombo(_core,"CURRENCY", "CURRENCY", "NAME", SelectTxnCode,cboCurCode, "", null);
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboTypeCode, 0, "Энгийн");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboTypeCode, 1, "Балансжуулах");
        }
        #endregion
        #region[Event]
        void FormConProduct_EventSave(bool isnew, ref bool cancel)
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
                object[] FieldName = { "ProdCode", "Name", "Name2", "CurCode", "GL", "TypeCode", "OrderNo"};
                object[] NewValue = {numProdCode.EditValue,txtName.EditValue,txtName2.EditValue,cboCurCode.EditValue,numGL.EditValue,cboTypeCode.EditValue,numOrderNo.EditValue};
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxnCode, EditTxnCode , new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxnCode, AddTxnCode, new object[] { NewValue,FieldName});
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
        void FormConProduct_EventDelete()
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
        void FormConProduct_EventEdit(ref bool cancel)
        {
            object[] Value = { numProdCode.EditValue, txtName.EditValue, txtName2.EditValue, cboCurCode.EditValue, numGL.EditValue, cboTypeCode.EditValue, numOrderNo.EditValue };
            OldValue = Value;
        }
        void FormConProduct_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Бүтээгдэхүүний код");
            this.FieldLinkSetColumnCaption(1, "Нэр");
            this.FieldLinkSetColumnCaption(2, "2- нэр",true);
            this.FieldLinkSetColumnCaption(3, "Валютын код");
            this.FieldLinkSetColumnCaption(4, "Ерөнхий дэвтрийн дугаар");
            this.FieldLinkSetColumnCaption(5, "Төрлийн дугаар",true);
            this.FieldLinkSetColumnCaption(6, "Төрөл", true);
            this.FieldLinkSetColumnCaption(7, "Жагсаалтын эрэмбэ");
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
        void FormConProduct_EventRefresh(ref DataTable dt)
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
        private void FormConProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormConProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormConProduct_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion

    }
}