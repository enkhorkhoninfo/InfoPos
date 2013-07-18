using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;

namespace IPos.Customer
{
    public class Customer : IModule
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
                    #region [ Customer ]
                    case 120000: 	            //Харилцагчийн жагсаалт авах
                        res = Txn120000(ci, ri, db, ref lg);
                        break;
                    case 120001: 	            //Харилцагчийн дэлгэрэнгүй
                        res = Txn120001(ci, ri, db, ref lg);
                        break;
                    case 120002: 	            //Харилцагчийн нэмэх
                        res = Txn120002(ci, ri, db, ref lg);
                        break;
                    case 120003: 	            //Харилцагчийн засах
                        res = Txn120003(ci, ri, db, ref lg);
                        break;
                    case 120004: 	            //Харилцагчийн устгах
                        res = Txn120004(ci, ri, db, ref lg);
                        break;
                    case 120066: 	            //Харилцагчийн жагсаалт регистрээр
                        res = Txn120066(ci, ri, db, ref lg);
                        break;
                    case 130010: 	            //Харилцагчийн жагсаалт авах дуудлага дээр.
                        res = Txn130010(ci, ri, db, ref lg);
                        break;

                    case 310125: 	            //Харилцагчийн жагсаалт авах
                        res = Txn310125(ci, ri, db, ref lg);
                        break;
                    case 310126: 	            //Харилцагчийн дэлгэрэнгүй
                        res = Txn310126(ci, ri, db, ref lg);
                        break;
                    case 310127: 	            //Харилцагчийн нэмэх
                        res = Txn310127(ci, ri, db, ref lg);
                        break;
                    case 310128: 	            //Харилцагчийн засах
                        res = Txn310128(ci, ri, db, ref lg);
                        break;
                    case 310129: 	            //Харилцагчийн устгах
                        res = Txn310129(ci, ri, db, ref lg);
                        break;
                    case 310140: 	            //Холбоо барьсан харилцагчийг үндсэн харилцагчруу нэмэх
                        res = Txn310140(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Customer address ]
                    case 120006:        	    //Харилцагчийн хаягийн жагсаалт авах
                        res = Txn120006(ci, ri, db, ref lg);
                        break;
                    case 120007:	            //Харилцагчийн хаягийн дэлгэрэнгүй мэдээлэл авах
                        res = Txn120007(ci, ri, db, ref lg);
                        break;
                    case 120008:	            //Харилцагчийн хаяг шинээр нэмэх
                        res = Txn120008(ci, ri, db, ref lg);
                        break;
                    case 120009:	            //Харилцагчийн хаяг засварлах
                        res = Txn120009(ci, ri, db, ref lg);
                        break;
                    case 120010:	            //Харилцагчийн хаяг устгах
                        res = Txn120010(ci, ri, db, ref lg);
                        break;

                    case 310135:        	    //Харилцагчийн хаягийн жагсаалт авах
                        res = Txn310135(ci, ri, db, ref lg);
                        break;
                    case 310136:	            //Харилцагчийн хаягийн дэлгэрэнгүй мэдээлэл авах
                        res = Txn310136(ci, ri, db, ref lg);
                        break;
                    case 310137:	            //Харилцагчийн хаяг шинээр нэмэх
                        res = Txn310137(ci, ri, db, ref lg);
                        break;
                    case 310138:	            //Харилцагчийн хаяг засварлах
                        res = Txn310138(ci, ri, db, ref lg);
                        break;
                    case 310139:	            //Харилцагчийн хаяг устгах
                        res = Txn310139(ci, ri, db, ref lg);
                        break;
                     #endregion
                    #region [ Харилцагчийн зураг ]
                    case 120011:	                //Харилцагчийн зургийн жагсаалт авах
                        res = Txn120011(ci, ri, db, ref lg);
                        break;
                    case 120012:	                //Харилцагчийн зургийн дэлгэрэнгүй мэдээлэл авах
                        res = Txn120012(ci, ri, db, ref lg);
                        break;
                    case 120013:	                //Харилцагчийн зураг шинээр нэмэх
                        res = Txn120013(ci, ri, db, ref lg);
                        break;
                    case 120014:	                //Харилцагчийн зураг засварлах
                        res = Txn120014(ci, ri, db, ref lg);
                        break;
                    case 120015:	                //Харилцагчийн зураг устгах
                        res = Txn120015(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Харилцагчийн зургийн түүх ]
                    case 120016:	                //Харилцагчийн зургийн түүхийн жагсаалт авах
                        res = Txn120016(ci, ri, db, ref lg);
                        break;
                    case 120017:	                //Харилцагчийн зургийн түүхийн дэлгэрэнгүй мэдээлэл авах
                        res = Txn120017(ci, ri, db, ref lg);
                        break;
                    case 120018:	                //Харилцагчийн зургийн түүх шинээр нэмэх
                        res = Txn120018(ci, ri, db, ref lg);
                        break;
                    case 120019:	                //Харилцагчийн зургийн түүх засварлах
                        res = Txn120019(ci, ri, db, ref lg);
                        break;
                    case 120020:	                //Харилцагчийн зургийн түүх устгах
                        res = Txn120020(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Харилцагчийн хамаатан садан ]
                    case 120021:	                //Харилцагчийн хамаатан садны жагсаалт авах
                        res = Txn120021(ci, ri, db, ref lg);
                        break;
                    case 120022:	                //Харилцагчийн хамаатан садны дэлгэрэнгүй мэдээлэл авах
                        res = Txn120022(ci, ri, db, ref lg);
                        break;
                    case 120023:	                //Харилцагчийн хамаатан садан шинээр нэмэх
                        res = Txn120023(ci, ri, db, ref lg);
                        break;
                    case 120024:	                //Харилцагчийн хамаатан садан засварлах
                        res = Txn120024(ci, ri, db, ref lg);
                        break;
                    case 120025:	                //Харилцагчийн хамаатан садан устгах
                        res = Txn120025(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Харилцагчийн байгууллагын захиралын мэдээлэл ]
                    case 120073:	                //Харилцагчийн байгууллагын захиралын мэдээлэл жагсаалт авах
                        res = DB205073(ci, ri, db, ref lg);
                        break;
                    case 120074:	                //Харилцагчийн байгууллагын захиралын мэдээлэл дэлгэрэнгүй мэдээлэл авах
                        res = DB205074(ci, ri, db, ref lg);
                        break;
                    case 120075:	                //Харилцагчийн байгууллагын захиралын мэдээлэл шинээр нэмэх
                        res = DB205075(ci, ri, db, ref lg);
                        break;
                    case 120076:	                //Харилцагчийн байгууллагын захиралын мэдээлэл засварлах
                        res = DB205076(ci, ri, db, ref lg);
                        break;
                    case 120077:	                //Харилцагчийн байгууллагын захиралын мэдээлэл устгах
                        res = DB205077(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Холбоо барьсан тэмдэглэл ]
                    case 120036:	                //	Харилцагчийн холбоо барьсан мэдээлэл жагсаалт авах
                        res = Txn120036(ci, ri, db, ref lg);
                        break;
                    case 120037:	                //	Харилцагчийн холбоо барьсан мэдээлэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn120037(ci, ri, db, ref lg);
                        break;
                    case 120038:	                //	Харилцагчийн холбоо барьсан мэдээлэл шинээр нэмэх
                        res = Txn120038(ci, ri, db, ref lg);
                        break;
                    case 120039:	                //	Харилцагчийн холбоо барьсан мэдээлэл засварлах
                        res = Txn120039(ci, ri, db, ref lg);
                        break;
                    case 120040:	                //	Харилцагчийн холбоо барьсан мэдээлэл устгах
                        res = Txn120040(ci, ri, db, ref lg);
                        break;

                    #endregion
                    #region [ Дансны мэдээлэл ]
                    case 120041:	                //	Харилцагчийн дансны мэдээлэл жагсаалт авах
                        res = Txn120041(ci, ri, db, ref lg);
                        break;
                    case 120042:	                //	Харилцагчийн дансны мэдээлэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn120042(ci, ri, db, ref lg);
                        break;
                    case 120043:	                //	Харилцагчийн дансны мэдээлэл шинээр нэмэх
                        res = Txn120043(ci, ri, db, ref lg);
                        break;
                    case 120044:	                //	Харилцагчийн дансны мэдээлэл засварлах
                        res = Txn120044(ci, ri, db, ref lg);
                        break;
                    case 120045:	                //	Харилцагчийн дансны мэдээлэл устгах
                        res = Txn120045(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Дотоод дансны мэдээлэл ]
                    case 120026:	                //	Харилцагчийн дотоот дансны мэдээлэл жагсаалт авах
                        res = Txn120026(ci, ri, db, ref lg);
                        break;
                    case 120027:	                //	Харилцагчийн дотоот дансны мэдээлэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn120027(ci, ri, db, ref lg);
                        break;
                    case 120028:	                //	Харилцагчийн дотоот дансны мэдээлэл шинээр нэмэх
                        res = Txn120028(ci, ri, db, ref lg);
                        break;
                    case 120029:	                //	Харилцагчийн дотоот дансны мэдээлэл засварлах
                        res = Txn120029(ci, ri, db, ref lg);
                        break;
                    case 120030:	                //	Харилцагчийн дотоот дансны мэдээлэл устгах
                        res = Txn120030(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Хавсралт мэдээлллүүд ]
                    case 120046:	                //	Харилцагчийн хавсралтууд жагсаалт авах
                        res = Txn120046(ci, ri, db, ref lg);
                        break;
                    case 120049:	                //	Харилцагчийн хавсралтууд засварлах
                        res = Txn120049(ci, ri, db, ref lg);
                        break;
                    case 120050:	                //	Харилцагчийн хавсралтууд устгах
                        res = Txn120050(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Товч дүгнэлт ]
                    case 120051:	                //	Харилцагчийн товч дүгнэлт жагсаалт авах
                        res = Txn120051(ci, ri, db, ref lg);
                        break;
                    case 120052:	                //	Харилцагчийн товч дүгнэлт дэлгэрэнгүй мэдээлэл авах
                        res = Txn120052(ci, ri, db, ref lg);
                        break;
                    case 120053:	                //	Харилцагчийн товч дүгнэлт шинээр нэмэх
                        res = Txn120053(ci, ri, db, ref lg);
                        break;
                    case 120054:	                //	Харилцагчийн товч дүгнэлт засварлах
                        res = Txn120054(ci, ri, db, ref lg);
                        break;
                    case 120055:	                //	Харилцагчийн товч дүгнэлт устгах
                        res = Txn120055(ci, ri, db, ref lg);
                        break;

                    case 310130:	                //	Харилцагчийн товч дүгнэлт жагсаалт авах
                        res = Txn310130(ci, ri, db, ref lg);
                        break;
                    case 310131:	                //	Харилцагчийн товч дүгнэлт дэлгэрэнгүй мэдээлэл авах
                        res = Txn310131(ci, ri, db, ref lg);
                        break;
                    case 310132:	                //	Харилцагчийн товч дүгнэлт шинээр нэмэх
                        res = Txn310132(ci, ri, db, ref lg);
                        break;
                    case 310133:	                //	Харилцагчийн товч дүгнэлт засварлах
                        res = Txn310133(ci, ri, db, ref lg);
                        break;
                    case 310134:	                //	Харилцагчийн товч дүгнэлт устгах
                        res = Txn310134(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region [ Харилцагчийн нэмэлт мэдээлэл ]
                    case 120056:	                //	Харилцагчийн нэмэлт мэдээлэл жагсаалт авах
                        res = Txn120056(ci, ri, db, ref lg);
                        break;
                    case 120057:	                //	Харилцагчийн нэмэлт мэдээлэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn120057(ci, ri, db, ref lg);
                        break;
                    case 120058:	                //	Харилцагчийн нэмэлт мэдээлэл шинээр нэмэх
                        res = Txn120058(ci, ri, db, ref lg);
                        break;
                    case 120059:	                //	Харилцагчийн нэмэлт мэдээлэл засварлах
                        res = Txn120059(ci, ri, db, ref lg);
                        break;
                    case 120060:	                //	Харилцагчийн нэмэлт мэдээлэл устгах
                        res = Txn120060(ci, ri, db, ref lg);
                        break;
                    #endregion
                    case 120072:	                //	Харилцагч дээрх дансууд
                        res = Txn120072(ci, ri, db, ref lg);
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
                ISM.Lib.Static.WriteToLogFile("Hpro.Customer", "\r\n<<Start:" + Static.ToStr(first) + ">>\r\n UserNo:" + Static.ToStr(ri.UserNo) + "\r\n Description:" + Static.ToStr(lg.item.Desc) + "\r\n ResultNo:" + Static.ToStr(res.ResultNo) + "\r\n ResultDescription:" + ResultDesk + "\r\n<<End " + Static.ToStr(second) + ">>");
            }
        }
        #region [ Customer general information functions ]
        public Result Txn120000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB205001(db,ri.PageIndex,ri.PageRows, null);
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
                lg.item.Desc = "Харилцагчийн жагсаалт авах";
            }
        }
        public Result Txn120001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(ri.ReceivedParam[1]) == 0)
                    res = IPos.DB.Main.DB205002(db, Static.ToLong(ri.ReceivedParam[0]));
                else
                    res = IPos.DB.Main.DB227003(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Харилцагчийн дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    long NewValue = Static.ToLong(ri.ReceivedParam[0]);
                    lg.AddDetail("Customer","CustomerID", lg.item.Desc, Static.ToStr(NewValue));
                }
            }
        }
        public Result Txn120002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = (object[])ri.ReceivedParam[0];
                //string pContractNo = "";
                
                if (Static.ToInt(ri.ReceivedParam[2]) == 0)         // BackOffice-оос бүртгэсэн
                {
                    //pContractNo = Static.ToStr(obj[46]);
                    //if (pContractNo != "")
                    //{
                    //    res = CheckContractNo(db, pContractNo);
                    //    if (res.ResultNo != 0)
                    //        return res;
                    //}
                    if (Static.ToStr(obj[0]).Trim() != "" && Static.ToStr(obj[0]).Trim() != "0")
                        res = IPos.DB.Main.DB205003(db, obj, 1, Static.ToStr(obj[0]));
                    else
                        res = IPos.DB.Main.DB205003(db, obj, 0, "");
                }
                else
                {                                                   // FrontOffice-оос бүртгэсэн
                    //pContractNo = Static.ToStr(obj[13]);
                    //if (pContractNo != "")
                    //{
                    //    res = CheckContractNo(db, pContractNo);
                    //    if (res.ResultNo != 0)
                    //        return res;
                    //}
                    res = IPos.DB.Main.DB227004(db, obj, 0, "");
                }

                if (res.ResultNo == 0)
                {
                    res.Param =(object[])ri.ReceivedParam[0];
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
                lg.item.Desc = "Харилцагч нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Customer", Static.ToStr(FieldName[i]), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result CheckContractNo(DbConnections db, string pContractNo)
        {
            Result res = new Result();
            try
            {

                res = IPos.DB.Main.DB227000(db, pContractNo);
                if (res.ResultNo != 0)
                {
                    res.ResultNo = 1;
                    res.ResultDesc = "Гэрээний дугаар шалгах үед алдаа гарлаа.";
                }

                if (res.AffectedRows == 0)
                {
                    res.ResultNo = 1;
                    res.ResultDesc = "Гэрээний дугаар системд бүртгэлгүй байна. Шалгана уу";
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
        }
        public Result Txn120003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = (object[])ri.ReceivedParam[0];
                string pContractNo = "";

                if (Static.ToInt(ri.ReceivedParam[3]) == 0)
                {
                    //pContractNo = Static.ToStr(obj[46]);
                    //if (pContractNo != "")
                    //{
                    //    res = CheckContractNo(db, pContractNo);
                    //    if (res.ResultNo != 0)
                    //        return res;
                    //}
                    res = IPos.DB.Main.DB205004(db, obj);
                }
                else
                {
                    //pContractNo = Static.ToStr(obj[13]);
                    //if (pContractNo != "")
                    //{
                    //    res = CheckContractNo(db, pContractNo);
                    //    if (res.ResultNo != 0)
                    //        return res;
                    //}
                    res = IPos.DB.Main.DB227005(db, obj);
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
                lg.item.Desc = "Харилцагч засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (Static.ToStr(OldValue[i]) != Static.ToStr(NewValue[i])) lg.AddDetail("Customer", FieldName[i].ToString(),Static.ToStr(OldValue[i]), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result Txn120004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                long customerid;
                try
                {
                    customerid = Static.ToLong(ri.ReceivedParam[0]);
                    res = IPos.DB.Main.DB205005(db, customerid);
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
                    lg.item.Desc = "Харилцагчийн мэдээлэл устгах";
                    if (res.ResultNo == 0)
                    {
                        lg.AddDetail("Customer", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    }
                }
            }
        public Result Txn120066(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                try
                {
                    res = IPos.DB.Main.DB205066(db, Static.ToStr(ri.ReceivedParam[0]));
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
                    lg.item.Desc = "Харилцагчийн жагсаалт регистрээр авах";
                    if (res.ResultNo == 0)
                    {
                        lg.AddDetail("Customer", "RegisterNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    }
                }
            }
        public Result Txn130010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                try
                {
                    res = IPos.DB.Main.DB205067(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
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
        //Харилцагчийн ерөнхий мэдээллийн жагшаалт авах
        public Result Txn310125(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                try
                {
                    res = IPos.DB.CRMDB.DB263000(db, ri.PageIndex, ri.PageRows, null);
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
                    lg.item.Desc = "Харилцагчийн жагсаалт авах";
                }
            }
        //Харилцагчийн ерөнхий мэдээллийн дэлгэрэнгүй авах
        public Result Txn310126(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                long customerid;
                try
                {
                    customerid = Static.ToLong(ri.ReceivedParam[0]);
                    res = IPos.DB.CRMDB.DB263001(db, customerid);
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
                    lg.item.Desc = "Харилцагчийн дэлгэрэнгүй мэдээлэл авах";
                    if (res.ResultNo == 0)
                    {
                        long NewValue = Static.ToLong(ri.ReceivedParam[0]);
                        lg.AddDetail("Customer", "CustomerID", lg.item.Desc, Static.ToStr(NewValue));
                    }
                }
            }
        //Харилцагчийн ерөнхий мэдээлэл нэмэх
        public Result Txn310127(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                try
                {
                    res = IPos.DB.CRMDB.DB263002(db, (object[])ri.ReceivedParam[0]);
                    if (res.ResultNo == 0)
                    {
                        res.Param = (object[])ri.ReceivedParam[0];
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
                    lg.item.Desc = "Харилцагч нэмэх";
                    if (res.ResultNo == 0)
                    {
                        object[] NewValue = (object[])ri.ReceivedParam[0];
                        object[] FieldName = (object[])ri.ReceivedParam[1];
                        for (int i = 0; i < FieldName.Length; i++)
                        {
                            lg.AddDetail("Customer", Static.ToStr(FieldName[i]), lg.item.Desc, Static.ToStr(NewValue[i]));
                        }
                    }
                }
            }
        //Харилцагчийн ерөнхий мэдээлэл засах
        public Result Txn310128(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                try
                {
                    res = IPos.DB.CRMDB.DB263003(db, (object[])ri.ReceivedParam[0]);
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
                    lg.item.Desc = "Харилцагч засварлах";
                    if (res.ResultNo == 0)
                    {
                        object[] NewValue = (object[])ri.ReceivedParam[0];
                        object[] OldValue = (object[])ri.ReceivedParam[1];
                        object[] FieldName = (object[])ri.ReceivedParam[2];
                        for (int i = 0; i < FieldName.Length; i++)
                        {
                            if (Static.ToStr(OldValue[i]) != Static.ToStr(NewValue[i])) lg.AddDetail("Customer", FieldName[i].ToString(), Static.ToStr(OldValue[i]), Static.ToStr(NewValue[i]));
                        }
                    }
                }
            }
        //Харилцагчийн ерөнхий мэдээлэл устгах
        public Result Txn310129(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                long customerid;
                try
                {
                    customerid = Static.ToLong(ri.ReceivedParam[0]);
                    res = IPos.DB.CRMDB.DB263004(db, customerid);
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
                    lg.item.Desc = "Харилцагчийн мэдээлэл устгах";
                    if (res.ResultNo == 0)
                    {
                        lg.AddDetail("Customer", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    }
                }
            }
        //Холбоо барьсан харилцагчийг үндсэн харилцагчруу нэмэх
        public Result Txn310140(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        res = IPos.DB.CRMDB.DB263005(db, (object[])ri.ReceivedParam[0]);
                        if (res.ResultNo == 0)
                        {
                            res.Param = (object[])ri.ReceivedParam[0];
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
                    //finally
                    //{
                    //    lg.item.Desc = "Харилцагч нэмэх";
                    //    if (res.ResultNo == 0)
                    //    {
                    //        object[] NewValue = (object[])ri.ReceivedParam[0];
                    //        object[] FieldName = (object[])ri.ReceivedParam[1];
                    //        for (int i = 0; i < FieldName.Length; i++)
                    //        {
                    //            lg.AddDetail("Customer", Static.ToStr(FieldName[i]), lg.item.Desc, Static.ToStr(NewValue[i]));
                    //        }
                    //    }
                    //}
                }
        #endregion
        #region [ Customer address information functions ]
            public Result Txn120006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    int pagenumber = 1;
                    int pagecount = 1;
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB205006(db, pagenumber, pagecount, customerid);
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
                        lg.item.Desc = "Харилцагчийн хаягын жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAdd", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            public Result Txn120007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205007(db, customerid, seqid );
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
                        lg.item.Desc = "Харилцагчийн хаягын дэлгэрэнгүй мэдээлэл авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAddress", "customerid", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerAddress", "seqid", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
            public Result Txn120008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[8];
                    obj = (object[])ri.ReceivedParam[0];
                    try
                    {
                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[1] = Static.ToStr(SeqNo);

                        res = IPos.DB.Main.DB205008(db,obj);
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
                        lg.item.Desc = "Харилцагчийн хаяг нэмэх";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] FieldName = { "CUSTOMERNO", "SEQNO", "CITYCODE", "DISTCODE", "SUBDISTCODE", "NOTE", "ADDRCURRENT", "APARTMENT" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                lg.AddDetail("CustomerAddress", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120009(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        res = IPos.DB.Main.DB205009(db,(object[])ri.ReceivedParam[0]);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл засварлах";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] OldValue = (object[])ri.ReceivedParam[1];
                            object[] FieldName={ "CUSTOMERNO", "SEQNO", "CITYCODE", "DISTCODE", "SUBDISTCODE", "NOTE", "ADDRCURRENT", "APARTMENT" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerAddress", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205010(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAddress", "customerid", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerAddress", "seqid", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
            //Харилцагчийн хаягийн жагшаалт авах
            public Result Txn310135(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.CRMDB.DB265000(db, customerid);
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
                        lg.item.Desc = "Харилцагчийн хаягын жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAdd", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            //Харилцагчийн хаягийн дэлгэрэнгүй авах авах
            public Result Txn310136(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.CRMDB.DB265001(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хаягын дэлгэрэнгүй мэдээлэл авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAddress", "customerid", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerAddress", "seqid", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
            //Харилцагчийн хаяг нэмэх
            public Result Txn310137(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[8];
                    obj = (object[])ri.ReceivedParam[0];
                    try
                    {
                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[1] = Static.ToStr(SeqNo);
                        res = IPos.DB.CRMDB.DB265002(db, obj);
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
                        lg.item.Desc = "Харилцагчийн хаяг нэмэх";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] FieldName = { "CUSTOMERNO", "SEQNO", "CITYCODE", "DISTCODE", "SUBDISTCODE", "NOTE", "ADDRCURRENT", "APARTMENT"};
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                lg.AddDetail("CustomerAddress", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            //Харилцагчийн хаяг засварлах
            public Result Txn310138(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        res = IPos.DB.CRMDB.DB265003(db, (object[])ri.ReceivedParam[0]);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл засварлах";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] OldValue = (object[])ri.ReceivedParam[1];
                            object[] FieldName = { "CUSTOMERNO", "SEQNO", "CITYCODE", "DISTCODE", "SUBDISTCODE", "NOTE", "ADDRCURRENT", "APARTMENT" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerAddress", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            //Харилцагчийн хаяг устгах
            public Result Txn310139(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.CRMDB.DB265004(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAddress", "customerid", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerAddress", "seqid", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
        #endregion
        #region [ Customer зурган information functions ]
            public Result Txn120011(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    int pagenumber = 1;
                    int pagecount = 1;
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB205011(db, pagenumber, pagecount, customerid);
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
                        lg.item.Desc = "Харилцагчийн зургын жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerPic", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            public Result Txn120012(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205012(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн зургын дэлгэрэнгүй мэдээлэл авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerPic", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerPic", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
            public Result Txn120013(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[4];
                    try
                    {
                        obj = (object[])ri.ReceivedParam[0];
                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[1] = Static.ToStr(SeqNo);

                        res = IPos.DB.Main.DB205013(db, obj);
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
                        lg.item.Desc = "Харилцагчийн зураг нэмэх";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] FieldName = { "CUSTOMERNO", "SEQNO", "PICTURETYPE", "ATTACHID" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                lg.AddDetail("CustomerPic", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120014(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        res = IPos.DB.Main.DB205014(db,(object[])ri.ReceivedParam[0]);
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
                        lg.item.Desc = "Харилцагчийн зураг мэдээлэл засварлах";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] OldValue = (object[])ri.ReceivedParam[1];
                            object[] FieldName = { "CUSTOMERNO", "SEQNO", "PICTURETYPE", "ATTACHID" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerPic", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120015(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205015(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн зураг мэдээлэл устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerPic", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerPic", "SeqNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
        #endregion
        #region [ Customer зургын түүх information functions ]
            public Result Txn120016(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    int pagenumber = 1;
                    int pagecount = 1;
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB205016(db, pagenumber, pagecount, customerid);
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
                        lg.item.Desc = "Харилцагчийн зургын жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerPic", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            public Result Txn120017(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205017(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хаягын дэлгэрэнгүй мэдээлэл авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerPic", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerPic", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
            public Result Txn120018(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[8];
                    try
                    {
                        obj[0] = Static.ToDate(ri.ReceivedParam[0]);  //TXNDATE
                        obj[1] = ri.ReceivedParam[1]; //CUSTOMERNO
                        obj[3] = ri.ReceivedParam[3]; //PICTURETYPE
                        obj[4] = ri.ReceivedParam[4]; //ATTACHID
                        obj[5] = ri.ReceivedParam[5]; //ACTION
                        obj[6] = ri.ReceivedParam[6]; //USERNO
                        obj[7] = ri.ReceivedParam[7]; //POSTDATE

                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[2] = Static.ToStr(SeqNo);

                        res = IPos.DB.Main.DB205018(db, obj);
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
                        lg.item.Desc = "Харилцагчийн хаяг нэмэх";
                        if (res.ResultNo == 0)
                        {
                            lg.item.Desc = "Харилцагчийн хаяг нэмэх";
                        }
                    }
                }
            public Result Txn120019(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[8];
                    try
                    {
                        obj[0] = Static.ToDate(ri.ReceivedParam[0]);  //TXNDATE
                        obj[1] = ri.ReceivedParam[1]; //CUSTOMERNO
                        obj[2] = ri.ReceivedParam[2];
                        obj[3] = ri.ReceivedParam[3]; //PICTURETYPE
                        obj[4] = ri.ReceivedParam[4]; //ATTACHID
                        obj[5] = ri.ReceivedParam[5]; //ACTION
                        obj[6] = ri.ReceivedParam[6]; //USERNO
                        obj[7] = ri.ReceivedParam[7]; //POSTDATE
                

                        res = IPos.DB.Main.DB205019(db, obj);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл засварлах";
                    }
                }
            public Result Txn120020(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205020(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл устгах";
                    }
                }
        #endregion
        #region [ Харилцагчийн хамаатан садан ]
            public Result Txn120021(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    int pagenumber = 1;
                    int pagecount = 1;
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB205021(db, pagenumber, pagecount, customerid);
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
                        lg.item.Desc = "Харилцагчийн хамаатан садан жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerFamily", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            public Result Txn120022(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205022(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хамаатан садан дэлгэрэнгүй мэдээлэл авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerFamily", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerFamily", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
            public Result Txn120023(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[11];
                    try
                    {
                        obj = (object[])ri.ReceivedParam[0];
                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[1] = Static.ToStr(SeqNo);

                        res = IPos.DB.Main.DB205023(db, obj);
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
                        lg.item.Desc = "Харилцагчийн хамаатан садан нэмэх";
                        if (res.ResultNo == 0)
                        {
                            object[] FieldName = { "CUSTOMERNO", "FAMILYTYPE", "FIRSTNAME", "LASTNAME", "MIDDLENAME", "REGISTERNO", "PASSNO", "EMAIL", "TELEPHONE", "MOBILE" };
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                lg.AddDetail("CustomerFamily", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120024(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        res = IPos.DB.Main.DB205024(db,(object[])ri.ReceivedParam[0]);
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
                        lg.item.Desc = "Харилцагчийн хамаатан садан мэдээлэл засварлах";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] OldValue = (object[])ri.ReceivedParam[1];
                            object[] FieldName = { "CUSTOMERNO", "FAMILYTYPE", "FIRSTNAME", "LASTNAME", "MIDDLENAME", "REGISTERNO", "PASSNO", "EMAIL", "TELEPHONE", "MOBILE" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerFamily", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120025(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205025(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerFamily", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerFamily", "SeqNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
        #endregion
        #region [ Харилцагчийн байгууллагын захиралын мэдээлэл ]
        //Харилцагчийн байгууллагын захиралын мэдээлэл жагсаалт авах
        public Result DB205073(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            long customerid;
            try
            {
                res = IPos.DB.Main.DB205073(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Харилцагч байгууллагын захиралын мэдээлэл жагсаалт авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("CustomerFamily", "CustomerNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //Харилцагчийн байгууллагын захиралын мэдээлэл дэлгэрэнгүй авах
        public Result DB205074(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                customerid = Static.ToLong(ri.ReceivedParam[0]);
                seqid = Static.ToLong(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB205074(db, customerid, seqid);
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
                lg.item.Desc = "Харилцагч байгууллагын захиралын мэдээлэл дэлгэрэнгүй авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("CUSTDIRECTOR", "CustomerNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("CUSTDIRECTOR", "SeqNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        //Харилцагчийн байгууллагын захиралын мэдээлэл нэмэх
        public Result DB205075(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            { 
                object[] obj=new object[10];
                obj = (object[])ri.ReceivedParam[0];
                ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                obj[1] = Static.ToStr(SeqNo);
                res = IPos.DB.Main.DB205075(db, obj);
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
                lg.item.Desc = "Харилцагч байгууллагын захиралын мэдээлэл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] FieldName = { "CUSTOMERNO", "FAMILYTYPE", "FIRSTNAME", "LASTNAME", "MIDDLENAME", "REGISTERNO", "PASSNO", "EMAIL", "TELEPHONE", "MOBILE" };
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CustomerFamily", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        //Харилцагчийн байгууллагын захиралын мэдээлэл засах
        public Result DB205076(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB205076(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Харилцагч байгууллагын захиралын мэдээлэл засах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = { "CUSTOMERNO", "SEQNO", "POSITION","FIRSTNAME", "LASTNAME", "MIDDLENAME", "REGISTERNO", "PASSNO", "SEX", "BIRTHDAY" };
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CUSTDIRECTOR", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        //Харилцагчийн байгууллагын захиралын мэдээлэл устгах
        public Result DB205077(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                customerid = Static.ToLong(ri.ReceivedParam[0]);
                seqid = Static.ToLong(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB205077(db, customerid, seqid);
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
                lg.item.Desc = "Харилцагчийн байгууллагын захиралын мэдээлэл устгах";
                lg.AddDetail("CUSTDIRECTOR", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("CUSTDIRECTOR", "SeqNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
            }
        }
        #endregion
        #region [ Холбоо барьсан тэмдэглэл ]
            public Result Txn120036(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    int pagenumber = 1;
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
            public Result Txn120037(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
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
            public Result Txn120038(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
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
            public Result Txn120039(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[7];
                    try
                    {
                        res = IPos.DB.Main.DB205039(db,(object[])ri.ReceivedParam[0]);
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
            public Result Txn120040(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
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
        #endregion        
        #region [ Дансны мэдээлэл ]
            public Result Txn120041(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    int pagenumber = 1;
                    int pagecount = 1;
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB205041(db, pagenumber, pagecount, customerid);
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
                        lg.item.Desc = "Харилцагчийн хаягын жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAccount", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            public Result Txn120042(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205042(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хаягын дэлгэрэнгүй мэдээлэл авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAccount", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerAccount", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
            public Result Txn120043(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[5];
                    try
                    {
                        obj = (object[])ri.ReceivedParam[0];
                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[1] = Static.ToStr(SeqNo);
                        res = IPos.DB.Main.DB205043(db, obj);
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
                        lg.item.Desc = "Харилцагчийн хаяг нэмэх";
                        if (res.ResultNo == 0)
                        {
                            object[] FieldName = { "CUSTOMERNO", "BANKNO", "CURCODE", "ACCOUNTNO" };
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                lg.AddDetail("CustomerAccount", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120044(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[5];
                    try
                    {
                        res = IPos.DB.Main.DB205044(db,(object[])ri.ReceivedParam[0]);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл засварлах";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] OldValue = (object[])ri.ReceivedParam[1];
                            object[] FieldName = { "CUSTOMERNO", "BANKNO", "CURCODE", "ACCOUNTNO" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerAccount", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120045(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205045(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хаягын мэдээлэл устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAccount", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerAccount", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
        #endregion
        #region [ Дотоод дансны мэдээлэл ]
        public Result Txn120026(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB205026(db, Static.ToLong(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Харилцагчийн дотоод дансны жагсаалт авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("CustomerAccount", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn120027(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB205027(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[0]));
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
                lg.item.Desc = "Харилцагчийн дотоод дансны дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("CustomerAccount", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("CustomerAccount", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        public Result Txn120028(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB205028(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Харилцагчийн дотоод данс нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] FieldName = { "CUSTOMERNO", "ACCOUNTNO", "ACCOUNTNAME" };
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CUSTACNT", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result Txn120029(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB205029(db, (object[])ri.ReceivedParam[0]);
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
                lg.item.Desc = "Харилцагчийн дотоод данс засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = { "CUSTOMERNO", "ACCOUNTNO", "ACCOUNTNAME" };
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CUSTACNT", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result Txn120030(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB205030(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]));
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
                lg.item.Desc = "Харилцагчийн дотоод данс устгах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("CUSTACNT", "CustomerNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("CUSTACNT", "ACCOUNTNO", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        #endregion
        #region [ Хавсралт мэдээлллүүд ]
            public Result Txn120046(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    int pagenumber = 1;
                    int pagecount = 1;
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB205046(db, pagenumber, pagecount, customerid);
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
                        lg.item.Desc = "Харилцагчийн хавсралт мэдээллийн жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAttach", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            public Result Txn120049(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        res = IPos.DB.Main.DB205049(db, ri.ReceivedParam);
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
                        lg.item.Desc = "Харилцагчийн хавсралт мэдээлэл засварлах";
                    }
                }
            public Result Txn120050(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205050(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн хавсралт мэдээлэл устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAttach", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerAttach", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
        #endregion
        #region [ Товч дүгнэлт ]
            public Result Txn120051(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    int pagenumber = 1;
                    int pagecount = 1;
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB205051(db, pagenumber, pagecount, customerid);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerNote", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            public Result Txn120052(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205052(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт дэлгэрэнгүй мэдээлэл авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerNote", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerNote", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
            public Result Txn120053(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[6];
                    try
                    {
                        obj = (object[])ri.ReceivedParam[0];
                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[1] = Static.ToStr(SeqNo);

                        res = IPos.DB.Main.DB205053(db, obj);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт нэмэх";
                        if (res.ResultNo == 0)
                        {
                            object[] FieldName = { "CUSTOMERNO", "TXNDATE", "POSTDATE", "USERNO", "NOTE" };
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                lg.AddDetail("CustomerNote", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            public Result Txn120054(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[6];
                    try
                    {
                        res = IPos.DB.Main.DB205054(db,(object[])ri.ReceivedParam[0]);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт засварлах";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] OldValue = (object[])ri.ReceivedParam[1];
                            object[] FieldName = { "CUSTOMERNO", "TXNDATE", "POSTDATE", "USERNO", "NOTE" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerNote", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                            }
                        }

                    }
                }
            public Result Txn120055(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.Main.DB205055(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerNote", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerNote", "SeqNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
        //Харилцагчийн товч дүгнэлтийн жагшаалт авах
            public Result Txn310130(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        res = IPos.DB.CRMDB.DB264000(db, customerid);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerNote", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
        //Харилцагчийн товч дүгнэлтийн дэлгэрэнгүй авах
            public Result Txn310131(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.CRMDB.DB264001(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт дэлгэрэнгүй мэдээлэл авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerNote", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerNote", "SeqID", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
        //Харилцагчийн товч дүгнэлт нэмэх
            public Result Txn310132(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[6];
                    try
                    {
                        obj = (object[])ri.ReceivedParam[0];
                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[1] = Static.ToStr(SeqNo);

                        res = IPos.DB.CRMDB.DB264002(db, obj);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт нэмэх";
                        if (res.ResultNo == 0)
                        {
                            object[] FieldName = { "CUSTOMERNO", "TXNDATE", "POSTDATE", "USERNO", "NOTE" };
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                lg.AddDetail("CustomerNote", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
        //Харилцагчийн товч дүгнэлт засах
            public Result Txn310133(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[6];
                    try
                    {
                        res = IPos.DB.CRMDB.DB264003(db, (object[])ri.ReceivedParam[0]);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт засварлах";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] OldValue = (object[])ri.ReceivedParam[1];
                            object[] FieldName = { "CUSTOMERNO", "TXNDATE", "POSTDATE", "USERNO", "NOTE" };
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerNote", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                            }
                        }

                    }
                }
        //Харилцагчийн товч дүгнэлт устгах
            public Result Txn310134(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        res = IPos.DB.CRMDB.DB264004(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн товч дүгнэлт устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerNote", "CustomerID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                            lg.AddDetail("CustomerNote", "SeqNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
                        }
                    }
                }
        #endregion
        #region [ Харилцагчийн нэмэлт мэдээлэл ]
            public Result Txn120056(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        //res = IPos.DB.Main.DB205056(db, pagenumber, pagecount, customerid);
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
                        lg.item.Desc = "Харилцагчийн нэмэлт мэдээлэл жагсаалт авах";
                        if (res.ResultNo == 0)
                        {
                            lg.AddDetail("CustomerAdd", "TypeCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            public Result Txn120057(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        //res = IPos.DB.Main.DB205057(db, customerid, seqid);
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
                            lg.item.Desc = "Харилцагчийн нэмэлт мэдээллийн дэлгэрэнгүй мэдээлэл авах";
                        }
                    }
                }
            public Result Txn120058(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[8];
                    try
                    {
                        obj[0] = ri.ReceivedParam[0];  //CUSTOMERNO
                        obj[2] = ri.ReceivedParam[2]; //CITYCODE
                        obj[3] = ri.ReceivedParam[3]; //DISTCODE
                        obj[4] = ri.ReceivedParam[4]; //SUBDISTCODE
                        obj[5] = ri.ReceivedParam[5]; //NOTE
                        obj[6] = ri.ReceivedParam[6]; //ADDRCURRENT
                        obj[7] = ri.ReceivedParam[7]; //APARTMENT

                        ulong SeqNo = EServ.Interface.Sequence.NextByVal("SeqNo");
                        obj[1] = Static.ToStr(SeqNo);

                        res = IPos.DB.Main.DB205058(db, obj);
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
                        lg.item.Desc = "Харилцагчийн нэмэлт мэдээлэл нэмэх";
                        if (res.ResultNo == 0)
                        {

                        }
                    }
                }
            public Result Txn120059(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    object[] obj = new object[8];
                    try
                    {
                        obj[0] = ri.ReceivedParam[0];  //CUSTOMERNO
                        obj[1] = ri.ReceivedParam[1]; //SEQNO
                        obj[2] = ri.ReceivedParam[2]; //CITYCODE
                        obj[3] = ri.ReceivedParam[3]; //DISTCODE
                        obj[4] = ri.ReceivedParam[4]; //SUBDISTCODE
                        obj[5] = ri.ReceivedParam[5]; //NOTE
                        obj[6] = ri.ReceivedParam[6]; //ADDRCURRENT
                        obj[7] = ri.ReceivedParam[7]; //APARTMENT
                        res = IPos.DB.Main.DB205059(db, obj);
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
                            lg.item.Desc = "Харилцагчийн нэмэлт мэдээлэл засварлах";
                    }
                }
            public Result Txn120060(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    long customerid;
                    long seqid;
                    try
                    {
                        customerid = Static.ToLong(ri.ReceivedParam[0]);
                        seqid = Static.ToLong(ri.ReceivedParam[1]);
                        //res = IPos.DB.Main.DB205060(db, customerid, seqid);
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
                        lg.item.Desc = "Харилцагчийн нэмэлт мэдээлэл устгах";
                    }
                }
        #endregion
        public Result Txn120072(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
            {
                Result res = new Result();
                long customerid;
                try
                {
                    customerid = Static.ToLong(ri.ReceivedParam[0]);
                    res = IPos.DB.Main.DB208011(db, customerid);
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
                    lg.item.Desc = "Харилцагч дээр дансны жагсаалт авах";
                    if (res.ResultNo == 0)
                    {
                        lg.AddDetail("BacAccount", "CUSTOMERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    }
                }
            }
    }
}
