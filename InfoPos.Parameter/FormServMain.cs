using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class ServMain : Form
    {
        #region[Variables]
        Core.Core _core;
        string _ServId = "";
        int PrivNo = 110100;        
        Form FormName = null;
        int btn = 0;
        string appname = "", formname = "";
        bool loadInv = true;
        Result res = new Result();
        string oldvalue = "";
        string oldvalueadd = "";
        object[] pOldValue;
        object[] pOldValueadd;
        #endregion[]
        #region[INIT]
        public ServMain(Core.Core core, string ServId)
        {
            InitializeComponent();
            _core = core;
            _ServId = ServId;
            Init();            
            InitCombo();            
            ucServMain.Resource = core.Resource;
            ucInMainAdd.Resource = core.Resource;
            ucServPrice.Resource = core.Resource;
            btndis.Image = core.Resource.GetImage("button_find");
            btnDiscount.Image = core.Resource.GetImage("button_find");
            btnRefund.Image = core.Resource.GetImage("button_find");
            btnSales.Image = core.Resource.GetImage("button_find");
            btnInv.Image = core.Resource.GetImage("button_find");  
        }        
        private void Init()
        {
            ucServMain.EventSave += new ucTogglePanel.delegateEventSave(ucServMain_EventSave);
            ucServMain.EventDelete +=new ucTogglePanel.delegateEventDelete(ucServMain_EventDelete);
            ucServMain.EventExit += new ucTogglePanel.delegateEventExit(ucServMain_EventExit);
            ucServMain.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucServMain_EventAddAfter);
            ucServMain.EventReject += new ucTogglePanel.delegateEventReject(ucServMain_EventReject);


            ucInMainAdd.EventDelete += new ucTogglePanel.delegateEventDelete(ucInMainAdd_EventDelete);
            ucInMainAdd.EventSave += new ucTogglePanel.delegateEventSave(ucInMainAdd_EventSave);
            ucInMainAdd.EventExit += new ucTogglePanel.delegateEventExit(ucInMainAdd_EventExit);
            ucInMainAdd.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucInMainAdd_EventAddAfter);
            ucInMainAdd.EventReject +=new ucTogglePanel.delegateEventReject(ucInMainAdd_EventReject);
            ucInMainAdd.EventEdit += new ucTogglePanel.delegateEventEdit(ucInMainAdd_EventEdit);


            ucServPrice.EventDelete += new ucTogglePanel.delegateEventDelete(ucServPrice_EventDelete);
            ucServPrice.EventSave += new ucTogglePanel.delegateEventSave(ucServPrice_EventSave);
            ucServPrice.EventExit += new ucTogglePanel.delegateEventExit(ucServPrice_EventExit);
            ucServPrice.EventAddAfter += new ucTogglePanel.delegateEventAddAfter(ucServPrice_EventAddAfter);
            ucServPrice.EventReject += new ucTogglePanel.delegateEventReject(ucServPrice_EventReject);
            ucServPrice.EventEdit += new ucTogglePanel.delegateEventEdit(ucServPrice_EventEdit);

            ucServMain.FieldLinkAdd("txtServID", 0, "SERVID", "", true, true);
            ucServMain.FieldLinkAdd("cboTypeCode", 0, "TypeCode", "", false, false);
            ucServMain.FieldLinkAdd("txtName", 0, "NAME", "", true, false);
            ucServMain.FieldLinkAdd("txtName2", 0, "NAME2", "", false, false);
            ucServMain.FieldLinkAdd("cboBrandID", 0, "BrandID", "", false, false);
            ucServMain.FieldLinkAdd("txtBarCode", 0, "BarCode", "", false, false);
            ucServMain.FieldLinkAdd("cboUnit", 0, "UNIT", "", false, false);
            ucServMain.FieldLinkAdd("numUnitSize", 0, "UNITSIZE", "", false, false);
            ucServMain.FieldLinkAdd("cboStatus", 0, "STATUS", "", false, false);
            ucServMain.FieldLinkAdd("txtPrice", 0, "PRICE", "", false, false);
            ucServMain.FieldLinkAdd("numCount", 0, "COUNT", "", false, false);
            ucServMain.FieldLinkAdd("dtCreateDate", 0, "CREATEDATE", "", false, false);
            ucServMain.FieldLinkAdd("dtSalesStartDate", 0, "SalesStartDate", "", true, false);
            ucServMain.FieldLinkAdd("dtSalesEndDate", 0, "SalesEndDate", "", true, false);
            ucServMain.FieldLinkAdd("txtNote", 0, "NOTE", "", false, false);
            ucServMain.FieldLinkAdd("txtSalesAccountNo", 0, "SALESACCOUNTNO", "", false, false);
            ucServMain.FieldLinkAdd("txtRefundAccountNo", 0, "REFUNDACCOUNTNO", "", false, false);
            ucServMain.FieldLinkAdd("txtDiscountAccountNo", 0, "DISCOUNTACCOUNTNO", "", false, false);
            ucServMain.FieldLinkAdd("txtBonusAccountNo", 0, "BONUSACCOUNTNO", "", false, false);
            ucServMain.FieldLinkAdd("txtBONUSEXPACCOUNTNO", 0, "BONUSEXPACCOUNTNO", "", false, false);
            ucServMain.FieldLinkAdd("chkIsTimeTable", 0, "IsTimeTable", "", false, false);
            ucServMain.FieldLinkAdd("cboTimeTableID", 0, "TimeTableID", "", false, false);
            ucServMain.FieldLinkAdd("numServiceTime", 0, "SERVICETIME", "", false, false);
            ucServMain.FieldLinkAdd("cboTagType", 0, "TAGTYPE", "", false, false);
            ucServMain.FieldLinkAdd("numTagTime", 0, "TAGTIME", "", false, false);
            ucServMain.FieldLinkAdd("cboTagTimeMethod", 0, "TAGTIMEMETHOD", "", false, false);

            ucInMainAdd.FieldLinkAdd("txtSerId", 0, "SERVID", "", false, true);
            ucInMainAdd.FieldLinkAdd("txInId", 0, "INVID", "", true, false);

            ucServPrice.FieldLinkAdd("numProdType", 0, "ProdType", "", false, true);
            ucServPrice.FieldLinkAdd("txtProdId", 0, "servid", "", false, false);
            ucServPrice.FieldLinkAdd("cboDayType", 0, "DayTypeid", "", true, false);
            ucServPrice.FieldLinkAdd("dtStartTime", 0, "StartTime", "", true, false);
            ucServPrice.FieldLinkAdd("dtEndTime", 0, "EndTime", "", true, false);
            ucServPrice.FieldLinkAdd("numPrice", 0, "Price", "", true, false);

            ucServPrice.FieldLinkAdd("picPicture", 0, "Picture", "", false, false);

            ucInMainAdd.ToggleShowDelete = true;
            ucInMainAdd.ToggleShowEdit = true;
            ucInMainAdd.ToggleShowExit = true;
            ucInMainAdd.ToggleShowNew = true;
            ucInMainAdd.ToggleShowReject = true;
            ucInMainAdd.ToggleShowSave = true;

            

            ucServMain.ToggleShowDelete = true;
            ucServMain.ToggleShowEdit = true;
            ucServMain.ToggleShowExit = true;
            ucServMain.ToggleShowNew = true;
            ucServMain.ToggleShowReject = true;
            ucServMain.ToggleShowSave = true;            


            ucServPrice.ToggleShowDelete = true;
            ucServPrice.ToggleShowEdit = true;
            ucServPrice.ToggleShowExit = true;
            ucServPrice.ToggleShowNew = true;
            ucServPrice.ToggleShowReject = true;
            ucServPrice.ToggleShowSave = true;

            ucServPrice.DataSource = null;
            ucInMainAdd.DataSource = null;
            ucServMain.DataSource = null;
            ucInMainAdd.GridView = gvwAdd;
            ucServPrice.GridView = gvwPrice;
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
                string[] name = new string[] { "SERVTYPE", "INVCAT", "UNITTYPECODE", "TAGSETUP", "SCHEDULETYPE", "PADAYTYPE" };
                //UNITTYPECODE	НЭГЖИЙН ТӨРӨЛ
                //INVCAT	Ангиллын төрлийн бүртгэл
                //SERVTYPE	Үйлчилгээний төрөл
                //TAGSETUP	Тагийн төрөл
                //SCHEDULETYPE	Хуваарийн төрөл
                //PADAYTYPE	Өдрийн төрөл

                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д SERVTYPE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboTypeCode, DT, "SERVTYPE", "name", "", new int[] { 2, 3, 4 });
                }
                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "\r\nDictionary-д INVCAT оруулаагүй байна" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboCatCode, DT, "CATCODE", "NAME", "", new int[] { 2, 3, 4 });
                }
                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д UNITTYPECODE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboUnit, DT, "UNITTYPECODE", "NAME", "", new int[] { 2, 3, 4 });
                }
                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "Dictionary-д TAGSETUP оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboTagType, DT, "TAGTYPE", "NAME", "", new int[] { 2, 3, 4 });
                }

                DT = (DataTable)Tables[4];
                if (DT == null)
                {
                    msg = "Dictionary-д SCHEDULETYPE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboTimeTableID, DT, "SCHEDULETYPE", "NAME", "", new int[] { 2, 3, 4 });
                }

                DT = (DataTable)Tables[5];
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

                FormUtility.LookUpEdit_SetList(ref cboTagTimeMethod, 0, "Борлуулалт хийгдсэнээр");
                FormUtility.LookUpEdit_SetList(ref cboTagTimeMethod, 1, "Сүүлд түрээсээр олгосноор");
                FormUtility.LookUpEdit_SetList(ref cboTagTimeMethod, 2, "Анх таг уншуулснаар");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Өгөгдлийн баазаас Dictionary олдсонгүй.");
            }
        }
        #endregion[]
        #region[Main]
        void ucServMain_EventReject()
        {
            cboCatCode.ItemIndex = 0;
            cboPrinterType.ItemIndex = 0;
            cboTimeTableID.ItemIndex = 0;
            cboTypeCode.ItemIndex = 0;
            cboStatus.ItemIndex = 0;
            cboTagTimeMethod.ItemIndex = 0;
            cboTagType.ItemIndex = 0;
            cboUnit.ItemIndex = 0;
            dtCreateDate.EditValue = _core.TxnDate;
            dtSalesStartDate.EditValue = _core.TxnDate;
            tdSalesEndDate.EditValue = _core.TxnDate;
            //btnInv.Enabled = false;
        }
        void ucServMain_EventAddAfter()
        {
            cboCatCode.ItemIndex = 0;
            cboPrinterType.ItemIndex = 0;
            cboTimeTableID.ItemIndex = 0;
            cboTypeCode.ItemIndex = 0;
            cboStatus.ItemIndex = 0;
            cboTagTimeMethod.ItemIndex = 0;
            cboTagType.ItemIndex = 0;
            cboUnit.ItemIndex = 0;
            dtCreateDate.EditValue = _core.TxnDate;
            dtSalesStartDate.EditValue = _core.TxnDate;
            tdSalesEndDate.EditValue = _core.TxnDate;
            btnInv.Enabled = true;
        }        
        void ucServMain_EventExit(bool editing, ref bool cancel)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            this.Close();
        }
        void ucServMain_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140230, 140230, new object[] { txtServID.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        ucServMain.FieldLinkSetNewState();
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ucServMain_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucServMain.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtServID.EditValue) != "")
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
                int status = 0;
                //int schedule = 0;
                Result r = new Result();

                byte[] _img = null;
                if (picPicture.Image != null)
                    _img = Static.ImageToByte(picPicture.Image);

                object[] NewValue = {txtServID.EditValue,            //0
                                     cboTypeCode.EditValue,          //1
                                     txtName.EditValue,              //2
                                     txtName2.EditValue,             //3
                                     Static.ToDate(dtSalesStartDate.EditValue),      //4
                                     Static.ToDate(tdSalesEndDate.EditValue),       //5
                                     txtPrice.EditValue,   //6
                                     numCount.EditValue,            //7
                                     cboCatCode.EditValue,           //8
                                     cboUnit.EditValue,              //9
                                     numUnitSize.EditValue,              //10
                                     cboPrinterType.EditValue,       //11
                                     Static.ToDate(dtCreateDate.EditValue),//12         
                                     txtNote.EditValue, //13             
                                     cboStatus.EditValue, //14           
                                     cboTagType.EditValue,//15
                                     numTagTime.EditValue,  //17                                         
                                     cboTagTimeMethod.EditValue,    //16  
                                     chkIsTimeTable.Checked ? 1 : 0,//18 
                                     cboTimeTableID.EditValue,          //19
                                     txtSalesAccountNo.EditValue,        //20
                                     txtRefundAccountNo.EditValue,       //21
                                     txtDiscountAccountNo.EditValue,     //22
                                     txtBonusAccountNo.EditValue,      //23
                                      txtBONUSEXPACCOUNTNO.EditValue,     //24
                                     numServiceTime.EditValue,  //25
                                      _img  //26
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140229, 140229, new object[] { NewValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай засварлалаа.");
                        RefreshData(_ServId);
                    }
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140228, 140228, new object[] { NewValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай нэмлээ .");
                        RefreshData(_ServId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
        }
        private void RefreshData(string pServId)
        {
            this.Show();
            Result res = new Result();
            DataSet ds = null;
            try
            {
                if (pServId != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140227, 140227, new object[] { pServId });

                    if (res.ResultNo == 0)
                    {
                        ucServMain.DataSource = res.Data;
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
        #region[ServMainAdd]     
        void ucInMainAdd_EventEdit(ref bool cancel)
        {
            //oldvalue = Static.ToStr(txInId.EditValue);     

            object[] Value = {    txtSerId.EditValue, 
                                   txInId.EditValue};
            pOldValueadd = Value;
            if (_ServId == "")
            {
                txtSerId.EditValue = _ServId;
            }
        }
        void ucInMainAdd_EventReject()
        {
            txtSerId.EditValue = txtServID.Text;
            
        }
        void ucInMainAdd_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucInMainAdd.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtSerId.EditValue) != "")
                {
                    ucInMainAddEventSave(isnew, ref cancel);
                    RefreshDataAdd(Static.ToStr(txtServID.EditValue));
                    ucInMainAdd.FieldLinkSetValues();
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
        void ucInMainAdd_EventAddAfter()
        {
            txtSerId.EditValue = txtServID.Text;
        }
        void ucInMainAdd_EventExit(bool editing, ref bool cancel)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            this.Close();
        }
        void ucInMainAddEventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucInMainAdd.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = {    
                                   Static.ToStr(txtServID.Text), 
                                   Static.ToStr(txInId.EditValue)};                 
                string msg = "";
                try
                {
                    if (isnew)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140238, 140238, new object[] { obj });
                        msg = "Амжилттай нэмлээ";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140239, 140239, new object[] { txtServID.EditValue, oldvalueadd, Static.ToStr(txInId.EditValue) });
                        msg = "Амжилттай засварлалаа";
                    }
                    if (res.ResultNo == 0)
                    {
                        RefreshDataAdd(Static.ToStr(txtServID.EditValue));
                        ucInMainAdd.FieldLinkSetSaveState();
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
        void ucInMainAdd_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140240, 140240, new object[] { txtServID.EditValue, txInId.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        RefreshDataAdd(Static.ToStr(txtServID.EditValue));
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }        
        private void RefreshDataAdd(string pServId)
        {
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtServID.EditValue) != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140236, 140236, new object[] { pServId });
                    if (res.ResultNo == 0)
                    {                        
                        grdAdd.DataSource = res.Data.Tables[0];
                        ucInMainAdd.DataSource = res.Data;
                        
                        SetServAddData();   
                     
                        loadInv = true;
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
            txtSerId.EditValue = txtServID.Text;
        }        
        void SetServAddData()
        {
            try
            {
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdAdd);

                gvwAdd.Columns[0].Caption = "Үйлчилгээний дугаар";
                gvwAdd.Columns[0].Visible = true;
                gvwAdd.Columns[0].OptionsColumn.AllowEdit = false;
                gvwAdd.Columns[1].Caption = "Барааны дугаар";
                gvwAdd.Columns[1].Visible = true;
                gvwAdd.Columns[1].OptionsColumn.AllowEdit = false;
                gvwAdd.Columns[2].Caption = "Барааны нэр";
                gvwAdd.Columns[2].Visible = true;
                gvwAdd.Columns[2].OptionsColumn.AllowEdit = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gvwServAdd_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ucInMainAdd.FieldLinkSetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Err");
            }
        }
        #endregion[]
        #region[FormEvents]
        private void ServMain_Load(object sender, EventArgs e)
        {
            this.Show();

            FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            if (_ServId == "")
            {
                ucServMain.FieldLinkSetNewState();                
                cboTimeTableID.ItemIndex = 0;
                cboTypeCode.ItemIndex = 0;
                cboStatus.ItemIndex = 0;
                cboTagTimeMethod.ItemIndex = 0;
                cboTagType.ItemIndex = 0;
                cboUnit.ItemIndex = 0;
                dtCreateDate.EditValue = _core.TxnDate;
                dtSalesStartDate.EditValue = _core.TxnDate;
                tdSalesEndDate.EditValue = _core.TxnDate;

                txtProdId.EditValue = txtServID.Text;
                numProdType.EditValue = 1;
                dtEndTime.EditValue = _core.TxnDate;
                dtStartTime.EditValue = _core.TxnDate;
                cboDayType.ItemIndex = 0;
                chkIsTimeTable.Checked = true;
            }
            else if (_ServId != "")
            {                
                RefreshData(_ServId);
                RefreshDataAdd(Static.ToStr(txtServID.EditValue));
                RefreshServPrice(Static.ToStr(txtServID.EditValue));
                
                txtSerId.EditValue = _ServId;
                txtProdId.EditValue = _ServId;

                ucServMain.FieldLinkSetValues();
                ucServMain.FieldLinkSetSaveState();
                ucInMainAdd.FieldLinkSetValues();
                ucInMainAdd.FieldLinkSetSaveState();                

                ucServPrice.FieldLinkSetValues();
                ucServPrice.FieldLinkSetSaveState();
            }
        }
        private void ServMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
        }
        private void ServMain_KeyDown(object sender, KeyEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void xtraTabControl1_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (txtServID.Text == "")
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
            InfoPos.List.InventoryList frm = new InfoPos.List.InventoryList(_core);
            frm.ucInventoryList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                txInId.Text = Static.ToStr(frm.ucInventoryList.SelectedRow["INVID"]);
            }
        }
        #endregion[]        
        #region[ServPrice]
        void ucServPrice_EventEdit(ref bool cancel)
        {
            object[] Value = {     Static.ToInt(1), 
                                   Static.ToStr(txtServID.EditValue),
                                   Static.ToStr(cboDayType.EditValue),
                                   Static.ToDateTime(dtStartTime.EditValue),
                                   Static.ToDateTime(dtEndTime.EditValue),
                                   Static.ToInt(numPrice.EditValue)};
            pOldValue = Value;
            if (_ServId == "")
            {
                txtProdId.EditValue = txtServID.Text;
                numProdType.EditValue = 1;
                dtEndTime.EditValue = _core.TxnDate;
                dtStartTime.EditValue = _core.TxnDate;
                cboDayType.ItemIndex = 0;
            }

        }
        void ucServPrice_EventReject()
        {
            txtProdId.EditValue = txtServID.Text;
            numProdType.EditValue = 1;
            dtEndTime.EditValue = _core.TxnDate;
            dtStartTime.EditValue = _core.TxnDate;
            cboDayType.ItemIndex = 0;
        }
        void ucServPrice_EventAddAfter()
        {
            txtProdId.EditValue =txtServID.Text;
            numProdType.EditValue = 1;
            dtEndTime.EditValue = _core.TxnDate;
            dtStartTime.EditValue = _core.TxnDate;
            cboDayType.ItemIndex = 0;
        }
        void ucServPrice_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void ucServPrice_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucServPrice.FieldValidate(ref err, ref cont) == true)
            {
                if (Static.ToStr(txtProdId.EditValue) != "")
                {
                    ucServPriceEventSave(isnew, ref cancel);
                    RefreshServPrice(Static.ToStr(txtServID.EditValue));
                    ucServPrice.FieldLinkSetValues();
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
        void ucServPriceEventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucServPrice.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = {    
                                   Static.ToInt(1), 
                                   Static.ToStr(txtServID.EditValue),
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
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140273, 140273, new object[] { obj });
                        msg = "Амжилттай нэмлээ";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140274, 140274, new object[] {pOldValue, obj  });
                        msg = "Амжилттай засварлалаа";
                    }
                    if (res.ResultNo == 0)
                    {
                        RefreshServPrice(Static.ToStr(txtServID.EditValue));
                        ucServPrice.FieldLinkSetSaveState();
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
        private void RefreshServPrice(string pServId)
        {
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtServID.EditValue) != "")
                {
                    grdAdd.DataSource = null;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140271, 140271, new object[] { 1, pServId });
                    if (res.ResultNo == 0)
                    {
                        grdPrice.DataSource = res.Data.Tables[0];
                        ucServPrice.DataSource = res.Data;
                        SetServPrice();                        
                        loadInv = true;
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                    }
                }
                if (Static.ToStr(txtServID.EditValue) == "")
                txtProdId.EditValue = txtServID.Text;
                numProdType.EditValue = 1;
                dtEndTime.EditValue = _core.TxnDate;
                dtStartTime.EditValue = _core.TxnDate;
                cboDayType.ItemIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtProdId.EditValue = txtServID.Text;
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
                gvwPrice.Columns[2].Caption = "Үйлчилгээний код";
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
        private void gvwPrice_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                ucServPrice.FieldLinkSetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Err");
            }
        }
        void ucServPrice_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140275, 140275, new object[] { numProdType.EditValue, _ServId, cboDayType.EditValue, dtStartTime.EditValue });//pProdType, pProdID, pDayType, pStartTime);
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
                    RefreshServPrice(Static.ToStr(txtServID.EditValue));
                    ucServMain.FieldLinkSetValues();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void chkIsSchedule_EditValueChanged(object sender, EventArgs e)
        {
            if (chkIsTimeTable.Checked == true)
            {
                numCount.Visible = true;
                labelControl8.Visible = true;
            }
            else
            {
                numCount.Visible = false;
                labelControl8.Visible = false;
            }
        }
        #endregion[]             
        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            if(e.PageIndex==1)
            {
                RefreshDataAdd(Static.ToStr(_ServId));
            }
            if (e.PageIndex == 2)
            {
                RefreshServPrice(Static.ToStr(_ServId));
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
        private void btndis_Click(object sender, EventArgs e)
        {

        }
    }
}