using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;

namespace InfoPos.Order
{
    public partial class ucOrderGroup : DevExpress.XtraEditors.XtraUserControl
    {
        #region[Properties]
        private InfoPos.Core.Core _core = null;
        public InfoPos.Core.Core Core
        {
            get { return _core; }
            set
            {
                if (value != null)
                {
                    _core = value;
                    if (_remote == null) _remote = _core.RemoteObject;
                    if (_resource == null) _resource = _core.Resource;
                }
            }
        }

        private ISM.CUser.Remote _remote = null;
        public ISM.CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }

        private ISM.Template.Resource _resource = null;
        public ISM.Template.Resource Resource
        {
            get { return _resource; }
            set
            {
                _resource = value;
                if (_resource != null)
                {
                    btnGroupAdd.Image = _resource.GetImage("object_save");
                    btnGroupDelete.Image = _resource.GetImage("object_delete");
                    btnProdDelete.Image = _resource.GetImage("object_delete");
                    btnProdSelect.Image = _resource.GetImage("gl_ok");
                    btnProdFind.Image = _resource.GetImage("button_find");
                }
            }
        }

        private ISM.Touch.TouchKeyboard _touchkeyboard = null;
        public ISM.Touch.TouchKeyboard TouchKeyboard
        {
            get { return _touchkeyboard; }
            set { _touchkeyboard = value; }
        }

        private string _orderno = "";
        public string orderno
        {
            get { return _orderno; }
            set { _orderno = value; }
        }

        private long _groupno = 0;
        public long groupno
        {
            get { return _groupno; }
            set { _groupno = value; }
        }
        private string _layoutfilename;
        public string layoutfilename
        {
            get {return _layoutfilename; }
            set { _layoutfilename = value; }
        }
        #endregion
        #region[Contructure]
        public ucOrderGroup()
        {
            InitializeComponent();
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboTabGroupRunTime, 0, "Нэг  ");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboTabGroupRunTime, 1, "Олон");
            cboTabGroupRunTime.ItemIndex = 0;
        }
        #endregion

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                txtTabGroupNo.EditValue = dr["Groupno"];
                dteTabGroupOrderDate.EditValue = dr["ORDERDATE"];
                tmeGroupStart.EditValue = dr["StartTime"];
                tmeGroupEnd.EditValue = dr["EndTime"];
                ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboTabGroupRunTime, dr["RUNTYPE"]);
                //RefreshProd(_orderno, Static.ToLong(txtTabGroupNo.EditValue));
                RefreshProd(_orderno, 1);
            }
        }
        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView2.GetFocusedDataRow();
            if (dr != null)
            {
                txtProdQty.EditValue = dr["QTY"];
                txtProdProdNo.EditValue = dr["PRODNO"];
            }
        }
        #region[BTN]
        //Багц нэмэх
        private void btnGroupAdd_Click(object sender, EventArgs e)
        {
            if (Validate() != "Дараах талбаруудыг гүйцэт бөглөнө үү.")
            {
                MessageBox.Show(Validate(), "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            object[] obj = {
                                   _orderno,
                                   Static.ToLong(txtTabGroupNo.EditValue),
                                   Static.ToDate(dteTabGroupOrderDate.DateTime),
                                   Static.ToDateTime(tmeGroupStart.Time),
                                   Static.ToDateTime(tmeGroupEnd.Time),
                                   Static.ToInt(cboTabGroupRunTime.EditValue)
                               };
            Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130113, 130113, obj);
            if (res.ResultNo == 0)
            {
                RefreshGroup(_orderno);
            }
            else
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //Багц устгах
        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {
                    txtTabGroupNo.EditValue = dr["GROUPNO"];
                }
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130115, 130115, new object[] { _orderno, Static.ToStr(txtTabGroupNo.EditValue) });

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    cboTabGroupRunTime.ItemIndex = 0;
                    RefreshGroup(_orderno);
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
        //Бүтээгдэхүүн устгах(Бараа/Үйлчилгээ)
        private void btnProdDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView2.GetFocusedDataRow();
            if (dr != null)
            {
                txtTabGroupNo.EditValue = dr["GROUPNO"];
                txtProdProdNo.EditValue = dr["PRODNO"];
            }
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130120, 130120, new object[] { _orderno, Static.ToLong(txtTabGroupNo.EditValue), Static.ToStr(txtProdProdNo.EditValue), Static.ToInt(dr["PRODTYPE"]) });

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгагдлаа");
                    //RefreshProd(_orderno, Static.ToLong(txtTabGroupNo.EditValue));
                    RefreshProd(_orderno,1);
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
        //Хуваарь руу үсрэх товч
        private void btnProdSelect_Click(object sender, EventArgs e)
        {
            if (gridControl2.DataSource != null)
            {
                DataRow dr = gridView2.GetFocusedDataRow();
                if (Static.ToInt(dr["ISSCHEDULE"]) == 1)
                {
                    OnEventChoose();
                }
                else { MessageBox.Show("Хуваарь тохируулаагүй байна.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }
        public delegate void delegateEventChoose(DataRow currentrow);
        public event delegateEventChoose EventChoose;
        public void OnEventChoose()
        {
            try
            {
                if (EventChoose != null)
                {
                        DataRow row = gridView2.GetFocusedDataRow();
                        EventChoose(row);
                }
            }
            catch
            { }
        }
        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView2.GetFocusedDataRow();
            if (dr != null)
            {
                if (Static.ToInt(dr["ISSCHEDULE"]) == 1)
                {
                    OnEventChoose();
                }
                else { MessageBox.Show("Хуваарь тохируулаагүй байна.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }
        private void btnProdFind_Click(object sender, EventArgs e)
        {
            //if (txtTabGroupNo.EditValue == null || txtTabGroupNo.Text == "")
            //{
            //    MessageBox.Show("Багцаа эхэлж үүсгэнэ үү.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            string errormsg = "Дараах талбаруудыг гүйцэт бөглөнө үү.";
            if (txtProdQty.EditValue == null || Static.ToInt(txtProdQty.EditValue) == 0)
                errormsg = errormsg + "\r\n Тоо ширхэг оруулна уу.";
            if (errormsg != "Дараах талбаруудыг гүйцэт бөглөнө үү.")
            {
                MessageBox.Show(errormsg, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frm = new Order.frmProductList(_core);
            frm.ucProductList1.EventChoose += new fo_panels.ucProductList.delegateEventChoose(ucProductList1_EventChoose);
            frm.ShowDialog();
        }
        #endregion
        #region[Function]
        private string Validate()
        {
            string msg="Дараах талбаруудыг гүйцэт бөглөнө үү.";
            if (txtTabGroupNo.EditValue == null || txtTabGroupNo.Text == "")
            {
                msg = msg + "\r\nБагцын дугаар оруулна уу.";
            }
            if (dteTabGroupOrderDate.EditValue == null)
                msg = msg + "\r\nБүртгэсэн огноо оруулна уу.";
            if (tmeGroupStart.EditValue == null)
                msg = msg + "\r\nЭхлэх цаг оруулна уу.";
            if (tmeGroupEnd.EditValue == null)
                msg = msg + "\r\nДуусах цаг оруулна уу.";
            if (cboTabGroupRunTime.EditValue==null)
                msg = msg + "\r\nДуусах цаг оруулна уу.";
            return msg;
        }
        public void RefreshProd(string orderno,long groupno)
        {
            Result res = new Result();
            gridControl2.DataSource = null;
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130116, 130116, new object[] { orderno, groupno });
            if (res.ResultNo == 0)
            {
                RepositoryItemImageComboBox imagecombo = new RepositoryItemImageComboBox();
                imagecombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
                ImageComboBoxItem imageitem = new ImageComboBoxItem();
                ImageCollection imgcol = new ImageCollection();
                imgcol.AddImage(_core.Resource.GetImage("alarmclock"));
                imagecombo.Properties.SmallImages = imgcol;
                imageitem.Value = Static.ToDecimal(1);
                imageitem.ImageIndex = 0;
                imagecombo.Properties.Items.Add(imageitem);
                ISM.Template.FormUtility.GridLayoutGet(gridView2, res.Data.Tables[0], _layoutfilename);
                gridView2.Appearance.Row.Font = new Font("Tahoma", 10.0F);
                gridView2.Columns[0].Caption = "Захиалгын дугаар";
                gridView2.Columns[0].Visible = false;
                gridView2.Columns[1].Caption = "Багцын дугаар";
                gridView2.Columns[1].Visible = false;
                gridView2.Columns[2].Caption = "Бүтээгдэхүүний дугаар";
                gridView2.Columns[2].Visible = false;
                gridView2.Columns[3].Caption = "Бүтээгдэхүүний төрлийн дугаар";
                gridView2.Columns[3].Visible = false;
                gridView2.Columns[4].Caption = "Бүтээгдэхүүний төрөл";
                gridView2.Columns[5].Caption = "Тоо ширхэг";
                gridView2.Columns[6].Caption = "Хуваарьтай эсэх";
                gridView2.Columns[6].ColumnEdit = imagecombo;
                gridView2.Columns[10].Caption = "Бүтээгдэхүүний нэр";
                gridView2.Columns[7].Visible = false;
                gridView2.Columns[8].Visible = false;
                gridView2.Columns[9].Visible = false;

                gridView2.Columns[0].OptionsColumn.AllowEdit = false;
                gridView2.Columns[1].OptionsColumn.AllowEdit = false;
                gridView2.Columns[2].OptionsColumn.AllowEdit = false;
                gridView2.Columns[3].OptionsColumn.AllowEdit = false;
                gridView2.Columns[4].OptionsColumn.AllowEdit = false;
                gridView2.Columns[5].OptionsColumn.AllowEdit = false;
                gridView2.Columns[6].OptionsColumn.AllowEdit = false;
                gridView2.Columns[7].OptionsColumn.AllowEdit = false;
                gridView2.Columns[8].OptionsColumn.AllowEdit = false;
                gridView2.Columns[9].OptionsColumn.AllowEdit = false;
                gridView2.Columns[10].OptionsColumn.AllowEdit = false;
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        public void RefreshGroup(string orderno)
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130111, 130111, new object[] { orderno });

                if (res.ResultNo == 0)
                {
                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layoutfilename);
                    gridView1.Columns[0].Caption = "Захиалгын дугаар";
                    gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns[1].Caption = "Багцын дугаар";
                    gridView1.Columns[2].Caption = "Бүртгэсэн огноо";
                    gridView1.Columns[3].Caption = "Эхлэх цаг";
                    gridView1.Columns[3].DisplayFormat.FormatString = "hh::mm:ss"; ;
                    gridView1.Columns[4].DisplayFormat.FormatString = "hh:mm:ss";
                    gridView1.Columns[4].Caption = "Дуусах цаг";
                    gridView1.Columns[5].Caption = "Ажиллах давтамжын дугаар";
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Caption = "Ажиллах давтамж";

                    gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[2].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[3].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[4].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[5].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[6].OptionsColumn.AllowEdit = false;
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

        private void ucOrderGroup_Load(object sender, EventArgs e)
        {
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(txtTabGroupNo);
                _touchkeyboard.AddToKeyboard(dteTabGroupOrderDate);

                _touchkeyboard.AddToKeyboard(cboTabGroupRunTime);
                _touchkeyboard.AddToKeyboard(txtProdQty);
                dteTabGroupOrderDate.EditValue = _core.TxnDate;
            }
        }
        InfoPos.Order.frmProductList frm;
        void ucProductList1_EventChoose(DataTable currentdata)
        {
            Result res = new Result();
            if (currentdata != null)
            {
                foreach (DataRow currentrow in currentdata.Rows)
                {
                    txtProdProdNo.EditValue = currentrow["PRODID"];
                    object[] obj = {
                                   Static.ToStr(_orderno),
                                   //Static.ToLong(txtTabGroupNo.EditValue),
                                   1,
                                   Static.ToStr(txtProdProdNo.EditValue),
                                   Static.ToInt(currentrow["PRODTYPE"]),
                                   Static.ToInt(txtProdQty.EditValue)
                               };
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130118, 130118, obj);
                    if (res.ResultNo == 0)
                    {
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (res.AffectedRows > 0)
                {
                    //RefreshProd(_orderno, Static.ToLong(txtTabGroupNo.EditValue));
                    RefreshProd(_orderno, 1);
                }
                frm.Close();
            }
        }
    }
}
