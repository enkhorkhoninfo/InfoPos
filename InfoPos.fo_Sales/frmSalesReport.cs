using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using ISM.Template;
using ISM.Touch;
using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmSalesReport : DevExpress.XtraEditors.XtraForm,ISM.Touch.ITouchCall
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        ISM.Touch.TouchKeyboard _kb;
        
        string _layoutfilename1 = "";
        string _layoutfilename2 = "";
        string _layoutfilename3 = "";
        string _layoutfilename4 = "";

        #endregion

        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _kb = new TouchKeyboard();
                _kb.Enable = true;
               
                xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                                
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
                    case "report_sales": xtraTabControl1.SelectedTabPageIndex = 0; break;
                    case "report_payments": xtraTabControl1.SelectedTabPageIndex = 1; break;
                    case "report_exit": item.IsClose = 1; this.Close(); break;
                }
                ISM.Template.FormUtility.ValidateQuery(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public frmSalesReport()
        {
            InitializeComponent();

            this.FormClosing += frmSalesDetails_FormClosing;
            this.xtraTabControl1.SelectedPageChanged += xtraTabControl1_SelectedPageChanged;

            gridControl1.Dock = DockStyle.Fill;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
            gridView1.ScrollStyle = ScrollStyleFlags.LiveHorzScroll;
            gridView1.OptionsCustomization.AllowColumnResizing = true;
            gridView1.OptionsCustomization.AllowColumnMoving = true;
            gridView1.OptionsCustomization.AllowFilter = true;
            gridView1.OptionsCustomization.AllowGroup = true;
            gridView1.OptionsCustomization.AllowQuickHideColumns = true;
            gridView1.OptionsCustomization.AllowSort = true;
            gridView1.OptionsView.ColumnAutoWidth = false;

            gridControl2.Dock = DockStyle.Fill;
            gridView2.OptionsView.ShowAutoFilterRow = true;
            gridView2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            gridView2.Appearance.Row.Font = new Font("Tahoma", 10.0F);
            gridView2.ScrollStyle = ScrollStyleFlags.LiveHorzScroll;
            gridView2.OptionsCustomization.AllowColumnResizing = true;
            gridView2.OptionsCustomization.AllowColumnMoving = true;
            gridView2.OptionsCustomization.AllowFilter = true;
            gridView2.OptionsCustomization.AllowGroup = true;
            gridView2.OptionsCustomization.AllowQuickHideColumns = true;
            gridView2.OptionsCustomization.AllowSort = true;
            gridView2.OptionsView.ColumnAutoWidth = false;

            
            _layoutfilename1 = string.Format(@"{0}\Data\Layout_{1}_rep1.xml", Static.WorkingFolder, this.GetType().Name);
            _layoutfilename2 = string.Format(@"{0}\Data\Layout_{1}_rep2.xml", Static.WorkingFolder, this.GetType().Name);
            _layoutfilename3 = string.Format(@"{0}\Data\Layout_{1}_rep3.xml", Static.WorkingFolder, this.GetType().Name);
            _layoutfilename4 = string.Format(@"{0}\Data\Layout_{1}_rep4.xml", Static.WorkingFolder, this.GetType().Name);
        }

        void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xtraTabControl1.TabPages.IndexOf(e.Page))
            {
                case 0:
                    Report1();
                    break;
                case 1:
                    Report2();
                    break;
            }
        }

        void frmSalesDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename1);
            ISM.Template.FormUtility.GridLayoutSave(gridView2, _layoutfilename2);
        }
        private void frmSalesDetails_Load(object sender, EventArgs e)
        {
            Report1();
        }
        

        public void Report1()
        {
            Result res = null;
            try
            {
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(1000, "Internal Error: Remote object not set.");
                    goto OnExit;
                }

                object[] param = new object[] { _core.TxnDate, _core.POSNo, _core.AreaCode };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605041, 605041, param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layoutfilename1);

                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Огноо");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Цаг");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Ээлж");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Касс");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "ПОС");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Талбар");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Борл.№");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "ХД");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Харилцагч");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Бүт.№");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Бүт.Төрөл");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "Бүт.Нэр");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 12, "Үнэ");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 13, "Суурь үнэ");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 14, "Тоо");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 15, "Дүн");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 16, "Хөн.Бараа");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 17, "Хөн.Борл");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 18, "Борл.Төрөл");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 19, "Флаг");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 20, "Дэд төрөл");
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        public void Report2()
        {
            Result res = null;
            try
            {
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(1000, "Internal Error: Remote object not set.");
                    goto OnExit;
                }

                object[] param = new object[] { _core.TxnDate, _core.POSNo, _core.AreaCode };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605042, 605042, param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                ISM.Template.FormUtility.GridLayoutGet(gridView2, res.Data.Tables[0], _layoutfilename2);

                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 0, "Огноо");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 1, "Цаг");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 2, "Ээлж");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 3, "Касс");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 4, "ПОС");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 5, "Талбар");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 6, "Борл.№");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 7, "ХД");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 8, "Харилцагч");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 9, "Төлбөр.№");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 10, "Хэрэгсэл");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 11, "Бүртгэл");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 12, "Төлбөр дүн");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 13, "Флаг");
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
    }
}