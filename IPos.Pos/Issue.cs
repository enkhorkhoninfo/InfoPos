using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using System.Data;

using IPos.DB;
using IPos.Core;

namespace IPos.Pos
{
    public class Issue : IModule
    {
        //310092-IssueList
        //310098-IssueAttach
        //310105-IssueTxn

        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            DateTime First = DateTime.Now;
            try
            {
                switch (ri.FunctionNo)
                {
                    #region[ Issue ]
                    case 310079:
                        {
                            res = Txn310079(ci, ri, db, ref lg);
                        }
                        break;
                    case 310080:
                        {
                            res = Txn310080(ci, ri, db, ref lg);
                        }
                        break;
                    case 310081:
                        {
                            res = Txn310081(ci, ri, db, ref lg);
                        }
                        break;
                    case 310082:
                        {
                            res = Txn310082(ci, ri, db, ref lg);
                        }
                        break;
                    case 310083:
                        {
                            res = Txn310083(ci, ri, db, ref lg);
                        }
                        break;
                    case 310006:
                        {
                            res = Txn310006(ci, ri, db, ref lg);
                        }
                        break;
                    case 310108:
                        {
                            res = Txn310108(ci, ri, db, ref lg);
                        }
                        break;
                    case 310117:
                        {
                            res = Txn310117(ci, ri, db, ref lg);
                        }
                        break;
                    case 310118:
                        {
                            res = Txn310118(ci, ri, db, ref lg);
                        }
                        break;
                    #endregion
                    #region[ Issue Txn ]
                    case 310105:
                        {
                            res = Txn310105(ci, ri, db, ref lg);
                        }
                        break;
                    case 310106:
                        {
                            res = Txn310106(ci, ri, db, ref lg);
                        }
                        break;
                    case 310120:
                        {
                            res = Txn310120(ci, ri, db, ref lg);
                        }
                        break;
                    case 310122:
                        {
                            res = Txn310122(ci, ri, db, ref lg);
                        }
                        break;
                    #endregion
                    #region[ Customer Note ]
                    case 310087:
                        {
                            res = Txn310087(ci, ri, db, ref lg);
                        }
                        break;
                    case 310088:
                        {
                            res = Txn310088(ci, ri, db, ref lg);
                        }
                        break;
                    case 310089:
                        {
                            res = Txn310089(ci, ri, db, ref lg);
                        }
                        break;
                    case 310090:
                        {
                            res = Txn310090(ci, ri, db, ref lg);
                        }
                        break;
                    case 310091:
                        {
                            res = Txn310091(ci, ri, db, ref lg);
                        }
                        break;
                    case 310092:
                        {
                            res = Txn310092(ci, ri, db, ref lg);
                        }
                        break;
                    #endregion
                    #region[ IssueTracking ]
                    case 310123:
                        {
                            res = Txn310123(ci, ri, db, ref lg);
                        }
                        break;
                    case 310124:
                        {
                            res = Txn310124(ci, ri, db, ref lg);
                        }
                        break;
                    case 310125:
                        {
                            res = Txn310125(ci, ri, db, ref lg);
                        }
                        break;
                    case 310126:
                        {
                            res = Txn310126(ci, ri, db, ref lg);
                        }
                        break;
                    #endregion
                    #region[ Attach ]
                    case 310098:
                        {
                            res = Txn310098(ci, ri, db, ref lg);
                        }
                        break;
                    #endregion

                    #region[ Жакад гаргаж өгөв ]
                    case 310112:
                        {
                            res = Txn310112(ci, ri, db, ref lg);
                        }
                        break;
                    case 300015:
                        {
                            res = Txn300015(ci, ri, db, ref lg);
                        }
                        break;
                    #endregion
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
                    ISM.Lib.Static.WriteToLogFile("IPos.Issue", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + lg.item.Desc + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("IPos.Issue", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + lg.item.Desc + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
            }
        }
        
        #region[ Function ]
       
        #region[ Issue ]
        public Result Txn310079(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int pagenumber = 0;
            int pagecount = 1;
            try
            {
                res = IPos.DB.CRMDB.DB256000(db, pagenumber,pagecount,null);
                if (res.Data != null)
                {
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 9110014;
                        res.ResultDesc = "Бичлэг олдсонгүй";
                    }
                }
                else
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Мэдээллийн бааз руу хандахад алдаа гарлаа";
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
                lg.item.Desc = "Асуудлын жагсаалт авах";
            }
        }
        public Result Txn310080(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256001(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Асуудлын дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result Txn310081(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] objs=(object[])(ri.ReceivedParam[0]);
                res = IPos.DB.CRMDB.DB256002(db,objs);
                if (res.ResultNo == 0)
                {
                    long IssueId = Static.ToLong(res.Param[0]);//10,13
                    int CreatUser = Static.ToInt(objs[10]);
                    int AssignUser = Static.ToInt(objs[13]);
                    object[] obj = res.Param;
                    res = IPos.DB.CRMDB.DB253002(db, new object[] { Static.ToLong(res.Param[0]), Static.ToLong(ri.ReceivedParam[1]) });
                    if (res.ResultNo != 0) return res;

                    #region[ SendMail copy ]
                    res = IPos.DB.CRMDB.DB250006(db, IssueId, 310081);
                    if (res.ResultNo != 0) return res;
                    DataTable DT = res.Data.Tables[0];
                    string[] ToUser = new string[DT.Rows.Count];
                    int k = 0;
                    if (DT.Rows.Count != 0)
                    {
                        DataRow rws = DT.NewRow();
                        foreach (DataRow rw in DT.Rows)
                        {
                            ToUser[k] = rw["email"].ToString();
                            k++;
                            rws=rw;
                        }
                        string Body = @"CRM Асуудал үүслээ - "+IssueId.ToString()
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + ri.UserNo.ToString() + "-" + GetName(db,ri.UserNo)+" асуудал үүсгэв"
                                        + "\r\n"
                                        + "-----------------------------------------"
                                        + "\r\n"
                                        + "Төсөлийн нэр : "+rws["PROJECTNAME"].ToString()
                                        + "\r\n"
                                        + ">>   Асуудлын дугаар - " + IssueId.ToString()
                                        + "\r\n"
                                        + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                                        + "\r\n"
                                        + ">>   Товч утга - " + rws["SUBJECT"].ToString()
                                        + "\r\n"
                                        + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                                        + "\r\n"
                                        + ">>   Үүсгэгч - " + rws["REPORTERUSER"].ToString()
                                        + "\r\n"
                                        + ">>   Яг одоо харуйцаж байгаа хэрэглэгч - "+rws["ASSIGNUSER"].ToString();
                        res = SendMail("CRM Асуудал шинээр нэмэх", Body, ToUser,db,ri);
                    }
                    #endregion
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
                lg.item.Desc = "Асуудал шинээр нэмэх";
            }
        }
        public Result Txn310082(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] objs = (object[])(ri.ReceivedParam[0]);
                res = IPos.DB.CRMDB.DB256003(db, (object[])(ri.ReceivedParam[0]));
                if (res.ResultNo == 0)
                {
                    long IssueId = Static.ToLong(objs[0]);//10,13
                    int CreatUser = Static.ToInt(objs[10]);
                    int AssignUser = Static.ToInt(objs[13]);

                    #region[ SendMail copy ]
                    res = IPos.DB.CRMDB.DB250006(db, IssueId, 310082);
                    if (res.ResultNo != 0) return res;
                    DataTable DT = res.Data.Tables[0];
                    string[] ToUser = new string[DT.Rows.Count];
                    int k = 0;
                    if (DT.Rows.Count != 0)
                    {
                        DataRow rws = DT.NewRow();
                        foreach (DataRow rw in DT.Rows)
                        {
                            ToUser[k] = rw["email"].ToString();
                            k++;
                            rws = rw;
                        }
                        string Body = @"CRM Асуудал засагдав - " + IssueId.ToString()
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " асуудал засав"
                                        + "\r\n"
                                        + "-----------------------------------------"
                                        + "\r\n"
                                        + "Төсөлийн нэр : " + rws["PROJECTNAME"].ToString()
                                        + "\r\n"
                                        + ">>   Асуудлын дугаар - " + IssueId.ToString()
                                        + "\r\n"
                                        + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                                        + "\r\n"
                                        + ">>   Товч утга - " + rws["SUBJECT"].ToString()
                                        + "\r\n"
                                        + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                                        + "\r\n"
                                        + ">>   Үүсгэгч - " + rws["REPORTERUSER"].ToString()
                                        + "\r\n"
                                        + ">>   Яг одоо харуйцаж байгаа хэрэглэгч - " + rws["ASSIGNUSER"].ToString();
                        res = SendMail("CRM Асуудал засварлах", Body, ToUser, db, ri);
                    }
                    #endregion
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
                lg.item.Desc = "Асуудал засварлах";
            }
        }
        public Result Txn310083(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256004(db,Static.ToLong(ri.ReceivedParam[0]));
                //if (res.ResultNo == 0)
                //{
                //    #region[ SendMail copy ]
                //    res = IPos.DB.CRMDB.DB250006(db, Static.ToLong(ri.ReceivedParam[0]), 310083);
                //    if (res.ResultNo != 0) return res;
                //    DataTable DT = res.Data.Tables[0];
                //    string[] ToUser = new string[DT.Rows.Count];
                //    int k = 0;
                //    if (DT.Rows.Count != 0)
                //    {
                //        DataRow rws = DT.NewRow();
                //        foreach (DataRow rw in DT.Rows)
                //        {
                //            ToUser[k] = rw["email"].ToString();
                //            k++;
                //            rws = rw;
                //        }
                //        string Body = @"CRM Асуудал үүслээ - " + Static.ToLong(ri.ReceivedParam[0]).ToString()
                //                        + "\r\n"
                //                        + "\r\n"
                //                        + "\r\n"
                //                        + "\r\n"
                //                        + "\r\n"
                //                        + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " асуудал үүсгэв"
                //                        + "\r\n"
                //                        + "-----------------------------------------"
                //                        + "\r\n"
                //                        + "\r\n"
                //                        + ">>   Асуудлын дугаар - " + Static.ToLong(ri.ReceivedParam[0]).ToString()
                //                        + "\r\n"
                //                        + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                //                        + "\r\n"
                //                        + ">>   Товч утга - " + rws["SUBJECT"].ToString()
                //                        + "\r\n"
                //                        + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                //                        + "\r\n"
                //                        + ">>   Үүсгэгч - " + rws["REPORTERUSER"].ToString()
                //                        + "\r\n"
                //                        + ">>   Яг одоо харуйцаж байгаа хэрэглэг - " + rws["ASSIGNUSER"].ToString();
                //        res = SendMail("CRM Асуудал үүслээ", Body, ToUser);
                //    }
                //    #endregion
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
                lg.item.Desc = "Асуудал устгах";
            }
        }

        public Result Txn310006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                System.Data.DataSet DS = new System.Data.DataSet();


                res = IPos.DB.CRMDB.DB241001(db, Static.ToInt(ri.ReceivedParam[0]));
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "VOTE";
                 DS.Tables.Add(res.Data.Tables[0].Copy());

                res = IPos.DB.CRMDB.DB262006(db, Static.ToLong(ri.ReceivedParam[1]));
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "SourceIssue";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res = IPos.DB.CRMDB.DB261000(db, 0, 1, Static.ToLong(ri.ReceivedParam[1]));
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "IssueAttach";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res = IPos.DB.CRMDB.DB259000(db, Static.ToLong(ri.ReceivedParam[1]));
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "IssueTxn";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res = IPos.DB.CRMDB.DB258001(db, Static.ToLong(ri.ReceivedParam[1]), Static.ToInt(ri.ReceivedParam[2]));
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "VoteCheck";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res = IPos.DB.CRMDB.DB258005(db, Static.ToLong(ri.ReceivedParam[1]));
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "VoteUser";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res.Data = DS;
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
                lg.item.Desc = "Асуудлын төрөлийн дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result Txn310108(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                System.Data.DataSet DS = new System.Data.DataSet();

                res = IPos.DB.CRMDB.DB258001(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]));
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "VoteCheck";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res = IPos.DB.CRMDB.DB258005(db, Static.ToLong(ri.ReceivedParam[0]));
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName="VoteUser";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res.Data = DS;
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
                lg.item.Desc = "Асуудлын санал өгөх хүмүүс дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result Txn310117(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256005(db,ri.ReceivedParam);
                if (res.ResultNo == 0&&res.AffectedRows==1)
                {
                    res = IPos.DB.CRMDB.DB256006(db, new object[] { Static.ToLong(ri.ReceivedParam[0]), ri.UserNo });
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
                lg.item.Desc = "Асуудал санал засварлах";
            }
        }
        public Result Txn310118(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256007(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Асуудал саналын тоо авах";
            }
        }
        #endregion
        #region[ Issue Txn ]
        public Result Txn310105(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB259000(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Асуудлын гүйлгээ жагсаалт авах";
            }
        }
        public Result Txn310120(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256008(db,ri.ReceivedParam);
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
                lg.item.Desc = "Асуудлын гүйлгээ жагсаалт авах";
            }
        }
        public Result Txn310106(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            ulong TxnID = 0;
            int TxnType = 0;
           // string Type = "";
            try
            {
                if (ri.ReceivedParam.Length != 1)
                {
                    TxnType = 1;
                    int Mail = 0;
                    #region[ Txn ]
                    res = IPos.DB.CRMDB.DB259002(db, (object[])ri.ReceivedParam[0]);
                    if (res.ResultNo == 0)
                    {
                        TxnID = Static.ToULong(res.Param[0]);
                        bool AttachCheck = (bool)ri.ReceivedParam[1];
                        if (AttachCheck)
                        {
                            object[] obj = (object[])ri.ReceivedParam[2];
                            obj[0] = Static.ToLong(res.Param[0]);
                            res = IPos.DB.CRMDB.DB261002(db, obj);
                        }
                        if (res.ResultNo == 0)
                        {

                            object[] RiObj = (object[])ri.ReceivedParam[0];
                            object[] Obj = new object[6];
                            Mail = Convert.ToInt32(RiObj[5]);
                            switch (Mail)
                            {
                                case 1://comment
                                    { Obj[5] = ri.ReceivedParam[3]; }
                                   // Type = "COMMENT";
                                    break;
                                case 12://reopen
                                    { Obj[5] = 2; }
                                  //  Type = "REOPEN";
                                    break;
                                case 13://resolve
                                    { Obj[5] = 3; }
                                  //  Type = "RESOLVE";
                                    break;
                                case 19://close
                                    { Obj[5] = 9; }
                                  //  Type = "CLOSE";
                                    break;
                            }

                            Obj[0] = RiObj[1];
                            Obj[1] = RiObj[4];
                            Obj[2] = DateTime.Now;
                            Obj[3] = RiObj[10];
                            Obj[4] = RiObj[9];

                            res = IPos.DB.CRMDB.DB256009(db, Obj);
                            if (res.ResultNo == 0)
                            {
                                long IssueId = Static.ToLong(((object[])ri.ReceivedParam[0])[1]);

                                #region[ SendMail copy ]
                                res = IPos.DB.CRMDB.DB250007(db, IssueId, 310106);
                                if (res.ResultNo != 0) return res;
                                DataTable DT = res.Data.Tables[0];
                                string[] ToUser = new string[DT.Rows.Count];
                                int k = 0;
                                if (DT.Rows.Count != 0)
                                {
                                    DataRow rws = DT.NewRow();
                                    foreach (DataRow rw in DT.Rows)
                                    {
                                        ToUser[k] = rw["email"].ToString();
                                        k++;
                                        rws = rw;
                                    }

                                    switch (Mail)
                                    {
                                        case 1://comment
                                            #region[ COMMENT ]
                                            {
                                                string Body = @"Тайлбар : [" + IssueId.ToString() + "] "+rws["SUBJECT"].ToString()
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " тайлбар оруулсан"
                    + "\r\n"
                    + "-----------------------------------------"
                    + "\r\n"
                    + "Төслийн нэр : " + rws["PROJECTNAME"].ToString()
                    + "\r\n"
                    + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                    + "\r\n"
                    + ">>   Асуудлын шатлал - " + rws["ISSUETRACKNAME"].ToString()
                    + "\r\n"
                    + ">>   Дараа хийх ажлын төлөвлөгөө - " + rws["NEXTPURPOSE"].ToString()
                    + "\r\n"
                    + ">>   Дараагийн ярилцах огноо - " + rws["NEXTDATE"].ToString()
                    + "\r\n"
                    + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                    + "\r\n"
                    + ">>   Үүсэгсэн хэрэглэгч - " + rws["REPORTERUSER"].ToString()
                    + "\r\n"
                    + ">>   Хариуцаж байгаа хэрэглэгч - " + rws["ASSIGNUSER"].ToString();
                                                res = SendMail("Тайлбар : [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString(), Body, ToUser, db, ri);
                                            }
                                            #endregion
                                            break;
                                        case 12://reopen
                                            #region[ REOPEN ]
                                            {
                                                string Body = @"Дахин нээсэн : [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString()
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " дахин нээсэн"
                    + "\r\n"
                    + "-----------------------------------------"
                    + "\r\n"
                    + "Төслийн нэр : " + rws["PROJECTNAME"].ToString()
                    + "\r\n"
                    + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                    + "\r\n"
                    + ">>   Асуудлын шатлал - " + rws["ISSUETRACKNAME"].ToString()
                    + "\r\n"
                    + ">>   Дараа хийх ажлын төлөвлөгөө - " + rws["NEXTPURPOSE"].ToString()
                    + "\r\n"
                    + ">>   Дараагийн ярилцах огноо - " + rws["NEXTDATE"].ToString()
                    + "\r\n"
                    + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                    + "\r\n"
                    + ">>   Үүсэгсэн хэрэглэгч - " + rws["REPORTERUSER"].ToString()
                    + "\r\n"
                    + ">>   Хариуцаж байгаа хэрэглэгч - " + rws["ASSIGNUSER"].ToString();
                                                res = SendMail("Дахин нээсэн : [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString(), Body, ToUser, db, ri);
                                            }
                                            #endregion
                                            break;
                                        case 13://resolve
                                            #region[ RESOLVE ]
                                            {
                                                string Body = @"Шийдвэрлэсэн : [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString()
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " шийдвэрлэсэн"
                    + "\r\n"
                    + "-----------------------------------------"
                    + "\r\n"
                    + "Төслийн нэр : " + rws["PROJECTNAME"].ToString()
                    + "\r\n"
                    + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                    + "\r\n"
                    + ">>   Асуудлын шатлал - " + rws["ISSUETRACKNAME"].ToString()
                    + "\r\n"
                    + ">>   Дараа хийх ажлын төлөвлөгөө - " + rws["NEXTPURPOSE"].ToString()
                    + "\r\n"
                    + ">>   Дараагийн ярилцах огноо - " + rws["NEXTDATE"].ToString()
                    + "\r\n"
                    + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                    + "\r\n"
                    + ">>   Үүсэгсэн хэрэглэгч - " + rws["REPORTERUSER"].ToString()
                    + "\r\n"
                    + ">>   Хариуцаж байгаа хэрэглэгч - " + rws["ASSIGNUSER"].ToString();
                                                res = SendMail("Шийдвэрлэсэн : [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString(), Body, ToUser, db, ri);
                                            }
                                            #endregion
                                            break;
                                        case 19://close
                                            #region[ CLOSE ]
                                            {
                                                string Body = @"Хаасан : [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString()
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + "\r\n"
                    + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " хаасан"
                    + "\r\n"
                    + "-----------------------------------------"
                    + "\r\n"
                    + "Төслийн нэр : " + rws["PROJECTNAME"].ToString()
                    + "\r\n"
                    + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                    + "\r\n"
                    + ">>   Асуудлын шатлал - " + rws["ISSUETRACKNAME"].ToString()
                    + "\r\n"
                    + ">>   Дараа хийх ажлын төлөвлөгөө - " + rws["NEXTPURPOSE"].ToString()
                    + "\r\n"
                    + ">>   Дараагийн ярилцах огноо - " + rws["NEXTDATE"].ToString()
                    + "\r\n"
                    + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                    + "\r\n"
                    + ">>   Үүсэгсэн хэрэглэгч - " + rws["REPORTERUSER"].ToString()
                    + "\r\n"
                    + ">>   Хариуцаж байгаа хэрэглэгч - " + rws["ASSIGNUSER"].ToString();
                                                res = SendMail("Хаасан : [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString(), Body, ToUser, db, ri);
                                            }
                                            #endregion
                                            break;
                                    }


                                }
                                #endregion
                            }
                        }
                    }
                    return res;
                    #endregion

                }
                else
                {
                    TxnType = 2;
                    res = IPos.DB.CRMDB.DB259002(db, (object[])ri.ReceivedParam[0]);
                    if (res.ResultNo == 0)
                    {
                        
                        TxnID = Static.ToULong(res.Param[0]);
                        object[] RiObj = (object[])ri.ReceivedParam[0];
                        object[] obj = new object[2];
                        obj[0] = RiObj[1];
                        obj[1] = RiObj[11];
                        res = IPos.DB.CRMDB.DB256001(db, Static.ToLong(obj[0]));
                        if (res.ResultNo != 0) return res;

                        int OldAssignUser = Static.ToInt(res.Data.Tables[0].Rows[0]["AssigneeUser"]);
                        int NewAssignUser = Static.ToInt(obj[1]);
                        res = IPos.DB.CRMDB.DB256010(db, obj);
                        if (res.ResultNo == 0)
                        {
                            long IssueId = Static.ToLong(((object[])ri.ReceivedParam[0])[1]);

                            #region[ SendMail copy ]
                            res = IPos.DB.CRMDB.DB250007(db, IssueId, 310106);
                            if (res.ResultNo != 0) return res;
                            DataTable DT = res.Data.Tables[0];
                            string[] ToUser = new string[DT.Rows.Count];
                            int k = 0;
                            if (DT.Rows.Count != 0)
                            {
                                DataRow rws = DT.NewRow();
                                foreach (DataRow rw in DT.Rows)
                                {
                                    ToUser[k] = rw["email"].ToString();
                                    k++;
                                    rws = rw;
                                }
                                string Body = @"Шилжүүлсэн : [" + IssueId.ToString()+"] "+rws["SUBJECT"].ToString()
                                                + "\r\n"
                                                + "\r\n"
                                                + "\r\n"
                                                + "\r\n"
                                                + "\r\n"
                                                + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " шилжүүлсэн"
                                                + "\r\n"
                                                + "-----------------------------------------"
                                                + "\r\n"
                                                + OldAssignUser + "-" + GetName(db, OldAssignUser) + " - аас " + NewAssignUser + "-" + GetName(db, NewAssignUser)+" руу"
                                                + "\r\n"
                                                + "Төсөлийн нэр : " + rws["PROJECTNAME"].ToString()
                                                + "\r\n"
                                                + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                                                + "\r\n"
                                                + ">>   Асуудалын шатлал - " + rws["ISSUETRACKNAME"].ToString()
                                                + "\r\n"
                                                + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                                                + "\r\n"
                                                + ">>   Үүсэгсэн хэрэглэгч - " + rws["REPORTERUSER"].ToString()
                                                + "\r\n"
                                                + ">>   Хариуцаж байгаа хэрэглэгч - " + rws["ASSIGNUSER"].ToString();
                                res = SendMail("Шилжүүлсэн : [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString(), Body, ToUser, db, ri);
                            }
                            #endregion
                        }
                    }
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
                lg.item.Desc = "Асуудлын гүйлгээ";
                if (TxnType == 2)
                {
                    if (TxnID != 0)
                    {
                        Result log = new Result();
                        object[] FieldName = new object[13];
                        object[] FieldValue = (object[])ri.ReceivedParam[0];
                        FieldName[0] = "IssueID";
                        FieldName[1] = "TxnDate";
                        FieldName[2] = "PostDate";
                        FieldName[3] = "UserNo";
                        FieldName[4] = "ActionTypeID";
                        FieldName[5] = "Subject";
                        FieldName[6] = "Description";
                        FieldName[7] = "Status";
                        FieldName[8] = "ResolutionTypeID";
                        FieldName[9] = "TrackID";
                        FieldName[10] = "AssigneeUser";
                        FieldName[11] = "NextPurpose";
                        FieldName[12] = "NextDate ";


                        for (int i = 0; i <= 12; i++)
                        {
                            log = IPos.DB.CRMDB.DB260001(db, TxnID, FieldName[i].ToString(), "ASSIGN", FieldValue[i+1].ToString());
                        }
                    }
                }
                if (TxnType == 1)
                {
                    if (TxnID != 0)
                    {
                        Result log = new Result();
                        object[] FieldName = new object[13];
                        object[] FieldValue = (object[])ri.ReceivedParam[0];
                        FieldName[0] = "IssueID";
                        FieldName[1] = "TxnDate";
                        FieldName[2] = "PostDate";
                        FieldName[3] = "UserNo";
                        FieldName[4] = "ActionTypeID";
                        FieldName[5] = "Subject";
                        FieldName[6] = "Description";
                        FieldName[7] = "Status";
                        FieldName[8] = "ResolutionTypeID";
                        FieldName[9] = "TrackID";
                        FieldName[10] = "AssigneeUser";
                        FieldName[11] = "NextPurpose";
                        FieldName[12] = "NextDate ";


                        for (int i = 0; i <= 12; i++)
                        {
                            log = IPos.DB.CRMDB.DB260001(db, TxnID, FieldName[i].ToString(), "TXN", FieldValue[i + 1].ToString());
                        }
                    }
                }
            }
        }
        public Result Txn310122(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int type = Static.ToInt(ri.ReceivedParam[1]);
            ulong TxnID = 0;
            object[] obj = (object[])ri.ReceivedParam[2];
            try
            {

                switch (type)
                {
                    case 0:
                        {
                            res = IPos.DB.CRMDB.DB259002(db, (object[])ri.ReceivedParam[0]);
                            if (res.ResultNo == 0)
                            {
                                TxnID=Static.ToULong(res.Param[0]);
                                res = IPos.DB.CRMDB.DB262002(db, obj);
                            }
                        }
                        break;
                    case 1:
                        {
                            res = IPos.DB.CRMDB.DB259002(db, (object[])ri.ReceivedParam[0]);
                            if (res.ResultNo == 0)
                            {
                                TxnID=Static.ToULong(res.Param[0]);
                                res = IPos.DB.CRMDB.DB262002(db, obj);
                            }
                        }
                        break;
                    case 2:
                        {
                            res = IPos.DB.CRMDB.DB259002(db, (object[])ri.ReceivedParam[0]);
                            if (res.ResultNo == 0)
                            {
                                TxnID=Static.ToULong(res.Param[0]);
                                res = IPos.DB.CRMDB.DB262005(db,Static.ToLong(obj[1]));
                            }
                        }
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
                lg.item.Desc = "Асуудлын гүйлгээ";
                if (TxnID != 0)
                {
                    Result log = new Result();
                    object[] FieldName = new object[13];
                    object[] FieldValue = (object[])ri.ReceivedParam[0];
                    FieldName[0] = "IssueID";
                    FieldName[1] = "TxnDate";
                    FieldName[2] = "PostDate";
                    FieldName[3] = "UserNo";
                    FieldName[4] = "ActionTypeID";
                    FieldName[5] = "Subject";
                    FieldName[6] = "Description";
                    FieldName[7] = "Status";
                    FieldName[8] = "ResolutionTypeID";
                    FieldName[9] = "TrackID";
                    FieldName[10] = "AssigneeUser";
                    FieldName[11] = "NextPurpose";
                    FieldName[12] = "NextDate ";
                    for (int i = 0; i <= 12; i++)
                    {
                        log = IPos.DB.CRMDB.DB260001(db, TxnID, FieldName[i].ToString(), "LINK", FieldValue[i+1].ToString());
                    }
                }
            }
        }
        #endregion
        #region[ Customer Contract ]
        //310087	Харилцагчтай холбоо барьсан тэмдэглэлийн жагсаалт авах
        //310088	Харилцагчтай холбоо барьсан тэмдэглэлийн дэлгэрэнгүй мэдээлэл авах
        //310089	Харилцагчтай холбоо барьсан тэмдэглэл шинээр нэмэх
        //310090	Харилцагчтай холбоо барьсан тэмдэглэл засварлах
        //310091	Харилцагчтай холбоо барьсан тэмдэглэл устгах
        
        //Харилцагчийн холбоо барисан жагсаалт мэдээлэл авах
        public Result Txn310087(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int pagenumber = 0;
            int pagecount = 1;
            long customerid;
            try
            {
                customerid = Static.ToLong(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB205036(db, pagenumber, pagecount, customerid);
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
                lg.item.Desc = "Харилцагчийн Холбоо барьсан тэмдэглэл жагсаалт авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("CustomerContract", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //Харилцагчийн холбоо барисан дэлгэрэнгүй мэдээлэл авах
        public Result Txn310088(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                customerid = Static.ToLong(ri.ReceivedParam[0]);
                seqid = Static.ToLong(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB205037(db, customerid, seqid);
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
                lg.item.Desc = "Харилцагчийн Холбоо барьсан тэмдэглэл дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("CustomerContract", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("CustomerContract", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        //Харилцагчийн холбоо барисан мэдээлэл нэмэх
        public Result Txn310089(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] obj = new object[7];
            try
            {
                obj = (object[])ri.ReceivedParam[0];
                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                obj[1] = Static.ToStr(SeqNo);

                res = IPos.DB.Main.DB205038(db, obj);
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
                lg.item.Desc = "Харилцагчийн Холбоо барьсан тэмдэглэл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] FieldName = { "CUSTOMERNO", "CONTACTDATE", "POSTDATE", "CONTACTTYPE", "NOTE", "BRIEFDESC" };
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CustomerContract", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        //Харилцагчийн холбоо барисан мэдээлэл засварлах
        public Result Txn310090(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] obj = new object[7];
            try
            {
                res = IPos.DB.Main.DB205039(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Харилцагчийн Холбоо барьсан тэмдэглэл  засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = { "CUSTOMERNO", "CONTACTDATE", "POSTDATE", "CONTACTTYPE", "NOTE", "BRIEFDESC" };
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerContract", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        //Харилцагчийн холбоо барисан мэдээлэл устгах
        public Result Txn310091(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                customerid = Static.ToLong(ri.ReceivedParam[0]);
                seqid = Static.ToLong(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB205040(db, customerid, seqid);
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
                lg.item.Desc = "Харилцагчийн Холбоо барьсан тэмдэглэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("CustomerContract", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("CustomerContract", "SeqNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        //Харилцагч дээрх асуудлын тухайн issue дээрх бүх жагсаалт авах
        public Result Txn310092(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            try
            {
                customerid = Static.ToLong(ri.ReceivedParam[0]);
                res = IPos.DB.CRMDB.DB253005(db,0,1, customerid);
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
                lg.item.Desc = "Харилцагч дээрх асуудлын тухайн issue дээрх бүх жагсаалт авах";
            }
        }
        #endregion
        #region[ Attach ]
        public Result Txn310098(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long IssueID;
            try
            {
                IssueID = Static.ToLong(ri.ReceivedParam[0]);
                res = IPos.DB.CRMDB.DB261000(db, 0, 1, IssueID);
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
                lg.item.Desc = "Харилцагч дээрх асуудлын тухайн issue дээрх бүх жагсаалт авах";
            }
        }
        #endregion

        #region[ Issue Tracking ]
        //Төсөл дээрх асуудлын тухайн issue дээрх бүх жагсаалт авах
        public Result Txn310123(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long projectid;
            try
            {
                projectid = Static.ToLong(ri.ReceivedParam[0]);
                res = IPos.DB.CRMDB.DB256011(db, projectid);
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
                lg.item.Desc = "Төсөл дээрх асуудлын тухайн issue дээрх бүх жагсаалт авах";
            }
        }
        //Төсөл дээрх асуудал шинээр нэмэх 
        public Result Txn310124(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256002(db, (object[])(ri.ReceivedParam[0]));
                if (res.ResultNo == 0)
                {
                    long IssueId = Static.ToLong(res.Param[0]);

                    #region[ SendMail copy ]
                    res = IPos.DB.CRMDB.DB250006(db, IssueId, 310124);
                    if (res.ResultNo != 0) return res;
                    DataTable DT = res.Data.Tables[0];
                    string[] ToUser = new string[DT.Rows.Count];
                    int k = 0;
                    if (DT.Rows.Count != 0)
                    {
                        DataRow rws = DT.NewRow();
                        foreach (DataRow rw in DT.Rows)
                        {
                            ToUser[k] = rw["email"].ToString();
                            k++;
                            rws = rw;
                        }
                        string Body = @"Шинэ: [" + IssueId.ToString() + "] " + rws["SUBJECT"].ToString()
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " шинээр нэмсэн"
                                        + "\r\n"
                                        + "-----------------------------------------"
                                        + "\r\n"
                                        + "Төсөлийн нэр : " + rws["PROJECTNAME"].ToString()
                                        + "\r\n"
                                        + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                                        + "\r\n"
                                        + ">>   Асуудлын шатлал - " + rws["ISSUETRACKNAME"].ToString()
                                        + "\r\n"
                                        + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                                        + "\r\n"
                                        + ">>   Үүсэгсэн хэрэглэгч - " + rws["REPORTERUSER"].ToString()
                                        + "\r\n"
                                        + ">>   Хариуцаж байгаа хэрэглэгч - " + rws["ASSIGNUSER"].ToString();
                        res = SendMail("Шинэ: ["+IssueId.ToString()+"] "+ rws["SUBJECT"].ToString(), Body, ToUser, db, ri);
                    }
                    #endregion
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
                lg.item.Desc = "Төсөл дээрх асуудал шинээр нэмэх";
            }
        }
        //Төсөл дээрх асуудал засах 
        public Result Txn310125(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256003(db, (object[])(ri.ReceivedParam[0]));
                if (res.ResultNo == 0)
                {
                    long IssueId = Static.ToLong(((object[])ri.ReceivedParam)[0]);

                    #region[ SendMail copy ]
                    res = IPos.DB.CRMDB.DB250006(db, IssueId, 310125);
                    if (res.ResultNo != 0) return res;
                    DataTable DT = res.Data.Tables[0];
                    string[] ToUser = new string[DT.Rows.Count];
                    int k = 0;
                    if (DT.Rows.Count != 0)
                    {
                        DataRow rws = DT.NewRow();
                        foreach (DataRow rw in DT.Rows)
                        {
                            ToUser[k] = rw["email"].ToString();
                            k++;
                            rws = rw;
                        }
                        string Body = @"CRM Асуудал засагдлаа - " + IssueId.ToString()
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + "\r\n"
                                        + ri.UserNo.ToString() + "-" + GetName(db, ri.UserNo) + " асуудал засав"
                                        + "\r\n"
                                        + "-----------------------------------------"
                                        + "\r\n"
                                        + "Төсөлийн нэр : " + rws["PROJECTNAME"].ToString()
                                        + "\r\n"
                                        + ">>   Асуудлын дугаар - " + IssueId.ToString()
                                        + "\r\n"
                                        + ">>   Асуудалын төрөл - " + rws["ISSUETYPENAME"].ToString()
                                        + "\r\n"
                                        + ">>   Товч утга - " + rws["SUBJECT"].ToString()
                                        + "\r\n"
                                        + ">>   Дэлгэрэнгүй - " + rws["DESCRIPTION"].ToString()
                                        + "\r\n"
                                        + ">>   Үүсгэгч - " + rws["REPORTERUSER"].ToString()
                                        + "\r\n"
                                        + ">>   Яг одоо харуйцаж байгаа хэрэглэгч - " + rws["ASSIGNUSER"].ToString();
                        res = SendMail("CRM Асуудал засварлах", Body, ToUser,db,ri);
                    }
                    #endregion
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
                lg.item.Desc = "Төсөл дээрх асуудал засах";
            }
        }
        //Төсөл дээрх асуудал устгах
        public Result Txn310126(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256004(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Төсөл дээрх асуудал устгах";
            }
        }
        #endregion
        #region[ Refresh All ]
        public Result RefreshAll(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long IssueID;
            try
            {
                DataSet DS = new DataSet();
                customerid = Static.ToLong(ri.ReceivedParam[0]);
                IssueID = Static.ToLong(ri.ReceivedParam[1]);

                res = IPos.DB.CRMDB.DB253005(db, 0, 1, customerid);
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "IssueList";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                
                res = IPos.DB.CRMDB.DB261000(db, 0, 1, IssueID);
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "IssueAttach";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res = IPos.DB.CRMDB.DB259000(db, IssueID);
                if (res.ResultNo != 0) return res;
                res.Data.Tables[0].TableName = "IssueTxn";
                DS.Tables.Add(res.Data.Tables[0].Copy());

                res.Data = DS;
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
        #region[ Жакад гаргаж өгөв ]
        public Result Txn310112(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB259001(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Асуудлын гүйлгээ жагсаалт авах";
            }
        }
        public Result Txn300015(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB256012(db,Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "DashBoard-ны aсуудлын дэлгэрэнгүй мэдээлэл авах";
            }
        }
        #endregion
        #region[ SEND MAIL ]
        Result SendMail(string Subject, string Body, string[] ToUser, DbConnections db, RequestInfo ri)
        {
            try
            {
                Result ress = IPos.DB.Main.DB203002(db, ri.UserNo);
                if (ress.ResultNo != 0) return ress;
                if (ress.Data == null) return new Result(1," Хэрэглэгчийн мэдээлэл олдонгүй .");
                if (ress.Data.Tables[0].Rows.Count == 0) return new Result(1, " Хэрэглэгчийн мэдээлэл олдонгүй .");
                DataRow rw = ress.Data.Tables[0].Rows[0];

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(rw["EMAIL"].ToString());
                for (int i = 0; i < ToUser.Length; i++)
                    msg.To.Add(new MailAddress(ToUser[i]));
                msg.Subject = Subject;
                msg.Body = Body;
                SmtpClient smtp = new SmtpClient(SystemProp.MServerName);
                smtp.Credentials = new System.Net.NetworkCredential(SystemProp.MailUserName, SystemProp.MailUserPass);
                smtp.Port = SystemProp.MServerPort;
                smtp.Send(msg);
                return new Result(0, "Майл амжилттай явлаа");
            }
            catch(Exception ex)
            {
                return new Result(1, ex.Message);
            }
        }
        string GetName(DbConnections db,int userno)
        {
            string name = "";
            try
            {
                Result res = IPos.DB.Main.DB203002(db, userno);
                if (res.ResultNo != 0) return "";
                if (res.Data == null) return "";
                if (res.Data.Tables[0].Rows.Count == 0) return "";
                DataRow dr = res.Data.Tables[0].Rows[0];
                name = Static.ToStr(dr["USERLNAME"]);
                return name;
            }
            catch 
            {
                return "";
            }
        
        }
        #endregion

        #endregion
    }
}
