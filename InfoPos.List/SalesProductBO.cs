using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using ISM.Template;
using EServ.Shared;

namespace InfoPos.List
{
    public partial class SalesProductBO : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;
        int PrivNo = 140368;
        string appname = "";
        string formname = "";
        Form FormName = null;
        public SalesProductBO(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            ucSalesProductBO.Resource = _core.Resource;
            Init();
        }
        #region [ Init ]
        private void Init()
        {
            ucSalesProductBO.EventFindPaging += new ISM.Template.ucGridPanel.delegateEventFindPaging(ucSalesSearch_EventFindPaging);
            ucSalesProductBO.EventSelected += new ucGridPanel.delegateEventSelected(ucSalesSearch_EventSelected);
            ucSalesProductBO.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucSalesSearch_EventDataChanged);

            ucSalesProductBO.FieldFindAdd("TRANDATE", "Огноо", typeof(DateTime), "");
            ucSalesProductBO.FieldFindAdd("CASHIERNO", "Хэрэглэгч", typeof(ArrayList), "");
            //ucSalesSearch.FieldFindAdd("CUSTNO", "Харилцагчийн дугаар", typeof(string), "");
            //ucSalesSearch.FieldFindAdd("SALESNO", "Борлуулалтын №", typeof(string), "");

            //ucSalesSearch.FieldFindAdd("SERIALNO", "Таг №", typeof(string), "");
            //ucSalesSearch.FieldFindAdd("CUSTOMERNO", "Харилцагчийн дугаар", typeof(string), "");
            //ucSalesSearch.FieldFindAdd("REGISTERNO", "Регистер №", typeof(string), "");
            //ucSalesSearch.FieldFindAdd("USERFNAME", "Овог", typeof(string), "");
            //ucSalesSearch.FieldFindAdd("USERLNAME", "Нэр", typeof(string), "");
            //ucSalesSearch.FieldFindAdd("CORPORATENAME", "Байгууллагын нэр", typeof(string), "");
            //ucSalesSearch.FieldFindAdd("PAYMENTNO", "Билл №", typeof(string), "");
            //ucSalesSearch.FieldFindAdd("STATUS", "Төлөв", typeof(ArrayList), "");

            ucSalesProductBO.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucSalesProductBO.FieldFindRefresh();
            InitDictionary();
            ucSalesProductBO.VisibleFind = true;

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
                string[] names = new string[] { "USERS" };
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
                    ucSalesProductBO.FindItemSetList("CASHIERNO", DT, "USERNO", "USERLNAME");
                }

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
        private void SalesSearch_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucSalesProductBO.OnEventFindPaging(0);
        }
        void ucSalesSearch_EventDataChanged()
        {
            ucSalesProductBO.FieldLinkSetColumnCaption(0, "Гүйлгээний огноо");
            ucSalesProductBO.FieldLinkSetColumnCaption(1, "Гүйлгээний огноо цаг");
            ucSalesProductBO.FieldLinkSetColumnCaption(2, "Ээлжийн №");
            ucSalesProductBO.FieldLinkSetColumnCaption(3, "Хэрэглэгч №");
            ucSalesProductBO.FieldLinkSetColumnCaption(4, "POS №");

            ucSalesProductBO.FieldLinkSetColumnCaption(5, "AREA №");
            ucSalesProductBO.FieldLinkSetColumnCaption(6, "Борлуулалт №");
            ucSalesProductBO.FieldLinkSetColumnCaption(7, "Харилцагчийн №");
            ucSalesProductBO.FieldLinkSetColumnCaption(8, "Нэр");
            ucSalesProductBO.FieldLinkSetColumnCaption(9, "Бүтээгдэхүүний №");

            ucSalesProductBO.FieldLinkSetColumnCaption(10, "Бүтээгдэхүүний төрөл");
            ucSalesProductBO.FieldLinkSetColumnCaption(11, "Бүтээгдэхүүний нэр");
            ucSalesProductBO.FieldLinkSetColumnCaption(12, "Үнэ");
            ucSalesProductBO.FieldLinkSetColumnCaption(13, "Суурь үнэ");
            ucSalesProductBO.FieldLinkSetColumnCaption(14, "Тоо");

            ucSalesProductBO.FieldLinkSetColumnCaption(15, "Борлуулалтын үнэ");
            ucSalesProductBO.FieldLinkSetColumnCaption(16, "Хөнгөлсөн бүтээгдэхүүн");
            ucSalesProductBO.FieldLinkSetColumnCaption(17, "Хөнгөлөлтийн үнэ");
            ucSalesProductBO.FieldLinkSetColumnCaption(18, "Борлуулалтын төрөл");
            ucSalesProductBO.FieldLinkSetColumnCaption(19, "Төлөв");

            ucSalesProductBO.FieldLinkSetColumnCaption(20, "Дэд төрөл");

            FormUtility.RestoreStateGrid(appname, formname, ref ucSalesProductBO.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucSalesProductBO.ucParameterPanel1.vGridControl1, ref ucSalesProductBO.groupControl1);
        }

        void ucSalesSearch_EventSelected(DataRow selectedrow)
        {
            //if (selectedrow != null)
            //{
            //    if (!ucSalesSearch.Browsable)
            //    {
            //        SalesDetail frm = new SalesDetail(_core, Static.ToStr(selectedrow["BATCHNO"]), Static.ToDateTime(selectedrow["POSTDATE"]));
            //        frm.MdiParent = _core.MainForm;
            //        frm.Show();
            //    }
            //    else
            //    {
            //        this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //        this.Close();
            //    }
            //}
        }
        #region [ LoadData ]
        void LoadData(object[] values)
        {
            Result res = new Result();

            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140368, 140368, values);

                if (res.ResultNo == 0)
                {
                    ucSalesProductBO.DataSource = res.Data.Tables[0];
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
        void ucSalesSearch_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140368, 140368, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 
            ucSalesProductBO.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
        private void SalesSearch_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void SalesSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}