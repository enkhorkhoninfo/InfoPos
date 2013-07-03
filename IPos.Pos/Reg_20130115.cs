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
                #region New autonum for registeration

                IPos.Core.AutoNumEnum enums = new IPos.Core.AutoNumEnum();
                enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 8, enums);
                if (res.ResultNo != 0)
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
                #endregion
                #region Барьцааг pledgedoc тэйбэлд бүртгэх

                pledgeno = res.ResultDesc;
                conn = db.BeginTransaction("core", "Txn601001");

                string sql = @"insert into pledgedoc
(pledgeno,doctype,docno,holddate,holduser,status,custno,custname,contact,memo)
 values(:1,:2,:3,:4,:5,:6,:7,:8,:9,:10)";

                object[] param = new object[] { pledgeno, typeid, docno, DateTime.Now, ri.UserNo, 0, custno, custname, phone, memo };
                res = conn.ExecuteQuery(sql, enumCommandType.INSERT, "Txn601001", param);
                if (res.ResultNo != 0) goto OnExit;
                #endregion
                #region Барьцааны үндсэн харилцагчийг холбоотой үйлчлүүлэгчийн тэйбэлд оруулах
                sql = "insert into pledgemain(custno,pledgeno) values(:1,:2)";
                param = new object[] { custno, pledgeno };
                res = conn.ExecuteQuery(sql, enumCommandType.INSERT, "Txn601001", param);
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
                    if (res != null && res.ResultNo != 0) conn.Commit();
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
                string custname = Static.ToStr(ri.ReceivedParam[3]);
                string phone = Static.ToStr(ri.ReceivedParam[4]);
                string memo = Static.ToStr(ri.ReceivedParam[5]);

                #endregion
                #region Update
                
                    string sql = @"update pledgedoc set
doctype=:2,docno=:3,custno=:4,custname=:5,contact=:6,memo=:7)
where pledgeno=:1
";

                    object[] param = new object[] { pledgeno, typeid, docno, custname, phone, memo };
                    res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn601002", param);

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Барьцаа засах
        public Result Txn601003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

                string docno = Static.ToStr(param[0]);
                string phone = Static.ToStr(param[1]);
                string name = Static.ToStr(param[2]);

                string pledgeno = Static.ToStr(param[3]);
                decimal custno = Static.ToDecimal(param[4]);
                string serialno = Static.ToStr(param[5]);

                int status = Static.ToInt(param[6]);

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1)*/ 
pd.pledgeno,pd.custno,pd.custname,pd.contact
,pt.typename,pd.docno,pd.holduser,pd.holddate
,pd.status,cid.serialno,cid.batchno
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
                    values.Add(9);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("nvl(pd.status,9)=:{0}", values.Count);
                }
                
                if (sb.Length > 0) sb.Insert(0, " where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "Txn601003", ri.PageIndex, ri.PageRows, values.ToArray());
                #endregion
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
                if (res.ResultNo != 0) goto OnExit;

                DataTable d1 = res.Data.Tables[0].Copy();
                d1.TableName = "pledge";
                
                #endregion
                #region Барьцаанд хамаарах үйлчлүүлэгчид
                sql = @"select p.custno,c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,d.serialno,decode(p.custno,pd.custno,1,0) owner
from pledgemain p
left join pledgedoc pd on pd.pledgeno=p.pledgeno
left join customer c on c.customerno=p.custno
left join customeriddevice d on d.custno=p.custno
where p.pledgeno=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601004", param);
                if (res.ResultNo != 0) goto OnExit;

                DataTable d2 = res.Data.Tables[0].Copy();
                d2.TableName = "customers";

                #endregion
                #region Барьцаанд хамаарах үйлчлүүлэгчдийн түрээсийн хэрэгслүүд
                sql = @"select p.pledgeno,p.custno,sr.itemno,sr.prodno,im.name
,decode(sr.damagetype,null,0,0,0,1) fined
,(sr.rentendtime-sr.rentstarttime)*(24*60) rentminutes
,sr.rentstatus
from pledgemain p
inner join pledgedoc pd on pd.pledgeno=p.pledgeno
inner join customeriddevice d on d.custno=p.custno 
left join sales s on s.custno=p.custno
left join salesrent sr on sr.salesno=s.salesno
left join invmain im on im.invid=sr.prodno
where p.pledgeno=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601004", param);
                if (res.ResultNo != 0) goto OnExit;

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
             * 1. Хэрэв харилцагч барьцаанд холбогдсон л бол PledgeMain тэйбэлд бичлэг үүснэ.
             * 2. Мөн CustomerIdDevice тэйбэлд Харилцагчийн бичлэгийг давхар үүсгэнэ. 
             *    Энэ нь харилцагч ерөнхийдөө өөр барьцаанд давхар хамаарч байгаа эсэхийг шалгах, таг холбох зэргийг тэмдэглэнэ.
             * 3. Харилцагчийг барьцаанаас салгах үед CustomerIdDevice тэйблээс бичлэгийг устгана.
             * 
             * Үйлдэл:
             * 1. pledgemain тэйбэлд харилцагчийн нэмэх
             * 2. CustomerIdDevice тэйбэлд уг харилцагчийн дугаараар шинээр харилцагч нэмэх ба өмнө нь бсн эсэхийг шалгана.
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
                decimal custno = Static.ToDecimal(ri.ReceivedParam[1]);

                #endregion

                conn = db.BeginTransaction("core", "Txn601005");

                #region Үйлдэл1

                string sql = @"insert into pledgemain(custno,pledgeno) values(:1,:2)";

                object[] param = new object[] { custno, pledgeno };
                res = conn.ExecuteQuery(sql, enumCommandType.INSERT, "Txn601005", param);
                if (res.ResultNo != 0)
                {
                    if (res.ResultNo == 1)
                    {
                        res.ResultNo = 6010051;
                        res.ResultDesc = string.Format("{0} дугаартай харилцагч холбоотой байна!", custno);
                    }
                    goto OnExit;
                }

                #endregion
                #region Үйлдэл2

                sql = @"insert into customeriddevice(custno) values(:1)";

                param = new object[] { custno, pledgeno };
                res = conn.ExecuteQuery(sql, enumCommandType.INSERT, "Txn601005", param);
                if (res.ResultNo != 0)
                {
                    if (res.ResultNo == 1)
                    {
                        res.ResultNo = 6010052;
                        res.ResultDesc = string.Format("{0} дугаартай харилцагч өөр барьцаанд холбоотой байна!", custno);
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
                    if (res != null && res.ResultNo != 0) conn.Commit();
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
                decimal custno = Static.ToDecimal(ri.ReceivedParam[1]);

                #endregion
                #region SQL

                //Барьцаанд бүртгэгдсэн үндсэн харилцагч мөн эсэхийг шалгах
                //Ийм харилцагчийн холбоосыг устгах боломжгүй байна.
                string sql = @"select pd.custno,cid.serialno 
from pledgemain pm
left join pledgedoc pd on pd.pledgeno=pm.pledgeno
left join customeriddevice cid on cid.custno=pm.custno
where pm.custno=:1 and pm.pledgeno=:2
";
                object[] param = new object[] { custno, pledgeno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601006", param);
                if (res.ResultNo == 0)
                {
                    if (res.AffectedRows > 0)
                    {
                        DataTable dt = res.Data.Tables[0];
                        decimal owner = Static.ToDecimal(dt.Rows[0]["custno"]);
                        string serialno = Static.ToStr(dt.Rows[0]["serialno"]);
                        if (owner == custno)
                        {
                            res = new Result(6010061, "Барьцаанд бүртгэлтэй байгаа үндсэн харилцагчийн холбоосыг устгах боломжгүй!");
                        }
                        else if (!string.IsNullOrEmpty(serialno))
                        {
                            res = new Result(6010062, "Харилцагч дээр таг уясан байна, тагаа салгана уу!");
                        }
                    }
                    else
                    {
                        sql = @"delete from pledgemain where custno=:1 and pledgeno=:2";
                        param = new object[] { custno, pledgeno };
                        res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn601006", param);
                        if (res.AffectedRows <= 0)
                        {
                            res.ResultNo = 6010063;
                            res.ResultDesc = "Бичлэг олдсонгүй!";
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
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
left join customeriddevice d on d.custno=p.custno
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
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(ri.ReceivedParam[0]);
                string tagno = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Шалгалт1

                string sql = @"select cid.custno,cid.serialno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
from customeriddevice cid
left join customer c on c.customerno=cid.custno
where serialno=:1";

                object[] param = new object[] { tagno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601008", param);
                if (res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    DataTable dt = res.Data.Tables[0];
                    decimal owned = Static.ToDecimal(dt.Rows[0]["custno"]);
                    string custname = Static.ToStr(dt.Rows[0]["custname"]);

                    res = new Result(6010081, string.Format("[{0}] дугаартай таг [{1}] {2} хэрэглэгч дээр бүртгэлтэй байна!"
                        , tagno, owned, custname));

                    goto OnExit;
                }

                #endregion
                #region Шалгалт2

                // Тагийн бүртгэлийг шалгах
                // Таг бүрийг сериал дугаараар, төлвийн хамт бүртгэсэн мэдээлэл бх ёстой.
                // Төөгийгөөр хийлгэх.
                // 2013.01.15

                #endregion

                #region Таг холбох

                sql = @"update customeriddevice set serialno=:2 where custno=:1";

                param = new object[] { custno, tagno };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn601008", param);
                if (res.ResultNo != 0) goto OnExit;

                #endregion
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
                #region Таг холбох

                string sql = @"update customeriddevice set serialno=null where custno=:1 and serialno=:2";

                object[] param = new object[] { custno, tagno };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn601009", param);
                if (res.ResultNo != 0) goto OnExit;
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
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(ri.ReceivedParam[0]);
                string tagno = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Таг холбох

                string sql = @"update customeriddevice set serialno=null where custno=:1 and serialno=:2";

                object[] param = new object[] { custno, tagno };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn601009", param);
                if (res.ResultNo != 0) goto OnExit;
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
        }    //Барьцааг чөлөөлөх

        
        #endregion
    }
}
