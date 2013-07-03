using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;
using ISM.Touch;
namespace InfoPos
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        TouchKeyboard kb = new TouchKeyboard();
        #region Constructors
        public frmLogin()
        {
            InitializeComponent();
            try
            {
                #region lblVersion -ийг тунгалаг болгох
                var pos = this.PointToScreen(lblVersion.Location);
                pos = pictureBox1.PointToClient(pos);
                lblVersion.Parent = pictureBox1;
                lblVersion.Location = pos;
                lblVersion.BackColor = Color.Transparent;
                lblVersion.Text = "Хувилбар " + Application.ProductVersion;
                #endregion
                #region IsTouch -ийг тунгалаг болгох
                pos = this.PointToScreen(IsTouch.Location);
                pos = pictureBox1.PointToClient(pos);
                IsTouch.Parent = pictureBox1;
                IsTouch.Location = pos;
                IsTouch.BackColor = Color.Transparent;
                #endregion

                if (Program.Core.Resource != null)
                {
                    btnConnect.Image = Program.Core.Resource.GetImage("image_ok");
                    btnCancel.Image = Program.Core.Resource.GetImage("image_exit");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Control Events
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                #region Prepare values

                ISM.CUser.Remote remote = Program.Core.RemoteObject;

                Program.Core.CacheSet("Connection_UserNo", numUser.EditValue);

                remote.ServerIP = Program.Core.CacheGetStr("Connection_Ip");
                remote.ServerPort = Program.Core.CacheGetInt("Connection_Port");
                remote.User.UserNo = Program.Core.CacheGetInt("Connection_UserNo");
                remote.User.PosNo = Program.Core.CacheGetStr("Connection_PosNo");
                remote.User.UserPassword = ISM.Lib.Static.Encrypt(txtPwd.Text);
                remote.User.ComputerName = SystemInformation.ComputerName;
                remote.User.UserLanguage = "MN";

                if (IsTouch.Checked)
                {
                    Program.Core.IsTouch = true;
                    Program.Core.CacheSet("IsTouch", 1);
                }
                else
                {
                    Program.Core.IsTouch = false;
                    Program.Core.CacheSet("IsTouch", 0);
                }
                remote.WaitTimeout = 10;
                remote.IdleTimeout = 20;
                remote.CheckInterval = 8;

                Program.Core.CacheSave();


                #endregion

                #region Connect to server

                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                Result res = remote.Reconnect(1);

                this.Cursor = System.Windows.Forms.Cursors.Default;

                switch (res.ResultNo)
                {
                    case 0: //  амжилттай холбогдсон
                        if (!(bool)res.Param[6])
                        {
                            //ISM.CUser.frmChangePass frm = new frmChangePass(moRemote.Connection, moRemote.User);
                            //frm.ShowDialog();
                        }

                        this.DialogResult = DialogResult.OK;
                        break;
                    default:
                        remote.Disconnect();
                        MessageBox.Show(res.ResultDesc, "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        private void frmLogin_Load(object sender, EventArgs e)
        {
            int _IsTouch = Static.ToInt(Program.Core.CacheGet("IsTouch"));
            numUser.EditValue = Program.Core.CacheGet("Connection_UserNo");
            kb.AddToKeyboard(numUser);
            kb.AddToKeyboard(txtPwd);
            txtPwd.Focus();
            if (_IsTouch == 1)
            {
                IsTouch.Checked = true;
                kb.Enable = true;
            }
            else { IsTouch.Checked = false; kb.Enable = false; }
            
        }

        private void IsTouch_CheckedChanged(object sender, EventArgs e)
        {
            if (IsTouch.Checked == true)
            {
                kb.Enable = true;
            }
            else
            {
                kb.Enable = false;
            }
        }
    }
}
