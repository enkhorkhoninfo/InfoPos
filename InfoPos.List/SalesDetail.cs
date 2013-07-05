using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using EServ.Shared;

namespace InfoPos.List
{
    public partial class SalesDetail : DevExpress.XtraEditors.XtraForm
    {
        Core.Core _core = null;
        string BatchNo = "";
        public SalesDetail(Core.Core core,string batchno,DateTime postdate)
        {
            InitializeComponent();
            _core = core;
            BatchNo = batchno;
            RefreshDetail(BatchNo, postdate);
        }
        private void RefreshDetail(string batchno, DateTime postdate)
        {
            try
            {
                gridView1.GroupSummary.Clear();
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 500007, 500007, new object[] { batchno, _core.TxnDate, postdate });
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                        #region[ ProductList ]
                        gridView1.OptionsBehavior.Editable = false;
                        gridView1.OptionsBehavior.ReadOnly = true;
                        gridControl1.DataSource = res.Data.Tables[0];

                        gridView1.Columns[0].GroupIndex = 0;
                        gridView1.Columns[0].GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value;
                        gridView1.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                        gridView1.Columns[0].Caption = "Харилцагчийн дугаар";
                        gridView1.Columns[0].Visible = false;

                        gridView1.Columns[1].Caption = "Харилцагчийн нэр";

                        gridView1.Columns[2].Caption = "Дугаар";

                        gridView1.Columns[3].Caption = "Төрөл";

                        gridView1.Columns[4].Caption = "Нэр";
                        gridView1.Columns[4].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                        gridView1.Columns[4].SummaryItem.DisplayFormat = "Нийлбэр дүн:";

                        gridView1.Columns[5].Caption = "Нэгж үнэ";
                        gridView1.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gridView1.Columns[5].SummaryItem.DisplayFormat = "{0:n2}";

                        gridView1.Columns[6].Caption = "Ширхэг";
                        gridView1.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gridView1.Columns[6].SummaryItem.DisplayFormat = "{0}";

                        gridView1.Columns[7].Caption = "Дүн";
                        gridView1.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gridView1.Columns[7].SummaryItem.DisplayFormat = "{0:n2}";

                        gridView1.Columns[8].Caption = "Хөнгөлөлт";
                        gridView1.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gridView1.Columns[8].SummaryItem.DisplayFormat = "{0:n2}";

                        gridView1.Columns[9].Caption = "Нийт дүн";
                        gridView1.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gridView1.Columns[9].SummaryItem.DisplayFormat = "{0:n2}";

                        gridView1.Columns[10].Caption = "Төлөв";
                        gridView1.Columns[10].Visible = false;

                        gridView1.Columns[11].Caption = "Огноо";
                        gridView1.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridView1.Columns[11].DisplayFormat.FormatString = "G";

                        gridView1.Columns[11].Visible = false;
                        gridView1.Columns[12].Visible = false;
                        gridView1.Columns[13].Visible = false;
                        gridView1.Columns[14].Visible = false;

                        GridGroupSummaryItem item = new GridGroupSummaryItem();
                        item.FieldName = "SALESAMOUNT";
                        item.DisplayFormat = "Нийт дүн {0:n2}";
                        item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gridView1.GroupSummary.Add(item);


                        gridView1.ExpandAllGroups();
                        gridView1.BestFitColumns();
                        #endregion
                        #region[ Payment List ]
                        gridControl2.DataSource = res.Data.Tables[1];
                        gridView2.OptionsBehavior.Editable = false;
                        gridView2.OptionsBehavior.ReadOnly = true;
                        gridView2.Columns[0].Caption = "№";
                        gridView2.Columns[1].Caption = "Төлбөрийн хэрэгсэл №";
                        gridView2.Columns[1].Visible = false;
                        gridView2.Columns[2].Caption = "Төлбөрийн хэрэгсэл";
                        gridView2.Columns[3].Caption = "Бүртгэлийн дугаар";
                        gridView2.Columns[4].Caption = "Төлөгдөх дүн";
                        gridView2.Columns[5].Caption = "Төлөв";
                        gridView2.Columns[6].Caption = "Хариулт дүн";
                        gridView2.Columns[6].Visible = false;


                        gridView2.Columns[3].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                        gridView2.Columns[3].SummaryItem.DisplayFormat = "Нийлбэр дүн:";

                        gridView2.Columns[4].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gridView2.Columns[4].SummaryItem.DisplayFormat = "{0:n2}";
                        gridView2.BestFitColumns();
                        #endregion

                    decimal salesamount = res.Data.Tables[0].AsEnumerable().Where(y => Static.ToInt(y["SALESAMOUNT"]) >= 0).Sum(y => y.Field<decimal>("SALESAMOUNT"));
                    decimal salesamount1 = res.Data.Tables[0].AsEnumerable().Where(y => Static.ToInt(y["SALESAMOUNT"]) < 0).Sum(y => y.Field<decimal>("SALESAMOUNT"));
                    decimal chargeamount = 0;
                    foreach (DataRow dr in res.Data.Tables[1].Rows)
                    {
                        if (Static.ToStr(dr["STATUS"]) == "" && Static.ToDecimal(dr["CHARGEAMOUNT"]) != 0)
                        {
                            chargeamount = Static.ToDecimal(dr["CHARGEAMOUNT"]);
                        }
                    }

                    decimal addmamount = res.Data.Tables[0].AsEnumerable().Where(y => Static.ToInt(y["STATUS"]) == 3).Sum(y => y.Field<decimal>("SALESAMOUNT"));
                    decimal returnmamount = res.Data.Tables[0].AsEnumerable().Where(y => Static.ToInt(y["STATUS"]) == 2).Sum(y => y.Field<decimal>("SALESAMOUNT"));

                    txtSalesAmount.EditValue = salesamount - addmamount;
                    txtReturnAmount.EditValue = salesamount1;

                    txtPaidAmount.EditValue = salesamount + salesamount1 + chargeamount;
                    txtAddAmount.EditValue = addmamount;
                    txtChargeAmount.EditValue = chargeamount;
                    txtTotalAmount.EditValue = salesamount + salesamount1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}