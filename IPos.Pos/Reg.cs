using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using System.Data;

using IPos.DB;
using IPos.Core;

namespace IPos.Pos
{
    public class Reg : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 601001: 	             //Барьцаа үүсгэх
                        res = Txn601001(ci, ri, db, ref lg);
                        break;
                    case 601002: 	             //Барьцаа засах
                        res = Txn601002(ci, ri, db, ref lg);
                        break;
                    case 601003: 	             //Барьцааны мэдээлэл хайх, харах
                        res = Txn601003(ci, ri, db, ref lg);
                        break;
                    case 601004: 	             //Барьцааны дэлгэрэнгүй мэдээлэл авах
                        res = Txn601004(ci, ri, db, ref lg);
                        break;
                    case 601005: 	             //Барьцаанд харилцагч холбох
                        res = Txn601005(ci, ri, db, ref lg);
                        break;
                    case 601006: 	             //Барьцаанаас харилцагч салгах
                        res = Txn601006(ci, ri, db, ref lg);
                        break;
                    case 601007: 	             //Тагны дугаараар барьцааны дугаар олох
                        res = Txn601007(ci, ri, db, ref lg);
                        break;
                    case 601008: 	             //Таг холбох
                        res = Txn601008(ci, ri, db, ref lg);
                        break;
                    case 601009: 	             //Таг салгах
                        res = Txn601009(ci, ri, db, ref lg);
                        break;
                    case 601010: 	             //Барьцаа чөлөөлөх
                        res = Txn601010(ci, ri, db, ref lg);
                        break;
                    case 601011: 	             //Барьцаанд хамрагдсан үйлчлүүлэгчид
                        res = Txn601011(ci, ri, db, ref lg);
                        break;

                    case 601012:                //Тагын дугаараар барьцаа хайх
                        res = Txn601012(ci, ri, db, ref lg);
                        break;
                    case 601013:                //Барьцааны торгуультай түрээсийн хэрэгслийн төлвийг өөрчлөх
                        res = Txn601013(ci, ri, db, ref lg);
                        break;

                    default:
                        res = new Result(9110009, "Функц тодорхойлогдоогүй байна");
                        break;
                }
                return res;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Функц дуудахад алдаа гарлаа.\r\n" + ex.Message);
            }
            return res;
        }

        #region [ Business functions ]

        public Result PledgeStatusCheck(DbConnections db, string pledgeno, decimal pledgecustno = 0, decimal linkcustno = 0)
        {
            string sql = "";
            object[] param = null;

            if (!string.IsNullOrEmpty(pledgeno))
            {
                sql = @"select status from pledgedoc where pledgeno=:1";
                param = new object[] { pledgeno };
            }
            else if (pledgecustno > 0)
            {
                sql = @"select status from pledgedoc where custno=:1 and rownum=1";
                param = new object[] { pledgecustno };
            }
            else
            {
                sql = "select status from pledgedoc where pledgeno=(select pledgeno from pledgemain where custno=:1 and rownum=1)";
                param = new object[] { linkcustno };
            }

            Result res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601000", param);

            if (res.ResultNo == 0 && res.AffectedRows > 0)
            {
                int status = Static.ToInt(res.Data.Tables[0].Rows[0]["status"]);
                if (status == 1)
                {
                    res.ResultNo = 6010001;
                    res.ResultDesc = "Барьцаа чөлөөлөгдсөн байна!";
                    res.Data = null;
                }
            }
            return res;
        }
        public Result PledgeSearch(DbConnections db, int pageindex, int pagerows, object[] param)
        {
            Result res = null;
            try
            {
                #region Prepare parameters
                //object[] param = ri.ReceivedParam;

                string docno = Static.ToStr(param[0]);
                string phone = Static.ToStr(param[1]);
                string name = Static.ToStr(param[2]);

                string pledgeno = Static.ToStr(param[3]);
                decimal custno = Static.ToDecimal(param[4]);
                string serialno = Static.ToStr(param[5]);

                int status = Static.ToInt(param[6]);

                string tagframeno = Static.ToStr(param[7]);

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1)*/ 
pd.pledgeno,pd.custno,pd.custname,pd.contact
,pt.typename,pd.docno,pd.holduser,pd.holddate
,pd.status,cid.serialno,cid.batchno,pd.doctype,pd.memo
from pledgedoc pd
left join papledgetype pt on pt.typeid=pd.doctype
left join customeriddevice cid on cid.custno=pd.custno
left join customer c on c.customerno=pd.custno
{0}
order by pd.holddate desc, pd.custname
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (0 < custno)
                {
                    values.Add(custno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("pd.custno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(pledgeno))
                {
                    values.Add(pledgeno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("pd.pledgeno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(phone))
                {
                    values.Add(phone);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("pd.contact like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(name))
                {
                    values.Add(name);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("pd.custname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(docno))
                {
                    values.Add(docno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("pd.docno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(serialno))
                {
                    values.Add(serialno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("cid.serialno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(tagframeno))
                {
                    values.Add(tagframeno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("cid.tagframeno=:{0}", values.Count);
                }

                if (status == 0)
                {
                    // Барьцаа өгсөн, өгөөгүй бүх харилцагчдийг харуулах.
                    // энэ үед шүүлт бхгүй бн.
                }
                else if (status == 1)
                {
                    // Зөвхөн барьцаа өгсөн харилцагчдийг
                    values.Add(0);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("pd.status=:{0}", values.Count);
                }
                else if (status == 2)
                {
                    // Зөвхөн барьцаа өгөөгүй харилцагчдийг
                    values.Add(1);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("pd.status=:{0}", values.Count);
                }

                if (sb.Length > 0) sb.Insert(0, " where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "Txn601003", pageindex, pagerows, values.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Барьцаа хайж жагсаалт авах
        
        public Result Txn601001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DbConnection conn = null;
            try
            {
                #region Prepare parameters

                string pledgeno = Static.ToStr(ri.ReceivedParam[0]);
                int typeid = Static.ToInt(ri.ReceivedParam[1]);
                string docno = Static.ToStr(ri.ReceivedParam[2]);
                decimal custno = Static.ToDecimal(ri.ReceivedParam[3]);
                string custname = Static.ToStr(ri.ReceivedParam[4]);
                string phone = Static.ToStr(ri.ReceivedParam[5]);
                string memo = Static.ToStr(ri.ReceivedParam[6]);

                #endregion
                #region Шалгалт1

                string sql = @"select p.pledgeno, p.custname, p.contact from pledgedoc p where p.custno=:1 and status=0";
                object[] param = new object[] { custno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601001", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    DataTable dt = res.Data.Tables[0];
                    pledgeno = Static.ToStr(dt.Rows[0]["pledgeno"]);
                    custname = Static.ToStr(dt.Rows[0]["custname"]);

                    res.ResultNo = 6010011;
                    res.ResultDesc = string.Format("[{0}] харилцагч [{1}-{2}] барьцаан дээр холбоотой байна! ", custno, pledgeno, custname);
                    res.Data = null;
                    goto OnExit;
                }

                #endregion
                #region Барьцааны шинэ дугаарлалт авах

                IPos.Core.AutoNumEnum enums = new IPos.Core.AutoNumEnum();
                enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 8, enums);
                if (res != null && res.ResultNo != 0)
                {
                    if (string.IsNullOrEmpty(res.ResultDesc))
                    {
                        res.ResultNo = 9110068;
                        res.ResultDesc = "Автомат дугаарлалтын хөрвүүлэлт дээр алдаа гарлаа. [ID:12][";
                        res.Param = null;
                        res.Data = null;
                    }
                    goto OnExit;
                }
                pledgeno = res.ResultDesc;

                #endregion
                #region Барьцааг pledgedoc тэйбэлд бүртгэх

                conn = db.BeginTransaction("core", "Txn601001");

                sql = @"insert into pledgedoc
(pledgeno,doctype,docno,holddate,holduser,status,custno,custname,contact,memo)
 values(:1,:2,:3,:4,:5,:6,:7,:8,:9,:10)";

                param = new object[] { pledgeno, typeid, docno, DateTime.Now, ri.UserNo, 0, custno, custname, phone, memo };
                res = conn.ExecuteQuery(sql, enumCommandType.INSERT, "Txn601001", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                #endregion
                #region Харилцагчийг pledgemain тэйбэлд бүртгэх

                sql = @"insert into pledgemain (custno,pledgeno) values(:1,:2)";
                param = new object[] { custno, pledgeno };
                res = conn.ExecuteQuery(sql, enumCommandType.INSERT, "Txn601001", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region Барьцааны үндсэн харилцагчийг холбоотой үйлчлүүлэгчийн тэйбэлд оруулах
                ////sql = "insert into pledgemain(custno,pledgeno) values(:1,:2)";
                //sql = "insert into customeriddevice(custno,parentno) values(:1,:2)";
                //param = new object[] { custno, custno };
                //res = conn.ExecuteQuery(sql, enumCommandType.INSERT, "Txn601001", param);
                //if (res.ResultNo == 1)
                //{
                //    //Хэрэв өмнө нь бичлэг үүссэн бол шууд ашиглана.
                //    res.ResultNo = 0;
                //    res.ResultDesc = "";
                //}

                if (res.ResultNo == 0) res.ResultDesc = pledgeno;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                if (conn != null)
                    if (res != null && res.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
            }
        OnExit:
            return res;
        }    //Барьцаа шинээр үүсгэх
        public Result Txn601002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string pledgeno = Static.ToStr(ri.ReceivedParam[0]);
                int typeid = Static.ToInt(ri.ReceivedParam[1]);
                string docno = Static.ToStr(ri.ReceivedParam[2]);
                decimal custno = Static.ToDecimal(ri.ReceivedParam[3]);
                string custname = Static.ToStr(ri.ReceivedParam[4]);
                string phone = Static.ToStr(ri.ReceivedParam[5]);
                string memo = Static.ToStr(ri.ReceivedParam[6]);

                #endregion
                #region Шалгалт1 - Төлөв шалгах

                res = PledgeStatusCheck(db, pledgeno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region Update

                string sql = @"update pledgedoc set doctype=:2,docno=:3,custno=:4,custname=:5,contact=:6,memo=:7 where pledgeno=:1";

                object[] param = new object[] { pledgeno, typeid, docno, custno, custname, phone, memo };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn601002", param);

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
        OnExit:
            return res;
        }    //Барьцаа засах
        public Result Txn601003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                res = PledgeSearch(db, ri.PageIndex, ri.PageRows, ri.ReceivedParam);
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Барьцаа хайж жагсаалт авах
        public Result Txn601004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string pledgeno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Барьцааны үндсэн мэдээлэл авах

                string sql = @"select pledgeno,doctype,docno,holddate,holduser,unholddate,unholduser
,status,custno,custname,contact,memo from pledgedoc where pledgeno=:1";

                object[] param = new object[] { pledgeno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601004", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                DataTable d1 = res.Data.Tables[0].Copy();
                d1.TableName = "pledge";
                
                #endregion
                #region Барьцаанд хамаарах үйлчлүүлэгчид
                sql = @"select p.custno,c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,d.serialno,decode(p.custno,pd.custno,1,0) owner
from pledgemain p
left join pledgedoc pd on pd.pledgeno=p.pledgeno
left join customeriddevice d on d.custno=p.custno
left join customer c on c.customerno=p.custno
where p.pledgeno=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601004", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                DataTable d2 = res.Data.Tables[0].Copy();
                d2.TableName = "customers";

                #endregion
                #region Барьцаанд хамаарах үйлчлүүлэгчдийн түрээсийн хэрэгслүүд
                sql = @"select p.pledgeno,p.custno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,sr.itemno,sr.prodno,im.name
,case when nvl(sr.rentstatus,0)=2 and
 (round((sr.rentendtime-sr.rentstarttime)*(24*60),2) > 0 or sr.damagetype is not null)
 then 1 else 0 end fined
,round((sr.rentendtime-sr.rentstarttime)*(24*60),2) rentminutes
,case when sr.rentstatus=0 then N'ОЛГООГҮЙ'
      when sr.rentstatus=1 then N'ОЛГОСОН'
      when sr.rentstatus=2 then 
            case when sr.damagetype is not null then dt.name
                 when round((sr.rentendtime-sr.rentstarttime)*24*60,2) > 0 then N'ТОРГУУЛЬ'
                 else N'БУЦААГДСАН' end 
      when sr.rentstatus=3 then N'АНХААРУУЛАВ'
      when sr.rentstatus=4 then N'ТӨЛӨГДСӨН ['||sr.losspaymentno||']'
      else N'UNKNOWN' end rentstatus
,sr.salesno,sr.damagenote,sr.rentstatus status
from pledgemain p
left join customeriddevice d on d.custno=p.custno
inner join sales s on s.custno=p.custno
left join salesrent sr on sr.salesno=s.salesno and sr.prodtype=0
left join invmain im on im.invid=sr.prodno
left join customer c on c.customerno=sr.custno
left join padamagetype dt on dt.damagetype=sr.damagetype
where p.pledgeno=:1 and sr.prodno is not null
order by 3,5";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601004", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                DataTable d3 = res.Data.Tables[0].Copy();
                d3.TableName = "rents";

                #endregion

                DataSet ds = new DataSet();
                ds.Tables.AddRange(new DataTable[] { d1, d2, d3 });

                res.Data = ds;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            OnExit:
            return res;
        }    //Барьцааны хамаарах түрээсийн хэрэгслүүд

        public Result Txn601005(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            #region Тооцоолол хийх дүрмийг заавал унш! бас бөглө!
            /**************************************************************
             *               ТООЦООЛОЛ ХИЙХ ДҮРЭМ
             * Шалгалт:
             * 1. Харилцагч барьцаанд холбогдсон л бол CustomerIdDevice тэйбэлд бичлэг үүснэ.
             * 2. Харилцагчийг барьцаанаас салгах үед CustomerIdDevice тэйблээс бичлэгийг устгана.
             * 
             * Үйлдэл:
             * 1. CustomerIdDevice тэйбэлд харилцагчийн холбоос бичлэг нэмэх. CustNo
             * 
             * 
             **************************************************************/
            #endregion

            Result res = null;
            DbConnection conn = null;
            try
            {
                #region Prepare parameters

                string pledgeno = Static.ToStr(ri.ReceivedParam[0]);
                decimal pledgecustno = Static.ToDecimal(ri.ReceivedParam[1]);
                decimal custno = Static.ToDecimal(ri.ReceivedParam[2]);

                #endregion

                #region Шалгалт1 - Төлөв шалгах

                res = PledgeStatusCheck(db, pledgeno, pledgecustno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region Шалгалт2 - Харилцагч өөр этгээдэд холбогдсон эсэх

                string sql = @"select p.custno,p.pledgeno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
from pledgemain p
left join customer c on c.customerno=p.custno
where p.custno=:1";

                object[] param = new object[] { custno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601005", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    DataTable dt = res.Data.Tables[0];
                    decimal parentno = Static.ToDecimal(dt.Rows[0]["CUSTNO"]);
                    string custname = Static.ToStr(dt.Rows[0]["CUSTNAME"]);
                    string serialno = Static.ToStr(dt.Rows[0]["PLEDGENO"]);

                    res.ResultNo = 6010051;
                    res.ResultDesc = string.Format("[{0}-{1}] харилцагч [{2}] дугаартай барьцаан дээр холбоотой байна! ", custno, custname, pledgeno);
                    res.Data = null;

                    goto OnExit;
                }


                #endregion

                conn = db.BeginTransaction("core", "Txn601005");
                
                #region Үйлдэл1

                //sql = @"insert into customeriddevice(custno, parentno) values(:1, :2)";
                sql = @"insert into pledgemain(custno, pledgeno) values(:1, :2)";

                param = new object[] { custno, pledgeno };
                res = conn.ExecuteQuery(sql, enumCommandType.INSERT, "Txn601005", param);
                if (res != null && res.ResultNo != 0)
                {
                    if (res.ResultNo == 1)
                    {
                        res.ResultNo = 6010052;
                        res.ResultDesc = string.Format("{0} дугаартай харилцагч өөр харилцагч дээр холбоотой байна!", custno);
                        res.Data = null;
                    }
                    goto OnExit;
                }

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                if (conn != null)
                    if (res != null && res.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
            }
            OnExit:
            return res;
        }    //Барьцаанд үйлчлүүлэгч холбох
        public Result Txn601006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string pledgeno = Static.ToStr(ri.ReceivedParam[0]);
                decimal pledgecustno = Static.ToDecimal(ri.ReceivedParam[1]);
                decimal custno = Static.ToDecimal(ri.ReceivedParam[2]);

                #endregion
                #region Шалгалт1 - Төлөв шалгах

                res = PledgeStatusCheck(db, null, pledgecustno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region Шалгалт2 - Барьцааны Үндсэн харилцагч эсэх

                string sql = @"select custno from pledgedoc where pledgeno=:1 and custno=:2";
                object[] param = new object[] { pledgeno, custno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601006", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    res = new Result(6010061, "Барьцаанд бүртгэлтэй байгаа үндсэн харилцагчийн холбоосыг салгах боломжгүй!");
                    goto OnExit;
                }

                #endregion
                #region Шалгалт3 - Таг уягдсан эсэх

                sql = @"select serialno from customeriddevice where custno=:1";
                param = new object[] { custno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601006", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    string serialno = Static.ToStr(res.Data.Tables[0].Rows[0]["SERIALNO"]);
                    if (!string.IsNullOrEmpty(serialno))
                    {
                        res = new Result(6010062, "Харилцагч дээр таг уясан байна, тагаа салгана уу!");
                        goto OnExit;
                    }
                }

                #endregion

                #region Үйлдэл1

                sql = @"delete from pledgemain where custno=:1 and pledgeno=:2";
                param = new object[] { custno, pledgeno };
                res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn601006", param);
                if (res.AffectedRows <= 0)
                {
                    res.ResultNo = 6010063;
                    res.ResultDesc = "Бичлэг олдсонгүй!";
                }

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            OnExit:
            return res;
        }    //Барьцаанаас үйлчлүүлэгч салгах

        public Result Txn601007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string tagno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region SQL

                string sql = @"select p.pledgeno,p.custno,d.serialno from pledgemain p
inner join customeriddevice d on d.custno=p.custno
where d.serialno=:1";

                object[] param = new object[] { tagno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601007", param);

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Тагны дугаараар барьцааны дугаар олох
        public Result Txn601008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            #region Тооцоолол хийх дүрмийг заавал унш! бас бөглө!
            /**************************************************************
             *               ТООЦООЛОЛ ХИЙХ ДҮРЭМ
             * Шалгалт:
             * 1. Тагийн дугаар нь CustomerIdDevice тэйбэлд ямар нэг харилцагч дээр уягдаагүй байх.
             * 2. Уях гэж буй тагийн дугаар нь Хэвийн төлөвтэй байх. Хаягдсан, гээгдсэн, уягдсан гм.
             * 
             * 
             **************************************************************/
            #endregion

            Result res = null;
            DataTable dt = null;
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(ri.ReceivedParam[0]);
                string tagno = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Шалгалт1 - Барьцааны Төлөв шалгах

                res = PledgeStatusCheck(db, null, 0, custno );
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region Шалгалт4 - Үйлчлүүлэгч дээр өөр таг холбогдсон эсэх

                string sql = @"select cid.custno,cid.serialno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
from customeriddevice cid
left join customer c on c.customerno=cid.custno
where cid.custno=:1";

                object[] param = new object[] { custno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601008", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    dt = res.Data.Tables[0];
                    string custname = Static.ToStr(dt.Rows[0]["custname"]);
                    string serialno = Static.ToStr(dt.Rows[0]["serialno"]);

                    if (!string.IsNullOrEmpty(serialno))
                    {
                        if (serialno != tagno)
                        {
                            res = new Result(6010081, string.Format("[{0}] үйлчлүүлэгч дээр [{1}] дугаартай таг бүртгэлтэй байна!"
                                , custname, serialno));
                        }
                        else
                        {
                            res.Data = null;
                        }
                        goto OnExit;
                    }
                }

                #endregion
                #region Шалгалт2 - Таг өөр үйлчлүүлэгч дээр холбогдсон эсэх

                sql = @"select cid.custno,cid.serialno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
from customeriddevice cid
left join customer c on c.customerno=cid.custno
where serialno=:1";

                decimal owned = 0;
                param = new object[] { tagno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601008", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    dt = res.Data.Tables[0];
                    owned = Static.ToDecimal(dt.Rows[0]["custno"]);
                    string custname = Static.ToStr(dt.Rows[0]["custname"]);
                    string serialno = Static.ToStr(dt.Rows[0]["serialno"]);

                    //if (!string.IsNullOrEmpty(serialno) && serialno != tagno)
                    {
                        res = new Result(6010081, string.Format("[{0}] дугаартай таг [{1}] {2} хэрэглэгч дээр бүртгэлтэй байна!"
                            , serialno, owned, custname));
                        goto OnExit;
                    }
                }

                #endregion
                #region Шалгалт3 - Тагийн төлөв шалгах

                // Тагийн бүртгэлийг шалгах
                // Таг бүрийг сериал дугаараар, төлвийн хамт бүртгэсэн мэдээлэл бх ёстой.
                // Төөгийгөөр хийлгэх.
                // 2013.01.15

                sql = @"select tagtype,status,frameno from tagmain where tagid=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601008", tagno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows <= 0)
                {
                    res = new Result(6010081, string.Format("[{0}] дугаартай таг бүртгэгдээгүй байна!", tagno));
                    goto OnExit;
                }

                dt = res.Data.Tables[0];
                int tagstatus = Static.ToInt(dt.Rows[0]["STATUS"]);
                string tagframeno = Static.ToStr(dt.Rows[0]["FRAMENO"]);

                if (tagstatus != 1)
                {
                    res = new Result(6010081, string.Format("[{0}] дугаартай таг хэвийн төлөвтэй таг биш байна!\r\nТөлөв={1}", tagno, tagstatus));
                    goto OnExit;
                }

                #endregion

                #region Таг холбох

                    sql = @"merge into customeriddevice a
using (select :1 custno, :2 serialno, :3 tagframeno from dual) b
on (a.custno=b.custno)
when matched then update set a.serialno=b.serialno, a.tagframeno=b.tagframeno
when not matched then insert (custno,serialno,tagframeno) values (b.custno,b.serialno,b.tagframeno)";

                param = new object[] { custno, tagno, tagframeno };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn601008", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion

                res.Data = null;
                res.Param = new object[] { tagframeno };
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            OnExit:
            return res;
        }    //Таг холбох
        public Result Txn601009(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(ri.ReceivedParam[0]);
                string tagno = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Таг салгах

                //string sql = @"update customeriddevice set serialno=null where custno=:1 and serialno=:2";
                string sql = @"delete customeriddevice where custno=:1 and serialno=:2";

                object[] param = new object[] { custno, tagno };
                res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn601009", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows <= 0)
                {
                    res = new Result(6010091, string.Format("[{0}] дугаартай тагийн мэдээлэл олдсонгүй!"
                        , tagno));
                }

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            OnExit:
            return res;
        }    //Таг салгах

        public Result Txn601010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            #region Тооцоолол хийх дүрмийг заавал унш! бас бөглө!
            /**************************************************************
             *               ТООЦООЛОЛ ХИЙХ ДҮРЭМ
             * Шалгалт:
             * 1. Холбоотой харилцагчид дотор таг уягдсан бол чөлөөлөхгүй. PledgeMain, CustomerIdDevice-ээс шалгана.
             * 2. Түрээсийн хэрэгслүүд бүгд буцаагдсан, эсвэл төлбөр хийгдсэн бх
             * 3. Борлуулалтын Төлбөр нь бүрэн хийгдсэн бх.
             * 
             * Үйлдэл:
             * 1. pledgedoc тэйбэлд төлвийг чөлөөлсөн болгох. Status=1
             * 2. CustomerIdDevice тэйбэлд үүссэн холбоотой харилцагчдийг цэвэрлэх.
             * 3. pledgemain тэйбэлд бга холбоостой харилцагчдийг цэвэрлэх.
             * 
             * 
             **************************************************************/
            #endregion

            Result res = null;
            DbConnection conn = null;
            string sql = null;
            DataTable dtSales = null;
            try
            {
                #region Prepare parameters
                string pledgeno = Static.ToStr(ri.ReceivedParam[0]);
                #endregion

                #region Шалгалт1 - Төлөв шалгах

                res = PledgeStatusCheck(db, pledgeno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region Шалгалт2 - Таг холбоотойг шалгах

                sql = @"select count(p.custno) tagcount
from pledgemain p
left join customeriddevice d on d.custno=p.custno
where p.pledgeno=:1 and d.serialno is not null";
                object[] param = new object[] { pledgeno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601010", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                DataTable dt = res.Data.Tables[0];
                int tagcount = Static.ToInt(dt.Rows[0]["tagcount"]);
                if (tagcount > 0)
                {
                    res.ResultNo = 6010101;
                    res.ResultDesc = string.Format("Бүртгэлд {0} ширхэг таг холбоотой үлдсэн байна!", tagcount);
                    res.Data = null;
                    goto OnExit;
                }

                #endregion

                #region Борлуулалтын дугааруудыг олох

                sql = @"select distinct s.salesno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname 
from sales s
left join customer c on c.customerno=s.custno
where custno in (
select custno from pledgedoc where pledgeno=:1
union all
select custno from pledgemain where pledgeno=:1 
)";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601010", pledgeno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                if (res.AffectedRows > 0) dtSales = res.Data.Tables[0];

                #endregion
                
                #region Шалгалт3 - Борлуулалтын төлбөр бүрэн төлөгдсөн эсэх

                if (dtSales != null)
                {
                    Sales CSales = new Sales();

                    foreach (DataRow r in dtSales.Rows)
                    {
                        string salesno = Static.ToStr(r["SALESNO"]);
                        string custname = Static.ToStr(r["CUSTNAME"]);
                        res = CSales.GetPaymentBalance(db, salesno);
                        if (res.ResultNo != 0) goto OnExit;

                        if (res.AffectedRows > 0)
                        {
                            dt = res.Data.Tables[0];
                            decimal ta = Static.ToDecimal(dt.Rows[0]["TOTALAMOUNT"]);
                            decimal pa = Static.ToDecimal(dt.Rows[0]["PAID"]);

                            if (ta > 0 && ta > pa)
                            {
                                res = new Result(6010102, string.Format("[{0}] Борлуулалтын төлбөр бүрэн төлөгдөөгүй байна.\r\nҮйлчлүүлэгч={1}\r\nҮлдэгдэл={2}", salesno, custname, ta - pa));
                                goto OnExit;
                            }
                        }
                    }
                }
                #endregion
                #region Шалгалт4 - Түрээсийн торгууль төлөгдсөн эсэх

                sql = @"select * from (
    select p.pledgeno,p.custno,sr.salesno,sr.rentstatus status,sr.itemno,sr.prodno,im.name
    ,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
    ,case when nvl(sr.rentstatus,0)=2 and
       ((nvl(sr.servicetime,0)>0 and round((sr.rentendtime-sr.rentstarttime)*(24*60),2)-nvl(sr.servicetime,0)>0)
       or sr.damagetype is not null)
     then 1 else 0 end fined
    ,case when nvl(sr.servicetime,0)>0 and (sr.rentendtime-sr.rentstarttime)*(24*60)-nvl(sr.servicetime,0)>0 then
     round((sr.rentendtime-sr.rentstarttime)*(24*60)-nvl(sr.servicetime,0),2) else 0 end rentminutes
    from pledgemain p
    left join customeriddevice d on d.custno=p.custno
    inner join sales s on s.custno=p.custno
    left join salesrent sr on sr.salesno=s.salesno
    left join invmain im on im.invid=sr.prodno
    left join customer c on c.customerno=sr.custno
    where p.pledgeno=:1
) a where a.fined=1 or a.status=1";
                param = new object[] { pledgeno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601010", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    res = new Result(6010103, "Түрээсийн хэрэгсэлийг бүрэн хүлээж аваагүй эсвэл торгууль төлөгдөөгүй байна.");
                    goto OnExit;
                }                

                #endregion
                
                #region Холбогдсон харилцагчдийн бичлэгийг устгах

                conn = db.BeginTransaction("core", "Txn601010");

                sql = @"delete from pledgemain where pledgeno=:1";

                param = new object[] { pledgeno };
                res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn601010", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                if (res.AffectedRows <= 0)
                {
                    //res = new Result(6010091, string.Format("[{0}] дугаартай тагийн мэдээлэл олдсонгүй!", tagno));
                }

                #endregion
                #region Барьцааны бичлэгийн төлвийг чөлөөлсөн төлөвт оруулах

                sql = @"update pledgedoc set status=1,unholduser=:2,unholddate=:3 where pledgeno=:1";

                param = new object[] { pledgeno, ri.UserNo, DateTime.Now };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn601010", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                if (conn != null)
                    if (res != null && res.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
            }
        OnExit:
            return res;
        }    //Барьцааг чөлөөлөх
        
        public Result Txn601011(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string pledgeno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Барьцаанд хамаарах үйлчлүүлэгчид
                string sql = @"select p.custno,c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,d.serialno,decode(p.custno,pd.custno,1,0) owner,c.birthday
from pledgemain p
left join pledgedoc pd on pd.pledgeno=p.pledgeno
left join customeriddevice d on d.custno=p.custno
left join customer c on c.customerno=p.custno
where p.pledgeno=:1";
                object[] param = new object[] { pledgeno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601004", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
        OnExit:
            return res;
        }    //Барьцаанд хамрагдсан үйлчлүүлэгчид
        public Result Txn601012(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                string tagno = Static.ToStr(ri.ReceivedParam[0]);

                object[] param = new object[]{
                    "" /*docno*/
                    ,"" /*phoneno*/
                    ,"" /*name*/
                    ,"" /*pledgeno*/
                    ,0 /*custno*/
                    ,tagno /*tagno*/
                    ,1 /*1-барьцаа өгсөн үйлчлүүлэгчид*/
                    , "" /*tag frameno*/
                };
                res = PledgeSearch(db, 0, 1, param);
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Тагын дугаараар барьцаа хайх
        public Result Txn601013(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string salesno = Static.ToStr(ri.ReceivedParam[0]);
                decimal custno = Static.ToDecimal(ri.ReceivedParam[1]);
                string prodno = Static.ToStr(ri.ReceivedParam[2]);
                int prodtype = Static.ToInt(ri.ReceivedParam[3]);
                int itemno = Static.ToInt(ri.ReceivedParam[4]);
                int rentstatus = Static.ToInt(ri.ReceivedParam[5]); // 3-Анхааруулав, 4-Төлөгдсөн гэсэн 2 төлөв л орж ирнэ.
                string losspaymentno = Static.ToStr(ri.ReceivedParam[6]);

                #endregion
                #region Түрээсийн хэрэгслийн төлөв өөрчлөх.
                string sql = @"update salesrent set rentstatus=:6, losspaymentno=:7
where salesno=:1 and custno=:2 and prodno=:3 and prodtype=:4 and itemno=:5";
                object[] param = new object[] { salesno, custno, prodno, prodtype, itemno, rentstatus, losspaymentno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601013", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
        OnExit:
            return res;
        }    //Барьцааны торгуультай түрээсийн хэрэгслийн төлвийг өөрчлөх
        
        #endregion
    }
}
