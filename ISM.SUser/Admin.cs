using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Interface;
using EServ.Shared;

using ISM.DB;
using IPos.Core;

namespace ISM.SUser
{
    public class User : IModule
    {
        #region Static Objects
        static internal Hashtable _husers = new Hashtable();
        #endregion
        #region [ Init functions ]
        static public Result Init(DbConnections dbs)
        {
            Result res = new Result();
            EServ.Interface.Events.EventReceived += new Events.delegateEventReceived(Events_EventReceived);
            return res;
        }
        static User()
        {
            EServ.Interface.Events.EventReceived +=new Events.delegateEventReceived(Events_EventReceived);
        }
        static void  Events_EventReceived(ClientInfo ci, RequestInfo ri, DbConnections dbs, int ReceivedBytes, ref Result res, ref bool cancel)
        {
 	        res = CheckTxn(ri.UserNo,ri.PrivilegeNo, dbs);
        }
        public static Result CheckTxn(int pUserNo, int pTranCode, DbConnections db)
        {
            Result res = new Result();

            if (pTranCode == 110000 || pTranCode == 100000 || pTranCode == 280000) { res.ResultNo = 0; return res; }

            res = ISM.DB.Main.DB101004(db, pUserNo, pTranCode);
            if (res.ResultNo != 0)
                return res;

            if (res.Data.Tables[0].Rows.Count == 0)
            {
                res.ResultNo = 9110007;
                res.ResultDesc = "Хэрэглэгч ийм эрхгүй байна (" + pTranCode.ToString()+")";
                return res;
            }

            return res;
        }
        #endregion
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            DateTime First = DateTime.Now;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 110000:        // Login
                        res = Login(ci, ri, db, ref lg);
                        break;
                    case 110001:        // Logout
                        res = Logout(ci, ri, db, ref lg);
                        break;
                    case 110002:        // ChangePass
                        res = ChangePass(ci, ri, db, ref lg);
                        break;
                    case 110003:        // ChangePass
                        res = CheckPosWorkArea(ci, ri, db, ref lg);
                        break;
                    case 110113:        //InitPassPolicy
                        res = InitPassPolicy(ci, ri, db, ref lg);
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
                    ISM.Lib.Static.WriteToLogFile("ISM.SUser", "<<Start : " + ISM.Lib.Static.ToStr(First) + ">>\r\n UserNo : " + ISM.Lib.Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + lg.item.Desc + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + ISM.Lib.Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("ISM.SUser", "<<Start : " + ISM.Lib.Static.ToStr(First) + ">>\r\n UserNo : " + ISM.Lib.Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + lg.item.Desc + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + ISM.Lib.Static.ToStr(Last) + ">>\r\n");
            }
        }
        #region [ Business functions ]
        public Result Login(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();
            DataTable DT;
            int WrongCount;
            DateTime LastChangeDate;
            UserInfo ui = new UserInfo(ci);

            try
            {
                #region Prepare parameters
                int pUserNo = Static.ToInt(ri.ReceivedParam[0]);
                string pPass = Static.ToStr(ri.ReceivedParam[1]);
                string pUserName = Static.ToStr(ri.ReceivedParam[2]);
                string pUserIP = Static.ToStr(ri.ReceivedParam[3]);
                string pUserMAC = Static.ToStr(ri.ReceivedParam[4]);
                #endregion

                string sql = @"select * from";
                res = ISM.DB.Main.DB101001(db, pUserNo);
                if (res.ResultNo != 0)
                    return res;

                DT = res.Data.Tables[0];

                if (DT.Rows.Count == 0)
                {
                    res.ResultNo = 9110003;
                    res.ResultDesc = "Ийм хэрэглэгч бүртгэгдээгүй байна";
                    return res;
                }

                WrongCount = Static.ToInt(DT.Rows[0]["WRONGCOUNT"]);
                LastChangeDate = Static.ToDate(DT.Rows[0]["PASSCHDATE"]);

                if (Static.ToInt(DT.Rows[0]["Status"]) != 0)
                {
                    res.ResultNo = 9110004;
                    res.ResultDesc = "Хэрэглэгч идэвхгүй байна";
                    return res;
                }

                if (Static.ToStr(DT.Rows[0]["UPassword"]).Trim() != "")
                {
                    if (Static.ToStr(DT.Rows[0]["UPassword"]) != pPass)
                    {
                        Result ResCheck = new Result();
                        ResCheck = ISM.DB.Main.DB101005(db);
                        if (ResCheck.ResultNo == 0)
                        {
                            #region[Хэтэрсэн байвал идвэхгүй болгоно]
                            int Count = Static.ToInt(ResCheck.Data.Tables[0].Rows[0]["WRONGCOUNT"]);
                            if (WrongCount >= Count - 1)
                            {
                                //Идвэхгүй болгоно
                                ResCheck = ISM.DB.Main.DB101011(db, pUserNo);
                                if (ResCheck.ResultNo != 0)
                                    return ResCheck;
                                else
                                {
                                    //Буруу оруулсан тоог ахиулж байна .
                                    ResCheck = ISM.DB.Main.DB101010(db, pUserNo);
                                    if (ResCheck.ResultNo != 0)
                                        return ResCheck;
                                    res.ResultNo = 9110078;
                                    res.ResultDesc = "Та нууц үгээ "+(WrongCount+1).ToString()+" удаа буруу оруулсан тул таны эрх хаагдлаа. Администратортайгаа холбоо барина уу.";
                                    return res;
                                }
                            }
                            #endregion
                            #region[Буруу оруулсан байвал тоог ахиулна]
                            else
                            {
                                //Буруу оруулсан тоог ахиулж байна .
                                ResCheck = ISM.DB.Main.DB101010(db, pUserNo);
                                if (ResCheck.ResultNo != 0)
                                    return ResCheck;
                            }
                            #endregion
                        }
                        else
                            return ResCheck;

                        res.ResultNo = 9110005;
                        res.ResultDesc = "Нууц үг буруу байна";
                        return res;
                    }
                }

                ui.UserNo = pUserNo;
                ui.BranchNo = Static.ToInt(DT.Rows[0]["BranchNo"]);
                ui.FName = Static.ToStr(DT.Rows[0]["UserFName"]);
                ui.FName2 = Static.ToStr(DT.Rows[0]["UserFName2"]);
                ui.LName = Static.ToStr(DT.Rows[0]["UserLName"]);
                ui.LName2 = Static.ToStr(DT.Rows[0]["UserLName2"]);
                ui.Level = Static.ToInt(DT.Rows[0]["UserLevel"]);
                ui.Password = Static.ToStr(DT.Rows[0]["UPassword"]);
                ui.Position = Static.ToStr(DT.Rows[0]["Position"]);
                ui.Status = Static.ToInt(DT.Rows[0]["Status"]);

                object[] p = new object[11];


                p[0] = Static.ToInt(DT.Rows[0]["BranchNo"]);
                p[1] = Static.ToStr(DT.Rows[0]["UserFName"]);
                p[2] = Static.ToStr(DT.Rows[0]["UserFName2"]);
                p[3] = Static.ToInt(DT.Rows[0]["UserLevel"]);
                p[4] = Static.ToStr(DT.Rows[0]["UserLName"]);
                p[5] = Static.ToStr(DT.Rows[0]["UserLName2"]);
                //шинэ
                p[6] = Static.ToStr(DT.Rows[0]["Level1"]);
                p[7] = Static.ToInt(DT.Rows[0]["Level2"]);
                p[8] = Static.ToStr(DT.Rows[0]["Level3"]);
                p[9] = Static.ToStr(DT.Rows[0]["Level4"]);
                p[10] = Static.ToStr(DT.Rows[0]["txngrouplevel"]);

                

                res = ISM.DB.Main.DB101003(db, pUserNo);     // Select user trancode lists
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "TXN";
                if (IPos.Core.SystemProp.GenList != null)
                {
                    res.Data.Tables.Add(IPos.Core.SystemProp.GenList.Copy());
                }
                res.Data.Tables[1].TableName = "PARAM";
                ret.Data = res.Data;
                ret.ResultNo = 0;
                ret.ResultDesc = "Амжилттай холбогдлоо";
                #region[Нууц үг буруу оруулалтыг 0 болгоно]
                Result ResClean = new Result();
                ResClean = ISM.DB.Main.DB101012(db, pUserNo);
                if (ResClean.ResultNo != 0)
                    return ResClean;
                #endregion
                #region[Нууц үгийн бүртгэлийн мэдээллээс VALIDDAY буюу хүчинэй байх өдрийг авч байна .]
                Result ResDate = new Result();
                ResDate = ISM.DB.Main.DB101005(db);
                if (ResDate.ResultNo != 0)
                    return ResDate;
                int ValDate = Static.ToInt(ResDate.Data.Tables[0].Rows[0]["VALIDDAY"]);
                #endregion
                #region[Шалгах утгыг бэлдэж байна CheckDTServer-server time CheckDTVal]
                DateTime DTime=DateTime.Now;
                DateTime ValDateTime = LastChangeDate.AddDays(ValDate);
                DateTime CheckDTServer = new DateTime(DTime.Year, DTime.Month, DTime.Day);
                DateTime CheckDTVal = new DateTime(ValDateTime.Year,ValDateTime.Month,ValDateTime.Day);
                #endregion
                #region[Нууц үг хамгийн сүүлд оруулсан хугацаа дээр хүчинтэй байх өдөрийг нэмээд server-н цагтай жишинэ]
                if (CheckDTVal <= CheckDTServer)
                {
                      p[6] = false;
                }
                else
                    p[6] = true;
                ret.Param = p;
                #endregion

                if (EServ.Interface.Users.Add(ui) == false)
                {
                    res.ResultNo = 9110008;
                    res.ResultDesc = "Хэрэглэгч нэвтрэхэд алдаа гарлаа";
                    return res;
                }

                if (Static.ToInt(DT.Rows[0]["LoginType"]) == 1)
                {
                    #region Check user is connected before from user list
                    UserInfo oldUser = null;
                    lock (_husers.SyncRoot)
                    { oldUser = (UserInfo)_husers[pUserNo]; }
                    if (oldUser != null)
                    {
                        Result r = new Result(9110071, string.Format("Энэ хэрэглэгчийн дугаараар өөр Терминалаас нэвтэрлээ.\r\nIP  = {0}\r\nMAC = {1}", pUserIP, pUserMAC));
                        EServ.Interface.Server.SendTo(oldUser.ClientId, pUserNo, r);
                        EServ.Interface.Users.OnDisconnect(oldUser.ClientId);
                    }
                    #endregion
                }

                #region Add user info into user list
                lock (_husers.SyncRoot)
                { _husers[pUserNo] = ui; }
                #endregion

                return ret;
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
                lg.item.Desc = "Логин хийх";
            }
        }
        public Result Logout(ClientInfo ci, RequestInfo ri, DbConnections db,ref Log lg)
        {
            Result res = new Result();
            //DataTable DT;

            try
            {
                int pUserNo = Static.ToInt(ri.ReceivedParam[0]);
                string pPass = Static.ToStr(ri.ReceivedParam[1]);
                string pUserName = Static.ToStr(ri.ReceivedParam[2]);
                string pUserIP = Static.ToStr(ri.ReceivedParam[3]);
                string pUserMAC = Static.ToStr(ri.ReceivedParam[4]);

                // Орсон хэрэглэгчийн тоог хөтлөөд, 0 болох үед нь хасах
                // Үүнийг дараа хийх!
                //lock (_husers.SyncRoot)
                //{ _husers.Remove(pUserNo); }
                
                res.ResultNo = 0;
                res.ResultDesc = "Программаас гарах";
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
                lg.item.Desc = "Нууц үг солих";
            }
        }
        public Result ChangePass(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                int pUserNo = Static.ToInt(ri.ReceivedParam[0]);
                string pOldPass = Static.ToStr(ri.ReceivedParam[1]);
                string pNewPass = Static.ToStr(ri.ReceivedParam[2]);
                int Count = Static.ToInt(ri.ReceivedParam[4]);
                    #region[ History-д нууц үг байна уу гэдэгийг шалгаж байна .]
                    res = ISM.DB.Main.DB101008(db, pUserNo, pNewPass);
                    if (res.ResultNo == 0)
                    {
                        if (res.Data.Tables[0].Rows.Count != 0)
                        {
                            res.ResultNo = 9110077;
                            res.ResultDesc = "Энэ нууц үгийг өмнө нь ашигласан байна .";
                            return res;
                        }
                    }
                    else
                        return res;
                    #endregion
                    #region[ Нууц үг солих]
                    //Солиж байна 
                    res = ISM.DB.Main.DB101002(db, pUserNo, pOldPass, pNewPass);
                    if (res.ResultNo != 0)
                        return res;
                    #endregion
                    
                    #region[ Хэрэв тоондоо хүрээгүй бол шууд хадгалах хүрсэн бол устгана]
                    res = ISM.DB.Main.DB101006(db, pUserNo);
                    if (res.ResultNo == 0)
                    {
                        int CountHis = res.Data.Tables[0].Rows.Count;
                        if (CountHis >= Count)
                        {
                            long SeqNo = Static.ToLong(res.Data.Tables[0].Rows[0]["SEQNO"]);
                            res = ISM.DB.Main.DB101009(db, SeqNo);

                            if (res.ResultNo != 0)
                                return res;
                        }
                    }
                    else
                        return res;
                    #endregion
                    #region[ Түүхэнд нэмэх]
                    DateTime DT = DateTime.Now;
                    ulong uSeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");

                    //Түүхэнд хадгална
                    res = ISM.DB.Main.DB101007(db, new object[] { uSeqNo, pUserNo, pNewPass, DT });
                    if (res.ResultNo != 0)
                        return res;

                    res.ResultNo = 0;
                    res.ResultDesc = "Нууц үгийг амжилттай солилоо";
                    return res;
                    #endregion
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
                lg.item.Desc = "Нууц үг солих";
            }
        }
        public Result CheckPosWorkArea(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"select * from posterminal a
left join workarealink b on b.id=a.posno
where b.type=0 and a.posno=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "CheckPosWorkArea", ri.ReceivedParam[0]);
                if (res.Data == null)
                {
                    res.ResultNo = 9112000;
                    res.ResultDesc = "Пос бүртгэлгүй эсвэл ажлын талбарт ороогүй байна.";
                    return res;
                }
                else
                {
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 9112000;
                        res.ResultDesc = "Пос бүртгэлгүй эсвэл ажлын талбарт ороогүй байна.";
                        return res;
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
                lg.item.Desc = "Нууц үг солих";
            }
        }
        public Result InitPassPolicy(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();

            try
            {
                res = ISM.DB.Main.DB101005(db);
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
                lg.item.Desc = "Нууц үгийн бүртгэлийн мэдээлэл авах";
            }
        }
        #endregion
    }
}
