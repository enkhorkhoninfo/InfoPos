using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;

using EServ.Shared;
using ISM.Template;

namespace InfoPos.List
{
    public partial class CustomerList : Form
    {
        #region [ Variables ]
        private Core.Core _core;
        long CustomerID;
        int PrivNo = 120000;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Init ]
        public CustomerList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucCustomerList.Resource = _core.Resource;
    //       CustomerList.ActiveForm.AcceptButton = ucCustomerList.btnFind;
        }
        public CustomerList(Core.Core core,string pClassCode)
        {
            InitializeComponent();
            _core = core;
            Init1(pClassCode);
            ucCustomerList.Resource = _core.Resource;
            //       CustomerList.ActiveForm.AcceptButton = ucCustomerList.btnFind;
        }
        private void Init()
        {
            ucCustomerList.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucCustomerList_EventFindPaging);
            ucCustomerList.EventSelected += new ISM.Template.ucGridPanel.delegateEventSelected(ucCustomerList_EventSelected);
            ucCustomerList.EventDataChanged += new ISM.Template.ucGridPanel.delegateEventDataChanged(ucCustomerList_EventDataChanged);
            //ucCustomerList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
           // ucCustomerList.gridView1.Appearance.OddRow.Options.UseBackColor = false;
           
      
            ucCustomerList.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);
  
            labelControl1.BackColor = Color.NavajoWhite;
            labelControl2.BackColor = Color.SkyBlue;

            labelControl1.ForeColor = Color.NavajoWhite;
            labelControl2.ForeColor = Color.SkyBlue;
         
            Function();
            ucCustomerList.FindItemAdd("CustomerNo", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucCustomerList.FieldFindAdd("ClassCode", "Харилцагчийн төрөл", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("BranchNo", "Харилцагчийн салбар", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("Status", "Харилцагчийн төлөв", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("FirstName", "Эцэг эхийн нэр", typeof(string), "");
            
            ucCustomerList.FieldFindAdd("LastName", "Харилцагчийн нэр", typeof(string), "");
            ucCustomerList.FieldFindAdd("RegisterNo", "Регистерийн дугаар", typeof(string), "");
            ucCustomerList.FieldFindAdd("PassNo", "Иргэний үнэмлэхний дугаар", typeof(string), "");
            ucCustomerList.FieldFindAdd("Sex", "Хүйс", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("isHInsurance", "Эрүүл мэндийн даатгалтай эсэх", typeof(ArrayList), "");
            
            ucCustomerList.FieldFindAdd("isSInsurance", "Нийгмийн даатгалтай эсэх", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("TypeCode", "Байгууллагын төрөл", typeof(int), "");
            ucCustomerList.FieldFindAdd("CorporateName", "Байгууллагын нэр", typeof(string), "");
            ucCustomerList.FieldFindAdd("InduTypeCode", "Үйл ажиллагааны чиглэл", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("InduSubTypeCode", "Үйл ажиллагааны дэд чиглэл", typeof(ArrayList), "");
            
            ucCustomerList.FieldFindAdd("DirLastName", "Захирлын нэр", typeof(string), "");
            ucCustomerList.FieldFindAdd("RateCode", "Харилцагчийн зэрэглэлийн код", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("isOtherInsurance", "Тухайн байгуулагаас өөр байгууллагад даатгалтай эсэх", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("Email", "Mайл хаяг", typeof(string), "");
            ucCustomerList.FindItemAdd("Telephone", "Ажлын утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            
            ucCustomerList.FindItemAdd("Mobile", "Гар утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucCustomerList.FindItemAdd("HomePhone", "Гэрийн утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucCustomerList.FindItemAdd("Fax", "Факсын дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            
            ucCustomerList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucCustomerList.FieldFindRefresh();
            InitDictionary();
            ucCustomerList.VisibleFind = true;
        }
        private void Init1(string pClassCode)
        {
            ucCustomerList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucCustomerList_EventFindPaging);
            ucCustomerList.EventSelected += new ISM.Template.ucGridPanel.delegateEventSelected(ucCustomerList_EventSelected);
            ucCustomerList.EventDataChanged += new ISM.Template.ucGridPanel.delegateEventDataChanged(ucCustomerList_EventDataChanged);
            //ucCustomerList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
            // ucCustomerList.gridView1.Appearance.OddRow.Options.UseBackColor = false;


            ucCustomerList.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);

            labelControl1.BackColor = Color.NavajoWhite;
            labelControl2.BackColor = Color.SkyBlue;

            labelControl1.ForeColor = Color.NavajoWhite;
            labelControl2.ForeColor = Color.SkyBlue;

            Function();
            ucCustomerList.FindItemAdd("CustomerNo", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucCustomerList.FieldFindAdd("ClassCode", "Харилцагчийн төрөл", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("BranchNo", "Харилцагчийн салбар", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("Status", "Харилцагчийн төлөв", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("FirstName", "Эцэг эхийн нэр", typeof(string), "");

            ucCustomerList.FieldFindAdd("LastName", "Харилцагчийн нэр", typeof(string), "");
            ucCustomerList.FieldFindAdd("RegisterNo", "Регистерийн дугаар", typeof(string), "");
            ucCustomerList.FieldFindAdd("PassNo", "Иргэний үнэмлэхний дугаар", typeof(string), "");
            ucCustomerList.FieldFindAdd("Sex", "Хүйс", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("isHInsurance", "Эрүүл мэндийн даатгалтай эсэх", typeof(ArrayList), "");

            ucCustomerList.FieldFindAdd("isSInsurance", "Нийгмийн даатгалтай эсэх", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("TypeCode", "Байгууллагын төрөл", typeof(int), "");
            ucCustomerList.FieldFindAdd("CorporateName", "Байгууллагын нэр", typeof(string), "");
            ucCustomerList.FieldFindAdd("InduTypeCode", "Үйл ажиллагааны чиглэл", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("InduSubTypeCode", "Үйл ажиллагааны дэд чиглэл", typeof(ArrayList), "");

            ucCustomerList.FieldFindAdd("DirLastName", "Захирлын нэр", typeof(string), "");
            ucCustomerList.FieldFindAdd("RateCode", "Харилцагчийн зэрэглэлийн код", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("isOtherInsurance", "Тухайн байгуулагаас өөр байгууллагад даатгалтай эсэх", typeof(ArrayList), "");
            ucCustomerList.FieldFindAdd("Email", "Mайл хаяг", typeof(string), "");
            ucCustomerList.FindItemAdd("Telephone", "Ажлын утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");

            ucCustomerList.FindItemAdd("Mobile", "Гар утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucCustomerList.FindItemAdd("HomePhone", "Гэрийн утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucCustomerList.FindItemAdd("Fax", "Факсын дугаар", "", DynamicParameterType.Decimal, false, "d", "");

            ucCustomerList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucCustomerList.FieldFindRefresh();
            InitDictionary();
            ucCustomerList.VisibleFind = true;
            if (Static.ToStr(pClassCode) != "")
            {
                ucCustomerList.FindItemSetValue("ClassCode", pClassCode);
            }
        }
        void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

                string Status = View.GetRowCellDisplayText(e.RowHandle, View.Columns[49]);
                if (Status == "0")
                {


                    ucCustomerList.gridView1.Appearance.OddRow.BackColor = Color.NavajoWhite;
                    ucCustomerList.gridView1.Appearance.OddRow.BackColor2 = Color.NavajoWhite;
                    ucCustomerList.gridView1.Appearance.EvenRow.BackColor = Color.NavajoWhite;
                    ucCustomerList.gridView1.Appearance.EvenRow.BackColor2 = Color.NavajoWhite;
                    e.Appearance.BackColor = Color.NavajoWhite;
                    e.Appearance.BackColor2 = Color.NavajoWhite;


                }
                if (Status == "1")
                {


                    ucCustomerList.gridView1.Appearance.OddRow.BackColor = Color.SkyBlue;
                    ucCustomerList.gridView1.Appearance.OddRow.BackColor2 = Color.SkyBlue;
                    ucCustomerList.gridView1.Appearance.EvenRow.BackColor = Color.SkyBlue;
                    ucCustomerList.gridView1.Appearance.EvenRow.BackColor2 = Color.SkyBlue;
                    e.Appearance.BackColor = Color.SkyBlue;
                    e.Appearance.BackColor2 = Color.SkyBlue;


                }
            }
        }
        private Result InitDictionary()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";
            DictUtility.PrivNo = PrivNo;
            try
            {
                string[] names = new string[] { "BRANCH", "INDUSTRY", "SUBINDUSTRY", "CUSTRATE" };
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
                    ucCustomerList.FindItemSetList("BranchNo", DT, "BRANCH", "NAME", "", new int[] { 2, 3, 4 });
                }

                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д үйл ажиллагааны чиглэлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucCustomerList.FindItemSetList("InduTypeCode", DT, "TYPECODE", "NAME");
                }

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д дэд үйл ажиллагааны чиглэлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucCustomerList.FindItemSetList("InduSubTypeCode", DT, "SUBTYPECODE", "NAME");
                }

                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "Dictionary-д харилцагчийн зэрэглэлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucCustomerList.FindItemSetList("RateCode", DT, "RATECODE", "NAME");
                }

                ucCustomerList.FindItemSetList("ClassCode", 0, "ИРГЭН");
                ucCustomerList.FindItemSetList("ClassCode", 1, "БАЙГУУЛЛАГА");

                ucCustomerList.FindItemSetList("Status", 0, "Гэрээ хийгээгүй");
                ucCustomerList.FindItemSetList("Status", 1, "Гэрээ хийсэн");

                ucCustomerList.FindItemSetList("Sex", 0, "ЭР");
                ucCustomerList.FindItemSetList("Sex", 1, "ЭМ");

                ucCustomerList.FindItemSetList("isOtherInsurance", 0, "ҮГҮЙ");
                ucCustomerList.FindItemSetList("isOtherInsurance", 1, "ТИЙМ");

                ucCustomerList.FindItemSetList("isHInsurance", 0, "ҮГҮЙ");
                ucCustomerList.FindItemSetList("isHInsurance", 1, "ТИЙМ");

                ucCustomerList.FindItemSetList("isSInsurance", 0, "ҮГҮЙ");
                ucCustomerList.FindItemSetList("isSInsurance", 1, "ТИЙМ");

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
        #endregion
        #region [ Events ]
        void ucCustomerList_EventDataChanged()
        {

            /*CustomerNo,ClassCode,decode(ClassCode, 0, 'ХУВЬ ХҮН', 1, 'БАЙГУУЛЛАГА') ClassCodeName,TypeCode,InduTypeCode,InduSubTypeCode,FirstName,LastName,MiddleName,CorporateName,CorporateName2,
RegisterNo,PassNo,DriverNo,Sex, decode(Sex, 0, 'ЭР', 1, 'ЭМ') SexName,BirthDay,Company,Position,Experience,DirFirstName,DirLastName,
DirMiddleName,DirRegisterNo,DirPassNo,DirSex, decode(DirSex, 0, 'ЭР', 1, 'ЭМ') DirSexName,DirBirthDay,Email,Telephone,Mobile,HomePhone,Fax,
WebSite,SpecialApproval,RateCode,CountryCode,LanguageCode,isOtherInsurance, decode(isOtherInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isOtherInsuranceName,
isHInsurance, decode(isHInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isHInsuranceName, isSInsurance, decode(isSInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isSInsuranceName,BranchNo,Status,
decode(Status, 0, 'ИДЭВХГҮЙ', 1, 'ИДЭВХТЭЙ') StatusName*/
            //ucCustomerList.gridView1.OptionsView.ColumnAutoWidth = false;
            //ucCustomerList.gridView1.BestFitColumns();
           
            ucCustomerList.FieldLinkSetColumnCaption(0, "Харилцагчийн дугаар");
          //ucCustomerList.gridView1.Columns[0].Width = 100;

            ucCustomerList.FieldLinkSetColumnCaption(1, "Харилцагчийн төрөл", true);
            //ucCustomerList.gridView1.Columns[1].Width = 100;
            ucCustomerList.FieldLinkSetColumnCaption(2, "Харилцагчийн төрөл");
          //  ucCustomerList.gridView1.Columns[2].Width = 100;

            ucCustomerList.FieldLinkSetColumnCaption(3, "Байгууллагын төрөл");
        //    ucCustomerList.gridView1.Columns[3].Width = 100;
            ucCustomerList.FieldLinkSetColumnCaption(4, "Үйл ажиллагааны чиглэл");
            ucCustomerList.gridView1.Columns[4].Visible = false;
            ucCustomerList.FieldLinkSetColumnCaption(5, "Үйл ажиллагааны чиглэл");
   

            ucCustomerList.FieldLinkSetColumnCaption(6, "Үйл ажиллагааны дэд чиглэл");
            ucCustomerList.gridView1.Columns[6].Visible = false;
            ucCustomerList.FieldLinkSetColumnCaption(7, "Үйл ажиллагааны дэд чиглэл");
            ucCustomerList.FieldLinkSetColumnCaption(8, "Эцэг эхийн нэр");
            //ucCustomerList.gridView1.Columns[6].Width = 100;

            ucCustomerList.FieldLinkSetColumnCaption(9, "Харилцагчийн нэр");
            ///ucCustomerList.gridView1.Columns[7].Width = 100;
            ucCustomerList.FieldLinkSetColumnCaption(10, "Овог");

            ucCustomerList.FieldLinkSetColumnCaption(11, "Байгууллагын нэр");
           //ucCustomerList.gridView1.Columns[9].Width = 100;
            ucCustomerList.FieldLinkSetColumnCaption(12, "Байгууллагын нэр 2");
         //  ucCustomerList.gridView1.Columns[10].Width = 100;

            ucCustomerList.FieldLinkSetColumnCaption(13, "Регистерийн дугаар");
            ucCustomerList.FieldLinkSetColumnCaption(14, "Иргэний үнэмлэхний дугаар");
            ucCustomerList.FieldLinkSetColumnCaption(15, "Жолооны үнэмлэхний дугаар");

            ucCustomerList.FieldLinkSetColumnCaption(16, "Хүйс", true);
            ucCustomerList.FieldLinkSetColumnCaption(17, "Хүйс");
            ucCustomerList.FieldLinkSetColumnCaption(18, "Төрсөн огноо");

            ucCustomerList.FieldLinkSetColumnCaption(19, "Ажиллаж буй албан байгууллага");
     //     ucCustomerList.gridView1.Columns[17].Width = 100;
            ucCustomerList.FieldLinkSetColumnCaption(20, "Албан тушаал");

            ucCustomerList.FieldLinkSetColumnCaption(21, "Туршлага");
            ucCustomerList.FieldLinkSetColumnCaption(22, "Захиралын эцэг эхийн нэр");
        //    ucCustomerList.gridView1.Columns[20].Width = 100;

            ucCustomerList.FieldLinkSetColumnCaption(23, "Захиралын нэр");
     //       ucCustomerList.gridView1.Columns[21].Width = 90;
            ucCustomerList.FieldLinkSetColumnCaption(24, "Захиралын овог");

            ucCustomerList.FieldLinkSetColumnCaption(25, "Захиралын регистерийн дугаар");
            ucCustomerList.FieldLinkSetColumnCaption(26, "Захиралын иргэний үнэмлэхний дугаар");

            ucCustomerList.FieldLinkSetColumnCaption(27, "Захиралын хүйс", true);
            ucCustomerList.FieldLinkSetColumnCaption(28, "Захиралын хүйс");
            ucCustomerList.FieldLinkSetColumnCaption(29, "Захиралын төрсөн огноо");

            ucCustomerList.FieldLinkSetColumnCaption(30, "Майл хаяг");
     //   ucCustomerList.gridView1.Columns[28].Width = 150;
            ucCustomerList.FieldLinkSetColumnCaption(31, "Ажлын утасны дугаар");

            ucCustomerList.FieldLinkSetColumnCaption(32, "Гар утасны дугаар");
            ucCustomerList.FieldLinkSetColumnCaption(33, "Гэрийн утас");

            ucCustomerList.FieldLinkSetColumnCaption(34, "Факсын дугаар");
            ucCustomerList.FieldLinkSetColumnCaption(35, "Вэб хуудасны хаяг");

            ucCustomerList.FieldLinkSetColumnCaption(36, "Харилцагч нь аль салбарын үйл ажиллагаа хийдэг");
            ucCustomerList.FieldLinkSetColumnCaption(37, "Харилцагчийн зэрэглэлийн код");

            ucCustomerList.FieldLinkSetColumnCaption(38, "Харилцагчийн улсын код");
            ucCustomerList.FieldLinkSetColumnCaption(39, "Харилцагчийн улс");
            ucCustomerList.FieldLinkSetColumnCaption(40, "Харилцагчийн хэлний код");
            ucCustomerList.FieldLinkSetColumnCaption(41, "Харилцагчийн хэл");

            ucCustomerList.FieldLinkSetColumnCaption(42, "Тухайн байгууллагаас өөр байгууллагад даатгалтай эсэх", true);
            ucCustomerList.FieldLinkSetColumnCaption(43, "Тухайн байгууллагаас өөр байгууллагад даатгалтай эсэх");
            ucCustomerList.FieldLinkSetColumnCaption(44, "Эрүүл мэндийн даатгалтай эсэх", true);
            ucCustomerList.FieldLinkSetColumnCaption(45, "Эрүүл мэндийн даатгалтай эсэх");

            ucCustomerList.FieldLinkSetColumnCaption(46, "Нийгмийн даатгалтай эсэх", true);
            ucCustomerList.FieldLinkSetColumnCaption(47, "Нийгмийн даатгалтай эсэх");
            ucCustomerList.FieldLinkSetColumnCaption(48, "Салбарын дугаар");

            ucCustomerList.FieldLinkSetColumnCaption(49, "Харилцагчийн төлөв", true);
            ucCustomerList.FieldLinkSetColumnCaption(50, "Харилцагчийн төлөв");
            ucCustomerList.FieldLinkSetColumnCaption(51, "Үүсгэсэн огноо");
            ucCustomerList.FieldLinkSetColumnCaption(52, "Үүсгэсэн хэрэглэгч");
            ucCustomerList.FieldLinkSetColumnCaption(53, "Хуучин дугаар");
            ucCustomerList.FieldLinkSetColumnCaption(54, "Өндөр");
            ucCustomerList.FieldLinkSetColumnCaption(55, "Гутлын хэмжээ");
            FormUtility.RestoreStateGrid(appname, formname, ref ucCustomerList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucCustomerList.ucParameterPanel1.vGridControl1, ref ucCustomerList.groupControl1);
         }
        void ucCustomerList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucCustomerList.Browsable)
                {
                    object[] obj = new object[4];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["CustomerNo"]);
                    obj[2] = Static.ToLong(selectedrow["ClassCode"]);
                    obj[3] = Static.ToInt(selectedrow["TypeCode"]);
                    EServ.Shared.Static.Invoke("InfoPos.Customer.dll", "InfoPos.Customer.Main", "CallCustomerProp", obj);
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
        void LoadData(object[] values)
        {
            Result res = new Result();

            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 120000, 120000, values);

                if (res.ResultNo == 0)
                {
                    ucCustomerList.DataSource = res.Data.Tables[0];
                  //  FormUtility.RestoreStateGrid(appname, formname, ref ucCustomerList.gridView1);
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ucCustomerList.DataSource != null)
            {
                DataRow DR = ucCustomerList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucCustomerList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    object[] obj = new object[2];
                    obj[0] = DR;
                    InfoPos.Enquiry.CustomerEnquiry CusEnq = new InfoPos.Enquiry.CustomerEnquiry(_core, CustomerID, obj);
                    CusEnq.ShowDialog();
                }
            }
        }
        void Function() 
        {
            if (_core.Resource != null)
            {
                simpleButton1.Image = _core.Resource.GetImage("navigate_refresh");
                simpleButton2.Image = _core.Resource.GetImage("menu_customer");
            }
        }
        #endregion
        private void CustomerList_Load(object sender, EventArgs e)
        {
            
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucCustomerList.OnEventFindPaging(0);
            Function();

        }
        private void CustomerList_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Escape)
           {
               this.Close();
           }
           if (e.KeyCode == Keys.Enter)
           {

               ucCustomerList.OnEventFindPaging(0);

           }

            
        }
        private void ucCustomerList_Load(object sender, EventArgs e)
        {

        }
        private void CustomerList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref ucCustomerList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucCustomerList.ucParameterPanel1.vGridControl1, ref ucCustomerList.groupControl1);
        }
        private void ucCustomerList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 120000, 120000, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucCustomerList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (ucCustomerList.DataSource != null)
            {
                DataRow DR = ucCustomerList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucCustomerList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    CustomerID = Static.ToLong(DR["CustomerNo"]);
                    InfoPos.Issue.FormMain iss = new InfoPos.Issue.FormMain(_core, CustomerID, DR);
                    iss.ShowDialog();
                }
            }
        }
    }
}
