using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Microsoft.Win32;  // RegisterKey

using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using IPos.DB;

namespace IPOS.Process
{
    public class ProcessList
    {
        //1			StartProcess	        Өдрийн хаалтын процесс эхлэв	1	D
        //2			SaveRate	            Ханшийн түүх хадгалах	1	D
        //3			MemberAmortization	    Гишүүний үлдэгдэлийг амортизашн хийх	1	D
        //4			MemeberClosing	        Гишүүний урьдчилсан төлбөрийг хугацаа дуусахад орлогод авах	1	D
        //5			ChangeBusinessDay	    Өдөр солих	1	D
        //6			SetStatusInvSerials	    Барааны төлөвийг Unavailable болгох	1	D
        //7			CleanCustomerSales	    Харицагч бүр дээр борлуулалтын дугаарыг цэвэрлэх	1	D
        //8			CleanCustomerSerials	Харицагч бүр дээр Serial дугаарыг цэвэрлэх	1	D
        //9			CleanRebates	        Хөнгөлөлтийн түр хадгалдаг table-уудыг цэвэрлэх	1	D
        //10		FinPosting	            Санхүүхгийн програм руу пост хийх	1	D
        //11		CleanJournal	        Борлуулалтын цэвэрлэх, түүх рүү хадгалах	1	D
        //12		PledgeMoveHist	        Барьцаа хөрөнгийн мэдээллийн table-уудыг цэвэрлэх, түүх рүү хадгалах (Pledge)	1	D
        //13		ContractMoveHist	    Хугацаа дууссан гэрээнүүдийг цэвэрлэх, түүх рүү хадгалах (Contract)	1	D
        //14		AutoStatement	        Автоматаар хуулга илгээх	1	D
        //15		SaveDailyRate	        Өдрийн ханш шинэчлэх	1	D
        //16		EndProcess	            Өдрийн хаалтын процесс дуусав	1	D

        #region [ Variables ]
        int _InitUserNo;
        DateTime  _TxnDate;
        int _ProcessNo;
        string _ProcessFunc;
        string _Name;
        DateTime _StartDate;
        DateTime _EndDate;
        int _Status;
        string _ErrorDesc;
        string _Freq;
        private static Hashtable _ProcessLists = new Hashtable();
        #endregion
        #region [ Properties ]
        public int InitUserNo { get { return _InitUserNo; } set { _InitUserNo = value; } }
        public DateTime TxnDate { get { return _TxnDate; } set { _TxnDate = value; } }
        public int ProcessNo { get { return _ProcessNo; } set { _ProcessNo = value; } }
        public string ProcessFunc { get { return _ProcessFunc; } set { _ProcessFunc = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public DateTime StartDate { get { return _StartDate; } set { _StartDate = value; } }
        public DateTime EndDate { get { return _EndDate; } set { _EndDate = value; } }
        public int Status { get { return _Status; } set { _Status = value; } }
        public string ErrorDesc { get { return _ErrorDesc; } set { _ErrorDesc = value; } }
        public string Freq { get { return _Freq; } set { _Freq = value; } }
        public Hashtable ProcessLists
        {
            get
            {
                return _ProcessLists;
            }
            set
            {
                _ProcessLists = value;
            }
        }
        #endregion
        #region [ Functions ]
        public Result Add(int pInitUserNo, DateTime pTxnDate, int pProcessNo, string pProcessFunc, string pName, DateTime pStartDate, DateTime pEndDate, int pStatus, string pErrorDesc, string pFreq)
        {
            Result res = new Result();
            try
            {
                ProcessList proc = new ProcessList();
                proc.InitUserNo  = pInitUserNo;
                proc.TxnDate = pTxnDate;
                proc.ProcessNo = pProcessNo;
                proc.ProcessFunc = pProcessFunc;
                proc.Name = pName;
                proc.StartDate = pStartDate;
                proc.EndDate = pEndDate;
                proc.Status = pStatus;
                proc.ErrorDesc = pErrorDesc;
                proc.Freq = pFreq;
                _ProcessLists.Add(pProcessNo, proc);

                res.ResultNo = 0;
                return res;
            }
            catch (Exception ex)
            {
                res = new Result(1, "Алдаа гарлаа. (" + ex.Message + ")");
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        public ProcessList Get(int key)
        {
            if (_ProcessLists.ContainsKey(key))
            {
                return (ProcessList)_ProcessLists[key];
            }
            else
            {
                return null;
            }
        }
        public Result Init(DbConnections db, DateTime pTxnDate, int pUserNo)
        {
            Result res = new Result();
            int ListEmpty = 0;
            DataTable DT;
            try
            {
                #region [ Өмнөх өдрийн EOD нь дуусаагүй байгаа эсэх ]
                res = ProcessDB.DB2131001(db, pTxnDate.AddDays(-1));
                if (res.ResultNo != 0) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                if (res.Data == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                if (res.Data.Tables[0] == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                DT = res.Data.Tables[0];
                ListEmpty = DT.Rows.Count;
                if (ListEmpty != 0)
                {
                    // Өмнө өндөрлөсөн өндөрлөлтийг гүйцээх гэж байна
                    res = Init_LoadDay(db, pTxnDate.AddDays(-1), pUserNo);
                    return res;
                }
                #endregion
                #region [ Энэ өдөр өмнө өндөрлөж байсан эсэх]
                // "select * from ProcessList where TxnDate=:1"
                res = ProcessDB.DB2131002(db, pTxnDate);
                if (res.ResultNo != 0) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                if (res.Data == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                if (res.Data.Tables[0] == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                DT = res.Data.Tables[0];
                ListEmpty = DT.Rows.Count;
                #endregion

                if (ListEmpty == 0)
                {
                    // Шинэ өдрийн өндөрлөлт хийх гэж байна
                    res = Init_NewDay(db, pTxnDate, pUserNo);
                }
                else
                {
                    // Өмнө өндөрлөсөн өндөрлөлтийг гүйцээх гэж байна
                    res = Init_LoadDay(db, pTxnDate, pUserNo);
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }
        private Result Init_NewDay(DbConnections db, DateTime pTxnDate, int pUserNo)
        {
            Result res = new Result();
            try
            {
                #region  []
                ProcessList proc = new ProcessList();
                proc.ProcessLists.Clear();
                res = ProcessDB.DB2131003(db, null);
                if (res.ResultNo != 0) return res;
                if (res.Data != null && res.Data.Tables[0] != null)
                {
                    foreach (DataRow dr in res.Data.Tables[0].Rows)
                    {
                        if(Static.ToInt(dr["Status"])==1)
                        proc.Add(pUserNo, pTxnDate, Static.ToInt(dr["pID"]), Static.ToStr(dr["FunctionName"]), Static.ToStr(dr["Description"]), DateTime.Now, DateTime.Now, 0, "", Static.ToStr(dr["Freq"]));
                    }
                }
                else
                {
                    return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); 
                }
                #endregion

                res = WriteFullProcess(db);

                res = ProcessDB.DB2131002(db, pTxnDate);
                if (res.ResultNo != 0) return res;
                if (res.Data == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                if (res.Data.Tables[0] == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }
        private Result Init_LoadDay(DbConnections db, DateTime pTxnDate, int pUserNo)
        {
            Result res = new Result();
            try
            {
                #region  []
                // "select * from ProcessList where TxnDate=:1"
                res = ProcessDB.DB2131002(db, pTxnDate);
                if (res.ResultNo != 0) return res;
                if (res.Data == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                if (res.Data.Tables[0] == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                DataTable DT = res.Data.Tables[0];

                ProcessList proc = new ProcessList();
                proc.ProcessLists.Clear();

                foreach (DataRow row in DT.Rows)
                {
                    res = proc.Add(pUserNo, pTxnDate, Static.ToInt(row["ProcessNo"]), Static.ToStr(row["ProcessFunc"]), Static.ToStr(row["Name"]), Static.ToDate(row["StartDate"]), Static.ToDate(row["EndDate"]), Static.ToInt(row["Status"]), Static.ToStr(row["ErrorDesc"]), Static.ToStr(row["Freq"]));
                    if (res.ResultNo != 0) return res;
                }
                #endregion
                DataSet DS = new DataSet();
                DS.Tables.Add(DT.Copy());
                res.Data = DS;
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }
        public Result WriteProcess(DbConnections db, int pProcessNo)
        {
            Result res = new Result();
            object[] obj = new object[6];
            try
            {
                ProcessList tmp = Get(pProcessNo);
                // update ProcessList
                // set StartDate = :1, EndDate = :2, Status=:3, ErrorDesc =:4
                // where TxnDate=:5 and ProcessNo=:6

                obj[0] = Static.ToDate(tmp.StartDate);
                obj[1] = Static.ToDate(tmp.EndDate);
                obj[2] = Static.ToInt(tmp.Status);
                obj[3] = Static.ToStr(tmp.ErrorDesc);
                obj[4] = Static.ToDate(tmp.TxnDate);
                obj[5] = Static.ToInt(tmp.ProcessNo);

                res = ProcessDB.DB2131005(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }
        public Result WriteFullProcess(DbConnections db)
        {
            Result res = new Result();
            object[] obj = new object[9];
            try
            {
                foreach (int key in _ProcessLists.Keys)
                {
                    ProcessList tmp = (ProcessList)_ProcessLists[key];

                    obj[0] = Static.ToDate(tmp.TxnDate);
                    obj[1] = Static.ToInt(tmp.ProcessNo);
                    obj[2] = Static.ToStr(tmp.ProcessFunc );
                    obj[3] = Static.ToStr(tmp.Name );
                    obj[4] = Static.ToDate(tmp.StartDate);
                    obj[5] = Static.ToDate(tmp.EndDate);
                    obj[6] = Static.ToInt(tmp.Status);
                    obj[7] = Static.ToStr(tmp.ErrorDesc);
                    obj[8] = Static.ToStr(tmp.Freq);
                    res = ProcessDB.DB2131004(db, obj);
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }
        #endregion
    }
    public class GLFILE
    {
        public string BatchNo;
        public string SalesNo;
        public long SeqNo;
        public DateTime TxnDate;
        public DateTime PostDate;
        public string AccountNo;
        public int AccountEntry;
        public int EntryType;       // 0 - Normal,  1 - Main,  2 - Payment
        public decimal TxnAmount;
        public string CurCode;
        public string Desc;
        public string CustNo;
        public string ContractNo;
        public string PaymentType;

        public void Clear()
        {
            this.BatchNo="";
            this.SalesNo = "";
            this.SeqNo = 0;
            this.TxnDate = DateTime.Now;
            this.PostDate = DateTime.Now;
            this.AccountNo = "";
            this.AccountEntry = 0;
            this.EntryType = 0;
            this.TxnAmount = 0;
            this.CurCode = "";
            this.Desc = "";
            this.CustNo = "";
            this.ContractNo = "";
            this.PaymentType = "";
        }
        public void ClearTran()
        {
            this.AccountNo = "";
            this.AccountEntry = 0;
            this.TxnAmount = 0;
        }
        public object[] Get()
        {
            object[] obj = new object[14];
            obj[0] = this.BatchNo;
            obj[1] = this.SalesNo;
            obj[2] = this.SeqNo;
            obj[3] = this.TxnDate;
            obj[4] = this.PostDate;
            obj[5] = this.AccountNo;
            obj[6] = this.AccountEntry;
            obj[7] = this.EntryType;
            obj[8] = this.TxnAmount;
            obj[9] = this.CurCode;
            obj[10] = this.Desc;
            obj[11] = this.CustNo;
            obj[12] = this.ContractNo;
            obj[13] = this.PaymentType;

            string GLFileStr =  this.BatchNo + "|"+
                                this.SalesNo + "|"+
                                Static.ToStr(this.SeqNo) + "|"+
                                Static.ToStr(this.TxnDate) + "|" +
                                this.AccountNo + "|" +
                                Static.ToStr(this.AccountEntry) + "|" +
                                Static.ToStr(this.TxnAmount) + "|" +
                                this.CurCode + "|" +
                                this.Desc + "|" +
                                this.CustNo + "|" +
                                this.ContractNo + "|" +
                                this.PaymentType;

            ExportFile("GLFILE.log", GLFileStr + "\n");
            
            return obj;
        }
        public void GetDRow(DataRow row)
        {
            this.Clear();

            BatchNo = Static.ToStr(row["BatchNo"]);
            SalesNo= Static.ToStr(row["BatchNo"]);
            SeqNo= Static.ToLong(row["BatchNo"]);
            TxnDate= Static.ToDate(row["BatchNo"]);
            PostDate= Static.ToDate(row["BatchNo"]);
            AccountNo= Static.ToStr(row["BatchNo"]);
            AccountEntry= Static.ToInt(row["BatchNo"]);
            EntryType= Static.ToInt(row["BatchNo"]);
            TxnAmount= Static.ToDecimal(row["BatchNo"]);
            CurCode= Static.ToStr(row["BatchNo"]);
            Desc= Static.ToStr(row["BatchNo"]);
            CustNo= Static.ToStr(row["BatchNo"]);
            ContractNo= Static.ToStr(row["BatchNo"]);
            PaymentType= Static.ToStr(row["BatchNo"]);
        }
        static public void ExportFile(string filename, string s)
        {
            try
            {
                string _workingfolder="";
                string path = Assembly.GetExecutingAssembly().Location;
                int i = path.LastIndexOf(@"\");
                if (i > 0) _workingfolder = path.Substring(0, i);

                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("{0}\\{1}_{2}.log", _workingfolder, filename, DateTime.Today.ToString("yyyyMMdd")));

                StreamWriter sw = new StreamWriter(sb.ToString(), true);
                sw.WriteLine(s);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
            }
        }
    }
}
