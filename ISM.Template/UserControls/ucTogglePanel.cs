using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace ISM.Template
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))] 
    public partial class ucTogglePanel : UserControl
    {
        private Hashtable _hfields = new Hashtable();

        #region Toggle Properties

        private int _toggle_flag = 0; // 0-Normal, 1-New, 2-Editing
        [DefaultValue(0), Browsable(false)]
        public int ToggleFlag
        { get { return _toggle_flag; } }

        private ISM.Template.Resource _resource = null;
        [DefaultValue(null), Browsable(false)]
        public ISM.Template.Resource Resource
        {
            get { return _resource; }
            set
            {
                _resource = value;
                if (_resource != null)
                {
                    btnAdd.Image = _resource.GetBitmap("navigate_add");
                    btnEdit.Image = _resource.GetBitmap("navigate_edit");
                    btnSave.Image = _resource.GetBitmap("navigate_save");
                    btnReject.Image = _resource.GetBitmap("navigate_cancel");
                    btnRemove.Image = _resource.GetBitmap("navigate_delete");
                    //btnRefresh.Image = _resource.GetBitmap("navigate_refresh");
                    btnExit.Image = _resource.GetBitmap("navigate_home");
                }
            }
        }
        
        private DataSet _dataset = null;
        [DefaultValue(null), Browsable(false)]
        public DataSet DataSource
        {
            get { return _dataset; }
            set
            {
                _dataset = value;
                if (_gridview != null && _gridview.GridControl != null)
                {
                    _gridview.GridControl.DataSource = null;
                    if (_dataset != null && _dataset.Tables.Count > 0)
                    {
                        _gridview.GridControl.DataSource = _dataset.Tables[0];
                        if (_dataset.Tables[0].Rows.Count > 0)
                        {
                            _gridview.SelectRow(_gridview.GetRowHandle(0));
                        }
                    }
                }
                OnEventDataChanged();
            }
        }

        private ControlCollection _container = null;
        [DefaultValue(null), Browsable(false)]
        public ControlCollection FieldContainer
        {
            get { return _container; }
            set { _container = value; }
        }

        private DevExpress.XtraGrid.Views.Grid.GridView _gridview = null;
        [DefaultValue(null), Browsable(false)]
        public DevExpress.XtraGrid.Views.Grid.GridView GridView
        {
            get { return _gridview; }
            set { _gridview = value; }
        }

        private bool _disableallbutton;
        [Description("Show toggle button for New."), Category("Toggle"), DefaultValue(false), Browsable(true)]
        public bool ToggleDisableAllButton
        {
            get { return _disableallbutton; }
            set
            {
                _disableallbutton = value;
                ToggleButtons();
            }
        }

        private bool _shownew;
        [Description("Show toggle button for New."), Category("Toggle"), DefaultValue(true), Browsable(true)]
        public bool ToggleShowNew
        {
            get { return _shownew; }
            set
            {
                _shownew = value;
                btnAdd.Visible = value;
            }
        }

        private bool _showedit;
        [Description("Show toggle button for Edit."), Category("Toggle"), DefaultValue(true), Browsable(true)]
        public bool ToggleShowEdit
        {
            get { return _showedit; }
            set
            {
                _showedit = value;
                btnEdit.Visible = value;
            }
        }

        private bool _showsave;
        [Description("Show toggle button for Save the changes."), Category("Toggle"), DefaultValue(true), Browsable(true)]
        public bool ToggleShowSave
        {
            get { return _showsave; }
            set
            {
                _showsave = value;
                btnSave.Visible = value;
            }
        }

        private bool _showreject;
        [Description("Show toggle button for Reject the changes."), Category("Toggle"), DefaultValue(true), Browsable(true)]
        public bool ToggleShowReject
        {
            get { return _showreject; }
            set
            {
                _showreject = value;
                btnReject.Visible = value;
            }
        }

        private bool _showdelete;
        [Description("Show toggle button for Delete."), Category("Toggle"), DefaultValue(true), Browsable(true)]
        public bool ToggleShowDelete
        {
            get { return _showdelete; }
            set
            {
                _showdelete = value;
                btnRemove.Visible = value;
            }
        }

        private bool _showexit;
        [Description("Show toggle button for Exit."), Category("Toggle"), DefaultValue(true), Browsable(true)]
        public bool ToggleShowExit
        {
            get { return _showexit; }
            set
            {
                _showexit = value;
                btnExit.Visible = value;
            }
        }

        #endregion

        #region Toggle Internal Methods

        private void ToggleButtons()
        {
            if (_toggle_flag == 0)
            {
                btnAdd.Enabled = true & !_disableallbutton;
                btnEdit.Enabled = true & !_disableallbutton;
                btnSave.Enabled = false & !_disableallbutton;
                btnReject.Enabled = false & !_disableallbutton;
                btnRemove.Enabled = true & !_disableallbutton;
                btnExit.Enabled = true & !_disableallbutton;
            }
            else
            {
                btnAdd.Enabled = false & !_disableallbutton;
                btnEdit.Enabled = false & !_disableallbutton;
                btnSave.Enabled = true & !_disableallbutton;
                btnReject.Enabled = true & !_disableallbutton;
                btnRemove.Enabled = false & !_disableallbutton;
                btnExit.Enabled = false & !_disableallbutton;
            }
            if (_gridview != null && _gridview.GridControl != null)
                _gridview.GridControl.Enabled = _toggle_flag == 0;
        }
        private void ToggleNew()
        {
            _toggle_flag = 1;
            ToggleButtons();
        }
        private void ToggleEdit()
        {
            _toggle_flag = 2;
            ToggleButtons();
        }
        private void ToggleSave()
        {
            _toggle_flag = 0;
            ToggleButtons();
        }

        #endregion

        #region Constractor

        public ucTogglePanel()
        {
            InitializeComponent();
        }

        #endregion

        #region Custom Events

        public delegate void delegateEventAdd(ref bool cancel);
        public event delegateEventAdd EventAdd;
        public void OnEventAdd(ref bool cancel)
        {
            if (EventAdd != null) EventAdd(ref cancel);
        }

        public delegate void delegateEventAddAfter();
        public event delegateEventAddAfter EventAddAfter;
        public void OnEventAddAfter()
        {
            if (EventAddAfter != null) EventAddAfter();
        }

        public delegate void delegateEventEdit(ref bool cancel);
        public event delegateEventEdit EventEdit;
        public void OnEventEdit(ref bool cancel)
        {
            if (EventEdit != null) EventEdit(ref cancel);
        }

        public delegate void delegateEventDelete();
        public event delegateEventDelete EventDelete;
        public void OnEventDelete()
        {
            if (EventDelete != null) EventDelete();
        }

        public delegate void delegateEventReject();
        public event delegateEventReject EventReject;
        public void OnEventReject()
        {
            if (EventReject != null) EventReject();
        }

        public delegate void delegateEventSave(bool isnew, ref bool cancel);
        public event delegateEventSave EventSave;
        public void OnEventSave(bool isnew, ref bool cancel)
        {
            if (EventSave != null) EventSave(isnew, ref cancel);
        }

        public delegate void delegateEventDataChanged();
        public event delegateEventDataChanged EventDataChanged;
        public void OnEventDataChanged()
        {
            if (EventDataChanged != null) EventDataChanged();
        }

        public delegate void delegateEventExit(bool editing, ref bool cancel);
        public event delegateEventExit EventExit;
        public void OnEventExit(ref bool cancel)
        {
            if (EventExit != null) EventExit(_toggle_flag != 0,  ref cancel);
        }

        #endregion

        #region Control Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_toggle_flag == 0)
            {
                bool cancel = false;
                try
                {
                    OnEventAdd(ref cancel);
                }
                catch
                { }
                
                if (!cancel) FieldLinkSetNewState();

                try
                {
                    OnEventAddAfter();
                }
                catch
                {}
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_toggle_flag == 0)
            {
                bool cancel = false;
                try
                {
                    OnEventEdit(ref cancel);
                }
                catch
                { }
                if (!cancel) FieldLinkSetEditState();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_toggle_flag != 0)
            {
                bool cancel = false;
                try
                {
                    OnEventSave(_toggle_flag == 1, ref cancel);
                }
                catch
                { }
                if (!cancel) FieldLinkSetSaveState();
            }
        }
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (_toggle_flag != 0)
            {
                FieldLinkSetValues();
                FieldLinkSetRejectState();
                Application.DoEvents();

                OnEventReject();
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                OnEventDelete();

                if (_gridview != null)
                {
                    if (_gridview.RowCount <= 0) FieldLinkClearValues();
                    else FieldLinkSetValues();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            bool cancel = false;
            try
            {
                OnEventExit(ref cancel);
            }
            catch
            { }
        }

        #endregion

        #region Control Managing Methods

        public FieldInfo FieldLinkAdd(string ctrlname, int tableindex, string dbfieldname, string editmask, bool ismandatory, bool iskey, bool isdisable)
        {
            FieldInfo fi = new FieldInfo();
            fi.ctrlname = ctrlname;
            fi.tableindex = tableindex;
            fi.dbfieldname = dbfieldname;
            fi.editmask = editmask;
            fi.ismandatory = ismandatory;
            fi.iskey = iskey;
            fi.isdisable = isdisable;

            lock (_hfields.SyncRoot)
            { _hfields.Add(ctrlname, fi); }

            return fi;
        }
        public FieldInfo FieldLinkAdd(string ctrlname, int tableindex, string dbfieldname, string editmask, bool ismandatory, bool iskey)
        {
            return FieldLinkAdd(ctrlname, tableindex, dbfieldname, editmask, ismandatory, iskey, false);
        }

        public void FieldLinkRemove(string ctrlname)
        {
            lock (_hfields.SyncRoot)
            { _hfields.Remove(ctrlname); }
        }
        public void FieldLinkClear()
        {
            lock (_hfields.SyncRoot)
            { _hfields.Clear(); }
        }

        private void FieldLinkClearValues(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl != null)
                    {
                        if (!string.IsNullOrEmpty(ctrl.Name))
                        {
                            if (_hfields.ContainsKey(ctrl.Name))
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                if (fi != null && !string.IsNullOrEmpty(fi.dbfieldname))
                                {
                                    fi.value = null;
                                    if (ctrl is DevExpress.XtraEditors.BaseEdit)
                                    {
                                        DevExpress.XtraEditors.BaseEdit lue = (DevExpress.XtraEditors.BaseEdit)ctrl;
                                        lue.EditValue = DBNull.Value;
                                    }
                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldLinkClearValues(ctrl.Controls);
                    }
                }
            }
        }
        public void FieldLinkClearValues()
        {
            this.SuspendLayout();
            FieldLinkClearValues(this.Controls);
            if (_container != null) FieldLinkClearValues(_container);
            this.ResumeLayout(true);
        }

        private void FieldLinkSetValues(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl != null)
                    {
                        if (!string.IsNullOrEmpty(ctrl.Name))
                        {
                            if (_hfields.ContainsKey(ctrl.Name))
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                if (fi != null && !string.IsNullOrEmpty(fi.dbfieldname))
                                {
                                    try
                                    {
                                        object value = fi.value;
                                        DataRow dr = null;
                                        if (_gridview != null)
                                        {
                                            #region Getting focused row from gridview
                                            if (_gridview.Columns.ColumnByFieldName(fi.dbfieldname.ToUpper()) != null)
                                            {
                                                dr = _gridview.GetFocusedDataRow();
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region Getting specified table row

                                            if (_dataset != null)
                                            {
                                                if (fi.tableindex >= 0 && fi.tableindex < _dataset.Tables.Count)
                                                {
                                                    DataTable dt = _dataset.Tables[fi.tableindex];
                                                    if (dt.Columns.Contains(fi.dbfieldname))
                                                    {
                                                        if (dt.Rows.Count > 0) dr = dt.Rows[0];
                                                    }
                                                }
                                            }

                                            #endregion
                                        }

                                        if (dr != null) value = dr[fi.dbfieldname];

                                        #region Setting control values

                                        if (value == null || value == DBNull.Value)
                                        {
                                            if (ctrl is DevExpress.XtraEditors.BaseEdit)
                                            {
                                                DevExpress.XtraEditors.BaseEdit lue = (DevExpress.XtraEditors.BaseEdit)ctrl;
                                                lue.EditValue = DBNull.Value;
                                            }
                                        }
                                        else
                                        {
                                            string ctrltype = ctrl.GetType().Name;
                                            switch (ctrltype)
                                            {
                                                case "CalcEdit":
                                                    ((DevExpress.XtraEditors.CalcEdit)ctrl).Value = EServ.Shared.Static.ToDecimal(value);
                                                    break;
                                                case "SpinEdit":
                                                    ((DevExpress.XtraEditors.SpinEdit)ctrl).Value = EServ.Shared.Static.ToDecimal(value);
                                                    break;
                                                case "DateEdit":
                                                    ((DevExpress.XtraEditors.DateEdit)ctrl).DateTime = EServ.Shared.Static.ToDateTime(value);
                                                    break;
                                                case "TimeEdit":
                                                    ((DevExpress.XtraEditors.TimeEdit)ctrl).Time = EServ.Shared.Static.ToDateTime(value);
                                                    break;
                                                case "TextEdit":
                                                case "MRUEdit":
                                                case "MemoEdit":
                                                case "MemoExEdit":
                                                case "ComboBoxEdit":
                                                case "CheckedComboBoxEdit":
                                                    ctrl.Text = Convert.ToString(value);
                                                    break;
                                                case "LookUpEdit":
                                                    DevExpress.XtraEditors.LookUpEdit lue = (DevExpress.XtraEditors.LookUpEdit)ctrl;
                                                    DataTable dt = null;
                                                    #region Casting DataTable/DataView into DataTable
                                                    if (lue.Properties.DataSource is DataView)
                                                    {
                                                        DataView dv = (DataView)lue.Properties.DataSource;
                                                        dt = dv.Table;
                                                    }
                                                    else
                                                    {
                                                        dt = (DataTable)lue.Properties.DataSource;
                                                    }
                                                    #endregion
                                                    if (dt != null && dt.Columns.Count > 0)
                                                    {
                                                        #region Get data type of ValueMember column
                                                        Type totype = typeof(string);
                                                        if (string.IsNullOrEmpty(lue.Properties.ValueMember))
                                                        {
                                                            totype = dt.Columns[0].DataType;
                                                        }
                                                        else
                                                        {
                                                            if (dt.Columns.Contains(lue.Properties.ValueMember))
                                                                totype = dt.Columns[lue.Properties.ValueMember].DataType;
                                                        }
                                                        #endregion
                                                        #region Converting value into data type
                                                        value = EServ.Shared.Static.ConvToType(totype, value);
                                                        #endregion
                                                    }
                                                    lue.EditValue = value;
                                                    break;
                                                case "CheckEdit":
                                                    ((DevExpress.XtraEditors.CheckEdit)ctrl).Checked = EServ.Shared.Static.ToBool(value);
                                                    break;
                                                case "PictureEdit":
                                                    using (MemoryStream ms = new MemoryStream((byte[])value))
                                                    {
                                                        ((DevExpress.XtraEditors.PictureEdit)ctrl).Image = System.Drawing.Image.FromStream(ms);
                                                    }
                                                    break;
                                                case "ImageEdit":
                                                    using (MemoryStream ms = new MemoryStream((byte[])value))
                                                    {
                                                        ((DevExpress.XtraEditors.ImageEdit)ctrl).Image = System.Drawing.Image.FromStream(ms);
                                                    }
                                                    break;
                                                default:
                                                    ctrl.Text = EServ.Shared.Static.ToStr(value);
                                                    break;
                                            }
                                        }
                                        #endregion
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldLinkSetValues(ctrl.Controls);
                    }
                }
            }
        }
        public void FieldLinkSetValues()
        {
            //this.SuspendLayout();
            FieldLinkSetValues(this.Controls);
            if (_container != null) FieldLinkSetValues(_container);
            //this.ResumeLayout(true);
        }

        private void FieldLinkSetNewStateFormat(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl != null)
                    {
                        if (!string.IsNullOrEmpty(ctrl.Name))
                        {
                            //if (_hfields.ContainsKey(ctrl.Name)) //2011.04.27
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                //if (fi != null) //Холбоосгүй контролуудыг ч бас хоослох. Ээгий Орхон хоёр, 2011.04.27
                                {
                                    if (ctrl is DevExpress.XtraEditors.BaseEdit) //BaseControl
                                    {
                                        ((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.ReadOnly = (fi != null && fi.isdisable);
                                        if (fi != null && fi.ismandatory)
                                        {
                                            ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Info);
                                        }
                                        else
                                        {
                                            ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.White);
                                        }
                                    }
                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldLinkSetNewStateFormat(ctrl.Controls);
                    }
                }
            }
        }
        private void FieldLinkSetNewState(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl != null)
                    {
                        if (!string.IsNullOrEmpty(ctrl.Name))
                        {
                            //if (_hfields.ContainsKey(ctrl.Name)) //2011.04.27
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                //if (fi != null) //Холбоосгүй контролуудыг ч бас хоослох. Ээгий Орхон хоёр, 2011.04.27
                                {
                                    if (ctrl is DevExpress.XtraEditors.BaseEdit) //BaseControl
                                    {
                                        ((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.ReadOnly = (fi != null && fi.isdisable);

                                            string ctrltype = ctrl.GetType().Name;
                                            switch (ctrltype)
                                            {
                                                case "CheckEdit":
                                                    ((DevExpress.XtraEditors.CheckEdit)ctrl).Checked = false;
                                                    break;
                                                case "LookUpEdit":
                                                    ((DevExpress.XtraEditors.LookUpEdit)ctrl).ClosePopup();
                                                    ((DevExpress.XtraEditors.LookUpEdit)ctrl).EditValue = DBNull.Value;
                                                    //((DevExpress.XtraEditors.LookUpEdit)ctrl).Properties.NullText = "";
                                                    break;
                                                default:
                                                    ctrl.Text = null;
                                                    break;
                                            }
                                        if (fi != null && fi.ismandatory)
                                        {
                                            ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Info);
                                        }
                                        else
                                        {
                                            ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.White);
                                        }
                                    }
                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldLinkSetNewState(ctrl.Controls);
                    }
                }
            }
        }
        public void FieldLinkSetNewState()
        {
            //this.SuspendLayout();
            ToggleNew();
            FieldLinkSetNewState(this.Controls);
            if (_container != null) FieldLinkSetNewState(_container);
            //this.ResumeLayout(true);
        }
        public void FieldLinkSetNewState(bool clearvalue)
        {
            //this.SuspendLayout();
            ToggleNew();

            if (clearvalue) FieldLinkSetNewState(this.Controls);
            else FieldLinkSetNewStateFormat(this.Controls);

            if (_container != null)
                if (clearvalue) FieldLinkSetNewState(_container);
                else FieldLinkSetNewStateFormat(_container);

            //this.ResumeLayout(true);
        }

        private void FieldLinkSetEditState(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl != null)
                    {
                        if (!string.IsNullOrEmpty(ctrl.Name))
                        {
                            if (_hfields.ContainsKey(ctrl.Name))
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                if (fi != null)
                                {
                                    ((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.ReadOnly = (fi.iskey || fi.isdisable);
                                    if (fi.iskey)
                                    {
                                        ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);
                                    }
                                    else if (fi.ismandatory)
                                    {
                                        ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Info);
                                        //((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.Appearance.BorderColor = System.Drawing.Color.Red;    
                                    }
                                    else
                                    {
                                        ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.White);
                                    }
                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldLinkSetEditState(ctrl.Controls);
                    }
                }
            }
        }
        public void FieldLinkSetEditState()
        {
            //this.SuspendLayout();
            ToggleEdit();
            FieldLinkSetEditState(this.Controls);
            if (_container != null) FieldLinkSetEditState(_container);
            //this.ResumeLayout(true);
        }

        private void FieldLinkSetSaveState(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl != null)
                    {
                        if (!string.IsNullOrEmpty(ctrl.Name))
                        {
                            if (_hfields.ContainsKey(ctrl.Name))
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                if (fi != null)
                                {
                                    ((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.ReadOnly = true;
                                    ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);

                                    DevExpress.XtraEditors.BaseEdit edit = (DevExpress.XtraEditors.BaseEdit)ctrl;
                                    fi.value = edit.EditValue;

                                    #region Setting specified table row with new value

                                    if (_dataset != null)
                                    {
                                        if (fi.tableindex >= 0 && fi.tableindex < _dataset.Tables.Count)
                                        {
                                            DataTable dt = _dataset.Tables[fi.tableindex];
                                            if (dt.Columns.Contains(fi.dbfieldname))
                                            {
                                                if (dt.Rows.Count > 0)
                                                {
                                                    DataRow dr = dt.Rows[0];
                                                    if (this._gridview != null) dr = dt.Rows[this._gridview.GetFocusedDataSourceRowIndex()];
                                                    dr[fi.dbfieldname] = edit.EditValue == null ? DBNull.Value : edit.EditValue;
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldLinkSetSaveState(ctrl.Controls);
                    }
                }
            }
        }
        public void FieldLinkSetSaveState()
        {
            //this.SuspendLayout();
            ToggleSave();
            FieldLinkSetSaveState(this.Controls);
            if (_container != null) FieldLinkSetSaveState(_container);
            //this.ResumeLayout(true);
        }

        private void FieldLinkSetRejectState(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl != null)
                    {
                        if (!string.IsNullOrEmpty(ctrl.Name))
                        {
                            if (_hfields.ContainsKey(ctrl.Name))
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                if (fi != null)
                                {
                                    ((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.ReadOnly = true;
                                    ctrl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control);
                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldLinkSetRejectState(ctrl.Controls);
                    }
                }
            }
        }
        public void FieldLinkSetRejectState()
        {
            //this.SuspendLayout();
            ToggleSave();
            FieldLinkSetRejectState(this.Controls);
            if (_container != null) FieldLinkSetRejectState(_container);
            //this.ResumeLayout(true);
        }

        public void FieldLinkSetColumnCaption(int colno, string caption, bool hide)
        {
        }
        public void FieldLinkSetColumnCaption(int colno, string caption)
        {
            FieldLinkSetColumnCaption(colno, caption, false);
        }

        private void FieldValidate(Control.ControlCollection ctrls,ref int number,ref Control first, ref ArrayList mandatories)
        {
            if (ctrls != null)
            {
                foreach (Control ctrl in ctrls)
                {
                    if (ctrl != null)
                    {
                        if (!string.IsNullOrEmpty(ctrl.Name))
                        {
                            if (_hfields.ContainsKey(ctrl.Name))
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                if (fi != null)
                                {
                                    if (ctrl is DevExpress.XtraEditors.BaseEdit)
                                    {
                                        DevExpress.XtraEditors.BaseEdit edit = (DevExpress.XtraEditors.BaseEdit)ctrl;
                                        if (fi.ismandatory && (edit.EditValue == null || edit.EditValue == DBNull.Value || Convert.ToString(edit.EditValue)==""))
                                        {
                                            number++;
                                            if (first == null) first = ctrl;
                                            mandatories.Add(string.Format("{0}. {1}", number, ((DevExpress.XtraEditors.BaseEdit)ctrl).ToolTipTitle));
                                        }
                                    }
                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldValidate(ctrl.Controls,ref number,ref first, ref mandatories);
                    }
                }
            }
        }
        public bool FieldValidate(ref string desc, ref Control first)
        {
            ArrayList ar = new ArrayList();
            int number = 0;

            if(_container == null)
                FieldValidate(this.Controls, ref number, ref first, ref ar);
            else
                FieldValidate(_container, ref number, ref first, ref ar);

            StringBuilder sb = new StringBuilder();
            foreach (string i in ar)
            {
                if (sb.Length > 0) sb.AppendLine();
                sb.Append(i);
            }
            desc = sb.ToString();
            return (ar.Count <= 0);
        }

        #endregion
    }
}
