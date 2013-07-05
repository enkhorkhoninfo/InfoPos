using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using EServ.Shared;

namespace InfoPos.Panels
{
    public partial class ucCalcSales : UserControl
    {
        #region [ Constant]

        const string cap_lblSalesAmount = "БОРЛУУЛАЛТ ДҮН:";
        const string cap_lblRebateAmount = "ХӨНГӨЛӨЛТ ДҮН:";
        const string cap_lblFineAmount = "ТОРГУУЛЬ ДҮН:";
        const string cap_lblTotalAmount = "ТӨЛБӨР ДҮН:";
        const string cap_lblPayAmount = "ТӨЛСӨН ДҮН:";
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
        InfoPos.Core.Core _core = null;
        public InfoPos.Core.Core core
        {
            get { return _core; }
            set { _core = value; }
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

        private long _SalesNo;
        public long SalesNo
        {
            get { return _SalesNo; }
            set { _SalesNo = value; }
        }

        private int _IsVat;
        public int IsVat
        {
            get { return _IsVat; }
            set 
            {
                chkISVAT.Properties.ReadOnly = true;
                _IsVat = value;
                if (_IsVat == 0)
                    chkISVAT.Checked = false;
                if (_IsVat == 1) chkISVAT.Checked = true;
            }
        }
        public DataTable prodlist;

        #endregion
        #region [ Control Init ]
        public ucCalcSales()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
            }
        }
        private void ucCalcSales_Load(object sender, EventArgs e)
        {
            try
            {
                InitCaption();

                if (_touchkeyboard != null)
                {
                    _touchkeyboard.AddToKeyboard(txtPayAmount);
                }

                SetViewForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
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
        #region [ Init Caption ]
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
        #region[ UserFunction ]
        /// <summary>
        /// Утга олгох (Харилцагч тус бүрээр)
        /// </summary>
        /// <param name="DT"></param>
        public void SetAmount(DataTable DT, long customerno)
        {
            try
            {
                prodlist = DT;
                decimal TotalAmount = 0;
                decimal SalesAmount = 0;
                decimal Discount = 0;
                var query = from row in DT.AsEnumerable()
                            where row.Field<long>("CUSTOMERNO") == customerno
                            select row;
                if (query != null && query.Count() > 0)
                {
                    DT = query.CopyToDataTable();
                    foreach (DataRow dr in DT.Rows)
                    {
                        TotalAmount = TotalAmount + Static.ToDecimal(dr["PRICE"]) * Static.ToDecimal(dr["QUANTITY"]);
                        SalesAmount = SalesAmount + Static.ToDecimal(dr["SALESAMOUNT"]) * Static.ToDecimal(dr["QUANTITY"]);
                        Discount = Discount + Static.ToDecimal(dr["DISCOUNT"]) * Static.ToDecimal(dr["QUANTITY"]);
                    }
                    txtTotalAmount.EditValue = SalesAmount;
                    txtSalesAmount.EditValue = TotalAmount;
                    txtRebateAmount.EditValue = Discount;
                }
                else
                {
                    txtTotalAmount.EditValue = 0;
                    txtSalesAmount.EditValue = 0;
                    txtRebateAmount.EditValue = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SetAmount(DataTable DT)
        {
            try
            {
                prodlist = DT;
                decimal TotalAmount = 0;
                decimal SalesAmount = 0;
                decimal Discount = 0;
                foreach (DataRow dr in DT.Rows)
                {
                    TotalAmount = TotalAmount + Static.ToDecimal(dr["PRICE"]) * Static.ToDecimal(dr["QUANTITY"]);
                    Discount = Discount + Static.ToDecimal(dr["DISCOUNT"]) * Static.ToDecimal(dr["QUANTITY"]);
                    if (chkISVAT.Checked == true)
                    {
                        SalesAmount = SalesAmount + Static.ToDecimal(dr["SALESAMOUNT"]) * Static.ToDecimal(dr["QUANTITY"]);
                    }
                    else
                    {

                        SalesAmount = SalesAmount + Static.ToDecimal(dr["SALESAMOUNT"]) * Static.ToDecimal(dr["QUANTITY"]) - Static.ToDecimal(dr["SALESAMOUNT"]) * Static.ToDecimal(dr["QUANTITY"]) / (_core.Vat + 1);
                    }
                }
                txtTotalAmount.EditValue = SalesAmount;
                txtSalesAmount.EditValue = TotalAmount;
                txtRebateAmount.EditValue = Discount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DiscountState()
        {
            if (prodlist != null)
            {
                prodlist = null;
                txtChargeAmount.EditValue = null;
                txtPayAmount.EditValue = null;
                txtRebateAmount.EditValue = null;
                txtSalesAmount.EditValue = null;
                txtTotalAmount.EditValue = null;

                btnCardPayment.Enabled = false;
                btnCashPayment.Enabled = false;
                btnOtherPayment.Enabled = false;
                btnPayment.Enabled = false;
            }
        }
        public void OtherPaymentState()
        {
            btnCardPayment.Enabled = true;
            btnCashPayment.Enabled = true;
            btnOtherPayment.Enabled = true;
            btnPayment.Enabled = false;
        }
        public void PaymentState()
        {
            btnCardPayment.Enabled = false;
            btnCashPayment.Enabled = false;
            btnOtherPayment.Enabled = false;
            txtPayAmount.Enabled = false;
            btnPayment.Enabled = true;
        }
        public void UnPaymentState()
        {
            btnCardPayment.Enabled = true;
            btnCashPayment.Enabled = true;
            btnOtherPayment.Enabled = true;
            btnPayment.Enabled = false;
        }
        public void FullState()
        {
            btnCardPayment.Enabled = true;
            btnCashPayment.Enabled = true;
            btnOtherPayment.Enabled = true;
        }
        /// <summary>
        /// return object 0 - ҮНЭ, 1 - ХӨНГӨЛӨЛТ,2 - БОРЛУУЛАЛТЫН ДҮН,3 - ТӨЛСӨН ДҮН, 4 - ХАРИУЛТ
        /// </summary>
        /// <returns></returns>
        public object[] GetAmount()
        {
            object[] obj=new object[5];
            obj[0] = txtSalesAmount.EditValue;
            obj[1] = txtRebateAmount.EditValue;
            obj[2] = txtTotalAmount.EditValue;
            obj[3] = txtPayAmount.EditValue;
            obj[4] = txtChargeAmount.EditValue;
            return obj;
        }
        #endregion
        #region[ BTN ]
        private void btnPayment_Click(object sender, EventArgs e)
        {
                OnPayment();
        }
        private void btnOtherPayment_Click(object sender, EventArgs e)
        {
            OnOtherPayment();
        }
        private void btnCashPayment_Click(object sender, EventArgs e)
        {
            if (Static.ToDecimal(txtTotalAmount.EditValue) <= Static.ToDecimal(txtPayAmount.EditValue))
                OnCashPayment();
            else
                MessageBox.Show("Төлсөн дүн төлбөрийн дүнгээс дутуу байна.", "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void btnRebateCalc_Click(object sender, EventArgs e)
        {
            OnDiscount();
        }
        private void btnCardPayment_Click(object sender, EventArgs e)
        {
            OnCardPayment();
        }
        #endregion
        #region[ Custom Events ]
        public delegate void delegateEventOnPayment();
        public event delegateEventOnPayment EventOnPayment;
        public void OnPayment()
        {
            try
            {
                if (EventOnPayment != null) EventOnPayment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }
        
        public delegate void delegateEventOnCashPayment();
        public event delegateEventOnCashPayment EventOnCashPayment;
        public void OnCashPayment()
        {
            try
            {
                if (EventOnCashPayment != null) EventOnCashPayment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }

        public delegate void delegateEventOnCardPayment();
        public event delegateEventOnCardPayment EventOnCardPayment;
        public void OnCardPayment()
        {
            try
            {
                if (EventOnCardPayment != null) EventOnCardPayment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }

        public delegate void delegateEventOnOtherPayment();
        public event delegateEventOnOtherPayment EventOnOtherPayment;
        public void OnOtherPayment()
        {
            try
            {
                if (EventOnOtherPayment != null) EventOnOtherPayment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }

        public delegate void delegateEventOnDiscount();
        public event delegateEventOnOtherPayment EventOnDiscount;
        public void OnDiscount()
        {
            try
            {
                if (EventOnDiscount != null) EventOnDiscount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }
        #endregion
        #region[ Control Value Changed ]
        private void txtPayAmount_EditValueChanged(object sender, EventArgs e)
        {
            txtChargeAmount.EditValue = Static.ToDecimal(txtPayAmount.EditValue) - Static.ToDecimal(txtTotalAmount.EditValue);
        }
        private void txtTotalAmount_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPayAmount.Enabled == false)
            {
                txtPayAmount.EditValue = txtTotalAmount.EditValue;
            }
            txtChargeAmount.EditValue = Static.ToDecimal(txtPayAmount.EditValue) - Static.ToDecimal(txtTotalAmount.EditValue);
        }
        private void txtRebateAmount_EditValueChanged(object sender, EventArgs e)
        {
            txtTotalAmount.EditValue = Static.ToDecimal(txtSalesAmount.EditValue) - Static.ToDecimal(txtRebateAmount.EditValue);
        }
        private void chkISVAT_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
