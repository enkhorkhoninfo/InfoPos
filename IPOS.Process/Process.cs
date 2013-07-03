using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using IPos.Core;
using IPos.DB;

namespace IPOS.Process
{
    public class Process : IModule
    {
        static ProcessList proc = new ProcessList();
        DbConnections dbase;
        RequestInfo rinfo;
        DbConnection con = null;
        #region [ Invoke ]
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 100004: 	            // Өдөр өндөрлөлтийн жагсаалт
                        res = ProcessListTerminal(ci, ri, db, ref lg);
                        break;
                    case 100005: 	            // Өдөр өндөрлөлт эхлэх
                        res = ProcessRunTerminal(ci, ri, db, ref lg);
                        break;
                    default:
                        res.ResultNo = 9110009;
                        res.ResultDesc = "Функц тодорхойлогдоогүй байна";
                        break;
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
        #region [ EOD Process ]
        private Result InitList(DbConnections db, int pUserNo)
        {
            Result res = new Result();
            int iUserNo =0;
            try
            {
                if (SystemProp.SystemState == 1)
                {
                    foreach ( object key in proc.ProcessLists.Keys)
                    {
                        iUserNo = ((ProcessList)proc.ProcessLists[key]).InitUserNo;
                        break;
                    }
                    if (iUserNo != pUserNo)
                    {
                        return new Result(1, "Өндөрлөх процесс эхэлсэн байна");
                    }
                }
                
                res = proc.Init(db, SystemProp.TxnDate, pUserNo);
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
        private Result ProcessListTerminal(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = InitList(db, ri.UserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Interface.Console.Print(ex.Message, EServ.Interface.Console.enumConsoleLogType.Process);
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }
        private Result ProcessRunTerminal(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();

            try
            {
                // EOD эхлүүлэхэд болох эсэх шалгалт
                res = CheckEOD(db);
                if (res.ResultNo != 0)
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Бүгд ПОС хаагдсан байх ёстой." + res.ResultDesc;
                    return res;
                }

                res = InitList(db, ri.UserNo);

                if (res.ResultNo == 0)
                {
                    dbase = db;
                    rinfo = ri;
                    System.Threading.Thread t;
                    t = new System.Threading.Thread(ProcessRunAsync);
                    t.Start();

                    res.ResultNo = 0;
                    res.ResultDesc = "Процессийг амжилттай эхлүүллээ...";
                }
                else { return res; }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Interface.Console.Print(ex.Message, EServ.Interface.Console.enumConsoleLogType.Process);
                ISM.Lib.Static.WriteToLogFile("ProcessErrorError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }
        private void ProcessRunAsync()
        {
            Result res;
            bool isRun = false;
            string day = "";
            ArrayList tmpList = new ArrayList(proc.ProcessLists.Keys);
            tmpList.Sort();
            System.Collections.IEnumerator tmpListEnum = tmpList.GetEnumerator();

            while (tmpListEnum.MoveNext())
            {
                ProcessList tmp = (ProcessList)proc.Get(Static.ToInt(tmpListEnum.Current));
                isRun = false;
                switch (tmp.Freq)
                {
                    case "D":
                        isRun = true;
                        break;
                    case "M":
                        //isRun = true;
                        string day02 = "0228";
                        if (SystemProp.TxnDate.Month == 2)
                        {
                            int days = DateTime.DaysInMonth(SystemProp.TxnDate.Year, SystemProp.TxnDate.Month);
                            day02 = "02" + days.ToString().Trim();
                        }

                        day = SystemProp.TxnDate.ToString("MMdd");
                        if (day == "0131" ||
                            day == "0331" ||
                            day == day02 ||
                            day == "0430" ||
                            day == "0531" ||
                            day == "0630" ||
                            day == "0731" ||
                            day == "0831" ||
                            day == "0930" ||
                            day == "1031" ||
                            day == "1130" ||
                            day == "1231")
                            isRun = true;
                        break;
                    case "Q":
                        day = SystemProp.TxnDate.ToString("MMdd");
                        if (day == "0331" || day == "0630" || day == "0930" || day == "1231")
                            isRun = true;
                        break;
                    case "Y":
                        day = SystemProp.TxnDate.ToString("MMdd");
                        if (day == "1231")
                            isRun = true;
                        break;
                }
                if (isRun)
                {
                    res = ProcessRunTask(dbase, rinfo, tmp);
                    if (res.ResultNo != 0) return;
                }
            }
        }
        private Result ProcessRunTask(DbConnections db, RequestInfo ri, ProcessList tmp)
        {
            Result res = new Result();
            DateTime starttime;
            DateTime endtime;
            try
            {
                if (tmp.Status == 0 || tmp.Status == 1)
                {
                    starttime = DateTime.Now;
                    tmp.StartDate = starttime;

                    switch (tmp.ProcessFunc)
                    {
                        case "StartProcess":
                            res = StartProcess(db);
                            break;
                        case "ContractAmortization":
                            res = ContractAmortization(db);
                            break;
                        case "PostToGLFile":
                            res = PostToGLFile(db);
                            break;
                        case "PostToAbacus":
                            res = PostToAbacus(db);
                            break;
                        case "SaveRate":
                            res = SaveRate(db);
                            break;
                        case "SaveDailyRate":
                            res = SaveDailyRate(db);
                            break;
                        //------------------------------------------------------------
                        case "ChangeBusinessDay":
                            res = ChangeBusinessDay(db);
                            break;
                        case "SetStatusInvSerials":
                            res = SetStatusInvSerials(db);
                            break;
                        case "CleanCustomerSales":
                            res = CleanCustomerSales(db);
                            break;
                        case "CleanJournal":
                            res = CleanJournal(db);
                            break;
                        case "PledgeMoveHist":
                            res = PledgeMoveHist(db);
                            break;
                        case "ContractMoveHist":
                            res = ContractMoveHist(db);
                            break;
                        case "EndProcess":
                            res = EndProcess(db);
                            break;
                    }
                    endtime = DateTime.Now;

                    tmp.EndDate = endtime;
                    if (res.ResultNo == 0)
                    {
                        tmp.Status = 9;
                        tmp.EndDate = endtime;
                        tmp.ErrorDesc = "Амжилттай хийгдлээ .";
                    }
                    else
                    {
                        tmp.Status = 1;
                        tmp.ErrorDesc = res.ResultDesc;
                    }

                    proc.WriteProcess(db, tmp.ProcessNo);
                    if (tmp.Status == 1)
                    {
                        res.ResultDesc = "Stop process ... (Please contact administrator)";
                        return res;
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Interface.Console.Print(ex.Message, EServ.Interface.Console.enumConsoleLogType.Process);
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                Result send = new Result();
                send.ResultNo = 6;
                send.Param = new object[] { tmp.ProcessFunc, tmp.ProcessNo, tmp.Status, tmp.ErrorDesc, tmp.StartDate, tmp.EndDate };
                EServ.Interface.Server.SendToAll(1, send);
            }
        }
        #endregion
        #region [ EOD STEP'S ]
        #region [System steps ]
        private Result StartProcess(DbConnections db)
        {
            Result res = new Result();
            try
            {
                SystemProp.SystemState = 1;
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result ChangeBusinessDay(DbConnections db)
        {
            Result res = new Result();
            try
            {
                SystemProp.TxnDate = SystemProp.TxnDate.AddDays(1);

                res = ProcessDB.DB2131101(db, new object[] { SystemProp.TxnDate.ToString("yyyy.MM.dd"), "TxnDate" });

                Result send = new Result();
                send.ResultNo = 1; // Огноо солих
                send.Param = new object[] { SystemProp.TxnDate };
                EServ.Interface.Server.SendToAll(1, send);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result CheckEOD(DbConnections db)
        {
            Result res = new Result();
            try
            {
                //Өдөр өндөрлөлт хийж болох эсэх. Бүх Пос хаагдсан эсэх
                //Select PosNo, PosName, Status from PosTerminal where Status<3;
                res = ProcessDB.DB2131102(db);
                if (res.Data.Tables[0].Rows.Count > 0)
                {
                    res.ResultNo = 1;
                    res.ResultDesc = Static.ToStr(res.Data.Tables[0].Rows[0][0]);
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result EndProcess(DbConnections db)
        {
            Result res = new Result();
            try
            {
                SystemProp.SystemState = 0;
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        #endregion
        #region [ EOD Business process]
        private Result ContractAmortization(DbConnections db)
        {
            Result res = new Result();
            string sql = "";
            decimal vTxnAmount= 0;
            int totalFreq = 0; 
            int todayFreq = 0;
            int i = 1;
            long vSalesno=0;
            try
            {
                res = ProcessDB.DB2131110(db, SystemProp.TxnDate);
                if (res.ResultNo != 0) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа" + res.ResultDesc; }
                if (res.Data == null) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа"; }
                if (res.Data.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа"; }

                DataTable sales = res.Data.Tables[0];
                vSalesno = DateTime.Now.Ticks;
                foreach (DataRow row in sales.Rows)
                {
                    // Amortization хийх дүнгээ тооцоолж гаргах
                    // ContractNo, ContractType, CustNo, ValidStartDate, ValidEndDate, Amount, CurCode, 
                    // DepFreq, DepAmount, BalanceType, Vat, AccountNo, IncomeAccountNo, DepBalance 
                    if (Static.ToDate(row["ValidEndDate"])<=SystemProp.TxnDate)
                    {
                        vTxnAmount = Static.ToDecimal(row["DepBalance"]);
                    }
                    else
                    {
                        if (Static.ToStr(row["DepFreq"])=="S")
                        {
                            if (Static.ToDecimal(row["DepBalance"]) < Static.ToDecimal(row["NRSAmount"]))
                            {
                                vTxnAmount = Static.ToDecimal(row["DepBalance"]);
                            }
                            else
                            {
                                vTxnAmount = Static.ToDecimal(row["NRSAmount"]);
                            }
                        }
                        if (Static.ToStr(row["DepFreq"])=="M" || Static.ToStr(row["DepFreq"])=="Q" || Static.ToStr(row["DepFreq"])=="Y") 
                        {
                            totalFreq = 0;
                            todayFreq = 0;
                            totalFreq = (Static.ToDate(row["ValidEndDate"]).Year * 12 + Static.ToDate(row["ValidEndDate"]).Month) - (Static.ToDate(row["ValidStartDate"]).Year * 12 + Static.ToDate(row["ValidStartDate"]).Month);
                            todayFreq = (SystemProp.TxnDate.Year * 12 + SystemProp.TxnDate.Month) - (Static.ToDate(row["ValidStartDate"]).Year * 12 + Static.ToDate(row["ValidStartDate"]).Month);
                            switch (Static.ToStr(row["DepFreq"]))
                            { 
                                case "M":
                                    totalFreq = totalFreq + 1;
                                    todayFreq = todayFreq + 1;
                                    break;
                                case "Q":
                                    totalFreq = Convert.ToInt16(Math.Truncate(totalFreq / 3.0)) + 1;
                                    todayFreq = Convert.ToInt16(Math.Truncate(todayFreq / 3.0)) + 1;
                                    break;
                                case "Y":
                                    totalFreq = Convert.ToInt16(Math.Truncate(totalFreq / 12.0)) + 1;
                                    todayFreq = Convert.ToInt16(Math.Truncate(todayFreq / 12.0)) + 1;
                                    break;
                            }

                            if (Static.ToDecimal(row["DepBalance"]) > (totalFreq - todayFreq) * Static.ToDecimal(row["DepAmount"]))
                            {
                                vTxnAmount = Static.ToDecimal(row["DepBalance"]) - (totalFreq - todayFreq) * Static.ToDecimal(row["DepAmount"]);
                            }
                       }
                    }
                    if (vTxnAmount > 0)
                    {
                        sql = @"
                                DECLARE 
                                BEGIN 
                                  DO_Amortization ( :1, :2, :3, :4, :5, :6, :7, :8, :9 );
                                END;
                    ";
                        #region [ PLSQL ]
                        ///* Formatted on 3/24/2013 7:47:37 PM (QP5 v5.185.11230.41888) */
                        //CREATE OR REPLACE PROCEDURE INFOPOSNEW.DO_Amortization (
                        //   pDate         IN     DATE,
                        //   pSalesNo      IN     NUMBER,
                        //   pSeqNo        IN     NUMBER,
                        //   pTxnAmount    IN     NUMBER,
                        //   pCurCode      IN     VARCHAR2,
                        //   pContractNo   IN     VARCHAR2,
                        //   pCustNo       IN     VARCHAR2,
                        //   pAccountNo1   IN     VARCHAR2,
                        //   pAccountNo2   IN     VARCHAR2,
                        //   pResult          OUT NUMBER,
                        //   pResultDesc      OUT NVARCHAR2)
                        //IS
                        //   vPostDate                 DATE;
                        //   vAccountEntry             NUMBER (1);                  -- 0-Debit, 1-Credit
                        //   vEntryType                NUMBER (2);       -- 0-Main, 1-General, 2-Payment
                        //   vDesc                     NVARCHAR2 (100);

                        //   amortization_domination   EXCEPTION;
                        //BEGIN
                        //   vEntryType := 0;                            -- 0-Main, 1-General, 2-Payment
                        //   vAccountEntry := 0;
                        //   vPostDate := SYSDATE;
                        //   vDesc := 'EOD гэрээний элэгдүүлэлт';

                        //   UPDATE CONTRACTMAIN
                        //      SET DepBalance = DepBalance - pTxnAmount
                        //    WHERE ContractNo = pContractNo;

                        //   INSERT INTO GLFILE (SALESNO,
                        //                       TXNDATE,
                        //                       POSTDATE,
                        //                       POSNO,
                        //                       CASHIERNO,
                        //                       SHIFTNO,
                        //                       CUSTNO,
                        //                       CONTRACTNO,
                        //                       SEQNO,
                        //                       ACCOUNTNO,
                        //                       ACCOUNTENTRY,
                        //                       ENTRYTYPE,
                        //                       TXNAMOUNT,
                        //                       CURCODE,
                        //                       DESCRIPTION,
                        //                       PAYMENTTYPE)
                        //        VALUES (pSalesNo,
                        //                pDate,
                        //                vPostDate,
                        //                0,
                        //                0,
                        //                0,
                        //                pCustNo,
                        //                pContractNo,
                        //                pSeqNo,
                        //                pAccountNo1,
                        //                vAccountEntry,
                        //                vEntryType,
                        //                pTxnAmount,
                        //                pCurCode,
                        //                vDesc,
                        //                '');

                        //   vEntryType := 1;                            -- 0-Main, 1-General, 2-Payment
                        //   vAccountEntry := 1;

                        //   INSERT INTO GLFILE (SALESNO,
                        //                       TXNDATE,
                        //                       POSTDATE,
                        //                       POSNO,
                        //                       CASHIERNO,
                        //                       SHIFTNO,
                        //                       CUSTNO,
                        //                       CONTRACTNO,
                        //                       SEQNO,
                        //                       ACCOUNTNO,
                        //                       ACCOUNTENTRY,
                        //                       ENTRYTYPE,
                        //                       TXNAMOUNT,
                        //                       CURCODE,
                        //                       DESCRIPTION,
                        //                       PAYMENTTYPE)
                        //        VALUES (pSalesNo,
                        //                pDate,
                        //                vPostDate,
                        //                0,
                        //                0,
                        //                0,
                        //                pCustNo,
                        //                pContractNo,
                        //                pSeqNo + 1,
                        //                pAccountNo2,
                        //                vAccountEntry,
                        //                vEntryType,
                        //                pTxnAmount,
                        //                pCurCode,
                        //                vDesc,
                        //                '');

                        //   COMMIT;
                        //EXCEPTION
                        //   WHEN DUP_VAL_ON_INDEX
                        //   THEN
                        //      raise_application_error (
                        //         -20001,
                        //         'You have tried to insert a duplicate supplier_id.');
                        //      ROLLBACK;
                        //   WHEN amortization_domination
                        //   THEN
                        //      raise_application_error (
                        //         -20222,
                        //            'Мэдээлэл дутуу буруу байна.'
                        //         || 'SalesNo='
                        //         || pSalesNo);
                        //      ROLLBACK;
                        //   WHEN OTHERS
                        //   THEN
                        //      raise_application_error (
                        //         -20001,
                        //         'An error was encountered - ' || SQLCODE || ' -ERROR- ' || SQLERRM);
                        //      ROLLBACK;
                        //END;
                        ///                    
                        #endregion
                        res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Amortization",
                            SystemProp.TxnDate,
                            vSalesno,
                            i,
                            vTxnAmount,
                            Static.ToStr(row["CurCode"]),
                            Static.ToStr(row["ContractNo"]),
                            Static.ToStr(row["CustNo"]),
                            Static.ToStr(row["AccountNo"]),
                            Static.ToStr(row["IncomeAccountNo"])
                            );
                        if (res.ResultNo != 0) return res;
                        i = i + 2;
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        #region [ Posting ]
        private Result PostToGLFile(DbConnections db)
        {
            Result res = new Result();
            try
            {
                string sql = @"
                                DECLARE 
                                BEGIN 
                                  POSTTOGLFILE ( :1, :2, :3 );
                                END;
                ";
                #region [ PL SQL ]
//CREATE OR REPLACE PROCEDURE INFOPOSNEW.PostToGlFile (
//   pDate              IN DATE,
//   pGSalesAccountNo   IN VARCHAR2,
//   pTaxAccountNo      IN VARCHAR2)
//IS
//   vSeqNo               NUMBER (10);

//   vSalesNo             VARCHAR2 (20);
//   vTxnDate             DATE;
//   vPostDate            DATE;
//   vPosNo               NUMBER (10);
//   vCashierNo           NUMBER (10);
//   vShiftNo             NUMBER (10);
//   vContractNo          VARCHAR2 (20);
//   vCustNo              NUMBER (20);
//   vCurCode             VARCHAR2 (3);
//   vAccountNo           VARCHAR2 (20);
//   vAccountEntry        NUMBER (1);                       -- 0-Debit, 1-Credit
//   vEntryType           NUMBER (2);                          -- 0-Main, 1-General, 2-Payment
//   vTxnAmount           NUMBER (18, 2);
//   vDesc                NVARCHAR2 (100);
//   vPaymentType         NVARCHAR2 (10);

//   vSalesAccountNo      VARCHAR2 (20);
//   vRefundAccountNo     VARCHAR2 (20);
//   vDiscountAccountNo   VARCHAR2 (20);
//   vBonusAccountNo      VARCHAR2 (20);
//   vBonusExpAccountNo   VARCHAR2 (20);

//   sales_domination     EXCEPTION;

//   CURSOR curSales
//   IS
//      SELECT a.salesno,
//             a.trandate,
//             a.postdate,
//             a.posno,
//             a.cashierno,
//             a.shiftno,
//             a.CONTRACTNO,
//             a.ORDERNO,
//             a.custno,
//             a.curcode,
//             a.vat,
//             a.salestype,
//             a.flag,
//             a.prodno,
//             a.prodtype,
//             a.qty,
//             a.price,
//             a.baseprice,
//             a.salesamount,
//             a.discountprod,
//             a.discountsales,
//             b.SALESACCOUNTNO,
//             b.REFUNDACCOUNTNO,
//             b.DISCOUNTACCOUNTNO,
//             b.BONUSACCOUNTNO,
//             b.BONUSEXPACCOUNTNO
//        FROM V_SALESLIST a JOIN invmain b ON a.prodno = b.invid
//       WHERE a.trandate = pDate AND a.prodtype = 0
//      UNION ALL
//      SELECT a.salesno,
//             a.trandate,
//             a.postdate,
//             a.posno,
//             a.cashierno,
//             a.shiftno,
//             a.CONTRACTNO,
//             a.ORDERNO,
//             a.custno,
//             a.curcode,
//             a.vat,
//             a.salestype,
//             a.flag,
//             a.prodno,
//             a.prodtype,
//             a.qty,
//             a.price,
//             a.baseprice,
//             a.salesamount,
//             a.discountprod,
//             a.discountsales,
//             b.SALESACCOUNTNO,
//             b.REFUNDACCOUNTNO,
//             b.DISCOUNTACCOUNTNO,
//             b.BONUSACCOUNTNO,
//             b.BONUSEXPACCOUNTNO
//        FROM V_SALESLIST a JOIN servmain b ON a.prodno = b.servid
//       WHERE a.trandate = pDate AND a.prodtype = 1;

//   CURSOR curPayment
//   IS
//      SELECT pa.salesno,
//             sa.trandate,
//             sa.postdate,
//             sa.posno,
//             sa.cashierno,
//             sa.shiftno,
//             SM.CONTRACTNO,
//             SM.ORDERNO,
//             sm.custno,
//             sm.curcode,
//             sm.vat,
//             PA.PAYMENTTYPE,
//             PA.AMOUNT,
//             PA.Flag,
//             PA.PAYMENTNO,
//             pt.paymentflag,
//             PT.SUSPACCOUNT
//        FROM salestxn pa
//             LEFT JOIN salesaction sa
//                ON sa.actionid = pa.actionid
//             LEFT JOIN sales sm
//                ON sm.salesno = pa.salesno
//             LEFT JOIN papaytype pt
//                ON pt.typeid = PA.PAYMENTTYPE
//       WHERE SA.TRANDATE = pDate;
//BEGIN
//   vSeqNo := 1;

//   -- Борлуулалтын гүйлгээний мэдээллийг унших
//   FOR returnrow IN curSales
//   LOOP
//      vSalesNo := returnrow.SalesNo;
//      vTxnDate := returnrow.TranDate;
//      vPostDate := returnrow.PostDate;
//      vPosNo := returnrow.PosNo;
//      vCashierNo := returnrow.CashierNo;
//      vShiftNo := returnrow.ShiftNo;
//      vContractNo := returnrow.ContractNo;
//      vCustNo := returnrow.CustNo;
//      vCurCode := returnrow.CurCode;
//      vPaymentType := '';

//      IF returnrow.salestype = 0
//      THEN                         -- Хэрэв үндсэн борлуулалтын гүйлгээ байвал
//         -- Txn No=1. Борлуулалтын авлагын үндсэн гүйлгээ
//         vAccountNo := pGSalesAccountNo;
//         vEntryType :=0;                                            -- 0-Main, 1-General, 2-Payment
//         vDesc :=
//            'Үндсэн борлуулалтын авлагын гүйлгээ';
//         vTxnAmount := ABS (returnrow.SalesAmount);

//         IF (returnrow.flag = 'N')
//         THEN
//            vAccountEntry := 0;                                     -- 0-Debit
//         ELSE
//            vAccountEntry := 1;                                    -- 1-Credit
//         END IF;

//         IF    vSalesNo = 0
//            OR vCustNo = 0
//            OR vAccountNo IS NULL
//            OR vAccountNo = ''
//            OR vTxnAmount = 0
//            OR vTxnAmount IS NULL
//         THEN
//            RAISE sales_domination;
//         END IF;

//         INSERT INTO GLFILE (SALESNO,
//                             TXNDATE,
//                             POSTDATE,
//                             POSNO,
//                             CASHIERNO,
//                             SHIFTNO,
//                             CUSTNO,
//                             CONTRACTNO,
//                             SEQNO,
//                             ACCOUNTNO,
//                             ACCOUNTENTRY,
//                             ENTRYTYPE,
//                             TXNAMOUNT,
//                             CURCODE,
//                             DESCRIPTION,
//                             PAYMENTTYPE)
//              VALUES (vSalesNo,
//                      vTxnDate,
//                      vPostDate,
//                      vPosNo,
//                      vCashierNo,
//                      vShiftNo,
//                      vCustNo,
//                      vContractNo,
//                      vSeqNo,
//                      vAccountNo,
//                      vAccountEntry,
//                      vEntryType,
//                      vTxnAmount,
//                      vCurCode,
//                      vDesc,
//                      vPaymentType);
//        vSeqNo := vSeqNo+1;
  
//         -- Txn No=2. Борлуулалтын данс үндсэн гүйлгээний
//         vAccountNo := returnrow.SALESACCOUNTNO;
//         vEntryType :=1;                                            -- 0-Main, 1-General, 2-Payment
//         vDesc :=
//            'Бараа материалын борлуулалтын гүйлгээ';
//         vTxnAmount :=
//              ABS (returnrow.SalesAmount)
//            - (ABS (returnrow.SalesAmount) * returnrow.Vat / 100);

//         IF (returnrow.flag = 'N')
//         THEN
//            vAccountEntry := 1;                                    -- 1-Credit
//         ELSE
//            vAccountEntry := 0;                                     -- 0-Debit
//         END IF;

//         IF    vSalesNo = 0
//            OR vCustNo = 0
//            OR vAccountNo IS NULL
//            OR vAccountNo = ''
//            OR vTxnAmount = 0
//            OR vTxnAmount IS NULL
//         THEN
//            RAISE sales_domination;
//         END IF;

//         INSERT INTO GLFILE (SALESNO,
//                             TXNDATE,
//                             POSTDATE,
//                             POSNO,
//                             CASHIERNO,
//                             SHIFTNO,
//                             CUSTNO,
//                             CONTRACTNO,
//                             SEQNO,
//                             ACCOUNTNO,
//                             ACCOUNTENTRY,
//                             ENTRYTYPE,
//                             TXNAMOUNT,
//                             CURCODE,
//                             DESCRIPTION,
//                             PAYMENTTYPE)
//              VALUES (vSalesNo,
//                      vTxnDate,
//                      vPostDate,
//                      vPosNo,
//                      vCashierNo,
//                      vShiftNo,
//                      vCustNo,
//                      vContractNo,
//                      vSeqNo,
//                      vAccountNo,
//                      vAccountEntry,
//                      vEntryType,
//                      vTxnAmount,
//                      vCurCode,
//                      vDesc,
//                      vPaymentType);
//        vSeqNo := vSeqNo+1;
        
//         -- Txn No=3. Үндсэн борлуулалтын гүйлгээний VAT
//         IF (returnrow.Vat > 0)
//         THEN
//            vAccountNo := pTaxAccountNo;
//            vEntryType :=1;                                         -- 0-Main, 1-General, 2-Payment
//            vDesc :=
//               'Бараа материалын борлуулалтын VAT гүйлгээ';
//            vTxnAmount := ABS (returnrow.SalesAmount) * returnrow.Vat / 100;

//            IF (returnrow.flag = 'N')
//            THEN
//               vAccountEntry := 0;                                  -- 0-Debit
//            ELSE
//               vAccountEntry := 1;                                 -- 1-Credit
//            END IF;

//            IF    vSalesNo = 0
//               OR vCustNo = 0
//               OR vAccountNo IS NULL
//               OR vAccountNo = ''
//               OR vTxnAmount = 0
//               OR vTxnAmount IS NULL
//            THEN
//               RAISE sales_domination;
//            END IF;

//            INSERT INTO GLFILE (SALESNO,
//                                TXNDATE,
//                                POSTDATE,
//                                POSNO,
//                                CASHIERNO,
//                                SHIFTNO,
//                                CUSTNO,
//                                CONTRACTNO,
//                                SEQNO,
//                                ACCOUNTNO,
//                                ACCOUNTENTRY,
//                                ENTRYTYPE,
//                                TXNAMOUNT,
//                                CURCODE,
//                                DESCRIPTION,
//                                PAYMENTTYPE)
//                 VALUES (vSalesNo,
//                         vTxnDate,
//                         vPostDate,
//                         vPosNo,
//                         vCashierNo,
//                         vShiftNo,
//                         vCustNo,
//                         vContractNo,
//                         vSeqNo,
//                         vAccountNo,
//                         vAccountEntry,
//                         vEntryType,
//                         vTxnAmount,
//                         vCurCode,
//                         vDesc,
//                         vPaymentType);
//         END IF;
//        vSeqNo := vSeqNo+1;
        
//         -- Txn No=4. Хөнгөлөлтийн гүйлгээний
//         IF (returnrow.discountprod + returnrow.discountsales > 0)
//         THEN
//            vAccountNo := returnrow.DISCOUNTACCOUNTNO;
//            vEntryType := 1;                                         -- 0-Main, 1-General, 2-Payment
//            vDesc :=
//               'Бараа материалын борлуулалтын хөнгөлөлтийн гүйлгээ';
//            vTxnAmount :=
//                 returnrow.discountprod
//               + returnrow.discountsales
//               - (  ABS (returnrow.discountprod + returnrow.discountsales)
//                  * returnrow.Vat
//                  / 100);

//            IF (returnrow.flag = 'N')
//            THEN
//               vAccountEntry := 0;                                  -- 0-Debit
//            ELSE
//               vAccountEntry := 1;                                 -- 1-Credit
//            END IF;

//            IF    vSalesNo = 0
//               OR vCustNo = 0
//               OR vAccountNo IS NULL
//               OR vAccountNo = ''
//               OR vTxnAmount = 0
//               OR vTxnAmount IS NULL
//            THEN
//               RAISE sales_domination;
//            END IF;

//            INSERT INTO GLFILE (SALESNO,
//                                TXNDATE,
//                                POSTDATE,
//                                POSNO,
//                                CASHIERNO,
//                                SHIFTNO,
//                                CUSTNO,
//                                CONTRACTNO,
//                                SEQNO,
//                                ACCOUNTNO,
//                                ACCOUNTENTRY,
//                                ENTRYTYPE,
//                                TXNAMOUNT,
//                                CURCODE,
//                                DESCRIPTION,
//                                PAYMENTTYPE)
//                 VALUES (vSalesNo,
//                         vTxnDate,
//                         vPostDate,
//                         vPosNo,
//                         vCashierNo,
//                         vShiftNo,
//                         vCustNo,
//                         vContractNo,
//                         vSeqNo,
//                         vAccountNo,
//                         vAccountEntry,
//                         vEntryType,
//                         vTxnAmount,
//                         vCurCode,
//                         vDesc,
//                         vPaymentType);
//         END IF;
//        vSeqNo := vSeqNo+1;
        
//         -- Txn No=5. Хөнгөлөлтийн гүйлгээний VAT
//         IF (    returnrow.discountprod + returnrow.discountsales > 0
//             AND returnrow.Vat > 0)
//         THEN
//            vAccountNo := pTaxAccountNo;
//            vEntryType := 1;                                         -- 0-Main, 1-General, 2-Payment
//            vDesc :=
//               'Бараа материалын борлуулалтын хөнгөлөлтийн гүйлгээ VAT';
//            vTxnAmount :=
//               (  ABS (returnrow.discountprod + returnrow.discountsales)
//                * returnrow.Vat
//                / 100);

//            IF (returnrow.flag = 'N')
//            THEN
//               vAccountEntry := 1;                                 -- 1-Credit
//            ELSE
//               vAccountEntry := 0;                                  -- 0-Debit
//            END IF;

//            IF    vSalesNo = 0
//               OR vCustNo = 0
//               OR vAccountNo IS NULL
//               OR vAccountNo = ''
//               OR vTxnAmount = 0
//               OR vTxnAmount IS NULL
//            THEN
//               RAISE sales_domination;
//            END IF;

//            INSERT INTO GLFILE (SALESNO,
//                                TXNDATE,
//                                POSTDATE,
//                                POSNO,
//                                CASHIERNO,
//                                SHIFTNO,
//                                CUSTNO,
//                                CONTRACTNO,
//                                SEQNO,
//                                ACCOUNTNO,
//                                ACCOUNTENTRY,
//                                ENTRYTYPE,
//                                TXNAMOUNT,
//                                CURCODE,
//                                DESCRIPTION,
//                                PAYMENTTYPE)
//                 VALUES (vSalesNo,
//                         vTxnDate,
//                         vPostDate,
//                         vPosNo,
//                         vCashierNo,
//                         vShiftNo,
//                         vCustNo,
//                         vContractNo,
//                         vSeqNo,
//                         vAccountNo,
//                         vAccountEntry,
//                         vEntryType,
//                         vTxnAmount,
//                         vCurCode,
//                         vDesc,
//                         vPaymentType);
//                vSeqNo := vSeqNo+1;
//         END IF;
//      ELSE
//         IF returnrow.salestype = 1
//         THEN                       -- Хэрэв бонус борлуулалтын гүйлгээ байвал
//            -- Txn No=6. Урамшууллын гүйлгээ
//            vAccountNo := returnrow.BONUSACCOUNTNO;
//            vEntryType := 1;                                         -- 0-Main, 1-General, 2-Payment
//            vDesc := 'Урамшууллын гүйлгээ';
//            vTxnAmount := ABS (returnrow.SalesAmount);

//            IF (returnrow.flag = 'N')
//            THEN
//               vAccountEntry := 1;                                 -- 1-Credit
//            ELSE
//               vAccountEntry := 0;                                  -- 0-Debit
//            END IF;

//            IF    vSalesNo = 0
//               OR vCustNo = 0
//               OR vAccountNo IS NULL
//               OR vAccountNo = ''
//               OR vTxnAmount = 0
//               OR vTxnAmount IS NULL
//            THEN
//               RAISE sales_domination;
//            END IF;

//            INSERT INTO GLFILE (SALESNO,
//                                TXNDATE,
//                                POSTDATE,
//                                POSNO,
//                                CASHIERNO,
//                                SHIFTNO,
//                                CUSTNO,
//                                CONTRACTNO,
//                                SEQNO,
//                                ACCOUNTNO,
//                                ACCOUNTENTRY,
//                                ENTRYTYPE,
//                                TXNAMOUNT,
//                                CURCODE,
//                                DESCRIPTION,
//                                PAYMENTTYPE)
//                 VALUES (vSalesNo,
//                         vTxnDate,
//                         vPostDate,
//                         vPosNo,
//                         vCashierNo,
//                         vShiftNo,
//                         vCustNo,
//                         vContractNo,
//                         vSeqNo,
//                         vAccountNo,
//                         vAccountEntry,
//                         vEntryType,
//                         vTxnAmount,
//                         vCurCode,
//                         vDesc,
//                         vPaymentType);
//            vSeqNo := vSeqNo+1;
//            -- Txn No=7. Урамшууллын зардлын гүйлгээ
//            vAccountNo := returnrow.BONUSEXPACCOUNTNO;
//            vEntryType := 1;                                         -- 0-Main, 1-General, 2-Payment
//            vDesc := 'Урамшууллын зардлын гүйлгээ';
//            vTxnAmount := ABS (returnrow.SalesAmount);

//            IF (returnrow.flag = 'N')
//            THEN
//               vAccountEntry := 0;                                  -- 0-Debit
//            ELSE
//               vAccountEntry := 1;                                 -- 1-Credit
//            END IF;

//            IF    vSalesNo = 0
//               OR vCustNo = 0
//               OR vAccountNo IS NULL
//               OR vAccountNo = ''
//               OR vTxnAmount = 0
//               OR vTxnAmount IS NULL
//            THEN
//               RAISE sales_domination;
//            END IF;

//            INSERT INTO GLFILE (SALESNO,
//                                TXNDATE,
//                                POSTDATE,
//                                POSNO,
//                                CASHIERNO,
//                                SHIFTNO,
//                                CUSTNO,
//                                CONTRACTNO,
//                                SEQNO,
//                                ACCOUNTNO,
//                                ACCOUNTENTRY,
//                                ENTRYTYPE,
//                                TXNAMOUNT,
//                                CURCODE,
//                                DESCRIPTION,
//                                PAYMENTTYPE)
//                 VALUES (vSalesNo,
//                         vTxnDate,
//                         vPostDate,
//                         vPosNo,
//                         vCashierNo,
//                         vShiftNo,
//                         vCustNo,
//                         vContractNo,
//                         vSeqNo,
//                         vAccountNo,
//                         vAccountEntry,
//                         vEntryType,
//                         vTxnAmount,
//                         vCurCode,
//                         vDesc,
//                         vPaymentType);
//                vSeqNo := vSeqNo+1;                         
//         END IF;
//      END IF;
//   END LOOP;

//   -- Төлбөрийн гүйлгээний мэдээллийг унших
//   FOR returnrow1 IN curPayment
//   LOOP
//      vSalesNo := returnrow1.SalesNo;
//      vTxnDate := returnrow1.TranDate;
//      vPostDate := returnrow1.PostDate;
//      vPosNo := returnrow1.PosNo;
//      vCashierNo := returnrow1.CashierNo;
//      vShiftNo := returnrow1.ShiftNo;
//      vContractNo := returnrow1.ContractNo;
//      vCustNo := returnrow1.CustNo;
//      vCurCode := returnrow1.CurCode;


//      -- Txn No=1. Борлуулалтын авлагын үндсэн гүйлгээ
//      vPaymentType := returnrow1.PaymentType;
//      vAccountNo := returnrow1.SUSPACCOUNT;
//      vEntryType := 2;                                               -- 0-Main, 1-General, 2-Payment
//      vDesc := 'Төлбөрийн гүйлгээ';
//      vTxnAmount := ABS (returnrow1.Amount);
//      vAccountEntry := 0;                                           -- 0-Debit

//      IF    vSalesNo = 0
//         OR vCustNo = 0
//         OR vAccountNo IS NULL
//         OR vAccountNo = ''
//         OR vTxnAmount = 0
//         OR vTxnAmount IS NULL
//      THEN
//         RAISE sales_domination;
//      END IF;

//      INSERT INTO GLFILE (SALESNO,
//                          TXNDATE,
//                          POSTDATE,
//                          POSNO,
//                          CASHIERNO,
//                          SHIFTNO,
//                          CUSTNO,
//                          CONTRACTNO,
//                          SEQNO,
//                          ACCOUNTNO,
//                          ACCOUNTENTRY,
//                          ENTRYTYPE,
//                          TXNAMOUNT,
//                          CURCODE,
//                          DESCRIPTION,
//                          PAYMENTTYPE,
//                          PaymentFlag)
//           VALUES (vSalesNo,
//                   vTxnDate,
//                   vPostDate,
//                   vPosNo,
//                   vCashierNo,
//                   vShiftNo,
//                   vCustNo,
//                   vContractNo,
//                   vSeqNo,
//                   vAccountNo,
//                   vAccountEntry,
//                   vEntryType,
//                   vTxnAmount,
//                   vCurCode,
//                   vDesc,
//                   vPaymentType,
//                   returnrow1.PaymentFlag);
//        vSeqNo := vSeqNo+1;                   
//   END LOOP;

//   COMMIT;
//EXCEPTION
//   WHEN DUP_VAL_ON_INDEX 
//   THEN
//      raise_application_error (-20001,'You have tried to insert a duplicate supplier_id.');
//      ROLLBACK;      
//   WHEN sales_domination
//   THEN
//      raise_application_error (
//         -20222,
//         'Мэдээлэл дутуу буруу байна.' || 'SalesNo=' || vSalesNo);
//      ROLLBACK;
//   WHEN OTHERS
//   THEN
//      raise_application_error(-20001,'An error was encountered - '||SQLCODE||' -ERROR- '||SQLERRM);
//      ROLLBACK;
//END;
///

                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "PostToGLFile", SystemProp.TxnDate, SystemProp.SaleAccountNo, SystemProp.VatAccountNo);
                if (res.ResultNo != 0) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа" + res.ResultDesc; }

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result PostToAbacus(DbConnections db)
        {
            Result res = new Result();
            string sql = "";
            try
            {
                sql = @"
                                DECLARE 
                                BEGIN 
                                  PostToABACUSCustomer ( :1 );
                                END;
                ";
                #region [ PLSQL ]
//CREATE OR REPLACE PROCEDURE INFOPOSNEW.PostToABACUSCustomer (pDate IN DATE)
//IS
//   vCustNo                    NUMBER (16);
//   vCustName                  NVARCHAR2 (100);
//   vCustAddr                  NVARCHAR2 (100);

//   vTypeCode                  NUMBER (10);
//   vFirstName                 NVARCHAR2 (100);
//   vLastName                  NVARCHAR2 (100);
//   vMiddleName                NVARCHAR2 (100);
//   vCorporateName             NVARCHAR2 (200);
//   vCorporateName2            NVARCHAR2 (200);
//   vRegisterNo                NVARCHAR2 (50);
//   vTelephone                 NUMBER (20);
//   vFax                       NUMBER (20);
//   vMobile                    NUMBER (20);
//   vDirFirstName              NVARCHAR2 (100);
//   vDirLastName               NVARCHAR2 (100);
//   vCreateDate                DATE;
//   vEMail                     NVARCHAR2 (100);
//   vWebSite                   NVARCHAR2 (100);
//   vCityCode                  NUMBER (20);
//   vDistCode                  NUMBER (20);
//   vSubDistCode               NUMBER (20);
//   vApartment                 NVARCHAR2 (100);
//   vNote                      NVARCHAR2 (100);
//   vDDirName                  NVARCHAR2 (100);

//   vCustCount                 NUMBER (2);

//   ABACUSCustomer_exception   EXCEPTION;

//   -- Өнөөдөр нийт хэдэн харилцагч гүйлгээ хийсэнийг олж авах
//   CURSOR curTodayCustomers
//   IS
//        SELECT CustNo
//          FROM glfile
//         WHERE Txndate = pDate
//      GROUP BY CustNo;
//BEGIN

//   -- Борлуулалтын гүйлгээний мэдээллийг унших
//   FOR returnrow IN curTodayCustomers
//   LOOP
//      vCustNo := returnrow.CustNo;

//      -- Харилцагчийн insert хийх мэдээллийг олж уншиж авах
//      SELECT C.ClassCode,
//             C.FirstName,
//             C.LastName,
//             C.MiddleName,
//             C.CorporateName,
//             C.CorporateName2,
//             C.RegisterNo,
//             C.Telephone,
//             C.Fax,
//             C.Mobile,
//             C.DirFirstName,
//             C.DirLastName,
//             C.CreateDate,
//             C.EMail,
//             C.WebSite,
//             A.CityCode,
//             A.DistCode,
//             A.SubDistCode,
//             A.Apartment,
//             A.Note
//        INTO vTypeCode,
//             vFirstName,
//             vLastName,
//             vMiddleName,
//             vCorporateName,
//             vCorporateName2,
//             vRegisterNo,
//             vTelephone,
//             vFax,
//             vMobile,
//             vDirFirstName,
//             vDirLastName,
//             vCreateDate,
//             vEMail,
//             vWebSite,
//             vCityCode,
//             vDistCode,
//             vSubDistCode,
//             vApartment,
//             vNote
//        FROM    customer C
//             LEFT JOIN
//                CUSTOMERADDR A
//             ON A.CUSTOMERNO = C.CUSTOMERNO AND A.addrcurrent = 1
//       WHERE C.CustomerNo = vCustNo;

//      -- Тухайн харилцагч бааз дээр байгаа эсэхийг шалгах
//      vCustCount := 0;

//      SELECT COUNT (*)
//        INTO vCustCount
//        FROM dbo.tblcustomers@msql
//       WHERE "SrbsMembershipId" = vCustNo;

//      IF (vCustCount = 0)
//      THEN
//         IF (vTypeCode = 0)
//         THEN
//            vCustName := vFirstName || vMiddleName || vLastName;
//         ELSE
//            vCustName := vCorporateName || vCorporateName2;
//         END IF;

//         vCustAddr :=
//            vCityCode || vDistCode || vSubDistCode || vApartment || vNote;
//         vDDirName := vDirFirstName || vDirLastName;

//         -- Харилцагчийг insert хийх процесс
//         INSERT INTO dbo.tblcustomers@msql ("SrbsMembershipId",
//                                            "Cust#",
//                                            "CustomerName",
//                                            "MailingAddress",
//                                            "MailingAddress2",
//                                            "MailingCity",
//                                            "MailingState",
//                                            "MailingCountry",
//                                            "MailingZip",
//                                            "ShipToAddress",
//                                            "ShipToAddress2",
//                                            "ShipToCity",
//                                            "ShipToState",
//                                            "ShipToCountry",
//                                            "ShipToZip",
//                                            "Phone#",
//                                            "Fax#",
//                                            "OtherPhone",
//                                            "Contact",
//                                            --"DateEntered",
//                                            "EMail",
//                                            "Internet",
//                                            "DefaultGLSales#",
//                                            "TermsID",
//                                            --"Delete_",
//                                            --"PO",
//                                            --"YTDsales",
//                                            --"LastSale",
//                                            --"LastPayment",
//                                            --"LastPayAmt",
//                                            --"LastSalesAmt",
//                                            --"CreditLimit",
//                                            --"CreditHigh",
//                                            --"CurBal",
//                                            "Active",
//                                            "Status",
//                                            "SalesmanID",
//                                            "TaxExempt#",
//                                            "CC#",
//                                            --"SalesCommission",
//                                            "InternationalID",
//                                            "CountryID",
//                                            "StateID",
//                                            "CountyID",
//                                            "CityID",
//                                            "OtherID",
//                                            "FinanceChargeID",
//                                            --"upsize_ts",
//                                            "AcquisitionID",
//                                            "CurrencyID",
//                                            "OnlineID",
//                                            "Latitude",
//                                            "Longitude")
//              VALUES (vCustNo,                             --SrbsMembershipId,
//                      vCustNo,                                        --Cust#,
//                      vCustName,                               --CustomerName,
//                      vCustAddr,                             --MailingAddress,
//                      vRegisterNo,                          --MailingAddress2,
//                      vCityCode,                                --MailingCity,
//                      vCityCode,                               --MailingState,
//                      vCityCode,                             --MailingCountry,
//                      0,
//                      vCustAddr,                              --ShipToAddress,
//                      vRegisterNo,                           --ShipToAddress2,
//                      vCityCode,                                 --ShipToCity,
//                      vCityCode,                                --ShipToState,
//                      vCityCode,                              --ShipToCountry,
//                      0,
//                      vTelephone,                                    --Phone#,
//                      vFax,                                            --Fax#,
//                      vMobile,                                   --OtherPhone,
//                      vDDirName,                                    --Contact,
//                      --vCreateDate,    --DateEntered,
//                      vEmail,                                         --EMail,
//                      vWebsite,                                    --Internet,
//                      4,
//                      0,
//                      --false,    --"Delete_",
//                      --false,    --"PO",
//                      --0,        --"YTDsales",
//                      --0,         --"LastSale",
//                      --0,        --"LastPayment",
//                      --0,    --"LastPayAmt"
//                      --0,    --"LastSalesAmt"
//                      --0,    --"CreditLimit",
//                      --0,    --"CreditHigh",
//                      --0,    --"CurBal",
//                      1,                                               --true,
//                      2,
//                      0,
//                      NULL,
//                      NULL,
//                      --0,    --"SalesCommission",
//                      0,
//                      0,
//                      0,
//                      0,
//                      0,
//                      0,
//                      0,
//                      --0,    --"upsize_ts",
//                      0,
//                      0,
//                      0,
//                      0,
//                      0);
//      END IF;
//   END LOOP;

//   COMMIT;
//EXCEPTION
//   WHEN DUP_VAL_ON_INDEX
//   THEN
//      raise_application_error (
//         -20001,
//         'You have tried to insert a duplicate supplier_id.');
//      ROLLBACK;
//   WHEN ABACUSCustomer_exception
//   THEN
//      raise_application_error (
//         -20222,
//         'Мэдээлэл дутуу буруу байна.');
//      ROLLBACK;
//   WHEN OTHERS
//   THEN
//      raise_application_error (
//         -20001,
//         'An error was encountered - ' || SQLCODE || ' -ERROR- ' || SQLERRM);
//      ROLLBACK;
//END;
///

                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "PostToAbacusCustomer", SystemProp.TxnDate);
                if (res.ResultNo != 0) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа" + res.ResultDesc; }

                sql = @"
                                DECLARE 
                                BEGIN 
                                  PostToABACUSInvoice ( :1 );
                                END;
                ";
                #region [PLSQL]
//CREATE OR REPLACE PROCEDURE INFOPOSNEW.PostToABACUSInvoice (
//   pDate              IN DATE)
//IS
//    vSeqNo               NUMBER (10);
//    vGetNextARID         NUMBER(10);
//    vGetNextBatch         NUMBER(10);
//    vGetJournalIDHist         NUMBER(10);
//    vGetCheckID         NUMBER(10);
//    vARAccountNo        VARCHAR2(20);
//    vTxnAmount          NUMBER(19,2);

//    vReturn_value int;
//    return_value int ; 
    
//    ABACUSInvoice_exception     EXCEPTION;
 
//    -- Өнөөдөр нийт хийгдсэн борлуулалтын гүйлгээнүүдийг унших 
//   CURSOR curTodaySales
//   IS
//    select SALESNO,TXNDATE, POSTDATE,POSNO,CASHIERNO,SHIFTNO,CUSTNO,CONTRACTNO,SEQNO,ACCOUNTNO,ACCOUNTENTRY,ENTRYTYPE,TXNAMOUNT,CURCODE,DESCRIPTION,PAYMENTTYPE, P.PaymentFlag
//    from GLFile G
//    left join papaytype P on G.paymenttype=P.typeid
//    where Txndate=pDate
//    order by SalesNo, EntryType;

//BEGIN
//   vSeqNo := 1;

//   -- Борлуулалтын нийт гүйлгээний мэдээллийг унших
//   FOR returnrow IN curTodaySales
//   LOOP
//           --------------------------------------------------------
//        --  АВЛАГЫН ГҮЙЛГЭЭНИЙ ҮНДСЭН БИЧИЛТИЙГ ҮҮСГЭХ
//        --------------------------------------------------------        

//        if (returnrow.ENTRYTYPE=0) then
//            -- invoice
           
//            --EXEC 
//            --@return_value = [dbo].[sp_GetNextBatch]
//            --@TheUser = N'Srgs_admin' 
//            --SELECT
//            --'Return Value' = @return_value;            

//            vGetNextBatch:=1;--return_value;    
            
//            --EXEC 
//            --@return_value = [dbo].[sp_GetNextARID]
//            --@AMOUNT  = 1 
//            --SELECT
//            --'Return Value' = @return_value;            
            
//            vGetNextARID:=1;--return_value;
        
//            vARAccountNo:=returnrow.AccountNo;
        
//            insert into dbo.tblARInvoices@msql (
//            "ARInvoiceID",
//            "OrderID",
//            "SrbsPaymentId",
//            "Invoice#",
//            "Batch#",    
//            "Ref#",
//            "Date",
//            "ARInvoiceDate",
//            "DebitGLID",
//            "Amount",
//            "ARBalance", 
//            "CustomerID",
//            "Author",
//            "PO#",
//            --"Processed",
//            --"ToApply",
//            --"AdjustmentAmount",
//            --"GLUpdateDate",
//            --"Job#",
//            "JobID",
//            "SalesmanID",
//            "DocType",
//            --"DueDate",
//            --"FCDate",
//            --"DiscountDate",
//            --"DateClosed",
//            --"upsize_ts",
//            --"CurrencyID",
//            --"CurrentRate",            
//            --"CurrentMethod",
//            --"Process#",
//            --"ProcessID",
//            "ActivityID"
//            )
//            values (
//            vGetNextARID,            --ARInvoiceID    [dbo].[sp_GetNextARID] 
//            0,                        --OrderID
//            returnrow.SalesNo,        --SrbsPaymentId
//            vGetNextARID,            --Invoice#        [dbo].[sp_GetNextARID] 
//            vGetNextBatch,            --Batch#        [dbo].[sp_GetNextBatch]
//            returnrow.SalesNo,        --Ref#
//            returnrow.TxnDate,        --Date
//            returnrow.TxnDate,        --ARInvoiceDate
//            returnrow.AccountNo,    --DebitGLID
//            returnrow.TxnAmount,    --Amount 
//            returnrow.TxnAmount,    --ARBalance 
//            returnrow.CustNo,        --CustomerID
//            returnrow.CashierNo,    --Author
//            returnrow.SalesNo,        --PO#
//            --true,        --"Processed",
//            --0,         --"ToApply",            
//            --0,         --"AdjustmentAmount",
//            --null,        --"GLUpdateDate",
//            --null,        --"Job#",
//            0,
//            returnrow.CashierNo,
//            1,            --"DocType",
//            --null,        --"DueDate",
//            --null,    --"FCDate",
//            --null,    --"DiscountDate",
//            --null,    --"DateClosed",
//            --0,    --"upsize_ts",
//            --0,    --"CurrencyID",
//            --0,    --"CurrentRate",
//            --0,    --"CurrentMethod",
//            --null,    --"Process#",    
//            --0,    --"ProcessID",
//            0
//            );
//        end if ;
//        --------------------------------------------------------
//        --  АВЛАГЫН ГҮЙЛГЭЭНИЙ ДЭЛГЭРЭНГҮЙ БИЧИЛТИЙГ ҮҮСГЭХ
//        --------------------------------------------------------        
//        if (returnrow.ENTRYTYPE=1) then
//            -- invoice detail
//            if (returnrow.AccountENTRY=1) then
//                vTxnAmount := returnrow.TxnAmount;
//            else
//                vTxnAmount := -1*returnrow.TxnAmount;
//            end if;
            
//            insert into dbo.tblARInvoiceDetail@msql (
//            "ARInvoiceID",
//            "ARInvoiceDetailID",
//            "SrbsPaymentDetailId",
//            "CreditGLID",
//            "UnitPrice",
//            "Quantity",
//            "SplitAmount",
//            "Description",
//            --"InternationalID",
//            --"InternationalAmount",
//            --"CountryID",
//            --"CountryAmount",
//            --"StateID",
//            --"StateAmount",
//            --"CountyID",
//            --"CountyAmount",
//            --"CityID",
//            --"CityAmount",
//            --"OtherID",
//            --"OtherAmount",
//            "Type",
//            --"Discount",
//            --"OrderID",
//            --"OrderDetailID",
//            --"MaterialID",
//            "ActivityID"
//            )
//            values
//            (
//            vGetNextARID,                                        --ARInvoiceID,
//            vGetNextARID + returnrow.seqNo,                        --ARInvoiceDetailID,
//            returnrow.seqNo,        --SrbsPaymentDetailId,
//            returnrow.AccountNo,    --CreditGLID
//            vTxnAmount ,            --UnitPrice 
//            1,                        --Quantity
//            vTxnAmount,                --SplitAmount ,
//            returnrow.DESCRIPTION,    --Description,
//            --0,     --"InternationalID",
//            --0,     --"InternationalAmount",
//            --0,     --"CountryID",
//            --0,     --"CountryAmount",
//            --0,     --"StateID",
//            --0,     --"StateAmount",
//            --0,     --"CountyID",
//            --0,     --"CountyAmount",
//            --0,     --"CityID",
//            --0,     --"CityAmount",
//            --0,     --"OtherID",
//            --0,     --"OtherAmount",
//            14,
//            --0,     --"Discount",
//            --0,     --"OrderID",
//            --0,     --"OrderDetailID",
//            --0,     --"MaterialID",
//            0     --"ActivityID"
//            );
//        end if;
//        --------------------------------------------------------
//        --  ТӨЛБӨРИЙН ГҮЙЛГЭЭНИЙ БИЧИЛТИЙГ ҮҮСГЭХ
//        --------------------------------------------------------
//        if (returnrow.ENTRYTYPE=2 and returnrow.PaymentFlag=0) then
//            -- payment
//            vSeqNo:=vSeqNo+1;
//            vGetJournalIDHist:=1;        --[dbo].[sp_GetJournalIDHist]
//            vGetCheckID:=1;                --[dbo].[sp_GetCheckID]
    
//            insert into dbo.tblARPayments@msql (
//            "Date",
//            "ARPaymentDate",
//            "InvoiceID",
//            "Batch#",
//            "PaymentType",
//            "Source",
//            "Check#",
//            "Ref#",
//            "GLID",
//            "Debit",
//            "Credit",
//            "GLUpdateDate",
//            "CustomerID",
//            "GLTransactionID",            
//            "CheckID",
//            "ActivityID"
//            )
//            values(
//            returnrow.TxnDate,                --Date,
//            returnrow.TxnDate,                --ARPaymentDate,
//            vGetNextARID,                     --InvoiceID,    [dbo].[sp_GetNextARID] 
//            vGetNextBatch,                    --Batch#,        [dbo].[sp_GetNextBatch]
//            1,                                --PaymentType,    КАСС
//            'A/R Payments',                    --Source,
//            returnrow.SalesNo,                --Check#,
//            returnrow.SalesNo,                --Ref#,
//            returnrow.AccountNo,                --GLID,
//            0,                                --Debit ,
//            returnrow.TxnAmount,            --Credit ,
//            returnrow.TxnDate,                --GLUpdateDate,
//            returnrow.CustNo,                --CustomerID,
//            vSeqNo,                                --GLTransactionID
//            vGetCheckID,                        --CheckID,
//            0                                 -- ActivityID        ХАТУУ КОД БАЙНА        
//            );           

//            vSeqNo:=vSeqNo+1;
//            insert into dbo.tblARPayments@msql (
//            "Date",
//            "ARPaymentDate",
//            "InvoiceID",
//            "Batch#",
//            "PaymentType",
//            "Source",
//            "Check#",
//            "Ref#",
//            "GLID",
//            "Debit",
//            "Credit",
//            "GLUpdateDate",
//            "CustomerID",
//            "GLTransactionID",
//            "CheckID",
//            "ActivityID"
//            )
//            values(
//            returnrow.TxnDate,                --Date,
//            returnrow.TxnDate,                --ARPaymentDate,
//            vGetNextARID,                     --InvoiceID,    [dbo].[sp_GetNextARID] 
//            vGetNextBatch,                    --Batch#,        [dbo].[sp_GetNextBatch]
//            1,                                --PaymentType,    КАСС
//            'A/R Payments',                    --Source,
//            returnrow.SalesNo,                --Check#,
//            returnrow.SalesNo,                --Ref#,
//            vARAccountNo,                      --GLID,
//            returnrow.TxnAmount,              --Debit ,
//            0,                                  --Credit ,
//            returnrow.TxnDate,                --GLUpdateDate,
//            returnrow.CustNo,                --CustomerID,
//            vSeqNo,                            --GLTransactionID
//            vGetCheckID,                                --CheckID,
//            0                                 -- ActivityID        ХАТУУ КОД БАЙНА        
//            );                  
            
//            --------------------------------------------------------
//            --  ТӨЛБӨРИЙН ГҮЙЛГЭЭНИЙ HISTORY БИЧИЛТИЙГ ҮҮСГЭХ
//            --------------------------------------------------------
             
//           insert into dbo.tblARPayments_history@msql (
//            "Date",
//            "ARPaymentDate",
//            "Batch",
//            "CustomerID",
//            "Check#",
//            "DebitGL",
//            "Amount",
//            --"Reconciled",
//            "PayHistoryID",
//            "CheckID",
//            --"Closed",
//            --"Approved",
//            --"ApprovalNeeded",
//            "ActivityID"
//            )
//            values(
//            returnrow.TxnDate,                --Date,
//            returnrow.TxnDate,                --ARPaymentDate,
//            vGetNextBatch,                    --Batch#,        [dbo].[sp_GetNextBatch]
//            returnrow.CustNo,                    --CustomerID,
//            returnrow.SalesNo,                --Check#,
//            returnrow.AccountNo,                --DebitGL,
//            returnrow.TxnAmount,                --Amount ,
//            --1,            --"Reconciled",
//            vGetJournalIDHist,                --"PayHistoryID",
//            vGetCheckID,                --"CheckID",
//            --0,            --"Closed",
//            --1,         --"Approved",
//            --0,        --"ApprovalNeeded",
//            0                               -- ActivityID        ХАТУУ КОД БАЙНА        
//            );                
//        end if;
//    END LOOP;
//    COMMIT;
//EXCEPTION
//   WHEN ABACUSInvoice_exception
//   THEN
//      raise_application_error (
//         -20222,         'Мэдээлэл дутуу буруу байна.');
//      ROLLBACK;
//   WHEN OTHERS
//   THEN
//      raise_application_error (-20002,
//                               'An error has occurred inserting a supplier.');
//      ROLLBACK;
//END;
///

                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "PostToAbacusInvoice", SystemProp.TxnDate);
                if (res.ResultNo != 0) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа" + res.ResultDesc; }

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        #endregion
        private Result SaveRate(DbConnections db)
        {
            Result res = new Result();
            try
            {
                //insert into CurrencyHist
                //select Currency, :1, SaveRate,CashBuyRate,CashSellRate,NonCashBuyRate,NonCashSellRate, oldrate from Currency
                res = ProcessDB.DB2131103(db, SystemProp.TxnDate);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result SaveDailyRate(DbConnections db)
        {
            Result res = new Result();
            try
            {
                // DB213010 - Тухайн өдрийн ханшийг хуучин ханш руу хадгалах
                //Update Currency set OldRate = Rate

                res = ProcessDB.DB2131104(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        //--- AFTER EOD
        private Result SetStatusInvSerials(DbConnections db)
        {
            Result res = new Result();
            try
            {
                //Барааны төлөвийг Unavailable болгох
                //Update invseries set Status=0 where  (Status = 1 or Status = 2) and InvID in (select invID from invmain where RentFlag=2)
                res = ProcessDB.DB2131105(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result CleanCustomerSales(DbConnections db)
        {
            Result res = new Result();
            try
            {
                //Харицагч бүр дээр борлуулалтын дугаарыг цэвэрлэх
                //truncate table CUSTOMERIDDEVICE;
                res = ProcessDB.DB2131106(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result CleanJournal(DbConnections db)
        {
            Result res = new Result();
            try
            {
                //Хугацаа дууссан гэрээг архивлах
                //insert into sales_Hist select * from sales ;
                //insert into salesprod_Hist select * from salesprod ;
                //insert into salesrent_Hist select * from salesrent ;
                //insert into salestxn_Hist select * from salestxn ;
                //insert into salesaction_Hist select * from salesaction ;
                //delete from sales;
                //delete from salesprod;
                //delete from salesrent;
                //delete from salestxn;
                //delete from salesaction;

                res = ProcessDB.DB2131107(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result PledgeMoveHist(DbConnections db)
        {
            Result res = new Result();
            try
            {
                // Барьцаа хөрөнгийн мэдээллийн table-уудыг цэвэрлэх, түүх рүү хадгалах (Pledge)
                
                //insert to pledgemain_Hist select * from pledgemain where status=1;
                //insert to pledgedoc_Hist select * from pledgedoc where status=1;

                //delete from pledgemain where status=1;
                //delete from pledgedoc where status=1;

                res = ProcessDB.DB2131108(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private Result ContractMoveHist(DbConnections db)
        {
            Result res = new Result();
            try
            {
                // Хугацаа дууссан гэрээг архивлах

                //insert to ContractMain _Hist select * from ContractMain  where ValidEndDate<=sysdate
                //insert to ContractProd_Hist select P.* from ContractProd P 
                //  left join ContractMain M on M.ContractNo=P.ContractNo
                //  where M.ValidEndDate<=sysdate
                //insert to ContractAcnt _Hist select A.* from ContractAcnt A 
                //  left join ContractMain M on M.ContractNo=A.ContractNo
                //  where M.ValidEndDate<=sysdate
                //insert to DepSchedule _Hist select A.* from DepSchedule A 
                //  left join ContractMain M on M.ContractNo=A.ContractNo
                //  where M.ValidEndDate<=sysdate


                //Delete from ContractProd 
                //where contractno in (select contractno from ContractMain where ValidEndDate<=sysdate);
                //Delete from ContractAcnt  
                //where contractno in (select contractno from ContractMain where ValidEndDate<=sysdate);
                //Delete from DepSchedule  
                //where contractno in (select contractno from ContractMain where ValidEndDate<=sysdate);
                //delete from ContractMain  where ValidEndDate<=sysdate;

                res = ProcessDB.DB2131109(db);  
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        #endregion
        #endregion
    }
}

#region [old]
//private Result AutoStatement(DbConnections db)
//{
//    Result res = new Result();
//    try
//    {
//        //15.	Автоматаар хуулга илгээх
//        return res;
//    }
//    catch (Exception ex)
//    {
//        res.ResultNo = 9110002;
//        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
//        ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
//        return res;
//    }
//    return res;
//}
//public Result ContractBalanceUse(DbConnections db, string ContractNo, decimal TxnAmount)
//{
//    Result res = new Result();
//    try
//    {
//        //DB213029 Гэрээний үлдэгдэл хасагдуулах
//        res = ProcessDB.DB213029(db, ContractNo, TxnAmount);
//        if (res.ResultNo != 0) return res;
//        if (res.AffectedRows > 0)
//        {
//            res.ResultNo = 0;
//            res.ResultDesc = "Амжилттай";
//        }
//        else
//        {
//            res.ResultNo = 1;
//            res.ResultDesc = "Гэрээний үлдэгдэл хүрэхгүй байна";
//        }
//        return res;
//    }
//    catch (Exception ex)
//    {
//        res.ResultNo = 9110002;
//        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
//        ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
//        return res;
//    }
//    return res;

//}
//private Result MemberAmortization(DbConnections db)
//{
//    Result res = new Result();
//    try
//    {
//        //BalanceUpdate хийхдээ давхар valudation хийнэ.
//        //Select ContractNo, Amount from DEPSCHEDULE where Day = sysdate;

//        //Дараах байдлаар гүйлгээний бичилт үүснэ.
//        //Хэрэв A.Vat=1
//        //    Debit GENERALPARAM. SALEACCONTNO	Balance+Balance%GeneralParam.Vat	CurCode
//        //    Credit IncomeAccountNo		Balance				CurCode
//        //    Credit VATAccountNo			Balance%GeneralParam.Vat		CurCode
//        //Үгүй бол
//        //Debit GENERALPARAM. SALEACCONTNO	Balance				CurCode
//        //    Credit IncomeAccountNo		Balance				CurCode

//        return res;
//    }
//    catch (Exception ex)
//    {
//        res.ResultNo = 9110002;
//        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
//        ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
//        return res;
//    }
//    return res;
//}
//private Result MemeberClosing(DbConnections db)
//{
//    Result res = new Result();
//    try
//    {
//        //select C.ContractNo, T.METHOD, C.status, C.amount, C.balance, C.balancetype, C.accountno, C.incomeaccountno 
//        //from contractmain C
//        //left join PACONTRACTTYPE T on C.CONTRACTTYPE=T.PACONTRACTTYPE

//        //BalanceUpdate хийхдээ давхар valudation хийнэ.
//        //Select M.ContractNo, A.AccountNo, A.IncomeAcountNo, M.Amount, M.Balance, A.CurCode, A.VAT
//        //from CONTRACTMAIN M
//        //left join CONTRACTACNT A on A.ContractNo=M.ContractNo
//        //where M.VALIDENDDATE< sysdate and Status=1  and M.Balance>0;

//        //Дараах байдлаар гүйлгээний бичилт үүснэ.
//        //Хэрэв A.Vat=1
//        //    Debit GENERALPARAM. SALEACCONTNO	Balance+Balance%GeneralParam.Vat	CurCode
//        //    Credit IncomeAccountNo			Balance					CurCode
//        //    Credit VATAccountNo			Balance%GeneralParam.Vat		CurCode
//        //Үгүй бол
//        //Debit GENERALPARAM. SALEACCONTNO	Balance					CurCode
//        //    Credit IncomeAccountNo			Balance					CurCode

//        //Update CONTRACTACNT set Balance=0, Status=0 where ContractNo=:1

//        return res;
//    }
//    catch (Exception ex)
//    {
//        res.ResultNo = 9110002;
//        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
//        ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
//        return res;
//    }
//    return res;
//}
//private Result FinPosting(DbConnections db)
//{
//    Result res = new Result();
//    GLFILE glfile = new GLFILE();
//    string salesno = "";
//    string contractNo = "";
//    decimal totalsale = 0;
//    int sequence = 0;

//    DataTable payment = new DataTable();
//    try
//    {

//        #region [ Payment records write ]
//        if (SystemProp.PaymentTran == 1)
//        {
//            res = ProcessDB.DB213027(db);           //Тухайн өдрийн төлбөрийн жагсаалтыг авах
//            if (res.ResultNo != 0) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа" + res.ResultDesc; }
//            if (res.Data == null) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа"; }
//            if (res.Data.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа"; }

//            payment = res.Data.Tables[0];
//        }
//        #endregion
//        #region [ Sales records write ]
//        res = ProcessDB.DB213026(db);           //Тухайн өдрийн борлуулалтын жагсаалтыг авах
//        if (res.ResultNo != 0) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа" + res.ResultDesc; }
//        if (res.Data == null) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа"; }
//        if (res.Data.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа"; }

//        DataTable sales = res.Data.Tables[0];

//        con = db.BeginTransaction("core", "Start DBChange");
//        if (con == null) return new Result(9110027, "Гүйлгээ хийж байхад алдаа гарлаа");

//        foreach (DataRow row in sales.Rows)
//        {
//            if (salesno != Static.ToStr(row["SalesNo"]))
//            {
//                if (salesno != "")
//                {
//                    glfile.ClearTran();
//                    glfile.SeqNo = sequence++;
//                    glfile.AccountNo = SystemProp.SaleAccountNo;// Ерөнхий борлууллатын авлагын данс
//                    glfile.AccountEntry = 0;// DEBIT
//                    glfile.TxnAmount = totalsale;
//                    glfile.Desc = "Борлууллатын авлагын гүйлгээ";
//                    glfile.ContractNo = contractNo;
//                    glfile.EntryType = 1;   // Main
//                    res = ProcessDB.DB213028(db, glfile.Get());
//                    totalsale = 0;
//                    contractNo = "";

//                    #region [ Generate payment entries ]
//                    if (SystemProp.PaymentTran == 1)
//                    {
//                        foreach (DataRow paymentrow in payment.Select("SalesNo=" + salesno))
//                        {
//                            // Ерөнхий борлуулалтын авлагаас тухайн авлагын данс руу хийх гүйлгээ
//                            glfile.ClearTran();
//                            glfile.SeqNo = sequence++;
//                            glfile.AccountNo = SystemProp.SaleAccountNo;
//                            glfile.AccountEntry = 1;// CREDIT
//                            glfile.TxnAmount = Static.ToDecimal(paymentrow["AMOUNT"]);
//                            glfile.Desc = "Борлууллатын авлагын гүйлгээ";
//                            glfile.PaymentType = Static.ToStr(paymentrow["PaymentType"]);
//                            glfile.EntryType = 2;
//                            res = ProcessDB.DB213028(db, glfile.Get());

//                            // Тухайн төлбөрийн төрөлийн авлага руу хийх гүйлгээ
//                            glfile.ClearTran();
//                            glfile.SeqNo = sequence++;
//                            glfile.AccountNo = Static.ToStr(paymentrow["ACCOUNTNO"]);
//                            glfile.AccountEntry = 0;// CREDIT
//                            glfile.TxnAmount = Static.ToDecimal(paymentrow["AMOUNT"]);
//                            glfile.Desc = "Төлбөрийн төрөлийн авлагын гүйлгээ";
//                            glfile.PaymentType = Static.ToStr(paymentrow["PaymentType"]);
//                            glfile.EntryType = 2;
//                            res = ProcessDB.DB213028(db, glfile.Get());
//                        }
//                    }
//                    #endregion
//                }

//                glfile.Clear();
//                glfile.BatchNo = Static.ToStr(row["BatchNo"]);
//                glfile.SalesNo = Static.ToStr(row["SalesNo"]);
//                glfile.EntryType = 0;   // Normal
//                glfile.CurCode = "MNT";
//                glfile.TxnDate = SystemProp.TxnDate;
//                glfile.PostDate = DateTime.Now;
//                glfile.CustNo = Static.ToStr(row["CustNo"]);
//                contractNo = Static.ToStr(row["ContractNo"]);

//                salesno = Static.ToStr(row["SalesNo"]);
//            }

//            if (Static.ToInt(row["SaleType"]) == 0)
//            {
//                // Бараа үйлчилгээний борлуулалтын орлогын гүйлгээний бичилт үүсгэх
//                glfile.SeqNo = sequence++;
//                glfile.AccountNo = Static.ToStr(row["SALESACCOUNTNO"]); // Бараа бүтээгдэхүүний борлуулалтын орлогын данс
//                glfile.AccountEntry = 1;// CREDIT
//                glfile.TxnAmount = Static.ToDecimal(row["Price"]);
//                glfile.Desc = "Бараа үйлчилгээний борлуулалтын орлогын гүйлгээ (" + Static.ToStr(row["NAME"]) + ")";
//                res = ProcessDB.DB213028(db, glfile.Get());
//                totalsale = totalsale + glfile.TxnAmount;
//                if (Static.ToInt(row["VAT"]) == 1)
//                {
//                    // Бараа үйлчилгээний борлуулалтын орлогын VAT гүйлгээний бичилт үүсгэх
//                    glfile.ClearTran();
//                    glfile.SeqNo = sequence++;
//                    glfile.AccountNo = SystemProp.VatAccountNo;
//                    glfile.AccountEntry = 1;// CREDIT
//                    glfile.TxnAmount = Static.ToDecimal(row["Price"]) * SystemProp.VAT / 100;
//                    glfile.Desc = "Бараа үйлчилгээний борлуулалтын орлогын НӨАТ гүйлгээ (" + Static.ToStr(row["NAME"]) + ")";
//                    res = ProcessDB.DB213028(db, glfile.Get());
//                    totalsale = totalsale + glfile.TxnAmount;
//                }

//                // Борлуулалтын хөнгөлөлтийн гүйлгээний бичилт үүсгэх
//                if (Static.ToDecimal(row["Price"]) != Static.ToDecimal(row["Discount"]))
//                {
//                    glfile.ClearTran();
//                    glfile.SeqNo = sequence++;
//                    glfile.AccountNo = Static.ToStr(row["DISCOUNTACCOUNTNO"]);// Бараа бүтээгдэхүүний борлуулалтын хөнгөлөлтийн данс
//                    glfile.AccountEntry = 0;// DEBIT
//                    glfile.TxnAmount = Static.ToDecimal(row["Price"]) - Static.ToDecimal(row["Discount"]);
//                    glfile.Desc = "Бараа үйлчилгээний борлуулалтын хөнгөлөлтийн гүйлгээ (" + Static.ToStr(row["NAME"]) + ")";
//                    res = ProcessDB.DB213028(db, glfile.Get());
//                    totalsale = totalsale - glfile.TxnAmount;

//                    // Борлуулалтын хөнгөлөлтийн VAT гүйлгээний бичилт үүсгэх
//                    if (Static.ToInt(row["VAT"]) == 1)
//                    {
//                        glfile.ClearTran();
//                        glfile.SeqNo = sequence++;
//                        glfile.AccountNo = SystemProp.VatAccountNo;
//                        glfile.AccountEntry = 0;// DEBIT
//                        glfile.TxnAmount = (Static.ToDecimal(row["Price"]) - Static.ToDecimal(row["Discount"])) * SystemProp.VAT / 100;
//                        glfile.Desc = "Бараа үйлчилгээний борлуулалтын хөнгөлөлтийн НӨАТ гүйлгээ (" + Static.ToStr(row["NAME"]) + ")";
//                        res = ProcessDB.DB213028(db, glfile.Get());
//                        totalsale = totalsale - glfile.TxnAmount;
//                    }
//                }
//            }
//            else
//            {
//                // Борлуулалтын бонусын урамшууллын гүйлгээний бичилт үүсгэх
//                glfile.ClearTran();
//                glfile.SeqNo = sequence++;
//                glfile.AccountNo = Static.ToStr(row["BONUSACCOUNTNO"]);  // Борлуулалтын бонусын урамшууллын данс
//                glfile.AccountEntry = 1;// CREDIT
//                glfile.TxnAmount = Static.ToDecimal(row["Price"]);
//                glfile.Desc = "Борлуулалтын бонусын урамшууллын гүйлгээ (" + Static.ToStr(row["NAME"]) + ")";
//                res = ProcessDB.DB213028(db, glfile.Get());

//                // Борлуулалтын бонусын урамшууллын зардлын гүйлгээний бичилт үүсгэх
//                glfile.ClearTran();
//                glfile.SeqNo = sequence++;
//                glfile.AccountNo = Static.ToStr(row["BONUSEXPACCOUNTNO"]);// Борлуулалтын бонусын урамшууллын зардлын данс
//                glfile.AccountEntry = 0;// DEBIT
//                glfile.TxnAmount = Static.ToDecimal(row["Price"]);
//                glfile.Desc = "Борлуулалтын бонусын урамшууллын зардлын гүйлгээ (" + Static.ToStr(row["NAME"]) + ")";
//                res = ProcessDB.DB213028(db, glfile.Get());
//            }
//        }
//        #endregion

//        return res;
//    }
//    catch (Exception ex)
//    {
//        res.ResultNo = 9110002;
//        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
//        ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
//        return res;
//    }
//    finally
//    {
//        if (res.ResultNo == 0)
//            con.Commit();
//        else
//            con.Rollback();
//    }
//    return res;
//}
//private Result FinPostingToAbacus(DbConnections db)
//{
//    Result res = new Result();
//    string salesno = "";
//    string contractNo = "";
//    decimal totalsale = 0;
//    int sequence = 0;

//    DataTable glfileDB = new DataTable();
//    GLFILE glfile = new GLFILE();
//    try
//    {

//        #region [ Select From GLFile ]
//        res = ProcessDB.DB213027(db);           //Тухайн өдрийн GLFILE order by BatchNo, SalesNo, SeqNo
//        if (res.ResultNo != 0) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа" + res.ResultDesc; }
//        if (res.Data == null) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа"; }
//        if (res.Data.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Бааз руу хандахад алдаа гарлаа"; }

//        glfileDB = res.Data.Tables[0];
//        #endregion

//        #region [ GLFILE уншиж SQL-үүд бэлдэх ]

//        foreach (DataRow row in glfileDB.Rows)
//        {
//            glfile.GetDRow(row);

//            #region [ Харилцагч Abacus дээр байгаа эсэх түүнийг нэмэх ]
//            #endregion

//            #region [ Авлагын үндсэн гүйлгээг үүсгэх ]
//            if (glfile.EntryType == 1)
//            {

//            }
//            #endregion
//            #region [ Авлагын үндсэн гүйлгээг үүсгэх ]
//            if (glfile.EntryType == 0)
//            {

//            }
//            #endregion

//            #region [ Payment-ийн бичилт үүсгэх ]
//            if (glfile.EntryType == 2)
//            {

//            }
//            #endregion
//        }
//        #endregion

//        return res;
//    }
//    catch (Exception ex)
//    {
//        res.ResultNo = 9110002;
//        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
//        ISM.Lib.Static.WriteToLogFile("ProcessError.log", ex.Message + ex.Source + ex.StackTrace);
//        return res;
//    }
//    return res;
//}
#endregion        