using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

using ISM.Template;
using EServ.Shared;
using InfoPos.Core;

namespace InfoPos.Parameter
{
    public partial class FormPAInvType : ISM.Template.frmTempProp
    {
        #region[Хувьсагчууд]
        InfoPos.Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        #endregion[]
        public FormPAInvType(Core.Core core)
        {
            _core = core;
            InitializeComponent();            
            Init();
            InitCombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        private void Init()
        {
            this.EventRefresh += new delegateEventRefresh(FormPAInvType_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPAInvType_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPAInvType_EventSave);
            this.EventEdit += new delegateEventEdit(FormPAInvType_EventEdit);
            this.EventDelete += new delegateEventDelete(FormPAInvType_EventDelete);

            this.FieldLinkAdd("txtInvType", "InvType", "", true, true);
            this.FieldLinkAdd("txtName", "NAME", "", true, false);
            this.FieldLinkAdd("txtName2", "NAME2", "", false, false);
            this.FieldLinkAdd("cboCatCode", "CatCode", "", true, false);
            this.FieldLinkAdd("txtNote", "NOTE", "", false, false);
            this.FieldLinkAdd("numOrderNo", "ORDERNO", "", true, false);            
            
        }

        private void InitCombo()
        {
            try
            {
                Result res = new Result();
                ArrayList Tables = new ArrayList();
                DataTable DT = null;
                string msg = "";

                DictUtility.PrivNo = 140216;
                string[] name = new string[] { "INVCAT" };

                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д INVCAT оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboCatCode, DT, "CatCode", "name");
                }

                if (msg != "")
                    MessageBox.Show(msg);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Өгөгдлийн баазаас Dictionary олдсонгүй.");
            }
        }

        void FormPAInvType_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140160, 140160, new object[] { txtInvType.EditValue });
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

        void FormPAInvType_EventEdit(ref bool cancel)
        {
            object[] Value = {   txtInvType.EditValue, 
                                 txtName.EditValue, 
                                 txtName2.EditValue, 
                                 cboCatCode.EditValue, 
                                 txtNote.EditValue, 
                                 numOrderNo.EditValue };
            OldValue = Value;
        }

        void FormPAInvType_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { Static.ToStr(txtInvType.EditValue), 
                                      Static.ToStr(txtName.EditValue), 
                                      Static.ToStr(txtName2.EditValue), 
                                      Static.ToInt(cboCatCode.EditValue),
                                      Static.ToStr(txtNote.EditValue),
                                      Static.ToInt(numOrderNo.EditValue)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140159, 140159, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140158, 140158, new object[] { NewValue, FieldName });
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

        void FormPAInvType_EventRefreshAfter()
        {
            try 
            {
                FormUtility.SaveStateForm(appname, ref FormName);
                this.FieldLinkSetColumnCaption(0, "Төрлийн код, дугаар");
                this.FieldLinkSetColumnCaption(1, "Төрлийн нэр");
                this.FieldLinkSetColumnCaption(2, "Төрлийн нэр 2догч хэлээр");
                this.FieldLinkSetColumnCaption(3, "Ангилал");
                this.FieldLinkSetColumnCaption(4, "Тайлбар");
                this.FieldLinkSetColumnCaption(5, "Эрэмбийн дугаар");
                //appname = _core.ApplicationName;
                //formname = "Parameter." + this.Name;
                //FormName = this;
                //FormUtility.RestoreStateForm(appname, ref FormName);
                //FormUtility.RestoreStateGrid(appname, "Parameter." + this.txtName, ref gridView1);
                switch (btn)
                {
                    case 0: gridView1.FocusedRowHandle = rowhandle; break;
                    case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
                }
                btn = 0;
            }           
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void FormPAInvType_EventRefresh(ref DataTable dt) //140156
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                object[] obj = new object[2];
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140156, 140156, null);                

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

        private void FormPAInvType_Load(object sender, EventArgs e)
        {

        }
    }
}