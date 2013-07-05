using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace InfoPos.fo_panels
{
    public partial class ucCalcSales : UserControl
    {
        #region [ Constant]

        const string cap_lblSalesAmount = "БОРЛУУЛАЛТ ДҮН:";
        const string cap_lblRebateAmount = "ХӨНГӨЛӨЛТ ДҮН:";
        const string cap_lblFineAmount = "ТОРГУУЛЬ ДҮН:";
        const string cap_lblTotalAmount = "ТӨЛБӨР ДҮН:";
        const string cap_lblPayAmount = "ТӨЛБӨР ДҮН:";
        const string cap_lblChargeAmount = "ХАРИУЛТ:";

        #endregion
        #region [ Variables ]
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
            set { _resource = value; }
        }

        private ISM.Touch.TouchKeyboard _touchkeyboard = null;
        public ISM.Touch.TouchKeyboard TouchKeyboard
        {
            get { return _touchkeyboard; }
            set { _touchkeyboard = value; }
        }


        private int _ViewType;
        public int ViewType
        {
            get { return _ViewType; }
            set { _ViewType = value; }
        }

        public DataTable prodlist;

        #endregion
        #region [ Control Init ]
        public ucCalcSales()
        {
            InitializeComponent();
        }
        private void ucCalcSales_Load(object sender, EventArgs e)
        {
            try
            {
                InitCaption();

                if (_touchkeyboard != null)
                {
                    _touchkeyboard.AddToKeyboard(txtSalesAmount);
                    _touchkeyboard.AddToKeyboard(txtRebateAmount);
                    _touchkeyboard.AddToKeyboard(txtTotalAmount);
                    _touchkeyboard.AddToKeyboard(txtPayAmount);
                    _touchkeyboard.AddToKeyboard(txtChargeAmount);
                }

                SetViewForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SetViewForm()
        {
            if (_ViewType == 1)
            {
                btnCashPayment.Visible = false;
                btnCardPayment.Visible = false;
                btnOtherPayment.Visible = false;
                btnRebateCalc.Visible = false;

                lblChargeAmount.Visible = false;
                lblPayAmount.Visible = false;

                txtPayAmount.Visible = false;
                txtChargeAmount.Visible = false;

                lblSalesAmount.Location = new Point(lblSalesAmount.Location.X - 147, lblSalesAmount.Location.Y - 47);
                lblRebateAmount.Location = new Point(lblRebateAmount.Location.X - 147, lblRebateAmount.Location.Y - 47);
                lblTotalAmount.Location = new Point(lblTotalAmount.Location.X - 147, lblTotalAmount.Location.Y - 47);
                txtSalesAmount.Location = new Point(txtSalesAmount.Location.X - 147, txtSalesAmount.Location.Y - 47);
                txtRebateAmount.Location = new Point(txtRebateAmount.Location.X - 147, txtRebateAmount.Location.Y - 47);
                txtTotalAmount.Location = new Point(txtTotalAmount.Location.X - 147, txtTotalAmount.Location.Y - 47);

                groupControl1.Width = 247;
                groupControl1.Height = 136;

                this.Width = 256;
                this.Height = 245;
            }
        }
        #endregion
        #region [ Init Caption]
        private void InitCaption()
        {
            try
            {
                lblSalesAmount.Text = cap_lblSalesAmount;
                lblRebateAmount.Text = cap_lblRebateAmount;
                lblTotalAmount.Text = cap_lblTotalAmount;
                lblPayAmount.Text = cap_lblPayAmount;
                lblChargeAmount.Text = cap_lblChargeAmount;

                txtSalesAmount.Value = 0;
                txtRebateAmount.Value = 0;
                txtTotalAmount.Value = 0;
                txtPayAmount.Value = 0;
                txtChargeAmount.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        public void SetAmount(DataTable DT)
        {
            try
            {
                if (DT != null)
                {
                    if (DT.Rows.Count > 0)
                    {
                        txtSalesAmount.Value = Static.ToDecimal(DT.Rows[0][0]);
                        txtRebateAmount.Value = Static.ToDecimal(DT.Rows[0][1]);
                        //txtFineAmount.Value = 0;
                        //txtTotalAmount.Value = Static.ToDecimal(DT.Rows[0][2]);
                        //txtPayAmount.Value = 0;
                        //txtChargeAmount.Value = 0;

                        txtTotalAmount.Value = txtSalesAmount.Value - txtRebateAmount.Value;
                        if (txtPayAmount.Value>0)
                            txtChargeAmount.Value = txtTotalAmount.Value - txtPayAmount.Value;
                        else
                            txtChargeAmount.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCashPayment_Click(object sender, EventArgs e)
        {
            string _custno = "";

            object[] salesmain = new object[12];
            object[] obj = new object[2];
            //CUSTNO, SALESNO, POSTDATE, STATUS, TOTALAMOUNT, SALESAMOUNT, DISCOUNT, VAT, CURCODE, POSNO, CASHIERNO, IP, MAC
            salesmain[0] = _custno;
            salesmain[1] = DateTime.Now;
            salesmain[2] = 0; //Status
            salesmain[3] = txtTotalAmount.Value;
            salesmain[4] = txtSalesAmount.Value;
            salesmain[5] = txtRebateAmount.Value;
            salesmain[6] = 0; //VAT
            salesmain[7] = "MNT";
            salesmain[8] = _remote.User.UserNo;
            salesmain[9] = _remote.User.UserNo;  //Casher ???
            salesmain[10] = _remote.User.IPAddress;
            salesmain[11] = _remote.User.NICAddress;

            obj[0] = salesmain;
            obj[1] = prodlist;

            Result res = _remote.Connection.Call(_remote.User.UserNo, 501, 500007, 500007, 0, 0, obj);
            if (res.ResultNo == 0)
            {
                OnPayment(0, res.ResultDesc);
            }
            else
            {
                MessageBox.Show(res.ResultDesc);
            }
        }

        public delegate void delegateEventOnPayment(int pPaymentType, string pSalesNo);
        public event delegateEventOnPayment EventOnPayment;
        public void OnPayment(int pPaymentType, string pSalesNo)
        {
            try
            {
                if (EventOnPayment != null) EventOnPayment(pPaymentType, pSalesNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.Source);
            }
        }
    }
}
