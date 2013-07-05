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

namespace InfoPos.Parameter
{
    public partial class RiskConControl : Form
    {
        Core.Core _core;
        int pRiskGroupID;
        int pRiskOptionID;
        string pRiskName;
        long pRiskID;
        string appname = "", formname = "";
        Form FormName = null;
        public RiskConControl(Core.Core core, int _pRiskGroupID, int _pRiskOptionID, long _pRiskID, string _pRiskName)
        {
            InitializeComponent();
            pRiskGroupID = _pRiskGroupID;
            _core = core;
            pRiskOptionID = _pRiskOptionID;
            pRiskName = _pRiskName;
            pRiskID = _pRiskID;
        }

        private void btnin_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                DataTable DT = (DataTable)grdConUnDep.DataSource;
                if (DT != null)
                {
                    int pToNo = 0;
                    obj[0] = pRiskGroupID;
                    obj[1] = pRiskOptionID;
                    obj[2] = pRiskID;
                    obj[3] = pToNo;
                    obj[4] = DT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140230, 140230, obj);
                    if (res.ResultNo == 0)
                    {
                        RefreshRiskCond(pRiskOptionID);
                        SetConChainData();
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

        private void btnout_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                DataTable DT = (DataTable)grdConChain.DataSource;
                if (DT != null)
                {
                    int pToNo = 1;
                    obj[0] = pRiskGroupID;
                    obj[1] = pRiskOptionID;
                    obj[2] = pRiskID;
                    obj[3] = pToNo;
                    obj[4] = DT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140230, 140230, obj);
                    if (res.ResultNo == 0)
                    {
                        RefreshRiskCond(pRiskOptionID);
                        SetConUnDepData();
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

        private void btnout1_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                DataTable DT = (DataTable)grdConChain.DataSource;
                if (DT != null)
                {
                    int pToNo = 2;
                    obj[0] = pRiskGroupID;
                    obj[1] = pRiskOptionID;
                    obj[2] = pRiskID;
                    obj[3] = pToNo;
                    obj[4] = DT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140230, 140230, obj);
                    if (res.ResultNo == 0)
                    {
                        RefreshRiskCond(pRiskOptionID);
                        SetConDepData();
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

        private void btnin1_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                DataTable DT = (DataTable)grdConDep.DataSource;
                if (DT != null)
                {
                    int pToNo = 0;
                    obj[0] = pRiskGroupID;
                    obj[1] = pRiskOptionID;
                    obj[2] = pRiskID;
                    obj[3] = pToNo;
                    obj[4] = DT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140230, 140230, obj);
                    if (res.ResultNo == 0)
                    {
                        RefreshRiskCond(pRiskOptionID);
                        SetConChainData();
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

        private void RiskConControl_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            if (_core.resource != null)
            {
                btnin.Image = _core.resource.GetImage("button_next");
                btnin1.Image = _core.resource.GetImage("button_prev");
                btnout.Image = _core.resource.GetImage("button_prev");
                btnout1.Image = _core.resource.GetImage("button_next");
            }
            RefreshRiskCond(pRiskOptionID);
            labelControl2.Text = pRiskName.ToString();
        }
        void RefreshRiskCond(int pRiskOptionID)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[3];
                obj[0] = 3;
                obj[1] = pRiskOptionID;
                obj[2] = pRiskID;

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140229, 140229, obj);
                if (res.ResultNo == 0 || res.ResultNo == 9110014)
                {
                    grdConUnDep.DataSource = res.Data.Tables[0];
                    SetConUnDepData();
                    grdConChain.DataSource = res.Data.Tables[1];
                    SetConChainData();
                    grdConDep.DataSource = res.Data.Tables[2];
                    SetConDepData();
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
        void SetConUnDepData()
        {
            /*RISKGROUPID, NAME, NAME2, RISKTYPE, ORDERNO*/
            gvwConUnDep.Columns[0].Caption = "Төлөв";
            gvwConUnDep.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwConUnDep.Columns[1].Caption = "Эрсдэлийн дугаар";
            gvwConUnDep.Columns[1].OptionsColumn.AllowEdit = false;
            gvwConUnDep.Columns[2].Caption = "Нөхцлийн дугаар";
            gvwConUnDep.Columns[2].OptionsColumn.AllowEdit = false;
            gvwConUnDep.Columns[3].Caption = "Нэр";
            gvwConUnDep.Columns[3].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwConUnDep);
        }
        void SetConChainData()
        {
            /*status, a.riskid, a.name, a.name2*/
            gvwConChain.Columns[0].Caption = "Төлөв";
            gvwConChain.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwConChain.Columns[1].Caption = "Эрсдэлийн дугаар";
            gvwConChain.Columns[1].OptionsColumn.AllowEdit = false;
            gvwConChain.Columns[2].Caption = "Нөхцлийн дугаар";
            gvwConChain.Columns[2].OptionsColumn.AllowEdit = false;
            gvwConChain.Columns[3].Caption = "Нэр";
            gvwConChain.Columns[3].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwConChain);

        }
        void SetConDepData()
        {
            /*status, a.Condid, a.name, a.name2*/
            gvwConDep.Columns[0].Caption = "Төлөв";
            gvwConDep.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwConDep.Columns[1].Caption = "Эрсдэлийн дугаар";
            gvwConDep.Columns[1].OptionsColumn.AllowEdit = false;
            gvwConDep.Columns[2].Caption = "Нөхцлийн дугаар";
            gvwConDep.Columns[2].OptionsColumn.AllowEdit = false;
            gvwConDep.Columns[3].Caption = "Нэр2";
            gvwConDep.Columns[3].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwConDep);

        }

        private void RiskConControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataTable _DT = (DataTable)grdConUnDep.DataSource;
            if (checkBox1.Checked == true)
            {
                checkBox1.Text = "Бүх сонголтыг арилгах";
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        dr["status"] = 1;
                    }
                }
            }
            if (checkBox1.Checked == false)
            {
                checkBox1.Text = "Бүгдийг сонгох";
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        dr["status"] = 0;
                    }
                }
            }
            grdConUnDep.DataSource = _DT;
        }
        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            DataTable _DT = (DataTable)grdConChain.DataSource;
            if (checkBox2.Checked == true)
            {
                checkBox2.Text = "Бүх сонголтыг арилгах";
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        dr["status"] = 1;
                    }
                }
            }
            if (checkBox2.Checked == false)
            {
                checkBox2.Text = "Бүгдийг сонгох";
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        dr["status"] = 0;
                    }
                }
            }
            grdConChain.DataSource = _DT;
        }
        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            DataTable _DT = (DataTable)grdConDep.DataSource;
            if (checkBox3.Checked == true)
            {
                checkBox3.Text = "Бүх сонголтыг арилгах";
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        dr["status"] = 1;
                    }
                }
            }
            if (checkBox3.Checked == false)
            {
                checkBox3.Text = "Бүгдийг сонгох";
                if (_DT != null)
                {
                    foreach (DataRow dr in _DT.Rows)
                    {
                        dr["status"] = 0;
                    }
                }
            }
            grdConDep.DataSource = _DT;
        }

        private void RiskConControl_FormClosing(object sender, FormClosingEventArgs e)
        {

            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwConUnDep);
            FormUtility.SaveStateGrid(appname, formname, ref gvwConDep);
            FormUtility.SaveStateGrid(appname, formname, ref gvwConChain);
        }
    }
}


   
    
    
