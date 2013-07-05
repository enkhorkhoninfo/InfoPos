using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EServ.Shared;
using Outlook = Microsoft.Office.Interop.Outlook;

using InfoPos.EOD;
using InfoPos.Core;

namespace InfoPos.EOD
{
    public partial class IPOSEODProcess : DevExpress.XtraEditors.XtraForm
    {
        #region [ Variables ]
        enum eEODStatus
        { 
            eEODStatus_New = 0,
            eEODStatus_Err = 1,
            eEODStatus_Done = 9,
        }

        Core.Core _core;
        string appname = "";
        string formname = "";
        Form FormName = null;
        DataTable DT = null;
        #endregion
        #region [ Init ]
        public IPOSEODProcess(Core.Core core)
        {
            _core = core;
            _core.EventProgressUpdate += new Core.Core.dlgProgressUpdate(_core_EventProgressUpdate);
            _core.EventDateChanged += new Core.Core.dlgServerDateChanged(_core_EventDateChanged);
            InitializeComponent();
        }
        void _core_EventDateChanged(DateTime TxnDate)
        {
            txtTxnDate.Text = Static.ToStr(TxnDate.Year) + "." + Static.ToStr(TxnDate.Month).PadLeft(2, '0') + "." + Static.ToStr(TxnDate.Day).PadLeft(2, '0');
        }
        private void IPOSEODProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            _core.EODUpdate -= new Core.Core.dlgEODUpdate(_core_EventProgressUpdate);
        }
        private void IPOSEODProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            ISM.Template.FormUtility.SaveStateGrid(appname, formname, ref gvwEODProcess);
        }
        void _core_EventProgressUpdate(string Func, int ProcessNo, int Status, string ErrMessage, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                this.BeginInvoke(new Core.Core.dlgGLProcessUpdate(ProcessUpdateSafe)
                    , Func, ProcessNo, Status, ErrMessage, StartDate, EndDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        void ProcessUpdateSafe(string Func, int ProcessNo, int Status, string ErrMessage, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                foreach (DataRow Row in DT.Rows)
                {
                    if (Static.ToInt(Row[1]) == ProcessNo)
                    {
                        Row[2] = Func;
                        Row[4] = StartDate;
                        Row[5] = EndDate;
                        Row[6] = Status;
                        Row[7] = ErrMessage;
                    }
                }
                grdEODProcess.DataSource = null;
                grdEODProcess.DataSource = DT;
                ControlEdit(6);
                SetColumnCaption();
                SetValue(DT);
            }
            catch
            { }
        }
        private void IPOSEODProcess_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Process." + this.Name;
            FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            txtTxnDate.Text = Static.ToStr(_core.TxnDate.Year) + "." + Static.ToStr(_core.TxnDate.Month).PadLeft(2, '0') + "." + Static.ToStr(_core.TxnDate.Day).PadLeft(2, '0');
            InitImage();
            LoadEODProcess();
        }
        void InitImage()
        {
            GLImageList.Images.Add(_core.Resource.GetImage("gl_ok"));
            GLImageList.Images.Add(_core.Resource.GetImage("gl_error"));
            GLImageList.Images.Add(_core.Resource.GetImage("gl_wait"));

            btnProcessStart.Image = _core.Resource.GetImage("gl_start");
            btnEmail.Image = _core.Resource.GetImage("menu_newmail");
        }
        void LoadEODProcess()
        {
            Result res = new Result();

            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 227, 100004, 100004, null);

            if (res.ResultNo == 0)
            {
                if (res.Data != null)
                {
                    DT = res.Data.Tables[0];
                    grdEODProcess.DataSource = DT;
                    ControlEdit(6);
                    SetColumnCaption();
                    SetValue(DT);
                }
            }
            else
                MessageBox.Show(res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void ControlEdit(int index)
        {
            gvwEODProcess.Columns[index].ColumnEdit = cboImageEdit;
            gvwEODProcess.Columns[index].OptionsColumn.AllowEdit = false;
        }
        void SetColumnCaption()
        {
            gvwEODProcess.Columns[0].Caption = "Гүйлгээний огноо";
            gvwEODProcess.Columns[0].Visible = false;
            gvwEODProcess.Columns[1].Caption = "Дугаар";
            gvwEODProcess.Columns[2].Caption = "Функц";
            gvwEODProcess.Columns[2].Visible = false;
            gvwEODProcess.Columns[3].Caption = "Процессийн нэр";
            gvwEODProcess.Columns[4].Caption = "Эхэлсэн цаг";
            gvwEODProcess.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gvwEODProcess.Columns[4].DisplayFormat.FormatString = "MM/dd/yyyy hh:mm:ss";
            gvwEODProcess.Columns[5].Caption = "Дууссан цаг";
            gvwEODProcess.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gvwEODProcess.Columns[5].DisplayFormat.FormatString = "MM/dd/yyyy hh:mm:ss";
            gvwEODProcess.Columns[6].Caption = "Төлөв";
            gvwEODProcess.Columns[6].VisibleIndex = 0;
            gvwEODProcess.Columns[7].Caption = "Тайлбар";
            gvwEODProcess.Columns[8].Caption = "Өдөр өндөрлөлтийн төрөл";
            gvwEODProcess.Columns[8].Visible = false;
            ISM.Template.FormUtility.SetFormatGrid(ref gvwEODProcess, false);
            ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwEODProcess);
        }
        void SetValue(DataTable DTable)
        {
            int count = 0;
            foreach (DataRow DRow in DTable.Rows)
            {
                if (DRow[6].ToString().Trim() == "1")
                {
                    //SetGGSValue(Convert.ToInt32(DRow[1]), true);
                }
                else
                {
                    if (DRow[6].ToString().Trim() == "9")
                    {
                        count++;
                    }
                }
            }
            if (count == DTable.Rows.Count)
            {
                //SetGGSValue(11, false);
                btnProcessStart.Enabled = false;
            }
        }
        #endregion
        #region [ Functions ]
        private void btnProcessStart_Click(object sender, EventArgs e)
        {
            Result res = new Result();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 227, 100005, 100005, null);

            if (res.ResultNo == 0)
            {
                MessageBox.Show(res.ResultDesc, "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnProcessStart.Enabled = false;
            }
            else
                MessageBox.Show(res.ResultDesc, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnEmail_Click(object sender, EventArgs e)
        {
            OutlookMail();
        }
        void OutlookMail()
        {
            try
            {
                DateTime DTime = DateTime.Now;
                gvwEODProcess.ExportToExcelOld(_core.ReportPathOut + "\\EODMail" + DTime.Hour + DTime.Minute + DTime.Second + ".xls");

                Outlook.Application oApp = new Outlook.Application();
                Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

                oMsg.Subject = "Өдөр өндөрлөлтийн процесс";
                oMsg.Body = _core.TxnDate.ToShortDateString() + "-н өдөр өндөрлөлтийн процессийн тайлан .";
                String sSource = _core.ReportPathOut + "\\EODMail" + DTime.Hour + DTime.Minute + DTime.Second + ".xls";
                String sDisplayName = "Хавсралт файл";
                int iPosition = 1;
                int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                Outlook.Attachment oAttach = oMsg.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);
                oMsg.Display(true);

                oAttach = null;
                oMsg = null;
                oApp = null;
            }
            catch
            {
                MessageBox.Show("Алдаа гарлаа .");
            }
        }
        #endregion
    }
}