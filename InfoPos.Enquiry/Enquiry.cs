using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;

using ISM.Template;

namespace InfoPos.Enquiry
{
    public partial class Enquiry : Form
    {
        #region [ Variable ]
        private Core.Core _core;
        private string Enquiry_Type;
        private DataRow Enquiry_DR;
        private string _ID = "";
        private object[] _obj;
        #endregion
        #region [ Init ]
        public Enquiry(Core.Core core, string eType, string ID, DataRow DR, object[] param)
        {
            InitializeComponent();

            _core = core;
            Enquiry_Type = eType;
            Enquiry_DR = DR;
            _ID = ID;
            _obj = param;
        }
        private void Enquiry_Load(object sender, EventArgs e)
        {
            InitData();
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.BestFitColumns();
        }
        #endregion
        #region [ Buttons ]
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 
        #region [ InitData ]
        private Result InitData()
        {
            Result res = new Result();

            switch (Enquiry_Type)
            {
                case "Journal":
                    Init_Journal(Enquiry_DR);
                    this.Text = "Гүйлгээ журналын дэлгэрэнгүй лавлагаа";
                    btnStatement.Visible = false;
                    break;
                case "PendJournal":
                    Init_PendJournal(Enquiry_DR);
                    this.Text = "Хүлээлгийн гүйлгээ журналын дэлгэрэнгүй лавлагаа";
                    btnStatement.Visible = false;
                    break;
                case "FA":
                    Init_FA(Enquiry_DR);
                    this.Text = "Үндсэн хөрөнгийн дэлгэрэнгүй лавлагаа";
                    break;
                case "INV":
                    Init_INV(Enquiry_DR);
                    this.Text = "Бараа материалын дэлгэрэнгүй лавлагаа";
                    break;
                case "BAC":
                    Init_BAC(Enquiry_DR);
                    this.Text = "Байгууллагын дансны дэлгэрэнгүй лавлагаа";
                    break;
                case "CON":
                    Init_CON(Enquiry_DR);
                    this.Text = "Балансын гадуурх дансны дэлгэрэнгүй лавлагаа";
                    break;
            }
            gridView1.OptionsView.ShowGroupPanel = false;
            return res;
        }
        #endregion
        public void Init_PendJournal(DataRow DR)
        {
            object[] obj = new object[1];
            Result res;
            try
            {
                obj[0] = Static.ToLong(_ID);
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 209, 231011, 231011, obj);
                if (res.ResultNo == 0)
                {
                    res = LoadPendJournal(res.Data);
                    if (res.ResultNo != 0) MessageBox.Show(res.ResultDesc);
                }
                else
                {
                    MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Result LoadPendJournal(DataSet DS)
        {
            Result res = new Result();
            DataRow DR;
            try
            {
                if (DS == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0].Rows.Count < 1) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }

                DR = DS.Tables[0].Rows[0];

                DataTable dd = new DataTable();
                dd.Columns.Add("Талбар", Type.GetType("System.String"));
                dd.Columns.Add("Утга", Type.GetType("System.String"));
                ISM.Template.FormUtility.SetFormatGrid(ref gridView1, false);

                dd.Rows.Add("Журналын дугаар", Static.ToStr(DR["JrNo"]));
                dd.Rows.Add("Дэд журналын дугаар", Static.ToStr(DR["JrSubNo"]));
                dd.Rows.Add("Гүйлгээний огноо", Static.ToStr(DR["TxnDate"]));
                dd.Rows.Add("Гүйлгээний огноо, цаг", Static.ToStr(DR["PostDate"]));
                dd.Rows.Add("Гүйлгээ хийсэн хэрэглэгч", Static.ToStr(DR["UserNo"]));
                dd.Rows.Add("Гүйлгээ хийсэн PC нэр", Static.ToStr(DR["HostName"]));
                dd.Rows.Add("Гүйлгээ хийсэн PC IP", Static.ToStr(DR["HostIP"]));
                dd.Rows.Add("Гүйлгээ хийсэн PC MAC", Static.ToStr(DR["HostMAC"]));
                dd.Rows.Add("Гүйлгээний код", Static.ToStr(DR["TxnCode"]));
                dd.Rows.Add("Гүйлгээний төрөл", Static.ToStr(DR["TxnEntry"]));
                dd.Rows.Add("Дебит дансны төрөл", Static.ToStr(DR["DRAccountMod"]));
                dd.Rows.Add("Дебит дансны дугаар", Static.ToStr(DR["DRAccountNo"]));

                dd.Rows.Add("Дебит гүйлгээний мөнгөн дүн", Static.ToStr(DR["DRAmount"]));
                dd.Rows.Add("Дебит гүйлгээний ханш", Static.ToStr(DR["DRRate"]));
                dd.Rows.Add("Дебит гүйлгээний валют", Static.ToStr(DR["DRCurCode"]));
                dd.Rows.Add("Дебет гүйлгээний мөнгөн дүнгийн үлдэгдэл", Static.ToStr(DR["DRAmountBalance"]));
                dd.Rows.Add("Кредит дансны төрөл", Static.ToStr(DR["CRAccountMod"]));
                dd.Rows.Add("Кредит дансны дугаар", Static.ToStr(DR["CRAccountNo"]));
                dd.Rows.Add("Кредит гүйлгээний мөнгөн дүн", Static.ToStr(DR["CRAmount"]));
                dd.Rows.Add("Кредит гүйлгээний ханш", Static.ToStr(DR["CRRate"]));
                dd.Rows.Add("Кредит гүйлгээний валют", Static.ToStr(DR["CRCurCode"]));
                dd.Rows.Add("Гүйлгээний утга", Static.ToStr(DR["Description"]));
                dd.Rows.Add("Холбогдох төрлийн дугаар", Static.ToStr(DR["ExtIDType"]));
                dd.Rows.Add("ID дугаар", Static.ToStr(DR["ExtID"]));
                dd.Rows.Add("Төлөв", Static.ToStr(DR["Status"]));

                gridControl1.DataSource = null;
                gridControl1.DataSource = dd;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message;
            }
            return res;
        }
        public void Init_Journal(DataRow DR)
        {
            object[] obj = new object[2];
            Result res;
            try
            {
                obj[0] = Static.ToLong(_ID);
                obj[1] = Static.ToLong(_obj[3]);
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 209, 231010, 231010, obj);
                if (res.ResultNo == 0)
                {
                    res = LoadJournal(res.Data);
                    if (res.ResultNo != 0) MessageBox.Show(res.ResultDesc);
                }
                else
                {
                    MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Result LoadJournal(DataSet DS)
        {
            Result res = new Result();
            DataRow DR;
            try
            {
                if (DS == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0].Rows.Count < 1) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }

                DR = DS.Tables[0].Rows[0];

                DataTable dd = new DataTable();
                dd.Columns.Add("Талбар", Type.GetType("System.String"));
                dd.Columns.Add("Утга", Type.GetType("System.String"));
                ISM.Template.FormUtility.SetFormatGrid(ref gridView1, false);

                dd.Rows.Add("Дансны төрөл", Static.ToStr(DR["TxnType"]));
                dd.Rows.Add("Журнал дугаар", Static.ToStr(DR["JRNO"]));
                dd.Rows.Add("Хэсэг дугаар", Static.ToStr(DR["PartJrNo"]));
                dd.Rows.Add("Дэд дугаар", Static.ToStr(DR["SUBJRNO"]));
                dd.Rows.Add("Дэс дугаар", Static.ToStr(DR["SEQNO"]));

                dd.Rows.Add("Дт/Кт", Static.ToStr(DR["TXNENTRY"]));
                dd.Rows.Add("Огноо", Static.ToStr(DR["TXNDATE"]));
                dd.Rows.Add("Огноо цаг,минут", Static.ToStr(DR["POSTDATE"]));

                dd.Rows.Add("Дансны дугаар", Static.ToStr(DR["ACCOUNTNO"]));
                dd.Rows.Add("Салбар", Static.ToStr(DR["BRANCHNO"]));
                dd.Rows.Add("Бүтээгдэхүүн", Static.ToStr(DR["PRODCODE"]));
                dd.Rows.Add("Хэрэглэгч", Static.ToStr(DR["USERNO"]));
                dd.Rows.Add("HOST", Static.ToStr(DR["HOSTNAME"]));
                dd.Rows.Add("IP", Static.ToStr(DR["HOSTIP"]));
                dd.Rows.Add("MAC", Static.ToStr(DR["HOSTMAC"]));

                dd.Rows.Add("Гүйлгээ код", Static.ToStr(DR["TXNCODE"]));
                dd.Rows.Add("Гүйлгээ дүн", Static.ToStr(DR["AMOUNT"]));
                dd.Rows.Add("Гүйлгээ ханш", Static.ToStr(DR["RATE"]));
                dd.Rows.Add("Гүйлгээ валют", Static.ToStr(DR["CURCODE"]));
                dd.Rows.Add("Гүйлгээ дараах үлдэгдэл", Static.ToStr(DR["BALANCE"]));

                dd.Rows.Add("Харьцсан данс", Static.ToStr(DR["CONTACCCOUNTNO"]));
                dd.Rows.Add("Харьцсан дансны валют", Static.ToStr(DR["CONTCURRCODE"]));
                dd.Rows.Add("Харьцсан валютын ханш", Static.ToStr(DR["CONTRATE"]));
                dd.Rows.Add("Харьцсан дансны дүн", Static.ToStr(DR["CONTAMOUNT"]));
                dd.Rows.Add("Суурь үлдэгдэл", Static.ToStr(DR["BASEAMOUNT"]));

                dd.Rows.Add("Гүйлгээ утга", Static.ToStr(DR["DESCRIPTION"]));
                dd.Rows.Add("Буцаасан эсэх", Static.ToStr(DR["CORR"]));
                dd.Rows.Add("Бэлэн гүйлгээ эсэх", Static.ToStr(DR["ISCASH"]));
                dd.Rows.Add("Хянасан хэрэглэгч", Static.ToStr(DR["SUPERVISOR"]));
                dd.Rows.Add("ЕД лүү илгээсэн эсэх", Static.ToStr(DR["FLAG"]));

                dd.Rows.Add("Гол гүйлгээ эсэх", Static.ToStr(DR["M"]));
                dd.Rows.Add("Групп гүйлгээ код", Static.ToStr(DR["GroupTxnCode"]));

                gridControl1.DataSource = null;
                gridControl1.DataSource = dd;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message;
            }
            return res;
        }
        #region [ Үндсэн хөрөнгийн дэлгэрэнгүй ]
        public void Init_FA(DataRow DR)
        {
            object[] obj = new object[1];
            Result res;
            try
            {
                obj[0] = Static.ToLong(_ID);
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 216, 160002, 160002, obj);
                if (res.ResultNo == 0)
                {
                    res = LoadFAData(res.Data);
                    if (res.ResultNo != 0) MessageBox.Show(res.ResultDesc);
                }
                else
                {
                    MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Result LoadFAData(DataSet DS)
        {
            Result res= new Result();
            DataRow DR;
            try
            {
                if (DS == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0].Rows.Count <1) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }

                DR = DS.Tables[0].Rows[0];

                DataTable dd = new DataTable();
                dd.Columns.Add("Талбар", Type.GetType("System.String"));
                dd.Columns.Add("Утга", Type.GetType("System.String"));
                ISM.Template.FormUtility.SetFormatGrid(ref gridView1, false);
              //  gridView1.OptionsView.ColumnAutoWidth = false;
              //  gridView1.BestFitColumns();
                //FAID, FATYPEID, NAME, NAME2, BRANCHNO, CREATEUSER, UNITTYPECODE, BALANCECOUNT, UNITCOST, BALANCETOTAL,
                //CURRCODE, POSITION, ACCOUNTNO, EMPNO, STARTDATE, ENDDATE, LASTTELLERTXNDATE, STATUS, DEPRECIATION, LASTDEPDATE

                dd.Rows.Add("Үндсэн хөрөнгийн код", Static.ToStr(DR["FAID"]));
                dd.Rows.Add("Үндсэн хөрөнгийн төрөл", Static.ToStr(DR["FATYPEID"]));
                dd.Rows.Add("Үндсэн хөрөнгийн нэр", Static.ToStr(DR["NAME"]));
                dd.Rows.Add("Салбар", Static.ToStr(DR["BRANCHNO"]));

                dd.Rows.Add("Хэмжих нэгж", Static.ToStr(DR["UNITTYPECODE"]));
                dd.Rows.Add("Үлдэгдэл Тоо ширхэг", Static.ToStr(DR["BALANCECOUNT"]));
                dd.Rows.Add("Нэгж үнэ", Static.ToStr(DR["UNITCOST"]));
                dd.Rows.Add("Үлдэгдэл Мөнгөн дүн", Static.ToStr(DR["BALANCETOTAL"]));
                dd.Rows.Add("Хуримтлагдсан элэгдэл", Static.ToStr(DR["DEPRECIATION"])); 
                dd.Rows.Add("Валютын код", Static.ToStr(DR["CURRCODE"]));

                dd.Rows.Add("ҮХ-ийн данс", Static.ToStr(DR["ACCOUNTNO"]));
                dd.Rows.Add("Эзэмшигчийн дугаар", Static.ToStr(DR["EMPNO"]));
                dd.Rows.Add("Эхлэх огноо", Static.ToStr(DR["STARTDATE"]));
                dd.Rows.Add("Дуусах огноо", Static.ToStr(DR["ENDDATE"])); 
                
                gridControl1.DataSource = null;
                gridControl1.DataSource = dd;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message;
            }
            return res;
        }
        #endregion
        #region [ Бараа материалын дэлгэрэнгүй ]
        public void Init_INV(DataRow DR)
        {
            object[] obj = new object[1];
            Result res;
            try
            {
                obj[0] = Static.ToLong(_ID);
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 204, 140212, 140212, obj);
                if (res.ResultNo == 0)
                {
                    res = LoadINVData(res.Data);
                    if (res.ResultNo != 0) MessageBox.Show(res.ResultDesc);
                }
                else
                {
                    MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Result LoadINVData(DataSet DS)
        {
            Result res = new Result();
            DataRow DR;
            try
            {
                if (DS == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0].Rows.Count < 1) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }

                DR = DS.Tables[0].Rows[0];

                DataTable dd = new DataTable();
                dd.Columns.Add("Талбар", Type.GetType("System.String"));
                dd.Columns.Add("Утга", Type.GetType("System.String"));
                ISM.Template.FormUtility.SetFormatGrid(ref gridView1, false);
//a.InvId, a.InvType, it.name as InvTypeName, a.Name, a.Name2, a.BrandId, b.name as BrandIdName, a.PriceAmount,
//a.PriceRefund, a.Count, a.CatCode, ic.name as CatCodeName, a.BarCode, a.Unit, ut.name as unittypecodename, a.UnitSize,
//a.IsVat, decode(a.IsVat, 0, 'НӨАТ тооцно', 1, 'НӨАТ тооцохгүй') as IsVatName, a.PrinterType, a.CreateDate, a.Note, 
//a.Status, decode(a.Status, 0, 'Идэвхтэй', 1, 'Идэвхгүй') as StatusName, a.SalesAccountNo,
//a.RefundAccountNo, a.DiscountAccountNo, a.BonusAccountNo

                dd.Rows.Add("Бараа материалын код", Static.ToStr(DR["INVID"]));
                dd.Rows.Add("Бараа материалын төрөл", Static.ToStr(DR["INVTYPE"]));
                dd.Rows.Add("Бараа материалын төрлийн нэр", Static.ToStr(DR["InvTypeName"]));
                dd.Rows.Add("Бараа материалын нэр", Static.ToStr(DR["NAME"]));
                dd.Rows.Add("Бараа материалын нэр2", Static.ToStr(DR["Name2"]));
                dd.Rows.Add("Брэндийн дугаар", Static.ToStr(DR["BrandId"]));
                dd.Rows.Add("Брэндийн нэр", Static.ToStr(DR["BrandIdName"]));

                dd.Rows.Add("Үнэ", Static.ToStr(DR["PriceAmount"]));
                dd.Rows.Add("Нөхөн төлбөрийн дүн", Static.ToStr(DR["PriceRefund"]));
                dd.Rows.Add("Тоо", Static.ToStr(DR["Count"]));
                dd.Rows.Add("Ангиллын код", Static.ToStr(DR["CatCode"]));
                dd.Rows.Add("Ангиллын нэр", Static.ToStr(DR["CatCodeName"]));

                dd.Rows.Add("Бар код", Static.ToStr(DR["BarCode"]));
                dd.Rows.Add("Хэмжих нэгж", Static.ToStr(DR["Unit"]));
                dd.Rows.Add("Хэмжих нэгжийн нэр", Static.ToStr(DR["unittypecodename"]));
                dd.Rows.Add("Хэмжээ, размер", Static.ToStr(DR["UnitSize"]));
                dd.Rows.Add("НӨАТ тооцох эсэх", Static.ToStr(DR["IsVat"]));
                dd.Rows.Add("НӨАТ тооцох эсэх", Static.ToStr(DR["IsVatName"]));
                dd.Rows.Add("Принтерийн төрөл", Static.ToStr(DR["PrinterType"]));
                dd.Rows.Add("Үүсгэсэн огноо", Static.ToStr(DR["CreateDate"]));
                dd.Rows.Add("Барааны тайлбар", Static.ToStr(DR["Note"]));
                dd.Rows.Add("Төлөв", Static.ToStr(DR["Status"]));
                dd.Rows.Add("Төлвийн нэр", Static.ToStr(DR["StatusName"]));
                dd.Rows.Add("Борлуулалтын данс", Static.ToStr(DR["SalesAccountNo"]));
                dd.Rows.Add("Борлуулалтын буцаалт данс", Static.ToStr(DR["RefundAccountNo"]));
                dd.Rows.Add("Хөнгөлөлт тооцох данс", Static.ToStr(DR["DiscountAccountNo"]));
                dd.Rows.Add("Урамшуулал тооцох данс", Static.ToStr(DR["BonusAccountNo"]));

                gridControl1.DataSource = null;
                gridControl1.DataSource = dd;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message;
            }
            return res;
        }
        #endregion
        #region [ Байгууллагын дансны дэлгэрэнгүй ]
        public void Init_BAC(DataRow DR)
        {
            object[] obj = new object[1];
            Result res;
            try
            {
                obj[0] = Static.ToLong(_ID);
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 216, 180007, 180007, obj);
                if (res.ResultNo == 0)
                {
                    res = LoadBACData(res.Data);
                    if (res.ResultNo != 0) MessageBox.Show(res.ResultDesc);
                }
                else
                {
                    MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Result LoadBACData(DataSet DS)
        {
            Result res = new Result();
            DataRow DR;
            try
            {
                if (DS == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0].Rows.Count < 1) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }

                DR = DS.Tables[0].Rows[0];

                DataTable dd = new DataTable();
                dd.Columns.Add("Талбар", Type.GetType("System.String"));
                dd.Columns.Add("Утга", Type.GetType("System.String"));
                ISM.Template.FormUtility.SetFormatGrid(ref gridView1, false);
                //SELECT ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
                //STATUS, STARTDATE, ENDDATE

                dd.Rows.Add("Дансны дугаар", Static.ToStr(DR["ACCOUNTNO"]));
                dd.Rows.Add("Бүтээгдэхүүний код", Static.ToStr(DR["PRODCODE"]));
                dd.Rows.Add("Дансны нэр", Static.ToStr(DR["NAME"]));
                dd.Rows.Add("Салбар", Static.ToStr(DR["BRANCHNO"]));

                dd.Rows.Add("Үлдэгдэл дүн", Static.ToStr(DR["BALANCE"]));
                dd.Rows.Add("Валютын код", Static.ToStr(DR["CURCODE"]));

                dd.Rows.Add("Дансны зэрэглэл", Static.ToStr(DR["LEVELNO"]));
                dd.Rows.Add("Эхлэх огноо", Static.ToStr(DR["STARTDATE"]));
                dd.Rows.Add("Дуусах огноо", Static.ToStr(DR["ENDDATE"]));

                gridControl1.DataSource = null;
                gridControl1.DataSource = dd;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message;
            }
            return res;
        }
        #endregion
        #region [ Балансын гадуурх дансны дэлгэрэнгүй ]
        public void Init_CON(DataRow DR)
        {
            object[] obj = new object[1];
            Result res;
            try
            {
                obj[0] = Static.ToLong(_ID);
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 216, 190002, 190002, obj);
                if (res.ResultNo == 0)
                {
                    res = LoadCONData(res.Data);
                    if (res.ResultNo != 0) MessageBox.Show(res.ResultDesc);
                }
                else
                {
                    MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Result LoadCONData(DataSet DS)
        {
            Result res = new Result();
            DataRow DR;
            try
            {
                if (DS == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0] == null) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }
                if (DS.Tables[0].Rows.Count < 1) { res.ResultNo = 1; res.ResultDesc = "Мэдээлэл олдсонгүй"; return res; }

                DR = DS.Tables[0].Rows[0];

                DataTable dd = new DataTable();
                dd.Columns.Add("Талбар", Type.GetType("System.String"));
                dd.Columns.Add("Утга", Type.GetType("System.String"));
                ISM.Template.FormUtility.SetFormatGrid(ref gridView1, false);
                //SELECT ACCOUNTNO, NAME, NAME2, BRANCHNO, PRODCODE, BALANCE, CURCODE, USERNO, LEVELNO, CREATEDATE,
                //STATUS, STARTDATE, ENDDATE, CONTRACTID, INSURANCENO, RIINSURANCENO, CLAIMNO, CUSTNO, PERSON, LastTellerTxnDate

                dd.Rows.Add("Дансны дугаар", Static.ToStr(DR["ACCOUNTNO"]));
                dd.Rows.Add("Бүтээгдэхүүний код", Static.ToStr(DR["PRODCODE"]));
                dd.Rows.Add("Дансны нэр", Static.ToStr(DR["NAME"]));
                dd.Rows.Add("Салбар", Static.ToStr(DR["BRANCHNO"]));

                dd.Rows.Add("Үлдэгдэл дүн", Static.ToStr(DR["BALANCE"]));
                dd.Rows.Add("Валютын код", Static.ToStr(DR["CURCODE"]));

                dd.Rows.Add("Дансны зэрэглэл", Static.ToStr(DR["LEVELNO"]));
                dd.Rows.Add("Эхлэх огноо", Static.ToStr(DR["STARTDATE"]));
                dd.Rows.Add("Дуусах огноо", Static.ToStr(DR["ENDDATE"]));

                gridControl1.DataSource = null;
                gridControl1.DataSource = dd;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message;
            }
            return res;
        }
        #endregion
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Enquiry_Type != "")
            {
                object[] obj = new object[3];
                obj[0] = _core;
                switch (Enquiry_Type)
                {
                    case "BAC":
                        obj[1] = 0;
                        break;
                    case "CON":
                        obj[1] = 1;
                        break;
                    case "FA":
                        obj[1] = 2;
                        break;
                    case "INV":
                        obj[1] = 3;
                        break;
                }
                obj[2] = _ID;
                ISM.Lib.Static.Invoke("InfoPos.List.dll", "InfoPos.List.List", "CallAccountTxnList", obj);
            }
        }
        private void Enquiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
