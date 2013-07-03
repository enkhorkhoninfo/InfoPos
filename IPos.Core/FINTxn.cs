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
    public class FinTxn
    {
        int _TxnCode;
        string _Name;
        string _Name2;
        string _DynamicSQL;
        FinTxnEntry _TxnEntry;

        public int TxnCode { get { return _TxnCode; } set { _TxnCode = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string DynamicSQL { get { return _DynamicSQL; } set { _DynamicSQL = value; } }
        public FinTxnEntry TxnEntry { get { return _TxnEntry; } set { _TxnEntry = value; } }

        private Hashtable _FinTxns = new Hashtable();
        public Hashtable FinTxns
        {
            get
            {
                return _FinTxns;
            }
            set
            {
                _FinTxns = value;
            }
        }

        public Result Init(DbConnections db)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init AccountCode ...");
                _FinTxns.Clear();
                #region [ Init General List ]
                res = IPos.DB.Main.DB210009(db);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110024;
                    res.ResultDesc = " Түр дансны мэдээлэл уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    FinTxn code = new FinTxn();
                    code.TxnCode = Static.ToInt(DR["TxnCode"]);
                    code.Name = Static.ToStr(DR["Name"]);
                    code.Name2 = Static.ToStr(DR["Name2"]);
                    code.DynamicSQL = Static.ToStr(DR["DynamicSQL"]);
                    code.TxnEntry = new FinTxnEntry();
                    code.TxnEntry.Init(db, code.TxnCode);
                    _FinTxns.Add(code.TxnCode, code);
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
        public FinTxn Get(int pTxnCode)
        {
            if (_FinTxns.ContainsKey(pTxnCode))
            {
                return (FinTxn)_FinTxns[pTxnCode];
            }
            else
            {
                return null;
            }
        }
    }
    public class FinTxnEntry
    {
        int _TxnCode;
        int _EntryCode;
        int _EntryTxnCode;
        string _DRAcntMod;
        string _DRAcntNo;
        string _DRCurCode;
        string _DRRate;
        string _DRAmount;
        string _CRAcntMod;
        string _CRAcntNo;
        string _CRCurCode;
        string _CRRate;
        string _CRAmount;
        string _Description;
        string _Condition;

        public int TxnCode { get { return _TxnCode; } set { _TxnCode = value; } }
        public int EntryCode { get { return _EntryCode; } set { _EntryCode = value; } }
        public int EntryTxnCode { get { return _EntryTxnCode; } set { _EntryTxnCode = value; } }
        public string DRAcntMod { get { return _DRAcntMod; } set { _DRAcntMod = value; } }
        public string DRAcntNo { get { return _DRAcntNo; } set { _DRAcntNo = value; } }
        public string DRCurCode { get { return _DRCurCode; } set { _DRCurCode = value; } }
        public string DRRate { get { return _DRRate; } set { _DRRate = value; } }
        public string DRAmount { get { return _DRAmount; } set { _DRAmount = value; } }
        public string CRAcntMod { get { return _CRAcntMod; } set { _CRAcntMod = value; } }
        public string CRAcntNo { get { return _CRAcntNo; } set { _CRAcntNo = value; } }
        public string CRCurCode { get { return _CRCurCode; } set { _CRCurCode = value; } }
        public string CRRate { get { return _CRRate; } set { _CRRate = value; } }
        public string CRAmount { get { return _CRAmount; } set { _CRAmount = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string Condition { get { return _Condition; } set { _Condition = value; } }

        private Hashtable _FinTxnEntrys = new Hashtable();
        public Hashtable FinTxnEntrys
        {
            get
            {
                return _FinTxnEntrys;
            }
            set
            {
                _FinTxnEntrys = value;
            }
        }

        public Result Init(DbConnections db, int pTxnCode)
        {
            Result res = new Result();

            try
            {
                EServ.Shared.Static.WriteToLogFile("Init AccountCode ...");
                /*asd
                #region [ Init General List ]
                res = HPro.DB.Main.DB210008(db, pTxnCode);

                if (res.ResultNo != 0) return res;
                if (res.Data == null)
                {
                    res.ResultNo = 9110024;
                    res.ResultDesc = " Түр дансны мэдээлэл уншихад алдаа гарлаа";
                    return res;
                }
                foreach (DataRow DR in res.Data.Tables[0].Rows)
                {
                    FinTxnEntry txnentry = new FinTxnEntry();
                    txnentry.TxnCode = Static.ToInt(DR["TranCode"]);
                    txnentry.EntryCode = Static.ToInt(DR["EntryCode"]);
                    txnentry.EntryTxnCode = Static.ToInt(DR["EntryTxnCode"]);
                    txnentry.DRAcntMod = Static.ToStr(DR["DRAcntMod"]);
                    txnentry.DRAcntNo = Static.ToStr(DR["DRAcntNo"]);
                    txnentry.DRAmount = Static.ToStr(DR["DRAmount"]);
                    txnentry.DRCurCode = Static.ToStr(DR["DRCurrCode"]);
                    txnentry.DRRate = Static.ToStr(DR["DRRate"]);
                    txnentry.CRAcntMod = Static.ToStr(DR["CRAcntMod"]);
                    txnentry.CRAcntNo = Static.ToStr(DR["CRAcntNo"]);
                    txnentry.CRAmount = Static.ToStr(DR["CRAmount"]);
                    txnentry.CRCurCode = Static.ToStr(DR["CRCurrCode"]);
                    txnentry.CRRate = Static.ToStr(DR["CRRate"]);
                    txnentry.Description = Static.ToStr(DR["Description"]);
                    txnentry.Condition = Static.ToStr(DR["Condition"]);
                    _FinTxnEntrys.Add(txnentry.EntryCode, txnentry);
                }
                #endregion*/
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
        public FinTxnEntry Get(int EntryCode)
        {
            if (_FinTxnEntrys.ContainsKey(EntryCode))
            {
                return (FinTxnEntry)_FinTxnEntrys[EntryCode];
            }
            else
            {
                return null;
            }
        }
    }
}
