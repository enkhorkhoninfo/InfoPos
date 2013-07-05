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
    public partial class CustContactEnquiry : Form
    {
        #region [ Параметер ]
        Core.Core _core;
        long _CustomerID;
        bool LoadCustomer;
        bool LoadPosition;
        bool LoadNote;
        DataRow DR;
        #endregion
        #region [ Constructor Function ]
        public CustContactEnquiry(Core.Core core, long CustomerID): this(core, CustomerID, null)
        {

        }

        public CustContactEnquiry(Core.Core core, long CustomerID, object[] obj)
        {
            try
            {
                InitializeComponent();
                _core = core;
                _CustomerID = CustomerID;
                DR = (DataRow)obj[0];
                Init(1);
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Обьектд хоосон утга дамжлаа" + ex);
            }
        }
        #endregion
        #region [ Init ]
        #region [ Switch_case ]
        public void Init(int _Type)
        {
            switch (_Type)
            {
                case 1:
                    Init_CustomerRef();             //Үндсэн мэдээлэл  
                    break;
                case 2:
                    Init_PositionRef();             //Хаягийн жагсаалт
                    break;
                case 3:
                    Init_NoteRef();                 // Товч дүгнэлт
                    break;
            }
        }
        #endregion
        #region [ Үндсэн мэдээлэл ]
        private void Init_CustomerRef()
        {
            if (!LoadCustomer)
            {
                Init_Customer(_CustomerID);
            }
        }
        public void Init_Customer(long CustomerID)
        {
            try
            {
                DataTable dd = new DataTable();
                dd.Columns.Add("Талбар", Type.GetType("System.String"));
                dd.Columns.Add("Утга", Type.GetType("System.String"));
                ISM.Template.FormUtility.SetFormatGrid(ref gvwCustomer, false);
                gvwCustomer.GroupPanelText = "Бүлэглэх баганаа оруулна уу";

                _CustomerID = Static.ToLong(DR["CustomerNo"]);

                dd.Rows.Add("Харилцагчийн дугаар", Static.ToStr(DR["CustomerNo"]));
                dd.Rows.Add("Харилцагчийн төрөл", Static.ToStr(DR["ClassCode"]));
                dd.Rows.Add("Байгууллагын төрөл", Static.ToStr(DR["TypeCode"]));
                dd.Rows.Add("Эцэг эхийн нэр", Static.ToStr(DR["FirstName"]));
                dd.Rows.Add("Харилцагчийн нэр", Static.ToStr(DR["LastName"]));
                dd.Rows.Add("Овог", Static.ToStr(DR["MiddleName"]));
                dd.Rows.Add("Байгууллагын нэр", Static.ToStr(DR["CorporateName"]));
                dd.Rows.Add("Регистерийн дугаар", Static.ToStr(DR["RegisterNo"]));
                dd.Rows.Add("Иргэний үнэмлэхний дугаар", Static.ToStr(DR["PassNo"]));
                dd.Rows.Add("Хүйс", Static.ToStr(DR["Sex"]));
                dd.Rows.Add("Төрсөн огноо", Static.ToStr(DR["BirthDay"]));
                dd.Rows.Add("Ажиллаж буй албан байгууллага", Static.ToStr(DR["Company"]));
                dd.Rows.Add("Албан тушаал", Static.ToStr(DR["Position"]));
                dd.Rows.Add("Туршлага", Static.ToStr(DR["Experience"]));
                dd.Rows.Add("Майл хаяг", Static.ToStr(DR["Email"]));
                dd.Rows.Add("Ажлын утасны дугаар", Static.ToStr(DR["Telephone"]));
                dd.Rows.Add("Гар утасны дугаар", Static.ToStr(DR["Mobile"]));
                dd.Rows.Add("Гэрийн утасны дугаар", Static.ToStr(DR["HomePhone"]));
                dd.Rows.Add("Факсын дугаар", Static.ToStr(DR["Fax"]));
                dd.Rows.Add("Вэб хуудас", Static.ToStr(DR["WebSite"]));
                dd.Rows.Add("Салбарын дугаар", Static.ToStr(DR["BranchNo"]));
                dd.Rows.Add("Харилцагчийн төлөв", Static.ToStr(DR["Status"]));
                dd.Rows.Add("Жолооны үнэмлэхний дугаар", Static.ToStr(DR["driverno"]));
                dd.Rows.Add("Үүсгэсэн огноо", Static.ToStr(DR["createdate"]));
                dd.Rows.Add("Үүсгэсэн хэрэглэгч", Static.ToStr(DR["createuser"]));

                grdCustomer.DataSource = null;
                grdCustomer.DataSource = dd;
                LoadCustomer = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Үндсэн мэдээллийг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #region [ Хаягийн лавлагаа ]
        private void Init_PositionRef()
        {
            if (!LoadPosition)
            {
                Init_Position(_CustomerID);
            }
        }
        public void Init_Position(long CustomerID)
        {
            try
            {
                object[] obj = new object[8];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 310135, 310135, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdPosition.DataSource = null;
                grdPosition.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwPosition, false);
                gvwPosition.GroupPanelText = "Бүлэглэх баганаа оруулна уу";
                gvwPosition.OptionsView.ColumnAutoWidth = false;
                gvwPosition.BestFitColumns();

//                SELECT CUSTOMERNO, SEQNO, CITYCODE, DISTCODE, SUBDISTCODE, NOTE, ADDRCURRENT, APARTMENT, postdate
//FROM CONTACTADDR
                gvwPosition.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwPosition.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwPosition.Columns[2].Caption = "Аймгийн хотын код";
                gvwPosition.Columns[3].Caption = "Сум, дүүргийн код";
                gvwPosition.Columns[4].Caption = "Баг, хорооны код";
                gvwPosition.Columns[5].Caption = "Байрны талаарх нэмэлт мэдээлэл";
                gvwPosition.Columns[6].Caption = "Нэмэлт мэдээлэл";
                gvwPosition.Columns[7].Caption = "Одоо идэвхитэй байгаа хаяг";
                gvwPosition.Columns[8].Caption = "Үүсгэсэн огноо";
                LoadPosition = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хаягийн лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }

        }
        #endregion
        #region [ Товч дүгнэлт ]
        private void Init_NoteRef()
        {
            if (!LoadNote)
            {
                Init_Note(_CustomerID);
            }
        }
        public void Init_Note(long CustomerID)
        {
            try
            {
                object[] obj = new object[6];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 310130, 310130, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdNote.DataSource = null;
                grdNote.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwNote, false);
                gvwNote.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
                //  gvwNote.Columns[0].Width = 150;
                //  gvwNote.Columns[1].Width = 150;
                //  gvwNote.Columns[2].Width = 80;
                //  gvwNote.Columns[3].Width = 100;
                //  gvwNote.Columns[4].Width = 180;
                //  gvwNote.Columns[5].Width = 250;
                gvwNote.OptionsView.ColumnAutoWidth = false;
                gvwNote.BestFitColumns();
//                SELECT CUSTOMERNO, SEQNO,TXNDATE, POSTDATE, USERNO, NOTE
//FROM CONTACTNOTE 
                gvwNote.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwNote.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwNote.Columns[2].Caption = "Огноо";
                gvwNote.Columns[3].Caption = "Огноо, цаг минут";
                gvwNote.Columns[4].Caption = "Өөрчилсөн теллерийн дугаар";
                gvwNote.Columns[5].Caption = "Тэмдэглэл";

                LoadNote = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Товч дүгнэлтийн лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #endregion
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = true;
            Init(1);
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPosition.Visible = true;
            Init(2);
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdNote.Visible = true;
            Init(3);
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.Close();
        }

        private void CustContactEnquiry_Load(object sender, EventArgs e)
        {
            gvwCustomer.OptionsView.ColumnAutoWidth = false;
            gvwCustomer.BestFitColumns();
        }

        private void CustContactEnquiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
