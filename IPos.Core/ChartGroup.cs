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
    public class ChartGroup
    {
        int _GroupNo;
        int _Type;
        string _Name;
        string _Name2;
        int _CloseType;

        public int GroupNo { get { return _GroupNo; } set { _GroupNo = value; } }
        public int Type { get { return _Type; } set { _Type = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public int CloseType { get { return _CloseType; } set { _CloseType = value; } }

        private Hashtable _ChartGroups = new Hashtable();
        public Hashtable ChartGroups
        {
            get
            {
                return _ChartGroups;
            }
            set
            {
                _ChartGroups = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init GL Chart Group ...");

                #region [ Init General List ]
                res = IPos.DB.Main.DB202121(db, 0, 0, null);
                _ChartGroups.Clear();
                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110023;
                    res.ResultDesc = "Валютын мэдээлэл уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    ChartGroup chartgroup = new ChartGroup();
                    chartgroup.GroupNo = Static.ToInt(DR["GroupNo"]);
                    chartgroup.Type = Static.ToInt(DR["Type"]);
                    chartgroup.Name = Static.ToStr(DR["Name"]);
                    chartgroup.Name2 = Static.ToStr(DR["Name2"]);
                    chartgroup.CloseType = Static.ToInt(DR["CloseType"]);

                    _ChartGroups.Add(chartgroup.GroupNo, chartgroup);
                }
                #endregion
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110015;
                res.ResultDesc = "Ерөнхий дэвтрийн бүлэгийн мэдээлэл уншихад алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
        }
        public ChartGroup Get(long key)
        {
            if (_ChartGroups.ContainsKey(key))
            {
                return (ChartGroup)_ChartGroups[key];
            }
            else
            {
                return null;
            }
        }
    }
}
