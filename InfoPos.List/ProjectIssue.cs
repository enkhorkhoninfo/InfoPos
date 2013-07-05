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
    public partial class ProjectIssue : Form
    {

        #region [ Variable ]
        Core.Core _core;
        long pPolicyNo;
        long ProjectID;
        int PrivNo = 310143;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Constructor Function ]
        public ProjectIssue(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucProjectIssueList.Resource = _core.Resource;

        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            ucProjectIssueList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucIssueProjectList_EventDataChanged);
            ucProjectIssueList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucIssueProjectList_EventFindPaging);
            ucProjectIssueList.EventSelected += new ucGridPanel.delegateEventSelected(ucIssueProjectList_EventSelected);


            Function();
            ucProjectIssueList.FindItemAdd("IssueID", "Асуудлын дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucProjectIssueList.FindItemAdd("ProjectID", "Дэд төрлийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");

            ucProjectIssueList.FieldFindAdd("status", "Төлөв", typeof(ArrayList), "");

            ucProjectIssueList.FieldFindAdd("name", "Нэр", typeof(string), "");
            ucProjectIssueList.FieldFindAdd("name2", "Нэр 2", typeof(string), "");
            ucProjectIssueList.FieldFindAdd("ShortName ", "Товч нэр ", typeof(string), "");
            ucProjectIssueList.FieldFindAdd("ShortName2", "Товч нэр 2", typeof(string), "");
            ucProjectIssueList.FieldFindAdd("owneruser", "Төслийг хариуцагч", typeof(ArrayList), "");

            ucProjectIssueList.FieldFindAdd("startdate", "Төслийн эхлэх огноо", typeof(DateTime), "");
            ucProjectIssueList.FieldFindAdd("enddate", "Төслийн дуусах огноо", typeof(DateTime), "");
            ucProjectIssueList.FieldFindAdd("projecttypeid", "Төслийн төрлийн ID", typeof(ArrayList), "");
            ucProjectIssueList.FieldFindAdd("createuserno", "Үүсгэсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");

            ucProjectIssueList.FieldFindAdd("createdate", "Төслийг үүсгэсэн огноо", typeof(DateTime), "");
            ucProjectIssueList.FieldFindAdd("NotifySchemaID", "Мэдэгдэлийн схем ID", typeof(ArrayList), "");
            ucProjectIssueList.FieldFindAdd("PermSchemaID", "Эрхийн схем ID", typeof(ArrayList), "");

            ucProjectIssueList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucProjectIssueList.FieldFindRefresh();

            InitDictionary();

            ucProjectIssueList.Visible = true;
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
                    ucProjectIssueList.FindItemSetList("owneruser", DT, "userno", "userlname");
                    ucProjectIssueList.FindItemSetList("createuserno", DT, "userno", "userlname");


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
                    ucProjectIssueList.FindItemSetList("projecttypeid", DT, "PROJECTTYPEID", "NAME");

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
                    ucProjectIssueList.FindItemSetList("NotifySchemaID", DT, "SCHEMAID", "NAME");

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
                    ucProjectIssueList.FindItemSetList("PermSchemaID", DT, "PERMSCHEMAID", "NAME");

                }


                ucProjectIssueList.FindItemSetList("status", 0, "Нээлттэй");
                ucProjectIssueList.FindItemSetList("status", 1, "Хаагдсан");



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

                if (!ucProjectIssueList.Browsable)
                {


                          long projectid = Static.ToLong(selectedrow["projectid"]);
                    long issueid = Static.ToLong(selectedrow["issueid"]);
                //    EServ.Shared.Static.Invoke("InfoPos.Parameter.dll", "InfoPos.Parameter.Main", "CallCRMIssueProject", obj);
                    InfoPos.Issue.FormProjectEnq f1 = new InfoPos.Issue.FormProjectEnq(_core,projectid,issueid);
                    f1.Show();
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
            ucProjectIssueList.FieldLinkSetColumnCaption(0, "Асуудлын дугаар ");
            ucProjectIssueList.FieldLinkSetColumnCaption(1, "Төслийн дугаар ");

            ucProjectIssueList.FieldLinkSetColumnCaption(2, "Төлөв");

            ucProjectIssueList.FieldLinkSetColumnCaption(3, "Төлвийн нэр");

            ucProjectIssueList.FieldLinkSetColumnCaption(4, "Нэр");

            ucProjectIssueList.FieldLinkSetColumnCaption(5, "Нэр 2");

            ucProjectIssueList.FieldLinkSetColumnCaption(6, "Товч нэр ");

            ucProjectIssueList.FieldLinkSetColumnCaption(7, "Товч нэр 2");

            ucProjectIssueList.FieldLinkSetColumnCaption(8, "Төслийн дэлгэрэнгүй мэдээлэл");

            ucProjectIssueList.FieldLinkSetColumnCaption(9, "Төслийг хариуцагч");
            ucProjectIssueList.FieldLinkSetColumnCaption(10, "Төслийг хариуцагч");



            ucProjectIssueList.FieldLinkSetColumnCaption(11, "Төслийн эхлэх огноо ");

            ucProjectIssueList.FieldLinkSetColumnCaption(12, "Төслийн дуусах огноо ");

            ucProjectIssueList.FieldLinkSetColumnCaption(13, "Төслийн төрлийн ID");
            ucProjectIssueList.FieldLinkSetColumnCaption(14, "Төслийн төрлийн нэр");
            ucProjectIssueList.FieldLinkSetColumnCaption(15, "Үүсгэсэн хэрэглэгчийн дугаар");


            ucProjectIssueList.FieldLinkSetColumnCaption(16, "Үүсгэсэн хэрэглэгчийн нэр");
            ucProjectIssueList.FieldLinkSetColumnCaption(17, "Төслийг үүсгэсэн огноо");



            ucProjectIssueList.FieldLinkSetColumnCaption(18, "Мэдэгдэлийн схем ID ");

            ucProjectIssueList.FieldLinkSetColumnCaption(19, "Мэдэгдэлийн схем нэр ");

            ucProjectIssueList.FieldLinkSetColumnCaption(20, "Эрхийн схем ID");
            ucProjectIssueList.FieldLinkSetColumnCaption(21, "Эрхийн схем нэр");
            ucProjectIssueList.FieldLinkSetColumnCaption(22, "Жагсаалтын эрэмбэ");

            FormUtility.RestoreStateGrid(appname, formname, ref  ucProjectIssueList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucProjectIssueList.ucParameterPanel1.vGridControl1, ref ucProjectIssueList.groupControl1);

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
            if (ucProjectIssueList.DataSource != null)
            {
                DataRow DR = ucProjectIssueList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucProjectIssueList.gridView1.DataSource;
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
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 310143, 310143, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucProjectIssueList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }

        private void IssueProject_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);


            ucProjectIssueList.OnEventFindPaging(0);
        }

        private void IssueProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucProjectIssueList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucProjectIssueList.ucParameterPanel1.vGridControl1, ref ucProjectIssueList.groupControl1);
        }



        private void IssueProject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucProjectIssueList.OnEventFindPaging(0);

            }
        }

        private void ucIssueProjectList_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (ucProjectIssueList.DataSource != null)
            {
                DataRow DR = ucProjectIssueList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucProjectIssueList.gridView1.DataSource;
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