using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using EServ.Shared;
using DevExpress.XtraGrid.Views.Grid;
using ISM.Template;
using DevExpress.Data;
using DevExpress.XtraGrid.GroupSummaryEditor;



namespace InfoPos.List
{
    public partial class AccountTxnList : Form
    {
        int discontinuedProductsCount;
        decimal customSum;
        #region [ Дамжуулах парамерт ]
        private Core.Core _core;
        private int Account_Type;
        private DataRow Account_DR;
        private long _ID;
        int PrivNo = 231015;
        private object[] _obj;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Үндсэн ]
        public AccountTxnList(Core.Core core, int pType, long pID, DataRow DR, object[] param)
        {
            InitializeComponent();
            _core = core;
            Account_Type = pType;
            Account_DR = DR;
            _ID = pID;
            _obj = param;
       
            Init();

            ucAccountTxnList.Resource = _core.Resource;
        }
        #endregion
        #region [ PageLoad ]
        private void AccountTxnList_Load(object sender, EventArgs e)
        {
            InitData();
            InitData1();
         //   ucAccountTxnList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucAccountTxnList_EventFindPaging);
        
         
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucAccountTxnList.Resource = _core.Resource;
            ucAccountTxnList.OnEventFindPaging(0);



        
        }
        #endregion
        #region [ InitData ]
        private Result InitData()
        {



            Result res = new Result();

            switch (Account_Type)
            {
                case 0:
                   // Init_BAC(Account_DR);
                    this.Text = "Байгууллагын дансны гүйлгээ жагсаалт";
                    break;
                case 1:
               //     Init_CON(Account_DR);
                    this.Text = "Балансын гадуурх дансны жагсаалт";
                    break;
                case 2:
                   // Init_FA(Account_DR);
                    this.Text = "Үндсэн хөрөнгийн гүйлгээний жагсаалт";
                    break;
                case 3:
                  //  Init_INV(Account_DR);
                    this.Text = "Бараа материалын жагсаалт";
                    break;
            }
            return res;
        }

        private Result InitData1()
        {



            Result res = new Result();

            switch (Account_Type)
            {
                case 0:
                    // Init_BAC(Account_DR);
                    InitBac();
                    break;
                case 1:
                    //     Init_CON(Account_DR);
                    InitCon();
                    this.Text = "Балансын гадуурх дансны жагсаалт";
                    break;
                case 2:
                    // Init_FA(Account_DR);
                    InitFa();
                    this.Text = "Үндсэн хөрөнгийн гүйлгээний жагсаалт";
                    break;
                case 3:
                    //  Init_INV(Account_DR);
                    InitInv();
                    this.Text = "Бараа материалын жагсаалт";
                    break;
            }
            return res;
        }

        #endregion

        private Result InitDictionary()
        {

            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";
            DictUtility.PrivNo = PrivNo;
            try
            {
                string[] names = new string[] { "BRANCH", "CURRENCY", "USERS", "BACPRODUCT", "FATYPEID", "INVTYPEID" };
                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д салбарын мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucAccountTxnList.FindItemSetList("BRANCHNO", DT, "BRANCH", "NAME", "", new int[] { 2, 3, 4 });
                }

                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д валютын мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucAccountTxnList.FindItemSetList("CURCODE", DT, "CURRENCY", "NAME");
                    ucAccountTxnList.FindItemSetList("CONTCURRCODE", DT, "CURRENCY", "NAME");
                    ucAccountTxnList.FindItemSetList("TXNCURRCODE", DT, "CURRENCY", "NAME");
                }

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д валютын мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucAccountTxnList.FindItemSetList("USERNO", DT, "userno", "userlname");
                }
                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "Dictionary-д Бүтээгдхүүний мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucAccountTxnList.FindItemSetList("PRODCODE", DT, "PRODCODE", "NAME");
                    //ucBacProductList.FindItemSetList("ProdCode", DT, "PRODCODE","NAME");
                }
                DT = (DataTable)Tables[4];
                if (DT == null)
                {
                    msg = "Dictionary-д үндсэн хөрөнгийн төрлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucAccountTxnList.FindItemSetList("FATYPEID", DT, "FATYPEID", "NAME");
                    //ucBacProductList.FindItemSetList("ProdCode", DT, "PRODCODE","NAME");
                }
                DT = (DataTable)Tables[5];
                if (DT == null)
                {
                    msg = "Dictionary-д БМ-ын төрлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucAccountTxnList.FindItemSetList("INVTYPEID", DT, "INVTYPEID", "NAME");
                    //ucBacProductList.FindItemSetList("ProdCode", DT, "PRODCODE","NAME");
                }


                ucAccountTxnList.FindItemSetList("CORR", 0, "Буцаагдаагүй");
                ucAccountTxnList.FindItemSetList("CORR",1, "Буцаагдсан");

                ucAccountTxnList.FindItemSetList("M", 0, "Үндсэн гүйлгээ биш");
                ucAccountTxnList.FindItemSetList("M", 1, "Үндсэн гүйлгээ");

                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                res.ResultNo = 1;
                this.Close();
            }
            return res;
        }


        #region [ Байгууллагын дансны жагсаалт ]
        private void InitBac()
        {
          



            /*"B.JRNO like", "B.PARTJRNO like", "B.SUBJRNO like","B.TXNDATE like", "B.TXNENTRY", "B.POSTDATE like", "B.ACCOUNTNO like", "B.BRANCHNO like","B.PRODCODE like",
"B.USERNO like","B.CURCODE", "B.CONTACCCOUNTNO like",
"B.CONTCURRCODE"
             */
            

            ucAccountTxnList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucAccountTxnList_EventFindPaging);
          
            ucAccountTxnList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(LoadBACData);
            /*"B.TXNDATE >=", "B.TXNDATE <=","B.CORR","B.TXNENTRY", "B.ACCOUNTNO like","B.PRODCODE like",
"B.USERNO","B.AMOUNT like","B.BALANCE like", "B.CONTACCCOUNTNO like","B.CONTAMOUNT like", "B.BASEAMOUNT like", "%B.DESCRIPTION like","B.M",*/


            //      ucBacAccountList.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);
            //ucAccountTxnList.gridView1.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(gridView1_CustomSummaryCalculate);
            //ucAccountTxnList.FieldFindAdd("JRNO", "Журналын дугаар", typeof(long), "");
            //ucAccountTxnList.FieldFindAdd("PARTJRNO", "Гүйлгээний хэсгийн журнал дугаар", typeof(long), "");
            //ucAccountTxnList.FieldFindAdd("SUBJRNO", "Дэд журналын дугаар", typeof(long), "");
       
                    ucAccountTxnList.FieldFindAdd("TXNDATE", "Гүйлгээний эхлэх огноо", typeof(DateTime), "");
                    ucAccountTxnList.FieldFindAdd("TO_CHAR(TXNDATE)", "Гүйлгээний дуусах огноо", typeof(DateTime), "");
                    ucAccountTxnList.FieldFindAdd("CORR", "Гүйлгээ буцаагдсан эсэх", typeof(ArrayList), "");
         
                    ucAccountTxnList.FieldFindAdd("TXNENTRY", "Гүйлгээний оролт", typeof(ArrayList), "");
          
                ucAccountTxnList.FindItemAdd("AccountNo", "Дансны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
         
                ucAccountTxnList.FieldFindAdd("PRODCODE", "Бүтээгдхүүний дугаар", typeof(ArrayList), "");
                ucAccountTxnList.FieldFindAdd("USERNO", "Гүйлгээ хийсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");
                ucAccountTxnList.FindItemAdd("AMOUNT", "Гүйлгээний дүн", "", DynamicParameterType.Decimal, false, "d", "");
                ucAccountTxnList.FindItemAdd("BALANCE", "Гүйлгээний дараах үлдэгдэл", "", DynamicParameterType.Decimal, false, "d", "");
                ucAccountTxnList.FindItemAdd("CONTACCCOUNTNO", "Харьцсан данс", "", DynamicParameterType.Decimal, false, "d", "");
                ucAccountTxnList.FindItemAdd("CONTAMOUNT", "Харьцсан дансны дүн", "", DynamicParameterType.Decimal, false, "d", "");
                ucAccountTxnList.FindItemAdd("BASEAMOUNT", "Харьцсан дансны суурь дүн", "", DynamicParameterType.Decimal, false, "d", "");
                ucAccountTxnList.FieldFindAdd("DESCRIPTION", "Гүйлгээний утга", typeof(string), "");
                ucAccountTxnList.FieldFindAdd("M", "Үндсэн гүйлгээ мөн эсэх ", typeof(ArrayList), "");
                ucAccountTxnList.FieldFindRefresh();
                InitDictionary();
                ucAccountTxnList.VisibleFind = true;
                ucAccountTxnList.FindItemSetValue("TXNDATE", _core.TxnDate);
                ucAccountTxnList.FindItemSetValue("TO_CHAR(TXNDATE)", _core.TxnDate);
                ucAccountTxnList.FindItemSetValue("CORR", "0");
         



        }

        

        
        
        //public void Init_BAC(DataRow DR)
        //{
        //    object[] obj = new object[2];
        //    Result res;
        //    try
        //    {
        //        obj[0] = Static.ToInt(Account_Type);
        //        obj[1] = Static.ToLong(_ID);
        //        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 231015, 231015, obj);
        //        if (res.ResultNo == 0)
        //        {
        //            try
        //            {
        //                ucAccountTxnList.DataSource = res.Data.Tables[0];
        //                LoadBACData();
        //            }
        //            catch(Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void Init()
        {



           
        }

        private void LoadBACData()
        {        
             /*B.JRNO, B.PARTJRNO, B.SUBJRNO, B.SEQNO, B.TXNDATE, B.TXNENTRY, B.POSTDATE, B.ACCOUNTNO, B.BRANCHNO, B.PRODCODE,
B.USERNO, B.HOSTNAME, B.HOSTIP, B.HOSTMAC, B.TXNCODE, B.AMOUNT, B.RATE, B.CURCODE, B.BALANCE, B.CONTACCCOUNTNO,
B.CONTCURRCODE, B.CONTRATE, B.CONTAMOUNT, B.BASEAMOUNT, B.DESCRIPTION, B.CORR, B.ISCASH, B.SUPERVISOR, B.FLAG, B.M,
B.GroupTxnCode*/

       
            FormUtility.RestoreStateGrid(appname, formname, ref ucAccountTxnList.gridView1);
            ucAccountTxnList.FieldLinkSetColumnCaption(0, "Журналын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(1, "Гүйлгээний хэсгийн журнал дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(2, "Дэд журналын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(3, "Дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(4, "Гүйлгээний огноо");
            ucAccountTxnList.FieldLinkSetColumnCaption(5, "Гүйлгээний оролт");
            ucAccountTxnList.FieldLinkSetColumnCaption(6, "Гүйлгээний огноо, цаг минут");
            ucAccountTxnList.FieldLinkSetColumnCaption(7, "Дансны дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(8, "Салбарын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(9, "Бүтээгдэхүүний дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(10, "Гүйлгээ хийсэн хэрэглэгчийн дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(11, "Компьютерийн нэр");
            ucAccountTxnList.FieldLinkSetColumnCaption(12, "Компьютерийн IP");
            ucAccountTxnList.FieldLinkSetColumnCaption(13, "Комьютерийн MAC");
            ucAccountTxnList.FieldLinkSetColumnCaption(14, "Гүйлгээний код");
            ucAccountTxnList.FieldLinkSetColumnCaption(15, "Гүйлгээний дүн");
            ucAccountTxnList.gridView1.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ucAccountTxnList.gridView1.Columns[15].DisplayFormat.FormatString = "{0:n2}";
            ucAccountTxnList.FieldLinkSetColumnCaption(16, "Гүйлгээний валютын ханш");
            ucAccountTxnList.FieldLinkSetColumnCaption(17, "Гүйлгээний валют");
            ucAccountTxnList.FieldLinkSetColumnCaption(18, "гүйлгээний дараах үлдэгдэл");
            ucAccountTxnList.gridView1.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ucAccountTxnList.gridView1.Columns[18].DisplayFormat.FormatString = "{0:n2}";
            ucAccountTxnList.FieldLinkSetColumnCaption(19, "Харьцсан данс");
            ucAccountTxnList.FieldLinkSetColumnCaption(20, "Харьцсан дансны валют");
            ucAccountTxnList.FieldLinkSetColumnCaption(21, "Харьцасан валютын ханш");
            ucAccountTxnList.FieldLinkSetColumnCaption(22, "Харьцсан дансны дүн");
            ucAccountTxnList.gridView1.Columns[22].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ucAccountTxnList.gridView1.Columns[22].DisplayFormat.FormatString = "{0:n2}";
            ucAccountTxnList.FieldLinkSetColumnCaption(23, "Харьцсан дансны суурь дүн");
            ucAccountTxnList.FieldLinkSetColumnCaption(24, "Гүйлгээний утга");
            ucAccountTxnList.FieldLinkSetColumnCaption(25, "Буцаагдсан эсэх");
            ucAccountTxnList.FieldLinkSetColumnCaption(26, "Бэлэн гүйлгээ эсэх");
            ucAccountTxnList.FieldLinkSetColumnCaption(27, "Зөвшөөрсөн теллерийн дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(28, "Ерөнхий дэвтэрлүү татагдсан эсэх");
            ucAccountTxnList.FieldLinkSetColumnCaption(29, "Үндсэн гүйлгээ мөн эсэх");
            ucAccountTxnList.FieldLinkSetColumnCaption(30, "Групп гүйлгээний код");
            ucAccountTxnList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucAccountTxnList.gridView1.Columns[18].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ucAccountTxnList.gridView1.Columns[18].SummaryItem.DisplayFormat = "Нийт:{0:C2}";
            ucAccountTxnList.gridView1.Columns[18].SummaryItem.Tag = 1;
            (ucAccountTxnList.gridView1.Columns[18].View as GridView).OptionsView.ShowFooter = true;


            // Create and setup the first summary item.
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "JRNO";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            item.DisplayFormat = "Нийт гүйлгээний тоо {0:c2}";
            ucAccountTxnList.gridView1.GroupSummary.Add(item);
            // Create and setup the second summary item.
            GridGroupSummaryItem item1 = new GridGroupSummaryItem();
            item1.FieldName = "BALANCE";
            item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item1.DisplayFormat = "Нийт б/үлдэгдэл {0:c2}";
            item1.ShowInGroupColumnFooter = ucAccountTxnList.gridView1.Columns["BALANCE"];
            ucAccountTxnList.gridView1.GroupSummary.Add(item1);

     



            FormUtility.RestoreStateGrid(appname, formname, ref ucAccountTxnList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucAccountTxnList.ucParameterPanel1.vGridControl1, ref ucAccountTxnList.groupControl1);

         
           
        }
        #endregion




        #region [ Балансын гадуурх дансны жагсаалт ]

        private void InitCon()
        {
            /*"B.JRNO like", "B.PARTJRNO like", "B.SUBJRNO like","B.TXNDATE like", "B.TXNENTRY", "B.POSTDATE like", "B.ACCOUNTNO like", "B.BRANCHNO like","B.PRODCODE like",
"B.USERNO like","B.CURCODE", "B.CONTACCCOUNTNO like",
"B.CONTCURRCODE"
             */
            ucAccountTxnList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucAccountTxnList_EventFindPaging);

            ucAccountTxnList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(LoadCONData);

            /*"C.JRNO like", "C.PARTJRNO like", "C.SUBJRNO like", "C.TXNENTRY like", "C.TXNDATE like", "C.POSTDATE like", 
                        "C.ACCOUNTNO like", "C.BRANCHNO like", "C.PRODCODE like", 
"C.USERNO like", "C.CURCODE like","C.CONTACCCOUNTNO like", 
"C.CONTCURRCODE like"*/

            ucAccountTxnList.FieldFindAdd("TXNDATE", "Гүйлгээний эхлэх огноо", typeof(DateTime), "");
            ucAccountTxnList.FieldFindAdd("TO_CHAR(TXNDATE)", "Гүйлгээний дуусах огноо", typeof(DateTime), "");
            ucAccountTxnList.FieldFindAdd("CORR", "Гүйлгээ буцаагдсан эсэх", typeof(ArrayList), "");

            ucAccountTxnList.FieldFindAdd("TXNENTRY", "Гүйлгээний оролт", typeof(ArrayList), "");

            ucAccountTxnList.FindItemAdd("AccountNo", "Дансны дугаар", "", DynamicParameterType.Decimal, false, "d", "");

            ucAccountTxnList.FieldFindAdd("PRODCODE", "Бүтээгдхүүний дугаар", typeof(ArrayList), "");
            ucAccountTxnList.FieldFindAdd("USERNO", "Гүйлгээ хийсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");
            ucAccountTxnList.FindItemAdd("AMOUNT", "Гүйлгээний дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FindItemAdd("BALANCE", "Гүйлгээний дараах үлдэгдэл", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FindItemAdd("CONTACCCOUNTNO", "Харьцсан данс", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FindItemAdd("CONTAMOUNT", "Харьцсан дансны дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FindItemAdd("BASEAMOUNT", "Харьцсан дансны суурь дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FieldFindAdd("DESCRIPTION", "Гүйлгээний утга", typeof(string), "");
            ucAccountTxnList.FieldFindAdd("M", "Үндсэн гүйлгээ мөн эсэх ", typeof(ArrayList), "");
            ucAccountTxnList.FieldFindRefresh();
            InitDictionary();
            ucAccountTxnList.VisibleFind = true;
            ucAccountTxnList.FindItemSetValue("TXNDATE", _core.TxnDate);
            ucAccountTxnList.FindItemSetValue("TO_CHAR(TXNDATE)", _core.TxnDate);
            ucAccountTxnList.FindItemSetValue("CORR", "0");
            //ucAccountTxnList.FieldFindAdd("JRNO", "Журналын дугаар", typeof(long), "");
            //ucAccountTxnList.FieldFindAdd("PARTJRNO", "Гүйлгээний хэсгийн журнал дугаар", typeof(long), "");
            //ucAccountTxnList.FieldFindAdd("SUBJRNO", "Дэд журналын дугаар", typeof(long), "");
            //ucAccountTxnList.FieldFindAdd("TXNENTRY", "Гүйлгээний оролт", typeof(ArrayList), "");
            //ucAccountTxnList.FieldFindAdd("TxnDate", "Гүйлгээний огноо", typeof(DateTime), "");
           
            //ucAccountTxnList.FieldFindAdd("POSTDATE", "Гүйлгээний огноо, цаг", typeof(DateTime), "");
            //ucAccountTxnList.FindItemAdd("AccountNo", "Дансны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            //ucAccountTxnList.FieldFindAdd("BRANCHNO", "Салбарын дугаар", typeof(ArrayList), "");
            //ucAccountTxnList.FieldFindAdd("PRODCODE", "Бүтээгдхүүний дугаар", typeof(ArrayList), "");
            //ucAccountTxnList.FieldFindAdd("USERNO", "Гүйлгээ хийсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");
            //ucAccountTxnList.FieldFindAdd("CURCODE", "Валютын код", typeof(ArrayList), "");
            //ucAccountTxnList.FindItemAdd("CONTACCCOUNTNO", "Харьцсан данс", "", DynamicParameterType.Decimal, false, "d", "");
            //ucAccountTxnList.FieldFindAdd("CONTCURRCODE", "Харьцсан дансны валют", typeof(ArrayList), "");


            ucAccountTxnList.FieldFindRefresh();
            InitDictionary();
            ucAccountTxnList.VisibleFind = true;
            ucAccountTxnList.FindItemSetValue("TXNDATE", _core.TxnDate);
            ucAccountTxnList.FindItemSetValue("TO_CHAR(TXNDATE)", _core.TxnDate);



        }

        //public void Init_CON(DataRow DR)
        //{
        //    object[] obj = new object[2];
        //    Result res;
        //    try
        //    {
        //        obj[0] = Static.ToInt(Account_Type);
        //        obj[1] = Static.ToLong(_ID);
        //        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 231015, 231015, obj);
               
        //        if (res.ResultNo == 0)
        //        {
        //            ucAccountTxnList.DataSource = res.Data.Tables[0];
        //            LoadCONData();
        //        }
        //        else
        //        {
        //            MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void LoadCONData()
        {
           
            //ucAccountTxnList.gridView1.OptionsView.ColumnAutoWidth = false;
            //ucAccountTxnList.gridView1.BestFitColumns();
            FormUtility.RestoreStateGrid(appname, formname, ref ucAccountTxnList.gridView1);
            ucAccountTxnList.FieldLinkSetColumnCaption(0, "Журналын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(1, "Гүйлгээний хэсгийн журнал дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(2, "Дэд журналын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(3, "Дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(4, "Гүйлгээний оролт");
            ucAccountTxnList.FieldLinkSetColumnCaption(5, "Гүйлгээний огноо");
            ucAccountTxnList.FieldLinkSetColumnCaption(6, "Гүйлгээний огноо, цаг минут");
            ucAccountTxnList.FieldLinkSetColumnCaption(7, "Дансны дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(8, "Салбарын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(9, "Бүтээгдэхүүний дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(10, "Гүйлгээ хийсэн хэрэглэгчийн дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(11, "Комьютерийн нэр");
            ucAccountTxnList.FieldLinkSetColumnCaption(12, "Комьютерийн IP");
            ucAccountTxnList.FieldLinkSetColumnCaption(13, "Комьютерийн MAC");
            ucAccountTxnList.FieldLinkSetColumnCaption(14, "Гүйлгээний код");
            ucAccountTxnList.FieldLinkSetColumnCaption(15, "Гүйлгээний дүн");
            ucAccountTxnList.FieldLinkSetColumnCaption(16, "Гүйлгээний валютын ханш");
            ucAccountTxnList.FieldLinkSetColumnCaption(17, "Гүйлгээний валют");
            ucAccountTxnList.FieldLinkSetColumnCaption(18, "Гүйлгээний дараах үлдэгдэл");
            ucAccountTxnList.FieldLinkSetColumnCaption(19, "Харьцсан данс");
            ucAccountTxnList.FieldLinkSetColumnCaption(20, "Харьцсан дансны валют");
            ucAccountTxnList.FieldLinkSetColumnCaption(21, "Харьцсан валютын ханш");
            ucAccountTxnList.FieldLinkSetColumnCaption(22, "Харьцсан дансны дүн");
            ucAccountTxnList.FieldLinkSetColumnCaption(23, "Харьцсан дансны суурь дүн");
            ucAccountTxnList.FieldLinkSetColumnCaption(24, "Гүйлгээний утга");
            ucAccountTxnList.FieldLinkSetColumnCaption(25, "Буцаагдсан эсэх");
            ucAccountTxnList.FieldLinkSetColumnCaption(26, "Бэлэн гүйлгээ эсэх");
            ucAccountTxnList.FieldLinkSetColumnCaption(27, "Зөвшөөрсөн теллерийн дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(28, "Ерөнхий дэвтэрлүү татагдсан эсэх");
            ucAccountTxnList.FieldLinkSetColumnCaption(29, "Үндсэн гүйлгээ мөн эсэх");
            ucAccountTxnList.FieldLinkSetColumnCaption(30, "Групп гүйлгээний код");
            FormUtility.RestoreStateGrid(appname, formname, ref ucAccountTxnList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucAccountTxnList.ucParameterPanel1.vGridControl1, ref ucAccountTxnList.groupControl1);
        }
        #endregion
        #region [ Үндсэн хөрөнгийн жагсаалт ]
        private void InitFa()
        {
            
            ucAccountTxnList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucAccountTxnList_EventFindPaging);

            ucAccountTxnList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(LoadFAData);

            /*"F.TXNDATE >=", "F.TXNDATE <=","F.FATYPEID=","F.FAID like","F.ACCOUNTNO like","F.TXNTYPE=","F.TXNCOUNT like","F.TXNAMOUNT like","F.USERNO=","UPPER(F.DESCRIPTION) like ","F.CONTACNTNO like","F.CONTAMOUNT like"*/

            ucAccountTxnList.FieldFindAdd("TXNDATE", "Гүйлгээний эхлэх огноо", typeof(DateTime), "");
            ucAccountTxnList.FieldFindAdd("TO_CHAR(TXNDATE)", "Гүйлгээний дуусах огноо", typeof(DateTime), "");

            //ucAccountTxnList.FieldFindAdd("JRNO", "Журналын дугаар", typeof(long), "");
            //ucAccountTxnList.FieldFindAdd("TxnDate", "Гүйлгээний огноо", typeof(DateTime), "");
            //ucAccountTxnList.FieldFindAdd("POSTDATE", "Гүйлгээний огноо, цаг", typeof(DateTime), "");
            ucAccountTxnList.FieldFindAdd("FATYPEID", "Үндсэн хөрөнгийн төрөл", typeof(ArrayList), "");
            ucAccountTxnList.FindItemAdd("FAID", "Үндсэн хөрөнгийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FindItemAdd("ACCOUNTNO", "Дансны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FieldFindAdd("TXNTYPE", @"Гүйлгээний төрөл
TxnCode=231020 бол
   0 - Орлого
   1 - Зарлага
TxnCode=231022 бол
   0 - Зарлага
   1 - Актлах", typeof(int), "");
            ucAccountTxnList.FindItemAdd("TXNCOUNT", "Гүйлгээний тоо ширхэг", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FindItemAdd("TXNAMOUNT", "Гүйлгээний дүн", "", DynamicParameterType.Decimal, false, "d", "");
            //ucAccountTxnList.FieldFindAdd("TXNCURRCODE", "Гүйлгээний валют", typeof(ArrayList), "");
            
            ucAccountTxnList.FieldFindAdd("USERNO", "Гүйлгээ хийсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");
            ucAccountTxnList.FieldFindAdd("DESCRIPTION", "Гүйлгээний утга", typeof(string), "");
            //ucAccountTxnList.FieldFindAdd("BRANCHNO", "Салбарын дугаар", typeof(ArrayList), ""); ucAccountTxnList.FieldFindAdd("SUBJRNO", "Дэд журналын дугаар", typeof(long), "");


            ucAccountTxnList.FindItemAdd("CONTACCCOUNTNO", "Харьцсан данс", "", DynamicParameterType.Decimal, false, "d", "");

            ucAccountTxnList.FindItemAdd("CONTAMOUNT", "Харьцсан дансны дүн", "", DynamicParameterType.Decimal, false, "d", "");
            //ucAccountTxnList.FieldFindAdd("CONTCURRCODE", "Харьцсан дансны валют", typeof(ArrayList), "");

            //ucAccountTxnList.FindItemAdd("CLAIMID", "Нөхөн төлбөрийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");




            ucAccountTxnList.FieldFindRefresh();
            InitDictionary();
            ucAccountTxnList.VisibleFind = true;
            ucAccountTxnList.FindItemSetValue("TXNDATE", _core.TxnDate);
            ucAccountTxnList.FindItemSetValue("TO_CHAR(TXNDATE)", _core.TxnDate);


        }
        
        //public void Init_FA(DataRow DR)
        //{
        //    object[] obj = new object[2];
        //    Result res;
        //    try
        //    {
        //        obj[0] = Static.ToInt(Account_Type);
        //        obj[1] = Static.ToLong(_ID);
        //        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 231015, 231015, obj);
        //        if (res.ResultNo == 0)
        //        {
        //            ucAccountTxnList.DataSource = res.Data.Tables[0];
        //            LoadCONData();
        //        }
        //        else
        //        {
        //            MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void LoadFAData()
        {
            /*F.JRNO, F.TXNDATE, F.POSTDATE, F.FATYPEID, F.FAID, F.ACCOUNTNO, F.TXNCODE, F.TXNTYPE, F.TXNCOUNT, F.TXNAMOUNT, 
F.TXNCURRCODE, F.RATE, F.USERNO, F.BRANCHNO, F.DESCRIPTION, F.CONTACNTNO, F.CONTCURCODE, F.CONTRATE, F.CONTAMOUNT, F.CLAIMID, 
F.INSURANCENO, F.DEPRECIATION, F.PROFIT, F.ORDERNO*/

            ucAccountTxnList.FieldLinkSetColumnCaption(0, "Журналын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(1, "Гүйлгээний огноо");
            ucAccountTxnList.FieldLinkSetColumnCaption(2, "Гүйлгээ хийсэн огноо, цаг");

            ucAccountTxnList.FieldLinkSetColumnCaption(3, "Төрлийн ID");
            ucAccountTxnList.FieldLinkSetColumnCaption(4, "Үндсэн хөрөнгийн материалын дугаар");
            ucAccountTxnList.gridView1.Columns[4].Width = 100;
            ucAccountTxnList.FieldLinkSetColumnCaption(5, "Дансны дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(6, "Гүйлгээний код");
            ucAccountTxnList.FieldLinkSetColumnCaption(7, "Гүйлгээний төрөл");
            ucAccountTxnList.FieldLinkSetColumnCaption(8, "Гүйлгээний тоо ширхэг");
            ucAccountTxnList.FieldLinkSetColumnCaption(9, "Гүйлгээний дүн");
            ucAccountTxnList.FieldLinkSetColumnCaption(10, "Гүйлгээний валют");
            ucAccountTxnList.FieldLinkSetColumnCaption(11, "Ханш");
            ucAccountTxnList.FieldLinkSetColumnCaption(12, "Гүйлгээ хийсэн хэрэглэгч");
            ucAccountTxnList.FieldLinkSetColumnCaption(13, "Салбарын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(14, "Гүйлгээний утга");
            ucAccountTxnList.FieldLinkSetColumnCaption(15, "Харьцсан дансны дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(16, "Харьцсан дансны валют");
            ucAccountTxnList.FieldLinkSetColumnCaption(17, "Харьцсан дансны ханш");
            ucAccountTxnList.FieldLinkSetColumnCaption(18, "Харьцсан дансны дүн");
            ucAccountTxnList.FieldLinkSetColumnCaption(19, "Нөхөн төлбөрийн дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(20, "Даатгалын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(21, "Үндсэн хөрөнгийн зарлагын хуримтлагдсан элэгдэл");
            ucAccountTxnList.FieldLinkSetColumnCaption(22, "Үндсэн хөрөнгийн зарлагын олз гарз");
            ucAccountTxnList.FieldLinkSetColumnCaption(23, "Актласаны тушаалын дугаар");
            FormUtility.RestoreStateGrid(appname, formname, ref ucAccountTxnList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucAccountTxnList.ucParameterPanel1.vGridControl1, ref ucAccountTxnList.groupControl1);

        }
        #endregion
        #region [ Бараа материалын жагсаалт ]
        private void InitInv()
        {
            /*"I.TXNDATE >=", "I.TXNDATE <=","I.INVTYPEID=","I.INVID like","I.ACCOUNTNO like","I.TXNTYPE=","I.TXNCOUNT like",
             * "I.TXNAMOUNT like","I.USERNO=","UPPER(I.DESCRIPTION) like ","I.CONTACNTNO like","I.CONTAMOUNT like"
*/
            ucAccountTxnList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucAccountTxnList_EventFindPaging);

            ucAccountTxnList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(LoadFAData);
            ucAccountTxnList.FieldFindAdd("TXNDATE", "Гүйлгээний эхлэх огноо", typeof(DateTime), "");
            ucAccountTxnList.FieldFindAdd("TO_CHAR(TXNDATE)", "Гүйлгээний дуусах огноо", typeof(DateTime), "");
            //ucAccountTxnList.FieldFindAdd("JRNO", "Журналын дугаар", typeof(long), "");
            //ucAccountTxnList.FieldFindAdd("TxnDate", "Гүйлгээний огноо", typeof(DateTime), "");
            //ucAccountTxnList.FieldFindAdd("POSTDATE", "Гүйлгээний огноо, цаг", typeof(DateTime), "");
            ucAccountTxnList.FieldFindAdd("INVTYPEID", "Төрөлийн ID", typeof(ArrayList), "");
            ucAccountTxnList.FindItemAdd("INVID", "Бараа материалын дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FindItemAdd("ACCOUNTNO", "Дансны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FieldFindAdd("TXNTYPE", @"Гүйлгээний төрөл 
   0 - Орлого
   1 - Зарлага", typeof(int), "");
            ucAccountTxnList.FindItemAdd("TXNCOUNT", "Гүйлгээний тоо ширхэг", "", DynamicParameterType.Decimal, false, "d", "");
            ucAccountTxnList.FindItemAdd("TXNAMOUNT", "Гүйлгээний дүн", "", DynamicParameterType.Decimal, false, "d", "");
            //ucAccountTxnList.FieldFindAdd("TXNCURRCODE", "Гүйлгээний валют", typeof(ArrayList), "");

            ucAccountTxnList.FieldFindAdd("USERNO", "Гүйлгээ хийсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");
            ucAccountTxnList.FieldFindAdd("DESCRIPTION", "Гүйлгээний утга", typeof(string), "");
            //ucAccountTxnList.FieldFindAdd("BRANCHNO", "Салбарын дугаар", typeof(ArrayList), ""); ucAccountTxnList.FieldFindAdd("SUBJRNO", "Дэд журналын дугаар", typeof(long), "");


            ucAccountTxnList.FindItemAdd("CONTACCCOUNTNO", "Харьцсан данс", "", DynamicParameterType.Decimal, false, "d", "");

            ucAccountTxnList.FindItemAdd("CONTAMOUNT", "Харьцсан дансны дүн", "", DynamicParameterType.Decimal, false, "d", "");
            //ucAccountTxnList.FieldFindAdd("BRANCHNO", "Салбарын дугаар", typeof(ArrayList), ""); ucAccountTxnList.FieldFindAdd("SUBJRNO", "Дэд журналын дугаар", typeof(long), "");


         


            //ucAccountTxnList.FieldFindAdd("CONTCURRCODE", "Харьцсан дансны валют", typeof(ArrayList), "");

            //ucAccountTxnList.FindItemAdd("CLAIMID", "Нөхөн төлбөрийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");




            ucAccountTxnList.FieldFindRefresh();
            InitDictionary();
            ucAccountTxnList.VisibleFind = true;

            ucAccountTxnList.FindItemSetValue("TXNDATE", _core.TxnDate);
            ucAccountTxnList.FindItemSetValue("TO_CHAR(TXNDATE)", _core.TxnDate);

        }


        //public void Init_INV(DataRow DR)
        //{
        //    object[] obj = new object[2];
        //    Result res;
        //    try
        //    {
        //        obj[0] = Static.ToInt(Account_Type);
        //        obj[1] = Static.ToLong(_ID);
        //        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 231015, 231015, obj);
        //        if (res.ResultNo == 0)
        //        {
        //            ucAccountTxnList.DataSource = res.Data.Tables[0];
        //            LoadCONData();
        //        }
        //        else
        //        {
        //            MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void LoadINVData()
        {
            ucAccountTxnList.FieldLinkSetColumnCaption(0, "Журналын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(1, "Гүйлгээний огноо");
            ucAccountTxnList.FieldLinkSetColumnCaption(2, "Гүйлгээ хийсэн огноо, цаг минут");
            ucAccountTxnList.FieldLinkSetColumnCaption(3, "Төрлийн ID");
            ucAccountTxnList.FieldLinkSetColumnCaption(4, "Үндсэн хөрөнгийн материалын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(5, "Дансны дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(6, "Гүйлгээний код");
            ucAccountTxnList.FieldLinkSetColumnCaption(7, "Гүйлгээний төрөл");
            ucAccountTxnList.FieldLinkSetColumnCaption(8, "Гүйлгээний тоо ширхэг");
            ucAccountTxnList.FieldLinkSetColumnCaption(9, "Гүйлгээний дүн");
            ucAccountTxnList.FieldLinkSetColumnCaption(10, "Гүйлгээний валют");
            ucAccountTxnList.FieldLinkSetColumnCaption(11, "Ханш");
            ucAccountTxnList.FieldLinkSetColumnCaption(12, "Гүйлгээ хийсэн хэрэглэгч");
            ucAccountTxnList.FieldLinkSetColumnCaption(13, "Салбарын дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(14, "Гүйлгээний утга");
            ucAccountTxnList.FieldLinkSetColumnCaption(15, "Харьцсан дансны дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(16, "Харьцсан дансны валют");
            ucAccountTxnList.FieldLinkSetColumnCaption(17, "Харьцсан дансны ханш");
            ucAccountTxnList.FieldLinkSetColumnCaption(18, "Харьцсан дансны дүн");
            ucAccountTxnList.FieldLinkSetColumnCaption(19, "Нөхөн төлбөрийн дугаар");
            ucAccountTxnList.FieldLinkSetColumnCaption(20, "Даатгалын дугаар");
            FormUtility.RestoreStateGrid(appname, formname, ref ucAccountTxnList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucAccountTxnList.ucParameterPanel1.vGridControl1, ref ucAccountTxnList.groupControl1);
        }
        #endregion
        private void AccountTxnList_KeyDown(object sender, KeyEventArgs e)
        {
         
        }
        private void ucAccountTxnList_KeyDown(object sender, KeyEventArgs e)
        {
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();

                        break;
                    case Keys.Enter:
                        ucAccountTxnList.OnEventFindPaging(1);
                       
                        break;
                }
                // return;
            }
        }

        private void ucAccountTxnList_Load(object sender, EventArgs e)
        {
            ucAccountTxnList.gridView1.OptionsSelection.MultiSelect = true;
        }

        private void AccountTxnList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucAccountTxnList.gridView1);

            FormUtility.SaveStateVGrid(appname, formname, ref ucAccountTxnList.ucParameterPanel1.vGridControl1, ref ucAccountTxnList.groupControl1);
        }

        private void ucAccountTxnList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            object[] obj = new object[2];
            obj[0] = Static.ToInt(Account_Type);
            obj[1] = Static.ToLong(_ID);
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 231015, 231015, pageindex, pagerows, new object[] {Account_Type,_ID, values});

            if (r.ResultNo == 0)
            {
                try
                {
                    ucAccountTxnList.DataSource = r.Data.Tables[0];

                    switch (Account_Type)
                    {
                        case 0:
                           
                            LoadBACData();
                         //  InitBac();
                         
                            break;
                        case 1:
                            LoadCONData();
                          
                            break;
                        case 2:
                            LoadFAData();
                   
                            break;
                        case 3:
                            LoadINVData();
                       
                            break;
                    }
                    return ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
            }

         
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

  


        }
    }
}
