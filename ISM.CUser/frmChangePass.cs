using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EServ;
using EServ.Shared;

namespace ISM.CUser
{
    public partial class frmChangePass : Form
    {
        private EServ.Client  moConnection;
        private User moUser;
        Result resCheck = new Result();
        int Count=0;
        
        public frmChangePass(EServ.Client mCon, User mUser)
        {
            InitializeComponent();
            moConnection = mCon;
            moUser = mUser;
        }
        private bool ValidateFields()
        {
            if (moUser.UserPassword != Static.Encrypt(txtOldPass.Text))
            {
                MessageBox.Show("Хуучин нууц үг бүруу байна .", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (txtOldPass.Text.Length == 0)
            {
                return false;
            }
            if (txtNewPass.Text.Length == 0)
            {
                return false;
            }
            if (txtNewPassAgain.Text.Length == 0)
            {
                return false;
            }

            if (txtNewPass.Text != txtNewPassAgain.Text)
            {
                MessageBox.Show("Шинэ нууц үг ижил биш байна.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                if (txtNewPass.Text.Length == 0)
                {
                    MessageBox.Show("Шинэ нууц үг оруулна уу.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == true)
            {
               #region[ Нууц үг солих]
                        string OldPass;
                        string NewPass;
                        object[] M = new object[5];

                        Result res = new Result();

                        
                        OldPass = Static.Encrypt(txtOldPass.Text);
                        NewPass = Static.Encrypt(txtNewPass.Text);
                        M[0] = moUser.UserNo;
                        M[1] = OldPass;
                        M[2] = NewPass;
                        M[3] = txtNewPass.Text.Length;
                        M[4] = Count;
                        try
                        {
                            res = moConnection.Call(moUser.UserNo, 101, 110002, 110002, M);

                            if (res.ResultNo == 0)
                            {
                                moUser.UserPassword = NewPass;

                                MessageBox.Show("Нууц үгийг амжилттай солилоо",
                                    "", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show(res.ResultDesc, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.DialogResult = DialogResult.Cancel;
                        }

                        #endregion
            }
        }
        private void frmChangePass_Load(object sender, EventArgs e)
        {
            Form frmForRef = this;
            resCheck = moConnection.Call(moUser.UserNo, 101, 110113, 110113, new object[] { moUser.UserNo });
            if (resCheck.ResultNo == 0)
            {
                DataRow DR = resCheck.Data.Tables[0].Rows[0];
                if (DR != null)
                {
                    txtNewPass.Properties.Mask.EditMask = DR["MaskValue"].ToString();
                    txtNewPassAgain.Properties.Mask.EditMask = DR["MaskValue"].ToString();
                    lblMask.Text = DR["MaskDescription"].ToString();
                    Count = Static.ToInt(DR["HistoryCount"]);
                }
            }
            else 
            {
                string msg = resCheck.ResultNo + ": " + resCheck.ResultDesc;
                MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            #region[Image]
            btnSaveD.Image = Resource.button_ok;
            btnExitD.Image = Resource.navigate_cancel;
            #endregion
        }
        private void frmChangePass_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form frmForRef = this;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmChangePass_Activated(object sender, EventArgs e)
        {
            txtOldPass.Focus();
        }

        private void frmChangePass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
