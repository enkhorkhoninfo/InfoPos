using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ISM.Template;
using EServ.Shared;
using ISM.CUser;

namespace InfoPos.Parameter
{
    public partial class FormContractAdd : Form
    {
        #region[ Variables ]
        InfoPos.Core.Core _core;
        string TableName = "CONTRACTADD";
        int TablePrivSelect = 140198, TablePrivUpdate = 140199;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        public FormContractAdd(Core.Core core)
        {
            InitializeComponent();
            //_core = core;
        }
        private void FormContractAdd_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            Init();
        }
        void Init()
        {
            ucDynamicParameter1.TablePrivSelect = TablePrivSelect;
            ucDynamicParameter1.TablePrivUpdate = TablePrivUpdate;
            ucDynamicParameter1.TableTypeId = 0;
            ucDynamicParameter1.TableName = TableName;
            ucDynamicParameter1.GridRead();

            ucDynamicParameter1.EventInsert += new ISM.Template.UserControls.ucDynamicParameter.delegateEventInsert(ucDynamicParameter1_EventInsert);
            ucDynamicParameter1.EventUpdate += new ISM.Template.UserControls.ucDynamicParameter.delegateEventInsert(ucDynamicParameter1_EventUpdate);
            ucDynamicParameter1.EventDelete += new ISM.Template.UserControls.ucDynamicParameter.delegateEventDelete(ucDynamicParameter1_EventDelete);
            ucDynamicParameter1.EventDataChanged += new ISM.Template.UserControls.ucDynamicParameter.delegateEventDataChanged(ucDynamicParameter1_EventDataChanged);
        }
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
                MessageBox.Show("Амжилттай засварлалаа .");
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

        private void FormContractAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FormContractAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref ucDynamicParameter1.gridView1);
        }
    }
}
