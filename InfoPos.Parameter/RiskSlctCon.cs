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
    public partial class RiskSlctCon : Form
    {
        #region[Variable]
        Core.Core _core;
        int pRiskID;
        int pRiskGroupID;
        int btn = 0;
        int rowhandle=0;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        public RiskSlctCon(Core.Core core, int _pRiskGroupID, int _pRiskID)
        {
            InitializeComponent();
            _core = core;
            pRiskID = _pRiskID;
            pRiskGroupID = _pRiskGroupID;
        }
        private void RiskSlctCon_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            this.Show();
            //ISM.Template.FormUtility.SetFormatGrid(ref gvwSlctCon, false);
            RefreshData(pRiskID);
            if (_core.resource != null)
            {
                btnAdd.Image = _core.resource.GetImage("navigate_add");
                btndelete.Image = _core.resource.GetImage("navigate_delete");
            }
        }
        void RefreshData(int pRiskID)
        {
            rowhandle = gvwSlctCon.FocusedRowHandle;
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140224, 140224, new object[] { pRiskID, pRiskGroupID });
                if (res.ResultNo == 0)
                {
                        grdSlctCon.DataSource = res.Data.Tables[0];
                        SetObjectItemsData();
                        switch (btn)
                        {
                            case 0: gvwSlctCon.FocusedRowHandle = rowhandle; break;
                            case 1: gvwSlctCon.FocusedRowHandle = rowhandle - 1; break;
                            btn = 0;
                        }
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
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
                /*
                 status, TYPE, CONCODE, NAME , NAME2*/
                gvwSlctCon.Columns[0].Caption = "Төлөв";
                gvwSlctCon.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                gvwSlctCon.Columns[1].Caption = "Нөхцлийн дугаар";
                gvwSlctCon.Columns[1].OptionsColumn.AllowEdit = false;
                gvwSlctCon.Columns[2].Caption = "Нэр";
                gvwSlctCon.Columns[2].OptionsColumn.AllowEdit = false;
                gvwSlctCon.Columns[3].Caption = "Нэр2";
                gvwSlctCon.Columns[3].OptionsColumn.AllowEdit = false;
                gvwSlctCon.OptionsView.ColumnAutoWidth = false;
                gvwSlctCon.BestFitColumns();
                FormUtility.RestoreStateGrid(appname, formname, ref gvwSlctCon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region[BTN]
        private void btndelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DataRow dr = gvwSlctCon.GetFocusedDataRow();
                if (dr != null)
                {
                    DataTable pDT = (DataTable)grdSlctCon.DataSource;

                    object[] obj = new object[3];
                    obj[0] = pRiskID;
                    obj[1] = pRiskGroupID;
                    obj[2] = pDT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140227, 140227, obj);
                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        MessageBox.Show("Амжилттай хасагдлаа .");
                        RefreshData(pRiskID);
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            InfoPos.Parameter.RiskUnSlctCon frm = new InfoPos.Parameter.RiskUnSlctCon(_core, pRiskGroupID, pRiskID);
            frm.ShowDialog();
            RefreshData(pRiskID);
        }
        #endregion
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
        private void RiskSlctCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataTable DT = (DataTable)grdSlctCon.DataSource;
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
            grdSlctCon.DataSource = DT;
        }

        private void RiskSlctCon_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwSlctCon);
        }
    }
}
