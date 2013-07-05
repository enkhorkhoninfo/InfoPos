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
    public partial class CurRateHistory : Form
    {
        private Core.Core _core;
        int PrivNo = 140200;
        string appname = "", formname = "";
        Form FormName = null;
        public CurRateHistory(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucCurRateHistory.Resource = _core.Resource;
        // CurRateHistory.ActiveForm.AcceptButton = ucCurRateHistory.btnFind;
        }

        #region [ Init ]
        private void Init()
        {
            ucCurRateHistory.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucBacAccountList_EventDataChanged);
            ucCurRateHistory.EventSelected += new ucGridPanel.delegateEventSelected(ucBacAccountList_EventSelected);
            ucCurRateHistory.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucCurRateHistory_EventFindPaging);

            ucCurRateHistory.FieldFindAdd("CURRENCY", "Валют", typeof(ArrayList), "");
            ucCurRateHistory.FieldFindAdd("CURDATE", "Огноо", typeof(DateTime), "");

            ucCurRateHistory.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";
            ucCurRateHistory.FieldFindRefresh();
            InitDictionary();
            ucCurRateHistory.VisibleFind = true;
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
                string[] names = new string[] { "CURRENCY" };
                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д валютын мэдээлэл оруулаагүй байна-" + res.ResultDesc;
                    res.ResultNo = 1;
                    res.ResultDesc = msg;
                }
                else
                {
                    ucCurRateHistory.FindItemSetList("CURRENCY", DT, "CURRENCY", "NAME");
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
        #endregion
        
        #region [ Өгөгдлийн ачааллалт ]
        void LoadData(object[] values)
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, PrivNo, PrivNo, values);

                if (res.ResultNo == 0)
                {
                    ucCurRateHistory.DataSource = res.Data.Tables[0];
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
        void ucBacAccountList_EventSelected(DataRow selectedrow)
        {
        }
        #endregion
        #region[ EventDataChanged ]
        void ucBacAccountList_EventDataChanged()
        {
           

               


               
            ucCurRateHistory.FieldLinkSetColumnCaption(0, "Валют");
        //    ucCurRateHistory.gridView1.Columns[0].Width = 50;
            ucCurRateHistory.FieldLinkSetColumnCaption(1, "Огноо");
            ucCurRateHistory.FieldLinkSetColumnCaption(2, "Албан ханш");
            ucCurRateHistory.FieldLinkSetColumnCaption(3, "Бэлэн авах ханш");
            ucCurRateHistory.FieldLinkSetColumnCaption(4, "Бэлэн зарах ханш");
            ucCurRateHistory.FieldLinkSetColumnCaption(5, "Бэлэн бус авах ханш");
            ucCurRateHistory.FieldLinkSetColumnCaption(6, "Бэлэн бус зарах ханш");
            ucCurRateHistory.FieldLinkSetColumnCaption(7, "Өмнөх өдрийн ханш (Хуучин ханш)");
            ucCurRateHistory.FieldLinkSetColumnCaption(8, "Эквивалент GL. ");
            ucCurRateHistory.FieldLinkSetColumnCaption(9, "Валютын код");
            ucCurRateHistory.FieldLinkSetColumnCaption(10, "Арилжааны ашиг GL");
            ucCurRateHistory.FieldLinkSetColumnCaption(11, "Арилжааны алдагдал GL");
            ucCurRateHistory.FieldLinkSetColumnCaption(12, "Ханшийн тэгшитгэлийн ашиг GL");
            ucCurRateHistory.FieldLinkSetColumnCaption(13, "Ханшийн тэгшитгэлийн алдагдал GL");
            ucCurRateHistory.FieldLinkSetColumnCaption(14, "Өмнөх өдрийн ханш (Хуучин ханш)");
            ucCurRateHistory.FieldLinkSetColumnCaption(15, "Жагсаалтын эрэмбэ");
            FormUtility.RestoreStateGrid(appname, formname, ref ucCurRateHistory.gridView1);
            FormUtility.RestoreStateVGrid(appname, formname, ref ucCurRateHistory.ucParameterPanel1.vGridControl1, ref ucCurRateHistory.groupControl1);
        }
        #endregion
        private void BacAccount_Load(object sender, EventArgs e)
        {
      
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucCurRateHistory.OnEventFindPaging(0);
        }

        private void ucCurRateHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape) {

                this.Close();

            
            }
        }

        private void CurRateHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();


            }
              if (e.KeyCode == Keys.Enter)
           {

               ucCurRateHistory.OnEventFindPaging(0);

           }
        }

        private void CurRateHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucCurRateHistory.gridView1);
            FormUtility.SaveStateVGrid(appname, formname, ref ucCurRateHistory.ucParameterPanel1.vGridControl1, ref ucCurRateHistory.groupControl1);
        }

        private void ucCurRateHistory_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, PrivNo, PrivNo, pageindex, pagerows, values);
            if (r.ResultNo != 0)
            {
                MessageBox.Show(r.ResultDesc);
                return;
            }
            pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

            ucCurRateHistory.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
        }

     
    }
}
