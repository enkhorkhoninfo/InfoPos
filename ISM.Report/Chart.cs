using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Shared;
using ISM.Lib;

namespace ISM.Report
{
    public class Chart
    {
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

        byte[] buffer = null;
        public byte[] Buffer
        {
            get { return buffer; }
        }

        string filename = "";
        public string GeneratedFileName
        {
            get { return filename; }
        }

        bool saved = false;
        public bool Saved
        {
            get { return saved; }
        }

        #endregion
        #region Methods
        public Result GetList(int privcode)
        {
            Result r = null;
            try
            {
                if (client == null)
                {
                    r = new Result(9, "Client object should not be null!");
                }
                r = client.Call(userno, 107, 107101, privcode, null);
            }
            catch(Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        public Result Upload(int privcode, int reportpriv, string filename)
        {
            Result r = null;
            try
            {
                byte[] body = null;
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    body = new byte[fs.Length];
                    fs.Read(body, 0, (int)fs.Length);
                    fs.Close();
                }
                object[] param = new object[] { reportpriv, filename, body};
                r = client.Call(userno, 107, 107102, privcode, param);
            }
            catch(Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        public Result Analyze(int privcode, int reportpriv)
        {
            Result r = null;
            try
            {
                object[] param = new object[] { reportpriv };
                r = client.Call(userno, 107, 107103, privcode, param);
            }
            catch(Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        public Result Generate(int privcode, int reportpriv, DateTime reportdate, bool autoopen)
        {
            Result r = null;
            try
            {
                object[] param = new object[] { reportpriv, reportdate };
                r = client.Call(userno, 107, 107104, privcode, param);
                if (r.ResultNo == 0)
                {
                    if (r.Param != null) buffer = (byte[])r.Param[0];
                    filename = string.Format(@"{0}\R{1}_{2}.xls"
                        , Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                        , reportpriv
                        , reportdate.ToString("yyyymmdd"));

                    if (autoopen)
                    {
                        r = Save();
                        if (r.ResultNo == 0) r = Open();
                    }
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }
            return r;
        }
        public Result Save()
        {
            Result r = new Result();
            try
            {
                if (buffer != null)
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(buffer, 0, buffer.Length);
                        fs.Flush();
                        fs.Close();
                    }
                    saved = true;
                }
            }
            catch (Exception ex)
            {
                r.ResultNo = 9;
                r.ResultDesc = ex.Message;
            }
            return r;
        }
        public Result Open()
        {
            Result r = new Result();
            try
            {
                if (saved)
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = filename;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                }
            }
            catch (Exception ex)
            {
                r.ResultNo = 9;
                r.ResultDesc = ex.Message;
            }
            return r;
        }
        #endregion
    }
}
