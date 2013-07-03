using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using IPos.Core;
namespace IPOS.Cash
{
    public class Main : IModule
    {
        DbConnection con = null;
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime First = DateTime.Now;
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 500004:        // Барааний багцийн жагсаалт авах
                        res = Txn500004(ci, ri, db, ref lg);
                        break;
                    case 500005:        // Борлуулалтын дугаараар барааны жагсаалт авах
                        res = Txn500005(ci, ri, db, ref lg);
                        break;
                    case 500006:        // Бараа, Үйлчилгээ хайх
                        res = Txn500006(ci, ri, db, ref lg);
                        break;
                    case 500007:        // Борлуулалт хийх
                        res = Txn99999(ci, ri, db, ref lg);
                        break;

                    case 600013:        // Борлуулалтын үндсэн мэдээлэл шинээр нэмэх
                        res = Txn600013(ci, ri, db, ref lg);
                        break;

                    //case 600014:        // Борлуулалтын үндсэн мэдээлэл шинээр нэмэх
                    //    res = Txn600014(ci, ri, db, ref lg);
                    //    break;
                    case 600006: //Багц үндсэн мэдээлэл жагсаалт авах
                        res = Txn600006(ci, ri, db, ref lg);
                        break;
                    case 600008: //Багц үндсэн мэдээлэл үүсгэх
                        res = Txn600008(ci, ri, db, ref lg);
                        break;
                    case 600010: //Багц үндсэн мэдээлэл устгах
                        res = Txn600010(ci, ri, db, ref lg);
                        break;
                    case 600018: //Борлуулалтаар гарсан бараа материал нэмэх
                        res = Txn600018(ci, ri, db, ref lg);
                        break;
                    case 600019: //Борлуулалтаар гарсан бараа материал засварлах
                        res = Txn600019(ci, ri, db, ref lg);
                        break;
                    case 600020: //Борлуулалтаар гарсан бараа материал устгах
                        res = Txn600020(ci, ri, db, ref lg);
                        break;
                    case 600022: //Борлуулсан багцын төлбөрийн төрлүүдийн жагсаалт авах
                        res = Txn600022(ci, ri, db, ref lg);
                        break;
                    case 600033: //Борлуулалтын Төлбөрийн Бүртгэл шинээр нэмэх
                        res = Txn600033(ci, ri, db, ref lg);
                        break;
                    case 90000: //Захиалга дээрх бүтээгдэхүүний жагсаалт авах
                        res = Txn90000(ci, ri, db, ref lg);
                        break;
                    case 600041: //Түрээсийн барааны хайлт 
                        res = Txn600041(ci, ri, db, ref lg);
                        break;
                    case 600042: //Түрээсийн барааны хайлт 
                        res = Txn600042(ci, ri, db, ref lg);
                        break;
                    case 600043: //Түрээсийн барааны хайлт 
                        res = Txn600043(ci, ri, db, ref lg);
                        break;
                    case 600090: //Багцын хайлт salesprod
                        res = Txn600090(ci, ri, db, ref lg);
                        break;
                    default:
                        res = new Result(1000, "Unknown transation.");
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
                    ISM.Lib.Static.WriteToLogFile("IPos.Cash", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("IPos.Cash", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
            }
        }

        public Result Txn500004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = "select * from productpanel";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn500004",null);
                //res = IPos.DB.Panels.DB500001(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
                return res;
            }
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
                string sql = @"
select S.ProdType, S.ProdNo, case when i.name is null then v.name else i.name end name, S.Price,0 UniDiscount, S.Quantity,  S.Discount, S.VAT, S.SalesAmount   
from salesprod S
left join invmain i on I.INVID = S.prodno and S.PRODTYPE=0
left join servmain v on V.SERVID = S.prodno and S.PRODTYPE=1 
where S.SalesNo=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn500005", ri.ReceivedParam);
                return res;
            }
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
        public Result Txn500006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            string sql = "";
            Result res = new Result();
            try
            {
                    sql = @"
select 0 status,0 prodtype, invid prodid, name, invtype, count  from invmain 
where status=0 and invid like '{0}%' and name like '{1}%' 
union
select 0 status,1 prodtype, servid prodid, name, servtype, count  from servmain 
where status=0 and servid like '{0}%' and name like '{1}%'";

                res = db.ExecuteQuery("core", string.Format(sql,Static.ToStr(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2])) , enumCommandType.SELECT, "Txn500006", ri.ReceivedParam);
                return res;
            }
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
            string sql = "";
            string paymentsql = "";
            #region[ SQL ]
            if (Static.ToDate(IPos.Core.SystemProp.TxnDate) == Static.ToDate(ri.ReceivedParam[1]))
            {
                sql = @"select a.custno,decode(k.CLASSCODE, 1, k.CORPORATENAME, substr(k.FIRSTNAME, 0, 1)||'.'||k.LASTNAME) customername,b.prodno,decode(b.prodtype,0,'Бараа','Үйлчилгээ') as prodtype,c.name,b.price,b.quantity,b.price*b.quantity as totalamount,b.discount,b.salesamount*b.quantity as salesamount,a.status,a.oldsalesno from salesmain dd
left join sales a on dd.salesno=a.salesno
left join salesprod b on dd.salesno=b.salesno
left join invmain c on b.prodno=c.invid
left join customer k on a.custno=k.customerno
where dd.batchno=:1 and B.PRODTYPE=0 and to_char(a.postdate,'mm/d/yyyy')=to_char(:2,'mm/d/yyyy')
union
select a.custno,decode(k.CLASSCODE, 1, k.CORPORATENAME, substr(k.FIRSTNAME, 0, 1)||'.'||k.LASTNAME) customername,b.prodno,decode(b.prodtype,0,'Бараа','Үйлчилгээ') as prodtype,c.name,b.price,b.quantity,b.price*b.quantity as totalamount,b.discount,b.salesamount*b.quantity as salesamount,a.status,a.oldsalesno from salesmain dd
left join sales a on dd.salesno=a.salesno
left join salesprod b on dd.salesno=b.salesno
left join servmain c on b.prodno=c.servid
left join customer k on a.custno=k.customerno
where dd.batchno=:1 and B.PRODTYPE=1 and to_char(a.postdate,'mm/d/yyyy')=to_char(:2,'mm/d/yyyy')";

                paymentsql = @"select spd.seqno,spd.PAYMENTTYPE,ppt.name,spd.contractno,spd.amount,spd.paymentflag from salespayment sp
left join salespaymentdetail spd on sp.paymentno=spd.paymentno
left join papaytype ppt on spd.paymenttype=ppt.typeid
where sp.salesno=:1";
            }
            else
            {
                sql = @"select a.custno,decode(k.CLASSCODE, 1, k.CORPORATENAME, substr(k.FIRSTNAME, 0, 1)||'.'||k.LASTNAME) customername,b.prodno,decode(b.prodtype,0,'Бараа','Үйлчилгээ') as prodtype,c.name,b.price,b.quantity,b.price*b.quantity as totalamount,b.discount,b.salesamount*b.quantity as salesamount,a.status from salesmain dd
left join sales_hist a on dd.salesno=a.salesno
left join salesprod_hist b on dd.salesno=b.salesno
left join invmain c on b.prodno=c.invid
left join customer k on a.custno=k.customerno
where dd.batchno=:1 and B.PRODTYPE=0 and to_char(a.postdate,'mm/d/yyyy')=to_char(:2,'mm/d/yyyy')
union
select a.custno,decode(k.CLASSCODE, 1, k.CORPORATENAME, substr(k.FIRSTNAME, 0, 1)||'.'||k.LASTNAME) customername,b.prodno,decode(b.prodtype,0,'Бараа','Үйлчилгээ') as prodtype,c.name,b.price,b.quantity,b.price*b.quantity as totalamount,b.discount,b.salesamount*b.quantity as salesamount,a.status from salesmain dd
left join sales_hist a on dd.salesno=a.salesno
left join salesprod_hist b on dd.salesno=b.salesno
left join servmain c on b.prodno=c.servid
left join customer k on a.custno=k.customerno
where dd.batchno=:1 and B.PRODTYPE=1 and to_char(a.postdate,'mm/d/yyyy')=to_char(:2,'mm/d/yyyy')";
                paymentsql = @"select spd.seqno,spd.PAYMENTTYPE,ppt.name,spd.contractno,spd.amount,spd.paymentflag from salespayment_hist sp
left join salespaymentdetail_hist spd on sp.paymentno=spd.paymentno
left join papaytype ppt on spd.paymenttype=ppt.typeid
where sp.salesno=:1";
            }
            #endregion
            Result res = new Result();
            try
            {
                DataSet ds = new DataSet();
                #region[ PRODUCTDETAILLIST ]
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn500007", ri.ReceivedParam[0], ri.ReceivedParam[2]);
                if (res.ResultNo != 0) return res;
                DataTable dt = res.Data.Tables[0].Copy();
                dt.Clear();
                var returnsales = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToInt(x["STATUS"]) == 1).Select(x => x);
                var sales = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToInt(x["STATUS"]) == 0).Select(x => x);
                if (returnsales != null)
                {
                    if (returnsales.Count() != 0)
                    {
                        long customerno = 0;
                        string prodno = "";
                        int prodtype = 0;
                        int quantity = 0;
                        int status = 0;
                        if (sales.Count() == 0)
                        {

                        }
                        else
                        {
                            foreach (DataRow salerow in sales.CopyToDataTable().Rows)
                            {
                                customerno = Static.ToLong(salerow["CUSTNO"]);
                                prodno = Static.ToStr(salerow["PRODNO"]);
                                prodtype = Static.ToInt(salerow["PRODTYPE"]);
                                quantity = Static.ToInt(salerow["QUANTITY"]);
                                status = Static.ToInt(salerow["status"]);

                                foreach (DataRow salereturn in returnsales.CopyToDataTable().Rows)
                                {
                                    if (customerno == Static.ToLong(salereturn["CUSTNO"]) && prodno == Static.ToStr(salereturn["PRODNO"]) && prodtype == Static.ToInt(salereturn["PRODTYPE"]))
                                    {
                                        int count = quantity - Static.ToInt(salereturn["QUANTITY"]);
                                        #region[ COUNT=0 ]
                                        if (count == 0)
                                        {
                                            var validate = dt.AsEnumerable().Where(x => Static.ToLong(x["CUSTNO"]) == customerno).Where(x => x["PRODNO"].ToString() == prodno).Where(x => Static.ToInt(x["PRODTYPE"]) == prodtype).Where(x => Static.ToInt(x["STATUS"]) == status).Select(x => x);
                                            if (validate != null)
                                            {
                                                if (validate.Count() == 0)
                                                {
                                                    dt.Rows.Add(new object[] { salerow["CUSTNO"], salerow["CUSTOMERNAME"], salerow["PRODNO"], salerow["PRODTYPE"], salerow["NAME"], salerow["PRICE"], salerow["QUANTITY"], salerow["TOTALAMOUNT"], salerow["DISCOUNT"], salerow["SALESAMOUNT"], salerow["STATUS"] });
                                                }
                                            }
                                        }
                                        #endregion
                                        else
                                        {
                                            #region[ COUNT > 0 ]
                                            if (count > 0)
                                            {
                                                var validate = dt.AsEnumerable().Where(x => Static.ToLong(x["CUSTNO"]) == customerno).Where(x => x["PRODNO"].ToString() == prodno).Where(x => Static.ToInt(x["PRODTYPE"]) == prodtype).Where(x => Static.ToInt(x["quantity"]) == quantity).Where(x => Static.ToInt(x["status"]) == 0).Select(x => x);
                                                if (validate != null)
                                                {
                                                    if (validate.Count() == 0)
                                                    {
                                                        dt.Rows.Add(new object[]
                                                {
                                                    salereturn["CUSTNO"]
                                                    , salereturn["CUSTOMERNAME"]
                                                    , salereturn["PRODNO"]
                                                    , salereturn["PRODTYPE"]
                                                    , salereturn["NAME"]
                                                    , Static.ToDecimal(salereturn["PRICE"])
                                                    , salereturn["QUANTITY"]
                                                    , Static.ToDecimal(salereturn["TOTALAMOUNT"])
                                                    , Static.ToDecimal(salereturn["DISCOUNT"])
                                                    , Static.ToDecimal(salereturn["SALESAMOUNT"])
                                                    , 0
                                                });
                                                        dt.Rows.Add(new object[] 
                                                {
                                                    salerow["CUSTNO"]
                                                    , salerow["CUSTOMERNAME"]
                                                    , salerow["PRODNO"]
                                                    , salerow["PRODTYPE"]
                                                    , salerow["NAME"] + " [Нэмэлт]"
                                                    , salerow["PRICE"]
                                                    , count
                                                    , Static.ToDecimal(salerow["TOTALAMOUNT"])-Static.ToDecimal(salereturn["TOTALAMOUNT"])
                                                    , Static.ToDecimal(salerow["DISCOUNT"])-Static.ToDecimal(salereturn["DISCOUNT"])
                                                    , Static.ToDecimal(salerow["SALESAMOUNT"])-Static.ToDecimal(salereturn["SALESAMOUNT"])
                                                    ,1
                                                });
                                                    }
                                                }
                                            }
                                            #endregion
                                            #region[ COUNT < 0 ]
                                            if (count < 0)
                                            {
                                                var validate = dt.AsEnumerable().Where(x => Static.ToLong(x["CUSTNO"]) == customerno).Where(x => x["PRODNO"].ToString() == prodno).Where(x => Static.ToInt(x["PRODTYPE"]) == prodtype).Where(x => Static.ToInt(x["quantity"]) == quantity).Where(x => Static.ToInt(x["status"]) == 0).Select(x => x);
                                                if (validate != null)
                                                {
                                                    if (validate.Count() == 0)
                                                    {
                                                        dt.Rows.Add(new object[]
                                                        {
                                                            salerow["CUSTNO"]
                                                            , salerow["CUSTOMERNAME"]
                                                            , salerow["PRODNO"]
                                                            , salerow["PRODTYPE"]
                                                            , salerow["NAME"] + " [Буцаалт]"
                                                            , Static.ToDecimal(salerow["PRICE"])
                                                            , -count
                                                            , (Static.ToDecimal(salerow["PRICE"])*count)
                                                            , -Static.ToDecimal(salerow["DISCOUNT"])
                                                            , (Static.ToDecimal(salerow["PRICE"])*count)
                                                            , 1
                                                        });
                                                        dt.Rows.Add(new object[]
                                                        {
                                                            salereturn["CUSTNO"]
                                                            , salereturn["CUSTOMERNAME"]
                                                            , salereturn["PRODNO"]
                                                            , salereturn["PRODTYPE"]
                                                            , salereturn["NAME"]
                                                            , salereturn["PRICE"]
                                                            , salereturn["QUANTITY"]
                                                            , salereturn["TOTALAMOUNT"]
                                                            , salereturn["DISCOUNT"]
                                                            , salereturn["SALESAMOUNT"]
                                                            , 0
                                                        });
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {                                               
                                        var validate = dt.AsEnumerable().Where(x => Static.ToLong(x["CUSTNO"]) == customerno).Where(x => x["PRODNO"].ToString() == prodno).Where(x => Static.ToInt(x["PRODTYPE"]) == prodtype).Where(x => Static.ToInt(x["quantity"]) == quantity).Where(x => Static.ToInt(x["status"]) == 0).Select(x => x);
                                        if (validate != null)
                                        {
                                            if (validate.Count() == 0)
                                            {
                                                dt.Rows.Add(new object[]
                                                        {
                                                            salerow["CUSTNO"]
                                                            , salerow["CUSTOMERNAME"]
                                                            , salerow["PRODNO"]
                                                            , salerow["PRODTYPE"]
                                                            , salerow["NAME"] + " [Нэмэлт]"
                                                            , salerow["PRICE"]
                                                            , salerow["QUANTITY"]
                                                            , salerow["PRICE"]
                                                            , salerow["DISCOUNT"]
                                                            , salerow["PRICE"]
                                                            , 0
                                                        });
                                            }
                                        }
                                    }
                                    #region[Бүтэн буцсан барааг олж байна]
                                    var query = sales.AsEnumerable().Where(x => Static.ToLong(x["CUSTNO"]) == Static.ToLong(salereturn["CUSTNO"])).Where(x => x["PRODNO"].ToString() == Static.ToStr(salereturn["PRODNO"])).Where(x => Static.ToInt(x["PRODTYPE"]) == Static.ToInt(salereturn["PRODTYPE"])).Select(x => x);
                                    if (query != null)
                                    {
                                        if (query.Count() == 0)
                                        {
                                            var validate = dt.AsEnumerable().Where(x => Static.ToLong(x["CUSTNO"]) == Static.ToLong(salereturn["CUSTNO"])).Where(x => x["PRODNO"].ToString() == Static.ToStr(salereturn["PRODNO"])).Where(x => Static.ToInt(x["PRODTYPE"]) == Static.ToInt(salereturn["PRODTYPE"])).Where(x => Static.ToInt(x["quantity"]) == Static.ToInt(salereturn["quantity"])).Where(x => Static.ToInt(x["status"]) == 0).Select(x => x);
                                            if (validate != null)
                                            {
                                                if (validate.Count() == 0)
                                                {
                                                    dt.Rows.Add(
                                                        new object[]
                                                {
                                                    salereturn["CUSTNO"]
                                                    , salereturn["CUSTOMERNAME"]
                                                    , salereturn["PRODNO"]
                                                    , salereturn["PRODTYPE"]
                                                    , salereturn["NAME"] + " [Буцаалт]"
                                                    , Static.ToDecimal(salereturn["PRICE"])
                                                    , Static.ToInt(salereturn["QUANTITY"])
                                                    , -Static.ToDecimal(salereturn["TOTALAMOUNT"])
                                                    , -Static.ToDecimal(salereturn["DISCOUNT"])
                                                    , -Static.ToDecimal(salereturn["SALESAMOUNT"])
                                                    , 1
                                                });
                                                    dt.Rows.Add(
                                                        new object[]
                                                {
                                                    salereturn["CUSTNO"]
                                                    , salereturn["CUSTOMERNAME"]
                                                    , salereturn["PRODNO"]
                                                    , salereturn["PRODTYPE"]
                                                    , salereturn["NAME"]
                                                    , salereturn["PRICE"]
                                                    , salereturn["QUANTITY"]
                                                    , salereturn["TOTALAMOUNT"]
                                                    , salereturn["DISCOUNT"]
                                                    , salereturn["SALESAMOUNT"]
                                                    , 0
                                                });
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                    else
                    {
                        dt = res.Data.Tables[0].Copy();
                    }
                }

                dt.TableName = "PRODUCTLIST";
                ds.Tables.Add(dt);

                #endregion
                #region[ PAYMENTDETAILLIST ]
                res = db.ExecuteQuery("core", paymentsql, enumCommandType.SELECT, "Txn50002", ri.ReceivedParam[0]);
                if (res.ResultNo != 0) return res;
                if (res.Data != null)
                {
                    int seqno = 0;
                    string paymenttype = null;
                    decimal amount = 0;
                    int status = 0;
                    //DataTable paydata = res.Data.Tables[0].Copy();
                    ds.Tables.Add(res.Data.Tables[0].Copy());
                //    paydata.Clear();
                //    var returnpay = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToInt(x["STATUS"]) == 1).Select(x => x);
                //    var pay = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToInt(x["STATUS"]) == 0).Select(x => x);
                //    if (returnpay != null)
                //    {
                //        if (returnpay.Count() != 0)
                //        {
                //            if (pay != null)
                //            {
                //                if (pay.Count() != 0)
                //                {
                //                    foreach (DataRow payrow in pay.CopyToDataTable().Rows)
                //                    {
                //                        seqno = Static.ToInt(payrow["SEQNO"]);
                //                        paymenttype = Static.ToStr(payrow["PAYMENTTYPE"]);
                //                        status = Static.ToInt(payrow["STATUS"]);
                //                        foreach (DataRow returnrow in returnpay.CopyToDataTable().Rows)
                //                        {

                //                        }
                //                    }
                //                }
                //            }
                //            else
                //            {

                //            }
                //        }
                //        else
                //        {

                //        }
                //    }
                }

                #endregion
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
                lg.item.Desc = "Борлуулалтын гүйлгээний дэлгэрэнгүй жагсаалт авах";
            }
        }
        public Result Txn99999(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            string sql = "";
            string paymentsql = "";
            Hashtable validate = new Hashtable();
            #region[ SQL ]
            if (Static.ToDate(IPos.Core.SystemProp.TxnDate) == Static.ToDate(ri.ReceivedParam[1]))
            {
                sql = @"select a.custno,decode(k.CLASSCODE, 1, k.CORPORATENAME, substr(k.FIRSTNAME, 0, 1)||'.'||k.LASTNAME) customername,b.prodno,decode(b.prodtype,0,'Бараа','Үйлчилгээ') as prodtype,c.name,b.price,b.quantity,b.price as totalamount,b.discount,b.salesamount as salesamount,a.status,a.postdate,a.oldsalesno,a.salesno,b.saletype from salesmain dd
left join sales a on dd.salesno=a.salesno
left join salesprod b on dd.salesno=b.salesno
left join invmain c on b.prodno=c.invid
left join customer k on a.custno=k.customerno
where dd.batchno=:1 and B.PRODTYPE=0
union
select a.custno,decode(k.CLASSCODE, 1, k.CORPORATENAME, substr(k.FIRSTNAME, 0, 1)||'.'||k.LASTNAME) customername,b.prodno,decode(b.prodtype,0,'Бараа','Үйлчилгээ') as prodtype,c.name,b.price,b.quantity,b.price as totalamount,b.discount,b.salesamount,a.status,a.postdate,a.oldsalesno,a.salesno,b.saletype from salesmain dd
left join sales a on dd.salesno=a.salesno
left join salesprod b on dd.salesno=b.salesno
left join servmain c on b.prodno=c.servid
left join customer k on a.custno=k.customerno
where dd.batchno=:1 and B.PRODTYPE=1";

                paymentsql = @"select spd.seqno,spd.PAYMENTTYPE,ppt.name,spd.contractno,spd.amount,decode(spd.paymentflag,1,'Буцаагдсан') as status,sp.chargeamount from salespayment sp
left join salespaymentdetail spd on sp.paymentno=spd.paymentno
left join papaytype ppt on spd.paymenttype=ppt.typeid
where sp.salesno=:1";
            }
            else
            {
                sql = @"select a.custno
,decode(k.CLASSCODE, 1, k.CORPORATENAME, substr(k.FIRSTNAME, 0, 1)||'.'||k.LASTNAME) customername
,b.prodno
,decode(b.prodtype,0,'Бараа','Үйлчилгээ') as prodtype
,c.name
,b.price
,b.quantity
,b.price as totalamount
,b.discount
,b.salesamount as salesamount
,a.status
,a.postdate
,a.oldsalesno
,a.salesno
,b.saletype from salesmain_hist dd
left join sales_hist a on dd.salesno=a.salesno
left join salesprod_hist b on dd.salesno=b.salesno
left join invmain c on b.prodno=c.invid
left join customer k on a.custno=k.customerno
where dd.batchno=:1 and B.PRODTYPE=0 and to_char(a.postdate,'mm/d/yyyy')=to_char(:2,'mm/d/yyyy')
union
select
a.custno
,decode(k.CLASSCODE, 1, k.CORPORATENAME, substr(k.FIRSTNAME, 0, 1)||'.'||k.LASTNAME) customername
,b.prodno
,decode(b.prodtype,0,'Бараа','Үйлчилгээ') as prodtype
,c.name
,b.price
,b.quantity
,b.price as totalamount
,b.discount
,b.salesamount
,a.status
,a.postdate
,a.oldsalesno
,a.salesno
,b.saletype from salesmain_hist dd
left join sales_hist a on dd.salesno=a.salesno
left join salesprod_hist b on dd.salesno=b.salesno
left join servmain c on b.prodno=c.servid
left join customer k on a.custno=k.customerno
where dd.baTchno=:1 and B.PRODTYPE=1 and to_char(a.postdate,'mm/d/yyyy')=to_char(:2,'mm/d/yyyy')";
                paymentsql = @"select spd.seqno,spd.PAYMENTTYPE,ppt.name,spd.contractno,spd.amount,spd.chargeamount,decode(spd.paymentflag,1,'Буцаагдсан') from salespayment_hist sp
left join salespaymentdetail_hist spd on sp.paymentno=spd.paymentno
left join papaytype ppt on spd.paymenttype=ppt.typeid
where sp.salesno=:1";
            }
            #endregion
            Result res = new Result();
            try
            {
                DataSet ds = new DataSet();
                //#region[ PRODUCTDETAILLIST ]
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn500007", ri.ReceivedParam[0]);//, ri.ReceivedParam[2]);
                if (res.ResultNo != 0) return res;

                DataTable dt = res.Data.Tables[0].Copy();
                dt.Clear();
                int index = 0;
                var GENERALSALES = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToStr(x["OLDSALESNO"]) == "").Select(x => x);
                var RETURNSALES = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToStr(x["OLDSALESNO"]) != "").Select(x => x).OrderBy(x => x["OLDSALESNO"]);
                #region[Үндсэн борлуулалтын мэдээлэлүүдээ оруулж байна]
                foreach (DataRow dr in GENERALSALES)
                {
                    dt.Rows.Add(new object[]
                                {
                                    dr["CUSTNO"]
                                    ,dr["CUSTOMERNAME"]
                                    ,dr["PRODNO"]
                                    ,dr["PRODTYPE"]
                                    ,dr["NAME"]
                                    ,dr["PRICE"]
                                    ,dr["QUANTITY"]
                                    ,Static.ToDecimal(dr["TOTALAMOUNT"])*Static.ToInt(dr["QUANTITY"])
                                    ,Static.ToDecimal(dr["DISCOUNT"])*Static.ToInt(dr["QUANTITY"])
                                    ,Static.ToDecimal(dr["SALESAMOUNT"])*Static.ToInt(dr["QUANTITY"])
                                    ,dr["STATUS"]
                                    ,dr["POSTDATE"]
                                    ,dr["OLDSALESNO"]
                                    ,dr["SALESNO"]
                                });
                }
                #endregion
                if (RETURNSALES != null)
                {
                    if (RETURNSALES.Count() != 0)
                    {
                        foreach (DataRow returnsale in RETURNSALES)
                        {
                            string returnsalesno = Static.ToStr(returnsale["OLDSALESNO"]);
                            if (!validate.Contains(returnsalesno))
                            {
                                var ALLSALES = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToStr(x["SALESNO"]) == returnsalesno);
                                if (ALLSALES != null)
                                {
                                    if (ALLSALES.Count() != 0)
                                    {
                                        foreach (DataRow rowsale in ALLSALES)
                                        {
                                            index++;
                                            string salesno = Static.ToStr(rowsale["SALESNO"]);
                                            string prodno = Static.ToStr(rowsale["PRODNO"]);
                                            string prodtype = Static.ToStr(rowsale["PRODTYPE"]);
                                            int saletype = Static.ToInt(rowsale["SALETYPE"]);
                                            int quantity = Static.ToInt(rowsale["QUANTITY"]);
                                            #region[ Бүтэн буцсан бараа ]
                                            var fullreturnprod = RETURNSALES.AsEnumerable().Where(x => Static.ToStr(x["OLDSALESNO"]) == salesno).Where(x => Static.ToStr(x["PRODNO"]) == prodno).Where(x => Static.ToStr(x["PRODTYPE"]) == prodtype).Where(x => Static.ToInt(x["SALETYPE"]) == saletype);
                                            if (fullreturnprod != null)
                                            {
                                                if (fullreturnprod.Count() == 0)
                                                {
                                                    dt.Rows.Add(new object[]
                                                                {
                                                                     rowsale["CUSTNO"]
                                                                    ,rowsale["CUSTOMERNAME"]
                                                                    ,rowsale["PRODNO"]
                                                                    ,rowsale["PRODTYPE"]
                                                                    ,rowsale["NAME"]+" [Буцаалт]"
                                                                    ,rowsale["PRICE"]
                                                                    ,rowsale["QUANTITY"]
                                                                    ,-Static.ToDecimal(rowsale["TOTALAMOUNT"])*Static.ToInt(rowsale["QUANTITY"])
                                                                    ,-Static.ToDecimal(rowsale["DISCOUNT"])*Static.ToInt(rowsale["QUANTITY"])
                                                                    ,-Static.ToDecimal(rowsale["SALESAMOUNT"])*Static.ToInt(rowsale["QUANTITY"])
                                                                    ,2
                                                                    ,rowsale["POSTDATE"]
                                                                    ,rowsale["OLDSALESNO"]
                                                                    ,rowsale["SALESNO"]
                                                                });
                                                }
                                            }
                                            #endregion
                                            var RETURNROWS = RETURNSALES.AsEnumerable().Where(x => Static.ToStr(x["OLDSALESNO"]) == returnsalesno);
                                            foreach (DataRow returnrow in RETURNROWS)
                                            {
                                                string returnprodno = Static.ToStr(returnrow["PRODNO"]);
                                                string returnprodtype = Static.ToStr(returnrow["PRODTYPE"]);
                                                int returnquantity = Static.ToInt(returnrow["QUANTITY"]);
                                                int returnsaletype = Static.ToInt(returnrow["SALETYPE"]);
                                                #region[ Бүтэн нэмэгдсэн бараа ]
                                                if (index == 1)
                                                {
                                                    var addprod = ALLSALES.AsEnumerable().Where(x => Static.ToStr(x["SALESNO"]) == returnsalesno).Where(x => Static.ToStr(x["PRODNO"]) == returnprodno).Where(x => Static.ToStr(x["PRODTYPE"]) == returnprodtype).Where(x => Static.ToInt(x["SALETYPE"]) == returnsaletype);
                                                    if (addprod != null)
                                                    {
                                                        if (addprod.Count() == 0)
                                                        {
                                                            dt.Rows.Add(new object[]
                                                                {
                                                                    returnrow["CUSTNO"]
                                                                    ,returnrow["CUSTOMERNAME"]
                                                                    ,returnrow["PRODNO"]
                                                                    ,returnrow["PRODTYPE"]
                                                                    ,returnrow["NAME"]+" [Нэмэлт]"
                                                                    ,returnrow["PRICE"]
                                                                    ,returnrow["QUANTITY"]
                                                                    ,Static.ToDecimal(returnrow["TOTALAMOUNT"])*Static.ToInt(returnrow["QUANTITY"])
                                                                    ,Static.ToDecimal(returnrow["DISCOUNT"])*Static.ToInt(returnrow["QUANTITY"])
                                                                    ,Static.ToDecimal(returnrow["SALESAMOUNT"])*Static.ToInt(returnrow["QUANTITY"])
                                                                    ,3
                                                                    ,returnrow["POSTDATE"]
                                                                    ,returnrow["OLDSALESNO"]
                                                                    ,returnrow["SALESNO"]
                                                                });
                                                        }
                                                    }
                                                }
                                                #endregion
                                                #region[ Тоо ширхэгээр буцсан болон нэмэгдсэн бараа]
                                                if (returnsalesno == salesno && returnprodno == prodno && returnprodtype == prodtype && returnsaletype == saletype)
                                                {
                                                    int count = returnquantity - quantity;
                                                    #region[Буцаалт]
                                                    if (count < 0)
                                                    {
                                                        dt.Rows.Add(new object[]
                                                                {
                                                                    returnrow["CUSTNO"]
                                                                    ,returnrow["CUSTOMERNAME"]
                                                                    ,returnrow["PRODNO"]
                                                                    ,returnrow["PRODTYPE"]
                                                                    ,returnrow["NAME"]+" [Буцаалт]"
                                                                    ,returnrow["PRICE"]
                                                                    ,-count
                                                                    ,Static.ToDecimal(returnrow["TOTALAMOUNT"])*count
                                                                    ,Static.ToDecimal(returnrow["DISCOUNT"])*count
                                                                    ,Static.ToDecimal(returnrow["SALESAMOUNT"])*count
                                                                    ,2
                                                                    ,returnrow["POSTDATE"]
                                                                    ,returnrow["OLDSALESNO"]
                                                                    ,returnrow["SALESNO"]
                                                                });
                                                    }
                                                    #endregion
                                                    #region[Нэмэлт]
                                                    if (count > 0)
                                                    {
                                                        dt.Rows.Add(new object[]
                                                                {
                                                                    returnrow["CUSTNO"]
                                                                    ,returnrow["CUSTOMERNAME"]
                                                                    ,returnrow["PRODNO"]
                                                                    ,returnrow["PRODTYPE"]
                                                                    ,returnrow["NAME"]+" [Нэмэлт]"
                                                                    ,returnrow["PRICE"]
                                                                    ,count
                                                                    ,Static.ToDecimal(returnrow["TOTALAMOUNT"])*count
                                                                    ,Static.ToDecimal(returnrow["DISCOUNT"])*count
                                                                    ,Static.ToDecimal(returnrow["SALESAMOUNT"])*count
                                                                    ,3
                                                                    ,returnrow["POSTDATE"]
                                                                    ,returnrow["OLDSALESNO"]
                                                                    ,returnrow["SALESNO"]
                                                                });
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                }
                                validate.Add(returnsalesno, 0);
                            }
                        }
                    }
                }
                dt.TableName = "SALESDETAIL";
                ds.Tables.Add(dt.Copy());

                #region[ PAYMENTDETAILLIST ]
                res = db.ExecuteQuery("core", paymentsql, enumCommandType.SELECT, "Txn50002", ri.ReceivedParam[0]);
                if (res.ResultNo != 0) return res;
                if (res.Data != null)
                {
                    int seqno = 0;
                    string paymenttype = null;
                    decimal amount = 0;
                    int status = 0;
                    //DataTable paydata = res.Data.Tables[0].Copy();
                    ds.Tables.Add(res.Data.Tables[0].Copy());
                    //    paydata.Clear();
                    //    var returnpay = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToInt(x["STATUS"]) == 1).Select(x => x);
                    //    var pay = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToInt(x["STATUS"]) == 0).Select(x => x);
                    //    if (returnpay != null)
                    //    {
                    //        if (returnpay.Count() != 0)
                    //        {
                    //            if (pay != null)
                    //            {
                    //                if (pay.Count() != 0)
                    //                {
                    //                    foreach (DataRow payrow in pay.CopyToDataTable().Rows)
                    //                    {
                    //                        seqno = Static.ToInt(payrow["SEQNO"]);
                    //                        paymenttype = Static.ToStr(payrow["PAYMENTTYPE"]);
                    //                        status = Static.ToInt(payrow["STATUS"]);
                    //                        foreach (DataRow returnrow in returnpay.CopyToDataTable().Rows)
                    //                        {

                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {

                    //            }
                    //        }
                    //        else
                    //        {

                    //        }
                    //    }
                }

                #endregion
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
                lg.item.Desc = "Борлуулалтын гүйлгээний дэлгэрэнгүй жагсаалт авах";
            }
        }
        public Result Txn600013(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {                
            Result res = new Result();

            string BatchNo = "";
            string ContractNo = "";
            string SalesNo = "";
            long CustomerNo = 0;

            decimal totalprice = 0;
            decimal totaldiscount = 0;
            decimal totalsalesamount = 0;
            decimal vat = 0;

            object[] sales = new object[15];
            object[] salesmain = new object[4];
            object[] salesprod = new object[8];
            object[] salesrent = new object[12];

            Hashtable customer = new Hashtable();

            ArrayList TagWriteCustomer = new ArrayList();

            try
            {
                DataTable dt = (DataTable)ri.ReceivedParam[0];
                ContractNo = Static.ToStr(ri.ReceivedParam[2]);
                con = db.BeginTransaction("core", "SALES");

                //Тухайн гэрээ буюу бүртгэлийн үлдэгдэл
                //борлуулалт хийж байгаа дүнд хүрэлцэж байна уу үгүй юу гэж шалгаж байна.
                //Шалгахдаа гэрээний балансын төрлийг шалгаж байгаа. 0 - Улайхгүй, 1 - Улайна
                #region [ Contract Validation]
                if (ContractNo != "")
                {
                    string sql = @"select * from contractmain where contractno=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn600013", ContractNo);
                    if (res.Data == null || res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 22222;
                        res.ResultDesc = "Бүртгэлийн мэдээлэл олдсонгүй";
                        return res;
                    }
                    else
                    {
                        if (Static.ToInt(res.Data.Tables[0].Rows[0]["balancetype"]) != 0)
                        {
                            if (Static.ToDecimal(res.Data.Tables[0].Rows[0]["balance"]) - Static.ToDecimal(ri.ReceivedParam[3]) < 0)
                            {
                                res.ResultNo = 22223;
                                res.ResultDesc = string.Format("Бүртгэлийн үлдэгдэл хүрэлцэхгүй байна.({0})", Static.ToDecimal(res.Data.Tables[0].Rows[0]["balance"]));
                                return res;
                            }
                        }
                    }
                }
                #endregion

                //Багцын дугаар авч байна
                #region[ BatchNo ]

                if (ContractNo == "") BatchNo = Static.ToStr(ri.ReceivedParam[5]);
                else BatchNo = Static.ToStr(6);

                if (BatchNo == "")
                {
                    IPos.Core.AutoNumEnum enums = new AutoNumEnum();
                    enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                    Result Batchres = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 6, enums);

                    if (Batchres.ResultNo == 0)
                    {
                        BatchNo = Batchres.ResultDesc;
                        if (BatchNo == "")
                        {
                            Batchres.ResultNo = 9110068;
                            Batchres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:12][" + Batchres.ResultDesc + "]";
                            return Batchres;
                        }
                    }
                    else
                        return Batchres;
                }
                else
                {
                    string sql = @"update sales set status=1 where salesno in (select salesno from salesmain where batchno=:1)";
                    res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn600013", BatchNo);
                    if (res.ResultNo != 0) return res;
                }
                #endregion

                //Нийт барааны жагсаалтаар гүйж байна.
                foreach (DataRow dr in dt.Rows)
                {
                    //Харилцагч тус бүр дээр SalesNo авч байна.
                    #region[ SalesNo ]
                    var query = from row in dt.AsEnumerable()
                                where row.Field<long>("CUSTOMERNO") == Static.ToLong(dr["CUSTOMERNO"])
                                select row;
                    if (!customer.ContainsKey(Static.ToLong(dr["CUSTOMERNO"])))
                    {
                        CustomerNo = Static.ToLong(dr["CUSTOMERNO"]);
                        res = IPos.DB.Main.DB227023(db, Static.ToLong(dr["CUSTOMERNO"]), BatchNo);
                        if (res.ResultNo != 0) return res;
                        customer.Add(Static.ToLong(dr["CUSTOMERNO"]), 0);
                        if (query != null && query.Count() > 0)
                        {
                            IPos.Core.AutoNumEnum enumss = new AutoNumEnum();
                            enumss.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                            Result Salesres = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 5, enumss);

                            if (Salesres.ResultNo == 0)
                            {
                                SalesNo = Salesres.ResultDesc;
                                if (SalesNo == "")
                                {
                                    Salesres.ResultNo = 9110068;
                                    Salesres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:12][" + Salesres.ResultDesc + "]";
                                    return Salesres;
                                }
                            }
                            else
                                return Salesres;
                            sales[0] = SalesNo;
                    #endregion

                            //Үйлчилгээнд хамаарагдаж байгаа бараанд түрээсийн бараа 
                            //байгаа эсэхийг шалгаж байвал salesrent руу бичлэг нэмэх
                            #region[ ServInventory to SalesRent ]
                            var serv = from row in query.CopyToDataTable().AsEnumerable()
                                       where row.Field<Int32>("PRODTYPE") == 1
                                       select row;
                            if (serv != null)
                            {
                                if (serv.Count() != 0)
                                {
                                    foreach (DataRow servrow in serv.CopyToDataTable().Rows)
                                    {

                                        int servquantity = Static.ToInt(servrow["QUANTITY"]);
                                        res = db.ExecuteQuery("core",
                                                                @"select b.invid,b.name,b.status,b.priceamount from servinventory a
                                                                left join invmain  b on a.invid=b.invid
                                                                where a.servid=:1 and b.rentflag<>0"
                                                                , enumCommandType.SELECT
                                                                , "Txn600013"
                                                                , Static.ToStr(servrow["PRODCODE"])
                                                                );
                                        if (res.ResultNo != 0) return res;
                                        if (res.Data != null)
                                        {
                                            foreach (DataRow servinventory in res.Data.Tables[0].Rows)
                                            {
                                                string sql = @"select * from salesrent where salesno=:1 and prodno=:2";
                                                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn600013", SalesNo, servinventory["INVID"]);
                                                if (res.ResultNo != 0) return res;

                                                int itemno = 0;

                                                if (res.Data != null)
                                                    if (res.Data.Tables[0].Rows.Count != 0)
                                                        itemno = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToStr(x["SALESNO"]) == SalesNo).Where(x => Static.ToStr(x["PRODNO"]) == Static.ToStr(servinventory["INVID"])).Max(x => Static.ToInt(x["ITEMNO"]));

                                                for (int i = 0; i < servquantity; i++)
                                                {
                                                    itemno = itemno + 1;
                                                    salesrent[0] = SalesNo;
                                                    salesrent[1] = 0;
                                                    salesrent[2] = servinventory["INVID"];
                                                    salesrent[3] = itemno;
                                                    salesrent[4] = null;
                                                    salesrent[5] = DateTime.MinValue;
                                                    salesrent[6] = DateTime.MinValue;
                                                    salesrent[7] = 0;
                                                    salesrent[8] = null;
                                                    salesrent[9] = "";
                                                    salesrent[10] = null;
                                                    salesrent[11] = null;
                                                    res = IPos.DB.Main.DB227028(db, salesrent);
                                                    if (res.ResultNo != 0) return res;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            //Багц үүсгэж байна.
                            #region[ SalesMain ]
                            salesmain[0] = BatchNo;                             //BatchNo
                            salesmain[1] = sales[0];                            //SalesNO
                            salesmain[2] = Static.ToInt(ri.ReceivedParam[1]);   //IsVat
                            salesmain[3] = ContractNo;                          //ContractNo
                            res = IPos.DB.Main.DB227008(db, salesmain);
                            if (res.ResultNo != 0) return res;
                            #endregion

                            //Борлуулалт дээрх бараа үйлчилгээг бүртгэж байна.
                            //SalesNo,ProdNo,ProdType,Quantity,Price,Discount,SalesAmount,RentStartTime,RentEndTime,RentStatus,DamageType,DamageNote,ReparationNo
                            #region[ SalesProd ]
                            foreach (DataRow drow in query.CopyToDataTable().Rows)
                            {
                                salesprod[0] = sales[0];
                                salesprod[1] = drow["PRODCODE"];
                                salesprod[2] = drow["PRODTYPE"];
                                salesprod[3] = drow["QUANTITY"];
                                salesprod[4] = drow["PRICE"];
                                salesprod[5] = drow["DISCOUNT"];

                                if (Static.ToInt(ri.ReceivedParam[1]) == 1) salesprod[6] = drow["SALESAMOUNT"];
                                else salesprod[6] = Static.ToDecimal(drow["SALESAMOUNT"]) - Static.ToDecimal(drow["SALESAMOUNT"]) / (IPos.Core.SystemProp.VAT + 1);

                                salesprod[7] = drow["FLAG"];
                                totalprice = totalprice + Static.ToDecimal(drow["PRICE"]) * Static.ToDecimal(drow["QUANTITY"]);
                                totaldiscount = totaldiscount + Static.ToDecimal(drow["DISCOUNT"]) * Static.ToDecimal(drow["QUANTITY"]);
                                totalsalesamount = totalsalesamount + Static.ToDecimal(drow["SALESAMOUNT"]) * Static.ToDecimal(drow["QUANTITY"]);
                                vat = vat + Static.ToDecimal(drow["SALESAMOUNT"]) * Static.ToDecimal(drow["QUANTITY"]) / (IPos.Core.SystemProp.VAT + 1);

                                res = IPos.DB.Main.DB227018(db, salesprod);
                                if (res.ResultNo != 0)
                                {
                                    return res;
                                }
                                #region[SalesRent]
                                //Түрээсийн бараа эсэхийг шалгаж байна.
                                if (Static.ToInt(drow["FLAG"]) == 1 || Static.ToInt(drow["FLAG"]) == 2)
                                {
                                    //Борлуулалт дээрх түрээсийн барааг бүртгэж байна.
                                    //SalesNo,ProdType,ProdNo,ItemNo,BarCode,
                                    //RentStartTime,RentEndTime,RentStatus,DamageType,DamageNote,
                                    //ReparationNo,RentOfficer
                                    string sql = @"select * from salesrent where salesno=:1 and prodno=:2";
                                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn600013", SalesNo, drow["PRODCODE"]);
                                    if (res.ResultNo != 0) return res;

                                    int itemno=0;
                                    if (res.Data != null)
                                        if (res.Data.Tables[0].Rows.Count != 0)
                                            itemno = res.Data.Tables[0].AsEnumerable().Where(x => Static.ToStr(x["SALESNO"]) == SalesNo).Where(x => Static.ToStr(x["PRODNO"]) == Static.ToStr(drow["PRODCODE"])).Max(x => Static.ToInt(x["ITEMNO"]));

                                    for (int i = 1; i <= Static.ToInt(drow["QUANTITY"]); i++)
                                    {
                                        salesrent[0] = SalesNo;
                                        salesrent[1] = drow["PRODTYPE"];
                                        salesrent[2] = drow["PRODCODE"];
                                        salesrent[3] = itemno + i;
                                        salesrent[4] = null;
                                        salesrent[5] = DateTime.MinValue;
                                        salesrent[6] = DateTime.MinValue;
                                        salesrent[7] = 0;
                                        salesrent[8] = null;
                                        salesrent[9] = "";
                                        salesrent[10] = null;
                                        salesrent[11] = null;
                                        res = IPos.DB.Main.DB227028(db, salesrent);
                                        if (res.ResultNo != 0) return res;
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            //Борлуулалтаа бүртгэж байна
                            //SalesNo,CustNo,PostDate,TotalAmount,SalesAmount,Discount,Vat,CurCode,PosNo,CashierNo,Ip,Mac,Status
                            #region[ Sales ]
                            sales[1] = dr["CUSTOMERNO"];
                            sales[2] = DateTime.Now;
                            sales[3] = totalprice - totalprice / (IPos.Core.SystemProp.VAT + 1);
                            sales[4] = totalsalesamount;
                            sales[5] = totaldiscount;
                            sales[6] = vat;
                            sales[7] = "MNT";
                            if (ContractNo == "")
                                sales[8] = ri.ReceivedParam[3];
                            else
                                sales[8] = ri.ReceivedParam[4];
                            sales[9] = ri.UserNo;
                            sales[10] = ci.ClientIp;
                            sales[11] = ci.ClientId;
                            sales[12] = 0;
                            if (ContractNo == "")
                                sales[13] = ri.ReceivedParam[4];
                            else
                                sales[13] = ri.ReceivedParam[5];
                            sales[14] = dr["SALESNO"];

                            res = IPos.DB.Main.DB227013(db, sales);
                            if (res.ResultNo != 0) return res;
                            totalsalesamount = 0;
                            totaldiscount = 0;
                            totalprice = 0;
                            vat = 0;
                            #endregion
                        }
                    }
                }
                res = IPos.DB.Panels.DB500024(db, BatchNo);
                if (res.ResultNo != 0) return res;
                object[] prop = new object[2];
                prop[0] = BatchNo;
                prop[1] = SalesNo;
                res.Param = prop;
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Борлуулалтын үндсэн мэдээлэл нэмэх";
                if (res.ResultNo == 0)
                    con.Commit();
                else
                    con.Rollback();
            }
        }
        //public Result Txn600014(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        //{
        //    Result res = new Result();
        //    string BatchNo = Static.ToStr(ri.ReceivedParam[1]);
        //    string SalesNo="";
        //    object[] salesmain=new object[2];
        //    object[] salesprod = new object[7];
        //    object[] sales = new object[13];

        //    try
        //    {
        //        DataTable dt=(DataTable)ri.ReceivedParam[0];
        //        Hashtable customer = new Hashtable();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if (Static.ToStr(dr["SALESNO"]) == "")
        //            {
        //                if (!customer.ContainsKey(Static.ToLong(dr["CUSTOMERNO"])))
        //                {
        //                    IPos.Core.AutoNumEnum enumss = new AutoNumEnum();
        //                    enumss.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
        //                    Result Salesres = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 5, enumss);

        //                    if (Salesres.ResultNo == 0)
        //                    {
        //                        SalesNo = Static.ToStr(Salesres.ResultDesc);
        //                        if (SalesNo == "")
        //                        {
        //                            Salesres.ResultNo = 9110068;
        //                            Salesres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:12][" + Salesres.ResultDesc + "]";
        //                            return Salesres;
        //                        }
        //                    }
        //                    else
        //                        return Salesres;

        //                    salesmain[0] = BatchNo;
        //                    salesmain[1] = SalesNo;
        //                    //SALESMAIN
        //                    res = IPos.DB.Main.DB227008(db, salesmain);
        //                    if (res.ResultNo != 0) return res;
        //                    //CUSTOMERIDDEVICE
        //                    res = IPos.DB.Main.DB227023(db, Static.ToLong(dr["CUSTOMERNO"]), BatchNo);
        //                    if (res.ResultNo != 0) return res;

        //                    customer.Add(Static.ToLong(dr["CUSTOMERNO"]), 0);
        //                    var query = from row in dt.AsEnumerable()
        //                                where row.Field<long>("CUSTOMERNO") == Static.ToLong(dr["CUSTOMERNO"])
        //                                select row;
        //                    if (query != null && query.Count() != 0)
        //                    {
        //                        decimal totalprice = 0;
        //                        decimal totalsalesamount = 0;
        //                        decimal totaldiscount = 0;
        //                        foreach (DataRow drow in query.CopyToDataTable().Rows)
        //                        {
        //                            //Борлуулалт дээрх бараа үйлчилгээг бүртгэж байна.
        //                            //SalesNo,ProdNo,ProdType,Quantity,Price,Discount,SalesAmount,RentStartTime,RentEndTime,RentStatus,DamageType,DamageNote,ReparationNo
        //                            salesprod[0] = SalesNo;
        //                            salesprod[1] = drow["PRODCODE"];
        //                            salesprod[2] = drow["PRODTYPE"];
        //                            salesprod[3] = drow["QUANTITY"];
        //                            salesprod[4] = drow["PRICE"];
        //                            totalprice = totalprice + Static.ToDecimal(drow["PRICE"]);
        //                            salesprod[5] = drow["DISCOUNT"];
        //                            totaldiscount = totaldiscount + Static.ToDecimal(dr["DISCOUNT"]);
        //                            salesprod[6] = drow["SALESAMOUNT"];
        //                            totalsalesamount = totalsalesamount + Static.ToDecimal(drow["SALESAMOUNT"]);
        //                            res = IPos.DB.Main.DB227018(db, salesprod);
        //                            if (res.ResultNo != 0)
        //                            {
        //                                return res;
        //                            }
        //                        }
        //                        //Борлуулалтаа бүртгэж байна
        //                        //SalesNo,CustNo,PostDate,TotalAmount,SalesAmount,Discount,Vat,CurCode,PosNo,CashierNo,Ip,Mac,Status
        //                        sales[0] = SalesNo;
        //                        sales[1] = dr["CUSTOMERNO"];
        //                        sales[2] = DateTime.Now;
        //                        sales[3] = totalprice;
        //                        sales[4] = totalsalesamount;
        //                        sales[5] = totaldiscount;
        //                        sales[6] = 0;
        //                        sales[7] = "MNT";
        //                        sales[8] = ri.UserNo;
        //                        sales[9] = ri.UserNo;
        //                        sales[10] = ci.ClientIp;
        //                        sales[11] = ci.ClientId;
        //                        sales[12] = 0;
        //                        res = IPos.DB.Main.DB227013(db, sales);
        //                        if (res.ResultNo != 0) return res;
        //                    }    
        //                }
        //            }
        //        }
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ResultNo = 9110002;
        //        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

        //        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

        //        return res;
        //    }
        //    finally
        //    {
        //        lg.item.Desc = "Борлуулалтын үндсэн мэдээлэл нэмэх";
        //    }
        //}
        public Result Txn600006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB227021(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Багцын жагсаалт авах";
            }
        }
        public Result Txn600008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                //SalesNo,CustNo,PostDate,TotalAmount,SalesAmount,Discount,Vat,CurCode,PosNo,CashierNo,Ip,Mac,Status
                object[] salesmain = new object[2];
                if (Static.ToInt(ri.ReceivedParam[0]) == 0)
                    salesmain[0] = DateTime.Now.Ticks;
                else
                    salesmain[0] = ri.ReceivedParam[0];

                salesmain[1] = ri.ReceivedParam[1];
                //BatchNo, SalesNo
                res = IPos.DB.Main.DB227008(db, salesmain);
                res.Param = salesmain;
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Багцын шинээр нэмэх";
            }
        }
        public Result Txn600010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                //Борлуулалтаа устгаж байна
                res = IPos.DB.Main.DB227015(db, Static.ToStr(ri.ReceivedParam[1]));
                if (res.ResultNo == 0)
                {
                    //Багцаа устгаж байна
                    res = IPos.DB.Main.DB227010(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToStr(ri.ReceivedParam[1]));
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
                lg.item.Desc = "Багцын шинээр нэмэх";
            }
        }
        public Result Txn600018(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB227018(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Багцын шинээр нэмэх";
            }
        }
        //Борлуулалтаар гарсан бараа материал засварлах
        public Result Txn600019(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[8];
                obj=ri.ReceivedParam;
                res = IPos.DB.Main.DB227019(db, Static.ToStr(obj[0]), Static.ToInt(obj[2]), Static.ToStr(obj[1]), obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Борлуулалтаар гарсан бараа материал засварлах";
            }
        }
        //Борлуулалтаар гарсан бараа материал засварлах
        public Result Txn600020(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB227020(db, Static.ToStr(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Борлуулалтаар гарсан бараа материал засварлах";
            }
        }
        //Борлуулсан багцын төлбөрийн төрлүүдийн жагсаалт авах
        public Result Txn600022(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB227022(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Борлуулсан багцын төлбөрийн төрлүүдийн жагсаалт авах";
            }
        }
        //Борлуулалтын Төлбөрийн Бүртгэл шинээр нэмэх
        public Result Txn600033(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Борлуулалтын Төлбөрийн Бүртгэл шинээр нэмэх";
            }
        }
        //
        public Result Txn90000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql = @"select a.custno,d.lastname,c.prodno,c.prodtype,c.qty,b.name,b.priceamount
from orders a
left join orderproduct c on a.orderno=c.orderno
left join invmain b on c.prodno=b.invid
left join customer d on a.custno=D.CUSTOMERNO
where a.orderno=:1 and prodtype=0
union
select a.custno,d.lastname,c.prodno,c.prodtype,c.qty,b.name,b.priceamount
from orders a
left join orderproduct c on a.orderno=c.orderno
left join servmain b on c.prodno=b.servid
left join customer d on a.custno=D.CUSTOMERNO
where a.orderno=:1 and prodtype=1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "TXN90000", ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Борлуулсан багцын төлбөрийн төрлүүдийн жагсаалт авах";
            }
        }
        //Түрээсийн барааны хайлт
        public Result Txn600041(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB227041(db, ri.ReceivedParam);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Түрээсийн барааны хайлт";
            }
        }
        //
        public Result Txn600042(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                DataTable dt = (DataTable)ri.ReceivedParam[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (Static.ToInt(dr["SELECT"]) == 1 && Static.ToInt(dr["STATUS"]) != 1)
                    {
                        res = IPos.DB.Main.DB227042(db, Static.ToStr(dr["INVID"]), Static.ToStr(dr["BARCODE"]), Static.ToInt(ri.UserNo), DateTime.Now);
                        if (res.ResultNo != 0) return res;
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
                lg.item.Desc = "Түрээсийн барааны хайлт";
            }
        }
        //
        public Result Txn600043(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                DataTable dt = (DataTable)ri.ReceivedParam[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (Static.ToInt(dr["SELECT"]) == 1 && Static.ToInt(dr["STATUS"]) != 0)
                    {
                        res = IPos.DB.Main.DB227043(db, Static.ToStr(dr["INVID"]), Static.ToStr(dr["BARCODE"]));
                        if (res.ResultNo != 0) return res;
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
                lg.item.Desc = "Түрээсийн барааны хайлт";
            }
        }
        //
        public Result Txn600090(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                DataTable dt = new DataTable();
                string sql = @"select a.salesno,d.custno as customerno,b.prodno as prodcode,b.prodtype,c.name as prodname,b.price,b.discount,b.salesamount,b.quantity,C.ISVAT,c.rentflag as flag,'' as edit,'' as remove from salesmain a
left join sales d on a.salesno=d.salesno
left join salesprod b on a.salesno=b.salesno
left join invmain c on b.prodno=c.invid
where batchno=:1 and b.prodtype=0
union
select a.salesno,d.custno as customerno,b.prodno as prodcode,b.prodtype,c.name as prodname,b.price,b.discount,b.salesamount,b.quantity,C.ISVAT,0 as flag,'' as edit,'' as remove from salesmain a
left join sales d on a.salesno=d.salesno
left join salesprod b on a.salesno=b.salesno
left join servmain c on b.prodno=c.servid
where batchno=:1 and b.prodtype=1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "TXN600090", ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Түрээсийн барааны хайлт";
            }
        }
        //
        public Result Txn600091(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                DataTable dt = new DataTable();
                string sql = @"update sales set status=1 where salesno in (select a.salesno from salesmain a
left join sales b on b.salesno=a.salesno
where a.batchno=:1)";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "TXN600091", ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Борлуулалтын буцаалт";
            }
        }
    }
}
