using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Collections;
using System.Diagnostics;
using EServ.Shared;
using ISM.CUser;

namespace InfoPos.Updater
{
    public partial class frmUpdateMain : XtraForm
    {
        string Terminaldir = "";
        string Docdir = "";
        string Slipsdir = "";
        string Reportdir = "";
        static public string mstrRegPath = "ISM";
        private EServ.Client moConnection;
        private User moUser;
        bool selfupdate = false;
        public frmUpdateMain()
        {
            try
            {
                InitializeComponent();
                btnUpdate.Image = InfoPos.TerminalUpdater.Resource.update;
                btnClose.Image = InfoPos.TerminalUpdater.Resource.exit;
                btnSettings.Image = InfoPos.TerminalUpdater.Resource.settings;
                Terminaldir = Static.ToStr(InfoPos.TerminalUpdater.Cache.XMLCacheGet("frmOption_TempPath"));
                Docdir = Static.ToStr(InfoPos.TerminalUpdater.Cache.XMLCacheGet("frmOption_DynamicPathIn"));
                Slipsdir = Static.ToStr(InfoPos.TerminalUpdater.Cache.XMLCacheGet("frmOption_SlipsPathIn"));
                Reportdir = Static.ToStr(InfoPos.TerminalUpdater.Cache.XMLCacheGet("frmOption_ReportPathIn"));

                moConnection = new EServ.Client();
                moUser = new User();
                moUser.ComputerName = System.Net.Dns.GetHostName();
                moUser.IPAddress = Static.NetworkGetIp(moUser.ComputerName);
                moUser.NICAddress = Static.NetworkGetNic();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Hashtable namedate = new Hashtable();
            Hashtable dochash = new Hashtable();
            Hashtable rephash = new Hashtable();
            Hashtable slipshash = new Hashtable();
            marqueeProgressBarControl1.Properties.Stopped = false;
            //Terminaldir = Static.ToStr(Static.RegisterGet(mstrRegPath, "Login", "TerminalPath", @"C:\Program Files\HeavenPro\Terminal"));
            string msg = "";
            bool check=true;
            //Өөр дээр байгаа файлуудын мэдээлэл авч байгаа хэсэг
            #region[Терминалын файлууд]
            if (Directory.Exists(Terminaldir))
            {
                string[] tfilenames = Directory.GetFiles(Terminaldir);
                object[] tmodifeddate = new object[tfilenames.Count()];
                string[] fname = new string[tfilenames.Count()];
                int i = 0;
                foreach (string name in tfilenames)
                {
                    tmodifeddate[i] = File.GetLastWriteTime(name);
                    FileInfo fileinfo = new FileInfo(name);
                    fname[i] = fileinfo.Name;
                    namedate.Add(fname[i], tmodifeddate[i]);
                    i++;
                }
            }
            else
            {
                msg = "Терминал байрлаж буй хавтасыг буруу тохируулсан байна.";
                check=false;
            }
            #endregion         
            #region[Документ загварын файлууд]
            if (Directory.Exists(Docdir))
            {
                string[] docfilenames = Directory.GetFiles(Docdir);
                object[] docmodifeddate = new object[docfilenames.Count()];
                string[] docname = new string[docfilenames.Count()];
                int i = 0;
                foreach (string name in docfilenames)
                {
                    docmodifeddate[i] = File.GetLastWriteTime(name);
                    FileInfo fileinfo = new FileInfo(name);
                    docname[i] = fileinfo.Name;
                    dochash.Add(docname[i], docmodifeddate[i]);
                    i++;
                }
            }
            else
            {
                if (msg != "")
                    msg = msg + "\r\n" + "Документ загвар байрлаж буй хавтасыг буруу тохируулсан байна.";
                else msg = "Документ загвар байрлаж буй хавтасыг буруу тохируулсан байна.";
                check=false;
            }
            #endregion
            #region[Динамик тайлангийн файлууд]
            if (Directory.Exists(Reportdir))
            {
                string[] repfilenames = Directory.GetFiles(Reportdir);
                object[] repmodifeddate = new object[repfilenames.Count()];
                string[] repname = new string[repfilenames.Count()];
                int i = 0;
                foreach (string name in repfilenames)
                {
                    repmodifeddate[i] = File.GetLastWriteTime(name);
                    FileInfo fileinfo = new FileInfo(name);
                    repname[i] = fileinfo.Name;
                    rephash.Add(repname[i], repmodifeddate[i]);
                    i++;
                }
            }
            else
            {
                if (msg != "")
                    msg = msg + "\r\n" + "Динамик тайлан байрлаж буй хавтасыг буруу тохируулсан байна.";
                else msg = "Динамик тайлан байрлаж буй хавтасыг буруу тохируулсан байна.";
                check=false;
            }
            #endregion
            #region[Slips тайлангийн файлууд]
            if (Directory.Exists(Slipsdir))
            {
                string[] slipsfilenames = Directory.GetFiles(Slipsdir);
                object[] slipsmodifeddate = new object[slipsfilenames.Count()];
                string[] slipsname = new string[slipsfilenames.Count()];
                int i = 0;
                foreach (string name in slipsfilenames)
                {
                    slipsmodifeddate[i] = File.GetLastWriteTime(name);
                    FileInfo fileinfo = new FileInfo(name);
                    slipsname[i] = fileinfo.Name;
                    slipshash.Add(slipsname[i], slipsmodifeddate[i]);
                    i++;
                }
            }
            else
            {
                if (msg != "")
                msg = msg + "\r\n" + "Slips тайлан байрлаж буй хавтасыг буруу тохируулсан байна.";
                else msg = "Slips тайлан байрлаж буй хавтасыг буруу тохируулсан байна.";
                check=false;
            }
            #endregion
            if (check)
            {
                Login(namedate, dochash, rephash, slipshash);
            }
            else
            {
                MessageBox.Show(this, msg, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void EndWriteCallback(IAsyncResult result)
        {
            this.Close();
        }
        private void Login(Hashtable namedate,Hashtable dochash,Hashtable rephash,Hashtable slipshash)
        {
            try
            {
                FileStream fs = null;
                moConnection.WaitTimeout = Static.ToInt(Static.RegisterGet(mstrRegPath, "Login", "TimeOut", 1));
                string serverip = Static.ToStr(Static.RegisterGet(mstrRegPath, "Login", "Server", ""));
                int portno = Static.ToInt(Static.RegisterGet(mstrRegPath, "Login", "PortNo", ""));
                int userno = Static.ToInt(Static.RegisterGet(mstrRegPath, "Login", "UserNo", ""));
                string[] tfilename = Directory.GetDirectories(Terminaldir);
                if (serverip != "" || portno != 0)
                {
                    moConnection.Connect(serverip, portno);
                    if (moConnection.Connected)
                    {
                        string cas = "";

                        Result res = moConnection.Call(userno, 224, 100003, 100003, new object[] { namedate, dochash, rephash, slipshash });
                        if (res.ResultNo == 0)
                        {
                            Hashtable hash = (Hashtable)res.Param[0];
                            if (hash.Count != 0)
                            {
                                if (!Directory.Exists(@Terminaldir + @"\temp"))
                                {
                                    Directory.CreateDirectory(@Terminaldir + @".\temp");
                                }
                                for (int j = 0; j < hash.Count; j++)
                                {
                                    ArrayList array = (ArrayList)hash[j];
                                    byte[] bytes = (byte[])array[1];
                                    fs = File.Create(Terminaldir + "\\temp\\" + Static.ToStr(array[0]));
                                    fs.Write(bytes, 0, Static.ToInt(bytes.Length));
                                    fs.Flush();
                                    fs.Close();
                                    fs.Dispose();
                                    cas = cas + SSTCryptographer.Encrypt(Static.ToStr(array[0]), "SampleKey") + " " + array[2] + " " + Static.ToStr(array[3]) + " ";
                                    Program.updatefiles = cas.TrimEnd();
                                    Program.selfupdate = 1;
                                    selfupdate = true;
                                }
                            }
                        }
                        else
                        {
                            if (res.ResultNo == 9110135)
                            {
                                Program.updatefiles = res.ResultDesc;
                                string[] name = res.ResultDesc.Split(' ');
                                Hashtable hash = (Hashtable)res.Param[0];
                                if (hash.Count != 0)
                                {
                                    if (!Directory.Exists(@".\temp"))
                                    {
                                        Directory.CreateDirectory(@".\temp");
                                    }
                                    for (int j = 0; j < hash.Count; j++)
                                    {
                                        ArrayList array = (ArrayList)hash[j];
                                        byte[] bytes = (byte[])array[1];
                                        fs = File.Create(Terminaldir + "\\temp\\" + Static.ToStr(array[0]));
                                        fs.Write(bytes, 0, Static.ToInt(bytes.Length));
                                        fs.Flush();
                                        fs.Close();
                                        fs.Dispose();
                                        cas = cas + SSTCryptographer.Encrypt(Static.ToStr(array[0]), "SampleKey") + " " + array[2] + " " + Static.ToStr(array[3]) + " ";
                                    }
                                }
                                Program.selfupdate = 1;
                                Program.updatefiles = cas;
                                MessageBox.Show(this, "Шинэчлэгч өөрийгөө шинэчлэсэн тул хуулах үйлдэл дууссаны дараа дахин шинэчлэх үйлдлээ хийнэ үү.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(res.ResultNo + "  " + res.ResultDesc);
                            }
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Серверт холбогдож чадсангүй");
                    }
                }
                else
                {
                    MessageBox.Show("Тохиргоо буруу байна.");
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
        private void btnSettings_Click(object sender, EventArgs e)
        {
            InfoPos.TerminalUpdater.frmSettings frm = new InfoPos.TerminalUpdater.frmSettings();
            frm.ShowDialog();
        }
        private void frmUpdateMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                moConnection.Disconnect();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmUpdateMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Terminaldir + "\\Readme.txt"))
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(Terminaldir + "\\Readme.txt");
                    mmoreadme.EditValue = file.ReadToEnd();
                }
                else
                {
                    mmoreadme.EditValue = "Readme.txt Файл олдсонгүй.";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }
    }
}