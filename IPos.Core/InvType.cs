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
    public class InvType
    {

        long _InvTypeID;
        string _Name;
        string _Name2;
        string _Currency;
        long _AccountNo;

        public long InvTypeID { get { return _InvTypeID; } set { _InvTypeID = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string Currency { get { return _Currency; } set { _Currency = value; } }
        public long AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }

        private Hashtable _InvTypes = new Hashtable();
        public Hashtable InvTypes
        {
            get
            {
                return _InvTypes;
            }
            set
            {
                _InvTypes = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init Inventory Types ...");

                _InvTypes.Clear();
                #region [ Init General List ]
                //select InvTypeID, Name, Name2, Currency, AccountNo from InvType 
                res = IPos.DB.Main.DB202101(db, 0, 0, null);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110015;
                    res.ResultDesc = "Үндсэн хөрөнгийн мэдээллийг уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    InvType invtype = new InvType();
                    invtype.InvTypeID = Static.ToLong(DR["InvTypeID"]);
                    invtype.Name = Static.ToStr(DR["Name"]);
                    invtype.Name2 = Static.ToStr(DR["Name2"]);
                    invtype.Currency = Static.ToStr(DR["Currency"]);
                    invtype.AccountNo = Static.ToLong(DR["AccountNo"]);
                    _InvTypes.Add(invtype.InvTypeID, invtype);
                }
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110015;
                res.ResultDesc = "Бараа материалын төрөлийн мэдээлэл уншихад алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
        }
        public InvType Get(long key)
        {
            if (_InvTypes.ContainsKey(key))
            {
                return (InvType)_InvTypes[key];
            }
            else
            {
                return null;
            }
        }
    }
}
