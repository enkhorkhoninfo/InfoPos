using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ISM.Touch;
using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmExtendService : DevExpress.XtraEditors.XtraForm
    {
        #region Variables

        private InfoPos.Core.Core _core = null;

        private decimal _qty = 0;
        public decimal Qty
        {
            get { return _qty; }
        }

        #endregion
        
        #region Control Events
        public frmExtendService(InfoPos.Core.Core core, string prodname)
        {
            InitializeComponent();
            this.ucNumpad1.EventClickExtraButton += ucNumpad1_EventClickExtraButton;

            _core = core;

            lblProdName.Text = prodname;

            ucNumpad1.ExtraShow2 = true;
            ucNumpad1.ExtraShow3 = true;

            ucNumpad1.ExtraText2 = "OK";
            ucNumpad1.ExtraText3 = "Гарах";

        }
        private void frmExtendService_Load(object sender, EventArgs e)
        {
            ucNumpad1.EditControl = numQty;
        }
        private void ucNumpad1_EventClickExtraButton(int num)
        {
            switch (num)
            {
                case 2:
                    decimal qty = Static.ToDecimal(numQty.EditValue);
                    if (qty < 0)
                    {
                        _core.AlertShow("Нэмэлт борлуулалт", "Тоо ширхэг буруу байна.", 2);
                        return;
                    }
                    _qty = qty;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    break;
                case 3:
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;
            }
        }
        #endregion

    }
}
