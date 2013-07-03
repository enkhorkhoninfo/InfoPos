using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace ISM.CUser
{
    
    public partial class frmLock : Form
    {
        ISM.CUser.Remote _remote;
        string _AppTitle;

        public frmLock(ISM.CUser.Remote remote, string AppTitle)
        {
            InitializeComponent();
            _AppTitle = AppTitle;
            _remote = remote;
        }

        private void frmLock_Load(object sender, EventArgs e)
        {
            userName.Text = Static.ToStr(_remote.User.UserNo);
        }

        #region [ Functions ]

        private bool CheckPassword(string Password)
        {
            string strWrongPass = "Нууц үг буруу байна.";
            if (Static.Encrypt(Password) == _remote.User.UserPassword)
            {
                return true;
            }
            MessageBox.Show(strWrongPass, _AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        #endregion

        private void loginButton_Click(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmLock_Activated(object sender, EventArgs e)
        {
            if (password.Enabled) password.Focus();
        }

        private void loginButton_Click_1(object sender, EventArgs e)
        {

            if (password.Text == "") return;
            
            if (CheckPassword(password.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                password.Focus();
            }
        }

        private void closeButton_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}



