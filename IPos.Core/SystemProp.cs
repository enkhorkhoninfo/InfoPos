using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;

namespace IPos.Core
{
    public static class SystemProp
    {
        enum ProcessStateFlag : int 
        { 
            Normal = 0,
            Process = 1
        };

        private static DateTime _TxnDate;

        private static DateTime _GLTxnDate;
        private static int _GLProcessStart;
        private static int _EODProcessStart;

        private static DataTable _GenList;

        private static int _SystemState = 0;
        private static int _GLProcessStatus = 0;
        private static int _SystemBranchNo = 0;
        private static string _InstCode;
        private static string _InstName;
        private static string _InstName2;
        private static string _BaseCurrency;
        private static string _BACFormat;
        private static string _DefaultNumberFormat;
        private static string _ContractNumberFormat;
        private static long _TrackGroupID;
        private static long _PosGLNumber;
        private static long _YearProfitGLNumber;
        private static long _YearLossGLNumber;
        private static string _PolicyMask;

        private static string _PathTerminal;
        private static string _PathDynamicRpt;
        private static string _PathDynamicDoc;
        private static string _PathSlips;
        private static string _UpdateExtention;

        private static Hashtable _ProcessStates;
        private static Hashtable _GLProcessStates;

        private static int _MServerPort;
        private static string _MServerName;
        private static string _MailUserName;
        private static string _MailUserPass;
        private static string _FromUser;

        private static long _VAT;

        private static string _VatAccountNo;
        private static string _SaleAccountNo;
        private static int _PaymentTran;

        static public DateTime TxnDate
        {
            get { return _TxnDate; }
            set 
            {
                try
                {
                    _TxnDate = value;
                }
                catch { }
            }
        }

        static public DateTime GLTxnDate
        {
            get { return _GLTxnDate; }
            set
            {
                try
                {
                    _GLTxnDate = value;
                }
                catch { }
            }
        }
        static public int GLProcessStart
        {
            get { return _GLProcessStart; }
            set
            {
                try
                {
                    _GLProcessStart = value;
                }
                catch { }
            }
        }
        static public int EODProcessStart
        {
            get { return _EODProcessStart; }
            set
            {
                try
                {
                    _EODProcessStart = value;
                }
                catch { }
            }
        }

        static public DataTable GenList
        {
            get { return _GenList; }
            set { _GenList = value; }
        }

        static public int SystemState
        {
            get { return _SystemState; }
            set { _SystemState = value; }
        }
        static public int GLProcessStatus
        {
            get { return _GLProcessStatus; }
            set { _GLProcessStatus = value; }
        }
        static public Result SetGLProcessStatus(DbConnections db, int status)
        {
            //asdResult res = HPro.DB.ProcessDB.DB213011(db, new object[] { status });
                Result res = null;
            return res;
        }
        static public int SystemBranchNo
        {
            get { return _SystemBranchNo; }
            set { _SystemBranchNo = value; }
        }
        static public string InstCode
        {
            get { return _InstCode; }
            set { _InstCode = value; }
        }
        static public string InstName
        {
            get { return _InstName; }
            set { _InstName = value; }
        }
        static public string InstName2
        {
            get { return _InstName2; }
            set { _InstName2 = value; }
        }
        static public string BaseCurrency
        {
            get { return _BaseCurrency; }
            set { _BaseCurrency = value; }
        }
        static public string BACFormat
        {
            get { return _BACFormat; }
            set { _BACFormat = value; }
        }
        static public string DefaultNumberFormat
        {
            get { return _DefaultNumberFormat; }
            set { _DefaultNumberFormat = value; }
        }
        static public string ContractNumberFormat
        {
            get { return _ContractNumberFormat; }
            set { _ContractNumberFormat = value; }
        }
        static public long TrackGroupID
        {
            get { return _TrackGroupID; }
            set { _TrackGroupID = value; }
        }
        static public long PosGLNumber
        {
            get { return _PosGLNumber; }
            set { _PosGLNumber = value; }
        }
        static public long YearProfitGLNumber
        {
            get { return _YearProfitGLNumber; }
            set { _YearProfitGLNumber = value; }
        }
        static public long YearLossGLNumber
        {
            get { return _YearLossGLNumber; }
            set { _YearLossGLNumber = value; }
        }
        static public string PolicyMask
        {
            get { return _PolicyMask; }
            set { _PolicyMask = value; }
        }
        static public ulong NextCustomerNo
        {
            get { return EServ.Interface.Sequence.NextByVal("CU",10); } 
        }
        static public ulong NextContractID
        {
            get { return EServ.Interface.Sequence.NextByVal("CO", 11); }
        }
        static public ulong NextRequestNo
        {
            get { return EServ.Interface.Sequence.NextByVal("RE", 12); }
        }
        static public ulong NextThreadNo
        {
            get { return EServ.Interface.Sequence.NextByVal("TH", 13); }
        }
        static public ulong NextJournalNo
        {
            get { return EServ.Interface.Sequence.NextByVal("JR",1); }
        }
        static public ulong NextGLEntryNo
        {
            get { return EServ.Interface.Sequence.NextByVal("GL", 15); }
        }
        static public ulong NextFANo
        {
            get { return EServ.Interface.Sequence.NextByVal("FA", 16); }
        }
        static public ulong NextCTANumber
        {
            get { return EServ.Interface.Sequence.NextByVal("CT", 17); }
        }
        static public ulong NextInvNo
        {
            get { return EServ.Interface.Sequence.NextByVal("IV", 18); }
        }

        static public string PathTerminal
        {
            get { return _PathTerminal; }
            set { _PathTerminal = value; }
        }
        static public string PathDynamicRpt
        {
            get { return _PathDynamicRpt; }
            set { _PathDynamicRpt = value; }
        }
        static public string PathDynamicDoc
        {
            get { return _PathDynamicDoc; }
            set { _PathDynamicDoc = value; }
        }
        static public string PathSlips
        {
            get { return _PathSlips; }
            set { _PathSlips = value; }
        }
        static public string UpdateExtention
        {
            get { return _UpdateExtention; }
            set { _UpdateExtention = value; }
        }

        public static Hashtable ProcessStates
        {
            get
            {
                return _ProcessStates;
            }
            set
            {
                _ProcessStates = value;
            }
        }
        public static Hashtable GLProcessStates
        {
          get
          {
            return _GLProcessStates;
          }
          set
          {
            _GLProcessStates = value;
          }
        }

        public static string MServerName
        {
            get
            {
                return _MServerName;
            }
            set
            {
                _MServerName = value;
            }
        }
        public static int MServerPort
        {
            get
            {
                return _MServerPort;
            }
            set
            {
                _MServerPort = value;
            }
        }
        public static string MailUserName
        {
            get
            {
                return _MailUserName;
            }
            set
            {
                _MailUserName = value;
            }
        }
        public static string MailUserPass
        {
            get
            {
                return _MailUserPass;
            }
            set
            {
                _MailUserPass = value;
            }
        }
        public static string MailFromUser
        {
            get
            {
                return _FromUser;
            }
            set
            {
                if (_FromUser == value)
                    return;
                _FromUser = value;
            }
        }

        static public long VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }

        static public string VatAccountNo
        {
            get { return _VatAccountNo; }
            set { _VatAccountNo = value; }
        }
        static public string SaleAccountNo
        {
            get { return _SaleAccountNo; }
            set { _SaleAccountNo = value; }
        }
        static public int PaymentTran
        {
            get { return _PaymentTran; }
            set { _PaymentTran = value; }
        }

        static public BACProd gBACProd = new BACProd();
        static public CONProd gCONProd = new CONProd(); 
        static public Cur gCur = new Cur();
        static public AccountCode gAccountCode = new AccountCode();
        static public FAType gFAType = new FAType();
        static public InvType gInvType = new InvType(); 
        static public FinTxn gFinTxn = new FinTxn();
        //static public DealProduct gDealProduct = new DealProduct();
        static public ChartGroup gChartGroup = new ChartGroup();
        static public Branch gBranch = new Branch();
        //static public FundProduct gFundProduct = new FundProduct();
        //static public Fund gFund = new Fund();
        static public AutoNum gAutoNum = new AutoNum();
        static public AutoNumValue gAutoNumValue = new AutoNumValue();
    }
}