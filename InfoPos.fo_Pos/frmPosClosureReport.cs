using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ISM.Touch;
using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.Pos
{
    public partial class frmPosClosureReport : DevExpress.XtraEditors.XtraForm
    {
        #region Internal variables
        InfoPos.Core.Core _core = null;
        int status = 0; // 0-Shift bill, 1-Pos bill
        #endregion
        #region Control Events
        public frmPosClosureReport(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
        }
        private void frmPosClosureReport_Load(object sender, EventArgs e)
        {

        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool success = false;
            if (status == 0) success = ShiftClosure_PrintBill();
            else success = PosClosure_PrintBill();

            if (success) this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        #region Public functions

        public string ShiftClosure_PrepareBill(string posno, int shiftno)
        {
            Result res = null;
            StringBuilder sb = new StringBuilder();
            DataTable dt = null;
            DataRow r = null;
            object hash = null;
            try
            {
                object[] param = new object[] { posno, shiftno };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211009, 211001, param);
                if (res.ResultNo != 0) goto OnExit;

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

                dt = res.Data.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    r = dt.Rows[0];
                    #region Field values
                    string posname = Static.ToStr(r["POSNAME"]);
                    decimal cash_amount = Static.ToDecimal(r["CASH_AMOUNT"]);
                    decimal card_amount = Static.ToDecimal(r["CARD_AMOUNT"]);
                    decimal other_amount = Static.ToDecimal(r["OTHER_AMOUNT"]);

                    int cash_count = Static.ToInt(r["CASH_COUNT"]);
                    int card_count = Static.ToInt(r["CARD_COUNT"]);
                    int other_count = Static.ToInt(r["OTHER_COUNT"]);

                    decimal cashin_amount = Static.ToDecimal(r["CASHIN_AMOUNT"]);
                    decimal cashout_amount = -Static.ToDecimal(r["CASHOUT_AMOUNT"]);

                    int cashin_count = Static.ToInt(r["CASHIN_COUNT"]);
                    int cashout_count = Static.ToInt(r["CASHOUT_COUNT"]);

                    decimal total_amount = Static.ToDecimal(r["TOTAL_AMOUNT"]);
                    decimal refund_amount = Static.ToDecimal(r["REFUND_AMOUNT"]);

                    int sales_count = Static.ToInt(r["SALES_COUNT"]);
                    int refund_count = Static.ToInt(r["REFUND_COUNT"]);
                    #endregion

                    posname = " ".PadLeft((40 - posname.Length) / 2) + posname;
                    sb.AppendFormat("{0}\r\n", posname.ToUpper());

                    sb.Append("           [Cashier Closure]\r\n");
                    sb.AppendFormat("{0,-16:yyyy/MM/dd HH:mm}   Pos:{1,4} Shft:{2,3}\r\n", DateTime.Now, Static.SubStr(posno, 0, 4), shiftno);
                    sb.AppendLine("========================================");

                    sb.AppendLine("[Deposit Total]");
                    sb.AppendFormat("        Cash : {0,-4} {1,15:#,##0.00}\r\n", cash_count, cash_amount);
                    sb.AppendFormat("        Card : {0,-4} {1,15:#,##0.00}\r\n", card_count, card_amount);
                    sb.AppendFormat("       Other : {0,-4} {1,15:#,##0.00}\r\n", other_count, other_amount);
                    sb.AppendLine("----------------------------------------");
                    sb.AppendFormat("       Total : {0,-4} {1,15:#,##0.00}\r\n", cash_count + card_count + other_count, cash_amount + card_amount + other_amount);
                    sb.AppendLine("----------------------------------------");

                    sb.AppendLine("[Cash Total]");
                    sb.AppendFormat("   Start Amt : {0,-4} {1,15:#,##0.00}\r\n", cashin_count, cashin_amount);
                    sb.AppendFormat("   Cash Sale : {0,-4} {1,15:#,##0.00}\r\n", cash_count, cash_amount);
                    sb.AppendFormat(" Deposit Amt : {0,-4} {1,15:#,##0.00}\r\n", cashin_count + cash_count, cashin_amount + cash_amount);
                    sb.AppendFormat("  Cash Total : {0,-4} {1,15:#,##0.00}\r\n", cashout_count, cashout_amount);
                    sb.AppendLine("----------------------------------------");
                    sb.AppendFormat("   Over/Lack :      {0,15:#,##0.00}\r\n", cashin_amount + cash_amount + cashout_amount);
                    sb.AppendLine("----------------------------------------");

                    sb.AppendLine("[Sale Total]");
                    sb.AppendFormat("        Sale : {0,-4} {1,15:#,##0.00}\r\n", sales_count, total_amount);
                    sb.AppendFormat("      Return : {0,-4} {1,15:#,##0.00}\r\n", refund_count, refund_amount);
                    sb.AppendLine("----------------------------------------");
                    sb.AppendFormat("       Total : {0,-4} {1,15:#,##0.00}\r\n", sales_count + refund_count, total_amount + refund_amount);

                    sb.AppendLine("========================================");
                    sb.Append("\r\n\r\n");
                    sb.AppendFormat("Cashier:...............{0}\r\n"
                        , (string.IsNullOrEmpty(_core.RemoteObject.User.UserFName) ? "" : Static.SubStr(_core.RemoteObject.User.UserFName, 0, 1) + ".") + _core.RemoteObject.User.UserLName
                        );
                    sb.Append("\r\n\r\n");
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
                res = new Result(211009, ex.ToString());
            }
        OnExit:
            if (res != null && res.ResultNo != 0)
            {
                _core.AlertShow("Ээлж хаалт", res.ResultDesc);
            }
            memoEdit1.EditValue = sb.ToString();
            status = 0;
            return sb.ToString();
        }
        public bool ShiftClosure_PrintBill()
        {
            //string text = ShiftClosure_PrepareBill(posno, shiftno);
            string text = memoEdit1.Text;

            if (string.IsNullOrEmpty(text))
            {
                _core.AlertShow("Ээлж хаалт", "Борлуулалтын мэдээ хоосон байна.", 2);
                return false;
            }

            string result = _core.Printer.Open();
            if (!string.IsNullOrEmpty(result)) goto OnExit;

            _core.Printer.Print(text);
            _core.Printer.Print("\r\n\r\n\r\n\r\n\r\n\x1Bi");
            _core.Printer.Close();

        OnExit:
            if (!string.IsNullOrEmpty(result))
            {
                _core.AlertShow("Ээлж хаалт", result, 2);
            }
            return string.IsNullOrEmpty(result);
        }

        public string PosClosure_PrepareBill(string posno)
        {
            Result res = null;
            StringBuilder sb = new StringBuilder();
            DataTable dt = null;
            DataRow r = null;
            object hash = null;
            try
            {
                object[] param = new object[] { posno };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 211, 211010, 211001, param);
                if (res.ResultNo != 0) goto OnExit;

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

                dt = res.Data.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    r = dt.Rows[0];
                    #region Field values
                    string posname = Static.ToStr(r["POSNAME"]);
                    decimal cash_amount = Static.ToDecimal(r["CASH_AMOUNT"]);
                    decimal card_amount = Static.ToDecimal(r["CARD_AMOUNT"]);
                    decimal other_amount = Static.ToDecimal(r["OTHER_AMOUNT"]);

                    int cash_count = Static.ToInt(r["CASH_COUNT"]);
                    int card_count = Static.ToInt(r["CARD_COUNT"]);
                    int other_count = Static.ToInt(r["OTHER_COUNT"]);

                    decimal cashin_amount = Static.ToDecimal(r["CASHIN_AMOUNT"]);
                    decimal cashout_amount = -Static.ToDecimal(r["CASHOUT_AMOUNT"]);

                    int cashin_count = Static.ToInt(r["CASHIN_COUNT"]);
                    int cashout_count = Static.ToInt(r["CASHOUT_COUNT"]);

                    decimal total_amount = Static.ToDecimal(r["TOTAL_AMOUNT"]);
                    decimal refund_amount = Static.ToDecimal(r["REFUND_AMOUNT"]);

                    int sales_count = Static.ToInt(r["SALES_COUNT"]);
                    int refund_count = Static.ToInt(r["REFUND_COUNT"]);
                    #endregion

                    posname = " ".PadLeft((40 - posname.Length) / 2) + posname;
                    sb.AppendFormat("{0}\r\n", posname.ToUpper());

                    sb.Append("             [POS Closure]\r\n");
                    sb.AppendFormat("{0,-16:yyyy/MM/dd HH:mm}   Pos:{1,4}\r\n", DateTime.Now, Static.SubStr(posno, 0, 4));
                    sb.AppendLine("========================================");

                    sb.AppendLine("[Deposit Total]");
                    sb.AppendFormat("        Cash : {0,-4} {1,15:#,##0.00}\r\n", cash_count, cash_amount);
                    sb.AppendFormat("        Card : {0,-4} {1,15:#,##0.00}\r\n", card_count, card_amount);
                    sb.AppendFormat("       Other : {0,-4} {1,15:#,##0.00}\r\n", other_count, other_amount);
                    sb.AppendLine("----------------------------------------");
                    sb.AppendFormat("       Total : {0,-4} {1,15:#,##0.00}\r\n", cash_count + card_count + other_count, cash_amount + card_amount + other_amount);
                    sb.AppendLine("----------------------------------------");

                    sb.AppendLine("[Cash Total]");
                    sb.AppendFormat("   Start Amt : {0,-4} {1,15:#,##0.00}\r\n", cashin_count, cashin_amount);
                    sb.AppendFormat("   Cash Sale : {0,-4} {1,15:#,##0.00}\r\n", cash_count, cash_amount);
                    sb.AppendFormat(" Deposit Amt : {0,-4} {1,15:#,##0.00}\r\n", cashin_count + cash_count, cashin_amount + cash_amount);
                    sb.AppendFormat("  Cash Total : {0,-4} {1,15:#,##0.00}\r\n", cashout_count, cashout_amount);
                    sb.AppendLine("----------------------------------------");
                    sb.AppendFormat("   Over/Lack :      {0,15:#,##0.00}\r\n", cashin_amount + cash_amount + cashout_amount);
                    sb.AppendLine("----------------------------------------");

                    sb.AppendLine("[Sale Total]");
                    sb.AppendFormat("        Sale : {0,-4} {1,15:#,##0.00}\r\n", sales_count, total_amount);
                    sb.AppendFormat("      Return : {0,-4} {1,15:#,##0.00}\r\n", refund_count, refund_amount);
                    sb.AppendLine("----------------------------------------");
                    sb.AppendFormat("       Total : {0,-4} {1,15:#,##0.00}\r\n", sales_count + refund_count, total_amount + refund_amount);

                    sb.AppendLine("========================================");
                    sb.Append("\r\n\r\n");
                    sb.AppendFormat("Cashier:...............{0}\r\n"
                        , (string.IsNullOrEmpty(_core.RemoteObject.User.UserFName) ? "" : Static.SubStr(_core.RemoteObject.User.UserFName, 0, 1) + ".") + _core.RemoteObject.User.UserLName
                        );
                    sb.Append("\r\n\r\n");
                }


                #endregion
                #region Барааны жагсаалт бэлдэх

                sb.Append("\r\n");
                sb.Append("Service Name            Qty      Amount\r\n");
                sb.Append("----------------------------------------\r\n");

                dt = res.Data.Tables[1];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string oldtypename = "";
                    foreach (DataRow rr in dt.Rows)
                    {
                        string typename = Static.ToStr(rr["TYPENAME"]);
                        string prodname = Static.SubStr(Static.ToStr(rr["PRODNAME"]), 0, 20);
                        decimal qty = Static.ToDecimal(rr["QTY"]);
                        decimal amount = Static.ToDecimal(rr["TOTALAMOUNT"]);

                        if (oldtypename != typename)
                        {
                            sb.AppendFormat("{0}\r\n", typename);
                            oldtypename = typename;
                        }
                        sb.AppendFormat("  {0,-22}{1,3}{2,13:#,##0}\r\n", prodname, qty, amount);
                    }
                }
                sb.Append("----------------------------------------\r\n");
                

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
                res = new Result(211009, ex.ToString());
            }
        OnExit:
            if (res != null && res.ResultNo != 0)
            {
                _core.AlertShow("Ээлж хаалт", res.ResultDesc);
            }
            memoEdit1.EditValue = sb.ToString();
            status = 1;
            return sb.ToString();
        }
        public bool PosClosure_PrintBill()
        {
            //string text = PosClosure_PrepareBill(posno);
            string text = memoEdit1.Text;

            if (string.IsNullOrEmpty(text))
            {
                _core.AlertShow("ПОС хаалт", "Борлуулалтын мэдээ хоосон байна.", 2);
                return false;
            }

            string result = _core.Printer.Open();
            if (!string.IsNullOrEmpty(result)) goto OnExit;

            _core.Printer.Print(text);
            _core.Printer.Print("\r\n\r\n\r\n\r\n\r\n\x1Bi");
            _core.Printer.Close();

        OnExit:
            if (!string.IsNullOrEmpty(result))
            {
                _core.AlertShow("ПОС хаалт", result, 2);
            }
            return string.IsNullOrEmpty(result);
        }

        #endregion

    }
}
