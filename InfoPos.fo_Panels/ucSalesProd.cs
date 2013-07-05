using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using EServ.Shared;
using System.Threading;
namespace InfoPos.Panels
{
    public partial class ucSalesProd : DevExpress.XtraEditors.XtraUserControl
    {
        #region [ Variables ]
        private ISM.CUser.Remote _remote = null;
        public ISM.CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }

        private ISM.Template.Resource _resource;
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

        private DataTable _productlist;
        public DataTable productlist
        {
            get { return _productlist; }
            set { _productlist = value; }
        }
        private DataTable productprice;
        #endregion

        #region[Constracture]
        public ucSalesProd()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
            }
        }
        private void ucSalesProd_Load(object sender, EventArgs e)
        {
            try
            {
                gridView1.OptionsBehavior.ReadOnly = true;
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsCustomization.AllowGroup = false;
                gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsView.ShowIndicator = false;
                gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);
                _productlist = new DataTable();
                _productlist.Columns.Add("SALESNO", typeof(string));
                _productlist.Columns.Add("CUSTOMERNO", typeof(long));
                _productlist.Columns.Add("PRODCODE", typeof(string));
                _productlist.Columns.Add("PRODTYPE", typeof(int));
                _productlist.Columns.Add("PRODNAME", typeof(string));
                _productlist.Columns.Add("PRICE", typeof(decimal));
                _productlist.Columns.Add("DISCOUNT", typeof(decimal));
                _productlist.Columns.Add("SALESAMOUNT", typeof(decimal));
                _productlist.Columns.Add("QUANTITY", typeof(long));
                _productlist.Columns.Add("FLAG", typeof(int));
                _productlist.Columns.Add("EDIT", typeof(int));
                _productlist.Columns.Add("REMOVE", typeof(int));
                _productlist.Columns["EDIT"].DefaultValue = 0;
                _productlist.Columns["REMOVE"].DefaultValue = 0;
                
                gridControl1.DataSource = _productlist;
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Борлуулалтын №", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Харилцагчийн №", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Код", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Төрөл", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Нэр");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Үнэ");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Хөнгөлөлт");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Борлуулсан дүн", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Ширхэг");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "Урамшуулалын бараа эсэх", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "Засах");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "Устгах");
                if (Resource != null)
                {
                    imageCollection1.AddImage(Resource.GetImage("navigate_edit"));
                    imageCollection1.AddImage(Resource.GetImage("object_delete"));

                    repobtndelete.LargeImages = imageCollection1;
                    repobtnedit.LargeImages = imageCollection1;

                    gridView1.Columns[10].ColumnEdit = repobtnedit;
                    gridView1.Columns[11].ColumnEdit = repobtndelete;
                }
                gridView1.RowHeight = 25;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region[UserFunction]
        public void BestFitColumns()
        {
            gridView1.BestFitColumns(); 
        }
        public void CustomerFilter(long CustomerNo)
        {
            gridControl1.DataSource = null;
            var query = from row in productlist.AsEnumerable()
                        where row.Field<long>("CUSTOMERNO") == CustomerNo
                        select row;
            if (query != null)
            {
                if (query.Count() > 0)
                {
                    gridControl1.DataSource = query.CopyToDataTable();
                }
            }     
        }
        public void UpdateCustomer(long OldCustNo, long NewCustNo)
        {
            try
            {
                var query = from row in productlist.AsEnumerable()
                            where row.Field<long>("CUSTOMERNO") == OldCustNo
                            select row;
                if (query != null)
                {
                    if (query.Count() > 0)
                    {    
                        foreach (DataRow dr in query.AsEnumerable())
                        {
                            dr["CUSTOMERNO"] = NewCustNo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void RemoveProduct(long CustomerNo)
        {
            try
            {
                gridControl1.DataSource = null;
                if (productlist != null)
                {
                    var query = from row in productlist.AsEnumerable()
                                where row.Field<long>("CUSTOMERNO") != CustomerNo
                                select row;
                    if (query != null)
                    {
                        if (query.Count() > 0)
                        {
                            gridControl1.DataSource = query.CopyToDataTable();
                            productlist = query.CopyToDataTable();
                            gridControl1.RefreshDataSource();
                        }
                        else
                        {
                            gridControl1.DataSource = null;
                            productlist.Clear();
                        }
                    }
                    else
                    {
                        gridControl1.DataSource = query.CopyToDataTable();
                        productlist = query.CopyToDataTable();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SalesFilter(string[] salesno)
        {
            gridControl1.DataSource = null;
            List<string> filterTags = new List<string>(salesno);
            var query = from row in _productlist.AsEnumerable()
                        where filterTags.Contains(row.Field<string>("SALESNO"))
                        select row;
            if (query != null)
            {
                if (query.Count() != 0)
                {
                    gridControl1.DataSource = query.CopyToDataTable();
                }
            }
        }
        public Result RefreshData(string BatchNo)
        {
            Result res = new Result();
            try
            {
                res = _remote.Connection.Call(_remote.User.UserNo, 501, 600006, 600006, new object[] { Static.ToStr(BatchNo) });
                if(ISM.Template.FormUtility.ValidateQuery(res))
                {
                    //BatchNo, a.SalesNo, c.classcode, decode(c.classcode, 0, c.firstname||'.'||c.lastname, 1, c.corporatename) as CustName, c.sex, c.registerno,
//s.prodtype, s.prodno, i.name, i.isvat, s.price, s.discount, s.salesamount, s.quantity, c.customerno
                    DataTable dt = res.Data.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drow = productlist.NewRow();
                        drow["SALESNO"] = dr["SALESNO"];
                        drow["CUSTOMERNO"]=dr["CUSTOMERNO"];
                        drow["PRODCODE"] = dr["PRODNO"];
                        drow["PRODTYPE"] = dr["PRODTYPE"];
                        drow["PRODNAME"] = dr["NAME"];
                        drow["PRICE"] = dr["PRICE"];
                        drow["DISCOUNT"] = dr["DISCOUNT"];
                        drow["SALESAMOUNT"] = dr["SALESAMOUNT"];
                        drow["QUANTITY"] = dr["QUANTITY"];
                        drow["FLAG"] = dr["SALETYPE"];
                        _productlist.Rows.Add(drow);
                    }
                    gridControl1.DataSource = productlist;
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo=1;
                res.ResultDesc=ex.Message;
            }
            return res;
        }
        public void ColumnVisible(int index,bool visible)
        {
            try
            {
                if (gridView1.Columns.Count >= index)
                {
                    gridView1.Columns[index].Visible = visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
   
        /// <summary>
        /// Борлуулалт дээр бүтээгдэхүүн нэмэх
        /// </summary>
        /// <param name="SalesNo">Борлуулалтын дугаар(Шинээр борлуулалт бүртгэх бол ="")</param>
        /// <param name="CustomerNo">Харилцагчийн дугаар</param>
        /// <param name="ProdCode">Бүтээгдэхүүний код</param>
        /// <param name="ProdType">Төрөл</param>
        /// <param name="ProdName">Нэр</param>
        /// <param name="Price">Үнэ</param>
        /// <param name="Discount">Хөнгөлөлт</param>
        /// <param name="SalesAmount">Дүн</param>
        /// <param name="Quantity">Тоо ширхэг</param>
        /// <param name="Flag">Урамшуулалын бараа</param>
        /// <param name="Type">Баазруу шууд нэмэх бол =1 else =0</param>
        public void AddProduct(string SalesNo, long CustomerNo, string ProdCode, int ProdType, string ProdName, decimal Price, decimal Discount, decimal SalesAmount, int Quantity, int Flag, int Type)
        {
            Result res = new Result();
            var query = from row in _productlist.AsEnumerable()
                        where row.Field<long>("CUSTOMERNO") == CustomerNo && row.Field<string>("PRODCODE") == ProdCode && row.Field<int>("PRODTYPE") == ProdType && row.Field<int>("FLAG")==Flag
                        select row;
            if (query != null && query.Count() > 0)
            {
                DataRow dr = query.First();
                dr["Quantity"] = Static.ToInt(dr["QUANTITY"]) + Quantity;
                if (Type == 1)
                {
                    object[] obj = new object[8];

                    obj[0] = Static.ToStr(dr["SALESNO"]);         //SalesNo
                    obj[2] = Static.ToInt(dr["PRODTYPE"]);        //ProdType
                    obj[1] = Static.ToStr(dr["PRODCODE"]);        //ProdNo
                    obj[3] = Static.ToInt(dr["QUANTITY"]);        //Quantity
                    obj[4] = Static.ToDecimal(dr["PRICE"]);           //Price
                    obj[5] = Static.ToDecimal(dr["DISCOUNT"]);        //Discount
                    obj[6] = Static.ToDecimal(dr["SALESAMOUNT"]);     //SalesAmount
                    obj[7] = Static.ToDecimal(dr["FLAG"]);     //SALESTYPE

                    res = _remote.Connection.Call(_remote.User.UserNo, 501, 600019, 600019, obj);
                    if (res.ResultNo != 0)
                        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Бүтээгдэхүүн нэмэхэд алдаа гарлаа");
                }
            }
            else
            {
                DataRow dr = _productlist.NewRow();
                dr["SALESNO"] = SalesNo;
                dr["CUSTOMERNO"] = CustomerNo;
                dr["PRODCODE"] = ProdCode;
                dr["PRODTYPE"] = ProdType;
                dr["PRODNAME"] = ProdName;
                dr["PRICE"] = Price;
                dr["DISCOUNT"] = Discount;
                dr["SALESAMOUNT"] = SalesAmount;
                dr["QUANTITY"] = Quantity;
                dr["FLAG"] = Flag;
                _productlist.Rows.Add(dr);
                if (Type == 1)
                {
                    object[] obj = new object[8];

                    obj[0] = Static.ToStr(dr["SALESNO"]);            //SalesNo
                    obj[1] = Static.ToStr(dr["PRODCODE"]);           //ProdNo
                    obj[2] = Static.ToInt(dr["PRODTYPE"]);           //ProdType
                    obj[3] = Static.ToInt(dr["QUANTITY"]);            //Quantity
                    obj[4] = Static.ToDecimal(dr["PRICE"]);           //Price
                    obj[5] = Static.ToDecimal(dr["DISCOUNT"]);        //Discount
                    obj[6] = Static.ToDecimal(dr["SALESAMOUNT"]);     //SalesAmount
                    obj[7] = Static.ToDecimal(dr["FLAG"]);     //SALESTYPE

                    res = _remote.Connection.Call(_remote.User.UserNo, 501, 600018, 600018, obj);
                    if (res.ResultNo != 0)
                        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Бүтээгдэхүүн нэмэхэд алдаа гарлаа");
                }
            }
            CustomerFilter(CustomerNo);
            BestFitColumns();
        }
        public void LoyalProductRemove()
        {
            foreach (DataRow row in _productlist.Select())
            {
                if (Static.ToInt(row["FLAG"]) == 3)
                {
                    row.Delete();
                    _productlist.AcceptChanges();
                }
            }
            gridControl1.DataSource = _productlist;
        }
        public void SetSalesAmount(long CustomerNo, string ProdCode, int ProdType, decimal SalesPrice)
        {
            var query = from row in productlist.AsEnumerable()
                        where row.Field<long>("CUSTOMERNO").Equals(CustomerNo) && row.Field<string>("PRODCODE").Equals(ProdCode) && row.Field<int>("PRODTYPE").Equals(ProdType)
                        select row;
            if (query != null && query.Count() != 0)
            {
                DataRow dr = query.First();
                dr["DISCOUNT"] = Static.ToDecimal(dr["PRICE"]) - SalesPrice;
                dr["SALESAMOUNT"] = SalesPrice;
            }
            gridControl1.DataSource = _productlist;
        }
        public void DefaultSalesAmount()
        {
            try
            {
                foreach (DataRow dr in productlist.Rows)
                {
                    dr["SALESAMOUNT"] = dr["PRICE"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowKeyboard(int rowhandle)
        {
            gridView1.FocusedRowHandle = rowhandle;
            _touchkeyboard.ShowKeyboard(gridView1, rowhandle, 8);
        }
        public DataTable GetProdList()
        {
            if (gridControl1.DataSource != null)
                return (DataTable)gridControl1.DataSource;
            else
                return null;
        }
        #endregion

        #region[Custom Events]
        public delegate void delegateEventOnRowCellClick();
        public event delegateEventOnRowCellClick EventOnRowCellClick;
        public void OnRowCellClick()
        {
            try
            {
                if (EventOnRowCellClick != null) EventOnRowCellClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }
        #endregion    

        #region[Control Events]
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Result res = new Result();
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            if (Static.ToInt(e.Value) != 0)
            {
                DataRow drow = productlist.AsEnumerable().Where(r => ((long)r["CUSTOMERNO"]).Equals(dr["CUSTOMERNO"]) && ((string)r["PRODCODE"]).Equals(dr["PRODCODE"]) && ((int)r["PRODTYPE"]).Equals(dr["PRODTYPE"])).First();
                drow["QUANTITY"] = e.Value;
                //if (Static.ToStr(dr["SALESNO"]) != "")
                //{
                //    object[] obj = new object[8];
                //    obj[0] = Static.ToStr(drow["SALESNO"]);
                //    obj[1] = Static.ToStr(drow["PRODCODE"]);
                //    obj[2] = Static.ToInt(drow["PRODTYPE"]);
                //    obj[3] = Static.ToInt(dr["QUANTITY"]);
                //    obj[4] = Static.ToDecimal(drow["PRICE"]);
                //    obj[5] = Static.ToDecimal(drow["DISCOUNT"]);
                //    obj[6] = Static.ToDecimal(drow["SALESAMOUNT"]);
                //    obj[7] = Static.ToInt(drow["FLAG"]);

                //    res = _remote.Connection.Call(_remote.User.UserNo, 501, 600019, 600019, obj);
                //    if (res.ResultNo != 0)
                //    {
                //        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Бүтээгдэхүүн засах үед алдаа гарлаа.");
                //    }
                //}
            }
            else
            {
                foreach (DataRow drow in productlist.Rows)
                {
                    if (Static.ToLong(drow["CUSTOMERNO"]) == Static.ToLong(dr["CUSTOMERNO"]) && Static.ToStr(drow["PRODCODE"]) == Static.ToStr(dr["PRODCODE"]) && Static.ToInt(drow["PRODTYPE"]) == Static.ToInt(dr["PRODTYPE"]))
                    {
                        //res = _remote.Connection.Call(_remote.User.UserNo, 501, 600020, 600020, new object[] { drow["SALESNO"], drow["PRODTYPE"], drow["PRODCODE"] });
                        //if (res.ResultNo != 0)
                        //{
                        //    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Бүтээгдэхүүн устгах үед алдаа гарлаа.");
                        //}
                        productlist.Rows.Remove(drow);
                        gridView1.DeleteRow(e.RowHandle);
                        gridView1.RefreshData();
                        break;
                    }
                }
            }
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            Result res = new Result();
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                if (e.Column.Name == "colREMOVE")
                {
                    if (Static.ToInt(dr["QUANTITY"]) > 1)
                    {
                        DataRow drow = productlist.AsEnumerable().Where(r => ((long)r["CUSTOMERNO"]).Equals(dr["CUSTOMERNO"]) && ((string)r["PRODCODE"]).Equals(dr["PRODCODE"]) && ((int)r["PRODTYPE"]).Equals(dr["PRODTYPE"])).First();
                        dr["QUANTITY"] = Static.ToInt(dr["QUANTITY"]) - 1;
                        drow["QUANTITY"] = Static.ToInt(drow["QUANTITY"]) - 1;
                        //object[] obj = new object[8];
                        //obj[0] = Static.ToStr(drow["SALESNO"]);
                        //obj[1] = Static.ToStr(drow["PRODCODE"]);
                        //obj[2] = Static.ToInt(drow["PRODTYPE"]);
                        //obj[3] = Static.ToInt(dr["QUANTITY"]);
                        //obj[4] = Static.ToDecimal(drow["PRICE"]);
                        //obj[5] = Static.ToDecimal(drow["DISCOUNT"]);
                        //obj[6] = Static.ToDecimal(drow["SALESAMOUNT"]);
                        //obj[7] = Static.ToInt(drow["FLAG"]);
                        //res = _remote.Connection.Call(_remote.User.UserNo, 501, 600019, 600019, obj);
                        //if (res.ResultNo != 0)
                        //{
                        //    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Бүтээгдэхүүн засах үед алдаа гарлаа.");
                        //}
                        //else
                        //{
                        OnRowCellClick();
                        //}
                    }
                    else
                    {
                        if (Static.ToInt(dr["QUANTITY"]) == 1)
                        {
                            dr["QUANTITY"] = Static.ToInt(dr["QUANTITY"]) - 1;
                            foreach (DataRow drow in productlist.Rows)
                            {
                                if (Static.ToLong(drow["CUSTOMERNO"]) == Static.ToLong(dr["CUSTOMERNO"]) && Static.ToStr(drow["PRODCODE"]) == Static.ToStr(dr["PRODCODE"]) && Static.ToInt(drow["PRODTYPE"]) == Static.ToInt(dr["PRODTYPE"]))
                                {
                                    //res = _remote.Connection.Call(_remote.User.UserNo, 501, 600020, 600020, new object[] { drow["SALESNO"], drow["PRODTYPE"], drow["PRODCODE"] });
                                    //if (res.ResultNo != 0)
                                    //{
                                    //    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc, "Бүтээгдэхүүн устгах үед алдаа гарлаа.");
                                    //}
                                    //else
                                    //{
                                    OnRowCellClick();
                                    //}
                                    productlist.Rows.Remove(drow);
                                    gridView1.DeleteRow(e.RowHandle);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (e.Column.Name == "colEDIT")
                {
                    ShowKeyboard(e.RowHandle);
                    OnRowCellClick();
                }
                gridView1.RefreshData();
            }
        }
        #endregion
    }
}
