using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

using EServ.Shared;
using InfoPos.Core;

namespace InfoPos.PostingGL
{
    public partial class frmPostingGL : Form
    {
        #region [ Class genereal variables ]
        Core.Core _core;
        OleDbConnection conn= null;

        string vSQLUserName="Skyresort_POS";
        string vSQLUserPass="v9p8LIaG";
        string vSQLServer="192.168.0.23";
        string vSQLDataBaseName="SkyResort_POS_Demo";
        #endregion
        #region [ Forms ]
        public frmPostingGL(Core.Core core)
        {
            _core = core;
            _core.EventProgressUpdate += new Core.Core.dlgProgressUpdate(_core_EventProgressUpdate);
            _core.EventDateChanged += new Core.Core.dlgServerDateChanged(_core_EventDateChanged);
            InitializeComponent();

            InitAll();
        }
        void _core_EventDateChanged(DateTime TxnDate)
        {
            throw new NotImplementedException();
        }
        void _core_EventProgressUpdate(string Func, int ProcessNo, int Status, string ErrMessage, DateTime StartDate, DateTime EndDate)
        {
            throw new NotImplementedException();
        }
        private void frmPostingGL_Load(object sender, EventArgs e)
        {

        }
        private void frmPostingGL_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAll();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ConnectToSQLServer();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            IntegrateCustomer();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            CleanList();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            PostSalesToGL();
        }
        #endregion
        #region [ Functions ] 
        void InitAll()
        {
            vSQLServer = Static.ToStr(Static.RegisterGet("PostingGL", "SQLServer", "192.168.0.23"));
            vSQLDataBaseName = Static.ToStr(Static.RegisterGet("PostingGL", "SQLDataBaseName", "SkyResort_POS_Demo"));
            vSQLUserName = Static.ToStr(Static.RegisterGet("PostingGL", "SQLUserName", "Skyresort_POS"));
            vSQLUserPass = Static.ToStr(Static.RegisterGet("PostingGL", "SQLUserPass", "v9p8LIaG"));

            txtUsername.Text = vSQLUserName;
            txtPassword.Text = vSQLUserPass;
            txtServer.Text = vSQLServer;
            txtDB.Text = vSQLDataBaseName;
        }
        void SaveAll()
        {
            Static.RegisterSave("PostingGL", "SQLServer", vSQLServer);
            Static.RegisterSave("PostingGL", "SQLDataBaseName", vSQLDataBaseName);
            Static.RegisterSave("PostingGL", "SQLUserName", vSQLUserName);
            Static.RegisterSave("PostingGL", "SQLUserPass", vSQLUserPass);
        }
        void CleanList()
        {
            listBox1.Items.Clear();
        }
        void ConnectToSQLServer()
        {
            int flag = 0;
            try
            {

                if (conn == null)
                    flag = 1;
                else
                    if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken || conn.State == ConnectionState.Connecting)
                        flag = 1;
                if (flag == 1)
                {
                    listBox1.Items.Add("Connecting to SQL (" + vSQLServer + " " + vSQLDataBaseName + " " + vSQLUserName + " ***********)");

                    vSQLUserName = txtUsername.Text.Trim();
                    vSQLUserPass = txtPassword.Text.Trim();
                    vSQLServer = txtServer.Text.Trim();
                    vSQLDataBaseName = txtDB.Text.Trim();

                    conn = new OleDbConnection();
                    string constr = "Provider=SQLOLEDB;" +
                    "Data Source="+ vSQLServer + ";" +        //Data Source
                    "Initial Catalog="+ vSQLDataBaseName + ";" +
                    "User ID="+ vSQLUserName + ";" +
                    "Password="+ vSQLUserPass + ";";
                    conn.ConnectionString = constr;
                    listBox1.Items.Add("Connection string="+conn.ConnectionString);
                    conn.Open();
                    if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken || conn.State == ConnectionState.Connecting)
                    {
                        listBox1.Items.Add("Connection is not established. Please check connection settings...");
                    }
                    else
                        listBox1.Items.Add("Connection is succesfull...");
                }
                else
                {
                    listBox1.Items.Add("Connection is already established...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source+ex.StackTrace+ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }
        void IntegrateCustomer()
        {
            Result res = new Result();
            try
            {
                #region [ variables ]
                DataSet myDataSet = new DataSet();
                OleDbCommand myAccessCommand;
                OleDbDataAdapter myDataAdapter;
                DataTable CustomerData;
                string SQL = "";
                string vCustNo="";
                string vCustName = "";
                string vCustAddr = "";
                string vRegisterNo = "";
                string vCityCode = "";
                string vTelephone = "";
                string vFax = "";
                string vMobile = "";
                string vDDirName = "";
                string vEmail = "";
                string vWebsite = "";
                int result=0;
                string sqlstr = "";
                txtProgress.Value = 0;
                int progress = 0;
                #endregion
                listBox1.Items.Add("Customer information loading for migrate to ABACUS.");
                #region [ Check connection state ]
                int flag = 0;
                if (conn == null)
                    flag = 1;
                else
                    if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken || conn.State == ConnectionState.Connecting)
                        flag = 1;
                if (flag == 1)
                {
                    MessageBox.Show("Та эхлээд SQL сервертэйгээ холбогдох ёстой. Холбох товчийг дарна уу", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                #endregion
                #region [ Өнөөдөр нийт хэдэн харилцагч гүйлгээ хийсэнийг олж авах ]
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 607, 1, 1, new object[] { txtTxnDate.Text.Trim()});
                if (res.ResultNo != 0)  
                {
                    MessageBox.Show(res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                DataTable TodayCustomers;
                TodayCustomers = res.Data.Tables[0];
                #endregion

                #region [SQL]
                sqlstr = "select * from dbo.tblcustomers where SrbsMembershipId = ?";
                #endregion

                foreach (DataRow row in TodayCustomers.Rows)
                {
                    vCustNo = Static.ToStr(row["CustNo"]);
                    #region [ Тухайн харилцагч Abacus дээр байгаа эсэхийг шалгах ]
                    myAccessCommand = new OleDbCommand(sqlstr, conn);
                    myAccessCommand.Parameters.Add("?", OleDbType.VarChar, 100).Value = vCustNo;
                    myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                    myDataSet = new DataSet();
                    myDataAdapter.Fill(myDataSet);
                    #endregion
                    if (myDataSet.Tables[0].Rows.Count < 1)
                    {
                        #region [ Тухайн харилцагчийн мэдээллийг цуглуулах ]
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 607, 2, 2, new object[] { vCustNo });
                        if (res.ResultNo != 0)
                        {
                            MessageBox.Show(res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close();
                        }
                        CustomerData = res.Data.Tables[0];
                        #endregion
                        if (CustomerData.Rows.Count > 0)
                        {
                            DataRow custdata = CustomerData.Rows[0];

                            #region [ Тухайн харилцагчийн мэдээллийг insert хийх ]

                            //vCustNo= Static.ToStr(CustomerData.Rows[0]["CustNo"]);
                            vRegisterNo = Static.ToStr(custdata["RegisterNo"]);
                            vCityCode = Static.ToStr(custdata["CityCode"]);
                            vTelephone = Static.ToStr(custdata["Telephone"]);
                            vFax = Static.ToStr(custdata["Fax"]);
                            vMobile = Static.ToStr(custdata["Mobile"]);
                            vEmail = Static.ToStr(custdata["EMail"]);
                            vWebsite = Static.ToStr(custdata["WebSite"]);

                            if (Static.ToInt(custdata["ClassCode"]) == 0)
                            {
                                vCustName = Static.ToStr(custdata["FirstName"]) + Static.ToStr(custdata["MiddleName"]) + Static.ToStr(custdata["LastName"]);
                            }
                            else
                            {
                                vCustName = Static.ToStr(custdata["CorporateName"]) + Static.ToStr(custdata["CorporateName2"]);
                            }
                            vCustAddr = Static.ToStr(custdata["CityCode"]) + Static.ToStr(custdata["DistCode"]) +
                                       Static.ToStr(custdata["SubDistCode"]) + Static.ToStr(custdata["Apartment"]) +
                                       Static.ToStr(custdata["Note"]);
                            vDDirName = Static.ToStr(custdata["DirFirstName"]) + Static.ToStr(custdata["DirLastName"]);

                            SQL = "INSERT INTO dbo.tblcustomers(SrbsMembershipId,Cust#,CustomerName,MailingAddress,MailingAddress2,MailingCity,MailingState,MailingCountry,MailingZip,ShipToAddress,ShipToAddress2,ShipToCity,ShipToState,ShipToCountry,ShipToZip,Phone#,Fax#,OtherPhone,Contact,EMail,Internet,DefaultGLSales#,TermsID,Active,Status,SalesmanID,TaxExempt#,CC#,InternationalID,CountryID,StateID,CountyID,CityID,OtherID,FinanceChargeID,AcquisitionID,CurrencyID,OnlineID,Latitude,Longitude)";
                            SQL += "VALUES('" + vCustNo + "','" + vCustNo + "',N'" + vCustName + "',N'" + vCustAddr + "',N'" + vRegisterNo + "','" + vCityCode + "','" + vCityCode + "','" + vCityCode + "',0,N'" + vCustAddr + "',N'" + vRegisterNo + "','" + vCityCode + "','" + vCityCode + "','" + vCityCode + "',0,'" + vTelephone + "','" + vFax + "','" + vMobile + "',N'" + vDDirName + "',N'" + vEmail + "',N'" + vWebsite + "',4,0,1,2,0,null,null,0,0,0,0,0,0,0,0,0,0,0,0)";
                            //CustNo - ?1
                            //CustName - ?2
                            //@CustAddr - ?3
                            //@RegisterNo - ?4
                            //@CityCode - ?5
                            //@Telephone - ?6
                            //@Fax - ?7
                            //@DDirName - ?8
                            //@Email - ?9
                            //@Website - ?10
                            //@Mobile - ?11
                            try
                            {
                                myAccessCommand = new OleDbCommand(SQL, conn);
                                result = myAccessCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                listBox1.Items.Add("Customer (" + vCustNo + "-" + vCustName + ")" + ex.Message);
                            }
                        }
                        #endregion
                    }
                    progress += 1;
                    txtProgress.Value = progress * 100 / TodayCustomers.Rows.Count;
                }

                listBox1.Items.Add("FINISH Customer information loading...");
                txtProgress.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source+ex.StackTrace+ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }       
        }
        void PostSalesToGL()
        {
            Result res = new Result();
            DataTable TodaySales;
            OleDbCommand myAccessCommand=null;
            OleDbDataAdapter myDataAdapter=null;
            OleDbDataReader reader = null;

            txtProgress.Value = 0;
            int progress = 0;
            int vGetNextBatch=0;
            int vGetNextARID = 0;
            int vGetJournalIDHist=0;
            int vGetCheckID=0;
            string vARAccountNo = "";
            string vSalesNo = "";
            int vEntryType = 0;
            int vAccountEntry = 0;
            int vPaymentFlag = 0;
            int vSeqNo = 0;
            int vPaymentSeqNo = 0;
            string vDesc="";
            DateTime vTxnDate;
            string vCustNo = "";
            string vCashierNo = "";
            decimal vTxnAmount=0;
            string sql = "";
            int result = 0;
            string datestr="";
            listBox1.Items.Add("Invoice information loading for migrate to ABACUS.");
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken || conn.State == ConnectionState.Connecting)
            {
                MessageBox.Show("Та эхлээд SQL сервертэйгээ холбогдох ёстой. Холбох товчийг дарна уу", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            #region [ Өнөөдөр борлуулалтын гүйлгээнүүд ]
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 607, 3, 3, new object[] { txtTxnDate.Text.Trim() });
            if (res.ResultNo != 0)
            {
                MessageBox.Show(res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            TodaySales = res.Data.Tables[0];
            #endregion

            foreach (DataRow row in TodaySales.Rows)
            {
                vAccountEntry= Static.ToInt(row["AccountENTRY"]);
                vEntryType = Static.ToInt(row["ENTRYTYPE"]);
                vSeqNo = Static.ToInt(row["SeqNo"]);
                vARAccountNo = Static.ToStr(row["AccountNo"]);
                vSalesNo = Static.ToStr(row["SalesNo"]);
                vTxnDate = Static.ToDate(row["TxnDate"]);
                vCustNo = Static.ToStr(row["CustNo"]);
                vCashierNo = Static.ToStr(row["CashierNo"]);
                vTxnAmount = Static.ToDecimal(row["TxnAmount"]);
                vDesc = Static.ToStr(row["DESCRIPTION"]);
                vPaymentFlag = Static.ToInt(row["PaymentFlag"]);

                switch (vEntryType)
                { 
                    case 0:
                        #region [ Next Batch No ]
                        vGetNextBatch = 0;
                        myAccessCommand = new OleDbCommand(@"
        DECLARE
        @return_value int 
        EXEC
        @return_value = [dbo].[sp_GetNextBatch]
        @TheUser = N'Srgs_admin'
        SELECT
        'Return Value' = @return_value
        ", conn);
                        myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                        reader= myAccessCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetValue(0) != null)
                            {
                                vGetNextBatch = (int)reader.GetValue(0);
                            }
                        }
                        #endregion
                        #region [ Next Invoice ID ]
                        vGetNextARID = 0;
                       myAccessCommand = new OleDbCommand(@"
        DECLARE
        @return_value int 
        EXEC
        @return_value= [dbo].[sp_GetNextARID] 
        @Amount =1
        SELECT
        'Return Value' = @return_value
        ", conn);
                        myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                        reader = myAccessCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetValue(0) != null)
                            {
                                vGetNextARID = (int)reader.GetValue(0);
                            }
                        }
                        #endregion             
                        #region [ Invoice]
                        datestr = "Convert(DateTime,'" + vTxnDate.ToString("yyyyMMdd") + "',112)";
                        sql = " INSERT INTO dbo.tblARInvoices(ARInvoiceID,OrderID,SrbsPaymentId,Invoice#,Batch#,Ref#,Date,ARInvoiceDate,DebitGLID,Amount,ARBalance,CustomerID,Author,PO#,JobID,SalesmanID,DocType,ActivityID)"+
                              " VALUES(" + vGetNextARID + ",0," + vSalesNo + "," + vGetNextARID + "," + vGetNextBatch + "," + vSalesNo + "," + datestr + "," + datestr + ",'" + vARAccountNo + "'," + vTxnAmount + "," + vTxnAmount + ",'" + vCustNo + "','" + vCashierNo + "'," + vSalesNo + ",0,'" + vCashierNo + "',1,0)";
                        myAccessCommand = new OleDbCommand(sql, conn);
                        result = myAccessCommand.ExecuteNonQuery();

                        #endregion
                        break;
                    case 1:
                        if (vAccountEntry != 1)
                            vTxnAmount = -1 * vTxnAmount;
                        break;
                        #region [ Invoice detail ]
                        sql = " INSERT INTO dbo.tblARInvoiceDetail (ARInvoiceID,ARInvoiceDetailID,SrbsPaymentDetailId,CreditGLID,UnitPrice,Quantity,SplitAmount,Description,Type,ActivityID)"+
                              " VALUES ("+vGetNextARID+","+vGetNextARID + vSeqNo+","+vSeqNo+",'"+vARAccountNo+"',"+vTxnAmount+",1,"+vTxnAmount+",'"+vDesc+"',14,0)";
                        myAccessCommand = new OleDbCommand(sql, conn);
                        result = myAccessCommand.ExecuteNonQuery();
                        #endregion
                    case 2:
                        if (vPaymentFlag != 0)
                            break;
                        #region [ Next GetJournalIDHist ]
                        vGetJournalIDHist = 0;
                        myAccessCommand = new OleDbCommand(@"
        DECLARE
        @return_value int 
        EXEC
        @return_value = [dbo].[sp_GetJournalIDHist]
        @TheUser = N'Srgs_admin'
        SELECT
        'Return Value' = @return_value
        ", conn);
                        myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                        reader= myAccessCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetValue(0) != null)
                            {
                                vGetJournalIDHist = (int)reader.GetValue(0);
                            }
                        }
                        #endregion
                        #region [ Next GetCheckID ]
                        vGetCheckID = 0;
                        myAccessCommand = new OleDbCommand(@"
        DECLARE
        @return_value int 
        EXEC
        @return_value = [dbo].[sp_GetCheckID]           
        @Amount =1
        SELECT
        'Return Value' = @return_value
        ", conn);
                        myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                        reader = myAccessCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.GetValue(0) != null)
                            {
                                vGetCheckID = (int)reader.GetValue(0);
                            }
                        }
                        #endregion             
                        #region [ Payments ]                         
                        vPaymentSeqNo = vPaymentSeqNo + 1;
                        sql = "INSERT INTO dbo.tblARPayments(Date,ARPaymentDate,InvoiceID,Batch#,PaymentType,Source,Check#,Ref#,GLID,Debit,Credit,GLUpdateDate,CustomerID,GLTransactionID,CheckID,ActivityID)" + 
                              "VALUES("+datestr+","+datestr+","+vGetNextARID+","+vGetNextBatch+",1,'A/RPayments',"+vSalesNo+","+vSalesNo+","+vARAccountNo+",0,"+vTxnAmount+","+datestr+","+vCustNo+","+vPaymentSeqNo+","+vGetCheckID+",0)";
                        myAccessCommand = new OleDbCommand(sql, conn);
                        result = myAccessCommand.ExecuteNonQuery();

                        //vPaymentSeqNo = vPaymentSeqNo + 1;
                        //sql = "INSERT INTO dbo.tblARPayments(Date,ARPaymentDate,InvoiceID,Batch#,PaymentType,Source,Check#,Ref#,GLID,Debit,Credit,GLUpdateDate,CustomerID,GLTransactionID,CheckID,ActivityID)" + 
                        //      "VALUES("+datestr+","+datestr+","+vGetNextARID+","+vGetNextBatch+",1,'A/RPayments',"+vSalesNo+","+vSalesNo+","+vARAccountNo+",0,"+vTxnAmount+","+datestr+","+vCustNo+","+vPaymentSeqNo+","+vGetCheckID+",0)";
                        //myAccessCommand = new OleDbCommand(sql, conn);
                        //result = myAccessCommand.ExecuteNonQuery();
                        #endregion
                        break;
                    default:
                        break;
                }

                progress += 1;
                txtProgress.Value = progress * 100 / TodaySales.Rows.Count;
            }
            listBox1.Items.Add("FINISH Invoice information loading...");
        }
        #endregion
    }
}
