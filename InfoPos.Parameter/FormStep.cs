using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using ISM.Template;
namespace InfoPos.Parameter
{
    public partial class FormStep : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        int SelectListTxnCode = 140281;
        int SI_SelectTxnCode = 140287;
        int SI_SaveTxnCode = 140286;
        int AddTxnCode = 140283;
        int EditTxnCode = 140284;
        int DeleteTxnCode = 140285;
        string StepID = "";
        object[] OldValue;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public FormStep(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnAdd.Image = _core.Resource.GetImage("navigate_add");
                btnDelete.Image = _core.Resource.GetImage("navigate_delete");
                btnEdit.Image = _core.Resource.GetImage("navigate_edit");
                btnClear.Image = _core.Resource.GetImage("navigate_clear");
                btnSave.Image = _core.Resource.GetImage("navigate_save");
            }
        }
        #endregion
        #region[BTN]
        private void btnClear_Click(object sender, EventArgs e)
        {
            numStepID.EditValue = null;
            numStepID.Text = "";
            txtName.EditValue = null;
            txtName.Text = "";
            txtName2.EditValue = null;
            txtName2.Text = "";
            mmeNote.EditValue = null;
            mmeNote.Text = "";
            mmeNote2.EditValue = null;
            mmeNote2.Text = "";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (check()) 
            {
                Add();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (check())
            {
                Edit();
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwStep);
            FormUtility.SaveStateGrid(appname, formname, ref gvwStepItem);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Del();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        #endregion
        #region[Function]
        bool check()
        {
            if (numStepID.Text == "")
            {
                MessageBox.Show("Бүлгийн дугаар оруулна уу .");
                return false;
            }
            else
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Бүлгийн нэр оруулна уу .");
                    return false;
                }
                else
                {
                    if (mmeNote.Text == "")
                    {
                        MessageBox.Show("Тайлбар оруулна уу .");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        void Add() 
        {
            try
            {
                object[] NewValue = { Static.ToInt(numStepID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToStr(mmeNote.EditValue), Static.ToStr(mmeNote2.EditValue) };
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxnCode, AddTxnCode, new object[] {NewValue});
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
                else
                {
                    Ref();
                    MessageBox.Show("Амжилттай нэмлээ .");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Ref() 
        {
            Result r = new Result();
            try
            {
                r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, SelectListTxnCode, SelectListTxnCode, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
                else
                {
                    grdStep.DataSource = null;
                    grdStep.DataSource = r.Data.Tables[0];

                    if (r.Data.Tables[0].Rows.Count != 0)
                    {
                        gvwStep.SelectRow(gvwStep.GetRowHandle(0));
                        FocusRow();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void RefStepItem(int StepItem)
        {
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, SI_SelectTxnCode, SI_SelectTxnCode, new object[]{StepItem});
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
                else
                {
                    grdStepItem.DataSource = r.Data.Tables[0];
                    gvwStepItem.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                    gvwStepItem.Columns[0].Caption = "Төлөв";
                    gvwStepItem.Columns[0].OptionsColumn.AllowEdit = true;
                    gvwStepItem.Columns[1].Caption = "Дамжлагын дугаар";
                    gvwStepItem.Columns[1].OptionsColumn.AllowEdit = false;
                    gvwStepItem.Columns[2].Caption = "Дамжлагын нэр";
                    gvwStepItem.Columns[2].OptionsColumn.AllowEdit = false;
                    gvwStepItem.Columns[3].Caption = "Дамжлагын нэр 2";
                    gvwStepItem.Columns[3].OptionsColumn.AllowEdit = false;
                    gvwStepItem.Columns[4].Caption = "Дамжлагын тайлбар";
                    gvwStepItem.Columns[4].OptionsColumn.AllowEdit = false;
                    gvwStepItem.Columns[5].Caption = "Дамжлагын тайлбар 2";
                    gvwStepItem.Columns[5].OptionsColumn.AllowEdit = false;
                    gvwStepItem.Columns[6].Caption = "Ажлын хэдэн өдөрт хийгдэх ажил вэ ";
                    gvwStepItem.Columns[6].OptionsColumn.AllowEdit = false;
                    gvwStepItem.Columns[7].Caption = "Жагсаалтын эрэмбэ";
                    gvwStepItem.Columns[7].OptionsColumn.AllowEdit = false;
                    FormUtility.RestoreStateGrid(appname, formname, ref gvwStepItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Edit() 
        {
            try
            {
                object[] NewValue = { Static.ToInt(numStepID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToStr(mmeNote.EditValue), Static.ToStr(mmeNote2.EditValue) };
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxnCode, EditTxnCode, new object[] {NewValue,OldValue});
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
                else
                {
                    Ref();
                    MessageBox.Show("Амжилттай засварлалаа .");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Del() 
        {
            if (numStepID.Text != "")
            {
                #region[]
                DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DR == System.Windows.Forms.DialogResult.No) return;
                else
                {
                    try
                    {
                        Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { Static.ToInt(numStepID.EditValue) });
                        if (r.ResultNo != 0)
                        {
                            MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                            return;
                        }
                        else
                        {
                            Ref();
                            MessageBox.Show("Амжилттай устгагдлаа .");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                #endregion
            }
        }
        void Caption() 
        {
            gvwStep.Columns[0].Caption = "Бүлэгийн дугаар";
            gvwStep.Columns[1].Caption = "Бүлэгийн нэр";
            gvwStep.Columns[2].Caption = "Бүлэгийн нэр 2";
            gvwStep.Columns[3].Caption = "Бүлэгийн тайлбар";
            gvwStep.Columns[4].Caption = "Бүлэгийн тайлбар 2";
            FormUtility.RestoreStateGrid(appname, formname, ref gvwStep);
        }
        void Save() 
        {
            try
            {
                if(StepID!="")
                {
                    DataTable DT = ((DataView)gvwStepItem.DataSource).Table;
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, SI_SaveTxnCode, SI_SaveTxnCode,new object[]{Static.ToInt(StepID),DT});
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        RefStepItem(Static.ToInt(StepID));
                        MessageBox.Show("Амжилттай хадгалагдлаа .");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        void FocusRow()
        {
            DataRow row = gvwStep.GetFocusedDataRow();
            if (row != null)
            {
                StepID = Static.ToStr(row["StepID"]);
                numStepID.EditValue = row["StepID"];
                txtName.EditValue = row["Name"];
                txtName2.EditValue = row["Name2"];
                mmeNote.EditValue = row["Note"];
                mmeNote2.EditValue = row["Note2"];
                RefStepItem(Static.ToInt(row["StepID"]));
                object[] Value = { Static.ToInt(numStepID.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToStr(mmeNote.EditValue), Static.ToStr(mmeNote2.EditValue) };
                OldValue = Value;
            }            
        }
        private void gvwStep_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gvwStep.GetFocusedDataRow();
            if (row != null)
            {
                StepID = Static.ToStr(row["StepID"]);
                numStepID.EditValue = row["StepID"];
                txtName.EditValue = row["Name"];
                txtName2.EditValue = row["Name2"];
                mmeNote.EditValue = row["Note"];
                mmeNote2.EditValue = row["Note2"];
                RefStepItem(Static.ToInt(row["StepID"]));
            }
        }
        #region[FormEvent]
        private void FormStep_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwStep);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwStepItem);
            Ref();
            Caption();
        }
        private void FormStep_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormStep_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwStep);
            FormUtility.SaveStateGrid(appname, formname, ref gvwStepItem);
        }
        #endregion
    }
}