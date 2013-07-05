using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Security.Permissions;
using Microsoft.Win32;
namespace InfoPos.Admin
{
	class MainForm
	{
        static private readonly InfoPos.Core.Core _core = new InfoPos.Core.Core();
        internal static frmSplash _splash;
        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr procHandle, Int32 min, Int32 max);


        static private bool Login()
        {
            try
            {
                if (_core.RemoteObject.ShowLogin(_core.RemoteObject) == DialogResult.OK)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message);
            }
            return false;
        }

        internal static void SetProcessWorkingSetSize()
        {
            try
            {
                Process CurProcess = Process.GetCurrentProcess();
                SetProcessWorkingSetSize(CurProcess.Handle, -1, -1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        internal static void ClearMemory()
        {
            SetProcessWorkingSetSize();
            GC.Collect();
        }

        [STAThread]
        static void Main()
        {
            try
            {
                #region SETUP Application definations ]

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                CultureInfo myCulture = new CultureInfo("en-US");

                DateTimeFormatInfo myDateTime = new DateTimeFormatInfo();
                myDateTime.ShortDatePattern = "yyyy.MM.dd";
                myDateTime.ShortTimePattern = "HH:mm:ss";
                myCulture.DateTimeFormat = myDateTime;

                NumberFormatInfo myNumber = new NumberFormatInfo();
                myNumber.CurrencyDecimalSeparator = ".";
                myNumber.CurrencyGroupSeparator = ",";
                myNumber.NumberDecimalSeparator = ".";
                myNumber.NumberGroupSeparator = ",";
                myCulture.NumberFormat = myNumber;

                Thread.CurrentThread.CurrentCulture = myCulture;
                #endregion

                if (Login())
                {
                    _splash = new frmSplash();
                    _splash.Show();

                    Application.DoEvents();
                    Application.AddMessageFilter(new MyFilter());
                    Application.Run(new frmMain(_core));
                    _core.RemoteObject.Disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public class MyFilter : IMessageFilter
        {
            private const int WM_KEYDOWN = 256;
            private const int WM_MOUSEMOVE = 512;

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == WM_KEYDOWN
                    || m.Msg == WM_MOUSEMOVE)
                {
                    frmMain.ResetTimer();
                }

                return false;
            }
        }
	}
}
