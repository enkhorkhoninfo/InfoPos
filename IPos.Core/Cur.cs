using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Shared;
using EServ.Data;

namespace IPos.Core
{
    public class Cur
    {
        string _Currency;
        decimal  _Rate;
        decimal _OldRate;
        long _GLEquiv;
        long _GLExchProfit;
        long _GLExchLoss;
        long _GLRevProfit;
        long _GLRevLoss;
        int _CurrencyCode;


        public string Currency { get { return _Currency; } set { _Currency = value; } }
        public decimal  Rate { get { return _Rate; } set { _Rate = value; } }
        public decimal OldRate { get { return _OldRate; } set { _OldRate = value; } }
        public long GLEquiv { get { return _GLEquiv; } set { _GLEquiv = value; } }
        public long GLExchProfit { get { return _GLExchProfit; } set { _GLExchProfit = value; } }
        public long GLExchLoss { get { return _GLExchLoss; } set { _GLExchLoss = value; } }
        public long GLRevProfit { get { return _GLRevProfit; } set { _GLRevProfit = value; } }
        public long GLRevLoss { get { return _GLRevLoss; } set { _GLRevLoss = value; } }
        public int CurrencyCode { get { return _CurrencyCode; } set { _CurrencyCode = value; } }

        private Hashtable _Curs = new Hashtable();
        public Hashtable Curs
        {
            get
            {
                return _Curs;
            }
            set
            {
                _Curs = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init Cur ...");

                _Curs.Clear();
                #region [ Init General List ]
                res = IPos.DB.Main.DB202016(db, 0, 0, null);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110023;
                    res.ResultDesc = "Валютын мэдээлэл уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    Cur cur = new Cur();
                    cur.Currency = Static.ToStr(DR["Currency"]);
                    cur.CurrencyCode = Static.ToInt(DR["CurrencyCode"]);
                    cur.Rate = Static.ToDecimal(DR["Rate"]);
                    cur.OldRate = Static.ToDecimal(DR["OldRate"]);
                    cur.GLEquiv = Static.ToLong(DR["GLEquiv"]);
                    cur.GLExchProfit = Static.ToLong(DR["GLExchProfit"]);
                    cur.GLExchLoss = Static.ToLong(DR["GLExchLoss"]);
                    cur.GLRevProfit = Static.ToLong(DR["GLRevProfit"]);
                    cur.GLRevLoss = Static.ToLong(DR["GLRevLoss"]);

                    _Curs.Add(cur.Currency, cur);
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
        public Cur Get(string key)
        {
            if (_Curs.ContainsKey(key))
            {
                return (Cur)_Curs[key];
            }
            else
            {
                return null;
            }
        }
    }
}
