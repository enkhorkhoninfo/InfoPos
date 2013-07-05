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
    public partial class TxnYNote : Form
    {
        private Core.Core _core;
        string appname = "", formname = "";
        Form FormName = null;

      
        public TxnYNote(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucTxnYNoteList.Resource = _core.Resource;
          ///  TxnYNote.ActiveForm.AcceptButton = ucTxnYNoteList.btnFind;
        }
        #region [ Init ]
        private void Init()
        {
            ucTxnYNoteList.EventSelected += new ucGridPanel.delegateEventSelected(ucTxnYNoteList_EventSelected);
            ucTxnYNoteList.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucTxnYNoteList_EventDataChanged);
        ucTxnYNoteList.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucTxnYNoteList_EventFindPaging);
          
            ucTxnYNoteList.FieldFindAdd("JrNo", "Журналын дугаар", typeof(long), "");
            ucTxnYNoteList.FieldFindAdd("EntryNo", "Дэд журналын дугаар", typeof(long), "");
            ucTxnYNoteList.FieldFindAdd("AccountNo", "Дансны дугаар", typeof(long), "");
            ucTxnYNoteList.FieldFindAdd("BranchNo", "Салбарын дугаар", typeof(int), "");
            ucTxnYNoteList.FieldFindAdd("CurrCode", "Валютын код", typeof(string), "");
            ucTxnYNoteList.FieldFindAdd("Amount", "Дүн", typeof(decimal), "");
            ucTxnYNoteList.FieldFindAdd("Description", "Гүйлгээний утга", typeof(string), "");
            ucTxnYNoteList.FieldFindAdd("TxnDate", "Гүйлгээний огноо", typeof(DateTime), "");
            ucTxnYNoteList.FieldFindAdd("PostDate", "Гүйлгээний огноо, цаг", typeof(DateTime), "");
            ucTxnYNoteList.FieldFindAdd("UserNo", "Хэрэглэгчийн дугаар", typeof(int), "");
            ucTxnYNoteList.FieldFindAdd("Corr", "Буцаагдсан эсэх", typeof(int), "");
            ucTxnYNoteList.FieldFindAdd("TxnCode", "Гүйлгээний код", typeof(int), "");
            ucTxnYNoteList.FieldFindAdd("Rate", "Ханш", typeof(decimal), "");
            ucTxnYNoteList.FieldFindAdd("Balance", "Гүйлгээний дараах үлдэгдэл", typeof(decimal), "");

            ucTxnYNoteList.FieldFindRefresh();
            ucTxnYNoteList.VisibleFind = true;
        }
        #endregion
      
        #region [ LoadData ]
        void LoadData(object[] values)
        {
            Result res = new Result();

            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 000004, 000004, values);

                if (res.ResultNo == 0)
                {
                    ucTxnYNoteList.DataSource = res.Data.Tables[0];
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
        #region [ EventDataChanged ]
        void ucTxnYNoteList_EventDataChanged()
        {
           
            ucTxnYNoteList.FieldLinkSetColumnCaption(0, "Журналын дугаар");
            //ucTxnYNoteList.gridView1.Columns[0].Width = 100;
            ucTxnYNoteList.FieldLinkSetColumnCaption(1, "Дэд журналын дугаар");
            //ucTxnYNoteList.gridView1.Columns[1].Width = 100;
            ucTxnYNoteList.FieldLinkSetColumnCaption(2, "Дансны дугаар");
           // ucTxnYNoteList.gridView1.Columns[2].Width = 100;
            ucTxnYNoteList.FieldLinkSetColumnCaption(3, "Салбарын дугаар");
            //ucTxnYNoteList.gridView1.Columns[3].Width = 100;
            ucTxnYNoteList.FieldLinkSetColumnCaption(4, "Валютын код");
            ucTxnYNoteList.FieldLinkSetColumnCaption(5, "Дүн");
            //ucTxnYNoteList.gridView1.Columns[5].Width = 100;
            ucTxnYNoteList.FieldLinkSetColumnCaption(6, "Гүйлгээний утга");
            //ucTxnYNoteList.gridView1.Columns[6].Width = 100;
            ucTxnYNoteList.FieldLinkSetColumnCaption(7, "Гүйлгээний огноо");
            ucTxnYNoteList.FieldLinkSetColumnCaption(8, "Гүйлгээний огноо, цаг");
            ucTxnYNoteList.FieldLinkSetColumnCaption(9, "Хэрэглэгчийн дугаар");
            //ucTxnYNoteList.gridView1.Columns[9].Width = 100;
            ucTxnYNoteList.FieldLinkSetColumnCaption(10, "Буцаагдсан эсэх");
            ucTxnYNoteList.FieldLinkSetColumnCaption(11, "Гүйлгээний код");
          //  ucTxnYNoteList.gridView1.Columns[11].Width = 100;
            ucTxnYNoteList.FieldLinkSetColumnCaption(12, "Ханш");
            ucTxnYNoteList.FieldLinkSetColumnCaption(13, "Гүйлгээний дараах үлдэгдэл");
        //    ucTxnYNoteList.gridView1.Columns[13].Width = 100;
            FormUtility.RestoreStateGrid(appname, formname, ref ucTxnYNoteList.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucTxnYNoteList.ucParameterPanel1.vGridControl1, ref ucTxnYNoteList.groupControl1);
        }
        #endregion 
        #region [ EventSelected ]
        void ucTxnYNoteList_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucTxnYNoteList.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["dans"]);
                    EServ.Shared.Static.Invoke("InfoPos.List.dll", "InfoPos.List.Main", "CallTxnYNoteList", obj);
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }
        #endregion

        private void TxnYNote_Load(object sender, EventArgs e)
        {
          
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucTxnYNoteList.OnEventFindPaging(0);
        }

        private void TxnYNote_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucTxnYNoteList.OnEventFindPaging(0);

            }
                
        }

        private void ucTxnYNoteList_Load(object sender, EventArgs e)
        {

        }

        private void TxnYNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucTxnYNoteList.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucTxnYNoteList.ucParameterPanel1.vGridControl1, ref ucTxnYNoteList.groupControl1);
        }

        private void ucTxnYNoteList_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 000004, 000004, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucTxnYNoteList.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
