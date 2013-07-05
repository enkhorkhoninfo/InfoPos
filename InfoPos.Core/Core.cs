using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using EServ.Shared;
using NativeExcel;

using ISM.Lib;
using lib = ISM.Lib.Static;

namespace InfoPos.Core
{
    public class Core
    {
        #region [ Delegate ]

        public delegate void dlgServerDateChanged(DateTime TxnDate);
        public event dlgServerDateChanged EventDateChanged;

        public delegate void dlgDataReceived(object[] param);
        public event dlgDataReceived EventDataReceived;

        public delegate void dlgProgressUpdate(string Func, int ProcessNo, int Status, string ErrMessage, DateTime StartDate, DateTime EndDate);
        public event dlgProgressUpdate EventProgressUpdate;

        public delegate void dlgEODUpdate(string Func, int ProcessNo, int Status, string ErrMessage, DateTime StartDate, DateTime EndDate);
        public event dlgEODUpdate EODUpdate;

        public delegate void dlgGLProcessUpdate(string Func, int ProcessNo, int Status, string ErrMessage, DateTime StartDate, DateTime EndDate);
        public event dlgGLProcessUpdate GLProcessUpdate;

        #endregion

        #region [ Local variables ]

        /*************************************************************
         * Хөнгөлөлтийн тодорхойлол бүхий Обектуудыг агуулна.
         * JSON файл хэлбэрээр сервер дээр хадгалагдах бөгөөд
         * JsonParser сангаар обект руу хөрвүүлж хадгалахад хэрэглэнэ.
         * 2013.02.17 Amaraa
         *************************************************************/
        public SortedList _Discounts = new SortedList();

        /*************************************************************
         * Өдрийн төрлийн мэдээллийг агуулсан 1 мөр бичлэг л байна.
         * DayType,DayTemperature,DayDesc,DayIcon, NightTemp...
         * 2013.02.17 Amaraa
         *************************************************************/
        public DataTable _dtDayInfo = null;

        /*************************************************************
         * Тухайн AreaCode дээр хамаарах бүтээгдэхүүний жагсаалт.
         * Үүнийг борлуулалт хийхэд ашиглана.
         * 2013.02.17 Amaraa
         *************************************************************/
        public DataTable _dtProdInfo = null;

        private DataTable _GeneralParam;
        private DataSet _Dictionary; 
        private DataTable _Priv;
        private string mApplicationPath;
        
        private DateTime mTxnDate;

        private string mInstCode;
        private string mInstName;
        private string mInstName2;
        private string mRegPath;
        private string mPolicyMask;
        public bool MessageFileCheck=false;
        
        private string mTempPath;
        private string mReportPathIn;
        private string mReportPathOut;
        private string mSlipPathIn;
        private string mSlipPathOut;
        private string mDynamicPathIn;
        private string mDynamicPathOut;
        private string mCustReportPathIn;
        private string mInstStateCode;
        private string mTerminalSkin = "0";
        private string mWindowType = "0";

        private string mSvrIP = "";
        private int mSvrPort = 0;

        private string mFontName = "Arial";
        private double mFontSize = 8;

        private static string _PathTerminal;
        private static string _PathDynamicRpt;
        private static string _PathDynamicDoc;
        private static string _PathSlips;
        private static string _UpdateExtention;

        #region Хэрэгггүй болсон хувьсагчууд, хасах хэрэгтэй!

        //public Icon icon;
        //public Hashtable Lang = new Hashtable();

        //private TagReader _tagreader = new TagReader();
        //public TagReader TagReader
        //{
        //    get { return _tagreader; }
        //}

        //private string _CashPayment = "1";
        //private string _CardPayment = "2";
        //private string _OfflineCardPayment = "3";

        #endregion

        #endregion

        #region [ Properties ]

        #region Connectivity Objects

        private InfoPos.Resource resource = null;
        public InfoPos.Resource Resource
        {
            get { return resource; }
            set { resource = value; }
        }

        private ISM.CUser.Remote moRemote = null;
        public ISM.CUser.Remote RemoteObject
        {
            get
            {
                return moRemote;
            }
            set
            {
                moRemote = value;
            }
        }

        private System.Windows.Forms.Form mfrmMainForm = null;
        public Form MainForm
        {
            get
            {
                return mfrmMainForm;
            }
            set
            {
                mfrmMainForm = value;
            }
        }

        #endregion
        #region Devices

        private Printer _printer = new Printer();
        public Printer Printer
        {
            get { return _printer; }
        }

        private Printer _liftprinter = new Printer();
        public Printer LiftPrinter
        {
            get { return _liftprinter; }
        }

        private PriceBoard _priceboard = new PriceBoard();
        public PriceBoard PriceBoard
        {
            get { return _priceboard; }
        }

        private Tag _tag = new Tag();
        public Tag Tag
        {
            get { return _tag; }
        }
        
        #endregion
        #region XML Caches
        private string _xmlcachename = "";
        public string xmlCacheName
        {
            get { return _xmlcachename; }
        }

        private object _xmlcache = null;
        public object xmlCache
        {
            get { return _xmlcache; }
        }
        #endregion
        #region General Parameters

        public DataTable GeneralParam
        {
            get
            {
                return _GeneralParam;
            }
            set 
            {
                _GeneralParam =value;
            }
        }
        public DataSet Dictionary
        {
            get{return _Dictionary;}
            set{_Dictionary = value;}
        }
        public DataTable Priv
        {
            get{return _Priv;}
            set{_Priv = value;}
        }

        public string SvrIP
        {
            get { return mSvrIP; }
            set { mSvrIP = value; }
        }
        public int SvrPort
        {
            get { return mSvrPort; }
            set { mSvrPort = value; }
        }

        public DateTime TxnDate
        {
            get{return mTxnDate;}
        }

        public string ApplicationName
        {
            get{
                return moRemote.ApplicationName;
            }
        }
        public string ApplicationTitle
        {
            get
            {
                return moRemote.ApplicationTitle;
            }
            set
            {
                moRemote.ApplicationTitle = value;
            }
        }
        public string ApplicationPath
        {
            get
            {
                return mApplicationPath;
            }
        }
        public string TempPath
        {
            get
            {
                return mTempPath;
            }
            set
            {
                mTempPath = value;
            }
        }
        public string ReportPathIn
        {
            get
            {
                return mReportPathIn;
            }
            set
            {
                mReportPathIn = value;
            }
        }
        public string ReportPathOut
        {
            get
            {
                return mReportPathOut;
            }
            set
            {
                mReportPathOut = value;
            }
        }
        public string SlipsPathIn
        {
            get
            {
                return mSlipPathIn;
            }
            set
            {
                mSlipPathIn = value;
            }
        }
        public string SlipsPathOut
        {
            get
            {
                return mSlipPathOut;
            }
            set
            {
                mSlipPathOut = value;
            }
        }
        public string DynamicPathIn
        {
            get
            {
                return mDynamicPathIn;
            }
            set
            {
                mDynamicPathIn = value;
            }
        }
        public string DynamicPathOut
        {
            get
            {
                return mDynamicPathOut;
            }
            set
            {
                mDynamicPathOut = value;
            }
        }
        public string CustReportPathIn
        {
            get
            {
                return mCustReportPathIn;
            }
            set
            {
                mCustReportPathIn = value;
            }
        }
        public string InstCode
        {
            get
            {
                return mInstCode;
            }
        }
        public string InstName
        {
            get
            {
                return mInstName;
            }
            set
            {
                mInstName = value;
            }

        }
        public string InstName2
        {
            get
            {
                return mInstName2;
            }
        }
        public string PolicyMask
        {
            get
            {
                return mPolicyMask;
            }
        }
        public string TerminalSkin
        {
            get
            {
                return mTerminalSkin;
            }
            set
            {
                mTerminalSkin = value;
            }
        }
        public string WindowType
        {
            get
            {
                return mWindowType;
            }
            set
            {
                mWindowType = value;
            }
        }
        public string InstStateCode
        {
            get
            {
                return mInstStateCode;
            }
            set
            {
                mInstStateCode = value;
            }
        }
        public string FontName
        {
            get
            {
                return mFontName;
            }
            set
            {
                mFontName = value;
            }
        }
        public double FontSize
        {
            get
            {
                return mFontSize;
            }
            set
            {
                mFontSize = value;
            }
        }

        public string PathTerminal
        {
            get { return _PathTerminal; }
            set { _PathTerminal = value; }
        }
        public string PathDynamicRpt
        {
            get { return _PathDynamicRpt; }
            set { _PathDynamicRpt = value; }
        }
        public string PathDynamicDoc
        {
            get { return _PathDynamicDoc; }
            set { _PathDynamicDoc = value; }
        }
        public string PathSlips
        {
            get { return _PathSlips; }
            set { _PathSlips = value; }
        }
        public string UpdateExtention
        {
            get { return _UpdateExtention; }
            set { _UpdateExtention = value; }
        }

        #endregion
        #region POS Related

        bool _IsTouch;
        public bool IsTouch
        {
            get { return _IsTouch; }
            set { _IsTouch = value; }
        }

        private decimal _Vat = 10;
        public decimal Vat
        {
            get { return _Vat; }
            set { _Vat = value; }
        }
                
        public string POSNo
        {
            get { return CacheGetStr("Connection_PosNo"); }
        }

        private int _PosStatus = 0;
        public int PosStatus
        {
            get { return _PosStatus; }
        }

        private int _ShiftNo = 1;
        public int ShiftNo
        {
            get { return _ShiftNo; }
            set { _ShiftNo = value; }
        }

        private int _ShiftStatus = 1;
        public int ShiftStatus
        {
            get { return _ShiftStatus; }
            set { _ShiftStatus = value; }
        }

        private int _ShiftUserNo = 0;
        public int ShiftUserNo
        {
            get { return _ShiftUserNo; }
            set { _ShiftUserNo = value; }
        }

        private string _AreaCode = "";
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }

        private string _DayType = null;
        public string DayType
        {
            /********************************************************
             * Борлуулалт дээр бүтээгдэхүүний үнэ авахад хэрэглэнэ.
             * 2013.02.17 Amaraa
             ********************************************************/
            get { return _DayType; }
        }
        

        private int _TagWrite = 0;
        public int TagWrite
        {
            get { return _TagWrite; }
            set { _TagWrite = value; }
        }

        public int BillPrinterType
        {
            get { return CacheGetInt("BillPrinterType", 0); }
        }
        public string BillPrinterPort
        {
            get { return CacheGetStr("BillPrinterPort", "COM1"); }
        }
        public string PriceBoardPort
        {
            get { return CacheGetStr("PriceBoardPort", "COM2"); }
        }

        #endregion

        #endregion

        #region [ Constructors ]
        public Core()
        {
            try
            {
                resource = new InfoPos.Resource();
                moRemote = new ISM.CUser.Remote();

                _xmlcachename = string.Format(@"{0}\Data\Settings.xml", lib.WorkingFolder);
                _xmlcache = ISM.Lib.Cache.XMLCacheOpen(_xmlcachename);

                mFontName = "Arial";
                mFontSize = 8;
                moRemote.ApplicationName = "InfoPos";
                moRemote.ApplicationTitle = "InfoPos system";
                mApplicationPath = Application.StartupPath;
                mTerminalSkin = "0";
                mWindowType = "0";

                mTempPath = CacheGetStr("frmOption_TempPath", "");
                mReportPathIn = CacheGetStr("frmOption_ReportPathIn", "");
                mReportPathOut = CacheGetStr("frmOption_ReportPathOut", "");
                mDynamicPathIn = CacheGetStr("frmOption_DynamicPathIn", "");
                mDynamicPathOut = CacheGetStr("frmOption_DynamicPathOut", "");
                mSlipPathIn = CacheGetStr("frmOption_SlipsPathIn", "");
                mSlipPathOut = CacheGetStr("frmOption_SlipsPathOut", "");
                mCustReportPathIn = CacheGetStr("frmOption_CustReportPathIn", "");

                mTerminalSkin = CacheGetStr("frmOption_TerminalSkin", "0");
                mWindowType = CacheGetStr("frmOption_WindowType", "0");

                moRemote.Received += new ISM.CUser.Remote.DelegateReceived(moRemote_Received);
            }
            catch{}
        }
        void moRemote_Received(EServ.Shared.Result r)
        {
            try
            {
                switch (r.ResultNo)
                {
                    case 1:     //Огноо солих
                        if (EventDateChanged != null)
                        {
                            DateTime date = lib.ToDate(r.Param[0]);
                            mTxnDate = date;
                            EventDateChanged(date);
                        }
                        break;
                    case 4:     //Gl process update
                        if (GLProcessUpdate != null)
                        {
                            GLProcessUpdate(lib.ToStr(r.Param[0]), lib.ToInt(r.Param[1]), lib.ToInt(r.Param[2]), lib.ToStr(r.Param[3]), lib.ToDateTime(r.Param[4]), lib.ToDateTime(r.Param[5]));
                        }
                        break;
                    case 6:     //Progress changes
                        if (EventProgressUpdate != null)
                        {
                            EventProgressUpdate(lib.ToStr(r.Param[0]), lib.ToInt(r.Param[1]), lib.ToInt(r.Param[2]), lib.ToStr(r.Param[3]), lib.ToDateTime(r.Param[4]), lib.ToDateTime(r.Param[5]));
                        }
                        break;
                    case 7:     //InfoPosEod
                        if (EODUpdate != null)
                        {
                            EODUpdate(EServ.Shared.Static.ToStr(r.Param[0]), EServ.Shared.Static.ToInt(r.Param[1]), EServ.Shared.Static.ToInt(r.Param[2]), EServ.Shared.Static.ToStr(r.Param[3]), EServ.Shared.Static.ToDateTime(r.Param[4]), EServ.Shared.Static.ToDateTime(r.Param[5]));
                        }
                        break;
                    default:
                        if (EventDataReceived != null)
                        {
                            EventDataReceived(r.Param);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        ~Core()
        {
        }
        #endregion

        #region User Functions

        public Result InitAll()
        {
            Result res = null;

            #region Init printer, price board

            _printer.Init(
            CacheGetStr("BillPrinterPort", "COM1")
            , CacheGetInt("BillPrinterRate", 9600)
            , 8
            , System.IO.Ports.StopBits.None
            , System.IO.Ports.Parity.None);

            _liftprinter.Init(
            CacheGetStr("LiftPrinterPort", "COM1")
            , CacheGetInt("LiftPrinterRate", 9600)
            , 8
            , System.IO.Ports.StopBits.None
            , System.IO.Ports.Parity.None);

            _priceboard.Init (
            CacheGetStr("PriceBoardPort", "COM2")
            , CacheGetInt("PriceBoardRate", 9600)
            , 8
            , System.IO.Ports.StopBits.None
            , System.IO.Ports.Parity.None);

            #endregion

            res = moRemote.Connection.Call(moRemote.User.UserNo, 200, 100000, 100000, null);
            if (res.ResultNo == 0)
            {
                _GeneralParam = res.Data.Tables[0];
                InitLang();
                InitPos();
                InitDiscount();
                SetGeneralParam();
                this.mTxnDate = lib.ToDate(res.Param[0]);
            }

            return res;
        }
        private void InitLang()
        {
            try
            {
                //MessageFileCheck = true;
                //int Key;
                //string Value = "";
                //Assembly asm = Assembly.GetExecutingAssembly();
                //Stream str = asm.GetManifestResourceStream("InfoPos.Core.Resources.Message.xls");
                //IWorkbook WBook = Factory.OpenWorkbook(str);
                //for (int i = 2; ; i++)
                //{
                //    if (lib.ToStr(WBook.Worksheets[1].Cells[i, 1].Value) != "")
                //    {
                //        if (moRemote.User.UserLanguage == "MN")
                //        {
                //            Key = lib.ToInt(WBook.Worksheets[1].Cells[i, 1].Value);
                //            Value = lib.ToStr(WBook.Worksheets[1].Cells[i, 2].Value);
                //        }
                //        else
                //        {
                //            Key = lib.ToInt(WBook.Worksheets[1].Cells[i, 1].Value);
                //            Value = lib.ToStr(WBook.Worksheets[1].Cells[i, 3].Value);
                //        }
                //        Lang.Add(Key, Value);
                //    }
                //    else
                //        break;
                //}
            }
            catch
            {
                MessageFileCheck = false;
            }

        }
        public void InitPos()
        {
            Result res = null;
            try
            {
                object[] param = new object[] { moRemote.User.PosNo, moRemote.User.AreaCode };
                res = moRemote.Connection.Call(moRemote.User.UserNo, 211, 211001, 100000, param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #region Ээлжтэй холбоотой мэдээлэл
                DataTable dt = res.Data.Tables[0];
                _ShiftNo = lib.ToInt(dt.Rows[0]["SHIFTNO"]);
                _ShiftUserNo = lib.ToInt(dt.Rows[0]["SHIFTUSERNO"]);
                _PosStatus = lib.ToInt(dt.Rows[0]["STATUS"]);
                #endregion
                #region DayType
                _dtDayInfo = res.Data.Tables[1];
                if (_dtDayInfo != null && _dtDayInfo.Rows.Count > 0)
                {
                    _DayType = lib.ToStr(_dtDayInfo.Rows[0]["DAYTYPE"]);
                }
                #endregion
                #region Product info
                _dtProdInfo = res.Data.Tables[2];
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        public void InitDiscount()
        {
            Result res = null;
            try
            {
                object[] param = new object[] { moRemote.User.PosNo, moRemote.User.AreaCode };
                res = moRemote.Connection.Call(moRemote.User.UserNo, 605, 605030, 100000, param);
                if (res.ResultNo == 0 && res.Param!=null)
                {
                    foreach(object i in res.Param)
                    {
                        try
                        {
                            Hashtable jo = (Hashtable)JSON.JsonDecode((string)i);
                            if (jo != null)
                            {
                                int priority = 1000 - JPathGetInt(jo, "priority");
                                
                                while (_Discounts.ContainsKey(priority)) priority--;

                                _Discounts.Add(priority, jo);
                            }
                        }
                        catch (Exception ex)
                        {
                            res = new Result(9, ex.ToString());
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            ISM.Template.FormUtility.ValidateQuery(res);
        }

        public string GetGeneralParam(string key)
        {
            DataRow[] DR = _GeneralParam.Select("Key='" + key + "'");
            if (DR == null) return "";
            if (DR.Length == 0) return "";
            return lib.ToStr(DR[0]["ItemValue"]);
        }
        public void SetGeneralParam()
        {
            foreach (DataRow dr in _GeneralParam.Rows)
            {
                switch (lib.ToStr(dr["Key"]))
                {
                    case "InstCode": mInstCode = lib.ToStr(dr["ItemValue"]); break;
                    case "InstName": mInstName = lib.ToStr(dr["ItemValue"]); break;
                    case "InstName2": mInstName2 = lib.ToStr(dr["ItemValue"]); break;
                    case "PolicyMask": mPolicyMask = lib.ToStr(dr["ItemValue"]); break;
                    case "PathTerminal": PathTerminal = lib.ToStr(dr["ItemValue"]); break;
                    case "PathDynamicRpt": PathDynamicRpt = lib.ToStr(dr["ItemValue"]); break;
                    case "PathDynamicDoc": PathDynamicDoc = lib.ToStr(dr["ItemValue"]); break;
                    case "PathSlips": PathSlips = lib.ToStr(dr["ItemValue"]); break;
                    case "UpdateExtention": PathSlips = lib.ToStr(dr["ItemValue"]); break;
                    case "VAT": Vat = lib.ToDecimal(dr["ItemValue"]); break;
                    
                    //case "CashPayment": CashPayment = lib.ToStr(dr["ItemValue"]); break;
                    //case "CardPayment": CardPayment = lib.ToStr(dr["ItemValue"]); break;
                    //case "OfflineCardPayment": OfflineCardPayment = lib.ToStr(dr["ItemValue"]); break;
                    //case "ShiftOpen": ShiftOpen = lib.ToInt(dr["ItemValue"]); break;

                    case "TagWrite": TagWrite = lib.ToInt(dr["ItemValue"]); break;
                }
            }
        }

        public bool CheckUserLevel(int Level)
        {
            if (Level <= moRemote.User.UserLevel)
                return true;
            else
                return false;
        }
        public delegate void DelegateAlertShow(string Caption, string Text, int image);
        public void AlertShowSafe(string Caption, string Text, int image = 0)
        {
            if (MainForm != null)
            {
                MainForm.BeginInvoke(new DelegateAlertShow(AlertShow), Caption, Text, image);
            }
        }
        public void AlertShow(string Caption, string Text, int image = 0)
        {
            DevExpress.XtraBars.Alerter.AlertControl alert = new DevExpress.XtraBars.Alerter.AlertControl();
            alert.FormLocation = DevExpress.XtraBars.Alerter.AlertFormLocation.TopLeft;
            alert.AutoFormDelay = 3000;
            alert.LookAndFeel.UseDefaultLookAndFeel = false;
            alert.AutoHeight = true;
            alert.AppearanceCaption.Font = new Font(alert.AppearanceCaption.Font.FontFamily, 9, FontStyle.Bold);
            alert.AppearanceText.Font = new Font(alert.AppearanceCaption.Font.FontFamily, 9);
            Image img = null;
            switch (image)
            {
                case 1: img = resource.GetImage("button_ok"); break;
                case 2: img = resource.GetImage("image_exit"); break;
                default: img = resource.GetImage("image_message"); break;
            }

            alert.Show(MainForm, Caption, Text, img);
        }

        #endregion

        #region Main Form Related Functions

        private object InvokeMethod(object instance, string methodName, params object[] param)
        {
            object ret = null;
            MethodInfo mi = instance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (mi != null) ret = mi.Invoke(instance, param);
            return ret;
        }
        public void MainForm_HeaderSet(int rowindex, string caption, string value)
        {
            try
            {
                object[] param = new object[] { rowindex, caption, value };
                InvokeMethod(MainForm, "SetHeaderValue", param);
            }
            catch
            { }
        }
        public string MainForm_HeaderGet(int rowindex)
        {
            string s = null;
            try
            {
                object[] param = new object[] { rowindex };
                object ret = InvokeMethod(MainForm, "SetHeaderValue", param);
                if (ret != null) s = Convert.ToString(ret);
            }
            catch
            { }
            return s;
        }
        public void MainForm_HeaderClear()
        {
            try
            {
                InvokeMethod(MainForm, "ClearHeaderValue", null);
            }
            catch
            { }
        }

        public void MainForm_StatusSet(int index, string value)
        {
        }
        public string MainForm_StatusGet(int index)
        {
            string s = null;

            try
            {
            }
            catch
            { }
            return s;
        }

        #endregion

        #region XML Cache Functions

        public object CacheGet(string key, object defaultvalue=null)
        {
            return Cache.XMLCacheGet(_xmlcache, key, defaultvalue);
        }
        public double CacheGetDbl(string key, double defaultvalue = 0.0D)
        {
            return Cache.XMLCacheGetDbl(_xmlcache, key, defaultvalue);
        }
        public decimal CacheGetDec(string key, decimal defaultvalue = 0)
        {
            return Cache.XMLCacheGetDec(_xmlcache, key, defaultvalue);
        }
        public int CacheGetInt(string key, int defaultvalue = 0)
        {
            return Cache.XMLCacheGetInt(_xmlcache, key, defaultvalue);
        }
        public long CacheGetLong(string key, long defaultvalue = 0)
        {
            return Cache.XMLCacheGetLong(_xmlcache, key, defaultvalue);
        }
        public string CacheGetStr(string key, string defaultvalue = null)
        {
            return Cache.XMLCacheGetStr(_xmlcache, key, defaultvalue);
        }
        public void CacheSet(string key, object value)
        {
            Cache.XMLCacheSet(_xmlcache, key, value);
        }
        public void CacheSave()
        {
            Cache.XMLCacheSave(_xmlcachename, _xmlcache);
        }

        #endregion

        #region JSON Path Functions

        static public object JPathGet(Hashtable jo, string path)
        {
            if (jo == null || string.IsNullOrEmpty(path)) return null;

            Hashtable h = jo;
            string[] trips = path.Split('\\');
            for (int i = 0; i < trips.Length - 1; i++)
            {
                h = (Hashtable)h[trips[i]];
                if (h == null) return null;
            }
            object value = h[trips[trips.Length - 1]];
            return value;
        }
        static public string JPathGetStr(Hashtable jo, string path)
        {
            return lib.ToStr(JPathGet(jo, path));
        }
        static public DateTime JPathGetDate(Hashtable jo, string path)
        {
            return lib.ToDate(JPathGet(jo, path));
        }
        static public int JPathGetInt(Hashtable jo, string path)
        {
            return lib.ToInt(JPathGet(jo, path));
        }
        static public double JPathGetDbl(Hashtable jo, string path)
        {
            return lib.ToDouble(JPathGet(jo, path));
        }
        static public decimal JPathGetDec(Hashtable jo, string path)
        {
            return lib.ToDecimal(JPathGet(jo, path));
        }

        static public object JPathGetArray(Hashtable jo, string path, int index)
        {
            Hashtable h = jo;
            string[] trips = path.Split('\\');
            for (int i = 0; i < trips.Length - 1; i++)
            {
                Hashtable tmp = (Hashtable)h[trips[i]];
                if (tmp == null) tmp = new Hashtable();
                h = tmp;
            }
            object value = h[trips[trips.Length - 1]];
            if (value != null && value is Array)
            {
                value = ((Array)value).GetValue(index);
            }
            return value;
        }
        static public bool JPathSet(Hashtable jo, string path, object data)
        {
            bool success = false;
            Hashtable h = jo;
            string[] trips = path.Split('\\');

            for (int i = 0; i < trips.Length - 1; i++)
            {
                Hashtable tmp = (Hashtable)h[trips[i]];
                if (tmp == null)
                {
                    tmp = new Hashtable();
                    h[trips[i]] = tmp;
                }
                h = tmp;
            }

            object value = h[trips[trips.Length - 1]];
            if (value == null || value.GetType().Name == data.GetType().Name)
            {
                h[trips[trips.Length - 1]] = data;
                success = true;
            }
            return success;
        }

        #endregion
    }
}