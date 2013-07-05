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
    public partial class DocTemplate : Form
    {
        private Core.Core _core;
        int PrivNo = 150000;
        string appname = "", formname = "";
        Form FormName = null;

        public DocTemplate(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucDocTemplate.Resource = _core.Resource;
          //  DocTemplate.ActiveForm.AcceptButton = ucDocTemplate.btnFind;

        }
        #region [ Init ]
        private void Init()
        {
            ucDocTemplate.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucDocTemplate_EventDataChanged);
            ucDocTemplate.EventSelected += new ucGridPanel.delegateEventSelected(ucDocTemplate_EventSelected);
            ucDocTemplate.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucDocTemplate_EventFindPaging);

            ucDocTemplate.FindItemAdd("ID", "Документын дугаар", "", DynamicParameterType.Decimal, false, "d", "");
            ucDocTemplate.FieldFindAdd("Name", "Документын тайлбар нэр", typeof(string), "");
            ucDocTemplate.FieldFindAdd("Name2", "Документын тайлбар нэр 2 дах хэлээр", typeof(string), "");
            ucDocTemplate.FieldFindAdd("DOCFILENAME", "Сервер дээр байгаа файлын нэр", typeof(string), "");
            ucDocTemplate.FieldFindAdd("EXPORTTYPE", "Экспорт хийх файлын формат", typeof(ArrayList), "");

            ucDocTemplate.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucDocTemplate.FieldFindRefresh();
            InitDictionary();
            ucDocTemplate.VisibleFind = true;
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
                ucDocTemplate.FindItemSetList("EXPORTTYPE", "XLS", "EXCEL");
                ucDocTemplate.FindItemSetList("EXPORTTYPE", "DOC", "WORD DOCUMENT");
                ucDocTemplate.FindItemSetList("EXPORTTYPE", "PDF", "PDF");

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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 150000, 150000, values);

                if (res.ResultNo == 0)
                {
                    ucDocTemplate.DataSource = res.Data.Tables[0];
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
        void ucDocTemplate_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucDocTemplate.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["ID"]);

                    EServ.Shared.Static.Invoke("InfoPos.List.dll", "InfoPos.List.Main", "CallDocTemplate", obj);
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
        void ucDocTemplate_EventDataChanged()
        {
           
            ucDocTemplate.FieldLinkSetColumnCaption(0, "Документын дугаар");
            //ucDocTemplate.gridView1.Columns[0].Width = 100;
            ucDocTemplate.FieldLinkSetColumnCaption(1, "Документын тайлбар нэр");
            //ucDocTemplate.gridView1.Columns[1].Width = 180;
            ucDocTemplate.FieldLinkSetColumnCaption(2, "Документын тайлбар нэр 2 дах хэлээр");
            //ucDocTemplate.gridView1.Columns[2].Width = 150;
            ucDocTemplate.FieldLinkSetColumnCaption(3, "Сервер дээр байгаа файлын нэр");
           // ucDocTemplate.gridView1.Columns[3].Width = 100;
            ucDocTemplate.FieldLinkSetColumnCaption(4, "Экспорт хийх файлын формат");
         //   ucDocTemplate.gridView1.Columns[4].Width = 100;
            FormUtility.RestoreStateGrid(appname, formname, ref ucDocTemplate.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucDocTemplate.ucParameterPanel1.vGridControl1, ref ucDocTemplate.groupControl1);
        }
        #endregion

        private void DocTemplate_Load(object sender, EventArgs e)
        {
           
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucDocTemplate.OnEventFindPaging(0);
        }

        private void DocTemplate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {

                ucDocTemplate.OnEventFindPaging(0);

            }
        }

        private void DocTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucDocTemplate.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucDocTemplate.ucParameterPanel1.vGridControl1, ref ucDocTemplate.groupControl1);
        }

        private void ucDocTemplate_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 150000, 150000, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucDocTemplate.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
