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
    public partial class PackMainList : Form
    {
        private Core.Core _core;
        int PrivNo = 140246;
        string appname = "", formname = "";
        Form FormName = null;
        public PackMainList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucPackMain.Resource = _core.Resource;
        }
        #region [ Init ]
        private void Init()
        {
            ucPackMain.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucEmployeeResourceFa_EventDataChanged);
            ucPackMain.EventSelected += new ucGridPanel.delegateEventSelected(ucEmployeeResourceFa_EventSelected);
            ucPackMain.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucEmployeeResourceFa_EventFindPaging);

//"a.PackId like","a.Name like","a.Name2 like","a.Note like","a.StartDate",
//"a.EndDate","a.Type","a.Status","a.SalesUser","a.SalesCreated"

            ucPackMain.FieldFindAdd("PackId", "Багцын дугаар", typeof(string), "");
            ucPackMain.FieldFindAdd("Name", "Багцын нэр", typeof(string), "");
            ucPackMain.FieldFindAdd("Name2", "Багцын нэр 2", typeof(string), "");
            ucPackMain.FieldFindAdd("Note", "Багцын тайлбар", typeof(string), "");
            ucPackMain.FieldFindAdd("StartDate", "Эхлэх огноо", typeof(DateTime), "");

            ucPackMain.FieldFindAdd("EndDate", "Дуусах огноо", typeof(DateTime), "");
            ucPackMain.FieldFindAdd("Type", "Багцын төрөл", typeof(ArrayList), "");
            ucPackMain.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucPackMain.FieldFindAdd("SalesUser", "Борлуулагчийн дугаар", typeof(ArrayList), "");
            ucPackMain.FieldFindAdd("SalesCreated", "Үүсэгсэн огноо", typeof(DateTime), "");

            ucPackMain.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucPackMain.FieldFindRefresh();
            InitDictionary();
            ucPackMain.VisibleFind = true;

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
                    ucPackMain.FindItemSetList("SalesUser", DT, "userno", "userlname");
                }

                ucPackMain.FindItemSetList("Type", 0, "НИЙТИЙН");
                ucPackMain.FindItemSetList("Type", 1, "ХУВИЙН");

                ucPackMain.FindItemSetList("Status", 0, "ИДЭВХГҮЙ");
                ucPackMain.FindItemSetList("Status", 1, "ИДЭВХТЭЙ");

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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140246, 140246, values);

                if (res.ResultNo == 0)
                {
                    ucPackMain.DataSource = res.Data.Tables[0];
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
                if (!ucPackMain.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToStr(selectedrow["PackID"]);
                    EServ.Shared.Static.Invoke("InfoPos.Parameter.dll", "InfoPos.Parameter.Main", "CallFormPackMain", obj);
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
//a.PackId, a.Name, a.Name2, a.Note, a.StartDate, a.EndDate,
//a.Type, decode(a.Type, 0, 'НИЙТИЙН', 1, 'ХУВИЙН') as TypeName, a.Status, 
//decode(a.Status, 0, 'ИДЭВХГҮЙ', 1, 'ИДЭВХТЭЙ') as StatusName,
//a.SalesUser, a.SalesCreated
            
            ucPackMain.FieldLinkSetColumnCaption(0, "Багцын дугаар");
            ucPackMain.FieldLinkSetColumnCaption(1, "Багцын нэр");
            ucPackMain.FieldLinkSetColumnCaption(2, "Багцын нэр 2");
            ucPackMain.FieldLinkSetColumnCaption(3, "Тайлбар");
            ucPackMain.FieldLinkSetColumnCaption(4, "Эхлэх огноо");
            ucPackMain.FieldLinkSetColumnCaption(5, "Дуусах огноо");
            ucPackMain.FieldLinkSetColumnCaption(6, "Төрлийн дугаар");
            ucPackMain.FieldLinkSetColumnCaption(7, "Төрлийн нэр");
            ucPackMain.FieldLinkSetColumnCaption(8, "Төлөв");
            ucPackMain.FieldLinkSetColumnCaption(9, "Төлвийн нэр");
            ucPackMain.FieldLinkSetColumnCaption(10, "Борлуулагчийн дугаар");
            ucPackMain.FieldLinkSetColumnCaption(11, "Багц үүсгэсэн огноо");
            FormUtility.RestoreStateGrid(appname, formname, ref ucPackMain.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucPackMain.ucParameterPanel1.vGridControl1, ref ucPackMain.groupControl1);
        }
        #endregion
        private void EmployeeList_Load(object sender, EventArgs e)
        {
           
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucPackMain.OnEventFindPaging(0);
        }
        private void EmployeeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter) {

                ucPackMain.OnEventFindPaging(0);
            }
        }
        private void EmployeeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucPackMain.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucPackMain.ucParameterPanel1.vGridControl1, ref ucPackMain.groupControl1);
        }
        private void ucEmployeeResourceFa_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140246, 140246, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 


            ucPackMain.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
