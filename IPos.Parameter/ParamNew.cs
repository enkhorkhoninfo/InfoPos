using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using EServ.Interface;
using EServ.Shared;
using EServ.Data;

using IPos.List;
using IPos.DB;

namespace IPos.Parameter
{
    class ParamNew : IModule
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
                    #region [ General parameters]
                    #region [ GenParam - Ерөнхий тохиргоо (140146) ]
                    case 140146: //Ерөнхий параметр жагсаалт мэдээлэл авах
                        res = SelectGenParam(ci, ri, db, ref lg);
                        break;
                    case 140149: //Ерөнхий параметр жагсаалт засварлах
                        res = EditGenParam(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Branch - Салбар (140001) ]
                    case 140001: //Салбарын жагсаалт мэдээлэл авах
                        res = SelectBranch(ci, ri, db, ref lg);
                        break;
                    case 140003: //Салбар нэмэх
                        res = AddBranch(ci, ri, db, ref lg);
                        break;
                    case 140005: //Салбар устгах
                        res = DeleteBranch(ci, ri, db, ref lg);
                        break;
                    case 140004: //Салбар засварлах
                        res = EditBranch(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ Country - Улс (140008) ]
                    case 140006: //Улсын жагсаалт мэдээлэл авах
                        res = SelectCountry(ci, ri, db, ref lg);
                        break;
                    case 140008: //Улсын жагсаалт нэмэх
                        res = AddCountry(ci, ri, db, ref lg);
                        break;
                    case 140010: //Улсын жагсаалт устгах
                        res = DeleteCountry(ci, ri, db, ref lg);
                        break;
                    case 140009: //Улсын жагсаалт засварлах
                        res = EditCountry(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ Language - Хэл (140013) ]
                    case 140011: //Хэлний жагсаалт мэдээлэл авах
                        res = SelectLanguage(ci, ri, db, ref lg);
                        break;
                    case 140013: //Хэлний жагсаалт нэмэх
                        res = AddLanguage(ci, ri, db, ref lg);
                        break;
                    case 140015: //Хэлний жагсаалт устгах
                        res = DeleteLanguage(ci, ri, db, ref lg);
                        break;
                    case 140014: //Хэлний жагсаалт засварлах
                        res = EditLanguage(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ Currency - Валют (140018) ]
                    case 140016: //Валютын жагсаалт мэдээлэл авах
                        res = SelectCurrency(ci, ri, db, ref lg);
                        break;
                    case 140018: //Валютын жагсаалт нэмэх
                        res = AddCurrency(ci, ri, db, ref lg);
                        break;
                    case 140020: //Валютын жагсаалт устгах
                        res = DeleteCurrency(ci, ri, db, ref lg);
                        break;
                    case 140019: //Валютын жагсаалт засварлах
                        res = EditCurrency(ci, ri, db, ref lg);
                        break;
                    case 140311: //Валютын ханш оруулах
                        res = ImportFile(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ Bank - Банк (140023) ]
                    case 140021: //Банкны жагсаалт мэдээлэл авах
                        res = SelectBank(ci, ri, db, ref lg);
                        break;
                    case 140023: //Банкны жагсаалт нэмэх
                        res = AddBank(ci, ri, db, ref lg);
                        break;
                    case 140025: //Банкны жагсаалт устгах
                        res = DeleteBank(ci, ri, db, ref lg);
                        break;
                    case 140024: //Банкны жагсаалт засварлах
                        res = EditBank(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ BankBranch - Банкны салбар (140098) ]
                    case 140096: //Банкны салбар жагсаалт мэдээлэл авах
                        res = SelectBankBranch(ci, ri, db, ref lg);
                        break;
                    case 140098: //Банкны салбар жагсаалт нэмэх
                        res = AddBankBranch(ci, ri, db, ref lg);
                        break;
                    case 140100: //Банкны салбар жагсаалт устгах
                        res = DeleteBankBranch(ci, ri, db, ref lg);
                        break;
                    case 140099: //Банкны салбар жагсаалт засварлах
                        res = EditBankBranch(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ AutoNum - Автомат дугаарлалт (140033) ]
                    case 140031: //Автомат дансны бүртгэл жагсаалт мэдээлэл авах
                        res = SelectAutoNum(ci, ri, db, ref lg);
                        break;
                    case 140033: //Автомат дансны бүртгэл жагсаалт нэмэх
                        res = AddAutoNum(ci, ri, db, ref lg);
                        break;
                    case 140035: //Автомат дансны бүртгэл жагсаалт устгах
                        res = DeleteAutoNum(ci, ri, db, ref lg);
                        break;
                    case 140034: //Автомат дансны бүртгэл жагсаалт засварлах
                        res = EditAutoNum(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ Shortcut - Shortcut (140323) ]
                    case 140321:        //Shortcut бүртгэлийн жагсаалт авах
                        res = SelectShortCut(ci, ri, db, ref lg);
                        break;
                    case 140322:        //Shortcut бүртгэлийн мэдээлэл устгах
                        res = InfoShortCut(ci, ri, db, ref lg);
                        break;
                    case 140323:        //Shortcut бүртгэлийн мэдээлэл нэмэх
                        res = AddShortCut(ci, ri, db, ref lg);
                        break;
                    case 140324:        //Shortcut бүртгэлийн мэдээлэл засварлах
                        res = UpdateShortCut(ci, ri, db, ref lg);
                        break;
                    case 140325:        //Shortcut бүртгэлийн мэдээлэл устгах
                        res = DeleteShortCut(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ BankNote - МӨНГӨН ТЭМДЭГТИЙН ДЭВСГЭРТИЙН БҮРТГЭЛ(140192) ]
                    case 140191: //Мөнгөн тэмдэгтийн  төрлийн бүртгэл жагсаалт мэдээлэл авах
                        res = SelectBankNote(ci, ri, db, ref lg);
                        break;
                    case 140192: //Мөнгөн тэмдэгтийн  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = InfoBankNote(ci, ri, db, ref lg);
                        break;
                    case 140193: //Мөнгөн тэмдэгтийн  төрлийн бүртгэл шинээр нэмэх
                        res = AddBankNote(ci, ri, db, ref lg);
                        break;
                    case 140194: //Мөнгөн тэмдэгтийн  төрлийн бүртгэл засварлах
                        res = UpdateBankNote(ci, ri, db, ref lg);
                        break;
                    case 140195: //Мөнгөн тэмдэгтийн  төрлийн бүртгэл устгах
                        res = DeleteBankNote(ci, ri, db, ref lg);
                        break;
                    #endregion[]
                    #endregion
                    #region [ POS parameters]
                    #region[ TagSetup - Тагийн төрлийн бүртгэл (140172) ]
                    case 140171: //Тагийн төрлийн бүртгэл жагсаалт мэдээлэл авах
                        res = SelectTagType(ci, ri, db, ref lg);
                        break;
                    case 140172: //Тагийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = InfoTagType(ci, ri, db, ref lg);
                        break;
                    case 140173: //Тагийн төрлийн бүртгэл шинээр нэмэх
                        res = AddTagType(ci, ri, db, ref lg);
                        break;
                    case 140174:  //Тагийн төрлийн бүртгэл засварлах
                        res = UpdateTagType(ci, ri, db, ref lg);
                        break;
                    case 140175: //Тагийн төрлийн бүртгэл устгах
                        res = DeleteTagType(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ TagMain - Таг-ын бүртгэл (140358) ]
                    case 140356: ////Тагын жагсаалт мэдээлэл авах
                        res = SelectTag(ci, ri, db, ref lg);
                        break;
                    case 140357: //Тагын дэлгэрэнгүй мэдээлэл авах
                        res = InfoTag(ci, ri, db, ref lg);
                        break;
                    case 140358: //Таг нэмэх
                        res = AddTag(ci, ri, db, ref lg);
                        break;
                    case 140359: //Таг засварлах
                        res = UpdateTag(ci, ri, db, ref lg);
                        break;
                    case 140360: //Таг устгах
                        res = DeleteTag(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ PaymentType - Төлбөрийн төрлийн код (140202) ]
                    case 140201: //Төлбөрийн төрлийн бүртгэл жагсаалт мэдээлэл авах
                        res = SelectPaymentType(ci, ri, db, ref lg);
                        break;
                    case 140202: //Төлбөрийн төрлийн  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = InfoPaymentType(ci, ri, db, ref lg);
                        break;
                    case 140203: //Төлбөрийн төрлийн бүртгэл шинээр нэмэх
                        res = AddPaymentType(ci, ri, db, ref lg);
                        break;
                    case 140204: //Төлбөрийн төрлийн  төрлийн бүртгэл засварлах
                        res = UpdatePaymentType(ci, ri, db, ref lg);
                        break;
                    case 140205: //Төлбөрийн төрлийн  төрлийн бүртгэл устгах
                        res = DeletePaymentType(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[ ContractType - Гэрээний төрлийн бүртгэл (140233) ]
                    case 140231: // Гэрээний төрөлийн жагсаалт
                        res = SelectContractType(ci, ri, db, ref lg);
                        break;
                    case 140232: //Төлбөрийн төрлийн  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = InfoContractType(ci, ri, db, ref lg);
                        break;
                    case 140233: //Төлбөрийн төрлийн бүртгэл шинээр нэмэх
                        res = AddContractType(ci, ri, db, ref lg);
                        break;
                    case 140234: //Төлбөрийн төрлийн  төрлийн бүртгэл засварлах
                        res = UpdateContractType(ci, ri, db, ref lg);
                        break;
                    case 140235: //Төлбөрийн төрлийн  төрлийн бүртгэл устгах
                        res = DeleteContractType(ci, ri, db, ref lg);
                        break;
                    #endregion[]
                    #region[ PosTerminal - POS ын терминалийн бүртгэл (140078) ]
                    case 140076: //POS ын бүртгэл жагсаалт мэдээлэл авах
                        res = SelectPosTerminal(ci, ri, db, ref lg);
                        break;
                    case 140077: //POS ын бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = InfoPosTerminal(ci, ri, db, ref lg);
                        break;
                    case 140078: //POS ын бүртгэл шинээр нэмэх
                        res = AddPosTerminal(ci, ri, db, ref lg);
                        break;
                    case 140079: //POS ын бүртгэл засварлах
                        res = UpdatePosTerminal(ci, ri, db, ref lg);
                        break;
                    case 140080: //POS ын бүртгэл устгах
                        res = DeletePosTerminal(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ PosTerminalPayment - POS дээрх төлбөрийн хэрэгсэлийн бүртгэл (111114) ]
                    case 111113: //POS төлбөрийн хэрэгсэл жагсаалт авах
                        res = SelectPosPayment(ci, ri, db, ref lg);
                        break;
                    case 111114: //POS төлбөрийн хэрэгсэл дэлгэрэнгүй мэдээлэл авах
                        res = InfoPosPayment(ci, ri, db, ref lg);
                        break;
                    case 111115: //POS төлбөрийн хэрэгсэл шинээр нэмэх
                        res = AddPosPayment(ci, ri, db, ref lg);
                        break;
                    case 111116: //POS төлбөрийн хэрэгсэл засварлах
                        res = UpdatePosPayment(ci, ri, db, ref lg);
                        break;
                    case 111117: //POS төлбөрийн хэрэгсэл устгах
                        res = DeletePosPayment(ci, ri, db, ref lg);
                        break;

                    #endregion
                    #endregion
                    #region [ Customer parameters ]
                    #region[ CustomerType - Харилцагчийн төрөл (140036) ]
                    case 140036: //Харилцагчийн төрөл мэдээлэл жагсаалт авах
                        res = SelectCustomerType(ci, ri, db, ref lg);
                        break;
                    case 140038: //Харилцагчийн төрөл жагсаалт нэмэх
                        res = AddCustomerType(ci, ri, db, ref lg);
                        break;
                    case 140040: //Харилцагчийн төрөл жагсаалт устгах
                        res = DeleteCustomerType(ci, ri, db, ref lg);
                        break;
                    case 140039: //Харилцагчийн төрөл жагсаалт засварлах
                        res = EditCustomerType(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #endregion
                    default:
                        res.ResultNo = 9110009;
                        res.ResultDesc = "Функц тодорхойлогдоогүй байна";
                        break;
                }
                return res;
            }
            #region[catch]
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
                ISM.Lib.Static.WriteToLogFile("IPos.Parameter", "\r\n<<Start:" + Static.ToStr(first) + ">>\r\n UserNo:" + Static.ToStr(ri.UserNo) + "\r\n Description:" + Static.ToStr(lg.item.Desc) + "\r\n ResultNo:" + Static.ToStr(res.ResultNo) + "\r\n ResultDescription:" + ResultDesk + "\r\n<<End " + Static.ToStr(second) + ">>");
            }
            #endregion[]
        }
        #region [ General parameters ]
        #region [ GenParam - Ерөнхий тохиргоо (140146) ]
        public Result SelectGenParam(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = DBIO.DB202146(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа " + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Ерөнхий Параметр жагсаалт авах";
            }
        }
        public Result EditGenParam(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value;
                int index;
                DataTable DT = (DataTable)ri.ReceivedParam[0];
                foreach (DataRow row in DT.Rows)
                {
                    value = null;
                    value = new object[DT.Columns.Count];
                    index = 0;
                    foreach (DataColumn col in DT.Columns)
                    {
                        value[index] = row[col];
                        index++;
                    }
                    res = DBIO.DB202149(db, value);
                }

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа " + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Ерөнхий Параметр засварлах";
            }
        }
        #endregion
        #region [ Branch - Салбар (140001) ]
        public Result SelectBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202001(db, 0, 0, null);
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
                lg.item.Desc = "Салбарын жагсаалт авах";
            }
        }
        public Result AddBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202003(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Салбарын жагсаалт нэмэх";
                lg.item.Desc = "Салбарын жагсаалт нэмэх";
            }
        }
        public Result DeleteBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202005(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Салбарын жагсаалт устгах";
            }
        }
        public Result EditBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202004(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Салбарын жагсаалт засварлах";
            }
        }
        #endregion
        #region[ Country - Улс (140008) ]
        public Result SelectCountry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202006(db, 0, 0, null);
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
                lg.item.Desc = "Улсын жагсаалт авах";
            }
        }
        public Result AddCountry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202008(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Улсын жагсаалт нэмэх";
            }
        }
        public Result DeleteCountry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202010(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Улсын жагсаалт устгах";
            }
        }
        public Result EditCountry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202009(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Улсын жагсаалт засварлах";
            }
        }
        #endregion
        #region[ Language - Хэл (140013) ]
        public Result SelectLanguage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202011(db, 0, 0, null);
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
                lg.item.Desc = "Хэлний жагсаалт авах";
            }
        }
        public Result AddLanguage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202013(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Хэлний жагсаалт нэмэх";
            }
        }
        public Result DeleteLanguage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202015(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Хэлний жагсаалт устгах";
            }
        }
        public Result EditLanguage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202014(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Хэлний жагсаалт засварлах";
            }
        }
        #endregion
        #region[ Currency - Валют (140018) ]
        public Result SelectCurrency(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202016(db, 0, 0, null);
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
                lg.item.Desc = "Валютын жагсаалт авах";
            }
        }
        public Result AddCurrency(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202018(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                return res;
            }
            finally
            {
                lg.item.Desc = "Валютын жагсаалт нэмэх";
            }
        }
        public Result DeleteCurrency(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202020(db, (ri.ReceivedParam[0]).ToString());
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
                lg.item.Desc = "Валютын жагсаалт устгах";
            }
        }
        public Result EditCurrency(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202019(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Валютын жагсаалт засварлах";
            }
        }
        public Result ImportFile(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202199(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Валютын ханшийн түүх оруулах";
            }
        }
        #endregion
        #region[ Bank - Банк (140023) ]
        public Result SelectBank(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202021(db, 0, 0, null);
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
                lg.item.Desc = "Банкны жагсаалт авах";
            }
        }
        public Result AddBank(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202023(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Банкны жагсаалт нэмэх";
            }
        }
        public Result DeleteBank(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202025(db, Static.ToLong(ri.ReceivedParam[0]));

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
                lg.item.Desc = "Банкны жагсаалт устгах";
            }
        }
        public Result EditBank(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202024(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Банкны жагсаалт засварлах";
            }
        }
        #endregion
        #region[ BankBranch - Банкны салбар (140098) ]
        public Result SelectBankBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202096(db, 0, 0, null);
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
                lg.item.Desc = "Банкны салбар жагсаалт авах";
            }
        }
        public Result AddBankBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202098(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Банкны салбар жагсаалт нэмэх";
            }
        }
        public Result DeleteBankBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202100(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
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
                lg.item.Desc = "Банкны салбар жагсаалт устгах";
            }
        }
        public Result EditBankBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] OldValueEdit = (object[])ri.ReceivedParam[1];
                object[] Value = new object[6];
                for (int i = 0; i < 5; i++)
                    Value[i] = ((object[])ri.ReceivedParam[0])[i];
                Value[5] = OldValueEdit[1];
                res = DBIO.DB202099(db, Value);
                if (res.ResultNo == 0)
                {

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
                lg.item.Desc = "Банкны салбар засварлах";
            }
        }
        #endregion
        #region[ AutoNum - Автомат дугаарлалт (140033) ]
        public Result SelectAutoNum(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202031(db);
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
                lg.item.Desc = "Автомат дансны бүртгэл жагсаалт авах";
            }
        }
        public Result AddAutoNum(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202033(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Автомат дансны бүртгэл жагсаалт нэмэх";
            }
        }
        public Result DeleteAutoNum(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202035(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Автомат дансны бүртгэл жагсаалт устгах";
            }
        }
        public Result EditAutoNum(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202034(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Автомат дансны бүртгэл жагсаалт засварлах";
            }
        }
        #endregion
        #region[ Shortcut - Shortcut (140323) ]
        public Result SelectShortCut(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202321(db);
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
                lg.item.Desc = "Дамжлагын бүртгэл жагсаалт авах";
            }
        }
        public Result InfoShortCut(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pID = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB202322(db, pID);
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
                lg.item.Desc = "Shortcut дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result AddShortCut(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202323(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "ShortCut бүртгэл нэмэх";
            }
        }
        public Result DeleteShortCut(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pID = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB202325(db, pID);
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
                lg.item.Desc = "ShortCut бүртгэл устгах";
            }
        }
        public Result UpdateShortCut(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202324(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Shortcut бүртгэл жагсаалт засварлах";
            }
        }
        #endregion
        #region [ BankNote - МӨНГӨН ТЭМДЭГТИЙН ДЭВСГЭРТИЙН БҮРТГЭЛ(140192) ]
        public Result SelectBankNote(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202191(db);
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
                lg.item.Desc = "Мөнгөн тэмдэгтийн  төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    
        public Result InfoBankNote(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pCurrency = Static.ToStr(ri.ReceivedParam[0]);
                string pBankNote = Static.ToStr(ri.ReceivedParam[1]);
                res = DBIO.DB202192(db, pCurrency, pBankNote);
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
                    lg.item.Desc = "Мөнгөн тэмдэгтийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pCurrency", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    
        public Result AddBankNote(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);

                if (Static.ToStr(obj[0]).Trim() == "")
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Валютыг оруулах шаардлагатай";
                    return res;
                }
                if (Static.ToStr(obj[1]).Trim() == "")
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Дэвсгэртийг оруулах шаардлагатай";
                    return res;
                }

                res = DBIO.DB202193(db, obj);
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
                lg.item.Desc = "Мөнгөн тэмдэгтийн төрлийн бүртгэл шинээр нэмэх";
            }
        }    
        public Result UpdateBankNote(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);

                res = DBIO.DB202194(db, obj);
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
                lg.item.Desc = "Мөнгөн тэмдэгтийн төрлийн бүртгэл засварлах";
            }
        }    
        public Result DeleteBankNote(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pCurrency = Static.ToStr(ri.ReceivedParam[0]);
                string pNote = Static.ToStr(ri.ReceivedParam[1]);
                res = DBIO.DB202195(db, pCurrency, pNote);
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
                lg.item.Desc = "Мөнгөн тэмдэгтийн төрлийн бүртгэл устгах";
            }
        }    
        #endregion 
        #endregion
        #region [ POS parameters]
        #region[ TagSetup - Тагийн төрлийн бүртгэл (140172) ]
        public Result SelectTagType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202171(db);
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
                lg.item.Desc = "Тагийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    
        public Result InfoTagType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pTagType = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB202172(db, pTagType);
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
                    lg.item.Desc = "Тагийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pTagType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    
        public Result AddTagType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);
                obj[4] = Static.ToInt(value[4]);

                res = DBIO.DB202173(db, obj);
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
                lg.item.Desc = "Тагийн төрлийн бүртгэл шинээр нэмэх";
            }
        }    
        public Result UpdateTagType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);
                obj[4] = Static.ToInt(value[4]);

                res = DBIO.DB202174(db, obj);
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
                lg.item.Desc = "Тагийн төрлийн бүртгэл засварлах";
            }
        }   
        public Result DeleteTagType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pTagType = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB202175(db, pTagType);
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
                lg.item.Desc = "Тагийн төрлийн бүртгэл устгах";
            }
        }    
        #endregion[]  
        #region[ TagMain - Таг-ын бүртгэл (140358) ]
        public Result SelectTag(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202331(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.Source + ex.StackTrace;
                return res;
            }
        }
        public Result InfoTag(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202332(db, ri.ReceivedParam[0].ToString());
                return res;
            }
            catch (Exception ex)
            {
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.StackTrace + ex.Source;
                res.ResultNo = 911002;
                return res;
            }
        }
        public Result AddTag(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202333(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.Source + ex.StackTrace;
                return res;
            }
        }
        public Result UpdateTag(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202334(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.Source + ex.StackTrace;
                return res;
            }
        }
        public Result DeleteTag(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202335(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.Source + ex.StackTrace;
                return res;
            }
        }
        #endregion
        #region[ PaymentType - Төлбөрийн төрлийн код (140202) ]
        public Result SelectPaymentType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202201(db);
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
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }
        public Result InfoPaymentType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pTyId = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB202202(db, pTyId);
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
                    lg.item.Desc = "Төлбөрийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pTyId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result AddPaymentType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[8];
                int pPaymentFlag = 9;
                string pTypeID = "";

                obj[0] = Static.ToStr(value[0]);
                pTypeID = Static.ToStr(obj[0]);

                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);
                //PaymentFlag
                //0-Бэлэн төлбөр. Validation хийж, зөвхөн 1 бүртгэл бүртгэсэн эсэхийг шалгах.
                //1-Картын төлбөр. Validation хийж, зөвхөн 1 бүртгэл бүртгэсэн эсэхийг шалгах.
                //9-Бусад төлбөр (гэрээ, т/д, ваучир гм)
                obj[5] = Static.ToInt(value[5]);
                pPaymentFlag = Static.ToInt(obj[5]);

                switch (pPaymentFlag)
                {

                    case 0:
                        {
                            res = DBIO.DB202196(db, pTypeID, pPaymentFlag);
                            if (res.AffectedRows != 0)
                            {
                                res.ResultNo = 1;
                                res.ResultDesc = "Бэлэн төлбөр гэсэн сонголтоор бүртгэл хийсэн байна. Төрлийн дугаар: " + res.Data.Tables[0].Rows[0]["TypeId"];
                                return res;
                            }
                        } break;
                    case 1:
                        {
                            res = DBIO.DB202196(db, pTypeID, pPaymentFlag);
                            if (res.AffectedRows != 0)
                            {
                                res.ResultNo = 1;
                                res.ResultDesc = "Картын төлбөр гэсэн сонголтоор бүртгэл хийсэн байна. Төрлийн дугаар: " + res.Data.Tables[0].Rows[0]["TypeId"];
                                return res;
                            } break;
                        }
                }

                obj[6] = Static.ToStr(value[6]);
                obj[7] = Static.ToStr(value[7]);

                res = DBIO.DB202203(db, obj);
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
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл шинээр нэмэх";
            }
        }
        public Result UpdatePaymentType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[8];
                int pPaymentFlag = 9;
                string pTypeID = "";

                obj[0] = Static.ToStr(value[0]);
                pTypeID = Static.ToStr(obj[0]);

                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);
                //PaymentFlag
                //0-Бэлэн төлбөр. Validation хийж, зөвхөн 1 бүртгэл бүртгэсэн эсэхийг шалгах.
                //1-Картын төлбөр. Validation хийж, зөвхөн 1 бүртгэл бүртгэсэн эсэхийг шалгах.
                //9-Бусад төлбөр (гэрээ, т/д, ваучир гм)
                obj[5] = Static.ToInt(value[5]);
                pPaymentFlag = Static.ToInt(obj[5]);

                switch (pPaymentFlag)
                {

                    case 0:
                        {
                            res = DBIO.DB202196(db, pTypeID, pPaymentFlag);
                            if (res.AffectedRows != 0)
                            {
                                res.ResultNo = 1;
                                res.ResultDesc = "Бэлэн төлбөр гэсэн сонголтоор бүртгэл хийсэн байна. Төрлийн дугаар: " + res.Data.Tables[0].Rows[0]["TypeId"];
                                return res;
                            }
                        } break;
                    case 1:
                        {
                            res = DBIO.DB202196(db, pTypeID, pPaymentFlag);
                            if (res.AffectedRows != 0)
                            {
                                res.ResultNo = 1;
                                res.ResultDesc = "Картын төлбөр гэсэн сонголтоор бүртгэл хийсэн байна. Төрлийн дугаар: " + res.Data.Tables[0].Rows[0]["TypeId"];
                                return res;
                            } break;
                        }
                }

                obj[6] = Static.ToStr(value[6]);
                obj[7] = Static.ToStr(value[7]);

                res = DBIO.DB202204(db, obj);
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
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл засварлах";
            }
        } 
        public Result DeletePaymentType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pTypeId = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB202205(db, pTypeId);
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
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл устгах";
            }
        }
        #endregion[]  
        #region[ ContractType - Гэрээний төрлийн бүртгэл (140233) ]
        public Result SelectContractType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202231(db);
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
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        } 
        public Result InfoContractType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pContractType = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202232(db, pContractType);
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
                    lg.item.Desc = "Төлбөрийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pTyId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    
        public Result AddContractType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);
                obj[4] = value[4];

                res = IPos.DB.Main.DB202233(db, obj);
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
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл шинээр нэмэх";
            }
        }    
        public Result UpdateContractType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);
                obj[4] = value[4];

                res = IPos.DB.Main.DB202234(db, obj);
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
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл засварлах";
            }
        }    
        public Result DeleteContractType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pContractType = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202235(db, pContractType);
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
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл устгах";
            }
        }    
        #endregion[] 
        #region[ PosTerminal - POS ын терминалийн бүртгэл (140078) ]
        public Result SelectPosTerminal(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202076(db);
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
                lg.item.Desc = "";
            }
        }    
        public Result InfoPosTerminal(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPosNo = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB202077(db, pPosNo);
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
        public Result AddPosTerminal(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[6];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToStr(value[4]);
                obj[5] = Static.ToStr(value[5]);

                res = DBIO.DB202078(db, obj);
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
                lg.item.Desc = "бүртгэл шинээр нэмэх";
            }
        }    
        public Result UpdatePosTerminal(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[6];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToStr(value[4]);
                obj[5] = Static.ToStr(value[5]);

                res = DBIO.DB202079(db, obj);
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
                lg.item.Desc = "бүртгэл засварлах";
            }
        }
        public Result DeletePosTerminal(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPOSnO = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB202080(db, pPOSnO);
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
                lg.item.Desc = "бүртгэл устгах";
            }
        }   
        #endregion[] 
        #region [ POS дээрх төлбөрийн хэрэгсэлийн бүртгэл ]
        public Result SelectPosPayment(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB203019(db);
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
                lg.item.Desc = "POS төлбөрийн хэрэгсэл жагсаалт авах";
            }
        }
        public Result InfoPosPayment(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPosNo = Static.ToStr(ri.ReceivedParam[0]);
                res = DBIO.DB203020(db, pPosNo);
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
                lg.item.Desc = "POS төлбөрийн хэрэгсэл дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result AddPosPayment(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[2];
                obj = (object[])ri.ReceivedParam[0];

                res = DBIO.DB203021(db, obj);
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
                lg.item.Desc = "POS төлбөрийн хэрэгсэл шинээр нэмэх ";
            }
        }
        public Result UpdatePosPayment(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPOSNo = Static.ToStr(ri.ReceivedParam[0]);
                string pOldTypeID = Static.ToStr(ri.ReceivedParam[1]);
                string pNewTypeID = Static.ToStr(ri.ReceivedParam[2]);
                res = DBIO.DB203022(db, pPOSNo, pOldTypeID, pNewTypeID);
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
                lg.item.Desc = "POS төлбөрийн хэрэгсэл засварлах";
            }
        }
        public Result DeletePosPayment(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string pPOSNo;
            string pTypeID = "";
            object[] obj = new object[2];
            try
            {
                pPOSNo = Static.ToStr(ri.ReceivedParam[0]);
                pTypeID = Static.ToStr(ri.ReceivedParam[1]);
                res = DBIO.DB203023(db, pPOSNo, pTypeID);
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
                lg.item.Desc = "POS төлбөрийн хэрэгсэл устгах";
            }
        }
        #endregion
        #endregion
        #region [ Customer parameters ]
        #region[ CustomerType - Харилцагчийн төрөл (140036) ]
        public Result SelectCustomerType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202036(db, 0, 0, null);
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
                lg.item.Desc = "Харилцагчийн Төрлийн жагсаалт авах";
            }
        }
        public Result AddCustomerType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202038(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Харилцагчийн Төрлийн жагсаалт нэмэх";
            }
        }
        public Result DeleteCustomerType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202040(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Харилцагчийн Төрлийн жагсаалт устгах";
            }
        }
        public Result EditCustomerType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB202039(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Харилцагчийн Төрлийн жагсаалт засварлах";
            }
        }
        #endregion
        #endregion
    }
}