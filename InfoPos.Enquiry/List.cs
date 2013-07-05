using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;

using IPos.DB;
using IPos.Core;

namespace IPos.List
{
    public class List : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime first=new DateTime();
            Result res = new Result();
            try
            {
                first = DateTime.Now;
                switch (ri.FunctionNo)
                {
                    case 120000: 	            //Харилцагчийн жагсаалт авах
                        res = Txn120000(ci, ri, db, ref lg);
                        break;
                    case 110100: 	            //Хэрэглэгчийн жагсаалт авах
                        res = Txn110100(ci, ri, db, ref lg);
                        break;
                    case 110105: 	            //Хэрэглэгчийн бүлгийн жагсаалт авах
                        res = Txn110105(ci, ri, db, ref lg);
                        break;
                    case 170001:                // Бараа материал жагсаалт авах
                        res = Txn170001(ci, ri, db, ref lg);
                        break;
                    case 000007:                // Динамик тайлангийн жагсаалт авах
                        res = Txn000007(ci, ri, db, ref lg);
                        break;
                    case 150000: 	            //Документ загварын жагсаалт
                        res = Txn150000(ci, ri, db, ref lg);    
                        break;
                    case 140161:                // Гүйлгээний кодуудын жагсаалт
                        res = Txn140161(ci, ri, db, ref lg);  
                        break;
                    case 140176:                // Гүйлгээний оролтуудын жагсаалт авах
                        res = Txn140176(ci, ri, db, ref lg);  
                        break;
                    case 140200:                // Ханшийн түүх харах
                        res = Txn140200(ci, ri, db, ref lg);
                        break;
                    case 110200:                // Log бүртгэлийн жагсаалт авах
                        res = Txn110200(ci, ri, db, ref lg);
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

                ISM.Lib.Static.WriteToLogFile("IPos.List", "\r\n<<Start:" + Static.ToStr(first) + ">>\r\n UserNo:" + ISM.Lib.Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + Static.ToStr(ri.UserNo) + "\r\n Description:" + Static.ToStr(lg.item.Desc) + "\r\n ResultNo:" + Static.ToStr(res.ResultNo) + "\r\n ResultDescription:" + ResultDesk + "\r\n<<End " + Static.ToStr(second) + ">>");
            }
        }
        #region [ Business functions ]
        public Result Txn120000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Харилцагчийн жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB205001(db, ri.PageIndex, ri.PageRows,ri.ReceivedParam);
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
                    lg.item.Desc = "Харилцагчийн жагсаалт авах";
                    object[] FieldName = new object[15];
                    FieldName[0] = "CustomerNo";
                    FieldName[1] = "ClassCode";
                    FieldName[2] = "BranchNo";
                    FieldName[3] = "Status";
                    FieldName[4] = "FirstName";
                    FieldName[5] = "LastName";
                    FieldName[6] = "RegisterNo";
                    FieldName[7] = "PassNo";
                    FieldName[8] = "Sex";
                    FieldName[9] = "isHInsurance";
                    FieldName[10] = "isSInsurance";
                    FieldName[11] = "TypeCode";
                    FieldName[12] = "CorporateName";
                    FieldName[13] = "InduTypeCode";
                    FieldName[14] = "InduSubTypeCode";

                    FieldName[3] = "DirLastName";
                    FieldName[4] = "RateCode";
                    FieldName[5] = "isOtherInsurance";
                    FieldName[6] = "Email";
                    FieldName[7] = "Telephone";
                    FieldName[8] = "Mobile";
                    FieldName[9] = "HomePhone";
                    FieldName[10] = "Fax";
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("CUSTOMER", FieldName[i].ToString(), "Харилцагчийн жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }
            }
        }
        public Result Txn110200(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //log бүртгэлийн жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB203200(db, ri.PageIndex, ri.PageRows, pParam);
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
        public Result Txn110100(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB203001(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
                    object[] FieldName = new object[15];
                    FieldName[0] = "UserNo";
                    FieldName[1] = "USERFNAME";
                    FieldName[2] = "USERLNAME";
                    FieldName[3] = "UserFname2";
                    FieldName[4] = "UserLname2";
                    FieldName[5] = "RegisterNo";
                    FieldName[6] = "Position";
                    FieldName[7] = "Status";
                    FieldName[8] = "BRANCHNO";
                    FieldName[9] = "UserLevel";
                    FieldName[10] = "UserType";
                    FieldName[11] = "Email";
                    FieldName[12] = "Mobile";
                    FieldName[13] = "agentcorpno";
                    FieldName[14] = "agentbranchno";
               

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("hpuser", FieldName[i].ToString(), "Хэрэглэгчийн жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }


            }
        }     //Хэрэглэгчийн жагсаалт авах
        public Result Txn110105(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB203012(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Хэрэглэгчийн бүлгийн жагсаалт авах";
                    object[] FieldName = new object[4];
                    FieldName[0] = "GROUPID";
                    FieldName[1] = "Name";
                    FieldName[2] = "Name2";
                    FieldName[3] = "OrderNo";
           

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("USERGROUP", FieldName[i].ToString(), "Хэрэглэгчийн бүлгийн жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }
 
		
            }
        }     //Хэрэглэгчийн бүлгийн жагсаалт авах
        public Result Txn170001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB206001(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Бараа материал жагсаалт авах";
                    object[] FieldName = new object[16];
                    FieldName[0] = "InvID";
                    FieldName[1] = "InvTypeID";
                    FieldName[2] = "Name";
                    FieldName[3] = "Name2";
                    FieldName[4] = "BranchNo";
                    FieldName[5] = "CreateUser";
                    FieldName[6] = "UnitTypeCode";
                    FieldName[7] = "BalanceCount";
                    FieldName[8] = "UnitCost";
                    FieldName[9] = "BalanceTotal";
                    FieldName[10] = "CurrCode";
                    FieldName[11] = "Position";
                    FieldName[12] = "AccountNo";
                    FieldName[13] = "EmpNo";
                    FieldName[14] = "LastTellerTxnDate";
                    FieldName[15] = "LEVELNO";

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("INVENTORY", FieldName[i].ToString(), "Бараа материал жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }
 
            }
        }     //Бараа материал жагсаалт авах
        public Result Txn000007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            //int pagenumber = 1;
            //int pagecount = 1;
            try
            {
                res = IPos.DB.Main.DB201001(db,ri.PageIndex,ri.PageRows, ri.ReceivedParam);
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
                lg.item.Desc = "Тайлангийн документ загварын жагсаалт авах";
            }
        }     //Динамик тайлангийн жагсаалт авах
        public Result Txn150000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Документ загварын жагсаалт
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            
            try
            {
                res = IPos.DB.Main.DB201001(db,ri.PageIndex,ri.PageRows, pParam);
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
                    lg.item.Desc = "Документ загварын жагсаалт";
                    object[] FieldName = new object[5];
                    FieldName[0] = "Id";
                    FieldName[1] = "Name";
                    FieldName[2] = "Name2";
                    FieldName[3] = "DOCFILENAME";
                    FieldName[4] = "EXPORTTYPE";

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("DOCTEMPLATE", FieldName[i].ToString(), "Документ загварын жагсаалт", Static.ToStr(pParam[i]));
                        }
                    }
                }
            }
        }
        public Result Txn140161(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Гүйлгээний кодуудын жагсаалт
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202316(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
                    lg.item.Desc = "Гүйлгээний кодуудын жагсаалт";
                    object[] FieldName = new object[3];
                    FieldName[0] = "TranCode";
                    FieldName[1] = "Name";
                    FieldName[2] = "Name2";
                  

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("List", FieldName[i].ToString(), "Гүйлгээний кодуудын жагсаалт", Static.ToStr(pParam[i]));
                        }
                    }
                }

            }
        }
        public Result Txn140176(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Гүйлгээний оролтуудын жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202317(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Гүйлгээний оролтуудын жагсаалт";
                    object[] FieldName = new object[3];
                    FieldName[0] = "Trancode";
                    FieldName[1] = "EntryCode";
                    FieldName[2] = "OrderNo";
                  

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("TXNENTRY", FieldName[i].ToString(), "Гүйлгээний оролтуудын жагсаалт", Static.ToStr(pParam[i]));
                        }
                    }
                }



            }
        }
        public Result Txn140200(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Ханшийн түүх харах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202200(db,ri.PageIndex,ri.PageRows, pParam);
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
                    lg.item.Desc = "Валютын ханшийн жагсаалт";
                    object[] FieldName = new object[2];
                    FieldName[0] = "CURRENCY";
                    FieldName[1] = "CURDATE";
                 
                

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("CURRENCY", FieldName[i].ToString(), "Валютын ханшийн жагсаалт", Static.ToStr(pParam[i]));
                        }
                    }
                }
 

            }
        }
        #endregion
    }
}

