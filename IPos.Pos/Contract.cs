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
    public class Contract : IModule
    {

        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 604001: 	             //Гэрээний жагсаалт хайлт
                        res = Txn604001(ci, ri, db, ref lg);
                        break;
                    case 604002: 	             //Гэрээнд хамрагдсан үйлчлүүлэгчид
                        res = Txn604002(ci, ri, db, ref lg);
                        break;
                    case 604003: 	             //Гэрээнийн үндсэн мэдээлэл
                        res = Txn604003(ci, ri, db, ref lg);
                        break;
                    case 604004: 	             //Гэрээнд хамрагдах барааны мэдээлэл
                        res = Txn604004(ci, ri, db, ref lg);
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

        public Result Txn604001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

                string contracttype = Static.ToStr(param[0]);
                string contractno = Static.ToStr(param[1]);
                string custno = Static.ToStr(param[2]);
                string custname1 = Static.ToStr(param[3]);
                string custname2 = Static.ToStr(param[4]);
                string corpname = Static.ToStr(param[5]);
                string regno = Static.ToStr(param[6]);

                #endregion
                #region Prepare query
                string sql = @"
select a.contractno, a.custno, c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,a.contracttype,ct.name typename
,a.amount,a.balance,a.curcode
,a.validstartdate,a.validenddate,a.personcount
, a.Status,decode(a.Status, 0, 'Идэвхгүй', 1, 'Идэвхтэй', 2, 'Баталгаажсан') as StatusName
, a.CreateDate,a.CreateUser,a.vat
from contractmain a
left join customer c on a.custno=c.customerno
left join pacontracttype ct on ct.contracttype=a.contracttype
{0}
order by 1 desc, 4
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (!string.IsNullOrEmpty(contracttype))
                {
                    values.Add(contracttype);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("a.contracttype=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(contractno))
                {
                    values.Add(contractno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("a.contractno like :{0}", values.Count);
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
                res = db.ExecuteQuery("core", sql, "Txn604001", ri.PageIndex, ri.PageRows, values.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Гэрээ хайж жагсаалт авах
        public Result Txn604002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string contractno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Prepare query
                string sql = @"select p.custno,c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,d.serialno,decode(p.custno,o.custno,1,0) owner
from orderperson p
left join orders o on o.orderno=p.orderno
left join customeriddevice d on d.custno=p.custno
left join customer c on c.customerno=p.custno
where p.contractno=:1";

                #endregion
                #region Execute query
                object[] param = new object[] { contractno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT , "Txn604002", param);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Гэрээнд хамрагдсан үйлчлүүлэгчид
        public Result Txn604003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string contractno = Static.ToStr(ri.ReceivedParam[0]);
                string contracttype = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Prepare query

                /***************************************************
                 * ContractMain.BalanceType = 0 улайхгүй
                 *                          = 1 улайж болно
                 * PaContractType.Method    = 0 үлдэгдэл нь борлуулалтаар буурна
                 *                          = 1 үлдэгдэл нь авто буурна
                 *                          = 2 үлдэгдэл хөтлөхгүй
                 ***************************************************/

                string sql = @"
select cm.contracttype,custno,amount,balance,validstartdate,validenddate,depfreq,status,balancetype,ct.method
from contractmain cm
left join pacontracttype ct on ct.contracttype=cm.contracttype
where  cm.contractno=:1 and cm.contracttype=:2";
                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn604003", contractno, contracttype);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Гэрээнийн үндсэн мэдээлэл
        public Result Txn604004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string contractno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Prepare query
                string sql = @"select prodno,prodtype from contractprod where contractno=:1 order by 1";
                #endregion
                #region Execute query
                object[] param = new object[] { contractno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn604004", param);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Гэрээнд хамрагдсан бараанууд


        #endregion
    }
}
