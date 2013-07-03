using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.JScript.Vsa;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Persistent;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using EServ.Shared;
namespace ISM.Template
{
    public partial class ucDynamicGridPanel : UserControl
    {
        int START_INDEX_DYNAMIC_COL = 25;
      //----------------20120508---------------
        Hashtable FormulaHash = new Hashtable();
        Hashtable RateHash = new Hashtable();
        ArrayList calccolumn = new ArrayList();
        Hashtable rowcalhash = new Hashtable();
        Hashtable ParentHash = new Hashtable();
        Hashtable InverseParentHash = new Hashtable();
        Hashtable LookUpHash = new Hashtable();
      //----------------------------------------
        DataTable _tbl_pivot = null;
        DataTable _tbl_def = null;
        int[] _merge_flag = new int[256];
        int rowno = 0;
        int column = 0;
        DataTable olddata = null;

        #region Constants

        const int CONST_FILEID = 105;

        #endregion

        #region Properties

        private CUser.Remote _remote = null;
        [DefaultValue(null), Browsable(false)]
        public CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }
        private Resource _resource = null;
        [DefaultValue(null), Browsable(false)]
        public Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        [DefaultValue(null), Browsable(false)]
        public DataRow SelectedRow
        {
            get { return gridView1.GetFocusedDataRow(); }
        }

        [DefaultValue(null), Browsable(false)]
        public DevExpress.XtraGrid.Views.Grid.GridView GridView
        {
            get { return gridView1; }
        }

        private bool _showfixedvalues = true;
        public bool ShowFixedValues
        {
            get { return _showfixedvalues; }
            set { _showfixedvalues = value; }
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

        private int _objectid = 0;
        [DefaultValue(0), Browsable(true)]
        public int ObjectId
        {
            get { return _objectid; }
            set { _objectid = value; }
        }

        private int _prowno = 0;
        [DefaultValue(0), Browsable(true)]
        public int ParentRowNo
        {
            get { return _prowno; }
            set { _prowno = value; }
        }

        [DefaultValue(0), Browsable(false)]
        public int RowNo
        {
            get
            {
                int handle = gridView1.GetRowHandle(gridView1.GetFocusedDataSourceRowIndex());
                int rowno = ISM.Lib.Static.ToInt(gridView1.GetRowCellValue(handle, "rowno"));
                return rowno;
            }
        }

        private ulong _linkid = 0;
        [DefaultValue(0), Browsable(true)]
        public ulong LinkId
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

        [DefaultValue(false), Browsable(true)]
        public bool VisibleFind
        {
            get
            {
                return gridView1.OptionsView.ShowAutoFilterRow;
            }
            set
            {
                gridView1.OptionsView.ShowAutoFilterRow = value;
                gridView1.OptionsView.ShowFilterPanelMode = value ?
                    DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
                    :
                    DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            }
        }
        private ulong _prodcode = 0;
        [DefaultValue(0), Browsable(true)]
        public ulong ProdCode
        {
            get { return _prodcode; }
            set { _prodcode = value; }
        }
        #endregion

        #region Constractor

        public ucDynamicGridPanel()
        {
            InitializeComponent();

            gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanging);
            gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView1_CellValueChanged);
            gridView1.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(gridView1_CellMerge);
            gridView1.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(gridView1_CustomRowCellEdit);
        }

        void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (ParentHash.ContainsKey(e.Column.AbsoluteIndex))
            {
                if (e.RepositoryItem is RepositoryItemLookUpEdit)
                {
                    try
                    {
                        int.Parse(Static.ToStr(gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[string.Format("C{0}", ParentHash[e.Column.AbsoluteIndex])])));
                    }
                    catch
                    {
                        MessageBox.Show("Оруулсан утгаа шалгана уу.Формат таарахгүй байна.");
                        return;
                    }
                    string filtervalue = Static.ToStr(gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[string.Format("C{0}", ParentHash[e.Column.AbsoluteIndex])]));
                    e.RepositoryItem.Name = "Column" + e.Column.AbsoluteIndex.ToString() + "-" + "Row" + e.RowHandle.ToString();
                    RepositoryItemLookUpEdit relookupedit = (RepositoryItemLookUpEdit)e.RepositoryItem;
                    relookupedit.Name = "Row" + e.RowHandle.ToString() + "-" + "Column" + e.Column.AbsoluteIndex.ToString();
                    DataTable dt = (DataTable)LookUpHash[e.Column.AbsoluteIndex];
                    DataView DV = new DataView((DataTable)LookUpHash[e.Column.AbsoluteIndex]);
                    if (filtervalue != "")
                        DV.RowFilter = string.Format("{0}={1}", e.Column.AppearanceCell.Name, filtervalue);
                    else
                    {
                        MessageBox.Show(gridView1.Columns[string.Format("C{0}", ParentHash[e.Column.AbsoluteIndex])].GetCaption() + " сонгоно уу.");
                        DV = null;
                    }
                    relookupedit.DataSource = DV;
                    e.RepositoryItem = relookupedit;
                    column = e.Column.AbsoluteIndex;
                }
            }
        }

      #endregion

        #region Control Events

        void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //bool editing = true;
            //if (e.Value != null)
            //    editing = !e.Value.Equals(e.Row.Properties.Value);

            //DynamicParameterItem pi = (DynamicParameterItem)e.Row.Tag;
            //if (pi != null) pi.Editing = editing;

            //_editing = editing;
        }
        void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (InverseParentHash.ContainsKey(Static.ToInt(e.Column.FieldName.Replace("C",""))))
            {
                gridView1.SetRowCellValue(e.RowHandle, gridView1.Columns[Static.ToInt(InverseParentHash[Static.ToInt(e.Column.FieldName.Replace("C", ""))])], null);
            }
            if (gridView1.Columns[column].ColumnEdit is RepositoryItemLookUpEdit)
            {
                RepositoryItemLookUpEdit newsad = (RepositoryItemLookUpEdit)gridView1.Columns[column].ColumnEdit;
                newsad.DataSource = (DataTable)LookUpHash[column];
                gridView1.Columns[column].ColumnEdit = newsad;
            }
        }
        void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.AbsoluteIndex < 0 || e.Column.AbsoluteIndex >= _merge_flag.Length) return;

            if (_merge_flag[e.Column.AbsoluteIndex] == 1)
            {
                object value1 = gridView1.GetRowCellValue(e.RowHandle1, e.Column);
                object value2 = gridView1.GetRowCellValue(e.RowHandle2, e.Column);
                if (value1.Equals(value2))
                {
                    e.Merge = true;
                    e.Handled = true;
                }
            }
        }

        void riFileEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit edit = sender as DevExpress.XtraEditors.ButtonEdit;

            string newvalue = null;
            DialogResult dlg = ShowFileForm((string)edit.EditValue, ref newvalue);
            if (dlg == DialogResult.OK)
            {
                edit.EditValue = newvalue;
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
            DevExpress.XtraEditors.ImageEdit edit = sender as DevExpress.XtraEditors.ImageEdit;
            DynamicParameterItem pi = null; // _row;
            try
            {
                if (pi != null)
                {
                    object newobj = null;
                    OnEventButtonClick(pi.Id, pi.Row.Index, pi.Value, ref newobj);
                    if (newobj != null && !(newobj is DBNull))
                    {
                        #region Нүдэнд Зураг олгоход CellValueChanged эвэнт дуудагддаггүй юм байна. Тиймээс хэрэв эвэнтээр шинэ зураг орж ирвэл, олгох
                        edit.EditValue = newobj;
                        pi.Value = newobj;
                        pi.Editing = true;
                        #endregion
                    }
                }
            }
            catch
            { }

            #region Зургийн форм дуудах

            Image newvalue = null;
            Image value = null;
            if (edit.EditValue != null && !(edit.EditValue is DBNull))
                value = (Image)edit.EditValue;

            DialogResult dlg = ShowImageForm(value, ref newvalue);
            if (dlg == DialogResult.OK)
            {
                edit.EditValue = newvalue;
                if (pi != null)
                {
                    pi.Editing = true;
                    pi.Value = newvalue;
                }
            }

            #endregion
        }
        void riRegisterEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            //ButtonEdit Editor = sender as ButtonEdit;
            //if (e.Button.Index == 0)
            //{
            //    HeavenPro.List.CustomerList frm = new HeavenPro.List.CustomerList(_core);
            //    frm.ucCustomerList.Browsable = true;

            //    DialogResult res = frm.ShowDialog();
            //    if ((res == System.Windows.Forms.DialogResult.OK))
            //    {
            //        Editor.EditValue = Static.ToStr(frm.ucCustomerList.SelectedRow["REGISTERNO"]);
            //    }
            //}
            //else
            //{
            //    if (Editor.EditValue != null)
            //    {
            //        object[] values = new object[15];
            //        if (Editor.EditValue != null)
            //        {
            //            values[6] = Editor.EditValue.ToString();
            //            Result r = _core.moRemote.Connection.Call(_remote.User.UserNo, 206, 120000, 120000, 0, 1, values);
            //            if (r.ResultNo == 0)
            //            {
            //                object[] obj = new object[3];
            //                obj[0] = _core;
            //                obj[1] = Static.ToLong(r.Data.Tables[0].Rows[0]["CustomerNo"]);
            //                obj[2] = r.Data.Tables[0].Rows[0];
            //                EServ.Shared.Static.Invoke("HeavenPro.Enquiry.dll", "HeavenPro.Enquiry.Main", "CallCustomerEnquiry", obj);
            //            }
            //        }
            //    }
            //}
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

        public delegate void delegateEventButtonClick(string id, int index, object value, ref object newvalue);
        public event delegateEventButtonClick EventButtonClick;
        public void OnEventButtonClick(string id, int index, object value, ref object newvalue)
        {
            if (EventButtonClick != null) EventButtonClick(id, index, value, ref newvalue);
        }

        public delegate void delegateEventDataChanged();
        public event delegateEventDataChanged EventDataChanged;
        public void OnEventDataChanged()
        {
            if (EventDataChanged != null) EventDataChanged();
        }

        public delegate void delegateEventRegButtonClick(object sender,ButtonPressedEventArgs e);
        public event delegateEventRegButtonClick EventRegButtonClick;
        
        #endregion

        #region Editor Browse

        private DialogResult ShowImageForm(Image value, ref Image newvalue)
        {
            ISM.Template.FormImage frm = new FormImage();
            frm.Resource = _resource;
            frm.ImageObject = value;

            DialogResult dlg = frm.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                newvalue = frm.ImageObject;
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
                ri.Mask.EditMask = EditMask;
                ri.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
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

            ri.EditMask = "yyyy.MM.dd";
            ri.Mask.UseMaskAsDisplayFormat = true;

            return ri;
        }
        public RepositoryItemDateEdit CreateRepositoryDateTimeEdit()
        {
            RepositoryItemDateEdit ri = new RepositoryItemDateEdit();
            ri.AutoHeight = false;
            ri.EditMask = "yyyy.MM.dd HH:mm:ss";
            ri.Mask.UseMaskAsDisplayFormat = true;

            return ri;
        }
        public RepositoryItemButtonEdit CreateRepositoryFileEdit()
        {
            RepositoryItemButtonEdit ri = new RepositoryItemButtonEdit();
            ri.AutoHeight = false;
            ri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            ri.TextEditStyle = TextEditStyles.DisableTextEditor;
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
             //This must be in.
            ri.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            ri.NullText = string.Empty;
            ri.Mask.UseMaskAsDisplayFormat = true;
            return ri;
        }

        public RepositoryItemImageEdit CreateRepositoryImageEdit()
        {
            RepositoryItemImageEdit ri = new RepositoryItemImageEdit();
            ri.AutoHeight = false;
            ri.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            ri.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "Browse", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null /*serializableAppearanceObject2*/, "", null, null, true)});
            ri.Buttons[0].Visible = false;

            return ri;
        }
        public RepositoryItemButtonEdit CreateRepositoryRegisterEdit()
        {
            RepositoryItemButtonEdit ri = new RepositoryItemButtonEdit();
            ri.AutoHeight = false;
            ri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            ri.TextEditStyle = TextEditStyles.DisableTextEditor;

            ri.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Up, "Browse", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null /*serializableAppearanceObject2*/, "", null, null, true)});
            return ri;
        }

        #endregion
        
        #region Server Methods

        public Result GridRead()
        {
            Result res = null;
            try
            {
                if (_remote != null)
                {
                    #region Preparing Calling Parameters
                    object[] param = new object[] { _tableprefix, _tabletypeid, _tablerowkey, _objectid, _prowno, _showfixedvalues, _prodcode };
                    #endregion
                    #region Call Server Function
                    res = _remote.Connection.Call(
                        _remote.User.UserNo
                        , CONST_FILEID
                        , 105001
                        , _tableprivselect
                        , param
                        );
                    #endregion
                    #region Set Pivot table into Grid
                    if (res.ResultNo == 0)
                    {
                        if (res.Data != null && res.Data.Tables.Count > 0)
                        {
                            _tbl_def = res.Data.Tables[0];
                            _tbl_pivot = res.Data.Tables[1];

                            gridControl1.DataSource = null;
                            gridControl1.DataSource = _tbl_pivot;
                            gridView1.PopulateColumns();

                            SetFormatPivot(_tbl_def);
                            SetComboList(_tbl_def);

                            OnEventDataChanged();
                            Read();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }

            return res;
        }
        private void Read()
        {            
            string formula = "";
            rowcalhash.Clear();
            try
            {
                if (gridControl1.DataSource != null)
                {
                    if (FormulaHash.Count != 0)
                    {
                        DataTable dt = (DataTable)gridControl1.DataSource;
                        int rowindex = 0;
                        int signindex = 0;
                        string colname = "";
                        decimal CALCAMOUNT = 1;
                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            for (int j = 0; j < calccolumn.Count; j++)
                            {
                                if (FormulaHash.ContainsKey(Static.ToInt(dt.Rows[i][Static.ToInt(calccolumn[j])])))
                                {
                                    formula = Static.ToStr(FormulaHash[Static.ToInt(dt.Rows[i][Static.ToInt(calccolumn[j])])]);
                                    if (formula.Contains(gridView1.Columns[Static.ToInt(calccolumn[j])].FieldName))
                                    {
                                        MessageBox.Show(string.Format("{0} Багананы томъёон дээр өөрийгөө оруулсан байна.\r\nСистемийн админтай холбоо барина уу.", gridView1.Columns[Static.ToInt(calccolumn[j])].GetCaption()), "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                                else
                                {
                                    if (formula.Contains(gridView1.Columns[Static.ToInt(calccolumn[j])].FieldName))
                                    {
                                        formula = formula.Replace(gridView1.Columns[Static.ToInt(calccolumn[j])].FieldName, Static.ToStr(RateHash[Static.ToInt(dt.Rows[i][Static.ToInt(calccolumn[j])])]));
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(formula))
                            {
                                rowcalhash.Add(Static.ToInt(dt.Rows[i]["ROWNO"]), Calc(formula));
                                formula = string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(string.Format("Тооцоолол хийж байх үед алдаа гарлаа. [{0},{1}]\r\nСистемийн админтай холбоо барина уу.", EX.Message, formula), "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Result GridSave()
        {
            Result res = null;
            try
            {
                if (_remote != null)
                {
                    #region Check mandatory fields
                    for (int c = 0; c < gridView1.Columns.Count; c++)
                    {
                        if (Convert.ToString(gridView1.Columns[c].ToolTip) == "mandatory")
                        {
                            foreach (DataRow row in _tbl_pivot.Rows)
                            {
                                if (row[c] == null || row[c] == DBNull.Value || ISM.Lib.Static.ToStr(row[c]) == "")
                                {
                                    res = new Result(9, string.Format("[{0}] баганын утгыг бүрэн оруулаагүй байна.", gridView1.Columns[c].GetCaption()));
                                    return res;
                                }
                            }
                        }
                    }
                    #endregion
                    #region Preparing Calling Parameters
                    object[] param = new object[] { _tableprefix, _tablerowkey, _objectid, _prowno, _showfixedvalues, _tbl_pivot };
                    #endregion
                    #region Call Server Function
                    res = _remote.Connection.Call(
                        _remote.User.UserNo
                        , CONST_FILEID
                        , 105002
                        , _tableprivupdate
                        , param
                        );
                    Read();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        public decimal RowCalcHash(int RowNo)
        {
            if (rowcalhash.ContainsKey(RowNo))
                return Static.ToDecimal(rowcalhash[RowNo]);
            else
            {
                return 0;
            }
        }
        private int max_rowno = -1;
        public void GridRowAdd()
        {
            if (max_rowno < 0)
            {
                if (_remote != null)
                {
                    #region Preparing Calling Parameters
                    object[] param = new object[] { _tableprefix, _tablerowkey, _objectid, _prowno };
                    #endregion
                    #region Call Server Function
                    Result res = _remote.Connection.Call(
                        _remote.User.UserNo
                        , CONST_FILEID
                        , 105004
                        , _tableprivselect
                        , param
                        );
                    #endregion
                    if (res.ResultNo != 0)
                    {
                        throw new Exception(res.ResultDesc);
                    }
                    DataTable dt = res.Data.Tables[0];
                    max_rowno = ISM.Lib.Static.ToInt(dt.Rows[0][0]);
                }
            }

            gridView1.AddNewRow();
            gridView1.UpdateCurrentRow();

            if (_tbl_pivot.Rows.Count > 0)
            {
                max_rowno++;
                _tbl_pivot.Rows[_tbl_pivot.Rows.Count - 1]["rowno"] = max_rowno;
                for (int columnindex = 0; columnindex < gridView1.Columns.Count; columnindex++)
                {
                    if (gridView1.Columns[columnindex].Tag != null && gridView1.Columns[columnindex].Tag != "")
                        _tbl_pivot.Rows[_tbl_pivot.Rows.Count - 1][gridView1.Columns[columnindex].FieldName] = gridView1.Columns[columnindex].Tag;
                }
            }
        }
        public void GridRowRemove(int index)
        {
            if (index < 0 || index > gridView1.RowCount) return;
            int handle = gridView1.GetRowHandle(index);
            int rowno = ISM.Lib.Static.ToInt(gridView1.GetRowCellValue(handle, "rowno"));

            if (_remote != null)
            {
                #region Preparing Calling Parameters
                object[] param = new object[] { _tableprefix, _tablerowkey, _objectid, _prowno, rowno};
                #endregion
                #region Call Server Function
                Result res = _remote.Connection.Call(
                    _remote.User.UserNo
                    , CONST_FILEID
                    , 105003
                    , _tableprivselect
                    , param
                    );
                #endregion
                if (res.ResultNo != 0)
                {
                    throw new Exception(res.ResultDesc);
                }
            }

            gridView1.DeleteRow(handle);
        }
        public void GridRowRemove()
        {
            GridRowRemove(gridView1.GetFocusedDataSourceRowIndex());
        }

        #endregion

        #region Methods
        
        private void SetFormatPivot(DataTable objectitem)
        {
            int rowindex = 1; 

            gridControl1.RepositoryItems.Clear();
            ParentHash.Clear();
            InverseParentHash.Clear();
            gridView1.Columns[0].Visible = false;

            #region Formatting Fixed Value Columns
            
            if (_showfixedvalues)
            {
                gridView1.Columns[1].ColumnEdit = CreateRepositoryCalcEdit(1, "0");   //status
                gridView1.Columns[2].ColumnEdit = CreateRepositoryDateEdit();   //startdate
                gridView1.Columns[3].ColumnEdit = CreateRepositoryDateEdit();   //enddate
                gridView1.Columns[4].ColumnEdit = CreateRepositoryCalcEdit(12, "###,###,###,##0.00"); //estimateamount
                gridView1.Columns[5].ColumnEdit = CreateRepositoryLookUpEdit(); //estimatecurcode
                gridView1.Columns[6].ColumnEdit = CreateRepositoryLookUpEdit(); //feetype
                gridView1.Columns[7].ColumnEdit = CreateRepositoryCalcEdit(3, "###,###,###,##0.00"); //feerate
                gridView1.Columns[8].ColumnEdit = CreateRepositoryCalcEdit(12, "###,###,###,##0.00"); //feeamount
                gridView1.Columns[9].ColumnEdit = CreateRepositoryLookUpEdit(); //feecurcode
                gridView1.Columns[10].ColumnEdit = CreateRepositoryCalcEdit(12, "###,###,###,##0.00"); //currate
                gridView1.Columns[11].ColumnEdit = CreateRepositoryCalcEdit(3, "###,###,###,##0.00"); //discountrate
                gridView1.Columns[12].ColumnEdit = CreateRepositoryCalcEdit(12, "###,###,###,##0.00"); //discountamount
                gridView1.Columns[13].ColumnEdit = CreateRepositoryLookUpEdit(); //discountcurcode
                gridView1.Columns[14].ColumnEdit = CreateRepositoryCalcEdit(10, null);  //OptionID
                gridView1.Columns[15].ColumnEdit = CreateRepositoryCalcEdit(1, "0");  //feestatus
                gridView1.Columns[16].ColumnEdit = CreateRepositoryCalcEdit(1, "0");  //feediscounttype
                gridView1.Columns[17].ColumnEdit = CreateRepositoryCalcEdit(12, "###,###,###,##0.00");  //feediscountamount
                gridView1.Columns[18].ColumnEdit = CreateRepositoryCalcEdit(3, "###,###,###,##0.00");  //feediscountrate
                gridView1.Columns[19].ColumnEdit = CreateRepositoryCalcEdit(12, "###,###,###,##0.00");  //calcamount
                gridView1.Columns[20].ColumnEdit = CreateRepositoryCalcEdit(3, "###,###,###,##0.00");  //calcrate
                gridView1.Columns[21].ColumnEdit = CreateRepositoryCalcEdit(10, null);  //UnOptionID
                gridView1.Columns[22].ColumnEdit = CreateRepositoryCalcEdit(12, "###,###,###,##0.00");  //ClaimAmount
                gridView1.Columns[23].ColumnEdit = CreateRepositoryCalcEdit(12, "###,###,###,##0.00");  //MarketValue
                gridView1.Columns[24].ColumnEdit = CreateRepositoryCalcEdit(16, null);  //MarketValue
                for (int i = 0; i < START_INDEX_DYNAMIC_COL; i++)
                {
                    gridControl1.RepositoryItems.Add(gridView1.Columns[i].ColumnEdit);
                }
                rowindex = START_INDEX_DYNAMIC_COL;
            }

            #endregion

            #region Formatting Dynamic Value Columns

            if (objectitem != null)
            {
                foreach (DataRow row in objectitem.Rows)
                {
                    #region Get row values
                    DynamicParameterType ValueType = (DynamicParameterType)Static.ToInt(row["valuetype"]);
                    int ValueLength = Static.ToInt(row["valuelength"]);
                    string EditMask = Static.ToStr(row["editmask"]);
                    bool IsMandatory = Static.ToInt(row["ismandatory"]) == 1;
                    bool IsCalculate = Static.ToInt(row["calculate"]) == 1;
                    if (Static.ToInt(row["dictparentobject"]) != 0 && row["dictparentobject"] != null && Static.ToStr(row["dictfilterdesc"]) != "")
                    {
                        ParentHash.Add(gridView1.Columns[rowindex].AbsoluteIndex, Static.ToInt(row["dictparentobject"]));
                        InverseParentHash.Add(Static.ToInt(row["dictparentobject"]), gridView1.Columns[rowindex].AbsoluteIndex);
                        gridView1.Columns[rowindex].AppearanceCell.Name = Static.ToStr(row["dictfilterdesc"]);
                    }
                    #endregion
                    #region Setting property values and types
                    switch (ValueType)
                    {
                        case DynamicParameterType.Text:
                            gridView1.Columns[rowindex].ColumnEdit = CreateRepositoryTextEdit(ValueLength, EditMask);
                            if (Static.ToStr(row["valuedefault"]) != "")
                                gridView1.Columns[rowindex].Tag = Static.ToStr(row["valuedefault"]);
                            break;
                        case DynamicParameterType.Decimal:
                            gridView1.Columns[rowindex].ColumnEdit = CreateRepositoryCalcEdit(ValueLength, EditMask);
                            if (Static.ToStr(row["valuedefault"]) != "")
                            gridView1.Columns[rowindex].Tag = Static.ToDecimal(row["valuedefault"]);
                            break;
                        case DynamicParameterType.Date:
                            gridView1.Columns[rowindex].ColumnEdit = CreateRepositoryDateEdit();
                            if (Static.ToStr(row["valuedefault"]) != "")
                            gridView1.Columns[rowindex].Tag = Static.ToDate(row["valuedefault"]);
                            break;
                        case DynamicParameterType.DateTime:
                            gridView1.Columns[rowindex].ColumnEdit = CreateRepositoryDateTimeEdit();
                            if (Static.ToStr(row["valuedefault"]) != "")
                            gridView1.Columns[rowindex].Tag = Static.ToDateTime(row["valuedefault"]);
                            break;
                        case DynamicParameterType.File:
                            RepositoryItemButtonEdit riFileEdit = CreateRepositoryFileEdit();
                            riFileEdit.ButtonPressed += new ButtonPressedEventHandler(riFileEdit_ButtonClick);
                            gridView1.Columns[rowindex].ColumnEdit = riFileEdit;
                            break;
                        case DynamicParameterType.Folder:
                            RepositoryItemButtonEdit riFolderEdit = CreateRepositoryFolderEdit();
                            riFolderEdit.ButtonPressed += new ButtonPressedEventHandler(riFolderEdit_ButtonClick);
                            gridView1.Columns[rowindex].ColumnEdit = riFolderEdit;
                            break;
                        case DynamicParameterType.Picture:
                            RepositoryItemImageEdit riImageEdit = CreateRepositoryImageEdit();
                            riImageEdit.ButtonPressed += new ButtonPressedEventHandler(riImageEdit_ButtonClick);
                            gridView1.Columns[rowindex].ColumnEdit = riImageEdit;
                            break;
                        case DynamicParameterType.Font:
                            gridView1.Columns[rowindex].ColumnEdit = CreateRepositoryFontEdit();
                            break;
                        case DynamicParameterType.Color:
                            gridView1.Columns[rowindex].ColumnEdit = CreateRepositoryColorEdit();
                            break;
                        case DynamicParameterType.List:
                            gridView1.Columns[rowindex].ColumnEdit = CreateRepositoryLookUpEdit();
                            if (Static.ToStr(row["valuedefault"]) != "")
                                gridView1.Columns[rowindex].Tag = Static.ToStr(row["valuedefault"]);
                            //gridView1.Columns[rowindex].Tag = "List";
                            break;
                        case DynamicParameterType.Register:
                            RepositoryItemButtonEdit riRegisterEdit = CreateRepositoryRegisterEdit();
                            riRegisterEdit.ButtonPressed += new ButtonPressedEventHandler(riRegisterEdit_ButtonPressed);
                            gridView1.Columns[rowindex].ColumnEdit = riRegisterEdit;
                            break;
                        default: // 1 and others is string type
                            gridView1.Columns[rowindex].ColumnEdit = CreateRepositoryTextEdit(4000, "");
                            if (Static.ToStr(row["valuedefault"]) != "")
                            gridView1.Columns[rowindex].Tag = Static.ToDecimal(row["valuedefault"]);
                            break;
                    }
                    #endregion

                    gridControl1.RepositoryItems.Add(gridView1.Columns[rowindex].ColumnEdit);
                    if (IsMandatory)
                    {
                        gridView1.Columns[rowindex].AppearanceCell.BackColor = Color.FromKnownColor(KnownColor.Info);
                        gridView1.Columns[rowindex].ToolTip = "mandatory";
                    }
                    if (IsCalculate)
                    {
                        calccolumn.Add(gridView1.Columns[rowindex].ColumnHandle);
                    }
                    rowindex++;
                }
            }
            #endregion

            ISM.Template.FormUtility.SetFormatGrid(ref gridView1, true);
        }


        private void SetComboList(DataTable definition)
        {
            Result r = null;
            #region Read Dictionary names

            List<string> ids = new List<string>();
            foreach (DataRow row in definition.Rows)
            {
                if (ISM.Lib.Static.ToInt(row["valuetype"]) == 9 && !string.IsNullOrEmpty(ISM.Lib.Static.ToStr(row["dictid"])))
                {
                    ids.Add(ISM.Lib.Static.ToStr(row["dictid"]));
                }
            }

            int icur = ids.IndexOf("CURRENCY");
            if (icur < 0)
            {
                ids.Add("CURRENCY");
                icur = ids.Count - 1;
            }
            int ifee = ids.IndexOf("FEETYPE");
            if (ifee < 0)
            {
                ids.Add("FEETYPE");
                ifee = ids.Count - 1;
            }

            #endregion
            #region Get Dictionary Tables
            ArrayList tables = null;
            r = DictUtility.Get(_remote, ids.ToArray(), ref tables);
            #endregion
            #region Set Dictionary Table into Dynamic Column
            try
            {
                if (r.ResultNo == 0)
                {
                    FormulaHash.Clear();
                    LookUpHash.Clear();
                    RateHash.Clear();
                    int i = 0;
                    foreach (DataRow row in definition.Rows)
                    {
                        if (ISM.Lib.Static.ToInt(row["valuetype"]) == 9 && !string.IsNullOrEmpty(ISM.Lib.Static.ToStr(row["dictid"])))
                        {
                            string colname = string.Format("C{0}", ISM.Lib.Static.ToStr(row["itemid"]));
                            DevExpress.XtraGrid.Columns.GridColumn col = gridView1.Columns[colname];
                            if (col.ColumnEdit is RepositoryItemLookUpEdit)
                            {
                                FormUtility.Column_SetList(ref gridView1, col.AbsoluteIndex, (DataTable)tables[i]
                                    , ISM.Lib.Static.ToStr(row["dictvaluefield"])
                                    , ISM.Lib.Static.ToStr(row["dictdescfield"]), new int[] { 2 });
                                LookUpHash.Add(col.AbsoluteIndex, (DataTable)tables[i]);
                                //Тооцоолол хийхийн тулд томъёо болон хувийг авч байгаа хэсэг
                                DataTable lkuptable = (DataTable)tables[i];
                                if (Static.ToInt(row["calculate"]) == 1)
                                {
                                    foreach (DataRow dr in lkuptable.Rows)
                                    {
                                        foreach (DataColumn column in lkuptable.Columns)
                                        {
                                            if (column.ColumnName == "FORMULA")
                                            {
                                                if (Static.ToStr(dr["FORMULA"]) != "")
                                                    FormulaHash.Add(Static.ToInt(dr["ID"]), Static.ToStr(dr["FORMULA"]));
                                                else
                                                {
                                                    MessageBox.Show(string.Format("{0} талбарт томъёо оруулж өгөөгүй байна \r\nСистемийн админтай холбоо барина уу.", dr["NAME"]), "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    this.Enabled = false;
                                                }
                                            }
                                            if (column.ColumnName == "RATE")
                                            {
                                                if (Static.ToStr(dr["RATE"]) != "")
                                                    RateHash.Add(Static.ToInt(dr["ID"]), Static.ToStr(dr["RATE"]));
                                                else
                                                {
                                                    MessageBox.Show(string.Format("{0} талбарт хэмжээ оруулж өгөөгүй байна \r\nСистемийн админтай холбоо барина уу.", dr["NAME"]), "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    this.Enabled = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(r.ResultNo + ":" + r.ResultDesc + " Dictionary-д оруулаагүй байна");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Алдаа гарлаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
            #region Set Dictionary Table into Fixed Column

            if (r.ResultNo == 0 && _showfixedvalues)
            {
                FormUtility.Column_SetList(ref gridView1, 5, (DataTable)tables[icur], "CURRENCY", "NAME");
                FormUtility.Column_SetList(ref gridView1, 9, (DataTable)tables[icur], "CURRENCY", "NAME");
                FormUtility.Column_SetList(ref gridView1, 13, (DataTable)tables[icur], "CURRENCY", "NAME");
                FormUtility.Column_SetList(ref gridView1, 6, (DataTable)tables[ifee], "TYPECODE", "NAME");
            }

            #endregion
        }

        public DevExpress.XtraGrid.Columns.GridColumn GetColumn(int index)
        {
            DevExpress.XtraGrid.Columns.GridColumn col = null;
            if (index >= 0 && index < gridView1.Columns.Count)
                col = gridView1.Columns[index];
            return col;
        }
        public void MergeColumn(int index, bool merge)
        {
            if (index >= 0 && index < _merge_flag.Length && index < gridView1.Columns.Count)
            {
                if (merge)
                {
                    gridView1.Columns[index].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    _merge_flag[index] = 1;
                }
                else
                {
                    gridView1.Columns[index].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.Default;
                    _merge_flag[index] = 0;
                }
            }
        }
        #endregion

        #region[Function]
        public static VsaEngine _engine = VsaEngine.CreateEngine();
        public static double Calc(string expr)
        {
            return double.Parse(Microsoft.JScript.Eval.JScriptEvaluate(expr, _engine).ToString());
        }
        #endregion
    }
}
