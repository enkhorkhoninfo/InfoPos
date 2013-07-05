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
    public partial class frmRentReceive : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;
        ISM.Touch.TouchKeyboard _touchkeyboard = null;

        string _salesno = null;
        int _itemno = 0;
        string _invcode = null;
        string _invname = null;
        string _barcode = null;
        int _userno = 0;
        int _userstate = 0;
        #region Constructor

        public frmRentReceive(InfoPos.Core.Core core, ISM.Touch.TouchKeyboard kb, string salesno, int itemno, string invcode, string invname, string barcode,int userno,int userstate)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmRentDeliver_FormClosing);

            _core = core;
            _touchkeyboard = kb;

            _salesno = salesno;
            _itemno = itemno;
            _invcode = invcode;
            _invname = invname;
            _barcode = barcode;
            _userno = userno;
            _userstate = userstate;
        }

        void frmRentDeliver_FormClosing(object sender, FormClosingEventArgs e)
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

        private void frmRentDeliver_Load(object sender, EventArgs e)
        {
            chkBroken_CheckedChanged(null, null);

            txtInvName.EditValue = _invname;
            txtInvCode.EditValue = _barcode;

            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(txtNote);
            }

        }
        private void chkBroken_CheckedChanged(object sender, EventArgs e)
        {
            radType.Enabled = chkBroken.Checked;
            txtNote.Enabled = chkBroken.Checked;
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

        }
        public string BarcodeRead()
        {
            //return _core.BarCoder.Read();
            return "";
        }
        public void ServerCall()
        {
            Result res = null;

            #region Prepare parameters
            object[] param = new object[] { 
                                            _salesno
                                            , _invcode
                                            , _itemno
                                            , DateTime.Now /*_core.TxnDate -энд утга орж ирэхгүй бн, шалгах!*/
                                            , chkBroken.Checked ? 9:2 /*0 -идэвхигүй,1- олгоогүй, 2-Олгосон, 9-Эвдэрсэн*/
                                            , chkBroken.Checked ? ISM.Lib.Static.ToInt (radType.EditValue) : 0
                                            , chkBroken.Checked ? txtNote.Text : ""
                                            , _userno
                                            , _userstate
                                            , _barcode
                                          };
            #endregion

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                , 500, 500102, 500102, param);

            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        #endregion
    }
}