using System;
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
    public partial class ucParameterPanel : UserControl
    {
        #region Private Objects

        private Hashtable _hitems = new Hashtable();
        private ArrayList _aitems = new ArrayList();

        #endregion

        #region Properties

        private DynamicParameterItem _row = null;
        [DefaultValue(null), Browsable(false)]
        public DynamicParameterItem SelectedRow
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
            }
        }

        private bool _showdesciption = true;
        [DefaultValue(false), Browsable(true)]
        public bool ShowDescription
        {
            get { return _showdesciption; }
            set
            {
                _showdesciption = value;
                labelControl1.Visible = value;
                splitterControl1.Visible = value;
            }
        }

        private bool _editing = false;
        public bool Editing
        {
            get { return _editing; }
        }
        
        #endregion

        #region Constractor

        public ucParameterPanel()
        {
            InitializeComponent();

            vGridControl1.ShowButtonMode = DevExpress.XtraVerticalGrid.ShowButtonModeEnum.ShowAlways;

            vGridControl1.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(vGridControl1_CellValueChanged);
            vGridControl1.CellValueChanging += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(vGridControl1_CellValueChanging);
            vGridControl1.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(vGridControl1_FocusedRowChanged);

        }

        #endregion

        #region Control Events

        void vGridControl1_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            bool editing = true;
            if (e.Value != null)
                editing = !e.Value.Equals(e.Row.Properties.Value);

            DynamicParameterItem pi = (DynamicParameterItem)e.Row.Tag;
            if (pi != null) pi.Editing = editing;

            _editing = editing;
        }
        void vGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            DynamicParameterItem pi = (DynamicParameterItem)e.Row.Tag;
            if (pi != null)
            {
                if (pi.Editing)
                    e.Row.Appearance.ForeColor = System.Drawing.Color.Red;

                switch (pi.ValueType)
                {
                    case DynamicParameterType.Color:
                        //2011.06.21 ri.StoreColorAsInteger = true; болгосон тул шаардлагагүй

                        //Color c = (Color)e.Row.Properties.Value;
                        //pi.Value = string.Format("{0},{1},{2}", c.R, c.G, c.B);
                        break;
                    default:
                        pi.Value = e.Row.Properties.Value;
                        break;
                }
            }
        }
        void vGridControl1_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.Row == null) return;
                DynamicParameterItem pi = (DynamicParameterItem)e.Row.Tag;
                _row = pi;

                if (pi != null) labelControl1.Text = pi.Description;

                if (e.OldRow != null)
                {
                    DynamicParameterItem old = (DynamicParameterItem)e.OldRow.Tag;
                    OnEventRowChanged(pi.Id, e.Row.Index, pi.Value, old.Id, e.OldRow.Index, old.Value);
                }
                else
                {
                    OnEventRowChanged(pi.Id, e.Row.Index, pi.Value, pi.Id, e.Row.Index, pi.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        void riFileEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit edit = sender as DevExpress.XtraEditors.ButtonEdit;
            DynamicParameterItem pi = _row;

            if (pi != null)
            {
                switch (e.Button.Index)
                {
                    case 0:
                        string newvalue = null;
                        DialogResult dlg = ShowFileForm((string)pi.Value, ref newvalue);
                        if (dlg == DialogResult.OK)
                        {
                            pi.Editing = true;
                            if (pi.Row != null)
                            {
                                //pi.Row.Properties.Value = newvalue; 
                                edit.EditValue = newvalue;
                                pi.Value = newvalue;
                            }
                            AttachUtility.FileRead(newvalue, ref pi.AttachData);
                        }
                        break;
                    case 1:
                        if (pi != null)
                        {
                            string filename = Static.ToStr(pi.Value);
                            GetAttachData(pi);
                            if (!string.IsNullOrEmpty(filename))
                                Globals.ShellOpenFile(filename, pi.AttachData);
                        }
                        break;
                }
            }
        }
        void riFolderEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit edit = sender as DevExpress.XtraEditors.ButtonEdit;

            string newvalue = null;
            DialogResult dlg = ShowFolderForm((string)edit.EditValue, ref newvalue);
            if (dlg == DialogResult.OK)
            {
                edit.EditValue = newvalue;
            }
        }
        void riImageEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit edit = sender as DevExpress.XtraEditors.ButtonEdit;
            DynamicParameterItem pi = _row;

            #region Loading picture data from the server
            GetAttachData(pi);
            #endregion

            #region Зургийн форм дуудах

            Image newimage = null;
            Image image = null;
            string filename = null;

            if (pi.AttachData != null) image = Static.ImageFromByte(pi.AttachData);
            DialogResult dlg = ShowImageForm(image, ref newimage, ref filename);
            if (dlg == DialogResult.OK)
            {
                if (pi != null)
                {
                    //if (pi.Row != null) pi.Row.Properties.Value = filename;
                    edit.EditValue = filename;
                    pi.Editing = true;
                    pi.Value = filename;
                    pi.AttachData = Static.ImageToByte(newimage);
                }
            }

            #endregion
        }

        #endregion

        #region Custom Events

        public delegate void delegateEventValueChanged(string id, int index, object newvalue, object oldvalue);
        public event delegateEventValueChanged EventValueChanged;
        public void OnEventValueChanged(string id, int index, object newvalue, object oldvalue)
        {
            if (EventValueChanged != null) EventValueChanged(id, index, newvalue, oldvalue);
        }

        public delegate void delegateEventRowChanged(string newid, int newindex, object newvalue, string oldid, int oldindex, object oldvalue);
        public event delegateEventRowChanged EventRowChanged;
        public void OnEventRowChanged(string newid, int newindex, object newvalue, string oldid, int oldindex, object oldvalue)
        {
            if (EventRowChanged != null) EventRowChanged(newid, newindex, newvalue, oldid, oldindex, oldvalue);
        }

        public delegate void delegateEventButtonClick(string id, DynamicParameterType type, int index, object value, ulong attachid, byte[] attachdata, ref object newvalue, ref byte[] newattachdata);
        public event delegateEventButtonClick EventButtonClick;
        public void OnEventButtonClick(string id, DynamicParameterType type, int index, object value, ulong attachid, byte[] attachdata, ref object newvalue, ref byte[] newattachdata)
        {
            if (EventButtonClick != null) EventButtonClick(id, type, index, value, attachid, attachdata, ref newvalue, ref newattachdata);
        }

        public void GetAttachData(DynamicParameterItem pi)
        {
            if (pi == null) return;

            if (pi.AttachId != 0 && pi.AttachData == null)
            {
                object newvalue = null;
                byte[] newattachdata = null;

                try
                {
                    OnEventButtonClick(pi.Id, pi.ValueType, pi.Row.Index, pi.Value, pi.AttachId, pi.AttachData, ref newvalue, ref newattachdata);
                }
                catch
                { }
                if (newattachdata != null)
                {
                    pi.AttachData = newattachdata;
                }
            }
        }

        #endregion

        #region Editor Browse

        private DialogResult ShowImageForm(Image value, ref Image newvalue, ref string filename)
        {
            ISM.Template.FormImage frm = new FormImage();
            frm.Resource = _resource;
            frm.ImageObject = value;
            
            DialogResult dlg = frm.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                newvalue = frm.ImageObject;
                filename = frm.FileName;
            }
            return dlg;
        }
        private DialogResult ShowFileForm(string value, ref string newvalue)
        {
            OpenFileDialog browser = new OpenFileDialog();
            browser.Title = "My title";
            browser.FileName = value;
            DialogResult dlg = browser.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                newvalue = browser.FileName;
            }
            return dlg;
        }
        private DialogResult ShowFolderForm(string value, ref string newvalue)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            browser.Description = "My description";
            browser.ShowNewFolderButton = true;
            browser.SelectedPath = value;
            DialogResult dlg = browser.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                newvalue = browser.SelectedPath;
            }
            return dlg;
        }

        #endregion

        #region Creating Repository Items

        public RepositoryItemTextEdit CreateRepositoryTextEdit(int MaxLength, string EditMask)
        {
            RepositoryItemTextEdit ri = new RepositoryItemTextEdit();
            ri.AutoHeight = false;
            ri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            ri.MaxLength = MaxLength;
            if (!string.IsNullOrEmpty(EditMask))
            {
                ri.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                ri.EditFormat.FormatString = EditMask;
                ri.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                ri.DisplayFormat.FormatString = EditMask;
                ri.Mask.EditMask = EditMask;
                ri.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Custom;
                ri.Mask.UseMaskAsDisplayFormat = true;
            }
            return ri;
        }
        public RepositoryItemCalcEdit CreateRepositoryCalcEdit(int MaxLength, string EditMask)
        {
            RepositoryItemCalcEdit ri = new RepositoryItemCalcEdit();
            ri.Buttons.Clear();
            ri.AutoHeight = false;
            ri.EditMask = EditMask;
            ri.Mask.UseMaskAsDisplayFormat = true;
            
            return ri;
        }
        public RepositoryItemDateEdit CreateRepositoryDateEdit()
        {
            RepositoryItemDateEdit ri = new RepositoryItemDateEdit();
            ri.AutoHeight = false;
            ri.DisplayFormat.FormatString = "yyyy.MM.dd";
            ri.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ri.EditFormat.FormatString = "yyyy.MM.dd";
            ri.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;

            return ri;
        }
        public RepositoryItemDateEdit CreateRepositoryDateTimeEdit()
        {
            RepositoryItemDateEdit ri = new RepositoryItemDateEdit();
            ri.AutoHeight = false;
            ri.DisplayFormat.FormatString = "yyyy.MM.dd HH:mm:ss";
            ri.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            ri.EditFormat.FormatString = "yyyy.MM.dd HH:mm:ss";
            ri.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;

            return ri;
        }
        public RepositoryItemButtonEdit CreateRepositoryFolderEdit()
        {
            RepositoryItemButtonEdit ri = new RepositoryItemButtonEdit();
            ri.AutoHeight = false;
            ri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            return ri;
        }
        public RepositoryItemCheckEdit CreateRepositoryCheckEdit()
        {
            RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
            ri.AutoHeight = false;
            return ri;
        }
        public RepositoryItemFontEdit CreateRepositoryFontEdit()
        {
            RepositoryItemFontEdit ri = new RepositoryItemFontEdit();
            ri.AutoHeight = false;
            return ri;
        }
        public RepositoryItemColorEdit CreateRepositoryColorEdit()
        {
            RepositoryItemColorEdit ri = new RepositoryItemColorEdit();
            ri.StoreColorAsInteger = true;
            ri.AutoHeight = false;
            return ri;
        }
        public RepositoryItemLookUpEdit CreateRepositoryLookUpEdit()
        {
            RepositoryItemLookUpEdit ri = new RepositoryItemLookUpEdit();
            ri.AutoHeight = false;
            //ri.HideSelection = false;
            ri.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            ri.EditFormat.FormatType = DevExpress.Utils.FormatType.None;

            ri.CaseSensitiveSearch = false;
            ri.CharacterCasing = CharacterCasing.Upper;

            ri.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            ri.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            ri.TextEditStyle = TextEditStyles.Standard;
            
            ri.ShowHeader = false;
            ri.ShowFooter = false;

            // This must be in.
            ri.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            ri.NullText = string.Empty;


            //ri.Mask.UseMaskAsDisplayFormat = true;
            return ri;
        }

        public RepositoryItemButtonEdit CreateRepositoryFileEdit()
        {
            RepositoryItemButtonEdit ri = new RepositoryItemButtonEdit();
            ri.AutoHeight = false;
            ri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            ri.TextEditStyle = TextEditStyles.DisableTextEditor;

            ri.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Up, "Browse", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null /*serializableAppearanceObject2*/, "", null, null, true)});
            return ri;
        }
        public RepositoryItemButtonEdit CreateRepositoryImageEdit()
        {
            //RepositoryItemImageEdit ri = new RepositoryItemImageEdit();
            //ri.AutoHeight = false;
            //ri.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            //ri.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            //new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "Browse", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null /*serializableAppearanceObject2*/, "", null, null, true)});
            //ri.Buttons[0].Visible = false;

            RepositoryItemButtonEdit ri = new RepositoryItemButtonEdit();
            ri.AutoHeight = false;
            ri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            ri.TextEditStyle = TextEditStyles.DisableTextEditor;
            return ri;
        }

        #endregion

        #region Managing Methods

        public DynamicParameterItem ItemAdd(string Id, string Name, string Name2, string Value, DynamicParameterType ValueType, int ValueLength, object ValueDefault, bool Mandatory, string EditMask, string Description, string DictId, bool DictEditable, string DictValueField, string DictDescField, int Orderno)
        {
            #region Creating new instance of DynamicParameterItem
            DynamicParameterItem pi = new DynamicParameterItem();
            pi.Id = Id;
            pi.Name = Name;
            pi.Name2 = Name2;
            pi.Value = Value;
            pi.ValueType = ValueType;
            pi.ValueLength = (ValueLength <= 0 ? 12 : ValueLength);
            pi.ValueDefault = ValueDefault;
            pi.Mandatory = Mandatory;
            pi.EditMask = EditMask;
            pi.Description = Description;
            pi.DictId = DictId;
            pi.DictEditable = DictEditable;
            pi.DictValueField = DictValueField;
            pi.DictDescField = DictDescField;
            pi.Orderno = Orderno;
            #endregion

            #region Add item into list
            lock (_hitems.SyncRoot)
            {
                _hitems[Id] = pi;
                _aitems.Add(pi);
            }
            #endregion

            return pi;
        }
        public DynamicParameterItem ItemAdd(string Id, string Name, string Value, DynamicParameterType ValueType, int ValueLength, object ValueDefault, bool Mandatory, string EditMask, string Description)
        {
            return ItemAdd(Id, Name, null, Value, ValueType, ValueLength, ValueDefault, Mandatory, EditMask, Description, null, true, null, null, 0);
        }
        public DynamicParameterItem ItemAdd(string Id, string Name, string Value, DynamicParameterType ValueType, bool Mandatory, string EditMask, string Description)
        {
            return ItemAdd(Id, Name, null, Value, ValueType, 4000, null, Mandatory, EditMask, Description, null, true, null, null, 0);
        }
        public DynamicParameterItem ItemAdd(string Id, string Name, string Value, DynamicParameterType ValueType, int ValueLength, bool Mandatory)
        {
            return ItemAdd(Id, Name, null, Value, ValueType, ValueLength, null, Mandatory, null, null, null, true, null, null, 0);
        }
        public DynamicParameterItem ItemAdd(string Id, string Name, string Value, DynamicParameterType ValueType, bool Mandatory)
        {
            return ItemAdd(Id, Name, null, Value, ValueType, 4000, null, Mandatory, null, null, null, true, null, null, 0);
        }
        public void ItemRemove(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                lock (_hitems.SyncRoot)
                {
                    DynamicParameterItem pi = (DynamicParameterItem)_hitems[Id];
                    if (pi != null) _aitems.Remove(pi);
                    _hitems.Remove(Id);
                }
            }
        }
        public void ItemRemoveAll()
        {
            lock (_hitems.SyncRoot)
            {
                _hitems.Clear();
                _aitems.Clear();
            }
            vGridControl1.Rows.Clear();
        }

        public void ItemClearAll()
        {
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                vGridControl1.Rows[i].Properties.Value = null;
            }
        }
        public void ItemDefaultAll()
        {
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                DynamicParameterItem pi = (DynamicParameterItem)vGridControl1.Rows[i].Tag;
                if (pi != null)
                {
                    ItemSetValue(pi.Id, pi.ValueDefault);
                }
            }
        }
        public void ItemListSetSaved()
        {
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                vGridControl1.Rows[i].Appearance.ForeColor = System.Drawing.Color.Black;
            }
        }

        public void ItemListRefresh()
        {
            vGridControl1.Rows.Clear();
            vGridControl1.RepositoryItems.Clear();
            foreach (DynamicParameterItem pi in _aitems)
            {
                EditorRow erow = new EditorRow();
                pi.Row = erow;
                erow.Properties.Caption = pi.Name;
                erow.Tag = pi;

                if (pi.Mandatory)
                {
                    erow.Appearance.Options.UseBackColor = true;
                    erow.Appearance.BackColor = Color.FromKnownColor(KnownColor.Info);
                }
                #region Setting property values and types

                switch (pi.ValueType)
                {
                    case DynamicParameterType.Text:
                        erow.Properties.RowEdit = CreateRepositoryTextEdit(pi.ValueLength, pi.EditMask);
                        break;
                    case DynamicParameterType.Decimal:
                        erow.Properties.RowEdit = CreateRepositoryCalcEdit(pi.ValueLength, pi.EditMask);
                        break;
                    case DynamicParameterType.Date:
                        erow.Properties.RowEdit = CreateRepositoryDateEdit();
                        break;
                    case DynamicParameterType.DateTime:
                        erow.Properties.RowEdit = CreateRepositoryDateTimeEdit();
                        break;
                    case DynamicParameterType.Folder:
                        RepositoryItemButtonEdit riFolderEdit = CreateRepositoryFolderEdit();
                        riFolderEdit.ButtonPressed += new ButtonPressedEventHandler(riFolderEdit_ButtonClick);
                        erow.Properties.RowEdit = riFolderEdit;
                        break;
                    case DynamicParameterType.File:
                        RepositoryItemButtonEdit riFileEdit = CreateRepositoryFileEdit();
                        riFileEdit.ButtonPressed += new ButtonPressedEventHandler(riFileEdit_ButtonClick);
                        erow.Properties.RowEdit = riFileEdit;
                        break;
                    case DynamicParameterType.Picture:
                        RepositoryItemButtonEdit riImageEdit = CreateRepositoryImageEdit();
                        riImageEdit.ButtonPressed += new ButtonPressedEventHandler(riImageEdit_ButtonClick);
                        erow.Properties.RowEdit = riImageEdit;
                        break;
                    case DynamicParameterType.Font:
                        erow.Properties.RowEdit = CreateRepositoryFontEdit();
                        break;
                    case DynamicParameterType.Color:
                        erow.Properties.RowEdit = CreateRepositoryColorEdit();
                        break;
                    case DynamicParameterType.List:
                        erow.Properties.RowEdit = CreateRepositoryLookUpEdit();
                        RepositoryItemLookUpEdit lue = (RepositoryItemLookUpEdit)erow.Properties.RowEdit;
                        break;
                    default: // 1 and others is string type
                        erow.Properties.RowEdit = CreateRepositoryTextEdit(4000, "");
                        break;

                }
                #endregion

                vGridControl1.RepositoryItems.Add(erow.Properties.RowEdit);
                vGridControl1.Rows.Add(erow);
            }
        }

        public void ItemListRefreshDefaultValues()
        {
            foreach (DynamicParameterItem pi in _aitems)
            {
                EditorRow erow = pi.Row;

                #region Setting property values and types

                object value = null;
                switch (pi.ValueType)
                {
                    case DynamicParameterType.Text:
                        value = Static.ToStr(pi.ValueDefault);
                        break;
                    case DynamicParameterType.Decimal:
                        value = Static.ToDecimal(pi.ValueDefault);
                        break;
                    case DynamicParameterType.Date:
                        value = Static.ToDate(pi.ValueDefault);
                        if ((DateTime)value == DateTime.MinValue) value = DBNull.Value;
                        break;
                    case DynamicParameterType.DateTime:
                        value = Static.ToDateTime(pi.ValueDefault);
                        if ((DateTime)value == DateTime.MinValue) value = DBNull.Value;
                        break;
                    case DynamicParameterType.Folder:
                        value = Static.ToStr(pi.ValueDefault);
                        break;
                    case DynamicParameterType.File:
                        value = Static.ToStr(pi.ValueDefault);
                        break;
                    case DynamicParameterType.Picture:
                        value = Static.ToStr(pi.ValueDefault);
                        break;
                    case DynamicParameterType.Font:
                        value = Static.ToStr(pi.ValueDefault);
                        break;
                    case DynamicParameterType.Color:
                        RepositoryItemColorEdit ce = (RepositoryItemColorEdit)erow.Properties.RowEdit;
                        break;
                    case DynamicParameterType.List:
                        RepositoryItemLookUpEdit lue = (RepositoryItemLookUpEdit)erow.Properties.RowEdit;

                        object setvalue = DBNull.Value;
                        DataTable dt = null;
                        #region Casting DataTable/DataView into DataTable
                        if (lue.DataSource is DataView)
                        {
                            DataView dv = (DataView)lue.DataSource;
                            dt = dv.Table;
                        }
                        else
                        {
                            dt = (DataTable)lue.DataSource;
                        }
                        #endregion
                        if (dt != null && dt.Columns.Count > 0)
                        {
                            #region Get data type of ValueMember column
                            Type totype = typeof(string);
                            if (string.IsNullOrEmpty(lue.ValueMember))
                            {
                                totype = dt.Columns[0].DataType;
                            }
                            else
                            {
                                if (dt.Columns.Contains(lue.ValueMember))
                                    totype = dt.Columns[lue.ValueMember].DataType;
                            }
                            #endregion
                            #region Converting value into data type
                            setvalue = EServ.Shared.Static.ConvToType(totype, pi.ValueDefault);
                            #endregion
                        }
                        value = setvalue;
                        break;
                    default: // 1 and others is string type
                        value = Static.ToStr(pi.ValueDefault);
                        break;

                }
                #endregion
                if (pi.ValueDefault == null || pi.ValueDefault == DBNull.Value || pi.ValueDefault == "") erow.Properties.Value = null;
                else erow.Properties.Value = value;
            }
        }
        public void ItemListRefreshValues()
        {
            foreach (DynamicParameterItem pi in _aitems)
            {
                EditorRow erow = pi.Row;

                #region Setting property values and types

                object value = null;
                switch (pi.ValueType)
                {
                    case DynamicParameterType.Text:
                        value = Static.ToStr(pi.Value);
                        break;
                    case DynamicParameterType.Decimal:
                        value = Static.ToDecimal(pi.Value);
                        break;
                    case DynamicParameterType.Date:
                        value = Static.ToDate(pi.Value);
                        if ((DateTime)value == DateTime.MinValue) value = DBNull.Value;
                        break;
                    case DynamicParameterType.DateTime:
                        value = Static.ToDateTime(pi.Value);
                        if ((DateTime)value == DateTime.MinValue) value = DBNull.Value;
                        break;
                    case DynamicParameterType.Folder:
                        value = Static.ToStr(pi.Value);
                        break;
                    case DynamicParameterType.File:
                        value = Static.ToStr(pi.Value);
                        break;
                    case DynamicParameterType.Picture:
                        value = Static.ToStr(pi.Value);
                        break;
                    case DynamicParameterType.Font:
                        value = Static.ToStr(pi.Value);
                        break;
                    case DynamicParameterType.Color:
                        RepositoryItemColorEdit ce = (RepositoryItemColorEdit)erow.Properties.RowEdit;

                        //2011.06.21 //2011.06.21 ri.StoreColorAsInteger = true; болгосон тул шаардлагагүй

                        //if (pi.Value is Color) erow.Properties.Value = (Color)pi.Value;
                        //else
                        //{
                        //    string colorstr = Static.ToStr(pi.Value);
                        //    string[] rgb = (colorstr + ",,").Split(',');
                        //    erow.Properties.Value = Color.FromArgb(
                        //        Static.ToInt(rgb[0])
                        //        , Static.ToInt(rgb[1])
                        //        , Static.ToInt(rgb[2])
                        //        );
                        //}
                        break;
                    case DynamicParameterType.List:
                        RepositoryItemLookUpEdit lue = (RepositoryItemLookUpEdit)erow.Properties.RowEdit;

                        object setvalue = DBNull.Value;
                        DataTable dt = null;
                        #region Casting DataTable/DataView into DataTable
                        if (lue.DataSource is DataView)
                        {
                            DataView dv = (DataView)lue.DataSource;
                            dt = dv.Table;
                        }
                        else
                        {
                            dt = (DataTable)lue.DataSource;
                        }
                        #endregion
                        if (dt != null && dt.Columns.Count > 0)
                        {
                            #region Get data type of ValueMember column
                            Type totype = typeof(string);
                            if (string.IsNullOrEmpty(lue.ValueMember))
                            {
                                totype = dt.Columns[0].DataType;
                            }
                            else
                            {
                                if (dt.Columns.Contains(lue.ValueMember))
                                    totype = dt.Columns[lue.ValueMember].DataType;
                            }
                            #endregion
                            #region Converting value into data type
                            setvalue = EServ.Shared.Static.ConvToType(totype, pi.Value);
                            #endregion
                        }
                        value = setvalue;
                        break;
                    default: // 1 and others is string type
                        value = Static.ToStr(pi.Value);
                        break;

                }
                #endregion
                if (pi.Value == null || pi.Value == DBNull.Value || pi.Value == "") erow.Properties.Value = null;
                else erow.Properties.Value = value;
            }
        }

        public object[] ItemGetValueList()
        {
            ArrayList ar = new ArrayList();
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                ar.Add(vGridControl1.Rows[i].Properties.Value);
            }
            return ar.ToArray();
        }
        public List<DynamicParameterItem> ItemGetList()
        {
            List<DynamicParameterItem> ar = new List<DynamicParameterItem>();
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                DynamicParameterItem pi = (DynamicParameterItem)vGridControl1.Rows[i].Tag;
                if (pi != null)
                {
                    ar.Add(pi);
                }
            }
            return ar;
        }
        public List<DynamicParameterItem> ItemCheckMandatory()
        {
            List<DynamicParameterItem> ar = new List<DynamicParameterItem>();
            for (int i = 0; i < vGridControl1.Rows.Count; i++)
            {
                DynamicParameterItem pi = (DynamicParameterItem)vGridControl1.Rows[i].Tag;
                if (pi != null)
                {
                    if (pi.Mandatory)
                        if (pi.Value == null || pi.Value == DBNull.Value || ISM.Lib.Static.ToStr(pi.Value) == "")
                        {
                            ar.Add(pi);
                        }
                }
            }
            return ar;
        }

        public DynamicParameterItem ItemGet(string id)
        {
            DynamicParameterItem pi = null;
            lock (_hitems.SyncRoot)
            { pi = (DynamicParameterItem)_hitems[id]; }
            return pi;
        }
        public object ItemGetValue(string Id)
        {
            object value = null;
            DynamicParameterItem pi = ItemGet(Id);
            if (pi != null)
            {
                EditorRow row = pi.Row;
                if (row != null) value = row.Properties.Value;
            }
            return value;
        }
        public object ItemGetValue(int index)
        {
            object value = null;
            if (index >= 0 && index < vGridControl1.Rows.Count)
            {
                BaseRow row = vGridControl1.Rows[index];
                if (row != null) value = row.Properties.Value;
            }
            return value;
        }
        public void ItemSetValue(string Id, object Value)
        {
            EditorRow erow = null;
            DynamicParameterItem pi = ItemGet(Id);
            if (pi != null) erow = pi.Row; 

            if (erow != null)
            {
                if (pi != null)
                {
                    pi.Value = Value;
                }
                if (pi == null || Value == null || Value == DBNull.Value)
                {
                    erow.Properties.Value = DBNull.Value;
                }
                else
                {
                    #region Setting property values and types

                    DateTime datevalue;
                    switch (pi.ValueType)
                    {
                        case DynamicParameterType.Text:
                            erow.Properties.Value = Static.ToStr(pi.Value);
                            break;
                        case DynamicParameterType.Decimal:
                            erow.Properties.Value = Static.ToDecimal(pi.Value);
                            break;
                        case DynamicParameterType.Date:
                            datevalue = Static.ToDate(pi.Value);
                            if (datevalue != DateTime.MinValue)
                                erow.Properties.Value = datevalue;
                            break;
                        case DynamicParameterType.DateTime:
                            datevalue = Static.ToDateTime(pi.Value);
                            if (datevalue != DateTime.MinValue)
                                erow.Properties.Value = datevalue;
                            break;
                        case DynamicParameterType.File:
                            erow.Properties.Value = Static.ToStr(pi.Value);
                            break;
                        case DynamicParameterType.Folder:
                            erow.Properties.Value = Static.ToStr(pi.Value);
                            break;
                        case DynamicParameterType.Picture:
                            erow.Properties.Value = (Image)pi.Value;
                            break;
                        case DynamicParameterType.Font:
                            erow.Properties.Value = (Font)pi.Value;
                            break;
                        case DynamicParameterType.Color:
                            erow.Properties.Value = (Color)pi.Value;
                            break;
                        case DynamicParameterType.List:
                            RepositoryItemLookUpEdit lue = (RepositoryItemLookUpEdit)erow.Properties.RowEdit;
                            object setvalue = DBNull.Value;
                            if (pi.Value != null)
                            {
                                DataTable dt = null;
                                #region Casting DataTable/DataView into DataTable
                                if (lue.DataSource is DataView)
                                {
                                    DataView dv = (DataView)lue.DataSource;
                                    dt = dv.Table;
                                }
                                else
                                {
                                    dt = (DataTable)lue.DataSource;
                                }
                                #endregion
                                if (dt != null && dt.Columns.Count > 0)
                                {
                                    #region Get data type of ValueMember column
                                    Type totype = typeof(string);
                                    if (string.IsNullOrEmpty(lue.ValueMember))
                                    {
                                        totype = dt.Columns[0].DataType;
                                    }
                                    else
                                    {
                                        if (dt.Columns.Contains(lue.ValueMember))
                                            totype = dt.Columns[lue.ValueMember].DataType;
                                    }
                                    #endregion
                                    #region Converting value into data type
                                    setvalue = EServ.Shared.Static.ConvToType(totype, pi.Value);
                                    #endregion
                                }
                            }
                            erow.Properties.Value = setvalue;
                            break;
                        default: // 1 and others is string type
                            erow.Properties.Value = Static.ToStr(pi.Value);
                            break;
                    }
                    #endregion
                }
            }
        }

        public void ItemSetList(string Id, DataTable Table, string ValueField, string NameField, string Filter, int[] HiddenColumns)
        {
            try
            {
                EditorRow erow = null;
                DynamicParameterItem pi = ItemGet(Id);
                if (pi != null) erow = pi.Row;

                if (erow != null)
                {
                    if (pi.ValueType == DynamicParameterType.List)
                    {
                        RepositoryItemLookUpEdit edit = (RepositoryItemLookUpEdit)erow.Properties.RowEdit;
                        if (pi.ValueType == DynamicParameterType.List)
                        {
                            FormUtility.LookUpEdit_SetList(ref edit, Table, ValueField.ToUpper(), NameField.ToUpper());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ItemSetList(string Id, DataTable Table, string ValueField, string NameField)
        {
            ItemSetList(Id, Table, ValueField, NameField, "", null);
        }
        public void ItemSetList(string Id, DataTable Table, string ValueField)
        {
            ItemSetList(Id, Table, ValueField, ValueField, "", null);
        }
        public void ItemSetList(string Id, object Value, object Name)
        {
            try
            {
                EditorRow erow = null;
                DynamicParameterItem pi = ItemGet(Id);
                if (pi != null) erow = pi.Row;

                if (erow != null)
                {
                    if (pi.ValueType == DynamicParameterType.List)
                    {
                        RepositoryItemLookUpEdit edit = (RepositoryItemLookUpEdit)erow.Properties.RowEdit;
                        FormUtility.LookUpEdit_SetList(ref edit, Value, Name);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public Result ItemSetListFromDictionary(EServ.Client client, int userno)
        {
            Result r = null;

            #region Read Dictionary names

            List<string> ids = new List<string>();
            List<DynamicParameterItem> list = ItemGetList();
            foreach (DynamicParameterItem pi in list)
            {
                if (!string.IsNullOrEmpty(pi.DictId))
                {
                    ids.Add(pi.DictId);
                }
            }

            #endregion

            #region Get Dictionary Tables
            ArrayList tables = null; 
            r = DictUtility.Get(client, userno, ids.ToArray(), ref tables);
            #endregion

            #region Set Dictionary table into control

            if (r.ResultNo == 0)
            {
                int i = 0;
                foreach (DynamicParameterItem pi in list)
                {
                    if (!string.IsNullOrEmpty(pi.DictId))
                    {
                        this.ItemSetList(pi.Id, (DataTable)tables[i], pi.DictValueField, pi.DictDescField);
                        i++;
                    }
                }
            }

            #endregion

            return r;
        }
        public Result ItemSetListFromDictionary(CUser.Remote remote)
        {
            return ItemSetListFromDictionary(remote.Connection, remote.User.UserNo);
        }

        #endregion

        private void splitterControl1_DoubleClick(object sender, EventArgs e)
        {
            if (splitterControl1.Visible)
            {
                labelControl1.Visible = labelControl1.Visible ? false : true;
            }
        }
    }

}

