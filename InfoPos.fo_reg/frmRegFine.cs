using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;
using ISM.Touch;
using InfoPos.Core;

namespace InfoPos.Reg
{
    public partial class frmRegFine : Form
    {
        #region Variables
        Core.Core _core = null;
        string _salesno = null;
        decimal _custno = 0;
        string _prodno = null;
        int _prodtype = 0;
        int _itemno = 0;
        #endregion
        #region Control Events

        public frmRegFine(Core.Core core, string salesno, decimal custno, string prodno, int prodtype, int itemno, string prodname, string rentstatus, string damagenote, decimal overtime)
        {
            InitializeComponent();
            _core = core;
            _salesno = salesno;
            _custno = custno;
            _prodno = prodno;
            _prodtype = prodtype;
            _itemno = itemno;

            txtRentStatus.EditValue = rentstatus;
            txtProdName.EditValue = prodname;
            txtNote.EditValue = damagenote;
            numOverTime.EditValue = overtime;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void btnFree_Click(object sender, EventArgs e)
        {
            Result res = SetRentStatus(_salesno, _custno, _prodno, _prodtype, _itemno, 3, "");
            if (res != null && res.ResultNo != 0) _core.AlertShow("Торгууль төлөх", res.ResultDesc, 2);
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        private void btnPayment_Click(object sender, EventArgs e)
        {
            Result res = null;

            #region Validation

            string paymentno = Static.ToStr(txtFineNo.EditValue);
            if (string.IsNullOrEmpty(paymentno))
            {
                res = new Result(9, "Торгууль төлсөн төлбөрийн дугаараа оруулна уу.");
                goto OnExit;
            }

            #endregion
            #region Update status
            res = SetRentStatus(_salesno, _custno, _prodno, _prodtype, _itemno, 4, paymentno);
            if (res != null && res.ResultNo == 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            #endregion
        OnExit:
            _core.AlertShow("Торгууль төлөх", res.ResultDesc, 2);
        }

        #endregion
        #region Functions
        public Result SetRentStatus(string salesno, decimal custno, string prodno, int prodtype, int itemno, int rentstatus, string paymentno)
        {
            Result res = null;
            try
            {
                if (_core != null && _core.RemoteObject != null)
                {
                    object[] param = new object[] { salesno, custno, prodno, prodtype, itemno, rentstatus, paymentno };
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601013, 601010, param);
                    //if (res.ResultNo != 0) goto OnExit;
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        #endregion
    }
}
