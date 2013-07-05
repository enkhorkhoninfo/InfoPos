using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Design;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

using EServ.Shared;
using ISM.Template;
using ISM.CUser;

namespace InfoPos.Enquiry
{
    public partial class CustomerEnquiry : Form
    {
        #region [ Параметер ]
        Core.Core _core;
        long _CustomerID;
        private long _TypeCode = 0;
        bool LoadCustomer;
        bool LoadPosition;
        bool LoadPicture;
        bool LoadFamily;
        bool LoadUZ;
        bool LoadIT;
        bool LoadContact;
        bool LoadAccount;
        bool LoadAttach;
        bool LoadNote;
        bool LoadAdd;
        DataRow DR;
        int TablePrivSelect = 120006;
        int TablePrivUpdate = 120009;
        string TableNamePrefix = "Customer";
        #endregion
        #region [ Constructor Function ]
        public CustomerEnquiry(Core.Core core, long CustomerID): this(core, CustomerID, null)
        {
        }
        public CustomerEnquiry(Core.Core core, long CustomerID, object[] obj)
        {
            try
            {
                InitializeComponent();
                _core = core;
                _CustomerID = CustomerID;
                 DR = (DataRow)obj[0];
                 Init(1);
            }
            catch(Exception ex)
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
                    Init_PictureRef();             //Зургийн мэдээлэл
                    break;
                case 4:
                    Init_FamilyRef();               // Хамаатан садан
                    break;
                case 5:
                    Init_UZRef();                   // Өв залгамжлагч
                    break;
                case 6:
                    Init_ITRef();                   // Итгэмжлэгч
                    break;
                case 7:
                    Init_ContactRef();              // Холбоо барисан мэдээлэл
                    break;
                case 8:
                    Init_AccountRef();              // Дансны мэдээлэл
                    break;
                case 9:
                    Init_AttachRef();               // Хавсралт файлууд
                    break;
                case 10:
                    Init_NoteRef();                 // Товч дүгнэлт
                    break;
                case 11:
                    Init_AddRef();                  // Нэмэлт мэдээлэл
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
                Function();
                ISM.Template.FormUtility.SetFormatGrid(ref gvwCustomer, false);
                gvwCustomer.GroupPanelText = "Бүлэглэх баганаа оруулна уу";
                
                _CustomerID = Static.ToLong(DR["CustomerNo"]);
               
                dd.Rows.Add("Харилцагчийн дугаар", Static.ToStr(DR["CustomerNo"]));
                dd.Rows.Add("Харилцагчийн төрөл", Static.ToStr(DR["ClassCode"]));
                dd.Rows.Add("Байгууллагын төрөл", Static.ToStr(DR["TypeCode"]));
                dd.Rows.Add("Үйл ажиллагааны чиглэл", Static.ToStr(DR["InduTypeCode"]));
                dd.Rows.Add("Үйл ажиллагааны дэд чиглэл", Static.ToStr(DR["InduSubTypeCode"]));
                dd.Rows.Add("Эцэг эхийн нэр", Static.ToStr(DR["FirstName"]));
                dd.Rows.Add("Харилцагчийн нэр", Static.ToStr(DR["LastName"]));
                dd.Rows.Add("Овог", Static.ToStr(DR["MiddleName"]));
                dd.Rows.Add("Байгууллагын нэр", Static.ToStr(DR["CorporateName"]));
                dd.Rows.Add("Байгууллагын нэр 2", Static.ToStr(DR["CorporateName2"]));
                dd.Rows.Add("Регистерийн дугаар", Static.ToStr(DR["RegisterNo"]));
                dd.Rows.Add("Иргэний үнэмлэхний дугаар", Static.ToStr(DR["PassNo"]));
                dd.Rows.Add("Хүйс", Static.ToStr(DR["Sex"]));
                dd.Rows.Add("Төрсөн огноо", Static.ToStr(DR["BirthDay"]));
                dd.Rows.Add("Ажиллаж буй албан байгууллага", Static.ToStr(DR["Company"]));
                dd.Rows.Add("Албан тушаал", Static.ToStr(DR["Position"]));
                dd.Rows.Add("Туршлага", Static.ToStr(DR["Experience"]));
                dd.Rows.Add("Захиралын эцэг эхийн нэр", Static.ToStr(DR["DirFirstName"]));
                dd.Rows.Add("Захиралын нэр", Static.ToStr(DR["DirLastName"]));
                dd.Rows.Add("Захиралын овог", Static.ToStr(DR["DirMiddleName"]));
                dd.Rows.Add("Захиралын регистерийн дугаар", Static.ToStr(DR["DirRegisterNo"]));
                dd.Rows.Add("Захиралын иргэний үнэмлэхний дугаар", Static.ToStr(DR["DirPassNo"]));
                dd.Rows.Add("Захиралын хүйс", Static.ToStr(DR["DirSex"]));
                dd.Rows.Add("Захиралын төрсөн огноо", Static.ToStr(DR["DirBirthDay"]));
                dd.Rows.Add("Майл хаяг", Static.ToStr(DR["Email"]));
                dd.Rows.Add("Ажлын утасны дугаар", Static.ToStr(DR["Telephone"]));
                dd.Rows.Add("Гар утасны дугаар", Static.ToStr(DR["Mobile"]));
                dd.Rows.Add("Гэрийн утасны дугаар", Static.ToStr(DR["HomePhone"]));
                dd.Rows.Add("Факсын дугаар", Static.ToStr(DR["Fax"]));
                dd.Rows.Add("Вэб хуудас", Static.ToStr(DR["WebSite"]));
                dd.Rows.Add("Харилцагч нь аль салбарын үйл ажиллагаа хийдэг", Static.ToStr(DR["SpecialApproval"]));
                dd.Rows.Add("Харилцагчийн зэрэглэлийн код", Static.ToStr(DR["RateCode"]));
                dd.Rows.Add("Харилцагчийн улсын код", Static.ToStr(DR["CountryCode"]));
                dd.Rows.Add("Харилцагчийн хэлний код", Static.ToStr(DR["LanguageCode"]));
                dd.Rows.Add("Тухайн байгууллагаас өөр байгууллагад даатгалтай эсэх", Static.ToStr(DR["isOtherInsurance"]));
                dd.Rows.Add("Эрүүл мэндийн даатгалтай эсэх", Static.ToStr(DR["isHInsurance"]));
                dd.Rows.Add("Нийгмийн даатгалтай эсэх", Static.ToStr(DR["isSInsurance"]));
                dd.Rows.Add("Салбарын дугаар", Static.ToStr(DR["BranchNo"]));
                dd.Rows.Add("Харилцагчийн төлөв", Static.ToStr(DR["Status"]));
                dd.Rows.Add("Харилцагчийн төлөв", Static.ToStr(DR["StatusName"]));

                //DriverNo, createdate, createuser, oldid, Height, Foot
                dd.Rows.Add("Жолооны үнэмлэхний дугаар", Static.ToStr(DR["DriverNo"]));
                dd.Rows.Add("Үүсэгсэн огноо", Static.ToStr(DR["createdate"]));
                dd.Rows.Add("Үүсэгсэн хэрэглэгч", Static.ToStr(DR["createuser"]));
                dd.Rows.Add("Хуучин дугаар", Static.ToStr(DR["oldid"]));
                dd.Rows.Add("Өндөр", Static.ToStr(DR["Height"]));
                dd.Rows.Add("Гутлын хэмжээ", Static.ToStr(DR["Foot"]));

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
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120006, 120006, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                
                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdPosition.DataSource = null;
                grdPosition.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwPosition, false);
                gvwPosition.GroupPanelText = "Бүлэглэх баганаа оруулна уу";
              //  gvwPosition.Columns[0].Width = 150;
              //  gvwPosition.Columns[1].Width = 130;
              //  gvwPosition.Columns[2].Width = 130;
              //  gvwPosition.Columns[3].Width = 130;
              //  gvwPosition.Columns[4].Width = 130;
              //  gvwPosition.Columns[5].Width = 180;
              //  gvwPosition.Columns[6].Width = 130;
              //  gvwPosition.Columns[7].Width = 180;
                gvwPosition.OptionsView.ColumnAutoWidth = false;
                gvwPosition.BestFitColumns();

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
        #region [ Зургийн лавлагаа ]
        private void Init_PictureRef()
        {
            if (!LoadPicture)
            {
                Init_Picture(_CustomerID);
            }
        }
        public void Init_Picture(long CustomerID)
        {
            try 
            {
                object[] obj = new object[4];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120011, 120011, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdPicture.DataSource = null;
                grdPicture.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwPicture, false);
                gvwPicture.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
              //  gvwPicture.Columns[0].Width = 150;
              //  gvwPicture.Columns[1].Width = 130;
              //  gvwPicture.Columns[2].Width = 130;
              //  gvwPicture.Columns[3].Width = 130;
                gvwPicture.OptionsView.ColumnAutoWidth = false;
                gvwPicture.BestFitColumns();

                gvwPicture.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwPicture.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwPicture.Columns[2].Caption = "Зургийн төрөл";
                gvwPicture.Columns[3].Caption = "Хавсралт файлын ID";

                LoadPicture = true;
            }
            catch(Exception ex)
            {
                 MessageBox.Show("Зургийн лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #region [ Хамаатан садан ]
        private void Init_FamilyRef()
        {
            if (!LoadFamily)
            {
                Init_Family(_CustomerID);
            }
        }
        public void Init_Family(long CustomerID)
        {
            try
            {
                object[] obj = new object[11];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120021, 120021, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdFamily.DataSource = null;
                grdFamily.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwFamily, false);
                gvwFamily.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
              //  gvwFamily.Columns[0].Width = 150;
              //  gvwFamily.Columns[1].Width = 150;
              //  gvwFamily.Columns[2].Width = 150;
              //  gvwFamily.Columns[3].Width = 150;
              //  gvwFamily.Columns[4].Width = 150;
              //  gvwFamily.Columns[5].Width = 150;
              //  gvwFamily.Columns[6].Width = 150;
              //  gvwFamily.Columns[7].Width = 150;
              //  gvwFamily.Columns[8].Width = 150;
              //  gvwFamily.Columns[9].Width = 150;
                gvwFamily.OptionsView.ColumnAutoWidth = false;
                gvwFamily.BestFitColumns();

                gvwFamily.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwFamily.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwFamily.Columns[2].Caption = "Хамаатны төрөл";
                gvwFamily.Columns[3].Caption = "Эцэг эхийн нэр";

                gvwFamily.Columns[4].Caption = "Овог";
                gvwFamily.Columns[5].Caption = "Регистерийн дугаар";
                gvwFamily.Columns[6].Caption = "Иргэний үнэмлэхний дугаар";
                gvwFamily.Columns[7].Caption = "И-майл хаяг";

                gvwFamily.Columns[8].Caption = "Гэрийн утасны дугаар";
                gvwFamily.Columns[9].Caption = "Гар утасны дугаар";
                gvwFamily.Columns[10].Caption = "Ажлын утасны дугаар";

                LoadFamily = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хамаатан садны лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #region [ Өв залгамжлагч ]
        private void Init_UZRef()
        {
            if (!LoadUZ)
            {
                Init_UZ(_CustomerID);
            }
        }
        public void Init_UZ(long CustomerID)
        {
            try
            {
                object[] obj = new object[10];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120026, 120026, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                //Function();
                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdUZ.DataSource = null;
                grdUZ.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwUZ, false);
                gvwUZ.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
              //  gvwUZ.Columns[0].Width = 150;
              //  gvwUZ.Columns[1].Width = 150;
              //  gvwUZ.Columns[2].Width = 150;
              //  gvwUZ.Columns[3].Width = 150;
              //  gvwUZ.Columns[4].Width = 150;
              //  gvwUZ.Columns[5].Width = 150;
              //  gvwUZ.Columns[6].Width = 150;
              //  gvwUZ.Columns[7].Width = 150;
              //  gvwUZ.Columns[8].Width = 150;
              //  gvwUZ.Columns[9].Width = 150;
                gvwUZ.OptionsView.ColumnAutoWidth = false;
                gvwUZ.BestFitColumns();

                gvwUZ.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwUZ.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwUZ.Columns[2].Caption = "Хамаатны төрөл";
                gvwUZ.Columns[3].Caption = "Эцэг эхийн нэр";

                gvwUZ.Columns[4].Caption = "Овог";
                gvwUZ.Columns[5].Caption = "Регистерийн дугаар";
                gvwUZ.Columns[6].Caption = "Иргэний үнэмлэхний дугаар";
                gvwUZ.Columns[7].Caption = "И-майл хаяг";

                gvwUZ.Columns[8].Caption = "Гэрийн утасны дугаар";
                gvwUZ.Columns[9].Caption = "Гар утасны дугаар";

                LoadUZ = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Өв залгамжлагчийн лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #region [ Итгэмжлэгч ]
        private void Init_ITRef()
        {
            if (!LoadIT)
            {
                Init_IT(_CustomerID);
            }
        }
        public void Init_IT(long CustomerID)
        {
            try
            {
                object[] obj = new object[11];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120031, 120031, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdIT.DataSource = null;
                grdIT.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwIT, false);
                gvwIT.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
             //   gvwIT.Columns[0].Width = 150;
             //   gvwIT.Columns[1].Width = 150;
             //   gvwIT.Columns[2].Width = 150;
             //   gvwIT.Columns[3].Width = 150;
             //   gvwIT.Columns[4].Width = 150;
             //   gvwIT.Columns[5].Width = 150;
             //   gvwIT.Columns[6].Width = 150;
             //   gvwIT.Columns[7].Width = 150;
             //   gvwIT.Columns[8].Width = 150;
             //   gvwIT.Columns[9].Width = 150;
             //   gvwIT.Columns[10].Width = 150;
                gvwIT.OptionsView.ColumnAutoWidth = false;
                gvwIT.BestFitColumns();

                gvwIT.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwIT.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwIT.Columns[2].Caption = "Хамаатны төрөл";
                gvwIT.Columns[3].Caption = "Эцэг эхийн нэр";
                gvwIT.Columns[4].Caption = "Овог";
                gvwIT.Columns[5].Caption = "Регистерийн дугаар";
                gvwIT.Columns[6].Caption = "Иргэний үнэмлэхний дугаар";
                gvwIT.Columns[7].Caption = "И-майл хаяг";
                gvwIT.Columns[8].Caption = "Гэрийн утасны дугаар";
                gvwIT.Columns[9].Caption = "Гар утасны дугаар";
                gvwIT.Columns[10].Caption = "Албан тушаал";
                LoadIT = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Итгэмжлэгчийн лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #region [ Холбоо барьсан мэдээлэл ]
        private void Init_ContactRef()
        {
            if (!LoadContact)
            {
                Init_Contact(_CustomerID);
            }
        }
        public void Init_Contact(long CustomerID)
        {
            try
            {
                object[] obj = new object[7];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120036, 120036, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdContact.DataSource = null;
                grdContact.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwContact, false);
                gvwContact.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
              //  gvwContact.Columns[0].Width = 150;
              //  gvwContact.Columns[1].Width = 150;
              //  gvwContact.Columns[2].Width = 150;
              //  gvwContact.Columns[3].Width = 150;
              //  gvwContact.Columns[4].Width = 180;
              //  gvwContact.Columns[5].Width = 200;
              //  gvwContact.Columns[6].Width = 150;
                gvwContact.OptionsView.ColumnAutoWidth = false;
                gvwContact.BestFitColumns();

                gvwContact.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwContact.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwContact.Columns[2].Caption = "Огноо";
                gvwContact.Columns[3].Caption = "Огноо, цаг минут";
                gvwContact.Columns[4].Caption = "Холбоо барьсан тэмдэглэл";
                gvwContact.Columns[5].Caption = "Холбоо барьсан талаарх тэмдэглэл";
                gvwContact.Columns[6].Caption = "Товч утга";
                LoadContact = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Холбоо барьсан тэмдэглэл лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #region [ Дансны мэдээлэл]
        private void Init_AccountRef()
        {
            if (!LoadAccount)
            {
                Init_Account(_CustomerID);
            }
        }
        public void Init_Account(long CustomerID)
        {
            try
            {
                object[] obj = new object[5];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120041, 120041, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdAccount.DataSource = null;
                grdAccount.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwAccount, false);
                gvwAccount.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
               // gvwAccount.Columns[0].Width = 150;
               // gvwAccount.Columns[1].Width = 150;
               // gvwAccount.Columns[2].Width = 120;
               // gvwAccount.Columns[3].Width = 90;
               // gvwAccount.Columns[4].Width = 120;
                gvwAccount.OptionsView.ColumnAutoWidth = false;
                gvwAccount.BestFitColumns();

                gvwAccount.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwAccount.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwAccount.Columns[2].Caption = "Банкны дугаар";
                gvwAccount.Columns[3].Caption = "Валют";
                gvwAccount.Columns[4].Caption = "Дансны дугаар";

                LoadAccount = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Дансны лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #region [ Хавсралт файлууд ]
        private void Init_AttachRef()
        {
            if (!LoadAttach)
            {
                Init_Attach(_CustomerID);
            }
        }
        public void Init_Attach(long CustomerID)
        {
            try
            {
                object[] obj = new object[3];
                obj[0] = Static.ToLong(CustomerID);
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120046, 120046, obj);
                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                _CustomerID = Static.ToLong(DR["CustomerNo"]);
                grdAttach.DataSource = null;
                grdAttach.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gvwAttach, false);
                gvwAttach.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
              //  gvwAttach.Columns[0].Width = 150;
              //  gvwAttach.Columns[1].Width = 150;
              //  gvwAttach.Columns[2].Width = 130;
                gvwAttach.OptionsView.ColumnAutoWidth = false;
                gvwAttach.BestFitColumns();

                gvwAttach.Columns[0].Caption = "Харилцагчийн дугаар";
                gvwAttach.Columns[1].Caption = "Дэс дугаарын дугаар";
                gvwAttach.Columns[2].Caption = "Хавсралтын ID";


                LoadAttach = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хавсралт файлын лавлагааг дахин ачааллах үед алдаа гарлаа" + ex.Message);
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
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120051, 120051, obj);
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
        #region [ Нэмэлт мэдээлэл ]
        private void Init_AddRef()
        {
            if (!LoadAdd)
            {
                Init_Add(_CustomerID);
            }
        }
        public void Init_Add(long CustomerID)
        {
            try
            {
                _TypeCode = Static.ToLong(DR["TypeCode"]);
                ucAdd.TablePrivSelect = TablePrivSelect;
                ucAdd.TablePrivUpdate = TablePrivUpdate;
                ucAdd.TableTypeId = (ulong)_TypeCode;
                ucAdd.Remote = _core.RemoteObject;
                ucAdd.TableRowKey = (ulong)_CustomerID;
                ucAdd.TableNamePrefix = TableNamePrefix;
                Result res = ucAdd.ListRead();

                if (res.ResultNo != 0)
                    MessageBox.Show(string.Format("Result: code={0} desc={1}", res.ResultNo, res.ResultDesc));
                LoadAdd = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нэмэлт мэдээлэл унших явцад алдаа гарлаа:" + ex.Message);
            }
        }
        #endregion
        #endregion
        #region [ Цэс холболт ]
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = true;
            Init(1);
            
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPosition.Visible = true;
            Init(2);
        }

        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            this.Close();
        }
        
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = true;
            Init(3);
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = true;
            Init(4);
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = true;
            Init(5);
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = true;
            Init(6);
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = true;
            Init(7);
        }

        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = true;
            Init(8);
        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = true;
            Init(9);
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            ucAdd.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = true;
            Init(10);
        }

        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            grdPosition.Visible = false;
            grdCustomer.Visible = false;
            grdPicture.Visible = false;
            grdFamily.Visible = false;
            grdUZ.Visible = false;
            grdIT.Visible = false;
            grdContact.Visible = false;
            grdAccount.Visible = false;
            grdAttach.Visible = false;
            grdNote.Visible = false;
            ucAdd.Visible = true;
            Init(11);
        }
        #endregion
        #region [ Зураг оруулах ]
        void Function()
        {
            if (_core.Resource != null)
            {
                //simpleButton1.Image = _core.Resource.GetImage("navigate_cancel");
                navBarGroup1.LargeImage = _core.Resource.GetImage("navigate_refresh");
                navBarGroup2.LargeImage = _core.Resource.GetImage("navigate_delete");
                navBarItem7.LargeImage = _core.Resource.GetImage("navigate_cancel");
                navBarItem1.LargeImage = _core.Resource.GetImage("navigate_cancel");
            }
        }
        #endregion
        private void CustomerEnquiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void CustomerEnquiry_Load(object sender, EventArgs e)
        {
            gvwCustomer.OptionsView.ColumnAutoWidth = false;
            gvwCustomer.BestFitColumns();
        }
    }
}
