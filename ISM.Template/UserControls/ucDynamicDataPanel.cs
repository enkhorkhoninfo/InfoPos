using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Persistent;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

using EServ.Shared;

namespace ISM.Template
{
    public partial class ucDynamicDataPanel : UserControl
    {
        #region Constants

        const int CONST_FILEID = 104;
        
        #endregion

        #region Properties

        private CUser.Remote _remote = null;
        [DefaultValue(null), Browsable(false)]
        public CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }
        private Resource _resource;
        [DefaultValue(null), Browsable(false)]
        public Resource Resource
        {
            get { return _resource; }
            set
            {
                _resource = value;
                ucParameterPanel1.Resource = value;
            }
        }

        [DefaultValue(null), Browsable(false)]
        public DynamicParameterItem SelectedRow
        {
            get { return ucParameterPanel1.SelectedRow; }
        }

        private DataTable _table = null;
        [DefaultValue(null), Browsable(false)]
        public DataTable DataSource
        {
            get { return _table; }
            set
            {
                _table = value;
                if (_table == null || _table.Rows.Count <= 0)
                {
                    ucParameterPanel1.ItemClearAll();
                }
                else
                {
                    ListSetDataTable(_table);
                }
            }
        }

        private string _tableprefix = "";
        [DefaultValue(""), Browsable(true)]
        public string TableNamePrefix
        {
            get { return _tableprefix; }
            set { _tableprefix = value; }
        }

        private ulong _tabletypeid;
        [DefaultValue(0), Browsable(true)]
        public ulong TableTypeId
        {
            get { return _tabletypeid; }
            set { _tabletypeid = value; }
        }

        private int _tableprivselect = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivSelect
        {
            get { return _tableprivselect; }
            set { _tableprivselect = value; }
        }
        private int _tableprivupdate = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivUpdate
        {
            get { return _tableprivupdate; }
            set { _tableprivupdate = value; }
        }

        private ulong _tablerowkey = 0;
        [DefaultValue(0), Browsable(true)]
        public ulong TableRowKey
        {
            get { return _tablerowkey; }
            set { _tablerowkey = value; }
        }

        private string _linkid = "";
        [DefaultValue(""), Browsable(true)]
        public string LinkId
        {
            get { return _linkid; }
            set { _linkid = value; }
        }

        private int _linktypecode = 0;
        [DefaultValue(0), Browsable(true)]
        public int LinkTypeCode
        {
            get { return _linktypecode; }
            set { _linktypecode = value; }
        }
        
        #endregion

        #region Constractor

        public ucDynamicDataPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region Control Events

        private void ucDynamicDataPanel_Load(object sender, EventArgs e)
        {
            ucParameterPanel1.EventButtonClick += new ucParameterPanel.delegateEventButtonClick(ucParameterPanel1_EventButtonClick);
        }
        private void ucParameterPanel1_EventButtonClick(string id, DynamicParameterType type, int index, object value, ulong attachid, byte[] attachdata, ref object newvalue, ref byte[] newattachdata)
        {
            switch (type)
            {
                case DynamicParameterType.Picture:
                case DynamicParameterType.File:
                    Result res = ReadBlob(ref newattachdata);
                    break;
            }
        }

        #endregion

        #region Server Methods

        public void ListRefresh()
        {
            ucParameterPanel1.ItemListRefresh();
            if (_remote != null)
            {
                ItemSetListFromDictionary(_remote);
            }
            ucParameterPanel1.ItemListRefreshValues();
        }
        public Result ListRead()
        {
            Result res = null;
            if (_remote != null)
            {
                #region Preparing Calling Parameters
                object[] param = new object[] { _tableprefix, _tabletypeid, _tablerowkey };
                #endregion

                #region Call Server Function
                res = _remote.Connection.Call(
                    _remote.User.UserNo
                    , CONST_FILEID
                    , 104001
                    , _tableprivselect
                    , param
                    );
                #endregion

                #region Set DataTable received from server into DataSource
                if (res.ResultNo == 0)
                {
                    if (res.Data != null && res.Data.Tables.Count > 0)
                        DataSource = res.Data.Tables[0];
                }
                else
                {
                    MessageBox.Show(res.ResultDesc);
                }
                #endregion
            }
            return res;
        }
        public Result ListSave()
        {
            Result res = null;
            List<DynamicParameterItem> items = ucParameterPanel1.ItemGetList();

            try
            {
                #region Additional Data мэдээллийг хадгалах

                ArrayList rows = new ArrayList(items.Count);
                foreach (DynamicParameterItem item in items)
                {
                    rows.Add(new object[] { Static.ToLong(item.Id), (int)item.ValueType, Static.ToStr(item.Value), item.AttachId });
                    //switch (item.ValueType)
                    //{
                    //    case DynamicParameterType.Picture:
                    //    case DynamicParameterType.File:
                    //        break;
                    //    default:
                    //        rows.Add(new object[] { Static.ToLong(item.Id), (int)item.ValueType, Static.ToStr(item.Value), item.AttachId });
                    //        break;
                    //}
                }

                object[] param = new object[] { _tableprefix, _tablerowkey, rows.ToArray() };
                res = _remote.Connection.Call(_remote.User.UserNo
                    , CONST_FILEID
                    , 104002
                    , _tableprivupdate
                    , param);
                if (res.ResultNo != 0) goto OnExit;

                #endregion

                #region AttachId дугаараар зургийн мэдээллүүдийг тусад нь илгээж хадгалах

                rows.Clear();
                foreach (DynamicParameterItem item in items)
                {
                    if (item.Editing)
                    {
                        if (item.ValueType == DynamicParameterType.Picture)
                        {
                            #region Saving Attach Blob
                            res = AttachUtility.SaveImage(_remote, _tableprivupdate, item.AttachId, Static.ToStr( item.Value), item.AttachData, _linktypecode, _linkid);
                            if (res.ResultNo != 0) goto OnExit;
                            #endregion

                            #region Collecting New Dynamic record of blob

                            if (item.AttachId == 0)
                            {
                                item.AttachId = (ulong)Static.ToLong(res.Param[0]);
                                rows.Add(new object[] { Static.ToLong(item.Id), (int)item.ValueType, item.Value, item.AttachId });
                            }

                            #endregion
                        }
                        if (item.ValueType == DynamicParameterType.File)
                        {
                            #region Saving Attach Blob
                            res = AttachUtility.SaveFromFile(_remote, _tableprivupdate, item.AttachId, 1, Static.ToStr(item.Value), _linktypecode, _linkid);
                            if (res.ResultNo != 0) goto OnExit;
                            #endregion

                            #region Collecting New Dynamic record of blob

                            if (item.AttachId == 0)
                            {
                                item.AttachId = (ulong)Static.ToLong(res.Param[0]);
                                rows.Add(new object[] { Static.ToLong(item.Id), (int)item.ValueType, item.Value, item.AttachId });
                            }

                            #endregion
                        }
                        item.Editing = false;
                    }
                }

                if (rows.Count > 0)
                {
                    param = new object[] { _tableprefix, _tablerowkey, rows.ToArray() };
                    res = _remote.Connection.Call(_remote.User.UserNo
                        , CONST_FILEID
                        , 104002
                        , _tableprivupdate
                        , param);
                }

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            if (res != null && res.ResultNo == 0) ucParameterPanel1.ItemListSetSaved();
            return res;
        }
        private Result ReadBlob(ref byte[] newattachdata)
        {
            Result res = new Result();

            #region Зураг Browse хийгдэх үед AttachId дугаартай зураг татагдаагүй бол татах

            DynamicParameterItem pi = ucParameterPanel1.SelectedRow;
            if (pi.AttachId != 0 && pi.AttachData == null)
            {
                res = AttachUtility.GetBytes(_remote, _tableprivselect, pi.AttachId, ref newattachdata);
            }
            #endregion

            return res;
        }

        #endregion

        #region Methods

        public void ItemSetDataSource(string Id, DataTable Table, string ValueFieldName, string NameFieldName, params string[] ColumnNames)
        {
            ucParameterPanel1.ItemSetList(Id, Table, ValueFieldName, NameFieldName, "", null);
        }
        public void ItemSetDataSource(string Id, DataTable Table, string ValueFieldName, params string[] ColumnNames)
        {
            ucParameterPanel1.ItemSetList(Id, Table, ValueFieldName, ValueFieldName, "", null);
        }
        public void ItemSetDataView(string Id, DataTable Table, string ValueFieldName, string NameFieldName, string Filter, params string[] ColumnNames)
        {
            ucParameterPanel1.ItemSetList(Id, Table, ValueFieldName, NameFieldName, Filter, null);
        }
        public void ItemSetDataView(string Id, DataTable Table, string ValueFieldName, string Filter, params string[] ColumnNames)
        {
            ucParameterPanel1.ItemSetList(Id, Table, ValueFieldName, ValueFieldName, Filter, null);
        }
        public void ItemSetList(string Id, object Value, object Name)
        {
            ucParameterPanel1.ItemSetList(Id, Value, Name);
        }
        public Result ItemSetListFromDictionary(CUser.Remote remote)
        {
            return ucParameterPanel1.ItemSetListFromDictionary(remote);
        }

        public void ListSetRow(DataRow row)
        {
            try
            {
                string name = Static.ToStr(row["name"]);
                if (string.IsNullOrEmpty(name)) name = "[NO NAME]";

                DynamicParameterItem item = ucParameterPanel1.ItemAdd(
                Static.ToStr(row["itemid"])
                , Static.ToStr(row["name"])
                , Static.ToStr(row["name2"])
                , Static.ToStr(row["value"])
                , (ISM.Template.DynamicParameterType)Static.ToInt(row["valuetype"])
                , Static.ToInt(row["valuelength"])
                , Static.ToStr(row["valuedefault"])
                , Static.ToBool(row["mandatory"])
                , Static.ToStr(row["editmask"])
                , Static.ToStr(row["description"])
                , Static.ToStr(row["dictid"])
                , Static.ToBool(row["dicteditable"])
                , Static.ToStr(row["dictvaluefield"])
                , Static.ToStr(row["dictdescfield"])
                , Static.ToInt(row["orderno"])
                );

                item.AttachId = (ulong)Static.ToLong(row["attachid"]);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ListSetDataTable(DataTable table)
        {
            ucParameterPanel1.ItemRemoveAll();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                    ListSetRow(dr);
            }
            ucParameterPanel1.ItemListRefresh();
            if (_remote != null)
            {
                ItemSetListFromDictionary(_remote);
            }
            ucParameterPanel1.ItemListRefreshValues();
        }
        
        #endregion
    }
}
