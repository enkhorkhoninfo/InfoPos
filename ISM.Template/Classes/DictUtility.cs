using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using EServ.Shared;

namespace ISM.Template
{
    public class DictUtility
    {
        #region Private Variables
        static private Hashtable _hash = new Hashtable();
        static public int FileId = 106;
        static public int FuncNo = 106002;
        static public int PrivNo = 106002;
        #endregion
        #region Public Methods

        static public Result Get(ISM.CUser.Remote remote, string id, int privno, ref DataTable table)
        {
            return Get(remote.Connection, remote.User.UserNo, id, privno, ref table);
        }
        static public Result Get(ISM.CUser.Remote remote, string id, ref DataTable table)
        {
            return Get(remote.Connection, remote.User.UserNo, id, PrivNo, ref table);
        }
        static public Result Get(ISM.CUser.Remote remote, string[] id, int privno, ref ArrayList tables)
        {
            return Get(remote.Connection, remote.User.UserNo, id, privno, ref tables);
        }
        static public Result Get(ISM.CUser.Remote remote, string[] id, ref ArrayList tables)
        {
            return Get(remote.Connection, remote.User.UserNo, id, PrivNo, ref tables);
        }

        static public Result Get(EServ.Client client, int userno, string id, int privno, ref DataTable table)
        {
            Result r = new Result();
            if (string.IsNullOrEmpty(id)) return r;
            ArrayList tables = null;
            r = Get(client, userno, new string[] { id }, privno, ref tables);
            if (r.ResultNo == 0)
            {
                table = (DataTable)tables[0];
            }
            return r;
        }
        static public Result Get(EServ.Client client, int userno, string id, ref DataTable table)
        {
            return Get(client, userno, id, PrivNo, ref table);
        }
        static public Result Get(EServ.Client client, int userno, string[] id, int privno, ref ArrayList tables)
        {
            Result r = new Result();
            if (id == null && id.Length <= 0) return r;

            try
            {
                if (tables == null) tables = new ArrayList();
                for (int i = 0; i < id.Length; i++)
                {
                    DataTable table = null;
                    string name = id[i].ToUpper();

                    string _dicfilename = string.Format(@"{0}\Data\Dic_{1}.xml", Static.WorkingFolder, name);
                    string _infofilename = string.Format(@"{0}\Data\Dic_{1}.inf", Static.WorkingFolder, name);
                    
                    #region Find the item from memory

                    DictItem item = null;
                    lock (_hash.SyncRoot)
                    {
                        item = (DictItem)_hash[name];
                        if (item == null)
                        {
                            item = new DictItem();
                            item.Id = name;
                        }
                    }

                    #endregion
                    
                    #region If item does not load into memory then load from local file
                    
                    if (item.Table == null) // item.Loaded == DateTime.MinValue
                    {
                        if (File.Exists(_infofilename))
                        {
                            string[] lines = File.ReadAllLines(_infofilename);
                            if (lines != null && lines.Length > 0)
                            {
                                item.Loaded = Static.ToDateTime(lines[0]);
                                item.RefreshInterval = Static.ToInt(lines[1]);
                                item.Name = Static.ToStr(lines[2]);

                                table = new DataTable();
                                XmlReadMode mode = table.ReadXml(_dicfilename);
                                item.Table = table;
                            }
                        }
                    }

                    #endregion

                    #region Calculate needs for refresh

                    double lifetime = DateTime.Now.Subtract(item.Loaded).TotalSeconds;

                    if (item.Table == null || (item.RefreshInterval > 0 && lifetime > item.RefreshInterval))
                    {
                        FormUtility.ProgressFormShow("Түр хүлээнэ үү, таталт хийгдэж байна.", item.Id + "...");
                        Result res = Refresh(client, userno, privno, ref item);
                        if (res.ResultNo == 0)
                        {
                            table = item.Table;
                            lock (_hash.SyncRoot)
                            { _hash[item.Id] = item; }

                            #region Save into local file

                            string[] lines = new string[] { 
                                Static.ToDateTimeStr( item.Loaded )
                                , Static.ToStr( item.RefreshInterval )
                                , item.Name
                            };
                            File.WriteAllLines(_infofilename, lines);
                            table.WriteXml(_dicfilename, XmlWriteMode.WriteSchema);

                            #endregion
                        }
                        else
                        {
                            r.ResultNo = res.ResultNo;
                            r.ResultDesc = res.ResultDesc;
                        }
                    }
                    else
                    {
                        table = item.Table;
                    }

                    #endregion

                    tables.Add(table);
                }
            }
            catch (Exception ex)
            {
                r.ResultNo = 9;
                r.ResultDesc = ex.ToString();
            }
            FormUtility.ProgressFormClose();
            return r;
        }
        static public Result Get(EServ.Client client, int userno, string[] id, ref ArrayList tables)
        {
            return Get(client, userno, id, PrivNo, ref tables);
        }
        
        static public List<DictItem> GetList()
        {
            List<DictItem> list = new List<DictItem>();
            try
            {
                lock (_hash.SyncRoot)
                {
                    foreach (DictItem item in _hash.Values)
                    {
                        list.Add(item);
                    }
                }
            }
            catch
            { }
            return list;
        }
        static private Result Refresh(EServ.Client client,int userno, int privno, ref DictItem item)
        {
            object[] param = new object[] { item.Id };
            Result r = null;
            try
            {
                r = client.Call(userno, FileId, FuncNo, privno, param);
                if (r.ResultNo == 0)
                {
                    DataTable dt1 = r.Data.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        item.Name = ISM.Lib.Static.ToStr(dt1.Rows[0]["Name"]);
                        item.Description = ISM.Lib.Static.ToStr(dt1.Rows[0]["Description"]);
                        item.RefreshInterval = ISM.Lib.Static.ToInt(dt1.Rows[0]["RefreshInterval"]);
                        item.Loaded = DateTime.Now;
                        item.Table = r.Data.Tables[1];
                    }
                    r.Data = null;
                    r.Param = null;
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }

            return r;
        }
        static private Result Refresh(ISM.CUser.Remote remote,int privno, ref DictItem item)
        {
            object[] param = new object[] { item.Id };
            Result r = null;
            try
            {
                r = remote.Connection.Call(remote.User.UserNo, FileId, FuncNo, privno, param);
                if (r.ResultNo == 0)
                {
                    DataTable dt1 = r.Data.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        item.Name = ISM.Lib.Static.ToStr(dt1.Rows[0]["Name"]);
                        item.Description = ISM.Lib.Static.ToStr(dt1.Rows[0]["Description"]);
                        item.RefreshInterval = ISM.Lib.Static.ToInt(dt1.Rows[0]["RefreshInterval"]);
                        item.Loaded = DateTime.Now;
                        item.Table = r.Data.Tables[1];
                    }
                    r.Data = null;
                    r.Param = null;
                }
                else
                {
                    return r;
                }
            }
            catch (Exception ex)
            {
                r = new Result(9, ex.ToString());
            }

            return r;
        }

        #endregion
    }
    public class DictItem
    {
        #region Public Variables
        public string Id = "";
        public string Name = "";
        public string Description = "";
        public int RefreshInterval = 0;
        public DateTime Loaded = DateTime.MinValue;
        public DataTable Table = null;
        #endregion
        #region Private Variables
        internal int _lockrefresh = 0;
        internal double elapsed_sec = 10;
        #endregion
    }
}
