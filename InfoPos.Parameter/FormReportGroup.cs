using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Data;
using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;
using ISM.CUser;
using System.Threading;

namespace InfoPos.Parameter
{
    public partial class FormReportGroup : Form
    {
        #region [ Variables ]
            Core.Core _core;
            int btn = 0;
            int rowhandle = 0;
            int pRiskGroupID;
            string appname = "", formname = "";
            Form FormName = null;
            object[] OldValue;
        #endregion
        public FormReportGroup(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            RefreshRiskGroupData();
        }
        private void RiskGroup_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            Init();
            RefreshRiskGroupData();
            RefreshRiskCondData(pRiskGroupID);
            if (_core.Resource != null)
            {
                btnRiskGroupAdd.Image = _core.Resource.GetImage("navigate_add");
                btnRiskGroupEdit.Image = _core.Resource.GetImage("navigate_edit");
                btnRiskGroupDelete.Image = _core.Resource.GetImage("navigate_delete");
                bntRiskGroupClear.Image = _core.Resource.GetImage("navigate_clear");
                btnJoin.Image = _core.Resource.GetImage("navigate_save");
            }
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwRiskGroup);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwRisk);
        }
        bool check()
        {
            if (txtGroupID.Text == "")
            {
                MessageBox.Show("Бүлгийн дугаар оруулна уу .");
                return false;
            }
            else
            {
                if (txtGroupName.Text == "")
                {
                    MessageBox.Show("Бүлгийн нэр оруулна уу .");
                    return false;
                }
                else
                {
                    if (txtOrderNo.Text == "")
                    {
                        MessageBox.Show("Жагшаалтын дугаар оруулна уу.");
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        void Init()
        {
            #region [ Combo ]
                FormUtility.SetFormatGrid(ref gvwRiskGroup, false);
            #endregion
        }
        void RefreshRiskGroupData()
        {
            rowhandle = gvwRiskGroup.FocusedRowHandle;
            Result res = new Result();
            try
            {
                object[] obj = new object[2];
                obj[0] = 1;

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140422, 140422, obj);
                if (res.ResultNo == 0 || res.ResultNo == 9110014)
                {
                    grdRiskGroup.DataSource = res.Data.Tables[0];
                    SetRiskGroupData();
                    if (btn == 1)
                    {
                        gvwRiskGroup.FocusedRowHandle = rowhandle - 1;
                        btn = 0;
                    }
                    else
                    {
                        gvwRiskGroup.FocusedRowHandle = rowhandle;
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
        void RefreshRiskCondData(int pRiskGroupID)
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140427, 140427, new object[] { pRiskGroupID });
                if (res.ResultNo == 0 || res.ResultNo == 9110014)
                {
                        grdRisk.DataSource = res.Data.Tables[0];
                        SetRiskData();
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
        void SetRiskGroupData()
        {
            /*RISKGROUPID, NAME, NAME2, RISKTYPE, ORDERNO*/
            gvwRiskGroup.Columns[0].Caption = "Динамик тайлангийн бүлгийн дугаар";
            gvwRiskGroup.Columns[1].Caption = "Нэр";
            gvwRiskGroup.Columns[2].Caption = "Нэр2";
            gvwRiskGroup.Columns[3].Caption = "Эрэмбэ";
            FormUtility.RestoreStateGrid(appname, formname, ref gvwRiskGroup);
        }
        void SetRiskData()
        {
            gvwRisk.Columns[0].Caption = "Төлөв";
            gvwRisk.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwRisk.Columns[1].Caption = "Гүйлгээний код";
            gvwRisk.Columns[1].OptionsColumn.AllowEdit = false;
            gvwRisk.Columns[2].Caption = "Нэр";
            gvwRisk.Columns[2].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwRisk);
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
        private void gvwRiskGroup_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwRiskGroup.GetFocusedDataRow();
            if (dr != null)
            {
                pRiskGroupID = Static.ToInt(dr["GROUPID"]);
                txtGroupID.Text = Static.ToStr(dr["GROUPID"]);
                txtGroupName.Text = Static.ToStr(dr["Name"]);
                txtGroupName2.Text = Static.ToStr(dr["Name2"]);
                txtOrderNo.Text = Static.ToStr(dr["OrderNo"]);
                RefreshRiskCondData(Static.ToInt(dr["GROUPID"]));
                if (gvwRiskGroup.RowCount > 0)
                    gvwRiskGroup.SelectRow(gvwRiskGroup.GetRowHandle(0));
                    object[] Value = new object[4];
                    Value[0] = Static.ToInt(txtGroupID.EditValue);
                    Value[1] = Static.ToStr(txtGroupName.EditValue);
                    Value[2] = Static.ToStr(txtGroupName2.EditValue);
                    Value[3] = Static.ToStr(txtOrderNo.EditValue);
                    OldValue = Value; 

            }
        }
        void SaveEdit(bool issave)
        {
            rowhandle = gvwRiskGroup.FocusedRowHandle;
            bool checkType=check();
            if (!checkType)
            {
                Result res = new Result();
                try
                {
                    object[] obj = new object[4];
                    string msg = "";
                    obj[0] = Static.ToInt(txtGroupID.EditValue);
                    obj[1] = Static.ToStr(txtGroupName.EditValue);
                    obj[2] = Static.ToStr(txtGroupName2.EditValue);
                    obj[3] = Static.ToInt(txtOrderNo.EditValue);
                    object[] FieldName = {"GROUPID", "Name", "Name2", "OrderNo"};

                    if (issave)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140424, 140424, new object[] { obj, FieldName });
                        msg = "Амжилттай хадгаллаа .";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140425, 140425, new object[] { obj, OldValue, FieldName });
                        msg = "Амжилттай засварлалаа .";
                        gvwRiskGroup.FocusedRowHandle = rowhandle;
                    }

                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        RefreshRiskGroupData();
                        MessageBox.Show(msg);
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
        }
        #region[BTN]
            private void btnRiskGroupAdd_Click(object sender, EventArgs e)
                {
                    SaveEdit(true);
                }
            private void btnRiskGroupEdit_Click(object sender, EventArgs e)
                {
                    SaveEdit(false);
                }
            private void btnRiskGroupDelete_Click(object sender, EventArgs e)
                {
                    Result res = new Result();
                    try
                    {
                        DataRow dr = gvwRiskGroup.GetFocusedDataRow();
                        if (dr != null)
                        {
                            string msg = "";
                            int RiskGroupID = Static.ToInt(dr["GroupID"]);
                            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140426, 140426, new object[] { RiskGroupID });
                            msg = "Амжилттай устгагдлаа .";
                            btn = 1;
                            if (res.ResultNo == 0 || res.ResultNo == 9110014)
                            {
                                RefreshRiskGroupData();
                                MessageBox.Show(msg);
                            }
                            else
                            {
                                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                            }
                        }
                        else
                            MessageBox.Show("Эрсдэлийн бүлгээ сонгоогүй байна");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            private void bntRiskGroupClear_Click(object sender, EventArgs e)
                {
                    txtGroupID.Text = "";
                    txtGroupID.EditValue = null;
                    txtGroupName.Text = "";
                    txtGroupName.EditValue = null;
                    txtGroupName2.Text = "";
                    txtGroupName2.EditValue = null;
                    txtOrderNo.Text = "";
                    txtOrderNo.EditValue = null;
                }
        #endregion
        #region[Check]
            private void checkBox2_CheckedChanged(object sender, EventArgs e)
                {  
                    DataTable DT=(DataTable)grdRisk.DataSource;
                    if (checkBox2.Checked == true)
                    {
                        if (DT != null)
                        {
                            foreach (DataRow dr in DT.Rows)
                            {
                                dr["Status"] = 1;
                            }
                        }
                    }
                    if (checkBox2.Checked == false)
                    {
                        if (DT != null)
                        {
                            foreach (DataRow dr in DT.Rows)
                            {
                                dr["Status"] = 0;
                            }
                        }
                    }
                    grdRisk.DataSource = DT;
                }
        #endregion
        #region[Event]
            private void RiskGroup_FormClosing(object sender, FormClosingEventArgs e)
            {
                FormUtility.SaveStateForm(appname, ref FormName);
                FormUtility.SaveStateGrid(appname, formname, ref gvwRiskGroup);
                FormUtility.SaveStateGrid(appname, formname, ref gvwRisk);
            }
            private void RiskGroup_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
        #endregion
       private void btnJoin_Click_1(object sender, EventArgs e)
       {
           Result res = new Result();
           try
           {
               string msg = "";
               object[] obj = new object[2];
               DataTable dt = (DataTable)grdRisk.DataSource;
               obj[0] = Static.ToInt(txtGroupID.EditValue);
               obj[1] = dt;
               res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140428, 140428, obj);
               msg = "Амжилттай хадгаллаа .";
               if (res.ResultNo == 0 || res.ResultNo == 9110014)
               {
                   RefreshRiskGroupData();
                   MessageBox.Show(msg);
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
           RefreshRiskCondData(pRiskGroupID);
       }

    }
}
