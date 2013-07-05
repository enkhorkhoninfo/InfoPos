using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using EServ.Shared;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using NativeExcel;
using System.IO;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class FormCurrency : ISM.Template.frmTempProp
    {
        #region[Variable]
        DataTable _DT;
        object[] OldValue;
        object[] FieldName;
        int SelectTxnCode = 140016;
        int AddTxnCode = 140018;
        int EditTxnCode = 140019;
        int DeleteTxnCode = 140020;
        int btn = 0;
        int rowhandle = 0;
        FunctionParam fp = new FunctionParam();
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormCurrency(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            this.Resource = _core.Resource;
            Init();
            InitCombo();
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init Function]
        public void InitCombo()
        {
            fp.SetCombo(_core, "CHART", "ACCOUNT", "NAME", SelectTxnCode, cboGLExchProfit, "", null);
            fp.SetCombo(_core, "CHART", "ACCOUNT", "NAME", SelectTxnCode, cboGLExchLoss, "", null);
            fp.SetCombo(_core, "CHART", "ACCOUNT", "NAME", SelectTxnCode, cboGLRevProfit, "", null);
            fp.SetCombo(_core, "CHART", "ACCOUNT", "NAME", SelectTxnCode, cboGLRevLoss, "", null);
        }
        private void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormCurrency_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormCurrency_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormCurrency_EventSave);
                this.EventEdit += new delegateEventEdit(FormCurrency_EventEdit);
                this.EventDelete += new delegateEventDelete(FormCurrency_EventDelete);

                this.FieldLinkAdd("numCurrency", "currency", "", true, true);
                this.FieldLinkAdd("txtName", "name", "", true, false);
                this.FieldLinkAdd("txtName2", "name2", "", true, false);
                this.FieldLinkAdd("numRate", "rate", "", true, false);
                this.FieldLinkAdd("numCashBuyRate", "cashbuyrate", "", false, false);
                this.FieldLinkAdd("numCashSellRate", "cashsellrate", "", false, false);
                this.FieldLinkAdd("numNonCashBuyRate", "noncashbuyrate", "", false, false);
                this.FieldLinkAdd("numNonCashSellRate", "noncashsellrate", "", false, false);
                this.FieldLinkAdd("txtGLEquiv", "glequiv", "", true, false);
                this.FieldLinkAdd("numCurrencyCode", "CurrencyCode", "", true, false);

                this.FieldLinkAdd("cboGLExchProfit", "GLExchProfit", "", false, false, false);
                this.FieldLinkAdd("cboGLExchLoss", "GLExchLoss", "", false, false, false);
                this.FieldLinkAdd("cboGLRevProfit", "GLRevProfit", "", false, false, false);
                this.FieldLinkAdd("cboGLRevLoss", "GLRevLoss", "", false, false, false);
                this.FieldLinkAdd("txtOldRate", "OldRate", "", false, false, true);
                this.FieldLinkAdd("numOrderNo", "orderno", "", true, false);
                this.FieldLinkAdd("txtFractionName","FractionName","",true,false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Event]

        void FormCurrency_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (!FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
                return;
            }
            Result r;
            try
            {
                object[] FieldName = { "CURRENCY", "NAME", "NAME2", "RATE", "CASHBUYRATE", "CASHSELLRATE", "NONCASHBUYRATE", "NONCASHSELLRATE", "GLEQUIV", "CurrencyCode", "GLExchProfit", "GLExchLoss", "GLRevProfit", "GLRevLoss", "OldRate", "OrderNo", "fractionname","CreateDate" };
                if (!isnew)
                {
                    object[] NewValue = { numCurrency.EditValue, txtName.EditValue, txtName2.EditValue, Static.ToDecimal(numRate.EditValue), Static.ToDecimal(numCashBuyRate.EditValue), Static.ToDecimal(numCashSellRate.EditValue), Static.ToDecimal(numNonCashBuyRate.EditValue), Static.ToDecimal(numNonCashSellRate.EditValue),Static.ToStr(txtGLEquiv.EditValue), numCurrencyCode.EditValue, 
                                           cboGLExchProfit.EditValue, cboGLExchLoss.EditValue, cboGLRevProfit.EditValue, cboGLRevLoss.EditValue, txtOldRate.EditValue, numOrderNo.EditValue,txtFractionName.EditValue,_core.TxnDate};
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, EditTxnCode, EditTxnCode, new object[] { NewValue, OldValue, FieldName });
                    if (r.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай засварлалаа .");
                    }
                }
                else
                {
                    object[] NewValue ={numCurrency.EditValue, txtName.EditValue, txtName2.EditValue, Static.ToDecimal(numRate.EditValue), Static.ToDecimal(numCashBuyRate.EditValue), Static.ToDecimal(numCashSellRate.EditValue), Static.ToDecimal(numNonCashBuyRate.EditValue), Static.ToDecimal(numNonCashSellRate.EditValue), txtGLEquiv.EditValue, numCurrencyCode.EditValue,
                        Static.ToDecimal(cboGLExchProfit.EditValue), Static.ToDecimal(cboGLExchLoss.EditValue), Static.ToDecimal(cboGLRevProfit.EditValue), Static.ToDecimal(cboGLRevLoss.EditValue), Static.ToDecimal(txtOldRate.EditValue), numOrderNo.EditValue,txtFractionName.EditValue,_core.TxnDate};
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, AddTxnCode, AddTxnCode, new object[] { NewValue, FieldName });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай нэмлээ .");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void FormCurrency_EventEdit(ref bool cancel)
        {
            object[] Value = {numCurrency.EditValue, txtName.EditValue, txtName2.EditValue, Static.ToDecimal(numRate.EditValue), Static.ToDecimal(numCashBuyRate.EditValue), Static.ToDecimal(numCashSellRate.EditValue), Static.ToDecimal(numNonCashBuyRate.EditValue), Static.ToDecimal(numNonCashSellRate.EditValue),Static.ToStr(txtGLEquiv.EditValue), numCurrencyCode.EditValue,
                                 Static.ToDecimal(cboGLExchProfit.EditValue), Static.ToDecimal(cboGLExchLoss.EditValue), Static.ToDecimal(cboGLRevProfit.EditValue), Static.ToDecimal(cboGLRevLoss.EditValue), Static.ToDecimal(txtOldRate.EditValue), numOrderNo.EditValue,txtFractionName.EditValue,_core.TxnDate };
            OldValue = Value;
        }
        void FormCurrency_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, DeleteTxnCode, DeleteTxnCode, new object[] { numCurrency.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void FormCurrency_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Валютын дугаар");
            this.FieldLinkSetColumnCaption(1, "Валютын утга");
            this.FieldLinkSetColumnCaption(2, "Валютын тайлбар", true);
            this.FieldLinkSetColumnCaption(3, "Ханш", true);
            this.FieldLinkSetColumnCaption(4, "Бэлэн авах ханш", true);
            this.FieldLinkSetColumnCaption(5, "Бэлэн зарах ханш", true);
            this.FieldLinkSetColumnCaption(6, "Бэлэн бус авах ханш", true);
            this.FieldLinkSetColumnCaption(7, "Бэлэн бус зарах ханш", true);
            this.FieldLinkSetColumnCaption(8, "Эквивалент GL", true);
            this.FieldLinkSetColumnCaption(9, "", true);
            this.FieldLinkSetColumnCaption(10, "Арилжааны ашиг GL");
            this.FieldLinkSetColumnCaption(11, "Арилжааны алдагдал GL");
            this.FieldLinkSetColumnCaption(12, "Ханшийн тэгшитгэлийн ашиг GL");
            this.FieldLinkSetColumnCaption(13, "Ханшийн тэгшитгэлийн алдагдал GL");
            this.FieldLinkSetColumnCaption(14, "Өмнөх өдрийн ханш (Хуучин ханш)");
            this.FieldLinkSetColumnCaption(15, "Жагсаалтын эрэмбэ");
            this.FieldLinkSetColumnCaption(16, "Бутархайн нэр");
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridView1);
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }
        void FormCurrency_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, SelectTxnCode, SelectTxnCode, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
                else
                {
                    dt = r.Data.Tables[0];
                    switch (btn)
                    {
                        case 0: gridView1.FocusedRowHandle = rowhandle; break;
                        case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        public void Browse(string strFileName)
        {
            try
            {
                if (!File.Exists(strFileName))
                {
                    MessageBox.Show("Файл олдсонгүй!", "Файл татах", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                IWorkbook book = Factory.OpenWorkbook(strFileName);
                if (book != null)
                {
                    _DT = book.Worksheets[1].UsedRange.GetDataTable(false, true);
                    gridControl2.DataSource = _DT;
                    setData();
                }
                else
                    MessageBox.Show("Файл уншихад алдаа гарлаа");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Алдаа",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void setData()
        {
            gridView2.Columns[0].Caption = "ID";
            gridView2.Columns[0].OptionsColumn.AllowEdit = false;
            gridView2.Columns[1].Caption = "Огноо";
            gridView2.Columns[1].OptionsColumn.AllowEdit = false;
            gridView2.Columns[2].Caption = "Албан ханш";
            gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            gridView2.Columns[3].Caption = "Бэлэн авах ханш";
            gridView2.Columns[3].OptionsColumn.AllowEdit = false;
            gridView2.Columns[4].Caption = "Бэлэн зарах ханш";
            gridView2.Columns[4].OptionsColumn.AllowEdit = false;
            gridView2.Columns[5].Caption = "Бэлэн бус авах ханш";
            gridView2.Columns[5].OptionsColumn.AllowEdit = false;
            gridView2.Columns[6].Caption = "Бэлэн бус зарах ханш";
            gridView2.Columns[6].OptionsColumn.AllowEdit = false;
            gridView2.Columns[7].Caption = "Өмнөх өдрийн ханш";
            gridView2.Columns[7].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gridView2);
        }
        #region[BTN]
        private void btnbrowse_Click_1(object sender, EventArgs e)
        {
            OpenFile.Title = "Ханшийн файл сонгох";
            OpenFile.FileName = "";
            OpenFile.Filter = "Excel Worksheets|*.xls";
            OpenFile.ShowDialog();

            if (OpenFile.FileName != "")
            {
                txtFileName.Text = OpenFile.FileName;
                Browse(txtFileName.Text);
            }
        }
        private void btninsert_Click_1(object sender, EventArgs e)
        {
            DataTable _DT = (DataTable)gridControl2.DataSource;
            Result r = new Result();
            try
            {
                object[] obj = new object[8];
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        obj[0] = Static.ToStr(dr["column1"]);
                        obj[1] = Static.ToDate((dr["column2"]));
                        obj[2] = Static.ToDecimal(dr["column3"]);
                        obj[3] = Static.ToDecimal(dr["column4"]);
                        obj[4] = Static.ToDecimal(dr["column5"]);
                        obj[5] = Static.ToDecimal(dr["column6"]);
                        obj[6] = Static.ToDecimal(dr["column7"]);
                        obj[7] = Static.ToDecimal(dr["column8"]);
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140311, 140311,new object[]{obj});
                    }
                    if (r.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилтай нэмэгдлээ");
                    }
                    else
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[FormEvent]
        private void FormCurrency_Load(object sender, EventArgs e)
        {
            if (_core.Resource != null)
            {
                btnbrowse.Image = _core.Resource.GetImage("image_folder");
                btninsert.Image = _core.Resource.GetImage("navigate_save");
            }
            gridControl2.DataSource = _DT;

            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        private void FormCurrency_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}