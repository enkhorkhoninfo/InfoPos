using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors.Repository;

using ISM.Report;
using ISM.Lib;
using ISM.Template;
         
namespace InfoPos.Admin.DashBoard
{
    public partial class frmDashboard : DevExpress.XtraEditors.XtraForm
    {
        
        #region[Variables]
        InfoPos.Core.Core _core;
     //   DataTable dt = null;
     //   long _DealNo;
     //   long _ReDealNo;
     //   long _ClaimNo;
     ////   int UserNo;
     //   int ClaimRepDay = 99;
     //   Bitmap bmp;
     //   string RegPath = "HKEY_LOCAL_MACHINE\\Software\\";
     //   RepositoryItemImageComboBox imagecombo = new RepositoryItemImageComboBox();
     //   ImageComboBoxItem read = new ImageComboBoxItem("", new decimal(new int[] { 1, 0, 0, 0 }), 1);
     //   ImageComboBoxItem Unread = new ImageComboBoxItem("", new decimal(new int[] { 0, 0, 0, 0 }), 2);
     //   string appname = "", formname = "";
     //   Form FormName = null;
     //   bool _deal = false, _claim = false, _oldcontract = false, _reinsurance = false, _claimrep = false, _dealrep = false;
     //   bool _issue = false, _newcontract = false, _dealpay = false, _moveredeal = false, _contactstep = false, _project = false, _operator = false;
     //   #region[Timer Variables]
     //   int second0 = 0, second1 = 0, second2 = 0, second3 = 0, second4 = 0, second5 = 0, second6 = 0, second7 = 0;
     //   int second8 = 0, second9 = 0, second10 = 0, second11 = 0, second12 = 0, second13 = 0, second14 = 0;
     //   Timer tmrDeal = new Timer();
     //   Timer tmrClaim = new Timer();
     //   Timer tmrContract = new Timer();
     //   Timer tmrReInsurance = new Timer();
     //   Timer tmrFinal = new Timer();
     //   Timer tmrFinal1 = new Timer();
     //   Timer tmrClaimRep = new Timer();
     //   Timer tmrDealRep = new Timer();
     //   Timer tmrIssue = new Timer();
     //   Timer tmrNewContract = new Timer();
     //   Timer tmrDealPay = new Timer();
     //   Timer tmrMoveReDeal = new Timer();
     //   Timer tmrContactStep = new Timer();
     //   Timer tmrProject = new Timer();
     //   Timer tmrOperator = new Timer();
     //   int DealRefTime, ClaimRefTime, ContractRefTime, InsuranceRefTime, FinalRefTime, Final1RefTime, DealRepTime;
     //   int ClaimRepTime, IssueRefTime, NewContractRefTime, DealPayRefTime, MoveReDealRefTime, ContactStepRefTime, ProjectRepTime, OperatorRepTime;
     //   #endregion
     //   #region[RowColor Variables]
     //   string ERowcolor1;
     //   string ERowcolor2;
     //   string SRowcolor1;
     //   string SRowcolor2;
     //   string ORowcolor1;
     //   string ORowcolor2;
     //   #endregion
        #endregion
        #region[Байгуулагч функц]
        public frmDashboard(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            //appname = _core.ApplicationName;
            //formname = "Dashboard." + this.Name;
            //FormName = this;
            //FormUtility.RestoreStateForm(appname, ref FormName);
            //Init();
            //if (_core.Resource != null)
            //{
            //    SettingsToolStripMenuItem.Image = _core.Resource.GetImage("dashboard_settings");
            //    imgCollection1.AddImage(_core.Resource.GetImage("dashboard_user"));
            //    imgCollection1.AddImage(_core.Resource.GetImage("dashboard_read"));
            //    imgCollection1.AddImage(_core.Resource.GetImage("dashboard_unread"));
            //}
        }
        #endregion
        //#region[Init]
        //private void Init()
        //{
        //    picClaimReport.DoubleClick += new EventHandler(picClaimReport_DoubleClick);
        //    picDealReport.DoubleClick += new EventHandler(picDealReport_DoubleClick);
        //    //Автоматаар сэргээж байх timer
        //    tmrDeal.Tick += new EventHandler(tmrDeal_Tick);
        //    tmrClaim.Tick += new EventHandler(tmrClaim_Tick);
        //    tmrContract.Tick += new EventHandler(tmrContract_Tick);
        //    tmrReInsurance.Tick += new EventHandler(tmrReInsurance_Tick);
        //    tmrFinal.Tick += new EventHandler(tmrFinal_Tick);
        //    tmrFinal1.Tick += new EventHandler(tmrFinal1_Tick);
        //    tmrDealRep.Tick += new EventHandler(tmrDealRep_Tick);
        //    tmrClaimRep.Tick += new EventHandler(tmrClaimRep_Tick);
        //    tmrIssue.Tick += new EventHandler(tmrIssue_Tick);
        //    tmrNewContract.Tick += new EventHandler(tmrNewContract_Tick);
        //    tmrDealPay.Tick += new EventHandler(tmrDealPay_Tick);
        //    tmrMoveReDeal.Tick += new EventHandler(tmrMoveReDeal_Tick);
        //    tmrContactStep.Tick += new EventHandler(tmrContactStep_Tick);
        //    tmrProject.Tick += new EventHandler(tmrProject_Tick);
        //    tmrOperator.Tick+=new EventHandler(tmrOperator_Tick);

        //    dockManager1.RestoreLayoutFromRegistry(RegPath + _core.RegPath + "\\Dashboard\\PanelPosition"); //Панелуудын үндсэн байрлалыг сэргээх

        //    //Нээлттэй байгааг нь шалгаж сэргээж байна.
        //    if (pnlNewDeal.Visibility != DockVisibility.Hidden)  RefreshNewDeal();
        //    if (pnlTopClaim.Visibility != DockVisibility.Hidden) RefreshClaim();
        //    if (pnlcontract.Visibility != DockVisibility.Hidden) RefreshOldContract();
        //    if (pnlreinsurance.Visibility != DockVisibility.Hidden) RefreshReInsurance();
        //    if (pnlFinal.Visibility != DockVisibility.Hidden) RefreshFinal(); 
        //    if (pnlFinal1.Visibility != DockVisibility.Hidden) RefreshFinal1();
        //    if (pnlDealRep.Visibility != DockVisibility.Hidden) RefreshDealRep(); 
        //    if (pnlClaimRep.Visibility != DockVisibility.Hidden) RefreshClaimRep();
        //    if (pnlIssue.Visibility != DockVisibility.Hidden) RefreshIssue();
        //    if (pnlNewContract.Visibility != DockVisibility.Hidden) RefreshNewContract();
        //    if (pnlDealPay.Visibility != DockVisibility.Hidden) RefreshDealPay();
        //    if (pnlMoveReDeal.Visibility != DockVisibility.Hidden) RefreshMoveReDeal();
        //    if (pnlContactStep.Visibility != DockVisibility.Hidden) RefreshContactStep();
        //    if (pnlProject.Visibility != DockVisibility.Hidden) RefreshProject();
        //    if (pnlOperator.Visibility != DockVisibility.Hidden) RefreshOperator();

        //    //Гридүүдийн тэгш сондгой мөрд өөрчлөлт оруулж болно.
        //    gvwNewDeal.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwNewDeal.OptionsView.EnableAppearanceOddRow = true;
        //    gvwNewDeal.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwClaim.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwClaim.OptionsView.EnableAppearanceOddRow = true;
        //    gvwClaim.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwOldContract.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwOldContract.OptionsView.EnableAppearanceOddRow = true;
        //    gvwOldContract.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwReInsurance.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwReInsurance.OptionsView.EnableAppearanceOddRow = true;
        //    gvwReInsurance.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwIssue.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwIssue.OptionsView.EnableAppearanceOddRow = true;
        //    gvwIssue.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwNewContract.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwNewContract.OptionsView.EnableAppearanceOddRow = true;
        //    gvwNewContract.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwDealPay.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwDealPay.OptionsView.EnableAppearanceOddRow = true;
        //    gvwDealPay.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwMoveReDeal.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwMoveReDeal.OptionsView.EnableAppearanceOddRow = true;
        //    gvwMoveReDeal.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwContactStep.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwContactStep.OptionsView.EnableAppearanceOddRow = true;
        //    gvwContactStep.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwProject.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwProject.OptionsView.EnableAppearanceOddRow = true;
        //    gvwProject.Appearance.FocusedRow.Options.UseBackColor = true;

        //    gvwOperator.OptionsView.EnableAppearanceEvenRow = true;
        //    gvwOperator.OptionsView.EnableAppearanceOddRow = true;
        //    gvwOperator.Appearance.FocusedRow.Options.UseBackColor = true;
        //}
        //#endregion
        #region[FormEvent]
        private void frmDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                //#region[RowColor]
                //ColorValues();
                //#endregion
                //imagecombo.SmallImages = imgCollection1;
                //imagecombo.Items.Add(read);
                //imagecombo.Items.Add(Unread);
                //imagecombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            //dockManager1.SaveLayoutToRegistry(RegPath + _core.RegPath + "\\Dashboard\\PanelPosition");
            //FormUtility.SaveStateForm(appname, ref FormName);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwOldContract);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwNewDeal);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwClaim);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwReInsurance);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwIssue);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwNewContract);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwDealPay);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwDealPay);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwContactStep);
            //FormUtility.SaveStateGrid(appname, formname, ref gvwProject);
        }
        //#region[Timer Events]
        //void tmrDeal_Tick(object sender, EventArgs e)
        //{
        //    second0++;
        //    if (second0 == DealRefTime)
        //    {
        //        tmrDeal.Enabled = false;
        //        RefreshNewDeal();
        //        second0 = 0;
        //    }
        //}
        //void tmrContract_Tick(object sender, EventArgs e)
        //{
        //    second1++;
        //    if (second1 == ContractRefTime)
        //    {
        //        tmrFinal.Enabled = false;
        //        RefreshOldContract();
        //        second1 = 0;
        //    }
        //}
        //void tmrReInsurance_Tick(object sender, EventArgs e)
        //{
        //    second2++;
        //    if (second2 == InsuranceRefTime)
        //    {
        //        tmrReInsurance.Enabled = false;
        //        RefreshReInsurance();
        //        second2 = 0;
        //    }
        //}
        //void tmrFinal_Tick(object sender, EventArgs e)
        //{
        //    second3++;
        //    if (second3 == FinalRefTime)
        //    {
        //        tmrFinal.Enabled = false;
        //        RefreshFinal();
        //        second3 = 0;
        //    }
        //}
        //void tmrFinal1_Tick(object sender, EventArgs e)
        //{
        //    second4++;
        //    if (second4 == Final1RefTime)
        //    {
        //        tmrFinal1.Enabled = false;
        //        RefreshFinal1();
        //        second4 = 0;
        //    }
        //}
        //void tmrClaim_Tick(object sender, EventArgs e)
        //{
        //    second5++;
        //    if (second5 == ClaimRefTime)
        //    {
        //        tmrClaim.Enabled = false;
        //        RefreshClaim();
        //        second5 = 0;
        //    }
        //}
        //void tmrDealRep_Tick(object sender, EventArgs e)
        //{
        //    second6++;
        //    if (second6 == DealRepTime)
        //    {
        //        tmrDealRep.Enabled = false;
        //        RefreshDealRep();
        //        second6 = 0;
        //    }
        //}
        //void tmrClaimRep_Tick(object sender, EventArgs e)
        //{
        //    second7++;
        //    if (second7 == ClaimRefTime)
        //    {
        //        tmrClaimRep.Enabled = false;
        //        RefreshClaimRep();
        //        second7 = 0;
        //    }
        //}
        //void tmrIssue_Tick(object sender, EventArgs e)
        //{
        //    second8++;
        //    if (second8 == IssueRefTime)
        //    {
        //        tmrIssue.Enabled = false;
        //        RefreshIssue();
        //        second8 = 0;
        //    }
        //}
        //void tmrNewContract_Tick(object sender, EventArgs e)
        //{
        //    second9++;
        //    if (second9 == NewContractRefTime)
        //    {
        //        tmrNewContract.Enabled = false;
        //        RefreshNewContract();
        //        second9 = 0;
        //    }
        //}
        //void tmrDealPay_Tick(object sender, EventArgs e)
        //{
        //    second10++;
        //    if (second10 == DealPayRefTime)
        //    {
        //        tmrDealPay.Enabled = false;
        //        RefreshDealPay();
        //        second10 = 0;
        //    }
        //}
        //void tmrMoveReDeal_Tick(object sender, EventArgs e)
        //{
        //    second11++;
        //    if (second11 == MoveReDealRefTime)
        //    {
        //        tmrMoveReDeal.Enabled = false;
        //        RefreshMoveReDeal();
        //        second11 = 0;
        //    }
        //}
        //void tmrContactStep_Tick(object sender, EventArgs e)
        //{
        //    second12++;
        //    if (second12 == ContactStepRefTime)
        //    {
        //        tmrContactStep.Enabled = false;
        //        RefreshContactStep();
        //        second12 = 0;
        //    }
        //}
        //void tmrProject_Tick(object sender, EventArgs e)
        //{
        //    second13++;
        //    if (second13 == ProjectRepTime)
        //    {
        //        tmrProject.Enabled = false;
        //        RefreshProject();
        //        second13 = 0;
        //    }
        //}

        //void tmrOperator_Tick(object sender, EventArgs e)
        //{
        //    second14++;
        //    if (second14 == OperatorRepTime)
        //    {
        //        tmrOperator.Enabled = false;
        //        RefreshOperator();
        //        second14 = 0;
        //    }
        //}
        //#endregion
        #endregion
        //#region[CheckBox]
        //void ri_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        //{
        //    string val = "";
        //    if (e.Value != null)
        //    {
        //        val = e.Value.ToString();
        //    }
        //    else
        //    {
        //        val = "False";
        //    }
        //    switch (val)
        //    {
        //        case "True":
        //            e.CheckState = CheckState.Checked;
        //            break;
        //        case "False":
        //            e.CheckState = CheckState.Unchecked;
        //            break;
        //        case "Yes":
        //            goto case "True";
        //        case "No":
        //            goto case "False";
        //        case "1":
        //            goto case "True";
        //        case "0":
        //            goto case "False";
        //        default:
        //            e.CheckState = CheckState.Checked;
        //            break;
        //    }
        //    e.Handled = true;
        //}
        //public RepositoryItemCheckEdit CreateRepositoryCheckEdit()
        //{
        //    RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
        //    ((System.ComponentModel.ISupportInitialize)(ri)).BeginInit();
        //    ri.AutoHeight = false;
        //    ri.AllowFocused = false;
        //    ri.ValueChecked = 1;
        //    ri.ValueUnchecked = 0;
        //    ((System.ComponentModel.ISupportInitialize)(ri)).EndInit();
        //    ri.QueryCheckStateByValue += new DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventHandler(ri_QueryCheckStateByValue);
        //    return ri;
        //}
        //#endregion
        #region[Лавлагаа]
        private void grdNewDeal_DoubleClick(object sender, EventArgs e)
        {
            //Result res = new Result();
            //try
            //{
            //    if (grdNewDeal.DataSource != null)
            //    {
            //        DataRow DR = gvwNewDeal.GetDataRow(gvwNewDeal.FocusedRowHandle);
            //        _DealNo = Static.ToLong(DR["DealNo"]);
            //        if (_DealNo != 0)
            //        {
            //            object[] obj1 = new object[33];
            //            obj1[0] = _DealNo;
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 101, _core.RemoteObject.User.UserNo, _DealNo, _core.TxnDate });
            //            if (res.ResultNo != 0)
            //            {
            //                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //            }
            //            object[] prop = new object[43];
            //            prop[0] = _DealNo;
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 212, 200096, 200096, prop);
            //            if (res.ResultNo == 0)
            //            {
            //                object[] obj = new object[1];
            //                obj[0] = res.Data.Tables[0].Rows[0];
            //                if (Static.ToLong(DR["POLICYNO"]) == 0)
            //                {
            //                    HeavenPro.Enquiry.InsuranceDealEnquiry frm = new HeavenPro.Enquiry.InsuranceDealEnquiry(_core, _DealNo, obj);
            //                    frm.ShowDialog();
            //                }
            //                else
            //                {
            //                    HeavenPro.Enquiry.PolicyEnquiry frm = new HeavenPro.Enquiry.PolicyEnquiry(_core, _DealNo, obj);
            //                    frm.ShowDialog();
            //                }
            //                RefreshNewDeal();
            //            }
            //            else
            //            {
            //                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void grdOldContract_DoubleClick(object sender, EventArgs e)
        {
            //Result res = new Result();
            //try
            //{
            //    if (gvwOldContract.DataSource != null)
            //    {
            //        DataRow DR = gvwOldContract.GetDataRow(gvwOldContract.FocusedRowHandle);
            //        long _ContractID = Static.ToLong(DR["DealNo"]);
            //        if (_ContractID != 0)
            //        {
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 201, _core.RemoteObject.User.UserNo, _ContractID, _core.TxnDate });
            //            if (res.ResultNo != 0)
            //            {
            //                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //            }

            //            object[] values = new object[49];
            //            values[0] = _ContractID;

            //            object[] obj1 = new object[6];
            //            obj1[0] = _core.User.BranchCode;
            //            obj1[1] = _core.RemoteObject.User.UserNo;
            //            obj1[2] = _core.User.Level1;
            //            obj1[3] = _core.User.Level2;
            //            obj1[4] = _core.User.Level3;
            //            obj1[5] = _core.User.Level4;
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 200051, 200051, 0, 1, new object[] { values, obj1 });
            //            if (res.ResultNo == 0)
            //            {
            //                if (res.Data.Tables[0] != null && res.Data.Tables[0].Rows.Count != 0)
            //                {
            //                    object[] obj = new object[2];
            //                    obj[0] = res.Data.Tables[0].Rows[0];
            //                    HeavenPro.Enquiry.InsuranceDealEnquiry frm = new HeavenPro.Enquiry.InsuranceDealEnquiry(_core, _ContractID, obj);
            //                    frm.ShowDialog();
            //                    RefreshOldContract();
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void grdReInsurance_DoubleClick(object sender, EventArgs e)
        {
            //Result res = new Result();
            //try
            //{
            //    if (gvwReInsurance.DataSource != null)
            //    {
            //        DataRow DR = gvwReInsurance.GetDataRow(gvwReInsurance.FocusedRowHandle);
            //        _ReDealNo = Static.ToLong(DR["REDEALNO"]);
            //        if (_ReDealNo != 0)
            //        {
            //            object[] values = new object[13];
            //            values[0] = _ReDealNo;
            //            object[] obj1 = new object[5];
            //            obj1[0] = _core.RemoteObject.User.UserNo;
            //            obj1[1] = _core.User.Level1;
            //            obj1[2] = _core.User.Level2;
            //            obj1[3] = _core.User.Level3;
            //            obj1[4] = _core.User.Level4;

            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 401, _core.RemoteObject.User.UserNo, _ReDealNo, _core.TxnDate });
            //            if (res.ResultNo != 0)
            //            {
            //                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //            }
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 200301, 200301, new object[] { values, obj1 });
            //            if (res.ResultNo == 0)
            //            {
            //                object[] obj = new object[1];
            //                obj[0] = res.Data.Tables[0].Rows[0];
            //                HeavenPro.Enquiry.ReInsuranceDealEnquiry frm = new HeavenPro.Enquiry.ReInsuranceDealEnquiry(_core, _ReDealNo, obj);
            //                frm.ShowDialog();
            //                RefreshReInsurance();
            //            }
            //            else
            //            {
            //                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void grdClaim_DoubleClick(object sender, EventArgs e)
        {
            //Result res = new Result();
            //try
            //{
            //    if (gvwClaim.DataSource != null)
            //    {
            //        DataRow DR = gvwClaim.GetDataRow(gvwClaim.FocusedRowHandle);
            //        _ClaimNo = Static.ToLong(DR["CLAIMID"]);
            //        if (_ClaimNo != 0)
            //        {
            //            //MarkedTask руу уншсан төлөвтэй болгож хадгалж байна.
            //            object[] obj3 = new object[40];
            //            obj3[0] = _ClaimNo;
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 301, _core.RemoteObject.User.UserNo, _ClaimNo, _core.TxnDate });
            //            if (res.ResultNo != 0)
            //            {
            //                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //            }
            //            //Дэлгэрэнгүй мэдээлэл авч байна.
            //            object[] obj2 = new object[6];
            //            obj2[0] = _core.User.BranchCode;
            //            obj2[1] = _core.RemoteObject.User.UserNo;
            //            obj2[2] = _core.User.Level1;
            //            obj2[3] = _core.User.Level2;
            //            obj2[4] = _core.User.Level3;
            //            obj2[5] = _core.User.Level4;
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 206, 200121, 200121, 0, 1, new object[] { obj3, obj2 });
            //            if (res.ResultNo == 0)
            //            {
            //                object[] obj = new object[1];
            //                obj[0] = res.Data.Tables[0].Rows[0];
            //                HeavenPro.Enquiry.ClaimEnquiry frm = new HeavenPro.Enquiry.ClaimEnquiry(_core, _ClaimNo, obj);
            //                frm.ShowDialog();
            //                RefreshClaim();
            //            }
            //            else
            //            {
            //                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void grdMoveReDeal_DoubleClick(object sender, EventArgs e)
        {
            //Result res = new Result();
            //try
            //{
            //    if (grdMoveReDeal.DataSource != null)
            //    {
            //        DataRow DR = gvwMoveReDeal.GetFocusedDataRow();
            //        _DealNo = Static.ToLong(DR["DealNo"]);
            //        if (_DealNo != 0)
            //        {
            //            object[] obj1 = new object[33];
            //            obj1[0] = _DealNo;
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 801, _core.RemoteObject.User.UserNo, _DealNo, _core.TxnDate });
            //            if (res.ResultNo != 0)
            //            {
            //                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //            }
            //            object[] prop=new object[47];
            //            prop[0] = _DealNo;
            //            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 212, 200051, 200051, prop);
            //            if (res.ResultNo == 0)
            //            {
            //                object[] obj = new object[2];
            //                obj[0] = res.Data.Tables[0].Rows[0];
            //                HeavenPro.Enquiry.InsuranceDealEnquiry frm = new HeavenPro.Enquiry.InsuranceDealEnquiry(_core, _DealNo, obj);
            //                frm.ShowDialog();
            //                RefreshMoveReDeal();
            //            }
            //            else
            //            {
            //                MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        void picDealReport_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    frmDealRepEnq frm = new frmDealRepEnq(_core);
            //    frm.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        void picClaimReport_DoubleClick(object sender, EventArgs e)
        {
            //try
            //{
            //    frmClaimRepEnq frm = new frmClaimRepEnq(_core, ClaimRepDay);
            //    frm.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void gvwIssue_DoubleClick(object sender, EventArgs e)
        {
            //DataRow dr=gvwIssue.GetFocusedDataRow();
            //if(dr!=null)
            //{
            //    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 501, _core.RemoteObject.User.UserNo, Static.ToLong(dr["JRNO"]), _core.TxnDate });
            //    if (res.ResultNo != 0)
            //    {
            //        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //    }
            //    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 223, 310112, 310112, new object[] { dr["JRNO"] });
            //    if (r.ResultNo == 0)
            //    {
            //        if (r.Data.Tables[0].Rows.Count != 0)
            //        {
            //            object[] obj = new object[1];
            //            obj[0] = r.Data.Tables[0].Rows[0];
            //            HeavenPro.Enquiry.IssueTxnEnquiry frm = new Enquiry.IssueTxnEnquiry(_core, Static.ToLong(dr["JRNO"]));
            //            frm.ShowDialog();
            //            RefreshIssue();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Мэдээлэл олдсонгүй.");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
            //    }
            //}
        }
        private void gvwNewContract_DoubleClick(object sender, EventArgs e)
        {
            //DataRow dr=gvwNewContract.GetFocusedDataRow();
            //if (dr != null)
            //{
            //    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 601, _core.RemoteObject.User.UserNo, dr["ISSUEID"], _core.TxnDate });
            //    if (res.ResultNo != 0)
            //    {
            //        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //    }
            //    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 223, 300015, 300015, new object[] { dr["ISSUEID"] });
            //    if (r.ResultNo == 0)
            //    {
            //        if (r.Data.Tables[0].Rows.Count != 0)
            //        {
            //            object[] obj = new object[1];
            //            obj[0] = r.Data.Tables[0].Rows[0];
            //            HeavenPro.Enquiry.DashboardIssueEnquiry frm = new Enquiry.DashboardIssueEnquiry(_core, obj);
            //            frm.ShowDialog();
            //            RefreshNewContract();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Мэдээлэл олдсонгүй.");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
            //    }
            //}
        }
        private void grdContactStep_DoubleClick(object sender, EventArgs e)
        {
            //DataRow dr = gvwContactStep.GetFocusedDataRow();
            //if (dr != null)
            //{
            //    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 901, _core.RemoteObject.User.UserNo, dr["ISSUEID"], _core.TxnDate });
            //    if (res.ResultNo != 0)
            //    {
            //        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //    }

            //    HeavenPro.Issue.FormContactEnq f1 = new HeavenPro.Issue.FormContactEnq(_core, Static.ToLong(dr["Custno"]), Static.ToLong(dr["IssueID"]));
            //    f1.Show();
            //    RefreshContactStep();
            //}
        }
        private void gvwProject_DoubleClick(object sender, EventArgs e)
        {
            //DataRow dr = gvwProject.GetFocusedDataRow();
            //if (dr != null)
            //{
            //    Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300005, 300005, new object[] { 902, _core.RemoteObject.User.UserNo, dr["ISSUEID"], _core.TxnDate });
            //    if (res.ResultNo != 0)
            //    {
            //        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            //    }
            //    HeavenPro.Issue.FormProjectEnq frm = new Issue.FormProjectEnq(_core, Static.ToLong(dr["projectid"]), Static.ToLong(dr["IssueID"]));
            //    frm.Show();
            //    RefreshProject();
            //}      
        }
        #endregion
        //#region[Function]
        //#region[Refresh]
        //private void RefreshNewDeal()
        //{
        //    Result r = new Result();
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnDealRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //            btnDealEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //        }
        //        long Amount = Static.ToLong(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "Amount", 10000));
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "UnReadDay", 99));
        //        DealRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "RefreshTime", 5));
        //        int Type = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\NewDeal", "Type", 3));

        //        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300000, 300000, new object[]
        //        { 
        //            101,
        //            ReadDay,
        //            Amount,
        //            UnReadDay,
        //            _core.TxnDate,
        //            Type,
        //            _core.User.BranchCode,
        //            _core.RemoteObject.User.UserNo, 
        //            _core.User.Level1,
        //            _core.User.Level2,
        //            _core.User.Level3,
        //            _core.User.Level4
        //        });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdNewDeal.DataSource = dt;
        //            SetNewDeal();
        //        }
        //        else
        //        {
        //            grdNewDeal.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show("Шинэ хэлцэл болон гэрээт баталгаа:" + r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrDeal.Enabled = false;
        //        second0 = 0;
        //        tmrDeal.Interval = 60000;
        //        tmrDeal.Enabled = true;
        //        _deal = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshOldContract()
        //{
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnOldContEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //            btnContRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //        }
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\OldContract", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\OldContract", "UnReadDay", 99));
        //        int Userno = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\OldContract", "UserNo", 1));
        //        ContractRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\OldContract", "RefreshTime", 5));

        //        Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300001, 300001, new object[] { 201, ReadDay, Userno, UnReadDay, _core.TxnDate });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdOldContract.DataSource = dt;
        //            SetOldContract();
        //        }
        //        else
        //        {
        //            grdOldContract.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrContract.Enabled = false;
        //        second1 = 0;
        //        tmrContract.Interval = 60000;
        //        tmrContract.Enabled = true;
        //        _oldcontract = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshClaim()
        //{
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnClaimEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //            btnClaimRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //        }
        //        int Amount = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Claim", "Amount", 100000));
        //        ClaimRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Claim", "RefreshTime", 5));
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Claim", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Claim", "UnReadDay", 99));
        //        Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300003, 300003, new object[]
        //        { 
        //            301,
        //            ReadDay, 
        //            Amount,
        //            UnReadDay, 
        //            _core.TxnDate,
        //            _core.User.BranchCode,
        //            _core.RemoteObject.User.UserNo, 
        //            _core.User.Level1,
        //            _core.User.Level2,
        //            _core.User.Level3,
        //            _core.User.Level4
        //        });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdClaim.DataSource = dt;
        //            SetClaim();
        //        }
        //        else
        //        {
        //            grdClaim.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrClaim.Enabled = false;
        //        second5 = 0;
        //        tmrClaim.Interval = 60000;
        //        tmrClaim.Enabled = true;
        //        _claim = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshReInsurance()
        //{
        //    try
        //    {
        //        _reinsurance = true;
        //        if (_core.Resource != null)
        //        {
        //            btnReInsEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //            btnReInsRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //        }

        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\ReInsurance", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\ReInsurance", "UnReadDay", 99));
        //        int UserNo = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\ReInsurance", "UserNo", 1));
        //        InsuranceRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\ReInsurance", "RefreshTime", 5));
        //        Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300004, 300004, new object[] { 401, ReadDay, UserNo, UnReadDay, _core.TxnDate });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = null;
        //            dt = r.Data.Tables[0];
        //            grdReInsurance.DataSource = dt;
        //            SetReInsurance();
        //        }
        //        else
        //        {
        //            grdReInsurance.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrReInsurance.Enabled = false;
        //        second2 = 0;
        //        tmrReInsurance.Interval = 60000;
        //        tmrReInsurance.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshFinal()
        //{
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnPicSave.Image = _core.Resource.GetImage("paging_export");
        //            btnFinalEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //            btnFinalRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //        }
        //        pnlFinal.Text = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "Name", "Final Statement"));
        //        int ViewType = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "ViewType", 0));
        //        int ReportNo = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "ReportNo", 261001));
        //        FinalRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements", "RefreshTime", 5));
        //        ISM.Report.Chart chart = new ISM.Report.Chart();
        //        chart.Client = _core.RemoteObject.Connection;
        //        chart.UserNo = _core.RemoteObject.User.UserNo;
        //        Result r = chart.Generate(ReportNo, ReportNo, _core.TxnDate, false);
        //        if (r.ResultNo == 0)
        //        {
        //            picMain.Image = null;
        //            if (ViewType == 0)
        //            {
        //                bmp = ISM.Lib.Excel.GetImageSheet(chart.Buffer);
        //                if (bmp != null) picMain.Image = bmp;
        //            }
        //            else
        //            {
        //                Bitmap bmp = ISM.Lib.Excel.GetImageChart(chart.Buffer);
        //                if (bmp != null) picMain.Image = bmp;
        //            }
        //        }
        //        else
        //        {
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " : " + r.ResultDesc);
        //        }
        //        tmrFinal.Enabled = false;
        //        second3 = 0;
        //        tmrFinal.Interval = 60000;
        //        tmrFinal.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshFinal1()
        //{
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnFinal1Save.Image = _core.Resource.GetImage("paging_export");
        //            btnFinal1Edt.Image = _core.Resource.GetImage("dashboard_edit");
        //            btnFinal1Ref.Image = _core.Resource.GetImage("dashboard_refresh");
        //        }
        //        int ViewType = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements1", "ViewType", 0));
        //        int ReportNo = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements1", "ReportNo", 261001));
        //        Final1RefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Final Statements1", "RefreshTime", 5));
        //        ISM.Report.Chart chart = new ISM.Report.Chart();
        //        chart.Client = _core.RemoteObject.Connection;
        //        chart.UserNo = _core.RemoteObject.User.UserNo;
        //        Result r = chart.Generate(ReportNo, ReportNo, _core.TxnDate, false);
        //        if (r.ResultNo == 0)
        //        {
        //            picFinal1.Image = null;
        //            if (ViewType == 0)
        //            {
        //                bmp = ISM.Lib.Excel.GetImageSheet(chart.Buffer);
        //                if (bmp != null) picFinal1.Image = bmp;
        //            }
        //            else
        //            {
        //                Bitmap bmp = ISM.Lib.Excel.GetImageChart(chart.Buffer);
        //                if (bmp != null) picFinal1.Image = bmp;
        //            }
        //        }
        //        else
        //        {
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " : " + r.ResultDesc);
        //        }
        //        tmrFinal1.Enabled = false;
        //        second4 = 0;
        //        tmrFinal1.Interval = 60000;
        //        tmrFinal1.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private int RefreshDealRep()
        //{
        //    btnDealRepEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //    btnDealRepSave.Image = _core.Resource.GetImage("paging_export");
        //    btnDealRepRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //    DealRepTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Deal Report\\Refresh Time", 5));
        //    string error = "";
        //    int success = 0;
        //    try
        //    {
        //        Generator gen = new Generator();
        //        gen.ReportPath = _core.ReportPathIn;
        //        gen.ReportOutPath = _core.ReportPathOut;
        //        gen.Client = _core.RemoteObject.Connection;
        //        gen.UserNo = _core.RemoteObject.User.UserNo;
        //        success = gen.Prepare(300010, ref error);
        //        if (success != 0)
        //        {
        //            return success;
        //        }

        //        Stream stream = null;

        //        success = gen.Generate("XLS", ref stream, ref error);
        //        if (success != 0) return success;

        //        byte[] bytes = new byte[stream.Length];
        //        stream.Read(bytes, 0, bytes.Length);
        //        picDealReport.Image = ISM.Lib.Excel.GetImageSheet(bytes);
        //        tmrDealRep.Enabled = false;
        //        second6 = 0;
        //        tmrDealRep.Interval = 60000;
        //        tmrDealRep.Enabled = true;
        //        _dealrep = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    return success;
        //}
        //private int RefreshClaimRep()
        //{
        //    ClaimRepDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Claim Report\\Days", "99"));
        //    btnClaimRepSave.Image = _core.Resource.GetImage("paging_export");
        //    btnClaimRepEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //    btnClaimRepRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //    ClaimRepTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Claim Report\\Refresh Time", 5));

        //    string error = "";
        //    int success = 0;
        //    try
        //    {
        //        Generator gen = new Generator();
        //        gen.ReportPath = _core.ReportPathIn;
        //        gen.ReportOutPath = _core.ReportPathOut;
        //        gen.Client = _core.RemoteObject.Connection;
        //        gen.UserNo = _core.RemoteObject.User.UserNo;
        //        success = gen.Prepare(300011, ref error);
        //        if (success != 0)
        //        {
        //            return success;
        //        }
        //        gen.SetValue("days", ClaimRepDay);
        //        gen.SetValue("date1", Static.ToDate(_core.TxnDate));
        //        Stream stream = null;
        //        success = gen.Generate("XLS", ref stream, ref error);
        //        if (success != 0) return success;
        //        byte[] bytes = new byte[stream.Length];
        //        stream.Read(bytes, 0, bytes.Length);
        //        picClaimReport.Image = ISM.Lib.Excel.GetImageSheet(bytes);
        //        tmrClaimRep.Enabled = false;
        //        second7 = 0;
        //        tmrClaimRep.Interval = 60000;
        //        tmrClaimRep.Enabled = true;
        //        _claimrep = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    return success;
        //}
        //private void RefreshIssue()
        //{
        //    Result r = new Result();
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnIssueRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //            btnIssueEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //        }
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Issue", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Issue", "UnReadDay", 99));
        //        IssueRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Issue", "RefreshTime", 5));
        //        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300012, 300012, new object[] { 501, ReadDay, _core.RemoteObject.User.UserNo, UnReadDay, _core.TxnDate, _core.User.TxnGroupLevel });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdIssue.DataSource = dt;
        //            SetIssue();
        //        }
        //        else
        //        {
        //            grdIssue.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrIssue.Enabled = false;
        //        second8 = 0;
        //        tmrIssue.Interval = 60000;
        //        tmrIssue.Enabled = true;
        //        _issue = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshNewContract()
        //{
        //    Result r = new Result();
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnContractRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //            btnContractEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //        }
        //        int UserNo = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\NewContract", "UserNo", 1));
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\NewContract", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\NewContract", "UnReadDay", 99));
        //        NewContractRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\NewContract", "RefreshTime", 5));
        //        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300013, 300013, new object[] { 601, ReadDay, UserNo, UnReadDay, _core.TxnDate, _core.User.TxnGroupLevel });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdNewContract.DataSource = dt;
        //            SetNewContract();
        //        }
        //        else
        //        {
        //            grdNewContract.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrNewContract.Enabled = false;
        //        second9 = 0;
        //        tmrNewContract.Interval = 60000;
        //        tmrNewContract.Enabled = true;
        //        _newcontract = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshDealPay()
        //{
        //    Result r = new Result();
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnDealPayRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //            btnDealPayEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //        }
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\DealPay", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\DealPay", "UnReadDay", 99));
        //        DealPayRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\DealPay", "RefreshTime", 5));
        //        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300014, 300014, new object[] { 701, ReadDay, UnReadDay, _core.TxnDate });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdDealPay.DataSource = dt;
        //            SetDealPay();
        //        }
        //        else
        //        {
        //            grdDealPay.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrDealPay.Enabled = false;
        //        second10 = 0;
        //        tmrDealPay.Interval = 60000;
        //        tmrDealPay.Enabled = true;
        //        _dealpay = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshMoveReDeal()
        //{
        //    Result r = new Result();
        //    try
        //    {
        //        grdMoveReDeal.DataSource = null;
        //        if (_core.Resource != null)
        //        {
        //            btnMoveReDealRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //            btnMoveReDealEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //        }
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\MoveReDeal", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\MoveReDeal", "UnReadDay", 99));
        //        MoveReDealRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\MoveReDeal", "RefreshTime", 5));
        //        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300016, 300016, new object[] 
        //        {
        //            801,
        //            ReadDay, 
        //            UnReadDay,
        //            _core.TxnDate,
        //            _core.User.BranchCode,
        //            _core.RemoteObject.User.UserNo, 
        //            _core.User.Level1,
        //            _core.User.Level2,
        //            _core.User.Level3,
        //            _core.User.Level4
        //        });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdMoveReDeal.DataSource = dt;
        //            SetMoveReDeal();
        //        }
        //        else
        //        {
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrMoveReDeal.Enabled = false;
        //        second11 = 0;
        //        tmrMoveReDeal.Interval = 60000;
        //        tmrMoveReDeal.Enabled = true;
        //        _moveredeal = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshContactStep()
        //{
        //    Result r = new Result();
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnContactStepRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //            btnContactStepEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //        }
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\ContactStep", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\ContactStep", "UnReadDay", 99));
        //        ContactStepRefTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\ContactStep", "RefreshTime", 5));
        //        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300017, 300017, new object[] { 901, ReadDay, _core.RemoteObject.User.UserNo, UnReadDay, _core.TxnDate, _core.User.TxnGroupLevel });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdContactStep.DataSource = dt;
        //            SetContactStep();
        //        }
        //        else
        //        {
        //            grdContactStep.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrContactStep.Enabled = false;
        //        second12 = 0;
        //        tmrContactStep.Interval = 60000;
        //        tmrContactStep.Enabled = true;
        //        _contactstep = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshProject()
        //{
        //    Result r = new Result();
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnProjectRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //            btnProjectEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //        }
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Project", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Project", "UnReadDay", 99));
        //        ProjectRepTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Project", "RefreshTime", 5));
        //        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300018, 300018, new object[] { 902, ReadDay, _core.RemoteObject.User.UserNo, UnReadDay, _core.TxnDate });
        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdProject.DataSource = dt;
        //            SetProject();
        //        }
        //        else
        //        {
        //            grdProject.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrProject.Enabled = false;
        //        second13 = 0;
        //        tmrProject.Interval = 60000;
        //        tmrProject.Enabled = true;
        //        _project = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //private void RefreshOperator()
        //{
        //    Result r = new Result();
        //    try
        //    {
        //        if (_core.Resource != null)
        //        {
        //            btnOperatorRef.Image = _core.Resource.GetImage("dashboard_refresh");
        //            btnOperatorEdt.Image = _core.Resource.GetImage("dashboard_edit");
        //        }
        //        int ReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Operator", "ReadDay", 9));
        //        int UnReadDay = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Operator", "UnReadDay", 99));
        //        OperatorRepTime = Static.ToInt(Static.RegisterGet(_core.RegPath, "Dashboard\\Operator", "RefreshTime", 5));
        //        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 221, 300019, 300019,
        //            new object[] {
        //            903,
        //            ReadDay,
        //            UnReadDay,
        //            _core.TxnDate,
        //            _core.User.BranchCode,
        //            _core.RemoteObject.User.UserNo,
        //            _core.User.Level1,
        //            _core.User.Level2,
        //            _core.User.Level3,
        //            _core.User.Level4
        //        });

        //        if (r.ResultNo == 0)
        //        {
        //            dt = r.Data.Tables[0];
        //            grdOperator.DataSource = dt;
        //            SetOperator();
        //        }
        //        else
        //        {
        //            grdOperator.DataSource = null;
        //            if (r.ResultNo != 9110014)
        //                MessageBox.Show(r.ResultNo + " " + r.ResultDesc);
        //        }
        //        tmrOperator.Enabled = false;
        //        second14 = 0;
        //        tmrOperator.Interval = 60000;
        //        tmrOperator.Enabled = true;
        //        _operator = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //#endregion
        //#region[Set]
        //void SetNewDeal()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwNewDeal);
        //    gvwNewDeal.Columns[0].Caption = "Төлөв";
        //    gvwNewDeal.Columns[0].ColumnEdit = imagecombo;
        //    gvwNewDeal.Columns[1].Caption = "Хэлцэлийн дугаар";
        //    gvwNewDeal.Columns[2].Caption = "Бүтээгдэхүүний нэр";
        //    gvwNewDeal.Columns[3].Caption = "Харилцагчийн нэр";
        //    gvwNewDeal.Columns[4].Caption = "Регистерийн дугаар";
        //    gvwNewDeal.Columns[5].Caption = "Дүн";
        //    gvwNewDeal.Columns[6].Caption = "Зөвшөөрсөн огноо";
        //    gvwNewDeal.Columns[7].Caption = "Хариуцсан хэрэглэгч";
        //    gvwNewDeal.Columns[8].Caption = "Гэрээт баталгааны дугаар";
        //    gvwNewDeal.Columns[7].ImageIndex = 0;

        //    gvwNewDeal.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwNewDeal.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwNewDeal.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwNewDeal.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwNewDeal.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwNewDeal.Columns[5].OptionsColumn.AllowEdit = false;
        //    gvwNewDeal.Columns[6].OptionsColumn.AllowEdit = false;
        //    gvwNewDeal.Columns[7].OptionsColumn.AllowEdit = false;
        //    gvwNewDeal.Columns[8].OptionsColumn.AllowEdit = false;
        //}
        //void SetOldContract()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwOldContract);
        //    gvwOldContract.Columns[0].Caption = "Төлөв";
        //    gvwOldContract.Columns[0].ColumnEdit = imagecombo;
        //    gvwOldContract.Columns[1].Caption = "Хэлцэлийн дугаар";
        //    gvwOldContract.Columns[2].Caption = "Бүтээгдэхүүний нэр";
        //    gvwOldContract.Columns[3].Caption = "Харилцагчийн нэр";
        //    gvwOldContract.Columns[4].Caption = "Регистерийн дугаар";
        //    gvwOldContract.Columns[5].Caption = "Дүн";
        //    gvwOldContract.Columns[6].Caption = "Дуусах огноо";
        //    gvwOldContract.Columns[7].Caption = "Хариуцсан хэрэглэгч";
        //    gvwOldContract.Columns[7].ImageIndex = 0;

        //    gvwOldContract.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwOldContract.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwOldContract.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwOldContract.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwOldContract.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwOldContract.Columns[5].OptionsColumn.AllowEdit = false;
        //    gvwOldContract.Columns[6].OptionsColumn.AllowEdit = false;
        //    gvwOldContract.Columns[7].OptionsColumn.AllowEdit = false;
        //}
        //void SetClaim()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwClaim);
        //    gvwClaim.Columns[0].Caption = "Төлөв";
        //    gvwClaim.Columns[0].ColumnEdit = imagecombo;
        //    gvwClaim.Columns[1].Caption = "Нөхөн төлбөрийн дугаар";
        //    gvwClaim.Columns[2].Caption = "Хэлцэлийн дугаар";
        //    gvwClaim.Columns[3].Caption = "Харилцагчийн нэр";
        //    gvwClaim.Columns[4].Caption = "Дүн";
        //    gvwClaim.Columns[5].Caption = "Зөвшөөрсөн огноо";

        //    gvwClaim.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwClaim.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwClaim.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwClaim.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwClaim.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwClaim.Columns[5].OptionsColumn.AllowEdit = false;
        //}
        //void SetReInsurance()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwReInsurance);
        //    gvwReInsurance.Columns[0].Caption = "Төлөв";
        //    gvwReInsurance.Columns[0].ColumnEdit = imagecombo;
        //    gvwReInsurance.Columns[1].Caption = "Хэлцлийн дугаар";
        //    gvwReInsurance.Columns[2].Caption = "Бүтээгдэхүүний дугаар";
        //    gvwReInsurance.Columns[3].Caption = "Бүтээгдэхүүний нэр";
        //    gvwReInsurance.Columns[4].Caption = "Эхлэх огноо";
        //    gvwReInsurance.Columns[5].Caption = "Дуусах огноо";

        //    gvwReInsurance.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwReInsurance.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwReInsurance.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwReInsurance.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwReInsurance.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwReInsurance.Columns[5].OptionsColumn.AllowEdit = false;

        //}
        //void SetIssue()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwIssue);
        //    gvwIssue.Columns[0].Caption = "Төлөв";
        //    gvwIssue.Columns[0].ColumnEdit = imagecombo;
        //    gvwIssue.Columns[1].Caption = "Товч мэдээлэл";
        //    gvwIssue.Columns[2].Caption = "Товч утга";
        //    gvwIssue.Columns[3].Caption = "Шатлалын нэр";
        //    gvwIssue.Columns[4].Caption = "Хариуцсан хэрэглэгч";
        //    gvwIssue.Columns[5].Caption = "Дараа хийх ажлын төлөвлөгөө";
        //    gvwIssue.Columns[6].Caption = "Дараагийн удаа уулзах огноо";
        //    gvwIssue.Columns[7].Caption = "Журналын дугаар";
        //    gvwIssue.Columns[7].Visible = false;

        //    gvwIssue.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwIssue.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwIssue.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwIssue.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwIssue.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwIssue.Columns[5].OptionsColumn.AllowEdit = false;
        //    gvwIssue.Columns[6].OptionsColumn.AllowEdit = false;
        //    gvwIssue.Columns[7].OptionsColumn.AllowEdit = false;

        //}
        //void SetNewContract()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwNewContract);
        //    gvwNewContract.Columns[0].Caption = "Төлөв";
        //    gvwNewContract.Columns[0].ColumnEdit = imagecombo;
        //    gvwNewContract.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[1].Caption = "Харилцагчийн нэр";
        //    gvwNewContract.Columns[2].Caption = "Товч утга";
        //    gvwNewContract.Columns[3].Caption = "Дэлгэрэнгүй";
        //    gvwNewContract.Columns[4].Caption = "Асуудлын төлөв";
        //    gvwNewContract.Columns[5].Caption = "Шатлал";
        //    gvwNewContract.Columns[6].Caption = "Үүсгэсэн огноо";
        //    gvwNewContract.Columns[7].Caption = "Хариуцсан хэрэглэгч";
        //    gvwNewContract.Columns[8].Caption = "Дуусгах огноо";
        //    gvwNewContract.Columns[9].Caption = "Хариуцсан хэрэглэгчийн дугаар";
        //    gvwNewContract.Columns[9].Visible = false;
        //    gvwNewContract.Columns[10].Caption = "Асуудлын дугаар";
        //    gvwNewContract.Columns[10].Visible = false;
        //    gvwNewContract.Columns[7].ImageIndex = 0;

        //    gvwNewContract.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[5].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[6].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[7].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[8].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[9].OptionsColumn.AllowEdit = false;
        //    gvwNewContract.Columns[10].OptionsColumn.AllowEdit = false;
        //}
        //void SetDealPay()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwDealPay);
        //    gvwDealPay.Columns[0].Caption = "Төлөв";
        //    gvwDealPay.Columns[0].Visible = false;
        //    gvwDealPay.Columns[1].Caption = "Хэлцэлийн дугаар";
        //    gvwDealPay.Columns[2].Caption = "Хариуцсан хэрэглэгч";
        //    gvwDealPay.Columns[3].Caption = "Төлбөр хийх огноо";
        //    gvwDealPay.Columns[4].Caption = "Валют";
        //    gvwDealPay.Columns[5].Caption = "Төлбөрийн дүн";
        //    gvwDealPay.Columns[2].ImageIndex = 0;

        //    gvwDealPay.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwDealPay.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwDealPay.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwDealPay.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwDealPay.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwDealPay.Columns[5].OptionsColumn.AllowEdit = false;
        //}
        //void SetMoveReDeal()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwMoveReDeal);
        //    gvwMoveReDeal.Columns[0].Caption = "Төлөв";
        //    gvwMoveReDeal.Columns[0].ColumnEdit = imagecombo;
        //    gvwMoveReDeal.Columns[1].Caption = "Хэлцэлийн дугаар";
        //    gvwMoveReDeal.Columns[2].Caption = "Бүтээгдэхүүний нэр";
        //    gvwMoveReDeal.Columns[3].Caption = "Харилцагчийн нэр";
        //    gvwMoveReDeal.Columns[4].Caption = "Регистерийн дугаар";
        //    gvwMoveReDeal.Columns[5].Caption = "Дүн";
        //    gvwMoveReDeal.Columns[6].Caption = "Зөвшөөрсөн огноо";
        //    gvwMoveReDeal.Columns[7].Caption = "Хариуцсан хэрэглэгч";
        //    gvwMoveReDeal.Columns[7].ImageIndex = 0;

        //    gvwMoveReDeal.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwMoveReDeal.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwMoveReDeal.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwMoveReDeal.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwMoveReDeal.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwMoveReDeal.Columns[5].OptionsColumn.AllowEdit = false;
        //    gvwMoveReDeal.Columns[6].OptionsColumn.AllowEdit = false;
        //    gvwMoveReDeal.Columns[7].OptionsColumn.AllowEdit = false;
        //}
        //void SetContactStep()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwContactStep);
        //    gvwContactStep.Columns[0].Caption = "Төлөв";
        //    gvwContactStep.Columns[0].ColumnEdit = imagecombo;
        //    gvwContactStep.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[1].Caption = "Харилцагчийн нэр";
        //    gvwContactStep.Columns[2].Caption = "Товч утга";
        //    gvwContactStep.Columns[3].Caption = "Дэлгэрэнгүй";
        //    gvwContactStep.Columns[4].Caption = "Асуудлын төлөв";
        //    gvwContactStep.Columns[5].Caption = "Шатлал";
        //    gvwContactStep.Columns[6].Caption = "Үүсгэсэн огноо";
        //    gvwContactStep.Columns[7].Caption = "Хариуцсан хэрэглэгч";
        //    gvwContactStep.Columns[8].Caption = "Дуусгах огноо";
        //    gvwContactStep.Columns[9].Caption = "Хариуцсан хэрэглэгчийн дугаар";
        //    gvwContactStep.Columns[9].Visible = false;
        //    gvwContactStep.Columns[10].Caption = "Асуудлын дугаар";
        //    gvwContactStep.Columns[10].Visible = false;
        //    gvwContactStep.Columns[11].Caption = "Харилцагчийн дугаар";
        //    gvwContactStep.Columns[11].Visible = false;
        //    gvwContactStep.Columns[7].ImageIndex = 0;

        //    gvwContactStep.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[5].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[6].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[7].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[8].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[9].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[10].OptionsColumn.AllowEdit = false;
        //    gvwContactStep.Columns[11].OptionsColumn.AllowEdit = false;
        //}
        //void SetProject()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwProject);
        //    gvwProject.Columns[0].Caption = "Төлөв";
        //    gvwProject.Columns[0].ColumnEdit = imagecombo;
        //    gvwProject.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[1].Caption = "Асуудлын дугаар";
        //    gvwProject.Columns[2].Caption = "Төслийн дугаар";
        //    gvwProject.Columns[3].Caption = "Нэр";
        //    gvwProject.Columns[4].Caption = "Товч нэр";
        //    gvwProject.Columns[5].Caption = "Төслийг хариуцагч";
        //    gvwProject.Columns[6].Caption = "Төслийн төрөл";
        //    gvwProject.Columns[7].Caption = "Үүсгэсэн хэрэглэгч";
        //    gvwProject.Columns[8].Caption = "Үүсгэсэн огноо";
        //    gvwProject.Columns[7].ImageIndex = 0;

        //    gvwProject.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[5].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[6].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[7].OptionsColumn.AllowEdit = false;
        //    gvwProject.Columns[8].OptionsColumn.AllowEdit = false;
        //}
        //void SetOperator()
        //{
        //    FormUtility.RestoreStateGrid(appname, formname, ref gvwOperator);
        //    gvwOperator.Columns[0].Caption = "Төлөв";
        //    gvwOperator.Columns[0].ColumnEdit = imagecombo;
        //    gvwOperator.Columns[1].Caption = "Дуудлага бүртгэсэн хэрэглэгч";
        //    gvwOperator.Columns[2].Caption = "Гүйлгээний огноо";
        //    gvwOperator.Columns[3].Caption = "Дуудлага өгсөн хүний нэр";
        //    gvwOperator.Columns[4].Caption = "Дуудлага өгсөн дугаар";
        //    gvwOperator.Columns[5].Caption = "Харилцагчийн нэр";
        //    gvwOperator.Columns[6].Caption = "Хэлцэлийн дугаар";
        //    gvwOperator.Columns[7].Caption = "Дуудлагын төлөв";
        //    gvwOperator.Columns[8].Caption = "Дуудлага өгсөн огноо";
        //    gvwOperator.Columns[9].Caption = "Эрсдэл";
        //    gvwOperator.Columns[10].Caption = "Товч утга";
        //    gvwOperator.Columns[11].Caption = "Хариуцсан хэрэглэгч";
        //    gvwOperator.Columns[12].Caption = "Учирсан эрсдэл";
        //    gvwOperator.Columns[13].Caption = "Нөхөн төлбөрийн дүн";
        //    gvwOperator.Columns[14].Caption = "Нөхөн төлбөрийн төлөв";

        //    gvwOperator.Columns[1].ImageIndex = 0;
        //    gvwOperator.Columns[5].ImageIndex = 0;
        //    gvwOperator.Columns[11].ImageIndex = 0;

        //    gvwOperator.Columns[0].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[1].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[2].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[3].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[4].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[5].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[6].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[7].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[8].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[9].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[10].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[11].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[12].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[13].OptionsColumn.AllowEdit = false;
        //    gvwOperator.Columns[14].OptionsColumn.AllowEdit = false;
        //}
        //#endregion
        //private void ColorValues()
        //{
        //    ERowcolor1 = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\RowColor", "EventRow1", "Color [Azure]"));
        //    ERowcolor2 = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\RowColor", "EventRow2", "Color [Azure]"));
        //    SRowcolor1 = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\RowColor", "SelectedRow1", "Color [YellowGreen]"));
        //    SRowcolor2 = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\RowColor", "SelectedRow2", "Color [OliveDrab]"));
        //    ORowcolor1 = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\RowColor", "OddRow1", "Color [PapayaWhip]"));
        //    ORowcolor2 = Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\RowColor", "OddRow2", "Color [PapayaWhip]"));
        //    ERowcolor1 = ERowcolor1.Substring(7, ERowcolor1.LastIndexOf("]") - 7);
        //    ERowcolor2 = ERowcolor2.Substring(7, ERowcolor2.LastIndexOf("]") - 7);
        //    SRowcolor1 = SRowcolor1.Substring(7, SRowcolor1.LastIndexOf("]") - 7);
        //    SRowcolor2 = SRowcolor2.Substring(7, SRowcolor2.LastIndexOf("]") - 7);
        //    ORowcolor1 = ORowcolor1.Substring(7, ORowcolor1.LastIndexOf("]") - 7);
        //    ORowcolor2 = ORowcolor2.Substring(7, ORowcolor2.LastIndexOf("]") - 7);
        //    gvwNewDeal.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwNewDeal.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwNewDeal.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwNewDeal.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwNewDeal.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwNewDeal.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwOldContract.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwOldContract.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwOldContract.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwOldContract.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwOldContract.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwOldContract.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwClaim.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwClaim.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwClaim.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwClaim.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwClaim.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwClaim.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwReInsurance.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwReInsurance.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwReInsurance.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwReInsurance.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwReInsurance.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwReInsurance.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwOldContract.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwOldContract.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwOldContract.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwOldContract.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwOldContract.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwOldContract.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwIssue.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwIssue.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwIssue.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwIssue.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwIssue.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwIssue.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwNewContract.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwNewContract.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwNewContract.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwNewContract.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwNewContract.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwNewContract.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwDealPay.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwDealPay.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwDealPay.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwDealPay.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwDealPay.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwDealPay.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwMoveReDeal.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwMoveReDeal.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwMoveReDeal.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwMoveReDeal.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwMoveReDeal.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwMoveReDeal.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwContactStep.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwContactStep.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwContactStep.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwContactStep.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwContactStep.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwContactStep.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwProject.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwProject.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwProject.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwProject.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwProject.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwProject.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //    gvwOperator.Appearance.EvenRow.BackColor = Color.FromName(ERowcolor1);
        //    gvwOperator.Appearance.EvenRow.BackColor2 = Color.FromName(ERowcolor2);
        //    gvwOperator.Appearance.FocusedRow.BackColor = Color.FromName(SRowcolor1);
        //    gvwOperator.Appearance.FocusedRow.BackColor2 = Color.FromName(SRowcolor2);
        //    gvwOperator.Appearance.OddRow.BackColor = Color.FromName(ORowcolor1);
        //    gvwOperator.Appearance.OddRow.BackColor2 = Color.FromName(ORowcolor2);
        //}
        //private void PanelVisible()
        //{
        //    #region[Жагсаалт]
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "NewDeal", "Visible")) == "Visible")
        //    {
        //        pnlNewDeal.Visibility = DockVisibility.Visible;
        //        if (_deal == false) { _deal = true; RefreshNewDeal(); }
        //    }
        //    else { pnlNewDeal.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "OldContract", "Visible")) == "Visible")
        //    {
        //        pnlcontract.Visibility = DockVisibility.Visible;
        //        if (_oldcontract == false) { _oldcontract = true; RefreshOldContract(); }
        //    }
        //    else { pnlcontract.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "ReInsurance", "Visible")) == "Visible")
        //    {
        //        pnlreinsurance.Visibility = DockVisibility.Visible;
        //        if (_reinsurance == false) { _reinsurance = true; RefreshReInsurance(); }
        //    }
        //    else { pnlreinsurance.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "TopClaim", "Visible")) == "Visible")
        //    {
        //        pnlTopClaim.Visibility = DockVisibility.Visible;
        //        if (_claim == false) { _claim = true; RefreshClaim(); }
        //    }
        //    else { pnlTopClaim.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "Issue", "Visible")) == "Visible")
        //    {
        //        pnlIssue.Visibility = DockVisibility.Visible;
        //        if (_issue == false) { _issue = true; RefreshIssue(); }
        //    }
        //    else { pnlIssue.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "NewContract", "Visible")) == "Visible")
        //    {
        //        pnlNewContract.Visibility = DockVisibility.Visible;
        //        if (_newcontract == false) { _newcontract = true; RefreshNewContract(); }
        //    }
        //    else { pnlNewContract.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "DealPay", "Visible")) == "Visible")
        //    {
        //        pnlDealPay.Visibility = DockVisibility.Visible;
        //        if (_dealpay == false) { _dealpay = true; RefreshDealPay(); }
        //    }
        //    else { pnlDealPay.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "MoveReDeal", "Visible")) == "Visible")
        //    {
        //        pnlMoveReDeal.Visibility = DockVisibility.Visible;
        //        if (_moveredeal == false) { _moveredeal = true; RefreshMoveReDeal(); }
        //    }
        //    else { pnlMoveReDeal.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "ContactStep", "Visible")) == "Visible")
        //    {
        //        pnlContactStep.Visibility = DockVisibility.Visible;
        //        if (_contactstep == false) { _contactstep = true; RefreshContactStep(); }
        //    }
        //    else { pnlContactStep.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "Project", "Visible")) == "Visible")
        //    {
        //        pnlProject.Visibility = DockVisibility.Visible;
        //        if (_project == false) { _contactstep = true; RefreshProject(); }
        //    }
        //    else { pnlProject.Visibility = DockVisibility.Hidden; }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "Operator", "Visible")) == "Visible")
        //    {
        //        pnlOperator.Visibility = DockVisibility.Visible;
        //        if (_project == false) { _contactstep = true; RefreshOperator(); }
        //    }
        //    else { pnlOperator.Visibility = DockVisibility.Hidden; }
        //    #endregion
        //    #region[График]
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "Final Statements", "Hidden")) == "Visible")
        //    {
        //        pnlFinal.Visibility = DockVisibility.Visible;
        //        RefreshFinal();
        //    }
        //    else
        //    {
        //        pnlFinal.Visibility = DockVisibility.Hidden;
        //    }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "Final Statements1", "Hidden")) == "Visible")
        //    {
        //        pnlFinal1.Visibility = DockVisibility.Visible;
        //        RefreshFinal1();
        //    }
        //    else
        //    {
        //        pnlFinal1.Visibility = DockVisibility.Hidden;
        //    }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "Deal Report", "Hidden")) == "Visible")
        //    {
        //        pnlDealRep.Visibility = DockVisibility.Visible;
        //        if (_dealrep == false)
        //        {
        //            _dealrep = true;
        //            RefreshDealRep();
        //        }
        //    }
        //    else
        //    {
        //        pnlDealRep.Visibility = DockVisibility.Hidden;
        //    }
        //    if (Static.ToStr(Static.RegisterGet(_core.RegPath, "Dashboard\\Visible", "Claim Report", "Hidden")) == "Visible")
        //    {
        //        pnlClaimRep.Visibility = DockVisibility.Visible;
        //        if (_claimrep == false)
        //        {
        //            _claimrep = true;
        //            RefreshClaimRep();
        //        }
        //    }
        //    else
        //    {
        //        pnlClaimRep.Visibility = DockVisibility.Hidden;
        //    }
        //    #endregion
        //}
        //#endregion
        #region[BTN]
        #region[BTN-Refresh]
        private void btnDealRef_Click(object sender, EventArgs e)
        {
            //RefreshNewDeal();
        }
        private void btnClaimRef_Click(object sender, EventArgs e)
        {
            //RefreshClaim();
        }
        private void btnContRef_Click(object sender, EventArgs e)
        {
            //RefreshOldContract();
        }
        private void btnReInsRef_Click(object sender, EventArgs e)
        {
            //RefreshReInsurance();
        }
        private void btnFinalRef_Click(object sender, EventArgs e)
        {
            //RefreshFinal();
        }
        private void btnFinal1Ref_Click(object sender, EventArgs e)
        {
            //RefreshFinal1();
        }
        private void btnDealRepRef_Click(object sender, EventArgs e)
        {
            //RefreshDealRep();
        }
        private void btnClaimRepRef_Click(object sender, EventArgs e)
        {
            //RefreshClaimRep();
        }
        private void btnIssueRef_Click(object sender, EventArgs e)
        {
            //RefreshIssue();
        }
        private void btnContractRef_Click(object sender, EventArgs e)
        {
            //RefreshNewContract();
        }
        private void btnDealPayRef_Click(object sender, EventArgs e)
        {
            //RefreshDealPay();
        }
        private void btnMoveReDealRef_Click(object sender, EventArgs e)
        {
            //RefreshMoveReDeal();
        }
        private void btnContactStepRef_Click(object sender, EventArgs e)
        {
            //RefreshContactStep();
        }
        private void btnProjectRef_Click(object sender, EventArgs e)
        {
            //RefreshProject();
        }
        private void btnOperatorRef_Click(object sender, EventArgs e)
        {
            //RefreshOperator();
        }
        #endregion
        #region[BTN-Settings]
        private void btnReInsEdt_Click(object sender, EventArgs e)
        {
            //frmReInsuraceSet frm = new frmReInsuraceSet(_core);
            //frm.ShowDialog();
            //RefreshReInsurance();
        }
        private void btnOldContSet_Click(object sender, EventArgs e)
        {
            //frmOldContact frm = new frmOldContact(_core);
            //frm.ShowDialog();
            //RefreshOldContract();
        }
        private void btnClaimEdt_Click(object sender, EventArgs e)
        {
            //frmClaimSet frm = new frmClaimSet(_core);
            //frm.ShowDialog();
            //RefreshClaim();
        }
        private void btnDealEdt_Click(object sender, EventArgs e)
        {
            //frmNewDeal frm = new frmNewDeal(_core);
            //frm.ShowDialog();
            //RefreshNewDeal();
        }
        private void btnFinalEdt_Click(object sender, EventArgs e)
        {
            //frmFinal frm = new frmFinal(_core);
            //frm.ShowDialog();
            //RefreshFinal();
        }
        private void btnFinal1Edt_Click(object sender, EventArgs e)
        {
            //frmFinal1 frm = new frmFinal1(_core);
            //frm.ShowDialog();
            //RefreshFinal1();
        }
        private void btnClaimRepEdt_Click(object sender, EventArgs e)
        {
            //frmClaimRepSet frm = new frmClaimRepSet(_core);
            //frm.ShowDialog();
            //RefreshClaimRep();
        }
        private void btnDealRepEdt_Click(object sender, EventArgs e)
        {
            //frmDealRepEdt frm = new frmDealRepEdt(_core);
            //frm.ShowDialog();
            //RefreshDealRep();
        }
        private void btnIssueEdt_Click(object sender, EventArgs e)
        {
            //frmIssue frm = new frmIssue(_core);
            //frm.ShowDialog();
            //RefreshIssue();
        }
        private void btnContractEdt_Click(object sender, EventArgs e)
        {
            //frmNewContract frm = new frmNewContract(_core);
            //frm.ShowDialog();
            //RefreshNewContract();
        }
        private void btnDealPayEdt_Click(object sender, EventArgs e)
        {
            //frmDealPay frm = new frmDealPay(_core);
            //frm.ShowDialog();
            //RefreshDealPay();
        }
        private void btnMoveReDealEdt_Click(object sender, EventArgs e)
        {
            //frmMoveReDeal frm = new frmMoveReDeal(_core);
            //frm.ShowDialog();
            //RefreshMoveReDeal();
        }
        private void btnContactStepEdt_Click(object sender, EventArgs e)
        {
            //frmContactStep frm = new frmContactStep(_core);
            //frm.ShowDialog();
            //RefreshContactStep();
        }
        private void btnProjectEdt_Click(object sender, EventArgs e)
        {
            //frmProject frm = new frmProject(_core);
            //frm.ShowDialog();
            //RefreshProject();
        }
        private void btnOperatorEdt_Click(object sender, EventArgs e)
        {
            //frmOperator frm = new frmOperator(_core);
            //frm.ShowDialog();
            //RefreshOperator();
        }
        #endregion
        #region[BTN-PicSave]
        private void btnPicSave_Click(object sender, EventArgs e)
        {
            if (picMain.Image != null)
            {
                SaveFileDialog sDlg = new SaveFileDialog();
                sDlg.RestoreDirectory = true;
                sDlg.InitialDirectory = "C:\\";
                sDlg.FilterIndex = 1;
                sDlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png |BMP Files (*.bmp)|*.bmp";
                if (DialogResult.OK == sDlg.ShowDialog())
                {
                    picMain.Image.Save(sDlg.FileName);
                }
            }
            else
            {
                MessageBox.Show("Зураг оруулаагүй байна.");
            }
        }
        private void btnDealRepSave_Click(object sender, EventArgs e)
        {

            if (picDealReport.Image != null)
            {
                SaveFileDialog sDlg = new SaveFileDialog();
                sDlg.RestoreDirectory = true;
                sDlg.InitialDirectory = "C:\\";
                sDlg.FilterIndex = 1;
                sDlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png |BMP Files (*.bmp)|*.bmp";
                if (DialogResult.OK == sDlg.ShowDialog())
                {
                    picDealReport.Image.Save(sDlg.FileName);
                }
            }
            else
            {
                MessageBox.Show("Зураг оруулаагүй байна.");
            }
        }
        private void btnClaimRepSave_Click(object sender, EventArgs e)
        {

            if (picClaimReport.Image != null)
            {
                SaveFileDialog sDlg = new SaveFileDialog();
                sDlg.RestoreDirectory = true;
                sDlg.InitialDirectory = "C:\\";
                sDlg.FilterIndex = 1;
                sDlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png |BMP Files (*.bmp)|*.bmp";
                if (DialogResult.OK == sDlg.ShowDialog())
                {
                    picClaimReport.Image.Save(sDlg.FileName);
                }
            }
            else
            {
                MessageBox.Show("Зураг оруулаагүй байна.");
            }
        }
        #endregion
        private void ажлынСамбарынТохиргооToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmDashBSettings frm = new frmDashBSettings(_core, ERowcolor1, ERowcolor2, SRowcolor1, SRowcolor2, ORowcolor1, ORowcolor2);
            //frm.ShowDialog();
            //ColorValues();
            //PanelVisible();
        }
        #endregion
        #region[Panel Closed Save Register]
        private void pnlNewDeal_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "NewDeal", "Hidden");
            _core.CacheSet(this.Name + "_NewDeal", "Hidden");
            _core.CacheSave();
        }
        private void pnlcontract_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_OldContract", "Hidden");
            _core.CacheSave();
        }
        private void pnlreinsurance_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_ReInsurance", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "ReInsurance", "Hidden");
        }
        private void pnlTopClaim_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_TopClaim", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "TopClaim", "Hidden");
        }
        private void pnlFinal_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_FinalStatements", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Final Statements", "Hidden");
        }
        private void pnlFinal1_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_FinalStatements1", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Final Statements1", "Hidden");
        }
        private void pnlDealRep_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_DealReport", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Deal Report", "Hidden");
        }
        private void pnlClaimRep_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_ClaimReport", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Claim Report", "Hidden");
        }
        private void pnlIssue_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_Issue", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Issue", "Hidden");
        }
        private void pnlNewContract_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_NewContract", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "NewContract", "Hidden");
        }
        private void pnlMoveReDeal_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_MoveReDeal", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "MoveReDeal", "Hidden");
        }
        private void pnlDealPay_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_DealPay", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "DealPay", "Hidden");
        }
        private void pnlContactStep_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_ContractStep", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "ContactStep", "Hidden");
        }
        private void pnlProject_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_Project", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Project", "Hidden");
        }
        private void pnlOperator_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            _core.CacheSet(this.Name + "_Operator", "Hidden");
            _core.CacheSave();
            //Static.RegisterSave(_core.RegPath, "Dashboard\\Visible", "Operator", "Hidden");
        }
        #endregion
        #region[PicZoom]
        private void zoomTrackBarControl2_EditValueChanged(object sender, EventArgs e)
        {
            int Value = Static.ToInt(trkDealZoom.EditValue);
            if (Value > 0)
            {
                srcDealReport.AutoScrollMinSize = new Size(0, 0);
                srcDealReport.AutoScrollMinSize = new Size(picDealReport.Width + Static.ToInt(trkDealZoom.EditValue) * 10, (picDealReport.Height + Static.ToInt(trkDealZoom.EditValue) * 10));
            }
            else
            {
                srcDealReport.AutoScrollMinSize = new Size(0, 0);
            }
        }
        private void trkZoom_EditValueChanged(object sender, EventArgs e)
        {
            int Value = Static.ToInt(trkZoom.EditValue);
            if (Value > 0)
            {
                scrZoom.AutoScrollMinSize = new Size(0, 0);
                scrZoom.AutoScrollMinSize = new Size(picMain.Width + Static.ToInt(trkZoom.EditValue) * 100, (picMain.Height + Static.ToInt(trkZoom.EditValue) * 100));
            }
            else
            {
                scrZoom.AutoScrollMinSize = new Size(0, 0);
            }
        }
        private void trkClaimZoom_EditValueChanged(object sender, EventArgs e)
        {
            int Value = Static.ToInt(trkClaimZoom.EditValue);
            if (Value > 0)
            {
                srcClaimZoom.AutoScrollMinSize = new Size(0, 0);
                srcClaimZoom.AutoScrollMinSize = new Size(picClaimReport.Width + Static.ToInt(trkClaimZoom.EditValue) * 10, (picClaimReport.Height + Static.ToInt(trkClaimZoom.EditValue) * 10));
            }
            else
            {
                scrZoom.AutoScrollMinSize = new Size(0, 0);
            }
        }
        private void dockManager1_ActiveChildChanged(object sender, DockPanelEventArgs e)
        {
            e.Panel.Tabbed = false;
        }
        private void zoomTrackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            int Value = Static.ToInt(zoomTrackBarControl1.EditValue);
            if (Value > 0)
            {
                scrZoomFinal1.AutoScrollMinSize = new Size(0, 0);
                scrZoomFinal1.AutoScrollMinSize = new Size(picFinal1.Width + Static.ToInt(zoomTrackBarControl1.EditValue) * 100, (picFinal1.Height + Static.ToInt(zoomTrackBarControl1.EditValue) * 100));
            }
            else
            {
                scrZoomFinal1.AutoScrollMinSize = new Size(0, 0);
            }
        }
        #endregion
    }
}