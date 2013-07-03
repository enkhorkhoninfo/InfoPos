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

namespace Ipos.admin
{
    class DBIO
    {
        public static Result F_Error(Result res)
        {
            int DdIdErrorNo = -2147467259;

            if (res.ResultNo == DdIdErrorNo)
            {
                if (res.ResultDesc.IndexOf("ORA-00001") != -1)
                {
                    res.ResultNo = 9110039;
                    res.ResultDesc = "Бичлэг давхардаж байна. {ID дугаар эсвэл Эрэмбийн дугаар}";
                }
            }
            else
            {
                if (res.ResultDesc.IndexOf("ORA-00001") != -1)
                {
                    res.ResultNo = 9110039;
                    res.ResultDesc = "Бичлэг давхардаж байна. {ID дугаар эсвэл Эрэмбийн дугаар}";
                }
            }
            return res;
        }
        #region [ DB203001 - Хэрэглэгчийн жагсаалт авах ]
        public static Result DB203001(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {//
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] {"UserNo like","UserFname like","UserLname like","UserFname2 like","UserLname2 like",
"RegisterNo like","Position","Status","BranchNo","UserLevel",
"UserType","Email like","Mobile like","AGENTCORPNO","AgentBranchno"};

                ArrayList dbparam = new ArrayList(fieldnames.Length);
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                if (pParam != null)
                {
                    int dbindex = 1;
                    for (int i = 0; i < pParam.Length; i++)
                    {
                        if (pParam[i] != null && pParam[i] != DBNull.Value && Static.ToStr(pParam[i]) != "")
                        {
                            if (sb.Length > 0) sb.Append(" AND ");

                            if (fieldnames[i].Substring(fieldnames[i].Length - 5, 5).ToLower() == " like")
                                sb.AppendFormat(" {0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" {0}=:{1} ", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@" select  UserNo, UserFname, UserLname,status,UserFname2, UserLname2 from HPUser  
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by UserNo");

                res = pDB.ExecuteQuery("core", sql, "DB203001", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB203002 - Хэрэглэгчийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB203002(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select UserNo, UserFname, UserLname, UserFname2, UserLname2, RegisterNo, Position, Status, BranchNo, UserLevel as ulevel, 
UPassword, UserType, Email, Mobile, logintype,agentcorpno,agentbranchno
from HPUser
where userno = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203002", pUserNo);

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
        #region [ DB203003 - Хэрэглэгчийн сонгогдсон болон сонгогдоогүй эрхийн бүлгүүд авах ]
        public static Result DB203003(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select  decode(a.groupid,  '',  0,  1)  status,  B.groupid ,  b.name  groupname,  a.expiredate,  nvl(a.level1,0) as level1,  nvl(a.level2,0) as level2,  nvl(a.level3,0) as level3,  nvl(a.level4,0) as level4
from  (select  * from  usergroup  where  userno=:1)  a
right  join  txngroup  b on  A.groupid=B.groupid
order  by  b.groupid";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203003", pUserNo);

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
        #region [ DB203004 - Хэрэглэгчийн сонгогдсон супервайзар эрхүүд авах ]
        public static Result DB203004(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select b.userno, b.userfname, b.userlname
from (select B.SUPUSERNO from hpuser a, usersup b where a.userno=b.userno and a.userno=:1 )a, hpuser b
where a.SUPUSERNO=b.userno Order By b.userno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203004", pUserNo);

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
        #region [ DB203005 - Хэрэглэгчийн зураг авах ]
        public static Result DB203005(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select pic from userpic where userno=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203005", pUserNo);

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
        #region [ DB203006 - Хэрэглэгч нэмэх ]
        public static Result DB203006(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();

            try
            {
                string sql =
@"insert into hpuser(UserNo, UserFname, UserLname, UserFname2, UserLname2, RegisterNo, Position, Status, BranchNo, UserLevel,
UPassword, UserType, Email, Mobile, LoginType, WRONGCOUNT, PASSCHDATE,agentcorpno,agentbranchno)
values(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203006", pParam);
                res = F_Error(res);
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
        #region [ DB203007 - Хэрэглэгч засварлах ]
        public static Result DB203007(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sqln = "UPassword=:11,";
                /*if (pParam[11] != null)
                    sqln = " UPassword=:11, ";*/

                string sql =
@"UPDATE hpuser SET 
UserFname=:2,
UserLname=:3,
UserFname2=:4,
UserLname2=:5,
RegisterNo=:6,
Position=:7,
Status=:8,
BranchNo=:9,
UserLevel=:10,
" + sqln + @"
UserType=:12,
Email=:13,
Mobile=:14,
LoginType=:15,
WRONGCOUNT=:16,
PASSCHDATE=:17,
agentcorpno=:18,
agentbranchno=:19
WHERE UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB203007", pParam);

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
        #region [ DB203008 - Хэрэглэгч устгах ]
        public static Result DB203008(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                //HPUser delete
                string sql =
@"DELETE FROM hpuser WHERE UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203008", pUserNo);
                if (res.ResultNo != 0) return res;

                //UserSup delete
                sql =
@"DELETE FROM usersup WHERE UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203008", pUserNo);
                if (res.ResultNo != 0) return res;

                //UserGroup delete
                sql =
@"DELETE FROM UserGroup WHERE UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203008", pUserNo);

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
        #region [ DB203009 - Хэрэглэгчийн эрхүүд нэмэх болон засварлах ]
        public static Result DB203009(DbConnections pDB, int pUserNo, DataTable DT)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete from usergroup where userno=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203009", pUserNo);

                if (res.ResultNo != 0) return res;

                foreach (DataRow dr in DT.Rows)
                {

                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
@"insert into usergroup(groupid, UserNo, ExpireDate,level1,level2,level3,level4)
values(:1, :2, :3,:4,:5,:6,:7)";

                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203009", dr["GroupID"], pUserNo, Static.ToDateTime(dr["ExpireDate"]), Static.ToInt(dr["level1"]), Static.ToInt(dr["level2"]), Static.ToInt(dr["level3"]), Static.ToInt(dr["level4"]));
                        if (res.ResultNo != 0) return res;
                    }
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
        #region [ DB203010 - Хэрэглэгчийн supriser нэмэх болон засварлах ]
        public static Result DB203010(DbConnections pDB, int pUserNo, DataTable DT)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete from UserSup where UserNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203010", pUserNo);

                if (res.ResultNo != 0) return res;

                foreach (DataRow dr in DT.Rows)
                {
                    sql =
@"insert into UserSup(UserNo, SupUserNo)
values(:1, :2)";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203010", pUserNo, dr["UserNo"]);
                    if (res.ResultNo != 0) return res;
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
        #region [ DB203011 - Хэрэглэгчийн зураг нэмэх болон засварлах ]
        public static Result DB203011(DbConnections pDB, int pUserNo, byte[] BA)
        {
            Result res = new Result();
            try
            {
                object[] param = new object[] { pUserNo, BA };

                string sql =
@"Update UserPic set pic =:2 WHERE UserNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB203011", param);

                if (res.ResultNo != 0) return res;

                if (res.AffectedRows == 0)
                {
                    sql =
@"INSERT INTO UserPic(UserNo, Pic) VALUES(:1, :2)";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203011", param);
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
        #region [ DB203012 - Хэрэглэгчийн бүлгийн жагсаалт авах ]
        public static Result DB203012(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "GroupID", "Name like", "Name2 like", "OrderNo" };

                ArrayList dbparam = new ArrayList(fieldnames.Length);
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                if (pParam != null)
                {
                    int dbindex = 1;
                    for (int i = 0; i < pParam.Length; i++)
                    {
                        if (pParam[i] != null && pParam[i] != DBNull.Value && Static.ToStr(pParam[i]) != "")
                        {
                            if (sb.Length > 0) sb.Append(" AND ");

                            if (fieldnames[i].Substring(fieldnames[i].Length - 5, 5).ToLower() == " like")
                                sb.AppendFormat(" {0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" {0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT GROUPID, NAME, NAME2, levelno, ORDERNO
FROM TXNGROUP
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by GROUPID");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203012", dbparam.ToArray());

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
        #region [ DB203013 - Хэрэглэгчийн бүлгийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB203013(DbConnections pDB, int pGroupId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT GROUPID, NAME, NAME2, ORDERNO,levelno
FROM TXNGROUP
where GROUPID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203013", pGroupId);

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
        #region [ DB203014 - Хэрэглэгчийн бүлгийн сонгогдсон болон сонгогдоогүй гүйлгээнүүд авах ]
        public static Result DB203014(DbConnections pDB, int pGroupId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select decode(a.trancode, '', 0, 1) status, d.trancode, d.name, d.name2
from (select trancode from grouptxn a, txngroup c where A.GROUPID=c.GROUPID and A.GROUPID=:1 ) a
right join txn d on A.trancode=d.trancode
order by d.trancode";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203014", pGroupId);

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
        #region [ DB203015 - Хэрэглэгчийн бүлэг нэмэх ]
        public static Result DB203015(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into TXNGROUP(GROUPID, NAME, NAME2, ORDERNO,levelno)
values(:1, :2, :3, :4,:5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203015", pParam);

                res = F_Error(res);

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
        #region [ DB203016 - Хэрэглэгчийн бүлэг засварлах ]
        public static Result DB203016(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE TXNGROUP SET 
NAME=:2, NAME2=:3, ORDERNO=:4,levelno=:5
WHERE GROUPID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB203016", pParam);

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
        #region [ DB203017 - Хэрэглэгчийн бүлэг устгах ]
        public static Result DB203017(DbConnections pDB, int pGroupId)
        {
            Result res = new Result();
            try
            {
                //TXNGROUP delete
                string sql =
@"DELETE FROM TXNGROUP WHERE GROUPID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203017", pGroupId);
                if (res.ResultNo != 0) return res;

                //GROUPTXN delete
                sql =
@"DELETE FROM GROUPTXN WHERE GROUPID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203017", pGroupId);
                if (res.ResultNo != 0) return res;

                //UserGroup delete
                sql =
@"DELETE FROM UserGroup WHERE GROUPID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203017", pGroupId);

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
        #region [ DB203018 - Хэрэглэгчийн бүлэг гүйлгээнүүдийг нэмэх болон засварлах ]
        public static Result DB203018(DbConnections pDB, int pGroupId, DataTable DT)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete from GroupTxn where GroupId=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203018", pGroupId);

                if (res.ResultNo != 0) return res;

                foreach (DataRow dr in DT.Rows)
                {
                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
@"insert into GroupTxn(GroupId, TranCode)
values(:1, :2)";

                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203018", pGroupId, dr["TranCode"]);
                        if (res.ResultNo != 0) return res;
                    }
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

        #region [ DB216001 - Нууц үгийн бүртгэлийн мэдээлэл авах ]
        public static Result DB216001(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT MASKTYPE, MASKVALUE, MASKDESCRIPTION, DEFAULTPASS, CREATETYPE, VALIDDAY, WRONGCOUNT, HISTORYCOUNT, servername, serverport,
mailusername, mailuserpass, fromuser, isSendMail
FROM PASSPOLICY
WHERE ID=1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB216001", null);

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
        #region [ DB216002 - Нууц үгийн бүртгэлийг засварлах ]
        public static Result DB216002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"MERGE INTO PASSPOLICY b
USING (
SELECT :1 MASKTYPE, :2 MASKVALUE,  :3 MASKDESCRIPTION, :4 DEFAULTPASS, :5 CREATETYPE, :6 VALIDDAY, :7 WRONGCOUNT, :8 HISTORYCOUNT, :9 servername, :10 serverport,
:11 mailusername, :12 mailuserpass, :13 fromuser, :14 isSendMail
  FROM dual
  ) e
ON (b.ID = 1)
WHEN MATCHED THEN
  UPDATE SET MASKTYPE=:1, MASKVALUE=:2, MASKDESCRIPTION=:3, DEFAULTPASS=:4, CREATETYPE=:5, VALIDDAY=:6, WRONGCOUNT=:7, HISTORYCOUNT=:8, servername=:9, serverport=:10,
mailusername=:11, mailuserpass=:12, fromuser=:13, isSendMail=:14
WHEN NOT MATCHED THEN
 insert (ID, MASKTYPE, MASKVALUE, MASKDESCRIPTION, DEFAULTPASS, CREATETYPE, VALIDDAY, WRONGCOUNT, HISTORYCOUNT, servername, serverport,
mailusername, mailuserpass, fromuser, isSendMail)
values (1, :1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14)
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB216002", pParam);

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

        #region [ DB203200 - Логийн жагсаалт мэдээлэл авах ]
        public static Result DB203200(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                string[] fieldnames = new string[] { "LOGID like","TXNDATE like","POSTDATE like","USERNO","BRANCHNO",
                    "SUPERVISORNO like","TXNCODE like","DESCRIPTION like","NOTE like","RESULTNO like",
                    "RESULTDESC like"
                };

                ArrayList dbparam = new ArrayList(fieldnames.Length);
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                if (pParam != null)
                {
                    int dbindex = 1;
                    for (int i = 0; i < pParam.Length; i++)
                    {
                        if (pParam[i] != null && pParam[i] != DBNull.Value && Static.ToStr(pParam[i]) != "")
                        {
                            if (sb.Length > 0) sb.Append(" AND ");
                            if (fieldnames[i].Substring(fieldnames[i].Length - 5, 5).ToLower() == " like")
                                sb.AppendFormat(" L.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" L.{0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT L.LOGID, L.TXNDATE, L.POSTDATE,  L.USERNO, substr(h.userfname, 0, 1)||'.'||h.userlname as USERNOName, 
L.BRANCHNO, b.name as branchname, L.SUPERVISORNO,  L.TXNCODE, t.name as txnname, L.DESCRIPTION, L.NOTE, L.RESULTNO, 
L.RESULTDESC, L.KEY1, L.KEY2, L.KEY3, L.KEY4, L.KEY5, L.KEY6, L.KEY7, L.KEY8, L.KEY9, L.KEY10
FROM LOG L
left join hpuser h on  L.USERNO=h.userno
left join branch b on L.BRANCHNO=B.BRANCH
left join txn t on  L.TXNCODE=t.trancode 
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by L.LOGID desc");

                res = pDB.ExecuteQuery("core", sql, "DB203200", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB203201 - Логийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB203201(DbConnections pDB, long pLogID)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT LOGID, TABLENAME, FIELDNAME, OLDVALUE, NEWVALUE
FROM LOGDETAIL
WHERE LOGID=:1 ORDER BY LOGID, TABLENAME, FIELDNAME";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203201", pLogID);

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
