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
    public class DicData : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 106001:
                        r = FN106001(ci, ri, db, ref lg);
                        break;
                    case 106002:
                        r = FN106002(ci, ri, db, ref lg);
                        break;
                    case 106003:
                        r = FN106003(ci, ri, db, ref lg);
                        break;
                }
            }
            catch
            { }
            return r;
        }

        public Result FN106001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string dictid = Static.ToStr(ri.ReceivedParam[0]); // Table name prefix of Dynamic Data
                r = Main.DB106001(db,dictid);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        public Result FN106002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string dictid = Static.ToStr(ri.ReceivedParam[0]); // Table name prefix of Dynamic Data
                r = Main.DB106002(db, dictid);
                if (r.ResultNo != 0)
                {
                    r.ResultDesc = dictid;
                    return r;
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        public Result FN106003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                r = Main.DB106003(db);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }

    }
}
