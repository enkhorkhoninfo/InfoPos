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
using System.Collections;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Design;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Data;
using DevExpress.XtraGrid;
using ISM.CUser;

namespace HeavenPro.Admin
{
    public partial class LogDetail : Form
    {

        #region [ Variables ]
        Core.Core _core;
        long _LogID=33778948200001;
        //private long _TypeCode = 0;
        //bool LoadList;
        //bool LoadLogDetail;
        //bool LoadNote;
        //bool LoadNote1;
        DataRow DR;
        string appname = "", formname = "";
        Form FormName = null;
        //int TablePrivSelect = 110201;
    //    int TablePrivUpdate = 120009;
        //string TableNamePrefix = "LogDetail";
        #endregion
       
        #region [Constructor Function]
        public LogDetail(Core.Core core, long LogID, object[] obj)
        {
            try
            {
                InitializeComponent();
                _core = core;
                _LogID = LogID;
                 DR = (DataRow)obj[0];
                 //Init(1);
                 //Init(2);
            }
            catch(Exception ex)
            {
                obj = null;
                MessageBox.Show("Обьектд хоосон утга дамжлаа" + ex);
            }
        }
        #endregion[]

        #region [ Switch_case ]7
        //public void Init(int _Type)
        //{
        //    switch (_Type)
        //    {
        //        case 1:
        //            Init_LogRef();             //Үндсэн мэдээлэл  
        //            break;
        //        case 2:
        //            Init_LogInfoRef();             //Хаягийн жагсаалт
        //            break;
               
        //    }
        //}
        //private void Init_LogRef()
        //{
        //    if (!LoadNote)
        //    {
        //        Init_Log(_LogID);
        //    }
        //}
        //private void Init_LogInfoRef()
        //{
        //    if (!LoadNote1)
        //    {
        //        //LoadLogDetail = true;
        //        _LogID = Static.ToLong(DR["LogID"]);
        //        Init_LogInfo(_LogID);
        //    }
        //}
        #endregion
        #region[init]
        public void Init_Log(long LogID)
        {
            try
            {
                DataTable dd = new DataTable();
                dd.Columns.Add("Логын ID", Type.GetType("System.String"));
                dd.Columns.Add("Утга", Type.GetType("System.String"));
               
                
                ISM.Template.FormUtility.SetFormatGrid(ref gridView1, false);
                gridView1.GroupPanelText = "Бүлэглэх баганаа оруулна уу";


                dd.Rows.Add("Логын ID", Static.ToLong(DR["LogID"]));
                dd.Rows.Add("Гүйлгээний огноо", Static.ToDate(DR["TxnDate"]));
                dd.Rows.Add("Цаг минут", Static.ToDateTime(DR["PostDate"]));
                dd.Rows.Add("Хэрэглэгчийн дугаар", Static.ToLong(DR["UserNo"]));
                dd.Rows.Add("Салбарын дугаар", Static.ToInt(DR["BranchNo"]));
                dd.Rows.Add("Хянасан хэрэглэгчийн дугаар", Static.ToInt(DR["SUPERVISORNO"]));
                dd.Rows.Add("Хянасан хэрэглэгчийн нэр", Static.ToInt(DR["USERNOName"]));
                dd.Rows.Add("Гүйлгээний код", Static.ToInt(DR["TxnCode"]));
                dd.Rows.Add("Гүйлгээний тайлбар", Static.ToStr(DR["Description"]));
                dd.Rows.Add("Тэмдэглэл", Static.ToStr(DR["Note"]));
                dd.Rows.Add("Үр дүн", Static.ToStr(DR["RESULTNO"]));
                dd.Rows.Add("Үр дүнгийн тайлбар", Static.ToStr(DR["RESULTDESC"]));
                dd.Rows.Add("Key1", Static.ToStr(DR["Key1"]));
                dd.Rows.Add("Key2", Static.ToStr(DR["Key2"]));
                dd.Rows.Add("Key3", Static.ToStr(DR["Key3"]));
                dd.Rows.Add("Key4", Static.ToStr(DR["Key4"]));
                dd.Rows.Add("Key5", Static.ToStr(DR["Key5"]));
                dd.Rows.Add("Key6", Static.ToStr(DR["Key6"]));
                dd.Rows.Add("Key7", Static.ToStr(DR["Key7"]));
                dd.Rows.Add("Key8", Static.ToStr(DR["Key8"]));
                dd.Rows.Add("Key9", Static.ToStr(DR["Key9"]));
                dd.Rows.Add("Key10", Static.ToStr(DR["Key10"]));
              
                gridControl1.DataSource = null;
                gridControl1.DataSource = dd;
                //LoadList = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Үндсэн мэдээллийг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        public void Init_LogInfo(long pLogID)
        {
            try
            {
                //LoadNote1 = true;
                object[] obj = new object[1];
                obj[0] = Static.ToLong(pLogID);
                Result res = new Result();
                res = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110201, 110201, new object[] { pLogID });
                //if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                //if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                //if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                //if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

                gridControl2.DataSource = null;


                // gridview2.DataSource = null;
                gridControl2.DataSource = res.Data.Tables[0];
                ISM.Template.FormUtility.SetFormatGrid(ref gridView2, false);
                gridView2.GroupPanelText = "Энд бүлэглэх баганаа оруулна уу";


                gridView2.Columns[0].Caption = "Логын ID";
                gridView2.Columns[1].Caption = "Table-ийн нэр";
                gridView2.Columns[2].Caption = "Талбарын нэр";
                gridView2.Columns[3].Caption = "Хуучин утга";
                gridView2.Columns[4].Caption = "Шинэ утга";

                // LoadLogDetail = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Логийн мэдээллийг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        void Function()
        {
            if (_core.resource != null)
            {
                btnClose.Image = _core.resource.GetImage("navigate_cancel");
            }
        }
        #endregion[]

        #region[btns]
        private void LogDetail_Load(object sender, EventArgs e)
        {
            Function();
            appname = _core.ApplicationName;
            formname = "Admin." + this.Name;
            
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gridView1);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gridView2);           

            FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
        }

        private void LogDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Admin." + this.Name;
            FormName = this;
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gridView1);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gridView2);
            
        }

        private void LogDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion[]
    }
}