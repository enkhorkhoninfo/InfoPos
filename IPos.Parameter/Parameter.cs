using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using EServ.Interface;
using EServ.Shared;
using EServ.Data;

using IPos.List;
using IPos.DB;

namespace IPos.Parameter
{
    class Parameter : IModule
    {
        DateTime first;
        //string tablename;
        Hashtable hashtable = new Hashtable();
        //int check = 0;
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            string[] value = { "  AGENTBRANCH", "AGENTBRANCH", "AGENTBRANCH",
                                     "AGENTCORP", "AGENTCORP", "AGENTCORP",
                                     "BACACCOUNT", "BACACCOUNT", "BACACCOUNT",
                                     "BACPRODUCT", "BACPRODUCT", "BACPRODUCT", 
                                     "BANK", "BANK", "BANK", 
                                     "BANKBRANCH", "BANKBRANCH", "BANKBRANCH",
                                     "BRANCH", "BRANCH", "BRANCH", 
                                     "CHART", "CHART", "CHART",
                                     "CLAIMCLOSETYPE", "CLAIMCLOSETYPE", "CLAIMCLOSETYPE", 
                                     "CLAIMOBJECTTYPE", "CLAIMOBJECTTYPE", "CLAIMOBJECTTYPE",
                                     "CLOSETYPE", "CLOSETYPE", "CLOSETYPE",
                                     "CONACCOUNT", "CONACCOUNT", "CONACCOUNT",
                                     "CONPRODUCT", "CONPRODUCT", "CONPRODUCT",
                                     "COUNTRY", "COUNTRY", "COUNTRY",
                                     "CURRENCY", "CURRENCY", "CURRENCY", 
                                     "CURRENCYRATE",
                                     "CUSTCITY", "CUSTCITY", "CUSTCITY", 
                                     "CUSTDISTRICT", "CUSTDISTRICT", "CUSTDISTRICT",
                                     "CUSTOMERTYPE", "CUSTOMERTYPE", "CUSTOMERTYPE",
                                     "CUSTRATE", "CUSTRATE", "CUSTRATE",
                                     "CUSTSUBDISTRICT", "CUSTSUBDISTRICT", "CUSTSUBDISTRICT",
                                     "DICTIONARY", "DICTIONARY", "DICTIONARY",
                                     "DOCTEMPLATE", "DOCTEMPLATE", "DOCTEMPLATE",
                                     "EMPLOYEE", "EMPLOYEE", "EMPLOYEE",
                                     "ENTRY", "ENTRY", "ENTRY", 
                                     "EXPENSETYPE", "EXPENSETYPE", "EXPENSETYPE",
                                     "FAMILYTYPE", "FAMILYTYPE", "FAMILYTYPE",
                                     "FAPOSITION", "FAPOSITION", "FAPOSITION",
                                     "FATYPE", "FATYPE", "FATYPE", 
                                     "FORMULA", "FORMULA", "FORMULA",
                                     "FUND", "FUND", "FUND",
                                     "GENERALPARAM", "GENERALPARAM", "GENERALPARAM",
                                     "INDUSTRY", "INDUSTRY", "INDUSTRY", 
                                     "INSURANCECOMPANY", "INSURANCECOMPANY", "INSURANCECOMPANY",
                                     "INSURANCETYPE", "INSURANCETYPE", "INSURANCETYPE",
                                     "INVENTORYTYPE", "INVENTORYTYPE", "INVENTORYTYPE", 
                                     "LANGUAGE", "LANGUAGE", "LANGUAGE", 
                                     "OBJECT", "OBJECT", "OBJECT",
                                     "OBJECTCLASS", "OBJECTCLASS", "OBJECTCLASS",
                                     "PRODUCT", "PRODUCT", "PRODUCT", 
                                     "RECONTRACTTYPE", "RECONTRACTTYPE", "RECONTRACTTYPE", 
                                     "REINSURANCECONTRACTTYPE", "REINSURANCECONTRACTTYPE", "REINSURANCECONTRACTTYPE", 
                                     "REINSURANCEPRODUCT", "REINSURANCEPRODUCT", "REINSURANCEPRODUCT",
                                     "RISKGROUP", "RISKGROUP", "RISKGROUP",
                                     "RISK", "RISK", "RISK",
                                     "STEP", "STEP", "STEP", 
                                     "SUBINDUSTRY", "SUBINDUSTRY", "SUBINDUSTRY",
                                     "SUBINSURANCETYPE", "SUBREINSURANCETYPE", "SUBREINSURANCETYPE", 
                                     "TXN", "TXN", "TXN", 
                                     "UNITTYPE", "UNITTYPE", "UNITTYPE",
                                     "CUSTOMERMASK", "CUSTOMERMASK", "CUSTOMERMASK"};
            int[] key = {       280102, 280103, 280104,
                                280003, 280004, 280005,
                                180008, 180009, 180010,
                                140113, 140114, 140115,
                                140023, 140024, 140025,
                                140098, 140099, 140100, 
                                140002, 140003, 140004, 
                                220011, 220012, 220013,
                                140158, 140159, 140160,
                                140268, 140269, 140270,
                                140093, 140094, 140095, 
                                190003, 190004, 190005,
                                140118, 140119, 140120,
                                140008, 140009, 140010,
                                140018, 140019, 140020, 
                                140311,
                                140058, 140059, 140060,
                                140063, 140064, 140065,
                                140038, 140039, 140040,
                                140073, 140074, 140075,
                                140068, 140069, 140070, 
                                140303, 140304, 140305,
                                150004, 150005, 150006,
                                180003, 180004, 180005,
                                140168, 140169, 140170,
                                140088, 140089, 140090,
                                140043, 140044, 140045,
                                140243, 140244, 140245, 
                                140138, 140139, 140140, 
                                140183, 140184, 140185, 
                                140293, 140294, 140295,
                                140148, 140149, 140150,
                                140048, 140049, 140050,
                                140263, 140264, 140265,
                                140078, 140079, 140080,
                                140103, 140104, 140105,
                                140013, 140014, 140015, 
                                140193, 140194, 140195, 
                                140238, 140239, 140240, 
                                140143, 140144, 140145,
                                140188, 140189, 140190,
                                140249, 140248, 140250,
                                140259, 140258, 140260,
                                140208, 140209, 140210, 
                                200058, 200059, 200060, 
                                140283, 140284, 140285, 
                                140053, 140054, 140055,
                                140253, 140254, 140255, 
                                140163, 140164, 140165, 
                                140108, 140109, 140110,
                                140128, 140129, 140130};// 
            for (int i = 0; i < 150; i++) //140073,140074,140075,  "DRIVERMASK", "DRIVERMASK", "DRIVERMASK",    "FEETYPE", "FEETYPE", "FEETYPE",    "INDUSTRYTYPECODE", "INDUSTRYTYPECODE", "INDUSTRYTYPECODE",                                         "CHARTGROUP", "CHARTGROUP", "CHARTGROUP",           "PASSMASK", "PASSMASK", "PASSMASK",   "REGISTERMASK", "REGISTERMASK", "REGISTERMASK",       "TXNFIN", "TXNFIN", "TXNFIN",    "USERS", "USERS", "USERS" 
            {
                hashtable.Add(key[i], value[i]);
            }
            Result res = new Result();
            try
            {
                first = DateTime.Now;
                switch (ri.FunctionNo)
                {                                    
                    #region [ Үндсэн хөрөнгийн байршил ]
                    case 140241: 	            // Үндсэн хөрөнгийн байршлын жагсаалт мэдээлэл авах
                        res = Txn140241(ci, ri, db, ref lg);
                        break;
                    /*case 140242: 	            //Үндсэн хөрөнгийн байршлын дэлгэрэнгүй мэдээлэл авах
                        res = Txn140242(ci, ri, db, ref lg);
                        break;*/
                    case 140243: 	            //Үндсэн хөрөнгийн байршил нэмэх
                        res = Txn140243(ci, ri, db, ref lg);
                        break;
                    case 140244: 	            //Үндсэн хөрөнгийн байршил засварлах
                        res = Txn140244(ci, ri, db, ref lg);
                        break;
                    case 140245: 	            //Үндсэн хөрөнгийн байршил устгах
                        res = Txn140245(ci, ri, db, ref lg);
                        break;
                    #endregion                                  
                    #region[Dictionary]
                    case 140301:        //Dictionary бүртгэлийн жагсаалт авах
                        res = DB202301(ci, ri, db, ref lg);
                        break;
                    case 140302:        //Dictionary бүртгэлийн дэлгэрэнгүй мэдээлэл авах
                        res = DB202302(ci, ri, db, ref lg);
                        break;
                    case 140303:        //Dictionary бүртгэлийн мэдээлэл нэмэх
                        res = DB202303(ci, ri, db, ref lg);
                        break;
                    case 140304:        //Dictionary бүртгэлийн мэдээлэл засварлах
                        res = DB202304(ci, ri, db, ref lg);
                        break;
                    case 140305:        //Dictionary бүртгэлийн мэдээлэл устгах
                        res = DB202305(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[DocTemplate]
                    #region[DocModel]
                    case 150000:        //DocTemplate бүртгэлийн жагсаалт авах
                        res = DB201001(ci, ri, db, ref lg);
                        break;
                    case 150004:        //DocTemplate нэмэх
                        res = DB201006(ci, ri, db, ref lg);
                        break;
                    case 150005:        //DocTemplate засварлах
                        res = DB201007(ci, ri, db, ref lg);
                        break;
                    case 150006:        //DocTemplate устгах
                        res = DB201008(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Sql]
                    case 150011:   //DocTemplate Sql бүртгэлийн жагсаалт авах
                        res = DB201003(ci, ri, db, ref lg);
                        break;
                    case 150013:        //DocTemplate Sql нэмэх
                        res = DB201013(ci, ri, db, ref lg);
                        break;
                    case 150014:        //DocTemplate Sql нэмэх
                        res = DB201014(ci, ri, db, ref lg);
                        break;
                    case 150015:        //DocTemplate Sql устгах
                        res = DB201015(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Params]
                    case 150001:        //DocTemplate Parameter жагсаалт авах
                        res = DB201002(ci, ri, db, ref lg);
                        break;
                    case 150017:        //DocTemplate Parameter жагсаалт авах
                        res = DB201017(ci, ri, db, ref lg);
                        break;
                    case 150018:        //DocTemplate Parameter жагсаалт авах
                        res = DB201018(ci, ri, db, ref lg);
                        break;
                    case 150019:        //DocTemplate Parameter жагсаалт авах
                        res = DB201019(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Chain]
                    case 150020:        //DocTemplate Parameter жагсаалт авах
                        res = DB201020(ci, ri, db, ref lg);
                        break;
                    case 150021:        //DocTemplate Parameter хадгалөх
                        res = DB201021(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #endregion
                    //Shortcut
                    #region[Shortcut]
                    case 140321:        //Shortcut бүртгэлийн жагсаалт авах
                        res = DB202321(ci, ri, db, ref lg);
                        break;
                    case 140322:        //Shortcut бүртгэлийн мэдээлэл устгах
                        res = DB202322(ci, ri, db, ref lg);
                        break;
                    case 140323:        //Shortcut бүртгэлийн мэдээлэл нэмэх
                        res = DB202323(ci, ri, db, ref lg);
                        break;
                    case 140324:        //Shortcut бүртгэлийн мэдээлэл засварлах
                        res = DB202324(ci, ri, db, ref lg);
                        break;
                    case 140325:        //Shortcut бүртгэлийн мэдээлэл устгах
                        res = DB202325(ci, ri, db, ref lg);
                        break;
                    #endregion
                    //Dynamic жагсаалт
                    #region[DynamicList]
                    case 140306:        //Dynamic жагсаалт мэдээллийн жагсаалт авах
                        res = DB202306(ci, ri, db, ref lg);
                        break;
                    case 140307:        //Dynamic жагсаалт мэдээллийн дэлгэрэнгүй мэдээлэл авах
                        res = DB202307(ci, ri, db, ref lg);
                        break;
                    case 140308:        //Dynamic жагсаалт мэдээлэл нэмэх
                        res = DB202308(ci, ri, db, ref lg);
                        break;
                    case 140309:        //Dynamic жагсаалт мэдээлэл засварлах
                        res = DB202309(ci, ri, db, ref lg);
                        break;
                    case 140310:        //Dynamic жагсаалт мэдээлэл устгах
                        res = DB202310(ci, ri, db, ref lg);
                        break;
                    #endregion                                                  

                    //Салбар OK   
                    #region[Branch]

                    case 140001: //Салбарын жагсаалт мэдээлэл авах
                        res = SelectBranch(ci, ri, db, ref lg);
                        break;
                    case 140003: //Салбар нэмэх
                        res = AddBranch(ci, ri, db, ref lg);
                        break;
                    case 140005: //Салбар устгах
                        res = DeleteBranch(ci, ri, db, ref lg);
                        break;
                    case 140004: //Салбар засварлах
                        res = EditBranch(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Улс OK
                    #region[Country]

                    case 140006: //Улсын жагсаалт мэдээлэл авах
                        res = SelectCountry(ci, ri, db, ref lg);
                        break;
                    case 140008: //Улсын жагсаалт нэмэх
                        res = AddCountry(ci, ri, db, ref lg);
                        break;
                    case 140010: //Улсын жагсаалт устгах
                        res = DeleteCountry(ci, ri, db, ref lg);
                        break;
                    case 140009: //Улсын жагсаалт засварлах
                        res = EditCountry(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Хэл OK  
                    #region[Language]

                    case 140011: //Хэлний жагсаалт мэдээлэл авах
                        res = SelectLanguage(ci, ri, db, ref lg);
                        break;
                    case 140013: //Хэлний жагсаалт нэмэх
                        res = AddLanguage(ci, ri, db, ref lg);
                        break;
                    case 140015: //Хэлний жагсаалт устгах
                        res = DeleteLanguage(ci, ri, db, ref lg);
                        break;
                    case 140014: //Хэлний жагсаалт засварлах
                        res = EditLanguage(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Валют OK
                    #region[Currency]

                    case 140016: //Валютын жагсаалт мэдээлэл авах
                        res = SelectCurrency(ci, ri, db, ref lg);
                        break;
                    case 140018: //Валютын жагсаалт нэмэх
                        res = AddCurrency(ci, ri, db, ref lg);
                        break;
                    case 140020: //Валютын жагсаалт устгах
                        res = DeleteCurrency(ci, ri, db, ref lg);
                        break;
                    case 140019: //Валютын жагсаалт засварлах
                        res = EditCurrency(ci, ri, db, ref lg);
                        break;
                    case 140311: //Валютын ханш оруулах
                        res = ImportFile(ci, ri, db, ref lg);
                        break;
                    #endregion

                    //Банк OK
                    #region[Bank]

                    case 140021: //Банкны жагсаалт мэдээлэл авах
                        res = SelectBank(ci, ri, db, ref lg);
                        break;
                    case 140023: //Банкны жагсаалт нэмэх
                        res = AddBank(ci, ri, db, ref lg);
                        break;
                    case 140025: //Банкны жагсаалт устгах
                        res = DeleteBank(ci, ri, db, ref lg);
                        break;
                    case 140024: //Банкны жагсаалт засварлах
                        res = EditBank(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Харилцагчийн төрөл OK
                    #region[CustomerType]

                    case 140036: //Харилцагчийн төрөл мэдээлэл жагсаалт авах
                        res = SelectCustomerType(ci, ri, db, ref lg);
                        break;
                    case 140038: //Харилцагчийн төрөл жагсаалт нэмэх
                        res = AddCustomerType(ci, ri, db, ref lg);
                        break;
                    case 140040: //Харилцагчийн төрөл жагсаалт устгах
                        res = DeleteCustomerType(ci, ri, db, ref lg);
                        break;
                    case 140039: //Харилцагчийн төрөл жагсаалт засварлах
                        res = EditCustomerType(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Автомат дансны бүртгэл OK
                    #region[AutoNum]

                    case 140031: //Автомат дансны бүртгэл жагсаалт мэдээлэл авах
                        res = SelectAutoNum(ci, ri, db, ref lg);
                        break;
                    case 140033: //Автомат дансны бүртгэл жагсаалт нэмэх
                        res = AddAutoNum(ci, ri, db, ref lg);
                        break;
                    case 140035: //Автомат дансны бүртгэл жагсаалт устгах
                        res = DeleteAutoNum(ci, ri, db, ref lg);
                        break;
                    case 140034: //Автомат дансны бүртгэл жагсаалт засварлах
                        res = EditAutoNum(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Гэр бүлийн төрөл OK
                    #region[FamilyType]

                    case 140041: //Гэр бүлийн төрөл жагсаалт мэдээлэл авах
                        res = SelectFamilyType(ci, ri, db, ref lg);
                        break;
                    case 140043: //Гэр бүлийн төрөл жагсаалт нэмэх
                        res = AddFamilyType(ci, ri, db, ref lg);
                        break;
                    case 140045: //Гэр бүлийн төрөл жагсаалт устгах
                        res = DeleteFamilyType(ci, ri, db, ref lg);
                        break;
                    case 140044: //Гэр бүлийн төрөл жагсаалт засварлах
                        res = EditFamilyType(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Үйл ажиллагааны чиглэл OK 
                    #region[Industry]

                    case 140046: //Үйл ажиллагааны чиглэл жагсаалт мэдээлэл авах
                        res = SelectIndustry(ci, ri, db, ref lg);
                        break;
                    case 140048: //Үйл ажиллагааны чиглэл жагсаалт нэмэх
                        res = AddIndustry(ci, ri, db, ref lg);
                        break;
                    case 140050: //Үйл ажиллагааны чиглэл жагсаалт устгах
                        res = DeleteIndustry(ci, ri, db, ref lg);
                        break;
                    case 140049: //Үйл ажиллагааны чиглэл жагсаалт засварлах
                        res = EditIndustry(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Үйл ажиллагааны дэд чиглэл OK
                    #region[SubIndustry]

                    case 140051: //Үйл ажиллагааны дэд чиглэл жагсаалт мэдээлэл авах
                        res = SelectSubIndustry(ci, ri, db, ref lg);
                        break;
                    case 140053: //Үйл ажиллагааны дэд чиглэл жагсаалт нэмэх
                        res = AddSubIndustry(ci, ri, db, ref lg);
                        break;
                    case 140055: //Үйл ажиллагааны дэд чиглэл жагсаалт устгах
                        res = DeleteSubIndustry(ci, ri, db, ref lg);
                        break;
                    case 140054: //Үйл ажиллагааны дэд чиглэл жагсаалт засварлах
                        res = EditSubIndustry(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Аймаг хотын бүртгэл OK
                    #region[CustCity]

                    case 140056: //Аймаг хотын бүртгэл жагсаалт мэдээлэл авах
                        res = SelectCustCity(ci, ri, db, ref lg);
                        break;
                    case 140058: //Аймаг хотын бүртгэл жагсаалт нэмэх
                        res = AddCustCity(ci, ri, db, ref lg);
                        break;
                    case 140060: //Аймаг хотын бүртгэл жагсаалт устгах
                        res = DeleteCustCity(ci, ri, db, ref lg);
                        break;
                    case 140059: //Аймаг хотын бүртгэл жагсаалт засварлах
                        res = EditCustCity(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Харилцагчийн ратинг OK
                    #region[CustRate]

                    case 140071: //Харилцагчийн ратинг жагсаалт мэдээлэл авах
                        res = SelectCustRate(ci, ri, db, ref lg);
                        break;
                    case 140073: //Харилцагчийн ратинг жагсаалт нэмэх
                        res = AddCustRate(ci, ri, db, ref lg);
                        break;
                    case 140075: //Харилцагчийн ратинг жагсаалт устгах
                        res = DeleteCustRate(ci, ri, db, ref lg);
                        break;
                    case 140074: //АХарилцагчийн ратинг жагсаалт засварлах
                        res = EditCustRate(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Сум дүүргийн бүртгэл OK
                    #region[CustDistrict]

                    case 140061: //Сум дүүргийн бүртгэл жагсаалт мэдээлэл авах
                        res = SelectCustDistrict(ci, ri, db, ref lg);
                        break;
                    case 140063: //Сум дүүргийн бүртгэл жагсаалт нэмэх
                        res = AddCustDistrict(ci, ri, db, ref lg);
                        break;
                    case 140065: //Сум дүүргийн бүртгэл жагсаалт устгах
                        res = DeleteCustDistrict(ci, ri, db, ref lg);
                        break;
                    case 140064: //Сум дүүргийн бүртгэл жагсаалт засварлах
                        res = EditCustDistrict(ci, ri, db, ref lg);
                        break;

                    #endregion                                                                        

                    //Бараа материалын төрөл OK
                    #region[InventoryType]

                    case 140101: //Бараа материалын төрөл жагсаалт мэдээлэл авах
                        res = SelectInventoryType(ci, ri, db, ref lg);
                        break;
                    case 140103: //Бараа материалын төрөл жагсаалт нэмэх
                        res = AddInventoryType(ci, ri, db, ref lg);
                        break;
                    case 140105: //Бараа материалын төрөл жагсаалт устгах
                        res = DeleteInventoryType(ci, ri, db, ref lg);
                        break;
                    case 140104: //Бараа материалын төрөл жагсаалт засварлах
                        res = EditInventoryType(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Байгуулгын дансны бүтээгдэхүүн OK
                    #region[BacProduct]

                    case 140111: //Байгуулгын дансны бүтээгдэхүүн жагсаалт мэдээлэл авах
                        res = SelectBacProduct(ci, ri, db, ref lg);
                        break;
                    case 140113: //Байгуулгын дансны бүтээгдэхүүн жагсаалт нэмэх
                        res = AddBacProduct(ci, ri, db, ref lg);
                        break;
                    case 140115: //Байгуулгын дансны бүтээгдэхүүн жагсаалт устгах
                        res = DeleteBacProduct(ci, ri, db, ref lg);
                        break;
                    case 140114: //Байгуулгын дансны бүтээгдэхүүн жагсаалт засварлах
                        res = EditBacProduct(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Балансын гадуурх дансны бүтээгдэхүүн OK
                    #region[ConProduct]

                    case 140116: //Балансын гадуурх дансны бүтээгдэхүүн жагсаалт мэдээлэл авах
                        res = SelectConProduct(ci, ri, db, ref lg);
                        break;
                    case 140118: //Балансын гадуурх дансны бүтээгдэхүүн жагсаалт нэмэх
                        res = AddConProduct(ci, ri, db, ref lg);
                        break;
                    case 140120: //Балансын гадуурх дансны бүтээгдэхүүн жагсаалт устгах
                        res = DeleteConProduct(ci, ri, db, ref lg);
                        break;
                    case 140119: //Балансын гадуурх дансны бүтээгдэхүүн жагсаалт засварлах
                        res = EditConProduct(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Үндсэн хөрөнгийн материал төрөл OK
                    #region[FAType]

                    case 140136: //Үндсэн хөрөнгийн материал төрөл жагсаалт мэдээлэл авах
                        res = SelectFAType(ci, ri, db, ref lg);
                        break;
                    case 140138: //Үндсэн хөрөнгийн материал төрөл жагсаалт нэмэх
                        res = AddFAType(ci, ri, db, ref lg);
                        break;
                    case 140140: //Үндсэн хөрөнгийн материал төрөл жагсаалт устгах
                        res = DeleteFAType(ci, ri, db, ref lg);
                        break;
                    case 140139: //Үндсэн хөрөнгийн материал төрөл жагсаалт засварлах
                        res = EditFAType(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Харилцагчийн маск OK
                    #region[CustomerMask]

                    case 140126: //Харилцагчийн маск жагсаалт мэдээлэл авах
                        res = SelectCustomerMask(ci, ri, db, ref lg);
                        break;
                    case 140128: //Харилцагчийн маск жагсаалт нэмэх
                        res = AddCustomerMask(ci, ri, db, ref lg);
                        break;
                    case 140130: //Харилцагчийн маск жагсаалт устгах
                        res = DeleteCustomerMask(ci, ri, db, ref lg);
                        break;
                    case 140129: //Харилцагчийн маск жагсаалт засварлах
                        res = EditCustomerMask(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Балансын данс төрөл OK
                    #region[ChartGroup]

                    case 140121: //Балансын данс төрөл жагсаалт мэдээлэл авах
                        res = SelectChartGroup(ci, ri, db, ref lg);
                        break;
                    case 140123: //Балансын данс төрөл жагсаалт нэмэх
                        res = AddChartGroup(ci, ri, db, ref lg);
                        break;
                    case 140125: //Балансын данс төрөл жагсаалт устгах
                        res = DeleteChartGroup(ci, ri, db, ref lg);
                        break;
                    case 140124: //Балансын данс төрөл жагсаалт засварлах
                        res = EditChartGroup(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Банкны салбар OK
                    #region[BankBranch]

                    case 140096: //Банкны салбар жагсаалт мэдээлэл авах
                        res = SelectBankBranch(ci, ri, db, ref lg);
                        break;
                    case 140098: //Банкны салбар жагсаалт нэмэх
                        res = AddBankBranch(ci, ri, db, ref lg);
                        break;
                    case 140100: //Банкны салбар жагсаалт устгах
                        res = DeleteBankBranch(ci, ri, db, ref lg);
                        break;
                    case 140099: //Банкны салбар жагсаалт засварлах
                        res = EditBankBranch(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Баг хорооны бүртгэл
                    #region[CustSubDistrict]

                    case 140066: //Баг хорооны бүртгэл жагсаалт мэдээлэл авах
                        res = SelectCustSubDistrict(ci, ri, db, ref lg);
                        break;
                    case 140068: //Баг хорооны бүртгэл жагсаалт нэмэх
                        res = AddCustSubDistrict(ci, ri, db, ref lg);
                        break;
                    case 140070: //Баг хорооны бүртгэл жагсаалт устгах
                        res = DeleteCustSubDistrict(ci, ri, db, ref lg);
                        break;
                    case 140069: //Баг хорооны бүртгэл жагсаалт засварлах
                        res = EditCustSubDistrict(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Ерөнхий параметр
                    #region[GenParam]

                    case 140146: //Ерөнхий параметр жагсаалт мэдээлэл авах
                        res = SelectGenParam(ci, ri, db, ref lg);
                        break;
                        //case 140148: //Ерөнхий параметр жагсаалт нэмэх
                        //    res = AddGenParam(ci, ri, db, ref lg);
                        //    break;
                        //case 140150: //Ерөнхий параметр жагсаалт устгах
                        //    res = DeleteGenParam(ci, ri, db, ref lg);
                    case 140149: //Ерөнхий параметр жагсаалт засварлах
                        res = EditGenParam(ci, ri, db, ref lg);
                        break;

                    #endregion                    

                    //Гүйлгээний код 
                    #region[Txn]

                    case 140161: //Гүйлгээний код төрөл жагсаалт мэдээлэл авах
                        res = SelectTxn(ci, ri, db, ref lg);
                        break;
                    case 140163: //Гүйлгээний код төрөл жагсаалт нэмэх
                        res = AddTxn(ci, ri, db, ref lg);
                        break;
                    case 140165: //Гүйлгээний код төрөл жагсаалт устгах
                        res = DeleteTxn(ci, ri, db, ref lg);
                        break;
                    case 140164: //Гүйлгээний код төрөл жагсаалт засварлах
                        res = EditTxn(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Гүйлгээний оролт
                    #region[TxnEntry]

                    case 140166: //Гүйлгээний оролт төрөл жагсаалт мэдээлэл авах
                        res = SelectTxnEntry(ci, ri, db, ref lg);
                        break;
                    case 140168: //Гүйлгээний оролт төрөл жагсаалт нэмэх
                        res = AddTxnEntry(ci, ri, db, ref lg);
                        break;
                    case 140170: //Гүйлгээний оролт төрөл жагсаалт устгах
                        res = DeleteTxnEntry(ci, ri, db, ref lg);
                        break;
                    case 140169: //Гүйлгээний оролт төрөл жагсаалт засварлах
                        res = EditTxnEntry(ci, ri, db, ref lg);
                        break;

                    #endregion

                    //Гүйлгээний санхүүгийн бичилт
                    #region[TxnWriteWriteWrite]

                    case 140176:
                        res = SelectTxnWrite(ci, ri, db, ref lg);
                        break;
                    case 140178:
                        res = AddTxnWrite(ci, ri, db, ref lg);
                        break;
                    case 140180:
                        res = DeleteTxnWrite(ci, ri, db, ref lg);
                        break;
                    case 140179:
                        res = EditTxnWrite(ci, ri, db, ref lg);
                        break;

                    #endregion                                      

                    //Түр дансны бүртгэл
                    #region[AccountCode]

                    case 140026: //Түр дансны бүртгэл жагсаалт мэдээлэл авах
                        res = SelectAccountCode(ci, ri, db, ref lg);
                        break;
                    case 140028: //Түр дансны бүртгэл жагсаалт нэмэх
                        res = AddAccountCode(ci, ri, db, ref lg);
                        break;
                    case 140030: //Түр дансны бүртгэл жагсаалт устгах
                        res = DeleteAccountCode(ci, ri, db, ref lg);
                        break;
                    case 140029: //Түр дансны бүртгэл жагсаалт засварлах
                        res = EditAccountCode(ci, ri, db, ref lg);
                        break;

                    #endregion
                                                 
                    #region[]
                    case 140276:
                        res = SelectStepItem(ci, ri, db, ref lg);
                        break;
                    case 140278:
                        res = AddStepItem(ci, ri, db, ref lg);
                        break;
                    case 140280:
                        res = DeleteStepItem(ci, ri, db, ref lg);
                        break;
                    case 140279:
                        res = EditStepItem(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[]
                    case 140281:
                        res = SelectStep(ci, ri, db, ref lg);
                        break;
                    case 140283:
                        res = AddStep(ci, ri, db, ref lg);
                        break;
                    case 140285:
                        res = DeleteStep(ci, ri, db, ref lg);
                        break;
                    case 140284:
                        res = EditStep(ci, ri, db, ref lg);
                        break;
                    case 140287:
                        res = SelectStepI(ci, ri, db, ref lg);
                        break;
                    case 140286:
                        res = SaveStepI(ci, ri, db, ref lg);
                        break;
                    #endregion                  
                    #region[CRM ProjectType]
                    case 310010:
                        res = DB240000(ci, ri, db, ref lg);
                        break;
                    //case 310001:
                    //    res = DB240001(ci, ri, db, ref lg);
                    //    break;
                    case 310002:
                        res = DB240002(ci, ri, db, ref lg);
                        break;
                    case 310003:
                        res = DB240003(ci, ri, db, ref lg);
                        break;
                    case 310004:
                        res = DB240004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM IssueTypes]
                    case 310005:
                        res = DB241000(ci, ri, db, ref lg);
                        break;
                    //case 310005:
                    //    res = DB241001(ci, ri, db, ref lg);
                    //    break;
                    case 310007:
                        res = DB241002(ci, ri, db, ref lg);
                        break;
                    case 310008:
                        res = DB241003(ci, ri, db, ref lg);
                        break;
                    case 310009:
                        res = DB241004(ci, ri, db, ref lg);
                        break;
                    case 310085:
                        res = DB243005(ci, ri, db, ref lg);
                        break;
                    case 310086:
                        res = DB243006(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM IssueTracks]
                    case 310100:
                        res = DB242000(ci, ri, db, ref lg);
                        break;
                    //case 3100011:
                    //    res = DB242001(ci, ri, db, ref lg);
                    //    break;
                    case 310012:
                        res = DB242002(ci, ri, db, ref lg);
                        break;
                    case 310013:
                        res = DB242003(ci, ri, db, ref lg);
                        break;
                    case 310014:
                        res = DB242004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM IssueActionType]
                    case 310020:
                        res = DB244000(ci, ri, db, ref lg);
                        break;
                    //case 310021:
                    //    res = DB244001(ci, ri, db, ref lg);
                    //    break;
                    case 310022:
                        res = DB244002(ci, ri, db, ref lg);
                        break;
                    case 310023:
                        res = DB244003(ci, ri, db, ref lg);
                        break;
                    case 310024:
                        res = DB244004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM IssueResolutionType]
                    case 310025:
                        res = DB245000(ci, ri, db, ref lg);
                        break;
                    //case 310021:
                    //    res = DB245001(ci, ri, db, ref lg);
                    //    break;
                    case 310027:
                        res = DB245002(ci, ri, db, ref lg);
                        break;
                    case 310028:
                        res = DB245003(ci, ri, db, ref lg);
                        break;
                    case 310029:
                        res = DB245004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM IssueMemberPurp]
                    case 310030:
                        res = DB246000(ci, ri, db, ref lg);
                        break;
                    //case 310031:
                    //    res = DB246001(ci, ri, db, ref lg);
                    //    break;
                    case 310032:
                        res = DB246002(ci, ri, db, ref lg);
                        break;
                    case 310033:
                        res = DB246003(ci, ri, db, ref lg);
                        break;
                    case 310034:
                        res = DB246004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM PriorityType]
                    case 310035:
                        res = DB247000(ci, ri, db, ref lg);
                        break;
                    //case 310036:
                    //    res = DB247001(ci, ri, db, ref lg);
                    //    break;
                    case 310037:
                        res = DB247002(ci, ri, db, ref lg);
                        break;
                    case 310038:
                        res = DB247003(ci, ri, db, ref lg);
                        break;
                    case 310039:
                        res = DB247004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM LinkType]
                    case 310040:
                        res = DB248000(ci, ri, db, ref lg);
                        break;
                    //case 310041:
                    //    res = DB248001 (ci, ri, db, ref lg);
                    //    break;
                    case 310042:
                        res = DB248002(ci, ri, db, ref lg);
                        break;
                    case 310043:
                        res = DB248003(ci, ri, db, ref lg);
                        break;
                    case 310044:
                        res = DB248004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM Project]
                    case 310069:
                        res = DB254000(ci, ri, db, ref lg);
                        break;
                    case 310070:
                        res = DB254001(ci, ri, db, ref lg);
                        break;
                    case 310071:
                        res = DB254002(ci, ri, db, ref lg);
                        break;
                    case 310072:
                        res = DB254003(ci, ri, db, ref lg);
                        break;
                    case 310073:
                        res = DB254004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM ProjectComp]
                    case 310074:
                        res = DB255000(ci, ri, db, ref lg);
                        break;
                    //case 310075:
                    //    res = DB255001(ci, ri, db, ref lg);
                    //    break;
                    case 310076:
                        res = DB255002(ci, ri, db, ref lg);
                        break;
                    case 310077:
                        res = DB255003(ci, ri, db, ref lg);
                        break;
                    case 310078:
                        res = DB255004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM NotifySchema]
                    case 310045:	//Мэдэгдэлийн схем жагсаалт авах
                        res = DB249000(ci, ri, db, ref lg);
                        break;
                    //310046:     //Мэдэгдэлийн схем дэлгэрэнгүй мэдээлэл авах
                    //    res = DB249001(ci, ri, db, ref lg);
                    //    break;
                    case 310047: 	//Мэдэгдэлийн схем шинээр нэмэх
                        res = DB249002(ci, ri, db, ref lg);
                        break;
                    case 310048:	//Мэдэгдэлийн схем засварлах
                        res = DB249003(ci, ri, db, ref lg);
                        break;
                    case 310049:	//Мэдэгдэлийн схем  устгах
                        res = DB249004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM NotifyTXNSchema]
                    case 310050:	//Мэдэгдэлийн схем гүйлгээ жагсаалт авах
                        res = DB250000(ci, ri, db, ref lg);
                        break;
                    //310051:     //Мэдэгдэлийн схем гүйлгээ дэлгэрэнгүй мэдээлэл авах
                    //    res = DB250001(ci, ri, db, ref lg);
                    //    break;
                    case 310052: 	//Мэдэгдэлийн схем гүйлгээ шинээр нэмэх
                        res = DB250002(ci, ri, db, ref lg);
                        break;
                    case 310053:	//Мэдэгдэлийн схем гүйлгээ засварлах
                        res = DB250003(ci, ri, db, ref lg);
                        break;
                    case 310054:	//Мэдэгдэлийн схем гүйлгээ устгах
                        res = DB250004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM PermSchema]
                    case 310084:	//Эрхийн схем жагсаалт авах
                        res = DB251000(ci, ri, db, ref lg);
                        break;
                    //310055:     //Эрхийн схем дэлгэрэнгүй мэдээлэл авах
                    //    res = DB251001(ci, ri, db, ref lg);
                    //    break;
                    case 310056: 	//Эрхийн схем шинээр нэмэх
                        res = DB251002(ci, ri, db, ref lg);
                        break;
                    case 310057:	//Эрхийн схем засварлах
                        res = DB251003(ci, ri, db, ref lg);
                        break;
                    case 310058:	//Эрхийн схем  устгах
                        res = DB251004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[CRM PermSchemaTXN]
                    case 310059:	//Эрхийн схем гүйлгээ жагсаалт авах
                        res = DB252000(ci, ri, db, ref lg);
                        break;
                    //310060:     //Эрхийн схем гүйлгээ дэлгэрэнгүй мэдээлэл авах
                    //    res = DB252001(ci, ri, db, ref lg);
                    //    break;
                    case 310061: 	//Эрхийн схем гүйлгээ шинээр нэмэх
                        res = DB252002(ci, ri, db, ref lg);
                        break;
                    case 310062:	//Эрхийн схем гүйлгээ засварлах
                        res = DB252003(ci, ri, db, ref lg);
                        break;
                    case 310063:	//Эрхийн схем гүйлгээ устгах
                        res = DB252004(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Параметр шинэчлэх]
                    case 140000:	//Параметр шинэчлэх
                        res = ParamUpdate(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Dynamic Report]
                    case 140421:	//Dynamic Тайлангын параметр
                        res = Txn140421(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Docement бүлэглэх]
                    case 150022: //Документ загвар бүлэглэх жагсаалт мэдээлэл авах
                        res = Txn150022(ci, ri, db, ref lg);
                        break;
                    case 150023: //Документ загвар бүлэглэх дэлгэрэнгүй мэдээлэл авах
                        res = Txn150023(ci, ri, db, ref lg);
                        break;
                    case 150024: //Документ загвар бүлэглэл нэмэх
                        res = Txn150024(ci, ri, db, ref lg);
                        break;
                    case 150025: //Документ загвар бүлэглэл засварлах
                        res = Txn150025(ci, ri, db, ref lg);
                        break;
                    case 150026: //Документ загвар бүлэглэл устгах
                        res = Txn150026(ci, ri, db, ref lg);
                        break;
                    case 150027: //Документ загварын холбоосын жагсаалт мэдээлэл авах
                        res = Txn150027(ci, ri, db, ref lg);
                        break;
                    case 150029: //Документ загварын холбоос нэмэх 
                        res = Txn150029(ci, ri, db, ref lg);
                        break;
                    #endregion
                    #region[Dynamic Report Group]
                        case 140422: //Dynamic тайлангийн бүлгийн жагсаалт мэдээлэл авах
                            res = Txn140422(ci, ri, db, ref lg);
                            break;
                        case 140423: //Dynamic тайлангийн бүлгийн дэлгэрэнгүй мэдээлэл авах
                            res = Txn140423(ci, ri, db, ref lg);
                            break;
                        case 140424: //Dynamic тайлангийн бүлэг нэмэх
                            res = Txn140424(ci, ri, db, ref lg);
                            break;
                        case 140425: //Dynamic тайлангийн бүлэг засварлах
                            res = Txn140425(ci, ri, db, ref lg);
                            break;
                        case 140426: //Dynamic тайлангийн бүлэг устгах
                            res = Txn140426(ci, ri, db, ref lg);
                            break;
                        case 140427: //Документ загварын холбоосын жагсаалт мэдээлэл авах
                            res = Txn140427(ci, ri, db, ref lg);
                            break;
                        case 140428: //Документ загварын холбоос нэмэх 
                            res = Txn140428(ci, ri, db, ref lg);
                            break;
                    #endregion
                    #region[Харилцагчтай холбоо барисан төрөл]
                        case 120067: //Харилцагчийн холбоо барьсан төрлийн жагсаалт авах
                            res = Txn120067(ci, ri, db, ref lg);
                            break;
                        case 120068: //Харилцагчийн холбоо барьсан төрлийн дэлгэрэнгүй мэдээлэл авах
                            res = Txn120068(ci, ri, db, ref lg);
                            break;
                        case 120069: //Харилцагчийн холбоо барьсан төрөл шинээр нэмэх
                            res = Txn120069(ci, ri, db, ref lg);
                            break;
                        case 120070: //Харилцагчийн холбоо барьсан төрөл засварлах
                            res = Txn120070(ci, ri, db, ref lg);
                            break;
                        case 120071: //Харилцагчийн холбоо барьсан төрөл устгах
                            res = Txn120071(ci, ri, db, ref lg);
                            break;
                        #endregion[]
                    #region[Өдрийн төрлийн бүртгэл]
                        case 140131: //Өдрийн төрлийн бүртгэл жагсаалт мэдээлэл авах
                            res = Txn140131(ci, ri, db, ref lg);
                            break;
                        case 140132: //Өдрийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
                            res = Txn140132(ci, ri, db, ref lg);
                            break;
                        case 140133: //Өдрийн төрлийн бүртгэл шинээр нэмэх
                            res = Txn140133(ci, ri, db, ref lg);
                            break;
                        case 140134: //Өдрийн төрлийн бүртгэл засварлах
                            res = Txn140134(ci, ri, db, ref lg);
                            break;
                        case 140135: //Өдрийн төрлийн бүртгэл устгах
                            res = Txn140135(ci, ri, db, ref lg);
                            break;
                        #endregion[]
                    #region[Цаг агаарын төрлийн бүртгэл]
                        case 140141: //Цаг агаарын төрлийн бүртгэл жагсаалт мэдээлэл авах
                            res = Txn140141(ci, ri, db, ref lg);
                            break;
                        case 140142: //Цаг агаарын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
                            res = Txn140142(ci, ri, db, ref lg);
                            break;
                        case 140143: //Цаг агаарын төрлийн бүртгэл шинээр нэмэх
                            res = Txn140143(ci, ri, db, ref lg);
                            break;
                        case 140144: //Цаг агаарын төрлийн бүртгэл засварлах
                            res = Txn140144(ci, ri, db, ref lg);
                            break;
                        case 140145: //Цаг агаарын төрлийн бүртгэл устгах
                            res = Txn140145(ci, ri, db, ref lg);
                            break;
                   #endregion[]
                        #region[POS ын бүртгэл]
                        case 140076: //POS ын бүртгэл жагсаалт мэдээлэл авах
                            res = Txn140076(ci, ri, db, ref lg);
                            break;
                        case 140077: //POS ын бүртгэл дэлгэрэнгүй мэдээлэл авах
                            res = Txn140077(ci, ri, db, ref lg);
                            break;
                        case 140078: //POS ын бүртгэл шинээр нэмэх
                            res = Txn140078(ci, ri, db, ref lg);
                            break;
                        case 140079: //POS ын бүртгэл засварлах
                            res = Txn140079(ci, ri, db, ref lg);
                            break;
                        case 140080: //POS ын бүртгэл устгах
                            res = Txn140080(ci, ri, db, ref lg);
                            break;
                        #endregion[]
                        #region[Бараа материалын төрлийн бүртгэл]
                        case 140156: //Бараа материалын төрлийн бүртгэл жагсаалт мэдээлэл авах
                            res = Txn140156(ci, ri, db, ref lg);
                            break;
                        case 140157: //Бараа материалын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
                            res = Txn140157(ci, ri, db, ref lg);
                            break;
                        case 140158: //Бараа материалын төрлийн бүртгэл шинээр нэмэх
                            res = Txn140158(ci, ri, db, ref lg);
                            break;
                        case 140159: //Бараа материалын төрлийн бүртгэл төлөвлөгөө засварлах
                            res = Txn140159(ci, ri, db, ref lg);
                            break;
                        case 140160: //Бараа материалын төрлийн бүртгэл төлөвлөгөө устгах
                            res = Txn140160(ci, ri, db, ref lg);
                            break;
                        #endregion[]



                        #region[Тагийн төрлийн бүртгэл]
                        case 140171: //
                            res = Txn140171(ci, ri, db, ref lg);
                            break;
                        case 140172: //
                            res = Txn140172(ci, ri, db, ref lg);
                            break;
                        case 140173: //
                            res = Txn140173(ci, ri, db, ref lg);
                            break;
                        case 140174: //
                            res = Txn140174(ci, ri, db, ref lg);
                            break;
                        case 140175: //
                            res = Txn140175(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[ҮЙЛЧИЛГЭЭНИЙ ТӨРЛИЙН БҮРТГЭЛ]
                        case 140186: //
                            res = Txn140186(ci, ri, db, ref lg);
                            break;
                        case 140187: //
                            res = Txn140187(ci, ri, db, ref lg);
                            break;
                        case 140188: //
                            res = Txn140188(ci, ri, db, ref lg);
                            break;
                        case 140189: //
                            res = Txn140189(ci, ri, db, ref lg);
                            break;
                        case 140190: //
                            res = Txn140190(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[МӨНГӨН ТЭМДЭГТИЙН ДЭВСГЭРТИЙН БҮРТГЭЛ]
                        case 140191: //
                            res = Txn140191(ci, ri, db, ref lg);
                            break;
                        case 140192: //
                            res = Txn140192(ci, ri, db, ref lg);
                            break;
                        case 140193: //
                            res = Txn140193(ci, ri, db, ref lg);
                            break;
                        case 140194: //
                            res = Txn140194(ci, ri, db, ref lg);
                            break;
                        case 140195: //
                            res = Txn140195(ci, ri, db, ref lg);
                            break;
                        #endregion[]


                        #region[Төлбөрийн төрлийн код]
                        case 140201: //
                            res = Txn140201(ci, ri, db, ref lg);
                            break;
                        case 140202: //
                            res = Txn140202(ci, ri, db, ref lg);
                            break;
                        case 140203: //
                            res = Txn140203(ci, ri, db, ref lg);
                            break;
                        case 140204: //
                            res = Txn140204(ci, ri, db, ref lg);
                            break;
                        case 140205: //
                            res = Txn140205(ci, ri, db, ref lg);
                            break;
                        #endregion[]


                        #region[Брэндийн бүртгэл]
                        case 140151: //
                            res = Txn140151(ci, ri, db, ref lg);
                            break;
                        case 140152: //
                            res = Txn140152(ci, ri, db, ref lg);
                            break;
                        case 140153: //
                            res = Txn140153(ci, ri, db, ref lg);
                            break;
                        case 140154: //
                            res = Txn140154(ci, ri, db, ref lg);
                            break;
                        case 140155: //
                            res = Txn140155(ci, ri, db, ref lg);
                            break;
                        #endregion[]


                        #region[ХУВААРИЛАЛТЫН ТӨРЛИЙН БҮРТГЭЛ]
                        case 140181: //
                            res = Txn140181(ci, ri, db, ref lg);
                            break;
                        case 140182: //
                            res = Txn140182(ci, ri, db, ref lg);
                            break;
                        case 140183: //
                            res = Txn140183(ci, ri, db, ref lg);
                            break;
                        case 140184: //
                            res = Txn140184(ci, ri, db, ref lg);
                            break;
                        case 140185: //
                            res = Txn140185(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[Барааны бүртгэл]
                        case 140211: //
                            res = Txn140211(ci, ri, db, ref lg);
                            break;
                        case 140212: //
                            res = Txn140212(ci, ri, db, ref lg);
                            break;
                        case 140213: //
                            res = Txn140213(ci, ri, db, ref lg);
                            break;
                        case 140214: //
                            res = Txn140214(ci, ri, db, ref lg);
                            break;
                        case 140215: //
                            res = Txn140215(ci, ri, db, ref lg);
                            break;
                        case 140266: //
                            res = Txn140266(ci, ri, db, ref lg);
                            break;
                        case 140267: //
                            res = Txn140267(ci, ri, db, ref lg);
                            break;
                        case 140268: //
                            res = Txn140268(ci, ri, db, ref lg);
                            break;
                        case 140269: //
                            res = Txn140269(ci, ri, db, ref lg);
                            break;
                        case 140270: //
                            res = Txn140270(ci, ri, db, ref lg);
                            break;

                        case 140291: //
                            res = Txn140291(ci, ri, db, ref lg);
                            break;
                        case 140292: //
                            res = Txn140292(ci, ri, db, ref lg);
                            break;
                        case 140293: //
                            res = Txn140293(ci, ri, db, ref lg);
                            break;
                        case 140294: //
                            res = Txn140294(ci, ri, db, ref lg);
                            break;
                        case 140295: //
                            res = Txn140295(ci, ri, db, ref lg);
                            break;



                        #endregion[]

                        #region[Барааны ангилалын бүртгэл]
                        case 140216: //
                            res = Txn140216(ci, ri, db, ref lg);
                            break;
                        case 140217: //
                            res = Txn140217(ci, ri, db, ref lg);
                            break;
                        case 140218: //
                            res = Txn140218(ci, ri, db, ref lg);
                            break;
                        case 140219: //
                            res = Txn140219(ci, ri, db, ref lg);
                            break;
                        case 140220: //
                            res = Txn140220(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[Үйлчилгээний ангилалын бүртгэл]
                        case 140221: //
                            res = Txn140221(ci, ri, db, ref lg);
                            break;
                        case 140222: //
                            res = Txn140222(ci, ri, db, ref lg);
                            break;
                        case 140223: //
                            res = Txn140223(ci, ri, db, ref lg);
                            break;
                        case 140224: //
                            res = Txn140224(ci, ri, db, ref lg);
                            break;
                        case 140225: //
                            res = Txn140225(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[Үйлчилгээний бүртгэл]
                        case 140226: //
                            res = Txn140226(ci, ri, db, ref lg);
                            break;
                        case 140227: //
                            res = Txn140227(ci, ri, db, ref lg);
                            break;
                        case 140228: //
                            res = Txn140228(ci, ri, db, ref lg);
                            break;
                        case 140229: //
                            res = Txn140229(ci, ri, db, ref lg);
                            break;
                        case 140230: //
                            res = Txn140230(ci, ri, db, ref lg);
                            break;


                        case 140236: //
                            res = Txn140236(ci, ri, db, ref lg);
                            break;
                        case 140237: //
                            res = Txn140237(ci, ri, db, ref lg);
                            break;
                        case 140238: //
                            res = Txn140238(ci, ri, db, ref lg);
                            break;
                        case 140239: //
                            res = Txn140239(ci, ri, db, ref lg);
                            break;
                        case 140240: //
                            res = Txn140240(ci, ri, db, ref lg);
                            break;



                        case 140271: //
                            res = Txn140271(ci, ri, db, ref lg);
                            break;
                        case 140272: //
                            res = Txn140272(ci, ri, db, ref lg);
                            break;
                        case 140273: //
                            res = Txn140273(ci, ri, db, ref lg);
                            break;
                        case 140274: //
                            res = Txn140274(ci, ri, db, ref lg);
                            break;
                        case 140275: //
                            res = Txn140275(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[UnitType]

                        case 140106: //Бараа материалын нэгжийн төрөл жагсаалт мэдээлэл авах
                            res = SelectUnitType(ci, ri, db, ref lg);
                            break;
                        case 140108: //Бараа материалын нэгжийн төрөл жагсаалт нэмэх
                            res = AddUnitType(ci, ri, db, ref lg);
                            break;
                        case 140110: //Бараа материалын нэгжийн төрөл жагсаалт устгах
                            res = DeleteUnitType(ci, ri, db, ref lg);
                            break;
                        case 140109: //Бараа материалын нэгжийн төрөл жагсаалт засварлах
                            res = EditUnitType(ci, ri, db, ref lg);
                            break;

                        #endregion

                        #region[Гэрээний төрлийн бүртгэл]
                        case 140231: //
                            res = Txn140231(ci, ri, db, ref lg);
                            break;
                        case 140232: //
                            res = Txn140232(ci, ri, db, ref lg);
                            break;
                        case 140233: //
                            res = Txn140233(ci, ri, db, ref lg);
                            break;
                        case 140234: //
                            res = Txn140234(ci, ri, db, ref lg);
                            break;
                        case 140235: //
                            res = Txn140235(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[Барааны багцын бүртгэл]
                        case 140246: //
                            res = Txn140246(ci, ri, db, ref lg);
                            break;
                        case 140247: //
                            res = Txn140247(ci, ri, db, ref lg);
                            break;
                        case 140248: //
                            res = Txn140248(ci, ri, db, ref lg);
                            break;
                        case 140249: //
                            res = Txn140249(ci, ri, db, ref lg);
                            break;
                        case 140250: //
                            res = Txn140250(ci, ri, db, ref lg);
                            break; //
                        case 140251: //
                            res = Txn140251(ci, ri, db, ref lg);
                            break;
                        case 140252: //
                            res = Txn140252(ci, ri, db, ref lg);
                            break;
                        case 140253: //
                            res = Txn140253(ci, ri, db, ref lg);
                            break;
                        case 140254: //
                            res = Txn140254(ci, ri, db, ref lg);
                            break;
                        case 140255: //
                            res = Txn140255(ci, ri, db, ref lg);
                            break;


                        case 140256: //
                            res = Txn140256(ci, ri, db, ref lg);
                            break;
                        case 140257: //
                            res = Txn140257(ci, ri, db, ref lg);
                            break;
                        case 140258: //
                            res = Txn140258(ci, ri, db, ref lg);
                            break;
                        case 140259: //
                            res = Txn140259(ci, ri, db, ref lg);
                            break;
                        case 140260: //
                            res = Txn140260(ci, ri, db, ref lg);
                            break;


                        case 140261: //
                            res = Txn140261(ci, ri, db, ref lg);
                            break;
                        case 140262: //
                            res = Txn140262(ci, ri, db, ref lg);
                            break;
                        case 140263: //
                            res = Txn140263(ci, ri, db, ref lg);
                            break;
                        case 140264: //
                            res = Txn140264(ci, ri, db, ref lg);
                            break;
                        case 140265: //
                            res = Txn140265(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр ]
                        case 140296:
                            res = Txn140296(ci, ri, db, ref lg);
                            break;
                        case 140297:
                            res = Txn140297(ci, ri, db, ref lg);
                            break;
                        case 140298:
                            res = Txn140298(ci, ri, db, ref lg);
                            break;
                        case 140299:
                            res = Txn140299(ci, ri, db, ref lg);
                            break;
                        case 140300:
                            res = Txn140300(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[Эвдэрлийн төрлийн бүртгэл]
                        case 140326:
                            res = Txn140326(ci, ri, db, ref lg);
                            break;
                        case 140327:
                            res = Txn140327(ci, ri, db, ref lg);
                            break;
                        case 140328:
                            res = Txn140328(ci, ri, db, ref lg);
                            break;
                        case 140329:
                            res = Txn140329(ci, ri, db, ref lg);
                            break;
                        case 140330:
                            res = Txn140330(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[Барьцааны төрлийн бүртгэл]
                        case 140331:
                            res = Txn140331(ci, ri, db, ref lg);
                            break;
                        case 140332:
                            res = Txn140332(ci, ri, db, ref lg);
                            break;
                        case 140333:
                            res = Txn140333(ci, ri, db, ref lg);
                            break;
                        case 140334:
                            res = Txn140334(ci, ri, db, ref lg);
                            break;
                        case 140335:
                            res = Txn140335(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[Тооцоолол хийх матриц]
                        case 140336:
                            res = Txn140336(ci, ri, db, ref lg);
                            break;
                        case 140337:
                            res = Txn140337(ci, ri, db, ref lg);
                            break;
                        case 140338:
                            res = Txn140338(ci, ri, db, ref lg);
                            break;
                        case 140339:
                            res = Txn140339(ci, ri, db, ref lg);
                            break;
                        case 140340:
                            res = Txn140340(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА]
                        case 140341:
                            res = Txn140341(ci, ri, db, ref lg);
                            break;
                        case 140342:
                            res = Txn140342(ci, ri, db, ref lg);
                            break;
                        case 140343:
                            res = Txn140343(ci, ri, db, ref lg);
                            break;
                        case 140344:
                            res = Txn140344(ci, ri, db, ref lg);
                            break;
                        case 140345:
                            res = Txn140345(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[НӨХЦӨЛИЙН ХҮСНЭГТ]
                        case 140346:
                            res = Txn140346(ci, ri, db, ref lg);
                            break;
                        case 140347:
                            res = Txn140347(ci, ri, db, ref lg);
                            break;
                        case 140348:
                            res = Txn140348(ci, ri, db, ref lg);
                            break;
                        case 140349:
                            res = Txn140349(ci, ri, db, ref lg);
                            break;
                        case 140350:
                            res = Txn140350(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[XLS Тайлнгийн бүртгэл]
                        case 140351://xls тайлангийн параметрийн жагсаалт мэдээлэл авах
                            res = Txn140351(ci, ri, db, ref lg);
                            break;
                        case 140352://xls тайлангийн параметрийн дэлгэрэнгүй мэдээлэл авах
                            res = Txn140352(ci, ri, db, ref lg);
                            break;
                        case 140353://xls тайлангийн параметр нэмэх
                            res = Txn140353(ci, ri, db, ref lg);
                            break;
                        case 140354://xls тайлангийн параметр засварлах
                            res = Txn140354(ci, ri, db, ref lg);
                            break;
                        case 140355://xls тайлангийн параметр устгах
                            res = Txn140355(ci, ri, db, ref lg);
                            break;
                        #endregion[]

                        #region[TagMain Таг-ын бүртгэл]
                        case 140356:
                                res = Txn140356(ci, ri, db, ref lg);
                                break;
                        case 140357:
                                res = Txn140357(ci, ri, db, ref lg);
                                break;
                        case 140358:
                                res = Txn140358(ci, ri, db, ref lg);
                                break;
                        case 140359:
                                res = Txn140359(ci, ri, db, ref lg);
                                break;
                        case 140360:
                                res = Txn140360(ci, ri, db, ref lg);
                                break;
                        #endregion[]

                        #region[Бүлэглэлийн Тухайн мөчрийн бүртгэл ]
                        case 140361:
                                res = Txn140361(ci, ri, db, ref lg);
                                break;
                        case 140362:
                                res = Txn140362(ci, ri, db, ref lg);
                                break;
                        case 140363:
                                res = Txn140363(ci, ri, db, ref lg);
                                break;
                        case 140364:
                                res = Txn140364(ci, ri, db, ref lg);
                                break;
                        case 140365:
                                res = Txn140365(ci, ri, db, ref lg);
                                break;
                        #endregion[]

                        #region [ POS дээрх төлбөрийн хэрэгсэлийн бүртгэл ]
                        case 111113:                //
                                res = Txn111113(ci, ri, db, ref lg);
                                break;
                        case 111114:                //
                                res = Txn111114(ci, ri, db, ref lg);
                                break;
                        case 111115:                //
                                res = Txn111115(ci, ri, db, ref lg);
                                break;
                        case 111116:                //
                                res = Txn111116(ci, ri, db, ref lg);
                                break;
                        case 111117:                //
                                res = Txn111117(ci, ri, db, ref lg);
                                break;                                        

                        #endregion

                        //Хэл OK  
                        #region[Age]

                        case 140371: //Насны бүртгэлийн жагсаалт мэдээлэл авах
                                res = SelectAge(ci, ri, db, ref lg);
                                break;
                        case 140373: //Насны бүртгэлийн жагсаалт нэмэх
                                res = AddAge(ci, ri, db, ref lg);
                                break;
                        case 140375: //Насны бүртгэлийн жагсаалт устгах
                                res = DeleteAge(ci, ri, db, ref lg);
                                break;
                        case 140374: //Насны бүртгэлийн жагсаалт засварлах
                                res = EditAge(ci, ri, db, ref lg);
                                break;

                        #endregion

                        //Үнийн төрөл OK  
                        #region[Үнийн төрөл]
                        
                        case 140416: //Үнийн төрөл бүртгэлийн жагсаалт мэдээлэл авах
                                res = Txn140416(ci, ri, db, ref lg);
                                break;
                        case 140417: //Үнийн төрөл бүртгэлийн дэлгэрэнгүй мэдээлэл авах
                                res = Txn140417(ci, ri, db, ref lg);
                                break;
                        case 140418: //Үнийн төрөл бүртгэлийн жагсаалт нэмэх
                                res = Txn140418(ci, ri, db, ref lg);
                                break;
                        case 140419: //Үнийн төрөл бүртгэлийн жагсаалт засварлах
                                res = Txn140419(ci, ri, db, ref lg);
                                break;
                        case 140420: //Үнийн төрөл бүртгэлийн жагсаалт устгах
                                res = Txn140420(ci, ri, db, ref lg);
                                break;

                        #endregion

                        default:
                        res.ResultNo = 9110009;
                        res.ResultDesc = "Функц тодорхойлогдоогүй байна";
                        break;
                }
                return res;
                //if (hashtable.ContainsKey(ri.FunctionNo))
                //{
                //    tablename = Static.ToStr(hashtable[ri.FunctionNo]);
                //    long seqno = Static.ToLong(EServ.Interface.Sequence.NextByVal("version"));
                //    //res = IPos.DB.Main.DB230001(db, tablename, seqno);
                //}
            }
            #region[catch]
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                string ResultDesk = "OK";
                if (res.ResultNo != 0)
                {
                    ResultDesk = res.ResultDesc;
                }
                DateTime second = DateTime.Now;
                ISM.Lib.Static.WriteToLogFile("IPos.Parameter", "\r\n<<Start:" + Static.ToStr(first) + ">>\r\n UserNo:" + Static.ToStr(ri.UserNo) + "\r\n Description:" + Static.ToStr(lg.item.Desc) + "\r\n ResultNo:" + Static.ToStr(res.ResultNo) + "\r\n ResultDescription:" + ResultDesk + "\r\n<<End " + Static.ToStr(second) + ">>");
            }
            #endregion[]
        }
       
        //Мэдээлэл log
        #region[Dictionary function]
        //мэдээллийн жагсаалт авах
        public Result DB202301(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202301(db);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Dictionary бүртгэлийн жагсаалт авах";
            }
        }
        // мэдээллийн дэлгэрэнгүй мэдээлэл авах
        public Result DB202302(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pID = Static.ToStr(ri.ReceivedParam[0]);  //pID
                res = IPos.DB.Main.DB202302(db, pID);
                if (res.Data != null)
                {
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 9110014;
                        res.ResultDesc = "Бичлэг олдсонгүй";
                    }
                }
                else
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Мэдээллийн бааз руу хандахад алдаа гарлаа";
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Dictionary бүртгэлийн дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("Dictionary", "ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //  мэдээлэл нэмэх
        public Result DB202303(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202303(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарсан" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Dictionary бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Dictionary", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        // мэдээлэл засварлах
        public Result DB202304(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202304(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Dictionary бүртгэл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Dictionary", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //мэдээлэл устгах
        public Result DB202305(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pID = Static.ToStr(ri.ReceivedParam[0]);  //pID
                res = IPos.DB.Main.DB202305(db, pID);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Dictionary бүртгэлийн жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Дугаар";
                    lg.AddDetail("Dictionary", "ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion

        //Dynamic Жагсаалт log
        #region[DynamicList function]
        //жагсаалт авах
        public Result DB202306(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202306(db);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Динамик жагсаалт бүртгэлийн жагсаалт авах";
            }
        }
        // жагсаалтын дэлгэрэнгүй мэдээлэл авах
        public Result DB202307(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {

                res = IPos.DB.Main.DB202307(db, Static.ToStr(ri.ReceivedParam[0]));
                if (res.Data != null)
                {
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 9110014;
                        res.ResultDesc = "Бичлэг олдсонгүй";
                    }
                }
                else
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Мэдээллийн бааз руу хандахад алдаа гарлаа";
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Динамик жагсаалтын дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("DynamicList", "ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        // Жагсаалт нэмэх
        public Result DB202308(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                //object[] value = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB202308(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Динамик жагсаалт бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("DynamicList", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        // Жагсаалт мэдээлэл засварлах
        public Result DB202309(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {                

                res = IPos.DB.Main.DB202309(db, (object[])ri.ReceivedParam[0], (object[])ri.ReceivedParam[1]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа " + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Динамик жагсаалтын бүртгэл засварлах ";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("DynamicList", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        //мэдээлэл устгах
        public Result DB202310(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202310(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа " + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Динамик жагсаалтын бүртгэлийн жагсаалт устгах ";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Дугаар";
                    lg.AddDetail("DynamicList", "ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
     
        // Үндсэн хөрөнгө бараа материалын байршил бүртгэл log
        #region [ Үндсэн хөрөнгө бараа материалын байршил]
        //Үндсэн хөрөнгийн байршилын бүртгэл жагсаалт
        public Result Txn140241(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202241(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үндсэн хөрөнгийн байршлын жагсаалт авах";
            }
        }
        // Үндсэн хөрөнгийн байршил нэмэх
        public Result Txn140243(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202243(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үндсэн хөрөнгийн байршил нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("FaPosition", FieldName[i].ToString(), "Үндсэн хөрөнгийн байршил нэмэх", NewValue[i].ToString());
                    }
                }
            }
        }
        // Үндсэн хөрөнгийн байршил засварлах
        public Result Txn140244(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202244(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үндсэн хөрөнгийн  байршлыг засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("FaPosition", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        // Үндсэн хөрөнгийн байршил устгах
        public Result Txn140245(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202245(db, Convert.ToInt32(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үндсэн хөрөнгийн байршил устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Байрлалын дугаар";
                    lg.AddDetail("FaPosition", "TypeCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion    

        //Салбар OK log
        #region[Branch]

        public Result SelectBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202001(db, 0, 0, null);
                //
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Салбарын жагсаалт авах";
            }
        }
        public Result AddBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202003(db, (object[])ri.ReceivedParam[0]);
                //
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Салбарын жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Branch", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202005(db, Static.ToLong(ri.ReceivedParam[0]));
                //
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Салбарын жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Салбарын дугаар";
                    lg.AddDetail("Branch", "branch", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202004(db, (object[])ri.ReceivedParam[0]);
                //
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Салбарын жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Branch", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Улс OK log
        #region[Country]

        public Result SelectCountry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202006(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Улсын жагсаалт авах";
            }
        }
        public Result AddCountry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202008(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Улсын жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Country", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteCountry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202010(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Улсын жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Улсын код";
                    lg.AddDetail("Country", "CountryCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditCountry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202009(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Улсын жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Country", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Хэл OK log
        #region[Language]

        public Result SelectLanguage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202011(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хэлний жагсаалт авах";
            }
        }
        public Result AddLanguage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202013(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэлний жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Language", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteLanguage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202015(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хэлний жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Хэлний код";
                    lg.AddDetail("Language", "LanguageCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditLanguage(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202014(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хэлний жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Language", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Валют OK log
        #region[Currency]

        public Result SelectCurrency(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202016(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Валютын жагсаалт авах";
            }
        }
        public Result AddCurrency(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202018(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                return res;
            }
            finally
            {
                lg.item.Desc = "Валютын жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Currency", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteCurrency(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202020(db, (ri.ReceivedParam[0]).ToString());
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Валютын жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Валютын дугаар";
                    lg.AddDetail("Currency", "Currency", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditCurrency(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202019(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Валютын жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (Static.ToStr(OldValue[i]) != Static.ToStr(NewValue[i])) lg.AddDetail("Currency", Static.ToStr(FieldName[i]), Static.ToStr(OldValue[i]), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result ImportFile(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202199(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Валютын ханшийн түүх оруулах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = { "CURRENCY", "CURDATE", "RATE", "CASHBUYRATE", "CASHSELLRATE", "NONCASHBUYRATE", "NONCASHSELLRATE", "OLDRATE" };
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CurrencyHist", FieldName[i].ToString(), lg.item.Desc, Convert.ToString(NewValue[i]));
                    }
                }
            }
        }
        #endregion

        //Банк OK log
        #region[Bank]

        public Result SelectBank(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202021(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны жагсаалт авах";
            }
        }
        public Result AddBank(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202023(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Bank", Static.ToStr(FieldName[i]), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteBank(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202025(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Банкны дугаар";
                    lg.AddDetail("Bank", "bankid", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditBank(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202024(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Bank", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Харилцагчийн төрөл OK log
        #region[CustomerType]

        public Result SelectCustomerType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202036(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн Төрлийн жагсаалт авах";
            }
        }
        public Result AddCustomerType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202038(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн Төрлийн жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CustomerType", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteCustomerType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202040(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн Төрлийн жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.AddDetail("CustomerType", "TypeCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditCustomerType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202039(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн Төрлийн жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Автомат дансны бүртгэл OK log
        #region[AutoNum]

        public Result SelectAutoNum(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202031(db);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Автомат дансны бүртгэл жагсаалт авах";
            }
        }
        public Result AddAutoNum(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202033(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Автомат дансны бүртгэл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Autonum", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        public Result DeleteAutoNum(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202035(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Автомат дансны бүртгэл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Салбарын дугаар";
                    lg.AddDetail("Autonum", "ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditAutoNum(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202034(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Автомат дансны бүртгэл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("AutoNum", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Гэр бүлийн төрөл OK log
        #region[FamilyType]

        public Result SelectFamilyType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202041(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гэр бүлийн төрөл жагсаалт авах";
            }
        }
        public Result AddFamilyType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202043(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гэр бүлийн төрөл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("FamilyType", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteFamilyType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202045(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гэр бүлийн төрөл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.AddDetail("FamilyType", "TypeCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditFamilyType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202044(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гэр бүлийн төрөл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("FamilyType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Үйл ажиллагааны чиглэл OK log
        #region[Industry]

        public Result SelectIndustry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202046(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйл ажиллагааны чиглэл жагсаалт авах";
            }
        }
        public Result AddIndustry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202048(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйл ажиллагааны чиглэл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("FaPosition", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteIndustry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202050(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйл ажиллагааны чиглэл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.AddDetail("Industry", "TypeCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditIndustry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202049(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйл ажиллагааны чиглэл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Industry", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Үйл ажиллагааны дэд чиглэл OK
        #region[SubIndustry]

        public Result SelectSubIndustry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202051(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйл ажиллагааны дэд чиглэл жагсаалт авах";
            }
        }
        public Result AddSubIndustry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202053(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйл ажиллагааны дэд чиглэл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("SubIndustry", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteSubIndustry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202055(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйл ажиллагааны дэд чиглэл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.item.Key2 = "Дэд төрлийн дугаар";
                    lg.AddDetail("SubIndustry", "TypeCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("SubIndustry", "SubTypeCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditSubIndustry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202054(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйл ажиллагааны дэд чиглэл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("SubIndustry", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Аймаг хотын бүртгэл OK log
        #region[CustCity]

        public Result SelectCustCity(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202056(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Аймаг хотын бүртгэл жагсаалт авах";
            }
        }
        public Result AddCustCity(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202058(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Аймаг хотын бүртгэл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CustCity", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteCustCity(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202060(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Аймаг хотын бүртгэл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Аймаг хотын код";
                    lg.AddDetail("CustCity", "CityCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditCustCity(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202059(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Аймаг хотын бүртгэл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustCity", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        #endregion

        //Харилцагчийн ратинг OK log
        #region[CustRate]

        public Result SelectCustRate(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202071(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн ратинг жагсаалт авах";
            }
        }
        public Result AddCustRate(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202073(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн ратинг жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CustRate", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteCustRate(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202075(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн ратинг жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Харилцагчийн зэрэглэлийн код";
                    lg.AddDetail("CustRate", "RateCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditCustRate(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202074(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн ратинг жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustRate", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Сум дүүрэгийн бүртгэл OK log
        #region[CustDistrict]

        public Result SelectCustDistrict(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202061(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Сум дүүрэгийн бүртгэл жагсаалт авах";
            }
        }
        public Result AddCustDistrict(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202063(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Сум дүүрэгийн бүртгэл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CustDistrict", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteCustDistrict(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202065(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Сум дүүрэгийн бүртгэл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Сум дүүрэгийн код";
                    lg.item.Key2 = "Аймаг хотын код";
                    lg.AddDetail("CustDistict", "CityCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("CustDistrict", "DistCode", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        public Result EditCustDistrict(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202064(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Сум дүүрэгийн бүртгэл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustDistrict", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion                             
       
        //Бараа материалын төрөл OK log
        #region[InventoryType]

        public Result SelectInventoryType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202101(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын төрөл жагсаалт авах";
            }
        }
        public Result AddInventoryType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202103(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын төрөл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("InventoryType", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteInventoryType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202105(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материал нэгжийн төрөл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.AddDetail("InventoryType", "InvTypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditInventoryType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202104(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын төрөл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("InventoryType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Байгуулгын дансны бүтээгдэхүүн OK log
        #region[BacProduct]

        public Result SelectBacProduct(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202111(db, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Байгуулгын дансны бүтээгдэхүүн жагсаалт авах";
            }
        }
        public Result AddBacProduct(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202113(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Байгуулгын дансны бүтээгдэхүүн жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("BacProduct", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteBacProduct(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202115(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Байгуулгын дансны бүтээгдэхүүн жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Бүтээгдэхүүний дугаар";
                    lg.AddDetail("BacProduct", "ProdCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditBacProduct(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202114(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Байгуулгын дансны бүтээгдэхүүн жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("BacProduct", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        #endregion

        //Балансын гадуурх дансны бүтээгдэхүүн OK log
        #region[ConProduct]

        public Result SelectConProduct(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202116(db, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Балансын гадуурх дансны бүтээгдэхүүн жагсаалт авах";
            }
        }
        public Result AddConProduct(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202118(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Балансын гадуурх дансны бүтээгдэхүүн жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("ConProduct", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteConProduct(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202120(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Балансын гадуурх дансны бүтээгдэхүүн жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Бүтээгдэхүүний дугаар";
                    lg.AddDetail("ConProduct", "ProdCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditConProduct(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202119(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Балансын гадуурх дансны бүтээгдэхүүн жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("ConProduct", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Үндсэн хөрөнгийн материал төрөл OK log
        #region[FAType]

        public Result SelectFAType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202136(db, 0, 0, null);
                //
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үндсэн хөрөнгийн материал төрөл жагсаалт авах";
            }
        }
        public Result AddFAType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202138(db, (object[])ri.ReceivedParam[0]);
                //
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үндсэн хөрөнгийн материал төрөл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("FaType", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteFAType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202140(db, Static.ToLong(ri.ReceivedParam[0]));
                //
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үндсэн хөрөнгийн материал төрөл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.AddDetail("FaType", "TypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditFAType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202139(db, (object[])ri.ReceivedParam[0]);
                //
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үндсэн хөрөнгийн материал төрөл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("FAType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Харилцагчийн маск OK log
        #region[CustomerMask]

        public Result SelectCustomerMask(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202126(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн маск жагсаалт авах";
            }
        }
        public Result AddCustomerMask(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202128(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн маск жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CustomerMask", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        public Result DeleteCustomerMask(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202130(db, Static.ToInt(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн маск жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Маскны дугаар";
                    lg.AddDetail("CustomerMask", "MaskID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditCustomerMask(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202129(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Харилцагчийн маск жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustomerMask", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Балансын данс төрөл OK
        #region[ChartGroup]
        public Result SelectChartGroup(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202121(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Балансын данс төрөл жагсаалт авах";
            }
        }
        public Result AddChartGroup(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202123(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Балансын данс төрөл жагсаалт нэмэх";
            }
        }
        public Result DeleteChartGroup(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202125(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Балансын данс төрөл жагсаалт устгах";
                lg.item.Key1 = "Төрлийн дугаар";
            }
        }
        public Result EditChartGroup(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202124(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Балансын данс төрөл засварлах";
                object[] NewValue = (object[])ri.ReceivedParam[0];
                object[] OldValue = (object[])ri.ReceivedParam[1];
                object[] FieldName = (object[])ri.ReceivedParam[2];
                for (int i = 0; i < FieldName.Length; i++)
                {
                    if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("ChartGroup", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                }
            }
        }
        #endregion

        //Банкны салбар log
        #region[BankBranch]

        public Result SelectBankBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202096(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны салбар жагсаалт авах";
            }
        }
        public Result AddBankBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202098(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны салбар жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Bankbranch", Static.ToStr(FieldName[i]), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteBankBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202100(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны салбар жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Банкны дугаар";
                    lg.item.Key2 = "Салбарын дугаар";
                    lg.AddDetail("Bankbranch", "Bankid", lg.item.Desc, Static.ToStr(ri.ReceivedParam[0]));
                    lg.AddDetail("Bankbranch", "branchid", lg.item.Desc, Static.ToStr(ri.ReceivedParam[0]));
                }
            }
        }
        public Result EditBankBranch(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] OldValueEdit = (object[])ri.ReceivedParam[1];
                object[] Value = new object[6];
                for (int i = 0; i < 5; i++)
                    Value[i] = ((object[])ri.ReceivedParam[0])[i];
                Value[5] = OldValueEdit[1];
                res = IPos.DB.Main.DB202099(db, Value);
                if (res.ResultNo == 0)
                {

                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны салбар засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (Static.ToStr(OldValue[i]) != Static.ToStr(NewValue[i])) lg.AddDetail("BankBranch", Static.ToStr(FieldName[i]), Static.ToStr(OldValue[i]), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }

        #endregion

        //Баг хорооны бүртгэл log
        #region[CustSubDistrict]

        public Result SelectCustSubDistrict(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202066(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Баг хорооны бүртгэл жагсаалт авах";
            }
        }
        public Result AddCustSubDistrict(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202068(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Баг хорооны бүртгэл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("CustSubDistrict", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteCustSubDistrict(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202070(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]), Static.ToLong(ri.ReceivedParam[2]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Баг хорооны бүртгэл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Баг хорооны код";
                    lg.item.Key2 = "Сум дүүрэгийн код";
                    lg.item.Key3 = "Аймаг хотын код";
                    lg.AddDetail("CustSubDistrict", "SubDistCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("CustSubDistrict", "DistCode", lg.item.Desc, ri.ReceivedParam[1].ToString());
                    lg.AddDetail("CustSubDistrict", "CustCode", lg.item.Desc, ri.ReceivedParam[2].ToString());
                }
            }
        }
        public Result EditCustSubDistrict(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202069(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Баг хорооны бүртгэл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("CustSubDistrict", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Ерөнхий Параметр
        #region[GenParam]

        public Result SelectGenParam(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202146(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа " + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Ерөнхий Параметр жагсаалт авах";
            }
        }
        //public Result AddGenParam(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        //{
        //    Result res = new Result();
        //    try
        //    {
        //        res = IPos.DB.Main.DB202148(db, ri.ReceivedParam);

        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ResultNo = 9110002;
        //        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

        //        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

        //        return res;
        //    }
        //    finally
        //    {
        //        if (res.ResultNo == 0)
        //        {
        //            lg.item.Desc = "Ерөнхий Параметр жагсаалт нэмэх";
        //            //object[] NewValue = (object[])ri.ReceivedParam[0];
        //            //object[] FieldName = (object[])ri.ReceivedParam[1];
        //            //for (int i = 0; i < FieldName.Length; i++)
        //            //{
        //            //    lg.AddDetail("GenParam", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
        //            //}
        //        }
        //    }
        //}
        //public Result DeleteGenParam(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        //{
        //    Result res = new Result();
        //    try
        //    {
        //        res = IPos.DB.Main.DB202150(db, Static.ToLong(ri.ReceivedParam[0]));

        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ResultNo = 9110002;
        //        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
        //        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
        //        return res;
        //    }
        //    finally
        //    {
        //        if (res.ResultNo == 0)
        //        {
        //            lg.item.Desc = "Ерөнхий Параметр жагсаалт устгах";
        //            lg.item.Key1 = "Дугаар";
        //            //lg.AddDetail("GenParam", "ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
        //        }
        //    }
        //}
        public Result EditGenParam(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value;
                int index;
                DataTable DT = (DataTable)ri.ReceivedParam[0];
                foreach (DataRow row in DT.Rows)
                {
                    value = null;
                    value = new object[DT.Columns.Count];
                    index = 0;
                    foreach (DataColumn col in DT.Columns)
                    {
                        value[index] = row[col];
                        index++;
                    }
                    res = IPos.DB.Main.DB202149(db, value);
                }

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа " + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Ерөнхий Параметр засварлах";
                if (res.ResultNo == 0)
                {
                    //    object[] NewValue;
                    //    int index;
                    //    DataTable DT = (DataTable)ri.ReceivedParam[0];
                    //    foreach (DataRow row in DT.Rows)
                    //    {
                    //        NewValue = null;
                    //        NewValue = new object[DT.Columns.Count];
                    //        index = 0;
                    //        foreach (DataColumn col in DT.Columns)
                    //        {
                    //            NewValue[index] = row[col];
                    //            index++;
                    //        }
                    //    }
                    //    object[] OldValue = (object[])ri.ReceivedParam[1];
                    //    object[] FieldName = (object[])ri.ReceivedParam[2];
                    //    for (int i = 0; i < FieldName.Length; i++)
                    //    {
                    //        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("GeneralParam", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    //    }
                }
            }
        }

        #endregion

        //Гүйлгээний код log
        #region[Txn]

        public Result SelectTxn(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202161(db, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний код төрөл жагсаалт авах";
            }
        }
        public Result AddTxn(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202163(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний код төрөл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Txn", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteTxn(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202165(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний код төрөл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Гүйлгээний код";
                    lg.AddDetail("Txn", "TxnCode", lg.item.Desc, Static.ToStr(ri.ReceivedParam[0]));
                }
            }
        }
        public Result EditTxn(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202164(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний код төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Txn", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Гүйлгээний оролт log
        #region[Entry]

        public Result SelectTxnEntry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202166(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний оролт төрөл жагсаалт авах";
            }
        }
        public Result AddTxnEntry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202168(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний оролт төрөл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("TxnEntry", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteTxnEntry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202170(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний оролт төрөл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Гүйлгээний код";
                    lg.item.Key2 = "Гүйлгээний оролт";
                    lg.AddDetail("TxnEntry", "EntryCOde", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditTxnEntry(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202169(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний оролт төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("TxnEntry", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Гүйлгээний санхүүгийн бичилт log
        #region[TxnWrite]

        public Result SelectTxnWrite(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202176(db, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний оролтуудын холбоос жагсаалт мэдээлэл авах";
            }
        }
        public Result AddTxnWrite(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202178(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний оролтуудыг холбоос нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("TxnWrite", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteTxnWrite(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202180(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний оролтуудыг холбоос устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Гүйлгээний код";
                    lg.item.Key2 = "Санхүүгийн бичилтийн код";
                    lg.AddDetail("TxnWrite", "TranCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("TxnWrite", "EntryCode", lg.item.Desc, ri.ReceivedParam[1].ToString());
                }
            }
        }
        public Result EditTxnWrite(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] OldNew = (object[])ri.ReceivedParam[3];
                res = IPos.DB.Main.DB202179(db, Static.ToInt(OldNew[0]), Static.ToInt(OldNew[1]), (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Гүйлгээний оролтуудыг холбоос засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("TxnWrite", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Түр дансны бүртгэл log
        #region[AccountCode]

        public Result SelectAccountCode(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202026(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Түр дансны бүртгэл төрөл жагсаалт авах";
            }
        }
        public Result AddAccountCode(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202028(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Түр дансны бүртгэл төрөл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("AccountCode", FieldName[i].ToString(), "Түр дансны бүртгэл төрөл жагсаалт нэмэх", NewValue[i].ToString());
                    }
                }
            }
        }
        public Result DeleteAccountCode(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202030(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Түр дансны бүртгэл төрөл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Дугаар";
                    lg.AddDetail("AccountCode", "Code", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditAccountCode(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202029(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Түр дансны бүртгэл төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("AccountCode", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }

        #endregion

        //Дамжлага log!
        #region[StepItem]
        public Result SelectStepItem(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202276(db);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүртгэл жагсаалт авах";
            }
        }
        public Result AddStepItem(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202278(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүртгэл жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("StepItem", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteStepItem(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202280(db, Static.ToInt(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүртгэл жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Дамжлагын дугаар";
                    lg.AddDetail("StepItem", "StepID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditStepItem(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202279(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүртгэл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("StepItem", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        #endregion

        #region[Step]
        public Result SelectStep(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202281(db);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүлэг жагсаалт авах";
            }
        }
        public Result AddStep(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202283(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүлэг жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = { "STEPID", "NAME", "NAME2", "NOTE", "NOTE2" };
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Step", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteStep(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202285(db, Static.ToInt(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүлэг жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Бүлэгийн дугаар";
                    lg.AddDetail("Step", "StepID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditStep(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202284(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүлэг жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = { "STEPID", "NAME", "NAME2", "NOTE", "NOTE2" };
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Step", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result SelectStepI(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202222(db, Static.ToInt(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын жагсаалт авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("StepItem", "StepID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result SaveStepI(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202223(db, Static.ToInt(ri.ReceivedParam[0]), (System.Data.DataTable)ri.ReceivedParam[1]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Дамжлагын жагсаалт засварлах";
                    //object[] NewValue = (object[])ri.ReceivedParam[0];
                    //object[] OldValue = (object[])ri.ReceivedParam[1];
                    //object[] FieldName = (object[])ri.ReceivedParam[2];
                    //for (int i = 0; i < FieldName.Length; i++)
                    //{
                    //    if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Step", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                    //}
                }
            }
        }
        #endregion
        //Shortcut log
        #region[Shortcut]
        //Shortcut жагсаалт авах
        public Result DB202321(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202321(db);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Дамжлагын бүртгэл жагсаалт авах";
            }
        }
        public Result DB202322(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202322(db, pID);
                if (res.Data != null)
                {
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 9110014;
                        res.ResultDesc = "Бичлэг олдсонгүй";
                    }
                }
                else
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Мэдээллийн бааз руу хандахад алдаа гарлаа";
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Shortcut дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("Shorcut", "ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //ShortCut бүртгэл нэмэх
        public Result DB202323(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202323(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "ShortCut бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Shorcut", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        //ShortCut бүртгэл устгах
        public Result DB202325(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202325(db, pID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "ShortCut бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "ShortCut дугаар";
                    lg.AddDetail("Shortcut", "ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //ShortCut бүртгэл засварлах
        public Result DB202324(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202324(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Shortcut бүртгэл жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Shorcut", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        #endregion
        #region[DocTemplate]
        public Result DB201001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201001(db, 0, 1000, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загварын бүртгэл жагсаалт авах";
            }
        }
        //DB201005
        public Result DB201006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201006(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загвар нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("DocTemplate", Static.ToStr(FieldName[i]), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DB201007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201007(db, (object[])ri.ReceivedParam[0]);
                if (res.ResultNo == 0)
                {

                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Банкны салбар засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (Static.ToStr(OldValue[i]) != Static.ToStr(NewValue[i])) lg.AddDetail("BankBranch", Static.ToStr(FieldName[i]), Static.ToStr(OldValue[i]), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DB201008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201008(db, Static.ToLong(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загварын жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Документ загварын дугаар";
                    lg.AddDetail("DocTemplate", "ID", lg.item.Desc, Static.ToStr(ri.ReceivedParam[0]));
                }
            }
        }


        public Result DB201003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201003(db, Static.ToLong(ri.ReceivedParam[0]));
                if (res.Data != null)
                {
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 9110014;
                        res.ResultDesc = "Бичлэг олдсонгүй";
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загвар SQL жагсаалт авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("DocSql", "TemplateID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result DB201013(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201013(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загвар Sql нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("DocSql", Static.ToStr(FieldName[i]), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DB201014(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201014(db, Static.ToInt(ri.ReceivedParam[3]), (object[])ri.ReceivedParam[0]);
                if (res.ResultNo == 0)
                {

                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загвар SQL засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (Static.ToStr(OldValue[i]) != Static.ToStr(NewValue[i])) lg.AddDetail("DOCSQL", Static.ToStr(FieldName[i]), Static.ToStr(OldValue[i]), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DB201015(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201015(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загварын SQL устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Документ загварын дугаар";
                    lg.item.Key1 = "Документ загварын Sql дугаар";
                    lg.AddDetail("DocTemplate", "TemplateID", lg.item.Desc, Static.ToStr(ri.ReceivedParam[0]));
                    lg.AddDetail("DocTemplate", "ItemNo", lg.item.Desc, Static.ToStr(ri.ReceivedParam[1]));
                }
            }
        }


        public Result DB201002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201002(db, Static.ToLong(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загварын параметрийн жагсаалт авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("DocParam", "TemplateID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result DB201017(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201017(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загвар параметр нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("DocParam", Static.ToStr(FieldName[i]), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DB201018(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201018(db, Static.ToInt(ri.ReceivedParam[3]), (object[])ri.ReceivedParam[0]);
                if (res.ResultNo == 0)
                {

                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загвар параметр засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (Static.ToStr(OldValue[i]) != Static.ToStr(NewValue[i])) lg.AddDetail("DocParam", Static.ToStr(FieldName[i]), Static.ToStr(OldValue[i]), Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DB201019(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201019(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загварын жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Документ загварын дугаар";
                    lg.item.Key1 = "Документ загварын параметрийн дугаар";
                    lg.AddDetail("DocParam", "ID", lg.item.Desc, Static.ToStr(ri.ReceivedParam[0]));
                    lg.AddDetail("DocParam", "ParamID", lg.item.Desc, Static.ToStr(ri.ReceivedParam[1]));
                }
            }
        }

        public Result DB201020(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(ri.ReceivedParam[1]) == 1)
                {
                    res = IPos.DB.Main.DB201003(db, Static.ToLong(ri.ReceivedParam[0]));
                    return res;
                }
                else
                {
                    res = IPos.DB.Main.DB201020(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[2]));
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "DOCSQL дээрх холбоотой болон холбоогүй DOCPARAM-ийн мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("DocParam", "TemplateID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                    lg.AddDetail("DocParam", "ItemNo", lg.item.Desc, ri.ReceivedParam[1].ToString());
                    lg.AddDetail("DocSql", "TemplateID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result DB201021(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB201021(db, Static.ToLong(ri.ReceivedParam[0]), Static.ToInt(ri.ReceivedParam[1]), Static.ToStr(ri.ReceivedParam[2]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Документ загвар DOCSQL дээр DOCPARAM-ийн ID-нүүдийг хадгалах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("DocParam", "DOCID", lg.item.Desc, Static.ToStr(ri.ReceivedParam[0]));
                    lg.AddDetail("DocParam", "ITEMNO", lg.item.Desc, Static.ToStr(ri.ReceivedParam[1]));
                    lg.AddDetail("DocParam", "PARAMID", lg.item.Desc, Static.ToStr(ri.ReceivedParam[2]));
                }
            }
        }
        #endregion        
        //Документын бүлгийн бүргэл
        
        #region[CRMProjectType]
        //CRM Төслийн төрөл жагсаалт авах
        public Result DB240000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB240000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Төслийн төрөл жагсаалт";
            }
        }
        //DB240001   - дэлгэрэнгүй мэдээлэл авах

        // CRM Төслийн төрөл нэмэх
        public Result DB240002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB240002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Төслийн төрөл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("ProjectType", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Төслийн төрөл засварлах
        public Result DB240003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB240003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Төслийн төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("ProjectType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM Төслийн төрөл устгах
        public Result DB240004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB240004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Төслийн төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төслийн дугаар";
                    lg.AddDetail("ProjectType", "ProjectTypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMIssueType]
        //CRM Асуудлын төрөл жагсаалт авах
        public Result DB241000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB241000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын төрөл жагсаалт";
            }
        }
        //DB241001    - дэлгэрэнгүй мэдээлэл авах

        // CRM Төслийн төрөл нэмэх
        public Result DB241002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB241002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын төрөл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssueType", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Төслийн төрөл засварлах
        public Result DB241003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB241003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssueType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM Төслийн төрөл устгах
        public Result DB241004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB241004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.AddDetail("IssueType", "IssueTypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //CRM Алхмуудын шатлал жагсаалт авах
        public Result DB243006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB243006(db,Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Алхмуудын шатлал жагсаалт авах";
            }
        }
        //CRM Алхмуудын шатлал хадгалах
        public Result DB243005(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                int typeid = Static.ToInt(ri.ReceivedParam[0]);
                res = IPos.DB.CRMDB.DB243005(db, typeid, (DataTable)ri.ReceivedParam[1]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Алхмуудын шатлал хадгалах";
                if (res.ResultNo == 0)
                {
                        lg.AddDetail("IssueTypeTracks", "IssueTypeID", lg.item.Desc, Static.ToStr(ri.ReceivedParam[0]));
                }
            }
        }
        #endregion
        #region[CRMIssueTracks]
        //CRM Асуудлын алхамууд жагсаалт авах
        public Result DB242000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB242000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын алхамууд жагсаалт";
            }
        }
        // CRM Асуудлын алхамууд шинээр нэмэх
        public Result DB242002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB242002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын алхамууд нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssueTracks", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Асуудлын алхамууд засварлах
        public Result DB242003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB242003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын алхамууд засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssueTracks", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM Асуудлын алхам устгах
        public Result DB242004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB242004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.AddDetail("IssueTracks", "IssueTrackID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMIssueActionType]
        //CRM Асуудлын үйлдлийн төрлийн жагсаалт авах
        public Result DB244000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB244000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын үйлдлийн төрлийн жагсаалт";
            }
        }
        // CRM Асуудлын алхамууд шинээр нэмэх
        public Result DB244002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB244002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын үйлдлийн төрөл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssueActionType", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Асуудлын алхамууд засварлах
        public Result DB244003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB244003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын үйлдлийн төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssueActionType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM Асуудлын алхам устгах
        public Result DB244004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB244004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын үйлдлийн төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төрлийн дугаар";
                    lg.AddDetail("IssueActionType", "ActionTypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMIssueResolutionType]
        //CRM Асуудлын хаагдсан төрлийн жагсаалт авах
        public Result DB245000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB245000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын хаагдсан төрлийн жагсаалт";
            }
        }
        // CRM Асуудлын хаагдсан төрөл шинээр нэмэх
        public Result DB245002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB245002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын хаагдсан төрөл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssueResolutionType", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Асуудлын хаагдсан төрөл засварлах
        public Result DB245003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB245003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын хаагдсан төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssueResolutionType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM Асуудлын хаагдсан төрөл устгах
        public Result DB245004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB245004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын хаагдсан төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Хаагдсан төрүлийн дугаар";
                    lg.AddDetail("IssueResolutionType", "ResolutionTypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMIssueMemberPurp]
        //CRM Асуудлын холбоотой хүний үүргийн төрлийн жагсаалт
        public Result DB246000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB246000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын хаагдсан төрлийн жагсаалт";
            }
        }
        // CRM Асуудлын холбоотой хүний үүргийн төрөл шинээр нэмэх
        public Result DB246002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB246002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын холбоотой хүний үүргийн төрөл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssueMemberPurp", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Асуудлын холбоотой хүний үүргийн төрөл засварлах
        public Result DB246003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB246003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM холбоотой хүний үүргийн төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssueMemberPurp", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM Асуудлын хаагдсан төрөл устгах
        public Result DB246004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB246004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM холбоотой хүний үүргийн төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Үүргийн төрлийн дугаар";
                    lg.AddDetail("IssueMemberPurp", "MemberPurpID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMIssuePriority]
        //CRM Асуудлын эрэмбэ жагсаалт
        public Result DB247000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB247000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын эрэмбэ жагсаалт";
            }
        }
        // CRM Асуудлын эрэмбэ нэмэх
        public Result DB247002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB247002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын эрэмбэ нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssuePriority", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Асуудлын эрэмбэ засварлах
        public Result DB247003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB247003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM эрэмбэ засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssuePriority", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM Асуудлын хаагдсан төрөл устгах
        public Result DB247004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB247004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын эрэмбэ устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Эрэмбэ дугаар";
                    lg.AddDetail("IssuePriority", "IssuePriorID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMIssueLinkType]
        //CRM Асуудлын холбоосын төрөл жагсаалт
        public Result DB248000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB248000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын холбоосын төрөл жагсаалт";
            }
        }
        // CRM Асуудлын холбоосын төрөл нэмэх
        public Result DB248002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB248002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын холбоосын төрөл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssueLinkType", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Асуудлын эрэмбэ засварлах
        public Result DB248003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB248003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын холбоосын төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssueLinkType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM Асуудлын хаагдсан төрөл устгах
        public Result DB248004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB248004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Асуудлын холбоосын төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Холбоосын төрлийн дугаар";
                    lg.AddDetail("IssueLinkType", "LinkTypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMIssueProject]
        //CRM төслийн жагсаалт
        public Result DB254000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB254000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Төслийн жагсаалт";
            }
        }
        //CRM төслийн дэлгэрэнгүй мэдээлэл
        public Result DB254001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            long ProdCode;
            try
            {
                ProdCode = Static.ToLong(ri.ReceivedParam[0]);
                res = IPos.DB.CRMDB.DB254001(db, ProdCode);
                if (res.Data != null)
                {
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res.ResultNo = 9110014;
                        res.ResultDesc = "Бичлэг олдсонгүй";
                    }
                }
                else
                {
                    res.ResultNo = 9110002;
                    res.ResultDesc = "Мэдээллийн бааз руу хандахад алдаа гарлаа";
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM төслийн дэлгэрэнгүй мэдээлэл";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("IssueProject", "ProjectID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        // CRM төсөл нэмэх
        public Result DB254002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB254002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM төсөл нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssueProject", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM төсөл засварлах
        public Result DB254003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB254003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM төсөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssueProject", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM төсөл устгах
        public Result DB254004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB254004(db, Static.ToLong(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM төсөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төслийн дугаар";
                    lg.AddDetail("IssueProject", "ProjectID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMIssueProjectComp]
        //CRM төслийн дэд төрөл жагсаалт
        public Result DB255000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB255000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Төслийн жагсаалт";
            }
        }
        // CRM төслийн дэд төрөл нэмэх
        public Result DB255002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB255002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM төслийн дэд төрөл";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("IssueProject", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM төслийн дэд төрөл засварлах
        public Result DB255003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB255003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM төслийн дэд төрөл засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("IssueProject", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM төслийн дэд төрөл устгах
        public Result DB255004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB255004(db, Static.ToInt(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM төслийн дэд төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төслийн дугаар";
                    lg.AddDetail("IssueProject", "ProjectID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMNotifySchema]
        //CRM Мэдэгдэлийн схем жагсаалт авах
        public Result DB249000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB249000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Мэдэгдэлийн схем жагсаалт авах";
            }
        }
        //  CRM Мэдэгдэлийн схем нэмэх
        public Result DB249002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB249002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Мэдэгдэлийн схем нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("NotifySchema", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Мэдэгдэлийн схем засварлах
        public Result DB249003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB249003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Мэдэгдэлийн схем засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("NotifySchema", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM CRM Мэдэгдэлийн схем устгах
        public Result DB249004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB249004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM CRM Мэдэгдэлийн схем устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Схемийн дугаар";
                    lg.AddDetail("NotifySchema", "SchemaID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMNotifySchemaTXN]
        //CRM Мэдэгдэлийн схем гүйлгээ жагсаалт авах
        public Result DB250000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB250000(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Мэдэгдэлийн схем гүйлгээ жагсаалт авах";
            }
        }
        //  CRM Мэдэгдэлийн схем гүйлгээ нэмэх
        public Result DB250002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB250002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Мэдэгдэлийн схем гүйлгээ нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("NotifySchemaTxn", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Мэдэгдэлийн схем гүйлгээ засварлах
        public Result DB250003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB250003(db, (object[])ri.ReceivedParam[0], (object[])ri.ReceivedParam[1]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Мэдэгдэлийн схем гүйлгээ засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[1];
                    object[] OldValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("NotifySchemaTxn", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM CRM Мэдэгдэлийн схем гүйлгээ устгах
        public Result DB250004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB250004(db, Static.ToInt(ri.ReceivedParam[0]),Static.ToLong(ri.ReceivedParam[1]),Static.ToInt(ri.ReceivedParam[2]),Static.ToLong(ri.ReceivedParam[3]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM CRM Мэдэгдэлийн схем гүйлгээ устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Схемийн дугаар";
                    lg.AddDetail("NotifySchemaTxn", "SchemaID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMPermSchema]
        //CRM Эрхийн схем жагсаалт авах
        public Result DB251000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB251000(db, 0, 0, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Эрхийн схем жагсаалт авах";
            }
        }
        //  CRM Эрхийн схем нэмэх
        public Result DB251002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB251002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Эрхийн схем нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("NotifySchema", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Эрхийн схем засварлах
        public Result DB251003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB251003(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Эрхийн схем засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("NotifySchema", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM CRM Эрхийн схем устгах
        public Result DB251004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB251004(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM CRM Эрхийн схем устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Схемийн дугаар";
                    lg.AddDetail("NotifySchema", "SchemaID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[CRMPermSchemaTXN]
        //CRM Эрхийн схем гүйлгээ жагсаалт авах
        public Result DB252000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB252000(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Эрхийн схем гүйлгээ жагсаалт авах";
            }
        }
        //  CRM Эрхийн схем гүйлгээ нэмэх
        public Result DB252002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB252002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Эрхийн схем гүйлгээ нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("NotifySchemaTxn", FieldName[i].ToString(), lg.item.Desc, NewValue[i].ToString());
                    }
                }
            }
        }
        // CRM Эрхийн схем гүйлгээ засварлах
        public Result DB252003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB252003(db, (object[])ri.ReceivedParam[0], (object[])ri.ReceivedParam[1]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Эрхийн схем гүйлгээ засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[1];
                    object[] OldValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("NotifySchemaTxn", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        //CRM CRM Эрхийн схем гүйлгээ устгах
        public Result DB252004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.CRMDB.DB252004(db, Static.ToInt(ri.ReceivedParam[0]), Static.ToLong(ri.ReceivedParam[1]), Static.ToInt(ri.ReceivedParam[2]), Static.ToLong(ri.ReceivedParam[3]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "CRM Эрхийн схем гүйлгээ устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Эрхийн схемийн дугаар";
                    lg.item.Key2 = "Гүйлгээний код";
                    lg.item.Key3 = "Төрлийн дугаар";
                    lg.item.Key4 = "Дугаар";
                    lg.AddDetail("PermSchemaTxn", "PermschemaID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        #region[Параметр шинэчлэх]
        public Result ParamUpdate(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            int check = 0;
            Result res = new Result();
            try
            {
                DataTable dt = (DataTable)ri.ReceivedParam[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (Static.ToInt(dr["Status"]) == 1)
                    {
                       res = IPos.Core.Core.InitDataParam(Static.ToInt(dr["ParamNo"]), db);
                       if (res.ResultNo != 0)
                       {
                           check = 1;
                       }
                    }
                }
                if (check != 0)
                {
                    res.ResultNo = 911002;
                    res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа";
                    return res;
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Параметр шинэчлэх";
            }
        }
        #endregion
        #region[Document бүлэглэх]
            //Документ загвар бүлэглэх жагсаалт мэдээлэл авах
             public Result Txn150022(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                            res = IPos.DB.Main.DB201022(db, null);
                         return res;
                    }
                    catch (Exception ex)
                    {
                        res.ResultNo = 9110002;
                        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                        return res;
                    }
                    finally
                    {
                        lg.item.Desc = "Документ бүлгүүдийн жагсаалт мэдээлэл авах";
                    }
                }
            //Документ загвар бүлэглэх дэлгэрэнгүй мэдээлэл авах
             public Result Txn150023(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        int pRiskGroup = Static.ToInt(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB201023(db, pRiskGroup);
                        return res;
                    }
                    catch (Exception ex)
                    {
                        res.ResultNo = 9110002;
                        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                        return res;
                    }
                    finally
                    {
                        if (res.ResultNo == 0)
                        {
                            lg.item.Desc = "Эрсдэлийн групп дэлгэрэнгүй мэдээлэл авах";
                            lg.AddDetail("RiskGroup", "RiskGroupID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
            //Документ загвар бүлэглэл нэмэх
             public Result Txn150024(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        res = IPos.DB.Main.DB201024(db, (object[])ri.ReceivedParam[0]);
                        return res;
                    }
                    catch (Exception ex)
                    {
                        res.ResultNo = 9110002;
                        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                        return res;
                    }
                    finally
                    {
                        lg.item.Desc = "Эрсдэлийн групп нэмэх";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] FieldName = (object[])ri.ReceivedParam[1];
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                lg.AddDetail("RiskGroup", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            //Документ загвар бүлэглэл засварлах
             public Result Txn150025(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        res = IPos.DB.Main.DB201025(db, (object[])ri.ReceivedParam[0]);
                        return res;
                    }
                    catch (Exception ex)
                    {
                        res.ResultNo = 9110002;
                        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                        return res;
                    }
                    finally
                    {
                        lg.item.Desc = "Эрсдэлийн бүлэг засварлах";
                        if (res.ResultNo == 0)
                        {
                            object[] NewValue = (object[])ri.ReceivedParam[0];
                            object[] OldValue = (object[])ri.ReceivedParam[1];
                            object[] FieldName = (object[])ri.ReceivedParam[2];
                            for (int i = 0; i < FieldName.Length; i++)
                            {
                                if (OldValue[i].ToString() != NewValue[i].ToString())
                                    lg.AddDetail("RiskGroup", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                            }
                        }
                    }
                }
            //Документ загвар бүлэглэл устгах
             public Result Txn150026(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
                {
                    Result res = new Result();
                    try
                    {
                        int pRiskGroup = Static.ToInt(ri.ReceivedParam[0]);
                        res = IPos.DB.Main.DB201026(db, pRiskGroup);
                        return res;
                    }
                    catch (Exception ex)
                    {
                        res.ResultNo = 9110002;
                        res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                        EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                        return res;
                    }
                    finally
                    {
                        lg.item.Desc = "Эрсдэлийн бүлэг устгах";
                        if (res.ResultNo == 0)
                        {
                            lg.item.Key1 = "Эрсдэлийн бүлгийн дугаар";
                            lg.AddDetail("RiskGroup", "RiskGroupID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                        }
                    }
                }
        #endregion
        #region[GroupData]
             //Документ загварын холбоосын жагсаалт мэдээлэл авах
             public Result Txn150027(ClientInfo ci, RequestInfo ri, DbConnections pdb, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB201027(pdb, Static.ToInt(ri.ReceivedParam[0]));
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "Эрсдэлийн бүлэгт холбогдоогүй  Эрсдэлүүдийн жагсаалт авах";
                     }
                 }
             }
             //Документ загварын холбоос нэмэх 
             public Result Txn150029(ClientInfo ci, RequestInfo ri, DbConnections pdb, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     int GroupID = Static.ToInt(ri.ReceivedParam[0]);
                     DataTable DT =(DataTable)(ri.ReceivedParam[1]);
                             res = IPos.DB.Main.DB201028(pdb,GroupID,DT);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 //finally
                 //{
                 //    if (res.ResultNo == 0)
                 //    {
                 //        lg.item.Desc = "Документын бүлэгд Документ нэмж холбох";
                 //    }
                 //}
             }
        #endregion
        #region[Dinamic Report Group]
             public Result Txn140421(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB214001(db, null);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Динамик тайлангын гүйлгээний кодын жагсаалт авах";
                 }
             }
             //Dynamic тайлангийн бүлгийн жагсаалт мэдээлэл авах
             public Result Txn140422(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB214002(db, null);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Документ бүлгүүдийн жагсаалт мэдээлэл авах";
                 }
             }
             //Dynamic тайлангийн бүлгийн дэлгэрэнгүй мэдээлэл авах
             public Result Txn140423(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     int pRiskGroup = Static.ToInt(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB214003(db, pRiskGroup);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "Эрсдэлийн групп дэлгэрэнгүй мэдээлэл авах";
                         lg.AddDetail("RiskGroup", "RiskGroupID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }
             //Dynamic тайлангийн бүлэг нэмэх
             public Result Txn140424(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB214004(db, (object[])ri.ReceivedParam[0]);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Эрсдэлийн групп нэмэх";
                     if (res.ResultNo == 0)
                     {
                         object[] NewValue = (object[])ri.ReceivedParam[0];
                         object[] FieldName = (object[])ri.ReceivedParam[1];
                         for (int i = 0; i < FieldName.Length; i++)
                         {
                             lg.AddDetail("RiskGroup", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                         }
                     }
                 }
             }
             //Dynamic тайлангийн бүлэг засварлах
             public Result Txn140425(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB214005(db, (object[])ri.ReceivedParam[0]);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Эрсдэлийн бүлэг засварлах";
                     if (res.ResultNo == 0)
                     {
                         object[] NewValue = (object[])ri.ReceivedParam[0];
                         object[] OldValue = (object[])ri.ReceivedParam[1];
                         object[] FieldName = (object[])ri.ReceivedParam[2];
                         for (int i = 0; i < FieldName.Length; i++)
                         {
                             if (OldValue[i].ToString() != NewValue[i].ToString())
                                 lg.AddDetail("RiskGroup", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                         }
                     }
                 }
             }
             //Dynamic тайлангийн бүлэг устгах
             public Result Txn140426(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     int pRiskGroup = Static.ToInt(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB214006(db, pRiskGroup);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Эрсдэлийн бүлэг устгах";
                     if (res.ResultNo == 0)
                     {
                         lg.item.Key1 = "Эрсдэлийн бүлгийн дугаар";
                         lg.AddDetail("RiskGroup", "RiskGroupID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }
             //Dynamic тайлангийн холбоосын сонгогдсон болон сонгогдоогүй жагсаалт мэдээлэл авах
             public Result Txn140427(ClientInfo ci, RequestInfo ri, DbConnections pdb, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB214007(pdb, Static.ToInt(ri.ReceivedParam[0]));
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "Эрсдэлийн бүлэгт холбогдоогүй  Эрсдэлүүдийн жагсаалт авах";
                     }
                 }
             }
             //Dynamic тайлангийн холбоос нэмэх 
             public Result Txn140428(ClientInfo ci, RequestInfo ri, DbConnections pdb, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     int GroupID = Static.ToInt(ri.ReceivedParam[0]);
                     DataTable DT = (DataTable)(ri.ReceivedParam[1]);
                     res = IPos.DB.Main.DB214008(pdb, GroupID, DT);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 //finally
                 //{
                 //    if (res.ResultNo == 0)
                 //    {
                 //        lg.item.Desc = "Документын бүлэгд Документ нэмж холбох";
                 //    }
                 //}
             }
        #endregion
        #region[Харилцагчтай холбоо барисан төрөл]
             public Result Txn120067(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB205068(db, null);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Харилцагчийн холбоо барьсан төрлийн жагсаалт авах ";
                 }
             }    //Харилцагчийн холбоо барьсан төрлийн жагсаалт авах
             public Result Txn120068(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     int _ID = Static.ToInt(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB205069(db, _ID);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "Харилцагчийн холбоо барьсан төрлийн дэлгэрэнгүй мэдээлэл авах";
                         lg.AddDetail("CustomerRate", "_ID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Харилцагчийн холбоо барьсан төрлийн дэлгэрэнгүй мэдээлэл авах
             public Result Txn120069(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];
                     object[] obj = new object[4];
                     obj[0] = Static.ToInt(value[0]);
                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]); 
                     obj[3] = Static.ToInt(value[3]);
                     res = IPos.DB.Main.DB205070(db, obj);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Харилцагчийн холбоо барьсан төрөл шинээр нэмэх";
                     if (res.ResultNo == 0)
                     {
                         
                     }
                 }
             }    //Харилцагчийн холбоо барьсан төрөл шинээр нэмэх
             public Result Txn120070(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];
                     object[] obj = new object[4];
                     obj[0] = Static.ToInt(value[0]);
                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]);
                     obj[3] = Static.ToInt(value[3]);
                     res = IPos.DB.Main.DB205071(db, obj);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Харилцагчийн холбоо барьсан төрөл засварлах";                   
                 }
             }    //Харилцагчийн холбоо барьсан төрөл засварлах
             public Result Txn120071(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     int pRiskGroup = Static.ToInt(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB205072(db, pRiskGroup);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Харилцагчийн холбоо барьсан төрөл устгах";
                     if (res.ResultNo == 0)
                     {
                         //lg.item.Key1 = "Эрсдэлийн бүлгийн дугаар";
                         //lg.AddDetail("RiskGroup", "RiskGroupID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Харилцагчийн холбоо барьсан төрөл устгах
        #endregion[]    
             //Бараа материал нэгжийн төрөл OK log
        #region[UnitType]

             public Result SelectUnitType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {

                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202106(db, 0, 0, null);

                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Бараа материал нэгжийн төрөл жагсаалт авах";
                 }
             }
             public Result AddUnitType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202108(db, (object[])ri.ReceivedParam[0]);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Бараа материал нэгжийн төрөл жагсаалт нэмэх";
                     if (res.ResultNo == 0)
                     {
                         object[] NewValue = (object[])ri.ReceivedParam[0];
                         object[] FieldName = (object[])ri.ReceivedParam[1];
                         for (int i = 0; i < FieldName.Length; i++)
                         {
                             lg.AddDetail("UnitType", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                         }

                     }
                 }
             }
             public Result DeleteUnitType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202110(db, Static.ToLong(ri.ReceivedParam[0]));

                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Бараа материал нэгжийн төрөл жагсаалт устгах";
                     if (res.ResultNo == 0)
                     {
                         lg.item.Key1 = "Нэгжийн Төрлийн дугаар";
                         lg.AddDetail("UnitType", "UnitTypeCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }
             public Result EditUnitType(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202109(db, (object[])ri.ReceivedParam[0]);

                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Бараа материал нэгжийн төрөл жагсаалт засварлах";
                     if (res.ResultNo == 0)
                     {
                         object[] NewValue = (object[])ri.ReceivedParam[0];
                         object[] OldValue = (object[])ri.ReceivedParam[1];
                         object[] FieldName = (object[])ri.ReceivedParam[2];
                         for (int i = 0; i < FieldName.Length; i++)
                         {
                             if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("UnitType", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                         }
                     }
                 }
             }

             #endregion
        #region[Өдрийн төрлийн бүртгэл]
             public Result Txn140131(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202131(db, 0, 0);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Өдрийн төрлийн бүртгэл жагсаалт мэдээлэл авах ";
                 }
             }    //Өдрийн төрлийн бүртгэл жагсаалт мэдээлэл авах
             public Result Txn140132(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string _ID = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202132(db,_ID);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "Өдрийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                         lg.AddDetail("PaDayType", "DayType", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Өдрийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
             public Result Txn140133(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     //object[] value = (object[])ri.ReceivedParam[0];
                     //object[] obj = new object[2];
                     //obj[0] = Static.ToStr(value[0]);
                     //obj[1] = Static.ToStr(value[1]);

                     res = IPos.DB.Main.DB202133(db, (object[])ri.ReceivedParam[0]);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Өдрийн төрлийн бүртгэл шинээр нэмэх";
                     if (res.ResultNo == 0)
                     {

                     }
                 }
             }    //Өдрийн төрлийн бүртгэл шинээр нэмэх
             public Result Txn140134(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     //object[] value = (object[])ri.ReceivedParam[0];
                     //object[] obj = new object[2];
                     //obj[0] = Static.ToStr(value[0]);
                     //obj[1] = Static.ToStr(value[1]);

                     res = IPos.DB.Main.DB202134(db, (object[])ri.ReceivedParam[0]);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Өдрийн төрлийн бүртгэл засварлах";
                 }
             }    //Өдрийн төрлийн бүртгэл засварлах
             public Result Txn140135(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pDayType = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202135(db, pDayType);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Өдрийн төрлийн бүртгэл устгах";
                     if (res.ResultNo == 0)
                     {
                         lg.item.Key1 = "Өдрийн төрлийн бүртгэл устгах";
                         lg.AddDetail("pDayType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Өдрийн төрлийн бүртгэл устгах

             #endregion[]  
        #region[Цаг агаарын төрлийн бүртгэл]
             public Result Txn140141(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202141(db, 0, 0);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Өдрийн төрлийн бүртгэл жагсаалт мэдээлэл авах ";
                 }
             }    //Цаг агаарын төрлийн бүртгэл жагсаалт мэдээлэл авах
             public Result Txn140142(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pWeatherId = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202142(db, pWeatherId);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                     //    lg.item.Desc = "Цаг агаарын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                     //    lg.AddDetail("pWeatherId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Цаг агаарын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
             public Result Txn140143(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     //object[] value = (object[])ri.ReceivedParam[0];
                     //object[] obj = new object[3];
                     //obj[0] = Static.ToStr(value[0]);
                     //obj[1] = Static.ToStr(value[1]);
                     //obj[2] = Static.ToStr(value[2]);

                     res = IPos.DB.Main.DB202143(db, (object[])ri.ReceivedParam[0]);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Цаг агаарын төрлийн бүртгэл шинээр нэмэх";
                     if (res.ResultNo == 0)
                     {

                     }
                 }
             }    //Цаг агаарын төрлийн бүртгэл шинээр нэмэх
             public Result Txn140144(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     //object[] value = (object[])ri.ReceivedParam[0];
                     //object[] obj = new object[3];
                     //obj[0] = Static.ToStr(value[0]);
                     //obj[1] = Static.ToStr(value[1]);
                     //obj[2] = Static.ToStr(value[2]);

                     res = IPos.DB.Main.DB202144(db, (object[])ri.ReceivedParam[0]);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Цаг агаарын төрлийн бүртгэл засварлах";
                 }
             }    //Цаг агаарын төрлийн бүртгэл засварлах
             public Result Txn140145(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pWeatherId = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202145(db, pWeatherId);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Цаг агаарын төрлийн бүртгэл устгах";
                     if (res.ResultNo == 0)
                     {
                         lg.item.Key1 = "Цаг агаарын төрлийн бүртгэл устгах";
                         lg.AddDetail("pWeatherId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Цаг агаарын төрлийн бүртгэл устгах
        #endregion[] 
        #region[Pos-ын бүртгэл]
             public Result Txn140076(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202076(db);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "";
                 }
             }    //Pos-ын бүртгэл жагсаалт мэдээлэл авах
             public Result Txn140077(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pPosNo = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202077(db, pPosNo);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "";
                         lg.AddDetail("pPosNo", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Pos-ын бүртгэл дэлгэрэнгүй мэдээлэл авах
             public Result Txn140078(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];
                     object[] obj = new object[6];
                     obj[0] = Static.ToStr(value[0]);
                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]);
                     obj[3] = Static.ToStr(value[3]);
                     obj[4] = Static.ToStr(value[4]);
                     obj[5] = Static.ToStr(value[5]);

                     res = IPos.DB.Main.DB202078(db, obj);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "бүртгэл шинээр нэмэх";
                     if (res.ResultNo == 0)
                     {

                     }
                 }
             }    //Pos-ын бүртгэл шинээр нэмэх
             public Result Txn140079(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];
                     object[] obj = new object[6];
                     obj[0] = Static.ToStr(value[0]);
                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]);
                     obj[3] = Static.ToStr(value[3]);
                     obj[4] = Static.ToStr(value[4]);
                     obj[5] = Static.ToStr(value[5]);

                     res = IPos.DB.Main.DB202079(db, obj);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "бүртгэл засварлах";
                 }
             }    //Pos-ын бүртгэл засварлах
             public Result Txn140080(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pPOSnO = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202080(db, pPOSnO);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "бүртгэл устгах";
                     if (res.ResultNo == 0)
                     {
                         lg.item.Key1 = "устгах";
                         lg.AddDetail("pPOSnO", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Pos-ын бүртгэл устгах
             #endregion[] 
        #region[Бараа материалын төрлийн бүртгэл]
             public Result Txn140156(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202156(db, 0, 0);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Бараа материалын  төрлийн бүртгэл жагсаалт мэдээлэл авах ";
                 }
             }    //Бараа материалын  төрлийн бүртгэл жагсаалт мэдээлэл авах
             public Result Txn140157(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pInvType = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202157(db, pInvType);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "Бараа материалын  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                         lg.AddDetail("pInvType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Бараа материалын  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
             public Result Txn140158(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];
                     object[] obj = new object[6];
                     obj[0] = Static.ToStr(value[0]);
                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]);
                     obj[3] = Static.ToInt(value[3]);
                     obj[4] = Static.ToStr(value[4]);
                     obj[5] = Static.ToInt(value[5]);

                     res = IPos.DB.Main.DB202158(db, obj);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Бараа материалын  төрлийн бүртгэл шинээр нэмэх";
                     if (res.ResultNo == 0)
                     {

                     }
                 }
             }    //Бараа материалын  төрлийн бүртгэл шинээр нэмэх
             public Result Txn140159(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];
                     object[] obj = new object[6];
                     obj[0] = Static.ToStr(value[0]);
                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]);
                     obj[3] = Static.ToInt(value[3]);
                     obj[4] = Static.ToStr(value[4]);
                     obj[5] = Static.ToInt(value[5]);

                     res = IPos.DB.Main.DB202159(db, obj);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Бараа материалын  төрлийн  бүртгэл засварлах";
                 }
             }    //Бараа материалын  төрлийн  бүртгэл засварлах
             public Result Txn140160(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pInvType = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202160(db, pInvType);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Бараа материалын  төрлийн бүртгэл устгах";
                     if (res.ResultNo == 0)
                     {
                         lg.item.Key1 = "Цаг агаарын төрлийн бүртгэл устгах";
                         lg.AddDetail("pInvType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Бараа материалын  төрлийн бүртгэл устгах
             #endregion[]  
        #region[Тагийн төрлийн бүртгэл]
             public Result Txn140171(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202171(db);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Тагийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
                 }
             }    //Тагийн төрлийн бүртгэл жагсаалт мэдээлэл авах
             public Result Txn140172(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pTagType = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202172(db, pTagType);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "Тагийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                         lg.AddDetail("pTagType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Тагийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
             public Result Txn140173(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];
                     object[] obj = new object[7];
                     obj[0] = Static.ToStr(value[0]);
                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]);
                     obj[3] = Static.ToStr(value[3]);
                     obj[4] = Static.ToInt(value[4]);
                     obj[5] = Static.ToInt(value[5]);
                     obj[6] = Static.ToInt(value[6]);                     

                     res = IPos.DB.Main.DB202173(db, obj);
                     return res;

                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Тагийн төрлийн бүртгэл шинээр нэмэх";
                     if (res.ResultNo == 0)
                     {

                     }
                 }
             }    //Тагийн төрлийн бүртгэл шинээр нэмэх
             public Result Txn140174(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];                     
                     object[] obj = new object[7];
                     obj[0] = Static.ToStr(value[0]);
                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]);
                     obj[3] = Static.ToStr(value[3]);
                     obj[4] = Static.ToInt(value[4]);
                     obj[5] = Static.ToInt(value[5]);
                     obj[6] = Static.ToInt(value[6]);

                     res = IPos.DB.Main.DB202174(db, obj);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Тагийн төрлийн бүртгэл засварлах";
                 }
             }    //Тагийн төрлийн бүртгэл засварлах
             public Result Txn140175(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pTagType = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202175(db, pTagType);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Тагийн төрлийн бүртгэл устгах";
                     if (res.ResultNo == 0)
                     {
                         lg.item.Key1 = "Тагийн төрлийн бүртгэл устгах";
                         lg.AddDetail("pInvType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Тагийн төрлийн бүртгэл устгах
             #endregion[]  
        #region[Үйлчилгээний төрлийн бүртгэл]
             public Result Txn140186(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     res = IPos.DB.Main.DB202186(db);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Үйлчилгээний  төрлийн бүртгэл жагсаалт мэдээлэл авах";
                 }
             }    //Үйлчилгээний  төрлийн бүртгэл жагсаалт мэдээлэл авах
             public Result Txn140187(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pServType = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202187(db, pServType);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     if (res.ResultNo == 0)
                     {
                         lg.item.Desc = "Үйлчилгээний төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                         lg.AddDetail("pServType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Үйлчилгээний  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
             public Result Txn140188(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     //object[] value = (object[])ri.ReceivedParam[0];
                     //object[] obj = new object[5];
                     //obj[0] = Static.ToStr(value[0]);
                     //obj[1] = Static.ToStr(value[1]);
                     //obj[2] = Static.ToStr(value[2]);
                     //obj[3] = Static.ToStr(value[3]);
                     //obj[4] = Static.ToInt(value[4]);

                     res = IPos.DB.Main.DB202188(db, (object[])ri.ReceivedParam[0]);
                     return res;                   
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Тагийн төрлийн бүртгэл шинээр нэмэх";
                     if (res.ResultNo == 0)
                     {

                     }
                 }
             }    //Үйлчилгээний  төрлийн бүртгэл шинээр нэмэх
             public Result Txn140189(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     //object[] value = (object[])ri.ReceivedParam[0];
                     //object[] obj = new object[5];
                     //obj[0] = Static.ToStr(value[0]);
                     //obj[1] = Static.ToStr(value[1]);
                     //obj[2] = Static.ToStr(value[2]);
                     //obj[3] = Static.ToStr(value[3]);
                     //obj[4] = Static.ToInt(value[4]);

                     res = IPos.DB.Main.DB202189(db, (object[])ri.ReceivedParam[0]);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Үйлчилгээний төрлийн бүртгэл засварлах";
                 }
             }    //Үйлчилгээний  төрлийн бүртгэл засварлах
             public Result Txn140190(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     string pServType = Static.ToStr(ri.ReceivedParam[0]);
                     res = IPos.DB.Main.DB202190(db, pServType);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Үйлчилгээний төрлийн бүртгэл устгах";
                     if (res.ResultNo == 0)
                     {
                         lg.item.Key1 = "Үйлчилгээний төрлийн бүртгэл устгах";
                         lg.AddDetail("pServType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                     }
                 }
             }    //Үйлчилгээний  төрлийн бүртгэл устгах
             #endregion[]  
        
        #region[Мөнгөн тэмдэгтийн төрлийн бүртгэл]
        public Result Txn140191(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202191(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Мөнгөн тэмдэгтийн  төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //Мөнгөн тэмдэгтийн  төрлийн бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140192(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pCurrency = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202192(db, pCurrency);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Мөнгөн тэмдэгтийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pCurrency", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Мөнгөн тэмдэгтийн  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140193(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);

                res = IPos.DB.Main.DB202193(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Мөнгөн тэмдэгтийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Мөнгөн тэмдэгтийн  төрлийн бүртгэл шинээр нэмэх
        public Result Txn140194(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];                     
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);

                res = IPos.DB.Main.DB202194(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Мөнгөн тэмдэгтийн төрлийн бүртгэл засварлах";
            }
        }    //Мөнгөн тэмдэгтийн  төрлийн бүртгэл засварлах
        public Result Txn140195(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pCurrency = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202195(db, pCurrency);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Мөнгөн тэмдэгтийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Мөнгөн тэмдэгтийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pCurrency", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Мөнгөн тэмдэгтийн  төрлийн бүртгэл устгах
        #endregion[]  

        #region[Төлбөрийн төрлийн  төрлийн бүртгэл]
        //Төлбөрийн төрлийн бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140201(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202201(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }
        //Төлбөрийн төрлийн  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140202(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pTyId = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202202(db, pTyId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Төлбөрийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pTyId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    
        //Төлбөрийн төрлийн бүртгэл шинээр нэмэх     
        public Result Txn140203(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
             {
                 Result res = new Result();
                 try
                 {
                     object[] value = (object[])ri.ReceivedParam[0];
                     object[] obj = new object[8];
                     int pPaymentFlag = 9;
                     string pTypeID = "";

                     obj[0] = Static.ToStr(value[0]);
                     pTypeID = Static.ToStr(obj[0]);

                     obj[1] = Static.ToStr(value[1]);
                     obj[2] = Static.ToStr(value[2]);
                     obj[3] = Static.ToStr(value[3]);
                     obj[4] = Static.ToInt(value[4]);
                     //PaymentFlag
                     //0-Бэлэн төлбөр. Validation хийж, зөвхөн 1 бүртгэл бүртгэсэн эсэхийг шалгах.
                     //1-Картын төлбөр. Validation хийж, зөвхөн 1 бүртгэл бүртгэсэн эсэхийг шалгах.
                     //9-Бусад төлбөр (гэрээ, т/д, ваучир гм)
                     obj[5] = Static.ToInt(value[5]);
                     pPaymentFlag = Static.ToInt(obj[5]);

                     switch (pPaymentFlag)
                     {

                         case 0:
                             {
                                 res = IPos.DB.Main.DB202196(db, pTypeID, pPaymentFlag);
                                 if (res.AffectedRows != 0)
                                 {
                                     res.ResultNo = 1;
                                     res.ResultDesc = "Бэлэн төлбөр гэсэн сонголтоор бүртгэл хийсэн байна. Төрлийн дугаар: " + res.Data.Tables[0].Rows[0]["TypeId"];
                                     return res;
                                 }
                             } break;
                         case 1:
                             {
                                 res = IPos.DB.Main.DB202196(db, pTypeID, pPaymentFlag);
                                 if (res.AffectedRows != 0)
                                 {
                                     res.ResultNo = 1;
                                     res.ResultDesc = "Картын төлбөр гэсэн сонголтоор бүртгэл хийсэн байна. Төрлийн дугаар: " + res.Data.Tables[0].Rows[0]["TypeId"];
                                     return res;
                                 } break;
                             }
                     }

                     obj[6] = Static.ToStr(value[6]);
                     obj[7] = Static.ToStr(value[7]);

                     res = IPos.DB.Main.DB202203(db, obj);
                     return res;
                 }
                 catch (Exception ex)
                 {
                     res.ResultNo = 9110002;
                     res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                     EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                     return res;
                 }
                 finally
                 {
                     lg.item.Desc = "Төлбөрийн төрлийн бүртгэл шинээр нэмэх";
                     if (res.ResultNo == 0)
                     {

                     }
                 }
             }    
        //Төлбөрийн төрлийн  төрлийн бүртгэл засварлах
        public Result Txn140204(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[8];
                int pPaymentFlag = 9;
                string pTypeID = "";

                obj[0] = Static.ToStr(value[0]);
                pTypeID = Static.ToStr(obj[0]);

                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);
                //PaymentFlag
                //0-Бэлэн төлбөр. Validation хийж, зөвхөн 1 бүртгэл бүртгэсэн эсэхийг шалгах.
                //1-Картын төлбөр. Validation хийж, зөвхөн 1 бүртгэл бүртгэсэн эсэхийг шалгах.
                //9-Бусад төлбөр (гэрээ, т/д, ваучир гм)
                obj[5] = Static.ToInt(value[5]);
                pPaymentFlag = Static.ToInt(obj[5]);

                switch (pPaymentFlag)
                {

                    case 0:
                        {
                            res = IPos.DB.Main.DB202196(db, pTypeID, pPaymentFlag);
                            if (res.AffectedRows != 0)
                            {
                                res.ResultNo = 1;
                                res.ResultDesc = "Бэлэн төлбөр гэсэн сонголтоор бүртгэл хийсэн байна. Төрлийн дугаар: " + res.Data.Tables[0].Rows[0]["TypeId"];
                                return res;
                            }
                        } break;
                    case 1:
                        {
                            res = IPos.DB.Main.DB202196(db, pTypeID, pPaymentFlag);
                            if (res.AffectedRows != 0)
                            {
                                res.ResultNo = 1;
                                res.ResultDesc = "Картын төлбөр гэсэн сонголтоор бүртгэл хийсэн байна. Төрлийн дугаар: " + res.Data.Tables[0].Rows[0]["TypeId"];
                                return res;
                            } break;
                        }
                }
                                          
                obj[6] = Static.ToStr(value[6]);
                obj[7] = Static.ToStr(value[7]);

                res = IPos.DB.Main.DB202204(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл засварлах";
            }
        }    
        //Төлбөрийн төрлийн  төрлийн бүртгэл устгах     
        public Result Txn140205(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pTypeId = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202205(db, pTypeId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төлбөрийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pTypeId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    
        #endregion[]  

        #region[Брэндийн төрлийн бүртгэл]
        public Result Txn140151(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202151(db, 0, 0);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //Брэндийн төрлийн бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140152(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pBrendId = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202152(db, pBrendId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Брэндийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pBrendId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Брэндийн төрлийн  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140153(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);                     


                res = IPos.DB.Main.DB202153(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Брэндийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Брэндийн төрлийн бүртгэл шинээр нэмэх
        public Result Txn140154(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]); 

                res = IPos.DB.Main.DB202154(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Брэндийн төрлийн бүртгэл засварлах";
            }
        }    //Брэндийн төрлийн  төрлийн бүртгэл засварлах
        public Result Txn140155(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pBrendId = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202155(db, pBrendId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Брэндийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Брэндийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pBrendId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Брэндийн төрлийн  төрлийн бүртгэл устгах
        #endregion[] 

        #region[ХУВААРИЛАЛТЫН ТӨРЛИЙН БҮРТГЭЛ]
        public Result Txn140181(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202181(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хуваарилалтын төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //Хуваарилалтын төрлийн бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140182(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pScheduleID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202182(db, pScheduleID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Хуваарилалтын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pScheduleID", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Хуваарилалтын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140183(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[6];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);
                obj[5] = Static.ToInt(value[5]);


                res = IPos.DB.Main.DB202183(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хуваарилалтын төрлийн бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Хуваарилалтын төрлийн бүртгэл нэмэх
        public Result Txn140184(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[6];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);
                obj[5] = Static.ToInt(value[5]);

                res = IPos.DB.Main.DB202184(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хуваарилалтын төрлийн бүртгэл засварлах";
            }
        }    //Хуваарилалтын төрлийн бүртгэл засварлах
        public Result Txn140185(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pScheduleId = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202185(db, pScheduleId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Брэндийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Хуваарилалтын төрлийн бүртгэл устгах";
                    lg.AddDetail("pScheduleId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Хуваарилалтын төрлийн бүртгэл устгах
        #endregion[] 

        #region[Бараа материалын үндсэн бүртгэл]
        public Result Txn140211(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            try
            {
                res = IPos.DB.Main.DB202211(db, pagenumber, pagecount, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын үндсэн бүртгэл жагсаалт мэдээлэл авах";

            }
        }     
        public Result Txn140212(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();
            int userno = 0;
            userno = Static.ToInt(ri.ReceivedParam[0]);
            try
            {
                ret.ResultNo = 9110002;

                string pInvTypeId = Static.ToStr(ri.ReceivedParam[0]);

                res = IPos.DB.Main.DB202212(db, pInvTypeId);     // Бараа материалын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "InvType";
                DataSet ds = new DataSet();
                ds.Tables.Add(res.Data.Tables[0].Copy());                    

                ret.Data = ds;
                ret.ResultNo = 0;
                return ret;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                object[] newvalue = new object[2];
                newvalue[0] = userno;
                newvalue[1] = res.ResultNo;
                lg.AddDetail("pInvTypeId", newvalue[0].ToString(), "Бараа материалын үндсэн бүртгэл дэлгэрэнгүй мэдээлэл авах", newvalue[1].ToString());
            }
        }
        public Result Txn140213(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB202213(db, value, ri.UserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын үндсэн бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Бараа материалын үндсэн бүртгэл нэмэх
        public Result Txn140214(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {                     
                object[] value = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB202214(db, value, ri.UserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хуваарилалтын төрлийн бүртгэл засварлах";
            }
        }
        public Result Txn140215(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pInvId = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202215(db, pInvId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Брэндийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Хуваарилалтын төрлийн бүртгэл устгах";
                    lg.AddDetail("pScheduleId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }

        //Барааны үнийн бүртгэл
        public Result Txn140266(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                int pProdType = Static.ToInt(ri.ReceivedParam[0]);
                string ProdID = Static.ToStr(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB202266(db, pProdType, ProdID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний үнийн бүртгэл жагсаалт мэдээлэл авах";

            }
        }
        public Result Txn140267(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                int pProdType = Static.ToInt(ri.ReceivedParam[0]);
                string pProdID = Static.ToStr(ri.ReceivedParam[1]);
                string PriceTypeID = Static.ToStr(ri.ReceivedParam[2]);
                //DateTime pStartTime = Static.ToDateTime(ri.ReceivedParam[3]);
                res = IPos.DB.Main.DB202267(db, pProdType, pProdID, PriceTypeID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                //lg.item.Desc = "дэлгэрэнгүй мэдээлэл авах";
                //if (res.ResultNo == 0)
                //{
                //    lg.AddDetail("", "pUserNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                //}
            }
        }
        public Result Txn140268(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                //object[] obj = new object[6];
                //obj[0] = Static.ToInt(value[0]);
                //obj[1] = Static.ToStr(value[1]);
                //obj[2] = Static.ToStr(value[2]);
                //obj[3] = Static.ToDateTime(value[3]);
                //obj[4] = Static.ToDateTime(value[4]);
                //obj[5] = Static.ToInt(value[5]);
                res = IPos.DB.Main.DB202268(db, value);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                //lg.item.Desc = "Үйлчилгээнд агуулагдах бараа материал бүртгэл нэмэх";
                //if (res.ResultNo == 0)
                //{

                //}
            }
        }
        public Result Txn140269(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[1];
                //object[] obj = new object[6];
                //obj[0] = Static.ToInt(value[0]);
                //obj[1] = Static.ToStr(value[1]);
                //obj[2] = Static.ToStr(value[2]);
                //obj[3] = Static.ToDateTime(value[3]);
                //obj[4] = Static.ToDateTime(value[4]);
                //obj[5] = Static.ToInt(value[5]);

                object[] pOldValue = (object[])ri.ReceivedParam[0];
                //object[] pOldValue = new object[6];
                //pOldValue[0] = Static.ToInt(OldValue[0]);
                //pOldValue[1] = Static.ToStr(OldValue[1]);
                //pOldValue[2] = Static.ToStr(OldValue[2]);
                //pOldValue[3] = Static.ToDateTime(OldValue[3]);
                //pOldValue[4] = Static.ToDateTime(OldValue[4]);
                //pOldValue[5] = Static.ToInt(OldValue[5]);

                res = IPos.DB.Main.DB202269(db, pOldValue, value);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл засварлах";
            }
        }
        public Result Txn140270(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                int pProdType = Static.ToInt(ri.ReceivedParam[0]);
                string pProdID = Static.ToStr(ri.ReceivedParam[1]);
                string PriceTypeID = Static.ToStr(ri.ReceivedParam[2]);
                //DateTime pStartTime = Static.ToDateTime(ri.ReceivedParam[3]);
                res = IPos.DB.Main.DB202270(db, pProdType, pProdID, PriceTypeID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний үнийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Үйлчилгээний үнийн бүртгэл устгах";
                    lg.AddDetail("pProdType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }

        //DB202291 - Барааны серийн дугааруудын жагсаалт
        public Result Txn140291(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pInvID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202291(db, pInvID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний үнийн бүртгэл жагсаалт мэдээлэл авах";

            }
        }
        //DB202292 - Барааны серийн дугааруудын дэлгэрэнгүй
        public Result Txn140292(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                //string pInvID = Static.ToStr(ri.ReceivedParam[0]);
                //string pBarCode = Static.ToStr(ri.ReceivedParam[1]);
                string pItemNo = Static.ToStr(ri.ReceivedParam[0]);

                res = IPos.DB.Main.DB202292(db, pItemNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                //lg.item.Desc = "дэлгэрэнгүй мэдээлэл авах";
                //if (res.ResultNo == 0)
                //{
                //    lg.AddDetail("", "pUserNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                //}
            }
        }
        //DB202293 - Барааны серийн дугаарууд нэмэх
        public Result Txn140293(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                value[0] = Static.ToStr(EServ.Interface.Sequence.NextByVal("ITEMNO"));
                //object[] obj = new object[3];
                //obj[0] = Static.ToStr(value[0]);
                //obj[1] = Static.ToStr(value[1]);
                //obj[2] = Static.ToInt(value[2]);
                res = IPos.DB.Main.DB202293(db, value);  
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                //lg.item.Desc = "Үйлчилгээнд агуулагдах бараа материал бүртгэл нэмэх";
                //if (res.ResultNo == 0)
                //{

                //}
            }
        }
        //DB202294 - Барааны серийн дугаарууд засварлах
        public Result Txn140294(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[1];

                res = IPos.DB.Main.DB202294(db, value);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Барааны серийн дугаарууд бүртгэл засварлах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Барааны серийн дугаарууд бүртгэл засварлах";
                    lg.AddDetail("INVSERIES", "ITEMNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //DB202295 - Барааны серийн дугаарууд устгах
        public Result Txn140295(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pItemNo = Static.ToStr(ri.ReceivedParam[0]);
                //string pBarCode = Static.ToStr(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB202295(db, pItemNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Барааны серийн дугаарууд бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Барааны серийн дугаарууд бүртгэл устгах";
                    lg.AddDetail("INVSERIES", "ITEMNO", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion[]

        #region[Бараа материалын ангиллын бүртгэл]
        public Result Txn140216(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202216(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын ангиллын бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //Бараа материалын ангиллын бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140217(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pCatCode = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202217(db, pCatCode);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Бараа материалын ангиллын төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pCatCode", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Бараа материалын ангиллын бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140218(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);


                res = IPos.DB.Main.DB202218(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын ангиллын төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Бараа материалын ангиллын бүртгэл шинээр нэмэх
        public Result Txn140219(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);

                res = IPos.DB.Main.DB202219(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын ангиллын төрлийн бүртгэл засварлах";
            }
        }    //Бараа материалын ангиллын бүртгэл засварлах
        public Result Txn140220(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pCatCode = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202220(db, pCatCode);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын ангиллын төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Бараа материалын ангиллын төрлийн бүртгэл устгах";
                    lg.AddDetail("pCatCode", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Бараа материалын ангиллын бүртгэл устгах
        #endregion[] 

        #region[Үйлчилгээний ангилалын бүртгэл]
        public Result Txn140221(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202216_1(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний ангиллын бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //Үйлчилгээний ангиллын бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140222(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pCatCode = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202217_1(db, pCatCode);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Үйлчилгээний ангиллын бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pCatCode", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Үйлчилгээний ангиллын бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140223(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);


                res = IPos.DB.Main.DB202218_1(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний ангиллын бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Үйлчилгээний ангиллын бүртгэл шинээр нэмэх
        public Result Txn140224(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[4];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);

                res = IPos.DB.Main.DB202219_1(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний ангиллын бүртгэл засварлах";
            }
        }    //Үйлчилгээний ангиллын бүртгэл засварлах
        public Result Txn140225(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pCatCode = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202220_1(db, pCatCode);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний ангиллын бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Үйлчилгээний ангиллын төрлийн бүртгэл устгах";
                    lg.AddDetail("pCatCode", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Үйлчилгээний ангиллын бүртгэл устгах
        #endregion[] 

        #region[Үйлчилгээний бүртгэл]
        public Result Txn140226(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            try
            {
                res = IPos.DB.Main.DB202226(db, pagenumber, pagecount, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний бүртгэл жагсаалт мэдээлэл авах";

            }
        }
        public Result Txn140227(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();
            int userno = 0;
            userno = Static.ToInt(ri.ReceivedParam[0]);
            try
            {
                ret.ResultNo = 9110002;

                string pServID = Static.ToStr(ri.ReceivedParam[0]);

                res = IPos.DB.Main.DB202227(db, pServID);
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "pServID";
                DataSet ds = new DataSet();
                ds.Tables.Add(res.Data.Tables[0].Copy());

                ret.Data = ds;
                ret.ResultNo = 0;
                return ret;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний бүртгэл дэлгэрэнгүй мэдээлэл авах";
                object[] newvalue = new object[2];
                newvalue[0] = userno;
                newvalue[1] = res.ResultNo;
                lg.AddDetail("pServID", newvalue[0].ToString(), "Үйлчилгээний бүртгэл дэлгэрэнгүй мэдээлэл авах", newvalue[1].ToString());
            }
        }
        //Үйлчилгээний бүртгэл нэмэх
        public Result Txn140228(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB202228(db, value, ri.UserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бараа материалын үндсэн бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    
        //Үйлчилгээний бүртгэл засах
        public Result Txn140229(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB202229(db, value, ri.UserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хуваарилалтын төрлийн бүртгэл засварлах";
                 
            }
        }
        public Result Txn140230(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pServId = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202230(db, pServId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Үйлчилгээний бүртгэл устгах";
                    lg.AddDetail("pServId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }

        public Result Txn140236(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try                     
            {
                string pSerID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202236(db, pSerID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээнд агуулагдах бараа материал жагсаалт мэдээлэл авах";

            }
        }
        public Result Txn140237(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                string pServID = Static.ToStr(value[0]);
                string pInvID = Static.ToStr(value[1]);
                res = IPos.DB.Main.DB202237(db, pServID, pInvID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн төлбөрийн хэрэгсэл дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("", "pUserNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result Txn140238(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[2];
                obj[0] = Static.ToStr(value[0]);  
                obj[1] = Static.ToStr(value[1]);

                res = IPos.DB.Main.DB202238(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээнд агуулагдах бараа материал бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Үйлчилгээний бүртгэл нэмэх
        public Result Txn140239(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
            string pServID = Static.ToStr(ri.ReceivedParam[0]);
            string pOldInvID = Static.ToStr(ri.ReceivedParam[1]);
            string pNewInvID = Static.ToStr(ri.ReceivedParam[2]);

                res = IPos.DB.Main.DB202239(db, pServID, pOldInvID, pNewInvID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээнд агуулагдах бараа материал бүртгэл засварлах";
            }
        }
        public Result Txn140240(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {                 
                string pSerID = Static.ToStr(ri.ReceivedParam[0]);
                string pInvId = Static.ToStr(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB202240(db, pSerID, pInvId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Үйлчилгээнд агуулагдах бараа материал устгах";
                    lg.AddDetail("pServId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }

        //Үйлчилгээний үнийн бүртгэл
        public Result Txn140271(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                int pProdType = Static.ToInt(ri.ReceivedParam[0]);
                string ProdID = Static.ToStr(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB202266(db, pProdType, ProdID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний үнийн бүртгэл жагсаалт мэдээлэл авах";

            }
        }  //Үйлчилгээний үнийн бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140272(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {

            Result res = new Result();
            try
            {
                int pProdType = Static.ToInt(ri.ReceivedParam[0]);
                string pProdID = Static.ToStr(ri.ReceivedParam[1]);
                string PriceTypeID = Static.ToStr(ri.ReceivedParam[2]);
                //DateTime pStartTime = Static.ToDateTime(ri.ReceivedParam[3]);
                res = IPos.DB.Main.DB202267(db, pProdType, pProdID, PriceTypeID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                //lg.item.Desc = "дэлгэрэнгүй мэдээлэл авах";
                //if (res.ResultNo == 0)
                //{
                //    lg.AddDetail("", "pUserNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                //}
            }
        }
        public Result Txn140273(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                //object[] obj = new object[6];
                //obj[0] = Static.ToInt(value[0]);
                //obj[1] = Static.ToStr(value[1]);
                //obj[2] = Static.ToStr(value[2]);
                //obj[3] = Static.ToDateTime(value[3]);
                //obj[4] = Static.ToDateTime(value[4]);
                //obj[5] = Static.ToInt(value[5]);
                res = IPos.DB.Main.DB202268(db, value);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                //lg.item.Desc = "Үйлчилгээнд агуулагдах бараа материал бүртгэл нэмэх";
                //if (res.ResultNo == 0)
                //{

                //}
            }
        }  
        public Result Txn140274(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] Oldvalue = (object[])ri.ReceivedParam[0];
                //object[] pOldValue = new object[6];
                //pOldValue[0] = Static.ToInt(Oldvalue[0]);
                //pOldValue[1] = Static.ToStr(Oldvalue[1]);
                //pOldValue[2] = Static.ToStr(Oldvalue[2]);
                //pOldValue[3] = Static.ToDateTime(Oldvalue[3]);
                //pOldValue[4] = Static.ToDateTime(Oldvalue[4]);
                //pOldValue[5] = Static.ToInt(Oldvalue[5]);

                object[] value = (object[])ri.ReceivedParam[1];
                //object[] obj = new object[6];
                //obj[0] = Static.ToInt(value[0]);
                //obj[1] = Static.ToStr(value[1]);
                //obj[2] = Static.ToStr(value[2]);
                //obj[3] = Static.ToDateTime(value[3]);
                //obj[4] = Static.ToDateTime(value[4]);
                //obj[5] = Static.ToInt(value[5]);
                res = IPos.DB.Main.DB202269(db, Oldvalue, value);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл засварлах";
            }
        }
        public Result Txn140275(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                int pProdType = Static.ToInt(ri.ReceivedParam[0]);
                string pProdID = Static.ToStr(ri.ReceivedParam[1]);
                string PriceTypeID = Static.ToStr(ri.ReceivedParam[2]);
                //DateTime pStartTime = Static.ToDateTime(ri.ReceivedParam[3]);
                res = IPos.DB.Main.DB202270(db, pProdType, pProdID, PriceTypeID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үйлчилгээний үнийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Үйлчилгээний үнийн бүртгэл устгах";
                    lg.AddDetail("pProdType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion[]

        #region[Гэрээний төрлийн бүртгэл]
        public Result Txn140231(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202231(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //
        public Result Txn140232(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pContractType = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202232(db, pContractType);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Төлбөрийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pTyId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Төлбөрийн төрлийн  төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140233(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);                    
                obj[3] = Static.ToInt(value[3]);
                obj[4] = value[4];


                res = IPos.DB.Main.DB202233(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Төлбөрийн төрлийн бүртгэл шинээр нэмэх
        public Result Txn140234(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToInt(value[3]);
                obj[4] = value[4];

                res = IPos.DB.Main.DB202234(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл засварлах";
            }
        }    //Төлбөрийн төрлийн  төрлийн бүртгэл засварлах
        public Result Txn140235(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pContractType = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202235(db,  pContractType);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төлбөрийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pTypeId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Төлбөрийн төрлийн  төрлийн бүртгэл устгах
        #endregion[] 

        #region[Бүтээгдэхүүний багцын бүртгэл]
        public Result Txn140246(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            try
            {
                res = IPos.DB.Main.DB202246(db, pagenumber, pagecount, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Посын бүртгэл жагсаалт мэдээлэл авах";

            }
        }
        public Result Txn140247(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();
            int _PackId = 0;
            _PackId = Static.ToInt(ri.ReceivedParam[0]);
            try
            {
                ret.ResultNo = 9110002;

                string pPackId = Static.ToStr(ri.ReceivedParam[0]);

                res = IPos.DB.Main.DB202247(db, pPackId);
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "pPackId";
                DataSet ds = new DataSet();
                ds.Tables.Add(res.Data.Tables[0].Copy());

                ret.Data = ds;
                ret.ResultNo = 0;
                return ret;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Посын бүртгэл дэлгэрэнгүй мэдээлэл авах";
                object[] newvalue = new object[2];
                //newvalue[0] = _PackId;
                //newvalue[1] = res.ResultNo;
                //lg.AddDetail("pPackId", newvalue[0].ToString(), "Посын бүртгэл дэлгэрэнгүй мэдээлэл авах", newvalue[1].ToString());
            }
        }
        public Result Txn140248(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[11];
                                //Static.ToStr(txtPackId.EditValue),
                                //Static.ToStr(txtName.EditValue),
                                //Static.ToStr(txtName2.EditValue),
                                //Static.ToStr(txtNote.EditValue),
                                //Static.ToDate(dtStartDate.EditValue),
                                //Static.ToDate(dtEndDate.EditValue),
                                //Static.ToInt(cboType.EditValue),
                                //Static.ToInt(cboStatus.EditValue),
                                //Static.ToStr(txtSalesUser.EditValue),
                                //Static.ToDate(dtSalesCreated.EditValue)

                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToDate(value[4]);
                obj[5] = Static.ToDate(value[5]);
                obj[6] = Static.ToInt(value[6]);
                obj[7] = Static.ToInt(value[7]);
                obj[8] = Static.ToStr(value[8]);
                obj[9] = Static.ToDate(value[9]);
                obj[10] = Static.ToDecimal(value[10]);

                res = IPos.DB.Main.DB202248(db, obj, ri.UserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Посын үндсэн бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn140249(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[11];
                //Static.ToStr(txtPackId.EditValue),
                //Static.ToStr(txtName.EditValue),
                //Static.ToStr(txtName2.EditValue),
                //Static.ToStr(txtNote.EditValue),
                //Static.ToDate(dtStartDate.EditValue),
                //Static.ToDate(dtEndDate.EditValue),
                //Static.ToInt(cboType.EditValue),
                //Static.ToInt(cboStatus.EditValue),
                //Static.ToStr(txtSalesUser.EditValue),
                //Static.ToDate(dtSalesCreated.EditValue)

                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToDate(value[4]);
                obj[5] = Static.ToDate(value[5]);
                obj[6] = Static.ToInt(value[6]);
                obj[7] = Static.ToInt(value[7]);
                obj[8] = Static.ToStr(value[8]);
                obj[9] = Static.ToDate(value[9]);
                obj[10] = Static.ToDecimal(value[10]);

                res = IPos.DB.Main.DB202249(db, obj, ri.UserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Посын бүртгэл засварлах";
            }
        }
        public Result Txn140250(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPackId = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202250(db, pPackId);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "бүртгэл устгах";
                    lg.AddDetail("pPackId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }

        public Result Txn140251(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string pPackID = "";
            try
            {
                pPackID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202251(db, pPackID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = " жагсаалт мэдээлэл авах";

            }
        }
        public Result Txn140252(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();                 
            try
            {
                ret.ResultNo = 9110002;

                string pPackId = Static.ToStr(ri.ReceivedParam[0]);
                string pProdID = Static.ToStr(ri.ReceivedParam[1]);
                int pProdType = Static.ToInt(ri.ReceivedParam[2]);

                res = IPos.DB.Main.DB202252(db, pPackId, pProdID, pProdType);
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "pPackId";
                DataSet ds = new DataSet();
                ds.Tables.Add(res.Data.Tables[0].Copy());

                ret.Data = ds;
                ret.ResultNo = 0;
                return ret;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                //lg.item.Desc = "Посын бүртгэл дэлгэрэнгүй мэдээлэл авах";
                //object[] newvalue = new object[2];
                ////newvalue[0] = _PackId;
                ////newvalue[1] = res.ResultNo;
                //lg.AddDetail("pPackId", newvalue[0].ToString(), "Посын бүртгэл дэлгэрэнгүй мэдээлэл авах", newvalue[1].ToString());
            }
        }
        public Result Txn140253(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[6];
                                //Static.ToStr(PackId.EditValue),
                                //Static.ToStr(txtProdId.EditValue),
                                //Static.ToInt(cboProdType.EditValue),
                                //Static.ToInt(numCount.EditValue),
                                //Static.ToInt(cboOptional.EditValue)        

                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToInt(value[2]);
                obj[3] = Static.ToInt(value[3]);
                obj[4] = Static.ToInt(value[4]);
                obj[5] = Static.ToInt(value[5]);                     

                res = IPos.DB.Main.DB202253(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn140254(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] OldValue = (object[])ri.ReceivedParam[0];
                object[] NewValue = (object[])ri.ReceivedParam[1];                     
                //object[] obj = new object[5];
                //obj[0] = Static.ToStr(NewValue[0]);
                //obj[1] = Static.ToStr(NewValue[1]);
                //obj[2] = Static.ToInt(NewValue[2]);
                //obj[3] = Static.ToInt(NewValue[3]);
                //obj[4] = Static.ToInt(NewValue[4]);

                //obj[0] = Static.ToStr(OldValue[0]);
                //obj[1] = Static.ToStr(OldValue[1]);
                //obj[2] = Static.ToInt(OldValue[2]);
                //obj[3] = Static.ToInt(OldValue[3]);
                //obj[4] = Static.ToInt(OldValue[4]);

                res = IPos.DB.Main.DB202254(db, OldValue, NewValue);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл засварлах";
            }
        }
        public Result Txn140255(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPackId = Static.ToStr(ri.ReceivedParam[0]);
                string pProdID = Static.ToStr(ri.ReceivedParam[1]);
                int pProdType = Static.ToInt(ri.ReceivedParam[2]);
                res = IPos.DB.Main.DB202255(db, pPackId, pProdID, pProdType);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "бүртгэл устгах";
                    lg.AddDetail("pPackId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }

        public Result Txn140256(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string pPackID = "";
            try
            {
                pPackID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202256(db, pPackID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = " жагсаалт мэдээлэл авах";

            }
        }
        public Result Txn140257(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();
            try
            {
                ret.ResultNo = 9110002;

                string pPackId = Static.ToStr(ri.ReceivedParam[0]);
                long pCustNo = Static.ToLong(ri.ReceivedParam[1]);                     

                res = IPos.DB.Main.DB202257(db, pPackId, pCustNo);
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "pPackId";
                DataSet ds = new DataSet();
                ds.Tables.Add(res.Data.Tables[0].Copy());

                ret.Data = ds;
                ret.ResultNo = 0;
                return ret;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "дэлгэрэнгүй мэдээлэл авах";
                object[] newvalue = new object[2];
                //newvalue[0] = _PackId;
                //newvalue[1] = res.ResultNo;
                lg.AddDetail("pPackId", newvalue[0].ToString(), "дэлгэрэнгүй мэдээлэл авах", newvalue[1].ToString());
            }
        }
        public Result Txn140258(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[2];    

                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);

                res = IPos.DB.Main.DB202258(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn140259(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPackID = Static.ToStr(ri.ReceivedParam[0]);
                long pOldCustNo = Static.ToLong(ri.ReceivedParam[1]);
                long pNewCustNo = Static.ToLong(ri.ReceivedParam[2]);
                res = IPos.DB.Main.DB202259(db, pPackID, pOldCustNo, pNewCustNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл засварлах";
            }
        }
        public Result Txn140260(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPackId = Static.ToStr(ri.ReceivedParam[0]);
                long pCustNo = Static.ToLong(ri.ReceivedParam[1]);
                     
                res = IPos.DB.Main.DB202260(db, pPackId, pCustNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "бүртгэл устгах";
                    lg.AddDetail("pPackId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }

        public Result Txn140261(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string pPackID = "";
            try
            {
                pPackID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202261(db, pPackID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = " жагсаалт мэдээлэл авах";

            }
        }
        public Result Txn140262(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();
            try
            {
                ret.ResultNo = 9110002;

                string pPackId = Static.ToStr(ri.ReceivedParam[0]);
                int pUserNo = Static.ToInt(ri.ReceivedParam[1]);

                res = IPos.DB.Main.DB202262(db, pPackId, pUserNo);
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "pPackId";
                DataSet ds = new DataSet();
                ds.Tables.Add(res.Data.Tables[0].Copy());

                ret.Data = ds;
                ret.ResultNo = 0;
                return ret;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "дэлгэрэнгүй мэдээлэл авах";
                object[] newvalue = new object[2];
                //newvalue[0] = _PackId;
                //newvalue[1] = res.ResultNo;
                lg.AddDetail("pPackId", newvalue[0].ToString(), "дэлгэрэнгүй мэдээлэл авах", newvalue[1].ToString());
            }
        }
        public Result Txn140263(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[2];

                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToInt(value[1]);

                res = IPos.DB.Main.DB202263(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        public Result Txn140264(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPackID = Static.ToStr(ri.ReceivedParam[0]);
                int pOldUserNo = Static.ToInt(ri.ReceivedParam[1]);
                int pNewUserNo = Static.ToInt(ri.ReceivedParam[2]);
                res = IPos.DB.Main.DB202264(db, pPackID, pOldUserNo, pNewUserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл засварлах";
            }
        }
        public Result Txn140265(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPackId = Static.ToStr(ri.ReceivedParam[0]);
                int pUserNo = Static.ToInt(ri.ReceivedParam[1]);

                res = IPos.DB.Main.DB202265(db, pPackId, pUserNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "бүртгэл устгах";
                    lg.AddDetail("pPackId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
        
        #region[Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр.]
        //Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр жагсаалт мэдээлэл авах
        public Result Txn140296(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202296(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }
        //Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр дэлгэрэнгүй мэдээлэл авах
        public Result Txn140297(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pParamCode = Static.ToStr(ri.ReceivedParam[0]);
                string pNodeCode = Static.ToStr(ri.ReceivedParam[1]);

                res = IPos.DB.Main.DB202297(db, pParamCode, pNodeCode);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Төлбөрийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pTyId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр шинээр нэмэх
        public Result Txn140298(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB202298(db, value);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        //Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр засварлах
        public Result Txn140299(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];

                string pOldParentID = ri.ReceivedParam[1].ToString();
                string pOldItemID = ri.ReceivedParam[2].ToString();

                res = IPos.DB.Main.DB202299(db, pOldParentID, pOldItemID, value);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл засварлах";
            }
        }
        //Бүтээгдэхүүний Бүлэглэлийн Бүртгэл - Мод бүтцээр устгах
        public Result Txn140300(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pParentCode = Static.ToStr(ri.ReceivedParam[0]);
                string pNodeCode = Static.ToStr(ri.ReceivedParam[1]);


                res = IPos.DB.Main.DB202300(db, pParentCode, pNodeCode);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Төлбөрийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pTypeId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    
        #endregion[]

        #region[Эвдэрлийн төрлийн бүртгэл]
        public Result Txn140326(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202326(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //Эвдэрлийн төрлийн бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140327(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pDamageType = Static.ToStr(ri.ReceivedParam[0]);

                res = IPos.DB.Main.DB202327(db, pDamageType);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Эвдэрлийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140328(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[3];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToInt(value[2]);

                res = IPos.DB.Main.DB202328(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Эвдэрлийн төрлийн бүртгэл шинээр нэмэх
        public Result Txn140329(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[3];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToInt(value[2]);
                     
                res = IPos.DB.Main.DB202329(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл засварлах";
            }
        }    //Эвдэрлийн төрлийн бүртгэл засварлах
        public Result Txn140330(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pDamageType = Static.ToStr(ri.ReceivedParam[0]);
                     
                res = IPos.DB.Main.DB202330(db, pDamageType);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Эвдэрлийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Эвдэрлийн төрлийн бүртгэл устгах
        #endregion[]

        #region[Барьцааны төрлийн бүртгэл]
        public Result Txn140331(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
                @"SELECT TYPEID, TYPENAME, MASKVALUE, MASKTYPE, ORDERNO
FROM PAPLEDGETYPE
Order by ORDERNO";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202331", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //Барьцааны төрлийн бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140332(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT TYPEID, TYPENAME, MASKVALUE, MASKTYPE, ORDERNO
FROM PAPLEDGETYPE
WHERE TYPEID = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202332", new object[]{ri.ReceivedParam[0]});
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Барьцааны төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140333(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToInt(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);

                string sql =
@"INSERT INTO PAPLEDGETYPE(TYPEID, TYPENAME, MASKVALUE, MASKTYPE, ORDERNO)
VALUES(:1, :2, :3, :4, :5)";

                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202333", obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Барьцааны төрлийн бүртгэл шинээр нэмэх
        public Result Txn140334(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToStr(value[1]);
                obj[2] = Static.ToInt(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);

                string sql =
@"UPDATE PAPLEDGETYPE SET
TYPENAME=:2, MASKVALUE=:3, MASKTYPE=:4, ORDERNO=:5
WHERE TYPEID = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202334", obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл засварлах";
            }
        }    //Барьцааны төрлийн бүртгэл засварлах
        public Result Txn140335(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM PAPLEDGETYPE WHERE TYPEID = :1";
                res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn140335",ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Эвдэрлийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Барьцааны төрлийн бүртгэл устгах
        #endregion[] 

        #region[Тооцоолол хийх матриц бүртгэл]
        public Result Txn140336(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
                @"SELECT MASTERID, MASTERTYPE, NAME, NAME2, LISTORDER
FROM REBATEMASTER
Order by LISTORDER";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202336", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //Тооцоолол хийх матриц бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140337(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =   
@"SELECT MASTERID, MASTERTYPE, NAME, NAME2, LISTORDER
FROM REBATEMASTER
WHERE MASTERID = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202337", new object[] { ri.ReceivedParam[0] });
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Тооцоолол хийх матриц бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140338(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToInt(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);
                     
                string sql =
@"INSERT INTO REBATEMASTER(MASTERID, MASTERTYPE, NAME, NAME2, LISTORDER)
VALUES(:1, :2, :3, :4, :5)";

                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202338", obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //Тооцоолол хийх матриц бүртгэл шинээр нэмэх
        public Result Txn140339(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToInt(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);

                string sql =
@"UPDATE REBATEMASTER SET
MASTERTYPE=:2, NAME=:3, NAME2=:4, LISTORDER=:5
WHERE MASTERID = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202339", obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл засварлах";
            }
        }    //Тооцоолол хийх матриц бүртгэл засварлах
        public Result Txn140340(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM REBATEMASTER WHERE MASTERID = :1";
                res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn140340", ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Эвдэрлийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //Тооцоолол хийх матриц бүртгэл устгах
        #endregion[]  

        #region[ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл]
        public Result Txn140341(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
                @"SELECT MASTERID, PRODTYPE, PRODNO, CALCTYPE, CALCAMOUNT
FROM REBATEDETAIL
Order by CALCAMOUNT";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202341", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140342(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
@"SELECT MASTERID, PRODTYPE, PRODNO, CALCTYPE, CALCAMOUNT
FROM REBATEDETAIL
WHERE MASTERID = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202342", new object[] { ri.ReceivedParam[0] });
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140343(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToInt(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);                     
                string sql =
@"INSERT INTO REBATEDETAIL(MASTERID, PRODTYPE, PRODNO, CALCTYPE, CALCAMOUNT)
VALUES(:1, :2, :3, :4, :5)";

                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202343", obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл шинээр нэмэх
        public Result Txn140344(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToInt(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToStr(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToInt(value[4]);
                     
                string sql =
@"UPDATE REBATEDETAIL SET
PRODTYPE=:2, PRODNO=:3, CALCTYPE=:4, CALCAMOUNT=:5
WHERE MASTERID = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202344", obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл засварлах";
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл засварлах
        public Result Txn140345(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
@"DELETE FROM REBATEDETAIL WHERE MASTERID = :1";
                res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn140345", ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Эвдэрлийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл устгах
        #endregion[] 

        #region[НӨХЦӨЛИЙН ХҮСНЭГТ]
        public Result Txn140346(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string sql =
                @"SELECT  FORMULAID, STATUS, BEGINDATE, ENDDATE, SQLFUNCTION
FROM REBATEFORMULA
Order by FORMULAID";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202346", null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140347(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {                     
                string sql =
@"SELECT FORMULAID, STATUS, BEGINDATE, ENDDATE, SQLFUNCTION
FROM REBATEFORMULA
WHERE FORMULAID = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "DB202347", new object[] { ri.ReceivedParam[0] });
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140348(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToInt(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToDate(value[2]);
                obj[3] = Static.ToDate(value[3]);
                obj[4] = Static.ToStr(value[4]);
                     

                string sql =
@"INSERT INTO REBATEFORMULA(FORMULAID, STATUS, BEGINDATE, ENDDATE, SQLFUNCTION)
VALUES(:1, :2, :3, :4, :5)";

                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "DB202348", obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл шинээр нэмэх
        public Result Txn140349(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[5];
                obj[0] = Static.ToInt(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToDate(value[2]);
                obj[3] = Static.ToDate(value[3]);
                obj[4] = Static.ToStr(value[4]);
                     
                string sql =
@"UPDATE REBATEFORMULA SET
STATUS=:2, BEGINDATE=:3, ENDDATE=:4, SQLFUNCTION=:5
WHERE FORMULAID = :1";

                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "DB202349", obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл засварлах";
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл засварлах
        public Result Txn140350(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                     
                string sql =
@"DELETE FROM REBATEFORMULA WHERE FORMULAID = :1";
                res = db.ExecuteQuery("core", sql, enumCommandType.DELETE, "Txn140350", ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Эвдэрлийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Эвдэрлийн төрлийн бүртгэл устгах";
                    lg.AddDetail("pDamageType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //ТООЦООЛОЛ ХИЙХ МАТРИЦ БҮТЭЭГДЭХҮҮНИЙ ЗАДАРГАА бүртгэл устгах
        #endregion[] 

        #region[xls ээс унших тайлангийн параметрүүд]
        public Result Txn140351(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {                     
                res = IPos.DB.Main.DB202351(db, "");
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "xls тайлангийн параметрийн жагсаалт мэдээлэл авах";
            }
        }    //xls тайлангийн параметрийн жагсаалт мэдээлэл авах
        public Result Txn140352(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
        Result  res = new Result();
            try
            {
                string pViewName = Static.ToStr(ri.ReceivedParam[0]);
                int pParamID = Static.ToInt(ri.ReceivedParam[1]);

                res = IPos.DB.Main.DB202352(db, pViewName, pParamID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "xls тайлангийн параметрийн дэлгэрэнгүй мэдээлэл авах";
                    lg.AddDetail("pViewName", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //xls тайлангийн параметрийн дэлгэрэнгүй мэдээлэл авах
        public Result Txn140353(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[11];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToInt(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToStr(value[4]);
                obj[5] = Static.ToStr(value[5]);
                obj[6] = Static.ToStr(value[6]);
                obj[7] = Static.ToStr(value[7]);
                obj[8] = Static.ToStr(value[8]);
                obj[9] = Static.ToStr(value[9]);
                obj[10] = Static.ToStr(value[10]);


                res = IPos.DB.Main.DB202353(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "xls тайлангийн параметр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }    //xls тайлангийн параметр нэмэх
        public Result Txn140354(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];
                object[] obj = new object[11];
                obj[0] = Static.ToStr(value[0]);
                obj[1] = Static.ToInt(value[1]);
                obj[2] = Static.ToInt(value[2]);
                obj[3] = Static.ToStr(value[3]);
                obj[4] = Static.ToStr(value[4]);
                obj[5] = Static.ToStr(value[5]);
                obj[6] = Static.ToStr(value[6]);
                obj[7] = Static.ToStr(value[7]);
                obj[8] = Static.ToStr(value[8]);
                obj[9] = Static.ToStr(value[9]);
                obj[10] = Static.ToStr(value[10]);

                res = IPos.DB.Main.DB202354(db,obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "xls тайлангийн параметр засварлах";
            }
        }    //xls тайлангийн параметр засварлах
        public Result Txn140355(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pViewName = Static.ToStr(ri.ReceivedParam[0]);
                int pParamID = Static.ToInt(ri.ReceivedParam[1]);


                res = IPos.DB.Main.DB202355(db, pViewName, pParamID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "xls тайлангийн параметр устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "xls тайлангийн параметр устгах";
                    lg.AddDetail("pViewName", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }    //xls тайлангийн параметр устгах
        #endregion[]
        #region[TagMain]
        //Тагын жагсаалт мэдээлэл авах
        public Result Txn140356(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202331(db);
                return res;
            }
            catch(Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.Source + ex.StackTrace;
                return res;
            }

        }    
        //Тагын дэлгэрэнгүй мэдээлэл авах
        public Result Txn140357(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202332(db, ri.ReceivedParam[0].ToString());
                return res;
            }
            catch (Exception ex)
            {
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.StackTrace + ex.Source;
                res.ResultNo = 911002;
                return res;
            }
        }    
        //Таг нэмэх
        public Result Txn140358(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202333(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.Source + ex.StackTrace;
                return res;
            }
        }    
        //Таг засварлах
        public Result Txn140359(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202334(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.Source + ex.StackTrace;
                return res;
            }
                     
        }
        //Таг устгах
        public Result Txn140360(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202335(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911002;
                res.ResultDesc = "Програмруу нэвтэрхэд алдаа гарлаа" + ex.Message + ex.Source + ex.StackTrace;
                return res;
            }
        }    
        #endregion[]

        #region[Бүлэглэлийн Тухайн мөчрийн бүртгэл]
        //Бүлэглэлийн Тухайн мөчрийн бүртгэл жагсаалт мэдээлэл авах
        public Result Txn140361(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202356(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бүлэглэлийн Тухайн мөчрийн бүртгэл жагсаалт мэдээлэл авах";
            }
        }
        //Бүлэглэлийн Тухайн мөчрийн бүртгэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140362(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202357(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                if (res.ResultNo == 0)
                {
                    lg.item.Desc = "Бүлэглэлийн Тухайн мөчрийн бүртгэл дэлгэрэнгүй мэдээлэл авах";
                }
            }
        }
        //Бүлэглэлийн Тухайн мөчрийн бүртгэл шинээр нэмэх
        public Result Txn140363(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202358(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Бүлэглэлийн Тухайн мөчрийн бүртгэл шинээр нэмэх";
                if (res.ResultNo == 0)
                {

                }
            }
        }
        //Бүлэглэлийн Тухайн мөчрийн бүртгэл засварлах
        public Result Txn140364(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] value = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB202359(db, value);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Бүлэглэлийн Тухайн мөчрийн бүртгэл засварлах";
            }
        }
        //Бүлэглэлийн Тухайн мөчрийн бүртгэл устгах
        public Result Txn140365(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202360(db, Static.ToStr(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн бүртгэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Бүлэглэлийн Тухайн мөчрийн бүртгэл устгах";
                    lg.AddDetail("pTypeId", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion[]

        #region [ POS дээрх төлбөрийн хэрэгсэлийн бүртгэл ]
        //POS төлбөрийн хэрэгсэл жагсаалт авах
        public Result Txn111113(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB203019(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "POS төлбөрийн хэрэгсэл жагсаалт авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PosPayType", "", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //POS төлбөрийн хэрэгсэл дэлгэрэнгүй мэдээлэл авах
        public Result Txn111114(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPosNo = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB203020(db, pPosNo);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "POS төлбөрийн хэрэгсэл дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PosPayType", "pPosNo", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //POS төлбөрийн хэрэгсэл шинээр нэмэх
        public Result Txn111115(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[2];
                obj = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB203021(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "POS төлбөрийн хэрэгсэл шинээр нэмэх ";
                //if (res.ResultNo == 0)
                //{
                //    object[] NewValue = (object[])ri.ReceivedParam[0];
                //    object[] FieldName = { "userno" };
                //    for (int i = 0; i < FieldName.Length; i++)
                //    {
                //        lg.AddDetail("userno", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                //    }
                //}
            }
        }
        //POS төлбөрийн хэрэгсэл засварлах
        public Result Txn111116(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string pPOSNo = Static.ToStr(ri.ReceivedParam[0]);
                string pOldTypeID = Static.ToStr(ri.ReceivedParam[1]);
                string pNewTypeID = Static.ToStr(ri.ReceivedParam[2]);
                res = IPos.DB.Main.DB203022(db, pPOSNo, pOldTypeID, pNewTypeID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "POS төлбөрийн хэрэгсэл засварлах";
                //if (res.ResultNo == 0)
                //{
                //    object[] NewValue = (object[])ri.ReceivedParam[0];
                //    object[] OldValue = (object[])ri.ReceivedParam[1];
                //    object[] FieldName = { "citycode", "distcode", "subdistcode", "apartment", "note", "addrcurrent", "postdate", "userno", "seqno" };
                //    for (int i = 0; i < FieldName.Length; i++)
                //    {
                //        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("agentaddr", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                //    }
                //}
            }
        }
        //POS төлбөрийн хэрэгсэл устгах
        public Result Txn111117(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string pPOSNo;
            string pTypeID = "";
            object[] obj = new object[2];
            try
            {
                pPOSNo = Static.ToStr(ri.ReceivedParam[0]);
                pTypeID = Static.ToStr(ri.ReceivedParam[1]);
                res = IPos.DB.Main.DB203023(db, pPOSNo, pTypeID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "POS төлбөрийн хэрэгсэл устгах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("pPOSNo", "POSno", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion

        // Насний бүртгэл
        #region[Нас]
        public Result SelectAge(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB22801(db, 0, 0, null);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хэлний жагсаалт авах";
            }
        }
        public Result AddAge(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB22803(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэлний жагсаалт нэмэх";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] FieldName = (object[])ri.ReceivedParam[1];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        lg.AddDetail("Language", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                    }
                }
            }
        }
        public Result DeleteAge(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB22805(db, Static.ToLong(ri.ReceivedParam[0]));

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хэлний жагсаалт устгах";
                if (res.ResultNo == 0)
                {
                    lg.item.Key1 = "Хэлний код";
                    lg.AddDetail("Language", "LanguageCode", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        public Result EditAge(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB22804(db, (object[])ri.ReceivedParam[0]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Хэлний жагсаалт засварлах";
                if (res.ResultNo == 0)
                {
                    object[] NewValue = (object[])ri.ReceivedParam[0];
                    object[] OldValue = (object[])ri.ReceivedParam[1];
                    object[] FieldName = (object[])ri.ReceivedParam[2];
                    for (int i = 0; i < FieldName.Length; i++)
                    {
                        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("Language", FieldName[i].ToString(), OldValue[i].ToString(), NewValue[i].ToString());
                    }
                }
            }
        }
        #endregion

        #region [ Үнийн төрөл ]
        //Үнийн төрөл жагсаалт авах
        public Result Txn140416(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = IPos.DB.Main.DB202336(db);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үнийн төрөл жагсаалт авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PAPriceType", "ALL", lg.item.Desc, "ALL");
                }
            }
        }
        //Үнийн төрөл дэлгэрэнгүй мэдээлэл авах
        public Result Txn140417(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                string PriceTypeID = Static.ToStr(ri.ReceivedParam[0]);
                res = IPos.DB.Main.DB202337(db, PriceTypeID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үнийн төрөл дэлгэрэнгүй мэдээлэл авах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PAPriceType", "PriceTypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        //Үнийн төрөл шинээр нэмэх
        public Result Txn140418(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[2];
                obj = (object[])ri.ReceivedParam[0];

                res = IPos.DB.Main.DB202338(db, obj);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үнийн төрөл шинээр нэмэх ";
                //if (res.ResultNo == 0)
                //{
                //    object[] NewValue = (object[])ri.ReceivedParam[0];
                //    object[] FieldName = { "userno" };
                //    for (int i = 0; i < FieldName.Length; i++)
                //    {
                //        lg.AddDetail("userno", FieldName[i].ToString(), lg.item.Desc, Static.ToStr(NewValue[i]));
                //    }
                //}
            }
        }
        //Үнийн төрөл засварлах
        public Result Txn140419(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                //string pPOSNo = Static.ToStr(ri.ReceivedParam[0]);
                //string pOldTypeID = Static.ToStr(ri.ReceivedParam[1]);
                //string pNewTypeID = Static.ToStr(ri.ReceivedParam[2]);
                res = IPos.DB.Main.DB202339(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Үнийн төрөл засварлах";
                //if (res.ResultNo == 0)
                //{
                //    object[] NewValue = (object[])ri.ReceivedParam[0];
                //    object[] OldValue = (object[])ri.ReceivedParam[1];
                //    object[] FieldName = { "citycode", "distcode", "subdistcode", "apartment", "note", "addrcurrent", "postdate", "userno", "seqno" };
                //    for (int i = 0; i < FieldName.Length; i++)
                //    {
                //        if (OldValue[i].ToString() != NewValue[i].ToString()) lg.AddDetail("agentaddr", FieldName[i].ToString(), OldValue[i].ToString(), Static.ToStr(NewValue[i]));
                //    }
                //}
            }
        }
        //Үнийн төрөл устгах
        public Result Txn140420(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string PriceTypeID = "";
            try
            {
                PriceTypeID = Static.ToStr(ri.ReceivedParam[0]);

                res = IPos.DB.Main.DB202340(db, PriceTypeID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Үнийн төрөл устгах";
                if (res.ResultNo == 0)
                {
                    lg.AddDetail("PAPriceType", "PriceTypeID", lg.item.Desc, ri.ReceivedParam[0].ToString());
                }
            }
        }
        #endregion
    }
}