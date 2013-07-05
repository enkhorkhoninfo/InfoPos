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
    public partial class ContractList : Form
    {
        private Core.Core _core;
        int PrivNo = 130001;
        string appname = "", formname = "";
        Form FormName = null;
        public ContractList(Core.Core core)
        {
            //this.Show();
            InitializeComponent();
            _core = core;
            Init();
            ucContractList.Resource = _core.Resource;

            //ContractList.ActiveForm.AcceptButton = ucContractList.btnFind;
        }
        public ContractList(Core.Core core, string[] pParam)
        {
            //this.Show();
            InitializeComponent();
            _core = core;
            Init1(pParam);

            ucContractList.Resource = _core.Resource;
            //ContractList.ActiveForm.AcceptButton = ucContractList.btnFind;
        }
        public ContractList(Core.Core core, long ppContractID)
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
            ucContractList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucContractList_EventDataChanged);
            ucContractList.EventSelected += new ucGridPanel.delegateEventSelected(ucContractList_EventSelected);
            ucContractList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucContractList_EventFindPaging);
           
            ucContractList.gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            ucContractList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
            ucContractList.gridView1.Appearance.OddRow.Options.UseBackColor = false;

            labelControl1.BackColor = Color.SkyBlue;
            labelControl3.BackColor = Color.Orange;
            labelControl1.ForeColor = Color.SkyBlue;
            labelControl3.ForeColor = Color.Orange;

            Function();

            ucContractList.FieldFindAdd("ContractNo", "Гэрээний дугаар", typeof(string), "");
            ucContractList.FieldFindAdd("ContractType", "Гэрээний төрөл", typeof(ArrayList), "");
            ucContractList.FindItemAdd("CustNo", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucContractList.FieldFindAdd("FirstName", "Харилцагчийн эцэг эхийн нэр", typeof(string), "");
            ucContractList.FieldFindAdd("LastName", "Харилцагчийн нэр", typeof(string), "");

            ucContractList.FieldFindAdd("CorporateName", "Компаний нэр", typeof(string), "");
            ucContractList.FieldFindAdd("ValidStartDate", "Эхлэх огноо", typeof(DateTime), "");
            ucContractList.FindItemAdd("ValidStartTime", "Эхлэх цаг", "", DynamicParameterType.DateTime, false, "T", "");
            ucContractList.FieldFindAdd("ValidEndDate", "Дуусах огноо", typeof(DateTime), "");
            ucContractList.FindItemAdd("ValidEndTime", "Дуусах цаг", "", DynamicParameterType.DateTime, false, "T", "");

            ucContractList.FindItemAdd("Amount", "Гэрээний үнийн дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucContractList.FindItemAdd("Balance", "Гэрээний үлдэгдэл үнэ", "", DynamicParameterType.Decimal, false, "d", "");
            ucContractList.FieldFindAdd("CurCode", "Валют", typeof(ArrayList), "");
            ucContractList.FieldFindAdd("PersonCount", "Гэрээнд хамрагдах үйчлүүлэгчийн тоо", typeof(int), "");
            ucContractList.FieldFindAdd("DepFreq", "Гэрээний дүнг элэгдүүлэх давтамж", typeof(ArrayList), "");

            ucContractList.FindItemAdd("DepAmount", "Гэрээний дүнг элэгдүүлэх дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucContractList.FieldFindAdd("Status", "Гэрээний төлөв", typeof(ArrayList), "");
            ucContractList.FieldFindAdd("CreateDate", "Үүсэгсэн огноо", typeof(DateTime), "");
            ucContractList.FieldFindAdd("CreatePostDate", "Үүсэгсэн огноо цаг", typeof(DateTime), "");
            ucContractList.FieldFindAdd("CreateUser", "Үүсэгсэн хэрэглэгч", typeof(ArrayList), "");
            ucContractList.FieldFindAdd("OwnerUser", "Хариуцсан хэрэглэгч", typeof(ArrayList), "");

            ucContractList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            
            ucContractList.FieldFindRefresh();
            InitDictionary();

            ucContractList.VisibleFind = true;
        }
        private void Init1(string[] pParam)
        {
            ucContractList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucContractList_EventDataChanged);
            ucContractList.EventSelected += new ucGridPanel.delegateEventSelected(ucContractList_EventSelected);
            ucContractList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucContractList_EventFindPaging);

            ucContractList.gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            ucContractList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
            ucContractList.gridView1.Appearance.OddRow.Options.UseBackColor = false;
            labelControl1.BackColor = Color.SkyBlue;
            labelControl3.BackColor = Color.Orange;
            labelControl1.ForeColor = Color.SkyBlue;
            labelControl3.ForeColor = Color.Orange;

            Function();
            ucContractList.FieldFindAdd("ContractNo", "Гэрээний дугаар", typeof(string), "");
            ucContractList.FieldFindAdd("ContractType", "Гэрээний төрөл", typeof(ArrayList), "");
            ucContractList.FindItemAdd("CustNo", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucContractList.FieldFindAdd("FirstName", "Харилцагчийн эцэг эхийн нэр", typeof(string), "");
            ucContractList.FieldFindAdd("LastName", "Харилцагчийн нэр", typeof(string), "");

            ucContractList.FieldFindAdd("CorporateName", "Компаний нэр", typeof(string), "");
            ucContractList.FieldFindAdd("ValidStartDate", "Эхлэх огноо", typeof(DateTime), "");
            ucContractList.FieldFindAdd("ValidStartTime", "Эхлэх цаг", typeof(DateTime), "");
            ucContractList.FieldFindAdd("ValidEndDate", "Дуусах огноо", typeof(DateTime), "");
            ucContractList.FieldFindAdd("ValidEndTime", "Дуусах цаг", typeof(DateTime), "");

            ucContractList.FindItemAdd("Amount", "Гэрээний үнийн дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucContractList.FindItemAdd("Balance", "Гэрээний үлдэгдэл үнэ", "", DynamicParameterType.Decimal, false, "d", "");
            ucContractList.FieldFindAdd("CurCode", "Валют", typeof(ArrayList), "");
            ucContractList.FieldFindAdd("PersonCount", "Гэрээнд хамрагдах үйчлүүлэгчийн тоо", typeof(int), "");
            ucContractList.FieldFindAdd("DepFreq", "Гэрээний дүнг элэгдүүлэх давтамж", typeof(ArrayList), "");

            ucContractList.FindItemAdd("DepAmount", "Гэрээний дүнг элэгдүүлэх дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucContractList.FieldFindAdd("Status", "Гэрээний төлөв", typeof(ArrayList), "");
            ucContractList.FieldFindAdd("CreateDate", "Үүсэгсэн огноо", typeof(DateTime), "");
            ucContractList.FieldFindAdd("CreatePostDate", "Үүсэгсэн огноо цаг", typeof(DateTime), "");
            ucContractList.FieldFindAdd("CreateUser", "Үүсэгсэн хэрэглэгч", typeof(ArrayList), "");
            ucContractList.FieldFindAdd("OwnerUser", "Хариуцсан хэрэглэгч", typeof(ArrayList), "");

            ucContractList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";

            ucContractList.FieldFindRefresh();
            InitDictionary();
            ucContractList.VisibleFind = true;
            ucContractList.FindItemSetValue("ContractNo", pParam[2]);
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
                string[] names = new string[] { "USERS", "CONTRACTTYPE", "CURRENCY" };
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
                    ucContractList.FindItemSetList("CreateUser", DT, "userno", "userlname");
                    ucContractList.FindItemSetList("OwnerUser", DT, "userno", "userlname");
                }

                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д CONTRACTTYPE мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucContractList.FindItemSetList("ContractType", DT, "contracttype", "NAME");
                }

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д CURRENCY мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucContractList.FindItemSetList("CurCode", DT, "CURRENCY", "NAME");
                }

                ucContractList.FindItemSetList("Status", 0, "ИДЭВХГҮЙ");
                ucContractList.FindItemSetList("Status", 1, "ИДЭВХТЭЙ");

                ucContractList.FindItemSetList("DepFreq", "A", "Хугацааны эцэст нийт дүнгээр");
                ucContractList.FindItemSetList("DepFreq", "B", "Хугацааны эцэст үлдэгдэл дүнгээр");
                ucContractList.FindItemSetList("DepFreq", "M", "Сар бүрийн эцэст тодорхой дүнгээр");
                ucContractList.FindItemSetList("DepFreq", "Q", "Улирал бүрийн эцэст тодорхой дүнгээр");
                ucContractList.FindItemSetList("DepFreq", "Y", "Жил бүрийн эцэст тодорхой дүнгээр");
                ucContractList.FindItemSetList("DepFreq", "S", "Хуваарийн дагуу");

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

//a.ContractNo, a.ContractType, ct.name as ContractTypeName, a.CustNo, c.FirstName, c.LastName,
//c.CorporateName, a.ValidStartDate, a.ValidStartTime, a.ValidEndDate, a.ValidEndTime, 
//a.Amount, a.Balance, a.CurCode, a.PersonCount, a.DepFreq,
//a.DepAmount, a.Status, decode(a.Status, 0, 'Идэвхгүй', 1, 'Идэвхтэй') as StatusName

            ucContractList.FieldLinkSetColumnCaption(0, "Гэрээний ID");
            ucContractList.FieldLinkSetColumnCaption(1, "Гэрээний төрөл");
            ucContractList.FieldLinkSetColumnCaption(2, "Гэрээний төрлийн нэр");
            ucContractList.FieldLinkSetColumnCaption(3, "Харилцагчийн дугаар");
            ucContractList.FieldLinkSetColumnCaption(4, "Харилцагчийн эцэг эхийн нэр");
            ucContractList.FieldLinkSetColumnCaption(5, "Харилцагчийн нэр");
            ucContractList.FieldLinkSetColumnCaption(6, "Компаний нэр");
            ucContractList.FieldLinkSetColumnCaption(7, "Эхлэх огноо");

            ucContractList.FieldLinkSetColumnCaption(8, "Эхлэх цаг");
            ucContractList.FieldLinkSetColumnCaption(9, "Дуусах огноо");
            ucContractList.FieldLinkSetColumnCaption(10, "Дуусах цаг");

            ucContractList.FieldLinkSetColumnCaption(11, "Гэрээний үнийн дүн");
            ucContractList.gridView1.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ucContractList.gridView1.Columns[11].DisplayFormat.FormatString = "{0:n2}";

            ucContractList.FieldLinkSetColumnCaption(12, "Гэрээний үлдэгдэл үнэ");
            ucContractList.gridView1.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ucContractList.gridView1.Columns[12].DisplayFormat.FormatString = "{0:n2}";

            ucContractList.FieldLinkSetColumnCaption(13, "Валют");
            ucContractList.FieldLinkSetColumnCaption(14, "Гэрээнд хамрагдах үйчлүүлэгчийн тоо");
            ucContractList.FieldLinkSetColumnCaption(15, "Гэрээний дүнг элэгдүүлэх давтамж");

            ucContractList.FieldLinkSetColumnCaption(16, "Гэрээний дүнг элэгдүүлэх дүн");
            ucContractList.gridView1.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ucContractList.gridView1.Columns[16].DisplayFormat.FormatString = "{0:n2}";

            ucContractList.FieldLinkSetColumnCaption(17, "Гэрээний төлөв");
            ucContractList.FieldLinkSetColumnCaption(18, "Гэрээний төлөв");

            ucContractList.FieldLinkSetColumnCaption(19, "Үүсэгсэн огноо");
            ucContractList.FieldLinkSetColumnCaption(20, "Үүсэгсэн огноо цаг");
            ucContractList.FieldLinkSetColumnCaption(21, "Үүсэгсэн хэрэглэгч");
            ucContractList.FieldLinkSetColumnCaption(22, "Хариуцсан хэрэглэгч");

            ucContractList.gridView1.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
           
            // Customize the total summary.

            ucContractList.gridView1.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            ucContractList.gridView1.Columns[0].SummaryItem.DisplayFormat = "Нийт гэрээний тоо :{0:C2}";
            ucContractList.gridView1.Columns[0].SummaryItem.Tag = 1;
            (ucContractList.gridView1.Columns[0].View as GridView).OptionsView.ShowFooter = true;

            ucContractList.gridView1.OptionsView.ShowGroupPanel = false;
            ucContractList.gridView1.ExpandAllGroups();

            ucContractList.gridView1.Columns[11].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ucContractList.gridView1.Columns[11].SummaryItem.DisplayFormat = "Нийт:{0:C2}";
            ucContractList.gridView1.Columns[11].SummaryItem.Tag = 1;
            (ucContractList.gridView1.Columns[11].View as GridView).OptionsView.ShowFooter = true;

            ucContractList.gridView1.OptionsView.ShowGroupPanel = true;
            ucContractList.gridView1.ExpandAllGroups();


            ucContractList.gridView1.Columns[12].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ucContractList.gridView1.Columns[12].SummaryItem.DisplayFormat = "Нийт:{0:C2}";
            ucContractList.gridView1.Columns[12].SummaryItem.Tag = 1;
            (ucContractList.gridView1.Columns[12].View as GridView).OptionsView.ShowFooter = true;


             // Create and setup the first summary item.
            GridGroupSummaryItem item = new GridGroupSummaryItem();
            item.FieldName = "ContractNo";
            item.SummaryType = DevExpress.Data.SummaryItemType.Count;
            ucContractList.gridView1.GroupSummary.Add(item);


            DevExpress.XtraGrid.GridGroupSummaryItem item1 = new DevExpress.XtraGrid.GridGroupSummaryItem();
            item1.FieldName = "Amount";

            item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item1.DisplayFormat = "Нийт б/хураамж {0:c2}";
            item1.Tag = 1;
            item1.ShowInGroupColumnFooter = ucContractList.gridView1.Columns["Amount"];
     
            ucContractList.gridView1.GroupSummary.Add(item1);


            DevExpress.XtraGrid.GridGroupSummaryItem item2 = new DevExpress.XtraGrid.GridGroupSummaryItem();
            item2.FieldName = "Balance";

            item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            item2.DisplayFormat = "Нийт б/үнэлгээ {0:c2}";
            item2.Tag = 1;
            item2.ShowInGroupColumnFooter = ucContractList.gridView1.Columns["Balance"];

            ucContractList.gridView1.GroupSummary.Add(item2);

            FormUtility.RestoreStateGrid(appname, formname, ref ucContractList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucContractList.ucParameterPanel1.vGridControl1, ref ucContractList.groupControl1);
        }
        #endregion
        #region [ EventSelected ]
        void ucContractList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucContractList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToStr(selectedrow["ContractNo"]);
                    EServ.Shared.Static.Invoke("InfoPos.Contract.dll", "InfoPos.Contract.Main", "CallContract", obj);
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 130001, 130001, values);

                if (res.ResultNo == 0)
                {
                    ucContractList.DataSource = res.Data.Tables[0];
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
            if (ucContractList.DataSource != null)
            {
                DataRow DR = ucContractList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucContractList.gridView1.DataSource;
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
            ucContractList.OnEventFindPaging(0);
        }
        void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string Status = View.GetRowCellDisplayText(e.RowHandle, View.Columns[17]);
                if (Status == "1")
                {
                    e.Appearance.BackColor = Color.SkyBlue;
                    e.Appearance.BackColor2 = Color.SkyBlue;
                }
                if (Status == "0")
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
                ucContractList.OnEventFindPaging(0);
            }
        }
        private void ContractList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref ucContractList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucContractList.ucParameterPanel1.vGridControl1, ref ucContractList.groupControl1);
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
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 130001, 130001, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucContractList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
