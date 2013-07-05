using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace InfoPos.sales
{
    public partial class frmDiscountChoice : DevExpress.XtraEditors.XtraForm
    {
        public DataTable _cart = new DataTable();

        #region Controls Events
        public frmDiscountChoice()
        {
            InitializeComponent();
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            gridControl1.Resize += gridControl1_Resize;

            Init();
        }

        void gridControl1_Resize(object sender, EventArgs e)
        {
            gridView1.Columns[5].Width = gridControl1.Width - gridView1.Columns[2].Width - 32;
        }

        private void frmDiscountChoice_Load(object sender, EventArgs e)
        {
            
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(e.PrevFocusedRowHandle);
            if (row != null) row["CHECKED"] = false;

            row = gridView1.GetDataRow(e.FocusedRowHandle);
            if (row != null)
            {
                row["CHECKED"] = true;
            }
        }
        #endregion
        #region Public Methods
        public void Init()
        {
            /***********************************************
             * Бараанд давхцсан хөнгөлөлт оруулах тэйбэл
             ***********************************************/
            _cart.TableName = "CART";
            _cart.Columns.Add("KEY", typeof(string));
            _cart.Columns.Add("KEYNAME", typeof(string));
            _cart.Columns.Add("CUSTNO", typeof(decimal));
            _cart.Columns.Add("CHECKED", typeof(bool));
            _cart.Columns.Add("ITEMKEY", typeof(string));
            _cart.Columns.Add("ITEMNAME", typeof(string));

            /***********************************************
             * Талбаруудын мэдээлэл
             ***********************************************/
            gridControl1.DataSource = _cart;
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "key", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Бараа үйлчилгээ");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "CustNo", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Сонго");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Хөнгөлөлт", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Хөнгөлөлт");

            gridView1.Columns[1].GroupIndex = 0;
            gridView1.GroupFormat = "{1}";
            gridView1.RowHeight = 28;

            RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
            ri.ValueChecked = true;
            ri.ValueUnchecked = false;

            gridView1.Columns[2].ColumnEdit = ri;
            gridView1.Columns[2].MaxWidth = 48;
            //gridView1.OptionsView.ColumnAutoWidth = true;
            gridView1.BestFitColumns();

            gridView1.Columns[5].Width = gridControl1.Width - gridView1.Columns[2].Width - 32;
        }
        public void Add(decimal custno, string custname, string prodno, string prodname, string discountkey, string discountdesc)
        {
            string key = string.Format("c{0}_d{1}", custno, discountkey);
            string keyname = string.Format("{0}", custname);

            _cart.Rows.Add(key, keyname, custno, false, discountkey, discountdesc);
        }
        #endregion
    }
}
