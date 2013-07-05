using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EServ.Shared;
namespace InfoPos.fo_panels
{
    public partial class ucSalesPayment : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSalesPayment()
        {
            InitializeComponent();
        }

        #region[ BTN Events]
        private void btnRebateCalc_Click(object sender, EventArgs e)
        {
            OnDiscount();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            OnPayment();
        }

        private void btnCashPayment_Click(object sender, EventArgs e)
        {
            OnCashPayment();
        }

        private void btnCardPayment_Click(object sender, EventArgs e)
        {
            OnCardPayment();
        }

        private void btnOtherPayment_Click(object sender, EventArgs e)
        {
            OnOtherPayment();
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
        #region[Function]
        #region[ PaymentState - Төлбөрүүдийн төлөв. ]

        /// <summary>
        /// Бүртгэл сонгож борлуулалт хийх төлөв.
        /// </summary>
        public void RegisterState()
        {
            btnRegisterPayment.Enabled = true;

            btnCardPayment.Enabled = false;
            btnCashPayment.Enabled = false;
            btnOtherPayment.Enabled = false;
        }

        /// <summary>
        /// Бэлэн, Карт, Бусад төлбөрийн хэрэгсэлээр борлуулалт хийх төлөв.
        /// </summary>
        public void PaymentState()
        {
            btnCardPayment.Enabled = true;
            btnCashPayment.Enabled = true;
            btnOtherPayment.Enabled = true;
            txtPayAmount.Properties.ReadOnly = false;

            btnRegisterPayment.Enabled = false;
        }

        public void DiscountState()
        {
            btnRebateCalc.Enabled = true;

            btnCardPayment.Enabled = false;
            btnCashPayment.Enabled = false;
            btnOtherPayment.Enabled = false;
            btnRegisterPayment.Enabled = false;

            txtChargeAmount.Properties.ReadOnly = true;
            txtPayAmount.Properties.ReadOnly = true;
            txtRebateAmount.Properties.ReadOnly = true;
            txtSalesAmount.Properties.ReadOnly = true;
            txtTotalAmount.Properties.ReadOnly = true;
        }
        #endregion
        /// <summary>
        /// Төлбөрийн мэдээлэл оруулах
        /// </summary>
        /// <param name="PRODUCTBASKET">Бараа үйлчилгээний сагс</param>
        /// <returns></returns>
        public Result SetPaymentAmount(DataTable PRODUCTBASKET)
        {
            Result res = new Result();
            try
            {
                if (PRODUCTBASKET != null)
                {
                    decimal salesamount = PRODUCTBASKET.AsEnumerable().Sum(x => Static.ToDecimal(x["PRICE"]) * Static.ToInt(x["QUANTITY"]));
                    decimal discountamount = PRODUCTBASKET.AsEnumerable().Sum(x => Static.ToDecimal(x["DISCOUNT"]) * Static.ToInt(x["QUANTITY"]));
                    decimal totalamount = PRODUCTBASKET.AsEnumerable().Sum(x => Static.ToDecimal(x["SALESAMOUNT"]) * Static.ToInt(x["QUANTITY"]));

                    txtSalesAmount.EditValue = salesamount;
                    txtRebateAmount.EditValue = discountamount;
                    txtTotalAmount.EditValue = totalamount;
                }
                else
                {
                    res.ResultNo = 1000;
                    res.ResultDesc = "Бараа үйлчилгээ цуглуулаагүй байна.";
                    return res;
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1001;
                res.ResultDesc = ex.Message;
                return res;
            }
        }
        /// <summary>
        /// Төлбөрийн мэдээлэл авах
        /// </summary>
        /// <param name="Amounts">
        ///0 - Борлуулалтын бохир дүн
        ///1 - Хөнгөлөлтийн дүн
        ///2 - Цэвэр дүн
        ///3 - Төлж буй дүн
        ///4 - Хариулт
        ///5 - Өмнө төлөгдсөн дүн</param>
        /// <returns></returns>
        public decimal[] GetPaymentAmount()
        {
            decimal[] Amounts = new decimal[6];

            Amounts[0] = Static.ToDecimal(txtSalesAmount.EditValue);
            Amounts[1] = Static.ToDecimal(txtRebateAmount.EditValue);
            Amounts[2] = Static.ToDecimal(txtTotalAmount.EditValue);
            Amounts[3] = Static.ToDecimal(txtPayAmount.EditValue);
            Amounts[4] = Static.ToDecimal(txtChargeAmount.EditValue);
            Amounts[5] = Static.ToDecimal(numPaidAmount.EditValue);   
     
            return Amounts;
        }
        #endregion
    }
}
