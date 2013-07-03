using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISM.CUser
{
    public class User
    {
        #region Properties

        private string mstrBranchCode;
        public string BranchCode
        {
            get { return mstrBranchCode; }
            set { mstrBranchCode = value; }
        }

        private int mintUserNo;
        public int UserNo
        {
            get { return mintUserNo; }
            set { mintUserNo = value; }
        }

        private string mstrUserPassword;
        public string UserPassword
        {
            get { return mstrUserPassword; }
            set { mstrUserPassword = value; }
        }

        private int mintUserLevel;
        public int UserLevel
        {
            get { return mintUserLevel; }
            set { mintUserLevel = value; }
        }

        private string mstrUserFName;
        public string UserFName
        {
            get { return mstrUserFName; }
            set { mstrUserFName = value; }
        }

        private string mstrUserFName2;
        public string UserFName2
        {
            get { return mstrUserFName2; }
            set { mstrUserFName2 = value; }
        }

        private string mstrUserLName;
        public string UserLName
        {
            get { return mstrUserLName; }
            set { mstrUserLName = value; }
        }

        private string mstrUserLName2;
        public string UserLName2
        {
            get { return mstrUserLName2; }
            set { mstrUserLName2 = value; }
        }

        private string mstrEMail;
        public string EMail
        {
            get { return mstrEMail; }
            set { mstrEMail = value; }
        }

        private string mstrMobileNo;
        public string MobileNo
        {
            get { return mstrMobileNo; }
            set { mstrMobileNo = value; }
        }

        private string mstrComputerName;
        public string ComputerName
        {
            get { return mstrComputerName; }
            set { mstrComputerName = value; }
        }

        private string mstrIPAddress;
        public string IPAddress
        {
            get { return mstrIPAddress; }
            set { mstrIPAddress = value; }
        }

        private string mstrNICAddress;
        public string NICAddress
        {
            get { return mstrNICAddress; }
            set { mstrNICAddress = value; }
        }

        private string mstrUserLanguage;
        public string UserLanguage
        {
            get { return mstrUserLanguage; }
            set { mstrUserLanguage = value; }
        }

        //шинэ
        private int mintLevel1;
        public int Level1
        {
            get { return mintLevel1; }
            set { mintLevel1 = value; }
        }

        private int mintLevel2;
        public int Level2
        {
            get { return mintLevel2; }
            set { mintLevel2 = value; }
        }

        private int mintLevel3;
        public int Level3
        {
            get { return mintLevel3; }
            set { mintLevel3 = value; }
        }

        private int mintLevel4;
        public int Level4
        {
            get { return mintLevel4; }
            set { mintLevel4 = value; }
        }

        private int _TxnGroupLevel;
        public int TxnGroupLevel
        {
            get { return _TxnGroupLevel; }
            set { _TxnGroupLevel = value; }
        }
        private string _AreaCode = "";
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }
        private string _PosNo = "";
        public string PosNo
        {
            get { return _PosNo; }
            set { _PosNo = value; }
        }

        #endregion
    }
}
