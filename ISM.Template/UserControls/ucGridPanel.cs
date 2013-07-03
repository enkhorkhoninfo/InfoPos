using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Persistent;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

using EServ.Shared;

namespace ISM.Template
{
    public partial class ucGridPanel : UserControl
    {
        Hashtable _hfields = new Hashtable();
        ArrayList _afields = new ArrayList();
        int[] _merge_flag = new int[256];

        #region Custom Properties

        [DefaultValue(null), Browsable(false)]
        public DevExpress.XtraGrid.Views.Grid.GridView GridView
        {
            get { return gridView1; }
        }

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
                gridView1.PopulateColumns();

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

                    btnFirst.Image = _resource.GetBitmap("paging_first");
                    btnPrev.Image = _resource.GetBitmap("paging_prev");
                    btnNext.Image = _resource.GetBitmap("paging_next");
                    btnLast.Image = _resource.GetBitmap("paging_last");

                    btnExcel.Image = _resource.GetBitmap("paging_export");
                    btnFilter.Image = _resource.GetBitmap("paging_filter");
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

        private bool _visiblefilter = false;
        public bool VisibleFilter
        {
            get { return _visiblefilter; }
            set
            {
                _visiblefilter = value;
                btnFilter.Checked = value;

                gridView1.OptionsView.ShowAutoFilterRow = value;
                gridView1.OptionsView.ShowFilterPanelMode = value ?
                    DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
                    :
                    DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            }
        }

        private bool _browsable = false;
        public bool Browsable
        {
            get { return _browsable; }
            set { _browsable = value; }
        }

        private int _pageindex = 0;
        [DefaultValue(null), Browsable(false)]
        public int PageIndex
        {
            get { return _pageindex; }
        }

        private int _pagerows = 10;
        [DefaultValue(20), Browsable(true)]
        public int PageRows
        {
            get { return _pagerows; }
            set { _pagerows = value; }
        }

        private int _pagecount = 0;
        [DefaultValue(0), Browsable(false)]
        public int PageCount
        {
            get { return _pagecount; }
        }

        #endregion

        #region Custom Events

        public delegate void delegateEventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount);
        public event delegateEventFindPaging EventFindPaging;
        public void OnEventFindPaging(int pageindex)
        {
            if (EventFindPaging != null)
            {
                object[] values = FindItemGetValueList();
                
                int pagecount = 0;
                EventFindPaging(values, pageindex, _pagerows, ref pagecount);

                if (pagecount > _pagecount) _pagecount = pagecount;
                //if (pageindex < _pagecount) _pageindex = pageindex;
                _pageindex = pageindex;
                lblPage.Text = string.Format("{0}/{1}+", _pageindex + 1, _pagecount);
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
        }

        #endregion

        #region Control Events

        private void ucGridPanel_Load(object sender, EventArgs e)
        {
            try
            {
                #region Control Inits

                //vGridControl1.ShowButtonMode = DevExpress.XtraVerticalGrid.ShowButtonModeEnum.ShowAlways;
                //vGridControl1.OptionsBehavior.DragRowHeaders = false;
                //vGridControl1.OptionsView.FixRowHeaderPanelWidth = true;

                ucParameterPanel1.ShowDescription = false;

                FormUtility.SetFormatGrid(ref gridView1, false);

                #endregion

                #region Declare Events

                gridControl1.FocusedViewChanged += new DevExpress.XtraGrid.ViewFocusEventHandler(gridControl1_FocusedViewChanged);
                gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridView1_RowClick);
                gridControl1.DoubleClick += new EventHandler(gridControl1_DoubleClick);
                gridView1.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(gridView1_CellMerge);

                #endregion
            }
            catch
            {
            }
        }

        void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            _row = gridView1.GetFocusedDataRow();
        }

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            _row = gridView1.GetFocusedDataRow();
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            _row = gridView1.GetFocusedDataRow();
            OnEventSelected(_row);
        }
        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.AbsoluteIndex < 0 || e.Column.AbsoluteIndex >= _merge_flag.Length) return;

            if (_merge_flag[e.Column.AbsoluteIndex] == 1)
            {
                e.Column.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

                //object value1 = gridView1.GetRowCellValue(e.RowHandle1, e.Column);
                //object value2 = gridView1.GetRowCellValue(e.RowHandle2, e.Column);
                //if (value1.Equals(value2))
                //{
                //    e.Merge = true;
                //    e.Handled = true;
                //}
                //else
                //{
                //    e.Merge = false;
                //    e.Handled = false;
                //}

            }
            else
            {
                e.Column.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            }
        }
        private void splitterControl1_DoubleClick(object sender, EventArgs e)
        {
            VisibleFind = !VisibleFind;
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            MoveFirst();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            FieldFindClear();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            MoveFirst();
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            MovePrev();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveNext();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            MoveLast();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        private void btnFilter_CheckedChanged(object sender, EventArgs e)
        {
            VisibleFilter = btnFilter.Checked;
        }

        #endregion

        #region Find Item Methods

        #region Old Functions

        public void FieldLinkSetColumnCaption(int colno, string caption, bool hide)
        {
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, colno, caption, hide);
        }
        public void FieldLinkSetColumnCaption(int colno, string caption)
        {
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, colno, caption);
        }

        public void FieldFindAdd(string id, string caption, Type type, object defaultvalue)
        {
            #region Converting type to dynamic parameter type
            DynamicParameterType dtype = DynamicParameterType.Text;
            switch (type.Name)
            {
                case "String":
                    dtype = DynamicParameterType.Text;
                    break;
                case "DateTime":
                    dtype = DynamicParameterType.Date;
                    break;
                case "List":
                case "ArrayList":
                    dtype = DynamicParameterType.List;
                    break;
                default:
                    dtype = DynamicParameterType.Decimal;
                    break;
            }
            #endregion

            ucParameterPanel1.ItemAdd(id, caption, "", dtype, false);
        }
        public void FieldFindRemove(string id)
        {
            ucParameterPanel1.ItemRemove(id);
        }
        public void FieldFindRemoveAll()
        {
            ucParameterPanel1.ItemRemoveAll();
        }
        public void FieldFindClear()
        {
            ucParameterPanel1.ItemClearAll();
        }
        public void FieldFindRefresh()
        {
            ucParameterPanel1.ItemListRefresh();
        }
        public object[] FieldFindGetList()
        {
            return ucParameterPanel1.ItemGetValueList();
        }

        #endregion

        public DynamicParameterItem FindItemAdd(string Id, string Name, string Name2, string Value, DynamicParameterType ValueType, int ValueLength, object ValueDefault, bool Mandatory, string EditMask, string Description, string DictId, bool DictEditable, string DictValueField, string DictDescField, int Orderno)
        {
            return ucParameterPanel1.ItemAdd(Id, Name, Name2, Value, ValueType, ValueLength, ValueDefault, Mandatory, EditMask, Description, DictId, DictEditable, DictValueField, DictDescField, Orderno);
        }
        public DynamicParameterItem FindItemAdd(string Id, string Name, string Value, DynamicParameterType ValueType, int ValueLength, object ValueDefault, bool Mandatory, string EditMask, string Description)
        {
            return ucParameterPanel1.ItemAdd(Id, Name, Value, ValueType, ValueLength, ValueDefault, Mandatory, EditMask, Description);
        }
        public DynamicParameterItem FindItemAdd(string Id, string Name, string Value, DynamicParameterType ValueType, bool Mandatory, string EditMask, string Description)
        {
            return ucParameterPanel1.ItemAdd(Id, Name, Value, ValueType, Mandatory, EditMask, Description);
        }
        public DynamicParameterItem FindItemAdd(string Id, string Name, string Value, DynamicParameterType ValueType, int ValueLength, bool Mandatory)
        {
            return ucParameterPanel1.ItemAdd(Id, Name, Value, ValueType, ValueLength, Mandatory);
        }
        public DynamicParameterItem FindItemAdd(string Id, string Name, string Value, DynamicParameterType ValueType, bool Mandatory)
        {
            return ucParameterPanel1.ItemAdd(Id, Name, Value, ValueType, Mandatory);
        }

        public void FindItemRemove(string id)
        {
            ucParameterPanel1.ItemRemove(id);
        }
        public void FindItemClear()
        {
            ucParameterPanel1.ItemClearAll();
        }
        public void FindItemRefreshList()
        {
            ucParameterPanel1.ItemListRefresh();
        }

        public DynamicParameterItem FindItemGet(string id)
        {
            return ucParameterPanel1.ItemGet(id);
        }
        public object FindItemGetValue(string id)
        {
            return ucParameterPanel1.ItemGetValue(id);
        }
        public object FindItemGetValue(int index)
        {
            return ucParameterPanel1.ItemGetValue(index);
        }
        public object[] FindItemGetValueList()
        {
            return ucParameterPanel1.ItemGetValueList();
        }
        public List<DynamicParameterItem> FindItemGetList()
        {
            return ucParameterPanel1.ItemGetList();
        }

        public void FindItemSetValue(string id, object value)
        {
            ucParameterPanel1.ItemSetValue(id, value);
        }

        #endregion

        #region Find Item List Methods

        public void FindItemSetList(string Id, DataTable Table, string ValueField, string NameField, string Filter, int[] HiddenColumns)
        {
            ucParameterPanel1.ItemSetList(Id, Table, ValueField, NameField, Filter, HiddenColumns);
        }
        public void FindItemSetList(string Id, DataTable Table, string ValueField, string NameField)
        {
            ucParameterPanel1.ItemSetList(Id, Table, ValueField, NameField);
        }
        public void FindItemSetList(string Id, DataTable Table, string ValueField)
        {
            ucParameterPanel1.ItemSetList(Id, Table, ValueField);
        }
        public void FindItemSetList(string Id, object Value, object Name)
        {
            ucParameterPanel1.ItemSetList(Id, Value, Name);
        }
        public Result FindItemSetListFromDictionary(CUser.Remote remote)
        {
            return ucParameterPanel1.ItemSetListFromDictionary(remote);
        }

        #endregion
       
        #region Method

        public DevExpress.XtraGrid.Columns.GridColumn GetColumn(int index)
        {
            DevExpress.XtraGrid.Columns.GridColumn col = null;
            if (index >= 0 && index < gridView1.Columns.Count)
                col = gridView1.Columns[index];
            return col;
        }
        public void MergeColumn(int index, bool merge)
        {
            //if (index >= 0 && index < _merge_flag.Length && index < gridView1.Columns.Count)
            //{
            //    if (merge)
            //    {
            //        //gridView1.Columns[index].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            //        _merge_flag[index] = 1;
            //    }
            //    else
            //    {
            //        //gridView1.Columns[index].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.Default;
            //        _merge_flag[index] = 0;
            //    }

            //    //gridView1.OptionsView.AllowCellMerge = true;
            //}
        }

        #endregion

        #region Navigation Methods

        public void MoveFirst()
        {
            OnEventFindPaging(0);
        }
        public void MovePrev()
        {
            if (_table == null || _pageindex <= 0)
            {
                MoveFirst();
            }
            else
            {
                OnEventFindPaging(_pageindex - 1);
            }
        }
        public void MoveNext()
        {
            if (_table == null || _pagecount <= 0)
            {
                MoveFirst();
            }
            else if (_pageindex < _pagecount)
            {
                OnEventFindPaging(_pageindex + 1);
            }
            else if (_pagecount > 0)
            {
                OnEventFindPaging(_pageindex);
            }
        }
        public void MoveLast()
        {
            if (_table == null || _pagecount <= 0)
            {
                MoveFirst();
            }
            else
            {
                OnEventFindPaging(_pagecount - 1);
            }
        }
        public void ExportToExcel()
        {
            try
            {
                SaveFileDialog fdlg = new SaveFileDialog();
                fdlg.Title = "Хүснэгт хадгалах";
                fdlg.Filter = "Excel 97-2003 Workbook (*.xls)|*.xls|Excel Workbook (*.xlsx)|*.xlsx";
                DialogResult res = fdlg.ShowDialog();
                if (res == DialogResult.OK)
                {
                    if (fdlg.FilterIndex == 0)
                    {
                        DevExpress.XtraPrinting.XlsExportOptions xls = new DevExpress.XtraPrinting.XlsExportOptions();
                        xls.ShowGridLines = true;
                        xls.ExportMode = DevExpress.XtraPrinting.XlsExportMode.SingleFile;
                        xls.Suppress65536RowsWarning = true;
                        xls.Suppress256ColumnsWarning = true;
                        xls.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                        gridView1.ExportToXls(fdlg.FileName, xls);
                    }
                    else
                    {
                        DevExpress.XtraPrinting.XlsxExportOptions xlsx = new DevExpress.XtraPrinting.XlsxExportOptions();
                        xlsx.ShowGridLines = true;
                        xlsx.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;
                        xlsx.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                        gridView1.ExportToXlsx(fdlg.FileName, xlsx);
                    }
                    System.Diagnostics.Process.Start(fdlg.FileName);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Хүснэгт хадгалахад алдаа гарлаа.\r\n{0}", ex.Message)
                    , ""
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Warning
                    );
            }
        }

        #endregion
    }
}
