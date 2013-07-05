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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace InfoPos.Reports
{
    public partial class DynamicReport : Form
    {   
        private Core.Core _core;
        string appname = "", formname = "";
        Form FormName = null;
     
       
   //   private System.Windows.Forms.DataGrid gridview;

        public event RowObjectCustomDrawEventHandler CustomDrawGroupRow; 
        public DynamicReport(Core.Core core)
        {
            
            InitializeComponent();
            _core = core;
            Init();
            //ucDynamicReport.Resource = _core.Resource;
            DynamicReport.ActiveForm.AcceptButton = ucDynamicReport.btnFind;
            //ucDynamicReport.gridView1.OptionsView.AllowHtmlDrawHeaders = true;
          
        }
  
        #region [ Init ]
        private void Init()
        {
            ucDynamicReport.EventDataChanged += new ucGridPanel.delegateEventDataChanged(ucDynamicReport_EventDataChanged);
            ucDynamicReport.EventSelected += new ucGridPanel.delegateEventSelected(ucDynamicReport_EventSelected);
           ucDynamicReport.EventFindPaging+=new ucGridPanel.delegateEventFindPaging(ucDynamicReport_EventFindPaging);
           // ucCallOperatorList.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
       ucDynamicReport.gridView1.CustomDrawGroupRow+=new RowObjectCustomDrawEventHandler(gridView1_CustomDrawGroupRow);
           //gridView1_CustomDrawGroupRow

           ucDynamicReport.gridView1.Appearance.Row.BackColor = Color.NavajoWhite;
           ucDynamicReport.gridView1.Appearance.Row.BackColor2 = Color.NavajoWhite;
            ucDynamicReport.FieldFindAdd("TRANCODE", "Гүйлгээ код", typeof(int), "");
            ucDynamicReport.FieldFindAdd("NAME", "Тайлангийн нэр", typeof(string), "");
            
            ucDynamicReport.FieldFindRefresh();
            ucDynamicReport.gridView1.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу.";
        
            ucDynamicReport.VisibleFind = true;
        }
        #endregion
        #region [ EventFind ]
      
     
        #endregion
        #region [ EventSelected ]
        void ucDynamicReport_EventSelected(DataRow selectedrow)
        {
            if (!ucDynamicReport.Browsable)
            {
                object[] obj = new object[3];
                obj[0] = _core;
                obj[1] = Static.ToLong(selectedrow["TRANCODE"]);
                obj[2] = Static.ToStr(selectedrow["NAME"]);
                EServ.Shared.Static.Invoke("HeavenPro.Reports.dll", "HeavenPro.Reports.Main", "CallBacTxn", obj);
            }
            else
            {
                if (selectedrow != null)
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region [ EventDatachanged ]
        void ucDynamicReport_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref ucDynamicReport.gridView1);

            ucDynamicReport.FieldLinkSetColumnCaption(0, "Тайлангийн бүлгийн дугаар");
            ucDynamicReport.FieldLinkSetColumnCaption(1, "Тайлангийн бүлгийн нэр");
            ucDynamicReport.FieldLinkSetColumnCaption(2, "Гүйлгээний код");
            ucDynamicReport.FieldLinkSetColumnCaption(3, "Тайлангийн нэр");
      

          

           
        }
        #endregion


        void gridView1_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
         
           
            //DevExpress.XtraGrid.Views.Grid.GridView view;
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo ee;
            //DevExpress.Utils.Drawing.OpenCloseButtonInfoArgs ocb;
            //DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            
            //view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //ee = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            //ocb = new DevExpress.Utils.Drawing.OpenCloseButtonInfoArgs(e.Cache, ee.ButtonBounds, 
            //    ee.GroupExpanded, ee.AppearanceGroupButton, DevExpress.Utils.Drawing.ObjectState.Normal);

            //if(!ee.ButtonBounds.IsEmpty) {
            //    painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            //    painter.ElementsPainter.OpenCloseButton.DrawObject(ocb);
            //}

        //    e.Handled = true;

            //GridView view = sender as GridView;

            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info =
            //    e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;

            //int handle = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);

            //if (info.Column.FieldName == "NAME")
            //{
            //    StringBuilder oGroupText = new StringBuilder();
            //    oGroupText.Append(view.GetRowCellValue(handle, "NAME").ToString());
            //    oGroupText.Append(" - ");
            //    oGroupText.Append(view.GetRowCellValue(handle, "trancode").ToString());

            //    info.GroupText = oGroupText.ToString();
            //}
            //ucDynamicReport.gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            //GridView view = sender as GridView;
            //GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            //if (info.Column.FieldName == "NAME")
            //{
            //    string quantity = Static.ToStr(view.GetGroupRowValue(e.RowHandle, info.Column));
            //    //  string colorName = getColorName(quantity);
            //    //info.GroupText = info.Column.Caption + ": <color=" + colorName + ">" + info.GroupValueText + "</color> ";
            //    //info.GroupText += "<color=LightSteelBlue>" + view.GetGroupSummaryText(e.RowHandle) + "</color> ";
            //}
        }
        
        string getColorName(int value)
        {
            if (value < 20) return "MediumOrchid";
            if (value >= 80) return "OrangeRed";
            return "Blue";
        }

        private void DynamicReport_Load(object sender, EventArgs e)
        {
           
            ucDynamicReport.Visible = true;

            appname = _core.ApplicationName;
            formname = "Reports." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            ucDynamicReport.OnEventFindPaging(0);
    //    new DevExpress.XtraGrid.Design.XViewsPrinting(ucDynamicReport.gridControl1);
            ucDynamicReport.gridView1.Columns[1].Group();
            ucDynamicReport.gridView1.CustomDrawGroupRow += new RowObjectCustomDrawEventHandler(gridView1_CustomDrawGroupRow);
        }
         /// </summary>
        

        
        private void DynamicReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {

                this.Close();
            }
        }

        private void ucDynamicReport_Load(object sender, EventArgs e)
        {
            //ucDynamicReport.OnEventFindPaging(0);
            //ucDynamicReport.gridView1.IsMultiSelect.ToString();
            //ucDynamicReport.gridView1.OptionsSelection.MultiSelect = true;
        }

        private void ucDynamicReport_EventFindPaging(object[] values, int pageindex, int pagerows, ref int pagecount)
        {
            object[] obj = new object[3];
            try
            {
                obj[0] = Static.ToInt(_core.RemoteObject.User.UserNo);
                obj[1] = Static.ToDate(_core.TxnDate.Date);

                obj[2] = values;
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 218, 240000, 240000, pageindex, pagerows, obj);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultDesc);
                    return;
                }
                pagecount = r.ResultPageCount; // Серверээс энэ утгаг заавал буцааж авч, энэ Ref параметрт олгоно !!! 

                ucDynamicReport.DataSource = r.Data.Tables[0]; // Хуудас бүхий тэйбэл буцаж ирнэ. 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DynamicReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref ucDynamicReport.gridView1);
        }
    }
}
