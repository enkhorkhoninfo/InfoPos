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
    public partial class Issue : Form
    {

        #region [ Variable ]
        Core.Core _core;
        long pPolicyNo;
        int PrivNo = 310079;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Constructor Function ]
        public Issue(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucIssueList.Resource = _core.Resource;

        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            ucIssueList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucIssueList_EventDataChanged);
            ucIssueList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucIssueList_EventFindPaging);
            ucIssueList.EventSelected += new ucGridPanel.delegateEventSelected(ucIssueList_EventSelected);


            Function();
            ucIssueList.FindItemAdd("custno ", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucIssueList.FindItemAdd("issueid ", "Асуудлын дугаар", "", DynamicParameterType.Decimal, false, "d", "");

            ucIssueList.FieldFindAdd("projectid", "Төслийн дугаар", typeof(ArrayList), "");

            ucIssueList.FieldFindAdd("PROJECTCOMPID", "Дэд төрлийн дугаар", typeof(ArrayList), "");
            ucIssueList.FieldFindAdd("IssueTypeID", "Асуудлын төрлийн дугаар", typeof(ArrayList), "");
            ucIssueList.FieldFindAdd("IssuePriorID", "Асуудлын эрэмбэ ID", typeof(ArrayList), "");
            ucIssueList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucIssueList.FieldFindAdd("ResolutionStatus", "Асуудал хаагдсан төлөв", typeof(ArrayList), "");
            ucIssueList.FieldFindAdd("TrackID", "Аль шатлал дээр явж байгаа эсэх", typeof(ArrayList), "");

            ucIssueList.FieldFindAdd("CreateUser", "Үүсгэсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");
            ucIssueList.FieldFindAdd("Createdate", "Асуудал үүсгэсэн огноо, цаг минут", typeof(DateTime), "");
            ucIssueList.FieldFindAdd("UpdateDate", "Сүүлд өөрчлөлт орсон огноо, цаг минут", typeof(DateTime), "");
            ucIssueList.FieldFindAdd("duedate", "Асуудал дуусгана огноо, цаг минут", typeof(DateTime), "");

            ucIssueList.FieldFindAdd("AssigneeUser", "Яг одоо хариуцаж байгаа хэрэглэгч", typeof(ArrayList), "");
            ucIssueList.FieldFindAdd("resolutiondate", "Асуудал хаагдсан огноо, цаг минут", typeof(DateTime), "");
            ucIssueList.FieldFindAdd("resolutionuser", "Асуудал хаагдсан хэрэглэгч", typeof(ArrayList), "");

            ucIssueList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucIssueList.FieldFindRefresh();

            InitDictionary();

            ucIssueList.Visible = true;
            ucIssueList.FindItemSetValue("AssigneeUser", _core.RemoteObject.User.UserNo);
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
                string[] names = new string[] { "ISSUEPROJECT", "PROJECTCOMP", "ISSUETYPES", "ISSUEPRIORITY", "ISSUETRACKS", "USERS" };

                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д төслийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueList.FindItemSetList("projectid", DT, "PROJECTID", "NAME");

                }


                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д төслийн дэд төрлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueList.FindItemSetList("PROJECTCOMPID", DT, "PROJECTCOMPID", "NAME");

                }
                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д асуудлын төрлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueList.FindItemSetList("IssueTypeID", DT, "ISSUETYPEID", "NAME");

                }

                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "Dictionary-д асуудлын эрэмбийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueList.FindItemSetList("IssuePriorID", DT, "ISSUEPRIORID", "NAME");

                }

                DT = (DataTable)Tables[4];
                if (DT == null)
                {
                    msg = "Dictionary-д асуудлын алхамын мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueList.FindItemSetList("TrackID", DT, "ISSUETRACKID", "NAME");

                }


                DT = (DataTable)Tables[5];
                if (DT == null)
                {
                    msg = "Dictionary-д хэрэглэгчийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucIssueList.FindItemSetList("CreateUser", DT, "userno", "userlname");
                    ucIssueList.FindItemSetList("AssigneeUser", DT, "userno", "userlname");
                    ucIssueList.FindItemSetList("resolutionuser", DT, "userno", "userlname");
                }


                ucIssueList.FindItemSetList("Status", 0, "Open");
                ucIssueList.FindItemSetList("Status", 1, "InProgress");
                ucIssueList.FindItemSetList("Status", 2, "ReOpen");
                ucIssueList.FindItemSetList("Status", 3, "Resolved");
                ucIssueList.FindItemSetList("Status", 9, "Closed");



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
        void ucIssueList_EventSelected(DataRow selectedrow)
        {


            if (selectedrow != null)
            {

                if (!ucIssueList.Browsable)
                {


                    //object[] obj = new object[2];
                  //  obj[0] = _core;
                  //  _core = (Core.Core)param[0];
                 long customerno = Static.ToLong(selectedrow["custno"]);
                    long issueid = Static.ToLong(selectedrow["issueid"]);
                    InfoPos.Issue.FormCustomerEnq f1 = new InfoPos.Issue.FormCustomerEnq(_core,customerno,issueid);
                    f1.Show();
                    //EServ.Shared.Static.Invoke("InfoPos.Issue.dll", "InfoPos.Issue.Main", "CallIssue", obj);
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
        void ucIssueList_EventDataChanged()
        {
            ucIssueList.FieldLinkSetColumnCaption(0, "Харилцагчийн дугаар ");
            ucIssueList.FieldLinkSetColumnCaption(1, "Асуудлын дугаар ");

            ucIssueList.FieldLinkSetColumnCaption(2, "Төслийн дугаар");
            ucIssueList.FieldLinkSetColumnCaption(3, "Төслийн нэр");


            ucIssueList.FieldLinkSetColumnCaption(4, "Дэд төрлийн дугаар");
            ucIssueList.FieldLinkSetColumnCaption(5, "Дэд төрлийн нэр");

            ucIssueList.FieldLinkSetColumnCaption(6, "Асуудлын төрлийн дугаар");

            ucIssueList.FieldLinkSetColumnCaption(7, "Асуудлын төрлийн нэр");

            ucIssueList.FieldLinkSetColumnCaption(8, "Асуудлын эрэмбэ ID");

            ucIssueList.FieldLinkSetColumnCaption(9, "Асуудлын эрэмбэ нэр");

            ucIssueList.FieldLinkSetColumnCaption(10, "Асуудлын товч утга");

            ucIssueList.FieldLinkSetColumnCaption(11, "Асуудлын дэлгэрэнгүй мэдээлэл");
            ucIssueList.FieldLinkSetColumnCaption(12, "Төлөв");



            ucIssueList.FieldLinkSetColumnCaption(13, "Төлвийн нэр");

            ucIssueList.FieldLinkSetColumnCaption(14, "Асуудал хаагдсан төлөв ");

            ucIssueList.FieldLinkSetColumnCaption(15, "Аль шатлал дээр явж байгаа эсэхийн дугаар");
            ucIssueList.FieldLinkSetColumnCaption(16, "Аль шатлал дээр явж байгаа эсэхийн нэр");
            ucIssueList.FieldLinkSetColumnCaption(17, "Үүсгэсэн хэрэглэгчийн дугаар");


            ucIssueList.FieldLinkSetColumnCaption(18, "Үүсгэсэн хэрэглэгчийн нэр");

            ucIssueList.FieldLinkSetColumnCaption(19, "Асуудал үүсгэсэн огноо, цаг минут");

            ucIssueList.FieldLinkSetColumnCaption(20, "Сүүлд өөрчлөлт орсон огноо, цаг минут");

            ucIssueList.FieldLinkSetColumnCaption(21, "Асуудал дуусгана огноо, цаг минут");
            ucIssueList.FieldLinkSetColumnCaption(22, "Яг одоо хариуцаж байгаа хэрэглэгчийн дугаар");



            ucIssueList.FieldLinkSetColumnCaption(23, "Яг одоо хариуцаж байгаа хэрэглэгчийн нэр");

            ucIssueList.FieldLinkSetColumnCaption(24, "Асуудал хаагдсан огноо, цаг минут ");

            ucIssueList.FieldLinkSetColumnCaption(25, "Асуудал хаагдсан хэрэглэгчийн дугаар");
            ucIssueList.FieldLinkSetColumnCaption(26, "Асуудал хаагдсан хэрэглэгчийн нэр");
            ucIssueList.FieldLinkSetColumnCaption(27, "Нийт саналын тоо");


            FormUtility.RestoreStateGrid(appname, formname, ref  ucIssueList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucIssueList.ucParameterPanel1.vGridControl1, ref ucIssueList.groupControl1);

        }
        #endregion
        #region [ Зураг оруулах ]
        void Function()
        {
            if (_core.Resource != null)
            {
                simpleButton1.Image = _core.Resource.GetImage("navigate_refresh");
            }
        }

        #endregion
        #region [ Лавлагаа холболт ]
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ucIssueList.DataSource != null)
            {
                DataRow DR = ucIssueList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucIssueList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    object[] obj = new object[2];
                    obj[0] = DR;
                    //InfoPos.Enquiry.IssueEnquiry payen = new InfoPos.Enquiry.IssueEnquiry(_core, obj);
                    //payen.ShowDialog();

                }
            }
        }



        private void ucIssueList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 310079, 310079, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucIssueList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }

        private void IssueList_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);

            ucIssueList.OnEventFindPaging(0);
        }

        private void Issue_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucIssueList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucIssueList.ucParameterPanel1.vGridControl1, ref ucIssueList.groupControl1);
        }



        private void Issue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucIssueList.OnEventFindPaging(0);

            }
        }

        private void ucIssueList_Load(object sender, EventArgs e)
        {

        }


    }
}
        #endregion