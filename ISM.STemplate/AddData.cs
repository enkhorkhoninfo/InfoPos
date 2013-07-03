using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Interface;
using EServ.Shared;
using EServ.Data;

using ISM.DB;

namespace ISM.STemplate
{
    public class AddData : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 104001:
                        r = FN104001(ci, ri, db, ref lg);
                        break;
                    case 104002:
                        r = FN104002(ci, ri, db, ref lg);
                        break;
                }
            }
            catch
            { }
            return r;
        }

        #region Getting Dynamic Data
        /// <summary>
        /// string tableprefix  = Static.ToStr(ri.ReceivedParam[0]);
        /// ulong key           = (ulong)ri.ReceivedParam[1];
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="ri"></param>
        /// <param name="db"></param>
        /// <param name="lg"></param>
        /// <returns></returns>
        public Result FN104001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string tableprefix = Static.ToStr(ri.ReceivedParam[0]); // Table name prefix of Dynamic Data
                ulong typeid = (ulong)ri.ReceivedParam[1]; // Dynamic Data Type ID no
                ulong key = (ulong)ri.ReceivedParam[2]; // Dynamic Data Rows
                r = Main.DB104001(db, tableprefix, typeid, key);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion
        #region Saving Dynamic Data
        /// <summary>
        /// string tableprefix  = Static.ToStr(ri.ReceivedParam[0]);
        /// ulong key           = (ulong)ri.ReceivedParam[1];
        /// object[] rows       = (object[])ri.ReceivedParam[2];
        ///     ulong id        = (ulong)Static.ToLong(row[0]);
        ///     int type        = Static.ToInt(row[1]);
        ///     string value    = Static.ToStr(row[2]);
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="ri"></param>
        /// <param name="db"></param>
        /// <param name="lg"></param>
        /// <returns></returns>
        public Result FN104002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            DbConnection conn = null;
            try
            {
                string tableprefix = Static.ToStr(ri.ReceivedParam[0]); // Table name prefix of Dynamic Data
                ulong key = (ulong)ri.ReceivedParam[1]; // Dynamic data Record Key
                object[] rows = (object[])ri.ReceivedParam[2]; // Dynamic Data Rows

                conn = db.BeginTransaction("core", "104002");

                foreach (object[] row in rows)
                {
                    ulong id = (ulong)Static.ToLong(row[0]);
                    int type = Static.ToInt(row[1]);
                    string value = Static.ToStr(row[2]);
                    ulong attachid = (ulong)Static.ToLong(row[3]);

                    r = Main.DB104002(db, tableprefix, key, id, value, attachid);
                    if (r.ResultNo != 0) break;
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            finally
            {
                if (r != null)
                {
                    if (conn != null && r.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
                }
            }
            return r;
        }
        #endregion

    }
}
