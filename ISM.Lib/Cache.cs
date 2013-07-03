using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;

namespace ISM.Lib
{
    public class Cache
    {
        #region XML Cache
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
        static public void XMLCacheSave(string filename, object handle)
        {
            try
            {
                if (string.IsNullOrEmpty(filename) || handle == null) return;

                Hashtable values = (Hashtable)handle;
                using (DataTable dt = new DataTable())
                {
                    dt.TableName = "Table1";
                    dt.Columns.Add("key", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    foreach (object value in values.Keys)
                    {
                        if (value != null && value != DBNull.Value)
                            dt.Rows.Add(value, values[value]);
                    }
                    Static.CheckFileFolder(filename);
                    dt.WriteXml(filename, XmlWriteMode.WriteSchema);
                }
            }
            catch
            { }
        }
        static public void XMLCacheClear(object handle)
        {
            if (handle == null) return;
            Hashtable h = (Hashtable)handle;
            h.Clear();
        }

        static public object XMLCacheSet(object handle, string key, object value)
        {
            object ret = null;
            try
            {
                if (handle != null)
                {
                    Hashtable h = (Hashtable)handle;
                    ret = h[key];
                    h[key] = value;
                }
            }
            catch
            { }
            return ret;
        }
        static public object XMLCacheGet(object handle, string key, object defaultvalue = null)
        {
            object ret = null;
            try
            {
                if (handle != null)
                {
                    Hashtable h = (Hashtable)handle;
                    ret = h[key];
                    if (ret == null) ret = defaultvalue;
                }
            }
            catch
            { }
            return ret;
        }

        static public string XMLCacheGetStr(object handle, string key, string defaultvalue = "")
        {
            return Static.ToStr(XMLCacheGet(handle, key, defaultvalue));
        }
        static public int XMLCacheGetInt(object handle, string key, int defaultvalue = 0)
        {
            return Static.ToInt(XMLCacheGet(handle, key, defaultvalue));
        }
        static public long XMLCacheGetLong(object handle, string key, long defaultvalue = 0L)
        {
            return Static.ToLong(XMLCacheGet(handle, key, defaultvalue));
        }
        static public double XMLCacheGetDbl(object handle, string key, double defaultvalue = 0.0D)
        {
            return Static.ToDouble(XMLCacheGet(handle, key, defaultvalue));
        }
        static public decimal XMLCacheGetDec(object handle, string key, decimal defaultvalue = 0)
        {
            return Static.ToDecimal(XMLCacheGet(handle, key, defaultvalue));
        }
        #endregion
    }
}
