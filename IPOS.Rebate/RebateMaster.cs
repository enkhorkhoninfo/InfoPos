using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;

namespace IPOS.Rebate
{
    public class RebateMaster
    {
        long _MasterID;             //  ID дугаар
        int _MasterType;            //  0 - Хөнгөлөлт, 1 - Урамшуулал, 2 - Оноо
        string _Name;               //  Хөнгөлөлтийн нэр
        int _ProdType;              //  0 - Бараа, 1 - Үйлчилгээ, 9 - Бусад бүх
        string _ProdNo;             //  Бараа үйлчилгээний ID дугаарыг хадгална.
        string _ProdName;             //  Бараа үйлчилгээний нэр хадгална.
        int _CalcType;              //  0 - Дүн, 1 - Хувь
        decimal _CalcAmount;        //  Хувь эсвэл дүнг оруулна 

        public long MasterID { get { return _MasterID; } set { _MasterID = value; } }
        public int MasterType { get { return _MasterType; } set { _MasterType = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public int ProdType { get { return _ProdType; } set { _ProdType = value; } }
        public string ProdNo { get { return _ProdNo; } set { _ProdNo = value; } }
        public string ProdName { get { return _ProdName; } set { _ProdName = value; } }
        public int CalcType { get { return _CalcType; } set { _CalcType = value; } }
        public decimal CalcAmount { get { return _CalcAmount; } set { _CalcAmount = value; } }


        private Hashtable _RebateMasters = new Hashtable();
        public Hashtable RebateMasters
        {
            get
            {
                return _RebateMasters;
            }
            set
            {
                _RebateMasters = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init RebateMaster ...");

                #region [ Init General List ]
                //res = HPro.DB.Main.DB202111(db,null);
//                string sql = @"
//                                select M.MasterID, M.MasterType,M.Name, D.ProdType, D.ProdNo, D.CalcType, D.CalcAmount 
//                                from RebateMaster M
//                                left join RebateDetail D on M.MasterID = D.MasterID
//                                left join InvMain D on M.MasterID = D.MasterID";
                string sql = @"select M.MasterID, M.MasterType,M.Name, D.ProdType, D.ProdNo,C.Name as ProdName,D.CalcType, D.CalcAmount 
                                from RebateMaster M
                                left join RebateDetail D on M.MasterID = D.MasterID
                                left join invmain c on c.invid=d.prodno
                                where prodtype=0
                                union
                                select M.MasterID, M.MasterType,M.Name, D.ProdType, D.ProdNo, C.Name as ProdName ,D.CalcType, D.CalcAmount 
                                from RebateMaster M
                                left join RebateDetail D on M.MasterID = D.MasterID
                                left join servmain c on c.servid=d.prodno
                                where prodtype=1";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn50000x", null);

                if (res.ResultNo != 0) return res;
                if (res.Data==null) 
                {
                    res.ResultNo = 9110015;
                    res.ResultDesc = "RebateMaster уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    RebateMaster rebate = new RebateMaster();
                    rebate.MasterID = Static.ToLong(DR["MasterID"]);
                    rebate.MasterType = Static.ToInt(DR["MasterType"]);
                    rebate.Name = Static.ToStr(DR["Name"]);
                    rebate.ProdType = Static.ToInt(DR["ProdType"]);
                    rebate.ProdNo = Static.ToStr(DR["ProdNo"]);
                    rebate.ProdName = Static.ToStr(DR["ProdName"]);
                    rebate.CalcType = Static.ToInt(DR["CalcType"]);
                    rebate.CalcAmount = Static.ToDecimal(DR["CalcAmount"]);
                    _RebateMasters.Add(rebate.MasterID + rebate.ProdType + rebate.ProdNo, rebate);
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
        public RebateMaster Get(long key)
        {
            if (_RebateMasters.ContainsKey(key))
            {
                return (RebateMaster)_RebateMasters[key];
            }
            else
            {
                return null;
            }
        }
    }
    public class RebateFormula
    {
        long _FormulaID;             //     Дүрэм дугаар
        int _Status;                 //     Статус
        DateTime _BeginDate;        //      Эхлэх огноо
        DateTime _EndDate;          //      Дуусах огноо
        string _SQL;                //      SQL

        public long FormulaID { get { return _FormulaID; } set { _FormulaID = value; } }
        public int Status { get { return _Status; } set { _Status = value; } }
        public string SQL { get { return _SQL; } set { _SQL = value; } }
        public DateTime BeginDate { get { return _BeginDate; } set { _BeginDate = value; } }
        public DateTime EndDate { get { return _EndDate; } set { _EndDate = value; } }

        private Hashtable _RebateFormulas = new Hashtable();
        public Hashtable RebateFormulas
        {
            get
            {
                return _RebateFormulas;
            }
            set
            {
                _RebateFormulas = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init RebateFormulas ...");

                #region [ Init General List ]
                //res = HPro.DB.Main.DB202111(db,null);
                string sql = "select FormulaID, Status, BeginDate, EndDate, SQLFunction  from RebateFormula";
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn50000x", null);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110015;
                    res.ResultDesc = "RebateFormulas уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    RebateFormula formula = new RebateFormula();
                    formula.FormulaID = Static.ToLong(DR["FormulaID"]);
                    formula.Status = Static.ToInt(DR["Status"]);
                    formula.BeginDate = Static.ToDate(DR["BeginDate"]);
                    formula.EndDate = Static.ToDate(DR["EndDate"]);
                    formula.SQL = Static.ToStr(DR["SQLFUNCTION"]);
                    _RebateFormulas.Add(formula.FormulaID, formula);
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
        public RebateFormula Get(long key)
        {
            if (_RebateFormulas.ContainsKey(key))
            {
                return (RebateFormula)_RebateFormulas[key];
            }
            else
            {
                return null;
            }
        }
    }
    public class RebateID
    {
        long _rebateid;             //     
        long _loyalid;             //     
        long _pointid;             //     

        public long rebateid { get { return _rebateid; } set { _rebateid = value; } }
        public long loyalid { get { return _loyalid; } set { _loyalid = value; } }
        public long pointid { get { return _pointid; } set { _pointid = value; } }

    }
}
