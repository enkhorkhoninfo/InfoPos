using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ISM.Touch;
using ISM.Template;
using EServ.Shared;

namespace InfoPos.Order
{
    public partial class frmOrderEdit : DevExpress.XtraEditors.XtraForm,ISM.Touch.ITouchCall
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        ISM.Touch.TouchKeyboard _kb;
        string _layoutfilename;
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
                _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
                ucOrderSearch1.TouchKeyboard = _kb;
                ucOrderSearch1.Core = _core;
                ucOrderSearch1.Remote = _core.RemoteObject;
                ucOrderSearch1.Resource = _core.Resource;

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

                ucOrderCancel1.Core = _core;
                ucOrderCancel1.Remote = _core.RemoteObject;
                ucOrderCancel1.Resource = _core.Resource;
                ucOrderCancel1.TouchKeyboard = _kb;

                ucOrderConfirm1.Core = _core;
                ucOrderConfirm1.Remote = _core.RemoteObject;
                ucOrderConfirm1.Resource = _core.Resource;
                ucOrderConfirm1.TouchKeyboard = _kb;

                ucOrderExpend1.Core = _core;
                ucOrderExpend1.Remote = _core.RemoteObject;
                ucOrderExpend1.Resource = _core.Resource;
                ucOrderExpend1.TouchKeyboard = _kb;

                tabMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                ucOrderSearch1.EventChoose += new ucOrderSearch.delegateEventChoose(ucOrderSearch1_EventChoose);
                ucCustSearch1.EventChoose+=new Panels.ucCustSearch.delegateEventChoose(ucCustSearch1_EventChoose);
                ucOrderGroup1.EventChoose += new ucOrderGroup.delegateEventChoose(ucOrderGroup1_EventChoose);

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
                if (txtOrderNo.EditValue != null || txtOrderNo.Text != "" || buttonkey == "fo_order_search"||buttonkey=="fo_order_edit"||buttonkey=="fo_order_edit_exit")
                {
                    switch (buttonkey)
                    {

                        case "fo_order_search":
                            tabMain.SelectedTabPageIndex = 0;
                            labelControl15.Text = "Захиалга - Захиалга сонгох";
                            break;
                        case "fo_order_orderedit":
                            tabMain.SelectedTabPageIndex = 1;
                            labelControl15.Text = "Захиалга - Захиалга засварлах";
                            break;
                        case "fo_order_personedit":
                            tabMain.SelectedTabPageIndex = 2;
                            RefreshPerson();
                            labelControl15.Text = "Захиалга - Хамрагдах үйлчлүүлэгчид";
                            break;
                        case "fo_order_groupedit":
                            tabMain.SelectedTabPageIndex = 3;
                            ucOrderGroup1.orderno = Static.ToStr(txtOrderNo.EditValue);
                            ucOrderGroup1.RefreshGroup(Static.ToStr(txtOrderNo.EditValue));
                            ucOrderGroup1.gridView1.Columns[0].Caption = "Захиалгын дугаар";
                            ucOrderGroup1.gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
                            ucOrderGroup1.gridView1.Columns[0].Visible = false;
                            ucOrderGroup1.gridView1.Columns[1].Caption = "Багцын дугаар";
                            ucOrderGroup1.gridView1.Columns[2].Caption = "Бүртгэсэн огноо";
                            ucOrderGroup1.gridView1.Columns[3].Caption = "Эхлэх цаг";
                            ucOrderGroup1.gridView1.Columns[3].DisplayFormat.FormatString = "hh::mm:ss"; ;
                            ucOrderGroup1.gridView1.Columns[4].DisplayFormat.FormatString = "hh:mm:ss";
                            ucOrderGroup1.gridView1.Columns[4].Caption = "Дуусах цаг";
                            ucOrderGroup1.gridView1.Columns[5].Caption = "Ажиллах давтамжын дугаар";
                            ucOrderGroup1.gridView1.Columns[5].Visible = false;
                            ucOrderGroup1.gridView1.Columns[6].Caption = "Ажиллах давтамж";

                            ucOrderGroup1.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                            ucOrderGroup1.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                            ucOrderGroup1.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
                            ucOrderGroup1.gridView1.Columns[3].OptionsColumn.AllowEdit = false;
                            ucOrderGroup1.gridView1.Columns[4].OptionsColumn.AllowEdit = false;
                            ucOrderGroup1.gridView1.Columns[5].OptionsColumn.AllowEdit = false;
                            ucOrderGroup1.gridView1.Columns[6].OptionsColumn.AllowEdit = false;
                            labelControl15.Text = "Захиалга - Багц үйлчилгээний бүлэг";
                            ucOrderGroup1.RefreshProd(Static.ToStr(txtOrderNo.EditValue), 1);
                            break;

                        case "fo_order_cancel":
                            if (Static.ToInt(cboStatus.EditValue) != 0)
                            {
                                ucOrderCancel1.orderno = Static.ToStr(txtOrderNo.EditValue);
                                tabMain.SelectedTabPageIndex = 4;
                                labelControl15.Text = "Захиалга - Захиалга цуцлах";
                            }
                            else
                            {
                                MessageBox.Show("Захиалга цуцлагдсан байна.");
                            }
                            break;
                        case "fo_order_confirm":
                            if (Static.ToInt(cboStatus.EditValue) != 2)
                            {
                                ucOrderConfirm1.orderno = Static.ToStr(txtOrderNo.EditValue);
                                tabMain.SelectedTabPageIndex = 5;
                                labelControl15.Text = "Захиалга - Захиалга баталгаажуулах";
                            }
                            else
                            {
                                MessageBox.Show("Захиалга баталгаажсан байна.");
                            }
                            break;
                        case "fo_order_expend":
                            ucOrderExpend1.orderno = Static.ToStr(txtOrderNo.EditValue);
                            ucOrderExpend1.expenddate = Static.ToDate(dteEndDate.EditValue);
                            tabMain.SelectedTabPageIndex = 6;
                            labelControl15.Text = "Захиалга - Захиалга сунгах";
                            break;
                        case "fo_order_edit_exit":
                            this.Close();
                            item.IsClose = 1;
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Захиалгаа эхэлж сонгоно уу", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                ISM.Template.FormUtility.ValidateQuery(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public frmOrderEdit()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmOrderEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (_core.Resource != null)
                {
                    simpleButton1.Image = _core.Resource.GetImage("object_save");
                    btnOwnerFind.Image = _core.Resource.GetImage("button_find");

                }
                #region[ Init Combo ]
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
                    cboStatus.ItemIndex = 1;
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
                #endregion
                #region[AddKeyboard]
                _kb.AddToKeyboard(dteStartDate);
                _kb.AddToKeyboard(dteEndDate);
                _kb.AddToKeyboard(txtFee);
                _kb.AddToKeyboard(txtConfirmTerm);
                _kb.AddToKeyboard(txtOrderAmount);
                _kb.AddToKeyboard(txtPersonCount);
                _kb.AddToKeyboard(txtPrepaidAmount);
                _kb.AddToKeyboard(dteStart);
                _kb.AddToKeyboard(dteEnd);
                _kb.AddToKeyboard(dteToday);
                #endregion
                ucOrderSearch1.DataRefresh(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Function]
        private void RefreshPerson()
        {
            Result res = new Result();
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
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
            return msg;
        }
        #endregion
        #region[BTN]
        private void simpleButton1_Click(object sender, EventArgs e)
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
            if (txtOrderNo.EditValue == null)
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
                msg = "Амжилттай хадгаллаа";
            }
            if (res.ResultNo == 0)
            {
                MessageBox.Show(msg);
                if (txtOrderNo.EditValue == null)
                    txtOrderNo.EditValue = res.Param[0];
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
        #region[Choose]
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
            tabMain.SelectedTabPageIndex = 7;
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
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Харилцагч сонгоогүй байна.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void ucOrderSearch1_EventChoose(DataRow currentrow)
        {
            if (currentrow != null)
            {
                _core.MainForm_HeaderClear();
                _core.MainForm_HeaderSet(0, "Захиалгын дугаар", currentrow["OrderNo"].ToString());
                _core.MainForm_HeaderSet(1, "Харилцагчийн дугаар", currentrow["CUSTNO"].ToString());
                txtOrderNo.EditValue = Static.ToStr(currentrow["orderno"]);
                txtCustNo.EditValue = Static.ToLong(currentrow["custno"]);
                dteStartDate.EditValue = Static.ToDate(currentrow["startdate"]);
                dteEndDate.EditValue = Static.ToDate(currentrow["enddate"]);
                txtPersonCount.EditValue = Static.ToInt(currentrow["personcount"]);
                txtConfirmTerm.EditValue = Static.ToInt(currentrow["confirmterm"]);
                cboTermType.EditValue = Static.ToInt(currentrow["termtype"]);
                txtOrderAmount.EditValue = Static.ToDecimal(currentrow["orderamount"]);
                cboCurCode.EditValue = Static.ToStr(currentrow["curcode"]);
                txtPrepaidAmount.EditValue = Static.ToDecimal(currentrow["prepaidamount"]);
                txtFee.EditValue = Static.ToDecimal(currentrow["fee"]);
                dteCreateDate.EditValue = Static.ToDate(currentrow["createdate"]);
                dtePostDate.EditValue = Static.ToDateTime(currentrow["postdate"]);
                txtOwnerUser.EditValue = Static.ToInt(currentrow["owneruser"]);
                txtCreateUser.EditValue = Static.ToInt(currentrow["createuser"]);
                cboStatus.EditValue = Static.ToInt(currentrow["status"]);
                cboRebateID.EditValue=Static.ToLong(currentrow["rebateid"]);
                cboLoyalID.EditValue = Static.ToLong(currentrow["loyalid"]);
                cboPointID.EditValue = Static.ToLong(currentrow["pointid"]);
                tabMain.SelectedTabPageIndex = 1;
                labelControl15.Text = "Захиалга - Захиалга засварлах";
            }
            else
            {
                MessageBox.Show("Захиалага сонгогдоогүй байна.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        #region[Events]
        private void frmOrderEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormUtility.GridLayoutSave(ucOrderGroup1.gridView1, _layoutfilename);
            FormUtility.GridLayoutSave(ucOrderGroup1.gridView2, _layoutfilename);
        }
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
        #endregion
    }
}