using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISM.Template;
using EServ.Shared;

namespace InfoPos.Issue
{
    public partial class ucIssueTxn : DevExpress.XtraEditors.XtraUserControl
    {
        #region[ Хувьсагч ]
        public delegate void dlgDoTxn();
        public event dlgDoTxn DoTran;

        string _Status = "";
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        string _OldStatus="";

        public InfoPos.Core.Core Core;
        public object[] SaveObj;

        int _StatusM;
        long _issueid = 0;
        long AttachID = 0;
        public bool AttachCheck = false;
        object[] AttachObj = null;
        int cboStatus = 0;
        int _userno;
        int _track = 0;
        #endregion

        public ucIssueTxn()
        {
            InitializeComponent();
        }
        private void ucIssueTxn_Load(object sender, EventArgs e)
        {
            this.Size = this.MinimumSize;
            btnExCo.Image = Issue.Resource.down_icon;
        }

        #region[ Init Combo ]
        public void InitCombos(DataSet Tables)
        {
            Result res = new Result();
            DataTable DT = null;
            string msg = "";

            DT = Tables.Tables[0];
            if (DT != null)
                FormUtility.LookUpEdit_SetList(ref cboTrackID, DT, "ISSUETRACKID", "NAME", "", new int[] { });

            DT = Tables.Tables[1];
            if (DT != null)
                FormUtility.LookUpEdit_SetList(ref cboResolutionTypeID, DT, "RESOLUTIONTYPEID", "NAME", "", new int[] { });

            DT = Tables.Tables[2];
            if (DT != null)
                FormUtility.LookUpEdit_SetList(ref cboContactType, DT, "TYPECODE", "NAME", "", new int[] { });
            //1-Comment
            //11-Progress
            //12-ReOpen
            //13-Resolve
            //19-Closed
        }
        #endregion

        private void btnExCo_Click(object sender, EventArgs e)
        {
            if (this.Size == new Size(408, 285))
            {
                this.Size = this.MinimumSize;
                btnExCo.Image = Issue.Resource.down_icon;
                lblStatus.Text = _OldStatus;
            }
            else
            {
                this.Size = new Size(408, 285);
                btnExCo.Image = Issue.Resource.up_icon;
                cboType.EditValue = 1;
                checkReport.Checked = false;
                _OldStatus = lblStatus.Text;

                txtSubject.EditValue=null;
                mmeComment.EditValue=null;
                cboContactType.EditValue=null;
                cboResolutionTypeID.EditValue=null;
                mmeNextPurpose.EditValue=null;
                dteNextDate.EditValue = null;
            }
        }

        /*------------------------------------------*/
        private void nbcIssueTxn_GroupExpanded(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if (checkReport.Checked == false)
            {
                nbgNext.Expanded = false;
            }
        }
        private void nbcIssueTxn_GroupCollapsed(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if (checkReport.Checked == false)
            {
                nbgNext.Expanded = false;
            }
        }

        private void checkReport_CheckedChanged(object sender, EventArgs e)
        {
            if (checkReport.Checked == true)
            {
                mmeNextPurpose.Enabled = true;
                dteNextDate.Enabled = true;
            }
            else
            {
                mmeNextPurpose.Enabled = false;
                dteNextDate.Enabled = false;
                mmeNextPurpose.EditValue = null;
                dteNextDate.EditValue = null;
                FunctionErrorCheck(0, mmeNextPurpose.ToolTipTitle, mmeNextPurpose);
                FunctionErrorCheck(0, dteNextDate.ToolTipTitle, dteNextDate);
                nbgNext.Expanded = false;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Size = this.MinimumSize;
            lblStatus.Text = _OldStatus;
            btnExCo.Image = Issue.Resource.down_icon;
        }
        /*------------------------------------------*/

        #region[ USER FUNCTION ]

        public void ChangeValue(string Value)
        {
            Status = Value;
            lblStatus.Text = Value;
            TxnData(Value);
        }
        public void ChangeValue(string Value, int AssignUser, int Track, long IssueID,int StatusM)
        {
            lblStatus.Text = Value;
            Status = Value;
            TxnData(Value);
            _userno = AssignUser;
            _track = Track;
            _issueid = IssueID;
            _StatusM = StatusM;
            ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboTrackID, _track);
        }


        void TxnComment()
        {
            cboType.Enabled = true;
            txtSubject.Enabled = true;
            mmeComment.Enabled = true;
            cboTrackID.Enabled = true;

            cboResolutionTypeID.Enabled = false;
        }
        void TxnReOpen()
        {
            cboType.Enabled = true;
            txtSubject.Enabled = true;
            mmeComment.Enabled = true;
            cboTrackID.Enabled = true;

            cboResolutionTypeID.Enabled = false;
        }
        void TxnReSolved()
        {
            cboType.Enabled = true;
            txtSubject.Enabled = true;
            mmeComment.Enabled = true;
            cboTrackID.Enabled = true;

            cboResolutionTypeID.Enabled = false;
        }
        void TxnClosed()
        {
            cboType.Enabled = true;
            txtSubject.Enabled = true;
            mmeComment.Enabled = true;
            cboTrackID.Enabled = true;

            cboResolutionTypeID.Enabled = true;
        }
        void TxnData(string Txn)
        {
            switch (Txn.ToUpper())
            {
                case "OPEN":
                    {
                        cboType.Properties.DataSource = null;
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "COMMENT");
                        FormUtility.LookUpEdit_SetList(ref cboType, 13, "RESOLVE");
                        FormUtility.LookUpEdit_SetList(ref cboType, 19, "CLOSE");
                    }
                    break;
                case "CLOSED":
                    {
                        cboType.Properties.DataSource = null;
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "COMMENT");
                        FormUtility.LookUpEdit_SetList(ref cboType, 12, "REOPEN");
                    }
                    break;
                case "REOPEN":
                    {
                        cboType.Properties.DataSource = null;
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "COMMENT");
                        FormUtility.LookUpEdit_SetList(ref cboType, 13, "RESOLVE");
                        FormUtility.LookUpEdit_SetList(ref cboType, 19, "CLOSE");
                    }
                    break;
                case "RESOLVED":
                    {
                        cboType.Properties.DataSource = null;
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "COMMENT");
                        FormUtility.LookUpEdit_SetList(ref cboType, 12, "REOPEN");
                        FormUtility.LookUpEdit_SetList(ref cboType, 19, "CLOSE");
                    }
                    break;
                case "INPROGRESS":
                    {
                        cboType.Properties.DataSource = null;
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "COMMENT");
                        FormUtility.LookUpEdit_SetList(ref cboType, 13, "RESOLVE");
                    }
                    break;

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
        private bool ValidateIssueTxn()
        {
            bool res = true;
            try
            {
                if (cboType.Text.Trim() == "") { res = false; FunctionErrorCheck(1, cboType.ToolTipTitle, cboType); }
                else FunctionErrorCheck(0, cboType.ToolTipTitle, cboType);

                if (cboTrackID.Text.Trim() == "") { res = false; FunctionErrorCheck(1, cboTrackID.ToolTipTitle, cboTrackID); }
                else FunctionErrorCheck(0, cboTrackID.ToolTipTitle, cboTrackID);
                if (cboStatus == 19)
                {
                    if (cboResolutionTypeID.Text.Trim() == "") { res = false; FunctionErrorCheck(1, cboResolutionTypeID.ToolTipTitle, cboResolutionTypeID); }
                    else FunctionErrorCheck(0, cboResolutionTypeID.ToolTipTitle, cboResolutionTypeID);
                }
                else
                {
                    FunctionErrorCheck(0, cboResolutionTypeID.ToolTipTitle, cboResolutionTypeID);
                }
             
                if (checkReport.Checked == true)
                {
                    if (mmeNextPurpose.Text.Trim() == "") { res = false; FunctionErrorCheck(1, mmeNextPurpose.ToolTipTitle, mmeNextPurpose); }
                    else FunctionErrorCheck(0, mmeNextPurpose.ToolTipTitle, mmeNextPurpose);

                    if (dteNextDate.Text.Trim() == "") { res = false; FunctionErrorCheck(1, dteNextDate.ToolTipTitle, dteNextDate); }
                    else FunctionErrorCheck(0, dteNextDate.ToolTipTitle, dteNextDate);
                }
                else
                {
                    FunctionErrorCheck(0, mmeNextPurpose.ToolTipTitle, mmeNextPurpose);
                    FunctionErrorCheck(0, dteNextDate.ToolTipTitle, dteNextDate);
                }

                return res;
            }
            catch (Exception ex)
            {
                res = false;
                return res;
            }
        }
        #endregion

        private void cboType_EditValueChanged(object sender, EventArgs e)
        {
            //1-Comment
            //11-Progress
            //12-ReOpen
            //13-Resolve
            //19-Closed
            switch (Static.ToInt(cboType.EditValue))
            {
                case 1:
                    {
                        TxnComment();
                    }
                    break;
                case 12:
                    {
                        TxnReOpen();
                    }
                    break;
                case 13:
                    {
                        TxnReSolved();
                    }
                    break;
                case 19:
                    {
                        TxnClosed();
                    }
                    break;
            }
            cboStatus = Static.ToInt(cboType.EditValue);
        }
        private void btnFile_Click(object sender, EventArgs e)
        {
            ISM.Template.FormAttachViewFileAdd frm = new FormAttachViewFileAdd(Core.RemoteObject, 300, Static.ToStr(_issueid), 310106);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                if (frm.Result != null)
                {
                    string s = "";
                    if (frm.Result.ResultNo == 0)
                    {
                        ulong attachid = ISM.Lib.Static.ToULong(frm.Result.Param[0]);
                        s = string.Format("Файл амжилттай хадгаллаа.\r\nХавсралт файлын дугаар: {0}", attachid);
                        AttachID = Static.ToLong(attachid);
                        AttachObj = new object[4];
                        AttachObj[0] = AttachID;
                        AttachObj[1] = Static.ToInt(frm.radType.EditValue);
                        AttachObj[2] = Static.ToStr(frm.txtFileName.EditValue);
                        AttachObj[3] = Static.ToStr(frm.txtDesc.EditValue);
                    }
                    else
                    {
                        s = string.Format("{0}: {1}", frm.Result.ResultNo, frm.Result.ResultDesc);
                        AttachID = 0;
                        AttachObj = null;
                    }
                    MessageBox.Show(s, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                AttachCheck = true;
            }
            else
            {
                AttachCheck = false;
            }
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (ValidateIssueTxn())
            {
                #region[ Save ]

                #region[ TxnObject ]
                object[] obj = new object[15];
                obj[0] = 0;
                obj[1] = _issueid;                              //IssueID
                obj[2] = Core.TxnDate;                         //TxnDate
                obj[3] = Core.TxnDate;                         //PostDate
                obj[4] = Core.RemoteObject.User.UserNo;                     //UserNo
                obj[5] = Static.ToInt(cboType.EditValue);       //ActionTypeID
                obj[6] = Static.ToStr(txtSubject.EditValue);    //Subject
                obj[7] = Static.ToStr(mmeComment.EditValue);    //Description

                if (Static.ToInt(cboType.EditValue) == 19)
                    obj[8] = 9;                                 //Status
                else
                    obj[8] = 0;                                 //Status
                obj[9] = Static.ToStr(cboResolutionTypeID.EditValue);     //ResolutionTypeID
                obj[10] = Static.ToInt(cboTrackID.EditValue);              //TrackID
                obj[11] = _userno;                                    //AssigneeUser
                obj[12] = Static.ToStr(mmeNextPurpose.EditValue);                 //NextPurpose
                obj[13] = Static.ToDate(dteNextDate.EditValue);                //NextDate
                obj[14] = Static.ToInt(cboContactType.EditValue);                //ContactType
                #endregion
                #region[ Attach Object ]
                object[] AttachTxn = new object[7];
                if (AttachCheck)
                {
                    if (AttachID != 0)
                    {
                        AttachTxn[0] = 0;            //IssueTxnID
                        AttachTxn[1] = Static.ToLong(AttachObj[0]); //AttachID
                        AttachTxn[2] = _issueid;     //IssueID
                        AttachTxn[3] = Static.ToStr(AttachObj[2]); //FileName
                        AttachTxn[4] = Core.TxnDate;//CreateDate
                        AttachTxn[5] = Static.ToStr(AttachObj[3]); //Description
                        AttachTxn[6] = Static.ToInt(AttachObj[1]); //FileType
                    }
                }
                if (AttachID == 0 && AttachCheck == true)
                {
                    MessageBox.Show("Attach хийхэд алдаа гарсан байна .", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                if (_issueid != 0)
                {
                    SaveObj = new object[] { obj, AttachCheck, AttachTxn, _StatusM };
                    DoTran();
                    if (this.Size != this.MinimumSize)
                    {
                        this.Size = this.MinimumSize;
                        btnExCo.Image = Issue.Resource.down_icon;
                        lblStatus.Text = _OldStatus;
                    }
                    else
                    {
                        btnExCo.Image = Issue.Resource.down_icon;
                    }
                }
                #endregion
                AttachID = 0;
                AttachCheck = false;
            }
        }
        private void ucIssueTxn_Leave(object sender, EventArgs e)
        {
            if (this.Size != this.MinimumSize)
            {
                this.Size = this.MinimumSize;
                btnExCo.Image = Issue.Resource.down_icon;
                lblStatus.Text = _OldStatus;
            }
        }
    }
}
