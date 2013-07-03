using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

using ISM.Touch;
using EServ.Shared;

namespace InfoPos
{
    public partial class frmInfoPos : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Properties
        InfoPos.Core.Core _core = null;
        bool _directclose = false;
        #endregion
        #region Internal functions
        internal object CreateInstance(string dll, string cls, out int result)
        {
            Assembly asm = null;
            Type type = null;
            object obj = null;
            int ret = 0;
            try
            {
                #region Load DLL file
                try
                {
                    asm = Assembly.LoadFrom(Path.Combine(Static.WorkingFolder, dll));
                }
                catch
                {
                    ret = 1;
                    goto OnExit;
                }
                #endregion
                #region Create class instance
                try
                {
                    type = asm.GetType(cls, true, true);
                    obj = Activator.CreateInstance(type);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    ret = 2;
                    goto OnExit;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ret = 4;
                //throw ex;
            }
        OnExit:
            result = ret;
            return obj;
        }
        #endregion
        #region Constructor and Control events

        public frmInfoPos()
        {
            InitializeComponent();

            try
            {
                _core = Program.Core; // энэ класс дээр глобал бдлаар үүсгэсэн байгаа.
                _core.MainForm = this;

                #region Event declaration
                this.MdiChildActivate += new EventHandler(frmInfoPos_MdiChildActivate);
                this.touchButtonGroup1.EventKeyDown += new ISM.Touch.TouchButtonGroup.delegateKeyDown(touchButtonGroup1_EventKeyDown);
                _core.RemoteObject.DisConnected += new ISM.CUser.Remote.DelegateDisConnected(RemoteObject_DisConnected);
                #endregion
                #region Header Grid Options

                gridView1.OptionsCustomization.AllowRowSizing = false;

                gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsBehavior.ReadOnly = true;

                gridView1.OptionsView.ShowIndicator = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsView.ShowColumnHeaders = false;
                gridView1.OptionsView.ShowAutoFilterRow = false;
                gridView1.OptionsView.ColumnAutoWidth = true;
                gridView1.OptionsView.RowAutoHeight = true;
                gridView1.OptionsView.AllowCellMerge = false;
                //gridView1.OptionsView.ShowHorzLines = false;

                gridView1.OptionsSelection.MultiSelect = false;
                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                //this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

                if (gridControl1.Parent != null)
                {
                    //gridView1.Appearance.Empty.BackColor = System.Drawing.Color.Transparent;
                    gridView1.Appearance.Empty.BackColor = gridControl1.Parent.BackColor;
                    gridView1.Appearance.Empty.Options.UseBackColor = true;
                }
                //gridView1.Appearance.Row.BackColor = System.Drawing.Color.Transparent;
                //gridView1.Appearance.Row.Options.UseBackColor = true;

                #endregion
                
                InitAll();
                touchButtonGroup1.ParentMDI = this;
                TouchButtonsInit();
                touchButtonGroup1.Init(2, 8, 2);

                _core.Tag.EventOnCard += Tag_EventOnCard;
            }
            catch (Exception ex)
            {
            }
        }

        void EventOnCardThreadSafe(Core.TagEventData tagdata)
        {
            if (tagdata !=null && !string.IsNullOrEmpty(tagdata.readtagno))
            {
                if (tagdata.readstatus != 0 || tagdata.readtagno.Length < 6)
                {
                    _core.AlertShowSafe("Таг дутуу уншигдлаа!"
                        , string.Format("Сериал: {0} Төлөв: {1}\r\n{2}", tagdata.readtagno, tagdata.readstatus, tagdata.errormsg)
                        , 2);
                }
                else
                {
                    TouchTag touchtag = new TouchTag();
                    touchtag.Set("call_tagreader", tagdata.readtagno, "", tagdata);

                    bool cancel = false;
                    TouchButtonPress(null, null, touchtag, ref cancel);
                }
            }
        }
        void Tag_EventOnCard(Core.TagEventData tagdata)
        {
            if (this.InvokeRequired)
            {
                Core.Tag.delegateEventOnCard evt = new Core.Tag.delegateEventOnCard(EventOnCardThreadSafe);
                this.Invoke(evt, tagdata);
            }
            else
            {
                EventOnCardThreadSafe(tagdata);
            }
        }

        private void frmInfoPos_Load(object sender, EventArgs e)
        {
            picTitle.Image = Program.Core.Resource.GetBitmap("frontmenu_cashier");
            lblTitle.Text = _core.InstName;
        }
        private void frmInfoPos_FormClosing(object sender, FormClosingEventArgs e)
        {
            _directclose = true;
            if (_core != null && _core.RemoteObject != null)
            {
                if (_core.RemoteObject.IsConnected && MessageBox.Show("Програмаас гарахдаа итгэлтэй байна уу?", _core.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    _directclose = false;
                }
            }
        }
        private void frmInfoPos_MdiChildActivate(object sender, EventArgs e)
        {
            Form frm = this.ActiveMdiChild;
            if (frm == null)
            {
                touchButtonGroup1.ButtonsDrawChildren("ROOT");
                return;
            }
            //frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
        }

        private void RemoteObject_DisConnected()
        {
            try
            {
                if (_directclose) return;
                MessageBox.Show(this, "Серверээс холболт тасарлаа!", _core.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Windows.Forms.Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void touchButtonGroup1_EventKeyDown(Control sender, MouseEventArgs e, TouchLinkItem item, ref bool cancel)
        {
            TouchButtonPress(sender, e, item, ref cancel);
        }
        
        private void ribbon_ApplicationButtonClick(object sender, EventArgs e)
        {
            frmInfoPosSettings frm = new frmInfoPosSettings(_core);
            frm.ShowDialog();
        }

        private void barButtonAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowVersionDetail(_core);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        #endregion
        #region User functions

        private void InitAll()
        {
            #region [ Build param ]
            _core.InitAll();
            #endregion
            #region [ SetStatusBar ]
            SetStatusBar();
            #endregion
            _core.TempPath = _core.CacheGetStr("frmOption_TempPath", "");
        }

        private void SetStatusBar()
        {

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append(_core.InstCode);
            sb.Append("-");
            sb.Append(_core.InstName);
            InstName.Caption = sb.ToString();
            InstName.Glyph = _core.Resource.GetImage("hpro_home");

            sb.Clear();
            sb.Append(_core.RemoteObject.User.UserNo);
            sb.Append("-");
            if (_core.RemoteObject.User.UserFName.Length > 1)
            {
                sb.Append(_core.RemoteObject.User.UserFName.Substring(0, 1));
                sb.Append(".");
            }
            sb.Append(_core.RemoteObject.User.UserLName);
            sb.Append("-");
            sb.Append(_core.RemoteObject.User.BranchCode);

            UserBranch.Caption = sb.ToString();
            UserBranch.Glyph = _core.Resource.GetImage("hpro_user");

            SystemDate.Caption = _core.TxnDate.ToShortDateString();
            SystemDate.Glyph = _core.Resource.GetImage("hpro_systemdate");
            //SystemDate.im
            ServerPort.Caption = _core.RemoteObject.ServerIP + ":" + _core.RemoteObject.ServerPort;
            ServerPort.Glyph = _core.Resource.GetImage("hpro_appserver");

            sbarTagReader.Caption = string.Format("Таг уншигч: {0}", _core.Tag.IsOpenned ? "OK" : "NONE");

            Version.Caption = "Хувилбар " + Application.ProductVersion;
        }
        public void SetStatusBarStatus(string status)
        {
            try
            {
                //Status.Caption = status;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void TouchButtonsInit()
        {
            #region Main menu

            touchButtonGroup1.Add("", "fo_reg", 1, 1, "Бүртгэл", "", Program.Core.Resource.GetBitmap("frontmenu_register"), "infopos.fo_reg.dll", "infopos.reg.frmReg");
            touchButtonGroup1.Add("", "master", 2, 1, "Мастерийн бүртгэл", "", Program.Core.Resource.GetBitmap("frontmenu_master"), "infopos.fo_sales.dll", "infopos.sales.frmMaster");

            touchButtonGroup1.Add("", "fo_sales", 1, 2, "Борлуулалт", "", Program.Core.Resource.GetBitmap("frontmenu_sales_add"), "infopos.fo_sales.dll", "infopos.sales.frmSales");
            touchButtonGroup1.Add("", "fo_sales_edit", 2, 2, "Борлуулалт Лавлах", "", Program.Core.Resource.GetBitmap("frontmenu_sales_remove"), "infopos.fo_sales.dll", "infopos.sales.frmSalesEdit");

            touchButtonGroup1.Add("", "rent", 1, 3, "Түрээс", "", Program.Core.Resource.GetBitmap("frontmenu_rent"), "infopos.fo_rent.dll", "infopos.rent.frmRent");
            touchButtonGroup1.Add("", "tag", 2, 3, "Таг", "", Program.Core.Resource.GetBitmap("frontmenu_tag"), "infopos.fo_tag.dll", "infopos.fo_tag.frmTag");

            touchButtonGroup1.Add("", "fo_order_new", 1, 4, "Захиалга", "", Program.Core.Resource.GetBitmap("frontmenu_order"), "infopos.fo_order.dll", "infopos.order.frmOrderNew");
            touchButtonGroup1.Add("", "fo_order_edit", 2, 4, "Захиалга Засах", "", Program.Core.Resource.GetBitmap("frontmenu_order_edit"), "infopos.fo_order.dll", "infopos.order.frmOrderEdit");

            touchButtonGroup1.Add("", "fo_schd", 1, 5, "Үйлчилгээний Хуваарь", "", Program.Core.Resource.GetBitmap("frontmenu_schedule"), "infopos.fo_schd.dll", "infopos.schd.frmSchedule");
            touchButtonGroup1.Add("", "report", 2, 5, "Лавлагаа", "", Program.Core.Resource.GetBitmap("frontmenu_scroll_view"), "infopos.fo_sales.dll", "infopos.sales.frmSalesReport");

            touchButtonGroup1.Add("", "fo_shift", 1, 6, "Ээлж", "", Program.Core.Resource.GetBitmap("frontmenu_cashier"), "infopos.fo_pos.dll", "infopos.pos.frmCash");

            touchButtonGroup1.Add("", "fo_cash_survey", 1, 8, "Санал Асуулга", "", Program.Core.Resource.GetBitmap("frontmenu_FAQ"), "infopos.fo_pos.dll", "infopos.pos.frmSurvey");
            touchButtonGroup1.Add("", "exit", 2, 8, "Гарах!", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));

            #endregion
            #region Reg sub menu

            touchButtonGroup1.Add("fo_reg", "fo_reg_search", 1, 1, "Хайлт", "", Program.Core.Resource.GetBitmap("frontmenu_passport_search"));
            touchButtonGroup1.Add("fo_reg", "fo_reg_edit", 2, 1, "Барьцаа засах", "", Program.Core.Resource.GetBitmap("frontmenu_FAQ"));

            //touchButtonGroup1.Add("fo_reg", "fo_reg_searchtag", 2, 1, "Тагаар хайх", "", Program.Core.Resource.GetBitmap("frontmenu_tag_search"));

            touchButtonGroup1.Add("fo_reg", "fo_reg_taglinkset", 1, 2, "Таг холбох", "", Program.Core.Resource.GetBitmap("frontmenu_tag_add"));
            touchButtonGroup1.Add("fo_reg", "fo_reg_taglinkdel", 2, 2, "Таг салгах", "", Program.Core.Resource.GetBitmap("frontmenu_tag_remove"));

            touchButtonGroup1.Add("fo_reg", "fo_reg_custadd", 1, 3, "Харилцагч холбох", "", Program.Core.Resource.GetBitmap("frontmenu_customer_add"));
            touchButtonGroup1.Add("fo_reg", "fo_reg_custdel", 2, 3, "Харилцагч салгах", "", Program.Core.Resource.GetBitmap("frontmenu_customer_remove"));

            touchButtonGroup1.Add("fo_reg", "fo_reg_add", 1, 5, "Барьцаа бүртгэх", "", Program.Core.Resource.GetBitmap("frontmenu_passport_add"));
            touchButtonGroup1.Add("fo_reg", "fo_reg_del", 2, 5, "Барьцаа чөлөөлөх", "", Program.Core.Resource.GetBitmap("frontmenu_passport_remove"));

            touchButtonGroup1.Add("fo_reg", "fo_reg_fine", 1, 6, "Торгууль", "", Program.Core.Resource.GetBitmap("frontmenu_pay"));

            touchButtonGroup1.Add("fo_reg", "fo_reg_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));

            #endregion
            #region Payment sub menu

            touchButtonGroup1.Add("fo_payment", "fo_payment_search", 1, 1, "Борлуулалт хайх", "", Program.Core.Resource.GetBitmap("frontmenu_sales_search"));
            touchButtonGroup1.Add("fo_payment", "fo_payment_tag", 2, 1, "Таг уншуулах", "", Program.Core.Resource.GetBitmap("frontmenu_tag_read"));
            touchButtonGroup1.Add("fo_payment", "fo_payment_set", 1, 2, "Төлбөр хийх", "", Program.Core.Resource.GetBitmap("frontmenu_pay"));
            touchButtonGroup1.Add("fo_payment", "fo_payment_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));

            #endregion
            #region Rental sub menu
            touchButtonGroup1.Add("rent", "rent_search", 1, 1, "Түрээс хайх", "", Program.Core.Resource.GetBitmap("frontmenu_rent_search"));
            touchButtonGroup1.Add("rent", "rent_barcode", 2, 1, "Баркод хайх", "", Program.Core.Resource.GetBitmap("frontmenu_rent_barcode"));

            touchButtonGroup1.Add("rent", "rent_edit", 1, 2, "Мэдээлэл оруулах", "", Program.Core.Resource.GetBitmap("frontmenu_rent_check"));
            touchButtonGroup1.Add("rent", "rent_cust", 2, 2, "Харилцагч үүсгэх", "", Program.Core.Resource.GetBitmap("frontmenu_customer_add"));

            touchButtonGroup1.Add("rent", "rent_deliver", 1, 4, "Бараа олгох", "", Program.Core.Resource.GetBitmap("frontmenu_rent_deliver"));
            touchButtonGroup1.Add("rent", "rent_receive", 1, 5, "Бараа хүлээн авах", "", Program.Core.Resource.GetBitmap("frontmenu_rent_receive"));

            touchButtonGroup1.Add("rent", "rent_tagdel", 2, 4, "Таг хасах", "", Program.Core.Resource.GetBitmap("frontmenu_tag_remove"));
            touchButtonGroup1.Add("rent", "rent_taghist", 2, 5, "Тагын түүх", "", Program.Core.Resource.GetBitmap("frontmenu_tag_search"));
            
            touchButtonGroup1.Add("rent", "rent_clear", 1, 8, "Цэвэрлэх", "", Program.Core.Resource.GetBitmap("frontmenu_recycle"));
            touchButtonGroup1.Add("rent", "rent_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));

            #endregion
            #region Master sub menu

            touchButtonGroup1.Add("master", "master_search", 1, 1, "Хайлт", "", Program.Core.Resource.GetBitmap("frontmenu_rent_search"));
            touchButtonGroup1.Add("master", "master_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));

            #endregion
            #region Sales sub menu

            touchButtonGroup1.Add("fo_sales", "fo_sales_pledge", 1, 1, "Барьцаа", "", Program.Core.Resource.GetBitmap("frontmenu_passport_search"));
            touchButtonGroup1.Add("fo_sales", "fo_sales_contract", 2, 1, "Гэрээ", "", Program.Core.Resource.GetBitmap("frontmenu_contract"));

            touchButtonGroup1.Add("fo_sales", "fo_sales_custadd", 1, 2, "Үйлчлүүлэгч нэмэх", "", Program.Core.Resource.GetBitmap("frontmenu_customer_add"));
            touchButtonGroup1.Add("fo_sales", "fo_sales_custdel", 2, 2, "Үйлчлүүлэгч хасах", "", Program.Core.Resource.GetBitmap("frontmenu_customer_remove"));

            touchButtonGroup1.Add("fo_sales", "fo_sales_order", 1, 3, "Захиалга", "", Program.Core.Resource.GetBitmap("frontmenu_order"));

            touchButtonGroup1.Add("fo_sales", "fo_sales_move", 1, 4, "Шилжүүлэх", "", Program.Core.Resource.GetBitmap("frontmenu_sales_exchange"));
            touchButtonGroup1.Add("fo_sales", "fo_sales_proddel", 2, 4, "Бараа хасах", "", Program.Core.Resource.GetBitmap("frontmenu_sales_proddel"));
            
            touchButtonGroup1.Add("fo_sales", "fo_sales_unpack", 1, 5, "Багц задлах", "", Program.Core.Resource.GetBitmap("frontmenu_sales_unpack"));

            touchButtonGroup1.Add("fo_sales", "fo_sales_payment", 1, 6, "Төлөх", "", Program.Core.Resource.GetBitmap("frontmenu_pay"));
            touchButtonGroup1.Add("fo_sales", "fo_sales_discount", 2, 6, "Хөнгөлөлт", "", Program.Core.Resource.GetBitmap("frontmenu_pay"));

            touchButtonGroup1.Add("fo_sales", "fo_sales_clear", 1, 8, "Цэвэрлэх", "", Program.Core.Resource.GetBitmap("frontmenu_recycle"));
            touchButtonGroup1.Add("fo_sales", "fo_sales_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));



            //touchButtonGroup1.Add("fo_cash_new", "fo_cash_new_1.1", 1, 1, "Сонголт", "", Program.Core.Resource.GetBitmap("frontmenu_sales_choose"));
            //touchButtonGroup1.Add("fo_cash_new", "fo_cash_new_2.1", 2, 1, "Харилцагч сонгох", "", Program.Core.Resource.GetBitmap("frontmenu_sales_cust_search"));
            //touchButtonGroup1.Add("fo_cash_new", "fo_cash_new_1.2", 1, 2, "Багц сонгох ", "", Program.Core.Resource.GetBitmap("frontmenu_batch_choose"));
            //touchButtonGroup1.Add("fo_cash_new", "fo_cash_new_2.2", 2, 2, "Бараа үйлчилгээ сонгох ", "", Program.Core.Resource.GetBitmap("frontmenu_product_choose"));
            //touchButtonGroup1.Add("fo_cash_new", "fo_cash_new_1.3", 1, 3, "Төлөх ", "", Program.Core.Resource.GetBitmap("frontmenu_pay"));
            //touchButtonGroup1.Add("fo_cash_new", "fo_cash_new_2.8", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));

            #endregion
            #region Sales edit sub menu
            touchButtonGroup1.Add("fo_sales_edit", "fo_sales_edit_search", 1, 1, "Хайх", "", Program.Core.Resource.GetBitmap("frontmenu_search"));
            touchButtonGroup1.Add("fo_sales_edit", "fo_sales_edit_bill", 2, 1, "Билл", "", Program.Core.Resource.GetBitmap("frontmenu_bill"));

            touchButtonGroup1.Add("fo_sales_edit", "fo_sales_edit_payment", 1, 2, "Төлбөр ", "", Program.Core.Resource.GetBitmap("frontmenu_pay"));
            touchButtonGroup1.Add("fo_sales_edit", "fo_sales_edit_move", 2, 2, "Таг бичих", "", Program.Core.Resource.GetBitmap("frontmenu_tag"));

            touchButtonGroup1.Add("fo_sales_edit", "fo_sales_edit_refund1", 1, 4, "Борлуулалт буцаах", "", Program.Core.Resource.GetBitmap("frontmenu_refund"));
            touchButtonGroup1.Add("fo_sales_edit", "fo_sales_edit_refund2", 2, 4, "Бараа буцаах", "", Program.Core.Resource.GetBitmap("frontmenu_refund"));
            touchButtonGroup1.Add("fo_sales_edit", "fo_sales_edit_refund3", 1, 5, "Төлбөр буцаах", "", Program.Core.Resource.GetBitmap("frontmenu_refund"));

            touchButtonGroup1.Add("fo_sales_edit", "fo_sales_edit_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));
            #endregion

            #region Pos sub menu

            touchButtonGroup1.Add("fo_shift", "fo_shift_supply", 1, 1, "Кассын зузаатгал", "", Program.Core.Resource.GetBitmap("frontmenu_income"));
            touchButtonGroup1.Add("fo_shift", "fo_shift_draw", 2, 1, "Касс тушаах", "", Program.Core.Resource.GetBitmap("frontmenu_refund"));
            touchButtonGroup1.Add("fo_shift", "fo_shift_open", 1, 3, "Ээлж нээх", "", Program.Core.Resource.GetBitmap("frontmenu_shiftopen"));
            touchButtonGroup1.Add("fo_shift", "fo_shift_close", 2, 3, "Ээлж хаах", "", Program.Core.Resource.GetBitmap("frontmenu_shiftclose"));
            
            touchButtonGroup1.Add("fo_shift", "fo_pos_open", 1, 4, "ПОС нээх", "", Program.Core.Resource.GetBitmap("frontmenu_posopen"));
            touchButtonGroup1.Add("fo_shift", "fo_pos_close", 2, 4, "ПОС хаах", "", Program.Core.Resource.GetBitmap("frontmenu_posclose"));

            touchButtonGroup1.Add("fo_shift", "fo_pos_repbrief", 1, 6, "Борл.Товч", "", Program.Core.Resource.GetBitmap("frontmenu_scroll_view"));

            touchButtonGroup1.Add("fo_shift", "fo_shift_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));

            #endregion

            #region Report sub menu

            touchButtonGroup1.Add("report", "report_sales", 1, 1, "Борл. дэлгэрэнгүй", "", Program.Core.Resource.GetBitmap("frontmenu_FAQ"));
            touchButtonGroup1.Add("report", "report_payments", 2, 1, "Төлбөр дэлгэрэнгүй", "", Program.Core.Resource.GetBitmap("frontmenu_FAQ"));
            touchButtonGroup1.Add("report", "report_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));

            #endregion


            #region Tag sub menu
            touchButtonGroup1.Add("tag", "tag_clear", 1, 1, "Таг цэвэрлэх", "", Program.Core.Resource.GetBitmap("frontmenu_tag_remove"));
            touchButtonGroup1.Add("tag", "tag_write", 1, 2, "Таг дээр мэдээлэл бичих", "", Program.Core.Resource.GetBitmap("frontmenu_tag_add"));
            touchButtonGroup1.Add("tag", "tag_recycle", 1, 8, "Дэлгэц цэвэрлэх", "", Program.Core.Resource.GetBitmap("frontmenu_recycle"));
            touchButtonGroup1.Add("tag", "tag_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_exit"));
            #endregion
            #region Cash sub menu

            touchButtonGroup1.Add("fo_cash_open", "fo_cash_open_set", 1, 1, "Касс нээх", "", Program.Core.Resource.GetBitmap("frontmenu_cash_open"));
            touchButtonGroup1.Add("fo_cash_open", "fo_cash_open_incr", 2, 1, "Касс зузаатгал", "", Program.Core.Resource.GetBitmap("frontmenu_cash_add"));
            touchButtonGroup1.Add("fo_cash_open", "fo_cash_open_decr", 2, 2, "Касс тушаах", "", Program.Core.Resource.GetBitmap("frontmenu_cash_back"));
            touchButtonGroup1.Add("fo_cash_open", "fo_cash_open_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));

            touchButtonGroup1.Add("fo_cash_close", "fo_cash_close_set", 1, 1, "Касс хаах", "", Program.Core.Resource.GetBitmap("frontmenu_cash_close"));
            touchButtonGroup1.Add("fo_cash_close", "fo_cash_close_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));

            touchButtonGroup1.Add("fo_cash_pos", "fo_cash_pos_set", 1, 1, "ПОС хаах", "", Program.Core.Resource.GetBitmap("frontmenu_POS_close"));
            touchButtonGroup1.Add("fo_cash_pos", "fo_cash_pos_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));

            #endregion
            #region Order sub menu

            touchButtonGroup1.Add("fo_order_new", "fo_customer_search", 1, 1, "Хайх", "", Program.Core.Resource.GetBitmap("frontmenu_order_search"));
            touchButtonGroup1.Add("fo_order_new", "fo_order_ordernew", 1, 2, "Захиалга бүртгэх", "", Program.Core.Resource.GetBitmap("frontmenu_order"));
            touchButtonGroup1.Add("fo_order_new", "fo_order_personnew", 2, 1, "Хамрагдах үйлчлүүлэгчид", "", Program.Core.Resource.GetBitmap("frontmenu_order_cust"));
            touchButtonGroup1.Add("fo_order_new", "fo_order_groupnew", 2, 2, "Багц үйлчилгээний бүлэг ", "", Program.Core.Resource.GetBitmap("frontmenu_order_batch"));
            touchButtonGroup1.Add("fo_order_new", "fo_order_new_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));

            touchButtonGroup1.Add("fo_order_edit", "fo_order_search", 1, 1, "Хайх", "", Program.Core.Resource.GetBitmap("frontmenu_order_search"));
            touchButtonGroup1.Add("fo_order_edit", "fo_order_orderedit", 1, 2, "Захиалга засварлах", "", Program.Core.Resource.GetBitmap("frontmenu_order"));
            touchButtonGroup1.Add("fo_order_edit", "fo_order_personedit", 2, 1, "Хамрагдах үйлчлүүлэгчид", "", Program.Core.Resource.GetBitmap("frontmenu_order_cust"));
            touchButtonGroup1.Add("fo_order_edit", "fo_order_groupedit", 2, 2, "Багц үйлчилгээний бүлэг", "", Program.Core.Resource.GetBitmap("frontmenu_order_batch"));
            touchButtonGroup1.Add("fo_order_edit", "fo_order_cancel", 1, 3, "Захиалга цуцлах", "", Program.Core.Resource.GetBitmap("frontmenu_order_cancel"));
            touchButtonGroup1.Add("fo_order_edit", "fo_order_confirm", 2, 3, "Захиалга баталгаажуулах", "", Program.Core.Resource.GetBitmap("frontmenu_order_confirm"));
            touchButtonGroup1.Add("fo_order_edit", "fo_order_expend", 1, 4, "Захиалга сунгах", "", Program.Core.Resource.GetBitmap("frontmenu_order_expend"));
            touchButtonGroup1.Add("fo_order_edit", "fo_order_edit_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));


            #endregion
            #region[Bill sub menu]
            touchButtonGroup1.Add("fo_cash_bill", "fo_repeat_bill", 1, 1, "Билл хэвлэх", "", Program.Core.Resource.GetBitmap("frontmenu_bill"));
            touchButtonGroup1.Add("fo_cash_bill", "fo_cash_bill_exit", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));
            #endregion
            #region[FeedBack]
            //touchButtonGroup1.Add("fo_cash_survey", "fo_cash_survey_1.1", 1, 1, "Хадгалах", "", Program.Core.Resource.GetBitmap("frontmenu_FAQ"));
            //touchButtonGroup1.Add("fo_cash_survey", "fo_cash_survey_2.8", 2, 8, "Гарах", "", Program.Core.Resource.GetBitmap("frontmenu_FAQ"));
            touchButtonGroup1.Add("fo_cashreg", "fo_cashreg_1.1", 1, 1, "Сонгох", "", Program.Core.Resource.GetBitmap("frontmenu_FAQ"));
            touchButtonGroup1.Add("fo_cashreg", "fo_cashreg_2.8", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_FAQ"));
            #endregion
            #region[CASHDETAILLIST]
            touchButtonGroup1.Add("fo_salesdetailsearch", "fo_salesdetailsearch_1.1", 1, 1, "Хайх", "", Program.Core.Resource.GetBitmap("frontmenu_passport_search"));
            touchButtonGroup1.Add("fo_salesdetailsearch", "fo_salesdetailsearch_2.8", 2, 8, "Хаах", "", Program.Core.Resource.GetBitmap("frontmenu_back"));
            #endregion
        }
        private void TouchButtonPress(Control sender, MouseEventArgs e, TouchLinkItem item, ref bool cancel)
        {
            try
            {
                if (item == null) return;

                #region Гарах товч дарагдсан эсэхийг шалгах

                if (item.Key == "exit")
                {
                    //string confirm = "Та програмаас гарахдаа итгэлтэй байна уу?";
                    //if (ISM.Template.FormUtility.ValidateConfirm(confirm)) 
                        this.Close();
                    return;
                }

                #endregion
                #region Invoke init function

                if (item != null && !string.IsNullOrEmpty(item.Dll) && !string.IsNullOrEmpty(item.Cls))
                {
                    int result = 0;
                    object obj = CreateInstance(item.Dll, item.Cls, out result);
                    if (result == 0 && obj is ITouchCall)
                    {
                        ITouchCall touch = (ITouchCall)obj;
                        touch.Init(item.Key, item, this, Program.Core, ref cancel);
                    }
                    else
                    {
                        cancel = true;
                        string s = string.Format("Модуль дуудахад алдаа гарлаа.\n  Алдааны дугаар\t\t= {0}\n  Түлхүүр нэр\t\t= {1}\n  Модулийн нэр\t\t= {2}\n  Классын нэр\t\t= {3}", result, item.Key, item.Dll, item.Cls);
                        MessageBox.Show(s, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                #endregion
                #region Invoke call function

                if (item != null && !cancel)
                {

                    object obj = this.ActiveMdiChild;
                    if (obj != null && obj is ITouchCall)
                    {
                        ITouchCall touch = (ITouchCall)obj;
                        ((Form)touch).Text = "";

                        if (item.parentKey == "ROOT")
                        {
                            picTitle.Image = item.Bitmap;
                            lblTitle.Text = item.Text;
                        }

                        touch.Call(item.Key, item, ref cancel);
                        if (item.IsClose == 1)
                        {
                            cancel = true;
                            touchButtonGroup1.ButtonsDrawChildren("ROOT");

                            picTitle.Image = Program.Core.Resource.GetBitmap("frontmenu_cashier");
                            lblTitle.Text = _core.InstName;
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                cancel = true;
                MessageBox.Show(ex.Message, "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        public void SetHeaderValue(int rowindex, string caption, string value)
        {
            DataTable dt = null;
            #region Хэрэв тэйбэл үүсээгүй бол шинээр үүсгэж холбох
            if (gridControl1.DataSource == null)
            {
                dt = new DataTable();
                dt.Columns.Add("KEY", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                gridControl1.DataSource = dt;

            }
            #endregion

            #region Мөрийн тоо нь RowIndex ээс бага бол дүүртэл нь мөр нэмэх
            dt = (DataTable)gridControl1.DataSource;
            while (dt.Rows.Count <= rowindex) dt.Rows.Add("", "");
            #endregion

            if (caption != null) dt.Rows[rowindex][0] = caption;
            dt.Rows[rowindex][1] = value;
        }
        public string GetHeaderValue(int rowindex)
        {
            string s = null;

            if (gridControl1.DataSource != null)
            {
                DataTable dt = (DataTable)gridControl1.DataSource;
                if (dt.Rows.Count > rowindex)
                {
                    s = Convert.ToString(dt.Rows[rowindex]);
                }
            }

            return s;
        }
        public void ClearHeaderValue()
        {
            try
            {
                for (int r = 0; r < gridView1.RowCount; r++)
                    gridView1.DeleteRow(r);
            }
            catch
            { }
        }

        public DataRow NewRow(DataTable t, string id, int count)
        {
            DataRow r = null;
            try
            {
                r = t.Rows.Add(id, string.Format("{0} ({1})", id, count));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return r;
        }

        public Result InitCashier()
        {
            Result res = null;
            try
            {

            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

            OnExit:
            return res;
        }

        #endregion
    }

    /// <summary>
    /// For Tag Simulator only!
    /// </summary>
    public class TouchTag : TouchLinkItem
    {
        public void Set(string key, string text, string desc)
        {
            base.key = key;
            base.text = text;
            base.description = desc;
        }
        public void Set(string key, string text, string desc, object tag)
        {
            base.key = key;
            base.text = text;
            base.description = desc;
            base.Tag = tag;
        }
    };
}
