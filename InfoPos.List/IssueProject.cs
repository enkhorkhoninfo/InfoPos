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

namespace InfoPos.List
{
    public partial class IssueProject : Form
    {

        #region [ Variable ]
        Core.Core _core;
        long pPolicyNo;
        long ProjectID;
        int PrivNo = 310069;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Constructor Function ]
        public IssueProject(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucIssueProjectList.Resource = _core.Resource;

        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            ucIssueProjectList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucIssueProjectList_EventDataChanged);
            ucIssueProjectList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucIssueProjectList_EventFindPaging);
            ucIssueProjectList.EventSelected += new ucGridPanel.delegateEventSelected(ucIssueProjectList_EventSelected);


            Function();
            ucIssueProjectList.FindItemAdd("ProjectID", "Дэд төрлийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");

            ucIssueProjectList.FieldFindAdd("status", "Төлөв", typeof(ArrayList), "");

            ucIssueProjectList.FieldFindAdd("name", "Нэр", typeof(string), "");
            ucIssueProjectList.FieldFindAdd("name2", "Нэр 2", typeof(string), "");
            ucIssueProjectList.FieldFindAdd("ShortName ", "Товч нэр ", typeof(string), "");
            ucIssueProjectList.FieldFindAdd("ShortName2", "Товч нэр 2", typeof(string), "");
            ucIssueProjectList.FieldFindAdd("owneruser", "Төслийг хариуцагч", typeof(ArrayList), "");

            ucIssueProjectList.FieldFindAdd("startdate", "Төслийн эхлэх огноо", typeof(DateTime), "");
            ucIssueProjectList.FieldFindAdd("enddate", "Төслийн дуусах огноо", typeof(DateTime), "");
            ucIssueProjectList.FieldFindAdd("projecttypeid", "Төслийн төрлийн ID", typeof(ArrayList), "");
            ucIssueProjectList.FieldFindAdd("createuserno", "Үүсгэсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");

            ucIssueProjectList.FieldFindAdd("createdate", "Төслийг үүсгэсэн огноо", typeof(DateTime), "");
            ucIssueProjectList.FieldFindAdd("NotifySchemaID", "Мэдэгдэлийн схем ID", typeof(ArrayList), "");
            ucIssueProjectList.FieldFindAdd("PermSchemaID", "Эрхийн схем ID", typeof(ArrayList), "");

            ucIssueProjectList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucIssueProjectList.FieldFindRefresh();

            InitDictionary();

            ucIssueProjectList.Visible = true;
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
                string[] names = new string[] { "USERS", "PROJECTTYPES", "NOTIFYSCHEMA", "PERMSCHEMA" };

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
                    ucIssueProjectList.FindItemSetList("owneruser", DT, "userno", "userlname");
                    ucIssueProjectList.FindItemSetList("createuserno", DT, "userno", "userlname");


                }
                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д төслийн төрлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueProjectList.FindItemSetList("projecttypeid", DT, "PROJECTTYPEID", "NAME");

                }
                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д мэдэгдэлийн схемийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueProjectList.FindItemSetList("NotifySchemaID", DT, "SCHEMAID", "NAME");

                }
                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "Dictionary-д эрхийн схемийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueProjectList.FindItemSetList("PermSchemaID", DT, "PERMSCHEMAID", "NAME");

                }


                ucIssueProjectList.FindItemSetList("status", 0, "Нээлттэй");
                ucIssueProjectList.FindItemSetList("status", 1, "Хаагдсан");



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
        #region [ EventSelected ]
        void ucIssueProjectList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {

                if (!ucIssueProjectList.Browsable)
                {


                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["ProjectID"]);
                    EServ.Shared.Static.Invoke("InfoPos.Parameter.dll", "InfoPos.Parameter.Main", "CallCRMIssueProject", obj);
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
        #endregion
        #region [ EventDataChanged ]
        void ucIssueProjectList_EventDataChanged()
        {

            ucIssueProjectList.FieldLinkSetColumnCaption(0, "Төслийн дугаар ");
            ucIssueProjectList.FieldLinkSetColumnCaption(1, "Төлөв");
            ucIssueProjectList.FieldLinkSetColumnCaption(2, "Төлвийн нэр");
            ucIssueProjectList.FieldLinkSetColumnCaption(3, "Нэр");
            ucIssueProjectList.FieldLinkSetColumnCaption(4, "Нэр 2");
            ucIssueProjectList.FieldLinkSetColumnCaption(5, "Товч нэр ");
            ucIssueProjectList.FieldLinkSetColumnCaption(6, "Товч нэр 2");
            ucIssueProjectList.FieldLinkSetColumnCaption(7, "Төслийн дэлгэрэнгүй мэдээлэл");
            ucIssueProjectList.FieldLinkSetColumnCaption(8, "Төслийг хариуцагч");
            ucIssueProjectList.FieldLinkSetColumnCaption(9, "Төслийг хариуцагч");
            ucIssueProjectList.FieldLinkSetColumnCaption(10, "Төслийн эхлэх огноо ");
            ucIssueProjectList.FieldLinkSetColumnCaption(11, "Төслийн дуусах огноо ");
            ucIssueProjectList.FieldLinkSetColumnCaption(12, "Төслийн төрлийн ID");
            ucIssueProjectList.FieldLinkSetColumnCaption(13, "Төслийн төрлийн нэр");
            ucIssueProjectList.FieldLinkSetColumnCaption(14, "Үүсгэсэн хэрэглэгчийн дугаар");
            ucIssueProjectList.FieldLinkSetColumnCaption(15, "Үүсгэсэн хэрэглэгчийн нэр");
            ucIssueProjectList.FieldLinkSetColumnCaption(16, "Төслийг үүсгэсэн огноо");
            ucIssueProjectList.FieldLinkSetColumnCaption(17, "Мэдэгдэлийн схем ID ");
            ucIssueProjectList.FieldLinkSetColumnCaption(18, "Мэдэгдэлийн схем нэр ");
            ucIssueProjectList.FieldLinkSetColumnCaption(19, "Эрхийн схем ID");
            ucIssueProjectList.FieldLinkSetColumnCaption(20, "Эрхийн схем нэр");
            ucIssueProjectList.FieldLinkSetColumnCaption(21, "Жагсаалтын эрэмбэ");

            FormUtility.RestoreStateGrid(appname, formname, ref  ucIssueProjectList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucIssueProjectList.ucParameterPanel1.vGridControl1, ref ucIssueProjectList.groupControl1);

        }
        #endregion
        #region [ Зураг оруулах ]
        void Function()
        {
            if (_core.Resource != null)
            {
                simpleButton1.Image = _core.Resource.GetImage("navigate_refresh");
                simpleButton2.Image = _core.Resource.GetImage("menu_customer");
            }
        }

        #endregion
        #region [ Лавлагаа холболт ]
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ucIssueProjectList.DataSource != null)
            {
                DataRow DR = ucIssueProjectList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucIssueProjectList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    object[] obj = new object[2];
                    obj[0] = DR;
                    InfoPos.Enquiry.IssueProjectEnquiry enq = new InfoPos.Enquiry.IssueProjectEnquiry(_core, obj);
                    enq.ShowDialog();

                }
            }
        }



        private void ucIssueProjectList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 310069, 310069, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucIssueProjectList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }

        private void IssueProject_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);


            ucIssueProjectList.OnEventFindPaging(0);
        }

        private void IssueProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucIssueProjectList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucIssueProjectList.ucParameterPanel1.vGridControl1, ref ucIssueProjectList.groupControl1);
        }



        private void IssueProject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucIssueProjectList.OnEventFindPaging(0);

            }
        }

        private void ucIssueProjectList_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (ucIssueProjectList.DataSource != null)
            {
                DataRow DR = ucIssueProjectList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucIssueProjectList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    ProjectID = Static.ToLong(DR["ProjectID"]);
                    InfoPos.Issue.FormIssueTracking iss = new InfoPos.Issue.FormIssueTracking(_core, ProjectID, DR);
                    iss.ShowDialog();
                }
            }
        }
    }
}
        #endregion