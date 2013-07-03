using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using NativeExcel;

using EServ.Interface;
using EServ.Shared;
using EServ.Data;
using ISM.DB;

namespace ISM.SReport
{
    public class Report : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 107001:
                        r = FN107001(ci, ri, db, ref lg);
                        break;

                    case 107101:
                        r = FN107101(ci, ri, db, ref lg);
                        break;
                    case 107102:
                        r = FN107102(ci, ri, db, ref lg);
                        break;
                    case 107103:
                        r = FN107103(ci, ri, db, ref lg);
                        break;
                    case 107104: // Generate report into stream as excel file.
                        r = FN107104(ci, ri, db, ref lg);
                        break;
                    case 107105: // Generate report into dataset object. Added for Dashboard report by Amaraa on 2011/10/20.
                        r = FN107105(ci, ri, db, ref lg);
                        break;
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }

        #region FN10700x - Dynamic Report

        #region Get User Dynamic Report Data
        public Result FN107001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
    
            try
            {
                string sql = (string)ri.ReceivedParam[0];
                string[] paramnames = (string[])ri.ReceivedParam[1];
                object[] paramvalues = (object[])ri.ReceivedParam[2];
               
 
             //   object[] pParamLevel = (object[])(ri.ReceivedParam[3]);
          
                r = Main.DB107001(db, sql, paramnames, paramvalues);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion

        #endregion
        #region FN10710x - Chart Report for XLS

        #region Chart Report - Internal Functions
        private Hashtable GetCommentHashTable(string src)
        {
            Hashtable ht = new Hashtable(7);
            try
            {
                string[] a = src.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < a.Length; i++)
                {
                    string[] b = a[i].Split('=');
                    if (b.Length > 1)
                    {
                        if (!string.IsNullOrEmpty(b[0])) ht[b[0].ToLower()] = b[1];
                    }
                }
            }
            catch
            { }
            return ht;
        }
        private string GetCommentValue(string src, string key)
        {
            string value = "";
            try
            {
                Hashtable ht = GetCommentHashTable(src);
                if (ht != null)
                {
                    value = (string)ht[key.ToLower()];
                    ht.Clear();
                }
            }
            catch
            { }
            return value;
        }
        private string GetCommentValue(Hashtable src, string key)
        {
            string value = "";
            try
            {
                if (src != null)
                {
                    value = (string)src[key.ToLower()];
                }
            }
            catch
            { }
            return value;
        }
        private string GetCommentValue(Hashtable src, string key, string defaultvalue)
        {
            string value = defaultvalue;
            try
            {
                if (src != null)
                {
                    if (src.ContainsKey(key.ToLower()))
                        value = (string)src[key.ToLower()];
                }
            }
            catch
            { }
            return value;
        }
        private IWorkbook OpenWorkBook(int reportpriv)
        {
            #region Create Instance of WorkBook
            IWorkbook book = null;
            string fullname = string.Format(@"{0}\Reports\R{1}.xls", ISM.Lib.Static.WorkingFolder, reportpriv);
            using (FileStream fs = new FileStream(fullname, FileMode.Open, FileAccess.Read))
            {
                book = Factory.OpenWorkbook(fs);
                fs.Close();
            }
            #endregion
            return book;
        }
        private IWorkbook OpenWorkBook(string reportid)
        {
            #region Create Instance of WorkBook
            IWorkbook book = null;
            string fullname = string.Format(@"{0}\Reports\{1}.xls", ISM.Lib.Static.WorkingFolder, reportid);
            using (FileStream fs = new FileStream(fullname, FileMode.Open, FileAccess.Read))
            {
                book = Factory.OpenWorkbook(fs);
                fs.Close();
            }
            #endregion
            return book;
        }
        private bool CopyWorkBook(IWorksheet src, out DataTable dest)
        {
            bool success = false;
            IRange used = src.UsedRange;
            dest = used.GetDataTable(true, false);
            
            return success;
        }
        private bool CopyWorkBook(IWorksheet src, IWorksheet dest, XlPasteType pastetype)
        {
            bool success = false;
            IRange used = src.UsedRange;
            IRange srcrange = src.Range[1, 1, used.Row + used.Rows.Count, used.Column + used.Columns.Count];
            IRange dstrange = dest.Range[1, 1, used.Row + used.Rows.Count, used.Column + used.Columns.Count];
            srcrange.Copy(dstrange, pastetype);
            return success;
        }
        private bool CopyWorkBook(IWorksheet src, IWorksheet dest)
        {
            return CopyWorkBook(src, dest, XlPasteType.xlPasteAll);
        }
        private bool CopyWorkBookValues(IWorksheet src, IWorksheet dest)
        {
            bool success = false;
            IRange used = src.UsedRange;
            IRange srcrange = src.Range[1, 1, used.Row + used.Rows.Count, used.Column + used.Columns.Count];
            IRange dstrange = dest.Range[1, 1, used.Row + used.Rows.Count, used.Column + used.Columns.Count];

            srcrange.Copy(dstrange);
            for (int row = 1; row <= srcrange.Rows.Count; row++)
            {
                for (int col = 1; col <= srcrange.Columns.Count; col++)
                {
                    dstrange[row, col].Formula = null;
                    dstrange[row, col].Value = srcrange[row, col].Value;
                    dstrange[row, col].ClearComments();
                }
            }

            return success;
        }

        private Result XlsGenerate(DbConnections db, int reportpriv, DateTime reportdate, out IWorkbook outbook)
        {
            Result r = null;
            IWorkbook book = null;
            try
            {
                #region Prepare parameters
                //int reportpriv = (int)param[0];
                //DateTime reportdate = (DateTime)param[1];
                string rid = string.Format("R{0}", reportpriv);
                #endregion
                #region Select sub report list
                r = Main.DB107106(db, rid);
                if (r.ResultNo != 0) goto OnExit;
                #endregion
                #region Create worksheets
                DataTable dt = r.Data.Tables[0];
                book = Factory.CreateWorkbook();
                foreach (DataRow row in dt.Rows)
                {
                    string reportid = (string)row["reportid2"];
                    IWorksheet sheet = book.Worksheets.Add();
                    sheet.Name = reportid;

                    IWorkbook srcbook = OpenWorkBook(reportid);
                    if (srcbook == null)
                    {
                        r = new Result(9, string.Format("Could not openned report file. Report id = {0}", reportid));
                        goto OnExit;
                    }
                    if (srcbook.Worksheets.Count <= 0)
                    {
                        r = new Result(9, string.Format("There is no sheets in report file. Report id = {0}", reportid));
                        goto OnExit;
                    }
                    CopyWorkBook(srcbook.Worksheets[1], sheet);
                }
                dt.Dispose();
                #endregion
                #region Select report data
                DataTable table = null;
                r = Main.DB107107(db, rid, reportdate, out table);
                if (r.ResultNo != 0) goto OnExit;

                #region FOR DEBIGING
                //foreach (DataRow row in r.Data.Tables[0].Rows)
                //{
                //    int rowno = ISM.Lib.Static.ToInt(row["rowno"]);
                //    int colno = ISM.Lib.Static.ToInt(row["colno"]);
                //    string value = ISM.Lib.Static.ToStr(row["value"]);
                //    EServ.Interface.Console.Print(string.Format("DB: cell={0}:{1} val={2}", rowno, colno, value), EServ.Interface.Console.enumConsoleLogType.Main);
                //}
                #endregion

                #endregion
                #region Set db values to cells
                foreach (DataRow row in table.Rows)
                {
                    string reportid = ISM.Lib.Static.ToStr(row["reportid"]);
                    int rowno = ISM.Lib.Static.ToInt(row["rowno"]);
                    int colno = ISM.Lib.Static.ToInt(row["colno"]);
                    string value = ISM.Lib.Static.ToStr(row["value"]);

                    IWorksheet sheet = book.Worksheets[reportid];
                    if (sheet == null)
                    {
                        sheet = book.Worksheets.Add();
                    }
                    sheet.Cells[rowno, colno].Value = null;
                    sheet.Cells[rowno, colno].Formula = string.Format("={0}", value);
                    
                    #region FOR DEBIGING
                    //EServ.Interface.Console.Print(string.Format("RP: cell={0}:{1} val={2}", rowno, colno, value), EServ.Interface.Console.enumConsoleLogType.Main);
                    #endregion
                }
                table.Dispose();
                #endregion
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
        OnExit:
            outbook = book;
            return r;
        }
        #endregion
        #region Chart Report - Get XLS Report List
        public Result FN107101(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                string path = string.Format(@"{0}\Reports", ISM.Lib.Static.WorkingFolder);
                string[] files = Directory.GetFiles(path, "*.xls", SearchOption.TopDirectoryOnly);

                DataTable dt = new DataTable();
                dt.Columns.Add("ReportId", typeof(string));
                dt.Columns.Add("PrivCode", typeof(int));
                dt.Columns.Add("Description", typeof(string));

                foreach (string s in files)
                {
                    string name = "[no privilege]";
                    int trancode = 0;
                    Match match = Regex.Match(s, @"\\R(\d+)\.xls$", RegexOptions.IgnoreCase);
                    if (match.Groups.Count >= 2)
                    {
                        trancode = ISM.Lib.Static.ToInt(match.Groups[1].Value);
                    }

                    r = Main.DB107103(db, trancode);
                    if (r.ResultNo == 0 && r.AffectedRows > 0) name = (string)r.Data.Tables[0].Rows[0]["name"];

                    dt.Rows.Add(string.Format("R{0}", trancode), trancode, name);
                }

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                r = new Result(0, "", ds);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion
        #region Chart Report - Save XLS Report
        public Result FN107102(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {//Done. 2011/07/05
            Result r = null;
            try
            {
                #region Prepare parameters
                int reportpriv = (int)ri.ReceivedParam[0];
                string xlsname = (string)ri.ReceivedParam[1];
                byte[] xlsbody = (byte[])ri.ReceivedParam[2];
                string fullname = string.Format(@"{0}\Reports\R{1}.xls", ISM.Lib.Static.WorkingFolder, reportpriv);
                #endregion
                #region Check file is existing
                if (File.Exists(fullname))
                {
                    r = new Result(9, string.Format("Report file is already existing. \r\n[{0}]", fullname));
                    return r;
                }
                #endregion
                #region Save xls file
                using (FileStream fs = new FileStream(fullname, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(xlsbody, 0, xlsbody.Length);
                    fs.Flush();
                    fs.Close();
                }
                #endregion
                #region Analyze the report
                r = Analyze(db, reportpriv);
                #endregion
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion
        #region Chart Report - Analyze XLS Report
        private Result Analyze(DbConnections db, int reportpriv)
        {//Done. 2011/07/06
            Result r = null;
            DbConnection conn = null;
            try
            {
                string rid = string.Format("R{0}", reportpriv);
                #region Create Instance of WorkBook
                IWorkbook book = OpenWorkBook(reportpriv);
                if (book == null)
                {
                    r = new Result(9, string.Format("Report file is not openned! Report id = {0}", rid));
                    return r;
                }
                #endregion
                #region Declare Bind Arrays
                List<string> rids = new List<string>();
                List<int> rows = new List<int>();
                List<int> cols = new List<int>();
                List<int> types = new List<int>();
                List<string> vals = new List<string>();
                List<string> dtypes = new List<string>();
                List<int> dvalues = new List<int>();
                List<string> branches = new List<string>();
                List<string> curs = new List<string>();
                List<string> tocurs = new List<string>();
                List<int> rounds = new List<int>();

                List<string> rids1 = new List<string>();
                List<string> rids2 = new List<string>();
                List<DateTime> chgd = new List<DateTime>();
                #endregion
                #region Loop for each cell
                Hashtable nodes = new Hashtable(); // include report names of depending sub reports
                for (int row = 1; row <= book.Worksheets[1].UsedRange.Rows.Count; row++)
                {
                    for (int col = 1; col <= book.Worksheets[1].UsedRange.Columns.Count; col++)
                    {
                        #region Keep cell parameters
                        string commFormula = "";
                        string commTermType = "";
                        string commTermValue = "";
                        string commBranch = "";
                        string commFromCur = "";
                        string commToCur = "";
                        string commRound = "";

                        IComment comment = book.Worksheets[1].UsedRange[row, col].Comment;
                        if (comment != null)
                        {
                            Hashtable ht = GetCommentHashTable(comment.Text);
                            commFormula = GetCommentValue(ht, "Formula", "N");
                            commTermType = GetCommentValue(ht, "TermType", "M");
                            commTermValue = GetCommentValue(ht, "TermValue", "0");
                            commBranch = GetCommentValue(ht, "Branch", "");
                            commFromCur = GetCommentValue(ht, "FromCurrency", "MNT");
                            commToCur = GetCommentValue(ht, "ToCurrency", "MNT");
                            commRound = GetCommentValue(ht, "Round", "1");
                        }
                        if (commFormula == "Y" || commFormula == "y")
                        {
                            string cellvalue = Convert.ToString(book.Worksheets[1].UsedRange[row, col].Value);
                            //Match match = Regex.Match(cellvalue, "(^|[-+%*/ ])([YQMD][ODCB][EY][0-9]+)($?)");
                            //if (match.Success)
                            if (!string.IsNullOrEmpty(cellvalue))
                            {
                                rids.Add(rid);
                                rows.Add(book.Worksheets[1].UsedRange.Row + row - 1);
                                cols.Add(book.Worksheets[1].UsedRange.Column + col - 1);
                                types.Add(1);
                                vals.Add(cellvalue);
                                dtypes.Add(commTermType);
                                dvalues.Add(ISM.Lib.Static.ToInt(commTermValue));
                                branches.Add(commBranch);
                                curs.Add(commFromCur);
                                tocurs.Add(commToCur);
                                rounds.Add(ISM.Lib.Static.ToInt(commRound));
                            }

                            #region Keep sub reports
                            Regex re = new Regex(@"([A-Z0-9]+)![A-Z]+\d+", RegexOptions.IgnoreCase);
                            MatchCollection mc = re.Matches(cellvalue, 0);

                            DateTime now = DateTime.Now;
                            foreach (Match m in mc)
                            {
                                if (!nodes.ContainsKey(m.Groups[1].Value))
                                {
                                    nodes.Add(m.Groups[1].Value, null);
                                    rids1.Add(rid);
                                    rids2.Add(m.Groups[1].Value);
                                    chgd.Add(now);
                                }
                            }
                            #endregion

                        }
                        #endregion
                    }
                }
                #endregion
                #region Save formula cells into db

                #region Begin Db transaction
                conn = db.BeginTransaction("core", "Chart Report Update");
                #endregion
                #region Delete old records
                r = Main.DB107102(db, rid);
                if (r == null || r.ResultNo != 0) return r;
                #endregion
                #region Insert Cell values
                if (rids.Count > 0)
                {
                    r = Main.DB107101(db
                        , rids.ToArray()
                        , rows.ToArray()
                        , cols.ToArray()
                        , types.ToArray()
                        , vals.ToArray()
                        , dtypes.ToArray()
                        , dvalues.ToArray()
                        , branches.ToArray()
                        , curs.ToArray()
                        , tocurs.ToArray()
                        , rounds.ToArray()
                        );
                }
                #endregion

                #endregion
                #region Save sub reports into db

                #region Begin Db transaction
                conn = db.BeginTransaction("core", "Chart Report Update");
                #endregion
                #region Delete old records
                r = Main.DB107105(db, rid);
                if (r == null || r.ResultNo != 0) return r;
                #endregion
                #region Insert sub report names
                if (rids1.Count > 0)
                {
                    r = Main.DB107104(db
                        , rids1.ToArray()
                        , rids2.ToArray()
                        , chgd.ToArray()
                        );
                }
                #endregion

                #endregion
                r.Data = null;
                r.Param = null;
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            finally
            {
                if (conn != null)
                    if (r != null && r.ResultNo == 0) conn.Commit();
                    else conn.Rollback();
            }
            return r;
        }
        public Result FN107103(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {//Done. 2011/07/06
            Result r = null;
            try
            {
                int reportpriv = (int)ri.ReceivedParam[0];
                r = Analyze(db, reportpriv);
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        #endregion
        #region Chart Report - Generate to XLS Report
        public Result FN107104(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                #region Prepare parameters
                int reportpriv = (int)ri.ReceivedParam[0];
                DateTime reportdate = (DateTime)ri.ReceivedParam[1];
                string rid = string.Format("R{0}", reportpriv);
                #endregion

                #region Create new worksheet for returning

                IWorkbook book = null;
                r = XlsGenerate(db, reportpriv, reportdate, out book);
                if (r == null || r.ResultNo != 0)
                {
                    goto OnExit;
                }

                IWorksheet ridsheet = book.Worksheets[rid];
                if (ridsheet == null)
                {
                    r = new Result(9, string.Format("Could not openned report file. Report id = {0}", rid));
                    goto OnExit;
                }

                IWorkbook retbook = Factory.CreateWorkbook();
                retbook.Worksheets.Add();
                retbook.Worksheets[1].Name = rid;
                CopyWorkBookValues(ridsheet, retbook.Worksheets[1]);
                
                #endregion
                #region Save xls sheet into buffer
                byte[] buffer = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    retbook.SaveAs(ms, XlFileFormat.xlExcel97);
                    buffer = ms.ToArray();
                    ms.Close();
                }
                r = new Result();
                r.Param = new object[] { buffer };
                #endregion
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
        OnExit:
            return r;
        }
        #endregion
        #region Chart Report - Generate to Dataset
        public Result FN107105(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result r = null;
            try
            {
                #region Prepare parameters
                int reportpriv = (int)ri.ReceivedParam[0];
                DateTime reportdate = (DateTime)ri.ReceivedParam[1];
                string rid = string.Format("R{0}", reportpriv);
                #endregion

                #region Create new worksheet for returning

                IWorkbook book = null;
                r = XlsGenerate(db, reportpriv, reportdate, out book);
                if (r == null || r.ResultNo != 0)
                {
                    goto OnExit;
                }

                IWorksheet ridsheet = book.Worksheets[rid];
                if (ridsheet == null)
                {
                    r = new Result(9, string.Format("Could not openned report file. Report id = {0}", rid));
                    goto OnExit;
                }

                DataTable dt = null;
                CopyWorkBook(ridsheet, out dt);
                if (dt == null)
                {
                    r = new Result(9, string.Format("Worksheet could not converted into dataset. Report id = {0}", rid));
                    goto OnExit;
                }

                r = new Result();
                r.Data = new DataSet();
                r.Data.Tables.Add(dt);

                #endregion
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
        OnExit:
            return r;
        }
        #endregion

        #endregion
    }
}
