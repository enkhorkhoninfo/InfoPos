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
        long _ID;
        string _Name;
        string _Name2;
        string _Mask;
        string _Keys;

        public long ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string Mask { get { return _Mask; } set { _Mask = value; } }
        public string Keys { get { return _Keys; } set { _Keys = value; } }

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
                    code.Name = Static.ToStr(DR["Name"]);
                    code.Name2 = Static.ToStr(DR["Name2"]);
                    code.Mask = Static.ToStr(DR["Mask"]);
                    code.Keys = Static.ToStr(DR["Key"]);

                    _AutoNums.Add(code.ID , code);
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
        public AutoNum Get(long code)
        {
            if (_AutoNums.ContainsKey(code))
            {
                return (AutoNum)_AutoNums[code];
            }
            else
            {
                return null;
            }
        }
        public Result GetNextNumber(DbConnections db, long code, AutoNumEnum anumenum)
        {
            Result ret = new Result();
            Hashtable hash = new Hashtable();
            string newnumber = "";
            try
            {
                if (_AutoNums.ContainsKey(code))
                {
                    #region [ Хэрэв автомат дугаарлалтыг олох хувьсагч нь хоосон байвал алдаа өгдөг байна]
                    string keys = Get(code).Keys;
                    string mask = Get(code).Mask;
                    string ch="";
                    string key = "";
                    for (int i = 0; i < keys.Length; i++)
                    {
                        ch = keys.Substring(i, 1);
                        switch (ch)
                        { 
                            case "B":
                                key += Static.ToStr(anumenum.B)+"-";
                                hash.Add("B", Static.ToStr(anumenum.B));
                                break;
                            case "C":
                                key += Static.ToStr(anumenum.C) + "-";
                                hash.Add("C", Static.ToStr(anumenum.C));
                                break;
                            case "G":
                                key += Static.ToStr(anumenum.G) + "-";
                                hash.Add("G", Static.ToStr(anumenum.G));
                                break;
                            case "P":
                                key += Static.ToStr(anumenum.P) + "-";
                                hash.Add("P", Static.ToStr(anumenum.P));
                                break;
                            case "Y":
                                key += Static.ToStr(anumenum.Y) + "-";
                                hash.Add("Y", Static.ToStr(anumenum.Y));
                                break;
                            case "M":
                                key += Static.ToStr(anumenum.M) + "-";
                                hash.Add("M", Static.ToStr(anumenum.M));
                                break;
                            case "R":
                                key += Static.ToStr(anumenum.R) + "-";
                                hash.Add("R", Static.ToStr(anumenum.R));
                                break;
                            case "S":
                                if(i == 0)
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
                    long next = SystemProp.gAutoNumValue.GetNext(db, code, key);
                    hash.Add("S", next);

                    newnumber = ISM.Lib.Static.ToMask(mask, hash);

                    ret.ResultNo = 0;
                    ret.ResultDesc = newnumber;
                    #endregion
                }
                else
                {
                    ret.ResultNo = 1;
                    ret.ResultDesc = code.ToString()+"Ийм дугаартай автомат дугаарлалт бүртгэгдээгүй байна";
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
    }
    public class AutoNumValue
    {
        long _ID;
        string _Key;
        long _Value;

        public long ID { get { return _ID; } set { _ID = value; } }
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
                    code.Key = Static.ToStr(DR["Key"]);
                    code.Value  = Static.ToLong(DR["Value"]);

                    _AutoNumValues.Add(Static.ToStr(code.ID) + ":" + code.Key, code);
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
        public AutoNumValue Get(long code, string key)
        {
            if (_AutoNumValues.ContainsKey(Static.ToStr(code) + ":" + key))
            {
                return (AutoNumValue)_AutoNumValues[Static.ToStr(code) + ":" + key];
            }
            else
            {
                return null;
            }
        }
        public long GetNext(DbConnections db, long code, string key)
        {
            long value;
            if (_AutoNumValues.ContainsKey(Static.ToStr(code)+":"+key))
            {
                value = ((AutoNumValue)_AutoNumValues[Static.ToStr(code) + ":" + key]).Value+1;
                ((AutoNumValue)_AutoNumValues[Static.ToStr(code) + ":" + key]).Value = value;

                Result res = IPos.DB.Main.DB202313(db, new object[] { code, key, value });
                return value;
            }
            else
            {
                AutoNumValue newcode = new AutoNumValue();
                newcode.ID = code;
                newcode.Key = key;
                newcode.Value = 0;

                _AutoNumValues.Add(Static.ToStr(code) + ":" + key, newcode);
                Result res = IPos.DB.Main.DB202312(db, new object[] { code, key, 0 });
               return 0;
            }
        }
    }
    public class AutoNumEnum
    {
        string _B;
        string _C;
        string _G;
        string _P;
        string _Y;
        string _M;
        string _R;

        public string B { get { return _B; } set { _B = value; } }
        public string C { get { return _C; } set { _C = value; } }
        public string P { get { return _G; } set { _G = value; } }
        public string G { get { return _P; } set { _P = value; } }
        public string Y { get { return _Y; } set { _Y = value; } }
        public string M { get { return _M; } set { _M = value; } }
        public string R { get { return _R; } set { _R = value; } }
    }
}
