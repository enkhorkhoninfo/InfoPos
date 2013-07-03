using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EServ.Data;
using EServ.Shared;
using EServ.Interface;

using IPos.DB;
using IPos.Core;

namespace IPos.Enquiry
{
    public class Enquiry: IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime startdate = new DateTime();
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    #region [ Үндсэн хөрөнгө]
                    case 140212: 	                        // Бараа материалын дэлгэрэнгүй мэдээлэл авах
                        res = Txn140212(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Харилцагчийн лавлагаа ]
                    case 120001: 	            // Үндсэн мэдээлэл
                        res = Txn120001(ci, ri, db, ref lg);
                        break;
                    case 120006: 	            // Хаяг
                        res = Txn120006(ci, ri, db, ref lg);
                        break;
                    case 120011: 	            // Зураг
                        res = Txn120011(ci, ri, db, ref lg);
                        break;
                    case 120021: 	            // Хамаатан садан
                        res = Txn120021(ci, ri, db, ref lg);
                        break;
                    case 120036: 	            // Холбоо барисан мэдээлэл
                        res = Txn120036(ci, ri, db, ref lg);
                        break;
                    case 120041: 	            // Дансны мэдээлэл
                        res = Txn120041(ci, ri, db, ref lg);
                        break;
                    case 120046: 	            // Хавсралт файлууд
                        res = Txn120046(ci, ri, db, ref lg);
                        break;
                    case 120051: 	            // Товч дүгнэлт
                        res = Txn120051(ci, ri, db, ref lg);
                        break;
                    #endregion
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
            finally
            {
                string ResultDesk = "OK";
                if (res.ResultNo != 0)
                {
                    ResultDesk = res.ResultDesc;
                }
                DateTime enddate = DateTime.Now;
                ISM.Lib.Static.WriteToLogFile("IPos.Enquiry", "\r\n<<Start:" + Convert.ToString(startdate) + ">>\r\n UserNo:" + Convert.ToString(ri.UserNo) + "\r\n Description:" + Convert.ToString(lg.item.Desc) + "\r\n ResultNo:" + Convert.ToString(res.ResultNo) + "\r\n ResultDescription:" + ResultDesk + "\r\n<<End " + Convert.ToString(enddate) + ">>");
            }
        }
        public Result Txn140212(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Бараа материалын дэлгэрэнгүй мэдээлэл авах
        {
            Result res = new Result();
            string pInvID = Static.ToStr(ri.ReceivedParam[0]);
            try
            {
                res = IPos.DB.Main.DB202212(db, pInvID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Бараа материалын дэлгэрэнгүй лавлагаа";
                    lg.AddDetail("Enquiry", "InvMain", "Бараа материалын дэлгэрэнгүй лавлагаа", pInvID.ToString());
                }
            }
        }
        #region [ Харилцагчийн лавлагаа ]
        public Result Txn120001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Харилцагчийн лавлагаа ( Үндсэн мэдээлэл )
        {
            Result res = new Result();
            long CustomerID = Static.ToLong(ri.ReceivedParam[0]);  //CustomerID
            try
            {
                
                res = IPos.DB.Main.DB205002(db, CustomerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Харилцагчийн үндсэн мэдээллийн лавлагаа";
                    lg.AddDetail("Enquiry", "Customer", "Харилцагчийн үндсэн мэдээллийн лавлагаа", CustomerID.ToString());
                }
            }
        }
        public Result Txn120006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Харилцагчийн лавлагаа ( Хаяг )
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            long CustomerID = Static.ToLong(ri.ReceivedParam[0]);
            try
            {
                
                object[] obj = new object[8];
                obj[0] = CustomerID;
                res = IPos.DB.Main.DB205006(db, pagenumber, pagecount, CustomerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Харилцагчийн хаягийн лавлагаа";
                    lg.AddDetail("Enquiry", "Position", "Харилцагчийн хаягийн лавлагаа", CustomerID.ToString());
                }
            }
        }
        public Result Txn120011(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Харилцагчийн лавлагаа ( Зураг)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            long CustomerID = Static.ToLong(ri.ReceivedParam[0]);
            try
            {
                  //CustomerID
                res = IPos.DB.Main.DB205011(db, pagenumber, pagecount, CustomerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Харилцагчийн зургийн лавлагаа";
                    lg.AddDetail("Enquiry", "Picture", "Харилцагчийн зургийн лавлагаа", CustomerID.ToString());
                }
            }
        }
        public Result Txn120021(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Харилцагчийн лавлагаа ( Хамаатан садан)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            long CustomerID = Static.ToLong(ri.ReceivedParam[0]);  //CustomerID
            try
            {
                
                res = IPos.DB.Main.DB205021(db, pagenumber, pagecount, CustomerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Харилцагчийн хамаатан садангийн лавлагаа";
                    lg.AddDetail("Enquiry", "Family", "Харилцагчийн хамаатан садангийн лавлагаа", CustomerID.ToString());
                }
            }
        }
        public Result Txn120036(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Харилцагчийн лавлагаа ( Холбоо барисан мэдээлэл)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            long CustomerID = Static.ToLong(ri.ReceivedParam[0]);  //CustomerID
            try
            {
                res = IPos.DB.Main.DB205036(db, pagenumber, pagecount, CustomerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Харилцагчийн холбоо барисан лавлагаа";
                    lg.AddDetail("Enquiry", "Contact", "Харилцагчийн холбоо барисан лавлагаа", CustomerID.ToString());
                }
            }
        }
        public Result Txn120041(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Харилцагчийн лавлагаа ( Дансны мэдээлэл)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            long CustomerID = Static.ToLong(ri.ReceivedParam[0]);  //CustomerID
            try
            {
                
                res = IPos.DB.Main.DB205041(db, pagenumber, pagecount, CustomerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Харилцагчийн дансны мэдээлэл лавлагаа";
                    lg.AddDetail("Enquiry", "Account", "Харилцагчийн дансны мэдээлэл лавлагаа", CustomerID.ToString());
                }
            }
        }
        public Result Txn120046(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Харилцагчийн лавлагаа ( Хавсралт файлууд)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            long CustomerID = Static.ToLong(ri.ReceivedParam[0]);  //CustomerID
            try
            {
                
                res = IPos.DB.Main.DB205046(db, pagenumber, pagecount, CustomerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Харилцагчийн хавсралт файлуудын лавлагаа";
                    lg.AddDetail("Enquiry", "Attach", "Харилцагчийн хавсралт файлуудын лавлагаа", CustomerID.ToString());
                }
            }
        }
        public Result Txn120051(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)     // Харилцагчийн лавлагаа ( Товч дүгнэлт)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            long CustomerID = Static.ToLong(ri.ReceivedParam[0]);  //CustomerID
            try
            {
                
                res = IPos.DB.Main.DB205051(db, pagenumber, pagecount, CustomerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Харилцагчийн товч дүгнэлтийн лавлагаа";
                    lg.AddDetail("Enquiry", "Note", "Харилцагчийн товч дүгнэлтийн лавлагаа", CustomerID.ToString());
                }
            }
        }
        #endregion
    }
}
