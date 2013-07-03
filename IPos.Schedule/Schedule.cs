using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;

namespace IPos.Schedule
{
    public class Schedule : IModule
    {
        DateTime first;
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {	
                first = DateTime.Now;
                switch (ri.FunctionNo)
                {
                    case 140206:	                //	Календарын код жагсаалт мэдээлэл авах
                        res = Txn140206(ci, ri, db, ref lg);
                        break;
                    case 140207:	                //  Календарын дэлгэрэнгүй мэдээлэл авах
                        res = Txn140207(ci, ri, db, ref lg);
                        break;
                    case 140208:	                //	Календарын бүртгэл нэмэх
                        res = Txn140208(ci, ri, db, ref lg);
                        break;
                    case 140209:	                //	Календарын бүртгэл засварлах
                        res = Txn140209(ci, ri, db, ref lg);
                        break;
                    case 140210:	                //	Календарын бүртгэл устгах
                        res = Txn140210(ci, ri, db, ref lg);
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
            finally
            {
                string ResultDesk = "OK";
                if (res.ResultNo != 0)
                {
                    ResultDesk = res.ResultDesc;
                }
                DateTime second = DateTime.Now;
                ISM.Lib.Static.WriteToLogFile("IPos.Schedule", "\r\n<<Start:" + Static.ToStr(first) + ">>\r\n UserNo:" + Static.ToStr(ri.UserNo) + "\r\n Description:" + Static.ToStr(lg.item.Desc) + "\r\n ResultNo:" + Static.ToStr(res.ResultNo) + "\r\n ResultDescription:" + ResultDesk + "\r\n<<End " + Static.ToStr(second) + ">>");
            }
        }
        #region [Календар ]
        public Result Txn140206(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202206(db, (DateTime)ri.ReceivedParam[0], (DateTime)ri.ReceivedParam[1]);
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
                lg.item.Desc = "Календарын жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PaCalendar", "StartDate", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("PaCalendar", "EndDate", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        public Result Txn140207(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                customerid = Static.ToLong(ri.ReceivedParam[0]);
                seqid = Static.ToLong(ri.ReceivedParam[1]);
                //res = IPos.DB.Main.DB205057(db, customerid, seqid);
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
                    lg.item.Desc = "";
                }
            }
        }
        public Result Txn140208(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] obj = new object[7];
            obj = ri.ReceivedParam;
            object[] prop = new object[6];
            string errorlist = "";
            try
            {
                if (ri.ReceivedParam[0].GetType() == typeof(DataTable))
                {
                    DataTable dt=(DataTable)ri.ReceivedParam[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToStr(dr["DAY"]) != "")
                        {
                            res = IPos.DB.Main.DB202210(db, Static.ToDate(dr["DAY"]));
                            if (res.ResultNo != 0)
                                return res;
                            prop[0] = Static.ToDate(dr["DAY"]);
                            prop[1] = dr["DAYTYPE"];
                            prop[2] = dr["DayTemperature"];
                            prop[3] = dr["DayWeatherType"];
                            prop[4] = dr["NightTemperature"];
                            prop[5] = dr["NightWeatherType"];
                            res = IPos.DB.Main.DB202208(db, prop);
                            if (res.ResultNo != 0)
                                return res;
                        } 
                    }
                }
                else
                {
                    DateTime startdate = Static.ToDate(obj[0]);
                    DateTime enddate = Static.ToDate(obj[1]);

                    prop[0] = startdate;
                    prop[1] = obj[2];
                    prop[2] = obj[3];
                    prop[3] = obj[4];
                    prop[4] = obj[5];
                    prop[5] = obj[6];

                    res = IPos.DB.Main.DB202208(db, prop);
                    if (res.ResultNo == 9110039)
                    {
                        errorlist = errorlist + "\r\n" + startdate.ToShortDateString();
                    }
                    for (; ; )
                    {
                        if (startdate != enddate)
                        {

                            startdate = startdate.AddDays(1);
                            prop[0] = startdate;
                            prop[1] = obj[2];
                            prop[2] = obj[3];
                            prop[3] = obj[4];
                            prop[4] = obj[5];
                            prop[5] = obj[6];
                            res = IPos.DB.Main.DB202208(db, prop);
                            if (res.ResultNo == 9110039)
                            {
                                errorlist = errorlist + "\r\n" + startdate.ToShortDateString();
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (errorlist != "")
                        res.ResultDesc = errorlist;
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
                lg.item.Desc = "Календар бүртгэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn140209(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] obj = new object[7];
            obj = ri.ReceivedParam;
            object[] prop = new object[6];
            try
            {
                DateTime startdate = Static.ToDate(obj[0]);
                DateTime enddate = Static.ToDate(obj[1]);

                prop[0] = startdate;
                prop[1] = obj[2];
                prop[2] = obj[3];
                prop[3] = obj[4];
                prop[4] = obj[5];
                prop[5] = obj[6];

                res = IPos.DB.Main.DB202209(db, prop);
                for (; ; )
                {
                    if (startdate != enddate)
                    {

                        startdate = startdate.AddDays(1);
                        prop[0] = startdate;
                        prop[1] = obj[2];
                        prop[2] = obj[3];
                        prop[3] = obj[4];
                        prop[4] = obj[5];
                        prop[5] = obj[6];
                        res = IPos.DB.Main.DB202209(db, prop);
                    }
                    else
                    {
                        break;
                    }
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
                lg.item.Desc = "Календар засварлах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn140210(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                res = IPos.DB.Main.DB202210(db, Static.ToDate(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Харилцагчийн нэмэлт мэдээлэл устгах";
            }
        }
        #endregion
    }
}
