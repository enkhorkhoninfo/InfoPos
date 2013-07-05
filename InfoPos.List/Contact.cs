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
    public partial class ContactList : Form
    {
        #region [ Variables ]
        private Core.Core _core;
        long CustomerID;
        int PrivNo = 310125;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Init ]
        public ContactList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucContactList.Resource = _core.Resource;
    //       CustomerList.ActiveForm.AcceptButton = ucCustomerList.btnFind;
        }
        private void Init()
        {
            ucContactList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucContactList_EventFindPaging);
            ucContactList.EventSelected += new ISM.Template.ucGridPanel.delegateEventSelected(ucContactList_EventSelected);
            ucContactList.EventDataChanged += new ISM.Template.ucGridPanel.delegateEventDataChanged(ucContactList_EventDataChanged);
            //ucCustomerList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
           // ucCustomerList.gridView1.Appearance.OddRow.Options.UseBackColor = false;
           
      
          
         
            Function();
            /*"CustomerNo like","ClassCode","BranchNo","Status","FirstName like",
                    "LastName like","RegisterNo like","PassNo like","Sex like","TypeCode","CorporateName like","Email like","Telephone like",
                    "Mobile like","HomePhone like","Fax like","DriverNo like"*/
            ucContactList.FindItemAdd("CustomerNo", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucContactList.FieldFindAdd("ClassCode", "Харилцагчийн төрөл", typeof(ArrayList), "");
            ucContactList.FieldFindAdd("BranchNo", "Харилцагчийн салбар", typeof(ArrayList), "");
            ucContactList.FieldFindAdd("Status", "Харилцагчийн төлөв", typeof(ArrayList), "");
            ucContactList.FieldFindAdd("FirstName", "Эцэг эхийн нэр", typeof(string), "");
            
            ucContactList.FieldFindAdd("LastName", "Харилцагчийн нэр", typeof(string), "");
            ucContactList.FieldFindAdd("RegisterNo", "Регистерийн дугаар", typeof(string), "");
            ucContactList.FieldFindAdd("PassNo", "Иргэний үнэмлэхний дугаар", typeof(string), "");
            ucContactList.FieldFindAdd("Sex", "Хүйс", typeof(ArrayList), "");
           
            ucContactList.FieldFindAdd("TypeCode", "Байгууллагын төрөл", typeof(int), "");
            ucContactList.FieldFindAdd("CorporateName", "Байгууллагын нэр", typeof(string), "");
           
            ucContactList.FieldFindAdd("Email", "Mайл хаяг", typeof(string), "");
            ucContactList.FindItemAdd("Telephone", "Ажлын утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            
            ucContactList.FindItemAdd("Mobile", "Гар утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucContactList.FindItemAdd("HomePhone", "Гэрийн утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucContactList.FindItemAdd("Fax", "Факсын дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucContactList.FindItemAdd("DriverNo", "Жолооны үнэмлэхний дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            
            ucContactList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucContactList.FieldFindRefresh();
            InitDictionary();
            ucContactList.VisibleFind = true;
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
                string[] names = new string[] { "BRANCH", "INDUSTRY", "SUBINDUSTRY" };
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
                    ucContactList.FindItemSetList("BranchNo", DT, "BRANCH", "NAME", "", new int[] { 2, 3, 4 });
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
                    ucContactList.FindItemSetList("InduTypeCode", DT, "TYPECODE", "NAME");
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
                    ucContactList.FindItemSetList("InduSubTypeCode", DT, "SUBTYPECODE", "NAME");
                }

          

                ucContactList.FindItemSetList("ClassCode", 0, "ИРГЭН");
                ucContactList.FindItemSetList("ClassCode", 1, "БАЙГУУЛЛАГА");

                ucContactList.FindItemSetList("Status", 0, "Идэвхгүй");
                ucContactList.FindItemSetList("Status", 1, "Идэвхтэй");

                ucContactList.FindItemSetList("Sex", 0, "ЭР");
                ucContactList.FindItemSetList("Sex", 1, "ЭМ");


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
        void ucContactList_EventDataChanged()
        {

            /*CustomerNo,ClassCode,decode(ClassCode, 0, 'ХУВЬ ХҮН', 1, 'БАЙГУУЛЛАГА') ClassCodeName,TypeCode,InduTypeCode,InduSubTypeCode,FirstName,LastName,MiddleName,CorporateName,CorporateName2,
RegisterNo,PassNo,DriverNo,Sex, decode(Sex, 0, 'ЭР', 1, 'ЭМ') SexName,BirthDay,Company,Position,Experience,DirFirstName,DirLastName,
DirMiddleName,DirRegisterNo,DirPassNo,DirSex, decode(DirSex, 0, 'ЭР', 1, 'ЭМ') DirSexName,DirBirthDay,Email,Telephone,Mobile,HomePhone,Fax,
WebSite,SpecialApproval,RateCode,CountryCode,LanguageCode,isOtherInsurance, decode(isOtherInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isOtherInsuranceName,
isHInsurance, decode(isHInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isHInsuranceName, isSInsurance, decode(isSInsurance, 0, 'ҮГҮЙ', 1, 'ТИЙМ') isSInsuranceName,BranchNo,Status,
decode(Status, 0, 'ИДЭВХГҮЙ', 1, 'ИДЭВХТЭЙ') StatusName*/
            //ucCustomerList.gridView1.OptionsView.ColumnAutoWidth = false;
            //ucCustomerList.gridView1.BestFitColumns();
           
            ucContactList.FieldLinkSetColumnCaption(0, "Харилцагчийн дугаар");
          //ucCustomerList.gridView1.Columns[0].Width = 100;

            ucContactList.FieldLinkSetColumnCaption(1, "Харилцагчийн төрөл", true);
            //ucCustomerList.gridView1.Columns[1].Width = 100;
            ucContactList.FieldLinkSetColumnCaption(2, "Харилцагчийн төрөл");
          //  ucCustomerList.gridView1.Columns[2].Width = 100;

            ucContactList.FieldLinkSetColumnCaption(3, "Байгууллагын төрөл");

            ucContactList.FieldLinkSetColumnCaption(4, "Үйл ажиллагааны чиглэл");
   


            ucContactList.FieldLinkSetColumnCaption(5, "Эцэг эхийн нэр");
            //ucCustomerList.gridView1.Columns[6].Width = 100;

            ucContactList.FieldLinkSetColumnCaption(6, "Харилцагчийн нэр");
            ///ucCustomerList.gridView1.Columns[7].Width = 100;
            ucContactList.FieldLinkSetColumnCaption(7, "Овог");

            ucContactList.FieldLinkSetColumnCaption(8, "Байгууллагын нэр");

         //  ucCustomerList.gridView1.Columns[10].Width = 100;

            ucContactList.FieldLinkSetColumnCaption(9, "Регистерийн дугаар");
            ucContactList.FieldLinkSetColumnCaption(10, "Иргэний үнэмлэхний дугаар");
          // 

            ucContactList.FieldLinkSetColumnCaption(11, "Хүйс", true);
            ucContactList.FieldLinkSetColumnCaption(12, "Хүйс");
            ucContactList.FieldLinkSetColumnCaption(13, "Төрсөн огноо");

            ucContactList.FieldLinkSetColumnCaption(14, "Ажиллаж буй албан байгууллага");
     //     ucCustomerList.gridView1.Columns[17].Width = 100;
            ucContactList.FieldLinkSetColumnCaption(15, "Албан тушаал");

            ucContactList.FieldLinkSetColumnCaption(16, "Туршлага");
 
          

            ucContactList.FieldLinkSetColumnCaption(17, "Майл хаяг");
     //   ucCustomerList.gridView1.Columns[28].Width = 150;
            ucContactList.FieldLinkSetColumnCaption(18, "Ажлын утасны дугаар");

            ucContactList.FieldLinkSetColumnCaption(19, "Гар утасны дугаар");
            ucContactList.FieldLinkSetColumnCaption(20, "Гэрийн утас");

            ucContactList.FieldLinkSetColumnCaption(21, "Факсын дугаар");
            ucContactList.FieldLinkSetColumnCaption(22, "Вэб хуудасны хаяг");

            ucContactList.FieldLinkSetColumnCaption(23, "Салбарын дугаар");

            ucContactList.FieldLinkSetColumnCaption(24, "Харилцагчийн төлөв", true);
            ucContactList.FieldLinkSetColumnCaption(25, "Харилцагчийн төлөв");
            ucContactList.FieldLinkSetColumnCaption(26, "Жолооны үнэмлэхний дугаар");
            ucContactList.FieldLinkSetColumnCaption(27, "Үүсгэсэн огноо");
            ucContactList.FieldLinkSetColumnCaption(28, "Үүсгэсэн хэрэглэгч");
     
            FormUtility.RestoreStateGrid(appname, formname, ref ucContactList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucContactList.ucParameterPanel1.vGridControl1, ref ucContactList.groupControl1);
         }
        void ucContactList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucContactList.Browsable)
                {
                    object[] obj = new object[4];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["CustomerNo"]);
                    obj[2] = Static.ToLong(selectedrow["ClassCode"]);
                    obj[3] = Static.ToInt(selectedrow["TypeCode"]);
                    EServ.Shared.Static.Invoke("InfoPos.Customer.dll", "InfoPos.Customer.Main", "CallCustomerInfo", obj);
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ucContactList.DataSource != null)
            {
                DataRow DR = ucContactList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucContactList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    object[] obj = new object[2];
                    obj[0] = DR;
                    InfoPos.Enquiry.CustContactEnquiry CusEnq = new InfoPos.Enquiry.CustContactEnquiry(_core, CustomerID, obj);
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

        private void ContactList_Load(object sender, EventArgs e)
        {
            
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucContactList.OnEventFindPaging(0);
            Function();

        }
        private void ContactList_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Escape)
           {
               this.Close();
           }
           if (e.KeyCode == Keys.Enter)
           {

               ucContactList.OnEventFindPaging(0);

           }

            
        }
        private void ucContactList_Load(object sender, EventArgs e)
        {

        }
        private void ContactList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref ucContactList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucContactList.ucParameterPanel1.vGridControl1, ref ucContactList.groupControl1);
        }
        private void ucContactList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 310125, 310125, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucContactList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (ucContactList.DataSource != null)
            {
                DataRow DR = ucContactList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucContactList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    CustomerID = Static.ToLong(DR["CustomerNo"]);
                    InfoPos.Issue.FormMain iss = new InfoPos.Issue.FormMain(_core, CustomerID, DR);
                    iss.ShowDialog();
                }
            }
        }
        private void ucContactList_Load_1(object sender, EventArgs e)
        {

        }
    }
}
