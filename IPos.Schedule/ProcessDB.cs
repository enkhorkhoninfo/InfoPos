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
namespace IPos.DB
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
        #region [ DB213 - Процессийн SQL ]
        #region [ DB213001 - Процессийн жагсаалт авах ]
        public static Result DB213001(DbConnections pDB, DateTime pDate)
        {
            Result res = new Result();
            try
            {
                string sql = @"select TXNDATE, PROCESSNO, PROCESSFUNC, NAME, STARTDATE, ENDDATE, STATUS, ERRORDESC, FREQ
from ProcessList where TxnDate=:1 order by ProcessNo";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213001", pDate);
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
        #region [ DB213002 - Update ProcessList]
        public static Result DB213002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"update ProcessList
set StartDate = :1, EndDate = :2, Status=:3, ErrorDesc =:4
where TxnDate=:5 and ProcessNo=:6";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213002", pParam);
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
        #region [ DB213003 - Ханш хадгалах]
        public static Result DB213003(DbConnections pDB, DateTime pCurDate)
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
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213003", pCurDate);
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
        #region [ DB213004 - Өдрийн гүйлгээг ерөнхий дэвтэрлүү хадгалах ]
        public static Result DB213004(DbConnections pDB, DateTime pDate)
        {
            Result res = new Result();
            try
            {
                string sql = @"insert into GLTxn 
select 
  JRNO,
  PARTJRNO,
  SUBJRNO,
  SEQNO,
  TXNDATE,
  TXNENTRY,
  POSTDATE,
  ACCOUNTMOD,
  ACCOUNTNO,
  GL,
  BRANCHNO,
  PRODCODE,
  USERNO,
  HOSTNAME,
  HOSTIP,
  HOSTMAC,
  TXNCODE,
  decode(txnentry,'C',-1*AMOUNT,AMOUNT) AMOUNT,
  RATE,
  CURCODE,
  BALANCE,
  CONTACCOUNTMOD,
  CONTACCCOUNTNO,
  CONTGL,
  CONTCURRCODE,
  CONTRATE,
  CONTAMOUNT,
  BASEAMOUNT,
  DESCRIPTION,
  CORR,
  ISCASH,
  SUPERVISOR,
  FLAG,
  M,
  GROUPTXNCODE
from DayTxn where TxnDate=:1
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213004", pDate);
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
        #region [ DB213005 - Өдрийн гүйлгээг цэвэрлэх ]
        public static Result DB213005(DbConnections pDB, DateTime pDate)
        {
            Result res = new Result();
            try
            {
                string sql = @"Delete from DayTxn where TxnDate=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213005", pDate);
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
        #region [ DB213006 - Insert Process ]
        public static Result DB213006(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"insert into ProcessList(TXNDATE, PROCESSNO, ProcessFunc, Name, STARTDATE, ENDDATE, STATUS, ERRORDESC, FREQ)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213006", pParam);
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
        #region [ DB213007 - Идэвхтэй үндсэн хөрөнгийн жагсаалт авах ]
        public static Result DB213007(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"select FAID, ACCOUNTNO, currcode from FAReg where Status=0 and Depreciation<BalanceTotal";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213007", null);
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
        #region [ DB213008 - Update GeneralParam]
        public static Result DB213008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update GeneralParam 
set ItemValue=:1
where Upper(Key)=Upper(:2)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213008", pParam);
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
        #region [ DB213009 - Хэлцлийг автоматаар хаах]
        public static Result DB213009(DbConnections pDB, DateTime pDate)
        {
            Result res = new Result();
            try
            {
                string sql = @"update deal set status=0
where status in (1,2) and EndDate+LifeTime>:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213009", pDate);
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
        #region [ DB213010 - Тухайн өдрийн ханш хадгалах]
        public static Result DB213010(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update Currency set OldRate = Rate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213010", null);
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
        #region [ DB213011 - Update GLProcessStatus GeneralParam ]
        public static Result DB213011(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update GENERALPARAM set ItemValue=:1
where Key='GLProcessStatus'";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213011", pParam);
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

        //GLPROCESS
        #region [ DB213012 - SELECT GLPROCESS ]
        public static Result DB213012(DbConnections pDB, object[] pParam)
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
  STATUS 
    FROM GLPROCESS
ORDER BY PID
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213012", null);

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
        #region [ DB213015 - UPDATE GLPROCESS ]
        public static Result DB213015(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE GLPROCESS SET DLLNAME=:2 ,
  CLASSNAME=:3 ,
  FUNCTIONNAME=:4,
  DESCRIPTION=:5 ,
  STATUS =:6
  
WHERE PID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213015", pParam);
                res = F_Error(res);
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

        //PROCESS
        #region [ DB213016 - SELECT PROCESS ]
        public static Result DB213016(DbConnections pDB, object[] pParam)
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

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213016", null);

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
        #region [ DB213017 - UPDATE PROCESS ]
        public static Result DB213017(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PROCESS SET DLLNAME=:2 ,
  CLASSNAME=:3 ,
  FUNCTIONNAME=:4,
  DESCRIPTION=:5 ,
  STATUS =:6,freq=:7
  
WHERE PID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213017", pParam);
                res = F_Error(res);
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

        #region [ DB213018 - Барааны төлөвийг Unavailable болгох ]
        public static Result DB213018(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update invseries
set Status=9
where (Status = 0 or Status = 1) and InvID in (select invID from invmain where RentFlag=2)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213018", null);
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
        #region [ DB213019 - Харицагч бүр дээр борлуулалтын дугаарыг цэвэрлэх ]
        public static Result DB213019(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update CUSTOMERIDDEVICE Set BatchNo=''";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213019", null);
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
        #region [ DB213020 - Харицагч бүр дээр Serial дугаарыг цэвэрлэх ]
        public static Result DB213020(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update CUSTOMERIDDEVICE set SerialNo=''
where ExpireDate is Null or ExpireDate <=sysdate";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB213020", null);
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
        #region [ DB213021 - Хөнгөлөлтийн түр хадгалдаг table-уудыг цэвэрлэх ]
        public static Result DB213021(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"Truncate table tmpsale";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213021", null);
                if(res.ResultNo != 0) return res;

                sql = @"Truncate table tmpsalevalue";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213021", null);
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
        #region [ DB213022 - Барьцаа хөрөнгийн мэдээллийн table-уудыг цэвэрлэх, түүх рүү хадгалах (Pledge) ]
        public static Result DB213022(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                #region [ INSERT ]
                string sql = @"insert into pledgemain_Hist select * from pledgemain where status=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213022", null);
                if(res.ResultNo != 0) return res;
                
                sql = @"insert into pledgedoc_Hist select * from pledgedoc where status=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213022", null);
                if(res.ResultNo != 0) return res;
                #endregion
                #region [ DELETE ]
                sql = @"delete from pledgemain where status=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213022", null);
                if(res.ResultNo != 0) return res;

                sql = @"delete from pledgedoc where status=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213022", null);
                
                return res;
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion        
        #region [ DB213023 - Хугацаа дууссан гэрээг архивлах ]
        public static Result DB213023(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                #region [ INSERT ]
                string sql = @"insert into ContractMain_Hist 
select * 
from ContractMain 
where ValidEndDate<=sysdate ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213023", null);
                if(res.ResultNo != 0) return res;

                sql = @"insert into ContractProd_Hist 
select P.* 
from ContractProd P 
left join ContractMain M on M.ContractNo=P.ContractNo 
where M.ValidEndDate<=sysdate ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213023", null);
                if(res.ResultNo != 0) return res;

                sql = @"insert into ContractAcnt_Hist 
select A.* 
from ContractAcnt A 
left join ContractMain M on M.ContractNo=A.ContractNo 
where M.ValidEndDate<=sysdate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213023", null);
                if(res.ResultNo != 0) return res;

                sql = @"insert into DepSchedule_Hist 
select A.* 
from DepSchedule A 
left join ContractMain M on M.ContractNo=A.ContractNo
where M.ValidEndDate<=sysdate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213023", null);
                if(res.ResultNo != 0) return res;

                #endregion
                #region [ DELETE ]
                sql = @"Delete from ContractProd
where contractno in (select contractno from ContractMain where ValidEndDate<=sysdate)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213023", null);
                if(res.ResultNo != 0) return res;

                sql = @"Delete from ContractAcnt 
where contractno in (select contractno from ContractMain where ValidEndDate<=sysdate)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213023", null);
                if(res.ResultNo != 0) return res;

                sql = @"Delete from DepSchedule 
where contractno in (select contractno from ContractMain where ValidEndDate<=sysdate)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213023", null);
                if(res.ResultNo != 0) return res;

                sql = @"Delete from ContractMain 
where ValidEndDate<=sysdate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213023", null);

                return res;
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion        
        #region [ DB213024 - Өдөр өндөрлөлт хийж болох эсэх. Бүх Пос хаагдсан эсэх ]
        public static Result DB213024(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"Select PosNo, PosName, Status
from PosTerminal 
where Status=0";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213024", null);
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
        #region [ DB213025 - Хугацаа дууссан гэрээг архивлах ]
        public static Result DB213025(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                #region [ INSERT ]
                string sql = @"insert into SALES_Hist select * from SALES";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into SALESMAIN_Hist select * from SALESMAIN";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into SALESPROD_Hist select * from SALESPROD";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into SALESPAYMENT_Hist select * from SALESPAYMENT";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into SALESPAYMENTDETAIL_Hist select * from SALESPAYMENTDETAIL";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"insert into SALESRENT_Hist select * from SALESRENT";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213025", null);
                if (res.ResultNo != 0) return res;
                #endregion
                #region [ DELETE ]
                sql = @"truncate table SALES";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"truncate table SALESMAIN";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"truncate table SALESPROD";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"truncate table SALESPAYMENT";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"truncate table SALESPAYMENTDETAIL";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213025", null);
                if (res.ResultNo != 0) return res;

                sql = @"truncate table SALESRENT";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB213025", null);

                return res;
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion        

        #region [ DB213026 - Тухайн өдрийн борлуулалтын жагсаалтыг авах ]
        public static Result DB213026(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"
select M.BatchNo, S.SalesNo, P.ProdType, P.ProdNo, P.Price*P.Quantity Price, 
P.Discount*P.Quantity Discount, P.SaleType, M.Vat, S.CustNo , 
i.SALESACCOUNTNO , i.REFUNDACCOUNTNO , i.DISCOUNTACCOUNTNO , i.BONUSACCOUNTNO , i.BONUSEXPACCOUNTNO,
m.contractno
from SalesProd P 
left join sales S on P.SalesNo=S.SalesNo 
left join SalesMain M on M.SalesNo=P.SalesNo 
left join invmain i on p.prodno=i.invid
where p.prodtype=0 
union
select M.BatchNo, S.SalesNo, P.ProdType, P.ProdNo, P.Price*P.Quantity Price, 
P.Discount*P.Quantity Discount, P.SaleType, M.Vat, S.CustNo , 
i.SALESACCOUNTNO , i.REFUNDACCOUNTNO , i.DISCOUNTACCOUNTNO , i.BONUSACCOUNTNO , i.BONUSEXPACCOUNTNO,
m.contractno
from SalesProd P 
left join sales S on P.SalesNo=S.SalesNo 
left join SalesMain M on M.SalesNo=P.SalesNo 
left join servmain i on p.prodno=i.servid
where p.prodtype=1
order by BatchNo, SalesNo
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213026", null);
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
        #region [ DB213027 - Тухайн өдрийн төлбөрийн жагсаалтыг авах ]
        public static Result DB213027(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"
select SP.SalesNo, PD.PaymentType, PD.Amount, PD.PaymentFlag, PD.ContractNo,
decode(PD.PaymentFlag, 0, c.accountno, 1, p.SUSPACCOUNT) as accountno
from SALESPAYMENTDETAIL PD
left join SALESPAYMENT SP on SP.PaymentNo=PD.PaymentNo
left join papaytype p on pd.PAYMENTTYPE=p.TYPEID
left join contractmain c on pd.contractno=c.contractno
order by SalesNo, PaymentType
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213027", null);
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
        #region [ DB213028 - Insert into GLFILE ]
        public static Result DB213028(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"
Insert into GLFILE (BatchNo,SalesNo,SeqNo ,TxnDate,PostDate,AccountNo,AccountEntry,EntryType ,TxnAmount ,CurCode,
Description,CustNo,ContractNo ,PaymentType)
values(:1, :2,:3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12,:13, :14)
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB213028", pParam);
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

        #region [ DB213029 - Гэрээний үлдэгдэл хасагдуулах ]
        public static Result DB213029(DbConnections pDB, string pContractNo, decimal pAmount)
        {
            Result res = new Result();
            try
            {
                string sql = @"
select BALANCE, BALANCETYPE
from contractmain
where contractno=:1
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213029", pContractNo);
                if (res.ResultNo != 0) return res;

                if (Static.ToInt(res.Data.Tables[0].Rows[0]["BALANCETYPE"]) == 0) //Улайхгүй
                {
                    if (Static.ToDecimal(res.Data.Tables[0].Rows[0]["BALANCE"]) - pAmount < 0)
                    {
                        res.AffectedRows = 0;
                        return res;
                    }
                }

                sql = @"
update contractmain
set BALANCE=BALANCE-:2
where contractno=:1
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213029", pContractNo, pAmount);

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
        #region [ DB213030 - Өмнөх өдрийн өндөрлөлт дуусаагүй байгаа эсэх ]
        public static Result DB213030(DbConnections pDB, DateTime pDate)
        {
            Result res = new Result();
            try
            {
                string sql = @"select TXNDATE, PROCESSNO, PROCESSFUNC, NAME, STARTDATE, ENDDATE, STATUS, ERRORDESC, FREQ
from ProcessList where TxnDate=:1 and Status<>9 order by ProcessNo";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB213030", pDate);
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
    }
}
