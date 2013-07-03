using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Interface;
using EServ.Shared;
using IPos.Core;

namespace IPOS.Process
{
    public class ProcessDB
    {
        public static Result F_Error(Result res)
        {
            int DdIdErrorNo = -2147467259;

            if (res.ResultNo == DdIdErrorNo)
            {
                if (res.ResultDesc.IndexOf("ORA-00001") != -1)
                {
                    res.ResultNo = 9110039;
                    res.ResultDesc = "Бичлэг давхардаж байна. {ID дугаар эсвэл Эрэмбийн дугаар}";
                }
            }
            return res;
        }
        #region [ System DB ]
        #region [ DB2131001 - Өмнөх өдрийн өндөрлөлт дуусаагүй байгаа эсэх ]
        public static Result DB2131001(DbConnections pDB, DateTime pDate)
        {
            Result res = new Result();
            try
            {
                string sql = @"select TXNDATE, PROCESSNO, PROCESSFUNC, NAME, STARTDATE, ENDDATE, STATUS, ERRORDESC, FREQ
from ProcessList where TxnDate=:1 and Status<>9 order by ProcessNo";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB2131001", pDate);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB2131002 - Процессийн жагсаалт авах ]
        public static Result DB2131002(DbConnections pDB, DateTime pDate)
        {
            Result res = new Result();
            try
            {
                string sql = @"select TXNDATE, PROCESSNO, PROCESSFUNC, NAME, STARTDATE, ENDDATE, STATUS, ERRORDESC, FREQ
from ProcessList where TxnDate=:1 order by ProcessNo";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB2131002", pDate);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB2131003 - SELECT PROCESS ]
        public static Result DB2131003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"
SELECT   PID ,
  DLLNAME ,
  CLASSNAME ,
  FUNCTIONNAME,
  DESCRIPTION ,
  STATUS ,
freq
    FROM PROCESS
ORDER BY PID
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB2131003", null);

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB2131004 - Insert Process ]
        public static Result DB2131004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"insert into ProcessList(TXNDATE, PROCESSNO, ProcessFunc, Name, STARTDATE, ENDDATE, STATUS, ERRORDESC, FREQ)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131004", pParam);
                res = F_Error(res);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB2131005 - Update ProcessList]
        public static Result DB2131005(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"update ProcessList
set StartDate = :1, EndDate = :2, Status=:3, ErrorDesc =:4
where TxnDate=:5 and ProcessNo=:6";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB2131005", pParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #endregion
        #region [ DB213 - Процессийн SQL ]
        #region [ DB2131101 - Update GeneralParam]
        public static Result DB2131101(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update GeneralParam 
set ItemValue=:1
where Upper(Key)=Upper(:2)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB2131101", pParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion 
        #region [ DB2131102 - Өдөр өндөрлөлт хийж болох эсэх. Бүх Пос хаагдсан эсэх ]
        public static Result DB2131102(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                //string sql = @"Select PosNo, PosName, Status from PosTerminal where Status<3";
                string sql = @"Select PosNo, PosName, Status from PosTerminal where Status>3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB2131102", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion 
        
        #region [ DB2131103 - Ханш хадгалах]
        public static Result DB2131103(DbConnections pDB, DateTime pCurDate)
        {
            Result res = new Result();
            try
            {
                string sql = "";
                sql = @"MERGE INTO CurrencyHist b
USING (
SELECT Currency, :1 curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE
FROM Currency
) e
ON (b.Currency = e.Currency and b.curdate = e.curdate)
WHEN MATCHED THEN
UPDATE SET b.Rate = e.Rate, b.CashBuyRate=e.CashBuyRate , b.CashSellRate=e.CashSellRate , b.NonCashBuyRate=e.NonCashBuyRate , b.NonCashSellRate=e.NonCashSellRate , b.OLDRATE=e.OLDRATE
WHEN NOT MATCHED THEN
insert (Currency, curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE)
values (e.Currency, :1, e.Rate, e.CashBuyRate, e.CashSellRate, e.NonCashBuyRate, e.NonCashSellRate, e.OLDRATE)
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB2131103", pCurDate);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB2131104 - Тухайн өдрийн ханш хадгалах]
        public static Result DB2131104(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update Currency set OldRate = Rate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB2131104", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion     
        #region [ DB2131105 - Барааны төлөвийг Unavailable болгох ]
        public static Result DB2131105(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update invseries
set Status=0
where (Status = 1 or Status = 2) and InvID in (select invID from invmain where RentFlag=2)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB2131105", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion    
        #region [ DB2131106 - Харицагч бүр дээр борлуулалтын дугаарыг цэвэрлэх ]
        public static Result DB2131106(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"truncate table CUSTOMERIDDEVICE";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB2131106", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion        
        #region [ DB2131107 - Хугацаа дууссан борлуулалт, түүний гүйлгээг архивлах ]
        public static Result DB2131107(DbConnections pDB)
        {
            Result res = new Result();
            DbConnection con = null;
            try
            {
                con = pDB.BeginTransaction("core", "Start");

                #region [ INSERT ]
                string sql = @"insert into sales_Hist select * from sales";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into salesprod_Hist select * from salesprod";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into salesrent_Hist select * from salesrent";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into salestxn_Hist select * from salestxn";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into salesaction_Hist select * from salesaction";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                #endregion
                #region [ DELETE ]
                sql = @"delete from sales";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                sql = @"delete from salesprod";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                sql = @"delete from salesrent";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                sql = @"delete from salestxn";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                sql = @"delete from salesaction";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131107", null);
                if (res.ResultNo != 0) return res;

                return res;
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                    con.Commit();
                else
                    con.Rollback();
            }
            return res;
        }
        #endregion  
        #region [ DB2131108 - Барьцаа хөрөнгийн мэдээллийн table-уудыг цэвэрлэх, түүх рүү хадгалах (Pledge) ]
        public static Result DB2131108(DbConnections pDB)
        {
            Result res = new Result();
            DbConnection con = null;
            try
            {
                con = pDB.BeginTransaction("core", "Start");

                #region [ INSERT ]
                string sql = @"insert into pledgemain_Hist select * from pledgemain where status=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131108", null);
                if (res.ResultNo != 0) return res;
                sql = @"insert into pledgedoc_Hist select * from pledgedoc where status=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131108", null);
                if (res.ResultNo != 0) return res;
                #endregion

                #region [ DELETE ]
                sql = @"delete from pledgemain where status=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131108", null);
                if (res.ResultNo != 0) return res;

                sql = @"delete from pledgedoc where status=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131108", null);

                return res;
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                    con.Commit();
                else
                    con.Rollback();
            }
            return res;
        }
        #endregion    
        #region [ DB2131109 - Хугацаа дууссан гэрээг архивлах ]
        public static Result DB2131109(DbConnections pDB)
        {
            Result res = new Result();
            DbConnection con = null;
            try
            {
                con = pDB.BeginTransaction("core", "Start");

                #region [ INSERT ]
                string sql = @"insert into ContractMain_Hist 
select * 
from ContractMain 
where ValidEndDate<sysdate ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131109", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into ContractProd_Hist 
select P.* 
from ContractProd P 
left join ContractMain M on M.ContractNo=P.ContractNo 
where M.ValidEndDate<sysdate ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131109", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into ContractAcnt_Hist 
select A.* 
from ContractAcnt A 
left join ContractMain M on M.ContractNo=A.ContractNo 
where M.ValidEndDate<sysdate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131109", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into DepSchedule_Hist 
select A.* 
from DepSchedule A 
left join ContractMain M on M.ContractNo=A.ContractNo
where M.ValidEndDate<sysdate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB2131109", null);
                if (res.ResultNo != 0) return res;

                #endregion
                #region [ DELETE ]
                sql = @"Delete from ContractProd where contractno in (select contractno from ContractMain where ValidEndDate<sysdate)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131109", null);
                if (res.ResultNo != 0) return res;

                sql = @"Delete from ContractAcnt where contractno in (select contractno from ContractMain where ValidEndDate<sysdate)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131109", null);
                if (res.ResultNo != 0) return res;

                sql = @"Delete from DepSchedule where contractno in (select contractno from ContractMain where ValidEndDate<sysdate)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131109", null);
                if (res.ResultNo != 0) return res;

                sql = @"Delete from ContractMain where ValidEndDate<sysdate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB2131109", null);

                return res;
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                    con.Commit();
                else
                    con.Rollback();
            }
            return res;
        }
        #endregion    
        #region [ DB2131110 - Элэгдүүлэх гэрээний бичлэгийг унших ]
        public static Result DB2131110(DbConnections pDB, DateTime pTxnDate)
        {
            Result res = new Result();
            DbConnection con = null;
            try
            {

                #region [ INSERT ]
                string sql = @"
select v.ContractNo, ContractType, CustNo, ValidStartDate, ValidEndDate, v.Amount, CurCode,
       DepFreq, DepAmount, BalanceType, Vat, AccountNo, IncomeAccountNo, DepBalance, d.amount as NRSAmount
from contractmain V
left join (select * from DepSchedule where day=:1 ) D on D.contractno=V.ContractNo
where V.Status=1 and
      V.DepBalance>0 and
      V.ValidStartDate<=:1
      and 
      (   V.ValidEndDate<=:1 or
          (
            (V.DepFreq='M' and last_day(:1) = :1) or
            (V.DepFreq='Q' and add_months(trunc(sysdate,'q'),3)-1 = :1) or
            (V.DepFreq='Y' and add_months(trunc(sysdate,'y'),12)-1 = :1) or
            (V.DepFreq='S') or
            (V.ValidEndDate=:1 and (V.DepFreq='A' or V.DepFreq='B') and V.DepBalance>0)
          ) 
       ) 
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB2131110", pTxnDate);
                if (res.ResultNo != 0) return res;
                
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
            return res;
        }
        #endregion   
        #endregion
    }
}

#region [old]
//        #region [ DB213004 - Өдрийн гүйлгээг ерөнхий дэвтэрлүү хадгалах ]
//        public static Result DB213004(DbConnections pDB, DateTime pDate)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql = @"insert into GLTxn 
//select 
//  JRNO,
//  PARTJRNO,
//  SUBJRNO,
//  SEQNO,
//  TXNDATE,
//  TXNENTRY,
//  POSTDATE,
//  ACCOUNTMOD,
//  ACCOUNTNO,
//  GL,
//  BRANCHNO,
//  PRODCODE,
//  USERNO,
//  HOSTNAME,
//  HOSTIP,
//  HOSTMAC,
//  TXNCODE,
//  decode(txnentry,'C',-1*AMOUNT,AMOUNT) AMOUNT,
//  RATE,
//  CURCODE,
//  BALANCE,
//  CONTACCOUNTMOD,
//  CONTACCCOUNTNO,
//  CONTGL,
//  CONTCURRCODE,
//  CONTRATE,
//  CONTAMOUNT,
//  BASEAMOUNT,
//  DESCRIPTION,
//  CORR,
//  ISCASH,
//  SUPERVISOR,
//  FLAG,
//  M,
//  GROUPTXNCODE
//from DayTxn where TxnDate=:1
//";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213004", pDate);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
//        #endregion
//        #region [ DB213005 - Өдрийн гүйлгээг цэвэрлэх ]
//        public static Result DB213005(DbConnections pDB, DateTime pDate)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql = @"Delete from DayTxn where TxnDate=:1";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213005", pDate);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
//        #endregion
//        #region [ DB213017 - UPDATE PROCESS ]
//        public static Result DB213017(DbConnections pDB, object[] pParam)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql =
//@"UPDATE PROCESS SET DLLNAME=:2 ,
//  CLASSNAME=:3 ,
//  FUNCTIONNAME=:4,
//  DESCRIPTION=:5 ,
//  STATUS =:6,freq=:7
//  
//WHERE PID=:1";

//                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213017", pParam);
//                res = F_Error(res);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
//        #endregion
//        #region [ DB213026 - Тухайн өдрийн борлуулалтын жагсаалтыг авах ]
//        public static Result DB213026(DbConnections pDB)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql = @"
//select M.BatchNo, S.SalesNo, P.ProdType, P.ProdNo, P.Price*P.Quantity Price, 
//P.Discount*P.Quantity Discount, P.SaleType, M.Vat, S.CustNo , 
//i.SALESACCOUNTNO , i.REFUNDACCOUNTNO , i.DISCOUNTACCOUNTNO , i.BONUSACCOUNTNO , i.BONUSEXPACCOUNTNO, i.name, 
//m.contractno
//from SalesProd P 
//left join sales S on P.SalesNo=S.SalesNo 
//left join SalesMain M on M.SalesNo=P.SalesNo 
//left join invmain i on p.prodno=i.invid
//where p.prodtype=0 
//union
//select M.BatchNo, S.SalesNo, P.ProdType, P.ProdNo, P.Price*P.Quantity Price, 
//P.Discount*P.Quantity Discount, P.SaleType, M.Vat, S.CustNo , 
//i.SALESACCOUNTNO , i.REFUNDACCOUNTNO , i.DISCOUNTACCOUNTNO , i.BONUSACCOUNTNO , i.BONUSEXPACCOUNTNO, i.name, 
//m.contractno
//from SalesProd P 
//left join sales S on P.SalesNo=S.SalesNo 
//left join SalesMain M on M.SalesNo=P.SalesNo 
//left join servmain i on p.prodno=i.servid
//where p.prodtype=1
//order by BatchNo, SalesNo
//";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213026", null);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
//        #endregion        
//        #region [ DB213027 - Тухайн өдрийн төлбөрийн жагсаалтыг авах ]
//        public static Result DB213027(DbConnections pDB)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql = @"
//select SP.SalesNo, PD.PaymentType, PD.Amount, PD.PaymentFlag, PD.ContractNo,
//decode(PD.PaymentFlag, 0, c.accountno, 1, p.SUSPACCOUNT) as accountno
//from SALESPAYMENTDETAIL PD
//left join SALESPAYMENT SP on SP.PaymentNo=PD.PaymentNo
//left join papaytype p on pd.PAYMENTTYPE=p.TYPEID
//left join contractmain c on pd.contractno=c.contractno
//order by SalesNo, PaymentType
//";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213027", null);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
//        #endregion        
//        #region [ DB213028 - Insert into GLFILE ]
//        public static Result DB213028(DbConnections pDB, object[] pParam)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql = @"
//Insert into GLFILE (BatchNo,SalesNo,SeqNo ,TxnDate,PostDate,AccountNo,AccountEntry,EntryType ,TxnAmount ,CurCode,
//Description,CustNo,ContractNo ,PaymentType)
//values(:1, :2,:3, :4, :5, :6, :7, :8, :9, :10, 
//:11, :12,:13, :14)
//";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213028", pParam);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
//        #endregion        
//        #region [ DB213029 - Гэрээний үлдэгдэл хасагдуулах ]
//        public static Result DB213029(DbConnections pDB, string pContractNo, decimal pAmount)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql = @"
//select BALANCE, BALANCETYPE
//from contractmain
//where contractno=:1
//";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213029", pContractNo);
//                if (res.ResultNo != 0) return res;

//                if (Static.ToInt(res.Data.Tables[0].Rows[0]["BALANCETYPE"]) == 0) //Улайхгүй
//                {
//                    if (Static.ToDecimal(res.Data.Tables[0].Rows[0]["BALANCE"]) - pAmount < 0)
//                    {
//                        res.AffectedRows = 0;
//                        return res;
//                    }
//                }

//                sql = @"
//update contractmain
//set BALANCE=BALANCE-:2
//where contractno=:1
//";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213029", pContractNo, pAmount);

//                return res;
//            }
//            catch (Exception ex)
//            {
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
//        #endregion        
#endregion
