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
    public class AutoNum
    {
        #region [ Variables & properties ]
        long _ID;       // Төрөл  (3-Гэрээний дугаарлалт, 4-Захиалгын дугаарлалт, 5-Борлуулалтын дугаарлалт, 7-Төлбөрийн дугаарлалт, 8-Барьцааны дугаарлалт, 12-Харилцагчийн дугаарлалт)
        string _Code;   // Дэд дугаар (NULL байж болно)
        string _Name;   // Нэр
        string _Name2;  // Нэр 2
        string _Mask;   // Дугаарлалтын mask
        string _Keys;   // Түлхүүр утгууд (Зөвхөн энэ түлхүүр утгуудаар дугаарлалт үүснэ)
        string _Note;   // Тайлбар оруулах

        public long ID { get { return _ID; } set { _ID = value; } }
        public string Code { get { return _Code; } set { _Code = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string Mask { get { return _Mask; } set { _Mask = value; } }
        public string Keys { get { return _Keys; } set { _Keys = value; } }
        public string Note { get { return _Note; } set { _Note = value; } }

        private Hashtable _AutoNums = new Hashtable();
        public Hashtable AutoNums
        {
            get
            {
                return _AutoNums;
            }
            set
            {
                _AutoNums = value;
            }
        }
        #endregion
        #region [ Functions ]
        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init AutoNum ...");
                _AutoNums.Clear();
                #region [ Init General List ]
                res = IPos.DB.Main.DB202031(db);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110024;
                    res.ResultDesc = " Автомат дугаарлалтын мэдээлэл уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    AutoNum code = new AutoNum();
                    code.ID = Static.ToLong(DR["ID"]);
                    code.Code = Static.ToStr(DR["Code"]);
                    code.Name = Static.ToStr(DR["Name"]);
                    code.Name2 = Static.ToStr(DR["Name2"]);
                    code.Mask = Static.ToStr(DR["Mask"]);
                    code.Keys = Static.ToStr(DR["Key"]);
                    code.Note = Static.ToStr(DR["Note"]);
                    _AutoNums.Add(Static.ToStr(code.ID) + "-" + code.Code, code);
                }
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110024;
                res.ResultDesc = "Автомат дугаарлалтын мэдээлэл уншихад алдаа гарлаа";

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
        }
        public AutoNum Get(long ID, string Code)
        {
            if (_AutoNums.ContainsKey(Static.ToStr(ID) + "-" + Code))
            {
                return (AutoNum)_AutoNums[Static.ToStr(ID) + "-" + Code];
            }
            else
            {
                return null;
            }
        }
        public Result GetNextNumber(DbConnections db, long ID, string Code, AutoNumEnum anumenum)
        {
            Result ret = new Result();
            Hashtable hash = new Hashtable();
            string newnumber = "";
            try
            {
                if (_AutoNums.ContainsKey(Static.ToStr(ID) + "-" + Code))
                {
                    #region [ Хэрэв автомат дугаарлалтыг олох хувьсагч нь хоосон байвал алдаа өгдөг байна]
                    string keys = Get(ID, Code).Keys;
                    string mask = Get(ID, Code).Mask;
                    string ch = "";
                    string key = "";
                    for (int i = 0; i < keys.Length; i++)
                    {
                        ch = keys.Substring(i, 1);
                        switch (ch)
                        {
                            case "Y":
                                key += Static.ToStr(anumenum.Y) + "-";
                                hash.Add("Y", Static.ToStr(anumenum.Y));
                                break;
                            case "Q":
                                key += Static.ToStr(anumenum.Q) + "-";
                                hash.Add("Q", Static.ToStr(anumenum.Q));
                                break;
                            case "M":
                                key += Static.ToStr(anumenum.M) + "-";
                                hash.Add("M", Static.ToStr(anumenum.M));
                                break;
                            case "D":
                                key += Static.ToStr(anumenum.D) + "-";
                                hash.Add("D", Static.ToStr(anumenum.D));
                                break;
                            case "B":
                                key += Static.ToStr(anumenum.B) + "-";
                                hash.Add("B", Static.ToStr(anumenum.B));
                                break;
                            case "C":
                                key += Static.ToStr(anumenum.C) + "-";
                                hash.Add("C", Static.ToStr(anumenum.C));
                                break;
                            case "F":
                                key += Static.ToStr(anumenum.F) + "-";
                                hash.Add("F", Static.ToStr(anumenum.F));
                                break;
                            case "G":
                                key += Static.ToStr(anumenum.G) + "-";
                                hash.Add("G", Static.ToStr(anumenum.G));
                                break;
                            case "P":
                                key += Static.ToStr(anumenum.P) + "-";
                                hash.Add("P", Static.ToStr(anumenum.P));
                                break;
                            case "R":
                                key += Static.ToStr(anumenum.R) + "-";
                                hash.Add("R", Static.ToStr(anumenum.R));
                                break;
                            case "T":
                                key += Static.ToStr(anumenum.T) + "-";
                                hash.Add("T", Static.ToStr(anumenum.T));
                                break;
                            case "Z":
                                key += Static.ToStr(anumenum.Z) + "-";
                                hash.Add("Z", Static.ToStr(anumenum.Z));
                                break;
                            case "L":
                                key += Static.ToStr(anumenum.L) + "-";
                                hash.Add("L", Static.ToStr(anumenum.L));
                                break;
                            case "S":
                                if (i == 0)
                                    key = "-";
                                break;
                            default:
                                {
                                    ret.ResultNo = 1;
                                    ret.ResultDesc = ch + "Ийм кодтой автомат дугаарлалтын элемент байхгүй байна";
                                    return ret;
                                }
                        }
                    }
                    #endregion
                    #region [ Next дугаарыг олно ]
                    long next = SystemProp.gAutoNumValue.GetNext(db, ID, Code, key);
                    hash.Add("S", next);

                    newnumber = ISM.Lib.Static.ToMask(mask, hash);

                    ret.ResultNo = 0;
                    ret.ResultDesc = newnumber;
                    #endregion
                }
                else
                {
                    ret.ResultNo = 1;
                    ret.ResultDesc = Static.ToStr(ID) + "-" + Code + "Ийм дугаартай автомат дугаарлалт бүртгэгдээгүй байна";
                }
            }
            catch (Exception ex)
            {
                EServ.Shared.Static.WriteToLogFile("ex=" + ex.ToString());
                ret.ResultNo = 1;
                ret.ResultDesc = ex.Message;
            }
            return ret;
        }
        #endregion
    }
    public class AutoNumValue
    {
        #region [ Variables & properties ]
        long _ID;
        string _Code;
        string _Key;
        long _Value;

        public long ID { get { return _ID; } set { _ID = value; } }
        public string Code { get { return _Code; } set { _Code = value; } }        
        public string Key { get { return _Key; } set { _Key = value; } }
        public long Value { get { return _Value; } set { _Value = value; } }

        private Hashtable _AutoNumValues = new Hashtable();
        public Hashtable AutoNumValues
        {
            get
            {
                return _AutoNumValues;
            }
            set
            {
                _AutoNumValues = value;
            }
        }
        #endregion
        #region [ Functions ]
        public Result Init(DbConnections db)
        {
            Result res = new Result();
            try
            {
                EServ.Shared.Static.WriteToLogFile("Init AutoNumValues ...");
                _AutoNumValues.Clear();
                #region [ Init General List ]
                res = IPos.DB.Main.DB202311(db);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110024;
                    res.ResultDesc = " Автомат дугаарлалтын дугаарын мэдээлэл уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    AutoNumValue code = new AutoNumValue();
                    code.ID = Static.ToLong(DR["ID"]);
                    code.Code = Static.ToStr(DR["Code"]);
                    code.Key = Static.ToStr(DR["Key"]);
                    code.Value  = Static.ToLong(DR["Value"]);

                    _AutoNumValues.Add(Static.ToStr(code.ID) + "-" + code.Code + "-" + code.Key, code);
                }
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110024;
                res.ResultDesc = " Автомат дугаарлалтын дугаарын мэдээлэл уншихад алдаа гарлаа";

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
        }
        public AutoNumValue Get(long id, string code, string key)
        {
            if (_AutoNumValues.ContainsKey(Static.ToStr(id) + "-" + code + "-" + key))
            {
                return (AutoNumValue)_AutoNumValues[Static.ToStr(id) + "-" + code + "-" + key];
            }
            else
            {
                return null;
            }
        }
        public long GetNext(DbConnections db, long id, string code, string key)
        {
            long value;
            if (_AutoNumValues.ContainsKey(Static.ToStr(id) + "-" + code + "-" + key))
            {
                value = ((AutoNumValue)_AutoNumValues[Static.ToStr(id) + "-" + code + "-" + key]).Value + 1;
                ((AutoNumValue)_AutoNumValues[Static.ToStr(id) + "-" + code + "-" + key]).Value = value;

                Result res = IPos.DB.Main.DB202313(db, new object[] { id, code, key, value });
                return value;
            }
            else
            {
                AutoNumValue newcode = new AutoNumValue();
                newcode.ID = id;
                newcode.Code = code;
                newcode.Key = key;
                newcode.Value = 0;

                _AutoNumValues.Add(Static.ToStr(id) + "-" + code + "-" + key, newcode);
                Result res = IPos.DB.Main.DB202312(db, new object[] { id, code, key, 0 });
               return 0;
            }
        }
        #endregion
    }
    public class AutoNumEnum
    {
        string _Y;      // Year
        string _Q;      // Quarter
        string _M;      // Month
        string _D;      // Day
        string _B;      // Branch
        string _C;      // CurrencyCode, ClassCode
        string _F;      // ClassCode
        string _G;      // TypeCode
        string _P;      // MemberType
        string _R;      // Random number
        string _T;      // ContractType
        string _Z;      // Business session
        string _L;      // PledgeType
        string _S;      // Sequence 

        public string Y { get { return _Y; } set { _Y = value; } }
        public string Q { get { return _Q; } set { _Q = value; } }
        public string M { get { return _M; } set { _M = value; } }
        public string D { get { return _D; } set { _D = value; } }
        public string B { get { return _B; } set { _B = value; } }
        public string C { get { return _C; } set { _C = value; } }
        public string F { get { return _F; } set { _F = value; } }
        public string G { get { return _G; } set { _G = value; } }
        public string P { get { return _P; } set { _P = value; } }
        public string R { get { return _R; } set { _R = value; } }
        public string T { get { return _T; } set { _T = value; } }
        public string Z { get { return _Z; } set { _Z = value; } }
        public string L { get { return _L; } set { _L = value; } }
        public string S { get { return _S; } set { _S = value; } }
    }
}
