using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace ISM.Template.UserControls
{
    public partial class ucDynamicParameter : UserControl
    {
        #region Constants

        const int CONST_FILEID = 105;

        #endregion

        #region Variables
        string[] valuenames = new string[] { "ТЕКСТ", "ТОО", "ОГНОО", "ЦАГ", "ФАЙЛ", "ХАВТАС", "ЗУРАГ", "ФОНТ", "ӨНГӨ", "ЖАГСААЛТ" };
        string[] valuenames2 = new string[] { "ТЕКСТ", "ТОО", "ОГНОО", "ЦАГ", "", "", "", "ФОНТ", "ӨНГӨ", "ЖАГСААЛТ","РЕГИСТЕР-Н ДУГААР" }; //Used for Zuil
        DataTable dict = null;
        #endregion

        #region Properties

        private CUser.Remote _remote = null;
        [DefaultValue(null), Browsable(false)]
        public CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }
        private Resource _resource = null;
        [DefaultValue(null), Browsable(false)]
        public Resource Resource
        {
            get { return _resource; }
            set
            {
                _resource = value;
                ucTogglePanel1.Resource = value;
            }
        }

        [DefaultValue(null), Browsable(false)]
        public DataRow SelectedRow
        {
            get
            {
                return gridView1.GetFocusedDataRow();
            }
        }

        private DataTable _table = null;
        [DefaultValue(null), Browsable(false)]
        public DataTable DataSource
        {
            get { return _table; }
            set
            {
                _table = value;
                gridControl1.DataSource = null;
                gridControl1.DataSource = _table;
                OnEventDataChanged();
            }
        }

        private string _tablename = "";
        [DefaultValue(""), Browsable(true)]
        public string TableName
        {
            get { return _tablename; }
            set { _tablename = value; }
        }

        private ulong _tabletypeid;
        [DefaultValue(0), Browsable(true)]
        public ulong TableTypeId
        {
            get { return _tabletypeid; }
            set { _tabletypeid = value; }
        }

        private int _tableprivselect = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivSelect
        {
            get { return _tableprivselect; }
            set { _tableprivselect = value; }
        }

        private int _tableprivupdate = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivUpdate
        {
            get { return _tableprivupdate; }
            set { _tableprivupdate = value; }
        }

        private bool _usetypelist2 = false;
        [DefaultValue(false), Browsable(true)]
        public bool UseTypeList2
        {
            get { return _usetypelist2; }
            set { _usetypelist2 = value; }
        }

        #endregion

        #region Custom Events

        public delegate void delegateEventDataChanged();
        public event delegateEventDataChanged EventDataChanged;
        public void OnEventDataChanged()
        {
            if (EventDataChanged != null) EventDataChanged();
        }

        public delegate void delegateEventSelected(DataRow selectedrow);
        public event delegateEventSelected EventSelected;
        public void OnEventSelected(DataRow selectedrow)
        {
            if (EventSelected != null) EventSelected(selectedrow);
        }

        public delegate void delegateEventInsert(Result r, ref bool cancel);
        public event delegateEventInsert EventInsert;
        public void OnEventInsert(Result r, ref bool cancel)
        {
            if (EventInsert != null) EventInsert(r, ref cancel);
        }
        public delegate void delegateEventUpdate(Result r, ref bool cancel);
        public event delegateEventInsert EventUpdate;
        public void OnEventUpdate(Result r, ref bool cancel)
        {
            if (EventUpdate != null) EventUpdate(r, ref cancel);
        }
        public delegate void delegateEventDelete(Result r, ref bool cancel);
        public event delegateEventDelete EventDelete;
        public void OnEventDelete(Result r, ref bool cancel)
        {
            if (EventDelete != null) EventDelete(r, ref cancel);
        }

        #endregion

        #region Constractor

        public ucDynamicParameter()
        {
            InitializeComponent();

            #region Toggle Events
            ucTogglePanel1.EventSave += new ucTogglePanel.delegateEventSave(ucTogglePanel1_EventSave);
            ucTogglePanel1.EventDelete += new ucTogglePanel.delegateEventDelete(ucTogglePanel1_EventDelete);
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            cmbDictId.EditValueChanged += new EventHandler(cmbDictId_EditValueChanged);
            #endregion

            #region Toggle Settings
            ucTogglePanel1.FieldContainer = pnlDetail.Controls;
            ucTogglePanel1.GridView = gridView1;

            ucTogglePanel1.ToggleShowNew = true;
            ucTogglePanel1.ToggleShowEdit = true;
            ucTogglePanel1.ToggleShowSave = true;
            ucTogglePanel1.ToggleShowReject = true;
            ucTogglePanel1.ToggleShowDelete = true;

            #endregion

            #region Field Links
            ucTogglePanel1.FieldLinkAdd("numItemId", 0, "ITEMID", "", true, true);
            ucTogglePanel1.FieldLinkAdd("txtName", 0, "NAME", "", true, false);
            ucTogglePanel1.FieldLinkAdd("txtName2", 0, "NAME2", "", false, false);
            ucTogglePanel1.FieldLinkAdd("cmbType", 0, "VALUETYPE", "", true, false);
            ucTogglePanel1.FieldLinkAdd("numLen", 0, "VALUELENGTH", "", false, false);
            ucTogglePanel1.FieldLinkAdd("txtDefault", 0, "VALUEDEFAULT", "", false, false);
            ucTogglePanel1.FieldLinkAdd("chkMandatory", 0, "MANDATORY", "", false, false);
            ucTogglePanel1.FieldLinkAdd("txtEditMask", 0, "EDITMASK", "", false, false);
            ucTogglePanel1.FieldLinkAdd("txtDesc", 0, "DESCRIPTION", "", false, false);
            ucTogglePanel1.FieldLinkAdd("cmbDictId", 0, "DICTID", "", false, false);
            ucTogglePanel1.FieldLinkAdd("cmbDictValue", 0, "DICTVALUEFIELD", "", false, false);
            ucTogglePanel1.FieldLinkAdd("cmbDictDesc", 0, "DICTDESCFIELD", "", false, false);
            ucTogglePanel1.FieldLinkAdd("numOrderNo", 0, "ORDERNO", "", false, false);
            ucTogglePanel1.FieldLinkAdd("chkDictEditable", 0, "DICTEDITABLE", "", false, false);
            ucTogglePanel1.FieldLinkAdd("chkCalculate", 0, "CALCULATE", "", false, false);
            ucTogglePanel1.FieldLinkAdd("cmbDictParentObject", 0, "DICTPARENTOBJECT", "", false, false);
            ucTogglePanel1.FieldLinkAdd("cmbDictFilterDesc", 0, "DICTFILTERDESC", "", false, false);
            #endregion
        }

        #endregion

        #region Control Events

        void ucTogglePanel1_EventSave(bool isnew, ref bool cancel)
        {
            Result r = null;
            string desc = "";
            Control first = null;

            bool success = ucTogglePanel1.FieldValidate(ref desc, ref first);

            if (isnew)
            {
                if (!success)
                {
                    r = new Result(9, string.Format("Мандатор талбарын утгыг бүрэн оруулаагүй байна.\r\n{0}", desc));
                    first.Select();
                }
                else
                {
                    if (Static.ToLong(numItemId.EditValue) != 0)
                        r = GridInsert();
                    else
                    {
                        MessageBox.Show("Талбарын дугаарыг 0 ээс ялгаатай оруулна уу.");
                        first.Select();
                    }
                }
                OnEventInsert(r, ref cancel);
            }
            else
            {
                if (!success)
                {
                    r = new Result(9, string.Format("Мандатор талбарын утгыг бүрэн оруулна уу.\r\n{0}", desc));
                    first.Select();
                }
                else
                {
                    if (Static.ToLong(numItemId.EditValue) != 0)
                        r = GridSave();
                    else
                    {
                        MessageBox.Show("Талбарын дугаарыг 0 ээс ялгаатай оруулна уу.");
                        first.Select();
                    }
                }
                OnEventUpdate(r, ref cancel);
            }
            if (!cancel) GridRead();
        }
        void ucTogglePanel1_EventDelete()
        {
            bool cancel = false;

            DialogResult dr = MessageBox.Show(string.Format("Бичлэг уcтгах гэж байна, цааш үргэлжлүүлэх үү?\r\nТалбарын нэр: {0}", txtName.Text)
                ,"", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                Result r = GridDelete();
                OnEventDelete(r, ref cancel);
                if (!cancel) GridRead();
            }
        }
        private void ucDynamicParameter_Load(object sender, EventArgs e)
        {
            ucTogglePanel1.FieldLinkSetSaveState();
        }
        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucTogglePanel1.FieldLinkSetValues();
        }
        void cmbDictId_EditValueChanged(object sender, EventArgs e)
        {
            string s = ISM.Lib.Static.ToStr(cmbDictId.EditValue);
            //if (string.IsNullOrEmpty(s))
            {
                SetListDictField();
            }
        }

        #endregion

        #region Server Functions

        public Result GridRead()
        {
            Result res = null;
            try
            {
                if (_remote != null)
                {
                    #region Preparing Calling Parameters
                    object[] param = new object[] { _tablename, _tabletypeid };
                    #endregion

                    #region Call Server Function
                    res = _remote.Connection.Call(
                        _remote.User.UserNo
                        , CONST_FILEID
                        , 105101
                        , _tableprivselect
                        , param
                        );
                    #endregion

                    #region Set table into Grid
                    if (res.ResultNo == 0)
                    {
                        gridControl1.DataSource = null;
                        if (res.Data != null && res.Data.Tables.Count > 0)
                        {
                            gridControl1.DataSource = res.Data.Tables[0];
                            SetFormatGrid();
                            SetListCombo();
                        }
                    }
                    else
                    {
                        MessageBox.Show(res.ResultDesc);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

            return res;
        }
        public Result GridSave()
        {
            Result res = null;
            try
            {
                if (_remote != null)
                {
                    #region Preparing Calling Parameters
                    object[] param = new object[] { 
                        _tablename 
                        , _tabletypeid
                        , numItemId.Value
                        , txtName.Text
                        , txtName2.Text
                        , cmbType.EditValue
                        , numLen.Value
                        , txtDefault.EditValue
                        , chkMandatory.EditValue
                        , txtEditMask.Text
                        , txtDesc.Text
                        , cmbDictId.EditValue
                        , chkDictEditable.EditValue
                        , cmbDictValue.Text
                        , cmbDictDesc.Text 
                        , numOrderNo.Value
                        , chkCalculate.EditValue
                        , cmbDictParentObject.EditValue
                        , cmbDictFilterDesc.EditValue
                    };
                    #endregion

                    #region Call Server Function
                    res = _remote.Connection.Call(
                        _remote.User.UserNo
                        , CONST_FILEID
                        , 105104
                        , _tableprivupdate
                        , param
                        );
                    #endregion

                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        public Result GridInsert()
        {
            Result res = null;
            try
            {
                if (_remote != null)
                {
                    #region Preparing Calling Parameters
                    object[] param = new object[] { 
                        _tablename 
                        , _tabletypeid
                        , numItemId.Value
                        , txtName.Text
                        , txtName2.Text
                        , cmbType.EditValue
                        , numLen.Value
                        , txtDefault.EditValue
                        , chkMandatory.EditValue
                        , txtEditMask.Text
                        , txtDesc.Text
                        , cmbDictId.EditValue
                        , chkDictEditable.EditValue
                        , cmbDictValue.Text
                        , cmbDictDesc.Text 
                        , numOrderNo.Value
                        , chkCalculate.EditValue
                        , cmbDictParentObject.EditValue
                        , cmbDictFilterDesc.EditValue
                    };
                    
                    #endregion

                    #region Call Server Function
                    res = _remote.Connection.Call(
                        _remote.User.UserNo
                        , CONST_FILEID
                        , 105105
                        , _tableprivupdate
                        , param
                        );
                    #endregion

                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        public Result GridDelete()
        {
            Result res = null;
            try
            {
                if (_remote != null)
                {
                    #region Preparing Calling Parameters
                    object[] param = new object[] { 
                        _tablename 
                        , _tabletypeid
                        , numItemId.Value
                    };
                    #endregion

                    #region Call Server Function
                    res = _remote.Connection.Call(
                        _remote.User.UserNo
                        , CONST_FILEID
                        , 105103
                        , _tableprivupdate
                        , param
                        );
                    #endregion
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }

        #endregion

        #region Control Managing Methods

        public void SetFormatGrid()
        {
            FormUtility.Column_SetCaption(ref gridView1, 0, "No");
            FormUtility.Column_SetCaption(ref gridView1, 1, "Нэр");
            FormUtility.Column_SetCaption(ref gridView1, 2, "Нэр2");
            FormUtility.Column_SetCaption(ref gridView1, 3, "Төрөл");
            FormUtility.Column_SetCaption(ref gridView1, 4, "Урт");
            FormUtility.Column_SetCaption(ref gridView1, 5, "Дефаулт");
            FormUtility.Column_SetCaption(ref gridView1, 6, "Мандатор");
            FormUtility.Column_SetCaption(ref gridView1, 7, "Маск");
            FormUtility.Column_SetCaption(ref gridView1, 8, "Тайлбар");
            FormUtility.Column_SetCaption(ref gridView1, 9, "Лист ID");
            FormUtility.Column_SetCaption(ref gridView1, 10, "Лист Засах");
            FormUtility.Column_SetCaption(ref gridView1, 11, "Лист Талбар1");
            FormUtility.Column_SetCaption(ref gridView1, 12, "Лист Талбар2");
            FormUtility.Column_SetCaption(ref gridView1, 13, "Эрэмбэ");
            FormUtility.Column_SetCaption(ref gridView1, 14, "Тооцоолол");
            FormUtility.Column_SetCaption(ref gridView1, 15, "Эх жагсаалт");
            FormUtility.Column_SetCaption(ref gridView1, 16, "Шүүлт хийх талбар");

            FormUtility.SetFormatGrid(ref gridView1, false);
        }
        public void SetListCombo()
        {
            FormUtility.Column_SetList(ref gridView1, 3, valuenames);

            if (_usetypelist2)
            {
                //This type list used for Dynamic Object forms
                //This list does not contain some types such as ФАЙЛ, ХАВТАС, ЗУРАГ
                for (int i = 0; i < valuenames2.Length; i++)
                {
                    if (!string.IsNullOrEmpty(valuenames2[i]))
                        FormUtility.LookUpEdit_SetList(ref cmbType, i, valuenames2[i]);
                }
            }
            else
            {
                //This type used for Additional Data forms
                FormUtility.LookUpEdit_SetList(ref cmbType, valuenames);
            }
            if (_remote != null)
            {
                DataTable dt = null;
                Result r = DictUtility.Get(_remote, "DICTIONARY", _tableprivselect, ref dt);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show("SetListCombo: " + r.ResultDesc);
                }

                FormUtility.LookUpEdit_SetList(ref cmbDictId, dt, "ID", "NAME", "", new int[] { 2 });
                dt = null;
                r = DictUtility.Get(_remote, "OBJECTITEM", _tableprivselect, ref dt);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show("SetListCombo: " + r.ResultDesc);
                }

                FormUtility.LookUpEdit_SetList(ref cmbDictParentObject, dt, "ITEMID", "NAME", "VALUETYPE=9", new int[] { 2 });
                SetListDictField();
            }
        }
        public void SetListDictField()
        {
            string fieldnames = ISM.Lib.Static.ToStr(cmbDictId.GetColumnValue("FIELDNAMES"));
            //if (!string.IsNullOrEmpty(fieldnames))
            {
                string[] values = fieldnames.Split(',');
                FormUtility.ComboEdit_SetList(ref cmbDictValue, values, true);
                FormUtility.ComboEdit_SetList(ref cmbDictDesc, values, true);
                FormUtility.ComboEdit_SetList(ref cmbDictFilterDesc, values, true);
            }
        }

        #endregion

        private void cmbDictParentObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                cmbDictParentObject.EditValue = null;
        }
    }
}
