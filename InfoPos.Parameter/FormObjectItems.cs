using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormObjectItems : Form
    {
        #region [ Variables ]
        Core.Core _core;
        int _objectitems = 0, _objectid = 0;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        public FormObjectItems(Core.Core core) : this(core, 0)
        {
            
        }
        public FormObjectItems(Core.Core core, int objectid)
        {
            InitializeComponent();
            _core = core;
            _objectid = objectid;
        }
        private void FormObjectItems_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            this.Show();
            ISM.Template.FormUtility.SetFormatGrid(ref gvwObjectItems, true);
            RefreshData(_objectid);
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("navigate_save");
            }
        }
        void RefreshData(int pobjectid)
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140196, 140196, new object[] { pobjectid, 2 });

                if (res.ResultNo == 0)
                {
                    if (res.AffectedRows != 0)
                    {
                        grdObjectItems.DataSource = res.Data.Tables[0];
                        SetObjectItemsData();
                    }
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
        void SetObjectItemsData()
        {
            try
            {
                //status, TYPEID, ITEMID, NAME , ISKEY, ISMANDATORY,ISCOMBOEDITABLE, VALUEDEFAULT, DESCRIPTION,ORDERNO
                gvwObjectItems.Columns[0].Caption = "Төлөв";
                gvwObjectItems.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                gvwObjectItems.Columns[1].Visible = false;
                gvwObjectItems.Columns[2].Caption = "Төрлийн дугаар";
                gvwObjectItems.Columns[2].OptionsColumn.AllowEdit = false;
                gvwObjectItems.Columns[3].Caption = "Нэр";
                gvwObjectItems.Columns[3].OptionsColumn.AllowEdit = false;
                gvwObjectItems.Columns[4].Caption = "Түлхүүр талбар эсэх";
                gvwObjectItems.Columns[4].ColumnEdit = CreateRepositoryCheckEdit();
                gvwObjectItems.Columns[5].Caption = "Заавал оруулах эсэх";
                gvwObjectItems.Columns[5].ColumnEdit = CreateRepositoryCheckEdit();
                gvwObjectItems.Columns[6].Caption = "Жагсаалт дээр гараас утга орох эсэх";
                gvwObjectItems.Columns[6].ColumnEdit = CreateRepositoryCheckEdit();
                gvwObjectItems.Columns[7].Caption = "Default";
                gvwObjectItems.Columns[8].Caption = "Тайлбар";
                gvwObjectItems.Columns[9].Caption = "Эрэмбэ";
                FormUtility.RestoreStateGrid(appname, formname, ref gvwObjectItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ri_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            string val = "";
            if (e.Value != null)
            {
                val = e.Value.ToString();
            }
            else
            {
                val = "False";
            }
            switch (val)
            {
                case "True":
                    e.CheckState = CheckState.Checked;
                    break;
                case "False":
                    e.CheckState = CheckState.Unchecked;
                    break;
                case "Yes":
                    goto case "True";
                case "No":
                    goto case "False";
                case "1":
                    goto case "True";
                case "0":
                    goto case "False";
                default:
                    e.CheckState = CheckState.Checked;
                    break;
            }
            e.Handled = true;
        }
        public RepositoryItemCheckEdit CreateRepositoryCheckEdit()
        {
            RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(ri)).BeginInit();
            ri.AutoHeight = false;
            ri.AllowFocused = false;
            ri.ValueChecked = 1;
            ri.ValueUnchecked = 0;
            ((System.ComponentModel.ISupportInitialize)(ri)).EndInit();
            ri.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(ri_QueryCheckStateByValue);
            return ri;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            object[] FieldName = {"STATUS","TYPEID","ITEMID","NAME","ISKEY","ISMANDATORY","ISCOMBOEDITABLE","VALUEDEFAULT","DESCRIPTION","ORDERNO"};
            Result res = new Result();
            try
            {
                object[] obj = new object[2];
                DataTable DT = (DataTable)grdObjectItems.DataSource;
                if (DT != null)
                {
                    obj[0] = _objectid;
                    obj[1] = DT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140197, 140197, obj);
                    if (res.ResultNo == 0)
                    {
                        RefreshData(_objectid);
                        SetObjectItemsData();
                        MessageBox.Show("Амжилттай нэмлээ .");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormObjectItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormObjectItems_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwObjectItems);
        }
    }
}
