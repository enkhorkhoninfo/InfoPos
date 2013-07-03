using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;

namespace IPos.Core
{
    public class BACProd
    {
        long _ProdCode;
        string _Name;
        string _Name2;
        string _CurCode;
        int _GL;
        int _Type;
        string _BalanceType;

        public long ProdCode { get { return _ProdCode; } set { _ProdCode = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string CurCode { get { return _CurCode; } set { _CurCode = value; } }
        public int GL { get { return _GL; } set { _GL = value; } }
        public int Type { get { return _Type; } set { _Type = value; } }
        public string BalanceType { get { return _BalanceType; } set { _BalanceType = value; } }


        private Hashtable  _BACProds = new Hashtable();
        public Hashtable BACProds
        {
            get
            {
                return _BACProds;
            }
            set
            {
                _BACProds = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init BACProd ...");

                _BACProds.Clear();
                #region [ Init General List ]
                res = IPos.DB.Main.DB202111(db, null);

                if (res.ResultNo != 0) return res;
                if (res.Data==null) 
                {
                    res.ResultNo = 9110015;
                    res.ResultDesc = "Байгууллагын дансны бүтээгдэхүүний уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    BACProd bac = new BACProd();
                    bac.ProdCode = Static.ToLong(DR["ProdCode"]);
                    bac.Name  = Static.ToStr(DR["Name"]);
                    bac.Name2  = Static.ToStr(DR["Name2"]);
                    bac.GL  = Static.ToInt(DR["GL"]);
                    bac.Type = Static.ToInt(DR["Type"]);
                    bac.BalanceType = Static.ToStr(DR["BalanceType"]);
                    _BACProds.Add(bac.ProdCode, bac);
                }
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110015;
                res.ResultDesc = "Байгууллагын дансны бүтээгдэхүүний уншихад алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
        }
        public BACProd Get(long key)
        {
            if (_BACProds.ContainsKey(key))
            {
                return (BACProd)_BACProds[key];
            }
            else
            {
                return null;
            }
        }
    }
}
