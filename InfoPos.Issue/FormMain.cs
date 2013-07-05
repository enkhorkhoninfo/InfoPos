using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Issue
{
    //310087	Харилцагчтай холбоо барьсан тэмдэглэлийн жагсаалт авах
    //310088	Харилцагчтай холбоо барьсан тэмдэглэлийн дэлгэрэнгүй мэдээлэл авах
    //310089	Харилцагчтай холбоо барьсан тэмдэглэл шинээр нэмэх
    //310090	Харилцагчтай холбоо барьсан тэмдэглэл засварлах
    //310091	Харилцагчтай холбоо барьсан тэмдэглэл устгах

    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {
        #region[ Хувьсагч ]
        InfoPos.Core.Core _core;
        long CustomerID;
        long IssueID=0;
        DataRow DRCustomer;
        object[] OldValue;
        int FileID = 223;
        int RowHandle = 0;
        string Check="";
        #endregion
        #region[ Байгууллагч болон Load функц ]
        public FormMain(InfoPos.Core.Core core,long _CustomerID,DataRow _DRCustomer)
        {
            InitializeComponent();
            _core = core;
            CustomerID = _CustomerID;
            DRCustomer = _DRCustomer;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            #region[ Set Value ]
            txtCustomerNo.EditValue = CustomerID;
            txtIssueCustomerNo.EditValue = CustomerID;
            if (DRCustomer["TypeCode"].ToString() == "2")
            {
                txtCustomerName.EditValue = DRCustomer["CorporateName"].ToString();
                txtIssueCustomerName.EditValue = DRCustomer["CorporateName"].ToString();
            }
            else
            {
                if (DRCustomer["FirstName"].ToString() != "")
                {
                    txtCustomerName.EditValue = (DRCustomer["FirstName"].ToString())[0] + "." + DRCustomer["LastName"].ToString();
                    txtIssueCustomerName.EditValue = (DRCustomer["FirstName"].ToString())[0] + "." + DRCustomer["LastName"].ToString();
                }
                else
                {
                    txtCustomerName.EditValue = DRCustomer["LastName"].ToString();
                    txtIssueCustomerName.EditValue = DRCustomer["LastName"].ToString();
                }
            }
            #endregion
            
            Init();
            RefreshDataCustomerList();
            Form FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(_core.ApplicationName, ref FormName);
        }
        #endregion
        #region[ Init ]
        private void Init()
        {
            InitEvents();
            InitToggles();
            InitData();
            InitCombos();
            LoadIssue();
        }
        private void InitEvents()
        {
            ucToggleCustomer.EventSave += new ucTogglePanel.delegateEventSave(ucToggleCustomer_EventSave);
            ucToggleCustomer.EventDelete += new ucTogglePanel.delegateEventDelete(ucToggleCustomer_EventDelete);
            ucToggleCustomer.EventExit += new ucTogglePanel.delegateEventExit(ucToggleCustomer_EventExit);
            ucToggleCustomer.EventAdd += new ucTogglePanel.delegateEventAdd(ucToggleCustomer_EventAdd);
            ucToggleCustomer.EventEdit += new ucTogglePanel.delegateEventEdit(ucToggleCustomer_EventEdit);
            ucToggleCustomer.EventReject += new ucTogglePanel.delegateEventReject(ucToggleCustomer_EventReject);
            ucAssign.UserAssign += new Issue.ucAssign.dlgAssign(ucAssign_UserAssign);
            ucIssueTran.DoTran += new ucIssueTxn.dlgDoTxn(ucIssueTran_DoTran);
        }
        private void InitCombos()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";
            DictUtility.PrivNo = 310064;

            string[] names = new string[] { "CUSTCONTACTTYPE", "USERS", "ISSUETRACKS", "ISSUERESOLUTIONTYPE", "CUSTCONTACTTYPE" };
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
            #region[ MAIN ]
            DT = (DataTable)Tables[0];
            if (DT == null)
            {
                msg = "Dictionary-д CUSTCONTACTTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboContactType, DT, "TYPECODE", "NAME", "", new int[] { });
            }

            DT = (DataTable)Tables[1];
            if (DT == null)
            {
                MessageBox.Show("Dictionary-д USERS оруулаагүй байна-" + res.ResultDesc);
            }
            else
            {
                ucAssign.DataSource = DT;
            }
            #endregion
            #region[ ISSUETXN ]
            DataSet DSTXN = new DataSet();

            DT = (DataTable)Tables[2];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUETRACKS оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                DT.TableName = "ISSUETRACKS";
                DSTXN.Tables.Add(DT.Copy());
            }

            DT = (DataTable)Tables[3];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUERESOLUTIONTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                DT.TableName = "ISSUERESOLUTIONTYPE";
                DSTXN.Tables.Add(DT.Copy());
            }

            DT = (DataTable)Tables[4];
            if (DT == null)
            {
                msg = "Dictionary-д CUSTCONTACTTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                DT.TableName = "CUSTCONTACTTYPE";
                DSTXN.Tables.Add(DT.Copy());
            }
            ucIssueTran.InitCombos(DSTXN);
            #endregion
        }
        private void InitToggles()
        {
            try
            {
                #region [ Issue General ]
                ucToggleCustomer.ToggleShowDelete = true;
                ucToggleCustomer.ToggleShowEdit = true;
                ucToggleCustomer.ToggleShowExit = true;
                ucToggleCustomer.ToggleShowNew = true;
                ucToggleCustomer.ToggleShowReject = true;
                ucToggleCustomer.ToggleShowSave = true;
                ucToggleCustomer.DataSource = null;
                ucToggleCustomer.FieldLinkSetSaveState();
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
                #region [ Issue Customer Note ]
                ucToggleCustomer.FieldLinkAdd("txtSeqNo", 0, "SeqNo", "", false, true, true);
                ucToggleCustomer.FieldLinkAdd("dteContactDate", 0, "ContactDate", "", true, false);
                ucToggleCustomer.FieldLinkAdd("cboContactType", 0, "ContactType", "", true, false);
                ucToggleCustomer.FieldLinkAdd("txtBriefDesc", 0, "BriefDesc", "", true, false);
                ucToggleCustomer.FieldLinkAdd("mmeNote", 0, "Note", "", true, false);

                ucToggleCustomer.DataSource = null;
                ucToggleCustomer.GridView = gvwCustomer;
                ucIssueTran.Core = _core;

                #region[ Image ]
                ucToggleCustomer.Resource = _core.Resource;
                btnECustomer.Image= _core.Resource.GetImage("navigate_refresh");
                btnIssueECustomer.Image = _core.Resource.GetImage("navigate_refresh");
                btnAddVote.Image = _core.Resource.GetImage("image_like");
                btnAddIssue.Image = _core.Resource.GetImage("navigate_add");
                btnEditIssue.Image = _core.Resource.GetImage("navigate_edit");
                btnDeleteIssue.Image = _core.Resource.GetImage("navigate_delete");
                btnAssignee.Image = _core.Resource.GetImage("issue_assign");
                btnLink.Image = _core.Resource.GetImage("issue_link");
                btnComment.Image = _core.Resource.GetImage("menu_txn");
                btnProgress.Image = _core.Resource.GetImage("issue_inprogress");

                IssueImageList.Images.Add(_core.Resource.GetImage("dashboard_user"));
                IssueImageList.Images.Add(_core.Resource.GetImage("gl_ok"));    //9 - Дууссан
                IssueImageList.Images.Add(_core.Resource.GetImage("gl_error")); //1 - Алдаа
                IssueImageList.Images.Add(_core.Resource.GetImage("issue_inprogress"));  //0 - Шинэ


                IssueImageList.Images.Add(_core.Resource.GetImage("issue_image"));
                IssueImageList.Images.Add(_core.Resource.GetImage("issue_document"));
                IssueImageList.Images.Add(_core.Resource.GetImage("issue_other"));

                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void xtraTabIssue_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            //switch (e.PageIndex)
            //{
            //    case 1:
            //        LoadIssue();
            //        break;
            //}
        }
        #endregion
        #region[ Toggle Event ]
        void ucToggleCustomer_EventReject()
        {
            ClearError();
        }
        void ucToggleCustomer_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(txtSeqNo.EditValue), Static.ToDate(dteContactDate.EditValue), Static.ToDateTime(dteContactDate.EditValue), Static.ToInt(cboContactType.EditValue), mmeNote.EditValue, txtBriefDesc.EditValue };
            OldValue = Value;
        }
        void ucToggleCustomer_EventAdd(ref bool cancel)
        {

        }
        void ucToggleCustomer_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void ucToggleCustomer_EventDelete()
        {
            DeleteCustomer();
        }
        void ucToggleCustomer_EventSave(bool isnew, ref bool cancel)
        {
            if (ValidateCustomerGen() == true)
            {
                SaveCustomerData(isnew, ref cancel);
            }
            else
            {
                cancel = true;
            }
        }
        #endregion
        #region[ Function ]
        
        #region[ Customer ]
        public void RefreshDataCustomerList()
        {
            Result res = new Result();
            try
            {

                if (CustomerID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310087, 310087, new object[] { CustomerID });
                    if (res.ResultNo == 0)
                    {
                        ucToggleCustomer.DataSource = res.Data;
                        ucToggleCustomer.FieldLinkSetValues();
                        SetDataCustomer();
                        ucToggleCustomer.FieldLinkSetSaveState();
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
        public void SetDataCustomer()
        {
            //CUSTOMERNO, SEQNO, CONTACTDATE, POSTDATE, CONTACTTYPE, NOTE, BRIEFDESC
            gvwCustomer.Columns[0].Caption = "Харилцагчийн дугаар";
            gvwCustomer.Columns[0].Visible = false;

            gvwCustomer.Columns[1].Caption = "Дэс дугаар";

            gvwCustomer.Columns[2].Caption = "Холбогдсон огноо";

            gvwCustomer.Columns[3].Caption = "Холбогдсон огноо, цаг минут";
            gvwCustomer.Columns[3].Visible = false;

            gvwCustomer.Columns[4].Caption = "Холбоо барьсан төрөл";
            gvwCustomer.Columns[4].Visible = false;

            gvwCustomer.Columns[5].Caption = "Дэлгэрэнгүй мэдээлэл";
            gvwCustomer.Columns[5].Visible = false;

            gvwCustomer.Columns[6].Caption = "Товч утга";
            gvwCustomer.Columns[7].Caption = "Хэрэглэгч";
            ISM.Template.FormUtility.SetFormatGrid(ref gvwCustomer, false);
        }
        public void SaveCustomerData(bool isnew,ref bool cancel)
        {
            Result res = new Result();
            string msg = "";
            try
            {
                object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(txtSeqNo.EditValue), Static.ToDate(dteContactDate.EditValue), Static.ToDateTime(dteContactDate.EditValue), Static.ToInt(cboContactType.EditValue), mmeNote.EditValue, txtBriefDesc.EditValue,_core.RemoteObject.User.UserNo };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310089, 310089, new object[] { Value });
                    msg = "Амжилттай нэмлээ";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310090, 310090, new object[] { Value, OldValue });
                    msg = "Амжилттай засварлалаа";
                }
                if (res.ResultNo == 0)
                {
                    RefreshDataCustomerList();
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
        public void DeleteCustomer()
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                long SeqNo = Static.ToLong(txtSeqNo.Text);
                if (SeqNo != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310091, 310091, new object[] { CustomerID, SeqNo });

                    if (res.ResultNo == 0)
                    {
                        ucToggleCustomer.FieldLinkSetNewState();
                        RefreshDataCustomerList();
                        MessageBox.Show("Амжилттай устгагдлаа");
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
        private bool ValidateCustomerGen()
        {
            bool res = true;
            try
            {
                if (mmeNote.Text.Trim() == "") { res = false; FunctionErrorCheck(1, mmeNote.ToolTipTitle, mmeNote); }
                else FunctionErrorCheck(0, mmeNote.ToolTipTitle, mmeNote);

                if (txtBriefDesc.Text.Trim() == "") { res = false; FunctionErrorCheck(1, txtBriefDesc.ToolTipTitle, txtBriefDesc); }
                else FunctionErrorCheck(0, txtBriefDesc.ToolTipTitle, txtBriefDesc);

                if (cboContactType.Text.Trim() == "-") { res = false; FunctionErrorCheck(1, cboContactType.ToolTipTitle, cboContactType); }
                else FunctionErrorCheck(0, cboContactType.ToolTipTitle, cboContactType);

                if (dteContactDate.Text.Trim() == "") { res = false; FunctionErrorCheck(1, dteContactDate.ToolTipTitle, dteContactDate); }
                else FunctionErrorCheck(0, dteContactDate.ToolTipTitle, dteContactDate);
                return res;
            }
            catch (Exception ex)
            {
                res = false;
                return res;
            }
        }
        #endregion
        #region[ Issue ]
        public void RefreshDataIssueList()
        {
            Result res = new Result();
            try
            {

                if (CustomerID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310092, 310092, new object[] { CustomerID });
                    if (res.ResultNo == 0)
                    {
                        grdIssue.DataSource = res.Data.Tables[0];
                        if (res.Data.Tables[0].Rows.Count > 0)
                        {
                            STATUS.Caption = "Нийт\r\n (" + res.Data.Tables[0].Rows.Count + ")";
                            gvwBandedIssue.UnselectRow(0);
                            gvwBandedIssue.SelectRow(RowHandle);
                            gvwBandedIssue.FocusedRowHandle = RowHandle;
                            DataRow DR = gvwBandedIssue.GetFocusedDataRow();
                            FocusRow(DR);
                            chartIssueLoad();
                        }
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
        public void RefreshDataIssueAttach()
        {
            Result res = new Result();
            System.IO.FileInfo file;
            try
            {

                if (IssueID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310098, 310098, new object[] { IssueID });
                    if (res.ResultNo == 0)
                    {
                        DataTable DTxn = new DataTable("Attach");
                        DataColumn DCol;
                        DataRow RW;
                        DCol = new DataColumn();
                        DCol.ColumnName = "ATTACHID";
                        DCol.DataType = System.Type.GetType("System.Int64");
                        DTxn.Columns.Add(DCol);

                        DCol = new DataColumn();
                        DCol.ColumnName = "FILENAME";
                        DCol.DataType = System.Type.GetType("System.String");
                        DTxn.Columns.Add(DCol);

                        DCol = new DataColumn();
                        DCol.ColumnName = "FILETYPE";
                        DCol.DataType = System.Type.GetType("System.Byte[]");
                        DTxn.Columns.Add(DCol);

                        DCol = new DataColumn();
                        DCol.ColumnName = "CREATEDATE";
                        DCol.DataType = System.Type.GetType("System.DateTime");
                        DTxn.Columns.Add(DCol);

                        DCol = new DataColumn();
                        DCol.ColumnName = "DESCRIPTION";
                        DCol.DataType = System.Type.GetType("System.String");
                        DTxn.Columns.Add(DCol);

                        DataTable DT=res.Data.Tables[0];
                        foreach(DataRow rw in DT.Rows)
                        {
                            file = new System.IO.FileInfo(rw["FILENAME"].ToString());
                            RW = DTxn.NewRow();
                            RW["ATTACHID"] = rw["ATTACHID"];
                            RW["FILENAME"] = file.Name;
                            RW["CREATEDATE"] = rw["CREATEDATE"];
                            RW["DESCRIPTION"] = rw["DESCRIPTION"];

                            if (rw["FILETYPE"].ToString().Trim() == "0")
                            {
                                RW["FILETYPE"] = ISM.Lib.Static.PngImageToByte(_core.Resource.GetImage("issue_image"));
                            }
                            if (rw["FILETYPE"].ToString().Trim() == "1")
                            {
                                RW["FILETYPE"] = ISM.Lib.Static.PngImageToByte(_core.Resource.GetImage("issue_document"));
                            }
                            if (rw["FILETYPE"].ToString().Trim() == "2")
                            {
                                RW["FILETYPE"] = ISM.Lib.Static.PngImageToByte(_core.Resource.GetImage("issue_other"));
                            }
                            DTxn.Rows.Add(RW);
                        }
                        grdAttach.DataSource = DTxn;
                        //grdAttach.
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
        void RefreshDataIssueAttachFromData(DataTable DT)
        {
            System.IO.FileInfo file;
            DataTable DTxn = new DataTable("Attach");
            DataColumn DCol;
            DataRow RW;
            DCol = new DataColumn();
            DCol.ColumnName = "ATTACHID";
            DCol.DataType = System.Type.GetType("System.Int64");
            DTxn.Columns.Add(DCol);

            DCol = new DataColumn();
            DCol.ColumnName = "FILENAME";
            DCol.DataType = System.Type.GetType("System.String");
            DTxn.Columns.Add(DCol);

            DCol = new DataColumn();
            DCol.ColumnName = "FILETYPE";
            DCol.DataType = System.Type.GetType("System.Byte[]");
            DTxn.Columns.Add(DCol);

            DCol = new DataColumn();
            DCol.ColumnName = "CREATEDATE";
            DCol.DataType = System.Type.GetType("System.DateTime");
            DTxn.Columns.Add(DCol);

            DCol = new DataColumn();
            DCol.ColumnName = "DESCRIPTION";
            DCol.DataType = System.Type.GetType("System.String");
            DTxn.Columns.Add(DCol);

            foreach (DataRow rw in DT.Rows)
            {
                file = new System.IO.FileInfo(rw["FILENAME"].ToString());
                RW = DTxn.NewRow();
                RW["ATTACHID"] = rw["ATTACHID"];
                RW["FILENAME"] = file.Name;
                RW["CREATEDATE"] = rw["CREATEDATE"];
                RW["DESCRIPTION"] = rw["DESCRIPTION"];

                if (rw["FILETYPE"].ToString().Trim() == "0")
                {
                    RW["FILETYPE"] = ISM.Lib.Static.PngImageToByte(_core.Resource.GetImage("issue_image"));
                }
                if (rw["FILETYPE"].ToString().Trim() == "1")
                {
                    RW["FILETYPE"] = ISM.Lib.Static.PngImageToByte(_core.Resource.GetImage("issue_document"));
                }
                if (rw["FILETYPE"].ToString().Trim() == "2")
                {
                    RW["FILETYPE"] = ISM.Lib.Static.PngImageToByte(_core.Resource.GetImage("issue_other"));
                }
                DTxn.Rows.Add(RW);
            }
            grdAttach.DataSource = DTxn;
        }
        public void RefreshDataIssueTxn() 
        {
            Result res = new Result();
            try
            {

                if (IssueID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310105, 310105, new object[] { IssueID });
                    if (res.ResultNo == 0)
                    {
                        DataTable DT = res.Data.Tables[0];
                        grdTxn.DataSource = DT;
                        ISM.Template.FormUtility.SetFormatGrid(ref gvwTxn, true);
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
        void RefreshDataIssueTxnFromData(DataTable DT)
        {
            grdTxn.DataSource = DT;
            ISM.Template.FormUtility.SetFormatGrid(ref gvwTxn, true);
        }

        void DeleteIssue(long issueid)
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                if (issueid != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310083, 310083, new object[] { issueid });

                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");
                        RefreshDataIssueList();
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
        void LoadIssue()
        {
            RefreshDataIssueList();
        }
        void FocusRow(DataRow DR)
        {
            if (DR != null)
            {
                
                lblSubject.Text = DR["ISSUEID"].ToString() + " - " + DR["SUBJECT"].ToString();
                lblECreateDate.Text = DR["CREATEDATE"].ToString();
                lblECreateUserName.Text = DR["CREATEUSER"].ToString() + " - " + DR["CREATEUSERNAME"].ToString();
                mmeDescription.EditValue = DR["DESCRIPTION"].ToString();
                ucAssign.ChangeName(Static.ToInt(DR["ASSIGNEEUSER"]), Static.ToStr(DR["ASSIGNEEUSERNAME"]));
                //lblEStatus.Text = DR["STATUSNAME"].ToString();

                ucIssueTran.ChangeValue(DR["STATUSNAME"].ToString(), Static.ToInt(DR["ASSIGNEEUSER"]), Static.ToInt(DR["TRACKID"]), IssueID, Static.ToInt(DR["STATUS"]));
                if (DR["STATUSNAME"].ToString() == "InProgress")
                {
                    btnProgress.Image = _core.Resource.GetImage("gl_wait");
                }
                else
                {
                    btnProgress.Image = _core.Resource.GetImage("issue_inprogress");
                }
                lblETrackID.Text = DR["TRACKNAME"].ToString();
                IssueID = Static.ToLong(DR["ISSUEID"]);
                if (DR["VOTES"].ToString() == "")
                    lblVote.Text = "Vote(0)";
                else
                    lblVote.Text = "Vote(" + DR["VOTES"].ToString() + ")";

                int ISSUETYPEID = Static.ToInt(DR["ISSUETYPEID"]);
                Result res = new Result();

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310006, 310006, new object[] { ISSUETYPEID, IssueID, _core.RemoteObject.User.UserNo });

                if (res.ResultNo == 0)
                {
                    RefreshDataIssueAttachFromData(res.Data.Tables[2]);
                    RefreshDataIssueTxnFromData(res.Data.Tables[3]);
                    grdSourceIssue.DataSource = res.Data.Tables[1];

                        if (res.Data.Tables[0].Rows.Count != 0)
                        {
                            DataRow rw = res.Data.Tables[0].Rows[0];
                            if (Static.ToInt((rw["Vote"])) == 0)
                            {
                                lblVote.Text = "Vote(Санал асуулга байхгүй)";
                                btnAddVote.Enabled = false;
                            }
                            else
                            {
                                    if (res.Data.Tables[5].Rows.Count != 0)
                                    {
                                        lblVote.ToolTip = "";
                                        foreach (DataRow dr in res.Data.Tables[5].Rows)
                                        {
                                            lblVote.ToolTip = lblVote.ToolTip + dr["userno"].ToString() + " - " + dr["username"].ToString() + "\r\n";
                                        }
                                    }

                                    if (res.Data.Tables[4].Rows.Count != 0)
                                    {
                                        DataRow rwVoteUser = res.Data.Tables[4].Rows[0];
                                        if (Static.ToInt((rwVoteUser["Vote"])) == 0)
                                        {
                                            btnAddVote.Enabled = true;
                                        }
                                        else
                                        {
                                            btnAddVote.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        btnAddVote.Enabled = false;
                                    }
                            }
                        }
                        else
                        {
                            btnAddVote.Enabled = false;
                        }

                }
                else
                {
                    MessageBox.Show(res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblVote.Text = "Vote(Санал өгөх боломжгүй)";
                    btnAddVote.Enabled = false;
                }



                if (btnAddVote.Enabled == false)
                {
                    btnAllEnableTrue();
                    btnAddVote.Enabled = false;
                }
                else
                {
                    btnAllEnableTrue();
                }
            }
        }
        void FocusRowSourceIssue(DataRow DR)
        {
            if (DR != null)
            {

                lblSubject.Text = DR["ISSUEID"].ToString() + " - " + DR["SUBJECT"].ToString();
                lblECreateDate.Text = DR["CREATEDATE"].ToString();
                lblECreateUserName.Text = DR["CREATEUSER"].ToString() + " - " + DR["CREATEUSERNAME"].ToString();
                mmeDescription.EditValue = DR["DESCRIPTION"].ToString();
                //lblEStatus.Text = DR["STATUSNAME"].ToString();
                if (DR["STATUSNAME"].ToString() == "InProgress")
                {
                    btnProgress.Image = _core.Resource.GetImage("gl_wait");
                }
                else
                {
                    btnProgress.Image = _core.Resource.GetImage("issue_inprogress");
                }
                lblETrackID.Text = DR["TRACKNAME"].ToString();
                IssueID = Static.ToLong(DR["ISSUEID"]);
                if (DR["VOTES"].ToString() == "")
                    lblVote.Text = "Vote(0)";
                else
                    lblVote.Text = "Vote(" + DR["VOTES"].ToString() + ")";

                int ISSUETYPEID = Static.ToInt(DR["ISSUETYPEID"]);
                Result res = new Result();

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310006, 310006, new object[] { ISSUETYPEID, IssueID, _core.RemoteObject.User.UserNo });

                if (res.ResultNo == 0)
                {
                    RefreshDataIssueAttachFromData(res.Data.Tables[2]);
                    RefreshDataIssueTxnFromData(res.Data.Tables[3]);

                    if (res.Data.Tables[0].Rows.Count != 0)
                    {
                        DataRow rw = res.Data.Tables[0].Rows[0];
                        if (Static.ToInt((rw["Vote"])) == 0)
                        {
                            lblVote.Text = "Vote(Санал асуулга байхгүй)";
                            btnAddVote.Enabled = false;
                        }
                        else
                        {
                            if (res.Data.Tables[5].Rows.Count != 0)
                            {
                                lblVote.ToolTip = "";
                                foreach (DataRow dr in res.Data.Tables[5].Rows)
                                {
                                    lblVote.ToolTip = lblVote.ToolTip + dr["userno"].ToString() + " - " + dr["username"].ToString() + "\r\n";
                                }
                            }

                            if (res.Data.Tables[4].Rows.Count != 0)
                            {
                                DataRow rwVoteUser = res.Data.Tables[4].Rows[0];
                                if (Static.ToInt((rwVoteUser["Vote"])) == 0)
                                {
                                    btnAddVote.Enabled = true;
                                }
                                else
                                {
                                    btnAddVote.Enabled = false;
                                }
                            }
                            else
                            {
                                btnAddVote.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        btnAddVote.Enabled = false;
                    }

                }
                else
                {
                    MessageBox.Show(res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblVote.Text = "Vote(Санал өгөх боломжгүй)";
                    btnAddVote.Enabled = false;
                }
                btnAllEnableFalse();
            }
          
        }
        public Result DownloadFile(ulong attachid)
        {
            Result r = null;
            if (_core != null)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Хавсралт файлыг байрлуулах хавтасыг зааж өгнө үү";
                dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                DialogResult dr = dialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    r = AttachUtility.GetFile(_core.RemoteObject, 310106, attachid, dialog.SelectedPath);
                    string s = "";
                    if (r.ResultNo == 0)
                    {
                        string filename = Convert.ToString(r.Param[0]);
                        s = string.Format("Файл амжилттай хадгаллаа.\r\nХавсралт файлын зам: {0}", filename);
                    }
                    else
                    {
                        s = string.Format("{0}: {1}", r.ResultNo, r.ResultDesc);
                    }
                    MessageBox.Show(s, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return r;
        }
        public void SourceRowClick()
        {
            Result res = new Result();
            try
            {
                if (gvwSourceIssue.GetFocusedDataRow() != null)
                {
                    DataRow DR = gvwSourceIssue.GetFocusedDataRow();
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310080, 310080, new object[] { Static.ToLong(DR[0]) });
                    if (res.ResultNo == 0)
                    {
                        if (res.Data.Tables[0].Rows.Count > 0)
                        {
                            FocusRowSourceIssue(res.Data.Tables[0].Rows[0]);
                        }
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
        void btnAllEnableFalse()
        {
            btnProgress.Enabled = false;
            btnAddVote.Enabled = false;
            btnLink.Enabled = false;
            btnAssignee.Enabled = false;
            btnComment.Enabled = false;
        }
        void btnAllEnableTrue()
        {
            btnProgress.Enabled = true;
            btnAddVote.Enabled = true;
            btnLink.Enabled = true;
            btnAssignee.Enabled = true;
            btnComment.Enabled = true;
        }
        void chartIssueLoad()
        {
            ChartIssue.BeginInit();
            Series pSeries = ChartIssue.Series[0];
            PieSeriesView view = new PieSeriesView();
            view.RuntimeExploding = true;
            pSeries.View = view;
            pSeries.Points.Clear();
            DataTable _DT = (DataTable)grdIssue.DataSource;
            if (_DT != null)
            {
                int val_reopen = 0;
                int val_open = 0;
                int val_closed = 0;
                int val_inprogress = 0;
                int val_resolved=0;

                foreach (DataRow dr in _DT.Rows)
                {
                    if (dr["STATUSNAME"].ToString() == "Open")
                    {
                        val_open++;
                    }
                    if (dr["STATUSNAME"].ToString() == "ReOpen")
                    {
                        val_reopen++;
                    }
                    if (dr["STATUSNAME"].ToString() == "Closed")
                    {
                        val_closed++;
                    }
                    if (dr["STATUSNAME"].ToString() == "InProgress")
                    {
                        val_inprogress++;
                    }
                    if (dr["STATUSNAME"].ToString() == "Resolved")
                    {
                        val_resolved++;
                    }
                }

                if (val_open != 0) pSeries.Points.Add(new SeriesPoint("OPEN", val_open));
                if (val_reopen != 0) pSeries.Points.Add(new SeriesPoint("REOPEN", val_reopen));
                if (val_closed != 0) pSeries.Points.Add(new SeriesPoint("CLOSED", val_closed));
                if (val_inprogress != 0) pSeries.Points.Add(new SeriesPoint("INPROGRESS", val_inprogress));
                if (val_resolved != 0) pSeries.Points.Add(new SeriesPoint("RESOLVED", val_resolved));
            }
            ChartIssue.EndInit();
        }
        #endregion

        #endregion
        #region[ Btn and Event]
        private void btnECustomer_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                if (Static.ToLong(txtCustomerNo.EditValue) != 0)
                {
                    object[] objSearch = new object[23];
                    objSearch[0] = Static.ToLong(txtCustomerNo.EditValue);

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120001, 120001, objSearch);

                    if (res.ResultNo == 0)
                    {
                        if (res.Data.Tables[0].Rows.Count == 0)
                        {
                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 310126, 310126, objSearch);
                            if (res.ResultNo == 0)
                            {
                                object[] obj = new object[3];
                                obj[0] = _core;
                                obj[1] = CustomerID;
                                obj[2] = res.Data.Tables[0].Rows[0];
                                EServ.Shared.Static.Invoke("InfoPos.Enquiry.dll", "InfoPos.Enquiry.Main", "CallCustContactEnquiry", obj);
                            }
                        }
                        else
                        {
                            object[] obj = new object[3];
                            obj[0] = _core;
                            obj[1] = CustomerID;
                            obj[2] = res.Data.Tables[0].Rows[0];
                            EServ.Shared.Static.Invoke("InfoPos.Enquiry.dll", "InfoPos.Enquiry.Main", "CallCustomerEnquiry", obj);
                        }
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
        private void btnComment_Click(object sender, EventArgs e)
        {
            if (gvwBandedIssue.RowCount != 0)
            {
                RowHandle = gvwBandedIssue.FocusedRowHandle;
                DataRow DR = gvwBandedIssue.GetFocusedDataRow();
                FormTxn txnForm = new FormTxn(_core, IssueID, Static.ToInt(DR["Status"]), Static.ToInt(DR["ASSIGNEEUSER"]), Static.ToInt(DR["TRACKID"]));
                txnForm.ShowDialog();
                if (txnForm.ResMain != null)
                {
                    if (txnForm.ResMain.ResultNo == 0)
                    {
                        RefreshDataIssueList();
                        //gvwBandedIssue.UnselectRow(0);
                        //gvwBandedIssue.SelectRow(RowHandle);
                        //gvwBandedIssue.FocusedRowHandle = RowHandle;
                        //FocusRow(DR);
                    }
                }
            }
        }
        private void btnIssueECustomer_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                if (Static.ToLong(txtCustomerNo.EditValue) != 0)
                {
                    object[] objSearch = new object[23];
                    objSearch[0] = Static.ToLong(txtCustomerNo.EditValue);

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120001, 120001, objSearch);

                    if (res.ResultNo == 0)
                    {
                        if (res.Data.Tables[0].Rows.Count == 0)
                        {
                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 310126, 310126, objSearch);
                            if (res.ResultNo == 0)
                            {
                                object[] obj = new object[3];
                                obj[0] = _core;
                                obj[1] = CustomerID;
                                obj[2] = res.Data.Tables[0].Rows[0];
                                EServ.Shared.Static.Invoke("InfoPos.Enquiry.dll", "InfoPos.Enquiry.Main", "CallCustContactEnquiry", obj);
                            }
                        }
                        else
                        {
                            object[] obj = new object[3];
                            obj[0] = _core;
                            obj[1] = CustomerID;
                            obj[2] = res.Data.Tables[0].Rows[0];
                            EServ.Shared.Static.Invoke("InfoPos.Enquiry.dll", "InfoPos.Enquiry.Main", "CallCustomerEnquiry", obj);
                        }
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
        private void gvwCustomer_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucToggleCustomer.FieldLinkSetValues();
        }
        private void gvwBandedIssue_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow DR = gvwBandedIssue.GetFocusedDataRow();
            FocusRow(DR);
        }
        private void btnAddVote_Click(object sender, EventArgs e)
        {
            if (IssueID != 0) 
            { 
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310118, 310118, new object[] {IssueID});

                if (res.ResultNo == 0)
                {
                    if (res.Data != null)
                    {
                        if (res.Data.Tables[0].Rows.Count != 0)
                        {
                            int votes = Static.ToInt(res.Data.Tables[0].Rows[0]["votes"]);
                            votes++;
                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310117, 310117, new object[] { IssueID,votes });
                            if (res.ResultNo == 0)
                            {
                                MessageBox.Show("Санал амжилттай нэмэгдлээ .");
                                lblVote.Text = "Vote(" + votes.ToString() + ")";
                                gvwBandedIssue.SetRowCellValue(gvwBandedIssue.FocusedRowHandle, VOTES, votes);
                                btnAddVote.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc, "");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void PictureEdit_DoubleClick(object sender, EventArgs e)
        {
            DataRow rw=gvwAttach.GetFocusedDataRow();
            ulong attachid = Static.ToULong(rw["ATTACHID"]);
            Result r = DownloadFile(attachid);
        }
        private void btnAddIssue_Click(object sender, EventArgs e)
        {
            if (txtCustomerNo.EditValue != null)
            {
                FormIssue frm = new FormIssue(_core, 0, Static.ToLong(txtCustomerNo.EditValue));
                frm.ShowDialog();
                if (frm.ResMain!=null)
                {
                    if (frm.ResMain.ResultNo == 0)
                    {
                        RefreshDataIssueList();
                    }
                }
            }
        }
        private void btnEditIssue_Click(object sender, EventArgs e)
        {
            if (ucIssueTran.Status != "Closed")
            {
                if (txtCustomerNo.EditValue != null)
                {
                    if (IssueID != 0)
                    {
                        FormIssue frm = new FormIssue(_core, IssueID, Static.ToLong(txtCustomerNo.EditValue));
                        frm.ShowDialog();
                        if (frm.ResMain != null)
                        {
                            if (frm.ResMain.ResultNo == 0)
                            {
                                RefreshDataIssueList();
                            }
                        }
                    }
                }
            }
        }
        private void btnDeleteIssue_Click(object sender, EventArgs e)
        {
            if (ucIssueTran.Status == "InProgress")
            {
                MessageBox.Show("Устгах боломжгүй процесс эхэлсэн байна .", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IssueID != 0)
            {
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310118, 310118, new object[] { IssueID });

                if (res.ResultNo == 0)
                {
                    if (res.Data != null)
                    {
                        if (res.Data.Tables[0].Rows.Count != 0)
                        {
                            #region[ Main ]
                            int checkDelete = 0;
                            int votes = Static.ToInt(res.Data.Tables[0].Rows[0]["votes"]);
                            if (votes == 0)
                            {
                                checkDelete++;
                            }
                            else
                            {
                                MessageBox.Show("Устгах боломжгүй санал өгөгдсөн байна .","Мэдээлэл",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                return;
                            }

                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310098, 310098, new object[] { IssueID });
                            if (res.ResultNo == 0)
                            {
                                if (res.Data.Tables[0].Rows.Count == 0)
                                {
                                    checkDelete++;
                                }
                                else
                                {
                                    MessageBox.Show("Устгах боломжгүй хавсралт файл оруулсан байна .", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show(res.ResultDesc, "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }


                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310105, 310105, new object[] { IssueID });
                            if (res.ResultNo == 0)
                            {
                                if (res.Data.Tables[0].Rows.Count == 0)
                                {
                                    checkDelete++;
                                }
                                else
                                {
                                    MessageBox.Show("Устгах боломжгүй гүйлгээ хийсэн байна .", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show(res.ResultDesc, "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (checkDelete == 3)
                            {
                                DeleteIssue(IssueID);
                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Устгах боломжгүй байна .", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Устгах боломжгүй байна .", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnProgress_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                RowHandle = gvwBandedIssue.FocusedRowHandle;
                if (IssueID != 0)
                {
                    if (ucIssueTran.Status == "ReOpen")
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310120, 310120, new object[] { IssueID,1});
                        if (res.ResultNo == 0)
                        {
                            RefreshDataIssueList();
                            btnProgress.Image=_core.Resource.GetImage("gl_wait");
                            return;
                        }
                        else
                        {
                            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                        }
                    }
                    if (ucIssueTran.Status == "Open")
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310120, 310120, new object[] { IssueID,1});
                        if (res.ResultNo == 0)
                        {
                            RefreshDataIssueList();
                            btnProgress.Image = _core.Resource.GetImage("gl_wait");
                            return;
                        }
                        else
                        {
                            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                        }
                    }
                    if (ucIssueTran.Status == "InProgress")
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310120, 310120, new object[] { IssueID, 0 });
                        if (res.ResultNo == 0)
                        {
                            RefreshDataIssueList();
                            btnProgress.Image = _core.Resource.GetImage("issue_inprogress");
                            return;
                        }
                        else
                        {
                            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAssignee_Click(object sender, EventArgs e)
        {
            if (gvwBandedIssue.RowCount != 0)
            {
                RowHandle = gvwBandedIssue.FocusedRowHandle;
                DataRow DR = gvwBandedIssue.GetFocusedDataRow();
                FormAssign txnForm = new FormAssign(_core, IssueID, Static.ToInt(DR["ASSIGNEEUSER"]));
                txnForm.ShowDialog();
                if (txnForm.ResMain != null)
                {
                    if (txnForm.ResMain.ResultNo == 0)
                    {
                        RefreshDataIssueList();
                    }
                }
            }
        }

        void ucAssign_UserAssign()
        {
            if (ucAssign.FocusedUserNo != 0)
            {
                object[] obj = new object[15];
                obj[0] = 0;
                obj[1] = IssueID;                              //IssueID
                obj[2] = _core.TxnDate;                         //TxnDate
                obj[3] = _core.TxnDate;                         //PostDate
                obj[4] = _core.RemoteObject.User.UserNo;                     //UserNo
                obj[5] = 2;                                     //ActionTypeID
                obj[6] = "";                                    //Subject
                obj[7] = Static.ToStr(ucAssign.Comment.EditValue);    //Description
                obj[8] = 0;                                     //Status
                obj[9] = "";                                    //ResolutionTypeID
                obj[10] = 0;                                    //TrackID
                obj[11] = ucAssign.FocusedUserNo;      //AssigneeUser
                obj[12] = "";                                   //NextPurpose
                obj[13] = Static.ToDate(0);                     //NextDate
                obj[14] = 0;                     //NextDate

                Result res = new Result();
                try
                {

                    if (IssueID != 0)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310106, 310106, new object[] { obj });
                        if (res.ResultNo == 0)
                        {
                            RefreshDataIssueList();
                            MessageBox.Show("Шилжүүлэг амжилттай хийгдлээ .");
                            ucAssign.Size = new Size(ucAssign.Size.Width, 20);
                        }
                        else
                        {
                            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Хэрэглэгч сонгогдоогүй байна .", "Алдаа .", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ucIssueTran_DoTran()
        {
            Result res = new Result();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 223, 310106, 310106, ucIssueTran.SaveObj);
            if (res.ResultNo == 0)
            {
                RefreshDataIssueList();
                MessageBox.Show("Амжилттай хадгалагдлаа .");
                ucIssueTran.Size = ucIssueTran.MinimumSize;
            }
            else
            {
                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc,"Алдаа",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            if (gvwBandedIssue.RowCount != 0)
            {
                RowHandle = gvwBandedIssue.FocusedRowHandle;
                DataRow DR = gvwBandedIssue.GetFocusedDataRow();
                FormLink frm = new FormLink(_core, Static.ToLong(DR["IssueID"]), Static.ToInt(DR["ASSIGNEEUSER"]), gvwSourceIssue.RowCount);

                frm.ShowDialog();
                if (frm.ResMain != null)
                {
                    if (frm.ResMain.ResultNo == 0)
                    {
                        gvwBandedIssue.UnselectRow(0);
                        gvwBandedIssue.SelectRow(RowHandle);
                        gvwBandedIssue.FocusedRowHandle = RowHandle;
                        FocusRow(DR);
                    }
                }
            }
        }
        private void gvwSourceIssue_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            SourceRowClick();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }
        private void gvwTxn_DoubleClick(object sender, EventArgs e)
        {
            if(gvwTxn.GetFocusedDataRow()!=null)
            {
                DataRow rw=gvwTxn.GetFocusedDataRow();
                InfoPos.Enquiry.IssueTxnEnquiry frm = new Enquiry.IssueTxnEnquiry(_core, Static.ToLong(rw["JRNO"]));
                frm.Show();
            }
        }
        private void navBarIssue_GroupCollapsed(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if (gvwAttach.RowCount == 0) { navBarGroup2.Expanded = false; }
            if (gvwTxn.RowCount == 0) { navBarGroup3.Expanded = false; }
        }
        private void navBarIssue_GroupExpanded(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if (gvwAttach.RowCount == 0) { navBarGroup2.Expanded = false; }
            if (gvwTxn.RowCount == 0) { navBarGroup3.Expanded = false; }
        }
        #endregion
    }
}