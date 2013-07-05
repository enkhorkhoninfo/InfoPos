using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

using EServ.Shared;

namespace InfoPos.fo_panels
{
    public partial class ucSalesProductList : UserControl
    {
        /* Шаардлага
        // - Тухайн control-ийнхоо хэмжээнд дүүрч харагддаг байна
        // - Гаднаас сонгосон бараа бүтээгдэхүүний жагсаалт DataTable-ээр өгнө. Түүнийг харуулна
        // - Жагсаалт дээр бараа бүтээгдэхүүн нэмнэ
        // - Жагсаалтаас бараа бүтээгдэхүүн хасна
        // - Жагсаалтыг хоослоно
        // - Жагсаалтын харагдах баганыг тохируулах боломжтой байна.
        // - Зарим нэгэн мөрий багана дээр зураг оруулах боломжтой байна.
        // - Тухайн зураг дээр дарахад гадагшаа Event raise хийх боломжтой байна. Зургыг Resource-оос авдаг байна.
        // - Тухайн жагсаалт дээрх барааны нийт дүнг авах
         */
        #region [ Variables ] 
        private ISM.CUser.Remote _remote = null;
        public ISM.CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }
        
        private ISM.Template.Resource _resource = null;
        public ISM.Template.Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        private ISM.Touch.TouchKeyboard _touchkeyboard = null;
        public ISM.Touch.TouchKeyboard TouchKeyboard
        {
            get { return _touchkeyboard; }
            set { _touchkeyboard = value; }
        }

        private string _layoutfilename = "";
        public string LayoutFileName
        {
            get { return _layoutfilename; }
        }

        private DataTable productlist;
        private DataTable productprice;
        #endregion
        #region [ Init ]
        public ucSalesProductList()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }
        public void InitAll()
        {
            InitGrid();
            InitData();
            DataColumnRefresh();
            InitAddColumns();
            InitHideColumns();
        }
        private void InitGrid()
        {
            try
            {
                grvSalesProductList.OptionsBehavior.ReadOnly = true;
                grvSalesProductList.OptionsBehavior.Editable = false;
                grvSalesProductList.OptionsCustomization.AllowGroup = false;
                grvSalesProductList.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
                grvSalesProductList.OptionsView.ColumnAutoWidth = false;
                //gridView1.OptionsView.ShowAutoFilterRow = false;
                grvSalesProductList.OptionsView.ShowGroupPanel = false;
                grvSalesProductList.OptionsView.ShowIndicator = false;
                grvSalesProductList.OptionsView.RowAutoHeight = true;
                grvSalesProductList.Appearance.Row.Font = new Font("Tahoma", 10.0F);

                // зурган багана нэмж оруулахад энэ үзэгдлийг зарлаж дотор нь зургаа set хийнэ.
                grvSalesProductList.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(grvSalesProductList_CustomUnboundColumnData);
                grvSalesProductList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(grvSalesProductList_RowCellClick);
                _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.Source);
            }
        }
        private void InitData()
        {
            try
            {
                productprice = new DataTable();
                productprice.Columns.Add("TotalPrice", typeof(decimal));
                productprice.Columns.Add("TotalDiscount", typeof(decimal));
                productprice.Columns.Add("TotalAmount", typeof(decimal));
                productprice.Rows.Add(0, 0, 0);


                productlist = new DataTable();
                productlist.Columns.Add("ProductType", typeof(int));
                productlist.Columns.Add("ProductID", typeof(string));
                productlist.Columns.Add("ProductName", typeof(string));
                productlist.Columns.Add("UnitPrice", typeof(decimal));
                productlist.Columns.Add("UnitPriceDis", typeof(decimal));
                productlist.Columns.Add("Quantity", typeof(long));
                productlist.Columns.Add("DiscountAmount", typeof(decimal));
                productlist.Columns.Add("VATAmount", typeof(decimal));
                productlist.Columns.Add("TotalAmount", typeof(decimal));

                grdSalesProductList.DataSource = productlist;
                DataColumnRefresh();
                ISM.Template.FormUtility.GridLayoutGet(grvSalesProductList, productlist, _layoutfilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ex.StackTrace+ex.Source);
            }
        }
        public void DataColumnRefresh()
        {
            try
            {
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 0, "Бараа төрөл");
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 1, "Бараа код");
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 2, "Бараа нэр");
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 3, "Нэгж үнэ");
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 4, "Нэгж үнэ хөнгөлөлт");
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 5, "Тоо");
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 6, "Нийт хөнгөлөлт");
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 7, "НӨАТ");
                ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 8, "Нийт үнэ");
                //ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 7, "Хасах");
                //ISM.Template.FormUtility.Column_SetCaption(ref grvSalesProductList, 8, "Хувиар");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.Source);
            }
        }
        void grvSalesProductList_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            //try
            //{
            //    DataRow row;
            //    if (e.IsGetData)
            //    {
            //        if (e.Column.ToolTip != "")
            //        {
            //            row = grvSalesProductList.GetDataRow(e.RowHandle);
            //            e.Value = _resource.GetImage(e.Column.ToolTip);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + ex.StackTrace + ex.Source);
            //}
        }
        private void InitAddColumns()
        {
            AddColumn("DEL", "Устгах", "Delete", "image_replymail");
            AddColumn("SCH", "Хувиар", "Schedule", "image_replymail");
        }
        private void InitHideColumns()
        {
            HideColumn(0);
        }
        #endregion
        #region [ Events ]
        void grvSalesProductList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "DEL")
                {
                    DataRow currentrow = grvSalesProductList.GetFocusedDataRow();
                    Remove(currentrow);
                }
                if (e.Column.FieldName == "SCH")
                {
                    MessageBox.Show("SCHEDULE");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.Source);
            }
        }

        public delegate void delegateEventProdChanged(DataTable prodlist, DataTable prodprice);
        public event delegateEventProdChanged EventProdChanged;
        public void OnProdChanged(DataTable prodlist, DataTable prodprice)
        {
            try
            {
                if (EventProdChanged != null) EventProdChanged(prodlist, prodprice);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.Source);
            }
        }
        #endregion
        #region [ User defined functions ]
        public void AddColumn(string ColName, string Caption, string Caption2, string ResourceName)
        {
            try
            {
                RepositoryItemPictureEdit ri = new RepositoryItemPictureEdit();
                grdSalesProductList.RepositoryItems.Add(ri);
                DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                col.VisibleIndex = grvSalesProductList.Columns.Count;
                if (_remote.User.UserLanguage == "MN")
                    col.Caption = Caption;
                else
                    col.Caption = Caption2;

                col.FieldName = string.Format(ColName);
                col.ColumnEdit = ri;
                col.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                col.OptionsColumn.ReadOnly = true;
                col.Width = 24;
                col.ToolTip = ResourceName;
                grvSalesProductList.Columns.Add(col);

                DataColumnRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.Source);
            }
        }
        public void HideColumn(int ColIndex)
        {
            grvSalesProductList.Columns[ColIndex].Visible = false;
        }
        public void DataSet(DataTable DT)
        {
            object[] obj = new object[9];
            productlist.Clear();
            foreach (DataRow row in DT.Rows)
            {
                obj = new object[8];
                obj[0] = Static.ToInt(row[0]);
                obj[1] = Static.ToStr(row[1]);
                obj[2] = Static.ToStr(row[2]);
                obj[3] = Static.ToDecimal(row[3]);
                obj[4] = Static.ToDecimal(row[4]);
                obj[5] = Static.ToLong(row[5]);
                obj[6] = Static.ToDecimal(row[6]);
                obj[7] = Static.ToDecimal(row[7]);
                obj[8] = Static.ToDecimal(row[8]);

                productlist.Rows.Add(obj);
            }
            DataRefresh();
        }
        public void Add(DataTable DT)
        {
            object[] obj = new object[9];

            foreach (DataRow row in DT.Rows)
            {
                obj = new object[9];
                obj[0] = Static.ToInt(row[0]);
                obj[1] = Static.ToStr(row[1]);
                obj[2] = Static.ToStr(row[2]);
                obj[3] = Static.ToDecimal(row[3]);
                obj[4] = Static.ToDecimal(row[4]);
                obj[5] = Static.ToLong(row[5]);
                obj[6] = Static.ToDecimal(row[6]);
                obj[7] = Static.ToDecimal(row[7]);
                obj[8] = Static.ToDecimal(row[8]);

                productlist.Rows.Add(obj);
            }
            DataRefresh();
        }
        public void Add(DataRow row)
        {
            object[] obj = new object[9];
            obj[0] = Static.ToInt(row[0]);
            obj[1] = Static.ToStr(row[1]);
            obj[2] = Static.ToStr(row[2]);
            obj[3] = Static.ToDecimal(row[3]);
            obj[4] = Static.ToDecimal(row[4]);
            obj[5] = Static.ToLong(row[5]);
            obj[6] = Static.ToDecimal(row[6]);
            obj[7] = Static.ToDecimal(row[7]);
            obj[8] = Static.ToDecimal(row[8]);

            productlist.Rows.Add(obj);
            DataRefresh();
        }
        public void Init(string pSalesNo)
        {
            // Борлуулалтын дугаараас дүүргэх

            productlist.Rows.Clear();

            DataTable dt;
            object[] param = new object[1];
            object[] obj = new object[9];
            param[0] = pSalesNo;
            Result res = _remote.Connection.Call(_remote.User.UserNo, 501, 500005, 500005, 0, 0, param);
            if (res.ResultNo == 0)
            {
            if (res.Data.Tables[0] != null)
            {
                dt = res.Data.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        obj = new object[9];
                        obj[0] = Static.ToInt(row[0]);
                        obj[1] = Static.ToStr(row[1]);
                        obj[2] = Static.ToStr(row[2]);
                        obj[3] = Static.ToDecimal(row[3]);
                        obj[4] = Static.ToDecimal(row[4]);
                        obj[5] = Static.ToLong(row[5]);
                        obj[6] = Static.ToDecimal(row[6]);
                        obj[7] = Static.ToDecimal(row[7]);
                        obj[8] = Static.ToDecimal(row[8]);

                        productlist.Rows.Add(obj);
                    }
                }
                else
                {
                    MessageBox.Show("Энэ үйлчилгээний мэдээлэл байхгүй байна. Системийн администраторт хандана уу!!! " + "Борлуулалтын дугаар= " + pSalesNo.ToString());
                }
            }
            }
 
            DataRefresh();
        }
        #endregion
        #region [ Methods ]
        public void DataRefresh()
        {
            DataColumnRefresh();
            OnProdChanged(productlist, GetCalculate());
        }
        public void Remove(DataRow row)
        {
            productlist.Rows.Remove(row);
            DataRefresh();
        }
        public void Remove(int index)
        {
            productlist.Rows.RemoveAt(index);
            DataRefresh();
        }
        public void Clear()
        {
            productlist.Rows.Clear();
            DataRefresh();
        }
        public DataTable GetCalculate()
        {
            decimal totalAmount = 0;
            decimal totalprice = 0;

            foreach (DataRow drow in productlist.Rows)
            { 
                totalprice += Static.ToDecimal(drow["UnitPrice"]) * Static.ToDecimal(drow["Quantity"]);
                totalAmount+=Static.ToDecimal(drow["TotalAmount"]);
            }

            productprice.Rows[0][0] = totalprice;
            productprice.Rows[0][1] = totalAmount - totalprice;
            productprice.Rows[0][2] = totalAmount;

            return productprice;
        }
        public void SaveLayout()
        {
            grvSalesProductList.SaveLayoutToXml(_layoutfilename);
        }
        #endregion
        private void grdSalesProductList_Click(object sender, EventArgs e)
        {

        }
    }
}
