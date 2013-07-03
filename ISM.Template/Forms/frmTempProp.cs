using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISM.Template
{
    public partial class frmTempProp : Form
    {
        private Hashtable _hfields = new Hashtable();
        private ArrayList _afields = new ArrayList();

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
                    btnRemove.Image = _resource.GetBitmap("navigate_delete");
                    btnRefresh.Image = _resource.GetBitmap("navigate_refresh");
                    btnExit.Image = _resource.GetBitmap("navigate_home");
                }
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

        public frmTempProp()
        {
            InitializeComponent();
            
            try
            {
                FormUtility.SetFormatGrid(ref gridView1, false);
            }
            catch
            { }
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

        public delegate void delegateEventSave(bool isnew, ref bool cancel);
        public event delegateEventSave EventSave;
        public void OnEventSave(bool isnew, ref bool cancel)
        {
            if (EventSave != null) EventSave(isnew, ref cancel);
        }

        public delegate void delegateEventReject();
        public event delegateEventReject EventReject;
        public void OnEventReject()
        {
            if (EventReject != null) EventReject();
        }

        public delegate void delegateEventRefresh(ref DataTable dt);
        public event delegateEventRefresh EventRefresh;
        public void OnEventRefresh(ref DataTable dt)
        {
            if (EventRefresh != null) EventRefresh(ref dt);
        }

        public delegate void delegateEventRefreshAfter();
        public event delegateEventRefreshAfter EventRefreshAfter;
        public void OnEventRefreshAfter()
        {
            if (EventRefreshAfter != null) EventRefreshAfter();
        }

        public delegate void delegateEventRowChanged(int rowno);
        public event delegateEventRowChanged EventRowChanged;
        public void OnEventRowChanged(int rowno)
        {
            if (EventRowChanged != null) EventRowChanged(rowno);
        }

        public delegate void delegateEventExit(ref bool cancel);
        public event delegateEventExit EventExit;
        public void OnEventExit(ref bool cancel)
        {
            if (EventExit != null) EventExit(ref cancel);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (_toggle_flag == 0)
            {
                bool cancel = false;
                OnEventAdd(ref cancel);

                if (!cancel) FieldLinkSetNewState();

                OnEventAddAfter();
            }
            else
            {
                int old_flag = _toggle_flag;

                bool cancel = false;
                OnEventSave(old_flag == 1, ref cancel);

                if (!cancel)
                {
                    FieldLinkSetSaveState();

                    btnRefresh_Click(sender, e);
                    FieldLinkSetValues();
                }
            }
            this.ResumeLayout(true);
            Application.DoEvents();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (_toggle_flag == 0)
            {
                bool cancel = false;
                OnEventEdit(ref cancel);
                if (!cancel) FieldLinkSetEditState();
                
            }
            else
            {
                FieldLinkSetValues();
                FieldLinkSetSaveState();

                OnEventReject();
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
            FieldDataRefresh();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (_toggle_flag != 0)
            {
                DialogResult d = MessageBox.Show("Цонхыг хаахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
            }

            bool cancel = false;
            OnEventExit(ref cancel);
            if (!cancel) this.Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FieldLinkSetValues();
            OnEventRowChanged(gridView1.FocusedRowHandle);
        }
        #endregion

        #region Control Managing Methods

        public FieldInfo FieldLinkAdd(string ctrlname, string dbfieldname, string editmask, bool ismandatory, bool iskey, bool isdisable)
        {
            FieldInfo fi = new FieldInfo();
            fi.ctrlname = ctrlname;
            fi.dbfieldname = dbfieldname;
            fi.editmask = editmask;
            fi.ismandatory = ismandatory;
            fi.iskey = iskey;
            fi.isdisable = isdisable;

            lock (_hfields.SyncRoot)
            {
                _hfields.Add(ctrlname, fi);
                _afields.Add(fi);
            }

            return fi;
        }
        public FieldInfo FieldLinkAdd(string ctrlname, string dbfieldname, string editmask, bool ismandatory, bool iskey)
        {
            return FieldLinkAdd(ctrlname, dbfieldname, editmask, ismandatory, iskey, false);
        }
        public void FieldLinkRemove(string ctrlname)
        {
            lock (_hfields.SyncRoot)
            {
                FieldInfo fi = (FieldInfo)_hfields[ctrlname];
                _hfields.Remove(ctrlname);
            }
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
                                                    DataTable dt = null;
                                                    if (lue.Properties.DataSource is DataView)
                                                    {
                                                        DataView dv = (DataView)lue.Properties.DataSource;
                                                        dt = dv.Table;
                                                    }
                                                    else
                                                    {
                                                        dt = (DataTable)lue.Properties.DataSource;
                                                    }
                                                    if (dt != null && dt.Columns.Count > 0)
                                                    {
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
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                                }
                            }
                        }
                        if (ctrl.Controls != null && ctrl.Controls.Count>0) FieldLinkSetValues(ctrl.Controls);
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
                                        ((DevExpress.XtraEditors.BaseEdit)ctrl).Properties.ReadOnly = (fi != null && fi.isdisable);
                                        ((DevExpress.XtraEditors.BaseEdit)ctrl).EditValue = null;
                                        
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

        private void FieldValidate(Control.ControlCollection ctrls, ref int number, ref Control first, ref ArrayList mandatories)
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
                                        if (fi.ismandatory && (edit.EditValue == null || edit.EditValue == DBNull.Value || Convert.ToString(edit.EditValue) == ""))
                                        {
                                            number++;
                                            if (first == null) first = ctrl;
                                            mandatories.Add(string.Format("{0}. {1}", number, ((DevExpress.XtraEditors.BaseEdit)ctrl).ToolTipTitle));
                                        }
                                    }
                                }
                            }
                        }
                        if (ctrl.Controls != null) FieldValidate(ctrl.Controls, ref number, ref first, ref mandatories);
                    }
                }
            }
        }
        public bool FieldValidate(ref string desc, ref Control first)
        {
            ArrayList ar = new ArrayList();
            int number = 0;

            FieldValidate(this.Controls, ref number, ref first, ref ar);

            StringBuilder sb = new StringBuilder();
            foreach (string i in ar)
            {
                if (sb.Length > 0) sb.AppendLine();
                sb.Append(i);
            }
            desc = sb.ToString();
            return (ar.Count <= 0);
        }

        public void FieldDataRefresh()
        {
            DataTable dt = new DataTable();
            OnEventRefresh(ref dt);

            gridControl1.DataSource = null;
            gridControl1.DataSource = dt;
            FieldLinkSetValues();

            OnEventRefreshAfter();
        }

        #endregion

        private void splitterControl1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

    }
}

