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

namespace InfoPos.Order
{
    public partial class frmOrder : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        long OldTabPersonCustNo = 0;
        string _orderno = ""; 
        string appname = "", formname = "";
        string knowbutton = "", knowbuttonProduct = "";
        Form FormName = null;
        public static ImageCollection image = new ImageCollection();
        public frmOrder(InfoPos.Core.Core core,string orderno)
        {
            InitializeComponent();
            _core = core; 
            _orderno = orderno;
            ucGeneral.Resource = _core.Resource;
            ucPerson.Resource = _core.Resource;
            btnCustEnq.Image = _core.Resource.GetImage("navigate_refresh");
            btnCustFind.Image = _core.Resource.GetImage("button_find");
            btnOwnerFind.Image = _core.Resource.GetImage("button_find");
            btnTabPersonCustEnq.Image = _core.Resource.GetImage("navigate_refresh");
            btnTabCustFind.Image = _core.Resource.GetImage("button_find");

            btnGroupAdd.Image = _core.Resource.GetImage("navigate_add");
            btnGroupEdit.Image = _core.Resource.GetImage("navigate_edit");
            btnGroupDelete.Image = _core.Resource.GetImage("navigate_delete");
            btnGroupCancel.Image = _core.Resource.GetImage("image_exit");

            btnProductAdd.Image = _core.Resource.GetImage("navigate_add");
            btnProductEdit.Image = _core.Resource.GetImage("navigate_edit");
            btnProductDelete.Image = _core.Resource.GetImage("navigate_delete");
            btnProductCancel.Image = _core.Resource.GetImage("image_exit");

            btnInvFind.Image = _core.Resource.GetImage("button_find");


            gvwGroup.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            gvwProduct.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            gvwOrderPerson.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            InitData();
            InitCombo();
            InitEvents();
            InitToggles();
            if (_orderno == "")
            {
                ucGeneral.FieldLinkSetNewState();
                dteStartDate.DateTime = DateTime.Now;
                dteEndDate.DateTime = DateTime.Now;
                cboCurCode.ItemIndex = 0;
                cboStatus.ItemIndex = 1;
                cboTermType.ItemIndex = 0;
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
            FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Цуцлагдсан");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Идэвхтэй");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 2, "Баталгаажсан");

            FormUtility.LookUpEdit_SetList(ref cboTermType, "T", "Цаг");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "D", "Өдөр");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "W", "Гараг");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "M", "Сар");

            string msg = "";
            ArrayList Tables = new ArrayList();
            string[] names = { "CURRENCY", "REBATEMASTER" };
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
                msg = "Dictionary-д REBATEMASTER оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboRebateID, dt, "MASTERID", "NAME", "MASTERTYPE=0", new int[] { 1 });
                FormUtility.LookUpEdit_SetList(ref cboLoyalID, dt, "MASTERID", "NAME", "MASTERTYPE=1", new int[] { 1 });
                FormUtility.LookUpEdit_SetList(ref cboPointID, dt, "MASTERID", "NAME", "MASTERTYPE=2", new int[] { 1 });
            }
            #endregion
            #region[Group]
            FormUtility.LookUpEdit_SetList(ref cboTabGroupRunTime, 0, "Нэг  ");
            FormUtility.LookUpEdit_SetList(ref cboTabGroupRunTime, 1, "Олон");          
            #endregion
            #region[Order]
            FormUtility.LookUpEdit_SetList(ref cboProdProdType, 0, "Бараа материал");
            FormUtility.LookUpEdit_SetList(ref cboProdProdType, 1, "Үйлчилгээ");
            FormUtility.LookUpEdit_SetList(ref cboProdProdType, 2, "Багц");
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
                ucGeneral.FieldLinkAdd("txtOrderNo", 0, "OrderNo", "", false, false, true);
                ucGeneral.FieldLinkAdd("txtCustNo", 0, "CustNo", "", true, false, true);
                ucGeneral.FieldLinkAdd("txtCustomerName", 0, "LASTNAME", "", false, false, true);
                ucGeneral.FieldLinkAdd("txtConfirmTerm", 0, "ConfirmTerm", "", false, false, false);
                ucGeneral.FieldLinkAdd("cboTermType", 0, "TermType", "", true, false);
                ucGeneral.FieldLinkAdd("txtOrderAmount", 0, "OrderAmount", "", false, false);
                ucGeneral.FieldLinkAdd("txtPrepaidAmount", 0, "PrepaidAmount", "", false, false);
                ucGeneral.FieldLinkAdd("txtPersonCount", 0, "PersonCount", "", false, false);
                ucGeneral.FieldLinkAdd("cboCurCode", 0, "CurCode", "", false, false);
                ucGeneral.FieldLinkAdd("txtFee", 0, "Fee", "", false, false);
                ucGeneral.FieldLinkAdd("dteStartDate", 0, "StartDate", "", false, false);
                ucGeneral.FieldLinkAdd("dteEndDate", 0, "EndDate", "", true, false);
                ucGeneral.FieldLinkAdd("cboStatus", 0, "Status", "", false, false);
                ucGeneral.FieldLinkAdd("dteCreateDate", 0, "CreateDate", "", false, false, true);
                ucGeneral.FieldLinkAdd("dtePostDate", 0, "CreatePostDate", "", false, false, true);
                ucGeneral.FieldLinkAdd("txtOwnerUser", 0, "OwnerUser", "", false, false, true);
                ucGeneral.FieldLinkAdd("txtCreateUser", 0, "CreateUser", "", false, false, true);
                ucGeneral.FieldLinkAdd("cboRebateID", 0, "RebateID", "", false, false);
                ucGeneral.FieldLinkAdd("cboLoyalID", 0, "LoyalID", "", false, false);
                ucGeneral.FieldLinkAdd("cboPointID", 0, "PointID", "", false, false);
                #endregion
                #region[Person]
                ucPerson.FieldLinkAdd("txtTabPersonCustomerNo", 0, "CUSTNO", "", true, false, true);
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130104, 130104, new object[] { txtOrderNo.EditValue });

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
                if (dteStartDate.DateTime > dteEndDate.DateTime)
                {
                    MessageBox.Show(this, "Эхлэх дуусах огноо алдаатай байна.", "Алдаа");
                    cancel = true;
                    return;
                }
                dteCreateDate.EditValue = _core.TxnDate;
                dtePostDate.EditValue = DateTime.Now;
                object[] obj = {
                                   Static.ToStr(txtOrderNo.EditValue),
                                   Static.ToStr(txtCustNo.EditValue),
                                   Static.ToInt(txtConfirmTerm.EditValue),
                                   Static.ToStr(cboTermType.EditValue),
                                   Static.ToDecimal(txtOrderAmount.EditValue),
                                   Static.ToDecimal(txtPrepaidAmount.EditValue),
                                   Static.ToStr(cboCurCode.EditValue),
                                   Static.ToDecimal(txtFee.EditValue),
                                   Static.ToDateTime(dteStartDate.EditValue),
                                   Static.ToDateTime(dteEndDate.EditValue),
                                   Static.ToInt(txtPersonCount.EditValue),
                                   Static.ToInt(cboStatus.EditValue),
                                   Static.ToDate(dteCreateDate.EditValue),
                                   Static.ToDateTime(dtePostDate.EditValue),
                                   Static.ToInt(txtCreateUser.EditValue),
                                   Static.ToInt(txtOwnerUser.EditValue),
                                   Static.ToLong(cboRebateID.EditValue),
                                   Static.ToLong(cboLoyalID.EditValue),
                                   Static.ToLong(cboPointID.EditValue),
                               };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130103, 130103, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130104, 130104, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    txtOrderNo.EditValue = res.Param[0];
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
            dteStartDate.DateTime = DateTime.Now;
            dteEndDate.DateTime = DateTime.Now;
            cboCurCode.ItemIndex = 0;
            cboStatus.ItemIndex = 1;
        }
        private void RefreshGeneral()
        {
            Result res = new Result();
            try
            {
                if (_orderno != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130102, 130102, new object[] { _orderno });

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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130110, 130110, new object[] { Static.ToStr(txtOrderNo.EditValue), Static.ToStr(txtTabPersonCustomerNo.EditValue) });

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
            if (ucPerson.FieldValidate(ref err, ref cont) == true)
            {
                object[] obj = {
                                   Static.ToStr(txtOrderNo.EditValue),
                                   Static.ToStr(txtTabPersonCustomerNo.EditValue)
                               };
                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130108, 130108, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130109, 130109, new object[] { txtOrderNo.EditValue, OldTabPersonCustNo, txtTabPersonCustomerNo.EditValue });
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    RefreshPerson();
                    btnTabCustFind.Enabled = false;
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
            OldTabPersonCustNo = Static.ToLong(txtTabPersonCustomerNo.EditValue);
            btnTabCustFind.Enabled = true;
        }
        void ucPerson_EventAddAfter()
        {
            btnTabCustFind.Enabled = true;
        }
        private void RefreshPerson()
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130106, 130106, new object[] { txtOrderNo.EditValue });

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
        #endregion
        #region[Group]
        void ucGroup_EventEdit(ref bool cancel)
        {
        }
        private void RefreshGroup()
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130111, 130111, new object[] { txtOrderNo.EditValue });

                if (res.ResultNo == 0)
                {
                    grdGroup.DataSource = res.Data.Tables[0];
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
            if (txtCustNo.EditValue == null)
            {
                txtCustomerName.EditValue = null;
                return;
            }
            Result res2 = new Result();
            res2 = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120001, 120001, new object[] { txtCustNo.EditValue, 0 });
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
                    txtCustomerName.EditValue = name;
                }
            }
            else
                MessageBox.Show(res2.ResultNo + " : " + res2.ResultDesc);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                if (Static.ToLong(txtTabPersonCustomerNo.EditValue) != 0)
                {
                    object[] obj1 = new object[23];
                    obj1[0] = Static.ToLong(txtTabPersonCustomerNo.EditValue);

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120001, 120001, obj1);

                    if (res.ResultNo == 0)
                    {
                        object[] obj = new object[3];
                        obj[0] = _core;
                        obj[1] = txtTabPersonCustomerNo.EditValue;
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
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            InfoPos.List.CustomerList frm = new List.CustomerList(_core);
            frm.ucCustomerList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                txtTabPersonCustomerNo.EditValue = frm.ucCustomerList.SelectedRow["CUSTOMERNO"];
            }
        }
        private void btnGroupAdd_Click(object sender, EventArgs e)
        {
            btnGroupEdit.Text = "Хадгалах";
            btnGroupAdd.Enabled = false;
            btnGroupDelete.Enabled = false;
            btnGroupCancel.Enabled = true;
            tmeGroupEnd.Properties.ReadOnly = false;
            tmeGroupStart.Properties.ReadOnly = false;
            txtTabGroupNo.Properties.ReadOnly = false;
            cboTabGroupRunTime.Properties.ReadOnly = false;
            tmeGroupEnd.BackColor = Color.LemonChiffon;
            tmeGroupStart.BackColor = Color.LemonChiffon;
            dteTabGroupOrderDate.BackColor = Color.White;
            txtTabGroupNo.BackColor = Color.LemonChiffon;
            cboTabGroupRunTime.BackColor = Color.LemonChiffon;
            cboTabGroupRunTime.ItemIndex = 0;
        }
        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (btnGroupEdit.Text == "Засах")
            {
                btnGroupAdd.Enabled = false;
                btnGroupDelete.Enabled = false;
                btnGroupCancel.Enabled = true;
                btnGroupEdit.Text = "Хадгалах";
                knowbutton="Засах";
                btnGroupCancel.Enabled = true;
                tmeGroupEnd.Properties.ReadOnly = false;
                tmeGroupStart.Properties.ReadOnly = false;
                cboTabGroupRunTime.Properties.ReadOnly = false;
                tmeGroupEnd.BackColor = Color.LemonChiffon;
                tmeGroupStart.BackColor = Color.LemonChiffon;
                dteTabGroupOrderDate.BackColor = Color.White;
                txtTabGroupNo.BackColor = Color.LemonChiffon;
                cboTabGroupRunTime.BackColor = Color.LemonChiffon;
                return;
            }
            else
            {
                Result res = new Result();
                string msg = "";
                object[] obj = {
                                   Static.ToStr(txtOrderNo.EditValue),
                                   Static.ToLong(txtTabGroupNo.EditValue),
                                   Static.ToDate(dteTabGroupOrderDate.DateTime),
                                   Static.ToDateTime(tmeGroupStart.Time),
                                   Static.ToDateTime(tmeGroupEnd.Time),
                                   Static.ToInt(cboTabGroupRunTime.EditValue)
                               };
                if (knowbutton == "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130113, 130113, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130114, 130114, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    RefreshGroup();
                    knowbutton = "";
                    Cancel();
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
        }
        private void btnGroupCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DataRow dr = gvwGroup.GetFocusedDataRow();
                if (dr != null)
                {
                    txtTabGroupNo.EditValue=dr["GROUPNO"];
                }
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130115, 130115, new object[] { Static.ToStr(txtOrderNo.EditValue), Static.ToStr(txtTabGroupNo.EditValue) });

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    cboTabGroupRunTime.ItemIndex = 0;
                    RefreshGroup();
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
        private void gvwGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwGroup.GetFocusedDataRow();
            if (dr != null)
            {
                txtTabGroupNo.EditValue=dr["groupno"];
                dteTabGroupOrderDate.EditValue = dr["orderdate"];
                tmeGroupStart.EditValue = dr["StartTime"];
                tmeGroupEnd.EditValue = dr["EndTime"];
                FormUtility.LookUpEdit_SetValue(ref cboTabGroupRunTime, dr["RunType"]);
                txtProdOrderNo.EditValue = txtOrderNo.EditValue;
                txtProdGroupNo.EditValue = dr["groupno"];
                RefreshProduct();
            }
        }
        void Cancel()
        {
            btnGroupEdit.Text = "Засах";
            btnGroupAdd.Enabled = true;
            btnGroupDelete.Enabled = true;
            btnGroupCancel.Enabled = false;
            knowbutton = "";
            tmeGroupEnd.Properties.ReadOnly = true;
            tmeGroupStart.Properties.ReadOnly = true;
            txtTabGroupNo.Properties.ReadOnly = true;
            cboTabGroupRunTime.Properties.ReadOnly = true;
            tmeGroupEnd.BackColor = Color.Gainsboro;
            tmeGroupStart.BackColor = Color.Gainsboro;
            dteTabGroupOrderDate.BackColor = Color.Gainsboro;
            txtTabGroupNo.BackColor = Color.Gainsboro;
            cboTabGroupRunTime.BackColor = Color.Gainsboro;
        }
        #endregion
        #region[Product]
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
                            }
                        }break;
                    case 1 : {
                            InfoPos.List.ServiceList frm = new List.ServiceList(_core);
                            frm.ucServiceList.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtProdProdNo.EditValue = frm.ucServiceList.SelectedRow["SERVID"];
                            }
                        }break;
                    case 2 : {
                        InfoPos.List.PackMainList frm = new List.PackMainList(_core);
                        frm.ucPackMain.Browsable = true;
                            DialogResult res = frm.ShowDialog();
                            if ((res == System.Windows.Forms.DialogResult.OK))
                            {
                                txtProdProdNo.EditValue = frm.ucPackMain.SelectedRow["PackId"];
                            }
                        }break;
                }
            }
            else
                MessageBox.Show("Бүтээгдэхүүний төрлөө сонгоно уу.");
        }
        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = gvwProduct.GetFocusedDataRow();
            if (dr != null)
            {
                txtProdOrderNo.EditValue = dr["ORDERNO"];
                txtProdGroupNo.EditValue=dr["GROUPNO"];
                txtProdProdNo.EditValue = dr["PRODNO"];
                FormUtility.LookUpEdit_SetValue(ref cboProdProdType, dr["PRODTYPE"]);
            }
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130120, 130120, new object[] { Static.ToStr(txtProdOrderNo.EditValue), Static.ToLong(txtProdGroupNo.EditValue), Static.ToStr(txtProdProdNo.EditValue), Static.ToInt(cboProdProdType.EditValue) });

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    cboProdProdType.ItemIndex = 0;
                    txtProdQty.EditValue = null;
                    txtProdProdNo.EditValue = null;
                    txtProdOrderNo.EditValue = null;
                    txtProdGroupNo.EditValue = null;
                    RefreshProduct();
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
        private void btnProductCancel_Click(object sender, EventArgs e)
        {
            CancelProduct();
        }
        private void btnProductEdit_Click(object sender, EventArgs e)
        {
            if (btnProductEdit.Text == "Засах")
            {
                btnProductAdd.Enabled = false;
                btnProductDelete.Enabled = false;
                btnProductCancel.Enabled = true;
                btnInvFind.Enabled = true;
                btnProductEdit.Text = "Хадгалах";
                knowbuttonProduct = "Засах";
                btnGroupCancel.Enabled = true;
                cboProdProdType.Properties.ReadOnly = false;
                txtProdQty.Properties.ReadOnly = false;
                cboProdProdType.BackColor = Color.LemonChiffon;
                txtProdProdNo.BackColor = Color.LemonChiffon;
                txtProdQty.BackColor = Color.LemonChiffon;
                return;
            }
            else
            {
                string errormsg = "Дараах талбаруудыг гүйцэт бөглөнө үү.";
                if (cboProdProdType.EditValue == null)
                    errormsg = errormsg + "\r\nБүтээгдэхүүни төрөл сонгоно уу.";
                if (txtProdProdNo.EditValue == null)
                    errormsg = errormsg + "\r\nБараа үйлчилгээ сонгоно уу.";
                if (txtProdQty.EditValue == null||Static.ToInt(txtProdQty.EditValue)==0)
                    errormsg = errormsg + "\r\n Тоо ширхэг оруулна уу.";
                if (errormsg != "Дараах талбаруудыг гүйцэт бөглөнө үү.")
                {
                    MessageBox.Show(errormsg, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Result res = new Result();
                string msg = "";
                object[] obj = {
                                   Static.ToStr(txtProdOrderNo.EditValue),
                                   1,//Static.ToLong(txtProdGroupNo.EditValue),
                                   Static.ToStr(txtProdProdNo.EditValue),
                                   Static.ToInt(cboProdProdType.EditValue),
                                   Static.ToInt(txtProdQty.EditValue)
                               };
                if (knowbuttonProduct == "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130118, 130118, obj);
                    msg = "Амжилттай нэмлээ.";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130119, 130119, obj);
                    msg = "Амжилттай засварлалаа.";
                }
                if (res.ResultNo == 0)
                {
                    MessageBox.Show(msg);
                    RefreshProduct();
                    knowbuttonProduct = "";
                    CancelProduct();
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
        }
        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            btnProductEdit.Text = "Хадгалах";
            txtProdGroupNo.EditValue = txtTabGroupNo.EditValue;
            txtProdOrderNo.EditValue = txtOrderNo.EditValue;
            btnProductAdd.Enabled = false;
            btnProductDelete.Enabled = false;
            btnProductCancel.Enabled = true;
            btnInvFind.Enabled = true;
            cboProdProdType.Properties.ReadOnly = false;
            txtProdQty.Properties.ReadOnly = false;
            cboProdProdType.BackColor = Color.LemonChiffon;
            txtProdProdNo.BackColor = Color.LemonChiffon;
            txtProdQty.BackColor = Color.LemonChiffon;
            txtProdQty.EditValue = 1;
            txtProdProdNo.EditValue = null;
            cboProdProdType.ItemIndex = 0;
        }
        void CancelProduct()
        {
            btnProductEdit.Text = "Засах";
            btnProductAdd.Enabled = true;
            btnProductDelete.Enabled = true;
            btnProductCancel.Enabled = false;
            btnInvFind.Enabled = false;
            knowbuttonProduct = "";
            cboProdProdType.Properties.ReadOnly = true;
            txtProdQty.Properties.ReadOnly = true;
            cboProdProdType.BackColor = Color.Gainsboro;
            txtProdProdNo.BackColor = Color.Gainsboro;
            txtProdQty.BackColor = Color.Gainsboro;
        }
        void RefreshProduct()
        {
            Result res = new Result();
            grdProduct.DataSource = null;
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130116, 130116, new object[] { Static.ToStr(txtOrderNo.EditValue), 1 });//Static.ToLong(txtTabGroupNo.EditValue) });
            if (res.ResultNo == 0)
            {
                RepositoryItemImageComboBox imagecombo = new RepositoryItemImageComboBox();
                imagecombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
                ImageComboBoxItem imageitem=new ImageComboBoxItem();
                ImageCollection imgcol=new ImageCollection();
                imgcol.AddImage(_core.Resource.GetImage("alarmclock"));
                imagecombo.Properties.SmallImages=imgcol;
                imageitem.Value = Static.ToDecimal(1);
                imageitem.ImageIndex=0;
                imagecombo.Properties.Items.Add(imageitem);
                grdProduct.DataSource = res.Data.Tables[0];
                gvwProduct.Columns[0].Caption = "Захиалгын дугаар";
                gvwProduct.Columns[0].Visible = false;
                gvwProduct.Columns[1].Caption = "Багцын дугаар";
                gvwProduct.Columns[2].Caption = "Бүтээгдэхүүний дугаар";
                gvwProduct.Columns[3].Caption = "Бүтээгдэхүүний төрлийн дугаар";
                gvwProduct.Columns[3].Visible = false;
                gvwProduct.Columns[4].Caption = "Бүтээгдэхүүний төрөл";
                gvwProduct.Columns[5].Caption = "Тоо ширхэг";
                gvwProduct.Columns[6].Caption = "Хуваарьтай эсэх";
                gvwProduct.Columns[6].ColumnEdit = imagecombo;
                //gvwProduct.Columns[7].Caption = "Замын тоо";
                //gvwProduct.Columns[8].Caption = "Хугацааны төрөл"; 
                //gvwProduct.Columns[9].Caption = "Хугацааны хэмжээ";
                gvwProduct.Columns[10].Caption = "Бүтээгдэхүүний нэр";
                gvwProduct.Columns[7].Visible = false;
                gvwProduct.Columns[8].Visible = false;
                gvwProduct.Columns[9].Visible = false;
                
                gvwProduct.Columns[0].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[1].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[2].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[3].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[4].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[5].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[6].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[7].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[8].OptionsColumn.AllowEdit = false; 
                gvwProduct.Columns[9].OptionsColumn.AllowEdit = false;
                gvwProduct.Columns[10].OptionsColumn.AllowEdit = false;

                FormUtility.RestoreStateGrid(appname, formname, ref gvwProduct);
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }

        }
        #endregion
        private void xtraTabControl1_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (Static.ToStr(txtOrderNo.EditValue) == "")
                {
                   e.Cancel = true;
                   MessageBox.Show("Үндсэн мэдээлэлээ эхэлж үүсгэнэ үү.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    gvwOrderPerson.Columns[1].Caption = "Харилцагчийн дугаар";
                    gvwOrderPerson.Columns[2].Caption = "Овог";
                    gvwOrderPerson.Columns[3].Caption = "Нэр";
                    gvwOrderPerson.Columns[4].Caption = "Байгууллагын нэр";

                    gvwOrderPerson.Columns[0].OptionsColumn.AllowEdit = false;
                    gvwOrderPerson.Columns[1].OptionsColumn.AllowEdit = false;
                    gvwOrderPerson.Columns[2].OptionsColumn.AllowEdit = false;
                    gvwOrderPerson.Columns[3].OptionsColumn.AllowEdit = false;
                    gvwOrderPerson.Columns[4].OptionsColumn.AllowEdit = false;
                    break;
                case 2:
                    //RefreshGroup();
                    RefreshProduct();
                    //gvwGroup.Columns[0].Caption = "Захиалгын дугаар";
                    //gvwGroup.Columns[0].Visible = false;
                    //gvwGroup.Columns[5].Visible = false;
                    //gvwGroup.Columns[1].Caption = "Багцын дугаар";
                    //gvwGroup.Columns[2].Caption = "Бүртгэсэн огноо";
                    //gvwGroup.Columns[3].Caption = "Эхлэх цаг";
                    //gvwGroup.Columns[3].DisplayFormat.FormatString = "hh::mm:ss"; ;
                    //gvwGroup.Columns[4].DisplayFormat.FormatString = "hh:mm:ss";
                    //gvwGroup.Columns[4].Caption = "Дуусах цаг";
                    //gvwGroup.Columns[5].Caption = "Ажиллах давтамжын дугаар";
                    //gvwGroup.Columns[6].Caption = "Ажиллах давтамж";

                    //gvwGroup.Columns[0].OptionsColumn.AllowEdit = false;
                    //gvwGroup.Columns[1].OptionsColumn.AllowEdit = false;
                    //gvwGroup.Columns[2].OptionsColumn.AllowEdit = false;
                    //gvwGroup.Columns[3].OptionsColumn.AllowEdit = false;
                    //gvwGroup.Columns[4].OptionsColumn.AllowEdit = false;
                    //gvwGroup.Columns[5].OptionsColumn.AllowEdit = false;
                    //gvwGroup.Columns[6].OptionsColumn.AllowEdit = false;
                    //FormUtility.RestoreStateGrid(appname, formname, ref gvwGroup);
                    break;
            }
        }
        private void gvwProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwProduct.GetFocusedDataRow();
            if (dr != null)
            {
                txtProdOrderNo.EditValue=dr["ORDERNO"];
                txtProdGroupNo.EditValue=dr["GROUPNO"];
                FormUtility.LookUpEdit_SetValue(ref cboProdProdType, dr["PRODTYPE"]);
                txtProdProdNo.EditValue = dr["PRODNO"];
                txtProdQty.EditValue = dr["QTY"];
            }
        }
        private void cboProdProdType_EditValueChanged(object sender, EventArgs e)
        {
            txtProdProdNo.EditValue = null;
        }
        private void gvwProduct_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gvwProduct.GetFocusedDataRow();

            if (Static.ToInt(dr["ISSCHEDULE"]) == 1)
            {
                InfoPos.Schedule.frmSchedul frm = new Schedule.frmSchedul(_core, Static.ToStr(dr["UNIT"]), Static.ToInt(dr["Duration"]), Static.ToStr(dr["ORDERNO"]), Static.ToLong(dr["GROUPNO"]), Static.ToStr(dr["PRODNO"]), Static.ToInt(dr["COUNT"]), txtCustomerName.Text);
                frm.ShowDialog();
            }
            else { MessageBox.Show("Хуваарь тохируулаагүй байна.","Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }
        private void gvwOrderPerson_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwOrderPerson.GetFocusedDataRow();
            if (dr != null)
            {
                txtTabPersonCustomerNo.EditValue=dr["CUSTNO"];
            }
        }
        private void btnOwnerFind_Click(object sender, EventArgs e)
        {
            InfoPos.List.UserList frm = new List.UserList(_core);
            frm.ucUserList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                txtOwnerUser.EditValue = frm.ucUserList.SelectedRow["USERNO"];
            }
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {

        }
    }
}
