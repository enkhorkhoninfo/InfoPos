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
    public partial class FormAssign : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        long _issueid;
        int FileID = 223;
        public Result ResMain=null;
        int _userno = 0;
        public FormAssign(InfoPos.Core.Core core,long issueid,int userno)
        {
            InitializeComponent();
            _core = core;
            _issueid = issueid;
            _userno = userno;
        }

        private void InitCombos()
        {
            ArrayList Tables = new ArrayList();
            string[] names = new string[] { "USERS" };
            Result res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

            DataTable DT = (DataTable)Tables[0];
            if (DT == null)
            {
                MessageBox.Show("Dictionary-д USERS оруулаагүй байна-" + res.ResultDesc);
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboUser, DT, "USERNO", "USERLNAME", "", new int[] { 1 });
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (Static.ToStr(cboUser.EditValue).Trim() != "")
            {
                object[] obj = new object[15];
                obj[0] = 0;
                obj[1] = _issueid;                              //IssueID
                obj[2] = _core.TxnDate;                         //TxnDate
                obj[3] = _core.TxnDate;                         //PostDate
                obj[4] = _core.RemoteObject.User.UserNo;                     //UserNo
                obj[5] = 2;                                     //ActionTypeID
                obj[6] = "";                                    //Subject
                obj[7] = Static.ToStr(mmeComment.EditValue);    //Description
                obj[8] = 0;                                     //Status
                obj[9] = "";                                    //ResolutionTypeID
                obj[10] = 0;                                    //TrackID
                obj[11] = Static.ToInt(cboUser.EditValue);      //AssigneeUser
                obj[12] = "";                                   //NextPurpose
                obj[13] = Static.ToDate(0);                     //NextDate
                obj[14] = 0;                     //NextDate

                Result res = new Result();
                try
                {

                    if (_issueid != 0)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, FileID, 310106, 310106, new object[]{obj} );
                        ResMain = res;
                        if (res.ResultNo == 0)
                        {
                            MessageBox.Show("Шилжүүлэг амжилттай хийгдлээ .");
                            this.Close();
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

        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            if (_userno == Static.ToInt(cboUser.EditValue))
            {
                MessageBox.Show("Одоо хариуцаж байгаа хэрэглэгч байна .", "Хэрэглэгч буруу сонгогдсон байна .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboUser.EditValue = null;
            }
        }

        private void FormAssign_Load(object sender, EventArgs e)
        {
            InitCombos();
            btnExit.Image = _core.Resource.GetImage("navigate_cancel");
            btnEnter.Image = _core.Resource.GetImage("image_ok");
            Form FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(_core.ApplicationName, ref FormName);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAssign_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }
    }
}