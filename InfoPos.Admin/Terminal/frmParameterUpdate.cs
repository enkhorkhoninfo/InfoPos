using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

using EServ.Shared;
using ISM.Template;

namespace InfoPos.Admin
{
    public partial class frmParameterUpdate : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        int UpdateTxn = 140000;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public frmParameterUpdate(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnUpdate.Image = _core.Resource.GetImage("gl_ok");
                btnClose.Image = _core.Resource.GetImage("navigate_cancel");
            }
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            Init();
            this.FormClosing += new FormClosingEventHandler(frmParameterUpdate_FormClosing);
        }
        #endregion
        #region[Init]
        void Init()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add("status", typeof(int));
                dt.Columns.Add("paramno", typeof(int));
                dt.Columns.Add("paramname", typeof(string));
                string[] name = { "Ерөнхий параметр", "Байгууллагын дансны бүтээгдэхүүн", "Балансын гадуурх дансны бүтээгдэхүүн",
                                    "Валют", "Дансны дугаар", "Үндсэн хөрөнгийн төрөл", "Бараа материалын төрөл", "Түр дансны мэдээлэл",
                                    "Даатгалын бүтээгдэхүүн", "Ерөнхий дэвтэр", "Нөөцийн сангийн томъёо", "Даатгалын бүтээгдэхүүний нөөцийн сан", 
                                    "Нөөцийн сан", "Автомат дугаарлалт", "Автомат дугаарлалтын утга" };
                for (int i = 0; i <= 14; i++)
                {
                    dr = dt.NewRow();
                    dr["status"] = 0;
                    dr["paramno"] = i;
                    dr["paramname"] = name[i];
                    dt.Rows.Add(dr);
                }
                grdParameter.DataSource = dt;
                SetParamData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Function]
        void SetParamData()
        {
            gvwParameter.Columns[0].Caption = "Төлөв";
            gvwParameter.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gvwParameter.Columns[1].Caption = "Параметрийн дугаар";
            gvwParameter.Columns[2].Caption = "Параметрийн нэр";
            gvwParameter.Columns[1].Visible = false;
            gvwParameter.Columns[1].OptionsColumn.AllowEdit = false;
            gvwParameter.Columns[2].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwParameter);
        }
        #endregion
        #region[Check]
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
        #endregion
        #region[BTN]
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int check = 0;
                Result res = new Result();
                DataTable dt = (DataTable)grdParameter.DataSource;
                foreach (DataRow dr in dt.Rows)
                {
                    if (Static.ToInt(dr["status"]) == 1)
                    {
                        check = 1;
                    }
                }
                if (check == 1)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, UpdateTxn, UpdateTxn, new object[] { (DataTable)grdParameter.DataSource });
                    if (res.ResultNo == 0)
                    {
                        progressbar.EditValue = 100;
                        MessageBox.Show("Амжилттай шинэчлэгдлээ");
                        check = 0;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + "" + res.ResultDesc);
                    }
                }
                else
                {
                    MessageBox.Show("Шинэчлэх параметрээ сонгоно уу");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region[FormEvent]
        void frmParameterUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        void frmParameterUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwParameter);
        }
        #endregion

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            DataTable DT = (DataTable)grdParameter.DataSource;
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
            grdParameter.DataSource = DT;
        }
    }
}