using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormDocTemplate : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        long _docid;
        int _itemno;
        int pItemNo;
        int pTemplateID;
        bool loadModel = false;
        bool loadSQL = false;
        bool loadParams = false;
        bool loadChain = false;
        object[] OldValue;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormDocTemplate(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            if (_core.Resource != null)
            {
                ucModelGen.Resource = _core.Resource;
                ucSql.Resource = _core.Resource;
                ucParams.Resource = _core.Resource;
            }
        }
        #endregion
        #region[Init Function]
        void Init()
        {
            InitEvent();
            InitToggles();
            InitData();
            InitCombo();
        }
        void InitEvent()
        {
            #region[Tab1]
            ucModelGen.EventDelete += new ucTogglePanel.delegateEventDelete(ucModelGen_EventDelete);
            ucModelGen.EventEdit += new ucTogglePanel.delegateEventEdit(ucModelGen_EventEdit);
            ucModelGen.EventSave += new ucTogglePanel.delegateEventSave(ucModelGen_EventSave);
            ucModelGen.EventExit += new ucTogglePanel.delegateEventExit(ucModelGen_EventExit);
            ucModelGen.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucModelGen_EventAddAfter);
            #endregion
            #region[Tab2]
            ucSql.EventDelete += new ucTogglePanel.delegateEventDelete(ucSql_EventDelete);
            ucSql.EventEdit += new ucTogglePanel.delegateEventEdit(ucSql_EventEdit);
            ucSql.EventSave += new ucTogglePanel.delegateEventSave(ucSql_EventSave);
            ucSql.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucSql_EventAddAfter);
            #endregion
            #region[Tab3]
            ucParams.EventDelete += new ucTogglePanel.delegateEventDelete(ucParams_EventDelete);
            ucParams.EventEdit += new ucTogglePanel.delegateEventEdit(ucParams_EventEdit);
            ucParams.EventSave += new ucTogglePanel.delegateEventSave(ucParams_EventSave);
            ucParams.EventAddAfter+=new ucTogglePanel.delegateEventAddAfter(ucParams_EventAddAfter);
            #endregion
        }
        void InitCombo()
        {
            #region[Tab1]
            FormUtility.LookUpEdit_SetList(ref cboExportType, "XLS","Excel");
            FormUtility.LookUpEdit_SetList(ref cboExportType, "DOC", "Word");
            FormUtility.LookUpEdit_SetList(ref cboExportType, "PDF", "PDF");
            FormUtility.LookUpEdit_SetValue(ref cboExportType,"XLS");
            #endregion
            #region[Tab3]
            FormUtility.LookUpEdit_SetList(ref cboDocParamType, 0, "Үсэг, тоо");
            FormUtility.LookUpEdit_SetList(ref cboDocParamType, 1, "Тоо");
            FormUtility.LookUpEdit_SetList(ref cboDocParamType, 2, "Огноо");
            FormUtility.LookUpEdit_SetList(ref cboRequired, 0, "Value Notrequired");
            FormUtility.LookUpEdit_SetList(ref cboRequired, 1, "Value Required");
            cboDocParamType.EditValue = 0;
            cboRequired.EditValue = 0;      
            #endregion
        }
        void InitToggles()
        {
            #region[Tab1]
            ucModelGen.ToggleShowDelete = true;
            ucModelGen.ToggleShowEdit = true;
            ucModelGen.ToggleShowExit = true;
            ucModelGen.ToggleShowNew = true;
            ucModelGen.ToggleShowReject = true;
            ucModelGen.ToggleShowSave = true;
            ucModelGen.DataSource = null;
            ucModelGen.GridView = gvwModel;
            #endregion
            #region[Tab2]
            ucSql.ToggleShowDelete = true;
            ucSql.ToggleShowEdit = true;
            ucSql.ToggleShowNew = true;
            ucSql.ToggleShowReject = true;
            ucSql.ToggleShowSave = true;
            ucSql.DataSource = null;
            ucSql.GridView = gvwSql;
            #endregion
            #region[Tab3]
            ucParams.ToggleShowDelete = true;
            ucParams.ToggleShowEdit = true;
            ucParams.ToggleShowNew = true;
            ucParams.ToggleShowReject = true;
            ucParams.ToggleShowSave = true;
            ucParams.DataSource = null;
            ucParams.GridView = gvwParameter;
            #endregion
        }
        void InitData()
        {
            #region[Tab1]
            ucModelGen.FieldLinkAdd("numID", 0, "ID", "", true, true);
            ucModelGen.FieldLinkAdd("txtName", 0, "Name", "", true, false);
            ucModelGen.FieldLinkAdd("txtName2", 0, "Name2", "", false, false);
            ucModelGen.FieldLinkAdd("txtDocFileName", 0, "DocFileName", "", true, false);
            ucModelGen.FieldLinkAdd("cboExportType", 0, "ExportType", "", true, false);
            #endregion
            #region[Tab2]
            ucSql.FieldContainer = pnlSql.Controls;
            ucSql.FieldLinkAdd("numDocID", 0, "ID", "", true, true, true);
            ucSql.FieldLinkAdd("mmoSql", 0, "Sql", "", false, true);
            ucSql.FieldLinkAdd("mmoParams", 0, "Params", "", false,false,true);
            ucSql.FieldLinkAdd("numItemNo", 0, "ItemNo", "", true, true);
            #endregion
            #region[Tab3]
            ucParams.FieldContainer = pnlParams.Controls;
            ucParams.FieldLinkAdd("numDocMID", 0, "ID", "", true, true, true);
            ucParams.FieldLinkAdd("numParamID", 0, "PARAMID", "", true, true);
            ucParams.FieldLinkAdd("mmoName", 0, "Name", "", true, false);
            ucParams.FieldLinkAdd("mmoDescription", 0, "Description", "", false, false);
            ucParams.FieldLinkAdd("cboDocParamType", 0, "DocParamType", "", true, false);
            ucParams.FieldLinkAdd("txtFormat", 0, "FORMAT", "", false, false);
            ucParams.FieldLinkAdd("mmoValue", 0, "VALUE", "", false, false);
            ucParams.FieldLinkAdd("mmoListValue", 0, "LISTVALUE", "", false, false);
            ucParams.FieldLinkAdd("cboRequired", 0, "REQUIRED", "", true, false);
            ucParams.FieldLinkAdd("numItemLen", 0, "ITEMLEN", "", false, false);
            ucParams.FieldLinkAdd("txtMask", 0, "MASK", "", false, false);
            ucParams.FieldLinkAdd("numOrderNo", 0, "OrderNo", "", true, false);
            #endregion
            if (_core.Resource != null) { btnSave.Image = _core.Resource.GetImage("navigate_save"); }
        }
        #endregion
        #region[General]
        private void FormDocTemplate_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            RefreshModelData();
        }
        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            switch (e.PageIndex)
            {
                case 0: LoadModelData(); break;
                case 1: LoadSqlData(); break;
                case 2: LoadParamsData(); break;
                case 3: LoadChainData(); break;
            }
        }
        private void xtraTabControl1_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (gvwModel.RowCount == 0)
                    e.Cancel = true;
                else
                {
                    Control control = FormUtility.FindControl(e.Page.Controls, typeof(ucTogglePanel));
                    if (control != null)
                    {
                        ucTogglePanel uc = (ucTogglePanel)control;
                        if (uc.ToggleFlag != 0) e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormDocTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwModel);
            FormUtility.SaveStateGrid(appname, formname, ref gvwSql);
            FormUtility.SaveStateGrid(appname, formname, ref gvwParameter);
            FormUtility.SaveStateGrid(appname, formname, ref gvwSqlCon);
            FormUtility.SaveStateGrid(appname, formname, ref gvwParCon);
        }
        private void FormDocTemplate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
        #region[Tab1]
        #region[Events]
        void ucModelGen_EventAddAfter()
        {
            cboExportType.ItemIndex = 0;
        }
        void ucModelGen_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void ucModelGen_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (ucModelGen.FieldValidate(ref err, ref cont) == true)
            {
                SaveModel(isnew, ref cancel);
            }
            else
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
        }
        void ucModelGen_EventEdit(ref bool cancel)
        {
            object[] Value = new object[5];
            Value[0] = Static.ToInt(numID.EditValue);                     //ID
            Value[1] = Static.ToStr(txtName.EditValue);                   //Name
            Value[2] = Static.ToStr(txtName2.EditValue);                  //Name2
            Value[3] = Static.ToStr(txtDocFileName.EditValue);  //Branch
            Value[4] = Static.ToStr(cboExportType.EditValue);   //FeeCurCode
            OldValue = Value;
        }
        void ucModelGen_EventDelete()
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150006, 150006, new object[] {Static.ToLong(numID.EditValue)});
                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    RefreshModelData();
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucModelGen_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwModel);
        }
        #endregion
        #region[Function]
        void LoadModelData()
        {
            RefreshModelData();
        }
        void RefreshModelData()
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150000, 150000,null);
                if (res.AffectedRows != 0)
                {
                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        ucModelGen.DataSource = res.Data;
                        ucModelGen.FieldLinkSetValues();
                        SetModelData();
                        ucModelGen.FieldLinkSetSaveState();
                        _itemno = -1;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                else
                {
                    ucModelGen.FieldLinkSetSaveState();
                    ucModelGen.DataSource = null;
                    _itemno = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SaveModel(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            object[] obj = new object[5];
            string msg = "";
            try
            {
                object[] FieldName = { "ID", "NAME", "NAME2", "DOCFILENAME", "CBOEXPORTTYPE" };
                object[] Value = new object[5];
                obj[0] = Static.ToInt(numID.EditValue);                     //ID
                obj[1] = Static.ToStr(txtName.EditValue);                   //Name
                obj[2] = Static.ToStr(txtName2.EditValue);                  //Name2
                obj[3] = Static.ToStr(txtDocFileName.EditValue);  //Branch
                obj[4] = Static.ToStr(cboExportType.EditValue);   //FeeCurCode
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150004, 150004, new object[] { obj, FieldName });
                    msg = "Амжилттай нэмлээ";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150005, 150005, new object[] { obj, OldValue, FieldName });
                    msg = "Амжилттай засварлалаа";
                }
                if (res.ResultNo == 0)
                {
                    RefreshModelData();
                    MessageBox.Show(msg);
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cancel = true;
            }
        }
        void SetModelData()
        {
            gvwModel.Columns[0].Caption = "Дугаар";
            gvwModel.Columns[1].Caption = "Нэр";
            gvwModel.Columns[2].Caption = "Нэр2";
            gvwModel.Columns[3].Caption = "Файлын нэр";
            gvwModel.Columns[4].Caption = "Файлын формат";

            gvwModel.Columns[0].OptionsColumn.AllowEdit = false;
            gvwModel.Columns[1].OptionsColumn.AllowEdit = false;
            gvwModel.Columns[2].OptionsColumn.AllowEdit = false;
            gvwModel.Columns[3].OptionsColumn.AllowEdit = false;
            gvwModel.Columns[4].OptionsColumn.AllowEdit = false;
        }
        #endregion
        private void gvwModel_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucModelGen.FieldLinkSetValues();
            _docid = Static.ToInt(numID.EditValue);
        }
        #endregion
        #region[Tab2]
        #region[Events]
        void ucSql_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (!ucSql.FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
            else
            {
                Result res = new Result();
                object[] obj = new object[4];
                string msg = "";
                try
                {
                    object[] FieldName = { "ID", "SQL", "PARAMS", "ITEMNO" };
                    object[] Value = new object[4];
                    obj[0] = Static.ToInt(numDocID.EditValue);      //ID
                    obj[1] = Static.ToStr(numItemNo.EditValue);     //ItemNo
                    obj[2] = Static.ToStr(mmoSql.EditValue);        //SQL
                    obj[3] = Static.ToStr(mmoParams.EditValue);     //Params
                    if (isnew)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150013, 150013, new object[] { obj, FieldName });
                        msg = "Амжилттай нэмлээ";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150014, 150014, new object[] { obj, OldValue, FieldName, OldValue[1] });
                        msg = "Амжилттай засварлалаа";
                    }
                    if (res.ResultNo == 0)
                    {
                        RefreshSqlData();
                        MessageBox.Show(msg);
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                        cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cancel = true;
                }
            }
        }
        void ucSql_EventEdit(ref bool cancel)
        {
            object[] Value = new object[4];
            Value[0] = Static.ToInt(numDocID.EditValue);                     //ID
            Value[1] = Static.ToStr(numItemNo.EditValue);  //ItemNo
            Value[2] = Static.ToStr(mmoSql.EditValue);  //SQL
            Value[3] = Static.ToStr(mmoParams.EditValue);  //Params
            OldValue = Value;
        }
        void ucSql_EventDelete()
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150015, 150015, new object[] { Static.ToLong(numID.EditValue), Static.ToInt(numItemNo.EditValue) });
                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    RefreshSqlData();
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucSql_EventAddAfter()
        {
            numDocID.EditValue = _docid;
        }
        void ucSql_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwSql);
        }
        #endregion
        #region[Function]
        void LoadSqlData()
        {
            RefreshSqlData();
            numDocID.EditValue = _docid;
        }
        void RefreshSqlData()
        {
            Result res = new Result();
            try
            {
                grdSql.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150011, 150011,new object[]{ _docid });
                if (res.ResultNo == 0 || res.ResultNo == 9110014)
                {
                    if (res.Data.Tables[0].Rows.Count != 0)
                    {
                        ucSql.DataSource = res.Data;
                        ucSql.FieldLinkSetValues();
                        ucSql.FieldLinkSetSaveState();
                        SetSqlData();
                    }
                    else
                    {
                        ucSql.FieldLinkClearValues();
                        ucSql.FieldLinkSetNewState();
                    }
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetSqlData()
        {
            gvwSql.Columns[0].Caption = "Документын дугаар";
            gvwSql.Columns[1].Caption = "SQL";
            gvwSql.Columns[2].Caption = "Параметр";
            gvwSql.Columns[3].Caption = "SQL -н дугаар";

            gvwSql.Columns[0].OptionsColumn.AllowEdit = false;
            gvwSql.Columns[1].OptionsColumn.AllowEdit = false;
            gvwSql.Columns[2].OptionsColumn.AllowEdit = false;
            gvwSql.Columns[3].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwSql);
        }
        #endregion
        private void gvwSql_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucSql.FieldLinkSetValues();
            _itemno = Static.ToInt(numItemNo.EditValue);
        }
        #endregion
        #region[Tab3]
        #region[Function]
        void LoadParamsData()
        {
            RefreshParamsData();
            numDocMID.EditValue = _docid;
            cboDocParamType.ItemIndex = 0;
            cboRequired.ItemIndex = 0;
        }
        void RefreshParamsData()
        {
            Result res = new Result();
            try
            {
                grdParameter.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150001, 150001, new object[] { _docid });
                if (res.ResultNo == 0 || res.ResultNo == 9110014)
                {
                    if (res.Data.Tables[0].Rows.Count!=0)
                    {
                        ucParams.DataSource = res.Data;
                        ucParams.FieldLinkSetValues();
                        ucParams.FieldLinkSetSaveState();
                        SetParamsData();
                    }
                    else
                    {
                        ucParams.FieldLinkClearValues();
                        ucParams.FieldLinkSetNewState();
                    }

                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetParamsData()
        {
            gvwParameter.Columns[0].Caption = "Документын угаар";
            gvwParameter.Columns[1].Caption = "Параметрийн код";
            gvwParameter.Columns[2].Caption = "Нэр";
            gvwParameter.Columns[3].Caption = "Тайлбар";
            gvwParameter.Columns[4].Caption = "Төрөл";
            gvwParameter.Columns[5].Caption = "Формат";
            gvwParameter.Columns[6].Caption = "Анхны утга";
            gvwParameter.Columns[7].Caption = "Өгөгдөл";
            gvwParameter.Columns[8].Caption = "Required";
            gvwParameter.Columns[9].Caption = "Утгын урт";
            gvwParameter.Columns[10].Caption = "Утгын маск";
            gvwParameter.Columns[11].Caption = "Жагсаалтын эрэмбэ";

            gvwParameter.Columns[0].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[1].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[2].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[3].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[4].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[5].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[6].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[7].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[8].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[9].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[10].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[11].OptionsColumn.AllowEdit = false;
        }
        void SaveParamsData(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            object[] obj = new object[12];
            string msg = "";
            try
            {
                object[] FieldName = { "ID", "PARAMID", "NAME", "Description", "DocParamType", "FORMAT", "VALUE", "LISTVALUE", "REQUIRED", "ITEMLEN", "MASK", "OrderNo" };
                obj[0] = Static.ToLong(numDocMID.EditValue);
                obj[1] = Static.ToInt(numParamID.EditValue);
                obj[2] = Static.ToStr(mmoName.EditValue);
                obj[3] = Static.ToStr(mmoDescription.EditValue);
                obj[4] = Static.ToInt(cboDocParamType.EditValue);
                obj[5] = Static.ToStr(txtFormat.EditValue);
                obj[6] = Static.ToStr(mmoValue.EditValue);
                obj[7] = Static.ToStr(mmoListValue.EditValue);
                obj[8] = Static.ToInt(cboRequired.EditValue);
                obj[9] = Static.ToInt(numItemLen.EditValue);
                obj[10] = Static.ToStr(txtMask.EditValue);
                obj[11] = Static.ToInt(numOrderNo.EditValue);
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150017, 150017, new object[] { obj, FieldName });
                    msg = "Амжилттай нэмлээ";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150018, 150018, new object[] { obj, OldValue, FieldName, OldValue[1] });
                    msg = "Амжилттай засварлалаа";
                }

                if (res.ResultNo == 0)
                {
                    RefreshParamsData();
                    MessageBox.Show(msg);
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cancel = true;
            }
        }
        #endregion
        #region[Events]
        void ucParams_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (ucParams.FieldValidate(ref err, ref cont) == true)
            {
                SaveParamsData(isnew, ref cancel);
            }
            else
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
        }
        void ucParams_EventEdit(ref bool cancel)
        {
            object[] Value = new object[12];
            Value[0] = Static.ToLong(numDocMID.EditValue);
            Value[1] = Static.ToInt(numParamID.EditValue);
            Value[2] = Static.ToStr(mmoName.EditValue);
            Value[3] = Static.ToStr(mmoDescription.EditValue);
            Value[4] = Static.ToInt(cboDocParamType.EditValue);
            Value[5] = Static.ToStr(txtFormat.EditValue);
            Value[6] = Static.ToStr(mmoValue.EditValue);
            Value[7] = Static.ToStr(mmoListValue.EditValue);
            Value[8] = Static.ToInt(cboRequired.EditValue);
            Value[9] = Static.ToInt(numItemLen.EditValue);
            Value[10] = Static.ToStr(txtMask.EditValue);
            Value[11] = Static.ToInt(numOrderNo.EditValue);
            OldValue = Value;
        }
        void ucParams_EventDelete()
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150019, 150019, new object[] { _docid,Static.ToStr(numParamID.EditValue) });
                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    RefreshParamsData();
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucParams_EventAddAfter()
        {
            numDocMID.EditValue = _docid;
            cboDocParamType.ItemIndex = 0;
            cboRequired.ItemIndex = 0;
        }
        void ucParams_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwParameter);
        }
        #endregion
        private void gvwParameter_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucParams.FieldLinkSetValues();
        }
        #endregion
        #region[Tab4]
        void LoadChainData()
        {
           RefreshSqlConData();
        }
        void RefreshSqlConData()
        {
            Result res = new Result();
            try
            {
                grdSqlCon.DataSource = null;
                int check = 1;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150020, 150020, new object[] { _docid,check});
                if (res.ResultNo == 0||res.ResultNo==9110014)
                {
                    if (res.Data.Tables[0].Rows.Count !=0)
                    {
                        grdSqlCon.DataSource = res.Data.Tables[0];
                        DataRow DRow = gvwSqlCon.GetDataRow(gvwSqlCon.FocusedRowHandle);
                        pItemNo = Static.ToInt(DRow[3]);
                        pTemplateID = Static.ToInt(DRow[0]);
                        SetSqlConData();
                    }
                    else
                    {
                        pItemNo = -1;
                        pTemplateID = -1;
                    }
                    RefreshParamConData(pTemplateID, pItemNo);
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void RefreshParamConData(long pTemplateID,int pItemNo)
        {
            Result res = new Result();
            try
            {
                grdParCon.DataSource = null;
                int check = 0;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150020, 150020, new object[] { pTemplateID,check,pItemNo});
                if (res.ResultNo == 0)
                {
                    if(res.Data.Tables[0].Rows.Count!=0)
                    {
                        grdParCon.DataSource = res.Data.Tables[0];
                        SetParamConData();
                    }
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetSqlConData()
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwSqlCon);
            gvwSqlCon.Columns[0].Caption = "Документын дугаар";
            gvwSqlCon.Columns[1].Caption = "SQL";
            gvwSqlCon.Columns[2].Caption = "Параметр";
            gvwSqlCon.Columns[3].Caption = "SQL -н дугаар";

            gvwSqlCon.Columns[0].OptionsColumn.AllowEdit = false;
            gvwSqlCon.Columns[1].OptionsColumn.AllowEdit = false;
            gvwSqlCon.Columns[2].OptionsColumn.AllowEdit = false;
            gvwSqlCon.Columns[3].OptionsColumn.AllowEdit = false;

        }
        void SetParamConData()
        {
            //            , a.ID, a.PARAMID, a.NAME, a.DESCRIPTION, a.DOCPARAMTYPE, a.FORMAT, a.VALUE, a.LISTVALUE, a.REQUIRED,
            //a.ITEMLEN, a.MASK, a.ORDERNO
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwParCon);
            gvwParCon.Columns[0].Caption = "Төлөв";
            gvwParCon.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwParCon.Columns[1].Caption = "Документын дугаар";
            gvwParCon.Columns[2].Caption = "Параметрын дугаар";
            gvwParCon.Columns[3].Caption = "Нэр";
            gvwParCon.Columns[4].Caption = "Тайлбар";
            gvwParCon.Columns[5].Caption = "Өгөгдлийн төрөл";
            gvwParCon.Columns[6].Caption = "Формат";
            gvwParCon.Columns[7].Caption = "Анхны утга";
            gvwParCon.Columns[8].Caption = "Жагсаалтын өгөгдөл";
            gvwParCon.Columns[9].Caption = "Required";
            gvwParCon.Columns[10].Caption = "Утгын урт";
            gvwParCon.Columns[11].Caption = "Утгын маск";
            gvwParCon.Columns[12].Caption = "Жагсаалтын урт";

            gvwParCon.Columns[1].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[2].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[3].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[4].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[5].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[6].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[7].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[8].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[9].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[10].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[11].OptionsColumn.AllowEdit = false;
            gvwParCon.Columns[12].OptionsColumn.AllowEdit = false;
        }
        void ri_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            string val = "";
            if (e.Value != null)
            {
                val = e.Value.ToString();
            }
            else
            {
                val = "False";
            }
            switch (val)
            {
                case "True":
                    e.CheckState = CheckState.Checked;
                    break;
                case "False":
                    e.CheckState = CheckState.Unchecked;
                    break;
                case "Yes":
                    goto case "True";
                case "No":
                    goto case "False";
                case "1":
                    goto case "True";
                case "0":
                    goto case "False";
                default:
                    e.CheckState = CheckState.Checked;
                    break;
            }
            e.Handled = true;
        }
        public RepositoryItemCheckEdit CreateRepositoryCheckEdit()
        {
            RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(ri)).BeginInit();
            ri.AutoHeight = false;
            ri.AllowFocused = false;
            ri.ValueChecked = 1;
            ri.ValueUnchecked = 0;
            ((System.ComponentModel.ISupportInitialize)(ri)).EndInit();
            ri.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(ri_QueryCheckStateByValue);
            return ri;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {   
            string paramid="";
            if (gvwSqlCon.RowCount != 0 && gvwParameter.RowCount != 0)
            {
                DataTable DT = (DataTable)grdParCon.DataSource;
                foreach (DataRow DR in DT.Rows)
                {
                    if (Static.ToInt(DR["STATUS"]) == 1)
                    {
                        paramid = paramid + Static.ToStr(DR["PARAMID"]) + ",";
                    }
                }
                Result res = new Result();
                object[] obj = new object[5];
                string msg = "";
                try
                {

                    if (paramid != "")
                        paramid = paramid.Substring(0, paramid.Length - 1);
                    object[] FieldName = { "DOCID", "ITEMNO", "PARAMID", };
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 150021, 150021, new object[] { pTemplateID, pItemNo, paramid, FieldName });
                    msg = "Амжилттай хадгаллаа .";
                    if (res.ResultNo == 0)
                    {
                        _docid = pTemplateID;
                        _itemno = pItemNo;
                        RefreshSqlConData();
                        MessageBox.Show(msg);
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("SQl эсвэл Параметр оруулаагүй байна .");
            }
        }
        private void gvwSqlCon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow DRow = gvwSqlCon.GetDataRow(gvwSqlCon.FocusedRowHandle);
            pItemNo = Static.ToInt(DRow[3]);
            pTemplateID = Static.ToInt(DRow[0]);
            RefreshParamConData(pTemplateID,pItemNo);
        }
        #endregion    
    }
}