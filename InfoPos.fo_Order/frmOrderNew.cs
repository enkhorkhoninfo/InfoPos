using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using ISM.Touch;
using ISM.Template;
using EServ.Shared;
using InfoPos.Panels;
namespace InfoPos.Order
{
    public partial class frmOrderNew : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        ISM.Touch.TouchKeyboard _kb;
        string ProdNo = "";
        #endregion
        #region[Init]
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _kb = new TouchKeyboard();
                _kb.Enable = true;

                ucCustSearch2.Core = _core;
                ucCustSearch2.Remote = _core.RemoteObject;
                ucCustSearch2.Resource = _core.Resource;
                ucCustSearch2.TouchKeyboard = _kb;

                ucSchedule1.core = _core;
                ucSchedule1.TouchKeyboard = _kb;

                ucOrderGroup1.TouchKeyboard = _kb;
                ucOrderGroup1.Core = _core;
                ucOrderGroup1.Remote = _core.RemoteObject;
                ucOrderGroup1.Resource = _core.Resource;

                ucCustSearch1.Core = _core;
                ucCustSearch1.Remote = _core.RemoteObject;
                ucCustSearch1.Resource = _core.Resource;
                ucCustSearch1.TouchKeyboard = _kb;

                ucCustSearch2.EventChoose += new ucCustSearch.delegateEventChoose(ucCustSearch2_EventChoose);
                ucCustSearch1.EventChoose += new ucCustSearch.delegateEventChoose(ucCustSearch1_EventChoose);
                ucOrderGroup1.EventChoose += new ucOrderGroup.delegateEventChoose(ucOrderGroup1_EventChoose);
                TabMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                this.MdiParent = parent;
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            Result res = new Result();
            try
            {
                if (txtOrderNo.EditValue != null || txtOrderNo.Text != "" || buttonkey == "fo_order_new" || buttonkey == "fo_order_new_exit" || buttonkey == "fo_customer_search")
                {
                    switch (buttonkey)
                    {

                        case "fo_customer_search":
                            TabMain.SelectedTabPageIndex = 0;
                            labelControl15.Text = "Захиалга - Харилцагч сонгох";
                            break;
                        case "fo_order_ordernew":
                            TabMain.SelectedTabPageIndex = 1;
                            labelControl15.Text = "Захиалга - Захиалга үүсгэх";
                            break;
                        case "fo_order_personnew":
                            TabMain.SelectedTabPageIndex = 2;
                            RefreshPerson();
                            labelControl15.Text = "Захиалга - Хамрагдах үйлчлүүлэгчид";
                            break;
                        case "fo_order_groupnew":
                            TabMain.SelectedTabPageIndex = 3;
                            ucOrderGroup1.orderno = Static.ToStr(txtOrderNo.EditValue);
                            ucOrderGroup1.RefreshGroup(Static.ToStr(txtOrderNo.EditValue));
                            labelControl15.Text = "Захиалга - Багц үйлчилгээний бүлэг";
                            break;
                        case "fo_order_new_exit":
                            this.Close();
                            item.IsClose = 1;
                            break;
                    }
                    ISM.Template.FormUtility.ValidateQuery(res);
                }
                else
                    MessageBox.Show("Захиалгаа эхэлж үүсгэнэ үү.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public frmOrderNew()
        {
            InitializeComponent();
        }
        private void frmOrderNew_Load(object sender, EventArgs e)
        {
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
                btnRefresh.Image = _core.Resource.GetImage("navigate_refresh");
            }
            FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Цуцлагдсан");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Идэвхтэй");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 2, "Баталгаажсан");

            FormUtility.LookUpEdit_SetList(ref cboTermType, "T", "Цаг");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "D", "Өдөр");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "W", "Гараг");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "M", "Сар");

            string msg = "";
            ArrayList Tables = new ArrayList();
            string[] names = { "CURRENCY","REBATEMASTER" };
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
                FormUtility.LookUpEdit_SetValue(ref cboStatus, 1);
                cboTermType.ItemIndex = 1;
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
            //General
            _kb.AddToKeyboard(dteStartDate);
            _kb.AddToKeyboard(dteEndDate);
            _kb.AddToKeyboard(txtFee);
            _kb.AddToKeyboard(txtConfirmTerm);
            _kb.AddToKeyboard(txtOrderAmount);
            _kb.AddToKeyboard(txtPersonCount);
            _kb.AddToKeyboard(txtPrepaidAmount);
            _kb.AddToKeyboard(cboRebateID);
            _kb.AddToKeyboard(cboLoyalID);
            _kb.AddToKeyboard(cboPointID);
            _kb.AddToKeyboard(cboCurCode);
            _kb.AddToKeyboard(cboTermType);
            
            dteStartDate.EditValue = _core.TxnDate;
            dteEndDate.EditValue = _core.TxnDate;
            ucCustSearch2.DataRefresh(1);
        }
        #endregion
        #region[Function]
        private string Validate()
        {
            string msg = "Дараах талбаруудыг гүйцэт бөглөнө үү.";
            if (dteStartDate.EditValue == null)
            {
                msg = msg + "\r\nЭхлэх огноо оруулна уу.";
            }
            if (dteEndDate.EditValue == null)
            {
                msg = msg + "\r\nДуусах огноо оруулна уу.";
            }
            if (txtPersonCount.Text == "" || txtPersonCount.Text == "0")
            {
                msg = msg + "\r\nХүний тоо оруулна уу.";
            }
            if (txtConfirmTerm.Text == "")
            {
                msg = msg + "\r\nБаталгаажих хугацаа оруулна уу";
            }
            if (cboTermType.EditValue == null)
            {
                msg = msg + "\r\nБаталгаажих хугацааны төрөл сонгоно уу";
            }
            return msg;

        }
        private void RefreshPerson()
        {
            Result res = new Result();
            try
            {
                if (txtOrderNo.EditValue != null)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130106, 130106, new object[] { txtOrderNo.EditValue });

                    if (res.ResultNo == 0)
                    {
                        gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
                        gridControl1.DataSource = res.Data.Tables[0];
                        gridView1.Columns[0].Caption = "Захиалгын дугаар";
                        gridView1.Columns[0].Visible = false;
                        gridView1.Columns[1].Caption = "Харилцагчийн дугаар";
                        gridView1.Columns[2].Caption = "Овог";
                        gridView1.Columns[3].Caption = "Нэр";
                        gridView1.Columns[4].Caption = "Байгууллагын нэр";
                        //_layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
                        gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                        gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                        gridView1.Columns[2].OptionsColumn.AllowEdit = false;
                        gridView1.Columns[3].OptionsColumn.AllowEdit = false;
                        gridView1.Columns[4].OptionsColumn.AllowEdit = false;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                else
                {
                    MessageBox.Show("Захиалгаа үүсгэнэ үү.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[BTN]
        private void btnSave_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            string msg = Validate();
            if (msg != "Дараах талбаруудыг гүйцэт бөглөнө үү.")
            {
                MessageBox.Show(msg, "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dteStartDate.DateTime > dteEndDate.DateTime)
            {
                MessageBox.Show(this, "Эхлэх дуусах огноо алдаатай байна.", "Алдаа");
                return;
            }
            if (txtOrderNo.EditValue == null || txtOrderNo.Text == "")
            {
                dteCreateDate.EditValue = _core.TxnDate;
                dtePostDate.EditValue = DateTime.Now;
                txtCreateUser.EditValue = _core.RemoteObject.User.UserNo;
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
                                   Static.ToLong(cboPointID.EditValue)
                               };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130103, 130103, obj);
                msg = "Амжилттай нэмлээ.";
            }
            else
            {
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
                                   Static.ToLong(cboPointID.EditValue)
                               };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130104, 130104, obj);
                msg = "Амжилттай засварлалаа.";
            }
            if (res.ResultNo == 0)
            {
                MessageBox.Show(msg);
                if (txtOrderNo.EditValue == null)
                    txtOrderNo.EditValue = res.Param[0];
                labelControl15.Text = "Захиалга - Хамрагдах үйлчлүүлэгчид";
                TabMain.SelectedTabPageIndex = 2;
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130110, 130110, new object[] { Static.ToStr(dr["ORDERNO"]), Static.ToStr(dr["CUSTNO"]) });

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Chooce]
        void ucOrderGroup1_EventChoose(DataRow currentrow)
        {
            if (Static.ToInt(currentrow["COUNT"]) == 0)
            {
                MessageBox.Show("Энэхүү үйлчилгээн дээр хувиарын тоог буруу тохируулсан байна.\r\nСистемийн администратортой холбоо барина уу.");
                return;
            }
            ucSchedule1.ProdType = 1;
            ProdNo = Static.ToStr(currentrow["PRODNO"]);
            ucSchedule1.ProdNo = ProdNo;
            ucSchedule1.LineNumber = Static.ToInt(currentrow["COUNT"]);
            ucSchedule1.Duration = Static.ToInt(currentrow["Duration"]); ;
            ucSchedule1.Unit = Static.ToStr(currentrow["UNIT"]);
            ucSchedule1.OrderNo = Static.ToStr(currentrow["ORDERNO"]);
            dteToday.DateTime = _core.TxnDate;
            ucSchedule1.RefreshData(1, ProdNo, dteToday.DateTime);

            ucSchedule1.StatusAdd(0, "Захиалга хийж байгаа", Color.NavajoWhite);
            ucSchedule1.StatusAdd(1, "Захиалсан", Color.DeepSkyBlue);
            ucSchedule1.StatusAdd(2, "Захиалга баталгаажсан", Color.DeepPink);
            ucSchedule1.StatusAdd(3, "Цуцлагдсан", Color.OrangeRed);
            ucSchedule1.StatusAdd(4, "Хаагдсан", Color.GreenYellow);
            TabMain.SelectedTabPageIndex = 4;

        }
        void ucCustSearch1_EventChoose(DataRow currentrow)
        {
            if (currentrow != null)
            {
                Result res = new Result();
                object[] obj = {
                                   Static.ToStr(txtOrderNo.EditValue),
                                   Static.ToStr(currentrow["CUSTOMERNO"])
                               };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130108, 130108, obj);
                if (res.ResultNo == 0)
                {
                    RefreshPerson();
                }
                else
                {
                    MessageBox.Show("", "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        void ucCustSearch2_EventChoose(DataRow currentrow)
        {
            if (currentrow != null)
            {
                txtCustNo.EditValue = currentrow["CUSTOMERNO"];
                ucSchedule1.CustName = Static.ToStr(currentrow["LASTNAME"]);
                _core.MainForm_HeaderClear();
                _core.MainForm_HeaderSet(0, "Харилцагчийн №", Static.ToStr(currentrow["CUSTOMERNO"]));
                _core.MainForm_HeaderSet(1, "Овог", Static.ToStr(currentrow["FIRSTNAME"]));
                _core.MainForm_HeaderSet(2, "Нэр", Static.ToStr(currentrow["LASTNAME"]));
                ucSchedule1.CustName = Static.ToStr(currentrow["LASTNAME"]);
                TabMain.SelectedTabPageIndex = 1;
                labelControl15.Text = "Захиалга - Захиалга үүсгэх";
            }
            else
                MessageBox.Show("Харилцагч сонгогдоогүй байна", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion
        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            if (dteEnd.DateTime.DayOfYear - dteStart.DateTime.DayOfYear < 0)
            {
                MessageBox.Show("Эхлэх дуусах хугацаа алдаатай байна.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dteEnd.DateTime.DayOfYear - dteStart.DateTime.DayOfYear > 30)
            {
                MessageBox.Show("Эхлэх дуусах хугацааны интервал их байна.(<31)", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dteToday.EditValue != null)
            {
                if (dteStart.EditValue == null && dteEnd.EditValue == null)
                {
                    ucSchedule1.RefreshData(1, ProdNo, dteToday.DateTime);
                }
            }
            if (dteToday.EditValue != null && dteStart.EditValue != null && dteEnd.EditValue != null)
                ucSchedule1.RefreshData(1, ProdNo, dteToday.DateTime, dteStart.DateTime, dteEnd.DateTime);
        }
    }
}