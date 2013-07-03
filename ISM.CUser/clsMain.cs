using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

using EServ;
using EServ.Shared;

namespace ISM.CUser
{
    public class Remote
    {
        #region [ Variables ]
        static public string mstrRegPath = "ISM";
        #endregion
        #region [ Properties ]

        private EServ.Client  moConnection;
        public EServ.Client Connection
        {
            get { return moConnection; }
            set { moConnection = value; }
        }

        private User moUser;
        public User User
        {
            get { return moUser; }
            set { moUser = value; }
        }

        private string mstrServerIP = "";
        public string ServerIP
        {
            get { return mstrServerIP; }
            set { mstrServerIP = value; }
        }

        private int mintServerPort = 0;
        public int ServerPort
        {
            get { return mintServerPort; }
            set { mintServerPort = value; }
        }

        public int CheckInterval
        {
            get { return moConnection.CheckInterval; }
            set { moConnection.CheckInterval = value; }
        }

        public int WaitTimeout
        {
            get { return moConnection.WaitTimeout; }
            set { moConnection.WaitTimeout = value; }
        }
        public int IdleTimeout
        {
            get { return moConnection.IdleTimeout; }
            set { moConnection.IdleTimeout = value; }
        }

        public bool IsConnected
        {
            get { return moConnection.Connected; }
        }

        private string mstrApplicationName;
        public string ApplicationName
        {
            get { return mstrApplicationName; }
            set { mstrApplicationName = value; }
        }

        private string mstrApplicationTitle;
        public string ApplicationTitle
        {
            get { return mstrApplicationTitle; }
            set { mstrApplicationTitle = value; }
        }

        private DataSet mstrDS;
        public DataSet DS
        {
            get { return mstrDS;  }
            set { mstrDS = value; }
        }

        #endregion
        #region [ Events ]
        public delegate void DelegateReceived(Result r) ;
        public delegate void DelegateDisConnected() ;
        public event DelegateReceived Received;
        public event DelegateDisConnected DisConnected;
        public void OnReceived(Result r)
        {
            if (Received != null) Received(r);
        }
        public void OnDisConnected()
        {
            if (DisConnected != null) DisConnected();
        }
        #endregion
        #region [ Constructors ]
        public Remote()
        {
            try
            {
                moConnection = new EServ.Client();
                moConnection.EventReceived += new Client.delegateReceived(moConnection_EventReceived);
                moConnection.EventDisconnected += new Client.delegateDisconnected(moConnection_EventDisconnected);

                moConnection.CheckInterval = 60;
                moConnection.IdleTimeout = 60;
                moConnection.WaitTimeout = 60;

                moUser = new User();
                moUser.ComputerName = System.Net.Dns.GetHostName();
                moUser.IPAddress = Static.NetworkGetIp(moUser.ComputerName);
                moUser.NICAddress = Static.NetworkGetNic();

            }
            catch
            { }
        }
        void moConnection_EventDisconnected()
        {
            OnDisConnected();
        }
        void moConnection_EventReceived(Result r)
        {
            OnReceived(r);
        }
        ~Remote()
        {
            moConnection.Disconnect();
            moConnection = null;
            moUser = null;
        }
        //void moConnection_EventDisconnected()
        //{
        //    throw new NotImplementedException();
        //}

        //void moConnection_EventReceived(EServ.Shared.Result r)
        //{
        //    throw new NotImplementedException();
        //}

        //void moConnection_Received(int rettype, int rettraceno, string retdesc, object retdata, object[] retparams)
        //{
        //    if (Received != null)
        //        Received(rettype, rettraceno, retdesc, retdata, retparams);
        //}
        //void moConnection_Disconnected()
        //{
        //    if (DisConnected != null)
        //        DisConnected();
        //}
        #endregion
        #region [ Functions ]
        public DialogResult ShowLogin(Remote pRemote)
        {
            frmLogin frm = new frmLogin(pRemote, "");
            return frm.ShowDialog();
        }
        public DialogResult ShowChangePass(Remote pRemote)
        {
            frmChangePass frm = new frmChangePass(moConnection, moUser);
            return frm.ShowDialog();
        }
        public Result Reconnect()
        {
            return Login(moUser.UserNo, moUser.UserPassword, moUser.ComputerName, moUser.IPAddress, moUser.NICAddress, true);
        }
        /// <summary>
        /// Холболт хийх
        /// </summary>
        /// <param name="ispos">Посоос холбогдож байгаа бол - 1</param>
        /// <returns></returns>
        public Result Reconnect(int ispos)
        {
            return Login(moUser.UserNo, moUser.UserPassword, moUser.ComputerName, moUser.IPAddress, moUser.NICAddress, true, 1);
        }
        public Result Login(int userno, string pwd, string pcname, string ip, string mac, bool relogin)
        {
            try
            {
                Result res = new Result();
                moConnection.CheckInterval = Static.ToInt(Static.RegisterGet(mstrRegPath, "Login", "CheckInterval", "10"));
                moConnection.WaitTimeout = Static.ToInt(Static.RegisterGet(mstrRegPath, "Login", "WaitTimeout", "60"));
                moConnection.Disconnect();
                moConnection.Connect(mstrServerIP, mintServerPort);
                if (relogin == false)
                {
                    ip = moConnection.IpLocal;
                    mac = moConnection.NetworkAddress ;
                }

                if (moConnection.Connected)
                {                 
                    object[] param = new object[] { userno, pwd, pcname, ip, mac, relogin };
                    res = moConnection.Call(userno, 101, 110000, 110000, param);

                    if (res.ResultNo == 0)
                    {
                        moUser.UserNo = userno;
                        moUser.BranchCode = Static.ToStr(res.Param[0]);
                        moUser.UserFName   = Static.ToStr(res.Param[1]);
                        moUser.UserFName2  = Static.ToStr(res.Param[2]);
                        moUser.UserLevel = Static.ToInt(res.Param[3]);
                        moUser.UserLName = Static.ToStr(res.Param[4]);
                        moUser.UserLName2 = Static.ToStr(res.Param[5]);

                        moUser.Level1 = Static.ToInt(res.Param[6]);
                        moUser.Level2 = Static.ToInt(res.Param[7]);
                        moUser.Level3 = Static.ToInt(res.Param[8]);
                        moUser.Level4 = Static.ToInt(res.Param[9]);

                        moUser.TxnGroupLevel = Static.ToInt(res.Param[10]);

                        mstrDS = res.Data;
                    }
                    return res;
                }
                else
                {
                    // Серверт холбогдож чадсангүй
                    return new Result(7, "Серверт холбогдож чадсангүй!");
                }
            } 
            catch (Exception ex)
            {
                return new Result(7, ex.Message);
            }
        }
        public Result Login(int userno, string pwd, string pcname, string ip, string mac, bool relogin,int ispos)
        {
            try
            {
                moConnection.CheckInterval = Static.ToInt(Static.RegisterGet(mstrRegPath, "Login", "CheckInterval", "10"));
                moConnection.WaitTimeout = Static.ToInt(Static.RegisterGet(mstrRegPath, "Login", "WaitTimeout", "60"));
                moConnection.Disconnect();
                moConnection.Connect(mstrServerIP, mintServerPort);
                if (relogin == false)
                {
                    ip = moConnection.IpLocal;
                    mac = moConnection.NetworkAddress;
                }

                if (moConnection.Connected)
                {
                    Result res = moConnection.Call(userno, 101, 110003, 110003, new object[] { moUser.PosNo });
                    if (res.ResultNo != 0) return res;
                    else moUser.AreaCode = Static.ToStr(res.Data.Tables[0].Rows[0]["AreaCode"]);

                    object[] param = new object[] { userno, pwd, pcname, ip, mac, relogin };
                    res = moConnection.Call(userno, 101, 110000, 110000, param);

                    if (res.ResultNo == 0)
                    {
                        moUser.UserNo = userno;
                        moUser.BranchCode = Static.ToStr(res.Param[0]);
                        moUser.UserFName = Static.ToStr(res.Param[1]);
                        moUser.UserFName2 = Static.ToStr(res.Param[2]);
                        moUser.UserLevel = Static.ToInt(res.Param[3]);
                        moUser.UserLName = Static.ToStr(res.Param[4]);
                        moUser.UserLName2 = Static.ToStr(res.Param[5]);

                        moUser.Level1 = Static.ToInt(res.Param[6]);
                        moUser.Level2 = Static.ToInt(res.Param[7]);
                        moUser.Level3 = Static.ToInt(res.Param[8]);
                        moUser.Level4 = Static.ToInt(res.Param[9]);

                        moUser.TxnGroupLevel = Static.ToInt(res.Param[10]);

                        mstrDS = res.Data;
                    }
                    return res;
                }
                else
                {
                    // Серверт холбогдож чадсангүй
                    return new Result(7, "Серверт холбогдож чадсангүй!");
                }
            }
            catch (Exception ex)
            {
                return new Result(7, ex.Message);
            }
        }
        public Result Logout()
        {
            try
            {
                return moConnection.Call(moUser.UserNo, 101, 110001, 110001, null);
            }
            catch (Exception ex)
            {
                return new Result(7, ex.Message);
            }
        }
        //public Result ChangePass(int userno, string oldpwd, string newpwd)
        //{
        //    try
        //    {
        //        object[] param = new object[] { userno, oldpwd, newpwd };
        //        Result res = moConnection.Call(userno, 4000, 110002, 110002, param);

        //        if (res.ResultNo == 0)
        //        {
        //            moUser.UserPassword = newpwd;
        //         }
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Result(7, ex.Message);
        //    }
        //}
        public void Disconnect()
        {
            moConnection.Disconnect();
        }
        public bool GetTxn(long txncode)
        {
            bool exits = false;

            try
            {
                DataRow[] row = mstrDS.Tables["TXN"].Select("trancode=" + txncode.ToString());

                if (row.Count() > 0)
                    return exits = true;
                else
                    return exits = false;
            }
            catch (Exception ex)
            {
                exits = false;
                return exits;
            }
            return exits;
        }
        #endregion
     }
}
