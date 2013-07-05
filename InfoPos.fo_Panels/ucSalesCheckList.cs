using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using EServ.Shared;
namespace InfoPos.fo_panels
{
    public partial class ucSalesCheckList : DevExpress.XtraEditors.XtraUserControl
    {
        #region[Properties]
        private InfoPos.Core.Core _core = null;
        public InfoPos.Core.Core Core
        {
            get { return _core; }
            set
            {
                if (value != null)
                {
                    _core = value;
                    if (_remote == null) _remote = _core.RemoteObject;
                    if (_resource == null) _resource = _core.Resource;
                }
            }
        }
        Hashtable _allreadycustomer = new Hashtable();
        private ISM.CUser.Remote _remote = null;
        public ISM.CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }

        private ISM.Template.Resource _resource = null;
        public ISM.Template.Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }
        #endregion

        #region[Variables]
        DataTable Data = new DataTable();
        RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
        #endregion

        #region[Constructure]
        public ucSalesCheckList()
        {
            InitializeComponent();
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);
            Data.Columns.Add("SELECT", typeof(int));
            Data.Columns.Add("SALESNO", typeof(string));
            Data.Columns.Add("SEX", typeof(int));
            Data.Columns.Add("CUSTOMERNO", typeof(long));
            Data.Columns.Add("CUSTNAME", typeof(string));
            Data.Columns.Add("CLASSCODE", typeof(int));
            Data.Columns.Add("REGISTERNO", typeof(string));
            gridControl1.DataSource = Data;
            gridView1.Columns[0].Caption = "Сонгох";
            gridView1.Columns[0].ColumnEdit = CreateRepositoryCheckEdit();
            gridControl1.RepositoryItems.Add(CreateRepositoryCheckEdit());
            gridView1.Columns[1].Caption = "Борлуулалтын №";
            gridView1.Columns[2].Caption = "Хүйс";
            gridView1.Columns[2].ColumnEdit = repositoryItemImageComboBox1;
            gridView1.Columns[2].Width = 10;
            gridView1.Columns[3].Caption = "Харилцагчийн №";
            gridView1.Columns[4].Caption = "Нэр";
            gridView1.Columns[5].Caption = "Төрөл";
            gridView1.Columns[6].Caption = "Регистерийн №";
            for (int i = 1; i <= 6; i++)
            {
                gridView1.Columns[i].BestFit();
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
        }
        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            OnEventRowChanged();
        }
        private void ucSalesCheckList_Load(object sender, EventArgs e)
        {
            try
            {
                gridView1.OptionsCustomization.AllowGroup = false;
                gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
                gridView1.OptionsView.ColumnAutoWidth = false;
                //gridView1.OptionsView.ShowAutoFilterRow = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsView.ShowIndicator = false;
                gridView1.OptionsView.RowAutoHeight = true;
                gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);

                if (Resource != null)
                {
                    image.AddImage(Resource.GetImage("dashboard_user"));
                    image.AddImage(Resource.GetImage("image_woman"));
                    image.AddImage(Resource.GetImage("menu_office"));
                    repositoryItemImageComboBox1.LargeImages = image;
                }
                gridView1.RowHeight = 30;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region[User Function]
        public Result RefreshData(string BatchNo)
        {
            Result res = new Result();
            gridControl1.DataSource = null;
            res = _remote.Connection.Call(_remote.User.UserNo, 501, 600006, 600006, new object[] { BatchNo });
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                DataTable dt = res.Data.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (!_allreadycustomer.ContainsKey(Static.ToStr(dr["SALESNO"])))
                    {
                        DataRow drow = Data.NewRow();
                        drow["SELECT"] = 0;
                        drow["SALESNO"] = dr["SALESNO"];
                        drow["SEX"] = dr["SEX"];
                        drow["CUSTOMERNO"] = dr["CUSTOMERNO"];
                        drow["CUSTNAME"] = dr["CUSTNAME"];
                        drow["CLASSCODE"] = dr["CLASSCODE"];
                        drow["REGISTERNO"] = dr["REGISTERNO"];
                        Data.Rows.Add(drow);
                        gridControl1.DataSource = Data;
                        _allreadycustomer.Add(Static.ToStr(dr["SALESNO"]), 0);
                    }
                }
            }
            return res;
        }
        public void SelectAll()
        {
            try
            {
                if (Data != null)
                {
                    foreach (DataRow dr in Data.Rows)
                    {
                        if (Static.ToInt(dr["SELECTAll"]) == 0)
                            dr["SELECT"] = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DeSelectAll()
        {
            try
            {
                if (Data != null)
                {
                    foreach (DataRow dr in Data.Rows)
                    {
                        if (Static.ToInt(dr["SELECTAll"]) == 1)
                            dr["SELECT"] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ColumnVisible(int index, bool visible)
        {
            try
            {
                if (index <= gridView1.Columns.Count)
                    gridView1.Columns[index].Visible = visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region[Check]
        void ri_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            string val = "";
            if (e.Value != null)
            {
                val = e.Value.ToString();
            }
            else
            {
                val = "False";
            }
            switch (val)
            {
                case "True":
                    e.CheckState = CheckState.Checked;
                    break;
                case "False":
                    e.CheckState = CheckState.Unchecked;
                    break;
                case "Yes":
                    goto case "True";
                case "No":
                    goto case "False";
                case "1":
                    goto case "True";
                case "0":
                    goto case "False";
                default:
                    e.CheckState = CheckState.Checked;
                    break;
            }
            e.Handled = true;
        }
        public RepositoryItemCheckEdit CreateRepositoryCheckEdit()
        {
            //((System.ComponentModel.ISupportInitialize)(ri)).BeginInit();
            ri.AutoHeight = false;
            ri.AllowFocused = false;
            ri.ValueChecked = 1;
            ri.ValueUnchecked = 0;
            ((System.ComponentModel.ISupportInitialize)(ri)).EndInit();
            ri.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(ri_QueryCheckStateByValue);
            ri.CheckedChanged += new EventHandler(ri_CheckedChanged);
            return ri;
        }
        void ri_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chk=sender as CheckEdit;

            DataRow row = gridView1.GetFocusedDataRow();
            if (row != null)
            {
                if (chk.Checked == true)
                    row["SELECT"] = 1;
                else
                    row["select"] = 0;
            }
            string sales = "";
            string[] salesno;
            foreach (DataRow dr in Data.Rows)
            {
                if (Static.ToInt(dr["SELECT"]) == 1)
                {
                    if (sales == "")
                    {
                        sales = Static.ToStr(dr["SALESNO"]);
                    }
                    else
                    {
                        sales = sales + ";" + Static.ToStr(dr["SALESNO"]);
                    }
                }
            }
            salesno = sales.Split(';');
            OnCheckedChanged(salesno);
        }
        #endregion

        #region[CustomEvents]
        public delegate void delegateEventOnCheckedChanged(string[] SalesNo);
        public event delegateEventOnCheckedChanged EventOnCheckedChanged;
        public void OnCheckedChanged(string[] SalesNo)
        {
            try
            {
                if (EventOnCheckedChanged != null) EventOnCheckedChanged(SalesNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }

        public  delegate  void  delegateEventOnRowChanged(string  SalesNo);
        public  event  delegateEventOnRowChanged  EventOnRowChanged;
        public void OnEventRowChanged()
        {
            try
            {
                if (EventOnRowChanged != null)
                {
                    DataRow row = gridView1.GetFocusedDataRow();
                    if (row != null)
                    {
                        string salesno = Static.ToStr(row["salesno"]);
                        EventOnRowChanged(salesno);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
