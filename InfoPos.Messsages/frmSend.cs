using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Messages
{
    public partial class frmSend : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core = null;
        DataTable DT;
        DataTable DTB;
        #endregion
        #region[Байгуулагч функц]
        public frmSend(InfoPos.Core.Core core, int userno, string description, int type)
        {
            InitializeComponent();
            _core = core;
            InitCombo();
            if (type == 1)
            {
                FormUtility.LookUpEdit_SetValue(ref cboUserNo, userno);
                mmoDesc.EditValue = description;
            }
            if (type == 2)
            {
                mmoDesc.EditValue = description;
            }
            if (_core.Resource != null)
            {
                btnSend.Image = _core.Resource.GetImage("menu_newmail");
                btnClose.Image = _core.Resource.GetImage("image_exit");
            }
        }
        #endregion
        #region[InitCombo]
        void InitCombo()
        {
            Result res = new Result();
            try
            {
                ArrayList Tables = new ArrayList();
                DataTable DT = null;
                string msg = "";

                DictUtility.PrivNo = 110115;

                string[] names = new string[] { "BRANCH","USERS" };
                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д BRANCH оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboGroupID, DT, "BRANCH","NAME");
                }
                DT = (DataTable)Tables[1];
                DTB = DT;
                if (DT == null)
                {
                    msg = "Dictionary-д USERS оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboUserNo, DTB, "USERNO", "USERLNAME");
                }

                if (msg != "")
                    MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[BTN]
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int type = 0;
                if (cboGroupID.EditValue == null && cboUserNo.EditValue == null)
                {
                    MessageBox.Show("Хэрэглэгчийн бүлэг эсвэл хэрэглэгчээ сонгоно уу");
                    cboUserNo.Select();
                }
                else
                {
                    if (cboUserNo.EditValue == null)
                    {
                        type = 1;
                    }
                    Result res = new Result();
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 226, 110117, 110117, new object[] { Static.ToInt(cboGroupID.EditValue), Static.ToInt(cboUserNo.EditValue), Static.ToStr(mmoDesc.EditValue), Static.ToDateTime(_core.TxnDate), type });
                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай илгээгдлээ");
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        private void cboGroupID_EditValueChanged(object sender, EventArgs e)
        {
            cboUserNo.EditValue = null;
            FormUtility.LookUpEdit_SetList(ref cboUserNo, DTB, "USERNO", "USERLNAME", "BRANCHNO=" + Static.ToStr(cboGroupID.EditValue), null);
        }
        #region[FormEvent]
        private void frmSend_Load(object sender, EventArgs e)
        {

        }
        #endregion

        private void frmSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void mmoDesc_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}