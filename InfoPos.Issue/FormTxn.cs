using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISM.Template;
using EServ.Shared;


namespace InfoPos.Issue
{
    public partial class FormTxn : DevExpress.XtraEditors.XtraForm
    {
        #region[ Хувьсагч ] 
        InfoPos.Core.Core _core;
        long _issueid=0;
        int FileID = 223;
        long AttachID=0;
        public bool AttachCheck = false;
        object[] AttachObj=null;
        int _Status;
        string status;
        public Result ResMain = null;
        int cboStatus = 0;
        int _userno;
        int _track=0;
        #endregion
        #region[ Constructor and Load Function ]
        public FormTxn(InfoPos.Core.Core core,long issueid,int Status,int userno,int track)
        {
            InitializeComponent();
            _core = core;
            _issueid = issueid;
            _Status = Status;
            _userno = userno;
            _track = track;
            switch (_Status)
            {
                case 0:
                    {
                        status = "Open";
                    }
                    break;
                case 9:
                    {
                        status = "Closed";
                    }
                    break;
                case 2:
                    {
                        status = "ReOpen";
                    }
                    break;
                case 3:
                    {
                        status = "ReSolved";
                    }
                    break;
                case 1:
                    {
                        status = "InProgress";
                    }
                    break;
            }
            
        }
        private void FormTxn_Load(object sender, EventArgs e)
        {
            InitCombos();
            TxnData(status);
            ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboTrackID, _track);
            #region[ Image ]
            btnExit.Image = _core.Resource.GetImage("navigate_cancel");
            btnEnter.Image = _core.Resource.GetImage("image_ok");
            btnFile.Image = _core.Resource.GetImage("image_folder");
            #endregion
            Form FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(_core.ApplicationName, ref FormName);
            cboType.ItemIndex = 0;
        }
        #endregion
        #region[ Init Combo ]
        private void InitCombos()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";
            DictUtility.PrivNo = 200051;

            string[] names = new string[] { "ISSUETRACKS", "ISSUERESOLUTIONTYPE", "CUSTCONTACTTYPE" };
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

            DT = (DataTable)Tables[0];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUETRACKS оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboTrackID, DT, "ISSUETRACKID", "NAME", "", new int[] { });
            }

            DT = (DataTable)Tables[1];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUERESOLUTIONTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboResolutionTypeID, DT, "RESOLUTIONTYPEID", "NAME", "", new int[] { });
            }

            DT = (DataTable)Tables[2];
            if (DT == null)
            {
                msg = "Dictionary-д CUSTCONTACTTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboContactType, DT, "TYPECODE", "NAME", "", new int[] { });
            }
                //1-Comment
                //11-Progress
                //12-ReOpen
                //13-Resolve
                //19-Closed
        }
        #endregion
        #region[ Btn and Event ]
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
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (ValidateIssueTxn())
            {
                #region[ Save ]
                //JRNO, IssueID, TxnDate, PostDate, UserNo, ActionTypeID, Subject, Description, Status, ResolutionTypeID,
                //TrackID, AssigneeUser, NextPurpose, NextDate

                #region[ TxnObject ]
                object[] obj = new object[15];
                obj[0] = 0;
                obj[1] = _issueid;                              //IssueID
                obj[2] = _core.TxnDate;                         //TxnDate
                obj[3] = _core.TxnDate;                         //PostDate
                obj[4] = _core.RemoteObject.User.UserNo;                     //UserNo
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
                //IssueTxnID,AttachID,IssueID,FileName,CreateDate,Description,FileType
                //AttachID-0,AttachType-1,AttachFileName-2,AttachDescription-3

                object[] AttachTxn = new object[7];
                if (AttachCheck)
                {
                    if (AttachID != 0)
                    {
                        AttachTxn[0] = 0;            //IssueTxnID
                        AttachTxn[1] = Static.ToLong(AttachObj[0]); //AttachID
                        AttachTxn[2] = _issueid;     //IssueID
                        AttachTxn[3] = Static.ToStr(AttachObj[2]); //FileName
                        AttachTxn[4] = _core.TxnDate;//CreateDate
                        AttachTxn[5] = Static.ToStr(AttachObj[3]); //Description
                        AttachTxn[6] = Static.ToInt(AttachObj[1]); //FileType
                    }
                }
                if (AttachID == 0 && AttachCheck==true)
                {
                    MessageBox.Show("Attach хийхэд алдаа гарсан байна .", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Result res = new Result();
                try
                {

                    if (_issueid != 0)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310106, 310106, new object[] { obj, AttachCheck, AttachTxn,_Status});
                        ResMain = res;
                        if (res.ResultNo == 0)
                        {
                            MessageBox.Show("Амжилттай хадгалагдлаа .");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc,"Алдаа",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                #endregion
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            ISM.Template.FormAttachViewFileAdd frm = new FormAttachViewFileAdd(_core.RemoteObject, 300, Static.ToStr(_issueid), 310106);
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
                        //cboContactType.Properties.ReadOnly = false;
                    }
                    break;
                case 12:
                    { 
                        TxnReOpen();
                        //cboContactType.Properties.ReadOnly = true;
                    }
                    break;
                case 13:
                    { 
                        TxnReSolved();
                        //cboContactType.Properties.ReadOnly = true;
                    }
                    break;
                case 19:
                    { 
                        TxnClosed();
                        //cboContactType.Properties.ReadOnly = true;
                    }
                    break;
            }
            cboStatus = Static.ToInt(cboType.EditValue);
        }

        private void FormTxn_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }
        #endregion
        #region[ Function ]
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

            //    if (mmeComment.Text.Trim() == "") { res = false; FunctionErrorCheck(1, mmeComment.ToolTipTitle, mmeComment); }
            //    else FunctionErrorCheck(0, mmeComment.ToolTipTitle, mmeComment);

            //    if (txtSubject.Text.Trim() == "") { res = false; FunctionErrorCheck(1, txtSubject.ToolTipTitle, txtSubject); }
            //    else FunctionErrorCheck(0, txtSubject.ToolTipTitle, txtSubject);

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
                //if (Static.ToInt(cboType.EditValue) == 1)
                //{
                //    if (cboContactType.Text.Trim() == "") { res = false; FunctionErrorCheck(1, cboContactType.ToolTipTitle, cboContactType); }
                //    else FunctionErrorCheck(0, cboContactType.ToolTipTitle, cboContactType);
                //}
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
        #region[ Txn ]
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
            switch (Txn)
            {
                case "Open":
                    {
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "Comment");
                        FormUtility.LookUpEdit_SetList(ref cboType, 13, "ReSolve");
                        FormUtility.LookUpEdit_SetList(ref cboType, 19, "Close");
                    }
                    break;
                case "Closed":
                    {
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "Comment");
                        FormUtility.LookUpEdit_SetList(ref cboType, 12, "ReOpen");
                    }
                    break;
                case "ReOpen":
                    {
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "Comment");
                        FormUtility.LookUpEdit_SetList(ref cboType, 13, "ReSolve");
                        FormUtility.LookUpEdit_SetList(ref cboType, 19, "Close");
                    }
                    break;
                case "ReSolved":
                    {
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "Comment");
                        FormUtility.LookUpEdit_SetList(ref cboType, 12, "ReOpen");
                        FormUtility.LookUpEdit_SetList(ref cboType, 19, "Close");
                    }
                    break;
                case "InProgress":
                    {
                        FormUtility.LookUpEdit_SetList(ref cboType, 1, "Comment");
                        FormUtility.LookUpEdit_SetList(ref cboType, 13, "ReSolve");
                    }
                    break;

            }
        }
        #endregion
    }
}