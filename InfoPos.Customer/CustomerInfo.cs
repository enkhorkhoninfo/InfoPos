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
    public partial class CustomerInfo : Form
    {
        #region [ Variables ]
            Core.Core _core;
            string EventType; 
            long _customerid;
            long _customerno;
            int TypeCount;
            bool loadAddress;
            bool loadNote;
            int EditValueClassCode;
            int _CLASSCODE;
            int PrivNo = 310125;
            string FilterSubIndustry = "";
            DataTable DTNote;
            DataRow dr = null;
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

            int[] C9Hide = { 0, 1 };
            int[] C9Caption = { 2, 3, 4, 5 };
            string[] C9CaptionValue = { "Гүйлгээ хийсэн огноо", "Гүйлгээ хийсэн цаг, огноо", "Хэрэглэгчийн дугаар", "Товч дүгнэлт" };
        #endregion
        #region [ Load ]
            public CustomerInfo(Core.Core core, long customerid, int classcode)
                {
                    InitializeComponent();
                    _core = core;
                    _customerid = customerid;
                    _CLASSCODE = classcode;
                    Init();
                    ucCustGeneral.FieldLinkSetSaveState();
                }
            private void CustomerInfo_Load(object sender, EventArgs e)
                {
                    FormUtility.RestoreStateGrid(appname, formname, ref gvwNote);
                    TypeCount = 0;
                    #region [Selecting All Tab Pages before for Lookupedits]
                for (int i = 0; i < xtraTabGeneral.TabPages.Count; i++)
                {
                    xtraTabGeneral.SelectedTabPageIndex = i;
                }
                xtraTabGeneral.SelectedTabPageIndex = 0;
            #endregion
                    DS = DataSetComboAll();
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
                    }
                    InitCombos();
                    if (_customerid == 0)
                    {
                        cboTypeCode.ItemIndex = 0;
                    }
                    LoadFunction();
                    #region[форматжуулалт]
                        appname = _core.ApplicationName;
                        formname = "CustomerProp." + this.Name;
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
                    ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
                    ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
                    ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
                    ucCustGeneral.FieldLinkAdd("txtMiddleName", 0, "MiddleName", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtFirstName", 0, "FirstName", "", false, false);
                    ucCustGeneral.FieldLinkAdd("txtLastName", 0, "LastName", "", true, false);
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
                    ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
                    ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
                    ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);

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

                FormUtility.LookUpEdit_SetList(ref cboTypeCode, 1, "ИРГЭН");
                FormUtility.LookUpEdit_SetList(ref cboTypeCode, 2, "БАЙГУУЛЛАГА");

                FormUtility.LookUpEdit_SetList(ref cboSex, 0, "Эр");
                FormUtility.LookUpEdit_SetList(ref cboSex, 1, "Эм");

                FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Гэрээ хийгээгүй");
                FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Гэрээ хийсэн");

                #endregion
                #region[ Хаяг ]
                SetCombo("SUBDISTCODE", "NAME", cboSubDist, DS.Tables["CUSTSUBDISTRICT"], new int[] { 1, 2 });
                SetCombo("DISTCODE", "NAME", cboDist, DS.Tables["CUSTDISTRICT"], new int[] { 1 });
                SetCombo("CITYCODE", "NAME", cboCity, DS.Tables["CUSTCITY"], null);

                #endregion

            }
        }
            private void xtraTabCustomer_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
                {
                    try
                    {
                        if (txtCustomerNo.Text == "")
                        {
                            e.Cancel = true;
                        }
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
                                FormUtility.RestoreStateGrid(appname, formname, ref gvwAddress);
                            }
                            break;
                        case 2:
                            if (!loadNote)
                            {
                                EventType = "RefreshNote";
                                RefreshNote();
                                FormUtility.RestoreStateGrid(appname, formname, ref gvwNote);
                            }
                            DTNote = AddData(Static.ToLong(txtCustomerNo.EditValue), 120051);
                            break;
                    }
                }
        #endregion
        #region [ Genereal data ]
            int TxnCodeT1Select = 310125;
            int TxnCodeT1SubSelect = 310126;
            int TxnCodeT1Save = 310127;
            int TxnCodeT1Edit = 310128;
            int TxnCodeT1Delete = 310129;
            void ucCustGeneral_EventDataChanged()
            {
                return;
            }
            void ucCustGeneral_EventAddAfter()
            {
                try
                {
                    ucCustGeneral.ToggleShowEdit = false;
                    ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboTypeCode, 1);
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
                TypeCount = 1;
                cboTypeCode.Enabled=false;
                cboRegisterMask.Enabled = true;
                txtRegisterNo.Enabled = true;
                object[] Value = new object[25 ];
                _customerno = Static.ToLong(txtCustomerNo.EditValue);
                if (EditValueClassCode == 0)
                {
                    #region[ Value хувь хүн]
                    Value[0] = _customerno;                       //CustomerNo
                    Value[1] = 0;                                 //ClassCode
                    Value[2] = Static.ToInt(cboTypeCode.EditValue);    //TypeCode
                    Value[3] = txtFirstName.EditValue;                 //FirstName
                    Value[4] = txtLastName.EditValue;                  //LastName
                    Value[5] = txtMiddleName.EditValue;                //MiddleName
                    Value[6] = "";                                //CorporateName
                    Value[7] = txtRegisterNo.EditValue;               //RegisterNo
                    Value[8] = txtPassNo.EditValue;                   //PassNo
                    Value[9] = Static.ToInt(cboSex.EditValue);        //Sex
                    Value[10] = Static.ToDate(dteBirthDay.EditValue);  //BirthDay
                    Value[11] = txtCompany.EditValue;                  //Company
                    Value[12] = txtPosition.EditValue;                 //Position
                    Value[13] = mmoExperience.EditValue;               //Experience
                    Value[14] = txtEmail.EditValue;                    //Email
                    Value[15] = txtTelephone.EditValue;                //Telephone
                    Value[16] = txtMobile.EditValue;                   //Mobile
                    Value[17] = txtHomePhone.EditValue;                //HomePhone
                    Value[18] = txtFax.EditValue;                      //Fax
                    Value[19] = "";                               //WebSite
                    Value[20] = cboBranch.EditValue;              //Branch                
                    Value[21] = cboStatus.EditValue;              //Status
                    Value[22] = Static.ToStr(txtDriverNo.EditValue);  //DriverNo
                    Value[23] = Convert.ToDateTime(_core.TxnDate);      //CreateTime
                    Value[24] = Static.ToInt(_core.RemoteObject.User.UserNo);     //CreateUser
                    #endregion
                }
                else
                {
                    #region[ Value байгууллага]

                    Value[0] = _customerno;                       //CustomerNo
                    Value[1] = 1;                                 //ClassCode
                    Value[2] = Static.ToInt(cboTypeCode.EditValue);    //TypeCode
                    Value[3] = "";                                //FirstName
                    Value[4] = "";                                //LastName
                    Value[5] = "";                                //MiddleName
                    Value[6] = txtCorporateName.EditValue;             //CorporateName
                    Value[7] = txtRegisterNo.EditValue;               //RegisterNo
                    Value[8] = txtPassNo.EditValue;                   //PassNo
                    Value[9] = "";                               //Sex
                    Value[10] = Static.ToDate(dteBirthDay.EditValue);  //BirthDay
                    Value[11] = "";                               //Company
                    Value[12] = "";                               //Position
                    Value[13] = mmoExperience.EditValue;               //Experience
                    Value[14] = txtCorEmail.EditValue;                 //Email
                    Value[15] = txtCorPhone.EditValue;                 //Telephone
                    Value[16] = "";                               //Mobile
                    Value[17] = "";                               //HomePhone
                    Value[18] = txtCorFax.EditValue;                   //Fax
                    Value[19] = txtCorWebSite.EditValue;               //WebSite
                    Value[20] = cboBranch.EditValue;                   //Branch                
                    Value[21] = cboStatus.EditValue;                   //Status
                    Value[22] = 0;                                      //DriverNo
                    Value[23] = Convert.ToDateTime(_core.TxnDate);      //CreateTime
                    Value[24] = Static.ToInt(_core.RemoteObject.User.UserNo);     //CreateUser
                    #endregion
                }
                OldValue = Value;
            }
            void ucCustGeneral_EventSave(bool isnew, ref bool cancel)
            {
                string err = "";
                Control cont = null;
                TypeCount = 0;
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
                try
                {
                    Result res = new Result();
                    try
                    {
                        if (customerid != 0)
                        {
                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeT1SubSelect, TxnCodeT1SubSelect, new object[] { customerid });

                            if (res.ResultNo == 0)
                            {
                                dr = res.Data.Tables[0].Rows[0];
                                ucCustGeneral.DataSource = res.Data;
                                ucCustGeneral.FieldLinkSetValues();
                                if (txtCustomerNo.Text != "")
                                {
                                    cboRegisterMask.Enabled = false;
                                    txtRegisterNo.Enabled = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                            }
                            if (txtCustomerNo.Text == "")
                            {
                                btnCreateCust.Visible = false;
                                btnCreateIssue.Visible = false;
                            }
                            else
                            {
                                btnCreateCust.Visible = true;
                                btnCreateIssue.Visible = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            void SaveCustomerData(bool isnew, ref bool cancel)
            {
                Result res = new Result();
                object[] obj = new object[25];
                string msg = "";
                try
                {
                    _customerno = Static.ToLong(txtCustomerNo.EditValue);
                    if (EditValueClassCode == 0)
                    {
                        #region[ obj хувь хүн]
                        obj[0] = _customerno;                       //CustomerNo
                        obj[1] = 0;                                 //ClassCode
                        obj[2] = Static.ToInt(cboTypeCode.EditValue);    //TypeCode
                        obj[3] = txtFirstName.EditValue;                 //FirstName
                        obj[4] = txtLastName.EditValue;                  //LastName
                        obj[5] = txtMiddleName.EditValue;                //MiddleName
                        obj[6] = "";                                //CorporateName
                        obj[7] = txtRegisterNo.EditValue;               //RegisterNo
                        obj[8] = txtPassNo.EditValue;                   //PassNo
                        obj[9] = Static.ToInt(cboSex.EditValue);        //Sex
                        obj[10] = Static.ToDate(dteBirthDay.EditValue);  //BirthDay
                        obj[11] = txtCompany.EditValue;                  //Company
                        obj[12] = txtPosition.EditValue;                 //Position
                        obj[13] = mmoExperience.EditValue;               //Experience
                        obj[14] = txtEmail.EditValue;                    //Email
                        obj[15] = txtTelephone.EditValue;                //Telephone
                        obj[16] = txtMobile.EditValue;                   //Mobile
                        obj[17] = txtHomePhone.EditValue;                //HomePhone
                        obj[18] = txtFax.EditValue;                      //Fax
                        obj[19] = "";                               //WebSite
                        obj[20] = cboBranch.EditValue;              //Branch                
                        obj[21] = cboStatus.EditValue;              //Status
                        obj[22] = Static.ToStr(txtDriverNo.EditValue);  //DriverNo
                        obj[23] = Convert.ToDateTime(_core.TxnDate);      //CreateTime
                        obj[24] = Static.ToInt(_core.RemoteObject.User.UserNo);     //CreateUser
                        #endregion
                    }
                    else
                    {
                        #region[ obj байгууллага]
                        obj[0] = _customerno;                       //CustomerNo
                        obj[1] = 1;                                 //ClassCode
                        obj[2] = Static.ToInt(cboTypeCode.EditValue);    //TypeCode
                        obj[3] = "";                                //FirstName
                        obj[4] = "";                                //LastName
                        obj[5] = "";                                //MiddleName
                        obj[6] = txtCorporateName.EditValue;             //CorporateName
                        obj[7] = txtRegisterNo.EditValue;               //RegisterNo
                        obj[8] = txtPassNo.EditValue;                   //PassNo
                        obj[9] = "";                               //Sex
                        obj[10] = Static.ToDate(dteBirthDay.EditValue);  //BirthDay
                        obj[11] = "";                               //Company
                        obj[12] = "";                               //Position
                        obj[13] = mmoExperience.EditValue;               //Experience
                        obj[14] = txtCorEmail.EditValue;                 //Email
                        obj[15] = txtCorPhone.EditValue;                 //Telephone
                        obj[16] = "";                               //Mobile
                        obj[17] = "";                               //HomePhone
                        obj[18] = txtCorFax.EditValue;                   //Fax
                        obj[19] = txtCorWebSite.EditValue;               //WebSite
                        obj[20] = cboBranch.EditValue;                   //Branch                
                        obj[21] = cboStatus.EditValue;                   //Status
                        obj[22] = 0;                                      //DriverNo
                        obj[23] = Convert.ToDateTime(_core.TxnDate);      //CreateTime
                        obj[24] = Static.ToInt(_core.RemoteObject.User.UserNo);     //CreateUser
                        #endregion
                    }
                    object[] FieldName = {"CustomerNo","ClassCode","TypeCode","FirstName","LastName","MiddleName","CorporateName",
                                            "RegisterNo","PassNo","Sex","BirthDay","Company","Position","Experience","Email","Telephone",
                                             "Mobile","HomePhone","Fax","WebSite","BranchNo","Status","DriverNo","CreateDate","CreateUser"};
                    if (isnew)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeT1Save, TxnCodeT1Save, new object[] { obj, FieldName });
                        msg = "Амжилттай нэмлээ .";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeT1Edit, TxnCodeT1Edit, new object[] { obj, OldValue, FieldName });
                        msg = "Амжилттай засварлалаа .";
                    }
                    if (res.ResultNo == 0)
                    {
                        if (isnew)
                        {
                            txtCustomerNo.EditValue = Static.ToStr(res.Param[0]);
                            _customerid = Static.ToLong(txtCustomerNo.EditValue);
                            GeneralRefreshData(_customerid);
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
            private void cboTypeCode_EditValueChanged(object sender, EventArgs e)
            {
                if (_customerid == 0)
                {
                    if (((DevExpress.XtraEditors.LookUpEdit)sender).GetSelectedDataRow() != null)
                    {
                        DataRow DRow = ((DataRowView)((DevExpress.XtraEditors.LookUpEdit)sender).GetSelectedDataRow()).Row;
                        if (DRow[0].ToString() == "1")
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
                        if (DRow[0].ToString() == "2")
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
                        FormUtility.LookUpEdit_SetValue(ref cboBranch, _core.RemoteObject.User.BranchCode);
                        cboDriverNoMask.ItemIndex = 0;
                        cboStatus.ItemIndex = 0;
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
            int TxnCodeT2Select = 310135;
            int TxnCodeT2Save = 310137;
            int TxnCodeT2Edit = 310138;
            int TxnCodeT2Delete = 310139;
            void ucAddress_EventDelete()
            {
                EventType = "RefreshAddress";
                EventDelete(Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue), TxnCodeT2Delete, TxnCodeT2Select, ucAddress, gvwAddress, C1Hide, C1Caption, C1CaptionValue);
                RefreshAddress();
            }
            void ucAddress_EventSave(bool isnew, ref bool cancel)
            {
                string err = "";
                Control cont = null;
                if (ucAddress.FieldValidate(ref err, ref cont) == true)
                {
                    object[] obj = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue), Static.ToInt(cboCity.EditValue), Static.ToInt(cboDist.EditValue), Static.ToInt(cboSubDist.EditValue), txtAdd.EditValue, (chkAddrCurrent.Checked ? 1 : 0), txtApartment.EditValue};
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
                object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue), Static.ToInt(cboCity.EditValue), Static.ToInt(cboDist.EditValue), Static.ToInt(cboSubDist.EditValue), txtAdd.EditValue, (chkAddrCurrent.Checked ? 1 : 0), txtApartment.EditValue};
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
                        ucAddress.DataSource = null;
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 310135, 310135, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
                        if (res.ResultNo == 0)
                        {
                            ucAddress.DataSource = res.Data;
                            ucAddress.FieldLinkSetValues();
                            ucAddress.FieldLinkSetSaveState();
                            SetAddress();
                            loadAddress = false;
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
        #region[ Note ]
            int TxnCodeT10Select = 310130;
            int TxnCodeT10Save = 310132;
            int TxnCodeT10Edit = 310133;
            int TxnCodeT10Delete = 310134;
            void ucNote_EventEdit(ref bool cancel)
            {
                DataRow row = gvwNote.GetFocusedDataRow();
                object[] Value = { Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(row["SeqNo"]), Static.ToDate(row["TxnDate"]), Static.ToDate(row["TxnDate"]), Static.ToInt(numTab10UserNo.EditValue), mmeTab10Note.EditValue };
                OldValue = Value;
            }
            void ucNote_EventDelete()
            {
                EventType = "RefreshNote";
                EventDelete(Static.ToLong(txtCustomerNo.EditValue), Static.ToLong(numTab10SeqNo.EditValue), TxnCodeT10Delete, TxnCodeT10Select, ucNote, gvwNote, C9Hide, C9Caption, C9CaptionValue);
            }
            void ucNote_EventSave(bool isnew, ref bool cancel)
            {
                string err = "";
                Control cont = null;
                EventType = "RefreshNote";
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
                        ucNote.DataSource = null;
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 310130, 310130, new object[] { Static.ToLong(txtCustomerNo.EditValue) });
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
                gvwNote.Columns[2].Caption = "Гүйлгээ хийсэн Огноо";
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
        #endregion
        #region[ Customer Function ]
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

                        //if (CustomerID != 0 && SeqID != 0)
                        //{
                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, TxnCodeDel, TxnCodeDel, new object[] { CustomerID, SeqID });
                            if (res.ResultNo == 0)
                            {
                                switch (EventType)
                                {
                                    case "RefreshAdress":
                                        RefreshAddress();
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
                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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
                    string[] names = new string[] { "BRANCH", "CUSTOMERTYPECODE", "REGISTERMASK", "PASSMASK", "CUSTCITY", "CUSTDISTRICT", "CUSTSUBDISTRICT", "INDUSTRY", "SUBINDUSTRY", "DRIVERMASK"};
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
                #region[Label]
                labelControl2.Text = "Ургийн овог :";
                labelControl5.Text = "Эцэг эхийн нэр :";
                labelControl10.Text = "Регистерийн дугаар :";
                labelControl4.Text = "Төрсөн огноо :";
                labelControl13.Text = "Үнэмлэхний дугаар :";

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
                #endregion
            }
            void FieldIndividual()
            {
                ucCustGeneral.FieldLinkClear();
                ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
                ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
                ucCustGeneral.FieldLinkAdd("txtMiddleName", 0, "MiddleName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtFirstName", 0, "FirstName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtLastName", 0, "LastName", "", true, false);
                ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", false, false);
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
                ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
            }
            void IndividualTrue()
            {
                Tab2.Text = "Ажил эрхлэлтийн байдал";
                #region[Label]
                labelControl2.Text = "Ургийн овог :";
                labelControl5.Text = "Эцэг эхийн нэр :";
                labelControl10.Text = "Регистерийн дугаар :";
                labelControl4.Text = "Төрсөн огноо :";
                labelControl13.Text = "Үнэмлэхний дугаар :";

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
                FormUtility.LookUpEdit_SetList(ref cboTypeCode, 1, "ИРГЭН");
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
                #endregion
            }
            void FieldIndividualTrue()
            {
                ucCustGeneral.FieldLinkClear();
                ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
                ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
                ucCustGeneral.FieldLinkAdd("txtMiddleName", 0, "MiddleName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtFirstName", 0, "FirstName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtLastName", 0, "LastName", "", true, false);
                //ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", false, false);
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
                ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
            }
            void Corporate()
            {
                FieldCorporate();
                Tab2.Text = "Байгууллагын мэдээлэл";

                #region[Label]
                labelControl2.Text = "Байгууллагын нэр :";
                labelControl5.Text = "Байгууллагын нэр2 :";
                labelControl10.Text = "Байгууллагын регистер :";
                labelControl4.Text = "Байгуулагдсан огноо :";
                labelControl13.Text = "Улсын бүртгэлийн дугаар :";
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
                #endregion
            }
            void FieldCorporate()
            {
                ucCustGeneral.FieldLinkClear();
                ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorporateName", 0, "CorporateName", "", true, false);
                ucCustGeneral.FieldLinkAdd("txtCorporateName2", 0, "CorporateName2", "", false, false);
                //ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtPassNo", 0, "PassNo", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
                ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
                ucCustGeneral.FieldLinkAdd("dteBirthDay", 0, "BirthDay", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorPhone", 0, "Telephone", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorFax", 0, "Fax", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorEmail", 0, "Email", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorWebSite", 0, "WebSite", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboIndustry", 0, "InduTypeCode", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboSubIndustry", 0, "InduSubTypeCode", "", false, false);
                ucCustGeneral.FieldLinkAdd("mmoExperience", 0, "Experience", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirFirstName", 0, "DirFirstName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirLastName", 0, "DirLastName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirMiddleName", 0, "DirMiddleName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirRegisterNo", 0, "DirRegisterNo", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirPassNo", 0, "DirPassNo", "", false, false);
                ucCustGeneral.FieldLinkAdd("dteDirBirthDay", 0, "DirBirthDay", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboDirSex", 0, "DirSex", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
            }
            void CorporateTrue()
            {
                Tab2.Text = "Байгууллагын мэдээлэл";
                #region[Label]
                labelControl2.Text = "Байгууллагын нэр :";
                labelControl5.Text = "Байгууллагын нэр2 :";
                labelControl10.Text = "Байгууллагын регистер :";
                labelControl4.Text = "Байгуулагдсан огноо :";

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

                FormUtility.LookUpEdit_SetList(ref cboTypeCode, 2, "БАЙГУУЛЛАГА");
                cboRegisterMask.ItemIndex = 0;
                cboPassMask.ItemIndex = 0;
                cboDriverNoMask.ItemIndex = 0;
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
                #endregion
            }
            void FieldCorporateTrue()
            {
                ucCustGeneral.FieldLinkClear();
                ucCustGeneral.FieldLinkAdd("txtCustomerNo", 0, "CustomerNo", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorporateName", 0, "CorporateName", "", true, false);
                ucCustGeneral.FieldLinkAdd("txtCorporateName2", 0, "CorporateName2", "", false, false);
                //ucCustGeneral.FieldLinkAdd("txtRegisterNo", 0, "RegisterNo", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtPassNo", 0, "PassNo", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
                ucCustGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
                ucCustGeneral.FieldLinkAdd("dteBirthDay", 0, "BirthDay", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorPhone", 0, "Telephone", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorFax", 0, "Fax", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorEmail", 0, "Email", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCorWebSite", 0, "WebSite", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboIndustry", 0, "InduTypeCode", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboSubIndustry", 0, "InduSubTypeCode", "", false, false);
                ucCustGeneral.FieldLinkAdd("mmoExperience", 0, "Experience", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirFirstName", 0, "DirFirstName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirLastName", 0, "DirLastName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirMiddleName", 0, "DirMiddleName", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirRegisterNo", 0, "DirRegisterNo", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtDirPassNo", 0, "DirPassNo", "", false, false);
                ucCustGeneral.FieldLinkAdd("dteDirBirthDay", 0, "DirBirthDay", "", false, false);
                ucCustGeneral.FieldLinkAdd("cboDirSex", 0, "DirSex", "", false, false);
                ucCustGeneral.FieldLinkAdd("txtCreateDate", 0, "CreateDate", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
                ucCustGeneral.FieldLinkAdd("txtOldID", 0, "OldID", "", false, false, true);
            }
            void LoadFunction()
            {
                if (_core.Resource != null)
                {
                    ucCustGeneral.Resource = _core.Resource;
                    ucAddress.Resource = _core.Resource;
                    ucNote.Resource = _core.Resource;
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
        #endregion
        #region[nothing]
            private void CustomerProp_KeyDown(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Escape)
                        this.Close();
                }
            private void CustomerProp_FormClosing(object sender, FormClosingEventArgs e)
                {
                    FormUtility.SaveStateForm(appname, ref FormName);
                    FormUtility.SaveStateGrid(appname, formname, ref gvwAddress);
                    FormUtility.SaveStateGrid(appname, formname, ref gvwNote);
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
                                    MessageBox.Show(this, "Харилцагч үүсгэх боломжгүй", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            private void btnCreateCust_Click(object sender, EventArgs e)
                {
                    if (txtRegisterNo.Text == "")
                    {
                        MessageBox.Show("Регистрийн дугаараа оруулна уу.");                    
                    }
                    else
                    {
                        if (TypeCount == 0)
                        {
                            Result res = new Result();
                            try
                            {
                                if (txtCustomerNo.Text != "")
                                {
                                    object[] objCus = new object[1];
                                    objCus[0] = txtCustomerNo.EditValue;
                                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 310140, 310140, new object[] { objCus });
                                    if (res.ResultNo == 0)
                                    {
                                        MessageBox.Show("Амжилттай нэмлээ");
                                        int cCode;
                                        if (Static.ToInt(cboTypeCode.EditValue) == 1)
                                        { cCode = 0; }
                                        else
                                        { cCode = 1; }
                                        object[] obj = new object[4];
                                        obj[0] = _core;
                                        obj[1] = Static.ToLong(txtCustomerNo.EditValue);
                                        obj[2] = Static.ToLong(cCode);
                                        obj[3] = Static.ToInt(cboTypeCode.EditValue);
                                        EServ.Shared.Static.Invoke("HeavenPro.Customer.dll", "HeavenPro.Customer.Main", "CallCustomerProp", obj);
                                    }
                                    else
                                    {
                                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                                    }
                                }
                                else
                                {
                                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                    this.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Харилцагч үүсгэхээсээ өмнө харилцагчаа хадгална уу.");
                        }
                    }
                }
        #endregion
          private void btnCreateIssue_Click(object sender, EventArgs e)
        {
            //if (txtCustomerNo.EditValue != null)
            //{
            //    HeavenPro.Issue.FormMain frm = new Issue.FormMain(_core, Static.ToLong(txtCustomerNo.EditValue), dr);
            //    frm.MdiParent = _core.MainForm;
            //    frm.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Үндсэн мэдээлэлээ эхэлж үүсгэнэ үү");
            //}
        }
    }
}