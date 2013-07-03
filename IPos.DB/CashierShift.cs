using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Data;
using EServ.Shared;

namespace IPos.DB
{
    public class CashierShift
    {
        /// Дэвсгэртийн жагсаалт
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB510001(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
select banknote,0 qty 
from pabanknote
where currency=:1
order by 1
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB510001", param);
                #endregion
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

        /// Хэрэглэгчийн сүүлийн ээлжийн дугаарыг олох.
        /// Бичлэг байхгүй бсн ч заавал 1 бичлэг буцаана.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">posno, userno</param>
        /// <returns></returns>
        public static Result DB510002(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
select s1.shiftno, nvl(s2.status,0) status  
from (
    select nvl(max(shiftno),0) shiftno 
    from shift
    where posno=:1 and userno=:2
) s1
left join shift s2 on s2.shiftno=s1.shiftno
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB510002", param);
                #endregion
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

        /// Кассын ээлж эхлүүлэх бичилт
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">postdate,posno,currency,shiftno,banknote,qty,trantype</param>
        /// <returns></returns>
        public static Result DB510003(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
insert into shift (posno,userno,shiftno,status,postdate)
values(:1,:2,:3,:4,:5)
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB510003", param);
                #endregion
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

        /// Кассын бэлэн мөнгөний гүйлгээний бичилт
        /// trantype: 1-Эхний үлдэгдлийн гүйлгээ, 2-Зузаатгалын гүйлгээ, 3-Мөнгө тушаах гүйлгээ, 4-Хаалтын гүйлгээ
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">posno,userno,shiftno,postdate,currency,banknote,qty,trantype</param>
        /// <returns></returns>
        public static Result DB510004(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
insert into shifttran (posno,userno,shiftno,postdate,currency,banknote,qty,trantype)
values(:1,:2,:3,:4,:5,:6,:7,:8)
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB510004", param);
                #endregion
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

        /// Кассын ээлжийн төлвийг өөрчлөх
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">posno,userno,shiftno,status</param>
        /// <returns></returns>
        public static Result DB510005(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
update shift set postdate=:5,status=:4, shiftno=:6 where posno=:1 and userno=:2 and shiftno=:3
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql,  enumCommandType.UPDATE, "DB510005", param);
                #endregion
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

        /// Посын төлвийг авах
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">posno</param>
        /// <returns></returns>
        public static Result DB510006(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"select status, decode(status, 0, 'Нээлттэй', 1, 'Хаалттай') as StatusName from posterminal where posno=:1";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB510006", param);
                #endregion
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

        /// Посын төлвийг өөрчлөх
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">posno,txndate,status</param>
        /// <returns></returns>
        public static Result DB510007(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
update posterminal set txndate=:2,status=:3 where posno=:1
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB510007", param);
                #endregion
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

        /// Дэвсгэрт дэх дэлгэрэнгүй
        /// Кассын ээлж хаахад ашиглана.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">posno,userno,shiftno</param>
        /// <returns></returns>
        public static Result DB510008(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
select a.*,a.open+a.incr-a.decr last,0 cash,0 diff
from (
    select  banknote
    ,sum(decode(trantype,1,nvl(qty,0),0)) open
    ,sum(decode(trantype,2,nvl(qty,0),0)) incr
    ,sum(decode(trantype,3,nvl(qty,0),0)) decr
    from shifttran
    where posno=:1 and userno=:2 and shiftno=:3
    group by banknote
) a
order by 1
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB510008", param);
                #endregion
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
    }
}
