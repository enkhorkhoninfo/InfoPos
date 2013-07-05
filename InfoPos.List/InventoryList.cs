using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.List
{
    public partial class InventoryList : Form
    {
        private Core.Core _core;
        int PrivNo = 140211;
        string appname = "", formname = "";
        Form FormName = null;
        public InventoryList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucInventoryList.Resource = _core.Resource;
           //InventoryList.ActiveForm.AcceptButton = ucInventoryList.btnFind;
        }
        public InventoryList(Core.Core core,string pParam)
        {
            InitializeComponent();
            _core = core;
            Init1(pParam);
            ucInventoryList.Resource = _core.Resource;
            //InventoryList.ActiveForm.AcceptButton = ucInventoryList.btnFind;
        }
        #region [ Init ]
        private void Init()
        {
            ucInventoryList.EventSelected += new ucGridPanel.delegateEventSelected(ucInventoryList_EventSelected);
            ucInventoryList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucInventoryList_EventDataChanged);
            ucInventoryList.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucInventoryList_EventFindPaging);
            ucInventoryList.gridView1.RowStyle += new RowStyleEventHandler(gridView1_RowStyle);
            labelControl1.BackColor = Color.NavajoWhite;
            //         labelControl3.BackColor = Color.White;
            labelControl2.BackColor = Color.Orange;

            labelControl1.ForeColor = Color.NavajoWhite;
            //   labelControl3.ForeColor = Color.White;
            labelControl2.ForeColor = Color.Orange;

//"a.InvId like","a.InvType","a.Name like","a.BrandId","a.PriceAmount",
//"a.PriceRefund","a.Count","a.CatCode","a.BarCode","a.Unit",
//"a.UnitSize","a.PrinterType","a.CreateDate","a.Note like",
            //"a.Status","a.SalesAccountNo like","a.RefundAccountNo like","a.DiscountAccountNo like","a.BonusAccountNo like" ,"a.BonusExpAccountNo like"           

            ucInventoryList.FindItemAdd("InvID", "Бараа материалын дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucInventoryList.FieldFindAdd("InvType", "Бараа материалын төрөл", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("Name", "Нэр", typeof(string), "");
            ucInventoryList.FieldFindAdd("Name2", "Нэр 2", typeof(string), "");
            ucInventoryList.FieldFindAdd("BrandId", "Брэндийн дугаар", typeof(ArrayList), "");

            ucInventoryList.FindItemAdd("PriceAmount", "Үнэ", "", DynamicParameterType.Decimal, false, "d", "");
            ucInventoryList.FindItemAdd("PriceRefund", "Нөхөн төлбөрийн дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucInventoryList.FieldFindAdd("Count", "Нийт тоо ширхэг", typeof(int), "");
            ucInventoryList.FieldFindAdd("CatCode ", "Ангилалын код", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("BarCode", "Бар код", typeof(string), "");

            ucInventoryList.FieldFindAdd("Unit", "Хэмжих нэгж", typeof(ArrayList), "");
            ucInventoryList.FindItemAdd("UnitSize", "Хэмжээ, размер", "", DynamicParameterType.Decimal, false, "d", "");
            ucInventoryList.FieldFindAdd("PrinterType", "Принтерийн төрөл", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("CreateDate", "Үүсгэсэн огноо", typeof(DateTime), "");
            ucInventoryList.FieldFindAdd("Note", "Барааны тайлбар", typeof(string), "");

            ucInventoryList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("SalesAccountNo", "Борлуулалтын орлогын данс", typeof(string), "");
            ucInventoryList.FieldFindAdd("RefundAccountNo", "Борлуулалтын буцаалт данс", typeof(string), "");
            ucInventoryList.FieldFindAdd("DiscountAccountNo", "Борлуулалтын хөнгөлөлтийн данс", typeof(string), "");
            ucInventoryList.FieldFindAdd("BonusAccountNo", "Урамшууллын данс", typeof(string), "");
            ucInventoryList.FieldFindAdd("BonusExpAccountNo", "Урамшууллын зардлын данс", typeof(string), "");

            ucInventoryList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucInventoryList.FieldFindRefresh();
            InitDictionary();
            ucInventoryList.VisibleFind = true;
        }
        private void Init1(string pParam)
        {
            ucInventoryList.EventSelected += new ucGridPanel.delegateEventSelected(ucInventoryList_EventSelected);
            ucInventoryList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucInventoryList_EventDataChanged);
            ucInventoryList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucInventoryList_EventFindPaging);
            ucInventoryList.gridView1.RowStyle += new RowStyleEventHandler(gridView1_RowStyle);
            labelControl1.BackColor = Color.NavajoWhite;
            //         labelControl3.BackColor = Color.White;
            labelControl2.BackColor = Color.Orange;

            labelControl1.ForeColor = Color.NavajoWhite;
            //   labelControl3.ForeColor = Color.White;
            labelControl2.ForeColor = Color.Orange;
            ucInventoryList.FindItemAdd("InvID", "Бараа материалын дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucInventoryList.FieldFindAdd("InvTypeID", "Бараа материалын төрөл", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("Name", "Нэр", typeof(string), "");
            ucInventoryList.FieldFindAdd("Name2", "Нэр 2", typeof(string), "");
            ucInventoryList.FieldFindAdd("BranchNo", "Салбарын дугаар", typeof(ArrayList), "");

            ucInventoryList.FieldFindAdd("CreateUser", "Үүсгэсэн хэрэглэгч", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("UnitTypeCode", "Хэмжээсийн төрөл", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("BalanceCount", "Тоо, ширхэг", typeof(int), "");
            ucInventoryList.FieldFindAdd("UnitCost ", "Нэгж үнэ", typeof(decimal), "");
            ucInventoryList.FieldFindAdd("BalanceTotal", "Нийт дүн", typeof(decimal), "");

            ucInventoryList.FieldFindAdd("CurrCode", "Валютын код", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("Position", "Байршил", typeof(ArrayList), "");
            ucInventoryList.FindItemAdd("AccountNo", "Дансны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucInventoryList.FieldFindAdd("EmpNo", "Хариуцагч", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("LastTellerTxnDate", "Сүүлийн гүйлгээ хийсэн огноо", typeof(DateTime), "");

            ucInventoryList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucInventoryList.FieldFindAdd("LEVELNO", "Зэрэглэл", typeof(ArrayList), "");

            ucInventoryList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucInventoryList.FieldFindRefresh();
            InitDictionary();
            ucInventoryList.VisibleFind = true;
            if (Static.ToStr(pParam) != "")
            {
                ucInventoryList.FindItemSetValue("InvID", pParam);
            }
        }
        void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

                string Status = View.GetRowCellDisplayText(e.RowHandle, View.Columns[20]);
                if (Status == "0")
                {
                    ucInventoryList.gridView1.Appearance.OddRow.BackColor = Color.NavajoWhite;
                    ucInventoryList.gridView1.Appearance.OddRow.BackColor2 = Color.NavajoWhite;
                    ucInventoryList.gridView1.Appearance.EvenRow.BackColor = Color.NavajoWhite;
                    ucInventoryList.gridView1.Appearance.EvenRow.BackColor2 = Color.NavajoWhite;
                    e.Appearance.BackColor = Color.NavajoWhite;
                    e.Appearance.BackColor2 = Color.NavajoWhite;
          

                }
                if (Status == "1")
                {
                    ucInventoryList.gridView1.Appearance.OddRow.BackColor = Color.Orange;
                    ucInventoryList.gridView1.Appearance.OddRow.BackColor2 = Color.Orange;
                    ucInventoryList.gridView1.Appearance.EvenRow.BackColor = Color.Orange;
                    ucInventoryList.gridView1.Appearance.EvenRow.BackColor2 = Color.Orange;
                    e.Appearance.BackColor = Color.Orange;
                    e.Appearance.BackColor2 = Color.Orange;
                }

            }
        }
        private Result InitDictionary()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";
            DictUtility.PrivNo = PrivNo;
            try
            {
                string[] names = new string[] { "INVTYPE", "UNITTYPECODE", "BRAND", "INVCAT" };
                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д бараа материалын төрлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucInventoryList.FindItemSetList("InvType", DT, "invtype", "name");
                }

                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д UNITTYPECODE мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucInventoryList.FindItemSetList("Unit", DT, "UNITTYPECODE", "NAME");
                }

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д BrandId мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucInventoryList.FindItemSetList("BrandId", DT, "brandid", "NAME");
                }

                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "Dictionary-д INVCAT мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucInventoryList.FindItemSetList("CatCode", DT, "catcode", "NAME");
                }

                ucInventoryList.FindItemSetList("Status", 0, "ИДЭВХТЭЙ");
                ucInventoryList.FindItemSetList("Status", 1, "ИДЭВХГҮЙ");

                ucInventoryList.FindItemSetList("PrinterType", 0, "Bill Printer");
                ucInventoryList.FindItemSetList("PrinterType", 1, "Lift Printer");

                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                res.ResultNo = 1;
                this.Close();
            }
            return res;
        }
        #endregion
        #region [ EventFind ]
        void ucInventoryList_EventFind(object[] values)
        {
            LoadData(values);
        }
        #endregion
        #region [ EventDataChanged ]
        void ucInventoryList_EventDataChanged()
        {
//a.InvId, a.InvType, it.name as InvTypeName, a.Name, a.Name2, 
//a.BrandId, b.name as BrandIdName, a.PriceAmount, a.PriceRefund, a.Count, 
//a.CatCode, ic.name as CatCodeName, a.BarCode, a.Unit, ut.name as unittypecodename, 
//a.UnitSize, a.PrinterType, a.CreateDate, a.Note, a.Status,
//decode(a.Status, 0, 'Идэвхтэй', 1, 'Идэвхгүй') as StatusName, a.SalesAccountNo, a.RefundAccountNo, a.DiscountAccountNo, a.BonusAccountNo

            ucInventoryList.FieldLinkSetColumnCaption(0, "Бараа материалын дугаар");
            ucInventoryList.FieldLinkSetColumnCaption(1, "Бараа материалын төрөл");
            ucInventoryList.FieldLinkSetColumnCaption(2, "Бараа материалын төрлийн нэр");
            ucInventoryList.FieldLinkSetColumnCaption(3, "Нэр");
            ucInventoryList.FieldLinkSetColumnCaption(4, "Нэр 2");

            ucInventoryList.FieldLinkSetColumnCaption(5, "Брэндийн дугаар");
            ucInventoryList.FieldLinkSetColumnCaption(6, "Брэндийн нэр");
            ucInventoryList.FieldLinkSetColumnCaption(7, "Үнэ");
            ucInventoryList.FieldLinkSetColumnCaption(8, "Нөхөн төлбөрийн дүн");
            ucInventoryList.FieldLinkSetColumnCaption(9, "Нийт тоо ширхэг");

            ucInventoryList.FieldLinkSetColumnCaption(10, "Ангилалын код");
            ucInventoryList.FieldLinkSetColumnCaption(11, "Ангиллын нэр");
            ucInventoryList.FieldLinkSetColumnCaption(12, "Бар код");
            ucInventoryList.FieldLinkSetColumnCaption(13, "Хэмжих нэгжийн төрөл");
            ucInventoryList.FieldLinkSetColumnCaption(14, "Хэмжих нэгж");

            ucInventoryList.FieldLinkSetColumnCaption(15, "Хэмжээ, размер");
            ucInventoryList.FieldLinkSetColumnCaption(16, "Принтерийн төрөл");
            ucInventoryList.FieldLinkSetColumnCaption(17, "Үүсгэсэн огноо");

            ucInventoryList.FieldLinkSetColumnCaption(18, "Барааны тайлбар");
            ucInventoryList.FieldLinkSetColumnCaption(19, "Төлөв");
            ucInventoryList.FieldLinkSetColumnCaption(20, "Төлөв");
            ucInventoryList.FieldLinkSetColumnCaption(21, "Борлуулалтын орлогын данс");
            ucInventoryList.FieldLinkSetColumnCaption(22, "Борлуулалтын буцаалт данс");

            ucInventoryList.FieldLinkSetColumnCaption(23, "Борлуулалтын хөнгөлөлтийн данс");
            ucInventoryList.FieldLinkSetColumnCaption(24, "Урамшууллын данс");
            ucInventoryList.FieldLinkSetColumnCaption(25, "Урамшууллын зардлын данс");
            ucInventoryList.FieldLinkSetColumnCaption(26, "Түрээсийн бараа эсэх");

            FormUtility.RestoreStateGrid(appname, formname, ref ucInventoryList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucInventoryList.ucParameterPanel1.vGridControl1, ref ucInventoryList.groupControl1);
        }
        #endregion
        #region [ EventSelected ]
        void ucInventoryList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucInventoryList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToStr(selectedrow["InvId"]);

                    EServ.Shared.Static.Invoke("InfoPos.Parameter.dll", "InfoPos.Parameter.Main", "CallFormInvMain", obj);
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
        #endregion
        #region [ LoadData ]
        void LoadData(object[] values)
        {
            Result res = new Result();

            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140211, 140211, values);

                if (res.ResultNo == 0)
                {
                    ucInventoryList.DataSource = res.Data.Tables[0];
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        private void InventoryList_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucInventoryList.OnEventFindPaging(0);
        }
        private void InventoryList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucInventoryList.OnEventFindPaging(0);

            }
        }
        private void InventoryList_Enter(object sender, EventArgs e)
        {
           
             
        }
        private void InventoryList_DragEnter(object sender, DragEventArgs e)
        {

        }
        private void InventoryList_KeyUp(object sender, KeyEventArgs e)
        {
          
        }
        private void InventoryList_FormClosing(object sender, FormClosingEventArgs e)
        {

            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucInventoryList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucInventoryList.ucParameterPanel1.vGridControl1, ref ucInventoryList.groupControl1);
        }
        private void ucInventoryList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {

            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140211, 140211, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucInventoryList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
