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
    public partial class ReportList : Form
    {
        private Core.Core _core;
        int PrivNo = 000007;
        string appname = "", formname = "";
        Form FormName = null;
        public ReportList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucReportList.Resource = _core.Resource;
           ReportList.ActiveForm.AcceptButton = ucReportList.btnFind;
        }
        #region [ Init ]
        private void Init()
        {
            ucReportList.EventSelected += new ucGridPanel.delegateEventSelected(ucReportList_EventSelected);
            ucReportList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucReportList_EventDataChanged);
         ucReportList.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucReportList_EventFindPaging);

            ucReportList.FindItemAdd("ID", "Документийн дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucReportList.FieldFindAdd("Name", "Документийн нэр", typeof(string), "");
            ucReportList.FieldFindAdd("Name2 ", "Документийн нэр 2", typeof(string), "");
            ucReportList.FieldFindAdd("DOCFileName", "Сервер дээр байгаа файлын нэр", typeof(string), "");
            ucReportList.FieldFindAdd("ExportType", "Экспорт хийх файлын формат", typeof(ArrayList), "");

            ucReportList.FieldFindRefresh();
            InitDictionary();
            ucReportList.VisibleFind = true;
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
                ucReportList.FindItemSetList("ExportType", "xls", "Excel [XLS]");
                ucReportList.FindItemSetList("ExportType", "doc", "Document [DOC]");
                ucReportList.FindItemSetList("ExportType", "pdf", "Acrobat Reader [PDF]");

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
        void ucReportList_EventDataChanged()
        {
          

            ucReportList.FieldLinkSetColumnCaption(0, "Тайлангийн дугаар");
          //  ucReportList.gridView1.Columns[0].Width = 100;
            
            ucReportList.FieldLinkSetColumnCaption(1, "Тайлангийн төрөл");
            ucReportList.FieldLinkSetColumnCaption(2, "");
            //ucReportList.gridView1.Columns[2].Width = 180;Тайлангийн нэр
            ucReportList.FieldLinkSetColumnCaption(3, "Тайлангийн нэр 2");
            //ucReportList.gridView1.Columns[3].Width = 180;
            ucReportList.FieldLinkSetColumnCaption(4, "Тайлангийн жагсаалтанд харагдах эрэмбэ");
            ucReportList.FieldLinkSetColumnCaption(5, "SQL");
            ucReportList.FieldLinkSetColumnCaption(6, "Тайлангийн файлын нэр");
            FormUtility.RestoreStateGrid(appname, formname, ref ucReportList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucReportList.ucParameterPanel1.vGridControl1, ref ucReportList.groupControl1);
           
        }
        #endregion
        #region [ EventSelected ]
        void ucReportList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucReportList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["ReportNo"]);

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
        #region [ LoadData ]
        void LoadData(object[] values)
        {
            Result res = new Result();

            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 000007, 000007, values);

                if (res.ResultNo == 0)
                {
                    ucReportList.DataSource = res.Data.Tables[0];
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

        private void ReportList_Load(object sender, EventArgs e)
        {
         
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucReportList.OnEventFindPaging(0);
        }

        private void ReportList_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            
        }

        private void ReportList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucReportList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucReportList.ucParameterPanel1.vGridControl1, ref ucReportList.groupControl1);
        }

        private void ucReportList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 000007, 000007, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucReportList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 

        }
    }
}
