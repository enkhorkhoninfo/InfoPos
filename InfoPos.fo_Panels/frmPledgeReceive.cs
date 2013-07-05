using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;

namespace InfoPos.Panels
{
    public partial class frmPledgeReceive : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;
        ISM.Touch.TouchKeyboard _touchkeyboard = null;

        public string _custno = null;
        public string _pledgeno = null;
        public string _docno = null;
        public int _doctype = 0;

        #region Constructor

        public frmPledgeReceive(InfoPos.Core.Core core, ISM.Touch.TouchKeyboard kb, string custno, string pledgeno, string docno=null, int doctype=0)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmPledgeReceive_FormClosing);

            _core = core;
            _touchkeyboard = kb;

            _custno = custno;
            _pledgeno = pledgeno;
            _docno = docno;
            _doctype = doctype;
        }

        void frmPledgeReceive_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //_core.BarCoder.Close();
            }
            catch
            { }
        }

        #endregion

        #region Control Events

        private void frmPledgeReceive_Load(object sender, EventArgs e)
        {
            txtDocNo.EditValue = _docno;
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(txtDocNo);
            }
            
            InitList();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            ServerCall();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region User Functions

        public void InitList()
        {
            try
            {
                DataTable dt = new DataTable();
                Result res = ISM.Template.DictUtility.Get(_core.RemoteObject, "PLEDGETYPE", 500011, ref dt);
                if (res != null && res.ResultNo == 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DevExpress.XtraEditors.Controls.RadioGroupItem item = new DevExpress.XtraEditors.Controls.RadioGroupItem();
                        item.Value = row[0];
                        item.Description = Static.ToStr(row[1]);
                        
                        radType.Properties.Items.Add(item);
                    }
                }
                radType.EditValue = _docno;
            }
            catch (Exception ex)
            {
                
            }
        }
        public void ServerCall()
        {
            Result res = null;

            #region Validation

            if (radType.SelectedIndex < 0)
            {
                res = new Result(9, "Баримт бичгийн төрөл сонгогдоогүй байна!");
                goto OnExit;
            }
            _doctype = Static.ToInt(radType.EditValue);
            _docno = Static.ToStr(txtDocNo.EditValue);

            if (string.IsNullOrEmpty(_docno))
            {
                res = new Result(9, "Баримт бичгийн дугаараа оруулаагүй байна!");
                goto OnExit;
            }

            #endregion

            #region Prepare parameters

            object[] param = new object[] { _custno, _pledgeno, _core.RemoteObject.User.UserNo, _docno, _doctype, 0 };

            #endregion

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                , 500, 500011, 500011, param);

            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                _pledgeno = res.ResultDesc; // Хэрэв шинээр үүсгэсэн бол шинэ барьцааны дугаар буцаж ирнэ.

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }

        OnExit:

            ISM.Template.FormUtility.ValidateQuery(res);

        }
        
        #endregion

    }
}