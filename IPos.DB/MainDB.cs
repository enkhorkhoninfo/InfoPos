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
    public static class Main
    {   
        public static Result F_Error(Result res)
        {
            //int DdIdErrorNo = -2147467259;
            int DdIdErrorNo = 1;

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
        #region [ DB000 - Ерөнхий SQL ]
        //SQL ажиллуулж буцаах
        public static Result DB000000(DbConnections pDB, string pSQL)
        {
            Result res = new Result();
            try
            {
                string sql = pSQL;
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB000000", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        //SQL-ыг параметртэй хамт ажиллуулж буцаах
        public static Result DB000001(DbConnections pDB, string pSQL, object [] pObj)
        {
            Result res = new Result();
            try
            {
                string sql = pSQL;
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB000001", pObj);
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
        #region [ DB200 - hpro.core.dll ]
        #region [ DB200001 - Select General List ]
        public static Result DB200001(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql = @" select  Key, ItemValue from GENERALPARAM ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB200001");

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
        #region [ DB201 - hpro.docutility.dll ]
        #region [ DB201001 - Document template-ийн жагсаалт авах ]
        public static Result DB201001(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] {"ID like", "Name like", "Name2 like", "DOCFileName like", "ExportType"};

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
@"select  ID, Name, Name2, DOCFileName, ExportType from DOCTEMPLATE
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by ID");
                res = pDB.ExecuteQuery("core", sql, "DB201001", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB201002 - Document template-ийн DOCPARAM-ийн жагсаалт авах ]
        public static Result DB201002(DbConnections pDB, long pTemplateID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" select ID, PARAMID, NAME, DESCRIPTION, DOCPARAMTYPE, FORMAT, VALUE, LISTVALUE, REQUIRED, ITEMLEN, 
MASK, ORDERNO
from DOCPARAM 
where ID=:1
order by OrderNo ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201002", pTemplateID);

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
        #region [ DB201003 - Document template-ийн DOCSQL жагсаалт авах ]
        public static Result DB201003(DbConnections pDB, long pTemplateID)
        {
            Result res = new Result();
            try
            {
                string sql = 
@" select  ID, SQL, Params, ItemNo
from DOCSQL
where ID=:1
order by ItemNo ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201003", pTemplateID);

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
        #region [ DB201004 - Document template-ийн SQL -ийг ажилуулах ]
        public static Result DB201004(DbConnections pDB, string pSQL, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = pSQL;
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201004", pParam);

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

        #region [ DB201005 - Document template-ийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB201005(DbConnections pDB, long pTemplateID)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql = 
@"select ID, Name, Name2, DOCFileName, ExportType
from DOCTEMPLATE
where id=:1
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201005", pTemplateID);

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
        #region [ DB201006 - Document template-ийг нэмэх ]
        public static Result DB201006(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();

            try
            {
                string sql =
@"insert into DOCTEMPLATE(ID, NAME, NAME2, DOCFILENAME, EXPORTTYPE)
values(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB201006", pParam);
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
        #region [ DB201007 - Document template-ийг засварлах ]
        public static Result DB201007(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE DOCTEMPLATE SET 
NAME=:2, NAME2=:3, DOCFILENAME=:4, EXPORTTYPE=:5
WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB201007", pParam);

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
        #region [ DB201008 - Document template-ийг устгах ]
        public static Result DB201008(DbConnections pDB, long pTemplateID)
        {
            Result res = new Result();
            try
            {
                //DOCPARAM delete
                string sql =
@"DELETE FROM DOCPARAM WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB201008", pTemplateID);
                if (res.ResultNo != 0) return res;

                //DOCSQL delete
                sql =
@"DELETE FROM DOCSQL WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB201008", pTemplateID);
                if (res.ResultNo != 0) return res;

                //DOCTEMPLATE delete
                sql =
@"DELETE FROM DOCTEMPLATE WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB201008", pTemplateID);

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

        #region [ DB201012 - DOCSQL-ийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB201012(DbConnections pDB, long pTemplateID, int pItemNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select ID, ITEMNO, SQL, PARAMS
from DOCSQL
where id=:1 AND ITEMNO=:2
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201012", pTemplateID, pItemNo);

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
        #region [ DB201013 - DOCSQL-ийг нэмэх ]
        public static Result DB201013(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();

            try
            {
                string sql =
@"insert into DOCSQL(ID, ITEMNO, SQL, PARAMS)
values(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB201013", pParam);
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
        #region [ DB201014 - DOCSQL-ийг засварлах ]
        public static Result DB201014(DbConnections pDB, int pOldItemNo, object[] pParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];

                obj[0] = pOldItemNo;
                obj[1] = pParam[0];
                obj[2] = pParam[1];
                obj[3] = pParam[2];
                obj[4] = pParam[3];

                string sql =
@"UPDATE DOCSQL SET 
ITEMNO=:3, SQL=:4, PARAMS=:5
WHERE ID=:2 AND ITEMNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB201014", obj);

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
        #region [ DB201015 - DOCSQL-ийг устгах ]
        public static Result DB201015(DbConnections pDB, long pTemplateID, int pItemNo)
        {
            Result res = new Result();
            try
            {
                //DOCSQL delete
                string sql =
@"DELETE FROM DOCSQL WHERE ID=:1 AND ITEMNO=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB201015", pTemplateID, pItemNo);

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

        #region [ DB201016 - DOCPARAM-ийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB201016(DbConnections pDB, long pTemplateID, int pParamID)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select ID, PARAMID, NAME, DESCRIPTION, DOCPARAMTYPE, FORMAT, VALUE, LISTVALUE, REQUIRED, ITEMLEN, 
MASK, ORDERNO
from DOCPARAM
where id=:1 AND PARAMID=:2 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201016", pTemplateID, pParamID);

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
        #region [ DB201017 - DOCPARAM-ийг нэмэх ]
        public static Result DB201017(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();

            try
            {
                string sql =
@"insert into DOCPARAM(ID, PARAMID, NAME, DESCRIPTION, DOCPARAMTYPE, FORMAT, VALUE, LISTVALUE, REQUIRED, ITEMLEN, 
MASK, ORDERNO)
values(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB201017", pParam);
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
        #region [ DB201018 - DOCPARAM-ийг засварлах ]
        public static Result DB201018(DbConnections pDB, int pOldParamID, object[] pParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[13];

                obj[0] = pOldParamID;
                obj[1] = pParam[0];
                obj[2] = pParam[1];
                obj[3] = pParam[2];
                obj[4] = pParam[3];
                obj[5] = pParam[4];
                obj[6] = pParam[5];
                obj[7] = pParam[6];
                obj[8] = pParam[7];
                obj[9] = pParam[8];
                obj[10] = pParam[9];
                obj[11] = pParam[10];
                obj[12] = pParam[11];

                string sql =
@"UPDATE DOCPARAM SET 
PARAMID=:3, NAME=:4, DESCRIPTION=:5, DOCPARAMTYPE=:6, FORMAT=:7, VALUE=:8, LISTVALUE=:9, REQUIRED=:10, ITEMLEN=:11, 
MASK=:12, ORDERNO=:13
WHERE ID=:2 AND PARAMID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB201018", obj);

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
        #region [ DB201019 - DOCPARAM-ийг устгах ]
        public static Result DB201019(DbConnections pDB, long pTemplateID, int pParamID)
        {
            Result res = new Result();
            try
            {
                //DOCPARAM delete
                string sql =
@"DELETE FROM DOCPARAM WHERE ID=:1 AND PARAMID=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB201019", pTemplateID, pParamID);

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
        #region [ DB201020 - DOCSQL дээрх холбоотой болон холбоогүй DOCPARAM-ийн мэдээлэл авах]
        public static Result DB201020(DbConnections pDB, long pTemplateID, int pItemNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select decode(aa.id , null, 0, 1) as status, a.ID, a.PARAMID, a.NAME, a.DESCRIPTION, a.DOCPARAMTYPE, a.FORMAT, a.VALUE, a.LISTVALUE, a.REQUIRED,
a.ITEMLEN, a.MASK, a.ORDERNO
from docparam a 
left join (
select a.ID, a.PARAMID, a.NAME, a.DESCRIPTION, a.DOCPARAMTYPE, a.FORMAT, a.VALUE, a.LISTVALUE, a.REQUIRED, a.ITEMLEN, 
a.MASK, a.ORDERNO
from docparam a
where a.ID = (
SELECT D.ID
FROM DOCSQL D
where d.id=:1 and d.itemno=:2
)
and a.PARAMID in 
(select column_value FROM 
TABLE ( 
SELECT HProSplit(D.PARAMS, ',')
FROM DOCSQL D
where d.id=:1 and d.itemno=:2))) aa on a.id=aa.id AND a.paramid=aa.paramid
where A.ID=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201020", pTemplateID, pItemNo);

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
        #region [ DB201021 - DOCSQL дээр DOCPARAM-ийн ID-нүүдийг хадгалах ]
        public static Result DB201021(DbConnections pDB, long pTemplateID, int pItemNo, string pParamIDs)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE DOCSQL SET 
PARAMS=:3
WHERE ID=:1 AND ITEMNO=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB201021", pTemplateID, pItemNo, pParamIDs);

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

        //docgroup
        #region [ DB201022 - Документ загвар бүлэглэх жагсаалт мэдээлэл авах ]
        public static Result DB201022(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select groupid,name,name2,orderno from docgroup
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201022", null);

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
        #region [ DB201023 - Документ загвар бүлэглэх дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB201023(DbConnections pDB, int pGroupID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select groupid,name,name2,orderno from docgroup
where groupid=:1
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201023", pGroupID);

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
        #region [ DB201024 - Документ загвар бүлэглэл нэмэх ]
        public static Result DB201024(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into docgroup(groupid, Name, Name2, OrderNo)
values(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB201024", pParam);
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
        #region [ DB201025 - Документ загвар бүлэглэл засварлах ]
        public static Result DB201025(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE docgroup SET Name=:2, Name2=:3,OrderNo=:4
WHERE groupid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB201025", pParam);
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
        #region [ DB201026 - Документ загвар бүлэглэл устгах ]
        public static Result DB201026(DbConnections pDB, long pgroupid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete FROM docgroup WHERE groupid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB201026", pgroupid);

            

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
        //doclink
        #region [ DB201027 - Документ загварын холбоосын сонгогдсон болон сонгогдоогүй жагсаалт мэдээлэл авах ]
        public static Result DB201027(DbConnections pDB, int pGroupID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select  decode(A.id,null,0,1) as status ,d.id ,d.name,d.name2,d.docfilename,d.exporttype  from (select id from docgroup a,doclink b where a.groupid=b.groupid and b.groupid=:1 ) a
right join doctemplate d on  a.id=d.id
ORDER BY D.ID
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201027", pGroupID);

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
        #region [ DB201028 - Документ загварын холбоосын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB201028(DbConnections pDB, int pGroupID, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"sselect a.groupid,a.id ,d.name,d.name2,d.docfilename,d.exporttype  from doclink a
left join doctemplate d on  a.id=d.id
where groupid=:1 and id=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201028", pGroupID,pID);

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
        #region [ DB201028 - Документ загварын холбоос нэмэх ]
        public static Result DB201028(DbConnections pDB, int pGroupID, DataTable pDT)
        {

            Result res = new Result();
            try
            {
                string sql = "";

                //Delete StepLink
                sql =
@"delete from doclink where GroupID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB201028", pGroupID);
                if (res.ResultNo != 0)
                    return res;
                //Insert StepLink
                foreach (DataRow dr in pDT.Rows)
                {
                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
@"insert into doclink(GroupID,ID)
values(:1, :2)";

                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB201028", new object[] { pGroupID, dr["ID"] });
                        res = F_Error(res);
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
        #region [ DB201029 - Документ загварын холбоос засварлах ]
        public static Result DB201029(DbConnections pDB, int pOldGroupID,int pOldID, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[4];
                obj[0] = pOldGroupID;
                obj[1] = pOldID;
                obj[2] = pNewParam[0];
                obj[3] = pNewParam[1];
          
                string sql =
@"UPDATE doclink SET groupid=:3, id=:4
WHERE groupid=:1 and id=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB201029", obj);
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
        #region [ DB201030 - Документ загварын холбоос устгах ]
        public static Result DB201030(DbConnections pDB, int pgroupid,int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete FROM doclink WHERE groupid=:1 and id=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB201030", pgroupid,pID);



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
        #region [ DB201031 - Документ загварын холбоосын сонгогдсон жагсаалт мэдээлэл авах ]
        public static Result DB201031(DbConnections pDB, int pGroupID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select d.id ,d.name,d.name2,d.docfilename,d.exporttype  from (select id from docgroup a,doclink b where a.groupid=b.groupid and b.groupid=:1 ) a
right join doctemplate d on  a.id=d.id
where  decode(d.id, null,0, 1)=1
order by d.id";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB201031", pGroupID);

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
        #region [ DB202 - Parameter ]
        #region [ Parameter-1-1 ]
        #region [ DB202001 - Салбарын жагсаалт авах ]
        public static Result DB202001(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT BRANCH, NAME, NAME2, DIRECTOR, ORDERNO
FROM BRANCH Order By ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202001", null);

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
        #region [ DB202002 - Салбарын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202002(DbConnections pDB, long pBranchNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BRANCH, NAME, NAME2, DIRECTOR, ORDERNO
FROM BRANCH
where BRANCH = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202002", pBranchNo);

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
        #region [ DB202003 - Салбар шинээр нэмэх ]
        public static Result DB202003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into branch(Branch, Name, Name2, Director, OrderNo)
values(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202003", pParam);
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
        #region [ DB202004 - Салбар засварлах ]
        public static Result DB202004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE branch SET Name=:2, Name2=:3, Director=:4, OrderNo=:5
WHERE Branch=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202004", pParam);
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
        #region [ DB202005 - Салбар устгах ]
        public static Result DB202005(DbConnections pDB, long pBranchNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select BRANCHNO FROM BACACCOUNT WHERE BRANCHNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202005", pBranchNo);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110047;
                    res.ResultDesc = "Энэ салбар дээр данс үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM Branch WHERE Branch=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202005", pBranchNo);

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
        //Өгсөн
        #region [ DB202006 - Улсын жагсаалт авах ]
        public static Result DB202006(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT COUNTRYCODE, NAME, NAME2, ORDERNO
FROM COUNTRY ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202006", null);

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
        #region [ DB202007 - Улсын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202007(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT COUNTRYCODE, NAME, NAME2, ORDERNO
FROM COUNTRY
where COUNTRYCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202007", pNo);

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
        #region [ DB202008 - Улс шинээр нэмэх ]
        public static Result DB202008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into COUNTRY(COUNTRYCODE, NAME, NAME2, ORDERNO)
values(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202008", pParam);
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
        #region [ DB202009 - Улс засварлах ]
        public static Result DB202009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE COUNTRY SET NAME=:2, NAME2=:3, ORDERNO=:4
WHERE COUNTRYCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202009", pParam);
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
        #region [ DB202010 - Улс устгах ]
        public static Result DB202010(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM COUNTRY WHERE COUNTRYCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202010", pNo);

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
        //Өгсөн
        #region [ DB202011 - Хэлний жагсаалт авах ]
        public static Result DB202011(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT LANGUAGECODE, NAME, NAME2, ORDERNO
FROM LANGUAGE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202011", null);

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
        #region [ DB202012 - Хэлний дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202012(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT LANGUAGECODE, NAME, NAME2, ORDERNO
FROM LANGUAGE
where LANGUAGECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202012", pNo);

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
        #region [ DB202013 - Хэл шинээр нэмэх ]
        public static Result DB202013(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO LANGUAGE(LANGUAGECODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202013", pParam);
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
        #region [ DB202014 - Хэл засварлах ]
        public static Result DB202014(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE LANGUAGE SET NAME=:2, NAME2=:3, ORDERNO=:4
WHERE LANGUAGECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202014", pParam);
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
        #region [ DB202015 - Хэл устгах ]
        public static Result DB202015(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM LANGUAGE WHERE LANGUAGECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202015", pNo);

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
        //Өгсөн
        #region [ DB202016 - Валютын жагсаалт авах ]
        public static Result DB202016(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CurrencyCode,
GLExchProfit, GLExchLoss, GLRevProfit, GLRevLoss, OldRate, OrderNO, fractionname
FROM CURRENCY
ORDER BY OrderNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202016", null);

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
        #region [ DB202017 - Валютын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202017(DbConnections pDB, int pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CurrencyCode,
GLExchProfit, GLExchLoss, GLRevProfit, GLRevLoss, OldRate, OrderNO, fractionname
FROM CURRENCY
WHERE CURRENCY = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202017", pNo);

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
        #region [ DB202018 - Валют шинээр нэмэх ]
        public static Result DB202018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[17];
                for (int i = 0; i <= 16; i++)
                    obj[i] = pParam[i];

                string sql =
@"INSERT INTO CURRENCY(CURRENCY, NAME, NAME2, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, GLEQUIV, CurrencyCode,
GLExchProfit, GLExchLoss, GLRevProfit, GLRevLoss, OldRate, OrderNO, fractionname)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202018", obj);

                if (res.ResultNo != 0)
                {
                    res = F_Error(res);
                    return res;
                }

                sql =
@"MERGE INTO CurrencyHist b
USING (
SELECT Currency, :1 curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE
FROM Currency
) e
ON (b.Currency = e.Currency and b.curdate = e.curdate)
WHEN MATCHED THEN
UPDATE SET b.Rate = e.Rate, b.CashBuyRate=e.CashBuyRate , b.CashSellRate=e.CashSellRate , b.NonCashBuyRate=e.NonCashBuyRate , b.NonCashSellRate=e.NonCashSellRate , b.OLDRATE=e.OLDRATE
WHEN NOT MATCHED THEN
insert (Currency, curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE)
values (e.Currency, :1, e.Rate, e.CashBuyRate, e.CashSellRate, e.NonCashBuyRate, e.NonCashSellRate, e.OLDRATE)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202018", pParam[17]);

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
        #region [ DB202019 - Валют засварлах ]
        public static Result DB202019(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[17];
                for(int i=0; i<=16; i++)
                    obj[i] = pParam[i];

                string sql =
@"UPDATE CURRENCY SET
NAME=:2, NAME2=:3, RATE=:4, CASHBUYRATE=:5, CASHSELLRATE=:6, NONCASHBUYRATE=:7, NONCASHSELLRATE=:8, GLEQUIV=:9, CurrencyCode=:10,
GLExchProfit=:11, GLExchLoss=:12, GLRevProfit=:13, GLRevLoss=:14, OldRate=:15, OrderNo=:16, fractionname=:17
WHERE CURRENCY=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202019", obj);
                res = F_Error(res);
                
                if (res.ResultNo != 0)
                    return res;

                sql =
@"MERGE INTO CurrencyHist b
USING (
SELECT Currency, :1 curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE
FROM Currency
) e
ON (b.Currency = e.Currency and b.curdate = e.curdate)
WHEN MATCHED THEN
UPDATE SET b.Rate = e.Rate, b.CashBuyRate=e.CashBuyRate , b.CashSellRate=e.CashSellRate , b.NonCashBuyRate=e.NonCashBuyRate , b.NonCashSellRate=e.NonCashSellRate , b.OLDRATE=e.OLDRATE
WHEN NOT MATCHED THEN
insert (Currency, curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE)
values (e.Currency, :1, e.Rate, e.CashBuyRate, e.CashSellRate, e.NonCashBuyRate, e.NonCashSellRate, e.OLDRATE)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202019", pParam[17]);
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
        #region [ DB202020 - Валют устгах ]
        public static Result DB202020(DbConnections pDB, string pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select CURCODE FROM BACACCOUNT WHERE CURCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202020", pNo);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110048;
                    res.ResultDesc = "Энэ валют дээр данс үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM CURRENCY WHERE CURRENCY=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202020", pNo);

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
        //Өгсөн
        #region [ DB202021 - Банкны жагсаалт авах ]
        public static Result DB202021(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BANKID, NAME, NAME2, ORDERNO
FROM BANK
ORDER BY OrderNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202021", null);

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
        #region [ DB202022 - Банкны дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202022(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BANKID, NAME, NAME2, ORDERNO
FROM BANK
WHERE BANKID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202022", pNo);

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
        #region [ DB202023 - Банк шинээр нэмэх ]
        public static Result DB202023(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO BANK(BANKID, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202023", pParam);
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
        #region [ DB202024 - Банк засварлах ]
        public static Result DB202024(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BANK SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE BANKID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202024", pParam);
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
        #region [ DB202025 - Банк устгах ]
        public static Result DB202025(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM BANK WHERE BANKID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202025", pNo);

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
        //Өгсөн
        #region [ DB202026 - Түр дансны бүртгэлийн жагсаалт авах ]
        public static Result DB202026(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CODE, CURRENCY, BRANCH, ACCOUNTNO, NOTE, CODETYPE
FROM ACCOUNTCODE
ORDER BY CODE";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202026", null);

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
        #region [ DB202027 - Түр дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202027(DbConnections pDB, string pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CODE, CURRENCY, BRANCH, ACCOUNTNO, NOTE, codetype
FROM ACCOUNTCODE
WHERE CODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202027", pNo);

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
        #region [ DB202028 - Түр дансны бүртгэл шинээр нэмэх ]
        public static Result DB202028(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO ACCOUNTCODE(CODE, CURRENCY, BRANCH, ACCOUNTNO, NOTE, CODETYPE)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202028", pParam);
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
        #region [ DB202029 - Түр дансны бүртгэл засварлах ]
        public static Result DB202029(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ACCOUNTCODE SET
CURRENCY=:2, BRANCH=:3, ACCOUNTNO=:4, NOTE=:5, CODETYPE=:6
WHERE CODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202029", pParam);
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
        #region [ DB202030 - Түр дансны бүртгэл устгах ]
        public static Result DB202030(DbConnections pDB, string pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ACCOUNTCODE WHERE CODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202030", pNo);

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
        //Өгсөн
        #region [ DB202031 - Автомат дансны бүртгэлийн жагсаалт авах ]
        public static Result DB202031(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =@"SELECT ID, CODE, NAME, NAME2, MASK, KEY, NOTE
                            FROM AUTONUM
                            order by ID";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202031", null);

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
        #region [ DB202032 - Автомат дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202032(DbConnections pDB, long pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, NAME, NAME2, MASK, KEY 
FROM AUTONUM
Where ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202032", pID);

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
        #region [ DB202033 - Автомат дансны бүртгэл шинээр нэмэх ]
        public static Result DB202033(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO AUTONUM(ID, NAME, NAME2, MASK, KEY)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202033", pParam);
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
        #region [ DB202034 - Автомат дансны бүртгэл засварлах ]
        public static Result DB202034(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE AUTONUM SET
NAME=:2, NAME2=:3, MASK=:4, KEY=:5
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202034", pParam);
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
        #region [ DB202035 - Автомат дансны бүртгэл устгах ]
        public static Result DB202035(DbConnections pDB, long pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM AUTONUM WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202035", pID);

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
        //Өгсөн
        #region [ DB202036 - Харилцагчийн төрлийн жагсаалт авах ]
        public static Result DB202036(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM CUSTOMERTYPE 
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202036", null);

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
        #region [ DB202037 - Харилцагчийн төрлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202037(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM CUSTOMERTYPE 
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202037", pNo);

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
        #region [ DB202038 - Харилцагчийн төрөл шинээр нэмэх ]
        public static Result DB202038(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTOMERTYPE(TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202038", pParam);
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
        #region [ DB202039 - Харилцагчийн төрөл засварлах ]
        public static Result DB202039(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERTYPE SET
CLASSCODE=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202039", pParam);
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
        #region [ DB202040 - Харилцагчийн төрөл устгах ]
        public static Result DB202040(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERTYPE WHERE TYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202040", pNo);

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
        //Өгсөн
        #region [ DB202041 - Гэр бүлийн хамаарлын төрөл жагсаалт авах ]
        public static Result DB202041(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, NAME, NAME2, ORDERNO,ClassCode 
FROM FAMILYTYPE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202041", null);

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
        #region [ DB202042 - Гэр бүлийн хамаарлын төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202042(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, NAME, NAME2,ORDERNO,ClassCode
FROM FAMILYTYPE
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202042", pNo);

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
        #region [ DB202043 - Гэр бүлийн хамаарлын төрөл шинээр нэмэх ]
        public static Result DB202043(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO FAMILYTYPE(TYPECODE, NAME, NAME2, ORDERNO,ClassCode)
VALUES(:1, :2, :3, :4,:5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202043", pParam);
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
        #region [ DB202044 - Гэр бүлийн хамаарлын төрөл засварлах ]
        public static Result DB202044(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE FAMILYTYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4,ClassCode=:5
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202044", pParam);
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
        #region [ DB202045 - Гэр бүлийн хамаарлын төрөл устгах ]
        public static Result DB202045(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM FAMILYTYPE WHERE TYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202045", pNo);

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
        //Өгсөн
        #region [ DB202046 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт жагсаалт авах ]
        public static Result DB202046(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM INDUSTRY
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202046", null);

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
        #region [ DB202047 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202047(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM INDUSTRY
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202047", pNo);

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
        #region [ DB202048 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт шинээр нэмэх ]
        public static Result DB202048(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO INDUSTRY(TYPECODE, CLASSCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202048", pParam);
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
        #region [ DB202049 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт засварлах ]
        public static Result DB202049(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE INDUSTRY SET
CLASSCODE=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE TYPECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202049", pParam);
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
        #region [ DB202050 - Байгууллага - Үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт устгах ]
        public static Result DB202050(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select INDUTYPECODE FROM CUSTOMER WHERE INDUTYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202050", pNo);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110062;
                    res.ResultDesc = "Энэ үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт дээр харилцагч үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM INDUSTRY WHERE TYPECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202050", pNo);

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
        //Өгсөн
        #region [ DB202051 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт жагсаалт авах ]
        public static Result DB202051(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, SUBTYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM SUBINDUSTRY
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202051", null);

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
        #region [ DB202052 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202052(DbConnections pDB, long pTypeCode, long pSubTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPECODE, SUBTYPECODE, CLASSCODE, NAME, NAME2, ORDERNO
FROM SUBINDUSTRY
WHERE TYPECODE=:1 AND SUBTYPECODE = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202052", pTypeCode, pSubTypeCode);

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
        #region [ DB202053 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт шинээр нэмэх ]
        public static Result DB202053(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO SUBINDUSTRY(TYPECODE, SUBTYPECODE, CLASSCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202053", pParam);
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
        #region [ DB202054 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт засварлах ]
        public static Result DB202054(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SUBINDUSTRY SET
CLASSCODE=:3, NAME=:4, NAME2=:5, ORDERNO=:6
WHERE TYPECODE=:1 AND SUBTYPECODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202054", pParam);
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
        #region [ DB202055 - Байгууллага - Дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт устгах ]
        public static Result DB202055(DbConnections pDB, long pTypeCode, long pSubTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select INDUTYPECODE FROM CUSTOMER WHERE INDUTYPECODE=:1 AND INDUSUBTYPECODE=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202055", pTypeCode, pSubTypeCode);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110063;
                    res.ResultDesc = "Энэ дэд үйл ажиллагааны чиглэл, Хувь хүн - Ажил эрхлэлт дээр харилцагч үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM SUBINDUSTRY WHERE TYPECODE=:1 AND SUBTYPECODE=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202055", pTypeCode, pSubTypeCode);
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
        #endregion
        #region [ Parameter-1-2 ]
        //
        #region [ DB202056 - Аймаг хотын бүртгэл жагсаалт авах ]
        public static Result DB202056(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTCITY
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202056", null);

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
        #region [ DB202057 - Аймаг хотын бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202057(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTCITY
WHERE CITYCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202057", pNo);

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
        #region [ DB202058 - Аймаг хотын бүртгэл нэмэх ]
        public static Result DB202058(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTCITY(CITYCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202058", pParam);
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
        #region [ DB202059 - Аймаг хотын бүртгэл засварлах ]
        public static Result DB202059(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTCITY SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE CITYCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202059", pParam);
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
        #region [ DB202060 - Аймаг хотын бүртгэл устгах ]
        public static Result DB202060(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTCITY WHERE CITYCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202060", pNo);

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
        //
        #region [ DB202061 - Сум дүүрэг бүртгэл жагсаалт авах ]
        public static Result DB202061(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT DISTCODE, CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTDISTRICT
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202061", null);

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
        #region [ DB202062 - Сум дүүрэг бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202062(DbConnections pDB, long pDistCode, long pCityCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT DISTCODE, CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTDISTRICT
WHERE DISTCODE = :1 AND CITYCODE = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202062", pDistCode, pCityCode);

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
        #region [ DB202063 - Сум дүүрэг бүртгэл шинээр нэмэх ]
        public static Result DB202063(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTDISTRICT( DISTCODE, CITYCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202063", pParam);
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
        #region [ DB202064 - Сум дүүрэг бүртгэл засварлах ]
        public static Result DB202064(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTDISTRICT SET
CITYCODE=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE DISTCODE=:1 AND CITYCODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202064", pParam);
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
        #region [ DB202065 - Сум дүүрэг бүртгэл устгах ]
        public static Result DB202065(DbConnections pDB, long pDistCode, long pCityCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTDISTRICT WHERE DISTCODE=:1 AND CITYCODE = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202065", pDistCode, pCityCode);

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
        //
        #region [ DB202066 - Баг, хороо бүртгэл жагсаалт авах ]
        public static Result DB202066(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT SUBDISTCODE, DISTCODE, CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTSUBDISTRICT
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202066", null);

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
        #region [ DB202067 - Баг, хороо бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202067(DbConnections pDB, long pSubDistCode, long pDistCode, long pCityCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT SUBDISTCODE, DISTCODE, CITYCODE, NAME, NAME2, ORDERNO
FROM CUSTSUBDISTRICT
WHERE SUBDISTCODE = :1 AND DISTCODE=:2 AND CITYCODE=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202067", pSubDistCode, pDistCode, pCityCode);

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
        #region [ DB202068 - Баг, хороо бүртгэл шинээр нэмэх ]
        public static Result DB202068(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTSUBDISTRICT(SUBDISTCODE, DISTCODE, CITYCODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202068", pParam);
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
        #region [ DB202069 - Баг, хороо бүртгэл засварлах ]
        public static Result DB202069(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTSUBDISTRICT SET
DISTCODE=:2, CITYCODE=:3, NAME=:4, NAME2=:5, ORDERNO=:6
WHERE SUBDISTCODE = :1 AND DISTCODE=:2 AND CITYCODE=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202069", pParam);
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
        #region [ DB202070 - Баг, хороо бүртгэл устгах ]
        public static Result DB202070(DbConnections pDB, long pSubDistCode, long pDistCode, long pCityCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTSUBDISTRICT WHERE SUBDISTCODE = :1 AND DISTCODE=:2 AND CITYCODE=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202070", pSubDistCode, pDistCode, pCityCode);

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
        //
        #region [ DB202071 - Харилцагчийн ратинг, зэрэглэл жагсаалт авах ]
        public static Result DB202071(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT RATECODE, NAME, NAME2, NOTE, MINSCORE, MAXSCORE, ORDERNO
FROM CUSTRATE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202071", null);

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
        #region [ DB202072 - Харилцагчийн ратинг, зэрэглэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202072(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT RATECODE, NAME, NAME2, NOTE, MINSCORE, MAXSCORE, ORDERNO
FROM CUSTRATE
WHERE RATECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202072", pNo);

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
        #region [ DB202073 - Харилцагчийн ратинг, зэрэглэл шинээр нэмэх ]
        public static Result DB202073(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTRATE(RATECODE, NAME, NAME2, NOTE, MINSCORE, MAXSCORE, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202073", pParam);
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
        #region [ DB202074 - Харилцагчийн ратинг, зэрэглэл засварлах ]
        public static Result DB202074(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTRATE SET
NAME=:2, NAME2=:3, NOTE=:4, MINSCORE=:5, MAXSCORE=:6, ORDERNO=:7
WHERE RATECODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202074", pParam);
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
        #region [ DB202075 - Харилцагчийн ратинг, зэрэглэл устгах ]
        public static Result DB202075(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTRATE WHERE RATECODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202075", pNo);

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

        #region [ DB202076 - ПОС ын бүртгэл жагсаалт авах ]
        public static Result DB202076(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select POSNo, POSName, POSDesc, POSIP, POSMAC,
POSType, decode(POSType, 'I', 'БАРАА', 'S', 'ҮЙЛЧИЛГЭЭ', 'IS', 'БАРАА БОЛОН ҮЙЛЧИЛГЭЭ') as POSTypeName
from posterminal
order by posname";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202076", null);

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
        #region [ DB202077 - ПОС ын бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202077(DbConnections pDB, string pPOSNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select POSNo, POSName, POSDesc, POSIP, POSMAC,
POSType, decode(POSType, 'I', 'БАРАА', 'S', 'ҮЙЛЧИЛГЭЭ', 'IS', 'БАРАА БОЛОН ҮЙЛЧИЛГЭЭ') as POSTypeName
from posterminal
WHERE POSNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202077", pPOSNo);

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
        #region [ DB202078 - ПОС ын бүртгэл шинээр нэмэх ]
        public static Result DB202078(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO posterminal(POSNo, POSName, POSDesc, POSIP, POSMAC, POSType)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202078", pParam);
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
        #region [ DB202079 - ПОС ын бүртгэл засварлах ]
        public static Result DB202079(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE posterminal SET
POSName=:2, POSDesc=:3, POSIP=:4, POSMAC=:5, POSType=:6
WHERE POSNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202079", pParam);
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
        #region [ DB202080 - ПОС ын бүртгэл устгах ]
        public static Result DB202080(DbConnections pDB, string pPOSNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM posterminal WHERE POSNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202080", pPOSNo);

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

        //
        #region [ DB202096 - Банкны салбарын жагсаалт авах ]
        public static Result DB202096(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BANKID, branchID , NAME, NAME2, ORDERNO
FROM BANKBRANCH
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202096", null);

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
        #region [ DB202097 - Банкны салбарын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202097(DbConnections pDB, long pBankID, long pBranchID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT BANKID, branchID , NAME, NAME2, ORDERNO
FROM BANKBRANCH
WHERE BANKID = :1 AND BRANCHID = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202097", pBankID, pBranchID);

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
        #region [ DB202098 - Банкны салбар шинээр нэмэх ]
        public static Result DB202098(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO BANKBRANCH(BANKID, branchID , NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202098", pParam);
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
        #region [ DB202099 - Банкны салбар засварлах ]
        public static Result DB202099(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BANKBRANCH SET
BRANCHID=:2, NAME=:3, NAME2=:4, ORDERNO=:5
WHERE BANKID = :1 AND BRANCHID = :6";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202099", pParam);
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
        #region [ DB202100 - Банкны салбар устгах ]
        public static Result DB202100(DbConnections pDB, long pBankID, long pBranchID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM BANKBRANCH WHERE BANKID = :1 AND BRANCHID = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202100", pBankID, pBranchID);

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

        #region [ DB202101 - Бараа материал төрөл жагсаалт авах ]
        public static Result DB202101(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INVTYPEID, NAME, NAME2, ORDERNO, CURRENCY, ACCOUNTNO
FROM INVENTORYTYPE
order by ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202101", null);

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
        #region [ DB202102 - Бараа материал төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202102(DbConnections pDB, long pInvTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT INVTYPEID, NAME, NAME2, ORDERNO, CURRENCY, ACCOUNTNO
FROM INVENTORYTYPE
WHERE INVTYPEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202102", pInvTypeID);

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
        #region [ DB202103 - Бараа материал төрөл шинээр нэмэх ]
        public static Result DB202103(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO INVENTORYTYPE(INVTYPEID, NAME, NAME2, ORDERNO, CURRENCY, ACCOUNTNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202103", pParam);
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
        #region [ DB202104 - Бараа материал төрөл засварлах ]
        public static Result DB202104(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE INVENTORYTYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4, CURRENCY=:5, ACCOUNTNO=:6
WHERE INVTYPEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202104", pParam);
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
        #region [ DB202105 - Бараа материал төрөл устгах ]
        public static Result DB202105(DbConnections pDB, long pInvTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select INVTYPEID FROM INVENTORY WHERE INVTYPEID=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202105", pInvTypeID);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110051;
                    res.ResultDesc = "Энэ бараа материалын төрөл дээр бараа материал үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM INVENTORYTYPE WHERE INVTYPEID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202105", pInvTypeID);

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

        #region [ DB202106 - Бараа материал нэгж жагсаалт авах ]
        public static Result DB202106(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT UNITTYPECODE, NAME, NAME2, ORDERNO
FROM PAUNITTYPE
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202106", null);

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
        #region [ DB202107 - Бараа материал нэгж дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202107(DbConnections pDB, long pUnitTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT UNITTYPECODE, NAME, NAME2, ORDERNO
FROM PAUNITTYPE
WHERE UNITTYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202107", pUnitTypeCode);

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
        #region [ DB202108 - Бараа материал нэгж шинээр нэмэх ]
        public static Result DB202108(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PAUNITTYPE(UNITTYPECODE, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202108", pParam);
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
        #region [ DB202109 - Бараа материал нэгж засварлах ]
        public static Result DB202109(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PAUNITTYPE SET
NAME=:2, NAME2=:3, ORDERNO=:4
WHERE UNITTYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202109", pParam);
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
        #region [ DB202110 - Бараа материал нэгж устгах ]
        public static Result DB202110(DbConnections pDB, long pUnitTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select UNITTYPECODE FROM INVENTORY WHERE UNITTYPECODE=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202110", pUnitTypeCode);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110051;
                    res.ResultDesc = "Энэ бараа материалын нэгж дээр бараа материал үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM PAUNITTYPE WHERE UNITTYPECODE = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202110", pUnitTypeCode);

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

        #region [ DB202111 - Байгууллагын дансны бүтээгдэхүүн жагсаалт авах ]
        public static Result DB202111(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] {"ProdCode like","Name like","CurCode","GL like","Type like"};

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

                sql =string.Format(
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, 
TYPE, decode(type, 0, 'БУСАД', 1, 'ОРЛОГО', 2, 'ЗАРЛАГА', 3, 'АВЛАГА', 4, 'ӨГЛӨГ', 5, 'КАСС', 6, 'ХӨРӨНГӨ', 7, 'ӨМЧ') typeName,
BalanceType, decode(BalanceType, 'D', 'DEBIT', 'C', 'CREDIT', 'Z', 'DEBIT AND CREDIT') BalanceTypeName, ORDERNO
FROM BACPRODUCT
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by ORDERNO");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT,"DB202111", dbparam.ToArray());

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
        #region [ DB202112 - Байгууллагын дансны бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202112(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, TYPE, BalanceType, ORDERNO
FROM BACPRODUCT
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202112", pProdCode);

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
        #region [ DB202113 - Байгууллагын дансны бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB202113(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO BACPRODUCT(PRODCODE, NAME, NAME2, CURCODE, GL, TYPE, BalanceType, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202113", pParam);
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
        #region [ DB202114 - Байгууллагын дансны бүтээгдэхүүн засварлах ]
        public static Result DB202114(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BACPRODUCT SET
NAME=:2, NAME2=:3, CURCODE=:4, GL=:5, TYPE=:6, BalanceType=:7, ORDERNO=:8
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202114", pParam);
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
        #region [ DB202115 - Байгууллагын дансны бүтээгдэхүүн устгах ]
        public static Result DB202115(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select PRODCODE FROM BACACCOUNT WHERE PRODCODE=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202115", pProdCode);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110052;
                    res.ResultDesc = "Энэ байгууллагын дансны бүтээгдэхүүн дээр байгууллагын данс үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM BACPRODUCT WHERE PRODCODE = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202115", pProdCode);

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
        #region [ Parameter-1-3 ]
        #region [ DB202116 - Балансын гадуурх дансны бүтээгдэхүүн жагсаалт авах ]
        public static Result DB202116(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] {"ProdCode","Name like","Name2 like","CurCode","GL",
                    "TypeCode","OrderNo"};

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

                sql =string.Format(
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, 
TypeCode, decode(TypeCode, 0, 'ЭНГИЙН', 1, 'БАЛАНСЖУУЛАХ') as TypeCodeName, ORDERNO
FROM CONPRODUCT
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by ORDERNO");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202116", dbparam.ToArray());

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
        #region [ DB202117 - Балансын гадуурх дансны бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202117(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, TypeCode , ORDERNO
FROM CONPRODUCT
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202117", pProdCode);

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
        #region [ DB202118 - Балансын гадуурх дансны бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB202118(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CONPRODUCT(PRODCODE, NAME, NAME2, CURCODE, GL, TypeCode, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202118", pParam);
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
        #region [ DB202119 - Балансын гадуурх дансны бүтээгдэхүүн засварлах ]
        public static Result DB202119(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONPRODUCT SET
NAME=:2, NAME2=:3, CURCODE=:4, GL=:5, TypeCode=:6, ORDERNO=:7
WHERE PRODCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202119", pParam);
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
        #region [ DB202120 - Балансын гадуурх дансны бүтээгдэхүүн устгах ]
        public static Result DB202120(DbConnections pDB, long pProdCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select PRODCODE FROM CONACCOUNT WHERE PRODCODE=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202120", pProdCode);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110053;
                    res.ResultDesc = "Энэ балансын гадуурх дансны бүтээгдэхүүн дээр балансын гадуурх данс үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM CONPRODUCT WHERE PRODCODE = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202120", pProdCode);

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

        #region [ DB202121 - Балансын данс төрөл жагсаалт авах ]
        public static Result DB202121(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT GROUPNO, NAME, NAME2, TYPE, CLOSETYPE, ORDERNO
FROM CHARTGROUP
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202121", null);

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
        #region [ DB202122 - Балансын данс төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202122(DbConnections pDB, long pGroupNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT GROUPNO, NAME, NAME2, TYPE, CLOSETYPE, ORDERNO
FROM CHARTGROUP
WHERE GROUPNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202122", pGroupNo);

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
        #region [ DB202123 - Балансын данс төрөл шинээр нэмэх ]
        public static Result DB202123(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CHARTGROUP(GROUPNO, NAME, NAME2, TYPE, CLOSETYPE, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202123", pParam);
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
        #region [ DB202124 - Балансын данс төрөл засварлах ]
        public static Result DB202124(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CHARTGROUP SET
NAME=:2, NAME2=:3, TYPE=:4, CLOSETYPE=:5, ORDERNO=:6
WHERE GROUPNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202124", pParam);
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
        #region [ DB202125 - Балансын данс төрөл устгах ]
        public static Result DB202125(DbConnections pDB, long pGroupNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select GROUPNO FROM CHART WHERE GROUPNO=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202125", pGroupNo);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110054;
                    res.ResultDesc = "Энэ балансын дансны төрөл дээр баланс үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM CHARTGROUP WHERE GROUPNO = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202125", pGroupNo);

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

        #region [ DB202126 - Харилцагчийн маск жагсаалт авах ]
        public static Result DB202126(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT MASKID, MASKNAME, MASKVALUE, MASKTYPE, CUSTTYPE, ORDERNO
FROM CUSTOMERMASK
Order by ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202126", null);

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
        #region [ DB202127 - Харилцагчийн маск дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202127(DbConnections pDB, int pMaskID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT MASKID, MASKNAME, MASKVALUE, MASKTYPE, CUSTTYPE, ORDERNO
FROM CUSTOMERMASK
WHERE MASKID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202127", pMaskID);

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
        #region [ DB202128 - Харилцагчийн маск шинээр нэмэх ]
        public static Result DB202128(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CUSTOMERMASK(MASKID, MASKNAME, MASKVALUE, MASKTYPE, CUSTTYPE, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202128", pParam);
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
        #region [ DB202129 - Харилцагчийн маск засварлах ]
        public static Result DB202129(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERMASK SET
MASKNAME=:2, MASKVALUE=:3, MASKTYPE=:4, CUSTTYPE=:5, ORDERNO=:6
WHERE MASKID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202129", pParam);
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
        #region [ DB202130 - Харилцагчийн маск устгах ]
        public static Result DB202130(DbConnections pDB, int pMaskID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERMASK WHERE MASKID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202130", pMaskID);

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
        //
        #region [ DB202131 - Өдрийн төрлийн бүртгэлийн жагсаалт авах ]
        public static Result DB202131(DbConnections pDB, int pagenumber, int pagecount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT DayType, Name, Name2, Description, IsDefault,
OrderNo
FROM PADayType
Order by OrderNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202131", null);

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
        #region [ DB202132 - Өдрийн төрлийн бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202132(DbConnections pDB, string pDayType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT DayType, Name, Name2, Description, IsDefault,
OrderNo
FROM PADayType
WHERE DayType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202132", pDayType);

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
        #region [ DB202133 - Өдрийн төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202133(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PADayType(DayType, Name, Name2, Description, IsDefault,
OrderNo )
VALUES(:1, :2, :3, :4, :5,
:6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202133", pParam);
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
        #region [ DB202134 - Өдрийн төрлийн бүртгэл засварлах ]
        public static Result DB202134(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PADayType SET
Name=:2, Name2=:3, Description=:4, IsDefault=:5,
OrderNo=:6
WHERE DayType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202134", pParam);
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
        #region [ DB202135 - Өдрийн төрлийн бүртгэл устгах ]
        public static Result DB202135(DbConnections pDB, string pDayType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PADayType WHERE DayType=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202135", pDayType);

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
        //
        #region [ DB202136 - Үндсэн хөрөнгийн материал төрөл жагсаалт авах ]
        public static Result DB202136(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT FATYPEID, NAME, NAME2, CURRENCY, ACCOUNTNO, DEFACCOUNTNO, DEFEXPACCOUNTNO,LOSSACCOUNTNO, WEARYEAR, DEPFORMULA, 
ORDERNO, Days, HalfMonthCalc,PROFITACCOUNTNO
FROM FATYPE
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202136", null);

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
        #region [ DB202137 - Үндсэн хөрөнгийн материал төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202137(DbConnections pDB, long pFaTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT FATYPEID, NAME, NAME2, CURRENCY, ACCOUNTNO, DEFACCOUNTNO, DEFEXPACCOUNTNO, LOSSACCOUNTNO, WEARYEAR, DEPFORMULA, 
ORDERNO, Days, HalfMonthCalc,PROFITACCOUNTNO
FROM FATYPE
WHERE FATYPEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202137", pFaTypeID);
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
        #region [ DB202138 - Үндсэн хөрөнгийн материал төрөл шинээр нэмэх ]
        public static Result DB202138(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO FATYPE(FATYPEID, NAME, NAME2, CURRENCY, ACCOUNTNO, DEFACCOUNTNO, DEFEXPACCOUNTNO, LOSSACCOUNTNO, WEARYEAR, DEPFORMULA, 
ORDERNO, Days, HalfMonthCalc,PROFITACCOUNTNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13,:14)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202138", pParam);
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
        #region [ DB202139 - Үндсэн хөрөнгийн материал төрөл засварлах ]
        public static Result DB202139(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE FATYPE SET
NAME=:2, NAME2=:3, CURRENCY=:4, ACCOUNTNO=:5, DEFACCOUNTNO=:6, DEFEXPACCOUNTNO=:7, LOSSACCOUNTNO=:8, WEARYEAR=:9, DEPFORMULA=:10, 
ORDERNO=:11, Days=:12, HalfMonthCalc=:13,PROFITACCOUNTNO=:14
WHERE FATYPEID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202139", pParam);
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
        #region [ DB202140 - Үндсэн хөрөнгийн материал төрөл устгах ]
        public static Result DB202140(DbConnections pDB, long pFaTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select FATYPEID FROM FAREG WHERE FATYPEID=:1 ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202140", pFaTypeID);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110056;
                    res.ResultDesc = "Энэ үндсэн хөрөнгийн материалын төрөл дээр үндсэн хөрөнгө үүссэн байгаа тул устгах боломжгүй";
                    return res;
                }

                sql =
@"DELETE FROM FATYPE WHERE FATYPEID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202140", pFaTypeID);

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
        //
        #region [ DB202141 - Цаг агаарын төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202141(DbConnections pDB, int pagenumber, int pagecount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT WeatherId, name, name2, Description, Icon, orderno
FROM PAWeather
order by Orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202141", null);

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
        #region [ DB202142 - Цаг агаарын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202142(DbConnections pDB, string pWeatherId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT WeatherId, name, name2, Description, Icon, orderno
FROM PAWeather
WHERE WeatherId = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202142", pWeatherId);

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
        #region [ DB202143 - Цаг агаарын төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202143(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PAWeather(WeatherId, name, name2, Description, Icon, orderno)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202143", pParam);
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
        #region [ DB202144 - Цаг агаарын төрлийн бүртгэл засварлах ]
        public static Result DB202144(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PAWeather SET
name=:2, name2=:3, Description=:4, Icon=:5, orderno=:6
WHERE WeatherId=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202144", pParam);
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
        #region [ DB202145 - Цаг агаарын төрлийн бүртгэл устгах ]
        public static Result DB202145(DbConnections pDB, string pWeatherId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PAWeather WHERE WeatherId = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202145", pWeatherId);

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
        //
        #region [ DB202146 - GeneralParam Ерөнхий параметрын жагсаалт авах ]
        public static Result DB202146(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, TYPECODE, NAME, ITEMVALUE, DESCRIPTION, ORDERNO, ITEMTYPE, ITEMLEN, mask
FROM GENERALPARAM
order by OrderNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202146", null);
                
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
        #region [ DB202147 - GeneralParam Ерөнхий параметрын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202147(DbConnections pDB, long pKey)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, TYPECODE, NAME, ITEMVALUE, DESCRIPTION, ORDERNO, ITEMTYPE, ITEMLEN, mask
FROM GENERALPARAM
WHERE TYPECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202147", pKey);

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
        #region [ DB202148 - GeneralParam Ерөнхий параметр шинээр нэмэх ]
        public static Result DB202148(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO GENERALPARAM(KEY, TYPECODE, NAME, ITEMVALUE, DESCRIPTION, ORDERNO, ITEMTYPE, ITEMLEN, mask)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202148", pParam);
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
        #region [ DB202149 - GeneralParam Ерөнхий параметр засварлах ]
        public static Result DB202149(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE GENERALPARAM SET
TYPECODE=:2, NAME=:3, ITEMVALUE=:4, DESCRIPTION=:5, ORDERNO=:6, ITEMTYPE=:7, ITEMLEN=:8, mask=:9
WHERE KEY=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202149", pParam);
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
        #region [ DB202150 - GeneralParam Ерөнхий параметр устгах ]
        public static Result DB202150(DbConnections pDB, long pKey)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM GENERALPARAM WHERE KEY = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202150", pKey);

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
        //
        #region [ DB202151 - Брэндийн бүртгэл жагсаалт авах ]
        public static Result DB202151(DbConnections pDB, int pagenumber, int pagecount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT Brandid, Name, Name2, OrderNo
FROM PABrand
order by OrderNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202151", null);

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
        #region [ DB202152 - Брэндийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202152(DbConnections pDB, string pBrandid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT Brandid, Name, Name2, OrderNo
FROM PABrand
WHERE Brandid = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202152", pBrandid);

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
        #region [ DB202153 - Брэндийн бүртгэл шинээр нэмэх ]
        public static Result DB202153(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PABrand(Brandid, Name, Name2, OrderNo)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202153", pParam);
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
        #region [ DB202154 - Брэндийн бүртгэл засварлах ]
        public static Result DB202154(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PABrand SET
Name=:2, Name2=:3, OrderNo=:4
WHERE Brandid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202154", pParam);
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
        #region [ DB202155 - Брэндийн бүртгэл устгах ]
        public static Result DB202155(DbConnections pDB, string pBrandid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PABrand WHERE Brandid = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202155", pBrandid);

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
        //
        #region [ DB202156 - Бараа материалын төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202156(DbConnections pDB, int pagenumber, int pagecount)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT InvType, Name, Name2, catcode, Note, OrderNo
FROM PAInvType
order by OrderNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202156", null);

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
        #region [ DB202157 - Бараа материалын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202157(DbConnections pDB, string pInvType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT InvType, Name, Name2, catcode, Note, OrderNo
FROM PAInvType
WHERE InvType = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202157", pInvType);

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
        #region [ DB202158 - Бараа материалын төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202158(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PAInvType(InvType, Name, Name2, catcode, Note, OrderNo)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202158", pParam);
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
        #region [ DB202159 - Бараа материалын төрлийн бүртгэл засварлах ]
        public static Result DB202159(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PAInvType SET
Name=:2, Name2=:3, catcode=:4, Note=:5, OrderNo=:6
WHERE InvType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202159", pParam);
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
        #region [ DB202160 - Бараа материалын төрлийн бүртгэл устгах ]
        public static Result DB202160(DbConnections pDB, string pInvType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PAInvType WHERE InvType = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202160", pInvType);

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
        //
        #region [ DB202161 - Гүйлгээний кодын жагсаалт авах ]
        public static Result DB202161(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "TRANCODE like", "NAME like", "NAME2 like" };

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

                            if (fieldnames[i].Substring(fieldnames[i].Length-5, 5).ToLower() == "like")
                                sb.AppendFormat(" {0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" {0}=:{1} ", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT TRANCODE, NAME, NAME2
FROM TXN
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by TRANCODE");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202161", dbparam.ToArray());

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
        #region [ DB202162 - Гүйлгээний кодын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202162(DbConnections pDB, long pTranCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TRANCODE, NAME, NAME2
FROM TXN
WHERE TRANCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202162", pTranCode);

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
        #region [ DB202163 - Гүйлгээний код шинээр нэмэх ]
        public static Result DB202163(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO TXN(TRANCODE, NAME, NAME2)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202163", pParam);
               
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
        #region [ DB202164 - Гүйлгээний код засварлах ]
        public static Result DB202164(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE TXN SET
NAME=:2, NAME2=:3
WHERE TRANCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202164", pParam);
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
        #region [ DB202165 - Гүйлгээний код устгах ]
        public static Result DB202165(DbConnections pDB, long pTranCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM TXN WHERE TRANCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202165", pTranCode);

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
        #region [ Parameter-1-4 ]
        //
        #region [ DB202166 - Гүйлгээний оролтын жагсаалт авах ]
        public static Result DB202166(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ENTRYCODE, ENTRYTXNCODE, DRACNTMOD, DRACNTNO, DRCURRCODE, DRRATE, DRAMOUNT, CRACNTMOD, CRACNTNO, CRCURRCODE, 
CRAMOUNT, CRRATE, DESCRIPTION, condition
FROM ENTRY";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202166", null);

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
        #region [ DB202167 - Гүйлгээний оролтын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202167(DbConnections pDB, long pEntry)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ENTRYCODE, ENTRYTXNCODE, DRACNTMOD, DRACNTNO, DRCURRCODE, DRRATE, DRAMOUNT, CRACNTMOD, CRACNTNO, CRCURRCODE, 
CRAMOUNT, CRRATE, DESCRIPTION, condition
WHERE ENTRYCODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202167", pEntry);

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
        #region [ DB202168 - Гүйлгээний оролт шинээр нэмэх ]
        public static Result DB202168(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO ENTRY(ENTRYCODE, ENTRYTXNCODE, DRACNTMOD, DRACNTNO, DRCURRCODE, DRRATE, DRAMOUNT, CRACNTMOD, CRACNTNO, CRCURRCODE, 
CRAMOUNT, CRRATE, DESCRIPTION, condition)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202168", pParam);
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
        #region [ DB202169 - Гүйлгээний оролт засварлах ]
        public static Result DB202169(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ENTRY SET
ENTRYTXNCODE=:2, DRACNTMOD=:3, DRACNTNO=:4, DRCURRCODE=:5, DRRATE=:6, DRAMOUNT=:7, CRACNTMOD=:8, CRACNTNO=:9, CRCURRCODE=:10, 
CRAMOUNT=:11, CRRATE=:12, DESCRIPTION=:13, condition=:14
WHERE ENTRYCODE=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202169", pParam);
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
        #region [ DB202170 - Гүйлгээний оролт устгах ]
        public static Result DB202170(DbConnections pDB, long pEntry)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ENTRY WHERE ENTRYCODE=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202170", pEntry);

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
        //
        #region [ DB202171 - Тагийн төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202171(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TagType, Name, name2, Offset, Length, Format, orderno
FROM PATagSetup
order by Name";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202171", null);

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
        #region [ DB202172 - Тагийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202172(DbConnections pDB, string pTagType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TagType, Name, name2, Offset, Length, Format, orderno
FROM PATagSetup
WHERE TagType = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202172", pTagType);

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
        #region [ DB202173 - Тагийн төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202173(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PATagSetup(TagType, Name, name2, Offset, Length, Format, orderno)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202173", pParam);
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
        #region [ DB202174 - Тагийн төрлийн бүртгэл засварлах ]
        public static Result DB202174(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PATagSetup SET
Name=:2, Name2=:3, Offset=:4, Length=:5, Format=:6, OrderNo=:7
WHERE TagType = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202174", pParam);
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
        #region [ DB202175 - Тагийн төрлийн бүртгэл устгах ]
        public static Result DB202175(DbConnections pDB, string pTagType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PATagSetup WHERE TagType = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202175", pTagType);

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
        //
        #region [ DB202176 - Гүйлгээний оролтуудын холбоосын жагсаалт авах ]
        public static Result DB202176(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TRANCODE, ENTRYCODE, ORDERNO
FROM TXNENTRY
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202176", null);

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
        #region [ DB202177 - Гүйлгээний оролтуудын холбоосын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202177(DbConnections pDB, long pTranCode, long pEntryCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TRANCODE, ENTRYCODE, ORDERNO
FROM TXNENTRY
WHERE TRANCODE = :1 and ENTRYCODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202177", pTranCode, pEntryCode);

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
        #region [ DB202178 - Гүйлгээний оролтууд холбоос шинээр нэмэх ]
        public static Result DB202178(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO TXNENTRY(TRANCODE, ENTRYCODE, ORDERNO)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202178", pParam);
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
        #region [ DB202179 - Гүйлгээний оролтууд холбоос засварлах ]
        public static Result DB202179(DbConnections pDB, int pOldTranCode, int pOldEntryCode, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                obj[0] = pOldTranCode;
                obj[1] = pOldEntryCode;
                obj[2] = pNewParam[0];
                obj[3] = pNewParam[1];
                obj[4] = pNewParam[2];

                string sql =
@"UPDATE TXNENTRY SET
TRANCODE = :3, ENTRYCODE=:4, ORDERNO=:5
WHERE TRANCODE = :1 and ENTRYCODE=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202179", obj);
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
        #region [ DB202180 - Гүйлгээний оролтууд холбоос устгах ]
        public static Result DB202180(DbConnections pDB, long pTranCode, long pEntryCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM TXNENTRY WHERE TRANCODE = :1 and ENTRYCODE=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202180", pTranCode, pEntryCode);

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

        //
        #region [ DB202181 - Хуваарилалтын төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202181(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ScheduleType, Name, Name2, Unit, Method, Duration
FROM PAScheduleType
order by Name";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202181", null);

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
        #region [ DB202182 - Хуваарилалтын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202182(DbConnections pDB, string pScheduleType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ScheduleType, Name, Name2, Unit, Method, Duration
FROM PAScheduleType
WHERE ScheduleType = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202182", pScheduleType);

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
        #region [ DB202183 - Хуваарилалтын төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202183(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PAScheduleType(ScheduleType, Name, Name2, Unit, Method, Duration)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202183", pParam);
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
        #region [ DB202184 - Хуваарилалтын төрлийн бүртгэл засварлах ]
        public static Result DB202184(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PAScheduleType SET
Name=:2, Name2=:3, Unit=:4, Method=:5, Duration=:6
WHERE ScheduleType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202184", pParam);
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
        #region [ DB202185 - Хуваарилалтын төрлийн бүртгэл устгах ]
        public static Result DB202185(DbConnections pDB, string pScheduleType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PAScheduleType WHERE ScheduleType=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202185", pScheduleType);

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
        //
        #region [ DB202186 - Үйлчилгээний төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202186(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ServType, Name, Name2, CatCode, Note, OrderNo
FROM PAServType
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202186", null);

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
        #region [ DB202187 - Үйлчилгээний төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202187(DbConnections pDB, string pServType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ServType, Name, Name2, CatCode, Note, OrderNo
FROM PAServType
WHERE ServType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202187", pServType);

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
        #region [ DB202188 - Үйлчилгээний төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202188(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PAServType(ServType, Name, Name2, CatCode, Note, OrderNo)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202188", pParam);
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
        #region [ DB202189 - Үйлчилгээний төрлийн бүртгэл засварлах ]
        public static Result DB202189(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PAServType SET
Name=:2, Name2=:3, CatCode=:4, Note=:5, OrderNo=:6
WHERE ServType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202189", pParam);
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
        #region [ DB202190 - Үйлчилгээний төрлийн бүртгэл устгах ]
        public static Result DB202190(DbConnections pDB, string pServType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PAServType WHERE ServType=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202190", pServType);

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

        #region [ DB202191 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл жагсаалт авах ]
        public static Result DB202191(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT Currency, BankNote, Description, OrderNo
FROM PABankNote
order by Currency";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202191", null);

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
        #region [ DB202192 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202192(DbConnections pDB, string pCurrency)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT Currency, BankNote, Description, OrderNo
FROM PABankNote
WHERE Currency = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202192", pCurrency);

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
        #region [ DB202193 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл шинээр нэмэх ]
        public static Result DB202193(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PABankNote(Currency, BankNote, Description, OrderNo)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202193", pParam);
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
        #region [ DB202194 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл засварлах ]
        public static Result DB202194(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PABankNote SET
BankNote=:2, Description=:3, OrderNo=:4
WHERE Currency=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202194", pParam);
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
        #region [ DB202195 - Мөнгөн тэмдэгтийн дэвсгэртийн бүртгэл устгах ]
        public static Result DB202195(DbConnections pDB, string pCurrency)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PABankNote WHERE Currency=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202195", pCurrency);

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
        //
        #region [ DB202196 - Төлбөрийн төрлийн код бүртгэл жагсаалт авах ]
        public static Result DB202196(DbConnections pDB, string pTypeId, int pPaymentFlag)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TypeId, Name, PaymentFlag
FROM PAPayType
where TypeId<>:1 and PaymentFlag=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202196", pTypeId, pPaymentFlag);

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

        #region [ DB202199 - Ханшийн түүх нэмэх ]
        public static Result DB202199(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"MERGE INTO CurrencyHist b
USING (
SELECT :1 currency, :2 curdate, :3 Rate, :4 CashBuyRate, :5 CashSellRate, :6 NonCashBuyRate, :7 NonCashSellRate, :8 OLDRATE
FROM dual
) e
ON (b.Currency = e.Currency and b.curdate = e.curdate)
WHEN MATCHED THEN
UPDATE SET b.Rate = e.Rate, b.CashBuyRate=e.CashBuyRate , b.CashSellRate=e.CashSellRate , b.NonCashBuyRate=e.NonCashBuyRate , b.NonCashSellRate=e.NonCashSellRate , b.OLDRATE=e.OLDRATE
WHEN NOT MATCHED THEN
insert (Currency, curdate, Rate, CashBuyRate, CashSellRate, NonCashBuyRate, NonCashSellRate, OLDRATE)
values (e.Currency, e.curdate, e.Rate, e.CashBuyRate, e.CashSellRate, e.NonCashBuyRate, e.NonCashSellRate, e.OLDRATE)
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202199", pParam);

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
        #region [ DB202200 - Ханшийн түүх харах ]
        public static Result DB202200(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "CURRENCY", "CURDATE" };

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
                            sb.AppendFormat(" {0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT CURRENCY, CURDATE, RATE, CASHBUYRATE, CASHSELLRATE, NONCASHBUYRATE, NONCASHSELLRATE, OLDRATE
FROM CURRENCYHIST 
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " ORDER BY CURDATE, CURRENCY");

                res = pDB.ExecuteQuery("core", sql, "DB202200", pageindex, pagerows, dbparam.ToArray());

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
        //
        #region [ DB202201 - Төлбөрийн төрлийн код бүртгэл жагсаалт авах ]
        public static Result DB202201(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TypeId, Name, Name2, SuspAccount, OrderNo, PaymentFlag, ContractType, ContractCheck
FROM PAPayType
order by Name";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202201", null);

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
        #region [ DB202202 - Төлбөрийн төрлийн код бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202202(DbConnections pDB, string pTypeId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TypeId, Name, Name2, SuspAccount, OrderNo, PaymentFlag, ContractType, ContractCheck
FROM PAPayType
WHERE TypeId = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202202", pTypeId);

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
        #region [ DB202203 - Төлбөрийн төрлийн код бүртгэл шинээр нэмэх ]
        public static Result DB202203(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PAPayType(TypeId, Name, Name2, SuspAccount, OrderNo, PaymentFlag, ContractType, ContractCheck)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202203", pParam);
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
        #region [ DB202204 - Төлбөрийн төрлийн код бүртгэл засварлах ]
        public static Result DB202204(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PAPayType SET
Name=:2, Name2=:3, SuspAccount=:4, OrderNo=:5, PaymentFlag=:6, ContractType=:7, ContractCheck=:8
WHERE TypeId=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202204", pParam);
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
        #region [ DB202205 - Төлбөрийн төрлийн код бүртгэл устгах ]
        public static Result DB202205(DbConnections pDB, string pTypeId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PAPayType WHERE TypeId=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202205", pTypeId);

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

        #region [ DB202206 - Календарын бүртгэл жагсаалт авах ]
        public static Result DB202206(DbConnections pDB, DateTime pStartDay, DateTime pEndDay)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT a.Day, a.DayType, b.Description,
a.DayTemperature, a.DayWeatherID, c.name as DayWeatherDesc, c.icon as DayWeathericon,
a.NightTemperature, a.NightWeatherID, cc.name as NightWeatherDesc, cc.icon as NightWeathericon
FROM PACalendar a
left join PADayType b on a.DayType=b.DayType
left join PAWeather c on a.DayWeatherID=c.WeatherId
left join PAWeather cc on a.NightWeatherID=cc.WeatherId
where a.day between :1 and :2
order by a.Day";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202206", pStartDay, pEndDay);

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
        #region [ DB202207 - Календарын бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202207(DbConnections pDB, DateTime pDay)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT a.Day, a.DayType, b.Description,
a.DayTemperature, a.DayWeatherID, c.name as DayWeatherDesc, c.icon as DayWeathericon,
a.NightTemperature, a.NightWeatherID, cc.name as NightWeatherDesc, cc.icon as NightWeathericon
FROM PACalendar a
left join PADayType b on a.DayType=b.DayType
left join PAWeather c on a.DayWeatherID=c.WeatherId
left join PAWeather cc on a.NightWeatherID=cc.WeatherId
where a.day between :1 and :2
WHERE a.Day = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202207", pDay);

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
        #region [ DB202208 - Календарын бүртгэл шинээр нэмэх ]
        public static Result DB202208(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PACalendar(Day, DayType, DayTemperature, DayWeatherID, NightTemperature, NightWeatherID)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202208", pParam);
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
        #region [ DB202209 - Календарын бүртгэл засварлах ]
        public static Result DB202209(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PACalendar SET
DayType=:2, DayTemperature=:3, DayWeatherID=:4, NightTemperature=:5, NightWeatherID=:6
WHERE Day=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202209", pParam);
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
        #region [ DB202210 - Календарын бүртгэл устгах ]
        public static Result DB202210(DbConnections pDB, DateTime pDay)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PACalendar WHERE Day=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202210", pDay);

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

        #region [ DB202211 - Бараа материалын үндсэн бүртгэл жагсаалт авах ]
        public static Result DB202211(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] {"InvId like","InvType","Name like","Name2 like","BrandId","PriceAmount",
"PriceRefund","Count","CatCode","BarCode","Unit",
"UnitSize","PrinterType","CreateDate","Note like","Status",
"SalesAccountNo like","RefundAccountNo like","DiscountAccountNo like","BonusAccountNo like","BONUSEXPACCOUNTNO like"};

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
@"select *
from V_InvMainList
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by InvId Desc");

                res = pDB.ExecuteQuery("core", sql, "DB202211", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202212 - Бараа материалын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202212(DbConnections pDB, string pInvId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select *
from V_InvMainList
WHERE InvId=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202212", pInvId);

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
        #region [ DB202213 - Бараа материалын үндсэн бүртгэл шинээр нэмэх ]
        public static Result DB202213(DbConnections pDB, object[] pParam, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql = "";

                //#region [//Merge ProdPriceHist]
                //object[] objPrice = new object[5];

                //objPrice[0] = IPos.Core.SystemProp.TxnDate;
                //objPrice[1] = 0;
                //objPrice[2] = pParam[0];
                //objPrice[3] = pParam[5];
                //objPrice[4] = pUserNo;

                //res = DB202288(pDB, objPrice);
                //if(res.ResultNo != 0)
                //    return res;
                //#endregion

                if (pParam[23] == null)
                {
                    object[] obj = new object[23];

                    for (int i = 0; i < 23; i++ )
                        obj[i] = pParam[i];

                    sql =
@"INSERT INTO InvMain(InvID, TypeCode, Name, Name2, BrandID, 
BarCode, Unit, UnitSize, Status, Price, 
Count, CreateDate, SalesStartDate, SalesEndDate, Note, 
SalesAccountNo, RefundAccountNo, DiscountAccountNo, BonusAccountNo, BonusExpAccountNo, 
RentFlag, PriceRefund, Prepared)
VALUES(:1, :2, :3, :4, :5,
:6,  :7,  :8,  :9,  :10,
:11, :12, :13, :14, :15,
:16, :17, :18, :19, :20,
:21, :22, :23)";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202213", obj);
                }
                else
                {
                    sql =
@"INSERT INTO InvMain(InvID, TypeCode, Name, Name2, BrandID, 
BarCode, Unit, UnitSize, Status, Price, 
Count, CreateDate, SalesStartDate, SalesEndDate, Note, 
SalesAccountNo, RefundAccountNo, DiscountAccountNo, BonusAccountNo, BonusExpAccountNo, 
RentFlag, PriceRefund, Prepared, Picture)
VALUES(:1, :2, :3, :4, :5,
:6,  :7,  :8,  :9,  :10,
:11, :12, :13, :14, :15,
:16, :17, :18, :19, :20,
:21, :22, :23, :24)";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202213", pParam);
                }

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
        #region [ DB202214 - Бараа материалын үндсэн бүртгэл засварлах ]
        public static Result DB202214(DbConnections pDB, object[] pParam, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql = "";

                //#region [//Merge ProdPriceHist]
                //object[] objPrice = new object[5];

                //objPrice[0] = IPos.Core.SystemProp.TxnDate;
                //objPrice[1] = 0;
                //objPrice[2] = pParam[0];
                //objPrice[3] = pParam[5];
                //objPrice[4] = pUserNo;

                //res = DB202288(pDB, objPrice);
                //if (res.ResultNo != 0)
                //    return res;
                //#endregion

                if (pParam[23] == null)
                {
                    object[] obj = new object[23];

                    for (int i = 0; i < 23; i++)
                        obj[i] = pParam[i];

                    sql =
@"UPDATE InvMain SET
TypeCode=:2, Name=:3, Name2=:4, BrandID=:5, 
BarCode=:6, Unit=:7, UnitSize=:8, Status=:9, Price=:10, 
Count=:11, CreateDate=:12, SalesStartDate=:13, SalesEndDate=:14, Note=:15, 
SalesAccountNo=:16, RefundAccountNo=:17, DiscountAccountNo=:18, BonusAccountNo=:19, BonusExpAccountNo=:20, 
RentFlag=:21, PriceRefund=:22, Prepared=:23
WHERE InvId=:1";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202214", obj);
                }
                else {
                    sql =
@"UPDATE InvMain SET
TypeCode=:2, Name=:3, Name2=:4, BrandID=:5, 
BarCode=:6, Unit=:7, UnitSize=:8, Status=:9, Price=:10, 
Count=:11, CreateDate=:12, SalesStartDate=:13, SalesEndDate=:14, Note=:15, 
SalesAccountNo=:16, RefundAccountNo=:17, DiscountAccountNo=:18, BonusAccountNo=:19, BonusExpAccountNo=:20, 
RentFlag=:21, PriceRefund=:22, Prepared=:23, Picture=:24
WHERE InvId=:1";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202214", pParam);
                }

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
        #region [ DB202215 - Бараа материалын үндсэн бүртгэл устгах ]
        public static Result DB202215(DbConnections pDB, string pInvId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM InvMain WHERE InvId=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202215", pInvId);

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

        #region [ DB202216 - Барааны ангиллын төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202216(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select catcode, name, name2, orderno from PaInvCat order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202216", null);

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
        #region [ DB202217 - Ангиллын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202217(DbConnections pDB, string pCatCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select catcode, name, name2, orderno
FROM PaInvCat
WHERE catcode=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202217", pCatCode);

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
        #region [ DB202218 - Ангиллын төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202218(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PaInvCat(catcode, name, name2, orderno)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202218", pParam);
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
        #region [ DB202219 - Ангиллын төрлийн бүртгэл засварлах ]
        public static Result DB202219(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PaInvCat SET
name=:2, name2=:3, orderno=:4
WHERE catcode=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202219", pParam);
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
        #region [ DB202220 - Ангиллын төрлийн бүртгэл устгах ]
        public static Result DB202220(DbConnections pDB, string pCatCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PaInvCat WHERE catcode=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202220", pCatCode);

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

        #region [ DB202216_1 - Үйлчилгээний төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202216_1(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select catcode, name, name2, orderno from PaServCat order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202216_1", null);

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
        #region [ DB202217_1 - Үйлчилгээний Ангиллын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202217_1(DbConnections pDB, string pCatCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select catcode, name, name2, orderno
FROM PaServCat
WHERE catcode=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202217_1", pCatCode);

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
        #region [ DB202218_1 - Үйлчилгээний Ангиллын төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202218_1(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PaServCat(catcode, name, name2, orderno)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202218_1", pParam);
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
        #region [ DB202219_1 - Үйлчилгээний Ангиллын төрлийн бүртгэл засварлах ]
        public static Result DB202219_1(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PaServCat SET
name=:2, name2=:3, orderno=:4
WHERE catcode=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202219_1", pParam);
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
        #region [ DB202220_1 - Үйлчилгээний Ангиллын төрлийн бүртгэл устгах ]
        public static Result DB202220_1(DbConnections pDB, string pCatCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PaServCat WHERE catcode=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202220_1", pCatCode);

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

        //
        #region [ DB202222 - Дамжлагын бүлгийн холбоотой дамжлагуудыг авах ]
        public static Result DB202222(DbConnections pDB, int pStepID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT decode(b.stepid, '', 0, 1) status, a.stepitemid, a.name, a.name2, a.note, a.note2, A.WORKDAYS, b.orderno
FROM stepitem a
left join (select * from steplink where stepid=:1) b on A.STEPITEMID=B.STEPITEMID
order by b.orderno, A.STEPITEMID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202222", pStepID);

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
        #region [ DB202223 - Дамжлагын бүлгийн холбоотой дамжлагуудыг хадгалах ]
        public static Result DB202223(DbConnections pDB, int pStepID, DataTable pDT)
        {
            Result res = new Result();
            try
            {
                string sql = "";

                //Delete StepLink
                sql =
@"delete from StepLink where StepID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202223", pStepID);
                if (res.ResultNo != 0)
                    return res;
                //Insert StepLink
                foreach (DataRow dr in pDT.Rows)
                {
                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
@"insert into StepLink(StepID, StepItemID, OrderNo)
values(:1, :2, :3)";

                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202223", new object[] { pStepID, dr["StepItemID"], dr["OrderNo"] });
                        res = F_Error(res);
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

        #region [ DB202226 - Үйлчилгээний үндсэн бүртгэл бүртгэл жагсаалт авах ]
        public static Result DB202226(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] {"ServID like","ServType","Name like","Name2 like","ServStartDate",
"ServEndDate","PriceAmount","Count","CatCode","Unit",
"UnitSize","PrinterType","CreateDate","Note like","Status", 
"TagType", "TagTime", "TagTimeMethod", "IsSchedule","ScheduleType", 
"SalesAccountNo like","RefundAccountNo like","DiscountAccountNo like","BonusAccountNo like","BONUSEXPACCOUNTNO like"};

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
@"select *
from V_servmainlist
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by ServID Desc");

                res = pDB.ExecuteQuery("core", sql, "DB202226", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202227 - Үйлчилгээний үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202227(DbConnections pDB, string pServID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select *
from V_servmainlist
WHERE ServID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202227", pServID);

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
        #region [ DB202228 - Үйлчилгээний үндсэн бүртгэл шинээр нэмэх ]
        public static Result DB202228(DbConnections pDB, object[] pParam, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql = "";

                #region [//Merge ProdPriceHist]
                //object[] objPrice = new object[5];

                //objPrice[0] = IPos.Core.SystemProp.TxnDate;
                //objPrice[1] = 1;
                //objPrice[2] = pParam[0];
                //objPrice[3] = pParam[6];
                //objPrice[4] = pUserNo;

                //res = DB202288(pDB, objPrice);
                //if (res.ResultNo != 0)
                //    return res;
                #endregion

                if (pParam[26] == null)
                {
                    object[] obj = new object[26];

                    for (int i = 0; i < 26; i++)
                        obj[i] = pParam[i];

                    sql =
@"INSERT INTO ServMain(ServID, TypeCode, Name, Name2, BrandID, 
BarCode, Unit, UnitSize, Status, Price, 
Count, CreateDate, SalesStartDate, SalesEndDate, Note, 
SalesAccountNo, RefundAccountNo, DiscountAccountNo, BonusAccountNo, BonusExpAccountNo, 
isTimeTable, TimeTableID, ServiceTime, TagType, TagTime, 
TagTimeMethod)
VALUES(:1, :2, :3, :4, :5,
:6,  :7,  :8,  :9,  :10,
:11, :12, :13, :14, :15,
:16, :17, :18, :19, :20,
:21, :22, :23, :24, :25,
:26)";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202228", obj);
                }
                else
                {
                    sql =
    @"INSERT INTO ServMain(ServID, TypeCode, Name, Name2, BrandID, 
BarCode, Unit, UnitSize, Status, Price, 
Count, CreateDate, SalesStartDate, SalesEndDate, Note, 
SalesAccountNo, RefundAccountNo, DiscountAccountNo, BonusAccountNo, BonusExpAccountNo, 
isTimeTable, TimeTableID, ServiceTime, TagType, TagTime, 
TagTimeMethod, picture)
VALUES(:1, :2, :3, :4, :5,
:6,  :7,  :8,  :9,  :10,
:11, :12, :13, :14, :15,
:16, :17, :18, :19, :20,
:21, :22, :23, :24, :25,
:26, :27)";
                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202228", pParam);
                }

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
        #region [ DB202229 - Үйлчилгээний үндсэн бүртгэл засварлах ]
        public static Result DB202229(DbConnections pDB, object[] pParam, int pUserNo)
        {
            Result res = new Result();
            try
            {

                string sql = "";

                #region [//Merge ProdPriceHist]
                //object[] objPrice = new object[5];

                //objPrice[0] = IPos.Core.SystemProp.TxnDate;
                //objPrice[1] = 1;
                //objPrice[2] = pParam[0];
                //objPrice[3] = pParam[6];
                //objPrice[4] = pUserNo;

                //res = DB202288(pDB, objPrice);
                //if (res.ResultNo != 0)
                //    return res;
                #endregion

                if (pParam[26] == null)
                {
                    object[] obj = new object[26];

                    for (int i = 0; i < 26; i++)
                        obj[i] = pParam[i];

                    sql =
@"UPDATE ServMain SET
TypeCode=:2, Name=:3, Name2=:4, BrandID=:5, 
BarCode=:6, Unit=:7, UnitSize=:8, Status=:9, Price=:10, 
Count=:11, CreateDate=:12, SalesStartDate=:13, SalesEndDate=:14, Note=:15, 
SalesAccountNo=:16, RefundAccountNo=:17, DiscountAccountNo=:18, BonusAccountNo=:19, BonusExpAccountNo=:20, 
isTimeTable=:21, TimeTableID=:22, ServiceTime=:23, TagType=:24, TagTime=:25, 
TagTimeMethod=:26
WHERE ServID=:1";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202229", obj);
                }
                else
                {

                    sql =
@"UPDATE ServMain SET
TypeCode=:2, Name=:3, Name2=:4, BrandID=:5, 
BarCode=:6, Unit=:7, UnitSize=:8, Status=:9, Price=:10, 
Count=:11, CreateDate=:12, SalesStartDate=:13, SalesEndDate=:14, Note=:15, 
SalesAccountNo=:16, RefundAccountNo=:17, DiscountAccountNo=:18, BonusAccountNo=:19, BonusExpAccountNo=:20, 
isTimeTable=:21, TimeTableID=:22, ServiceTime=:23, TagType=:24, TagTime=:25, 
TagTimeMethod=:26, picture=:27
WHERE ServID=:1";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202229", pParam);
                }
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
        #region [ DB202230 - Үйлчилгээний үндсэн бүртгэл устгах ]
        public static Result DB202230(DbConnections pDB, string pServID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ServMain WHERE ServID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202230", pServID);

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

        #region [ DB202231 - Гэрээний төрлийн бүртгэл жагсаалт авах ]
        public static Result DB202231(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select contracttype, name, name2, orderno, METHOD
from pacontracttype
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202231", null);

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
        #region [ DB202232 - Гэрээний төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202232(DbConnections pDB, string pContractType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ContractType, name, name2, orderno, METHOD
from pacontracttype
WHERE ContractType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202232", pContractType);

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
        #region [ DB202233 - Гэрээний төрлийн бүртгэл шинээр нэмэх ]
        public static Result DB202233(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO pacontracttype(ContractType, name, name2, orderno, METHOD)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202233", pParam);
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
        #region [ DB202234 - Гэрээний төрлийн бүртгэл засварлах ]
        public static Result DB202234(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE pacontracttype SET
name=:2, name2=:3, orderno=:4, METHOD=:5
WHERE ContractType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202234", pParam);
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
        #region [ DB202235 - Гэрээний төрлийн бүртгэл устгах ]
        public static Result DB202235(DbConnections pDB, string pContractType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM pacontracttype WHERE ContractType=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202235", pContractType);

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

        #region [ DB202236 - Үйлчилгээнд агуулагдах бараа материал жагсаалт авах ]
        public static Result DB202236(DbConnections pDB, string pServID)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select a.servid, a.invid, im.name as invname
from ServInventory a
left join invmain im on a.invid=im.invid
where a.servid=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202236", pServID);

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
        #region [ DB202237 - Үйлчилгээнд агуулагдах бараа материал дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202237(DbConnections pDB, string pServID, string pInvID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.servid, a.invid, im.name as invname
from ServInventory a
left join invmain im on a.invid=im.invid
where a.servid=:1 and a.InvID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202237", pServID, pInvID);

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
        #region [ DB202238 - Үйлчилгээнд агуулагдах бараа материал шинээр нэмэх ]
        public static Result DB202238(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO ServInventory(servid, invid)
VALUES(:1, :2)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202238", pParam);
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
        #region [ DB202239 - Үйлчилгээнд агуулагдах бараа материал засварлах ]
        public static Result DB202239(DbConnections pDB, string pServID, string pOldInvID, string pNewInvID)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE ServInventory SET
InvID=:3
WHERE servid=:1 and InvID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202239", pServID, pOldInvID, pNewInvID);
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
        #region [ DB202240 - Үйлчилгээнд агуулагдах бараа материал устгах ]
        public static Result DB202240(DbConnections pDB, string pServID, string pInvID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ServInventory WHERE servid=:1 and InvID=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202240", pServID, pInvID);

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
        #region [ Parameter-1-6 ]
        #region [ DB202241 - Үндсэн хөрөнгийн байршлын жагсаалт мэдээлэл авах ]
        public static Result DB202241(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select typecode, position, orderno
from FaPosition
order by orderno ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202241", null);

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
        #region [ DB202242 - Үндсэн хөрөнгийн байршлын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202242(DbConnections pDB, int pTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"Select typecode, position, orderno
from FaPosition
WHERE typecode = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202242", pTypeCode);

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
        #region [ DB202243 - Үндсэн хөрөнгийн байршил нэмэх ]
        public static Result DB202243(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO FaPosition(typecode, position, orderno)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202243", pParam);
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
        #region [ DB202244 - Үндсэн хөрөнгийн байршил засварлах ]
        public static Result DB202244(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE FaPosition SET
position=:2, orderno=:3
WHERE typecode = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202244", pParam);
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
        #region [ DB202245 - Үндсэн хөрөнгийн байршил устгах ]
        public static Result DB202245(DbConnections pDB, int pTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM FaPosition WHERE typecode = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202245", pTypeCode);

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

        #region [ DB202246 - Бүтээгдэхүүний багцын бүртгэл жагсаалт мэдээлэл авах ]
        public static Result DB202246(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                string[] fieldnames = new string[] { 
                    "a.PackageId like","a.Name like","a.Name2 like","a.BrandID","a.BarCode like",
                    "a.UnitSize like","a.Status","a.Price like","a.Count like","a.CreateDate",
                    "a.SalesStartDate","a.SalesEndDate","a.Note like","a.SalesAccountNo like","a.RefundAccountNo like",
                    "a.DiscountAccountNo like","a.BonusAccountNo like","a.BonusExpAccountNo like"
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
@"select a.PackageID, a.Name, a.Name2, a.BrandID, pb.name as brandidname, a.BarCode, 
a.Unit, a.name as unitname, a.UnitSize, a.Status, decode(a.status, 0, 'Идэвхгүй', 1, 'Идэвхтэй') as Statusname, a.Price, a.Count, 
a.CreateDate, a.SalesStartDate, a.SalesEndDate, a.Note, a.SalesAccountNo, 
a.RefundAccountNo, a.DiscountAccountNo, a.BonusAccountNo, a.BonusExpAccountNo
from packagemain a
left join pabrand pb on a.brandid=pb.brandid 
left join paunittype pu on a.Unit=pu.UNITTYPECODE
{0} {1} ", sb.Length > 0 ? "where" : "", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB202246", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202247 - Бүтээгдэхүүний багцын бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202247(DbConnections pDB, string pPackId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select PackageID, Name, Name2, BrandID, BarCode, 
Unit, UnitSize, Status, Price, Count, 
CreateDate, SalesStartDate, SalesEndDate, Note, SalesAccountNo, 
RefundAccountNo, DiscountAccountNo, BonusAccountNo, BonusExpAccountNo, Picture 
from packmain 
WHERE PackageID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202247", pPackId);

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
        #region [ DB202248 - Бүтээгдэхүүний багцын бүртгэл нэмэх ]
        public static Result DB202248(DbConnections pDB, object[] pParam, int pUserNo)
        {
            Result res = new Result();
            try
            {
                #region [//Merge ProdPriceHist]
                //object[] objPrice = new object[5];

                //objPrice[0] = IPos.Core.SystemProp.TxnDate;
                //objPrice[1] = 2;
                //objPrice[2] = pParam[0];
                //objPrice[3] = pParam[10];
                //objPrice[4] = pUserNo;

                //res = DB202288(pDB, objPrice);
                //if (res.ResultNo != 0)
                //    return res;
                #endregion

                string sql = "";

                if (pParam[19] == null)
                {
                    object[] obj = new object[19];

                    for (int i = 0; i < 19; i++)
                        obj[i] = pParam[i];

                    sql =
@"INSERT INTO packmain(PackageID, Name, Name2, BrandID, BarCode, 
Unit, UnitSize, Status, Price, Count, 
CreateDate, SalesStartDate, SalesEndDate, Note, SalesAccountNo, 
RefundAccountNo, DiscountAccountNo, BonusAccountNo, BonusExpAccountNo )
VALUES(:1, :2, :3, :4, :5,
:6, :7, :8, :9)";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202214", obj);
                }
                else
                {
                    sql =
    @"INSERT INTO packmain(PackageID, Name, Name2, BrandID, BarCode, Unit, UnitSize, Status, Price, Count, 
CreateDate, SalesStartDate, SalesEndDate, Note, SalesAccountNo, RefundAccountNo, DiscountAccountNo, BonusAccountNo, BonusExpAccountNo, Picture )
VALUES(:1, :2, :3, :4, :5,
:6, :7, :8, :9, :10)";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202248", pParam);
                }
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
        #region [ DB202249 - Бүтээгдэхүүний багцын бүртгэл засварлах ]
        public static Result DB202249(DbConnections pDB, object[] pParam, int pUserNo)
        {
            Result res = new Result();
            try
            {
                #region [//Merge ProdPriceHist]
                //object[] objPrice = new object[5];

                //objPrice[0] = IPos.Core.SystemProp.TxnDate;
                //objPrice[1] = 2;
                //objPrice[2] = pParam[0];
                //objPrice[3] = pParam[10];
                //objPrice[4] = pUserNo;

                //res = DB202288(pDB, objPrice);
                //if (res.ResultNo != 0)
                //    return res;
                #endregion

                string sql = "";

                if (pParam[19] == null)
                {
                    object[] obj = new object[19];

                    for (int i = 0; i < 19; i++)
                        obj[i] = pParam[i];

                    sql =
@"UPDATE packagemain SET
Name=:2, Name2=:3, BrandID=:4, BarCode=:5, 
Unit=:6, UnitSize=:7, Status=:8, Price=:9, Count=:10, 
CreateDate=:11, SalesStartDate=:12, SalesEndDate=:13, Note=:14, SalesAccountNo=:15, 
RefundAccountNo=:16, DiscountAccountNo=:17, BonusAccountNo=:18, BonusExpAccountNo=:19
WHERE PackId=:1";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202214", obj);
                }
                else
                {
                    sql =
@"UPDATE packagemain SET
Name=:2, Name2=:3, BrandID=:4, BarCode=:5, 
Unit=:6, UnitSize=:7, Status=:8, Price=:9, Count=:10, 
CreateDate=:11, SalesStartDate=:12, SalesEndDate=:13, Note=:14, SalesAccountNo=:15, 
RefundAccountNo=:16, DiscountAccountNo=:17, BonusAccountNo=:18, BonusExpAccountNo=:19, Picture=:20
WHERE PackId=:1";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202249", pParam);
                }
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
        #region [ DB202250 - Бүтээгдэхүүний багцын бүртгэл устгах ]
        public static Result DB202250(DbConnections pDB, string pPackId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM packagemain WHERE packageId=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202250", pPackId);

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

        #region [ DB202251 - Бүтээгдэхүүний багц дахь барааны холбоос жагсаалт мэдээлэл авах ]
        public static Result DB202251(DbConnections pDB, string pPackID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.PackageId, a.ProdId, c.name as ProdName, a.ProdType, decode(a.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as ProdTypeName,
a.Count, a.Optional, decode(a.Optional, 0, 'Уг барааг заавал борлуулна', 1, 'Уг бараа өөр ийм төрөлтэй бараанаас аль нэгийг сонгож болно') as OptionalName
from packageitem a
left join (select InvId as prodno, TypeCode as prodtype, Name from invmain) c on a.ProdId=c.prodno
where a.PackageId=:1 and a.ProdType=0
union
select a.PackageId, a.ProdId, c.name as ProdName, a.ProdType, decode(a.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as ProdTypeName,
a.Count, a.Optional, decode(a.Optional, 0, 'Уг барааг заавал борлуулна', 1, 'Уг бараа өөр ийм төрөлтэй бараанаас аль нэгийг сонгож болно') as OptionalName
from Packageitem a
left join (select ServId as prodno, TypeCode as prodtype, Name from servmain) c on a.ProdId=c.prodno
where a.PackageId=:1 and a.ProdType=1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202251", pPackID);

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
        #region [ DB202252 - Бүтээгдэхүүний багц дахь барааны холбоос дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202252(DbConnections pDB, string pPackID, string pProdID, int pProdType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.PackageId, a.ProdId, c.name as ProdName, a.ProdType, decode(a.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as ProdTypeName,
a.Count, a.Optional, decode(a.Optional, 0, 'Уг барааг заавал борлуулна', 1, 'Уг бараа өөр ийм төрөлтэй бараанаас аль нэгийг сонгож болно') as OptionalName
from packageitem a
left join (select InvId as prodno, TypeCode as prodtype, Name from invmain) c on a.ProdId=c.prodno
where a.PackageId=:1 and a.ProdType=0 and a.ProdId=:2 and a.ProdType=:3
union
select a.PackageId, a.ProdId, c.name as ProdName, a.ProdType, decode(a.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as ProdTypeName,
a.Count, a.Optional, decode(a.Optional, 0, 'Уг барааг заавал борлуулна', 1, 'Уг бараа өөр ийм төрөлтэй бараанаас аль нэгийг сонгож болно') as OptionalName
from packageitem a
left join (select ServId as prodno, TypeCode as prodtype, Name from servmain) c on a.ProdId=c.prodno
where a.PackageId=:1 and a.ProdType=1 and a.ProdId=:2 and a.ProdType=:3
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202252", pPackID, pProdID, pProdType);

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
        #region [ DB202253 - Бүтээгдэхүүний багц дахь барааны холбоос нэмэх ]
        public static Result DB202253(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO packageitem(PackageId, ProdId, ProdType, Count, Optional)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202253", pParam);
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
        #region [ DB202254 - Бүтээгдэхүүний багц дахь барааны холбоос засварлах ]
        public static Result DB202254(DbConnections pDB, object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[7];

                obj[0] = pOldParam[0];
                obj[1] = pOldParam[1];
                obj[2] = pOldParam[2];
                obj[3] = pNewParam[1];
                obj[4] = pNewParam[2];
                obj[5] = pNewParam[3];
                obj[6] = pNewParam[4];

                string sql =
@"UPDATE packageitem SET
ProdId=:4, ProdType=:5, Count=:6, Optional=:7
WHERE PackageId=:1 and ProdId=:2 and ProdType=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202254", obj);
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
        #region [ DB202255 - Бүтээгдэхүүний багц дахь барааны холбоос устгах ]
        public static Result DB202255(DbConnections pDB, string pPackId, string pProdID, int pProdType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM packageitem WHERE PackageId=:1 and ProdId=:2 and ProdType=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202255", pPackId, pProdID, pProdType);

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

        #region [ DB202256 - Бүтээгдэхүүний багцийн үйлчлүүлэгчид жагсаалт мэдээлэл авах ]
        public static Result DB202256(DbConnections pDB, string pPackID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.packageid, a.custno, c.firstname, c.lastname, c.corporatename
from PackageCust a
left join customer c on a.custno=c.customerno
where a.PackageId=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202256", pPackID);

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
        #region [ DB202257 - Бүтээгдэхүүний багцийн үйлчлүүлэгчид дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202257(DbConnections pDB, string pPackID, long pCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.packageid, a.custno, c.firstname, c.lastname, c.corporatename
from PackageCust a
left join customer c on a.custno=c.customerno
where a.PackageId=:1 and a.custno=:2
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202257", pPackID, pCustNo);

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
        #region [ DB202258 - Бүтээгдэхүүний багцийн үйлчлүүлэгчид нэмэх ]
        public static Result DB202258(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PackageCust(PackageId, custno)
VALUES(:1, :2)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202258", pParam);
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
        #region [ DB202259 - Бүтээгдэхүүний багцийн үйлчлүүлэгчид засварлах ]
        public static Result DB202259(DbConnections pDB, string pPackID, long pOldCustNo, long pNewCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PackageCust SET
custno=:3
WHERE PackageId=:1 and custno=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202259", pPackID, pOldCustNo, pNewCustNo);
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
        #region [ DB202260 - Бүтээгдэхүүний багцийн үйлчлүүлэгчид устгах ]
        public static Result DB202260(DbConnections pDB, string pPackID, long pCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PackageCust WHERE PackageId=:1 and custno=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202260", pPackID, pCustNo);

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

        #region [ DB202261 - Бүтээгдэхүүний багцийг борлуулах хэрэглэгчид жагсаалт мэдээлэл авах ]
        public static Result DB202261(DbConnections pDB, string pPackID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.packageid, a.UserNo, c.userfname, c.userlname
from PackageUser a
left join hpuser c on a.UserNo=c.UserNo
where a.PackageId=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202261", pPackID);

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
        #region [ DB202262 - Бүтээгдэхүүний багцийг борлуулах хэрэглэгчид дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202262(DbConnections pDB, string pPackID, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.packageid, a.UserNo, c.userfname, c.userlname
from PackageUser a
left join hpuser c on a.UserNo=c.UserNo
where a.PackageId=:1 and a.UserNo=:2
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202262", pPackID, pUserNo);

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
        #region [ DB202263 - Бүтээгдэхүүний багцийг борлуулах хэрэглэгчид нэмэх ]
        public static Result DB202263(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PackageUser(PackageId, UserNo)
VALUES(:1, :2)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202263", pParam);
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
        #region [ DB202264 - Бүтээгдэхүүний багцийг борлуулах хэрэглэгчид засварлах ]
        public static Result DB202264(DbConnections pDB, string pPackID, int pOldUserNo, int pNewUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PackageUser SET
UserNo=:3
WHERE PackageId=:1 and UserNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202264", pPackID, pOldUserNo, pNewUserNo);
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
        #region [ DB202265 - Бүтээгдэхүүний багцийг борлуулах хэрэглэгчид устгах ]
        public static Result DB202265(DbConnections pDB, string pPackID, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PackageUser WHERE PackageId=:1 and UserNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202265", pPackID, pUserNo);

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
        //
        #region [ DB202266 - Барааны үнийн бүртгэл жагсаалт мэдээлэл авах ]
        public static Result DB202266(DbConnections pDB, int pProdType, string pProdId)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select 
a.ProdType, a.ProdId, a.PriceTypeID, b.name as PriceTypeIDname, a.Price, c.name as daytypename
from ProdPrice a
left join PAPriceType b on a.PriceTypeID=b.PriceTypeID
left join PADayType c on b.DayType=c.DayType
where a.ProdType=:1 and a.ProdId=:2
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202266", pProdType, pProdId);

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
        #region [ DB202267 - Барааны үнийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202267(DbConnections pDB, int pProdType, string pProdId, string PriceTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select 
a.ProdType, a.ProdId, a.PriceTypeID, b.name as PriceTypeIDname, a.Price, c.name as daytypename
from ProdPrice a
left join PAPriceType b on a.PriceTypeID=b.PriceTypeID
left join PADayType c on b.DayType=c.DayType
where a.ProdType=:1 and a.ProdId=:2 and A.PriceTypeID=:3
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202267", pProdType, pProdId, PriceTypeID);

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
        #region [ DB202268 - Барааны үнийн бүртгэл нэмэх ]
        public static Result DB202268(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO ProdPrice(ProdType, ProdId, PriceTypeID, Price)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202268", pParam);
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
        #region [ DB202269 - Барааны үнийн бүртгэл засварлах ]
        public static Result DB202269(DbConnections pDB, object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[7];

                //ProdType-0, ProdId-1, PriceTypeID-2, Price-3

                obj[0] = pOldParam[0];
                obj[1] = pOldParam[1];
                obj[2] = pOldParam[2];

                obj[3] = pNewParam[0];
                obj[4] = pNewParam[1];
                obj[5] = pNewParam[2];
                obj[6] = pNewParam[3];

                string sql =
@"UPDATE ProdPrice SET
ProdType=:4, ProdId=:5, PriceTypeID=:6, Price=:7
WHERE ProdType=:1 and ProdId=:2 and PriceTypeID=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202269", obj);
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
        #region [ DB202270 - Барааны үнийн бүртгэл устгах ]
        public static Result DB202270(DbConnections pDB, int pProdType, string pProdId, string PriceTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ProdPrice WHERE ProdType=:1 and ProdId=:2 and PriceTypeID=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202270", pProdType, pProdId, PriceTypeID);

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
        //
        #region [ DB202276 - Дамжлагын бүртгэлийн жагсаалт мэдээлэл авах ]
        public static Result DB202276(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT STEPITEMID, NAME, NAME2, NOTE, NOTE2, WORKDAYS
FROM STEPITEM
Order By STEPITEMID ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202276", null);

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
        #region [ DB202277 - Дамжлагын бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202277(DbConnections pDB, int pStepItemID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT STEPITEMID, NAME, NAME2, NOTE, NOTE2, WORKDAYS
FROM STEPITEM 
WHERE STEPITEMID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202277", pStepItemID);

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
        #region [ DB202278 - Дамжлагын бүртгэл нэмэх ]
        public static Result DB202278(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO STEPITEM(STEPITEMID, NAME, NAME2, NOTE, NOTE2, WORKDAYS)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202278", pParam);
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
        #region [ DB202279 - Дамжлагын бүртгэл засварлах ]
        public static Result DB202279(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE STEPITEM SET
NAME=:2, NAME2=:3, NOTE=:4, NOTE2=:5, WORKDAYS=:6
WHERE STEPITEMID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202279", pParam);
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
        #region [ DB202280 - Дамжлагын бүртгэл устгах ]
        public static Result DB202280(DbConnections pDB, int pStepItemID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM STEPITEM WHERE STEPITEMID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202280", pStepItemID);

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

        #region [ DB202281 - Дамжлагын бүлгийн жагсаалт мэдээлэл авах ]
        public static Result DB202281(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT STEPID, NAME, NAME2, NOTE, NOTE2
FROM STEP
Order BY STEPID ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202281", null);

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
        #region [ DB202282 - Дамжлагын бүлгийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202282(DbConnections pDB, int pStepID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT STEPID, NAME, NAME2, NOTE, NOTE2
FROM STEP
WHERE STEPID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202282", pStepID);

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
        #region [ DB202283 - Дамжлагын бүлэг нэмэх ]
        public static Result DB202283(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO STEP(STEPID, NAME, NAME2, NOTE, NOTE2)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202283", pParam);
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
        #region [ DB202284 - Дамжлагын бүлэг засварлах ]
        public static Result DB202284(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE STEP SET
NAME=:2, NAME2=:3, NOTE=:4, NOTE2=:5
WHERE STEPID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202284", pParam);
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
        #region [ DB202285 - Дамжлагын бүлэг устгах ]
        public static Result DB202285(DbConnections pDB, int pStepID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM StepLink WHERE STEPID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202285", pStepID);

                if (res.ResultNo != 0)
                    return res;

                sql =
@"DELETE FROM STEP WHERE STEPID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202285", pStepID);

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

        #region [ DB202287 - Бараа үйлчилгээ багцын үнийн түүхийн жагсаалтыг хуудаслаж авах ]
        public static Result DB202287(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] {"TxnDate","ProdType","ProdId like","a.Price like"};

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
@"select TxnDate, ProdType, 'Бараа' as ProdTypeName , ProdId , b.name, Price, a.userno
from ProdPriceHist a
left join invmain b on a.prodid=B.INVID
where ProdType=0
{0} {1}
union
select TxnDate, ProdType, 'Үйлчилгээ' as ProdTypeName , ProdId , b.name, Price , a.userno
from ProdPriceHist a
left join servmain b on a.prodid=B.servid
where ProdType=1
{0} {1}
union
select a.TxnDate, a.ProdType, 'Багц' as ProdTypeName , a.ProdId , b.name, a.Price , a.userno
from ProdPriceHist a
left join packmain b on a.prodid=B.packid
where a.ProdType=2
{0} {1} {2}", sb.Length > 0 ? " and " : "", sb.ToString(), " Order by TxnDate desc");

                res = pDB.ExecuteQuery("core", sql, "DB202287", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202288 - Бараа үйлчилгээ багцын үнийн түүх хадгалах ]
        public static Result DB202288(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"MERGE INTO ProdPriceHist b
USING (
SELECT :1 TxnDate, :2 ProdType, :3 ProdId, :4 Price, :5 UserNo
  FROM dual
  ) e
ON (b.TxnDate = e.TxnDate and b.ProdType = e.ProdType and b.ProdId = e.ProdId)
WHEN MATCHED THEN
  UPDATE SET Price=:4, UserNo=:5
WHEN NOT MATCHED THEN
 insert (TxnDate, ProdType,  ProdId, Price, UserNo)
values (:1, :2, :3, :4, :5)
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202288", pParam);
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
        #region [ DB202289 - Балансын гадуурх дансны бүтээгдэхүүн жагсаалтыг хуудаслаж авах ]
        public static Result DB202289(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] {"ProdCode like","Name like","Name2 like","CurCode","GL",
                    "TypeCode","OrderNo"};

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
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, 
TypeCode, decode(TypeCode, 0, 'ЭНГИЙН', 1, 'БАЛАНСЖУУЛАХ') as TypeCodeName, ORDERNO
FROM CONPRODUCT
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by ORDERNO");

                res = pDB.ExecuteQuery("core", sql, "DB202289", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202290 - Байгууллагын дансны бүтээгдэхүүн жагсаалтыг хуудаслаж авах ]
        public static Result DB202290(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "ProdCode like", "Name like", "CurCode", "GL like", "Type like" };

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
                                sb.AppendFormat(" {0} '%'||:{1}||'%' ", fieldnames[i], dbindex++);
                            else
                            sb.AppendFormat(" {0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT PRODCODE, NAME, NAME2, CURCODE, GL, 
TYPE, decode(type, 0, 'БУСАД', 1, 'ОРЛОГО', 2, 'ЗАРЛАГА', 3, 'АВЛАГА', 4, 'ӨГЛӨГ', 5, 'КАСС', 6, 'ХӨРӨНГӨ', 7, 'ӨМЧ') typeName,
BalanceType, decode(BalanceType, 'D', 'DEBIT', 'C', 'CREDIT', 'Z', 'DEBIT AND CREDIT') BalanceTypeName, ORDERNO
FROM BACPRODUCT
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by ORDERNO");

                res = pDB.ExecuteQuery("core", sql, "DB202290", pageindex, pagerows, dbparam.ToArray());

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

        #region [ DB202291 - Барааны серийн дугаарууд мэдээлэл авах ]
        public static Result DB202291(DbConnections pDB, string pInvID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ItemNo,
BarCode,
InvID,
Status,
decode(status, 0, 'Идэвхгүй', 'Идэхтэй') as StatusName,
LastPrepareUserNo,
LastPrepareDate,
Note
from InvSeries
where InvID=:1
order by barcode";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202291", pInvID);

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
        #region [ DB202292 - Барааны серийн дугаарууд дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202292(DbConnections pDB, string pItemNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ItemNo,
BarCode,
InvID,
Status,
decode(status, 0, 'Идэвхгүй', 'Идэхтэй') as StatusName,
LastPrepareUserNo,
LastPrepareDate,
Note
from InvSeries
where ItemNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202292", pItemNo);

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
        #region [ DB202293 - Барааны серийн дугаарууд нэмэх ]
        public static Result DB202293(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO INVSERIES(ItemNo, BarCode, InvID, Status, LastPrepareUserNo, 
LastPrepareDate, Note)
VALUES(:1, :2, :3, :4, :5,
:6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202293", pParam);
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
        #region [ DB202294 - Барааны серийн дугаарууд засварлах ]
        public static Result DB202294(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE INVSERIES SET
BarCode=:2, InvID=:3, Status=:4, LastPrepareUserNo=:5, 
LastPrepareDate=:6, Note=:7
WHERE ItemNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202294", pParam);
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
        #region [ DB202295 - Барааны серийн дугаарууд устгах ]
        public static Result DB202295(DbConnections pDB, string pItemNo)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM INVSERIES WHERE ItemNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202295", pItemNo);

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

        #region [ DB202296 - Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр. авах ]
        public static Result DB202296(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.ParentId, b.name as ParentName, a.ItemId, bb.name as ItemName, a.ItemType
from ProdTree a
left join ProdTreeDesc b on a.ParentId=b.ItemId
left join ProdTreeDesc bb on a.ItemId=b.ItemId
order by a.ParentId, a.ItemId";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202296", null);

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
        #region [ DB202297 - Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр. дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202297(DbConnections pDB, string pParentID, string pItemID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.ParentId, b.name as ParentName, a.ItemId, bb.name as ItemName, a.ItemType
from ProdTree a
left join ProdTreeDesc b on a.ParentId=b.ItemId
left join ProdTreeDesc bb on a.ItemId=b.ItemId
where a.ParentId=:1 and a.ItemId=:2
order by a.ParentId, a.ItemId";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202297", pParentID, pItemID);

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
        #region [ DB202298 - Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр. нэмэх ]
        public static Result DB202298(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO ProdTree(ParentId, ItemId, ItemType)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202298", pParam);
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
        #region [ DB202299 - Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр. засварлах ]
        public static Result DB202299(DbConnections pDB, string pOldParentID, string pOldItemID, object[] pNewParam)
        {
            Result res = new Result();
            try
            {

                object[] obj = new object[5];

                obj[0] = pOldParentID;
                obj[1] = pOldItemID;
                obj[2] = pNewParam[0];
                obj[3] = pNewParam[1];
                obj[4] = pNewParam[2];


                string sql =
@"UPDATE ProdTree SET
ParentId=:3, ItemId=:4, ItemType=:5
WHERE ParentId=:1
and ItemId=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202299", obj);
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
        #region [ DB202300 - Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр. устгах ]
        public static Result DB202300(DbConnections pDB, string pParentID, string pItemID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM ProdTree WHERE ParentId=:1 and ItemId=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202300", pParentID, pItemID);

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

        #region [ DB202301 - Жагсаалтын бүртгэлийн жагсаалт мэдээлэл авах ]
        public static Result DB202301(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, NAME, LASTUPDATED, DESCRIPTION, SQL, REFRESHINTERVAL, FIELDNAMES
FROM DICTIONARY
ORDER BY ID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202301", null);

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
        #region [ DB202302 - Жагсаалтын бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202302(DbConnections pDB, string pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, NAME, LASTUPDATED, DESCRIPTION, SQL, REFRESHINTERVAL, FIELDNAMES
FROM DICTIONARY
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202302", pID);

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
        #region [ DB202303 - Жагсаалтын бүртгэл нэмэх ]
        public static Result DB202303(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO DICTIONARY(ID, NAME, LASTUPDATED, DESCRIPTION, SQL, REFRESHINTERVAL, FIELDNAMES)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202303", pParam);
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
        #region [ DB202304 - Жагсаалтын бүртгэл засварлах ]
        public static Result DB202304(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE DICTIONARY SET
NAME=:2, LASTUPDATED=:3, DESCRIPTION=:4, SQL=:5, REFRESHINTERVAL=:6, FIELDNAMES=:7
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202304", pParam);
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
        #region [ DB202305 - Жагсаалтын бүртгэл устгах ]
        public static Result DB202305(DbConnections pDB, string pID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM DICTIONARY WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202305", pID);

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

        #region [ DB202306 - Динамик жагсаалт бүртгэлийн жагсаалт мэдээлэл авах ]
        public static Result DB202306(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, ID, NAME,FORMULA,RATE
FROM DYNAMICLIST
Order by Key, ID ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202306", null);

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
        #region [ DB202307 - Динамик жагсаалт бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202307(DbConnections pDB, string pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, ID, NAME,FORMULA,RATE
FROM DYNAMICLIST
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202307", pID);

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
        #region [ DB202308 - Динамик жагсаалт бүртгэл нэмэх ]
        public static Result DB202308(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO DYNAMICLIST(KEY, ID, NAME,FORMULA,RATE)
VALUES(:1, :2, :3,:4,:5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202308", pParam);
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
        #region [ DB202309 - Динамик жагсаалт бүртгэл засварлах ]
        public static Result DB202309(DbConnections pDB, object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[10];
                obj[0] = pOldParam[0];
                obj[1] = pOldParam[1];
                obj[2] = pOldParam[2];
                obj[3] = pOldParam[3];
                obj[4] = pOldParam[4];
        
                obj[5] = pNewParam[0];
                obj[6] = pNewParam[1];
                obj[7] = pNewParam[2];
                obj[8] = pNewParam[3];
                obj[9] = pNewParam[4];

                string sql =
@"UPDATE DYNAMICLIST SET
KEY=:6, ID=:7, NAME=:8,FORMULA=:9,RATE=:10
WHERE KEY=:1 AND ID=:2 AND NAME=:3 and FORMULA=:4 and RATE=:5";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202309", obj);
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
        #region [ DB202310 - Динамик жагсаалт бүртгэл устгах ]
        public static Result DB202310(DbConnections pDB, string pID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM DYNAMICLIST WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202310", pID);

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

        #region [ DB202311 - Автомат дугаарлалтын утгын жагсаалт мэдээлэл авах ]
        public static Result DB202311(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, CODE, KEY, VALUE
FROM AUTONUMVALUE";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202311", null);

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
        #region [ DB202312 - Автомат дугаарлалтын утга нэмэх ]
        public static Result DB202312(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO AUTONUMVALUE(ID, CODE, KEY, VALUE)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202312", pParam);

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
        #region [ DB202313 - Автомат дугаарлалтын утга засварлах ]
        public static Result DB202313(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update
    AUTONUMVALUE
set VALUE=:3
where
    ID=:1 AND CODE=:2 AND KEY=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202313", pParam);
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

        #region [ DB202315 - Балансын гадуурх дансны бүртгэлийн жагсаалтыг хуудаслаж авах ]
        public static Result DB202315(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "AccountNo like","Name like","Name2 like","BranchNo","ProdCode like",
"Balance","CurCode","UserNo","LevelNo","CreateDate",
"StartDate","EndDate","Status","ContractID like","InsuranceNo like",
"RiInsuranceNo like","ClaimNo like","CustNo like","Person like"
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
                            sb.AppendFormat(" c.{0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"SELECT c.ACCOUNTNO, c.NAME, c.NAME2, c.BRANCHNO, 
c.PRODCODE, cc.name as ProdName, c.BALANCE, c.CURCODE, c.USERNO, c.LEVELNO, c.CREATEDATE,
c.STATUS, decode(c.STATUS, 0, 'НЭЭЛТТЭЙ', 1, 'ХААГДСАН') StatusName,
c.STARTDATE, c.ENDDATE, c.CONTRACTID, c.INSURANCENO, c.RIINSURANCENO, c.CLAIMNO, c.CUSTNO, c.PERSON, c.LastTellerTxnDate
FROM CONACCOUNT c
left join conproduct cc on c.prodcode=cc.prodcode
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by c.ACCOUNTNO");

                res = pDB.ExecuteQuery("core", sql, "DB202315", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202316 - Гүйлгээний кодын жагсаалтыг хуудаслаж авах ]
        public static Result DB202316(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "TRANCODE", "NAME like", "NAME2 like" };

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
@"SELECT TRANCODE, NAME, NAME2
FROM TXN
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by TRANCODE");

                res = pDB.ExecuteQuery("core", sql, "DB202316", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202317 - Гүйлгээний оролтуудын холбоосын жагсаалтыг хуудаслаж авах ]
        public static Result DB202317(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TRANCODE, ENTRYCODE, ORDERNO
FROM TXNENTRY
order by orderno";

                res = pDB.ExecuteQuery("core", sql, "DB202317", pageindex, pagerows, null);

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
        #region [ DB202318 - Борлуулалтын дэлгэрэнгүй жагсаалт BO хуудаслаж авах ]
        public static Result DB202318(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "sa1.trandate", "sa1.CASHIERNO" };

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
@"select 
nvl(sp.trandate,st.trandate) trandate
,nvl(sp.salesno,st.salesno) salesno
,s.custno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,decode(sp.flag,'N','БОРЛ','R','БУЦ','E','ХАРИУЛТ') flag
,sp.salesamount,sp.discount,sp.amount
,sp.amount*s.vat/100 vat
,st.totalpaid,st.cash,st.card,st.other,
sp.cashierno, hp.userfname, hp.userlname
from (
    select sa1.trandate,sp1.salesno,sp1.flag
    ,sum(sp1.price*sp1.qty) salesamount
    ,sum(sp1.price*sp1.discountprod + sp1.discountsales) discount
    ,sum(sp1.qty*(sp1.price-sp1.discountprod)-sp1.discountsales) amount,
    sa1.cashierno
    from salesprod sp1
    left join salesaction sa1 on sa1.actionid=sp1.actionid
    {0} {1}
    group by sa1.trandate,sp1.salesno,sp1.flag, sa1.cashierno
) sp
full join
(
    select sa1.trandate,st1.salesno,st1.flag
    ,sum(st1.amount) totalpaid
    ,sum(decode(pt.paymentflag,0,st1.amount,0)) cash
    ,sum(decode(pt.paymentflag,1,st1.amount,0)) card
    ,sum(decode(pt.paymentflag,0,0,1,0,st1.amount)) other
    from salestxn st1
    left join salesaction sa1 on sa1.actionid=st1.actionid
    left join papaytype pt on pt.typeid=st1.paymenttype
    {0} {1}
    group by sa1.trandate,st1.salesno,st1.flag
) st on /*st.trandate=sp.trandate and*/ st.salesno=sp.salesno
left join sales s on s.salesno=nvl(sp.salesno,st.salesno)
left join customer c on s.custno=c.customerno
left join hpuser hp on sp.cashierno=hp.userno
{2}", sb.Length > 0 ? "where" : "", sb.ToString(), " order by 1,2");

                res = pDB.ExecuteQuery("core", sql, "DB202318", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202319 - Борлуулсан бараа болон үйлчилгээний жагсаалт BO ]
        public static Result DB202319(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "sa.trandate", "sa.CASHIERNO" };

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
@"select sa.trandate,sa.postdate,sa.shiftno,sa.cashierno,sa.posno,sa.areacode
,sp.salesno,sp.custno
,decode(c.classcode,1,c.corporatename,c.lastname||', '||c.firstname) custname
,sp.prodno,sp.prodtype
,decode(sp.prodtype,0,im.name,1,sm.name,pm.name) prodname
,sp.price,sp.baseprice,sp.qty,sp.salesamount
,sp.discountprod,sp.discountsales,sp.salestype,sp.flag,sp.subtype
from salesprod sp
inner join salesaction sa on sa.actionid=sp.actionid
left join customer c on c.customerno=sp.custno
left join invmain im on im.invid=sp.prodno and sp.prodtype=0
left join servmain sm on sm.servid=sp.prodno and sp.prodtype=1
left join packmain pm on pm.packid=sp.prodno and sp.prodtype=2
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " order by salesno");

                res = pDB.ExecuteQuery("core", sql, "DB202319", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB202320 - Төлбөрийн жагсаалт BO ]
        public static Result DB202320(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "sa.trandate", "sa.CASHIERNO" };

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
@"select sa.trandate,sa.postdate,sa.shiftno,sa.cashierno,sa.posno,sa.areacode
,sp.salesno,s.custno,decode(c.classcode,1,c.corporatename,c.lastname||', '||c.firstname) custname
,sp.paymentno,pt.name paymentname,sp.sourceno,sp.amount,sp.flag 
from salestxn sp
inner join salesaction sa on sa.actionid=sp.actionid
left join sales s on s.salesno=sp.salesno
left join customer c on c.customerno=s.custno
left join papaytype pt on pt.typeid=sp.paymenttype
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " order by salesno");

                res = pDB.ExecuteQuery("core", sql, "DB202320", pageindex, pagerows, dbparam.ToArray());

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
        

        #region [ DB202321 - ShortCut бүртгэлийн жагсаалт мэдээлэл авах ]
        public static Result DB202321(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, KEYS, KEYS1, KEYS2, NAME, IDVALUE, DESCRIPTION, TYPE, DECODE(TYPE,1,'ЦОНХ',2,'TEКСТ',TYPE) AS TYPENAME
FROM SHORTCUT 
ORDER BY ID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202321", null);

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
        #region [ DB202322 - ShortCut бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202322(DbConnections pDB, string pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, KEYS, KEYS1, KEYS2, NAME, IDVALUE, DESCRIPTION, TYPE
FROM SHORTCUT
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202322", pID);

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
        #region [ DB202323 - ShortCut бүртгэл нэмэх ]
        public static Result DB202323(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO SHORTCUT(ID, KEYS, KEYS1, KEYS2, NAME, IDVALUE, DESCRIPTION, TYPE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202323", pParam);
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
        #region [ DB202324 - ShortCut бүртгэл засварлах ]
        public static Result DB202324(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SHORTCUT SET
KEYS=:2, KEYS1=:3, KEYS2=:4, NAME=:5, IDVALUE=:6, DESCRIPTION=:7, TYPE=:8
WHERE ID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202324", pParam);
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
        #region [ DB202325 - ShortCut бүртгэл устгах ]
        public static Result DB202325(DbConnections pDB, string pID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM SHORTCUT WHERE ID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202325", pID);

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

        #region [ DB202326 - Эвдэрлийн төрөл жагсаалт мэдээлэл авах ]
        public static Result DB202326(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT DamageType, Name, OrderNo
from PADamageType
ORDER BY DamageType";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202326", null);

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
        #region [ DB202327 - Эвдэрлийн төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202327(DbConnections pDB, string pDamageType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT DamageType, Name, OrderNo
from PADamageType
WHERE DamageType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202327", pDamageType);

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
        #region [ DB202328 - Эвдэрлийн төрөл нэмэх ]
        public static Result DB202328(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PADamageType(DamageType, Name, OrderNo)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202328", pParam);
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
        #region [ DB202329 - Эвдэрлийн төрөл засварлах ]
        public static Result DB202329(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PADamageType SET
Name=:2, OrderNo=:3
WHERE DamageType=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202329", pParam);
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
        #region [ DB202330 - Эвдэрлийн төрөл устгах ]
        public static Result DB202330(DbConnections pDB, string pDamageType)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM PADamageType WHERE DamageType=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202330", pDamageType);

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

        #region [ DB202331 - Тагын жагсаалт мэдээлэл авах ]
        public static Result DB202331(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TagId, TagType, Status, OrderNo
from Tagmain
ORDER BY OrderNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202331", null);

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
        #region [ DB202332 - Тагын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202332(DbConnections pDB, string TagID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TagID, TagType, Status, OrderNo
from TagMain
WHERE TagID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202332", TagID);

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
        #region [ DB202333 - Таг нэмэх ]
        public static Result DB202333(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO TagMain(TagID, TagType, Status, OrderNo)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202333", pParam);
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
        #region [ DB202334 - Таг засварлах ]
        public static Result DB202334(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE TagMain SET
TagType=:2, Status=:3, OrderNo=:4
WHERE TagID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202334", pParam);
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
        #region [ DB202335 - Таг устгах ]
        public static Result DB202335(DbConnections pDB, string TagID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM TagMain WHERE TagID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202335", TagID);

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

        #region [ DB202336 - Үнийн төрөл жагсаалт мэдээлэл авах ]
        public static Result DB202336(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.pricetypeid, a.name, a.name2, a.daytype, c.name as daytypename , a.starttime, a.endtime,
a.orderno
from PAPriceType a 
left join PADayType c on a.DayType=c.DayType 
order by a.orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202336", null);

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
        #region [ DB202337 - Үнийн төрөл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202337(DbConnections pDB, string pricetypeid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.pricetypeid, a.name, a.name2, a.daytype, c.name as daytypename , a.starttime, a.endtime,
a.orderno
from PAPriceType a 
left join PADayType c on a.DayType=c.DayType 
order by a.orderno
WHERE a.pricetypeid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202337", pricetypeid);

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
        #region [ DB202338 - Үнийн төрөл нэмэх ]
        public static Result DB202338(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO PAPriceType(PriceTypeID, Name, Name2, DayType, StartTime, 
EndTime, OrderNo)
VALUES(:1, :2, :3, :4, :5,
:6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202338", pParam);
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
        #region [ DB202339 - Үнийн төрөл засварлах ]
        public static Result DB202339(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE PAPriceType SET
Name=:2, Name2=:3, DayType=:4, StartTime=:5, 
EndTime=:6, OrderNo=:7
WHERE PriceTypeID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202339", pParam);
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
        #region [ DB202340 - Үнийн төрөл устгах ]
        public static Result DB202340(DbConnections pDB, string PriceTypeID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM PAPriceType WHERE PriceTypeID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202340", PriceTypeID);

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

        #region [ DB202351 - xls тайлангийн параметрийн жагсаалт мэдээлэл авах ]
        public static Result DB202351(DbConnections pDB, string pViewName)
        {
            Result res = new Result();
            try
            {
                string sql ;

                if (pViewName == "")
                {
                sql =
@"select ViewName,ParamID,ParamType,FieldName,ParamName,
ParamDesc,DefaultValue,Condition,DicID,DicNameField,
DicValueField
from repparam
ORDER BY ViewName,ParamID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202351", null);
                }
                else
                {
                sql =
@"select ViewName,ParamID,ParamType,FieldName,ParamName,
ParamDesc,DefaultValue,Condition,DicID,DicNameField,
DicValueField
from repparam
where ViewName=:1
ORDER BY ViewName,ParamID";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202351", pViewName);
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
        #region [ DB202352 - xls тайлангийн параметр дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202352(DbConnections pDB, string pViewName, int pParamID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ViewName,ParamID,ParamType,FieldName,ParamName,
ParamDesc,DefaultValue,Condition,DicID,DicNameField,
DicValueField
from repparam
WHERE ViewName=:1 and ParamID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202352", pViewName, pParamID);

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
        #region [ DB202353 - xls тайлангийн параметр нэмэх ]
        public static Result DB202353(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO repparam(ViewName,ParamID,ParamType,FieldName,ParamName,
ParamDesc,DefaultValue,Condition,DicID,DicNameField,
DicValueField)
VALUES(:1, :2, :3, :4, :5,
:6, :7, :8, :9, :10,
:11)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202353", pParam);
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
        #region [ DB202354 - xls тайлангийн параметр засварлах ]
        public static Result DB202354(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE repparam SET
ParamType=:3,FieldName=:4,ParamName=:5,
ParamDesc=:6,DefaultValue=:7,Condition=:8,DicID=:9,DicNameField=:10,
DicValueField=:11
WHERE ViewName=:1 and ParamID=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202354", pParam);
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
        #region [ DB202355 - xls тайлангийн параметр устгах ]
        public static Result DB202355(DbConnections pDB, string pViewName, int pParamID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM repparam WHERE ViewName=:1 and ParamID=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202355", pViewName, pParamID);

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

        #region [ DB202356 - Бүлэглэлийн Тухайн мөчрийн бүртгэл авах ]
        public static Result DB202356(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select itemid, name, name2 from prodtreedesc order by name";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202356", null);

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
        #region [ DB202357 - Бүлэглэлийн Тухайн мөчрийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB202357(DbConnections pDB, string pitemid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select itemid, name, name2 from prodtreedesc where itemid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202357", pitemid);

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
        #region [ DB202358 - Бүлэглэлийн Тухайн мөчрийн бүртгэл нэмэх ]
        public static Result DB202358(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO prodtreedesc(itemid, name, name2)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202358", pParam);
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
        #region [ DB202359 - Бүлэглэлийн Тухайн мөчрийн бүртгэл засварлах ]
        public static Result DB202359(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE prodtreedesc SET
name=:2, name2=:3
WHERE itemid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202359", pParam);
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
        #region [ DB202360 - Бүлэглэлийн Тухайн мөчрийн бүртгэл устгах ]
        public static Result DB202360(DbConnections pDB, string pItemID)
        {
            Result res = new Result();
            try
            {
                string sql;
                sql =
@"DELETE FROM prodtreedesc WHERE ItemId=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB202360", pItemID);

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
        #endregion
        #region [ DB203 - Admin&LG ]
        #region [ Admin ]
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

                sql =string.Format(
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

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203002", pUserNo );

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
"+sqln+ @"
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

                foreach (DataRow dr in DT.Rows )
                {

                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
@"insert into usergroup(groupid, UserNo, ExpireDate,level1,level2,level3,level4)
values(:1, :2, :3,:4,:5,:6,:7)";

                     res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203009", dr["GroupID"], pUserNo,Static.ToDateTime(dr["ExpireDate"]),Static.ToInt(dr["level1"]),Static.ToInt(dr["level2"]),Static.ToInt(dr["level3"]),Static.ToInt(dr["level4"]));
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
                string[] fieldnames = new string[] {"GroupID","Name like","Name2 like","OrderNo"};

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

                sql =string.Format(
@"SELECT GROUPID, NAME, NAME2, ORDERNO,levelno
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

        #region [ DB203019 - POS төлбөрийн хэрэгсэл бүртгэл жагсаалт авах ]
        public static Result DB203019(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT a.POSNo, a.PayTypeId, b.name
FROM POSPayType a
left join PAPayType b on a.PayTypeId=b.typeid
order by a.PayTypeId";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203019", null);

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
        #region [ DB203020 - POS төлбөрийн хэрэгсэл бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB203020(DbConnections pDB, string pPOSNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT a.POSNo, a.PayTypeId, b.name
FROM POSPayType a
left join PAPayType b on a.PayTypeId=b.typeid
WHERE a.POSNo = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB203020", pPOSNo);

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
        #region [ DB203021 - POS төлбөрийн хэрэгсэл бүртгэл шинээр нэмэх ]
        public static Result DB203021(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO POSPayType(POSNo, PayTypeId)
VALUES(:1, :2)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB203021", pParam);
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
        #region [ DB203022 - POS төлбөрийн хэрэгсэл бүртгэл засварлах ]
        public static Result DB203022(DbConnections pDB, string pPosNo, string pOldTypeID, string pNewTypeID)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE POSPayType SET
PayTypeId =:3
WHERE POSNo=:1 and PayTypeId=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB203022", pPosNo, pOldTypeID, pNewTypeID);
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
        #region [ DB203023 - POS төлбөрийн хэрэгсэл бүртгэл устгах ]
        public static Result DB203023(DbConnections pDB, string pPosNo, string pTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM POSPayType WHERE PosNo=:1 and PayTypeId=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB203023", pPosNo, pTypeID);

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
        #region [ LG ]
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

                sql =string.Format(
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
        #endregion
        #endregion
        #region [ DB204 - Contract & Orders & PreSale ]
        #region [ Contract ]
        #region [ DB204001 - Гэрээний үндсэн бүртгэл жагсаалт авах ]
        public static Result DB204001(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                string[] fieldnames = new string[] { "ContractNo like","ContractType","CustNo like","FirstName like","LastName like",
"CorporateName like","ValidStartDate","ValidStartTime","ValidEndDate","ValidEndTime",
"Amount","Balance","CurCode","PersonCount","DepFreq",
"DepAmount","Status","CreateDate","CreatePostDate","CreateUser",
"OwnerUser"};

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
@"select *
from V_CONTRACTLIST
{0} {1} ", sb.Length > 0 ? "where" : " order by CONTRACTNO desc", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB204001", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB204002 - Гэрээний үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204002(DbConnections pDB, string pContractNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select *
from V_CONTRACTLIST
where ContractNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204002", pContractNo);

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
        #region [ DB204003 - Гэрээний үндсэн бүртгэл шинээр нэмэх ]
        public static Result DB204003(DbConnections pDB, object[] pParam, int flag, string contractno)
        {
            Result res = new Result();
            try
            {
                if (flag == 0)
                {
                    string seq = "";
                    #region [ ContractNo ]
                    //[T02][Y04][C02][P04][R03][S06]	YCPTRS	"Y=Year; C=CurrencyCode; P=MemberType; T=ContractType; R=Random number; S=Sequence (CODE=ContractType)"

                    Core.AutoNumEnum enums = new Core.AutoNumEnum();
                    enums.Y = Static.ToStr(Static.ToDate(pParam[14]).Year);
                    enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[9])).CurrencyCode);
                    //enums.P = Static.ToStr(pParam[1]);
                    enums.T = Static.ToStr(pParam[1]);

                    Random rnd = new Random();
                    enums.R = Static.ToStr(rnd.Next(100000000, 999999999));
                    
                    Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 3, enums.T, enums);

                    if (seqres.ResultNo == 0)
                    {
                        seq = Static.ToStr(seqres.ResultDesc);
                        if (seq == "")
                        {
                            seqres.ResultNo = 9110068;
                            seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:3][" + seqres.ResultDesc + "]";
                            return seqres;
                        }
                    }
                    else
                        return seqres;

                    pParam[0] = seq;
                    #endregion
                }
                else
                {
                    pParam[0] = contractno;
                }
                string sql =
@"INSERT INTO ContractMain(ContractNo, ContractType, CustNo, ValidStartDate, ValidStartTime, ValidEndDate, ValidEndTime, Amount, Balance, CurCode,
PersonCount, DepFreq, DepAmount, Status, CreateDate, CreatePostDate, CreateUser, OwnerUser, Rebateid, Loyalid, 
Pointid, BalanceType, Vat, Accountno, IncomeAccountno, BalanceMethod, DepBalance)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23, :24, :25, :26, :27)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204003", pParam);
                res = F_Error(res);
                if (res.ResultNo == 0)
                {
                    object[] obbj = new object[1];

                    obbj[0] = pParam[0];

                    res.Param = obbj;
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
        #region [ DB204004 - Гэрээний үндсэн бүртгэл засварлах ]
        public static Result DB204004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ContractMain SET
ContractType=:2, CustNo=:3, ValidStartDate=:4, ValidStartTime=:5, ValidEndDate=:6, ValidEndTime=:7, Amount=:8, Balance=:9, CurCode=:10,
PersonCount=:11, DepFreq=:12, DepAmount=:13, Status=:14, CreateDate=:15, CreatePostDate=:16, CreateUser=:17, OwnerUser=:18, Rebateid=:19, Loyalid=:20,
Pointid=:21, BalanceType=:22, Vat=:23, Accountno=:24, IncomeAccountno=:25, BalanceMethod=:26, DepBalance=:27
WHERE ContractNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204004", pParam);

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
        #region [ DB204005 - Гэрээний үндсэн бүртгэл устгах ]
        public static Result DB204005(DbConnections pDB, string pContractNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ContractMain WHERE ContractNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204005", pContractNo);

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

        #region [ DB204006 - Гэрээнд хамрагдах бүтээгдэхүүн жагсаалт авах ]
        public static Result DB204006(DbConnections pDB, string pContractNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select a.ContractNo, a.ProdNo, c.name as ProdName, a.ProdType, decode(a.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as ProdTypeName, a.price
from ContractProd a
left join (select InvId as prodno, InvType as prodtype, Name from invmain) c on a.prodno=c.prodno
where a.ContractNo=:1 and a.ProdType=0
union
select a.ContractNo, a.ProdNo, c.name as ProdName, a.ProdType, decode(a.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as ProdTypeName, a.price
from ContractProd a
left join (select ServId as prodno, ServType as prodtype, Name from servmain) c on a.prodno=c.prodno
where a.ContractNo=:1 and a.ProdType=1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204006", pContractNo);

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
        #region [ DB204007 - Гэрээнд хамрагдах бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204007(DbConnections pDB, string pContractNo, string pProdNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.ContractNo, a.ProdNo, c.name as ProdName, a.ProdType, decode(a.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as ProdTypeName, a.price
from ContractProd a
left join (select InvId as prodno, InvType as prodtype, Name from invmain) c on a.prodno=c.prodno
where a.ContractNo=:1 and a.ProdType=0 and a.ProdNo=:2
union
select a.ContractNo, a.ProdNo, c.name as ProdName, a.ProdType, decode(a.ProdType, 0, 'Бараа', 1, 'Үйлчилгээ') as ProdTypeName, a.price
from ContractProd a
left join (select ServId as prodno, ServType as prodtype, Name from servmain) c on a.prodno=c.prodno
where a.ContractNo=:1 and a.ProdType=1 and a.ProdNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204007", pContractNo, pProdNo);

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
        #region [ DB204008 - Гэрээнд хамрагдах бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB204008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO ContractProd(ContractNo, ProdNo, ProdType, price)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204008", pParam);
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
        #region [ DB204009 - Гэрээнд хамрагдах бүтээгдэхүүн засварлах ]
        public static Result DB204009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ContractProd SET
ProdType=:3, price=:4
WHERE ContractNo=:1 and ProdNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204009", pParam);

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
        #region [ DB204010 - Гэрээнд хамрагдах бүтээгдэхүүн устгах ]
        public static Result DB204010(DbConnections pDB, string pContractNo, int pProdType, string pProdNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ContractProd WHERE ContractNo=:1 and ProdType=:2 and ProdNo=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204010", pContractNo, pProdType, pProdNo);

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

        #region [ DB204011 - Гэрээний төлбөрийн төрөл ба дансны бүртгэл жагсаалт авах ]
        public static Result DB204011(DbConnections pDB, string pContractNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select a.ContractNo, a.PayType, pt.name as PayTypeName,
a.AccountNo, a.AccountName
from ContractAcnt a
left join papaytype pt on a.PayType=pt.TYPEID
where a.ContractNo=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204011", pContractNo);

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
        #region [ DB204012 - Гэрээний төлбөрийн төрөл ба дансны бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204012(DbConnections pDB, string pContractNo, string pPayType, string pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.ContractNo, a.PayType, pt.name as PayTypeName,
a.AccountNo, a.AccountName
from ContractAcnt a
left join papaytype pt on a.PayType=pt.TYPEID
where a.ContractNo=:1 and a.PayType=:2 and a.AccountNo=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204012", pContractNo, pPayType, pAccountNo);

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
        #region [ DB204013 - Гэрээний төлбөрийн төрөл ба дансны бүртгэл шинээр нэмэх ]
        public static Result DB204013(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO ContractAcnt(ContractNo, PayType, AccountNo, AccountName)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204013", pParam);
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
        #region [ DB204014 - Гэрээний төлбөрийн төрөл ба дансны бүртгэл засварлах ]
        public static Result DB204014(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ContractAcnt SET
AccountName=:4
WHERE ContractNo=:1 and PayType=:2 and AccountNo=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204014", pParam);

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
        #region [ DB204015 - Гэрээний төлбөрийн төрөл ба дансны бүртгэл устгах ]
        public static Result DB204015(DbConnections pDB, string pContractNo, string pPayType, string pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ContractAcnt WHERE ContractNo=:1 and PayType=:2 and AccountNo=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204015", pContractNo, pPayType, pAccountNo);

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

        #region [ DB204016 - Гэрээний дүнг элэгдүүлэх хуваарь жагсаалт авах ]
        public static Result DB204016(DbConnections pDB, string pContractNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select ContractNo, Day, Amount
from DepSchedule
where ContractNo=:1
order by day
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204016", pContractNo);

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
        #region [ DB204017 - Гэрээний дүнг элэгдүүлэх хуваарь дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204017(DbConnections pDB, string pContractNo, DateTime pDay)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ContractNo, Day, Amount
from DepSchedule
where ContractNo=:1 and Day=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204017", pContractNo, pDay);

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
        #region [ DB204018 - Гэрээний дүнг элэгдүүлэх хуваарь шинээр нэмэх ]
        public static Result DB204018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO DepSchedule(ContractNo, Day, Amount)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204018", pParam);
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
        #region [ DB204019 - Гэрээний дүнг элэгдүүлэх хуваарь засварлах ]
        public static Result DB204019(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE DepSchedule SET
Amount=:3
WHERE ContractNo=:1 and Day=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204019", pParam);

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
        #region [ DB204020 - Гэрээний дүнг элэгдүүлэх хуваарь устгах ]
        public static Result DB204020(DbConnections pDB, string pContractNo, DateTime pDay)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM DepSchedule WHERE ContractNo=:1 and Day=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204020", pContractNo, pDay);

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
        #region [ DB204021 - Тухайн гэрээний бүх элэгдүүлэх хуваарь устгах ]
        public static Result DB204021(DbConnections pDB, string pContractNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM DepSchedule WHERE ContractNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204021", pContractNo);

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

        #region [ DB204022 - Гэрээний үндсэн мэдээлэл xls - ээс оруулах ]
        public static Result DB204022(DbConnections pDB, object[] obj, int flag, string contractno)
        {
            Result res = new Result();
            try
            {
                if (flag == 0)
                {
                    string seq = "";
                    #region [ ContractNo ]
                    //[T02][Y04][C02][P04][R03][S06]	YCPTRS	"Y=Year; C=CurrencyCode; P=MemberType; T=ContractType; R=Random number; S=Sequence (CODE=ContractType)"

                    Core.AutoNumEnum enums = new Core.AutoNumEnum();
                    //enums.A = "0";
                    //enums.P = Static.ToStr(obj[1]);
                    enums.Y = Static.ToStr(Static.ToDate(obj[21]).Year);
                    enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(obj[8])).CurrencyCode);
                    //enums.P = Static.ToStr(pParam[1]);
                    enums.T = Static.ToStr(obj[1]);
                    Random rnd = new Random();
                    enums.R = Static.ToStr(rnd.Next(1, 2));
                    Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 3, enums.T, enums);
                    //Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 19, enums);
                    if (seqres.ResultNo == 0)
                    {
                        seq = Static.ToStr(seqres.ResultDesc);
                        if (seq == "")
                        {
                            seqres.ResultNo = 9110068;
                            seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:3][" + seqres.ResultDesc + "]";
                            return seqres;
                        }
                    }
                    else
                        return seqres;

                    obj[0] = seq;
                    #endregion
                }
                else
                {
                    obj[0] = contractno;
                }
                string sql =
@"INSERT INTO ContractMain(ContractNo, ContractType, CustNo, ValidStartDate, ValidStartTime, ValidEndDate, ValidEndTime, Amount, CurCode, BalanceType, 
PersonCount, DepFreq, DepAmount, OwnerUser, Rebateid, Loyalid, Pointid, Vat, AccountNo, IncomeAccountNo, 
BALANCEMETHOD, CreateDate, CreatePostDate, CreateUser, Status, Balance, DepBalance)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23, :24, :25, :26, :27)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204022", obj);
                res = F_Error(res);
                if (res.ResultNo == 0)
                {
                    object[] obbj = new object[1];

                    obbj[0] = obbj;

                    res.Param = obbj;
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
        #region [ DB204023 - Гэрээнд хамаарагдах бүтээгдэхүүнүүд,  Гэрээний төлбөрийн төрөл ба дансны бүртгэл, Гэрээний дүнг элэгдүүлэх хуваарь ]
        public static Result DB204023(DbConnections pDB, object[] obj)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CONTRACTPROD(ContractNo, ProdNo, ProdType, Price)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204023", obj);
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
        #region [ DB204024 - Гэрээний төлбөрийн төрөл ба дансны бүртгэл, Гэрээний дүнг элэгдүүлэх хуваарь ]
        public static Result DB204024(DbConnections pDB, object[] obj)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO CONTRACTACNT(ContractNo, PayType, AccountNo, AccountName)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204024", obj);
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
        #region [ DB204025 - Гэрээний дүнг элэгдүүлэх хуваарь ]
        public static Result DB204025(DbConnections pDB, object[] obj)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO DEPSCHEDULE(ContractNo, Day, Amount)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204025", obj);
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

        #endregion

        #region [ Orders ]
        #region [ DB204101 - Захиалгын үндсэн бүртгэл жагсаалт авах ]
        public static Result DB204101(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                string[] fieldnames = new string[] { "OrderNo like","CustNo like","CustomerName like",
"ConfirmTerm","TermType","OrderAmount","PrepaidAmount",
"CurCode","Fee","StartDate","EndDate","PersonCount",
"Status","CreateDate","PostDate","CreateUser","OwnerUser"};

                ArrayList dbparam = new ArrayList(fieldnames.Length);
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                #region [Auto Condition]
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
                #endregion

                sql = string.Format(
@"select *
from V_ORDERLIST
{0} {1} ", sb.Length > 0 ? "where" : " order by ORDERNO desc", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB204101", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB204102 - Захиалгын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204102(DbConnections pDB, string pOrderNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select *
from V_ORDERLIST
where OrderNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204102", pOrderNo);

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
        #region [ DB204103 - Захиалгын үндсэн бүртгэл шинээр нэмэх ]
        public static Result DB204103(DbConnections pDB, object[] pParam, int flag, string orderno)
        {
            Result res = new Result();
            try
            {
                if (flag == 0)
                {
                    int ImplementYear = Core.SystemProp.ImplementYear;
                    long seq = 0;
                    #region [ OrderNo ]
                    //Y=Year; C=CurrencyCode; M=Month; D=Day; Q=Quarter; Z=Business session; S=Sequence

                    Core.AutoNumEnum enums = new Core.AutoNumEnum();
                    enums.Y = Static.ToStr(Static.ToDate(pParam[7]).Year);
                    enums.M = Static.ToStr(Static.ToDate(pParam[7]).Month);
                    enums.D = Static.ToStr(Static.ToDate(pParam[7]).Day);
                    enums.Q = Static.ToStr(Math.Round(Static.ToDecimal(Static.ToDate(pParam[7]).Month / 3), 0, MidpointRounding.ToEven));
                    enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[21])).CurrencyCode);
                    enums.Z = Static.ToStr(Static.ToDate(pParam[7]).Year - ImplementYear);
                    Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 4, "", enums);
                    if (seqres.ResultNo == 0)
                    {
                        seq = Static.ToLong(seqres.ResultDesc);
                        if (seq == 0)
                        {
                            seqres.ResultNo = 9110068;
                            seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:4][" + seqres.ResultDesc + "]";
                            return seqres;
                        }
                    }
                    else
                        return seqres;

                    pParam[0] = seq;
                    #endregion
                }
                else
                {
                    pParam[0] = orderno;
                }


                string sql =
@"INSERT INTO orders(OrderNo, ChannelID, OrderName, OrderContactInfo, UserID, CustNo, OrderType, CreateDate, Status, CreateUser, 
SalesUser, PersonCount, StartDateTime, EndDateTime, GraceHoursStart, GraceHoursEnd, OrderAmount, OrderAmountMin, OrderAmountMax, OrderBalance, 
PrepaidAmount, CurCode, PriceType, DiscountID, DiscountType, DicountAmount, CancelDateTime, CancelNote, CancelUserNo, ExpireDateTime, 
ExpireNote, ExpireUserNo, ConfirmDateTime, ConfirmNote, ConfirmUserNo, SaleDateTime, SalesNo, ContractNo)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20, 
:21, :22, :23, :24, :25, :26, :27, :28, :29, :30,
:31, :32, :33, :34, :35, :36, :37, :38
)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204103", pParam);
                res = F_Error(res);
                if (res.ResultNo == 0)
                {
                    object[] obbj = new object[1];

                    obbj[0] = pParam[0];

                    res.Param = obbj;
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
        #region [ DB204104 - Захиалгын үндсэн бүртгэл засварлах ]
        public static Result DB204104(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE orders SET
ChannelID=:2, OrderName=:3, OrderContactInfo=:4, UserID=:5, 
CustNo=:6, OrderType=:7, CreateDate=:8, Status=:9, CreateUser=:10, 
SalesUser=:11, PersonCount=:12, StartDateTime=:13, EndDateTime=:14, GraceHoursStart=:15, 
GraceHoursEnd=:16, OrderAmount=:17, OrderAmountMin=:18, OrderAmountMax=:19, OrderBalance=:20, 
PrepaidAmount=:21, CurCode=:22, PriceType=:23, DiscountID=:24, DiscountType=:25, 
DicountAmount=:26, CancelDateTime=:27, CancelNote=:28, CancelUserNo=:29, ExpireDateTime=:30, 
ExpireNote=:31, ExpireUserNo=:32, ConfirmDateTime=:33, ConfirmNote=:34, ConfirmUserNo=:35, 
SaleDateTime=:36, SalesNo=:37, ContractNo=:38
WHERE OrderNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204104", pParam);

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
        #region [ DB204105 - Захиалгын үндсэн бүртгэл устгах ]
        public static Result DB204105(DbConnections pDB, string pOrderNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM orders WHERE OrderNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204105", pOrderNo);

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

        #region [ DB204106 - Захиалгад орсон үйлчлүүлэгчийн бүртгэл жагсаалт авах ]
        public static Result DB204106(DbConnections pDB, string pOrderNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select OrderNo, ItemNo, RegisterNo, FirstName, LastName, 
MiddleName, Sex, BirthDay, Email, Mobile, 
Company, CountryCode, Height, FootSize, SerialNo
from OrderPersonal
where orderno=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204106", pOrderNo);

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
        #region [ DB204107 - Захиалгад орсон үйлчлүүлэгийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204107(DbConnections pDB, string pOrderNo, long pItemNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select OrderNo, ItemNo, RegisterNo, FirstName, LastName, 
MiddleName, Sex, BirthDay, Email, Mobile, 
Company, CountryCode, Height, FootSize, SerialNo
from OrderPersonal
where orderno=:1 and ItemNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204107", pOrderNo, pItemNo);

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
        #region [ DB204108 - Захиалгад орсон үйлчлүүлэгийн бүртгэл шинээр нэмэх ]
        public static Result DB204108(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO OrderPersonal(OrderNo, ItemNo, RegisterNo, FirstName, LastName, 
MiddleName, Sex, BirthDay, Email, Mobile, 
Company, CountryCode, Height, FootSize, SerialNo)
VALUES(:1, :2, :3, :4, :5, 
:6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204108", pParam);
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
        #region [ DB204109 - Захиалгад орсон үйлчлүүлэгийн бүртгэл засварлах ]
        public static Result DB204109(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE OrderPersonal SET
RegisterNo=:3, FirstName=:4, LastName=:5, 
MiddleName=:6, Sex=:7, BirthDay=:8, Email=:9, Mobile=:10, 
Company=:11, CountryCode=:12, Height=:13, FootSize=:14, SerialNo=:15
WHERE orderno=:1 and ItemNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204109", pParam);

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
        #region [ DB204110 - Захиалгад орсон үйлчлүүлэгийн бүртгэл устгах ]
        public static Result DB204110(DbConnections pDB, string pOrderNo, long pItemNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM OrderPersonal WHERE OrderNo=:1 AND ItemNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204110", pOrderNo, pItemNo);

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

        #region [ DB204111 - Захиалга доторх багц дахь бүтээгдэхүүн жагсаалт авах ]
        public static Result DB204111(DbConnections pDB, string pOrderNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select o.OrderNo, o.ProdNo, im.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, o.Qty, o.QtyMin, o.QtyMax, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProduct o
left join invmain im on o.prodno=im.invid
where prodtype=0 and orderno=:1
union
select o.OrderNo, o.ProdNo, sm.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, o.Qty, o.QtyMin, o.QtyMax, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProduct o
left join servmain sm on o.prodno=sm.servid
where prodtype=1 and orderno=:1
union
select o.OrderNo, o.ProdNo, pm.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, o.Qty, o.QtyMin, o.QtyMax, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProduct o
left join packagemain pm on o.prodno=pm.packageid
where prodtype=2 and orderno=:1
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204111", pOrderNo);

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
        #region [ DB204112 - Захиалга доторх багц дахь бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204112(DbConnections pDB, string pOrderNo, int pProdType, string pProdNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select o.OrderNo, o.ProdNo, im.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, o.Qty, o.QtyMin, o.QtyMax, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProduct o
left join invmain im on o.prodno=im.invid
where prodtype=0 and orderno=:1 and o.ProdType=:2 and o.ProdNo=:3
union
select o.OrderNo, o.ProdNo, sm.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, o.Qty, o.QtyMin, o.QtyMax, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProduct o
left join servmain sm on o.prodno=sm.servid
where prodtype=1 and orderno=:1 and o.ProdType=:2 and o.ProdNo=:3
union
select o.OrderNo, o.ProdNo, pm.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, o.Qty, o.QtyMin, o.QtyMax, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProduct o
left join packagemain pm on o.prodno=pm.packageid
where prodtype=2 and orderno=:1 and o.ProdType=:2 and o.ProdNo=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204112", pOrderNo, pProdType, pProdNo);

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
        #region [ DB204113 - Захиалга доторх багц дахь бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB204113(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO OrderProduct(OrderNo, ProdNo, ProdType, Qty, QtyMin, QtyMax, DiscountType, DiscountAmount, Price)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204113", pParam);
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
        #region [ DB204114 - Захиалга доторх багц дахь бүтээгдэхүүн засварлах ]
        public static Result DB204114(DbConnections pDB, object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {

                object[] obj = new object[11];

                obj[0] = pOldParam[0];
                obj[1] = pOldParam[1];
                obj[2] = pOldParam[2];

                obj[3] = pNewParam[1];
                obj[4] = pNewParam[2];
                obj[5] = pNewParam[3];
                obj[6] = pNewParam[4];
                obj[7] = pNewParam[5];
                obj[8] = pNewParam[6];
                obj[9] = pNewParam[7];
                obj[10] = pNewParam[8];

                string sql =
@"UPDATE OrderProduct SET
ProdNo=:4, ProdType=:5, Qty=:6, QtyMin=:7, QtyMax=:8, DiscountType=:9, DiscountAmount=:10, Price=:11
WHERE OrderNo=:1 and ProdType=:2 and ProdNo=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204114", obj);

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
        #region [ DB204115 - Захиалга доторх багц дахь бүтээгдэхүүн устгах ]
        public static Result DB204115(DbConnections pDB, string pOrderNo, int pProdType, string pProdNo)
        {
            Result res = new Result();
            try
            {
                string sql = "";

                sql =
@"select orderno FROM OrderProductPrice WHERE OrderNo=:1 and ProdType=:2 and ProdNo=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204115", pOrderNo, pProdType, pProdNo);

                if (res.ResultNo != 0)
                {
                    res.ResultDesc = "Захиалга доторх багц дахь бүтээгдэхүүний үнэ шалгах үед алдаа гарлаа. " + res.ResultDesc;
                    res.ResultNo = 10;
                    return res;
                }

                if (res.AffectedRows != 0)
                {
                    res.ResultDesc = "Энэ бүтээгдэхүүн дээр үнэ тохируулсан байгаа тул устгах боломжгүй. [Захиалга доторх багц дахь бүтээгдэхүүний үнээ шалгана уу]";
                    res.ResultNo = 10;
                    return res;
                }

                sql = 
@"DELETE FROM OrderProduct WHERE OrderNo=:1 and ProdType=:2 and ProdNo=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204115", pOrderNo, pProdType, pProdNo);

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

        #region [ DB204116 - Захиалга доторх багц дахь бүтээгдэхүүний үнийн бүртгэл жагсаалт авах ]
        public static Result DB204116(DbConnections pDB, string pOrderNo, string pProdNo, int pProdType)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select o.OrderNo, o.ProdNo, im.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, 
o.PriceTypeID, pp.name as PriceTypeIDName, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProductPrice o
left join invmain im on o.prodno=im.invid
left join papricetype pp on o.PriceTypeID=pp.PriceTypeID
where o.prodtype=0 and o.orderno=:1 and o.ProdNo=:2 and o.ProdType=:3
union
select o.OrderNo, o.ProdNo, im.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, 
o.PriceTypeID, pp.name as PriceTypeIDName, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProductPrice o
left join servmain im on o.prodno=im.servid
left join papricetype pp on o.PriceTypeID=pp.PriceTypeID
where o.prodtype=1 and o.orderno=:1 and o.ProdNo=:2 and o.ProdType=:3
union
select o.OrderNo, o.ProdNo, im.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, 
o.PriceTypeID, pp.name as PriceTypeIDName, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProductPrice o
left join packagemain im on o.prodno=im.packageid
left join papricetype pp on o.PriceTypeID=pp.PriceTypeID
where o.prodtype=2 and o.orderno=:1 and o.ProdNo=:2 and o.ProdType=:3
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204116", pOrderNo, pProdNo, pProdType);

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
        #region [ DB204117 - Захиалга доторх багц дахь бүтээгдэхүүний үнийн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204117(DbConnections pDB, string pOrderNo, string pProdNo, int pProdType, string pPriceTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select o.OrderNo, o.ProdNo, im.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, 
o.PriceTypeID, pp.name as PriceTypeIDName, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProductPrice o
left join invmain im on o.prodno=im.invid
left join papricetype pp on o.PriceTypeID=pp.PriceTypeID
where o.prodtype=0 and o.orderno=:1 and o.ProdNo=:2 and o.ProdType=:3 and o.PriceTypeID=:4
union
select o.OrderNo, o.ProdNo, im.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, 
o.PriceTypeID, pp.name as PriceTypeIDName, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProductPrice o
left join servmain im on o.prodno=im.servid
left join papricetype pp on o.PriceTypeID=pp.PriceTypeID
where o.prodtype=1 and o.orderno=:1 and o.ProdNo=:2 and o.ProdType=:3 and o.PriceTypeID=:4
union
select o.OrderNo, o.ProdNo, im.name as prodnoname, o.ProdType, decode(o.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ', 2, 'БАГЦ') as ProdTypeName, 
o.PriceTypeID, pp.name as PriceTypeIDName, o.DiscountType, 
decode(o.DiscountType, 0, 'Хөнгөлөлт байхгүй', 1, 'Хувиар хөнгөлөнө', 2, 'Дүнгээр хөнгөлөнө') as DiscountTypeName, o.DiscountAmount, o.Price
from OrderProductPrice o
left join packagemain im on o.prodno=im.packageid
left join papricetype pp on o.PriceTypeID=pp.PriceTypeID
where o.prodtype=2 and o.orderno=:1 and o.ProdNo=:2 and o.ProdType=:3 and o.PriceTypeID=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204117", pOrderNo, pProdNo, pProdType, pPriceTypeID);

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
        #region [ DB204118 - Захиалга доторх багц дахь бүтээгдэхүүний үнийн бүртгэл шинээр нэмэх ]
        public static Result DB204118(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO OrderProductPrice(OrderNo, ProdNo, ProdType, PriceTypeID, DiscountType, DiscountAmount, Price)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204118", pParam);
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
        #region [ DB204119 - Захиалга доторх багц дахь бүтээгдэхүүний үнийн бүртгэл засварлах ]
        public static Result DB204119(DbConnections pDB, object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[8];

                obj[0] = pOldParam[0];
                obj[1] = pOldParam[1];
                obj[2] = pOldParam[2];
                obj[3] = pOldParam[3];

                obj[4] = pNewParam[3];
                obj[5] = pNewParam[4];
                obj[6] = pNewParam[5];
                obj[7] = pNewParam[6];

                string sql =
@"UPDATE OrderProductPrice SET
PriceTypeID=:5, DiscountType=:6, DiscountAmount=:7, Price=:8
WHERE orderno=:1 and ProdNo=:2 and ProdType=:3 and PriceTypeID=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204119", obj);

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
        #region [ DB204120 - Захиалга доторх багц дахь бүтээгдэхүүний үнийн бүртгэл устгах ]
        public static Result DB204120(DbConnections pDB, string pOrderNo, string pProdNo, int pProdType, string pPriceTypeID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM OrderProductPrice WHERE orderno=:1 and ProdNo=:2 and ProdType=:3 and PriceTypeID=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204120", pOrderNo, pProdNo, pProdType, pPriceTypeID);

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

        #region [ DB204121 - Захиалгын хүснэгт жагсаалт авах ]
        public static Result DB204121(DbConnections pDB, int pProdType, string ProdNo, DateTime pStartDate, DateTime pEndDate)
        {
            Result res = new Result();
            try
            {
                string sql;

                if (pEndDate.Year == 1)
                {
                    sql =
@"select a.ProdType, decode(a.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, a.ProdNo, im.name as ProdNoName, a.LineNumber,
a.OrderNo, a.SalesNo, a.StartDate, a.EndDate, a.Status,
decode(a.Status, 0, 'ЗАХИАЛГА ХИЙЖ БАЙГАА', 1, 'ЗАХИАЛСАН', 2, 'ЗАХИАЛГА БАТАЛГААЖСАН', 3, 'ЦУЦЛАГДСАН', 4, 'ХААГДСАН') as StatusName
from ProdTimeSheet a
left join invmain im on a.prodno=im.invid
where a.ProdType=0
and a.ProdType=:1 and a.ProdNo=:2 and a.StartDate>=:3
union
select a.ProdType, decode(a.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, a.ProdNo, im.name as ProdNoName, a.LineNumber,
a.OrderNo, a.SalesNo, a.StartDate, a.EndDate, a.Status,
decode(a.Status, 0, 'ЗАХИАЛГА ХИЙЖ БАЙГАА', 1, 'ЗАХИАЛСАН', 2, 'ЗАХИАЛГА БАТАЛГААЖСАН', 3, 'ЦУЦЛАГДСАН', 4, 'ХААГДСАН') as StatusName
from ProdTimeSheet a
left join servmain im on a.prodno=im.servid
where a.ProdType=1
and a.ProdType=:1 and a.ProdNo=:2 and a.StartDate>=:3
";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204121", pProdType, ProdNo, pStartDate);
                }
                else
                {
                    sql =
@"select a.ProdType, decode(a.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, a.ProdNo, im.name as ProdNoName, a.LineNumber,
a.OrderNo, a.SalesNo, a.StartDate, a.EndDate, a.Status,
decode(a.Status, 0, 'ЗАХИАЛГА ХИЙЖ БАЙГАА', 1, 'ЗАХИАЛСАН', 2, 'ЗАХИАЛГА БАТАЛГААЖСАН', 3, 'ЦУЦЛАГДСАН', 4, 'ХААГДСАН') as StatusName
from ProdTimeSheet a
left join invmain im on a.prodno=im.invid
where a.ProdType=0
and a.ProdType=:1 and a.ProdNo=:2 and a.StartDate between :3 and :4
union
select a.ProdType, decode(a.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, a.ProdNo, im.name as ProdNoName, a.LineNumber,
a.OrderNo, a.SalesNo, a.StartDate, a.EndDate, a.Status,
decode(a.Status, 0, 'ЗАХИАЛГА ХИЙЖ БАЙГАА', 1, 'ЗАХИАЛСАН', 2, 'ЗАХИАЛГА БАТАЛГААЖСАН', 3, 'ЦУЦЛАГДСАН', 4, 'ХААГДСАН') as StatusName
from ProdTimeSheet a
left join servmain im on a.prodno=im.servid
where a.ProdType=1
and a.ProdType=:1 and a.ProdNo=:2 and a.StartDate between :3 and :4
";

                    res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204121", pProdType, ProdNo, pStartDate, pEndDate);
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
        #region [ DB204122 - Захиалгын хүснэгт дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204122(DbConnections pDB, int pProdType, string pProdNo, long pLineNumber, string pOrderNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.ProdType, decode(a.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, a.ProdNo, im.name as ProdNoName, a.LineNumber, a.OrderNo,
a.SalesNo, a.StartDate, a.EndDate, a.Status,
decode(a.Status, 0, 'ЗАХИАЛГА ХИЙЖ БАЙГАА', 1, 'ЗАХИАЛСАН', 2, 'ЗАХИАЛГА БАТАЛГААЖСАН', 3, 'ЦУЦЛАГДСАН', 4, 'ХААГДСАН') as StatusName
from ProdTimeSheet a
left join invmain im on a.prodno=im.invid
where a.ProdType=0
and a.ProdType=:1 and a.ProdNo=:2 and a.LineNumber=:3 and a.OrderNo=:4 
union
select a.ProdType, decode(a.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, a.ProdNo, im.name as ProdNoName, a.LineNumber, a.OrderNo,
a.SalesNo, a.StartDate, a.EndDate, a.Status,
decode(a.Status, 0, 'ЗАХИАЛГА ХИЙЖ БАЙГАА', 1, 'ЗАХИАЛСАН', 2, 'ЗАХИАЛГА БАТАЛГААЖСАН', 3, 'ЦУЦЛАГДСАН', 4, 'ХААГДСАН') as StatusName
from ProdTimeSheet a
left join servmain im on a.prodno=im.servid
where a.ProdType=1
and a.ProdType=:1 and a.ProdNo=:2 and a.LineNumber=:3 and a.OrderNo=:4 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204122", pProdType, pProdNo, pLineNumber, pOrderNo);

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
        #region [ DB204123 - Захиалгын хүснэгт шинээр нэмэх ]
        public static Result DB204123(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO ProdTimeSheet(ProdType, ProdNo, LineNumber, OrderNo, SalesNo,
StartDate, EndDate, Status)
VALUES(:1, :2, :3, :4, :5,
:6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204123", pParam);
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
        #region [ DB204124 - Захиалгын хүснэгт засварлах ]
        public static Result DB204124(DbConnections pDB, object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {

                //ProdType-0, ProdNo-1, LineNumber-2, OrderNo-3, SalesNo-4, StartDate-5,
                //EndDate-6, Status-7

                object[] obj = new object[12];

                obj[0] = pOldParam[0];
                obj[1] = pOldParam[1];
                obj[2] = pOldParam[2];
                obj[3] = pOldParam[3];
                obj[4] = pOldParam[4];
                obj[5] = pOldParam[5];
                obj[6] = pNewParam[2];
                obj[7] = pNewParam[3];
                obj[8] = pNewParam[4];
                obj[9] = pNewParam[5];
                obj[10] = pNewParam[6];
                obj[11] = pNewParam[7];

                string sql =
@"UPDATE ProdTimeSheet SET
LineNumber=:7, OrderNo=:8, SalesNo=:9, StartDate=:10, EndDate=:11, Status=:12
WHERE ProdType=:1 and ProdNo=:2 and LineNumber=:3 and OrderNo=:4 and SalesNo=:5 and StartDate=:6";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204124", obj);

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
        #region [ DB204125 - Захиалгын хүснэгт устгах ]
        public static Result DB204125(DbConnections pDB, int pProdType, string pProdNo, long pLineNumber, string pOrderNo, string pSalesNo, DateTime pStartDate)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ProdTimeSheet WHERE ProdType=:1 and ProdNo=:2 and LineNumber=:3 and OrderNo=:4 and SalesNo=:5 and StartDate=:6";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204125", pProdType, pProdNo, pLineNumber, pOrderNo, pSalesNo, pStartDate);

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

        #region [ DB204126 - Захиалга баталгаажуулах ]
        public static Result DB204126(DbConnections pDB, string pOrderNo, int pUserNo, string pNote, DateTime pConfirmDateTime)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE orders
SET 
CONFIRMUSERNO=:2, 
CONFIRMNOTE=:3
CONFIRMDATETIME=:4, 
status=1
WHERE orderno=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204126", pOrderNo, pUserNo, pNote, pConfirmDateTime);

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
        #region [ DB204127 - Захиалга цуцлах ]
        public static Result DB204127(DbConnections pDB, string pOrderNo, int pUserNo, string pNote, DateTime pConfirmDateTime)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE orders
SET 
CANCELUSERNO=:2, 
CANCELNOTE=:3
CANCELDATETIME=:4, 
status=8
WHERE orderno=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204127", pOrderNo, pUserNo, pNote, pConfirmDateTime);

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
        #region [ DB204128 - Захиалга сэргээх ]
        public static Result DB204128(DbConnections pDB, string pOrderNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE orders
SET status=0
WHERE orderno=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204128", pOrderNo);

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

        #region [ DB204150 - Захиалгад орсон үйлчлүүлэгчийн захиалсан бүтээгдэхүүн бүртгэл жагсаалт авах ]
        public static Result DB204150(DbConnections pDB, string pOrderNo, long pItemNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select opp.OrderNo, opp.ItemNo, opp.ProdNo, im.name, opp.ProdType, opp.Qty
from OrderPersonalProduct opp
left join invmain im on opp.prodno=IM.INVID
where opp.prodtype=0 and opp.orderno=:1 and opp.itemno=:2
union
select opp.OrderNo, opp.ItemNo, opp.ProdNo, sm.name, opp.ProdType, opp.Qty
from OrderPersonalProduct opp
left join servmain sm on opp.prodno=sm.servid
where opp.prodtype=1 and opp.orderno=:1 and opp.itemno=:2
union
select opp.OrderNo, opp.ItemNo, opp.ProdNo, pm.name, opp.ProdType, opp.Qty
from OrderPersonalProduct opp
left join packagemain pm on opp.prodno=pm.packageid
where opp.prodtype=2 and opp.orderno=:1 and opp.itemno=:2
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204150", pOrderNo, pItemNo);

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
        #region [ DB204151 - Захиалгад орсон үйлчлүүлэгийн захиалсан бүтээгдэхүүн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204151(DbConnections pDB, string pOrderNo, long pItemNo, int pProdType, string pProdNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select opp.OrderNo, opp.ItemNo, opp.ProdNo, im.name, opp.ProdType, opp.Qty
from OrderPersonalProduct opp
left join invmain im on opp.prodno=IM.INVID
where opp.prodtype=0 and opp.orderno=:1 and opp.itemno=:2 and opp.ProdType=:3 and opp.ProdNo=:4
union
select opp.OrderNo, opp.ItemNo, opp.ProdNo, sm.name, opp.ProdType, opp.Qty
from OrderPersonalProduct opp
left join servmain sm on opp.prodno=sm.servid
where opp.prodtype=1 and opp.orderno=:1 and opp.itemno=:2 and opp.ProdType=:3 and opp.ProdNo=:4
union
select opp.OrderNo, opp.ItemNo, opp.ProdNo, pm.name, opp.ProdType, opp.Qty
from OrderPersonalProduct opp
left join packagemain pm on opp.prodno=pm.packageid
where opp.prodtype=2 and opp.orderno=:1 and opp.itemno=:2 and opp.ProdType=:3 and opp.ProdNo=:4
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204151", pOrderNo, pItemNo, pProdType, pProdNo);

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
        #region [ DB204152 - Захиалгад орсон үйлчлүүлэгийн захиалсан бүтээгдэхүүн бүртгэл шинээр нэмэх ]
        public static Result DB204152(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO OrderPersonalProduct(OrderNo, ItemNo, ProdNo, ProdType, Qty)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204152", pParam);
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
        #region [ DB204153 - Захиалгад орсон үйлчлүүлэгийн захиалсан бүтээгдэхүүн бүртгэл засварлах ]
        public static Result DB204153(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE OrderPersonalProduct SET
ProdNo=:3, ProdType=:4, Qty=:5
WHERE orderno=:1 and ItemNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204153", pParam);

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
        #region [ DB204154 - Захиалгад орсон үйлчлүүлэгийн захиалсан бүтээгдэхүүн бүртгэл устгах ]
        public static Result DB204154(DbConnections pDB, string pOrderNo, long pItemNo, int pProdType, string pProdNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM OrderPersonalProduct WHERE OrderNo=:1 AND ItemNo=:2 and ProdType=:3 and ProdNo=:4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204154", pOrderNo, pItemNo, pProdType, pProdNo);

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

        #region [ PreSale ]

        #region [ DB204301 - Урьдчилсан борлуулалтын үндсэн бүртгэл жагсаалт авах ]
        public static Result DB204301(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                string[] fieldnames = new string[] { "presaleno like","CustNo like","CustomerName like",
"ConfirmTerm","TermType","OrderAmount","PrepaidAmount",
"CurCode","Fee","StartDate","EndDate","PersonCount",
"Status","CreateDate","PostDate","CreateUser","OwnerUser"};

                ArrayList dbparam = new ArrayList(fieldnames.Length);
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                #region [Auto Condition]
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
                #endregion

                sql = string.Format(
@"select *
from V_PRESALELIST
{0} {1} ", sb.Length > 0 ? "where" : " order by presaleno desc", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB204301", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB204302 - Урьдчилсан борлуулалтын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204302(DbConnections pDB, string ppresaleno)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select *
from V_PRESALELIST
where presaleno=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204302", ppresaleno);

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
        #region [ DB204303 - Урьдчилсан борлуулалтын үндсэн бүртгэл шинээр нэмэх ]
        public static Result DB204303(DbConnections pDB, object[] pParam, int flag, string presaleno)
        {
            Result res = new Result();
            try
            {
                string sql = "";
                if (flag == 0)
                {
                    int ImplementYear = Core.SystemProp.ImplementYear;
                    long seq = 0;
                    int autnumno = 3;
                    #region [ Урьдчилсан борлуулалт ]
                    Core.AutoNumEnum enums = new Core.AutoNumEnum();
                    enums.Y = Static.ToStr(Static.ToDate(pParam[6]).Year);
                    enums.M = Static.ToStr(Static.ToDate(pParam[6]).Month);
                    enums.D = Static.ToStr(Static.ToDate(pParam[6]).Day);

                    enums.Q = Static.ToStr(Math.Round(Static.ToDecimal(Static.ToDate(pParam[6]).Month / 3), 0, MidpointRounding.ToEven));

                    enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[23])).CurrencyCode);
                    enums.Z = Static.ToStr(Static.ToDate(pParam[6]).Year - ImplementYear);

                    if (Static.ToInt(pParam[1]) == 2) // vauchir
                    { 
                        //asd
                        sql = "select autonumno from presalemain where presaleprod=:1";
                        res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204303", Static.ToStr(pParam[4]));
                        if (res.ResultNo != 0)
                        {
                            res.ResultDesc = "УБ ын бүтээгдэхүүн сонгоход алдаа гарлаа. " + res.ResultDesc;
                            res.ResultNo = 10;
                            return res;
                        }

                        if (res.AffectedRows == 0)
                        {
                            res.ResultDesc = "УБ ын бүтээгдэхүүн олдсонгүй.";
                            res.ResultNo = 11;
                            return res;
                        }

                        autnumno = Static.ToInt(res.Data.Tables[0].Rows[0]["autonumno"]);
                    }

                    Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, autnumno, "", enums);
                    if (seqres.ResultNo == 0)
                    {
                        seq = Static.ToLong(seqres.ResultDesc);
                        if (seq == 0)
                        {
                            seqres.ResultNo = 9110068;
                            seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:" + autnumno + "][" + seqres.ResultDesc + "]";
                            return seqres;
                        }
                    }
                    else
                        return seqres;

                    pParam[0] = seq;
                    #endregion
                }
                else
                {
                    pParam[0] = presaleno;
                }

                sql =
@"INSERT INTO presale(PreSaleNo, PreSaleType, CustNo, ChannelID, PreSaleProd, UserID, CreateDate, Status, CreateUser, SalesUser, 
PersonCount, StartDateTime, EndDateTime, GraceHoursStart, GraceHoursEnd, PreSaleAmount, PreSaleAmountMin, PreSaleAmountMax, SaleAmount, AmartizationAmount, 
AmartizationType, AmartizationFreq, AmartizationMethod, CurCode, PriceType, DiscountID, DiscountType, DicountAmount, CancelDateTime, CancelNote, 
CancelUserNo, ExpireDateTime, ExpireNote, ExpireUserNo, ConfirmDateTime, ConfirmNote, ConfirmUserNo, ContractNo, SalesAccountNo, RefundAccountNo, 
DiscountAccountNo, BonusAccountNo, BonusExpAccountNo)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20, 
:21, :22, :23, :24, :25, :26, :27, :28, :29, :30,
:31, :32, :33, :34, :35, :36, :37, :38, :39, :40,
:41, :42, :43)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204303", pParam);
                res = F_Error(res);
                if (res.ResultNo == 0)
                {
                    object[] obbj = new object[1];

                    obbj[0] = pParam[0];

                    res.Param = obbj;
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
        #region [ DB204304 - Урьдчилсан борлуулалтын үндсэн бүртгэл засварлах ]
        public static Result DB204304(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE presale SET
PreSaleType=:2, CustNo=:3, ChannelID=:4, PreSaleProd=:5, UserID=:6, CreateDate=:7, Status=:8, CreateUser=:9, SalesUser=:10, 
PersonCount=:11, StartDateTime=:12, EndDateTime=:13, GraceHoursStart=:14, GraceHoursEnd=:15, PreSaleAmount=:16, PreSaleAmountMin=:17, PreSaleAmountMax=:18, SaleAmount=:19, AmartizationAmount=:20, 
AmartizationType=:21, AmartizationFreq=:22, AmartizationMethod=:23, CurCode=:24, PriceType=:25, DiscountID=:26, DiscountType=:27, DicountAmount=:28, CancelDateTime=:29, CancelNote=:30, 
CancelUserNo=:31, ExpireDateTime=:32, ExpireNote=:33, ExpireUserNo=:34, ConfirmDateTime=:35, ConfirmNote=:36, ConfirmUserNo=:37, ContractNo=:38, SalesAccountNo=:39, RefundAccountNo=:40, 
DiscountAccountNo=:41, BonusAccountNo=:42, BonusExpAccountNo=:43
WHERE PreSaleNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204304", pParam);

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
        #region [ DB204305 - Урьдчилсан борлуулалтын үндсэн бүртгэл устгах ]
        public static Result DB204305(DbConnections pDB, string pPreSaleNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM presale WHERE PreSaleNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204305", pPreSaleNo);

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

        #region [ DB204421 - Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн жагсаалт авах ]
        public static Result DB204421(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select p.PreSaleProd, p.name, p.Name2, p.Count, p.AutoNumNo, a.name as AutoNumNoName, p.SalesAccountNo, p.RefundAccountNo, p.DiscountAccountNo, p.BonusAccountNo,
p.BonusExpAccountNo
from PreSaleMain p
left join autonum a on p.autonumno=A.ID
";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204421", null);
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
        #region [ DB204422 - Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204422(DbConnections pDB, string pPreSaleProd)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select p.PreSaleProd, p.name, p.Name2, p.Count, p.AutoNumNo, a.name as AutoNumNoName, p.SalesAccountNo, p.RefundAccountNo, p.DiscountAccountNo, p.BonusAccountNo,
p.BonusExpAccountNo
from PreSaleMain p
left join autonum a on p.autonumno=A.ID 
where p.PreSaleProd=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204422", pPreSaleProd);

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
        #region [ DB204423 - Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн шинээр нэмэх ]
        public static Result DB204423(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO PreSaleMain(PreSaleProd, Name, Name2, Count, AutoNumNo, 
SalesAccountNo, RefundAccountNo, DiscountAccountNo, BonusAccountNo, BonusExpAccountNo)
VALUES(:1, :2, :3, :4, :5, 
:6, :7, :8, :9, :10)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204423", pParam);
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
        #region [ DB204424 - Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн засварлах ]
        public static Result DB204424(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"UPDATE PreSaleMain SET
Name=:2, Name2=:3, Count=:4, AutoNumNo=:5, 
SalesAccountNo=:6, RefundAccountNo=:7, DiscountAccountNo=:8, BonusAccountNo=:9, BonusExpAccountNo=:10
WHERE PreSaleProd=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204424", pParam);

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
        #region [ DB204425 - Урьдчилсан борлуулалтын үндсэн бүтээгдэхүүн устгах ]
        public static Result DB204425(DbConnections pDB, string pPreSaleProd)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PreSaleMain WHERE PreSaleProd=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204425", pPreSaleProd);

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
        #endregion
        #region [ DB205 - Customer ]
        #region [ DB205001 - Харилцагч жагсаалт авах ]
        public static Result DB205001(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "CustomerNo like","ClassCode","BranchNo","Status","FirstName like",
                    "LastName like","RegisterNo like","PassNo like","Sex like","TypeCode",
                    "CorporateName like","InduTypeCode","InduSubTypeCode","LEVELNO","Email like",
                    "Telephone like","Mobile like","HomePhone like","Fax like"
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
                                sb.AppendFormat(" {0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }
                
                sql = string.Format(
@"select *
From V_Customerlist
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), "Order by CustomerNo desc");

                res = pDB.ExecuteQuery("core", sql, "DB205001", pageindex, pagerows, dbparam.ToArray());
                
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
        #region [ DB205002 - Харилцагч дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205002(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select CustomerNo, ClassCode, TypeCode, InduTypeCode, InduSubTypeCode, FirstName, LastName, MiddleName, CorporateName, CorporateName2,
RegisterNo, PassNo, Sex, BirthDay, Company, Position, Experience, 
Email, Telephone, Mobile, HomePhone, Fax, WebSite, SpecialApproval, levelno, CountryCode, LanguageCode,
BranchNo, Status, decode(Status, 0, 'Идэвхгүй', 'Идэвхтэй') as StatusName, DriverNo, createdate, createuser, oldid, 
ContractNo, Accountno, IncomeAccountno
From Customer
where CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205002", CustomerID);

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
        #region [ DB205003 - Харилцагч шинээр нэмэх ]
        public static Result DB205003(DbConnections pDB, object[] pParam, int flag, string custno)
        {
            Result res = new Result();
            try
            {
                if (flag == 0)
                {
                    //[B02[Y04][F1][G1][P02][S06]	BYFGPS	"B=Branch; Y=Year; F=ClassCode; G=TypeCode; P=MemberType; S=Sequence (CODE=MemberType)"
                    long seq = 0;
                    Core.AutoNumEnum enums = new AutoNumEnum();
                    enums.B = Static.ToStr(pParam[27]); //Branch
                    enums.Y = Static.ToStr(Static.ToDate(pParam[30]).Year); //CreateDate
                    enums.F = Static.ToStr(pParam[1]); //ClassCode
                    enums.G = Static.ToStr(pParam[2]); //TypeCode

                    Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 12, "",enums);
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
                }
                else
                {
                    pParam[0] = custno;
                }

                string sql =
@"INSERT INTO Customer(CustomerNo, ClassCode, TypeCode, InduTypeCode, InduSubTypeCode, FirstName, LastName, MiddleName, CorporateName, CorporateName2,
RegisterNo, PassNo, Sex, BirthDay, Company, Position, Experience, Email, Telephone, Mobile, 
HomePhone, Fax, WebSite, SpecialApproval, levelno, CountryCode, LanguageCode, BranchNo, Status, DriverNo, 
createdate, createuser, oldid, ContractNo, Accountno, IncomeAccountno)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20, 
:21, :22, :23, :24, :25, :26, :27, :28, :29, :30,
:31, :32, :33, :34, :35, :36)";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205003", pParam);


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
        #region [ DB205004 - Харилцагч засварлах ]
        public static Result DB205004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE Customer SET
ClassCode=:2, TypeCode=:3, InduTypeCode=:4, InduSubTypeCode=:5, FirstName=:6, LastName=:7, MiddleName=:8, CorporateName=:9, CorporateName2=:10,
RegisterNo=:11, PassNo=:12, Sex=:13, BirthDay=:14, Company=:15, Position=:16, Experience=:17, Email=:18, Telephone=:19, Mobile=:20,
HomePhone=:21, Fax=:22, WebSite=:23, SpecialApproval=:24, levelno=:25, CountryCode=:26, LanguageCode=:27, BranchNo=:28, Status=:29, DriverNo=:30, 
createdate=:31, createuser=:32, oldid=:33, ContractNo=:34, Accountno=:35, IncomeAccountno=:36
WHERE CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205004", pParam);

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
        #region [ DB205005 - Харилцагч устгах ]
        public static Result DB205005(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM Customer WHERE CustomerNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205005", CustomerID);

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

        #region [ DB205006 - Харилцагчийн хаягийн жагсаалт авах ]
        public static Result DB205006(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate
FROM CUSTOMERADDR
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205006", CustomerID);

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
        #region [ DB205007 - Харилцагчийн хаягийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205007(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate
FROM CUSTOMERADDR
where CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205007", CustomerID, SeqNo);

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
        #region [ DB205008 - Харилцагчийн хаяг шинээр нэмэх ]
        public static Result DB205008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERADDR(CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, sysdate)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205008", pParam);

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
        #region [ DB205009 - Харилцагчийн хаяг засварлах ]
        public static Result DB205009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERADDR SET
CITYCODE=:3, DISTCODE=:4, SUBDISTCODE=:5, NOTE=:6, ADDRCURRENT=:7, APARTMENT=:8
WHERE CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205009", pParam);

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
        #region [ DB205010 - Харилцагчийн хаяг устгах ]
        public static Result DB205010(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERADDR WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205010", CustomerID, SeqNo);

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
        //
        #region [ DB205011 - Харилцагчийн зургийн жагсаалт авах ]
        public static Result DB205011(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID
FROM CUSTOMERPIC
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205011", CustomerID);

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
        #region [ DB205012 - Харилцагчийн зургийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205012(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID
FROM CUSTOMERPIC
where CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205012", CustomerID, SeqNo);

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
        #region [ DB205013 - Харилцагчийн зураг шинээр нэмэх ]
        public static Result DB205013(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERPIC(CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205013", pParam);

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
        #region [ DB205014 - Харилцагчийн зураг засварлах ]
        public static Result DB205014(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERPIC SET
PICTURETYPE=:3, ATTACHID=:4
WHERE CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205014", pParam);

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
        #region [ DB205015 - Харилцагчийн зураг устгах ]
        public static Result DB205015(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERPIC WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205015", CustomerID, SeqNo);

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
        //
        #region [ DB205016 - Харилцагчийн зургийн түүхийн жагсаалт авах ]
        public static Result DB205016(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT TXNDATE, CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID, ACTION, USERNO, POSTDATE
FROM CUSTOMERPICHIST
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205016", CustomerID);

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
        #region [ DB205017 - Харилцагчийн зургийн түүхийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205017(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TXNDATE, CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID, ACTION, USERNO, POSTDATE
FROM CUSTOMERPICHIST
where CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205017", CustomerID, SeqNo);

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
        #region [ DB205018 - Харилцагчийн зургийн түүх шинээр нэмэх ]
        public static Result DB205018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERPICHIST(TXNDATE, CUSTOMERNO, SEQNO, PICTURETYPE, ATTACHID, ACTION, USERNO, POSTDATE)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205018", pParam);

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
        #region [ DB205019 - Харилцагчийн зургийн түүх засварлах ]
        public static Result DB205019(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERPICHIST SET
TXNDATE=:1, PICTURETYPE=:4, ATTACHID=:5, ACTION=:6, USERNO=:7, POSTDATE=:8
WHERE CustomerNo = :2 AND SEQNO =: 3 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205019", pParam);

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
        #region [ DB205020 - Харилцагчийн зургийн түүх устгах ]
        public static Result DB205020(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERPICHIST WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205020", CustomerID, SeqNo);

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

        #region [ DB205021 - Харилцагчийн хамаатан садны жагсаалт авах ]
        public static Result DB205021(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, FAMILYTYPE, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE,CustNo 
FROM CUSTOMERFAMILY
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205021", CustomerID);

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
        #region [ DB205022 - Харилцагчийн хамаатан садны дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205022(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, FAMILYTYPE, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE,CustNo
FROM CUSTOMERFAMILY
where CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205022", CustomerID, SeqNo);

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
        #region [ DB205023 - Харилцагчийн хамаатан садны шинээр нэмэх ]
        public static Result DB205023(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERFAMILY(CUSTOMERNO, SEQNO, FAMILYTYPE, FIRSTNAME, LASTNAME, MIDDLENAME, REGISTERNO, PASSNO, EMAIL, TELEPHONE, MOBILE,CustNo)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, :11,:12)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205023", pParam);

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
        #region [ DB205024 - Харилцагчийн хамаатан садны засварлах ]
        public static Result DB205024(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERFAMILY SET
FAMILYTYPE=:3, FIRSTNAME=:4, LASTNAME=:5, MIDDLENAME=:6, REGISTERNO=:7, PASSNO=:8, EMAIL=:9, TELEPHONE=:10, MOBILE=:11,CustNo=:12
WHERE CustomerNo = :1 AND SEQNO = :2 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205024", pParam);

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
        #region [ DB205025 - Харилцагчийн хамаатан садны устгах ]
        public static Result DB205025(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERFAMILY WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205025", CustomerID, SeqNo);

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

        #region [ DB205026 - Харилцагчийн дотоод дансны жагсаалт авах ]
        public static Result DB205026(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {

                string sql =
@"SELECT CUSTOMERNO, AccountNo, AccountName
FROM CUSTACNT
where CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205026", CustomerID);

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
        #region [ DB205027 - Харилцагчийн дотоод дансны дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205027(DbConnections pDB, long CustomerID, string pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, AccountNo, AccountName
FROM CUSTACNT
where CustomerNo=:1 AND AccountNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205027", CustomerID, pAccountNo);

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
        #region [ DB205028 - Харилцагчийн дотоод данс шинээр нэмэх ]
        public static Result DB205028(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTACNT(CUSTOMERNO, AccountNo, AccountName)
VALUES(:1, :2, :3)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205028", pParam);

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
        #region [ DB205029 - Харилцагчийн дотоод данс засварлах ]
        public static Result DB205029(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTACNT SET
AccountName=:3
WHERE CustomerNo=:1 AND AccountNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205029", pParam);

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
        #region [ DB205030 - Харилцагчийн дотоод данс устгах ]
        public static Result DB205030(DbConnections pDB, long CustomerID, string pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTACNT WHERE CustomerNo=:1 AND AccountNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205030", CustomerID, pAccountNo);

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

        #region [ DB205036 - Харилцагчийн холбоо барьсан мэдээлэл жагсаалт авах ]
        public static Result DB205036(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, TO_CHAR(CONTACTDATE, 'YYYY.MM.DD') AS CONTACTDATE, POSTDATE, CONTACTTYPE, NOTE, BRIEFDESC,substr(h.userfname,0,1)||'.'||userlname as userno
FROM CUSTOMERCONTACT c
left join hpuser h on c.userno=h.userno
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205036", CustomerID);

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
        #region [ DB205037 - Харилцагчийн холбоо барьсан мэдээлэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205037(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, TO_CHAR(CONTACTDATE, 'YYYY.MM.DD') AS CONTACTDATE, POSTDATE, CONTACTTYPE, NOTE, BRIEFDESC,substr(h.userfname,0,1)||'.'||userlname as userno
FROM CUSTOMERCONTACT c
left join hpuser h on c.userno=h.userno
where CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205037", CustomerID, SeqNo);

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
        #region [ DB205038 - Харилцагчийн холбоо барьсан мэдээлэл шинээр нэмэх ]
        public static Result DB205038(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;

                string sql =
@"INSERT INTO CUSTOMERCONTACT(CUSTOMERNO, SEQNO, CONTACTDATE, POSTDATE, CONTACTTYPE, NOTE, BRIEFDESC,USERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205038", pParam);

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
        #region [ DB205039 - Харилцагчийн холбоо барьсан мэдээлэл засварлах ]
        public static Result DB205039(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;

                string sql =
@"UPDATE CUSTOMERCONTACT SET
CONTACTDATE=:3, POSTDATE=:4, CONTACTTYPE=:5, NOTE=:6, BRIEFDESC=:7,USERNO=:8
WHERE CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205039", pParam);

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
        #region [ DB205040 - Харилцагчийн холбоо барьсан мэдээлэл устгах ]
        public static Result DB205040(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERCONTACT WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205040", CustomerID, SeqNo);

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

        #region [ DB205041 - Харилцагчийн дансны мэдээлэл жагсаалт авах ]
        public static Result DB205041(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, BANKNO, CURCODE, ACCOUNTNO
FROM CUSTOMERACCOUNT
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205041", CustomerID);

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
        #region [ DB205042 - Харилцагчийн дансны мэдээлэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205042(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, BANKNO, CURCODE, ACCOUNTNO
FROM CUSTOMERACCOUNT
where CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205042", CustomerID, SeqNo);

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
        #region [ DB205043 - Харилцагчийн дансны мэдээлэл шинээр нэмэх ]
        public static Result DB205043(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERACCOUNT(CUSTOMERNO, SEQNO, BANKNO, CURCODE, ACCOUNTNO)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205043", pParam);

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
        #region [ DB205044 - Харилцагчийн дансны мэдээлэл засварлах ]
        public static Result DB205044(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERACCOUNT SET
BANKNO=:3, CURCODE=:4, ACCOUNTNO=:5
WHERE CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205044", pParam);

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
        #region [ DB205045 - Харилцагчийн дансны мэдээлэл устгах ]
        public static Result DB205045(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERACCOUNT WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205045", CustomerID, SeqNo);

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

        #region [ DB205046 - Харилцагчийн хавсралтууд жагсаалт авах ]
        public static Result DB205046(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, ATTACHID, DESCRIPTION, FILETYPE
FROM CUSTOMERATTACH
where CustomerNo=:1
order by SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205046", CustomerID);

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
        #region [ DB205047 - Харилцагчийн хавсралтууд дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205047(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, ATTACHID, DESCRIPTION, FILETYPE
FROM CUSTOMERATTACH
where CustomerNo=:1 AND SEQNO= :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205047", CustomerID, SeqNo);

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
        #region [ DB205048 - Харилцагчийн хавсралтууд шинээр нэмэх ]
        public static Result DB205048(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTOMERATTACH(CUSTOMERNO, SEQNO, ATTACHID, DESCRIPTION, FILETYPE)
VALUES(:1, :2, :3, :4, :5)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205048", pParam);

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
        #region [ DB205049 - Харилцагчийн хавсралтууд засварлах ]
        public static Result DB205049(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTOMERATTACH SET
ATTACHID=:3, DESCRIPTION=:4, FILETYPE=:5
WHERE CustomerNo=:1 AND SEQNO=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205049", pParam);

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
        #region [ DB205050 - Харилцагчийн хавсралтууд устгах ]
        public static Result DB205050(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERATTACH WHERE CustomerNo=:1 AND SEQNO=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205050", CustomerID, SeqNo);

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

        #region [ DB205051 - Харилцагчийн товч дүгнэлт жагсаалт авах ]
        public static Result DB205051(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT CUSTOMERNO, SEQNO, TXNDATE, POSTDATE, USERNO, NOTE
FROM CUSTOMERNOTE
where CustomerNo = :1
Order by CUSTOMERNO, SEQNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205051", CustomerID);

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
        #region [ DB205052 - Харилцагчийн товч дүгнэлт дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205052(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CUSTOMERNO, SEQNO, TXNDATE, POSTDATE, USERNO, NOTE
FROM CUSTOMERNOTE
where CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205052", CustomerID, SeqNo);

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
        #region [ DB205053 - Харилцагчийн товч дүгнэлт шинээр нэмэх ]
        public static Result DB205053(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;

                string sql =
@"INSERT INTO CUSTOMERNOTE(CUSTOMERNO, SEQNO, TXNDATE, POSTDATE, USERNO, NOTE)
VALUES(:1, :2, :3, :4, :5, :6)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205053", pParam);

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
        #region [ DB205054 - Харилцагчийн товч дүгнэлт засварлах ]
        public static Result DB205054(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                pParam[3] = DateTime.Now;
                string sql =
@"UPDATE CUSTOMERNOTE SET
TXNDATE=:3, POSTDATE=:4, USERNO=:5, NOTE=:6
WHERE CustomerNo = :1 AND SEQNO = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205054", pParam);

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
        #region [ DB205055 - Харилцагчийн товч дүгнэлт устгах ]
        public static Result DB205055(DbConnections pDB, long CustomerID, long SeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTOMERNOTE WHERE CustomerNo = :1 AND SEQNO = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205055", CustomerID, SeqNo);

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

        #region [ DB205056 - Харилцагчийн нэмэлт мэдээлэл жагсаалт авах /CustAdd/ ]
        public static Result DB205056(DbConnections pDB, int pagenumber, int pagecount)
        {
            Result res = new Result();
            try
            {
                string sql =
@" SELECT ID, NAME, NAME2, ADDTYPE, LEN, MANDATORY, MASK, ADDDEFAULT, DESCRIPTION, LISTCOMBO, COMBOEDIT,  TABLENAME, FIELDLD,  FIELDNAME, ORDERNO
FROM CUSTADD
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205056");

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
        #region [ DB205057 - Харилцагчийн нэмэлт мэдээлэл дэлгэрэнгүй мэдээлэл авах /CustAdd/ ]
        public static Result DB205057(DbConnections pDB, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ID, NAME, NAME2, ADDTYPE, LEN, MANDATORY, MASK, ADDDEFAULT, DESCRIPTION, LISTCOMBO, COMBOEDIT,  TABLENAME, FIELDLD,  FIELDNAME, ORDERNO
FROM CUSTADD
where ID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205057", pID);

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
        #region [ DB205058 - Харилцагчийн нэмэлт мэдээлэл шинээр нэмэх /CustAdd/ ]
        public static Result DB205058(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTADD(ID, NAME, NAME2, ADDTYPE, LEN, MANDATORY, MASK, ADDDEFAULT, DESCRIPTION, LISTCOMBO, COMBOEDIT, TABLENAME, FIELDID, FIELDNAME, ORDERNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205058", pParam);

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
        #region [ DB205059 - Харилцагчийн нэмэлт мэдээлэл засварлах /CustAdd/ ]
        public static Result DB205059(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTADD SET
NAME=:2, NAME2=:3, ADDTYPE=:4, LEN=:5, MANDATORY=:6, MASK=:7, ADDDEFAULT=:8, DESCRIPTION=:9, LISTCOMBO=:10, 
COMBOEDIT=:11, TABLENAME=:12, FIELDID=:13, FIELDNAME=:14, ORDERNO=:15
WHERE ID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205059", pParam);

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
        #region [ DB205060 - Харилцагчийн нэмэлт мэдээлэл устгах /CustAdd/ ]
        public static Result DB205060(DbConnections pDB, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTADD WHERE ID = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205060", pID);

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
        //
        #region [ DB205061 - Харилцагчийн нэмэлт мэдээллийн өгөгдөлийн жагсаалт авах /CustAddData/ ]
        public static Result DB205061(DbConnections pDB, int pagenumber, int pagecount, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT a.KEY, a.ID, a.VALUE, a.ATTACHID, C.NAME, C.NAME2, C.ADDTYPE, C.LEN, C.MANDATORY, C.MASK,
C.ADDDEFAULT, C.DESCRIPTION, C.LISTCOMBO, C.COMBOEDIT, C.TABLENAME, C.FIELDID, C.FIELDNAME, C.ORDERNO 
FROM CUSTADDDATA a
left join (SELECT ID, NAME, NAME2, ADDTYPE, LEN, MANDATORY, MASK, ADDDEFAULT, DESCRIPTION,
            LISTCOMBO, COMBOEDIT, TABLENAME, FIELDID, FIELDNAME, ORDERNO
            FROM CUSTADD) c on a.id=c.id
WHERE a.KEY=:1
ORDER BY C.ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205061", CustomerID);

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
        #region [ DB205062 - Харилцагчийн нэмэлт мэдээлэлийн өгөгдөлийн дэлгэрэнгүй мэдээлэл авах /CustAddData/ ]
        public static Result DB205062(DbConnections pDB, long CustomerID, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT KEY, ID, VALUE, ATTACHID
FROM CUSTADDDATA
WHERE KEY=:1 AND ID = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205062", CustomerID, pID);

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
        #region [ DB205063 - Харилцагчийн нэмэлт мэдээлэлийн өгөгдөлийн шинээр нэмэх /CustAddData/ ]
        public static Result DB205063(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO CUSTADDDATA(KEY, ID, VALUE, ATTACHID)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205063", pParam);

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
        #region [ DB205064 - Харилцагчийн нэмэлт мэдээлэлийн өгөгдөлийн засварлах /CustAdd/ ]
        public static Result DB205064(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CUSTADDDATA SET
VALUE=:3, ATTACHID=:4
WHERE KEY=:1 AND ID = :2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205064", pParam);

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
        #region [ DB205065 - Харилцагчийн нэмэлт мэдээлэлийн өгөгдөлийн устгах /CustAddData/ ]
        public static Result DB205065(DbConnections pDB, long CustomerID, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CUSTADDDATA WHERE KEY=:1 AND ID = :2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205065", CustomerID, pID);

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


        #region [ DB205066 - Харилцагчийн жагсаалт авах (Регистрийн дугаараар)]
        public static Result DB205066(DbConnections pDB, string RegisterNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select c.CustomerNo,
c.ClassCode,decode(c.ClassCode, 0, 'ХУВЬ ХҮН', 1, 'БАЙГУУЛЛАГА') ClassCodeName,
c.TypeCode,c.InduTypeCode,i.name as InduTypeCodeName,
c.InduSubTypeCode, s.name as InduSubTypeCodeName, 
c.FirstName,c.LastName,c.MiddleName,c.CorporateName,c.CorporateName2,
c.RegisterNo,c.PassNo,c.DriverNo,c.Sex, decode(c.Sex, 0, 'ЭР', 1, 'ЭМ') SexName,c.BirthDay,c.Company,c.Position,c.Experience,c.DirFirstName,c.DirLastName,
c.DirMiddleName,c.DirRegisterNo,c.DirPassNo,c.DirSex, decode(c.DirSex, 0, 'ЭР', 1, 'ЭМ') DirSexName,c.DirBirthDay,c.Email,c.Telephone,c.Mobile,c.HomePhone,c.Fax,
c.WebSite,c.SpecialApproval,c.RateCode,
c.CountryCode, cc.name as CountryCodeName, c.LanguageCode, ll.name as LanguageCodeName,
c.isOtherInsurance, decode(c.isOtherInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isOtherInsuranceName,
c.isHInsurance, decode(c.isHInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isHInsuranceName, 
c.isSInsurance, decode(c.isSInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isSInsuranceName,c.BranchNo,c.Status,
decode(c.Status, 0, 'Гэрээ хийгээгүй', 1, 'Гэрээ хийсэн') StatusName, createuser, oldid
From Customer c
left join INDUSTRY i on C.INDUTYPECODE=I.TYPECODE
left join SUBINDUSTRY s on C.InduSubTypeCode=S.SUBTYPECODE and C.INDUTYPECODE=S.TYPECODE
left join country cc on C.COUNTRYCODE=CC.COUNTRYCODE
left join language ll on C.LANGUAGECODE=LL.LANGUAGECODE
where c.RegisterNo=:1
Order by c.CustomerNo desc";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205066", RegisterNo);

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

        #region [ DB205067 - Харилцагч жагсаалт авах(Дуудлага) ]
        public static Result DB205067(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "FirstName like",
                    "LastName like","CorporateName like",  "RegisterNo like","BirthDay like","Sex","PassNo like"
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
c.TypeCode,c.InduTypeCode,i.name as InduTypeCodeName,
c.InduSubTypeCode, s.name as InduSubTypeCodeName, 
c.FirstName,c.LastName,c.MiddleName,c.CorporateName,c.CorporateName2,
c.RegisterNo,c.PassNo,c.DriverNo,c.Sex, decode(c.Sex, 0, 'ЭР', 1, 'ЭМ') SexName,c.BirthDay,c.Company,c.Position,c.Experience,c.DirFirstName,c.DirLastName,
c.DirMiddleName,c.DirRegisterNo,c.DirPassNo,c.DirSex, decode(c.DirSex, 0, 'ЭР', 1, 'ЭМ') DirSexName,c.DirBirthDay,c.Email,c.Telephone,c.Mobile,c.HomePhone,c.Fax,
c.WebSite,c.SpecialApproval,c.RateCode,
c.CountryCode, cc.name as CountryCodeName, c.LanguageCode, ll.name as LanguageCodeName,
c.isOtherInsurance, decode(c.isOtherInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isOtherInsuranceName,
c.isHInsurance, decode(c.isHInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isHInsuranceName, 
c.isSInsurance, decode(c.isSInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isSInsuranceName,c.BranchNo,c.Status,
decode(c.Status, 0, 'Гэрээ хийгээгүй', 1, 'Гэрээ хийсэн') StatusName, createuser, oldid
From Customer c
left join INDUSTRY i on C.INDUTYPECODE=I.TYPECODE
left join SUBINDUSTRY s on C.InduSubTypeCode=S.SUBTYPECODE and C.INDUTYPECODE=S.TYPECODE
left join country cc on C.COUNTRYCODE=CC.COUNTRYCODE
left join language ll on C.LANGUAGECODE=LL.LANGUAGECODE
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), "Order by c.CustomerNo desc");

                res = pDB.ExecuteQuery("core", sql, "DB205067", pageindex, pagerows, dbparam.ToArray());

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
        //
        #region [ DB205068 - Харилцагчийн холбоо барьсан төрлийн жагсаалт авах ]
        public static Result DB205068(DbConnections pDB,object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TypeCode,
Name,
Name2,
OrderNo from custcontacttype
Order by TypeCode";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205068", null);

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
        #region [ DB205069 - Харилцагчийн холбоо барьсан төрлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205069(DbConnections pDB, int pTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TypeCode,
Name,
Name2,
OrderNo from custcontacttype
where TypeCode=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205069", pTypeCode);

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
        #region [ DB205070 - Харилцагчийн холбоо барьсан төрөл шинээр нэмэх ]
        public static Result DB205070(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
               

                string sql =
@"INSERT INTO custcontacttype(TypeCode,
Name,
Name2,
OrderNo)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205070", pParam);
               res= F_Error(res);
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
        #region [ DB205071 - Харилцагчийн холбоо барьсан төрөл засварлах ]
        public static Result DB205071(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
               

                string sql =
@"UPDATE custcontacttype SET 
Name=:2,
Name2=:3,
OrderNo=:4
WHERE TypeCode = :1 ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205071", pParam);
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
        #region [ DB205072 - Харилцагчийн холбоо барьсан төрөл устгах ]
        public static Result DB205072(DbConnections pDB, int pTypeCode)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM custcontacttype WHERE TypeCode = :1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205072", pTypeCode);

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
        //
        #region [ DB205073 - Харилцагчийн захиралын жагсаалт авах ]
        public static Result DB205073(DbConnections pDB,long pCustomerno )
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CustomerNo,
SeqNo ,
Position ,
FirstName  ,
LastName ,
MiddleName ,
RegisterNo ,
PassNo ,
Sex ,
BirthDay  from CustDirector 
where CustomerNo=:1
Order by CustomerNo,SeqNo";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205073", pCustomerno);

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
        #region [ DB205074 - Харилцагчийн захиралын дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB205074(DbConnections pDB, long pCustomerno,long pSeqno)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT CustomerNo,
SeqNo ,
Position ,
FirstName  ,
LastName ,
MiddleName ,
RegisterNo ,
PassNo ,
Sex ,
BirthDay  from CustDirector 
where CustomerNo=:1 and SeqNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB205074", pCustomerno,pSeqno);

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
        #region [ DB205075 - Харилцагчийн захиралын мэдээлэл шинээр нэмэх ]
        public static Result DB205075(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {


                string sql =
@"INSERT INTO CustDirector(CustomerNo,
SeqNo ,
Position ,
FirstName  ,
LastName ,
MiddleName ,
RegisterNo ,
PassNo ,
Sex ,
BirthDay)
VALUES(:1, :2, :3, :4,:5,:6,:7,:8,:9,:10)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB205075", pParam);

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
        #region [ DB205076 - Харилцагчийн захиралын мэдээлэл засварлах ]
        public static Result DB205076(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {


                string sql =
@"UPDATE CustDirector SET 
Position=:3 ,
FirstName =:4 ,
LastName=:5,
MiddleName=:6,
RegisterNo=:7,
PassNo=:8,
Sex=:9,
BirthDay=:10
WHERE CustomerNo=:1 and SeqNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB205076", pParam);

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
        #region [ DB205077 - Харилцагчийн захиралын мэдээлэл устгах ]
        public static Result DB205077(DbConnections pDB, long pCustomerno, long pSeqno)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM CustDirector WHERE CustomerNo=:1 and SeqNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB205077", pCustomerno,pSeqno);

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
        #region [ DB207 - FA ]
        #region [ DB207001 - Үндсэн хөрөнгийн жагсаалт авах ]
        public static Result DB207001(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
             
                string[] fieldnames = new string[] { "FAID like","FATypeID","Name like","Name2 like","BranchNo",
"CreateUser","UnitTypeCode","BalanceCount","UnitCost","BalanceTotal",
"CurrCode","Position","AccountNo","EmpNo","StartDate",
"EndDate","LastTellerTxnDate","Status","Depreciation","LastDepDate","LEVELNO"
                
             
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
                                sb.AppendFormat(" a.{0} :{1}||'%' ", fieldnames[i], dbindex++);

                            else
                            sb.AppendFormat(" a.{0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                        
                        
                    }
                }

                sql =string.Format(
@"SELECT a.FAID, a.FATYPEID, t.name as fatypename, a.NAME, a.NAME2, a.BRANCHNO, a.CREATEUSER, a.UNITTYPECODE, b.name unitname, a.BALANCECOUNT, a.UNITCOST,
a.BALANCETOTAL, a.CURRCODE, a.POSITION, a.ACCOUNTNO, c.name accountname, a.EMPNO, d.name empname, a.startdate, a.enddate, a.LASTTELLERTXNDATE,
a.STATUS, decode(a.STATUS, 0, 'НЭЭЛТТЭЙ', 9, 'ХААГДСАН') STATUSName, a.DEPRECIATION, a.LASTDEPDATE,a.LEVELNO
FROM FAREG a
left join UNITTYPE b on A.UNITTYPECODE=B.UNITTYPECODE
left join bacaccount c on C.aCCOUNTno=A.ACCOUNTNO
left join EMPLOYEE d on D.EMPNO=A.EMPNO
left join fatype t on a.fatypeid=t.FATYPEID
{0} {1} ", sb.Length > 0 ? "where" : "", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB207001", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB207002 - Үндсэн хөрөнгийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB207002(DbConnections pDB, long pFaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT FAID, f.FATYPEID, f.NAME, f.NAME2, f.BRANCHNO, CREATEUSER, UNITTYPECODE, BALANCECOUNT, UNITCOST, BALANCETOTAL,
CURRCODE, f.POSITION, f.ACCOUNTNO, f.EMPNO, STARTDATE, ENDDATE, LASTTELLERTXNDATE, f.STATUS, DEPRECIATION, LASTDEPDATE,f.LEVELNO,t.name as fatypename,e.name as empname
FROM FAREG f
left join fatype t on f.fatypeid=T.FATYPEID
left join employee e on f.empno=e.empno
where FAID = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB207002", pFaID);

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
        #region [ DB207003 - Үндсэн хөрөнгө шинээр нэмэх ]
        public static Result DB207003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                long seq = 0;
                #region [ FAID ]
                Core.AutoNumEnum enums = new AutoNumEnum();
                enums.B = Static.ToStr(pParam[4]);
                enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[10])).CurrencyCode);
                enums.P = Static.ToStr(pParam[1]);
                enums.Y = Static.ToStr(Static.ToDate(pParam[5]).Year);

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 14, "",enums);

                if (seqres.ResultNo == 0)
                {
                    seq = Static.ToLong(seqres.ResultDesc);
                    if (seq == 0)
                    {
                        seqres.ResultNo = 9110068;
                        seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:14][" + seqres.ResultDesc + "]";
                        return seqres;
                    }
                }
                else
                    return seqres;

                pParam[0] = seq;
                #endregion

                string sql =
@"INSERT INTO FAREG(FAID, FATYPEID, NAME, NAME2, BRANCHNO, CREATEUSER, UNITTYPECODE, BALANCECOUNT, UNITCOST, BALANCETOTAL,
CURRCODE, POSITION, ACCOUNTNO, EMPNO, STARTDATE, ENDDATE, LASTTELLERTXNDATE, STATUS, DEPRECIATION, LASTDEPDATE, LEVELNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20, :21)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB207003", pParam);
                object[] obbj = new object[1];

                obbj[0] = pParam[0];

                res.Param = obbj;
         
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
        #region [ DB207004 - Үндсэн хөрөнгө засварлах ]
        public static Result DB207004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE FAREG SET
FATYPEID=:2, NAME=:3, NAME2=:4, BRANCHNO=:5, CREATEUSER=:6, UNITTYPECODE=:7, BALANCECOUNT=:8, UNITCOST=:9, BALANCETOTAL=:10, 
CURRCODE=:11, POSITION=:12, ACCOUNTNO=:13, EMPNO=:14, startdate=:15, enddate=:16, LASTTELLERTXNDATE=:17, STATUS=:18, DEPRECIATION=:19, LASTDEPDATE=:20, LEVELNO=:21
WHERE FAID=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB207004", pParam);

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
        #region [ DB207005 - Үндсэн хөрөнгө устгах ]
        public static Result DB207005(DbConnections pDB, long pFaID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM FAREG WHERE FAID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB207005", pFaID);

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
        //Үндсэн хөрөнгийн шилжилт хөдөлгөөн
        #region [ DB207006 - Үндсэн хөрөнгийн шилжилт хөдөлгөөний жагсаалт авах ]
        public static Result DB207006(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                string[] fieldnames = new string[] { "JrNo like","TxnDate","PostDate like","FAID like","TxnCount like","UserNo like","BranchNo like",
"OldEmpNo like","NewEmpNo like","Description like","NewFAID like"
                
             
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
                                sb.AppendFormat(" a.{0} :{1}||'%' ", fieldnames[i], dbindex++);

                            else
                                sb.AppendFormat(" a.{0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }


                    }
                }

                sql = string.Format(
@"SELECT JrNo,
TxnDate,
PostDate,
a.FAID,
f.name,
TxnCount,
a.UserNo,substr(h.userfname,0,1)||'.'||h.userlname as username,
a.BranchNo,
OldEmpNo,E.NAME as oldempname,
NewEmpNo,ee.name as newempname,
Description,
NewFAID
FROM FAMovement  a
left join fareg f on A.FAID=F.FAID
left join hpuser h on a.userno=h.userno
left join employee e on A.OLDEMPNO=E.EMPNO
left join employee ee on A.OLDEMPNO=eE.EMPNO
{0} {1} ", sb.Length > 0 ? "where" : "", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB207006", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB207007 - Үндсэн хөрөнгийн шилжилт хөдөлгөөний дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB207007(DbConnections pDB, long pFaID, DateTime pTxnDate)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT JrNo,
TxnDate,
PostDate,
FAID,
TxnCount,
UserNo,
BranchNo,
OldEmpNo,
NewEmpNo,
Description,
NewFAID
FROM FAMovement 
where JrNo = :1,TxnDate=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB207007", pFaID, pTxnDate);

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
        #region [ DB207008 - Үндсэн хөрөнгийн шилжилт хөдөлгөөн шинээр нэмэх ]
        public static Result DB207008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                

                string sql =
@"INSERT INTO FAMovement(JrNo,
TxnDate,
PostDate,
FAID,
TxnCount,
UserNo,
BranchNo,
OldEmpNo,
NewEmpNo,
Description,
NewFAID
)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB207008", pParam);

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
        #region [ DB207009 - Үндсэн хөрөнгийн хариуцагч засах ]
        public static Result DB207009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@" update fareg
set empno=:2
where faid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB207009", pParam);

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
        #region [ DB207010 - Үндсэн хөрөнгийн элэгдэл болон үлдэгдэл засах ]
        public static Result DB207010(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"update fareg
set balancetotal=:2,
DEPRECIATION=:3,BALANCECOUNT=:4
where faid=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB207010", pParam);

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
        
        
        //

        #endregion
        #region [ DB208 - Employee&BACACCOUNT Байгууллагын данс ]
        #region [ DB208001 - Ажилчидийн бүртгэлийн жагсаалт авах ]
        public static Result DB208001(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "EmpNo like","Name like","Name2 like","Position","Status",
                    "BranchNo","UserNo","LEVELNO"
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
                            sb.AppendFormat(" c.{0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql =string.Format(
@"SELECT EMPNO, c.NAME, c.NAME2, c.POSITION, c.STATUS,decode(c.STATUS,9,'ИДЭВХГҮЙ', 0,'ИДЭВХТЭЙ') as STATUSNAME, 
c.BRANCHNO,cc.NAME as BRANCHNAME,c.userno,c.LEVELNO
FROM EMPLOYEE c
left join BRANCH cc on c.BRANCHNO=cc.BRANCH
 {0} {1} ", sb.Length > 0 ? "where" : "", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB208001", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB208002 - Ажилчидийн бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB208002(DbConnections pDB, long pEmpID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT EMPNO, NAME, NAME2, POSITION, STATUS, BRANCHNO, USERNO, LEVELNO
FROM EMPLOYEE
where EMPNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB208002", pEmpID);

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
        #region [ DB208003 - Ажилчидийн бүртгэл шинээр нэмэх ]
        public static Result DB208003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                long seq = 0;
                #region [ EMPNO ]
                Core.AutoNumEnum enums = new AutoNumEnum();
                enums.B = Static.ToStr(pParam[5]);

                //enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[10])).CurrencyCode);
                //enums.P = Static.ToStr(pParam[4]);
                //enums.G = Static.ToStr(pParam[4]);

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 16,"", enums);

                if (seqres.ResultNo == 0)
                {
                    seq = Static.ToLong(seqres.ResultDesc);
                    if (seq == 0)
                    {
                        seqres.ResultNo = 9110068;
                        seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [ID:16][" + seqres.ResultDesc + "]";
                        return seqres;
                    }
                }
                else
                    return seqres;

                pParam[0] = seq;
                #endregion

                string sql =
@"INSERT INTO EMPLOYEE(EMPNO, NAME, NAME2, POSITION, STATUS, BRANCHNO, USERNO, LEVELNO)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB208003", pParam);

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
        #region [ DB208004 - Ажилчидийн бүртгэл засварлах ]
        public static Result DB208004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE EMPLOYEE SET
NAME=:2, NAME2=:3, POSITION=:4, STATUS=:5, BRANCHNO=:6, USERNO=:7, LEVELNO=:8
WHERE EMPNO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB208004", pParam);

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
        #region [ DB208005 - Ажилчидийн бүртгэл устгах ]
        public static Result DB208005(DbConnections pDB, long pEmpID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM EMPLOYEE WHERE EMPNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB208005", pEmpID);

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

        #region [ DB208006 - Байгууллагын дансны бүртгэлийн жагсаалт авах ]
        public static Result DB208006(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "Accountno like","Name like","Name2 like","BranchNo","ProdCode like",
                "Balance","CurCode","UserNo","LevelNo","CreateDate",
                "Status","StartDate","EndDate", "customerno like"
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
                                sb.AppendFormat(" a.{0} '%'||:{1}||'%' ", fieldnames[i], dbindex++);
                               // sb.AppendFormat(" a.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else

                                
                            sb.AppendFormat(" a.{0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql =string.Format(
@"SELECT a.ACCOUNTNO, a.NAME, a.NAME2, a.BRANCHNO, a.PRODCODE, bb.name as ProdName, a.BALANCE, a.CURCODE, a.USERNO, a.LEVELNO, a.CREATEDATE,
a.STATUS, decode(a.STATUS, 0, 'НЭЭЛТТЭЙ', 1, 'ХААГДСАН') StatusName, 
a.STARTDATE, a.ENDDATE, a.LastTellerTxnDate, a.CustomerNo,
decode(b.CLASSCODE, 1, CORPORATENAME, substr(b.FIRSTNAME, 0, 1)||'.'||b.LASTNAME) customername
FROM BACACCOUNT a
left join customer b on A.CUSTOMERNO=b.CUSTOMERNO
left join bacproduct bb on A.PRODCODE=BB.PRODCODE
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by ACCOUNTNO");

                res = pDB.ExecuteQuery("core", sql, "DB208006", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB208007 - Байгууллагын дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB208007(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
STATUS, STARTDATE, ENDDATE, LastTellerTxnDate, customerno
FROM BACACCOUNT
where ACCOUNTNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB208007", pAccountNo);

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
        #region [ DB208008 - Байгууллагын дансны бүртгэл шинээр нэмэх ]
        public static Result DB208008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                //ulong seq = EServ.Interface.Sequence.NextByVal("DealNo");
                long seq = 0;
                Core.AutoNumEnum enums = new AutoNumEnum();
                enums.B = Static.ToStr(pParam[3]);
                enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[6])).CurrencyCode);
                enums.P = Static.ToStr(pParam[4]);
                enums.Y = Static.ToStr(Static.ToDate(pParam[9]).Year);
                enums.M = Static.ToStr(Static.ToDate(pParam[9]).Month);
                enums.R = Static.ToStr(pParam[14]);
                //enums.G = Static.ToStr(pParam[4]);

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 1, "",enums);
                //ISM.Lib.Static.WriteToLogFile("GetSeq:" + seqres.ResultDesc);

                if (seqres.ResultNo == 0)
                {
                    seq = Static.ToLong(seqres.ResultDesc);
                    if (seq == 0)
                    {
                        seqres.ResultNo = 9110068;
                        seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [" + seqres.ResultDesc + "]";
                        return seqres;
                    }
                }
                else
                    return seqres;

                pParam[0] = seq;

                string sql =
@"INSERT INTO BACACCOUNT(ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
STATUS, STARTDATE, ENDDATE, LastTellerTxnDate, customerno)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14, :15)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB208008", pParam);
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
        #region [ DB208009 - Байгууллагын дансны бүртгэл засварлах ]
        public static Result DB208009(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE BACACCOUNT SET
NAME=:2, NAME2=:3, BRANCHNO=:4, PRODCODE=:5, BALANCE=:6, CURCODE=:7, USERNO=:8, LEVELNO=:9, CREATEDATE=:10,
STATUS=:11, STARTDATE=:12, ENDDATE=:13, LastTellerTxnDate=:14, customerno=:15
WHERE ACCOUNTNO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB208009", pParam);

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
        #region [ DB208010 - Байгууллагын дансны бүртгэл устгах ]
        public static Result DB208010(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql = "";

                sql =
@"SELECT ACCOUNTNO FROM BACTXN WHERE ACCOUNTNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB208010", pAccountNo);

                if (res.AffectedRows != 0)
                { 
                    res.ResultNo = 9110079;
                    res.ResultDesc = "Энэ дансан дээр гүйлгээ гарсан байгаа тул устгах боломжгүй.";
                    return res;
                }

                sql =
@"DELETE FROM BACACCOUNT WHERE ACCOUNTNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB208010", pAccountNo);

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
        #region [ DB208011 - Харилцагчийн дансны жагсаалт авах ]
        public static Result DB208011(DbConnections pDB, long pCustomerno)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"SELECT a.ACCOUNTNO, a.NAME, a.NAME2, a.BRANCHNO, a.PRODCODE, bb.name as ProdName, a.BALANCE, a.CURCODE, a.USERNO, a.LEVELNO, a.CREATEDATE,
a.STATUS, decode(a.STATUS, 0, 'НЭЭЛТТЭЙ', 1, 'ХААГДСАН') StatusName, 
a.STARTDATE, a.ENDDATE, a.LastTellerTxnDate
FROM BACACCOUNT a
left join customer b on A.CUSTOMERNO=b.CUSTOMERNO
left join bacproduct bb on A.PRODCODE=BB.PRODCODE
where a.CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB208011", pCustomerno);

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
        #region [ DB209 - CTA Балансын гадуурх данс CONACCOUNT ]
        #region [ DB209001 - Балансын гадуурх дансны бүртгэлийн жагсаалт авах ]
        public static Result DB209001(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "AccountNo like","Name like","Name2 like","BranchNo","ProdCode",
"Balance","CurCode","UserNo","LevelNo","CreateDate",
"StartDate","EndDate","Status","ContractID","InsuranceNo",
"RiInsuranceNo","ClaimNo","CustNo","Person"
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
                            sb.AppendFormat(" c.{0}=:{1}", fieldnames[i], dbindex++);
                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql =string.Format(
@"SELECT c.ACCOUNTNO, c.NAME, c.NAME2, c.BRANCHNO, 
c.PRODCODE, cc.name as ProdName, c.BALANCE, c.CURCODE, c.USERNO, c.LEVELNO, c.CREATEDATE,
c.STATUS, decode(c.STATUS, 0, 'НЭЭЛТТЭЙ', 1, 'ХААГДСАН') StatusName,
c.STARTDATE, c.ENDDATE, c.CONTRACTID, c.INSURANCENO, c.RIINSURANCENO, c.CLAIMNO, c.CUSTNO, c.PERSON, c.LastTellerTxnDate
FROM CONACCOUNT c
left join conproduct cc on c.prodcode=cc.prodcode
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " Order by c.ACCOUNTNO");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB209001",dbparam.ToArray());

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
        #region [ DB209002 - Балансын гадуурх дансны бүртгэлийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB209002(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
STATUS, STARTDATE, ENDDATE, CONTRACTID, INSURANCENO, RIINSURANCENO, CLAIMNO, CUSTNO, PERSON, LastTellerTxnDate
FROM CONACCOUNT
where ACCOUNTNO = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB209002", pAccountNo);

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
        #region [ DB209003 - Балансын гадуурх дансны бүртгэл шинээр нэмэх ]
        public static Result DB209003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                //ulong seq = EServ.Interface.Sequence.NextByVal("DealNo");
                long seq = 0;
                Core.AutoNumEnum enums = new AutoNumEnum();
                enums.B = Static.ToStr(pParam[3]);
                enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[6])).CurrencyCode);
                enums.P = Static.ToStr(pParam[4]);
                enums.Y = Static.ToStr(Static.ToDate(pParam[9]).Year);
                //enums.G = Static.ToStr(pParam[4]);

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 2, "",enums);
                //ISM.Lib.Static.WriteToLogFile("GetSeq:" + seqres.ResultDesc);

                if (seqres.ResultNo == 0)
                {
                    seq = Static.ToLong(seqres.ResultDesc);
                    if (seq == 0)
                    {
                        seqres.ResultNo = 9110068;
                        seqres.ResultDesc = "Автомат дугаар нэмэхэд хөрвүүлэлт дээр алдаа гарлаа. [" + seqres.ResultDesc + "]";
                        return seqres;
                    }
                }
                else
                    return seqres;

                pParam[0] = seq;

                string sql =
@"INSERT INTO CONACCOUNT(ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
STATUS, STARTDATE, ENDDATE, LastTellerTxnDate)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10,
:11, :12, :13, :14)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB209003", pParam);

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
        #region [ DB209004 - Балансын гадуурх дансны бүртгэл засварлах ]
        public static Result DB209004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE CONACCOUNT SET
NAME=:2, NAME2=:3, BRANCHNO=:4, PRODCODE=:5, BALANCE=:6, CURCODE=:7, USERNO=:8, LEVELNO=:9, CREATEDATE=:10,
STATUS=:11, STARTDATE=:12, ENDDATE=:13, LastTellerTxnDate=:14
WHERE ACCOUNTNO=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB209004", pParam);

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
        #region [ DB209005 - Балансын гадуурх дансны бүртгэл устгах ]
        public static Result DB209005(DbConnections pDB, long pAccountNo)
        {
            Result res = new Result();
            try
            {
                string sql = "";

                sql =
@"SELECT ACCOUNTNO FROM CONTXN WHERE ACCOUNTNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB209005", pAccountNo);

                if (res.AffectedRows != 0)
                {
                    res.ResultNo = 9110079;
                    res.ResultDesc = "Энэ дансан дээр гүйлгээ гарсан байгаа тул устгах боломжгүй.";
                    return res;
                }

                sql =
@"DELETE FROM CONACCOUNT WHERE ACCOUNTNO=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB209005", pAccountNo);

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
        #region [ DB214 - Динамик тайлан ]
        #region [ DB214000 - Динамик тайлангуудын жагсаалт авах ]
        public static Result DB214000(DbConnections pDB, int pageindex, int pagerows, int pUserNo, DateTime pTxnDate, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "d.TRANCODE like","d.NAME like","a.groupid" };

                ArrayList dbparam = new ArrayList(fieldnames.Length);
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                dbparam.Add(pUserNo);
                dbparam.Add(pTxnDate);
                if (pParam != null)
                {
                    int dbindex = 3;
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
                //sql =
@"select a.groupid ,a.name as groupname,d.trancode ,d.name from (select b.groupid,b.id,a.name from ReportsGroup a,ReportsLink b where a.groupid=b.groupid ) a
right join (select A.TRANCODE, A.NAME
from txn a, grouptxn b, usergroup c
where
    B.trancode=a.trancode
and B.GROUPID=C.GROUPID
and substr(a.trancode, 0, 2)='24'
and a.trancode not in (240000, 249998, 249999)
and c.userno=:1 and C.EXPIREDATE>=:2
group by A.TRANCODE, A.NAME
) d on  a.id=d.trancode

{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), " ORDER BY a.groupid,D.TRANCODE ");
                res = pDB.ExecuteQuery("core", sql, "DB214000", pageindex, pagerows, dbparam.ToArray());
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
        #region [ DB214001 - Динамик тайлангын гүйлгээний кодын жагсаалт авах ]
        public static Result DB214001(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
     @"select A.TRANCODE, A.NAME
from txn a, grouptxn b, usergroup c
where
    B.trancode=a.trancode
and B.GROUPID=C.GROUPID
and substr(a.trancode, 0, 2)='24'
and a.trancode<>'240000'
group by A.TRANCODE, A.NAME
order by A.TRANCODE";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB214001", null);

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

        //ReportsGroup
        #region [ DB214002 - Dynamic тайлангийн бүлгийн жагсаалт мэдээлэл авах ]
        public static Result DB214002(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select groupid,name,name2,orderno from ReportsGroup
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB214002", null);

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
        #region [ DB214003 - Dynamic тайлангийн бүлгийн дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB214003(DbConnections pDB, int pGroupID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select groupid,name,name2,orderno from ReportsGroup
where groupid=:1
order by orderno";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB214003", pGroupID);

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
        #region [ DB214004 - Dynamic тайлангийн бүлэг нэмэх ]
        public static Result DB214004(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"insert into ReportsGroup(groupid, Name, Name2, OrderNo)
values(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB214004", pParam);
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
        #region [ DB214005 - Dynamic тайлангийн бүлэг засварлах ]
        public static Result DB214005(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ReportsGroup SET Name=:2, Name2=:3,OrderNo=:4
WHERE groupid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB214005", pParam);
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
        #region [ DB214006 - Dynamic тайлангийн бүлэг устгах ]
        public static Result DB214006(DbConnections pDB, long pgroupid)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete FROM ReportsGroup WHERE groupid=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB214006", pgroupid);



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
        //ReportsLink
        #region [ DB214007 - Dynamic тайлангийн холбоосын сонгогдсон болон сонгогдоогүй жагсаалт мэдээлэл авах ]
        public static Result DB214007(DbConnections pDB, int pGroupID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select  decode(A.id,null,0,1) as status ,d.trancode ,d.name from (select id from ReportsGroup a,ReportsLink b where a.groupid=b.groupid and b.groupid=:1 ) a
right join (select A.TRANCODE, A.NAME
from txn a, grouptxn b, usergroup c
where
    B.trancode=a.trancode
and B.GROUPID=C.GROUPID
and substr(a.trancode, 0, 2)='24'
and a.trancode<>'240000'
group by A.TRANCODE, A.NAME
) d on  a.id=d.trancode
ORDER BY D.TRANCODE
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB214007", pGroupID);

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

        #region [ DB214008 - Dynamic тайлангийн холбоос нэмэх ]
        public static Result DB214008(DbConnections pDB, int pGroupID, DataTable pDT)
        {

            Result res = new Result();
            try
            {
                string sql = "";

                //Delete ReportsLink
                sql =
@"delete from ReportsLink where GroupID=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB214008", pGroupID);
                if (res.ResultNo != 0)
                    return res;
                //Insert ReportsLink
                foreach (DataRow dr in pDT.Rows)
                {
                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                        sql =
@"insert into ReportsLink(GroupID,ID)
values(:1, :2)";

                        res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB214008", new object[] { pGroupID, dr["trancode"] });
                        res = F_Error(res);
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
        #region [ DB214009 - Dynamic тайлангийн холбоос засварлах ]
        public static Result DB214009(DbConnections pDB, int pOldGroupID, int pOldID, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[4];
                obj[0] = pOldGroupID;
                obj[1] = pOldID;
                obj[2] = pNewParam[0];
                obj[3] = pNewParam[1];

                string sql =
@"UPDATE ReportsLink SET groupid=:3, id=:4
WHERE groupid=:1 and id=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB214009", obj);
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
        #region [ DB214010 - Dynamic тайлангийн холбоос устгах ]
        public static Result DB214010(DbConnections pDB, int pgroupid, int pID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"delete FROM ReportsLink WHERE groupid=:1 and id=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB214010", pgroupid, pID);



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
        #region [ DB216 - PassPolicy ]
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
        #endregion
        #region [ DB210009 - Санхүүгийн гүйлгээний жагсаалт ]
        public static Result DB210009(DbConnections pDB)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select TxnCode, Name, Name2, DynamicSQL
FROM TxnFin
order by OrderNo ";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB210009", null);
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
        #region [ DB227 - InfoPos FrontOffice ]
        #region [ DB227000 - Гэрээний дугаараар гэрээ байгаа эсэхийг шалгах ]
        public static Result DB227000(DbConnections pDB, string pContractNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select ContractNo
From CONTRACTMAIN
where ContractNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227000", pContractNo);

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
        #region [ DB227001 - Гэрээний хайлтын жагсаалт мэдээлэл авах ]
        public static Result DB227001(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                string[] fieldnames = new string[] { "ContractType","ContractNo like","CustNo like","FirstName like","LastName like",
"CorporateName like","REGISTERNO like"};

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
@"select *
from V_CONTRACTLIST
{0} {1} ", sb.Length > 0 ? "where" : "", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB227001", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB227002 - Захиалгын хайлтын жагсаалт мэдээлэл авах ]
        public static Result DB227002(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;

                string[] fieldnames = new string[] { "a.OrderNo like","a.CustNo like","c.FirstName like","c.LastName like","c.CorporateName like",
"c.REGISTERNO like"};

                ArrayList dbparam = new ArrayList(fieldnames.Length);
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                #region [AutoNum]
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
                #endregion

                sql = string.Format(
@"select a.OrderNo, a.CustNo, c.FirstName, c.LastName, c.CorporateName,
a.ConfirmTerm, a.TermType, decode(a.TermType, 'T', 'Цаг', 'D', 'Өдөр', 'W', 'Гараг', 'M', 'Сар') as TermTypeNAME, a.OrderAmount,
a.PrepaidAmount, a.CurCode, a.Fee, a.StartDate, a.EndDate,
a.PersonCount, a.Status, decode(a.Status, 0, 'Цуцлагдсан', 1, 'Идэвхтэй', 2, 'Баталгаажсан') as StatusName, a.CreateDate, a.PostDate,
a.CreateUser, a.OwnerUser, a.Rebateid, a.Loyalid, a.Pointid
from orders a
left join customer c on a.custno=c.customerno
{0} {1} ", sb.Length > 0 ? "where" : " order by a.OrderNo desc", sb.ToString());

                res = pDB.ExecuteQuery("core", sql, "DB227002", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB227003 - Харилцагч дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB227003(DbConnections pDB, long CustomerID)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select CustomerNo, ClassCode, FirstName, LastName, CorporateName,
RegisterNo, Sex, Mobile, CreateDate, CreateUser,
Height, Foot
From Customer
where CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227003", CustomerID);

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
        #region [ DB227004 - Харилцагч шинээр нэмэх ]
        public static Result DB227004(DbConnections pDB, object[] pParam, int flag, string custno)
        {
            Result res = new Result();
            try
            {
                if (flag == 0)
                {
                    //[B02[Y04][F1][G1][P02][S06]	BYFGPS	"B=Branch; Y=Year; F=ClassCode; G=TypeCode; P=MemberType; S=Sequence (CODE=MemberType)"
                    long seq = 0;
                    Core.AutoNumEnum enums = new AutoNumEnum();

                    enums.B = Static.ToStr(Core.SystemProp.SystemBranchNo); //Branch
                    enums.Y = Static.ToStr(Static.ToDate(pParam[8]).Year); //CreateDate
                    enums.F = Static.ToStr(pParam[1]); //ClassCode
                    enums.G = Static.ToStr(pParam[12]); //TypeCode
                    //enums.P = Static.ToStr(pParam[14]); //MemberType

                    Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 12, "", enums);
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
                }
                else
                {
                    pParam[0] = custno;
                }
                pParam[13] = 0;
                pParam[14] = "";

                string sql =
@"INSERT INTO Customer(CustomerNo, ClassCode, FirstName, LastName, CorporateName,
RegisterNo, Sex, Mobile, CreateDate, CreateUser,
Height, Foot, typecode, membertype, MEMBERCONTRACTNO)
VALUES(:1, :2, :3, :4, :5,
:6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB227004", pParam);


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
        #region [ DB227005 - Харилцагч засварлах ]
        public static Result DB227005(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE Customer SET
ClassCode=:2, FirstName=:3, LastName=:4, CorporateName=:5,
RegisterNo=:6, Sex=:7, Mobile=:8, CreateDate=:9, CreateUser=:10,
Height=:11, Foot=:12, typecode=:13, MEMBERCONTRACTNO=:14, membertype=:15
WHERE CustomerNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB227005", pParam);

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

        #region [ DB227006 - Борлуулалтын багцын бүртгэлийн жагсаалт ]
        public static Result DB227006(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "BatchNo like","SalesNo like"
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
                                sb.AppendFormat(" a.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" a.{0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select a.BatchNo, a.SalesNo, c.classcode, decode(c.classcode, 0, c.firstname||'.'||c.lastname, 1, c.corporatename) as CustName, c.sex, c.registerno,
a.vat, a.contractno
from salesmain a
left join sales b on a.salesno=b.salesno
left join customer c on b.custno=c.customerno
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), "Order by a.BatchNo, a.SalesNo desc");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227006", dbparam.ToArray());

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
        #region [ DB227007 - Борлуулалтын багцын бүртгэлийн дэлгэрэнгүй ]
        public static Result DB227007(DbConnections pDB, string pPatchID, string pSalesNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.BatchNo, a.SalesNo, c.classcode, decode(c.classcode, 0, c.firstname||'.'||c.lastname, 1, c.corporatename) as CustName, c.sex, c.registerno,
a.vat, a.contractno
from salesmain a
left join sales b on a.salesno=b.salesno
left join customer c on b.custno=c.customerno
where a.BatchNo=:1 and a.SalesNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227007", pPatchID, pSalesNo);

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
        #region [ DB227008 - Борлуулалтын багцын бүртгэл шинээр нэмэх ]
        public static Result DB227008(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO salesmain(BatchNo, SalesNo, vat, contractno)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB227008", pParam);

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
        #region [ DB227009 - Борлуулалтын багцын бүртгэл засварлах ]
        public static Result DB227009(DbConnections pDB, object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[6];

                obj[0] = pOldParam[0];
                obj[1] = pOldParam[1];
                obj[2] = pNewParam[0];
                obj[3] = pNewParam[1];
                obj[4] = pNewParam[2];
                obj[5] = pNewParam[3];

                string sql =
@"UPDATE salesmain SET
BatchNo=:3, SalesNo=:4, vat=:5, contractno=:6
WHERE BatchNo=:1 and SalesNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB227009", obj);

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
        #region [ DB227010 - Борлуулалтын багцын бүртгэл устгах ]
        public static Result DB227010(DbConnections pDB, string pBatchNo, string pSalesNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM salesmain WHERE BatchNo=:1 and SalesNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB227010", pBatchNo, pSalesNo);

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

        #region [ DB227011 - Борлуулалтын үндсэн бүртгэлийн жагсаалт ]
        public static Result DB227011(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "SalesNo like","CustNo like","PostDate","TotalAmount","SalesAmount",
                    "Discount","Vat","CurCode","PosNo like","CashierNo like",
                    "Status"
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
                                sb.AppendFormat(" a.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" a.{0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select a.SalesNo,a.CustNo,a.PostDate,a.TotalAmount,a.SalesAmount,
a.Discount,a.Vat,a.CurCode,a.PosNo,a.CashierNo,
a.Ip,a.Mac,a.Status, b.status as paymentstatus, b.PaymentNo, b.SalesJrNo
from Sales a
left join SalesPayment b on a.salesno=b.salesno
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), "Order by a.SalesNo desc");

                res = pDB.ExecuteQuery("core", sql, "DB227011", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB227012 - Борлуулалтын үндсэн бүртгэлийн дэлгэрэнгүй ]
        public static Result DB227012(DbConnections pDB, string pSalesNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.SalesNo,a.CustNo,a.PostDate,a.TotalAmount,a.SalesAmount,
a.Discount,a.Vat,a.CurCode,a.PosNo,a.CashierNo,
a.Ip,a.Mac,a.Status, b.status as paymentstatus, b.PaymentNo, b.SalesJrNo
from Sales a
left join SalesPayment b on a.salesno=b.salesno
where a.SalesNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227012", pSalesNo);

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
        #region [ DB227013 - Борлуулалтын үндсэн бүртгэл шинээр нэмэх ]
        public static Result DB227013(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO Sales(SalesNo,CustNo,PostDate,TotalAmount,SalesAmount,
Discount,Vat,CurCode,PosNo,CashierNo,
Ip,Mac,Status,AreaCode,OldSalesNo)
VALUES(:1, :2, :3, :4, :5, 
:6, :7, :8, :9, :10, 
:11, :12, :13,:14,:15)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB227013", pParam);

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
        #region [ DB227014 - Борлуулалтын үндсэн бүртгэл засварлах ]
        public static Result DB227014(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                
                string sql =
@"UPDATE Sales SET
CustNo=:2,PostDate=:3,TotalAmount=:4,SalesAmount=:5,
Discount=:6,Vat=:7,CurCode=:8,PosNo=:9,CashierNo=:10,
Ip=:11,Mac=:12,Status=:13
WHERE SalesNo=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB227014", pParam);

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
        #region [ DB227015 - Борлуулалтын үндсэн бүртгэл устгах ]
        public static Result DB227015(DbConnections pDB, string pSalesNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM Sales WHERE SalesNo=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB227015", pSalesNo);

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

        #region [ DB227016 - Борлуулалтаар гарсан бараа материалуудын жагсаалт ]
        public static Result DB227016(DbConnections pDB, int pageindex, int pagerows, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "SalesNo like","ProdNo like","ProdType","Quantity","Price",
                    "Discount","SalesAmount"
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
                                sb.AppendFormat(" a.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" a.{0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select a.SalesNo,a.ProdNo, b.name,a.ProdType,a.Quantity,a.Price,
a.Discount,a.SalesAmount, a.SaleType
from SalesProd a
left join invmain b on b.invid=a.ProdNo
where a.prodtype=0
union
select a.SalesNo,a.ProdNo, b.name,a.ProdType,a.Quantity,a.Price,
a.Discount,a.SalesAmount, a.SaleType
from SalesProd a
left join servmain b on b.servid=a.ProdNo
where a.prodtype=1
{0} {1} {2}", sb.Length > 0 ? "where" : "", sb.ToString(), "Order by name");

                res = pDB.ExecuteQuery("core", sql, "DB227016", pageindex, pagerows, dbparam.ToArray());

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
        #region [ DB227017 - Борлуулалтаар гарсан бараа материалуудын дэлгэрэнгүй ]
        public static Result DB227017(DbConnections pDB, string pSalesNo, int pProdType, string pProdNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.SalesNo,a.ProdNo, b.name,a.ProdType,a.Quantity,a.Price,
a.Discount,a.SalesAmount, a.SaleType
from SalesProd a
left join invmain b on b.invid=a.ProdNo
where a.prodtype=0
and a.SalesNo=:1 and a.ProdType=:2 and a.ProdNo=:3
union
select a.SalesNo,a.ProdNo, b.name,a.ProdType,a.Quantity,a.Price,
a.Discount,a.SalesAmount, a.SaleType
from SalesProd a
left join servmain b on b.servid=a.ProdNo
where a.prodtype=1
and a.SalesNo=:1 and a.ProdType=:2 and a.ProdNo=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227017", pSalesNo, pProdType, pProdNo);

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
        #region [ DB227018 - Борлуулалтаар гарсан бараа материал шинээр нэмэх ]
        public static Result DB227018(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO SalesProd(SalesNo,ProdNo,ProdType,Quantity,Price,
Discount,SalesAmount, SaleType)
VALUES(:1, :2, :3, :4, :5, 
:6, :7, :8)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB227018", pParam);

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
        #region [ DB227019 - Борлуулалтаар гарсан бараа материал засварлах ]
        public static Result DB227019(DbConnections pDB, string pOldSalesNo, int pOldProdType, string pOldProdNo, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[11];

                obj[0] = pOldSalesNo;
                obj[1] = pOldProdType;
                obj[2] = pOldProdNo;
                obj[3] = pNewParam[0];
                obj[4] = pNewParam[1];
                obj[5] = pNewParam[2];
                obj[6] = pNewParam[3];
                obj[7] = pNewParam[4];
                obj[8] = pNewParam[5];
                obj[9] = pNewParam[6];
                obj[10] = pNewParam[7];


                string sql =
@"UPDATE SalesProd SET
SalesNo=:4,ProdNo=:5,ProdType=:6,Quantity=:7,Price=:8,
Discount=:9,SalesAmount=:10, SaleType=:11
WHERE SalesNo=:1 and ProdType=:2 and ProdNo=:3";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB227019", obj);

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
        #region [ DB227020 - Борлуулалтаар гарсан бараа материал устгах ]
        public static Result DB227020(DbConnections pDB, string pSalesNo, int pProdType, string pProdNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM SalesProd WHERE SalesNo=:1 and ProdType=:2 and ProdNo=:3";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB227020", pSalesNo, pProdType, pProdNo);

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

        #region [ DB227021 - Борлуулалтын багцын бүртгэлийг pBatchNo оор авах жагсаалт ]
        public static Result DB227021(DbConnections pDB, string pBatchNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select *
from V_SalesList227021
where
BatchNo=:1
order by salesno
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227021", pBatchNo);

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
        #region [ DB227022 - Борлуулсан багцын төлбөрийн төрлүүдийн жагсаалт авах ]
        public static Result DB227022(DbConnections pDB, string pBatchNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select a.batchno, a.salesno, b.totalamount, b.salesamount, b.vat,
b.discount, nvl(s.status,0) as status, s.paymentno, ss.seqno, ss.paymenttype,
ss.amount, p.name as paymenttypename, p.SUSPACCOUNT
from salesmain a
left join sales b on a.salesno=b.salesno
left join salespayment s on a.salesno=s.salesno
left join salespaymentdetail ss on s.paymentno=ss.paymentno
left join PAPAYTYPE p on ss.paymenttype=p.typeid
where a.batchno=:1
order by a.salesno
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227022", pBatchNo);

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
        #region [ DB227023 - Харилцагч болон төхөөрөмжийн холбоос бичлэг үүсгэх Merge BatchNo гоор ]
        public static Result DB227023(DbConnections pDB, long pCustNo, string pBatchNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"MERGE INTO CustomerIdDevice b
USING (
SELECT :1 custno, :2 batchno
FROM dual
) e
ON (b.CustNo = e.CustNo and e.custno=:1)
WHEN MATCHED THEN
UPDATE SET b.BatchNo=e.BatchNo
WHEN NOT MATCHED THEN
insert (CustNo, BatchNo)
values (:1, :2)
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227023", pCustNo, pBatchNo);

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
        #region [ DB227024 - Харилцагч болон төхөөрөмжийн холбоос бичлэг үүсгэх Merge SerialNo гоор ]
        public static Result DB227024(DbConnections pDB, long pCustNo, string pSerialNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"MERGE INTO CustomerIdDevice b
USING (
SELECT :1 custno, :2 SerialNo
FROM dual
) e
ON (b.CustNo = e.CustNo and e.custno=:1)
WHEN MATCHED THEN
UPDATE SET b.SerialNo=e.SerialNo
WHEN NOT MATCHED THEN
insert (CustNo, SerialNo)
values (:1, :2)
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227024", pCustNo, pSerialNo);

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

        #region [ DB227026 - Борлуулалтын түрээсийн хэрэгслүүд жагсаалт ]
        public static Result DB227026(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "SalesNo like","ProdType","ProdNo like","ItemNo","BarCode",
                    "RentStartTime","RentEndTime","RentStatus","DamageType","DamageNote",
                    "ReparationNo","RentOfficer"
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
                                sb.AppendFormat(" a.{0} :{1}||'%' ", fieldnames[i], dbindex++);
                            else
                                sb.AppendFormat(" a.{0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select a.SalesNo,a.ProdType,a.ProdNo,a.ItemNo,a.BarCode,a.RentStartTime,
a.RentEndTime,a.RentStatus,a.DamageType,a.DamageNote,a.ReparationNo,
a.RentOfficer, s.batchno, ss.salesno, i.name as prodname, i.invid
from SalesRent a
left join salesmain s on a.salesno=s.salesno
left join sales ss on a.salesno=ss.salesno
left join invmain i on a.ProdNo=i.invid
where a.ProdType=0
{0} {1} {2}
union
select a.SalesNo,a.ProdType,a.ProdNo,a.ItemNo,a.BarCode,a.RentStartTime,
a.RentEndTime,a.RentStatus,a.DamageType,a.DamageNote,a.ReparationNo,
a.RentOfficer, s.batchno, ss.salesno, i.name as prodname, i.servid
from SalesRent a
left join salesmain s on a.salesno=s.salesno
left join sales ss on a.salesno=ss.salesno
left join servmain i on a.ProdNo=i.servid
where a.ProdType=1
{0} {1} {2}", sb.Length > 0 ? " and " : "", sb.ToString(), " ");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227026", dbparam.ToArray());

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
        #region [ DB227027 - Борлуулалтын түрээсийн хэрэгслүүд дэлгэрэнгүй ]
        public static Result DB227027(DbConnections pDB, string pSalesNo, int pProdType, string pProdNo, int pItemNo)
        {
            Result res = new Result();
            try
            {

                string sql =
@"select a.SalesNo,a.ProdType,a.ProdNo,a.ItemNo,a.BarCode,a.RentStartTime,
a.RentEndTime,a.RentStatus,a.DamageType,a.DamageNote,a.ReparationNo,
a.RentOfficer, s.batchno, ss.salesno, i.name as prodname, i.invid
from SalesRent a
left join salesmain s on a.salesno=s.salesno
left join sales ss on a.salesno=ss.salesno
left join invmain i on a.ProdNo=i.invid
where a.ProdType=0
and a.SalesNo=:1 and a.ProdType=:2 and a.ProdNo=:3 and a.itemno=:4
union
select a.SalesNo,a.ProdType,a.ProdNo,a.ItemNo,a.BarCode,a.RentStartTime,
a.RentEndTime,a.RentStatus,a.DamageType,a.DamageNote,a.ReparationNo,
a.RentOfficer, s.batchno, ss.salesno, i.name as prodname, i.servid
from SalesRent a
left join salesmain s on a.salesno=s.salesno
left join sales ss on a.salesno=ss.salesno
left join servmain i on a.ProdNo=i.servid
where a.ProdType=1
and a.SalesNo=:1 and a.ProdType=:2 and a.ProdNo=:3 and a.itemno=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227027", pSalesNo, pProdType, pProdNo, pItemNo);

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
        #region [ DB227028 - Борлуулалтын түрээсийн хэрэгслүүд шинээр нэмэх ]
        public static Result DB227028(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO SalesRent(SalesNo,ProdType,ProdNo,ItemNo,BarCode,
RentStartTime,RentEndTime,RentStatus,DamageType,DamageNote,
ReparationNo,RentOfficer
)
VALUES(:1, :2, :3, :4, :5, 
:6, :7, :8, :9, :10, 
:11, :12)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB227028", pParam);

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
        #region [ DB227029 - Борлуулалтын түрээсийн хэрэгслүүд засварлах ]
        public static Result DB227029(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SalesRent SET
BarCode=:5,
RentStartTime=:6,RentEndTime=:7,RentStatus=:8,DamageType=:9,DamageNote=:10,
ReparationNo=:11,RentOfficer=:12
WHERE SalesNo=:1 and ProdType=:2 and ProdNo=:3 and itemno=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB227029", pParam);

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
        #region [ DB227030 - Борлуулалтын түрээсийн хэрэгслүүд устгах ]
        public static Result DB227030(DbConnections pDB, string pSalesNo, int pProdType, string pProdNo, int pItemNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM SalesRent WHERE SalesNo=:1 and ProdType=:2 and ProdNo=:3 and itemno=:4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB227030", pSalesNo, pProdType, pProdNo, pItemNo);

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

        #region [ DB227031 - Борлуулалтын Төлбөрийн Бүртгэл жагсаалт ]
        public static Result DB227031(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "SalesNo like","PaymentNo","SalesJrNo like","Status"
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
                                sb.AppendFormat(" {0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select SalesNo,PaymentNo,SalesJrNo,Status
from SalesPayment
{0} {1} {2}", sb.Length > 0 ? " where  " : "", sb.ToString(), " order by salesno desc ");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227031", dbparam.ToArray());

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
        #region [ DB227032 - Борлуулалтын Төлбөрийн Бүртгэл дэлгэрэнгүй ]
        public static Result DB227032(DbConnections pDB, string pSalesNo, string pPaymentNo)
        {
            Result res = new Result();
            try
            {

                string sql =
@"select SalesNo,PaymentNo,SalesJrNo,Status
from SalesPayment
where SalesNo=:1 and PaymentNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227032", pSalesNo, pPaymentNo);

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
        #region [ DB227033 - Борлуулалтын Төлбөрийн Бүртгэл шинээр нэмэх ]
        public static Result DB227033(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO SalesPayment(SalesNo,PaymentNo,SalesJrNo,Status)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB227033", pParam);

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
        #region [ DB227034 - Борлуулалтын Төлбөрийн Бүртгэл засварлах ]
        public static Result DB227034(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SalesPayment SET
SalesJrNo=:3,Status=:4
WHERE SalesNo=:1 and PaymentNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB227034", pParam);

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
        #region [ DB227035 - Борлуулалтын Төлбөрийн Бүртгэл устгах ]
        public static Result DB227035(DbConnections pDB, string pSalesNo, string pPaymentNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM SalesPayment WHERE SalesNo=:1 and PaymentNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB227035", pSalesNo, pPaymentNo);

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

        #region [ DB227036 - Борлуулалтын Биллийн Бүртгэл жагсаалт ]
        public static Result DB227036(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "a.PaymentNo like","a.SeqNo","a.PaymentType like","a.Amount",
                    "a.PaymentJrNo","b.salesno","b.salesjrno","b.status",
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
                                sb.AppendFormat(" {0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select a.PaymentNo,a.SeqNo,a.PaymentType,a.Amount,a.PaymentJrNo,
b.salesno, b.salesjrno, b.status, a.PaymentFlag, a.ContractNo
from SalesPaymentDetail a
left join SalesPayment b on a.PaymentNo=b.PaymentNo
{0} {1} {2}", sb.Length > 0 ? " where  " : "", sb.ToString(), " order by a.PaymentNo desc ");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227036", dbparam.ToArray());

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
        #region [ DB227037 - Борлуулалтын Биллийн Бүртгэл дэлгэрэнгүй ]
        public static Result DB227037(DbConnections pDB, string pPaymentNo, string pSeqNo)
        {
            Result res = new Result();
            try
            {

                string sql =
@"select a.PaymentNo,a.SeqNo,a.PaymentType,a.Amount,a.PaymentJrNo,
b.salesno, b.salesjrno, b.status, a.PaymentFlag, a.ContractNo
from SalesPaymentDetail a
left join SalesPayment b on a.PaymentNo=b.PaymentNo
where a.PaymentNo=:1 and a.SeqNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227037", pPaymentNo, pSeqNo);

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
        #region [ DB227038 - Борлуулалтын Биллийн Бүртгэл шинээр нэмэх ]
        public static Result DB227038(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO SalesPaymentDetail(PaymentNo,SeqNo,PaymentType,Amount,PaymentJrNo, PaymentFlag, ContractNo)
VALUES(:1, :2, :3, :4, :5, :6, :7)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB227038", pParam);

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
        #region [ DB227039 - Борлуулалтын Биллийн Бүртгэл засварлах ]
        public static Result DB227039(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE SalesPaymentDetail SET
PaymentType=:3,Amount=:4,PaymentJrNo=:5, PaymentFlag=:6, ContractNo=:7
WHERE PaymentNo=:1 and SeqNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB227039", pParam);

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
        #region [ DB227040 - Борлуулалтын Биллийн Бүртгэл устгах ]
        public static Result DB227040(DbConnections pDB, string pPaymentNo, string pSeqNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM SalesPaymentDetail WHERE PaymentNo=:1 and SeqNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB227040", pPaymentNo, pSeqNo);

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

        #region [ DB227041 - Түрээсийн барааны хайлтын жагсаалт ]
        public static Result DB227041(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql;
                string[] fieldnames = new string[] { "a.invid like","a.name like","b.barcode like","b.status"
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
                                sb.AppendFormat(" {0}=:{1} ", fieldnames[i], dbindex++);

                            dbparam.Add(pParam[i]);
                        }
                    }
                }

                sql = string.Format(
@"select a.invid, a.invtype, c.name as invtypename, a.name as invname, a.note as invnote,
b.barcode, b.status, b.LASTPREPAREUSERNO, b.LASTPREPAREDATE, b.note as invseriesnote
from invmain a
left join invseries b on a.invid=b.invid
left join painvtype c on a.invtype=c.invtype
where a.rentflag=2 and a.status=0
{0} {1} {2}", sb.Length > 0 ? " and  " : "", sb.ToString(), " order by a.name ");

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227041", dbparam.ToArray());

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
        #region [ DB227042 - Түрээсийн барааг идэвхжүүлэх бэлдэх ]
        public static Result DB227042(DbConnections pDB, string pInvID, string pBarCode, int pLastPrepareUserNo, DateTime pLastPrepareDate)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql = 
@"update invseries
set status = 1, LastPrepareUserNo=:3, LastPrepareDate=:4
where invid=:1 and barcode=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227042", pInvID, pBarCode, pLastPrepareUserNo, pLastPrepareDate);

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
        #region [ DB227043 - Түрээсийн барааг идэвхгүй болгох ]
        public static Result DB227043(DbConnections pDB, string pInvID, string pBarCode)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"update invseries
set status = 0
where invid=:1 and barcode=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227043", pInvID, pBarCode);

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
        #region [ DB227044 - Түрээсийн барааг олгох ]
        public static Result DB227044(DbConnections pDB, string pInvID, string pBarCode)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"update invseries
set status = 2
where invid=:1 and barcode=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227044", pInvID, pBarCode);

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
        #region [ DB227045 - Түрээсийн барааны эвдрэл гэмтэл оруулах ]
        public static Result DB227045(DbConnections pDB, string pInvID, string pBarCode, string pNote)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"update invseries
set status = 3, note=:3
where invid=:1 and barcode=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227045", pInvID, pBarCode, pNote);

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


        #region [ DB227046 - Тухайн ПОС д хамааралтай бараа болон үйлчилгээний хайлтын жагсаалт ]
        public static Result DB227046(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql = @"
select 0 as status,1 as prodtype, servid as prodid, name, servtype, count
from servmain 
where status=0
and servid in (select ID
                    from workarealink
                    where areacode in ((select areacode
                                from workarealink
                                where type=0
                                and id=:1))
                    and type = 2 and PRODUCTTYPE=1)
and servid like :2||'%' and name like :3||'%'
union
select 0 ,0, invid, name, invtype, count
from invmain 
where status=0
and invid in (select ID
                    from workarealink
                    where areacode in ((select areacode
                                from workarealink
                                where type=0
                                and id=:1))
                    and type = 2 and PRODUCTTYPE=0)
and invid like :2||'%' and name like :3||'%' ";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB227046", pParam);

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

        //Насны бүлэг
        #region [ DB22801 - Насны жагсаалт авах ]
        public static Result DB22801(DbConnections pDB, int pagenumber, int pagecount, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT agecode, NAME, NAME2, ORDERNO
FROM paage
ORDER BY ORDERNO";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB22801", null);

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
        #region [ DB22802 - Насны дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB22802(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT agecode, NAME, NAME2, ORDERNO
FROM paage
where LANGUAGECODE = :1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB22802", pNo);

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
        #region [ DB22803 - Насны бүлэг шинээр нэмэх ]
        public static Result DB22803(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"INSERT INTO paage(agecode, NAME, NAME2, ORDERNO)
VALUES(:1, :2, :3, :4)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB22803", pParam);
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
        #region [ DB22804 - Насны бүлэг засварлах ]
        public static Result DB22804(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE paage SET NAME=:2, NAME2=:3, ORDERNO=:4
WHERE agecode=:1";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB22804", pParam);
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
        #region [ DB22805 - Насны бүлэг устгах ]
        public static Result DB22805(DbConnections pDB, long pNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM paage WHERE agecode=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB22805", pNo);

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
