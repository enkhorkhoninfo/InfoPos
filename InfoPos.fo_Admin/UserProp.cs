using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

using ISM.Template;
using EServ.Shared;
using InfoPos.Core;

namespace HeavenPro
{
    public partial class UserProp : Form
    {
        #region [ Variables ]
        Core.Core _core;
        int _userno;
        string _upassword;
        int PrivNo = 110100;
        string appname = "", formname = "";
        Form FormName = null;
        object[] obj = new object[21];
        object[] fieldValue = new object[19];
        object[] oldValue = new object[19];
        Result res = new Result();
        DataRow _dr = null;
        DataSet DSAgent;
        //DataSet DS;
        string msg = "";
        bool loadAddress = true;
        bool loadFamily = true;
        object OldValue;
        string errorname = "";
        int creattype = 0;
        string DefaultPass = "";
        string MaskValue;
        #endregion
        #region [ Init ]
        public UserProp(Core.Core core) : this(core, 0)
        {

        }
        public UserProp(Core.Core core, int UserNo)
        {
            InitializeComponent();
            _core = core;
            _userno = UserNo;
            InitEvents();
            InitToggles();
            picData.Text = "Зураг харах";
            RefreshPass();
        }
        private void UserProp_Load(object sender, EventArgs e)
        {
            this.Show();
            appname = _core.ApplicationName;
            formname = "Admin." + this.Name;

            FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwPriv);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwSupervisor);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwAddress);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwFamily);
            if (_userno != 0)
            {
                RefreshData(_userno);
                ucUserProp.FieldLinkSetSaveState();
            }
            else
            {
                ucUserProp.FieldLinkSetNewState();
            }
            RefreshPass();


            oldValue[0] = Static.ToInt(numUserNo.EditValue); // UserNo
            oldValue[1] = txtUserFName.EditValue;   //UserFname
            oldValue[2] = txtUserLName.EditValue;  //UserLname
            oldValue[3] = txtUserFName2.EditValue;  //UserFname2
            oldValue[4] = txtUserLName2.EditValue; //UserLname2
            oldValue[5] = txtRegister.EditValue; //RegisterNo
            oldValue[6] = txtPosition.EditValue; //Position
            oldValue[7] = (chkStatus.Checked ? 0 : 9); //Status
            oldValue[8] = Static.ToInt(cboBranch.EditValue); //BranchNo
            oldValue[9] = Static.ToInt(cboLevel.EditValue); //UserLevel
            oldValue[10] = Static.ToStr(txtPassword.OldEditValue);
            oldValue[11] = Static.ToInt(cboUserType.EditValue); //UserType
            oldValue[12] = Static.ToStr(txtEMail.Text); //Email
            oldValue[13] = txtMobile.EditValue; //Mobile
            oldValue[14] = Static.ToInt(cboLogintype.EditValue); //Logintype
            oldValue[15] = 0;
            oldValue[16] = Static.ToDate(_core.TxnDate); //Change Pass Date
            oldValue[17] = Static.ToInt(cboAgentCorp.EditValue); //agentcorp
            oldValue[18] = Static.ToInt(cboAgentBranch.EditValue); //agentbranch

        }
        private void InitEvents()
        {
            #region[General tab1]
            ucUserProp.EventSave += new ucTogglePanel.delegateEventSave(ucUserProp_EventSave);
            ucUserProp.EventExit += new ucTogglePanel.delegateEventExit(ucUserProp_EventExit);
            ucUserProp.EventDelete += new ucTogglePanel.delegateEventDelete(ucUserProp_EventDelete);

            ISM.Template.FormUtility.SetFormatGrid(ref gvwAddress, false);

            ucUserProp.FieldLinkAdd("numUserNo", 0, "UserNo", "", true, true);
            ucUserProp.FieldLinkAdd("chkStatus", 0, "Status", "", true, false);
            ucUserProp.FieldLinkAdd("cboLevel", 0, "ulevel", "", true, false);
            ucUserProp.FieldLinkAdd("txtUserFName", 0, "UserFName", "", true, false);
            ucUserProp.FieldLinkAdd("txtUserFName2", 0, "UserFName2", "", false, false);
            ucUserProp.FieldLinkAdd("txtUserLName", 0, "UserLName", "", true, false);
            ucUserProp.FieldLinkAdd("txtUserLName2", 0, "UserLName2", "", false, false);
            ucUserProp.FieldLinkAdd("txtRegister", 0, "RegisterNo", "", true, false);
            ucUserProp.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
            ucUserProp.FieldLinkAdd("txtPosition", 0, "Position", "", false, false);
            ucUserProp.FieldLinkAdd("cboUserType", 0, "UserType", "", true, false);
            ucUserProp.FieldLinkAdd("cboLogintype", 0, "Logintype", "", true, false);
            ucUserProp.FieldLinkAdd("txtEMail", 0, "EMail", "", true, false);
            ucUserProp.FieldLinkAdd("txtMobile", 0, "Mobile", "", true, false);
            ucUserProp.FieldLinkAdd("txtPassword", 0, "UPassword", "", true, false);
            ucUserProp.FieldLinkAdd("cboAgentCorp", 0, "AGENTCORPNO", "", false, false);
            ucUserProp.FieldLinkAdd("cboAgentBranch", 0, "AGENTBRANCHNO", "", false, false);
                        
            InitCombos();
            InitCombosStatic();

            ucUserProp.Resource = _core.resource;


            ucUserProp.ToggleShowDelete = true;
            ucUserProp.ToggleShowEdit = true;
            ucUserProp.ToggleShowExit = true;
            ucUserProp.ToggleShowNew = true;
            ucUserProp.ToggleShowReject = true;
            ucUserProp.ToggleShowSave = true;

            ucUserProp.DataSource = null;
            ucUserProp.FieldLinkSetSaveState();
            #endregion[]
            #region[Address tab]
            ucAddress.EventSave += new ucTogglePanel.delegateEventSave(ucAddress_EventSave);
            ucAddress.EventDelete += new ucTogglePanel.delegateEventDelete(ucAddress_EventDelete);
            ucAddress.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucAddress_EventDataChanged);
            ucAddress.EventEdit += new ucTogglePanel.delegateEventEdit(ucAddress_EventEdit);
            ucAddress.EventExit += new ucTogglePanel.delegateEventExit(ucAddress_EventExit);


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
            ucAddress.Resource = _core.resource;

            //ucAddress.FieldLinkSetColumnCaption(0, "sdf");                               
  
            #endregion[]
            #region[Family tab]
            ucFamily.EventSave += new ucTogglePanel.delegateEventSave(ucFamily_EventSave);
            ucFamily.EventDelete += new ucTogglePanel.delegateEventDelete(ucFamily_EventDelete);
            ucFamily.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucFamily_EventDataChanged);
            ucFamily.EventEdit += new ucTogglePanel.delegateEventEdit(ucFamily_EventEdit);
            //ucFamily.EventExit += new ucTogglePanel.delegateEventExit(ucFamily_EventExit);

            ucFamily.ToggleShowDelete = true;
            ucFamily.ToggleShowEdit = true;
            ucFamily.ToggleShowExit = false;
            ucFamily.ToggleShowNew = true;
            ucFamily.ToggleShowReject = true;
            ucFamily.ToggleShowSave = true;

            ucFamily.FieldLinkAdd("numTab4SeqNo", 0, "SeqNo", "", false, true, true);
            ucFamily.FieldLinkAdd("cboTab4FamilyType", 0, "FamilyType", "", true, false);
            ucFamily.FieldLinkAdd("txtTab4FirstName", 0, "FirstName", "", true, false);
            ucFamily.FieldLinkAdd("txtTab4LastName", 0, "LastName", "", true, false);
            ucFamily.FieldLinkAdd("txtTab4MiddleName", 0, "MiddleName", "", true, false);
            ucFamily.FieldLinkAdd("txtTab4RegisterNo", 0, "RegisterNo", "", true, false);
            ucFamily.FieldLinkAdd("txtTab4PassNo", 0, "PassNo", "", false, false);
            ucFamily.FieldLinkAdd("txtTab4Email", 0, "Email", "", false, false);
            ucFamily.FieldLinkAdd("numTab4Telephone", 0, "Telephone", "", false, false);
            ucFamily.FieldLinkAdd("numTab4Mobile", 0, "Mobile", "", false, false);
            ucFamily.Resource = _core.resource;
            #endregion[]
        }
        private void InitCombos()
        {
            try
            {
                Result res = new Result();
                ArrayList Tables = new ArrayList();
                DataTable DT = null;
                string msg = "";

                DictUtility.PrivNo = PrivNo;
                string[] name = new string[] { "BRANCH", "REGISTERMASK", "CUSTSUBDISTRICT", "CUSTDISTRICT", "CUSTCITY", "AGENTBRANCH", "AGENTCORP", "FAMILYTYPECODE", "PASSMASK" };
                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д Branch оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboBranch, DT, "BRANCH", "NAME", "", new int[] { 2, 3, 4 });
                }
                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д REGISTERMASK оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboRegisterMask, DT, "MASKID", "MASKNAME");
                }
                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д CUSTSUBDISTRICT оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboSubDist, DT, "DISTCODE", "NAME");
                }
                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д CUSTDISTRICT оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboDist, DT, "DISTCODE", "NAME");
                }
                DT = (DataTable)Tables[4];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д CUSTCITY оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboCity, DT, "CITYCODE", "NAME");
                }
                DT = (DataTable)Tables[5];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д AGENTBRANCH оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboAgentBranch, DT, "AGENTBRANCHNO", "NAME");
                }
                DT = (DataTable)Tables[6];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д AGENTCORP оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboAgentCorp, DT, "AGENTCORPNO", "NAME");
                }
                DT = (DataTable)Tables[7];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д FAMILYTYPECODE оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboTab4FamilyType, DT, "TYPECODE", "NAME");
                }
                DataSet DSAgentTmp = new DataSet();
                if (msg != "" && DT == null)
                    MessageBox.Show(msg);
                for (int i = 0; i < Tables.Count; i++)
                {
                    DataTable DTmp = ((DataTable)Tables[i]).Copy();
                    DTmp.TableName = name[i];
                    DSAgentTmp.Tables.Add(DTmp);
                }
                DSAgent = DSAgentTmp;

                SetCombo("MASKID", "MASKNAME", cboPass, DSAgent.Tables["PASSMASK"], new int[] { 2, 3 });
                SetCombo("MASKID", "MASKNAME", cboReg, DSAgent.Tables["REGISTERMASK"], new int[] { 2, 3 });
                string NameField = "";
                if (cboAgentCorp.EditValue != null && cboAgentCorp.EditValue != DBNull.Value)
                    NameField = "AGENTCORPNO=" + cboAgentCorp.EditValue;
                if (DSAgent.Tables["AGENTBRANCH"] != null)
                {
                    SetComboSub("AGENTBRANCHNO", "NAME", cboAgentBranch, DSAgent.Tables["AGENTBRANCH"], NameField, new int[] { 1 });
                }
                else
                {
                    MessageBox.Show("Dictionary-д AGENTBRANCH оруулаагүй байна.");
                }
            }
            catch(Exception ex)
            {
                
                MessageBox.Show(ex.Message+"Өгөгдлийн баазаас Dictionary олдсонгүй.");
            }
            

        }
        private void InitCombosStatic()
        {

            FormUtility.LookUpEdit_SetList(ref cboLevel, 0, "Түвшин 0");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 1, "Түвшин 1");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 2, "Түвшин 2");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 3, "Түвшин 3");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 4, "Түвшин 4");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 5, "Түвшин 5");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 6, "Түвшин 6");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 7, "Түвшин 7");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 8, "Түвшин 8");
            FormUtility.LookUpEdit_SetList(ref cboLevel, 9, "Түвшин 9");

            FormUtility.LookUpEdit_SetList(ref cboUserType, 0, "Үндсэн хэрэглэгч");
            FormUtility.LookUpEdit_SetList(ref cboUserType, 1, "Агент хэрэглэгч");


            FormUtility.LookUpEdit_SetList(ref cboLogintype, 0, "Олон Terminal-аас");
            FormUtility.LookUpEdit_SetList(ref cboLogintype, 1, "Нэг Terminal-аас");

            simpleButton1.Image = _core.resource.GetImage("small_browse");
            simpleButton8.Image = _core.resource.GetImage("small_save");



            return;
        }
        private void InitToggles()
        {
            #region[Address Toggle]
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
            #endregion[]
            #region [Family Toggle]
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
        }
        private void RefreshData(int puserno)
        {
            this.Show();
            //FormName = this;
            //ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            appname = _core.ApplicationName;
            formname = "Admin." + this.Name;            

            Result res = new Result();
            DataSet ds = null;
            try
            {
                if (_userno != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110101, 110101, new object[] { puserno });

                    if (res.ResultNo == 0)
                    {
                        int status = Static.ToInt(res.Data.Tables[0].Rows[0]["STATUS"]);
                        ucUserProp.DataSource = res.Data;
                        

                        ucUserProp.FieldLinkSetValues();
                        ds = res.Data;
                        _upassword = Static.ToStr(res.Data.Tables[0].Rows[0]["upassword"]);

                        if (status == 0)
                            chkStatus.Checked = true;
                        else
                            chkStatus.Checked = false;
                        //FormUtility.LookUpEdit_SetValue(ref cboLevel, Static.ToInt(ds.Tables[0].Rows[0]["ulevel"].ToString()));

                        Refresh_Priv(ds);
                        Refresh_Sup(ds);
                        RefreshAddress();
                        RefreshFamily();
                        DataSet DS = new DataSet();
                        Refresh_Picture(DS);                        
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
        private void RefreshPass()
        {
            DataRow _dr;
            Result res = new Result();

            res = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110113, 110113, null);
            try
            {
                if (res.ResultNo == 0)
                {
                    _dr = res.Data.Tables[0].Rows[0];
                    creattype = Static.ToInt(_dr["CREATETYPE"]);
                    DefaultPass = Static.ToStr(_dr["DEFAULTPASS"]);
                    MaskValue = Static.ToStr(_dr["MASKVALUE"]);
                    lblMask.Text = Static.ToStr(_dr["MASKDESCRIPTION"]);
                }
            }


            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void ActionChangePass()
        {
            try
            {
                    if (creattype == 0)
                    {
                        txtPassword.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                        txtPassword.Properties.Mask.EditMask = MaskValue;
                    }

                    if (creattype == 1)
                    {
                        txtPassword.Text = DefaultPass;
                        lblMask.Text = "Нууц үг random-аар үүссэн байна.";
                        txtPassword.Properties.PasswordChar = '*';
                        txtPassword.Properties.ReadOnly = true;
                    }

                if (creattype == 2)
                    {
                        txtPassword.Text = DefaultPass;
                        lblMask.Text = "Нууц үг Default утгаар сонгогдсон байна.";
                        txtPassword.Properties.PasswordChar = '*';
                        txtPassword.Properties.ReadOnly = true;
                    }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void Refresh_Priv(DataSet ds)
        {
            try
            {

                grdPriv.DataSource = ds.Tables[1];
                SetPrivData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetPrivData()
        {
            try
            {
                /*
                 * 0 status, 
                 * 1 groupid, 
                 * 2 groupname, 
                 * 3 expiredate
                 * 4 level1
                 * 5 level2
                 * 6 level3
                 * 7 level4
                 */
                gvwPriv.Columns[0].Caption = "Төлөв";
                gvwPriv.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                gvwPriv.Columns[1].Caption = "Бүлгийн дугаар";
                gvwPriv.Columns[1].OptionsColumn.AllowEdit = false;

                gvwPriv.Columns[2].Caption = "Бүлгийн нэр";
                gvwPriv.Columns[2].OptionsColumn.AllowEdit = false;
                gvwPriv.Columns[3].Caption = "Хүчинтэй хугацаа";

                gvwPriv.Columns[4].Caption = "Өөрийн бүртгэлийг";
                gvwPriv.Columns[4].ColumnEdit = CreateRepositoryCheckEdit();

                gvwPriv.Columns[5].Caption = "Зөвхөн өөрийн салбар";
                gvwPriv.Columns[5].ColumnEdit = CreateRepositoryCheckEdit();

                gvwPriv.Columns[6].Caption = "Өөрийн болон дэд салбар";
                gvwPriv.Columns[6].ColumnEdit = CreateRepositoryCheckEdit();

                gvwPriv.Columns[7].Caption = "Бүх салбарын хэмжээнд";
                gvwPriv.Columns[7].ColumnEdit = CreateRepositoryCheckEdit();

                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdPriv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetAddressData()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdAddress);
                gvwAddress.Columns[0].Caption = "Хэрэглэгчийн дугаар";                
                gvwAddress.Columns[0].Visible = false;
                gvwAddress.Columns[2].Caption = "Аймаг хотын бүртгэл";
                //gvwAddress.Columns[2].OptionsColumn.AllowEdit = false;
                gvwAddress.Columns[2].Visible = false;
                gvwAddress.Columns[3].Caption = "Аймаг хотын нэр";
                gvwAddress.Columns[3].OptionsColumn.AllowEdit = false;
                gvwAddress.Columns[4].Caption = "Сум дүүргийн бүртгэл";
                gvwAddress.Columns[4].Visible = false;
                gvwAddress.Columns[5].Caption = "Сум дүүргийн нэр";
                gvwAddress.Columns[5].OptionsColumn.AllowEdit = false;
                gvwAddress.Columns[6].Caption = "Баг хорооны бүртгэл";
                gvwAddress.Columns[6].Visible = false;
                gvwAddress.Columns[7].Caption = "Баг хорооны нэр";
                gvwAddress.Columns[7].OptionsColumn.AllowEdit = false;
                gvwAddress.Columns[8].Caption = "Байрны талаар нэмэлт мэдээлэл";
                gvwAddress.Columns[8].Visible = false;
                gvwAddress.Columns[9].Caption = "Нэмэлт мэдээлэл";
                gvwAddress.Columns[9].OptionsColumn.AllowEdit = false;
                gvwAddress.Columns[10].Visible = false;
                gvwAddress.Columns[11].Caption = "Одоо идэвхтэй байгаа хаяг ";                
                gvwAddress.Columns[12].Caption = "Нэмсэн огноо, цаг";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetFamilyData()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdFamily);

                gvwFamily.Columns[0].Caption = "Хэрэглэгчийн дугаар";
                gvwFamily.Columns[0].Visible = false;
                gvwFamily.Columns[1].Caption = "Дэс дугаар";
                gvwFamily.Columns[1].Visible = false;
                gvwFamily.Columns[2].Caption = "Хамаатаны төрөл";
                gvwFamily.Columns[2].OptionsColumn.AllowEdit = false;
                gvwFamily.Columns[3].Caption = "Эцэг эхийн нэр";
                gvwFamily.Columns[4].Caption = "Харилцагчийн нэр";
                gvwFamily.Columns[5].Caption = "Овог";
                gvwFamily.Columns[6].Caption = "Регистрийн дугаар";
                gvwFamily.Columns[7].Caption = "Иргэний үнэмлэх дугаар";
                gvwFamily.Columns[8].Caption = "Майл хаяг";
                gvwFamily.Columns[9].Caption = "Ажлын утасын дугаар";
                gvwFamily.Columns[10].Caption = "Гар утасны дугаар";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Refresh_Sup(DataSet ds)
        {
            try
            {
                grdSupervisor.DataSource = ds.Tables[2];
                SetSupData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetSupData()
        {
            try
            {
                gvwSupervisor.Columns[0].Caption = "Хэрэглэгчийн дугаар";
                gvwSupervisor.Columns[1].Caption = "Овог";
                gvwSupervisor.Columns[1].Visible = true;
                gvwSupervisor.Columns[2].Caption = "Нэр";
                gvwSupervisor.Columns[2].Visible = true;
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwSupervisor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UserProp_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwPriv);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwSupervisor);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwAddress);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwFamily);
        }
        private void Refresh_Picture(DataSet ds)
        {
        }
        void ri_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            string val = "";
            if (e.Value != null)
            {
                val = e.Value.ToString();
            }
            else
            {
                val = "False";
            }
            switch (val)
            {
                case "True":
                    e.CheckState = CheckState.Checked;
                    break;
                case "False":
                    e.CheckState = CheckState.Unchecked;
                    break;
                case "Yes":
                    goto case "True";
                case "No":
                    goto case "False";
                case "1":
                    goto case "True";
                case "0":
                    goto case "False";
                default:
                    e.CheckState = CheckState.Checked;
                    break;
            }
            e.Handled = true;
        }
        public RepositoryItemCheckEdit CreateRepositoryCheckEdit()
        {
            RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(ri)).BeginInit();
            ri.AutoHeight = false;
            ri.AllowFocused = false;
            ri.ValueChecked = 1;
            ri.ValueUnchecked = 0;
            ((System.ComponentModel.ISupportInitialize)(ri)).EndInit();
            ri.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(ri_QueryCheckStateByValue);
            return ri;
        }
        private void xtraTabUser_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (numUserNo.Text == "")
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
        private void xtraTabCustomer_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            switch (e.PageIndex)
            {
                case 1:
                    if (loadAddress == false)
                    {
                        RefreshAddress();
                    }
                    break;
                case 2:
                    if (loadFamily == false)
                    {
                        RefreshFamily();
                    }
                    break;
            }
        }
        private void cboUserType_EditValueChanged(object sender, EventArgs e)
        {
            if (Static.ToInt(cboUserType.EditValue) == 0)
            {
                tabAddress.PageVisible = false;
                tabFamily.PageVisible = false;

                ucUserProp.FieldLinkClear();
                ucUserProp.FieldLinkAdd("numUserNo", 0, "UserNo", "", true, true);
                ucUserProp.FieldLinkAdd("chkStatus", 0, "Status", "", true, false);
                ucUserProp.FieldLinkAdd("cboLevel", 0, "ulevel", "", true, false);
                ucUserProp.FieldLinkAdd("txtUserFName", 0, "UserFName", "", true, false);
                ucUserProp.FieldLinkAdd("txtUserFName2", 0, "UserFName2", "", false, false);
                ucUserProp.FieldLinkAdd("txtUserLName", 0, "UserLName", "", true, false);
                ucUserProp.FieldLinkAdd("txtUserLName2", 0, "UserLName2", "", false, false);
                ucUserProp.FieldLinkAdd("txtRegister", 0, "RegisterNo", "", true, false);
                ucUserProp.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
                ucUserProp.FieldLinkAdd("txtPosition", 0, "Position", "", false, false);
                ucUserProp.FieldLinkAdd("cboUserType", 0, "UserType", "", true, false);
                ucUserProp.FieldLinkAdd("cboLogintype", 0, "Logintype", "", true, false);
                ucUserProp.FieldLinkAdd("txtEMail", 0, "EMail", "", true, false);
                ucUserProp.FieldLinkAdd("txtMobile", 0, "Mobile", "", true, false);
                ucUserProp.FieldLinkAdd("txtPassword", 0, "UPassword", "", true, false);
                ucUserProp.FieldLinkAdd("cboAgentCorp", 0, "AGENTCORPNO", "", false, false, true);
                ucUserProp.FieldLinkAdd("cboAgentBranch", 0, "AGENTBRANCHNO", "", false, false, true);
                InitCombos();
                //FormUtility.LookUpEdit_SetValue(ref cboUserType, 0);
            }
            else
            {
                ucUserProp.FieldLinkClear();
                ucUserProp.FieldLinkAdd("numUserNo", 0, "UserNo", "", true, true);
                ucUserProp.FieldLinkAdd("chkStatus", 0, "Status", "", true, false);
                ucUserProp.FieldLinkAdd("cboLevel", 0, "ulevel", "", true, false);
                ucUserProp.FieldLinkAdd("txtUserFName", 0, "UserFName", "", true, false);
                ucUserProp.FieldLinkAdd("txtUserFName2", 0, "UserFName2", "", false, false);
                ucUserProp.FieldLinkAdd("txtUserLName", 0, "UserLName", "", true, false);
                ucUserProp.FieldLinkAdd("txtUserLName2", 0, "UserLName2", "", false, false);
                ucUserProp.FieldLinkAdd("txtRegister", 0, "RegisterNo", "", true, false);
                ucUserProp.FieldLinkAdd("cboBranch", 0, "BranchNo", "", true, false);
                ucUserProp.FieldLinkAdd("txtPosition", 0, "Position", "", false, false);
                ucUserProp.FieldLinkAdd("cboUserType", 0, "UserType", "", true, false);
                ucUserProp.FieldLinkAdd("cboLogintype", 0, "Logintype", "", true, false);
                ucUserProp.FieldLinkAdd("txtEMail", 0, "EMail", "", true, false);
                ucUserProp.FieldLinkAdd("txtMobile", 0, "Mobile", "", true, false);
                ucUserProp.FieldLinkAdd("txtPassword", 0, "UPassword", "", true, false);
                ucUserProp.FieldLinkAdd("cboAgentCorp", 0, "AGENTCORPNO", "", true, false);
                ucUserProp.FieldLinkAdd("cboAgentBranch", 0, "AGENTBRANCHNO", "", true, false);


                InitCombos();
                //FormUtility.LookUpEdit_SetValue(ref cboUserType, 1);

                tabAddress.PageVisible = true;
                tabFamily.PageVisible = true;
            }
            switch (ucUserProp.ToggleFlag)
            {
                case 0:
                    ucUserProp.FieldLinkSetSaveState();
                    break;
                case 1:
                    ucUserProp.FieldLinkSetNewState(false);
                    break;
                case 2:
                    ucUserProp.FieldLinkSetEditState();
                    break;
            }
            switch (ucAddress.ToggleFlag)
            {
                case 0:
                    ucAddress.FieldLinkSetSaveState();
                    break;
                case 1:
                    ucAddress.FieldLinkSetNewState(false);
                    break;
                case 2:
                    ucAddress.FieldLinkSetEditState();
                    break;
            }
            switch (ucFamily.ToggleFlag)
            {
                case 0:
                    ucFamily.FieldLinkSetSaveState();
                    break;
                case 1:
                    ucFamily.FieldLinkSetNewState(false);
                    break;
                case 2:
                    ucFamily.FieldLinkSetEditState();
                    break;
            }
        }
        #endregion
        #region [ Toggles ]
        #region[General tab]
        void ucUserProp_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucUserProp.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToInt(numUserNo.EditValue) != 0)
                {
                    EventSave(isnew, ref cancel);
                }
                else
                {
                    MessageBox.Show("Хэрэглэгчийн дугаарыг 0-ээс ялгаатай оруулна уу");
                    cancel = true;
                }
            }
            else
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
        }
        void EventSave(bool isnew, ref bool cancel)
        {
            _userno = Static.ToInt(numUserNo.Text);
            res = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110113, 110113, null);
            try
            {
                if (res.ResultNo == 0)
                {
                    _dr = res.Data.Tables[0].Rows[0];
                    creattype = Static.ToInt(_dr["CREATETYPE"]);
                }
                if (_userno != 0)
                {
                    if (Validate())
                    {
                        //string enpass = Static.ToStr(Static.Encrypt(txtPassword.Text));
                        obj[0] = Static.ToInt(numUserNo.EditValue); // UserNo
                        obj[1] = txtUserFName.Text;   //UserFname
                        obj[2] = txtUserLName.Text;  //UserLname
                        obj[3] = txtUserFName2.Text;  //UserFname2
                        obj[4] = txtUserLName2.Text; //UserLname2
                        obj[5] = txtRegister.Text; //RegisterNo
                        obj[6] = txtPosition.Text; //Position
                        obj[7] = (chkStatus.Checked ? 0 : 9); //Status
                        obj[8] = Static.ToInt(cboBranch.EditValue); //BranchNo
                        obj[9] = Static.ToInt(cboLevel.EditValue); //UserLevel
                        if (Static.ToStr(txtPassword.EditValue) == _upassword && creattype == 0)
                            obj[10] = Static.ToStr(txtPassword.EditValue); //UPassword
                        else
                            obj[10] = Static.Encrypt(Static.ToStr(txtPassword.Text)); //UPassword
                        obj[11] = Static.ToInt(cboUserType.EditValue); //UserType
                        obj[12] = Static.ToStr(txtEMail.Text); //Email
                        obj[13] = txtMobile.Text; //Mobile
                        obj[14] = Static.ToInt(cboLogintype.EditValue); //Logintype
                        obj[15] = 0;
                        obj[16] = Static.ToDate(_core.TxnDate); //Change Pass Date
                        obj[17] = Static.ToInt(cboAgentCorp.EditValue);
                        obj[18] = Static.ToInt(cboAgentBranch.EditValue);
                        if (gvwPriv.DataSource == null)
                            obj[19] = null;

                        else
                        {
                            DataTable dtPriv = (DataTable)grdPriv.DataSource;
                            obj[19] = dtPriv;
                        }
                        if (grdSupervisor.DataSource == null)
                            obj[20] = null;
                        else
                        {
                            DataTable dtSupervisor = (DataTable)grdSupervisor.DataSource;
                            dtSupervisor.AcceptChanges();
                            obj[20] = dtSupervisor;
                        }
                        fieldValue[0] = "UserNo";
                        fieldValue[1] = "UserFname";
                        fieldValue[2] = "UserLname";
                        fieldValue[3] = "UserFname2";
                        fieldValue[4] = "UserLname2";
                        fieldValue[5] = "RegisterNo";
                        fieldValue[6] = "Position";
                        fieldValue[7] = "Status";
                        fieldValue[8] = "BranchNo";
                        fieldValue[9] = "ulevel";
                        fieldValue[10] = "UPASSWORD";
                        fieldValue[11] = "UserType";
                        fieldValue[12] = "Email";
                        fieldValue[13] = "Mobile";
                        fieldValue[14] = "LoginType";
                        fieldValue[15] = "WRONGCOUNT";
                        fieldValue[16] = "PASSCHDATE";
                        fieldValue[17] = "AGENTCORP";
                        fieldValue[18] = "AGENTBRANCH";

                        object[] msgs = new object[3];
                        msgs[0] = msg;

                        object[] upassword = new object[1];
                        upassword[0] = txtPassword.EditValue;
                        object[] olpassvalue = new object[1];
                        olpassvalue[0] = txtPassword.OldEditValue;

                        if (isnew)
                        {
                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110102, 110102, new object[] { obj, fieldValue, upassword });
                            msg = "Амжилттай нэмлээ";
                        }
                        else
                        {
                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110104, 110104, new object[] { obj, fieldValue, oldValue, olpassvalue, upassword, _userno });
                            if (res.ResultNo == 0)
                            {
                                msg = "Амжилттай засварлалаа";
                            }
                            else
                            {
                                MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                            }
                        }
                        if (res.ResultNo == 0)
                        {
                            ucUserProp.FieldLinkSetEditState();
                            RefreshData(_userno);
                            MessageBox.Show(msg);
                        }
                        else
                        {
                            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                            cancel = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0} бүлгийн хүчинтэй хугацаа оруулгаагүй байна.",errorname));
                    }
                }     
            }
            catch (Exception ex)
            {
                cancel = true;
                MessageBox.Show(ex.Message);
            }
        }
        void ucUserProp_EventDelete()
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                int userno = Static.ToInt(numUserNo.Text);
                if (_userno != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110103, 110103, new object[] { userno });
                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");                        
                        ucUserProp.FieldLinkSetNewState();                        
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
        void ucUserProp_EventExit(bool editing, ref bool cancel)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwPriv);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwSupervisor);
            this.Close();
        }
        //private bool Validate()
        //{
        //    DataTable dt = (DataTable)grdPriv.DataSource;
        //    if (dt != null)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if (Static.ToInt(dr["status"]) == 1)
        //            {
        //                if (Static.ToStr(dr["expiredate"]) == "")
        //                {
        //                    errorname = Static.ToStr(dr["GroupName"]);
        //                    return false;
        //                }
        //            }
        //            //MessageBox.Show("asdfsdgy");
        //        }
        //    }
        //    return true;
        //}
        #endregion[]        
        #endregion
        #region [ BTN's ]
        private void SaveImage()
        {
            Result res = new Result();
            string msg = "";
            //object[] obj = new object[1];
            try
            {
                //_userno = Static.ToInt(numUserNo.Text);
                if (_userno != 0)
                {
                    if (picData.Image != null)
                    {
                        //byte[] a = Static.ImageToByte(picData.Image);

                        //obj[0] = _userno;
                        //obj[1] = a;

                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110111, 110111, new object[] { Static.ToInt(numUserNo.EditValue), Static.ImageToByte(picData.Image) });
                        msg = "Амжилттай хадгаллаа.";

                        if (res.ResultNo == 0)
                        {
                            MessageBox.Show(msg);
                            //this.FieldLinkSetEditState();
                        }
                        else
                        {
                            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                        }
                    }
                    else
                        MessageBox.Show("Та зургаа сонгоно уу.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadImage()
        {
            Result res = new Result();
            //string msg = "";
            object[] obj = new object[1];
            try
            {
                _userno = Static.ToInt(numUserNo.Text);
                obj[0] = _userno;

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110112, 110112, obj);
                msg = "Амжилттай засварлалаа";

                if (res.ResultNo == 0)
                {
                    if (res.Data != null)
                        if (res.AffectedRows > 0)
                        {
                            byte[] a = (byte[])res.Data.Tables[0].Rows[0][0];
                            picData.Image = Static.ImageFromByte(a);
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
        private void AddSupUser()
        {
            try
            {
                if (numSupervisor.Text != "" && Static.ToInt(numSupervisor.Text) != 0)
                {
                    if (Static.ToInt(numSupervisor.Text) != Static.ToInt(numUserNo.Text))
                    {
                        DataTable dt = (DataTable)grdSupervisor.DataSource;
                        DataRow[] dr = dt.Select("Userno=" + numSupervisor.Text);
                            
                        if (dr.Length != 1)
                        {                            
                            object[] tmpdrow = new object[3];
                            tmpdrow[0] = Static.ToInt(numSupervisor.Text);
                            tmpdrow[1] = numSupervisor.Text;
                            tmpdrow[2] = numSupervisor.Text;

                            dt.Rows.Add(tmpdrow);
                            grdSupervisor.DataSource = null;
                            grdSupervisor.DataSource = dt;
                            MessageBox.Show("Амжилттай нэмлээ");
                        }
                        else
                            MessageBox.Show("Урьд нь нэмэгдсэн байна");
                    }
                    else
                    {
                        MessageBox.Show("Өөрийгөө нэмэх боломжгүй");
                    }
                }
                else
                    MessageBox.Show("Хэрэглэгчээ сонгоно уу");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Supervisor хадгалаагүй үед нэмэгдэхгүй");
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveSupUser()
        {
            if (gvwSupervisor.DataRowCount > 0)
            {
                if (gvwSupervisor.FocusedRowHandle >= 0)
                {
                    if (gvwSupervisor.FocusedRowHandle < gvwSupervisor.DataRowCount)
                    {
                        gvwSupervisor.BeginUpdate();
                        gvwSupervisor.BeginDataUpdate();
                        gvwSupervisor.DeleteRow(gvwSupervisor.FocusedRowHandle);
                        gvwSupervisor.EndDataUpdate();
                        gvwSupervisor.EndUpdate();
                        MessageBox.Show("Амжилттай устгагдлаа");
                    }
                    else
                    {
                        MessageBox.Show("Өөрийгөө нэмэх боломжгүй");
                    }
                }
            }
        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            HeavenPro.List.UserList frm = new HeavenPro.List.UserList(_core);

            frm.ucUserList.Browsable = true;

            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                numSupervisor.Text = Static.ToStr(frm.ucUserList.SelectedRow["UserNo"]);
            }
        }
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            SaveImage();
        }
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            ISM.Template.FormImage img = new FormImage();
            img.Resource = _core.resource;
            img.ShowDialog();
            if (img.DialogResult == System.Windows.Forms.DialogResult.OK)
                picData.Image = img.ImageObject;
        }
        private void picData_Click(object sender, EventArgs e)
        {
            if (Static.ToStr(numUserNo.Text) != "")
            {
                LoadImage();
            }
            else 
            {
                MessageBox.Show("Хэрэглэгч сонгоогүй байна.");
            }
                
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (Static.ToInt(numUserNo.EditValue) != 0)
            {
                AddSupUser();
            }
            else
                MessageBox.Show("Хэрэглэгчээ хадгалаагүй байгаа тул Supervisor хэрэглэгчийг бүртгэх боломжгүй.");
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            RemoveSupUser();
        }
        private void txtRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Static.ToStr(cboRegisterMask.EditValue) == "")
            {
                e.Handled = true;
                MessageBox.Show("Регистерийн дугаарын Маск оруула уу .");
            }
            else
                e.Handled = false;
        }
        private void cboRegisterMask_EditValueChanged(object sender, EventArgs e)
        {
            if (cboRegisterMask.GetSelectedDataRow() != null)
            {
                txtRegister.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                DataRowView drv = (DataRowView)cboRegisterMask.GetSelectedDataRow();

                if (Static.ToStr(drv["MaskValue"]) != "")
                    txtRegister.Properties.Mask.EditMask = Static.ToStr(drv["MaskValue"]);
            }
        }
        private void gvwPriv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucUserProp.FieldLinkSetEditState();
        }
        private void gvwSupervisor_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucUserProp.FieldLinkSetEditState();
        }
        private void UserProp_KeyDown(object sender, KeyEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void txtEMail_EditValueChanged(object sender, EventArgs e)
        {
        }
        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            ActionChangePass();
        }
        #endregion
        #region [Address]
        //int TxnCodeT2Select = 280008;
        int TxnCodeT2Save = 280010;
        int TxnCodeT2Edit = 280011;
        void ucAddress_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucAddress.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = { Static.ToInt(numUserNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue), Static.ToInt(cboCity.EditValue), Static.ToInt(cboDist.EditValue),Static.ToInt(cboSubDist.EditValue), txtApartment.EditValue, txtAdd.EditValue,(chkAddrCurrent.Checked ? 1 : 0) };
                string msg = "";
                try
                {
                    if (isnew)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, TxnCodeT2Save, TxnCodeT2Save, new object[] { obj });
                        msg = "Амжилттай нэмлээ";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, TxnCodeT2Edit, TxnCodeT2Edit, new object[] { obj, OldValue });
                        msg = "Амжилттай засварлалаа";
                    }
                    if (res.ResultNo == 0)
                    {
                        RefreshAddress();
                        ucAddress.FieldLinkSetSaveState();
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
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucAddress_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToInt(numUserNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue), Static.ToInt(cboCity.EditValue), Static.ToInt(cboDist.EditValue), Static.ToInt(cboSubDist.EditValue), txtAdd.EditValue, (chkAddrCurrent.Checked ? 1 : 0), txtApartment.EditValue };
            OldValue = Value;

        }
        void ucAddress_EventExit(bool editing, ref bool cancel)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            this.Close();
        }
        void ucAddress_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwAddress);
        }
        void ucAddress_EventDelete()
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                if (Static.ToInt(numUserNo.EditValue) != 0 && Static.ToInt(txtAddrSeqNo.EditValue) != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 280012, 280012, new object[] { Static.ToInt(numUserNo.EditValue), Static.ToLong(txtAddrSeqNo.EditValue) });

                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");
                        RefreshAddress();
                        ucAddress.FieldLinkSetSaveState();
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
        private void cboCity_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                if (cboCity.EditValue != null && cboCity.EditValue != DBNull.Value)
                    filter = "CITYCODE=" + cboCity.EditValue;

                if (DSAgent.Tables["CUSTDISTRICT"] != null)
                    SetComboSub("DISTCODE", "NAME", cboDist, DSAgent.Tables["CUSTDISTRICT"], filter, new int[] { 0, 3, 4 });              
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
                if (cboCity.EditValue != null && cboCity.EditValue != DBNull.Value && Static.ToStr(cboCity.EditValue) != "")
                    filter = "CITYCODE=" + cboCity.EditValue;

                if (cboDist.EditValue != null && cboDist.EditValue != DBNull.Value && Static.ToStr(cboDist.EditValue) != "")
                {
                    if (filter != "")
                        filter = filter + "and DISTCODE=" + cboDist.EditValue;
                    else
                        filter = "DISTCODE=" + cboDist.EditValue;
                }
                SetComboSub("SUBDISTCODE", "NAME", cboSubDist, DSAgent.Tables["CUSTSUBDISTRICT"], filter, new int[] { 0, 2 });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cboAgentCorp_EditValueChanged(object sender, EventArgs e)
        {
            InitCombos();
        }
        private void gvwAddress_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try 
            {
                ucAddress.FieldLinkSetValues();
            }            
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"Err");
            }
        }
        #endregion
        #region[Family]
        int TxnCodeT4Select = 280013;
        int TxnCodeT4Save = 280015;
        int TxnCodeT4Edit = 280016;
        void ucFamily_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (ucFamily.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = { Static.ToInt(numUserNo.EditValue), Static.ToLong(numTab4SeqNo.EditValue), Static.ToInt(cboTab4FamilyType.EditValue), txtTab4FirstName.EditValue, txtTab4LastName.EditValue, txtTab4MiddleName.EditValue, txtTab4RegisterNo.EditValue, txtTab4PassNo.EditValue, txtTab4Email.EditValue, Static.ToLong(numTab4Telephone.EditValue), Static.ToLong(numTab4Mobile.EditValue) };
                string msg = "";
                try
                {
                    if (isnew)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, TxnCodeT4Save, TxnCodeT4Save, new object[] { obj });
                        msg = "Амжилттай нэмлээ";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, TxnCodeT4Edit, TxnCodeT4Edit, new object[] { obj, OldValue });
                        msg = "Амжилттай засварлалаа";
                    }
                    if (res.ResultNo == 0)
                    {
                        RefreshFamily();
                        ucFamily.FieldLinkSetSaveState();
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
            else
            {
                cancel = true;
                MessageBox.Show(err);
                cont.Select();
            }
        }
        void ucFamily_EventDelete()
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                if (Static.ToInt(numUserNo.EditValue) != 0 && Static.ToInt(numTab4SeqNo.EditValue) != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 280017, 280017, new object[] { Static.ToInt(numUserNo.EditValue),Static.ToLong(numTab4SeqNo.EditValue) });

                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");
                        RefreshFamily();
                        ucFamily.FieldLinkSetSaveState();
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
        void ucFamily_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToInt(numUserNo.EditValue), Static.ToLong(numTab4SeqNo.EditValue), Static.ToInt(cboTab4FamilyType.EditValue), txtTab4FirstName.EditValue, txtTab4LastName.EditValue, txtTab4MiddleName.EditValue, txtTab4RegisterNo.EditValue, txtTab4PassNo.EditValue, txtTab4Email.EditValue, Static.ToLong(numTab4Telephone.EditValue), Static.ToLong(numTab4Mobile.EditValue) };
            OldValue = Value;
        }
        void ucFamily_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwFamily);
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
        private void gvwFamily_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucFamily.FieldLinkSetValues();
        }
        #endregion[]
        #region[Agent functions]
        void RefreshFamily()
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(numUserNo.EditValue) != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, TxnCodeT4Select, TxnCodeT4Select, new object[] { Static.ToInt(numUserNo.EditValue) });
                    if (res.ResultNo == 0)
                    {                        
                        ucFamily.DataSource = res.Data;
                        SetFamilyData();
                        ucFamily.FieldLinkSetValues();
                        loadFamily = true;
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void RefreshAddress()
        {
            Result res = new Result();
            try
            {
                if (Static.ToInt(numUserNo.EditValue) != 0)
                {
                    //SetAddressData();
                    ucAddress.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 280008, 280008, new object[] { Static.ToInt(numUserNo.EditValue) });
                    if (res.ResultNo == 0)
                    {
                        ucAddress.DataSource = res.Data;
                        SetAddressData();
                        //gvwAddress.Columns[2].Visible = true;
                        ucAddress.FieldLinkSetValues();
                        loadAddress = true;
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void EventAgentDelete(int numUserNo, long SeqID, int TxnCodeDel, int TxnCodeRef, ISM.Template.ucTogglePanel ucTP, DevExpress.XtraGrid.Views.Grid.GridView GView, int[] hide, int[] caption, string[] CaptionValue)
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                if (numUserNo != 0 && SeqID != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, TxnCodeDel, TxnCodeDel, new object[] { numUserNo, SeqID });

                    if (res.ResultNo == 0)
                    {
                        //RefreshAgentData(numUserNo, TxnCodeRef, ucTP, GView, hide, caption, CaptionValue);
                        ucAddress.FieldLinkSetSaveState();
                        MessageBox.Show("Амжилттай устгагдлаа");
                        ucTP.FieldLinkSetSaveState();
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
        #endregion
    }
}