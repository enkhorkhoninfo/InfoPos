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
    public partial class FormObjectShowItems : Form
    {
        int rowhandle=0;
        #region [ Variables ]
        Core.Core _core;
        int _objectitems = 0, _objectid = 0;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        public FormObjectShowItems(Core.Core core) : this(core, 0)
        {
            
        }
        public FormObjectShowItems(Core.Core core, int objectid)
        {
            InitializeComponent();
            _core = core;
            _objectid = objectid;
        }
        private void FormObjectShowItems_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            this.Show();
            //ISM.Template.FormUtility.SetFormatGrid(ref gvwObjectItems, false);
            RefreshData(_objectid);
            if (_core.Resource != null)
            {
                btnAdd.Image = _core.Resource.GetImage("navigate_add");
                btnSave.Image = _core.Resource.GetImage("navigate_save");
            }
        }
        void RefreshData(int pobjectid)
        {
            rowhandle = gvwObjectItems.FocusedRowHandle;
            Result res = new Result();
            try
            {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140196, 140196, new object[] { pobjectid, 1 });
                    if (res.ResultNo == 0)
                    {
                        if (res.AffectedRows != 0)
                        {
                            grdObjectItems.DataSource = res.Data.Tables[0];
                            SetObjectItemsData();
                            gvwObjectItems.FocusedRowHandle = rowhandle;
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
                gvwObjectItems.Columns[3].Caption = "Нэр";
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            InfoPos.Parameter.FormObjectItems frm = new InfoPos.Parameter.FormObjectItems(_core, _objectid);
            frm.ShowDialog();
            RefreshData(_objectid);
        }

        private void FormObjectShowItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FormObjectShowItems_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwObjectItems);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140288, 140288, new object[] { _objectid, grdObjectItems.DataSource });
                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай хадгаллаа");
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
