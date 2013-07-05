using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

using EServ.Shared;
using DevExpress.XtraEditors;
using InfoPos.Core;

namespace InfoPos.Admin
{
    public partial class UserGroupProp : ISM.Template.frmTempProp
    {
        #region [ Variables ]
        private Core.Core _core = null;
        private long _groupid = 0, _groupid1 = 0;
        string appname = "", formname = "";
        int rowhandle = 0;
        Form FormName = null;
        object[] oldValue = new object[5];
        object[] newValue = new object[5];
        object[] fieldValue = new object[5];
        #endregion
        #region [ Init ]
        public UserGroupProp(Core.Core core) : this(core, 0)
        {            
        }
        public UserGroupProp(HeavenPro.Core.Core core, long GroupID)
        {
            try
            {
                InitializeComponent();
                _core = core;
                _groupid = GroupID;
                _groupid1 = GroupID;
                InitEvents();

                if (GroupID != 0) this.FieldLinkSetEditState();
                else this.FieldLinkSetNewState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UserGroupProp_Load(object sender, EventArgs e)
        {
            this.Show();
            appname = _core.ApplicationName;
            formname = "Admin." + this.Name;

            FormName = this;            
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwTxns);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdTxns);
                       

            oldValue[0] = Static.ToInt(numTxnGroup.EditValue);
            oldValue[1] = txtName.EditValue;
            oldValue[2] = txtName2.EditValue;
            oldValue[3] = Static.ToInt(numOrderNo.EditValue);
            oldValue[4] = Static.ToInt(numlevelno.EditValue);

            this.FieldLinkSetSaveState();
        }
        private void InitEvents()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(UserGroupProp_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(UserGroupProp_EventRefreshAfter);
                this.EventSave += new delegateEventSave(UserGroupProp_EventSave);
                this.EventEdit += new delegateEventEdit(UserGroupProp_EventEdit);
                this.EventDelete += new delegateEventDelete(UserGroupProp_EventDelete);
                gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);

                this.FieldLinkAdd("numTxnGroup", "groupid", "", true, true);
                this.FieldLinkAdd("txtName", "name", "",false, false);
                this.FieldLinkAdd("txtName2", "name2", "", false, false);
                this.FieldLinkAdd("numOrderNo", "orderno", "", false, false);
                this.FieldLinkAdd("numlevelno", "levelno", "", true, false);
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwTxns);
                ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdTxns);
                this.Resource = _core.resource;
                //this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                _groupid = Static.ToInt(numTxnGroup.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _groupid = 0;
            }
            Refresh_Txn(_groupid);

            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdTxns);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwTxns);
        }
        #endregion
        #region [ Toggles ]
        void UserGroupProp_EventRefresh(ref DataTable dt)
        {
            if (_groupid1 != 0)
                RefreshData(ref dt, _groupid);
            else RefreshData(ref dt, 0);
        }
        private void RefreshData(ref DataTable dt, long pgroupid)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                if (pgroupid != 0)
                    obj[0] = pgroupid;
                else obj[0] = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110105, 110105, obj);

                if (res.ResultNo == 0)
                {                    
                    dt = res.Data.Tables[0];                                       
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
        private void Refresh_Txn(long groupid)
        {
            try
            {
                if (groupid != 0)
                {
                    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110110, 110110, new object[] { groupid });

                    if (res.ResultNo == 0)
                    {
                        grdTxns.DataSource = null;
                        grdTxns.DataSource = res.Data.Tables[0];
                        gvwTxns.Columns[0].ColumnEditName = "CheckEdit";

                        gvwTxns.Columns[0].Caption = "Төлөв";
                        gvwTxns.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                        gvwTxns.Columns[1].Caption = "Гүйлгээ код";
                        gvwTxns.Columns[1].OptionsColumn.AllowEdit = false;

                        gvwTxns.Columns[2].Caption = "Гүйлгээ кодны нэр";
                        gvwTxns.Columns[2].OptionsColumn.AllowEdit = false;
                        gvwTxns.Columns[3].Caption = "Гүйлгээ кодны нэр2";
                        gvwTxns.Columns[3].OptionsColumn.AllowEdit = false;                       
                        ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdTxns);
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
        void UserGroupProp_EventRefreshAfter()
        {           
            this.FieldLinkSetColumnCaption(0, "Бүлгийн дугаар");             
            this.FieldLinkSetColumnCaption(1, "Нэр");
            this.FieldLinkSetColumnCaption(2, "Нэр 2", true);
            this.FieldLinkSetColumnCaption(3, "Зэрэглэлийн түвшин");
            this.FieldLinkSetColumnCaption(4, "Эрэмбэ", true);
                     
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref grdTxns);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwTxns);
            gridView1.FocusedRowHandle = rowhandle;
        }
        void UserGroupProp_EventSave(bool isnew, ref bool cancel)
        {
            rowhandle = gridView1.FocusedRowHandle;           
            string err = "";
            Control cont = null;
            if (!FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
            else
            {
                try
                {
                    Result res = new Result();
                    if (_groupid != 0)
                    {
                        DataTable dt = (DataTable)grdTxns.DataSource;
                        newValue[0] = numTxnGroup.Text;
                        newValue[1] = txtName.Text;
                        newValue[2] = txtName2.Text;
                        newValue[3] = Static.ToInt(numOrderNo.Text);
                        newValue[4] = Static.ToInt(numlevelno.Text);

                        fieldValue[0] = "GROUPID";
                        fieldValue[1] = "NAME";
                        fieldValue[2] = "NAME2";
                        fieldValue[3] = "ORDERNO";
                        fieldValue[4] = "LEVELNO";

                        
                        
                        if (!isnew)
                        {
                            res = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110109, 110109, new object[] { newValue, fieldValue, oldValue, dt });
                            MessageBox.Show("Амжилттай засварлагдлаа.");


                            //gridView1.FocusedRowChanged(("Groupid"));
                            //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, ("Groupid"));
                            //gridView1.GetFocusedRowCellValue(("Groupid"));
                            //RefreshData(ref dt, _groupid); 
                        

                            //grdTxns.AllowRestoreSelectionAndFocusedRow(_groupid);

                            //_groupid = Static.ToInt(numTxnGroup.Text);


                            //GroupProp_EventRefreshAfter();

                        }
                        else
                        {
                            res = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110107, 110107, new object[] { newValue, fieldValue, dt});
                            MessageBox.Show("Амжилттай хадгалагдлаа.");
                            //_groupid = 0;
                            //RefreshData(ref dt, _groupid); 

                        }
                        if (res.ResultNo != 0)
                        {
                            MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
                            return;
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void UserGroupProp_EventEdit(ref bool cancel)
        {
            oldValue[0] = Static.ToInt(numTxnGroup.EditValue);
            oldValue[1] = txtName.EditValue;
            oldValue[2] = txtName2.EditValue;
            oldValue[3] = Static.ToInt(numOrderNo.EditValue);
            oldValue[4] = Static.ToInt(numlevelno.EditValue);
        }
        void UserGroupProp_EventDelete()
        {
            _groupid = 0;
            int groupid = Static.ToInt(numTxnGroup.Text);
            if (groupid != 0)
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                Result res = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110108, 110108, new object[] { groupid, oldValue });
                MessageBox.Show("Амжилттай устгагдлаа.");
                if (res.ResultNo != 0)
                {
                    MessageBox.Show(res.ResultNo.ToString() + " " + res.ResultDesc);
                    DataTable DT = null;
                    RefreshData(ref DT, 0);
                    return;
                }
            }
        }        
        private void UserGroupProp_KeyDown(object sender, KeyEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwTxns);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref grdTxns);
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            DataTable DT = (DataTable)grdTxns.DataSource;
            if (checkBox1.Checked == true)
            {
                if (DT != null)
                {
                    foreach (DataRow dr in DT.Rows)
                    {
                        dr["Status"] = 1;
                    }
                }
            }
            if (checkBox1.Checked == false)
            {
                if (DT != null)
                {
                    foreach (DataRow dr in DT.Rows)
                    {
                        dr["Status"] = 0;
                    }
                }
            }
        }
        #endregion        
    }
}