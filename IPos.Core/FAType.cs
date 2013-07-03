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
    public class FAType
    {
        long _FATypeID;
        string _Name;
        string _Name2;
        string _Currency;
        long _AccountNo;
        long _DefAccountNo;
        long _DefExpAccountNo;
        long _ProfitLossAccountNo;
        string _DepFormula;
        int _WearYear;
        int _Days;
        int _HalfMonthCalc;


        public long FATypeID { get { return _FATypeID; } set { _FATypeID = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string Currency { get { return _Currency; } set { _Currency = value; } }
        public long AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }
        public long DefAccountNo { get { return _DefAccountNo; } set { _DefAccountNo = value; } }
        public long DefExpAccountNo { get { return _DefExpAccountNo; } set { _DefExpAccountNo = value; } }
        public long ProfitLossAccountNo { get { return _ProfitLossAccountNo; } set { _ProfitLossAccountNo = value; } }
        public int WearYear { get { return _WearYear; } set { _WearYear = value; } }
        public string DepFormula { get { return _DepFormula; } set { _DepFormula = value; } }
        public int Days { get { return _Days; } set { _Days = value; } }
        public int HalfMonthCalc { get { return _HalfMonthCalc; } set { _HalfMonthCalc = value; } }

        private Hashtable _FATypes = new Hashtable();
        public Hashtable FATypes
        {
            get
            {
                return _FATypes;
            }
            set
            {
                _FATypes = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init FATypes ...");

                _FATypes.Clear();
                #region [ Init General List ]
                //select FATypeID, Name, Name2, Currency, AccountNo, DefAccountNo, DefExpAccountNo, ProfitLossAccountNo, WearYear  from FAType 
                res = IPos.DB.Main.DB202136(db, 0, 0, null);

                if (res.ResultNo != 0) return res;
                if (res.Data==null) 
                {
                    res.ResultNo = 9110015;
                    res.ResultDesc = "Үндсэн хөрөнгийн мэдээллийг уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    FAType fatype = new FAType();
                    fatype.FATypeID = Static.ToLong(DR["FATypeID"]);
                    fatype.Name = Static.ToStr(DR["Name"]);
                    fatype.Name2 = Static.ToStr(DR["Name2"]);
                    fatype.Currency = Static.ToStr(DR["Currency"]);
                    fatype.AccountNo = Static.ToLong(DR["AccountNo"]);
                    fatype.DefAccountNo = Static.ToLong(DR["DefAccountNo"]);
                    fatype.DefExpAccountNo = Static.ToLong(DR["DefExpAccountNo"]);
                    fatype.DepFormula = Static.ToStr(DR["DepFormula"]);
                    fatype.Days = Static.ToInt(DR["Days"]);
                    fatype.HalfMonthCalc = Static.ToInt(DR["HalfMonthCalc"]);
                    _FATypes.Add(fatype.FATypeID, fatype);
                }
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110015;
                res.ResultDesc = "Үндсэн хөрөнгийн төрөлийн мэдээлэл уншихад алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
        }
        public FAType Get(long key)
        {
            if (_FATypes.ContainsKey(key))
            {
                return (FAType)_FATypes[key];
            }
            else
            {
                return null;
            }
        }
    }
}
