using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing.Imaging;

using EServ.Interface;
using EServ.Shared;
using EServ.Data;

using ISM.DB;

namespace ISM.STemplate
{
    public class Attach : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 103001:
                        r = FN103001(ci, ri, db, ref lg);
                        break;
                    case 103002:
                        r = FN103002(ci, ri, db, ref lg);
                        break;
                    case 103003:
                        r = FN103003(ci, ri, db, ref lg);
                        break;

                    case 103101:
                        r = FN103101(ci, ri, db, ref lg);
                        break;
                    case 103102:
                        r = FN103102(ci, ri, db, ref lg);
                        break;

                }
            }
            catch
            { }
            return r;
        }

        #region Getting Attach Data
        /// <summary>
        /// ulong attachid  = (ulong)ri.ReceivedParam[0];
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="ri"></param>
        /// <param name="db"></param>
        /// <param name="lg"></param>
        /// <returns>
        /// Result.Data.Tables[0] - Attach Main Table
        /// Result.Data.tables[1] - Attach Link Table
        /// </returns>
        public Result FN103001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                ulong attachid = (ulong)ri.ReceivedParam[0];
                r = Main.DB103001(db,attachid);
                if (r.ResultNo == 0)
                {
                    r.Data.Tables[0].TableName = "Attach";

                    #region Холбоос мэдээллийн тэйблийг нэмэх

                    Result rAttachLink = Main.DB103011(db, attachid);
                    if (rAttachLink.ResultNo != 0)
                    {
                        r = rAttachLink;
                    }
                    else
                    {
                        rAttachLink.Data.Tables[0].TableName = "AttachLink";
                        r.Data.Tables.Add(rAttachLink.Data.Tables[0].Copy());
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion

        #region Saving Attach Data
        /// <summary>
        /// ulong attachid  = (ulong)ri.ReceivedParam[0];
        /// int attachtype  = (int)ri.ReceivedParam[1];
        /// byte[] blob     = (byte[])ri.ReceivedParam[2];
        /// int typecode    = (int)ri.ReceivedParam[3];
        /// ulong typeid    = (ulong)ri.ReceivedParam[4];
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="ri"></param>
        /// <param name="db"></param>
        /// <param name="lg"></param>
        /// <returns>
        /// Result.Param[0] - Attach Id of newly inserted record.
        /// </returns>
        public Result FN103002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                ulong attachid = (ulong)ri.ReceivedParam[0];
                int attachtype = (int)ri.ReceivedParam[1];
                byte[] blob = (byte[])ri.ReceivedParam[2];

                int typecode = (int)ri.ReceivedParam[3];
                string typeid = (string)ri.ReceivedParam[4];
                string filename = (string)ri.ReceivedParam[5];
                string description = (string)ri.ReceivedParam[6];

                if (attachid == 0)
                {
                    #region Insert new attach items

                    attachid = Sequence.NextByVal("attach");

                    r = Main.DB103003(db, attachid, attachtype, blob, ri.UserNo, filename, description);
                    if (r.ResultNo == 0)
                    {
                        r = Main.DB103013(db, attachid, typecode, typeid);
                    }
                    #endregion
                }
                else
                {
                    #region Update existing attach items

                    r = Main.DB103002(db, attachid, attachtype, blob, ri.UserNo, filename,description);
                    if (r.ResultNo == 0)
                    {
                        r = Main.DB103012(db, attachid, typecode, typeid);
                    }

                    #endregion
                }
                r.Param = new object[] { attachid,attachtype,filename,description };
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion

        #region Deleting Attach Data
        /// <summary>
        /// ulong attachid  = (ulong)ri.ReceivedParam[0];
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="ri"></param>
        /// <param name="db"></param>
        /// <param name="lg"></param>
        /// <returns>
        /// </returns>
        public Result FN103003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                ulong attachid = (ulong)ri.ReceivedParam[0];
                
                r = Main.DB103004(db, attachid);
                if (r.ResultNo == 0)
                {
                    r = Main.DB103014(db, attachid);
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion

        #region Getting Attach List
        public Result FN103101(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                int typecode = (int)ri.ReceivedParam[0];
                string typeid = (string)ri.ReceivedParam[1];
                int attachtype = (int)ri.ReceivedParam[2];
                r = Main.DB103101(db, typecode, typeid, attachtype );
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion
        #region Getting Attach Thumbnail List
        public Result FN103102(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                int typecode = (int)ri.ReceivedParam[0];
                string typeid = (string)ri.ReceivedParam[1];
                r = Main.DB103102(db, typecode, typeid);

                if (r.ResultNo == 0)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = r.Data.Tables[0].Clone();
                    foreach (DataRow row in r.Data.Tables[0].Rows)
                    {
                        #region Getting thumbnail images
                        byte[] image = null;
                        int type = ISM.Lib.Static.ToInt(row["attachtype"]);
                        switch (type)
                        {
                            case 0: // Picture
                                image = ISM.Lib.Static.ImageResize((byte[])row["attachblob"], 64, 64);
                                break;
                            case 1: // Document
                                image = null;
                                break;
                            default: // Others
                                image = null;
                                break;
                        }
                        #endregion
                        #region Adding rows
                        dt.Rows.Add(
                             row["filename"]
                            ,row["attachdate"]
                            ,row["attachtype"]
                            ,row["userno"]
                            ,row["attachid"]
                            ,row["attachsize"]
                            ,row["description"]
                            ,image);
                        #endregion
                    }
                    ds.Tables.Add(dt);
                    r.Data.Dispose();
                    r.Data = ds;
                }
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
