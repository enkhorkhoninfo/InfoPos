using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using ISM.Template;
using EServ.Shared;

namespace InfoPos.PreSale
{
    public partial class frmPreSale : DevExpress.XtraEditors.XtraForm
    {
        #region [ Variable ]
        InfoPos.Core.Core _core;
        long OldTabPersonCustNo = 0;
        string _PreSaleNo = ""; 
        string appname = "", formname = "";
        string knowbutton = "", knowbuttonProduct = "";
        Form FormName = null;
        public static ImageCollection image = new ImageCollection();
        #endregion
        public frmPreSale(InfoPos.Core.Core core, string PreSaleNo)
        {
            InitializeComponent();
            _core = core;
            _PreSaleNo = PreSaleNo;
            ucGeneral.Resource = _core.Resource;
            ucPerson.Resource = _core.Resource;
            btnCustEnq.Image = _core.Resource.GetImage("navigate_refresh");
            btnCustFind.Image = _core.Resource.GetImage("button_find");
            btnOwnerFind.Image = _core.Resource.GetImage("button_find");
            //btnTabPersonCustEnq.Image = _core.Resource.GetImage("navigate_refresh");
            //btnTabCustFind.Image = _core.Resource.GetImage("button_find");

            btnGroupAdd.Image = _core.Resource.GetImage("navigate_add");
            btnGroupEdit.Image = _core.Resource.GetImage("navigate_edit");
            btnGroupDelete.Image = _core.Resource.GetImage("navigate_delete");

            btnProductAdd.Image = _core.Resource.GetImage("navigate_add");
            btnProductEdit.Image = _core.Resource.GetImage("navigate_edit");
            btnProductDelete.Image = _core.Resource.GetImage("navigate_delete");

            btnInvFind.Image = _core.Resource.GetImage("button_find");


            gvwProduct.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            gvwPrice.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            gvwOrderPerson.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            InitData();
            InitCombo();
            InitEvents();
            InitToggles();
            if (_PreSaleNo == "")
            {
                ucGeneral.FieldLinkSetNewState();
                dteStartDateTime.DateTime = DateTime.Now;
                dteEndDateTime.DateTime = DateTime.Now;
                //dteGraceHoursStart.DateTime = DateTime.Now;

                cboCurCode.ItemIndex = 0;
                cboDiscountID.ItemIndex = 0;
                cboDiscountType.ItemIndex = 0;
                cboAmartizationFreq.ItemIndex = 1;
                cboAmartizationMethod.ItemIndex = 0;
                cboAmartizationType.ItemIndex = 0;
                cboPriceType.ItemIndex = 0;
                cboPreSaleType.ItemIndex = 0;
                cboPreSaleProd.ItemIndex = 0;

                FormUtility.LookUpEdit_SetValue(ref cboStatus, 0);
                FormUtility.LookUpEdit_SetValue(ref cboChannelID, 0);

                txtCreateUser.EditValue = _core.RemoteObject.User.UserNo;
                txtSalesUser.EditValue = _core.RemoteObject.User.UserNo;
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

            FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Шинэ");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Баталгаажсан");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 2, "Гүйцэтгэгдсэн");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 8, "Хугацаа дууссан");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 9, "Цуцалсан");
            FormUtility.LookUpEdit_SetValue(ref cboStatus, 0);

            FormUtility.LookUpEdit_SetList(ref cboPreSaleType, 0, "Session ticket (Хугацаандаа хязгааргүй боруулалт, Элэгдүүлэлт, ABA руу явахгүй");
            FormUtility.LookUpEdit_SetList(ref cboPreSaleType, 1, "Barter (Хугацаандаа, дүндээ хязгаартай борлуулалт, Элэгдүүлэлт ABA руу явна");
            FormUtility.LookUpEdit_SetList(ref cboPreSaleType, 2, "Ваучер (Хугацаандаа, дүндээ хязгаартай борлуулалт, Элэгдүүлэлт ABA руу явна, Урьдчилсан борлуулалтын бүтээгдэхүүнтэй)");
            FormUtility.LookUpEdit_SetList(ref cboPreSaleType, 3, "Гэрээ");
            FormUtility.LookUpEdit_SetValue(ref cboPreSaleType, 0);

            FormUtility.LookUpEdit_SetList(ref cboPriceType, 0, "Захиалгын дагуу үнэ байхгүй. Үйлчилгээ авах үеийнхээр");
            FormUtility.LookUpEdit_SetList(ref cboPriceType, 1, "Захиалгын дагуу үнээр борлуулалт хийнэ");
            FormUtility.LookUpEdit_SetValue(ref cboPriceType, 0);

            FormUtility.LookUpEdit_SetList(ref cboDiscountType, 0, "Хөнгөлөлт байхгүй");
            FormUtility.LookUpEdit_SetList(ref cboDiscountType, 1, "Хувиар хөнгөлөнө");
            FormUtility.LookUpEdit_SetList(ref cboDiscountType, 2, "Дүнгээр хөнгөлөнө");
            FormUtility.LookUpEdit_SetValue(ref cboDiscountType, 0);

            FormUtility.LookUpEdit_SetList(ref cboChannelID, 0, "Terminal");
            FormUtility.LookUpEdit_SetList(ref cboChannelID, 1, "Web");
            FormUtility.LookUpEdit_SetList(ref cboChannelID, 2, "Mobile");
            FormUtility.LookUpEdit_SetList(ref cboChannelID, 3, "Kiosk");
            FormUtility.LookUpEdit_SetValue(ref cboChannelID, 0);

            FormUtility.LookUpEdit_SetList(ref cboAmartizationType, 0, "Хугацааны эцэст");
            FormUtility.LookUpEdit_SetList(ref cboAmartizationType, 1, "Давтамжаар");
            FormUtility.LookUpEdit_SetValue(ref cboAmartizationType, 0);
            //D-Өдөр бүр, M-Сар бүр, Q-Улирал бүр, Y-Жил бүр
            FormUtility.LookUpEdit_SetList(ref cboAmartizationFreq, "D", "Өдөр бүр");
            FormUtility.LookUpEdit_SetList(ref cboAmartizationFreq, "M", "Сар бүр");
            FormUtility.LookUpEdit_SetList(ref cboAmartizationFreq, "Q", "Улирал бүр");
            FormUtility.LookUpEdit_SetList(ref cboAmartizationFreq, "Y", "Жил бүр");

            FormUtility.LookUpEdit_SetValue(ref cboAmartizationFreq, "M");

            FormUtility.LookUpEdit_SetList(ref cboAmartizationMethod, 0, "Шулуун шугам арга");
            FormUtility.LookUpEdit_SetValue(ref cboAmartizationMethod, 0);

            string msg = "";
            ArrayList Tables = new ArrayList();
            string[] names = { "CURRENCY", "PRESALEMAIN", "COUNTRY", "PAPRICETYPE" };
            DictUtility.PrivNo = 130001;
            Result res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
            DataTable dt = (DataTable)Tables[0];
            if (dt == null)
            {
                msg = "Dictionary-д CURRENCY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboCurCode, dt, "CURRENCY", "NAME", "", null);
                cboCurCode.ItemIndex = 0;
            }
            dt = (DataTable)Tables[1];
            if (dt == null)
            {
                msg = "Dictionary-д PRESALEMAIN оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboPreSaleProd, dt, "PRESALEPROD", "NAME");
            }

            dt = (DataTable)Tables[2];
            if (dt == null)
            {
                msg = "Dictionary-д COUNTRY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboOPCountryCode, dt, "COUNTRYCODE", "NAME");
            }

            dt = (DataTable)Tables[3];
            if (dt == null)
            {
                msg = "Dictionary-д PAPRICETYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboPricePriceTypeID, dt, "PriceTypeID", "NAME");
            }

            if (msg != "")
                MessageBox.Show(msg);

            #endregion
            #region[Personal]
            FormUtility.LookUpEdit_SetList(ref cboOPSex, 0, "Эр");
            FormUtility.LookUpEdit_SetList(ref cboOPSex, 1, "Эм");

            FormUtility.LookUpEdit_SetList(ref cboOPProdType, 0, "Бараа");
            FormUtility.LookUpEdit_SetList(ref cboOPProdType, 1, "Үйлчилгээ");
            FormUtility.LookUpEdit_SetList(ref cboOPProdType, 2, "Багц");
            #endregion
            #region[Product]
            FormUtility.LookUpEdit_SetList(ref cboProdProdType, 0, "Бараа материал");
            FormUtility.LookUpEdit_SetList(ref cboProdProdType, 1, "Үйлчилгээ");
            FormUtility.LookUpEdit_SetList(ref cboProdProdType, 2, "Багц");

            FormUtility.LookUpEdit_SetList(ref cboProdDiscountType, 0, "Хөнгөлөлт байхгүй");
            FormUtility.LookUpEdit_SetList(ref cboProdDiscountType, 1, "Хувиар хөнгөлөнө");
            FormUtility.LookUpEdit_SetList(ref cboProdDiscountType, 2, "Дүнгээр хөнгөлөнө");

            FormUtility.LookUpEdit_SetValue(ref cboProdDiscountType, 0);

            FormUtility.LookUpEdit_SetList(ref cboPriceDiscountType, 0, "Хөнгөлөлт байхгүй");
            FormUtility.LookUpEdit_SetList(ref cboPriceDiscountType, 1, "Хувиар хөнгөлөнө");
            FormUtility.LookUpEdit_SetList(ref cboPriceDiscountType, 2, "Дүнгээр хөнгөлөнө");

            FormUtility.LookUpEdit_SetValue(ref cboPriceDiscountType, 0);

            #endregion
        }
        void InitEvents()
        {
            #region[General]
            ucGeneral.EventEdit += new ucTogglePanel.delegateEventEdit(ucGeneral_EventEdit);
            ucGeneral.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucGeneral_EventDataChanged);
            ucGeneral.EventSave += new ucTogglePanel.delegateEventSave(ucGeneral_EventSave);
            ucGeneral.EventDelete += new ucTogglePanel.delegateEventDelete(ucGeneral_EventDelete);
            ucGeneral.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucGeneral_EventAddAfter);
            #endregion
            #region[Person]
            ucPerson.EventEdit += new ucTogglePanel.delegateEventEdit(ucPerson_EventEdit);
            ucPerson.EventDataChanged += new ucTogglePanel.delegateEventDataChanged(ucPerson_EventDataChanged);
            ucPerson.EventSave += new ucTogglePanel.delegateEventSave(ucPerson_EventSave);
            ucPerson.EventDelete += new ucTogglePanel.delegateEventDelete(ucPerson_EventDelete);
            ucPerson.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucPerson_EventAddAfter);
            gvwOPProduct.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvwOPProduct_FocusedRowChanged);
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
            #region [Person]

            ucPerson.ToggleShowDelete = true;
            ucPerson.ToggleShowEdit = true;
            ucPerson.ToggleShowExit = true;
            ucPerson.ToggleShowNew = true;
            ucPerson.ToggleShowReject = true;
            ucPerson.ToggleShowSave = true;

            //ucGeneral.DataSource = null;
            #endregion
        }
        void InitData()
        {
            try
            {
                #region [General]
                ucGeneral.FieldLinkAdd("txtPreSaleNo", 0, "PreSaleNo", "", false, false, true);
                ucGeneral.FieldLinkAdd("cboPreSaleType", 0, "PreSaleType", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtCustNo", 0, "CustNo", "", true, false, true);
                ucGeneral.FieldLinkAdd("txtCustomerName", 0, "CustomerName", "", false, false, true);
                ucGeneral.FieldLinkAdd("cboChannelID", 0, "ChannelID", "", true, false, false);
                ucGeneral.FieldLinkAdd("cboPreSaleProd", 0, "PreSaleProd", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtUserID", 0, "UserID", "", false, false, false);
                ucGeneral.FieldLinkAdd("dteCreateDate", 0, "CreateDate", "", false, false, false);
                ucGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", true, false, true);
                ucGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtSalesUser", 0, "SalesUser", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtPersonCount", 0, "PersonCount", "", false, false, false);
                ucGeneral.FieldLinkAdd("dteStartDateTime", 0, "StartDateTime", "", false, false, false);
                ucGeneral.FieldLinkAdd("dteEndDateTime", 0, "EndDateTime", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtGraceHoursStart", 0, "GraceHoursStart", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtGraceHoursEnd", 0, "GraceHoursEnd", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtPreSaleAmount", 0, "PreSaleAmount", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtPreSaleAmountMin", 0, "PreSaleAmountMin", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtPreSaleAmountMax", 0, "PreSaleAmountMax", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtSaleAmount", 0, "SaleAmount", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtAmartizationAmount", 0, "AmartizationAmount", "", true, false, false);
                ucGeneral.FieldLinkAdd("cboAmartizationType", 0, "AmartizationType", "", true, false, false);
                ucGeneral.FieldLinkAdd("cboAmartizationFreq", 0, "AmartizationFreq", "", true, false, false);
                ucGeneral.FieldLinkAdd("cboAmartizationMethod", 0, "AmartizationMethod", "", true, false, false);
                ucGeneral.FieldLinkAdd("cboCurCode", 0, "CurCode", "", true, false, false);
                ucGeneral.FieldLinkAdd("cboPriceType", 0, "PriceType", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtDiscountID", 0, "DiscountID", "", true, false, false);
                ucGeneral.FieldLinkAdd("cboDiscountType", 0, "DiscountType", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtDicountAmount", 0, "DiscountAmount", "", false, false, false);
                ucGeneral.FieldLinkAdd("dteCancelDateTime", 0, "CancelDateTime", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtCancelNote", 0, "CancelNote", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtCancelUserNo", 0, "CancelUserNo", "", false, false, false);
                ucGeneral.FieldLinkAdd("dteExpireDateTime", 0, "ExpireDateTime", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtExpireNote", 0, "ExpireNote", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtExpireUserNo", 0, "ExpireUserNo", "", false, false, false);
                ucGeneral.FieldLinkAdd("dteConfirmDateTime", 0, "ConfirmDateTime", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtConfirmNote", 0, "ConfirmNote", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtConfirmUserNo", 0, "ConfirmUserNo", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtContractNo", 0, "ContractNo", "", false, false, false);
                ucGeneral.FieldLinkAdd("txtSalesAccountNo", 0, "SalesAccountNo", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtRefundAccountNo", 0, "RefundAccountNo", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtDiscountAccountNo", 0, "DiscountAccountNo", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtBonusAccountNo", 0, "BonusAccountNo", "", true, false, false);
                ucGeneral.FieldLinkAdd("txtBonusExpAccountNo", 0, "BonusExpAccountNo", "", true, false, false);

                #endregion
                #region[Person]
                ucPerson.FieldLinkAdd("txtOPItemNo", 0, "ItemNo", "", false, false, true);
                ucPerson.FieldLinkAdd("txtOPRegisterNo", 0, "RegisterNo", "", true, false, false);
                ucPerson.FieldLinkAdd("txtOPFirstName", 0, "FirstName", "", true, false, false);
                ucPerson.FieldLinkAdd("txtOPLastName", 0, "LastName", "", true, false, false);
                ucPerson.FieldLinkAdd("txtOPMiddleName", 0, "MiddleName", "", false, false, false);
                ucPerson.FieldLinkAdd("cboOPSex", 0, "Sex", "", true, false, false);
                ucPerson.FieldLinkAdd("dteOPBirthDay", 0, "BirthDay", "", false, false, false);
                ucPerson.FieldLinkAdd("txtOPEmail", 0, "Email", "", false, false, false);
                ucPerson.FieldLinkAdd("txtOPMobile", 0, "Mobile", "", false, false, false);
                ucPerson.FieldLinkAdd("txtOPCompany", 0, "Company", "", false, false, false);
                ucPerson.FieldLinkAdd("cboOPCountryCode", 0, "CountryCode", "", false, false, false);
                ucPerson.FieldLinkAdd("txtOPHeight", 0, "Height", "", false, false, false);
                ucPerson.FieldLinkAdd("txtOPFootSize", 0, "FootSize", "", false, false, false);
                ucPerson.FieldLinkAdd("txtOPSerialNo", 0, "SerialNo", "", false, false, false);
                ucPerson.GridView = gvwOrderPerson;
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130305, 130305, new object[] { txtPreSaleNo.EditValue });

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

            string _retstr = CheckStatus();
            if (_retstr != "")
            {
                MessageBox.Show(_retstr);
                return;
            }
            
            if (ucGeneral.FieldValidate(ref err, ref cont) == true)
            {
                if (dteStartDateTime.DateTime > dteEndDateTime.DateTime)
                {
                    MessageBox.Show(this, "Эхлэх дуусах огноо алдаатай байна.", "Алдаа");
                    cancel = true;
                    return;
                }

                object[] obj = {
                                    Static.ToStr(txtPreSaleNo.EditValue),
                                    Static.ToInt(cboPreSaleType.EditValue),
                                    Static.ToStr(txtCustNo.EditValue),
                                    Static.ToInt(cboChannelID.EditValue),
                                    Static.ToStr(cboPreSaleProd.EditValue),
                                    Static.ToStr(txtUserID.EditValue),
                                    Static.ToDateTime(dteCreateDate.EditValue),
                                    Static.ToInt(cboStatus.EditValue),
                                    Static.ToInt(txtCreateUser.EditValue),
                                    Static.ToInt(txtSalesUser.EditValue),
                                    Static.ToInt(txtPersonCount.EditValue),
                                    Static.ToDate(dteStartDateTime.EditValue),
                                    Static.ToDate(dteEndDateTime.EditValue),
                                    Static.ToInt(txtGraceHoursStart.EditValue),
                                    Static.ToInt(txtGraceHoursEnd.EditValue),
                                    Static.ToDecimal(txtPreSaleAmount.EditValue),
                                    Static.ToDecimal(txtPreSaleAmountMin.EditValue),
                                    Static.ToDecimal(txtPreSaleAmountMax.EditValue),
                                    Static.ToDecimal(txtSaleAmount.EditValue),
                                    Static.ToDecimal(txtAmartizationAmount.EditValue),
                                    Static.ToInt(cboAmartizationType.EditValue),
                                    Static.ToStr(cboAmartizationFreq.EditValue),
                                    Static.ToInt(cboAmartizationMethod.EditValue),
                                    Static.ToStr(cboCurCode.EditValue),
                                    Static.ToInt(cboPriceType.EditValue),
                                    Static.ToInt(cboDiscountID.EditValue),
                                    Static.ToInt(cboDiscountType.EditValue),
                                    Static.ToDecimal(txtDicountAmount.EditValue),
                                    Static.ToDateTime(dteCancelDateTime.EditValue),
                                    Static.ToStr(txtCancelNote.EditValue),
                                    Static.ToInt(txtCancelUserNo.EditValue),
                                    Static.ToDateTime(dteExpireDateTime.EditValue),
                                    Static.ToStr(txtExpireNote.EditValue),
                                    Static.ToInt(txtExpireUserNo.EditValue),
                                    Static.ToDateTime(dteConfirmDateTime.EditValue),
                                    Static.ToStr(txtConfirmNote.EditValue),
                                    Static.ToInt(txtConfirmUserNo.EditValue),
                                    Static.ToStr(txtContractNo.EditValue),
                                    Static.ToStr(txtSalesAccountNo.EditValue),
                                    Static.ToStr(txtRefundAccountNo.EditValue),
                                    Static.ToStr(txtDiscountAccountNo.EditValue),
                                    Static.ToStr(txtBonusAccountNo.EditValue),
                                    Static.ToStr(txtBonusExpAccountNo.EditValue)
                               };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130303, 130303, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130305, 130305, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    txtPreSaleNo.EditValue = res.Param[0];
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
            txtSalesUser.EditValue = _core.RemoteObject.User.UserNo;
            ucGeneral.FieldLinkSetNewState();

            dteStartDateTime.DateTime = DateTime.Now;
            dteEndDateTime.DateTime = DateTime.Now;

            cboCurCode.ItemIndex = 0;
            cboDiscountID.ItemIndex = 0;
            cboDiscountType.ItemIndex = 0;
            cboAmartizationFreq.ItemIndex = 1;
            cboAmartizationMethod.ItemIndex = 0;
            cboAmartizationType.ItemIndex = 0;
            cboPriceType.ItemIndex = 0;
            cboPreSaleType.ItemIndex = 0;
            cboPreSaleProd.ItemIndex = 0;

            FormUtility.LookUpEdit_SetValue(ref cboStatus, 0);
            FormUtility.LookUpEdit_SetValue(ref cboChannelID, 0);

        }
        private void RefreshGeneral()
        {
            Result res = new Result();
            try
            {
                if (_PreSaleNo != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130302, 130302, new object[] { _PreSaleNo });

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
        #region[Person]
        void ucPerson_EventDelete()
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130310, 130310, new object[] { Static.ToStr(txtPreSaleNo.EditValue), Static.ToStr(txtOPItemNo.EditValue) });

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    RefreshPerson();
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
        void ucPerson_EventSave(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            string err = "";
            string msg = "";
            Control cont = null;

            string _retstr = CheckStatus();
            if (_retstr != "")
            {
                MessageBox.Show(_retstr);
                return;
            }

            if (ucPerson.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = {
                                   Static.ToStr(txtPreSaleNo.EditValue),
                                   Static.ToLong(txtOPItemNo.EditValue),
                                   Static.ToStr(txtOPRegisterNo.EditValue),
                                   Static.ToStr(txtOPFirstName.EditValue),
                                   Static.ToStr(txtOPLastName.EditValue),
                                   Static.ToStr(txtOPMiddleName.EditValue),
                                   Static.ToInt(cboOPSex.EditValue),
                                   Static.ToDate(dteOPBirthDay.EditValue),
                                   Static.ToStr(txtOPEmail.EditValue),
                                   Static.ToInt(txtOPMobile.EditValue),
                                   Static.ToStr(txtOPCompany.EditValue),
                                   Static.ToInt(cboOPCountryCode.EditValue),
                                   Static.ToInt(txtOPHeight.EditValue),
                                   Static.ToInt(txtOPFootSize.EditValue),
                                   Static.ToStr(txtOPSerialNo.EditValue)
                               };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130308, 130308, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130309, 130309, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    RefreshPerson();
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
        void ucPerson_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref gvwOrderPerson);
        }
        void ucPerson_EventEdit(ref bool cancel)
        {
            //OldTabPersonCustNo = Static.ToLong(txtOPItemNo.EditValue);
            //btnTabCustFind.Enabled = true;
        }
        void ucPerson_EventAddAfter()
        {
            //btnTabCustFind.Enabled = true;
        }
        private void RefreshPerson()
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130306, 130306, new object[] { txtPreSaleNo.EditValue });

                if (res.ResultNo == 0)
                {
                    grdOrderPerson.DataSource = res.Data.Tables[0];
                    ucPerson.FieldLinkSetValues();
                    ucPerson.FieldLinkSetSaveState();
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
        private void gvwOrderPerson_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwOrderPerson.GetFocusedDataRow();
            if (dr != null)
            {
                txtOPItemNo.EditValue = dr["ItemNo"];
                txtOPRegisterNo.EditValue = dr["RegisterNo"];
                txtOPFirstName.EditValue = dr["FirstName"];
                txtOPLastName.EditValue = dr["LastName"];
                txtOPMiddleName.EditValue = dr["MiddleName"];
                FormUtility.LookUpEdit_SetValue(ref cboOPSex, dr["Sex"]);
                dteOPBirthDay.EditValue = dr["BirthDay"];
                txtOPEmail.EditValue = dr["Email"];
                txtOPMobile.EditValue = dr["Mobile"];
                txtOPCompany.EditValue = dr["Company"];
                cboOPCountryCode.EditValue = dr["CountryCode"];
                txtOPHeight.EditValue = dr["Height"];
                txtOPFootSize.EditValue = dr["FootSize"];
                txtOPSerialNo.EditValue = dr["SerialNo"];
                OPRefreshProduct(Static.ToStr(txtPreSaleNo.EditValue), Static.ToLong(txtOPItemNo.EditValue));
            }
        }
        //OPProduct
        private void btnOPProdType_Click(object sender, EventArgs e)
        {
            if (cboOPProdType.EditValue != null)
            {
                switch (Static.ToInt(cboOPProdType.EditValue))
                {
                    case 0:
                        {
                            InfoPos.List.InventoryList frm = new List.InventoryList(_core);
                            frm.ucInventoryList.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtOPProdNo.EditValue = frm.ucInventoryList.SelectedRow["INVID"];
                            }
                        } break;
                    case 1:
                        {
                            InfoPos.List.ServiceList frm = new List.ServiceList(_core);
                            frm.ucServiceList.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtOPProdNo.EditValue = frm.ucServiceList.SelectedRow["SERVID"];
                            }
                        } break;
                    case 2:
                        {
                            InfoPos.List.PackMainList frm = new List.PackMainList(_core);
                            frm.ucPackMain.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtOPProdNo.EditValue = frm.ucPackMain.SelectedRow["PackageId"];
                            }
                        } break;
                }
            }
            else
                MessageBox.Show("Бүтээгдэхүүний төрлөө сонгоно уу.");
        }
        private void btnOPRefresh_Click(object sender, EventArgs e)
        {
            OPRefreshProduct(Static.ToStr(txtPreSaleNo.EditValue), Static.ToLong(txtOPItemNo.EditValue));
        }
        void OPRefreshProduct(string pOrderNo, long pItemNo)
        {
            Result res = new Result();
            try
            {
                if (pOrderNo != "" || pItemNo != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130350, 130350, new object[] { pOrderNo, pItemNo });

                    if (res.ResultNo == 0)
                    {
                        grdOPProduct.DataSource = res.Data.Tables[0];
                        SetOPProduct();
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
        void SetOPProduct()
        {
            gvwOPProduct.Columns[0].Caption = "Захиалгын дугаар";
            gvwOPProduct.Columns[0].Visible = false;
            gvwOPProduct.Columns[1].Caption = "Дэс дугаар";
            gvwOPProduct.Columns[2].Caption = "Барааны дугаар";
            gvwOPProduct.Columns[3].Caption = "Барааны нэр";
            gvwOPProduct.Columns[4].Caption = "Барааны төрөл";
            gvwOPProduct.Columns[5].Caption = "Тоо ширхэг";

            for (int i = 0; i < 6; i++)
            {
                gvwOPProduct.Columns[i].OptionsColumn.AllowEdit = false;
            }
        }
        private void btnOPAdd_Click(object sender, EventArgs e)
        {
            SaveOPProduct(true);
        }
        private void btnOPEdit_Click(object sender, EventArgs e)
        {
            SaveOPProduct(false);
        }
        void SaveOPProduct(bool isnew)
        {
            Result res = new Result();
            string msg = "";

            string _retstr = CheckStatus();
            if (_retstr != "")
            {
                MessageBox.Show(_retstr);
                return;
            }

            if(Static.ToStr(txtOPProdNo.EditValue) == "")
            {
                MessageBox.Show("Бүтээгдэхүүнээ сонгоно уу");
                return;
            }

            if (Static.ToDecimal(txtOPQty.EditValue) == 0)
            {
                MessageBox.Show("Тоо ширхэгээ оруулна уу");
                return;
            }

            object[] obj = {
                                Static.ToStr(txtPreSaleNo.EditValue),
                                Static.ToLong(txtOPItemNo.EditValue),
                                Static.ToStr(txtOPProdNo.EditValue),
                                Static.ToInt(cboOPProdType.EditValue),
                                Static.ToDecimal(txtOPQty.EditValue)
                            };
            if (isnew)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130352, 130352, obj);
                msg = "Амжилттай нэмлээ.";
            }
            else
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130353, 130353, obj);
                msg = "Амжилттай засварлалаа.";
            }
            if (res.ResultNo == 0)
            {
                MessageBox.Show(msg);
                OPRefreshProduct(Static.ToStr(txtPreSaleNo.EditValue), Static.ToLong(txtOPItemNo.EditValue));
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        private void btnOPDelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130354, 130354, new object[] { Static.ToStr(txtPreSaleNo.EditValue), Static.ToStr(txtOPItemNo.EditValue), 
                Static.ToInt(cboOPProdType.EditValue), Static.ToStr(txtOPProdNo.EditValue)
                });

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    OPRefreshProduct(Static.ToStr(txtPreSaleNo.EditValue), Static.ToLong(txtOPItemNo.EditValue));
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
        void gvwOPProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwOPProduct.GetFocusedDataRow();
            if (dr != null)
            {
                FormUtility.LookUpEdit_SetValue(ref cboOPProdType, dr["ProdType"]);
                txtOPProdNo.EditValue = dr["ProdNo"];
                txtOPQty.EditValue = dr["Qty"];
            }
        }
        //OPProduct
        #endregion
        #region[Product]
        void ucGroup_EventEdit(ref bool cancel)
        {
        }
        private void RefreshProduct()
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130311, 130311, new object[] { txtPreSaleNo.EditValue });

                if (res.ResultNo == 0)
                {
                    grdProduct.DataSource = res.Data.Tables[0];
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
        private void btnCustFind_Click(object sender, EventArgs e)
        {
            InfoPos.List.CustomerList frm = new List.CustomerList(_core);
            frm.ucCustomerList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                txtCustNo.EditValue = frm.ucCustomerList.SelectedRow["CUSTOMERNO"];

                if (Static.ToInt(frm.ucCustomerList.SelectedRow["CLASSCODE"]) == 0)
                    txtCustomerName.EditValue = Static.ToStr(frm.ucCustomerList.SelectedRow["FIRSTNAME"]) + "." + Static.ToStr(frm.ucCustomerList.SelectedRow["LASTNAME"]);
                else
                    txtCustomerName.EditValue = Static.ToStr(frm.ucCustomerList.SelectedRow["CORPORATENAME"]);
            }
        }
        private void btnCustEnq_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                if (Static.ToLong(txtCustNo.EditValue) != 0)
                {
                    object[] obj1 = new object[23];
                    obj1[0] = Static.ToLong(txtCustNo.EditValue);

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120001, 120001, obj1);

                    if (res.ResultNo == 0)
                    {
                        object[] obj = new object[3];
                        obj[0] = _core;
                        obj[1] = txtCustNo.EditValue;
                        obj[2] = res.Data.Tables[0].Rows[0];
                        EServ.Shared.Static.Invoke("InfoPos.Enquiry.dll", "InfoPos.Enquiry.Main", "CallCustomerEnquiry", obj);
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
        private void txtCustNo_EditValueChanged(object sender, EventArgs e)
        {
            //if (txtCustNo.EditValue == null)
            //{
            //    txtCustomerName.EditValue = null;
            //    return;
            //}
            //Result res2 = new Result();
            //res2 = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120001, 120001, new object[] { txtCustNo.EditValue, 0 });
            //if (res2.ResultNo == 0)
            //{
            //    if (res2.Data.Tables[0].Rows.Count != 0)
            //    {
            //        string name = "";
            //        if (Static.ToInt(res2.Data.Tables[0].Rows[0]["CLASSCODE"]) == 0)         //Иргэн
            //        {
            //            name = Static.ToStr(res2.Data.Tables[0].Rows[0]["LASTNAME"]);
            //        }
            //        else
            //        {                                                       //Байгууллага
            //            name = Static.ToStr(res2.Data.Tables[0].Rows[0]["CORPORATENAME"]);
            //        }
            //        txtCustomerName.EditValue = name;
            //    }
            //}
            //else
            //    MessageBox.Show(res2.ResultNo + " : " + res2.ResultDesc);
        }
        private void btnGroupAdd_Click(object sender, EventArgs e)
        {
            SaveProduct(true);
        }
        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            SaveProduct(false);
        }
        private void SaveProduct(bool isnew)
        {
            Result res = new Result();
            string msg = "";
            object[] OldObj = null;

            string _retstr = CheckStatus();
            if (_retstr != "")
            {
                MessageBox.Show(_retstr);
                return;
            }

            if (Static.ToInt(cboProdProdType.EditValue) == 0)
            { msg += "Бүтээгдэхүүний төрлөө сонгоно уу."; }

            if (Static.ToStr(txtProdProdNo.EditValue).Trim() == "" || Static.ToStr(txtProdProdNo.EditValue) == "0")
            { msg += "\nБүтээгдэхүүнээ сонгоно уу."; }

            if (Static.ToDecimal(txtProdQty.EditValue) == 0)
            { msg += "\nТоо ширхэгээ оруулна уу."; }

            if (msg != "")
            {
                MessageBox.Show(msg);
                return;
            }

            if(!isnew)
            {
                DataRow dr = gvwProduct.GetFocusedDataRow();

                if (dr == null)
                    return;

                    OldObj = new object[] {
                                Static.ToStr(txtPreSaleNo.EditValue),
                                Static.ToInt(dr["ProdType"]),
                                Static.ToStr(dr["ProdNo"])
                            };
            }
                object[] NewObj = {
                                Static.ToStr(txtPreSaleNo.EditValue),
                                Static.ToStr(txtProdProdNo.EditValue),
                                Static.ToInt(cboProdProdType.EditValue),
                                Static.ToDecimal(txtProdQty.EditValue),
                                Static.ToDecimal(txtProdQtyMin.EditValue),
                                Static.ToDecimal(txtProdQtyMax.EditValue),
                                Static.ToInt(cboProdDiscountType.EditValue),
                                Static.ToDecimal(txtProdDiscountAmount.EditValue),
                                Static.ToDecimal(txtProdPrice.EditValue)
                            };

                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130313, 130313, NewObj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130314, 130314, new object[] { OldObj, NewObj });
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
                }
        }
        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DataRow dr = gvwProduct.GetFocusedDataRow();
                if (dr != null)
                {
                    DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == System.Windows.Forms.DialogResult.No) return;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130315, 130315, new object[] { Static.ToStr(txtPreSaleNo.EditValue), Static.ToInt(dr["prodtype"]), Static.ToStr(dr["prodno"]) });

                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");
                        RefreshProduct();
                        //RefreshPrice();
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
        private void gvwGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwProduct.GetFocusedDataRow();
            if (dr != null)
            {
                txtProdProdNo.EditValue = dr["ProdNo"];
                FormUtility.LookUpEdit_SetValue(ref cboProdProdType, dr["ProdType"]);
                txtProdQty.EditValue = dr["Qty"];
                txtProdQtyMin.EditValue = dr["QtyMin"];
                txtProdQtyMax.EditValue = dr["QtyMax"];
                FormUtility.LookUpEdit_SetValue(ref cboProdDiscountType, dr["DiscountType"]);
                txtProdDiscountAmount.EditValue = dr["DiscountAmount"];
                txtProdPrice.EditValue = dr["Price"];

                RefreshPrice(Static.ToStr(txtPreSaleNo.EditValue), Static.ToStr(dr["ProdNo"]), Static.ToInt(dr["ProdType"]));
            }
        }
        #endregion
        #region[Price]
        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            if (cboProdProdType.EditValue != null)
            {
                switch(Static.ToInt(cboProdProdType.EditValue))
                {
                    case 0 : {
                            InfoPos.List.InventoryList frm = new List.InventoryList(_core);
                            frm.ucInventoryList.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtProdProdNo.EditValue = frm.ucInventoryList.SelectedRow["INVID"];
                                txtProdPrice.EditValue = frm.ucInventoryList.SelectedRow["price"];
                            }
                        }break;
                    case 1 : {
                            InfoPos.List.ServiceList frm = new List.ServiceList(_core);
                            frm.ucServiceList.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtProdProdNo.EditValue = frm.ucServiceList.SelectedRow["SERVID"];
                                txtProdPrice.EditValue = frm.ucServiceList.SelectedRow["price"];
                            }
                        }break;
                    case 2 : {
                        InfoPos.List.PackMainList frm = new List.PackMainList(_core);
                        frm.ucPackMain.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtProdProdNo.EditValue = frm.ucPackMain.SelectedRow["PackageId"];
                                txtProdPrice.EditValue = frm.ucPackMain.SelectedRow["price"];
                            }
                        }break;
                }
            }
            else
                MessageBox.Show("Бүтээгдэхүүний төрлөө сонгоно уу.");
        }
        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = gvwPrice.GetFocusedDataRow();
            if (dr != null)
            {
                Result res = new Result();
                try
                {
                    DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == System.Windows.Forms.DialogResult.No) return;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130320, 130320, new object[] { Static.ToStr(txtPreSaleNo.EditValue), Static.ToStr(dr["prodno"]), Static.ToInt(dr["prodtype"]), Static.ToStr(dr["PriceTypeID"]) });

                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");
                        cboProdProdType.ItemIndex = 0;
                        txtProdQty.EditValue = null;
                        txtProdProdNo.EditValue = null;
                        txtPriceDiscountAmount.EditValue = null;
                        txtPricePrice.EditValue = null;

                        RefreshPrice(Static.ToStr(txtPreSaleNo.EditValue), Static.ToStr(dr["prodno"]), Static.ToInt(dr["prodtype"]));
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
        private void btnProductEdit_Click(object sender, EventArgs e)
        {
            SavePrice(false);
        }
        void SavePrice(bool isnew)
        { 
            object[] oldobj = null;
            
            string _retstr = CheckStatus();
            if (_retstr != "")
            {
                MessageBox.Show(_retstr);
                return;
            }

            if(!isnew)
            {
                DataRow dr = gvwPrice.GetFocusedDataRow();
                if(dr == null)
                    return;

                oldobj = new object[]{
                    Static.ToStr(txtPreSaleNo.EditValue),
                    Static.ToStr(dr["ProdNo"]),
                    Static.ToInt(dr["ProdType"]),
                    Static.ToStr(dr["PriceTypeID"]),
                    Static.ToInt(dr["DiscountType"]),
                    Static.ToDecimal(dr["DiscountAmount"]),
                    Static.ToDecimal(dr["Price"])
                };

            }
            string errormsg = "Дараах талбаруудыг гүйцэт бөглөнө үү.";
            if (cboPricePriceTypeID.EditValue == null)
                errormsg = errormsg + "\r\nҮнийн төрөл сонгоно уу.";

            if (cboPriceDiscountType.EditValue == null)
                errormsg = errormsg + "\r\nХөнгөлөлтийн төрлөө сонгоно уу.";

            if (errormsg != "Дараах талбаруудыг гүйцэт бөглөнө үү.")
            {
                MessageBox.Show(errormsg, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Result res = new Result();
            string msg = "";

            object[] newobj = {
                                Static.ToStr(txtPreSaleNo.EditValue),
                                Static.ToStr(txtProdProdNo.EditValue),
                                Static.ToInt(cboProdProdType.EditValue),
                                Static.ToStr(cboPricePriceTypeID.EditValue),
                                Static.ToInt(cboPriceDiscountType.EditValue),
                                Static.ToDecimal(txtPriceDiscountAmount.EditValue),
                                Static.ToDecimal(txtPricePrice.EditValue)
                            };
            if (isnew)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130318, 130318, newobj);
                msg = "Амжилттай нэмлээ.";
            }
            else
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130319, 130319, new object[] { oldobj, newobj });
                msg = "Амжилттай засварлалаа.";
            }

            if (res.ResultNo == 0)
            {
                MessageBox.Show(msg);
                RefreshPrice(Static.ToStr(txtPreSaleNo.EditValue), Static.ToStr(txtProdProdNo.EditValue), Static.ToInt(cboProdProdType.EditValue));
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            SavePrice(true);
        }
        private void RefreshPrice(string pOrderNo, string pProdNo, int ProdType)
        {
            Result res = new Result();
            grdPrice.DataSource = null;
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130316, 130316, new object[] { pOrderNo, pProdNo, ProdType });
            if (res.ResultNo == 0)
            {
                //RepositoryItemImageComboBox imagecombo = new RepositoryItemImageComboBox();
                //imagecombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
                //ImageComboBoxItem imageitem=new ImageComboBoxItem();
                //ImageCollection imgcol=new ImageCollection();
                //imgcol.AddImage(_core.Resource.GetImage("alarmclock"));
                //imagecombo.Properties.SmallImages=imgcol;
                //imageitem.Value = Static.ToDecimal(1);
                //imageitem.ImageIndex=0;
                //imagecombo.Properties.Items.Add(imageitem);

                grdPrice.DataSource = res.Data.Tables[0];

                gvwPrice.Columns[0].Caption = "Захиалгын дугаар";
                gvwPrice.Columns[0].Visible = false;
                gvwPrice.Columns[1].Caption = "Бүтээгдэхүүний дугаар";
                gvwPrice.Columns[2].Caption = "Бүтээгдэхүүний нэр";
                gvwPrice.Columns[3].Caption = "Бүтээгдэхүүний төрлийн дугаар";
                gvwPrice.Columns[3].Visible = false;
                gvwPrice.Columns[4].Caption = "Бүтээгдэхүүний төрөл";
                gvwPrice.Columns[5].Caption = "Үнийн төрөл";
                gvwPrice.Columns[5].Visible = false;
                gvwPrice.Columns[6].Caption = "Үнийн төрөл";

                gvwPrice.Columns[7].Caption = "Хөнгөлөлтийн төрөл";
                gvwPrice.Columns[7].Visible = false;
                gvwPrice.Columns[8].Caption = "Хөнгөлөлтийн төрөл";
                //gvwPrice.Columns[6].ColumnEdit = imagecombo;

                gvwProduct.Columns[7].Caption = "Хөнгөлөлтийн дүн";
                gvwProduct.Columns[7].Caption = "Дүн";

                gvwPrice.Columns[0].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[1].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[2].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[3].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[4].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[5].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[6].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[7].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[8].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[9].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[10].OptionsColumn.AllowEdit = false;

                FormUtility.RestoreStateGrid(appname, formname, ref gvwPrice);
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }

        }
        private void gvwProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwPrice.GetFocusedDataRow();
            if (dr != null)
            {
                FormUtility.LookUpEdit_SetValue(ref cboPricePriceTypeID, dr["PriceTypeID"]);
                FormUtility.LookUpEdit_SetValue(ref cboPriceDiscountType, dr["DiscountType"]);
                txtPriceDiscountAmount.EditValue = dr["DiscountAmount"];
                txtPricePrice.EditValue = dr["Price"];
            }
        }
        #endregion
        #region [ Check Status ]
        private string CheckStatus()
        {
            string msg = "";
            int status = Static.ToInt(cboStatus.EditValue);
            switch (status)
            {
                case 1: msg = "Баталгаажсан байгаа тул засварлах боломжгүй."; break;
                case 2: msg = "Гүйцэтгэгдсэн байгаа тул засварлах боломжгүй."; break;
                case 8: msg = "Хугацаа дууссан байгаа тул засварлах боломжгүй."; break;
                case 9: msg = "Цуцлагдсан байгаа тул засварлах боломжгүй."; break;
            }

            return msg;
        }
        #endregion
        private void xtraTabControl1_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (Static.ToStr(txtPreSaleNo.EditValue) == "")
                {
                   e.Cancel = true;
                   MessageBox.Show("Үндсэн мэдээллээ эхэлж үүсгэнэ үү.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            switch (e.PageIndex)
            {
                case 1:
                    RefreshPerson();
                    gvwOrderPerson.Columns[0].Caption = "Захиалгын дугаар";
                    gvwOrderPerson.Columns[0].Visible = false;
                    gvwOrderPerson.Columns[1].Caption = "Дэс дугаар";
                    gvwOrderPerson.Columns[2].Caption = "РД";
                    gvwOrderPerson.Columns[3].Caption = "Эцэг эхийн нэр";
                    gvwOrderPerson.Columns[4].Caption = "Өөрийн нэр";
                    gvwOrderPerson.Columns[5].Caption = "Овог";
                    gvwOrderPerson.Columns[6].Caption = "Хүйс";
                    gvwOrderPerson.Columns[7].Caption = "Төрсөн огноо";
                    gvwOrderPerson.Columns[8].Caption = "Email";
                    gvwOrderPerson.Columns[9].Caption = "Утас";
                    gvwOrderPerson.Columns[10].Caption = "Компани";
                    gvwOrderPerson.Columns[11].Caption = "Улсын код";
                    gvwOrderPerson.Columns[12].Caption = "Өндөр";
                    gvwOrderPerson.Columns[13].Caption = "Гутлын хэмжээ";
                    gvwOrderPerson.Columns[14].Caption = "Төхөөрөмжийн дугаар";

                    for (int i = 0; i < 15; i++)
                    {
                        gvwOrderPerson.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    break;
                case 2:
                    RefreshProduct();
                    
                    gvwProduct.Columns[0].Caption = "Захиалгын дугаар";
                    gvwProduct.Columns[0].Visible = false;
                    
                    gvwProduct.Columns[1].Caption = "Бүтээгдэхүүний дугаар";
                    gvwProduct.Columns[2].Caption = "Бүтээгдэхүүний нэр";
                    gvwProduct.Columns[3].Caption = "Бүтээгдэхүүний төрөл";
                    gvwProduct.Columns[3].Visible = false;
                    gvwProduct.Columns[4].Caption = "Бүтээгдэхүүний төрөл";
                    
                    gvwProduct.Columns[4].Caption = "Тоо ширхэг";
                    gvwProduct.Columns[5].Caption = "Тоо ширхэгийн доод хязгаар";
                    gvwProduct.Columns[6].Caption = "Тоо ширхэгийн дээд хязгаар";
                    gvwProduct.Columns[7].Caption = "Хөнгөлөлтийн төрөл";
                    gvwProduct.Columns[7].Visible = false;
                    gvwProduct.Columns[8].Caption = "Хөнгөлөлтийн төрөл";
                    gvwProduct.Columns[9].Caption = "Хөнгөлөлтийн дүн";
                    gvwProduct.Columns[10].Caption = "Бараа үйлчилгээний үнэ";

                    FormUtility.RestoreStateGrid(appname, formname, ref gvwProduct);
                    for (int i = 0; i < 11; i++)
                    {
                        gvwProduct.Columns[i].OptionsColumn.AllowEdit = false;
                    }

                    break;
            }
        }
        private void cboProdProdType_EditValueChanged(object sender, EventArgs e)
        {
            txtProdProdNo.EditValue = null;
        }
        private void gvwProduct_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gvwPrice.GetFocusedDataRow();

            if (Static.ToInt(dr["ISSCHEDULE"]) == 1)
            {
                InfoPos.Schedule.frmSchedul frm = new Schedule.frmSchedul(_core, Static.ToStr(dr["UNIT"]), Static.ToInt(dr["Duration"]), Static.ToStr(dr["ORDERNO"]), Static.ToLong(dr["GROUPNO"]), Static.ToStr(dr["PRODNO"]), Static.ToInt(dr["COUNT"]), txtCustomerName.Text);
                frm.ShowDialog();
            }
            else { MessageBox.Show("Хуваарь тохируулаагүй байна.","Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }
        private void btnOwnerFind_Click(object sender, EventArgs e)
        {
            InfoPos.List.UserList frm = new List.UserList(_core);
            frm.ucUserList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                txtSalesUser.EditValue = frm.ucUserList.SelectedRow["USERNO"];
            }
        }
        private void frmPreSale_Load(object sender, EventArgs e)
        {

        }
        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void cboOPProdType_EditValueChanged(object sender, EventArgs e)
        {
            txtOPProdNo.EditValue = null;
            txtOPQty.EditValue = 0;
        }
    }
}
