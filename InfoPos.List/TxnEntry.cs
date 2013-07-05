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
    public partial class TxnEntry : Form
    {
        #region [ Constructor Function ]
        Core.Core _core;
        string appname = "", formname = "";
        Form FormName = null;
        public TxnEntry(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucTxnEntry.Resource = _core.Resource;
           //TxnEntry.ActiveForm.AcceptButton = ucTxnEntry.btnFind;
        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            ucTxnEntry.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucTxnEntry_EventDataChanged);
        
            ucTxnEntry.EventSelected += new ucGridPanel.delegateEventSelected(ucTxnEntry_EventSelected);
            ucTxnEntry.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucTxnEntry_EventFindPaging);
            ucTxnEntry.FindItemAdd("TranCode", "Гүйлгээний код", "", DynamicParameterType.Decimal, false, "d", "");
            ucTxnEntry.FindItemAdd("EntryCode", "Гүйлгээний оролтын код", "", DynamicParameterType.Decimal, false, "d", "");
            ucTxnEntry.FindItemAdd("OrderNo", "Жагсаалтын эрэмбэ", "", DynamicParameterType.Decimal, false, "d", "");

            ucTxnEntry.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucTxnEntry.FieldFindRefresh();
            ucTxnEntry.VisibleFind = true;
        }
        #endregion
        #region [ EventSelected ]
        void ucTxnEntry_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucTxnEntry.Browsable)
                {
                    object[] obj = new object[2];
                    obj[0] = _core;
                    obj[1] = Static.ToLong(selectedrow["TranCode"]);
                    //EServ.Shared.Static.Invoke("InfoPos.FA.dll", "InfoPos.FA.Main", "CallEmployee", obj);
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
        void ucTxnEntry_EventDataChanged()
        {
          
            ucTxnEntry.FieldLinkSetColumnCaption(0, "Гүйлгээний код");
            ucTxnEntry.FieldLinkSetColumnCaption(1, "Гүйлгээний оролтын код");
            ucTxnEntry.FieldLinkSetColumnCaption(2, "Жагсаалтын эрэмбэ");
            FormUtility.RestoreStateGrid(appname, formname, ref ucTxnEntry.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucTxnEntry.ucParameterPanel1.vGridControl1, ref ucTxnEntry.groupControl1);
        }
        #endregion

        private void TxnEntry_Load(object sender, EventArgs e)
        {
           
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucTxnEntry.OnEventFindPaging(0);

        }

        private void TxnEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucTxnEntry.OnEventFindPaging(0);

            }
                
            
        }

     

        private void ucTxnEntry_Load(object sender, EventArgs e)
        {

        }

        private void TxnEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucTxnEntry.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucTxnEntry.ucParameterPanel1.vGridControl1, ref ucTxnEntry.groupControl1);
        }

        private void ucTxnEntry_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140176, 140176, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucTxnEntry.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }
    }
}
