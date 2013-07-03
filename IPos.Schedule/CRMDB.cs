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
    public class CRMDB
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
        #region[DB240-ProjectTypes (Төслийн төрөл)]
        #region [ DB240000 - Төслийн төрлийн жагсаалт авах ]
        public static Result DB240000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select projecttypeid,name,name2,orderno from projecttypes
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB240000", null);

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
        #region [ DB240001 - Төслийн төрлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB240001(DbConnections pDB, int pProjectTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select projecttypeid,name,name2,orderno from projecttypes
where projecttypeid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB240001", pProjectTypeID);

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
        #region [ DB240002 - Төслийн төрөл шинээр нэмэх ]
        public static Result DB240002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into projecttypes(projecttypeid,name,name2,orderno) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB240002", pParam);
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
        #region [ DB240003 - Төслийн төрөл засварлах ]
        public static Result DB240003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update projecttypes set name=:2,name2=:3,orderno=:4 
where projecttypeid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB240003", pParam);
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
        #region [ DB240004 - Төслийн төрөл  устгах ]
        public static Result DB240004(DbConnections pDB, int pProjectTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM projecttypes WHERE projecttypeid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB240004", pProjectTypeID);

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
#endregion
        #region[DB241-IssueTypes (Асуудлын төрөл)]
        #region [ DB241000 - Асуудлын төрлийн жагсаалт авах ]
        public static Result DB241000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select IssueTypeID,name,name2,orderno,vote from IssueTypes
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB241000", null);

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
        #region [ DB241001 - Асуудлын төрлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB241001(DbConnections pDB, int pIssueTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select IssueTypeID,name,name2,orderno,vote from IssueTypes 
where IssueTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB241001", pIssueTypeID);

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
        #region [ DB241002 - Асуудлын төрөл шинээр нэмэх ]
        public static Result DB241002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueTypes(IssueTypeID,name,name2,orderno,vote) values(:1,:2,:3,:4,:5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB241002", pParam);
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
        #region [ DB241003 - Асуудлын төрөл засварлах ]
        public static Result DB241003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssueTypes set name=:2,name2=:3,orderno=:4 ,vote=:5
where IssueTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB241003", pParam);
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
        #region [ DB241004 - Асуудлын төрөл  устгах ]
        public static Result DB241004(DbConnections pDB, int pIssueTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueTypes WHERE IssueTypeID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB241004", pIssueTypeID);

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
        #endregion
        #region[DB242-IssueTracks (Асуудлын алхамууд)]
        #region [ DB242000 - Асуудлын алхамууд жагсаалт авах ]
        public static Result DB242000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select IssueTrackID,name,name2,orderno from IssueTracks  
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB242000", null);

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
        #region [ DB242001 - Асуудлын алхамууд дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB242001(DbConnections pDB, int pIssueTrackID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select IssueTrackID,name,name2,orderno from IssueTracks  
where IssueTrackID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB242001", pIssueTrackID);

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
        #region [ DB242002 -  Асуудлын алхамууд шинээр нэмэх ]
        public static Result DB242002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueTracks(IssueTrackID,name,name2,orderno) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB242002", pParam);
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
        #region [ DB242003 -  Асуудлын алхамууд засварлах ]
        public static Result DB242003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssueTracks set name=:2,name2=:3,orderno=:4 
where IssueTrackID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB242003", pParam);
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
        #region [ DB242004 - Асуудлын алхамууд  устгах ]
        public static Result DB242004(DbConnections pDB, int pIssueTrackID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueTracks WHERE IssueTrackID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB242004", pIssueTrackID);

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
        #endregion
        #region[DB243-IssueTypeTracks (Асуудлын төрлийн шатлалууд)]
        #region [ DB243000 - Асуудлын төрлийн шатлалууд жагсаалт авах ]
        public static Result DB243000(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT IssueTypeID, IssueTrackID, ORDERNO
FROM IssueTypeTracks 
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB243000", null);

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
        #region [ DB243001 - Асуудлын төрлийн шатлалууд дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB243001(DbConnections pDB, int pIssueTypeID, int pIssueTrackID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT IssueTypeID, IssueTrackID, ORDERNO
FROM IssueTypeTracks 
WHERE IssueTypeID = :1 and IssueTrackID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB243001", pIssueTypeID, pIssueTrackID);

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
        #region [ DB243002 - Асуудлын төрлийн шатлалууд  шинээр нэмэх ]
        public static Result DB243002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO IssueTypeTracks(IssueTypeID, IssueTrackID, ORDERNO)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB243002", pParam);
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
        #region [ DB243003 - Асуудлын төрлийн шатлалууд засварлах ]
        public static Result DB243003(DbConnections pDB, int pOldIssueTypeID, int pOldIssueTrackID, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                obj[0] = pOldIssueTypeID;
                obj[1] = pOldIssueTrackID;
                obj[2] = pNewParam[0];
                obj[3] = pNewParam[1];
                obj[4] = pNewParam[2];

                string sql =
@"UPDATE IssueTypeTracks SET
IssueTypeID = :3, IssueTrackID=:4, ORDERNO=:5
WHERE IssueTypeID = :1 and IssueTrackID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB243003", obj);
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
        #region [ DB243004 - Асуудлын төрлийн шатлалууд устгах ]
        public static Result DB243004(DbConnections pDB, int pIssueTypeID, int pIssueTrackID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueTypeTracks WHERE IssueTypeID = :1 and IssueTrackID=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB243004", pIssueTypeID, pIssueTrackID);

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

        #region [ DB243005 - Асуудлын төрлийн шатлал сонгосон сонгоогүй хадгалах ]
        public static Result DB243005(DbConnections pDB, int pIssueTypeID, DataTable pDT)
        {
            Result res = new Result();
            try
            {
                string sql = "";

                //Delete IssueTypeTracks
                sql =
@"delete from IssueTypeTracks where IssueTypeID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB243005", pIssueTypeID);
                if (res.ResultNo != 0)
                    return res;
                //Insert IssueTypeTracks
                foreach (DataRow dr in pDT.Rows)
                {
                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
@"insert into IssueTypeTracks(IssueTypeID, IssueTrackID, OrderNo)
values(:1, :2, :3)";

                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB243005", new object[] { pIssueTypeID, dr["IssueTrackID"], dr["OrderNo"] });
                        if (res.ResultNo != 0)
                            return res;
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
        #region [ DB243006 - Асуудлын төрлийн шатлалуудын холбоотой жагсаалт авах ]
        public static Result DB243006(DbConnections pDB, int pIssueTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select  decode(b.issuetypeid,null, 0, 1) status,i.IssueTrackID,i.name,i.name2, i.orderno  from issuetracks i
left join  (select *  from IssueTypeTracks where issuetypeid=:1) b  on b.ISSUETRACKID=i.IssueTrackID
order by b.orderno,i.IssueTrackID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB243006", pIssueTypeID);

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


        #endregion
        #region[DB244-IssueActionType (Асуудлын үйлдлийн төрөл]

        
        #region [ DB244000 - IssueActionType (Асуудлын үйлдлийн төрөл) жагсаалт мэдээлэл авах ]
        public static Result DB244000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ActionTypeID,name,name2,orderno from IssueActionType 
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB244000", null);

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
        #region [ DB244001 - Асуудлын үйлдлийн төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB244001(DbConnections pDB, int pActionTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ActionTypeID,name,name2,orderno from IssueActionType 
WHERE ActionTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB244001", pActionTypeID);

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
        #region [ DB244002 -Асуудлын үйлдлийн төрөл шинээр нэмэх ]
        public static Result DB244002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueActionType(ActionTypeID,name,name2,orderno) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB244002", pParam);
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
        #region [ DB244003 - Асуудлын үйлдлийн төрөл засварлах ]
        public static Result DB244003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssueActionType set name=:2,name2=:3,orderno=:4 
WHERE ActionTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB244003", pParam);
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
        #region [ DB244004 -Асуудлын үйлдлийн төрөл  устгах ]
        public static Result DB244004(DbConnections pDB, int pActionTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueActionType WHERE ActionTypeID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB244004", pActionTypeID);

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
        #endregion
        #region[DB245-IssueResolutionType (Асуудлын хаагдсан төрөл)]


        #region [ DB245000 - Асуудлын хаагдсан төрөл жагсаалт мэдээлэл авах ]
        public static Result DB245000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ResolutionTypeID,name,name2,orderno from IssueResolutionType  
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB245000", null);

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
        #region [ DB245001 - Асуудлын хаагдсан төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB245001(DbConnections pDB, int pResolutionTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ResolutionTypeID,name,name2,orderno from IssueResolutionType  
WHERE ResolutionTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB245001", pResolutionTypeID);

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
        #region [ DB245002 -Асуудлын хаагдсан төрөл шинээр нэмэх ]
        public static Result DB245002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueResolutionType(ResolutionTypeID,name,name2,orderno) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB245002", pParam);
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
        #region [ DB245003 - Асуудлын хаагдсан төрөл засварлах ]
        public static Result DB245003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssueResolutionType set name=:2,name2=:3,orderno=:4 
WHERE ResolutionTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB245003", pParam);
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
        #region [ DB245004 -Асуудлын хаагдсан төрөл  устгах ]
        public static Result DB245004(DbConnections pDB, int pResolutionTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueResolutionType WHERE ResolutionTypeID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB245004", pResolutionTypeID);

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
        #endregion
        #region[DB246-IssueMemberPurp (Асуудлын холбоотой хүний үүрэгийн төрөл)]
        #region [ DB246000 - Асуудлын холбоотой хүний үүрэгийн төрөл жагсаалт авах ]
        public static Result DB246000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select IssuePurpID,name,name2,orderno,GroupID from IssueMemberPurp 
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB246000", null);

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
        #region [ DB246001 - Асуудлын холбоотой хүний үүрэгийн төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB246001(DbConnections pDB, int pIssuePurpID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select IssuePurpID,name,name2,orderno,GroupID from IssueMemberPurp 
where IssuePurpID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB246001", pIssuePurpID);

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
        #region [ DB246002 - Асуудлын холбоотой хүний үүрэгийн төрөл шинээр нэмэх ]
        public static Result DB246002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueMemberPurp(IssuePurpID,name,name2,orderno,GroupID) values(:1,:2,:3,:4,:5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB246002", pParam);
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
        #region [ DB246003 - Асуудлын холбоотой хүний үүрэгийн төрөл засварлах ]
        public static Result DB246003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssueMemberPurp set name=:2,name2=:3,orderno=:4 ,GroupID=:5
where IssuePurpID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB246003", pParam);
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
        #region [ DB246004 - Асуудлын холбоотой хүний үүрэгийн төрөл  устгах ]
        public static Result DB246004(DbConnections pDB, int pIssuePurpID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueMemberPurp WHERE IssuePurpID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB240004", pIssuePurpID);

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
        #endregion
        #region[DB247-IssuePriority (Асуудлын эрэмбэ)]
        #region [ DB247000 - Асуудлын эрэмбэ жагсаалт авах ]
        public static Result DB247000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select IssuePriorID,name,name2,orderno  from IssuePriority  
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB247000", null);

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
        #region [ DB247001 - Асуудлын эрэмбэ дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB247001(DbConnections pDB, int pIssuePriorID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select IssuePriorID,name,name2,orderno  from IssuePriority  
where IssuePriorID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB247001", pIssuePriorID);

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
        #region [ DB247002 - Асуудлын эрэмбэ шинээр нэмэх ]
        public static Result DB247002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssuePriority(IssuePriorID,name,name2,orderno) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB247002", pParam);
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
        #region [ DB247003 - Асуудлын эрэмбэ засварлах ]
        public static Result DB247003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssuePriority set name=:2,name2=:3,orderno=:4 
where IssuePriorID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB247003", pParam);
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
        #region [ DB247004 - Асуудлын эрэмбэ  устгах ]
        public static Result DB247004(DbConnections pDB, int pIssuePriorID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssuePriority WHERE IssuePriorID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB247004", pIssuePriorID);

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
        #endregion
        #region[DB248-IssueLinkType (Асуудлын холбоосын төрөл)]
        #region [ DB248000 -Асуудлын холбоосын төрөл жагсаалт авах ]
        public static Result DB248000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select LinkTypeID,name,name2,orderno  from IssueLinkType 
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB248000", null);

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
        #region [ DB248001 - Асуудлын холбоосын төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB248001(DbConnections pDB, int pLinkTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select LinkTypeID,name,name2,orderno  from IssueLinkType 
where LinkTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB248001", pLinkTypeID);

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
        #region [ DB248002 -Асуудлын холбоосын төрөл шинээр нэмэх ]
        public static Result DB248002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueLinkType(LinkTypeID,name,name2,orderno) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB248002", pParam);
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
        #region [ DB248003 - Асуудлын холбоосын төрөл засварлах ]
        public static Result DB248003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssueLinkType set name=:2,name2=:3,orderno=:4 
where LinkTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB248003", pParam);
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
        #region [ DB248004 - Асуудлын холбоосын төрөл  устгах ]
        public static Result DB248004(DbConnections pDB, int pLinkTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueLinkType WHERE LinkTypeID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB248004", pLinkTypeID);

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
        #endregion
        #region[DB249-NotifySchema (Мэдэгдэлийн схем)]
        #region [ DB249000 - Мэдэгдэлийн схем жагсаалт авах ]
        public static Result DB249000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select SchemaID,name,name2,orderno  from NotifySchema  
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB249000", null);

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
        #region [ DB249001 - Мэдэгдэлийн схем дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB249001(DbConnections pDB, int pSchemaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select SchemaID,name,name2,orderno  from NotifySchema  
where SchemaID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB249001", pSchemaID);

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
        #region [ DB249002 - Мэдэгдэлийн схем шинээр нэмэх ]
        public static Result DB249002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into NotifySchema(SchemaID,name,name2,orderno) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB249002", pParam);
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
        #region [ DB249003 - Мэдэгдэлийн схем засварлах ]
        public static Result DB249003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update NotifySchema set name=:2,name2=:3,orderno=:4 
where SchemaID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB249003", pParam);
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
//        #region [ DB249004 - Мэдэгдэлийн схем  устгах ]
//        public static Result DB249004(DbConnections pDB, int pSchemaID)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql =
//@"DELETE FROM NotifySchema WHERE SchemaID=:1";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB249004", pSchemaID);

//                return res;
//            }
//            catch (Exception ex)
//            {
//                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
        //        #endregion
        #region [ DB249004 -  Мэдэгдэлийн схем  устгах ]
        public static Result DB249004(DbConnections pDB, int pSchemaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select * from NotifySchemaTxn  
where SchemaID=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB249004", pSchemaID);

                if (res.Data.Tables[0].Rows.Count != 0)
                {
                    res.ResultNo = 9110086;
                    res.ResultDesc = "Холбоотой бичлэг мэдэгдэлийн схемийн гүйлгээн дээр байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM NotifySchema WHERE SchemaID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB249004", pSchemaID);

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
        #endregion
        #region[DB250-NotifySchemaTxn (Мэдэгдэлийн схем гүйлгээ)]
        #region [ DB250000 - Мэдэгдэлийн схем гүйлгээ жагсаалт мэдээлэл авах ]
        public static Result DB250000(DbConnections pDB,int pSchemaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select n.SchemaID,n.TranCode,n.TypeID,decode(n.typeid,0,'Reporter',1,'CurrentAssignee',2,'CurrentUser',3,'Project Owner',4,'Component Owner',5,'User',6,'Group') typeidname,n.ID 
from NotifySchemaTxn n
left join txn t on n.trancode=t.trancode
where SchemaID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB250000", pSchemaID);

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
        #region [ DB250001 - Мэдэгдэлийн схем гүйлгээ дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB250001(DbConnections pDB,int pSchemaID,long pTrancode, int pTypeID, long pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select SchemaID,TranCode,TypeID,decode(typeid,0,'Reporter',1,'CurrentAssignee',2,'CurrentUser',3,'Project Owner',4,'Component Owner',5,'User',6,'Group') typeidname,ID 
from NotifySchemaTxn 
where SchemaID=:1 and TranCode=:2 and TypeID=:3 and ID=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB250001", pSchemaID,pTrancode,pTypeID,pID);

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
        #region [ DB250002 - Мэдэгдэлийн схем гүйлгээ шинээр нэмэх ]
        public static Result DB250002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into NotifySchemaTxn(SchemaID,TranCode,TypeID,ID) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB250002", pParam);
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
        #region [ DB250003 - Мэдэгдэлийн схем гүйлгээ засварлах ]
        public static Result DB250003(DbConnections pDB, object[] pOldParam,  object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[8];
                obj[0] = Static.ToInt(pOldParam[0]);
                obj[1] = Static.ToInt(pOldParam[1]);
                obj[2] = Static.ToInt(pOldParam[2]);
                obj[3] = Static.ToLong(pOldParam[3]);
                obj[4] = pNewParam[0];
                obj[5] = pNewParam[1];
                obj[6] = pNewParam[2];
                obj[7] = pNewParam[3];
                
                string sql =
@"update NotifySchemaTxn set SchemaID=:5,TranCode=:6,TypeID=:7,ID=:8 
where SchemaID=:1 and TranCode=:2 and TypeID=:3 and ID=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB250003", obj);
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
        #region [ DB250004 -Мэдэгдэлийн схем гүйлгээ  устгах ]
        public static Result DB250004(DbConnections pDB, int pSchemaID, long pTrancode, int pTypeID, long pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM NotifySchemaTxn WHERE SchemaID=:1 and trancode=:2 and typeid=:3 and id=:4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB250004", pSchemaID, pTrancode, pTypeID, pID);

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
        #region [ DB250005 - Мэдэгдэлийн схем гүйлгээний дэлгэрэнгүй[typeid,id] ]
        public static Result DB250005(DbConnections pDB, int pSchemaID, long pTrancode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select TypeID,
decode(typeid,0,'Reporter',1,'CurrentAssignee',2,'CurrentUser',3,'Project Owner',4,'Component Owner',5,'User',6,'Group') typeidname,ID 
from NotifySchemaTxn 
where SchemaID=:1 and TranCode=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB250005", pSchemaID, pTrancode);

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
        #region [ DB250006 - iSSUE ДЭЭР MAIL ЯВУУЛАХ]
        public static Result DB250006(DbConnections pDB, long pIssueID,int pTranCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select h.userno, h.userfname, h.userlname, h.email, h.mobile, h.position, a.subject, a.description, a.issueid, b.name as projectname,
it.name as issuetypename, h1.userfname ||'-'|| h1.userlname as reporteruser, h2.userfname ||'-'|| h2.userlname as assignuser
from issue a
left join issueproject b on A.PROJECTID=b.PROJECTID
left join issuetypes it on A.ISSUETYPEID=it.ISSUETYPEID
left join PROJECTCOMP p on A.PROJECTID=p.PROJECTID and A.PROJECTCOMPID=p.PROJECTCOMPID
left join NOTIFYSCHEMA n on n.SCHEMAID=b.NOTIFYSCHEMAID
left join (select * from NOTIFYSCHEMATXN where typeid<>6 and TRANCODE=:2
            union
            select A.SCHEMAID, a.trancode, 5, b.userno from NOTIFYSCHEMATXN a
            left join usergroup b on a.id=B.groupid
            where a.typeid=6 and a.TRANCODE=:2) nt on nt.SCHEMAID=b.NOTIFYSCHEMAID 
join hpuser h on h.userno=CASE nt.typeid
                                                      WHEN 0 THEN a.CREATEUSER
                                                      WHEN 1 THEN a.ASSIGNEEUSER
                                                      WHEN 2 THEN a.CREATEUSER
                                                      WHEN 3 THEN b.OWNERUSER
                                                      WHEN 4 THEN p.OWNERUSER
                                                      WHEN 5 THEN nt.id
                                    END
left join hpuser h1 on A.createuser=h1.userno
left join hpuser h2 on A.ASSIGNEEUSER=h2.userno
where a.issueid=:1
group by h.userno, h.userfname, h.userlname, h.email, h.mobile, h.position, a.subject, a.description, a.issueid, b.name,
it.name, h1.userfname ||'-'|| h1.userlname, h2.userfname ||'-'|| h2.userlname
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB250006", pIssueID,pTranCode);

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
        #region [ DB250007 - iSSUETXN ДЭЭР MAIL ЯВУУЛАХ]
        public static Result DB250007(DbConnections pDB, long pIssueID, int pTranCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select z.jrnoo,tx.subject, tx.description,tx.nextpurpose,TX.NEXTDATE,ctt.name as contacttypename, h.userno, h.userfname, h.userlname, h.email, h.mobile, h.position,a.issueid, b.name as projectname,
it.name as issuetypename, h1.userfname ||'-'|| h1.userlname as reporteruser, h2.userfname ||'-'|| h2.userlname as assignuser from (select max(i.jrno) as jrnoo from issuetxn i where i.issueid=:1   ) z
 join  issuetxn tx on tx.jrno=z.jrnoo
left join issue a on tx.issueid=a.issueid
left join issueproject b on A.PROJECTID=b.PROJECTID
left join issuetypes it on A.ISSUETYPEID=it.ISSUETYPEID
left join PROJECTCOMP p on A.PROJECTID=p.PROJECTID and A.PROJECTCOMPID=p.PROJECTCOMPID
left join custcontacttype ctt on CTT.TYPECODE=TX.CONTRACTTYPE
left join NOTIFYSCHEMA n on n.SCHEMAID=b.NOTIFYSCHEMAID
left join (select * from NOTIFYSCHEMATXN where typeid<>6 and TRANCODE=:2
            union
            select A.SCHEMAID, a.trancode, 5, b.userno from NOTIFYSCHEMATXN a
            left join usergroup b on a.id=B.groupid
            where a.typeid=6 and a.TRANCODE=:2) nt on nt.SCHEMAID=b.NOTIFYSCHEMAID 
join hpuser h on h.userno=CASE nt.typeid
                                                      WHEN 0 THEN a.CREATEUSER
                                                      WHEN 1 THEN tx.ASSIGNEEUSER
                                                      WHEN 2 THEN tx.userno
                                                      WHEN 3 THEN b.OWNERUSER
                                                      WHEN 4 THEN p.OWNERUSER
                                                      WHEN 5 THEN nt.id
                                    END
left join hpuser h1 on tx.userno=h1.userno
left join hpuser h2 on tx.ASSIGNEEUSER=h2.userno

group by  z.jrnoo,tx.subject, tx.description,tx.nextpurpose,TX.NEXTDATE,ctt.name,h.userno, h.userfname, h.userlname, h.email, h.mobile, h.position,
a.issueid, b.name,
it.name, h1.userfname ||'-'|| h1.userlname, h2.userfname ||'-'|| h2.userlname
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB250007", pIssueID, pTranCode);

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

        #endregion
        #region[DB251-PermSchema (Эрхийн схем)]
        #region [ DB251000 - Эрхийн схем жагсаалт авах ]
        public static Result DB251000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select PermSchemaID,name,name2,orderno from PermSchema 
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB251000", null);

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
        #region [ DB251001 - Эрхийн схем дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB251001(DbConnections pDB, int pPermSchemaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select PermSchemaID,name,name2,orderno from PermSchema 
where PermSchemaID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB251001", pPermSchemaID);

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
        #region [ DB251002 - Эрхийн схем шинээр нэмэх ]
        public static Result DB251002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into PermSchema(PermSchemaID,name,name2,orderno) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB251002", pParam);
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
        #region [ DB251003 - Эрхийн схем засварлах ]
        public static Result DB251003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update PermSchema set name=:2,name2=:3,orderno=:4 
where PermSchemaID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB251003", pParam);
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
//        #region [ DB251004 - Эрхийн схем  устгах ]
//        public static Result DB251004(DbConnections pDB, int pPermSchemaID)
//        {
//            Result res = new Result();
//            try
//            {
//                string sql =
//@"DELETE FROM PermSchema WHERE PermSchemaID=:1";
//                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB251004", pPermSchemaID);

//                return res;
//            }
//            catch (Exception ex)
//            {
//                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
//                res.ResultNo = 9110001;
//                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
//                return res;
//            }
//        }
        //        #endregion
        #region [ DB251004 -  Эрхийн схем  устгах ]
        public static Result DB251004(DbConnections pDB, int pPermSchemaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select * from PermSchemaTxn  
where PermSchemaID=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB251004", pPermSchemaID);

                if (res.Data.Tables[0].Rows.Count != 0)
                {
                    res.ResultNo = 9110087;
                    res.ResultDesc = "Холбоотой бичлэг эрхийн схемийн гүйлгээн дээр байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM PermSchema WHERE PermSchemaID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB251004", pPermSchemaID);

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
        #endregion
        #region[DB252-PermSchemaTxn (Эрхийн схем гүйлгээ)]
        #region [ DB252000 - Эрхийн схем гүйлгээ жагсаалт мэдээлэл авах ]
        public static Result DB252000(DbConnections pDB, int pPermSchemaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select n.PermSchemaID,n.TranCode,n.TypeID,decode(n.typeid,0,'Reporter',1,'CurrentAssignee',2,'CurrentUser',3,'Project Owner',4,'Component Owner',5,'User',6,'Group') typeidname,n.ID 
from PermSchemaTxn  n
left join txn t on n.trancode=t.trancode
where PermSchemaID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB252000", pPermSchemaID);

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
        #region [ DB252001 - Эрхийн схем гүйлгээ дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB252001(DbConnections pDB, int pPermSchemaID, long pTrancode, int pTypeID, long pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select PermSchemaID,TranCode,TypeID,decode(typeid,0,'Reporter',1,'CurrentAssignee',2,'CurrentUser',3,'Project Owner',4,'Component Owner',5,'User',6,'Group') typeidname,ID 
from PermSchemaTxn 
where PermSchemaID=:1 and TranCode=:2 and TypeID=:3 and ID=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB252001", pPermSchemaID, pTrancode, pTypeID, pID);

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
        #region [ DB252002 - Эрхийн схем гүйлгээ шинээр нэмэх ]
        public static Result DB252002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into PermSchemaTxn(PermSchemaID,TranCode,TypeID,ID) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB252002", pParam);
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
        #region [ DB252003 - Эрхийн схем гүйлгээ засварлах ]
        public static Result DB252003(DbConnections pDB,object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {

                object[] obj = new object[8];
                obj[0] = Static.ToInt(pOldParam[0]);
                obj[1] = Static.ToInt(pOldParam[1]);
                obj[2] = Static.ToInt(pOldParam[2]);
                obj[3] = Static.ToLong(pOldParam[3]);
                obj[4] = pNewParam[0];
                obj[5] = pNewParam[1];
                obj[6] = pNewParam[2];
                obj[7] = pNewParam[3];
                

                string sql =
@"update PermSchemaTxn set PermSchemaID=:5, TranCode=:6,TypeID=:7,ID=:8 
where PermSchemaID=:1 and TranCode=:2 and TypeID=:3 and ID=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB252003", obj);
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
        #region [ DB252004 - Эрхийн схем гүйлгээ  устгах ]
        public static Result DB252004(DbConnections pDB, int pPermSchemaID, long pTrancode, int pTypeID, long pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PermSchemaTxn WHERE PermSchemaID=:1 and trancode=:2 and typeid=:3 and id=:4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB252004", pPermSchemaID, pTrancode, pTypeID, pID);

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

        #endregion
        #region[DB253-IssueCustomer (Харилцагч дээрх асуудал)]
        #region [ DB253000 - Харилцагч дээрх асуудлын  жагсаалт авах ]
        public static Result DB253000(DbConnections pDB, int pagenumber, int pagecount,long pCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.issueid, i.CustNo  from IssueCustomer i
left join customer c on  i.custno=C.CUSTOMERNO
left join contact co on i.custno=co.customerno
left join issue ii on ii.issueid=i.issueid
where  ii.issueid=i.issueid
and i.custno=:1
order by ii.issueid";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB253000", pCustNo);

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
        #region [ DB253001 -Харилцагч дээрх асуудлын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB253001(DbConnections pDB, long pIssueid, long pCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.issueid,i.CustNo  from IssueCustomer i
left join issue ii on i.issueid=ii.issueid
left join customer c on i.custno=c.customerno
left join contact co on i.custno=co.customerno
where i.issueid=:1 and i.CustNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB253001",pIssueid,pCustNo);
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
        #region [ DB253002 - Харилцагч дээрх асуудал шинээр нэмэх ]
        public static Result DB253002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueCustomer(issueid,CustNo) values(:1,:2)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB253002", pParam);
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
        #region [ DB253003 - Харилцагч дээрх асуудал засварлах ]
        public static Result DB253003(DbConnections pDB, int pOldissueid, int pOldCustNo, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[4];
                obj[0] = pOldissueid;
                obj[1] = pOldCustNo;
                obj[2] = pNewParam[0];
                obj[3] = pNewParam[1];
                string sql =
@"update IssueCustomer set issueid=:3,CustNo=:4 
where issueid=:1 and CustNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB253003", obj);
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
        #region [ DB253004 - Харилцагч дээрх асуудал  устгах ]
        public static Result DB251004(DbConnections pDB, long pissueid, long pCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueCustomer WHERE where issueid=:1 and CustNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB251004", pissueid,pCustNo);

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
        #region [ DB253005 - Харилцагч дээрх асуудлын тухайн issue дээрх бүх талбарыг авах ]
        public static Result DB253005(DbConnections pDB, int pagenumber, int pagecount,long pCustNo )
        {
            Result res = new Result();
            try
            {
                string sql =
@"select  ii.issueid,ii.projectid,i1.name as prjectname, Ii.PROJECTCOMPID,p.name as projectcopname,  ii.IssueTypeID,i2.name as issuetypeidname, ii.IssuePriorID,i3.name as issuepriorname, ii.subject,ii.description,ii.status, decode(ii.status,0,'Open',1,'InProgress',2,'ReOpen',3,'Resolved',9,'Closed')   as statusname,
ii.ResolutionStatus,ii.TrackID,i4.name as trackname, ii.CreateUser, substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserName,  ii.Createdate,ii.UpdateDate,ii.duedate,ii.AssigneeUser, substr(h1.userfname, 0, 1)||'.'||h1.userlname as AssigneeUserName, ii.resolutiondate,ii.resolutionuser,
substr(h3.userfname, 0, 1)||'.'||h3.userlname as resolutionuserName,
ii.votes  from IssueCustomer i,issue ii

left join IssueProject  i1 on ii.projectid=i1.projectid
left join ProjectComp p on ii.PROJECTCOMPID=p.PROJECTCOMPID 
left join IssueTypes  i2 on ii.IssueTypeID=i2.IssueTypeID
left join IssuePriority i3 on ii.IssuePriorID=i3.IssuePriorID
left join hpuser h1 on ii.AssigneeUser=h1.userno
left join hpuser h2 on ii.CreateUser=h2.userno
left join hpuser h3 on ii.resolutionuser=h3.userno
left join IssueTracks i4 on ii.trackid=i4.IssueTrackID

where 
 ii.issueid=i.issueid
and i.custno=:1
order by ii.issueid";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB253005", pCustNo);

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

        #endregion
        #region[DB254-IssueProject (Төсөл)]
        #region [ DB254000 - Төслийн жагсаалт авах ]
        public static Result DB254000(DbConnections pDB, int pageindex, int pagerow, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "ProjectID like", "status", "name like","name2 like", "ShortName like","ShortName2 like",
                "owneruser", "startdate", "enddate", "projecttypeid","createuserno","createdate","NotifySchemaID","PermSchemaID"
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
                                sb.AppendFormat(" i.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" i.{0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select i.ProjectID,i.status,decode(i.status,0,'Нээлттэй',9,'Хаагдсан') statusname,i.name,i.name2,i.ShortName,i.ShortName2,i.description,i.owneruser,
 substr(h1.userfname, 0, 1)||'.'||h1.userlname as OwnerUserNoName,
i.startdate,i.enddate,i.projecttypeid ,pr.name as projecttypename,
i.createuserno,substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserNoName, i.createdate,i.NotifySchemaID,n.name as NotifySchemaName,  i.PermSchemaID,p.name as PermSchemaName,
i.orderno from IssueProject i
left join hpuser h1 on i.owneruser=h1.userno
left join hpuser h2 on i.CreateUserNo=h2.userno
left join NotifySchema n on i.NotifySchemaID=n.SchemaID
left join PermSchema p on i.PermSchemaID=p.PermSchemaID
left join ProjectTypes pr on i.projecttypeid=pr.ProjectTypeID

{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " order by orderno ");

                res = pDB.ExecuteQuery("core", sql, "DB254000", pageindex, pagerow, dbparam.ToArray());
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
        #region [ DB254001 - Төслийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB254001(DbConnections pDB, long pProjectID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.ProjectID,i.status,decode(i.status,0,'Нээлттэй',9,'Хаагдсан') statusname,i.name,i.name2,i.ShortName,i.ShortName2,i.description,i.owneruser,
 substr(h1.userfname, 0, 1)||'.'||h1.userlname as OwnerUserNoName,
i.startdate,i.enddate,i.projecttypeid ,pr.name as projecttypename,
i.createuserno,substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserNoName, i.createdate,i.NotifySchemaID,n.name as NotifySchemaName,  i.PermSchemaID,p.name as PermSchemaName,
 i.orderno from IssueProject i
left join hpuser h1 on i.owneruser=h1.userno
left join hpuser h2 on i.CreateUserNo=h2.userno
left join NotifySchema n on i.NotifySchemaID=n.SchemaID
left join PermSchema p on i.PermSchemaID=p.PermSchemaID
left join ProjectTypes pr on i.projecttypeid=pr.ProjectTypeID
where i.ProjectID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB254001", pProjectID);

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
        #region [ DB254002 - Төсөл шинээр нэмэх ]
        public static Result DB254002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                long seq = 0;
                #region [ ProjectID ]
                Core.AutoNumEnum enums = new AutoNumEnum();
       

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 18, enums);

                if (seqres.ResultNo == 0)
                {
                    seq = Static.ToLong(seqres.ResultDesc);
                    if (seq == 0)
                    {
                        seqres.ResultNo = 9110068;
                        seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:18][" + seqres.ResultDesc + "]";
                        return seqres;
                    }
                }
                else
                    return seqres;

                pParam[0] = seq;
                #endregion
                string sql =
@"insert into IssueProject(ProjectID,status,name,name2,ShortName,ShortName2,description,owneruser,startdate,enddate,projecttypeid,createuserno,createdate,orderno,NotifySchemaID,PermSchemaID) values(:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB254002", pParam);
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
        #region [ DB254003 - Төсөл засварлах ]
        public static Result DB254003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssueProject set status=:2,name=:3,name2=:4,ShortName=:5,ShortName2=:6,description=:7,owneruser=:8,startdate=:9,enddate=:10,
projecttypeid=:11,createuserno=:12,createdate=:13,orderno=:14, NotifySchemaID=:15,PermSchemaID=:16 
where ProjectID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB254003", pParam);
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
        #region [ DB254004 - Төсөл  устгах ]
        public static Result DB254004(DbConnections pDB, long pProjectID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueProject WHERE ProjectID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB254004", pProjectID);

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
        #region [ DB254005 - Асуудал дээрх төслийн жагсаалт авах ]
        public static Result DB254005(DbConnections pDB, int pageindex, int pagerow, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "i.issueid like","i.ProjectID like", "i.status", "i.name like","i.name2 like", "i.ShortName like","i.ShortName2 like",
                "i.owneruser", "i.startdate", "i.enddate", "i.projecttypeid","i.createuserno","i.createdate","i.NotifySchemaID","i.PermSchemaID"
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
                                sb.AppendFormat(" {0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" {0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select  ss.issueid, i.ProjectID,i.status,decode(i.status,0,'Нээлттэй',9,'Хаагдсан') statusname,i.name,i.name2,i.ShortName,i.ShortName2,i.description,i.owneruser,
 substr(h1.userfname, 0, 1)||'.'||h1.userlname as OwnerUserNoName,
i.startdate,i.enddate,i.projecttypeid ,pr.name as projecttypename,
i.createuserno,substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserNoName, i.createdate,i.NotifySchemaID,n.name as NotifySchemaName,  i.PermSchemaID,p.name as PermSchemaName,
i.orderno from IssueProject i
left join hpuser h1 on i.owneruser=h1.userno
left join hpuser h2 on i.CreateUserNo=h2.userno
left join NotifySchema n on i.NotifySchemaID=n.SchemaID
left join PermSchema p on i.PermSchemaID=p.PermSchemaID
left join ProjectTypes pr on i.projecttypeid=pr.ProjectTypeID
 join issue ss on I.PROJECTID=SS.PROJECTID

{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " order by orderno ");

                res = pDB.ExecuteQuery("core", sql, "DB254005", pageindex, pagerow, dbparam.ToArray());
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
        #endregion
        #region[DB255- ProjectComp (Төслийн дэд төрөл)]
        #region [ DB255000 - Төслийн дэд төрөл жагсаалт авах ]
        public static Result DB255000(DbConnections pDB, int pageindex, int pagerow, object[] pParam)
        {
            Result res = new Result();
            try
            {


                string sql =
@"select p.ProjectCompID,p.ProjectID,i.name as ProjectName, p.name,p.name2,p.shortname,p.shortname2,
p.description,p.owneruser, substr(h1.userfname, 0, 1)||'.'||h1.userlname as OwnerUserNoName, p.orderno from ProjectComp p
left join IssueProject i on p.ProjectID=i.ProjectID 
left join hpuser h1 on p.owneruser=h1.userno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB255000", null);
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
        #region [ DB255001 - Төслийн дэд төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB255001(DbConnections pDB, int pProjectCompID, long pProjectID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select p.ProjectCompID,p.ProjectID,i.name as ProjectName, p.name,p.name2,p.shortname,p.shortname2,
p.description,p.owneruser, substr(h1.userfname, 0, 1)||'.'||h1.userlname as OwnerUserNoName, p.orderno from ProjectComp p
left join IssueProject i on p.ProjectID=i.ProjectID 
left join hpuser h1 on p.owneruser=h1.userno
where p.ProjectCompID=:1 and p.ProjectID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB255001", pProjectCompID, pProjectID);

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
        #region [ DB255002 - Төслийн дэд төрөл шинээр нэмэх ]
        public static Result DB255002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                
                
                string sql =
@"insert into ProjectComp(ProjectCompID,ProjectID,name,name2,ShortName,ShortName2,description,owneruser,orderno) 
values(:1,:2,:3,:4,:5,:6,:7,:8,:9)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB255002", pParam);
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
        #region [ DB255003 - Төслийн дэд төрөл засварлах ]
        public static Result DB255003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update ProjectComp set name=:3,name2=:4,ShortName=:5,ShortName2=:6,description=:7,owneruser=:8 ,orderno=:9 
where  ProjectCompID=:1 and ProjectID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB255003", pParam);
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
        #region [ DB255004 - Төслийн дэд төрөл  устгах ]
        public static Result DB255004(DbConnections pDB, int pProjectCompID, long pProjectID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ProjectComp WHERE ProjectCompID=:1 and ProjectID=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB255004", pProjectCompID, pProjectID);

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
        #endregion
        #region[DB256-Issue (Асуудал)]
        #region [ DB256000 - Асуудлын жагсаалт авах ]
        public static Result DB256000(DbConnections pDB, int pageindex, int pagerow, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "c.custno like","ii.issueid like", "ii.projectid", "ii.PROJECTCOMPID like", "ii.IssueTypeID like",
                "ii.IssuePriorID","ii.Status", "ii.ResolutionStatus", "ii.TrackID", "ii.CreateUser","ii.Createdate","ii.UpdateDate","ii.duedate","ii.AssigneeUser","ii.resolutiondate","ii.resolutionuser"
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
                                sb.AppendFormat(" {0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" {0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT c.custno,ii.issueid,ii.projectid,i1.name as prjectname, Ii.PROJECTCOMPID,p.name as projectcopname,  ii.IssueTypeID,i2.name as issuetypeidname, ii.IssuePriorID,i3.name as issuepriorname, ii.subject,ii.description,ii.status ,decode(ii.status,0,'Open',1,'InProgress',2,'ReOpen',3,'Resolved',9,'Closed')   as statusname,
ii.ResolutionStatus,ii.TrackID,i4.name as trackname, ii.CreateUser, substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserName,  ii.Createdate,ii.UpdateDate,ii.duedate,ii.AssigneeUser, substr(h1.userfname, 0, 1)||'.'||h1.userlname as AssigneeUserName, ii.resolutiondate,ii.resolutionuser,
substr(h3.userfname, 0, 1)||'.'||h3.userlname as resolutionuserName,
ii.votes  from issue ii
left join IssueProject  i1 on ii.projectid=i1.projectid
left join ProjectComp p on ii.PROJECTCOMPID=p.PROJECTCOMPID 
left join IssueTypes  i2 on ii.IssueTypeID=i2.IssueTypeID
left join IssuePriority i3 on ii.IssuePriorID=i3.IssuePriorID
left join hpuser h1 on ii.AssigneeUser=h1.userno
left join hpuser h2 on ii.CreateUser=h2.userno
left join hpuser h3 on ii.resolutionuser=h3.userno
left join IssueTracks i4 on ii.trackid=i4.IssueTrackID
left join issuecustomer c on ii.issueid=c.issueid
left join customer cc on c.custno=cc.customerno
where ii.issueid=c.issueid and c.custno=cc.customerno
{0} {1} {2}", sb.Length > 0 ? "and" : "", sb.ToString(), " order by ii.issueid desc");

                res = pDB.ExecuteQuery("core", sql, "DB256000", pageindex, pagerow, dbparam.ToArray());
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
        #region [ DB256001 - Асуудлын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB256001(DbConnections pDB, long pissueid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT  ii.issueid,ii.projectid,i1.name as prjectname, Ii.PROJECTCOMPID,p.name as projectcopname,  ii.IssueTypeID,i2.name as issuetypeidname, ii.IssuePriorID,i3.name as issuepriorname, ii.subject,ii.description,ii.status ,decode(ii.status,0,'Open',1,'InProgress',2,'ReOpen',3,'Resolved',9,'Closed')   as statusname,
ii.ResolutionStatus,ii.TrackID,i4.name as trackname, ii.CreateUser, substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserName,  ii.Createdate,ii.UpdateDate,ii.duedate,ii.AssigneeUser, substr(h1.userfname, 0, 1)||'.'||h1.userlname as AssigneeUserName, ii.resolutiondate,ii.resolutionuser,
substr(h3.userfname, 0, 1)||'.'||h3.userlname as resolutionuserName,
ii.votes  from issue ii
left join IssueProject  i1 on ii.projectid=i1.projectid
left join ProjectComp p on ii.PROJECTCOMPID=p.PROJECTCOMPID 
left join IssueTypes  i2 on ii.IssueTypeID=i2.IssueTypeID
left join IssuePriority i3 on ii.IssuePriorID=i3.IssuePriorID
left join hpuser h1 on ii.AssigneeUser=h1.userno
left join hpuser h2 on ii.CreateUser=h2.userno
left join hpuser h3 on ii.resolutionuser=h3.userno
left join IssueTracks i4 on ii.trackid=i4.IssueTrackID
where ii.issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB256001", pissueid);

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
        #region [ DB256002 - Асуудал шинээр нэмэх ]
        public static Result DB256002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                long seq = 0;
                #region [ IssueID ]
                
                Core.AutoNumEnum enums = new AutoNumEnum();


                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 17, enums);

                if (seqres.ResultNo == 0)
                {
                    seq = Static.ToLong(seqres.ResultDesc);
                    if (seq == 0)
                    {
                        seqres.ResultNo = 9110068;
                        seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:17][" + seqres.ResultDesc + "]";
                        return seqres;
                    }
                }
                else
                    return seqres;

                pParam[0] = seq;
                #endregion
                string sql =
@"insert into issue(issueid,projectid,PROJECTCOMPID,IssueTypeID,IssuePriorID,subject,description,status,ResolutionStatus,TrackID,CreateUser,Createdate,duedate,AssigneeUser) 
values(:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB256002", pParam);
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
        #region [ DB256003 - Асуудал засварлах ]
        public static Result DB256003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update issue set projectid=:2,PROJECTCOMPID=:3,IssueTypeID=:4,IssuePriorID=:5,subject=:6,description=:7,status=:8,ResolutionStatus=:9,TrackID=:10,
CreateUser=:11,Createdate=:12,UpdateDate=:13,duedate=:14,AssigneeUser=:15,resolutiondate=:16,resolutionuser=:17
where issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB256003", pParam);
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
        #region [ DB256004 - Асуудал  устгах ]
        public static Result DB256004(DbConnections pDB, long pissueid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM issue WHERE issueid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB256004", pissueid);

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
        #region [ DB256005 - Асуудалын Votes засварлах ]
        public static Result DB256005(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update issue set votes=:2 where issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB256005", pParam);
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
        #region [ DB256006 - Асуудалын Votes тэмдэглэх ]
        public static Result DB256006(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ISSUEVOTES SET VOTE=1 WHERE ISSUEID=:1 AND USERNO=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB256006", pParam);
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
        #region [ DB256007 - Асуудлын Votes жагсаалт авах ]
        public static Result DB256007(DbConnections pDB,long pIssueID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select votes from issue
where issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB256007", pIssueID);

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
        #region [ DB256008 - Асуудалын status өөрчлөх ]
        public static Result DB256008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update issue set status=:2 where issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB256008", pParam);
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
        #region [ DB256009 - Асуудал өөрчлөх1 ]
        public static Result DB256009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update issue set  ResolutionUser=:2,
    ResolutionDate=:3,
    TrackID=:4,
    ResolutionStatus=:5,
    Status=:6
    where issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB256009", pParam);
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
        #region [ DB256010 - Асуудалын assigneeuser өөрчлөх ]
        public static Result DB256010(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update issue set assigneeuser=:2 where issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB256010", pParam);
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
        #region [ DB256011 - Асуудлын төслийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB256011(DbConnections pDB, long pissueid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT  ii.issueid,ii.projectid,i1.name as prjectname, Ii.PROJECTCOMPID,p.name as projectcopname,  ii.IssueTypeID,i2.name as issuetypeidname, ii.IssuePriorID,i3.name as issuepriorname, ii.subject,ii.description,ii.status ,decode(ii.status,0,'Open',1,'InProgress',2,'ReOpen',3,'Resolved',9,'Closed')   as statusname,
ii.ResolutionStatus,ii.TrackID,i4.name as trackname, ii.CreateUser, substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserName,  ii.Createdate,ii.UpdateDate,ii.duedate,ii.AssigneeUser, substr(h1.userfname, 0, 1)||'.'||h1.userlname as AssigneeUserName, ii.resolutiondate,ii.resolutionuser,
substr(h3.userfname, 0, 1)||'.'||h3.userlname as resolutionuserName,
ii.votes  from issue ii
left join IssueProject  i1 on ii.projectid=i1.projectid
left join ProjectComp p on ii.PROJECTCOMPID=p.PROJECTCOMPID 
left join IssueTypes  i2 on ii.IssueTypeID=i2.IssueTypeID
left join IssuePriority i3 on ii.IssuePriorID=i3.IssuePriorID
left join hpuser h1 on ii.AssigneeUser=h1.userno
left join hpuser h2 on ii.CreateUser=h2.userno
left join hpuser h3 on ii.resolutionuser=h3.userno
left join IssueTracks i4 on ii.trackid=i4.IssueTrackID
where II.PROJECTID=:1
order by  ii.issueid";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB256011", pissueid);

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
        #region [ DB256012 -DashBoard-ны aсуудлын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB256012(DbConnections pDB, long pissueid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT  ii.issueid,decode(c.classcode, 0, substr(c.FIRSTNAME,0,1)||'.'||c.lastname, 1, CORPORATENAME) customername,ii.projectid,i1.name as prjectname, Ii.PROJECTCOMPID,p.name as projectcopname,  ii.IssueTypeID,i2.name as issuetypeidname, ii.IssuePriorID,i3.name as issuepriorname, ii.subject,ii.description,ii.status ,decode(ii.status,0,'Open',1,'InProgress',2,'ReOpen',3,'Resolved',9,'Closed')   as statusname,
ii.ResolutionStatus,ii.TrackID,i4.name as trackname, ii.CreateUser, substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserName,  ii.Createdate,ii.UpdateDate,ii.duedate,ii.AssigneeUser, substr(h1.userfname, 0, 1)||'.'||h1.userlname as AssigneeUserName, ii.resolutiondate,ii.resolutionuser,
substr(h3.userfname, 0, 1)||'.'||h3.userlname as resolutionuserName,
ii.votes  from issue ii
left join IssueProject  i1 on ii.projectid=i1.projectid
left join ProjectComp p on ii.PROJECTCOMPID=p.PROJECTCOMPID 
left join IssueTypes  i2 on ii.IssueTypeID=i2.IssueTypeID
left join IssuePriority i3 on ii.IssuePriorID=i3.IssuePriorID
left join hpuser h1 on ii.AssigneeUser=h1.userno
left join hpuser h2 on ii.CreateUser=h2.userno
left join hpuser h3 on ii.resolutionuser=h3.userno
left join IssueTracks i4 on ii.trackid=i4.IssueTrackID
left join issuecustomer ic on II.ISSUEID=IC.ISSUEID
left join customer c on ic.CUSTNO=C.CUSTOMERNO 
where ii.issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB256012", pissueid);

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

        #region [ DB256013 - Холбоо барьсан харилцагчийн асуудлын жагсаалт авах ]
        public static Result DB256013(DbConnections pDB, int pageindex, int pagerow, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "c.custno like","ii.issueid like", "ii.projectid", "ii.PROJECTCOMPID like", "ii.IssueTypeID like",
                "ii.IssuePriorID","ii.Status", "ii.ResolutionStatus", "ii.TrackID", "ii.CreateUser","ii.Createdate","ii.UpdateDate","ii.duedate","ii.AssigneeUser","ii.resolutiondate","ii.resolutionuser"
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
                                sb.AppendFormat(" {0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" {0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT c.custno,ii.issueid,ii.projectid,i1.name as prjectname, Ii.PROJECTCOMPID,p.name as projectcopname,  ii.IssueTypeID,i2.name as issuetypeidname, ii.IssuePriorID,i3.name as issuepriorname, ii.subject,ii.description,ii.status ,decode(ii.status,0,'Open',1,'InProgress',2,'ReOpen',3,'Resolved',9,'Closed')   as statusname,
ii.ResolutionStatus,ii.TrackID,i4.name as trackname, ii.CreateUser, substr(h2.userfname, 0, 1)||'.'||h2.userlname as CreateUserName,  ii.Createdate,ii.UpdateDate,ii.duedate,ii.AssigneeUser, substr(h1.userfname, 0, 1)||'.'||h1.userlname as AssigneeUserName, ii.resolutiondate,ii.resolutionuser,
substr(h3.userfname, 0, 1)||'.'||h3.userlname as resolutionuserName,
ii.votes  from issue ii
left join IssueProject  i1 on ii.projectid=i1.projectid
left join ProjectComp p on ii.PROJECTCOMPID=p.PROJECTCOMPID 
left join IssueTypes  i2 on ii.IssueTypeID=i2.IssueTypeID
left join IssuePriority i3 on ii.IssuePriorID=i3.IssuePriorID
left join hpuser h1 on ii.AssigneeUser=h1.userno
left join hpuser h2 on ii.CreateUser=h2.userno
left join hpuser h3 on ii.resolutionuser=h3.userno
left join IssueTracks i4 on ii.trackid=i4.IssueTrackID
left join issuecustomer c on ii.issueid=c.issueid
left join contact cc on c.custno=cc.customerno
where ii.issueid=c.issueid and c.custno=cc.customerno
{0} {1} {2}", sb.Length > 0 ? "and" : "", sb.ToString(), " order by ii.issueid desc");

                res = pDB.ExecuteQuery("core", sql, "DB256013", pageindex, pagerow, dbparam.ToArray());
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




        #endregion
        #region[DB257-IssueMembers (Асуудлын холбогдох хүмүүс)]
        #region [ DB257000 - Асуудлын холбогдох хүмүүсийн жагсаалт авах ]
        public static Result DB257000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.issueid,i.userno,substr(h.userfname, 0, 1)||'.'||h.userlname as username, i.issuepurpid,ii.name as IssuePurpIdName, i.description from IssueMembers i
left join hpuser h on i.userno=h.userno
left join IssueMemberPurp ii on i.issuepurpid=ii.issuepurpid
order by i.issueid";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB257000", null);

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
        #region [ DB257001 - Асуудлын холбогдох хүмүүс дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB257001(DbConnections pDB, long pIssueid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.issueid,i.userno,substr(h.userfname, 0, 1)||'.'||h.userlname as username, i.issuepurpid,ii.name as IssuePurpIdName, i.description from IssueMembers i
left join hpuser h on i.userno=h.userno
left join IssueMemberPurp ii on i.issuepurpid=ii.issuepurpid
where i.issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB257001", pIssueid);

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
        #region [ DB257002 - Асуудлын холбогдох хүмүүс шинээр нэмэх ]
        public static Result DB257002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueMembers(issueid,userno,issuepurpid,description) values(:1,:2,:3,:4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB257002", pParam);
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
        #region [ DB257003 -Асуудлын холбогдох хүмүүс засварлах ]
        public static Result DB257003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update IssueMembers set userno=:2,issuepurpid=:3,description=:4
where issueid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB257003", pParam);
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
        #region [ DB257004 - Асуудлын холбогдох хүмүүс  устгах ]
        public static Result DB257004(DbConnections pDB, long pissueid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueMembers WHERE issueid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB257004", pissueid);

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

        
        
        
        #endregion
        #region[DB258-IssueVotes (Асуудлын санал өгөх хүмүүс)]
        #region [ DB258000 - Асуудлын санал өгөх хүмүүс жагсаалт авах ]
        public static Result DB258000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.issueid,i.userno,substr(h.userfname, 0, 1)||'.'||h.userlname as username, i.vote,decode(i.vote,0,'Санал өгөөгүй',1,'Тийм',2,'Үгүй') as VoteName from IssueVotes i
left join issue ii on i.issueid=ii.issueid
left join hpuser h on i.userno=h.userno
order by  i.issueid";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB258000", null);

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
        #region [ DB258001 - Асуудлын санал өгөх хүмүүс дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB258001(DbConnections pDB, long pIssueID, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.issueid,i.userno,substr(h.userfname, 0, 1)||'.'||h.userlname as username, i.vote,decode(i.vote,0,'Санал өгөөгүй',1,'Тийм',2,'Үгүй') as VoteName from IssueVotes i
left join issue ii on i.issueid=ii.issueid
left join hpuser h on i.userno=h.userno
where i.issueid=:1 and i.userno=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB258001", pIssueID, pUserNo);

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
        #region [ DB258002 - Асуудлын санал өгөх хүмүүс шинээр нэмэх ]
        public static Result DB258002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueVotes(issueid,userno,vote) values(:1,:2,:3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB258002", pParam);
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
        #region [ DB258003 -Асуудлын санал өгөх хүмүүс засварлах ]
        public static Result DB258003(DbConnections pDB, long pOldIssueID, int pOldUserNo, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                obj[0] = pOldIssueID;
                obj[1] = pOldUserNo;
                obj[2] = pNewParam[0];
                obj[3] = pNewParam[1];
                obj[4] = pNewParam[2];
            
                string sql =
@"update IssueVotes set issueid=:3,userno=:4,vote=:5
where issueid=:1 and userno=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB258003", obj);
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
        #region [ DB258004 - Асуудлын санал өгөх хүмүүс  устгах ]
        public static Result DB258004(DbConnections pDB, long pIssueID,int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueVotes WHERE issueid=:1 and userno=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB258004", pIssueID,pUserNo);

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
        #region [ DB258005 - Асуудлын санал өгсөн хүмүүсийн жагсаалт авах ]
        public static Result DB258005(DbConnections pDB,long pIssueID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.userno,substr(h.userfname, 0, 1)||'.'||h.userlname as username from IssueVotes i
left join issue ii on i.issueid=ii.issueid
left join hpuser h on i.userno=h.userno
where i.issueid=:1 and vote=1
order by  i.issueid";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB258005", pIssueID);

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




        #endregion
        #region[DB259-IssueTxn (Асуудлын гүйлгээ)]
        #region [ DB259000 - Асуудлын гүйлгээ жагсаалт авах ]
        public static Result DB259000(DbConnections pDB,long pIssueID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.JRNO,i.issueid,i.txndate,i.postdate,i.userno, substr(h1.userfname, 0, 1)||'.'||h1.userlname as UserName,  i.actiontypeid,a.name as actiontypename, i.subject,i.description,i.status,decode(i.status,0,'Хэвийн гүйлгээ',9,'Хаах гүйлгээ') as statusname,
i.resolutiontypeid,b.name as resolutiontypename, i.trackid,c.name as trackname, i.assigneeuser,substr(h1.userfname, 0, 1)||'.'||h1.userlname as assigneeuserName, i.nextpurpose,i.nextdate,i.ContractType,cc.name as contacttypename
 from issuetxn i
 left join IssueActionType a on  i.actiontypeid=a.actiontypeid
 left join IssueResolutionType b on i.resolutiontypeid=b.resolutiontypeid
 left join IssueTracks c on i.trackid=c.IssueTrackID
 left join hpuser h1 on i.userno=h1.userno
left join hpuser h2 on i.assigneeuser=h2.userno
left join custcontacttype cc on cc.typecode=i.ContractType
where i.issueid=:1
order by 1 desc";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB259000", pIssueID);

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
        #region [ DB259001 - Асуудлын гүйлгээ жагсаалт авах JRNO ]
        public static Result DB259001(DbConnections pDB, long pJRNO)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.jrno,i.issueid,i.txndate,i.postdate,i.userno, substr(h1.userfname, 0, 1)||'.'||h1.userlname as UserName,  i.actiontypeid,a.name as actiontypename, i.subject,i.description,i.status,decode(i.status,0,'Хэвийн гүйлгээ',1,'Хаах гүйлгээ') as statusname,
i.resolutiontypeid,b.name as resolutiontypename, i.trackid,c.name as trackname, i.assigneeuser,substr(h1.userfname, 0, 1)||'.'||h1.userlname as assigneeuserName, i.nextpurpose,i.nextdate,i.ContractType,cc.name as contacttypename
 from issuetxn i
 left join IssueActionType a on  i.actiontypeid=a.actiontypeid
 left join IssueResolutionType b on i.resolutiontypeid=b.resolutiontypeid
 left join IssueTracks c on i.trackid=c.IssueTrackID
 left join hpuser h1 on i.userno=h1.userno
left join hpuser h2 on i.assigneeuser=h2.userno
left join custcontacttype cc on cc.typecode=i.ContractType
where i.JRNO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB259001", pJRNO);

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
        #region [ DB259002 - Insert IssueTxn-ий гүйлгээ хийх ]
        public static Result DB259002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                ulong JRNO = EServ.Interface.Sequence.NextByVal("JRNO");

                pParam[0] = Static.ToStr(JRNO);
                string sql =
@"INSERT INTO IssueTxn(JRNO, IssueID, TxnDate, PostDate, UserNo, ActionTypeID, Subject, Description, Status, ResolutionTypeID,
TrackID, AssigneeUser, NextPurpose, NextDate,ContractType)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14,:15)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB259002", pParam);
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

        #endregion
        #region[DB260-IssueTxnLog (Асуудлын гүйлгээний дэлгэрэнгүй лог)]
        #region [ DB260000 - Асуудлын гүйлгээний логийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB260000(DbConnections pDB, long pIssueTxnID)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT IssueTxnID,SubJRNo,ItemName,OldValue,NewValue
FROM IssueTxnLog
WHERE IssueTxnID=:1 ORDER BY  IssueTxnID,SubJRNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB260000", pIssueTxnID);

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
        #region [ DB260001 -Асуудлын гүйлгээний дэлгэрэнгүй лог шинээр нэмэх]
        public static Result DB260001(DbConnections pDB, ulong pIssueTxnID, string pItemName, string pOldValue, string pNewValue)
        {
            Result res = new Result();
            try
            {

                ulong SubJRNo = EServ.Interface.Sequence.NextByVal("SubJRNo");
              
                string sql = @"insert into IssueTxnLog(IssueTxnID,SubJRNo,ItemName,OldValue,NewValue) values(:1,:2,:3,:4,:5)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB260001", new object[] { pIssueTxnID, SubJRNo, pItemName, pOldValue, pNewValue });

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
        #region[DB261-IssueTxnAttach (Асуудлын гүйлгээний хавсралт)]
        #region [ DB261000 - Асуудлын гүйлгээний хавсралтын жагсаалт авах ]
        public static Result DB261000(DbConnections pDB, int pagenumber, int pagecount, long pIssueID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select issuetxnid,AttachID,IssueID,FileName,CreateDate,Description,FileType from IssueTxnAttach 
WHERE issueid=:1
order by 1 desc";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB261000", pIssueID);

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
        #region [ DB261001 - Асуудлын гүйлгээний хавсралтын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB261001(DbConnections pDB, int pagenumber, int pagecount,long pIssueTxnID, long pAttachID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select issuetxnid,AttachID,IssueID,FileName,CreateDate,Description,FileType from IssueTxnAttach 
where issuetxnid=:1 and ATTACHID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB261001", pIssueTxnID,pAttachID);

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
        #region [ DB261002 - Асуудал гүйлгээний хавсралт шинээр нэмэх ]
        public static Result DB261002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
               
                
                string sql =
@"insert into IssueTxnAttach(IssueTxnID,AttachID,IssueID,FileName,CreateDate,Description,FileType) 
values(:1,:2,:3,:4,:5,:6,:7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB261002", pParam);
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
        #region[DB261003-Асуудлын гүйлгээний хавсралтыг засварлах]
        public static Result DB261003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE IssueTxnAttach  SET
IssueID=:3, FileName=:4, CreateDate=:5, Description=:6, FileType=:7
WHERE IssueTxnID=:1 AND AttachID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB261003", pParam);

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
        #region [ DB261004 - Асуудлын гүйлгээний хавсралтууд устгах ]
        public static Result DB261004(DbConnections pDB, long IssueTxnID, long AttachID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueTxnAttach WHERE IssueTxnID=:1 AND AttachID=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB261004", IssueTxnID, AttachID);

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



        #endregion
        #region[DB262-IssueLinks (Асуудлын холбоосууд)]
        #region [ DB262000 - Асуудлын холбоосуудын жагсаалт мэдээлэл авах ]
        public static Result DB262000(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.SourceIssueID,i.DestIssueID,I.LINKETYPEID,ii.name as linktypename from IssueLinks  i
left join issuelinktype ii on I.LINKETYPEID=II.LINKTYPEID
order by 1,2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB262000", null);

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
        #region [ DB262001 - Асуудлын холбоосуудын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB262001(DbConnections pDB, int pagenumber, int pagecount,long pSourceID,long pDestIssueID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select i.SourceIssueID,i.DestIssueID,I.LINKETYPEID,ii.name as linktypename from IssueLinks  i
left join issuelinktype ii on I.LINKETYPEID=II.LINKTYPEID
where I.SOURCEISSUEID=:1
and I.DESTISSUEID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB262001", pSourceID,pDestIssueID);

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
        #region [ DB262002 - Асуудлын холбоосууд шинээр нэмэх ]
        public static Result DB262002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into IssueLinks(SourceIssueID,DestIssueID,LINKETYPEID) values(:1,:2,:3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB262002", pParam);
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
        #region [ DB262004 - Асуудлын холбоосууд устгах ]
        public static Result DB262004(DbConnections pDB, long pSourceID, long pDestIssueID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM IssueLinks WHERE where I.SOURCEISSUEID=:1 and I.DESTISSUEID=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB262004", pSourceID,pDestIssueID);

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
        #region [ DB262005 - Асуудлыг дэд болон үндсэн асуудлруу шилжүүлэх]
        public static Result DB262005(DbConnections pDB,long pIssueID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete from issuelinks i where i.destissueid=(select ii.issueid from issue ii where i.destissueid=ii.issueid and ii.issueid=:1) and i.linketypeid=1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB262005",pIssueID);

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
        #region [ DB262006 - Асуудлын холбоосуудын "DESTISSUEID" авах ]
        public static Result DB262006(DbConnections pDB, long pDestIssueID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select l.sourceissueid,I.SUBJECT,I.ASSIGNEEUSER,substr(h.userfname, 0, 1)||'.'||h.userlname as AssigneeUserName from issuelinks l
left join issue i on L.SOURCEISSUEID=I.ISSUEID
left join  hpuser h on I.ASSIGNEEUSER=H.USERNO
where destissueid=:1
order by 1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB262006", pDestIssueID);

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
        #endregion
        //
        #region[DB263-Холбоо барьсан харилцагч]
        #region [ DB263000 - Холбоо барьсан харилцагчийн жагсаалт авах ]
        public static Result DB263000(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "CustomerNo like","ClassCode","BranchNo","Status","FirstName like",
                    "LastName like","RegisterNo like","PassNo like","Sex like","TypeCode","CorporateName like","Email like","Telephone like",
                    "Mobile like","HomePhone like","Fax like","DriverNo like"
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
                                sb.AppendFormat(" c.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" c.{0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select c.CustomerNo,
c.ClassCode,decode(c.ClassCode, 0, 'ХУВЬ ХҮН', 1, 'БАЙГУУЛЛАГА') ClassCodeName,
c.TypeCode,i.name as InduTypeCodeName,

c.FirstName,c.LastName,c.MiddleName,c.CorporateName,
c.RegisterNo,c.PassNo,c.Sex, decode(c.Sex, 0, 'ЭР', 1, 'ЭМ') SexName,c.BirthDay,c.Company,c.Position,c.Experience,
c.Email,c.Telephone,c.Mobile,c.HomePhone,c.Fax,
c.WebSite,c.BranchNo,c.Status,
decode(c.Status, 0, 'Идэвхгүй', 1, 'Идэвхтэй') StatusName,c.driverno,c.createdate,c.createuser
From CONTACT  c
left join INDUSTRY i on C.TYPECODE=I.TYPECODE
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), "Order by c.CustomerNo desc");

                res = pDB.ExecuteQuery("core", sql, "DB263000", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB263001 - Холбоо барьсан харилцагчийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB263001(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select c.CustomerNo,
c.ClassCode,c.TypeCode,c.FirstName,c.LastName,c.MiddleName,c.CorporateName,
c.RegisterNo,c.PassNo,c.Sex,c.BirthDay,c.Company,c.Position,c.Experience,
c.Email,c.Telephone,c.Mobile,c.HomePhone,c.Fax,
c.WebSite,c.BranchNo,c.Status,c.driverno,c.createdate,c.createuser
From CONTACT  c
where CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB263001", CustomerID);

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
        #region [ DB263002 - Холбоо барьсан харилцагч шинээр нэмэх ]
        public static Result DB263002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                long seq = 0;
                Core.AutoNumEnum enums = new AutoNumEnum();
                enums.B = Static.ToStr(pParam[20]);
                enums.Y = Static.ToStr(Static.ToDate(pParam[23]).Year);
                //enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[20])).CurrencyCode);
                //enums.P = Static.ToStr(pParam[5]);

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 12, enums);
                //ISM.Lib.Static.WriteToLogFile("GetSeq:" + seqres.ResultDesc);

                if (seqres.ResultNo == 0)
                {
                    seq = Static.ToLong(seqres.ResultDesc);
                    if (seq == 0)
                    {
                        seqres.ResultNo = 9110068;
                        seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:12][" + seqres.ResultDesc + "]";
                        return seqres;
                    }
                }
                else
                    return seqres;

                pParam[0] = seq;

                string sql =
@"INSERT INTO CONTACT(CustomerNo,ClassCode,TypeCode,FirstName,LastName,MiddleName,CorporateName,RegisterNo,PassNo,Sex,BirthDay,Company,Position,Experience,
Email,
Telephone,
Mobile,
HomePhone,
Fax,
WebSite,
BranchNo,
Status,
DriverNo,
CreateDate,
CreateUser
)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20, :21, :22, 
:23, :24, :25)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB263002", pParam);


                int DdIdErrorNo = -2147467259;

                if (res.ResultNo == DdIdErrorNo)
                {
                    if (res.ResultDesc.IndexOf("ORA-00001") != -1)
                    {
                        res.ResultNo = 9110039;
                        res.ResultDesc = "Харилцагчийн регистрийн дугаар давхардаж байна.";
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


        #region [ DB263003 - Холбоо барьсан харилцагч засварлах ]
        public static Result DB263003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTACT SET
ClassCode=:2,TypeCode=:3,FirstName=:4,LastName=:5,MiddleName=:6,CorporateName=:7,RegisterNo=:8,PassNo=:9,Sex=:10,BirthDay=:11,Company=:12,Position=:13,Experience=:14,
Email=:15,Telephone=:16,Mobile=:17,HomePhone=:18,Fax=:19,WebSite=:20,BranchNo=:21,Status=:22,DriverNo=:23,createdate=:24, CreateUser=:25
WHERE CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB263003", pParam);

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
        #region [ DB263004 - Холбоо барьсан харилцагч устгах ]
        public static Result DB263004(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CONTACT WHERE CustomerNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB263004", CustomerID);

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

        #region [ DB263005 - Холбоо барьсан харилцагчийг үндсэн харилцагчруу нэмэх ]
        public static Result DB263005(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            string sql;
            try
            { 

               

              sql =
@"INSERT INTO Customer(CustomerNo,ClassCode,TypeCode,FirstName,LastName,MiddleName,CorporateName,RegisterNo,PassNo,Sex,BirthDay,Company,Position,Experience,
Email,
Telephone,
Mobile,
HomePhone,
Fax,
WebSite,
BranchNo,
Status,
DriverNo,
CreateDate,
CreateUser
)
select c.CustomerNo,
c.ClassCode,c.TypeCode,c.FirstName,c.LastName,c.MiddleName,c.CorporateName,
c.RegisterNo,c.PassNo,c.Sex,c.BirthDay,c.Company,c.Position,c.Experience,
c.Email,c.Telephone,c.Mobile,c.HomePhone,c.Fax,
c.WebSite,c.BranchNo,c.Status,c.driverno,c.createdate,c.createuser
From CONTACT  c
where CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB263005", pParam);


                int DdIdErrorNo = -2147467259;

                if (res.ResultNo == DdIdErrorNo)
                {
                    if (res.ResultDesc.IndexOf("ORA-00001") != -1)
                    {
                        res.ResultNo = 9110039;
                        res.ResultDesc = "Харилцагчийн регистрийн дугаар давхардаж байна.";
                    }
                }

                if (res.ResultNo == 0) {

                    sql =
 @"insert  into customernote(CUSTOMERNO, SEQNO,TXNDATE, POSTDATE, USERNO, NOTE)
SELECT CUSTOMERNO, SEQNO,TXNDATE, POSTDATE, USERNO, NOTE
FROM CONTACTNOTE 
where CustomerNo = :1";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB263005", pParam);
                
                }
                if (res.ResultNo == 0)
                {

                    sql =
 @"insert into customeraddr(CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate)
SELECT CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate
FROM CONTACTADDR
where CustomerNo = :1";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB263005", pParam);

                }

                if (res.ResultNo == 0)
                {

                    sql =
 @"delete from contact where customerno=:1";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB263005", pParam);

                }
            
           if (res.ResultNo == 0)
                {

                    sql =
 @"delete from contactnote where customerno=:1";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB263005", pParam);

                }

           if (res.ResultNo == 0)
           {

               sql =
@"delete from contactaddr where customerno=:1";
               res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB263005", pParam);

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
        

        #endregion
        #region[DB264-Холбоо барьсан харилцагчийн талаарх товч дүгнэлтүүд]
        #region [ DB264000 - Холбоо барьсан харилцагчийн талаарх жагсаалт авах ]
        public static Result DB264000(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO,TXNDATE, POSTDATE, USERNO, NOTE
FROM CONTACTNOTE 
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB264000", CustomerID);

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
        #region [ DB264001 - Холбоо барьсан харилцагчийн талаарх дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB264001(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO,TXNDATE, POSTDATE, USERNO, NOTE
FROM CONTACTNOTE 
where CustomerNo = :1 AND SEQNO = :2
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB264001", CustomerID, SeqNo);

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
        #region [ DB264002 - Холбоо барьсан харилцагчийн талаарх мэдээлэл шинээр нэмэх ]
        public static Result DB264002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;

                string sql =
@"INSERT INTO CONTACTNOTE(CUSTOMERNO, SEQNO, TxnDate, POSTDATE, UserNo, NOTE)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB264002", pParam);

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
        #region [ DB264003 - Холбоо барьсан харилцагчийн талаарх засварлах ]
        public static Result DB264003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;

                string sql =
@"UPDATE CONTACTNOTE  SET
TxnDate=:3, POSTDATE=:4, UserNo=:5, NOTE=:6
WHERE CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB264003", pParam);

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
        #region [ DB264004 - Холбоо барьсан харилцагчийн талаарх мэдээлэл устгах ]
        public static Result DB264004(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CONTACTNOTE  WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB264004", CustomerID, SeqNo);

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
        #endregion
        #region[DB265-Холбоо барьсан харилцагчийн хаяг]
        #region [ DB265000 - Холбоо барьсан харилцагчийн хаягийн жагсаалт авах ]
        public static Result DB265000(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate
FROM CONTACTADDR 
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB265000", CustomerID);

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
        #region [ DB265001 - Холбоо барьсан харилцагчийн хаягийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB265001(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate
FROM CONTACTADDR
where CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB265001", CustomerID, SeqNo);

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
        #region [ DB265002 - Холбоо барьсан харилцагчийн хаяг шинээр нэмэх ]
        public static Result DB265002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CONTACTADDR(CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, sysdate)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB265002", pParam);

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
        #region [ DB265003 -Холбоо барьсан харилцагчийн хаяг засварлах ]
        public static Result DB265003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONTACTADDR SET
CITYCODE=:3, DISTCODE=:4, SUBDISTCODE=:5, NOTE=:6, ADDRCURRENT=:7, APARTMENT=:8
WHERE CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB265003", pParam);

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
        #region [ DB265004 - Холбоо барьсан харилцагчийн хаяг устгах ]
        public static Result DB265004(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CONTACTADDR WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB265004", CustomerID, SeqNo);

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
        #endregion
        //
        #region [ DB266000 - Харилцагч болон холбоо барьсан харилцагчийн жагсаалт авах ]
        public static Result DB266000(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "CustomerNo like","ClassCode","BranchNo","Status","FirstName like",
                    "LastName like","RegisterNo like","PassNo like","Sex like","TypeCode","CorporateName like","Email like","Telephone like",
                    "Mobile like","HomePhone like","Fax like","DriverNo like","CreateDate like","CreateUser"
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
                                sb.AppendFormat(" c.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" c.{0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select c.CustomerNo,
c.ClassCode,decode(c.ClassCode, 0, 'Хувь хүн', 1, 'Байгууллага') ClassCodeName,
c.TypeCode,i.name as InduTypeCodeName,

c.FirstName,c.LastName,c.MiddleName,c.CorporateName,
c.RegisterNo,c.PassNo,c.Sex, decode(c.Sex, 0, 'ЭР', 1, 'ЭМ') SexName,c.BirthDay,c.Company,c.Position,c.Experience,
c.Email,c.Telephone,c.Mobile,c.HomePhone,c.Fax, 
c.WebSite,c.BranchNo,c.Status,
decode(c.Status, 0, 'Идэвхгүй', 1, 'Идэвхтэй') StatusName,c.driverno,c.createdate,substr(u.userfname, 0, 1)||'.'||U.USERLNAME as createuser
From CONTACT  c
left join INDUSTRY i on C.TYPECODE=I.TYPECODE
left join hpuser u on u.userno=c.createuser
{0} {1}", sb.Length > 0 ? "where" : "", sb.ToString());
                string sql1 = string.Format(
@"
{0}

union all
select c.CustomerNo,
c.ClassCode,decode(c.ClassCode, 0,  'Хувь хүн', 1, 'Байгууллага') ClassCodeName,
c.TypeCode,i.name as InduTypeCodeName,
c.FirstName,c.LastName,c.MiddleName,c.CorporateName,
c.RegisterNo,c.PassNo,c.Sex, decode(c.Sex, 0, 'ЭР', 1, 'ЭМ') SexName,c.BirthDay,c.Company,c.Position,c.Experience,c.Email,c.Telephone,c.Mobile,c.HomePhone,c.Fax,
c.WebSite,c.BranchNo,c.Status,
decode(c.Status, 0, 'Гэрээ хийгээгүй', 1, 'Гэрээ хийсэн')  StatusName,c.driverno,c.createdate, substr(u.userfname, 0, 1)||'.'||U.USERLNAME as createuser
From Customer c
left join INDUSTRY i on C.INDUTYPECODE=I.TYPECODE
left join hpuser u on u.userno=c.createuser
{1} {2} {3}", sql, sb.Length > 0 ? "where" : "", sb.ToString(), "Order by CustomerNo desc");

                res = pDB.ExecuteQuery("core", sql1, "DB266000", pageindex, pagerows, dbparam.ToArray());

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
