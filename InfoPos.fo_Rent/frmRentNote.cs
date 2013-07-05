using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;

namespace InfoPos.Rent
{
    public partial class frmRentNote : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;
        ISM.Touch.TouchKeyboard _touchkeyboard = null;

        string _salesno = null;
        decimal _custno = 0;
        int _itemno = 0;
        string _prodno = null;
        string _prodname = null;
        int _rentstatus = 0;
        string _damagetype = null;
        string _reparation=null;
        int _userno = 0;

        #region Constructor

        public frmRentNote(InfoPos.Core.Core core, ISM.Touch.TouchKeyboard kb, string salesno, decimal custno, int itemno, string prodno, string prodname, int userno)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmReceive_FormClosing);

            _core = core;
            _touchkeyboard = kb;

            _salesno = salesno;
            _custno = custno;
            _itemno = itemno;
            _prodno = prodno;
            _prodname = prodname;
            _userno = userno;

            //_invname = invname;
            //_rentstatus = rentstatus;
            //_damagetype = damagetype;
            //_reparation = reparation;
            //_userstate = userstate;
        }
        void frmReceive_FormClosing(object sender, FormClosingEventArgs e)
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
            InitDamageType();
            SetStatus();

            txtInvCode.EditValue = _prodno;
            txtInvName.EditValue = _prodname;
            radType.EditValue = _damagetype;
            txtReparation.EditValue = "";

            ServerGet();

            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(txtNote);
            }
        }
        private void chkBroken_CheckedChanged(object sender, EventArgs e)
        {
            SetStatus();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            Result res = ServerSave();

            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region User Functions

        public void InitDamageType()
        {
            DataTable dt = null;
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject, "PADAMAGETYPE", ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                foreach(DataRow r in dt.Rows)
                {
                    DevExpress.XtraEditors.Controls.RadioGroupItem item = new DevExpress.XtraEditors.Controls.RadioGroupItem();
                    item.Value = r["DAMAGETYPE"];
                    item.Description = Static.ToStr(r["NAME"]);
                    radType.Properties.Items.Add(item);
                }
            }
        }

        public void SetStatus()
        {
            radType.Enabled = chkBroken.Checked;
            txtNote.Enabled = chkBroken.Checked;
            txtReparation.Enabled = chkBroken.Checked;
                       
        }
        public Result ServerGet()
        {
            Result res = null;

            #region Prepare parameters
            object[] param = new object[] { _salesno, _custno, _prodno, _itemno };
            #endregion

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 606, 606009, 606001, param);

            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                DataTable dt = res.Data.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    _damagetype = Static.ToStr(dt.Rows[0]["DAMAGETYPE"]);
                    _rentstatus = Static.ToInt(dt.Rows[0]["RENTSTATUS"]);

                    chkBroken.Checked = _damagetype != "";

                    radType.EditValue = _damagetype;
                    txtNote.EditValue = Static.ToStr(dt.Rows[0]["DAMAGENOTE"]);
                    txtReparation.EditValue = Static.ToStr(dt.Rows[0]["LOSSPAYMENTNO"]);

                    txtDuration.EditValue = string.Format("ИЛҮҮ ЦАГ {0} МИНУТ.", Static.ToStr(dt.Rows[0]["DURATION"]));
                    
                    if (_rentstatus == 0) txtStatus.EditValue = "ОЛГООГҮЙ";
                    else if (_rentstatus == 1) txtStatus.EditValue = "ОЛГОСОН";
                    else if (_rentstatus == 2) txtStatus.EditValue = "ХҮЛЭЭН АВСАН";
                    else txtStatus.EditValue = "ТОДОРХОЙГҮЙ!";
                }
            }

            return res;
        }
        public Result ServerSave()
        {
            Result res = null;

            #region Prepare parameters
            object[] param = new object[] { 
                _salesno, _custno, _prodno, _itemno
                , chkBroken.Checked ? ISM.Lib.Static.ToStr (radType.EditValue) : ""
                , chkBroken.Checked ? txtNote.Text : ""
                , chkBroken.Checked ? txtReparation.Text : ""
                , _userno
            };
            #endregion

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 606, 606008, 606008, param);

            return res;
        }

        #endregion
    }
}