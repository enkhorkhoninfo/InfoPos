using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Excel = Microsoft.Office.Interop.Excel;
using NativeExcel;
using EServ.Shared;
namespace InfoPos.bo_Reports
{
    public partial class frmReport : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        Core.Core _core = null;
        Hashtable colindex = new Hashtable();
        #endregion

        #region[Constructure]
        public frmReport(Core.Core core)
        {
            InitializeComponent();
            _core = core;
        }
        private void frmReport_Load(object sender, EventArgs e)
        {
            try
            {
                GeneralTemplateSearch(_core.ReportPathIn);
                UserTemplateSearch(_core.CustReportPathIn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Function]
        private void GeneralTemplateSearch(string DirectoryPath)
        {
            #region[History]
            /*
            string[] filepath = null;
            gridControl1.DataSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportFullName", typeof(string));
            dt.Columns.Add("ReportName", typeof(string));
            try
            {
                if (DirectoryPath != "")
                {
                    filepath = Directory.GetFiles(DirectoryPath, "*.xls");
                    FileInfo fileinfo = null;

                    foreach (string name in filepath)
                    {
                        fileinfo = new FileInfo(name);
                        DataRow dr = dt.NewRow();
                        dr["ReportFullName"] = name;
                        dr["ReportName"] = fileinfo.Name;
                        dt.Rows.Add(dr);
                    }
                }
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Caption = "Тайлангын зам";
                gridView1.Columns[1].Caption = "Тайлангын нэр";
                gridView1.OptionsBehavior.ReadOnly = true;
                gridView1.OptionsBehavior.Editable = false;
             * */
            #endregion
            try
            {
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 503, 249998, 249998, null);
                if(ISM.Template.FormUtility.ValidateQuery(res))
                {
                    gridControl1.DataSource = res.Data.Tables[0];
                    gridView1.Columns[0].Caption = "Тайлангын зам";
                    gridView1.Columns[1].Caption = "Тайлангын нэр";
                    gridView1.OptionsBehavior.ReadOnly = true;
                    gridView1.OptionsBehavior.Editable = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UserTemplateSearch(string DirectoryPath)
        {
            string[] filepath = null;
            gridControl2.DataSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportFullName", typeof(string));
            dt.Columns.Add("ReportName", typeof(string));
            try
            {
                if (DirectoryPath != "")
                {
                    filepath = Directory.GetFiles(DirectoryPath, "*.xls");
                    FileInfo fileinfo = null;

                    foreach (string name in filepath)
                    {
                        fileinfo = new FileInfo(name);
                        DataRow dr = dt.NewRow();
                        dr["ReportFullName"] = name;
                        dr["ReportName"] = fileinfo.Name;
                        dt.Rows.Add(dr);
                    }
                }
                gridControl2.DataSource = dt;
                gridView2.Columns[0].Caption = "Тайлангын зам";
                gridView2.Columns[1].Caption = "Тайлангын нэр";
                gridView2.OptionsBehavior.ReadOnly = true;
                gridView2.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private DataTable DataRefresh(string viewname)
        {
            Result res = new Result();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 502, 1, 1, new object[] { viewname });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                return res.Data.Tables[0];
            }
            return null;
        }
        #endregion

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {

                    frmInputParam frm = new frmInputParam(_core, Static.ToStr(dr["ReportFullName"]));
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                colindex.Clear();
                MessageBox.Show("Тайлангын формат таарахгүй байна: " + ex.Message);
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gridView2.GetFocusedDataRow();
                Excel.Application xlsm = new Excel.Application();
                Excel.Workbook xlsWorkBook = xlsm.Workbooks.Open(@Convert.ToString(dr["ReportFullName"]), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Excel.Worksheet xlsWorkSheet = xlsWorkBook.Worksheets[1];
                for (int i = 1; i <= xlsWorkSheet.UsedRange.Columns.Count; i++)
                {
                    if (!colindex.ContainsKey(xlsWorkSheet.Cells[1, i].Value.ToString()))
                    {
                        colindex.Add(xlsWorkSheet.Cells[1, i].Value.ToString(), i);
                    }
                    else
                    {
                        colindex.Clear();
                        MessageBox.Show(string.Format("{0} талбар өмнө нь үүссэн байна.", xlsWorkSheet.Cells[1, i].Value));
                    }
                }
                frmInputParam frm = new frmInputParam(_core, xlsWorkSheet.Name);
                frm.ShowDialog();
                int rowindex = 2;
                if (frm.ReportData != null)
                {
                    foreach (DataRow row in frm.ReportData.Rows)
                    {
                        foreach (DictionaryEntry fieldname in colindex)
                        {
                            xlsWorkSheet.Cells[rowindex, Convert.ToInt32(fieldname.Value)].Value = row[fieldname.Key.ToString()];
                        }
                        rowindex++;
                    }
                    string path = string.Format("{0}\\Reports{1}.xlsm", _core.ReportPathOut, DateTime.Now.Ticks.ToString());
                    xlsWorkBook.SaveCopyAs(path);
                    System.Diagnostics.Process.Start(path);
                    colindex.Clear();
                }

            }
            catch (Exception ex)
            {
                colindex.Clear();
                MessageBox.Show("Тайлангын формат таарахгүй байна: " + ex.Message);
            }
        }
    }
}