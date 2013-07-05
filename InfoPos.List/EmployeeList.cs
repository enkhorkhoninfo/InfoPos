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
    public partial class EmployeeList : Form
    {
        private Core.Core _core;
        int PrivNo = 180001;
        string appname = "", formname = "";
        Form FormName = null;
        public EmployeeList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucEmployeeResourceFa.Resource = _core.Resource;
         //  EmployeeList.ActiveForm.AcceptButton = ucEmployeeResourceFa.btnFind;
                
        }
        #region [ Init ]
        private void Init()
        {
            ucEmployeeResourceFa.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucEmployeeResourceFa_EventDataChanged);
            ucEmployeeResourceFa.EventSelected += new ucGridPanel.delegateEventSelected(ucEmployeeResourceFa_EventSelected);
           ucEmployeeResourceFa.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucEmployeeResourceFa_EventFindPaging);

            ucEmployeeResourceFa.FieldFindAdd("EmpNo", "Ажилтны дугаар", typeof(ArrayList), "");
            ucEmployeeResourceFa.FieldFindAdd("Name", "Хэрэглэгчийн нэр", typeof(string), "");
            ucEmployeeResourceFa.FieldFindAdd("Name2", "Хэрэглэгчийн нэр 2", typeof(string), "");
            ucEmployeeResourceFa.FieldFindAdd("Position", "Албан тушаал", typeof(string), "");
            ucEmployeeResourceFa.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");

            ucEmployeeResourceFa.FieldFindAdd("BranchNo", "Салбарын дугаар", typeof(ArrayList), "");
            ucEmployeeResourceFa.FieldFindAdd("UserNo", "Хэрэглэгчийн дугаар", typeof(ArrayList), "");
            ucEmployeeResourceFa.FieldFindAdd("LEVELNO", "Зэрэглэл", typeof(ArrayList), "");

            ucEmployeeResourceFa.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucEmployeeResourceFa.FieldFindRefresh();
            InitDictionary();
            ucEmployeeResourceFa.VisibleFind = true;

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
                string[] names = new string[] { "USERS", "CURRENCY", "EMPLOYEE", "BRANCH" };
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
                    ucEmployeeResourceFa.FindItemSetList("UserNo", DT, "userno", "userlname");
                }

                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д валютын мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucEmployeeResourceFa.FindItemSetList("CurCode", DT, "CURRENCY", "NAME");
                }

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д Ажилтны мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucEmployeeResourceFa.FindItemSetList("EmpNo", DT, "EmpNo", "NAME");
                }

                DT = (DataTable)Tables[3];
                if (DT == null)
                {
                    msg = "Dictionary-д Ажилтны мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucEmployeeResourceFa.FindItemSetList("BranchNo", DT, "Branch", "NAME");
                }
                for (int i = 0; i < 10; i++)
                    ucEmployeeResourceFa.FindItemSetList("LEVELNO", i, Static.ToStr(i) + " - р зэрэглэл");

                ucEmployeeResourceFa.FindItemSetList("Status", 0, "ИДЭВХТЭЙ");
                ucEmployeeResourceFa.FindItemSetList("Status", 9, "ИДЭВХГҮЙ");

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
     
        #region [ LoadData ]
        void LoadData(object[] values)
        {
            Result res = new Result();

            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 180001, 180001, values);

                if (res.ResultNo == 0)
                {
                    ucEmployeeResourceFa.DataSource = res.Data.Tables[0];
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
        #region [ EventSelected ]
        void ucEmployeeResourceFa_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucEmployeeResourceFa.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["EmpNo"]);
                    EServ.Shared.Static.Invoke("InfoPos.FA.dll", "InfoPos.FA.Main", "CallEmployee", obj);
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
        void ucEmployeeResourceFa_EventDataChanged()
        {
            
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(0, "Ажилтаны дугаар");
            //ucEmployeeResourceFa.gridView1.Columns[0].Width = 100;

            ucEmployeeResourceFa.FieldLinkSetColumnCaption(1, "Хэрэглэгчийн нэр");
          //  ucEmployeeResourceFa.gridView1.Columns[1].Width = 80;
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(2, "Хэрэглэгчийн нэр 2");
        //    ucEmployeeResourceFa.gridView1.Columns[2].Width = 90;
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(3, "Албан тушаал");
      //      ucEmployeeResourceFa.gridView1.Columns[3].Width = 80;
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(4, "Төлөв");
            ucEmployeeResourceFa.gridView1.Columns[4].Visible = false;
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(5, "Төлөв");
       
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(6, "Салбарын дугаар");
         //   ucEmployeeResourceFa.gridView1.Columns[6].Visible = false;
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(7, "Салбар");
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(8, "Хэрэглэгчийн дугаар");
            ucEmployeeResourceFa.FieldLinkSetColumnCaption(9, "Зэрэглэл");
            FormUtility.RestoreStateGrid(appname, formname, ref ucEmployeeResourceFa.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucEmployeeResourceFa.ucParameterPanel1.vGridControl1, ref ucEmployeeResourceFa.groupControl1);
        }
        #endregion

        private void EmployeeList_Load(object sender, EventArgs e)
        {
           
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucEmployeeResourceFa.OnEventFindPaging(0);
        }

        private void EmployeeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter) {

                ucEmployeeResourceFa.OnEventFindPaging(0);
            
            }
               
               
        }

        private void EmployeeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucEmployeeResourceFa.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucEmployeeResourceFa.ucParameterPanel1.vGridControl1, ref ucEmployeeResourceFa.groupControl1);
        }

        private void ucEmployeeResourceFa_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 180001, 180001, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 


            ucEmployeeResourceFa.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
