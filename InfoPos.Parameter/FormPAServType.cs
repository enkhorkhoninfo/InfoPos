﻿using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class PAServType : ISM.Template.frmTempProp
    {
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        public PAServType(Core.Core core)
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
            this.EventRefresh += new delegateEventRefresh(FormPAServType_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPAServType_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPAServType_EventSave);
            this.EventEdit += new delegateEventEdit(FormPAServType_EventEdit);
            this.EventDelete += new delegateEventDelete(FormPAServType_EventDelete);

            this.FieldLinkAdd("txtServType", "SERVTYPE", "", true, true);
            this.FieldLinkAdd("txtName", "NAME", "", true, false);
            this.FieldLinkAdd("txtName2", "NAME2", "", false, false);
            this.FieldLinkAdd("cboCatCode", "CatCode", "", false, false);
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

                DictUtility.PrivNo = 140221;
                string[] name = new string[] { "SERVCAT" };

                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д SERVCAT оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboCatCode, DT, "CatCode", "name");
                }
                
                if(msg != "")
                    MessageBox.Show(msg);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Өгөгдлийн баазаас Dictionary олдсонгүй.");
            }
        }
        #region[Үзэгдлүүд]
        void FormPAServType_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140186, 140186, null);
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
        void FormPAServType_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Төрлийн код, дугаар");
            this.FieldLinkSetColumnCaption(1, "Төрлийн нэр ");
            this.FieldLinkSetColumnCaption(2, "Төрлийн нэр 2");
            this.FieldLinkSetColumnCaption(3, "Ангилал");
            this.FieldLinkSetColumnCaption(4, "Тайлбар");
            this.FieldLinkSetColumnCaption(5, "Эрэмбийн дугаар");

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
        void FormPAServType_EventSave(bool isnew, ref bool cancel)
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


                object[] NewValue = { Static.ToStr(txtServType.EditValue), 
                                      Static.ToStr(txtName.EditValue), 
                                      Static.ToStr(txtName2.EditValue), 
                                      Static.ToInt(cboCatCode.EditValue),
                                      Static.ToStr(txtNote.EditValue),
                                      Static.ToInt(numOrderNo.EditValue)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140189, 140189, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140188, 140188, new object[] { NewValue, FieldName });
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
        void FormPAServType_EventEdit(ref bool cancel)
        {
            try
            {
                object[] Value = { txtServType.EditValue, txtName.EditValue, txtName2.EditValue, cboCatCode.EditValue, txtNote.EditValue, numOrderNo.EditValue };
                OldValue = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormPAServType_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140190, 140190, new object[] { txtServType.EditValue });
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
        #endregion[]

        private void PAServType_Load(object sender, EventArgs e)
        {

        }
    }
}