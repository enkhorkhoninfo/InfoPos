using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISM.Template;
using EServ.Shared;
using InfoPos.List;

namespace InfoPos.Parameter
{
    public partial class FormPackMain : Form
    {
        #region[Variables]
        Core.Core _core;
        string _pPackID = "";
        int PrivNo = 110100;
        Form FormName = null;
        int btn = 0;
        string appname = "", formname = "";
        bool loadInv = true;
        object[] oldValue = new object[1];
        long oldValueCust = 0;
        object[] OldValueucItem;
        string  oldValueUser = "";
        Result res = new Result();
        decimal pPrice = 0;
        object[] pOldValue;
        #endregion[]
        public FormPackMain(Core.Core core, string pPackID)
        {
            InitializeComponent();
            _core = core;
            _pPackID = pPackID;
            Init();
            InitCombo();
            ucPackMain.Resource = core.Resource;
            ucPackItem.Resource = core.Resource;
            ucPackCust.Resource = core.Resource;
            ucPackUser.Resource = core.Resource;
            btnInv.Image = core.Resource.GetImage("button_find");
            btnUser.Image = core.Resource.GetImage("button_find");
            btnCust.Image = core.Resource.GetImage("button_find");
            //btnSalesUser.Image = core.Resource.GetImage("button_find");
        }
        #region[Init]
        private void Init()
        {
            #region[ucPackMain]
            ucPackMain.EventSave += new ISM.Template.ucTogglePanel.delegateEventSave(ucPackMain_EventSave);
            ucPackMain.EventExit += new ISM.Template.ucTogglePanel.delegateEventExit(ucPackMain_EventExit);
            ucPackMain.EventDelete += new ISM.Template.ucTogglePanel.delegateEventDelete(ucPackMain_EventDelete);
            ucPackMain.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucPackMain_EventAddAfter);
            ucPackMain.EventReject += new ucTogglePanel.delegateEventReject(ucPackMain_EventReject);

            ucPackMain.FieldLinkAdd("txtPackageId", 0, "PackageId", "", true, true);
            ucPackMain.FieldLinkAdd("txtName", 0, "Name", "", true, false);
            ucPackMain.FieldLinkAdd("txtName2", 0, "Name2", "", false, false);
            ucPackMain.FieldLinkAdd("cboBrandID", 0, "BrandID", "", true, false);
            ucPackMain.FieldLinkAdd("txtBarCode", 0, "BarCode", "", false, false);
            ucPackMain.FieldLinkAdd("cboUnit", 0, "Unit", "", false, false);
            ucPackMain.FieldLinkAdd("numUnitSize", 0, "UnitSize", "", false, false);
            ucPackMain.FieldLinkAdd("cboStatus", 0, "Status", "", true, false);
            ucPackMain.FieldLinkAdd("numPrice", 0, "Price", "", true, false);
            ucPackMain.FieldLinkAdd("numCount", 0, "Count", "", false, false);
            ucPackMain.FieldLinkAdd("dtCreateDate", 0, "CreateDate", "", true, false);
            ucPackMain.FieldLinkAdd("dtSalesStartDate", 0, "SalesStartDate", "", true, false);
            ucPackMain.FieldLinkAdd("dtSalesEndDate", 0, "SalesEndDate", "", true, false);
            ucPackMain.FieldLinkAdd("txtNote", 0, "Note", "", false, false);

            ucPackMain.FieldLinkAdd("txtSalesAccountNo", 0, "SALESACCOUNTNO", "", false, false);
            ucPackMain.FieldLinkAdd("txtRefundAccountNo", 0, "REFUNDACCOUNTNO", "", false, false);
            ucPackMain.FieldLinkAdd("txtDiscountAccountNo", 0, "DISCOUNTACCOUNTNO", "", false, false);
            ucPackMain.FieldLinkAdd("txtBonusAccountNo", 0, "BONUSACCOUNTNO", "", false, false);
            ucPackMain.FieldLinkAdd("txtBONUSEXPACCOUNTNO", 0, "BONUSEXPACCOUNTNO", "", false, false);

            ucPackMain.ToggleShowDelete = true;
            ucPackMain.ToggleShowEdit = true;
            ucPackMain.ToggleShowExit = true;
            ucPackMain.ToggleShowNew = true;
            ucPackMain.ToggleShowReject = true;
            ucPackMain.ToggleShowSave = true;
            ucPackMain.DataSource = null;
            #endregion[]
            #region[ucPackItem]
            ucPackItem.EventSave += new ucTogglePanel.delegateEventSave(ucPackItem_EventSave);
            ucPackItem.EventExit += new ucTogglePanel.delegateEventExit(ucPackItem_EventExit);
            ucPackItem.EventDelete += new ucTogglePanel.delegateEventDelete(ucPackItem_EventDelete);
            ucPackItem.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucPackItem_EventAddAfter);
            ucPackItem.EventReject += new ucTogglePanel.delegateEventReject(ucPackItem_EventReject);
            ucPackItem.EventEdit += new ucTogglePanel.delegateEventEdit(ucPackItem_EventEdit);


            ucPackItem.FieldLinkAdd("PackageId", 0, "PackageId", "", false, true);
            ucPackItem.FieldLinkAdd("txtProdId", 0, "ProdId", "", true, false);
            ucPackItem.FieldLinkAdd("cboProdType", 0, "ProdType", "", false, false);
            ucPackItem.FieldLinkAdd("numCount", 0, "Count", "", false, false);
            ucPackItem.FieldLinkAdd("cboOptional", 0, "Optional", "", true, false);
            ucPackItem.FieldLinkAdd("numItemPrice", 0, "Price", "", true, false);

            ucPackItem.ToggleShowDelete = true;
            ucPackItem.ToggleShowEdit = true;
            ucPackItem.ToggleShowExit = true;
            ucPackItem.ToggleShowNew = true;
            ucPackItem.ToggleShowReject = true;
            ucPackItem.ToggleShowSave = true;

            ucPackItem.DataSource = null;
            ucPackItem.GridView = gvwPackItem;
            #endregion[]
            #region[ucPackCust]
            ucPackCust.EventSave += new ucTogglePanel.delegateEventSave(ucPackCust_EventSave);
            ucPackCust.EventExit += new ucTogglePanel.delegateEventExit(ucPackCust_EventExit);
            ucPackCust.EventDelete += new ucTogglePanel.delegateEventDelete(ucPackCust_EventDelete);
            ucPackCust.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucPackCust_EventAddAfter);
            ucPackCust.EventReject += new ucTogglePanel.delegateEventReject(ucPackCust_EventReject);
            ucPackCust.EventEdit += new ucTogglePanel.delegateEventEdit(ucPackCust_EventEdit);

            ucPackCust.FieldLinkAdd("txtPackageIdCust", 0, "PackageId", "", false, true);
            ucPackCust.FieldLinkAdd("numCustNo", 0, "CustNo", "", true, false);

            ucPackCust.ToggleShowDelete = true;
            ucPackCust.ToggleShowEdit = true;
            ucPackCust.ToggleShowExit = true;
            ucPackCust.ToggleShowNew = true;
            ucPackCust.ToggleShowReject = true;
            ucPackCust.ToggleShowSave = true;

            ucPackCust.DataSource = null;
            ucPackCust.GridView = gwvPackCust;
            #endregion[]
            #region[ucPackUser]
            ucPackUser.EventSave += new ucTogglePanel.delegateEventSave(ucPackUser_EventSave);
            ucPackUser.EventExit += new ucTogglePanel.delegateEventExit(ucPackUser_EventExit);
            ucPackUser.EventDelete += new ucTogglePanel.delegateEventDelete(ucPackUser_EventDelete);
            ucPackUser.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucPackUser_EventAddAfter);
            ucPackUser.EventReject += new ucTogglePanel.delegateEventReject(ucPackUser_EventReject);
            ucPackUser.EventEdit += new ucTogglePanel.delegateEventEdit(ucPackUser_EventEdit);

            ucPackUser.FieldLinkAdd("numPackageID", 0, "PackageId", "", false, true);
            ucPackUser.FieldLinkAdd("numUserNo", 0, "Userno", "", true, false);

            ucPackUser.ToggleShowDelete = true;
            ucPackUser.ToggleShowEdit = true;
            ucPackUser.ToggleShowExit = true;
            ucPackUser.ToggleShowNew = true;
            ucPackUser.ToggleShowReject = true;
            ucPackUser.ToggleShowSave = true;

            ucPackUser.DataSource = null;
            ucPackUser.GridView = gvwPackUser;
            #endregion[]
            #region[ucProdPrice]

            ucProdPrice.EventSave += new ucTogglePanel.delegateEventSave(ucProdPrice_EventSave);
            ucProdPrice.EventExit += new ucTogglePanel.delegateEventExit(ucProdPrice_EventExit);
            ucProdPrice.EventDelete += new ucTogglePanel.delegateEventDelete(ucProdPrice_EventDelete);
            ucProdPrice.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucProdPrice_EventAddAfter);
            ucProdPrice.EventReject += new ucTogglePanel.delegateEventReject(ucProdPrice_EventReject);
            ucProdPrice.EventEdit += new ucTogglePanel.delegateEventEdit(ucProdPrice_EventEdit);

            ucProdPrice.FieldLinkAdd("numPriceProdType", 0, "ProdType", "", false, true);
            ucProdPrice.FieldLinkAdd("txtPricePackID", 0, "prodid", "", true, false);
            ucProdPrice.FieldLinkAdd("cboPriceTypeID", 0, "pricetypeid", "", true, false);
            ucProdPrice.FieldLinkAdd("txtPricePrice", 0, "price", "", true, false);

            ucProdPrice.ToggleShowDelete = true;
            ucProdPrice.ToggleShowEdit = true;
            ucProdPrice.ToggleShowExit = true;
            ucProdPrice.ToggleShowNew = true;
            ucProdPrice.ToggleShowReject = true;
            ucProdPrice.ToggleShowSave = true;

            ucProdPrice.DataSource = null;
            ucProdPrice.GridView = gvwPrice;
            #endregion[]
        }                
        private void InitCombo()
        {
            #region[ucPackMain]
            try
            {
                Result res = new Result();
                ArrayList Tables = new ArrayList();
                DataTable DT = null;
                string msg = "";

                DictUtility.PrivNo = PrivNo;
                string[] name = new string[] { "UNITTYPECODE", "BRAND", "PAPRICETYPE" };

                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д UNITTYPECODE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboUnit, DT, "UNITTYPECODE", "name");
                }

                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д BRAND оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboBrandID, DT, "BRANDID", "NAME");
                }

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д PAPRICETYPE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboPriceTypeID, DT, "PRICETYPEID", "NAME");
                }

                if (msg != "")
                    MessageBox.Show(msg);

                FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Идэвхгүй");
                FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Идэвхтэй");
            #endregion[]
            #region[ucPackItem]
            FormUtility.LookUpEdit_SetList(ref cboProdType, 0, "Бараа материал");
            FormUtility.LookUpEdit_SetList(ref cboProdType, 1, "Үйлчилгээ");
            FormUtility.LookUpEdit_SetList(ref cboProdType, 2, "Багц");

            FormUtility.LookUpEdit_SetList(ref cboOptional, 0, "Уг барааг заавал борлуулна");
            FormUtility.LookUpEdit_SetList(ref cboOptional, 1, "Уг бараа өөр ийм төрөлтэй бараанаас аль нэгийг сонгож болно");
            #endregion[]
            }
            catch (Exception ex)
            {
                MessageBox.Show("Өгөгдлийн баазаас Dictionary олдсонгүй.");
            }
        }
        #endregion[]
        #region[btn&FormEvents]
        private void FormPackMain_Load(object sender, EventArgs e)
        {
            this.Show();

            FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            if (_pPackID == "")
            {
                ucPackMain.FieldLinkSetNewState();
                ucPackItem.FieldLinkSetNewState();
                ucPackCust.FieldLinkSetNewState();
                ucPackUser.FieldLinkSetNewState();
                ucProdPrice.FieldLinkSetNewState();

                cboStatus.EditValue = 1;
                cboUnit.ItemIndex = 0;
                cboProdType.ItemIndex = 0;
                cboStatus.EditValue = 1;
                cboOptional.ItemIndex = 0;

                dtSalesEndDate.EditValue = _core.TxnDate;
                dtCreateDate.EditValue = _core.TxnDate;
                dtSalesStartDate.EditValue = _core.TxnDate;
            }
            else if (_pPackID != "")
            {
                PackageId.Text = _pPackID;
                txtPackageIdCust.EditValue = _pPackID;
                numPackageID.EditValue = _pPackID;

                RefreshData(_pPackID);
                ucPackItemRefreshData(_pPackID);
                ucPackCustRefreshData(_pPackID);
                ucPackUserRefreshData(_pPackID);

                ucPackMain.FieldLinkSetValues();
                ucPackItem.FieldLinkSetValues();
                ucPackCust.FieldLinkSetValues();
                ucPackUser.FieldLinkSetValues();

                ucPackMain.FieldLinkSetSaveState();
                ucPackItem.FieldLinkSetSaveState();
                ucPackCust.FieldLinkSetSaveState();
                ucPackUser.FieldLinkSetSaveState();
            }
        }
        private void gvwPackItem_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ucPackItem.FieldLinkSetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Err");
            }
        }
        private void gvwPackUser_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ucPackUser.FieldLinkSetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Err");
            }
        }
        private void gwvPackCust_FocusedRowChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ucPackCust.FieldLinkSetValues();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "Err"); }
        }
        private void tabPackMain_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            if (loadInv == true)
                ucPackItemRefreshData(txtPackageId.EditValue.ToString());
            ucPackItem.FieldLinkSetSaveState();

            ucPackCustRefreshData(txtPackageId.EditValue.ToString());
            ucPackCust.FieldLinkSetSaveState();

            ucPackUserRefreshData(txtPackageId.EditValue.ToString());
            ucPackUser.FieldLinkSetSaveState();
        }
        private void tabPackMain_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (txtPackageId.Text == "")
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
        private void btnInv_Click(object sender, EventArgs e)
        {
            switch (Static.ToInt(cboProdType.EditValue))
            {
                case 0:
                    {
                        InfoPos.List.InventoryList frm = new InfoPos.List.InventoryList(_core);
                        frm.ucInventoryList.Browsable = true;
                        DialogResult res = frm.ShowDialog();
                        if ((res == System.Windows.Forms.DialogResult.OK))
                        {
                            txtProdId.Text = Static.ToStr(frm.ucInventoryList.SelectedRow["INVID"]);
                        }
                    }
                    break;
                case 1:
                    {
                        InfoPos.List.ServiceList frm = new InfoPos.List.ServiceList(_core);
                        frm.ucServiceList.Browsable = true;
                        DialogResult res = frm.ShowDialog();
                        if ((res == System.Windows.Forms.DialogResult.OK))
                        {
                            txtProdId.Text = Static.ToStr(frm.ucServiceList.SelectedRow["SERVID"]);
                        }
                    }
                    break;
                case 2:
                    {
                        InfoPos.List.PackMainList frm = new InfoPos.List.PackMainList(_core);
                        frm.ucPackMain.Browsable = true;
                        DialogResult res = frm.ShowDialog();
                        if ((res == System.Windows.Forms.DialogResult.OK))
                        {
                            txtProdId.Text = Static.ToStr(frm.ucPackMain.SelectedRow["packageid"]);
                        }
                    }
                    break;
            }
        }
        private void btnUser_Click(object sender, EventArgs e)
        {
            InfoPos.List.UserList frm = new InfoPos.List.UserList(_core);
            frm.ucUserList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                numUserNo.Text = Static.ToStr(frm.ucUserList.SelectedRow["UserNo"]);
            }
        }
        private void btnCust_Click(object sender, EventArgs e)
        {
            try
            {
                InfoPos.List.CustomerList frm = new InfoPos.List.CustomerList(_core);
                frm.ucCustomerList.Browsable = true;
                DialogResult res = frm.ShowDialog();
                if ((res == System.Windows.Forms.DialogResult.OK))
                {
                    numCustNo.Text = Static.ToStr(frm.ucCustomerList.SelectedRow["CUSTOMERNO"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void FormPackMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormPackMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            this.Close();
        }
        #endregion[]
        #region[ucPackMain]
        void ucPackMain_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140250, 140250, new object[] { txtPackageId.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        ucPackMain.FieldLinkSetNewState();
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucPackMain_EventExit(bool editing, ref bool cancel)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            this.Close();
        }
        void ucPackMain_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucPackMain.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtPackageId.EditValue) != "")
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
        private void RefreshData(string pPackID)
        {
            Result res = new Result();
            DataSet ds = null;
            try
            {
                if (Static.ToStr(pPackID) != "")
                {
                    ucPackMain.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140247, 140247, new object[] { pPackID });
                    if (res.ResultNo == 0)
                    {
                        ucPackMain.DataSource = res.Data;
                        loadInv = true;

                        ds = res.Data;
                        byte[] a = null;

                        if (ds.Tables[0].Rows[0]["Picture"] != null && ds.Tables[0].Rows[0]["Picture"] != DBNull.Value && ds.Tables[0].Rows[0]["Picture"] != "")
                            a = (byte[])ds.Tables[0].Rows[0]["Picture"];

                        picPicture.Image = Static.ImageFromByte(a);

                        //DataSet DS = new DataSet();
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
        void EventSave(bool isnew, ref bool cancel)
        {
            try
            {
                Result r = new Result();

                byte[] _img = null;
                if (picPicture.Image != null)
                    _img = Static.ImageToByte(picPicture.Image);

                object[] NewValue = {   
                                        Static.ToStr(txtPackageId.EditValue),
                                        Static.ToStr(txtName.EditValue),
                                        Static.ToStr(txtName2.EditValue),
                                        Static.ToStr(cboBrandID.EditValue),
                                        Static.ToStr(txtBarCode.EditValue),

                                        Static.ToStr(cboUnit.EditValue),
                                        Static.ToStr(numUnitSize.EditValue),
                                        Static.ToInt(cboStatus.EditValue),
                                        Static.ToDecimal(numPrice.EditValue),
                                        Static.ToDecimal(txtCount.EditValue),

                                        Static.ToDate(dtCreateDate.EditValue),
                                        Static.ToDate(dtSalesStartDate.EditValue),
                                        Static.ToDate(dtSalesEndDate.EditValue),
                                        Static.ToStr(txtNote.EditValue),
                                        Static.ToStr(txtSalesAccountNo.EditValue),

                                        Static.ToStr(txtRefundAccountNo.EditValue),                                      
                                        Static.ToStr(txtDiscountAccountNo.EditValue),
                                        Static.ToStr(txtBonusAccountNo.EditValue),
                                        Static.ToStr(txtBonusExpAccountNo.EditValue),
                                        _img
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140249, 140249, new object[] { NewValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай засварлалаа.");
                        RefreshData(_pPackID);
                    }
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140248, 140248, new object[] { NewValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай нэмлээ .");
                        RefreshData(_pPackID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
        }
        void ucPackMain_EventAddAfter()
        {
            cboStatus.EditValue = 1;
            cboUnit.ItemIndex = 0;
            cboBrandID.ItemIndex = 0;
            cboProdType.ItemIndex = 0;
            cboStatus.EditValue = 1;
            PackageId.Text = _pPackID;

            dtSalesEndDate.EditValue = _core.TxnDate;
            dtCreateDate.EditValue = _core.TxnDate;
            dtSalesStartDate.EditValue = _core.TxnDate;
        }
        void ucPackMain_EventReject()
        {
            RefreshData(Static.ToStr(txtPackageId.EditValue));

            cboStatus.EditValue = 1;
            cboUnit.ItemIndex = 0;
            cboBrandID.ItemIndex = 0;
            cboProdType.ItemIndex = 0;
            cboStatus.EditValue = 1;

            dtSalesEndDate.EditValue = _core.TxnDate;
            dtCreateDate.EditValue = _core.TxnDate;
            dtSalesStartDate.EditValue = _core.TxnDate;
        }
        #endregion[]
        #region[ucPackItem]
        void ucPackItem_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140255, 140255, new object[] { txtPackageId.EditValue, txtProdId.EditValue, cboProdType.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        ucPackItemRefreshData(Static.ToStr(txtPackageId.EditValue));
                        ucPackItem.FieldLinkSetValues();
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucPackItem_EventExit(bool editing, ref bool cancel)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            this.Close();
        }
        void ucPackItem_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucPackItem.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtPackageId.Text) != "")
                {
                    ucPackItemEventSave(isnew, ref cancel);
                    ucPackItemRefreshData(txtPackageId.EditValue.ToString());
                    ucPackItem.FieldLinkSetValues();
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
        void ucPackItemEventSave(bool isnew, ref bool cancel)
        {
            try
            {
                Result r = new Result();
                object[] NewValue = {   Static.ToStr(txtPackageId.EditValue),
                                        Static.ToStr(txtProdId.EditValue),
                                        Static.ToInt(cboProdType.EditValue),
                                        Static.ToInt(numCount.EditValue),
                                        Static.ToInt(cboOptional.EditValue)
                                    };

                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140254, 140254, new object[] { OldValueucItem, NewValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай засварлалаа.");
                    }
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140253, 140253, new object[] { NewValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай нэмлээ .");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
        }
        void ucPackItem_EventAddAfter()
        {
            PackageId.EditValue = txtPackageId.Text;
            cboProdType.ItemIndex = 0;
            cboOptional.ItemIndex = 0;
        }
        void ucPackItem_EventReject()
        {
            PackageId.EditValue = txtPackageId.Text;
            cboProdType.ItemIndex = 0;
            cboOptional.ItemIndex = 0;
            ucPackItemRefreshData(Static.ToStr(txtPackageId.EditValue));
        }
        private void ucPackItemRefreshData(string pPackID)
        {
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtPackageId.EditValue) != "")
                {
                    pPrice = Static.ToDecimal(numPrice.EditValue);

                    grdPackItem.DataSource = null;
                    ucPackItem.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140251, 140251, new object[] { txtPackageId.EditValue }); //pPackId, pProdID, pProdType);
                    if (res.ResultNo == 0)
                    {
                        grdPackItem.DataSource = res.Data.Tables[0];
                        ucPackItem.DataSource = res.Data;
                        ucPackItemSetData();
                        loadInv = true;
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                    }
                }
                PackageId.Text = txtPackageId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucPackItemSetData()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdPackItem);
                gvwPackItem.Columns[0].Caption = "Багцын дугаар";
                gvwPackItem.Columns[0].Visible = true;
                gvwPackItem.Columns[0].OptionsColumn.AllowEdit = false;
                gvwPackItem.Columns[1].Caption = "Бүтээгдэхүүний код ";
                gvwPackItem.Columns[1].Visible = true;
                gvwPackItem.Columns[1].OptionsColumn.AllowEdit = false;
                gvwPackItem.Columns[2].Caption = "Төрлийн дугаар";
                gvwPackItem.Columns[2].Visible = true;
                gvwPackItem.Columns[2].OptionsColumn.AllowEdit = false;
                gvwPackItem.Columns[3].Caption = "Бүтээгдэхүүний тоо ширхэг";
                gvwPackItem.Columns[3].Visible = true;
                gvwPackItem.Columns[3].OptionsColumn.AllowEdit = false;
                gvwPackItem.Columns[4].Caption = "Сонголтын төлөв";
                gvwPackItem.Columns[4].Visible = true;
                gvwPackItem.Columns[4].OptionsColumn.AllowEdit = false;
                gvwPackItem.Columns[5].Caption = "Тоо ширхэг";
                gvwPackItem.Columns[5].Visible = true;
                gvwPackItem.Columns[5].OptionsColumn.AllowEdit = false;
                gvwPackItem.Columns[6].Caption = "Сонголтын дугаар";
                gvwPackItem.Columns[6].Visible = true;
                gvwPackItem.Columns[6].OptionsColumn.AllowEdit = false;
                gvwPackItem.Columns[7].Caption = "Сонголтын нэр";
                gvwPackItem.Columns[7].Visible = true;
                gvwPackItem.Columns[7].OptionsColumn.AllowEdit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucPackItem_EventEdit(ref bool cancel)
        {
            object[] Value = {          Static.ToStr(txtPackageId.EditValue),
                                        Static.ToStr(txtProdId.EditValue),
                                        Static.ToInt(cboProdType.EditValue),
                                        Static.ToInt(numCount.EditValue),
                                        Static.ToInt(cboOptional.EditValue),
                                        Static.ToDecimal(cboOptional.EditValue)
                             };
            OldValueucItem = Value;
        }
        #endregion[]
        #region[ucPackUser]
        void ucPackUser_EventReject()
        {
            numPackageID.EditValue = txtPackageId.Text;
        }
        void ucPackUser_EventAddAfter()
        {
            numPackageID.EditValue = txtPackageId.Text;
        }
        void ucPackUser_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140265, 140265, new object[] {txtPackageId.EditValue, numUserNo.EditValue });  //pPackId, pUserNo);
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        ucPackUserRefreshData(txtPackageId.EditValue.ToString());                        
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucPackUser_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void ucPackUser_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucPackUser.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtPackageId.EditValue) != "")
                {
                    ucPackUserEventSave(isnew, ref cancel);
                    ucPackUserRefreshData(_pPackID);
                    ucPackUser.FieldLinkSetValues();
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
        void ucPackUser_EventEdit(ref bool cancel)
        {
            oldValueUser = Static.ToStr(numUserNo.EditValue);
        }        
        void ucPackUserEventSave(bool isnew, ref bool cancel)
        {
            try
            {
                Result r = new Result();
                object[] NewValue = {   Static.ToStr(txtPackageId.EditValue),
                                        Static.ToInt(numUserNo.EditValue)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140264, 140264, new object[] { txtPackageId.EditValue, oldValueUser, numUserNo.Text });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай засварлалаа.");
                    }
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140263, 140263, new object[] { NewValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай нэмлээ .");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
        }
        private void ucPackUserRefreshData(string pPackID)
        {
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtPackageId.EditValue) != "")
                {
                    grdPackUser.DataSource = null;                    
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140261, 140261, new object[] { txtPackageId.EditValue }); 
                    if (res.ResultNo == 0)
                    {
                        grdPackUser.DataSource = res.Data.Tables[0];
                        ucPackUser.DataSource = res.Data;
                        ucPackUserSetData();
                        loadInv = true;
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                    }
                }
                PackageId.Text = txtPackageId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucPackUserSetData()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdPackUser);
                gvwPackUser.Columns[0].Caption = "Багцын дугаар";
                gvwPackUser.Columns[0].Visible = true;
                gvwPackUser.Columns[0].OptionsColumn.AllowEdit = false;
                gvwPackUser.Columns[1].Caption = "Хэрэглэгчийн дугаар";
                gvwPackUser.Columns[1].Visible = true;
                gvwPackUser.Columns[1].OptionsColumn.AllowEdit = false;


                gvwPackUser.Columns[2].Caption = "Хэрэглэгчийн овог";
                gvwPackUser.Columns[2].Visible = true;
                gvwPackUser.Columns[2].OptionsColumn.AllowEdit = false;
                gvwPackUser.Columns[3].Caption = "Хэрэглэгчийн нэр";
                gvwPackUser.Columns[3].Visible = true;
                gvwPackUser.Columns[3].OptionsColumn.AllowEdit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion[]        
        #region[ucPackCust]
        void ucPackCust_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140260, 140260, new object[] { txtPackageId.EditValue, numCustNo.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        ucPackCustRefreshData(txtPackageId.EditValue.ToString());                        
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucPackCust_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void ucPackCust_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucPackCust.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtPackageId.EditValue) != "")
                {
                    ucPackCustEventSave(isnew, ref cancel);
                    ucPackCustRefreshData(Static.ToStr(txtPackageId.EditValue));
                    ucPackCust.FieldLinkSetValues();
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
        void ucPackCustEventSave(bool isnew, ref bool cancel)
        {
            try
            {
                Result r = new Result();
                object[] NewValue = {   Static.ToStr(txtPackageId.Text),
                                        Static.ToLong(numCustNo.EditValue)                                                                           
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140259, 140259, new object[] { txtPackageId.EditValue, oldValueCust, numCustNo.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай засварлалаа.");                        
                    }
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140258, 140258, new object[] { NewValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай нэмлээ .");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
        }
        private void ucPackCustRefreshData(string pPackID)
        {            
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtPackageId.EditValue) != "")
                {
                    //grdPackCust.DataSource = null;
                    //ucPackCust.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140256, 140256, new object[] { txtPackageId.EditValue });
                    if (res.ResultNo == 0)
                    {
                        grdPackCust.DataSource = res.Data.Tables[0];
                        ucPackCust.DataSource = res.Data;
                        ucPackCustSetData();
                        loadInv = true;                        
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                    }
                }
                ucPackCust.FieldLinkSetValues();
                PackageId.Text = txtPackageId.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucPackCustSetData()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdPackCust);
                gwvPackCust.Columns[0].Caption = "Багцын дугаар";
                gwvPackCust.Columns[0].Visible = true;
                gwvPackCust.Columns[0].OptionsColumn.AllowEdit = false;
                gwvPackCust.Columns[1].Caption = "Үйлчлүүлэгчийн дугаар";
                gwvPackCust.Columns[1].Visible = true;
                gwvPackCust.Columns[1].OptionsColumn.AllowEdit = false;
                gwvPackCust.Columns[2].Caption = "Овог";
                gwvPackCust.Columns[2].Visible = true;
                gwvPackCust.Columns[2].OptionsColumn.AllowEdit = false;
                gwvPackCust.Columns[3].Caption = "Нэр";
                gwvPackCust.Columns[3].Visible = true;
                gwvPackCust.Columns[3].OptionsColumn.AllowEdit = false;
                gwvPackCust.Columns[4].Caption = "Байгууллагын нэр";
                gwvPackCust.Columns[4].Visible = true;
                gwvPackCust.Columns[4].OptionsColumn.AllowEdit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucPackCust_EventAddAfter()
        {
            txtPackageIdCust.EditValue = txtPackageId.Text;
        }
        void ucPackCust_EventReject()
        {
            txtPackageIdCust.EditValue = txtPackageId.Text;
            //ucPackCustRefreshData(Static.ToStr(txtPackId.EditValue));
        }
        void ucPackCust_EventEdit(ref bool cancel)
        {
            oldValueCust = Static.ToLong(numCustNo.EditValue);
        }
        #endregion[]        
        #region[Price]
        void ucProdPrice_EventEdit(ref bool cancel)
        {
            object[] Value = {      Static.ToInt(2), 
                                   Static.ToStr(txtPackageId.EditValue),
                                   Static.ToStr(cboPriceTypeID.EditValue),
                                   Static.ToInt(txtPricePrice.EditValue)};
            pOldValue = Value;
            if (_pPackID == "")
            {
                txtPricePackID.EditValue = txtPackageId.EditValue;
                numPriceProdType.EditValue = 2;
                cboPriceTypeID.ItemIndex = 0;
            }
        }
        void ucProdPrice_EventReject()
        {
            txtPricePackID.EditValue = txtPackageId.EditValue;
            numPriceProdType.EditValue = 0;
            cboPriceTypeID.ItemIndex = 0;
        }
        void ucProdPrice_EventAddAfter()
        {
            txtPricePackID.EditValue = txtPackageId.EditValue;
            numPriceProdType.EditValue = 2;
            cboPriceTypeID.ItemIndex = 0;
        }
        void ucProdPrice_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void ucProdPrice_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucProdPrice.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(cboPriceTypeID.EditValue) != "")
                {
                    ucProdPriceEventSave(isnew, ref cancel);
                    RefreshPrice(Static.ToStr(txtPackageId.EditValue));
                    ucProdPrice.FieldLinkSetValues();
                }
                else
                {
                    MessageBox.Show("Үнийн төрлөө сонгоно уу");
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
        void ucProdPriceEventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucProdPrice.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = {    
                                   Static.ToInt(2), 
                                   Static.ToStr(txtPackageId.EditValue),
                                   Static.ToStr(cboPriceTypeID.EditValue),
                                   Static.ToInt(txtPricePrice.EditValue)
                               };
                string msg = "";
                try
                {
                    if (isnew)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140433, 140433, new object[] { obj });
                        msg = "Амжилттай нэмлээ";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140434, 140434, new object[] { pOldValue, obj });
                        msg = "Амжилттай засварлалаа";
                    }
                    if (res.ResultNo == 0)
                    {
                        RefreshPrice(Static.ToStr(txtPackageId.EditValue));
                        ucProdPrice.FieldLinkSetSaveState();
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
        private void RefreshPrice(string pPackID)
        {
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtPackageId.EditValue) != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140431, 140431, new object[] { 2, pPackID });
                    if (res.ResultNo == 0)
                    {
                        grdPrice.DataSource = res.Data.Tables[0];
                        ucProdPrice.DataSource = res.Data;
                        SetPrice();
                        loadInv = true;
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                    }
                }
                else
                {
                }
                txtPricePackID.EditValue = txtPackageId.EditValue;
                numPriceProdType.EditValue = 2;
                cboPriceTypeID.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetPrice()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdPrice);
                gvwPrice.Columns[0].Caption = "Төрлийн дугаар";
                gvwPrice.Columns[0].Visible = true;
                gvwPrice.Columns[0].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[1].Caption = "Багцын код";
                gvwPrice.Columns[1].Visible = true;
                gvwPrice.Columns[1].OptionsColumn.AllowEdit = false;


                gvwPrice.Columns[2].Caption = "Үнийн төрлийн код";
                gvwPrice.Columns[2].Visible = true;
                gvwPrice.Columns[2].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[3].Caption = "Үнийн төрлийн нэр";
                gvwPrice.Columns[3].Visible = true;
                gvwPrice.Columns[3].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[4].Caption = "Эдгээр нөхцөл дэх үнэ";
                gvwPrice.Columns[4].Visible = true;
                gvwPrice.Columns[4].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[5].Caption = "Өдрийн төрөл";
                gvwPrice.Columns[5].Visible = true;
                gvwPrice.Columns[5].OptionsColumn.AllowEdit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ucProdPrice_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140435, 140435, new object[] { 2, _pPackID, cboPriceTypeID.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        btn = 1;
                    }
                    RefreshPrice(Static.ToStr(txtPackageId.EditValue));
                    ucProdPrice.FieldLinkSetValues();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion[]        
        private void btnPicEnter_Click(object sender, EventArgs e)
        {
            ISM.Template.FormImage img = new FormImage();
            img.Resource = _core.Resource;
            img.ShowDialog();
            if (img.DialogResult == System.Windows.Forms.DialogResult.OK)
                picPicture.Image = img.ImageObject;
        }
        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (picPicture.Image != null)
            {
                ISM.Template.FormImage frm = new FormImage();
                frm.Resource = _core.Resource;
                frm.ImageObject = picPicture.Image;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    picPicture.Image = frm.ImageObject;
                }
            }
            else
            {
                MessageBox.Show("Зураг сонгогдоогүй байна .");
            }
        }
    }
}
