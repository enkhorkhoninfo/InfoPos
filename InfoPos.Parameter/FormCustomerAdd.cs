using System;
using System.Collections;
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
    public partial class FormCustomerAdd : Form
    {
        #region[ Variables ]
        InfoPos.Core.Core _core;
        string TableName = "CUSTOMERADD";
        int TablePrivSelect = 120056, TablePrivUpdate = 120059, PrivNo = 120056;
        #endregion
        public FormCustomerAdd(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            ucDynamicParameter1.Remote = _core.RemoteObject;
            ucDynamicParameter1.Resource = _core.Resource;
        }
        private void FormContractAdd_Load(object sender, EventArgs e)
        {
            this.Show();
            Init();
        }
        void Init()
        {
            try
            {
                #region [ Combo ]
                Result res = new Result();
                ArrayList Tables = new ArrayList();
                DataTable DT = null;
                string msg = "";

                DictUtility.PrivNo = PrivNo;

                string[] names = new string[] { "CUSTOMERTYPECODE" };
                res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д CUSTOMERTYPECODE оруулаагүй байна-" + res.ResultDesc;
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboCustomerType, DT, "TYPECODE", "NAME");
                }

                if (msg != "")
                    MessageBox.Show(msg);
                #endregion
                #region [ Events ]
                ucDynamicParameter1.EventInsert += new ISM.Template.UserControls.ucDynamicParameter.delegateEventInsert(ucDynamicParameter1_EventInsert);
                ucDynamicParameter1.EventUpdate += new ISM.Template.UserControls.ucDynamicParameter.delegateEventInsert(ucDynamicParameter1_EventUpdate);
                ucDynamicParameter1.EventDelete += new ISM.Template.UserControls.ucDynamicParameter.delegateEventDelete(ucDynamicParameter1_EventDelete);
                #endregion
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                MessageBox.Show("Амжилттай засварлагдлаа .");
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
                MessageBox.Show("Амжилттай нэмэгдлээ.");
            }
        }
        private void cboCustomerType_EditValueChanged(object sender, EventArgs e)
        {
            ucDynamicParameter1.TablePrivSelect = TablePrivSelect;
            ucDynamicParameter1.TablePrivUpdate = TablePrivUpdate;
            ucDynamicParameter1.TableTypeId = (ulong)Static.ToLong(cboCustomerType.EditValue);
            ucDynamicParameter1.TableName = TableName;
            ucDynamicParameter1.GridRead();
        }

        private void FormCustomerAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FormCustomerAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }
    }
}
