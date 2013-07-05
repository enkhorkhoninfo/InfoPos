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
    public partial class ConInRel : Form
    {
        #region [ Variables ]
        Core.Core _core;
        int pRiskGroupID;
        string appname = "", formname = "";
        Form FormName = null;


        #endregion
        public ConInRel(Core.Core core, int _pRiskGroupID)
        {
            InitializeComponent();
            _core = core;
            pRiskGroupID = _pRiskGroupID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                object[] obj = new object[2];
                DataTable pDT = (DataTable)grdConInRel.DataSource;
                if (pDT != null)
                {
                    obj[0] = pRiskGroupID;
                    obj[1] = pDT;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140215, 140215, obj);
                    if (res.ResultNo == 0)
                    {
                        RefreshData(pRiskGroupID);
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

        private void ConInRel_Load_1(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            this.Show();
            ISM.Template.FormUtility.SetFormatGrid(ref gvwConInRel, true);
            RefreshData(pRiskGroupID);
            if (_core.resource != null)
            {
                btnSave.Image = _core.resource.GetImage("navigate_save");
            }
        }
        void RefreshData(int pRiskGroupID)
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140221, 140221, new object[] { pRiskGroupID });
                if (res.ResultNo == 0)
                {
                    if (res.AffectedRows != 0)
                    {
                        grdConInRel.DataSource = res.Data.Tables[0];
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
                gvwConInRel.Columns[0].Caption = "Төлөв";
                gvwConInRel.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();

                gvwConInRel.Columns[1].Caption = "Үйлчилгээний дугаар";
                gvwConInRel.Columns[1].OptionsColumn.AllowEdit = false;
                
                gvwConInRel.Columns[2].Caption = "Нэр";
                gvwConInRel.Columns[2].OptionsColumn.AllowEdit = false;


                gvwConInRel.Columns[3].Caption = "Нэр2";
                gvwConInRel.Columns[3].OptionsColumn.AllowEdit = false;
                gvwConInRel.OptionsView.ColumnAutoWidth = false;
                gvwConInRel.BestFitColumns();
                FormUtility.RestoreStateGrid(appname, formname, ref gvwConInRel);
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

        private void grdConInRel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void ConInRel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void grdConInRel_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataTable _DT = (DataTable)grdConInRel.DataSource;

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
            grdConInRel.DataSource = _DT;
        }

        private void ConInRel_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);

            FormUtility.SaveStateGrid(appname, formname, ref gvwConInRel);
        }
    }
}
