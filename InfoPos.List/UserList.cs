using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using EServ.Shared;
using ISM.Template;


namespace InfoPos.List
{
    public partial class UserList : Form
    {   
        private Core.Core _core;
        int PrivNo = 110100;
        string appname = "", formname = "";
        Form FormName = null;
        public UserList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucUserList.Resource = _core.Resource;
          //UserList.ActiveForm.AcceptButton = ucUserList.btnFind;
        }
        #region [ Init ]
        private void Init()
        {
            ucUserList.EventSelected += new ISM.Template.ucGridPanel.delegateEventSelected(ucUserList_EventSelected);
            ucUserList.EventDataChanged += new ISM.Template.ucGridPanel.delegateEventDataChanged(ucUserList_EventDataChanged);
           ucUserList.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucUserList_EventFindPaging);
           ucUserList.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);

           labelControl1.BackColor = Color.NavajoWhite;
           //         labelControl3.BackColor = Color.White;
           labelControl2.BackColor = Color.Orange;

           labelControl1.ForeColor = Color.NavajoWhite;
           //   labelControl3.ForeColor = Color.White;
           labelControl2.ForeColor = Color.Orange;
            ucUserList.FindItemAdd("UserNo", "Хэрэглэгчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucUserList.FieldFindAdd("USERFNAME", "Хэрэглэгчийн овог", typeof(string), "");
            ucUserList.FieldFindAdd("USERLNAME", "Хэрэглэгчийн нэр", typeof(string), "");
            ucUserList.FieldFindAdd("UserFname2", "Хэрэглэгчийн овог 2", typeof(string), "");
            ucUserList.FieldFindAdd("UserLname2", "Хэрэглэгчийн нэр 2", typeof(string), "");

            ucUserList.FieldFindAdd("RegisterNo", "Регистерийн дугаар", typeof(string), "");
            ucUserList.FieldFindAdd("Position", "Албан тушаал", typeof(string), "");
            ucUserList.FieldFindAdd("Status", "Төлөв", typeof(ArrayList), "");
            ucUserList.FieldFindAdd("BranchNo", "Салбарын дугаар", typeof(ArrayList), "");
            ucUserList.FieldFindAdd("UserLevel", "Зэрэглэл", typeof(ArrayList), "");

            ucUserList.FieldFindAdd("UserType", "Хэрэглэгчийн төрөл", typeof(ArrayList), "");
            ucUserList.FieldFindAdd("Email", "Майл хаяг", typeof(string), "");
            ucUserList.FindItemAdd("Mobile", "Гар утасны дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucUserList.FieldFindAdd("AgentCorpno", "Агентын байгууллагын дугаар", typeof(ArrayList), "");
            ucUserList.FieldFindAdd("AgentBranchno", "Агентын салбарын дугаар", typeof(ArrayList), "");

            ucUserList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucUserList.FieldFindRefresh();
          InitDictionary();
            ucUserList.VisibleFind = true;
        }

        void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

                string status = View.GetRowCellDisplayText(e.RowHandle, View.Columns[3]);
                if (status == "0")
                {
                    ucUserList.gridView1.Appearance.OddRow.BackColor = Color.NavajoWhite;
                    ucUserList.gridView1.Appearance.OddRow.BackColor2 = Color.NavajoWhite;
                    ucUserList.gridView1.Appearance.EvenRow.BackColor = Color.NavajoWhite;
                    ucUserList.gridView1.Appearance.EvenRow.BackColor2 = Color.NavajoWhite;
                    e.Appearance.BackColor = Color.NavajoWhite;
                    e.Appearance.BackColor2 = Color.NavajoWhite;


                }
                if (status == "9")
                {
                    ucUserList.gridView1.Appearance.OddRow.BackColor = Color.Orange;
                    ucUserList.gridView1.Appearance.OddRow.BackColor2 = Color.Orange;
                    ucUserList.gridView1.Appearance.EvenRow.BackColor = Color.Orange;
                    ucUserList.gridView1.Appearance.EvenRow.BackColor2 = Color.Orange;
                    e.Appearance.BackColor = Color.Orange;
                    e.Appearance.BackColor2 = Color.Orange;
           
                }
             
            }
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
                #region [ Dictionary ]
                string[] names = new string[] { "BRANCH","AGENTCORP","AGENTBRANCH" };
                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д салбарын мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucUserList.FindItemSetList("BranchNo", DT, "BRANCH", "NAME", "", new int[] { 2, 3, 4 });
                }
                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д BRANCH мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucUserList.FindItemSetList("AgentCorpno", DT, "AgentCorpno", "NAME");
                }
                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д BRANCH мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucUserList.FindItemSetList("AgentBranchno", DT, "AgentBranchno", "NAME");
                }
                #endregion

                for (int i = 0; i < 10; i++)
                    ucUserList.FindItemSetList("UserLevel", i, Static.ToStr(i) + " - Р ЗЭРЭГЛЭЛ");

                ucUserList.FindItemSetList("UserType", 0, "ҮНДСЭН ХЭРЭГЛЭГЧ");
                ucUserList.FindItemSetList("UserType", 1, "АГЕНТ ХЭРЭГЛЭГЧ");

                ucUserList.FindItemSetList("Status", 0, "ИДЭВХТЭЙ");
                ucUserList.FindItemSetList("Status", 9, "ИДЭВХГҮЙ");

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
        void ucUserList_EventDataChanged()
        {

            
            ucUserList.FieldLinkSetColumnCaption(0, "Хэрэглэгчийн дугаар");
      
            ucUserList.FieldLinkSetColumnCaption(1, "Хэрэглэгчийн овог");
         


            ucUserList.FieldLinkSetColumnCaption(2, "Хэрэглэгчийн нэр");

            ucUserList.FieldLinkSetColumnCaption(3, "Төлөв",false);

            ucUserList.gridView1.Columns[3].Visible = false;
            ucUserList.FieldLinkSetColumnCaption(4, "Хэрэглэгчийн овог2");



            ucUserList.FieldLinkSetColumnCaption(5, "Хэрэглэгчийн нэр2");
      
            //ucUserList.FieldLinkSetColumnCaption(3, "Хэрэглэгчийн овог 2");
        
            //ucUserList.FieldLinkSetColumnCaption(4, "Хэрэглэгчийн нэр 2");
         
            //ucUserList.FieldLinkSetColumnCaption(5, "Регистерийн дугаар");
         
            //ucUserList.FieldLinkSetColumnCaption(6, "Албан тушаал");
      
            //ucUserList.FieldLinkSetColumnCaption(7, "Төлөв");
           
            //ucUserList.FieldLinkSetColumnCaption(8, "Салбарын дугаар");
          
            //ucUserList.FieldLinkSetColumnCaption(9, "Зэрэглэл");
            //ucUserList.FieldLinkSetColumnCaption(10, "Нууц үг");
            //ucUserList.FieldLinkSetColumnCaption(11, "Хэрэглэгчийн төрөл");
            //ucUserList.FieldLinkSetColumnCaption(12, "Мэйл хаяг");
          
            //ucUserList.FieldLinkSetColumnCaption(13, "Гар утасны дугаар");
            //ucUserList.FieldLinkSetColumnCaption(14, "Агент байгууллагын дугаар");
         
            //ucUserList.FieldLinkSetColumnCaption(15, "Агентын салбарын дугаар");

            FormUtility.RestoreStateGrid(appname, formname, ref ucUserList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucUserList.ucParameterPanel1.vGridControl1, ref ucUserList.groupControl1);



        }
        #endregion
        #region [ EventSelected ]
        void ucUserList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucUserList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["UserNo"]);

                    EServ.Shared.Static.Invoke("InfoPos.Admin.dll", "InfoPos.Admin.Main", "CallUserProp", obj);
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
        #endregion
        private void UserList_Load(object sender, EventArgs e)
        {
            ucUserList.gridView1.Appearance.EvenRow.Options.UseBackColor = false;
            ucUserList.gridView1.Appearance.OddRow.Options.UseBackColor = false;
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucUserList.OnEventFindPaging(0);
        }
        private void UserList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucUserList.OnEventFindPaging(0);

            }
        }
        private void UserList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucUserList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucUserList.ucParameterPanel1.vGridControl1, ref ucUserList.groupControl1);
        }
        private void ucUserList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 110100, 110100, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucUserList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
        private void ucUserList_Load(object sender, EventArgs e)
        {

        }
    }
}

