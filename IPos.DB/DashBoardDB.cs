using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using EServ.Data;
using EServ.Interface;
using EServ.Shared;
using IPos.Core;
namespace IPos.DB
{
    public class DashBoardDB
    {
        #region [ DB228001 - Захиалгын DashBoard жагсаалт мэдээлэл авах ]
        public static Result DB228001(DbConnections pDB, string pRefreshType, int pRefreshNum)
        {
            Result res = new Result();
            try
            {
                string sql;

                //pRefreshType D-Day, T-Time, M-Minute

                DateTime pTxnDate = Core.SystemProp.TxnDate;

                switch (pRefreshType)
                {
                    case "D":
                        {
                            pTxnDate = pTxnDate.AddDays(-pRefreshNum);
                            sql =
        @"select a.OrderNo, a.CustNo, decode(c.classcode, 0, c.firstname||'.'||c.lastname, 1, c.corporatename) as CustName,
            a.OrderAmount,a.PrepaidAmount, a.CurCode, a.Fee, a.StartDate, a.EndDate,
            a.PersonCount, decode(a.Status, 0, 'Цуцлагдсан', 1, 'Идэвхтэй', 2, 'Баталгаажсан') as StatusName
            from orders a
            left join customer c on a.custno=c.customerno
            where a.EndDate >= :1
            order by a.orderno desc
            ";
                            res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB228001", pTxnDate);

                        } break;
                    case "T":
                        {
                            DateTime pTxnTime = DateTime.Now;
                            pTxnTime = pTxnTime.AddHours(-pRefreshNum);

                            sql =
    @"select a.OrderNo, a.CustNo, decode(c.classcode, 0, c.firstname||'.'||c.lastname, 1, c.corporatename) as CustName,
        a.OrderAmount,a.PrepaidAmount, a.CurCode, a.Fee, a.StartDate, a.EndDate,
        a.PersonCount, decode(a.Status, 0, 'Цуцлагдсан', 1, 'Идэвхтэй', 2, 'Баталгаажсан') as StatusName
        from orders a
        left join customer c on a.custno=c.customerno
        left join ordergroup o on a.orderno=o.orderno
        where a.ENDTIME>=:1
        group by a.OrderNo, a.CustNo, decode(c.classcode, 0, c.firstname||'.'||c.lastname, 1, c.corporatename),
        a.OrderAmount,a.PrepaidAmount, a.CurCode, a.Fee, a.StartDate, a.EndDate,
        a.PersonCount, decode(a.Status, 0, 'Цуцлагдсан', 1, 'Идэвхтэй', 2, 'Баталгаажсан')
        order by a.orderno desc

        ";

                            res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB228001", pTxnTime);
                        } break;
                    case "M":
                        {
                            DateTime pTxnTime = DateTime.Now;
                            pTxnTime = pTxnTime.AddMinutes(-pRefreshNum);

                            sql =
    @"select a.OrderNo, a.CustNo, decode(c.classcode, 0, c.firstname||'.'||c.lastname, 1, c.corporatename) as CustName,
        a.OrderAmount,a.PrepaidAmount, a.CurCode, a.Fee, a.StartDate, a.EndDate,
        a.PersonCount, decode(a.Status, 0, 'Цуцлагдсан', 1, 'Идэвхтэй', 2, 'Баталгаажсан') as StatusName
        from orders a
        left join customer c on a.custno=c.customerno
        left join ordergroup o on a.orderno=o.orderno
        where a.ENDTIME>=:1
        group by a.OrderNo, a.CustNo, decode(c.classcode, 0, c.firstname||'.'||c.lastname, 1, c.corporatename),
        a.OrderAmount,a.PrepaidAmount, a.CurCode, a.Fee, a.StartDate, a.EndDate,
        a.PersonCount, decode(a.Status, 0, 'Цуцлагдсан', 1, 'Идэвхтэй', 2, 'Баталгаажсан')
        order by a.orderno desc

        ";

                            res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB228001", pTxnTime);
                        } break;
                }
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

        #region [DB228006 -  Уншигдсан төлөв тэмдэглэх]
        public static Result DB228006(DbConnections pDB,int taskid, int userno, long sourceid, DateTime systemdate)
        {
            Result res = new Result();
            try
            {
                string sql =
@"
merge into markedtask a
using (select :1 taskid,:2 userno,:3 sourceid,:4 checked from dual
) b
on (a.taskid=b.taskid and a.userno=b.userno and a.sourceid=b.sourceid)
when matched then
  update set a.status=1,a.checked=b.checked
when not matched then
  insert (taskid,userno,sourceid,checked,status) 
  values (b.taskid,b.userno,b.sourceid,b.checked,1)

";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB228006",taskid, userno, sourceid, systemdate);

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
        #region[DB228010 - Аларм/сануулга]
        public static Result DB228010(DbConnections pDB, int taskid, int readdays, int userno, int unreaddays, DateTime Txndate,long pLevelno)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select   nvl(mt.status, 0) as status, I3.SUBJECT as issuesubject, i.subject,
c.name as trackname,substr(h1.userfname, 0, 1)||'.'||h1.userlname as assigneeuserName, i.nextpurpose,I.NEXTDATE,I.JRNO
 from issuetxn i
 left join IssueTracks c on i.trackid=c.IssueTrackID
 left join hpuser h1 on i.userno=h1.userno
 left join hpuser h2 on i.assigneeuser=h2.userno
 left join issue i3 on I.ISSUEID=i3.ISSUEID
 left join markedtask mt on mt.taskid=:1 and mt.userno=decode(:3, 0, i.AssigneeUser, :3)  and mt.sourceid=I.JRNO
 left join usergroup u on h2.userno=u.userno
left join txngroup t on u.groupid=t.groupid
where    ( decode(mt.status,null,i.nextdate,0,i.nextdate)  between :5  and :5 + :4  
or
 decode(mt.status,1,mt.checked)  between :5 - :2    and :5)
and (i.AssigneeUser=decode(:3,0,i.AssigneeUser,:3)
or
mt.userno=decode(:3, 0, i.AssigneeUser, :3))
and  t.levelno like :6||'%'
order by 1 asc";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB228010", taskid, readdays, userno, unreaddays, Txndate, pLevelno);

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
        #region[DB228013 - Холбоо барьсан харилцагчийн  явцын үе шат дарааллыг холбогдох удирдлагад сануулах]
        public static Result DB228013(DbConnections pDB, int taskid, int readdays, int userno, int unreaddays, DateTime Txndate,long pLevelno)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT nvl(mt.status, 0) as status,decode(c.classcode, 0, substr(c.FIRSTNAME,0,1)||'.'||c.lastname, 1, CORPORATENAME) customername, ii.subject,ii.description ,decode(ii.status,0,'Open',1,'InProgress',2,'ReOpen',3,'Resolved',9,'Closed')   as statusname
,i4.name as trackname,II.CREATEDATE,substr(h1.userfname, 0, 1)||'.'||h1.userlname as AssigneeUserName,II.DUEDATE,II.ASSIGNEEUSER,ii.issueid,c.customerno as custno
from issue ii
left join hpuser h1 on ii.AssigneeUser=h1.userno
left join hpuser h3 on ii.resolutionuser=h3.userno
left join IssueTracks i4 on ii.trackid=i4.IssueTrackID
left join markedtask mt on mt.taskid=:1 and mt.userno=decode(:3, 0, ii.AssigneeUser, :3)  and mt.sourceid=ii.issueid
left join issuecustomer ic on II.ISSUEID=IC.ISSUEID
left join contact c on ic.CUSTNO=C.CUSTOMERNO 
left join usergroup u on h1.userno=u.userno
left join txngroup t on u.groupid=t.groupid
where (II.ASSIGNEEUSER=decode(:3,0,ii.AssigneeUser,:3)
or
mt.userno=decode(:3, 0, ii.AssigneeUser, :3)    )
and  ic.CUSTNO=C.CUSTOMERNO 
and ii.status<>9
and t.levelno like :6||'%'
and (decode(mt.status,null,ii.createdate,0,ii.createdate,1,mt.checked)  between  :5 and :5 + decode(mt.status,1,:2,:4)
or
  decode(mt.status,1,mt.checked)  between :5 - :2    and :5)
order by 1 asc";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB228013", taskid, readdays, userno, unreaddays, Txndate,pLevelno);

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
        #region [DB228014 -  Төслийн dashboard ]
        public static Result DB228014(DbConnections pDB, int taskid, int readdays, int userno, int unreaddays, DateTime TxnDate)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select   nvl(mt.status, 0) as status,s.issueid,  i.ProjectID,i.name,i.description,
 substr(h1.userfname, 0, 1)||'.'||h1.userlname as OwnerUserNoName ,pr.name as projecttypename,
 substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserNoName, i.createdate
 from IssueProject i
left join hpuser h1 on i.owneruser=h1.userno
left join hpuser h2 on i.CreateUserNo=h2.userno
left join ProjectTypes pr on i.projecttypeid=pr.ProjectTypeID
join issue s on i.projectid=s.projectid
left join markedtask mt on mt.taskid=:1 and mt.userno=decode(:3, 0, i.owneruser, :3) and mt.sourceid= i.ProjectID 
where  i.status<>9
and
  decode(mt.status,null,i.createdate,0,i.createdate,1,mt.checked) between :5 - decode(mt.status,1,:2,:4) and :5
and  (i.owneruser=decode(:3,0,i.owneruser,:3)
or
mt.userno=decode(:3, 0, i.owneruser, :3))
order by i.projectid desc";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB228014", taskid, readdays, userno, unreaddays, TxnDate);

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
        #region [DB228015 -  Хэрэглэгчийн зэрэглэлийн дугаар авах[TxnGroup] ]
        public static Result DB228015(DbConnections pDB, int userno)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select T.LEVELNO from usergroup u,txngroup t where U.GROUPID=T.GROUPID and U.USERNO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB228015", userno);

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

    }
}
