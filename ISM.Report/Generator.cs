using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Reflection;
using EServ;
using EServ.Shared;

namespace ISM.Report
{
    public class Generator
    {
        HeavenPro.Core.Core _core;
        /************* Error codes and descriptions **************
         * 10 - Ini file not found.
         * 20 - Empty report file name.
         * 21 - Empty data source.
         * 22 - Not found specified report file.
         * 23 - Export error.
         * 24 - User cancel the report.
         * 29 - Other error.
         * 30 - Report ini file not found.
         * 31 - Report is not prepared.
         ********************************************************/
        public Generator(HeavenPro.Core.Core core)
        {

            _core = core;



            //   BacAccount.ActiveForm.AcceptButton = ucBacAccountList.btnFind;


        }
        public Generator()
        {




            //   BacAccount.ActiveForm.AcceptButton = ucBacAccountList.btnFind;


        }
        #region Internal Varibales

        Hashtable tables = new Hashtable();
        int reportpriv = 0;

        #endregion
        #region Public Properties

        EServ.Client client = null;
        public EServ.Client Client
        {
            get { return client; }
            set { client = value; }
        }

        int userno = 0;
        public int UserNo
        {
            get { return userno; }
            set { userno = value; }
        }

        List<ParameterItem> parameters = new List<ParameterItem>();
        public List<ParameterItem> Parameters
        {
            get { return parameters; }
        }

        public int TableCount
        {
            get { return tables.Count; }
        }
        public int ParameterCount
        {
            get { return parameters.Count; }
        }

        string reportname = "";
        public string ReportName
        {
            get { return reportname; }
            set { reportname = value; }
        }

        string reportpath = "";
        public string ReportPath
        {
            get { return reportpath; }
            set { reportpath = value; }
        }

        string reportoutpath = "";
        public string ReportOutPath
        {
            get { return reportoutpath; }
            set { reportoutpath = value; }
        }

        string reportFormTitle = "";
        public string ReportFormTitle
        {
            get { return reportFormTitle; }
            set { reportFormTitle = value; }
        }

        bool prepared = false;
        public bool Prepared
        {
            get { return prepared; }
        }

        #endregion
        #region Internal Functions
        internal int LoadIniFile(string filename, ref string error)
        {
            bool success = false;
            int status = 0;
            try
            {
                #region Reading Ini File
                if (!File.Exists(filename)) return 30; // Report ini file not found.

                ISM.Lib.IniFile ini = new Lib.IniFile();
                status = ini.Read(filename);
                #endregion
                #region Getting Main Info

                string paramcount = "";
                string tablecount = "";
                int pcount = 0;
                int tcount = 0;

                ini.GetValue("main", "reportname", ref reportname);
                ini.GetValue("main", "parametercount", ref paramcount);
                ini.GetValue("main", "tablecount", ref tablecount);

                pcount = ISM.Lib.Static.ToInt(paramcount);
                tcount = ISM.Lib.Static.ToInt(tablecount);

                #endregion
                #region Getting Parameters Info
                parameters.Clear();

                string value = "";

                for (int i = 0; i < pcount; i++)
                {
                    #region Creating Parameter item
                    ParameterItem pi = new ParameterItem();
                    string section = string.Format("Parameter{0}", i + 1);

                    success = ini.GetValue(section, "id", ref value);
                    if (success) pi.Id = value;

                    success = ini.GetValue(section, "name", ref value);
                    if (success) pi.Name = value;

                    success = ini.GetValue(section, "type", ref value);
                    if (success)
                    {
                        #region Type Convertion
                        switch (value.ToLower())
                        {
                            case "number":
                            case "decimal":
                            case "integer":
                            case "int":
                                pi.ValueType = typeof(decimal);
                                break;
                            case "date":
                            case "datetime":
                            case "time":
                                pi.ValueType = typeof(DateTime);
                                break;
                            default:
                                pi.ValueType = typeof(string);
                                break;
                        }
                        #endregion
                    }
                    success = ini.GetValue(section, "value", ref value);
                   
                    if (success) if ((value.ToLower() == "txndate"))
                        {
                            pi.Value = _core.TxnDate;

                        }
        
                        else if (success) pi.Value = ISM.Lib.Static.ConvToType(pi.ValueType, value);

          

                    success = ini.GetValue(section, "mandatory", ref value);
                    if (success) pi.Mandatory = (value.ToLower() == "true");

                    success = ini.GetValue(section, "length", ref value);
                    if (success) pi.ValueLength = ISM.Lib.Static.ToInt(value);

                    success = ini.GetValue(section, "editmask", ref value);
                    if (success) pi.EditMask = value;

                    success = ini.GetValue(section, "description", ref value);
                    if (success) pi.Desc = value;

                    success = ini.GetValue(section, "dictid", ref value);
                    if (success) pi.DictId = value;

                    success = ini.GetValue(section, "dictvaluefield", ref value);
                    if (success) pi.DictValueField = value;

                    success = ini.GetValue(section, "dictnamefield", ref value);
                    if (success) pi.DictNameField = value;

                    success = ini.GetValue(section, "tablenums", ref value);
                    if (success)
                    {
                        string[] tablenums = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] tnums = new int[tablenums.Length];
                        pi.TableNumbers = tnums;
                    }
                    #endregion
                    parameters.Add(pi);
                }

                #endregion
                #region Getting Tables Info

                for (int i = 0; i < tcount; i++)
                {
                    string section = string.Format("Table{0}", i + 1);
                    success = ini.GetBody(section, ref value);
                    if (success) tables[section] = value;
                }

                #endregion
            }
            catch
            {
                status = 39;
            }
            return status;
        }
        internal DialogResult LoadInputForm(int reportpriv)
        {
            frmInputParam frm = new frmInputParam(client, userno,reportpriv, parameters);
            frm.Text = reportFormTitle;
            DialogResult r = frm.ShowDialog();

            return r;
        }
        internal int LoadTableData(int reportpriv, ref DataSet ds, ref string error)
        {
            int success = 0;

            if (client != null)
            {
                if (ds == null) ds = new DataSet();
                for (int i = 0; i < tables.Count; i++)
                {
                    string tablename = string.Format("Table{0}", i + 1);
                    string sql = (string)tables[tablename];

                        if (!string.IsNullOrEmpty(sql))
                        {
                            #region Collect parameters
                            List<string> paramnames = new List<string>();
                            ArrayList paramvalues = new ArrayList();
                         
                            foreach (ParameterItem p in parameters)
                            {
                                if (p == null || p.TableNumbers == null || p.TableNumbers.Length <= 0
                                    || Array.IndexOf<int>(p.TableNumbers, 0) >= 0
                                    || Array.IndexOf<int>(p.TableNumbers, i) >= 0)
                                {
                              //      string regexPattern = @"(?<=TO_CHAR(BRANCHNO) =)[^;\n]*";
                                    string regexPattern = @":ppBranchCode";
                                    string regexUserno = @":ppUserNo";
                                    string regexLevel1 = @":pLevelNo1";
                                    string regexLevel2 = @":pLevelNo2";
                                    string regexLevel3 = @":pLevelNo3";
                                    string regexLevel4 = @":pLevelNo4";
                                    string regexLevel5 = @":pTxnDate";
                                    var pBranchCode = Regex.Match(sql, regexPattern); // 
                                    var pUserNo = Regex.Match(sql, regexUserno); // 
                                    var LevelNo1 = Regex.Match(sql, regexLevel1); // 
                                    var LevelNo2 = Regex.Match(sql, regexLevel2); // 
                                    var LevelNo3 = Regex.Match(sql, regexLevel3); // 
                                    var LevelNo4 = Regex.Match(sql, regexLevel4); // 
                                    var Txndate = Regex.Match(sql, regexLevel5); // 
                                    if (Static.ToStr(pBranchCode) != "")
                                    {
                                        paramnames.Add("ppBranchCode");
                                        paramvalues.Add(_core.User.BranchCode);
                                    }
                                    if (Static.ToStr(pUserNo) != "")
                                    {
                                        paramnames.Add("ppUserNo");
                                        paramvalues.Add(_core.User.UserNo);
                                    }
                                    if (Static.ToStr(LevelNo1) != "")
                                    {
                                        paramnames.Add("pLevelNo1");
                                        paramvalues.Add(_core.User.Level1);
                                    }
                                    if (Static.ToStr(LevelNo2) != "")
                                    {
                                        paramnames.Add("pLevelNo2");
                                        paramvalues.Add(_core.User.Level2);
                                    }
                                    if (Static.ToStr(LevelNo3) != "")
                                    {
                                        paramnames.Add("pLevelNo3");
                                        paramvalues.Add(_core.User.Level3);
                                    }
                                    if (Static.ToStr(LevelNo4) != "")
                                    {
                                        paramnames.Add("pLevelNo4");
                                        paramvalues.Add(_core.User.Level4);
                                    }

                                    if (Static.ToStr(Txndate) != "")
                                    {
                                        paramnames.Add("pTxnDate");
                                        paramvalues.Add(_core.TxnDate);
                                    }

                                   
                                    paramvalues.Add(p.Value);
                                   paramnames.Add(p.Id);
                 
                                }


                            }

                        #endregion
                        #region Calling server function
                        object[] param = new object[] { sql, paramnames.ToArray(), paramvalues.ToArray() };
                        Result r = client.Call(userno, 107, 107001, reportpriv, param);
                     //  Result rr = client.Call(userno, 107, 107001, reportpriv, param);
                        if (r.ResultNo != 0)
                        {
                            success = r.ResultNo;
                            error = r.ResultDesc;
                            break;
                     
                        }

                        r.Data.Tables[0].TableName = tablename;
                        ds.Tables.Add(r.Data.Tables[0].Copy());
                        r.Data = ds;
                     //   string tablename = string.Format("Table{0}", i + 1);
                       // string sql = (string)tables[tablename];

         
          //      ds.Tables.Add(r.Data.Tables[1].Copy());
      
                        #endregion
                    }
                }
            }
            if (success != 0) ds.Dispose();
            return success;
        }

        #endregion
        #region Report Functions

        /// <summary>
        /// Тайлангийн тохируулгуудыг INI файлаас уншиж бэлтгэх ба Prepared гишүүн нь True утгатай болно.
        /// Уг функцын дараа параметрийн утгуудыг гараас өөрчилж оруулж болох ба Generate()
        /// функцээр тайланг бодуулж гаргана.
        /// </summary>
        /// <param name="reportpriv">
        /// Тайлан дугаар. Энэ нь мөн тайланг боловсруулах эрхийн дугаар байх ба урьдчилан эрх бүртгэгдсэн байх шаардлагатай.
        /// </param>
        /// <param name="error">
        /// 10 - Ini file not found.
        /// 20 - Empty report file name.
        /// 21 - Empty data source.
        /// 22 - Not found specified report file.
        /// 23 - Export error.
        /// 24 - User cancel the report.
        /// 29 - Other error.
        /// 30 - Report ini file not found.
        /// 31 - Report is not prepared.
        /// </param>
        /// <returns></returns>
        public int Prepare(int reportpriv, ref string error)
        {
            int success = 0;

            #region Initialization

            string inifilename = string.Format("{0}\\rep{1}.ini", reportpath, reportpriv);
            success = LoadIniFile(inifilename, ref error);
            if (success != 0) return success;

            this.reportpriv = reportpriv;
            this.prepared = true;

            #endregion

            return success;
        }

        /// <summary>
        /// Тайлангийн параметрийн утга оруулах.
        /// </summary>
        /// <param name="paramid">
        /// Параметрийн Id дугаар.
        /// </param>
        /// <param name="value">
        /// Параметрийн утга. Текстээр илэрхийлэгдсэн утга байж болно.
        /// </param>
        public void SetValue(string paramid, object value)
        {
            if (string.IsNullOrEmpty(paramid)) return;

            foreach (ParameterItem pi in parameters)
            {
                if (paramid == pi.Id)
                {
                    pi.Value = ISM.Lib.Static.ConvToType(pi.ValueType, value);
                    break;
                }
            }
        }

        /// <summary>
        /// Prepare() хийгдсэний дараа тайланг боловсруулах функц.
        /// </summary>
        /// <param name="outfiletype">
        /// PDF, DOC, XLS, XLS1, RTF, HTML
        /// </param>
        /// <param name="stream">
        /// Боловсруулсан тайланг уг Stream обектоор буцаана.
        /// </param>
        /// <param name="error">
        /// 10 - Ini file not found.
        /// 20 - Empty report file name.
        /// 21 - Empty data source.
        /// 22 - Not found specified report file.
        /// 23 - Export error.
        /// 24 - User cancel the report.
        /// 29 - Other error.
        /// 30 - Report ini file not found.
        /// 31 - Report is not prepared.
        /// </param>
        /// <returns></returns>
        public int Generate(string outfiletype, ref Stream stream, ref string error)
        {
            if (!prepared) return 31;

            #region Initialization Variables
            Core core = new Core();
            DataSet ds = null;
            int success = 0;
            string reportfilename = string.Format("{0}\\rep{1}.rpt", reportpath, reportpriv);
            #endregion
            #region Loading report table data
            success = LoadTableData(reportpriv, ref ds, ref error);
            if (success != 0) return success;
            #endregion
            #region Export report
            success = core.ExportToStream(reportfilename, outfiletype, ds, ref stream, ref error);
            #endregion

            // for test
            if (success == 0)
            {
                FileStream fs = new FileStream("c:\\stream.xls", FileMode.Create, FileAccess.Write);
                if (stream != null)
                {
                    stream.CopyTo(fs);
                }
                fs.Close();
                stream.Position = 0;
            }


            return success;
        }

        /// <summary>
        /// Тайланг бэлтгээд шууд боловсруулж гаргах функц. Энэ нь дараах үйлдлийг хийсэнтэй адил бөгөөд хялбарчилсэн функц юм.
        /// 1. Prepare(...)
        /// 2. Generate(...)
        /// </summary>
        /// <param name="reportpriv">
        /// Тайлан дугаар. Энэ нь мөн тайланг боловсруулах эрхийн дугаар байх ба урьдчилан эрх бүртгэгдсэн байх шаардлагатай.
        /// </param>
        /// <param name="outfiletype">
        /// PDF, DOC, XLS, XLS1, RTF, HTML
        /// </param>
        /// <param name="stream">
        /// Боловсруулсан тайланг уг Stream обектоор буцаана.
        /// </param>
        /// <param name="error">
        /// 10 - Ini file not found.
        /// 20 - Empty report file name.
        /// 21 - Empty data source.
        /// 22 - Not found specified report file.
        /// 23 - Export error.
        /// 24 - User cancel the report.
        /// 29 - Other error.
        /// 30 - Report ini file not found.
        /// 31 - Report is not prepared.
        /// </param>
        /// <returns></returns>
        public int GetReport(int reportpriv, string outfiletype, ref Stream stream, ref string error)
        {
            Core core = new Core();
            DataSet ds = null;
            int success = 0;

            #region Initialization
            string reportfilename = string.Format("{0}\\rep{1}.rpt", reportpath, reportpriv);
            string inifilename = string.Format("{0}\\rep{1}.ini", reportpath, reportpriv);

            success = LoadIniFile(inifilename, ref error);
            if (success != 0) return success;
            #endregion
            #region Loading parameter input form
            if (parameters.Count > 0)
            {
                DialogResult r = LoadInputForm(reportpriv);
                if (r != DialogResult.OK)
                {
                    success = 24; // User cancel the report.
                    return success;
                }
            }
            #endregion
            #region Loading report table data
            success = LoadTableData(reportpriv, ref ds, ref error);
            if (success != 0) return success;
            #endregion
            #region Export report
            success = core.ExportToStream(reportfilename, outfiletype, ds, ref stream, ref error);
            #endregion
            return success;
        }
        public int GetReport(int reportpriv, string outfiletype, ref string outfilename, ref string error)
        {
            Core core = new Core();
            DataSet ds = null;
            int success = 0;

            #region Initialization
            string reportfilename = string.Format("{0}\\rep{1}.rpt", reportpath, reportpriv);
            string inifilename = string.Format("{0}\\rep{1}.ini", reportpath, reportpriv);

            if (string.IsNullOrEmpty(outfilename))
            {
                string extention = "";
                ExportFormatType type = core.GetExportType(outfiletype, ref extention);
                string tmpfile = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                string path = string.IsNullOrEmpty(this.reportoutpath) ? Path.GetTempPath() : this.reportoutpath;
                outfilename = string.Format("{0}\\rep{1}_{2}.{3}", path, reportpriv, tmpfile, extention);
            }

            success = LoadIniFile(inifilename, ref error);
            if (success != 0) return success;
            #endregion
            #region Loading parameter input form
            if (parameters.Count > 0)
            {
                DialogResult r = LoadInputForm(reportpriv);
                if (r != DialogResult.OK)
                {
                    success = 24; // User cancel the report.
                    return success;
                }
            }
            #endregion
            #region Loading report table data
            success = LoadTableData(reportpriv, ref ds, ref error);
            if (success != 0) return success;
            #endregion
            #region Export report
            success = core.ExportToFile(reportfilename, outfiletype, ds, outfilename, ref error);
            #endregion
            return success;
        }
        public int GetReport(int reportpriv, string outfiletype, DataSet ds, ref string outfilename, ref string error)
        {
            Core core = new Core();
            int success = 0;
            
            #region Initialization
            string reportfilename = string.Format("{0}\\rep{1}.rpt", reportpath, reportpriv);

            if (string.IsNullOrEmpty(outfilename))
            {
                string extention = "";
                ExportFormatType type = core.GetExportType(outfiletype, ref extention);
                string tmpfile = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                string path = string.IsNullOrEmpty(this.reportoutpath) ? Path.GetTempPath() : this.reportoutpath;
                outfilename = string.Format("{0}\\rep{1}_{2}.{3}", path, reportpriv, tmpfile, extention);
            }
            #endregion
            #region Export report
            success = core.ExportToFile(reportfilename, outfiletype, ds, outfilename, ref error);
            //ISM.Template.Globals.ShellOpenFile(outfilename);
            #endregion
            return success;
        }

        #region[Slip тайлан гаргаж авах функц]
        public int GetReportTxn(string slipname, string outfiletype, DataSet ds, ref string outfilename, ref string error)
        {
            Core core = new Core();
            int success = 0;

            #region Initialization
            string reportfilename = string.Format("{0}\\slip{1}.rpt", reportpath, slipname);

            if (string.IsNullOrEmpty(outfilename))
            {
                string extention = "";
                ExportFormatType type = core.GetExportType(outfiletype, ref extention);
                string tmpfile = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                string path = string.IsNullOrEmpty(this.reportoutpath) ? Path.GetTempPath() : this.reportoutpath;
                outfilename = string.Format("{0}\\slip{1}_{2}.{3}", path, slipname, tmpfile, extention);
            }



            success = core.ExportToFile(reportfilename, outfiletype, ds, outfilename, ref error);
            if (!File.Exists(outfilename))
            {




                MessageBox.Show(string.Format("Output file does not exist.\r\n{0}", outfilename));



                return success = 20;
            }



        ISM.Template.Globals.ShellOpenFile(outfilename);
            #endregion
            return success;



        }
        #endregion

        #endregion
        #region[Тайлангийн парамертүүдийн .dll -ээс унших]
        public string DynamicRead(string resourcetype, string privno)
        {
            if (File.Exists(string.Format("{0}\\rep{1}.dll", _core.ReportPathIn, privno)))
            {
                Assembly asm = Assembly.LoadFrom(_core.ReportPathIn + "\\rep" + privno + ".dll");
                SaveFileDialog file = new SaveFileDialog();
                file.FileName = string.Format("{0}\\rep{1}.{2}", _core.ReportPathIn, privno, resourcetype);
                Stream outFile = file.OpenFile();
                Stream stream = asm.GetManifestResourceStream(string.Format("rep{0}.{1}", privno, resourcetype));
                long length = stream.Length;
                if (length > int.MaxValue)
                {
                    MessageBox.Show("Энэхүү тайлан гарах боломжгүй байна.");
                    outFile.Close();
                    stream.Close();
                }
                else
                {
                    byte[] bytesini = new byte[length];
                    stream.Read(bytesini, 0, (int)length);
                    outFile.Write(bytesini, 0, (int)length);

                    outFile.Flush();
                    stream.Flush();
                    stream.Close();
                    outFile.Close();
                }
                return "";
            }
            else
            {
                return string.Format("rep{0}.dll not found", privno);
            }
        }
        public string SlipsRead(string slipsname)
        {
            if (File.Exists(string.Format("{0}\\{1}.dll", _core.SlipsPathIn, slipsname)))
            {
                Assembly asm = Assembly.LoadFrom(_core.SlipsPathIn + "\\" + slipsname + ".dll");
                SaveFileDialog file = new SaveFileDialog();
                file.FileName = string.Format("{0}\\{1}.rpt", _core.SlipsPathIn, slipsname);
                Stream outFile = file.OpenFile();
                Stream stream = asm.GetManifestResourceStream(string.Format("{0}.rpt", slipsname));
                long length = stream.Length;
                if (length > int.MaxValue)
                {
                    MessageBox.Show("Энэхүү тайлан гарах боломжгүй байна.");
                    outFile.Close();
                    stream.Close();
                }
                else
                {
                    byte[] bytesini = new byte[length];
                    stream.Read(bytesini, 0, (int)length);
                    outFile.Write(bytesini, 0, (int)length);

                    outFile.Flush();
                    stream.Flush();
                    stream.Close();
                    outFile.Close();
                }
                return "";
            }
            else
            {
              return string.Format("{0}.dll not found", slipsname);
            }
        }
        #endregion
        }
    public class ParameterItem
    {
        #region Variables
        public string Id;
        public string Name;
        public string Desc;

        public Type ValueType;
        public int ValueLength;
        public object Value;
        public string EditMask;
        public bool Mandatory;

        public string DictId;
        public string DictValueField;
        public string DictNameField;

        public int[] TableNumbers;
        #endregion
    }
}
