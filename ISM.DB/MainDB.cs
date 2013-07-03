using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using EServ.Data;
using EServ.Interface;
using EServ.Shared;
//using IPos.Core;

namespace ISM.DB
{
    public static class Main
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
            return res;
        }
        #region [ 100 - ism.core.dll UNUSED ! NEED TO REMOVE LATER ]
        #region [ DB100001 - Insert Attach ]
        public static Result DB100001(DbConnections pDB, int pUserno, ulong pID, DateTime pAttachDate, int pAttachType, byte[] pAttachData )
        {
            Result res = new Result();
            try
            {
                string sql = @"insert into attach (attachid,attachdate,attachtype,attachblob,userno) values(:1,:2,:3,:4,:5)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB100001", pID, pAttachDate, pAttachType, pAttachData, pUserno);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB100002 - Insert Attach link ]
        public static Result DB100002(DbConnections pDB, ulong pID, ulong pTypeID, int pTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql = @"insert into attachlink (attachid,typeid,typecode) values(:1,:2,:3)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB100002", pID, pTypeID, pTypeCode);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB100003 - Delete Attach]
        public static Result DB100003(DbConnections pDB, ulong pID)
        {
            Result res = new Result();
            try
            {
                string sql = @"delete attach where attachid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB100003", pID);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB100004 - Delete Attach link ]
        public static Result DB100004(DbConnections pDB, ulong pID)
        {
            Result res = new Result();
            try
            {
                string sql = @"delete attachlink where attachid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB100004", pID);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB100005 - Insert Log ]
        public static Result DB100005(DbConnections pDB, 
                                      ulong pLogID, DateTime pTxnDate, DateTime pPostDate,
                                      int pUserNo, int pBranchNo, int pSupervisorNo,
                                      int pTxnCode, string pDescription, string pNote,
                                      int pResultNo, string pResultDesc,
                                      string pKey1,string pKey2,string pKey3,string pKey4,string pKey5,string pKey6,string pKey7,string pKey8,string pKey9,string pKey10)
        {
            Result res = new Result();
            try
            {
                string sql = @" insert into log (logid
                                ,txndate,postdate,userno,branchno,supervisorno,txncode,description,note,resultno,resultdesc
                                ,key1,key2,key3,key4,key5,key6,key7,key8,key9,key10)
                                values(:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16,:17,:18,:19,:20,:21)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB100005", pLogID, pTxnDate, pPostDate, pUserNo, pBranchNo, pSupervisorNo, 
                                                                                        pTxnCode,pDescription, pNote, pResultNo,pResultDesc,pKey1,pKey2,pKey3,pKey4,pKey5,pKey6,pKey7,pKey8,pKey9,pKey10 );

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB100006 - Insert Log Detail ]
        public static Result DB100006(DbConnections pDB, ulong pLogID, string pTableName, string pFieldName, string pOldValue, string pNewValue)
        {
            Result res = new Result();
            try
            {
                string sql = @"insert into logdetail (logid,tablename,fieldname,oldvalue,newvalue) values(:1,:2,:3,:4,:5)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB100006", pLogID,pTableName, pFieldName, pOldValue, pNewValue);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion    
        #region [ DB100007 - Select Error Messages ]
        public static Result DB100007(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @"select msgid, name, name2 from errmsg";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB100007");

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion    
        #endregion
        #region [ 101 - ism.suser.dll ]
        #region [ DB101001 - User select ]
        public static Result DB101001(DbConnections pDB, int pUserNo)   
        {
            Result res = null;
            try
            {
                string sql = @"select a.UserNo,UserFName,UserFName2,UserLName,UserLName2,Position,Status,BranchNo,UserLevel,UPassword,
UserType, WRONGCOUNT, PASSCHDATE, LOGINTYPE, level1, level2, level3, level4, tg.txngrouplevel,a.agentcorpno,a.agentbranchno
from HPUSER a
left join (select userno, decode(sum(level1), 0, 0, null, 0, 1) as level1, decode(sum(level2), 0, 0, null, 0, 1) as level2, decode(sum(level3), 0, 0, null, 0, 1) as level3, decode(sum(level4), 0, 0, null, 0, 1) as level4
from usergroup where userno=:1 group by userno) b on a.UserNo=b.UserNo
left join (select min(decode(tg.levelno, null, 1, tg.levelno)) as txngrouplevel, ug.userno from usergroup ug
            left join txngroup tg on UG.GROUPID=tg.groupid
            where ug.userno=:1 group by ug.userno) tg on a.userno=tg.userno
where a.UserNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB101001", pUserNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB101002 - Change User password ]
        public static Result DB101002(DbConnections pDB, int pUserNo, string pOldPass, string pNewPass)
        {
            Result res = new Result();
            try
            {
                string sql = @"Update HPUSER 
set UPASSWORD=:3, WRONGCOUNT=0, PASSCHDATE=sysdate
where UserNo=:1 and UPASSWORD=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB101002", pUserNo, pOldPass, pNewPass);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB101003 - User txncodes select ]
        public static Result DB101003(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql = @" select  u.userno, gt.trancode
from hpuser u
left join UserGroup ug on u.UserNo=ug.UserNo and ug.ExpireDate>sysdate
inner join GroupTxn gt on ug.GroupID=gt.GroupID 
where u.userno=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB101003", pUserNo);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }

        }
        #endregion
        #region [ DB101004 - User txncode check select ]
        public static Result DB101004(DbConnections pDB, int pUserNo, int pTranCode)
        {
            Result res = new Result();
            try
            {
                string sql = @" select  u.userno, gt.trancode
                                from hpuser u
                                left join UserGroup ug on u.UserNo=ug.UserNo 
                                inner join GroupTxn gt on ug.GroupID=gt.GroupID 
                                where u.userno=:1 and gt.trancode=:2 and nvl(ug.ExpireDate,sysdate)>=sysdate";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB101004", pUserNo, pTranCode);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }

        }
        #endregion
        #region [ DB101005 - Нууц үгийн бүртгэлийн мэдээлэл авах ]
        public static Result DB101005(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT MASKTYPE, MASKVALUE, MASKDESCRIPTION, DEFAULTPASS, CREATETYPE, VALIDDAY, WRONGCOUNT, HISTORYCOUNT
FROM PASSPOLICY
WHERE ID=1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB101005", null);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB101006 - Нууц үгийн түүхийн жагсаалт авах ]
        public static Result DB101006(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select SeqNo, UserNo, Pass, ChangeDate
from PassHistory
where userno=:1
order by SeqNo
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB101006", pUserNo);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB101007 - Нууц үгийн түүхийн бүртгэлийг нэмэх ]
        public static Result DB101007(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"insert into PassHistory(SeqNo, UserNo, Pass, ChangeDate)
values(:1, :2, :3, :4)
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB101007", pParam);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB101008 - Нууц үгийг нууц үгийн түүх дотор байгааг шалгах ]
        public static Result DB101008(DbConnections pDB, int pUserNo, string pPass)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select SeqNo, UserNo, Pass, ChangeDate
from PassHistory
where UserNo=:1 and Pass=:2
order by ChangeDate";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB101008", pUserNo, pPass);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB101009 - Нууц үгийн түүхээс нууц үг устгах ]
        public static Result DB101009(DbConnections pDB, long pSeqNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"Delete from PassHistory where SeqNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB101009", pSeqNo);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB101010 - Нууц үг буруу оруулалтыг ахиулах ]
        public static Result DB101010(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"UPDATE HPUSER
set
    WRONGCOUNT=decode(WRONGCOUNT, null, 0, WRONGCOUNT)+1
Where
    USERNO=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB101010", pUserNo);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB101011 - Хэрэглэгчийн эрхийг идэвхгүй болгох ]
        public static Result DB101011(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"UPDATE HPUSER
set
    STATUS=9
Where
    USERNO=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB101011", pUserNo);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB101012 - Нууц үг буруу оруулалтыг 0 болгох ]
        public static Result DB101012(DbConnections pDB, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"UPDATE HPUSER
set
    WRONGCOUNT=0
Where
    USERNO=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB101012", pUserNo);

                return res;
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion

        #region [ DB101013 - User select+AgentCorp ]
        public static Result DB101013(DbConnections pDB, int pUserNo)
        {
            Result res = null;
            try
            {
                string sql = @"select a.UserNo,UserFName,UserFName2,UserLName,UserLName2,Position,Status,BranchNo,UserLevel,UPassword,
UserType, WRONGCOUNT, PASSCHDATE, LOGINTYPE, level1, level2, level3, level4, A.AGENTCORPNO
from HPUSER a
left join (select userno, decode(sum(level1), 0, 0, null, 0, 1) as level1, decode(sum(level2), 0, 0, null, 0, 1) as level2, decode(sum(level3), 0, 0, null, 0, 1) as level3, decode(sum(level4), 0, 0, null, 0, 1) as level4
from usergroup where userno=:1 group by userno) b on a.UserNo=b.UserNo
where a.UserNo=:1
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB101013", pUserNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
  
       
        



#endregion
        #region[102 - HPUSERMESSAGE]
        #region [ DB102000 - HPUSERMESSAGE- жагсаалт авах ]
        public static Result DB102000(DbConnections pDB, object[] pParam)
        {
            Result res = null;
            try
            {
                string sql = @"select MSGID,TXNDATE,POSTDATE,FROMUSERNO,TOUSERNO,DESCRIPTION,ISREAD from  hpusermessage order by 1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB102000", null);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB102001 - HPUSERMESSAGE -дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB102001(DbConnections pDB, long pMSGID)
        {
            Result res = null;
            try
            {
                string sql = @"select MSGID,TXNDATE,POSTDATE,FROMUSERNO,TOUSERNO,DESCRIPTION,ISREAD from  hpusermessage where MSGID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB102001", pMSGID);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB102002 - HPUSERMESSAGE- шинээр нэмэх ]
        public static Result DB102002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong MSGID = EServ.Interface.Sequence.NextByVal("MSGID");

                pParam[0] = Static.ToStr(MSGID);

                string sql =
@"insert into hpusermessage(MSGID,TXNDATE,POSTDATE,FROMUSERNO,TOUSERNO,DESCRIPTION,ISREAD) values(:1,:2,:3,:4,:5,:6,:7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB102002", pParam);
                res = F_Error(res);
                res.Param = pParam;
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
        #region [ DB102003 - HPUSERMESSAGE- засварлах ]
        public static Result DB102003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update hpusermessage set TXNDATE=:2,POSTDATE=:3,FROMUSERNO=:4,TOUSERNO=:5,DESCRIPTION=:6,ISREAD=:7
where MSGID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB102003", pParam);
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
        #region [ DB102004 - HPUSERMESSAGE устгах ]
        public static Result DB102004(DbConnections pDB, long pMSGID, int pUserNo, int pType)
        {
            Result res = new Result();
            string sql;
            try
            {
                if (pType == 1)
                {

                    sql =
@"update hpusermessage set FROMSTATUS=1
where MSGID=:1 AND FROMUSERNO=:2";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB102004", pMSGID, pUserNo);

                    return res;

                }

                if (pType == 0)
                {
                    sql =
@"update hpusermessage set TOSTATUS=1
where MSGID=:1 AND TOUSERNO=:2";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB102004", pMSGID, pUserNo);
                    res = F_Error(res);
                    return res;


                }



                sql =
@"DELETE FROM hpusermessage 
WHERE MSGID=:1
AND     TOSTATUS=1
AND FROMSTATUS=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB102004", pMSGID);

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
        #region [ DB102005 - HPUSERMESSAGE -ирсэн хэрэглэгчийн жагсаалт мэдээлэл авах ]
        public static Result DB102005(DbConnections pDB, int pFROMUSERNO)
        {
            Result res = null;
            try
            {
                string sql = @"select m.ISREAD,m.MSGID,trim(m.FROMUSERNO)||'-'||trim(substr(h.userfname, 0, 1)||'.'||h.userlname) as FROMUSERNO,trim(m.TOUSERNO)||'-'||trim(substr(hh.userfname, 0, 1)||'.'||hh.userlname) as TOUSERNO,m.DESCRIPTION,m.TXNDATE,m.POSTDATE,m.FROMUSERNO,m.TOUSERNO from  hpusermessage m
left join hpuser h on M.FROMUSERNO=H.USERNO
left join hpuser hh on M.TOUSERNO=HH.USERNO
where m.TOUSERNO=:1 
and m.tostatus=0
order by 3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB102005", pFROMUSERNO);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB102006 - HPUSERMESSAGE -явуулсан хэрэглэгчийн жагсаалт мэдээлэл авах ]
        public static Result DB102006(DbConnections pDB, int pTOUSERNO)
        {
            Result res = null;
            try
            {
                string sql = @"select m.ISREAD,m.MSGID,trim(m.FROMUSERNO)||'-'||trim(substr(h.userfname, 0, 1)||'.'||h.userlname) as FROMUSERNO,trim(m.TOUSERNO)||'-'||trim(substr(hh.userfname, 0, 1)||'.'||hh.userlname) as TOUSERNO,m.DESCRIPTION,m.TXNDATE,m.POSTDATE,m.FROMUSERNO,m.TOUSERNO from  hpusermessage m
left join hpuser h on M.FROMUSERNO=H.USERNO
left join hpuser hh on M.TOUSERNO=HH.USERNO
where m.FROMUSERNO=:1 
and m.fromstatus=0
order by 3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB102006", pTOUSERNO);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB102007 - HPUSERMESSAGE -Уншсан ,уншаагүй төлөв тэмдэглэх ]
        public static Result DB102007(DbConnections pDB, long pMSGID)
        {
            Result res = null;
            try
            {
                string sql = @"update  hpusermessage set isread=1 where msgid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB102007", pMSGID);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB102008 - Хэрэглэгчийн салбарийн ажилчдийн жагсаалт мэдээлэл авах ]
        public static Result DB102008(DbConnections pDB, int pBranchNo)
        {
            Result res = null;
            try
            {
                string sql = @"select  h.userno,substr(h.userfname, 0, 1)||'.'||h.userlname as UserName from hpuser h 
where h.branchno=:1
order by 1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB102008", pBranchNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #endregion
        #region [ 103 - Attach Data Related ]
        #region [ DB103001 - Select Attached records using specified AttachId ]
        public static Result DB103001(DbConnections pDB, ulong pAttachId)
        {
            Result res = null;
            try
            {
                string sql = @"select attachid,attachdate,attachtype,attachblob,userno,filename,description from attach a where attachid=:1";
                res = pDB.ExecuteQuery("data", sql, enumCommandType.SELECT, "DB103001", pAttachId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB103002 - Update Attach data on specified AttachId ]
        public static Result DB103002(DbConnections pDB, ulong pAttachId, int pAttachType, byte[] pAttachBlob, int pUserNo, string pFileName, string pDescription)
        {
            Result res = null;
            try
            {
                string sql = @"update attach set attachdate=sysdate,attachtype=:2,attachblob=:3,userno=:4,filename=:5,description=:6 where attachid=:1";
                res = pDB.ExecuteQuery("data", sql, enumCommandType.UPDATE, "DB103002", pAttachId, pAttachType, pAttachBlob, pUserNo, pFileName, pDescription);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB103003 - Insert Attach data on specified AttachId ]
        public static Result DB103003(DbConnections pDB, ulong pAttachId, int pAttachType, byte[] pAttachBlob, int pUserNo, string pFileName, string pDescription)
        {
            Result res = null;
            try
            {
                string sql = @"insert into attach (attachid,attachdate,attachtype,attachblob,userno,filename,description) values (:1,sysdate,:2,:3,:4,:5,:6)";
                res = pDB.ExecuteQuery("data", sql, enumCommandType.INSERT, "DB103003", pAttachId, pAttachType, pAttachBlob, pUserNo, pFileName,pDescription);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB103004 - Delete Attach data on specified AttachId ]
        public static Result DB103004(DbConnections pDB, ulong pAttachId)
        {
            Result res = null;
            try
            {
                string sql = @"delete from attach where attachid=:1";
                res = pDB.ExecuteQuery("data", sql, enumCommandType.DELETE, "DB103004", pAttachId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion

        #region [ DB103011 - Select Attach Link records using specified AttachId ]
        public static Result DB103011(DbConnections pDB, ulong pAttachId)
        {
            Result res = null;
            try
            {
                string sql = "select attachid,typecode,typeid from attachlink where attachid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB103011", pAttachId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB103012 - Update Attach Link data on specified AttachId ]
        public static Result DB103012(DbConnections pDB, ulong pAttachId, int pTypeCode, string pTypeId)
        {
            Result res = null;
            try
            {
                string sql = @"update attachlink set typecode=:1,typeid=:2 where attachid=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB103012", pTypeCode, pTypeId, pAttachId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB103013 - Insert Attach Link data on specified AttachId ]
        public static Result DB103013(DbConnections pDB, ulong pAttachId, int pTypeCode, string pTypeId)
        {
            Result res = null;
            try
            {
                string sql = @"insert into attachlink (attachid,typecode,typeid) values (:1,:2,:3)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB103013", pAttachId, pTypeCode, pTypeId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB103014 - Delete Attach Link data on specified AttachId ]
        public static Result DB103014(DbConnections pDB, ulong pAttachId)
        {
            Result res = null;
            try
            {
                string sql = @"delete from attachlink where attachid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB103014", pAttachId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion

        #region [ DB103101 - Select Attach List of specified Linked TypeId ]
        public static Result DB103101(DbConnections pDB, int pTypeCode, string pTypeId, int pAttachType)
        {
            Result res = null;
            string sql = "";
            try
            {
                if (pAttachType >= 0)
                {
                    sql = @"select b.filename,b.attachdate,b.attachtype,b.userno,a.attachid,length(b.attachblob) attachsize, b.description
from attachlink a
left join attach b on a.attachid = b.attachid
where typecode=:1 and typeid=:2
and attachtype=:3";
                    res = pDB.ExecuteQuery("data", sql, enumCommandType.SELECT, "DB103101", pTypeCode, pTypeId, pAttachType);
                }
                else
                {
                    sql = @"select b.filename,b.attachdate,b.attachtype,b.userno,a.attachid,length(b.attachblob) attachsize, b.description
from attachlink a
left join attach b on a.attachid = b.attachid
where typecode=:1 and typeid=:2";
                    res = pDB.ExecuteQuery("data", sql, enumCommandType.SELECT, "DB103101", pTypeCode, pTypeId);
                }
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB103102 - Select Attach Image List of specified Linked TypeId ]
        public static Result DB103102(DbConnections pDB, int pTypeCode, string pTypeId)
        {
            Result res = null;
            string sql = "";
            try
            {
                sql = @"select b.filename,b.attachdate,b.attachtype,b.userno,a.attachid,length(b.attachblob) attachsize,b.description,b.attachblob
from attachlink a
left join attach b on a.attachid = b.attachid
where typecode=:1 and typeid=:2
and attachtype=0";
                res = pDB.ExecuteQuery("data", sql, enumCommandType.SELECT, "DB103102", pTypeCode, pTypeId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #endregion
        #region [ 104 - Additional Data Related ]
        #region [ DB104001 - Select Additional Data definitions with thier values ]
        public static Result DB104001(DbConnections pDB, string pTableNamePrefix, ulong pTypeId, ulong pKey)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"select 
b.itemid,b.name,b.name2,b.valuetype,b.valuelength,b.valuedefault
,b.mandatory,b.editmask,b.description,b.dictid,b.dicteditable
,b.dictvaluefield,b.dictdescfield,b.orderno,a.value,a.attachid
from {0}add b
left join {0}adddata a on b.itemid = a.itemid and a.key=:2
where b.typeid=:1 order by b.orderno, b.itemid"
                    , pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB104001", pTypeId, pKey);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB104002 - Merge Additional Data on specified Key ]
        public static Result DB104002(DbConnections pDB, string pTableNamePrefix, ulong pKey, ulong pId, string pValue, ulong pAttachId)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"merge into {0}adddata a
using (select :1 key,:2 itemid,:3 value,:4 attachid from dual) b
on (a.key=b.key and a.itemid=b.itemid)
when matched then
 update set a.value=b.value, a.attachid=b.attachid
when not matched then
 insert (key,itemid,value,attachid) values(b.key,b.itemid,b.value,b.attachid)"
                    , pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB104002", pKey, pId, pValue, pAttachId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB10403 - Delete All Id values from Additional Data ]
        public static Result DB104003(DbConnections pDB, string pTableNamePrefix, ulong pKey)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"delete from {0}adddata where key=:1", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB104003", pKey);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB10404 - Delete One Id value from Additional Data ]
        public static Result DB104004(DbConnections pDB, string pTableNamePrefix, ulong pKey, ulong pId)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"delete from {0}adddata where key=:1 and itemid=:2", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB104004", pKey, pId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #endregion
        #region [ 105 - Dynamic Grid Related ]

        #region [ DB105001 - Select Definition Table ]
        public static Result DB105001(DbConnections pDB, ulong pTypeId, int pObjectId)
        {
            Result res = null;
            try
            {
                string sql = @"
select oc.itemid,oc.iskey
,nvl(oc.ismandatory,oi.mandatory) ismandatory
,nvl(oc.iscomboeditable, oi.dicteditable) iscomboeditable
,nvl(oc.description,oi.description) description
,nvl(oc.valuedefault,oi.valuedefault) valuedefault
,oc.orderno,oi.name,oi.name2,oi.valuetype,oi.valuelength,oi.editmask
,oi.dictid,oi.dicteditable,oi.dictvaluefield,oi.dictdescfield,oi.calculate,OI.DICTPARENTOBJECT,OI.DICTFILTERDESC
from objectitems oc
left join objectitem oi on oc.itemid=oi.itemid and oi.typeid=:1
where oc.objectid=:2
order by oc.orderno";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB105001", pTypeId,  pObjectId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105002 - Select Row Fixed Values ]
        public static Result DB105002(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId, int pParentRowNo)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"
select a.rowno,b.status,b.startdate,b.enddate
,b.feetype,b.feerate,b.feeamount,b.feecurcode
,b.discountrate,b.discountamount,b.discountcurcode
,b.currate,b.estimateamount,b.estimatecurcode,b.optionid,b.selectstatus,b.feediscounttype,b.feediscountamount,b.feediscountrate,b.calcamount,b.calcrate,b.unoptionid,b.CLAIMAMOUNT,b.marketvalue,b.recordid,b.updatedate
from (
    select ov.rowno
    from objectvalues{0} ov
    where ov.recordid=:1 and ov.objectid=:2 and ov.prowno=:3
    group by ov.rowno
) a
left join objectfixed{0} b 
on a.rowno=b.rowno and recordid=:1 and objectid=:2 and b.prowno=:3
order by a.rowno", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB105002", pRecordId, pObjectId, pParentRowNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105003 - Select Row Dynamic Values ]
        public static Result DB105003(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId, int pParentRowNo)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"
select ov.rowno,ov.itemid,ov.value 
from objectvalues{0} ov
left join objectitems oi on oi.objectid=ov.objectid and oi.itemid=ov.itemid
where ov.recordid=:1 and ov.objectid=:2 and ov.prowno=:3
order by ov.rowno, oi.orderno, ov.itemid", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB105003", pRecordId, pObjectId, pParentRowNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105004 - Select Dynamic Values for Pivot ]
        public static Result DB105004(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId, int pParentRowNo)
        {
            Result res = null;
            try
            {
                #region Prepare SQL
                string sql = string.Format(@"
select aa.rowno,aa.itemid,cc.rate,cc.valuation,cc.startdate,cc.enddate,bb.value 
from 
(
    select a.rowno, b.itemid
    from objectvalues{0} a, objectitems b
    where a.recordid=:1 and a.objectid=:2 and a.prowno=:3
    and a.objectid=b.objectid
    group by a.rowno,b.itemid
) aa
left join (
    select rowno,itemid,value
    from objectvalues{0} 
    where recordid=:1 and objectid=:2 and prowno=:3
) bb
on bb.itemid=aa.itemid and bb.rowno=aa.rowno
left join objectfixed{0} cc
on cc.recordid=:1 and cc.objectid=:2 and cc.prowno=:3 and cc.rowno=aa.rowno", pTableNamePrefix);
                #endregion
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB105004", pRecordId, pObjectId, pParentRowNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105005 - Update Fixed Values ]
        public static Result DB105005(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId, int pParentRowNo, int pRowno
            , int pStatus, DateTime pStartDate, DateTime pEndDate, int pFeeType, decimal pFeeRate, decimal pFeeAmount, string pFeeCurCode
            , decimal pDiscountRate,decimal pDiscountAmount, string pDiscountCurCode
            , decimal pCurRate, decimal pEstimateAmount, string pEstimateCurCode, int pOptionid, int pselectstatus, int pFeeDiscountType, decimal pFeeDisCountAmount, decimal pFeeDisCountRate, decimal pCalcAmount, decimal pcalcrate, int pUnOptionID, decimal pCLAIMAMOUNT, decimal pMarketValue)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"
update objectfixed{0} set status=:5,startdate=:6,enddate=:7
,feetype=:8,feerate=:9,feeamount=:10,feecurcode=:11
,discountrate=:12,discountamount=:13,discountcurcode=:14
,currate=:15,estimateamount=:16,estimatecurcode=:17,optionid=:18,selectstatus=:19,feediscounttype=:20,feediscountamount=:21,feediscountrate=:22,calcamount=:23,calcrate=:24,unoptionid=:25,CLAIMAMOUNT=:26,marketvalue=:27,updatedate=sysdate
where recordid=:1 and objectid=:2 and prowno=:3 and rowno=:4", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB105005"
                    , pRecordId, pObjectId, pParentRowNo, pRowno
                    , pStatus, pStartDate, pEndDate, pFeeType, pFeeRate, pFeeAmount, pFeeCurCode
                    , pDiscountRate,pDiscountAmount,pDiscountCurCode
                    , pCurRate, pEstimateAmount, pEstimateCurCode, pOptionid, pselectstatus, pFeeDiscountType, pFeeDisCountAmount, pFeeDisCountRate, pCalcAmount, pcalcrate,pUnOptionID,pCLAIMAMOUNT,pMarketValue);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105006 - Insert Fixed Values ]
        public static Result DB105006(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId, int pParentRowNo, int pRowno
            , int pStatus, DateTime pStartDate, DateTime pEndDate, int pFeeType, decimal pFeeRate, decimal pFeeAmount, string pFeeCurCode
            , decimal pDiscountRate, decimal pDiscountAmount, string pDiscountCurCode
            , decimal pCurCode, decimal pEstimateAmount, string pEstimateCurCode, int pOptionid, int pselectstatus, int pFeeDiscountType, decimal pFeeDisCountAmount, decimal pFeeDisCountRate, decimal pCalcAmount, decimal pcalcrate, int pUnOptionID, decimal pCLAIMAMOUNT, decimal pMarketValue)
        {
            Result res = null;
            try
            {   
                string sql = string.Format(@"
insert into objectfixed{0} (recordid,objectid,prowno,rowno,status,startdate,enddate
,feetype,feerate,feeamount,feecurcode,discountrate,discountamount,discountcurcode,currate,estimateamount,estimatecurcode,optionid,selectstatus,feediscounttype,feediscountamount,feediscountrate,calcamount,calcrate,unoptionid,CLAIMAMOUNT,MarketValue)
values(:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16,:17,:18,:19,:20,:21,:22,:23,:24,:25,:26,:27)", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB105006"
                    , pRecordId, pObjectId, pParentRowNo, pRowno
                    , pStatus, pStartDate, pEndDate, pFeeType, pFeeRate, pFeeAmount, pFeeCurCode
                    , pDiscountRate, pDiscountAmount, pDiscountCurCode, pCurCode, pEstimateAmount, pEstimateCurCode, pOptionid, pselectstatus, pFeeDiscountType, pFeeDisCountAmount, pFeeDisCountRate, pCalcAmount, pcalcrate,pUnOptionID,pCLAIMAMOUNT,pMarketValue);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105007 - Update Dynamic Values ]
        public static Result DB105007(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId
            , int pParentRowNo, int pRowno, int pItemId, string pValue)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"
update objectvalues{0} set value=:6
where recordid=:1 and objectid=:2 and prowno=:3 and rowno=:4 and itemid=:5", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB105007"
                    , pRecordId, pObjectId, pParentRowNo, pRowno, pItemId
                    , pValue);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105008 - Insert Dynamic Values ]
        public static Result DB105008(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId
            , int pParentRowNo, int pRowno, int pItemId, string pValue)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"
insert into objectvalues{0} (recordid,objectid,prowno,rowno,itemid,value)
values(:1,:2,:3,:4,:5, :6)", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB105008"
                    , pRecordId, pObjectId, pParentRowNo, pRowno, pItemId, pValue);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105009 - Delete Dynamic Values of specified rowno ]
        public static Result DB105009(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId
            , int pParentRowNo, int pRowno)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"delete objectvalues{0} where recordid=:1 and objectid=:2 and prowno=:3 and rowno=:4", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB105009"
                    , pRecordId, pObjectId, pParentRowNo, pRowno);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105010 - Delete Fixed Values of specified rowno ]
        public static Result DB105010(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId
            , int pParentRowNo, int pRowno)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"delete objectfixed{0} where recordid=:1 and objectid=:2 and prowno=:3 and rowno=:4", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB105010"
                    , pRecordId, pObjectId, pParentRowNo, pRowno);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105011 - Get Max RowNo of Fixed Values ]
        public static Result DB105011(DbConnections pDB, string pTableNamePrefix, ulong pRecordId, int pObjectId, int pParentRowNo)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"select nvl(max(rowno),0) rowno from objectfixed{0} where recordid=:1 and objectid=:2 and prowno=:3", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB105011", pRecordId, pObjectId, pParentRowNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105012 - Select Row Fixed Values for ProdCode]
        public static Result DB105012(DbConnections pDB, string pTableNamePrefix, ulong pProdCode,int pObjectID)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"select a.rowno,b.status,b.startdate,b.enddate
,b.feetype,b.feerate,b.feeamount,b.feecurcode
,b.discountrate,b.discountamount,b.discountcurcode
,b.currate,b.estimateamount,b.estimatecurcode,b.optionid,b.selectstatus,b.feediscounttype,b.feediscountamount,b.feediscountrate,b.calcamount,b.calcrate,b.unoptionid,b.CLAIMAMOUNT,b.marketvalue,b.recordid,b.repayamount
from (
    select ov.rowno
    from objectvalues{0} ov
    left join deal d on ov.recordid=d.dealno 
      where d.prodcode=:1 and ov.objectid=:2  and ov.prowno=0
    and d.status=2 
    and nvl(D.FIRSTPAYMENTAMOUNT,0)>0
    group by ov.rowno
) a
left join objectfixed{0} b 
on a.rowno=b.rowno   and b.prowno=0  and objectid=:2
left join deal d on b.recordid=d.dealno and d.prodcode=:1
where d.prodcode=:1
    and d.status=2 
    and nvl(D.FIRSTPAYMENTAMOUNT,0)>0
and nvl(b.repayamount,0)=0
order by a.rowno", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB105012", pProdCode,pObjectID);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105013 - Select Row Dynamic Values[Prodcode] ]
        public static Result DB105013(DbConnections pDB, string pTableNamePrefix, ulong pProdCode,int pObjectID)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"select ov.rowno,ov.itemid,ov.value 
from objectvalues{0} ov
left join objectitems oi on oi.objectid=ov.objectid and oi.itemid=ov.itemid
left join objectfixed{0} o on ov.recordid=o.recordid and ov.rowno=o.rowno and o.prowno=0
    left join deal d on ov.recordid=d.dealno 
       where d.prodcode=:1  and ov.prowno=0 and  ov.objectid=:2
       and d.status=2 
       and nvl(D.FIRSTPAYMENTAMOUNT,0)>0
       and nvl(o.repayamount,0)=0
order by ov.rowno, oi.orderno, ov.itemid", pTableNamePrefix);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB105013", pProdCode,pObjectID);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB105101 - Select All Dynamic Parameter Info ]

        public static Result DB105101(DbConnections pDB, string pTableName, ulong pTypeId)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"select itemid,name,name2
,valuetype,valuelength,valuedefault,mandatory,editmask
,description,dictid,dicteditable,dictvaluefield,dictdescfield,orderno,calculate,DICTPARENTOBJECT,DICTFILTERDESC
from {0} where typeid=:1", pTableName);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB105101", pTypeId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }

        #endregion
        #region [ DB105102 - Merge Dynamic Parameter Info ]

        public static Result DB105102(DbConnections pDB, string pTableName, ulong pTypeId, int pItemId, string pName, string pName2
            , int pVType, int pVLength, string pVDefault, int pMandatory, string pEditMask, string pDesc
            , string pDictId, int pDictEditable, string pDictValueField, string pDictDescField, int pOrderNo,int pCalculate,long pDICTPARENTOBJECT, string pDICTFILTERDESC)
        {
            Result res = null;
            Result ret = new Result();
            try
            {
                string sql = string.Format(@"
merge into {0} a
using 
(
select :1 typeid, :2 itemid,:3 name,:4 name2,:5 valuetype,:6 valuelength,:7 valuedefault
,:8 mandatory,:9 editmask,:10 description,:11 dictid,:12 dicteditable,:13 dictvaluefield
,:14 dictdescfield,:15 orderno,:16 calculate,:17 DICTPARENTOBJECT,:18 DICTFILTERDESC
from dual
) b
on (a.typeid=b.typeid and a.itemid=b.itemid)
when matched then
  update set a.name=b.name,a.name2=b.name2,a.valuetype=b.valuetype
  ,a.valuelength=b.valuelength,a.valuedefault=b.valuedefault,a.mandatory=b.mandatory,a.editmask=b.editmask
  ,a.description=b.description,a.dictid=b.dictid,a.dicteditable=b.dicteditable
  ,a.dictvaluefield=b.dictvaluefield,a.dictdescfield=b.dictdescfield,a.orderno=b.orderno,a.calculate=b.calculate,A.DICTPARENTOBJECT,B.DICTFILTERDESC
when not matched then
  insert (typeid,itemid,name,name2,valuetype,valuelength,valuedefault,mandatory,editmask
  ,description,dictid,dicteditable,dictvaluefield,dictdescfield,orderno,calculate,DICTPARENTOBJECT,DICTFILTERDESC) 
  values (b.typeid,b.itemid,b.name,b.name2,b.valuetype,b.valuelength,b.valuedefault,b.mandatory,b.editmask
  ,b.description,b.dictid,b.dicteditable,b.dictvaluefield,b.dictdescfield,b.orderno,b.calculate,DICTPARENTOBJECT,DICTFILTERDESC)", pTableName);

                object[] param = new object[] { 
                pTypeId, pItemId, pName, pName2, pVType, pVLength, pVDefault, pMandatory, pEditMask
                , pDesc, pDictId, pDictEditable, pDictValueField, pDictDescField, pOrderNo,pCalculate, pDICTPARENTOBJECT,pDICTFILTERDESC
                };
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB105102", param);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }

        #endregion
        #region [ DB105103 - Delete Dynamic Parameter Info ]

        public static Result DB105103(DbConnections pDB, string pTableName, ulong pTypeId, int pItemId)
        {
            Result res = null;
            try
            {
                string sql = string.Format(@"delete from {0} where typeid=:1 and itemid=:2", pTableName);
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB105103",pTypeId, pItemId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }

        #endregion
        #region [ DB105104 - Update Dynamic Parameter Info ]

        public static Result DB105104(DbConnections pDB, string pTableName, ulong pTypeId, int pItemId, string pName, string pName2
            , int pVType, int pVLength, string pVDefault, int pMandatory, string pEditMask, string pDesc
            , string pDictId, int pDictEditable, string pDictValueField, string pDictDescField, int pOrderNo, int pCalculate, long pDICTPARENTOBJECT, string pDICTFILTERDESC)
        {
            Result res = null;
            Result ret = new Result();
            try
            {
                string sql = string.Format(@"
update {0} a set
a.name=:3,a.name2=:4,a.valuetype=:5
,a.valuelength=:6,a.valuedefault=:7,a.mandatory=:8,a.editmask=:9
,a.description=:10,a.dictid=:11,a.dicteditable=:12
,a.dictvaluefield=:13,a.dictdescfield=:14,a.orderno=:15,calculate=:16,DICTPARENTOBJECT=:17,DICTFILTERDESC=:18
where a.typeid=:1 and a.itemid=:2", pTableName);

                object[] param = new object[] { 
                pTypeId, pItemId, pName, pName2, pVType, pVLength, pVDefault, pMandatory, pEditMask
                , pDesc, pDictId, pDictEditable, pDictValueField, pDictDescField, pOrderNo,pCalculate,pDICTPARENTOBJECT,pDICTFILTERDESC
                };
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB105104", param);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }

        #endregion
        #region [ DB105105 - Insert Dynamic Parameter Info ]

        public static Result DB105105(DbConnections pDB, string pTableName, ulong pTypeId, int pItemId, string pName, string pName2
            , int pVType, int pVLength, string pVDefault, int pMandatory, string pEditMask, string pDesc
            , string pDictId, int pDictEditable, string pDictValueField, string pDictDescField, int pOrderNo, int pCalculate,long pDICTPARENTOBJECT,string pDICTFILTERDESC)
        {
            Result res = null;
            Result ret = new Result();
            try
            {
                string sql = string.Format(@"
insert into {0} a
(typeid,itemid,name,name2,valuetype,valuelength,valuedefault,mandatory,editmask
  ,description,dictid,dicteditable,dictvaluefield,dictdescfield,orderno,calculate,DICTPARENTOBJECT,DICTFILTERDESC ) 
values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16,:17,:18)", pTableName);

                object[] param = new object[] { 
                pTypeId, pItemId, pName, pName2, pVType, pVLength, pVDefault, pMandatory, pEditMask
                , pDesc, pDictId, pDictEditable, pDictValueField, pDictDescField, pOrderNo,pCalculate,pDICTPARENTOBJECT,pDICTFILTERDESC
                };
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB105105", param);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }

        #endregion
        
        #endregion
        #region [ 106 - Dictionary Data ]
        #region [ DB106001 - Select Dictionary Info ]
        public static Result DB106001(DbConnections pDB, string pDictId)
        {
            Result res = null;
            try
            {
                string sql = @"select id,name,description,refreshinterval
from dictionary where upper(id)=upper(:1)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB106001", pDictId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB106002 - Select Dictionary Info with Query Table ]
        public static Result DB106002(DbConnections pDB, string pDictId)
        {
            Result res = null;
            Result ret = new Result();
            try
            {
                ISM.Lib.Static.WriteToLogFile("ISM.Template", pDictId + "=resultno");
                string sql = @"select upper(id) as id,name,description,refreshinterval,upper(sql) as sql from dictionary where upper(id)=upper(:1)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB106002", pDictId);
                if (res.ResultNo == 0)
                {
                    ISM.Lib.Static.WriteToLogFile("ISM.Template", res.AffectedRows+ "=resultno");
                    if (res.AffectedRows <= 0)
                    {  
                        res.ResultNo = 106002;
                        res.ResultDesc = string.Format("There is no distionary information such as {0}.", pDictId);
                        goto OnExit;
                    }

                    DataTable dt1 = res.Data.Tables[0];
                    dt1.TableName = "Table1";

                    sql = (string)dt1.Rows[0]["sql"];
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB106002b");
                    if (res.ResultNo != 0) goto OnExit;

                    DataTable dt2 = res.Data.Tables[0];
                    dt2.TableName = "Table2";

                    ret.Data = new DataSet();
                    ret.Data.Tables.Add(dt1.Copy());
                    ret.Data.Tables.Add(dt2.Copy());
                    dt1.Dispose();
                    dt2.Dispose();

                    res = ret;
                }
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            OnExit:
            return res;
        }
        #endregion
        #region [ DB106003 - Select All Dictionary Info ]

        public static Result DB106003(DbConnections pDB)
        {
            Result res = null;
            try
            {
                string sql = @"select id,name,fieldnames from dictionary";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB106003");
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }

        #endregion
        #endregion
        #region [ 107 - Report Dynamic Query ]
        #region [ DB107001 - Execute Client Report Query ]

 

        public static Result DB107001(DbConnections pDB, string sql, string[] paramnames, object[] paramvalues)
        {
            Result res = new Result();
            try
            {

              
                #region Validation
                if (paramnames == null || paramvalues == null )
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Биелүүлэх скриптний параметр тодорхойлогдоогүй байна.";
                    return res;
                }
                if (paramnames.Length != paramvalues.Length) 
                {
                    res.ResultNo = 9110003;
                    res.ResultDesc = "Биелүүлэх скриптний параметрийн нэр, утга тохирохгүй байна.";
                    return res;
                }
                #endregion
                #region Prepare command
                Oracle.DataAccess.Client.OracleCommand cmd = new Oracle.DataAccess.Client.OracleCommand();
                cmd.CommandText = sql;
     
 
                for (int i = 0; i < paramnames.Length; i++)
                {
                    cmd.Parameters.Add(paramnames[i], paramvalues[i]);

                } 

              
                #endregion
                #region Execute user query
                IDbCommand icmd = cmd;
                res = pDB.ExecuteQuery("report", icmd, enumCommandType.SELECT, "DB107001");
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }



        }
        #endregion
        #endregion
        #region [ 1071xx - Chart Reporting ]
        #region [ DB107101 - Insert Chart Report Definition ]
        public static Result DB107101(DbConnections pDB, string[] rid, int[] rows, int[] cols, int[] types, string[] values, string[] dtypes, int[] dvalues, string[] branches, string[] currs, string[] tocurrs, int[] rounds)
        {
            Result res = new Result();
            try
            {
                #region Prepare SQL
                string sql = @"insert into chartrptcell (reportid,rowno,colno,celltype,value,datetype,datevalue,branch,currency,tocurrency,round)
values(:reportid,:rowno,:colno,:celltype,:value,:datetype,:datevalue,:branch,:currency,:tocurrency,:round)";
                #endregion
                #region Prepare command
                Oracle.DataAccess.Client.OracleCommand cmd = new Oracle.DataAccess.Client.OracleCommand();
                cmd.CommandText = sql;

                cmd.Parameters.Add("reportid", Oracle.DataAccess.Client.OracleDbType.Varchar2, rid, ParameterDirection.Input);
                cmd.Parameters.Add("rowno", Oracle.DataAccess.Client.OracleDbType.Int32, rows, ParameterDirection.Input);
                cmd.Parameters.Add("colno", Oracle.DataAccess.Client.OracleDbType.Int32, cols, ParameterDirection.Input);
                cmd.Parameters.Add("celltype", Oracle.DataAccess.Client.OracleDbType.Int32, types, ParameterDirection.Input);
                cmd.Parameters.Add("value", Oracle.DataAccess.Client.OracleDbType.Varchar2, values, ParameterDirection.Input);
                cmd.Parameters.Add("datetype", Oracle.DataAccess.Client.OracleDbType.Varchar2, dtypes, ParameterDirection.Input);
                cmd.Parameters.Add("datevalue", Oracle.DataAccess.Client.OracleDbType.Int32, dvalues, ParameterDirection.Input);
                cmd.Parameters.Add("branch", Oracle.DataAccess.Client.OracleDbType.Varchar2, branches, ParameterDirection.Input);
                cmd.Parameters.Add("currency", Oracle.DataAccess.Client.OracleDbType.Varchar2, currs, ParameterDirection.Input);
                cmd.Parameters.Add("tocurrency", Oracle.DataAccess.Client.OracleDbType.Varchar2, tocurrs, ParameterDirection.Input);
                cmd.Parameters.Add("round", Oracle.DataAccess.Client.OracleDbType.Int32, rounds, ParameterDirection.Input);
                cmd.ArrayBindCount = rid.Length;
                cmd.BindByName = true;

                #endregion
                #region Execute query
                IDbCommand icmd = cmd;
                res = pDB.ExecuteQuery("core", icmd, enumCommandType.INSERT, "DB107101");
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB107102 - Delete Chart Report Definition ]
        public static Result DB107102(DbConnections pDB, string rid)
        {
            Result res = new Result();
            try
            {
                #region Prepare SQL
                string sql = @"delete from chartrptcell where reportid=:1";
                #endregion
                #region Execute query
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB107102", rid);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                res.Data = null;
                res.Param = null;
                return res;
            }
        }
        #endregion
        #region [ DB107103 - Select Chart Report Txn Description ]
        public static Result DB107103(DbConnections pDB, int trancode)
        {
            Result res = new Result();
            try
            {
                #region Prepare SQL
                string sql = @"select name,name2 from txn where trancode=:1";
                #endregion
                #region Execute query
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB107103", trancode);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        
        #region [ DB107104 - Insert Chart Report Node ]
        public static Result DB107104(DbConnections pDB, string[] rid, string[] rid2, DateTime[] changed)
        {
            Result res = new Result();
            try
            {
                #region Prepare SQL
                string sql = @"insert into chartrptnode (reportid,reportid2,postdate) values(:reportid,:reportid2,:postdate)";
                #endregion
                #region Prepare command
                Oracle.DataAccess.Client.OracleCommand cmd = new Oracle.DataAccess.Client.OracleCommand();
                cmd.CommandText = sql;

                cmd.Parameters.Add("reportid", Oracle.DataAccess.Client.OracleDbType.Varchar2, rid, ParameterDirection.Input);
                cmd.Parameters.Add("reportid2", Oracle.DataAccess.Client.OracleDbType.Varchar2, rid2, ParameterDirection.Input);
                cmd.Parameters.Add("postdate", Oracle.DataAccess.Client.OracleDbType.Date, changed, ParameterDirection.Input);
                cmd.ArrayBindCount = rid.Length;
                cmd.BindByName = true;

                #endregion
                #region Execute query
                IDbCommand icmd = cmd;
                res = pDB.ExecuteQuery("core", icmd, enumCommandType.INSERT, "DB107104");
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB107105 - Delete Chart Report Node ]
        public static Result DB107105(DbConnections pDB, string rid)
        {
            Result res = new Result();
            try
            {
                #region Prepare SQL
                string sql = @"delete from chartrptnode where reportid=:1";
                #endregion
                #region Execute query
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB107105", rid);
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion

        #region [ DB107106 - Select Chart Report Nodes ]
        public static Result DB107106(DbConnections pDB, string rid)
        {
            Result res = new Result();
            try
            {
                #region Prepare SQL
                string sql = @"select cast(reportid2 as nvarchar2(20)) reportid2, level from chartrptnode 
start with reportid = :1
connect by prior reportid2 = reportid
union all
select :1, 0 from dual
order by 2 desc";
                #endregion
                #region Execute query
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB107106", rid);
                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            return res;
        }
        #endregion
        #region [ DB107107 - Generate Chart Report ]
        public static Result DB107107(DbConnections pDB, string rid, DateTime rdate, out DataTable table)
        {
            Result res = new Result();
            DataTable dt = null;
            try
            {
                #region Prepare SQL
                string sql = @"declare
    type cachetype is table of number index by varchar2(100);
    prid           varchar2(50)     := :reportid;
    prdate         date             := :reportdate;
    cursor cur is 
        select reportid,rowno,colno,value,branch,format
        ,nvl(celltype,0) celltype
        ,nvl(datetype,'M') datetype
        ,nvl(datevalue,0) datevalue
        ,nvl(currency,'MNT') currency
        ,nvl(tocurrency,'MNT') tocurrency
        ,decode(round,null,1,0,1,round) round
        ,nvl(visible,1) visible
        from chartrptcell a
        left join (
            select reportid2, level from chartrptnode 
            start with reportid = prid
            connect by prior reportid2 = reportid
            union all
            select prid, 0 from dual
        ) b on b.reportid2=a.reportid
        where b.reportid2 is not null;

    pat1           varchar2(100)    := '(^|[-+%*/ ])([YQMD][ODCBNA][EY][0-9]+)($?)';
    pat2           varchar2(100)    := '[YQMD][ODCBNA][EY][0-9]+';

    tbl            ChartRptTable := ChartRptTable();
    mem            cachetype;
    key            varchar2(2000);
    fnd            int              := 0;
    pos            int              := 1;
    src            varchar2(2000);
    bal            varchar2(2000);
    ret            varchar2(2000);
    val            number(18,5);
    rat            number(18,5);
    yer            number(4);
    mnt            number(2);
    dte            date;

    ob             number(18,5);
    dt1            number(18,5);
    ct1            number(18,5);
    dt2            number(18,5);
    ct2            number(18,5);
    cb             number(18,5);
    ab             number(18,5);

    procedure bal_mo(pd date,pb varchar2,pc varchar2,pa varchar2,py number, pm number,pob out number) as
    begin
        select nvl(sum(openbal*nvl(c.rate,1)),0) endbal
        into pob
        from (
            select branchno,curcode,account,year,month,openbal
            ,row_number() over (partition by branchno,curcode,account order by month desc) rank
            from chartmonthbal
            where year=py and month<=pm and regexp_like(branchno,pb) and regexp_like(curcode,pc) and account like pa||'%'
            order by curcode
        ) a
        left join currencyhist c on c.currency=a.curcode and c.curdate=pd
        where a.rank=1;
    exception when others then
        pob:=0;
    end;

    procedure bal_mc(pd date,pb varchar2,pc varchar2,pa varchar2,py number, pm number,pcb out number) as
    begin
        --select nvl(sum(endbal*nvl(c.rate,1)),0) endbal
        --into pcb
        --from (
        --    select branchno,curcode,account,year,month,endbal
        --    ,row_number() over (partition by branchno,curcode,account order by month desc) rank
        --    from chartmonthbal
        --    where year=py and month<=pm and regexp_like(branchno,pb) and regexp_like(curcode,pc) and account like pa||'%'
        --    order by curcode
        --) a
        --left join currencyhist c on c.currency=a.curcode and c.curdate=pd
        --where a.rank=1;
        
        select nvl(sum(endbal*nvl(c.rate,1)),0) - nvl(sum(nvl(d.amount,0)),0) endbal
        into pcb
        from (
        select branchno,curcode,account,year,month,endbal
            ,row_number() over (partition by branchno,curcode,account order by month desc) rank
            from chartmonthbal
            where year=py and month<=pm and regexp_like(branchno,pb) and regexp_like(curcode,pc) and account like pa||'%'
            order by curcode
        ) a
        left join currencyhist c on c.currency=a.curcode and c.curdate=pd
        left join (select to_char(pd, 'yyyy')||'12' as month, branchno, accountno, currcode, sum(amount*rate) as amount
                            from charttxn 
                            where upper(description) = 'ОНЫ ХААЛТ' and corr=0
                            and to_char(txndate, 'yyyymmdd')=to_char(pd, 'yyyy')||'1231'
                            and regexp_like(branchno,pb) 
                            and regexp_like(currcode,pc) 
                            and accountno like pa||'%'
                            group by to_char(pd, 'yyyy')||'12', branchno, accountno, currcode) d on a.branchno=d.branchno and a.account=d.accountno and a.curcode=d.currcode and a.year||a.month = d.month
        where a.rank=1;

    exception when others then
        pcb:=0;
    end;

    procedure bal_mt(pd date,pb varchar2,pc varchar2,pa varchar2,py number,pm1 number,pm2 number,pdt out number,pct out number) as
    begin
        -- -  C
        -- +  D

        /*select nvl(sum(dtbal*nvl(c.rate,1)),0) dtbal,nvl(sum(ctbal*nvl(c.rate,1)),0) ctbal
        into pdt,pct
        from chartmonthbal a
        left join currencyhist c on c.currency=a.curcode and c.curdate=pd
        where year=py and month between pm1 and pm2 and regexp_like(branchno,pb) and regexp_like(curcode,pc) and account like pa||'%';
        */

        select  aa.dtbal+nvl((select sum(nvl(amount,0))  from chartadjust
                            where pd between startdate and enddate and entry='D' and regexp_like(branchno,pb)
                            and regexp_like(curcode,pc) and account=pa),0) as dtbal, 
                    aa.ctbal-nvl((select sum(nvl(amount,0))  from chartadjust
                            where pd between startdate and enddate and entry='C' and regexp_like(branchno,pb)
                            and regexp_like(curcode,pc)  and account=pa),0) as ctbal
        into pdt,pct
        from
        (
        select 
        nvl(sum(dtbal*nvl(c.rate,1)),0) dtbal1,
        nvl(sum(dtbal*nvl(c.rate,1)),0)-decode(sum(nvl(d.amount, 0))-abs(sum(nvl(d.amount, 0))), 0, sum(nvl(d.amount, 0)), 0) dtbal,
        nvl(sum(ctbal*nvl(c.rate,1)),0) ctbal1,
        nvl(sum(ctbal*nvl(c.rate,1)),0)-decode(sum(nvl(d.amount, 0))-abs(sum(nvl(d.amount, 0))), 0, 0, sum(nvl(d.amount, 0))) ctbal
                from chartmonthbal a
                left join currencyhist c on c.currency=a.curcode and c.curdate=pd
                left join (select to_char(pd, 'yyyy')||'12' as month, branchno, accountno, currcode, sum(amount*rate) as amount
                            from charttxn 
                            where upper(description) = 'ОНЫ ХААЛТ' and corr=0
                            and to_char(txndate, 'yyyymmdd')=to_char(pd, 'yyyy')||'1231'
                            and regexp_like(branchno,pb) 
                            and regexp_like(currcode,pc) 
                            and accountno like pa||'%'
                            group by to_char(pd, 'yyyy')||'12', branchno, accountno, currcode) d on a.branchno=d.branchno and a.account=d.accountno and a.curcode=d.currcode and a.year||a.month = d.month
                where a.year=py and a.month between pm1 and pm2 and regexp_like(a.branchno,pb) and regexp_like(a.curcode,pc) and a.account like pa||'%'
        ) aa;

    exception when others then
        pdt:=0;pct:=0;
    end;

    procedure bal_ma(pd date,pb varchar2,pc varchar2,pa varchar2,py number,pm number,pab out number) as
    begin
        select trunc(nvl(sum(endbal*frq),0)/nvl(sum(frq),1),5) abal
        into pab
        from (
           select endbal*nvl(cc.rate,1) endbal
           ,nvl(lead(txndate) over (partition by branchno,curcode,account order by txndate),txndate+1)-txndate frq
           from chartbalancedaily bb
           left join currencyhist cc on cc.currency=bb.curcode and cc.curdate=pd
           where extract(year from txndate)=py and extract(month from txndate)=pm
           and regexp_like(branchno,pb) and regexp_like(curcode,pc) and account like pa||'%'
        ) a;
    exception when others then
        pab:=0;
    end;

    procedure bal_db(pd date,pb varchar2,pc varchar2,pa varchar2,pob out number,pdt1 out number,pct1 out number,pdt2 out number,pct2 out number,pcb out number,pab out number) as
    begin
        
        --select 
        --        nvl(sum((nvl(endbal,0)-nvl(dtbal,0)-nvl(ctbal,0))*nvl(c.rate,1)), 0) ob
        --        ,nvl(sum(nvl(dtbal,0)*nvl(c.rate,1)), 0) dtbal
        --        ,nvl(sum(nvl(ctbal,0)*nvl(c.rate,1)), 0) ctbal
        --        ,nvl(sum(nvl(endbal,0)*nvl(c.rate,1)), 0) curbal
        --       into pob,pdt1,pct1,pcb
        --from
        --(       select branchno,curcode,account,txndate,endbal,dtbal,ctbal,
        --            row_number() over (partition by branchno,curcode,account order by txndate desc) rank
        --        from chartbalancedaily bb
        --        where txndate<=pd and regexp_like(branchno,pb) and regexp_like(curcode,pc) and account like pa||'%'
        --        order by curcode
        --) a
        --left join currencyhist c on c.currency=a.curcode and c.curdate=pd
        --where a.rank=1;
        
        select 
                nvl(sum((nvl(endbal,0)-nvl(dtbal,0)-nvl(ctbal,0))*nvl(c.rate,1)), 0) ob
                ,nvl(sum(nvl(dtbal,0)*nvl(c.rate,1)), 0) dtbal
                ,nvl(sum(nvl(ctbal,0)*nvl(c.rate,1)), 0) ctbal
                ,nvl(sum(nvl(endbal,0)*nvl(c.rate,1)), 0) - nvl(sum(nvl(d.amount,0)),0) curbal
                into pob,pdt1,pct1,pcb
        from
        (       select branchno,curcode,account,txndate,endbal,dtbal,ctbal,
                    row_number() over (partition by branchno,curcode,account order by txndate desc) rank
                from chartbalancedaily bb
                where txndate<=pd and regexp_like(branchno,pb) and regexp_like(curcode,pc) and account like pa||'%'
                order by curcode
        ) a
        left join currencyhist c on c.currency=a.curcode and c.curdate=pd
        left join (select to_char(pd, 'yyyy')||'12' as month, branchno, accountno, currcode, sum(amount*rate) as amount
                            from charttxn 
                            where upper(description) = 'ОНЫ ХААЛТ' and corr=0
                            and to_char(txndate, 'yyyymmdd')=to_char(pd, 'yyyy')||'1231'
                            and regexp_like(branchno,pb) 
                            and regexp_like(currcode,pc) 
                            and accountno like pa||'%'
                            group by to_char(pd, 'yyyy')||'12', branchno, accountno, currcode) d on a.branchno=d.branchno and a.account=d.accountno and a.curcode=d.currcode and to_char(a.txndate, 'yyyymm') = d.month
        where a.rank=1;

        pdt2:=pdt1; pct2:=pct1; pab:=pcb;

    exception when others then
        pob:=0;pdt1:=0;pct1:=0;pdt2:=0;pct2:=0;pcb:=0;pab:=0;
    end;
   
    function getbalance(pd date,branch varchar2,curr varchar2,account varchar2) return number as
      trmtype varchar2(4)  := substr(account, 1, 1);
      baltype varchar2(4)  := substr(account, 2, 1);
      sumtype varchar2(4)  := substr(account, 3, 1);
      pa varchar2(10)      := substr(account, 4);
      pb varchar2(500)     := replace(branch,',','|');
      pc varchar2(500)     := replace(curr,',','|');
      yy int;
      mm1 int;
      mm2 int;
      ret     number(18,5) := 0;
    begin
      if (trmtype = 'Y') then
         yy  := extract(year from pd);
         mm2 := extract(month from pd);
         mm1 := 1;
         if (baltype = 'O') then
            bal_mo(pd,pb,pc,pa,yy,mm1,ob);
            ret:=ob;
         elsif (baltype = 'B') then
            bal_mc(pd,pb,pc,pa,yy,mm2,cb);
            ret:=cb;
         elsif (baltype = 'D') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=dt1;
         elsif (baltype = 'C') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=ct1;
         elsif (baltype = 'N') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=dt1+ct1;
         elsif (baltype = 'A') then
            bal_ma(pd,pb,pc,pa,yy,mm1,ab);
            ret:=ab;
         end if;
      elsif(trmtype = 'Q') then
         yy  := extract(year from pd);
         mm2 := (trunc((extract(month from pd)-1)/3)+1)*3;
         mm1 := mm2-2;
         if (mm2>extract(month from pd)) then 
            mm2:=extract(month from pd);
         end if;
         if (baltype = 'O') then
            bal_mo(pd,pb,pc,pa,yy,mm1,ob);
            ret:=ob;
         elsif (baltype = 'B') then
            bal_mc(pd,pb,pc,pa,yy,mm2,cb);
            ret:=cb;
         elsif (baltype = 'D') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=dt1;
         elsif (baltype = 'C') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=ct1;
         elsif (baltype = 'N') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=dt1+ct1;
         elsif (baltype = 'A') then
            bal_ma(pd,pb,pc,pa,yy,mm1,ab);
            ret:=ab;
         end if;
      elsif(trmtype = 'M') then
         yy  := extract(year from pd);
         mm2 := extract(month from pd);
         mm1 := mm2;
         if (baltype = 'O') then
            bal_mo(pd,pb,pc,pa,yy,mm1,ob);
            ret:=ob;
         elsif (baltype = 'B') then
            bal_mc(pd,pb,pc,pa,yy,mm2,cb);
            ret:=cb;
         elsif (baltype = 'D') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=dt1;
         elsif (baltype = 'C') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=ct1;
         elsif (baltype = 'N') then
            bal_mt(pd,pb,pc,pa,yy,mm1,mm2,dt1,ct1);
            ret:=dt1+ct1;
         elsif (baltype = 'A') then
            bal_ma(pd,pb,pc,pa,yy,mm1,ab);
            ret:=ab;
         end if;
      elsif(trmtype = 'D') then
         bal_db(pd,pb,pc,pa,ob,dt1,ct1,dt2,ct2,cb,ab);
         if (baltype = 'B') then ret:=cb;
         elsif (baltype = 'D') then if (sumtype='Y') then ret:=dt2; else ret:=dt1; end if;
         elsif (baltype = 'C') then if (sumtype='Y') then ret:=ct2; else ret:=ct1; end if;
         elsif (baltype = 'N') then if (sumtype='Y') then ret:=dt2+ct2; else ret:=dt1+ct1; end if;
         elsif (baltype = 'O') then ret:=ob;
         elsif (baltype = 'A') then ret:=ab;
         end if;
      end if;

      return ret;
    end;
begin
    for row in cur loop
        if (row.celltype = 1 and row.value is not null) then
            ret := '';fnd:=0;pos:=1;
            loop
                src := regexp_substr(row.value, pat1, pos, 1);
                fnd := regexp_instr(row.value, pat1, pos, 1);
                exit when fnd = 0;
                if (fnd > pos) then ret := ret || substr(row.value,pos,fnd-pos); end if;

                bal := regexp_substr(src, pat2, 1, 1);
                key := bal||';'||row.branch||';'||row.currency||';'||row.datetype||';'||row.datevalue;
                begin
                  val := mem(key);
                exception when others then
                  dte:=prdate;
                  if (nvl(row.datevalue,0)<>0) then
                     if (row.datetype='Y') then dte := add_months(prdate,row.datevalue*12);
                        dte:=last_day(add_months(dte,12-extract(month from dte)));
                     elsif (row.datetype='Q') then dte := add_months(prdate,row.datevalue*3);
                        dte:=add_months(dte,trunc((extract(month from dte)-1)/3+1)*3 - extract(month from dte));
                        dte:=last_day(dte);
                     elsif (row.datetype='M') then dte := add_months(prdate,row.datevalue);
                        dte:=last_day(dte);
                     else dte := prdate+row.datevalue;
                     end if;
                  end if;
                  val := getbalance(dte,row.branch,row.currency,bal);
                  mem(key) := val;
                end;

                rat := 1;
                val:=nvl(val,0);
                if (val<>0 and nvl(row.tocurrency,'MNT')<>'MNT') then
                begin
                  select rate into rat
                  from currencyhist where curdate=dte and currency=nvl(row.tocurrency,'MNT');
                exception when others then
                  rat := 1;
                end;
                end if;
                val := val/rat/nvl(row.round,1);

                bal := regexp_replace(src, pat2, val);
                ret := ret || bal;
                pos := fnd + length(src);
          end loop;
          fnd := length(row.value);
          ret := ret || substr(row.value, pos, fnd-pos+1);
          src := ret;
        else
          src := row.value;
        end if;
        tbl.extend();
        tbl(tbl.count) := ChartRptObject(
          row.reportid,row.rowno,row.colno,row.celltype
         ,src,row.datetype,row.datevalue,row.branch
         ,row.currency,row.tocurrency,row.format
        );
    end loop;
    open :reporttable for select * from table (cast (tbl as ChartRptTable));
end;";
                #endregion
                #region Execute query

                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;

                cmd.Parameters.Add("reportid", OracleDbType.Varchar2, rid, ParameterDirection.Input);
                cmd.Parameters.Add("reportdate", OracleDbType.Date, rdate, ParameterDirection.Input);
                cmd.Parameters.Add("reporttable", OracleDbType.RefCursor, ParameterDirection.Output);

                res = pDB.ExecuteQuery("core", cmd, enumCommandType.SELECT, "DB107107");
                if (res.ResultNo == 0) dt = res.Data.Tables[0];

                #endregion
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
            }
            table = dt;
            return res;
        }
        #endregion
        #endregion
        #region [ 108 - Dynamic Step ]
        #region [ DB108001 - Dynamic Step List ]
        public static Result DB108001(DbConnections pDB)
        {
            Result res = null;
            try
            {
                string sql = @"select stepid,name,name2,note,note2 from step order by 1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB108001");
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108002 - Dynamic Step Update ]
        public static Result DB108002(DbConnections pDB, int pStepId, string pName, string pName2, string pNote, string pNote2)
        {
            Result res = null;
            try
            {
                string sql = @"update step set name=:2,name2=:3,note=:4,note2=:5 where stepid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB108002", pStepId, pName, pName2, pNote, pNote2);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108003 - Dynamic Step Insert ]
        public static Result DB108003(DbConnections pDB, int pStepId, string pName, string pName2, string pNote, string pNote2)
        {
            Result res = null;
            try
            {
                string sql = @"insert into step (stepid,name,name2,note,note2) values(:1,:2,:3,:4,:5)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB108003", pStepId, pName, pName2, pNote, pNote2);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108004 - Dynamic Step Delete ]
        public static Result DB108004(DbConnections pDB, int pStepId)
        {
            Result res = null;
            try
            {
                string sql = @"delete from step where stepid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB108004", pStepId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion

        #region [ DB108101 - Dynamic Step Item List ]
        public static Result DB108101(DbConnections pDB)
        {
            Result res = null;
            try
            {
                string sql = @"select stepitemid,name,name2,note,note2 from stepitem order by 1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB108101");
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108102 - Dynamic Step Item Update ]
        public static Result DB108102(DbConnections pDB, int pStepItemId, string pName, string pName2, string pNote, string pNote2)
        {
            Result res = null;
            try
            {
                string sql = @"update stepitem set name=:2,name2=:3,note=:4,note2=:5 where stepitemid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB108102", pStepItemId, pName, pName2, pNote, pNote2);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108103 - Dynamic Step Item Insert ]
        public static Result DB108103(DbConnections pDB, int pStepItemId, string pName, string pName2, string pNote, string pNote2)
        {
            Result res = null;
            try
            {
                string sql = @"insert into stepitem (stepitemid,name,name2,note,note2) values(:1,:2,:3,:4,:5)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB108103", pStepItemId, pName, pName2, pNote, pNote2);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108104 - Dynamic Step Item Delete ]
        public static Result DB108104(DbConnections pDB, int pStepItemId)
        {
            Result res = null;
            try
            {
                string sql = @"delete from stepitem where stepitemid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB108104", pStepItemId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion

        #region [ DB108201 - Dynamic Step Link List ]
        public static Result DB108201(DbConnections pDB, int pStepId)
        {
            Result res = null;
            try
            {
                string sql = @"select stepid,stepitemid,orderno from steplink where stepid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB108201", pStepId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108202 - Dynamic Step Link Update ]
        public static Result DB108202(DbConnections pDB, int pStepId, int pStepItemId, int pOrderNo)
        {
            Result res = null;
            try
            {
                string sql = @"update steplink set orderno=:3 where stepid=:1 and stepitemid=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB108202", pStepId, pStepItemId, pOrderNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108203 - Dynamic Step Link Insert ]
        public static Result DB108203(DbConnections pDB, int pStepId, int pStepItemId, int pOrderNo)
        {
            Result res = null;
            try
            {
                string sql = @"insert into steplink (stepid,stepitemid,orderno) values(:1,:2,:3)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB108203", pStepId, pStepItemId, pOrderNo);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108204 - Dynamic Step Link Delete ]
        public static Result DB108204(DbConnections pDB, int pStepId, int pStepItemId)
        {
            Result res = null;
            try
            {
                string sql = @"delete from steplink where stepid=:1 and stepitemid=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB108204", pStepId, pStepItemId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        //#region [ DB108205 - Dynamic Step Link Next Item ]
        //public static Result DB108205(DbConnections pDB, int pStepId, int pStepItemId)
        //{
        //    Result res = null;
        //    try
        //    {
        //        string sql = @"select * from (select stepitemid from steplink where stepid=:1 order by orderno) ";
        //        res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB108205", pStepId);
        //    }
        //    catch (Exception ex)
        //    {
        //        res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
        //    }
        //    return res;
        //}
        //#endregion

        #region [ DB108301 - Dynamic Step Records List ]
        public static Result DB108301(DbConnections pDB, int pTypeCode, ulong pTypeId, int pStepId)
        {
            Result res = null;
            try
            {
                string sql = @"select sl.stepitemid,si.name,si.note,sr.performance,sr.started,sr.finished,sr.status
,sr.owner,u.userfname,u.userlname,u.position
from steplink sl
left join stepitem si on si.stepitemid=sl.stepitemid
left join steprecord sr on sr.typecode=:1 and sr.typeid=:2 and sr.stepid=:3 and sr.stepitemid=sl.stepitemid
left join hpuser u on u.userno=sr.owner
where sl.stepid=:3
order by sl.orderno";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB108301",pTypeCode, pTypeId, pStepId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108302 - Dynamic Step Records Select ]
        public static Result DB108302(DbConnections pDB, int pTypeCode, ulong pTypeId, int pStepId, int pStepItemId)
        {
            Result res = null;
            try
            {
                string sql = @"select sr.owner,sr.performance,sr.started,sr.finished,sr.status
,sr.owner,u.userfname,u.userlname,u.position
from steprecord sr
left join hpuser u on u.userno=sr.owner
where sr.typecode=:1 and sr.typeid=:2 and sr.stepid=:3 sr.stepid=:4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB108302", pTypeCode, pTypeId, pStepId, pStepItemId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108303 - Dynamic Step Records Merge ]
        public static Result DB108303(DbConnections pDB, int pTypeCode, ulong pTypeId, int pStepId, int pStepItemId, int pOwner, decimal pPerformance, DateTime pStarted, DateTime pFinished, int pStatus)
        {
            Result res = null;
            try
            {
                string sql = @"merge into steprecord a
using (select :1 typecode,:2 typeid,:3 stepid,:4 stepitemid,:5 owner,:6 performance,:7 started,:8 finished,:9 status from dual) b
on (a.typecode=b.typecode and a.typeid=b.typeid and a.stepid=b.stepid and a.stepitemid=b.stepitemid)
when matched then
  update set a.owner=b.owner,a.performance=b.performance,a.started=b.started,a.finished=b.finished,a.status=b.status
when not matched then
  insert (typecode,typeid,stepid,stepitemid,owner,performance,started,finished,status)
  values (b.typecode,b.typeid,b.stepid,b.stepitemid,b.owner,b.performance,b.started,b.finished,b.status)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB108303", pTypeCode, pTypeId, pStepId, pStepItemId, pOwner, pPerformance, pStarted, pFinished, pStatus);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion

        #region [ DB108401 -     ]
        public static Result DB108401(DbConnections pDB, int pTypeCode, ulong pTypeId, int pStepItemId)
        {
            Result res = null;
            try
            {
                string sql = @"select id,owner,started,postuser,postdate,progress,performance,note
from steptxn where typecode=:1 and typeid=:2 and stepitemid=:3
order by postdate desc";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB108401", pTypeCode, pTypeId, pStepItemId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108402 - Dynamic Step Txn Update ]
        public static Result DB108402(DbConnections pDB, ulong pTxnId, int pTypeCode, ulong pTypeId, int pStepItemId, int pOwner, DateTime pStarted, int pPostUser, DateTime pPostDate, decimal pProgress, decimal pPerformance, string pNote)
        {
            Result res = null;
            try
            {
                string sql = @"update steptxn set typecode=:2,typeid=:3,stepitemid=:4
,owner=:5,started=:6,postuser=:7,postdate=:8,progress=:9,performance=:10,note=:11
from steptxn where id=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB108402"
                    , pTxnId, pTypeCode, pTypeId, pStepItemId
                    , pOwner, pStarted, pPostUser, pPostDate, pProgress, pPerformance, pNote);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108403 - Dynamic Step Txn Insert ]
        public static Result DB108403(DbConnections pDB, ulong pTxnId, int pTypeCode, ulong pTypeId, int pStepItemId, int pOwner, DateTime pStarted, int pPostUser, DateTime pPostDate, decimal pProgress, decimal pPerformance, string pNote)
        {
            Result res = null;
            try
            {
                string sql = @"insert into steptxn 
(id,typecode,typeid,stepitemid,owner,started,postuser,postdate,progress,performance,note)
values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB108403"
                    , pTxnId, pTypeCode, pTypeId, pStepItemId
                    , pOwner, pStarted, pPostUser, pPostDate, pProgress, pPerformance, pNote);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #region [ DB108404 - Dynamic Step Txn Delete ]
        public static Result DB108404(DbConnections pDB, ulong pTxnId)
        {
            Result res = null;
            try
            {
                string sql = @"delete from steptxn where id=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB108404", pTxnId);
            }
            catch (Exception ex)
            {
                res = new Result(9110001, "Датабааз руу хандахад алдаа гарлаа" + ex.Message);
            }
            return res;
        }
        #endregion
        #endregion
    }
}
