using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.Contract
{
    public partial class frmContract : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        Core.Core _core;
        string _contractid;
        object OldValue;
        string appname = "", formname = "";
        Form FormName = null;
        DataRow enquiryrow = null;
        #endregion
        public frmContract(Core.Core core, string ContractID)
        { 
            InitializeComponent();
            _core = core;
            _contractid = ContractID;
            btnCustomerEnq.Image = _core.Resource.GetImage("navigate_button");
            btnCustomerFind.Image = _core.Resource.GetImage("button_find");
            btnCustomerEnq.Image = _core.Resource.GetImage("navigate_refresh");
            btnProdFind.Image = _core.Resource.GetImage("button_find");
            btnChangeOwner.Image = _core.Resource.GetImage("button_find");
            ucGeneral.Resource = _core.Resource;
            ucProduct.Resource = _core.Resource;
            ucAccount.Resource = _core.Resource;
            ucDepSchedule.Resource = _core.Resource;
            InitData();
            InitEvents();
            InitCombo();
            InitToggles();
            if (_contractid == "")
            {
                ucGeneral.FieldLinkSetNewState();
                dteValidStartDate.DateTime = DateTime.Now;
                dteValidEndDate.DateTime = DateTime.Now;
                tmeValidEndTime.Time = DateTime.Now;
                tmeValidStartTime.Time = DateTime.Now;
                cboContractType.ItemIndex = 0;
                cboCurCode.ItemIndex = 0;
                cboDepFreq.ItemIndex = 0;
                cboStatus.ItemIndex = 1;
                txtCreateUser.EditValue = _core.RemoteObject.User.UserNo;
                txtOwnerUser.EditValue = _core.RemoteObject.User.UserNo;
            }
            else
            {
                RefreshGeneral();
            }
        }
        #region[Init]
        void InitCombo()
        {
            #region[General]

            FormUtility.LookUpEdit_SetList(ref cboDepFreq, "A", "Хугацааны эцэст нийт дүнгээр");
            FormUtility.LookUpEdit_SetList(ref cboDepFreq, "B", "Хугацааны эцэст үлдэгдэл дүнгээр");
            FormUtility.LookUpEdit_SetList(ref cboDepFreq, "M", "Сар бүрийн эцэст тодорхой дүнгээр");
            FormUtility.LookUpEdit_SetList(ref cboDepFreq, "Q", "Улирал бүрийн эцэст тодорхой дүнгээр");
            FormUtility.LookUpEdit_SetList(ref cboDepFreq, "Y", "Жил бүрийн эцэст тодорхой дүнгээр");
            FormUtility.LookUpEdit_SetList(ref cboDepFreq, "S", "Хуваарийн дагуу");

            FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Идэвхгүй");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Идэвхтэй");

            FormUtility.LookUpEdit_SetList(ref cboVat, 0, "Тооцохгүй");
            FormUtility.LookUpEdit_SetList(ref cboVat, 1, "Тооцно");

            FormUtility.LookUpEdit_SetList(ref cboBalanceType, 0, "Улайхгүй");
            FormUtility.LookUpEdit_SetList(ref cboBalanceType, 1, "Улайна");

            FormUtility.LookUpEdit_SetList(ref cboBalanceMethod, 0, "Үлдэгдэл хөдөлгөхгүй");
            FormUtility.LookUpEdit_SetList(ref cboBalanceMethod, 1, "Зөвхөн гэрээнд заагдсан бараанд үйлчилнэ");
            FormUtility.LookUpEdit_SetList(ref cboBalanceMethod, 2, "Бүх бараанд үйлчилнэ");

            string msg = "";
            ArrayList Tables = new ArrayList();
            string[] names = { "CONTRACTTYPE","CURRENCY","PAPAYTYPE","REBATEMASTER" };
            DictUtility.PrivNo = 130001;
            Result res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
            DataTable dt = (DataTable)Tables[0];
            if (dt == null)
            {
                msg = "Dictionary-д CONTRACTTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboContractType, dt, "CONTRACTTYPE", "NAME", "", null);
                cboContractType.ItemIndex = 0;
            }
            dt = (DataTable)Tables[1];
            if (dt == null)
            {
                msg = "Dictionary-д CURRENCY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboCurCode, dt, "CURRENCY", "NAME", "", new int[] { 2 });                
            }
            dt = (DataTable)Tables[2];
            if (dt == null)
            {
                msg = "Dictionary-д PAPAYTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboAcntPayType, dt, "TYPEID", "NAME", "", null);
            }
            dt = (DataTable)Tables[3];
            if (dt == null)
            {
                msg = "Dictionary-д REBATEMASTER оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboRebateID, dt, "MASTERID", "NAME", "MASTERTYPE=0", new int[] { 1 });
                FormUtility.LookUpEdit_SetList(ref cboLoyalID, dt, "MASTERID", "NAME", "MASTERTYPE=1", new int[] { 1 });
                FormUtility.LookUpEdit_SetList(ref cboPointID, dt, "MASTERID", "NAME", "MASTERTYPE=2", new int[] { 1 });
            }
            #endregion
            #region[Product]
            FormUtility.LookUpEdit_SetList(ref cboProdProductType, 0, "Бараа");
            FormUtility.LookUpEdit_SetList(ref cboProdProductType, 1, "Үйлчилгээ");
            FormUtility.LookUpEdit_SetList(ref cboProdProductType, 2, "Багц");
            #endregion
            #region[Schedule]
            FormUtility.LookUpEdit_SetList(ref cboDateType, "D", "Өдрөөр");
            FormUtility.LookUpEdit_SetList(ref cboDateType, "M", "Сараар");
            FormUtility.LookUpEdit_SetList(ref cboDateType, "Q", "Уриралаар");
            FormUtility.LookUpEdit_SetList(ref cboDateType, "H", "Хагас жилээр");
            FormUtility.LookUpEdit_SetList(ref cboDateType, "Y", "Жилээр");
            FormUtility.LookUpEdit_SetValue(ref cboDateType, "M");

            FormUtility.LookUpEdit_SetList(ref cboSchType, 0, "Тогтмол дүнгээр");
            FormUtility.LookUpEdit_SetList(ref cboSchType, 1, "Нийт дүнг хугацаанд хуваах");
            FormUtility.LookUpEdit_SetValue(ref cboSchType, 0);
            #endregion
        }
        void InitEvents()
        {
            #region[General]
            ucGeneral.EventEdit += new ucTogglePanel.delegateEventEdit(ucGeneral_EventEdit);
            ucGeneral.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucGeneral_EventDataChanged);
            ucGeneral.EventSave += new ucTogglePanel.delegateEventSave(ucGeneral_EventSave);
            ucGeneral.EventDelete += new ucTogglePanel.delegateEventDelete(ucGeneral_EventDelete);
            ucGeneral.EventAddAfter+=new ucTogglePanel.delegateEventAddAfter(ucGeneral_EventAddAfter);
            #endregion
            #region[Product]
            ucProduct.EventEdit += new ucTogglePanel.delegateEventEdit(ucProduct_EventEdit);
            ucProduct.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucProduct_EventDataChanged);
            ucProduct.EventSave += new ucTogglePanel.delegateEventSave(ucProduct_EventSave);
            ucProduct.EventDelete += new ucTogglePanel.delegateEventDelete(ucProduct_EventDelete);
            ucProduct.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucProduct_EventAddAfter);
            #endregion
            #region[Account]
            ucAccount.EventEdit += new ucTogglePanel.delegateEventEdit(ucAccount_EventEdit);
            ucAccount.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucAccount_EventDataChanged);
            ucAccount.EventSave += new ucTogglePanel.delegateEventSave(ucAccount_EventSave);
            ucAccount.EventDelete += new ucTogglePanel.delegateEventDelete(ucAccount_EventDelete);
            ucAccount.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucAccount_EventAddAfter);
            #endregion
            #region[DepSchedule]
            ucDepSchedule.EventEdit += new ucTogglePanel.delegateEventEdit(ucDepSchedule_EventEdit);
            ucDepSchedule.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucDepSchedule_EventDataChanged);
            ucDepSchedule.EventSave += new ucTogglePanel.delegateEventSave(ucDepSchedule_EventSave);
            ucDepSchedule.EventDelete += new ucTogglePanel.delegateEventDelete(ucDepSchedule_EventDelete);
            ucDepSchedule.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucDepSchedule_EventAddAfter);

            #endregion
            #region[Dep]
            #endregion
        }
        void InitToggles()
        {
            #region [General]

            ucGeneral.ToggleShowDelete = true;
            ucGeneral.ToggleShowEdit = true;
            ucGeneral.ToggleShowExit = true;
            ucGeneral.ToggleShowNew = true;
            ucGeneral.ToggleShowReject = true;
            ucGeneral.ToggleShowSave = true;

            //ucGeneral.DataSource = null;
            #endregion
            #region [Product]
            ucProduct.ToggleShowDelete = true;
            ucProduct.ToggleShowEdit = true;
            ucProduct.ToggleShowExit = true;
            ucProduct.ToggleShowNew = true;
            ucProduct.ToggleShowReject = true;
            ucProduct.ToggleShowSave = true;

            ucProduct.DataSource = null;
            ucProduct.GridView = gvwProduct;
            ISM.Template.FormUtility.SetFormatGrid(ref gvwProduct, false);
            #endregion
            #region [Account]
            ucAccount.ToggleShowDelete = true;
            ucAccount.ToggleShowEdit = true;
            ucAccount.ToggleShowExit = true;
            ucAccount.ToggleShowNew = true;
            ucAccount.ToggleShowReject = true;
            ucAccount.ToggleShowSave = true;

            ucAccount.DataSource = null;
            ucAccount.GridView = gvwAccount;
            ISM.Template.FormUtility.SetFormatGrid(ref gvwAccount, false);
            #endregion
            #region [DepSchedule]
            ucDepSchedule.ToggleShowDelete = true;
            ucDepSchedule.ToggleShowEdit = true;
            ucDepSchedule.ToggleShowExit = true;
            ucDepSchedule.ToggleShowNew = true;
            ucDepSchedule.ToggleShowReject = true;
            ucDepSchedule.ToggleShowSave = true;

            ucDepSchedule.DataSource = null;
            ucDepSchedule.GridView = gvwDepSchedule;
            ISM.Template.FormUtility.SetFormatGrid(ref gvwDepSchedule, false);
            #endregion
        }
        void InitData()
        {
            try
            {
                #region [General]
                ucGeneral.FieldLinkAdd("txtContractNo", 0, "ContractNo", "", false, false, true);
                ucGeneral.FieldLinkAdd("cboContractType", 0, "ContractType", "", true, false);
                ucGeneral.FieldLinkAdd("txtCustomerNo", 0, "Custno", "", true, false, true);
                ucGeneral.FieldLinkAdd("txtCustomerName", 0, "LASTNAME", "", false, false, true);
                ucGeneral.FieldLinkAdd("dteValidStartDate", 0, "ValidStartDate", "", true, false);
                ucGeneral.FieldLinkAdd("tmeValidStartTime", 0, "ValidStartTime", "", true, false);
                ucGeneral.FieldLinkAdd("dteValidEndDate", 0, "ValidEndDate", "", false, false);
                ucGeneral.FieldLinkAdd("tmeValidEndTime", 0, "ValidEndTime", "", false, false);
                ucGeneral.FieldLinkAdd("txtAmount", 0, "Amount", "", false, false);
                ucGeneral.FieldLinkAdd("txtBalance", 0, "Balance", "", false, false);
                ucGeneral.FieldLinkAdd("cboCurCode", 0, "Curcode", "", true, false);
                ucGeneral.FieldLinkAdd("txtPersonCount", 0, "PersonCount", "", false, false);
                ucGeneral.FieldLinkAdd("cboDepFreq", 0, "DepFreq", "", true, false);
                ucGeneral.FieldLinkAdd("txtDepAmount", 0, "DepAmount", "", false, false);
                ucGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
                ucGeneral.FieldLinkAdd("txtOwnerUser", 0, "OwnerUser", "", false, false, true);
                ucGeneral.FieldLinkAdd("dteCreateDate", 0, "CreateDate", "", false, false, true);
                ucGeneral.FieldLinkAdd("dtePostDate", 0, "CreatePostDate", "", false, false, true);
                ucGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);

                ucGeneral.FieldLinkAdd("cboRebateID", 0, "RebateID", "", false, false, false);
                ucGeneral.FieldLinkAdd("cboLoyalID", 0, "LoyalID", "", false, false, false);
                ucGeneral.FieldLinkAdd("cboPointID", 0, "PointID", "", false, false, false);
                ucGeneral.FieldLinkAdd("cboVat", 0, "VAT", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtAccountNo", 0, "AccountNo", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtIncomeAccountNo", 0, "IncomeAccountNo", "", false, false, false);
                ucGeneral.FieldLinkAdd("cboBalanceType", 0, "BalanceType", "", true, false);
                ucGeneral.FieldLinkAdd("cboBalanceMethod", 0, "BalanceMethod", "", true, false);
                ucGeneral.FieldLinkAdd("txtDepBalance", 0, "DepBalance", "", false, false);
                
                #endregion
                #region[Product]
                ucProduct.FieldLinkAdd("txtProdProductNo", 0, "ProdNo", "", true, false);
                ucProduct.FieldLinkAdd("cboProdProductType", 0, "ProdType", "", true, false);
                ucProduct.FieldLinkAdd("txtProdPrice", 0, "Price", "", true, false);
                #endregion
                #region[Account]
                ucAccount.FieldLinkAdd("cboAcntPayType", 0, "PayType", "", true, false);
                ucAccount.FieldLinkAdd("txtAcntAccount", 0, "AccountNo", "", true, false);
                ucAccount.FieldLinkAdd("txtAcntAccountName", 0, "AccountName", "", false, false);
                #endregion
                #region[DepSchedule]
                ucDepSchedule.FieldLinkAdd("dteDepDay", 0, "Day", "", true, false);
                ucDepSchedule.FieldLinkAdd("txtDepSAmount", 0, "Amount", "", true, false);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[General]
        void ucGeneral_EventDelete()
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130005, 130005, new object[] { txtContractNo.EditValue });

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucGeneral_EventSave(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            string err = "";
            string msg = "";
            Control cont = null;

            if (ucGeneral.FieldValidate(ref err, ref cont) == true)
            {
                if (dteValidStartDate.DateTime.Date > dteValidEndDate.DateTime.Date)
                {
                    MessageBox.Show(this, "Эхлэх дуусах огноо алдаатай байна.", "Алдаа");
                    cancel = true;
                    return;
                }
                else
                {
                    if (tmeValidStartTime.Time > tmeValidEndTime.Time && dteValidStartDate.DateTime.Date == dteValidEndDate.DateTime.Date)
                    {
                        MessageBox.Show(this, "Эхлэх дуусах цаг алдаатай байна.", "Алдаа");
                        cancel = true;
                        return;
                    }
                }
                if (Static.ToDecimal(txtBalance.EditValue) > Static.ToDecimal(txtAmount.EditValue))
                {
                    MessageBox.Show(this, "Үлдэгдэл дүн үнийн дүнгээс хэтэрсэн байна.", "Алдаа");
                    cancel = true;
                    return;
                }
                dteCreateDate.EditValue = _core.TxnDate;
                dtePostDate.EditValue = DateTime.Now;
                object[] obj = {
                                   Static.ToStr(txtContractNo.EditValue),
                                   Static.ToStr(cboContractType.EditValue),
                                   Static.ToStr(txtCustomerNo.EditValue),
                                   Static.ToDate(dteValidStartDate.EditValue),
                                   tmeValidStartTime.EditValue,
                                   Static.ToDate(dteValidEndDate.EditValue),
                                   tmeValidEndTime.EditValue,
                                   Static.ToDecimal(txtAmount.EditValue),
                                   Static.ToDecimal(txtBalance.EditValue),
                                   Static.ToStr(cboCurCode.EditValue),
                                   Static.ToInt(txtPersonCount.EditValue),
                                   Static.ToStr(cboDepFreq.EditValue),
                                   Static.ToDecimal(txtDepAmount.EditValue),
                                   Static.ToInt(cboStatus.EditValue),
                                   Static.ToDateTime(dteCreateDate.EditValue),
                                   dtePostDate.EditValue,
                                   Static.ToInt(txtCreateUser.EditValue),
                                   Static.ToInt(txtOwnerUser.EditValue),

                                   Static.ToLong(cboRebateID.EditValue),
                                   Static.ToLong(cboLoyalID.EditValue),
                                   Static.ToLong(cboPointID.EditValue),

                                   Static.ToInt(cboBalanceType.EditValue),
                                   Static.ToInt(cboVat.EditValue),
                                   Static.ToStr(txtAccountNo.EditValue),
                                   Static.ToStr(txtIncomeAccountNo.EditValue),
                                   Static.ToInt(cboBalanceMethod.EditValue),
                                   Static.ToDecimal(txtDepBalance.EditValue)
                               };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130003, 130003, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130004, 130004, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    txtContractNo.EditValue = res.Param[0];
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
        void ucGeneral_EventDataChanged()
        {

        }
        void ucGeneral_EventEdit(ref bool cancel)
        {
            throw new NotImplementedException();
        }
        void ucGeneral_EventAddAfter()
        {
            txtCreateUser.EditValue = _core.RemoteObject.User.UserNo;
            txtOwnerUser.EditValue = _core.RemoteObject.User.UserNo;
            ucGeneral.FieldLinkSetNewState();
            dteValidStartDate.DateTime = DateTime.Now;
            dteValidEndDate.DateTime = DateTime.Now;
            tmeValidEndTime.Time = DateTime.Now;
            tmeValidStartTime.Time = DateTime.Now;
            cboContractType.ItemIndex = 0;
            cboBalanceMethod.ItemIndex = 0;
            cboCurCode.ItemIndex = 0;
            cboDepFreq.ItemIndex = 0;
            cboStatus.ItemIndex = 1;
        }
        private void RefreshGeneral()
        {
            Result res = new Result();
            try
            {
                if (_contractid != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130002, 130002, new object[] { _contractid });

                    if (res.ResultNo == 0)
                    {
                            ucGeneral.DataSource = res.Data;
                            ucGeneral.FieldLinkSetValues();
                            ucGeneral.FieldLinkSetSaveState();
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
        #region[Product]
        void ucProduct_EventDelete()
        {
            Result res = new Result();
            try
            {
                DataRow DR = gvwProduct.GetFocusedDataRow();

                if (DR != null)
                {
                    string pContractNo = Static.ToStr(DR["ContractNo"]);
                    int pProdType = Static.ToInt(DR["ProdType"]);
                    string pProdNo = Static.ToStr(DR["ProdNo"]);

                    DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == System.Windows.Forms.DialogResult.No) return;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130010, 130010, new object[] { pContractNo, pProdType, pProdNo });

                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");
                        RefreshProduct();
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                else
                {
                    MessageBox.Show("Бүтээгдэхүүнээ сонгоно уу.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucProduct_EventSave(bool isnew, ref bool cancel)
        {
            Result res=new Result();
            string err = "";
            string msg = "";
            Control cont = null;

            if (ucProduct.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = {
                                 Static.ToStr(txtContractNo.EditValue), 
                                 Static.ToStr(txtProdProductNo.EditValue), 
                                 Static.ToInt(cboProdProductType.EditValue),
                                 Static.ToDecimal(txtProdPrice.EditValue)
                               };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130008, 130008, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130009, 130009, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    RefreshProduct();
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
        void ucProduct_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwProduct);
        }
        void ucProduct_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToStr(txtContractNo.EditValue), Static.ToLong(txtProdProductNo.EditValue), Static.ToInt(cboProdProductType.EditValue), Static.ToDecimal(txtProdPrice.EditValue) };
            OldValue = Value;
        }
        void ucProduct_EventAddAfter()
        {
            cboProdProductType.ItemIndex = 0;
        }
        private void RefreshProduct()
        {
            Result res = new Result();
            try
            {
                grdProduct.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130006, 130006, new object[] { Static.ToStr(txtContractNo.EditValue) });
                if (res.ResultNo == 0)
                {
                    grdProduct.DataSource = res.Data.Tables[0];
                    ucProduct.FieldLinkSetSaveState();
                    ucProduct.FieldLinkSetValues();
                    gvwProduct.Columns[0].Caption = "Гэрээний дугаар";
                    gvwProduct.Columns[0].Visible = false;
                    gvwProduct.Columns[1].Caption = "Бүтээгдэхүүний дугаар";
                    gvwProduct.Columns[2].Caption = "Бүтээгдэхүүний нэр";
                    gvwProduct.Columns[3].Caption = "Төрлийн дугаар";
                    gvwProduct.Columns[3].Visible = false;
                    gvwProduct.Columns[4].Caption = "Төрлийн нэр";
                    gvwProduct.Columns[5].Caption = "Үнэ";
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
        #endregion
        #region[Account]
        void ucAccount_EventDelete()
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130015, 130015, new object[] { txtContractNo.EditValue, cboAcntPayType.EditValue, txtAcntAccount.EditValue });

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    RefreshAccount();
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
        void ucAccount_EventSave(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            string err = "";
            string msg = "";
            Control cont = null;

            if (ucAccount.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = { Static.ToLong(txtContractNo.EditValue), Static.ToInt(cboContractType.EditValue), Static.ToLong(txtAcntAccount.EditValue), Static.ToStr(txtAcntAccountName.EditValue) };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130013, 130013, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130014, 130014, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    RefreshAccount();
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
        void ucAccount_EventEdit(ref bool cancel)
        {

        }
        void ucAccount_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwAccount);
        }
        void ucAccount_EventAddAfter()
        {
            cboAcntPayType.ItemIndex = 0;
        }
        private void RefreshAccount()
        {
            Result res = new Result();
            try
            {
                grdAccount.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130011, 130011, new object[] { Static.ToStr(txtContractNo.EditValue) });
                if (res.ResultNo == 0)
                {
                    grdAccount.DataSource = res.Data.Tables[0];
                    ucAccount.FieldLinkSetSaveState();
                    ucAccount.FieldLinkSetValues();
                    gvwAccount.Columns[0].Caption = "Гэрээний дугаар";
                    gvwAccount.Columns[0].Visible = false;
                    gvwAccount.Columns[1].Caption = "Төрлийн дугаар";
                    gvwAccount.Columns[2].Caption = "Төрлийн нэр";
                    gvwAccount.Columns[3].Caption = "Дансны дугаар";
                    gvwAccount.Columns[4].Caption = "Дансны нэр";
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
        #endregion
        #region[Schedule]
        void ucDepSchedule_EventDelete()
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130020, 130020, new object[] { txtContractNo.EditValue, Static.ToDateTime(dteDepDay.EditValue), Static.ToDecimal(txtDepSAmount.EditValue) });

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    RefreshDep();
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
        void ucDepSchedule_EventSave(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            string err = "";
            string msg = "";
            Control cont = null;

            if (ucDepSchedule.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = { Static.ToStr(txtContractNo.EditValue), Static.ToDate(dteDepDay.EditValue), Static.ToDecimal(txtDepSAmount.EditValue) };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130018, 130018, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130019, 130019, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    RefreshDep();
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
        void ucDepSchedule_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwDepSchedule);
        }
        void ucDepSchedule_EventEdit(ref bool cancel)
        {
            
        }
        void ucDepSchedule_EventAddAfter()
        {
            dteDepDay.EditValue = _core.TxnDate;
        }
        private void btnScheduleAdd_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                if (_core.RemoteObject.IsConnected && MessageBox.Show("Автоматаар хуваарилахдаа итгэлтэй байна уу? \n(Өмнө үүсэгсэн бичлэгүүдийг устгаад шинээр үүсгэнэ.)", _core.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Static.ToInt(cboSchType.EditValue) == 0 && Static.ToDecimal(numSchAllAmount.EditValue) == 0)
                    {
                        MessageBox.Show("Төлбөрийн дүн тэг байна.");
                        return;
                    }

                    if (Static.ToInt(cboSchType.EditValue) == 0)
                    {
                        if (dteSchStartDate.EditValue == null)
                        {
                            MessageBox.Show("Эхлэх огноог шалгана уу.");
                            return;
                        }
                    }
                    else
                    {
                        if (dteSchStartDate.EditValue == null)
                        {
                            MessageBox.Show("Эхлэх огноог шалгана уу.");
                            return;
                        }
                        if (dteSchEndDate.EditValue == null)
                        {
                            MessageBox.Show("Дуусах огноог шалгана уу.");
                            return;
                        }
                    }

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130021, 130021, new object[] { 
                        Static.ToStr(txtContractNo.EditValue), 
                        Static.ToInt(cboSchType.EditValue), 
                        Static.ToStr(cboDateType.EditValue), 
                        Static.ToDate(dteSchStartDate.EditValue), 
                        Static.ToDate(dteSchEndDate.EditValue), 
                        Static.ToDecimal(numSchAmount.EditValue),
                        Static.ToDecimal(numSchAllAmount.EditValue)
                    });

                    if (res.ResultNo == 0)
                    {
                        RefreshDep();
                        MessageBox.Show("Амжилттай хуваариллаа.");
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
        private void RefreshDep()
        {
            Result res = new Result();
            try
            {
                grdDepSchedule.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130016, 130016, new object[] { Static.ToStr(txtContractNo.EditValue) });

                if (res.ResultNo == 0)
                {
                    grdDepSchedule.DataSource = res.Data.Tables[0];
                    ucDepSchedule.FieldLinkSetSaveState();
                    ucDepSchedule.FieldLinkSetValues();
                    gvwDepSchedule.Columns[0].Caption = "Гэрээний дугаар";
                    gvwDepSchedule.Columns[0].Visible = false;
                    gvwDepSchedule.Columns[1].Caption = "Элэгдүүлэх огноо";
                    gvwDepSchedule.Columns[2].Caption = "Элэгдүүлэх дүн";
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
        #endregion
        private void xtraTabControl1_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (txtContractNo.EditValue == null)
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
        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            switch (e.PageIndex)
            {
                case 1: 
                    RefreshProduct();
                    break;
                case 2:
                    RefreshAccount();
                    break;
                case 3:
                    RefreshDep();
                    break;
                case 4:
                    break;
            }
        }
        private void btnProdFind_Click(object sender, EventArgs e)
        {
            if (cboProdProductType.EditValue != null)
            {
                switch (Static.ToInt(cboProdProductType.EditValue))
                {
                    case 0:
                        {
                            InfoPos.List.InventoryList frm = new List.InventoryList(_core);
                            frm.ucInventoryList.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtProdProductNo.EditValue = frm.ucInventoryList.SelectedRow["INVID"];
                                txtProdPrice.EditValue = frm.ucInventoryList.SelectedRow["PriceAmount"];
                            }
                            
                        } break;
                    case 1:
                        {
                            InfoPos.List.ServiceList frm = new List.ServiceList(_core);
                            frm.ucServiceList.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtProdProductNo.EditValue = frm.ucServiceList.SelectedRow["SERVID"];
                                txtProdPrice.EditValue = frm.ucServiceList.SelectedRow["PriceAmount"];
                            }
                        } break;
                    case 2:
                        {
                            InfoPos.List.PackMainList frm = new List.PackMainList(_core);
                            frm.ucPackMain.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtProdProductNo.EditValue = frm.ucPackMain.SelectedRow["PackId"];
                                txtProdPrice.EditValue = frm.ucPackMain.SelectedRow["Price"];
                            }
                        } break;
                }
                
            }
            else
                MessageBox.Show("Бүтээгдэхүүний төрлөө сонгоно уу.");
        }
        private void btnChangeOwner_Click(object sender, EventArgs e)
        {
            InfoPos.List.UserList frm = new List.UserList(_core);
            frm.ucUserList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                txtOwnerUser.EditValue = frm.ucUserList.SelectedRow["USERNO"];
            }
        }
        private void frmContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwProduct);
            FormUtility.SaveStateGrid(appname, formname, ref gvwAccount);
            FormUtility.SaveStateGrid(appname, formname, ref gvwDepSchedule);
        }
        #region[gvw focus]
        private void gvwProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucProduct.FieldLinkSetValues();
        }
        private void gvwAccount_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucAccount.FieldLinkSetValues();
        }
        private void gvwDepSchedule_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucDepSchedule.FieldLinkSetValues();
        }
        #endregion
        private void btnCustomerFind_Click(object sender, EventArgs e)
        {
            InfoPos.List.CustomerList frm = new List.CustomerList(_core);
            frm.ucCustomerList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                txtCustomerNo.EditValue=frm.ucCustomerList.SelectedRow["CUSTOMERNO"];
                //if (Static.ToInt(frm.ucCustomerList.SelectedRow["CLASSCODE"]) == 0)
                //    txtCustomerName.EditValue = frm.ucCustomerList.SelectedRow["LASTNAME"];
                //else
                //    txtCustomerNo.EditValue = frm.ucCustomerList.SelectedRow["CORPORATENAME"];
            }
        }
        private void cboProdProductType_EditValueChanged(object sender, EventArgs e)
        {
            txtProdProductNo.EditValue = null;
        }
        private void btnCustomerEnq_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                if (Static.ToLong(txtCustomerNo.EditValue) != 0)
                {
                    object[] obj1 = new object[23];
                    obj1[0] = Static.ToLong(txtCustomerNo.EditValue);

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120001, 120001, obj1);

                    if (res.ResultNo == 0)
                    {
                        object[] obj = new object[3];
                        obj[0] = _core;
                        obj[1] = txtCustomerNo.EditValue;
                        obj[2] = res.Data.Tables[0].Rows[0];
                        EServ.Shared.Static.Invoke("HeavenPro.Enquiry.dll", "HeavenPro.Enquiry.Main", "CallCustomerEnquiry", obj);
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
        private void txtCustomerNo_EditValueChanged(object sender, EventArgs e)
        {
            Result res2=new Result();
            res2 = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120001, 120001, new object[] { txtCustomerNo.EditValue, 0 });
            if (res2.ResultNo == 0)
            {
                if (res2.Data.Tables[0].Rows.Count != 0)
                {
                    string name = "";
                    if (Static.ToInt(res2.Data.Tables[0].Rows[0]["CLASSCODE"]) == 0)         //Иргэн
                    {
                        name = Static.ToStr(res2.Data.Tables[0].Rows[0]["LASTNAME"]);
                    }
                    else
                    {                                                       //Байгууллага
                        name = Static.ToStr(res2.Data.Tables[0].Rows[0]["CORPORATENAME"]);
                    }
                    txtCustomerName.EditValue=name;
                }
            }
            else
                MessageBox.Show(res2.ResultNo + " : " + res2.ResultDesc);
        }
        private void frmContract_Load(object sender, EventArgs e)
        {

        }
    }
}
