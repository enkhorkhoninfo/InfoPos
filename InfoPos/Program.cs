using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using DevExpress.Skins;

using Lib = ISM.Lib.Static;
using Cache = ISM.Lib.Cache;
using Util = ISM.Lib.Cache;

namespace InfoPos
{
    public static class Program
    {
        private static InfoPos.Core.Core _core = null;
        public static InfoPos.Core.Core Core
        {
            get { return _core;}
        }
        private static bool _isadmin = false;
        public static bool IsAdmin
        {
            get { return _isadmin; }
        }
        static void ConsoleMode(string[] args)
        {
            foreach (string s in args)
            {
                if (s.ToLower() == "-admin")
                {
                    _isadmin = true;
                }
            }
            return;
        }
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                #region Initialization variables

                _core = new Core.Core();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.BonusSkins).Assembly);
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Money Twins");
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

                #region Check parameters

                ConsoleMode(args);
                if (_isadmin)
                {
                    frmSettings frm = new frmSettings();
                    frm.ShowDialog();
                    return;
                }

                #endregion

                #region Login form

                frmLogin login = new frmLogin();
                DialogResult res = login.ShowDialog();
                if (res != DialogResult.OK) return;

                #endregion

                #region Load main form

                Application.Run(new frmInfoPos());
                _core.RemoteObject.Disconnect();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
