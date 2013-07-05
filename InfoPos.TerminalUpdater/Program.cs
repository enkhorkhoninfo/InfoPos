using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using System.Threading;
using System.Reflection;
using System.Collections;
using EServ.Shared;

namespace InfoPos.Updater
{
    public static class Program
    {
        private static int _selfupdate = 0;
        public static int selfupdate
        {
            get { return _selfupdate; }
            set { _selfupdate = value; }
        }
        private static string _updatefiles = "";
        public static string updatefiles
        {
            get { return _updatefiles; }
            set { _updatefiles = value; }
        }
        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        /////        
        ///// 
        [STAThread]
        static void Main()
        {
            try
            {

                if (IsAlreadyRunning()||terminal())
                {
                    MessageBox.Show(null, "Энэхүү файл ашиглагдаж байгаа тул дахин нээх боломжгүй байна.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Application.EnableVisualStyles();
                    if (File.Exists(@".\start.bat"))
                    {
                       // Process.Start(@".\start.bat");
                    }
                    Application.Run(new frmUpdateMain());
                    if (File.Exists(@".\end.bat"))
                    {
                        //Process.Start(@".\end.bat");
                    }
                    if (selfupdate == 1)
                    {
                        if (File.Exists(@".\temp\updatefiles.txt"))
                            File.Delete(@".\temp\updatefiles.txt");

                        System.IO.File.WriteAllText(@".\temp\updatefiles.txt", updatefiles);
                        Process proc = new Process();
                        proc.StartInfo.FileName = @".\InfoPos.UpdaterCopy.exe";
                        proc.StartInfo.Arguments = @".\temp\updatefiles.txt";
                        proc.StartInfo.LoadUserProfile = true;
                        proc.Start();
                        selfupdate = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static Mutex mutex;
        private static bool IsAlreadyRunning()
        {
            string strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            string sExeName = fileInfo.Name;
            bool bCreatedNew;

            mutex = new Mutex(true, "Global\\" + sExeName, out bCreatedNew);
            if (bCreatedNew)
                mutex.ReleaseMutex();

            return !bCreatedNew;
        }
        private static bool terminal()
        {
            bool bCreatedNew;
            mutex = new Mutex(true,"Global\\" + "InfoPos.Admin.exe", out bCreatedNew);
            if (bCreatedNew)
                mutex.ReleaseMutex();

            return !bCreatedNew;
        }
    }
}
