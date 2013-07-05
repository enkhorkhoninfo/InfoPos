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
    public partial class IssueContact : Form
    {

        #region [ Variable ]
        Core.Core _core;
        long pPolicyNo;
        int PrivNo = 310079;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Constructor Function ]
        public IssueContact(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucIssueContactList.Resource = _core.Resource;

        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            ucIssueContactList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucIssueList_EventDataChanged);
            ucIssueContactList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucIssueList_EventFindPaging);
            ucIssueContactList.EventSelected += new ucGridPanel.delegateEventSelected(ucIssueList_EventSelected);


            Function();
            ucIssueContactList.FindItemAdd("custno ", "Харилцагчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucIssueContactList.FindItemAdd("issueid ", "Асуудлын дугаар", "", DynamicParameterType.Decimal, false, "d", "");

            ucIssueContactList.FieldFindAdd("projectid", "Төслийн дугаар", typeof(ArrayList), "");

            ucIssueContactList.FieldFindAdd("PROJECTCOMPID", "Дэд төрлийн дугаар", typeof(ArrayList), "");
            ucIssueContactList.FieldFindAdd("IssueTypeID", "Асуудлын төрлийн дугаар", typeof(ArrayList), "");
            ucIssueContactList.FieldFindAdd("IssuePriorID", "Асуудлын эрэмбэ ID", typeof(ArrayList), "");
            ucIssueContactList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucIssueContactList.FieldFindAdd("ResolutionStatus", "Асуудал хаагдсан төлөв", typeof(ArrayList), "");
            ucIssueContactList.FieldFindAdd("TrackID", "Аль шатлал дээр явж байгаа эсэх", typeof(ArrayList), "");

            ucIssueContactList.FieldFindAdd("CreateUser", "Үүсгэсэн хэрэглэгчийн дугаар", typeof(ArrayList), "");
            ucIssueContactList.FieldFindAdd("Createdate", "Асуудал үүсгэсэн огноо, цаг минут", typeof(DateTime), "");
            ucIssueContactList.FieldFindAdd("UpdateDate", "Сүүлд өөрчлөлт орсон огноо, цаг минут", typeof(DateTime), "");
            ucIssueContactList.FieldFindAdd("duedate", "Асуудал дуусгана огноо, цаг минут", typeof(DateTime), "");

            ucIssueContactList.FieldFindAdd("AssigneeUser", "Яг одоо хариуцаж байгаа хэрэглэгч", typeof(ArrayList), "");
            ucIssueContactList.FieldFindAdd("resolutiondate", "Асуудал хаагдсан огноо, цаг минут", typeof(DateTime), "");
            ucIssueContactList.FieldFindAdd("resolutionuser", "Асуудал хаагдсан хэрэглэгч", typeof(ArrayList), "");

            ucIssueContactList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucIssueContactList.FieldFindRefresh();

            InitDictionary();

            ucIssueContactList.Visible = true;
            ucIssueContactList.FindItemSetValue("AssigneeUser", _core.RemoteObject.User.UserNo);
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
                    ucIssueContactList.FindItemSetList("projectid", DT, "PROJECTID", "NAME");

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
                    ucIssueContactList.FindItemSetList("PROJECTCOMPID", DT, "PROJECTCOMPID", "NAME");

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
                    ucIssueContactList.FindItemSetList("IssueTypeID", DT, "ISSUETYPEID", "NAME");

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
                    ucIssueContactList.FindItemSetList("IssuePriorID", DT, "ISSUEPRIORID", "NAME");

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
                    ucIssueContactList.FindItemSetList("TrackID", DT, "ISSUETRACKID", "NAME");

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
                    ucIssueContactList.FindItemSetList("CreateUser", DT, "userno", "userlname");
                    ucIssueContactList.FindItemSetList("AssigneeUser", DT, "userno", "userlname");
                    ucIssueContactList.FindItemSetList("resolutionuser", DT, "userno", "userlname");
                }


                ucIssueContactList.FindItemSetList("Status", 0, "Open");
                ucIssueContactList.FindItemSetList("Status", 1, "InProgress");
                ucIssueContactList.FindItemSetList("Status", 2, "ReOpen");
                ucIssueContactList.FindItemSetList("Status", 3, "Resolved");
                ucIssueContactList.FindItemSetList("Status", 9, "Closed");



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

                if (!ucIssueContactList.Browsable)
                {


                    long customerno = Static.ToLong(selectedrow["custno"]);
                    long issueid = Static.ToLong(selectedrow["issueid"]);
                    InfoPos.Issue.FormContactEnq f1 = new InfoPos.Issue.FormContactEnq(_core, customerno, issueid);
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
        void ucIssueList_EventDataChanged()
        {
            ucIssueContactList.FieldLinkSetColumnCaption(0, "Харилцагчийн дугаар ");
            ucIssueContactList.FieldLinkSetColumnCaption(1, "Асуудлын дугаар ");

            ucIssueContactList.FieldLinkSetColumnCaption(2, "Төслийн дугаар");
            ucIssueContactList.FieldLinkSetColumnCaption(3, "Төслийн нэр");


            ucIssueContactList.FieldLinkSetColumnCaption(4, "Дэд төрлийн дугаар");
            ucIssueContactList.FieldLinkSetColumnCaption(5, "Дэд төрлийн нэр");

            ucIssueContactList.FieldLinkSetColumnCaption(6, "Асуудлын төрлийн дугаар");

            ucIssueContactList.FieldLinkSetColumnCaption(7, "Асуудлын төрлийн нэр");

            ucIssueContactList.FieldLinkSetColumnCaption(8, "Асуудлын эрэмбэ ID");

            ucIssueContactList.FieldLinkSetColumnCaption(9, "Асуудлын эрэмбэ нэр");

            ucIssueContactList.FieldLinkSetColumnCaption(10, "Асуудлын товч утга");

            ucIssueContactList.FieldLinkSetColumnCaption(11, "Асуудлын дэлгэрэнгүй мэдээлэл");
            ucIssueContactList.FieldLinkSetColumnCaption(12, "Төлөв");



            ucIssueContactList.FieldLinkSetColumnCaption(13, "Төлвийн нэр");

            ucIssueContactList.FieldLinkSetColumnCaption(14, "Асуудал хаагдсан төлөв ");

            ucIssueContactList.FieldLinkSetColumnCaption(15, "Аль шатлал дээр явж байгаа эсэхийн дугаар");
            ucIssueContactList.FieldLinkSetColumnCaption(16, "Аль шатлал дээр явж байгаа эсэхийн нэр");
            ucIssueContactList.FieldLinkSetColumnCaption(17, "Үүсгэсэн хэрэглэгчийн дугаар");


            ucIssueContactList.FieldLinkSetColumnCaption(18, "Үүсгэсэн хэрэглэгчийн нэр");

            ucIssueContactList.FieldLinkSetColumnCaption(19, "Асуудал үүсгэсэн огноо, цаг минут");

            ucIssueContactList.FieldLinkSetColumnCaption(20, "Сүүлд өөрчлөлт орсон огноо, цаг минут");

            ucIssueContactList.FieldLinkSetColumnCaption(21, "Асуудал дуусгана огноо, цаг минут");
            ucIssueContactList.FieldLinkSetColumnCaption(22, "Яг одоо хариуцаж байгаа хэрэглэгчийн дугаар");



            ucIssueContactList.FieldLinkSetColumnCaption(23, "Яг одоо хариуцаж байгаа хэрэглэгчийн нэр");

            ucIssueContactList.FieldLinkSetColumnCaption(24, "Асуудал хаагдсан огноо, цаг минут ");

            ucIssueContactList.FieldLinkSetColumnCaption(25, "Асуудал хаагдсан хэрэглэгчийн дугаар");
            ucIssueContactList.FieldLinkSetColumnCaption(26, "Асуудал хаагдсан хэрэглэгчийн нэр");
            ucIssueContactList.FieldLinkSetColumnCaption(27, "Нийт саналын тоо");


            FormUtility.RestoreStateGrid(appname, formname, ref  ucIssueContactList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucIssueContactList.ucParameterPanel1.vGridControl1, ref ucIssueContactList.groupControl1);

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
            if (ucIssueContactList.DataSource != null)
            {
                DataRow DR = ucIssueContactList.gridView1.GetFocusedDataRow();
                DataView DV = (DataView)ucIssueContactList.gridView1.DataSource;
                DataTable DT = DV.ToTable();
                if (DR != null && DV != null)
                {
                    object[] obj = new object[2];
                    obj[0] = DR;
                    InfoPos.Enquiry.IssueContactEnquiry payen = new InfoPos.Enquiry.IssueContactEnquiry(_core, obj);
                    payen.ShowDialog();

                }
            }
        }



        private void ucIssueList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 310142, 310142, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucIssueContactList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }

        private void IssueList_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);

            ucIssueContactList.OnEventFindPaging(0);
        }

        private void Issue_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucIssueContactList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucIssueContactList.ucParameterPanel1.vGridControl1, ref ucIssueContactList.groupControl1);
        }



        private void Issue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucIssueContactList.OnEventFindPaging(0);

            }
        }

        private void ucIssueList_Load(object sender, EventArgs e)
        {

        }


    }
}
        #endregion