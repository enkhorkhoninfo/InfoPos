using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using System.Data;

using IPos.DB;
using IPos.Core;

namespace IPos.Pos
{
    public class Pos : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime first = new DateTime();
            Result res = new Result();
            try
            {
                first = DateTime.Now;
                switch (ri.FunctionNo)
                {
                    case 211001: 	             //Пос терминалын мэдээлэл
                        res = Txn211001(ci, ri, db, ref lg);
                        break;
                    case 211002: 	             //Кассын нэгдсэн дүнгүүд, ээлжийн мэдээлэл
                        res = Txn211002(ci, ri, db, ref lg);
                        break;

                    case 211003: 	             //Касс зузаатгах
                        res = Txn211003(ci, ri, db, ref lg);
                        break;
                    case 211004: 	             //Касс тушаах
                        res = Txn211004(ci, ri, db, ref lg);
                        break;
                    case 211005: 	             //Ээлж нээх
                        res = Txn211005(ci, ri, db, ref lg);
                        break;
                    case 211006: 	             //Ээлж хаах
                        res = Txn211006(ci, ri, db, ref lg);
                        break;

                    case 211007: 	             //ПОС нээх
                        res = Txn211007(ci, ri, db, ref lg);
                        break;
                    case 211008: 	             //ПОС хаах
                        res = Txn211008(ci, ri, db, ref lg);
                        break;

                    case 211009: 	             //Ээлж хаах биллийн мэдээлэл
                        res = Txn211009(ci, ri, db, ref lg);
                        break;
                    case 211010: 	             //ПОС хаах биллийн мэдээлэл
                        res = Txn211010(ci, ri, db, ref lg);
                        break;
                    case 211011: 	             //Кассын хөдөлгөөний лавлагаа
                        res = Txn211011(ci, ri, db, ref lg);
                        break;

                    
                    case 211099:
                        res = Txn211099(ci, ri, db, ref lg); //Version description
                        break;



                    case 510001: 	             //Дэвсгэртийн жагсаалт
                        res = Txn510001(ci, ri, db, ref lg);
                        break;
                    case 510003: 	             //Кассын ээлж эхлүүлэх бичилт
                        res = Txn510003(ci, ri, db, ref lg);
                        break;
                    case 510004: 	             //Кассын ээлжийн бэлэн мөнгөний гүйлгээ
                        res = Txn510004(ci, ri, db, ref lg);
                        break;
                    case 510005: 	            //Кассын ээлжийг хаах
                        res = Txn510005(ci, ri, db, ref lg);
                        break;
                    case 510006:                //Посын төлвийг авах
                        res = Txn510006(ci, ri, db, ref lg);
                        break;
                    case 510007:                //Посын төлвийг өөрчлөх
                        res = Txn510007(ci, ri, db, ref lg);
                        break;
                    case 510008:                //Дэвсгэрт дэх дэлгэрэнгүй
                        res = Txn510008(ci, ri, db, ref lg);
                        break;
                    case 510009:
                        res = Txn510009(ci, ri, db, ref lg); //Ажилтнй жагсаалт авах
                        break;
                    case 510010:
                        res = Txn510010(ci, ri, db, ref lg); //Асуудлын жагсаалт авах
                        break;
                    case 510011:
                        res = Txn510011(ci, ri, db, ref lg);
                        break;
                    case 510012:
                        res = Txn510012(ci, ri, db, ref lg); // Кассын ээлжийн төлөв олох
                        break;
                    case 510013:
                        res = Txn510013(ci, ri, db, ref lg); // Биллийн дугаар олох
                        break;
                    case 510014:
                        res = Txn510014(ci, ri, db, ref lg); // Төлбөрийн төрлөөр дүн олох                        
                        break;
                    case 510015:
                        res = Txn510015(ci, ri, db, ref lg);
                        break;
                    case 510016:
                        res = Txn510016(ci, ri, db, ref lg);
                        break;
                    case 510017:
                        res = Txn510017(ci, ri, db, ref lg);
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
                if (res != null && res.ResultNo != 0)
                {
                    ResultDesk = res.ResultDesc;
                }
                DateTime second = DateTime.Now;

                ISM.Lib.Static.WriteToLogFile("IPos.List", "\r\n<<Start:" + Static.ToStr(first) + ">>\r\n UserNo:" + ISM.Lib.Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + Static.ToStr(ri.UserNo) + "\r\n Description:" + Static.ToStr(lg.item.Desc) + "\r\n ResultNo:" + Static.ToStr(res.ResultNo) + "\r\n ResultDescription:" + ResultDesk + "\r\n<<End " + Static.ToStr(second) + ">>");
            }
        }
        #region [ Business functions -- OLD, USED FOR BAZARAA!!!  ]
        public Result Txn510001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CashierShift.DB510001(db, ri.PageIndex, ri.PageRows, new object[] { "MNT" });
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
            }
        }    //Дэвсгэртийн жагсаалт     
        public Result Txn510003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DbConnection con = db.BeginTransaction("core", "");
            Result res = new Result();
            int shNo = 1;
            int status = 0;
            try
            {
                #region[сүүлийн ээлжийн дугаарыг олох]
                object[] obj = new object[2];
                obj = (object[])ri.ReceivedParam[0];
                string sql = @"select  shiftno,status from  shift  where  posno=:1  and  userno=:2  order  by  postdate desc";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "", obj);
                if (res.Data.Tables[0].Rows.Count != 0)
                {
                    shNo = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                     status = Static.ToInt(res.Data.Tables[0].Rows[0]["STATUS"]);
                    shNo = shNo + 1;
                }
                #endregion[]
                
                
                object[] posValue = new object[3];
                posValue[0] = Static.ToStr(obj[0]);
                posValue[1] = DateTime.Now;
                posValue[2] = 0;

                object[] pValue = new object[5];
                pValue[0] = Static.ToStr(obj[0]);
                pValue[1] = Static.ToStr(obj[1]);
                pValue[2] = shNo;
                pValue[3] = 1;
                pValue[4] = DateTime.Now;

                res = IPos.DB.CashierShift.DB510007(db, ri.PageIndex, ri.PageRows, posValue);

                if (res.ResultNo == 0)
                {
                    if (status != 1)
                    {
                        res = IPos.DB.CashierShift.DB510003(db, ri.PageIndex, ri.PageRows, pValue);
                        if (res.ResultNo == 0)
                        {
                            res.ResultDesc = "Пос нээгдлээ.\n Кассын ээлж амжилттай нээгдлээ";
                        }
                        else 
                        {
                            return res;
                        }
                    }
                    else
                    {
                        res.ResultDesc = "Кассын ээлж эхэлсэн төлөвтэй байна.";
                        res.ResultNo = 911002;
                        return res;
                    }
                }
                else
                {
                    res.ResultDesc = "Пос нээхэд алдаа гарлаа"+ res.ResultDesc;
                    res.ResultNo = 911002;
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
                if (res.ResultNo == 0)
                {
                    con.Commit();
                    lg.item.Desc = "Кассын ээлж эхлүүлэх бичилт";
                }
                else                 
                { 
                    con.Rollback(); 
                    lg.item.Desc = "Кассын ээлж эхлүүлэх бичилт"; 
                }
            }
        }    //Кассын ээлж эхлүүлэх бичилт
        public Result Txn510004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {           
            Result res = new Result();
            try
            {
                object[] obj = new object[3];
                obj = (object[])ri.ReceivedParam[0];

                object[] prop = new object[2];
                prop[0] = obj[0];
                prop[1] = obj[1];
                #region[ сүүлийн ээлжийн дугаарыг олох]
                res = IPos.DB.CashierShift.DB510002(db, ri.PageIndex, ri.PageRows, prop);
                int shNo = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                int status = Static.ToInt(res.Data.Tables[0].Rows[0]["STATUS"]);
                #endregion[]
                if (status == 1)
                {
                    object[] grd = new object[8];
                    DataTable dt = (DataTable)ri.ReceivedParam[1];

                    foreach (DataRow DR in dt.Rows)
                    {
                        grd[0] = Static.ToStr(obj[0]);
                        grd[1] = Static.ToStr(obj[1]);
                        grd[2] = shNo;
                        grd[3] = DateTime.Now;
                        grd[4] = "MNT";
                        grd[5] = Static.ToInt(DR["BANKNOTE"]);
                        grd[6] = Static.ToInt(DR["QTY"]);
                        grd[7] = Static.ToInt(obj[2]);

                        res = IPos.DB.CashierShift.DB510004(db, ri.PageIndex, ri.PageRows, grd);
                    }
                    if (res.ResultNo == 0)
                    {
                        res.ResultDesc = "Кассын бэлэн мөнгөний гүйлгээний бичилт амжилттай хийгдлээ.";
                    }
                    else 
                    { 
                        res.ResultDesc = "Кассын бэлэн мөнгөний гүйлгээний бичилт хийхэд алдаа гарлаа."+ res.ResultDesc;
                        res.ResultNo = 911002;
                    }
                }
                else
                {
                    res.ResultDesc = "Ээлж эхлээгүй байна.";
                    res.ResultNo = 911002;
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
                if (res.ResultNo == 0)
                {                   
                    lg.item.Desc = "Кассын бэлэн мөнгөний гүйлгээний бичилт";
                }
                else
                {
                    lg.item.Desc = "Кассын бэлэн мөнгөний гүйлгээний бичилт";
                }
            }
        }    //Кассын ээлжийг гүйлгээний бичилт
        public Result Txn510005(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            DbConnection con = db.BeginTransaction("core", "");
            object[] pParam = ri.ReceivedParam;
            try
            {                
                #region[ сүүлийн ээлжийн дугаарыг олох]
                object[] obj = new object[5];
                obj = (object[])ri.ReceivedParam[0];

                object[] obj1 = new object[2];
                obj1[0] = Static.ToStr(obj[0]);
                obj1[1] = Static.ToStr(obj[1]);
                string sql = @"select  shiftno,status from  shift  where  posno=:1  and  userno=:2  order  by  postdate desc";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "", obj1);

                int shNo = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                int status = Static.ToInt(res.Data.Tables[0].Rows[0]["STATUS"]);
                #endregion[]                
                //Кассын ээлжийн төлвийг өөрчлөх
                #region[Гүйлгээний бичилтийн утгууд]
                object[] grd = new object[8];
                DataTable dt = (DataTable)ri.ReceivedParam[1];

                foreach (DataRow DR in dt.Rows)
                {
                    grd[0] = Static.ToStr(obj[0]);
                    grd[1] = Static.ToStr(obj[1]);
                    grd[2] = shNo;
                    grd[3] = DateTime.Now;
                    grd[4] = "MNT";
                    grd[5] = Static.ToInt(DR["BANKNOTE"]);
                    grd[6] = Static.ToInt(DR["CASH"]);
                    grd[7] = 4;
                #endregion[]
                    if (status != 2)
                    {
                        object[] pValue = new object[5];
                        pValue[0] = Static.ToStr(obj[0]);
                        pValue[1] = Static.ToStr(obj[1]);
                        pValue[2] = Static.ToInt(shNo);
                        pValue[3] = 2;
                        pValue[4] = DateTime.Now;

                        res = IPos.DB.CashierShift.DB510003(db, ri.PageIndex, ri.PageRows, pValue);
                        if (res.ResultNo == 0)
                        {
                            res = IPos.DB.CashierShift.DB510004(db, ri.PageIndex, ri.PageRows, grd);
                            if (res.ResultNo == 0)
                            {
                                res.ResultDesc = "Кассын ээлжийн хаалтын бэлэн мөнгөний гүйлгээ амжилттай хийгдлээ";
                                return res;
                            }
                            else
                            {
                                res.ResultDesc = "Кассын ээлж хаахад алдаа гарлаа.";
                                res.ResultNo = 911002;
                                return res;
                            }
                        }
                        else
                        {
                            res.ResultDesc = "Кассын ээлж хаахад алдаа гарлаа."+ res.ResultDesc;
                            res.ResultNo = 911002;
                            return res;
                        }
                        
                    }
                    else
                    {
                        res.ResultDesc = "Кассын ээлж хаагдсан дахин хаах боломжгүй";
                        res.ResultNo = 911002;
                        return res;
                    }                    
                }
                
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    con.Commit();
                    lg.item.Desc = "Кассын ээлжийн төлвийг өөрчлөх";
                }
                else { con.Rollback(); }
            }

        }    //Кассын ээлжийг хаах
        public Result Txn510006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.CashierShift.DB510006(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
                    lg.item.Desc = "Захиалгын жагсаалт авах";
                }
            }
        }    //Посын төлвийг авах
        public Result Txn510007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[2];
                obj = (object[])ri.ReceivedParam[1];
                string sql = @"select  shiftno,status from  shift  where  posno=:1  and  userno=:2  order  by  postdate desc";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "", obj);
                if (res.ResultNo == 0)
                {
                        if (Static.ToInt(res.Data.Tables[0].Rows[0]["status"]) == 1)
                        {
                            res.ResultDesc = "Кассын ээлж хаана уу.";
                            res.ResultNo = 911002;
                        }
                        else
                        {
                            res = IPos.DB.CashierShift.DB510007(db, ri.PageIndex, ri.PageRows, (object[])ri.ReceivedParam[0]);
                            if (res.ResultNo == 0)
                            {
                                res.ResultDesc = "Пос амжилттай хаалаа.";
                            }
                            else
                            {
                                res.ResultDesc = "Пос хаахад алдаа гарлаа.\n" + res.ResultDesc;
                                res.ResultNo = 911002;
                            }
                        }                        
                }
                else
                {
                    res.ResultDesc = "Посын төлөв олдсонгүй.";
                    res.ResultNo = 911002;
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
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Посын төлвийг өөрчлөх";
                }
            }


        }    //Посын төлвийг өөрчлөх    
        public Result Txn510008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region[Ээлжийн дугаар авах]
                object[] pParam = ri.ReceivedParam;

                object[] obj1 = new object[2];
                obj1[0] = ri.ReceivedParam[0];
                obj1[1] = ri.ReceivedParam[1];

                res = IPos.DB.CashierShift.DB510002(db, ri.PageIndex, ri.PageRows, obj1);

                int shNo = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                //shNo = shNo + 1;
                #endregion[]
                #region[Дэвсгэрт дэх дэлгэрэнгүй]
                object[] pParamV = ri.ReceivedParam;
                object[] obj = new object[3];
                obj[0] = ri.ReceivedParam[0];
                obj[1] = ri.ReceivedParam[1];
                obj[2] = shNo;

                res = IPos.DB.CashierShift.DB510008(db, ri.PageIndex, ri.PageRows, obj);
                return res;
            }
                #endregion[]
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
                    lg.item.Desc = "Дэвсгэрт дэх дэлгэрэнгүй";
                }


            }
        }    //Дэвсгэрт дэх дэлгэрэнгүй  
        public Result Txn510009(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
                     @"SELECT *FROM hpuser";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB510009", null);
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
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";

            }
        }    //Ажилтын жагсаалт авах 
        public Result Txn510010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"select * from feedback where userno=:1";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn510010", ri.ReceivedParam[0]);
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

            }
        }    //Асуудлын жагсаалт
        public Result Txn510011(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"insert into feedback(ID, USERNO, TYPE, SUBJECT, DESCRIPTION, CREATEDATE, POSTDATE) values(:1, :2, :3, :4, :5, :6, :7)";
                object[] obj = ri.ReceivedParam;
                obj[0] = Static.ToLong(EServ.Interface.Sequence.NextByVal("FEEDBACKID"));
                obj[6] = DateTime.Now;
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn510011", obj);

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
            }
        }    //Асуудал нэмэх
        public Result Txn510012(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {                                      
                object[] obj = new object[2];
                obj[0] = Static.ToInt(ri.ReceivedParam[0]);
                obj[1] = Static.ToInt(ri.ReceivedParam[1]);
                res = IPos.DB.CashierShift.DB510002(db, ri.PageIndex, ri.PageRows, obj);
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

            }
        }
        public Result Txn510013(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {

                string sql = @"select *from workarealink where type = 0 and id = :1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn510013", ri.ReceivedParam[0]);
                if (res.ResultNo == 0)
                {
                    if (res.Data == null) { res.ResultNo = 911002; res.ResultDesc = "Ажлын талбарт ийм дугаартай пос бүртгэлгүй байна."; return res; }
                    if (res.Data.Tables[0].Rows.Count == 0) { res.ResultNo = 911002; res.ResultDesc = "Ажлын талбарт ийм дугаартай пос бүртгэлгүй байна."; return res; }
                    string areacode = Static.ToStr(res.Data.Tables[0].Rows[0]["AREACODE"]);
                    string _billno = "";
                    IPos.Core.AutoNumEnum enums = new AutoNumEnum();
                    enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                    Result Batchres = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 13, "", enums);

                    if (Batchres.ResultNo == 0)
                    {
                        _billno = Batchres.ResultDesc;
                        if (_billno == "")
                        {
                            Batchres.ResultNo = 9110068;
                            Batchres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:12][" + Batchres.ResultDesc + "]";
                            return Batchres;
                        }
                        res.ResultDesc = string.Format("{0}-{1}-{2}", areacode, ri.ReceivedParam[0], _billno);
                    }
                    else
                    {
                        return Batchres;
                    }

                }
                else return res;
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
            }
        }
        public Result Txn510014(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"select  B.PAYMENTNO,c.paymenttype,d.name,c.amount,c.contractno,b.chargeamount from salespayment  b
left  join  salespaymentdetail  c on  c.paymentno=b.paymentno
left  join  papaytype  d on  D.TYPEID=C.PAYMENTTYPE
where  b.salesno=:1 and  c.paymentno is  not  null";

                string _batchno = ri.ReceivedParam[0].ToString();

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "510014", new object[] { _batchno });
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }    //Төлбөрийн төрлөөр биллийн дүн олох        
        public Result Txn510015(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try 
            {
                res = IPos.DB.CashierShift.DB510002(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);                
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
        public Result Txn510016(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try 
            {
                object[] obj = new object[4];
                obj[0] = ri.ReceivedParam[0];
                obj[1] = ri.ReceivedParam[1];
                obj[2] = ri.ReceivedParam[2];
                obj[3] = DateTime.Now;
                string sql = @"select kk.*,dd.name 
from (select c.paymenttype,sum(c.amount),sum(b.chargeamount) from salesmain sm
left join sales a on a.salesno=sm.salesno
left join salespayment b on sm.batchno=b.salesno
left join salespaymentdetail c on b.paymentno=c.paymentno
where a.posno=:1 and a.cashierno=:2 and a.postdate between (select postdate from shift where posno=:1 and userno=:2 and shiftno=:3 and status = 1) and :4 group by c.paymenttype) kk
left join papaytype dd on kk.paymenttype=dd.typeid";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "510016", obj);
                return res;                
            }
            catch(Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа."+ ex.StackTrace+ex.Source+ex.Message;
                return res;
            }
        }
        public Result Txn510017(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"select  SUM(c.amount) as amount, SUM(B.CHARGEAMOUNT) as chargeamount, SUM(d.totalamount) as totalamount,  SUM(d.vat) as vat, SUM(d.salesamount) as salesamount from  salesmain   a
left join sales d on d.salesno=a.salesno
left  join  salespayment  b on  b.salesno=a.batchno
left  join  salespaymentdetail  c on  c.paymentno=b.paymentno
where  a.batchno=:1  and  c.paymentno is  not  null";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn510017", ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message+ ex.Source + ex.StackTrace;
                res.ResultNo = 911002;
                return res;
            }
        }    //Нийт төлсөн дүн, Хариулт ....

        #endregion[]

        #region Pos functions new. Amaraa issued.

        public Result GetPosInfo(DbConnections db, string posno, out DataRow row)
        {
            DataRow r = null;
            string sql = @"select p.posno,p.posname,p.posdesc,p.posip,p.posmac,p.postype
,p.shiftno,p.shiftuserno,p.status,p.trandate
,decode(u.userfname,null,u.userlname,substr(u.userfname,1,1)||'.'||u.userlname) username
from posterminal p
left join hpuser u on u.userno=p.shiftuserno 
where p.posno=:1";
            object[] param = new object[] { posno };
            Result res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211005", param);
            if (res.ResultNo != 0) goto OnExit;
            if (res.AffectedRows <= 0) res = new Result(2110011, "Ээлж нээгдээгүй байна!");
            else r = res.Data.Tables[0].Rows[0];

        OnExit:
            row = r;
            return res;
        }

        public Result Txn211001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                #region Prepare parameters
                string posno = Static.ToStr(ri.ReceivedParam[0]);
                string areacode = Static.ToStr(ri.ReceivedParam[1]);
                #endregion
                #region Pos info
                DataRow r = null;
                res = GetPosInfo(db, posno, out r);
                if (res != null && res.ResultNo != 0) goto OnExit;
                dt = res.Data.Tables[0];
                dt.TableName = "PosInfo";
                ds.Tables.Add(dt.Copy());
                #endregion
                #region Day type info
                string sql = @"select daytype
,daytemperature,w1.description daydesc,w1.icon dayicon
,nighttemperature,w2.description nightdesc,w2.icon nighticon 
from pacalendar c
left join paweather w1 on w1.weatherid=c.dayweathertype
left join paweather w2 on w2.weatherid=c.nightweathertype
where day=:1";
                object[] param = new object[] { SystemProp.TxnDate };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211001", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                dt = res.Data.Tables[0];
                dt.TableName = "DayType";
                ds.Tables.Add(dt.Copy());
                #endregion
                #region Тухайн AreaCode дээрх барааны жагсаалт

                sql = @"select a.parentid, a.itemid, a.itemtype
,case 
when a.itemtype=0 then (select name from invmain where invid=a.itemid)
when a.itemtype=1 then (select name from servmain where servid=a.itemid)
when a.itemtype=2 then (select name from packmain where packid=a.itemid)
when a.itemtype=3 then (select name from prodtreedesc where itemid=a.itemid)
end itemname
,case 
when a.itemtype=0 then (select picture from invmain where invid=a.itemid)
when a.itemtype=1 then (select picture from servmain where servid=a.itemid)
end itempicture
from prodtree a
left join workarealink w on w.areacode=:1 and w.id=a.itemid and w.producttype=a.itemtype
where a.itemtype=3 or (w.id is not null)
order by 3 desc,4
";
                param = new object[] { areacode };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211001", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                dt = res.Data.Tables[0];
                dt.TableName = "ProductInfo";
                ds.Tables.Add(dt.Copy());

                #endregion
                res.Data = ds;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
            }
            OnExit:
            return res;
        }    //Пос терминалын мэдээлэл
        public Result Txn211002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);
                int shiftno = Static.ToInt(ri.ReceivedParam[1]);
                
                #endregion

                #region SQL - Кассын нийт дүн

                string sql = @"
select * from 
(
    select sum(decode(pt.paymentflag,0,amount,0)) cash_amount
    ,count(distinct decode(pt.paymentflag,0,st.salesno,null)) cash_count
    ,sum(decode(pt.paymentflag,1,amount,0)) card_amount
    ,count(distinct decode(pt.paymentflag,1,st.salesno,null)) card_count
    ,sum(decode(pt.paymentflag,0,0,1,0,amount)) other_amount
    ,count(distinct decode(pt.paymentflag,0,null,1,null,st.salesno)) other_count
    from salestxn st
    left join salesaction sa on sa.actionid=st.actionid
    left join papaytype pt on pt.typeid=st.paymenttype
    where sa.trandate=:1 and sa.posno=:2 and sa.shiftno=:3
) a,
(
    select 
    sum(decode(sf.trantype,1,sf.qty*sf.banknote,0)) cashin_amount
    ,count(distinct decode(sf.trantype,1,sf.postdate,null)) cashin_count
    ,sum(decode(sf.trantype,2,sf.qty*sf.banknote,0)) cashout_amount
    ,count(distinct decode(sf.trantype,2,sf.postdate,null)) cashout_count
    ,max(pos.posname) posname
    from shifttran sf
    left join posterminal pos on pos.posno=sf.posno
    where sf.trandate=:1 and sf.posno=:2 and sf.shiftno=:3
) b,
(
    select sum(salesamount-discountprod-discountsales) total_amount
    ,count(distinct salesno) sales_count
    ,sum(decode(sp.flag,'R',salesamount-discountprod-discountsales,0)) refund_amount
    ,count(distinct decode(sp.flag,'R',salesno,null)) refund_count
    from salesprod sp
    left join salesaction sa on sa.actionid=sp.actionid
    where sa.trandate=:1 and sa.posno=:2 and shiftno=:3
) c
";
                #region old sql
                //                string sql = @"
//select 
//b.cashsupply,b.cashsupplyc
//,b.cashdraw,b.cashdrawc
//,a.cashin,a.cashinc
//,a.cashexch,a.cashexchc
//,a.cashrefund,a.cashrefundc
//,b.cashsupply+b.cashdraw+a.cashin+a.cashexch+a.cashrefund cashremain
//from 
//(
//    select 
//    sum(case when st.flag='N' and p.paymentflag=0 then st.amount else 0 end) cashin
//    ,sum(case when st.flag='E' and p.paymentflag=0 then st.amount else 0 end) cashexch
//    ,sum(case when st.flag='R' and p.paymentflag=0 then st.amount else 0 end) cashrefund
//    ,count(distinct case when st.flag='N' and p.paymentflag=0 then st.salesno else null end) cashinc
//    ,count(distinct case when st.flag='E' and p.paymentflag=0 then st.salesno else null end) cashexchc
//    ,count(distinct case when st.flag='R' and p.paymentflag=0 then st.salesno else null end) cashrefundc
//    from salestxn st
//    inner join papaytype p on p.typeid=st.paymenttype
//    inner join salesaction sa on sa.actionid=st.actionid
//    where sa.trandate=:1 and sa.shiftno=:3
//) a
//,( 
//    select
//    sum(decode(trantype,1,banknote*qty,0)) cashsupply
//    ,sum(decode(trantype,2,banknote*qty,0)) cashdraw
//    ,count(decode(trantype,1,1,null)) cashsupplyc
//    ,count(decode(trantype,2,1,null)) cashdrawc
//    from shifttran
//    where trandate=:1 and posno=:2 and shiftno=:3 and qty<>0
//) b
//";
// SALES, SALESPROD тэйблийг эцэслэн шийдвэрлэсний дараа үүнийг нэмэх
//(
//select 
//sum(decode(sp.flag,0,sp.salesamount,0)) totalin
//,sum(decode(sp.flag,1,sp.salesamount,0)) totalrefund
//,sum(sp.discount+sp.discountsales) totaldiscount
//,count(distinct decode(sp.flag,0,sp.salesno,null)) totalinc
//,count(distinct decode(sp.flag,1,sp.salesno,null)) totalrefundc
//from salesprod sp
//left join sales sm on sm.salesno=sp.salesno
//where sp.saletype=0
//and trandate=:1
                //)
                #endregion

                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211002", SystemProp.TxnDate, posno, shiftno);
                if (res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "SUMMARY";
                ds.Tables.Add(dt.Copy());

                #region SQL - Дэвсгэртүүд
                sql = @"
select bn.banknote,st.qty
from pabanknote bn
left join (
select banknote,nvl(sum(qty),0) qty 
from shifttran
where trandate=:1 and posno=:2 and shiftno=:3
group by banknote
) st on st.banknote=bn.banknote
where bn.currency='MNT'
order by 1
";
                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211002", SystemProp.TxnDate, posno, shiftno);
                if (res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "CASH";
                ds.Tables.Add(dt.Copy());

                #region SQL - Ээлжийн хөдөлгөөн
                sql = @"
select s.shiftno,s.userno
,decode(length(u.userfname),0,u.userlname,substr(u.userfname,1,1)||'.'||u.userlname) username
,s.postdate
from shift s
left join hpuser u on u.userno=s.userno
where s.trandate=:1 and s.posno=:2
order by 1 desc,4 desc
";
                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211002", SystemProp.TxnDate, posno);
                if (res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "SHIFT";
                ds.Tables.Add(dt.Copy());

                res.Data.Dispose();
                res.Data = ds;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
                ds.Dispose();
            }
            finally
            {
            }
            OnExit:
            return res;
        }    //Кассын үндсэн мэдээлэл

        public Result Txn211003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);
                int shiftno = Static.ToInt(ri.ReceivedParam[1]);
                DataTable banknotes = (DataTable)ri.ReceivedParam[2];

                #endregion
                #region Ээлж нээгдсэн эсэхийг шалгах
                DataRow r = null;
                res = GetPosInfo(db, posno, out r);
                if (res.ResultNo != 0) goto OnExit;

                int posstatus = Static.ToInt(r["STATUS"]);

                if (posstatus != 1)
                {
                    res = new Result(2110011, "Ээлж нээгдээгүй байна.");
                    goto OnExit;
                }
                #endregion

                db.BeginTransaction("core", "Txn211003");

                string sql = @"insert into shifttran 
(trandate,posno,shiftno,banknote,qty,currency,userno,trantype,postdate)
values (:1,:2,:3,:4,:5,:6,:7,:8,:9)";

                object[] param = new object[9];
                param[0] = SystemProp.TxnDate;
                param[1] = posno;
                param[2] = shiftno;
                param[5] = "MNT";
                param[6] = ri.UserNo;
                param[7] = 1;
                param[8] = DateTime.Now;

                foreach (DataRow i in banknotes.Rows)
                {
                    /********************************************
                     * Тэг дүнтэй бичлэгийг оруулах шаардлагагүй.
                     ********************************************/
                    decimal qty = Static.ToDecimal(i["QTY2"]);
                    if (qty != 0)
                    {
                        param[3] = i["BANKNOTE"];
                        param[4] = i["QTY2"];

                        res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn211003", param);
                        if (res.ResultNo != 0) goto OnExit;
                    }
                }
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
                if (res != null)
                    if (res.ResultNo == 0) db.Commit("core");
                    else db.RollBack("core");
            }
        OnExit:
            return res;
        }    //Кассын зузаатгал
        public Result Txn211004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);
                int shiftno = Static.ToInt(ri.ReceivedParam[1]);
                DataTable banknotes = (DataTable)ri.ReceivedParam[2];

                #endregion
                #region Ээлж нээгдсэн эсэхийг шалгах
                DataRow r = null;
                res = GetPosInfo(db, posno, out r);
                if (res.ResultNo != 0) goto OnExit;

                int posstatus = Static.ToInt(r["STATUS"]);

                if (posstatus != 1)
                {
                    res = new Result(2110011, "Ээлж нээгдээгүй байна.");
                    goto OnExit;
                }
                #endregion

                db.BeginTransaction("core", "Txn211003");

                string sql = @"insert into shifttran 
(trandate,posno,shiftno,banknote,qty,currency,userno,trantype,postdate)
values (:1,:2,:3,:4,:5,:6,:7,:8,:9)";

                object[] param = new object[9];
                param[0] = SystemProp.TxnDate;
                param[1] = posno;
                param[2] = shiftno;
                param[5] = "MNT";
                param[6] = ri.UserNo;
                param[7] = 2;
                param[8] = DateTime.Now;

                foreach (DataRow i in banknotes.Rows)
                {
                    /********************************************
                     * Тэг дүнтэй бичлэгийг оруулах шаардлагагүй.
                     ********************************************/
                    decimal qty = Static.ToDecimal(i["QTY2"]);
                    if (qty != 0)
                    {
                        param[3] = i["BANKNOTE"];
                        param[4] = -1 * Static.ToDecimal(i["QTY2"]);

                        res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn211003", param);
                        if (res.ResultNo != 0) goto OnExit;
                    }
                }
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
                if (res != null)
                    if (res.ResultNo == 0) db.Commit("core");
                    else db.RollBack("core");
            }
        OnExit:
            return res;
        }    //Касс тушаах
        public Result Txn211005(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Ээлж нээж болох эсэхийг шалгах

                DataRow r = null;
                res = GetPosInfo(db, posno, out r);
                if (res.ResultNo != 0) goto OnExit;

                int shiftno = Static.ToInt(r["SHIFTNO"]);
                string shiftuser = Static.ToStr(r["USERNAME"]);
                int posstatus = Static.ToInt(r["STATUS"]);

                if (posstatus == 1)
                {
                    res = new Result(2110051, string.Format("[{0}] дугаартай ээлж [{1}] хэрэглэгч дээр нээлттэй байгаа тул давхар ээлж нээх боломжгүй байна.", shiftno, shiftuser));
                    goto OnExit;
                }
                if (posstatus == 3)
                {
                    res = new Result(2110052, "ПОС-ыг хаасан тул ээлж нээх боломжгүй байна.");
                    goto OnExit;
                }

                #endregion

                //Одоо байгаа ээлжийн дугаарыг 1ээр ахиулах.
                shiftno++;

                db.BeginTransaction("core", "Txn211005");

                string sql = @"insert into shift (trandate,posno,shiftno,userno,status,postdate) values(:1,:2,:3,:4,:5,:6)";
                object[] param = new object[] { SystemProp.TxnDate, posno, shiftno, ri.UserNo, 1, DateTime.Now };
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn211005", param);
                if (res.ResultNo != 0) goto OnExit;

                sql = @"update posterminal set shiftno=:2,shiftuserno=:3,trandate=:4,status=:5 where posno=:1";
                param = new object[] { posno, shiftno, ri.UserNo, SystemProp.TxnDate, 1 };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn211005", param);
                if (res.ResultNo != 0) goto OnExit;

                res.Param = new object[] { shiftno };
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
                if (res != null)
                    if (res.ResultNo == 0) db.Commit("core");
                    else db.RollBack("core");
            }
            OnExit:
            return res;
        }    //Ээлж нээх
        public Result Txn211006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);
                decimal debt = Static.ToDecimal(ri.ReceivedParam[1]);

                #endregion
                #region Ээлж хааж болох эсэхийг шалгах

                DataRow r = null;
                res = GetPosInfo(db, posno, out r);
                if (res.ResultNo != 0) goto OnExit;

                int shiftno = Static.ToInt(r["SHIFTNO"]);
                int shiftuserno = Static.ToInt(r["SHIFTUSERNO"]);
                string shiftuser = Static.ToStr(r["USERNAME"]);
                int posstatus = Static.ToInt(r["STATUS"]);

                if (posstatus == 1 && shiftuserno != ri.UserNo)
                {
                    res = new Result(2110061, string.Format("[{0}] дугаартай ээлж [{1}] хэрэглэгч дээр байгаа тул өөр хэрэглэгч хаах боломжгүй.", shiftno, shiftuser));
                    goto OnExit;
                }
                if (posstatus == 2)
                {
                    res = new Result(2110062, string.Format("[{0}] дугаартай ээлж хаасан байгаа тул давхар хаах боломжгүй байна.", shiftno));
                    goto OnExit;
                }
                if (posstatus == 3)
                {
                    res = new Result(2110063, "ПОС-ыг хаасан тул ээлж хаах боломжгүй байна.");
                    goto OnExit;
                }

                #endregion

                db.BeginTransaction("core", "Txn211006");

                //Ээлжийн түүх бичих
                string sql = @"update shift set status=:4, debt=nvl(debt,0)+:5 where trandate=:1 and posno=:2 and shiftno=:3";
                object[] param = new object[] { SystemProp.TxnDate, posno, shiftno, 2, debt };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn211006", param);
                if (res.ResultNo != 0) goto OnExit;

                //ПОСын төлвийг өөрчлөх
                sql = @"update posterminal set shiftno=:2,shiftuserno=:3,trandate=:4,status=:5 where posno=:1";
                param = new object[] { posno, shiftno, ri.UserNo, SystemProp.TxnDate, 2 };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn211006", param);
                if (res.ResultNo != 0) goto OnExit;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
                if (res != null)
                    if (res.ResultNo == 0) db.Commit("core");
                    else db.RollBack("core");
            }
        OnExit:
            return res;
        }    //Ээлж хаах

        public Result Txn211007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region ПОС нээж болох эсэхийг шалгах

                DataRow r = null;
                res = GetPosInfo(db, posno, out r);
                if (res.ResultNo != 0) goto OnExit;

                int shiftno = Static.ToInt(r["SHIFTNO"]);
                int shiftuserno = Static.ToInt(r["SHIFTUSERNO"]);
                string shiftuser = Static.ToStr(r["USERNAME"]);
                int posstatus = Static.ToInt(r["STATUS"]);
                DateTime trandate = Static.ToDate(r["TRANDATE"]);

                if (posstatus != 3)
                {
                    res = new Result(2110071, "ПОС хаагдаагүй байна.");
                    goto OnExit;
                }
                if (trandate >= Core.SystemProp.TxnDate.Date)
                {
                    res = new Result(2110072
                        , string.Format("Систем шинэ өдөрлүү шилжээгүй байна.\r\nПОС хаагдсан: {0}, Системийн огноо: {1}"
                        , trandate.ToString("yyyy-MM-dd")
                        , SystemProp.TxnDate.ToString("yyyy-MM-dd")
                        ));
                    goto OnExit;
                }

                #endregion

                db.BeginTransaction("core", "Txn211007");

                //ПОСын төлвийг өөрчлөх
                string sql = @"update posterminal set shiftno=:2,shiftuserno=:3,trandate=:4,status=:5 where posno=:1";
                object[] param = new object[] { posno, 1, ri.UserNo, SystemProp.TxnDate, 0 };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn211007", param);
                if (res.ResultNo != 0) goto OnExit;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
                if (res != null)
                    if (res.ResultNo == 0) db.Commit("core");
                    else db.RollBack("core");
            }
        OnExit:
            return res;
        }    //ПОС нээх
        public Result Txn211008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region ПОС нээж болох эсэхийг шалгах

                DataRow r = null;
                res = GetPosInfo(db, posno, out r);
                if (res.ResultNo != 0) goto OnExit;

                int shiftno = Static.ToInt(r["SHIFTNO"]);
                int shiftuserno = Static.ToInt(r["SHIFTUSERNO"]);
                string shiftuser = Static.ToStr(r["USERNAME"]);
                int posstatus = Static.ToInt(r["STATUS"]);

                if (posstatus != 2)
                {
                    res = new Result(2110081, string.Format("[{0}] дугаартай ээлж хаагдаагүй байна, ээлжээ эхлээд хаана уу.", shiftno));
                    goto OnExit;
                }

                #endregion

                db.BeginTransaction("core", "Txn211008");

                //ПОСын төлвийг өөрчлөх
                string sql = @"update posterminal set status=:2 where posno=:1";
                object[] param = new object[] { posno, 3 };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn211008", param);
                if (res.ResultNo != 0) goto OnExit;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
                if (res != null)
                    if (res.ResultNo == 0) db.Commit("core");
                    else db.RollBack("core");
            }
        OnExit:
            return res;
        }    //ПОС хаах

        public Result Txn211009(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);
                int shiftno = Static.ToInt(ri.ReceivedParam[1]);

                #endregion

                #region SQL - Кассын нийт дүн
                string sql = @"
select * from 
(
    select sum(decode(pt.paymentflag,0,amount,0)) cash_amount
    ,count(distinct decode(pt.paymentflag,0,st.salesno,null)) cash_count
    ,sum(decode(pt.paymentflag,1,amount,0)) card_amount
    ,count(distinct decode(pt.paymentflag,1,st.salesno,null)) card_count
    ,sum(decode(pt.paymentflag,0,0,1,0,amount)) other_amount
    ,count(distinct decode(pt.paymentflag,0,null,1,null,st.salesno)) other_count
    from salestxn st
    left join salesaction sa on sa.actionid=st.actionid
    left join papaytype pt on pt.typeid=st.paymenttype
    where sa.trandate=:1 and sa.posno=:2 and sa.shiftno=:3
) a,
(
    select 
    sum(decode(sf.trantype,1,sf.qty*sf.banknote,0)) cashin_amount
    ,count(distinct decode(sf.trantype,1,sf.postdate,null)) cashin_count
    ,sum(decode(sf.trantype,2,sf.qty*sf.banknote,0)) cashout_amount
    ,count(distinct decode(sf.trantype,2,sf.postdate,null)) cashout_count
    ,max(pos.posname) posname
    from shifttran sf
    left join posterminal pos on pos.posno=sf.posno
    where sf.trandate=:1 and sf.posno=:2 and sf.shiftno=:3
) b,
(
    select sum(salesamount-discountprod-discountsales) total_amount
    ,count(distinct salesno) sales_count
    ,sum(decode(sp.flag,'R',salesamount-discountprod-discountsales,0)) refund_amount
    ,count(distinct decode(sp.flag,'R',salesno,null)) refund_count
    from salesprod sp
    left join salesaction sa on sa.actionid=sp.actionid
    where sa.trandate=:1 and sa.posno=:2 and shiftno=:3
) c
";

                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211009", Core.SystemProp.TxnDate, posno, shiftno);
                if (res.ResultNo != 0) goto OnExit;
                                
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
            }
        OnExit:
            return res;
        }    //Ээлж хаах биллийн мэдээлэл
        public Result Txn211010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);
                //int shiftno = Static.ToInt(ri.ReceivedParam[1]);

                #endregion

                #region SQL - Кассын нийт дүн
                string sql = @"
select * from 
(
    select sum(decode(pt.paymentflag,0,amount,0)) cash_amount
    ,count(distinct decode(pt.paymentflag,0,st.salesno,null)) cash_count
    ,sum(decode(pt.paymentflag,1,amount,0)) card_amount
    ,count(distinct decode(pt.paymentflag,1,st.salesno,null)) card_count
    ,sum(decode(pt.paymentflag,0,0,1,0,amount)) other_amount
    ,count(distinct decode(pt.paymentflag,0,null,1,null,st.salesno)) other_count
    from salestxn st
    left join salesaction sa on sa.actionid=st.actionid
    left join papaytype pt on pt.typeid=st.paymenttype
    where sa.trandate=:1 and sa.posno=:2
) a,
(
    select 
    sum(decode(sf.trantype,1,sf.qty*sf.banknote,0)) cashin_amount
    ,count(distinct decode(sf.trantype,1,sf.postdate,null)) cashin_count
    ,sum(decode(sf.trantype,2,sf.qty*sf.banknote,0)) cashout_amount
    ,count(distinct decode(sf.trantype,2,sf.postdate,null)) cashout_count
    ,max(pos.posname) posname
    from shifttran sf
    left join posterminal pos on pos.posno=sf.posno
    where sf.trandate=:1 and sf.posno=:2
) b,
(
    select sum(salesamount-discountprod-discountsales) total_amount
    ,count(distinct salesno) sales_count
    ,sum(decode(sp.flag,'R',salesamount-discountprod-discountsales,0)) refund_amount
    ,count(distinct decode(sp.flag,'R',salesno,null)) refund_count
    from salesprod sp
    left join salesaction sa on sa.actionid=sp.actionid
    where sa.trandate=:1 and sa.posno=:2
) c
";

                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211010", Core.SystemProp.TxnDate, posno);
                if (res.ResultNo != 0) goto OnExit;
                dt = res.Data.Tables[0];
                dt.TableName = "TOTAL";
                ds.Tables.Add(dt.Copy());

                #region SQL - Борлуулалтын бараа үйлчилгээ
                sql = @"
select sp.prodtype,sp.prodno
,max(decode(sp.prodtype,0,i.prodname,1,s.prodname,2,pk.name,'None')) prodname
,max(decode(sp.prodtype,0,i.typename,1,s.typename,2,'Pkg','None')) typename
,sum(salesamount-discountsales-discountprod) totalamount
,sum(qty) qty
from salesprod sp
left join salesaction sa on sa.actionid=sp.actionid
left join (
    select im.invid,im.invtype,im.name prodname,pi.name typename 
    from invmain im left join painvtype pi on pi.invtype=im.invtype
) i on i.invid=sp.prodno and sp.prodtype=0
left join (
    select sm.servid,sm.servtype,sm.name prodname,ps.name typename 
    from servmain sm left join paservtype ps on ps.servtype=sm.servtype
) s on s.servid=sp.prodno and sp.prodtype=1
left join packmain pk on pk.packid=sp.prodno and sp.prodtype=2
where sa.trandate=:1 and sa.posno=:2
group by sp.prodtype,sp.prodno
order by prodtype,typename,prodname";
                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211010", Core.SystemProp.TxnDate, posno);
                if (res.ResultNo != 0) goto OnExit;
                dt = res.Data.Tables[0];
                dt.TableName = "PRODUCTS";
                ds.Tables.Add(dt.Copy());

                res.Data = ds;

            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
            }
        OnExit:
            return res;
        }    //ПОС хаах биллийн мэдээлэл
        public Result Txn211011(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion

                #region SQL - Кассын нийт дүн
                string sql = @"
select s.shiftno,s.userno,s.postdate
,max(decode(u.userfname,null,null,substr(u.userfname,1,1)||'.')||u.userlname) username
,sum(decode(s.trantype,1,s.banknote*s.qty,0)) supplied
,sum(decode(s.trantype,2,s.banknote*s.qty,0)) delivered
from shifttran s
left join hpuser u on u.userno=s.userno
where s.trandate=:1 and s.posno=:2
group by s.shiftno,s.userno,s.postdate
order by 1,2,3
";

                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn211011", Core.SystemProp.TxnDate, posno);
                if (res.ResultNo != 0) goto OnExit;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа.\r\n" + ex.ToString());
            }
            finally
            {
            }
        OnExit:
            return res;
        }    //Кассын хөдөлгөөний лавлагаа



        public Result Txn211099(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string filename = EServ.Shared.Static.WorkingFolder + "\\Version.txt";
                if (File.Exists(filename))
                {
                    res.ResultDesc = File.ReadAllText(filename);
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
            }
        }    //Version Description    


        #endregion
    }
}