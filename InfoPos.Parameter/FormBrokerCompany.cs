using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;
using System.Collections;

namespace InfoPos.Parameter
{
    public partial class FormBrokerCompany : Form
    {
        #region[Хувьсагч]
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        object[] OldValue;
        DataTable DS;
        string msg = "";
        long _brokerid;
        #endregion[]
        public FormBrokerCompany(InfoPos.Core.Core core, long Brokerid)
        {
            InitializeComponent();
            _core = core;
            _brokerid = Brokerid;
            Init();       
        }
        #region[Init Function]
        private void Init()
        {
            try
            {
                ucBroker.EventSave += new ucTogglePanel.delegateEventSave(FormBrokerCompany_EventSave);
                ucBroker.EventDelete += new ucTogglePanel.delegateEventDelete(FormBrokerCompany_EventDelete);
                ucBroker.EventExit += new ucTogglePanel.delegateEventExit(ucBroker_EventExit);
                ucBroker.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucBroker_EventAddAfter);

                ucBroker.Resource = _core.Resource;

                ucBroker.ToggleShowDelete = true;
                ucBroker.ToggleShowEdit = true;
                ucBroker.ToggleShowExit = true;
                ucBroker.ToggleShowNew = true;
                ucBroker.ToggleShowReject = true;
                ucBroker.ToggleShowSave = true;

                ucBroker.DataSource = null;                

                ucBroker.FieldLinkAdd("numBrokeriD",0, "BrokeriD", "", true, true);
                ucBroker.FieldLinkAdd("txtName", 0, "name", "", true, false);
                ucBroker.FieldLinkAdd("txtName2", 0, "name2", "", false, false);
                ucBroker.FieldLinkAdd("cboCountryCode",0,"CountryCode", "", false, false);
                ucBroker.FieldLinkAdd("cboBankID",0, "BankID", "", false, false);
                ucBroker.FieldLinkAdd("cboBankBranchID", 0, "BankBranchID", "", false, false);
                ucBroker.FieldLinkAdd("numAcountNo", 0, "AccountNo", "", false, false);
                ucBroker.FieldLinkAdd("cboCurCode", 0, "CurCode", "", false, false);
                ucBroker.FieldLinkAdd("txtAddress", 0, "Address", "", true, false);
                ucBroker.FieldLinkAdd("txtEmail", 0, "Email", "", false, false);
                ucBroker.FieldLinkAdd("txtTelephone", 0, "Telephone", "", false, false);
                ucBroker.FieldLinkAdd("txtFax", 0, "Fax", "", false, false);
                ucBroker.FieldLinkAdd("txtWebSite", 0, "WebSite", "", false, false);
                ucBroker.FieldLinkAdd("numOrderNo", 0, "OrderNo", "", true, false);           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ucBroker_EventAddAfter()
        {
            Initcombo();
            ucBroker.FieldLinkSetNewState();
            cboBankBranchID.ItemIndex = 0;
            cboBankID.ItemIndex = 0;
            cboCountryCode.ItemIndex = 0;
            cboCurCode.ItemIndex = 0;
            numOrderNo.EditValue = 0;
        }

        private void Initcombo() 
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";
            DictUtility.PrivNo = 140131;

            string[] names = new string[] { "CURRENCY", "COUNTRY", "BANK", "BANKBRANCH" };
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

            #region [ Валют ]
            DT = (DataTable)Tables[0];          // CURRENCY-ыг уншиж байгаа
            if (DT == null)
            {
                msg = "Dictionary-д CURRENCY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboCurCode, DT, "CURRENCY", "NAME");  
            }

            if (msg != "")
            {
                MessageBox.Show(msg);
            }
            #endregion[]
            #region [ Улсын код ]
            DT = (DataTable)Tables[1];          // COUNTRY-ыг уншиж байгаа
            if (DT == null)
            {
                msg = "Dictionary-д COUNTRY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboCountryCode, DT, "COUNTRYCODE", "NAME");                
            }

            if (msg != "")
            {
                MessageBox.Show(msg);
            }
            #endregion  
            #region [ Банк ]
            DT = (DataTable)Tables[2];          // Банк-ыг уншиж байгаа
            if (DT == null)
            {
                msg = "Dictionary-д BANK оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboBankID, DT, "BANKID", "NAME");
            }

            if (msg != "")
            {
                MessageBox.Show(msg);
            }
            #endregion[]
            #region [ Банк ]
            DT = (DataTable)Tables[3];          // Банк-ыг уншиж байгаа
            DS = DT;
            if (DT == null)
            {
                msg = "Dictionary-д BANKBRANCH оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboBankBranchID, DT, "BRANCHID", "NAME", "", new int[] { 0, 3 });
            }

            if (msg != "")
            {
                MessageBox.Show(msg);
            }
       
           
            #endregion 
        }
        void ucBroker_EventExit(bool editing, ref bool cancel)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);            
            this.Close();
        }
        #endregion[]
        #region[Үзэгдэлүүд]
        void FormBrokerCompany_EventDelete()
        {
            if (Static.ToStr(numBrokeriD.EditValue) == "")
            {
                MessageBox.Show("Бүртгэгдээгүй хоосон талбар!");
            }
            else
            {
                DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DR == System.Windows.Forms.DialogResult.No) return;
                else
                {
                    try
                    {
                        Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140330, 140330, new object[] { numBrokeriD.EditValue });
                        if (r.ResultNo != 0)
                        {
                            MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Амжилттай устгагдлаа .");
                            
                            btn = 1;

                            RefreshData(_brokerid);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        void FormBrokerCompany_EventEdit(ref bool cancel)
        {
            object[] NewValue = new object[14];
            NewValue[0] = Static.ToLong(numBrokeriD.EditValue);
            NewValue[1] = Static.ToStr(txtName.EditValue);
            NewValue[2] = Static.ToStr(txtName2.EditValue);
            NewValue[3] = Static.ToInt(cboCountryCode.EditValue);
            NewValue[4] = Static.ToLong(cboBankID.EditValue);
            NewValue[5] = Static.ToLong(cboBankBranchID.EditValue);
            NewValue[6] = Static.ToLong(numAcountNo.EditValue);
            NewValue[7] = Static.ToInt(cboCurCode.EditValue);
            NewValue[8] = Static.ToStr(txtAddress.EditValue);
            NewValue[9] = Static.ToStr(txtEmail.EditValue);
            NewValue[10] = Static.ToStr(txtTelephone.EditValue);
            NewValue[11] = Static.ToStr(txtFax.EditValue);
            NewValue[12] = Static.ToStr(txtWebSite.EditValue);
            NewValue[13] = Static.ToStr(txtWebSite.EditValue);
            OldValue = NewValue;        
        }
        void FormBrokerCompany_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            _brokerid = Static.ToLong(numBrokeriD.EditValue);
            if (!ucBroker.FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
            else
            {                                         
                object[] NewValue = new object[14];
                NewValue[0] = Static.ToLong(numBrokeriD.EditValue); //
                NewValue[1] = Static.ToStr(txtName.EditValue); //
                NewValue[2] = Static.ToStr(txtName2.EditValue); //
                NewValue[3] = Static.ToInt(cboCountryCode.EditValue);// 
                NewValue[4] = Static.ToLong(cboBankID.EditValue); //
                NewValue[5] = Static.ToLong(cboBankBranchID.EditValue); //
                NewValue[6] = Static.ToLong(numAcountNo.EditValue); //
                NewValue[7] = Static.ToStr(cboCurCode.EditValue); //
                NewValue[8] = Static.ToStr(txtAddress.EditValue); //
                NewValue[9] = Static.ToStr(txtEmail.EditValue);// 
                NewValue[10] = Static.ToStr(txtTelephone.EditValue); //
                NewValue[11] = Static.ToStr(txtFax.EditValue); //
                NewValue[12] = Static.ToStr(txtWebSite.EditValue); //
                NewValue[13] = Static.ToInt(numOrderNo.EditValue); //
                Result r;
                try
                {
                    if (!isnew)
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140329, 140329, new object[] { NewValue, OldValue  });
                        msg = "Амжилттай засварлалаа.";
                        
                    }
                    else
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140328, 140328, new object[] { NewValue });
                         msg = "Амжилттай нэмлээ .";
                    }
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show(msg);
                        RefreshData(_brokerid);
                        FormUtility.SaveStateForm(appname, ref FormName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
        }
        void RefreshData(long _brokerid)
        {
            Result res = new Result();
            try
            {
                if (_brokerid != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140327, 140327, new object[] { Static.ToLong(_brokerid) });

                    if (res.ResultNo == 0)
                    {
                        ucBroker.DataSource = res.Data;                                                                       
                        ucBroker.FieldLinkSetValues();                       
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
        #endregion
        #region[FormEvent]
        private void FormBrokerCompany_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            //FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormBrokerCompany_Load(object sender, EventArgs e)
        {
            object[] Value = new object[14];
                        Value[0] = Static.ToLong(numBrokeriD.EditValue);
                        Value[1] = Static.ToStr(txtName.EditValue);
                        Value[2] = Static.ToStr(txtName2.EditValue);
                        Value[3] = Static.ToInt(cboCountryCode.EditValue);
                        Value[4] = Static.ToLong(cboBankID.EditValue);
                        Value[5] = Static.ToLong(cboBankBranchID.EditValue);
                        Value[6] = Static.ToLong(numAcountNo.EditValue);
                        Value[7] = Static.ToStr(cboCurCode.EditValue);
                        Value[8] = Static.ToStr(txtAddress.EditValue);
                        Value[9] = Static.ToStr(txtEmail.EditValue);
                        Value[10] = Static.ToStr(txtTelephone.EditValue);
                        Value[11] = Static.ToStr(txtFax.EditValue);
                        Value[12] = Static.ToStr(txtWebSite.EditValue);
                        Value[13] = Static.ToInt(numOrderNo.EditValue);

            OldValue = Value;

            if (_brokerid != 0)
            {
                Initcombo();                
                //ucBroker.FieldLinkSetValues();                   
                RefreshData(_brokerid);                    
                ucBroker.FieldLinkSetRejectState();
            }
            else
            {
                Initcombo();   
                ucBroker.FieldLinkSetNewState();
                cboBankBranchID.ItemIndex = 0;
                cboBankID.ItemIndex = 0;
                cboCountryCode.ItemIndex = 0;
                cboCurCode.ItemIndex = 0;
                numOrderNo.EditValue = 0;
            }
            
            FormUtility.RestoreStateForm(appname, ref FormName);            
        }
        private void FormBrokerCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion[]       

        private void cboBankID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                    string filter = "";
                    if (cboBankID.EditValue != null && cboBankID.EditValue != DBNull.Value && cboBankID.EditValue != "")
                    {
                        filter = "BankID=" + cboBankID.EditValue;
                        SetComboSub("BRANCHID", "NAME", cboBankBranchID, DS, filter, new int[] { 0, 3 });
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
    }
}