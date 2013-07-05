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
    public class AccountCode
    {
        #region [ Variables & properties ]
        string _Code;
        string _Currency;
        int _BranchNo;
        long _AccountNo;

        public string Code { get { return _Code; } set { _Code = value; } }
        public string Currency { get { return _Currency; } set { _Currency = value; } }
        public int BranchNo { get { return _BranchNo; } set { _BranchNo = value; } }
        public long AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }

        private Hashtable _AccountCodes = new Hashtable();
        public Hashtable AccountCodes
        {
            get
            {
                return _AccountCodes;
            }
            set
            {
                _AccountCodes = value;
            }
        }
        #endregion
        #region [ Functions ]
        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init AccountCode ...");
                _AccountCodes.Clear();
                #region [ Init General List ]
                res = IPos.DB.Main.DB202026(db, 0, 0, null);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110024;
                    res.ResultDesc = " Түр дансны мэдээлэл уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    AccountCode code = new AccountCode();
                    code.Code = Static.ToStr(DR["Code"]);
                    code.BranchNo = Static.ToInt(DR["BRANCH"]);
                    code.Currency = Static.ToStr(DR["Currency"]);
                    code.AccountNo = Static.ToLong(DR["AccountNo"]);

                    _AccountCodes.Add(code.Code+Static.ToStr(code.BranchNo) +code.Currency , code);
                }
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110024;
                res.ResultDesc = " Түр дансны мэдээлэл уншихад алдаа гарлаа";

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
        }
        public AccountCode Get(string code, string curcode, int branchno)
        {
            if (_AccountCodes.ContainsKey(code + Static.ToStr(branchno) + curcode))
            {
                return (AccountCode)_AccountCodes[code + Static.ToStr(branchno) + curcode];
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
