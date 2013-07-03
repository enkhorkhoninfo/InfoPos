using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid;

using EServ.Shared;

namespace ISM.Template
{
    public class FormUtility
    {
        #region Formatting - LookUpEdit

        static public void LookUpEdit_SetValue(ref LookUpEdit lookUpEdit, object value)
        {
            if (string.IsNullOrEmpty(lookUpEdit.Properties.ValueMember)) return;

            Type totype = null;
            DataTable dt = (DataTable)lookUpEdit.Properties.DataSource;

            #region Find ValueMember column
            if (dt.Columns.Contains(lookUpEdit.Properties.ValueMember))
            {
                totype = dt.Columns[lookUpEdit.Properties.ValueMember].DataType;
            }
            #endregion
            #region Converting value into data type
            if (totype != null)
            {
                value = ISM.Lib.Static.ConvToType(totype, value);
            }
            lookUpEdit.EditValue = value;
            #endregion
        }
        static public void LookUpEdit_SetList(ref LookUpEdit lookUpEdit, DataTable table, string valuefield, string namefield, string filter, int[] hiddencolumns)
        {
            #region Creating table
            if (table != null)
            {
                if (string.IsNullOrEmpty(filter))
                {
                    lookUpEdit.Properties.DataSource = table;
                }
                else
                {
                    DataView dv = new DataView(table);
                    dv.RowFilter = filter;
                    lookUpEdit.Properties.DataSource = dv;
                }
                lookUpEdit.Properties.ForceInitialize();
                lookUpEdit.Properties.PopulateColumns();
            }
            #endregion
            #region Hide columns
            if (hiddencolumns != null && hiddencolumns.Length > 0)
            {
                for (int i = 0; i < hiddencolumns.Length; i++)
                {
                    int index = hiddencolumns[i];
                    if (index >= 0 && index < lookUpEdit.Properties.Columns.Count)
                        lookUpEdit.Properties.Columns[index].Visible = false;
                }
            }
            #endregion
            #region Setting properties
            lookUpEdit.Properties.CaseSensitiveSearch = false;
            lookUpEdit.Properties.CharacterCasing = CharacterCasing.Upper;
            lookUpEdit.Properties.ValueMember = valuefield.ToUpper();
            lookUpEdit.Properties.DisplayMember = namefield.ToUpper();
            lookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            lookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lookUpEdit.Properties.ShowHeader = false;
            //lookUpEdit.Properties.ShowFooter = false;

            lookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            lookUpEdit.Properties.NullText = string.Empty;
            #endregion
            if (valuefield != namefield)
            {
                LookUpEdit_SetDisplayValue(ref lookUpEdit, valuefield, namefield);
            }
        }
        static public void LookUpEdit_SetList(ref LookUpEdit lookUpEdit, DataTable table, string valuefield, string namefield)
        {
            LookUpEdit_SetList(ref lookUpEdit, table, valuefield, namefield, "", null);
        }
        static public void LookUpEdit_SetList(ref LookUpEdit lookUpEdit, DataTable table, string valuefield)
        {
            LookUpEdit_SetList(ref lookUpEdit, table, valuefield, valuefield);
        }
        static public void LookUpEdit_SetList(ref LookUpEdit lookUpEdit, DataTable table, string idfieldname, string filter, int[] hiddencolumns)
        {
            #region Creating table
            if (table != null)
            {
                DataView dv = new DataView(table);
                dv.RowFilter = filter;
                lookUpEdit.Properties.DataSource = dv;

                lookUpEdit.Properties.ForceInitialize();
                lookUpEdit.Properties.PopulateColumns();
            }
            #endregion
            #region Hide columns
            if (hiddencolumns != null && hiddencolumns.Length > 0)
            {
                for (int i = 0; i < hiddencolumns.Length; i++)
                {
                    int index = hiddencolumns[i];
                    if (index >= 0 && index < lookUpEdit.Properties.Columns.Count)
                        lookUpEdit.Properties.Columns[index].Visible = false;
                }
            }
            #endregion
            #region Setting properties
            lookUpEdit.Properties.CaseSensitiveSearch = false;
            lookUpEdit.Properties.CharacterCasing = CharacterCasing.Upper;
            lookUpEdit.Properties.DisplayMember = idfieldname.ToUpper();
            lookUpEdit.Properties.ValueMember = idfieldname.ToUpper();
            lookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            lookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;

            lookUpEdit.Properties.ShowHeader = false;
            //lookUpEdit.Properties.ShowFooter = false;

            lookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            lookUpEdit.Properties.NullText = string.Empty;
            #endregion
        }
        static public void LookUpEdit_SetList(ref LookUpEdit lookUpEdit, object idvalue, object namevalue)
        {
            object source = lookUpEdit.Properties.DataSource;
            DataTable dt = null;
            if (source == null)
            {
                dt = new DataTable();
                dt.Columns.Add("VALUE", idvalue.GetType());
                dt.Columns.Add("NAME", namevalue.GetType());

                lookUpEdit.Properties.DataSource = dt;
                lookUpEdit.Properties.ForceInitialize();
                lookUpEdit.Properties.PopulateColumns();
                LookUpEdit_SetList(ref lookUpEdit, null, "VALUE", "NAME");
            }
            else
            {
                dt = (DataTable)source;
            }
            if (dt.Columns.Count >= 2)
            {
                dt.Rows.Add(idvalue, namevalue);
            }
        }
        static public void LookUpEdit_SetList(ref LookUpEdit lookUpEdit, string[] valuenames)
        {
            #region Creating DataTable from Array of String
            DataTable table = new DataTable();
            table.Columns.Add("VALUE", typeof(int));
            table.Columns.Add("NAME", typeof(string));
            if (valuenames != null)
            {
                for (int i = 0; i < valuenames.Length; i++)
                {
                    table.Rows.Add(i, valuenames[i]);
                }
            }
            #endregion

            LookUpEdit_SetList(ref lookUpEdit, table, "VALUE", "NAME");
        }

        static public void LookUpEdit_GetValue(ref LookUpEdit lookUpEdit)
        {
            
        }
        static public void LookUpEdit_SetDisplayValue(ref LookUpEdit lookUpEdit, string valuefield, string namefield)
        {
            lookUpEdit.Properties.ValueMember = valuefield.ToUpper();
            lookUpEdit.Properties.DisplayMember = namefield.ToUpper();
            lookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            lookUpEdit.Properties.AutoSearchColumnIndex = 0;
            lookUpEdit.Properties.ImmediatePopup = true;
            lookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            lookUpEdit.CustomDisplayText -= new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(LookUpEdit_CustomDisplayText);
            lookUpEdit.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(LookUpEdit_CustomDisplayText);
        }

        static private void LookUpEdit_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            DevExpress.XtraEditors.LookUpEdit le = (DevExpress.XtraEditors.LookUpEdit)sender;
            if (!le.Properties.ValueMember.Equals(le.Properties.DisplayMember))
            {
                object v1 = e.Value;
                object v2 = e.DisplayText;
                if (v1 != null && v2 != null)
                {
                    if (!(v1 is string) || ISM.Lib.Static.ToStr(v1) != "")
                    {
                        e.DisplayText = string.Format("{0} - {1}", v1, v2);
                    }   
                }
            }
        }
        static private void LookUpEdit_ReCustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            RepositoryItemLookUpEdit le = (RepositoryItemLookUpEdit)sender;
            if (!le.ValueMember.Equals(le.DisplayMember))
            {
                object v1 = e.Value;
                object v2 = e.DisplayText;
                if (v1 != null && v2 != null)
                {
                    if (!(v1 is string) || ISM.Lib.Static.ToStr(v1) != "")
                    {
                        e.DisplayText = string.Format("{0} - {1}", v1, v2);
                    }
                }
            }
        }

        #endregion
        #region Formatting - RepositoryLookUpEdit
        static public void LookUpEdit_SetList(ref RepositoryItemLookUpEdit lookUpEdit, DataTable table, string valuefield, string namefield, string filter, int[] hiddencolumns)
        {
            #region Creating table
            if (table != null)
            {
                if (string.IsNullOrEmpty(filter))
                {
                    lookUpEdit.DataSource = table;
                }
                else
                {
                    DataView dv = new DataView(table);
                    dv.RowFilter = filter;
                    lookUpEdit.DataSource = dv;
                }
                lookUpEdit.ForceInitialize();
                lookUpEdit.PopulateColumns();
            }
            #endregion
            #region Hide columns
            if (hiddencolumns != null && hiddencolumns.Length > 0)
            {
                for (int i = 0; i < hiddencolumns.Length; i++)
                {
                    int index = hiddencolumns[i];
                    if (index >= 0 && index < lookUpEdit.Columns.Count)
                        lookUpEdit.Columns[index].Visible = false;
                }
            }
            #endregion
            #region Setting properties
            lookUpEdit.CaseSensitiveSearch = false;
            lookUpEdit.CharacterCasing = CharacterCasing.Upper;
            lookUpEdit.ValueMember = valuefield.ToUpper();
            lookUpEdit.DisplayMember = namefield.ToUpper();
            lookUpEdit.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            lookUpEdit.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;

            lookUpEdit.ShowHeader = false;
            //lookUpEdit.ShowFooter = false;

            lookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            lookUpEdit.NullText = string.Empty;
            #endregion
            if (valuefield != namefield)
            {
                LookUpEdit_SetDisplayValue(ref lookUpEdit, valuefield, namefield);
            }
        }
        static public void LookUpEdit_SetList(ref RepositoryItemLookUpEdit lookUpEdit, DataTable table, string valuefield, string namefield)
        {
            LookUpEdit_SetList(ref lookUpEdit, table, valuefield, namefield, "", null);
        }
        static public void LookUpEdit_SetList(ref RepositoryItemLookUpEdit lookUpEdit, DataTable table, string valuefield)
        {
            LookUpEdit_SetList(ref lookUpEdit, table, valuefield, valuefield);
        }
        static public void LookUpEdit_SetList(ref RepositoryItemLookUpEdit lookUpEdit, DataTable table, string idfieldname, string filter, int[] hiddencolumns)
        {
            #region Creating table
            if (table != null)
            {
                DataView dv = new DataView(table);
                dv.RowFilter = filter;
                lookUpEdit.Properties.DataSource = dv;

                lookUpEdit.Properties.ForceInitialize();
                lookUpEdit.Properties.PopulateColumns();
            }
            #endregion
            #region Hide columns
            if (hiddencolumns != null && hiddencolumns.Length > 0)
            {
                for (int i = 0; i < hiddencolumns.Length; i++)
                {
                    int index = hiddencolumns[i];
                    if (index >= 0 && index < lookUpEdit.Properties.Columns.Count)
                        lookUpEdit.Properties.Columns[index].Visible = false;
                }
            }
            #endregion
            #region Setting properties
            lookUpEdit.Properties.CaseSensitiveSearch = false;
            lookUpEdit.Properties.CharacterCasing = CharacterCasing.Upper;
            lookUpEdit.Properties.DisplayMember = idfieldname.ToUpper();
            lookUpEdit.Properties.ValueMember = idfieldname.ToUpper();
            lookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            lookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;

            lookUpEdit.Properties.ShowHeader = false;
            //lookUpEdit.Properties.ShowFooter = false;

            lookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            lookUpEdit.Properties.NullText = string.Empty;
            #endregion
        }
        static public void LookUpEdit_SetList(ref RepositoryItemLookUpEdit lookUpEdit, object idvalue, object namevalue)
        {
            object source = lookUpEdit.Properties.DataSource;
            DataTable dt = null;
            if (source == null)
            {
                dt = new DataTable();
                dt.Columns.Add("VALUE", idvalue.GetType());
                dt.Columns.Add("NAME", namevalue.GetType());

                lookUpEdit.Properties.DataSource = dt;
                lookUpEdit.Properties.ForceInitialize();
                lookUpEdit.Properties.PopulateColumns();
                LookUpEdit_SetList(ref lookUpEdit, null, "VALUE", "NAME");
            }
            else
            {
                dt = (DataTable)source;
            }
            if (dt.Columns.Count >= 2)
            {
                dt.Rows.Add(idvalue, namevalue);
            }
        }
        static public void LookUpEdit_SetList(ref RepositoryItemLookUpEdit lookUpEdit, string[] valuenames)
        {
            #region Creating DataTable from Array of String
            DataTable table = new DataTable();
            table.Columns.Add("VALUE", typeof(int));
            table.Columns.Add("NAME", typeof(string));
            if (valuenames != null)
            {
                for (int i = 0; i < valuenames.Length; i++)
                {
                    table.Rows.Add(i, valuenames[i]);
                }
            }
            #endregion

            LookUpEdit_SetList(ref lookUpEdit, table, "VALUE", "NAME");
        }
        static public void LookUpEdit_GetValue(ref RepositoryItemLookUpEdit lookUpEdit)
        {

        }
        static public void LookUpEdit_SetDisplayValue(ref RepositoryItemLookUpEdit lookUpEdit, string valuefield, string namefield)
        {
            lookUpEdit.ValueMember = valuefield.ToUpper();
            lookUpEdit.DisplayMember = namefield.ToUpper();
            lookUpEdit.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            lookUpEdit.AutoSearchColumnIndex = 0;
            lookUpEdit.ImmediatePopup = true;
            lookUpEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
           lookUpEdit.CustomDisplayText -= new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(LookUpEdit_ReCustomDisplayText);
           lookUpEdit.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(LookUpEdit_ReCustomDisplayText);
        }


        #endregion
        #region Formatting - Grid Column

        static public void Column_SetList(ref GridView view, int column_index, DataTable table, string valuemember, string displaymember)
        {
            if (column_index >= 0 && column_index < view.Columns.Count)
            {
                RepositoryItemLookUpEdit re;

                #region Setting Values to Column

                RepositoryItem ri = view.Columns[column_index].ColumnEdit;
                if (ri == null)
                {
                    if (!(ri is RepositoryItemLookUpEdit))
                    {
                        view.GridControl.RepositoryItems.Remove(ri);
                        re = new RepositoryItemLookUpEdit();
                        view.Columns[column_index].ColumnEdit = re;
                        view.GridControl.RepositoryItems.Add(re);
                    }
                    else
                    {
                        re = (RepositoryItemLookUpEdit)ri;
                    }
                }
                else
                {
                    re = new RepositoryItemLookUpEdit();
                    view.Columns[column_index].ColumnEdit = re;
                    view.GridControl.RepositoryItems.Add(re);
                }

                #endregion

                #region Setting Repository Editor
                re.DataSource = table;
                re.ValueMember = valuemember;
                re.DisplayMember = displaymember;
                re.ForceInitialize();
                re.PopulateColumns();
                re.CaseSensitiveSearch = false;
                re.CharacterCasing = CharacterCasing.Upper;
                re.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                re.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
               
                re.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                re.NullText = string.Empty;
                re.ShowHeader = false;
                re.ShowFooter = false;
                #endregion

                if (valuemember != displaymember)
                {
                    LookUpEdit_SetDisplayValue(ref re, valuemember, displaymember);
                }
            }
        }
        static public void Column_SetList(ref GridView view, int column_index, DataTable table, string valuemember, string displaymember,int[] hide)  //20120508 нэмэв
        {
            if (column_index >= 0 && column_index < view.Columns.Count)
            {
                RepositoryItemLookUpEdit re;

                #region Setting Values to Column

                RepositoryItem ri = view.Columns[column_index].ColumnEdit;
                
                if (ri == null)
                {
                    if (!(ri is RepositoryItemLookUpEdit))
                    {
                        view.GridControl.RepositoryItems.Remove(ri);
                        re = new RepositoryItemLookUpEdit();
                        view.Columns[column_index].ColumnEdit = re;
                        view.GridControl.RepositoryItems.Add(re);
                    }
                    else
                    {
                        re = (RepositoryItemLookUpEdit)ri;
                    }
                }
                else
                {
                    re = new RepositoryItemLookUpEdit();
                    view.Columns[column_index].ColumnEdit = re;
                    view.GridControl.RepositoryItems.Add(re);
                }

                #endregion

                #region Setting Repository Editor
                re.DataSource = table;
                re.ValueMember = valuemember;
                re.DisplayMember = displaymember;
                re.ForceInitialize();
                re.PopulateColumns();
                re.CaseSensitiveSearch = false;
                re.CharacterCasing = CharacterCasing.Upper;
                re.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                re.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;

                re.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                re.NullText = string.Empty;
                re.ShowHeader = false;
                re.ShowFooter = false;
                #endregion

                if (valuemember != displaymember)
                {
                    LookUpEdit_SetDisplayValue(ref re, valuemember, displaymember);
                }
            }
        }
        static public void Column_SetList(ref GridView view, int column_index, DataTable table, string filter, string valuemember, string displaymember, int[] hiddencolumns)
        {
            if (column_index >= 0 && column_index < view.Columns.Count)
            {
                RepositoryItemLookUpEdit re;
                #region Creating DataView with Filter

                DataView dv = new DataView();
                dv.Table = table;
                dv.RowFilter = filter;

                #endregion
                #region Setting Values to Column

                RepositoryItem ri = view.Columns[column_index].ColumnEdit;
                if (ri == null)
                {
                    if (!(ri is RepositoryItemLookUpEdit))
                    {
                        view.GridControl.RepositoryItems.Remove(ri);
                        re = new RepositoryItemLookUpEdit();
                        view.Columns[column_index].ColumnEdit = re;
                        view.GridControl.RepositoryItems.Add(re);
                    }
                    else
                    {
                        re = (RepositoryItemLookUpEdit)ri;
                    }
                }
                else
                {
                    re = new RepositoryItemLookUpEdit();
                    view.Columns[column_index].ColumnEdit = re;
                    view.GridControl.RepositoryItems.Add(re);
                }

                #endregion
                #region Creating and Setting Repository Editor
                re.DataSource = dv;
                re.ValueMember = valuemember;
                re.DisplayMember = displaymember;
                re.ForceInitialize();
                re.PopulateColumns();

                re.CaseSensitiveSearch = false;
                re.CharacterCasing = CharacterCasing.Upper;
                re.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                re.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;

                re.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                re.NullText = string.Empty;
                re.ShowHeader = false;
                re.ShowFooter = false;
                #endregion
                #region Hide columns
                if (hiddencolumns != null && hiddencolumns.Length > 0)
                {
                    for (int i = 0; i < hiddencolumns.Length; i++)
                    {
                        int index = hiddencolumns[i];
                        if (index >= 0 && index < re.Columns.Count)
                            re.Columns[index].Visible = false;
                    }
                }
                #endregion

                if (valuemember != displaymember)
                {
                    LookUpEdit_SetDisplayValue(ref re, valuemember, displaymember);
                }
            }
        }
        static public void Column_SetList(ref GridView view, int column_index, string[] values)
        {
            if (column_index >= 0 && column_index < view.Columns.Count)
            {
                #region Creating DataTable from Array of String
                DataTable table = new DataTable();
                table.Columns.Add("VALUE", typeof(int));
                table.Columns.Add("NAME", typeof(string));
                if (values != null)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        table.Rows.Add(i, values[i]);
                    }
                }
                #endregion

                Column_SetList(ref view, column_index, table, "VALUE", "NAME");
            }
        }

        static public void Column_SetCheck(ref GridView view, int column_index, object checkvalue, object uncheckvalue)
        {
            if (column_index >= 0 && column_index < view.Columns.Count)
            {
                #region Creating and Setting Repository Editor
                DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit re = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
                re.ValueChecked = checkvalue;
                re.ValueUnchecked = uncheckvalue;
                re.AllowGrayed = false;
                #endregion
                #region Setting Values to Column
                if (view.Columns[column_index].ColumnEdit != null)
                {
                    view.GridControl.RepositoryItems.Remove(view.Columns[column_index].ColumnEdit);
                    view.Columns[column_index].ColumnEdit.Dispose();
                    view.Columns[column_index].ColumnEdit = null;
                }
                view.Columns[column_index].ColumnEdit = re;
                view.GridControl.RepositoryItems.Add(re);
                #endregion
            }
        }
        static public void Column_SetCheck(ref GridView view, int column_index)
        {
            Column_SetCheck(ref view, column_index, 1, 0);
        }

        static public void Column_SetDate(ref GridView view, int column_index, string dateformat)
        {
            if (column_index >= 0 && column_index < view.Columns.Count)
            {
                #region Creating and Setting Repository Editor
                DevExpress.XtraEditors.Repository.RepositoryItemDateEdit re = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                re.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                re.DisplayFormat.FormatString = dateformat;
                re.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                re.EditFormat.FormatString = dateformat;
                #endregion
                #region Setting Values to Column
                if (view.Columns[column_index].ColumnEdit != null)
                {
                    view.GridControl.RepositoryItems.Remove(view.Columns[column_index].ColumnEdit);
                    view.Columns[column_index].ColumnEdit.Dispose();
                    view.Columns[column_index].ColumnEdit = null;
                }
                view.Columns[column_index].ColumnEdit = re;
                view.GridControl.RepositoryItems.Add(re);
                #endregion
            }
        }
        static public void Column_SetDate(ref GridView view, int column_index)
        {
            Column_SetDate(ref view, column_index, "yyyy.MM.dd");
        }

        static public void Column_SetNumber(ref GridView view, int column_index, string valueformat)
        {
            if (column_index >= 0 && column_index < view.Columns.Count)
            {
                #region Creating and Setting Repository Editor
                DevExpress.XtraEditors.Repository.RepositoryItemDateEdit re = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                re.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                re.DisplayFormat.FormatString = valueformat;
                re.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                re.EditFormat.FormatString = valueformat;
                #endregion
                #region Setting Values to Column
                if (view.Columns[column_index].ColumnEdit != null)
                {
                    view.GridControl.RepositoryItems.Remove(view.Columns[column_index].ColumnEdit);
                    view.Columns[column_index].ColumnEdit.Dispose();
                    view.Columns[column_index].ColumnEdit = null;
                }
                view.Columns[column_index].ColumnEdit = re;
                view.GridControl.RepositoryItems.Add(re);
                #endregion
            }
        }

        static public void Column_SetCaption(ref GridView view, int column_index, string caption, bool hide)
        {
            if (column_index >= 0 && column_index < view.Columns.Count)
            {
                view.Columns[column_index].Visible = !hide;
                view.Columns[column_index].Caption = caption;
            }
        }
        static public void Column_SetCaption(ref GridView view, int column_index, string caption)
        {
            Column_SetCaption(ref view, column_index, caption, false);
        }

        #endregion
        #region Formatting - Grid

        static public void SetFormatGrid(ref DevExpress.XtraGrid.Views.Grid.GridView grid, bool editable)
        {
            grid.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;

            grid.OptionsBehavior.Editable = editable;
            grid.OptionsBehavior.ReadOnly = !editable;

            grid.OptionsView.ColumnAutoWidth = false;
            grid.OptionsView.ShowGroupPanel = true;
            grid.OptionsView.EnableAppearanceEvenRow = true;

            //grid.Appearance.EvenRow.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Bisque);
            //grid.Appearance.EvenRow.BackColor2 = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Bisque);
            //grid.Appearance.EvenRow.Options.UseBackColor = true;

            grid.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            grid.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;
            grid.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            grid.OptionsSelection.InvertSelection = true;
            grid.OptionsSelection.MultiSelect = true;
        }

        #endregion
        #region Formatting - Combo

        static public void ComboEdit_SetList(ref ComboBoxEdit combo, object[] values, bool editable)
        {
            combo.Properties.Items.Clear();
            combo.Properties.Items.AddRange(values);
            combo.Properties.TextEditStyle =
                editable ?
                DevExpress.XtraEditors.Controls.TextEditStyles.Standard
                :
                DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }
        static public void ComboEdit_SetList(ref ComboBoxEdit combo, string[] values, bool editable)
        {
            combo.Properties.Items.Clear();
            combo.Properties.Items.AddRange(values);
            combo.Properties.TextEditStyle =
                editable ?
                DevExpress.XtraEditors.Controls.TextEditStyles.Standard
                :
                DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }
        static public void ComboEdit_SetList(ref ComboBoxEdit combo, DataTable table, string valuefield, bool editable)
        {
            if (table != null && !string.IsNullOrEmpty(valuefield))
            {
                object[] values = new  object[table.Rows.Count];
                for(int i=0; i< values.Length; i++)
                {
                    values[i] = table.Rows[i][valuefield];
                }
                ComboEdit_SetList(ref combo, values, editable);
            }
        }
        
        #endregion

        #region Form State Related

        static public void SaveStateGrid(string appname, string formname, ref GridView grid)
        {
            if (grid == null) return;
            string regkey = string.Format(@"{0}\{1}\{2}", appname, formname, grid.Name);
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                Static.RegisterSave(regkey, string.Format("ColumnWidth{0}", i), grid.Columns[i].Width);
                //Static.RegisterSave(regkey, string.Format("ColumnOrder{0}", i), grid.Columns[i].);
                if(Static.ToInt(grid.Columns[i].VisibleIndex)==-1)
                Static.RegisterSave(regkey, string.Format("ColumnVisible{0}", i), "false");
                else
                    Static.RegisterSave(regkey, string.Format("ColumnVisible{0}", i), grid.Columns[i].VisibleIndex);
            }
        }
        static public void RestoreStateGrid(string appname, string formname, ref GridView grid)
        {
            if (grid == null) return;
            string regkey = string.Format(@"{0}\{1}\{2}", appname, formname, grid.Name);
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].Width = Static.ToInt(Static.RegisterGet(regkey, string.Format("ColumnWidth{0}", i), grid.Columns[i].Width));
                //grid.Columns[i].SortIndex = Static.ToInt(Static.RegisterGet(regkey, string.Format("ColumnOrder{0}", i), grid.Columns[i].SortIndex));
                if (Static.ToStr(Static.RegisterGet(regkey, string.Format("ColumnVisible{0}", i), "true")) == "false")
                    grid.Columns[i].VisibleIndex = -1;
                else
                    grid.Columns[i].VisibleIndex = Static.ToInt(Static.RegisterGet(regkey, string.Format("ColumnVisible{0}", i), grid.Columns[i].VisibleIndex));
            }
        }

        static public void SaveStateGrid(string appname, string formname, ref GridControl grid)
        {
            if (grid == null) return;
            GridView view = (GridView)grid.MainView;
            SaveStateGrid(appname, formname, ref view);
        }
        static public void RestoreStateGrid(string appname, string formname, ref GridControl grid)
        {
            if (grid == null) return;
            GridView view = (GridView)grid.MainView;
            RestoreStateGrid(appname, formname, ref view);
        }

        static public void SaveStateVGrid(string appname, string formname, ref VGridControl grid,ref GroupControl group)
        {
            if (grid == null) return;
            string regkey = string.Format(@"{0}\{1}\{2}", appname, formname, grid.Name);
            Static.RegisterSave(regkey, string.Format("HeaderWidth"), grid.RowHeaderWidth);
            Static.RegisterSave(regkey, string.Format("GroupWidth"), group.Width);
        }
        static public void RestoreStateVGrid(string appname, string formname, ref VGridControl grid, ref GroupControl group)
        {
            if (grid == null) return;
            string regkey = string.Format(@"{0}\{1}\{2}", appname, formname, grid.Name);
            grid.RowHeaderWidth = Static.ToInt(Static.RegisterGet(regkey, string.Format("HeaderWidth"), grid.RowHeaderWidth));
            group.Width = Static.ToInt(Static.RegisterGet(regkey, string.Format("GroupWidth"), group.Width));
        }

        static public void SaveStateForm(string appname, ref Form form)
        {
            if (form == null) return;
            string regkey = string.Format(@"{0}\{1}", appname, form.Name);
            if (form.WindowState == FormWindowState.Normal)
            {
                EServ.Shared.Static.RegisterSave(regkey, "WindowX", form.Left);
                EServ.Shared.Static.RegisterSave(regkey, "WindowY", form.Top);
                EServ.Shared.Static.RegisterSave(regkey, "WindowWidth", form.Width);
                EServ.Shared.Static.RegisterSave(regkey, "WindowHeight", form.Height);
            }
            EServ.Shared.Static.RegisterSave(regkey, "WindowState", (int)form.WindowState);
        }
        static public void RestoreStateForm(string appname, ref Form form)
        {
            if (form == null) return;
            string regkey = string.Format(@"{0}\{1}", appname, form.Name);
            form.Left = Static.ToInt(Static.RegisterGet(regkey, "WindowX", form.Left));
            form.Top = Static.ToInt(Static.RegisterGet(regkey, "WindowY", form.Top));
            form.Width = Static.ToInt(Static.RegisterGet(regkey, "WindowWidth", form.Width));
            form.Height = Static.ToInt(Static.RegisterGet(regkey, "WindowHeight", form.Height));
            form.WindowState = (FormWindowState)Static.ToInt(Static.RegisterGet(regkey, "WindowState", 0));
        }

        #endregion

        #region Grid Related

        static public void GridLayoutGet(GridView gridView, DataTable table, string layoutfile)
        {
            gridView.GridControl.SuspendLayout();
            gridView.BeginUpdate();
            try
            {
                #region Keep current cursor position
                int index = gridView.FocusedRowHandle;
                if (index < 0) index = 0;
                #endregion
                #region Setting data source
                gridView.GridControl.RepositoryItems.Clear();
                gridView.GridControl.DataSource = table;
                gridView.PopulateColumns();
                #endregion
                #region Restore layout format
                if (!string.IsNullOrEmpty(layoutfile))
                {
                    if (File.Exists(layoutfile))
                    {
                        gridView.OptionsLayout.StoreVisualOptions = true;
                        gridView.OptionsLayout.LayoutVersion = DateTime.Now.Ticks.ToString();
                        gridView.RestoreLayoutFromXml(layoutfile);
                    }
                }
                #endregion
                #region Restore old cursor position
                if (table != null && table.Rows.Count > 0)
                    if (index >= table.Rows.Count) index = table.Rows.Count - 1;

                if (gridView.FocusedRowHandle != index)
                    gridView.FocusedRowHandle = index;
                #endregion
            }
            catch (Exception ex)
            {
            }
            gridView.EndUpdate();
            gridView.GridControl.ResumeLayout();
        }
        static public void GridLayoutSave(GridView gridView, string layoutfile)
        {
            gridView.GridControl.SuspendLayout();
            gridView.BeginUpdate();
            try
            {
                gridView.OptionsLayout.StoreVisualOptions = true;
                gridView.OptionsLayout.LayoutVersion = DateTime.Now.Ticks.ToString();
                gridView.SaveLayoutToXml(layoutfile);
            }
            catch (Exception ex)
            {
            }
            gridView.EndUpdate();
            gridView.GridControl.ResumeLayout();
        }
        static public void GridExport(GridView gridView, string Title)
        {
            Result res = null;
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();

                dlg.Title = Title;
                dlg.Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|Acrobat Reader|*.pdf|Text Format|*.txt";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dlg.RestoreDirectory = true;

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    switch (dlg.FilterIndex)
                    {
                        case 1:
                            DevExpress.XtraPrinting.XlsxExportOptions opt = new DevExpress.XtraPrinting.XlsxExportOptions();
                            opt.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;
                            opt.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                            gridView.ExportToXlsx(dlg.FileName, opt);
                            break;
                        case 2:
                            gridView.ExportToXls(dlg.FileName);
                            break;
                        case 3:
                            DevExpress.XtraPrinting.PdfExportOptions pdf = new DevExpress.XtraPrinting.PdfExportOptions();
                            pdf.ConvertImagesToJpeg = true;
                            gridView.ExportToPdf(dlg.FileName);
                            break;
                        case 4:
                            gridView.ExportToText(dlg.FileName);
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Result r = new Result(1, ex.ToString());
                ValidateQuery(res);
            }
        }
        
        #endregion

        #region Result Validation
        static public bool ValidateQuery(Result res)
        {
            return ValidateQuery(res, null);
        }
        static public bool ValidateQuery(Result res, string success_text)
        {
            if (res == null) return true;

            bool success = false;
            string msg = res.ResultDesc;
            if (res.ResultNo == 0)
            {
                msg = success_text;
                success = true;
            }
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return success;
        }
        static public bool ValidateChanges(Result res, string notfound_text, string duplicate_text)
        {
            if (res == null) return true;

            bool success = false;
            string msg = "";
            MessageBoxIcon icon = MessageBoxIcon.Information;
            switch (res.ResultNo)
            {
                case 0:
                    if (res.AffectedRows <= 0) msg = notfound_text;
                    success = true;
                    break;
                case -2147467259:
                    msg = duplicate_text;
                    icon = MessageBoxIcon.Warning;
                    break;
                default:
                    msg = res.ResultDesc;
                    break;
            }
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg, "", MessageBoxButtons.OK, icon);
            }
            return success;
        }
        static public bool ValidateConfirm(string confirm_text)
        {
            DialogResult res = MessageBox.Show(confirm_text, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (res == DialogResult.Yes);
        }
        #endregion

        #region Control Related

        static public Control FindControl(Control.ControlCollection controls, Type type)
        {
            Control obj = null;
            foreach (Control control in controls)
            {
                if (control.GetType().Equals(type))
                {
                    obj = control;
                    break;
                }
            }
            return obj;
        }
        static public ArrayList FindControlAll(Control.ControlCollection controls, Type type)
        {
            ArrayList list = FindControlAllRecursive(controls, type);
            return list;
        }
        static internal ArrayList FindControlAllRecursive(Control.ControlCollection controls, Type type)
        {
            ArrayList list = new ArrayList();
            foreach (Control control in controls)
            {
                Console.WriteLine(control.Name + " : " + control.GetType().Name);

                if (control.GetType().Equals(type))
                {
                    list.Add(control);
                }
                if (control.Controls != null)
                {
                    ArrayList childlist = FindControlAllRecursive(control.Controls, type);
                    if (childlist != null)
                    {
                        list.AddRange(childlist);
                    }
                }
            }
            return list;
        }

        #endregion

        #region Progress Form Related

        static private FormDictProgress _progress_form = null;
        static private Form _owner_form = null;
        static internal int _lck_progress_open = 0;
        static internal int _lck_progress_close = 0;

        static public void ProgressFormShow(string title, string taskname, Form owner)
        {
            int ret = Interlocked.CompareExchange(ref _lck_progress_open, 1, 0);
            if (ret == 0)
            {
                #region Create progress form thread
                Thread th = new Thread(new ParameterizedThreadStart(_ProgressFormShow));
                th.Name = "Thread_ProgressForm";
                th.Start(new object[] { title, taskname, owner });
                #endregion
                #region Wait for Progress form is created.
                //DateTime started = DateTime.Now;
                //while(_progress_form == null)
                //{
                //    if (DateTime.Now.Subtract(started).TotalSeconds > 5) break;
                //    else Thread.Sleep(20);
                //}
                #endregion
            }
            else
            {
                #region Set value into progress form
                if (_progress_form != null)
                    _progress_form.SetProgress(title, taskname);
                #endregion
            }
        }
        static public void ProgressFormShow(string title, string taskname)
        {
            ProgressFormShow(title, taskname, null);
        }
        static public void ProgressFormClose()
        {
            _ProgressFormClose(null);
        }

        static private void _ProgressFormShow(object state)
        {
            try
            {
                object[] param = (object[])state;
                string title = Convert.ToString(param[0]);
                string taskname = Convert.ToString(param[1]);
                Form owner = (Form)param[2];

                if (_progress_form == null)
                {
                    _progress_form = new FormDictProgress();
                    _progress_form.SetProgress(title, taskname);

                    _owner_form = owner;
                    _progress_form.ShowDialog();
                }
                else
                {
                    _progress_form.SetProgress(title, taskname);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }
        static private void _ProgressFormClose(object state)
        {
            try
            {
                #region Exit when progress form does not created
                if (_lck_progress_open == 0) return;
                #endregion
                #region One Close when progress form is loaded
                int ret = Interlocked.CompareExchange(ref _lck_progress_close, 1, 0);
                if (ret == 0)
                {
                    _progress_form = null;
                    if (_owner_form != null && !_owner_form.IsDisposed) _owner_form.Activate();
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }

    public class FieldInfo
    {
        #region Field Variables

        public string ctrlname;
        public int tableindex;
        public string dbfieldname;
        public bool ismandatory;
        public bool iskey;
        public bool isdisable;
        public string editmask;
        public object value;

        #endregion
    }
}
