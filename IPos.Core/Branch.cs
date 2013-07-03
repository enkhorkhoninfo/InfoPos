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
    public class Branch
    {
        int _BranchNo;
        string _Name;
        string _Name2;
        string _Director;

        public int BranchNo { get { return _BranchNo; } set { _BranchNo = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string Director { get { return _Director; } set { _Director = value; } }

        private Hashtable _Branchs = new Hashtable();
        public Hashtable Branchs
        {
            get
            {
                return _Branchs;
            }
            set
            {
                _Branchs = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();
            _Branchs.Clear();
            try
            {
                EServ.Shared.Static.WriteToLogFile("Init Branch ...");

                #region [ Init General List ]
                res = IPos.DB.Main.DB202001(db, 0, 0, null);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110024;
                    res.ResultDesc = " Түр дансны мэдээлэл уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    Branch code = new Branch();
                    code.BranchNo = Static.ToInt(DR["Branch"]);
                    code.Name = Static.ToStr(DR["Name"]);
                    code.Name2 = Static.ToStr(DR["Name2"]);
                    code.Director = Static.ToStr(DR["Director"]);

                    _Branchs.Add(code.BranchNo, code);
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
        public Branch Get(int branch)
        {
            if (_Branchs.ContainsKey(branch))
            {
                return (Branch)_Branchs[branch];
            }
            else
            {
                return null;
            }
        }
    }
}