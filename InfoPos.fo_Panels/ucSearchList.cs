using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InfoPos.Panels
{
    public partial class ucSearchList : UserControl
    {
        bool _nosearch = false;
        bool _ismanual = false;
        #region Properties

        private int _grouplen = 1;
        public int GroupLength
        {
            get { return _grouplen; }
            set
            {
                if (value >= 1 && value < 10)
                {
                    _grouplen = value;
                }
            }
        }

        private int _keyfieldindex = 0;
        public int KeyFieldIndex
        {
            get { return _keyfieldindex; }
            set
            {
                if (value >= 0 && value < 10)
                {
                    _keyfieldindex = value;
                }
            }
        }

        private DataTable _dt = null;
        public DataTable DataSource
        {
            get { return _dt; }
            set {
                _dt = value;
                DataRefreshGroup();
                DataRefresh("");
            }
        }

        private ISM.Touch.TouchKeyboard _touchkeyboard = null;
        public ISM.Touch.TouchKeyboard TouchKeyboard
        {
            get { return _touchkeyboard; }
            set { _touchkeyboard = value; }
        }

        public DataRow CurrentRow
        {
            get { return gridViewDetail.GetFocusedDataRow(); }
        }

        private string _layoutfilename = "";
        public string LayoutFileName
        {
            get { return _layoutfilename; }
        }

        #endregion

        #region Constructors

        public ucSearchList()
        {
            InitializeComponent();

            gridViewGroup.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewGroup_FocusedRowChanged);
            gridViewGroup.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(gridViewGroup_RowClick);
            gridViewDetail.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewDetail_FocusedRowChanged);
            txtSearch.EditValueChanged += new EventHandler(txtSearch_EditValueChanged);

            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", ISM.Lib.Static.WorkingFolder, this.GetType().Name);

            gridViewGroup.OptionsBehavior.Editable = false;
            gridViewGroup.OptionsView.ShowGroupPanel = false;
            gridViewGroup.OptionsView.ShowIndicator = false;
            gridViewGroup.OptionsView.RowAutoHeight = true;

            gridViewDetail.OptionsBehavior.Editable = false;
            gridViewDetail.OptionsView.ShowGroupPanel = false;
            gridViewDetail.OptionsView.ShowIndicator = false;
            gridViewDetail.OptionsView.RowAutoHeight = true;
            
            if (_touchkeyboard != null)
            {
                _touchkeyboard.AddToKeyboard(txtSearch);
            }
        }

        #endregion

        #region Control Events

        private void ucSearchList_Load(object sender, EventArgs e)
        {
           
        }

        private void gridViewGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }
        private void gridViewGroup_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            GroupClick();
        }
        private void gridViewDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridViewDetail.GetFocusedDataRow();
            if (row != null)
            {
                _nosearch = true;
                txtSearch.EditValue = Convert.ToString(row[_keyfieldindex]);
                _nosearch = false;
            }
            
            OnEventChoose();
        }
        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            if (!_nosearch)
            DataRefresh(Convert.ToString(txtSearch.EditValue));
        }
        
        private void btnPlus_Click(object sender, EventArgs e)
        {
            GroupLength++;
            DataRefreshGroup();
            GroupClick();
        }
        private void btnMinus_Click(object sender, EventArgs e)
        {
            GroupLength--;
            DataRefreshGroup();
            GroupClick();
        }

        #endregion

        #region User Functions

        public void DataRefresh(string startswith = null)
        {
            DataTable dt = null;
            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    var query = from row in _dt.AsEnumerable()
                                where row.Field<string>(_keyfieldindex).StartsWith(startswith)
                                orderby row.Field<string>(_keyfieldindex)
                                select row;
                    if (query != null && query.Count() > 0)
                    {
                        dt = query.CopyToDataTable();
                        gridControlDetail.DataSource = dt;
                        OnEventDataSourceChanged();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (dt == null) gridControlDetail.DataSource = null;
        }
        public void DataRefreshGroup()
        {
            DataTable dt = null;
            dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("desc", typeof(string));

            try
            {
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    var query = from row in _dt.AsEnumerable()
                                group row by row.Field<string>(_keyfieldindex).Substring(0,
                                    row.Field<string>(_keyfieldindex).Length >= _grouplen
                                    ? _grouplen : row.Field<string>(_keyfieldindex).Length
                                ) into d
                                orderby d.Key
                                select NewGroupRow(dt, d.Key, d.Count());

                    if (query != null && query.Count() > 0)
                    {
                        dt = query.CopyToDataTable();
                        gridControlGroup.DataSource = dt;

                        gridViewGroup.Columns[0].Visible = false;
                        gridViewGroup.Columns[1].Caption = "Бүлэг";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (dt == null) gridControlGroup.DataSource = null;
        }
        public void GroupClick()
        {
            DataRow row = gridViewGroup.GetFocusedDataRow();
            if (row != null)
            {
                txtSearch.EditValue = null;
                string s = Convert.ToString(row[0]);
                DataRefresh(s);
            }
        }
        internal DataRow NewGroupRow(DataTable t, string id, int count)
        {
            DataRow r = null;
            try
            {
                r = t.NewRow();
                r[0] = id;
                r[1] = string.Format("{0} ({1})", id, count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return r;
        }

        public void SelectedControl()
        {
            if (!txtSearch.Focus())
            {
                txtSearch.SelectAll();
            }
        }
        public void SaveLayout()
        {
            ISM.Template.FormUtility.GridLayoutSave(gridViewDetail, _layoutfilename);
        }

        #endregion

        #region User Events

        public delegate void delegateEventChoose(DataRow currentrow);
        public event delegateEventChoose EventChoose;
        public void OnEventChoose()
        {
            try
            {
                if (EventChoose != null)
                {
                    DataRow row = gridViewDetail.GetFocusedDataRow();
                    EventChoose(row);
                }
            }
            catch
            { }
        }

        public delegate void delegateEventDataSourceChanged(DevExpress.XtraGrid.Views.Grid.GridView gridView);
        public event delegateEventDataSourceChanged EventDataSourceChanged;
        public void OnEventDataSourceChanged()
        {
            try
            {
                if (EventDataSourceChanged != null)
                {
                    EventDataSourceChanged(gridViewDetail);
                }
            }
            catch
            { }
        }

        #endregion

    }
}
