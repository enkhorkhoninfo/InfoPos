using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ;
using EServ.Shared;
using EServ.Interface;
using EServ.Data;

namespace IPos.Core
{
    public class Core : IModule
    {
        static public Result Init(DbConnections dbs)
        {
            EServ.Interface.Events.OnServerTraceLog(0, "InfoPos Core Init...");
            SystemProp.gBACProd = new BACProd();
            SystemProp.gCONProd = new CONProd();
            SystemProp.gCur = new Cur();
            SystemProp.gFAType = new FAType();
            SystemProp.gInvType = new InvType();
            SystemProp.gFinTxn = new FinTxn();
            SystemProp.gBranch = new Branch();
            SystemProp.gAccountCode = new AccountCode();
            SystemProp.gChartGroup = new ChartGroup();
            SystemProp.gAutoNum = new AutoNum();

            return InitAll(dbs);
        }
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime First = DateTime.Now;
            string Desc = "";
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 100000: 
                        Desc = "Init ALL";
                        res = HPo_InitAll(ci, ri, db, ref lg);
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
                    ISM.Lib.Static.WriteToLogFile("InfoPos.Core", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Desc + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("InfoPos.Core", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Desc + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
            }
        }
        static public Result HPo_InitAll(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();

            try
            {
                res.ResultNo = 0;
                DataSet DS = new DataSet();
                DS.Tables.Add(SystemProp.GenList.Copy());
                res.Data = DS;
                object[] obj = new object[1];
                obj[0] = SystemProp.TxnDate;
                res.Param = obj;
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        static public Result InitAll(DbConnections dbs)
        {
            Result res =  new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("InitALL ...");

                #region [ Init General List ]
                res = IPos.DB.Main.DB200001(dbs);

                if (res.ResultNo != 0)
                    return res;

                SystemProp.GenList = res.Data.Tables[0].Copy();
                SystemProp.GenList.TableName = "GENERALPARAM";

                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    switch (Static.ToStr(DR["Key"]).ToUpper())
                    {
                        case "INSTCODE": SystemProp.InstCode = Static.ToStr(DR["ItemValue"]); break;
                        case "INSTNAME": SystemProp.InstName = Static.ToStr(DR["ItemValue"]); break;
                        case "INSTNAME2": SystemProp.InstName2 = Static.ToStr(DR["ItemValue"]); break;
                        case "BASECURRENCY": SystemProp.BaseCurrency = Static.ToStr(DR["ItemValue"]); break;
                        case "BACFORMAT": SystemProp.BACFormat = Static.ToStr(DR["ItemValue"]); break;
                        case "POSGLNUMBER": SystemProp.PosGLNumber = Static.ToLong(DR["ItemValue"]); break;
                        case "YEARPROFITGLNUMBER": SystemProp.YearProfitGLNumber = Static.ToLong(DR["ItemValue"]); break; 
                        case "YEARLOSSGLNUMBER": SystemProp.YearLossGLNumber = Static.ToLong(DR["ItemValue"]); break;
                        case "TXNDATE": SystemProp.TxnDate = Static.ToDate(DR["ItemValue"]); break;
                        case "SYSTEMBRANCHNO": SystemProp.SystemBranchNo = Static.ToInt(DR["ItemValue"]); break;
                        case "GLPROCESSSTATUS": SystemProp.GLProcessStatus  = Static.ToInt(DR["ItemValue"]); break;
                        case "POLICYMASK": SystemProp.PolicyMask = Static.ToStr(DR["ItemValue"]); break;
                        case "GLTXNDATE": SystemProp.GLTxnDate = Static.ToDate(DR["ItemValue"]); break;
                        case "GLPROCESSSTART": SystemProp.GLProcessStart = Static.ToInt(DR["ItemValue"]); break;
                        case "EODPROCESSSTART": SystemProp.EODProcessStart = Static.ToInt(DR["ItemValue"]); break;

                        case "PATHTERMINAL": SystemProp.PathTerminal = Static.ToStr(DR["ItemValue"]); break;
                        case "PATHDYNAMICRPT": SystemProp.PathDynamicRpt = Static.ToStr(DR["ItemValue"]); break;
                        case "PATHDYNAMICDOC": SystemProp.PathDynamicDoc = Static.ToStr(DR["ItemValue"]); break;
                        case "PATHSLIPS": SystemProp.PathSlips = Static.ToStr(DR["ItemValue"]); break;
                        case "UPDATEEXTENTION": SystemProp.UpdateExtention = Static.ToStr(DR["ItemValue"]); break;

                        case "VAT": SystemProp.VAT = Static.ToLong(DR["ItemValue"]); break;
                        case "VATACCOUNTNO": SystemProp.VatAccountNo = Static.ToStr(DR["ItemValue"]); break;
                        case "SALEACCOUNTNO": SystemProp.SaleAccountNo = Static.ToStr(DR["ItemValue"]); break;
                        case "PAYMENTTRAN": SystemProp.PaymentTran = Static.ToInt(DR["ItemValue"]); break;
                        case "ImplementYear": SystemProp.ImplementYear = Static.ToInt(DR["ItemValue"]); break;
                    }
                }
                #endregion
                #region  [ Data ]
                EServ.Shared.Static.WriteToLogFile("Init Branch ...");
                SystemProp.gBranch.Init(dbs);
                EServ.Shared.Static.WriteToLogFile("Init BAC Prod ...");
                SystemProp.gBACProd.Init(dbs);
                EServ.Shared.Static.WriteToLogFile("Init CON Prod ...");
                SystemProp.gCONProd.Init(dbs); 
                EServ.Shared.Static.WriteToLogFile("Init Currency ...");
                SystemProp.gCur.Init(dbs);
                EServ.Shared.Static.WriteToLogFile("Init Account codes ...");
                SystemProp.gAccountCode.Init(dbs);
                EServ.Shared.Static.WriteToLogFile("Init FA Type codes ...");
                SystemProp.gFAType.Init(dbs);
                EServ.Shared.Static.WriteToLogFile("Init Inventory Type codes ...");
                SystemProp.gInvType.Init(dbs); 
                EServ.Shared.Static.WriteToLogFile("Init Fin transactions ...");
                SystemProp.gFinTxn.Init(dbs);
                EServ.Shared.Static.WriteToLogFile("Init Chart Group ...");
                SystemProp.gChartGroup.Init(dbs);
                EServ.Shared.Static.WriteToLogFile("Init AutoNum ...");
                SystemProp.gAutoNum.Init(dbs);
                EServ.Shared.Static.WriteToLogFile("Init AutoNum Value...");
                SystemProp.gAutoNumValue.Init(dbs);
                #endregion
                #region[ Init Process States]
                res = IPos.DB.ProcessDB.DB213012(dbs, null);
                if (res.ResultNo != 0) return res;
                if (res.Data == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                if (res.Data.Tables[0] == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }

                DataTable GLDTProcess = res.Data.Tables[0];
                if (IPos.Core.SystemProp.GLProcessStates != null)
                    IPos.Core.SystemProp.GLProcessStates.Clear();
                else
                    IPos.Core.SystemProp.GLProcessStates = new System.Collections.Hashtable();
                int GLP = 0;
                foreach (DataRow rw in GLDTProcess.Rows)
                {
                    if (Static.ToInt(rw["STATUS"]) == 1)
                    {
                        IPos.Core.SystemProp.GLProcessStates.Add(Static.ToStr(rw["FUNCTIONNAME"]), GLP);
                        GLP = Static.ToInt(rw["PID"]);
                    }
                }

                res = IPos.DB.ProcessDB.DB213016(dbs, null);
                if (res.ResultNo != 0) return res;
                if (res.Data == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }
                if (res.Data.Tables[0] == null) { return new Result(1, "Процессийн жагсаалтыг авахад алдаа гарлаа"); }

                DataTable DTProcess = res.Data.Tables[0];
                if (IPos.Core.SystemProp.ProcessStates != null)
                    IPos.Core.SystemProp.ProcessStates.Clear();
                else
                    IPos.Core.SystemProp.ProcessStates = new System.Collections.Hashtable();
                int P = 0;
                foreach (DataRow rw in DTProcess.Rows)
                {
                    if (Static.ToInt(rw["STATUS"]) == 1)
                    {
                        IPos.Core.SystemProp.ProcessStates.Add(Static.ToStr(rw["FUNCTIONNAME"]), Static.ToInt(rw["PID"]));
                        P = Static.ToInt(rw["PID"]);
                    }
                }
                #endregion
                #region[ Init MailServer Conf ]
                res = IPos.DB.Main.DB216001(dbs);
                if (res.ResultNo != 0) return res;
                if (res.Data == null) { return new Result(1, " MailServer тохиргоо авахад алдаа гарлаа"); }
                if (res.Data.Tables[0] == null) { return new Result(1, " MailServer тохиргоо авахад алдаа гарлаа"); }
                DataRow MS = res.Data.Tables[0].Rows[0];
                IPos.Core.SystemProp.MServerName = Static.ToStr(MS["SERVERNAME"]);
                IPos.Core.SystemProp.MServerPort = Static.ToInt(MS["SERVERPORT"]);
                IPos.Core.SystemProp.MailUserName = Static.ToStr(MS["MAILUSERNAME"]);
                IPos.Core.SystemProp.MailUserPass = Static.ToStr(MS["MAILUSERPASS"]);
                IPos.Core.SystemProp.MailFromUser = Static.ToStr(MS["FROMUSER"]);
                #endregion
                SystemProp.SystemState = 0;
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
        static public Result InitDataParam(int functionno, DbConnections dbs)
        {
            Result res = new Result();
            try
            {
                /*asd
                #region  [ Data ]
                
                switch(functionno)
                {
                    case 0:
                        #region [ Init General List ]
                        EServ.Shared.Static.WriteToLogFile("Init GeneralParam ...");
                        
                        res = HPro.DB.Main.DB200001(dbs);

                        if (res.ResultNo != 0)
                            return res;

                        SystemProp.GenList = res.Data.Tables[0].Copy();
                        SystemProp.GenList.TableName = "GENERALPARAM";

                        foreach (DataRow DR in res.Data.Tables[0].Rows)
                        {
                            switch (Static.ToStr(DR["Key"]).ToUpper())
                            {
                                case "INSTCODE": SystemProp.InstCode = Static.ToStr(DR["ItemValue"]); break;
                                case "INSTNAME": SystemProp.InstName = Static.ToStr(DR["ItemValue"]); break;
                                case "INSTNAME2": SystemProp.InstName2 = Static.ToStr(DR["ItemValue"]); break;
                                case "BASECURRENCY": SystemProp.BaseCurrency = Static.ToStr(DR["ItemValue"]); break;
                                case "BACFORMAT": SystemProp.BACFormat = Static.ToStr(DR["ItemValue"]); break;
                                case "DEFAULTNUMBERFORMAT": SystemProp.DefaultNumberFormat = Static.ToStr(DR["ItemValue"]); break;
                                case "CONTRACTNUMBERFORMAT": SystemProp.ContractNumberFormat = Static.ToStr(DR["ItemValue"]); break;
                                case "TRACKGROUPID": SystemProp.TrackGroupID = Static.ToLong(DR["ItemValue"]); break;
                                case "POSGLNUMBER": SystemProp.PosGLNumber = Static.ToLong(DR["ItemValue"]); break;
                                case "YEARPROFITGLNUMBER": SystemProp.YearProfitGLNumber = Static.ToLong(DR["ItemValue"]); break;
                                case "YEARLOSSGLNUMBER": SystemProp.YearLossGLNumber = Static.ToLong(DR["ItemValue"]); break;
                                case "TXNDATE": SystemProp.TxnDate = Static.ToDate(DR["ItemValue"]); break;
                                case "SYSTEMBRANCHNO": SystemProp.SystemBranchNo = Static.ToInt(DR["ItemValue"]); break;
                                case "GLPROCESSSTATUS": SystemProp.GLProcessStatus  = Static.ToInt(DR["ItemValue"]); break;
                                case "POLICYMASK": SystemProp.PolicyMask = Static.ToStr(DR["ItemValue"]); break;
                                case "GLTXNDATE": SystemProp.GLTxnDate = Static.ToDate(DR["ItemValue"]); break;
                                case "GLPROCESSSTART": SystemProp.GLProcessStart = Static.ToInt(DR["ItemValue"]); break;
                                case "EODPROCESSSTART": SystemProp.EODProcessStart = Static.ToInt(DR["ItemValue"]); break;

                                case "PATHTERMINAL": SystemProp.PathTerminal = Static.ToStr(DR["ItemValue"]); break;
                                case "PATHDYNAMICRPT": SystemProp.PathDynamicRpt = Static.ToStr(DR["ItemValue"]); break;
                                case "PATHDYNAMICDOC": SystemProp.PathDynamicDoc = Static.ToStr(DR["ItemValue"]); break;
                                case "PATHSLIPS": SystemProp.PathSlips = Static.ToStr(DR["ItemValue"]); break;
                                case "UPDATEEXTENTION": SystemProp.UpdateExtention = Static.ToStr(DR["ItemValue"]); break;
                            }
                        }
                        #endregion
                        break;
                    case 1:
                EServ.Shared.Static.WriteToLogFile("Init Branch ...");
                SystemProp.gBranch.Init(dbs);
                break;
                    case 2:
                EServ.Shared.Static.WriteToLogFile("Init BAC Prod ...");
                SystemProp.gBACProd.Init(dbs);
                break;
                    case 3:
                EServ.Shared.Static.WriteToLogFile("Init CON Prod ...");
                SystemProp.gCONProd.Init(dbs);
                break;
                    case 4:
                EServ.Shared.Static.WriteToLogFile("Init Currency ...");
                SystemProp.gCur.Init(dbs);
                break;
                    case 5:
                EServ.Shared.Static.WriteToLogFile("Init Account codes ...");
                SystemProp.gAccountCode.Init(dbs);
                break;
                    case 6:
                EServ.Shared.Static.WriteToLogFile("Init FA Type codes ...");
                SystemProp.gFAType.Init(dbs);
                break;
                    case 7:
                EServ.Shared.Static.WriteToLogFile("Init Inventory Type codes ...");
                SystemProp.gInvType.Init(dbs);
                break;
                    case 8:
                EServ.Shared.Static.WriteToLogFile("Init Fin transactions ...");
                SystemProp.gFinTxn.Init(dbs);
                break;
                    case 9:
                EServ.Shared.Static.WriteToLogFile("Init Product ...");
                SystemProp.gDealProduct.Init(dbs);
                break;
                    case 10:
                EServ.Shared.Static.WriteToLogFile("Init Chart Group ...");
                SystemProp.gChartGroup.Init(dbs);
                break;
                    case 11:
                EServ.Shared.Static.WriteToLogFile("Init Formula ...");
                SystemProp.gFormula.Init(dbs);
                break;
                    case 12:
                EServ.Shared.Static.WriteToLogFile("Init Fund Product ...");
                SystemProp.gFundProduct.Init(dbs);
                break;
                    case 13:
                EServ.Shared.Static.WriteToLogFile("Init Fund ...");
                SystemProp.gFund.Init(dbs);
                break;
                    case 14:
                EServ.Shared.Static.WriteToLogFile("Init AutoNum ...");
                SystemProp.gAutoNum.Init(dbs);
                break;
                    case 15:
                EServ.Shared.Static.WriteToLogFile("Init AutoNum Value...");
                SystemProp.gAutoNumValue.Init(dbs);
                break;
                    default:
                res.ResultNo = 1;
                res.ResultDesc=functionno.ToString()+" дугаартай функц байхгүй байна .";
                break;
            }
                #endregion*/
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
    }
}
