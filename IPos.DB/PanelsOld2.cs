using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Text;

using EServ.Data;
using EServ.Shared;


namespace IPos.DB
{
    public class Panels
    {
        /// <summary>
        /// Борлуулалтын хайлт.
        /// Дараах утгууд орж ирнэ.
        /// string salesno, string tagno, string custno, string reg, string fname, string lname, string cname
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500001(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                string salesno = Static.ToStr(param[0]);
                string tagno = Static.ToStr(param[1]);
                string custno = Static.ToStr(param[2]);
                string reg = Static.ToStr(param[3]);
                string fname = Static.ToStr(param[4]);
                string lname = Static.ToStr(param[5]);
                string cname = Static.ToStr(param[6]);
                #endregion
                #region Prepare query
                string sql = @"
select  /*+ first_rows(1) */
sm.batchno,s.salesno
, decode(c.classcode,0,c.lastname,c.corporatename) lastname
, decode(c.classcode,0,c.firstname,'') firstname
, c.registerno
, s.salesamount
, s.postdate
, k.serialno
, s.custno
from sales s
left join salesmain sm on sm.salesno=s.salesno
left join customer c on c.customerno=s.custno
left join customeriddevice k on s.custno=k.custno and sm.batchno=k.batchno
{0}
order by postdate desc,lastname
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(salesno))
                {
                    values.Add(salesno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("k.salesno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(tagno))
                {
                    values.Add(tagno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("k.serialno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("k.custno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(reg))
                {
                    values.Add(reg);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.registerno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(fname))
                {
                    values.Add(fname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.firstname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(lname))
                {
                    values.Add(lname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.lastname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(cname))
                {
                    values.Add(cname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.corporatename like :{0}||'%'", values.Count);
                }

                if (sb.Length > 0) sb.Insert(0, "where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "DB500001", pagenumber, pagecount, values.ToArray());
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }

        /// <summary>
        /// Харилцагч хайх.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500002(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                string custno = Static.ToStr(param[0]);
                string contract = Static.ToStr(param[1]);
                string reg = Static.ToStr(param[2]);
                string fname = Static.ToStr(param[3]);
                string lname = Static.ToStr(param[4]);
                string cname = Static.ToStr(param[5]);

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1) */
c.customerno
, decode(c.classcode,0,c.lastname,c.corporatename) lastname
, decode(c.classcode,0,c.firstname,'') firstname
, c.registerno
, c.membercontractno
, C.CLASSCODE
, c.sex
from customer c
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.customerno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(reg))
                {
                    values.Add(reg);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.registerno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(fname))
                {
                    values.Add(fname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.firstname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(lname))
                {
                    values.Add(lname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.lastname=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(cname))
                {
                    values.Add(cname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.corporatename like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(contract))
                {
                    values.Add(contract);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.membercontractno=:{0}", values.Count);
                }

                if (sb.Length > 0) sb.Insert(0, "where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "DB500002", pagenumber, pagecount, values.ToArray());
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }

        /// <summary>
        /// Өгөгдсөн борлуулалтийн дугаар дээр бүртгэлтэй түрээсийн хэрэгслийн жагсаалт
        /// 
        /// ucRentList панелаас дуудагдаж байгаа.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">salesno</param>
        /// <returns></returns>
        public static Result DB500003(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                //Түрээсийн форм дээр ашиглагдана.
                string salesno = Static.ToStr(param[0]);

                // Барьцааны форм дээр дараах 2 хайлт ашиглагдана.
                string custno = Static.ToStr(param[1]);
                string batchno = Static.ToStr(param[2]);

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1) */
sr.salesno,sr.prodno,sr.itemno
,i.name,sr.barcode
,decode(sr.damagetype,null,0,0,0,1) fined
,sr.rentstarttime
,sr.rentendtime
,sr.rentstatus
from sales s,salesmain sm,customeriddevice cid,salesrent sr
left join invmain i on i.invid=sr.prodno
where s.salesno=sm.salesno and sr.salesno=sm.salesno 
and s.custno=cid.custno and sm.batchno=cid.batchno
and sr.rentstatus>0
{0}
";
                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(salesno))
                {
                    values.Add(salesno);
                    //if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat(" and s.salesno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    //if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat(" and s.custno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(batchno))
                {
                    values.Add(batchno);
                    //if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat(" and cid.batchno=:{0}", values.Count);
                }

                //if (sb.Length > 0) sb.Insert(0, "where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "DB500003", pagenumber, pagecount, values.ToArray());
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        
        /// <summary>
        /// Өгөгдсөн salesno дугаар дээр борлуулсан барааны жагсаалт
        /// 
        /// ucSaleProdList панелаас дуудагдаж байгаа.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">salesno</param>
        /// <returns></returns>
        public static Result DB500004(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1) */
 sp.prodno
 ,decode(sp.prodtype,0,im.name,sm.name) name
 ,quantity,price,discount,salesamount,0 vat
 ,decode(sp.prodtype,0,im.salesaccountno,sm.salesaccountno) salesaccountno
 ,decode(sp.prodtype,0,im.refundaccountno,sm.refundaccountno) refundaccountno
 ,decode(sp.prodtype,0,im.discountaccountno,sm.discountaccountno) discountaccountno
 ,decode(sp.prodtype,0,im.bonusaccountno,sm.bonusaccountno) bonusaccountno
from salesprod sp
left join invmain im on im.invid=sp.prodno and sp.prodtype=0
left join servmain sm on sm.servid=sp.prodno and sp.prodtype=1
where sp.salesno=:1
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "DB500004", pagenumber, pagecount, param);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }

        /// <summary>
        /// Өгөгдсөн борлуулалтаар олгосон нийт тагийн жагсаалт
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">salesno</param>
        /// <returns></returns>
        public static Result DB500005(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                string salesno = Static.ToStr(param[0]);
                string serialno = Static.ToStr(param[1]);
                string custno = Static.ToStr(param[2]);

                #endregion
                #region Prepare query
                string sql = @"
select  /*+ first_rows(1) */
sm.batchno,s.salesno
, k.serialno
, decode(c.classcode,0,c.lastname,c.corporatename) lastname
, decode(c.classcode,0,c.firstname,'') firstname
, c.registerno
from sales s
left join salesmain sm on sm.salesno=s.salesno
left join customer c on c.customerno=s.custno
left join customeriddevice k on s.custno=k.custno and sm.batchno=k.batchno
{0}
";

                #endregion

                #region Prepare filter

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(salesno))
                {
                    values.Add(salesno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("s.salesno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(serialno))
                {
                    values.Add(serialno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("k.serialno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("k.custno=:{0}", values.Count);
                }

                if (sb.Length > 0) sb.Insert(0, "where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "DB500005", pagenumber, pagecount, values.ToArray());
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }


        /// <summary>
        /// Өгөгдсөн salesno борлуулалтын төлбөрийн нийт дүнг гаргах.
        /// Нэг мөр бичлэг буцаана.
        /// 
        /// ucPayment панелаас дуудагдана.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">salesno</param>
        /// <returns></returns>
        public static Result DB500007(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1) */
sm.batchno
, sum(s.totalamount) totalamount
, sum(s.salesamount) salesamount
, sum(s.discount) discount
, sum(s.vat) vat
, sum(sp.paid) paid
, max(sp.seqno) seqno
from sales s, salesmain sm
left join (
    select sp.salesno, sum(amount) paid, max(spd.seqno) seqno
    from salespayment sp
    left join salespaymentdetail spd on spd.paymentno=sp.paymentno
    group by sp.salesno
) sp on sp.salesno=sm.salesno
where s.salesno=sm.salesno and sm.batchno=:1
group by sm.batchno
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "DB500007", pagenumber, pagecount, param);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        /// <summary>
        /// Өгөгдсөн paytype төлбөрийн төрлийн suspaccount дансны дугаарыг олох
        /// res.Param[0] дээр дансны дугаар.
        /// 
        /// ucPayment панелаас дуудагдана.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">paytype</param>
        /// <returns></returns>
        public static Result DB500007_get_susp(DbConnections db, object[] param)
        {
            Result res = new Result();
            string susp = "";
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1) */
suspaccount from papaytype where typeid=:1
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB500007", param);
                if (res.ResultNo == 0 && res.AffectedRows > 0)
                {
                    susp = Static.ToStr(res.Data.Tables[0].Rows[0][0]);
                }

                #endregion

                res.Data.Dispose();
                res.Data = null;
                res.Param = new object[] { susp };

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }

        /// <summary>
        /// Өгөгдсөн userno хэрэглэгчийн төлбөрийн төрлийн жагсаалтыг гаргах.
        /// Төлбөрийн төрлөөр олон мөр бичлэг буцаана.
        /// 
        /// ucPayment панелаас дуудагдана.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">userno</param>
        /// <returns></returns>
        public static Result DB500008(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1) */
pt.typeid,pt.name,pt.suspaccount,0 paid
from papaytype pt
inner join papaytypeuser pu on pu.typeid=pt.typeid
where pu.userno=:1
order by pt.orderno
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "DB500008", pagenumber, pagecount, param);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }

        #region Pledge Related

        /// <summary>
        /// Барьцааны жагсаалт, хайлт.
        /// 
        /// ucPledgeSearch панелаас дуудагдана.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500009(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(param[0]);
                string pledgeno = Static.ToStr(param[1]);
                string reg = Static.ToStr(param[2]);
                string fname = Static.ToStr(param[3]);
                string lname = Static.ToStr(param[4]);
                string cname = Static.ToStr(param[5]);
                string docno = Static.ToStr(param[6]);
                int status = Static.ToInt(param[7]);
                string serialno = Static.ToStr(param[8]);
                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1)*/ 
c.customerno custno
, decode(c.classcode,0,c.lastname,c.corporatename) lastname
, decode(c.classcode,0,c.firstname,'') firstname
, c.registerno
, pm.pledgeno
, pt.typename
, pd.docno
, pd.holduser
, pd.holddate
, pd.status
, cid.serialno
, cid.batchno
from customer c
left join pledgemain pm on c.customerno=pm.custno and pm.status=0
left join pledgedoc pd on pd.pledgeno=pm.pledgeno --and pd.status=0
left join papledgetype pt on pt.typeid=pd.doctype
left join customeriddevice cid on cid.custno=c.customerno
{0}
order by 2,3,1
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (0<custno)
                {
                    values.Add(custno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.customerno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(pledgeno))
                {
                    values.Add(pledgeno);
                    if (sb.Length > 0) sb.Append(" and "); 
                    sb.AppendFormat("pm.pledgeno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(reg))
                {
                    values.Add(reg);
                    if (sb.Length > 0) sb.Append(" and "); 
                    sb.AppendFormat("c.registerno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(fname))
                {
                    values.Add(fname);
                    if (sb.Length > 0) sb.Append(" and "); 
                    sb.AppendFormat("c.firstname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(lname))
                {
                    values.Add(lname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.lastname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(cname))
                {
                    values.Add(cname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.corporatename like :{0}||'%'", values.Count);
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
                    // Зөвхөн барьцаа өгсөн харилцагчдийг
                    values.Add(0);
                    if (sb.Length > 0) sb.Append(" and "); 
                    sb.AppendFormat("pd.status=:{0}", values.Count);
                }
                if (status == 1)
                {
                    // Зөвхөн барьцаа өгөөгүй харилцагчдийг
                    values.Add(9);
                    if (sb.Length > 0) sb.Append(" and "); 
                    sb.AppendFormat("nvl(pd.status,9)=:{0}",values.Count);
                }
                if (status == 2)
                {
                    // Барьцаа өгсөн, өгөөгүй бүх харилцагчдийг харуулах.
                    // энэ үед шүүлт бхгүй бн.
                }

                if (sb.Length > 0) sb.Insert(0, "where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "DB500009", pagenumber, pagecount, values.ToArray());
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        /// <summary>
        /// Өгөгдсөн custno, pledgeno дугаар дээрх барьцаалсан эд зүйлийн жагсаалт.
        /// 
        /// ucPledgeList панелаас дуудагдана.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">custno,pledgeno</param>
        /// <returns></returns>
        public static Result DB500010(DbConnections db, int pagenumber, int pagecount, object[] param)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(param[0]);
                string pledgeno = Static.ToStr(param[1]);

                #endregion
                #region Prepare query

                string sql1 = @"
select /*+ first_rows(1)*/
custno,pledgeno,note,createdate
from pledgemain pm
where custno=:1 and pledgeno=:2
";

                string sql2 = @"
select /*+ first_rows(1)*/
pd.rowid
, pd.pledgeno
, pd.doctype
, pt.typename
, pd.docno
, pd.holduser
, pd.holddate
, pd.unholduser
, pd.unholddate
, pd.status
from pledgedoc pd
left join papledgetype pt on pt.typeid=pd.doctype
where pledgeno=:1
";

                #endregion
                #region Execute query
                DataSet ds = new DataSet();
                res = db.ExecuteQuery("core", sql1, "DB500010", pagenumber, pagecount, custno, pledgeno);
                if (res.ResultNo == 0)
                {
                    res.Data.Tables[0].TableName = "pledgemain";
                    ds.Tables.Add(res.Data.Tables[0].Copy());

                    res = db.ExecuteQuery("core", sql2, "DB500010", pagenumber, pagecount, pledgeno);
                    if (res.ResultNo == 0)
                    {
                        res.Data.Tables[0].TableName = "pledgedoc";
                        ds.Tables.Add(res.Data.Tables[0].Copy());
                    }
                }
                #endregion

                res.Data.Dispose();
                res.Data = ds;

            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res = new Result();
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            return res;
        }
        /// <summary>
        /// Барьцаалсан бичиг баримтыг олгох
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500012(DbConnections db, object[] param)
        {
            DbConnection conn = null;
            Result res = null;
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(param[0]);
                string pledgeno = Static.ToStr(param[1]);
                string rowid = Static.ToStr(param[2]);
                int userno = Static.ToInt(param[3]);

                #endregion
                #region Prepare query
                string sql1 = @"
update pledgedoc set status=1,unholduser=:2,unholddate=sysdate
where rowid=:1
";
                string sql2 = @"
update pledgemain pm set status =
(
    select min(pd.status) status
    from pledgedoc pd
    where pd.pledgeno=:2
)
where pm.custno=:1 and pm.pledgeno=:2
";
                #endregion
                #region Execute query
                conn = db.BeginTransaction("core", "DB500012");
                res = db.ExecuteQuery("core", sql1, enumCommandType.UPDATE, "DB500012", rowid, userno);
                if (res.ResultNo == 0)
                {
                    res = db.ExecuteQuery("core", sql2, enumCommandType.UPDATE, "DB500012", custno, pledgeno);
                }
                #endregion
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res = new Result();
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            finally
            {
                if (conn != null)
                    if (res.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
            }
            return res;
        }
        /// <summary>
        /// Бичиг баримт барьцаалах
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"> custno, pledgeno, UserNo, docno, doctype</param>
        /// <returns></returns>
        public static Result DB500011(DbConnections db, object[] param)
        {
            DbConnection conn = null;
            Result res = null;
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(param[0]);
                string pledgeno = Static.ToStr(param[1]);
                int userno = Static.ToInt(param[2]);
                string docno = Static.ToStr(param[3]);
                int doctype = Static.ToInt(param[4]);

                #endregion

                

                #region Prepare query
                string sql1 = @"
insert into pledgedoc(pledgeno,docno,doctype,holduser,holddate,status) 
values(:1,:2,:3,:4,sysdate,0)
";
                string sql2 = @"
merge into pledgemain a
using (select :1 custno,:2 pledgeno from dual) b
on (a.custno=b.custno and a.pledgeno=b.pledgeno)
when matched then update set a.status=0
when not matched then insert (custno,pledgeno,createdate,status) values(b.custno,b.pledgeno,sysdate,0)
";
                #endregion
                #region Execute query
                conn = db.BeginTransaction("core", "DB500011");
                res = db.ExecuteQuery("core", sql1, enumCommandType.UPDATE, "DB500011", pledgeno,docno,doctype,userno);
                if (res.ResultNo == 0)
                {
                    res = db.ExecuteQuery("core", sql2, enumCommandType.UPDATE, "DB500011", custno, pledgeno);
                }
                #endregion
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res = new Result();
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            finally
            {
                if (conn != null)
                    if (res.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
            }
            return res;
        }
        /// <summary>
        /// Барьцаа дээр тайлбар оруулах
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">custno, pledgeno</param>
        /// <returns></returns>
        public static Result DB500013(DbConnections db, object[] param)
        {
            DbConnection conn = null;
            Result res = null;
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(param[0]);
                string pledgeno = Static.ToStr(param[1]);
                string note = Static.ToStr(param[2]);

                #endregion
                
                #region Prepare query
                string sql = @"
update pledgemain set note=:3 where custno=:1 and pledgeno=:2
";
                
                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB500013", custno, pledgeno, note);
                #endregion
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res = new Result();
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            return res;
        }

        #endregion

        #region Tag Related

        /// <summary>
        /// Харилцагч дээр таг уях
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500020(DbConnections db, object[] param)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(param[0]);
                string serialno = Static.ToStr(param[1]);

                #endregion
                
                #region Prepare query
                string sql = @"
merge into customeriddevice a
using (select :1 custno,:2 serialno from dual) b
on (a.custno=b.custno)
when matched then update set a.serialno=b.serialno
when not matched then insert (custno,serialno) values(b.custno,b.serialno)
";
                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB500020", custno, serialno);
                #endregion
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res = new Result();
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            return res;
        }
        /// <summary>
        /// Харилцагч дээр уягдсан тагийг салгах
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500021(DbConnections db, object[] param)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                decimal custno = Static.ToDecimal(param[0]);
                //string serialno = Static.ToStr(param[1]);

                #endregion

                #region Prepare query
                string sql = @"
update customeriddevice set serialno=null where custno=:1
";
                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB500021", custno);
                #endregion
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res = new Result();
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            return res;
        }

        #endregion

        #region Rent Related

        /// <summary>
        /// Түрээс дээр шаардлагатай Харилцагчийн мэдээлэл
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500100(DbConnections db, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                string custno = Static.ToStr(param[0]);
                #endregion
                #region Prepare query
                string sql = @"
select customerno 
,sex,membertype
,membercontractno
,cr.name ratename
,height,foot
,mobile,telephone,homephone
,status
from customer c
left join custrate cr on cr.ratecode=c.ratecode
where customerno=:1";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB500100", custno);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }

        /// <summary>
        /// Түрээсийн барааг олгох
        /// salesno,prodno,itemno,barcode,rentstarttime,rentstatus
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500101(DbConnections db, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                string salesno = Static.ToStr(param[0]);
                string serialno = Static.ToStr(param[1]);
                string prodno = Static.ToStr(param[2]);

                string barcode = Static.ToStr(param[3]);
                DateTime beg = Static.ToDateTime(param[4]);
                int status = Static.ToInt(param[5]);

                #endregion
                #region Prepare query
                string sql = @"
update salesrent set barcode=:4,rentstarttime=:5,rentstatus=:6
where salesno=:1 and prodno=:2 and itemno=:3
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB500101", param);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }

        /// <summary>
        /// Түрээсийн барааг хүлээн авах
        /// salesno,prodno,itemno,rentendtime,rentstatus,damagetype,damagenote
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Result DB500102(DbConnections db, object[] param)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                string salesno = Static.ToStr(param[0]);
                string serialno = Static.ToStr(param[1]);
                string prodno = Static.ToStr(param[2]);

                DateTime end = Static.ToDateTime(param[3]);
                int status = Static.ToInt(param[4]);
                int finetype = Static.ToInt(param[5]);
                string finenote = Static.ToStr(param[6]);

                #endregion
                #region Prepare query
                string sql = @"
update salesrent set rentendtime=:4
,rentstatus=:5,damagetype=:6,damagenote=:7
where salesno=:1 and prodno=:2 and itemno=:3
";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB500102", param);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }

        #endregion

        #region Payment Related

        /// <summary>
        /// Өгөгдсөн batchno дээрх борлуулалтын үндсэн бичлэгүүдийг харилцагчийн мэдээлэлтэй хамт авах
        /// </summary>
        /// <param name="db"></param>
        /// <param name="param">batchno</param>
        /// <returns></returns>
        public static Result DB500200(DbConnections db, string batchno)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                //string batchno = Static.ToStr(param[0]);
                #endregion
                #region Prepare query
                string sql = @"
select 
sm.batchno,s.salesno,s.custno
,decode(c.classcode,0
    ,c.lastname||decode(c.firstname,null,'','.'||substr(c.firstname,1,1))
    ,c.corporatename) custname
,c.registerno
,s.totalamount,s.salesamount,s.discount,s.vat,s.postdate
,p.paid
,pic.attachblob
from sales s
left join salesmain sm on sm.salesno=s.salesno
left join customer c on c.customerno=s.custno
left join (
    select sp.salesno, sum(amount) paid
    from salespayment sp
    left join salespaymentdetail spd on spd.paymentno=sp.paymentno
    group by sp.salesno
) p on p.salesno=s.salesno
left join (
    select cp.customerno,a.attachblob
    from customerpic cp,attach a 
    where a.attachid=cp.attachid and picturetype=0
) pic on pic.customerno=s.custno
where batchno=:1
order by 2";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB500200", batchno);
                #endregion
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            return res;
        }
        /// <summary>
        /// Өгөгдсөн batchno дээрх борлуулалтын үндсэн бичлэгүүдийн дүнг авах
        /// </summary>
        /// <param name="db"></param>
        /// <param name="param">batchno</param>
        /// <returns></returns>
        public static Result DB500201(DbConnections db, string batchno)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                //string batchno = Static.ToStr(param[0]);
                #endregion
                #region Prepare query
                string sql = @"
select 
sm.batchno,s.salesno,s.salesamount
,nvl(p.paid,0) paid, nvl(p.seqno,0) seqno
from sales s
left join salesmain sm on sm.salesno=s.salesno
left join (
    select sp.salesno, sum(amount) paid, max(spd.seqno) seqno
    from salespayment sp
    left join salespaymentdetail spd on spd.paymentno=sp.paymentno
    group by sp.salesno
) p on p.salesno=s.salesno
where batchno=:1
order by 2";

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB500201", batchno);
                #endregion
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            return res;
        }
        /// <summary>
        /// Борлуулалтын төлбөрийн бичлэг оруулах
        /// ucPayment панелаас дуудаж байгаа.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">salesno,billno,seqno,paytype,payamount,accountno</param>
        /// <returns></returns>
        public static Result DB500202(DbConnections db, string payno, int seqno, string paytype, decimal amount)
        {
            Result res = null;
            try
            {
                #region Prepare parameters
                
                #endregion
                #region Prepare query
                string sql = @"
insert into salespaymentdetail (paymentno,seqno,paymenttype,amount)
values (:1,:2,:3,:4)
";
                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB500202", payno, seqno, paytype, amount);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result();
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            }
            return res;
        }
        /// <summary>
        /// Борлуулалтын төлбөрийн бичлэг оруулах
        /// ucPayment панелаас дуудаж байгаа.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pagenumber"></param>
        /// <param name="pagecount"></param>
        /// <param name="param">salesno,paymentno</param>
        /// <returns></returns>
        public static Result DB500203(DbConnections db, string salesno, string payno)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                #endregion
                #region Prepare query
                string sql = @"
insert into salespayment (salesno,paymentno)
values (:1,:2)
";
                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB500203", salesno, payno);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result();
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            }
            return res;
        }

        #endregion
    }
}
