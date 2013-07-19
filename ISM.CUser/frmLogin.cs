using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

using EServ.Shared;
using EServ;
using ISM.Lib;
using lib = ISM.Lib.Static;

namespace ISM.CUser
{
    internal partial class frmLogin : Form
    {
        Remote moRemote;
        string _xmlcachename = "";
        object _xmlcache = null;

        #region [ Form events ]
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                _xmlcachename = string.Format(@"{0}\Data\Settings.xml", lib.WorkingFolder);
                _xmlcache = ISM.Lib.Cache.XMLCacheOpen(_xmlcachename);
                numUserNo.Text = ISM.Lib.Cache.XMLCacheGetStr(_xmlcache, "UserNo", "0");
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLoginDataToReg();
        }
        public frmLogin(Remote pRemote, string pRegPath)
        {
            try
            {
                InitializeComponent();
                _xmlcachename = string.Format(@"{0}\Data\Settings.xml", lib.WorkingFolder);
                _xmlcache = ISM.Lib.Cache.XMLCacheOpen(_xmlcachename);
                
                moRemote = pRemote;
                numUserNo.Text = ISM.Lib.Cache.XMLCacheGetStr(_xmlcache, "UserNo", "0");
                //numUserNo.Text = EServ.Shared.Static.ToStr(EServ.Shared.Static.RegisterGet(Remote.mstrRegPath, "Login", "UserNo", "0"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reg: " + ex.ToString());
            }
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lang: " + ex.ToString());
            }
        }
         #endregion
        #region [ Functions ]
        private void SaveLoginDataToReg()
        {
            try
            {
                //EServ.Shared.Static.RegisterSave(Remote.mstrRegPath, "Login", "UserNo", numUserNo.Text);
                ISM.Lib.Cache.XMLCacheSet(_xmlcache, "UserNo", numUserNo.Text);
                ISM.Lib.Cache.XMLCacheSave(_xmlcachename, _xmlcache);
            }
            catch
            {
            }
        }
        private bool ValidateFields()
        {
            // хэрэв шаардлагтай бүх талбарууд бөглөгдсөн бол 'Холбогдох' товчийг идэвхтэй болгоно
            if (numUserNo.Text.Length == 0)
            {
                //btnConnect.Enabled = false;
                return false;
            }
            if (txtPassword.Text.Length == 0)
            {
                //btnConnect.Enabled = false;
                return false;
            }
            return true;
            //btnConnect.Enabled = true;
        }
        #endregion
        #region [ Button events ]
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;
            Result res;

            //moRemote.ServerIP = Static.ToStr(Static.RegisterGet(Remote.mstrRegPath, "Login", "Server", ""));
            //moRemote.ServerPort = Static.ToInt(Static.RegisterGet(Remote.mstrRegPath, "Login", "PortNo", ""));
            //moRemote.User.UserNo = Static.ToInt(numUserNo.Text);
            //moRemote.User.UserPassword = Static.Encrypt(txtPassword.Text);
            //moRemote.User.ComputerName = SystemInformation.ComputerName;


            moRemote.ServerIP = ISM.Lib.Cache.XMLCacheGetStr(_xmlcache, "Server", "");
            moRemote.ServerPort = ISM.Lib.Cache.XMLCacheGetInt(_xmlcache, "PortNo", 8888);
            moRemote.User.UserNo = EServ.Shared.Static.ToInt(numUserNo.Text);
            moRemote.User.UserPassword = EServ.Shared.Static.Encrypt(txtPassword.Text);
            moRemote.User.ComputerName = SystemInformation.ComputerName;

            ISM.Lib.Cache.XMLCacheSet(_xmlcache, "UserNo", numUserNo.Text);
            ISM.Lib.Cache.XMLCacheSave(_xmlcachename, _xmlcache);

            #region[Language]
            if (btnLan.Text == "EN")
                moRemote.User.UserLanguage = "MN";
            else
                moRemote.User.UserLanguage = "EN";
            #endregion
            
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            
            res = moRemote.Reconnect();

            this.Cursor = System.Windows.Forms.Cursors.Default;

            switch (res.ResultNo )
            {
                case 0: //  амжилттай холбогдсон
                    if (!(bool)res.Param[6])
                    {
                     ISM.CUser.frmChangePass frm=new frmChangePass(moRemote.Connection, moRemote.User);
                     frm.ShowDialog();
                    }
                    
                    this.DialogResult = DialogResult.OK;
                    break;
                default:
                    moRemote.Disconnect();
                    MessageBox.Show(res.ResultDesc );
                    break;
            }
        }       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSetup_Click(object sender, EventArgs e)
        {
            Form frm = new frmLoginSetup();
            frm.ShowDialog();
        }
        #endregion
        private void frmLogin_Activated(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
        private void btnLan_Click(object sender, EventArgs e)
        {
            if (btnLan.Text == "EN")
                btnLan.Text = "MN";
            else
                btnLan.Text = "EN";
        }
    }
}