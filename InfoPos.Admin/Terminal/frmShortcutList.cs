using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Admin
{
    public partial class frmShortcutList : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region[Байгуулагч функц]
        public frmShortcutList(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            if (_core.Resource != null)
            {
                btnExit.Image = _core.Resource.GetImage("navigate_home");
            }
        }
        #endregion
        #region[Init Function]
        void Init()
        {
            try
            {
                DataTable dt;
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140321, 140321, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc.ToString());
                }
                else
                {
                    if (r.Data.Tables[0] != null)
                    {
                        dt = r.Data.Tables[0];
                        grdShortcutList.DataSource = dt;
                        SetFormat();
                    }
                    else
                    {
                        MessageBox.Show("Бичлэг олдсонгүй .");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Function]
        void SetFormat()
        {
            appname = _core.ApplicationName;
            formname = "Terminal." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gvwShorcutList);
            gvwShorcutList.Columns[0].Caption = "Дугаар";
            gvwShorcutList.Columns[1].Caption="Түлхүүр";
            gvwShorcutList.Columns[1].Visible = false;
            gvwShorcutList.Columns[2].Caption = "Түлхүүр1";
            gvwShorcutList.Columns[2].Visible = false;
            gvwShorcutList.Columns[3].Caption="Түлхүүр2";
            gvwShorcutList.Columns[3].Visible = false;
            gvwShorcutList.Columns[4].Caption = "Товчны хослол";
            gvwShorcutList.Columns[5].Caption = "Талбарын утга";
            gvwShorcutList.Columns[6].Caption = "Тайлбар";
            gvwShorcutList.Columns[7].Caption = "Төрөл дугаар";
            gvwShorcutList.Columns[7].Visible = false;
            gvwShorcutList.Columns[8].Caption = "Төрөл";
            gvwShorcutList.Columns[0].OptionsColumn.AllowEdit = false;
            gvwShorcutList.Columns[1].OptionsColumn.AllowEdit = false;
            gvwShorcutList.Columns[2].OptionsColumn.AllowEdit = false;
            gvwShorcutList.Columns[3].OptionsColumn.AllowEdit = false;
            gvwShorcutList.Columns[4].OptionsColumn.AllowEdit = false;
            gvwShorcutList.Columns[5].OptionsColumn.AllowEdit = false;
            gvwShorcutList.Columns[6].OptionsColumn.AllowEdit = false;
            gvwShorcutList.Columns[7].OptionsColumn.AllowEdit = false;
            gvwShorcutList.Columns[8].OptionsColumn.AllowEdit = false;
        }
        #endregion
        #region[FormEvent]
        private void frmShortcutList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwShorcutList);
        }
        private void frmShortcutList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        #endregion
        #region[BTN]
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}