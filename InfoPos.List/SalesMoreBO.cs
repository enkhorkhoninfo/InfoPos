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
    public partial class SalesMoreBO : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;
        int PrivNo = 140367;
        string appname = "";
        string formname = "";
        Form FormName = null;
        public SalesMoreBO(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            ucSalesSearch.Resource = _core.Resource;
            Init();
        }
        #region [ Init ]
        private void Init()
        {
            ucSalesSearch.EventFindPaging += new ISM.Template.ucGridPanel.delegateEventFindPaging(ucSalesSearch_EventFindPaging);
            ucSalesSearch.EventSelected += new ucGridPanel.delegateEventSelected(ucSalesSearch_EventSelected);
            ucSalesSearch.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucSalesSearch_EventDataChanged);

            ucSalesSearch.FieldFindAdd("TRANDATE", "Огноо", typeof(DateTime), "");
            ucSalesSearch.FieldFindAdd("CASHIERNO", "Хэрэглэгч", typeof(ArrayList), "");
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

            ucSalesSearch.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucSalesSearch.FieldFindRefresh();
            InitDictionary();
            ucSalesSearch.VisibleFind = true;

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
                    ucSalesSearch.FindItemSetList("CASHIERNO", DT, "USERNO", "USERLNAME");
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
            ucSalesSearch.OnEventFindPaging(0);
        }
        void ucSalesSearch_EventDataChanged()
        {
            ucSalesSearch.FieldLinkSetColumnCaption(0, "Гүйлгээний огноо");
            ucSalesSearch.FieldLinkSetColumnCaption(1, "Борлуулалт №");
            ucSalesSearch.FieldLinkSetColumnCaption(2, "Харилцагчийн дугаар");
            ucSalesSearch.FieldLinkSetColumnCaption(3, "Нэр");
            ucSalesSearch.FieldLinkSetColumnCaption(4, "Төлөв");
            ucSalesSearch.FieldLinkSetColumnCaption(5, "Борлуулалтын үнэ");
            ucSalesSearch.FieldLinkSetColumnCaption(6, "Хөнгөлөлтийн үнэ");
            ucSalesSearch.FieldLinkSetColumnCaption(7, "Үнэ");
            ucSalesSearch.FieldLinkSetColumnCaption(8, "VAT");
            ucSalesSearch.FieldLinkSetColumnCaption(9, "Нийт төлбөр");
            ucSalesSearch.FieldLinkSetColumnCaption(10, "Кассаар");
            ucSalesSearch.FieldLinkSetColumnCaption(11, "Картаар");
            ucSalesSearch.FieldLinkSetColumnCaption(12, "Бусад");
            ucSalesSearch.FieldLinkSetColumnCaption(13, "Хэрэглэгч №");
            ucSalesSearch.FieldLinkSetColumnCaption(14, "Хэрэглэгчийн овог");
            ucSalesSearch.FieldLinkSetColumnCaption(15, "Хэрэглэгчийн нэр");

            FormUtility.RestoreStateGrid(appname, formname, ref ucSalesSearch.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucSalesSearch.ucParameterPanel1.vGridControl1, ref ucSalesSearch.groupControl1);
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140367, 140367, values);

                if (res.ResultNo == 0)
                {
                    ucSalesSearch.DataSource = res.Data.Tables[0];
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
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140367, 140367, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 
            ucSalesSearch.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
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