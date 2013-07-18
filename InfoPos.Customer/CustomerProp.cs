using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Customer
{
    public partial class CustomerProp : Form
    {
        #region [ Variables ]
        InfoPos.Core.Core _core;
        string EventType;
        long _customerid;
        long _customerno;
        bool loadAddress = true, loadPicture = true, loadFamily = true, loadDirector = true, 
        loadUV = true, loadIT = true, loadContact = true, loadAccount = true, loadAcnt = false;
        int TablePrivSelect = 120006;
        int TablePrivFileSelect = 120048;
        int TablePrivUpdate = 120009;
        string TableNamePrefix = "CUSTOMER";
        System.Drawing.Image GetImage;
        bool loadAttach = false;
        bool loadNote;
        bool loadAdd = false;
        long AttachID = 0;
        long AttachIDFile = 0;
        int SaveCheck;
        int EditValueClassCode;
        int _CLASSCODE;
        int PrivNo = 120000;
        string FilterSubIndustry = "";
        string FileName = "";
        DataTable DTNote, DTFamily;
        DataSet DS;
        string appname = "", formname = "";
        Form FormName = null;
        object OldValue;
        #endregion
        #region[ Variable hide caption CaptionValue ]

        int[] C1Hide = { 0, 1, 6, 5 };
        int[] C1Caption = { 2, 3, 4, 7, 8 };
        string[] C1CaptionValue = { "Хот", "Аймаг", "Сум", "Байр", "Нэмсэн огноо" };

        int[] C2Hide = { 0, 1 };
        int[] C2Caption = { 2, 3 };
        string[] C2CaptionValue = { "Зурагийн төрөл", "Хавсралт файлын ID" };

        int[] C3Hide = { 0, 1, 2, 5, 6, 7, 8, 9, 10 };
        int[] C3Caption = { 3, 4 };
        string[] C3CaptionValue = { "Эцэг эхийн нэр", "Харилцагчийн нэр" };

        int[] C4Hide = { 0, 1, 4, 5, 6, 7, 8, 9 };
        int[] C4Caption = { 2, 3 };
        string[] C4CaptionValue = { "Эцэг эхийн нэр", "Харилцагчийн нэр" };

        int[] C5Hide = { 0, 1, 4, 5, 6, 7, 8, 9, 10 };
        int[] C5Caption = { 2, 3 };
        string[] C5CaptionValue = { "Эцэг эхийн нэр", "Харилцагчийн нэр" };

        int[] C6Hide = { 0, 1, 4, 5, 6 };
        int[] C6Caption = { 2, 3 };
        string[] C6CaptionValue = { "Огноо", "Огноо цаг минут" };

        int[] C7Hide = { 0, 1 };
        int[] C7Caption = { 2, 3, 4 };
        string[] C7CaptionValue = { "Банкны дугаар", "Валют", "Дансны дугаар" };

        int[] C8Hide = { 0, 1 };
        int[] C8Caption = { 2, 3 };
        string[] C8CaptionValue = { "Хавсралтын ID", "Файлын нэр" };

        int[] C9Hide = { 0, 1 };
        int[] C9Caption = { 2, 3, 4, 5 };
        string[] C9CaptionValue = { "Гүйлгээ хийсэн огноо", "Гүйлгээ хийсэн цаг, огноо", "Хэрэглэгчийн дугаар", "Товч дүгнэлт" };

        #endregion
        #region [ Load ]
        public CustomerProp(InfoPos.Core.Core core)
            : this(core, 0, 0)
        {

        }
        public CustomerProp(InfoPos.Core.Core core, long customerid, int classcode)
        {
            InitializeComponent();
            _core = core;
            _customerid = customerid;
            _CLASSCODE = classcode;
            Init();
            ucCustGeneral.FieldLinkSetSaveState();
        }
        private void CustomerProp_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwNote);
            #region Selecting All Tab Pages before for Lookupedits

            for (int i = 0; i < xtraTabGeneral.TabPages.Count; i++)
            {
                xtraTabGeneral.SelectedTabPageIndex = i;
            }
            xtraTabGeneral.SelectedTabPageIndex = 0;

            #endregion
            DS = DataSetComboAll();
            InitCombos();
            this.Show();
            if (_customerid == 0)
            {
                ucCustGeneral.FieldLinkSetNewState();
            }
            else
            {
                if (_CLASSCODE == 0)
                    IndividualTrue();
                if (_CLASSCODE == 1)
                    CorporateTrue();
                GeneralRefreshData(_customerid);
                //cboTypeCode.Enabled = false;
            }

            if (_customerid == 0)
            {
                cboClassCode.ItemIndex = 0;
            }
            LoadFunction();
            #region[форматжуулалт]
            appname = _core.ApplicationName;
            formname = "CustomerInfo." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            #endregion
        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            InitEvents();
            InitToggles();
            InitData();

        }
        //Энд нэмнэ [Event Save,Delete гэх мэт]
        private void InitEvents()
        {
            ucCustGeneral.EventSave += new ucTogglePanel.delegateEventSave(ucCustGeneral_EventSave);
            ucCustGeneral.EventDelete += new ucTogglePanel.delegateEventDelete(ucCustGeneral_EventDelete);
            ucCustGeneral.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucCustGeneral_EventDataChanged);
            ucCustGeneral.EventEdit += new ucTogglePanel.delegateEventEdit(ucCustGeneral_EventEdit);
            ucCustGeneral.EventExit += new ucTogglePanel.delegateEventExit(ucCustGeneral_EventExit);
            ucCustGeneral.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucCustGeneral_EventAddAfter);

            ucAddress.EventSave += new ucTogglePanel.delegateEventSave(ucAddress_EventSave);
            ucAddress.EventDelete += new ucTogglePanel.delegateEventDelete(ucAddress_EventDelete);
            ucAddress.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucAddress_EventDataChanged);
            ucAddress.EventEdit += new ucTogglePanel.delegateEventEdit(ucAddress_EventEdit);

            ucPicture.EventSave += new ucTogglePanel.delegateEventSave(ucPicture_EventSave);
            ucPicture.EventDelete += new ucTogglePanel.delegateEventDelete(ucPicture_EventDelete);
            ucPicture.EventAdd += new ucTogglePanel.delegateEventAdd(ucPicture_EventAdd);
            ucPicture.EventEdit += new ucTogglePanel.delegateEventEdit(ucPicture_EventEdit);
            ucPicture.EventReject += new ucTogglePanel.delegateEventReject(ucPicture_EventReject);
            ucPicture.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucPicture_EventDataChanged);

            ucFamily.EventSave += new ucTogglePanel.delegateEventSave(ucFamily_EventSave);
            ucFamily.EventDelete += new ucTogglePanel.delegateEventDelete(ucFamily_EventDelete);
            ucFamily.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucFamily_EventDataChanged);
            ucFamily.EventEdit += new ucTogglePanel.delegateEventEdit(ucFamily_EventEdit);

            ucDirector.EventSave += new ucTogglePanel.delegateEventSave(ucDirector_EventSave);
            ucDirector.EventDelete += new ucTogglePanel.delegateEventDelete(ucDirector_EventDelete);
            ucDirector.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucDirector_EventDataChanged);
            ucDirector.EventEdit += new ucTogglePanel.delegateEventEdit(ucDirector_EventEdit);

            ucContact.EventSave += new ucTogglePanel.delegateEventSave(ucContact_EventSave);
            ucContact.EventDelete += new ucTogglePanel.delegateEventDelete(ucContact_EventDelete);
            ucContact.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucContact_EventDataChanged);
            ucContact.EventEdit += new ucTogglePanel.delegateEventEdit(ucContact_EventEdit);

            ucAccount.EventSave += new ucTogglePanel.delegateEventSave(ucAccount_EventSave);
            ucAccount.EventDelete += new ucTogglePanel.delegateEventDelete(ucAccount_EventDelete);
            ucAccount.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucAccount_EventDataChanged);
            ucAccount.EventEdit += new ucTogglePanel.delegateEventEdit(ucAccount_EventEdit);

            ucAcnt.EventSave += new ucTogglePanel.delegateEventSave(ucAcnt_EventSave);
            ucAcnt.EventDelete += new ucTogglePanel.delegateEventDelete(ucAcnt_EventDelete);
            ucAcnt.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucAcnt_EventDataChanged);
            ucAcnt.EventEdit += new ucTogglePanel.delegateEventEdit(ucAcnt_EventEdit);

            ucNote.EventAddAfter += new ISM.Template.ucTogglePanel.delegateEventAddAfter(ucNote_EventAddAfter);
            ucNote.EventSave += new ucTogglePanel.delegateEventSave(ucNote_EventSave);
            ucNote.EventDelete += new ucTogglePanel.delegateEventDelete(ucNote_EventDelete);
            ucNote.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucNote_EventDataChanged);
            ucNote.EventEdit += new ucTogglePanel.delegateEventEdit(ucNote_EventEdit);
        }
        void ucNote_EventAddAfter()
        {
            numTab10UserNo.EditValue = _core.RemoteObject.User.UserNo;
        }
        //Энд нэмнэ [Toggle Save,Delete гэх мэт]
        private void InitToggles()
        {
            try
            {
                #region [ Tab1 - Customer general ]
                ucCustGeneral.ToggleShowDelete = true;
                ucCustGeneral.ToggleShowEdit = true;
                ucCustGeneral.ToggleShowExit = true;
                ucCustGeneral.ToggleShowNew = true;
                ucCustGeneral.ToggleShowReject = true;
                ucCustGeneral.ToggleShowSave = true;

                ucCustGeneral.DataSource = null;
                #endregion
                #region [ Tab2 - Address ]
                ucAddress.ToggleShowDelete = true;
                ucAddress.ToggleShowEdit = true;
                ucAddress.ToggleShowExit = true;
                ucAddress.ToggleShowNew = true;
                ucAddress.ToggleShowReject = true;
                ucAddress.ToggleShowSave = true;

                ucAddress.DataSource = null;
                ucAddress.GridView = gvwAddress;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwAddress, false);

                loadAddress = false;
                #endregion
                #region [ Tab3 - Picture ]
                ucPicture.ToggleShowDelete = true;
                ucPicture.ToggleShowEdit = true;
                ucPicture.ToggleShowExit = true;
                ucPicture.ToggleShowNew = true;
                ucPicture.ToggleShowReject = true;
                ucPicture.ToggleShowSave = true;

                ucPicture.DataSource = null;
                ucPicture.GridView = gvwPicture;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwPicture, false);

                loadPicture = false;
                #endregion
                #region [ Tab4 - Family ]
                ucFamily.ToggleShowDelete = true;
                ucFamily.ToggleShowEdit = true;
                ucFamily.ToggleShowExit = true;
                ucFamily.ToggleShowNew = true;
                ucFamily.ToggleShowReject = true;
                ucFamily.ToggleShowSave = true;

                ucFamily.DataSource = null;
                ucFamily.GridView = gvwFamily;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwFamily, false);
                loadFamily = false;
                #endregion
                #region [ TABDIRECTOR - DIRECTOR ]
                ucDirector.ToggleShowDelete = true;
                ucDirector.ToggleShowEdit = true;
                ucDirector.ToggleShowExit = true;
                ucDirector.ToggleShowNew = true;
                ucDirector.ToggleShowReject = true;
                ucDirector.ToggleShowSave = true;

                ucDirector.DataSource = null;
                ucDirector.GridView = gvwDirector;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwDirector, false);
                loadDirector = false;
                #endregion
                #region [ Tab7 - Холбоо барьсан тэмдэглэл ]
                ucContact.ToggleShowDelete = true;
                ucContact.ToggleShowEdit = true;
                ucContact.ToggleShowExit = true;
                ucContact.ToggleShowNew = true;
                ucContact.ToggleShowReject = true;
                ucContact.ToggleShowSave = true;

                ucContact.DataSource = null;
                ucContact.GridView = gvwContact;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwContact, false);
                loadContact = false;
                #endregion
                #region [ Tab8 - Харилцагчийн дансны мэдээлэл ]
                ucAccount.ToggleShowDelete = true;
                ucAccount.ToggleShowEdit = true;
                ucAccount.ToggleShowExit = true;
                ucAccount.ToggleShowNew = true;
                ucAccount.ToggleShowReject = true;
                ucAccount.ToggleShowSave = true;

                ucAccount.DataSource = null;
                ucAccount.GridView = gvwAccount;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwAccount, false);
                loadAccount = false;
                #endregion
                #region [ Tab8 - Харилцагчийн дансны мэдээлэл ]
                ucAcnt.ToggleShowDelete = true;
                ucAcnt.ToggleShowEdit = true;
                ucAcnt.ToggleShowExit = true;
                ucAcnt.ToggleShowNew = true;
                ucAcnt.ToggleShowReject = true;
                ucAcnt.ToggleShowSave = true;

                ucAcnt.DataSource = null;
                ucAcnt.GridView = gvwAcnt;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwAcnt, false);
                loadAcnt = false;
                #endregion
                #region[ Tab10 - Товч тэмдэглэл]

                ucNote.ToggleShowDelete = true;
                ucNote.ToggleShowEdit = true;
                ucNote.ToggleShowExit = true;
                ucNote.ToggleShowNew = true;
                ucNote.ToggleShowReject = true;
                ucNote.ToggleShowSave = true;

                ucNote.DataSource = null;
                ucNote.GridView = gvwNote;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwNote, false);
                loadNote = false;

                #endregion
                #region[ Tab11-Add]
                ucAdd.DataSource = null;
                loadAdd = false;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Энд нэмнэ
        private void InitData()
        {
            try
            {
                #region [ Tab1 - Customer general ]

                if (_customerid == 0)
                {
                    ucCustGeneral.ToggleShowDelete = true;
                    ucCustGeneral.ToggleShowEdit = false;
                    ucCustGeneral.ToggleShowExit = false;
                    ucCustGeneral.ToggleShowNew = true;
                    ucCustGeneral.ToggleShowReject = true;
                    ucCustGeneral.ToggleShowSave = true;
                    #region[ FieldLinkAdd]

                    ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
                    ucCustGeneral.FieldLinkAdd("cboClassCode", 0, "ClassCode", "", true, false);
                    ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
                    ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
                    ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
                    ucCustGeneral.FieldLinkAdd("txtMiddleName", 0, "MiddleName", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtFirstName", 0, "FirstName", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtLastName", 0, "LastName", "", true, false);
                    ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", true, false);
                    ucCustGeneral.FieldLinkAdd("txtPassNo", 0, "PassNo", "", false, false);
                    ucCustGeneral.FieldLinkAdd("dteBirthDay", 0, "BirthDay", "", false, false);
                    ucCustGeneral.FieldLinkAdd("cboSex", 0, "Sex", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtMobile", 0, "Mobile", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtHomePhone", 0, "HomePhone", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtTelephone", 0, "Telephone", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtFax", 0, "Fax", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtEmail", 0, "Email", "", false, false);
                    ucCustGeneral.FieldLinkAdd("cboIndustry", 0, "InduTypeCode", "", true, false);
                    ucCustGeneral.FieldLinkAdd("cboSubIndustry", 0, "InduSubTypeCode", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtCompany", 0, "Company", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtPosition", 0, "Position", "", false, false);
                    ucCustGeneral.FieldLinkAdd("mmoExperience", 0, "Experience", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtDriverNo", 0, "DriverNo", "", false, false);
                    ucCustGeneral.FieldLinkAdd("cboLevelNo", 0, "LevelNo", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
                    ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
                    ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
                    ucCustGeneral.FieldLinkAdd("numHeight", 0, "Height", "", false, false);
                    ucCustGeneral.FieldLinkAdd("numFootSize", 0, "Foot", "", false, false);
                    ucCustGeneral.FieldLinkAdd("cboMemberType", 0, "MemberType", "", false, false);
                    ucCustGeneral.FieldLinkAdd("numContractNo", 0, "ContractNo", "", false, false, true);

                    ucCustGeneral.FieldLinkAdd("txtAccountNo", 0, "AccountNo", "", false, false, false);
                    ucCustGeneral.FieldLinkAdd("txtIncomeAccountNo", 0, "IncomeAccountNo", "", false, false, false);

                    #endregion
                }
                else
                {
                    ucCustGeneral.ToggleShowDelete = true;
                    ucCustGeneral.ToggleShowEdit = true;
                    ucCustGeneral.ToggleShowExit = false;
                    ucCustGeneral.ToggleShowNew = false;
                    ucCustGeneral.ToggleShowReject = true;
                    ucCustGeneral.ToggleShowSave = true;

                    if (_CLASSCODE == 0) FieldIndividualTrue();
                    if (_CLASSCODE == 1) FieldCorporateTrue();
                }
                #endregion
                #region [ Tab2 - Address ]

                ucAddress.ToggleShowDelete = true;
                ucAddress.ToggleShowEdit = true;
                ucAddress.ToggleShowExit = false;
                ucAddress.ToggleShowNew = true;
                ucAddress.ToggleShowReject = true;
                ucAddress.ToggleShowSave = true;

                ucAddress.FieldLinkAdd("txtAddrSeqNo", 0, "SeqNo", "", false, true, true);
                ucAddress.FieldLinkAdd("cboCity", 0, "CITYCODE", "", true, false);
                ucAddress.FieldLinkAdd("cboDist", 0, "DISTCODE", "", true, false);
                ucAddress.FieldLinkAdd("cboSubDist", 0, "SUBDISTCODE", "", true, false);
                ucAddress.FieldLinkAdd("txtApartment", 0, "Apartment", "", false, false);
                ucAddress.FieldLinkAdd("txtAdd", 0, "Note", "", false, false);
                ucAddress.FieldLinkAdd("chkAddrCurrent", 0, "AddrCurrent", "", false, false);

                #endregion
                #region [ Tab3 - Picture ]
                ucPicture.ToggleShowDelete = true;
                ucPicture.ToggleShowEdit = true;
                ucPicture.ToggleShowExit = false;
                ucPicture.ToggleShowNew = true;
                ucPicture.ToggleShowReject = true;
                ucPicture.ToggleShowSave = true;

                ucPicture.FieldLinkAdd("numSeqNo", 0, "SeqNo", "", false, true, true);
                ucPicture.FieldLinkAdd("cboPictureType", 0, "PictureType", "", true, false);
                ucPicture.FieldLinkAdd("numAttachID", 0, "AttachID", "", false, false, true);
                #endregion
                #region [ Tab4 - Family ]
                ucFamily.ToggleShowDelete = true;
                ucFamily.ToggleShowEdit = true;
                ucFamily.ToggleShowExit = false;
                ucFamily.ToggleShowNew = true;
                ucFamily.ToggleShowReject = true;
                ucFamily.ToggleShowSave = true;

                ucFamily.FieldLinkAdd("numTab4SeqNo", 0, "SeqNo", "", false, true, true);
                ucFamily.FieldLinkAdd("cboTab4FamilyType", 0, "FamilyType", "", true, false);
                ucFamily.FieldLinkAdd("txtTab4FirstName", 0, "FirstName", "", false, false);
                ucFamily.FieldLinkAdd("txtTab4LastName", 0, "LastName", "", true, false);
                ucFamily.FieldLinkAdd("txtTab4MiddleName", 0, "MiddleName", "", false, false);
                ucFamily.FieldLinkAdd("txtTab4RegisterNo", 0, "RegisterNo", "", false, false);
                ucFamily.FieldLinkAdd("txtTab4PassNo", 0, "PassNo", "", false, false);
                ucFamily.FieldLinkAdd("txtTab4Email", 0, "Email", "", false, false);
                ucFamily.FieldLinkAdd("numTab4Telephone", 0, "Telephone", "", false, false);
                ucFamily.FieldLinkAdd("numTab4Mobile", 0, "Mobile", "", false, false);
                ucFamily.FieldLinkAdd("numTab4FamilyCustNo", 0, "Custno", "", false, false);
                #endregion
                #region [ TabDirector - Director ]
                ucDirector.ToggleShowDelete = true;
                ucDirector.ToggleShowEdit = true;
                ucDirector.ToggleShowExit = false;
                ucDirector.ToggleShowNew = true;
                ucDirector.ToggleShowReject = true;
                ucDirector.ToggleShowSave = true;

                ucDirector.FieldLinkAdd("numTabDirSeqNo", 0, "SeqNo", "", false, true, true);
                ucDirector.FieldLinkAdd("txtTabDirPosition", 0, "Position", "", true, false);
                ucDirector.FieldLinkAdd("txtTabDirFirstName", 0, "FirstName", "", false, false);
                ucDirector.FieldLinkAdd("txtTabDirLastName", 0, "LastName", "", true, false);
                ucDirector.FieldLinkAdd("txtTabDirMiddleName", 0, "MiddleName", "", false, false);
                ucDirector.FieldLinkAdd("numTabDirRegisterNo", 0, "RegisterNo", "", true, false);
                ucDirector.FieldLinkAdd("numTabDirPassNo", 0, "PassNo", "", false, false);
                ucDirector.FieldLinkAdd("cboTabDirSex", 0, "Sex", "", false, false);
                ucDirector.FieldLinkAdd("dteTabDirBirthday", 0, "Birthday", "", false, false);
                #endregion
                #region [ Tab7 - Холбоо барих ]
                ucContact.ToggleShowDelete = true;
                ucContact.ToggleShowEdit = true;
                ucContact.ToggleShowExit = false;
                ucContact.ToggleShowNew = true;
                ucContact.ToggleShowReject = true;
                ucContact.ToggleShowSave = true;

                ucContact.FieldLinkAdd("numTab7SeqNo", 0, "SeqNo", "", false, true, true);
                ucContact.FieldLinkAdd("dteTab7ContactDate", 0, "ContactDate", "", true, false);
                ucContact.FieldLinkAdd("cboTab7ContactType", 0, "ContactType", "", true, false);
                ucContact.FieldLinkAdd("txtTab7BriefDesc", 0, "BriefDesc", "", true, false);
                ucContact.FieldLinkAdd("txtTab7Note", 0, "Note", "", true, false);
                ucContact.FieldLinkAdd("numUserNo", 0, "UserNo", "", true, false, true);
                #endregion
                #region [ Tab8 - Харилцагчийн дансны мэдээлэл ]

                ucAccount.ToggleShowDelete = true;
                ucAccount.ToggleShowEdit = true;
                ucAccount.ToggleShowExit = false;
                ucAccount.ToggleShowNew = true;
                ucAccount.ToggleShowReject = true;
                ucAccount.ToggleShowSave = true;

                ucAccount.FieldLinkAdd("numTab8SeqNo", 0, "SeqNo", "", false, true, true);
                ucAccount.FieldLinkAdd("cboTab8BankNo", 0, "BankNo", "", true, false);
                ucAccount.FieldLinkAdd("cboTab8CurCode", 0, "CurCode", "", true, false);
                ucAccount.FieldLinkAdd("txtTab8AccountNo", 0, "AccountNo", "", true, false);

                #endregion
                #region [ Харилцагчийн дотоодын дансны мэдээлэл ]

                ucAcnt.ToggleShowDelete = true;
                ucAcnt.ToggleShowEdit = true;
                ucAcnt.ToggleShowExit = false;
                ucAcnt.ToggleShowNew = true;
                ucAcnt.ToggleShowReject = true;
                ucAcnt.ToggleShowSave = true;

                ucAcnt.FieldLinkAdd("numAcntAccountNo", 0, "AccountNo", "", true, true);
                ucAcnt.FieldLinkAdd("txtAcntAccountName", 0, "AccountName", "", true, false);

                #endregion
                #region[ Tab10 - Товч тэмдэглэл]

                ucNote.ToggleShowDelete = true;
                ucNote.ToggleShowEdit = true;
                ucNote.ToggleShowExit = false;
                ucNote.ToggleShowNew = true;
                ucNote.ToggleShowReject = true;
                ucNote.ToggleShowSave = true;

                ucNote.FieldLinkAdd("numTab10SeqNo", 0, "SeqNo", "", false, true, true);
                ucNote.FieldLinkAdd("numTab10UserNo", 0, "UserNo", "", true, false, true);
                ucNote.FieldLinkAdd("mmeTab10Note", 0, "Note", "", true, false);

                #endregion
                #region [ Images ]
                if (_core.Resource != null)
                {
                    btnAddSave.Image = _core.Resource.GetImage("image_save");
                    btnAttach.Image = _core.Resource.GetImage("image_folder");

                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitCombos()
        {
            if (DS != null)
            {
                #region[ Үндсэн мэдээлэл ]

                SetCombo("BRANCH", "NAME", cboBranch, DS.Tables["BRANCH"], new int[] { 2, 3, 4 });

                SetCombo("MASKID", "MASKNAME", cboRegisterMask, DS.Tables["REGISTERMASK"], "CUSTTYPE=0", new int[] { 2, 3, 4 });
                SetCombo("MASKID", "MASKNAME", cboPassMask, DS.Tables["PASSMASK"], "CUSTTYPE=0", new int[] { 2, 3, 4 });
                SetCombo("MASKID", "MASKNAME", cboDriverNoMask, DS.Tables["DRIVERMASK"], "CUSTTYPE=0", new int[] { 2, 3, 4 });


                FormUtility.LookUpEdit_SetList(ref cboClassCode, 0, "ИРГЭН");
                FormUtility.LookUpEdit_SetList(ref cboClassCode, 1, "БАЙГУУЛЛАГА");

                //FormUtility.LookUpEdit_SetList(ref cboMemberType, 0, "Гишүүн бус");
                //FormUtility.LookUpEdit_SetList(ref cboMemberType, 1, "Гишүүн");

                FormUtility.LookUpEdit_SetList(ref cboSex, 0, "Эр");
                FormUtility.LookUpEdit_SetList(ref cboSex, 1, "Эм");

                FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Идэвхгүй");
                FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Идэвхтэй");

                for (int i = 0; i < 10; i++)
                {
                    FormUtility.LookUpEdit_SetList(ref cboLevelNo, i, i + " - зэрэглэл");
                }

                    //SetCombo("RATECODE", "NAME", cboLevelNo, DS.Tables["CUSTRATE"], null);
                    SetCombo("TYPECODE", "NAME", cboTypeCode, DS.Tables["CUSTOMERTYPECODE"], new int[] { 1 });
                #endregion
                #region[ Хаяг ]
                SetCombo("SUBDISTCODE", "NAME", cboSubDist, DS.Tables["CUSTSUBDISTRICT"], new int[] { 1, 2 });
                SetCombo("DISTCODE", "NAME", cboDist, DS.Tables["CUSTDISTRICT"], new int[] { 1 });
                SetCombo("CITYCODE", "NAME", cboCity, DS.Tables["CUSTCITY"], null);

                #endregion
                #region[ Зураг ]

                FormUtility.LookUpEdit_SetList(ref cboPictureType, 0, "Цээж зураг");
                FormUtility.LookUpEdit_SetList(ref cboPictureType, 1, "Тамга");
                FormUtility.LookUpEdit_SetList(ref cboPictureType, 2, "Гэрчилгээ");
                FormUtility.LookUpEdit_SetList(ref cboPictureType, 3, "Бусад");

                #endregion
                #region[ Хамаатан садан ]
                SetCombo("TYPECODE", "NAME", cboTab4FamilyType, DS.Tables["FAMILYTYPECODE"], null);
                SetCombo("MASKID", "MASKNAME", cboReg, DS.Tables["REGISTERMASK"], new int[] { 2, 3 });
                SetCombo("MASKID", "MASKNAME", cboPass, DS.Tables["PASSMASK"], new int[] { 2, 3 });
                #endregion
                #region[Захирал]
                FormUtility.LookUpEdit_SetList(ref cboTabDirSex, 0, "Эр");
                FormUtility.LookUpEdit_SetList(ref cboTabDirSex, 1, "Эм");
                SetCombo("MASKID", "MASKNAME", cboTabDirReg, DS.Tables["REGISTERMASK"], new int[] { 2, 3 });
                SetCombo("MASKID", "MASKNAME", cboTabDirPass, DS.Tables["PASSMASK"], new int[] { 2, 3 });
                #endregion
                #region[ Холбоо барьсан мэдээлэл ]
                SetCombo("TYPECODE", "NAME", cboTab7ContactType, DS.Tables["CUSTCONTACTTYPE"], null);
                #endregion
                #region[ Харилцагчийн дансны мэдээлэл ]
                SetCombo("BANKID", "NAME", cboTab8BankNo, DS.Tables["BANK"], null);
                SetCombo("CURRENCY", "NAME", cboTab8CurCode, DS.Tables["CURRENCY"], null);
                #endregion
            }
        }
        private void xtraTabCustomer_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (txtCustomerNo.Text == "")
                    e.Cancel = true;
                else
                {
                    Control control = FormUtility.FindControl(e.Page.Controls, typeof(ucTogglePanel));
                    if (control != null)
                    {
                        ucTogglePanel uc = (ucTogglePanel)control;
                        if (uc.ToggleFlag != 0) e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Энд нэмнэ
        private void xtraTabCustomer_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            switch (e.PageIndex)
            {
                case 1:
                    if (!loadAddress)
                    {
                        EventType = "RefreshAddress";
                        RefreshAddress();
                    }
                    break;
                case 2:
                    LoadDataPic(loadPicture, Static.ToLong(txtCustomerNo.EditValue), 120011, ucPicture, gvwPicture, C2Hide, C2Caption, C2CaptionValue, btnPicEnter, btnZoom);
                    break;
                case 3:
                    if (!loadDirector)
                    {
                        EventType = "RefreshDirector";
                        RefreshDirector();
                    }
                    break;
                case 4:
                    if (!loadFamily)
                    {
                        EventType = "RefreshFamily";
                        RefreshFamily();
                    }
                    break;
                case 5:
                    if (!loadContact)
                    {
                        EventType = "RefreshContact";
                        RefreshContact();
                    }
                    break;
                case 6:
                    if (!loadAccount)
                    {
                        EventType = "RefreshAccount";
                        RefreshAccount();
                    }
                    break;
                case 7:
                    if (!loadAcnt)
                    {
                        RefreshAcnt();
                        SetAcnt();
                    }
                    break;
                case 8:
                    if (!loadNote)
                    {
                        EventType = "RefreshNote";
                        RefreshNote();
                        FormUtility.RestoreStateGrid(appname, formname, ref gvwNote);
                    }
                    DTNote = AddData(Static.ToLong(txtCustomerNo.EditValue), 120051);
                    break;
                case 9:
                    LoadAdd();
                    break;
                case 10:
                    RefreshAccountList(Static.ToLong(txtCustomerNo.EditValue));
                    break;
            }
        }
        #endregion
        #region [ Genereal data ]
        int TxnCodeT1Select = 120000;
        int TxnCodeT1SubSelect = 120001;
        int TxnCodeT1Save = 120002;
        int TxnCodeT1Edit = 120003;
        int TxnCodeT1Delete = 120004;
        void ucCustGeneral_EventDataChanged()
        {
            return;
        }
        void ucCustGeneral_EventAddAfter()
        {
            try
            {
                ucCustGeneral.ToggleShowEdit = false;
                ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboClassCode, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucCustGeneral_EventDelete()
        {
            GeneralDeleteCustomer(TxnCodeT1Delete);
        }
        void ucCustGeneral_EventEdit(ref bool cancel)
        {
            object[] Value = new object[38];
            _customerno = Static.ToLong(txtCustomerNo.EditValue);
            if (EditValueClassCode == 0)
            {
                #region[ Value хувь хүн]
                Value[0] = _customerno;                       //CustomerNo
                Value[1] = 0;                                 //ClassCode
                Value[2] = Static.ToInt(cboClassCode.EditValue);    //TypeCode
                Value[3] = Static.ToInt(cboIndustry.EditValue);    //InduTypeCode
                Value[4] = Static.ToInt(cboSubIndustry.EditValue); //InduSubTypeCode
                Value[5] = txtFirstName.EditValue;                 //FirstName
                Value[6] = txtLastName.EditValue;                  //LastName
                Value[7] = txtMiddleName.EditValue;                //MiddleName
                Value[8] = "";                                //CorporateName
                Value[9] = "";                                //CorporateName2
                Value[10] = txtRegisterNo.EditValue;               //RegisterNo
                Value[11] = txtPassNo.EditValue;                   //PassNo
                Value[12] = Static.ToInt(cboSex.EditValue);        //Sex
                Value[13] = Static.ToDate(dteBirthDay.EditValue);  //BirthDay
                Value[14] = txtCompany.EditValue;                  //Company
                Value[15] = txtPosition.EditValue;                 //Position
                Value[16] = mmoExperience.EditValue;               //Experience
                Value[17] = txtEmail.EditValue;                    //Email
                Value[18] = txtTelephone.EditValue;                //Telephone
                Value[19] = txtMobile.EditValue;                   //Mobile
                Value[20] = txtHomePhone.EditValue;                //HomePhone
                Value[21] = txtFax.EditValue;                      //Fax
                Value[22] = "";                               //WebSite
                Value[23] = "";                               //SpecialApproval
                Value[24] = cboLevelNo.EditValue;            //levelno
                Value[25] = 0;                                //CountryCode
                Value[26] = 0;                                //LanguageCode
                Value[27] = cboBranch.EditValue;              //Branch                
                Value[28] = cboStatus.EditValue;              //Status
                Value[29] = Static.ToStr(txtDriverNo.EditValue);            //DriverNo
                Value[30] = Convert.ToDateTime(_core.TxnDate);     //CreateTime
                Value[31] = Static.ToInt(_core.RemoteObject.User.UserNo);     //CreateUser
                Value[32] = Static.ToStr(txtOldID.EditValue);    //OldID
                Value[33] = Static.ToInt(numHeight.EditValue);     //Height
                Value[34] = Static.ToDecimal(numFootSize.EditValue);    //FootSize
                Value[35] = Static.ToStr(numContractNo.EditValue);    //ContractNo
                Value[36] = Static.ToStr(txtAccountNo.EditValue);    //txtAccountNo
                Value[37] = Static.ToStr(txtIncomeAccountNo.EditValue);    //txtIncomeAccountNo
                #endregion
            }
            else
            {
                #region[ Value байгууллага]

                Value[0] = _customerno;                    //CustomerNo
                Value[1] = 1;                                 //ClassCode
                Value[2] = Static.ToInt(cboClassCode.EditValue);    //TypeCode
                Value[3] = Static.ToInt(cboIndustry.EditValue);    //InduTypeCode
                Value[4] = Static.ToInt(cboSubIndustry.EditValue); //InduSubTypeCode
                Value[5] = "";                                //FirstName
                Value[6] = "";                                //LastName
                Value[7] = "";                                //MiddleName
                Value[8] = txtCorporateName.EditValue;             //CorporateName
                Value[9] = txtCorporateName2.EditValue;            //CorporateName2
                Value[10] = txtRegisterNo.EditValue;               //RegisterNo
                Value[11] = txtPassNo.EditValue;                   //PassNo
                Value[12] = "";                               //Sex
                Value[13] = Static.ToDate(dteBirthDay.EditValue);  //BirthDay
                Value[14] = "";                               //Company
                Value[15] = "";                               //Position
                Value[16] = mmoExperience.EditValue;               //Experience
                Value[17] = txtCorEmail.EditValue;                 //Email
                Value[18] = txtCorPhone.EditValue;                 //Telephone
                Value[19] = "";                               //Mobile
                Value[20] = "";                               //HomePhone
                Value[21] = txtCorFax.EditValue;                   //Fax
                Value[22] = txtCorWebSite.EditValue;               //WebSite
                Value[23] = "";                               //SpecialApproval
                Value[24] = cboLevelNo.EditValue;            //levelno
                Value[25] = 0;                                //CountryCode
                Value[26] = 0;                                //LanguageCode
                Value[27] = cboBranch.EditValue;                   //Branch                
                Value[28] = cboStatus.EditValue;                   //Status
                Value[29] = Static.ToStr(txtDriverNo.EditValue);                 //DriverNo
                Value[30] = Convert.ToDateTime(_core.TxnDate);           //CreateTime
                Value[31] = Static.ToInt(_core.RemoteObject.User.UserNo);     //CreateUser
                Value[32] = Static.ToStr(txtOldID.EditValue);    //OldID
                Value[33] = 0;                                   //Height
                Value[34] = 0;                                   //FootSize
                Value[35] = Static.ToStr(numContractNo.EditValue);    //ContractNo
                Value[36] = Static.ToStr(txtAccountNo.EditValue);    //txtAccountNo
                Value[37] = Static.ToStr(txtIncomeAccountNo.EditValue);    //txtIncomeAccountNo
                #endregion
            }
            OldValue = Value;
        }
        void ucCustGeneral_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucCustGeneral.FieldValidate(ref err, ref cont) == true)
            {
                SaveCustomerData(isnew, ref cancel);
            }
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucCustGeneral_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void GeneralRefreshData(long customerid)
        {
            cboPassMask.ItemIndex = 0;
            Result res = new Result();
            try
            {
                if (customerid != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeT1SubSelect, TxnCodeT1SubSelect, new object[] { customerid, 0 });

                    if (res.ResultNo == 0)
                    {
                        DataRow DR = res.Data.Tables[0].Rows[0];
                        if (_core.RemoteObject.GetTxn(120002))
                        {
                            cboTypeCode.Enabled = true;
                            cboLevelNo.Enabled = true;
                            ucCustGeneral.DataSource = res.Data;
                            ucCustGeneral.FieldLinkSetValues();
                        }
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SaveCustomerData(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            object[] obj = new object[38];
            string msg = "";
            try
            {
                _customerno = Static.ToLong(txtCustomerNo.EditValue);
                if (Static.ToInt(cboClassCode.EditValue) == 0)
                {
                    #region[ Value хувь хүн]
                    obj[0] = _customerno;                       //CustomerNo
                    obj[1] = 0;                                 //ClassCode
                    obj[2] = Static.ToInt(cboClassCode.EditValue);    //TypeCode
                    obj[3] = Static.ToInt(cboIndustry.EditValue);    //InduTypeCode
                    obj[4] = Static.ToInt(cboSubIndustry.EditValue); //InduSubTypeCode
                    obj[5] = txtFirstName.EditValue;                 //FirstName
                    obj[6] = txtLastName.EditValue;                  //LastName
                    obj[7] = txtMiddleName.EditValue;                //MiddleName
                    obj[8] = "";                                //CorporateName
                    obj[9] = "";                                //CorporateName2
                    obj[10] = txtRegisterNo.EditValue;               //RegisterNo
                    obj[11] = txtPassNo.EditValue;                   //PassNo
                    obj[12] = Static.ToInt(cboSex.EditValue);        //Sex
                    obj[13] = Static.ToDate(dteBirthDay.EditValue);  //BirthDay
                    obj[14] = txtCompany.EditValue;                  //Company
                    obj[15] = txtPosition.EditValue;                 //Position
                    obj[16] = mmoExperience.EditValue;               //Experience
                    obj[17] = txtEmail.EditValue;                    //Email
                    obj[18] = txtTelephone.EditValue;                //Telephone
                    obj[19] = txtMobile.EditValue;                   //Mobile
                    obj[20] = txtHomePhone.EditValue;                //HomePhone
                    obj[21] = txtFax.EditValue;                      //Fax
                    obj[22] = "";                               //WebSite
                    obj[23] = "";                               //SpecialApproval
                    obj[24] = cboLevelNo.EditValue;            //levelno
                    obj[25] = 0;                                //CountryCode
                    obj[26] = 0;                                //LanguageCode
                    obj[27] = cboBranch.EditValue;              //Branch                
                    obj[28] = cboStatus.EditValue;              //Status
                    obj[29] = Static.ToStr(txtDriverNo.EditValue);            //DriverNo
                    obj[30] = Convert.ToDateTime(_core.TxnDate);     //CreateTime
                    obj[31] = Static.ToInt(_core.RemoteObject.User.UserNo);     //CreateUser
                    obj[32] = Static.ToStr(txtOldID.EditValue);    //OldID
                    obj[33] = Static.ToInt(numHeight.EditValue);     //Height
                    obj[34] = Static.ToDecimal(numFootSize.EditValue);    //FootSize
                    obj[35] = Static.ToStr(numContractNo.EditValue);    //ContractNo
                    obj[36] = Static.ToStr(txtAccountNo.EditValue);    //txtAccountNo
                    obj[37] = Static.ToStr(txtIncomeAccountNo.EditValue);    //txtIncomeAccountNo
                    #endregion
                }
                else
                {
                    #region[ Value байгууллага]
                    obj[0] = _customerno;                    //CustomerNo
                    obj[1] = 1;                                 //ClassCode
                    obj[2] = Static.ToInt(cboClassCode.EditValue);    //TypeCode
                    obj[3] = Static.ToInt(cboIndustry.EditValue);    //InduTypeCode
                    obj[4] = Static.ToInt(cboSubIndustry.EditValue); //InduSubTypeCode
                    obj[5] = "";                                //FirstName
                    obj[6] = "";                                //LastName
                    obj[7] = "";                                //MiddleName
                    obj[8] = txtCorporateName.EditValue;             //CorporateName
                    obj[9] = txtCorporateName2.EditValue;            //CorporateName2
                    obj[10] = txtRegisterNo.EditValue;               //RegisterNo
                    obj[11] = txtPassNo.EditValue;                   //PassNo
                    obj[12] = "";                               //Sex
                    obj[13] = Static.ToDate(dteBirthDay.EditValue);  //BirthDay
                    obj[14] = "";                               //Company
                    obj[15] = "";                               //Position
                    obj[16] = mmoExperience.EditValue;               //Experience
                    obj[17] = txtCorEmail.EditValue;                 //Email
                    obj[18] = txtCorPhone.EditValue;                 //Telephone
                    obj[19] = "";                               //Mobile
                    obj[20] = "";                               //HomePhone
                    obj[21] = txtCorFax.EditValue;                   //Fax
                    obj[22] = txtCorWebSite.EditValue;               //WebSite
                    obj[23] = "";                               //SpecialApproval
                    obj[24] = cboLevelNo.EditValue;            //levelno
                    obj[25] = 0;                                //CountryCode
                    obj[26] = 0;                                //LanguageCode
                    obj[27] = cboBranch.EditValue;                   //Branch                
                    obj[28] = cboStatus.EditValue;                   //Status
                    obj[29] = Static.ToStr(txtDriverNo.EditValue);                 //DriverNo
                    obj[30] = Convert.ToDateTime(_core.TxnDate);           //CreateTime
                    obj[31] = Static.ToInt(_core.RemoteObject.User.UserNo);     //CreateUser
                    obj[32] = Static.ToStr(txtOldID.EditValue);    //OldID
                    obj[33] = 0;                                   //Height
                    obj[34] = 0;                                   //FootSize
                    obj[35] = Static.ToStr(numContractNo.EditValue);    //ContractNo
                    obj[36] = Static.ToStr(txtAccountNo.EditValue);    //txtAccountNo
                    obj[37] = Static.ToStr(txtIncomeAccountNo.EditValue);    //txtIncomeAccountNo
                    #endregion
                }
                object[] FieldName = {"CustomerNo","ClassCode","TypeCode","InduTypeCode","InduSubTypeCode","FirstName","LastName","MiddleName","CorporateName","CorporateName2",
                                         "RegisterNo","PassNo","Sex","BirthDay","Company","Position","Experience","Email","Telephone","Mobile",
                                         "HomePhone","Fax", "WebSite","SpecialApproval","levelno","CountryCode","LanguageCode", "Branch","Status","DriverNo",
                                         "CreateTime","CreateUser", "OldId","Height","Foot","ContractNo","AccountNo","IncomeAccountNo"};
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeT1Save, TxnCodeT1Save, new object[] { obj, FieldName, 0 });
                    msg = "Амжилттай нэмлээ .";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeT1Edit, TxnCodeT1Edit, new object[] { obj, OldValue, FieldName, 0 });
                    msg = "Амжилттай засварлалаа .";
                }
                if (res.ResultNo == 0)
                {
                    if (isnew)
                    {
                        txtCustomerNo.EditValue = Static.ToStr(res.Param[0]);
                        _customerid = Static.ToLong(txtCustomerNo.EditValue);
                    }
                    MessageBox.Show(msg);
                    ucCustGeneral.ToggleShowEdit = true;
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cancel = true;
            }
        }
        void GeneralDeleteCustomer(int TxnDelete)
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                long _customerid = Static.ToLong(txtCustomerNo.EditValue);

                if (_customerid != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnDelete, TxnDelete, new object[] { _customerid });

                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region[ EditValueChanged]
        private void cboIndustry_EditValueChanged(object sender, EventArgs e)
        {
            string filter = "";
            if (Static.ToStr(cboIndustry.EditValue) != "")
                filter = "TypeCode=" + Static.ToStr(cboIndustry.EditValue);
            SetComboSub("SubTypeCode", "NAME", cboSubIndustry, DS.Tables["SubIndustry"], filter, new int[] { 0, 2 });
        }
        private void cboRegisterMask_EditValueChanged(object sender, EventArgs e)
        {
            if (cboRegisterMask.GetSelectedDataRow() != null)
            {
                txtRegisterNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                DataRowView drv = (DataRowView)cboRegisterMask.GetSelectedDataRow();

                if (Static.ToStr(drv["MaskValue"]) != "")
                    txtRegisterNo.Properties.Mask.EditMask = Static.ToStr(drv["MaskValue"]);
            }
        }
        private void cboPassMask_EditValueChanged(object sender, EventArgs e)
        {
            if (cboPassMask.GetSelectedDataRow() != null)
            {
                txtPassNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                DataRowView drv = (DataRowView)cboPassMask.GetSelectedDataRow();

                if (Static.ToStr(drv["MaskValue"]) != "")
                    txtPassNo.Properties.Mask.EditMask = Static.ToStr(drv["MaskValue"]);
            }
        }
        private void cboClassCode_EditValueChanged(object sender, EventArgs e)
        {
            if (_customerid == 0)
            {
                if (((DevExpress.XtraEditors.LookUpEdit)sender).GetSelectedDataRow() != null)
                {
                    DataRow DRow = ((DataRowView)((DevExpress.XtraEditors.LookUpEdit)sender).GetSelectedDataRow()).Row;
                    if (DRow[0].ToString() == "0")
                    {
                        EditValueClassCode = 0;
                        Individual();
                        switch (ucCustGeneral.ToggleFlag)
                        {
                            case 0:
                                ucCustGeneral.FieldLinkSetSaveState();
                                break;
                            case 1:
                                ucCustGeneral.FieldLinkSetNewState(false);
                                break;
                            case 2:
                                ucCustGeneral.FieldLinkSetEditState();
                                break;
                        }
                    }
                    if (DRow[0].ToString() == "1")
                    {
                        EditValueClassCode = 1;
                        Corporate();
                        switch (ucCustGeneral.ToggleFlag)
                        {
                            case 0:
                                ucCustGeneral.FieldLinkSetSaveState();
                                break;
                            case 1:
                                ucCustGeneral.FieldLinkSetNewState(false);
                                break;
                            case 2:
                                ucCustGeneral.FieldLinkSetSaveState();
                                break;
                        }
                    }
                    cboRegisterMask.ItemIndex = 0;
                    cboPassMask.ItemIndex = 0;
                    cboTypeCode.ItemIndex = 0;
                    FormUtility.LookUpEdit_SetValue(ref cboBranch, _core.RemoteObject.User.BranchCode);
                    cboDriverNoMask.ItemIndex = 0;
                    cboStatus.ItemIndex = 0;
                    cboLevelNo.ItemIndex = 0;
                    cboIndustry.ItemIndex = 0;
                }
                else
                {
                    Individual();
                }
            }
            else
            {
                EditValueClassCode = _CLASSCODE;
            }

        }
        private void cboDriverNoMask_EditValueChanged(object sender, EventArgs e)
        {
            if (cboDriverNoMask.GetSelectedDataRow() != null)
            {
                txtDriverNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                DataRowView drv = (DataRowView)cboDriverNoMask.GetSelectedDataRow();

                if (Static.ToStr(drv["MaskValue"]) != "")
                    txtDriverNo.Properties.Mask.EditMask = Static.ToStr(drv["MaskValue"]);
            }
        }
        #endregion

        #endregion
        #region [ Address data ]

        int TxnCodeT2Select = 120006;
        int TxnCodeT2Save = 120008;
        int TxnCodeT2Edit = 120009;
        int TxnCodeT2Delete = 120010;
        void ucAddress_EventDelete()
        {
            EventDelete(Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue), TxnCodeT2Delete, TxnCodeT2Select, ucAddress, gvwAddress, C1Hide, C1Caption, C1CaptionValue);
            EventType = "RefreshAddress";
            RefreshAddress();
        }
        void ucAddress_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucAddress.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue), Static.ToInt(cboCity.EditValue), Static.ToInt(cboDist.EditValue), Static.ToInt(cboSubDist.EditValue), txtAdd.EditValue, (chkAddrCurrent.Checked ? 1 : 0), txtApartment.EditValue };
                EventSave(isnew, obj, TxnCodeT2Edit, TxnCodeT2Save, Static.ToLong(txtCustomerNo.EditValue), TxnCodeT2Select, ucAddress, gvwAddress, C1Hide, C1Caption, C1CaptionValue);
            }
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucAddress_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue), Static.ToInt(cboCity.EditValue), Static.ToInt(cboDist.EditValue), Static.ToInt(cboSubDist.EditValue), txtAdd.EditValue, (chkAddrCurrent.Checked ? 1 : 0), txtApartment.EditValue };
            OldValue = Value;
        }
        void ucAddress_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwAddress);
        }
        void RefreshAddress()
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(txtCustomerNo.EditValue) != 0)
                {
                    ucAddress.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120006, 120006, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
                    if (res.ResultNo == 0)
                    {
                        ucAddress.DataSource = res.Data;
                        ucAddress.FieldLinkSetSaveState();
                        ucAddress.FieldLinkSetValues();
                        SetAddress();
                        loadAddress = false;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetAddress()
        {
            gvwAddress.Columns[0].Caption = "Харилцагчийн дугаар";
            gvwAddress.Columns[0].Visible = false;
            gvwAddress.Columns[1].Caption = "Дэс дугаар";
            gvwAddress.Columns[1].Visible = false;
            gvwAddress.Columns[2].Caption = "Аймаг Хот";
            gvwAddress.Columns[3].Caption = "Сум дүүрэг";
            gvwAddress.Columns[5].Caption = "Нэмэлт мэдээлэл";
            gvwAddress.Columns[7].Caption = "Байр";
            gvwAddress.Columns[4].Caption = "Баг хороо";
            gvwAddress.Columns[6].Caption = "Идэвхитэй эсэх";
            gvwAddress.Columns[6].Visible = false;
            gvwAddress.Columns[8].Caption = "Огноо";
        }
        private void cboCity_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                if (cboCity.EditValue != null && cboCity.EditValue != DBNull.Value && cboCity.EditValue != "")
                    filter = "CITYCODE=" + cboCity.EditValue;

                if (DS.Tables["CUSTDISTRICT"] != null)
                    SetComboSub("DISTCODE", "NAME", cboDist, DS.Tables["CUSTDISTRICT"], filter, new int[] { 1 });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cboDist_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                if (cboCity.EditValue != null && cboCity.EditValue != DBNull.Value && cboCity.EditValue != "")
                    filter = "CITYCODE=" + cboCity.EditValue;

                if (cboDist.EditValue != null && cboDist.EditValue != DBNull.Value && cboDist.EditValue != "")
                {
                    if (filter != "")
                        filter = filter + "and DISTCODE=" + cboDist.EditValue;
                    else
                        filter = "DISTCODE=" + cboDist.EditValue;
                }
                SetComboSub("SUBDISTCODE", "NAME", cboSubDist, DS.Tables["CUSTSUBDISTRICT"], filter, new int[] { 1, 2 });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gvwAddress_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucAddress.FieldLinkSetValues();
        }
        #endregion
        #region[ Picture data ]

        int TxnCodeT3Select = 120011;
        int TxnCodeT3Save = 120013;
        int TxnCodeT3Edit = 120014;
        int TxnCodeT3Delete = 120015;

        void ucPicture_EventSave(bool isnew, ref bool cancel)
        {
            //string err = "";
            //Control cont = null;
            //if (isnew)
            //{
            //    if (ucPicture.FieldValidate(ref err, ref cont) == true)
            //    {
            //        if (picCustomer.Image != null)
            //        {
            //            Result ResImage = ISM.Template.AttachUtility.SaveImage(_core, 120013, 0, "PICTURE", picCustomer.Image, Static.ToInt(cboPictureType.EditValue), cboPictureType.EditValue.ToString());
            //            if (ResImage.ResultNo == 0)
            //            {
            //                AttachID = Static.ToLong(ResImage.Param[0]);
            //                if (AttachID != 0)
            //                {
            //                    object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numSeqNo.EditValue), Static.ToInt(cboPictureType.EditValue), Static.ToULong(AttachID) };
            //                    EventSave(isnew, obj, TxnCodeT3Edit, TxnCodeT3Save, Static.ToLong(txtCustomerNo.EditValue), TxnCodeT3Select, ucPicture, gvwPicture, C2Hide, C2Caption, C2CaptionValue);
            //                    btnPicEnter.Enabled = false;
            //                    btnZoom.Enabled = false;
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show(ResImage.ResultDesc);
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Таны зураг сонгогдоогүй байна .");
            //            cancel = true;
            //        }
            //    }
            //    else
            //    {
            //        cancel = true;
            //        MessageBox.Show(err);
            //        cont.Select();
            //    }
            //}
            //else
            //{
            //    if (picCustomer.Image != null)
            //    {
            //        Result ResImage = ISM.Template.AttachUtility.SaveImage(_core, 120014, Static.ToULong(numAttachID.EditValue), "PICTURE", picCustomer.Image, Static.ToInt(cboPictureType.EditValue), cboPictureType.EditValue.ToString());
            //        if (ResImage.ResultNo == 0)
            //        {
            //            AttachID = Static.ToLong(ResImage.Param[0]);
            //            if (AttachID != 0)
            //            {
            //                object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numSeqNo.EditValue), Static.ToInt(cboPictureType.EditValue), Static.ToULong(AttachID) };
            //                EventSave(isnew, obj, TxnCodeT3Edit, TxnCodeT3Save, Static.ToLong(txtCustomerNo.EditValue), TxnCodeT3Select, ucPicture, gvwPicture, C2Hide, C2Caption, C2CaptionValue);
            //                btnPicEnter.Enabled = false;
            //                btnZoom.Enabled = false;
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show(ResImage.ResultDesc);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Таны зураг сонгогдоогүй байна .");
            //        cancel = true;
            //    }
            //}
        }
        void ucPicture_EventDelete()
        {
            EventDeleteImage(Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numSeqNo.EditValue), TxnCodeT3Delete, TxnCodeT3Select, ucPicture, gvwPicture, C2Hide, C2Caption, C2CaptionValue);
        }
        void ucPicture_EventEdit(ref bool cancel)
        {
            btnPicEnter.Enabled = true;
            btnZoom.Enabled = true;
            object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numSeqNo.EditValue), Static.ToInt(cboPictureType.EditValue), Static.ToULong(AttachID) };
            OldValue = Value;
        }
        void ucPicture_EventAdd(ref bool cancel)
        {
            btnPicEnter.Enabled = true;
            btnZoom.Enabled = true;
        }
        void ucPicture_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwPicture);
        }
        private void btnPicEnter_Click(object sender, EventArgs e)
        {
            ISM.Template.FormImage frm = new FormImage();
            frm.Resource = _core.Resource;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                picCustomer.Image = frm.ImageObject;
            }
        }
        void ucPicture_EventReject()
        {
            btnPicEnter.Enabled = false;
            btnZoom.Enabled = false;
        }
        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (picCustomer.Image != null)
            {
                ISM.Template.FormImage frm = new FormImage();
                frm.Resource = _core.Resource;
                frm.ImageObject = picCustomer.Image;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    picCustomer.Image = frm.ImageObject;
                }
            }
            else
            {
                MessageBox.Show("Зураг сонгогдоогүй байна .");
            }
        }
        private void picCustomer_DoubleClick(object sender, EventArgs e)
        {
            //Result ResGetImage = ISM.Template.AttachUtility.GetImage(_core, Static.ToInt(120012), Static.ToULong(numAttachID.EditValue), ref GetImage);
            //if (ResGetImage.ResultNo == 0)
            //{
            //    picCustomer.Image = GetImage;
            //}
            //else
            //{
            //    MessageBox.Show(ResGetImage.ResultDesc);
            //}
        }
        private void gvwPicture_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //ucPicture.FieldLinkSetValues();
            //Result ResGetImage = ISM.Template.AttachUtility.GetImage(_core, Static.ToInt(120012), Static.ToULong(numAttachID.EditValue), ref GetImage);
            //if (ResGetImage.ResultNo == 0)
            //{
            //    picCustomer.Image = GetImage;
            //}
        }
        #endregion
        #region[ Director ]
        int TxnCodeDirSelect = 120073;
        int TxnCodeDirDelete = 120077;
        void ucDirector_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTabDirSeqNo.EditValue), Static.ToStr(txtPosition.EditValue), Static.ToStr(txtFirstName.EditValue), Static.ToStr(txtLastName.EditValue), Static.ToStr(txtMiddleName.EditValue), Static.ToStr(numTabDirRegisterNo.EditValue), Static.ToStr(numTabDirPassNo.EditValue), Static.ToInt(cboTabDirSex.EditValue), Static.ToDateTime(dteTabDirBirthday.EditValue) };
            OldValue = Value;
        }
        void ucDirector_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (ucDirector.FieldValidate(ref err, ref cont) == true)
            {
                SaveDirector(isnew);
            }
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucDirector_EventDelete()
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                if (txtCustomerNo.EditValue != null && numTabDirSeqNo.EditValue != null)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120077, 120077, new object[] { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTabDirSeqNo.EditValue) });

                    if (res.ResultNo == 0)
                    {
                        RefreshDirector();
                        MessageBox.Show("Амжилттай устгагдлаа");
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucDirector_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwDirector);
        }
        void RefreshDirector()
        {
            Result res = new Result();
            try
            {
                ucDirector.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120073, 120073, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
                if (res.ResultNo == 0)
                {
                    ucDirector.DataSource = res.Data;
                    ucDirector.FieldLinkSetValues();
                    ucDirector.FieldLinkSetSaveState();
                    SetDirector();
                    loadDirector = false;
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
        void SetDirector()
        {
            gvwDirector.Columns[0].Caption = "Харилцагчийн дугаар";
            gvwDirector.Columns[0].Visible = false;
            gvwDirector.Columns[1].Caption = "Дэс дугаар";
            gvwDirector.Columns[1].Visible = false;
            gvwDirector.Columns[2].Caption = "Албан тушаал";
            gvwDirector.Columns[3].Caption = "Овог";
            gvwDirector.Columns[4].Caption = "Эцэг эхийн нэр";
            gvwDirector.Columns[5].Caption = "Захиралын нэр";
            gvwDirector.Columns[6].Caption = "Регистрийн дугаар";
            gvwDirector.Columns[7].Caption = "Иргэний үнэмлэхний дугаар";
            gvwDirector.Columns[8].Caption = "Хүйс";
            gvwDirector.Columns[9].Caption = "Төрсөн огноо";
        }
        void SaveDirector(bool isnew)
        {
            Result res = new Result();
            string msg = "";
            try
            {
                object[] obj = 
                { 
                    Static.ToLong(txtCustomerNo.EditValue),
                    Static.ToLong(numTabDirSeqNo.EditValue),
                    Static.ToStr(txtTabDirPosition.EditValue),
                    Static.ToStr(txtTabDirFirstName.EditValue),
                    Static.ToStr(txtTabDirLastName.EditValue),
                    Static.ToStr(txtTabDirMiddleName.EditValue),
                    Static.ToStr(numTabDirRegisterNo.EditValue),
                    Static.ToStr(numTabDirPassNo.EditValue),
                    Static.ToInt(cboTabDirSex.EditValue),
                    Static.ToDateTime(dteTabDirBirthday.EditValue) };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120075, 120075, new object[] { obj });
                    msg = "Амжилттай нэмлээ";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120076, 120076, new object[] { obj, OldValue });
                    msg = "Амжилттай засварлалаа";
                }
                if (res.ResultNo == 0)
                {
                    RefreshDirector();
                    MessageBox.Show(msg);
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
        private void gvwDirector_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucDirector.FieldLinkSetValues();
        }
        private void cboTabDirPass_EditValueChanged(object sender, EventArgs e)
        {
            if (cboTabDirPass.GetSelectedDataRow() != null)
            {
                numTabDirPassNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                DataRowView drv = (DataRowView)cboTabDirPass.GetSelectedDataRow();

                if (Static.ToStr(drv["MaskValue"]) != "")
                    numTabDirPassNo.Properties.Mask.EditMask = Static.ToStr(drv["MaskValue"]);
            }
        }
        private void cboTabDirReg_EditValueChanged(object sender, EventArgs e)
        {
            if (cboTabDirReg.GetSelectedDataRow() != null)
            {
                numTabDirRegisterNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                DataRowView drv = (DataRowView)cboTabDirReg.GetSelectedDataRow();

                if (Static.ToStr(drv["MaskValue"]) != "")
                    numTabDirRegisterNo.Properties.Mask.EditMask = Static.ToStr(drv["MaskValue"]);
            }
        }
        #endregion
        #region[ Family Data ]
        int TxnCodeT4Select = 120021;
        int TxnCodeT4Delete = 120025;
        void ucFamily_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab4SeqNo.EditValue), Static.ToInt(cboTab4FamilyType.EditValue), txtTab4FirstName.EditValue, txtTab4LastName.EditValue, txtTab4MiddleName.EditValue, txtTab4RegisterNo.EditValue, txtTab4PassNo.EditValue, txtTab4Email.EditValue, Static.ToLong(numTab4Telephone.EditValue), Static.ToLong(numTab4Mobile.EditValue) };
            OldValue = Value;
        }
        void ucFamily_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (Static.ToInt(cboClassCode.EditValue) == 1)
            {
                txtTab4FirstName.EditValue = 1;
                txtTab4MiddleName.EditValue = 2;
            }
            if (ucFamily.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToInt(cboClassCode.EditValue) == 1)
                {
                    txtTab4FirstName.EditValue = "";
                    txtTab4MiddleName.EditValue = "";
                }
                SaveFamily(isnew);
            }
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucFamily_EventDelete()
        {
            EventDelete(Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab4SeqNo.EditValue), TxnCodeT4Delete, TxnCodeT4Select, ucFamily, gvwFamily, C3Hide, C3Caption, C3CaptionValue);
        }
        void ucFamily_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwFamily);
        }
        void RefreshFamily()
        {
            Result res = new Result();
            try
            {
                ucFamily.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120021, 120021, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
                if (res.ResultNo == 0)
                {
                    ucFamily.DataSource = res.Data;
                    ucFamily.FieldLinkSetValues();
                    ucFamily.FieldLinkSetSaveState();
                    SetFamily();
                    loadFamily = false;
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
        void SetFamily()
        {
            gvwFamily.Columns[0].Caption = "Харилцагчийн дугаар";
            gvwFamily.Columns[0].Visible = false;
            gvwFamily.Columns[1].Caption = "Дэс дугаар";
            gvwFamily.Columns[1].Visible = false;
            gvwFamily.Columns[2].Caption = "Хамаатны төрөл";
            gvwFamily.Columns[3].Caption = "Эцэг эхийн нэр";
            gvwFamily.Columns[4].Caption = "Харилцагчийн нэр";
            gvwFamily.Columns[6].Caption = "Регистрийн дугаар";
            gvwFamily.Columns[5].Caption = "Овог";
            gvwFamily.Columns[7].Caption = "Иргэний үнэмлэхний дугаар";
            gvwFamily.Columns[8].Caption = "Майл хаяг";
            gvwFamily.Columns[9].Caption = "Ажлын утасны дугаар";
            gvwFamily.Columns[10].Caption = "Гар утасны дугаар";
            gvwFamily.Columns[11].Caption = "Харилцагчийн дугаар";
        }
        void SaveFamily(bool isnew)
        {
            Result res = new Result();
            string msg = "";
            try
            {
                object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab4SeqNo.EditValue), Static.ToInt(cboTab4FamilyType.EditValue), txtTab4FirstName.EditValue, txtTab4LastName.EditValue, txtTab4MiddleName.EditValue, txtTab4RegisterNo.EditValue, txtTab4PassNo.EditValue, txtTab4Email.EditValue, Static.ToLong(numTab4Telephone.EditValue), Static.ToLong(numTab4Mobile.EditValue), Static.ToLong(numTab4FamilyCustNo.EditValue) };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120023, 120023, new object[] { obj });
                    msg = "Амжилттай нэмлээ";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120024, 120024, new object[] { obj, OldValue });
                    msg = "Амжилттай засварлалаа";
                }
                if (res.ResultNo == 0)
                {
                    RefreshFamily();
                    MessageBox.Show(msg);
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
        private void gvwFamily_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucDirector.FieldLinkSetValues();
        }
        private void cboReg_EditValueChanged(object sender, EventArgs e)
        {
            if (cboReg.GetSelectedDataRow() != null)
            {
                txtTab4RegisterNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                DataRowView drv = (DataRowView)cboReg.GetSelectedDataRow();

                if (Static.ToStr(drv["MaskValue"]) != "")
                    txtTab4RegisterNo.Properties.Mask.EditMask = Static.ToStr(drv["MaskValue"]);
            }
        }
        private void cboPass_EditValueChanged(object sender, EventArgs e)
        {
            if (cboPass.GetSelectedDataRow() != null)
            {
                txtTab4PassNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                DataRowView drv = (DataRowView)cboPass.GetSelectedDataRow();

                if (Static.ToStr(drv["MaskValue"]) != "")
                    txtTab4PassNo.Properties.Mask.EditMask = Static.ToStr(drv["MaskValue"]);
            }
        }
        #endregion
        #region[ Contact ]

        int TxnCodeT7Select = 120036;
        int TxnCodeT7Save = 120038;
        int TxnCodeT7Edit = 120039;
        int TxnCodeT7Delete = 120040;
        void ucContact_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab7SeqNo.EditValue), Static.ToDate(dteTab7ContactDate.EditValue), Static.ToDateTime(dteTab7ContactDate.EditValue), Static.ToInt(cboTab7ContactType.EditValue), txtTab7Note.EditValue, txtTab7BriefDesc.EditValue };
            OldValue = Value;
        }
        void ucContact_EventDelete()
        {
            EventDelete(Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab7SeqNo.EditValue), TxnCodeT7Delete, TxnCodeT7Select, ucContact, gvwContact, C6Hide, C6Caption, C6CaptionValue);
        }
        void ucContact_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucContact.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab7SeqNo.EditValue), Static.ToDate(dteTab7ContactDate.EditValue), Static.ToDateTime(dteTab7ContactDate.EditValue), Static.ToInt(cboTab7ContactType.EditValue), txtTab7Note.EditValue, txtTab7BriefDesc.EditValue, _core.RemoteObject.User.UserNo };
                EventSave(isnew, obj, TxnCodeT7Edit, TxnCodeT7Save, Static.ToLong(txtCustomerNo.EditValue), TxnCodeT7Select, ucContact, gvwContact, C6Hide, C6Caption, C6CaptionValue);
            }
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucContact_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwContact);
        }
        void RefreshContact()
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(txtCustomerNo.EditValue) != 0)
                {
                    ucContact.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120036, 120036, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
                    if (res.ResultNo == 0)
                    {
                        ucContact.DataSource = res.Data;
                        ucContact.FieldLinkSetValues();
                        ucContact.FieldLinkSetSaveState();
                        SetContact();
                        loadContact = false;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetContact()
        {
            gvwContact.Columns[0].Caption = "Харилцагчийн дугаар";
            gvwContact.Columns[0].Visible = false;
            gvwContact.Columns[1].Caption = "Дэс дугаар";
            gvwContact.Columns[1].Visible = false;
            gvwContact.Columns[2].Caption = "Огноо";
            gvwContact.Columns[3].Caption = "Шийдвэрлэсэн Огноо";
            gvwContact.Columns[4].Caption = "Холбоо барьсан төрөл";
            gvwContact.Columns[5].Caption = "Холбоо барьсан талаарх мэдээлэл";
            gvwContact.Columns[6].Caption = "Товч утга";
            gvwContact.Columns[7].Caption = "Хэрэглэгч";
        }
        private void gvwContact_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucContact.FieldLinkSetValues();
        }

        #endregion
        #region[ Account ]

        int TxnCodeT8Select = 120041;
        int TxnCodeT8Save = 120043;
        int TxnCodeT8Edit = 120044;
        int TxnCodeT8Delete = 120045;
        void ucAccount_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab8SeqNo.EditValue), Static.ToInt(cboTab8BankNo.EditValue), Static.ToStr(cboTab8CurCode.EditValue), txtTab8AccountNo.EditValue };
            OldValue = Value;
        }
        void ucAccount_EventDelete()
        {
            EventDelete(Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab8SeqNo.EditValue), TxnCodeT8Delete, TxnCodeT8Select, ucAccount, gvwAccount, C7Hide, C7Caption, C7CaptionValue);
        }
        void ucAccount_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (ucAccount.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab8SeqNo.EditValue), Static.ToInt(cboTab8BankNo.EditValue), Static.ToStr(cboTab8CurCode.EditValue), txtTab8AccountNo.EditValue };
                EventSave(isnew, obj, TxnCodeT8Edit, TxnCodeT8Save, Static.ToLong(txtCustomerNo.EditValue), TxnCodeT8Select, ucAccount, gvwAccount, C7Hide, C7Caption, C7CaptionValue);
            }
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucAccount_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwAccount);
        }
        void RefreshAccount()
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(txtCustomerNo.EditValue) != 0)
                {
                    ucAccount.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120041, 120041, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
                    if (res.ResultNo == 0)
                    {
                        ucAccount.DataSource = res.Data;
                        ucAccount.FieldLinkSetValues();
                        ucAccount.FieldLinkSetSaveState();
                        SetAccount();
                        loadAccount = false;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetAccount()
        {
            gvwAccount.Columns[0].Caption = "Харилцагчийн дугаар";
            gvwAccount.Columns[0].Visible = false;
            gvwAccount.Columns[1].Caption = "Дэс дугаар";
            gvwAccount.Columns[1].Visible = false;
            gvwAccount.Columns[2].Caption = "Банкны дугаар";
            gvwAccount.Columns[3].Caption = "Валют";
            gvwAccount.Columns[4].Caption = "Дансны дугаар";
        }
        private void gvwAccount_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucAccount.FieldLinkSetValues();
        }

        #endregion
        #region[ Inner Account ]

        ///int InnerAccountSelect = 120026;
        ///int InnerAccountSave = 120027;
        ///int InnerAccountEdit = 120028;
        ///int InnerAccountDelete = 120029;
        void ucAcnt_EventEdit(ref bool cancel)
        {
            object[] Value = { txtCustomerNo.EditValue, numAcntAccountNo.EditValue, txtAcntAccountName.EditValue };
            OldValue = Value;
        }
        void ucAcnt_EventDelete()
        {
             Result res = new Result();
             try
             {
                 if (txtCustomerNo.EditValue != null && numAcntAccountNo.EditValue != null)
                 {
                     DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                     if (d == System.Windows.Forms.DialogResult.No) return;


                     res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120030, 120030, new object[] { txtCustomerNo.EditValue, numAcntAccountNo.EditValue });

                     if (res.ResultNo == 0)
                     {
                         RefreshAcnt();
                         MessageBox.Show("Амжилттай устгагдлаа");
                     }
                     else
                     {
                         MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
        }
        void ucAcnt_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            string msg = "";
            Result res = new Result();
            Control cont = null;
            if (ucAcnt.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj=new object[3];
                obj[0] = txtCustomerNo.EditValue;
                obj[1] = numAcntAccountNo.EditValue;
                obj[2] = txtAcntAccountName.EditValue;
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120028, 120028, new object[] { obj });
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120029, 120029, new object[] { obj ,OldValue});
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    RefreshAcnt();
                    MessageBox.Show(msg);
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                    cancel = true;
                }
            }
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucAcnt_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwAcnt);
        }
        void RefreshAcnt()
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(txtCustomerNo.EditValue) != 0)
                {
                    ucAcnt.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120026, 120026, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
                    if (res.ResultNo == 0)
                    {
                        ucAcnt.DataSource = res.Data;
                        ucAcnt.FieldLinkSetValues();
                        ucAcnt.FieldLinkSetSaveState();
                        loadAcnt = false;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetAcnt()
        {
            gvwAcnt.Columns[0].Caption = "Харилцагчийн дугаар";
            gvwAcnt.Columns[0].Visible = false;
            gvwAcnt.Columns[1].Caption = "Дансны дугаар";
            gvwAcnt.Columns[2].Caption = "Дансны нэр";
        }
        private void gvwAcnt_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucAcnt.FieldLinkSetValues();
        }
        #endregion
        #region[ Note ]

        int TxnCodeT10Select = 120051;
        int TxnCodeT10Save = 120053;
        int TxnCodeT10Edit = 120054;
        int TxnCodeT10Delete = 120055;
        void ucNote_EventEdit(ref bool cancel)
        {
            DataRow row = gvwNote.GetFocusedDataRow();
            object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(row["SeqNo"]), Static.ToDate(row["TxnDate"]), Static.ToDate(row["TxnDate"]), Static.ToInt(numTab10UserNo.EditValue), mmeTab10Note.EditValue };
            OldValue = Value;
        }
        void ucNote_EventDelete()
        {
            EventDelete(Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab10SeqNo.EditValue), TxnCodeT10Delete, TxnCodeT10Select, ucNote, gvwNote, C9Hide, C9Caption, C9CaptionValue);
        }
        void ucNote_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucNote.FieldValidate(ref err, ref cont) == true)
            {
                if (isnew)
                {
                    object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab10SeqNo.EditValue), Static.ToDate(_core.TxnDate), Static.ToDate(_core.TxnDate), Static.ToInt(numTab10UserNo.EditValue), mmeTab10Note.EditValue };
                    EventSave(isnew, obj, TxnCodeT10Edit, TxnCodeT10Save, Static.ToLong(txtCustomerNo.EditValue), TxnCodeT10Select, ucNote, gvwNote, C9Hide, C9Caption, C9CaptionValue);
                }
                else
                {
                    DataRow row = gvwNote.GetFocusedDataRow();
                    object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(row["SeqNo"]), Static.ToDate(row["TxnDate"]), Static.ToDate(row["TxnDate"]), Static.ToInt(numTab10UserNo.EditValue), mmeTab10Note.EditValue };
                    EventSave(isnew, obj, TxnCodeT10Edit, TxnCodeT10Save, Static.ToLong(txtCustomerNo.EditValue), TxnCodeT10Select, ucNote, gvwNote, C9Hide, C9Caption, C9CaptionValue);
                }
            }
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucNote_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwNote);
        }
        void RefreshNote()
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(txtCustomerNo.EditValue) != 0)
                {
                    ucNote.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120051, 120051, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
                    if (res.ResultNo == 0)
                    {
                        ucNote.DataSource = res.Data;
                        ucNote.FieldLinkSetValues();
                        ucNote.FieldLinkSetSaveState();
                        SetNote();
                        loadNote = false;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetNote()
        {
            gvwNote.Columns[0].Caption = "Харилцагчийн дугаар";
            gvwNote.Columns[0].Visible = false;
            gvwNote.Columns[1].Caption = "Дэс дугаар";
            gvwNote.Columns[1].Visible = false;
            gvwNote.Columns[2].Caption = "TXN Огноо";
            gvwNote.Columns[2].Visible = false;
            gvwNote.Columns[3].Caption = "Явуулсан Огноо";
            gvwNote.Columns[3].Visible = false;
            gvwNote.Columns[4].Caption = "Хэрэглэгчийн дугаар";
            gvwNote.Columns[5].Caption = "Харилцагчийн талаарх товч дүгнэлт";
        }
        private void gvwNote_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucNote.FieldLinkSetValues();
        }
        //private void BtnSearch_Click(object sender, EventArgs e)
        //{
        //    HeavenPro.List.UserList frm = new HeavenPro.List.UserList(_core);

        //    frm.ucUserList.Browsable = true;

        //    DialogResult res = frm.ShowDialog();

        //    if ((res == System.Windows.Forms.DialogResult.OK))
        //    {
        //        numTab10UserNo.EditValue = Static.ToStr(frm.ucUserList.SelectedRow["UserNo"]);
        //    }
        //}

        #endregion
        #region[ Add ]

        private void LoadAdd()
        {
            if (!loadAdd)
            {
                AddRead();
            }
        }
        private void AddRead()
        {
            try
            {
                loadAdd = true;
                ucDynamicDataPanel1.TablePrivSelect = TablePrivSelect;
                ucDynamicDataPanel1.TablePrivUpdate = TablePrivUpdate;
                ucDynamicDataPanel1.TableTypeId = (ulong)Static.ToLong(cboTypeCode.EditValue);
                ucDynamicDataPanel1.Remote = _core.RemoteObject;
                ucDynamicDataPanel1.TableRowKey = (ulong)_customerid;
                ucDynamicDataPanel1.TableNamePrefix = TableNamePrefix;
                Result res = ucDynamicDataPanel1.ListRead();

                if (res.ResultNo != 0)
                    MessageBox.Show(string.Format("Result: code={0} desc={1}", res.ResultNo, res.ResultDesc));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нэмэлт мэдээлэл унших явцад алдаа гарлаа:" + ex.Message);
            }
        }
        private void btnAddSave_Click(object sender, EventArgs e)
        {
            try
            {
                Result res = ucDynamicDataPanel1.ListSave();
                if (res.ResultNo != 0)
                    MessageBox.Show(string.Format("Result: code={0} desc={1}", res.ResultNo, res.ResultDesc));
                else
                    MessageBox.Show("Амжилттай хадгаллаа");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Нэмэлт мэдээллийг хадгалах явцад алдаа гарлаа:" + ex.Message);
            }
        }

        #endregion
        #region[ Customer Function ]
        private void RefreshAccountList(long CustomerNo)
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120072, 120072, new object[] { txtCustomerNo.EditValue });
                if (res.ResultNo == 0)
                {
                    grdAccountList.DataSource = res.Data.Tables[0];
                    SetAccountList();
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
        private void SetAccountList()
        {
            gvwAccountList.Columns[0].Caption = "Дансны дугаар";
            gvwAccountList.Columns[1].Caption = "Дансны нэр";
            gvwAccountList.Columns[2].Caption = "Дансны нэр 2";
            gvwAccountList.Columns[3].Caption = "Салбарын дугаар";
            gvwAccountList.Columns[4].Caption = "Бүтээгдэхүүний дугаар";
            gvwAccountList.Columns[5].Caption = "Бүтээгдэхүүн";
            gvwAccountList.Columns[6].Caption = "Үлдэгдэл";
            gvwAccountList.Columns[7].Caption = "Валютын код";
            gvwAccountList.Columns[8].Caption = "Хариуцсан хэрэглэгчийн дугаар";
            gvwAccountList.Columns[9].Caption = "Зэрэглэл";
            gvwAccountList.Columns[10].Caption = "Үүсгэсэн огноо";
            gvwAccountList.Columns[11].Caption = "Дансны төлвийн дугаар";
            gvwAccountList.Columns[12].Caption = "Дансны төлөв";
            gvwAccountList.Columns[13].Caption = "Өглөг авлагын эхлэх огноо";
            gvwAccountList.Columns[14].Caption = "Өглөг авлагын дуусах огноо";
            gvwAccountList.Columns[15].Caption = "Сүүлийн гүйлгээ хийсэн теллерийн дугаар";
            gvwAccountList.OptionsView.ColumnAutoWidth = false;

            gvwAccountList.Columns[1].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[2].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[3].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[4].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[5].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[6].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[7].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[8].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[9].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[10].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[11].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[12].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[13].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[14].OptionsColumn.AllowEdit = false;
            gvwAccountList.Columns[15].OptionsColumn.AllowEdit = false;


            gvwAccountList.OptionsBehavior.Editable = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwAccountList);
            //gvwClaim.OptionsBehavior.ReadOnly = true;
        }
        void SetData(DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue)
        {
            try
            {
                for (int i = 0; i < hide.Length; i++)
                {
                    GView.Columns[hide[i]].Visible = false;
                }

                for (int i = 0; i < caption.Length; i++)
                {
                    GView.Columns[caption[i]].Caption = CaptionValue[i];
                }
            }
            catch
            {
                MessageBox.Show("GridView-д харуулж чадахгүй байна .");
            }

        }
        void RefreshData(long CustomerID, int TxnCode, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue)
        {
            Result res = new Result();
            try
            {
                if (CustomerID != 0)
                {
                    ucTP.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCode, TxnCode, new object[] { CustomerID });
                    if (res.ResultNo == 0)
                    {
                        ucTP.DataSource = res.Data;
                        ucTP.FieldLinkSetValues();
                        SetData(GView, hide, caption, CaptionValue);
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void RefreshDataAttach(long CustomerID, int TxnCode, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue, DevExpress.XtraEditors.SimpleButton SBtn1, DevExpress.XtraEditors.SimpleButton SBtn2)
        {
            Result res = new Result();

            try
            {

                if (CustomerID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCode, TxnCode, new object[] { CustomerID });

                    if (res.ResultNo == 0)
                    {
                        ucTP.DataSource = res.Data;
                        ucTP.FieldLinkSetValues();
                        SetData(GView, hide, caption, CaptionValue);
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void RefreshDataPic(long CustomerID, int TxnCode, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue, DevExpress.XtraEditors.SimpleButton SBtn1Image, DevExpress.XtraEditors.SimpleButton SBtn2Image)
        {
            Result res = new Result();

            try
            {

                if (CustomerID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCode, TxnCode, new object[] { CustomerID });

                    if (res.ResultNo == 0)
                    {
                        ucTP.DataSource = res.Data;
                        ucTP.FieldLinkSetValues();
                        ucTP.FieldLinkSetSaveState();
                        SetData(GView, hide, caption, CaptionValue);
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        DataTable AddData(long CustomerID, int TxnCode)
        {
            DataTable DT;
            DT = null;
            Result res = new Result();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCode, TxnCode, new object[] { CustomerID });
            if (res.ResultNo == 0)
            {
                DT = res.Data.Tables[0];
            }
            return DT;
        }
        void LoadData(bool check, long CustomerID, int TxnCode, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue)
        {
            if (!check)
            {
                RefreshAddress();
            }
        }
        void LoadDataAttach(bool check, long CustomerID, int TxnCode, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue, DevExpress.XtraEditors.SimpleButton BADD, DevExpress.XtraEditors.SimpleButton BEDIT)
        {
            if (!check)
            {
                RefreshDataAttach(CustomerID, TxnCode, ucTP, GView, hide, caption, CaptionValue, BADD, BEDIT);
            }
        }
        void LoadDataPic(bool check, long CustomerID, int TxnCode, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue, DevExpress.XtraEditors.SimpleButton BADD, DevExpress.XtraEditors.SimpleButton BEDIT)
        {
            if (!check)
            {
                RefreshDataPic(CustomerID, TxnCode, ucTP, GView, hide, caption, CaptionValue, BADD, BEDIT);
            }
        }
        void EventDelete(long CustomerID, long SeqID, int TxnCodeDel, int TxnCodeRef, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue)
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                if (CustomerID != 0 && SeqID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeDel, TxnCodeDel, new object[] { CustomerID, SeqID });

                    if (res.ResultNo == 0)
                    {
                        switch (EventType)
                        {
                            case "RefreshAdress":
                                RefreshAddress();
                                break;
                            case "RefreshFamily":
                                RefreshFamily();
                                break;
                            case "RefreshContact":
                                RefreshContact();
                                break;
                            case "RefreshAccount":
                                RefreshAccount();
                                break;
                            case "RefreshNote":
                                RefreshNote();
                                break;
                        }
                        MessageBox.Show("Амжилттай устгагдлаа");
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void EventDeleteImage(long CustomerID, long SeqID, int TxnCodeDel, int TxnCodeRef, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue)
        {
            //Result res = new Result();

            //try
            //{
            //    DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (d == System.Windows.Forms.DialogResult.No) return;

            //    if (CustomerID != 0 && SeqID != 0)
            //    {
            //        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeDel, TxnCodeDel, new object[] { CustomerID, SeqID });

            //        if (res.ResultNo == 0)
            //        {
            //            DataRow DR = gvwPicture.GetFocusedDataRow();

            //            if (DR != null)
            //            {
            //                Result ResFile = ISM.Template.AttachUtility.Delete(_core, TxnCodeDel, (ulong)Static.ToLong(DR["ATTACHID"]));
            //                RefreshData(CustomerID, TxnCodeRef, ucTP, GView, hide, caption, CaptionValue);
            //                MessageBox.Show("Амжилттай устгагдлаа");
            //                btnPicEnter.Enabled = true;
            //                btnZoom.Enabled = true;
            //                ucTP.FieldLinkSetSaveState();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Устгах файл сонгогдоогүй байна.");
            //            }

            //        }
            //        else
            //        {
            //            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        void EventSave(bool isnew, object[] obj, int TxnCodeEdit, int TxnCodeSave, long CustomerID, int TxnCodeRef, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue)
        {
            Result res = new Result();
            string msg = "";
            try
            {
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeSave, TxnCodeSave, new object[] { obj });
                    msg = "Амжилттай нэмлээ";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeEdit, TxnCodeEdit, new object[] { obj, OldValue });
                    msg = "Амжилттай засварлалаа";
                }
                if (res.ResultNo == 0)
                {
                    switch (EventType)
                    {
                        case "RefreshAddress":
                            RefreshAddress();
                            break;
                        case "RefreshFamily":
                            RefreshFamily();
                            break;    
                        case "RefreshContact":
                            RefreshContact();
                            break;
                        case "RefreshAccount":
                            RefreshAccount();
                            break;
                        case "RefreshNote":
                            RefreshNote();
                            break;
                    }
                    MessageBox.Show(msg);
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
        DataSet DataSetComboAll()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DictUtility.PrivNo = PrivNo;
            string[] names = new string[] { "BRANCH", "CUSTOMERTYPECODE", "REGISTERMASK", "PASSMASK", "CUSTCITY", "CUSTDISTRICT", "CUSTSUBDISTRICT", "FAMILYTYPECODE", "BANK", "CURRENCY", "INDUSTRY", "SUBINDUSTRY", "DRIVERMASK", "CUSTRATE","CUSTCONTACTTYPE" };
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
            DataSet DS = new DataSet();

            for (int i = 0; i < Tables.Count; i++)
            {
                DataTable DT = ((DataTable)Tables[i]).Copy();
                DT.TableName = names[i];
                DS.Tables.Add(DT);
            }
            return DS;
        }
        void SetCombo(string ValueField, string NameField, DevExpress.XtraEditors.LookUpEdit LEdit, DataTable DT, int[] Hide)
        {
            string msg = "";
            if (DT == null)
            {
                msg = "Dictionary-д утга оруулна уу .";
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref LEdit, DT, ValueField, NameField, "", Hide);
            }
            if (msg != "")
                MessageBox.Show(msg);
        }
        void SetCombo(string ValueField, string NameField, DevExpress.XtraEditors.LookUpEdit LEdit, DataTable DT, string Filter, int[] Hide)
        {
            string msg = "";
            if (DT == null)
            {
                msg = "Dictionary-д утга оруулна уу .";
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref LEdit, DT, ValueField, NameField, Filter, Hide);
            }
            if (msg != "")
                MessageBox.Show(msg);
        }
        void SetComboSub(string ValueField, string NameField, DevExpress.XtraEditors.LookUpEdit LEdit, DataTable DT, string Filter, int[] Hide)
        {
            try
            {
                string msg = "";
                if (DT == null)
                {
                    msg = "Dictionary-д утга оруулна ууаагүй байна. ";
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref LEdit, DT, ValueField, NameField, Filter, Hide);
                }
                if (msg != "")
                    MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + Filter);
            }
        }
        void Individual()
        {
            FieldIndividual();
            Tab2.Text = "Ажил эрхлэлтийн байдал";
            tabDirector.PageVisible = false;
            //tabFamily.PageVisible = true;
            tabFamily.Text = "Хамаатан садан";

            #region[Label]
            labelControl2.Text = "Ургийн овог :";
            labelControl5.Text = "Эцэг эхийн нэр :";
            labelControl10.Text = "Регистерийн дугаар :";
            labelControl4.Text = "Төрсөн огноо :";
            labelControl13.Text = "Үнэмлэхний дугаар :";
            labelControl22.Text = "Ажил эрхлэлтийн төрөл";
            labelControl23.Text = "Ажил эрхлэлтийн дэд төрөл";

            labelControl15.Text = "Гар утас";
            labelControl21.Text = "Гэрийн утас";
            labelControl19.Text = "Ажлын утас";
            labelControl20.Text = "Факс";
            #endregion

            #region[Visible]
            txtCorporateName.Visible = false;
            txtCorporateName2.Visible = false;
            txtCorEmail.Visible = false;
            txtCorFax.Visible = false;
            txtCorPhone.Visible = false;
            txtCorWebSite.Visible = false;
            #endregion

            #region[ Show ]
            txtDriverNo.Visible = true;
            cboDriverNoMask.Visible = true;
            //txtMiddleName.
            txtMiddleName.Visible = true;
            txtLastName.Visible = true;
            txtFirstName.Visible = true;
            cboSex.Visible = true;
            txtCompany.Visible = true;
            txtPosition.Visible = true;
            labelControl12.Visible = true;
            labelControl16.Visible = true;
            labelControl14.Visible = true;
            labelControl18.Visible = true;
            labelControl74.Visible = true;
            labelControl65.Visible = true;
            labelControl59.Visible = true;

            txtMobile.Visible = true;
            txtTelephone.Visible = true;
            txtEmail.Visible = true;
            txtFax.Visible = true;
            txtHomePhone.Visible = true;

            #endregion

            #region[ Combo ]

            FilterSubIndustry = "CUSTTYPE=" + 0;

            SetCombo("MASKID", "MASKNAME", cboRegisterMask, DS.Tables["REGISTERMASK"], FilterSubIndustry, new int[] { 2, 3, 4 });
            SetCombo("MASKID", "MASKNAME", cboPassMask, DS.Tables["PASSMASK"], FilterSubIndustry, new int[] { 2, 3, 4 });
            SetCombo("MASKID", "MASKNAME", cboDriverNoMask, DS.Tables["DRIVERMASK"], FilterSubIndustry, new int[] { 2, 3, 4 });

            SetCombo("TypeCode", "NAME", cboTypeCode, DS.Tables["CUSTOMERTYPECODE"], "CLASSCODE<>1", new int[] { 1 });
            SetCombo("TypeCode", "NAME", cboIndustry, DS.Tables["INDUSTRY"], "CLASSCODE<>1", new int[] { 1 });
            FilterSubIndustry = "";
            if (Static.ToStr(cboIndustry.EditValue) != "")
                FilterSubIndustry = "TypeCode=" + Static.ToInt(cboIndustry.EditValue);
            SetComboSub("SubTypeCode", "NAME", cboSubIndustry, DS.Tables["SUBINDUSTRY"], FilterSubIndustry, new int[] { 0, 2 });
            #endregion

            #region[ ToolTip]
            txtMiddleName.ToolTipTitle = "Ургийн овог ороогүй байна .";
            txtFirstName.ToolTipTitle = "Эцэг эхийн нэр ороогүй байна .";
            txtLastName.ToolTipTitle = "Өөрийн нэр ороогүй байна .";
            txtRegisterNo.ToolTipTitle = "Регистерийн дугаар ороогүй байна .";
            txtPassNo.ToolTipTitle = "Үнэмлэхийн дугаар ороогүй байна .";
            cboBranch.ToolTipTitle = "Салбар ороогүй байна .";
            cboStatus.ToolTipTitle = "Төлөв ороогүй байна .";
            cboSex.ToolTipTitle = "Хүйс ороогүй байна .";
            dteBirthDay.ToolTipTitle = "Төрсөн огноо ороогүй байна .";
            cboIndustry.ToolTipTitle = "Ажил эрхлэлтийн төрөл ороогүй байна .";
            cboSubIndustry.ToolTipTitle = "Дэд төрөл ороогүй байна .";
            #endregion
        }
        void FieldIndividual()
        {
            ucCustGeneral.FieldLinkClear();
            ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("cboClassCode", 0, "classcode", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtMiddleName", 0, "MiddleName", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtFirstName", 0, "FirstName", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtLastName", 0, "LastName", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtPassNo", 0, "PassNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("dteBirthDay", 0, "BirthDay", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboSex", 0, "Sex", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtMobile", 0, "Mobile", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtHomePhone", 0, "HomePhone", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtTelephone", 0, "Telephone", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtFax", 0, "Fax", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtEmail", 0, "Email", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboIndustry", 0, "InduTypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboSubIndustry", 0, "InduSubTypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCompany", 0, "Company", "",false, false);
            ucCustGeneral.FieldLinkAdd("txtPosition", 0, "Position", "", false, false);
            ucCustGeneral.FieldLinkAdd("mmoExperience", 0, "Experience", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtDriverNo", 0, "DriverNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboLevelNo", 0, "LevelNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("numHeight", 0, "Height", "", false, false);
            ucCustGeneral.FieldLinkAdd("numFootSize", 0, "Foot", "", false, false);
            //ucCustGeneral.FieldLinkAdd("cboMemberType", 0, "MemberType", "", false, false);
            ucCustGeneral.FieldLinkAdd("numContractNo", 0, "ContractNo", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtAccountNo", 0, "AccountNo", "", false, false, false);
            ucCustGeneral.FieldLinkAdd("txtIncomeAccountNo", 0, "IncomeAccountNo", "", false, false, false);
        }
        void IndividualTrue()
        {
            SetCombo("TYPECODE", "NAME", cboTab4FamilyType, DS.Tables["FAMILYTYPECODE"], "CLASSCODE=1", new int[] { 1 });
            Tab2.Text = "Ажил эрхлэлтийн байдал";
            tabDirector.PageVisible = false;
            tabFamily.PageVisible = true;
            //tabUV.PageVisible = true;
            tabFamily.Text = "Хамаатан садан";
            labelControl33.Visible = true;
            labelControl35.Visible = true;
            txtTab4FirstName.Visible = true;
            txtTab4MiddleName.Visible = true;
            labelControl37.Text = "Иргэний үнэмлэх дугаар";
            labelControl32.Location = new Point(19, 14);
            numTab4SeqNo.Location = new Point(156, 11);
            labelControl28.Location = new Point(19, 40);
            cboTab4FamilyType.Location = new Point(156, 37);
            labelControl95.Location = new Point(19, 63);
            numTab4FamilyCustNo.Location = new Point(156, 60);
            btnTab4Find.Location = new Point(359, 59);

            #region[Label]
            labelControl2.Text = "Ургийн овог :";
            labelControl5.Text = "Эцэг эхийн нэр :";
            labelControl10.Text = "Регистерийн дугаар :";
            labelControl4.Text = "Төрсөн огноо :";
            labelControl13.Text = "Үнэмлэхний дугаар :";
            labelControl22.Text = "Ажил эрхлэлтийн төрөл";
            labelControl23.Text = "Ажил эрхлэлтийн дэд төрөл";

            labelControl15.Text = "Гар утас";
            labelControl21.Text = "Гэрийн утас";
            labelControl19.Text = "Ажлын утас";
            labelControl20.Text = "Факс";
            #endregion

            #region[Visible]
            txtCorporateName.Visible = false;
            txtCorporateName2.Visible = false;
            txtCorEmail.Visible = false;
            txtCorFax.Visible = false;
            txtCorPhone.Visible = false;
            txtCorWebSite.Visible = false;
            Tab5.PageVisible = true;
            #endregion

            #region[ Show ]
            txtDriverNo.Visible = true;
            cboDriverNoMask.Visible = true;
            txtMiddleName.Visible = true;
            txtLastName.Visible = true;
            txtFirstName.Visible = true;
            cboSex.Visible = true;
            txtCompany.Visible = true;
            txtPosition.Visible = true;
            labelControl12.Visible = true;
            labelControl16.Visible = true;
            labelControl14.Visible = true;
            labelControl18.Visible = true;
            labelControl74.Visible = true;
            labelControl65.Visible = true;
            labelControl59.Visible = true;

            txtMobile.Visible = true;
            txtTelephone.Visible = true;
            txtEmail.Visible = true;
            txtFax.Visible = true;
            txtHomePhone.Visible = true;

            #endregion

            #region[ Combo ]
            cboClassCode.Properties.DataSource = null; 
            FormUtility.LookUpEdit_SetList(ref cboClassCode, 0, "ИРГЭН");
            SetCombo("TypeCode", "NAME", cboIndustry, DS.Tables["INDUSTRY"], "CLASSCODE<>1", new int[] { 1 });
            if (Static.ToStr(cboIndustry.EditValue) != "")
                FilterSubIndustry = "CLASSCODE<>1 and TypeCode=" + cboIndustry.EditValue;
            SetComboSub("SubTypeCode", "NAME", cboSubIndustry, DS.Tables["SubIndustry"], FilterSubIndustry, new int[] { 0, 2 });

            #endregion

            #region[ ToolTip]
            txtMiddleName.ToolTipTitle = "Ургийн овог ороогүй байна .";
            txtFirstName.ToolTipTitle = "Эцэг эхийн нэр ороогүй байна .";
            txtLastName.ToolTipTitle = "Өөрийн нэр ороогүй байна .";
            txtRegisterNo.ToolTipTitle = "Регистерийн дугаар ороогүй байна .";
            txtPassNo.ToolTipTitle = "Үнэмлэхийн дугаар ороогүй байна .";
            cboBranch.ToolTipTitle = "Салбар ороогүй байна .";
            cboStatus.ToolTipTitle = "Төлөв ороогүй байна .";
            cboSex.ToolTipTitle = "Хүйс ороогүй байна .";
            dteBirthDay.ToolTipTitle = "Төрсөн огноо ороогүй байна .";
            cboIndustry.ToolTipTitle = "Ажил эрхлэлтийн төрөл ороогүй байна .";
            cboSubIndustry.ToolTipTitle = "Дэд төрөл ороогүй байна .";
            #endregion
        }
        void FieldIndividualTrue()
        {
            ucCustGeneral.FieldLinkClear();
            ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("cboClassCode", 0, "ClassCode", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtMiddleName", 0, "MiddleName", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtFirstName", 0, "FirstName", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtLastName", 0, "LastName", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtPassNo", 0, "PassNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("dteBirthDay", 0, "BirthDay", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboSex", 0, "Sex", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtMobile", 0, "Mobile", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtHomePhone", 0, "HomePhone", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtTelephone", 0, "Telephone", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtFax", 0, "Fax", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtEmail", 0, "Email", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboIndustry", 0, "InduTypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboSubIndustry", 0, "InduSubTypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCompany", 0, "Company", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtPosition", 0, "Position", "", false, false);
            ucCustGeneral.FieldLinkAdd("mmoExperience", 0, "Experience", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtDriverNo", 0, "DriverNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboLevelNo", 0, "LevelNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("numHeight", 0, "Height", "", false, false);
            ucCustGeneral.FieldLinkAdd("numFootSize", 0, "Foot", "", false, false);
            //ucCustGeneral.FieldLinkAdd("cboMemberType", 0, "MemberType", "", false, false);
            ucCustGeneral.FieldLinkAdd("numContractNo", 0, "ContractNo", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtAccountNo", 0, "AccountNo", "", false, false, false);
            ucCustGeneral.FieldLinkAdd("txtIncomeAccountNo", 0, "IncomeAccountNo", "", false, false, false);
        }
        void Corporate()
        {
            FieldCorporate();
            Tab2.Text = "Байгууллагын мэдээлэл";
            tabDirector.PageVisible = true;
            //tabFamily.PageVisible = false;
            tabFamily.Text = "Холбоотой тал";

            #region[Label]
            labelControl2.Text = "Байгууллагын нэр :";
            labelControl5.Text = "Байгууллагын нэр2 :";
            labelControl10.Text = "Байгууллагын регистер :";
            labelControl4.Text = "Байгуулагдсан огноо :";
            labelControl13.Text = "Улсын бүртгэлийн дугаар :";
            labelControl22.Text = "Үйл ажиллагааны чиглэл :";
            labelControl23.Text = "Үйл ажиллагааны дэд чиглэл :";

            labelControl15.Text = "Утас";
            labelControl21.Text = "Факс";
            labelControl19.Text = "Имэйл";
            labelControl20.Text = "Вэб хуудас";
            #endregion

            #region[Visible]
            txtDriverNo.Visible = false;
            cboDriverNoMask.Visible = false;
            txtMiddleName.Visible = false;
            txtLastName.Visible = false;
            txtFirstName.Visible = false;
            cboSex.Visible = false;
            txtCompany.Visible = false;
            txtPosition.Visible = false;
            labelControl12.Visible = false;
            labelControl16.Visible = false;
            labelControl14.Visible = false;
            labelControl18.Visible = false;
            labelControl74.Visible = false;
            labelControl65.Visible = false;
            labelControl59.Visible = false;

            txtMobile.Visible = false;
            txtTelephone.Visible = false;
            txtEmail.Visible = false;
            txtFax.Visible = false;
            txtHomePhone.Visible = false;
            Tab5.PageVisible = false;
            #endregion

            #region[ Show ]
            txtCorporateName.Visible = true;
            txtCorporateName2.Visible = true;
            txtCorEmail.Visible = true;
            txtCorFax.Visible = true;
            txtCorPhone.Visible = true;
            txtCorWebSite.Visible = true;
            #endregion

            #region[ Combo ]
            FilterSubIndustry = "CUSTTYPE=" + 1;

            SetCombo("MASKID", "MASKNAME", cboRegisterMask, DS.Tables["REGISTERMASK"], FilterSubIndustry, new int[] { 2, 3, 4 });
            SetCombo("MASKID", "MASKNAME", cboPassMask, DS.Tables["PASSMASK"], FilterSubIndustry, new int[] { 2, 3, 4 });
            SetCombo("MASKID", "MASKNAME", cboDriverNoMask, DS.Tables["DRIVERMASK"], FilterSubIndustry, new int[] { 2, 3, 4 });
            SetCombo("TypeCode", "NAME", cboTypeCode, DS.Tables["CUSTOMERTYPECODE"], "CLASSCODE<>0", new int[] { 1 });
            SetCombo("TypeCode", "NAME", cboIndustry, DS.Tables["INDUSTRY"], "CLASSCODE<>0", new int[] { 1 });
            FilterSubIndustry = "";
            if (Static.ToStr(cboIndustry.EditValue) != "")
                FilterSubIndustry = "CLASSCODE<>0 and TypeCode=" + cboIndustry.EditValue;
            SetComboSub("SubTypeCode", "NAME", cboSubIndustry, DS.Tables["SubIndustry"], FilterSubIndustry, new int[] { 0, 2 });

            //SetCombo("MASKID", "MASKNAME", cboCorRegisterMask, DS.Tables["REGISTERMASK"], new int[] { 2, 3 });
            //SetCombo("MASKID", "MASKNAME", cboCorPassMask, DS.Tables["PASSMASK"], new int[] { 2, 3 });
            cboLevelNo.ItemIndex = 0;
            cboTypeCode.ItemIndex = 0;

            #endregion

            #region[ ToolTip]
            txtCorporateName.ToolTipTitle = "Байгууллагын нэр ороогүй байна .";
            txtCorporateName2.ToolTipTitle = "Байгууллагын нэр2 ороогүй байна .";
            txtRegisterNo.ToolTipTitle = "Байгууллагын регистер ороогүй байна .";
            txtPassNo.ToolTipTitle = "Улсын бүртгэлийн дугаар ороогүй байна .";
            cboBranch.ToolTipTitle = "Салбар ороогүй байна .";
            cboStatus.ToolTipTitle = "Төлөв ороогүй байна .";
            dteBirthDay.ToolTipTitle = "Байгуулагдсан огноо ороогүй байна .";
            txtCorPhone.ToolTipTitle = "Утас ороогүй байна .";
            cboIndustry.ToolTipTitle = "Үйл ажиллагааны чиглэл ороогүй байна .";
            cboSubIndustry.ToolTipTitle = "Үйл ажиллагааны дэд чиглэл ороогүй байна .";

            #endregion
        }
        void FieldCorporate()
        {
            ucCustGeneral.FieldLinkClear();
            ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("cboClassCode", 0, "ClassCode", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorporateName", 0, "CorporateName", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtCorporateName2", 0, "CorporateName2", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtPassNo", 0, "PassNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
            ucCustGeneral.FieldLinkAdd("dteBirthDay", 0, "BirthDay", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorPhone", 0, "Telephone", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorFax", 0, "Fax", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorEmail", 0, "Email", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorWebSite", 0, "WebSite", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboIndustry", 0, "InduTypeCode", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboSubIndustry", 0, "InduSubTypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("mmoExperience", 0, "Experience", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboLevelNo", 0, "LevelNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("numHeight", 0, "Height", "", false, false);
            ucCustGeneral.FieldLinkAdd("numFootSize", 0, "Foot", "", false, false);
            //ucCustGeneral.FieldLinkAdd("cboMemberType", 0, "MemberType", "", false, false);
            ucCustGeneral.FieldLinkAdd("numContractNo", 0, "ContractNo", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtAccountNo", 0, "AccountNo", "", false, false, false);
            ucCustGeneral.FieldLinkAdd("txtIncomeAccountNo", 0, "IncomeAccountNo", "", false, false, false);
        }
        void CorporateTrue()
        {
            SetCombo("TYPECODE", "NAME", cboTab4FamilyType, DS.Tables["FAMILYTYPECODE"], "CLASSCODE=2", new int[] { 1 });
            #region[ Location ]
            labelControl32.Location = new Point(19, 63);
            numTab4SeqNo.Location = new Point(156, 60);
            labelControl28.Location = new Point(19, 85);
            cboTab4FamilyType.Location = new Point(156, 82);
            labelControl95.Location = new Point(19, 110);
            numTab4FamilyCustNo.Location = new Point(156, 107);
            btnTab4Find.Location = new Point(359, 105);
            #endregion

            #region[Label]
            labelControl2.Text = "Байгууллагын нэр :";
            labelControl5.Text = "Байгууллагын нэр2 :";
            labelControl10.Text = "Байгууллагын регистер :";
            labelControl4.Text = "Байгуулагдсан огноо :";
            labelControl13.Text = "Улсын бүртгэлийн дугаар :";
            labelControl22.Text = "Үйл ажиллагааны чиглэл :";
            labelControl23.Text = "Үйл ажиллагааны дэд чиглэл :";
            labelControl37.Text = "Улсын бүртгэлийн дугаар";
            labelControl15.Text = "Утас";
            Tab2.Text = "Байгууллагын мэдээлэл";
            tabFamily.Text = "Холбоотой тал";
            labelControl21.Text = "Факс";
            labelControl19.Text = "Имэйл";
            labelControl20.Text = "Вэб хуудас";
            #endregion

            #region[Visible]
            txtDriverNo.Visible = false;
            cboDriverNoMask.Visible = false;
            txtMiddleName.Visible = false;
            txtLastName.Visible = false;
            txtFirstName.Visible = false;
            cboSex.Visible = false;
            txtCompany.Visible = false;
            txtPosition.Visible = false;
            labelControl12.Visible = false;
            labelControl16.Visible = false;
            labelControl14.Visible = false;
            labelControl18.Visible = false;
            labelControl74.Visible = false;
            labelControl65.Visible = false;
            labelControl59.Visible = false;
            tabDirector.PageVisible = true;
            labelControl33.Visible = false;
            labelControl35.Visible = false;
            txtTab4FirstName.Visible = false;
            txtTab4MiddleName.Visible = false;
            txtMobile.Visible = false;
            txtTelephone.Visible = false;
            txtEmail.Visible = false;
            txtFax.Visible = false;
            txtHomePhone.Visible = false;

            #endregion

            #region[ Show ]
            txtCorporateName.Visible = true;
            txtCorporateName2.Visible = true;
            txtCorEmail.Visible = true;
            txtCorFax.Visible = true;
            txtCorPhone.Visible = true;
            txtCorWebSite.Visible = true;
            #endregion

            #region[ Combo ]
            cboClassCode.Properties.DataSource = null;
            FormUtility.LookUpEdit_SetList(ref cboClassCode, 1, "БАЙГУУЛЛАГА");

            SetCombo("TypeCode", "NAME", cboIndustry, DS.Tables["INDUSTRY"], "CLASSCODE<>0", new int[] { 1 });
            if (Static.ToStr(cboIndustry.EditValue) != "")
                FilterSubIndustry = "CLASSCODE<>0 and TypeCode=" + cboIndustry.EditValue;
            SetComboSub("SubTypeCode", "NAME", cboSubIndustry, DS.Tables["SubIndustry"], FilterSubIndustry, new int[] { 0, 2 });

            //cboRegisterMask.ItemIndex = 0;
            //cboPassMask.ItemIndex = 0;
            //cboDriverNoMask.ItemIndex = 0;
            //cboIndustry.ItemIndex = 0;
            #endregion

            #region[ ToolTip]
            txtCorporateName.ToolTipTitle = "Байгууллагын нэр ороогүй байна .";
            txtCorporateName2.ToolTipTitle = "Байгууллагын нэр2 ороогүй байна .";
            txtRegisterNo.ToolTipTitle = "Байгууллагын регистер ороогүй байна .";
            txtPassNo.ToolTipTitle = "Улсын бүртгэлийн дугаар ороогүй байна .";
            cboBranch.ToolTipTitle = "Салбар ороогүй байна .";
            cboStatus.ToolTipTitle = "Төлөв ороогүй байна .";
            dteBirthDay.ToolTipTitle = "Байгуулагдсан огноо ороогүй байна .";
            txtCorPhone.ToolTipTitle = "Утас ороогүй байна .";
            cboIndustry.ToolTipTitle = "Үйл ажиллагааны чиглэл ороогүй байна .";
            cboSubIndustry.ToolTipTitle = "Үйл ажиллагааны дэд чиглэл ороогүй байна .";
            #endregion
        }
        void FieldCorporateTrue()
        {
            ucCustGeneral.FieldLinkClear();
            ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("cboClassCode", 0, "ClassCode", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorporateName", 0, "CorporateName", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtCorporateName2", 0, "CorporateName2", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", true, false);
            ucCustGeneral.FieldLinkAdd("txtPassNo", 0, "PassNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
            ucCustGeneral.FieldLinkAdd("dteBirthDay", 0, "BirthDay", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorPhone", 0, "Telephone", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorFax", 0, "Fax", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorEmail", 0, "Email", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCorWebSite", 0, "WebSite", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboIndustry", 0, "InduTypeCode", "", true, false);
            ucCustGeneral.FieldLinkAdd("cboSubIndustry", 0, "InduSubTypeCode", "", false, false);
            ucCustGeneral.FieldLinkAdd("mmoExperience", 0, "Experience", "", false, false);
            ucCustGeneral.FieldLinkAdd("cboLevelNo", 0, "LevelNo", "", false, false);
            ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("numHeight", 0, "Height", "", false, false);
            ucCustGeneral.FieldLinkAdd("numFootSize", 0, "Foot", "", false, false);
            //ucCustGeneral.FieldLinkAdd("cboMemberType", 0, "MemberType", "", false, false);
            ucCustGeneral.FieldLinkAdd("numContractNo", 0, "ContractNo", "", false, false, true);
            ucCustGeneral.FieldLinkAdd("txtAccountNo", 0, "AccountNo", "", false, false, false);
            ucCustGeneral.FieldLinkAdd("txtIncomeAccountNo", 0, "IncomeAccountNo", "", false, false, false);
        }
        void LoadFunction()
        {
            if (_core.Resource != null)
            {
                btnZoom.Image = _core.Resource.GetImage("image_zoom");
                //BtnSearch.Image = _core.Resource.GetImage("button_find");
                ucCustGeneral.Resource = _core.Resource;
                ucAccount.Resource = _core.Resource;
                ucAddress.Resource = _core.Resource;
                ucContact.Resource = _core.Resource;
                ucFamily.Resource = _core.Resource;
                ucNote.Resource = _core.Resource;
                ucPicture.Resource = _core.Resource;
                ucDirector.Resource = _core.Resource;
                ucAcnt.Resource = _core.Resource;
                btnAddSave.Image = _core.Resource.GetImage("navigate_save");
                btnContractFind.Image = _core.Resource.GetImage("button_find");
            }
        }
        #endregion
        #region[ Event KeyPress]
        private void txtRegisterNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboRegisterMask.Text == " - ")
            {
                e.Handled = true;
                MessageBox.Show("Регистерийн дугаарын Маск оруулна уу .");
            }
            else
                e.Handled = false;
        }
        private void txtPassNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboPassMask.Text == " - ")
            {
                e.Handled = true;
                MessageBox.Show("Үнэмлэхний дугаарын Маск оруулна уу .");
            }
            else
                e.Handled = false;
        }
        private void txtTab4RegisterNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboReg.Text == " - ")
            {
                e.Handled = true;
                MessageBox.Show("Регистерийн дугаарын Маск оруулна уу .");
            }
            else
                e.Handled = false;
        }
        private void txtTab4PassNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboPass.Text == " - ")
            {
                e.Handled = true;
                MessageBox.Show("Үнэмлэхний дугаарын Маск оруулна уу.");
            }
            else
                e.Handled = false;
        }
        private void txtDriverNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboDriverNoMask.Text == " - ")
            {
                e.Handled = true;
                MessageBox.Show("Үнэмлэхний дугаарын Маск оруулна уу .");
            }
            else
                e.Handled = false;
        }
        private void numTabDirRegisterNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboTabDirReg.Text == " - ")
            {
                e.Handled = true;
                MessageBox.Show("Регистерийн дугаарын Маск оруулна уу .");
            }
            else
                e.Handled = false;
        }
        private void numTabDirPassNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboTabDirPass.Text == " - ")
            {
                e.Handled = true;
                MessageBox.Show("Үнэмлэхний дугаарын Маск оруулна уу .");
            }
            else
                e.Handled = false;
        }
        #endregion
        #region[nothing]
        private void CustomerProp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void btnAttach_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    _customerno = Static.ToLong(txtCustomerNo.EditValue);
            //    if (_customerno != 0)
            //    {
            //        int TxnCodeT9Select = 120046;
            //        int TxnCodeT9Edit = 120049;
            //        int TxnCodeT9Delete = 120050;
            //        HeavenPro.Contract.FormAttachView frm = new HeavenPro.Contract.FormAttachView(_core, Static.ToStr(_customerno), 101, TxnCodeT9Select, TxnCodeT9Edit, TxnCodeT9Delete);
            //        frm.ShowDialog();
            //    }
            //    else MessageBox.Show("Харилцагчаа эхэлж үүсгэнэ үү.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void CustomerProp_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwAddress);
            FormUtility.SaveStateGrid(appname, formname, ref gvwPicture);
            FormUtility.SaveStateGrid(appname, formname, ref gvwAccount);
            FormUtility.SaveStateGrid(appname, formname, ref gvwContact);
            FormUtility.SaveStateGrid(appname, formname, ref gvwFamily);
            FormUtility.SaveStateGrid(appname, formname, ref gvwNote);
            FormUtility.SaveStateGrid(appname, formname, ref gvwAccountList);
            FormUtility.SaveStateGrid(appname, formname, ref gvwDirector);
            FormUtility.SaveStateGrid(appname, formname, ref gvwAcnt);

        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtRegisterNo.Text == "")
            {
                MessageBox.Show(this, "Регистерийн дугаар оруулаагүй байна. Оруулна уу.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Result res = new Result();
                try
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120066, 120066, new object[] { Static.ToStr(txtRegisterNo.EditValue) });
                    if (res.ResultNo == 0)
                    {
                        if (res.Data.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show(this, "Харилцагч үүсгэх боломжтой", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            foreach (DataRow DR in res.Data.Tables[0].Rows)
                            {
                                if (DR != null)
                                {
                                    object[] obj = new object[4];
                                    obj[0] = _core;
                                    obj[1] = Static.ToLong(DR["CustomerNo"]);
                                    obj[2] = Static.ToLong(DR["ClassCode"]);
                                    obj[3] = Static.ToInt(DR["TypeCode"]);
                                    EServ.Shared.Static.Invoke("HeavenPro.Customer.dll", "HeavenPro.Customer.Main", "CallCustomerProp", obj);
                                }
                            }
                        }
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
        }
        #endregion

        private void btnFind_Click(object sender, EventArgs e)
        {
            //if (Static.ToInt(cboClassCode.EditValue) == 0)
            //{
            //    HeavenPro.List.CustomerList frm = new List.CustomerList(_core, "0");
            //    frm.ucCustomerList.Browsable = true;
            //    DialogResult res = frm.ShowDialog();
            //    if ((res == System.Windows.Forms.DialogResult.OK))
            //    {
            //        numTab4FamilyCustNo.EditValue = frm.ucCustomerList.SelectedRow["CustomerNo"];
            //        txtTab4LastName.EditValue = frm.ucCustomerList.SelectedRow["LASTNAME"];
            //        txtTab4MiddleName.EditValue = frm.ucCustomerList.SelectedRow["MIDDLENAME"];
            //        txtTab4FirstName.EditValue = frm.ucCustomerList.SelectedRow["FIRSTNAME"];
            //        txtTab4RegisterNo.EditValue = frm.ucCustomerList.SelectedRow["REGISTERNO"];
            //        txtTab4PassNo.EditValue = frm.ucCustomerList.SelectedRow["PASSNO"];
            //    }
            //}
            //else
            //{
            //    HeavenPro.List.CustomerList frm = new List.CustomerList(_core, "1");
            //    frm.ucCustomerList.Browsable = true;
            //    DialogResult res = frm.ShowDialog();
            //    if ((res == System.Windows.Forms.DialogResult.OK))
            //    {
            //        numTab4FamilyCustNo.EditValue = frm.ucCustomerList.SelectedRow["CustomerNo"];
            //        txtTab4LastName.EditValue = frm.ucCustomerList.SelectedRow["CORPORATENAME"];
            //        txtTab4RegisterNo.EditValue = frm.ucCustomerList.SelectedRow["REGISTERNO"];
            //        txtTab4PassNo.EditValue = frm.ucCustomerList.SelectedRow["PASSNO"];
            //    }
            //}
        }
        private void btnTab5Find_Click(object sender, EventArgs e)
        {
            //HeavenPro.List.CustomerList frm = new List.CustomerList(_core, "0");
            //frm.ucCustomerList.Browsable = true;
            //DialogResult res = frm.ShowDialog();
            //if ((res == System.Windows.Forms.DialogResult.OK))
            //{
            //    numTab5CustNo.EditValue = frm.ucCustomerList.SelectedRow["CUSTOMERNO"];
            //    txtTab5LastName.EditValue = frm.ucCustomerList.SelectedRow["LASTNAME"];
            //    txtTab5MiddleName.EditValue = frm.ucCustomerList.SelectedRow["MIDDLENAME"];
            //    txtTab5FirstName.EditValue = frm.ucCustomerList.SelectedRow["FIRSTNAME"];
            //    txtTab5RegisterNo.EditValue = frm.ucCustomerList.SelectedRow["REGISTERNO"];
            //    txtTab5PassNo.EditValue = frm.ucCustomerList.SelectedRow["PASSNO"];
            //}
        }
        private void btnTab6Find_Click(object sender, EventArgs e)
        {
            //HeavenPro.List.CustomerList frm = new List.CustomerList(_core, "0");
            //frm.ucCustomerList.Browsable = true;
            //DialogResult res = frm.ShowDialog();
            //if ((res == System.Windows.Forms.DialogResult.OK))
            //{
            //    numTab6CustNo.EditValue=frm.ucCustomerList.SelectedRow["CUSTOMERNO"];
            //    txtTab6LastName.EditValue = frm.ucCustomerList.SelectedRow["LASTNAME"];
            //    txtTab6MiddleName.EditValue = frm.ucCustomerList.SelectedRow["MIDDLENAME"];
            //    txtTab6FirstName.EditValue = frm.ucCustomerList.SelectedRow["FIRSTNAME"];
            //    txtTab6RegisterNo.EditValue = frm.ucCustomerList.SelectedRow["REGISTERNO"];
            //    txtTab6PassNo.EditValue = frm.ucCustomerList.SelectedRow["PASSNO"];
            //}
        }
        private void btnDirectorFind_Click(object sender, EventArgs e)
        {
            //HeavenPro.List.CustomerList frm = new List.CustomerList(_core, "0");
            //frm.ucCustomerList.Browsable = true;
            //DialogResult res = frm.ShowDialog();
            //if ((res == System.Windows.Forms.DialogResult.OK))
            //{
            //    if (frm.ucCustomerList.SelectedRow != null)
            //    {
            //        txtTabDirLastName.EditValue = frm.ucCustomerList.SelectedRow["LASTNAME"];
            //        txtTabDirMiddleName.EditValue = frm.ucCustomerList.SelectedRow["MIDDLENAME"];
            //        txtTabDirFirstName.EditValue = frm.ucCustomerList.SelectedRow["FIRSTNAME"];
            //        numTabDirRegisterNo.EditValue = frm.ucCustomerList.SelectedRow["REGISTERNO"];
            //        numTabDirPassNo.EditValue = frm.ucCustomerList.SelectedRow["PASSNO"];
            //        txtTabDirPosition.EditValue = frm.ucCustomerList.SelectedRow["POSITION"];
            //    }
            //}
        }
        private void btnGeneralFind_Click(object sender, EventArgs e)
        {
            
        }
        private void btnContractFind_Click(object sender, EventArgs e)
        {
            InfoPos.List.ContractList frm = new InfoPos.List.ContractList(_core);
            frm.ucContractList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                if (frm.ucContractList.SelectedRow != null)
                {
                    numContractNo.EditValue=frm.ucContractList.SelectedRow["ContractNo"];
                }
            }
        }
    }
}
