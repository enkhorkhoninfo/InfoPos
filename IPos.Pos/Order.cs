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
    public class Order : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 603001: 	             //Захиалгын жагсаалт хайлт
                        res = Txn603001(ci, ri, db, ref lg);
                        break;
                    case 603002: 	             //Захиалгад хамрагдсан үйлчлүүлэгчид
                        res = Txn603002(ci, ri, db, ref lg);
                        break;
                    case 603003: 	             //Захиалгад хамрагдсан бараанууд
                        res = Txn603003(ci, ri, db, ref lg);
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

        public Result Txn603001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

                string orderno = Static.ToStr(param[0]);
                string custno = Static.ToStr(param[1]);
                string custname1 = Static.ToStr(param[2]);
                string custname2 = Static.ToStr(param[3]);
                string corpname = Static.ToStr(param[4]);
                string regno = Static.ToStr(param[5]);

                #endregion
                #region Prepare query
                string sql = @"
select a.OrderNo, a.CustNo
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,c.registerno
,a.ConfirmTerm||' '||decode(a.TermType, 'T', 'Цаг', 'D', 'Өдөр', 'W', 'Гараг', 'M', 'Сар') as confirmterm
, a.OrderAmount,a.PrepaidAmount,a.CurCode, a.StartDate, a.EndDate,a.PersonCount
, a.Status,decode(a.Status, 0, 'Цуцлагдсан', 1, 'Идэвхтэй', 2, 'Баталгаажсан') as StatusName
, a.CreateDate,a.CreateUser
from orders a
left join customer c on a.custno=c.customerno
{0}
order by 1 desc, 2
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(orderno))
                {
                    values.Add(orderno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("a.orderno=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("a.custno like :{0}||'%'", values.Count);
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
                #region Execute query
                res = db.ExecuteQuery("core", sql, "Txn603001", ri.PageIndex, ri.PageRows, values.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Захиалга хайж жагсаалт авах
        public Result Txn603002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string orderno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Prepare query
                string sql = @"select p.custno,c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,d.serialno,decode(p.custno,o.custno,1,0) owner
from orderperson p
left join orders o on o.orderno=p.orderno
left join customeriddevice d on d.custno=p.custno
left join customer c on c.customerno=p.custno
where p.orderno=:1";

                #endregion
                #region Execute query
                object[] param = new object[] { orderno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn603002", param);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Захиалгад хамрагдсан үйлчлүүлэгчид
        public Result Txn603003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string orderno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Prepare query
                string sql = @"select orderno,groupno,prodno,prodtype,qty from orderproduct where orderno=:1";

                #endregion
                #region Execute query
                object[] param = new object[] { orderno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn603003", param);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Захиалгад хамрагдсан бараанууд


        #endregion
    }
}
