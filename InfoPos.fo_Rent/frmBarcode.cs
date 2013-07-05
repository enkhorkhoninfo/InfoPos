using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;

namespace InfoPos.Rent
{
    public partial class frmBarcode : Form
    {
        #region Internal variables

        DataTable _dtSeriesInv = null;
        DataTable _dtSeries = null;
        public DataTable TableSeries
        {
            get { return _dtSeries; }
            set { _dtSeries = value; }
        }
        
        DataTable _dtInv = null;
        public DataTable TableInv
        {
            get { return _dtInv; }
            set { _dtInv = value; }
        }

        string _barcode = null;
        public string BarCode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }
        string _invid = null;
        public string InvId
        {
            get { return _invid; }
            set { _invid = value; }
        }
        string _invname = null;
        public string InvName
        {
            get { return _invname; }
            set { _invname = value; }
        }

        int _searchminlength = 1;
        public int SearchMinLength
        {
            get { return _searchminlength; }
            set {
                if (value > 0 && value < 20) _searchminlength = value;
            }
        }

        #endregion
        #region Control events
        public frmBarcode()
        {
            InitializeComponent();
            #region Events
            this.gridViewDetail.FocusedRowChanged += gridViewDetail_FocusedRowChanged;
            this.gridViewDetail.DataSourceChanged += gridViewDetail_DataSourceChanged;
            this.txtSearch.EditValueChanged += txtSearch_EditValueChanged;
            this.KeyDown += frmBarcode_KeyDown;
            #endregion
            #region GridView1 Formatting

            gridViewDetail.OptionsBehavior.ReadOnly = true;
            gridViewDetail.OptionsBehavior.Editable = false;
            gridViewDetail.OptionsCustomization.AllowGroup = false;
            gridViewDetail.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridViewDetail.OptionsView.ColumnAutoWidth = true;
            //gridView1.OptionsView.ShowAutoFilterRow = false;
            gridViewDetail.OptionsView.ShowIndicator = false;
            gridViewDetail.OptionsView.ShowGroupPanel = false;
            gridViewDetail.OptionsView.ShowIndicator = false;
            gridViewDetail.OptionsView.RowAutoHeight = true;
            gridViewDetail.Appearance.Row.Font = new Font("Tahoma", 10.0F);
            gridViewDetail.RowHeight = 28;

            // зурган багана нэмж оруулахад энэ үзэгдлийг зарлаж дотор нь зургаа set хийнэ.
            //gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(gridView1_CustomUnboundColumnData);
            //gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gridView1_RowCellClick);
            //_layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
            #endregion
        }

        void frmBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab
                || e.KeyCode == Keys.ShiftKey
                || e.Alt
                || e.Control
                || e.SuppressKeyPress
                ) return;

            if (this.ActiveControl != null
                && this.ActiveControl.Parent != null
                && this.ActiveControl.Parent.Name != "txtSearch")
            {
                txtSearch.SelectAll();
                txtSearch.SelectedText = new string((char)e.KeyValue, 1);
                txtSearch.Select();
            }
        }
        void frmDeliver_Load(object sender, EventArgs e)
        {
            txtInvName.EditValue = _invname;

            if (_dtSeries != null)
            {
                var q = from r in _dtSeries.AsEnumerable()
                        where Static.ToStr(r["INVID"]) == _invid
                        select r;

                if (q != null && q.Count() > 0)
                {
                    _dtSeriesInv = q.CopyToDataTable();
                }
            }
        }

        void gridViewDetail_DataSourceChanged(object sender, EventArgs e)
        {
            SetInvName();
        }
        void gridViewDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetInvName();
        }
        void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            string value = Static.ToStr(txtSearch.EditValue);
            if (value.Length >= _searchminlength)
            {
                GetSeriesList(1, value);
            }
        }

        #endregion
        #region User Functions

        public void GetSeriesList(int fieldindex, string startswith = null)
        {
            DataTable dt = null;
            try
            {
                if (_dtSeriesInv != null && _dtSeriesInv.Rows.Count > 0)
                {
                    var query = from row in _dtSeriesInv.AsEnumerable()
                                where row.Field<string>(fieldindex).StartsWith(startswith)
                                orderby row.Field<string>(fieldindex)
                                select row;
                    if (query != null && query.Count() > 0)
                    {
                        dt = query.CopyToDataTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            gridControlDetail.DataSource = null;
            gridControlDetail.DataSource = dt;

            ISM.Template.FormUtility.Column_SetCaption(ref gridViewDetail, 0, "Код", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridViewDetail, 1, "Баркод");
        }
        public void GetInvName(string invid)
        {
            //if (_dtInv == null || string.IsNullOrEmpty(invid)) return;

            //_invid = "";
            //_invname = "";

            //DataRow[] rows = _dtInv.Select(string.Format("INVID='{0}'", invid));
            //if (rows != null & rows.Length > 0)
            //{
            //    _invid = Static.ToStr(rows[0]["INVID"]);
            //    _invname = Static.ToStr(rows[0]["NAME"]);
            //}
            //txtInvName.EditValue = _invname;
        }
        public void SetInvName()
        {
            try
            {
                //DataRow r = gridViewDetail.GetFocusedDataRow();
                //if (r == null) return;

                //_barcode = Static.ToStr(r["BARCODE"]);
                //_invid = Static.ToStr(r["INVID"]);

                //GetInvName(_invid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        private void btnChoose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

    }
}
