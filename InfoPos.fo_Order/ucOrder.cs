using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;
using ISM.Touch;
namespace InfoPos.fo_order
{
    public partial class ucOrder : DevExpress.XtraEditors.XtraUserControl
    {
        #region[Property]
        private InfoPos.Core.Core _core = null;
        public InfoPos.Core.Core Core
        {
            get { return _core; }
            set
            {
                if (value != null)
                {
                    _core = value;
                    if (_remote == null) _remote = _core.RemoteObject;
                    if (_resource == null) _resource = _core.Resource;
                }
            }
        }

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

        private string _orderno = null;
        public string orderno
        {
            get { return _orderno; }
            set { _orderno = value; }
        }

        private long _custno = 0;
        public long custno
        {
            get { return _custno; }
            set { _custno = value; }
        }

        private DateTime _startdate;
        public DateTime startdate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }

        private DateTime _enddate;
        public DateTime enddate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }

        private int _personcount = 0;
        public int personcount
        {
            get { return _personcount; }
            set { _personcount = value; }
        }

        private int _confrimterm = 0;
        public int confrimterm
        {
            get { return _confrimterm; }
            set { _confrimterm = value; }
        }

        private int _termtype = 0;
        public int termtype
        {
            get { return _termtype; }
            set { _termtype = value; }
        }

        private decimal _orderamount = 0;
        public decimal orderamount
        {
            get { return _orderamount; }
            set { _orderamount = value; }
        }

        private string _curcode = "";
        public string curcode
        {
            get { return _curcode; }
            set { _curcode = value; }
        }

        private decimal _prepaidamount = 0;
        public decimal prepaidamount
        {
            get { return _prepaidamount; }
            set { _prepaidamount = value; }
        }

        private decimal _fee = 0;
        public decimal fee
        {
            get { return _fee; }
            set { _fee = value; }
        }

        private int _status = 0;
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        private DateTime _createdate;
        public DateTime createdate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }

        private DateTime _postdate;
        public DateTime postdate
        {
            get { return _postdate; }
            set { _postdate = value; }
        }

        private int _owneruserno;
        public int owneruserno
        {
            get { return _owneruserno; }
            set { _owneruserno = value; }
        }

        private int _createuserno;
        public int createuserno
        {
            get { return _createuserno; }
            set { _createuserno = value; }
        }
        #endregion
        #region[Constructure]
        public ucOrder()
        {
            InitializeComponent();
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            string msg = Validate();
            if (msg != "Дараах талбаруудыг гүйцэт бөглөнө үү.")
            {
                MessageBox.Show(msg, "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dteStart.DateTime > dteEnd.DateTime)
            {
                MessageBox.Show(this, "Эхлэх дуусах огноо алдаатай байна.", "Алдаа");
                return;
            }
            if (txtOrder.EditValue == null)
            {
                object[] obj = {
                                   Static.ToStr(txtOrder.EditValue),
                                   Static.ToStr(txtCust.EditValue),
                                   Static.ToInt(txtConfirmTerm.EditValue),
                                   Static.ToStr(cboTermType.EditValue),
                                   Static.ToDecimal(txtOrderAmount.EditValue),
                                   Static.ToDecimal(txtPrepaidAmount.EditValue),
                                   Static.ToStr(cboCurCode.EditValue),
                                   Static.ToDecimal(txtFee.EditValue),
                                   Static.ToDateTime(dteStart.EditValue),
                                   Static.ToDateTime(dteEnd.EditValue),
                                   Static.ToInt(txtPersonCount.EditValue),
                                   Static.ToInt(cboStatus.EditValue),
                                   Static.ToDate(_core.TxnDate),
                                   Static.ToDateTime(DateTime.Now),
                                   Static.ToInt(_core.RemoteObject.User.UserNo),
                                   Static.ToInt(_core.RemoteObject.User.UserNo)
                               };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130103, 130103, obj);
                msg = "Амжилттай нэмлээ.";
            }
            else
            {
                object[] obj = {
                                   Static.ToStr(txtOrder.EditValue),
                                   Static.ToStr(txtCust.EditValue),
                                   Static.ToInt(txtConfirmTerm.EditValue),
                                   Static.ToStr(cboTermType.EditValue),
                                   Static.ToDecimal(txtOrderAmount.EditValue),
                                   Static.ToDecimal(txtPrepaidAmount.EditValue),
                                   Static.ToStr(cboCurCode.EditValue),
                                   Static.ToDecimal(txtFee.EditValue),
                                   Static.ToDateTime(dteStart.EditValue),
                                   Static.ToDateTime(dteEnd.EditValue),
                                   Static.ToInt(txtPersonCount.EditValue),
                                   Static.ToInt(cboStatus.EditValue),
                                   Static.ToDate(_createdate),
                                   Static.ToDateTime(_postdate),
                                   Static.ToInt(_createuserno),
                                   Static.ToInt(_owneruserno)
                               };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130104, 130104, obj);
                msg = "Амжилттай засварлалаа.";
            }
            if (res.ResultNo == 0)
            {
                MessageBox.Show(msg);
                if(txtOrder.EditValue==null)
                                    txtOrder.EditValue = res.Param[0];
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        private void ucOrder_Load(object sender, EventArgs e)
        {
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
            }
            FormUtility.LookUpEdit_SetList(ref cboStatus, 0, "Цуцлагдсан");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 1, "Идэвхтэй");
            FormUtility.LookUpEdit_SetList(ref cboStatus, 2, "Баталгаажсан");

            FormUtility.LookUpEdit_SetList(ref cboTermType, "T", "Цаг");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "D", "Өдөр");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "W", "Гараг");
            FormUtility.LookUpEdit_SetList(ref cboTermType, "M", "Сар");

            string msg = "";
            ArrayList Tables = new ArrayList();
            string[] names = { "CURRENCY" };
            DictUtility.PrivNo = 130001;
            Result res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
            DataTable dt = (DataTable)Tables[0];
            if (dt == null)
            {
                msg = "Dictionary-д CURRENCY оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboCurCode, dt, "CURRENCY", "NAME", "", null);
                cboCurCode.ItemIndex = 0;
                cboStatus.ItemIndex = 1;
                cboTermType.ItemIndex = 1;
            }
            _touchkeyboard.AddToKeyboard(dteStart);
            _touchkeyboard.AddToKeyboard(dteEnd);
            _touchkeyboard.AddToKeyboard(txtFee);
            _touchkeyboard.AddToKeyboard(txtConfirmTerm);
            _touchkeyboard.AddToKeyboard(txtOrderAmount);
            _touchkeyboard.AddToKeyboard(txtPersonCount);
            _touchkeyboard.AddToKeyboard(txtPrepaidAmount);
        }
        public void SetValue()
        {
            txtOrder.EditValue = _orderno;
            txtCust.EditValue = _custno;
            txtPersonCount.EditValue = _personcount;
            dteStart.EditValue = _startdate;
            dteEnd.EditValue = _enddate;
            txtOrderAmount.EditValue = _orderamount;
            cboCurCode.EditValue = _curcode;
            cboStatus.EditValue = _status;
            cboTermType.EditValue = _termtype;
            txtPrepaidAmount.EditValue = _prepaidamount;
            txtFee.EditValue = _fee;
            txtConfirmTerm.EditValue = _confrimterm;
        }
        private string Validate()
        {
            string msg="Дараах талбаруудыг гүйцэт бөглөнө үү.";
            if(dteStart.EditValue==null)
            {
                msg = msg + "\r\nЭхлэх огноо оруулна уу.";
            }
            if(dteEnd.EditValue==null)
            {
                msg = msg + "\r\nДуусах огноо оруулна уу.";
            }
            if (txtPersonCount.Text == "" || txtPersonCount.Text == "0")
            {
                msg = msg + "\r\nХүний тоо оруулна уу.";
            }
            if (txtConfirmTerm.Text == "")
            {
                msg = msg + "\r\nБаталгаажих хугацаа оруулна уу";
            }
            return msg;

        }
    }
}
