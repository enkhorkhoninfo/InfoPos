using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraScheduler;

using EServ.Shared;
namespace InfoPos.schd
{
    public partial class ucSchedule : UserControl
    {
        #region[Variables]
        bool isViewInit = false;
        #endregion
        
        #region[Contructure]
        public ucSchedule()
        {
            InitializeComponent();
            schedulerStorage1.Appointments.Labels.Clear();
        }
        #endregion

        #region[Properties]
        InfoPos.Core.Core _core = null;
        public InfoPos.Core.Core core
        {
            get { return _core; }
            set { _core = value; }
        }

        string _OrderNo = "";
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }

        string _CustName = "";
        public string CustName
        {
            get { return _CustName; }
            set { _CustName = value; }
        }
        string _ProdNo = "";
        public string ProdNo
        {
            get { return _ProdNo; }
            set { _ProdNo = value; }
        }

        int _ProdType;
        public int ProdType
        {
            get { return _ProdType; }
            set { _ProdType = value; }
        }

        string _Unit = "T";
        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }

        int _Duration = 30;
        public int Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        int _LineNumber = 1;
        public int LineNumber
        {
            get { return _LineNumber; }
            set { _LineNumber = value; }
        }

        string _Text = "";
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        private ISM.Touch.TouchKeyboard _touchkeyboard = null;
        public ISM.Touch.TouchKeyboard TouchKeyboard
        {
            get { return _touchkeyboard; }
            set { _touchkeyboard = value; }
        }
        #endregion

        #region[General Event]
        private void schedulerControl1_EditAppointmentFormShowing(object sender, DevExpress.XtraScheduler.AppointmentFormEventArgs e)
        {
            Appointment apt = e.Appointment;
            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);
            CustomAppFront myForm = new CustomAppFront(_core, (SchedulerControl)sender, apt, openRecurrenceForm, _OrderNo, _ProdNo, _ProdType, _touchkeyboard, CustName);
                try
                {
                    myForm.LookAndFeel.ParentLookAndFeel = schedulerControl1.LookAndFeel;
                    e.DialogResult = myForm.ShowDialog();
                    schedulerControl1.Refresh();
                    e.Handled = true;
                }
                finally
                {
                    myForm.Dispose();
                }         
        }
        private void schedulerControl1_AppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            Result res = new Result();
            object[] OldObj = new object[8];
            OldObj[0] = ProdType;
            OldObj[1] = ProdNo;
            OldObj[2] = Static.ToStr(e.SourceAppointment.ResourceId);
            OldObj[3] = e.SourceAppointment.Description;
            OldObj[4] = e.SourceAppointment.Subject;
            OldObj[5] = e.SourceAppointment.Start;
            OldObj[6] = e.SourceAppointment.End;
            OldObj[7] = e.SourceAppointment.LabelId;

            object[] obj = new object[8];
            obj[0] = ProdType;
            obj[1] = ProdNo;
            obj[2] = Static.ToStr(e.EditedAppointment.ResourceId);
            obj[3] = e.SourceAppointment.Description;
            obj[4] = e.SourceAppointment.Subject;
            obj[5] = e.EditedAppointment.Start;
            obj[6] = e.EditedAppointment.End;
            obj[7] = e.SourceAppointment.LabelId;

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130124, 130124, new object[] { OldObj, obj });
            if (res.ResultNo != 0)
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        private void schedulerControl1_AppointmentResized(object sender, AppointmentResizeEventArgs e)
        {
            bool edit = false;
            Result res = new Result();
            object[] OldObj = new object[8];
            OldObj[0] = ProdType;
            OldObj[1] = ProdNo;
            OldObj[2] = Static.ToStr(e.SourceAppointment.ResourceId);
            OldObj[3] = e.SourceAppointment.Description;
            OldObj[4] = e.SourceAppointment.Subject;
            OldObj[5] = e.SourceAppointment.Start;
            OldObj[6] = e.SourceAppointment.End;
            OldObj[7] = e.SourceAppointment.LabelId;
            object[] obj = new object[8];
            obj[0] = ProdType;
            obj[1] = ProdNo;
            obj[2] = Static.ToStr(e.SourceAppointment.ResourceId);
            obj[3] = e.SourceAppointment.Description;
            obj[4] = e.SourceAppointment.Subject;
            obj[5] = e.EditedAppointment.Start;
            obj[6] = e.EditedAppointment.End;
            obj[7] = e.SourceAppointment.LabelId;

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130124, 130124, new object[] { OldObj, obj });
            if (res.ResultNo != 0)
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        private void schedulerControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.Menu.Id == SchedulerMenuItemId.DefaultMenu)
            {
                e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment).Caption = "Хуваарь оруулах";
                e.Menu.GetMenuItemById(SchedulerMenuItemId.GotoDate).Caption = "Өдөр сонгох";
                e.Menu.GetMenuItemById(SchedulerMenuItemId.GotoToday).Caption = "Өнөөдөр";
                e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAllDayEvent).Caption = "Өдрөөр хуваарь оруулах";
                e.Menu.GetPopupMenuById(SchedulerMenuItemId.SwitchViewMenu).Caption = "Харагдах төлөв солих";
                e.Menu.GetMenuCheckItemById(SchedulerMenuItemId.SwitchToDayView).Caption = "Өдрөөр харах";
                e.Menu.GetMenuCheckItemById(SchedulerMenuItemId.SwitchToTimelineView).Caption = "TimeLine харах";
            }
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleEnable);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleVisible);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate);
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday);
        }
        #endregion

        #region[Function]
        /// <summary>
        /// Schedule жагсаалт авах
        /// </summary>
        /// <param name="ProdType">Бүтээгдэхүүний төрөл ( 0-Бараа,1-Үйлчилгээ).</param>
        /// <param name="ProdNo">Бүтээгдэхүүний дугаар.</param>
        /// <param name="Today">Сонгогдож харагдах өдөр</param>
        /// <param name="StartDate">Эхлэх огноо</param>
        /// <param name="EndDate">Дуусах огноо</param>
        /// <returns></returns>
        public Result RefreshData(int ProdType,string ProdNo,DateTime Today,DateTime StartDate, DateTime EndDate)
        {
            ViewInit();
            EServ.Shared.Result res = new EServ.Shared.Result();
            try
            {
                schedulerStorage1.Appointments.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130121, 130121, new object[] { ProdType, ProdNo, StartDate, EndDate });
                if (res.ResultNo == 0)
                {
                    DataTable dt = new DataTable();
                    dt = res.Data.Tables[0];
                    schedulerStorage1.Appointments.DataSource = dt;
                    schedulerStorage1.Appointments.Mappings.Start = "STARTDATE";
                    schedulerStorage1.Appointments.Mappings.End = "ENDDATE";
                    schedulerStorage1.Appointments.Mappings.ResourceId = "LINENUMBER";
                    schedulerStorage1.Appointments.Mappings.Description = "ORDERNO";
                    schedulerStorage1.Appointments.Mappings.Subject = "SALESNO";
                    schedulerStorage1.Appointments.Mappings.Label = "STATUS";
                    schedulerControl1.GoToDate(Today);
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = "Утга олгох үед алдаа гарлаа./" + ex.Message + "/";
            }
            return res;
        }
        /// <summary>
        /// Schedule жагсаалт авах
        /// </summary>
        /// <param name="ProdType">Бүтээгдэхүүний төрөл ( 0-Бараа,1-Үйлчилгээ).</param>
        /// <param name="ProdNo">Бүтээгдэхүүний дугаар.</param>
        /// <param name="Today">Тухайн өдрийн мэдээлэл татагдана.</param>
        /// <returns></returns>
        public Result RefreshData(int ProdType,string ProdNo,DateTime Today)
        {
            ViewInit();
            EServ.Shared.Result res = new EServ.Shared.Result();
            try
            {
                schedulerStorage1.Appointments.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130121, 130121, new object[] { ProdType, ProdNo, Today, Static.ToDateTime("0001.01.01") });
                if (res.ResultNo == 0)
                {
                    DataTable dt = new DataTable();
                    dt = res.Data.Tables[0];
                    schedulerControl1.Storage.Appointments.DataSource = dt;
                    schedulerControl1.Storage.Appointments.Mappings.Start = "STARTDATE";
                    schedulerControl1.Storage.Appointments.Mappings.End = "ENDDATE";
                    schedulerControl1.Storage.Appointments.Mappings.ResourceId = "LINENUMBER";
                    schedulerControl1.Storage.Appointments.Mappings.Description = "ORDERNO";
                    schedulerControl1.Storage.Appointments.Mappings.Subject = "SALESNO";
                    schedulerControl1.Storage.Appointments.Mappings.Label = "STATUS";
                    schedulerControl1.GoToDate(Today);
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                res.ResultNo = 1;
                res.ResultDesc = "Утга олгох үед алдаа гарлаа.";
            }
            return res;
        }
        /// <summary>
        /// Schedule жагсаалт авах
        /// </summary>
        /// <param name="ProdType">Бүтээгдэхүүний төрөл ( 0-Бараа,1-Үйлчилгээ).</param>
        /// <param name="ProdNo">Бүтээгдэхүүний дугаар.</param>
        /// <param name="Today">Сонгогдож харагдах өдөр</param>
        /// <param name="DayCount">Сонгогдох өдрөөс хэдэн өдрийн өмнөх болон хойшхи мэдээлэл.</param>
        /// <returns></returns>
        public Result RefreshData(int ProdType,string ProdNo,DateTime Today, int DayCount)
        {
            ViewInit();
            EServ.Shared.Result res = new EServ.Shared.Result();
            try
            {
                schedulerStorage1.Appointments.DataSource = null;
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 210, 130121, 130121, new object[] { ProdType, ProdNo, Today, Static.ToDateTime("0001.01.01") });
                if (res.ResultNo == 0)
                {
                    DataTable dt = new DataTable();
                    dt = res.Data.Tables[0];
                    schedulerControl1.Storage.Appointments.DataSource = dt;
                    schedulerControl1.Storage.Appointments.Mappings.Start = "STARTDATE";
                    schedulerControl1.Storage.Appointments.Mappings.End = "ENDDATE";
                    schedulerControl1.Storage.Appointments.Mappings.ResourceId = "LINENUMBER";
                    schedulerControl1.Storage.Appointments.Mappings.Description = "ORDERNO";
                    schedulerControl1.Storage.Appointments.Mappings.Subject = "SALESNO";
                    schedulerControl1.Storage.Appointments.Mappings.Label = "STATUS";
                    schedulerControl1.GoToDate(Today);
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                res.ResultNo = 1;
                res.ResultDesc = "Утга олгох үед алдаа гарлаа.";
            }
            return res;
        }
        private void ViewInit()
        {
            try
            {
                if (!isViewInit)
                {
                    schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline;
                    schedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource;
                    DevExpress.XtraScheduler.TimeScaleWeek timeScaleWeek1 = new DevExpress.XtraScheduler.TimeScaleWeek();
                    if (Unit == "T")
                    {
                        DevExpress.XtraScheduler.TimeScaleFixedInterval timeScaleFixedInterval1 = new DevExpress.XtraScheduler.TimeScaleFixedInterval();
                        schedulerControl1.Views.TimelineView.Scales.Add(timeScaleFixedInterval1);
                        if (Duration >= 60)
                        {
                            int hour = Duration / 60;
                            int uldegdel = 0;
                            uldegdel = Duration % 60;
                            if (uldegdel == 0)
                            {
                                timeScaleFixedInterval1.Value = System.TimeSpan.Parse(string.Format("{0}:00:00", hour));
                                timeScaleFixedInterval1.DisplayFormat = "";
                            }
                            else
                            {
                                Duration = Duration - 60;
                                timeScaleFixedInterval1.Value = System.TimeSpan.Parse(string.Format("{0}:00:00", hour, Duration));
                            }

                        }
                        else
                        {
                            timeScaleFixedInterval1.Value = System.TimeSpan.Parse(string.Format("00:{0}:00", Duration));
                            timeScaleFixedInterval1.Width = 100;
                        }
                    }
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("NAME", typeof(string));
                    for (int i = 1; i <= LineNumber; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ID"] = i;
                        dr["NAME"] = i;
                        dt.Rows.Add(dr);
                    }
                    schedulerStorage1.Resources.Mappings.Id = "ID";
                    schedulerStorage1.Resources.Mappings.Caption = "NAME";
                    schedulerStorage1.Resources.DataSource = dt;
                    schedulerControl1.GoToToday();
                }
                isViewInit = true;
            }
            catch(Exception ex)
            {
            }
        }
        /// <summary>
        /// Төлөв оруулах 0 ээс эхэлж дугаарлана уу.
        /// </summary>
        /// <param name="ID">Дугаар</param>
        /// <param name="Caption">Нэр</param>
        /// <param name="Color">Өнгө</param>
        public void StatusAdd(int ID, string Caption, Color Color)
        {
            schedulerStorage1.Appointments.Labels.Add(Color, Caption, string.Format("{0}-{1}", ID, Caption));
        }
        /// <summary>
        /// Өдөр сонгох
        /// </summary>
        /// <param name="Date">Сонгох өдөр</param>
        public void GoToDate(DateTime Date)
        {
            GoToDate(Date);
        }
        #endregion   
    }
}
