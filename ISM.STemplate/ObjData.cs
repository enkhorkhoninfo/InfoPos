using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Text;

using EServ.Interface;
using EServ.Shared;
using EServ.Data;

using ISM.DB;

namespace ISM.STemplate
{
    public class ObjData : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 105001:
                        r = FN105001(ci, ri, db, ref lg);
                        break;
                    case 105002:
                        r = FN105002(ci, ri, db, ref lg);
                        break;
                    case 105003:
                        r = FN105003(ci, ri, db, ref lg);
                        break;
                    case 105004:
                        r = FN105004(ci, ri, db, ref lg);
                        break;

                    case 105101:
                        r = FN105101(ci, ri, db, ref lg);
                        break;
                    case 105102:
                        r = FN105102(ci, ri, db, ref lg);
                        break;
                    case 105103:
                        r = FN105103(ci, ri, db, ref lg);
                        break;
                    case 105104:
                        r = FN105104(ci, ri, db, ref lg);
                        break;
                    case 105105:
                        r = FN105105(ci, ri, db, ref lg);
                        break;
                }
            }
            catch
            { }
            return r;
        }

        #region [ FN105001 - Getting Dynamic Pivot Table]

        public Result FN105001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            DataSet ds = new DataSet();
            DataTable dt1 = null;
            DataTable dt2 = null;
            DataTable dt3 = null;
            DataColumn col = null;
            try
            {
                #region Preparing Parameters
                string tableprefix = (string)ri.ReceivedParam[0];
                ulong typeid = (ulong)ri.ReceivedParam[1];
                ulong recordid = (ulong)ri.ReceivedParam[2];
                int objectid = (int)ri.ReceivedParam[3];
                int prowno = (int)ri.ReceivedParam[4];
                bool showfixed = (bool)ri.ReceivedParam[5];
                ulong prodcode = (ulong)ri.ReceivedParam[6];
                #endregion

                #region Getting Table Data
                r = Main.DB105001(db, typeid, objectid);
                if (r.ResultNo != 0) goto OnExit;

                dt1 = r.Data.Tables[0];
                dt1.TableName = "Definition";

                if (prodcode == 0)
                {
                    r = Main.DB105002(db, tableprefix, recordid, objectid, prowno);
                    if (r.ResultNo != 0) goto OnExit;
                }
                else
                {
                    r = Main.DB105012(db, tableprefix, prodcode, objectid);
                    if (r.ResultNo != 0) goto OnExit;
                }

                dt2 = r.Data.Tables[0];
                dt2.TableName = "Fixed";
                if (prodcode == 0)
                {
                    r = Main.DB105003(db, tableprefix, recordid, objectid, prowno);
                    if (r.ResultNo != 0) goto OnExit;
                }
                else
                {
                    r = Main.DB105013(db, tableprefix, prodcode, objectid);
                    if (r.ResultNo != 0) goto OnExit;
                }

                dt3 = r.Data.Tables[0];
                dt3.TableName = "Values";

                #endregion

                DataTable pivot = new DataTable();

                col = new DataColumn();
                col.ColumnName = "rowno";
                col.Caption = "ДД#";
                pivot.Columns.Add(col);

                #region Add Fixed Columns
                if (showfixed)
                {
                    col = new DataColumn();
                    col.ColumnName = "status";
                    col.Caption = "ТӨЛӨВ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "startdate";
                    col.Caption = "ЭХЛЭХ ОГНОО";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "enddate";
                    col.Caption = "ДУУСАХ ОГНОО";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "estimateamount";
                    col.Caption = "ҮНЭЛГЭЭ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "estimatecurcode";
                    col.Caption = "ҮНЭЛГЭЭ ВАЛЮТ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "feetype";
                    col.Caption = "ХО ТӨРӨЛ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "feerate";
                    col.Caption = "ХО%";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "feeamount";
                    col.Caption = "ХО ДҮН";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "feecurcode";
                    col.Caption = "ХО ВАЛЮТ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "currate";
                    col.Caption = "ХАНШ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "discountrate";
                    col.Caption = "ХӨНГӨЛӨЛТ%";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "discountamount";
                    col.Caption = "НЭМЭГДҮҮЛЭЛТ ХАСАГДУУЛАЛТ ДҮН";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "discountcurcode";
                    col.Caption = "ХӨНГӨЛӨЛТ ВАЛЮТ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "optionid";
                    col.Caption = "ХАМААРАЛТАЙ СОНГОЛТ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "selectstatus";
                    col.Caption = "ХУРААМЖИЙН ТӨЛӨВ";
                    pivot.Columns.Add(col);


                    col = new DataColumn();
                    col.ColumnName = "feediscounttype";
                    col.Caption = "ХӨНГӨЛӨЛТИЙН ТӨРӨЛ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "feediscountamount";
                    col.Caption = "ХӨНГӨЛӨЛТИЙН ДҮН";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "feediscountrate";
                    col.Caption = "ХӨНГӨЛӨЛТИЙН ВАЛЮТ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "calcamount";
                    col.Caption = "ТООЦООЛСОН ДҮН";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "calcrate";
                    col.Caption = "ТООЦООЛСОН%";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "unoptionid";
                    col.Caption = "ХАМААРАЛГҮЙ СОНГОЛТ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "claimamount";
                    col.Caption = "ӨМНӨХ НТ-Н ДҮН";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "marketvalue";
                    col.Caption = "ЗАХ ЗЭЭЛИЙН ҮНЭЛГЭЭ";
                    pivot.Columns.Add(col);

                    col = new DataColumn();
                    col.ColumnName = "recordid";
                    col.Caption = "ХЭЛЦЛИЙН ДУГААР";
                    pivot.Columns.Add(col);
                }

                #endregion

                #region Add Dynamic Columns

                foreach (DataRow row in dt1.Rows)
                {
                    col = new DataColumn();
                    col.ColumnName = "C" + ISM.Lib.Static.ToStr(row["itemid"]);
                    col.Caption = ISM.Lib.Static.ToStr(row["name"]).ToUpper();
                    pivot.Columns.Add(col);
                }

                #endregion

                #region Create Rows

                //pivot тэйблийн мөрийн дугаар, RowNo утга хоёрыг холбосон лист.
                //RowNo дугаараар тэйблийн мөрийн дугаарыг олж баганын утга олгоход ашиглана.
                Hashtable map_row_index = new Hashtable();
                int rowno = 0;
                int rowindex = 0;
                foreach (DataRow row in dt2.Rows)
                {
                    pivot.Rows.Add();
                    rowindex = pivot.Rows.Count - 1;

                    rowno = ISM.Lib.Static.ToInt(row["rowno"]);
                    map_row_index[rowno] = rowindex;

                    pivot.Rows[rowindex]["rowno"] = rowno;

                    #region Set fixed values
                    if (showfixed)
                    {
                        pivot.Rows[rowindex]["status"] = row["status"] == DBNull.Value ? DBNull.Value : (object)Static.ToInt(row["status"]);
                        pivot.Rows[rowindex]["startdate"] = row["startdate"] == DBNull.Value ? DBNull.Value : (object)Static.ToDate(row["startdate"]);
                        pivot.Rows[rowindex]["enddate"] = row["enddate"] == DBNull.Value ? DBNull.Value : (object)Static.ToDate(row["enddate"]);
                        pivot.Rows[rowindex]["estimateamount"] = row["estimateamount"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["estimateamount"]);
                        pivot.Rows[rowindex]["estimatecurcode"] = row["estimatecurcode"] == DBNull.Value ? DBNull.Value : (object)Static.ToStr(row["estimatecurcode"]);
                        pivot.Rows[rowindex]["feetype"] = row["feetype"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["feetype"]);
                        pivot.Rows[rowindex]["feerate"] = row["feerate"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["feerate"]);
                        pivot.Rows[rowindex]["feeamount"] = row["feeamount"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["feeamount"]);
                        pivot.Rows[rowindex]["feecurcode"] = row["feecurcode"] == DBNull.Value ? DBNull.Value : (object)Static.ToStr(row["feecurcode"]);
                        pivot.Rows[rowindex]["currate"] = row["currate"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["currate"]);
                        pivot.Rows[rowindex]["discountrate"] = row["discountrate"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["discountrate"]);
                        pivot.Rows[rowindex]["discountamount"] = row["discountamount"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["discountamount"]);
                        pivot.Rows[rowindex]["discountcurcode"] = row["discountcurcode"] == DBNull.Value ? DBNull.Value : (object)Static.ToStr(row["discountcurcode"]);
                        pivot.Rows[rowindex]["optionid"] = row["optionid"] == DBNull.Value ? DBNull.Value : (object)Static.ToStr(row["optionid"]);
                        pivot.Rows[rowindex]["selectstatus"] = row["selectstatus"] == DBNull.Value ? DBNull.Value : (object)Static.ToInt(row["selectstatus"]);
                        pivot.Rows[rowindex]["feediscounttype"] = row["feediscounttype"] == DBNull.Value ? DBNull.Value : (object)Static.ToInt(row["feediscounttype"]);
                        pivot.Rows[rowindex]["feediscountamount"] = row["feediscountamount"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["feediscountamount"]);
                        pivot.Rows[rowindex]["feediscountrate"] = row["feediscountrate"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["feediscountrate"]);
                        pivot.Rows[rowindex]["calcamount"] = row["calcamount"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["calcamount"]);
                        pivot.Rows[rowindex]["calcrate"] = row["feediscountrate"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["calcrate"]);
                        pivot.Rows[rowindex]["unoptionid"] = row["unoptionid"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["unoptionid"]);
                        pivot.Rows[rowindex]["claimamount"] = row["claimamount"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["claimamount"]);
                        pivot.Rows[rowindex]["marketvalue"] = row["marketvalue"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["marketvalue"]);
                        pivot.Rows[rowindex]["recordid"] = row["recordid"] == DBNull.Value ? DBNull.Value : (object)Static.ToDecimal(row["recordid"]);
                    }
                    #endregion
                }

                #endregion

                #region Setting Values

                string c;
                foreach (DataRow row in dt3.Rows)
                {
                    rowno = ISM.Lib.Static.ToInt(row["rowno"]);
                    if (map_row_index.ContainsKey(rowno))
                    {
                        rowindex = (int)map_row_index[rowno];

                        c = "C" + ISM.Lib.Static.ToStr(row["itemid"]);
                        if (pivot.Columns.Contains(c))
                            pivot.Rows[rowindex][c] = row["value"]; // rowno is started 1, then convert into index based
                    }
                }

                #endregion

                #region Preparing Result

                ds.Tables.Add(dt1.Copy());
                ds.Tables.Add(pivot);

                dt1.Dispose();
                dt2.Dispose();
                dt3.Dispose();

                r = new Result(0, "");
                r.AffectedRows = dt2.Rows.Count;
                r.Data = ds;

                #endregion
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }

        OnExit:
            return r;
        }

        #endregion
        #region [ FN105002 - Save Dynamic Pivot Table]

        public Result FN105002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            DataTable dt1 = null;
            DbConnection conn = null;
            int START_INDEX_DYNAMIC_COL = 23;
            try
            {
                #region Preparing Parameters

                string tableprefix = (string)ri.ReceivedParam[0];
                ulong recordid = (ulong)ri.ReceivedParam[1];
                int objectid = (int)ri.ReceivedParam[2];
                int prowno = (int)ri.ReceivedParam[3];
                bool showfixed = (bool)ri.ReceivedParam[4];
                dt1 = (DataTable)ri.ReceivedParam[5];

                #endregion

                conn = db.BeginTransaction("core", "FN105002");

                int rn;
                foreach (DataRow row in dt1.Rows)
                {
                    rn = ISM.Lib.Static.ToInt(row["rowno"]);
                    if (showfixed)
                    {
                        #region Collectiong Values

                        int status = ISM.Lib.Static.ToInt(row["status"]);
                        DateTime startdate = ISM.Lib.Static.ToDate(row["startdate"]);
                        DateTime enddate = ISM.Lib.Static.ToDate(row["enddate"]);
                        int feetype = ISM.Lib.Static.ToInt(row["feetype"]);
                        decimal feerate = ISM.Lib.Static.ToDecimal(row["feerate"]);
                        decimal feeamount = ISM.Lib.Static.ToDecimal(row["feeamount"]);
                        string feecurcode = ISM.Lib.Static.ToStr(row["feecurcode"]);
                        decimal discountrate = ISM.Lib.Static.ToDecimal(row["discountrate"]);
                        decimal discountamount = ISM.Lib.Static.ToDecimal(row["discountamount"]);
                        string discountcurcode = ISM.Lib.Static.ToStr(row["discountcurcode"]);
                        decimal currate = ISM.Lib.Static.ToDecimal(row["currate"]);
                        decimal estimateamount = ISM.Lib.Static.ToDecimal(row["estimateamount"]);
                        string estimatecurcode = ISM.Lib.Static.ToStr(row["estimatecurcode"]);
                        int optionid = ISM.Lib.Static.ToInt(row["optionid"]);
                        int selectstatus = ISM.Lib.Static.ToInt(row["selectstatus"]);
                        int feediscounttype = ISM.Lib.Static.ToInt(row["feediscounttype"]);
                        decimal feediscountamount = ISM.Lib.Static.ToDecimal(row["feediscountamount"]);
                        decimal feediscountrate = ISM.Lib.Static.ToDecimal(row["feediscountrate"]);
                        decimal calcamount = ISM.Lib.Static.ToDecimal(row["calcamount"]);
                        decimal calcrate = ISM.Lib.Static.ToDecimal(row["calcrate"]);
                        int unoptionid = ISM.Lib.Static.ToInt(row["unoptionid"]);
                        decimal claimamount = ISM.Lib.Static.ToDecimal(row["claimamount"]);
                        decimal marketvalue = ISM.Lib.Static.ToDecimal(row["marketvalue"]);

                        #endregion

                        #region Saving Fixed Values

                        // Update Fixed Values
                        r = Main.DB105005(db, tableprefix, recordid, objectid, prowno, rn
                            , status, startdate, enddate, feetype, feerate, feeamount, feecurcode
                            , discountrate, discountamount, discountcurcode, currate, estimateamount, estimatecurcode, optionid, selectstatus, feediscounttype, feediscountamount, feediscountrate, calcamount, calcrate, unoptionid, claimamount, marketvalue);
                        if (r.ResultNo != 0) goto OnExit;

                        if (r.AffectedRows <= 0)
                        {
                            // If there is no updated rows, then insert new row
                            r = Main.DB105006(db, tableprefix, recordid, objectid, prowno, rn
                                , status, startdate, enddate, feetype, feerate, feeamount, feecurcode
                                , discountrate, discountamount, discountcurcode, currate, estimateamount, estimatecurcode, optionid, selectstatus, feediscounttype, feediscountamount, feediscountrate, calcamount, calcrate, unoptionid, claimamount, marketvalue);
                            if (r.ResultNo != 0) goto OnExit;
                        }

                        #endregion
                    }

                    #region Saving Dynamic Values

                    int startindex = showfixed ? START_INDEX_DYNAMIC_COL : 0;
                    for (int c = startindex; c < dt1.Columns.Count; c++)
                    {
                        int itemid = ISM.Lib.Static.ToInt(dt1.Columns[c].ColumnName.Substring(1));
                        string value = ISM.Lib.Static.ToStr(row[c]);

                        // Update Dynamic Values
                        r = Main.DB105007(db, tableprefix, recordid, objectid, prowno, rn, itemid, value);
                        if (r.ResultNo != 0) goto OnExit;

                        if (r.AffectedRows <= 0)
                        {
                            // If there is no update rows, then insert new row
                            r = Main.DB105008(db, tableprefix, recordid, objectid, prowno, rn, itemid, value);
                            if (r.ResultNo != 0) goto OnExit;
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            finally
            {
                if (r != null && conn != null)
                {
                    if (r.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
                }
            }

        OnExit:
            return r;
        }

        #endregion
        #region [ FN105003 - Delete Dynamic Pivot Table Row]

        public Result FN105003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            DbConnection conn = null;
            try
            {
                #region Preparing Parameters

                string tableprefix = (string)ri.ReceivedParam[0];
                ulong recordid = (ulong)ri.ReceivedParam[1];
                int objectid = (int)ri.ReceivedParam[2];
                int prowno = (int)ri.ReceivedParam[3];
                int rowno = (int)ri.ReceivedParam[4];

                #endregion

                conn = db.BeginTransaction("core", "FN105003");

                #region Delete Row from Fixed table
                r = Main.DB105010(db, tableprefix, recordid, objectid, prowno, rowno);
                if (r.ResultNo != 0) return r;
                #endregion

                #region Delete Rows from Dynamic Records
                r = Main.DB105009(db, tableprefix, recordid, objectid, prowno, rowno);
                #endregion
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            finally
            {
                if (r != null && conn != null)
                {
                    if (r.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
                }
            }

        OnExit:
            return r;
        }

        #endregion
        #region [ FN105004 - Get Max RowNo from Dynamic Pivot Tables]

        public Result FN105004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                #region Preparing Parameters

                string tableprefix = (string)ri.ReceivedParam[0];
                ulong recordid = (ulong)ri.ReceivedParam[1];
                int objectid = (int)ri.ReceivedParam[2];
                int prowno = (int)ri.ReceivedParam[3];

                #endregion

                r = Main.DB105011(db, tableprefix, recordid, objectid, prowno);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }

        #endregion

        #region [ FN105101 - Select All Parameter Info ]

        public Result FN105101(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string tablename = ISM.Lib.Static.ToStr(ri.ReceivedParam[0]);
                ulong typeid = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                r = Main.DB105101(db, tablename, typeid);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }

        #endregion
        #region [ FN105102 - Merge Parameter Info ]

        public Result FN105102(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string tablename = ISM.Lib.Static.ToStr(ri.ReceivedParam[0]);
                ulong typeid = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                int itemid = ISM.Lib.Static.ToInt(ri.ReceivedParam[2]);
                string name = ISM.Lib.Static.ToStr(ri.ReceivedParam[3]);
                string name2 = ISM.Lib.Static.ToStr(ri.ReceivedParam[4]);
                int vtype = ISM.Lib.Static.ToInt(ri.ReceivedParam[5]);
                int vlen = ISM.Lib.Static.ToInt(ri.ReceivedParam[6]);
                string vdef = ISM.Lib.Static.ToStr(ri.ReceivedParam[7]);
                int mand = ISM.Lib.Static.ToInt(ri.ReceivedParam[8]);
                string mask = ISM.Lib.Static.ToStr(ri.ReceivedParam[9]);
                string desc = ISM.Lib.Static.ToStr(ri.ReceivedParam[10]);
                string dictid = ISM.Lib.Static.ToStr(ri.ReceivedParam[11]);
                int dicted = ISM.Lib.Static.ToInt(ri.ReceivedParam[12]);
                string dictf1 = ISM.Lib.Static.ToStr(ri.ReceivedParam[13]);
                string dictf2 = ISM.Lib.Static.ToStr(ri.ReceivedParam[14]).Trim();
                int orderno = ISM.Lib.Static.ToInt(ri.ReceivedParam[15]);
                int calculate = ISM.Lib.Static.ToInt(ri.ReceivedParam[16]);
                long dictparent = ISM.Lib.Static.ToLong(ri.ReceivedParam[17]);
                string dictfilter = ISM.Lib.Static.ToStr(ri.ReceivedParam[18]);

                r = Main.DB105102(db, tablename, typeid, itemid, name, name2, vtype, vlen, vdef, mand, mask, desc, dictid, dicted, dictf1, dictf2, orderno, calculate, dictparent, dictfilter);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }

        #endregion
        #region [ FN105103 - Delete Parameter Info ]

        public Result FN105103(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string tablename = ISM.Lib.Static.ToStr(ri.ReceivedParam[0]);
                ulong typeid = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                int itemid = ISM.Lib.Static.ToInt(ri.ReceivedParam[2]);
                r = Main.DB105103(db, tablename, typeid, itemid);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }

        #endregion
        #region [ FN105104 - Update Parameter Info ]

        public Result FN105104(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string tablename = ISM.Lib.Static.ToStr(ri.ReceivedParam[0]);
                ulong typeid = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                int itemid = ISM.Lib.Static.ToInt(ri.ReceivedParam[2]);
                string name = ISM.Lib.Static.ToStr(ri.ReceivedParam[3]);
                string name2 = ISM.Lib.Static.ToStr(ri.ReceivedParam[4]);
                int vtype = ISM.Lib.Static.ToInt(ri.ReceivedParam[5]);
                int vlen = ISM.Lib.Static.ToInt(ri.ReceivedParam[6]);
                string vdef = ISM.Lib.Static.ToStr(ri.ReceivedParam[7]);
                int mand = ISM.Lib.Static.ToInt(ri.ReceivedParam[8]);
                string mask = ISM.Lib.Static.ToStr(ri.ReceivedParam[9]);
                string desc = ISM.Lib.Static.ToStr(ri.ReceivedParam[10]);
                string dictid = ISM.Lib.Static.ToStr(ri.ReceivedParam[11]);
                int dicted = ISM.Lib.Static.ToInt(ri.ReceivedParam[12]);
                string dictf1 = ISM.Lib.Static.ToStr(ri.ReceivedParam[13]);
                string dictf2 = ISM.Lib.Static.ToStr(ri.ReceivedParam[14]).Trim();
                int orderno = ISM.Lib.Static.ToInt(ri.ReceivedParam[15]);
                int calculate = ISM.Lib.Static.ToInt(ri.ReceivedParam[16]);
                long dictparent = ISM.Lib.Static.ToLong(ri.ReceivedParam[17]);
                string dictfilter = ISM.Lib.Static.ToStr(ri.ReceivedParam[18]);

                r = Main.DB105104(db, tablename, typeid, itemid, name, name2, vtype, vlen, vdef, mand, mask, desc, dictid, dicted, dictf1, dictf2, orderno, calculate, dictparent, dictfilter);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }

        #endregion
        #region [ FN105105 - Insert Parameter Info ]

        public Result FN105105(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string tablename = ISM.Lib.Static.ToStr(ri.ReceivedParam[0]);
                ulong typeid = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                int itemid = ISM.Lib.Static.ToInt(ri.ReceivedParam[2]);
                string name = ISM.Lib.Static.ToStr(ri.ReceivedParam[3]);
                string name2 = ISM.Lib.Static.ToStr(ri.ReceivedParam[4]);
                int vtype = ISM.Lib.Static.ToInt(ri.ReceivedParam[5]);
                int vlen = ISM.Lib.Static.ToInt(ri.ReceivedParam[6]);
                string vdef = ISM.Lib.Static.ToStr(ri.ReceivedParam[7]);
                int mand = ISM.Lib.Static.ToInt(ri.ReceivedParam[8]);
                string mask = ISM.Lib.Static.ToStr(ri.ReceivedParam[9]);
                string desc = ISM.Lib.Static.ToStr(ri.ReceivedParam[10]);
                string dictid = ISM.Lib.Static.ToStr(ri.ReceivedParam[11]);
                int dicted = ISM.Lib.Static.ToInt(ri.ReceivedParam[12]);
                string dictf1 = ISM.Lib.Static.ToStr(ri.ReceivedParam[13]);
                string dictf2 = ISM.Lib.Static.ToStr(ri.ReceivedParam[14]).Trim();
                int orderno = ISM.Lib.Static.ToInt(ri.ReceivedParam[15]);
                int calculate = ISM.Lib.Static.ToInt(ri.ReceivedParam[16]);
                long dictparent = ISM.Lib.Static.ToLong(ri.ReceivedParam[17]);
                string dictfilter = ISM.Lib.Static.ToStr(ri.ReceivedParam[18]);

                r = Main.DB105105(db, tablename, typeid, itemid, name, name2, vtype, vlen, vdef, mand, mask, desc, dictid, dicted, dictf1, dictf2, orderno, calculate,dictparent,dictfilter);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }

        #endregion
    }
}
