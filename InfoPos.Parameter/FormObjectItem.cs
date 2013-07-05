using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class FormObjectItem : Form
    {
        #region[ Variables ]
        Core.Core _core;
        string TableName = "OBJECTITEM";
        int TablePrivSelect = 140403;
        int TablePrivUpdate = 140404;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Form]
        public FormObjectItem(Core.Core core )
        {
            InitializeComponent();
            _core = core;
        }
        private void FormObjectItem_Load(object sender, EventArgs e)
        {
            try
            {
                appname = _core.ApplicationName;
                formname = "Parameter." + this.Name;
                FormName = this;
                FormUtility.RestoreStateForm(appname, ref FormName);
                ucDynamicParameter1.UseTypeList2 = true;
                ucDynamicParameter1.TablePrivSelect = TablePrivSelect;
                ucDynamicParameter1.TablePrivUpdate = TablePrivUpdate;
                ucDynamicParameter1.TableTypeId = 0;
                ucDynamicParameter1.Resource = _core.Resource;
                //ucDynamicParameter1.Remote = _core.moRemote;
                ucDynamicParameter1.TableName = TableName;
                ucDynamicParameter1.GridRead();
                
                ucDynamicParameter1.EventInsert += new ISM.Template.UserControls.ucDynamicParameter.delegateEventInsert(ucDynamicParameter1_EventInsert);
                ucDynamicParameter1.EventUpdate += new ISM.Template.UserControls.ucDynamicParameter.delegateEventInsert(ucDynamicParameter1_EventUpdate);
                ucDynamicParameter1.EventDelete += new ISM.Template.UserControls.ucDynamicParameter.delegateEventDelete(ucDynamicParameter1_EventDelete);
                ucDynamicParameter1.EventDataChanged += new ISM.Template.UserControls.ucDynamicParameter.delegateEventDataChanged(ucDynamicParameter1_EventDataChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Зүйлийн мэдээлэл унших явцад алдаа гарлаа:" + ex.Message);
            }
        }
        private void FormObjectItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref ucDynamicParameter1.gridView1);
        }
        private void FormObjectItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
        #region[Event]
        void ucDynamicParameter1_EventDelete(Result r, ref bool cancel)
        {
            if (r.ResultNo != 0)
            {
                MessageBox.Show(Static.ToStr(r.ResultNo) + " " + r.ResultDesc);
                cancel = true;
            }
             else
            {
                MessageBox.Show("Амжилттай устгагдлаа .");
            }
        }
        void ucDynamicParameter1_EventUpdate(Result r, ref bool cancel)
        {
            if (r.ResultNo != 0)
            {
                MessageBox.Show(Static.ToStr(r.ResultNo) + " " + r.ResultDesc);
                cancel = true;
            }
            else
            {
                MessageBox.Show("Амжилттай засварлалаа."); 
            }
        } 
        void ucDynamicParameter1_EventInsert(Result r, ref bool cancel)
        {
            if (r.ResultNo != 0)
            {
                if (r.ResultNo == -2147467259)
                    MessageBox.Show("Бичлэгийн дугаар давхардаж байна. {ID дугаар эсвэл Эрэмбийн дугаар}");
                else
                    MessageBox.Show(Static.ToStr(r.ResultNo) + " " + r.ResultDesc);
                cancel = true;
            }
            else
            {
                MessageBox.Show("Амжилттай нэмлээ .");
            }
        }
        void ucDynamicParameter1_EventDataChanged()
        {
            FormUtility.RestoreStateGrid(appname, formname, ref ucDynamicParameter1.gridView1);
        }
#endregion
        private void ucDynamicParameter1_Load(object sender, EventArgs e)
        {
            ucDynamicParameter1.UseTypeList2 = true;
        }
    }
}