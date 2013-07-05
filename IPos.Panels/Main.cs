using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;


namespace IPos.Panels
{
    public class Main : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime First = DateTime.Now;
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 500001: // борлуулалт хайх
                        res = Txn500001(ci, ri, db, ref lg);
                        break;
                    case 500002: // харилцагч хайх
                        res = Txn500002(ci, ri, db, ref lg);
                        break;
                    case 500003: // түрээсийн барааны жагсаалт. salesno
                        res = Txn500003(ci, ri, db, ref lg);
                        break;
                    case 500004: // Барааны жагсаалт, орхон
                        //res = Txn500004(ci, ri, db, ref lg);
                        break;
                    case 500005: // борлуулалтаар олгосон тагийн жагсаалт
                        res = Txn500005(ci, ri, db, ref lg);
                        break;
                    case 500007: // борлуулалтын төлбөрийн нийт дүн ба төрлийн жагсаалт. salesno, userno
                        res = Txn500007(ci, ri, db, ref lg);
                        break;

                    case 500009: // Барьцаа бүхий харилцагчдийн жагсаалт
                        res = Txn500009(ci, ri, db, ref lg);
                        break;
                    case 500010: // Барьцаалагдсан бичиг баримтын жагсаалт. custno,pledgeno
                        res = Txn500010(ci, ri, db, ref lg);
                        break;
                    case 500011: //Бичиг баримт барьцаанд авах
                        res = Txn500011(ci, ri, db, ref lg);
                        break;
                    case 500012: //Барьцаалсан бичиг баримтыг олгох
                        res = Txn500012(ci, ri, db, ref lg);
                        break;
                    case 500013: //Барьцаанд тайлбар оруулах
                        res = Txn500013(ci, ri, db, ref lg);
                        break;
                    case 500014: //Барьцаанд орсон харилцагч хасах
                        res = Txn500014(ci, ri, db, ref lg);
                        break;

                    case 500020: // Харилцагч дээр таг уях
                        res = Txn500020(ci, ri, db, ref lg);
                        break;
                    case 500021: // Харилцагч дээр уягдсан тагийн салгах
                        res = Txn500021(ci, ri, db, ref lg);
                        break;
                    case 500022: // Борлуулалтын хайлт дэлгэрэнгүйгээр
                    res = Txn500022(ci, ri, db, ref lg);
                    break;
                    case 500023: // Борлуулалтаас таг дээрх үйлчилгээний цаг авах
                    res = Txn500023(ci, ri, db, ref lg);
                    break;
                    case 500100: // харилцагчийн хэмжээсийн мэдээлэл авах
                        res = Txn500100(ci, ri, db, ref lg);
                        break;
                    case 500101: // түрээсийн бараа олгох
                        res = Txn500101(ci, ri, db, ref lg);
                        break;
                    case 500102: // түрээсийн бараа хүлээн авах
                        res = Txn500102(ci, ri, db, ref lg);
                        break;
                    case 500103: // түрээсийн барааг хүлээн авах автоматаар
                        res = Txn500103(ci, ri, db, ref lg);
                        break;
                    case 500104: // Түрээсийн хэрэгслийг 1 үйлдэл буцаах
                        res = Txn500104(ci, ri, db, ref lg);
                        break;
                    case 500105: // Түрээсийн ажилтны нэвтрэх хэсэг
                        res = Txn500105(ci, ri, db, ref lg);
                        break;
                    case 500106: // Таг түрээсийн хэсэгт уншигдсан огноог тэмдэглэх
                        res = Txn500106(ci, ri, db, ref lg);
                        break;
                    case 500107: // Түрээсийн түүх харах
                        res = Txn500107(ci, ri, db, ref lg);
                        break;

                    case 500200: // борлуулалтын жагсаалт, харилцагчийн мэдээлэлтэй
                        res = Txn500200(ci, ri, db, ref lg);
                        break;
                    //case 500202: // төлбөрийн гүйлгээг тэйблээс хийх
                    //    res = Txn500202(ci, ri, db, ref lg);
                    //    break;
                    case 500203: // төлбөрийн гүйлгээний нэг мөр бичилт
                        res = Txn500203(ci, ri, db, ref lg);
                        break;
                    case 500204: // гэрээний мэдээлэл авах
                        res = Txn500204(ci, ri, db, ref lg);
                        break;
                    case 500205: // Багцаас төлбөрийн гүйлгээ оруулах
                        res = Txn500205(ci, ri, db, ref lg);
                        break;
                    case 500206: // Төлбөрийн гүйлгээ устгах
                        res = Txn500206(ci, ri, db, ref lg);
                        break;

                    default:
                        res = new Result (1000, "Unknown transaction.");
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
                DateTime Last = DateTime.Now;
                if (res.ResultNo == 0)
                    ISM.Lib.Static.WriteToLogFile("IPos.Panels", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("IPos.Panels", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
            }
        }

        public Result Txn500001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500001(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        }
        public Result Txn500002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500002(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        }
        public Result Txn500003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500003(db, ri.PageIndex, ri.PageRows, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]));
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
        }
        public Result Txn500004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500004(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        }
        public Result Txn500005(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500005(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        }
        public Result Txn500007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                // төлбөрийн дутуу үлдсэн нийт дүнг авах
                object[] param1 = new object[] { ri.ReceivedParam[0] /*batchno*/ };
                res = IPos.DB.Panels.DB500007(db, ri.PageIndex, ri.PageRows, param1);
                if (res.ResultNo == 0)
                {
                    dt = res.Data.Tables[0].Copy();
                    dt.TableName = "TotalAmounts";
                    ds.Tables.Add(dt);
                    res.Data.Dispose();

                    // хэрэглэгчийн боломжит төлбөрийн төрлүүдийн жагсаалт авах
                    object[] param2 = new object[] { ri.ReceivedParam[1] /*userno*/ };
                    res = IPos.DB.Panels.DB500008(db, ri.PageIndex, ri.PageRows, param2);
                    if (res.ResultNo == 0)
                    {
                        dt = res.Data.Tables[0].Copy();
                        dt.TableName = "PaymentTypes";
                        ds.Tables.Add(dt);
                        res.Data.Dispose();
                    }
                }

                if (res.ResultNo == 0)
                {
                    res.Data = ds;
                }
                else
                {
                    ds.Dispose();
                    ds = null;
                }
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
            }

            return res;
        }


        public Result Txn500009(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500009(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        }
        public Result Txn500010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500010(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        }
        public Result Txn500011(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(ri.ReceivedParam[0]);
                string pledgeno = Static.ToStr(ri.ReceivedParam[1]);
                int userno = Static.ToInt(ri.ReceivedParam[2]);
                string docno = Static.ToStr(ri.ReceivedParam[3]);
                int doctype = Static.ToInt(ri.ReceivedParam[4]);

                #endregion
                #region New autonum for registeration

                if (string.IsNullOrEmpty(pledgeno))
                {
                    IPos.Core.AutoNumEnum enums = new IPos.Core.AutoNumEnum();
                    enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                    res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 8, "",enums);

                    if (res.ResultNo == 0)
                    {
                        if (string.IsNullOrEmpty(res.ResultDesc))
                        {
                            res.ResultNo = 9110068;
                            res.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:12][";
                        }
                        else
                        {
                            pledgeno = res.ResultDesc;
                        }
                    }
                }
                #endregion

                if (Static.ToInt(ri.ReceivedParam[5]) == 0)
                {
                    object[] param = new object[] { custno, pledgeno, userno, docno, doctype };
                    res = IPos.DB.Panels.DB500011(db, param);
                    if (res.ResultNo == 0)
                        res.ResultDesc = pledgeno;
                    return res;
                }
                else
                {
                    object[] param = new object[] { custno, pledgeno, "", DateTime.Now, 0 };
                    string sql = @"insert into PLEDGEMAIN(CUSTNO,PLEDGENO,NOTE,CREATEDATE,STATUS) values(:1,:2,:3,:4,:5)";
                    res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn500011", param);
                    return res;
                }
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
        }
        public Result Txn500012(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500012(db, ri.ReceivedParam);
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
        }
        public Result Txn500013(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500013(db, ri.ReceivedParam);
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
        }
        public Result Txn500014(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500014(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]));
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
        }

        public Result Txn500020(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = db.ExecuteQuery("core", "select * from tagmain where tagid=:1 and status=1", enumCommandType.SELECT, "Txn500200", ri.ReceivedParam[1]);
                if (res.ResultNo != 0) return res;
                if (res.Data.Tables[0].Rows.Count != 0)
                {
                    res = IPos.DB.Panels.DB500020(db, ri.ReceivedParam);
                    return res;
                }
                else
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Тухайн таг бүртгэлгүй эсвэл идэвхигүй байна.";
                    return res;
                }
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
        }
        public Result Txn500021(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500021(db, ri.ReceivedParam);
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
        }
        public Result Txn500022(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500022(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        }
        public Result Txn500023(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500023(db, Static.ToStr(ri.ReceivedParam[0]));
                //if (res.ResultNo == 0)
                //{
                //    if (res.Data != null)
                //    {
                //        foreach (DataRow dr in res.Data.Tables[0].Rows)
                //        {
                //            decimal returnamount = 0;
                //            string salesno = Static.ToStr(dr["salesno"]);
                //            string oldsalesno = Static.ToStr(dr["oldsalesno"]);
                //            decimal amount = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToStr(x["oldsalesno"]) == salesno).Max(x => Static.ToDecimal(x["salesamount"]));
                //        }
                //    }
                //}
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
                lg.item.Desc = "Борлуулалтаас таг дээр бичигдэх үйлчилгээний цаг авах";

            }
        }
        // Rent related
        public Result Txn500100(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500100(db, ri.ReceivedParam);
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
        }
        public Result Txn500101(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                    res = IPos.DB.Panels.DB500101(db, ri.ReceivedParam);
                    if (res.ResultNo != 0) return res;
                    res = IPos.DB.Main.DB227044(db, Static.ToStr(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[3]));
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
        }
        public Result Txn500102(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[9];
                obj[0] = ri.ReceivedParam[0];
                obj[1] = ri.ReceivedParam[1];
                obj[2] = ri.ReceivedParam[2];
                obj[3] = ri.ReceivedParam[3];
                obj[4] = ri.ReceivedParam[4];
                obj[5] = ri.ReceivedParam[5];
                obj[6] = ri.ReceivedParam[6];
                obj[7] = ri.ReceivedParam[7];
                obj[8] = ri.ReceivedParam[8];
                res = IPos.DB.Panels.DB500102(db, (object[])obj);
                if (res.ResultNo != 0) return res;
                res = IPos.DB.Main.DB227042(db, Static.ToStr(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[9]), ri.UserNo, DateTime.Now);
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
        }

        public Result Txn500103(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"select * from invseries where invid in ({0}) and barcode='{1}'";

                string[] invids = Static.ToStr(ri.ReceivedParam[0]).Split(';');
                string prodno="";
                foreach (string invid in invids)
                {
                    if (prodno.Length == 0)
                    {
                        prodno = prodno + "'" + invid + "'";
                    }
                    else
                    {
                        prodno = prodno + ",'" + invid + "'";
                    }
                }
                res = db.ExecuteQuery("core", string.Format(sql, prodno, Static.ToStr(ri.ReceivedParam[1])), enumCommandType.SELECT, "Txn500103", null);
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
        }
        public Result Txn500104(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500104(db, ri.ReceivedParam);
                if (res.ResultNo != 0) return res;
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
                lg.item.Desc = "Түрээсийн хэрэгсэлийг 1 үйлдэл буцаах";
            }
            return res;
        }
        public Result Txn500105(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500105(db, Static.ToInt(ri.ReceivedParam[0]));
                if (res.ResultNo != 0) return res;
                else 
                {
                    if (res.Data != null)
                    {
                        if (res.Data.Tables[0].Rows.Count != 0)
                        {
                            if (Static.ToStr(res.Data.Tables[0].Rows[0]["UPASSWORD"]) == Static.Encrypt(Static.ToStr(ri.ReceivedParam[1])))
                            {
                                return res;
                            }
                            else
                            {
                                res.ResultNo = 1001;
                                res.ResultDesc = "Нууц үг буруу байна.";
                                return res;
                            }
                        }
                        else
                        {
                            res.ResultNo = 1000;
                            res.ResultDesc = "Ийм хэрэглэгч бүртгэгдээгүй байна.";
                            return res;
                        }

                    }
                    else 
                    {
                        res.ResultNo = 1000;
                        res.ResultDesc = "Ийм хэрэглэгч бүртгэгдээгүй байна.";
                        return res;
                    }
                }
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
                lg.item.Desc = "Түрээсийн хэрэгсэлийг 1 үйлдэл буцаах";
            }
            return res;
        }
        public Result Txn500106(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500106(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToDateTime(ri.ReceivedParam[1]));
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
                lg.item.Desc = "Таг түрээсийн хэсэгт уншигдсан огноог тэмдэглэх";
            }
            return res;
        }
        public Result Txn500107(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Panels.DB500107(db);
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
                lg.item.Desc = "Түрээсийн түүх харах";
            }
            return res;
        }
        // Payment related

        public Result Txn500200(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                string batchno = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Panels.DB500200(db, batchno);
            }
            catch (Exception ex)
            {
                res = new Result();
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";

            }
            return res;
        }

        /// <summary>
        /// Төлбөрийн үндсэн бичилт хийх функц.
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="ri"></param>
        /// <param name="db"></param>
        /// <param name="lg"></param>
        /// <returns></returns>
        private Result Payment(DbConnections db, bool fullpayment, string batchno, DataTable salesdt, DataTable payments /* salesno, paytype, amount гэсэн баганатай бн*/, string frstpaynno, bool isreturn,decimal chargeamount)
        {
            Result res = new Result();
            DbConnection conn = null;
            try
            {
                //int seqno = 1;
                //string susp = "";
                string payno = "";
                decimal sum = 0;

                #region Параметр утгуудыг шалгах

                //if (salesno == null || paytype == null || paid == null)
                //{
                //    res = new Result(5002011, "Төлбөрийн мэдээлэл буруу байна!");
                //    goto OnExit;
                //}
                //if (salesno.Length != paytype.Length || salesno.Length != paid.Length)
                //{
                //    res = new Result(5002012, "Төлбөрийн мэдээлэл зөрүүтэй байна!");
                //    goto OnExit;
                //}

                //decimal total=0;
                //for (int i = 0; i < salesno.Length; i++)
                //{
                //    total += paid[i];
                //}

                //if (total <=0 )
                //{
                //    res = new Result(5002013, "Төлбөрийн дүн хоосон байна!");
                //    goto OnExit;
                //}

                #endregion

                #region Төлөх үлдэгдэл дүн байгаа эсэхийг шалгах

                var v1 = from row in payments.AsEnumerable()
                         select row.Field<decimal>("AMOUNT");

                var v2 = from row in salesdt.AsEnumerable()
                         select row.Field<decimal>("SALESAMOUNT") - row.Field<decimal>("PAID");

                decimal d2 = v2.Sum();
                decimal d1 = v1.Sum();

                if (d2 == 0 && isreturn == false)
                {
                    res = new Result(50002011, "Үлдэгдэл төлбөр байхгүй байна!");
                    goto OnExit;
                }
                //if (d2 < d1)
                //{
                //    res = new Result(50002011, string.Format("Төлбөрийн дүн их байна! \r\nНийт төлөх={0}, төлсөн={1}", d2, d1));
                //    goto OnExit;
                //}

                #endregion
                conn = db.BeginTransaction("core", "payment");
                if (isreturn == false)
                {
                    IPos.Core.AutoNumEnum enums = new IPos.Core.AutoNumEnum();
                    enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                    res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 9, "", enums);
                    if (res.ResultNo != 0) goto OnExit;
                    payno = string.Format("{0}-{1}", frstpaynno, res.ResultDesc);

                    res = IPos.DB.Panels.DB500203(db, batchno, payno, chargeamount > 0 ? chargeamount : 0);
                    if (res.ResultNo != 0) goto OnExit;
                }
                else
                {
                    res = IPos.DB.Panels.DB500205(db, batchno, chargeamount > 0 ? chargeamount : 0);
                    if (res.ResultNo != 0) goto OnExit;
                }

                foreach (DataRow salesrow in salesdt.Rows)
                {
                    string salesno = Static.ToStr(salesrow["SALESNO"]);
                    decimal salesamount = Static.ToDecimal(salesrow["SALESAMOUNT"]);
                    int seqno = Static.ToInt(salesrow["SEQNO"]);
                    //if (seqno <= 0) seqno = 1;

                    /*
                     * Төлж буй дүнгүүдийн нийлбэрийг олж, төлөх
                     * ёстой дүнгээс ихээр оруулсан эсэхийг шалгах.
                     */
                    #region

                    var var1 = from row in payments.AsEnumerable()
                               where row.Field<string>("SALESNO") == salesno
                               select row.Field<decimal>("AMOUNT");

                    decimal total = var1.Sum();
                    sum += total;

                    //if (total > salesamount)
                    //{
                    //    res = new Result(50002011, string.Format("Төлбөрийн дүн их байна! \r\nБорл={0}, төлөх={1}, төлсөн={2} "
                    //        , salesno, salesamount, total));
                    //    goto OnExit;
                    //}

                    #endregion

                    /*
                     * Заавал бүрэн төлбөр хийх эсэх төлөв
                     * болон бүрэн төлбөр хийгдсэн эсэхийг шалгах.
                     * Хэрэв fullpayment=false бол дутуу төлбөр хийгдэж болно.
                     */
                    #region

                    if (sum == 0 || (fullpayment && total < salesamount))
                    {
                        res = new Result(50002011, string.Format("Төлбөрийн дүн дутуу байна! \r\nБорл={0}, төлөх={1}, төлсөн={2} "
                            , salesno, salesamount, total));
                        goto OnExit;
                    }
                    #endregion

                    /*
                     * Тухайн salesno дээрх төлбөрийн төрөл бүр дээрх
                     * дүнг уншиж төлбөрийн бичлэгийг оруулах.
                     * Хэрэв Payments тэйбэл дээр SalesNo утга өгөгдөөгүй бол 
                     * бүх борлуулалт дээр уг төлбөрийн төрлөөр төлбөр хийгдэнэ гэж үзнэ.
                     */
                    #region

                    var var2 = from row in payments.AsEnumerable()
                               where row.Field<string>("SALESNO") == salesno
                               select row;

                    if (var2 != null && var2.Count() > 0)
                    {
                        #region Төлбөрийн төрлөөр төлбөрийн дүнг оруулах

                        foreach (DataRow payrow in var2)
                        {
                            string paytype = Static.ToStr(payrow["PAYTYPE"]);
                            decimal amount = Static.ToDecimal(payrow["AMOUNT"]);
                            seqno++;

                            #region Төлбөрийн бичилт хийх
                            if (isreturn == false)
                            {
                                res = IPos.DB.Panels.DB500202(db, payno, seqno, paytype, amount, "", DateTime.Now);
                                if (res.ResultNo != 0) goto OnExit;
                                res.Param = new object[] { payno };
                            }
                            else
                            {
                                res = ReturnContract(db, frstpaynno);
                                if (res.ResultNo != 0) return res;

                                res = IPos.DB.Panels.DB500202(db, frstpaynno, seqno, paytype, amount, "", DateTime.Now);
                                if (res.ResultNo != 0) goto OnExit;
                                res.Param = new object[] { frstpaynno };
                            }
                            #endregion
                        }
                        break;

                        #endregion
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
            finally
            {
                if (conn != null)
                    if (res.ResultNo == 0) conn.Commit();
                    else conn.Rollback();

                //lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
            }
        OnExit:
            return res;
        }
                
        /// <summary>
        /// Төлбөрийн гүйлгээг тэйблээс уншиж хийх
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="ri"></param>
        /// <param name="db"></param>
        /// <param name="lg"></param>
        /// <returns></returns>
        //public Result Txn500202(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        //{
        //    //DbConnection conn = null;
        //    Result res = null;
        //    try
        //    {
        //        string batchno = Static.ToStr(ri.ReceivedParam[0]);
        //        DataTable dt = (DataTable)ri.ReceivedParam[1];
        //        bool fullpayment = false;

        //        /*
        //         * Ирэх тэйблийн бүтэц нь дараах баганатай байна.
        //         * salesno,paytype,amount
        //         */

        //        #region Борлуулалтын дүнгийн жагсаалт авах

        //        res = IPos.DB.Panels.DB500201(db, batchno);
        //        if (res.ResultNo != 0) return res;
        //        DataTable salesdt = res.Data.Tables[0];

        //        #endregion

        //        res = Payment(db, fullpayment, batchno, salesdt, dt, Static.ToStr(ri.ReceivedParam[3]));
        //    }
        //    catch (Exception ex)
        //    {
        //        res = new Result();
        //        res.ResultNo = 9110002;
        //        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

        //        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
        //    }
        //    //finally
        //    //{
        //    //    if (conn != null)
        //    //        if (res.ResultNo == 0) conn.Commit();
        //    //        else conn.Rollback();

        //    //    lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
        //    //}

        //    return res;
        //}
        /// <summary>
        /// Төлбөрийн дан гүйлгээ
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="ri"></param>
        /// <param name="db"></param>
        /// <param name="lg"></param>
        /// <returns></returns>
        public Result Txn500203(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                bool fullpayment = false;

                string batchno = Static.ToStr(ri.ReceivedParam[0]);
                string paytype = Static.ToStr(ri.ReceivedParam[1]);
                decimal payment = Static.ToDecimal(ri.ReceivedParam[2]);

                #region Борлуулалтын дүнгийн жагсаалт авах

                res = IPos.DB.Panels.DB500201(db, batchno);
                if (res.ResultNo != 0) return res;
                DataTable salesdt = res.Data.Tables[0];

                #endregion

                DataTable dt = new DataTable();
                dt.Columns.Add("SALESNO", typeof(string));
                dt.Columns.Add("PAYTYPE", typeof(string));
                dt.Columns.Add("AMOUNT", typeof(decimal));
                foreach (DataRow row in salesdt.Rows)
                {
                    string salesno = Static.ToStr(row["SALESNO"]);
                    decimal salesamount = Static.ToDecimal(row["SALESAMOUNT"]);
                    decimal prepaid = Static.ToDecimal(row["PAID"]);

                    decimal now = Math.Min(salesamount - prepaid, payment);
                    dt.Rows.Add(salesno, paytype, payment);
                    payment -= now;
                    if (payment <= 0) break;
                }
                if (Static.ToStr(ri.ReceivedParam[4]) == "")
                {
                    res = Payment(db, fullpayment, batchno, salesdt, dt, Static.ToStr(ri.ReceivedParam[3]), false, Static.ToDecimal(ri.ReceivedParam[5]));
                }
                else
                {
                    res = Payment(db, fullpayment, batchno, salesdt, dt, Static.ToStr(ri.ReceivedParam[4]), true, Static.ToDecimal(ri.ReceivedParam[5]));
                }

            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
            return res;
        }
        public Result Txn500204(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                string contractno = Static.ToStr(ri.ReceivedParam[0]);
                string contracttype = Static.ToStr(ri.ReceivedParam[1]);
                res = IPos.DB.Panels.DB500204(db, contractno, contracttype);
            }
            catch (Exception ex)
            {
                res = new Result();
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";

            }
            return res;
        }
        public Result Txn500205(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DbConnection conn = null;
            try
            {
                conn = db.BeginTransaction("core", "Txn500205");
                string paymentno = Static.ToStr(ri.ReceivedParam[3]);
                string batchno = Static.ToStr(ri.ReceivedParam[0]);
                int seqno = 0;
                string sql = "";
                if (paymentno == "")
                {
                    # region PaymentNo
                    IPos.Core.AutoNumEnum enums = new IPos.Core.AutoNumEnum();
                    enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                    res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 9, "", enums);
                    if (res.ResultNo != 0) return res;
                    paymentno = string.Format("{0}-{1}", ri.ReceivedParam[2], res.ResultDesc);
                    #endregion
                    //SalesPayment дээр борлуулалт болон төлбөрийн холбоос хийж байна.
                    res = IPos.DB.Panels.DB500203(db, batchno, paymentno, Static.ToDecimal(ri.ReceivedParam[4]) > 0 ? Static.ToDecimal(ri.ReceivedParam[4]) : 0);
                    if (res.ResultNo != 0) return res;

                    //Багцын дугаараар тухайн багц дахь борлуулалт дээр төлбөрийн мэдээлэл татаж байна.
                    res = IPos.DB.Panels.DB500201(db, batchno);
                    if (res.ResultNo != 0) return res;
                    DataTable salesdt = res.Data.Tables[0];
                    seqno = 0;
                    foreach (DataRow dr in salesdt.Rows)
                    {
                        if (Static.ToInt(dr["SEQNO"]) > seqno)
                        {
                            seqno = Static.ToInt(dr["SEQNO"]);
                        }
                    }
                }
                else
                {
                    res = ReturnContract(db, paymentno);
                    if (res.ResultNo != 0) return res;
                }

                DataTable PaymentData = (DataTable)ri.ReceivedParam[1];
                foreach (DataRow dr in PaymentData.Rows)
                {
                    seqno = seqno + 1;
                    string contractno = Static.ToStr(dr["CONTRACTNO"]);
                    decimal amount = Static.ToDecimal(dr["AMOUNT"]);
                    if (contractno != "")
                    {
                        sql = @"select * from contractmain where contractno=:1";
                        res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn500205", contractno);
                        if (res.Data == null || res.Data.Tables[0].Rows.Count == 0)
                        {
                            res.ResultNo = 22222;
                            res.ResultDesc = string.Format("[{0}] - Бүртгэлийн мэдээлэл олдсонгүй", contractno);
                            return res;
                        }
                        else
                        {
                            if (Static.ToInt(res.Data.Tables[0].Rows[0]["STATUS"]) == 1)
                            {
                                if (Static.ToDateTime(res.Data.Tables[0].Rows[0]["VALIDSTARTDATE"]) <= DateTime.Now && Static.ToDateTime(res.Data.Tables[0].Rows[0]["VALIDENDDATE"]) >= DateTime.Now)
                                {
                                    if (Static.ToInt(res.Data.Tables[0].Rows[0]["balancetype"]) == 0 && Static.ToInt(dr["METHOD"]) == 0)
                                    {
                                        sql = @"update contractmain set balance=balance-:2 where contractno=:1";
                                        res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn500206", contractno, amount);
                                        if (res.ResultNo != 0) return res;
                                    }
                                    else
                                    {
                                        if (Static.ToDecimal(res.Data.Tables[0].Rows[0]["balance"]) - amount < 0)
                                        {
                                            res.ResultNo = 22223;
                                            res.ResultDesc = string.Format("[{0}] - Бүртгэлийн үлдэгдэл хүрэлцэхгүй байна. [{1}]", contractno, res.Data.Tables[0].Rows[0]["balance"]);
                                            return res;
                                        }
                                        else
                                        {
                                            if (Static.ToInt(dr["METHOD"]) == 0)
                                            {
                                                //Бүртгэлийн үлдэгдэл бууруулж байна.
                                                sql = @"update contractmain set balance=balance-:2 where contractno=:1";
                                                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn500206", contractno, amount);
                                                if (res.ResultNo != 0) return res;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    res.ResultNo = 22224;
                                    res.ResultDesc = string.Format("[{0}] - Бүртгэлийн хугацаа дууссан байна.", contractno);
                                    return res;
                                }
                            }
                            else
                            {
                                res.ResultNo = 22225;
                                res.ResultDesc = string.Format("[{0}] - Бүртгэл идэвхигүй байна.", contractno);
                                return res;
                            }
                        }
                    }
                    //SalesPaymentDetail дээр бичлэг нэмэж байна.
                    res = IPos.DB.Panels.DB500202(db, paymentno, seqno, Static.ToStr(dr["PAYMENTTYPE"]), amount, contractno, DateTime.Now);
                    if (res.ResultNo != 0) return res;
                }
                //
                sql = @"
select a.salesno,b.paymentno,b.seqno,b.paymenttype,c.name,b.amount
from salespayment a
left join salespaymentdetail b on a.paymentno=b.paymentno
left join papaytype c on b.paymenttype=c.typeid
where a.salesno=:1
";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn500205", batchno);
                if (res.ResultNo != 0) return res;
                res.Param = new object[] { paymentno };
                return res;
            }
            catch (Exception ex)
            {
                res = new Result();
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            }
            finally
            {
                lg.item.Desc = "Борлуулалтын төлбөр багцаас хийх";
                if (conn != null)
                    if (res.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
            }
            return res;
        }
        public Result Txn500206(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DbConnection conn=null;
            string paymentno = Static.ToStr(ri.ReceivedParam[5]);
            try
            {
                string sql = @"select * from contractmain where contractno=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn500206", ri.ReceivedParam[1]);
                if (res.Data == null || res.Data.Tables[0].Rows.Count == 0)
                {
                    res.ResultNo = 22222;
                    res.ResultDesc = string.Format("[{0}] - Бүртгэлийн мэдээлэл олдсонгүй", ri.ReceivedParam[1]);
                    return res;
                }
                else
                {
                    conn = db.BeginTransaction("core", "Txn500206");
                    if (paymentno == "")
                    {
                        if (Static.ToInt(res.Data.Tables[0].Rows[0]["STATUS"]) == 1)
                        {
                            if (Static.ToDateTime(res.Data.Tables[0].Rows[0]["VALIDSTARTDATE"]) <= DateTime.Now && Static.ToDateTime(res.Data.Tables[0].Rows[0]["VALIDENDDATE"]) >= DateTime.Now)
                            {
                                if (Static.ToInt(res.Data.Tables[0].Rows[0]["balancetype"]) == 0)
                                {
                                    sql = @"update contractmain set balance=balance-:2 where contractno=:1";
                                    res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn500206", Static.ToStr(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
                                    if (res.ResultNo != 0) return res;
                                }
                                else
                                {
                                    if (Static.ToDecimal(res.Data.Tables[0].Rows[0]["balance"]) - Static.ToDecimal(ri.ReceivedParam[2]) < 0)
                                    {
                                        res.ResultNo = 22223;
                                        res.ResultDesc = string.Format(" [{0}] - Бүртгэлийн үлдэгдэл хүрэлцэхгүй байна.", ri.ReceivedParam[1]);
                                        return res;
                                    }
                                    else
                                    {
                                        //Бүртгэлийн үлдэгдэл бууруулж байна.
                                        sql = @"update contractmain set balance=balance-:2 where contractno=:1";
                                        res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn500206", Static.ToStr(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
                                        if (res.ResultNo != 0) return res;
                                    }
                                }
                                //PaymentNo авч байна.
                                IPos.Core.AutoNumEnum enums = new IPos.Core.AutoNumEnum();
                                enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                                res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 9, "", enums);
                                if (res.ResultNo != 0) return res;
                                paymentno = string.Format("{0}-{1}", ri.ReceivedParam[3], res.ResultDesc);

                                //SalesPaiment дээр багц болон төлбөрийн холбоос хийж байна.
                                res = IPos.DB.Panels.DB500203(db, Static.ToStr(ri.ReceivedParam[0]), paymentno, 0);
                                if (res.ResultNo != 0) return res;
                            }
                            else
                            {
                                res.ResultNo = 22224;
                                res.ResultDesc = string.Format("[{0}] - Бүртгэлийн хугацаа дууссан байна.", ri.ReceivedParam[1]);
                                return res;
                            }
                        }
                        else
                        {
                            res.ResultNo = 22225;
                            res.ResultDesc = string.Format("[{0}] - Бүртгэлийн хугацаа дууссан байна.", ri.ReceivedParam[1]);
                            return res;
                        }
                    }
                    else
                    {
                        res = ReturnContract(db, paymentno);
                        if (res.ResultNo != 0) return res;
                    }

                    //SalesPaymentDetail дээр бичлэг нэмэж байна.
                    sql = "select * from papaytype where contracttype=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn500206", ri.ReceivedParam[4]);
                    if (res.ResultNo != 0) return res;
                    if (res.Data == null)
                    {
                        res.ResultNo = 9110147;
                        res.ResultDesc = "Төлбөрийн төрлийн мэдээлэл олдсонгүй";
                        return res;
                    }
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 9110147;
                        res.ResultDesc = "Төлбөрийн төрлийн мэдээлэл олдсонгүй";
                        return res;
                    }
                    string paytype = Static.ToStr(res.Data.Tables[0].Rows[0]["TYPEID"]);
                    res = IPos.DB.Panels.DB500202(db, paymentno, 1, paytype, Static.ToDecimal(ri.ReceivedParam[2]), Static.ToStr(ri.ReceivedParam[1]), DateTime.Now);
                    if (res.ResultNo != 0) return res;
                    res.Param = new object[] { paymentno };
                    return res;
                }
            }
            catch (Exception ex)
            {
                res = new Result();
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
                if (conn != null)
                    if (res.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
            }
            return res;
        }
        /// <summary>
        /// Бүртгэлийн үлдэгдэлийг буцаана ингэхдээ
        /// бүртгэлийн баланс хөдөлөх методоос хамаарна
        /// 0 - борлуулалтаар буурч байгаа учир буцаахдаа 0 - г л буцаана
        /// бусад нь борлуулалтаар үлдэгдэл хөдлөөгүй учир шаардлагагүй
        /// </summary>
        /// <param name="db"></param>
        /// <param name="paymentno"></param>
        /// <returns></returns>
        private Result ReturnContract(DbConnections db,string paymentno)
        {
            Result res = null;
            try
            {
                string sql = "";

                //Тухайн төлбөрийн дугаараар хийгдсэн бүртгэлийн үлдэгдэлийг буцааж байна.
                sql = @"select b.paymentno,b.seqno,b.paymenttype,b.amount,b.contractno,ct.method from salespaymentdetail b
left join contractmain cm on cm.contractno=b.contractno
left join pacontracttype ct on ct.contracttype=cm.contracttype
where b.paymentno=:1 and b.contractno is not null and b.paymentflag=0";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn5000206", paymentno);
                if (res.ResultNo != 0) return res;
                if (res.Data != null)
                {
                    if (res.Data.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow dr in res.Data.Tables[0].Rows)
                        {
                            if (Static.ToInt(dr["METHOD"]) == 0)
                            {
                                sql = @"update contractmain set balance=balance+:2 where contractno=:1";
                                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn500206", Static.ToStr(dr["CONTRACTNO"]), Static.ToDecimal(dr["AMOUNT"]));
                                if (res.ResultNo != 0) return res;
                            }
                        }
                    }
                }
                //Өмнө нь байсан бүх төлбөрийн мэдээлэлийн төлвийг буцаагдсан төлөвтэй болгож байна.
                res = db.ExecuteQuery("core", @"update salespaymentdetail set paymentflag=1 where paymentno=:1", enumCommandType.UPDATE, "Payment", paymentno);
                if (res.ResultNo != 0) return res;
            }
            catch (Exception ex)
            {
                res = new Result();
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            }
            return res;
        }
    }
}
