using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;

namespace ISM.Touch
{
    public class TouchKeyboard
    {
        public bool Enable = false;

        internal Rectangle GetAbsolutePosition(Control control)
        {
            Control c = control;
            Point src = new Point(0, 0);
            Point dst = control.PointToScreen(src);
            dst.Y += control.Height * 2;
            Rectangle rct = new Rectangle(dst, control.Size);
            return rct;
        }
        internal Point GetVisiblePosition(Control control, Form keyboard)
        {
            Rectangle r = GetAbsolutePosition(control);
            Point p = GetVisiblePosition(r, keyboard);
            return p;
        }
        internal Point GetVisiblePosition(Rectangle r, Form keyboard)
        {
            Rectangle scr = Screen.PrimaryScreen.WorkingArea;

            Point p = new Point();
            p.X += (scr.Width - keyboard.Width) / 2;
            p.Y += (scr.Height - keyboard.Height);

            return p;
        }
        internal void control_MouseClick(object sender, EventArgs e)
        {
            if (Enable)
            {
                ShowKeyboard((Control)sender);
            }
        }

        public void AddToKeyboard(Control control)
        {
            if (control == null) return;
            control.Click -= new EventHandler(control_MouseClick);
            control.Click += new EventHandler(control_MouseClick);
        }
        public void ShowKeyboard(Control control, bool showmodal = false)
        {
            if (control == null) return;
            Form frm = null;
            if (control is NumericUpDown || control is CalcEdit)
            {
                frm = new frmNumpad(control);
            }
            else if (control is DateTimePicker || control is DateEdit)
            {
                frm = new frmDatepad(control);
            }
            else if (control is LookUpEdit)
            {
                frm = new frmCombo(control);
            }
            else if (control is TextBoxBase || control is TextEdit || control is MemoEdit)
            {
                frm = new frmKeyboard(control);
            }

            Point p = GetVisiblePosition(control, frm);
            frm.Location = p;
            if (showmodal) frm.ShowDialog();
            else frm.Show();
        }

        public DialogResult ShowKeyboard(GridView view, int rowhandle, int columnindex)
        {
            if (view == null) return DialogResult.Abort;

            DataRowView dr = (DataRowView)view.GetRow(rowhandle);
            DevExpress.XtraGrid.Columns.GridColumn col = view.Columns[columnindex];
            Type type = col.ColumnType;
            object value = dr[columnindex];

            GridViewInfo info = (GridViewInfo)view.GetViewInfo();
            GridCellInfo cell = info.GetGridCellInfo(rowhandle, columnindex);
            Rectangle r;
            if (cell != null) r = cell.Bounds;
            else r = Rectangle.Empty;

            DialogResult res = DialogResult.OK;

            switch (type.Name)
            {
                case "String":
                    frmKeyboard frm1 = new frmKeyboard(null);
                    frm1.Location = GetVisiblePosition(r, frm1);
                    frm1.Value = value == null ? "" : Convert.ToString(value);
                    res = frm1.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        view.SetRowCellValue(rowhandle, col, frm1.Value);
                    }
                    break;
                case "DateTime":
                    frmDatepad frm2 = new frmDatepad(null);
                    frm2.Location = GetVisiblePosition(r, frm2);
                    frm2.Value = value == null ? DateTime.Today : Convert.ToDateTime(value);
                    res = frm2.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        view.SetRowCellValue(rowhandle, col, frm2.Value);
                    }
                    break;
                default:
                    frmNumpad frm3 = new frmNumpad(null);
                    frm3.Location = GetVisiblePosition(r, frm3);
                    frm3.Value = value == null ? 0 : Convert.ToDecimal(value);

                    res = frm3.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        view.SetRowCellValue(rowhandle, col, frm3.Value);
                    }
                    break;
            }
            return res;
        }

    }
}
