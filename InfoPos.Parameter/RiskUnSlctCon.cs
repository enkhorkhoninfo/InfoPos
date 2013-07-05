using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class RiskUnSlctCon : Form
    {
        #region[Variables]
        Core.Core _core;
        int pRiskID;
        int pRiskGroupID;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        public RiskUnSlctCon(Core.Core core, int _pRiskGroupID,int _pRiskID)
        {
            _core = core;
            pRiskID = _pRiskID;
            pRiskGroupID = _pRiskGroupID;
            InitializeComponent();
        }

        private void RiskUnSlctCon_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName); this.Show();
            ISM.Template.FormUtility.SetFormatGrid(ref gvwUnSlctCon, true);
            RefreshData(pRiskID);
            if (_core.resource != null)
            {
                btnSave.Image = _core.resource.GetImage("navigate_save");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[3];
                DataTable pDT = (DataTable)grdUnSlctCon.DataSource;
                if (pDT != null)
                {
                    obj[0] = pRiskID;
                    obj[1] = pRiskGroupID;
                    obj[2] = pDT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140226, 140226, obj);
                    if (res.ResultNo == 0)
                    {
                        RefreshData(pRiskID);
                        SetObjectItemsData();
                        MessageBox.Show("Амжилттай нэмлээ.");
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
        void RefreshData(int pRiskID)
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140225, 140225, new object[] { pRiskID, pRiskGroupID });
                if (res.ResultNo == 0)
                {
                    if (res.AffectedRows != 0)
                    {
                        grdUnSlctCon.DataSource = res.Data.Tables[0];
                        SetObjectItemsData();
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
                 status, TYPE, CONCODE, NAME , NAME2
                 */
                gvwUnSlctCon.Columns[0].Caption = "Төлөв";
                gvwUnSlctCon.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();

                gvwUnSlctCon.Columns[1].Caption = "Нөхцлийн дугаар";
                gvwUnSlctCon.Columns[1].OptionsColumn.AllowEdit = false;

                gvwUnSlctCon.Columns[2].Caption = "Нэр";
                gvwUnSlctCon.Columns[2].OptionsColumn.AllowEdit = false;

                gvwUnSlctCon.Columns[3].Caption = "Нэр2";
                gvwUnSlctCon.Columns[3].OptionsColumn.AllowEdit = false;
                gvwUnSlctCon.OptionsView.ColumnAutoWidth = false;
                gvwUnSlctCon.BestFitColumns();
                FormUtility.RestoreStateGrid(appname, formname, ref gvwUnSlctCon);

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

        private void grdUnSlctCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void RiskUnSlctCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataTable DT = (DataTable)grdUnSlctCon.DataSource;
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
            {if (DT != null)
                {
                foreach (DataRow dr in DT.Rows)
                {
                    dr["Status"] = 0;
                }
                }
            }
            grdUnSlctCon.DataSource = DT;
        }

        private void RiskUnSlctCon_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwUnSlctCon);
        }
    }
}
