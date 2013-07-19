using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;

namespace IPos.Contract
{
    public class Contract : IModule
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
                    #region[General]
                    case 130001:	                //	Гэрээний үндсэн бүртгэл жагсаалт мэдээлэл авах
                        res = Txn130001(ci, ri, db, ref lg);
                        break;
                    case 130002:	                //  Гэрээний үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn130002(ci, ri, db, ref lg);
                        break;
                    case 130003:	                //	Гэрээний үндсэн бүртгэл нэмэх
                        res = Txn130003(ci, ri, db, ref lg);
                        break;
                    case 130004:	                //	Гэрээний үндсэн бүртгэл засах
                        res = Txn130004(ci, ri, db, ref lg);
                        break;
                    case 130005:	                //	Гэрээний үндсэн бүртгэл устгах
                        res = Txn130005(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Product]
                    case 130006:	                //	Гэрээнд хамрагдах бүтээгдэхүүн жагсаалт мэдээлэл авах
                        res = Txn130006(ci, ri, db, ref lg);
                        break;
                    case 130007:	                //  Гэрээнд хамрагдах бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах
                        res = Txn130007(ci, ri, db, ref lg);
                        break;
                    case 130008:	                //	Гэрээнд хамрагдах бүтээгдэхүүн шинээр нэмэх
                        res = Txn130008(ci, ri, db, ref lg);
                        break;
                    case 130009:	                //	Гэрээнд хамрагдах бүтээгдэхүүн засварлах
                        res = Txn130009(ci, ri, db, ref lg);
                        break;
                    case 130010:	                //	Гэрээнд хамрагдах бүтээгдэхүүн устгах
                        res = Txn130010(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Account]
                    case 130011:	                //	Гэрээний төлбөрийн төрөл ба дансны бүртгэл жагсаалт мэдээлэл авах
                        res = Txn130011(ci, ri, db, ref lg);
                        break;
                    case 130012:	                //  Гэрээний төлбөрийн төрөл ба дансны бүртгэл дэлгэрэнгүй авах
                        res = Txn130012(ci, ri, db, ref lg);
                        break;
                    case 130013:	                //	Гэрээний төлбөрийн төрөл ба дансны бүртгэл нэмэх
                        res = Txn130013(ci, ri, db, ref lg);
                        break;
                    case 130014:	                //	Гэрээний төлбөрийн төрөл ба дансны бүртгэл засварлах
                        res = Txn130014(ci, ri, db, ref lg);
                        break;
                    case 130015:	                //	Гэрээний төлбөрийн төрөл ба дансны бүртгэл устгах
                        res = Txn130015(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Dep]
                    case 130016:	                //	Гэрээний дүнг элэгдүүлэх хуваарь жагсаалт мэдээлэл авах
                        res = Txn130016(ci, ri, db, ref lg);
                        break;
                    case 130017:	                //  Гэрээний дүнг элэгдүүлэх хуваарь дэлгэрэнгүй мэдээлэл авах
                        res = Txn130017(ci, ri, db, ref lg);
                        break;
                    case 130018:	                //	Гэрээний дүнг элэгдүүлэх хуваарь нэмэх
                        res = Txn130018(ci, ri, db, ref lg);
                        break;
                    case 130019:	                //	Гэрээний дүнг элэгдүүлэх засах
                        res = Txn130019(ci, ri, db, ref lg);
                        break;
                    case 130020:	                //	Гэрээний дүнг элэгдүүлэх хуваарь устгах
                        res = Txn130020(ci, ri, db, ref lg);
                        break;
                    case 130021:	                //	Гэрээний дүнг Автоматаар хуваарилах
                        res = Txn130021(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Захиалгын үндсэн бүртгэл]
                    case 130101:	                //	Захиалгын үндсэн бүртгэл жагсаалт мэдээлэл авах
                        res = Txn130101(ci, ri, db, ref lg);
                        break;
                    case 130102:	                //  Захиалгын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn130102(ci, ri, db, ref lg);
                        break;
                    case 130103:	                //	Захиалгын үндсэн бүртгэл нэмэх
                        res = Txn130103(ci, ri, db, ref lg);
                        break;
                    case 130104:	                //	Захиалгын үндсэн бүртгэл засах
                        res = Txn130104(ci, ri, db, ref lg);
                        break;
                    case 130105:	                //	Захиалгын үндсэн бүртгэл устгах
                        res = Txn130105(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Захиалга өгсөн үйлчлүүлэгчид]
                    case 130106:	                //
                        res = Txn130106(ci, ri, db, ref lg);
                        break;
                    case 130107:	                //
                        res = Txn130107(ci, ri, db, ref lg);
                        break;
                    case 130108:	                //
                        res = Txn130108(ci, ri, db, ref lg);
                        break;
                    case 130109:	                //
                        res = Txn130109(ci, ri, db, ref lg);
                        break;
                    case 130110:	                //
                        res = Txn130110(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Захиалга доторх багц үйлчилгээний бүлэг]
                    case 130111:	                //	Захиалга доторх багц үйлчилгээний бүлэг жагсаалт мэдээлэл авах
                        res = Txn130111(ci, ri, db, ref lg);
                        break;
                    case 130112:	                //  Захиалга доторх багц үйлчилгээний бүлэг дэлгэрэнгүй мэдээлэл авах
                        res = Txn130112(ci, ri, db, ref lg);
                        break;
                    case 130113:	                //	Захиалга доторх багц үйлчилгээний бүлэг нэмэх
                        res = Txn130113(ci, ri, db, ref lg);
                        break;
                    case 130114:	                //	Захиалга доторх багц үйлчилгээний бүлэг засах
                        res = Txn130114(ci, ri, db, ref lg);
                        break;
                    case 130115:	                //	Захиалга доторх багц үйлчилгээний бүлэг устгах
                        res = Txn130115(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Захиалгын багц дах бүтээгдэхүүний бүртгэл]
                    case 130116:	                //	Захиалгын багц дахь бүтээгдэхүүний бүртгэл жагсаалт мэдээлэл авах
                        res = Txn130116(ci, ri, db, ref lg);
                        break;
                    case 130117:	                //  Захиалгын багц дахь бүтээгдэхүүний бүртгэл дэлгэрэнгүй мэдээлэл авах
                        res = Txn130117(ci, ri, db, ref lg);
                        break;
                    case 130118:	                //	Захиалгын багц дахь бүтээгдэхүүн нэмэх
                        res = Txn130118(ci, ri, db, ref lg);
                        break;
                    case 130119:	                //	Захиалгын багц дахь бүтээгдэхүүн засах
                        res = Txn130116(ci, ri, db, ref lg);
                        break;
                    case 130120:	                //	Захиалгын багц дахь бүтээгдэхүүн устгах
                        res = Txn130120(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Ажлын талбар]
                    case 130130:	                //	Ажлын талбарын үндсэн мэдээлэлийн жагсаалт авах
                        res = Txn130130(ci, ri, db, ref lg);
                        break;
                    case 130131:	                //	Ажлын талбарын үндсэн мэдээлэлийн дэлгэрэгүй авах
                        res = Txn130131(ci, ri, db, ref lg);
                        break;
                    case 130132:	                //	Ажлын талбарын үндсэн мэдээлэл нэмэх
                        res = Txn130132(ci, ri, db, ref lg);
                        break;
                    case 130133:	                //	Ажлын талбарын үндсэн мэдээлэлийн засах
                        res = Txn130133(ci, ri, db, ref lg);
                        break;
                    case 130134:	                //	Ажлын талбарын үндсэн мэдээлэлийн устгах
                        res = Txn130134(ci, ri, db, ref lg);
                        break;
                    case 130135:	                //	Ажлын талбар дээр пос бараа үйлчилгээ багц төлбөрийн хэрэгслийн жагсаалт авах
                        res = Txn130135(ci, ri, db, ref lg);
                        break;
                    case 130136:	                //	Ажлын талбар дээр пос бараа үйлчилгээ багц төлбөрийн хэрэгслийн жагсаалт авах
                        res = Txn130136(ci, ri, db, ref lg);
                        break;
                    case 130137:	                //	Ажлын талбар дээр пос бараа үйлчилгээ багц төлбөрийн хэрэгслийн жагсаалт авах
                        res = Txn130137(ci, ri, db, ref lg);
                        break;

                    #endregion
                    #region[Гэрээг xls ээс багцаар оруулах]
                    case 130022:	                //	Гэрээг xls ээс багцаар оруулах
                        res = Txn130022(ci, ri, db, ref lg);
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
        #region [ Гэрээний функцүүд]
        #region [Гэрээний үндсэн бүртгэл]
        public Result Txn130001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(ri.ReceivedParam[1]) == 0)
                    res = IPos.DB.Main.DB204001(db, ri.PageIndex, ri.PageRows, (object[])ri.ReceivedParam[0]);
                if (Static.ToInt(ri.ReceivedParam[1]) == 1)
                    res = IPos.DB.Main.DB227001(db, ri.PageIndex, ri.PageRows, (object[])ri.ReceivedParam[0]);
                if (Static.ToInt(ri.ReceivedParam[1]) == 2)
                    res = IPos.DB.Main.DB227002(db, ri.PageIndex, ri.PageRows, (object[])ri.ReceivedParam[0]);
                    return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний үндсэн бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    
                }
            }
        }
        public Result Txn130002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204002(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("Contract", "ContractNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        public Result Txn130003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string contractno = Static.ToStr(ri.ReceivedParam[0]).Trim();
                if (contractno!="" && contractno!="0")
                    res = IPos.DB.Main.DB204003(db, ri.ReceivedParam, 1, contractno);
                else
                    res = IPos.DB.Main.DB204003(db, ri.ReceivedParam, 0, "");
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний үндсэн бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204004(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний дүнг элэгдүүлэх хуваарь засах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130005(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                res = IPos.DB.Main.DB204005(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний үндсэн бүртгэл устгах";
            }
        }
        #endregion
        #region [Гэрээнд хамрагдах бүтээгдэхүүн ]
        public Result Txn130006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204006(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээнд хамрагдах бүтээгдэхүүн жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ContractProd", "ContractNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204007(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                    lg.item.Desc = "Гэрээнд хамрагдах бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("ContractProd", "ContractNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("ContractProd", "ProdCode", lg.item.Desc, ri.ReceivedParam[1].ToString());
            }
        }
        public Result Txn130008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204008(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээнд хамрагдах бүтээгдэхүүн шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130009(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204009(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээнд хамрагдах бүтээгдэхүүн засах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                res = IPos.DB.Main.DB204010(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээнд хамрагдах бүтээгдэхүүн устгах";
            }
        }
        #endregion
        #region [Гэрээний төлбөрийн төрөл ба дансны бүртгэл]
        public Result Txn130011(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204011(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний төлбөрийн төрөл ба дансны бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ContractAcnt", "ContractNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130012(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204012(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний төлбөрийн төрөл ба дансны бүртгэл дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("ContractAcnt", "ContractNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ContractAcnt", "PayType", lg.item.Desc, ri.ReceivedParam[1].ToString());
                lg.AddDetail("ContractAcnt", "AccountNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
            }
        }
        public Result Txn130013(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204013(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний төлбөрийн төрөл ба дансны бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130014(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204013(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний төлбөрийн төрөл ба дансны бүртгэл засварлах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130015(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                res = IPos.DB.Main.DB204015(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний төлбөрийн төрөл ба дансны бүртгэл устгах";
            }
        }
        #endregion
        #region [Гэрээний дүнг элэгдүүлэх хуваарь]
        public Result Txn130016(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204016(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний төлбөрийн төрөл ба дансны бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("DepSchedule", "ContractNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130017(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204017(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToDateTime(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний дүнг элэгдүүлэх хуваарь жагсаалт мэдээлэл авах";
                lg.AddDetail("DepSchedule", "ContractNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("DepSchedule", "Day", lg.item.Desc, ri.ReceivedParam[1].ToString());
            }
        }
        public Result Txn130018(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204018(db, ri.ReceivedParam);

                //DateTime startdate = Static.ToDate(ri.ReceivedParam[1]);
                //DateTime enddate = Static.ToDate(ri.ReceivedParam[2]);
                //int daycount = enddate.Subtract(startdate).Days;
                //if (daycount == 0)
                //{
                //    obj[0] = ri.ReceivedParam[0];
                //    obj[1] = startdate;
                //    obj[2] = Static.ToDecimal(ri.ReceivedParam[3]);
                //    res = IPos.DB.Main.DB204018(db, obj);
                //    return F_Error(res);
                //}
                //else
                //{
                //    decimal amount = Static.ToDecimal(ri.ReceivedParam[3]) / (daycount + 1);
                //    for (DateTime date = startdate; date.Date <= enddate.Date; date = date.AddDays(1))
                //    {
                //        obj[0] = ri.ReceivedParam[0];
                //        obj[1] = date;
                //        obj[2] = amount;
                //        res = IPos.DB.Main.DB204018(db, obj);
                //        if (res.ResultNo != 0) return F_Error(res);
                //    }
                //}
                //return F_Error(res);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний дүнг элэгдүүлэх хуваарь нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130019(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204019(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний дүнг элэгдүүлэх хуваарь засах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130020(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long customerid;
            long seqid;
            try
            {
                res = IPos.DB.Main.DB204020(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToDateTime(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэрээний дүнг элэгдүүлэх хуваарь устгах";
            }
        }
        //Гэрээний дүнг Автоматаар хуваарилах
        public Result Txn130021(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            string pContractno = Static.ToStr(ri.ReceivedParam[0]);
            int pSchType = Static.ToInt(ri.ReceivedParam[1]);                   //SchType 0-togtmol dungeer, 1-niit dung hugatsaand hamaaruulah
            string pDateType = Static.ToStr(ri.ReceivedParam[2]);               //DateType D, M, Q, H, Y
            DateTime pSchStartDate = Static.ToDate(ri.ReceivedParam[3]);        //SchStartDate
            DateTime pSchEndDate = Static.ToDate(ri.ReceivedParam[4]);          //SchEndDate
            decimal pSchAmount = Static.ToDecimal(ri.ReceivedParam[5]);         //SchAmount
            decimal pSchAllAmount = Static.ToDecimal(ri.ReceivedParam[6]);      //SchAllAmount

            Result res = new Result();
            try
            {
                long _count = 0;
                decimal _division = 0;
                if (pSchAmount != 0)
                    _division = pSchAllAmount / pSchAmount;
                decimal _round = (Math.Round(_division, 2) * 100 - Math.Round(_division, 0) * 100);

                //Delete all rows
                res = IPos.DB.Main.DB204021(db, pContractno);
                if (res.ResultNo != 0) return res;

                #region [ 0 - togtmol dungeer ]
                if (pSchType == 0)
                {
                    _count = System.Convert.ToInt64(Math.Round(_division, 0));

                    if (_round > 0) _count += 1;

                    _division = 0;

                    for (long i = 0; i < _count; i++)
                    {
                        //INSERT INTO SCHEDULE(DEALNO, SEQNO, SCHDATE, AMOUNT) VALUES(:1, :2, :3, :4)"
                        if (_division + pSchAmount >= pSchAllAmount)
                            pSchAmount = pSchAllAmount - _division;

                        res = IPos.DB.Main.DB204018(db, new object[]{
                            pContractno,
                            pSchStartDate,
                            pSchAmount
                        });
                        if (res.ResultNo != 0) return res;

                        switch (pDateType)
                        {
                            case "D": pSchStartDate = pSchStartDate.AddDays(1); break;
                            case "M": pSchStartDate = pSchStartDate.AddMonths(1); break;
                            case "Q": pSchStartDate = pSchStartDate.AddMonths(3); break;
                            case "H": pSchStartDate = pSchStartDate.AddMonths(6); break;
                            case "Y": pSchStartDate = pSchStartDate.AddYears(1); break;
                        }
                        _division = _division + pSchAmount;
                    }
                }
                #endregion
                #region [ 1-niit dung hugatsaand hamaaruulah ]
                else if (pSchType == 1)
                {
                    switch (pDateType)
                    {
                        case "D":
                            DateTime dt;
                            for (dt = pSchStartDate, _count = 0; dt <= pSchEndDate; dt = dt.AddDays(1), _count++)
                            { }
                            break;
                        case "Q":
                        case "H":
                        case "M":
                            _count = (pSchEndDate.Month + pSchEndDate.Year * 12) - (pSchStartDate.Month + pSchStartDate.Year * 12);
                            break;
                        case "Y":
                            _count = (pSchEndDate.Year - pSchStartDate.Year);
                            break;
                    }

                    pSchAmount = Math.Round(pSchAllAmount / _count, 2);

                    _division = 0;

                    for (long i = 0; i < _count; i++)
                    {
                        //INSERT INTO SCHEDULE(DEALNO, SEQNO, SCHDATE, AMOUNT) VALUES(:1, :2, :3, :4)"
                        if (_division + pSchAmount >= pSchAllAmount)
                            pSchAmount = pSchAllAmount - _division;

                        res = IPos.DB.Main.DB204018(db, new object[]{
                            pContractno,
                            pSchStartDate,
                            pSchAmount
                        });
                        if (res.ResultNo != 0) return res;

                        switch (pDateType)
                        {
                            case "D": pSchStartDate = pSchStartDate.AddDays(1); break;
                            case "M": pSchStartDate = pSchStartDate.AddMonths(1); break;
                            case "Q": pSchStartDate = pSchStartDate.AddMonths(3); break;
                            case "H": pSchStartDate = pSchStartDate.AddMonths(6); break;
                            case "Y": pSchStartDate = pSchStartDate.AddYears(1); break;
                        }
                        _division = _division + pSchAmount;
                    }
                }
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
        }
        #endregion
        #endregion
        #region [ Захиалгын функцүүд]
        #region [Захиалгын бүртгэл]
        public Result Txn130101(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204101(db, 1, 1, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын үндсэн бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDERS", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130102(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204102(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("ORDERS", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        public Result Txn130103(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string orderno = Static.ToStr(ri.ReceivedParam[0]).Trim();
                if (orderno != "" && orderno != "0")
                    res = IPos.DB.Main.DB204103(db, ri.ReceivedParam, 1, orderno);
                else
                    res = IPos.DB.Main.DB204103(db, ri.ReceivedParam, 0, "");
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын үндсэн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130104(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204104(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын үндсэн бүртгэл засварлах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130105(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204105(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын үндсэн бүртгэл устгах";
            }
        }
        #endregion
        #region [Захиалгад орсон үйлчлүүлэгч]
        public Result Txn130106(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204106(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгад орсон үйлчлүүлэгчийн бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDERPERSON", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130107(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204107(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгад орсон үйлчлүүлэгчийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("ORDERPERSON", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ORDERPERSON", "CUSTNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        public Result Txn130108(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204108(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгад орсон үйлчлүүлэгчийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130109(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204109(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]), Static.ToLong(ri.ReceivedParam[2]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгад орсон үйлчлүүлэгчийн бүртгэл засварлах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDERPERSON", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("ORDERPERSON", "CUSTNO", ri.ReceivedParam[1].ToString(), ri.ReceivedParam[2].ToString());
                }
            }
        }
        public Result Txn130110(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204110(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгад орсон үйлчлүүлэгчийн бүртгэл устгах";
                lg.AddDetail("ORDERPERSON", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ORDERPERSON", "CUSTNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        #endregion
        #region [Захиалга доторх багц үйлчилгээний бүлэг]
        public Result Txn130111(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204111(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалга доторх багц үйлчилгээний бүлэг жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDERPERSON", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130112(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204112(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалга доторх багц үйлчилгээний бүлэг дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("ORDERPERSON", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ORDERPERSON", "CUSTNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        public Result Txn130113(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204113(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалга доторх багц үйлчилгээний бүлэг шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130114(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204114(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалга доторх багц үйлчилгээний бүлэг засварлах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDERGROUP", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("ORDERGROUP", "GROUPNO", ri.ReceivedParam[1].ToString(), ri.ReceivedParam[2].ToString());
                }
            }
        }
        public Result Txn130115(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204115(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалга доторх багц үйлчилгээний бүлэг устгах";
                lg.AddDetail("ORDERGROUP", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ORDERGROUP", "GROUPNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        #endregion
        #region [Захиалгын багц дах бүтээгдэхүүний бүртгэл]
        public Result Txn130116(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204116(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын багц дахь бүтээгдэхүүний бүртгэл жагсаалт мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("ORDERPRODUCT", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn130117(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204117(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]), Static.ToInt(ri.ReceivedParam[3]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын багц дахь бүтээгдэхүүний дэлгэрэнгүй мэдээлэл авах";
                lg.AddDetail("ORDERPRODUCT", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ORDERPRODUCT", "GROUPNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ORDERPRODUCT", "PRODNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ORDERPRODUCT", "PRODTYPE", lg.item.Desc, ri.ReceivedParam[0].ToString());
            }
        }
        public Result Txn130118(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204118(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын багц дахь бүтээгдэхүүн шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130119(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204119(db, (object[])ri.ReceivedParam[0], (object[])ri.ReceivedParam[1]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын багц дахь бүтээгдэхүүн засварлах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130120(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204120(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]), Static.ToInt(ri.ReceivedParam[3]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалга доторх багц дах бүтээгдэхүүн устгах";
                lg.AddDetail("ORDERGROUP", "ORDERNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                lg.AddDetail("ORDERGROUP", "GROUPNO", lg.item.Desc, ri.ReceivedParam[1].ToString());
                lg.AddDetail("ORDERGROUP", "PRODNO", lg.item.Desc, ri.ReceivedParam[2].ToString());
                lg.AddDetail("ORDERGROUP", "PRODTYPE", lg.item.Desc, ri.ReceivedParam[3].ToString());
            }
        }
        #endregion
        #region[Ажлын талбарын үндсэн мэдээлэл]
        public Result Txn130130(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"select * from workarea";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn13021", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Ажлын талбарын жагсаалт авах";
            }
        }
        public Result Txn130131(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB204117(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]), Static.ToInt(ri.ReceivedParam[3]));
                return res;
                //return F_Error(res);
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын багц дахь бүтээгдэхүүний дэлгэрэнгүй мэдээлэл авах";
            }
        }
        public Result Txn130132(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"insert into workarea values(:1,:2,:3,:4)";
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130123", ri.ReceivedParam);
                return IPos.DB.Main.F_Error(res);
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын багц дахь бүтээгдэхүүн шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130133(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"update workarea set name=:2,name2=:3,orderno=:4 where areacode=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn130124", ri.ReceivedParam);
                return IPos.DB.Main.F_Error(res);
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалгын багц дахь бүтээгдэхүүн засварлах";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn130134(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"delete workarea where areacode=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn130125", ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалга доторх багц дах бүтээгдэхүүн устгах";
            }
        }
        #endregion
        #region[Гэрээг xls ээс багцаар оруулах]
        public Result Txn130022(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                DataTable _DT = null;
                int pType = 0;
                int pRowNum = 0;
                int pUserNo = 0;
                DateTime pTxnDate;
                string pPrefix= "";

                pType = Static.ToInt(ri.ReceivedParam[0]);
                _DT = (DataTable)ri.ReceivedParam[1];
                pUserNo = Static.ToInt(ri.ReceivedParam[2]);
                pTxnDate = Static.ToDate(ri.ReceivedParam[3]);
                pRowNum = Static.ToInt(ri.ReceivedParam[4]);
                pPrefix= Static.ToStr(ri.ReceivedParam[5]);

                //Гэрээний үндсэн мэдээлэл xls ees oruulah, 
                //Гэрээнд хамаарагдах бүтээгдэхүүнүүд, 
                //Гэрээний төлбөрийн төрөл ба дансны бүртгэл, 
                //Гэрээний дүнг элэгдүүлэх хуваарь

                switch (pType)
                {
                    case 0:
                        #region [ ContractMain ]
                        {
                            object[] obj = new object[27];
                            DateTime _dtime;
                            string _strdtime = "";
                            string[] times;

                            int i = 0;
                            foreach (DataRow dr in _DT.Rows)
                            {
                                i++;
                                if (i >= pRowNum)
                                {
                                    /*
                                    -1.Гэрээний дугаар - AutoNum
                                    0.Гэрээний төрөл
                                    1.Харилцагчийн дугаар
                                    2.Гэрээ хүчин төгөлдөр болох өдөр
                                    3.Гэрээ үйлчлэх цаг
                                    4.Гэрээ дуусах өдөр
                                    5.Гэрээ дуусах цаг
                                    6.Гэрээний үнийн дүн
                                    7.Валют
                                    8.Балансын төрөл
                                    9.Гэрээнд хамрагдах үйчлүүлэгчийн тоо
                                    10.Гэрээний дүнг элэгдүүлэх давтамж
                                    11.Гэрээний дүнг элэгдүүлэх дүн.
                                    12.Хариуцсан хэрэглэгч
                                    13.Rebateid
                                    14.Loyalid
                                    15.Pointid
                                    16.Гүйлгээнд НӨАТ тооцох эсэх
                                    17.Авлагын дансны дугаар 
                                    18.Орлогын дансны дугаар
                                    19.Гэрээний үлдэгдлийг хөдөлгөх арга

                                    20.CreateDate
                                    21.CreatePostDate
                                    22.CreateUser - Param
                                    23.Status - 0
                                    24.Balance - 0
                                    25.DepBalance - 0
                                    */

                                    obj[0] = "0";
                                    obj[1] = Static.ToStr((dr["column1"]));
                                    if (Static.ToStr((dr["column1"])) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Гэрээний төрөл хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[2] = Static.ToLong(dr["column2"]);
                                    if (Static.ToLong(dr["column2"]) == 0)
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Харилцагчийн дугаар хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[3] = Static.ToDate(dr["column3"]);
                                    if (Static.ToDate(dr["column3"]) == null)
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Эхлэх огноо хоосон байна шалгана уу";
                                        return res;
                                    }

                                    _strdtime = Static.ToStr(dr["column4"]);
                                    if (_strdtime == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Эхлэх цаг хоосон байна шалгана уу";
                                        return res;
                                    }

                                    times = _strdtime.Split('.');
                                    _dtime = Static.ToDateTime(dr["column3"]);

                                    DateTime _dateValue1 = new DateTime(_dtime.Year, _dtime.Month, _dtime.Day, Static.ToInt(times[0]), Static.ToInt(times[1]), Static.ToInt(times[2]));

                                    obj[4] = Static.ToDateTime(_dateValue1);

                                    obj[5] = Static.ToDate(dr["column5"]);

                                    _strdtime = Static.ToStr(dr["column6"]);
                                    if (_strdtime == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Дуусах цаг хоосон байна шалгана уу";
                                        return res;
                                    }

                                    times = _strdtime.Split('.');
                                    _dtime = Static.ToDateTime(dr["column5"]);

                                    DateTime _dateValue2 = new DateTime(_dtime.Year, _dtime.Month, _dtime.Day, Static.ToInt(times[0]), Static.ToInt(times[1]), Static.ToInt(times[2]));

                                    obj[6] = Static.ToDateTime(_dateValue2);
                                    obj[7] = Static.ToDecimal(dr["column7"]);

                                    obj[8] = Static.ToStr(dr["column8"]);
                                    obj[9] = Static.ToInt(dr["column9"]);
                                    obj[10] = Static.ToInt(dr["column10"]);
                                    obj[11] = Static.ToStr(dr["column11"]);
                                    obj[12] = Static.ToDecimal(dr["column12"]);
                                    obj[13] = Static.ToInt(dr["column13"]);
                                    obj[14] = Static.ToLong(dr["column14"]);
                                    obj[15] = Static.ToLong(dr["column15"]);
                                    obj[16] = Static.ToLong(dr["column16"]);

                                    obj[17] = Static.ToInt(dr["column17"]);
                                    obj[18] = Static.ToStr(dr["column18"]);
                                    obj[19] = Static.ToStr(dr["column19"]);

                                    obj[20] = Static.ToInt(dr["column20"]);

                                    obj[21] = Static.ToDate(pTxnDate);
                                    obj[22] = DateTime.Now;
                                    obj[23] = pUserNo;
                                    obj[24] = 1;
                                    obj[25] = 0;
                                    obj[26] = 0;

                                    string contractno = Static.ToStr(obj[0]).Trim();
                                    if (contractno != "" && contractno != "0")
                                        res = IPos.DB.Main.DB204022(db, obj, 1, contractno);
                                    else
                                        res = IPos.DB.Main.DB204022(db, obj, 0, "");
                                    if (res.ResultNo != 0)
                                        return res;
                                }
                            } 
                        } break;
                    #endregion [ ContractMain ]
                    case 1: 
                        #region [ ContractProd ]
                        {
                            object[] obj = new object[4];

                            int i = 0;
                            foreach (DataRow dr in _DT.Rows)
                            {
                                i++;
                                if (i >= pRowNum)
                                {
                                    /*
                                    0.Гэрээний дугаар
                                    1.Бүтээгдэхүүний дугаар
                                    2.Бүтээгдэхүүний төрөл
                                    3.Үнэ
                                    */

                                    obj[0] = Static.ToStr((dr["column1"]));
                                    if (Static.ToStr((dr["column1"])) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Гэрээний дугаар хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[1] = Static.ToStr(dr["column2"]);
                                    if (Static.ToStr(dr["column2"]) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Бүтээгдэхүүний дугаар хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[2] = Static.ToInt(dr["column3"]);
                                    obj[3] = Static.ToDecimal(dr["column4"]);

                                    res = IPos.DB.Main.DB204023(db, obj);
                                    if (res.ResultNo != 0)
                                        return res;
                                }
                            } 
                        } break;
                    #endregion
                    case 2: 
                        #region [ ContractAcnt ]
                        {
                            object[] obj = new object[4];

                            int i = 0;
                            foreach (DataRow dr in _DT.Rows)
                            {
                                i++;
                                if (i >= pRowNum)
                                {
                                    /*
                                    0.Гэрээний дугаар
                                    1.Төлбөрийн төрлийн дугаар
                                    2.Уг т.төрлөөр төлбөр төлөх дансны дугаар
                                    3.Дансны нэр
                                    */

                                    obj[0] = Static.ToStr((dr["column1"]));
                                    if (Static.ToStr((dr["column1"])) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Гэрээний дугаар хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[1] = Static.ToStr(dr["column2"]);
                                    if (Static.ToStr(dr["column2"]) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Төлбөрийн төрлийн дугаар хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[2] = Static.ToStr(dr["column3"]);
                                    if (Static.ToStr(dr["column3"]) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Уг т.төрлөөр төлбөр төлөх дансны дугаар хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[3] = Static.ToStr(dr["column4"]);
                                    if (Static.ToStr(dr["column4"]) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Дансны нэр хоосон байна шалгана уу";
                                        return res;
                                    }

                                    res = IPos.DB.Main.DB204024(db, obj);
                                    if (res.ResultNo != 0)
                                        return res;
                                }
                            } 
                        } break;
                    #endregion
                    case 3: 
                        #region [ ContractSchedule ]
                        {
                            object[] obj = new object[3];

                            int i = 0;
                            foreach (DataRow dr in _DT.Rows)
                            {
                                i++;
                                if (i >= pRowNum)
                                {
                                    /*
                                    0.Гэрээний дугаар
                                    1.Элэгдүүлэх огноо
                                    2.Элэгдүүлэх дүн
                                    */

                                    obj[0] = Static.ToStr((dr["column1"]));
                                    if (Static.ToStr((dr["column1"])) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Гэрээний дугаар хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[1] = Static.ToDate(dr["column2"]);
                                    if (Static.ToStr(dr["column2"]) == "")
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Элэгдүүлэх огноо хоосон байна шалгана уу";
                                        return res;
                                    }

                                    obj[2] = Static.ToDecimal(dr["column3"]);
                                    if (Static.ToDecimal(dr["column3"]) == 0)
                                    {
                                        res.ResultNo = 1;
                                        res.ResultDesc = "Элэгдүүлэх дүн хоосон байна шалгана уу";
                                        return res;
                                    }

                                    res = IPos.DB.Main.DB204025(db, obj);
                                    if (res.ResultNo != 0)
                                        return res;
                                }
                            } 
                        } break;
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
                lg.item.Desc = "Захиалга доторх багц дах бүтээгдэхүүн устгах";
            }
        }
        #endregion
        public Result Txn130135(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DataSet ds = new DataSet();
            Result res = new Result();
            try
            {
                string sql = @"select 0 as status,posno,posname,1 as type from posterminal
minus
select 0 as staus,b.posno,b.posname,1 as type from workarealink a
left join posterminal b on a.id=b.posno
where a.type=0 and a.areacode=:1
union
select 0 as staus,b.posno,b.posname,0 as type from workarealink a
left join posterminal b on a.id=b.posno
where a.type=0 and a.areacode=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn130135", ri.ReceivedParam[0]);
                    res.Data.Tables[0].TableName = "POS";
                    ds.Tables.Add(res.Data.Tables[0].Copy());

                    sql = @"select 0 as status,typeid,name,1 as type from papaytype
minus
select 0 as staus,b.typeid,b.name,1 as type from workarealink a
left join papaytype b on a.id=b.typeid
where a.type=1 and a.areacode=:1
union
select 0 as staus,b.typeid,b.name,0 as type from workarealink a
left join papaytype b on a.id=b.typeid
where a.type=1 and a.areacode=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn130135", ri.ReceivedParam[0]);
                    res.Data.Tables[0].TableName = "PAYTYPE";
                    ds.Tables.Add(res.Data.Tables[0].Copy());

                    sql = @"select 0 as status,id,name,prodtype,decode(ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as prodtypename,1 as type from 
(select invid as id,name,0 as prodtype from invmain union select servid as id,name,1 as prodtype from servmain)
minus
select 0 as staus,b.id,b.name,b.prodtype,decode(b.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as prodtypename ,1 as type from workarealink a
left join (select invid as id,name,0 as prodtype from invmain union select servid as id,name,1 as prodtype from servmain) b on a.id=b.id
where a.type=2 and a.areacode=:1
union
select 0 as staus,b.id,b.name,b.prodtype,decode(b.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as prodtypename,0 as type from workarealink a
left join (select invid as id,name,0 as prodtype from invmain union select servid as id,name,1 as prodtype from servmain) b on a.id=b.id
where a.type=2 and a.areacode=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn130135", ri.ReceivedParam[0]);
                    res.Data.Tables[0].TableName = "PRODUCT";
                    ds.Tables.Add(res.Data.Tables[0].Copy());

                    sql = @"select 0 as status,packid,name,1 as type from packmain
minus
select 0 as staus,b.packid,b.name,1 as type from workarealink a
left join packmain b on a.id=b.packid
where a.type=3 and a.areacode=:1
union
select 0 as staus,b.packid,b.name,0 as type from workarealink a
left join packmain b on a.id=b.packid
where a.type=3 and a.areacode=:1 order by name";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn130135", ri.ReceivedParam[0]);
                    res.Data.Tables[0].TableName = "PACKAGE";
                    ds.Tables.Add(res.Data.Tables[0].Copy());
                    res.Data = ds;
                    return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Захиалга доторх багц дах бүтээгдэхүүн устгах";
            }
        }
        public Result Txn130136(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DataSet ds = new DataSet();
            Result res = new Result();
            try
            {
                //Пос
                if (Static.ToInt(ri.ReceivedParam[2]) == 0)
                {
                    string sql = @"insert into workarealink(areacode,type,id) values(:1,:2,:3)";
                    DataTable dt = (DataTable)ri.ReceivedParam[1];
                    object[] obj = new object[3];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["status"]) == 1)
                        {
                            obj[0] = Static.ToStr(ri.ReceivedParam[0]);
                            obj[1] = 0;
                            obj[2] = Static.ToStr(dr["POSNO"]);
                            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130135", obj);
                            if (res.ResultNo != 0) return res;
                        }
                    }
                }
                //Төлбөрийн хэрэгсэл
                if (Static.ToInt(ri.ReceivedParam[2]) == 1)
                {
                    string sql = @"insert into workarealink(areacode,type,id) values(:1,:2,:3)";
                    DataTable dt = (DataTable)ri.ReceivedParam[1];
                    object[] obj = new object[3];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["status"]) == 1)
                        {
                            obj[0] = Static.ToStr(ri.ReceivedParam[0]);
                            obj[1] = 1;
                            obj[2] = Static.ToStr(dr["typeid"]);
                            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130135", obj);
                            if (res.ResultNo != 0) return res;
                        }
                    }
                }
                //Бараа үйлчилгээ
                if (Static.ToInt(ri.ReceivedParam[2]) == 2)
                {
                    string sql = @"insert into workarealink(areacode,type,producttype,id) values(:1,:2,:3,:4)";
                    DataTable dt = (DataTable)ri.ReceivedParam[1];
                    object[] obj = new object[4];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["status"]) == 1)
                        {
                            obj[0] = Static.ToStr(ri.ReceivedParam[0]);
                            obj[1] = 2;
                            obj[2] = Static.ToInt(dr["prodtype"]);
                            obj[3] = Static.ToStr(dr["id"]);
                            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130135", obj);
                            if (res.ResultNo != 0) return res;
                        }
                    }
                }
                //Багц
                if (Static.ToInt(ri.ReceivedParam[2]) == 3)
                {
                    string sql = @"insert into workarealink(areacode,type,id) values(:1,:2,:3)";
                    DataTable dt = (DataTable)ri.ReceivedParam[1];
                    object[] obj = new object[3];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["status"]) == 1)
                        {
                            obj[0] = Static.ToStr(ri.ReceivedParam[0]);
                            obj[1] = 3;
                            obj[2] = Static.ToStr(dr["packid"]);
                            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130135", obj);
                            if (res.ResultNo != 0) return res;
                        }
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
                lg.item.Desc = "Захиалга доторх багц дах бүтээгдэхүүн устгах";
            }
        }
        public Result Txn130137(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DataSet ds = new DataSet();
            Result res = new Result();
            try
            {
                //ri.receivedparm[4] төрөл
                /// 0 бол ПОС
                /// 1 бол Төлбөрийн хэрэгсэл
                /// 2 бол Бараа үйлчилгээ
                /// 3 бол Багц
                if (Static.ToInt(ri.ReceivedParam[2]) == 0)
                {
                    string sql = @"delete workarealink where areacode=:1 and type=:2 and id=:3";
                    DataTable dt = (DataTable)ri.ReceivedParam[1];
                    object[] obj = new object[3];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["status"]) == 1)
                        {
                            obj[0] = Static.ToStr(ri.ReceivedParam[0]);
                            obj[1] = 0;
                            obj[2] = Static.ToStr(dr["POSNO"]);
                            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130135", obj);
                            if (res.ResultNo != 0) return res;
                        }
                    }
                }
                if (Static.ToInt(ri.ReceivedParam[2]) == 1)
                {
                    string sql = @"delete workarealink where areacode=:1 and type=:2 and id=:3";
                    DataTable dt = (DataTable)ri.ReceivedParam[1];
                    object[] obj = new object[3];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["status"]) == 1)
                        {
                            obj[0] = Static.ToStr(ri.ReceivedParam[0]);
                            obj[1] = 1;
                            obj[2] = Static.ToStr(dr["TYPEID"]);
                            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130135", obj);
                            if (res.ResultNo != 0) return res;
                        }
                    }
                }
                if (Static.ToInt(ri.ReceivedParam[2]) == 2)
                {
                    string sql = @"delete workarealink where areacode=:1 and type=:2 and id=:3 and producttype=:4";
                    DataTable dt = (DataTable)ri.ReceivedParam[1];
                    object[] obj = new object[4];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["status"]) == 1)
                        {
                            obj[0] = Static.ToStr(ri.ReceivedParam[0]);
                            obj[1] = 2;
                            obj[2] = Static.ToStr(dr["ID"]);
                            obj[3] = Static.ToInt(dr["PRODTYPE"]);
                            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130135", obj);
                            if (res.ResultNo != 0) return res;
                        }
                    }
                }
                if (Static.ToInt(ri.ReceivedParam[2]) == 3)
                {
                    string sql = @"delete workarealink where areacode=:1 and type=:2 and id=:3";
                    DataTable dt = (DataTable)ri.ReceivedParam[1];
                    object[] obj = new object[3];
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToInt(dr["status"]) == 1)
                        {
                            obj[0] = Static.ToStr(ri.ReceivedParam[0]);
                            obj[1] = 3;
                            obj[2] = Static.ToStr(dr["PACKID"]);
                            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn130135", obj);
                            if (res.ResultNo != 0) return res;
                        }
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
                lg.item.Desc = "Захиалга доторх багц дах бүтээгдэхүүн устгах";
            }
        }
        #endregion
    }
}
