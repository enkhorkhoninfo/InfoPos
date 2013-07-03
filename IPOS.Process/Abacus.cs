using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using IPos.Core;
using IPos.DB;

namespace IPOS.Process
{
    public class abacusCustomer
    {
        public int CustomerID;
        public int SrbsMembershipId;
        public string Cust;
        public string CustomerName;
        public string MailingAddress;
        public string MailingAddress2;
        public string MailingCity;
        public string MailingState;
        public string MailingCountry;
        public string MailingZip;
        public string ShipToAddress;
        public string ShipToAddress2;
        public string ShipToCity;
        public string ShipToState;
        public string ShipToCountry;
        public string ShipToZip;
        public string Phone;
        public string Fax;
        public string OtherPhone;
        public string Contact;
        public string DateEntered;
        public string EMail;
        public string Internet;
        public string DefaultGLSales;
        public int TermsID;
        public bool Delete_;
        public bool PO;
        public decimal YTDsales;
        public string LastSale;
        public string LastPayment;
        public decimal LastPayAmt;
        public decimal LastSalesAmt;
        public decimal CreditLimit;
        public decimal CreditHigh;
        public decimal CurBal;
        public bool Active;
        public string Status;
        public int SalesmanID;
        public string TaxExempt;
        public string CC;
        public string SalesCommission;
        public int InternationalID;
        public int CountryID;
        public int StateID;
        public int CountyID;
        public int CityID;
        public int OtherID;
        public int FinanceChargeID;
        public string upsize_ts;
        public string AcquisitionID;
        public string CurrencyID;
        public int OnlineID;
        public int Latitude;
        public int Longitude;

        abacusCustomer()
        {
            CustomerID = 0;
             SrbsMembershipId= 0;
              Cust= "";
              CustomerName= "";
             MailingAddress="";
              MailingAddress2= "";
               MailingCity="";
               MailingState= "";
               MailingCountry= "";
             MailingZip="";
               ShipToAddress="";
              ShipToAddress2="";
              ShipToCity= "";
            ShipToState= "";
            ShipToCountry= "";
              ShipToZip= "";
               Phone= "";
             Fax="";
            OtherPhone= "";
            Contact="";
            DateEntered= "";
            EMail= "";
            Internet= "";
            DefaultGLSales= "4";
            TermsID= 0;
            Delete_= false;
            PO= false;
            YTDsales= 0;
            LastSale= "";
            LastPayment= "";
            LastPayAmt= 0;
            LastSalesAmt= 0;
            CreditLimit= 0;
            CreditHigh= 0;
            CurBal= 0;
            Active=true;
            Status= "2";
            SalesmanID= 0;
            TaxExempt= "";
            CC= "";
            SalesCommission= "";
            InternationalID=0;
            CountryID= 0;
            StateID= 0;
            CountyID= 0;
            CityID= 0;
            OtherID= 0;
            FinanceChargeID= 0;
            upsize_ts= "";
            AcquisitionID= "";
            CurrencyID="";
            OnlineID= 0;
            Latitude=0;
            Longitude= 0;
        }
        void SetCustomerData(DataRow row)
        { 
            this.CustomerID = 0; //????
            this.SrbsMembershipId = Static.ToInt(row["CustomerNo"]);
            this.Cust= Static.ToStr(row["CustomerNo"]);
            if (Static.ToInt(row["Type"])==1)
                this.CustomerName = Static.ToStr(row["FirstName"]) + " " + Static.ToStr(row["LastName"]) + " " + Static.ToStr(row["MiddleName"]);
            else
                this.CustomerName = Static.ToStr(row[" CorporateName"]) + " " + Static.ToStr(row["CorporateName2"]);
            this.MailingAddress = Static.ToStr(row["CustAddr"]);  //CustomerAddr.CityCode+CustomerAddr.DistCode+CustomerAddr.SubDistCode+CustomerAddr.Apartment+CustomerAddr.Note where CustomerAddr.AddrCurrent=1
            this.MailingAddress2 = Static.ToStr(row["RegisterNo"]);
            this.MailingCity = Static.ToStr(row["CityCode"]);
            this.MailingState = Static.ToStr(row["CityCode"]);
            this.MailingCountry = Static.ToStr(row["CountryCode"]);
            this.MailingZip= "0";
            this.ShipToAddress = Static.ToStr(row["CustAddr"]);
            this.ShipToAddress2 = Static.ToStr(row["RegisterNo"]);
            this.ShipToCity = Static.ToStr(row["CityCode"]);
            this.ShipToState = Static.ToStr(row["CityCode"]);
            this.ShipToCountry = Static.ToStr(row["CountryCode"]);
            this.ShipToZip= "0";
            this.Phone = Static.ToStr(row[".Telephone"]);
            this.Fax = Static.ToStr(row["Fax"]);
            this.OtherPhone = Static.ToStr(row["Mobile"]);
            this.Contact = Static.ToStr(row["DirFirstName"])+" " +Static.ToStr(row["DirLastName"]);
            this.DateEntered= Static.ToStr(row["CreateDate"]);
            this.EMail= Static.ToStr(row["Email"]);
            this.Internet= Static.ToStr(row["WebSite"]);
        }
        string GetSQL()
        {
            string sql = "";
            sql = " insert to dbo_tbltblCustomers ( CustomerID, SrbsMembershipId,  Cust,  CustomerName,  MailingAddress,   MailingAddress2,   MailingCity,   MailingState,   MailingCountry,   MailingZip,   ShipToAddress,   ShipToAddress2,   ShipToCity,   ShipToState,   ShipToCountry,   ShipToZip,   Phone,   Fax,   OtherPhone,   Contact,   DateEntered,   EMail,   Internet,   DefaultGLSales,   TermsID,   Delete_,   PO,   YTDsales,   LastSale,   LastPayment,   LastPayAmt,   LastSalesAmt,   CreditLimit,   CreditHigh,   CurBal,   Active,   Status,   SalesmanID,   TaxExempt,   CC,   SalesCommission,   InternationalID,   CountryID,   StateID,   CountyID,   CityID,   OtherID,   FinanceChargeID,   upsize_ts,   AcquisitionID,   CurrencyID,   OnlineID,   Latitude,   Longitude) ";
            sql += " values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16,:17,:18,:19,:20,:21,:22,:23,:24,:25,:26,:27,:28,:29,:30,:31,:32,:33,:34,:35,:36,:37,:38,:39,:40,:41,:42,:43,:44,:45,:46,:47,:48,:49,:50,:51,:52,:53,:54)";

            //sql +=""+	CustomerID+""+",";
            //sql +=""+	SrbsMembershipId+""+",";
            //sql +=""+	Cust+""+",";
            //sql +=""+	CustomerName+""+",";
            //sql +=""+	MailingAddress+""+",";
            //sql +=""+	MailingAddress2+""+",";
            //sql +=""+	MailingCity+""+",";
            //sql +=""+	MailingState+""+",";
            //sql +=""+	MailingCountry+""+",";
            //sql +=""+	MailingZip+""+",";
            //sql +=""+	ShipToAddress+""+",";
            //sql +=""+	ShipToAddress2+""+",";
            //sql +=""+	ShipToCity+""+",";
            //sql +=""+	ShipToState+""+",";
            //sql +=""+	ShipToCountry+""+",";
            //sql +=""+	ShipToZip+""+",";
            //sql +=""+	Phone+""+",";
            //sql +=""+	Fax+""+",";
            //sql +=""+	OtherPhone+""+",";
            //sql +=""+	Contact+""+",";
            //sql +=""+	DateEntered+""+",";
            //sql +=""+	EMail+""+",";
            //sql +=""+	Internet+""+",";
            //sql +=""+	DefaultGLSales+""+",";
            //sql +=""+	TermsID+""+",";
            //sql +=""+	Delete_+""+",";
            //sql +=""+	PO+""+",";
            //sql +=""+	YTDsales+""+",";
            //sql +=""+	LastSale+""+",";
            //sql +=""+	LastPayment+""+",";
            //sql +=""+	LastPayAmt+""+",";
            //sql +=""+	LastSalesAmt+""+",";
            //sql +=""+	CreditLimit+""+",";
            //sql +=""+	CreditHigh+""+",";
            //sql +=""+	CurBal+""+",";
            //sql +=""+	Active+""+",";
            //sql +=""+	Status+""+",";
            //sql +=""+	SalesmanID+""+",";
            //sql +=""+	TaxExempt+""+",";
            //sql +=""+	CC+""+",";
            //sql +=""+	SalesCommission+""+",";
            //sql +=""+	InternationalID+""+",";
            //sql +=""+	CountryID+""+",";
            //sql +=""+	StateID+""+",";
            //sql +=""+	CountyID+""+",";
            //sql +=""+	CityID+""+",";
            //sql +=""+	OtherID+""+",";
            //sql +=""+	FinanceChargeID+""+",";
            //sql +=""+	upsize_ts+""+",";
            //sql +=""+	AcquisitionID+""+",";
            //sql +=""+	CurrencyID+""+",";
            //sql +=""+	OnlineID+""+",";
            //sql +=""+	Latitude+""+",";
            //sql +=""+	Longitude +")";

            return sql;
        }
        object[] GetObj()
        {
            object[] obj = new object[54];

            obj[0] = CustomerID;
            obj[1] = SrbsMembershipId;
            obj[2] =  Cust;
            obj[3] =  CustomerName;
            obj[4] =  MailingAddress;
            obj[5] =   MailingAddress2;
            obj[6] =   MailingCity;
            obj[7] =   MailingState;
            obj[8] =   MailingCountry;
            obj[9] =   MailingZip;
            obj[10] =   ShipToAddress;
            obj[11] =   ShipToAddress2;
            obj[12] =   ShipToCity;
            obj[13] =   ShipToState;
            obj[14] =   ShipToCountry;
            obj[15] =   ShipToZip;
            obj[16] =   Phone;
            obj[17] =   Fax;
            obj[18] =   OtherPhone;
            obj[19] =   Contact;
            obj[20] =   DateEntered;
            obj[21] =   EMail;
            obj[22] =   Internet;
            obj[23] =   DefaultGLSales;
            obj[24] =   TermsID;
            obj[25] =   Delete_;
            obj[26] =   PO;
            obj[27] =   YTDsales;
            obj[28] =   LastSale;
            obj[29] =   LastPayment;
            obj[30] =   LastPayAmt;
            obj[31] =   LastSalesAmt;
            obj[32] =   CreditLimit;
            obj[33] =   CreditHigh;
            obj[34] =   CurBal;
            obj[35] =   Active;
            obj[36] =   Status;
            obj[37] =   SalesmanID;
            obj[38] =   TaxExempt;
            obj[39] =   CC;
            obj[40] =   SalesCommission;
            obj[41] =   InternationalID;
            obj[42] =   CountryID;
            obj[43] =   StateID;
            obj[44] =   CountyID;
            obj[45] =   CityID;
            obj[46] =   OtherID;
            obj[47] =   FinanceChargeID;
            obj[48] =   upsize_ts;
            obj[49] =   AcquisitionID;
            obj[50] =   CurrencyID;
            obj[51] =   OnlineID;
            obj[52] =   Latitude;
            obj[53] =   Longitude;

            return obj;
        }
    }
    public class abacusInvoice
    {
        private	int	ARInvoiceID;
        private	int	        OrderID;
        private	int	        SrbsPaymentId;
        private	string	        Invoice;
        private	int	        Batch;
        private	string	        Ref;
        private	DateTime	        Date;
        private	DateTime	        ARInvoiceDate;
        private	int	        DebitGLID;
        private	decimal	        Amount ;
        private	decimal	        ARBalance ;
        private	int	        CustomerID;
        private	string	        Author;
        private	string	        PO;
        private	bool	        Processed;
        private	decimal	        ToApply ;
        private	decimal	        AdjustmentAmount ;
        private	DateTime	        GLUpdateDate;
        private	string	        Job;
        private	int	        JobID;
        private	int	        SalesmanID;
        private	int	        DocType;
        private	DateTime	        DueDate;
        private	DateTime	        FCDate;
        private	DateTime	        DiscountDate;
        private	DateTime	        DateClosed;
        private	int	        upsize_ts;
        private	int	        CurrencyID;
        private	decimal	        CurrentRate;
        private	decimal	        CurrentMethod;
        private	string	        Process;
        private	int	        ProcessID;
        private	int	        ActivityID;


        abacusInvoice()
        {
            ARInvoiceID = 0;
            OrderID = 0;
            SrbsPaymentId= 0;
            Invoice= "";
            Batch= 0;
            Ref= "";
            Date = new DateTime(1, 1, 1);
            ARInvoiceDate = new DateTime(1, 1, 1);
            DebitGLID = 0;
            Amount = 0;
            ARBalance = 0;
            CustomerID = 0;
            Author= "";
            PO = "";
            Processed = true;
            ToApply = 0;
            AdjustmentAmount = 0;
            GLUpdateDate = new DateTime(1, 1, 1);
            Job ="";
            JobID= 0;
            SalesmanID =0;
            DocType=1;
            DueDate = new DateTime(1, 1, 1);
            FCDate = new DateTime(1, 1, 1);
            DiscountDate = new DateTime(1, 1, 1);
            DateClosed = new DateTime(1, 1, 1);
            upsize_ts = 0;
            CurrencyID= 0;
            CurrentRate= 0;
            CurrentMethod=0;
            Process = null;
            ProcessID =0;
            ActivityID=0;

        }
        string GetSQL()
        {
            string sql = "";
            sql = " insert to dbo_tblARInvoices ( ARInvoiceID,OrderID,SrbsPaymentId,Invoice,Batch,Ref,Date,ARInvoiceDate,DebitGLID,Amount ,ARBalance ,CustomerID,Author,PO,Processed,ToApply ,AdjustmentAmount ,GLUpdateDate,Job,JobID,SalesmanID,DocType,DueDate,FCDate,DiscountDate,DateClosed,upsize_ts,CurrencyID,CurrentRate,CurrentMethod,Process,ProcessID,ActivityID) ";
            sql += " values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16,:17,:18,:19,:20,:21,:22,:23,:24,:25,:26,:27,:28,:29,:30,:31,:32,:33)";

            return sql;
        }
        object[] GetObj()
        {
            object[] obj = new object[33];

            obj[0]=ARInvoiceID;
            obj[1]=OrderID;
            obj[2]=SrbsPaymentId;
            obj[3]=Invoice;
            obj[4]=Batch;
            obj[5]=Ref;
            obj[6]=Date;
            obj[7]=ARInvoiceDate;
            obj[8]=DebitGLID;
            obj[9]=Amount ;
            obj[10]=ARBalance ;
            obj[11]=CustomerID;
            obj[12]=Author;
            obj[13]=PO;
            obj[14]=Processed;
            obj[15]=ToApply ;
            obj[16]=AdjustmentAmount ;
            obj[17]=GLUpdateDate;
            obj[18]=Job;
            obj[19]=JobID;
            obj[20]=SalesmanID;
            obj[21]=DocType;
            obj[22]=DueDate;
            obj[23]=FCDate;
            obj[24]=DiscountDate;
            obj[25]=DateClosed;
            obj[26]=upsize_ts;
            obj[27]=CurrencyID;
            obj[28]=CurrentRate;
            obj[29]=CurrentMethod;
            obj[30]=Process;
            obj[31]=ProcessID;
            obj[32] = ActivityID;

            return obj;
        }
    }
    public class abacusInvoiceDetail
    {
        private int? _ActivityID;
        private int _ARInvoiceDetailID;
        private int? _ARInvoiceID;
        private decimal? _CityAmount;
        private int? _CityID;
        private decimal? _CountryAmount;
        private int? _CountryID;
        private decimal? _CountyAmount;
        private int? _CountyID;
        private int? _CreditGLID;
        private string _Description;
        private double? _Discount;
        private decimal? _InternationalAmount;
        private int? _InternationalID;
        private int? _MaterialID;
        private int? _OrderDetailID;
        private int? _OrderID;
        private decimal? _OtherAmount;
        private int? _OtherID;
        private int? _Quantity;
        private decimal? _SplitAmount;
        private int? _SrgsPaymentDetailId;
        private decimal? _StateAmount;
        private int? _StateID;
        private short? _Type;
        private decimal? _UnitPrice;
    }
    public class abacusPayment
    {

    }
    public class abacusPaymentHistory
    {

    }
    public class abacusOrders
    {
        private int? _ActivityID;
        private decimal? _Amount;
        private int? _Batch_;
        private bool _Billed;
        private string _City;
        private bool _Cleared;
        private int? _CustomerID;
        private DateTime? _DateClosed;
        private DateTime? _DateShipped;
        private int? _DebitGLID;
        private DateTime? _DueDate;
        private int? _JobID;
        private int? _Order_;
        private DateTime? _OrderDate;
        private string _OrderedBy;
        private int _OrderID;
        private int? _OrderSourceID;
        private int? _OrderType;
        private string _PackedBy;
        private byte? _PayMethod;
        private string _PO_;
        private bool _Printed;
        private int? _ProcessID;
        private string _Ref_;
        private DateTime? _RequestDate;
        private int? _ReversalOfOrderID;
        private int? _SalesmanID;
        private string _Ship1;
        private string _Ship2;
        private string _ShipMethod;
        private int? _SrgsPaymentId;
        private string _State;
        private short? _Terms;
        private string _Tracking_;
        private string _Zip;
    }
    public class abacusOrdersDetail
    {
        private int? _ActivityID;
        private float? _BackOrdered;
        private decimal? _CityAmount;
        private int? _CityID;
        private decimal? _CountryAmount;
        private int? _CountryID;
        private decimal? _CountyAmount;
        private int? _CountyID;
        private string _Description;
        private double? _Discount;
        private bool? _IncludeKitItems;
        private float? _InStock;
        private decimal? _InternationalAmount;
        private int? _InternationalID;
        private int? _MaterialID;
        private string _MaterialNumber;
        private int _OEOrderDetailID;
        private int? _OrderID;
        private decimal? _OtherAmount;
        private int? _OtherID;
        private float? _Qty;
        private DateTime? _RequestDate;
        private int? _ReversalOfDetailID;
        private int? _SalesGLID;
        private float? _Shipped;
        private decimal? _SplitAmount;
        private int? _SrgsPaymentDetail;
        private decimal? _StateAmount;
        private int? _StateID;
        private decimal? _UnitPrice;
        private byte[] _upsize_ts;
        private int? _Warranty;
    }
    public class abacusMaterials 
    {
        private bool _AddInFulfillmentAllowed;
        private bool _APValid;
        private bool _ARValid;
        private bool _Assembly;
        private decimal? _AverageCost;
        private bool _CatalogItem;
        private int? _CategoryID;
        private decimal? _CurrentAverageCost;
        private decimal? _CurrentCost;
        private bool _Discontinued;
        private float? _Discount;
        private bool _ECommerce;
        private int? _EvaluationClassID;
        private float? _Height;
        private int? _HeightUOMID;
        private int? _IndustrySectorID;
        private bool _Internet;
        private decimal? _InternetPrice;
        private int? _KitID;
        private bool _KitItem;
        private float? _Length;
        private int? _LengthUOMID;
        private bool _LotTracking;
        private string _MaterialDescription;
        private int _MaterialID;
        private string _MaterialNotes;
        private string _MaterialNumber;
        private int? _MaterialTypeID;
        private bool _OEValid;
        private float? _ParValue;
        private bool _POValid;
        private decimal? _PriceDollarAmount;
        private float? _PricePercentage;
        private short? _PriceType;
        private int? _PricingClassID;
        private string _ProductNumber;
        private int? _ReleaseMethodID;
        private decimal? _SellingPrice;
        private decimal? _SellingPriceUSD;
        private bool _Serialized;
        private string _SKU;
        private int? _SrgsServiceId;
        private decimal? _StandardPrice;
        private bool _Taxable;
        private decimal? _UnitCost;
        private string _UPCCode;
        private byte[] _upsize_ts;
        private string _VendorNumber;
        private float? _Weight;
        private int? _WeightUOMID;
        private float? _Width;
        private int? _WidthUOMID;
    }
}
