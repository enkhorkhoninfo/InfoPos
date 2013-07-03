using System;
using System.Collections;
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
    public partial class ucXDataPanel : UserControl
    {
        private Hashtable _himages = new Hashtable();
        private bool _existsrecord = false;

        #region Properties

        private DataRow _row = null;
        [DefaultValue(null), Browsable(false)]
        public DataRow SelectedRow
        {
            get { return _row; }
        }

        private ISM.Template.Resource _resource = null;
        [DefaultValue(null), Browsable(false)]
        public ISM.Template.Resource Resource
        {
            get { return _resource; }
            set
            {
                _resource = value;
                if (_resource != null)
                {
                    btnSave.Image = _resource.GetBitmap("navigate_save");
                    btnCancel.Image = _resource.GetBitmap("navigate_cancel");
                }
            }
        }

        private DataTable _table = null;
        [DefaultValue(null), Browsable(false)]
        public DataTable DataSource
        {
            get { return _table; }
            set
            {
                _table = value;
                FieldFindRefresh();
            }
        }

        private bool _editing = false;
        public bool Editing
        {
            get { return _editing; }
        }

        #endregion

        #region Constractor

        public ucXDataPanel()
        {
            InitializeComponent();

            vGridControl1.ShowButtonMode = DevExpress.XtraVerticalGrid.ShowButtonModeEnum.ShowAlways;

            vGridControl1.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(vGridControl1_CellValueChanged);
            vGridControl1.CellValueChanging += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(vGridControl1_CellValueChanging);

            this.riButtonEdit.ButtonClick += new ButtonPressedEventHandler(riButtonEdit_ButtonClick);
            this.riButtonEditReadOnly.ButtonClick += new ButtonPressedEventHandler(riButtonEdit_ButtonClick);
        }

        void vGridControl1_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            _editing = !e.Value.Equals(e.Row.Properties.Value);
            ItemInfo ii = (ItemInfo)e.Row.Tag;
            ii.isediting = _editing;
        }
        void vGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (_editing)
            {
                e.Row.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            ToggleButton();
        }

        #endregion

        #region Control Events

        private void ucXDataPanel_Load(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
        private void btnView_Click(object sender, EventArgs e)
        {
            ISM.Template.FormImage frm = new FormImage();
            frm.Resource = _resource;
            DialogResult dlg = frm.ShowDialog();
        }
        private void btnInput_Click(object sender, EventArgs e)
        {

        }

        void riButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ShowButtonForm(sender);
        }
        
        #endregion

        #region Control Managing Methods

        public void ToggleButton()
        {
            btnSave.Enabled = _editing;
            btnCancel.Enabled = _editing;
        }
        public void ShowButtonForm(object sender)
        {
            ItemInfo ii = (ItemInfo)vGridControl1.FocusedRow.Tag;
            DataRow row = ii.row;

            if (row != null)
            {
                int valuetype = Static.ToInt(row["addtype"]);
                switch (valuetype)
                {
                    case 3: //Picture type
                        MessageBox.Show(string.Format("Button Click: Value Type = {0}", row["addtype"]));

                        ISM.Template.FormImage frm = new FormImage();
                        frm.Resource = _resource;
                        DialogResult dlg = frm.ShowDialog();
                        if (dlg == DialogResult.OK)
                        {
                            DevExpress.XtraEditors.ButtonEdit edit = sender as DevExpress.XtraEditors.ButtonEdit;
                            if (string.IsNullOrEmpty( edit.Text ) && frm.ImageObject !=null)
                                edit.EditValue = "[new]";

                            edit.ForeColor = System.Drawing.Color.Red;
                        }

                        break;
                    case 4: // Folder type
                        FolderBrowserDialog folder = new FolderBrowserDialog();
                        DialogResult res = folder.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            DevExpress.XtraEditors.ButtonEdit edit = sender as DevExpress.XtraEditors.ButtonEdit;
                            edit.EditValue = folder.SelectedPath;
                            //edit.ForeColor = System.Drawing.Color.Red;
                        }

                        break;
                }
            }
        }

        public void FieldFindRefresh()
        {
            vGridControl1.Rows.Clear();
            foreach (DataRow row in _table.Rows)
            {
                EditorRow erow = new EditorRow();
                erow.Properties.Caption = Static.ToStr(row["name"]);
                
                ItemInfo ii = new ItemInfo();
                ii.row = row;

                switch (Static.ToInt(row["addtype"]))
                {
                    case 1: // number type
                        erow.Properties.RowEdit = vGridControl1.RepositoryItems[1];
                        //erow.Properties.RowEdit.OwnerEdit.Text = Static.ToStr(row["value"]);
                        erow.Properties.Value = Static.ToInt(row["value"]);
                        break;
                    case 2: // date type
                        erow.Properties.RowEdit = vGridControl1.RepositoryItems[2];
                        erow.Properties.Value = Static.ToDate(row["value"]);
                        //erow.Properties.RowEdit.OwnerEdit.Text = Static.ToStr(row["value"]);
                        break;
                    case 3: // picture type
                        erow.Properties.RowEdit = vGridControl1.RepositoryItems[4];
                        //ulong attachid = (ulong)Static.ToLong(row["attachid"]);
                        string attachid = Static.ToStr(row["attachid"]);
                        erow.Properties.Value = attachid;
                        ii.isnew = (string.IsNullOrEmpty(attachid));
                        break;
                    case 4: // file type
                        erow.Properties.RowEdit = vGridControl1.RepositoryItems[3];
                        erow.Properties.Value = Static.ToStr(row["value"]).ToUpper();
                        break;
                    default: // 1 and others is string type
                        erow.Properties.RowEdit = vGridControl1.RepositoryItems[0];
                        erow.Properties.Value = Static.ToStr(row["value"]).ToUpper();
                        break;
                }

                erow.Tag = ii;
                vGridControl1.Rows.Add(erow);
            }
        }


        public object FieldFindGetValue(string id)
        {
            object value = null;
            BaseRow row = vGridControl1.Rows.GetRowByFieldName(id);
            if (row != null) value = row.Properties.Value;
            return value;
        }
        public object[] FieldFindGetList()
        {
            ArrayList ar = new ArrayList();
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                ar.Add(vGridControl1.Rows[i].Properties.Value);
            }
            return ar.ToArray();
        }
        private void FieldFindClear()
        {
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                vGridControl1.Rows[i].Properties.Value = null;
            }
        }

        #endregion
    }

    public class ItemInfo
    {
        #region Variables
        internal int id;
        internal string name;
        internal string name2;
        internal int addtype;
        internal int len;
        internal int mandatory;
        internal string mask;
        internal string adddefault;
        internal string description;
        internal string listcombo;
        internal int comboedit;
        internal string tablename;
        internal string fieldid;
        internal string fieldname;
        internal int orderno;

        internal DataRow row = null;
        internal System.Drawing.Image image = null;
        internal bool isediting = false;
        internal bool isnew = false;

        #endregion

        #region Constractor

        public ItemInfo()
        {
        }
        public ItemInfo(DataRow row)
        {
            id = Static.ToInt(row["id"]);
            name = Static.ToStr(row["name"]);
            name2 = Static.ToStr(row["name2"]);
            addtype = Static.ToInt(row["addtype"]);
            len = Static.ToInt(row["len"]);
            mandatory = Static.ToInt(row["mandatory"]);
            mask = Static.ToStr(row["mask"]);
            adddefault = Static.ToStr(row["adddefault"]);
            description = Static.ToStr(row["description"]);
            listcombo = Static.ToStr(row["listcombo"]);
            comboedit = Static.ToInt(row["comboedit"]);
            tablename = Static.ToStr(row["tablename"]);
            fieldid = Static.ToStr(row["fieldid"]);
            fieldname = Static.ToStr(row["fieldname"]);
            orderno = Static.ToInt(row["orderno"]);
        }
        #endregion
    }
}
