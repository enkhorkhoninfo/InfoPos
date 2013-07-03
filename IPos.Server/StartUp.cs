using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

using EServ;
using EServ.Data;
using EServ.Interface;
using EServ.Shared;

namespace Development
{
    public partial class StartUp : Form
    {
        #region Constractor
        public StartUp()
        {
            InitializeComponent();

            EServ.Interface.Events.EventServerPortStarted += new EServ.Interface.Events.delegateEventServerPort(Events_EventServerPortStarted);
            EServ.Interface.Events.EventServerPortStopped += new EServ.Interface.Events.delegateEventServerPort(Events_EventServerPortStopped);
            EServ.Interface.Events.ServerTraceLog += new EServ.Interface.Events.delegateServerTraceLog(Events_ServerTraceLog);
            EServ.Interface.Events.EventUserConnected += new EServ.Interface.Events.delegateEventUserConnected(Events_EventUserConnected);
            EServ.Interface.Events.EventUserDisconnected += new EServ.Interface.Events.delegateEventUserDisconnected(Events_EventUserDisconnected);
            EServ.Interface.Events.EventReceiveTimeout += new EServ.Interface.Events.delegateEventReceiveTimeout(Events_EventReceiveTimeout);
            EServ.Interface.Events.EventReceiveTerminated += new EServ.Interface.Events.delegateEventReceiveTerminated(Events_EventReceiveTerminated);
            EServ.Interface.Events.EventReceiving += new EServ.Interface.Events.delegateEventReceiving(Events_EventReceiving);
            EServ.Interface.Events.EventReceived += new EServ.Interface.Events.delegateEventReceived(Events_EventReceived);

            EServ.Interface.Console.EventConsoleLog += new EServ.Interface.Console.delegateEventConsoleLog(Console_EventConsoleLog);
        }

        #endregion

        #region EServ Events

        void Events_EventServerPortStopped(string ip, int port, string protocol, bool success)
        {
            LogInvoke(0, string.Format("\t{0}:{1} ({2})      {3}", ip, port, protocol, success ? "Stopped" : "Error"));
        }
        void Events_EventServerPortStarted(string ip, int port, string protocol, bool success)
        {
            LogInvoke(0, string.Format("\t{0}:{1} ({2})      {3}", ip, port, protocol, success ? "Started" : "Error"));
        }

        void Events_EventReceived(EServ.Interface.ClientInfo ci, EServ.Interface.RequestInfo ri, EServ.Data.DbConnections dbs, int ReceivedBytes, ref Result res, ref bool cancel)
        {
            Log(0, string.Format("EventReceived: {0} usr={1} req={2} fid={3} fnc={4} bytes={5}", ci.ClientIp, ri.UserNo, ri.RequestNo, ri.FileId, ri.FunctionNo, ri.DataSize));
        }
        void Events_EventReceiving(EServ.Interface.ClientInfo ci, int ReceivedBytes)
        {
            LogInvoke(0, string.Format("EventReceiving: {0} bytes={1}", ci.ClientIp, ReceivedBytes));
        }
        void Events_EventReceiveTerminated(EServ.Interface.ClientInfo ci, int ReceivedBytes, EServ.Interface.enumReceiveErrorType Reason)
        {
            LogInvoke(0, string.Format("EventReceiveTerminated: {0} reason={1}", ci.ClientIp, Reason.ToString()));
        }
        void Events_EventReceiveTimeout(EServ.Interface.ClientInfo ci, int ReceivedBytes, int TimeoutSeconds)
        {
            LogInvoke(0, string.Format("EventReceiveTimeout: {0} timeout={1}", ci.ClientIp, TimeoutSeconds));
        }
        void Events_EventUserDisconnected(EServ.Interface.ClientInfo ci)
        {
            LogInvoke(0, string.Format("EventUserDisconnected: {0}", ci.ClientIp));
        }
        void Events_EventUserConnected(EServ.Interface.ClientInfo ci)
        {
            LogInvoke(0, string.Format("EventUserConnected: {0}", ci.ClientIp));
        }
        void Events_ServerTraceLog(int jpno, string message)
        {
            Log(jpno, message);
        }

        void Console_EventConsoleLog(string message, EServ.Interface.Console.enumConsoleLogType logtype)
        {
            switch (logtype)
            {
                case EServ.Interface.Console.enumConsoleLogType.Main:
                    Log(0, message);
                    break;
                case EServ.Interface.Console.enumConsoleLogType.Process:
                    LogProcess(0, message);
                    break;
            }
        }
        #endregion

        #region Log Methods
        internal void LogInvoke(int jpno, string message)
        {
            if (txtLog.TextLength > 0) txtLog.AppendText("\r\n");
            txtLog.AppendText(message);
        }
        public void Log(int jpno, string s)
        {
            txtLog.Invoke(new EServ.Interface.Events.delegateServerTraceLog(LogInvoke), jpno, s);
        }

        internal void LogProcessInvoke(int jpno, string message)
        {
            if (txtLogProcess.TextLength > 0) txtLogProcess.AppendText("\r\n");
            txtLogProcess.AppendText(message);
        }
        public void LogProcess(int jpno, string s)
        {
            txtLogProcess.Invoke(new EServ.Interface.Events.delegateServerTraceLog(LogProcessInvoke), jpno, s);
        }

        #endregion

        #region Form Events
        private void StartUp_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool success = EServ.Server.PortCloseAll();
            EServ.Server.ServerStop(true);
        }
        private void btnPrepare_Click(object sender, EventArgs e)
        {
            LogInvoke(0, "Preparing server parameters...");

            EServ.Server.IniFileRead("");

            LogInvoke(0, "Prepared server parameters.");
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            LogInvoke(0, "Listenning ports...");
            bool success = EServ.Server.PortOpenAll();
            EServ.Server.ServerStart();

        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            EServ.Server.ServerStop(true);
        }
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }
        #endregion

        #region EOD Events

        private Result Invoke(string dll, string cls, string fnc, int funcno, int privno, object[] param)
        {
            Result r = new Result();

            #region Prepare log info

            Log log = new EServ.Interface.Log(
                EServ.Interface.Sequence.NextByVal("LogId")
                ,DateTime.Now
                ,privno
                ,99999
                );

            log.item.TxnDate = DateTime.Now;
            log.item.SupervisorNo = 0;
            log.item.BranchNo = 0;

            #endregion
            #region Instance: Load assemble file

            Assembly asm = null;
            try
            {
                asm = Assembly.LoadFile(string.Format("{0}\\{1}", EServ.Interface.Server.WorkingFolder, dll));
            }
            catch (Exception e)
            {
                r.ResultNo = 9;
                r.ResultDesc = string.Format("CONSOLE{0}: Component file not found. Info: dll={1} err={2}"
                    , 0
                    , dll
                    , e.Message);

                return r;
            }

            #endregion
            #region Instance: Create class instance
            object obj = null;
            try
            {
                obj = Activator.CreateInstance(asm.GetType(cls, false, true), null);
            }
            catch (Exception e)
            {
                r.ResultNo = 9;
                r.ResultDesc = string.Format("CONSOLE{0}: Class not found. Info: dll={1} cls={2} err={3}"
                    , 0, dll, cls, e.Message);

                return r;
            }

            #endregion
            #region Instance: Casting to interface class
            IModule mod = null;
            try
            {
                mod = (IModule)obj;
            }
            catch (Exception e)
            {
                r.ResultNo = 9;
                r.ResultDesc = string.Format("CONSOLE{0}: Server class does not match with module interface. Info: dll={1} cls={2} err={3}"
                    , 0, dll, cls, e.Message);

                return r;
            }

            #endregion
            #region Instance: Prepare parameters
            EServ.Interface.ClientInfo client = new ClientInfo();
            EServ.Interface.RequestInfo request = new RequestInfo(0, 99999, funcno, 0);

            DbConnections dbs = null;
            bool success = EServ.Interface.Server.CreateDbs("CONSOLE", out dbs);
            #endregion
            #region Instance: Invoke method
            r = mod.Invoke(client, request, dbs, ref log);
            if (r == null)
            {
                r = new Result(9, "No result is returned!");
            }
            log.item.ResultNo = r.ResultNo;
            log.item.ResultDesc = r.ResultDesc;
            EServ.Interface.Server.LogAdd(log, request);

            #endregion

            return r;
        }

        private void btnEODList_Click(object sender, EventArgs e)
        {
            try
            {
                Result r = Invoke("HPro.Process.dll", "HPro.Process.Process", "Invoke", 1, 1, null);
                LogProcess(0, r.ResultDesc);
            }
            catch (Exception ex)
            {
                LogProcess(0, ex.ToString());
            }
        }
        private void btnEODStart_Click(object sender, EventArgs e)
        {
            try
            {
                Result r = Invoke("HPro.Process.dll", "HPro.Process.Process", "Invoke", 2, 2, null);
                LogProcess(0, r.ResultDesc);
            }
            catch (Exception ex)
            {
                LogProcess(0, ex.ToString());
            }
        }
        private void btnClearLogProcess_Click(object sender, EventArgs e)
        {
            txtLogProcess.Clear();
        }
        #endregion

        private void StartUp_Load(object sender, EventArgs e)
        {

        }
    }
}
