using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace InfoPos.foo_panels
{
    public partial class ucVeiwSales : UserControl
    {
        #region [ Constant]
        const string cap_lblSalesAmount = "БОРЛУУЛАЛТ ДҮН:";
        const string cap_lblRebateAmount = "ХӨНГӨЛӨЛТ ДҮН:";
        const string cap_lblTotalAmount = "ТӨЛБӨР ДҮН:";
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

        #endregion
        #region [ Control Init ]
        public ucVeiwSales()
        {
            InitializeComponent();
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

                txtSalesAmount.Value = 0;
                txtRebateAmount.Value = 0;
                txtTotalAmount.Value = 0;
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
                        txtTotalAmount.Value = txtSalesAmount.Value - txtRebateAmount.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ucVeiwSales_Load(object sender, EventArgs e)
        {

        }
    }
}
