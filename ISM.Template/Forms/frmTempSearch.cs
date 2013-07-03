using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISM.Template
{
    public partial class frmTempSearch : Form
    {
        #region Custom Properties

        private DataRow _row = null;
        [DefaultValue(null), Browsable(false)]
        public DataRow SelectedRow
        {
            get { return _row; }
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
                    btnSearch.Image = _resource.GetBitmap("button_find");
                    btnSelect.Image = _resource.GetBitmap("button_select");
                    btnExit.Image = _resource.GetBitmap("navigate_home");
                }
            }
        }
        #endregion

        #region Custom Events

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

        public delegate void delegateEventSelected(DataRow selectedrow);
        public event delegateEventSelected EventSelected;
        public void OnEventSelected(DataRow selectedrow)
        {
            if (EventSelected != null) EventSelected(selectedrow);
        }
        
        #endregion

        #region Constractor

        public frmTempSearch()
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

        #region Control Events

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            OnEventRefresh(ref dt);
            gridControl1.DataSource = dt;
            OnEventRefreshAfter();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            _row = gridView1.GetFocusedDataRow();
            if (_row != null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                OnEventSelected(_row);
                //this.Close();
            }
        }
        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            _row = gridView1.GetFocusedDataRow();
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            btnSelect_Click(sender, e);
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

        #endregion

    }
}
