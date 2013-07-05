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
using ISM.CUser;

namespace InfoPos.Issue
{
    public partial class FormLink : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public Result ResMain=null;
        long _issueid;
        int _userno = 0;
        int _count = 0;
        int FileID = 223;

         public FormLink(InfoPos.Core.Core core, long issueid, int userno,int count)
        {
            InitializeComponent();
            _core = core;
            _issueid = issueid;
            _userno = userno;
            _count = count;
        }

        private void FormLink_Load(object sender, EventArgs e)
        {
            InitCombo();
            AllEnable();
            #region[ Image ]

            btnExit.Image = _core.Resource.GetImage("navigate_cancel");
            btnEnter.Image = _core.Resource.GetImage("image_ok");
            btnSourceIssueID.Image = _core.Resource.GetImage("button_find");
            btnDestIssueID.Image = _core.Resource.GetImage("button_find");

            #endregion
            Form FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(_core.ApplicationName, ref FormName);
        }

        void InitCombo()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";

            string[] names = new string[] { "ISSUELINKTYPE" };
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

            DT = (DataTable)Tables[0];
            if (DT == null)
            {
                msg = "Dictionary-д ISSUELINKTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboLinkType, DT, "LINKTYPEID", "NAME", "", new int[] { });
            }

            if (_count == 0)
            {
                ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 0, "Асуудал болон дэд асуудлуудыг хооронд нь холбох");
                ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 1, "Асуудлыг дэд асуудалруу шилжүүлэх");
            }
            else
            {
                ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 0, "Асуудал болон дэд асуудлуудыг хооронд нь холбох");
                ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 1, "Асуудлыг дэд асуудалруу шилжүүлэх");
                ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 2, "Дэд асуудлыг асуудалруу шилжүүлэх");
            }
        }

        void Txn0()
        {
            txtDestIssueID.EditValue = null;
            txtSourceIssueID.EditValue = null;
            cboLinkType.EditValue = null;
            mmeComment.EditValue = null;

            cboType.BackColor = Color.OldLace;

            txtSourceIssueID.Enabled = true;
            txtSourceIssueID.BackColor = Color.OldLace;

            txtDestIssueID.Enabled = true;
            txtDestIssueID.BackColor = Color.OldLace;

            txtDestIssueID.EditValue = _issueid;

            cboLinkType.Enabled = false;
            cboLinkType.BackColor = Color.OldLace;
            ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboLinkType, 1);

            mmeComment.Enabled = false;
            mmeComment.BackColor = Color.White;

            btnDestIssueID.Enabled = true;
            btnSourceIssueID.Enabled = true;
        }
        void Txn0Check(ref bool res)
        {
            if (txtSourceIssueID.Text.Trim() == "") { res = false; FunctionErrorCheck(1, txtSourceIssueID.ToolTipTitle, txtSourceIssueID); }
            else FunctionErrorCheck(0, txtSourceIssueID.ToolTipTitle, txtSourceIssueID);

            if (txtDestIssueID.Text.Trim() == "") { res = false; FunctionErrorCheck(1, txtDestIssueID.ToolTipTitle, txtDestIssueID); }
            else FunctionErrorCheck(0, txtDestIssueID.ToolTipTitle, txtDestIssueID);

            if (cboLinkType.Text.Trim() == "") { res = false; FunctionErrorCheck(1, cboLinkType.ToolTipTitle, cboLinkType); }
            else FunctionErrorCheck(0, cboLinkType.ToolTipTitle, cboLinkType);
        }

        void Txn1()
        {
            txtDestIssueID.EditValue = null;
            txtSourceIssueID.EditValue = null;
            cboLinkType.EditValue = null;
            mmeComment.EditValue = null;

            txtSourceIssueID.Enabled = true;
            txtSourceIssueID.BackColor = Color.OldLace;

            txtDestIssueID.Enabled = false;
            txtDestIssueID.BackColor = Color.White;

            txtDestIssueID.EditValue = _issueid;
            ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboLinkType, 2);
            cboLinkType.Enabled = false;
            cboLinkType.BackColor = Color.White;
            mmeComment.Enabled = true;
            mmeComment.BackColor = Color.OldLace;
            btnDestIssueID.Enabled = false;
            btnSourceIssueID.Enabled = true;
        }
        void Txn1Check(ref bool res)
        {
            if (txtSourceIssueID.Text.Trim() == "") { res = false; FunctionErrorCheck(1, txtSourceIssueID.ToolTipTitle, txtSourceIssueID); }
            else FunctionErrorCheck(0, txtSourceIssueID.ToolTipTitle, txtSourceIssueID);

            if (txtDestIssueID.Text.Trim() == "") { res = false; FunctionErrorCheck(1, txtDestIssueID.ToolTipTitle, txtDestIssueID); }
            else FunctionErrorCheck(0, txtDestIssueID.ToolTipTitle, txtDestIssueID);

            if (mmeComment.Text.Trim() == "") { res = false; FunctionErrorCheck(1, mmeComment.ToolTipTitle, mmeComment); }
            else FunctionErrorCheck(0, mmeComment.ToolTipTitle, mmeComment);
        }

        void Txn2()
        {
            txtDestIssueID.EditValue = null;
            txtSourceIssueID.EditValue = null;
            cboLinkType.EditValue = null;
            mmeComment.EditValue = null;

            txtSourceIssueID.Enabled = false;
            txtSourceIssueID.BackColor = Color.White;

            txtDestIssueID.Enabled = true;
            txtDestIssueID.BackColor = Color.OldLace;

            txtDestIssueID.EditValue = _issueid;
            cboLinkType.EditValue = null;
            ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboLinkType, 1);
            cboLinkType.Enabled = false;
            cboLinkType.BackColor = Color.White;
            mmeComment.Enabled = true;
            mmeComment.BackColor = Color.OldLace;
            btnDestIssueID.Enabled = false;
            btnSourceIssueID.Enabled = false;
        }
        void Txn2Check(ref bool res)
        {
            if (txtDestIssueID.Text.Trim() == "") { res = false; FunctionErrorCheck(1, txtDestIssueID.ToolTipTitle, txtDestIssueID); }
            else FunctionErrorCheck(0, txtDestIssueID.ToolTipTitle, txtDestIssueID);

            if (mmeComment.Text.Trim() == "") { res = false; FunctionErrorCheck(1, mmeComment.ToolTipTitle, mmeComment); }
            else FunctionErrorCheck(0, mmeComment.ToolTipTitle, mmeComment);
        }

        void AllEnable()
        {
            txtSourceIssueID.Enabled = false;
            txtDestIssueID.Enabled = false;
            cboLinkType.Enabled = false;
            mmeComment.Enabled = false;
            btnDestIssueID.Enabled = false;
            btnSourceIssueID.Enabled = false;
        }

        private void cboType_EditValueChanged(object sender, EventArgs e)
        {
            switch (ISM.Lib.Static.ToInt(cboType.EditValue))
            {
                case 0:
                    {
                        Txn0();
                    }
                    break;
                case 1:
                    {
                        Txn1();
                    }
                    break;
                case 2:
                    {
                        Txn2();
                    }
                    break;
            }
            ClearError();
        }

        private void btnSourceIssueID_Click(object sender, EventArgs e)
        {
            InfoPos.List.Issue frm = new InfoPos.List.Issue(_core);
            frm.ucIssueList.Browsable = true;
            DialogResult res = frm.ShowDialog();

            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                if (ISM.Lib.Static.ToLong(txtDestIssueID.EditValue) == ISM.Lib.Static.ToLong(frm.ucIssueList.SelectedRow["ISSUEID"]))
                {
                    MessageBox.Show("Асуудлын дугаар давхардаж байна .","Мэдээлэл",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    txtSourceIssueID.EditValue = null;
                    return;
                }
                txtSourceIssueID.EditValue = ISM.Lib.Static.ToLong(frm.ucIssueList.SelectedRow["ISSUEID"]);
            }
        }

        private void btnDestIssueID_Click(object sender, EventArgs e)
        {
            InfoPos.List.Issue frm = new InfoPos.List.Issue(_core);
            frm.ucIssueList.Browsable = true;
            DialogResult res = frm.ShowDialog();

            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                if (ISM.Lib.Static.ToLong(txtSourceIssueID.EditValue) == ISM.Lib.Static.ToLong(frm.ucIssueList.SelectedRow["ISSUEID"]))
                {
                    MessageBox.Show("Асуудлын дугаар давхардаж байна .", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDestIssueID.EditValue = null;
                    return;
                }
                txtDestIssueID.EditValue = ISM.Lib.Static.ToLong(frm.ucIssueList.SelectedRow["ISSUEID"]);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (cboType.EditValue != null)
            {
                if (ValidateLink() == true)
                {
                    #region[ Main ]
                    //SourceIssueID,DestIssueID,LINKETYPEID
                    object[] obj = new object[3];
                    obj[0] = txtSourceIssueID.EditValue;
                    obj[1] = txtDestIssueID.EditValue;
                    obj[2] = cboLinkType.EditValue;

                    object[] TxnObj = new object[14];
                    TxnObj[0] = 0;
                    TxnObj[1] = _issueid;                                 //IssueID
                    TxnObj[2] = _core.TxnDate;                            //TxnDate
                    TxnObj[3] = _core.TxnDate;                            //PostDate
                    TxnObj[4] = _core.RemoteObject.User.UserNo;                        //UserNo

                    int Value = 0;
                    if (Static.ToInt(cboType.EditValue) == 0)
                    {
                        Value = 0;
                    }
                    if (Static.ToInt(cboType.EditValue) == 1)
                    {
                        Value = 2;
                    }
                    if (Static.ToInt(cboType.EditValue) == 2)
                    {
                        Value = 4;
                    }

                    TxnObj[5] = Value;                                    //ActionTypeID
                    TxnObj[6] = "";                                       //Subject
                    TxnObj[7] = Static.ToStr(mmeComment.EditValue);       //Description
                    TxnObj[8] = 0;                                        //Status
                    TxnObj[9] = "";                                       //ResolutionTypeID
                    TxnObj[10] = 0;                                       //TrackID
                    TxnObj[11] = _userno;                                 //AssigneeUser
                    TxnObj[12] = "";                                      //NextPurpose
                    TxnObj[13] = Static.ToDate(0);                        //NextDate

                    Result res = new Result();
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310122, 310122, new object[] { TxnObj, cboType.EditValue, obj });
                    ResMain = res;
                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай хадгалагдлаа .");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                    #endregion
                }
            }
            else
            {
                MessageBox.Show("Гүйлгээний төрөл сонгоно уу .");
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
        private bool ValidateLink()
        {
            bool res = true;
            try
            {
                switch(Static.ToInt(cboType.EditValue))
                {
                    case 0: Txn0Check(ref res); break;
                    case 1: Txn1Check(ref res); break;
                    case 2: Txn2Check(ref res); break;
                }
                return res;
            }
            catch (Exception ex)
            {
                res = false;
                return res;
            }
        }

        private void FormLink_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }
    }
}