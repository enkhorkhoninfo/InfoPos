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
    public partial class ucPropPanel : UserControl
    {
        private Hashtable _hfields = new Hashtable();

        #region Toggle Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(DevExpress.XtraEditors.Container.EditorContainer), typeof(UITypeEditor))]
        public DevExpress.XtraEditors.GroupControl GroupControlData
        {
            get { return groupControl1; }
        }

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
                    btnRemove.Image = _resource.GetBitmap("navigate_delete");
                    btnRefresh.Image = _resource.GetBitmap("navigate_refresh");
                    btnExit.Image = _resource.GetBitmap("navigate_home");
                }
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

        #endregion

        #region Toggle Internal Methods

        private void ToggleButtons()
        {
            if (_toggle_flag == 0)
            {
                btnRefresh.Enabled = true;
                btnRemove.Enabled = true;
                btnExit.Enabled = true;

                btnAdd.Text = "Нэмэх";
                if (_resource != null) btnAdd.Image = _resource.GetBitmap("navigate_add");

                btnEdit.Text = "Засах";
                if (_resource != null) btnEdit.Image = _resource.GetBitmap("navigate_edit");
            }
            else
            {
                btnRefresh.Enabled = false;
                btnRemove.Enabled = false;
                btnExit.Enabled = false;

                btnAdd.Text = "Хадгалах";
                if (_resource != null) btnAdd.Image = _resource.GetBitmap("navigate_save");

                btnEdit.Text = "Болих";
                if (_resource != null) btnEdit.Image = _resource.GetBitmap("navigate_cancel");
            }
            gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            gridControl1.Enabled = _toggle_flag == 0;
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
        public ucPropPanel()
        {
            InitializeComponent();
        }
        #endregion

        #region Custom Events

        public delegate void delegateEventAdd();
        public event delegateEventAdd EventAdd;
        public void OnEventAdd()
        {
            if (EventAdd != null) EventAdd();
        }

        public delegate void delegateEventEdit();
        public event delegateEventEdit EventEdit;
        public void OnEventEdit()
        {
            if (EventEdit != null) EventEdit();
        }

        public delegate void delegateEventDelete();
        public event delegateEventDelete EventDelete;
        public void OnEventDelete()
        {
            if (EventDelete != null) EventDelete();
        }

        public delegate void delegateEventSave(bool isnew);
        public event delegateEventSave EventSave;
        public void OnEventSave(bool isnew)
        {
            if (EventSave != null) EventSave(isnew);
        }

        public delegate void delegateEventDataChanged();
        public event delegateEventDataChanged EventDataChanged;
        public void OnEventDataChanged()
        {
            if (EventDataChanged != null) EventDataChanged();
        }

        public delegate void delegateEventRowChanged(int rowno);
        public event delegateEventRowChanged EventRowChanged;
        public void OnEventRowChanged(int rowno)
        {
            if (EventRowChanged != null) EventRowChanged(rowno);
        }

        public delegate void delegateEventExit();
        public event delegateEventExit EventExit;
        public void OnEventExit()
        {
            if (EventExit != null) EventExit();
        }

        #endregion

        #region Control Events

        private void frmTempProp_Load(object sender, EventArgs e)
        {
            try
            {
                ToggleButtons();
                btnRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (_toggle_flag == 0)
            {
                FieldLinkSetNewState();
                OnEventAdd();
            }
            else
            {
                int old_flag = _toggle_flag;
                OnEventSave(old_flag == 1);
                FieldLinkSetSaveState();

                btnRefresh_Click(sender, e);
                FieldLinkSetValues();
            }
            this.ResumeLayout(true);
            Application.DoEvents();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (_toggle_flag == 0)
            {
                FieldLinkSetEditState();
                OnEventEdit();
            }
            else
            {
                FieldLinkSetValues();
                FieldLinkSetSaveState();
            }
            this.ResumeLayout(true);
            Application.DoEvents();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnEventDelete();
            btnRefresh_Click(sender, e);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            gridControl1.DataSource = _table;
            OnEventDataChanged();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            OnEventExit();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FieldLinkSetValues();
            OnEventRowChanged(gridView1.FocusedRowHandle);
        }
        private void splitterControl1_DoubleClick(object sender, EventArgs e)
        {
            gridControl1.Visible = !gridControl1.Visible;
        }

        #endregion

        #region Control Managing Methods

        public FieldInfo FieldLinkAdd(string ctrlname, string dbfieldname, string editmask, bool ismandatory, bool iskey)
        {
            FieldInfo fi = new FieldInfo();
            fi.ctrlname = ctrlname;
            fi.dbfieldname = dbfieldname;
            fi.editmask = editmask;
            fi.ismandatory = ismandatory;
            fi.iskey = iskey;

            lock (_hfields.SyncRoot)
            { _hfields.Add(ctrlname, fi); }

            return fi;
        }
        public void FieldLinkRemove(string ctrlname)
        {
            lock (_hfields.SyncRoot)
            { _hfields.Remove(ctrlname); }
        }

        private void FieldLinkSetValues(Control.ControlCollection ctrls)
        {
            if (ctrls != null)
            {
                DataRow dr = gridView1.GetFocusedDataRow();

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
                                        #region Setting control values

                                        string ctrltype = ctrl.GetType().Name;
                                        object value = null;
                                        if (dr != null) value = dr[fi.dbfieldname];
                                        if (value == null || value is DBNull) value = null;
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
                                                if (value == null)
                                                {
                                                    lue.EditValue = DBNull.Value;
                                                }
                                                else
                                                {
                                                    DataTable dt = (DataTable)lue.Properties.DataSource;

                                                    if (dt != null && dt.Columns.Count > 0)
                                                    {
                                                        Type totype = typeof(string);
                                                        if (string.IsNullOrEmpty(lue.Properties.ValueMember))
                                                        {
                                                            totype = dt.Columns[0].DataType;
                                                        }
                                                        else
                                                        {
                                                            totype = dt.Columns[lue.Properties.ValueMember].DataType;
                                                        }
                                                        lue.EditValue = EServ.Shared.Static.ConvToType(totype, value);
                                                    }
                                                    else
                                                    {
                                                        lue.EditValue = DBNull.Value;
                                                    }
                                                }
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
                                                ctrl.Text = Convert.ToString(value);
                                                break;
                                        }

                                        #endregion
                                    }
                                    catch
                                    { }
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
            //this.ResumeLayout(true);
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
                            if (_hfields.ContainsKey(ctrl.Name))
                            {
                                FieldInfo fi = (FieldInfo)_hfields[ctrl.Name];
                                if (fi != null)
                                {
                                    if (ctrl is DevExpress.XtraEditors.BaseControl)
                                    {
                                        ((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.ReadOnly = false;
                                        ctrl.Text = null;

                                        if (fi.ismandatory)
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
            this.SuspendLayout();
            ToggleNew();
            FieldLinkSetNewState(this.Controls);
            this.ResumeLayout(true);
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
                                    ((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.ReadOnly = fi.iskey;
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
            this.SuspendLayout();
            ToggleEdit();
            FieldLinkSetEditState(this.Controls);
            this.ResumeLayout(true);
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
            this.SuspendLayout();
            ToggleSave();
            FieldLinkSetSaveState(this.Controls);
            this.ResumeLayout(true);
        }

        public void FieldLinkSetColumnCaption(int colno, string caption, bool hide)
        {
            if (colno >= 0 && colno < gridView1.Columns.Count)
            {
                gridView1.Columns[colno].Visible = !hide;
                gridView1.Columns[colno].Caption = caption;
            }
        }
        public void FieldLinkSetColumnCaption(int colno, string caption)
        {
            FieldLinkSetColumnCaption(colno, caption, false);
        }

        #endregion
    }
}
