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
    //
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
                    case 130001: 	            //Гэрээний жагсаалт авах
                        res = Txn130001(ci, ri, db, ref lg);
                        break;
                    case 130101: 	            //Захиалгын жагсаалт авах
                        res = Txn130101(ci, ri, db, ref lg);
                        break;
                    case 110100: 	            //Хэрэглэгчийн жагсаалт авах
                        res = Txn110100(ci, ri, db, ref lg);
                        break;
                    case 110105: 	            //Хэрэглэгчийн бүлгийн жагсаалт авах
                        res = Txn110105(ci, ri, db, ref lg);
                        break;
                    case 140211:                // Бараа материал жагсаалт авах
                        res = Txn140211(ci, ri, db, ref lg);
                        break;
                    case 140226:                // Үйлчилгээний жагсаалт авах
                        res = Txn140226(ci, ri, db, ref lg);
                        break;
                    case 000007:                // Динамик тайлангийн жагсаалт авах
                        res = Txn000007(ci, ri, db, ref lg);
                        break;
                    case 150000: 	            // Документ загварын жагсаалт
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
                    case 140246:                // Багц бүтээгдэхүүний жагсаалт авах
                        res = Txn140246(ci, ri, db, ref lg);
                        break;
                    case 310069:                // Төслийн жагсаалт авах
                        res = Txn310069(ci, ri, db, ref lg);
                        break;
                    case 310079:                // Асуудлын жагсаалт авах
                        res = Txn310079(ci, ri, db, ref lg);
                        break;
                    case 310125:                //Холбоо барьсан харилцагчийн жагсаалт авах
                        res = Txn310125(ci, ri, db, ref lg);
                        break;
                    case 310142:                //Холбоо барьсан харилцагчийн асуудлын жагсаалт авах
                        res = Txn310142(ci, ri, db, ref lg);
                        break;
                    case 310143:                // Асуудлын төслийн жагсаалт авах
                        res = Txn310143(ci, ri, db, ref lg);
                        break;
                    case 310144:                // Харилцагч болон холбоо барьсан харилцагчийн жагсаалт авах
                        res = Txn310144(ci, ri, db, ref lg);
                        break;
                    case 140366:                // Үнийн түүхийн жагсаалт авах
                        res = Txn140366(ci, ri, db, ref lg);
                        break;
                    case 140367:                // Борлуулалтын дэлгэрэнгүй жагсаалт BO
                        res = Txn140367(ci, ri, db, ref lg);
                        break;
                    case 140368:                // Борлуулсан бараа болон үйлчилгээний жагсаалт BO
                        res = Txn140368(ci, ri, db, ref lg);
                        break;
                    case 140369:                // Төлбөрийн жагсаалт BO
                        res = Txn140369(ci, ri, db, ref lg);
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
//            IPos.DB.Main.db204111
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
        public Result Txn130001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Гэрээний жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB204001(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
                    lg.item.Desc = "Гэрээний жагсаалт авах";
                    object[] FieldName = new object[20];

                    FieldName[0] = "ContractNo";
                    FieldName[1] = "ContractType";
                    FieldName[2] = "CustNo";
                    FieldName[3] = "FirstName";
                    FieldName[4] = "LastName";
                    FieldName[5] = "CorporateName";
                    FieldName[6] = "ValidStartDate";
                    FieldName[7] = "ValidStartTime";
                    FieldName[8] = "ValidEndDate";
                    FieldName[9] = "ValidEndTime";
                    FieldName[10] = "Balance";
                    FieldName[11] = "CurCode";
                    FieldName[12] = "PersonCount";
                    FieldName[13] = "DepFreq";
                    FieldName[14] = "DepAmount";
                    FieldName[15] = "Status";
                    FieldName[16] = "CreateDate";
                    FieldName[17] = "CreatePostDate";
                    FieldName[18] = "CreateUser";
                    FieldName[19] = "OwnerUser";

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("CONTRACT", FieldName[i].ToString(), "Гэрээний жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }
            }
        }
        public Result Txn130101(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Захиалгын жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB204101(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
                    object[] FieldName = new object[19];

                    FieldName[0] = "OrderNo";
                    FieldName[1] = "CustNo";
                    FieldName[2] = "FirstName";
                    FieldName[3] = "LastName";
                    FieldName[4] = "CorporateName";
                    FieldName[5] = "ConfirmTerm";
                    FieldName[6] = "TermType";
                    FieldName[7] = "OrderAmount";
                    FieldName[8] = "PrepaidAmount";
                    FieldName[9] = "CurCode";

                    FieldName[10] = "Fee";
                    FieldName[11] = "StartDate";
                    FieldName[12] = "EndDate";
                    FieldName[13] = "PersonCount";
                    FieldName[14] = "Status";
                    FieldName[15] = "CreateDate";
                    FieldName[16] = "PostDate";
                    FieldName[17] = "CreateUser";
                    FieldName[18] = "OwnerUser";

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("ORDER", FieldName[i].ToString(), "Захиалгын жагсаалт авах", Static.ToStr(pParam[i]));
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
        public Result Txn140211(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202211(db, ri.PageIndex, ri.PageRows, pParam);
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
        public Result Txn140226(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202226(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Үйлчилгээний жагсаалт авах";
                    object[] FieldName = new object[16];
                    FieldName[0] = "ServID";
                    FieldName[1] = "ServType";
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
                            lg.AddDetail("INVENTORY", FieldName[i].ToString(), "Үйлчилгээний жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }

            }
        }     //Үйлчилгээний жагсаалт авах
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
                res = IPos.DB.Main.DB202200(db, ri.PageIndex, ri.PageRows, pParam);
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
        public Result Txn140246(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Багц бүтээгдэхүүний жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202246(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Багц бүтээгдэхүүний жагсаалт авах";
                    object[] FieldName = new object[10];
                    FieldName[0] = "PackId";
                    FieldName[1] = "Name";
                    FieldName[2] = "Name2";
                    FieldName[3] = "Note";
                    FieldName[4] = "StartDate";
                    FieldName[5] = "EndDate";
                    FieldName[6] = "Type";
                    FieldName[7] = "Status";
                    FieldName[8] = "SalesUser";
                    FieldName[9] = "SalesCreated";

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("PackMainList", FieldName[i].ToString(), "Багц бүтээгдэхүүний жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }
            }
        }
        
        public Result Txn310069(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Төслийн жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.CRMDB.DB254000(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Төслийн жагсаалт авах";
                    object[] FieldName = new object[13];
                    FieldName[0] = "ProjectID";
                    FieldName[1] = "name";
                    FieldName[2] = "name2";
                    FieldName[3] = "ShortName";
                    FieldName[4] = "ShortName2";
                    FieldName[5] = "owneruser";
                    FieldName[6] = "startdate";
                    FieldName[7] = "enddate";
                    FieldName[8] = "projecttypeid";
                    FieldName[9] = "createuserno";
                    FieldName[10] = "createdate";
                    FieldName[11] = "NotifySchemaID";
                    FieldName[12] = "PermSchemaID";

                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("IssueProject", FieldName[i].ToString(), "Төслийн жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }
            }
        }
        public Result Txn310143(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Төслийн жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.CRMDB.DB254005(db, ri.PageIndex, ri.PageRows, pParam);
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


                //if (res.ResultNo == 0)
                //{
                //    lg.item.Desc = "Төслийн жагсаалт авах";
                //    object[] FieldName = new object[13];
                //    FieldName[0] = "ProjectID";
                //    FieldName[1] = "name";
                //    FieldName[2] = "name2";
                //    FieldName[3] = "ShortName";
                //    FieldName[4] = "ShortName2";
                //    FieldName[5] = "owneruser";
                //    FieldName[6] = "startdate";
                //    FieldName[7] = "enddate";
                //    FieldName[8] = "projecttypeid";
                //    FieldName[9] = "createuserno";
                //    FieldName[10] = "createdate";
                //    FieldName[11] = "NotifySchemaID";
                //    FieldName[12] = "PermSchemaID";

                //    for (int i = 0; i < FieldName.Length; i++)
                //    {
                //        if (pParam[i] != null)
                //        {
                //            lg.AddDetail("IssueProject", FieldName[i].ToString(), "Төслийн жагсаалт авах", Static.ToStr(pParam[i]));
                //        }
                //    }
                //}
            }
        }
        public Result Txn310079(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Асуудлын жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.CRMDB.DB256000(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Асуудлын жагсаалт авах";
                    object[] FieldName = new object[15];
                    FieldName[0] = "issueid";
                    FieldName[1] = "PROJECTCOMPID";
                    FieldName[2] = "IssueTypeID";
                    FieldName[3] = "IssuePriorID";
                    FieldName[4] = "Status";
                    FieldName[5] = "ResolutionStatus";
                    FieldName[6] = "TrackID";
                    FieldName[7] = "CreateUser";
                    FieldName[8] = "Createdate";
                    FieldName[9] = "UpdateDate";
                    FieldName[10] = "duedate";
                    FieldName[11] = "AssigneeUser";
                    FieldName[12] = "PermSchemaID";
                    FieldName[13] = "resolutiondate";
                    FieldName[14] = "resolutionuser";
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("Issue", FieldName[i].ToString(), "Асуудлын жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }
            }
        }
        public Result Txn310142(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Холбоо барьсан харилцагчийн асуудлын жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.CRMDB.DB256013(db, ri.PageIndex, ri.PageRows, pParam);
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
                    lg.item.Desc = "Асуудлын жагсаалт авах";
                    object[] FieldName = new object[15];
                    FieldName[0] = "issueid";
                    FieldName[1] = "PROJECTCOMPID";
                    FieldName[2] = "IssueTypeID";
                    FieldName[3] = "IssuePriorID";
                    FieldName[4] = "Status";
                    FieldName[5] = "ResolutionStatus";
                    FieldName[6] = "TrackID";
                    FieldName[7] = "CreateUser";
                    FieldName[8] = "Createdate";
                    FieldName[9] = "UpdateDate";
                    FieldName[10] = "duedate";
                    FieldName[11] = "AssigneeUser";
                    FieldName[12] = "PermSchemaID";
                    FieldName[13] = "resolutiondate";
                    FieldName[14] = "resolutionuser";
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (pParam[i] != null)
                        {
                            lg.AddDetail("Issue", FieldName[i].ToString(), "Асуудлын жагсаалт авах", Static.ToStr(pParam[i]));
                        }
                    }
                }
            }
        }
        public Result Txn310144(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Харилцагч болон холбоо барьсан харилцагчийн жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.CRMDB.DB266000(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        public Result Txn310125(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Холбоо барьсан харилцагчийн жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.CRMDB.DB263000(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        public Result Txn140366(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)        //Үнийн түүхийн жагсаалт авах
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202287(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        //Борлуулалтын дэлгэрэнгүй жагсаалт BO
        public Result Txn140367(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202318(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        //Борлуулсан бараа болон үйлчилгээний жагсаалт BO
        public Result Txn140368(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202319(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        //Төлбөрийн жагсаалт BO
        public Result Txn140369(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] pParam = ri.ReceivedParam;
            try
            {
                res = IPos.DB.Main.DB202320(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        #endregion
    }
}
