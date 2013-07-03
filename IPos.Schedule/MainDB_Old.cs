using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Interface;
using EServ.Shared;
using HPro.Core;
namespace HPro.DB
{
    public static class Main
    {
        #region [ DB000 - Ерөнхий SQL ]
        //SQL ажиллуулж буцаах
        public static Result DB000000(DbConnections pDB, string pSQL)
        {
            Result res = new Result();
            try
            {
                string sql = pSQL;
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB000000", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        //SQL-ыг параметртэй хамт ажиллуулж буцаах
        public static Result DB000001(DbConnections pDB, string pSQL, object [] pObj)
        {
            Result res = new Result();
            try
            {
                string sql = pSQL;
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB000001", pObj);
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
        #region [ LOG ]
        public static Result WriteDBLog(string sqllog, object[] pParam, int count)
        {
            Result res = new Result();

            sqllog += "\n VALUES(";

            for (int i = 0; i < count - 1; i++)
            {
                sqllog += pParam[i] + ", \n";
            }

            sqllog += pParam[count - 1] + " )";

            EServ.Shared.Static.WriteToLogFile("DBLog.log", sqllog);

            return res;
        }
        #endregion
        #region [ DB200 - hpro.core.dll ]
        #region [ DB200001 - Select General List ]
        public static Result DB200001(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @" select  Key, ItemValue from GENERALPARAM ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB200001");

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
        #endregion
        #region [ DB201 - hpro.docutility.dll ]
        #region [ DB201001 - Document template-ийн жагсаалт авах ]
        public static Result DB201001(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @" select  ID, Name, Name2, DOCFileName, ExportType from DOCTEMPLATE ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201001");

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
        #region [ DB201002 - Document template-ийн параметрийн жагсаалт авах ]
        public static Result DB201002(DbConnections pDB, long pID)
        {
            Result res = new Result();
            try
            {
                string sql = 
@" select  ID, PARAMID, NAME, Description, DocParamType, FORMAT, VALUE, LISTVALUE, REQUIRED
from DOCPARAM 
where ID=:1
order by OrderNo ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201002", pID);

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
        #region [ DB201003 - Document template-ийн SQL жагсаалт авах ]
        public static Result DB201003(DbConnections pDB, long pID)
        {
            Result res = new Result();
            try
            {
                string sql = 
@" select  ID, SQL, Params, ItemNo
from DOCSQL
where ID=:1
order by ItemNo ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201003", pID);

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
        #region [ DB201004 - Document template-ийн SQL -ийг ажилуулах ]
        public static Result DB201004(DbConnections pDB, string pSQL, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = pSQL;
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201004", pParam);

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
        #endregion
        #region [ DB202 - Parameter ]
        //Өгсөн
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB202002 - Салбарын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202002(DbConnections pDB, long pBranchNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BRANCH, NAME, NAME2, DIRECTOR, ORDERNO
FROM BRANCH
where BRANCH = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202002", pBranchNo);

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
        #region [ DB202003 - Салбар шинээр нэмэх ]
        public static Result DB202003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into branch(Branch, Name, Name2, Director, OrderNo)
values(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202003", pParam);

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
        #region [ DB202005 - Салбар устгах ]
        public static Result DB202005(DbConnections pDB, long pBranchNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM Branch WHERE Branch=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202005", pBranchNo);

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
        //Өгсөн
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        //Өгсөн
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        //Өгсөн
        #region [ DB202016 - Валютын жагсаалт авах ]
        public static Result DB202016(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CURRENCYCODE, OrderNO
FROM CURRENCY
ORDER BY OrderNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202016", null);

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
        #region [ DB202017 - Валютын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202017(DbConnections pDB, int pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CURRENCYCODE, OrderNO
FROM CURRENCY
WHERE CURRENCY = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202017", pNo);

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
        #region [ DB202018 - Валют шинээр нэмэх ]
        public static Result DB202018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CURRENCY(CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CURRENCYCODE, OrderNo)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, :11)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202018", pParam);

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
        #region [ DB202019 - Валют засварлах ]
        public static Result DB202019(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CURRENCY SET
NAME=:2, NAME2=:3, RATE=:4, CASHBUYRATE=:5, CASHSELLRATE=:6, NONCASHBUYRATE=:7, NONCASHSELLRATE=:8, GLEQUIV=:9, CURRENCYCODE=:10, OrderNo=:11
WHERE CURRENCY=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202019", pParam);

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
        #region [ DB202020 - Валют устгах ]
        public static Result DB202020(DbConnections pDB, int pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CURRENCY WHERE CURRENCY=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202020", pNo);

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
        //Өгсөн
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        //Өгсөн
        #region [ DB202026 - Түр дансны бүртгэлийн жагсаалт авах ]
        public static Result DB202026(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CODE, CURRENCY, BRANCH, ACCOUNTNO, NOTE
FROM ACCOUNTCODE
ORDER BY CODE";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202026", null);

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
        #region [ DB202027 - Түр дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202027(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CODE, CURRENCY, BRANCH, ACCOUNTNO, NOTE
FROM ACCOUNTCODE
WHERE CODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202027", pNo);

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
        #region [ DB202028 - Түр дансны бүртгэл шинээр нэмэх ]
        public static Result DB202028(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO ACCOUNTCODE(CODE, CURRENCY, BRANCH, ACCOUNTNO, NOTE)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202028", pParam);

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
        #region [ DB202029 - Түр дансны бүртгэл засварлах ]
        public static Result DB202029(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ACCOUNTCODE SET
CURRENCY=:2, BRANCH=:3, ACCOUNTNO=:4, NOTE=:5
WHERE CODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202029", pParam);

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
        #region [ DB202030 - Түр дансны бүртгэл устгах ]
        public static Result DB202030(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ACCOUNTCODE WHERE CODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202030", pNo);

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
        //Өгсөн
        #region [ DB202031 - Автомат дансны бүртгэлийн жагсаалт авах ]
        public static Result DB202031(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BRANCH, YEAR, NEXTNO
FROM AUTONUM
ORDER BY BRANCH";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202031", null);

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
        #region [ DB202032 - Автомат дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202032(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BRANCH, YEAR, NEXTNO FROM AUTONUM
WHERE BRANCH = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202032", pNo);

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
        #region [ DB202033 - Автомат дансны бүртгэл шинээр нэмэх ]
        public static Result DB202033(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO AUTONUM(BRANCH, YEAR, NEXTNO)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202033", pParam);

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
        #region [ DB202034 - Автомат дансны бүртгэл засварлах ]
        public static Result DB202034(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE AUTONUM SET
YEAR=:2, NEXTNO=:3
WHERE BRANCH=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202034", pParam);

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
        #region [ DB202035 - Автомат дансны бүртгэл устгах ]
        public static Result DB202035(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM AUTONUM WHERE BRANCH=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202035", pNo);

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
        //Өгсөн
        #region [ DB202036 - Харилцагчийн төрлийн жагсаалт авах ]
        public static Result DB202036(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM CUSTOMERTYPE 
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202036", null);

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
        #region [ DB202037 - Харилцагчийн төрлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202037(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM CUSTOMERTYPE 
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202037", pNo);

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
        #region [ DB202038 - Харилцагчийн төрөл шинээр нэмэх ]
        public static Result DB202038(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTOMERTYPE(TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202038", pParam);

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
        #region [ DB202039 - Харилцагчийн төрөл засварлах ]
        public static Result DB202039(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERTYPE SET
CLASSCODE=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202039", pParam);

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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        //Өгсөн
        #region [ DB202041 - Гэр бүлийн хамаарлын төрөл жагсаалт авах ]
        public static Result DB202041(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, NAME, NAME2, ORDERNO
FROM FAMILYTYPE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202041", null);

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
        #region [ DB202042 - Гэр бүлийн хамаарлын төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202042(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, NAME, NAME2, ORDERNO
FROM FAMILYTYPE
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202042", pNo);

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
        #region [ DB202043 - Гэр бүлийн хамаарлын төрөл шинээр нэмэх ]
        public static Result DB202043(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO FAMILYTYPE(TYPECODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202043", pParam);

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
        #region [ DB202044 - Гэр бүлийн хамаарлын төрөл засварлах ]
        public static Result DB202044(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE FAMILYTYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202044", pParam);

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
        #region [ DB202045 - Гэр бүлийн хамаарлын төрөл устгах ]
        public static Result DB202045(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM FAMILYTYPE WHERE TYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202045", pNo);

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
        //Өгсөн
        #region [ DB202046 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт жагсаалт авах ]
        public static Result DB202046(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM INDUSTRY
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202046", null);

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
        #region [ DB202047 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202047(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM INDUSTRY
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202047", pNo);

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
        #region [ DB202048 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт шинээр нэмэх ]
        public static Result DB202048(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO INDUSTRY(TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202048", pParam);

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
        #region [ DB202049 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт засварлах ]
        public static Result DB202049(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE INDUSTRY SET
CLASSCODE=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202049", pParam);

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
        #region [ DB202050 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт устгах ]
        public static Result DB202050(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM INDUSTRY WHERE TYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202050", pNo);

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
        //Өгсөн
        #region [ DB202051 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт жагсаалт авах ]
        public static Result DB202051(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, SUBTYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM SUBINDUSTRY
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202051", null);

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
        #region [ DB202052 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202052(DbConnections pDB, long pTypeCode, long pSubTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, SUBTYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM SUBINDUSTRY
WHERE TYPECODE=:1 AND SUBTYPECODE = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202052", pTypeCode, pSubTypeCode);

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
        #region [ DB202053 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт шинээр нэмэх ]
        public static Result DB202053(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO SUBINDUSTRY(TYPECODE, SUBTYPECODE, CLASSCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202053", pParam);

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
        #region [ DB202054 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт засварлах ]
        public static Result DB202054(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SUBINDUSTRY SET
CLASSCODE=:3, NAME=:4, NAME2=:5, ORDERNO=:6
WHERE TYPECODE=:1 AND SUBTYPECODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202054", pParam);

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
        #region [ DB202055 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт устгах ]
        public static Result DB202055(DbConnections pDB, long pTypeCode, long pSubTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM SUBINDUSTRY WHERE TYPECODE=:1 AND SUBTYPECODE=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202055", pTypeCode, pSubTypeCode);

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
        //
        #region [ DB202056 - Аймаг хотын бүртгэл жагсаалт авах ]
        public static Result DB202056(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTCITY
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202056", null);

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
        #region [ DB202057 - Аймаг хотын бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202057(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTCITY
WHERE CITYCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202057", pNo);

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
        #region [ DB202058 - Аймаг хотын бүртгэл нэмэх ]
        public static Result DB202058(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTCITY(CITYCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202058", pParam);

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
        #region [ DB202059 - Аймаг хотын бүртгэл засварлах ]
        public static Result DB202059(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTCITY SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE CITYCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202059", pParam);

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
        #region [ DB202060 - Аймаг хотын бүртгэл устгах ]
        public static Result DB202060(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTCITY WHERE CITYCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202060", pNo);

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
        //
        #region [ DB202061 - Сум дүүрэг бүртгэл жагсаалт авах ]
        public static Result DB202061(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT DISTCODE, CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTDISTRICT
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202061", null);

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
        #region [ DB202062 - Сум дүүрэг бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202062(DbConnections pDB, long pDistCode, long pCityCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT DISTCODE, CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTDISTRICT
WHERE DISTCODE = :1 AND CITYCODE = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202062", pDistCode, pCityCode);

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
        #region [ DB202063 - Сум дүүрэг бүртгэл шинээр нэмэх ]
        public static Result DB202063(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTDISTRICT( DISTCODE, CITYCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202063", pParam);

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
        #region [ DB202064 - Сум дүүрэг бүртгэл засварлах ]
        public static Result DB202064(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTDISTRICT SET
CITYCODE=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE DISTCODE=:1 AND CITYCODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202064", pParam);

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
        #region [ DB202065 - Сум дүүрэг бүртгэл устгах ]
        public static Result DB202065(DbConnections pDB, long pDistCode, long pCityCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTDISTRICT WHERE DISTCODE=:1 AND CITYCODE = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202065", pDistCode, pCityCode);

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
        //
        #region [ DB202066 - Баг, хороо бүртгэл жагсаалт авах ]
        public static Result DB202066(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT SUBDISTCODE, DISTCODE, CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTSUBDISTRICT
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202066", null);

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
        #region [ DB202067 - Баг, хороо бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202067(DbConnections pDB, long pSubDistCode, long pDistCode, long pCityCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT SUBDISTCODE, DISTCODE, CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTSUBDISTRICT
WHERE SUBDISTCODE = :1 AND DISTCODE=:2 AND CITYCODE=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202067", pSubDistCode, pDistCode, pCityCode);

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
        #region [ DB202068 - Баг, хороо бүртгэл шинээр нэмэх ]
        public static Result DB202068(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTSUBDISTRICT(SUBDISTCODE, DISTCODE, CITYCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202068", pParam);

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
        #region [ DB202069 - Баг, хороо бүртгэл засварлах ]
        public static Result DB202069(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTSUBDISTRICT SET
DISTCODE=:2, CITYCODE=:3, NAME=:4, NAME2=:5, ORDERNO=:6
WHERE SUBDISTCODE = :1 AND DISTCODE=:2 AND CITYCODE=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202069", pParam);

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
        #region [ DB202070 - Баг, хороо бүртгэл устгах ]
        public static Result DB202070(DbConnections pDB, long pSubDistCode, long pDistCode, long pCityCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTSUBDISTRICT WHERE SUBDISTCODE = :1 AND DISTCODE=:2 AND CITYCODE=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202070", pSubDistCode, pDistCode, pCityCode);

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
        //
        #region [ DB202071 - Харилцагчийн ратинг, зэрэглэл жагсаалт авах ]
        public static Result DB202071(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT RATECODE, NAME, NAME2, NOTE, MINSCORE, MAXSCORE, ORDERNO
FROM CUSTRATE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202071", null);

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
        #region [ DB202072 - Харилцагчийн ратинг, зэрэглэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202072(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT RATECODE, NAME, NAME2, NOTE, MINSCORE, MAXSCORE, ORDERNO
FROM CUSTRATE
WHERE RATECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202072", pNo);

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
        #region [ DB202073 - Харилцагчийн ратинг, зэрэглэл шинээр нэмэх ]
        public static Result DB202073(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTRATE(RATECODE, NAME, NAME2, NOTE, MINSCORE, MAXSCORE, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202073", pParam);

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
        #region [ DB202074 - Харилцагчийн ратинг, зэрэглэл засварлах ]
        public static Result DB202074(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTRATE SET
NAME=:2, NAME2=:3, NOTE=:4, MINSCORE=:5, MAXSCORE=:6, ORDERNO=:7
WHERE RATECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202074", pParam);

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
        #region [ DB202075 - Харилцагчийн ратинг, зэрэглэл устгах ]
        public static Result DB202075(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTRATE WHERE RATECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202075", pNo);

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
        //
        #region [ DB202076 - Даатгалын төрөл жагсаалт авах ]
        public static Result DB202076(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, NAME, NAME2, ORDERNO
FROM INSURANCETYPE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202076", null);

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
        #region [ DB202077 - Даатгалын төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202077(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, NAME, NAME2, ORDERNO
FROM INSURANCETYPE
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202077", pNo);

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
        #region [ DB202078 - Даатгалын төрөл шинээр нэмэх ]
        public static Result DB202078(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO INSURANCETYPE(TYPECODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202078", pParam);

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
        #region [ DB202079 - Даатгалын төрөл засварлах ]
        public static Result DB202079(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE INSURANCETYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202079", pParam);

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
        #region [ DB202080 - Даатгалын төрөл устгах ]
        public static Result DB202080(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM INSURANCETYPE WHERE TYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202080", pNo);

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
        //
        #region [ DB202081 - Дэд төрөл буюу хэлбэр жагсаалт авах ]
        public static Result DB202081(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, SUBTYPECODE, NAME, NAME2, ACCOUNTNO1, ACCOUNTNO2, ACCOUNTNO3, ACCOUNTNO4, ORDERNO
FROM SUBINSURANCETYPE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202081", null);

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
        #region [ DB202082 - Дэд төрөл буюу хэлбэр дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202082(DbConnections pDB, long pTypeCode, long pSubTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, SUBTYPECODE, NAME, NAME2, ACCOUNTNO1, ACCOUNTNO2, ACCOUNTNO3, ACCOUNTNO4, ORDERNO
FROM SUBINSURANCETYPE
WHERE TYPECODE=:1 AND SUBTYPECODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202082", pTypeCode, pSubTypeCode);

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
        #region [ DB202083 - Дэд төрөл буюу хэлбэр шинээр нэмэх ]
        public static Result DB202083(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO SUBINSURANCETYPE(TYPECODE, SUBTYPECODE, NAME, NAME2, ACCOUNTNO1, ACCOUNTNO2, ACCOUNTNO3, ACCOUNTNO4, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202083", pParam);

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
        #region [ DB202084 - Дэд төрөл буюу хэлбэр засварлах ]
        public static Result DB202084(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SUBINSURANCETYPE SET
TYPECODE=:1, NAME=:3, NAME2=:4, ACCOUNTNO1=:5, ACCOUNTNO2=:6, ACCOUNTNO3=:7, ACCOUNTNO4=:8, ORDERNO=:9
WHERE TYPECODE=:1 AND SUBTYPECODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202084", pParam);

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
        #region [ DB202085 - Дэд төрөл буюу хэлбэр устгах ]
        public static Result DB202085(DbConnections pDB, long pTypeCode, long pSubTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM SUBINSURANCETYPE WHERE TYPECODE=:1 AND SUBTYPECODE=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202085", pTypeCode, pSubTypeCode);

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
        //
        #region [ DB202086 - Нөхөн төлбөрийн зардалын төрөлүүд жагсаалт авах ]
        public static Result DB202086(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, NAME, NAME2, ORDERNO
FROM EXPENSETYPE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202086", null);

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
        #region [ DB202087 - Нөхөн төлбөрийн зардалын төрөлүүд дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202087(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, NAME, NAME2, ORDERNO
FROM EXPENSETYPE
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202087", pNo);

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
        #region [ DB202088 - Нөхөн төлбөрийн зардалын төрөлүүд шинээр нэмэх ]
        public static Result DB202088(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO EXPENSETYPE(TYPECODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202088", pParam);

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
        #region [ DB202089 - Нөхөн төлбөрийн зардалын төрөлүүд засварлах ]
        public static Result DB202089(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE EXPENSETYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202089", pParam);

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
        #region [ DB202090 - Нөхөн төлбөрийн зардалын төрөлүүд устгах ]
        public static Result DB202090(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM EXPENSETYPE WHERE TYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202090", pNo);

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
        //
        #region [ DB202091 - Гэрээний хаалтын төрөл жагсаалт авах ]
        public static Result DB202091(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CLOSEID, NAME, NAME2, ORDERNO
FROM CLOSETYPE
order by ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202091", null);

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
        #region [ DB202092 - Гэрээний хаалтын төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202092(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CLOSEID, NAME, NAME2, ORDERNO
FROM CLOSETYPE
WHERE CLOSEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202092", pNo);

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
        #region [ DB202093 - Гэрээний хаалтын төрөл шинээр нэмэх ]
        public static Result DB202093(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CLOSETYPE(CLOSEID, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202093", pParam);

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
        #region [ DB202094 - Гэрээний хаалтын төрөл засварлах ]
        public static Result DB202094(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CLOSETYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE CLOSEID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202094", pParam);

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
        #region [ DB202095 - Гэрээний хаалтын төрөл устгах ]
        public static Result DB202095(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CLOSETYPE WHERE CLOSEID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202095", pNo);

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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
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
        #region [ DB202099 - Банкны салбар засварлах ]
        public static Result DB202099(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BANKBRANCH SET
BANKID=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE BANKID = :1 AND BRANCHID = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202099", pParam);

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
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion

        #region [ DB202101 - Бараа материал төрөл жагсаалт авах ]
        public static Result DB202101(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INVTYPEID, NAME, NAME2, ORDERNO, CURRENCY, ACCOUNTNO
FROM INVENTORYTYPE
order by ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202101", null);

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
        #region [ DB202102 - Бараа материал төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202102(DbConnections pDB, long pInvTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INVTYPEID, NAME, NAME2, ORDERNO, CURRENCY, ACCOUNTNO
FROM INVENTORYTYPE
WHERE INVTYPEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202102", pInvTypeID);

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
        #region [ DB202103 - Бараа материал төрөл шинээр нэмэх ]
        public static Result DB202103(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO INVENTORYTYPE(INVTYPEID, NAME, NAME2, ORDERNO, CURRENCY, ACCOUNTNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202103", pParam);

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
        #region [ DB202104 - Бараа материал төрөл засварлах ]
        public static Result DB202104(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE INVENTORYTYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4, CURRENCY=:5, ACCOUNTNO=:6
WHERE INVTYPEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202104", pParam);

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
        #region [ DB202105 - Бараа материал төрөл устгах ]
        public static Result DB202105(DbConnections pDB, long pInvTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM INVENTORYTYPE WHERE INVTYPEID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202105", pInvTypeID);

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

        #region [ DB202106 - Бараа материал нэгж жагсаалт авах ]
        public static Result DB202106(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT UNITTYPECODE, NAME, NAME2, ORDERNO
FROM UNITTYPE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202106", null);

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
        #region [ DB202107 - Бараа материал нэгж дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202107(DbConnections pDB, long pUnitTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT UNITTYPECODE, NAME, NAME2, ORDERNO
FROM UNITTYPE
WHERE UNITTYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202107", pUnitTypeCode);

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
        #region [ DB202108 - Бараа материал нэгж шинээр нэмэх ]
        public static Result DB202108(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO UNITTYPE(UNITTYPECODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202108", pParam);

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
        #region [ DB202109 - Бараа материал нэгж засварлах ]
        public static Result DB202109(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE UNITTYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE UNITTYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202109", pParam);

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
        #region [ DB202110 - Бараа материал нэгж устгах ]
        public static Result DB202110(DbConnections pDB, long pUnitTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM UNITTYPE WHERE UNITTYPECODE = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202110", pUnitTypeCode);

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

        #region [ DB202111 - Байгууллагын дансны бүтээгдэхүүн жагсаалт авах ]
        public static Result DB202111(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, TYPE, BalanceType, ORDERNO
FROM BACPRODUCT
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202111", null);

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
        #region [ DB202112 - Байгууллагын дансны бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202112(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, TYPE, BalanceType, ORDERNO
FROM BACPRODUCT
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202112", pProdCode);

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
        #region [ DB202113 - Байгууллагын дансны бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB202113(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO BACPRODUCT(PRODCODE, NAME, NAME2, CURCODE, GL, TYPE, BalanceType, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202113", pParam);

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
        #region [ DB202114 - Байгууллагын дансны бүтээгдэхүүн засварлах ]
        public static Result DB202114(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BACPRODUCT SET
NAME=:2, NAME2=:3, CURCODE=:4, GL=:5, TYPE=:6, BalanceType=:7, ORDERNO=:8
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202114", pParam);

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
        #region [ DB202115 - Байгууллагын дансны бүтээгдэхүүн устгах ]
        public static Result DB202115(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM BACPRODUCT WHERE PRODCODE = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202115", pProdCode);

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

        #region [ DB202116 - Балансын гадуурх дансны бүтээгдэхүүн жагсаалт авах ]
        public static Result DB202116(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, TypeCode , ORDERNO
FROM CONPRODUCT
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202116", null);

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
        #region [ DB202117 - Балансын гадуурх дансны бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202117(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, TypeCode , ORDERNO
FROM CONPRODUCT
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202117", pProdCode);

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
        #region [ DB202118 - Балансын гадуурх дансны бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB202118(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CONPRODUCT(PRODCODE, NAME, NAME2, CURCODE, GL, TypeCode, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202118", pParam);

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
        #region [ DB202119 - Балансын гадуурх дансны бүтээгдэхүүн засварлах ]
        public static Result DB202119(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONPRODUCT SET
NAME=:2, NAME2=:3, CURCODE=:4, GL=:5, TypeCode=:6, ORDERNO=:7
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202119", pParam);

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
        #region [ DB202120 - Балансын гадуурх дансны бүтээгдэхүүн устгах ]
        public static Result DB202120(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CONPRODUCT WHERE PRODCODE = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202120", pProdCode);

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

        #region [ DB202121 - Балансын данс төрөл жагсаалт авах ]
        public static Result DB202121(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT GROUPNO, NAME, NAME2, TYPE, CLOSETYPE, ORDERNO
FROM CHARTGROUP
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202121", null);

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
        #region [ DB202122 - Балансын данс төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202122(DbConnections pDB, long pGroupNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT GROUPNO, NAME, NAME2, TYPE, CLOSETYPE, ORDERNO
FROM CHARTGROUP
WHERE GROUPNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202122", pGroupNo);

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
        #region [ DB202123 - Балансын данс төрөл шинээр нэмэх ]
        public static Result DB202123(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CHARTGROUP(GROUPNO, NAME, NAME2, TYPE, CLOSETYPE, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202123", pParam);

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
        #region [ DB202124 - Балансын данс төрөл засварлах ]
        public static Result DB202124(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CHARTGROUP SET
NAME=:2, NAME2=:3, TYPE=:4, CLOSETYPE=:5, ORDERNO=:6
WHERE GROUPNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202124", pParam);

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
        #region [ DB202125 - Балансын данс төрөл устгах ]
        public static Result DB202125(DbConnections pDB, long pGroupNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CHARTGROUP WHERE GROUPNO = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202125", pGroupNo);

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

        #region [ DB202126 - Харилцагчийн маск жагсаалт авах ]
        public static Result DB202126(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT MASKID, MASKNAME, MASKVALUE, MASKTYPE, ORDERNO
FROM CUSTOMERMASK
Order by ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202126", null);

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
        #region [ DB202127 - Харилцагчийн маск дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202127(DbConnections pDB, int pMaskID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT MASKID, MASKNAME, MASKVALUE, MASKTYPE, ORDERNO
FROM CUSTOMERMASK
WHERE MASKID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202127", pMaskID);

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
        #region [ DB202128 - Харилцагчийн маск шинээр нэмэх ]
        public static Result DB202128(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTOMERMASK(MASKID, MASKNAME, MASKVALUE, MASKTYPE, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202128", pParam);

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
        #region [ DB202129 - Харилцагчийн маск засварлах ]
        public static Result DB202129(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERMASK SET
MASKNAME=:2, MASKVALUE=:3, MASKTYPE=:4, ORDERNO=:5
WHERE MASKID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202129", pParam);

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
        #region [ DB202130 - Харилцагчийн маск устгах ]
        public static Result DB202130(DbConnections pDB, int pMaskID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERMASK WHERE MASKID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202130", pMaskID);

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
        //
        #region [ DB202136 - Үндсэн хөрөнгийн материал төрөл жагсаалт авах ]
        public static Result DB202136(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT FATYPEID, NAME, NAME2, CURRENCY, ACCOUNTNO, DEFACCOUNTNO, DEFEXPACCOUNTNO, PROFITLOSSACCOUNTNO, WEARYEAR, DEPFORMULA, 
ORDERNO, Days, HalfMonthCalc
FROM FATYPE
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202136", null);

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
        #region [ DB202137 - Үндсэн хөрөнгийн материал төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202137(DbConnections pDB, long pFaTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT FATYPEID, NAME, NAME2, CURRENCY, ACCOUNTNO, DEFACCOUNTNO, DEFEXPACCOUNTNO, PROFITLOSSACCOUNTNO, WEARYEAR, DEPFORMULA, 
ORDERNO, Days, HalfMonthCalc
FROM FATYPE
WHERE FATYPEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202137", pFaTypeID);

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
        #region [ DB202138 - Үндсэн хөрөнгийн материал төрөл шинээр нэмэх ]
        public static Result DB202138(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO FATYPE(FATYPEID, NAME, NAME2, CURRENCY, ACCOUNTNO, DEFACCOUNTNO, DEFEXPACCOUNTNO, PROFITLOSSACCOUNTNO, WEARYEAR, DEPFORMULA, 
ORDERNO, Days, HalfMonthCalc)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202138", pParam);

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
        #region [ DB202139 - Үндсэн хөрөнгийн материал төрөл засварлах ]
        public static Result DB202139(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE FATYPE SET
NAME=:2, NAME2=:3, CURRENCY=:4, ACCOUNTNO=:5, DEFACCOUNTNO=:6, DEFEXPACCOUNTNO=:7, PROFITLOSSACCOUNTNO=:8, WEARYEAR=:9, DEPFORMULA=:10, 
ORDERNO=:11, Days=:12, HalfMonthCalc=:13
WHERE FATYPEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202139", pParam);

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
        #region [ DB202140 - Үндсэн хөрөнгийн материал төрөл устгах ]
        public static Result DB202140(DbConnections pDB, long pFaTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM FATYPE WHERE FATYPEID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202140", pFaTypeID);

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
        //
        #region [ DB202141 - Даатгалын бүтээгдэхүүний жагсаалт авах ]
        public static Result DB202141(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, NAME, NAME2, TYPECODE, SUBTYPECODE, ORDERNO, STATUS, STARTDATE, ENDDATE, TERMFREQ, 
TERMLEN, TERMLENMIN, TERMLENMAX, RECVPRODCODE, PAYBPRODCODE, INCOMEACCOUNTNO, CHARGEMETHOD, CHARGEMIN, CHARGEMAX, DEFAULTFEE, 
FEECURRENCY, RISKGROUP, CHANGECHARGE, CHARGEMARGIN, CHANGERISK, CUSTTYPE, CONTRACTTYPE, CONTRACTTEMPLATE, CONTRACTTRACKID, CLAIMTRACKID,
FORMULAID, FORMULAEDIT, LIFETIME, OTH, UBNN, NN, UBN
FROM PRODUCT
Order by ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202141", null);

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
        #region [ DB202142 - Даатгалын бүтээгдэхүүний дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202142(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, NAME, NAME2, TYPECODE, SUBTYPECODE, ORDERNO, STATUS, STARTDATE, ENDDATE, TERMFREQ, 
TERMLEN, TERMLENMIN, TERMLENMAX, RECVPRODCODE, PAYBPRODCODE, INCOMEACCOUNTNO, CHARGEMETHOD, CHARGEMIN, CHARGEMAX, DEFAULTFEE, 
FEECURRENCY, RISKGROUP, CHANGECHARGE, CHARGEMARGIN, CHANGERISK, CUSTTYPE, CONTRACTTYPE, CONTRACTTEMPLATE, CONTRACTTRACKID, CLAIMTRACKID,
FORMULAID, FORMULAEDIT, LIFETIME, OTH, UBNN, NN, UBN
FROM PRODUCT
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202142", pProdCode);

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
        #region [ DB202143 - Даатгалын бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB202143(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PRODUCT(PRODCODE, NAME, NAME2, TYPECODE, SUBTYPECODE, ORDERNO, STATUS, STARTDATE, ENDDATE, TERMFREQ, 
TERMLEN, TERMLENMIN, TERMLENMAX, RECVPRODCODE, PAYBPRODCODE, INCOMEACCOUNTNO, CHARGEMETHOD, CHARGEMIN, CHARGEMAX, DEFAULTFEE, 
FEECURRENCY, RISKGROUP, CHANGECHARGE, CHARGEMARGIN, CHANGERISK, CUSTTYPE, CONTRACTTYPE, CONTRACTTEMPLATE, CONTRACTTRACKID, CLAIMTRACKID,
FORMULAID, FORMULAEDIT, LIFETIME, OTH, UBNN, NN, UBN)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23, :24, :25, :26, :27, :28, :29, :30,
:31, :32, :33, :34, :35, :36, :37)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202143", pParam);

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
        #region [ DB202144 - Даатгалын бүтээгдэхүүн төрөл засварлах ]
        public static Result DB202144(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PRODUCT SET
NAME=:2, NAME2=:3, TYPECODE=:4, SUBTYPECODE=:5, ORDERNO=:6, STATUS=:7, STARTDATE=:8, ENDDATE=:9, TERMFREQ=:10, 
TERMLEN=:11, TERMLENMIN=:12, TERMLENMAX=:13, RECVPRODCODE=:14, PAYBPRODCODE=:15, INCOMEACCOUNTNO=:16, CHARGEMETHOD=:17, CHARGEMIN=:18, CHARGEMAX=:19, DEFAULTFEE=:20, 
FEECURRENCY=:21, RISKGROUP=:22, CHANGECHARGE=:23, CHARGEMARGIN=:24, CHANGERISK=:25, CUSTTYPE=:26, CONTRACTTYPE=:27, CONTRACTTEMPLATE=:28, CONTRACTTRACKID=:29, CLAIMTRACKID=:30,
FORMULAID=:31, FORMULAEDIT=:32, LIFETIME=:33, OTH=:34, UBNN=:35, NN=:36, UBN=:37
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202144", pParam);

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
        #region [ DB202145 - Даатгалын бүтээгдэхүүн устгах ]
        public static Result DB202145(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PRODUCT WHERE PRODCODE = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202145", pProdCode);

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
        //
        #region [ DB202146 - GeneralParam Ерөнхий параметрын жагсаалт авах ]
        public static Result DB202146(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, TYPECODE, NAME, ITEMVALUE, DESCRIPTION, ORDERNO, ITEMTYPE, ITEMLEN
FROM GENERALPARAM
order by OrderNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202146", null);
                
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
        #region [ DB202147 - GeneralParam Ерөнхий параметрын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202147(DbConnections pDB, long pKey)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, TYPECODE, NAME, ITEMVALUE, DESCRIPTION, ORDERNO, ITEMTYPE, ITEMLEN
FROM GENERALPARAM
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202147", pKey);

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
        #region [ DB202148 - GeneralParam Ерөнхий параметр шинээр нэмэх ]
        public static Result DB202148(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO GENERALPARAM(KEY, TYPECODE, NAME, ITEMVALUE, DESCRIPTION, ORDERNO, ITEMTYPE, ITEMLEN)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202148", pParam);

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
        #region [ DB202149 - GeneralParam Ерөнхий параметр засварлах ]
        public static Result DB202149(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE GENERALPARAM SET
TYPECODE=:2, NAME=:3, ITEMVALUE=:4, DESCRIPTION=:5, ORDERNO=:6, ITEMTYPE=:7, ITEMLEN=:8
WHERE KEY=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202149", pParam);

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
        #region [ DB202150 - GeneralParam Ерөнхий параметр устгах ]
        public static Result DB202150(DbConnections pDB, long pKey)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM GENERALPARAM WHERE KEY = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202150", pKey);

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
        //
        #region [ DB202151 - Бүтээгдэхүүн борлуулалтын төлөвлөгөөний жагсаалт авах ]
        public static Result DB202151(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, YEAR, MONTH, BALAMOUNT, BALCOUNT
FROM PRODSALESPLAN";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202151", null);

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
        #region [ DB202152 - Бүтээгдэхүүн борлуулалтын төлөвлөгөөний дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202152(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, YEAR, MONTH, BALAMOUNT, BALCOUNT
FROM PRODSALESPLAN
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202152", pProdCode);

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
        #region [ DB202153 - Бүтээгдэхүүн борлуулалтын төлөвлөгөө шинээр нэмэх ]
        public static Result DB202153(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PRODSALESPLAN(PRODCODE, YEAR, MONTH, BALAMOUNT, BALCOUNT)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202153", pParam);

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
        #region [ DB202154 - Бүтээгдэхүүн борлуулалтын төлөвлөгөө засварлах ]
        public static Result DB202154(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PRODSALESPLAN SET
YEAR=:2, MONTH=:3, BALAMOUNT=:4, BALCOUNT=:5
WHERE PRODCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202154", pParam);

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
        #region [ DB202155 - Бүтээгдэхүүн борлуулалтын төлөвлөгөө устгах ]
        public static Result DB202155(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PRODSALESPLAN WHERE PRODCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202155", pProdCode);

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
        //
        #region [ DB202156 - Нөхөн төлбөрийн хаалтын төрлийн жагсаалт авах ]
        public static Result DB202156(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select CLOSEID, NAME, NAME2, ORDERNO
from claimclosetype
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202156", null);

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
        #region [ DB202157 - Нөхөн төлбөрийн хаалтын төрлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202157(DbConnections pDB, long pCloseID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select CLOSEID, NAME, NAME2, ORDERNO
from claimclosetype
WHERE CLOSEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202157", pCloseID);

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
        #region [ DB202158 - Нөхөн төлбөрийн хаалтын төрөл шинээр нэмэх ]
        public static Result DB202158(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO claimclosetype(CLOSEID, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202158", pParam);

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
        #region [ DB202159 - Нөхөн төлбөрийн хаалтын төрөл засварлах ]
        public static Result DB202159(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE claimclosetype SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE CLOSEID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202159", pParam);

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
        #region [ DB202160 - Нөхөн төлбөрийн хаалтын төрөл устгах ]
        public static Result DB202160(DbConnections pDB, long pCloseID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM claimclosetype WHERE CLOSEID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202160", pCloseID);

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
        //
        #region [ DB202161 - Гүйлгээний төрлийн жагсаалт авах ]
        public static Result DB202161(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TRANCODE, NAME, NAME2
FROM TXN
order by trancode";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202161", null);

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
        #region [ DB202162 - Гүйлгээний төрлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202162(DbConnections pDB, long pTranCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TRANCODE, NAME, NAME2
FROM TXN
WHERE TRANCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202162", pTranCode);

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
        #region [ DB202163 - Гүйлгээний төрөл шинээр нэмэх ]
        public static Result DB202163(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO TXN(TRANCODE, NAME, NAME2)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202163", pParam);

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
        #region [ DB202164 - Гүйлгээний төрөл засварлах ]
        public static Result DB202164(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE TXN SET
NAME=:2, NAME2=:3
WHERE TRANCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202164", pParam);

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
        #region [ DB202165 - Гүйлгээний төрөл устгах ]
        public static Result DB202165(DbConnections pDB, long pTranCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM TXN WHERE TRANCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202165", pTranCode);

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
        //
        #region [ DB202166 - Гүйлгээний оролтын жагсаалт авах ]
        public static Result DB202166(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ENTRYCODE, ENTRYTXNCODE, DRACNTMOD, DRACNTNO, DRCURRCODE, DRRATE, DRAMOUNT, CRACNTMOD, CRACNTNO, CRCURRCODE, 
CRAMOUNT, CRRATE, DESCRIPTION
FROM ENTRY";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202166", null);

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
        #region [ DB202167 - Гүйлгээний оролтын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202167(DbConnections pDB, long pEntry)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ENTRYCODE, ENTRYTXNCODE, DRACNTMOD, DRACNTNO, DRCURRCODE, DRRATE, DRAMOUNT, CRACNTMOD, CRACNTNO, CRCURRCODE, 
CRAMOUNT, CRRATE, DESCRIPTION
WHERE ENTRYCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202167", pEntry);

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
        #region [ DB202168 - Гүйлгээний оролт шинээр нэмэх ]
        public static Result DB202168(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO ENTRY(ENTRYCODE, ENTRYTXNCODE, DRACNTMOD, DRACNTNO, DRCURRCODE, DRRATE, DRAMOUNT, CRACNTMOD, CRACNTNO, CRCURRCODE, 
CRAMOUNT, CRRATE, DESCRIPTION)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13,)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202168", pParam);

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
        #region [ DB202169 - Гүйлгээний оролт засварлах ]
        public static Result DB202169(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ENTRY SET
ENTRYTXNCODE=:2, DRACNTMOD=:3, DRACNTNO=:4, DRCURRCODE=:5, DRRATE=:6, DRAMOUNT=:7, CRACNTMOD=:8, CRACNTNO=:9, CRCURRCODE=:10, 
CRAMOUNT=:11, CRRATE=:12, DESCRIPTION=:13
WHERE ENTRYCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202169", pParam);

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
        #region [ DB202170 - Гүйлгээний оролт устгах ]
        public static Result DB202170(DbConnections pDB, long pEntry)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ENTRY WHERE ENTRYCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202170", pEntry);

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
        //
        #region [ DB202171 - Даатгалын нөхцөлүүд, буюу эрсдлүүдийн жагсаалт авах ]
        public static Result DB202171(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT RISKID, NAME, NAME2, ORDERNO
FROM RISK
Order by ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202171", null);

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
        #region [ DB202172 - Даатгалын нөхцөлүүд, буюу эрсдлүүдийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202172(DbConnections pDB, long pRiskID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT RISKID, NAME, NAME2, ORDERNO
FROM RISK
WHERE RISKID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202172", pRiskID);

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
        #region [ DB202173 - Даатгалын нөхцөлүүд, буюу эрсдлүүдийг шинээр нэмэх ]
        public static Result DB202173(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO RISK(RISKID, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202173", pParam);

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
        #region [ DB202174 - Даатгалын нөхцөлүүд, буюу эрсдлүүдийг засварлах ]
        public static Result DB202174(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE RISK SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE RISKID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202174", pParam);

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
        #region [ DB202175 - Даатгалын нөхцөлүүд, буюу эрсдлүүдийг устгах ]
        public static Result DB202175(DbConnections pDB, long pRiskID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM RISK WHERE RISKID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202175", pRiskID);

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
        //
        #region [ DB202176 - Гүйлгээний оролтуудын жагсаалт авах ]
        public static Result DB202176(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TRANCODE, ENTRYCODE, ORDERNO
FROM TXNENTRY
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202176", null);

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
        #region [ DB202177 - Гүйлгээний оролтуудын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202177(DbConnections pDB, long pTranCode, long pEntryCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TRANCODE, ENTRYCODE, ORDERNO
FROM TXNENTRY
WHERE TRANCODE = :1 and ENTRYCODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202177", pTranCode, pEntryCode);

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
        #region [ DB202178 - Гүйлгээний оролтууд шинээр нэмэх ]
        public static Result DB202178(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO TXNENTRY(TRANCODE, ENTRYCODE, ORDERNO)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202178", pParam);

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
        #region [ DB202179 - Гүйлгээний оролтууд засварлах ]
        public static Result DB202179(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE TXNENTRY SET
ORDERNO=:3
WHERE TRANCODE = :1 and ENTRYCODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202179", pParam);

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
        #region [ DB202180 - Гүйлгээний оролтууд устгах ]
        public static Result DB202180(DbConnections pDB, long pTranCode, long pEntryCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM TXNENTRY WHERE TRANCODE = :1 and ENTRYCODE=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202180", pTranCode, pEntryCode);

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
        #endregion
        #region [ DB203 - Admin ]
        #region [ DB203001 - Хэрэглэгчийн жагсаалт авах ]
        public static Result DB203001(DbConnections pDB, int pagenumber, int pagecount)
        {
            Result res = new Result();
            try
            {
                string sql = 
@" select  UserNo, UserFname, UserLname from HPUser 
order by UserNo ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203001",null);

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
        #region [ DB203002 - Хэрэглэгчийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB203002(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select UserNo, UserFname, UserLname, UserFname2, UserLname2, RegisterNo, Position, Status, BranchNo, UserLevel, UPassword, UserType, Email, Mobile
from HPUser
where userno = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203002", pUserNo );

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
        #region [ DB203003 - Хэрэглэгчийн сонгогдсон болон сонгогдоогүй эрхийн бүлгүүд авах ]
        public static Result DB203003(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select decode(a.groupid, '', 0, 1) status, B.groupid , b.name groupname, a.expiredate
from (select * from usergroup where userno=:1) a
right join txngroup b on A.groupid=B.groupid
order by b.groupid";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203003", pUserNo);

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
        #region [ DB203004 - Хэрэглэгчийн сонгогдсон супервайзар эрхүүд авах ]
        public static Result DB203004(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select b.userno, b.userfname, b.userlname
from (select B.SUPUSERNO from hpuser a, usersup b where a.userno=b.userno and a.userno=:1 )a, hpuser b
where a.SUPUSERNO=b.userno Order By b.userno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203004", pUserNo);

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
        #region [ DB203005 - Хэрэглэгчийн зураг авах ]
        public static Result DB203005(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select pic from userpic where userno=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203005", pUserNo);

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
        #region [ DB203006 - Хэрэглэгч нэмэх ]
        public static Result DB203006(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into hpuser(UserNo, UserFname, UserLname, UserFname2, UserLname2, RegisterNo, Position, Status, BranchNo, UserLevel, UPassword, UserType, Email, Mobile)
values(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, :11, :12, :13, :14)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203006", pParam);

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
        #region [ DB203007 - Хэрэглэгч засварлах ]
        public static Result DB203007(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sqln = "UPassword=:11,";
                /*if (pParam[11] != null)
                    sqln = " UPassword=:11, ";*/

                string sql =
@"UPDATE hpuser SET 
UserFname=:2,
UserLname=:3,
UserFname2=:4,
UserLname2=:5,
RegisterNo=:6,
Position=:7,
Status=:8,
BranchNo=:9,
UserLevel=:10,
"+sqln+@"
UserType=:12,
Email=:13,
Mobile=:14
WHERE UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB203007", pParam);

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
        #region [ DB203008 - Хэрэглэгч устгах ]
        public static Result DB203008(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                //HPUser delete
                string sql =
@"DELETE FROM hpuser WHERE UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203008", pUserNo);
                if (res.ResultNo != 0) return res;

                //UserSup delete
                sql =
@"DELETE FROM usersup WHERE UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203008", pUserNo);
                if (res.ResultNo != 0) return res;

                //UserGroup delete
                sql =
@"DELETE FROM UserGroup WHERE UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203008", pUserNo);

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
        #region [ DB203009 - Хэрэглэгчийн эрхүүд нэмэх болон засварлах ]
        public static Result DB203009(DbConnections pDB, int pUserNo, DataTable DT)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete from usergroup where userno=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203009", pUserNo);
                
                if (res.ResultNo != 0) return res;

                foreach (DataRow dr in DT.Rows )
                {

                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
    @"insert into usergroup(groupid, UserNo, ExpireDate)
values(:1, :2, :3)";

                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203009", dr["GroupID"], pUserNo, dr["ExpireDate"]);
                        if (res.ResultNo != 0) return res;
                    }
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
        #region [ DB203010 - Хэрэглэгчийн supriser нэмэх болон засварлах ]
        public static Result DB203010(DbConnections pDB, int pUserNo, DataTable DT)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete from UserSup where UserNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203010", pUserNo);

                if (res.ResultNo != 0) return res;

                foreach (DataRow dr in DT.Rows)
                {
                    sql =
@"insert into UserSup(UserNo, SupUserNo)
values(:1, :2)";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203010", pUserNo, dr["UserNo"]);
                    if (res.ResultNo != 0) return res;
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
        #region [ DB203011 - Хэрэглэгчийн зураг нэмэх болон засварлах ]
        public static Result DB203011(DbConnections pDB, int pUserNo, byte[] BA)
        {
            Result res = new Result();
            try
            {
                object[] param = new object[] { pUserNo, BA };

                string sql =
@"Update UserPic set pic =:2 WHERE UserNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB203011", param);

                if (res.ResultNo != 0) return res;

                if (res.AffectedRows == 0)
                {
                    sql =
@"INSERT INTO UserPic(UserNo, Pic) VALUES(:1, :2)";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203011", param);
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
        #region [ DB203012 - Хэрэглэгчийн бүлгийн жагсаалт авах ]
        public static Result DB203012(DbConnections pDB, int pagenumber, int pagecount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT GROUPID, NAME, NAME2, ORDERNO
FROM TXNGROUP
ORDER BY GROUPID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203012", null);

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
        #region [ DB203013 - Хэрэглэгчийн бүлгийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB203013(DbConnections pDB, int pGroupId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT GROUPID, NAME, NAME2, ORDERNO
FROM TXNGROUP
where GROUPID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203013", pGroupId);

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
        #region [ DB203014 - Хэрэглэгчийн бүлгийн сонгогдсон болон сонгогдоогүй гүйлгээнүүд авах ]
        public static Result DB203014(DbConnections pDB, int pGroupId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select decode(a.trancode, '', 0, 1) status, d.trancode, d.name, d.name2
from (select trancode from grouptxn a, txngroup c where A.GROUPID=c.GROUPID and A.GROUPID=:1 ) a
right join txn d on A.trancode=d.trancode
order by d.trancode";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203014", pGroupId);

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
        #region [ DB203015 - Хэрэглэгчийн бүлэг нэмэх ]
        public static Result DB203015(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into TXNGROUP(GROUPID, NAME, NAME2, ORDERNO)
values(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203015", pParam);

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
        #region [ DB203016 - Хэрэглэгчийн бүлэг засварлах ]
        public static Result DB203016(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE TXNGROUP SET 
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE GROUPID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB203016", pParam);

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
        #region [ DB203017 - Хэрэглэгчийн бүлэг устгах ]
        public static Result DB203017(DbConnections pDB, int pGroupId)
        {
            Result res = new Result();
            try
            {
                //TXNGROUP delete
                string sql =
@"DELETE FROM TXNGROUP WHERE GROUPID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203017", pGroupId);
                if (res.ResultNo != 0) return res;

                //GROUPTXN delete
                sql =
@"DELETE FROM GROUPTXN WHERE GROUPID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203017", pGroupId);
                if (res.ResultNo != 0) return res;

                //UserGroup delete
                sql =
@"DELETE FROM UserGroup WHERE GROUPID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203017", pGroupId);

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
        #region [ DB203018 - Хэрэглэгчийн бүлэг гүйлгээнүүдийг нэмэх болон засварлах ]
        public static Result DB203018(DbConnections pDB, int pGroupId, DataTable DT)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete from GroupTxn where GroupId=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203018", pGroupId);

                if (res.ResultNo != 0) return res;

                foreach (DataRow dr in DT.Rows)
                {
                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
@"insert into GroupTxn(GroupId, TranCode)
values(:1, :2)";

                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203018", pGroupId, dr["TranCode"]);
                        if (res.ResultNo != 0) return res;
                    }
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
        #endregion
        #region [ DB204 - Contract&Insurance&Claim&ReContract ]
        #region [ DB204 - Contract ]
        #region [ DB204001 - Гэрээний жагсаалт авах ]
        public static Result DB204001(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where=" ";


                if (pParam != null)
                {
                    where = " WHERE ";
                    if (pParam[0] != "" && pParam[0] != null) where += "  b.ContractID='" + Static.ToStr(pParam[0]) + "' AND ";
                    if (pParam[1] != "" && pParam[1] != null) where += "  b.ContractNo='" + Static.ToStr(pParam[1]) + "' AND ";
                    if (pParam[2] != "" && pParam[2] != null) where += "  b.CustNo='" + Static.ToStr(pParam[2]) + "' AND ";
                    if (pParam[3] != "" && pParam[3] != null) where += "  b.CreateDate='" + Static.ToStr(pParam[3]) + "' AND ";
                    if (pParam[4] != "" && pParam[4] != null) where += "  b.ReqNo='" + Static.ToStr(pParam[4]) + "' AND ";
                    if (pParam[5] != "" && pParam[5] != null) where += "  b.CreateUser='" + Static.ToStr(pParam[5]) + "' AND ";
                    if (pParam[6] != "" && pParam[6] != null) where += "  b.AssignUser='" + Static.ToStr(pParam[6]) + "' AND ";
                    if (pParam[7] != "" && pParam[7] != null) where += "  b.Status='" + Static.ToStr(pParam[7]) + "' AND ";
                    if (pParam[8] != "" && pParam[8] != null) where += "  b.Approvedate='" + Static.ToStr(pParam[8]) + "' AND ";
                    if (pParam[9] != "" && pParam[9] != null) where += "  b.ContractUser='" + Static.ToStr(pParam[9]) + "' AND ";
                    if (pParam[10] != "" && pParam[10] != null) where += "  b.isExtended='" + Static.ToStr(pParam[10]) + "' AND ";
                    if (pParam[11] != "" && pParam[11] != null) where += "  b.ExtendCount='" + Static.ToStr(pParam[11]) + "' AND ";
                    if (pParam[12] != "" && pParam[12] != null) where += "  b.FeeAmount='" + Static.ToStr(pParam[12]) + "' AND ";
                    if (pParam[13] != "" && pParam[13] != null) where += "  b.FeeCurrency='" + Static.ToStr(pParam[13]) + "' AND ";
                    if (pParam[14] != "" && pParam[14] != null) where += "  b.Branch='" + Static.ToStr(pParam[14]) + "' AND ";
                    if (pParam[15] != "" && pParam[15] != null) where += "  b.ContractUser='" + Static.ToStr(pParam[15]) + "' AND ";
                    if (pParam[16] != "" && pParam[16] != null) where += "  b.ContractType='" + Static.ToStr(pParam[16]) + "' AND ";
                    if (pParam[17] != "" && pParam[17] != null) where += "  b.isChanged='" + Static.ToStr(pParam[17]) + "' AND ";
                    if (pParam[18] != "" && pParam[18] != null) where += "  b.CloseStatus='" + Static.ToStr(pParam[18]) + "' AND ";
                    if (pParam[19] != "" && pParam[19] != null) where += "  b.ClosedType='" + Static.ToStr(pParam[19]) + "' AND ";
                    if (pParam[20] != "" && pParam[20] != null) where += "  b.ClosedNote='" + Static.ToStr(pParam[20]) + "' AND ";
                    if (pParam[21] != "" && pParam[21] != null) where += "  b.ValutionAmount='" + Static.ToStr(pParam[21]) + "' AND ";
                    if (pParam[22] != "" && pParam[22] != null) where += "  b.StartDate='" + Static.ToStr(pParam[22]) + "' AND ";
                    if (pParam[23] != "" && pParam[23] != null) where += "  b.EndDate='" + Static.ToStr(pParam[23]) + "' AND ";
                    if (pParam[24] != "" && pParam[24] != null) where += "  b.CommissionAmount='" + Static.ToStr(pParam[24]) + "' AND ";

                    where = where.Substring(0, where.Length - 4);

                    WriteDBLog("\nLIST WHERE\n" + where, pParam, 25);
                }

                sql =
@" SELECT CONTRACTID, TEMPCONTRACTNO, CONTRACTNO, BRANCH, CUSTNO, REQNO, STARTDATE, ENDDATE, ORGENDDATE, CREATEUSER,
CREATEDATE, APPROVEDATE, APPROVEUSER, ASSIGNUSER, CONTRACTUSER,
( select sum(a.EstimateAmount*c.rate) from insurance a, currency c where A.CURCODE=C.CURRENCY and a.contractid=b.CONTRACTID ) VALUTIONAMOUNT
, VALUTIONCURRENCY, 
( select sum(a.FeeAmount*c.rate) from insurance a, currency c  where A.CURCODE=C.CURRENCY and a.contractid=b.CONTRACTID ) FEEAMOUNT, FEECURRENCY, CONTRACTTYPE,
ISCHANGED, STATUS, CLOSESTATUS, CLOSEDTYPE, CLOSEDNOTE, CLOSEDDATE, ISEXTENDED, EXTENDCOUNT, EXTENDDATE, CHANGEDUSER
FROM CONTRACT b
" + where + " Order by b.CONTRACTID";

                EServ.Shared.Static.WriteToLogFile("Error.log", sql);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204001", null);
                
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
        #region [ DB204002 - Гэрээний дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204002(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {

                string sql =
@"SELECT CONTRACTID, TEMPCONTRACTNO, CONTRACTNO, BRANCH, CUSTNO, REQNO, STARTDATE, ENDDATE, ORGENDDATE, CREATEUSER,
CREATEDATE, APPROVEDATE, APPROVEUSER, ASSIGNUSER, CONTRACTUSER,
( select sum(a.EstimateAmount*c.rate) from insurance a, currency c where A.CURCODE=C.CURRENCY and a.contractid=b.CONTRACTID ) VALUTIONAMOUNT
, VALUTIONCURRENCY, 
( select sum(a.FeeAmount*c.rate) from insurance a, currency c  where A.CURCODE=C.CURRENCY and a.contractid=b.CONTRACTID ) FEEAMOUNT, FEECURRENCY, CONTRACTTYPE,
ISCHANGED, STATUS, CLOSESTATUS, CLOSEDTYPE, CLOSEDNOTE, CLOSEDDATE, ISEXTENDED, EXTENDCOUNT, EXTENDDATE, CHANGEDUSER
FROM CONTRACT b
where b.CONTRACTID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204002", pNo);

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
        #region [ DB204003 - Гэрээ шинээр нэмэх ]
        public static Result DB204003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong seq = EServ.Interface.Sequence.NextByVal("ContractID");

                //Orhoogoos function avah
                string pSeqNo = "000002";
                string ContractNumberFormat = HPro.Core.SystemProp.ContractNumberFormat;
                string CONTRACTNO, TEMPCONTRACTNO, pYear = Static.SubStr(HPro.Core.SystemProp.TxnDate.ToString(), 0, 4);
                string pBranch = Static.ToStr(pParam[3]);

                #region [ Set CONTRACTNO ]
                /*
                C-[BBBB][YYYY][SSSSSS]
                ][
                0- C-[BBBB
                    [
                    0- C-
                    1- BBBB
                1- YYYY
                2- SSSSSS]
                    ]
                    0- SSSSSS
                */
                string[] ContractNumberFormatSplit1 = ContractNumberFormat.Split("][".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                CONTRACTNO = ContractNumberFormatSplit1[0];

                if (ContractNumberFormatSplit1[1] == "BBBB") CONTRACTNO += pBranch;
                if (ContractNumberFormatSplit1[1] == "YYYY") CONTRACTNO += pYear;
                if (ContractNumberFormatSplit1[1] == "SSSSSS") CONTRACTNO += pSeqNo;

                if (ContractNumberFormatSplit1[2] == "BBBB") CONTRACTNO += pBranch;
                if (ContractNumberFormatSplit1[2] == "YYYY")    CONTRACTNO += pYear;
                if (ContractNumberFormatSplit1[2] == "SSSSSS") CONTRACTNO += pSeqNo;

                if (ContractNumberFormatSplit1[3] == "BBBB") CONTRACTNO += pBranch;
                if (ContractNumberFormatSplit1[3] == "YYYY") CONTRACTNO += pYear;
                if (ContractNumberFormatSplit1[3] == "SSSSSS") CONTRACTNO += pSeqNo;
                #endregion
                
                #region [ Set TEMPCONTRACTNO]
                ContractNumberFormat = HPro.Core.SystemProp.DefaultNumberFormat;
                ContractNumberFormatSplit1 = ContractNumberFormat.Split("][".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                TEMPCONTRACTNO = ContractNumberFormatSplit1[0];

                if (ContractNumberFormatSplit1[1] == "BBBB") TEMPCONTRACTNO += pBranch;
                if (ContractNumberFormatSplit1[1] == "YYYY") TEMPCONTRACTNO += pYear;
                if (ContractNumberFormatSplit1[1] == "SSSSSS") TEMPCONTRACTNO += pSeqNo;

                if (ContractNumberFormatSplit1[2] == "BBBB") TEMPCONTRACTNO += pBranch;
                if (ContractNumberFormatSplit1[2] == "YYYY") TEMPCONTRACTNO += pYear;
                if (ContractNumberFormatSplit1[2] == "SSSSSS") TEMPCONTRACTNO += pSeqNo;

                if (ContractNumberFormatSplit1[3] == "BBBB") TEMPCONTRACTNO += pBranch;
                if (ContractNumberFormatSplit1[3] == "YYYY") TEMPCONTRACTNO += pYear;
                if (ContractNumberFormatSplit1[3] == "SSSSSS") TEMPCONTRACTNO += pSeqNo;
                #endregion
                
                pParam[0] = Static.ToStr(seq);
                pParam[1] = TEMPCONTRACTNO;
                pParam[2] = CONTRACTNO;

                string sql =
@"INSERT INTO CONTRACT(ContractID, TEMPCONTRACTNO, CONTRACTNO, BRANCH, CUSTNO, REQNO, STARTDATE, ENDDATE, ORGENDDATE, CREATEUSER,
CREATEDATE, APPROVEDATE, APPROVEUSER, ASSIGNUSER, CONTRACTUSER, VALUTIONAMOUNT, VALUTIONCURRENCY, FEEAMOUNT, FEECURRENCY, CONTRACTTYPE,
ISCHANGED, STATUS, CLOSESTATUS, CLOSEDTYPE, CLOSEDNOTE, CLOSEDDATE, ISEXTENDED, EXTENDCOUNT, EXTENDDATE, CHANGEDUSER)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23, :24, :25, :26, :27, :28, :29, :30)";

                WriteDBLog(sql, pParam, 30);

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204003", pParam);

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
        #region [ DB204004 - Гэрээ засварлах ]
        public static Result DB204004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTRACT SET
TEMPCONTRACTNO=:2, CONTRACTNO=:3, BRANCH=:4, CUSTNO=:5, REQNO=:6, STARTDATE=:7, ENDDATE=:8, ORGENDDATE=:9, CREATEUSER=:10,
CREATEDATE=:11, APPROVEDATE=:12, APPROVEUSER=:13, ASSIGNUSER=:14, CONTRACTUSER=:15, VALUTIONAMOUNT=:16, VALUTIONCURRENCY=:17, FEEAMOUNT=:18, FEECURRENCY=:19, CONTRACTTYPE=:20,
ISCHANGED=:21, STATUS=:22, CLOSESTATUS=:23, CLOSEDTYPE=:24, CLOSEDNOTE=:25, CLOSEDDATE=:26, ISEXTENDED=:27, EXTENDCOUNT=:28, EXTENDDATE=:29, CHANGEDUSER=:30
WHERE CONTRACTID=:1";

                WriteDBLog(sql, pParam, 30);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204004", pParam);

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
        #region [ DB204005 - Гэрээ устгах ]
        public static Result DB204005(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CONTRACT WHERE CONTRACTID=:1";

                WriteDBLog(sql, new object[] { pNo }, 1);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204005", pNo);

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

        #region [ DB204006 - Нөхөн төлбөр хүлээн авагч жагсаалт авах ]
        public static Result DB204006(DbConnections pDB, int pagenumber, int pagecount, long pContractID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CONTRACTID, SEQNO, CustNo, FamTypeCode, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO
FROM CONTRACTRECV
WHERE CONTRACTID=:1
ORDER BY CONTRACTID, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204006", pContractID);

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
        #region [ DB204007 - Нөхөн төлбөр хүлээн авагч дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204007(DbConnections pDB, long pContractID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CONTRACTID, SEQNO, CustNo, FamTypeCode, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO
FROM CONTRACTRECV
WHERE CONTRACTID=:1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204007", pContractID, SeqNo);

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
        #region [ DB204008 - Нөхөн төлбөр хүлээн авагч шинээр нэмэх ]
        public static Result DB204008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");

                pParam[1] = Static.ToStr(SeqNo);

                string sql =
@"INSERT INTO CONTRACTRECV(CONTRACTID, SEQNO, CustNo, FamTypeCode, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204008", pParam);

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
        #region [ DB204009 - Нөхөн төлбөр хүлээн авагч засварлах ]
        public static Result DB204009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTRACTRECV SET
CustNo=:3, FamTypeCode=:4, FIRSTNAME=:5, LASTNAME=:6, MIDDLENAME=:7, REGISTERNO=:8, PASSNO=:9
WHERE CONTRACTID = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204009", pParam);

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
        #region [ DB204010 - Нөхөн төлбөр хүлээн авагч устгах ]
        public static Result DB204010(DbConnections pDB, long pContractID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CONTRACTRECV WHERE CONTRACTID = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204010", pContractID, SeqNo);

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

        #region [ DB204016 - Гэрээний хийсэн бүтээгдэхүүний жагсаалт авах ]
        public static Result DB204016(DbConnections pDB, int pagenumber, int pagecount, long pContractID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CONTRACTID, PRODCODE, CHARGEMETHOD, DEFAULTFEE, FEECURRENCY, CHANGECHARGE, DISCOUNTRATE, CHANGERISK, LIFETIME, FORMULAEDIT, 
FORMULAID
FROM CONTRACTPRODUCT
WHERE CONTRACTID=:1
ORDER BY CONTRACTID, ProdCode";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204016", pContractID);

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
        #region [ DB204017 - Гэрээний хийсэн бүтээгдэхүүний дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204017(DbConnections pDB, long pContractID, long ProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CONTRACTID, PRODCODE, CHARGEMETHOD, DEFAULTFEE, FEECURRENCY, CHANGECHARGE, DISCOUNTRATE, CHANGERISK, LIFETIME, FORMULAEDIT, 
FORMULAID
FROM CONTRACTPRODUCT
WHERE CONTRACTID=:1 AND ProdCode =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204017", pContractID, ProdCode);

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
        #region [ DB204018 - Гэрээний хийсэн бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB204018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");

                pParam[1] = Static.ToStr(SeqNo);

                string sql =
@"INSERT INTO CONTRACTPRODUCT(CONTRACTID, PRODCODE, CHARGEMETHOD, DEFAULTFEE, FEECURRENCY, CHANGECHARGE, DISCOUNTRATE, CHANGERISK, LIFETIME, FORMULAEDIT, 
FORMULAID)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204018", pParam);

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
        #region [ DB204019 - Гэрээний хийсэн бүтээгдэхүүн засварлах ]
        public static Result DB204019(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTRACTPRODUCT SET
PRODCODE=:2, CHARGEMETHOD=:3, DEFAULTFEE=:4, FEECURRENCY=:5, CHANGECHARGE=:6, DISCOUNTRATE=:7, CHANGERISK=:8, LIFETIME=:9, FORMULAEDIT=:10, 
FORMULAID=:11
WHERE CONTRACTID = :1 AND PRODCODE =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204019", pParam);

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
        #region [ DB204020 - Гэрээний хийсэн бүтээгдэхүүн устгах ]
        public static Result DB204020(DbConnections pDB, long pContractID, long ProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CONTRACTPRODUCT WHERE CONTRACTID = :1 AND PRODCODE =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204020", pContractID, ProdCode);

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

        #region [ DB204021 - Гэрээг зөвшөөрөх ]
        public static Result DB204021(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTRACT SET
APPROVEDATE=:2, APPROVEUSER=:3, Status=1
WHERE CONTRACTID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204021", pParam);

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
        #region [ DB204022 - Гэрээг цуцлах ]
        public static Result DB204022(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTRACT SET
APPROVEDATE=:2, APPROVEUSER=:3, Status=1
WHERE CONTRACTID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204022", pParam);

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
        #region [ DB204023 - Гэрээг сунгах ]
        public static Result DB204023(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTRACT SET
APPROVEDATE=:2, APPROVEUSER=:3
WHERE CONTRACTID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204023", pParam);

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
        #region [ DB204024 - Гэрээг хаах ]
        public static Result DB204024(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTRACT SET
CloseDDate=:2, ChangedUser=:3, ClosedType=:4, ClosedNote=:5, CLOSESTATUS=9
WHERE CONTRACTID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204024", pParam);

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
        #region [ DB204025 - Гэрээг сунгах захиалга өгөх ]
        public static Result DB204025(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTRACT SET
APPROVEDATE=:2, APPROVEUSER=:3
WHERE CONTRACTID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204025", pParam);

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
        #endregion
        #region [ DB204 - Insurance ]
        #region [ DB204051 - Даатгалын үндсэн мэдээллийн жагсаалт авах ]
        public static Result DB204051(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where;
                where = " WHERE 1 = 1 ";

                if (pParam != null)
                {
                    if (pParam[0] != null & pParam[0] != "") where += " AND InsuranceNo='" + Static.ToStr(pParam[0]) + "'";
                    if (pParam[1] != null & pParam[1] != "") where += " AND ProdCode like '" + Static.ToStr(pParam[1]) + "'%";
                    if (pParam[2] != null & pParam[2] != "") where += " AND (ContractNo like '%" + Static.ToStr(pParam[2]) + "%'" + " OR ContractID like '%" + Static.ToStr(pParam[2]) + "%')";
                    if (pParam[3] != null & pParam[3] != "") where += " AND CreateDate='" + Static.ToStr(pParam[3]) + "'";
                    if (pParam[4] != null & pParam[4] != "") where += " AND StateDate='" + Static.ToStr(pParam[4]) + "'";
                    if (pParam[5] != null & pParam[5] != "") where += " AND EndDate='" + Static.ToStr(pParam[5]) + "'";
                    if (pParam[6] != null & pParam[6] != "") where += " AND OrgEndDate='" + Static.ToStr(pParam[6]) + "'";
                    if (pParam[7] != null & pParam[7] != "") where += " AND CreateUser='" + Static.ToStr(pParam[7]) + "'";
                    if (pParam[8] != null & pParam[8] != "") where += " AND EstimateAmount='" + Static.ToStr(pParam[8]) + "'";
                    if (pParam[9] != null & pParam[9] != "") where += " AND CurCode='" + Static.ToStr(pParam[9]) + "'";
                    if (pParam[10] != null & pParam[10] != "") where += " AND ReqNo='" + Static.ToStr(pParam[10]) + "'";
                    if (pParam[11] != null & pParam[11] != "") where += " AND RecvAccountNo='" + Static.ToStr(pParam[11]) + "'";
                    if (pParam[12] != null & pParam[12] != "") where += " AND PaybAccountNo='" + Static.ToStr(pParam[12]) + "'";
                    if (pParam[13] != null & pParam[13] != "") where += " AND FirstPaymentDate='" + Static.ToStr(pParam[13]) + "'";
                    if (pParam[14] != null & pParam[14] != "") where += " AND FirstPaymentAmount='" + Static.ToStr(pParam[14]) + "'";
                    if (pParam[15] != null & pParam[15] != "") where += " AND NextPayDate='" + Static.ToStr(pParam[15]) + "'";
                    if (pParam[16] != null & pParam[16] != "") where += " AND NextAmountPayment='" + Static.ToStr(pParam[16]) + "'";
                    if (pParam[17] != null & pParam[17] != "") where += " AND WhichDate='" + Static.ToStr(pParam[17]) + "'";
                    if (pParam[18] != null & pParam[18] != "") where += " AND isExtended='" + Static.ToStr(pParam[18]) + "'";
                    if (pParam[19] != null & pParam[19] != "") where += " AND ExtendCount='" + Static.ToStr(pParam[19]) + "'";
                    if (pParam[20] != null & pParam[20] != "") where += " AND isNRS='" + Static.ToStr(pParam[20]) + "'";
                    if (pParam[21] != null & pParam[21] != "") where += " AND DiscountRate='" + Static.ToStr(pParam[21]) + "'";
                    if (pParam[22] != null & pParam[22] != "") where += " AND FeeRate='" + Static.ToStr(pParam[22]) + "'";
                    if (pParam[23] != null & pParam[23] != "") where += " AND FeeAmount='" + Static.ToStr(pParam[23]) + "'";
                    if (pParam[24] != null & pParam[24] != "") where += " AND FeeCurCode='" + Static.ToStr(pParam[24]) + "'";
                    if (pParam[25] != null & pParam[25] != "") where += " AND AddFeeAmount='" + Static.ToStr(pParam[25]) + "'";
                    if (pParam[26] != null & pParam[26] != "") where += " AND AddFeeCurrency='" + Static.ToStr(pParam[26]) + "'";
                    if (pParam[27] != null & pParam[27] != "") where += " AND OTH='" + Static.ToStr(pParam[27]) + "'";

                    WriteDBLog("\nLIST WHERE\n" + where, pParam, 28);
                }

                sql =
@"SELECT InsuranceNo, Prodcode, ContractID, Status, ContractNo, StartDate, EndDate, WhichDate, EstimateAmount, CurCode,
FeeAmount, FeeCurCode, Deductivable, RecvAccountNo, PaybAccountNo, ReqNo, Branch, CreateUser, CreateDate, isReInsurance,
isExtended, isOrderExtend, custno
FROM Insurance " + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204051", null);

                if (res.AffectedRows == 0)
                {
                    res.ResultNo = 9110013;
                    res.ResultDesc="Даатгал олдсонгүй";
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
        #region [ DB204052 - Даатгалын үндсэн мэдээллийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204052(DbConnections pDB, long pInsuranceNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT InsuranceNo, Prodcode, ContractID, Status, ContractNo, StartDate, EndDate, WhichDate,
EstimateAmount, CurCode, FeeAmount, FeeCurCode, Deductivable, RecvAccountNo, PaybAccountNo, 
ReqNo, Branch, CreateUser, CreateDate, isReInsurance, isExtended, isOrderExtend, custno
FROM Insurance
where InsuranceNo = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204052", pInsuranceNo);

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
        #region [ DB204053 - Даатгалын үндсэн мэдээлэл шинээр нэмэх ]
        public static Result DB204053(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong seq = EServ.Interface.Sequence.NextByVal("InsuranceNo");

                pParam[0] = Static.ToStr(seq);

                string sql =
@"INSERT INTO Insurance(InsuranceNo, Prodcode, ContractID, Status, ContractNo, StartDate, EndDate, WhichDate, EstimateAmount, CurCode,
FeeAmount, FeeCurCode, Deductivable, RecvAccountNo, PaybAccountNo, ReqNo, Branch, CreateUser, CreateDate, isReInsurance,
isExtended, isOrderExtend, custno)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23)";

                WriteDBLog(sql, pParam, 22);

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204053", pParam);

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
        #region [ DB204054 - Даатгалын үндсэн мэдээлэл засварлах ]
        public static Result DB204054(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE Insurance SET
Prodcode=:2, ContractID=:3, Status=:4, ContractNo=:5, StartDate=:6, EndDate=:7, WhichDate=:8, EstimateAmount=:9, CurCode=:10,
FeeAmount=:11, FeeCurCode=:12, Deductivable=:13, RecvAccountNo=:14, PaybAccountNo=:15, ReqNo=:16, Branch=:17, CreateUser=:18, CreateDate=:19, isReInsurance=:20,
isExtended=:21, isOrderExtend=:22, custno=:23
WHERE InsuranceNo=:1";

                WriteDBLog(sql, pParam, 22);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204054", pParam);

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
        #region [ DB204055 - Даатгалын үндсэн мэдээлэл устгах ]
        public static Result DB204055(DbConnections pDB, long pInsuranceNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM Insurance WHERE InsuranceNo=:1";

                //WriteDBLog(sql, new object[] { pNo }, 1);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204055", pInsuranceNo);

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

        #region [ DB204066 - Нөхөн төлбөр хүлээн авагч жагсаалт авах ]
        public static Result DB204066(DbConnections pDB, int pagenumber, int pagecount, long pInsuranceNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INSURANCENO, SEQNO, CustNo, FamTypeCode, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO
FROM INSURANCERECV
WHERE INSURANCENO=:1
ORDER BY INSURANCENO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204066", pInsuranceNo);

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
        #region [ DB204067 - Нөхөн төлбөр хүлээн авагч дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204067(DbConnections pDB, long pInsuranceNo, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INSURANCENO, SEQNO, CustNo, FamTypeCode, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO
FROM INSURANCERECV
WHERE INSURANCENO=:1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204067", pInsuranceNo, SeqNo);

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
        #region [ DB204068 - Нөхөн төлбөр хүлээн авагч шинээр нэмэх ]
        public static Result DB204068(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");

                pParam[1] = Static.ToStr(SeqNo);

                string sql =
@"INSERT INTO INSURANCERECV(INSURANCENO, SEQNO, CustNo, FamTypeCode, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204068", pParam);

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
        #region [ DB204069 - Нөхөн төлбөр хүлээн авагч засварлах ]
        public static Result DB204069(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE INSURANCERECV SET
CustNo=:3, FamTypeCode=:4, FIRSTNAME=:5, LASTNAME=:6, MIDDLENAME=:7, REGISTERNO=:8, PASSNO=:9
WHERE INSURANCENO = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204069", pParam);

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
        #region [ DB204070 - Нөхөн төлбөр хүлээн авагч устгах ]
        public static Result DB204070(DbConnections pDB, long pInsuranceNo, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM INSURANCERECV WHERE INSURANCENO = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204070", pInsuranceNo, SeqNo);

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

        #region [ DB204071 - Төлбөрийн графикийн жагсаалт авах ]
        public static Result DB204071(DbConnections pDB, int pagenumber, int pagecount, long pInsuranceNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INSURANCENO, SEQNO, SCHDATE, AMOUNT
FROM SCHEDULE
WHERE INSURANCENO=:1
ORDER BY INSURANCENO, SchDate";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204071", pInsuranceNo);

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
        #region [ DB204072 - Төлбөрийн графикийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204072(DbConnections pDB, long pInsuranceNo, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INSURANCENO, SEQNO, SCHDATE, AMOUNT
FROM SCHEDULE
WHERE INSURANCENO=:1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204072", pInsuranceNo, SeqNo);

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
        #region [ DB204073 - Төлбөрийн графикийг шинээр нэмэх ]
        public static Result DB204073(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");

                pParam[1] = Static.ToStr(SeqNo);

                string sql =
@"INSERT INTO SCHEDULE(INSURANCENO, SEQNO, SCHDATE, AMOUNT)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204073", pParam);

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
        #region [ DB204074 - Төлбөрийн графикийг засварлах ]
        public static Result DB204074(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SCHEDULE SET
SCHDATE=:3, AMOUNT=:4
WHERE INSURANCENO = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204074", pParam);

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
        #region [ DB204075 - Төлбөрийн графикийг устгах ]
        public static Result DB204075(DbConnections pDB, long pInsuranceNo, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM SCHEDULE WHERE INSURANCENO = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204075", pInsuranceNo, SeqNo);

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

        #region [ DB204086 - Даатгалыг зөвшөөрөх ]
        public static Result DB204086(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE Insurance SET
APPROVEDATE=:2, APPROVEUSER=:3, Status=2
WHERE InsuranceNo = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204086", pParam);

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
        #region [ DB204087 - Даатгалыг хаах ]
        public static Result DB204087(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE Insurance SET
CloseDDate=:2, ChangedUser=:3, ClosedType=:4, ClosedNote=:5, CLOSESTATUS=9
WHERE InsuranceNo = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204087", pParam);

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
        #endregion
        #region [ DB204 - Claim ]
        #region [ DB204121 - Даатгалын нөхөн төлбөрийн үндсэн мэдээллийн жагсаалт авах ]
        public static Result DB204121(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where = " ";
                /*if (pParam != null)
                {
                    where = " WHERE 1 = 1 ";
                    if (pParam[0] != null & pParam[0] != "") where += " AND InsuranceNo='" + Static.ToStr(pParam[0]) + "'";
                    if (pParam[1] != null & pParam[1] != "") where += " AND ProdCode like '" + Static.ToStr(pParam[1]) + "'%";
                    if (pParam[2] != null & pParam[2] != "") where += " AND (ContractNo like '%" + Static.ToStr(pParam[2]) + "%'" + " OR ContractID like '%" + Static.ToStr(pParam[2]) + "%')";
                    if (pParam[3] != null & pParam[3] != "") where += " AND CreateDate='" + Static.ToStr(pParam[3]) + "'";
                    if (pParam[4] != null & pParam[4] != "") where += " AND StateDate='" + Static.ToStr(pParam[4]) + "'";
                    if (pParam[5] != null & pParam[5] != "") where += " AND EndDate='" + Static.ToStr(pParam[5]) + "'";
                    if (pParam[6] != null & pParam[6] != "") where += " AND OrgEndDate='" + Static.ToStr(pParam[6]) + "'";
                    if (pParam[7] != null & pParam[7] != "") where += " AND CreateUser='" + Static.ToStr(pParam[7]) + "'";
                    if (pParam[8] != null & pParam[8] != "") where += " AND EstimateAmount='" + Static.ToStr(pParam[8]) + "'";
                    if (pParam[9] != null & pParam[9] != "") where += " AND CurCode='" + Static.ToStr(pParam[9]) + "'";
                    WriteDBLog("\nLIST WHERE\n" + where, pParam, 28);
                }*/

                sql =
@"SELECT reqno, claimid, contractid, contractno, insuranceno, custno, riskid, GuiltyPerson, LossInsurancePercent, isApplication,
isCallPolicy, isLanded, ClaimServiceDate, Branch, Status, CallCreateDate, CallUserNo, AssignDate, AssignUser, ResolveType, 
ResolveDate, ResolutionNo, ResolutionDate
FROM CLAIM" + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204121", null);
                if (res.AffectedRows == 0)
                {
                    res.ResultNo = 9110013;
                    res.ResultDesc = "Даатгал олдсонгүй";
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
        #region [ DB204122 - Даатгалын нөхөн төлбөрийн үндсэн мэдээллийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204122(DbConnections pDB, long pClaimID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT reqno, claimid, contractid, contractno, insuranceno, custno, riskid, GuiltyPerson, LossInsurancePercent, isApplication,
isCallPolicy, isLanded, ClaimServiceDate, Branch, Status, CallCreateDate, CallUserNo, AssignDate, AssignUser, ResolveType, 
ResolveDate, ResolutionNo, ResolutionDate
FROM CLAIM
where ClaimID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204122", pClaimID);

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
        #region [ DB204123 - Даатгалын нөхөн төлбөрийн үндсэн мэдээллийг шинээр нэмэх ]
        public static Result DB204123(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong ClaimID = EServ.Interface.Sequence.NextByVal("ClaimID");

                pParam[1] = Static.ToStr(ClaimID);
                
                string sql =
@"INSERT INTO CLAIM(reqno, claimid, contractid, contractno, insuranceno, custno, riskid, GuiltyPerson, LossInsurancePercent, isApplication,
isCallPolicy, isLanded, ClaimServiceDate, Branch, Status, CallCreateDate, CallUserNo, AssignDate, AssignUser, ResolveType, 
ResolveDate, ResolutionNo, ResolutionDate)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23)";
                WriteDBLog(sql, pParam, 23);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204123", pParam);
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
        #region [ DB204124 - Даатгалын нөхөн төлбөрийн үндсэн мэдээллийг засварлах ]
        public static Result DB204124(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CLAIM SET
reqno=:1, contractid=:3, contractno=:4, insuranceno=:5, custno=:6, riskid=:7, GuiltyPerson=:8, LossInsurancePercent=:9, isApplication=:10,
isCallPolicy=:11, isLanded=:12, ClaimServiceDate=:13, Branch=:14, Status=:15, CallCreateDate=:16, CallUserNo=:17, AssignDate=:18, AssignUser=:19, ResolveType=:20,
ResolveDate=:21, ResolutionNo=:22, ResolutionDate=:23
WHERE ClaimID=:2";
                WriteDBLog(sql, pParam, 23);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204124", pParam);
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
        #region [ DB204125 - Даатгалын нөхөн төлбөрийн үндсэн мэдээллийг устгах ]
        public static Result DB204125(DbConnections pDB, long pClaimID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM Claim WHERE ClaimID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204125", pClaimID);
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
        
        #region [/**/]
        /*
        #region [ DB204126 - Даатгалын нөхөн төлбөрийн хохирлын жагсаалт авах ]
        public static Result DB204126(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where = " ";

                sql =
@"SELECT reqno, claimid, contractid, contractno, insuranceno, custno, riskid, GuiltyPerson, LossInsurancePercent, isApplication,
isCallPolicy, isLanded, ClaimServiceDate, Branch, Status, CallCreateDate, CallUserNo, AssignDate, AssignUser, ResolveType, 
ResolveDate, ResolutionNo, ResolutionDate
FROM CLAIM" + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204121", null);
                if (res.AffectedRows == 0)
                {
                    res.ResultNo = 9110013;
                    res.ResultDesc = "Даатгал олдсонгүй";
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
        #region [ DB204127 - Даатгалын нөхөн төлбөрийн хохирлын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204127(DbConnections pDB, long pClaimID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT reqno, claimid, contractid, contractno, insuranceno, custno, riskid, GuiltyPerson, LossInsurancePercent, isApplication,
isCallPolicy, isLanded, ClaimServiceDate, Branch, Status, CallCreateDate, CallUserNo, AssignDate, AssignUser, ResolveType, 
ResolveDate, ResolutionNo, ResolutionDate
FROM CLAIM
where ClaimID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204122", pClaimID);

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
        #region [ DB204128 - Даатгалын нөхөн төлбөрийн хохирлыг шинээр нэмэх ]
        public static Result DB204128(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong ClaimID = EServ.Interface.Sequence.NextByVal("ClaimID");

                pParam[1] = Static.ToStr(ClaimID);

                string sql =
@"INSERT INTO CLAIM(reqno, claimid, contractid, contractno, insuranceno, custno, riskid, GuiltyPerson, LossInsurancePercent, isApplication,
isCallPolicy, isLanded, ClaimServiceDate, Branch, Status, CallCreateDate, CallUserNo, AssignDate, AssignUser, ResolveType, 
ResolveDate, ResolutionNo, ResolutionDate)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23)";
                WriteDBLog(sql, pParam, 23);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204123", pParam);
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
        #region [ DB204129 - Даатгалын нөхөн төлбөрийн хохирлыг засварлах ]
        public static Result DB204129(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CLAIM SET
reqno=:1, contractid=:3, contractno=:4, insuranceno=:5, custno=:6, riskid=:7, GuiltyPerson=:8, LossInsurancePercent=:9, isApplication=:10,
isCallPolicy=:11, isLanded=:12, ClaimServiceDate=:13, Branch=:14, Status=:15, CallCreateDate=:16, CallUserNo=:17, AssignDate=:18, AssignUser=:19, ResolveType=:20,
ResolveDate=:21, ResolutionNo=:22, ResolutionDate=:23
WHERE ClaimID=:2";
                WriteDBLog(sql, pParam, 23);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204124", pParam);
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
        #region [ DB204130 - Даатгалын нөхөн төлбөрийн хохирлыг устгах ]
        public static Result DB204130(DbConnections pDB, long pClaimID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM Claim WHERE ClaimID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204125", pClaimID);
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
        */
        #endregion

        #region [ DB204131 - Даатгалын нөхөн төлбөрийн зардлын жагсаалт авах ]
        public static Result DB204131(DbConnections pDB, int pagenumber, int pagecount, long pClaimID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"SELECT CLAIMID, SEQNO, EXPENSETYPE, AMOUNT, CURCODE, NOTE
FROM CLAIMEXPENSE
WHERE CLAIMID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204131", pClaimID);
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
        #region [ DB204132 - Даатгалын нөхөн төлбөрийн зардлын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204132(DbConnections pDB, long pClaimID, long pSeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CLAIMID, SEQNO, EXPENSETYPE, AMOUNT, CURCODE, NOTE
FROM CLAIMEXPENSE
where ClaimID = :1 and SEQNO=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204132", pClaimID, pSeqNo);

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
        #region [ DB204133 - Даатгалын нөхөн төлбөрийн зардлыг шинээр нэмэх ]
        public static Result DB204133(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");

                pParam[1] = Static.ToStr(SeqNo);

                string sql =
@"INSERT INTO CLAIMEXPENSE(CLAIMID, SEQNO, EXPENSETYPE, AMOUNT, CURCODE, NOTE)
VALUES(:1, :2, :3, :4, :5, :6)";
                WriteDBLog(sql, pParam, 6);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204133", pParam);
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
        #region [ DB204134 - Даатгалын нөхөн төлбөрийн зардлыг засварлах ]
        public static Result DB204134(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CLAIMEXPENSE SET
EXPENSETYPE=:3, AMOUNT=:4, CURCODE=:5, NOTE=:6
WHERE ClaimID=:1 and SEQNO=:2";
                WriteDBLog(sql, pParam, 6);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204134", pParam);
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
        #region [ DB204135 - Даатгалын нөхөн төлбөрийн зардлыг устгах ]
        public static Result DB204135(DbConnections pDB, long pClaimID, long pSeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CLAIMEXPENSE WHERE ClaimID=:1 and SEQNO=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204135", pClaimID, pSeqNo);
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

        #region [ DB204141 - Даатгалын нөхөн төлбөрийн буруутай этгээдийн жагсаалт авах ]
        public static Result DB204141(DbConnections pDB, int pagenumber, int pagecount, long pClaimID)
        {
            Result res = new Result();
            try
            {
                string sql, where = " ";

                sql =
@"SELECT CLAIMID, SEQNO, CUSTTYPE, FNAME, LNAME, REGISTERNO, FAX, PASSNO, DRIVENO, EMAIL, 
JOBPHONE, HOMEPHONE, MOBILE, ADDRESS, JOB, POSITION, ACCOUNTNO
FROM CLAIMGUILTY
where ClaimID = :1" + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204141", pClaimID);
                if (res.AffectedRows == 0)
                {
                    res.ResultNo = 9110013;
                    res.ResultDesc = "Даатгал олдсонгүй";
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
        #region [ DB204142 - Даатгалын нөхөн төлбөрийн буруутай этгээдийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204142(DbConnections pDB, long pClaimID, long pSeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CLAIMID, SEQNO, CUSTTYPE, FNAME, LNAME, REGISTERNO, FAX, PASSNO, DRIVENO, EMAIL, 
JOBPHONE, HOMEPHONE, MOBILE, ADDRESS, JOB, POSITION, ACCOUNTNO
FROM CLAIMGUILTY
where ClaimID = :1 and SeqNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204142", pClaimID, pSeqNo);

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
        #region [ DB204143 - Даатгалын нөхөн төлбөрийн буруутай этгээдийн шинээр нэмэх ]
        public static Result DB204143(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");

                pParam[1] = Static.ToStr(SeqNo);

                string sql =
@"INSERT INTO CLAIMGUILTY(CLAIMID, SEQNO, CUSTTYPE, FNAME, LNAME, REGISTERNO, FAX, PASSNO, DRIVENO, EMAIL, 
JOBPHONE, HOMEPHONE, MOBILE, ADDRESS, JOB, POSITION, ACCOUNTNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17)";
                WriteDBLog(sql, pParam, 17);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204143", pParam);
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
        #region [ DB204144 - Даатгалын нөхөн төлбөрийн буруутай этгээдийн засварлах ]
        public static Result DB204144(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CLAIMGUILTY SET
CUSTTYPE=:3, FNAME=:4, LNAME=:5, REGISTERNO=:6, FAX=:7, PASSNO=:8, DRIVENO=:9, EMAIL=:10, 
JOBPHONE=:11, HOMEPHONE=:12, MOBILE=:13, ADDRESS=:14, JOB=:15, POSITION=:16, ACCOUNTNO=:17
WHERE ClaimID = :1 and SeqNo=:2 ";
                WriteDBLog(sql, pParam, 17);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204144", pParam);
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
        #region [ DB204145 - Даатгалын нөхөн төлбөрийн буруутай этгээдийн устгах ]
        public static Result DB204145(DbConnections pDB, long pClaimID, long pSeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CLAIMGUILTY WHERE ClaimID = :1 and SeqNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204145", pClaimID, pSeqNo);
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

        #region [ DB204146 - Даатгалын нөхөн төлбөрийн татгалзсан шалтгааны мэдээлэл авах ]
        public static Result DB204146(DbConnections pDB, long pClaimID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select ClaimID, ReqNo, ResolveNote from CLAIM WHERE ClaimID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204146", pClaimID);
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
        #region [ DB204147 - Даатгалын нөхөн төлбөрийн татгалзсан шалтгааны мэдээлэл засварлах ]
        public static Result DB204147(DbConnections pDB, long pClaimID, string pResolveNote)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CLAIM SET ResolveNote =:2 WHERE ClaimID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204147", pClaimID, pResolveNote);
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
        #endregion
        #region [ DB204 - ReContract ]
        #region [ DB204201 - Д/Д - ын гэрээний жагсаалт авах ]
        public static Result DB204201(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where = " ";


                /*if (pParam != null)
                {
                    where = " WHERE ";
                    if (pParam[0] != "" && pParam[0] != null) where += "  b.ContractID='" + Static.ToStr(pParam[0]) + "' AND ";
                    if (pParam[1] != "" && pParam[1] != null) where += "  b.ContractNo='" + Static.ToStr(pParam[1]) + "' AND ";
                    if (pParam[2] != "" && pParam[2] != null) where += "  b.CustNo='" + Static.ToStr(pParam[2]) + "' AND ";
                    if (pParam[3] != "" && pParam[3] != null) where += "  b.CreateDate='" + Static.ToStr(pParam[3]) + "' AND ";
                    if (pParam[4] != "" && pParam[4] != null) where += "  b.ReqNo='" + Static.ToStr(pParam[4]) + "' AND ";
                    if (pParam[5] != "" && pParam[5] != null) where += "  b.CreateUser='" + Static.ToStr(pParam[5]) + "' AND ";
                    if (pParam[6] != "" && pParam[6] != null) where += "  b.AssignUser='" + Static.ToStr(pParam[6]) + "' AND ";
                    if (pParam[7] != "" && pParam[7] != null) where += "  b.Status='" + Static.ToStr(pParam[7]) + "' AND ";
                    if (pParam[8] != "" && pParam[8] != null) where += "  b.Approvedate='" + Static.ToStr(pParam[8]) + "' AND ";
                    if (pParam[9] != "" && pParam[9] != null) where += "  b.ContractUser='" + Static.ToStr(pParam[9]) + "' AND ";

                    where = where.Substring(0, where.Length - 4);

                    WriteDBLog("\nLIST WHERE\n" + where, pParam, 25);
                }*/

                sql =
@"SELECT REContractID, Branch, StartDate, EndDate, OrgEndDate, CreateUser, CreateDate, ApproveDate, ApproveUser, AssignUser, 
ContractUser, ContractType, isChanged, Status, BrokerName, RecvAccountNo, PaybAccountNo, isNRS, TxnDate
FROM REinsuranceCONTRACT
" + where + " Order by REContractID";

                EServ.Shared.Static.WriteToLogFile("Error.log", sql);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204201", null);

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
        #region [ DB204202 - Д/Д - ын гэрээний дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204202(DbConnections pDB, long pReContractID)
        {
            Result res = new Result();
            try
            {

                string sql =
@"SELECT REContractID, Branch, StartDate, EndDate, OrgEndDate, CreateUser, CreateDate, ApproveDate, ApproveUser, AssignUser, 
ContractUser, ContractType, isChanged, Status, BrokerName, RecvAccountNo, PaybAccountNo, isNRS, TxnDate
FROM REinsuranceCONTRACT
where REContractID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204202", pReContractID);

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
        #region [ DB204203 - Д/Д - ын гэрээ шинээр нэмэх ]
        public static Result DB204203(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong seq = EServ.Interface.Sequence.NextByVal("ReContractID");

                pParam[0] = seq;

                string sql =
@"INSERT INTO REinsuranceCONTRACT(REContractID, Branch, StartDate, EndDate, OrgEndDate, CreateUser, CreateDate, ApproveDate, ApproveUser, AssignUser, 
ContractUser, ContractType, isChanged, Status, BrokerName, RecvAccountNo, PaybAccountNo, isNRS, TxnDate)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19)";

                WriteDBLog(sql, pParam, 19);

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204203", pParam);

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
        #region [ DB204204 - Д/Д - ын гэрээ засварлах ]
        public static Result DB204204(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE REinsuranceCONTRACT SET
Branch=:2, StartDate=:3, EndDate=:4, OrgEndDate=:5, CreateUser=:6, CreateDate=:7, ApproveDate=:8, ApproveUser=:9, AssignUser=:10, 
ContractUser=:11, ContractType=:12, isChanged=:13, Status=:14, BrokerName=:15, RecvAccountNo=:16, PaybAccountNo=:17, isNRS=:18, TxnDate=:19
WHERE REContractID=:1";

                WriteDBLog(sql, pParam, 19);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204204", pParam);

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
        #region [ DB204205 - Д/Д - ын гэрээ устгах ]
        public static Result DB204205(DbConnections pDB, long pReContractID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM REinsuranceCONTRACT WHERE REContractID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204205", pReContractID);

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

        #region [ DB204206 - Төлбөрийн графикийн жагсаалт авах ]
        public static Result DB204206(DbConnections pDB, int pagenumber, int pagecount, long pReContractID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT RECONTRACTID, SEQNO, RISCHDATE, AMOUNT
FROM RISCHEDULE
WHERE RECONTRACTID=:1
order by RISCHDATE";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204206", pReContractID);

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
        #region [ DB204207 - Төлбөрийн графикийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204207(DbConnections pDB, long pReContractID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ReContractID, SEQNO, RISCHDATE, AMOUNT
FROM RISCHEDULE
WHERE ReContractID=:1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204207", pReContractID, SeqNo);

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
        #region [ DB204208 - Төлбөрийн графикийг шинээр нэмэх ]
        public static Result DB204208(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");

                pParam[1] = Static.ToStr(SeqNo);

                string sql =
@"INSERT INTO RISCHEDULE(ReContractID, SEQNO, RISCHDATE, AMOUNT)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204208", pParam);

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
        #region [ DB204209 - Төлбөрийн графикийг засварлах ]
        public static Result DB204209(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE RISCHEDULE SET
RISCHDATE=:3, AMOUNT=:4
WHERE ReContractID = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204209", pParam);

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
        #region [ DB204210 - Төлбөрийн графикийг устгах ]
        public static Result DB204210(DbConnections pDB, long pReContractID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM RISCHEDULE WHERE ReContractID = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204210", pReContractID, SeqNo);

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

        #region [ DB204211 - Д/Д-ын зүйл ба эрсдэлийн жагсаалт авах ]
        public static Result DB204211(DbConnections pDB, int pagenumber, int pagecount, long pReContractID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT REINSURANCENO, INSURANCENO, RECONTRACTID, CONTRACTID, CONTRACTNO, CUSTNO, CREATEUSER, CREATEDATE, STARTDATE, ENDDATE,
LOCATION, COVERAGE, OCCUPANCY, RIPRODUCTCODE, OBJECTID, OBJECTAMOUNT, OBJECTCURCODE, RETENTION, RIRID1, RIRID2,
RIRID3, REINSURERSHARE1, REINSURERSHARE2, REINSURERSHARE3, CONTRACTPREMIUM, REINSURANCEPREMIUM, CONTRACTAMOUNT, CONTRACTCURCODE, REINSURANCEAMOUNT, REINSURANCECURCODE,
INSUREDDEDUCTIONS, REINSUREDDEDUCTIONS, COMMISSION, REINSURANCERISKPREMIUMAMOUNT, REINSURANCERISKPREMIUMCURCODE, COMMISSIONAMOUNT, COMMISSIONCURCODE, REINSURERPAIDAMOUNT, REINSURERPAIDCURCODE, DIFFERENCEAMOUNT, 
DIFFERENCECURCODE, DIFFERENCEPREMIUM, OPTION1RETATIONAMOUNT, Option1RetationCurCode, OPTION1COMMISSIONAMOUNT, OPTION1COMMISSIONCURCODE, OPTION2RETATIONAMOUNT, Option2RetationCurCode, OPTION2COMMISSIONAMOUNT, OPTION2COMMISSIONCURCODE
FROM REinsurance
WHERE REContractID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204211", pReContractID);

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
        #region [ DB204212 - Д/Д-ын зүйл ба эрсдэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204212(DbConnections pDB, long pReInsuranceNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT REINSURANCENO, INSURANCENO, RECONTRACTID, CONTRACTID, CONTRACTNO, CUSTNO, CREATEUSER, CREATEDATE, STARTDATE, ENDDATE,
LOCATION, COVERAGE, OCCUPANCY, RIPRODUCTCODE, OBJECTID, OBJECTAMOUNT, OBJECTCURCODE, RETENTION, RIRID1, RIRID2,
RIRID3, REINSURERSHARE1, REINSURERSHARE2, REINSURERSHARE3, CONTRACTPREMIUM, REINSURANCEPREMIUM, CONTRACTAMOUNT, CONTRACTCURCODE, REINSURANCEAMOUNT, REINSURANCECURCODE,
INSUREDDEDUCTIONS, REINSUREDDEDUCTIONS, COMMISSION, REINSURANCERISKPREMIUMAMOUNT, REINSURANCERISKPREMIUMCURCODE, COMMISSIONAMOUNT, COMMISSIONCURCODE, REINSURERPAIDAMOUNT, REINSURERPAIDCURCODE, DIFFERENCEAMOUNT, 
DIFFERENCECURCODE, DIFFERENCEPREMIUM, OPTION1RETATIONAMOUNT, Option1RetationCurCode, OPTION1COMMISSIONAMOUNT, OPTION1COMMISSIONCURCODE, OPTION2RETATIONAMOUNT, Option2RetationCurCode, OPTION2COMMISSIONAMOUNT, OPTION2COMMISSIONCURCODE
FROM REinsurance
WHERE REINSURANCENO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204212", pReInsuranceNo);

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
        #region [ DB204213 - Д/Д-ын зүйл ба эрсдэлийг шинээр нэмэх ]
        public static Result DB204213(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                ulong SeqNo = EServ.Interface.Sequence.NextByVal("ReInsuranceNo");

                pParam[0] = Static.ToStr(SeqNo);

                string sql =
@"INSERT INTO REinsurance(REINSURANCENO, INSURANCENO, RECONTRACTID, CONTRACTID, CONTRACTNO, CUSTNO, CREATEUSER, CREATEDATE, STARTDATE, ENDDATE,
LOCATION, COVERAGE, OCCUPANCY, RIPRODUCTCODE, OBJECTID, OBJECTAMOUNT, OBJECTCURCODE, RETENTION, RIRID1, RIRID2,
RIRID3, REINSURERSHARE1, REINSURERSHARE2, REINSURERSHARE3, CONTRACTPREMIUM, REINSURANCEPREMIUM, CONTRACTAMOUNT, CONTRACTCURCODE, REINSURANCEAMOUNT, REINSURANCECURCODE,
INSUREDDEDUCTIONS, REINSUREDDEDUCTIONS, COMMISSION, REINSURANCERISKPREMIUMAMOUNT, REINSURANCERISKPREMIUMCURCODE, COMMISSIONAMOUNT, COMMISSIONCURCODE, REINSURERPAIDAMOUNT, REINSURERPAIDCURCODE, DIFFERENCEAMOUNT, 
DIFFERENCECURCODE, DIFFERENCEPREMIUM, OPTION1RETATIONAMOUNT, Option1RetationCurCode, OPTION1COMMISSIONAMOUNT, OPTION1COMMISSIONCURCODE, OPTION2RETATIONAMOUNT, Option2RetationCurCode, OPTION2COMMISSIONAMOUNT, OPTION2COMMISSIONCURCODE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20, 
:21, :22, :23, :24, :25, :26, :27, :28, :29, :30, 
:31, :32, :33, :34, :35, :36, :37, :38, :39, :40,
:41, :42, :43, :44, :45, :46, :47, :48, :49, :50)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204213", pParam);

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
        #region [ DB204214 - Д/Д-ын зүйл ба эрсдэлийг засварлах ]
        public static Result DB204214(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE REinsurance SET
INSURANCENO=:2, RECONTRACTID=:3, CONTRACTID=:4, CONTRACTNO=:5, CUSTNO=:6, CREATEUSER=:7, CREATEDATE=:8, STARTDATE=:9, ENDDATE=:10,
LOCATION=:11, COVERAGE=:12, OCCUPANCY=:13, RIPRODUCTCODE=:14, OBJECTID=:15, OBJECTAMOUNT=:16, OBJECTCURCODE=:17, RETENTION=:18, RIRID1=:19, RIRID2=:20,
RIRID3=:21, REINSURERSHARE1=:22, REINSURERSHARE2=:23, REINSURERSHARE3=:24, CONTRACTPREMIUM=:25, REINSURANCEPREMIUM=:26, CONTRACTAMOUNT=:27, CONTRACTCURCODE=:28, REINSURANCEAMOUNT=:29, REINSURANCECURCODE=:30,
INSUREDDEDUCTIONS=:31, REINSUREDDEDUCTIONS=:32, COMMISSION=:33, REINSURANCERISKPREMIUMAMOUNT=:34, REINSURANCERISKPREMIUMCURCODE=:35, COMMISSIONAMOUNT=:36, COMMISSIONCURCODE=:37, REINSURERPAIDAMOUNT=:38, REINSURERPAIDCURCODE=:39, DIFFERENCEAMOUNT=:40, 
DIFFERENCECURCODE=:41, DIFFERENCEPREMIUM=:42, OPTION1RETATIONAMOUNT=:43, Option1RetationCurCode=:44, OPTION1COMMISSIONAMOUNT=:45, OPTION1COMMISSIONCURCODE=:46, OPTION2RETATIONAMOUNT=:47, Option2RetationCurCode=:48, OPTION2COMMISSIONAMOUNT=:49, OPTION2COMMISSIONCURCODE=:50
WHERE REINSURANCENO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204214", pParam);

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
        #region [ DB204215 - Д/Д-ын зүйл ба эрсдэлийг устгах ]
        public static Result DB204215(DbConnections pDB, long pReInsuranceNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM REinsurance WHERE REINSURANCENO=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204215", pReInsuranceNo);

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
        #endregion
        #endregion
        #region [ DB205 - Customer ]
        #region [ DB205001 - Харилцагч жагсаалт авах ]
        public static Result DB205001(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where;

                where = " WHERE 1 = 1 ";

                if (pParam != null)
                {
                    if (pParam[0] != "" && pParam[0] != null) where += " AND CustomerNo='" + Static.ToStr(pParam[0]) + "'";
                    if (pParam[1] != "" && pParam[1] != null) where += " AND ClassCode='" + Static.ToStr(pParam[1]) + "'";
                    if (pParam[2] != "" && pParam[2] != null) where += " AND BranchNo='" + Static.ToStr(pParam[2]) + "'";
                    if (pParam[3] != "" && pParam[3] != null) where += " AND Status='" + Static.ToStr(pParam[3]) + "'";
                    if (pParam[4] != "" && pParam[4] != null) where += " AND FirstName='" + Static.ToStr(pParam[4]) + "'";
                    if (pParam[5] != "" && pParam[5] != null) where += " AND LastName='" + Static.ToStr(pParam[5]) + "'";
                    if (pParam[6] != "" && pParam[6] != null) where += " AND RegisterNo='" + Static.ToStr(pParam[6]) + "'";
                    if (pParam[7] != "" && pParam[7] != null) where += " AND PassNo='" + Static.ToStr(pParam[7]) + "'";
                    if (pParam[8] != "" && pParam[8] != null) where += " AND Sex='" + Static.ToStr(pParam[8]) + "'";
                    if (pParam[9] != "" && pParam[9] != null) where += " AND isHInsurance='" + Static.ToStr(pParam[9]) + "'";
                    if (pParam[10] != "" && pParam[10] != null) where += " AND isSInsurance=" + Static.ToStr(pParam[10]) + "'";
                    if (pParam[11] != "" && pParam[11] != null) where += " AND TypeCode='" + Static.ToStr(pParam[11]) + "'";
                    if (pParam[12] != "" && pParam[12] != null) where += " AND CorporateName='" + Static.ToStr(pParam[12]) + "'";
                    if (pParam[13] != "" && pParam[13] != null) where += " AND InduTypeCode='" + Static.ToStr(pParam[13]) + "'";
                    if (pParam[14] != "" && pParam[14] != null) where += " AND InduSubTypeCode='" + Static.ToStr(pParam[14]) + "'";
                    if (pParam[15] != "" && pParam[15] != null) where += " AND DirFirstName='" + Static.ToStr(pParam[15]) + "'";
                    if (pParam[16] != "" && pParam[16] != null) where += " AND DirLastName='" + Static.ToStr(pParam[16]) + "'";
                    if (pParam[17] != "" && pParam[17] != null) where += " AND RateCode='" + Static.ToStr(pParam[17]) + "'";
                    if (pParam[18] != "" && pParam[18] != null) where += " AND isOtherInsurance='" + Static.ToStr(pParam[18]) + "'";
                    if (pParam[19] != "" && pParam[19] != null) where += " AND Email='" + Static.ToStr(pParam[19]) + "'";
                    if (pParam[20] != "" && pParam[20] != null) where += " AND Telephone='" + Static.ToStr(pParam[20]) + "'";
                    if (pParam[21] != "" && pParam[21] != null) where += " AND Mobile='" + Static.ToStr(pParam[21]) + "'";
                    if (pParam[22] != "" && pParam[22] != null) where += " AND HomePhone='" + Static.ToStr(pParam[22]) + "'";

                    WriteDBLog("\nLIST\n" + where, pParam, 23);
                }
                sql =
@"select CustomerNo, ClassCode, BranchNo, Status, FirstName, LastName, RegisterNo, PassNo, Sex, isHInsurance, isSInsurance, 
TypeCode, CorporateName, InduTypeCode, InduSubTypeCode, DirFirstName, DirLastName, RateCode, isOtherInsurance, 
Email, Telephone, Mobile, HomePhone, 
MiddleName, BirthDay, Company, Position, Experience, DirMiddleName, DirRegisterNo, DirPassNo, 
DirSex, DirBirthDay, Fax, WebSite, SpecialApproval, CountryCode, LanguageCode, CorporateName2
From Customer
" + where + "Order by CustomerNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205001", null);
                
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
        #region [ DB205002 - Харилцагч дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205002(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select CustomerNo, ClassCode, TypeCode, InduTypeCode, InduSubTypeCode, FirstName, LastName, MiddleName, CorporateName, CorporateName2,
RegisterNo, PassNo, Sex, BirthDay, Company, Position, Experience, DirFirstName, DirLastName, DirMiddleName, DirRegisterNo, DirPassNo,
DirSex, DirBirthDay, Email, Telephone, Mobile, HomePhone, Fax, WebSite, SpecialApproval, RateCode, CountryCode, LanguageCode,
isOtherInsurance, isHInsurance, isSInsurance, BranchNo, Status
From Customer
where CustomerNo = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205002", CustomerID);

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
        #region [ DB205003 - Харилцагч шинээр нэмэх ]
        public static Result DB205003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO Customer(CustomerNo, ClassCode, TypeCode, InduTypeCode, InduSubTypeCode, FirstName, LastName, MiddleName, CorporateName, CorporateName2,
RegisterNo, PassNo, Sex, BirthDay, Company, Position, Experience, DirFirstName, DirLastName, DirMiddleName, DirRegisterNo, DirPassNo,
DirSex, DirBirthDay, Email, Telephone, Mobile, HomePhone, Fax, WebSite, SpecialApproval, RateCode, CountryCode, LanguageCode,
isOtherInsurance, isHInsurance, isSInsurance, BranchNo, Status)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20, :21, :22, 
:23, :24, :25, :26, :27, :28, :29, :30, :31, :32, :33, :34,
:35, :36, :37, :38, :39)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205003", pParam);

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
        #region [ DB205004 - Харилцагч засварлах ]
        public static Result DB205004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE Customer SET
ClassCode=:2, TypeCode=:3, InduTypeCode=:4, InduSubTypeCode=:5, FirstName=:6, LastName=:7, MiddleName=:8, CorporateName=:9, CorporateName2=:10,
RegisterNo=:11, PassNo=:12, Sex=:13, BirthDay=:14, Company=:15, Position=:16, Experience=:17, DirFirstName=:18, DirLastName=:19, DirMiddleName=:20, DirRegisterNo=:21, DirPassNo=:22,
DirSex=:23, DirBirthDay=:24, Email=:25, Telephone=:26, Mobile=:27, HomePhone=:28, Fax=:29, WebSite=:30, SpecialApproval=:31, RateCode=:32, CountryCode=:33, LanguageCode=:34,
isOtherInsurance=:35, isHInsurance=:36, isSInsurance=:37, BranchNo=:38, Status=:39
WHERE CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205004", pParam);

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
        #region [ DB205005 - Харилцагч устгах ]
        public static Result DB205005(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM Customer WHERE CustomerNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205005", CustomerID);

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

        #region [ DB205006 - Харилцагчийн хаягийн жагсаалт авах ]
        public static Result DB205006(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT
FROM CUSTOMERADDR
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205006", CustomerID);

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
        #region [ DB205007 - Харилцагчийн хаягийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205007(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT
FROM CUSTOMERADDR
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205007", CustomerID, SeqNo);

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
        #region [ DB205008 - Харилцагчийн хаяг шинээр нэмэх ]
        public static Result DB205008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERADDR(CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205008", pParam);

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
        #region [ DB205009 - Харилцагчийн хаяг засварлах ]
        public static Result DB205009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERADDR SET
CITYCODE=:3, DISTCODE=:4, SUBDISTCODE=:5, NOTE=:6, ADDRCURRENT=:7, APARTMENT=:8
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205009", pParam);

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
        #region [ DB205010 - Харилцагчийн хаяг устгах ]
        public static Result DB205010(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERADDR WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205010", CustomerID, SeqNo);

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
        //
        #region [ DB205011 - Харилцагчийн зургийн жагсаалт авах ]
        public static Result DB205011(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID
FROM CUSTOMERPIC
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205011", CustomerID);

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
        #region [ DB205012 - Харилцагчийн зургийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205012(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID
FROM CUSTOMERPIC
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205012", CustomerID, SeqNo);

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
        #region [ DB205013 - Харилцагчийн зураг шинээр нэмэх ]
        public static Result DB205013(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERPIC(CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205013", pParam);

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
        #region [ DB205014 - Харилцагчийн зураг засварлах ]
        public static Result DB205014(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERPIC SET
PICTURETYPE=:3, ATTACHID=:4
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205014", pParam);

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
        #region [ DB205015 - Харилцагчийн зураг устгах ]
        public static Result DB205015(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERPIC WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205015", CustomerID, SeqNo);

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
        //
        #region [ DB205016 - Харилцагчийн зургийн түүхийн жагсаалт авах ]
        public static Result DB205016(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT TXNDATE, CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID, ACTION, USERNO, POSTDATE
FROM CUSTOMERPICHIST
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205016", CustomerID);

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
        #region [ DB205017 - Харилцагчийн зургийн түүхийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205017(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TXNDATE, CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID, ACTION, USERNO, POSTDATE
FROM CUSTOMERPICHIST
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205017", CustomerID, SeqNo);

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
        #region [ DB205018 - Харилцагчийн зургийн түүх шинээр нэмэх ]
        public static Result DB205018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERPICHIST(TXNDATE, CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID, ACTION, USERNO, POSTDATE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205018", pParam);

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
        #region [ DB205019 - Харилцагчийн зургийн түүх засварлах ]
        public static Result DB205019(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERPICHIST SET
TXNDATE=:1, PICTURETYPE=:4, ATTACHID=:5, ACTION=:6, USERNO=:7, POSTDATE=:8
WHERE CustomerNo = :2 AND SEQNO =: 3 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205019", pParam);

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
        #region [ DB205020 - Харилцагчийн зургийн түүх устгах ]
        public static Result DB205020(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERPICHIST WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205020", CustomerID, SeqNo);

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

        #region [ DB205021 - Харилцагчийн хамаатан садны жагсаалт авах ]
        public static Result DB205021(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, FAMILYTYPE, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE
FROM CUSTOMERFAMILY
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205021", CustomerID);

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
        #region [ DB205022 - Харилцагчийн хамаатан садны дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205022(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, FAMILYTYPE, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE
FROM CUSTOMERFAMILY
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205022", CustomerID, SeqNo);

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
        #region [ DB205023 - Харилцагчийн хамаатан садны шинээр нэмэх ]
        public static Result DB205023(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERFAMILY(CUSTOMERNO, SEQNO, FAMILYTYPE, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, :11)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205023", pParam);

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
        #region [ DB205024 - Харилцагчийн хамаатан садны засварлах ]
        public static Result DB205024(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERFAMILY SET
FAMILYTYPE=:3, FIRSTNAME=:4, LASTNAME=:5, MIDDLENAME=:6, REGISTERNO=:7, PASSNO=:8, EMAIL=:9, TELEPHONE=:10, MOBILE=:11
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205024", pParam);

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
        #region [ DB205025 - Харилцагчийн хамаатан садны устгах ]
        public static Result DB205025(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERFAMILY WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205025", CustomerID, SeqNo);

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

        #region [ DB205026 - Харилцагчийн өв залгамжлагчийн жагсаалт авах ]
        public static Result DB205026(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE
FROM CUSTOMEROV
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205026", CustomerID);

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
        #region [ DB205027 - Харилцагчийн өв залгамжлагчийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205027(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE
FROM CUSTOMEROV
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205027", CustomerID, SeqNo);

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
        #region [ DB205028 - Харилцагчийн өв залгамжлагч шинээр нэмэх ]
        public static Result DB205028(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMEROV(CUSTOMERNO, SEQNO, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205028", pParam);

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
        #region [ DB205029 - Харилцагчийн өв залгамжлагч засварлах ]
        public static Result DB205029(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMEROV SET
FIRSTNAME=:3, LASTNAME=:4, MIDDLENAME=:5, REGISTERNO=:6, PASSNO=:7, EMAIL=:8, TELEPHONE=:9, MOBILE=:10
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205029", pParam);

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
        #region [ DB205030 - Харилцагчийн өв залгамжлагч устгах ]
        public static Result DB205030(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMEROV WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205030", CustomerID, SeqNo);

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

        #region [ DB205031 - Харилцагчийн итгэмжлэгч жагсаалт авах ]
        public static Result DB205031(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE, POSITION
FROM CUSTOMERIT
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205031", CustomerID);

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
        #region [ DB205032 - Харилцагчийн итгэмжлэгч дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205032(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE, POSITION
FROM CUSTOMERIT
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205032", CustomerID, SeqNo);

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
        #region [ DB205033 - Харилцагчийн итгэмжлэгч шинээр нэмэх ]
        public static Result DB205033(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERIT(CUSTOMERNO, SEQNO, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE, POSITION)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, :11)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205033", pParam);

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
        #region [ DB205034 - Харилцагчийн итгэмжлэгч засварлах ]
        public static Result DB205034(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERIT SET
FIRSTNAME=:3, LASTNAME=:4, MIDDLENAME=:5, REGISTERNO=:6, PASSNO=:7, EMAIL=:8, TELEPHONE=:9, MOBILE=:10, POSITION=:11
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205034", pParam);

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
        #region [ DB205035 - Харилцагчийн итгэмжлэгч устгах ]
        public static Result DB205035(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERIT WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205035", CustomerID, SeqNo);

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

        #region [ DB205036 - Харилцагчийн холбоо барьсан мэдээлэл жагсаалт авах ]
        public static Result DB205036(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, CONTACTDATE, POSTDATE, CONTACTTYPE, NOTE, BRIEFDESC
FROM CUSTOMERCONTACT
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205036", CustomerID);

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
        #region [ DB205037 - Харилцагчийн холбоо барьсан мэдээлэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205037(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, CONTACTDATE, POSTDATE, CONTACTTYPE, NOTE, BRIEFDESC
FROM CUSTOMERCONTACT
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205037", CustomerID, SeqNo);

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
        #region [ DB205038 - Харилцагчийн холбоо барьсан мэдээлэл шинээр нэмэх ]
        public static Result DB205038(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;

                string sql =
@"INSERT INTO CUSTOMERCONTACT(CUSTOMERNO, SEQNO, CONTACTDATE, POSTDATE, CONTACTTYPE, NOTE, BRIEFDESC)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205038", pParam);

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
        #region [ DB205039 - Харилцагчийн холбоо барьсан мэдээлэл засварлах ]
        public static Result DB205039(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;

                string sql =
@"UPDATE CUSTOMERCONTACT SET
CONTACTDATE=:3, POSTDATE=:4, CONTACTTYPE=:5, NOTE=:6, BRIEFDESC=:7
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205039", pParam);

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
        #region [ DB205040 - Харилцагчийн холбоо барьсан мэдээлэл устгах ]
        public static Result DB205040(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERCONTACT WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205040", CustomerID, SeqNo);

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

        #region [ DB205041 - Харилцагчийн дансны мэдээлэл жагсаалт авах ]
        public static Result DB205041(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, BANKNO, CURCODE, ACCOUNTNO
FROM CUSTOMERACCOUNT
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205041", CustomerID);

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
        #region [ DB205042 - Харилцагчийн дансны мэдээлэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205042(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, BANKNO, CURCODE, ACCOUNTNO
FROM CUSTOMERACCOUNT
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205042", CustomerID, SeqNo);

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
        #region [ DB205043 - Харилцагчийн дансны мэдээлэл шинээр нэмэх ]
        public static Result DB205043(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERACCOUNT(CUSTOMERNO, SEQNO, BANKNO, CURCODE, ACCOUNTNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205043", pParam);

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
        #region [ DB205044 - Харилцагчийн дансны мэдээлэл засварлах ]
        public static Result DB205044(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERACCOUNT SET
BANKNO=:3, CURCODE=:4, ACCOUNTNO=:5
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205044", pParam);

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
        #region [ DB205045 - Харилцагчийн дансны мэдээлэл устгах ]
        public static Result DB205045(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERACCOUNT WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205045", CustomerID, SeqNo);

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

        #region [ DB205046 - Харилцагчийн хавсралтууд жагсаалт авах ]
        public static Result DB205046(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, ATTACHID
FROM CUSTOMERATTACH
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205046", CustomerID);

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
        #region [ DB205047 - Харилцагчийн хавсралтууд дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205047(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, ATTACHID
FROM CUSTOMERATTACH
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205047", CustomerID, SeqNo);

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
        #region [ DB205048 - Харилцагчийн хавсралтууд шинээр нэмэх ]
        public static Result DB205048(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERATTACH(CUSTOMERNO, SEQNO, ATTACHID)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205048", pParam);

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
        #region [ DB205049 - Харилцагчийн хавсралтууд засварлах ]
        public static Result DB205049(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERATTACH SET
ATTACHID=:3
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205049", pParam);

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
        #region [ DB205050 - Харилцагчийн хавсралтууд устгах ]
        public static Result DB205050(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERATTACH WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205050", CustomerID, SeqNo);

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

        #region [ DB205051 - Харилцагчийн товч дүгнэлт жагсаалт авах ]
        public static Result DB205051(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, TXNDATE, POSTDATE, USERNO, NOTE
FROM CUSTOMERNOTE
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205051", CustomerID);

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
        #region [ DB205052 - Харилцагчийн товч дүгнэлт дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205052(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, TXNDATE, POSTDATE, USERNO, NOTE
FROM CUSTOMERNOTE
where CustomerNo = :1 AND SEQNO =: 2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205052", CustomerID, SeqNo);

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
        #region [ DB205053 - Харилцагчийн товч дүгнэлт шинээр нэмэх ]
        public static Result DB205053(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;

                string sql =
@"INSERT INTO CUSTOMERNOTE(CUSTOMERNO, SEQNO, TXNDATE, POSTDATE, USERNO, NOTE)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205053", pParam);

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
        #region [ DB205054 - Харилцагчийн товч дүгнэлт засварлах ]
        public static Result DB205054(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;
                string sql =
@"UPDATE CUSTOMERNOTE SET
TXNDATE=:3, POSTDATE=:4, USERNO=:5, NOTE=:6
WHERE CustomerNo = :1 AND SEQNO =: 2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205054", pParam);

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
        #region [ DB205055 - Харилцагчийн товч дүгнэлт устгах ]
        public static Result DB205055(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERNOTE WHERE CustomerNo = :1 AND SEQNO =: 2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205055", CustomerID, SeqNo);

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
        //Параметр дээр орно
        #region [ DB205056 - Харилцагчийн нэмэлт мэдээлэл жагсаалт авах /CustAdd/ ]
        public static Result DB205056(DbConnections pDB, int pagenumber, int pagecount)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT ID, NAME, NAME2, ADDTYPE, LEN, MANDATORY, MASK, ADDDEFAULT, DESCRIPTION, LISTCOMBO, COMBOEDIT,  TABLENAME, FIELDLD,  FIELDNAME, ORDERNO
FROM CUSTADD
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205056");

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
        #region [ DB205057 - Харилцагчийн нэмэлт мэдээлэл дэлгэрэнгүй мэдээлэл авах /CustAdd/ ]
        public static Result DB205057(DbConnections pDB, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, NAME, NAME2, ADDTYPE, LEN, MANDATORY, MASK, ADDDEFAULT, DESCRIPTION, LISTCOMBO, COMBOEDIT,  TABLENAME, FIELDLD,  FIELDNAME, ORDERNO
FROM CUSTADD
where ID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205057", pID);

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
        #region [ DB205058 - Харилцагчийн нэмэлт мэдээлэл шинээр нэмэх /CustAdd/ ]
        public static Result DB205058(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTADD(ID, NAME, NAME2, ADDTYPE, LEN, MANDATORY, MASK, ADDDEFAULT, DESCRIPTION, LISTCOMBO, COMBOEDIT, TABLENAME, FIELDID, FIELDNAME, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205058", pParam);

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
        #region [ DB205059 - Харилцагчийн нэмэлт мэдээлэл засварлах /CustAdd/ ]
        public static Result DB205059(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTADD SET
NAME=:2, NAME2=:3, ADDTYPE=:4, LEN=:5, MANDATORY=:6, MASK=:7, ADDDEFAULT=:8, DESCRIPTION=:9, LISTCOMBO=:10, 
COMBOEDIT=:11, TABLENAME=:12, FIELDID=:13, FIELDNAME=:14, ORDERNO=:15
WHERE ID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205059", pParam);

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
        #region [ DB205060 - Харилцагчийн нэмэлт мэдээлэл устгах /CustAdd/ ]
        public static Result DB205060(DbConnections pDB, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTADD WHERE ID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205060", pID);

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
        //
        #region [ DB205061 - Харилцагчийн нэмэлт мэдээллийн өгөгдөлийн жагсаалт авах /CustAddData/ ]
        public static Result DB205061(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT a.KEY, a.ID, a.VALUE, a.ATTACHID, C.NAME, C.NAME2, C.ADDTYPE, C.LEN, C.MANDATORY, C.MASK,
C.ADDDEFAULT, C.DESCRIPTION, C.LISTCOMBO, C.COMBOEDIT, C.TABLENAME, C.FIELDID, C.FIELDNAME, C.ORDERNO 
FROM CUSTADDDATA a
left join (SELECT ID, NAME, NAME2, ADDTYPE, LEN, MANDATORY, MASK, ADDDEFAULT, DESCRIPTION,
            LISTCOMBO, COMBOEDIT, TABLENAME, FIELDID, FIELDNAME, ORDERNO
            FROM CUSTADD) c on a.id=c.id
WHERE a.KEY=:1
ORDER BY C.ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205061", CustomerID);

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
        #region [ DB205062 - Харилцагчийн нэмэлт мэдээлэлийн өгөгдөлийн дэлгэрэнгүй мэдээлэл авах /CustAddData/ ]
        public static Result DB205062(DbConnections pDB, long CustomerID, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, ID, VALUE, ATTACHID
FROM CUSTADDDATA
WHERE KEY=:1 AND ID = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205062", CustomerID, pID);

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
        #region [ DB205063 - Харилцагчийн нэмэлт мэдээлэлийн өгөгдөлийн шинээр нэмэх /CustAddData/ ]
        public static Result DB205063(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTADDDATA(KEY, ID, VALUE, ATTACHID)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205063", pParam);

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
        #region [ DB205064 - Харилцагчийн нэмэлт мэдээлэлийн өгөгдөлийн засварлах /CustAdd/ ]
        public static Result DB205064(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTADDDATA SET
VALUE=:3, ATTACHID=:4
WHERE KEY=:1 AND ID = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205064", pParam);

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
        #region [ DB205065 - Харилцагчийн нэмэлт мэдээлэлийн өгөгдөлийн устгах /CustAddData/ ]
        public static Result DB205065(DbConnections pDB, long CustomerID, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTADDDATA WHERE KEY=:1 AND ID = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205065", CustomerID, pID);

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

        #endregion
        #region [ DB206 - Inventory ]
        #region [ DB206001 - Бараа материалын жагсаалт авах ]
        public static Result DB206001(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where;

                where = " WHERE 1 = 1 ";
                /*
                if (pParam != null)
                {
                    if (pParam[0] != "" && pParam[0] != null) where += " AND CustomerNo='" + Static.ToStr(pParam[0]) + "'";
                    if (pParam[1] != "" && pParam[1] != null) where += " AND ClassCode='" + Static.ToStr(pParam[1]) + "'";
                    if (pParam[2] != "" && pParam[2] != null) where += " AND BranchNo='" + Static.ToStr(pParam[2]) + "'";
                    if (pParam[3] != "" && pParam[3] != null) where += " AND Status='" + Static.ToStr(pParam[3]) + "'";
                    if (pParam[4] != "" && pParam[4] != null) where += " AND FirstName='" + Static.ToStr(pParam[4]) + "'";
                    if (pParam[5] != "" && pParam[5] != null) where += " AND LastName='" + Static.ToStr(pParam[5]) + "'";
                    if (pParam[6] != "" && pParam[6] != null) where += " AND RegisterNo='" + Static.ToStr(pParam[6]) + "'";
                    if (pParam[7] != "" && pParam[7] != null) where += " AND PassNo='" + Static.ToStr(pParam[7]) + "'";
                    if (pParam[8] != "" && pParam[8] != null) where += " AND Sex='" + Static.ToStr(pParam[8]) + "'";
                    if (pParam[9] != "" && pParam[9] != null) where += " AND isHInsurance='" + Static.ToStr(pParam[9]) + "'";
                    if (pParam[10] != "" && pParam[10] != null) where += " AND isSInsurance=" + Static.ToStr(pParam[10]) + "'";
                    if (pParam[11] != "" && pParam[11] != null) where += " AND TypeCode='" + Static.ToStr(pParam[11]) + "'";
                    if (pParam[12] != "" && pParam[12] != null) where += " AND CorporateName='" + Static.ToStr(pParam[12]) + "'";
                    if (pParam[13] != "" && pParam[13] != null) where += " AND InduTypeCode='" + Static.ToStr(pParam[13]) + "'";
                    if (pParam[14] != "" && pParam[14] != null) where += " AND InduSubTypeCode='" + Static.ToStr(pParam[14]) + "'";
                    if (pParam[15] != "" && pParam[15] != null) where += " AND DirFirstName='" + Static.ToStr(pParam[15]) + "'";
                    if (pParam[16] != "" && pParam[16] != null) where += " AND DirLastName='" + Static.ToStr(pParam[16]) + "'";
                    if (pParam[17] != "" && pParam[17] != null) where += " AND RateCode='" + Static.ToStr(pParam[17]) + "'";
                    if (pParam[18] != "" && pParam[18] != null) where += " AND isOtherInsurance='" + Static.ToStr(pParam[18]) + "'";
                    if (pParam[19] != "" && pParam[19] != null) where += " AND Email='" + Static.ToStr(pParam[19]) + "'";
                    if (pParam[20] != "" && pParam[20] != null) where += " AND Telephone='" + Static.ToStr(pParam[20]) + "'";
                    if (pParam[21] != "" && pParam[21] != null) where += " AND Mobile='" + Static.ToStr(pParam[21]) + "'";
                    if (pParam[22] != "" && pParam[22] != null) where += " AND HomePhone='" + Static.ToStr(pParam[22]) + "'";

                    WriteDBLog("\nLIST\n" + where, pParam, 23);
                }*/

                sql =
@"SELECT INVID, INVTYPEID, NAME, NAME2, BRANCHNO, CREATEUSER, UNITTYPECODE, BALANCECOUNT, UNITCOST, BALANCETOTAL,
CURRCODE, POSITION, ACCOUNTNO, EMPNO, FIRSTTXNDATE
FROM INVENTORY
" + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB206001", null);

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
        #region [ DB206002 - Бараа материалын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB206002(DbConnections pDB, long pInvID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INVID, INVTYPEID, NAME, NAME2, BRANCHNO, CREATEUSER, UNITTYPECODE, BALANCECOUNT, UNITCOST, BALANCETOTAL,
CURRCODE, POSITION, ACCOUNTNO, EMPNO, FIRSTTXNDATE
FROM INVENTORY
where INVID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB206002", pInvID);

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
        #region [ DB206003 - Бараа материал шинээр нэмэх ]
        public static Result DB206003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO INVENTORY(INVID, INVTYPEID, NAME, NAME2, BRANCHNO, CREATEUSER, UNITTYPECODE, BALANCECOUNT, UNITCOST, BALANCETOTAL,
CURRCODE, POSITION, ACCOUNTNO, EMPNO, FIRSTTXNDATE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB206003", pParam);

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
        #region [ DB206004 - Бараа материал засварлах ]
        public static Result DB206004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE INVENTORY SET
INVTYPEID=:2, NAME=:3, NAME2=:4, BRANCHNO=:5, CREATEUSER=:6, UNITTYPECODE=:7, BALANCECOUNT=:8, UNITCOST=:9, BALANCETOTAL=:10,
CURRCODE=:11, POSITION=:12, ACCOUNTNO=:13, EMPNO=:14, FIRSTTXNDATE=:15
WHERE INVID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB206004", pParam);

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
        #region [ DB206005 - Бараа материал устгах ]
        public static Result DB206005(DbConnections pDB, long pInvID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM INVENTORY WHERE INVID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB206005", pInvID);

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
        #endregion
        #region [ DB207 - FA ]
        #region [ DB207001 - Үндсэн хөрөнгийн жагсаалт авах ]
        public static Result DB207001(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where = " ";
                sql =
@"SELECT a.FAID, a.FATYPEID, a.NAME, a.NAME2, a.BRANCHNO, a.CREATEUSER, a.UNITTYPECODE, b.name unitname, a.BALANCECOUNT, a.UNITCOST,
a.BALANCETOTAL, a.CURRCODE, a.POSITION, a.ACCOUNTNO, c.name accountname, a.EMPNO, d.name empname, a.startdate, a.enddate, a.LASTTELLERTXNDATE,
a.STATUS, a.DEPRECIATION, a.LASTDEPDATE
FROM FAREG a
left join UNITTYPE b on A.UNITTYPECODE=B.UNITTYPECODE
left join bacaccount c on C.aCCOUNTno=A.ACCOUNTNO
left join EMPLOYEE d on D.EMPNO=A.EMPNO
" + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB207001", null);

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
        #region [ DB207002 - Үндсэн хөрөнгийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB207002(DbConnections pDB, long pFaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT FAID, FATYPEID, NAME, NAME2, BRANCHNO, CREATEUSER, UNITTYPECODE, BALANCECOUNT, UNITCOST, BALANCETOTAL,
CURRCODE, POSITION, ACCOUNTNO, EMPNO, STARTDATE, ENDDATE, LASTTELLERTXNDATE, STATUS, DEPRECIATION, LASTDEPDATE
FROM FAREG
where FAID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB207002", pFaID);

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
        #region [ DB207003 - Үндсэн хөрөнгө шинээр нэмэх ]
        public static Result DB207003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO FAREG(FAID, FATYPEID, NAME, NAME2, BRANCHNO, CREATEUSER, UNITTYPECODE, BALANCECOUNT, UNITCOST, BALANCETOTAL,
CURRCODE, POSITION, ACCOUNTNO, EMPNO, STARTDATE, ENDDATE, LASTTELLERTXNDATE, STATUS, DEPRECIATION, LASTDEPDATE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB207003", pParam);

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
        #region [ DB207004 - Үндсэн хөрөнгө засварлах ]
        public static Result DB207004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE FAREG SET
FATYPEID=:2, NAME=:3, NAME2=:4, BRANCHNO=:5, CREATEUSER=:6, UNITTYPECODE=:7, BALANCECOUNT=:8, UNITCOST=:9, BALANCETOTAL=:10, 
CURRCODE=:11, POSITION=:12, ACCOUNTNO=:13, EMPNO=:14, startdate=:15, enddate=:16, LASTTELLERTXNDATE=:17, STATUS=:18, DEPRECIATION=:19, LASTDEPDATE=:20
WHERE FAID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB207004", pParam);

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
        #region [ DB207005 - Үндсэн хөрөнгө устгах ]
        public static Result DB207005(DbConnections pDB, long pFaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM FAREG WHERE FAID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB207005", pFaID);

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
        #endregion
        #region [ DB208 - Employee&BACACCOUNT Байгууллагын данс ]
        #region [ DB208001 - Ажилчидийн бүртгэлийн жагсаалт авах ]
        public static Result DB208001(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where;

                where = " WHERE 1 = 1 ";
                /*
                if (pParam != null)
                {
                    if (pParam[0] != "" && pParam[0] != null) where += " AND CustomerNo='" + Static.ToStr(pParam[0]) + "'";
                    if (pParam[1] != "" && pParam[1] != null) where += " AND ClassCode='" + Static.ToStr(pParam[1]) + "'";
                    if (pParam[2] != "" && pParam[2] != null) where += " AND BranchNo='" + Static.ToStr(pParam[2]) + "'";
                    if (pParam[3] != "" && pParam[3] != null) where += " AND Status='" + Static.ToStr(pParam[3]) + "'";
                    if (pParam[4] != "" && pParam[4] != null) where += " AND FirstName='" + Static.ToStr(pParam[4]) + "'";
                    if (pParam[5] != "" && pParam[5] != null) where += " AND LastName='" + Static.ToStr(pParam[5]) + "'";
                    if (pParam[6] != "" && pParam[6] != null) where += " AND RegisterNo='" + Static.ToStr(pParam[6]) + "'";
                    if (pParam[7] != "" && pParam[7] != null) where += " AND PassNo='" + Static.ToStr(pParam[7]) + "'";
                    if (pParam[8] != "" && pParam[8] != null) where += " AND Sex='" + Static.ToStr(pParam[8]) + "'";
                    if (pParam[9] != "" && pParam[9] != null) where += " AND isHInsurance='" + Static.ToStr(pParam[9]) + "'";
                    if (pParam[10] != "" && pParam[10] != null) where += " AND isSInsurance=" + Static.ToStr(pParam[10]) + "'";
                    if (pParam[11] != "" && pParam[11] != null) where += " AND TypeCode='" + Static.ToStr(pParam[11]) + "'";
                    if (pParam[12] != "" && pParam[12] != null) where += " AND CorporateName='" + Static.ToStr(pParam[12]) + "'";
                    if (pParam[13] != "" && pParam[13] != null) where += " AND InduTypeCode='" + Static.ToStr(pParam[13]) + "'";
                    if (pParam[14] != "" && pParam[14] != null) where += " AND InduSubTypeCode='" + Static.ToStr(pParam[14]) + "'";
                    if (pParam[15] != "" && pParam[15] != null) where += " AND DirFirstName='" + Static.ToStr(pParam[15]) + "'";
                    if (pParam[16] != "" && pParam[16] != null) where += " AND DirLastName='" + Static.ToStr(pParam[16]) + "'";
                    if (pParam[17] != "" && pParam[17] != null) where += " AND RateCode='" + Static.ToStr(pParam[17]) + "'";
                    if (pParam[18] != "" && pParam[18] != null) where += " AND isOtherInsurance='" + Static.ToStr(pParam[18]) + "'";
                    if (pParam[19] != "" && pParam[19] != null) where += " AND Email='" + Static.ToStr(pParam[19]) + "'";
                    if (pParam[20] != "" && pParam[20] != null) where += " AND Telephone='" + Static.ToStr(pParam[20]) + "'";
                    if (pParam[21] != "" && pParam[21] != null) where += " AND Mobile='" + Static.ToStr(pParam[21]) + "'";
                    if (pParam[22] != "" && pParam[22] != null) where += " AND HomePhone='" + Static.ToStr(pParam[22]) + "'";

                    WriteDBLog("\nLIST\n" + where, pParam, 23);
                }*/

                sql =
@"SELECT EMPNO, NAME, NAME2, POSITION, STATUS, BRANCHNO, USERNO
FROM EMPLOYEE
" + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB208001", null);

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
        #region [ DB208002 - Ажилчидийн бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB208002(DbConnections pDB, long pEmpID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT EMPNO, NAME, NAME2, POSITION, STATUS, BRANCHNO, USERNO
FROM EMPLOYEE
where EMPNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB208002", pEmpID);

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
        #region [ DB208003 - Ажилчидийн бүртгэл шинээр нэмэх ]
        public static Result DB208003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO EMPLOYEE(EMPNO, NAME, NAME2, POSITION, STATUS, BRANCHNO, USERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB208003", pParam);

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
        #region [ DB208004 - Ажилчидийн бүртгэл засварлах ]
        public static Result DB208004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE EMPLOYEE SET
NAME=:2, NAME2=:3, POSITION=:4, STATUS=:5, BRANCHNO=:6, USERNO=:7
WHERE EMPNO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB208004", pParam);

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
        #region [ DB208005 - Ажилчидийн бүртгэл устгах ]
        public static Result DB208005(DbConnections pDB, long pEmpID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM EMPLOYEE WHERE EMPNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB208005", pEmpID);

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

        #region [ DB208006 - Байгууллагын дансны бүртгэлийн жагсаалт авах ]
        public static Result DB208006(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where;

                where = " WHERE 1 = 1 ";
                /*
                if (pParam != null)
                {
                    if (pParam[0] != "" && pParam[0] != null) where += " AND CustomerNo='" + Static.ToStr(pParam[0]) + "'";
                    if (pParam[1] != "" && pParam[1] != null) where += " AND ClassCode='" + Static.ToStr(pParam[1]) + "'";
                    if (pParam[2] != "" && pParam[2] != null) where += " AND BranchNo='" + Static.ToStr(pParam[2]) + "'";
                    if (pParam[3] != "" && pParam[3] != null) where += " AND Status='" + Static.ToStr(pParam[3]) + "'";
                    if (pParam[4] != "" && pParam[4] != null) where += " AND FirstName='" + Static.ToStr(pParam[4]) + "'";
                    if (pParam[5] != "" && pParam[5] != null) where += " AND LastName='" + Static.ToStr(pParam[5]) + "'";
                    if (pParam[6] != "" && pParam[6] != null) where += " AND RegisterNo='" + Static.ToStr(pParam[6]) + "'";
                    if (pParam[7] != "" && pParam[7] != null) where += " AND PassNo='" + Static.ToStr(pParam[7]) + "'";
                    if (pParam[8] != "" && pParam[8] != null) where += " AND Sex='" + Static.ToStr(pParam[8]) + "'";
                    if (pParam[9] != "" && pParam[9] != null) where += " AND isHInsurance='" + Static.ToStr(pParam[9]) + "'";

                    WriteDBLog("\nLIST\n" + where, pParam, 10);
                }*/

                sql =
@"SELECT ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE, STATUS, STARTDATE, ENDDATE
FROM BACACCOUNT
" + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB208006", null);

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
        #region [ DB208007 - Байгууллагын дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB208007(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE, STATUS, STARTDATE, ENDDATE
FROM BACACCOUNT
where ACCOUNTNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB208007", pAccountNo);

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
        #region [ DB208008 - Байгууллагын дансны бүртгэл шинээр нэмэх ]
        public static Result DB208008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO BACACCOUNT(ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
STATUS, STARTDATE, ENDDATE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB208008", pParam);

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
        #region [ DB208009 - Байгууллагын дансны бүртгэл засварлах ]
        public static Result DB208009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BACACCOUNT SET
NAME=:2, NAME2=:3, BRANCHNO=:4, PRODCODE=:5, BALANCE=:6, CURCODE=:7, USERNO=:8, LEVELNO=:9, CREATEDATE=:10,
STATUS=:11, STARTDATE=:12, ENDDATE=:13
WHERE ACCOUNTNO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB208009", pParam);

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
        #region [ DB208010 - Байгууллагын дансны бүртгэл устгах ]
        public static Result DB208010(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM BACACCOUNT WHERE ACCOUNTNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB208010", pAccountNo);

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
        #endregion
        #region [ DB209 - CTA Балансын гадуурх данс CONACCOUNT ]
        #region [ DB209001 - Балансын гадуурх дансны бүртгэлийн жагсаалт авах ]
        public static Result DB209001(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where= " ";

                //where = " WHERE 1 = 1 ";

                sql =
@"SELECT ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
STATUS, STARTDATE, ENDDATE, CONTRACTID, INSURANCENO, RIINSURANCENO, CLAIMNO, CUSTNO, PERSON, LastTellerTxnDate
FROM CONACCOUNT " + where;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB209001", null);

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
        #region [ DB209002 - Балансын гадуурх дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB209002(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
STATUS, STARTDATE, ENDDATE, CONTRACTID, INSURANCENO, RIINSURANCENO, CLAIMNO, CUSTNO, PERSON, LastTellerTxnDate
FROM CONACCOUNT
where ACCOUNTNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB209002", pAccountNo);

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
        #region [ DB209003 - Балансын гадуурх дансны бүртгэл шинээр нэмэх ]
        public static Result DB209003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CONACCOUNT(ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
STATUS, STARTDATE, ENDDATE, CONTRACTID, INSURANCENO, RIINSURANCENO, CLAIMNO, CUSTNO, PERSON, LastTellerTxnDate)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB209003", pParam);

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
        #region [ DB209004 - Балансын гадуурх дансны бүртгэл засварлах ]
        public static Result DB209004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONACCOUNT SET
NAME=:2, NAME2=:3, BRANCHNO=:4, PRODCODE=:5, BALANCE=:6, CURCODE=:7, USERNO=:8, LEVELNO=:9, CREATEDATE=:10,
STATUS=:11, STARTDATE=:12, ENDDATE=:13, CONTRACTID=:14, INSURANCENO=:15, RIINSURANCENO=:16, CLAIMNO=:17, CUSTNO=:18, PERSON=:19, LastTellerTxnDate=:20
WHERE ACCOUNTNO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB209004", pParam);

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
        #region [ DB209005 - Балансын гадуурх дансны бүртгэл устгах ]
        public static Result DB209005(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CONACCOUNT WHERE ACCOUNTNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB209005", pAccountNo);

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
        #endregion
        #region [ DB210 - TXNs ]
        #region [ DB210001 - Select Account ]
        public static Result DB210001(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select
A.AccountNo, A.Name, A.Name2, A.BranchNo, A.ProdCode, P.GL, P.Type, P.BalanceType, A.Balance, A.CurCode, A.StartDate,
A.EndDate, A.LevelNo, A.Status, A.LASTTELLERTXNDATE
from bacaccount A
left join bacproduct  P on A.ProdCode=P.ProdCode
where AccountNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB210001", pAccountNo);

                return res;
            }
            catch(Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB210002 - Insert TXNs {CON(CTA), BAC} ]
        public static Result DB210002(DbConnections pDB, string pType, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, tables=pType+"txn";

                sql =
@"insert into " + tables + @"(JrNo, SubJrNo, SeqNo, TxnEntry, TxnDate, TxnDate, PostDate, AccountNo, BranchNo, ProdCode, UserNo,
HostName, HostIP, HostMAC, TxnCode, Amount, Rate, CurCode, Balance, ContAcccountNo, ContCurrCode, 
ContRate, ContAmount, BaseAmount, Description, Corr, isCash, Supervisor, Flag, M)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23, :24, :25, :26, :27, :28, :29)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB210002", pParam);

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
        #region [ DB210003 - UPDATE TXNACCOUNTs {CON(CTA), BAC} ]
        public static Result DB210003(DbConnections pDB, string pType, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, tables = pType + "Account";

                sql =
@"UPDATE " + tables + " SET Balance =: 2, LastTellerTxnDate=:3 WHERE AccountNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210003", pParam);

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
        #region [ DB210004 - Буцаагдах гүйлгээний жагсаалт ]
        public static Result DB210004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select 'BA', jrno, subjrno, seqno, txnentry, txndate, postdate, accountno, branchno, prodcode,
userno, hostname, hostip, hostmac, txncode, amount, rate, curcode, balance, contacccountno,
contcurrcode, contrate, contamount, baseamount, description, corr, iscash, supervisor, flag, M
from bactxn
where jrno=:1 and txndate=:2
union all
select 'CT', jrno, subjrno, seqno, txnentry,  txndate, postdate, accountno, branchno, prodcode,
userno, hostname, hostip, hostmac, txncode, amount, rate, curcode, balance, contacccountno,
contcurrcode, contrate, contamount, baseamount, description, corr, iscash, supervisor, flag, M
from contxn
where jrno=:1 and txndate=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210004", pParam);

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
        #region [ DB210005 - Гүйлгээгээ буцаасан update ]
        public static Result DB210005(DbConnections pDB, string pType, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, tables = pType + "txn";

                sql =
@"update " + tables + @" set corr=1
where jrno=:1 and txndate=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210005", pParam);

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
        #region [ DB210006 - Буцаах гүйлгээний мэдээлэл авах ]
        public static Result DB210006(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select 'BA', jrno, subjrno, seqno, txnentry, txndate, postdate, accountno, branchno, prodcode,
userno, hostname, hostip, hostmac, txncode, amount, rate, curcode, balance, contacccountno,
contcurrcode, contrate, contamount, baseamount, description, corr, iscash, supervisor, flag, M
from bactxn
where jrno=:1 and txndate=:2 and M = 1
union all
select 'CT', jrno, subjrno, seqno, txnentry,  txndate, postdate, accountno, branchno, prodcode,
userno, hostname, hostip, hostmac, txncode, amount, rate, curcode, balance, contacccountno,
contcurrcode, contrate, contamount, baseamount, description, corr, iscash, supervisor, flag, M
from contxn
where jrno=:1 and txndate=:2 and M = 1 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210006", pParam);

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
        #region [ DB210007 - Гүйлгээний журнал лавлагаа{BAC, CON} ]
        public static Result DB210007(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql, where=" ";

                if (pParam != null)
                {
                    where = " WHERE ";
                    if (pParam[0] != null & pParam[0] != "") where += " JrNo='" + Static.ToStr(pParam[0]) + "' AND ";
                    if (pParam[1] != null & pParam[1] != "") where += " TxnDate='" + Static.ToStr(pParam[1]) + "' AND ";
                    if (pParam[2] != null & pParam[2] != "") where += " AccountNo='" + Static.ToStr(pParam[2]) + "' AND ";
                    if (pParam[3] != null & pParam[3] != "") where += " ContAcccountNo='" + Static.ToStr(pParam[3]) + "' AND ";
                    if (pParam[4] != null & pParam[4] != "") where += " Amount='" + Static.ToStr(pParam[4]) + "' AND ";
                    if (pParam[5] != null & pParam[5] != "") where += " ContAmount='" + Static.ToStr(pParam[5]) + "' AND ";
                    if (pParam[6] != null & pParam[6] != "") where += " CurCode='" + Static.ToStr(pParam[6]) + "' AND ";
                    if (pParam[7] != null & pParam[7] != "") where += " ContCurrCode='" + Static.ToStr(pParam[7]) + "' AND ";
                    if (pParam[8] != null & pParam[8] != "") where += " UserNo='" + Static.ToStr(pParam[8]) + "' AND ";

                    where = where.Substring(0, where.Length - 4);
                    WriteDBLog("\nTXNLIST\n" + where, pParam, 9);
                }

                sql =
@"SELECT 'BAC' txnType, JRNO, SUBJRNO, SEQNO, TXNENTRY, TXNDATE, POSTDATE, ACCOUNTNO, BRANCHNO, PRODCODE, USERNO, 
HOSTNAME, HOSTIP, HOSTMAC, TXNCODE, AMOUNT, RATE, CURCODE, BALANCE, CONTACCCOUNTNO, CONTCURRCODE, 
CONTRATE, CONTAMOUNT, BASEAMOUNT, DESCRIPTION, CORR, ISCASH, SUPERVISOR, FLAG, M
FROM BACTXN " 
+ where + @"
union all
SELECT 'CON', JRNO, SUBJRNO, SEQNO, TXNENTRY, TXNDATE, POSTDATE, ACCOUNTNO, BRANCHNO, PRODCODE, USERNO, 
HOSTNAME, HOSTIP, HOSTMAC, TXNCODE, AMOUNT, RATE, CURCODE, BALANCE, CONTACCCOUNTNO, CONTCURRCODE, 
CONTRATE, CONTAMOUNT, BASEAMOUNT, DESCRIPTION, CORR, ISCASH, SUPERVISOR, FLAG, M
FROM CONTXN " 
+ where + @"
Order By txnType, TXNDATE, JRNO, SUBJRNO, ACCOUNTNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210007", pParam);

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
        #region [ DB210008 - Гүйлгээн дээрх оролтуудын жагсаалт ]
        public static Result DB210008(DbConnections pDB, int pTranCode)
        {
            Result res = new Result();
            try
            {
                 string sql =
@"select  TE.TranCode, TE.EntryCode, E.EntryTxnCode , E.DRAcntNo, E.DRCurrCode, E.DRRate, E.DRAmount, E.CRAcntNo, E.CRCurrCode, E.CRRate, E.CRAmount,
E.Description
from TxnEntry TE, ENTRY E
where TE.EntryCode=E.EntryCode AND TE.TranCode=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210008", pTranCode);
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
        #region [ DB210009 - Санхүүгийн гүйлгээний жагсаалт ]
        public static Result DB210009(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select TxnCode, Name, Name2, DynamicSQL
FROM TxnFin
order by OrderNo ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210009", null);
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
        #region [ DB210010 - Insert FATXN ]
        public static Result DB210010(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO FATXN(JrNo, TxnDate, PostDate, FATypeID, FAID, AccountNo, TxnCode, TxnType, TxnCount, TxnAmount, 
TxnCurrCode, Rate, UserNo, BranchNo, Description, ContAcntNo, ContAmount, ContCurCode, ContRate, ClaimlD, 
InsuranceNo, OrderNo, Profit, Depreciation)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23, :24)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB210010", pParam);
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
        #region [ DB210011 - Update FAReg ]
        public static Result DB210011(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Update FAReg
Set BalanceTotal=:2, BalanceCount=:3, LastTellerTxnDate=:4, Depreciation=:5, LastDepDate=:6
where FAID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210011", pParam);
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
        #region [ DB210012 - Insert PendTxn ]
        public static Result DB210012(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PendTxn(JrNo, JrSubNo, TxnDate, PostDate, UserNo, HostName, HostIP, HostMAC, TxnCode, TxnEntry,
DRAccountMod, DRAccountNo, DRAmount, DRRate, DRCurCode, DRAmountBalance, CRAccountMod, CRAccountNo, CRAmount, CRRate,
CRCurCode, Description, ExtIDType, ExtID, Status)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23, :24, :25)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210012", pParam);
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
        #region [ DB210013 - Идэвхтэй байгаа PENDTXN -ий жагсаалт ]
        public static Result DB210013(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select jrno, jrsubno, txndate, postdate, userno, txncode, draccountno, dramount, drcurcode, craccountno,
cramount, crcurcode, description, extidtype, extid
from pendtxn
where status<>9";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB210013", null);
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
        #region [ DB210014 - PENDTXN -ий жагсаалт ]
        public static Result DB210014(DbConnections pDB, long pJrNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select jrno, jrsubno, txndate, postdate, userno, hostname, hostip, hostmac,  txncode, draccountno,
dramount, drcurcode, drrate, craccountno, cramount, crcurcode, crrate, description, extidtype, extid,
status
from pendtxn
where Jrno=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB210012", pJrNo);
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
        #region [ DB210015 - PendTxn-ийг гүйлгээ хийх ]
        public static Result DB210015(DbConnections pDB, long pJrNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update pendtxn set
status=9
where Jrno=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210015", pJrNo);
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
        #region [ DB210016 - Бараа материалын дэлгэрэнгүй ]
        public static Result DB210016(DbConnections pDB, long pInvID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select A.InvID, P.InvTypeID,  A.Name, A.Name2, A.BranchNo, A.UnitTypeCode, A.BalanceCount, A.UnitCost, A.BalanceTotal, A.CurrCode,
A.Position, A.AccountNo, A.EmpNo
from Inventory A
left join InventoryType P on P.InvTypeID=A.InvTypeID
where A.InvID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB210016", pInvID);
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
        #region [ DB210017 - Бараа материалын үндсэн мэдээлэл дээр update хийх ]
        public static Result DB210017(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Update Inventory Set
BalanceTotal=:2, BalanceCount=:3, LastTellerTxnDate=:4
Where InvID=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB210017", pParam);
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
        #region [ DB210018 - Insert InventoryTxn-ий гүйлгээ хийх ]
        public static Result DB210018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO Inventorytxn(JrNo, TxnDate, PostDate, InvTypeID, InvID, AccountNo, TxnCode, TxnType, TxnCount, TxnAmount,
TxnCurrCode, Rate, UserNo, BranchNo, Description, ContAcntNo, ContAmount, ContCurCode, ContRate, ClaimlD,
InsuranceNo)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB210018", pParam);
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
        #endregion
        #region [ DB211 - GL ]
        #region [ DB211001 - Дансны үлдэгдлийн жагсаалт ]
        public static Result DB211001(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT ACCOUNT, BRANCHNO, CURCODE, YEAR, MONTH, OBAL, CURRBAL, MONTHLYDT, MONTHLYCR, QDT,
QCR, YDT, YCR
FROM CHARTBALANCE";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211001", null);

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
        #region [ DB211002 - Дансны өдрийн үлдэгдлийн жагсаалт ]
        public static Result DB211002(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT ACCOUNT, BRANCHNO, CURCODE, TXNDATE, DAILYDT, DAILYCR, DAILYBALANCE
FROM CHARTBALANCEDAILY";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211002", null);

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
        #region [ DB211003 - Дансны гүйлгээний жагсаалт ]
        public static Result DB211003(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT JRNO, ENTRYNO, ACCOUNTNO, BRANCHNO, CURRCODE, AMOUNT, DESCRIPTION, TXNDATE, POSTDATE, USERNO, 
CORR, TXNCODE, RATE, BALANCE
FROM CHARTTXN";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211003", null);

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

        #region [ DB211004 - Дансны төсвийн жагсаалт ]
        public static Result DB211004(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT ACCOUNT, BRANCHNO, CURCODE, YEAR, MONTH, BUDGETBAL
FROM CHARTBUDGET";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211004", null);

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
        #region [ DB211005 - Дансны төсвийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB211005(DbConnections pDB, long pAccount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ACCOUNT, BRANCHNO, CURCODE, YEAR, MONTH, BUDGETBAL
FROM CHARTBUDGET
WHERE ACCOUNT=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211005", pAccount);

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
        #region [ DB211006 - Дансны төсвийг шинээр нэмэх ]
        public static Result DB211006(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong seq = EServ.Interface.Sequence.NextByVal("Account");

                pParam[0] = Static.ToStr(seq);

                string sql =
@"INSERT INTO CHARTBUDGET(ACCOUNT, BRANCHNO, CURCODE, YEAR, MONTH, BUDGETBAL)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB211006", pParam);

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
        #region [ DB211007 - Дансны төсвийг засварлах ]
        public static Result DB211007(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CHARTBUDGET SET
BRANCHNO=:2, CURCODE=:3, YEAR=:4, MONTH=:5, BUDGETBAL=:6
WHERE ACCOUNT = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211007", pParam);

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
        #region [ DB211008 - Дансны төсвийг устгах ]
        public static Result DB211008(DbConnections pDB, long pAccount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CHARTBUDGET WHERE ACCOUNT = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB211008", pAccount);

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
        #region [ DB211009 - Дансны төлөвлөгөө жагсаалт авах ]
        public static Result DB211009(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ACCOUNT, GROUPNO, NAME, NAME2, ORDERNO
FROM CHART
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211009", null);

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
        #region [ DB211010 - Дансны төлөвлөгөө дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB211010(DbConnections pDB, long pAccount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ACCOUNT, GROUPNO, NAME, NAME2, ORDERNO
FROM CHART
WHERE ACCOUNT = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211010", pAccount);

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
        #region [ DB211011 - Дансны төлөвлөгөө шинээр нэмэх ]
        public static Result DB211011(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CHART(ACCOUNT, GROUPNO, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB211011", pParam);

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
        #region [ DB211012 - Дансны төлөвлөгөө засварлах ]
        public static Result DB211012(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CHART SET
GROUPNO=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE ACCOUNT = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB211012", pParam);

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
        #region [ DB211013 - Дансны төлөвлөгөө устгах ]
        public static Result DB211013(DbConnections pDB, long pAccount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CHART WHERE ACCOUNT = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB211013", pAccount);

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
        #region [ DB211014 - Гүйлгээний журнал дэлгэрэнгүй лавлагаа{BAC, CON} ]
        public static Result DB211014(DbConnections pDB, long pJrNo)
        {
            Result res = new Result();
            try
            {
                string sql, where = " WHERE JRNO =: 1 ";

                sql =
@"SELECT 'BAC' txnType, JRNO, SUBJRNO, SEQNO, TXNENTRY, TXNDATE, POSTDATE, ACCOUNTNO, BRANCHNO, PRODCODE, USERNO, 
HOSTNAME, HOSTIP, HOSTMAC, TXNCODE, AMOUNT, RATE, CURCODE, BALANCE, CONTACCCOUNTNO, CONTCURRCODE, 
CONTRATE, CONTAMOUNT, BASEAMOUNT, DESCRIPTION, CORR, ISCASH, SUPERVISOR, FLAG, M
FROM BACTXN "
+ where + @"
union all
SELECT 'CON', JRNO, SUBJRNO, SEQNO, TXNENTRY, TXNDATE, POSTDATE, ACCOUNTNO, BRANCHNO, PRODCODE, USERNO, 
HOSTNAME, HOSTIP, HOSTMAC, TXNCODE, AMOUNT, RATE, CURCODE, BALANCE, CONTACCCOUNTNO, CONTCURRCODE, 
CONTRATE, CONTAMOUNT, BASEAMOUNT, DESCRIPTION, CORR, ISCASH, SUPERVISOR, FLAG, M
FROM CONTXN "
+ where ;

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211014", pJrNo);

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
        #region [ DB211015 - Гүйлгээний журнал дэлгэрэнгүй лавлагаа{PENDTXN} ]
        public static Result DB211015(DbConnections pDB, long pJrNo)
        {
            Result res = new Result();
            try
            {
                string sql, where = " WHERE P.JRNO =: 1 ";
                sql =
@"SELECT P.JRNO, P.JRSUBNO, P.TXNDATE,  P.POSTDATE, P.USERNO, P.HOSTNAME, P.HOSTIP, P.HOSTMAC, P.TXNCODE, P.TXNENTRY,
P.DRACCOUNTMOD, P.DRACCOUNTNO, P.DRAMOUNT, P.DRRATE, P.DRCURCODE, P.DRAMOUNTBALANCE, P.CRACCOUNTMOD, P.CRACCOUNTNO, P.CRAMOUNT, P.CRRATE,
P.CRCURCODE, P.DESCRIPTION, P.EXTIDTYPE, P.EXTID, P.STATUS
FROM PENDTXN P
WHERE P.JRNO =: 1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB211015", pJrNo);
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
        #endregion
    } 
}
