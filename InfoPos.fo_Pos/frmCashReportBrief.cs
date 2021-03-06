﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace InfoPos.Pos
{
    public partial class frmCashReportBrief : Form
    {
        #region Properties

        private int _pageno = 0;
        public int PageNo
        {
            get { return _pageno; }
        }
        private int _pagecount = 0;
        public int PageCount
        {
            get { return _pagecount; }
        }
        private int _pagerows = 20;
        public int PageRows
        {
            get { return _pagerows; }
            set
            {
                if (value > 0 && value < 100)
                    _pagerows = value;
            }
        }

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

        public DataRow CurrentRow
        {
            get { return gridView1.GetFocusedDataRow(); }
        }

        private string _layoutfilename = "";
        public string LayoutFileName
        {
            get { return _layoutfilename; }
        }

        #endregion

        public frmCashReportBrief(InfoPos.Core.Core core)
        {
            InitializeComponent();

            this.Core = core;
            this.ResizeRedraw = true;

            this.FormClosing += frmSalesReportBrief_FormClosing;

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            //gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            //gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.OptionsView.ShowFooter = true;

            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
        }

        void frmSalesReportBrief_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }

        private void frmSalesReportBrief_Load(object sender, EventArgs e)
        {
            Result res = DataRefresh(1);
            if (res != null && res.ResultNo != 0)
                MessageBox.Show(string.Format("{0}: {1}", res.ResultNo, res.ResultDesc));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        
        public void DataColumnRefresh()
        {
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Ээлж");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Кассчин No");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Хийгдсэн цаг");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Кассчин Нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Зузаатгал");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Тушаасан");

            gridView1.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns[2].DisplayFormat.FormatString = "MM/dd HH:mm";

            gridView1.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[5].DisplayFormat.FormatString = "#,###";
            gridView1.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns[5].SummaryItem.DisplayFormat = "{0:#,###}";

            gridView1.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns[4].DisplayFormat.FormatString = "#,###";
            gridView1.Columns[4].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns[4].SummaryItem.DisplayFormat = "{0:#,###}";
            
            //gridView1.RowHeight = 28;
        }
        public Result DataRefresh(int pageno)
        {
            #region Prepare parameters

            object[] param = new object[] { _core.POSNo, _core.AreaCode };

            #endregion
            #region Call server
            Result res = null;
            if (_remote != null)
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 211, 211011, 211011, param);
                if (res != null && res.ResultNo == 0)
                {
                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layoutfilename);
                    DataColumnRefresh();
                }
            }
            else
            {
                res = new Result(1000, "Internal Error: Remote object not set.");
            }
            #endregion

            return res;
        }

    }
}
