using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using IPos.DB;

namespace IPOS.PostingGL
{
    public class PostingGL : IModule
    {
        #region [ Invoke ]
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 1: 	            
                        res = GetTodayCustomer(ci, ri, db, ref lg);
                        break;
                    case 2: 	           
                        res = GetCustomerInfo(ci, ri, db, ref lg);
                        break;
                    case 3:
                        res = GetTodaySales(ci, ri, db, ref lg);
                        break;
                    case 4:
                        res = GetTodayCashierTxn(ci, ri, db, ref lg);
                        break;
                    //case 4:
                    //    res = UpdateSalesToPost(ci, ri, db, ref lg);
                    //    break;
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
        private Result GetTodayCustomer(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string sql = "";
            try
            {
                //-- Өнөөдөр нийт хэдэн харилцагч гүйлгээ хийсэнийг олж авах
                sql = @"
                     SELECT CustNo
                       FROM glfile
                      WHERE Txndate = :1
                     GROUP BY CustNo";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "GetTodayCustomer", Static.ToDate(ri.ReceivedParam[0]));

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
        private Result GetCustomerInfo(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string sql = "";
            try
            {
                // Харилцагчийн мэдээлэл авах
                sql = @"
                        SELECT C.ClassCode,
                          C.FirstName,
                          C.LastName,
                          C.MiddleName,
                          C.CorporateName,
                          C.CorporateName2,
                          C.RegisterNo,
                          C.Telephone,
                          C.Fax,
                          C.Mobile,
                          C.DirFirstName,
                          C.DirLastName,
                          C.CreateDate,
                          C.EMail,
                          C.WebSite,
                          A.CityCode,
                          A.DistCode,
                          A.SubDistCode,
                          A.Apartment,
                          A.Note
                     FROM    customer C
                          LEFT JOIN
                             CUSTOMERADDR A
                          ON A.CUSTOMERNO = C.CUSTOMERNO AND A.addrcurrent = 1
                    WHERE C.CustomerNo = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "GetCustomerInfo", Static.ToStr(ri.ReceivedParam[0]));

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
        private Result GetTodaySales(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string sql = "";
            try
            {
                // Өнөөдөр нийт хийгдсэн борлуулалтын гүйлгээнүүдийг унших
                sql=@"
                        SELECT SALESNO,
                               TXNDATE,
                               POSTDATE,
                               POSNO,
                               CASHIERNO,
                               SHIFTNO,
                               CUSTNO,
                               CONTRACTNO,
                               SEQNO,
                               ACCOUNTNO,
                               ACCOUNTENTRY,
                               ENTRYTYPE,
                               TXNAMOUNT,
                               CURCODE,
                               DESCRIPTION,
                               PAYMENTTYPE,
                               P.PaymentFlag,
                               DISCOUNT 
                          FROM GLFile G LEFT JOIN papaytype P ON G.paymenttype = P.typeid
                         WHERE Txndate = :1
                      ORDER BY SalesNo, EntryType";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "GetTodaySales", Static.ToDate(ri.ReceivedParam[0]));
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
        private Result GetTodayCashierTxn(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string sql = "";
            try
            {
                // Өнөөдөр хийгдсэн кассын илүүдэл дутагдлын гүйлгээ
                // debt>0 Илүүдэл, -
                sql = @"select T.Trandate, T.UserNo, U.DebitAcntNo, U.CreditAcntNo, T.Debt 
                        From shift T
                        Left join hpuser U on u.userno=T.UserNo
                        where T.status=2 and T.Debt<>0 and T.TxnDate=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "GetTodayCashierTxn", Static.ToDate(ri.ReceivedParam[0]));
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
    }
}