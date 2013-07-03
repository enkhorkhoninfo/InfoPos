using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using EServ.Data;
using EServ.Interface;
using EServ.Shared;

namespace IPos.Contract
{
    public static class DBIO
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
        public static Result DB204003(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string seq = "";
                #region [ ContractNo ]
                Core.AutoNumEnum enums = new Core.AutoNumEnum();
                //enums.B = Static.ToStr(pParam[4]);
                enums.A = "0";
                enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[9])).CurrencyCode);
                enums.P = Static.ToStr(pParam[1]);
                enums.Y = Static.ToStr(Static.ToDate(pParam[14]).Year);

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 3, enums);

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

                string sql =
@"INSERT INTO ContractMain(ContractNo, ContractType, CustNo, ValidStartDate, ValidStartTime, ValidEndDate, ValidEndTime, Amount, Balance, CurCode,
PersonCount, DepFreq, DepAmount, Status, CreateDate, CreatePostDate, CreateUser, OwnerUser, Rebateid, Loyalid, 
Pointid, BalanceType, Vat, Accountno, IncomeAccountno, BalanceMethod, DepBalance)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19, :20,
:21, :22, :23, :24, :25, :26, :27)";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB204003", pParam);
                res = F_Error(res);
                if (res.ResultNo==0)
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
        public static Result DB204022(DbConnections pDB, object[] obj, string pPrefix)
        {
            Result res = new Result();
            try
            {
                string seq = "";
                #region [ ContractNo ]
                Core.AutoNumEnum enums = new Core.AutoNumEnum();
                enums.U = pPrefix;
                enums.A = "0";
                enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(obj[8])).CurrencyCode);
                enums.P = Static.ToStr(obj[1]);
                enums.Y = Static.ToStr(Static.ToDate(obj[21]).Year);

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 19, enums);

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

                string[] fieldnames = new string[] { "OrderNo like","CustNo like","FirstName like","LastName like","CorporateName like",
"ConfirmTerm","TermType","OrderAmount","PrepaidAmount",
"CurCode","Fee","StartDate","EndDate","PersonCount",
"Status","CreateDate","PostDate","CreateUser","OwnerUser"};

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
        public static Result DB204103(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                long seq = 0;
                #region [ OrderNo ]
                Core.AutoNumEnum enums = new Core.AutoNumEnum();
                //enums.B = Static.ToStr(pParam[4]);
                enums.C = Static.ToStr(Core.SystemProp.gCur.Get(Static.ToStr(pParam[6])).CurrencyCode);
                //enums.P = Static.ToStr(pParam[1]);
                enums.Y = Static.ToStr(Static.ToDate(pParam[12]).Year);

                Result seqres = Core.SystemProp.gAutoNum.GetNextNumber(pDB, 4, enums);

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

                string sql =
@"INSERT INTO orders(OrderNo, CustNo, ConfirmTerm, TermType, OrderAmount,
PrepaidAmount, CurCode, Fee, StartDate, EndDate,
PersonCount, Status, CreateDate, PostDate, CreateUser,
OwnerUser, Rebateid, Loyalid, Pointid)
VALUES(:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, 
:11, :12, :13, :14, :15, :16, :17, :18, :19)";

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
CustNo=:2, ConfirmTerm=:3, TermType=:4, OrderAmount=:5,
PrepaidAmount=:6, CurCode=:7, Fee=:8, StartDate=:9, EndDate=:10,
PersonCount=:11, Status=:12, CreateDate=:13, PostDate=:14, CreateUser=:15,
OwnerUser=:16, Rebateid=:17, Loyalid=:18, Pointid=:19
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
@"select a.orderno, a.custno, c.FirstName, c.LastName, c.CorporateName
from orderperson a
left join customer c on a.custno=c.customerno
where a.orderno=:1
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
        public static Result DB204107(DbConnections pDB, string pOrderNo, long pCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select a.orderno, a.custno, c.FirstName, c.LastName, c.CorporateName
from orderperson a
left join customer c on a.custno=c.customerno
where a.orderno=:1 and a.custno=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204107", pOrderNo, pCustNo);

                return res;
            }
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
@"INSERT INTO OrderPerson(OrderNo, CustNo)
VALUES(:1, :2)";

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
        public static Result DB204109(DbConnections pDB, string pOrderNo, long pOldCustNo, long pNewCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE OrderPerson SET
CustNo=:3
WHERE OrderNo=:1 and CustNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204109", pOrderNo, pOldCustNo, pNewCustNo);

                return res;
            }
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
        public static Result DB204110(DbConnections pDB, string pOrderNo, string pCustNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM OrderPerson WHERE OrderNo=:1 AND CustNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204110", pOrderNo, pCustNo);

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion

        #region [ DB204111 - Захиалга доторх багц үйлчилгээний бүлэг жагсаалт авах ]
        public static Result DB204111(DbConnections pDB, string pOrderNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select OrderNo, GroupNo, OrderDate, StartTime, EndTime, RunType,
decode(RunType, 0, 'НЭГ', 1, 'ОЛОН') as RunTypeName
from ordergroup
where orderno=:1
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
        #region [ DB204112 - Захиалга доторх багц үйлчилгээний бүлэг дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204112(DbConnections pDB, string pOrderNo, long pGroupNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select OrderNo, GroupNo, OrderDate, StartTime, EndTime, RunType,
decode(RunType, 0, 'НЭГ', 1, 'ОЛОН') as RunTypeName
from ordergroup
where orderno=:1 and GroupNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204112", pOrderNo, pGroupNo);

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB204113 - Захиалга доторх багц үйлчилгээний бүлэг шинээр нэмэх ]
        public static Result DB204113(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO ordergroup(OrderNo, GroupNo, OrderDate, StartTime, EndTime, RunType)
VALUES(:1, :2, :3, :4, :5, :6)";

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
        #region [ DB204114 - Захиалга доторх багц үйлчилгээний бүлэг засварлах ]
        public static Result DB204114(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE ordergroup SET
OrderDate=:3, StartTime=:4, EndTime=:5, RunType=:6
WHERE OrderNo=:1 and GroupNo=:2";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204114", pParam);

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB204115 - Захиалга доторх багц үйлчилгээний бүлэг устгах ]
        public static Result DB204115(DbConnections pDB, string pOrderNo, long pGroupNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM ordergroup WHERE OrderNo=:1 and GroupNo=:2";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204115", pOrderNo, pGroupNo);

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion

        #region [ DB204116 - Захиалгын багц дахь бүтээгдэхүүний бүртгэл жагсаалт авах ]
        public static Result DB204116(DbConnections pDB, string pOrderNo, long pGroupNo)
        {
            Result res = new Result();
            try
            {
                string sql;

                sql =
@"select A.OrderNo, A.GroupNo, A.ProdNo, A.ProdType, decode(A.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, A.Qty, 0 as ISSCHEDULE, b.Count, 'T' as Unit, 0 as Duration, b.name as prodname
from orderproduct a
left join invmain b on a.ProdNo=b.InvID
where a.orderno=:1 and a.GroupNo=:2 and A.ProdType=0
union
select A.OrderNo, A.GroupNo, A.ProdNo, A.ProdType, decode(A.ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, A.Qty, b.ISSCHEDULE, b.Count, p.Unit, p.Duration, b.name as prodname
from orderproduct a
left join servmain b on a.ProdNo=b.ServID
left join PAScheduleType p on b.ScheduleType=p.ScheduleType
where a.orderno=:1 and a.GroupNo=:2 and A.ProdType=1
union
select A.OrderNo, A.GroupNo, A.ProdNo, A.ProdType, decode(A.ProdType, 0, 'БАРАА', 2, 'БАГЦ') as ProdTypeName, A.Qty, 0 as ISSCHEDULE, 1 as Count, 'T' as Unit, 0 as Duration, b.name as prodname
from orderproduct a
left join packmain b on a.ProdNo=b.PackID
where a.orderno=:1 and a.GroupNo=:2 and A.ProdType=2
";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204116", pOrderNo, pGroupNo);

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB204117 - Захиалгын багц дахь бүтээгдэхүүний бүртгэл дэлгэрэнгүй мэдээлэл авах ]
        public static Result DB204117(DbConnections pDB, string pOrderNo, long pGroupNo, string pProdNo, int pProdType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"select OrderNo, GroupNo, ProdNo, ProdType, decode(ProdType, 0, 'БАРАА', 1, 'ҮЙЛЧИЛГЭЭ') as ProdTypeName, Qty
from orderproduct
where orderno=:1 and GroupNo=:2 and ProdNo=:3 and ProdType=:4";

                res = pDB.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB204117", pOrderNo, pGroupNo, pProdNo, pProdType);

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB204118 - Захиалгын багц дахь бүтээгдэхүүний бүртгэл шинээр нэмэх ]
        public static Result DB204118(DbConnections pDB, object[] pParam)
        {
            Result res = new Result();
            try
            {

                string sql =
@"INSERT INTO orderproduct( OrderNo, GroupNo, ProdNo, ProdType, Qty)
VALUES(:1, :2, :3, :4, :5)";

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
        #region [ DB204119 - Захиалгын багц дахь бүтээгдэхүүний бүртгэл засварлах ]
        public static Result DB204119(DbConnections pDB, object[] pOldParam, object[] pNewParam)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[8];

                obj[0] = pOldParam[0];
                obj[1] = pOldParam[1];
                obj[2] = pOldParam[2];
                obj[3] = pNewParam[3];
                obj[4] = pNewParam[1];
                obj[5] = pNewParam[2];
                obj[6] = pNewParam[3];
                obj[7] = pNewParam[4];

                string sql =
@"UPDATE orderproduct SET
GroupNo=:5, ProdNo=:6, ProdType=:7, Qty=:8
WHERE orderno=:1 and GroupNo=:2 and ProdNo=:3 and ProdType=:4";

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
        #region [ DB204120 - Захиалгын багц дахь бүтээгдэхүүний бүртгэл устгах ]
        public static Result DB204120(DbConnections pDB, string pOrderNo, long pGroupNo, string pProdNo, int pProdType)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM orderproduct WHERE orderno=:1 and GroupNo=:2 and ProdNo=:3 and ProdType=:4";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.DELETE, "DB204120", pOrderNo, pGroupNo, pProdNo, pProdType);

                return res;
            }
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
        public static Result DB204126(DbConnections pDB, string pOrderNo, DateTime pConfirmDateTime, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE orders
SET ConfirmDateTime=:2, ConfirmUserNo=:3, status=2
WHERE orderno=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204126", pOrderNo, pConfirmDateTime, pUserNo);

                return res;
            }
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
        public static Result DB204127(DbConnections pDB, string pOrderNo, DateTime pCancelDateTime, int pUserNo, string pCancelNote)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE orders
SET CancelDateTime=:2, CancelUserNo=:3, status=0, CancelNote=:4
WHERE orderno=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204127", pOrderNo, pCancelDateTime, pUserNo, pCancelNote);

                return res;
            }
            catch (Exception ex)
            {
                ISM.Lib.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                res.ResultNo = 9110001;
                res.ResultDesc = "Датабааз руу хандахад алдаа гарлаа" + ex.Message;
                return res;
            }
        }
        #endregion
        #region [ DB204128 - Захиалга сунгах ]
        public static Result DB204128(DbConnections pDB, string pOrderNo, DateTime pExpendDateTime, int pUserNo)
        {
            Result res = new Result();
            try
            {
                string sql =
@"UPDATE orders
SET ExpendDateTime=:2, ExpendUserNo=:3
WHERE orderno=:1";
                res = pDB.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB204128", pOrderNo, pExpendDateTime, pUserNo);

                return res;
            }
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
    }
}