using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISM.Touch;
using EServ.Shared;
namespace InfoPos.Cash
{
    public partial class frmCash : DevExpress.XtraEditors.XtraForm,ISM.Touch.ITouchCall
    {
        #region[ Variables ]
        InfoPos.Core.Core _core = null;
        ISM.Template.Resource _resource = null;
        ISM.Touch.TouchKeyboard _kb = null;
        Hashtable productpanel = new Hashtable();
        string SalesNo = "";
        string CustomerNo = "";
        string RegisterNo = "";
        string OrderNo = "";
        #endregion
        #region[ Init ]
        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;
                _kb = new ISM.Touch.TouchKeyboard();
                _kb.Enable = true;

                this.ucCustSearch1.Remote = _core.RemoteObject;
                this.ucCustSearch1.Core = _core;
                this.ucCustSearch1.Resource = _resource;

                this.ucSalesProd1.Remote = _core.RemoteObject;
                this.ucSalesProd1.Resource = _core.Resource;
                this.ucSalesProd1.TouchKeyboard = _kb;

                this.ucContractSearch1.Core = _core;
                this.ucContractSearch1.Remote = _core.RemoteObject;
                this.ucContractSearch1.Resource = _core.Resource;
                this.ucContractSearch1.TouchKeyboard = _kb;

                this.ucOrderSearch1.Core = _core;
                this.ucOrderSearch1.Remote = _core.RemoteObject;
                this.ucOrderSearch1.Resource = _core.Resource;
                this.ucOrderSearch1.TouchKeyboard = _kb;

                this.ucPayment1.Core = _core;
                this.ucPayment1.Remote = _core.RemoteObject;
                this.ucPayment1.Resource = _core.Resource;
                this.ucPayment1.TouchKeyboard = _kb;

                this.MdiParent = _core.MainForm;
                this.Show();
                if (item.Key == "fo_cashreg")
                {
                    FormLoad(param, 0);
                }
                if (item.Key == "fo_salesreturn")
                {
                    FormLoad(param, 1);
                }
                TabMAIN.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            try
            {
                switch (buttonkey)
                {
                    case "fo_cashreg_1.1":
                        TabMAIN.SelectedTabPageIndex = 0;
                        break;
                    case "fo_cash_new_2.1":
                        TabMAIN.SelectedTabPageIndex = 1;
                        break;
                    case "fo_cash_new_1.2":
                        TabMAIN.SelectedTabPageIndex = 2;
                        break;
                    case "fo_cashreg_2.8":
                        item.IsClose = 1;
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public frmCash()
        {
            InitializeComponent();
        }
        private void FormLoad(object param,int func)
        {
            if (func == 1)
            {
                _core.MainForm_HeaderSet(0, "Харилцагчийн №", "");
                _core.MainForm_HeaderSet(1, "Овог", "");
                _core.MainForm_HeaderSet(2, "Нэр", "");
            }
            else
            {
                _core.MainForm_HeaderSet(0, "Борлуулалтын №", "");
                _core.MainForm_HeaderSet(1, "Төлбөрийн №", "");
                _core.MainForm_HeaderSet(2, "Харилцагчийн №", "");
                _core.MainForm_HeaderSet(3, "Овог", "");
                _core.MainForm_HeaderSet(4, "Нэр", "");
            }
            #region[ Events ]
            TouchChoice.EventKeyDown += new TouchButtonGroup.delegateKeyDown(TouchChoice_EventKeyDown);
            TouchProductMenu.EventKeyDown += new TouchButtonGroup.delegateKeyDown(TouchProductMenu_EventKeyDown);
            ucSalesPayment1.EventOnDiscount += new fo_panels.ucSalesPayment.delegateEventOnOtherPayment(ucSalesPayment1_EventOnDiscount);
            ucSalesPayment1.EventOnOtherPayment += new fo_panels.ucSalesPayment.delegateEventOnOtherPayment(ucSalesPayment1_EventOnOtherPayment);

            ucCustSearch1.EventChoose += new Panels.ucCustSearch.delegateEventChoose(ucCustSearch1_EventChoose);
            ucContractSearch1.EventChoose += new Panels.ucContractSearch.delegateEventChoose(ucContractSearch1_EventChoose);
            ucOrderSearch1.EventChoose += new Order.ucOrderSearch.delegateEventChoose(ucOrderSearch1_EventChoose);
            #endregion
            InitProductMenu();
            InitChoice();
        }

        void ucSalesPayment1_EventOnOtherPayment()
        {
            TabMAIN.SelectedTabPageIndex = 5;
        }
        void ucCustSearch1_EventChoose(DataRow currentrow)
        {
            CustomerNo=Static.ToStr(currentrow["CUSTOMERNO"]);
            _core.MainForm_HeaderSet(0, "", CustomerNo);
            _core.MainForm_HeaderSet(1, "", Static.ToStr(currentrow["FIRSTNAME"]));
            _core.MainForm_HeaderSet(2, "", Static.ToStr(currentrow["LASTNAME"]));
            TabMAIN.SelectedTabPageIndex = 4;
        }
        void ucOrderSearch1_EventChoose(DataRow currentrow)
        {
            OrderNo = CustomerNo = Static.ToStr(currentrow["ORDERNO"]);
            TabMAIN.SelectedTabPageIndex = 4;
        }

        void ucContractSearch1_EventChoose(DataRow currentrow)
        {
            RegisterNo = CustomerNo = Static.ToStr(currentrow["CONTRACTNO"]);
            TabMAIN.SelectedTabPageIndex = 4;
        }
        void ucSalesPayment1_EventOnDiscount()
        {
            ucSalesPayment1.SetPaymentAmount(ucSalesProd1.productlist);
            if (RegisterNo == "") ucSalesPayment1.PaymentState();
            else ucSalesPayment1.RegisterState();
        }
        #endregion
        #region[ Function ]
        private DataTable GetPack(string ID, DataTable prodlist)
        {

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            Result res;
            res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PACKMAIN", 500101, ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PACKITEM", 500101, ref dt1);
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    var query = from row in dt1.AsEnumerable()
                                where row.Field<string>("PackID") == ID
                                select row;
                    if (query != null && query.Count() > 0)
                    {
                        dt = query.CopyToDataTable();
                        foreach (DataRow drow in dt.Rows)
                        {
                            switch (Static.ToInt(drow["ProdType"]))
                            {
                                case 0:
                                    prodlist = GetInv(Static.ToStr(drow["ProdID"]), prodlist, Static.ToInt(drow["count"]));
                                    break;
                                case 1:
                                    prodlist = GetServ(Static.ToStr(drow["ProdID"]), prodlist, Static.ToInt(drow["count"]));
                                    break;
                            }
                        }
                    }
                }
            }

            return prodlist;
        }
        private DataTable GetInv(string ID, DataTable prodlist, int invcount)
        {
            object[] obj = new object[10];
            DataTable dt = new DataTable();
            decimal tmpamount = 0;
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "INVMAIN", 500101, ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                var query = from row in dt.AsEnumerable()
                            where row.Field<string>("INVID") == ID
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dt = query.CopyToDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        // 0 - SALESNO
                        // 1 - CUSTOMERNO
                        // 2 - PRODCODE
                        // 3 - PRODTYPE
                        // 4 - PRODNAME
                        // 5 - PRICE
                        // 6 - DISCOUNT
                        // 7 - SALESAMOUNT
                        // 8 - QUANTITY
                        // 9 - FLAG 0 - UnFlag, 1 - RentFlag, 9- RebateFlag 
                        obj[0] = SalesNo;
                        obj[1] = CustomerNo;
                        obj[2] = Static.ToStr(ID);
                        obj[3] = 0;
                        obj[4] = Static.ToStr(dt.Rows[0]["NAME"]);
                        obj[5] = Static.ToDecimal(dt.Rows[0]["PRICEAMOUNT"]);
                        obj[6] = 0;
                        obj[7] = 0;
                        if (invcount != 0)
                            obj[8] = invcount;
                        else
                            obj[8] = 1;
                        obj[9] = Static.ToInt(dt.Rows[0]["RENTFLAG"]);
                        tmpamount = GetCalendarPrice(0, ID, DateTime.Now);
                        if (tmpamount != 0)
                        {
                            obj[5] = tmpamount;
                        }

                        prodlist.Rows.Add(obj);
                    }
                    else
                    {
                        MessageBox.Show("Энэ үйлчилгээний мэдээлэл байхгүй байна. Системийн администраторт хандана уу!!! " + "Бараа " + ID);
                    }

                }
            }
            return prodlist;
        }
        private DataTable GetServ(string ID, DataTable prodlist, int servcount)
        {
            object[] obj = new object[10];
            DataTable dt = new DataTable();
            decimal tmpamount = 0;
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "SERVMAIN", 500101, ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                var query = from row in dt.AsEnumerable()
                            where row.Field<string>("SERVID") == ID
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dt = query.CopyToDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        // 0 - SALESNO
                        // 1 - CUSTOMERNO
                        // 2 - PRODCODE
                        // 3 - PRODTYPE
                        // 4 - PRODNAME
                        // 5 - PRICE
                        // 6 - DISCOUNT
                        // 7 - SALESAMOUNT
                        // 8 - QUANTITY
                        // 9 - FLAG 0 - UnFlag, 1 - RentFlag, 9- RebateFlag 
                        // 10 - EDIT
                        // 11 - DELETE
                        obj[0] = SalesNo;
                        obj[1] = CustomerNo;
                        obj[2] = Static.ToStr(ID);
                        obj[3] = 1;
                        obj[4] = Static.ToStr(dt.Rows[0]["NAME"]);
                        obj[5] = Static.ToDecimal(dt.Rows[0]["PRICEAMOUNT"]);
                        obj[6] = 0;
                        obj[7] = 0;
                        if (servcount != 0)
                            obj[8] = servcount;
                        else
                            obj[8] = 1;
                        obj[9] = 0;
                        tmpamount = GetCalendarPrice(0, ID, DateTime.Now);
                        if (tmpamount != 0)
                        {
                            obj[5] = tmpamount;
                        }

                        prodlist.Rows.Add(obj);
                    }
                    else
                    {
                        MessageBox.Show("Энэ үйлчилгээний мэдээлэл байхгүй байна. Системийн администраторт хандана уу!!! " + "Бараа " + ID);
                    }
                }
            }
            return prodlist;
        }
        private decimal GetCalendarPrice(int typeid, string id, DateTime datetime)
        {
            DataTable dt = new DataTable();
            decimal result = 0;
            string daytype = "";
            Result res;
            res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PACALENDAR", 500101, ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                var query = from row in dt.AsEnumerable()
                            where row.Field<DateTime>("DAY") == datetime
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dt = query.CopyToDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        daytype = Static.ToStr(dt.Rows[0]["DayType"]);
                    }
                }
            }

            if (daytype == "") return result;

            DataTable dt1 = new DataTable();
            res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PRODPRICE", 500101, ref dt1);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                var query = from row in dt1.AsEnumerable()
                            where row.Field<int>("ProdType") == typeid &&
                                  row.Field<string>("ProdID") == id &&
                                  row.Field<DateTime>("StartTime") >= datetime &&
                                  row.Field<DateTime>("EndTime") <= datetime
                            select row;
                if (query != null && query.Count() > 0)
                {
                    dt1 = query.CopyToDataTable();
                    if (dt1.Rows.Count > 0)
                    {
                        result = Static.ToDecimal(dt1.Rows[0]["Price"]);
                    }
                }
            }
            return result;
        }
        #region[TouchButtonInit]
        private void InitProductMenu()
        {
            string parent = "";
            string code = "";
            string name = "";
            int col = 0;
            int row = 0;
            int ptype = 0;
            string pid = "";
            try
            {
                productpanel = new Hashtable();

                DataTable dt = new DataTable();
                Result res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "PRODUCTPANEL", 500101, ref dt);
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        TouchProductMenu.Init(3, 3, 2);
                        foreach (DataRow drow in dt.Rows)
                        {
                            parent = Static.ToStr(drow["PARENTCODE"]);
                            code = Static.ToStr(drow["NodeCode"]);
                            if (_core.RemoteObject.User.UserLanguage == "MN")
                                name = Static.ToStr(drow["Name"]);
                            else
                                name = Static.ToStr(drow["Name2"]);
                            col = Static.ToInt(drow["COLINDEX"]);
                            row = Static.ToInt(drow["ROWINDEX"]);
                            ptype = Static.ToInt(drow["NODETYPE"]);
                            pid = Static.ToStr(drow["NODEID"]);

                            TouchProductMenu.Add(parent, code, row, col, name, name, _core.Resource.GetBitmap("frontmenu_batch_choose"));
                            if (ptype != 0)
                                productpanel.Add(parent.ToString() + "-" + code.ToString(), drow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void InitChoice()
        {
            try
            {
                TouchChoice.Init(3, 1, 2);
                TouchChoice.Add("ROOT", "customertosale", 1, 1, "Харилцагч хайх", "", _core.Resource.GetBitmap("frontmenu_sales_cust_search"));
                TouchChoice.Add("ROOT", "ordertosale", 2, 1, "Захиалга хайх", "", _core.Resource.GetBitmap("frontmenu_order_search"));
                TouchChoice.Add("ROOT", "registertosale", 3, 1, "Бүртгэл хайх", "", _core.Resource.GetBitmap("frontmenu_batch_choose"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #endregion
        #region [User Control Events]
        void TouchProductMenu_EventKeyDown(Control sender, MouseEventArgs e, TouchLinkItem item, ref bool cancel)
        {
            if (item != null)
            {
                if (productpanel.ContainsKey(item.parentKey.ToString() + "-" + item.Key.ToString()))
                {
                    DataTable productlist = new DataTable();
                    productlist.Columns.Add("SALESNO", typeof(string));
                    productlist.Columns.Add("CUSTOMERNO", typeof(long));
                    productlist.Columns.Add("PRODCODE", typeof(string));
                    productlist.Columns.Add("PRODTYPE", typeof(int));
                    productlist.Columns.Add("PRODNAME", typeof(string));
                    productlist.Columns.Add("PRICE", typeof(decimal));
                    productlist.Columns.Add("DISCOUNT", typeof(decimal));
                    productlist.Columns.Add("SALESAMOUNT", typeof(decimal));
                    productlist.Columns.Add("QUANTITY", typeof(long));
                    productlist.Columns.Add("FLAG", typeof(int));
                    productlist.Columns.Add("EDIT");
                    productlist.Columns.Add("DELETE");
                    DataRow drow = (DataRow)productpanel[item.parentKey.ToString() + "-" + item.Key.ToString()];
                    switch (Static.ToInt(drow["NODETYPE"]))
                    {
                        case 1:
                            productlist = GetPack(Static.ToStr(drow["NODEID"]), productlist);
                            break;
                        case 2:
                            productlist = GetInv(Static.ToStr(drow["NODEID"]), productlist, 1);
                            break;
                        case 3:
                            productlist = GetServ(Static.ToStr(drow["NODEID"]), productlist, 1);
                            break;
                    }
                    foreach (DataRow dr in productlist.Rows)
                    {
                        ucSalesProd1.AddProduct(SalesNo,
                        Static.ToLong(dr["CUSTOMERNO"]),
                        Static.ToStr(dr["PRODCODE"]),
                        Static.ToInt(dr["PRODTYPE"]),
                        Static.ToStr(dr["PRODNAME"]),
                        Static.ToDecimal(dr["PRICE"]),
                        Static.ToDecimal(dr["DISCOUNT"]),
                        Static.ToDecimal(dr["SALESAMOUNT"]),
                        Static.ToInt(dr["QUANTITY"]),
                        Static.ToInt(dr["FLAG"]),
                        0
                        );
                    }
                    ucSalesPayment1.DiscountState();
                }
            }
        }
        void TouchChoice_EventKeyDown(Control sender, MouseEventArgs e, TouchLinkItem item, ref bool cancel)
        {
            switch (item.Key)
            {
                case "customertosale": TabMAIN.SelectedTabPageIndex = 1; ucCustSearch1.DataRefresh(1); break;
                case "ordertosale": TabMAIN.SelectedTabPageIndex = 2; ucOrderSearch1.DataRefresh(1); break;
                case "registertosale": TabMAIN.SelectedTabPageIndex = 3; ucContractSearch1.DataRefresh(1); break;
            }
        }
        #endregion
    }
}