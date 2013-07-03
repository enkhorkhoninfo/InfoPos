using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace ISM.Template
{
    public class FormUtility
    {
        static public void LookUpEdit_SetList(ref DevExpress.XtraEditors.LookUpEdit lookUpEdit, DataTable table, string idfieldname)
        {
            if (table != null) lookUpEdit.Properties.DataSource = table;

            lookUpEdit.Properties.CaseSensitiveSearch = false;
            lookUpEdit.Properties.CharacterCasing = CharacterCasing.Upper;
            lookUpEdit.Properties.DisplayMember = idfieldname.ToUpper();
            lookUpEdit.Properties.ValueMember = idfieldname.ToUpper();
            lookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            lookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;

            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.ShowFooter = false;

            // This must be in.
            lookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            lookUpEdit.Properties.NullText = string.Empty;
        }
        static public void LookUpEdit_SetList(ref DevExpress.XtraEditors.LookUpEdit lookUpEdit, DataTable table, string idfieldname, string filter, int[] hidecolumns)
        {
            if (table != null)
            {
                DataView dv = new DataView(table);
                dv.RowFilter = filter;
                lookUpEdit.Properties.DataSource = dv;
                lookUpEdit.Properties.PopulateColumns();
            }

            if (hidecolumns != null && hidecolumns.Length > 0)
            {
                for (int i = 0; i < hidecolumns.Length; i++)
                {
                    int index = hidecolumns[i];
                    if (index >= 0 && index < lookUpEdit.Properties.Columns.Count)
                        lookUpEdit.Properties.Columns[index].Visible = false;
                }
            }

            lookUpEdit.Properties.CaseSensitiveSearch = false;
            lookUpEdit.Properties.CharacterCasing = CharacterCasing.Upper;
            lookUpEdit.Properties.DisplayMember = idfieldname.ToUpper();
            lookUpEdit.Properties.ValueMember = idfieldname.ToUpper();
            lookUpEdit.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            lookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;

            lookUpEdit.Properties.ShowHeader = false;
            lookUpEdit.Properties.ShowFooter = false;

            // This must be in.
            lookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            lookUpEdit.Properties.NullText = string.Empty;
        }
        static public void LookUpEdit_SetList(ref DevExpress.XtraEditors.LookUpEdit lookUpEdit, object idvalue, object namevalue)
        {
            object source = lookUpEdit.Properties.DataSource;
            DataTable dt = null;
            if (source == null)
            {
                dt = new DataTable();
                dt.Columns.Add("ID", idvalue.GetType());
                dt.Columns.Add("NAME", namevalue.GetType());

                lookUpEdit.Properties.DataSource = dt;
                LookUpEdit_SetList(ref lookUpEdit, null, "ID");
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
        static public void LookUpEdit_GetValue(ref DevExpress.XtraEditors.LookUpEdit lookUpEdit)
        {
            
        }

        static public void SetFormatGrid(ref DevExpress.XtraGrid.Views.Grid.GridView grid, bool editable)
        {
            grid.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;

            grid.OptionsBehavior.Editable = editable;
            grid.OptionsBehavior.ReadOnly = !editable;

            grid.OptionsView.ColumnAutoWidth = false;
            grid.OptionsView.ShowGroupPanel = true;
            grid.OptionsView.EnableAppearanceEvenRow = true;

            grid.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            grid.Appearance.EvenRow.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Bisque);
            grid.Appearance.EvenRow.BackColor2 = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Bisque);
            grid.Appearance.EvenRow.Options.UseBackColor = true;

        }

        static public void SaveStateGrid(ref DevExpress.XtraGrid.Views.Grid.GridView grid, string regkey)
        {
            if (grid == null) return;
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                Static.RegisterSave(regkey, string.Format("ColumnWidth{0}", i), grid.Columns[i].Width);
            }
        }
        static public void RestoreStateGrid(ref DevExpress.XtraGrid.Views.Grid.GridView grid, string regkey)
        {
            if (grid == null) return;
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].Width = Static.ToInt(Static.RegisterGet(regkey, string.Format("ColumnWidth{0}", i), grid.Columns[i].Width));
            }
        }
        static public void SaveStateForm(ref Form form, string regkey)
        {
            if (form == null) return;
            if (form.WindowState == FormWindowState.Normal)
            {
                EServ.Shared.Static.RegisterSave(regkey, "WindowX", form.Left);
                EServ.Shared.Static.RegisterSave(regkey, "WindowY", form.Top);
                EServ.Shared.Static.RegisterSave(regkey, "WindowWidth", form.Width);
                EServ.Shared.Static.RegisterSave(regkey, "WindowHeight", form.Height);
            }
            EServ.Shared.Static.RegisterSave(regkey, "WindowState", (int)form.WindowState);
        }
        static public void RestoreStateForm(ref Form form, string regkey)
        {
            if (form == null) return;
            form.Left = Static.ToInt(Static.RegisterGet(regkey, "WindowX", form.Left));
            form.Top = Static.ToInt(Static.RegisterGet(regkey, "WindowY", form.Top));
            form.Width = Static.ToInt(Static.RegisterGet(regkey, "WindowWidth", form.Width));
            form.Height = Static.ToInt(Static.RegisterGet(regkey, "WindowHeight", form.Height));
            form.WindowState = (FormWindowState)Static.ToInt(Static.RegisterGet(regkey, "WindowState", 0));
        }

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
    }

    public class FieldInfo
    {
        public string ctrlname;
        public int tableindex;
        public string dbfieldname;
        public bool ismandatory;
        public bool iskey;
        public bool isdisable;
        public string editmask;
    }
}
