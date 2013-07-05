using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;

namespace InfoPos.Admin.DashBoard
{
    public partial class frmFinal : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core;
        public frmFinal(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            InitCombo();
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
            }
        }
        private void InitCombo()
        {
            FormUtility.LookUpEdit_SetList(ref cboViewType , 0 , "Worksheet");
            FormUtility.LookUpEdit_SetList(ref cboViewType , 1 , "Graphic");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Static.ToLong(numRefreshTime.EditValue) >= 5)
                {
                    _core.CacheSet("frmFinalStatements_Name", txtPanelName.EditValue);
                    _core.CacheSet("frmFinalStatements_RefreshTime", numRefreshTime.EditValue);
                    _core.CacheSet("frmFinalStatements_ReportNo", txtReportNo.EditValue);
                    _core.CacheSet("frmFinalStatements_ViewType", cboViewType.EditValue);
                    _core.CacheSet("frmFinalStatements_UnReadDay", numUnReadDay.EditValue);
                    _core.CacheSet("frmFinalStatements_ReadDay", numReadDay.EditValue);
                    _core.CacheSet("frmFinalStatements_RefreshTime", numRefreshTime.EditValue);
                    _core.CacheSave();
                    MessageBox.Show("Амжилттай хадгалагдлаа");
                    this.Close();
                }
                else
                {
                    ErrorProvider.SetError(numRefreshTime, "5-с их утга оруулна уу.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmFinal_Load(object sender, EventArgs e)
        {
            //txtPanelName.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "Name", "Final Statement"));
            //frmDashboard frm = new frmDashboard(_core);
            //frm.pnlFinal.Text = Static.ToStr(txtPanelName.EditValue);
            //this.Name = Static.ToStr(txtPanelName.EditValue) + " тохиргоо";
            //numRefreshTime.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "RefreshTime", 60));
            //txtReportNo.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "ReportNo", 261001));
            //dteTxnDate.EditValue = _core.TxnDate;
            //FormUtility.LookUpEdit_SetValue(ref cboViewType, Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "ViewType", 0)));
            //numReadDay.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "ReadDay", 9));
            //numUnReadDay.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "UnReadDay", 99));
            //numRefreshTime.EditValue = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "RefreshTime", 5));
        }
    }
}