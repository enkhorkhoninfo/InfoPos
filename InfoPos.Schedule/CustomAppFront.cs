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

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Schedule
{
    public partial class CustomAppFront : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        SchedulerControl control;
        Appointment apt;
        Core.Core _core;
        bool openRecurrenceForm = false;
        int suspendUpdateCount;
        string _orderno;
        string _prodno;
        int _prodtype;
        bool isnew = true;
        object[] OldObj = new object[8];
        MyAppointmentFormController controller;
        protected AppointmentStorage Appointments
        {
            get { return control.Storage.Appointments; }
        }
        protected bool IsUpdateSuspended { get { return suspendUpdateCount > 0; } }
        #endregion
        #region[Contructure]
        
        public CustomAppFront(Core.Core core, SchedulerControl control, Appointment apt, bool openRecurrenceForm, string orderno, string prodno, int prodtype)
        {
            this.openRecurrenceForm = openRecurrenceForm;
            this.controller = new MyAppointmentFormController(control, apt);
            this.apt = apt;
            this.control = control;
            _core = core;
            _prodno = prodno;
            _prodtype = prodtype;
            _orderno = orderno;
            SuspendUpdate();
            InitializeComponent();
            ResumeUpdate();
            UpdateForm();
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
            if (!IsUpdateSuspended)
                UpdateAppointmentStatus();

            UpdateIntervalControls();
        }
        private void CustomApp_Load(object sender, EventArgs e)
        {
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
            OldObj[4] = Static.ToLong(txtSalesNo.EditValue);
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
                edStatus.Status = Appointments.Statuses[controller.StatusId];
                txtOrderNo.Text = controller.Description;
                edLabel.Label = Appointments.Labels[controller.LabelId];

                dteStartDay.DateTime = controller.DisplayStart.Date;
                dteEndDay.DateTime = controller.DisplayEnd.Date;

                tmeStart.Time = new DateTime(controller.DisplayStart.TimeOfDay.Ticks);
                tmeEnd.Time = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);
                checkAllDay.Checked = controller.AllDay;

                edStatus.Storage = control.Storage;
                edLabel.Storage = control.Storage;

                txtCustom2.Text = controller.Custom2;
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
        void UpdateAppointmentStatus()
        {
            AppointmentStatus currentStatus = edStatus.Status;
            AppointmentStatus newStatus = controller.UpdateAppointmentStatus(currentStatus);
            if (newStatus != currentStatus)
                edStatus.Status = newStatus;
        }
        #endregion
        #region[BTN]
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
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
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            // Required to check the appointment for conflicts.
            if (!controller.IsConflictResolved())
                return;

            controller.Subject = Static.ToStr(txtSalesNo.EditValue);
            controller.SetStatus(edStatus.Status);
            controller.Description = Static.ToStr(txtOrderNo.EditValue);
            controller.SetLabel(edLabel.Label);
            //controller.AllDay = this.checkAllDay.Checked;
            controller.DisplayStart = this.dteStartDay.DateTime.Date + this.tmeStart.Time.TimeOfDay;
            controller.DisplayEnd = this.dteEndDay.DateTime.Date + this.tmeEnd.Time.TimeOfDay;
            controller.Custom1 = txtOrderNo.Text;
            controller.Custom2 = txtCustom2.Text;
            controller.ResourceId = resType.EditValue;

            // Save all changes of the editing appointment.
            controller.ApplyChanges();
            EServ.Shared.Result res = new EServ.Shared.Result();

            object[] obj = new object[8];
            obj[0] = _prodtype;
            obj[1] = _prodno;
            obj[2] = Static.ToInt(resType.EditValue);
            obj[3] = Static.ToStr(txtOrderNo.EditValue);
            obj[4] = Static.ToLong(txtSalesNo.EditValue);
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
    }
} 