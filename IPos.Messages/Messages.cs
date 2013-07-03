using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using IPos.Core;

namespace IPos.Messages
{
    public class Messages : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            DateTime First = DateTime.Now;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 110115: 	           
                        res = TxnSelectListMessage(ci, ri, db, ref lg);
                        break;
                    case 110116: 	          
                        res = TxnSelectMessage(ci, ri, db, ref lg);
                        break;
                    case 110117: 	           
                        res = TxnInsertMessage(ci, ri, db, ref lg);
                        break;
                    case 110118: 	          
                        res = TxnUpdateMessage(ci, ri, db, ref lg);
                        break;
                    case 110119: 	           
                        res = TxnDeleteMessage(ci, ri, db, ref lg);
                        break;
                    case 110120:
                        res = Txn110120(ci, ri, db, ref lg);
                        break;
                    case 110121:
                        res = Txn110121(ci, ri, db, ref lg);
                        break;
                    case 110122:
                        res = Txn110122(ci, ri, db, ref lg);
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
                DateTime Last = DateTime.Now;
                if (res.ResultNo == 0)
                    ISM.Lib.Static.WriteToLogFile("Hpro.Messages", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + lg.item.Desc + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("Hpro.Messages", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + lg.item.Desc + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
            }
        }

        #region[ Function ]

        public Result TxnInsertMessage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                DateTime PostDate=DateTime.Now;
                int type=Static.ToInt(ri.ReceivedParam[4]);
                switch(type)
                {
                    case 0:
                        {
                            #region[ Set Value ]
                            UserInfo UInfo;
                            UserInfo ToInfo;
                            int ToUser = Convert.ToInt32(ri.ReceivedParam[1]);
                                object[] Param = new object[] { ri.ReceivedParam[2].ToString(),ri.UserNo, PostDate};
                                //MSGID,TXNDATE,POSTDATE,FROMUSERNO,TOUSERNO,DESCRIPTION,ISREAD
                                object[] InsertParam = new object[] { 0, Static.ToDate(ri.ReceivedParam[3]), PostDate, ri.UserNo, ToUser, ri.ReceivedParam[2].ToString(), 0 };
                            #endregion
                            #region[ Insert DB ]
                            res = ISM.DB.Main.DB102002(db, InsertParam);
                            #endregion
                            #region[ Send Messages ]
                            if ((UserInfo)Users.Get(ToUser) != null)
                            {
                                UInfo = (UserInfo)Users.Get(ri.UserNo);
                                ToInfo = (UserInfo)Users.Get(ToUser);
                                Result SendMessage = new Result();
                                SendMessage.ResultNo = 5;
                                SendMessage.Param = Param;
                                EServ.Interface.Server.SendTo(ToInfo.ClientId, ToUser, SendMessage);
                            }
                            #endregion
                        }
                        break;
                    case 1:
                        {
                            res=ISM.DB.Main.DB102008(db,Static.ToInt(ri.ReceivedParam[0]));
                            if(res.ResultNo==0)
                            {
                                DataTable DT=res.Data.Tables[0];
                                foreach(DataRow rw in DT.Rows)
                                {
                                #region[ Set Value ]
                                UserInfo UInfo;
                                UserInfo ToInfo;
                                int ToUser = Static.ToInt(rw["UserNo"]);
                                object[] Param = new object[] { ri.ReceivedParam[2].ToString(),ri.UserNo, PostDate};
                                //MSGID,TXNDATE,POSTDATE,FROMUSERNO,TOUSERNO,DESCRIPTION,ISREAD
                                object[] InsertParam = new object[] { 0, Static.ToDate(ri.ReceivedParam[3]), PostDate, ri.UserNo, ToUser, ri.ReceivedParam[2].ToString(), 0 };
                                #endregion
                                #region[ Insert DB ]
                                res = ISM.DB.Main.DB102002(db, InsertParam);
                                #endregion
                                #region[ Send Messages ]
                                if ((UserInfo)Users.Get(ToUser) != null)
                                {
                                    UInfo = (UserInfo)Users.Get(ri.UserNo);
                                    ToInfo = (UserInfo)Users.Get(ToUser);
                                    Result SendMessage = new Result();
                                    SendMessage.ResultNo = 5;
                                    SendMessage.Param = Param;
                                    EServ.Interface.Server.SendTo(ToInfo.ClientId, ToUser, SendMessage);
                                }
                                #endregion
                                }
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
                lg.item.Desc = "Message шинээр үүсгэх";
            }
        }
        public Result TxnSelectListMessage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {

                #region[ Select List DB ]
                res = ISM.DB.Main.DB102000(db, null);
                #endregion

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
                lg.item.Desc = "Message жагсаалт мэдээлэл авах";
            }
        }
        public Result TxnSelectMessage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region[ SetValue ]
                long MsgID = 0;
                #endregion
                #region[ Select DB ]
                res = ISM.DB.Main.DB102001(db, MsgID);
                #endregion
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
                lg.item.Desc = "Message дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result TxnUpdateMessage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region[ SetValue ]
                object[] UpdateParam = new object[5];
                #endregion
                #region[ Update DB ]
                res = ISM.DB.Main.DB102003(db, UpdateParam);
                #endregion
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
                lg.item.Desc = "Message засварлах";
            }
        }
        public Result TxnDeleteMessage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region[ Delete DB ]
                res = ISM.DB.Main.DB102004(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]), Static.ToInt(ri.ReceivedParam[2]));
                #endregion
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
                lg.item.Desc = "Message устгах";
            }
        }

        public Result Txn110120(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = ISM.DB.Main.DB102005(db,ri.UserNo);
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
                lg.item.Desc = "Ирсэн хэрэглэгчийн дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result Txn110121(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = ISM.DB.Main.DB102006(db,ri.UserNo);
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
                lg.item.Desc = "Явуулсан хэрэглэгчийн дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result Txn110122(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = ISM.DB.Main.DB102007(db,Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Уншсан ,уншаагүй төлөвийн жагсаалт мэдээлэл авах";
            }
        }


        #endregion
    }
}
