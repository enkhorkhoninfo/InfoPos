using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Interface;
using EServ.Shared;
using EServ.Data;

using ISM.DB;

namespace ISM.STemplate
{
    public class StepData : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 108301:
                        r = FN108301(ci, ri, db, ref lg);
                        break;
                    case 108302:
                        r = FN108302(ci, ri, db, ref lg);
                        break;
                    case 108303:
                        r = FN108303(ci, ri, db, ref lg);
                        break;
                    case 108401:
                        r = FN108401(ci, ri, db, ref lg);
                        break;
                }
            }
            catch
            { }
            return r;
        }

        #region [ FN108301 - Dynamic Step Record List ]
        public Result FN108301(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                int pTypeCode = ISM.Lib.Static.ToInt(ri.ReceivedParam[0]);
                ulong pTypeId = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                int pStepId = ISM.Lib.Static.ToInt(ri.ReceivedParam[2]);

                r = Main.DB108301(db, pTypeCode, pTypeId, pStepId);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion
        #region [ FN108302 - Dynamic Step Record Select ]
        public Result FN108302(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                int pTypeCode = ISM.Lib.Static.ToInt(ri.ReceivedParam[0]);
                ulong pTypeId = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                int pStepId = ISM.Lib.Static.ToInt(ri.ReceivedParam[2]);
                int pStepItemId = ISM.Lib.Static.ToInt(ri.ReceivedParam[3]);

                r = Main.DB108302(db, pTypeCode, pTypeId, pStepId, pStepItemId);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion
        #region [ FN108303 - Dynamic Step Record Merge ]
        public Result FN108303(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            DbConnection conn = null;
            try
            {
                #region Param
                int pTypeCode = ISM.Lib.Static.ToInt(ri.ReceivedParam[0]);
                ulong pTypeId = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                int pStepId = ISM.Lib.Static.ToInt(ri.ReceivedParam[2]);

                int pStepItemId = ISM.Lib.Static.ToInt(ri.ReceivedParam[3]);
                int pOwner = ISM.Lib.Static.ToInt(ri.ReceivedParam[4]);
                decimal pPerformance = ISM.Lib.Static.ToDecimal(ri.ReceivedParam[5]);
                DateTime pStarted = ISM.Lib.Static.ToDateTime(ri.ReceivedParam[6]);
                DateTime pFinished = ISM.Lib.Static.ToDateTime(ri.ReceivedParam[7]);
                int pStatus = ISM.Lib.Static.ToInt(ri.ReceivedParam[8]);

                DateTime pSysDate = DateTime.Now; //ISM.Lib.Static.ToDateTime(ri.ReceivedParam[9]);
                decimal pProgress = ISM.Lib.Static.ToDecimal(ri.ReceivedParam[10]);
                string pComment = ISM.Lib.Static.ToStr(ri.ReceivedParam[11]);
                int pNewOwner = ISM.Lib.Static.ToInt(ri.ReceivedParam[12]);
                #endregion

                int owner = pNewOwner != 0 ? pNewOwner : pOwner;

                if (pPerformance == 0)
                {
                    owner = 0;
                    pStarted = DateTime.MinValue;
                    pFinished = DateTime.MinValue;
                    pStatus = 0;
                }
                conn = db.BeginTransaction("core", "FN108303");
                r = Main.DB108303(db, pTypeCode, pTypeId, pStepId, pStepItemId, owner, pPerformance, pStarted, pFinished, pStatus);
                if (r.ResultNo != 0) goto OnExit;

                if (pProgress != 0 || !string.IsNullOrEmpty(pComment) || pNewOwner != 0)
                {
                    ulong txnid = EServ.Interface.Sequence.NextByVal("StepTxnId");
                    r = Main.DB108403(db, txnid, pTypeCode, pTypeId, pStepItemId, owner, pStarted, ri.UserNo, pSysDate, pProgress, pPerformance, pComment);
                    if (r.ResultNo != 0) goto OnExit;
                }

                //r = Main.DB108303(db, pTypeCode, pTypeId, pStepId, pStepItemId, owner, 0, pStarted, DateTime.MinValue, 0);
                //if (r.ResultNo != 0) goto OnExit;
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            OnExit:
            if (r != null && conn != null)
                if (r.ResultNo == 0) conn.Commit();
                else conn.Rollback();

            return r;
        }
        #endregion
        #region [ FN108401 - Dynamic Step Txn List ]
        public Result FN108401(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                int pTypeCode = ISM.Lib.Static.ToInt(ri.ReceivedParam[0]);
                ulong pTypeId = ISM.Lib.Static.ToULong(ri.ReceivedParam[1]);
                int pStepItemId = ISM.Lib.Static.ToInt(ri.ReceivedParam[2]);

                r = Main.DB108401(db, pTypeCode, pTypeId, pStepItemId);
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
