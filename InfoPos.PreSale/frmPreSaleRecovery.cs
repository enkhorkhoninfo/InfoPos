using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.PreSale
{
    public partial class frmPreSaleRecovery : DevExpress.XtraEditors.XtraForm
    {
        #region [ Variable ]
        InfoPos.Core.Core _core;
        string appname = "", formname = "";
        string _presaleno = "";
        Form FormName = null;
        #endregion
        public frmPreSaleRecovery(InfoPos.Core.Core core, string presaleno)
        {
            InitializeComponent();
            _presaleno = presaleno;
            if (_presaleno != "")
            {
                txtPreSaleNo.EditValue = presaleno;
            }
            _core = core;
        }
        private void frmOrderConfirm_Load(object sender, EventArgs e)
        {

        }
        private void btnOrderNo_Click(object sender, EventArgs e)
        {
            InfoPos.List.OrderList frm = new List.OrderList(_core);
            frm.ucOrderList.Browsable = true;
            DialogResult res = frm.ShowDialog();
            if ((res == System.Windows.Forms.DialogResult.OK))
            {
                int status = Static.ToInt(frm.ucOrderList.SelectedRow["STATUS"]);
                string msg = "";

                switch(status)
                {
                    //case 1: msg = "Захиалгыг баталгаажуулсан байна."; break;
                    case 2: msg = "Урьдчилсан борлуулалт гүйцэтгэгдсэн байна."; break;
                    //case 8: msg = "Захиалгын хугацаа дууссан байна."; break;
                    //case 9: msg = "Захиалга цуцлагдсан байна."; break;
                }

                if (msg != "")
                {
                    MessageBox.Show(msg);
                    return;
                }

                if(status != 0)
                {
                    MessageBox.Show("Урьдчилсан борлуулалтын төлөв тодорхойгүй байна.");
                    return;
                }

                txtPreSaleNo.EditValue = frm.ucOrderList.SelectedRow["PRESALENO"];
                //txtOrderName.EditValue = frm.ucOrderList.SelectedRow["ORDERNAME"];
                txtCustomerName.EditValue = frm.ucOrderList.SelectedRow["CUSTOMERNAME"];
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                if (Static.ToStr(txtPreSaleNo.EditValue) != "0" && Static.ToStr(txtPreSaleNo.EditValue).Trim() != "")
                {
                    object[] obj1 = new object[1];
                    obj1[0] = Static.ToStr(txtPreSaleNo.EditValue);

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 130102, 130102, obj1);

                    if (res.ResultNo == 0)
                    {
                        object[] obj = new object[3];
                        obj[0] = _core;
                        obj[1] = txtPreSaleNo.EditValue;
                        obj[2] = res.Data.Tables[0].Rows[0];
                        EServ.Shared.Static.Invoke("InfoPos.Enquiry.dll", "InfoPos.Enquiry.Main", "CallOrderEnquiry", obj);
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
        private void btnOk_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                string msg = "";
                if (Static.ToStr(txtPreSaleNo.EditValue) == "0" || Static.ToStr(txtPreSaleNo.EditValue).Trim() == "")
                    msg += "Урьдчилсан борлуулалтаа сонгоно уу";

                //if (Static.ToStr(txtNote.EditValue).Trim() == "")
                //    msg += "\nБаталгаажуулах тайлбараа оруулна уу";

                if (msg == "")
                {
                    MessageBox.Show(msg);
                    return;
                }
                
                object[] obj1 = new object[2];
                obj1[0] = Static.ToStr(txtPreSaleNo.EditValue);
                obj1[1] = Static.ToStr(_core.RemoteObject.User);
                //obj1[2] = Static.ToStr(txtNote.EditValue);

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 213, 130333, 130333, obj1);

                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай сэргээгдлээ.");
                    btnOk.Enabled = false;
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}