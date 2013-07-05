using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace InfoPos.fo_order
{
    public partial class ucPerson : UserControl
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
                    //if (_resource == null) _resource = _core.Resource;
                }
            }
        }

        private ISM.CUser.Remote _remote = null;
        public ISM.CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }

        //private ISM.Template.Resource _resource = null;
        //public ISM.Template.Resource Resource
        //{
        //    get { return _resource; }
        //    set { _resource = value; }
        //}

        private ISM.Touch.TouchKeyboard _touchkeyboard = null;
        public ISM.Touch.TouchKeyboard TouchKeyboard
        {
            get { return _touchkeyboard; }
            set { _touchkeyboard = value; }
        }

        private string _orderno = "";
        public string orderno
        {
            get { return _orderno; }
            set { _orderno = value; }
        }
        #endregion
        public ucPerson()
        {
            InitializeComponent();
            this.ResizeRedraw = true;

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            //gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
        }
        public Result Refresh()
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130106, 130106, new object[] { _orderno });

                if (res.ResultNo == 0)
                {
                    gridControl1.DataSource = res.Data.Tables[0];
                    gridView1.Columns[0].Caption = "Захиалгын дугаар";
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns[1].Caption = "Харилцагчийн дугаар";
                    gridView1.Columns[2].Caption = "Овог";
                    gridView1.Columns[3].Caption = "Нэр";
                    gridView1.Columns[4].Caption = "Байгууллагын нэр";
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return res;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
