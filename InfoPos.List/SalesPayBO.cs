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
    public partial class SalesPayBO : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;
        int PrivNo = 140369;
        string appname = "";
        string formname = "";
        Form FormName = null;
        public SalesPayBO(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            ucSalesPayBO.Resource = _core.Resource;
            Init();
        }
        #region [ Init ]
        private void Init()
        {
            ucSalesPayBO.EventFindPaging += new ISM.Template.ucGridPanel.delegateEventFindPaging(ucSalesSearch_EventFindPaging);
            ucSalesPayBO.EventSelected += new ucGridPanel.delegateEventSelected(ucSalesSearch_EventSelected);
            ucSalesPayBO.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucSalesSearch_EventDataChanged);

            ucSalesPayBO.FieldFindAdd("TRANDATE", "Огноо", typeof(DateTime), "");
            ucSalesPayBO.FieldFindAdd("CASHIERNO", "Хэрэглэгч", typeof(ArrayList), "");
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

            ucSalesPayBO.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucSalesPayBO.FieldFindRefresh();
            InitDictionary();
            ucSalesPayBO.VisibleFind = true;

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
                    ucSalesPayBO.FindItemSetList("CASHIERNO", DT, "USERNO", "USERLNAME");
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
            ucSalesPayBO.OnEventFindPaging(0);
        }
        void ucSalesSearch_EventDataChanged()
        {
            ucSalesPayBO.FieldLinkSetColumnCaption(0, "Гүйлгээний огноо");
            ucSalesPayBO.FieldLinkSetColumnCaption(1, "Гүйлгээний огноо цаг");
            ucSalesPayBO.FieldLinkSetColumnCaption(2, "Ээлжийн №");
            ucSalesPayBO.FieldLinkSetColumnCaption(3, "Хэрэглэгч №");
            ucSalesPayBO.FieldLinkSetColumnCaption(4, "POS №");

            ucSalesPayBO.FieldLinkSetColumnCaption(5, "AREA №");
            ucSalesPayBO.FieldLinkSetColumnCaption(6, "Борлуулалт №");
            ucSalesPayBO.FieldLinkSetColumnCaption(7, "Харилцагчийн №");
            ucSalesPayBO.FieldLinkSetColumnCaption(8, "Нэр");
            ucSalesPayBO.FieldLinkSetColumnCaption(9, "Төлбөрийн №");

            ucSalesPayBO.FieldLinkSetColumnCaption(10, "Төлбөрийн нэр");
            ucSalesPayBO.FieldLinkSetColumnCaption(11, "Төлбөрийн хэрэгсэлийн №");
            ucSalesPayBO.FieldLinkSetColumnCaption(12, "Дүн");
            ucSalesPayBO.FieldLinkSetColumnCaption(13, "Төлөв");

            FormUtility.RestoreStateGrid(appname, formname, ref ucSalesPayBO.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucSalesPayBO.ucParameterPanel1.vGridControl1, ref ucSalesPayBO.groupControl1);
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140369, 140369, values);

                if (res.ResultNo == 0)
                {
                    ucSalesPayBO.DataSource = res.Data.Tables[0];
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
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140369, 140369, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 
            ucSalesPayBO.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
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