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
    public partial class TxnList : Form
    {
        Core.Core _core;
        string appname = "", formname = "";
        Form FormName = null;
        public TxnList(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucTxn.Resource = _core.Resource;
        //   TxnList.ActiveForm.AcceptButton = ucTxn.btnFind;
        }
        #region [ Init ]
        private void Init()
        {
            ucTxn.EventDataChanged +=new ucGridPanel.delegateEventDataChanged(ucTxn_EventDataChanged);
            ucTxn.EventSelected +=new ucGridPanel.delegateEventSelected(ucTxn_EventSelected);
            ucTxn.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucTxn_EventFindPaging);

            ucTxn.FindItemAdd("TranCode", "Гүйлгээний код", "", DynamicParameterType.Decimal, false, "d", "");
            ucTxn.FieldFindAdd("Name", "Гүйлгээний нэр", typeof(string), "");
            ucTxn.FieldFindAdd("Name2", "Гүйлгээний нэр 2", typeof(string), "");

            ucTxn.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucTxn.FieldFindRefresh();
            ucTxn.VisibleFind = true;
        }
        #endregion
        
        #region [ LoadData ]
        void LoadData(object[] values)
        {
            Result res = new Result();

            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140161, 140161, values);

                if (res.ResultNo == 0)
                {
                    ucTxn.DataSource = res.Data.Tables[0];
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
        void ucTxn_EventSelected(DataRow selectedrow)
        {
            if (selectedrow != null)
            {
                if (!ucTxn.Browsable)
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
        void ucTxn_EventDataChanged()
        {
          
            ucTxn.FieldLinkSetColumnCaption(0, "Гүйлгээний код");
            ucTxn.FieldLinkSetColumnCaption(1, "Гүйлгээний нэр");
           // ucTxn.gridView1.Columns[1].Width = 400;
            ucTxn.FieldLinkSetColumnCaption(2, "Гүйлгээний нэр 2");
         //   ucTxn.gridView1.Columns[2].Width = 200;
            FormUtility.RestoreStateGrid(appname, formname, ref ucTxn.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucTxn.ucParameterPanel1.vGridControl1, ref ucTxn.groupControl1);
        }
        #endregion

        private void TxnList_Load(object sender, EventArgs e)
        {
       
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucTxn.OnEventFindPaging(0);
        }

        private void TxnList_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {

                ucTxn.OnEventFindPaging(0);

            }
                
     
        }

        private void TxnList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucTxn.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucTxn.ucParameterPanel1.vGridControl1, ref ucTxn.groupControl1);
        }

        private void ucTxn_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 140161, 140161, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucTxn.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }

        private void ucTxn_Load(object sender, EventArgs e)
        {

        }
    }
}
