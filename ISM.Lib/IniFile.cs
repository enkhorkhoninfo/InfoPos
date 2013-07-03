using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace ISM.Lib
{
    public class IniFile
    {
        #region Internals
        string[] lines;
        #endregion
        #region Properties
        string filename = "";
        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }
        #endregion
        #region Methods

        public int Read()
        {
            int success = 0;
            FileStream fs = null;
            try
            {
                lines = File.ReadAllLines(filename, Encoding.UTF8);
            }
            catch
            {
                success = 10;
            }
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
            }
            return success;
        }
        public int Read(string filename)
        {
            this.filename = filename;
            return Read();
        }

        internal bool issection(string line, ref string sectionname)
        {
            bool success = false;
            if (!string.IsNullOrEmpty(line) && line.Length > 2)
            {
                string s = line.Trim();
                if (s[0] == '[' && s[s.Length - 1] == ']')
                {
                    success = true;
                    sectionname = s.Substring(1, s.Length - 2);
                }
            }
            return success;
        }
        public bool GetBody(string section, ref string body)
        {
            if (string.IsNullOrEmpty(section) || lines == null) return false;

            StringBuilder sb = new StringBuilder();
            bool started = false;

            foreach (string line in lines)
            {
                string tmp = "";
                if (issection(line, ref tmp))
                {
                    // Second section is started, ends up body collection.
                    if (started) break;
                    if (tmp.Equals(section, StringComparison.OrdinalIgnoreCase)) started = true;
                }
                else
                {
                    // Body text is continuing
                    if (started) sb.AppendLine(line);
                }
            }
            body = sb.ToString();
            return started;
        }
        public bool GetValue(string section, string key, ref string value)
        {
            if (string.IsNullOrEmpty(section) || lines == null) return false;

            bool started = false;
            bool found = false;

            foreach (string line in lines)
            {
                string tmp = "";
                if (issection(line, ref tmp))
                {
                    #region Second section is started, ends up body collection.
                    if (started) break;
                    if (tmp.Equals(section, StringComparison.OrdinalIgnoreCase)) started = true;
                    #endregion
                }
                else
                {
                    #region Body text is continuing
                    if (started)
                    {
                        int p = line.IndexOf('=');
                        if (p > 0)
                        {
                            string tmpkey = line.Substring(0, p);
                            tmpkey = tmpkey.Trim();
                            if (tmpkey.Equals(key, StringComparison.OrdinalIgnoreCase))
                            {
                                if (line.Length > p)
                                {
                                    value = line.Substring(p + 1).Trim();
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            return found;
        }

        public string[] GetSections()
        {
            if (lines == null) return null;

            List<string> list = new List<string>();
            foreach (string line in lines)
            {
                string tmp = "";
                if (issection(line, ref tmp))
                {
                    list.Add(tmp);
                }
            }
            return list.ToArray();
        }
        public string[] GetKeys(string section)
        {
            if (string.IsNullOrEmpty(section) || lines == null) return null;

            bool started = false;
            List<string> list = new List<string>();
            foreach (string line in lines)
            {
                string tmp = "";
                if (issection(line, ref tmp))
                {
                    #region Second section is started, ends up body collection.
                    if (started) break;
                    if (tmp.Equals(section, StringComparison.OrdinalIgnoreCase)) started = true;
                    #endregion
                }
                else
                {
                    #region Body text is continuing
                    int p = line.IndexOf('=');
                    if (p > 0)
                    {
                        string tmpkey = line.Substring(0, p);
                        tmpkey = tmpkey.Trim();
                        list.Add(tmpkey);
                    }
                    #endregion
                }
            }
            return list.ToArray();
        }
        public NameValueCollection GetPairs(string section)
        {
            if (string.IsNullOrEmpty(section) || lines == null) return null;

            bool started = false;
            NameValueCollection list = new NameValueCollection();
            foreach (string line in lines)
            {
                string tmp = "";
                if (issection(line, ref tmp))
                {
                    #region Second section is started, ends up body collection.
                    if (started) break;
                    if (tmp.Equals(section, StringComparison.OrdinalIgnoreCase)) started = true;
                    #endregion
                }
                else
                {
                    #region Body text is continuing
                    int p = line.IndexOf('=');
                    if (p > 0)
                    {
                        string tmpkey = line.Substring(0, p);
                        tmpkey = tmpkey.Trim();

                        string value = "";
                        if (line.Length > p)
                        {
                            value = line.Substring(p + 1).Trim();
                        }

                        list[tmpkey] = value;
                    }
                    #endregion
                }
            }
            return list;
        }

        #endregion
    }
}
