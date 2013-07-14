using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;
using InfoPos.Admin.DashBoard;
using DevExpress.XtraBars.Alerter;

namespace InfoPos.Admin
{
    public partial class frmMain : Form

    {
        #region [ Variables ]
        static private InfoPos.Core.Core _core = new InfoPos.Core.Core();
        private static System.Windows.Forms.Timer _timer;
        float fontsize = 8;
        private static System.Windows.Forms.Timer timer;
        private static int _tick;
        internal static bool _directClose;
        private string _apppath;
        DataTable dt;
        Hashtable hashname = new Hashtable();
        int second = 0;
        string TxnCode;
        DataRow dr;
        private String[,] MenuList = new String[100, 10];
        string fontname = "Tahoma";

        XtraTabbedMdiManager xtra = new XtraTabbedMdiManager();
        string[][] mTxnArray = new string[999999][];
        public DataTable DTMenu = null;
        #endregion
        #region [ INIT ]
        public frmMain(InfoPos.Core.Core mcore)
        {
            InitializeComponent();
            try
            {
                _core = mcore;
                _apppath = _core.ApplicationPath;
                InitAll();
                if (_core.Resource != null)
                {
                    barButtonItem184.Glyph = _core.Resource.GetImage("image_update");
                    barButtonItem170.Glyph = _core.Resource.GetImage("image_vertical");
                    barButtonItem171.Glyph = _core.Resource.GetImage("image_horizontal");
                    barButtonItem172.Glyph = _core.Resource.GetImage("image_cascade");
                    barButtonItem177.Glyph = _core.Resource.GetImage("image_windowadd");
                    barButtonItem178.Glyph = _core.Resource.GetImage("image_exit");
                    barButtonItem181.Glyph = _core.Resource.GetImage("image_settings");
                    barButtonItem182.Glyph = _core.Resource.GetImage("image_changepass");
                    barButtonItem183.Glyph = _core.Resource.GetImage("image_pupdate");
                    barButtonItem186.Glyph = _core.Resource.GetImage("image_shortcut");
                    barButtonItem185.Glyph = _core.Resource.GetImage("image_about");
                    imageCollection1.Images.Add(_core.Resource.GetImage("dashboard_unread"));
                    imageCollection1.Images.Add(_core.Resource.GetImage("image_mailalert"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void _core_MsgRecv(string Msg)
        {
           MessageBox.Show(Msg, "Систем мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void _core_TxnDateChanged(DateTime TxnDate)
        {
            SetStatusBar();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_directClose)
            {
                if (_core.RemoteObject.IsConnected && MessageBox.Show("Програмаас гарахдаа итгэлтэй байна уу?",_core.ApplicationTitle, MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    MainForm.ClearMemory(); 
                }
            }
        }
        private void InitAll()
        {
            #region [ Building events ... ]
            MainForm._splash.SetStatus("Building events ...");
            System.Windows.Forms.Application.DoEvents();

            InitEvents();
            #endregion
            #region [ Building lock timeout ... ]
            MainForm._splash.SetStatus("Building timer ...");
            System.Windows.Forms.Application.DoEvents();

            _directClose = true;

            int locktimeout = 1;
            try
            {
                locktimeout = _core.CacheGetInt("frmOption_LockTimeOut", 99);
            }
            catch { }

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;
            _timer.Enabled = true;
            #endregion
            #region [ Build param ]
            MainForm._splash.SetStatus("Building parameter...");
            System.Windows.Forms.Application.DoEvents();

            //_core.RemoteObject.InitALL();
            _core.InitAll();
            #endregion
            #region [ Set status ]
            MainForm._splash.SetStatus("Building status ...");
            System.Windows.Forms.Application.DoEvents();

            //Stream stream = _core.Resource.GetStream("hpro_icon");
            
            //_core.icon = new System.Drawing.Icon(stream);

            this.Text = "InfoPos";
            //this.Icon = _core.icon;

            #endregion
            #region [ Set Menu Settings ]
            MainForm._splash.SetStatus("Building menu ...");
            System.Windows.Forms.Application.DoEvents();

            #endregion
            #region [ Set Outlook menu Settings ]
            SetLeftMenu();
            #endregion
            #region [ Set StatusBar ]
            SetStatusBar();
            #endregion
            #region [ Building dictionary ... ]
            MainForm._splash.SetStatus("Building dictionary ...");
            System.Windows.Forms.Application.DoEvents();

            try
            {
                string filename = "ism.dicdata.dll";
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    _core.Dictionary = (DataSet)bf.Deserialize(fs);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                this.Close();
            }
            #endregion
            _core.MainForm = this;
            _directClose = false;
            //frmDashboard frm = new frmDashboard(_core);
            //frm.MdiParent = _core.MainForm;
            //frm.Dock = DockStyle.Fill;
            //frm.Show();
            _core.TempPath = _core.CacheGetStr("frmOption_TempPath", "");
            //_core.TxnDateChanged += new InfoPos.Core.Core.dlgServerDateChanged(_core_TxnDateChanged);
            //_core.MsgRecv += new InfoPos.Core.Core.dlgMsgRecv(_core_MsgRecv);
            #region[Terminal Message]
            timer = new System.Windows.Forms.Timer();
            timer.Tick+=timer_Tick;
            //_core.HeavenProMessages += new InfoPos.Core.Core.dlgHeavenProMessages(_core_HeavenProMessages);
            timer.Interval = 1000;
            timer.Enabled = true;
            #endregion
        }

        #region [ Timer ]
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                InfoPos.Admin.MainForm._splash.Close();
                InfoPos.Admin.MainForm._splash.Dispose();
                //if (_core.MessageFileCheck == false)
                //{
                //    MessageBox.Show("Message файл олдсонгүй .");
                //    this.Close();
                //}
                //else
                {
                    this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
                }
                string strstyle = "Caramel";

                switch (Static.ToInt(_core.TerminalSkin))
                {
                    case 0: strstyle = "Caramel"; break;
                    case 1: strstyle = "Black"; break;
                    case 2: strstyle = "Blue"; break;
                    case 3: strstyle = "DevExpress Style"; break;
                    case 4: strstyle = "Money Twins"; break;
                    case 5: strstyle = "Lilian"; break;
                    case 6: strstyle = "DevExpress Dark Style"; break;
                }
            defaultLookAndFeel1.LookAndFeel.SetSkinStyle(strstyle);
            if (_core.WindowType == "1")
            {
                xtra.MdiParent = this;
            }
            else
            {
                xtra.MdiParent = null;
            }
                #region[Shortcut]
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140321, 140321, null);
                if (r.ResultNo == 0)
                {
                    dt = r.Data.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["TYPE"]) == 1)
                        {
                            BarButtonItem shortcut = new BarButtonItem();
                            shortcut.Caption = dr["IDValue"].ToString();
                            shortcut.ItemShortcut = new BarShortcut((Keys)Static.ToInt(dr["KEYS"]) | (Keys)Static.ToInt(dr["KEYS1"]) | (Keys)Static.ToInt(dr["KEYS2"]));
                            barSubItem44.AddItem(shortcut);
                            shortcut.ItemClick += new ItemClickEventHandler(shortcut_ItemClick);

                        }
                        else
                        {
                            BarButtonItem pastetext = new BarButtonItem();
                            pastetext.Caption = dr["IDValue"].ToString();
                            pastetext.ItemShortcut = new BarShortcut((Keys)Static.ToInt(dr["KEYS"]) | (Keys)Static.ToInt(dr["KEYS1"]) | (Keys)Static.ToInt(dr["KEYS2"]));
                            barSubItem45.AddItem(pastetext);
                            pastetext.ItemClick += new ItemClickEventHandler(text_ItemClick);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void _timer_Tick(object sender, EventArgs e)
        {
            int timeLimit = _core.CacheGetInt("frmOption_LockTimeOut", 99);
            _tick++;
            if (_tick >= timeLimit * 60)
            {
                _timer.Enabled = false;
                ResetTimer();
                ISM.CUser.frmLock lockWindow = new ISM.CUser.frmLock(_core.RemoteObject, _core.ApplicationTitle);
                lockWindow.Owner = this;
                if (lockWindow.ShowDialog() != DialogResult.OK)
                {
                    _directClose = true;
                    System.Windows.Forms.Application.Exit();
                }
                _timer.Enabled = true;
            }
        }
        #region[Shortcut]
        void shortcut_ItemClick(object sender, ItemClickEventArgs e)
        {

            try
            {
                string s = GetTxn(Static.ToInt(e.Item.Caption));
                string[] words = s.Split(';');
                if (s != ";;")
                {
                    string[] word = s.Split(';');
                    Execute(word[0], word[1], word[2], new object[] { _core });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void editcopy(ItemClickEventArgs e)
        {
            Clipboard.SetText(e.Item.Caption);
        }
        void text_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ActiveMdiChild.ActiveControl != null)
                {
                    if (this.ActiveMdiChild.ActiveControl.GetType().ToString() == "DevExpress.XtraEditors.TextBoxMaskBox")
                    {
                        TextBoxMaskBox edit = (TextBoxMaskBox)ActiveForm.ActiveMdiChild.ActiveControl;
                        editcopy(e);
                        edit.EditValue = "";
                        edit.Paste();
                        Clipboard.Clear();
                    }
                }
            }
        }
        #endregion
        public static void ResetTimer()
        {
            _tick = 0;
        }
        #endregion
        #region [ SetLeftMenu ]
        private void SetLeftMenu()
        {
            try
            {
                navBarControl1.Visible = false;
                #region [ TXNTABLE ]
                DTMenu = new DataTable("TXNTABLE");
                DataColumn column;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int64");
                column.ColumnName = "TXNCODE";
                column.Unique = false;
                DTMenu.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "TXNNAME";
                column.Unique = false;
                DTMenu.Columns.Add(column);
                #endregion
                System.Drawing.Font font = new System.Drawing.Font(fontname, fontsize, FontStyle.Bold);
                string Caption = " ";
                string controlname = " ";
                string picname = "";
                int MenuType;
                string StrDll, StrClass, StrFunction,StrPrivNo;
                long trancode;

                int index = 2, Top = -1;
                object rowIndex = 2;
                int savetop = -1;
                int itemcount = 0;
                int subitemcount = 0;
                bool isSaveSubGroup = false;
                bool isSaveTreeSubGroup = false;
                NavBarGroup[] LeftMenu = new NavBarGroup[20];
                NavBarItem SaveSubGroupItems = new NavBarItem();
                DevExpress.XtraNavBar.NavBarGroupControlContainer[] navBarGroupControlContainer3 = new DevExpress.XtraNavBar.NavBarGroupControlContainer[20];

                DevExpress.Utils.Design.DXTreeView[] treeView1 = new DevExpress.Utils.Design.DXTreeView[20];
                DevExpress.Utils.Design.DXTreeView savetreeview = null;

                TreeNode[] RootNode = new TreeNode[20];
                TreeNode SaveRootNode = null;

                if (_core.FontName.Trim() != "")
                    fontname = _core.FontName;

                if (_core.FontSize != 0)
                    fontsize = (float)_core.FontSize;
                string line;
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream str = asm.GetManifestResourceStream("InfoPos.Admin.Resources.LeftMenu.txt");
                StreamReader file= new StreamReader(str);
                navBarControl1.Items.Clear();
                navBarControl1.Groups.Clear();

                int fview = 0, savefview = 0;

                #region [ while ]
                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(';');

                    MenuType = Static.ToInt(words[2]);
                    controlname = words[5];

                    if (words[9] != "0")
                        Caption = words[9];

                    //Group
                    if (MenuType == 10 || MenuType == 0)
                    {
                        picname = words[3];
                        Top++;
                        if (MenuType == 10)
                        {
                            #region [ TreeView ]
                            treeView1[Top] = new DevExpress.Utils.Design.DXTreeView();
                            treeView1[Top].BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                            //treeView1[Top].Dock = System.Windows.Forms.DockStyle.Fill;
                            treeView1[Top].Size = new System.Drawing.Size(400, 322);
                            treeView1[Top].ImageIndex = 0;
                            treeView1[Top].ImageList = il;
                            treeView1[Top].Location = new System.Drawing.Point(0, 0);
                            treeView1[Top].Name = "treeView1" + Static.ToStr(Top);
                            treeView1[Top].BackColor = navBarControl1.BackColor;

                            treeView1[Top].NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeView_NodeMouseClick);
                            treeView1[Top].KeyDown += new KeyEventHandler(frmMain_KeyDown);
                            //treeView1[Top].

                            navBarGroupControlContainer3[Top] = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
                            navBarControl1.Controls.Add(navBarGroupControlContainer3[Top]);
                            navBarGroupControlContainer3[Top].Controls.Add(treeView1[Top]);
                            navBarGroupControlContainer3[Top].Name = "navBarGroupControlContainer3" + Static.ToStr(Top);
                            navBarGroupControlContainer3[Top].Size = new System.Drawing.Size(navBarControl1.Width, 322);
                            navBarGroupControlContainer3[Top].TabIndex = 2;

                            LeftMenu[Top] = navBarControl1.Groups.Add();
                            LeftMenu[Top].Caption = Caption;
                            if (picname != "")
                            {
                                LeftMenu[Top].SmallImage = _core.Resource.GetImage(picname);
                                il.Images.Add(picname, _core.Resource.GetImage(picname));
                            }
                            LeftMenu[Top].Name = controlname + Static.ToStr(Top);
                            LeftMenu[Top].GroupStyle = NavBarGroupStyle.ControlContainer;
                            LeftMenu[Top].ControlContainer = navBarGroupControlContainer3[Top];
                            LeftMenu[Top].DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
                            LeftMenu[Top].Expanded = true;
                            LeftMenu[Top].GroupClientHeight = 322;

                            treeView1[Top].Dock = DockStyle.Fill;

                            savetreeview = treeView1[Top];

                            if (savetop != -1 && itemcount == 0)
                                LeftMenu[savetop].Visible = false;

                            if (isSaveSubGroup == true && subitemcount == 0)
                                SaveSubGroupItems.Visible = false;
                            #endregion
                        }
                        else
                            if (MenuType == 0)
                            {
                                #region [ navBarControl ]
                                LeftMenu[Top] = navBarControl1.Groups.Add();
                                LeftMenu[Top].Caption = Caption;
                                if (picname != "")
                                    LeftMenu[Top].SmallImage = _core.Resource.GetImage(picname);
                                LeftMenu[Top].Name = controlname;

                                if (savetop != -1 && itemcount == 0)
                                    LeftMenu[savetop].Visible = false;

                                if (isSaveSubGroup == true && subitemcount == 0)
                                    SaveSubGroupItems.Visible = false;

                                if (isSaveTreeSubGroup == true && subitemcount == 0)
                                    savetreeview.Nodes.Remove(SaveRootNode);

                                if (fview == 0)
                                    savefview = Top;
                                fview++;
                                #endregion
                            }
                        savetop = Top;
                        itemcount = 0;
                        subitemcount = 0;
                        isSaveSubGroup = false;
                        isSaveTreeSubGroup = false;
                    }
                    else
                    {
                        //SubGroup
                        if (MenuType == 1 || MenuType == 11)
                        {
                            if (MenuType == 11)
                            {
                                #region [ RootNodeSubGroup ]
                                if (isSaveTreeSubGroup == true && subitemcount == 0)
                                {
                                    savetreeview.Nodes.Remove(SaveRootNode);
                                }

                                if (isSaveSubGroup == true && subitemcount == 0)
                                {
                                    SaveSubGroupItems.Visible = false;
                                }

                                TreeNode states1 = savetreeview.Nodes.Add(controlname, Caption, picname);
                                states1.SelectedImageKey = picname;
                                SaveRootNode = states1;
                                isSaveTreeSubGroup = true;
                                isSaveSubGroup = false;
                                #endregion
                            }
                            else
                            {
                                #region [ TreeSubGroup ]
                                if (isSaveTreeSubGroup == true && subitemcount == 0)
                                {
                                    savetreeview.Nodes.Remove(SaveRootNode);
                                }

                                if (isSaveSubGroup == true && subitemcount == 0)
                                {
                                    SaveSubGroupItems.Visible = false;
                                }

                                NavBarItem indexItem = navBarControl1.Items.Add();
                                indexItem.Caption = Caption;
                                indexItem.Appearance.Font = font;
                                indexItem.Name = controlname;
                                LeftMenu[Top].ItemLinks.Add(indexItem);

                                SaveSubGroupItems = indexItem;
                                isSaveTreeSubGroup = false;
                                isSaveSubGroup = true;
                                #endregion
                            }
                            //Parameters
                            subitemcount = 0;
                        }
                        else
                        {
                            //Item
                            if (MenuType == 2 || MenuType == 12)
                            {
                                #region [ Items ]
                                trancode = Static.ToLong(words[5]);
                                if (_core.RemoteObject.GetTxn(trancode) == true)
                                {
                                    picname = words[3];
                                    StrPrivNo = words[5];
                                    StrDll = words[6];
                                    StrClass = words[7];
                                    StrFunction = words[8];

                                    StringBuilder sb = new StringBuilder();
                                    sb.Clear();
                                    sb.Append(controlname);
                                    sb.Append(";");
                                    sb.Append(StrDll);
                                    sb.Append(";");
                                    sb.Append(StrClass);
                                    sb.Append(";");
                                    sb.Append(StrFunction);
                                    sb.Append(";");
                                    sb.Append(StrPrivNo);

                                    mTxnArray[trancode] = new string[] { Static.ToStr(trancode), StrDll, StrClass, StrFunction };

                                    Caption = Caption + " [" + Static.ToStr(trancode) + "]";
                                    hashname.Add(trancode, Caption);
                                    if (MenuType == 12)
                                    {
                                        //TreeView
                                        il.Images.Add(picname, _core.Resource.GetImage(picname));
                                        TreeNode states1 = SaveRootNode.Nodes.Add(controlname, Caption, picname);
                                        states1.Tag = sb.ToString();
                                        states1.SelectedImageKey = picname;
                                    }
                                    else
                                    {
                                        //NavBarItem
                                        NavBarItem indexItem = navBarControl1.Items.Add();
                                        indexItem.Caption = Caption;
                                        if (picname != "")
                                            indexItem.SmallImage = _core.Resource.GetImage(picname);
                                        indexItem.LinkClicked += new NavBarLinkEventHandler(indexItem_LinkClicked);
                                        indexItem.Name = controlname;

                                        indexItem.Tag = sb.ToString();

                                        LeftMenu[Top].ItemLinks.Add(indexItem);
                                    }

                                    DTMenu.Rows.Add(trancode, Caption);

                                    //Param
                                    itemcount++;
                                    subitemcount++;
                                }
                                #endregion
                            }
                            #region [ Seperator ]
                            else
                            {
                                //Seperator
                                if (MenuType == 3)
                                {
                                    NavBarItem indexItem = navBarControl1.Items.Add();
                                    Caption = " ";
                                    indexItem.Caption = Caption;
                                    indexItem.Name = controlname;
                                    LeftMenu[Top].ItemLinks.Add(indexItem);
                                }
                            }
                            #endregion
                        }
                    }
                    index++;
                    rowIndex = index;
                }
                #endregion
                file.Close();
                navBarControl1.Visible = true;
                LeftMenu[savefview].Expanded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void frmMain_KeyDown(object sender, KeyEventArgs e)
        {

        }
        void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Tag != null && e.Node.Tag != "")
                {
                    string s = Static.ToStr(e.Node.Tag);
                    string[] words = s.Split(';');

                    if (e.Node.Name == words[0])
                    {
                        Execute(words[1], words[2], words[3], Static.ToInt(words[4]), new object[] { _core });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void indexItem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                if (e.Link.Item.Tag != null && e.Link.Item.Tag != "")
                {
                    string s = Static.ToStr(e.Link.Item.Tag);
                    string[] words = s.Split(';');

                    if (e.Link.ItemName == words[0])
                    {
                        Execute(words[1], words[2], words[3],Static.ToInt(words[4]), new object[] { _core });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void SetStatusBar()
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append(_core.InstCode);
            sb.Append("-");
            sb.Append(_core.InstName);

            barStatus.Manager.Items["InstName"].Caption = sb.ToString();
            barStatus.Manager.Items["InstName"].Glyph = _core.Resource.GetImage("hpro_home");

            sb.Clear();
            sb.Append(_core.RemoteObject.User.UserNo);
            sb.Append("-");
            if (_core.RemoteObject.User.UserFName.Length > 1)
            {
                sb.Append(_core.RemoteObject.User.UserFName.Substring(0, 1));
                sb.Append(".");
            }
            sb.Append(_core.RemoteObject.User.UserLName);
            sb.Append("-");
            sb.Append(_core.RemoteObject.User.BranchCode);

            barStatus.Manager.Items["UserBranch"].Caption = sb.ToString();
            barStatus.Manager.Items["UserBranch"].Glyph = _core.Resource.GetImage("hpro_user");
            barStatus.Manager.Items["SystemDate"].Caption = _core.TxnDate.ToShortDateString();
            barStatus.Manager.Items["SystemDate"].Glyph = _core.Resource.GetImage("hpro_systemdate");
            barStatus.Manager.Items["ServerPort"].Caption = _core.RemoteObject.ServerIP + ":" + _core.RemoteObject.ServerPort;
            barStatus.Manager.Items["ServerPort"].Glyph = _core.Resource.GetImage("hpro_appserver");
            barStatus.Manager.Items["SystemName"].Caption = "HeavenPro 1.0.0";
            barStatus.Manager.Items["SystemName"].Glyph = _core.Resource.GetImage("hpro_application");
        }
        public string GetTxn(int txncode)
        {
            string retstr = mTxnArray[txncode][1] + ";" + mTxnArray[txncode][2] + ";" + mTxnArray[txncode][3];
            return retstr;
        }
        #endregion
        #region [ Events ]
        private void InitEvents()
        {
            _core.RemoteObject.DisConnected += new ISM.CUser.Remote.DelegateDisConnected(moRemote_DisConnected);
        }
        void moRemote_DisConnected()
        {
            try
            {
                if (_directClose) return;
                MessageBox.Show(this, "Серверээс холболт тасарлаа!", _core.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Windows.Forms.Application.Exit();
            }
            catch
            {
            }
        }
        #endregion
        internal void Execute(string dll, string cls, string func, object[] p)
        {
            try
            {
                Assembly asm = Assembly.LoadFrom(Path.Combine(_apppath, dll));
                Type asmType = asm.GetType(dll.Substring(0, dll.LastIndexOf(".")) + "." + cls, true, true);

                object obj = Activator.CreateInstance(asmType);
                MethodInfo mi = obj.GetType().GetMethod(func);
                if (mi.IsStatic)
                {
                    asmType.InvokeMember(func, BindingFlags.InvokeMethod, null, asmType, new object[] { p });
                }
                else
                {
                    mi.Invoke(obj, new object[] { p });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.Source + ex.StackTrace);
            }
        }
        internal void Execute(string dll, string cls, string func,int privno, object[] p)
        {
            try
            {
                Assembly asm = Assembly.LoadFrom(Path.Combine(_apppath, dll));
                Type asmType = asm.GetType(dll.Substring(0, dll.LastIndexOf(".")) + "." + cls, true, true);

                object obj = Activator.CreateInstance(asmType);
                MethodInfo mi = obj.GetType().GetMethod(func);
                if (mi.IsStatic)
                {
                    asmType.InvokeMember(func, BindingFlags.InvokeMethod, null, asmType, new object[] { p });
                }
                else
                {
                    mi.Invoke(obj, new object[] { p });
                }
                if (ActiveForm != null && ActiveForm.ActiveMdiChild != null)
                    ActiveForm.ActiveMdiChild.Text = ActiveForm.ActiveMdiChild.Text + "-" + privno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.Source + ex.StackTrace);
            }
        }
        #endregion
        #region [ ItemClick's ]

       #region[Child цонхнуудын харагдац]
        private void barButtonItem170_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }
        private void barButtonItem171_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }
        private void barButtonItem172_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }
       #endregion

        private void barButtonItem177_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmCallTxn frm = new frmCallTxn(_core, DTMenu);
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                string ret = GetTxn(Static.ToInt(frm.cboTxns.EditValue));
                if (ret != ";;")
                {
                    string[] words = ret.Split(';');
                    Execute(words[0], words[1], words[2], Static.ToInt(frm.cboTxns.EditValue), new object[] { _core });
                }
                else
                    MessageBox.Show("Цонх дуудахад алдаа гараа [" + Static.ToStr(frm.cboTxns.EditValue) + "]");
            }
        }
        private void barButtonItem178_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion
        #region[ Цонх ]
        private void barButtonItem181_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmOption frm = new frmOption(_core);
            frm.ShowDialog();
            if (_core.WindowType == "1")
            {
                xtra.MdiParent = this;
            }
            else
            {
                xtra.MdiParent = null;
            }
        }
        private void barButtonItem182_ItemClick(object sender, ItemClickEventArgs e)
        {
            ISM.CUser.frmChangePass frm = new ISM.CUser.frmChangePass(_core.RemoteObject.Connection, _core.RemoteObject.User);
            frm.ShowDialog();
        }
        private void barButtonItem185_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmAbout frm = new frmAbout(_core);
            frm.ShowDialog();
        }
        private void barButtonItem184_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (File.Exists(@".\InfoPos.TerminalUpdater.exe"))
            {
                Process.Start(@".\InfoPos.TerminalUpdater.exe");
                this.Close();
            }
            else
            {
                MessageBox.Show("Терминал шинэчлэгч олдсонгүй.");
            }
        }
        private void barButtonItem183_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmParameterUpdate frm = new frmParameterUpdate(_core);
            frm.ShowDialog();
        }
        #endregion
        private void barButtonItem186_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmShortcutList frm = new frmShortcutList(_core);
            frm.ShowDialog();
        }
        #region[ Message ]
        private void SystemMsg_ItemClick(object sender, ItemClickEventArgs e)
        {
            //InfoPos.Messages.frmMail frm = new InfoPos.Messages.frmMail(_core);
            //frm.MdiParent = _core.MainForm;
            //frm.Show();
            //SystemMsg.Visibility = BarItemVisibility.Never;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            second++;
            if (second % 2 == 0)
            {
                barStatus.Manager.Items["SystemMsg"].Glyph = _core.Resource.GetImage("dashboard_unread");
                second = 0;
            }
            else
            {
                barStatus.Manager.Items["SystemMsg"].Glyph = _core.Resource.GetImage("dashboard_unreadred");
            }
        }
        void _core_HeavenProMessages(object[] Param)
        {
            barStatus.Manager.Items["SystemMsg"].Glyph = _core.Resource.GetImage("dashboard_unread");
            string message = Static.ToStr(Param[0]);
            if (message.Length > 20)
            {
                message = message.Substring(0, 19);
                barStatus.Manager.Items["SystemMsg"].Caption = message + " ...";
            }
            else
            {
                barStatus.Manager.Items["SystemMsg"].Caption = message;
            }
            SystemMsg.Visibility = BarItemVisibility.Always;

            timer.Enabled = true;
        }
        #endregion
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            //HeavenPro.Parameter.frmDynReport frm = new HeavenPro.Parameter.frmDynReport(_core);
            //frm.MdiParent = _core.MainForm;
            //frm.Show();
        }
    }
}
