using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;
using InfoPos.Messages;

namespace InfoPos.Parameter
{
    public partial class CRMIssueProject : XtraForm
    {
        #region [ Хувьсагчууд ]
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        int rowhandle = 0;
        long _projectid;
        FunctionParam fp = new FunctionParam();
        int SelectTxn = 310069;
        int EditTxn = 310072;
        int DeleteTxn = 310073;
        int AddTxn = 310071;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Байгуулагч функц ]
        public CRMIssueProject(InfoPos.Core.Core core,long projectid)
        {
            InitializeComponent();
            _core = core;
            _projectid = projectid;
            InitToggles();
            Init();
            InitCombo();

            
        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            try
            {
                ucProject.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucProject_EventAddAfter);
                ucProject.EventSave += new ucTogglePanel.delegateEventSave(ucProject_EventSave);
                ucProject.EventEdit += new ucTogglePanel.delegateEventEdit(ucProject_EventEdit);
                ucProject.EventDelete += new ucTogglePanel.delegateEventDelete(ucProject_EventDelete);
                ucProject.EventExit += new ucTogglePanel.delegateEventExit(ucProject_EventExit);

                ucProject.Resource = _core.Resource;
                ucProject.FieldContainer = groupControl1.Controls;

                ucProject.FieldLinkAdd("numProjectID", 0, "ProjectID", "", false, true, true);
                ucProject.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
                ucProject.FieldLinkAdd("txtName", 0, "Name", "", true, false);
                ucProject.FieldLinkAdd("txtName2", 0, "Name2", "", false, false);
                ucProject.FieldLinkAdd("txtShortName", 0, "ShortName", "", true, false);
                ucProject.FieldLinkAdd("txtShortName2", 0, "ShortName2", "", false, false);
                ucProject.FieldLinkAdd("txtDescription", 0, "Description", "", true, false);
                ucProject.FieldLinkAdd("cboOwnerUser", 0, "OwnerUser", "", true, false);
                ucProject.FieldLinkAdd("dteStartDate", 0, "StartDate", "", true, false);
                ucProject.FieldLinkAdd("dteEndDate", 0, "EndDate", "", true, false);
                ucProject.FieldLinkAdd("cboProjectTypeID", 0, "ProjectTypeID", "", true, false);
                ucProject.FieldLinkAdd("txtCreateUser", 0, "CreateUserNo", "", true, false, true);
                ucProject.FieldLinkAdd("dteCreateDate", 0, "CreateDate", "", false, false, true);
                ucProject.FieldLinkAdd("numOrderNo", 0, "OrderNo", "", false, false, false);
                ucProject.FieldLinkAdd("cboNotifySchemaID", 0, "NotifySchemaID", "", true, false);
                ucProject.FieldLinkAdd("cboPermSchemaID", 0, "PermSchemaID", "", true, false);               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region [ InitToggles ]
        private void InitToggles()
        {
            try
            {
                ucProject.ToggleShowDelete = true;
                ucProject.ToggleShowEdit = true;
                ucProject.ToggleShowExit = true;
                ucProject.ToggleShowNew = true;
                ucProject.ToggleShowReject = true;
                ucProject.ToggleShowSave = true;

                ucProject.DataSource = null;

                cboNotifySchemaID.ItemIndex = 0;
                cboPermSchemaID.ItemIndex = 0;
                cboStatus.ItemIndex = 0;
                cboOwnerUser.EditValue = _core.RemoteObject.User.UserNo;
                dteStartDate.EditValue = _core.TxnDate;
                dteCreateDate.EditValue = _core.TxnDate;
                dteEndDate.EditValue = _core.TxnDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        private void InitCombo()
        {
            try
            {
                FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Нээлттэй");
                FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Хаалттай");
                fp.SetCombo(_core, "USERS", "USERNO", "USERLNAME", SelectTxn, cboOwnerUser, "", null);
                fp.SetCombo(_core, "NOTIFYSCHEMA", "SchemaID", "NAME", SelectTxn, cboNotifySchemaID, "", null);
                fp.SetCombo(_core, "PERMSCHEMA", "PERMSchemaID", "NAME", SelectTxn, cboPermSchemaID, "", null);
                fp.SetCombo(_core, "PROJECTTYPES", "PROJECTTYPEID", "NAME", SelectTxn, cboProjectTypeID, "", null);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region [ Events ]
        void ucProject_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Устгахдаа итгэлтэй байна уу", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxn, DeleteTxn, new object[] { Static.ToLong(numProjectID.EditValue) });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа.");                        
                        btn = 1;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucProject_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToLong(numProjectID.EditValue),Static.ToInt(cboStatus.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue),Static.ToStr(txtShortName.EditValue),Static.ToStr(txtShortName.EditValue),
                                 Static.ToStr(txtDescription.EditValue), Static.ToInt(cboOwnerUser.EditValue), Static.ToDateTime(dteStartDate.EditValue),Static.ToDateTime(dteEndDate),Static.ToInt(cboProjectTypeID.EditValue),Static.ToInt(txtCreateUser.EditValue),
                                 Static.ToDateTime(dteCreateDate.EditValue), Static.ToInt(numOrderNo.EditValue),Static.ToInt(cboNotifySchemaID.EditValue),Static.ToInt(cboPermSchemaID.EditValue) };
            OldValue = Value;
        }
        void ucProject_EventSave(bool isnew, ref bool cancel)
        {
            try
            {
                Result r;
                string err = "";
                string msg="";
                Control cont = null;
                if (ucProject.FieldValidate(ref err, ref cont))
                {
                    if (Static.ToDateTime(dteStartDate.EditValue) > Static.ToDateTime(dteEndDate.EditValue))
                    {
                        MessageBox.Show("Эхлэх дуусах огноо алдаатай байна .");
                        cancel = true;
                    }
                    else
                    {
                        object[] FieldName = { "PROJECTID", "STATUS", "NAME", "NAME2", "SHORTNAME", "SHORTNAME2", "DESCRIPTION", "OWNERUSER", "STARTDATE", "ENDDATE", "PROJECTYPEID", "CREATEUSER", "CREATEDATE", "ORDERNO", "NOTIFYSCHEMAID", "PERMSCHEMAID" };
                        object[] NewValue = { Static.ToLong(numProjectID.EditValue),Static.ToInt(cboStatus.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue),Static.ToStr(txtShortName.EditValue),Static.ToStr(txtShortName2.EditValue),
                                Static.ToStr(txtDescription.EditValue), Static.ToInt(cboOwnerUser.EditValue), Static.ToDateTime(dteStartDate.EditValue),Static.ToDateTime(dteEndDate.EditValue),Static.ToInt(cboProjectTypeID.EditValue),Static.ToInt(txtCreateUser.EditValue),
                                Static.ToDate(dteCreateDate.EditValue), Static.ToInt(numOrderNo.EditValue),Static.ToInt(cboNotifySchemaID.EditValue),Static.ToInt(cboPermSchemaID.EditValue) };
                        if (!isnew)
                        {
                            r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxn, EditTxn, new object[] { NewValue, OldValue, FieldName });
                            msg = "Амжилттай хадгаллаа.";
                        }
                        else
                        {
                            r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxn, AddTxn, new object[] { NewValue, FieldName });
                            msg = "Амжилттай хадгаллаа.";
                        }
                        if (r.ResultNo != 0)
                        {
                            MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        }
                        else
                        {
                            MessageBox.Show(msg);
                            if (isnew)
                            {
                                object[] obj = new object[14];

                                obj = r.Param;
                                numProjectID.EditValue = Static.ToLong(obj[0]);
                                dteCreateDate.EditValue = obj[12];
                                txtCreateUser.EditValue = obj[11];
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(err);
                    cont.Select();
                    cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucProject_EventAddAfter()
        {
            
        }
        void ucProject_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        #endregion
        #region[FormEvent]
        private void CRMIssueProject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        private void CRMIssueProject_Load(object sender, EventArgs e)
        {
            if (_projectid != 0)
            {
                RefreshData(_projectid);
                ucProject.FieldLinkSetEditState();
            }
            else
            {
                ucProject.FieldLinkSetNewState();
            }
            dteCreateDate.EditValue = _core.TxnDate;
            txtCreateUser.EditValue = _core.RemoteObject.User.UserNo;
            cboStatus.ItemIndex = 0;
            object[] Value = { Static.ToLong(numProjectID.EditValue),Static.ToInt(cboStatus.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue),Static.ToStr(txtShortName.EditValue),Static.ToStr(txtShortName.EditValue),
                               Static.ToStr(txtDescription.EditValue), Static.ToInt(cboOwnerUser.EditValue), Static.ToDateTime(dteStartDate.EditValue),Static.ToDateTime(dteEndDate),Static.ToInt(cboProjectTypeID.EditValue),Static.ToInt(txtCreateUser.EditValue),
                               Static.ToDateTime(dteCreateDate.EditValue), Static.ToInt(numOrderNo.EditValue),Static.ToInt(cboNotifySchemaID.EditValue),Static.ToInt(cboPermSchemaID.EditValue) };
            OldValue = Value;

            cboNotifySchemaID.ItemIndex = 0;
            cboPermSchemaID.ItemIndex = 0;
            cboStatus.ItemIndex = 0;
            cboProjectTypeID.ItemIndex = 0;
            cboOwnerUser.EditValue = _core.RemoteObject.User.UserNo;          
            dteStartDate.EditValue = _core.TxnDate;
            dteCreateDate.EditValue = _core.TxnDate;
            dteEndDate.EditValue = _core.TxnDate;

            Form FormName=this;
            FormUtility.RestoreStateForm(appname, ref FormName);
        }
        #endregion 
        void RefreshData(long _projectid)   
        {
            Result res = new Result();
            try
            {
                if (_projectid != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 310070, 310070, new object[] { _projectid });

                    if (res.ResultNo == 0)
                    {
                        ucProject.DataSource = res.Data;
                        ucProject.FieldLinkSetValues();
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CRMIssueProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
        }
    }
}