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
    public partial class PreSaleList : Form
    {
        private Core.Core _core;
        int PrivNo = 130301;
        string appname = "", formname = "";
        Form FormName = null;
        public PreSaleList(Core.Core core)
        {
            //this.Show();
            InitializeComponent();
            _core = core;
            Init();
            ucReSaleList.Resource = _core.Resource;

            //ContractList.ActiveForm.AcceptButton = ucContractList.btnFind;
        }
        public PreSaleList(Core.Core core, string[] pParam)
        {
            //this.Show();
            InitializeComponent();
            _core = core;
            Init1(pParam);

            ucReSaleList.Resource = _core.Resource;
            //ContractList.ActiveForm.AcceptButton = ucContractList.btnFind;
        }
        public PreSaleList(Core.Core core, long ppContractID)
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
            ucReSaleList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucContractList_EventDataChanged);
            //ucReSaleList.EventSelected += new ucGridPanel.delegateEventSelected(ucContractList_EventSelected);
            ucReSaleList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucContractList_EventFindPaging);
           
            ucReSaleList.gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            ucReSaleList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
            ucReSaleList.gridView1.Appearance.OddRow.Options.UseBackColor = false;

            labelControl1.BackColor = Color.SkyBlue;
            labelControl1.ForeColor = Color.SkyBlue;

            labelControl3.BackColor = Color.Orange;
            labelControl3.ForeColor = Color.Orange;

            labelControl6.BackColor = Color.LimeGreen;
            labelControl6.ForeColor = Color.LimeGreen;

            labelControl8.BackColor = Color.DeepPink;
            labelControl8.ForeColor = Color.DeepPink;

            Function();

            ucReSaleList.FieldFindAdd("PreSaleNo", "Урьдчилсан борлуулалтын дугаар", typeof(string), "");
            ucReSaleList.FieldFindAdd("PreSaleType", "Урьдчилсан борлуулалтын төрөл", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("CustomerName", "Харилцагчийн нэр", typeof(string), "");
            ucReSaleList.FieldFindAdd("ChannelID", "Сувагын ID ", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("PreSaleProd", "Урьчилсан бүтээгдэхүүний код (Зөвхөн ваучерийн хувьд)", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("UserID", "Хэрэв Виртуаль сувагаар орж ирсэн байвал энэ ID", typeof(string), "");
            ucReSaleList.FieldFindAdd("CreateDate", "Үүсэгсэн огноо цаг минут", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("CreateUser", "Үүсэгсэн хэрэглэгч", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("SalesUser", "Борлуулалтын ажилтан", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("PersonCount", "Борлуулалтанд хамрагдах хүний тоо", typeof(int), "");
            ucReSaleList.FieldFindAdd("StartDateTime", "Хүчинтэй хугацааны өдөр цаг. Эхлэх", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("EndDateTime", "Хүчинтэй хугацааны өдөр цаг. Дуусах", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("GraceHoursStart", "захиалгаа баталгаажуулаагүй үед expire хийх хүлээх цаг", typeof(int), "");
            ucReSaleList.FieldFindAdd("GraceHoursEnd", "захиалгаа баталгаажуулчихсан үед expire хийх хүлээх цаг", typeof(int), "");

            ucReSaleList.FindItemAdd("PreSaleAmount", "Урьчилсан борлуулалтын үнийн дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FindItemAdd("PreSaleAmountMin", "Урьчилсан борлуулалтын үнийн дүнгийн доод хязгаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FindItemAdd("PreSaleAmountMax", "Урьчилсан борлуулалтын үнийн дүнгийн дээд хязгаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FindItemAdd("SaleAmount", "Борлуулалтын дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FindItemAdd("AmartizationAmount", "Элэгдүүлэлтийн дүн", "", DynamicParameterType.Decimal, false, "d", "");

            ucReSaleList.FieldFindAdd("AmartizationType", "Элэгдүүлэлтийн төрөл", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("AmartizationFreq", "Элэгдүүлэх давтамж", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("AmartizationMethod", "Элэгдүүлэлтийн арга", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("CurCode", "Валют", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("PriceType", "Үнээ захиалгын дагуу явах эсэх", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("DiscountID", "Хөнгөлөлтийн дүрэмийн ID", typeof(string), "");
            ucReSaleList.FieldFindAdd("DiscountType", "Хөнгөлөлтийн төрөл", typeof(ArrayList), "");
            ucReSaleList.FindItemAdd("DiscountAmount", "Хөнгөлөлтийн дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FieldFindAdd("CancelDateTime", "Цуцалсан огноо цаг", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("CancelNote", "Цуцалсан тайлбар", typeof(string), "");
            ucReSaleList.FieldFindAdd("CancelUserNo", "Цуцалсан хэрэглэгч", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("ExpireDateTime", "Хугацаа дууссан огноо цаг", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("ExpireNote", "Хугацаа дууссан  тайлбар", typeof(string), "");
            ucReSaleList.FieldFindAdd("ExpireUserNo", "Хугацаа дууссан  хэрэглэгч", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("ConfirmDateTime", "Баталгаажуулсан огноо цаг", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("ConfirmNote", "Баталгаажуулсан тайлбар", typeof(string), "");
            ucReSaleList.FieldFindAdd("ConfirmUserNo", "Баталгаажуулсан хэрэглэгч", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("ContractNo", "Захиалгын гэрээний дугаар", typeof(string), "");
            ucReSaleList.FieldFindAdd("SalesAccountNo", "Борлуулалтын орлогын данс", typeof(string), "");
            ucReSaleList.FieldFindAdd("RefundAccountNo", "Борлуулалтын буцаалт данс", typeof(string), "");
            ucReSaleList.FieldFindAdd("DiscountAccountNo", "Борлуулалтын хөнгөлөлтийн данс", typeof(string), "");
            ucReSaleList.FieldFindAdd("BonusAccountNo", "Урамшууллын данс", typeof(string), "");
            ucReSaleList.FieldFindAdd("BonusExpAccountNo", "Урамшууллын зардлын данс", typeof(string), "");


            ucReSaleList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            
            ucReSaleList.FieldFindRefresh();
            InitDictionary();

            ucReSaleList.VisibleFind = true;
        }
        private void Init1(string[] pParam)
        {
            ucReSaleList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucContractList_EventDataChanged);
            ucReSaleList.EventSelected += new ucGridPanel.delegateEventSelected(ucContractList_EventSelected);
            ucReSaleList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucContractList_EventFindPaging);

            ucReSaleList.gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            ucReSaleList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
            ucReSaleList.gridView1.Appearance.OddRow.Options.UseBackColor = false;

            labelControl1.BackColor = Color.SkyBlue;
            labelControl1.ForeColor = Color.SkyBlue;

            labelControl3.BackColor = Color.Orange;
            labelControl3.ForeColor = Color.Orange;

            labelControl6.BackColor = Color.LimeGreen;
            labelControl6.ForeColor = Color.LimeGreen;

            labelControl8.BackColor = Color.DeepPink;
            labelControl8.ForeColor = Color.DeepPink;

            Function();

            ucReSaleList.FieldFindAdd("PreSaleNo", "Урьдчилсан борлуулалтын дугаар", typeof(string), "");
            ucReSaleList.FieldFindAdd("PreSaleType", "Урьдчилсан борлуулалтын төрөл", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("CustomerName", "Харилцагчийн нэр", typeof(string), "");
            ucReSaleList.FieldFindAdd("ChannelID", "Сувагын ID ", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("PreSaleProd", "Урьчилсан бүтээгдэхүүний код (Зөвхөн ваучерийн хувьд)", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("UserID", "Хэрэв Виртуаль сувагаар орж ирсэн байвал энэ ID", typeof(string), "");
            ucReSaleList.FieldFindAdd("CreateDate", "Үүсэгсэн огноо цаг минут", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("CreateUser", "Үүсэгсэн хэрэглэгч", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("SalesUser", "Борлуулалтын ажилтан", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("PersonCount", "Борлуулалтанд хамрагдах хүний тоо", typeof(int), "");
            ucReSaleList.FieldFindAdd("StartDateTime", "Хүчинтэй хугацааны өдөр цаг. Эхлэх", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("EndDateTime", "Хүчинтэй хугацааны өдөр цаг. Дуусах", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("GraceHoursStart", "захиалгаа баталгаажуулаагүй үед expire хийх хүлээх цаг", typeof(int), "");
            ucReSaleList.FieldFindAdd("GraceHoursEnd", "захиалгаа баталгаажуулчихсан үед expire хийх хүлээх цаг", typeof(int), "");

            ucReSaleList.FindItemAdd("PreSaleAmount", "Урьчилсан борлуулалтын үнийн дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FindItemAdd("PreSaleAmountMin", "Урьчилсан борлуулалтын үнийн дүнгийн доод хязгаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FindItemAdd("PreSaleAmountMax", "Урьчилсан борлуулалтын үнийн дүнгийн дээд хязгаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FindItemAdd("SaleAmount", "Борлуулалтын дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FindItemAdd("AmartizationAmount", "Элэгдүүлэлтийн дүн", "", DynamicParameterType.Decimal, false, "d", "");

            ucReSaleList.FieldFindAdd("AmartizationType", "Элэгдүүлэлтийн төрөл", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("AmartizationFreq", "Элэгдүүлэх давтамж", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("AmartizationMethod", "Элэгдүүлэлтийн арга", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("CurCode", "Валют", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("PriceType", "Үнээ захиалгын дагуу явах эсэх", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("DiscountID", "Хөнгөлөлтийн дүрэмийн ID", typeof(string), "");
            ucReSaleList.FieldFindAdd("DiscountType", "Хөнгөлөлтийн төрөл", typeof(ArrayList), "");
            ucReSaleList.FindItemAdd("DiscountAmount", "Хөнгөлөлтийн дүн", "", DynamicParameterType.Decimal, false, "d", "");
            ucReSaleList.FieldFindAdd("CancelDateTime", "Цуцалсан огноо цаг", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("CancelNote", "Цуцалсан тайлбар", typeof(string), "");
            ucReSaleList.FieldFindAdd("CancelUserNo", "Цуцалсан хэрэглэгч", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("ExpireDateTime", "Хугацаа дууссан огноо цаг", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("ExpireNote", "Хугацаа дууссан  тайлбар", typeof(string), "");
            ucReSaleList.FieldFindAdd("ExpireUserNo", "Хугацаа дууссан  хэрэглэгч", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("ConfirmDateTime", "Баталгаажуулсан огноо цаг", typeof(DateTime), "");
            ucReSaleList.FieldFindAdd("ConfirmNote", "Баталгаажуулсан тайлбар", typeof(string), "");
            ucReSaleList.FieldFindAdd("ConfirmUserNo", "Баталгаажуулсан хэрэглэгч", typeof(ArrayList), "");
            ucReSaleList.FieldFindAdd("ContractNo", "Захиалгын гэрээний дугаар", typeof(string), "");
            ucReSaleList.FieldFindAdd("SalesAccountNo", "Борлуулалтын орлогын данс", typeof(string), "");
            ucReSaleList.FieldFindAdd("RefundAccountNo", "Борлуулалтын буцаалт данс", typeof(string), "");
            ucReSaleList.FieldFindAdd("DiscountAccountNo", "Борлуулалтын хөнгөлөлтийн данс", typeof(string), "");
            ucReSaleList.FieldFindAdd("BonusAccountNo", "Урамшууллын данс", typeof(string), "");
            ucReSaleList.FieldFindAdd("BonusExpAccountNo", "Урамшууллын зардлын данс", typeof(string), "");

            ucReSaleList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";

            ucReSaleList.FieldFindRefresh();
            InitDictionary();
            ucReSaleList.VisibleFind = true;
            ucReSaleList.FindItemSetValue("PreSaleNo", pParam[2]);
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
                    ucReSaleList.FindItemSetList("CreateUser", DT, "userno", "userlname");
                    ucReSaleList.FindItemSetList("SalesUser", DT, "userno", "userlname");

                    ucReSaleList.FindItemSetList("CancelUserNo", DT, "userno", "userlname");
                    ucReSaleList.FindItemSetList("ExpireUserNo", DT, "userno", "userlname");
                    ucReSaleList.FindItemSetList("ConfirmUserNo", DT, "userno", "userlname");
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
                    ucReSaleList.FindItemSetList("CurCode", DT, "CURRENCY", "NAME");
                }

                ucReSaleList.FindItemSetList("AmartizationMethod", 0, "Шулуун шугам арга");

                ucReSaleList.FindItemSetList("AmartizationType", 0, "Хугацааны эцэст");
                ucReSaleList.FindItemSetList("AmartizationType", 1, "Давтамжаар");

                ucReSaleList.FindItemSetList("ChannelID", 0, "Терминал");
                ucReSaleList.FindItemSetList("ChannelID", 1, "Web");
                ucReSaleList.FindItemSetList("ChannelID", 2, "Mobile");
                ucReSaleList.FindItemSetList("ChannelID", 3, "Kiosk");

                ucReSaleList.FindItemSetList("Status", 0, "Шинэ");
                ucReSaleList.FindItemSetList("Status", 1, "Баталгаажсан");
                ucReSaleList.FindItemSetList("Status", 2, "Гүйцэтгэгдсэн");
                ucReSaleList.FindItemSetList("Status", 8, "Хугацаа дууссан");
                ucReSaleList.FindItemSetList("Status", 9, "Цуцлагдсан");

                ucReSaleList.FindItemSetList("AmartizationFreq", "D", "ӨДӨР");
                ucReSaleList.FindItemSetList("AmartizationFreq", "M", "САР");
                ucReSaleList.FindItemSetList("AmartizationFreq", "Q", "Улирал бүр");
                ucReSaleList.FindItemSetList("AmartizationFreq", "Y", "Жил бүр");

                ucReSaleList.FindItemSetList("PriceType", 0, "Захиалгын дагуу үнэ байхгүй. Үйлчилгээ авах үеийнхээр");
                ucReSaleList.FindItemSetList("PriceType", 1, "Захиалгын дагуу үнээр борлуулалт хийнэ");

                ucReSaleList.FindItemSetList("DiscountType", 0, "Хөнгөлөлт байхгүй");
                ucReSaleList.FindItemSetList("DiscountType", 1, "Хувиар хөнгөлөнө");
                ucReSaleList.FindItemSetList("DiscountType", 2, "Дүнгээр хөнгөлөнө");

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

            FormUtility.RestoreStateGrid(appname, formname, ref ucReSaleList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucReSaleList.ucParameterPanel1.vGridControl1, ref ucReSaleList.groupControl1);
        }
        #endregion
        #region [ EventSelected ]
        void ucContractList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucReSaleList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToStr(selectedrow["PreSaleNo"]);
                    EServ.Shared.Static.Invoke("InfoPos.PreSale.dll", "InfoPos.PreSale.Main", "CallPreSale", obj);
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, PrivNo, PrivNo, values);

                if (res.ResultNo == 0)
                {
                    ucReSaleList.DataSource = res.Data.Tables[0];
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
            if (ucReSaleList.DataSource != null)
            {
                DataRow DR = ucReSaleList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucReSaleList.gridView1.DataSource;
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
            ucReSaleList.OnEventFindPaging(0);
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
                ucReSaleList.OnEventFindPaging(0);
            }
        }
        private void ContractList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref ucReSaleList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucReSaleList.ucParameterPanel1.vGridControl1, ref ucReSaleList.groupControl1);
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
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, PrivNo, PrivNo, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucReSaleList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
