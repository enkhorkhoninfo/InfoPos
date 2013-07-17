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

namespace IPos.Parameter
{
    public static class DBIO
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
            else
            {
                if (res.ResultDesc.IndexOf("ORA-00001") != -1)
                {
                    res.ResultNo = 9110039;
                    res.ResultDesc = "Бичлэг давхардаж байна. {ID дугаар эсвэл Эрэмбийн дугаар}";
                }
            }
            return res;
        }
        #region [ DB202146 - GeneralParam Ерөнхий параметрын жагсаалт авах ]
        public static Result DB202146(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, TYPECODE, NAME, ITEMVALUE, DESCRIPTION, ORDERNO, ITEMTYPE, ITEMLEN, mask
        FROM GENERALPARAM
        order by OrderNo";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202146", null);
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
        #region [ DB202149 - GeneralParam Ерөнхий параметр засварлах ]
        public static Result DB202149(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE GENERALPARAM SET
        TYPECODE=:2, NAME=:3, ITEMVALUE=:4, DESCRIPTION=:5, ORDERNO=:6, ITEMTYPE=:7, ITEMLEN=:8, mask=:9
        WHERE KEY=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202149", pParam);
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
        #region [ DB202001 - Салбарын жагсаалт авах ]
        public static Result DB202001(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT BRANCH, NAME, NAME2, DIRECTOR, ORDERNO
        FROM BRANCH Order By ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202001", null);

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
        #region [ DB202003 - Салбар шинээр нэмэх ]
        public static Result DB202003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =@"insert into branch(Branch, Name, Name2, Director, OrderNo)
        values(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202003", pParam);
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
        #region [ DB202005 - Салбар устгах ]
        public static Result DB202005(DbConnections pDB, long pBranchNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select BRANCHNO FROM BACACCOUNT WHERE BRANCHNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202005", pBranchNo);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110047;
                    res.ResultDesc = "Энэ салбар дээр данс үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM Branch WHERE Branch=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202005", pBranchNo);

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
        #region [ DB202004 - Салбар засварлах ]
        public static Result DB202004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE branch SET Name=:2, Name2=:3, Director=:4, OrderNo=:5
        WHERE Branch=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202004", pParam);
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
        #region [ DB202006 - Улсын жагсаалт авах ]
        public static Result DB202006(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT COUNTRYCODE, NAME, NAME2, ORDERNO
FROM COUNTRY ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202006", null);

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
        #region [ DB202007 - Улсын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202007(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT COUNTRYCODE, NAME, NAME2, ORDERNO
FROM COUNTRY
where COUNTRYCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202007", pNo);

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
        #region [ DB202008 - Улс шинээр нэмэх ]
        public static Result DB202008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into COUNTRY(COUNTRYCODE, NAME, NAME2, ORDERNO)
values(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202008", pParam);
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
        #region [ DB202009 - Улс засварлах ]
        public static Result DB202009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE COUNTRY SET NAME=:2, NAME2=:3, ORDERNO=:4
WHERE COUNTRYCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202009", pParam);
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
        #region [ DB202010 - Улс устгах ]
        public static Result DB202010(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM COUNTRY WHERE COUNTRYCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202010", pNo);

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
        #region [ DB202011 - Хэлний жагсаалт авах ]
        public static Result DB202011(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT LANGUAGECODE, NAME, NAME2, ORDERNO
FROM LANGUAGE
ORDER BY ORDERNO";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202011", null);

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
        #region [ DB202012 - Хэлний дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202012(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT LANGUAGECODE, NAME, NAME2, ORDERNO
FROM LANGUAGE
where LANGUAGECODE = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202012", pNo);

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
        #region [ DB202013 - Хэл шинээр нэмэх ]
        public static Result DB202013(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO LANGUAGE(LANGUAGECODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202013", pParam);
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
        #region [ DB202014 - Хэл засварлах ]
        public static Result DB202014(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE LANGUAGE SET NAME=:2, NAME2=:3, ORDERNO=:4
WHERE LANGUAGECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202014", pParam);
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
        #region [ DB202015 - Хэл устгах ]
        public static Result DB202015(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM LANGUAGE WHERE LANGUAGECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202015", pNo);

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
        #region [ DB202016 - Валютын жагсаалт авах ]
        public static Result DB202016(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CurrencyCode,
GLExchProfit, GLExchLoss, GLRevProfit, GLRevLoss, OldRate, OrderNO, fractionname
FROM CURRENCY
ORDER BY OrderNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202016", null);
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
        #region [ DB202017 - Валютын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202017(DbConnections pDB, int pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CurrencyCode,
GLExchProfit, GLExchLoss, GLRevProfit, GLRevLoss, OldRate, OrderNO, fractionname
FROM CURRENCY
WHERE CURRENCY = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202017", pNo);

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
        #region [ DB202018 - Валют шинээр нэмэх ]
        public static Result DB202018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[17];
                for (int i = 0; i <= 16; i++)
                    obj[i] = pParam[i];

                string sql =
@"INSERT INTO CURRENCY(CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CurrencyCode,
GLExchProfit, GLExchLoss, GLRevProfit, GLRevLoss, OldRate, OrderNO, fractionname)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202018", obj);

                if (res.ResultNo != 0)
                {
                    res = F_Error(res);
                    return res;
                }

                sql =
@"MERGE INTO CurrencyHist b
USING (
SELECT Currency, :1 curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE
FROM Currency
) e
ON (b.Currency = e.Currency and b.curdate = e.curdate)
WHEN MATCHED THEN
UPDATE SET b.Rate = e.Rate, b.CashBuyRate=e.CashBuyRate , b.CashSellRate=e.CashSellRate , b.NonCashBuyRate=e.NonCashBuyRate , b.NonCashSellRate=e.NonCashSellRate , b.OLDRATE=e.OLDRATE
WHEN NOT MATCHED THEN
insert (Currency, curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE)
values (e.Currency, :1, e.Rate, e.CashBuyRate, e.CashSellRate, e.NonCashBuyRate, e.NonCashSellRate, e.OLDRATE)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202018", pParam[17]);

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
        #region [ DB202019 - Валют засварлах ]
        public static Result DB202019(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[17];
                for (int i = 0; i <= 16; i++)
                    obj[i] = pParam[i];

                string sql =
@"UPDATE CURRENCY SET
NAME=:2, NAME2=:3, RATE=:4, CASHBUYRATE=:5, CASHSELLRATE=:6, NONCASHBUYRATE=:7, NONCASHSELLRATE=:8, GLEQUIV=:9, CurrencyCode=:10,
GLExchProfit=:11, GLExchLoss=:12, GLRevProfit=:13, GLRevLoss=:14, OldRate=:15, OrderNo=:16, fractionname=:17
WHERE CURRENCY=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202019", obj);
                res = F_Error(res);

                if (res.ResultNo != 0)
                    return res;

                sql =
@"MERGE INTO CurrencyHist b
USING (
SELECT Currency, :1 curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE
FROM Currency
) e
ON (b.Currency = e.Currency and b.curdate = e.curdate)
WHEN MATCHED THEN
UPDATE SET b.Rate = e.Rate, b.CashBuyRate=e.CashBuyRate , b.CashSellRate=e.CashSellRate , b.NonCashBuyRate=e.NonCashBuyRate , b.NonCashSellRate=e.NonCashSellRate , b.OLDRATE=e.OLDRATE
WHEN NOT MATCHED THEN
insert (Currency, curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE)
values (e.Currency, :1, e.Rate, e.CashBuyRate, e.CashSellRate, e.NonCashBuyRate, e.NonCashSellRate, e.OLDRATE)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202019", pParam[17]);
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
        #region [ DB202020 - Валют устгах ]
        public static Result DB202020(DbConnections pDB, string pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select CURCODE FROM BACACCOUNT WHERE CURCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202020", pNo);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110048;
                    res.ResultDesc = "Энэ валют дээр данс үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM CURRENCY WHERE CURRENCY=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202020", pNo);

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
        #region [ DB202199 - Ханшийн түүх нэмэх ]
        public static Result DB202199(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"MERGE INTO CurrencyHist b
USING (
SELECT :1 currency, :2 curdate, :3 Rate, :4 CashBuyRate, :5 CashSellRate, :6 NonCashBuyRate, :7 NonCashSellRate, :8 OLDRATE
FROM dual
) e
ON (b.Currency = e.Currency and b.curdate = e.curdate)
WHEN MATCHED THEN
UPDATE SET b.Rate = e.Rate, b.CashBuyRate=e.CashBuyRate , b.CashSellRate=e.CashSellRate , b.NonCashBuyRate=e.NonCashBuyRate , b.NonCashSellRate=e.NonCashSellRate , b.OLDRATE=e.OLDRATE
WHEN NOT MATCHED THEN
insert (Currency, curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE)
values (e.Currency, e.curdate, e.Rate, e.CashBuyRate, e.CashSellRate, e.NonCashBuyRate, e.NonCashSellRate, e.OLDRATE)
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202199", pParam);

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
        #region [ DB202200 - Ханшийн түүх харах ]
        public static Result DB202200(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "CURRENCY", "CURDATE" };

                ArrayList dbparam = new ArrayList(fieldnames.Length);
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                if (pParam != null)
                {
                    int dbindex = 1;
                    for (int i = 0; i < pParam.Length; i++)
                    {
                        if (pParam[i] != null && pParam[i] != DBNull.Value && Static.ToStr(pParam[i]) != "")
                        {
                            if (sb.Length > 0) sb.Append(" AND ");
                            sb.AppendFormat(" {0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT CURRENCY, CURDATE, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, OLDRATE
FROM CURRENCYHIST 
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " ORDER BY CURDATE, CURRENCY");

                res = pDB.ExecuteQuery("core", sql, "DB202200", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202021 - Банкны жагсаалт авах ]
        public static Result DB202021(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BANKID, NAME, NAME2, ORDERNO
FROM BANK
ORDER BY OrderNo";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202021", null);
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
        #region [ DB202022 - Банкны дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202022(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BANKID, NAME, NAME2, ORDERNO
FROM BANK
WHERE BANKID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202022", pNo);

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
        #region [ DB202023 - Банк шинээр нэмэх ]
        public static Result DB202023(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO BANK(BANKID, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202023", pParam);
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
        #region [ DB202024 - Банк засварлах ]
        public static Result DB202024(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BANK SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE BANKID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202024", pParam);
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
        #region [ DB202025 - Банк устгах ]
        public static Result DB202025(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM BANK WHERE BANKID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202025", pNo);
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
        #region [ DB202096 - Банкны салбарын жагсаалт авах ]
        public static Result DB202096(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BANKID, branchID , NAME, NAME2, ORDERNO
FROM BANKBRANCH
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202096", null);

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
        #region [ DB202097 - Банкны салбарын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202097(DbConnections pDB, long pBankID, long pBranchID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BANKID, branchID , NAME, NAME2, ORDERNO
FROM BANKBRANCH
WHERE BANKID = :1 AND BRANCHID = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202097", pBankID, pBranchID);

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
        #region [ DB202098 - Банкны салбар шинээр нэмэх ]
        public static Result DB202098(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO BANKBRANCH(BANKID, branchID , NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202098", pParam);
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
        #region [ DB202099 - Банкны салбар засварлах ]
        public static Result DB202099(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BANKBRANCH SET
BRANCHID=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE BANKID = :1 AND BRANCHID = :6";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202099", pParam);
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
        #region [ DB202100 - Банкны салбар устгах ]
        public static Result DB202100(DbConnections pDB, long pBankID, long pBranchID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM BANKBRANCH WHERE BANKID = :1 AND BRANCHID = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202100", pBankID, pBranchID);

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
        #region [ DB202031 - Автомат дансны бүртгэлийн жагсаалт авах ]
        public static Result DB202031(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, NAME, NAME2, MASK, KEY 
FROM AUTONUM
order by ID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202031", null);

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
        #region [ DB202032 - Автомат дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202032(DbConnections pDB, long pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, NAME, NAME2, MASK, KEY 
FROM AUTONUM
Where ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202032", pID);

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
        #region [ DB202033 - Автомат дансны бүртгэл шинээр нэмэх ]
        public static Result DB202033(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO AUTONUM(ID, NAME, NAME2, MASK, KEY)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202033", pParam);
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
        #region [ DB202034 - Автомат дансны бүртгэл засварлах ]
        public static Result DB202034(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE AUTONUM SET
NAME=:2, NAME2=:3, MASK=:4, KEY=:5
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202034", pParam);
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
        #region [ DB202035 - Автомат дансны бүртгэл устгах ]
        public static Result DB202035(DbConnections pDB, long pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM AUTONUM WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202035", pID);

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
        #region [ DB202321 - ShortCut бүртгэлийн жагсаалт мэдээлэл авах ]
        public static Result DB202321(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, KEYS, KEYS1, KEYS2, NAME, IDVALUE, DESCRIPTION, TYPE, DECODE(TYPE,1,'ЦОНХ',2,'TEКСТ',TYPE) AS TYPENAME
FROM SHORTCUT 
ORDER BY ID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202321", null);

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
        #region [ DB202322 - ShortCut бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202322(DbConnections pDB, string pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, KEYS, KEYS1, KEYS2, NAME, IDVALUE, DESCRIPTION, TYPE
FROM SHORTCUT
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202322", pID);

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
        #region [ DB202323 - ShortCut бүртгэл нэмэх ]
        public static Result DB202323(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO SHORTCUT(ID, KEYS, KEYS1, KEYS2, NAME, IDVALUE, DESCRIPTION, TYPE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202323", pParam);
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
        #region [ DB202324 - ShortCut бүртгэл засварлах ]
        public static Result DB202324(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SHORTCUT SET
KEYS=:2, KEYS1=:3, KEYS2=:4, NAME=:5, IDVALUE=:6, DESCRIPTION=:7, TYPE=:8
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202324", pParam);
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
        #region [ DB202325 - ShortCut бүртгэл устгах ]
        public static Result DB202325(DbConnections pDB, string pID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM SHORTCUT WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202325", pID);

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
        #region [ DB202191 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл жагсаалт авах ]
        public static Result DB202191(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT Currency, BankNote, Description, OrderNo
FROM PABankNote
order by Currency";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202191", null);

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
        #region [ DB202192 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202192(DbConnections pDB, string pCurrency, string pBankNote)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT Currency, Description, OrderNo
FROM PABankNote
WHERE Currency = :1 and BankNote=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202192", pCurrency, pBankNote);

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
        #region [ DB202193 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл шинээр нэмэх ]
        public static Result DB202193(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PABankNote(Currency, BankNote, Description, OrderNo)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202193", pParam);
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
        #region [ DB202194 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл засварлах ]
        public static Result DB202194(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PABankNote SET
Description=:3, OrderNo=:4
WHERE Currency=:1 and BankNote=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202194", pParam);
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
        #region [ DB202195 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл устгах ]
        public static Result DB202195(DbConnections pDB, string pCurrency, string pNote)
        {
            Result res = new Result();
            try
            {
                string sql =
                @"DELETE FROM PABankNote WHERE Currency=:1 and banknote=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202195", pCurrency, pNote);

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
        #region [ DB202171 - Тагийн төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202171(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TagType, Name, Offset, Length, Format
FROM PATagSetup
order by Name";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202171", null);

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
        #region [ DB202172 - Тагийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202172(DbConnections pDB, string pTagType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TagType, Name, Offset, Length, Format
FROM PATagSetup
WHERE TagType = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202172", pTagType);

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
        #region [ DB202173 - Тагийн төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202173(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PATagSetup(TagType, Name, Offset, Length, Format)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202173", pParam);
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
        #region [ DB202174 - Тагийн төрлийн бүртгэл засварлах ]
        public static Result DB202174(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PATagSetup SET
Name=:2, Offset=:3, Length=:4, Format=:5
WHERE TagType = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202174", pParam);
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
        #region [ DB202175 - Тагийн төрлийн бүртгэл устгах ]
        public static Result DB202175(DbConnections pDB, string pTagType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PATagSetup WHERE TagType = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202175", pTagType);

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
        #region [ DB202331 - Тагын жагсаалт мэдээлэл авах ]
        public static Result DB202331(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TagId, TagType, Status, OrderNo
from Tagmain
ORDER BY OrderNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202331", null);

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
        #region [ DB202332 - Тагын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202332(DbConnections pDB, string TagID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TagID, TagType, Status, OrderNo
from TagMain
WHERE TagID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202332", TagID);

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
        #region [ DB202333 - Таг нэмэх ]
        public static Result DB202333(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO TagMain(TagID, TagType, Status, OrderNo)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202333", pParam);
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
        #region [ DB202334 - Таг засварлах ]
        public static Result DB202334(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE TagMain SET
TagType=:2, Status=:3, OrderNo=:4
WHERE TagID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202334", pParam);
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
        #region [ DB202335 - Таг устгах ]
        public static Result DB202335(DbConnections pDB, string TagID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM TagMain WHERE TagID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202335", TagID);

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
        #region [ DB202201 - Төлбөрийн төрлийн код бүртгэл жагсаалт авах ]
        public static Result DB202201(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TypeId, Name, Name2, SuspAccount, OrderNo, PaymentFlag, ContractType, ContractCheck
FROM PAPayType
order by Name";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202201", null);

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
        #region [ DB202202 - Төлбөрийн төрлийн код бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202202(DbConnections pDB, string pTypeId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TypeId, Name, Name2, SuspAccount, OrderNo, PaymentFlag, ContractType, ContractCheck
FROM PAPayType
WHERE TypeId = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202202", pTypeId);

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
        #region [ DB202203 - Төлбөрийн төрлийн код бүртгэл шинээр нэмэх ]
        public static Result DB202203(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PAPayType(TypeId, Name, Name2, SuspAccount, OrderNo, PaymentFlag, ContractType, ContractCheck)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202203", pParam);
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
        #region [ DB202204 - Төлбөрийн төрлийн код бүртгэл засварлах ]
        public static Result DB202204(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PAPayType SET
Name=:2, Name2=:3, SuspAccount=:4, OrderNo=:5, PaymentFlag=:6, ContractType=:7, ContractCheck=:8
WHERE TypeId=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202204", pParam);
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
        #region [ DB202205 - Төлбөрийн төрлийн код бүртгэл устгах ]
        public static Result DB202205(DbConnections pDB, string pTypeId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PAPayType WHERE TypeId=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202205", pTypeId);

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
        #region [ DB202196 - Төлбөрийн төрлийн код бүртгэл жагсаалт авах ]
        public static Result DB202196(DbConnections pDB, string pTypeId, int pPaymentFlag)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TypeId, Name, PaymentFlag
FROM PAPayType
where TypeId<>:1 and PaymentFlag=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202196", pTypeId, pPaymentFlag);

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
        #region [ DB202076 - ПОС ын бүртгэл жагсаалт авах ]
        public static Result DB202076(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select POSNo, POSName, POSDesc, POSIP, POSMAC,
POSType, decode(POSType, 'I', 'БАРАА', 'S', 'ҮЙЛЧИЛГЭЭ', 'IS', 'БАРАА БОЛОН ҮЙЛЧИЛГЭЭ') as POSTypeName
from posterminal
order by posname";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202076", null);

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
        #region [ DB202077 - ПОС ын бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202077(DbConnections pDB, string pPOSNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select POSNo, POSName, POSDesc, POSIP, POSMAC,
POSType, decode(POSType, 'I', 'БАРАА', 'S', 'ҮЙЛЧИЛГЭЭ', 'IS', 'БАРАА БОЛОН ҮЙЛЧИЛГЭЭ') as POSTypeName
from posterminal
WHERE POSNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202077", pPOSNo);

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
        #region [ DB202078 - ПОС ын бүртгэл шинээр нэмэх ]
        public static Result DB202078(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO posterminal(POSNo, POSName, POSDesc, POSIP, POSMAC, POSType)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202078", pParam);
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
        #region [ DB202079 - ПОС ын бүртгэл засварлах ]
        public static Result DB202079(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE posterminal SET
POSName=:2, POSDesc=:3, POSIP=:4, POSMAC=:5, POSType=:6
WHERE POSNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202079", pParam);
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
        #region [ DB202080 - ПОС ын бүртгэл устгах ]
        public static Result DB202080(DbConnections pDB, string pPOSNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM posterminal WHERE POSNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202080", pPOSNo);

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
        #region [ DB203019 - POS төлбөрийн хэрэгсэл бүртгэл жагсаалт авах ]
        public static Result DB203019(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT a.POSNo, a.PayTypeId, b.name
FROM POSPayType a
left join PAPayType b on a.PayTypeId=b.typeid
order by a.PayTypeId";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203019", null);

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
        #region [ DB203020 - POS төлбөрийн хэрэгсэл бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB203020(DbConnections pDB, string pPOSNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT a.POSNo, a.PayTypeId, b.name
FROM POSPayType a
left join PAPayType b on a.PayTypeId=b.typeid
WHERE a.POSNo = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203020", pPOSNo);

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
        #region [ DB203021 - POS төлбөрийн хэрэгсэл бүртгэл шинээр нэмэх ]
        public static Result DB203021(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO POSPayType(POSNo, PayTypeId)
VALUES(:1, :2)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203021", pParam);
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
        #region [ DB203022 - POS төлбөрийн хэрэгсэл бүртгэл засварлах ]
        public static Result DB203022(DbConnections pDB, string pPosNo, string pOldTypeID, string pNewTypeID)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE POSPayType SET
PayTypeId =:3
WHERE POSNo=:1 and PayTypeId=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB203022", pPosNo, pOldTypeID, pNewTypeID);
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
        #region [ DB203023 - POS төлбөрийн хэрэгсэл бүртгэл устгах ]
        public static Result DB203023(DbConnections pDB, string pPosNo, string pTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM POSPayType WHERE PosNo=:1 and PayTypeId=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203023", pPosNo, pTypeID);

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

        #region [ DB202036 - Харилцагчийн төрлийн жагсаалт авах ]
        public static Result DB202036(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, Accountno, ORDERNO, IncomeAccountno
FROM CUSTOMERTYPE 
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202036", null);

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
        #region [ DB202037 - Харилцагчийн төрлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202037(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, Accountno, ORDERNO, IncomeAccountno
FROM CUSTOMERTYPE 
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202037", pNo);

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
        #region [ DB202038 - Харилцагчийн төрөл шинээр нэмэх ]
        public static Result DB202038(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTOMERTYPE(TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO, Accountno, IncomeAccountno)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202038", pParam);
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
        #region [ DB202039 - Харилцагчийн төрөл засварлах ]
        public static Result DB202039(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERTYPE SET
CLASSCODE=:2, NAME=:3, NAME2=:4, ORDERNO=:5, Accountno=:6, IncomeAccountno=:7
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202039", pParam);
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
        #region [ DB202040 - Харилцагчийн төрөл устгах ]
        public static Result DB202040(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERTYPE WHERE TYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202040", pNo);

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
    }
}