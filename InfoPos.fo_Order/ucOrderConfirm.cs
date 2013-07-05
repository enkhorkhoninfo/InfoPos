using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
namespace InfoPos.Order
{
    public partial class ucOrderConfirm : DevExpress.XtraEditors.XtraUserControl
    {
        #region[Properties]
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
        private string _orderno;
        public string orderno
        {
            get { return _orderno; }
            set { _orderno = value; txtOrderNo.EditValue = _orderno; }
        }
        #endregion
        public ucOrderConfirm()
        {
            InitializeComponent();
        }

        private void ucStatusChange_Load(object sender, EventArgs e)
        {
            txtOrderNo.EditValue = _orderno;
            if (Resource != null)
            {
                btnConfrim.Image = Resource.GetImage("button_ok");
            }
        }

        private void btnConfrim_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Захиалгыг баталгаажуулахдаа итгэлтэй байна уу.","Баталгаажуулалт", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return;
            Result res = OrderConfirm();
            if (res.ResultNo == 0)
            {
                MessageBox.Show("Амжилттай баталгаажууллаа.");
            }
            else
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
        }
        /// <summary>
        /// Захиалга
        /// </summary>
        /// <returns></returns>
        public Result OrderConfirm()
        {
            Result res = new Result();
            if (txtOrderNo.EditValue == null || txtOrderNo.Text == "")
            {
                res.ResultNo = 1;
                res.ResultDesc = "Захиалга сонгогдоогүй байна.";
                return res;
            }
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130126, 130126, new object[] { txtOrderNo.EditValue, DateTime.Now, _core.RemoteObject.User.UserNo });
            return res;
        }
    }
}
