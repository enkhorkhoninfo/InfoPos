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
    public class Cust : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 602001: 	             //Харилцагч лавлах
                        res = Txn602001(ci, ri, db, ref lg);
                        break;
                    case 602003: 	             //Байгууллага лавлах
                        res = Txn602003(ci, ri, db, ref lg);
                        break;
                    case 602004: 	             //Иргэн лавлах
                        res = Txn602004(ci, ri, db, ref lg);
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
   
        public Result Txn602001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

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
, decode(c.classcode,0,c.lastname||' '||c.firstname,c.corporatename) custname
, c.mobile, c.registerno, c.membercontractno
, decode(c.classcode,0, decode(c.membercontractno,null,'ИРГЭН','ГИШҮҮН'),'ААН') classcode
, decode(c.classcode,0, decode(c.sex,0,'ЭР','ЭМ'), null) sex
from customer c
{0} order by c.customerno desc
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
                res = db.ExecuteQuery("core", sql, "Txn602001", ri.PageIndex, ri.PageRows, values.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
            }
            return res;
        }       // Харилцагч хайх
        public Result Txn602002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

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
{0} order by c.customerno desc
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
                res = db.ExecuteQuery("core", sql, "DB500002", ri.PageIndex, ri.PageRows, values.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
            }
            return res;
        }       // ПОС үйлчлүүлэгч үүсгэх

        public Result Txn602003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

                string custno = Static.ToStr(param[0]);
                string cname = Static.ToStr(param[1]);
                string reg = Static.ToStr(param[2]);

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1) */
c.customerno,c.corporatename custname,c.registerno,nvl(telephone, c.mobile) phoneno
from customer c where c.classcode=1 {0}
order by 2
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    sb.AppendFormat("and c.customerno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(cname))
                {
                    values.Add(cname);
                    sb.AppendFormat("and c.corporatename like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(reg))
                {
                    values.Add(reg);
                    sb.AppendFormat("and c.registerno like :{0}||'%'", values.Count);
                }

                //if (sb.Length > 0) sb.Insert(0, "where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "Txn602001", ri.PageIndex, ri.PageRows, values.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
            }
            return res;
        }       // Байгууллага хайх
        public Result Txn602004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

                string custno = Static.ToStr(param[0]);
                string fname = Static.ToStr(param[1]);
                string lname = Static.ToStr(param[2]);
                string reg = Static.ToStr(param[3]);
                string contract = Static.ToStr(param[4]);

                #endregion
                #region Prepare query
                string sql = @"
select /*+ first_rows(1) */
c.customerno
,decode(c.classcode,0,c.lastname||' '||c.firstname,c.corporatename) custname
,decode(c.sex,0,'♂','♀') sex,c.registerno,c.mobile,c.membercontractno
from customer c where c.classcode=0 {0}
order by 2
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    sb.AppendFormat("and c.customerno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(reg))
                {
                    values.Add(reg);
                    sb.AppendFormat("and c.registerno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(fname))
                {
                    values.Add(fname);
                    sb.AppendFormat("and c.firstname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(lname))
                {
                    values.Add(lname);
                    sb.AppendFormat("and c.lastname=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(contract))
                {
                    values.Add(contract);
                    sb.AppendFormat("and c.membercontractno=:{0}", values.Count);
                }

                //if (sb.Length > 0) sb.Insert(0, "where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "Txn602001", ri.PageIndex, ri.PageRows, values.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";
            }
            return res;
        }       // Хувь хүн хайх

    }
}
