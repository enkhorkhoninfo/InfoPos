using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using ISM.Touch;
using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmBillShow : Form
    {
        #region Internal Variables
        InfoPos.Core.Core _core = null;
        DataSet _billdata = null;
        DataTable _griddata = null;
        string _salesno = null;

        #endregion
        #region Control Events
        public frmBillShow(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            //_salesno = salesno;

            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            gridView1.RowCellClick += gridView1_RowCellClick;

            InitGrid();
        }

        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "SELECTED")
            {
                DataRow r = (DataRow)gridView1.GetDataRow(e.RowHandle);
                bool selected = Static.ToBool(r["SELECTED"]);
                r["SELECTED"] = !selected;
            }
        }

        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView1.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                int loop = 0;
                string text = "";
                string id = Static.ToStr(r["ID"]);

                if (id == "BILL")
                {
                    text = PrepareBill();
                }
                else if (id == "TAG")
                {
                    text = PrepareTag();
                }
                else
                {
                    text = PrepareTicket(id, out loop);
                }

                memoEdit1.EditValue = text;
            }
        }
        private void frmBillShow_Load(object sender, EventArgs e)
        {
            //PrepareBillContents(_salesno);
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintBillContents();

                //string ret = _core.Printer.Open();
                //if (!string.IsNullOrEmpty(ret))
                //{
                //    _core.AlertShow("Билл Хэвлэх", ret, 2);
                //    return;
                //}
                //_core.Printer.Print((string)memoEdit1.EditValue);
                //_core.Printer.Close();

                this.Close();
            }
            catch (Exception ex)
            {
                _core.AlertShow("Билл Хэвлэх", ex.Message);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region Business Functions

        public void InitGrid()
        {
            _griddata = new DataTable();
            _griddata.Columns.Add("ID", typeof(string));
            _griddata.Columns.Add("NAME", typeof(string));
            _griddata.Columns.Add("SELECTED", typeof(bool));

            gridControl1.DataSource = _griddata;

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Баримтын төрөл", false);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Хэвлэх", false);

            gridView1.RowHeight = 28;
            gridView1.Columns[2].Width = 32;
            gridView1.Columns[1].Width = gridControl1.Width - gridView1.Columns[2].Width - 8;

            //gridView1.Columns[1].GroupIndex = 0;
            //gridView1.GroupFormat = "{1}";
        }
        public void PrepareBillContents(string salesno)
        {
            DataTable dt = null;

            _salesno = salesno;

            object[] param = new object[] { salesno };
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605050, 605012, param);
            if (res.ResultNo != 0) goto OnExit;

            _billdata = res.Data;

            #region Grid - Add bill group

            _griddata.Rows.Clear();
            _griddata.Rows.Add("BILL", "БАРИМТ  : БИЛЛ ХЭВЛЭХ", true);

            #endregion
            #region Grid - Tag list

            dt = _billdata.Tables["Tags"];
            if (dt != null && dt.Rows.Count > 0)
            {
                _griddata.Rows.Add("TAG", "БАРИМТ  : ОЛГОСОН ТАГ ХЭВЛЭХ", true);
            }

            #endregion
            #region Grid - Add ticket group

            dt = _billdata.Tables["Tickets"];
            if (dt != null && dt.Rows.Count > 0)
            {
                int rowindex = 0;
                foreach (DataRow r in dt.Rows)
                {
                    string custno = Static.ToStr(r["CUSTNO"]);
                    string custname = Static.ToStr(r["CUSTNAME"]);
                    string prodno = Static.ToStr(r["PRODNO"]);
                    string prodname = Static.ToStr(r["PRODNAME"]);

                    decimal qty = Static.ToDecimal(r["QTY"]);
                    int servicetime = Static.ToInt(r["SERVICETIME"]);

                    string id = string.Format("{0}~{1}~{2}~{3}~{4}", rowindex++, custno, prodno, qty, servicetime);
                    string name = string.Format ("ТАСАЛБАР: {0}, {1}ш, {2}мин", prodname, qty, servicetime);
                    _griddata.Rows.Add(id, name, true);
                }
            }

            #endregion

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
            //_core.AlertShow("Билл Хэвлэх", );
        }
        public string PrepareBill()
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = null;
            DataRow r = null;
            object hash = null;

            decimal sa = 0;
            decimal dp = 0;
            decimal ds = 0;
            decimal ta = 0;
            decimal vat = 0;
            decimal paid = 0;
            try
            {
                #region Биллийн формат файлыг унших

                string filename = string.Format("{0}\\Bill\\BillFormat.txt", _core.ApplicationPath);
                if (System.IO.File.Exists(filename))
                {
                    string s = System.IO.File.ReadAllText(filename);

                    bool success = false;
                    hash = JSON.JsonDecode(s, ref success);
                    if (!success)
                    {
                        throw new Exception("Биллын формат файл буруу байна.");
                    }
                }

                #endregion
                #region Толгойн custom мэдээллийг унших
                object hdr = Core.Core.JPathGet((Hashtable)hash, "header");
                object ftr = Core.Core.JPathGet((Hashtable)hash, "footer");
                if (hdr == null || ftr == null || !(hdr is ArrayList) || !(ftr is ArrayList))
                {
                    throw new Exception("Биллын формат файл буруу байна.");
                }

                ArrayList list = (ArrayList)hdr;
                foreach (object o in list)
                {
                    sb.AppendLine(Static.ToStr(o));
                }

                #endregion

                #region Толгойн мэдээлэл

                dt = _billdata.Tables["Totals"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    r = dt.Rows[0];

                    string salesno = Static.ToStr(r["SALESNO"]);
                    string cashiername = Static.ToStr(r["USERNAME"]);
                    DateTime postdate = Static.ToDateTime(r["POSTDATE"]);
                    string posno = Static.ToStr(r["POSNO"]);
                    string posname = Static.ToStr(r["POSNAME"]);
                    int shiftno = Static.ToInt(r["SHIFTNO"]);

                    posname = " ".PadLeft((40 - posname.Length) / 2) + posname;
                    sb.AppendFormat("{0}\r\n", posname.ToUpper());
                    sb.AppendFormat("{0,-16:yyyy/MM/dd HH:mm}   {1,21}\r\n", DateTime.Now, salesno);
                    sb.AppendFormat("Cashier: {0,-17} POS: {1} ({2})\r\n", cashiername, posno, shiftno);

                    sa = Static.ToDecimal (r["SA"]);
                    dp = Static.ToDecimal (r["DP"]);
                    ds = Static.ToDecimal (r["DS"]);
                    vat = Static.ToDecimal (r["VAT"]);
                    paid = Static.ToDecimal (r["PAID"]);
                    ta = sa - dp - ds;
                }

                #endregion
                #region Биллийн барааны жагсаалт бэлдэх

                sb.AppendLine("========================================");
                sb.AppendLine(" Item                 Price  Qty  Amount");
                sb.AppendLine("----------------------------------------");

                dt = _billdata.Tables["Products"];
                if (dt != null && dt.Rows.Count >0 )
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendFormat("{0,-21}{1,6}*{2,4}={3,7:###0}\r\n"
                            , Static.SubStr(Static.ToStr(row["PRODNAME"]), 0, 21)
                            , row["PRICE"]
                            , row["QTY"]
                            , row["SALESAMOUNT"]
                            );
                    }
                }

                sb.AppendLine("========================================");
                sb.AppendFormat("Total    :  {0,28:#,##0}\r\n", sa);
                sb.AppendFormat("Discount :  {0,28:#,##0}\r\n", dp + ds);
                sb.AppendLine("----------------------------------------");
                sb.AppendFormat("NetPrice :  {0,28:#,##0}\r\n", ta);
                sb.AppendFormat("VAT      :  {0,28:#,##0}\r\n", vat);
                sb.AppendLine("----------------------------------------");

                #endregion
                #region Биллийн төлбөрийн жагсаалт бэлдэх

                dt = _billdata.Tables["Payments"];
                if (dt != null && dt.Rows.Count>0)
                {
                    //decimal paid = 0;
                    foreach(DataRow row in dt.Rows)
                    {
                        decimal amount = Static.ToDecimal(row["AMOUNT"]);
                        int pflag = Static.ToInt(row["PAYMENTFLAG" /*"PAYMENTFLAG"*/]);
                        string pname = Static.ToStr(row["PAYMENTNAME"]);
                        string preg = Static.ToStr(row["SOURCENO"]);
                        string ptype = Static.ToStr("FLAG");

                        //paid += amount;

                        if (pflag == 0) pname = "CASH";
                        else if (pflag == 1) pname = "IPPOS";

                        if (ptype == "E") preg = "CHANGE";
                        if (ptype == "R") preg = "REFUND";

                        sb.AppendFormat("{0,-12}{1,-12}{2,16:#,##0}\r\n"
                            , Static.SubStr(pname, 0, 10)
                            , Static.SubStr(preg, 0, 12)
                            , amount
                            );
                    }

                    if (paid < ta)
                    {
                        sb.AppendLine();
                        sb.AppendFormat("UNPAID AMOUNT  {0,25:#,##0}", ta - paid);
                        sb.AppendLine();
                    }

                    sb.AppendLine("========================================");
                }
                #endregion
                #region Биллийн сүүл бэлдэх
                list = (ArrayList)ftr;
                foreach (object o in list)
                {
                    sb.AppendLine(Static.ToStr(o));
                }
                #endregion
            }
            catch (Exception ex)
            {
                _core.AlertShow("Билл Хэвлэх", ex.Message);
            }
            //System.IO.File.WriteAllText("c:\\bill1.txt", sb.ToString());
            return sb.ToString();
        }
        public string PrepareTag()
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = null;
            DataRow r = null;
            object hash = null;

            try
            {
                #region Биллийн формат файлыг унших

                string filename = string.Format("{0}\\Bill\\BillFormat.txt", _core.ApplicationPath);
                if (System.IO.File.Exists(filename))
                {
                    string s = System.IO.File.ReadAllText(filename);

                    bool success = false;
                    hash = JSON.JsonDecode(s, ref success);
                    if (!success)
                    {
                        throw new Exception("Биллын формат файл буруу байна.");
                    }
                }

                #endregion
                #region Толгойн custom мэдээллийг унших
                object hdr = Core.Core.JPathGet((Hashtable)hash, "header");
                object ftr = Core.Core.JPathGet((Hashtable)hash, "footer");
                if (hdr == null || ftr == null || !(hdr is ArrayList) || !(ftr is ArrayList))
                {
                    throw new Exception("Биллын формат файл буруу байна.");
                }

                ArrayList list = (ArrayList)hdr;
                foreach (object o in list)
                {
                    sb.AppendLine(Static.ToStr(o));
                }

                #endregion
                #region Биллийн толгой бэлдэх

                dt = _billdata.Tables["Totals"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    r = dt.Rows[0];

                    string salesno = Static.ToStr(r["SALESNO"]);
                    string cashiername = Static.ToStr(r["USERNAME"]);
                    DateTime postdate = Static.ToDateTime(r["POSTDATE"]);
                    string posno = Static.ToStr(r["POSNO"]);
                    string posname = Static.ToStr(r["POSNAME"]);
                    int shiftno = Static.ToInt(r["SHIFTNO"]);

                    sb.Append("                TAG INFO\r\n");
                    sb.AppendFormat("{0,-16:yyyy/MM/dd HH:mm}   {1,21}\r\n", DateTime.Now, salesno);
                    //sb.AppendFormat("Cashier: {0,-17} POS: {1} ({2})\r\n", cashiername, posno, shiftno);

                }


                #endregion
                #region Биллийн барааны жагсаалт бэлдэх

                sb.AppendLine("========================================");
                sb.AppendLine(" Customer                Serial   Series");
                sb.AppendLine("----------------------------------------");

                dt = _billdata.Tables["Tags"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string custname = Static.ToStr(row["CUSTNAME"]);
                        string tagno = Static.ToStr(row["SERIALNO"]);
                        string tagframeno = Static.ToStr(row["TAGFRAMENO"]);

                        sb.AppendFormat("{0,-25}{1,8}{2,7}\r\n"
                            , custname
                            , tagno
                            , tagframeno
                            );
                    }
                }

                sb.AppendLine("========================================");

                #endregion
                #region Биллийн сүүл бэлдэх
                list = (ArrayList)ftr;
                foreach (object o in list)
                {
                    sb.AppendLine(Static.ToStr(o));
                }
                #endregion
            }
            catch (Exception ex)
            {
                _core.AlertShow("Билл Хэвлэх", ex.Message);
            }
            //System.IO.File.WriteAllText("c:\\bill2.txt", sb.ToString());
            return sb.ToString();
        }
        public string PrepareTicket(string rowid, out int printcount)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = null;
            DataRow r = null;
            object hash = null;

            DateTime postdate = DateTime.MinValue;
            int count = 0;

            try
            {
                #region Биллийн формат файлыг унших

                string filename = string.Format("{0}\\Bill\\BillFormat.txt", _core.ApplicationPath);
                if (System.IO.File.Exists(filename))
                {
                    string s = System.IO.File.ReadAllText(filename);

                    bool success = false;
                    hash = JSON.JsonDecode(s, ref success);
                    if (!success)
                    {
                        throw new Exception("Биллын формат файл буруу байна.");
                    }
                }

                #endregion
                #region Толгойн custom мэдээллийг унших
                object hdr = Core.Core.JPathGet((Hashtable)hash, "header");
                object ftr = Core.Core.JPathGet((Hashtable)hash, "footer");
                if (hdr == null || ftr == null || !(hdr is ArrayList) || !(ftr is ArrayList))
                {
                    throw new Exception("Биллын формат файл буруу байна.");
                }

                ArrayList list = (ArrayList)hdr;
                foreach (object o in list)
                {
                    sb.AppendLine(Static.ToStr(o));
                }

                #endregion
                #region Биллийн толгой бэлдэх

                dt = _billdata.Tables["Totals"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    r = dt.Rows[0];

                    string salesno = Static.ToStr(r["SALESNO"]);
                    string cashiername = Static.ToStr(r["USERNAME"]);
                    string posno = Static.ToStr(r["POSNO"]);
                    string posname = Static.ToStr(r["POSNAME"]);
                    int shiftno = Static.ToInt(r["SHIFTNO"]);

                    postdate = Static.ToDateTime(r["POSTDATE"]);

                    sb.Append("                 TICKET\r\n");
                    sb.AppendFormat("{0,-16:yyyy/MM/dd HH:mm}   {1,21}\r\n", DateTime.Now, salesno);
                    //sb.AppendFormat("Cashier: {0,-17} POS: {1} ({2})\r\n", cashiername, posno, shiftno);

                }


                #endregion
                #region Биллийн барааны жагсаалт бэлдэх

                sb.AppendLine("========================================");
                sb.AppendLine("");

                dt = _billdata.Tables["Tickets"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string[] a = rowid.Split(new string[] { "~" }, StringSplitOptions.RemoveEmptyEntries);
                    int rowindex = Static.ToInt(a[0]);
                    if (dt.Rows.Count > rowindex)
                    {
                        r = dt.Rows[rowindex];

                        string prodname = Static.ToStr(r["PRODNAME"]);
                        int qty = Static.ToInt(r["QTY"]);
                        int servicetime = Static.ToInt(r["SERVICETIME"]);
                        DateTime endtime = postdate.AddMinutes(servicetime);

                        count = qty;

                        sb.AppendFormat("Service    : {0}\r\n", Static.SubStr(prodname, 0, 26));
                        sb.AppendFormat("Duration   : {0} minutes\r\n", servicetime);
                        sb.AppendLine();
                        sb.AppendFormat("Start time : {0}\r\n", postdate.ToString("yyyy/MM/dd HH:mm"));
                        sb.AppendFormat("End time   : {0}\r\n", endtime.ToString("yyyy/MM/dd HH:mm"));
                        sb.AppendLine();
                    }
                }
                
                sb.AppendLine("========================================");

                #endregion
                #region Биллийн сүүл бэлдэх
                list = (ArrayList)ftr;
                foreach (object o in list)
                {
                    sb.AppendLine(Static.ToStr(o));
                }
                #endregion
            }
            catch (Exception ex)
            {
                _core.AlertShow("Билл Хэвлэх", ex.Message);
            }
            //System.IO.File.WriteAllText("c:\\bill3.txt", sb.ToString());

            printcount = count;
            return sb.ToString();
        }

        public void PrintBillContents()
        {
            string result = null;
            try
            {
                foreach (DataRow r in _griddata.Rows)
                {
                    bool selected = Static.ToBool(r["SELECTED"]);
                    if (selected)
                    {
                        int loop = 1;
                        string text = "";
                        string id = Static.ToStr(r["ID"]);

                        if (id == "BILL")
                        {
                            text = PrepareBill();

                            result = _core.Printer.Open();
                            if (!string.IsNullOrEmpty(result)) goto OnExit;

                            _core.Printer.Print(text);
                            _core.Printer.Print("\r\n\r\n\r\n\r\n\r\n\x1Bi");
                            _core.Printer.Close();
                        }
                        else if (id == "TAG")
                        {
                            text = PrepareTag();

                            result = _core.Printer.Open();
                            if (!string.IsNullOrEmpty(result)) goto OnExit;

                            _core.Printer.Print(text);
                            _core.Printer.Print("\r\n\r\n\r\n\r\n\r\n\x1Bi");
                            _core.Printer.Close();
                        }
                        else
                        {
                            text = PrepareTicket(id, out loop);

                            result = _core.LiftPrinter.Open();
                            if (!string.IsNullOrEmpty(result)) goto OnExit;

                            for (int i = 0; i < loop; i++)
                            {
                                _core.LiftPrinter.Print(text);
                                _core.Printer.Print("\r\n\r\n\r\n\r\n\r\n\x1Bi");
                            }

                            _core.LiftPrinter.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
        OnExit:
            if (!string.IsNullOrEmpty(result))
            {
                _core.AlertShow("Билл хэвлэх", result, 2);
            }
        }

        #endregion

        #region Old Functions

        //decimal _totalamount = 0;
        //decimal _salesamount = 0;
        //decimal _discountprod = 0;
        //decimal _discountsales = 0;
        //decimal _vat = 0;

        //string _billtext = null;
        //string _lifttext = null;

        //public void RefreshBoardValues(DataTable cart, bool isvat)
        //{
        //    try
        //    {
        //        DataTable _cart = cart;

        //        #region Нийт дүнгүүдийг тооцоолох

        //        decimal sa = 0;
        //        decimal dp = 0;
        //        decimal ds = 0;
        //        decimal up = 0;

        //        if (_cart.Rows.Count > 0)
        //        {
        //            var qtotal = from r in _cart.AsEnumerable()
        //                         where string.IsNullOrEmpty(Static.ToStr(r["CONTRACTNO"]))
        //                         group r by 1
        //                             into agg
        //                             select new
        //                             {
        //                                 up = agg.Sum(rr => Static.ToDecimal(rr["QTY"]) * Static.ToDecimal(rr["UNITPRICE"]))
        //                                 ,
        //                                 sa = agg.Sum(rr => Static.ToDecimal(rr["SALESAMOUNT"]))
        //                                 ,
        //                                 tdp = agg.Sum(rr => Static.ToDecimal(rr["DISCOUNT"]))
        //                                 ,
        //                                 dp = agg.Sum(rr => Static.ToDecimal(rr["DISCOUNTPROD"]))
        //                                 ,
        //                                 ds = agg.Sum(rr => Static.ToDecimal(rr["DISCOUNTSALES"]))
        //                             };
        //            if (qtotal != null && qtotal.Count() > 0)
        //            {
        //                var v = qtotal.First();
        //                sa = v.sa;
        //                ds = v.ds;
        //                up = v.up;

        //                decimal dd = v.up - v.sa;
        //                if (dd < 0) dd = 0;
        //                dp = v.dp + dd;

        //                if (dp > sa) dp = sa;
        //            }
        //        }
        //        decimal ta = (sa - dp - ds);
        //        //decimal paid = Static.ToDecimal(numPaid.EditValue);  // paid amount

        //        _salesamount = sa;    // НӨАТ орсон дүнгээрээ
        //        _discountprod = dp; // НӨАТ орсон дүнгээрээ
        //        _discountsales = ds;

        //        if (isvat)
        //        {
        //            _totalamount = ta;
        //            _vat = ta * _core.Vat / 100;
        //        }
        //        else
        //        {
        //            _totalamount = ta * (1 - _core.Vat / 100);
        //            _vat = -ta * _core.Vat / 100;
        //        }

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        _core.AlertShow("Нийт дүн бодох", ex.ToString());
        //    }
        //}
        //public void BillShow(string salesno, bool isvat, string posno, int shiftno, string cashiername, DataTable cart, object[] payments)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region Биллийн формат файлыг унших

        //        string filename = string.Format("{0}\\Bill\\BillFormat.txt", _core.ApplicationPath);
        //        string s = System.IO.File.ReadAllText(filename);

        //        bool success = false;
        //        object hash = JSON.JsonDecode(s, ref success);
        //        if (!success)
        //        {
        //            throw new Exception("Биллын формат файл буруу байна.");
        //        }
        //        object hdr = Core.Core.JPathGet((Hashtable)hash, "header");
        //        object ftr = Core.Core.JPathGet((Hashtable)hash, "footer");
        //        if (hdr == null || ftr == null || !(hdr is ArrayList) || !(ftr is ArrayList))
        //        {
        //            throw new Exception("Биллын формат файл буруу байна.");
        //        }

        //        #endregion
        //        #region Нийт дүнгүүдийг бодох

        //        RefreshBoardValues(cart, isvat);

        //        #endregion
        //        #region Биллийн толгой бэлдэх

        //        ArrayList list = (ArrayList)hdr;
        //        foreach (object o in list)
        //        {
        //            sb.AppendLine(Static.ToStr(o));
        //        }

        //        sb.AppendFormat("{0,-16:yyyy/MM/dd HH:mm}   {1,21}\r\n", DateTime.Now, salesno);
        //        sb.AppendFormat("Cashier: {0,-17} POS: {1} ({2})\r\n", cashiername, posno, shiftno);

        //        #endregion
        //        #region Биллийн барааны жагсаалт бэлдэх

        //        sb.AppendLine("========================================");
        //        sb.AppendLine(" Item                 Price  Qty  Amount");
        //        sb.AppendLine("----------------------------------------");

        //        if (cart != null)
        //        {
        //            foreach (DataRow r in cart.Rows)
        //            {
        //                sb.AppendFormat("{0,-21}{1,6}*{2,4}={3,7:###0}\r\n"
        //                    , Static.SubStr(Static.ToStr(r["PRODNAME"]), 0, 21)
        //                    , r["PRICE"]
        //                    , r["QTY"]
        //                    , r["SALESAMOUNT"]
        //                    );
        //            }
        //        }

        //        sb.AppendLine("========================================");
        //        sb.AppendFormat("Total    :  {0,28:#,##0}\r\n", _salesamount);
        //        sb.AppendFormat("Discount :  {0,28:#,##0}\r\n", _discountprod + _discountsales);
        //        sb.AppendLine("----------------------------------------");
        //        sb.AppendFormat("NetPrice :  {0,28:#,##0}\r\n", _totalamount);
        //        sb.AppendFormat("VAT      :  {0,28:#,##0}\r\n", _vat);
        //        sb.AppendLine("----------------------------------------");

        //        #endregion
        //        #region Биллийн төлбөрийн жагсаалт бэлдэх

        //        if (payments != null)
        //        {
        //            decimal paid = 0;
        //            foreach (object[] r in payments)
        //            {
        //                decimal amount = Static.ToDecimal(r[4]);
        //                int pflag = Static.ToInt(r[3 /*"PAYMENTFLAG"*/]);
        //                string pname = Static.ToStr(r[10]);
        //                string preg = Static.ToStr(r[6]);
        //                string ptype = Static.ToStr(r[5]);

        //                paid += amount;

        //                if (pflag == 0) pname = "CASH";
        //                else if (pflag == 0) pname = "CARD";

        //                if (ptype == "E") preg = "CHANGE";

        //                sb.AppendFormat("{0,-12}{1,-12}{2,16:#,##0}\r\n"
        //                    , Static.SubStr(pname, 0, 10)
        //                    , Static.SubStr(preg, 0, 12)
        //                    , r[4]
        //                    );
        //            }

        //            if (paid < _totalamount)
        //            {
        //                sb.AppendLine();
        //                sb.AppendFormat("UNPAID AMOUNT  {0,25:#,##0}", _totalamount - paid);
        //                sb.AppendLine();
        //            }

        //            sb.AppendLine("========================================");
        //        }
        //        #endregion
        //        #region Биллийн сүүл бэлдэх
        //        list = (ArrayList)ftr;
        //        foreach (object o in list)
        //        {
        //            sb.AppendLine(Static.ToStr(o));
        //        }
        //        #endregion
        //        _billtext = sb.ToString();
        //        memoEdit1.EditValue = sb.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        _core.AlertShow("Билл Хэвлэх", ex.Message);
        //    }
        //}
        //public void BillReprintShow(string salesno, bool isvat, string posno, int shiftno, string cashiername, DataTable cart, DataTable payments)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        ArrayList paymentParam = new ArrayList();

        //        if (payments != null)
        //        {
        //            foreach (DataRow r in payments.Rows)
        //            {
        //                paymentParam.Add(new object[]{
        //                    salesno
        //                    , ""        /*paymentno*/
        //                    , r["PAYMENTTYPE"] /*ptype*/
        //                    , r["PAYMENTFLAG"]
        //                    , r["AMOUNT"]
        //                    , "N" /*flag=N orig txn*/
        //                    , r["SOURCENO"]
        //                    , _core.RemoteObject.User.AreaCode
        //                    , _core.POSNo 
        //                    , "" /*detail info*/
        //                    , r["PAYMENTNAME"] /*билл хэвлэхэд зориулав*/
        //                    });
        //            }
        //        }

        //        BillShow(salesno, isvat, posno, shiftno, cashiername, cart, paymentParam.ToArray());


        //        PrepareBillContents(salesno);
        //        PrepareBill();
        //        PrepareTag();
        //    }
        //    catch (Exception ex)
        //    {
        //        _core.AlertShow("Билл Хэвлэх", ex.Message);
        //    }
        //}
        //public void BillTagShow(string salesno, DataTable rents, DataTable custs)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {
        //        #region Биллийн формат файлыг унших

        //        string filename = string.Format("{0}\\Bill\\BillFormat.txt", _core.ApplicationPath);
        //        string s = System.IO.File.ReadAllText(filename);

        //        bool success = false;
        //        object hash = JSON.JsonDecode(s, ref success);
        //        if (!success)
        //        {
        //            throw new Exception("Биллын формат файл буруу байна.");
        //        }
        //        object hdr = Core.Core.JPathGet((Hashtable)hash, "header");
        //        object ftr = Core.Core.JPathGet((Hashtable)hash, "footer");
        //        if (hdr == null || ftr == null || !(hdr is ArrayList) || !(ftr is ArrayList))
        //        {
        //            throw new Exception("Биллын формат файл буруу байна.");
        //        }

        //        #endregion
        //        #region Биллийн толгой бэлдэх

        //        ArrayList list = (ArrayList)hdr;
        //        foreach (object o in list)
        //        {
        //            sb.AppendLine(Static.ToStr(o));
        //        }

        //        sb.AppendFormat("{0,-16:yyyy/MM/dd HH:mm}   {1,21}\r\n", DateTime.Now, salesno);
        //        //sb.AppendFormat("Cashier: {0,-17} POS: {1} ({2})\r\n", cashiername, posno, shiftno);

        //        #endregion
        //        #region Биллийн барааны жагсаалт бэлдэх

        //        sb.AppendLine("========================================");
        //        sb.AppendLine(" Customer                Serial   Series");
        //        sb.AppendLine("----------------------------------------");

        //        if (custs != null)
        //        {
        //            foreach (DataRow r in custs.Rows)
        //            {
        //                string custname = Static.ToStr(r["CUSTNAME"]);
        //                string tagno = Static.ToStr(r["SERIALNO"]);
        //                string tagframeno = Static.ToStr(r["TAGFRAMENO"]);

        //                sb.AppendFormat("{0,-25}{1,8}{2,7}\r\n"
        //                    , custname
        //                    , tagno
        //                    , tagframeno
        //                    );
        //            }
        //        }

        //        sb.AppendLine("========================================");

        //        #endregion
        //        #region Биллийн сүүл бэлдэх
        //        list = (ArrayList)ftr;
        //        foreach (object o in list)
        //        {
        //            sb.AppendLine(Static.ToStr(o));
        //        }
        //        #endregion
        //        _billtext = sb.ToString();
        //        memoEdit1.EditValue = sb.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        _core.AlertShow("Билл Хэвлэх", ex.Message);
        //    }
        //}

        #endregion
    }
}
