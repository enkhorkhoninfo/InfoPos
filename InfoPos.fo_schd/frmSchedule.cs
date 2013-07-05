using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Touch;

namespace InfoPos.schd
{
    public partial class frmSchedule : Form,ISM.Touch.ITouchCall
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
        private string _prodno = "";
        public string prodno
        {
            get { return _prodno; }
            set { _prodno = value; }
        }

        private int _prodtype = 0;
        public int prodtype
        {
            get { return _prodtype; }
            set { _prodtype = value; }
        }
        private int _linenumber = 1;
        public int linenumber
        {
            get { return _linenumber; }
            set { _linenumber = value; }
        }
        private int _duration = 120;
        public int duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        private string _orderno = "";
        public string orderno
        {
            get { return _orderno; }
            set { _orderno = value; }
        }

        private string _unit = "T";
        public string unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        private DateTime _selectday;
        public DateTime selectday
        {
            get { return _selectday; }
            set { _selectday = value; }
        }
        #endregion
        #region[Variables]
        bool isinit = false;
        #endregion
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                isinit = true;
                _core = (InfoPos.Core.Core)param;
                if (_core.Resource != null)
                    btnRefresh.Image = _core.Resource.GetImage("navigate_refresh");
                ucSchedule1.core = _core;
                this.ucSchedule1.LineNumber = _linenumber;
                this.ucSchedule1.ProdType = _prodtype;
                this.ucSchedule1.OrderNo = _orderno;
                this.ucSchedule1.ProdNo = _prodno;
                this.ucSchedule1.Duration = _duration;
                this.ucSchedule1.Unit = _unit;

                
                ucSchedule1.StatusAdd(0,"Захиалга хийгдэж байгаа", Color.Aqua);
                ucSchedule1.StatusAdd(1, "Захиалга хаагдсан", Color.OliveDrab);
                ucSchedule1.StatusAdd(2, "Захиалга цуцлагдсан", Color.OrangeRed);
                ucSchedule1.RefreshData(_prodtype, _prodno, _selectday);

                _touchkeyboard = new TouchKeyboard();
                _touchkeyboard.Enable = true;
                _touchkeyboard.AddToKeyboard(dteToday);
                _touchkeyboard.AddToKeyboard(dteStart);
                _touchkeyboard.AddToKeyboard(dteEnd);
                ucSchedule1.TouchKeyboard = _touchkeyboard;
                this.MdiParent = parent;
                this.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            Result res = new Result();
            try
            {
                switch (buttonkey)
                {
                    //case "fo_rent_search":
                    //    tabMain.SelectedTabPageIndex = 0;
                    //    break;
                    //case "fo_rent_tag":
                    //    ShowTagReader();
                    //    break;
                    //case "fo_rent_deliver":
                    //    if (tabMain.SelectedTabPageIndex != 1)
                    //    {
                    //        res = Msg.Get(EnumMessage.PRODUCT_NOT_SELECTED);
                    //    }
                    //    else
                    //    {
                    //        res = ucRentList1.Deliver();
                    //    }

                    //    break;
                    //case "fo_rent_receive":
                    //    if (tabMain.SelectedTabPageIndex != 1)
                    //    {
                    //        res = Msg.Get(EnumMessage.PRODUCT_NOT_SELECTED);
                    //    }
                    //    else
                    //    {
                    //        res = ucRentList1.Receive();
                    //    }
                    //    break;
                }
                ISM.Template.FormUtility.ValidateQuery(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public frmSchedule()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (dteEnd.DateTime.DayOfYear - dteStart.DateTime.DayOfYear < 0)
            {
                MessageBox.Show("Эхлэх дуусах хугацаа алдаатай байна.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dteEnd.DateTime.DayOfYear - dteStart.DateTime.DayOfYear > 30)
            {
                MessageBox.Show("Эхлэх дуусах хугацааны интервал их байна.(<31)", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dteToday.EditValue != null)
            {
                if (dteStart.EditValue == null && dteEnd.EditValue == null)
                {
                    ucSchedule1.RefreshData(1, "100", dteToday.DateTime);
                }
            }
            if (dteToday.EditValue != null && dteStart.EditValue != null && dteEnd.EditValue != null)
                ucSchedule1.RefreshData(1, "100", dteToday.DateTime, dteStart.DateTime, dteEnd.DateTime);
        }

        private void frmSchedule_Load(object sender, EventArgs e)
        {
            if (!isinit)
            {
                if (_core.Resource != null)
                    btnRefresh.Image = _core.Resource.GetImage("navigate_refresh");
                ucSchedule1.core = _core;
                ucSchedule1.LineNumber = _linenumber;
                ucSchedule1.ProdType = _prodtype;
                ucSchedule1.OrderNo = _orderno;
                ucSchedule1.ProdNo = _prodno;
                ucSchedule1.Duration = _duration;
                ucSchedule1.Unit = _unit;

                ucSchedule1.StatusAdd(0, "Захиалга хийгдэж байгаа", Color.Aqua);
                ucSchedule1.StatusAdd(1, "Захиалга хаагдсан", Color.OliveDrab);
                ucSchedule1.StatusAdd(2, "Захиалга цуцлагдсан", Color.OrangeRed);
                dteToday.EditValue=DateTime.Now;
                ucSchedule1.RefreshData(_prodtype, _prodno, Static.ToDate(dteToday.EditValue));
                _touchkeyboard = new TouchKeyboard();
                _touchkeyboard.Enable = true;
                _touchkeyboard.AddToKeyboard(dteToday);
                _touchkeyboard.AddToKeyboard(dteStart);
                _touchkeyboard.AddToKeyboard(dteEnd);
                ucSchedule1.TouchKeyboard = _touchkeyboard;
            }
        }
    }
}