using System;
using System.Collections.Generic;
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
    public partial class GroupUserList : Form
    {
        private Core.Core _core;
        string appname = "", formname = "";
        Form FormName = null;
        public GroupUserList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucGroupUserList.Resource = _core.Resource;
      //    GroupUserList.ActiveForm.AcceptButton = ucGroupUserList.btnFind;

        }
        #region [ Init ] 
        private void Init()
        {
            ucGroupUserList.EventSelected += new ISM.Template.ucGridPanel.delegateEventSelected(ucGroupUserList_EventSelected);
            ucGroupUserList.EventDataChanged += new ISM.Template.ucGridPanel.delegateEventDataChanged(ucGroupUserList_EventDataChanged);
           ucGroupUserList.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucGroupUserList_EventFindPaging);

            ucGroupUserList.FindItemAdd("GROUPID", "Гүйлгээний эрхийн бүлгийн код", "", DynamicParameterType.Decimal, false, "d", "");
            ucGroupUserList.FieldFindAdd("Name", "Гүйлгээний нэр", typeof(string), "");
            ucGroupUserList.FieldFindAdd("Name2 ", "Гүйлгээний нэр 2", typeof(string), "");
            ucGroupUserList.FindItemAdd("OrderNo", "Эрэмбэ", "", DynamicParameterType.Decimal, false, "d", "");

            ucGroupUserList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucGroupUserList.FieldFindRefresh();
            ucGroupUserList.VisibleFind = true;
        }
        #endregion
        #region [ EventDataChanged ]
        void ucGroupUserList_EventDataChanged()
        {
          
            ucGroupUserList.FieldLinkSetColumnCaption(0, "Гүйлгээний эрхийн бүлгийн код");
            ucGroupUserList.FieldLinkSetColumnCaption(1, "Гүйлгээний нэр");
            //ucGroupUserList.gridView1.Columns[1].Width = 180;
            ucGroupUserList.FieldLinkSetColumnCaption(2, "Гүйлгээний нэр 2");
            //ucGroupUserList.gridView1.Columns[2].Width = 100;
            ucGroupUserList.FieldLinkSetColumnCaption(3, "Зөвхөн өөрийн салбар");
            ucGroupUserList.FieldLinkSetColumnCaption(4, "Зэрэглэлийн түвшин");
            //ucGroupUserList.gridView1.Columns[3].Width = 100;
            FormUtility.RestoreStateGrid(appname, formname, ref ucGroupUserList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucGroupUserList.ucParameterPanel1.vGridControl1, ref ucGroupUserList.groupControl1);
        }
        #endregion
        #region [ EventSelected ]
        void ucGroupUserList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucGroupUserList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["GROUPID"]);
                    EServ.Shared.Static.Invoke("InfoPos.Admin.dll", "InfoPos.Admin.Main", "CallGroupProp", obj);
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
        #endregion

        #region [ LoadData ]
        void LoadData(object[] values)
        {
            Result res = new Result();

            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 110105, 110105, values);

                if (res.ResultNo == 0)
                {
                    ucGroupUserList.DataSource = res.Data.Tables[0];
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
        #region [ Load ]
        private void GroupUserList_Load(object sender, EventArgs e)
        {
          
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucGroupUserList.OnEventFindPaging(0);
        }
        #endregion

        private void GroupUserList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucGroupUserList.OnEventFindPaging(0);

            }
        }

        private void GroupUserList_FormClosing(object sender, FormClosingEventArgs e)
        {

            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucGroupUserList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucGroupUserList.ucParameterPanel1.vGridControl1, ref ucGroupUserList.groupControl1);
        }

        private void ucGroupUserList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 110105, 110105, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucGroupUserList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 


        }
    }
}

