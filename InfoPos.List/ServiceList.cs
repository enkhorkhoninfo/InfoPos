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
    public partial class ServiceList : Form
    {
        private Core.Core _core;
        int PrivNo = 140226;
        string appname = "", formname = "";
        Form FormName = null;
        public ServiceList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucServiceList.Resource = _core.Resource;
           //InventoryList.ActiveForm.AcceptButton = ucInventoryList.btnFind;
        }
        public ServiceList(Core.Core core, string pParam)
        {
            InitializeComponent();
            _core = core;
            Init1(pParam);
            ucServiceList.Resource = _core.Resource;
            //InventoryList.ActiveForm.AcceptButton = ucInventoryList.btnFind;
        }
        #region [ Init ]
        private void Init()
        {
            ucServiceList.EventSelected += new ucGridPanel.delegateEventSelected(ucInventoryList_EventSelected);
            ucServiceList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucInventoryList_EventDataChanged);
             ucServiceList.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucInventoryList_EventFindPaging);
             ucServiceList.gridView1.RowStyle += new RowStyleEventHandler(gridView1_RowStyle);
             labelControl1.BackColor = Color.NavajoWhite;
             //         labelControl3.BackColor = Color.White;
             labelControl2.BackColor = Color.Orange;

             labelControl1.ForeColor = Color.NavajoWhite;
             //   labelControl3.ForeColor = Color.White;
             labelControl2.ForeColor = Color.Orange;

//"a.ServID like","a.ServType","a.Name like","a.Name2 like","a.ServStartDate",
//"a.ServEndDate","a.PriceAmount","a.Count","a.CatCode","a.Unit",
//"a.UnitSize","a.PrinterType","a.CreateDate","a.Note like","a.Status", 
//"a.TagType", "a.TagTime", "a.TagTimeMethod", "a.IsSchedule","a.ScheduleType",
//"a.SalesAccountNo like","a.RefundAccountNo like","a.DiscountAccountNo like","a.BonusAccountNo like"          

             ucServiceList.FindItemAdd("ServID", "Үйлчилгээний дугаар", "", DynamicParameterType.Decimal, false, "d", "");
             ucServiceList.FieldFindAdd("ServType", "Үйлчилгээний төрөл", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("Name", "Нэр", typeof(string), "");
            ucServiceList.FieldFindAdd("Name2", "Нэр 2", typeof(string), "");
            ucServiceList.FieldFindAdd("ServStartDate", "Эхлэх огноо", typeof(DateTime), "");

            ucServiceList.FieldFindAdd("ServEndDate", "Дуусах огноо", typeof(DateTime), "");
            ucServiceList.FindItemAdd("PriceAmount", "Үнэ", "", DynamicParameterType.Decimal, false, "d", "");
            ucServiceList.FieldFindAdd("Count", "Нийт тоо ширхэг", typeof(int), "");
            ucServiceList.FieldFindAdd("CatCode ", "Ангилалын код", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("Unit", "Хэмжих нэгж", typeof(ArrayList), "");
            
            ucServiceList.FindItemAdd("UnitSize", "Хэмжээ, размер", "", DynamicParameterType.Decimal, false, "d", "");
            ucServiceList.FieldFindAdd("PrinterType", "Принтерийн төрөл", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("CreateDate", "Үүсгэсэн огноо", typeof(DateTime), "");
            ucServiceList.FieldFindAdd("Note", "Барааны тайлбар", typeof(string), "");

            ucServiceList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("TagType", "Тагийн төрөл", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("TagTime", "Таг дээр бичигдэх цаг", typeof(int), "");
            ucServiceList.FieldFindAdd("TagTimeMethod", "Тагийн цаг тоолж эхлэх арга", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("IsSchedule", "Хуваарьтай эсэх", typeof(ArrayList), "");


            ucServiceList.FieldFindAdd("ScheduleType", "Хуваарийн төрөл", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("SalesAccountNo", "Борлуулалтын орлогын данс", typeof(string), "");
            ucServiceList.FieldFindAdd("RefundAccountNo", "Борлуулалтын буцаалт данс", typeof(string), "");
            ucServiceList.FieldFindAdd("DiscountAccountNo", "Борлуулалтын хөнгөлөлтийн данс", typeof(string), "");
            ucServiceList.FieldFindAdd("BonusAccountNo", "Урамшууллын данс", typeof(string), "");
            ucServiceList.FieldFindAdd("BonusExpAccountNo", "Урамшууллын зардлын данс", typeof(string), "");

            ucServiceList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucServiceList.FieldFindRefresh();
            InitDictionary();
            ucServiceList.VisibleFind = true;
        }
        private void Init1(string pParam)
        {
            ucServiceList.EventSelected += new ucGridPanel.delegateEventSelected(ucInventoryList_EventSelected);
            ucServiceList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucInventoryList_EventDataChanged);
            ucServiceList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucInventoryList_EventFindPaging);
            ucServiceList.gridView1.RowStyle += new RowStyleEventHandler(gridView1_RowStyle);
            labelControl1.BackColor = Color.NavajoWhite;
            //         labelControl3.BackColor = Color.White;
            labelControl2.BackColor = Color.Orange;

            labelControl1.ForeColor = Color.NavajoWhite;
            //   labelControl3.ForeColor = Color.White;
            labelControl2.ForeColor = Color.Orange;
            ucServiceList.FindItemAdd("InvID", "Бараа материалын дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucServiceList.FieldFindAdd("InvTypeID", "Бараа материалын төрөл", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("Name", "Нэр", typeof(string), "");
            ucServiceList.FieldFindAdd("Name2", "Нэр 2", typeof(string), "");
            ucServiceList.FieldFindAdd("BranchNo", "Салбарын дугаар", typeof(ArrayList), "");

            ucServiceList.FieldFindAdd("CreateUser", "Үүсгэсэн хэрэглэгч", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("UnitTypeCode", "Хэмжээсийн төрөл", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("BalanceCount", "Тоо, ширхэг", typeof(int), "");
            ucServiceList.FieldFindAdd("UnitCost ", "Нэгж үнэ", typeof(decimal), "");
            ucServiceList.FieldFindAdd("BalanceTotal", "Нийт дүн", typeof(decimal), "");

            ucServiceList.FieldFindAdd("CurrCode", "Валютын код", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("Position", "Байршил", typeof(ArrayList), "");
            ucServiceList.FindItemAdd("AccountNo", "Дансны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucServiceList.FieldFindAdd("EmpNo", "Хариуцагч", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("LastTellerTxnDate", "Сүүлийн гүйлгээ хийсэн огноо", typeof(DateTime), "");

            ucServiceList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucServiceList.FieldFindAdd("LEVELNO", "Зэрэглэл", typeof(ArrayList), "");

            ucServiceList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucServiceList.FieldFindRefresh();
            InitDictionary();
            ucServiceList.VisibleFind = true;
            if (Static.ToStr(pParam) != "")
            {
                ucServiceList.FindItemSetValue("InvID", pParam);
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
                    ucServiceList.gridView1.Appearance.OddRow.BackColor = Color.NavajoWhite;
                    ucServiceList.gridView1.Appearance.OddRow.BackColor2 = Color.NavajoWhite;
                    ucServiceList.gridView1.Appearance.EvenRow.BackColor = Color.NavajoWhite;
                    ucServiceList.gridView1.Appearance.EvenRow.BackColor2 = Color.NavajoWhite;
                    e.Appearance.BackColor = Color.NavajoWhite;
                    e.Appearance.BackColor2 = Color.NavajoWhite;
          

                }
                if (Status == "1")
                {
                    ucServiceList.gridView1.Appearance.OddRow.BackColor = Color.Orange;
                    ucServiceList.gridView1.Appearance.OddRow.BackColor2 = Color.Orange;
                    ucServiceList.gridView1.Appearance.EvenRow.BackColor = Color.Orange;
                    ucServiceList.gridView1.Appearance.EvenRow.BackColor2 = Color.Orange;
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
                string[] names = new string[] { "SERVTYPE", "UNITTYPECODE", "TAGSETUP", "INVCAT", "SCHEDULETYPE" };
                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д үйлчилгээний төрлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucServiceList.FindItemSetList("ServType", DT, "invtype", "name");
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
                    ucServiceList.FindItemSetList("Unit", DT, "UNITTYPECODE", "NAME");
                }

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д TagType мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucServiceList.FindItemSetList("TagType", DT, "tagtype", "NAME");
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
                    ucServiceList.FindItemSetList("CatCode", DT, "catcode", "NAME");
                }

                DT = (DataTable)Tables[4];
                if (DT == null)
                {
                    msg = "Dictionary-д SCHEDULETYPE мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucServiceList.FindItemSetList("ScheduleType", DT, "SCHEDULETYPE", "NAME");
                }

                ucServiceList.FindItemSetList("Status", 0, "ИДЭВХТЭЙ");
                ucServiceList.FindItemSetList("Status", 1, "ИДЭВХГҮЙ");

                ucServiceList.FindItemSetList("PrinterType", 0, "BILL PRINTER");
                ucServiceList.FindItemSetList("PrinterType", 1, "LIFT PRINTER");

                ucServiceList.FindItemSetList("IsSchedule", 0, "ХУВААРЬГҮЙ");
                ucServiceList.FindItemSetList("IsSchedule", 1, "ХУВААРЬТАЙ");

                ucServiceList.FindItemSetList("TagTimeMethod", 0, "БОРЛУУЛАЛТ ХИЙГДСЭНЭЭР");
                ucServiceList.FindItemSetList("TagTimeMethod", 1, "ТҮРЭЭСЭЭР ОЛГОСНООР");
                ucServiceList.FindItemSetList("TagTimeMethod", 2, "АНХ ТАГ УНШУУЛСНААР");

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
//a.ServID, a.ServType, it.name as ServTypeName, a.Name, a.Name2,
//a.ServStartDate, a.ServEndDate, a.PriceAmount, a.Count, a.CatCode,  ic.name as CatCodeName,
//a.Unit, ut.name as UnitTypeName, a.UnitSize, a.PrinterType, a.CreateDate, 
//a.Note, a.Status,  decode(a.Status, 0, 'Идэвхтэй', 1, 'Идэвхгүй') as StatusName,a.TagType, tp.name as TagTypeName, 
//a.TagTime, a.TagTimeMethod, decode(a.TagTimeMethod, 0, 'Борлуулалт хийгдсэнээр', 1, 'Түрээсээр олгосноор', 2, 'Анх таг уншуулснаар') as TagTimeMethodName,a.IsSchedule, decode(a.IsSchedule, 0, 'Хуваарьгүй', 1, 'Хуваарьтай') as IsScheduleName, 
//a.ScheduleType, st.name as ScheduleTypeName, a.SalesAccountNo, a.RefundAccountNo, a.DiscountAccountNo, 
//a.BonusAccountNo

            ucServiceList.FieldLinkSetColumnCaption(0, "Үйлчилгээний дугаар");
            ucServiceList.FieldLinkSetColumnCaption(1, "Үйлчилгээний төрөл");
            ucServiceList.FieldLinkSetColumnCaption(2, "Үйлчилгээний төрлийн нэр");
            ucServiceList.FieldLinkSetColumnCaption(3, "Нэр");
            ucServiceList.FieldLinkSetColumnCaption(4, "Нэр 2");

            ucServiceList.FieldLinkSetColumnCaption(5, "Эхлэх огноо");
            ucServiceList.FieldLinkSetColumnCaption(6, "Дуусах огноо");
            ucServiceList.FieldLinkSetColumnCaption(7, "Үнэ");
            ucServiceList.FieldLinkSetColumnCaption(8, "Нийт тоо ширхэг");
            ucServiceList.FieldLinkSetColumnCaption(9, "Ангилалын код");

            ucServiceList.FieldLinkSetColumnCaption(10, "Ангиллын нэр");
            ucServiceList.FieldLinkSetColumnCaption(11, "Хэмжих нэгжийн төрөл");
            ucServiceList.FieldLinkSetColumnCaption(12, "Хэмжих нэгж");
            ucServiceList.FieldLinkSetColumnCaption(13, "Хэмжээ, размер");
            ucServiceList.FieldLinkSetColumnCaption(14, "Принтерийн төрөл");

            ucServiceList.FieldLinkSetColumnCaption(15, "Үүсгэсэн огноо");
            ucServiceList.FieldLinkSetColumnCaption(16, "Барааны тайлбар");
            ucServiceList.FieldLinkSetColumnCaption(17, "Төлөв");
            ucServiceList.FieldLinkSetColumnCaption(18, "Төлөв");
            ucServiceList.FieldLinkSetColumnCaption(19, "Тагийн төрөл");

            ucServiceList.FieldLinkSetColumnCaption(20, "Тагийн төрлийн нэр");
            ucServiceList.FieldLinkSetColumnCaption(21, "Таг дээр бичигдэх цаг");
            ucServiceList.FieldLinkSetColumnCaption(22, "Тагийн цаг тоолж эхлэх арга");
            ucServiceList.FieldLinkSetColumnCaption(23, "Тагийн цаг тоолж эхлэх арга");
            ucServiceList.FieldLinkSetColumnCaption(24, "Хуваарьтай эсэх");

            ucServiceList.FieldLinkSetColumnCaption(25, "Хуваарьтай эсэх");
            ucServiceList.FieldLinkSetColumnCaption(26, "Хуваарийн төрөл");
            ucServiceList.FieldLinkSetColumnCaption(27, "Хуваарийн төрөл");
            ucServiceList.FieldLinkSetColumnCaption(28, "Борлуулалтын орлогын данс");
            ucServiceList.FieldLinkSetColumnCaption(29, "Борлуулалтын буцаалт данс");

            ucServiceList.FieldLinkSetColumnCaption(30, "Борлуулалтын хөнгөлөлтийн данс");
            ucServiceList.FieldLinkSetColumnCaption(31, "Урамшууллын данс");
            ucServiceList.FieldLinkSetColumnCaption(32, "Урамшууллын зардлын данс");

            FormUtility.RestoreStateGrid(appname, formname, ref ucServiceList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucServiceList.ucParameterPanel1.vGridControl1, ref ucServiceList.groupControl1);
        }
        #endregion
        #region [ EventSelected ]
        void ucInventoryList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucServiceList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToStr(selectedrow["ServId"]);

                    EServ.Shared.Static.Invoke("InfoPos.Parameter.dll", "InfoPos.Parameter.Main", "CallFormServMain", obj);
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140226, 140226, values);

                if (res.ResultNo == 0)
                {
                    ucServiceList.DataSource = res.Data.Tables[0];
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
            ucServiceList.OnEventFindPaging(0);
        }
        private void InventoryList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucServiceList.OnEventFindPaging(0);

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

            FormUtility.SaveStateGrid(appname, formname, ref ucServiceList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucServiceList.ucParameterPanel1.vGridControl1, ref ucServiceList.groupControl1);
        }
        private void ucInventoryList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {

            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140226, 140226, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucServiceList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
