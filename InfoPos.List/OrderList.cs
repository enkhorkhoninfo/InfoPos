using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;
using ISM.Template;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;

using DevExpress.Data;
using DevExpress.XtraGrid.GroupSummaryEditor;

namespace InfoPos.List
{
    public partial class OrderList : Form
    {
        private Core.Core _core;
        int PrivNo = 130101;
        string appname = "", formname = "";
        Form FormName = null;
        public OrderList(Core.Core core)
        {
            //this.Show();
            InitializeComponent();
            _core = core;
            Init();
            ucOrderList.Resource = _core.Resource;

            //ContractList.ActiveForm.AcceptButton = ucContractList.btnFind;
        }
        public OrderList(Core.Core core, string[] pParam)
        {
            //this.Show();
            InitializeComponent();
            _core = core;
            Init1(pParam);

            ucOrderList.Resource = _core.Resource;
            //ContractList.ActiveForm.AcceptButton = ucContractList.btnFind;
        }
        public OrderList(Core.Core core, long ppContractID)
        {
            //this.Show();
            InitializeComponent();
            _core = core;
            //ppContractID = _ContractID;
            //ContractList.ActiveForm.AcceptButton = ucContractList.btnFind;
        }
        #region [ Init ]
        private void Init()
        {
            ucOrderList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucContractList_EventDataChanged);
            ucOrderList.EventSelected += new ucGridPanel.delegateEventSelected(ucContractList_EventSelected);
            ucOrderList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucContractList_EventFindPaging);
           
            ucOrderList.gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            ucOrderList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
            ucOrderList.gridView1.Appearance.OddRow.Options.UseBackColor = false;

            labelControl1.BackColor = Color.SkyBlue;
            labelControl1.ForeColor = Color.SkyBlue;

            labelControl3.BackColor = Color.Orange;
            labelControl3.ForeColor = Color.Orange;

            labelControl6.BackColor = Color.LimeGreen;
            labelControl6.ForeColor = Color.LimeGreen;

            labelControl8.BackColor = Color.DeepPink;
            labelControl8.ForeColor = Color.DeepPink;

            Function();
            //"a.OrderNo like","a.CustNo like","c.FirstName like","c.LastName like","c.CorporateName like",
            //"a.ConfirmTerm","a.TermType","a.OrderAmount","a.PrepaidAmount","a.CurCode",
            //"a.Fee","a.StartDate","a.EndDate","a.PersonCount","a.Status",
            //"a.CreateDate","a.PostDate","a.CreateUser","a.OwnerUser"

            ucOrderList.FieldFindAdd("OrderNo", "Гэрээний дугаар", typeof(string), "");
            ucOrderList.FindItemAdd("CustNo", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucOrderList.FieldFindAdd("FirstName", "Харилцагчийн эцэг эхийн нэр", typeof(string), "");
            ucOrderList.FieldFindAdd("LastName", "Харилцагчийн нэр", typeof(string), "");
            ucOrderList.FieldFindAdd("CorporateName", "Компаний нэр", typeof(string), "");

            ucOrderList.FieldFindAdd("ConfirmTerm", "Баталгаажуулга хийх хугацаа", typeof(string), "");
            ucOrderList.FieldFindAdd("TermType", "Баталгаажуулга хийх хугацааны төрөл", typeof(ArrayList), "");
            ucOrderList.FieldFindAdd("OrderAmount", "Гэрээний үнийн дүн", typeof(long), "");
            ucOrderList.FieldFindAdd("PrepaidAmount", "Урьдчилж төлсөн дүн", typeof(long), "");
            ucOrderList.FieldFindAdd("CurCode", "Валют", typeof(ArrayList), "");

            ucOrderList.FindItemAdd("Fee", "Үйлчилгээний шимтгэл", "", DynamicParameterType.Decimal, false, "d", "");
            ucOrderList.FieldFindAdd("StartDate", "Гэрээ үйлчлэх өдөр", typeof(DateTime), "");
            ucOrderList.FieldFindAdd("EndDate", "Гэрээ дуусах өдөр", typeof(DateTime), "");
            ucOrderList.FieldFindAdd("PersonCount", "Захиалгад хамрагдах хүний тоо", typeof(int), "");
            ucOrderList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");

            ucOrderList.FieldFindAdd("CreateDate", "Үүсэгсэн огноо", typeof(DateTime), "");
            ucOrderList.FieldFindAdd("PostDate", "Гэрээний төлөв", typeof(DateTime), "");
            ucOrderList.FieldFindAdd("CreateUser", "Үүсэгсэн хэрэглэгч", typeof(ArrayList), "");
            ucOrderList.FieldFindAdd("OwnerUser", "Хариуцсан хэрэглэгч", typeof(ArrayList), "");

            ucOrderList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            
            ucOrderList.FieldFindRefresh();
            InitDictionary();

            ucOrderList.VisibleFind = true;
        }
        private void Init1(string[] pParam)
        {
            ucOrderList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucContractList_EventDataChanged);
            ucOrderList.EventSelected += new ucGridPanel.delegateEventSelected(ucContractList_EventSelected);
            ucOrderList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucContractList_EventFindPaging);

            ucOrderList.gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            ucOrderList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
            ucOrderList.gridView1.Appearance.OddRow.Options.UseBackColor = false;

            labelControl1.BackColor = Color.SkyBlue;
            labelControl1.ForeColor = Color.SkyBlue;

            labelControl3.BackColor = Color.Orange;
            labelControl3.ForeColor = Color.Orange;

            labelControl6.BackColor = Color.LimeGreen;
            labelControl6.ForeColor = Color.LimeGreen;

            labelControl8.BackColor = Color.DeepPink;
            labelControl8.ForeColor = Color.DeepPink;

            Function();

//"a.OrderNo like","a.CustNo like","c.FirstName like","c.LastName like","c.CorporateName like",
//"a.ConfirmTerm","a.TermType","a.OrderAmount","a.PrepaidAmount","a.CurCode",
//"a.Fee","a.StartDate","a.EndDate","a.PersonCount","a.Status",
//"a.CreateDate","a.PostDate","a.CreateUser","a.OwnerUser"

            ucOrderList.FieldFindAdd("OrderNo", "Гэрээний дугаар", typeof(string), "");
            ucOrderList.FindItemAdd("CustNo", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucOrderList.FieldFindAdd("FirstName", "Харилцагчийн эцэг эхийн нэр", typeof(string), "");
            ucOrderList.FieldFindAdd("LastName", "Харилцагчийн нэр", typeof(string), "");
            ucOrderList.FieldFindAdd("CorporateName", "Компаний нэр", typeof(string), "");

            ucOrderList.FieldFindAdd("ConfirmTerm", "Баталгаажуулга хийх хугацаа", typeof(string), "");
            ucOrderList.FieldFindAdd("TermType", "Баталгаажуулга хийх хугацааны төрөл", typeof(ArrayList), "");
            ucOrderList.FieldFindAdd("OrderAmount", "Гэрээний үнийн дүн", typeof(long), "");
            ucOrderList.FieldFindAdd("PrepaidAmount", "Урьдчилж төлсөн дүн", typeof(long), "");
            ucOrderList.FieldFindAdd("CurCode", "Валют", typeof(ArrayList), "");

            ucOrderList.FindItemAdd("Fee", "Үйлчилгээний шимтгэл", "", DynamicParameterType.Decimal, false, "d", "");
            ucOrderList.FieldFindAdd("StartDate", "Гэрээ үйлчлэх өдөр", typeof(DateTime), "");
            ucOrderList.FieldFindAdd("EndDate", "Гэрээ дуусах өдөр", typeof(DateTime), "");
            ucOrderList.FieldFindAdd("PersonCount", "Захиалгад хамрагдах хүний тоо", typeof(int), "");
            ucOrderList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");

            ucOrderList.FieldFindAdd("CreateDate", "Үүсэгсэн огноо", typeof(DateTime), "");
            ucOrderList.FieldFindAdd("PostDate", "Гэрээний төлөв", typeof(DateTime), "");
            ucOrderList.FieldFindAdd("CreateUser", "Үүсэгсэн хэрэглэгч", typeof(ArrayList), "");
            ucOrderList.FieldFindAdd("OwnerUser", "Хариуцсан хэрэглэгч", typeof(ArrayList), "");

            ucOrderList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";

            ucOrderList.FieldFindRefresh();
            InitDictionary();
            ucOrderList.VisibleFind = true;
            ucOrderList.FindItemSetValue("OrderNo", pParam[2]);
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
                string[] names = new string[] { "USERS", "CURRENCY" };
                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д хэрэглэгчийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucOrderList.FindItemSetList("CreateUser", DT, "userno", "userlname");
                    ucOrderList.FindItemSetList("OwnerUser", DT, "userno", "userlname");
                }

                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д CURRENCY мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucOrderList.FindItemSetList("CurCode", DT, "CURRENCY", "NAME");
                }

                ucOrderList.FindItemSetList("Status", 0, "ЦУЦЛАГДСАН");
                ucOrderList.FindItemSetList("Status", 1, "ИДЭВХТЭЙ");
                ucOrderList.FindItemSetList("Status", 2, "БАТАЛГААЖСАН");

                ucOrderList.FindItemSetList("TermType", "T", "ЦАГ");
                ucOrderList.FindItemSetList("TermType", "D", "ӨДӨР");
                ucOrderList.FindItemSetList("TermType", "W", "ГАРАГ");
                ucOrderList.FindItemSetList("TermType", "M", "САР");

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
        #region [ EventDataChanged ]
        void ucContractList_EventDataChanged()
        {

//a.OrderNo, a.CustNo, c.FirstName, c.LastName, c.CorporateName,
//a.ConfirmTerm, a.TermType, decode(a.TermType, 'T', 'Цаг', 'D', 'Өдөр', 'W', 'Гараг', 'M', 'Сар') as TermTypeNAME, a.OrderAmount,
//a.PrepaidAmount, a.CurCode, a.Fee, a.StartDate, a.EndDate,
//a.PersonCount, a.Status, decode(a.Status, 0, 'Цуцлагдсан', 1, 'Идэвхтэй', 2, 'Баталгаажсан') as StatusName, a.CreateDate, a.PostDate,
//a.CreateUser, a.OwnerUser

            //ucOrderList.FieldLinkSetColumnCaption(0, "Захиалгын дугаар");
            //ucOrderList.FieldLinkSetColumnCaption(1, "Харилцагчийн дугаар");
            //ucOrderList.FieldLinkSetColumnCaption(2, "Харилцагчийн эцэг эхийн нэр");
            //ucOrderList.FieldLinkSetColumnCaption(3, "Харилцагчийн нэр");
            //ucOrderList.FieldLinkSetColumnCaption(4, "Компаний нэр");

            //ucOrderList.FieldLinkSetColumnCaption(5, "Баталгаажуулга хийх хугацаа");
            //ucOrderList.FieldLinkSetColumnCaption(6, "Баталгаажуулга хийх хугацааны төрөл");
            //ucOrderList.FieldLinkSetColumnCaption(7, "Баталгаажуулга хийх хугацааны төрлийн нэр");

            //ucOrderList.FieldLinkSetColumnCaption(8, "Гэрээний үнийн дүн");
            //ucOrderList.gridView1.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //ucOrderList.gridView1.Columns[8].DisplayFormat.FormatString = "{0:n2}";

            //ucOrderList.FieldLinkSetColumnCaption(9, "Урьдчилж төлсөн дүн");
            //ucOrderList.gridView1.Columns[9].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //ucOrderList.gridView1.Columns[9].DisplayFormat.FormatString = "{0:n2}";

            //ucOrderList.FieldLinkSetColumnCaption(10, "Валют");
            //ucOrderList.FieldLinkSetColumnCaption(11, "Үйлчилгээний шимтгэл");
            //ucOrderList.gridView1.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //ucOrderList.gridView1.Columns[11].DisplayFormat.FormatString = "{0:n2}";

            //ucOrderList.FieldLinkSetColumnCaption(12, "Гэрээ үйлчлэх өдөр");
            //ucOrderList.FieldLinkSetColumnCaption(13, "Гэрээ дуусах өдөр");
            //ucOrderList.FieldLinkSetColumnCaption(14, "Захиалгад хамрагдах хүний тоо");

            //ucOrderList.FieldLinkSetColumnCaption(15, "Гэрээний төлөв");
            //ucOrderList.FieldLinkSetColumnCaption(16, "Гэрээний төлөв");

            //ucOrderList.FieldLinkSetColumnCaption(17, "Үүсэгсэн огноо");
            //ucOrderList.FieldLinkSetColumnCaption(18, "Үүсэгсэн огноо цаг");
            //ucOrderList.FieldLinkSetColumnCaption(19, "Үүсэгсэн хэрэглэгч");
            //ucOrderList.FieldLinkSetColumnCaption(20, "Хариуцсан хэрэглэгч");

            //ucOrderList.gridView1.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
           
            //// Customize the total summary.

            //ucOrderList.gridView1.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //ucOrderList.gridView1.Columns[0].SummaryItem.DisplayFormat = "Нийт гэрээний тоо :{0:C2}";
            //ucOrderList.gridView1.Columns[0].SummaryItem.Tag = 1;
            //(ucOrderList.gridView1.Columns[0].View as GridView).OptionsView.ShowFooter = true;

            //ucOrderList.gridView1.OptionsView.ShowGroupPanel = false;
            //ucOrderList.gridView1.ExpandAllGroups();

            //ucOrderList.gridView1.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //ucOrderList.gridView1.Columns[8].SummaryItem.DisplayFormat = "Нийт:{0:C2}";
            //ucOrderList.gridView1.Columns[8].SummaryItem.Tag = 1;
            //(ucOrderList.gridView1.Columns[8].View as GridView).OptionsView.ShowFooter = true;

            //ucOrderList.gridView1.OptionsView.ShowGroupPanel = true;
            //ucOrderList.gridView1.ExpandAllGroups();


            //ucOrderList.gridView1.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //ucOrderList.gridView1.Columns[9].SummaryItem.DisplayFormat = "Нийт:{0:C2}";
            //ucOrderList.gridView1.Columns[9].SummaryItem.Tag = 1;
            //(ucOrderList.gridView1.Columns[9].View as GridView).OptionsView.ShowFooter = true;


            // // Create and setup the first summary item.
            //GridGroupSummaryItem item = new GridGroupSummaryItem();
            //item.FieldName = "OrderNo";
            //item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //ucOrderList.gridView1.GroupSummary.Add(item);


            //DevExpress.XtraGrid.GridGroupSummaryItem item1 = new DevExpress.XtraGrid.GridGroupSummaryItem();
            //item1.FieldName = "Amount";

            //item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //item1.DisplayFormat = "Нийт гэрээний үнийн дүн {0:c2}";
            //item1.Tag = 1;
            //item1.ShowInGroupColumnFooter = ucOrderList.gridView1.Columns["OrderAmount"];
     
            //ucOrderList.gridView1.GroupSummary.Add(item1);


            //DevExpress.XtraGrid.GridGroupSummaryItem item2 = new DevExpress.XtraGrid.GridGroupSummaryItem();
            //item2.FieldName = "Balance";

            //item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //item2.DisplayFormat = "Нийт урьдчилж төлсөн дүн {0:c2}";
            //item2.Tag = 1;
            //item2.ShowInGroupColumnFooter = ucOrderList.gridView1.Columns["PrepaidAmount"];

            //ucOrderList.gridView1.GroupSummary.Add(item2);

            FormUtility.RestoreStateGrid(appname, formname, ref ucOrderList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucOrderList.ucParameterPanel1.vGridControl1, ref ucOrderList.groupControl1);
        }
        #endregion
        #region [ EventSelected ]
        void ucContractList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucOrderList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToStr(selectedrow["OrderNo"]);
                    EServ.Shared.Static.Invoke("InfoPos.Order.dll", "InfoPos.Order.Main", "CallOrder", obj);
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 130101, 130101, values);

                if (res.ResultNo == 0)
                {
                    ucOrderList.DataSource = res.Data.Tables[0];
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
        #region [ Button ]
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ucOrderList.DataSource != null)
            {
                DataRow DR = ucOrderList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucOrderList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    //object[] obj = new object[2];
                    //obj[0] = DR;
                    
                    //InfoPos.Enquiry.ContractEnquiry ConEn = new InfoPos.Enquiry.ContractEnquiry(_core, pContractID, obj);
                    //ConEn.ShowDialog();
                }
            }
        }
        void Function()
        {
            if (_core.Resource != null)
            {
                simpleButton1.Image = _core.Resource.GetImage("navigate_refresh");
            }
        }
        #endregion
        private void ContractList_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucOrderList.OnEventFindPaging(0);
        }
        void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string Status = View.GetRowCellDisplayText(e.RowHandle, View.Columns[15]);

                if (Status == "0")
                {
                    e.Appearance.BackColor = Color.SkyBlue;
                    e.Appearance.BackColor2 = Color.SkyBlue;
                }
                if (Status == "1")
                {
                    e.Appearance.BackColor = Color.LimeGreen;
                    e.Appearance.BackColor2 = Color.LimeGreen;
                }
                if (Status == "8")
                {
                    e.Appearance.BackColor = Color.DeepPink;
                    e.Appearance.BackColor2 = Color.DeepPink;
                }
                if (Status == "9")
                {
                    e.Appearance.BackColor = Color.Orange;
                    e.Appearance.BackColor2 = Color.Orange;
                }
            }
        }
        private void ContractList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                ucOrderList.OnEventFindPaging(0);
            }
        }
        private void ContractList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref ucOrderList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucOrderList.ucParameterPanel1.vGridControl1, ref ucOrderList.groupControl1);
        }
        private void ucContractList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            //object[] obj = new object[6];
            //obj[0] = _core.User.BranchCode;
            //obj[1] = _core.RemoteObject.User.UserNo;
            //obj[2] = _core.User.Level1;
            //obj[3] = _core.User.Level2;
            //obj[4] = _core.User.Level3;
            //obj[5] = _core.User.Level4;

            //Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 130001, 130001, pageindex, pagerows, new object[] { values, obj });
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 130101, 130101, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucOrderList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
