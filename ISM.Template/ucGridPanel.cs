using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Persistent;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

namespace ISM.Template
{
    public partial class ucGridPanel : UserControl
    {
        Hashtable _hfields = new Hashtable();
        ArrayList _afields = new ArrayList();

        #region Custom Properties

        private DataRow _row = null;
        [DefaultValue(null), Browsable(false)]
        public DataRow SelectedRow
        {
            get { return _row; }
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
                    btnFind.Image = _resource.GetBitmap("button_find");
                    btnClear.Image = _resource.GetBitmap("button_cut");
                }
            }
        }

        private bool _visiblefind = false;
        public bool VisibleFind
        {
            get { return _visiblefind; }
            set
            {
                _visiblefind = value;
                groupControl1.Visible = value;
                //splitterControl1.Visible = value;
            }
        }
        
        #endregion

        #region Custom Events

        public delegate void delegateEventFind(object[] values);
        public event delegateEventFind EventFind;
        public void OnEventFind()
        {
            if (EventFind != null)
            {
                object[] values = FieldFindGetList();
                EventFind(values);
            }
        }

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

        #endregion

        #region Constractor

        public ucGridPanel()
        {
            InitializeComponent();

            vGridControl1.ShowButtonMode = DevExpress.XtraVerticalGrid.ShowButtonModeEnum.ShowAlways;
            vGridControl1.OptionsBehavior.DragRowHeaders = false;
            vGridControl1.OptionsView.FixRowHeaderPanelWidth = true;

            gridControl1.FocusedViewChanged +=new DevExpress.XtraGrid.ViewFocusEventHandler(gridControl1_FocusedViewChanged);
            gridControl1.DoubleClick+=new EventHandler(gridControl1_DoubleClick);

            FormUtility.SetFormatGrid(ref gridView1, false);
        }

        #endregion

        #region Control Events

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            _row = gridView1.GetFocusedDataRow();
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //_row = gridView1.GetFocusedDataRow();
            //OnEventSelected(_row);
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            _row = gridView1.GetFocusedDataRow();
            OnEventSelected(_row);
        }
        private void splitterControl1_DoubleClick(object sender, EventArgs e)
        {
            VisibleFind = !VisibleFind;
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            OnEventFind();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            FieldFindClear();
        }

        #endregion

        #region Control Managing Methods

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
        public void FieldFindAdd(string id, string caption, Type type, object defaultvalue)
        {
            FindValueItem fi = new FindValueItem();
            fi._id = id;
            fi._caption = caption;
            fi._valuetype = type;
            fi._value = defaultvalue;

            lock (_hfields.SyncRoot)
            {
                _hfields[id] = fi;
                _afields.Add(fi);
            }
        }
        public void FieldFindRemove(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                lock (_hfields.SyncRoot)
                {
                    FindValueItem fi = (FindValueItem)_hfields[id];
                    _hfields.Remove(id);
                    if (fi != null) _afields.Remove(fi);
                }
            }
        }
        public void FieldFindClear()
        {
            _hfields.Clear();
            _afields.Clear();
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                vGridControl1.Rows[i].Properties.Value = null;
            }
        }

        public void FieldFindRefresh()
        {
            vGridControl1.Rows.Clear();
            foreach (FindValueItem fi in _afields)
            {
                EditorRow erow = new EditorRow();
                erow.Properties.Caption = fi._caption;

                switch (fi._valuetype.Name)
                {
                    case "String":
                        erow.Properties.RowEdit = vGridControl1.RepositoryItems[0];
                        break;
                    case "DateTime":
                        erow.Properties.RowEdit = vGridControl1.RepositoryItems[2];
                        break;
                    default:
                        erow.Properties.RowEdit = vGridControl1.RepositoryItems[1];
                        break;
                }
                erow.Properties.Value = fi._value;
                erow.Name = fi._id;
                vGridControl1.Rows.Add(erow);
            }
        }
        public object FieldFindGetValue(string id)
        {
            object value = null;
            BaseRow row = vGridControl1.Rows.GetRowByFieldName(id);
            if (row != null) value = row.Properties.Value;
            return value;
        }
        public object[] FieldFindGetList()
        {
            ArrayList ar = new ArrayList();
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                ar.Add(vGridControl1.Rows[i].Properties.Value);
            }
            return ar.ToArray();
        }

        #endregion
    }

    internal class FindValueItem
    {
        #region Properties

        internal string _id;
        public string Id
        {
            get { return _id; }
        }

        internal string _caption;
        public string Caption
        {
            get { return _caption; }
        }

        internal Type _valuetype;
        public Type ValueType
        {
            get { return _valuetype; }
        }

        internal object _value;
        public object Value
        {
            get { return _valuetype; }
        }

        #endregion
    }
}
