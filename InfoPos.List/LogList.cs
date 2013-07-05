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
    public partial class LogList : Form
    {
        private Core.Core _core;
        long pLogID;
        int PrivNo = 110200;
        string appname = "", formname = "";
        Form FormName = null;
        public LogList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucLogList.Resource = _core.Resource;
            // ContractList.ActiveForm.AcceptButton = ucContractList.btnFind;
        }
        private void Init()
        {
            ucLogList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucLogList_EventDataChanged);
            // ucLogList.EventSelected += new ucGridPanel.delegateEventSelected(ucContractList_EventSelected);
            ucLogList.EventFindPaging += new ucGridPanel.delegateEventFindPaging(ucLogList_EventFindPaging);

            Function();
            ucLogList.FindItemAdd("LogID", "Логын ID", "", DynamicParameterType.Decimal, false, "d", "");
            ucLogList.FieldFindAdd("TxnDate", "Гүйлгээний огноо", typeof(DateTime), "");
            ucLogList.FieldFindAdd("PostDate", "Цаг минут", typeof(DateTime), "");
            ucLogList.FieldFindAdd("UserNo", "Хэрэглэгчийн дугаар", typeof(ArrayList), "");
            ucLogList.FieldFindAdd("BranchNo", "Салбар", typeof(ArrayList), "");
            ucLogList.FindItemAdd("SUPERVISORNO", "Хянасан хэрэглэгчийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucLogList.FieldFindAdd("TXNCODE", "Гүйлгээний код", typeof(ArrayList), "");


            ucLogList.FieldFindAdd("Description", "Гүйлгээний тайлбар", typeof(string), "");
            ucLogList.FieldFindAdd("Note", "Тэмдэглэл", typeof(string), "");

            ucLogList.FindItemAdd("RESULTNO", "Үр дүн", "", DynamicParameterType.Decimal, false, "d", "");

            ucLogList.FieldFindAdd("RESULTDESC", "Үр дүнгийн тайлбар", typeof(string), "");





            ucLogList.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";

            ucLogList.FieldFindRefresh();
            InitDictionary();
            ucLogList.VisibleFind = true;
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
                string[] names = new string[] { "USERS", "BRANCH", "TXNCODE" };
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
                    ucLogList.FindItemSetList("UserNo", DT, "userno", "userlname");

                }

                DT = (DataTable)Tables[1];
                if (DT == null)
                {
                    msg = "Dictionary-д Салбарын мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucLogList.FindItemSetList("BranchNo", DT, "BRANCH", "NAME");
                }

                DT = (DataTable)Tables[2];
                if (DT == null)
                {
                    msg = "Dictionary-д Хаагдсан төрлийн мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucLogList.FindItemSetList("TXNCODE", DT, "TRANCODE", "NAME");
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
        #region [ EventDataChanged ]
        void ucLogList_EventDataChanged()
        {





            ucLogList.FieldLinkSetColumnCaption(0, "Логын ID");
            //ucContractList.gridView1.Columns[0].Width = 100;

            ucLogList.FieldLinkSetColumnCaption(1, "Гүйлгээний огноо");
            // ucContractList.gridView1.Columns[1].Width = 100;
            ucLogList.FieldLinkSetColumnCaption(2, "Цаг минут");
            //   ucContractList.gridView1.Columns[2].Width = 100;
            ucLogList.FieldLinkSetColumnCaption(3, "Хэрэглэгчийн дугаар");
            //ucContractList.gridView1.Columns[3].Width = 100;
            ucLogList.FieldLinkSetColumnCaption(4, "Хэрэглэгчийн нэр");
            //ucContractList.gridView1.Columns[4].Width = 100;
            ucLogList.FieldLinkSetColumnCaption(5, "Салбарын дугаар");
            //ucContractList.gridView1.Columns[5].Width = 100;
            ucLogList.FieldLinkSetColumnCaption(6, "Салбарын нэр");
            //ucContractList.gridView1.Columns[6].Width = 100;
            ucLogList.FieldLinkSetColumnCaption(7, "Хянасан хэрэглэгчийн дугаар");

            ucLogList.FieldLinkSetColumnCaption(8, "Гүйлгээний код");
            ucLogList.FieldLinkSetColumnCaption(9, "Гүйлгээний нэр");
            ucLogList.FieldLinkSetColumnCaption(10, "Гүйлгээний тайлбар");
            ucLogList.FieldLinkSetColumnCaption(11, "Тэмдэглэл");

            ucLogList.FieldLinkSetColumnCaption(12, "Үр дүн");

            ucLogList.FieldLinkSetColumnCaption(13, "Үр дүнгийн тайлбар");
            ucLogList.FieldLinkSetColumnCaption(14, "KEY1");
            ucLogList.FieldLinkSetColumnCaption(15, "KEY2");
            ucLogList.FieldLinkSetColumnCaption(16, "KEY3");
            ucLogList.FieldLinkSetColumnCaption(17, "KEY4");
            ucLogList.FieldLinkSetColumnCaption(18, "KEY5");
            ucLogList.FieldLinkSetColumnCaption(19, "KEY6");
            ucLogList.FieldLinkSetColumnCaption(20, "KEY7");
            ucLogList.FieldLinkSetColumnCaption(21, "KEY8");
            ucLogList.FieldLinkSetColumnCaption(22, "KEY9");
            ucLogList.FieldLinkSetColumnCaption(23, "KEY10");




            FormUtility.RestoreStateGrid(appname, formname, ref ucLogList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucLogList.ucParameterPanel1.vGridControl1, ref ucLogList.groupControl1);

        }
        #endregion
        #region [ EventSelected ]
        private void ucLogList_EventSelected(DataRow selectedrow)
        {
            try
            {
                if (ucLogList.DataSource != null)
                {


                    DataRow DR = ucLogList.gridView1.GetFocusedDataRow();
                    DataView DV = (DataView)ucLogList.gridView1.DataSource;
                    DataTable DT = DV.ToTable();
                    if (DR != null && DV != null)
                    {
                        object[] obj = new object[3];
                        obj[0] = _core;
                        obj[1] = Static.ToLong(DR["LogID"]);
                        obj[2] = DR;
                        EServ.Shared.Static.Invoke("InfoPos.Admin.dll", "InfoPos.Admin.Main", "CallLogDetail", obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }


        }
        #endregion
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucLogList.DataSource != null)
                {


                    DataRow DR = ucLogList.gridView1.GetFocusedDataRow();
                    DataView DV = (DataView)ucLogList.gridView1.DataSource;
                    DataTable DT = DV.ToTable();
                    if (DR != null && DV != null)
                    {
                        object[] obj = new object[3];
                        obj[0] = _core;
                        obj[1] = Static.ToLong(DR["LogID"]);
                        obj[2] = DR;
                        EServ.Shared.Static.Invoke("InfoPos.Admin.dll", "InfoPos.Admin.Main", "CallLogDetail", obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        void Function()
        {
            if (_core.Resource != null)
            {
                simpleButton1.Image = _core.Resource.GetImage("navigate_refresh");
            }
        }
        private void LogList_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucLogList.OnEventFindPaging(0);
        }
        private void LogList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucLogList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucLogList.ucParameterPanel1.vGridControl1, ref ucLogList.groupControl1);
        }
        private void ucLogList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 110200, 110200, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucLogList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
        private void LogList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucLogList.OnEventFindPaging(0);

            }
        }
    }
}
