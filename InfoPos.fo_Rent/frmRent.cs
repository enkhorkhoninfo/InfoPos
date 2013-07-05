using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EServ.Shared;
using ISM.Touch;
using InfoPos.Core;

namespace InfoPos.Rent
{
    public partial class frmRent : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        #region Internal events

        public event Core.Tag.delegateEventOnCard EventOnCard;

        #endregion
        #region Variables
        TouchKeyboard _kb = null;

        DataTable _dtTag = null;
        ArrayList _dict = new ArrayList();

        /**********************************
         * Хамгийн сүүлийн идэвхтэй хэрэглэгчийн
         * контролыг хадгална. Өөр хэрэглэгч логин
         * хийх үед хуучин хэрэглэгчээ лог-офф 
         * болгоход ашиглана.
         **********************************/
        Control _control = null; //current user login control

        /**********************************
         * Нэвтэрсэн хэрэглэгчдийн контролыг
         * хадгалах. Грид дээр өнгөөр ялгаж
         * харуулахын тулд үүнийг ашиглана.
         **********************************/
        Hashtable _users = new Hashtable();

        string _custname= null;
        int _custheight=0;
        int _custfoot=0;
        int _custsex=0;
        int _custmember=0;
        string _custrate=null;
        string _custphone=null;

        #endregion
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
                try
                {
                    if (value != null)
                    {
                        _core = value;
                        if (_remote == null) _remote = _core.RemoteObject;
                        if (_resource == null) _resource = _core.Resource;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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

        public DataTable Data
        {
            get { return (DataTable)gridControl1.DataSource; }
        }
        private string _layoutfilename = "";
        public string LayoutFileName
        {
            get { return _layoutfilename; }
        }

        private string _layoutfilenametaglist = "";
        public string LayoutFileNameTagList
        {
            get { return _layoutfilenametaglist; }
        }

        private string _salesno = null;
        public string SalesNo
        {
            get { return _salesno; }
            set { _salesno = value; }
        }

        private string _custno = null;
        public string CustNo
        {
            get { return _custno; }
            set { _custno = value; }
        }

        private string _serialno = null;
        public string SerialNo
        {
            get { return _serialno; }
            set { _serialno = value; }
        }

        private int _userno = 0;
        public int Userno
        {
            get { return _userno; }
            set { _userno = value; }
        }

        private int _userstate = 0;
        public int UserState
        {
            get { return _userstate; }
            set { _userstate = value; }
        }

        #endregion
        #region Menu functions
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                if (_core != null)
                {
                    _remote = _core.RemoteObject;
                    _resource = _core.Resource;
                }



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
                    case "rent_search":
                        SubMenu_Search();
                        break;
                    case "rent_barcode":
                        SubMenu_Barcode();
                        break;

                    case "rent_tag":
                        //SubMenu_SearchTag();
                        break;
                    case "rent_tagdel":
                        SubMenu_TagDel();
                        break;
                    case "rent_taghist":
                        SubMenu_TagHist();
                        break;
                    case "rent_deliver":
                        SubMenu_Deliver();
                        break;
                    case "rent_receive":
                        SubMenu_Receive();
                        break;
                    case "rent_edit":
                        SubMenu_Edit();
                        break;
                    case "rent_cust":
                        SubMenu_CreateCustomer();
                        break;
                    case "rent_clear":
                        SubMenu_Clear();
                        break;
                    case "rent_exit":
                        item.IsClose = 1;
                        this.Close();
                        break;
                    case "call_tagreader":
                        string tagno = item.Text;
                        InfoPos.Core.TagEventData tagdata = (InfoPos.Core.TagEventData)item.Tag;
                        if (EventOnCard != null)
                        {
                            EventOnCard(tagdata);
                        }
                        else
                        {
                            SubMenu_TagReader(tagno);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SubMenu_Search()
        {
            Result res = null;
            try
            {
                frmRentSearch frm = new frmRentSearch(_core);
                DialogResult dr = frm.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    if (frm.CurrentRow != null)
                    {
                        _salesno = Static.ToStr(frm.CurrentRow["SALESNO"]);
                        _custno = Static.ToStr(frm.CurrentRow["CUSTNO"]);
                        _custname = Static.ToStr(frm.CurrentRow["CUSTNAME"]);
                        _serialno = Static.ToStr(frm.CurrentRow["SERIALNO"]);

                        frm.Dispose();

                        res = TagReaderGet(_serialno);
                    }
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        private void SubMenu_Barcode()
        {
            #region Сонгогдсон хэрэгслийн код нэр авах
            DataRow r = gridView1.GetFocusedDataRow();
            if (r == null)
            {
                MessageBox.Show("Түрээсийн хэрэгсэл сонгогдоогүй байна.");
                return;
            }

            string prodno = Static.ToStr(r["PRODNO"]);
            string prodname = Static.ToStr(r["PRODNAME"]);
            #endregion
            #region Баркод хайх дэлгэц дуудах
            frmBarcode frm = new frmBarcode();
            frm.TableSeries = (DataTable)_dict[3];
            frm.TableInv = (DataTable)_dict[4];
            frm.InvId = prodno;
            frm.InvName = prodname;

            frm.StartPosition = FormStartPosition.CenterScreen;
            DialogResult dlg = frm.ShowDialog();
            if (dlg == System.Windows.Forms.DialogResult.OK)
            {
                txtBarCode.Text = frm.BarCode;
            }
            #endregion
        }
        private void SubMenu_TagReader(string serialno)
        {
            _serialno = serialno;
            Result res = TagReaderGet(serialno);

            Alert(res, "Таг уншуулах");
            //ISM.Template.FormUtility.ValidateQuery(res);
        }
        private void SubMenu_TagDel()
        {
            Result res = null;

            DataRow r = gridView2.GetFocusedDataRow();
            if (r == null)
            {
                res = new Result(6060011, "Таг сонгогдоогүй байна.");
                goto OnExit;
            }
            string serialno = Static.ToStr(r["SERIALNO"]);
            string confirm = string.Format("[{0}] дугаартай тагыг жагсаалтаас хасах уу?", serialno);
            if (ISM.Template.FormUtility.ValidateConfirm(confirm))
            {
                _core.MainForm_HeaderSet(0, null, "");
                _core.MainForm_HeaderSet(1, null, "");
                _core.MainForm_HeaderSet(2, null, "");
                _core.MainForm_HeaderSet(3, null, "");
                
                _serialno = "";
                _custno ="";
                _custname = "";
                _salesno = "";
                
                r.Delete();
                r.Table.AcceptChanges();
                
                gridControl1.DataSource = null;
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        private void SubMenu_TagHist()
        {
        }
        private void SubMenu_Clear()
        {
            string confirm = string.Format("Дэлгэцийн мэдээллийг цэвэрлэхдээ итгэлтэй байна уу?");
            if (ISM.Template.FormUtility.ValidateConfirm(confirm))
            {
                _dtTag.Clear();
                _dtTag.AcceptChanges();

                gridControl1.DataSource = null;

                _core.MainForm_HeaderSet(0, null, "");
                _core.MainForm_HeaderSet(1, null, "");
                _core.MainForm_HeaderSet(2, null, "");
                _core.MainForm_HeaderSet(3, null, "");
            }
        }
        private void SubMenu_Deliver()
        {
            #region Validation

            if (_dict == null || _dict.Count < 5)
            {
                MessageBox.Show("Жагсаалт мэдээллүүд бүрэн татагдаагүй байна!");
                return;
            }

            DataRow r = gridView1.GetFocusedDataRow();
            if (r == null)
            {
                MessageBox.Show("Түрээсийн хэрэгсэл сонгогдоогүй байна.");
                return;
            }

            int rentstatus = Static.ToInt(r["RENTSTATUS"]);
            int overtime  = Static.ToInt(r["DURATION"]);
            if (rentstatus == 1)
            {
                MessageBox.Show("Түрээсийн хэрэгсэл олгогдсон байна.");
                return;
            }
            if (rentstatus == 2 && overtime > 0)
            {
                MessageBox.Show("Торгууль төлөгдөөгүй байна.");
                return;
            }
                        
            string barcode = Static.ToStr(txtBarCode.EditValue);
            
            // Баркодгүйгээр олгодог гэсэн тул хаав. 2013.04.05
            //if (string.IsNullOrEmpty(barcode))
            //{
            //    MessageBox.Show("Түрээсийн хэрэгслийн баркодыг оруулна уу.");
            //    return;
            //}

            #endregion

            _salesno = Static.ToStr(r["SALESNO"]);
            int itemno = Static.ToInt(r["ITEMNO"]);
            decimal custno = Static.ToDecimal(r["CUSTNO"]);
            string prodno = Static.ToStr(r["PRODNO"]);

            Result res = Deliver(_salesno, custno, itemno, prodno, barcode, _userno, _userstate);
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        private void SubMenu_Receive()
        {
            #region Validation

            if (_dict == null || _dict.Count < 5)
            {
                MessageBox.Show("Жагсаалт мэдээллүүд бүрэн татагдаагүй байна!");
                return;
            }

            DataRow r = gridView1.GetFocusedDataRow();
            if (r == null)
            {
                MessageBox.Show("Түрээсийн хэрэгсэл сонгогдоогүй байна.");
                return;
            }

            int rentstatus = Static.ToInt(r["RENTSTATUS"]);
            if (rentstatus != 1)
            {
                MessageBox.Show("Түрээсийн хэрэгслийг хүлээн авах боломжгүй.");
                return;
            }

            string barcode = Static.ToStr(txtBarCode.EditValue);

            // Баркодгүйгээр олгож хүлээн авдаг гэсэн тул хаав. 2013.04.05

            //string barcode = Static.ToStr(txtBarCode.EditValue);
            //if (string.IsNullOrEmpty(barcode))
            //{
            //    MessageBox.Show("Түрээсийн хэрэгслийн баркодыг оруулна уу.");
            //    return;
            //}

            //DataTable dt = (DataTable)_dict[3];
            //DataRow[] rows = dt.Select(string.Format("BARCODE='{0}'", barcode));
            //if (rows == null || rows.Length <= 0)
            //{
            //    MessageBox.Show("Бүртгэлд ороогүй баркод байна.");
            //    return;
            //}

            #endregion

            _salesno = Static.ToStr(r["SALESNO"]);
            int itemno = Static.ToInt(r["ITEMNO"]);
            decimal custno = Static.ToDecimal(r["CUSTNO"]);
            string prodno = Static.ToStr(r["PRODNO"]);

            Result res = Receive(_salesno, custno, itemno, prodno, barcode, _userno, _userstate);
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        private void SubMenu_Edit()
        {
            #region Validation

            DataRow r = gridView1.GetFocusedDataRow();
            if (r == null)
            {
                MessageBox.Show("Түрээсийн хэрэгсэл сонгогдоогүй байна.");
                return;
            }

            int rentstatus = Static.ToInt(r["RENTSTATUS"]);
            if (rentstatus != 2)
            {
                MessageBox.Show("Түрээсийн хэрэгслийг хүлээн аваагүй байна.");
                return;
            }

            #endregion

            _salesno = Static.ToStr(r["SALESNO"]);
            int itemno = Static.ToInt(r["ITEMNO"]);
            decimal custno = Static.ToDecimal(r["CUSTNO"]);
            string prodno = Static.ToStr(r["PRODNO"]);
            string prodname = Static.ToStr(r["PRODNAME"]);
            
            frmRentNote frm = new frmRentNote(_core, _kb, _salesno, custno, itemno, prodno, prodname, _userno);
            frm.StartPosition = FormStartPosition.CenterScreen;
            DialogResult dlg = frm.ShowDialog();
            if (dlg == System.Windows.Forms.DialogResult.OK)
            {
                Result res = TagRentGet(_serialno);
            }
        }
        public void SubMenu_CreateCustomer()
        {
            InfoPos.fo_Customer.frmCustomer frm = new fo_Customer.frmCustomer(_core, "", "", "", "", "", "");
            frm.ShowDialog();
        }

        #endregion
        #region Control events

        public frmRent()
        {
            InitializeComponent();
            this.KeyPreview = true;

            #region Events
            this.KeyDown += frmRentMain_KeyDown;
            this.gridView1.RowStyle += gridView1_RowStyle;
            this.gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            this.gridView2.FocusedRowChanged += gridView2_FocusedRowChanged;
            this.FormClosing += frmRentMain_FormClosing;
            this.txtBarCode.KeyDown += txtBarCode_KeyDown;

            this.lblUser1.Click += lblUserAll_Click;
            this.lblUser2.Click += lblUserAll_Click;
            this.lblUser3.Click += lblUserAll_Click;
            this.lblUser4.Click += lblUserAll_Click;
            this.lblUser5.Click += lblUserAll_Click;

            #endregion
            #region GridView1 Formatting

            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            //gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
            gridView1.OptionsSelection.UseIndicatorForSelection = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            gridView1.RowHeight = 28;

            // зурган багана нэмж оруулахад энэ үзэгдлийг зарлаж дотор нь зургаа set хийнэ.
            //gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
            //gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView1_RowCellClick);
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);

            #endregion
        }

        void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (gridView1.IsGroupRow(e.RowHandle)) return;

                DataRow r = gridView1.GetDataRow(e.RowHandle);
                if (r != null)
                {
                    int userno = Static.ToInt(r["USERNO"]);
                    if (userno > 0)
                    {
                        Control ctl = (Control)_users[userno];
                        if (ctl != null)
                        {
                            e.Appearance.BackColor = ctl.BackColor;
                            e.Appearance.BackColor2 = Color.White;
                            e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
                        }
                    }
                }
            }
            catch
            { }
        }

        bool _enterkey = false;
        void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (_enterkey)
            {
                _enterkey = false;
                txtBarCode.EditValue = "";
            }
            if (e.KeyCode == Keys.Enter)
            {
                _enterkey = true;
                string barcode = Static.ToStr(txtBarCode.EditValue);
                if (barcode.Length > 2) BarcodeReaderGet(barcode);
            }
        }
                
        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }
        void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView2.GetFocusedDataRow();
            if (r == null) return;

            _serialno = Static.ToStr(r["SERIALNO"]);
            Result res = TagRentGet(_serialno);
            ISM.Template.FormUtility.ValidateQuery(res);

            _custno = Static.ToStr(r["CUSTNO"]);
            _custname = Static.ToStr(r["CUSTNAME"]);
            _custsex = Static.ToInt(r["SEX"]);
            _custheight = Static.ToInt(r["HEIGHT"]);
            _custfoot = Static.ToInt(r["FOOT"]);
            _custmember = Static.ToInt(r["MEMBERTYPE"]);
            _custrate = Static.ToStr(r["RATENAME"]);
            _custphone = string.Format("{0} {1} {2}", r["MOBILE"], r["TELEPHONE"], r["HOMEPHONE"]);

            _core.MainForm_HeaderSet(0, "Гишүүн", string.Format("{0}{1}"
                , _custsex == 1 ? "ЭР" : "ЭМ"
                , _custmember == 0 ? ", ЭНГИЙН" : ", ГИШҮҮН"
                ));
            _core.MainForm_HeaderSet(1, "Зэрэглэл", _custrate);
            _core.MainForm_HeaderSet(2, "Хэмжээ", string.Format("{0}см, {1}", _custheight, _custfoot));
            _core.MainForm_HeaderSet(3, "Утас", _custphone.Replace("  ", " "));

        }

        void frmRentMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
            ISM.Template.FormUtility.GridLayoutSave(gridView2, _layoutfilenametaglist);
        }
        void frmRentMain_KeyDown(object sender, KeyEventArgs e)
        {
            //System.IO.File.AppendAllText("c:\\barcode.txt", string.Format("\r\n{0} {1}", DateTime.Now.ToString("mm:ss.fff"), e.KeyValue));
            if (e.KeyCode == Keys.Tab
                || e.KeyCode == Keys.ShiftKey
                || e.Alt
                || e.Control
                || e.SuppressKeyPress
                ) return;
            
            if (this.ActiveControl != null
                && this.ActiveControl.Parent != null
                && this.ActiveControl.Parent.Name != "txtBarCode")
            {
                _enterkey = false;
                txtBarCode.SelectAll();
                txtBarCode.SelectedText = new string((char)e.KeyValue, 1);
                txtBarCode.Select();
            }
        }
        void frmRentMain_Load(object sender, EventArgs e)
        {
            _core.MainForm_HeaderClear();
            _core.MainForm_HeaderSet(0, "Гишүүн", "");
            _core.MainForm_HeaderSet(1, "Зэрэглэл", "");
            _core.MainForm_HeaderSet(2, "Хэмжээ", "");
            _core.MainForm_HeaderSet(3, "Утас", "");
            _core.MainForm_HeaderSet(4, "Түрээсийн ажилтан", "");

            InitTagTable();
            InitDict();
            InitUsers();
        }

        private void lblUserAll_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LabelControl ctl = (DevExpress.XtraEditors.LabelControl)sender;
            if (ctl == null) return;

            UserInfo uiOld = (UserInfo)_control.Tag;

            #region Шинээр анх удаа логин хийх гэж бгаа

            UserInfo uiNew = (UserInfo)ctl.Tag;
            if (uiNew.userno == 0) goto OnLogin;

            #endregion

            #region Холбогдсон идэвхтэй хэрэглэгч бол идэвхгүй болгох үйлдэл хийгдэнэ.

            if (uiNew.active && uiNew.Equals(uiOld))
            {
                string confirm = "Хэрэглэгчийн төлвөө идэвхгүй болгох уу?";
                if (ISM.Template.FormUtility.ValidateConfirm(confirm))
                {
                    RentUserActive(null);
                }
                return;
            }

            #endregion

        OnLogin:
            frmRentUserConnect frm = new frmRentUserConnect(_core, uiNew.userno);
            DialogResult dlg = frm.ShowDialog();

            if (dlg == System.Windows.Forms.DialogResult.OK)
            {
                txtBarCode.Properties.Appearance.BorderColor = ctl.Appearance.BorderColor;
                txtBarCode.Properties.Appearance.ForeColor = ctl.Appearance.BackColor;

                uiNew.active = true;
                uiNew.userno = frm.UserNo;
                uiNew.username = frm.UserName;

                _users[frm.UserNo] = ctl;

                RentUserActive(ctl);
            }

        }

        #endregion
        #region Business functions

        public Result InitDict()
        {
            string[] dictid = new string[] { "PADAMAGETYPE", "USERS", "WORKAREA", "INVSERIES", "INVMAIN" };
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject, dictid, 601003, ref _dict);
            ISM.Template.FormUtility.ValidateQuery(res);
            return res;
        }
        public void InitTagTable()
        {
            gridControl2.BeginUpdate();
            gridControl2.SuspendLayout();

            _dtTag = new DataTable();
            _dtTag.Columns.Add("CUSTNO", typeof(string));
            _dtTag.Columns.Add("CUSTNAME", typeof(string));
            _dtTag.Columns.Add("SERIALNO", typeof(string));
            _dtTag.Columns.Add("TAGTIME", typeof(DateTime));
            _dtTag.Columns.Add("HEIGHT", typeof(int));
            _dtTag.Columns.Add("FOOT", typeof(int));
            _dtTag.Columns.Add("SEX", typeof(int));
            _dtTag.Columns.Add("MEMBERTYPE", typeof(int));
            _dtTag.Columns.Add("RATENAME", typeof(string));
            _dtTag.Columns.Add("MOBILE", typeof(string));
            _dtTag.Columns.Add("TELEPHONE", typeof(string));
            _dtTag.Columns.Add("HOMEPHONE", typeof(string));

            gridView2.OptionsView.ColumnAutoWidth = false;

            //ISM.Template.FormUtility.GridLayoutGet(gridView2, _dtTag, _layoutfilenametaglist);
            gridControl2.DataSource = _dtTag;

            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 0, "Харилцагч", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 1, "Харилцагч");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 2, "Тагын дугаар");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 3, "Уншуулсан");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 4, "Өндөр",true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 5, "Гутал",true);

            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 6, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 7, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 8, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 9, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 10, "", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 11, "", true);

            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView2.OptionsSelection.EnableAppearanceHideSelection = false;

            gridView2.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView2.Columns[3].DisplayFormat.FormatString = "yy.MM.dd HH:mm";

            
            //gridView2.Columns[2].Width = 120;
            //gridView2.Columns[3].Width = 130;

            gridControl2.EndUpdate();
            gridControl2.ResumeLayout();

        }
        public void InitUsers()
        {
            string username = string.IsNullOrEmpty(_core.RemoteObject.User.UserFName) ?
                "" : _core.RemoteObject.User.UserFName.Substring(0, 1) + "." + _core.RemoteObject.User.UserLName;

            _userno = _core.RemoteObject.User.UserNo;
            _users[_userno] = lblUser1;

            lblUser1.Text = username;
            lblUser1.Tag = new UserInfo (_userno, username , true);
            lblUser2.Tag = new UserInfo (0, "" , false);
            lblUser3.Tag = new UserInfo (0, "" , false);
            lblUser4.Tag = new UserInfo (0, "" , false);
            lblUser5.Tag = new UserInfo (0, "" , false);

            RentUserActive(lblUser1);
        }

        public void RentUserActive(Control ctl)
        {
            if (_control != null)
            {
                UserInfo ui = (UserInfo)_control.Tag;
                ui.active = false;
                _control.ForeColor = Color.Black;

                _userno = 0;
                _userstate = 0;

                _core.MainForm_HeaderSet(4, "Түрээсийн ажилтан", "");
            }
            if (ctl != null)
            {
                UserInfo ui = (UserInfo)ctl.Tag;
                ui.active = true;
                ctl.ForeColor = Color.Red;
                ctl.Text = ui.username;

                _userno = ui.userno;
                _userstate = 0;
                _control = ctl;

                _core.MainForm_HeaderSet(4, "Түрээсийн ажилтан", ui.username);

            }
        }

        public void TagRentCaption()
        {
            Result res = null;
            try
            {
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Дэс №",true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Хэрэгслийн №", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Хэрэгслийн нэр");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Баркод");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Төлөв");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Торгууль"); // Dictionary -тэй холбож нэрийг гаргана.
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Илүү цаг (мин)");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Олгосон");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Авсан");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Олгосон байр", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Олгосон хэрэглэгч");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "Авсан хэрэглэгч байр", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 12, "Авсан хэрэглэгч");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 13, "Таг уншуулсан", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 14, "salesno", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 15, "custno", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 16, "rentstatus", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 17, "userno", true);

                gridView1.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns[7].DisplayFormat.FormatString = "HH:mm MM/dd";

                gridView1.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns[8].DisplayFormat.FormatString = "HH:mm MM/dd";

                gridView1.Columns[4].GroupIndex = 0;
                gridView1.ExpandAllGroups();

                //ISM.Template.FormUtility.Column_SetList(ref gridView1, 4, new string[] { "ОЛГООГҮЙ", "ОЛГОСОН", "АВСАН" });
                //ISM.Template.FormUtility.Column_SetList(ref gridView1, 5, (DataTable)_dict[0], "DAMAGETYPE", "NAME");
                //ISM.Template.FormUtility.Column_SetList(ref gridView1, 9, (DataTable)_dict[2], "AREACODE", "NAME");
                //ISM.Template.FormUtility.Column_SetList(ref gridView1, 10, (DataTable)_dict[1], "USERNO", "USERFNAME");
                //ISM.Template.FormUtility.Column_SetList(ref gridView1, 11, (DataTable)_dict[2], "AREACODE", "NAME");
                //ISM.Template.FormUtility.Column_SetList(ref gridView1, 12, (DataTable)_dict[1], "USERNO", "USERFNAME");
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            //gridView1.BestFitColumns();
            OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        public Result TagRentGet(string serialno)
        {
            Result res = null;

            gridControl1.BeginUpdate();
            gridControl1.SuspendLayout();

            try
            {
                #region Validation

                if (string.IsNullOrEmpty(serialno) || string.IsNullOrEmpty(serialno))
                {
                    res = new Result(6060011, "Борлуулалтын дугаар эсвэл тагын сериал дугаар хоосон орж ирлээ.");
                    goto OnExit;
                }

                #endregion
                #region Call server
                if (_remote != null)
                {
                    object[] param = new object[] { serialno };
                    res = _remote.Connection.Call(_remote.User.UserNo, 606, 606002, 606001, param);
                    if (res != null && res.ResultNo != 0) goto OnExit;

                    //this.Text = DateTime.Now.ToString("mm:ss.fff");

                    ISM.Template.FormUtility.GridLayoutGet(gridView1, res.Data.Tables[0], _layoutfilename);
                    TagRentCaption();

                }
                else
                {
                    res = new Result(99999, "Internal Error: Remote object not set.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            OnExit:

            gridControl1.ResumeLayout();
            gridControl1.EndUpdate();

            return res;
        }
        public Result TagReaderGet(string serialno)
        {
            Result res = null;
            try
            {
                #region Validation

                if (string.IsNullOrEmpty(serialno))
                {
                    res = new Result(6060011, "Үйлчлүүлэгчид Таг олгогдоогүй байна.");
                    goto OnExit;
                }

                DataRow[] rows = _dtTag.Select(string.Format("SERIALNO='{0}'", serialno));
                if (rows.Length > 0)
                {
                    rows[0]["TAGTIME"] = DateTime.Now;
                    res = new Result(9, string.Format("[{0}] дугаартай таг орсон байна.", serialno));
                    goto OnExit;
                }

                #endregion
                #region Call server
                if (_remote != null)
                {
                    object[] param = new object[] { serialno };
                    res = _remote.Connection.Call(_remote.User.UserNo, 606, 606003, 606001, param);
                    if (res != null && res.ResultNo != 0) goto OnExit;

                    DataTable dt = res.Data.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        rows = _dtTag.Select(string.Format("SERIALNO='{0}'", serialno));
                        if (rows.Length > 0)
                        {
                            rows[0]["TAGTIME"] = DateTime.Now;
                        }
                        else
                        {
                            _dtTag.Rows.Add(
                                dt.Rows[0]["CUSTNO"]
                                , dt.Rows[0]["CUSTNAME"]
                                , dt.Rows[0]["SERIALNO"]
                                , DateTime.Now
                                , dt.Rows[0]["HEIGHT"]
                                , dt.Rows[0]["FOOT"]

                                , dt.Rows[0]["SEX"]
                                , dt.Rows[0]["MEMBERTYPE"]
                                , dt.Rows[0]["RATENAME"]
                                , dt.Rows[0]["MOBILE"]
                                , dt.Rows[0]["TELEPHONE"]
                                , dt.Rows[0]["HOMEPHONE"]                                
                                );
                            _dtTag.AcceptChanges();

                            //ISM.Template.FormUtility.GridLayoutGet(gridView2, _dtTag, _layoutfilenametaglist);

                            //res = TagRentGet(serialno);
                        }
                    }
                }
                else
                {
                    res = new Result(99999, "Internal Error: Remote object not set.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            OnExit:
            return res;
        }

        public Result BarcodeReaderGet(string barcode)
        {
            Result res = null;
            try
            {
                #region Баркод уягдсан хэрэгслийн кодыг авах

                if (_dict == null || _dict.Count < 5)
                {
                    res = new Result(9, "Жагсаалт мэдээлэл бүрэн татагдаагүй байна.");
                    goto OnExit;
                }
                DataTable dt = (DataTable)_dict[3];
                DataRow[] rows = dt.Select(string.Format("BARCODE='{0}'", barcode));
                if (rows == null || rows.Length < 1)
                {
                    res = new Result(9, "Бүртгэлгүй баркод байна.");
                    goto OnExit;
                }

                string prodno_barcode = Static.ToStr(rows[0]["INVID"]);

                #endregion
                #region Баркодоор хэрэгслийн бичлэгийг олох

                dt = (DataTable)gridControl1.DataSource;
                if (dt == null)
                {
                    res = new Result(9, "Түрээсийн хэрэгсэл олдсонгүй.");
                    goto OnExit;
                }

                rows = dt.Select(string.Format("PRODNO='{0}'", prodno_barcode));
                if (rows == null || rows.Length <= 0)
                {
                    res = new Result(9, string.Format("[{0}] Түрээсийн хэрэгсэл олдсонгүй.", prodno_barcode));
                    goto OnExit;
                }

                DataRow r = null;
                foreach (DataRow i in rows)
                {
                    int rs = Static.ToInt(i["RENTSTATUS"]);
                    string bc = Static.ToStr(i["BARCODE"]);

                    if (rs == 0)
                    {
                        r = i;
                    }
                    else if (rs == 1 && bc == barcode)
                    {
                        /***********************************************
                         * Олголт хийчихсэн барааны бар код орж ирвэл 
                         * шууд хүлээн авах.
                         ***********************************************/
                        r = i;
                        break;
                    }
                }

                if (r == null)
                {
                    res = new Result(9, "Тохирох түрээсийн хэрэгсэл олдсонгүй.");
                    goto OnExit;
                }

                #endregion
                #region Deliver or Receive

                _salesno = Static.ToStr(r["SALESNO"]);
                int itemno = Static.ToInt(r["ITEMNO"]);
                decimal custno = Static.ToDecimal(r["CUSTNO"]);
                string prodno = Static.ToStr(r["PRODNO"]);
                string bcode = Static.ToStr(r["BARCODE"]);
                int rentstatus = Static.ToInt(r["RENTSTATUS"]);

                if (rentstatus == 0)
                {
                    res = Deliver(_salesno, custno, itemno, prodno, barcode, _userno, _userstate);
                }
                else if (rentstatus == 1)
                {
                    res = Receive(_salesno, custno, itemno, prodno, barcode, _userno, _userstate);
                }
                else
                {
                    res = new Result(9, "Тохирох түрээсийн хэрэгсэл олдсонгүй.");
                }

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:

            if (res != null && res.ResultNo != 0)
            {
                _core.AlertShow("Мэдээлэл", res.ResultDesc);
            }
            return res;
        }

        public Result Deliver(string salesno, decimal custno, int itemno, string prodno, string barcode, int userno, int userstate)
        {
            Result res = null;
            try
            {
                #region Validation
                
                if (string.IsNullOrEmpty(_serialno))
                {
                    res = new Result(6060011, "Тагын сериал дугаар хоосон байна.");
                    goto OnExit;
                }

                #endregion
                #region Call server
                if (_remote != null)
                {
                    object[] param = new object[] { salesno, custno, itemno, prodno, barcode, userno, userstate };
                    res = _remote.Connection.Call(_remote.User.UserNo, 606, 606006, 606006, param);
                    if (res != null && res.ResultNo != 0) goto OnExit;
                    
                    res = TagRentGet(_serialno);
                }
                else
                {
                    res = new Result(99999, "Internal Error: Remote object not set.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            return res;
        }
        public Result Receive(string salesno, decimal custno, int itemno, string prodno, string barcode, int userno, int userstate)
        {
            Result res = null;
            try
            {
                #region Validation

                if (string.IsNullOrEmpty(_serialno))
                {
                    res = new Result(6060011, "Тагын сериал дугаар хоосон байна.");
                    goto OnExit;
                }

                #endregion
                #region Call server
                if (_remote != null)
                {
                    object[] param = new object[] { salesno, custno, itemno, prodno, barcode, userno, userstate };
                    res = _remote.Connection.Call(_remote.User.UserNo, 606, 606007, 606007, param);
                    if (res != null && res.ResultNo != 0) goto OnExit;

                    res = TagRentGet(_serialno);
                }
                else
                {
                    res = new Result(99999, "Internal Error: Remote object not set.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            return res;
        }

        public void Alert(Result res, string caption)
        {
            if (res != null && res.ResultNo != 0)
            {
                if (_core != null)
                {
                    _core.AlertShow(caption, res.ResultDesc);
                }
            }
        }
        public void Alert(string text, string caption)
        {
                if (_core != null)
                {
                    _core.AlertShow(caption, text);
                }
        }
        #endregion
    }


    public class UserInfo
    {
        public int userno =0;
        public string username = null;
        public bool active = false;
        public string pwd = null;

        public UserInfo(int userno, string username, bool active)
        {
            this.userno = userno;
            this.username = username;
            this.active = active;
        }
    }
}
