using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace InfoPos.Panels
{
    public partial class frmRentTagReader : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;

        #region Public Properties

        private string _custno = null;
        public string CustNo
        {
            get { return _custno; }
        }
        private string _serialno = null;
        public string SerialNo
        {
            get { return _serialno; }
        }

        #endregion
        #region Constractors
        public frmRentTagReader(InfoPos.Core.Core core)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmRentTagReader_FormClosing);
            _core = core;
        }
        #endregion
        #region Control Events
        void frmRentTagReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Таг уншигчийг хаах, чөлөөлөх
            // _core.TagReader.Close();
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            // Таг уншигчийг ажилд оруулах, унших
            // _core.TagReader.Open();

            _custno = "22000000"; // тагаас уншсан утгаа энд хийнэ.
            _serialno = "222"; // тагаас уншсан утгаа энд хийнэ.

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}