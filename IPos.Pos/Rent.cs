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
    public class Rent : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 606001: 	             //Борлуулалтын жагсаалт хайлт
                        res = Txn606001(ci, ri, db, ref lg);
                        break;
                    case 606002: 	             //Таг дээрх түрээсийн хэрэгслийн жагсаалт
                        res = Txn606002(ci, ri, db, ref lg);
                        break;
                    case 606003: 	             //Тагийн хэрэглэгчийн мэдээлэл авах
                        res = Txn606003(ci, ri, db, ref lg);
                        break;
                    case 606004: 	             //Борлуулалтын мэдээлэл хайх
                        res = Txn606004(ci, ri, db, ref lg);
                        break;
                    case 606005: 	             //Түрээсийн ажилтан нэвтрэх
                        res = Txn606005(ci, ri, db, ref lg);
                        break;

                    case 606006: 	             //Түрээсийн хэрэгсэл олгох
                        res = Txn606006(ci, ri, db, ref lg);
                        break;
                    case 606007: 	             //Түрээсийн хэрэгсэл хүлээн авах
                        res = Txn606007(ci, ri, db, ref lg);
                        break;
                    case 606008: 	             //Хүлээн авсан түрээсийн хэрэгсэлд тайлбар оруулах
                        res = Txn606008(ci, ri, db, ref lg);
                        break;
                    case 606009: 	             //Эвдрэлийн мэдээлэл
                        res = Txn606009(ci, ri, db, ref lg);
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

        public Result Txn606001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters

                string salesno = Static.ToStr(ri.ReceivedParam[0]);
                string serialno = Static.ToStr(ri.ReceivedParam[1]);
                //string tagframeno = Static.ToStr(ri.ReceivedParam[2]);

                #endregion
                #region Prepare sql
                string sql = @"select 
cid.custno
,cid.serialno
,sr.itemno
,sr.prodno
,im.name
,sr.barcode
,decode(sr.damagetype,null,0,0,0,1) fined
,SR.RENTSTARTTIME,SR.RENTENDTIME
,SR.RENTSTATUS
,s.salesno
,sr.deliveruser
,sr.receiveruser
,sr.deliveruserstate
,sr.receiveruserstate
from customeriddevice cid
left join sales s on s.custno=cid.custno
left join salesrent sr on sr.salesno=s.salesno
left join invmain im on im.invid=sr.prodno
where sr.prodno is not null {0} order by sr.tagreaddate
";
                #endregion
                #region Prepare filter
                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(salesno))
                {
                    values.Add(salesno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("and s.salesno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(serialno))
                {
                    if (sb.Length > 0) sb.Append(" and ");
                    values.Add(serialno);
                    sb.AppendFormat("and cid.serialno in ({0})", serialno);
                }
                sql = string.Format(sql, sb.ToString());
                #endregion
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606001", values.ToArray());
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
            return res;
        }    //Түрээсийн лавлагаа
        public Result Txn606002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string serialno = Static.ToStr(ri.ReceivedParam[0]);
                //string salesno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Prepare sql
                string sql = @"select sr.itemno,sr.prodno,im.name prodname,sr.barcode
,decode(sr.rentstatus,0,'ОЛГООГҮЙ',1,'ОЛГОСОН','АВСАН') statusname
,decode(sr.rentstatus
,2,case when nvl(sr.servicetime,0)>0 and trunc((sr.rentendtime-sr.rentstarttime)*1440)-nvl(sr.servicetime,0)>0 then N'ТОРГУУЛЬ' else dt.name end
,3,N'САНУУЛСАН',4,N'ТӨЛСӨН',dt.name) damagetype
,case when nvl(sr.servicetime,0)>0 and trunc((sr.rentendtime-sr.rentstarttime)*1440)-nvl(sr.servicetime,0)>0 then
 trunc((sr.rentendtime-sr.rentstarttime)*1440)-nvl(sr.servicetime,0) else 0 end duration
,sr.rentstarttime,sr.rentendtime
,sr.deliveruserarea
,u1.userlname deliveruser
,sr.receiveruserarea
,u2.userlname receiveruser
,sr.tagreaddate,sr.salesno,sr.custno,sr.rentstatus
,decode(sr.rentstatus,0,0,1,sr.deliveruser,sr.receiveruser) userno
from salesrent sr
inner join customeriddevice cid on cid.custno=sr.custno
left join invmain im on im.invid=sr.prodno
left join padamagetype dt on dt.damagetype=sr.damagetype
left join hpuser u1 on u1.userno=sr.deliveruser
left join hpuser u2 on u2.userno=sr.receiveruser
where cid.serialno=:1 and prodtype=0";
                //SalesRent дээр ProdType=1 буюу тагын цагтай үйлчилгээ давхар орж ирнэ.
                //Үүнийг нь түрээсийн хэрэгслийн жагсаалтаас хасаж харуулах.
                #endregion
                object[] param = new object[] { serialno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606002", param);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа.\r\n" + ex.Message);
            }
            return res;
        }    //Тагтай түрээсийн хэрэгслийн лавлагаа
        public Result Txn606003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string serialno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Тагийн төлвийн шалгах

                //Хүчингүй, танигдахгүй, эсвэл хураагдсан таг эсэхийг шалгах.

                #endregion
                #region Prepare sql
                string sql = @"select cid.custno,cid.serialno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,c.sex,c.membertype,cr.name ratename,c.mobile,c.telephone,c.homephone,c.height,c.foot
from customeriddevice cid
left join customer c on c.customerno=cid.custno
left join custrate cr on cr.ratecode=c.ratecode
where cid.serialno=:1";
                #endregion
                object[] param = new object[] { serialno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606003", param);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа.\r\n" + ex.Message);
            }
            return res;
        }    //Тагтай үйлчлүүлэгчдийн лавлагаа
        public Result Txn606004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                object[] param = ri.ReceivedParam;
                string salesno = Static.ToStr(param[0]);
                string tagno = Static.ToStr(param[1]);
                string custno = Static.ToStr(param[2]);
                string regno = Static.ToStr(param[3]);
                string custname1 = Static.ToStr(param[4]);
                string custname2 = Static.ToStr(param[5]);
                string corpname = Static.ToStr(param[6]);
                DateTime postdate = Static.ToDate(param[7]);
                string tagframeno = Static.ToStr(param[8]);

                #endregion
                #region Prepare sql
                string sql = @"select s.salesno,sr.custno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,c.registerno,cid.serialno,sr.prodcount,ss.salesamount,sp.payment,ss.trandate,cid.tagframeno
from sales s
left join (
    select ss1.salesno,sum(salesamount) salesamount
    ,max(sa1.trandate) trandate
    from salesprod ss1
    left join salesaction sa1 on sa1.actionid=ss1.actionid
    group by ss1.salesno
) ss on ss.salesno=s.salesno
left join (
    select salesno,custno,count(prodno) prodcount
    from salesrent
    group by salesno,custno
) sr on sr.salesno=s.salesno
left join (
    select salesno,sum(amount) payment 
    from salestxn group by salesno
) sp on sp.salesno=s.salesno
left join customeriddevice cid on cid.custno=sr.custno
inner join customer c on c.customerno=sr.custno
{0}
order by 1 desc,3";
                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (postdate != DateTime.MinValue)
                {
                    values.Add(postdate);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("ss.trandate=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(salesno))
                {
                    values.Add(salesno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("s.salesno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(tagno))
                {
                    values.Add(tagno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("cid.serialno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(tagframeno))
                {
                    values.Add(tagframeno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("cid.tagframeno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("sr.custno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(custname1))
                {
                    values.Add(custname1);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.firstname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(custname2))
                {
                    values.Add(custname2);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.lastname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(corpname))
                {
                    values.Add(corpname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.corporatename like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(regno))
                {
                    values.Add(corpname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.registerno like :{0}||'%'", values.Count);
                }

                if (sb.Length > 0) sb.Insert(0, " where ");
                sql = string.Format(sql, sb.ToString());

                #endregion

                res = db.ExecuteQuery("core", sql, "Txn606004", ri.PageIndex, ri.PageRows, values.ToArray());
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа.\r\n" + ex.Message);
            }
            return res;
        }    //Борлуулалтын мэдээлэл хайх

        public Result Txn606005(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameter

                int userno = Static.ToInt(ri.ReceivedParam[0]);
                string pwd = Static.Encrypt(Static.ToStr(ri.ReceivedParam[1]));

                #endregion
                #region Execute
                string sql = @"select userno
,decode(userfname,null,userlname,substr(userfname,0,1)||'.'||userlname) username
,upassword from hpuser where userno=:1 and upassword=:2";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606005", userno, pwd);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows <= 0)
                {
                    res = new Result(1000, "Хэрэглэгчийн нэр эсвэл нууц үг буруу байна.");
                }
                
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                lg.item.Desc = "Түрээсийн ажилтан системд нэвтрэх";
            }
            OnExit:
            return res;
        }    //Түрээсийн ажилтан нэвтрэх

        public Result Txn606006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameter

                string salesno = Static.ToStr(ri.ReceivedParam[0]);
                decimal custno = Static.ToDecimal(ri.ReceivedParam[1]);
                int itemno = Static.ToInt(ri.ReceivedParam[2]);
                string prodno = Static.ToStr(ri.ReceivedParam[3]);
                string barcode = Static.ToStr(ri.ReceivedParam[4]);

                int userno = Static.ToInt(ri.ReceivedParam[5]);
                int userstate = Static.ToInt(ri.ReceivedParam[6]);

                string sql = "";

                #endregion

                #region Баркодтой бараа эсхийг шалгах

                bool hasbarcode = false;

                sql = @"select invid from invseries where invid=:1 and rownum=1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606006", prodno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    hasbarcode = true;
                }

                #endregion

                #region Баркод нь бүртгэлтэй эсэхийг шалгах

                if (hasbarcode)
                {
                    //Хэрэв баркод орж ирвэл бүртгэлтэй эсхийг шалгах.
                    if (string.IsNullOrEmpty(barcode))
                    {
                        res = new Result(6060061, string.Format("Баркодтой түрээсийн хэрэгсэл байна, баркодоо оруулна уу."));
                        goto OnExit;
                    }

                    sql = "select invid from invseries where barcode=:1 and status>0";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606006", barcode);
                    if (res != null && res.ResultNo != 0) goto OnExit;
                    if (res.AffectedRows <= 0)
                    {
                        res = new Result(6060061, string.Format("[{0}] Бүртгэлд ороогүй баркод байна.", barcode));
                        goto OnExit;
                    }

                    string invid = Static.ToStr(res.Data.Tables[0].Rows[0]["INVID"]);
                    if (invid != prodno)
                    {
                        res = new Result(6060061, string.Format("Хэрэгсэлд бүртгэсэн баркод биш байна.", barcode));
                        goto OnExit;
                    }

                }

                #endregion
                #region Өмнө нь олгосон хэрэгслийн баркод мөн эсэх

                sql = @"select sr.salesno,sr.custno,sr.prodno
,(
    select name from invmain im, invseries ser 
    where im.invid=ser.invid and ser.invid=sr.prodno
    and ser.barcode=sr.barcode
) invname
from salesrent sr
where sr.barcode=:1 and sr.rentstatus=1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606006", barcode);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    string cust = Static.ToStr(res.Data.Tables[0].Rows[0]["CUSTNO"]);
                    string invname = Static.ToStr(res.Data.Tables[0].Rows[0]["INVNAME"]);
                    res = new Result(6060072, string.Format("[{0}-{1}] Дугаартай хэрэгслийг [{2}] үйлчлүүлэгчид олгосон байна.", barcode, invname, custno));
                    goto OnExit;
                }

                #endregion

                #region Шалгалт4 - Дахин олгох үед Түрээсийн торгууль төлөгдсөн эсэх

                sql = @"
select * from (
    select sr.salesno,sr.rentstatus,sr.itemno,sr.prodno
    ,case when nvl(sr.rentstatus,0)=2 and
       ((nvl(sr.servicetime,0)>0 and round((sr.rentendtime-sr.rentstarttime)*(24*60),2)-nvl(sr.servicetime,0)>0)
       or sr.damagetype is not null)
     then 1 else 0 end fined
    ,case when nvl(sr.servicetime,0)>0 and (sr.rentendtime-sr.rentstarttime)*(24*60)-nvl(sr.servicetime,0)>0 then
     round((sr.rentendtime-sr.rentstarttime)*(24*60)-nvl(sr.servicetime,0),2) else 0 end rentminutes
    from salesrent sr
    where salesno=:1 and custno=:2 and itemno=:3 and prodno=:4
) a where a.fined=1
";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601010", salesno, custno, itemno, prodno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    //DataTable dt = res.Data.Tables[0];
                    //int rentstatus = Static.ToInt(dt.Rows[0]["RENTSTATUS"]);
                    //int fined = Static.ToInt(dt.Rows[0]["FINED"]);
                    res = new Result(6010103, "Түрээсийн хэрэгсэлийг бүрэн хүлээж аваагүй эсвэл торгууль төлөгдөөгүй байна.");
                    goto OnExit;
                }

                #endregion
                #region Execute

                sql = @"update salesrent set rentstatus=1,deliveruser=:5,deliveruserarea=:6,barcode=:7,rentstarttime=:8
where salesno=:1 and custno=:2 and itemno=:3 and prodno=:4 and rentstatus in (0,2,3,4)";
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn606006", salesno, custno, itemno, prodno, userno, userstate, barcode, DateTime.Now);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows <= 0)
                {
                    res = new Result(6060062, "Түрээсийн хэрэгсэл олгогдсон байна.");
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
                lg.item.Desc = "Түрээсийн хэрэгсэл олгох";
            }
        OnExit:
            return res;
        }    //Түрээсийн хэрэгслийг олгосон төлөвт оруулах
        public Result Txn606007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameter

                string salesno = Static.ToStr(ri.ReceivedParam[0]);
                decimal custno = Static.ToDecimal(ri.ReceivedParam[1]);
                int itemno = Static.ToInt(ri.ReceivedParam[2]);
                string prodno = Static.ToStr(ri.ReceivedParam[3]);
                string barcode = Static.ToStr(ri.ReceivedParam[4]);

                int userno = Static.ToInt(ri.ReceivedParam[5]);
                int userstate = Static.ToInt(ri.ReceivedParam[6]);

                string sql = "";

                #endregion

                #region Баркод нь бүртгэлтэй эсэхийг шалгах

                //if (!string.IsNullOrEmpty(barcode))
                //{
                //    sql = "select invid from invseries where barcode=:1 and status>0";
                //    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606006", barcode);
                //    if (res != null && res.ResultNo != 0) goto OnExit;
                //    if (res.AffectedRows <= 0)
                //    {
                //        res = new Result(6060061, string.Format("[{0}] Бүртгэлд ороогүй баркод байна.", barcode));
                //        goto OnExit;
                //    }
                //    string invid = Static.ToStr(res.Data.Tables[0].Rows[0]["INVID"]);
                //    if (invid != prodno)
                //    {
                //        res = new Result(6060061, string.Format("Хэрэгсэлд бүртгэсэн баркод биш байна.", barcode));
                //        goto OnExit;
                //    }
                //}

                #endregion

                #region Олгосон хэрэгслийн баркод мөн эсэх

                sql = @"select barcode from salesrent where salesno=:1 and custno=:2 and itemno=:3 and prodno=:4 and rentstatus=1";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606007", salesno, custno, itemno, prodno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    DataTable dt = res.Data.Tables[0];
                    string rent_barcode = Static.ToStr(dt.Rows[0]["BARCODE"]);
                    if (!string.IsNullOrEmpty(rent_barcode) && rent_barcode != barcode)
                    {
                        res = new Result(6060072, string.Format("Олгосон хэрэгслийн баркод таарахгүй байна. Баркод = [{0}]", rent_barcode));
                        goto OnExit;
                    }
                }
                else
                {
                    res = new Result(6060073, string.Format("[{0}] кодтой түрээсийн хэрэгслийг олгоогүй байна.", prodno));
                    goto OnExit;
                }

                #endregion

                #region Execute

                sql = @"update salesrent set rentstatus=2,receiveruser=:5,receiveruserarea=:6,rentendtime=:7
where salesno=:1 and custno=:2 and itemno=:3 and prodno=:4 and rentstatus=1";
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn606007", salesno, custno, itemno, prodno, userno, userstate,DateTime.Now);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows <= 0)
                {
                    res = new Result(6060073, "Түрээсийн хэрэгсэл олгогдоогүй байна.");
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
                lg.item.Desc = "Түрээсийн хэрэгсэл хүлээн авах";
            }
        OnExit:
            return res;
        }    //Түрээсийн хэрэгслийг авсан төлөвт оруулах
        public Result Txn606008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

                string salesno = Static.ToStr(param[0]);
                decimal custno = Static.ToDecimal(param[1]);
                string prodno = Static.ToStr(param[2]);
                int itemno = Static.ToInt(param[3]);
                int damagetype = Static.ToInt(param[4]);
                string damagenote = Static.ToStr(param[5]);
                string reparationno = Static.ToStr(param[6]);
                int userno = Static.ToInt(param[7]);

                #endregion
                #region Prepare query
                string sql = @"update salesrent set damagetype=:5,damagenote=:6,losspaymentno=:7,receiveruser=:8
where salesno=:1 and custno=:2 and prodno=:3 and itemno=:4";

                #endregion
                #region Execute query
                param = new object[] { salesno, custno, prodno, itemno, damagetype, damagenote, reparationno, userno };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn606008", param);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }    //Хүлээн авсан хэрэгсэлд тайлбар оруулах
        public Result Txn606009(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

                string salesno = Static.ToStr(param[0]);
                decimal custno = Static.ToDecimal(param[1]);
                string prodno = Static.ToStr(param[2]);
                int itemno = Static.ToInt(param[3]);

                #endregion
                #region Prepare query
                string sql = @"select damagetype,damagenote,losspaymentno,deliveruser,receiveruser
,rentstatus,trunc((rentendtime-rentstarttime)*1440) duration
from salesrent
where salesno=:1 and custno=:2 and prodno=:3 and itemno=:4";

                #endregion
                #region Execute query
                param = new object[] { salesno, custno, prodno, itemno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn606008", param);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }    //Эвдрэлийн мэдээлэл лавлах

        #endregion
    }
}
