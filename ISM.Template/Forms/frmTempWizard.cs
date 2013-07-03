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
    public partial class frmTempWizard : Form
    {
        private Hashtable _hfields = new Hashtable();
        private DataSet _dataset = null;

        #region Property

        [DefaultValue(0), Browsable(false)]
        public int PageIndex
        { get { return xtraTabControl1.SelectedTabPageIndex; } }

        [DefaultValue(0), Browsable(false)]
        public int PageCount
        { get { return xtraTabControl1.TabPages.Count; } }

        private int _previndex = 0;
        public int PrevIndex
        { get { return _previndex; } }

        private int _toggle_flag = 0; // 0-Normal, 1-New, 2-Editing
        [DefaultValue(0), Browsable(false)]
        public int ToggleFlag
        { get { return _toggle_flag; } }

        [DefaultValue(null), Browsable(false)]
        public DataSet DataSource
        {
            get { return _dataset; }
            set
            {
                _dataset = value;
                FieldLinkSetValues();
            }
        }

        #endregion

        #region Custom Events

        public delegate void delegateEventFinalize();
        public event delegateEventFinalize EventFinalize;
        public void OnEventFinalize()
        {
            if (EventFinalize != null) EventFinalize();
        }

        public delegate void delegateEventTabChanged(int selected,int previous, int pagecount);
        public event delegateEventTabChanged EventTabChanged;
        public void OnEventTabChanged()
        {
            if (EventTabChanged != null) 
                EventTabChanged(
                    xtraTabControl1.SelectedTabPageIndex
                    , _previndex
                    , xtraTabControl1.TabPages.Count
                    );
        }

        public delegate void delegateEventExit();
        public event delegateEventExit EventExit;
        public void OnEventExit()
        {
            if (EventExit != null) EventExit();
        }

        #endregion
        
        #region Toggle Internal Methods

        private void ToggleButtons()
        {
            btnPrev.Enabled = xtraTabControl1.SelectedTabPageIndex > 0;
            btnNext.Enabled = xtraTabControl1.SelectedTabPageIndex < xtraTabControl1.TabPages.Count - 1;
            btnFinish.Enabled = xtraTabControl1.SelectedTabPageIndex == xtraTabControl1.TabPages.Count - 1;
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

        public frmTempWizard()
        {
            InitializeComponent();
            xtraTabControl1.SelectedPageChanging += new DevExpress.XtraTab.TabPageChangingEventHandler(xtraTabControl1_SelectedPageChanging);
            xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl1_SelectedPageChanged);

            ToggleButtons();
        }

        #endregion

        #region Control Events

        void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            ToggleButtons();
            OnEventTabChanged();
        }
        void xtraTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            int p1 = xtraTabControl1.TabPages.IndexOf(e.Page);
            int p2 = xtraTabControl1.TabPages.IndexOf(e.PrevPage);

            if (Math.Abs(p1 - p2) == 1) _previndex = p2;
            else e.Cancel = true;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex > 0) xtraTabControl1.SelectedTabPageIndex--;
            ToggleButtons();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex < xtraTabControl1.TabPages.Count - 1) xtraTabControl1.SelectedTabPageIndex++;

            ToggleButtons();
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            FieldLinkSetSaveState();
            OnEventFinalize();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            OnEventExit();
        }

        #endregion

        #region Control Managing Methods

        public FieldInfo FieldLinkAdd(string ctrlname, int tableindex, string dbfieldname, string editmask, bool ismandatory, bool iskey)
        {
            FieldInfo fi = new FieldInfo();
            fi.ctrlname = ctrlname;
            fi.tableindex = tableindex;
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
            if (ctrls != null && _dataset != null)
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
                                        #region Finding column value

                                        object value = null;
                                        if (fi.tableindex >= 0 && fi.tableindex < _dataset.Tables.Count)
                                        {
                                            DataTable dt = _dataset.Tables[fi.tableindex];

                                            if (dt.Columns.Contains(fi.dbfieldname))
                                                if (dt.Rows.Count > 0)
                                                {
                                                    DataRow dr = dt.Rows[0];
                                                    value = dr[fi.dbfieldname];
                                                }
                                        }

                                        #endregion

                                        #region Setting control values

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
                                            case "LookUpEdit":
                                            case "ComboBoxEdit":
                                            case "CheckedComboBoxEdit":
                                                if (!(value is DBNull))
                                                {
                                                    ctrl.Text = Convert.ToString(value);
                                                }
                                                break;
                                            case "CheckEdit":
                                                ((DevExpress.XtraEditors.CheckEdit)ctrl).Checked = EServ.Shared.Static.ToBool(value);
                                                break;
                                            case "PictureEdit":
                                                if (!(value is DBNull))
                                                {
                                                    using (MemoryStream ms = new MemoryStream((byte[])value))
                                                    {
                                                        ((DevExpress.XtraEditors.PictureEdit)ctrl).Image = System.Drawing.Image.FromStream(ms);
                                                    }
                                                }
                                                break;
                                            case "ImageEdit":
                                                if (!(value is DBNull))
                                                {
                                                    using (MemoryStream ms = new MemoryStream((byte[])value))
                                                    {
                                                        ((DevExpress.XtraEditors.ImageEdit)ctrl).Image = System.Drawing.Image.FromStream(ms);
                                                    }
                                                }
                                                break;
                                            default:
                                                ctrl.Text = EServ.Shared.Static.ToStr(value);
                                                break;
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
            this.SuspendLayout();
            FieldLinkSetValues(this.Controls);
            this.ResumeLayout(true);
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
        }
        public void FieldLinkSetColumnCaption(int colno, string caption)
        {
            FieldLinkSetColumnCaption(colno, caption, false);
        }

        #endregion

    }
}
