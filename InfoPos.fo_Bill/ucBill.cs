using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Touch;
using InfoPos.Core;
using ISM.Template;
using System.Drawing.Printing;
using System.Collections;
using System.IO.Ports;

namespace InfoPos.Bill
{
    public partial class ucBill : XtraUserControl
    {       
    #region[Variable]
    int height = 0;        
    int x = 0;
    int y = 10;
    Font rptFont;
    Font hdrFont;
    Font titleFont;
    Font headerFont;
    Font repeatFont;
    Font ReturnFont;
    Font ReturnHeaderFont;
    decimal OldPaidAmount = 0;
    Font ReturntitleFont;           
    Hashtable salesallready = new Hashtable();
    PrintDocument pd = new PrintDocument(); // Golf Printer
    PrintDocument pdr = new PrintDocument(); // Golf Printer
    PrintDocument pdc = new PrintDocument(); // Cashier Closure
    PrintDocument pdcadd = new PrintDocument();
    PrintDocument pdPosClose = new PrintDocument();
    PrintDocument pdDelivery = new PrintDocument();
    StringFormat strFrmt = new StringFormat();
    string[] salesnos;
    decimal _score = 0;       
    public decimal _paydamount = 0;
    public decimal sumamount = 0;
    decimal price = 0;
    public string _billno = "";
    public decimal vat = 0;
    decimal _payenmenttype = 0;
    decimal _payenmentamnt = 0;
    public decimal _changeAmount = 0;
    decimal pricesum = 0;
    decimal discount = 0;
    decimal qnty = 0;
    decimal havevat = 0; // НӨАТ-тэй нэгжийн дүн
    int isvat = 0;       // НӨАТ эсэх
    int _shiftno = 0;
    string _name = "";    
    decimal salesamountsum = 0;
    int quantity = 0;
    decimal StartAmt = 0;   // Кассын хаалт
    decimal Cashtotal = 0;  // Кассын хаалт
    decimal Deposit = 0;    // Кассын хаалт
    decimal CashSale = 0;   // Борлуулалтаар хийсэн нийт дүн
    DataTable dtb;
    DataTable DTable;
    DataTable DTAdd;
    DataTable _DataPayType;
    DateTime _servertime;    
    #region[CashCloseBill]
    decimal Total = 0;
    decimal CashTotalAmt = 0;
    decimal CashSaleTotal = 0;
    string name = "";
    #endregion[]

    #endregion[]
    #region[Properties]
    private int _pagecount = 0;
    public int PageCount
    {
    get { return _pagecount; }
    }

    private int _pagerows = 20;
    public int PageRows
    {
    get { return _pagerows; }
    set
    {
    if (value > 0 && value < 100)
        _pagerows = value;
    }
    }

    private string _salesno = null;
    public string SalesNo
    {
    get { return _salesno; }
    set { _salesno = value; }
    }

    private string _batchno = null;
    public string BatchNo
    {
    get { return _batchno; }
    set { _batchno = value; }
    }
      
    private Font _font = null;
    private string _text = null;

    private TouchKeyboard _kb = null;
    public TouchKeyboard kb 
    {
    get { return _kb; }
    set { _kb = value; }
    }

    private ISM.CUser.Remote _remote;
    public ISM.CUser.Remote remote
    { 
    get { return _remote; }
    set
    {
    _remote = value; if (_remote != null)
    {
        ucSalesProd1.Remote = _remote;
        ucSalesCheckList1.Remote = _remote;
    }
    }
    }

    private InfoPos.Core.Core _core = null;
    public InfoPos.Core.Core core
    {
    get { return _core; }
    set { _core = value;
    if (_core != null) { ucSalesProd1.Resource = _core.Resource; ucSalesCheckList1.Resource = _core.Resource; }
    }
    }

    private DataTable _Data = null;
    public DataTable Data
    {
    get { return _Data; }
    set { _Data = value; }
    }
    #endregion[]
    #region[Constuction]
    public ucBill()
    {
    try
    {
    InitializeComponent();
    _Data = ucSalesProd1.productlist;           
    ucSalesCheckList1.EventOnCheckedChanged += new fo_panels.ucSalesCheckList.delegateEventOnCheckedChanged(ucSalesCheckList1_EventOnCheckedChanged);    
    }
    catch (Exception ex)
    {
    MessageBox.Show(ex.ToString());
    }
    }
    void ucSalesCheckList1_EventOnCheckedChanged(string[] SalesNo)
    {
    try
    {
    salesnos = SalesNo;
    ucSalesProd1.SalesFilter(SalesNo);
    DataTable DT = ucSalesProd1.GetProdList();
    if (DT != null)
    {
        salesamountsum = DT.AsEnumerable().Sum(x => Static.ToDecimal(x["SALESAMOUNT"]) * Static.ToInt(x["QUANTITY"]));  // Нийт үнэ 
        pricesum= DT.AsEnumerable().Sum(x => x.Field<decimal>("PRICE"));                                          // Нэгжийн үнэ      
        havevat = pricesum / 11;
        discount = DT.AsEnumerable().Sum(x => x.Field<decimal>("DISCOUNT"));                                      // Урамшуулал
        qnty = DT.AsEnumerable().Sum(x => Static.ToDecimal(x["QUANTITY"]));                                             // Ширхэг       

        numSalesAmount.Text = salesamountsum.ToString("#,##0");
        numDiscount.Text = discount.ToString("#,##0");
        numQuantity.Text = qnty.ToString("#,##0");
        textEdit1.Text = _core.Vat.ToString("#,##0");
    }
    else
    {
        numSalesAmount.Text = "";
        numQuantity.Text = "";
        numDiscount.Text = "";
    }
    }
    catch(Exception ex)
    {
    MessageBox.Show("Өгөгдөл алдаатай байна:"+ex.Message);
    }
    }
    #endregion[]
    #region[Events]
    private void ucSaleSearch1_EventChoose(DataRow currentrow)
    {            
    Result res = new Result();
    try
    {
    if (currentrow == null)
    {
        res = Msg.Get(EnumMessage.SALES_NOT_SELECTED);
    }
    else
    {
        string salesno = Static.ToStr(currentrow["salesno"]);
        string lname = Static.ToStr(currentrow["lastname"]);
        string fname = Static.ToStr(currentrow["firstname"]);
        _core.MainForm_HeaderSet(0, null, fname);
        _core.MainForm_HeaderSet(1, null, lname);
        _core.MainForm_HeaderSet(2, null, salesno);                                       
    }
    ucSalesProd1.Refresh();        
    }
    catch (Exception ex)
    {
    MessageBox.Show(ex.ToString());
    }
    }
    private void frmPayment_Load(object sender, EventArgs e)
    {
    try
    {
    numBatchNo.Text = _batchno;
    _Data = ucSalesProd1.productlist;
    if (_core != null)
    {
        ucSalesProd1.Resource = _core.Resource;
        ucSalesProd1.Remote = _core.RemoteObject;
        ucSalesCheckList1.Remote = _core.RemoteObject;
    }
    }
    catch (Exception ex)
    {
    MessageBox.Show(ex.Message + " : " + ex.StackTrace);
    }
    }
    #endregion[]
    #region[bill]
    public void ucSaleSearchToBill_EventChoose(DataRow currentrow)
    {
    Result res = new Result();
            
    try
    {
    if (currentrow == null)
    {
        res = Msg.Get(EnumMessage.SALES_NOT_SELECTED);
    }
    else
    {
        _batchno = Static.ToStr(currentrow["batchno"]);
        _salesno = Static.ToStr(currentrow["salesno"]);
        string lname = Static.ToStr(currentrow["lastname"]);
        string fname = Static.ToStr(currentrow["firstname"]);

        _core.MainForm_HeaderSet(0, null, fname);
        _core.MainForm_HeaderSet(1, null, lname);
        _core.MainForm_HeaderSet(2, null, _salesno);

        numBatchNo.Text = _batchno;

        //tabMain.SelectedTabPageIndex = 3;

        res = ucSalesCheckList1.RefreshData(_batchno);

        if (ISM.Template.FormUtility.ValidateQuery(res))
        {
            res = ucSalesProd1.RefreshData(_batchno);

            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                ucSalesProd1.ColumnVisible(0, false);
                ucSalesProd1.ColumnVisible(1, false);
                ucSalesProd1.ColumnVisible(9, false);
                ucSalesProd1.ColumnVisible(10, false);
                ucSalesProd1.ColumnVisible(11, false);                          
            }

        }

    }      

    }
    catch (Exception ex)
    {
    MessageBox.Show(ex.ToString());
    }
    }
    #endregion[]
    #region[User_Function]
    public void refresh(string batchno) 
    {
    try
    {
    Result res = ucSalesCheckList1.RefreshData(batchno);
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
        res = ucSalesProd1.RefreshData(batchno);
        ISM.Template.FormUtility.ValidateQuery(res);
        numBatchNo.Text = batchno;
    }
    }
    catch (Exception ex) 
    { 
    MessageBox.Show(ex.Message); 
    }
    }
    private void btnPrinter_Click(object sender, EventArgs e)
    {
        PrindReportSerial();
    }
    public void Print(System.Drawing.Font font, string printername, string text)
    {
    try
    {
    using (PrintDocument doc = new PrintDocument())
    {
        _font = font;
        _text = text;
        doc.PrinterSettings.PrinterName = printername;
        doc.PrintPage += new PrintPageEventHandler(doc_PrintPage);
        doc.Print();
    }
    }
    catch (Exception ex)
    {
    }
    }
    private void doc_PrintPage(object sender, PrintPageEventArgs e)
    {
    int y = e.MarginBounds.Y;
    e.Graphics.DrawString(_text, _font, Brushes.Black, 300, y);
    for (int i = 1; i <= 5; i++)
    {
    Console.WriteLine(i);
    }
    }
    public void SalesPrint(string Batchno, decimal Score, int IsVat, decimal PaydAmount, decimal ChangeAmount, string BillNo,decimal oldpaidamount) 
    /*Batchno/Багцын дугаар||Score/Оноо||IsVat/НӨАТ||TotalAmount/Төлсөн дүн||ChangeAmount/Хариулт*/
    {
    Result res = _remote.Connection.Call(_remote.User.UserNo, 501, 600006, 600006, new object[] { Batchno });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
    DataTable prodlist = new DataTable();
    prodlist.Columns.Add("salesno",typeof(string));
    prodlist.Columns.Add("CUSTOMERNO",typeof(string));
    prodlist.Columns.Add("PRODCODE",typeof(string));
    prodlist.Columns.Add("PRODTYPE",typeof(string));
    prodlist.Columns.Add("PRODNAME",typeof(string));
    prodlist.Columns.Add("PRICE",typeof(string));
    prodlist.Columns.Add("DISCOUNT",typeof(string));
    prodlist.Columns.Add("SALESAMOUNT",typeof(string));
    prodlist.Columns.Add("QUANTITY",typeof(string));
    prodlist.Columns.Add("FLAG", typeof(string));
    DataTable dt = res.Data.Tables[0];
    string salesno = "";

    foreach (DataRow dr in dt.Rows)
    {
        DataRow drow = prodlist.NewRow();
        drow["SALESNO"] = dr["SALESNO"];
        drow["CUSTOMERNO"] = dr["CUSTOMERNO"];
        drow["PRODCODE"] = dr["PRODNO"];
        drow["PRODTYPE"] = dr["PRODTYPE"];
        drow["PRODNAME"] = dr["NAME"];
        drow["PRICE"] = dr["PRICE"];
        drow["DISCOUNT"] = dr["DISCOUNT"];
        drow["SALESAMOUNT"] = dr["SALESAMOUNT"];
        drow["QUANTITY"] = dr["QUANTITY"];
        drow["FLAG"] = dr["SALETYPE"];
        prodlist.Rows.Add(drow);
        _Data = prodlist;

        if (!salesallready.ContainsKey(Static.ToStr(drow["SALESNO"])))
        {
            if (salesno != "")
            {
                salesno = salesno + ";" + Static.ToStr(dr["SALESNO"]);
            }
            else
            {
                salesno = Static.ToStr(dr["SALESNO"]);
            }
            salesallready.Add(Static.ToStr(drow["SALESNO"]), 0);
        }
    }
    _billno = BillNo;
    _score = Score;
    OldPaidAmount = oldpaidamount;
    _paydamount = PaydAmount; // Төлсөн дүн
    _changeAmount = ChangeAmount; // Хариулт                
    _batchno = Batchno;
    isvat = IsVat;
    numBatchNo.Text = _batchno;
    salesnos = salesno.Split(';');
        if (_core.BillPrinterType == 0)
        {
            PrindReportSerial();   // SerialData 
        }
        else
        {
            PrindReport(); // win printer
        }
    }            
    }
    public void ReturnPrint(string Batchno, DateTime PostDate, string Billno, int IsVat) 
    {
    Result res = new Result();
    try
    {                       
    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 500007, 500007, new object[]{ Batchno,_core.TxnDate,  PostDate} );
    if (ISM.Template.FormUtility.ValidateQuery(res)) 
    {                    
        _Data = res.Data.Tables[0];
        string salesno = "";

        foreach (DataRow dr in _Data.Rows)
        {
            if (!salesallready.ContainsKey(Static.ToStr(dr["SALESNO"])))
            {
                if (salesno != "")
                {
                    salesno = salesno + ";" + Static.ToStr(dr["SALESNO"]);
                }
                else
                {
                    salesno = Static.ToStr(dr["SALESNO"]);
                }
                salesallready.Add(Static.ToStr(dr["SALESNO"]), 0);
            }
        }

        _DataPayType = res.Data.Tables[1];                     

        _batchno = Batchno;
        _billno = Billno;
        numBatchNo.Text = _batchno;
        salesnos = salesno.Split(';');
        isvat = IsVat;
        PrindReportReturnSerial();                    
    }
                
    }
    catch(Exception ex)
    {
    MessageBox.Show(ex.Message);
    }
    }
    #endregion[]
    #region[Return Bill]
    #region[BillPrinter]
    public void PrindReportReturn()
    {
    pdr.PrinterSettings.PrinterName = _core.CacheGetStr("BillPrinterName");
    pdr.PrintPage += new PrintPageEventHandler(pdr_PrintPage);
    pdr.BeginPrint += new PrintEventHandler(pdr_BeginPrint);
    pdr.EndPrint += new PrintEventHandler(pdr_EndPrint);
    pdr.Print();
    pdr.Dispose();
    }
    public void pdr_EndPrint(object sender, PrintEventArgs e)
    {
    try
    {
    ReturnFont.Dispose();
    ReturnHeaderFont.Dispose();
    ReturntitleFont.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }

    }// Фонт хаасан урсгал хаасан
    public void pdr_BeginPrint(object sender, PrintEventArgs e)
    {
    ReturnFont = new Font("Courier New", 9, FontStyle.Bold);
    ReturnHeaderFont = new Font("Courier New", 10, FontStyle.Bold);
    ReturntitleFont = new Font("Courier New", 11, FontStyle.Bold);
    }  // Фонт үүсгэсэн
    public void pdr_PrintPage(object sender, PrintPageEventArgs e)
    {
    Graphics g = e.Graphics;
    height = Convert.ToInt32(ReturnHeaderFont.GetHeight(g));
    ReturnHeader(g);
    }
    #endregion[]
    #region[Template]
    private void ReturnHeader(Graphics g)
    {
    try
    {
    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
        _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
        _servertime = Static.ToDateTime(res.Param.GetValue(0));
    }
    y = 10;
    strFrmt.Trimming = StringTrimming.EllipsisCharacter;
    strFrmt.FormatFlags = StringFormatFlags.NoWrap;
    g.DrawString("Скай-Резорт Билл", ReturnHeaderFont, Brushes.Black, 80, y);
    g.DrawString("[Return Bill]", ReturnHeaderFont, Brushes.Black, 82, y = y + 20);
    y = y + height * 3 / 2 + 10;
    Result resu = new Result();
    resu = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510018, 510018, new object[] { _batchno });
    if (ISM.Template.FormUtility.ValidateQuery(resu))
    {
        g.DrawString(resu.Data.Tables[0].Rows[0]["MIN(POSTDATE)"].ToString(), ReturnFont, Brushes.Black, x, y); // Борлуулалтын огноо
        g.DrawString(string.Format(_billno), ReturnFont, Brushes.Black, 190, y); // Борлуулалтын биллийн дугаар
        g.DrawString(resu.Data.Tables[0].Rows[0]["MAX(POSTDATE)"].ToString(), ReturnFont, Brushes.Black, x, y + 20);  // Буцаалтын огноо
        g.DrawString(string.Format(_billno), ReturnFont, Brushes.Black, 190, y = y + 20); // Буцаалтын биллийн дугаар
    }
    float[] ts = { 80, 40, 90, 60 };
    StringFormat strFormat = new StringFormat();
    strFormat.SetTabStops(0, ts);
    y = y + height * 3 / 2;
    g.DrawString("Нэр\tШх\tҮнэ\tНийт үнэ", ReturnFont, Brushes.Black, x, y, strFormat);
    g.DrawString("---------------------------------------", ReturnFont, Brushes.Black, x, y + 8, strFormat);
    ReturnBody(g);
    ReturnFooter(g);
    pd.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void ReturnBody(Graphics g)
    {
    Result res = new Result();
    y = y + height * 3 / 2 + 10;
    salesamountsum = 0;
    foreach (string sales in salesnos)
    {
    if (_Data != null)
    {
        var query = from row in _Data.AsEnumerable() where row.Field<string>("SALESNO") == sales select row;
        sumamount = 0;
        if (query != null && query.Count() != 0)
        {
            DataTable DT = query.CopyToDataTable();//Бараа
            foreach (DataRow dr in DT.Rows)
            {
                string inveid = Static.ToStr(dr["NAME"]);
                quantity = Static.ToInt(dr["QUANTITY"]);
                decimal unitprice = Static.ToDecimal(dr["SALESAMOUNT"]);
                price = unitprice * quantity;
                y = y + height;
                if (inveid.Length > 9)
                {
                    g.DrawString(inveid.Substring(0, 9), ReturnFont, Brushes.Black, 0, y);
                }
                else
                {
                    g.DrawString(inveid, ReturnFont, Brushes.Black, 0, y); // Барааны нэр 
                }
                g.DrawString(quantity.ToString(), ReturnFont, Brushes.Black, 80, y); // Тоо ширхэг
                g.DrawString(unitprice.ToString("#,##0"), ReturnFont, Brushes.Black, 120, y); // Нэгжийн үнэ
                g.DrawString(price.ToString("#,##0"), ReturnFont, Brushes.Black, 210, y); // Үнэ
                sumamount = sumamount + price; // Нийт дүн
                strFrmt.Alignment = StringAlignment.Far;
                strFrmt.Trimming = StringTrimming.None;
            }
            if (isvat == 1)
            {
                vat = sumamount / (_core.Vat + 1);
            }
            salesamountsum = salesamountsum + sumamount;
            g.DrawString("---------------------------------------", ReturnFont, Brushes.Black, x, y = y + 20);
            dtb = query.CopyToDataTable();
            y = y + height * 5;
            g.DrawString("Цэвэр дүн:", ReturnFont, Brushes.Black, 0, y - 60);
            g.DrawString((sumamount - vat).ToString("#,##0.00"), ReturnFont, Brushes.Black, 210, y - 60);
            g.DrawString("НӨАТ:", ReturnFont, Brushes.Black, 0, y - 45);
            g.DrawString(vat.ToString("#,##0.00.00"), ReturnFont, Brushes.Black, 210, y - 45);
            g.DrawString("Нийт дүн:", ReturnFont, Brushes.Black, 0, y - 30);
            g.DrawString(sumamount.ToString("#,##0.00"), ReturnFont, Brushes.Black, 210, y - 30);
            g.DrawString("---------------------------------------", ReturnFont, Brushes.Black, x, y - 20);
        }

        //if (_DataPayType != null)
        //{
        //    var query = from row in _Data.AsEnumerable() where row.Field<string>("SALESNO") == sales select row;
        //    sumamount = 0;
        //    if (query != null && query.Count() != 0)
        //    {
        //        DataTable DT = query.CopyToDataTable();//Бараа
        //        foreach (DataRow dr in DT.Rows)
        //        {
        //            name = Static.ToStr(dr["NAME"]);
        //            prodprice = Static.ToDecimal(dr["SALESAMOUNT"]);
        //            y = y + height;
        //            if (name.Length > 9)
        //            {
        //                g.DrawString(name.Substring(0, 9), ReturnFont, Brushes.Black, 0, y);
        //            }
        //            else
        //            {
        //                g.DrawString(name, ReturnFont, Brushes.Black, 0, y); // Барааны нэр 
        //            }
        //            g.DrawString(quantity.ToString(), ReturnFont, Brushes.Black, 80, y); // Тоо ширхэг
        //            g.DrawString(prodprice.ToString("#,##0"), ReturnFont, Brushes.Black, 120, y); // Нэгжийн үнэ
        //            g.DrawString(price.ToString("#,##0"), ReturnFont, Brushes.Black, 210, y); // Үнэ
        //            sumamount = sumamount + price; // Нийт дүн
        //            strFrmt.Alignment = StringAlignment.Far;
        //            strFrmt.Trimming = StringTrimming.None;
        //            if (isvat == 1) { vat = sumamount / (_core.Vat + 1); }
        //        }
        //        salesamountsum = salesamountsum + sumamount;
        //        g.DrawString("---------------------------------------", ReturnFont, Brushes.Black, x, y = y + 20);
        //        dtb = query.CopyToDataTable();
        //        y = y + height * 5;
        //        g.DrawString(name, ReturnFont, Brushes.Black, 0, y - 60);
        //        g.DrawString((sumamount - vat).ToString("#,##0"), ReturnFont, Brushes.Black, 210, y - 60);                        
        //        g.DrawString("---------------------------------------", ReturnFont, Brushes.Black, x, y - 20);
        //    }
        //}

    }
    }
    }
    private void ReturnFooter(Graphics g)
    {
     g.DrawString("Cashier:", ReturnFont, Brushes.Black, x, y = y + 20);
     g.DrawString(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)), ReturnFont, Brushes.Black, 100, y);
    }
    #endregion[]
    #endregion[]
    #region[Return Bill Serial]
    #region[BillPrinter]
    public void PrindReportReturnSerial()
    {
        Printer _serial = new Printer();
        _serial.Init(_core.BillPrinterPort, 9600, 8, StopBits.One, Parity.None);
        _serial.Open();
        ReturnHeader(_serial);
    }    
    #endregion[]
    #region[Template]
    private void ReturnHeader(Printer _serial)
    {
        try
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                _servertime = Static.ToDateTime(res.Param.GetValue(0));
            }
            y = 10;
            strFrmt.Trimming = StringTrimming.EllipsisCharacter;
            strFrmt.FormatFlags = StringFormatFlags.NoWrap;
            _serial.Print("Скай-Резорт Билл");
            _serial.Print("[Return Bill]");
            y = y + height * 3 / 2 + 10;
            Result resu = new Result();
            resu = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510018, 510018, new object[] { _batchno });
            if (ISM.Template.FormUtility.ValidateQuery(resu))
            {
                _serial.Print(resu.Data.Tables[0].Rows[0]["MIN(POSTDATE)"].ToString());
                _serial.Print(string.Format(_billno));
                _serial.Print(resu.Data.Tables[0].Rows[0]["MAX(POSTDATE)"].ToString());
                _serial.Print(string.Format(_billno));
            }
            float[] ts = { 80, 40, 90, 60 };
            StringFormat strFormat = new StringFormat();
            strFormat.SetTabStops(0, ts);
            _serial.Print("Нэр\tШх\tҮнэ\tНийт үнэ");
            _serial.Print("---------------------------------------");
            ReturnBody(_serial);
            ReturnFooter(_serial);
            pd.Dispose();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void ReturnBody(Printer _serial)
    {
        Result res = new Result();
        y = y + height * 3 / 2 + 10;
        salesamountsum = 0;
        foreach (string sales in salesnos)
        {
            if (_Data != null)
            {
                var query = from row in _Data.AsEnumerable() where row.Field<string>("SALESNO") == sales select row;
                sumamount = 0;
                if (query != null && query.Count() != 0)
                {
                    DataTable DT = query.CopyToDataTable();//Бараа
                    foreach (DataRow dr in DT.Rows)
                    {
                        string inveid = Static.ToStr(dr["NAME"]);
                        quantity = Static.ToInt(dr["QUANTITY"]);
                        decimal unitprice = Static.ToDecimal(dr["SALESAMOUNT"]);
                        price = unitprice * quantity;
                        if (inveid.Length > 9)
                        {
                            _serial.Print(inveid.Substring(0, 9));
                        }
                        else
                        {
                            _serial.Print(inveid);
                        }
                        _serial.Print(quantity.ToString());
                        _serial.Print(unitprice.ToString("#,##0"));
                        _serial.Print(price.ToString("#,##0"));
                        sumamount = sumamount + price; // Нийт дүн
                        strFrmt.Alignment = StringAlignment.Far;
                        strFrmt.Trimming = StringTrimming.None;
                    }
                    if (isvat == 1)
                    {
                        vat = sumamount / (_core.Vat + 1);
                    }
                    salesamountsum = salesamountsum + sumamount;
                    _serial.Print("---------------------------------------");
                    dtb = query.CopyToDataTable();
                    y = y + height * 5;
                    _serial.Print("Цэвэр дүн:");
                    _serial.Print((sumamount - vat).ToString("#,##0.00"));
                    _serial.Print("НӨАТ:");
                    _serial.Print(vat.ToString("#,##0.00.00"));
                    _serial.Print("Нийт дүн:");
                    _serial.Print(sumamount.ToString("#,##0.00"));
                    _serial.Print("---------------------------------------");
                }

                //if (_DataPayType != null)
                //{
                //    var query = from row in _Data.AsEnumerable() where row.Field<string>("SALESNO") == sales select row;
                //    sumamount = 0;
                //    if (query != null && query.Count() != 0)
                //    {
                //        DataTable DT = query.CopyToDataTable();//Бараа
                //        foreach (DataRow dr in DT.Rows)
                //        {
                //            name = Static.ToStr(dr["NAME"]);
                //            prodprice = Static.ToDecimal(dr["SALESAMOUNT"]);
                //            y = y + height;
                //            if (name.Length > 9)
                //            {
                //                g.DrawString(name.Substring(0, 9), ReturnFont, Brushes.Black, 0, y);
                //            }
                //            else
                //            {
                //                g.DrawString(name, ReturnFont, Brushes.Black, 0, y); // Барааны нэр 
                //            }
                //            g.DrawString(quantity.ToString(), ReturnFont, Brushes.Black, 80, y); // Тоо ширхэг
                //            g.DrawString(prodprice.ToString("#,##0"), ReturnFont, Brushes.Black, 120, y); // Нэгжийн үнэ
                //            g.DrawString(price.ToString("#,##0"), ReturnFont, Brushes.Black, 210, y); // Үнэ
                //            sumamount = sumamount + price; // Нийт дүн
                //            strFrmt.Alignment = StringAlignment.Far;
                //            strFrmt.Trimming = StringTrimming.None;
                //            if (isvat == 1) { vat = sumamount / (_core.Vat + 1); }
                //        }
                //        salesamountsum = salesamountsum + sumamount;
                //        g.DrawString("---------------------------------------", ReturnFont, Brushes.Black, x, y = y + 20);
                //        dtb = query.CopyToDataTable();
                //        y = y + height * 5;
                //        g.DrawString(name, ReturnFont, Brushes.Black, 0, y - 60);
                //        g.DrawString((sumamount - vat).ToString("#,##0"), ReturnFont, Brushes.Black, 210, y - 60);                        
                //        g.DrawString("---------------------------------------", ReturnFont, Brushes.Black, x, y - 20);
                //    }
                //}

            }
        }
    }
    private void ReturnFooter(Printer _serial)
    {
        _serial.Print("Cashier:");
        _serial.Print(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)));
    }
    #endregion[]
    #endregion[]
    #region[Golf Bill]
    #region[BillPrinter]
    public void PrindReport()
    {
    pd.PrinterSettings.PrinterName = _core.CacheGetStr("BillPrinterName");
    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
    pd.BeginPrint += new PrintEventHandler(pd_BeginPrint);
    pd.EndPrint += new PrintEventHandler(pd_EndPrint);
    pd.Print();
    pd.Dispose();
    }
    public void pd_EndPrint(object sender, PrintEventArgs e)
    {
    try
    {
    rptFont.Dispose();
    hdrFont.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }

    }// Фонт хаасан урсгал хаасан
    public void pd_BeginPrint(object sender, PrintEventArgs e)
    {
    rptFont = new Font("Courier New", 9, FontStyle.Bold);
    hdrFont = new Font("Courier New", 10, FontStyle.Bold);
    titleFont = new Font("Courier New", 10, FontStyle.Bold);
    }  // Фонт үүсгэсэн
    public void pd_PrintPage(object sender, PrintPageEventArgs e)
    {
    Graphics g = e.Graphics;
    height = Convert.ToInt32(hdrFont.GetHeight(g));
    Header(g);
    }
    #endregion[]
    #region[Template]
    private void Header(Graphics g)
    {
    try
    {
    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
        _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
        _servertime = Static.ToDateTime(res.Param.GetValue(0));
    }
    else
    {
        MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
        return;
    }
    y = 10;
    strFrmt.Trimming = StringTrimming.EllipsisCharacter;
    strFrmt.FormatFlags = StringFormatFlags.NoWrap;
    g.DrawString("Скай-Резорт Билл", hdrFont, Brushes.Black, 40, 10);
    y = y + height * 3 / 2 + 10;
    g.DrawString(_servertime.ToString(), rptFont, Brushes.Black, x, y);
    g.DrawString(string.Format(_billno), rptFont, Brushes.Black, 190, y); // Биллийн дугаар хэвлэх
    float[] ts = { 80, 40, 90, 60 };
    StringFormat strFormat = new StringFormat();
    strFormat.SetTabStops(0, ts);
    y = y + height * 3 / 2;
    g.DrawString("Нэр\tШх\tҮнэ\tНийт үнэ", rptFont, Brushes.Black, x, y, strFormat);
    g.DrawString("---------------------------------------", rptFont, Brushes.Black, x, y + 20, strFormat);
    Body(g);
    Footer(g);
    pd.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void Body(Graphics g)
    {
    Result res = new Result();
    y = y + height * 3 / 2 + 10;
    salesamountsum = 0;
    foreach (string sales in salesnos)
    {
    if (_Data != null)
    {
        var query = from row in _Data.AsEnumerable() where row.Field<string>("SALESNO") == sales select row;
        sumamount = 0;
        if (query != null && query.Count() != 0)
        {                        
            DataTable DT = query.CopyToDataTable();//Бараа
            foreach (DataRow dr in DT.Rows)
            {
                if (Static.ToInt(dr["FLAG"]) != 3)
                {
                    string inveid = Static.ToStr(dr["PRODNAME"]);
                    quantity = Static.ToInt(dr["QUANTITY"]);
                    decimal unitprice = Static.ToDecimal(dr["SALESAMOUNT"]);
                    price = unitprice * quantity;
                    y = y + height;
                    if (inveid.Length > 9)
                    {
                        g.DrawString(inveid.Substring(0, 9), rptFont, Brushes.Black, 0, y);
                    }
                    else
                    {
                        g.DrawString(inveid, rptFont, Brushes.Black, 0, y); // Барааны нэр 
                    }
                    g.DrawString(quantity.ToString(), rptFont, Brushes.Black, 80, y); // Тоо ширхэг
                    g.DrawString(unitprice.ToString("#,##0"), rptFont, Brushes.Black, 120, y); // Нэгжийн үнэ
                    g.DrawString(price.ToString("#,##0"), rptFont, Brushes.Black, 210, y); // Үнэ
                    sumamount = sumamount + price; // Нийт дүн
                    strFrmt.Alignment = StringAlignment.Far;
                    strFrmt.Trimming = StringTrimming.None;                                
                }
            }
            if (isvat == 1) { vat = sumamount / (_core.Vat + 1); }
            salesamountsum = salesamountsum + sumamount;
            g.DrawString("---------------------------------------", rptFont, Brushes.Black, x, y = y + 20);
            dtb = query.CopyToDataTable();        
            y = y + height * 5;                        
            g.DrawString("Цэвэр дүн:", rptFont, Brushes.Black, 0, y - 60);
            g.DrawString((sumamount - vat).ToString("#,##0.00"), rptFont, Brushes.Black, 180, y - 60); 
            g.DrawString("НӨАТ:", rptFont, Brushes.Black, 0, y - 45);
            g.DrawString(vat.ToString("#,##0.00"), rptFont, Brushes.Black, 180, y - 45);
            g.DrawString("Нийт дүн:", rptFont, Brushes.Black, 0, y - 30);
            g.DrawString(sumamount.ToString("#,##0.00"), rptFont, Brushes.Black, 180, y - 30);
            g.DrawString("---------------------------------------", rptFont, Brushes.Black, x, y - 20);     
        }
    }
    }
    }
    private void Footer(Graphics g)
    {
    Result res = new Result();
    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510014, 510014, new object[] { numBatchNo.Text });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
    DTable = res.Data.Tables[0];
    if (res.Data.Tables[0].Rows.Count != 0)
    {
        if (_paydamount != 0)
        {
            for (int i = 0; i < DTable.Rows.Count; i++)//Төлбөрийн хэрэгсэл
            {
                y = y + 20;
                _name = Static.ToStr(DTable.Rows[i]["NAME"]);
                _payenmenttype = Static.ToDecimal(DTable.Rows[i]["paymenttype"]);
                _payenmentamnt = Static.ToDecimal(DTable.Rows[i]["AMOUNT"]);
                g.DrawString(_name + ":", rptFont, Brushes.Black, 0, y);
                g.DrawString(_payenmentamnt.ToString("#,##0"), rptFont, Brushes.Black, 180, y);
            }
        }
        else
        {
            for (int i = 0; i < DTable.Rows.Count; i++)//Төлбөрийн хэрэгсэл
            {
                y = y + 20;
                _changeAmount = Static.ToDecimal(DTable.Rows[0]["CHARGEAMOUNT"]);
                _name = Static.ToStr(DTable.Rows[i]["NAME"]);
                _payenmenttype = Static.ToDecimal(DTable.Rows[i]["paymenttype"]);
                _payenmentamnt = Static.ToDecimal(DTable.Rows[i]["AMOUNT"]);
                //_paydamount = _paydamount + _payenmentamnt;
                g.DrawString(_name + ":", rptFont, Brushes.Black, 0, y);
                g.DrawString(sumamount.ToString("#,##0"), rptFont, Brushes.Black, 180, y);
            }
        }
        g.DrawString("---------------------------------------", rptFont, Brushes.Black, x, y = y + 10);
    }
    if (_paydamount != 0)
    {
        g.DrawString("Нийт дүн:", rptFont, Brushes.Black, 0, y = y + 20);
        g.DrawString(salesamountsum.ToString("#,##0.00"), rptFont, Brushes.Black, 180, y);

        if (OldPaidAmount != 0)
        {
            g.DrawString("Өмнө төлөгдсөн дүн:", rptFont, Brushes.Black, 0, y = y + 20);
            g.DrawString(OldPaidAmount.ToString("#,##0.00"), rptFont, Brushes.Black, 180, y);
        }
        g.DrawString("Төлсөн дүн:", rptFont, Brushes.Black, 0, y =y + 20);
        if(_changeAmount>=0)
            g.DrawString((_paydamount).ToString("#,##0.00"), rptFont, Brushes.Black, 180, y);
        else
            g.DrawString((_paydamount).ToString("#,##0.00"), rptFont, Brushes.Black, 210, y);
        g.DrawString("Хариулт:", rptFont, Brushes.Black, 0, y= y + 20);
        g.DrawString(_changeAmount.ToString("#,##0.00"), rptFont, Brushes.Black, 180, y);
    }
    else
    {                    
        g.DrawString("Нийт дүн:", rptFont, Brushes.Black, 0, y = y + 20);
        g.DrawString(salesamountsum.ToString("#,##0.00"), rptFont, Brushes.Black, 180, y);
    }
    if (Static.ToInt(_score) != 0)
    {
        g.DrawString("Оноо:", titleFont, Brushes.Black, 0, y = y + 20);
        g.DrawString(_score.ToString("#,##0"), rptFont, Brushes.Black, 180, y);
    }
    g.DrawString("---------------------------------------", rptFont, Brushes.Black, x, y = y + 10);
    g.DrawString("Кассын ажилтан:", rptFont, Brushes.Black, 0, y = y + 20);
    g.DrawString(string.Format("{0}.{1}", core.RemoteObject.User.UserLName, core.RemoteObject.User.UserFName.Substring(0,1)), rptFont, Brushes.Black, 140, y);
    g.DrawString("Баярлалаа!", rptFont, Brushes.Black, 0, y = y + 20);
    g.DrawString("---------------------------------------", rptFont, Brushes.Black, x, y = y + 20);

    if (dtb.Rows.Count != 0)
    {

        g.DrawString("Урамшууллын бараа----------------------", rptFont, Brushes.Black, x, y = y + 40);
        foreach (DataRow dr in dtb.Rows)//урамшууллын бараа
        {
            if (Static.ToInt(dr["PRICE"]) == 0)
            {                            
                string inveid = Static.ToStr(dr["prodname"]);
                quantity = Static.ToInt(dr["quantity"]);
                decimal unitprice = Static.ToDecimal(0);
                if (inveid.Length > 9)
                {
                    g.DrawString(inveid.Substring(0, 9), rptFont, Brushes.Black, 0, y = y + 20);
                }
                else
                    g.DrawString(inveid, rptFont, Brushes.Black, 0, y = y + 20);
                strFrmt.Alignment = StringAlignment.Far;
                strFrmt.Trimming = StringTrimming.None;
                g.DrawString(quantity.ToString(), rptFont, Brushes.Black, 90, y);
                g.DrawString(0.ToString("#,##0"), rptFont, Brushes.Black, 120, y);
                g.DrawString(0.ToString("#,##0"), rptFont, Brushes.Black, 210, y);                            
            }
            g.DrawString("---------------------------------------", rptFont, Brushes.Black, x, y + 40);
        }                    
    }
    }    
    }
    #endregion[]
    #endregion[]
    #region[Golf Bill Serial]
    #region[BillPrinter]
    public void PrindReportSerial()
    {
        try
        {
            Printer _serial = new Printer();
            _serial.Init(_core.BillPrinterPort, 9600, 8, StopBits.One, Parity.None);
            _serial.Open();
            Header(_serial);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }   
    #endregion[]
    #region[Template]
    private void Header(Printer _serial)
    {
        try
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                _servertime = Static.ToDateTime(res.Param.GetValue(0));
            }
            else
            {
                MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
                return;
            }
            strFrmt.Trimming = StringTrimming.EllipsisCharacter;
            strFrmt.FormatFlags = StringFormatFlags.NoWrap;
            _serial.Print("Скай-Резорт Билл");
            y = y + height * 3 / 2 + 10;
            _serial.Print(_servertime.ToString());
            _serial.Print(string.Format(_billno));
            float[] ts = { 80, 40, 90, 60 };
            StringFormat strFormat = new StringFormat();
            strFormat.SetTabStops(0, ts);
            y = y + height * 3 / 2;            
            _serial.Print("Нэр    Шх    Үнэ   Нийт үнэ");
            _serial.Print("---------------------------------------");
            Body(_serial);
            Footer(_serial);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void Body(Printer _serial)
    {
        Result res = new Result();
        salesamountsum = 0;
        foreach (string sales in salesnos)
        {
            if (_Data != null)
            {
                var query = from row in _Data.AsEnumerable() where row.Field<string>("SALESNO") == sales select row;
                sumamount = 0;
                if (query != null && query.Count() != 0)
                {
                    DataTable DT = query.CopyToDataTable();//Бараа
                    foreach (DataRow dr in DT.Rows)
                    {
                        if (Static.ToInt(dr["FLAG"]) != 3)
                        {
                            string inveid = Static.ToStr(dr["PRODNAME"]);
                            quantity = Static.ToInt(dr["QUANTITY"]);
                            decimal unitprice = Static.ToDecimal(dr["SALESAMOUNT"]);
                            price = unitprice * quantity;
                            y = y + height;
                            if (inveid.Length > 9)
                            {
                                _serial.Print(inveid.Substring(0, 9));
                            }
                            else
                            {
                                _serial.Print(inveid);
                            }
                            _serial.Print(quantity.ToString());
                            _serial.Print(unitprice.ToString("#,##0"));
                            _serial.Print(price.ToString("#,##0"));
                            sumamount = sumamount + price; // Нийт дүн
                            strFrmt.Alignment = StringAlignment.Far;
                            strFrmt.Trimming = StringTrimming.None;
                        }
                    }
                    if (isvat == 1) { vat = sumamount / (_core.Vat + 1); }
                    salesamountsum = salesamountsum + sumamount;
                    _serial.Print("---------------------------------------");
                    dtb = query.CopyToDataTable();
                    y = y + height * 5;
                    _serial.Print("Цэвэр дүн:         " + (sumamount - vat).ToString("#,##0.00"));                                     
                    _serial.Print("НӨАТ:        " + vat.ToString("#,##0.00"));
                    _serial.Print("Нийт дүн:        "+sumamount.ToString("#,##0.00"));
                    _serial.Print("---------------------------------------");
                }
            }
        }
    }
    private void Footer(Printer _serial)
    {
        Result res = new Result();
        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510014, 510014, new object[] { numBatchNo.Text });
        if (ISM.Template.FormUtility.ValidateQuery(res))
        {
            DTable = res.Data.Tables[0];
            if (res.Data.Tables[0].Rows.Count != 0)
            {
                if (_paydamount != 0)
                {
                    for (int i = 0; i < DTable.Rows.Count; i++)//Төлбөрийн хэрэгсэл
                    {
                        y = y + 20;
                        _name = Static.ToStr(DTable.Rows[i]["NAME"]);
                        _payenmenttype = Static.ToDecimal(DTable.Rows[i]["paymenttype"]);
                        _payenmentamnt = Static.ToDecimal(DTable.Rows[i]["AMOUNT"]);
                        _serial.Print(_name + ":        " + _payenmentamnt.ToString("#,##0"));
                    }
                }
                else
                {
                    for (int i = 0; i < DTable.Rows.Count; i++)//Төлбөрийн хэрэгсэл
                    {
                        y = y + 20;
                        _changeAmount = Static.ToDecimal(DTable.Rows[0]["CHARGEAMOUNT"]);
                        _name = Static.ToStr(DTable.Rows[i]["NAME"]);
                        _payenmenttype = Static.ToDecimal(DTable.Rows[i]["paymenttype"]);
                        _payenmentamnt = Static.ToDecimal(DTable.Rows[i]["AMOUNT"]);
                        //_paydamount = _paydamount + _payenmentamnt;
                        _serial.Print(_name + ":            " + sumamount.ToString("#,##0"));
                    }
                }
                _serial.Print("---------------------------------------");
            }
            if (_paydamount != 0)
            {
                _serial.Print("Нийт дүн:        " + salesamountsum.ToString("#,##0.00"));

                if (OldPaidAmount != 0)
                {
                    _serial.Print("Өмнө төлөгдсөн дүн:      " + OldPaidAmount.ToString("#,##0.00"));
                }
                _serial.Print("Төлсөн дүн:");
                if (_changeAmount >= 0)
                    _serial.Print((_paydamount).ToString("#,##0.00"));

                else
                _serial.Print((_paydamount).ToString("#,##0.00"));
                _serial.Print("Хариулт:     "+_changeAmount.ToString("#,##0.00"));
            }
            else
            {
                _serial.Print("Нийт дүн:    " + salesamountsum.ToString("#,##0.00"));
            }
            if (Static.ToInt(_score) != 0)
            {
                _serial.Print("Оноо:        " + _score.ToString("#,##0"));
            }
            _serial.Print("---------------------------------------");
            _serial.Print("Cashier:  "+string.Format("{0}.{1}", core.RemoteObject.User.UserLName, core.RemoteObject.User.UserFName.Substring(0, 1)));
            _serial.Print("Thank you!");
            _serial.Print("---------------------------------------");

            if (dtb.Rows.Count != 0)
            {
                _serial.Print("Free Product----------------------");//Урамшууллын бараа----------------------"
                foreach (DataRow dr in dtb.Rows)//урамшууллын бараа
                {
                    if (Static.ToInt(dr["PRICE"]) == 0)
                    {
                        string inveid = Static.ToStr(dr["prodname"]);
                        quantity = Static.ToInt(dr["quantity"]);
                        decimal unitprice = Static.ToDecimal(0);
                        if (inveid.Length > 9)
                        {
                            _serial.Print(inveid.Substring(0, 9));
                        }
                        else
                        _serial.Print(inveid);
                        strFrmt.Alignment = StringAlignment.Far;
                        strFrmt.Trimming = StringTrimming.None;
                        _serial.Print(quantity.ToString());
                        _serial.Print(0.ToString("#,##0"));
                        _serial.Print(0.ToString("#,##0"));
                    }
                    _serial.Print("---------------------------------------");
                }
            }
        }
        _serial.Close();
    }
    #endregion[]
    #endregion[]
    #region[Cashier Closure]
    public void PintCashierClosure( decimal _startAmt, decimal _cashtotal, decimal _deposit, decimal _cashsale)
    {
    StartAmt = _startAmt;
    Cashtotal = _cashtotal;
    Deposit = _deposit;
    CashSale = _cashsale;
    pdc.PrinterSettings.PrinterName = _core.CacheGetStr("BillPrinterName");
    pdc.PrintPage += new PrintPageEventHandler(pdc_PrintPage);
    pdc.BeginPrint += new PrintEventHandler(pdc_BeginPrint);
    pdc.EndPrint += new PrintEventHandler(pdc_EndPrint);
    pdc.Print();
    pdc.Dispose();
    }        
    public void pdc_EndPrint(object sender, PrintEventArgs e)
    {
    try
    {
    repeatFont.Dispose();
    headerFont.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }

    }// Фонт хаасан урсгал хаасан
    public void pdc_BeginPrint(object sender, PrintEventArgs e)
    {
    repeatFont = new Font("Courier New", 10, FontStyle.Bold);
    headerFont = new Font("Courier New", 11, FontStyle.Bold);
    //titleFont = new Font("Courier", 8, FontStyle.Bold);
    }
    public void pdc_PrintPage(object sender, PrintPageEventArgs e)
    {
    Graphics g = e.Graphics;
    height = Convert.ToInt32(headerFont.GetHeight(g));
    HeaderCashier(g);
    }
    #region[Template]
    private void HeaderCashier(Graphics g)
    {
    try
    {
    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
        _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
        _servertime = Static.ToDateTime(res.Param.GetValue(0));
    }
    else
    {
        MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
        return;
    }
    y = 10;
    strFrmt.Trimming = StringTrimming.EllipsisCharacter;
    strFrmt.FormatFlags = StringFormatFlags.NoWrap;
    g.DrawString("Скай-Резорт Билл", headerFont, Brushes.Black, x = x + 60, y);
    g.DrawString("[Cashier Closure]", headerFont, Brushes.Black, x, y = y + 20);
    g.DrawString(_servertime.ToString("yyyy.MM.dd hh:mm:ss"), repeatFont, Brushes.Black, x = x - 60, y = y + 20);
    g.DrawString(string.Format("Pos:{0}", _core.POSNo), repeatFont, Brushes.Black, x = x + 180, y);
    g.DrawString(string.Format("Res:{0}", _core.RemoteObject.User.UserNo), repeatFont, Brushes.Black, x = x + 60, y);// Биллийн дугаар хэвлэх
    g.DrawString("[ShiftNo:]", repeatFont, Brushes.Black, x - 240, y = y + 20);
    g.DrawString(_shiftno.ToString(), repeatFont, Brushes.Black, x - 140, y);

    float[] ts = { 100, 50, 60, 50 };
    StringFormat strFormat = new StringFormat();
    strFormat.SetTabStops(0, ts);
    y = y + 20;
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x = x - 240, y, strFormat);
    DepositTotal(g);
    pdc.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void DepositTotal(Graphics g)
    {
        Result res = new Result();           
        y = y + 20;
        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510016, 510016, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo, _shiftno });
        if (ISM.Template.FormUtility.ValidateQuery(res))
        {

            g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y);
            g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y);
            g.DrawString("[Deposit Total]", repeatFont, Brushes.Black, 0, y = y + 20);
            foreach(DataRow dr in res.Data.Tables[0].Rows)
            {
                Total = Static.ToDecimal(dr["SUM(C.AMOUNT)"]);
                name = Static.ToStr(dr["NAME"]);
                CashTotalAmt = CashTotalAmt + Total;
                //if(_core.CashPayment == Static.ToStr(dr["PAYMENTTYPE"]))
                //{
                //    CashSaleTotal = CashSaleTotal + Static.ToDecimal(dr["SUM(C.AMOUNT)"]);
                //}
                g.DrawString(name, repeatFont, Brushes.Black, 30, y = y + 20);
                g.DrawString(Total.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
            }
            g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
            g.DrawString("Total:", repeatFont, Brushes.Black, 30, y = y + 20);
            g.DrawString(Deposit.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
            g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
                
        }
        else
        {
        MessageBox.Show(res.ResultDesc);
        }
           
    CashTotal(g);
    }
    private void CashTotal(Graphics g)
    {
        CashSale = CashSaleTotal - CashSaleTotal + 100;
        Result res = new Result();
        y = y + 20;
        g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y);
        g.DrawString("[Cash Total]", repeatFont, Brushes.Black, 0, y = y + 20);
        g.DrawString("Start Amt:", repeatFont, Brushes.Black, 30, y = y + 20);
        g.DrawString(StartAmt.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
        g.DrawString("Cash Sale:", repeatFont, Brushes.Black, 30, y = y + 20);  // Борлуулалтаар хийгдсэн гүйлгээний нийт дүн
        g.DrawString(CashSaleTotal.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
        g.DrawString("Deposit Amt:", repeatFont, Brushes.Black, 30, y = y + 20);
        g.DrawString((Deposit).ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
        g.DrawString("Cash Total:", repeatFont, Brushes.Black, 30, y = y + 20);
        g.DrawString((Cashtotal).ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
        g.DrawString("Over /Lake:", repeatFont, Brushes.Black, 30, y = y + 20);
        g.DrawString((Deposit - StartAmt).ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
        g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
        SaleTotal(g);
    }
    private void SaleTotal(Graphics g)
    {
        Result res = new Result();
        y = y + 20;
        g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y);
        g.DrawString("[Sale Total]", repeatFont, Brushes.Black, 0, y = y + 20);
        g.DrawString("Sale:", repeatFont, Brushes.Black, 30, y = y + 20);
        g.DrawString(CashSale.ToString("#,##0.00"), repeatFont, Brushes.Black, 180, y);
        g.DrawString("Return:", repeatFont, Brushes.Black, 30, y = y + 20);
        g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
        g.DrawString("Total:", repeatFont, Brushes.Black, 30, y = y + 20);
        decimal _total = Deposit + CashSale;
        g.DrawString(_total.ToString("#,##0.00"), repeatFont, Brushes.Black, 180, y);
        g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
        g.DrawString("Cashier:", repeatFont, Brushes.Black, 30, y = y + 20);
        g.DrawString(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)), repeatFont, Brushes.Black, 110, y);
    }
    #endregion[]        
    #endregion[]
    #region[Cashier Closure Serial]
    public void PintCashierClosureSerial(decimal _startAmt, decimal _cashtotal, decimal _deposit, decimal _cashsale)
    {
        StartAmt = _startAmt;
        Cashtotal = _cashtotal;
        Deposit = _deposit;
        CashSale = _cashsale;
        Printer _serial = new Printer();
        _serial.Init(_core.BillPrinterPort, 9600, 8, StopBits.One, Parity.None);
        _serial.Open();
        HeaderCashierSerial(_serial);
    }
    #region[Template]
    private void HeaderCashierSerial(Printer _serial)
    {
        try
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                _servertime = Static.ToDateTime(res.Param.GetValue(0));
            }
            else
            {
                MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
                return;
            }
            y = 10;
            strFrmt.Trimming = StringTrimming.EllipsisCharacter;
            strFrmt.FormatFlags = StringFormatFlags.NoWrap;
            _serial.Print("Скай-Резорт Билл");
            _serial.Print("[Cashier Closure]");
            _serial.Print(_servertime.ToString("yyyy.MM.dd hh:mm:ss"));
            _serial.Print(string.Format("Pos:{0}", _core.POSNo));
            _serial.Print(string.Format("Res:{0}", _core.RemoteObject.User.UserNo));
            _serial.Print("[ShiftNo:]");
            _serial.Print(_shiftno.ToString());

            float[] ts = { 100, 50, 60, 50 };
            StringFormat strFormat = new StringFormat();
            strFormat.SetTabStops(0, ts);
            y = y + 20;
            _serial.Print("---------------------------------");
            DepositTotalSerial(_serial);
            pdc.Dispose();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void DepositTotalSerial(Printer _serial)
    {
        Result res = new Result();
        y = y + 20;
        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510016, 510016, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo, _shiftno });
        if (ISM.Template.FormUtility.ValidateQuery(res))
        {
            _serial.Print("---------------------------------");
            _serial.Print("---------------------------------");
            _serial.Print("[Deposit Total]");
            foreach (DataRow dr in res.Data.Tables[0].Rows)
            {
                Total = Static.ToDecimal(dr["SUM(C.AMOUNT)"]);
                name = Static.ToStr(dr["NAME"]);
                CashTotalAmt = CashTotalAmt + Total;
                //if (_core.CashPayment == Static.ToStr(dr["PAYMENTTYPE"]))
                //{
                //    CashSaleTotal = CashSaleTotal + Static.ToDecimal(dr["SUM(C.AMOUNT)"]);
                //}
                _serial.Print(name);
                _serial.Print(Total.ToString("#,##0"));
            }
            _serial.Print("---------------------------------");
            _serial.Print("Total:");
            _serial.Print(Deposit.ToString("#,##0"));
            _serial.Print("---------------------------------");

        }
        else
        {
            MessageBox.Show(res.ResultDesc);
        }

        CashTotalSerial(_serial);
    }
    private void CashTotalSerial(Printer _serial)
    {
        CashSale = CashSaleTotal - CashSaleTotal + 100;
        Result res = new Result();
        y = y + 20;
        _serial.Print("---------------------------------");
        _serial.Print("[Cash Total]");
        _serial.Print("Start Amt:");
        _serial.Print(StartAmt.ToString("#,##0"));
        _serial.Print("Cash Sale:");
        _serial.Print(CashSaleTotal.ToString("#,##0"));
        _serial.Print("Deposit Amt:");
        _serial.Print((Deposit).ToString("#,##0"));
        _serial.Print("Cash Total:");
        _serial.Print((Cashtotal).ToString("#,##0"));
        _serial.Print("Over /Lake:");
        _serial.Print((Deposit - StartAmt).ToString("#,##0"));
        _serial.Print("---------------------------------");
        SaleTotalSerial(_serial);
    }
    private void SaleTotalSerial(Printer _serial)
    {
        Result res = new Result();
        y = y + 20;
        _serial.Print("---------------------------------");
        _serial.Print("[Sale Total]");
        _serial.Print("Sale:");
        _serial.Print(CashSale.ToString("#,##0.00"));
        _serial.Print("Return:");
        _serial.Print("---------------------------------");
        _serial.Print("Total:");
        decimal _total = Deposit + CashSale;
        _serial.Print(_total.ToString("#,##0.00"));
        _serial.Print("---------------------------------");
        _serial.Print("Cashier:");
        _serial.Print(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)));
    }
    #endregion[]
    #endregion[]
    #region[Cashier Add Transaction]
    public void PintCashierAdd(decimal _startAmt, DataTable DT)
    {
    if (DT.Rows.Count == 0)
    {
    return;
    }
    else
    {
    StartAmt = _startAmt;
    DTAdd = DT;
    pdcadd.PrinterSettings.PrinterName = _core.CacheGetStr("BillPrinterName");
    pdcadd.PrintPage += new PrintPageEventHandler(pdcadd_PrintPage);
    pdcadd.BeginPrint += new PrintEventHandler(pdcadd_BeginPrint);
    pdcadd.EndPrint += new PrintEventHandler(pdcadd_EndPrint);
    pdcadd.Print();
    pdcadd.Dispose();
    }
    }        
    public void pdcadd_EndPrint(object sender, PrintEventArgs e)
    {
    try
    {
    repeatFont.Dispose();
    headerFont.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }

    }// Фонт хаасан урсгал хаасан
    public void pdcadd_BeginPrint(object sender, PrintEventArgs e)
    {
    repeatFont = new Font("Courier New", 10, FontStyle.Bold);
    headerFont = new Font("Courier New", 11, FontStyle.Bold);
    }
    public void pdcadd_PrintPage(object sender, PrintPageEventArgs e)
    {
    Graphics g = e.Graphics;
    height = Convert.ToInt32(headerFont.GetHeight(g));
    HeaderCashierAdd(g);
    }
    #region[Template]
    private void HeaderCashierAdd(Graphics g)
    {
    try
    {
    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
        _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
        _servertime = Static.ToDateTime(res.Param.GetValue(0));
    }
    else
    {
        MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
        return;
    }
    y = 10;
    strFrmt.Trimming = StringTrimming.EllipsisCharacter;
    strFrmt.FormatFlags = StringFormatFlags.NoWrap;
    g.DrawString("Скай-Резорт Билл", headerFont, Brushes.Black, x = x + 60, y);
    g.DrawString("[Cashier Add]", headerFont, Brushes.Black, x, y = y + 20);
    g.DrawString(_servertime.ToString("yyyy.MM.dd hh:mm:ss"), repeatFont, Brushes.Black, x = x - 60, y = y + 20);
    g.DrawString(string.Format("Pos:{0}", _core.POSNo), repeatFont, Brushes.Black, x = x + 180, y);
    g.DrawString(string.Format("Res:{0}", _core.RemoteObject.User.UserNo), repeatFont, Brushes.Black, x = x + 60, y);// Биллийн дугаар хэвлэх

    g.DrawString("[ShiftNo:]", repeatFont, Brushes.Black, x - 240, y = y + 20);
    g.DrawString(_shiftno.ToString(), repeatFont, Brushes.Black, x - 140, y);

    float[] ts = { 100, 50, 60, 50 };
    StringFormat strFormat = new StringFormat();
    strFormat.SetTabStops(0, ts);
    y = y + 20;
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x = x - 240, y, strFormat);
    SaleTotalAdd(g);
    pdcadd.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void SaleTotalAdd(Graphics g)
    {
    string Banknote = "";
    string QTY = "";
    Result res = new Result();
    y = y + 20;
    g.DrawString("[Added Total]", repeatFont, Brushes.Black, 0, y = y + 20);
    g.DrawString(StartAmt.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("[Banknotes]", repeatFont, Brushes.Black, 0, y = y + 20);
    foreach (DataRow DR in DTAdd.Rows)
    {
    y = y + 20;
        Banknote = DR["BANKNOTE"].ToString();
        QTY = DR["QTY"].ToString();
        g.DrawString(Banknote, repeatFont, Brushes.Black, 10, y);
        g.DrawString(QTY, repeatFont, Brushes.Black, 180, y);
    }
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("Cashier:", repeatFont, Brushes.Black, 10, y = y + 20);
    g.DrawString(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)), repeatFont, Brushes.Black, 100, y);
    }      
    #endregion[]        
    #endregion[]
    #region[Cashier Add Transaction Serial]
    public void PintCashierAddSerial(decimal _startAmt, DataTable DT)
    {
        if (DT.Rows.Count == 0)
        {
            return;
        }
        else
        {
            StartAmt = _startAmt;
            DTAdd = DT;
            Printer _serial = new Printer();
            _serial.Init(_core.BillPrinterPort, 9600, 8, StopBits.One, Parity.None);
            _serial.Open();
            HeaderCashierAddSerial(_serial);
        }
    }
    #region[Template]
    private void HeaderCashierAddSerial(Printer _serial)
    {
        try
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                _servertime = Static.ToDateTime(res.Param.GetValue(0));
            }
            else
            {
                MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
                return;
            }
            y = 10;
            strFrmt.Trimming = StringTrimming.EllipsisCharacter;
            strFrmt.FormatFlags = StringFormatFlags.NoWrap;
            _serial.Print("Скай-Резорт Билл");
            _serial.Print("[Cashier Add]");
            _serial.Print(_servertime.ToString("yyyy.MM.dd hh:mm:ss"));
            _serial.Print(string.Format("Pos:{0}", _core.POSNo));
            _serial.Print(string.Format("Res:{0}", _core.RemoteObject.User.UserNo));
            _serial.Print("[ShiftNo:]");
            _serial.Print(_shiftno.ToString());

            float[] ts = { 100, 50, 60, 50 };
            StringFormat strFormat = new StringFormat();
            strFormat.SetTabStops(0, ts);
            y = y + 20;
            _serial.Print("---------------------------------");
            SaleTotalAddSerial(_serial);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void SaleTotalAddSerial(Printer _serial)
    {
        string Banknote = "";
        string QTY = "";
        Result res = new Result();
        y = y + 20;
        _serial.Print("[Added Total]");
        _serial.Print(StartAmt.ToString("#,##0"));
        _serial.Print("---------------------------------");
        _serial.Print("[Banknotes]");
        foreach (DataRow DR in DTAdd.Rows)
        {
            y = y + 20;
            Banknote = DR["BANKNOTE"].ToString();
            QTY = DR["QTY"].ToString();
            _serial.Print(Banknote);
            _serial.Print(QTY);
        }
        _serial.Print("---------------------------------");
        _serial.Print("Cashier:");
        _serial.Print(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)));
    }
    #endregion[]
    #endregion[]
    #region[Cashier Delivery]
    public void PintCashierDelivery(decimal _startAmt, DataTable DT)
    {
        try
        {
            if (DT.Rows.Count == 0)
            {
                return;
            }
            else
            {
                StartAmt = _startAmt;
                DTAdd = DT;
                pdDelivery.PrinterSettings.PrinterName = _core.CacheGetStr("BillPrinterName");
                pdDelivery.PrintPage += new PrintPageEventHandler(pdDelivery_PrintPage);
                pdDelivery.BeginPrint += new PrintEventHandler(pdDelivery_BeginPrint);
                pdDelivery.EndPrint += new PrintEventHandler(pdDelivery_EndPrint);
                pdDelivery.Print();
                pdDelivery.Dispose();
            }
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    void pdDelivery_EndPrint(object sender, PrintEventArgs e)
    {
    try
    {
    repeatFont.Dispose();
    headerFont.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    void pdDelivery_BeginPrint(object sender, PrintEventArgs e)
    {
    repeatFont = new Font("Courier New", 10, FontStyle.Bold);
    headerFont = new Font("Courier New", 11, FontStyle.Bold);
    }
    void pdDelivery_PrintPage(object sender, PrintPageEventArgs e)
    {
    Graphics g = e.Graphics;
    height = Convert.ToInt32(headerFont.GetHeight(g));
    HeaderDelivery(g);
    }              
    #region[Template]
    private void HeaderDelivery(Graphics g)
    {
    try
    {
    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
        _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
        _servertime = Static.ToDateTime(res.Param.GetValue(0));
    }
    else
    {
        MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
        return;
    }
    y = 10;
    strFrmt.Trimming = StringTrimming.EllipsisCharacter;
    strFrmt.FormatFlags = StringFormatFlags.NoWrap;
    g.DrawString("Скай-Резорт Билл", headerFont, Brushes.Black, x = x + 60, y);
    g.DrawString("[Cashier Delivery]", headerFont, Brushes.Black, x, y = y + 20);

    g.DrawString(_servertime.ToString("yyyy.MM.dd hh:mm:ss"), repeatFont, Brushes.Black, x = x - 60, y = y + 20);
    g.DrawString(string.Format("Pos:{0}", _core.POSNo), repeatFont, Brushes.Black, x = x + 180, y);
    g.DrawString(string.Format("Res:{0}", _core.RemoteObject.User.UserNo), repeatFont, Brushes.Black, x = x + 60, y);// Биллийн дугаар хэвлэх

    g.DrawString("[ShiftNo:]", repeatFont, Brushes.Black, x - 240, y = y + 20);
    g.DrawString(_shiftno.ToString(), repeatFont, Brushes.Black, x - 140, y);

    float[] ts = { 100, 50, 60, 50 };
    StringFormat strFormat = new StringFormat();
    strFormat.SetTabStops(0, ts);
    y = y + 20;
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x = x - 240, y, strFormat);
    SaleTotalDelivery(g);
    pdcadd.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }
    }        
    private void SaleTotalDelivery(Graphics g)
    {
    string Banknote = "";
    string QTY = "";
    Result res = new Result();
    y = y + 20;
    g.DrawString("[Deliveried Total]", repeatFont, Brushes.Black, 0, y = y + 20);
    g.DrawString(StartAmt.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("[Banknotes]", repeatFont, Brushes.Black, 0, y = y + 20);
    foreach (DataRow DR in DTAdd.Rows)
    {
    y = y + 20;
    Banknote = DR["BANKNOTE"].ToString();
    QTY = DR["QTY"].ToString();
    g.DrawString(Banknote, repeatFont, Brushes.Black, 10, y);
    g.DrawString(QTY, repeatFont, Brushes.Black, 180, y);
    }
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("Cashier:", repeatFont, Brushes.Black, 10, y = y + 20);
    g.DrawString(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)), repeatFont, Brushes.Black, 100, y);
    }
    #endregion[]
    #endregion[]
    #region[Cashier Delivery Serial]
    public void PintCashierDeliverySerial(decimal _startAmt, DataTable DT)
    {
        if (DT.Rows.Count == 0)
        {
            return;
        }
        else
        {
            StartAmt = _startAmt;
            DTAdd = DT;
            Printer _serial = new Printer();
            _serial.Init(_core.BillPrinterPort, 9600, 8, StopBits.One, Parity.None);
            _serial.Open();
            HeaderDelivery(_serial);
        }
    }          
    #region[Template]
    private void HeaderDelivery(Printer _serial)
    {
        try
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                _servertime = Static.ToDateTime(res.Param.GetValue(0));
            }
            else
            {
                MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
                return;
            }
            y = 10;
            strFrmt.Trimming = StringTrimming.EllipsisCharacter;
            strFrmt.FormatFlags = StringFormatFlags.NoWrap;           
            _serial.Print("Скай-Резорт Билл");            
            _serial.Print("[Cashier Delivery]");

            _serial.Print(_servertime.ToString("yyyy.MM.dd hh:mm:ss"));
            _serial.Print(string.Format("Pos:{0}", _core.POSNo));
            _serial.Print(string.Format("Res:{0}", _core.RemoteObject.User.UserNo));
            _serial.Print("[ShiftNo:]");
            _serial.Print(_shiftno.ToString());

            float[] ts = { 100, 50, 60, 50 };
            StringFormat strFormat = new StringFormat();
            strFormat.SetTabStops(0, ts);
            _serial.Print("---------------------------------");
            SaleTotalDelivery(_serial);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void SaleTotalDelivery(Printer _serial)
    {
        string Banknote = "";
        string QTY = "";
        Result res = new Result();
        _serial.Print("[Deliveried Total]");
        _serial.Print(StartAmt.ToString("#,##0"));
        _serial.Print("---------------------------------");
        _serial.Print("[Banknotes]");
        foreach (DataRow DR in DTAdd.Rows)
        {
            y = y + 20;
            Banknote = DR["BANKNOTE"].ToString();
            QTY = DR["QTY"].ToString();
            _serial.Print(Banknote);
            _serial.Print(QTY);
        }
        _serial.Print("---------------------------------");
        _serial.Print("Cashier:");
        _serial.Print(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)));
    }
    #endregion[]
    #endregion[]
    #region[Pos Closure]
    public void PintPosClosure(decimal _startAmt)
    {
    //StartAmt = _startAmt;
    pdPosClose.PrinterSettings.PrinterName = _core.CacheGetStr("BillPrinterName");
    pdPosClose.PrintPage += new PrintPageEventHandler(pdPosClose_PrintPage);
    pdPosClose.BeginPrint += new PrintEventHandler(pdPosClose_BeginPrint);
    pdPosClose.EndPrint += new PrintEventHandler(pdPosClose_EndPrint);
    pdPosClose.Print();
    pdPosClose.Dispose();
    }
    void pdPosClose_EndPrint(object sender, PrintEventArgs e)
    {            
    try
    {
    repeatFont.Dispose();
    headerFont.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }

    }// Фонт хаасан урсгал хаасан
    void pdPosClose_BeginPrint(object sender, PrintEventArgs e)
    {
    repeatFont = new Font("Courier New", 10, FontStyle.Bold);
    headerFont = new Font("Courier New", 11, FontStyle.Bold);
    //titleFont = new Font("Courier", 8, FontStyle.Bold);
    }
    void pdPosClose_PrintPage(object sender, PrintPageEventArgs e)
    {
    Graphics g = e.Graphics;
    height = Convert.ToInt32(headerFont.GetHeight(g));
    HeaderPos(g);
    }
    #region[Template]
    private void HeaderPos(Graphics g)
    {
    try
    {
    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {
        _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
        _servertime = Static.ToDateTime(res.Param.GetValue(0));
    }
    else
    {
        MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
        return;
    }
    y = 10;
    strFrmt.Trimming = StringTrimming.EllipsisCharacter;
    strFrmt.FormatFlags = StringFormatFlags.NoWrap;
    g.DrawString("Скай-Резорт Билл", headerFont, Brushes.Black, x = x + 60, y);

    g.DrawString("[Pos Closure]", headerFont, Brushes.Black, x, y = y + 20);

    g.DrawString(_servertime.ToString("yyyy.MM.dd hh:mm:ss"), repeatFont, Brushes.Black, x = x - 60, y = y + 20);
    g.DrawString(string.Format("Pos:{0}", _core.POSNo), repeatFont, Brushes.Black, x = x + 180, y);
    g.DrawString(string.Format("Res:{0}", _core.RemoteObject.User.UserNo), repeatFont, Brushes.Black, x = x + 60, y);// Биллийн дугаар хэвлэх
    g.DrawString("[ShiftNo:]", repeatFont, Brushes.Black, x - 240, y = y + 20);
    g.DrawString(_shiftno.ToString(), repeatFont, Brushes.Black, x - 140, y);

    float[] ts = { 100, 50, 60, 50 };
    StringFormat strFormat = new StringFormat();
    strFormat.SetTabStops(0, ts);
    y = y + 20;
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x = x - 240, y, strFormat);
    DepositTotalPos(g);
    pdc.Dispose();
    }
    catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void DepositTotalPos(Graphics g)
    {
    Result res = new Result();
    y = y + 20;
    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510016, 510016, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo, _shiftno });
    if (ISM.Template.FormUtility.ValidateQuery(res))
    {

        g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y);
        g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y);
        g.DrawString("[Deposit Total]", repeatFont, Brushes.Black, 0, y = y + 20);
    foreach (DataRow dr in res.Data.Tables[0].Rows)
    {
        Total = Static.ToDecimal(dr["SUM(C.AMOUNT)"]);
        name = Static.ToStr(dr["NAME"]);
        CashTotalAmt = CashTotalAmt + Total;
        //if (_core.CashPayment == Static.ToStr(dr["PAYMENTTYPE"]))
        //{
        //    CashSaleTotal = CashSaleTotal + Static.ToDecimal(dr["SUM(C.AMOUNT)"]);
        //}
        g.DrawString(name, repeatFont, Brushes.Black, 30, y = y + 20);
        g.DrawString(Total.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    }
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("Total:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString(CashTotalAmt.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);

    }
    else
    {
    MessageBox.Show(res.ResultDesc);
    }

    PosTotal(g);
    }
    private void PosTotal(Graphics g)
    {
    Result res = new Result();
    y = y + 20;
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y);
    g.DrawString("[Cash Total]", repeatFont, Brushes.Black, 0, y = y + 20);
    g.DrawString("Start Amt:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString(StartAmt.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("Cash Sale:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString(CashTotalAmt.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("Deposit Amt:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString((CashSaleTotal + StartAmt).ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("Cash Total:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString((CashSaleTotal + StartAmt).ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("Over /Lake:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString(CashSale.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    SaleTotalPos(g);
    }
    private void SaleTotalPos(Graphics g)
    {
    Result res = new Result();
    y = y + 20;
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y);
    g.DrawString("[Sale Total]", repeatFont, Brushes.Black, 0, y = y + 20);
    g.DrawString("Sale:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString(CashSale.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("Return:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("Total:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString(CashTotalAmt.ToString("#,##0"), repeatFont, Brushes.Black, 180, y);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("Cashier:", repeatFont, Brushes.Black, 10, y = y + 20);
    g.DrawString(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)), repeatFont, Brushes.Black, 110, y);
    ServiceNames(g);
    }
    private void ServiceNames(Graphics g)
    {
    Result res = new Result();
    y = y + 40;
    float[] ts = { 80, 40, 90};
    StringFormat strFormat = new StringFormat();
    strFormat.SetTabStops(0, ts);
    g.DrawString("Service name\tQty\tAmount", repeatFont, Brushes.Black, x, y, strFormat);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("Sale:", repeatFont, Brushes.Black, 10, y = y + 20);
    g.DrawString(CashSale.ToString("#,##0"), repeatFont, Brushes.Black, 210, y = y + 20);
    g.DrawString("Return:", repeatFont, Brushes.Black, 30, y);
    g.DrawString("Total:", repeatFont, Brushes.Black, 30, y = y + 20);
    g.DrawString(CashTotalAmt.ToString("#,##0"), repeatFont, Brushes.Black, 210, y);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y = y + 20);
    g.DrawString("---------------------------------", repeatFont, Brushes.Black, x, y + 20);
    }
    #endregion[]               
    #endregion[]
    #region[Pos Closure Serial]
    public void PintPosClosureSerial(decimal _startAmt)
    {       
        Printer _serial = new Printer();
        _serial.Init(_core.BillPrinterPort, 9600, 8, StopBits.One, Parity.None);
        _serial.Open();
        HeaderPosSerial(_serial);
    }
    #region[Template]
    private void HeaderPosSerial(Printer _serial)
    {
        try
        {
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510015, 510015, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                _shiftno = Static.ToInt(res.Data.Tables[0].Rows[0]["SHIFTNO"]);
                _servertime = Static.ToDateTime(res.Param.GetValue(0));
            }
            else
            {
                MessageBox.Show(res.ResultDesc + "Билл хэвлэхэд алдаа гарлаа");
                return;
            }
            y = 10;
            strFrmt.Trimming = StringTrimming.EllipsisCharacter;
            strFrmt.FormatFlags = StringFormatFlags.NoWrap;
            _serial.Print("Скай-Резорт Билл");
            _serial.Print("[Pos Closure]");
            _serial.Print(_servertime.ToString("yyyy.MM.dd hh:mm:ss"));
            _serial.Print(string.Format("Pos:{0}", _core.POSNo));
            _serial.Print(string.Format("Res:{0}", _core.RemoteObject.User.UserNo));
            _serial.Print("[ShiftNo:]");
            _serial.Print(_shiftno.ToString());

            float[] ts = { 100, 50, 60, 50 };
            StringFormat strFormat = new StringFormat();
            strFormat.SetTabStops(0, ts);
            y = y + 20;
            _serial.Print("---------------------------------");
            DepositTotalPosSerial(_serial);
            pdc.Dispose();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }
    private void DepositTotalPosSerial(Printer _serial)
    {
        Result res = new Result();
        y = y + 20;
        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 510016, 510016, new object[] { _core.POSNo, _core.RemoteObject.User.UserNo, _shiftno });
        if (ISM.Template.FormUtility.ValidateQuery(res))
        {

            _serial.Print("---------------------------------");
            _serial.Print("---------------------------------");
            _serial.Print("[Deposit Total]");
            foreach (DataRow dr in res.Data.Tables[0].Rows)
            {
                Total = Static.ToDecimal(dr["SUM(C.AMOUNT)"]);
                name = Static.ToStr(dr["NAME"]);
                CashTotalAmt = CashTotalAmt + Total;
                //if (_core.CashPayment == Static.ToStr(dr["PAYMENTTYPE"]))
                //{
                //    CashSaleTotal = CashSaleTotal + Static.ToDecimal(dr["SUM(C.AMOUNT)"]);
                //}
                _serial.Print(name);
                _serial.Print(Total.ToString("#,##0"));
            }
            _serial.Print("---------------------------------");
            _serial.Print("Total:");
            _serial.Print(CashTotalAmt.ToString("#,##0"));
            _serial.Print("---------------------------------");

        }
        else
        {
            MessageBox.Show(res.ResultDesc);
        }

        PosTotalSerial(_serial);
    }
    private void PosTotalSerial(Printer _serial)
    {
        Result res = new Result();
        y = y + 20;
        _serial.Print("---------------------------------");
        _serial.Print("[Cash Total]");
        _serial.Print("Start Amt:");
        _serial.Print(StartAmt.ToString("#,##0"));
        _serial.Print("Cash Sale:");
        _serial.Print(CashTotalAmt.ToString("#,##0"));
        _serial.Print("Deposit Amt:");
        _serial.Print((CashSaleTotal + StartAmt).ToString("#,##0"));
        _serial.Print("Cash Total:");
        _serial.Print((CashSaleTotal + StartAmt).ToString("#,##0"));
        _serial.Print("Over /Lake:");
        _serial.Print(CashSale.ToString("#,##0"));
        _serial.Print("---------------------------------");
        SaleTotalPosSerial(_serial);
    }
    private void SaleTotalPosSerial(Printer _serial)
    {
        Result res = new Result();
        y = y + 20;
        _serial.Print("---------------------------------");
        _serial.Print("[Sale Total]");
        _serial.Print("Sale:");
        _serial.Print(CashSale.ToString("#,##0"));
        _serial.Print("Return:");
        _serial.Print("---------------------------------");
        _serial.Print("Total:");
        _serial.Print(CashTotalAmt.ToString("#,##0"));
        _serial.Print("---------------------------------");
        _serial.Print("Cashier:");       
        _serial.Print(string.Format("{0}.{1}", _core.RemoteObject.User.UserLName, _core.RemoteObject.User.UserFName.Substring(0, 1)));
        ServiceNamesSerial(_serial);
    }
    private void ServiceNamesSerial(Printer _serial)
    {
        Result res = new Result();
        y = y + 40;
        float[] ts = { 80, 40, 90 };
        StringFormat strFormat = new StringFormat();
        strFormat.SetTabStops(0, ts);
        _serial.Print("Service name\tQty\tAmount");
        _serial.Print("---------------------------------");
        _serial.Print("Sale:");
        _serial.Print(CashSale.ToString("#,##0"));
        _serial.Print("Return:");
        _serial.Print("Total:");
        _serial.Print(CashTotalAmt.ToString("#,##0"));
        _serial.Print("---------------------------------");
        _serial.Print("---------------------------------");
    }
    #endregion[]

    private void panelControl1_Paint(object sender, PaintEventArgs e)
    {

    }
    #endregion[]
    }
}