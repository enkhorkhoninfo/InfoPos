namespace InfoPos.Schedule
{
    partial class ucSchedule
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraScheduler.Printing.DailyPrintStyle dailyPrintStyle2 = new DevExpress.XtraScheduler.Printing.DailyPrintStyle();
            DevExpress.XtraScheduler.Printing.WeeklyPrintStyle weeklyPrintStyle2 = new DevExpress.XtraScheduler.Printing.WeeklyPrintStyle();
            DevExpress.XtraScheduler.Printing.MonthlyPrintStyle monthlyPrintStyle2 = new DevExpress.XtraScheduler.Printing.MonthlyPrintStyle();
            DevExpress.XtraScheduler.Printing.TriFoldPrintStyle triFoldPrintStyle2 = new DevExpress.XtraScheduler.Printing.TriFoldPrintStyle();
            DevExpress.XtraScheduler.Printing.CalendarDetailsPrintStyle calendarDetailsPrintStyle2 = new DevExpress.XtraScheduler.Printing.CalendarDetailsPrintStyle();
            DevExpress.XtraScheduler.Printing.MemoPrintStyle memoPrintStyle2 = new DevExpress.XtraScheduler.Printing.MemoPrintStyle();
            DevExpress.XtraScheduler.TimeRuler timeRuler3 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler4 = new DevExpress.XtraScheduler.TimeRuler();
            this.schedulerControl1 = new DevExpress.XtraScheduler.SchedulerControl();
            this.schedulerStorage1 = new DevExpress.XtraScheduler.SchedulerStorage();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).BeginInit();
            this.SuspendLayout();
            // 
            // schedulerControl1
            // 
            this.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schedulerControl1.Location = new System.Drawing.Point(0, 0);
            this.schedulerControl1.Name = "schedulerControl1";
            this.schedulerControl1.OptionsCustomization.AllowAppointmentCopy = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.Custom;
            this.schedulerControl1.OptionsCustomization.AllowAppointmentDelete = DevExpress.XtraScheduler.UsedAppointmentType.None;
            dailyPrintStyle2.PrintTime.End = System.TimeSpan.Parse("00:00:00");
            this.schedulerControl1.PrintStyles.Add(dailyPrintStyle2);
            this.schedulerControl1.PrintStyles.Add(weeklyPrintStyle2);
            this.schedulerControl1.PrintStyles.Add(monthlyPrintStyle2);
            this.schedulerControl1.PrintStyles.Add(triFoldPrintStyle2);
            this.schedulerControl1.PrintStyles.Add(calendarDetailsPrintStyle2);
            this.schedulerControl1.PrintStyles.Add(memoPrintStyle2);
            this.schedulerControl1.Size = new System.Drawing.Size(729, 437);
            this.schedulerControl1.Start = new System.DateTime(2012, 6, 24, 0, 0, 0, 0);
            this.schedulerControl1.Storage = this.schedulerStorage1;
            this.schedulerControl1.TabIndex = 1;
            this.schedulerControl1.Text = "schedulerControl1";
            this.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler3);
            this.schedulerControl1.Views.MonthView.Enabled = false;
            this.schedulerControl1.Views.TimelineView.TimelineScrollBarVisible = true;
            this.schedulerControl1.Views.WeekView.Enabled = false;
            this.schedulerControl1.Views.WorkWeekView.Enabled = false;
            this.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler4);
            this.schedulerControl1.AppointmentDrop += new DevExpress.XtraScheduler.AppointmentDragEventHandler(this.schedulerControl1_AppointmentDrop);
            this.schedulerControl1.AppointmentResized += new DevExpress.XtraScheduler.AppointmentResizeEventHandler(this.schedulerControl1_AppointmentResized);
            this.schedulerControl1.PopupMenuShowing += new DevExpress.XtraScheduler.PopupMenuShowingEventHandler(this.schedulerControl1_PopupMenuShowing);
            this.schedulerControl1.EditAppointmentFormShowing += new DevExpress.XtraScheduler.AppointmentFormEventHandler(this.schedulerControl1_EditAppointmentFormShowing);
            this.schedulerControl1.Click += new System.EventHandler(this.schedulerControl1_Click);
            // 
            // ucSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.schedulerControl1);
            this.Name = "ucSchedule";
            this.Size = new System.Drawing.Size(729, 437);
            ((System.ComponentModel.ISupportInitialize)(this.schedulerControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraScheduler.SchedulerControl schedulerControl1;
        private DevExpress.XtraScheduler.SchedulerStorage schedulerStorage1;
    }
}
