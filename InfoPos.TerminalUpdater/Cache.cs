using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.IO;
namespace InfoPos.TerminalUpdater
{
    class Cache
    {

        static public object XMLCacheGet(string key, object defaultvalue = null)
        {
            string _workingfolder = "";
            string s = Assembly.GetExecutingAssembly().Location;
            int i = s.LastIndexOf(@"\");
            if (i > 0)
            {
                _workingfolder = s.Substring(0, i);
            }
            string _xmlcachename = string.Format(@"{0}\Data\Settings.xml", _workingfolder);
            object _xmlcache = XMLCacheOpen(_xmlcachename);
            object ret = null;
            try
            {
                if (_xmlcache != null)
                {
                    Hashtable h = (Hashtable)_xmlcache;
                    ret = h[key];
                    if (ret == null) ret = defaultvalue;
                }
            }
            catch
            { }
            return ret;
        }
        static public object XMLCacheOpen(string filename, bool clean = false)
        {
            Hashtable values = null;
            try
            {
                values = new Hashtable(1024);
                if (!clean && File.Exists(filename))
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.ReadXml(filename);
                        if (dt.Columns.Contains("key") && dt.Columns.Contains("value"))
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                values[row["key"]] = row["value"];
                            }
                        }
                    }
                }
            }
            catch
            { }
            return values;
        }
    }
}
