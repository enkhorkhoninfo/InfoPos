using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Calendar;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using System.IO;
using EServ.Shared;
using ISM.Template;
namespace InfoPos.Schedule
{
    public partial class frmCalendar : DevExpress.XtraEditors.XtraForm
    {
        #region[Varibles]
        Core.Core _core;
        Hashtable day=new Hashtable();
        DataTable dt = new DataTable();
        DataTable dtable = new DataTable();
        RepositoryItemImageComboBox imagecombo = new RepositoryItemImageComboBox();
        public static ImageCollection image = new ImageCollection();
        DataRow FocusedRow;
        int rowhandle;
        #endregion
        #region[Байгуулагагч функц]
        public frmCalendar(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnRefresh.Image = _core.Resource.GetImage("date_refresh");
                btnAdd.Image = _core.Resource.GetImage("date_add");
                btnEdit.Image = _core.Resource.GetImage("date_edit");
                btnDelete.Image = _core.Resource.GetImage("date_delete");
                btnTableSave.Image = _core.Resource.GetImage("table_save");
            }
            Result r = new Result();
            r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140141, 140141, null);
            imagecombo.SmallImages = image;
            if (r.ResultNo == 0)
            {
                dtable = r.Data.Tables[0];
                int i=0;
                foreach (DataRow dr in r.Data.Tables[0].Rows)
                {
                    i++;
                    image.AddImage(Base64ToImage(Static.ToStr(dr["ICON"])));
                    ImageComboBoxItem ri = new ImageComboBoxItem(Static.ToStr(dr["DESCRIPTION"]), Static.ToInt(dr["WEATHERID"]), i);
                    imagecombo.Items.Add(ri);
                }
            }
            dt.Columns.Add("DAY", typeof(string));
            dt.Columns.Add("DAYDATE", typeof(int));
            dt.Columns.Add("DayType", typeof(int));
            dt.Columns.Add("DayWeatherType", typeof(int));
            dt.Columns.Add("DayTemperature", typeof(int));
            dt.Columns.Add("NightWeatherType", typeof(int));
            dt.Columns.Add("NightTemperature", typeof(int));
            RefreshData(DateTime.Now);
            gvwCalendar.Columns[0].Visible = false;
            gvwCalendar.Columns[1].Caption = "№";
            gvwCalendar.Columns[1].MinWidth = 20;
            gvwCalendar.Columns[1].MaxWidth = 20;
            gvwCalendar.Columns[1].OptionsColumn.AllowEdit = false;
            gvwCalendar.Columns[1].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gvwCalendar.Columns[1].OptionsColumn.AllowSize = false;
            gvwCalendar.Columns[2].Caption = "Өдрийн төрөл";

            gvwCalendar.Columns[3].Caption = "Өдрийн цаг агаар";
            gvwCalendar.Columns[3].ColumnEdit = imagecombo;
            gvwCalendar.Columns[4].Caption = "Өдрийн хэм (градус)";
            gvwCalendar.Columns[5].Caption = "Шөнийн цаг агаар";
            gvwCalendar.Columns[5].ColumnEdit = imagecombo;
            gvwCalendar.Columns[6].Caption = "Шөнийн хэм (градус)";

            string msg = "";
            ArrayList Tables = new ArrayList();
            string[] names = { "PADAYTYPE" };
            DictUtility.PrivNo = 130001;
            Result res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
            DataTable dataCombo = (DataTable)Tables[0];
            if (dataCombo == null)
            {
                msg = "Dictionary-д PADAYTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                gvwCalendar.Columns[2].ColumnEdit = CreateRepositoryLookUpEdit(dataCombo);
            }
        }
        #endregion
        #region[Own Events]
        private void grdCalendar_DoubleClick(object sender, EventArgs e)
        {
            FocusedRow = gvwCalendar.GetFocusedDataRow();
            if (Static.ToStr(FocusedRow["DAY"]) == "")
            {
                frmDay frm = new frmDay(_core, FocusedRow, dateNavigator1.SelectionStart, 0, dtable);
                frm.ShowDialog();
            }
            else
            {
                frmDay frm = new frmDay(_core, FocusedRow, dateNavigator1.SelectionStart, 1, dtable);
                frm.ShowDialog();
            }
            RefreshData(dateNavigator1.DateTime);
            dateNavigator1.Refresh();
        }      
        private void dateNavigator1_EditDateModified_1(object sender, EventArgs e)
        {
            RefreshData(dateNavigator1.DateTime);
        }
        private void dateNavigator1_CustomDrawDayNumberCell(object sender, CustomDrawDayNumberCellEventArgs e)
        {
            e.Handled = true;
            Brush chosenBrush = Brushes.Black;
            System.Drawing.Font font = new Font(e.Style.Font, FontStyle.Bold);

            DataTable dt = (DataTable)grdCalendar.DataSource;
            foreach(DataRow datarow in dt.Rows)
            {
                if(day.ContainsKey(e.Date))
                {
                    chosenBrush = Brushes.Blue;
                    e.Graphics.FillRectangle(Brushes.YellowGreen, e.Bounds);
                }
            }

            if ((e.Date.Year == DateTime.Now.Year) && (e.Date.Month == DateTime.Now.Month) && (e.Date.Day == DateTime.Now.Day))
            {
                chosenBrush = Brushes.Blue;
                e.Graphics.FillRectangle(Brushes.OrangeRed, e.Bounds);
            }
            if (e.Selected)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            }
            e.Graphics.DrawString(e.Date.Day.ToString(), e.Style.Font, chosenBrush, e.Bounds);
        }
        private void gvwCalendar_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRow = gvwCalendar.GetFocusedDataRow();
            if (FocusedRow != null)
            {
                if (Static.ToStr(FocusedRow["DAY"]) != "")
                {
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnAdd.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }
        #endregion
        #region[BTN]
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmDay frm = new frmDay(_core, FocusedRow = gvwCalendar.GetFocusedDataRow(), dateNavigator1.SelectionStart, 0, dtable);
            frm.ShowDialog();
            RefreshData(dateNavigator1.DateTime);
            dateNavigator1.Refresh();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData(dateNavigator1.DateTime);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            FocusedRow=gvwCalendar.GetFocusedDataRow();
            Result res = new Result();
            if (FocusedRow != null)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 228, 140210, 140210, new object[] { FocusedRow["DAY"] });
                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай устгалаа.");
                    if (gvwCalendar.FocusedRowHandle == 0)
                    {
                        btnAdd.Enabled = true;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                    RefreshData(dateNavigator1.DateTime);
                    dateNavigator1.Refresh();

                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmDay frm = new frmDay(_core, FocusedRow = gvwCalendar.GetFocusedDataRow(), dateNavigator1.SelectionStart, 1, dtable);
            frm.ShowDialog();
            RefreshData(dateNavigator1.DateTime);
            dateNavigator1.Refresh();
        }
        #endregion
        #region[Function]
        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        private void RefreshData(DateTime date)
        {
            day.Clear();
            grdCalendar.DataSource = null;
            dt.Clear();
            Result res = new Result();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 228, 140206, 140206, new object[] { Convert.ToDateTime(date.ToString("M/1/yyyy")), Convert.ToDateTime(date.ToString(string.Format("M/{0}/yyyy", DateTime.DaysInMonth(date.Year, date.Month)))) });
            if (res.ResultNo == 0)
            {      
                for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["DAYDATE"] = i;
                    foreach (DataRow drow in res.Data.Tables[0].Rows)
                    {
                        if (Convert.ToDateTime(drow["DAY"]).Day == i)
                        {
                            dr["DAY"] = Convert.ToString(drow["DAY"]);
                            dr["DayType"] = drow["DayType"];
                            dr["DayWeatherType"] = drow["DayWeatherType"];
                            dr["DayTemperature"] = drow["DayTemperature"];
                            dr["NightWeatherType"] = drow["NightWeatherType"];
                            dr["NightTemperature"] = drow["NightTemperature"];
                            day.Add(Convert.ToDateTime(drow["day"]).Date, i);
                        }
                    }
                    dt.Rows.Add(dr);
                    grdCalendar.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        public RepositoryItemLookUpEdit CreateRepositoryLookUpEdit(DataTable Data)
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
            ri.DataSource = Data;
            ri.DisplayMember = "DESCRIPTION";
            ri.ValueMember = "DAYTYPE";
            //ri.DisplayFormat.FormatString = ri.ValueMember + "-" + ri.DisplayMember;

            //ri.Mask.UseMaskAsDisplayFormat = true;
            return ri;
        }
        #endregion
        private bool Validate()
        {
            DataTable dt=(DataTable)grdCalendar.DataSource;
            foreach (DataRow dr in dt.Rows)
            {
                if (Static.ToStr(dr["DAYTYPE"]) != "")
                {
                    if (Static.ToStr(dr["DAY"]) == "")
                    {
                        dr["DAY"] = Static.ToDate(dateNavigator1.DateTime.ToString(string.Format("yyyy.M.{0}", dr["DayDate"])));
                    }
                }
            }
            return true;
        }
        private void btnTableSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                Result res = new Result();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 228, 140208, 140208, new object[] { dt });
                if (res.ResultNo == 0)
                {
                    MessageBox.Show("Амжилттай хадгаллаа.");
                    if (gvwCalendar.FocusedRowHandle == 0)
                    {
                        
                    }
                    RefreshData(dateNavigator1.DateTime);
                    dateNavigator1.Refresh();
                }
                else
                {
                    MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                }
            }
        }
    }
}