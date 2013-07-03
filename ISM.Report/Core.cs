using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ISM.Report
{
    public class Core
    {
        #region Properties

        #endregion
        #region Constractor

        public Core()
        {
        }

        #endregion
        #region Internal Methods

        internal void SetDataSource(ReportDocument rd, DataSet tables)
        {
            for (int i = 0; i < rd.Database.Tables.Count; i++)
            {
                string tablename = rd.Database.Tables[i].Name;
                if (tables.Tables.Contains(tablename))
                {
                    rd.Database.Tables[i].SetDataSource(tables.Tables[tablename]);
                }
            }
        }
        internal int Load(string rptfile, DataTable table, ref ReportDocument rd, ref string error)
        {
            int success = 0;

            if (string.IsNullOrEmpty(rptfile))
            {
                success = 20;
                error = "Empty report file name.";
                return success;
            }
            if (table == null)
            {
                success = 21;
                error = "Empty data source.";
                return success;
            }

            try
            {
                if (File.Exists(rptfile))
                {
                    if (!rd.IsLoaded)
                    {
                        //rd.Load(rptfile, OpenReportMethod.OpenReportByTempCopy);
                        rd.Load(rptfile);
                    }
                    rd.SetDataSource(table);
                }
                else
                {
                    success = 22;
                    error = "Not found specified report file.";
                }
            }
            catch (Exception ex)
            {
                success = 29;
                error = ex.Message;
            }
            return success;
        }
        internal int Load(string rptfile, DataSet tables, ref ReportDocument rd, ref string error)
        {
            int success = 0;

            if (string.IsNullOrEmpty(rptfile))
            {
                success = 20;
                error = "Empty report file name.";
                return success;
            }
            if (tables == null)
            {
                success = 21;
                error = "Empty data source.";
                return success;
            }

            try
            {
                if (File.Exists(rptfile))
                {
                    if (!rd.IsLoaded)
                    {
                        //rd.Load(rptfile, OpenReportMethod.OpenReportByTempCopy);
                        rd.Load(rptfile);
                    }
                    if (tables != null)
                    {
                        if (tables.Tables.Count > 0) rd.SetDataSource(tables.Tables[0]);
                        foreach (DataTable table in tables.Tables)
                        {
                            for (int i = 0; i < rd.Subreports.Count; i++)
                            {
                                rd.Subreports[i].SetDataSource(tables);
                            }
                        }
                    }
                }
                else
                {
                    success = 22;
                    error = "Not found specified report file.";
                }
            }
            catch (Exception ex)
            {
                success = 29;
                error = ex.Message;
            }
            return success;
        }
        
        #endregion
        #region Core Methods

        public ExportFormatType GetExportType(string outfiletype, ref string extention)
        {
            ExportFormatType type = ExportFormatType.PortableDocFormat;
            #region Checking export type
            switch (outfiletype.ToUpper())
            {
                case "PDF":
                    type = ExportFormatType.PortableDocFormat;
                    extention = "pdf";
                    break;
                case "DOC":
                    type = ExportFormatType.WordForWindows;
                    extention = "doc";
                    break;
                case "XLS":
                    type = ExportFormatType.Excel;
                    extention = "xls";
                    CrystalDecisions.CrystalReports.Engine.Border BORDER;

                    break;
                case "XLS1":
                    type = ExportFormatType.ExcelRecord;
                    extention = "xls";
                    break;
                case "RTF":
                    type = ExportFormatType.RichText;
                    extention = "rtf";
                    break;
                case "HTML":
                    type = ExportFormatType.HTML40;
                    extention = "html";
                    break;
                default:
                    type = ExportFormatType.PortableDocFormat;
                    extention = "pdf";
                    break;
            }
            #endregion
            return type;
        }

        public int ExportToStream(string rptfile, string outfiletype, DataTable table, ref Stream stream, ref string error)
        {
            int success = 0;
            try
            {
                ReportDocument rd = new ReportDocument();
                success = Load(rptfile, table, ref rd, ref error);
                if (success == 0)
                {
                    string extention = "";
                    ExportFormatType type = GetExportType(outfiletype, ref extention);
                    stream = rd.ExportToStream(type);
                    rd.Dispose();
                }
            }
            catch (Exception ex)
            {
                success = 23;
                error = ex.Message;
            }
            return success;
        }
        public int ExportToStream(string rptfile, string outfiletype, DataSet tables, ref Stream stream, ref string error)
        {
            int success = 0;
            try
            {
                ReportDocument rd = new ReportDocument();
                if (tables.Tables.Count == 1)
                {
                    success = Load(rptfile, tables.Tables[0], ref rd, ref error);
                }
                else
                {
                    success = Load(rptfile, tables, ref rd, ref error);
                }
                if (success == 0)
                {
                    string extention = "";
                    ExportFormatType type = GetExportType(outfiletype, ref extention);
                    stream = rd.ExportToStream(type);
                    rd.Dispose();
                }
            }
            catch (Exception ex)
            {
                success = 23;
                error = ex.Message;
            }
            return success;
        }

        public int ExportToFile(string rptfile, string outfiletype, DataTable table, string outfilename, ref string error)
        {
            int success = 0;
            try
            {
                ReportDocument rd = new ReportDocument();
                success = Load(rptfile, table, ref rd, ref error);
                if (success == 0)
                {
                    string extention = "";
                    ExportFormatType type = GetExportType(outfiletype, ref extention);
                    rd.ExportToDisk(type, outfilename);
                    rd.Dispose();
                }
            }
            catch (Exception ex)
            {
                success = 23;
                error = ex.Message;
            }
            return success;
        }
        public int ExportToFile(string rptfile, string outfiletype, DataSet tables, string outfilename, ref string error)
        {
            int success = 0;
            try
            {
                ReportDocument rd = new ReportDocument();
                if (tables.Tables.Count == 1)
                {
                    success = Load(rptfile, tables.Tables[0], ref rd, ref error);
                }
                else
                {
                    success = Load(rptfile, tables, ref rd, ref error);
                }

                if (success == 0)
                {
                    string extention = "";
                    ExportFormatType type = GetExportType(outfiletype, ref extention);

                    rd.ExportToDisk(type, outfilename);

                    rd.Dispose();
                }
            }
            catch (Exception ex)
            {
                success = 23;
                error = ex.Message;
            }
            return success;
        }

        #endregion
    }
}
