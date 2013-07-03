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
    public class GLDB
    {
        #region [ DB211 - GLDB SQL's ]
        #region [ DB211014 - Дансны үлдэгдэл авах ]
        public static Result DB211014(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = 
@"select Account,BranchNo,CurCode,Year,OpenBal,EndBal,DTBal,CTBal from ChartBalance 
where year=:1 and branchNo=:2 and curcode=:3 and account=:4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211014", pParam);
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
        #region [ DB211015 - Өмнөх өдрийн үлдэгдэл авах ]
        public static Result DB211015(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select Account,BranchNo,CurCode,TxnDate,DTBal,CTBal,OpenBal, EndBal from ChartBalanceDaily
where TxnDate=:1 and branchNo=:2 and curcode=:3 and account=:4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211015", pParam);
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
        #region [ DB211016 - Өмнөх өдрийн хамгийн сүүлийн өдрийн үлдэгдэл авах ]
        public static Result DB211016(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql = 
@"select Account,BranchNo,CurCode,TxnDate,DTBal,CTBal,OpenBal, EndBal from ChartBalanceDaily
where TxnDate<:1 and branchNo=:2 and curcode=:3 and account=:4 and rownum<2
order by TxnDate desc";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211016", pParam);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211017 - ChartBalance Insert||Update ]
        public static Result DB211017(DbConnections pDB, string pTxnType, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                object[] obj = new object[7];
                obj[0] = pParam[0];
                obj[1] = pParam[1];
                obj[2] = pParam[2];
                obj[3] = pParam[3];
                obj[4] = pParam[4];
                obj[5] = pParam[5];
                obj[6] = pTxnType;

                sql =
@"MERGE INTO ChartBalance b
USING (
SELECT :1 Account, :2 BranchNo,  :3 CurCode, :4 Year, :5+:6 EndBal, decode(:7, 'D', :5, 0) DTBal, decode(:7, 'C', :5, 0) CTBal
  FROM dual
  ) e
ON (b.Account = e.Account and b.branchNo = e.branchNo and b.curcode = e.curcode and b.year = e.year)
WHEN MATCHED THEN
  UPDATE SET EndBal = EndBal +:5, DTBal=DTBal+decode(:7, 'D', :5, 0), CTBal=CTBal+decode(:7, 'C', :5, 0)
WHEN NOT MATCHED THEN
 insert (Account, BranchNo, CurCode, Year, OpenBal, EndBal, DTBal, CTBal)
values (:1, :2, :3, :4, 0, :5+:6, decode(:7, 'D', :5, 0), decode(:7, 'C', :5, 0))";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211017", obj);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211018 - ChartBalance Update ]
        public static Result DB211018(DbConnections pDB, string pTxnType, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                object[] obj = new object[6];
                obj[0] = pParam[0];
                obj[1] = pParam[1];
                obj[2] = pParam[2];
                obj[3] = pParam[3];
                obj[4] = pParam[4];
                obj[5] = pTxnType;

                sql = 
@"UPDATE ChartBalance
SET
    OpenBal = OpenBal+:5,
    EndBal = EndBal+:5,
    DTBal=DTBal+decode(:6, 'D', :5, 0),
    CTBal=CTBal+decode(:6, 'C', :5, 0)
where
    Account=:1 and branchNo=:2 and curcode=:3 and year>:4
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211018", obj);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion

        #region [ DB211019 - ChartBalanceDaily Insert||Update ]
        public static Result DB211019(DbConnections pDB, string pTxnType, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                decimal pEndBal = 0;
                #region [Check Rows]
                object[] objrows = new object[4];
                objrows[0] = pParam[0]; //Account
                objrows[1] = pParam[1]; //BranchNo
                objrows[2] = pParam[2]; //CurCode
                objrows[3] = pParam[3]; //TxnDate

                sql = @"
select account, endbal from ChartBalanceDaily 
where Account=:1 and branchNo = :2 and curcode = :3 and txndate = :4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211019", objrows);

                if (res.ResultNo != 0)
                    return res;
                #endregion

                if (res.AffectedRows == 0)
                {
                    #region [ Select Last TxnRow ]
                    sql = @"
select EndBal
from ChartBalanceDaily
where
Account=:1 and branchNo=:2 and curcode = :3
and txndate=(select max(txndate) from ChartBalanceDaily where Account=:1 and branchNo = :2 and curcode = :3 and txndate < :4)";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211019", objrows);

                    if (res.ResultNo != 0)
                        return res;
                    #endregion
                    #region [ Insert ]
                    if (res.AffectedRows == 0)
                    {
                        sql = @"
insert into ChartBalanceDaily(Account, BranchNo, CurCode, txndate, OpenBal, EndBal, DTBal, CTBal)
values (:1, :2, :3, :4, 0, 0, 0, 0)
";
                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB211019", objrows);
                    }
                    else
                    {
                        pEndBal = Static.ToDecimal(res.Data.Tables[0].Rows[0]["ENDBAL"]);

                        object[] objinsertrows = new object[5];
                        objinsertrows[0] = pParam[0]; //Account
                        objinsertrows[1] = pParam[1]; //BranchNo
                        objinsertrows[2] = pParam[2]; //CurCode
                        objinsertrows[3] = pParam[3]; //TxnDate
                        objinsertrows[4] = pEndBal; //OpenBal & EndBal

                        sql = @"
insert into ChartBalanceDaily(Account, BranchNo, CurCode, txndate, OpenBal, EndBal, DTBal, CTBal)
values (:1, :2, :3, :4, :5, :5, 0, 0)
";
                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB211019", objinsertrows);
                    }
                    #endregion
                }

                object[] obj = new object[6];
                obj[0] = pParam[0]; //Account
                obj[1] = pParam[1]; //BranchNo
                obj[2] = pParam[2]; //CurCode
                obj[3] = pParam[3]; //TxnDate
                obj[4] = pParam[4]; //Amount
                obj[5] = pTxnType;  //TxnType

                sql =
    @"
update ChartBalanceDaily b
set
    b.EndBal = b.EndBal +:5, 
    b.DTBal=b.DTBal+decode(:6, 'D', :5, 0), 
    b.CTBal=b.CTBal+decode(:6, 'C', :5, 0)
where
    Account=:1 and branchNo = :2 and curcode = :3 and txndate = :4
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211019", obj);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211020 - ChartBalanceDaily Update ]
        public static Result DB211020(DbConnections pDB, string pTxnType, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                object[] obj = new object[6];
                obj[0] = pParam[0];
                obj[1] = pParam[1];
                obj[2] = pParam[2];
                obj[3] = pParam[3];
                obj[4] = pParam[4];
                obj[5] = pTxnType;

                sql = @"
UPDATE ChartBalanceDaily
SET
OpenBal = OpenBal +:5,
EndBal = EndBal +:5,
DTBal=DTBal+decode(:6, 'D', :5, 0),
CTBal=CTBal+decode(:6, 'C', :5, 0)
where
Account=:1 and branchNo=:2 and curcode=:3 and txndate>:4
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211020", obj);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211021 - ChartTxn INSERT ]
        public static Result DB211021(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql = @"
insert into ChartTxn (JrNo,EntryNo,AccountNo,BranchNo,CurrCode,Amount,Description,TxnDate,PostDate,UserNo,
Corr,TxnCode,Rate,Balance,TxnType)
values(:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,
:11,:12,:13, :14, :15)
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB211021", pParam);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211022 - Ерөнхий дэвтэрлүү бичигдээгүй GL гүйлгээ жагсаалт авах ]
        public static Result DB211022(DbConnections pDB, DateTime pTxnDate)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"select txndate, branchno, curcode, GL, TxnEntry, sum(Amount) as amount, sum(BASEAMOUNT) as BASEAMOUNT
from gltxn
where corr=0 and flag=0 and txndate=:1
group by txndate, branchno, curcode, gl, TxnEntry
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211022", pTxnDate);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211023 - Ерөнхий дэвтэрлүү бичигдсэн төлөвт оруулах ]
        public static Result DB211023(DbConnections pDB, DateTime pTxnDate)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql = @"
update gltxn 
set flag = 1
where corr=0 and flag=0 and txndate=:1"
;
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211023", pTxnDate);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211024 - ChartMonthBal Insert||Update ]
        public static Result DB211024(DbConnections pDB, string pTxnType, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                #region [Check Rows]
                object[] objrows = new object[5];
                objrows[0] = pParam[0]; //Account
                objrows[1] = pParam[1]; //BranchNo
                objrows[2] = pParam[2]; //CurCode
                objrows[3] = pParam[3]; //Year
                objrows[4] = pParam[4]; //Month

                sql = @"
select account
from CHARTMONTHBAL 
where Account=:1 and branchNo = :2 and curcode = :3 and year = :4 and Month = :5";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211024", objrows);

                if (res.ResultNo != 0)
                    return res;
                #endregion

                if (res.AffectedRows == 0)
                {

                    #region [Select Last TxnRow]
                    sql = @"
select EndBal
from chartmonthbal
where
Account=:1 and branchNo = :2 and curcode = :3 and year = :4
and Month=(select max(month) as maxmonth from chartmonthbal where Account=:1 and branchNo = :2 and curcode = :3 and year = :4 and month<:5)";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211024", objrows);

                    if (res.ResultNo != 0)
                        return res;
                    #endregion

                    if (res.AffectedRows == 0)
                    {
                        sql = @"
 insert into chartmonthbal(Account, BranchNo, CurCode, Year, month, OpenBal, EndBal, DTBal, CTBal)
values (:1, :2, :3, :4, :5, 0, 0, 0, 0)
";
                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB211024", objrows);
                    }
                    else
                    {

                        object[] objinsertrows = new object[6];
                        objinsertrows[0] = pParam[0]; //Account
                        objinsertrows[1] = pParam[1]; //BranchNo
                        objinsertrows[2] = pParam[2]; //CurCode
                        objinsertrows[3] = pParam[3]; //Year
                        objinsertrows[4] = pParam[4]; //Month
                        objinsertrows[5] = Static.ToDecimal(res.Data.Tables[0].Rows[0]["ENDBAL"]); //OpenBal & EndBal

                        sql = @"
 insert into chartmonthbal(Account, BranchNo, CurCode, Year, month, OpenBal, EndBal, DTBal, CTBal)
values (:1, :2, :3, :4, :5, :6, :6, 0, 0)
";
                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB211024", objinsertrows);
                    }
                }

                object[] obj = new object[7];
                obj[0] = pParam[0]; //Account
                obj[1] = pParam[1]; //BranchNo
                obj[2] = pParam[2]; //CurCode
                obj[3] = pParam[3]; //Year
                obj[4] = pParam[4]; //Month

                obj[5] = pParam[5]; //Amount
                obj[6] = pTxnType;  //TxnType

                sql = @"
update
    CHARTMONTHBAL
SET
    EndBal = EndBal +:6,
    DTBal=DTBal+decode(:7, 'D', :6, 0),
    CTBal=CTBal+decode(:7, 'C', :6, 0)
where Account=:1 and branchNo = :2 and curcode = :3 and year = :4 and Month = :5
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211024", obj);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211025 - ChartMonthBal Update ]
        public static Result DB211025(DbConnections pDB, string pTxnType, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql;
                object[] obj = new object[7];
//(db, TxnType, new object[] { account, branch, currency, Year, Month,  amount, curbal });
                obj[0] = pParam[0]; //account
                obj[1] = pParam[1]; //branch
                obj[2] = pParam[2]; //currency
                obj[3] = pParam[3]; //Year
                obj[4] = pParam[4]; //Month
                obj[5] = pParam[5]; //amount
                obj[6] = pTxnType;  //TxnType
sql = @"
UPDATE
    CHARTMONTHBAL
SET
    EndBal = EndBal +:6,
    OpenBal = OpenBal +:6,
    DTBal=DTBal+decode(:7, 'D', :6, 0),
    CTBal=CTBal+decode(:7, 'C', :6, 0)
where
    Account=:1 and branchNo=:2 and curcode=:3 and to_date(year||month, 'yyyymm')>to_date(:4||:5, 'yyyymm')
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211025", obj);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211026 - ChartTxn-аас бүх талбарыг нь select parameter pJrNo ]
        public static Result DB211026(DbConnections pDB, long pJrNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT JRNO, ENTRYNO, ACCOUNTNO, BRANCHNO, CURRCODE, AMOUNT, DESCRIPTION, TXNDATE, POSTDATE, USERNO,
CORR, TXNCODE, RATE, BALANCE, TXNTYPE
FROM CHARTTXN
WHERE JRNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211026", pJrNo);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211027 - Update ChartTxn parameter pJrNo ]
        public static Result DB211027(DbConnections pDB, long pJrNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"Update
    ChartTxn
set
    Corr=1
where
    JrNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211027", pJrNo);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211028 - Ханшийн тэгшитгэл, арилжааны ашиг алдагдлыг тооцох ]
        public static Result DB211028(DbConnections pDB, object[] pParam)
        //new object[] {Core.SystemProp.BaseCurrency, Core.SystemProp.GLTxnDate.Year, 0, Core.SystemProp.PosGLNumber, GLTxnDate}
        {
            Result res = new Result();
            try
            {
                string sql;

                if (Static.ToLong(pParam[2]) != 0)
                {
                    /*(20111028
                            select a.currency, nvl(c.RATE,0)  oldrate, nvl(b.rate,0) rate, a.GLEQUIV
                            from currency a
                            left join currencyhist b on a.currency=b.currency and b.curdate=:5
                            left join currencyhist c on a.currency=c.currency and c.curdate=:5-1
                            ) C */
                    //Салбараар
                    sql =
@"select C.currency, nvl(c.oldrate,0) OldRate, nvl(C.Rate,0) Rate,
         trunc(nvl(BE.EndBal,0),2) BEBal, trunc(nvl(BP.EndBal,0) * nvl(C.Rate,0),2) BPBal,
         trunc(nvl(BP.EndBal,0) * nvl(C.Rate,0) - nvl(BE.EndBal,0),2) TxnAmount, BP.BranchNo Branch
from (
    select a.currency, nvl(b.oldrate,0)  oldrate, nvl(b.rate,0) rate, a.GLEQUIV
    from currency a
    left join currencyhist b on a.currency=b.currency and b.curdate=:5
    ) c
left join ChartBalance BP on BP.curcode=C.CURRENCY and BP.year=:2 and
BP.ACCOUNT=:4 and BP.BranchNo=:3
left join ChartBalance BE on BE.curcode=:1 and BE.year=:2 and BE.ACCOUNT=C.GLEQUIV and BE.BranchNo=:3 where C.Currency<>:1";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211028", new object[] { pParam[0], pParam[1], pParam[2], pParam[3], pParam[4] });
                }
                else
                {
                    //Системийн хэмжээнд
                    sql =
@"select    C.currency, nvl(c.oldrate,0) OldRate, nvl(C.Rate,0) Rate,
            trunc(nvl(BE.EndBal,0),2) BEBal, trunc(nvl(BP.EndBal,0) * nvl(C.Rate,0),2) BPBal,
            trunc(nvl(BP.EndBal,0) * nvl(C.Rate,0) - nvl(BE.EndBal,0),2) TxnAmount
from 
    (
    select a.currency, nvl(b.oldrate,0)  oldrate, nvl(b.rate,0) rate, a.GLEQUIV
    from currency a
    left join currencyhist b on a.currency=b.currency and b.curdate=:4
    ) C
left join ( select Account, CurCode, Year, sum(EndBal) EndBal from ChartBalance where year=:2 and ACCOUNT=:3 group by Account, CurCode, Year
        ) BP on BP.curcode=C.CURRENCY
left join ( select Account, CurCode, Year, sum(EndBal) EndBal from ChartBalance where curcode=:1 and year=:2 group by Account, CurCode, Year
        ) BE on BE.ACCOUNT=C.GLEQUIV
where C.Currency<>:1";
/*
                    sql =
@"
select    C.currency, nvl(c.oldrate,0) OldRate, nvl(C.Rate,0) Rate,
            trunc(nvl(BE.CurBal,0),2) BEBal, trunc(nvl(BP.CurBal,0) * nvl(C.Rate,0),2) BPBal,
            trunc(nvl(BP.CurBal,0) * nvl(C.Rate,0) - nvl(BE.CurBal,0),2) TxnAmount
from (
        select a.currency, nvl(c.RATE,0)  oldrate, nvl(b.rate,0) rate, a.GLEQUIV
        from currency a
        left join currencyhist b on a.currency=b.currency and b.curdate=:4
        left join currencyhist c on a.currency=c.currency and c.curdate=:4-1
        ) C
left join (
        select Account, CurCode, Year, sum(CurBal) CurBal
    from ChartBalance where year=:2 and ACCOUNT=:3 group by Account, CurCode, Year
) BP on BP.curcode=C.CURRENCY
left join (
    select Account, CurCode, Year, sum(CurBal) CurBal from ChartBalance
    where curcode=:1 and year=:2 group by Account, CurCode, Year
) BE on BE.ACCOUNT=C.GLEQUIV
where C.Currency<>:1
";*/
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211028", new object[] { pParam[0], pParam[1], pParam[3], pParam[4] });
                }
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211029 - Ерөнхий дэвтрийн орлого зарлагыг суурь валют руу хөрвүүлэх ]
        public static Result DB211029(DbConnections pDB, object[] pParam)
        //{Core.SystemProp.GLTxnDate.Year, Core.SystemProp.BaseCurrency, 0, GLTxndate}
        {
            Result res = new Result();
            try
            {
                string sql;
                /*20111028
    currency =
                (
                select b.currency, nvl(b.rate,0) rate
                from currencyhist b
                where b.curdate=:4
                )
                */
                sql =
@"select BR.branch, C.account, B.EndBal, B.CURCODE, (B.EndBal * Cur.Rate) BaseAmount
from chart C
left join chartgroup G on G.groupno=C.groupno
left join chartbalance B on B.account=C.account and B.year=:1
left join 
    (
    select b.currency, nvl(b.rate,0) rate
    from currencyhist b
    where b.curdate=:4
    ) Cur on CUR.CURRENCY=B.CurCode
right join branch BR on BR.branch=B.branchno
where G.Type in (4,5) and B.CurCode<>:2
and BR.branch = decode(:3, 0, BR.branch, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211029", pParam);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211030 - Шинэ оны эхний үлдэгдэлийг суулгах ]
        public static Result DB211030(DbConnections pDB, int pYear)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"insert into chartbalance 
(ACCOUNT,BRANCHNO,CURCODE,YEAR,OPENBAL,EndBal,DTBAL, CTBAL)
select C.account, BR.branch, B.CURCODE, B.Year+1, B.EndBal,  B.EndBal, 
(CASE WHEN B.EndBal>0 THEN B.EndBal ELSE 0 END),
(CASE WHEN B.EndBal<0 THEN B.EndBal ELSE 0 END)
from chartbalance B
left join chart C on B.account=C.account 
left join Currency Cur on CUR.CURRENCY=B.CurCode
right join branch BR on BR.branch=B.branchno
where B.EndBal<>0 and B.year=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211030", pYear);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211031 - Оны хаалт хийх орлого зарлагын дансны жагсаалт авах ]
        public static Result DB211031(DbConnections pDB, int pYear, int pBranch, DateTime pGLTxnDate)
        {
            Result res = new Result();
            try
            {
                string sql;
                /*20111028
    currency =
                (
                select b.currency, nvl(b.rate,0) rate
                from currencyhist b
                where b.curdate=:3
                )
                */
                sql =
@"select BR.branch, C.account, B.EndBal, B.CURCODE, (B.EndBal * Cur.Rate) BaseAmount from chart C
left join chartgroup G on G.groupno=C.groupno
left join chartbalance B on B.account=C.account and B.year=:1
left join (
    select b.currency, nvl(b.rate,0) rate
    from currencyhist b
    where b.curdate=:3
    ) Cur on CUR.CURRENCY=B.CurCode
right join branch BR on BR.branch=B.branchno
where G.Type in (4,5)
and BR.branch = decode(:2, 0, BR.branch, :2)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211031", pYear, pBranch, pGLTxnDate);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211032 - Chart-ын данс байгаа эсэхийг шалгах  ]
        public static Result DB211032(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select account from chart where account=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211032", pAccountNo);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion

        #region [ DB211033 - GLProcessList-ийн жагсаалт мэдээлэл авах  ]
        public static Result DB211033(DbConnections pDB, DateTime pTxnDate)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select TxnDate, ProcessNo, ProcessFunc, Name, StartDate, EndDate, Status, ErrorDesc
from GLProcessList
where TxnDate=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211033", pTxnDate);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211034 - GLProcessList-д бичлэг нэмэх ]
        public static Result DB211034(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"insert into GLProcessList(TXNDATE, PROCESSNO, ProcessFunc, Name, STARTDATE, ENDDATE, STATUS, ERRORDESC)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB211034", pParam);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211035 - GLProcessList-ийн мэдээлэл өөрчлөх ]
        public static Result DB211035(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"
update
    GLProcessList
set
    StartDate = :1, 
    EndDate = :2, 
    Status=:3, 
    ErrorDesc=:4
where
    TxnDate=:5 
and ProcessNo=:6";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211035", pParam);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion

        #region [ DB211036 - Шинэ оны  сарын үлдэгдэл суулгах ]
        public static Result DB211036(DbConnections pDB, int pYear)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"insert into CHARTMONTHBAL (ACCOUNT,BRANCHNO,CURCODE,YEAR, 
MONTH,OPENBAL,EndBal,DTBAL, CTBAL)
select C.account, BR.branch, B.CURCODE, B.Year+1, 1, B.EndBal,  B.EndBal, 
(CASE WHEN B.EndBal>0 THEN B.EndBal ELSE 0 END),
(CASE WHEN B.EndBal<0 THEN B.EndBal ELSE 0 END)
from CHARTBALANce B
left join chart C on B.account=C.account 
left join Currency Cur on CUR.CURRENCY=B.CurCode
right join branch BR on BR.branch=B.branchno
where B.EndBal<>0 and B.year=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211036", pYear);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211037 - Шинэ оны эхний үлдэгдэлийг суулгах[Өдөр] ]
        public static Result DB211037(DbConnections pDB, int pYear)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"
insert into CHARTBALANCEDAILY(ACCOUNT,BRANCHNO,CURCODE,txndate,OPENBAL,EndBal,DTBAL, CTBAL)
select C.account, BR.branch, B.CURCODE, to_date(B.Year+1||'0101', 'yyyymmdd'), B.EndBal,  B.EndBal, 
(CASE WHEN B.EndBal>0 THEN B.EndBal ELSE 0 END),
(CASE WHEN B.EndBal<0 THEN B.EndBal ELSE 0 END)
from CHARTBALANce B
left join chart C on B.account=C.account 
left join Currency Cur on CUR.CURRENCY=B.CurCode
right join branch BR on BR.branch=B.branchno
where B.EndBal<>0 and B.year=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211037", pYear);
                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB211038 - Оны хаалт хийх /Орлого зарлагын дансыг тэглээд үлдэгдэлийг TmpYearBalance руу update хийх/ ]
        public static Result DB211038(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update bacaccount
set TMPYEARBALANCE=balance, balance=0
where accountno in (
select a.accountno
from bacaccount a
left join bacproduct b on a.prodcode=b.prodcode
where b.type in (1,2)
and a.balance<>0)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211038");
                
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

        #endregion
    }
}
 