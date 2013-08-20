using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;

namespace IPos.PreSale
{
    public class PreSale : IModule
    {
        DateTime first;
        public static Result F_Error(Result res)
        {
            //int DdIdErrorNo = -2147467259;
            int DdIdErrorNo = 1;

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
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                first = DateTime.Now;
                switch (ri.FunctionNo)
                {
                    #region[Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн]
                    case 130326:	                //	Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн жагсаалт мэдээлэл авах
                        res = Txn130326(ci, ri, db, ref lg);
                        break;
                    case 130327:	                //  Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах
                        res = Txn130327(ci, ri, db, ref lg);
                        break;
                    case 130328:	                //	Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн бүртгэл нэмэх
                        res = Txn130328(ci, ri, db, ref lg);
                        break;
                    case 130329:	                //	Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн бүртгэл засах
                        res = Txn130329(ci, ri, db, ref lg);
                        break;
                    case 130330:	                //	Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн бүртгэл устгах
                        res = Txn130330(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын үндсэн бүртгэл]
                    case 130301:	                //	Урьдчилсан борлуулалтын үндсэн бүртгэл жагсаалт мэдээлэл авах
                        res = Txn130301(ci, ri, db, ref lg);
                        break;
                    case 130302:	                //  Урьдчилсан борлуулалтын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn130302(ci, ri, db, ref lg);
                        break;
                    case 130303:	                //	Урьдчилсан борлуулалтын үндсэн бүртгэл нэмэх
                        res = Txn130303(ci, ri, db, ref lg);
                        break;
                    case 130304:	                //	Урьдчилсан борлуулалтын үндсэн бүртгэл засах
                        res = Txn130304(ci, ri, db, ref lg);
                        break;
                    case 130305:	                //	Урьдчилсан борлуулалтын үндсэн бүртгэл устгах
                        res = Txn130305(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын баталгаажуулах]
                    case 130126:	                //	
                        res = Txn130126(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын цуцлах]
                    case 130127:	                //	
                        res = Txn130127(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын сунгах]
                    case 130128:	                //	
                        res = Txn130128(ci, ri, db, ref lg);
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
                DateTime second = DateTime.Now;
                ISM.Lib.Static.WriteToLogFile("IPos.Contract", "\r\n<<Start:" + Static.ToStr(first) + ">>\r\n UserNo:" + Static.ToStr(ri.UserNo) + "\r\n Description:" + Static.ToStr(lg.item.Desc) + "\r\n ResultNo:" + Static.ToStr(res.ResultNo) + "\r\n ResultDescription:" + ResultDesk + "\r\n<<End " + Static.ToStr(second) + ">>");
            }
        }

        #region [Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн]
        public Result Txn130326(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204421(db);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    //lg.AddDetail("PRESALEMAIN", "ALL", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130327(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204422(db, Static.ToStr(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("PRESALEMAIN", "PRESALEPROD", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        public Result Txn130328(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB204423(db, obj);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130329(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204424(db, ri.ReceivedParam);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн засварлах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALEMAIN", "PRESALEPROD", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130330(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204425(db, Static.ToStr(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн устгах";
                lg.AddDetail("PRESALEMAIN", "PRESALEPROD", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        #endregion
        #region [Урьдчилсан борлуулалтын бүртгэл]
        public Result Txn130301(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204301(db, 1, 1, null);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDERS", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130302(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204302(db, Static.ToStr(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("ORDERS", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        public Result Txn130303(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = (object[])ri.ReceivedParam;

                obj[6] = Static.ToDateTime(DateTime.Now); //Createdate

                res = IPos.DB.Main.DB204303(db, obj, 0, "");
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130304(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204304(db, ri.ReceivedParam);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүртгэл засварлах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130305(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204305(db, Static.ToStr(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын үндсэн бүртгэл устгах";
            }
        }
        #endregion
        #region[Урьдчилсан борлуулалтын баталгаажуулах]
        public Result Txn130126(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string _orderno = Static.ToStr(ri.ReceivedParam[0]);
                int _userno = Static.ToInt(ri.ReceivedParam[1]);
                string _note = Static.ToStr(ri.ReceivedParam[2]);
                DateTime _dte = Static.ToDateTime(DateTime.Now);

                res = IPos.DB.Main.DB204126(db, _orderno, _userno, _note, _dte);
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
                lg.item.Desc = "Захиалгыг баталгаажуулах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDER", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[Урьдчилсан борлуулалтын цуцлах]
        public Result Txn130127(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string _orderno = Static.ToStr(ri.ReceivedParam[0]);
                int _userno = Static.ToInt(ri.ReceivedParam[1]);
                string _note = Static.ToStr(ri.ReceivedParam[2]);
                DateTime _dte = Static.ToDateTime(DateTime.Now);

                res = IPos.DB.Main.DB204127(db, _orderno, _userno, _note, _dte);
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
                lg.item.Desc = "Захиалгыг цуцлах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDER", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[Урьдчилсан борлуулалтын сэргээх]
        public Result Txn130128(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204128(db, Static.ToStr(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Захиалгыг сунгах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDER", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("ORDER", "ExpendDate", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        #endregion
    }
}
