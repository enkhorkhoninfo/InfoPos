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
    public partial class RiskGroup : Form
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
        public RiskGroup(Core.Core core)
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
            if (_core.resource != null)
            {
                btnRiskGroupAdd.Image = _core.resource.GetImage("navigate_save");
                btnRiskGroupEdit.Image = _core.resource.GetImage("navigate_edit");
                btnRiskGroupDelete.Image = _core.resource.GetImage("navigate_delete");
                bntRiskGroupClear.Image = _core.resource.GetImage("navigate_clear");
                btnRiskRemove.Image = _core.resource.GetImage("navigate_delete");
                btnRiskJoin.Image = _core.resource.GetImage("navigate_add");
                btnRiskAddCon.Image = _core.resource.GetImage("navigate_add");
                simpleButton4.Image = _core.resource.GetImage("navigate_delete");
                btnjoin.Image = _core.resource.GetImage("navigate_add");
            }
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwRiskGroup);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwRisk);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwCond);
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
                    if (cboRiskType.Text == "")
                    {
                        MessageBox.Show("Эрсдэлийн төрөл оруулна уу.");
                        return false;
                    }
                    else
                    {
                        if (txtOrderNo.Text == "")
                        {
                            MessageBox.Show("Эрэмбэ оруулна уу.");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }
        void Init()
        {
            #region [ Combo ]
            FormUtility.LookUpEdit_SetList(ref cboRiskType, 0, "Хувь");
            FormUtility.LookUpEdit_SetList(ref cboRiskType, 1, "Дүн");
            FormUtility.LookUpEdit_SetList(ref cboType, 0, "Хамааалтай");
            FormUtility.LookUpEdit_SetList(ref cboType, 1, "Хамааралгүй");
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

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140206, 140206, obj);
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
                object[] obj = new object[2];
                obj[0] = 2;
                obj[1] = pRiskGroupID;

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140212, 140212, obj);
                if (res.ResultNo == 0 || res.ResultNo == 9110014)
                {
                   
                        grdRisk.DataSource = res.Data.Tables[0];
                        SetRiskData();
                        grdCond.DataSource = res.Data.Tables[1];
                        SetCondData();
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
            gvwRiskGroup.Columns[0].Caption = "Бүлгийн дугаар";
            gvwRiskGroup.Columns[1].Caption = "Нэр";
            gvwRiskGroup.Columns[2].Caption = "Нэр2";
            gvwRiskGroup.Columns[3].Caption = "Эрсдэлийн үнэлгээний хэмжээ";
            gvwRiskGroup.Columns[4].Caption = "Эрэмбэ";
            gvwRiskGroup.Columns[5].Caption = "Эрсдэлийн төрөл";
            FormUtility.RestoreStateGrid(appname, formname, ref gvwRiskGroup);
        }
        void SetRiskData()
        {
            /*status, a.riskid, a.name, a.name2*/
            gvwRisk.Columns[0].Caption = "Төлөв";
            gvwRisk.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwRisk.Columns[1].Caption = "Эрсдэлийн дугаар";
            gvwRisk.Columns[1].OptionsColumn.AllowEdit = false;
            gvwRisk.Columns[2].Caption = "Нэр";
            gvwRisk.Columns[2].OptionsColumn.AllowEdit = false;
            gvwRisk.Columns[3].Caption = "Нэр2";
            gvwRisk.Columns[3].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwRisk);
        }
        void SetCondData()
        {
            /*status, a.Condid, a.name, a.name2*/
            gvwCond.Columns[0].Caption = "Төлөв";
            gvwCond.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwCond.Columns[1].Caption = "Үйлчилгээний дугаар";
            gvwCond.Columns[1].OptionsColumn.AllowEdit = false;
            gvwCond.Columns[2].Caption = "Нэр";
            gvwCond.Columns[2].OptionsColumn.AllowEdit = false;
            gvwCond.Columns[3].Caption = "Нэр2";
            gvwCond.Columns[3].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwCond);
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
                pRiskGroupID = Static.ToInt(dr["RISKGROUPID"]);
                txtGroupID.Text = Static.ToStr(dr["RISKGROUPID"]);
                txtGroupName.Text = Static.ToStr(dr["Name"]);
                txtGroupName2.Text = Static.ToStr(dr["Name2"]);
                cboRiskType.EditValue = Static.ToInt(dr["RiskType"]);
                cboType.EditValue = Static.ToInt(dr["Type"]);
                txtOrderNo.Text = Static.ToStr(dr["OrderNo"]);
                if (gvwRiskGroup.RowCount > 0)
                    gvwRiskGroup.SelectRow(gvwRiskGroup.GetRowHandle(0));
                    object[] Value = new object[6];
                    Value[0] = Static.ToInt(txtGroupID.EditValue);
                    Value[1] = Static.ToStr(txtGroupName.EditValue);
                    Value[2] = Static.ToStr(txtGroupName2.EditValue);
                    Value[3] = Static.ToInt(cboRiskType.EditValue);
                    Value[4] = Static.ToStr(txtOrderNo.EditValue);
                    Value[5] = Static.ToInt(cboType.EditValue);
                    OldValue = Value; 
                RefreshRiskCondData(pRiskGroupID);
            }
        }
        void SaveEdit(bool issave)
        {
            rowhandle = gvwRiskGroup.FocusedRowHandle;
            if (check())
            {
                Result res = new Result();
                try
                {
                    object[] obj = new object[6];
                    string msg = "";
                    obj[0] = Static.ToInt(txtGroupID.EditValue);
                    obj[1] = Static.ToStr(txtGroupName.EditValue);
                    obj[2] = Static.ToStr(txtGroupName2.EditValue);
                    obj[3] = Static.ToInt(cboRiskType.EditValue);
                    obj[4] = Static.ToStr(txtOrderNo.EditValue); 
                    obj[5] = Static.ToInt(cboType.EditValue);
                    object[] FieldName = {"RISKGROUPID", "Name", "Name2", "RiskType", "Type", "OrderNo"};

                    if (issave)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140208, 140208, new object[]{obj, FieldName});
                        msg = "Амжилттай хадгаллаа .";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140209, 140209, new object[]{ obj, OldValue, FieldName});
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
                    int RiskGroupID = Static.ToInt(dr["RiskGroupID"]);
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140210, 140210, new object[] { RiskGroupID });
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
            cboRiskType.EditValue = null;
            txtOrderNo.Text = "";
            txtOrderNo.EditValue = null;
        }
        private void btnRiskRemove_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DataRow dr = gvwRisk.GetFocusedDataRow();
                if (dr != null)
                {
                    DataTable pDT = (DataTable)grdRisk.DataSource;

                    object[] obj = new object[2];
                    obj[0] = pRiskGroupID;
                    obj[1] = pDT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140213, 140213, obj );
                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        RefreshRiskCondData(pRiskGroupID);
                        MessageBox.Show("Амжилттай хасагдлаа .");
                        btn = 1;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                else
                    MessageBox.Show("Эрсдлээ сонгоогүй байна");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
        private void btnjoin_Click_1(object sender, EventArgs e)
        {
            InfoPos.Parameter.ConInRel frm = new InfoPos.Parameter.ConInRel(_core, pRiskGroupID);
            frm.ShowDialog();
            RefreshRiskCondData(pRiskGroupID);
        }
        private void btnRiskAddCon_Click(object sender, EventArgs e)
        {
            DataRow DR = gvwRisk.GetDataRow(gvwRisk.FocusedRowHandle);

            if (DR != null)
            {
                int pRiskID = Static.ToInt(DR["RiskID"]);
                InfoPos.Parameter.RiskSlctCon frm = new InfoPos.Parameter.RiskSlctCon(_core, pRiskGroupID, pRiskID);
                frm.ShowDialog();
                //RefreshRiskCondData(pRiskGroupID);
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DataRow dr = gvwCond.GetFocusedDataRow();
                if (dr != null)
                {
                    DataTable pDT = (DataTable)grdCond.DataSource;

                    object[] obj = new object[2];
                    obj[0] = pRiskGroupID;
                    obj[1] = pDT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140213, 140213, obj);
                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        RefreshRiskCondData(pRiskGroupID);
                        MessageBox.Show("Амжилттай хасагдлаа .");
                        btn = 1;
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                else
                    MessageBox.Show("Үйлчилгээгээ сонгоогүй байна");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataTable _DT = (DataTable)grdCond.DataSource;

            if (checkBox1.Checked == true)
            {
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        dr["Status"] = 1;
                    }
                }
            }
            if (checkBox1.Checked == false)
            {
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        dr["Status"] = 0;
                    }
                }
            }
            grdCond.DataSource = _DT;
        }
        #endregion
        private void RiskGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwRiskGroup);
            FormUtility.SaveStateGrid(appname, formname, ref gvwRisk);
            FormUtility.SaveStateGrid(appname, formname, ref gvwCond);
        }
        private void RiskGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
