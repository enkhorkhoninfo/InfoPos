using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraEditors.Repository;
using ISM.Template;
using EServ.Shared;

namespace InfoPos.schd
{
    public partial class CustomAppFront : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
        SchedulerControl control;
        Appointment apt;
        Core.Core _core;
        bool openRecurrenceForm = false;
        int suspendUpdateCount;
        string _orderno;
        string _prodno;
        int _prodtype;
        string _custname = "";
        bool isnew = true;
        object[] OldObj = new object[8];
        MyAppointmentFormController controller;
        ISM.Touch.TouchKeyboard _touchkeyboard = null;
        protected AppointmentStorage Appointments
        {
            get { return control.Storage.Appointments; }
        }
        protected bool IsUpdateSuspended { get { return suspendUpdateCount > 0; } }
        #endregion
        #region[Contructure]
        public CustomAppFront(Core.Core core, SchedulerControl control, Appointment apt, bool openRecurrenceForm, string orderno, string prodno, int prodtype, ISM.Touch.TouchKeyboard touchkeyboard, string custname)
        {
            this.openRecurrenceForm = openRecurrenceForm;
            this.controller = new MyAppointmentFormController(control, apt);
            this.apt = apt;
            this.control = control;
            _core = core;
            _prodno = prodno;
            _prodtype = prodtype;
            _orderno = orderno;
            _custname = custname;
            SuspendUpdate();
            InitializeComponent();
            _touchkeyboard = touchkeyboard;
            ResumeUpdate();
            UpdateForm();
            if (_core.Resource != null)
            {
                btnServChooce.Image = _core.Resource.GetImage("button_ok");
                btnServDelete.Image = _core.Resource.GetImage("object_delete");
                btnServFind.Image = _core.Resource.GetImage("button_find");
            }
        }
        #endregion
        #region[Events]
        private void dteStartDay_EditValueChanged(object sender, System.EventArgs e)
        {
            if (!IsUpdateSuspended)
                controller.DisplayStart = dteStartDay.DateTime.Date + tmeStart.Time.TimeOfDay;
            UpdateIntervalControls();
        }
        private void tmeStart_EditValueChanged(object sender, System.EventArgs e)
        {
            if (!IsUpdateSuspended)
                controller.DisplayStart = dteStartDay.DateTime.Date + tmeStart.Time.TimeOfDay;
            UpdateIntervalControls();
        }
        private void tmeEnd_EditValueChanged(object sender, System.EventArgs e)
        {
            if (IsUpdateSuspended) return;
            if (IsIntervalValid())
                controller.DisplayEnd = dteEndDay.DateTime.Date + tmeEnd.Time.TimeOfDay;
            else
                tmeEnd.EditValue = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);
        }
        private void dteEndDay_EditValueChanged(object sender, System.EventArgs e)
        {
            if (IsUpdateSuspended) return;
            if (IsIntervalValid())
                controller.DisplayEnd = dteEndDay.DateTime.Date + tmeEnd.Time.TimeOfDay;
            else
                dteEndDay.EditValue = controller.DisplayEnd.Date;
        }
        private void checkAllDay_CheckedChanged(object sender, System.EventArgs e)
        {
            controller.AllDay = this.checkAllDay.Checked;
            UpdateIntervalControls();
        }
        private void CustomApp_Load(object sender, EventArgs e)
        {
            if (_touchkeyboard != null)
            {
                _touchkeyboard.Enable = true;
                _touchkeyboard.AddToKeyboard(txtOrderNo);
                _touchkeyboard.AddToKeyboard(txtSalesNo);
                _touchkeyboard.AddToKeyboard(dteStartDay);
                _touchkeyboard.AddToKeyboard(dteEndDay);
                _touchkeyboard.AddToKeyboard(tmeStart);
                _touchkeyboard.AddToKeyboard(tmeEnd);
            }
            if (controller.Subject != null && controller.Subject != "")
            {
                txtSalesNo.EditValue = controller.Subject;
            }
            else
            {
                txtSalesNo.EditValue = _custname;
            }
            
            btnSave.Image = _core.Resource.GetImage("object_save");
            btnCancel.Image = _core.Resource.GetImage("image_exit");
            btnDelete.Image = _core.Resource.GetImage("object_delete");

            resType.SchedulerControl = control;
            resType.Storage = control.Storage;
            if (txtSalesNo.EditValue != "")
                isnew = false;
            txtOrderNo.EditValue = _orderno;
            OldObj[0] = _prodtype;
            OldObj[1] = _prodno;
            OldObj[2] = Static.ToInt(resType.EditValue);
            OldObj[3] = _orderno;
            OldObj[4] = txtSalesNo.EditValue;
            OldObj[5] = Static.ToDateTime(controller.DisplayStart);
            OldObj[6] = Static.ToDateTime(controller.DisplayEnd);
            OldObj[7] = Static.ToInt(edLabel.Label.MenuCaption);
            resType.Properties.Items.RemoveAt(0);

        }
        private void CustomApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        #endregion
        #region[Function]
        bool IsIntervalValid()
        {
            DateTime start = dteStartDay.DateTime + tmeStart.Time.TimeOfDay;
            DateTime end = dteEndDay.DateTime + tmeEnd.Time.TimeOfDay;
            return end >= start;
        }
        protected void SuspendUpdate()
        {
            suspendUpdateCount++;
        }
        protected void ResumeUpdate()
        {
            if (suspendUpdateCount > 0)
                suspendUpdateCount--;
        }
        void UpdateForm()
        {
            SuspendUpdate();
            try
            {
                txtSalesNo.Text = controller.Subject;
                txtOrderNo.Text = controller.Description;
                edLabel.Label = Appointments.Labels[controller.LabelId];

                dteStartDay.DateTime = controller.DisplayStart.Date;
                dteEndDay.DateTime = controller.DisplayEnd.Date;

                tmeStart.Time = new DateTime(controller.DisplayStart.TimeOfDay.Ticks);
                tmeEnd.Time = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);
                checkAllDay.Checked = controller.AllDay;

                edLabel.Storage = control.Storage;

                resType.ResourceId = controller.ResourceId;
            }
            finally
            {
                ResumeUpdate();
            }
            UpdateIntervalControls();
        }
        protected virtual void UpdateIntervalControls()
        {
            if (IsUpdateSuspended)
                return;

            SuspendUpdate();
            try
            {
                dteStartDay.EditValue = controller.DisplayStart.Date;
                dteEndDay.EditValue = controller.DisplayEnd.Date;
                tmeStart.EditValue = new DateTime(controller.DisplayStart.TimeOfDay.Ticks);
                tmeEnd.EditValue = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);

                tmeStart.Visible = !controller.AllDay;
                tmeEnd.Visible = !controller.AllDay;
                tmeStart.Enabled = !controller.AllDay;
                tmeEnd.Enabled = !controller.AllDay;
            }
            finally
            {
                ResumeUpdate();
            }
        }
        #endregion
        #region[BTN]
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130125, 130125, new object[] { _prodtype, _prodno, Static.ToInt(resType.EditValue), txtOrderNo.EditValue, txtSalesNo.EditValue, controller.DisplayStart });

                if (res.ResultNo == 0)
                {
                    apt.Delete();
                    Close();
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
        private void btnServDelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            try
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {
                    DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == System.Windows.Forms.DialogResult.No) return;
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130125, 130125, new object[] { 1, Static.ToStr(dr["prodno"]), Static.ToInt(resType.EditValue), txtOrderNo.EditValue, txtSalesNo.EditValue, controller.DisplayStart });
                    if (res.ResultNo == 0)
                    {
                        RefreshServ();
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
        private void btnServFind_Click(object sender, EventArgs e)
        {
            RefreshServList();
            xtraTabPage1.PageVisible = false;
            xtraTabPage2.PageVisible = false;
            xtraTabPage3.PageVisible = true;
            xtraTabControl1.SelectedTabPageIndex = 2;
        }
        private void btnServChooce_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            DataRow dr = gridView2.GetFocusedDataRow();
            if (dr != null)
            {
                object[] obj = new object[8];
                obj[0] = 1;
                obj[1] = Static.ToStr(dr["prodno"]);
                obj[2] = Static.ToInt(resType.EditValue);
                obj[3] = Static.ToStr(txtOrderNo.EditValue);
                obj[4] = Static.ToStr(txtSalesNo.EditValue);
                obj[5] = Static.ToDateTime(this.dteStartDay.DateTime.Date + this.tmeStart.Time.TimeOfDay);
                obj[6] = Static.ToDateTime(this.dteEndDay.DateTime.Date + this.tmeEnd.Time.TimeOfDay);
                string[] labelid = edLabel.Label.MenuCaption.Split('-');
                obj[7] = Static.ToInt(labelid[0]);
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130123, 130123, obj);

                if (res.ResultNo != 0)
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                xtraTabPage1.PageVisible = true;
                xtraTabPage2.PageVisible = true;
                xtraTabPage3.PageVisible = false;
                xtraTabControl1.SelectedTabPageIndex = 1;
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dteStartDay.DateTime.Date <= dteEndDay.DateTime.Date)
            {
                if (this.dteStartDay.DateTime.Date + this.tmeStart.Time.TimeOfDay > this.dteEndDay.DateTime.Date + this.tmeEnd.Time.TimeOfDay)
                {
                    MessageBox.Show("Эхлэх дуусах цаг алдаатай байна.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Эхлэх дуусах огноо алдаатай байна.");
                return;
            }
            if (!controller.IsConflictResolved())
                return;
            controller.Subject = Static.ToStr(txtSalesNo.EditValue);
            controller.Description = Static.ToStr(txtOrderNo.EditValue);
            controller.SetLabel(edLabel.Label);
            //controller.AllDay = this.checkAllDay.Checked;
            controller.DisplayStart = this.dteStartDay.DateTime.Date + this.tmeStart.Time.TimeOfDay;
            controller.DisplayEnd = this.dteEndDay.DateTime.Date + this.tmeEnd.Time.TimeOfDay;

            controller.Custom1 = txtOrderNo.Text;
            controller.ResourceId = resType.EditValue;

            // Save all changes of the editing appointment.
            controller.ApplyChanges();
            EServ.Shared.Result res = new EServ.Shared.Result();

            object[] obj = new object[8];
            obj[0] = _prodtype;
            obj[1] = _prodno;
            obj[2] = Static.ToInt(resType.EditValue);
            obj[3] = Static.ToStr(txtOrderNo.EditValue);
            obj[4] = Static.ToStr(txtSalesNo.EditValue);
            obj[5] = Static.ToDateTime(controller.DisplayStart);
            obj[6] = Static.ToDateTime(controller.DisplayEnd);
            string[] labelid = edLabel.Label.MenuCaption.Split('-');
            obj[7] = Static.ToInt(labelid[0]);
            if (isnew)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130123, 130123, obj);
            }
            else
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130124, 130124, new object[] { OldObj, obj });
            }
            if (res.ResultNo != 0)
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            Close();
        }
        #endregion
        public class MyAppointmentFormController : AppointmentFormController
        {

            public string Custom1 { get { return (string)EditedAppointmentCopy.CustomFields["Custom1"]; } set { EditedAppointmentCopy.CustomFields["Custom1"] = value; } }
            public string Custom2 { get { return (string)EditedAppointmentCopy.CustomFields["Custom2"]; } set { EditedAppointmentCopy.CustomFields["Custom2"] = value; } }

            string SourceCustom1 { get { return (string)SourceAppointment.CustomFields["Custom1"]; } set { SourceAppointment.CustomFields["Custom1"] = value; } }
            string SourceCustom2 { get { return (string)SourceAppointment.CustomFields["Custom2"]; } set { SourceAppointment.CustomFields["Custom2"] = value; } }

            public MyAppointmentFormController(SchedulerControl control, Appointment apt)
                : base(control, apt)
            {
            }
            public override bool IsAppointmentChanged()
            {
                if (base.IsAppointmentChanged())
                    return true;
                return SourceCustom1 != Custom1 ||
                    SourceCustom2 != Custom2;
            }
            protected override void ApplyCustomFieldsValues()
            {
                SourceCustom1 = Custom1;
                SourceCustom2 = Custom2;
            }
        }
        void RefreshServ()
        {
            try
            {
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130501, 130501, new object[] { Static.ToStr(txtOrderNo.EditValue), _prodno });
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    gridControl1.DataSource = res.Data.Tables[0];
                    gridView1.Columns[0].Caption = "Дугаар";
                    gridView1.Columns[1].Caption = "Нэр";
                    gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void RefreshServList()
        {
            try
            {
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130500, 130500, new object[] { _prodno, Static.ToStr(txtOrderNo.EditValue) });
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    gridControl2.DataSource = res.Data.Tables[0];
                    gridView2.Columns[0].Caption = "Төлөв";
                    gridView2.Columns[1].Caption = "Дугаар";
                    gridView2.Columns[2].Caption = "Нэр";
                    gridView2.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
                    gridView2.Columns[1].OptionsColumn.AllowEdit = false;
                    gridView2.Columns[2].OptionsColumn.AllowEdit = false;
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
            ri.AutoHeight = false;
            ri.AllowFocused = false;
            ri.ValueChecked = 1;
            ri.ValueUnchecked = 0;
            ((System.ComponentModel.ISupportInitialize)(ri)).EndInit();
            ri.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(ri_QueryCheckStateByValue);
            return ri;
        }
        private void xtraTabControl1_Selecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            if (e.PageIndex == 1)
            {
                RefreshServ();
            }
        }

        private void xtraTabControl1_Deselecting(object sender, DevExpress.XtraTab.TabPageCancelEventArgs e)
        {
            try
            {
                if (txtSalesNo.EditValue == null || txtSalesNo.Text == "")
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(e.RowHandle);
            if (dr != null)
            {
                if (Static.ToInt(dr["status"]) == 0)
                    dr["status"] = 1;
                else
                    dr["status"] = 0;
            }
        }

        private void tmeStart_Click(object sender, EventArgs e)
        {
            _touchkeyboard.time = tmeStart.Time;
        }

        private void tmeEnd_Click(object sender, EventArgs e)
        {
            _touchkeyboard.time = tmeEnd.Time;
        }
    }
}