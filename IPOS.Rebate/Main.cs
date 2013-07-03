using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;

namespace IPOS.Rebate
{
    public class Rebate : IModule
    {
        RebateMaster gRebateMaster = new RebateMaster();
        RebateFormula gRebateFormula = new RebateFormula();
        static long tmptrankey=0;
        string RebateName = "";
        string LoyalName = "";
        string PointName = "";
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime First = DateTime.Now;
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 500099:        // Барааний хөнгөлөлт тооцоолох
                        res = Txn500099(ci, ri, db, ref lg);
                        break;
                    default:
                        res = new Result(1000, "Unknown transation.");
                        break;
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                DateTime Last = DateTime.Now;
                if (res.ResultNo == 0)
                    ISM.Lib.Static.WriteToLogFile("IPos.Cash", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("IPos.Cash", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
            }
        }

        public Result Txn500099(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string sql;

            DataTable saleDB;
            DataTable valueDB;
            Hashtable ids = new Hashtable();
            RebateID id;
            int index = 0 ;
            try
            {
                gRebateMaster.Init(db);
                gRebateFormula.Init(db);

                #region [ Гэрээний дугаар орж ирсэн бол rebate ID-г авах ]

                long contractno = Static.ToLong(ri.ReceivedParam[0]);
                if (contractno != 0)
                {
                    sql = "select rebateid, loyalid, pointid from ContractMain where contractNo=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn50000x", contractno);

                    if (res.ResultNo != 0) return res;
                    if (res.Data == null)
                    {
                        res.ResultNo = 9110015;
                        res.ResultDesc = "Гэрээний мэдээлэл уншихад алдаа гарлаа";
                        return res;
                    }
                    if (res.Data.Tables[0] == null)
                    {
                        res.ResultNo = 9110015;
                        res.ResultDesc = "Гэрээний мэдээлэл уншихад алдаа гарлаа";
                        return res;
                    }
                    if (res.Data.Tables[0].Rows.Count > 0)
                    {
                        id = new RebateID();
                        id.rebateid = Static.ToLong(res.Data.Tables[0].Rows[0][0]);
                        id.loyalid = Static.ToLong(res.Data.Tables[0].Rows[0][1]);
                        id.pointid = Static.ToLong(res.Data.Tables[0].Rows[0][2]);
                        ids.Add(index, id);
                    }
                }
                #endregion
                #region [ Захиалгын дугаар орж ирсэн бол rebate ID-г авах ]
                long orderno = Static.ToLong(ri.ReceivedParam[1]);
                if (orderno != 0)
                {
                    sql = "select rebateid, loyalid, pointid from Orders where OrderNo=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn50000x", orderno);

                    if (res.ResultNo != 0) return res;
                    if (res.Data == null)
                    {
                        res.ResultNo = 9110015;
                        res.ResultDesc = "Захиалгын уншихад алдаа гарлаа";
                        return res;
                    }
                    if (res.Data.Tables[0] == null)
                    {
                        res.ResultNo = 9110015;
                        res.ResultDesc = "Захиалгын уншихад алдаа гарлаа";
                        return res;
                    }
                    if (res.Data.Tables[0].Rows.Count > 0)
                    {
                        id = new RebateID();
                        id.rebateid = Static.ToLong(res.Data.Tables[0].Rows[0][0]);
                        id.loyalid = Static.ToLong(res.Data.Tables[0].Rows[0][1]);
                        id.pointid = Static.ToLong(res.Data.Tables[0].Rows[0][2]);
                        ids.Add(index, id);
                    }
                }
                #endregion
                #region [ Хүсэлт дээрээс Rebate ID-г авах ]
                if (contractno == 0 && orderno == 0)
                {
                    long rebateid = 0;
                    long loyalid = 0;
                    long pointid = 0;
                    if (Static.ToLong(ri.ReceivedParam[2]) != 0)
                    {
                        rebateid = Static.ToLong(ri.ReceivedParam[2]);
                    }
                    else rebateid = 0;

                    if (Static.ToLong(ri.ReceivedParam[3]) != 0)
                    {
                        loyalid = Static.ToLong(ri.ReceivedParam[3]);
                    }
                    else loyalid = 0;
                    if (Static.ToLong(ri.ReceivedParam[4]) != 0)
                    {
                        pointid = Static.ToLong(ri.ReceivedParam[4]);
                    }
                    else pointid = 0;
                    if (!(loyalid == 0 && loyalid == 0 && pointid == 0))
                    {
                        id = new RebateID();
                        id.rebateid = rebateid;
                        id.loyalid = loyalid;
                        id.pointid = pointid;
                        ids.Add(index, id);
                    }
                }
                #endregion
                #region [ Sale, Value DataTable-уудыг унших ]

                if (ri.ReceivedParam[5] != null)
                {
                    DataTable productlist = (DataTable)ri.ReceivedParam[5];
                    saleDB = new DataTable();
                    saleDB.Columns.Add("CUSTOMERNO", typeof(long));
                    saleDB.Columns.Add("PRODTYPE", typeof(int));
                    saleDB.Columns.Add("PRODNO", typeof(string));
                    saleDB.Columns.Add("PRICE", typeof(decimal));
                    saleDB.Columns.Add("QUANTITY", typeof(long));
                    saleDB.Columns.Add("SALESPRICE", typeof(decimal));

                    foreach (DataRow dr in productlist.Rows)
                    {
                        if (Static.ToInt(dr["FLAG"]) != 3)
                        {
                            DataRow drow = saleDB.NewRow();
                            drow["CUSTOMERNO"] = dr["CUSTOMERNO"];
                            drow["PRODTYPE"] = dr["PRODTYPE"];
                            drow["PRODNO"] = dr["PRODCODE"];
                            drow["PRICE"] = dr["PRICE"];
                            drow["QUANTITY"] = dr["QUANTITY"];
                            drow["SALESPRICE"] = dr["PRICE"];
                            saleDB.Rows.Add(drow);
                        }
                    }
                }
                else
                {
                    res.ResultNo = 9110015;
                    res.ResultDesc = "Борлуулалтын мэдээлэл орж ирээгүй байна.";
                    return res;
                }
                if (ri.ReceivedParam[6] != null)
                {
                    valueDB = (DataTable)(ri.ReceivedParam[6]);
                }
                else
                {
                    res.ResultNo = 9110015;
                    res.ResultDesc = "Нэмэлт мэдээлэл орж ирээгүй байна.";
                    return res;
                }
                #endregion
                #region [ Rebate ID байхгүй бол Macro ажиллуулж RebateID-нуудыг авах ]
                if (ids.Count == 0)
                {
                    tmptrankey = InsertTmpDatas(db, saleDB, valueDB);
                    if (tmptrankey == 0)
                    {
                        res.ResultNo = 1;
                        res.ResultDesc = "Temporary бааз руу оруулахад алдаа гарлаа";
                        return res;
                    }

                    foreach (object obj in gRebateFormula.RebateFormulas.Keys)
                    {
                        RebateFormula formula = (RebateFormula)gRebateFormula.RebateFormulas[obj];
                        if (formula.Status == 0 && (formula.BeginDate <= DateTime.Now && formula.EndDate >= DateTime.Now))
                        {
                            sql = formula.SQL;
                            if (sql.Trim() != "")
                            {
                                object[] resdb = ProcessPLSQL(db, tmptrankey, formula.SQL);
                                if (resdb == null)
                                {
                                    res.ResultNo = 1;
                                    res.ResultDesc = "PL SQL-ийг ажиллуулахад алдаа гарлаа";
                                    return res;
                                }
                                if (resdb.Count() > 0)
                                {
                                    if (Static.ToLong(resdb[0]) != 0 || Static.ToLong(resdb[1]) != 0 || Static.ToLong(resdb[2]) != 0)
                                    {
                                        id = new RebateID();
                                        id.rebateid = Static.ToLong(resdb[0]);
                                        id.loyalid = Static.ToLong(resdb[1]);
                                        id.pointid = Static.ToLong(resdb[2]);
                                        ids.Add(index, id);
                                        index += 1;
                                    }
                                }
                                else
                                {
                                    //id = new RebateID();
                                    //id.rebateid = 0;
                                    //id.loyalid = 0;
                                    //id.pointid = 0;
                                    //ids.Add(index, id);
                                    //index += 1;
                                }
                            }
                        }

                    }
                }
                #endregion

                #region [ Calculation ]
                object[] resData = new object[ids.Count];
                object[] oneresData;
                index = 0;
                foreach (object obj in ids.Keys)
                {
                    id = (RebateID)ids[obj];
                    oneresData = GetCalcResult(id, saleDB);
                    resData[index] = oneresData;
                    index++;
                }
                res.Param = resData;
                res.ResultNo = 0;
                #endregion

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            return res;
        }
        private object[] GetCalcResult(RebateID id, DataTable saleDB)
        {
            object[] res = new object[7];
            DataRow row;
            decimal saleprice=0;
            int saletype=0;
            decimal point=0;
            DataTable loyalDB= null;
            //saleDB нь хөнгөлсөн дүн оруулах баганатайгаа хамт ирэх ёстой гэж үзнэ.
            //
            #region [ Хөнгөлөлт ] 
            if (id.rebateid!=0)
            {
                // Хөнгөлсөн бараанууд дээр үнэ тогтоох процесс
                for(int i=0; i<saleDB.Rows.Count;i++)
                {
                    row = saleDB.Rows[i];
                    saleprice = GetPrice(row, gRebateMaster, id.rebateid);
                    saleDB.Rows[i]["SALESPRICE"] = saleprice;
                }
                // Үлдсэн бараанууд дээр нийтэд нь үнэ тогтоох 
                saleprice = GetOtherPrice(gRebateMaster, id.rebateid, ref saletype);
                if (saleprice!=0)
                {
                    for(int i=0; i<saleDB.Rows.Count;i++)
                    {
                        row = saleDB.Rows[i];
                        if (!ProductInRebate(gRebateMaster, id.rebateid,  Static.ToInt(row["ProdType"]), Static.ToStr(row["ProdNo"])))
                        {
                            switch (saletype)
                            {
                                case 1:
                                    saleDB.Rows[i]["SALESPRICE"] = Math.Round(Static.ToDecimal(saleDB.Rows[i]["Price"]) * saleprice / 100);
                                    break;
                                case 0:
                                    saleDB.Rows[i]["SALESPRICE"] = saleprice;
                                    break;
                            }
                        }
                    }
                }
            }
            res[0]= saleDB;
            #endregion

            #region [ Урамшуулал ] 
            if (id.loyalid!=0)
            {
                loyalDB = GetLoyalProduts(id.loyalid);                
            }
            #endregion

            #region [ Оноо ] 
            point=0;
            if (id.pointid!=0)
            {
                RebateMaster rebate;
                foreach (Object obj in gRebateMaster.RebateMasters.Keys)
                {
                    rebate = (RebateMaster)gRebateMaster.RebateMasters[obj];
                    if (rebate.MasterID == id.pointid && rebate.MasterType == 2)
                    {
                        point = rebate.CalcAmount;
                    }
                }
            }
            #endregion
            
            res[0] = saleDB.Copy();
            res[1] = loyalDB;
            res[2] = point;
            res[3] = id.rebateid;
            res[4] = id.loyalid;
            res[5] = id.pointid;
            res[6] = RebateName;

            return res;
        }
        decimal GetPrice(DataRow pRow, RebateMaster pRebate, long pID)
        {
            RebateMaster rebate;
            decimal price = 0;
            try
            {
                price = Static.ToDecimal(pRow["Price"]);
                foreach (Object obj in pRebate.RebateMasters.Keys)
                {
                    rebate = (RebateMaster)pRebate.RebateMasters[obj];

                    if (rebate.MasterID == pID && rebate.MasterType == 0)
                    {
                        RebateName = rebate.Name;
                        if (rebate.ProdType == Static.ToInt(pRow["ProdType"]) && rebate.ProdNo == Static.ToStr(pRow["ProdNo"]))
                        {
                            switch (rebate.CalcType)
                            {
                                case 1:
                                    price = Math.Round(price * rebate.CalcAmount / 100,2);
                                    break;
                                case 0:
                                    price = rebate.CalcAmount;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return price;
        }
        decimal GetOtherPrice(RebateMaster pRebate, long pID, ref int saletype)
        {
            RebateMaster rebate;
            decimal price = 0;
            try
            {
                foreach (Object obj in pRebate.RebateMasters.Keys)
                {
                    rebate = (RebateMaster)pRebate.RebateMasters[obj];
                    if (rebate.MasterID == pID && rebate.MasterType == 0)
                    {
                        if (rebate.ProdType == 9)
                        {
                            price = rebate.CalcAmount;
                            saletype = rebate.CalcType;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return price;
        }
        bool ProductInRebate(RebateMaster pRebate, long pID, int pProdType, string pProdNo)
        {
            RebateMaster rebate;
            decimal price = 0;
            try
            {
                foreach (Object obj in pRebate.RebateMasters.Keys)
                {
                    rebate = (RebateMaster)pRebate.RebateMasters[obj];
                    if (rebate.MasterID == pID && rebate.MasterType == 0)
                    {
                        if (rebate.ProdType == pProdType && rebate.ProdNo == pProdNo)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        DataTable GetLoyalProduts(long pID)
        {
            RebateMaster rebate;
            object[] row;
            DataTable loyalDB = null;
            try
            {
                loyalDB = new DataTable();
                loyalDB.Columns.Add("ProdType", typeof(int));
                loyalDB.Columns.Add("ProdNo", typeof(string));
                loyalDB.Columns.Add("ProdName", typeof(string));

                foreach (Object obj in gRebateMaster.RebateMasters.Keys)
                {
                    rebate = (RebateMaster)gRebateMaster.RebateMasters[obj];
                    if (rebate.MasterID == pID && rebate.MasterType == 1)
                    {
                        if (rebate.ProdType == 0 || rebate.ProdType == 0)
                        {
                            row = new object[3];
                            row[0] = rebate.ProdType;
                            row[1] = rebate.ProdNo;
                            row[2] = rebate.ProdName;
                            loyalDB.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return loyalDB;
        }
        long InsertTmpDatas(DbConnections db, DataTable pSaleDB, DataTable pValueDB)
        {
            long key = 0; 
            Result res = new Result();
            try
            {
                key = DateTime.Now.Ticks;

                string sql = " insert into tmpSale (SaleID, CustomerID, ProdType, ProdNo, Price, Quantity) values (:1, :2, :3, :4, :5, :6)";
                foreach (DataRow row in pSaleDB.Rows)
                {
                    res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn500099", key,
                        Static.ToStr(row["CustomerNo"]),
                        Static.ToInt(row["ProdType"]),
                        Static.ToStr(row["ProdNo"]),
                        Static.ToDecimal(row["Price"]),
                        Static.ToDecimal(row["Quantity"]));
                    if (res.ResultNo != 0) return 0;
                }

                sql = " insert into tmpSaleValue (SaleID, ItemName, ItemValue) values (:1, :2, :3)";
                foreach (DataRow row in pValueDB.Rows)
                {
                    res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn500099", key,
                        Static.ToStr(row["ItemName"]),
                        Static.ToInt(row["ItemValue"]));
                    if (res.ResultNo != 0) return 0;
                }
                return key;
            }
            catch (Exception ex)
            {
                return 0;
            }
            return key;
        }
        object[] ProcessPLSQL(DbConnections db, long pkey, string PLSQLName)
        {
            object[] obj = new object[3];  
            try
            {
                 
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = PLSQLName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pID", OracleDbType.Decimal, ParameterDirection.Input);
                cmd.Parameters.Add("rebateID", OracleDbType.Decimal, ParameterDirection.Output);
                cmd.Parameters.Add("loyalID", OracleDbType.Decimal, ParameterDirection.Output);
                cmd.Parameters.Add("pointID", OracleDbType.Decimal, ParameterDirection.Output);
                cmd.Parameters["pID"].Value = pkey;
                
                Result res = db.ExecuteQuery("core", cmd, enumCommandType.SELECT, "Txn50000x");
                if(res.ResultNo!=0) return obj;

                obj[0] = cmd.Parameters["rebateID"].Value;
                obj[1] = cmd.Parameters["loyalID"].Value;
                obj[2] = cmd.Parameters["pointID"].Value;
            }
            catch (Exception ex)
            {
                obj = null;
                return obj;
            }
            return obj;
        }
    }
}
