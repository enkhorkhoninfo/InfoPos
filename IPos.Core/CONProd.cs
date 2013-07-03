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
    public class CONProd
    {
        long _ProdCode;
        string _Name;
        string _Name2;
        string _CurCode;
        int _GL;
        int _TypeCode;

        public long ProdCode { get { return _ProdCode; } set { _ProdCode = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string CurCode { get { return _CurCode; } set { _CurCode = value; } }
        public int GL { get { return _GL; } set { _GL = value; } }
        public int TypeCode { get { return _TypeCode; } set { _TypeCode = value; } }

        private Hashtable _CONProds = new Hashtable();
        public Hashtable CONProds
        {
            get
            {
                return _CONProds;
            }
            set
            {
                _CONProds = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init CON Prod ...");

                _CONProds.Clear();
                #region [ Init General List ]
                res = IPos.DB.Main.DB202116(db, null);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110015;
                    res.ResultDesc = "Балансын гадуурх дансны бүтээгдэхүүний уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    CONProd con = new CONProd();
                    con.ProdCode = Static.ToLong(DR["ProdCode"]);
                    con.Name = Static.ToStr(DR["Name"]);
                    con.Name2 = Static.ToStr(DR["Name2"]);
                    con.GL = Static.ToInt(DR["GL"]);
                    con.TypeCode = Static.ToInt(DR["TypeCode"]);

                    _CONProds.Add(con.ProdCode, con);
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
        public CONProd Get(long key)
        {
            if (_CONProds.ContainsKey(key))
            {
                return (CONProd)_CONProds[key];
            }
            else
            {
                return null;
            }
        }
    }
}
