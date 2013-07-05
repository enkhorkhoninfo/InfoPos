using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Issue
{
    public partial class FormIssueTrack : DevExpress.XtraEditors.XtraForm
    {
        #region[ Хувьсагч ]
        InfoPos.Core.Core _core;
        long _issueid;
        int FileID=223;
        long _projectid=0;
        public Result ResMain = null;
        #endregion
        #region[ Load and Байгуулагч функц ]
        public FormIssueTrack(InfoPos.Core.Core core,long issueid,long ProjectID)
        {
            _core = core;
            _issueid = issueid;
            _projectid = ProjectID;
            InitializeComponent();
        }
        private void FormIssue_Load(object sender, EventArgs e)
        {
            this.Show();
            Init();
            ucToggleIssue.Resource = _core.Resource;
            if (_issueid == 0)
            {
                ucToggleIssue.FieldLinkSetNewState();
                cboStatus.EditValue = 0;
                txtCreateUser.EditValue = _core.RemoteObject.User.UserNo;
                cboAssigneeUser.EditValue = _core.RemoteObject.User.UserNo;
                dteCreateDate.EditValue = DateTime.Now;
                cboProjectID.EditValue = _projectid;

                cboProjectID.ItemIndex = 0;
                cboProjectCompID.ItemIndex=0;
                cboIssueTypeID.ItemIndex=0;
                cboIssuePriorID.ItemIndex=0;
                cboStatus.ItemIndex=0;
                cboTrackID.ItemIndex = 0;
            }
            else
            {
                RefreshData(_issueid);
                ucToggleIssue.FieldLinkSetEditState();
            }
            Form FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(_core.ApplicationName, ref FormName);
        }
        #endregion
        #region[ Init Function ]
        private void Init()
        {
            InitEvents();
            InitToggles();
            InitData();
            InitCombos();
        }
        private void InitEvents()
        {
            ucToggleIssue.EventSave += new ucTogglePanel.delegateEventSave(ucToggleIssue_EventSave);
            ucToggleIssue.EventDelete += new ucTogglePanel.delegateEventDelete(ucToggleIssue_EventDelete);
            ucToggleIssue.EventExit += new ucTogglePanel.delegateEventExit(ucToggleIssue_EventExit);
            ucToggleIssue.EventAdd += new ucTogglePanel.delegateEventAdd(ucToggleIssue_EventAdd);
            ucToggleIssue.EventEdit += new ucTogglePanel.delegateEventEdit(ucToggleIssue_EventEdit);
            ucToggleIssue.EventReject += new ucTogglePanel.delegateEventReject(ucToggleIssue_EventReject);
        }
        private void InitCombos()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";

            DictUtility.PrivNo = 200051;

            string[] names = new string[] { "ISSUEPRIORITY", "ISSUETYPES", "ISSUEPROJECT", "PROJECTCOMP", "ISSUERESOLUTIONTYPE", "ISSUETRACKS", "USERS" };
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

            DT = (DataTable)Tables[0];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUEPRIORITY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboIssuePriorID, DT, "ISSUEPRIORID", "NAME", "",new int[]{});
            }

            DT = (DataTable)Tables[1];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUETYPES оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboIssueTypeID, DT, "ISSUETYPEID", "NAME", "", new int[] { });
            }

            DT = (DataTable)Tables[2];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUEPROJECT оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboProjectID, DT, "PROJECTID", "NAME", "", new int[] { });
            }

            DT = (DataTable)Tables[3];
            if (DT == null)
            {
                msg = "Dictionary-д PROJECTCOMP оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboProjectCompID, DT, "PROJECTCOMPID", "NAME", "", new int[] { });
            }

            DT = (DataTable)Tables[4];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUERESOLUTIONTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboResolutionStatus, DT, "RESOLUTIONTYPEID", "NAME", "", new int[] { });
            }

            DT = (DataTable)Tables[5];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUETRACKS оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboTrackID, DT, "ISSUETRACKID", "NAME", "", new int[] { });
            }

            DT = (DataTable)Tables[6];
            if (DT == null)
            {
                msg = "Dictionary-д USERS оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboAssigneeUser, DT, "USERNO", "USERLNAME", "", new int[] { });
            }

            //0 - Open 1 – InProgress 2 - ReOpen 3 - ReSolved 9 – Closed

            FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Open");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "InProgress");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 2, "ReOpen");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 3, "ReSolved");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 9, "Closed");

        }
        private void InitToggles()
        {
            try
            {
                #region [ Issue General ]
                ucToggleIssue.ToggleShowDelete = true;
                ucToggleIssue.ToggleShowEdit = true;
                ucToggleIssue.ToggleShowExit = true;
                ucToggleIssue.ToggleShowNew = true;
                ucToggleIssue.ToggleShowReject = true;
                ucToggleIssue.ToggleShowSave = true;
                ucToggleIssue.DataSource = null;
                ucToggleIssue.FieldLinkSetSaveState();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitData()
        {
            try
            {
                #region [ Issue General ]
                ucToggleIssue.FieldLinkAdd("txtIssueID", 0, "IssueID", "",false, true, true);
                ucToggleIssue.FieldLinkAdd("cboProjectID", 0, "ProjectID", "", true, false,true);
                ucToggleIssue.FieldLinkAdd("cboProjectCompID", 0, "ProjectCompID", "", true, false);
                ucToggleIssue.FieldLinkAdd("cboIssueTypeID", 0, "IssueTypeID", "", true, false);
                ucToggleIssue.FieldLinkAdd("cboIssuePriorID", 0, "IssuePriorID", "", true, false);
                ucToggleIssue.FieldLinkAdd("mmeSubject", 0, "Subject", "", true, false);
                ucToggleIssue.FieldLinkAdd("mmeDescription", 0, "Description", "", true, false);
                ucToggleIssue.FieldLinkAdd("cboStatus", 0, "Status", "", false, false,true);
                ucToggleIssue.FieldLinkAdd("cboResolutionStatus", 0, "ResolutionStatus", "", false, false,true);
                ucToggleIssue.FieldLinkAdd("cboTrackID", 0, "TrackID", "", true, false);
                ucToggleIssue.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false,true);
                ucToggleIssue.FieldLinkAdd("dteCreateDate", 0, "CreateDate", "", true, false);
                ucToggleIssue.FieldLinkAdd("dteUpdateDate", 0, "UpdateDate", "", false, false, true);
                ucToggleIssue.FieldLinkAdd("dteDueDate", 0, "DueDate", "", true, false);
                ucToggleIssue.FieldLinkAdd("cboAssigneeUser", 0, "AssigneeUser", "", true, false);
                ucToggleIssue.FieldLinkAdd("dteResolutionDate", 0, "ResolutionDate", "", false, false, true);
                ucToggleIssue.FieldLinkAdd("txtResolutionUser", 0, "ResolutionUser", "", false, false, true);
                //Энд нэмэгдвэл Vote нэмэгдэж болно .
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[ Function ]

        //Асуудалгүй
        private void SaveIssueData(bool isnew, ref bool cancel)
        {
            try
            {
                Result res = new Result();
                string msg = "";
                

                if (isnew)
                {
                    #region[ Value ]
                    Object[] NewValue = new object[14];
                    //ISSUEID,PROJECTID,PROJECTCOMPID,ISSUETYPEID,ISSUEPRIORID,SUBJECT,DESCRIPTION,STATUS,RESOLUTIONSTATUS,TRACKID,CREATEUSER,CREATEDATE,DUEDATE,ASSIGNEEUSER
                    NewValue[0] = Static.ToLong(txtIssueID.EditValue);
                    NewValue[1] = Static.ToLong(cboProjectID.EditValue);
                    NewValue[2] = Static.ToInt(cboProjectCompID.EditValue);
                    NewValue[3] = Static.ToInt(cboIssueTypeID.EditValue);
                    NewValue[4] = Static.ToInt(cboIssuePriorID.EditValue);
                    NewValue[5] = Static.ToStr(mmeSubject.EditValue);
                    NewValue[6] = Static.ToStr(mmeDescription.EditValue);
                    NewValue[7] = Static.ToInt(cboStatus.EditValue);
                    NewValue[8] = Static.ToInt(cboResolutionStatus.EditValue);
                    NewValue[9] = Static.ToInt(cboTrackID.EditValue);
                    NewValue[10] = Static.ToLong(_core.RemoteObject.User.UserNo);
                    NewValue[11] = Static.ToDateTime(dteCreateDate.EditValue);
                    NewValue[12] = Static.ToDateTime(dteDueDate.EditValue);
                    NewValue[13] = Static.ToLong(cboAssigneeUser.EditValue);

                    #endregion
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310124, 310124, new object[] { NewValue });
                    ResMain = res;
                    msg = "Амжилттай нэмлээ";
                }
                else
                {
                    #region[ Value ]

                    Object[] NewValue = new object[17];

                    NewValue[0] = Static.ToLong(txtIssueID.EditValue);
                    NewValue[1] = Static.ToLong(cboProjectID.EditValue);
                    NewValue[2] = Static.ToInt(cboProjectCompID.EditValue);
                    NewValue[3] = Static.ToInt(cboIssueTypeID.EditValue);
                    NewValue[4] = Static.ToInt(cboIssuePriorID.EditValue);
                    NewValue[5] = Static.ToStr(mmeSubject.EditValue);
                    NewValue[6] = Static.ToStr(mmeDescription.EditValue);
                    NewValue[7] = Static.ToInt(cboStatus.EditValue);
                    NewValue[8] = Static.ToInt(cboResolutionStatus.EditValue);
                    NewValue[9] = Static.ToInt(cboTrackID.EditValue);
                    NewValue[10] = Static.ToLong(txtCreateUser.EditValue);
                    NewValue[11] = Static.ToDateTime(dteCreateDate.EditValue);
                    NewValue[12] = Static.ToDateTime(dteUpdateDate.EditValue);
                    NewValue[13] = Static.ToDateTime(dteDueDate.EditValue);
                    NewValue[14] = Static.ToLong(cboAssigneeUser.EditValue);
                    NewValue[15] = Static.ToDateTime(dteResolutionDate.EditValue);
                    NewValue[16] = Static.ToLong(txtResolutionUser.EditValue);
                    #endregion
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310125, 310125, new object[] { NewValue });
                    ResMain = res;
                    msg = "Амжилттай засварлалаа";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg,"Мэдээлэл",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
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
        void RefreshData(long issueid)
        {
            Result res = new Result();
            try
            {
                if (issueid != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310080, 310080, new object[] { issueid });
                    if (res.ResultNo == 0)
                    {
                        ucToggleIssue.DataSource = res.Data;
                        ucToggleIssue.FieldLinkSetValues();
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
        void DeleteIssue()
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                long IssueID = Static.ToLong(txtIssueID.Text);
                if (IssueID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310126, 310126, new object[] { IssueID });

                    if (res.ResultNo == 0)
                    {
                        ucToggleIssue.FieldLinkSetNewState();
                        MessageBox.Show("Амжилттай устгагдлаа");
                        this.Close();
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

        void FunctionErrorCheck(int type, string msj, Control Con)
        {
            if (type == 1)
                ErrorChecker.SetError(Con, msj, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
            else
                ErrorChecker.SetError(Con, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
        }
        void ClearError()
        {
            ErrorChecker.ClearErrors();
        }
        private bool ValidateIssueGen()
        {
            bool res = true;
            try
            {
                if (cboProjectID.Text.Trim() == "-") { res = false; FunctionErrorCheck(1, cboProjectID.ToolTipTitle, cboProjectID); }
                else FunctionErrorCheck(0, cboProjectID.ToolTipTitle, cboProjectID);

                if (cboProjectCompID.Text.Trim() == "-") { res = false; FunctionErrorCheck(1, cboProjectCompID.ToolTipTitle, cboProjectCompID); }
                else FunctionErrorCheck(0, cboProjectCompID.ToolTipTitle, cboProjectCompID);

                if (cboIssueTypeID.Text.Trim() == "-") { res = false; FunctionErrorCheck(1, cboIssueTypeID.ToolTipTitle, cboIssueTypeID); }
                else FunctionErrorCheck(0, cboIssueTypeID.ToolTipTitle, cboIssueTypeID);

                if (cboIssuePriorID.Text.Trim() == "-") { res = false; FunctionErrorCheck(1, cboIssuePriorID.ToolTipTitle, cboIssuePriorID); }
                else FunctionErrorCheck(0, cboIssuePriorID.ToolTipTitle, cboIssuePriorID);

                if (mmeSubject.Text.Trim() == "") { res = false; FunctionErrorCheck(1, mmeSubject.ToolTipTitle, mmeSubject); }
                else FunctionErrorCheck(0, mmeSubject.ToolTipTitle, mmeSubject);

                if (mmeDescription.Text.Trim() == "") { res = false; FunctionErrorCheck(1, mmeDescription.ToolTipTitle, mmeDescription); }
                else FunctionErrorCheck(0, mmeDescription.ToolTipTitle, mmeDescription);

                if (cboTrackID.Text.Trim() == "-") { res = false; FunctionErrorCheck(1, cboTrackID.ToolTipTitle, cboTrackID); }
                else FunctionErrorCheck(0, cboTrackID.ToolTipTitle, cboTrackID);

                if (dteDueDate.Text.Trim() == "") { res = false; FunctionErrorCheck(1, dteDueDate.ToolTipTitle, dteDueDate); }
                else FunctionErrorCheck(0, dteDueDate.ToolTipTitle, dteDueDate);

                if (cboAssigneeUser.Text.Trim() == "-") { res = false; FunctionErrorCheck(1, cboAssigneeUser.ToolTipTitle, cboAssigneeUser); }
                else FunctionErrorCheck(0, cboAssigneeUser.ToolTipTitle, cboAssigneeUser);

                if (dteCreateDate.Text.Trim() == "") { res = false; FunctionErrorCheck(1, dteCreateDate.ToolTipTitle, dteCreateDate); }
                else FunctionErrorCheck(0, dteCreateDate.ToolTipTitle, dteCreateDate);

                return res;
            }
            catch (Exception ex)
            {
                res = false;
                return res;
            }
        }

        #endregion
        #region[ Event ]
        void ucToggleIssue_EventReject()
        {
            ClearError();
        }
        void ucToggleIssue_EventEdit(ref bool cancel)
        {

        }
        void ucToggleIssue_EventAdd(ref bool cancel)
        {

        }
        void ucToggleIssue_EventExit(bool editing, ref bool cancel)
        {

        }
        void ucToggleIssue_EventDelete()
        {
            DeleteIssue();
        }
        void ucToggleIssue_EventSave(bool isnew, ref bool cancel)
        {
            if (ValidateIssueGen() == true)
            {
                if (dteCreateDate.DateTime <= dteDueDate.DateTime)
                {
                    SaveIssueData(isnew, ref cancel);
                }
                else
                {
                    MessageBox.Show("Эхлэх дуусах огноо алдаатай байна");
                    cancel = true;
                }
            }
            else
            {
                cancel = true;
            }
            
        }
        private void FormIssue_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }
        #endregion
    }
}