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

namespace InfoPos.Parameter
{
    public partial class FormInvMain : Form
    {
        #region[Variables]
        Core.Core _core;
        int PrivNo = 110100;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        Result res = new Result();
        string _InvID = "";
        bool loadInv;
        string formname = "";
        object[] pOldValue;
        object[] pSerOldValue;
        #endregion[]
        public FormInvMain(Core.Core core, string pInvID)
        {
            InitializeComponent();
            _core = core;            
            Init();
            _InvID = pInvID;            
            ucInvMain.Resource = core.Resource;
            btnBonus.Image = core.Resource.GetImage("button_find");
            btnDiscount.Image = core.Resource.GetImage("button_find");
            btnReFund.Image = core.Resource.GetImage("button_find");
            btnSales.Image = core.Resource.GetImage("button_find");
            ucProdPrice.Resource = _core.Resource;
            ucInvSer.Resource = _core.Resource;
        }
        #region[Init]
        private void Init() 
        {
            ucInvMain.EventSave += new ucTogglePanel.delegateEventSave(ucInvMain_EventSave);
            ucInvMain.EventDelete += new ucTogglePanel.delegateEventDelete(ucInvMain_EventDelete);
            ucInvMain.EventExit += new ucTogglePanel.delegateEventExit(ucInvMain_EventExit);
            ucInvMain.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucInvMain_EventAddAfter);


            ucProdPrice.EventDelete += new ucTogglePanel.delegateEventDelete(ucProdPrice_EventDelete);
            ucProdPrice.EventSave += new ucTogglePanel.delegateEventSave(ucProdPrice_EventSave);
            ucProdPrice.EventExit += new ucTogglePanel.delegateEventExit(ucProdPrice_EventExit);
            ucProdPrice.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucProdPrice_EventAddAfter);
            ucProdPrice.EventReject += new ucTogglePanel.delegateEventReject(ucProdPrice_EventReject);
            ucProdPrice.EventEdit += new ucTogglePanel.delegateEventEdit(ucProdPrice_EventEdit);



            ucInvSer.EventDelete += new ucTogglePanel.delegateEventDelete(ucInvSer_EventDelete);
            ucInvSer.EventSave += new ucTogglePanel.delegateEventSave(ucInvSer_EventSave);
            ucInvSer.EventExit += new ucTogglePanel.delegateEventExit(ucInvSer_EventExit);
            ucInvSer.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucInvSer_EventAddAfter);
            ucInvSer.EventReject += new ucTogglePanel.delegateEventReject(ucInvSer_EventReject);
            ucInvSer.EventEdit += new ucTogglePanel.delegateEventEdit(ucInvSer_EventEdit);



            ucInvMain.FieldLinkAdd("txtInvId", 0, "INVID", "", true, true);
            ucInvMain.FieldLinkAdd("cboInvType", 0, "INVTYPE", "", false, false);
            ucInvMain.FieldLinkAdd("txtName", 0, "NAME", "", true, false);
            ucInvMain.FieldLinkAdd("txtName2", 0, "NAME2", "", false, false);
            ucInvMain.FieldLinkAdd("cboBrandId", 0, "BRANDID", "", false, false);
            ucInvMain.FieldLinkAdd("numPriceAmount", 0, "PRICEAMOUNT", "", false, false);
            ucInvMain.FieldLinkAdd("numPriceRefund", 0, "PRICEREFUND", "", false, false);
            ucInvMain.FieldLinkAdd("numCount", 0, "COUNT", "", true, false);
            ucInvMain.FieldLinkAdd("cboCatCode", 0, "CATCODE", "", false, false);
            ucInvMain.FieldLinkAdd("txtBarCode", 0, "BARCODE", "", false, false);
            ucInvMain.FieldLinkAdd("cboUnit", 0, "UNIT", "", false, false);
            ucInvMain.FieldLinkAdd("numUnitSize", 0, "UNITSIZE", "", false, false);
            ucInvMain.FieldLinkAdd("cboPrinterType", 0, "PRINTERTYPE", "", false, false);
            ucInvMain.FieldLinkAdd("dtCreateDate", 0, "CREATEDATE", "", false, false);
            ucInvMain.FieldLinkAdd("txtNote", 0, "NOTE", "", false, false);
            ucInvMain.FieldLinkAdd("cboStatus", 0, "STATUS", "", false, false);
            ucInvMain.FieldLinkAdd("txtSalesAccountNo", 0, "SALESACCOUNTNO", "", false, false);
            ucInvMain.FieldLinkAdd("txtRefundAccountNo", 0, "REFUNDACCOUNTNO", "", false, false);
            ucInvMain.FieldLinkAdd("txtDiscountAccountNo", 0, "DISCOUNTACCOUNTNO", "", false, false);
            ucInvMain.FieldLinkAdd("txtBonusAccountNo", 0, "BONUSACCOUNTNO", "", false, false);
            ucInvMain.FieldLinkAdd("cboRentFlag", 0, "RentFlag", "", false, false);
            ucInvMain.FieldLinkAdd("txtBonusExpAccountNo", 0, "BonusExpAccountNo", "", false, false);



            ucProdPrice.FieldLinkAdd("numProdType", 0, "ProdType", "", false, true);
            ucProdPrice.FieldLinkAdd("txtProdId", 0, "servid", "", false, false);
            ucProdPrice.FieldLinkAdd("cboDayType", 0, "DayTypeid", "", true, false);
            ucProdPrice.FieldLinkAdd("dtStartTime", 0, "StartTime", "", true, false);
            ucProdPrice.FieldLinkAdd("dtEndTime", 0, "EndTime", "", true, false);
            ucProdPrice.FieldLinkAdd("numPrice", 0, "Price", "", true, false);


            ucInvSer.FieldLinkAdd("strInvCode", 0, "txtInvId", "", false, true);
            ucInvSer.FieldLinkAdd("txtSerNo", 0, "BARCODE", "", false, false);
            ucInvSer.FieldLinkAdd("cboInvStatus", 0, "STATUS", "", false, false);

            ucInvSer.FieldLinkAdd("picPicture", 0, "Picture", "", false, false);

            ucProdPrice.ToggleShowDelete = true;
            ucProdPrice.ToggleShowEdit = true;
            ucProdPrice.ToggleShowExit = true;
            ucProdPrice.ToggleShowNew = true;
            ucProdPrice.ToggleShowReject = true;
            ucProdPrice.ToggleShowSave = true;

            InitCombo();


            ucInvMain.ToggleShowDelete = true;
            ucInvMain.ToggleShowEdit = true;
            ucInvMain.ToggleShowExit = true;
            ucInvMain.ToggleShowNew = true;
            ucInvMain.ToggleShowReject = true;
            ucInvMain.ToggleShowSave = true;

            ucInvMain.DataSource = null;
            ucInvMain.FieldLinkSetSaveState();

            ucProdPrice.GridView = gvwPrice;


            ucInvSer.ToggleShowDelete = true;
            ucInvSer.ToggleShowEdit = true;
            ucInvSer.ToggleShowExit = true;
            ucInvSer.ToggleShowNew = true;
            ucInvSer.ToggleShowReject = true;
            ucInvSer.ToggleShowSave = true;

            ucInvSer.DataSource = null;
            ucInvSer.FieldLinkSetSaveState();

            ucInvSer.GridView = gvwSerial;
        }
        private void InitCombo()
        {
            try
            {
                Result res = new Result();
                ArrayList Tables = new ArrayList();
                DataTable DT = null;
                string msg = "";

                DictUtility.PrivNo = PrivNo;
                string[] name = new string[] { "INVTYPE", "BRAND", "INVCAT", "UNITTYPECODE", "PADAYTYPE" };
                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д INVTYPE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboInvType, DT, "invtype", "name", "", new int[] { 2, 3, 4 });
                }
                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д BRAND оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboBrandId, DT, "BrandId", "NAME");
                }
                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д INVCAT оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboCatCode, DT, "catcode", "NAME", "", new int[] { 2, 3, 4 });
                }
                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "Dictionary-д UNITTYPECODE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboUnit, DT, "UNITTYPECODE", "NAME", "", new int[] { 2, 3, 4 });
                }

                DT = (DataTable)Tables[4];
                if (DT == null)
                {
                    msg = "Dictionary-д PADAYTYPE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboDayType, DT, "DAYTYPE", "DESCRIPTION", "", new int[] { 2, 3, 4 });
                }

                FormUtility.LookUpEdit_SetList(ref cboPrinterType, 0, "Bill Printer");
                FormUtility.LookUpEdit_SetList(ref cboPrinterType, 1, "Lift Printer");

                FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Идэвхтэй");
                FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Идэвхгүй");

                FormUtility.LookUpEdit_SetList(ref cboRentFlag, 0, "Түрээсийн хэрэгсэл биш");
                FormUtility.LookUpEdit_SetList(ref cboRentFlag, 1, "Түрээсийн хэрэгсэл");
                FormUtility.LookUpEdit_SetList(ref cboRentFlag, 2, "Урьдчилан бэлддэг түрээсийн бараа");

                FormUtility.LookUpEdit_SetList(ref cboInvStatus, 0, "Ашиглах боломжтой");
                FormUtility.LookUpEdit_SetList(ref cboInvStatus, 1, "Эвдрэлтэй");
                FormUtility.LookUpEdit_SetList(ref cboInvStatus, 2, "Ашиглагдаж байгаа");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Өгөгдлийн баазаас Dictionary олдсонгүй.");
            }
        }
        #endregion[]
        #region[ucInvMain]
        void ucInvMain_EventAddAfter()
        {
            if (txtInvId.Text == "" && txtName.Text == "")
            {
                cboBrandId.ItemIndex = 0;
                cboCatCode.ItemIndex = 0;
                cboInvType.ItemIndex = 0;
                cboPrinterType.ItemIndex = 0;
                cboStatus.ItemIndex = 0;
                cboUnit.ItemIndex = 0;
                cboRentFlag.ItemIndex = 0;
            }
        }
        void ucInvMain_EventExit(bool editing, ref bool cancel)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            this.Close();
        }
        void ucInvMain_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140215, 140215, new object[] { txtInvId.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        ucInvMain.FieldLinkSetNewState();
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucInvMain_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucInvMain.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtInvId.EditValue) != "")
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
            try
            {
                Result r = new Result();

                byte[] _img = null;
                if (picPicture.Image != null)
                    _img = Static.ImageToByte(picPicture.Image);

                object[] NewValue = { Static.ToStr(txtInvId.EditValue), 
                                      Static.ToStr(cboInvType.EditValue), 
                                      Static.ToStr(txtName.EditValue), 
                                      Static.ToStr(txtName2.EditValue),
                                      Static.ToStr(cboBrandId.EditValue),
                                      Static.ToDecimal(numPriceAmount.EditValue), 
                                      Static.ToDecimal(numPriceRefund.EditValue), 
                                      Static.ToInt(numCount.EditValue), 
                                      Static.ToStr(cboCatCode.EditValue),
                                      Static.ToStr(txtBarCode.EditValue),
                                      Static.ToInt(cboUnit.EditValue), 
                                      Static.ToInt(numUnitSize.EditValue), 
                                      Static.ToStr(cboPrinterType.EditValue),
                                      Static.ToDate(dtCreateDate.EditValue),
                                      Static.ToStr(txtNote.EditValue), 
                                      Static.ToInt(cboStatus.EditValue), 
                                      Static.ToStr(txtSalesAccountNo.EditValue),
                                      Static.ToStr(txtRefundAccountNo.EditValue),                                      
                                      Static.ToStr(txtDiscountAccountNo.EditValue),
                                      Static.ToStr(txtBonusAccountNo.EditValue),
                                      Static.ToStr(cboRentFlag.EditValue),
                                      Static.ToStr(txtBonusExpAccountNo.EditValue),
                                      _img
                                    };
                if (!isnew)
                {                    
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140214, 140214, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140213, 140213, new object[] { NewValue, FieldName });
                    MessageBox.Show("Амжилттай нэмлээ .");
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
        }
        private void RefreshData(string pinvId)
        {
            this.Show();
            Result res = new Result();
            DataSet ds = null;
            try
            {
                if (pinvId != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140212, 140212, new object[] { pinvId });

                    if (res.ResultNo == 0)
                    {
                        ucInvMain.DataSource = res.Data;
                        ucInvMain.FieldLinkSetValues();
                        ds = res.Data;
                        byte[] a = null;

                        if (ds.Tables[0].Rows[0]["Picture"] != null && ds.Tables[0].Rows[0]["Picture"] != DBNull.Value && ds.Tables[0].Rows[0]["Picture"] != "")
                            a = (byte[])ds.Tables[0].Rows[0]["Picture"];

                        picPicture.Image = Static.ImageFromByte(a);

                        DataSet DS = new DataSet();

                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion[]                
        #region[FormEvent]
        private void FormInvMain_Load(object sender, EventArgs e)
        {
            this.Show();           
            FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            if (_InvID != "")
            {
                RefreshData(_InvID);                               
                RefreshInvPrice(Static.ToStr(txtInvId.EditValue));
                RefreshInvSer(Static.ToStr(txtInvId.EditValue));
                ucInvMain.FieldLinkSetValues();
                ucProdPrice.FieldLinkSetValues();
                ucProdPrice.FieldLinkSetSaveState();                
                ucInvMain.FieldLinkSetSaveState();
                numProdType.EditValue = 0;
                txtInvId.EditValue = _InvID;
                strInvCode.EditValue = _InvID;
            }
            else if (_InvID == "")
            {                                
                ucInvMain.FieldLinkSetNewState();
                cboBrandId.ItemIndex = 0;
                cboCatCode.ItemIndex = 0;
                cboInvType.ItemIndex = 0;
                cboPrinterType.ItemIndex = 0;
                cboStatus.ItemIndex = 0;
                cboUnit.ItemIndex = 0;
                dtCreateDate.EditValue = _core.TxnDate;
                strInvCode.EditValue = txtInvId.EditValue;


                txtProdId.EditValue = txtInvId.Text;
                numProdType.EditValue = 0;
                dtEndTime.EditValue = _core.TxnDate;
                dtStartTime.EditValue = _core.TxnDate;
                cboDayType.ItemIndex = 0;
            }
        }
        private void FormInvMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
        }
        private void FormInvMain_KeyDown(object sender, KeyEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void tabInvMain_Deselected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            if (loadInv == true)
                RefreshInvPrice(Static.ToStr(txtInvId.EditValue));
        }
        private void tabInvMain_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (txtInvId.Text == "")
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
        private void gvwPrice_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ucProdPrice.FieldLinkSetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Err");
            }
        }
        #endregion[]
        #region[ServPrice]
        void ucProdPrice_EventEdit(ref bool cancel)
        {
           object[] Value = {      Static.ToInt(0), 
                                   Static.ToStr(txtInvId.EditValue),
                                   Static.ToStr(cboDayType.EditValue),
                                   Static.ToDateTime(dtStartTime.EditValue),
                                   Static.ToDateTime(dtEndTime.EditValue),
                                   Static.ToInt(numPrice.EditValue)};
            pOldValue = Value;
            if (_InvID == "")
            {
                txtProdId.EditValue = txtInvId.EditValue;
                numProdType.EditValue = 0;
                dtEndTime.EditValue = _core.TxnDate;
                dtStartTime.EditValue = _core.TxnDate;
                cboDayType.EditValue = 0;
            }
        }
        void ucProdPrice_EventReject()
        {
            txtProdId.EditValue = txtInvId.Text;
            numProdType.EditValue = 0;
            dtEndTime.EditValue = _core.TxnDate;
            dtStartTime.EditValue = _core.TxnDate;
            cboDayType.ItemIndex = 0;
        }
        void ucProdPrice_EventAddAfter()
        {
            txtProdId.EditValue = txtInvId.Text;
            numProdType.EditValue = 0;
            dtEndTime.EditValue = _core.TxnDate;
            dtStartTime.EditValue = _core.TxnDate;
            cboDayType.ItemIndex = 0;
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
                if (Static.ToStr(txtProdId.EditValue) != "")
                {
                    ucProdPriceEventSave(isnew, ref cancel);
                    RefreshInvPrice(Static.ToStr(txtInvId.EditValue));
                    ucProdPrice.FieldLinkSetValues();
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
        void ucProdPriceEventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucProdPrice.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = {    
                                   Static.ToInt(0), 
                                   Static.ToStr(txtInvId.EditValue),
                                   Static.ToStr(cboDayType.EditValue),
                                   Static.ToDateTime(dtStartTime.EditValue),
                                   Static.ToDateTime(dtEndTime.EditValue),
                                   Static.ToInt(numPrice.EditValue)
                               
                               };
                string msg = "";
                try
                {
                    if (isnew)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140268, 140268, new object[] { obj });
                        msg = "Амжилттай нэмлээ";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140269, 140269, new object[] {pOldValue, obj });
                        msg = "Амжилттай засварлалаа";
                    }
                    if (res.ResultNo == 0)
                    {
                        RefreshInvPrice(Static.ToStr(txtInvId.EditValue));
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
        private void RefreshInvPrice(string pInvID)
        {
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtInvId.EditValue) != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140266, 140266, new object[] { 0, pInvID });
                    if (res.ResultNo == 0)
                    {
                        grdPrice.DataSource = res.Data.Tables[0];
                        ucProdPrice.DataSource = res.Data;
                        SetServPrice();
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
                txtProdId.EditValue = txtInvId.EditValue;
                numProdType.EditValue = 0;
                dtEndTime.EditValue = _core.TxnDate;
                dtStartTime.EditValue = _core.TxnDate;
                cboDayType.EditValue = 0;
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        private void SetServPrice()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdPrice);
                gvwPrice.Columns[0].Caption = "Төрлийн дугаар";
                gvwPrice.Columns[0].Visible = true;
                gvwPrice.Columns[0].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[1].Caption = "Төрлийн нэр";
                gvwPrice.Columns[1].Visible = true;
                gvwPrice.Columns[1].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[2].Caption = "Барааны код";
                gvwPrice.Columns[2].Visible = true;
                gvwPrice.Columns[2].OptionsColumn.AllowEdit = false;


                gvwPrice.Columns[3].Caption = "Өдрийн төрлийн код";
                gvwPrice.Columns[3].Visible = true;
                gvwPrice.Columns[3].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[4].Caption = "Өдрийн төрлийн нэр";
                gvwPrice.Columns[4].Visible = true;
                gvwPrice.Columns[4].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[5].Caption = "Хамрах цагийн эхлэл";
                gvwPrice.Columns[5].Visible = true;
                gvwPrice.Columns[5].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[6].Caption = "Хамрах цагийн төгсгөл";
                gvwPrice.Columns[6].Visible = true;
                gvwPrice.Columns[6].OptionsColumn.AllowEdit = false;
                gvwPrice.Columns[7].Caption = "Эдгээр нөхцөл дэх үнэ";
                gvwPrice.Columns[7].Visible = true;
                gvwPrice.Columns[7].OptionsColumn.AllowEdit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetSerNo()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdSerial);
                gvwSerial.Columns[0].Caption = "Барааны код";
                gvwSerial.Columns[0].Visible = true;
                gvwSerial.Columns[0].OptionsColumn.AllowEdit = false;
                gvwSerial.Columns[1].Caption = "Сериал дугаар";
                gvwSerial.Columns[1].Visible = true;
                gvwSerial.Columns[1].OptionsColumn.AllowEdit = false;                
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
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140270, 140270, new object[] { 0, _InvID, cboDayType.EditValue, dtStartTime.EditValue });//pProdType, pProdID, pDayType, pStartTime);
                    if (r.ResultNo != 0)                                                                                        //db, pProdType, pProdID, pDayType, pStartTime);
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");                        
                        btn = 1;
                    }
                    RefreshInvPrice(Static.ToStr(txtInvId.EditValue));
                    ucProdPrice.FieldLinkSetValues();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
        }        
        #endregion[]        
        #region[InvSer]
        void ucInvSer_EventEdit(ref bool cancel)
        {
            object[] Value = {     Static.ToStr(txtInvId.EditValue),
                                   Static.ToStr(txtSerNo.EditValue),
                                   Static.ToInt(cboInvStatus.EditValue)
                             };
            pSerOldValue = Value;
            if (_InvID == "")
            {
                strInvCode.EditValue = txtInvId.EditValue;                
            }
        }
        void ucInvSer_EventReject()
        {
            strInvCode.EditValue = txtInvId.EditValue;
        }
        void ucInvSer_EventAddAfter()
        {
            strInvCode.EditValue = txtInvId.EditValue;       
        }
        void ucInvSer_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void ucInvSer_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucInvSer.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtProdId.EditValue) != "")
                {
                    ucInvSerEventSave(isnew, ref cancel);
                    RefreshInvSer(Static.ToStr(txtInvId.EditValue));
                    ucInvSer.FieldLinkSetValues();
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
        void ucInvSer_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140295, 140295, new object[] {_InvID, txtSerNo.EditValue});
                    if (r.ResultNo != 0)                                                                                        //db, pProdType, pProdID, pDayType, pStartTime);
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        btn = 1;
                    }
                    RefreshInvSer(Static.ToStr(txtInvId.EditValue));
                    ucInvSer.FieldLinkSetValues();
                    strInvCode.EditValue = txtInvId.EditValue;          
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucInvSerEventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucInvSer.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = {    
                                   Static.ToStr(txtInvId.EditValue),
                                   Static.ToStr(txtSerNo.EditValue),
                                   Static.ToInt(cboInvStatus.EditValue)
                               
                               };
                string msg = "";
                try
                {
                    if (isnew)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140293, 140293, new object[] { obj });
                        msg = "Амжилттай нэмлээ";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140294, 140294, new object[] { pSerOldValue, obj });
                        msg = "Амжилттай засварлалаа";
                    }
                    if (res.ResultNo == 0)
                    {
                        RefreshInvSer(Static.ToStr(txtInvId.EditValue));
                        ucInvSer.FieldLinkSetSaveState();
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
        private void RefreshInvSer(string p_InvID)
        {
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtInvId.EditValue) != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140291, 140291, new object[] { p_InvID });
                    if (res.ResultNo == 0)
                    {
                        grdSerial.DataSource = res.Data.Tables[0];
                        ucInvSer.DataSource = res.Data;
                        SetSerNo();
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
                strInvCode.EditValue = txtInvId.EditValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion[]

        private void gvwSerial_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ucInvSer.FieldLinkSetValues();
                strInvCode.EditValue = txtInvId.EditValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Err");
            }
        }

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