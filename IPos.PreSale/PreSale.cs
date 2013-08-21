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

                    #region[Урьдчилсан борлуулалтын өгсөн үйлчлүүлэгчид]
                    case 130306:	                //
                        res = Txn130306(ci, ri, db, ref lg);
                        break;
                    case 130307:	                //
                        res = Txn130307(ci, ri, db, ref lg);
                        break;
                    case 130308:	                //
                        res = Txn130308(ci, ri, db, ref lg);
                        break;
                    case 130309:	                //
                        res = Txn130309(ci, ri, db, ref lg);
                        break;
                    case 130310:	                //
                        res = Txn130310(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын өгсөн үйлчлүүлэгчийн захиалсан бүтээгдэхүүн]
                    case 130350:	                //Жагсаалт
                        res = Txn130350(ci, ri, db, ref lg);
                        break;
                    case 130351:	                //Дэлгэрэнгүй
                        res = Txn130351(ci, ri, db, ref lg);
                        break;
                    case 130352:	                //Нэмэх
                        res = Txn130352(ci, ri, db, ref lg);
                        break;
                    case 130353:	                //Засварлах
                        res = Txn130353(ci, ri, db, ref lg);
                        break;
                    case 130354:	                //Устгах
                        res = Txn130354(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн]
                    case 130311:	                //	Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн жагсаалт мэдээлэл авах
                        res = Txn130311(ci, ri, db, ref lg);
                        break;
                    case 130312:	                //  Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах
                        res = Txn130312(ci, ri, db, ref lg);
                        break;
                    case 130313:	                //	Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн нэмэх
                        res = Txn130313(ci, ri, db, ref lg);
                        break;
                    case 130314:	                //	Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн засах
                        res = Txn130314(ci, ri, db, ref lg);
                        break;
                    case 130315:	                //	Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн устгах
                        res = Txn130315(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн бүртгэл]
                    case 130316:	                //	Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн бүртгэл жагсаалт мэдээлэл авах
                        res = Txn130316(ci, ri, db, ref lg);
                        break;
                    case 130317:	                //  Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn130317(ci, ri, db, ref lg);
                        break;
                    case 130318:	                //	Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн бүртгэл нэмэх
                        res = Txn130318(ci, ri, db, ref lg);
                        break;
                    case 130319:	                //	Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн бүртгэл засах
                        res = Txn130319(ci, ri, db, ref lg);
                        break;
                    case 130320:	                //	Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн бүртгэл устгах
                        res = Txn130320(ci, ri, db, ref lg);
                        break;
                    #endregion

                    #region[Урьдчилсан борлуулалтын баталгаажуулах]
                    case 130331:	                //	
                        res = Txn130331(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын цуцлах]
                    case 130332:	                //	
                        res = Txn130332(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Урьдчилсан борлуулалтын сунгах]
                    case 130333:	                //	
                        res = Txn130333(ci, ri, db, ref lg);
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
                res = IPos.DB.Main.DB204301(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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

        #region [Урьдчилсан борлуулалтынд орсон үйлчлүүлэгч]
        public Result Txn130306(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204406(db, Static.ToStr(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALEPERSON", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130307(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204407(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("PRESALEPERSON", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("PRESALEPERSON", "CUSTNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        public Result Txn130308(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = (object[])ri.ReceivedParam;

                obj[1] = EServ.Interface.Sequence.NextByVal("ItemNo");

                res = IPos.DB.Main.DB204408(db, obj);
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130309(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204409(db, (object[])ri.ReceivedParam);
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн бүртгэл засварлах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALEPERSON", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("PRESALEPERSON", "CUSTNO", ri.ReceivedParam[1].ToString(), ri.ReceivedParam[2].ToString());
                }
            }
        }
        public Result Txn130310(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204410(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн бүртгэл устгах";
                lg.AddDetail("PRESALEPERSON", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("PRESALEPERSON", "CUSTNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        #endregion
        #region [Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн захиалсан бүтээгдэхүүн]
        public Result Txn130350(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204450(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн захиалсан бүтээгдэхүүн бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALEPERSONPRODUCT", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("PRESALEPERSONPRODUCT", "ITEMNO", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        public Result Txn130351(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204451(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]), Static.ToInt(ri.ReceivedParam[2]), Static.ToStr(ri.ReceivedParam[3]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн захиалсан бүтээгдэхүүн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("PRESALERPERSONPRODUCT", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("PRESALEPERSONPRODUCT", "ITEMNO", lg.item.Desc, ri.ReceivedParam[1].ToString());
                lg.AddDetail("PRESALEPERSONPRODUCT", "PRODTYPE", lg.item.Desc, ri.ReceivedParam[2].ToString());
                lg.AddDetail("PRESALEPERSONPRODUCT", "PRODNO", lg.item.Desc, ri.ReceivedParam[3].ToString());
            }
        }
        public Result Txn130352(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = (object[])ri.ReceivedParam;

                //obj[1] = EServ.Interface.Sequence.NextByVal("ItemNo");

                res = IPos.DB.Main.DB204452(db, obj);
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн захиалсан бүтээгдэхүүн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130353(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204453(db, (object[])ri.ReceivedParam);
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн захиалсан бүтээгдэхүүн бүртгэл засварлах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALEPERSONPRODUCT", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    //lg.AddDetail("ORDERPERSONPRODUCT", "ITEM", ri.ReceivedParam[1].ToString(), ri.ReceivedParam[2].ToString());
                }
            }
        }
        public Result Txn130354(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204454(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]), Static.ToInt(ri.ReceivedParam[2]), Static.ToStr(ri.ReceivedParam[3]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтынд орсон үйлчлүүлэгчийн захиалсан бүтээгдэхүүн бүртгэл устгах";
                lg.AddDetail("PRESALEPERSONPRODUCT", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("PRESALEPERSONPRODUCT", "ITEMNO", lg.item.Desc, ri.ReceivedParam[1].ToString());
                lg.AddDetail("PRESALEPERSONPRODUCT", "PRODTYPE", lg.item.Desc, ri.ReceivedParam[2].ToString());
                lg.AddDetail("PRESALEPERSONPRODUCT", "PRODNO", lg.item.Desc, ri.ReceivedParam[3].ToString());
            }
        }
        #endregion
        #region [Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн]
        public Result Txn130311(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204411(db, Static.ToStr(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALEPRODUCT", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130312(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204412(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALEPRODUCT", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("PRESALEPRODUCT", "PRODTYPE", lg.item.Desc, ri.ReceivedParam[1].ToString());
                    lg.AddDetail("PRESALEPRODUCT", "PRODNO", lg.item.Desc, ri.ReceivedParam[2].ToString());
                }
            }
        }
        public Result Txn130313(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204413(db, ri.ReceivedParam);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130314(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] OldParam = (object[])ri.ReceivedParam[0];
            object[] NewParam = (object[])ri.ReceivedParam[1];
            //object[] FieldParam = (object[])ri.ReceivedParam[2];

            try
            {

                res = IPos.DB.Main.DB204414(db, OldParam, NewParam);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн засварлах";
                if (res.ResultNo == 0)
                {
                    //lg.AddDetail("ORDERPRODUCT", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    //lg.AddDetail("ORDERPRODUCT", "GROUPNO", ri.ReceivedParam[1].ToString(), ri.ReceivedParam[2].ToString());
                }
            }
        }
        public Result Txn130315(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204115(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
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
                    lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүн устгах";
                    lg.AddDetail("ORDERPRODUCT", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("ORDERPRODUCT", "PRODTYPE", lg.item.Desc, ri.ReceivedParam[1].ToString());
                    lg.AddDetail("ORDERPRODUCT", "PRODNO", lg.item.Desc, ri.ReceivedParam[2].ToString());
                }
            }
        }
        #endregion
        #region [Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн бүртгэл]
        public Result Txn130316(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204416(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]), Static.ToInt(ri.ReceivedParam[2]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALEPRODUCT", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130317(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204417(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]), Static.ToInt(ri.ReceivedParam[2]), Static.ToStr(ri.ReceivedParam[3]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("PRESALEPRODUCTPRICE", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("PRESALEPRODUCTPRICE", "PRODNO", lg.item.Desc, ri.ReceivedParam[1].ToString());
                lg.AddDetail("PRESALEPRODUCTPRICE", "PRODTYPE", lg.item.Desc, ri.ReceivedParam[2].ToString());
                lg.AddDetail("PRESALEPRODUCTPRICE", "PRICETYPEID", lg.item.Desc, ri.ReceivedParam[3].ToString());
            }
        }
        public Result Txn130318(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204418(db, ri.ReceivedParam);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130319(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204419(db, (object[])ri.ReceivedParam[0], (object[])ri.ReceivedParam[1]);
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн засварлах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130320(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204420(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]), Static.ToInt(ri.ReceivedParam[2]), Static.ToStr(ri.ReceivedParam[3]));
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
                lg.item.Desc = "Урьдчилсан борлуулалтын доторх багц дахь бүтээгдэхүүний үнийн устгах";
                lg.AddDetail("PRESALEPRODUCTPRICE", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("PRESALEPRODUCTPRICE", "PRODNO", lg.item.Desc, ri.ReceivedParam[1].ToString());
                lg.AddDetail("PRESALEPRODUCTPRICE", "PRODTYPE", lg.item.Desc, ri.ReceivedParam[2].ToString());
                lg.AddDetail("PRESALEPRODUCTPRICE", "PRICETYPEID", lg.item.Desc, ri.ReceivedParam[3].ToString());
            }
        }
        #endregion

        #region[Урьдчилсан борлуулалтын баталгаажуулах]
        public Result Txn130331(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string _presaleno = Static.ToStr(ri.ReceivedParam[0]);
                int _userno = Static.ToInt(ri.ReceivedParam[1]);
                string _note = Static.ToStr(ri.ReceivedParam[2]);
                DateTime _dte = Static.ToDateTime(DateTime.Now);

                res = IPos.DB.Main.DB204126(db, _presaleno, _userno, _note, _dte);
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
                lg.item.Desc = "Урьдчилсан борлуулалтыг баталгаажуулах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALE", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[Урьдчилсан борлуулалтын цуцлах]
        public Result Txn130332(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string _presaleno = Static.ToStr(ri.ReceivedParam[0]);
                int _userno = Static.ToInt(ri.ReceivedParam[1]);
                string _note = Static.ToStr(ri.ReceivedParam[2]);
                DateTime _dte = Static.ToDateTime(DateTime.Now);

                res = IPos.DB.Main.DB204127(db, _presaleno, _userno, _note, _dte);
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
                lg.item.Desc = "Урьдчилсан борлуулалтыг цуцлах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALE", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[Урьдчилсан борлуулалтын сэргээх]
        public Result Txn130333(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
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
                lg.item.Desc = "Урьдчилсан борлуулалтыг сэргээх";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PRESALE", "PRESALENO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    //lg.AddDetail("PRESALE", "ExpendDate", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        #endregion
    }
}
