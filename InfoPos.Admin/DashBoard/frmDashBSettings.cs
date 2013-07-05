using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.Admin
{
    public partial class frmDashBSettings : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        public int a = 0;
        string appname = "", formname = "";
        Form FormName = null;
        //RepositoryItemColorEdit colorEditor;
        
        #endregion
        #region[Байгуулагч функц]
        public frmDashBSettings(InfoPos.Core.Core core, string eventrow1, string eventrow2, string selectedrow1, string selectedrow2, string oddrow1, string oddrow2)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnExit.Image = _core.Resource.GetImage("navigate_home");
                btnSave.Image = _core.Resource.GetImage("object_save");
                clrEventRow1.EditValue = eventrow1;
                clrEventRow2.EditValue = eventrow2;
                clrSelectedRow1.EditValue = selectedrow1;
                clrSelectedRow2.EditValue = selectedrow2;
                clrOddRow1.EditValue = oddrow1;
                clrOddRow2.EditValue = oddrow2;
            }
        }
        #endregion
        #region[FormEvent]
        private void frmDashBSettings_Load(object sender, EventArgs e)
        {

            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            try
            {
                string[] name = {
                                    "Шинэ хэлцэл болон гэрээт баталгаа", 
                                    "Хугацаа дөхсөн даатгалын хэлцэл", 
                                    "Хугацаа дөхсөн давхар даатгалын хэлцэл",
                                    "Том нөхөн төлбөр",
                                    _core.CacheGetStr("frmFinalStatement_Name",""),
                                    "Final Statements1",
                                    "Хэрэглэгчийн хариуцсан хэлцлийн мэдээлэл",
                                    "Хэрэглэгчийн хариуцсан нөхөн төлбөрийн мэдээлэл", 
                                    "Ажлын төлөвлөгөө сануулга (CRM & Issue Tracking)", 
                                    "Шинээр гэрээ хийгдэж байгаа явц",
                                    "Төлбөр хувааж төлөх хуваарь",
                                    "Давхар даатгалд өгөх хэлцэл",
                                    "Х/Б харилцагчийн явцын үе шат",
                                    "Нээлттэй төсөл",
                                    "Дуудлага"
                                };
                string[] status = {
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),
                                       //_core.CacheGetStr("frmOldContract_OldContract", "Visible"),
                                       //_core.CacheGetStr("frmReInsurance_ReInsurance", "Visible"),
                                       //_core.CacheGetStr("frmTopClaim_TopClaim", "Visible"),
                                       //_core.CacheGetStr("frmFinalStatements_FinalStatements", "Hidden"),
                                       //_core.CacheGetStr("frmFinalStatements1_FinalStatements1", "Hidden"),
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),
                                       //_core.CacheGetStr("frmNewDeal_NewDeal", "Visible"),

                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "OldContract", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "ReInsurance", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "TopClaim", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "Final Statements", "Hidden")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "Final Statements1", "Hidden")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "Deal Report", "Hidden")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "Claim Report", "Hidden")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "Issue", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "NewContract", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "DealPay", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "MoveReDeal", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "ContactStep", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "Project", "Visible")),
                                       //Static.ToStr(Static.RegisterGet(_core.RegPath , "Dashboard\\Visible", "Operator", "Visible"))
                                  };

                DataTable DT = new DataTable();
                DataRow dr = null;
                DT.Columns.Add("status", typeof(string));//Төлөв
                DT.Columns.Add("name", typeof(string));//Панелийн нэр
                for (int i = 0; i < name.Length; i++)
                {
                    dr = DT.NewRow();
                    dr["status"] = status[i];
                    dr["name"] = name[i];
                    DT.Rows.Add(dr);
                }
               
                grdSettings.DataSource = DT;
                gvwSettings.Columns[0].Caption = "Төлөв";
                gvwSettings.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                gvwSettings.Columns[1].Caption = "Панелийн нэр";
                gvwSettings.Columns[1].OptionsColumn.AllowEdit = false;
                FormUtility.RestoreStateForm(appname, ref FormName);
                FormUtility.RestoreStateGrid(appname, formname, ref gvwSettings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int i = 0;
        void colorEditor_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            i++;
            this.Text = e.Value.ToString()+"   "+i.ToString();
        }


        private void frmDashBSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        #endregion
        #region[BTN]
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                DataTable DT1 = (DataTable)grdSettings.DataSource;
                string[] ints = new string[15];
                foreach (DataRow row in DT1.Rows)
                {
                    ints[i] = Static.ToStr(row["status"]);
                    i++;
                }
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "NewDeal", ints[0]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "OldContract", ints[1]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "ReInsurance", ints[2]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "TopClaim", ints[3]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Final Statements", ints[4]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Final Statements1", ints[5]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Deal Report", ints[6]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Claim Report", ints[7]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Issue", ints[8]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "NewContract", ints[9]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "DealPay", ints[10]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "MoveReDeal", ints[11]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "ContactStep", ints[12]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Project", ints[13]);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Operator", ints[14]);

                //Static.RegisterSave(_core.RegPath, "Dashboard\\RowColor", "EventRow1", clrEventRow1.EditValue);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\RowColor", "EventRow2", clrEventRow2.EditValue);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\RowColor", "SelectedRow1", clrSelectedRow1.EditValue);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\RowColor", "SelectedRow2", clrSelectedRow2.EditValue);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\RowColor", "OddRow1", clrOddRow1.EditValue);
                //Static.RegisterSave(_core.RegPath, "Dashboard\\RowColor", "OddRow2", clrOddRow2.EditValue);
                MessageBox.Show("Амжилттай хадгалагдлаа");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region[Checkbox]
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
                case "Visible":
                    goto case "True";
                case "Hidden":
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
            ri.ValueChecked = "Visible";
            ri.ValueUnchecked = "Hidden";
            ((System.ComponentModel.ISupportInitialize)(ri)).EndInit();
            ri.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(ri_QueryCheckStateByValue);
            return ri;
        }
        #endregion

        private void frmDashBSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwSettings);
        }
    }
}