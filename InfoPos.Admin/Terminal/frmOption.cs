using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.Admin
{
    public partial class frmOption : Form
    {
        private readonly InfoPos.Core.Core _core;
        public frmOption(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            xtraTabPage1.Text = "Ерөнхий тохиргоо";
            xtraTabPage2.Text = "Хавтасны тохиргоо";
            if (_core.Resource != null)
            {
                btnCancel.Image = _core.Resource.GetImage("navigate_home");
                btnConnect.Image = _core.Resource.GetImage("navigate_save");
            }

        }
        private void frmOption_Load(object sender, EventArgs e)
        {
            this.Icon = _core.icon;
            string mstrRegPath = ISM.CUser.Remote.mstrRegPath;
            string DefaultPathIn = _core.ApplicationPath;
            string DefaultPathOut = _core.ApplicationPath;
            string TerminalSkin = _core.TerminalSkin;
            string WindowType = _core.WindowType;


            txtLockTimeOut.EditValue = _core.CacheGet("frmOption_LockTimeOut", 60);
            txtCheckInterval.EditValue = _core.CacheGet("frmOption_CheckInterVal", 60);
            txtWaitTimeout.EditValue = _core.CacheGet("frmOption_WaitTimeout", 60);
            txtTempPath.EditValue = _core.CacheGet("frmOption_TempPath", "");
            txtReportPathIn.EditValue = _core.CacheGet("frmOption_ReportPathIn", "");
            txtReportPathOut.EditValue = _core.CacheGet("frmOption_ReportPathOut", "");
            txtDynamicPathIn.EditValue = _core.CacheGet("frmOption_DynamicPathIn", "");
            txtDynamicPathOut.EditValue = _core.CacheGet("frmOption_DynamicPathOut", "");
            txtSlipsPathIn.EditValue = _core.CacheGet("frmOption_SlipsPathIn", "");
            txtSlipsPathOut.EditValue = _core.CacheGet("frmOption_SlipsPathOut", "");
            txtCustReportPathIn.EditValue = _core.CacheGet("frmOption_CustReportPathIn", "");

            //txtWaitTimeout.EditValue = Static.ToStr(Static.RegisterGet(mstrRegPath, "Login", "WaitTimeout", "60"));
            //txtTempPath.EditValue = Static.ToStr(Static.RegisterGet(RegPath, "MainOptions", "TempPath", DefaultPathOut));
            //txtReportPathIn.EditValue = Static.ToStr(Static.RegisterGet(RegPath, "MainOptions", "ReportPathIn", DefaultPathIn + "\\Reports"));
            //txtReportPathOut.EditValue = Static.ToStr(Static.RegisterGet(RegPath, "MainOptions", "ReportPathOut", DefaultPathOut));
            //txtDynamicPathIn.EditValue = Static.ToStr(Static.RegisterGet(RegPath, "MainOptions", "DynamicPathIn", DefaultPathIn + "\\DynamicTemplates"));
            //txtDynamicPathOut.EditValue = Static.ToStr(Static.RegisterGet(RegPath, "MainOptions", "DynamicPathOut", DefaultPathOut));
            //txtSlipsPathIn.EditValue = Static.ToStr(Static.RegisterGet(RegPath, "MainOptions", "SlipsPathIn", DefaultPathIn + "\\Slips"));
            //txtSlipsPathOut.EditValue = Static.ToStr(Static.RegisterGet(RegPath, "MainOptions", "SlipsPathOut", DefaultPathOut));
           

            /*
            Caramel - Бор саарал - 0
            Black - Гүн хар - 1
            Blue - Гүн цэнхэр - 2
            DevExpress Style - Саарал - 3
            Money Twins - Цэнхэр - 4
            Lilian - Ягаан саарал - 5
            DevExpress Dark Style - Хар - 6
            */

            FormUtility.LookUpEdit_SetList(ref cboStyle, 0, "Бор саарал");
            FormUtility.LookUpEdit_SetList(ref cboStyle, 1, "Гүн хар");
            FormUtility.LookUpEdit_SetList(ref cboStyle, 2, "Гүн цэнхэр");
            FormUtility.LookUpEdit_SetList(ref cboStyle, 3, "Саарал");
            FormUtility.LookUpEdit_SetList(ref cboStyle, 4, "Цэнхэр");
            FormUtility.LookUpEdit_SetList(ref cboStyle, 5, "Ягаан саарал");
            FormUtility.LookUpEdit_SetList(ref cboStyle, 6, "Хар");
            //int i = Static.ToInt(Static.RegisterGet(RegPath, "MainOptions", "TerminalSkin", TerminalSkin));
            int i = _core.CacheGetInt("frmOption_TerminalSkin", 3);
            cboStyle.EditValue = i;
            defaultLookAndFeel1.LookAndFeel.SetSkinStyle(GetSkinName(i));
            FormUtility.LookUpEdit_SetList(ref cboWindowType, 0, "Child");
            FormUtility.LookUpEdit_SetList(ref cboWindowType, 1, "Tab");
            FormUtility.LookUpEdit_SetValue(ref cboWindowType, WindowType);
        }

        private void frmOption_Activated(object sender, EventArgs e)
        {
            txtLockTimeOut.Focus();
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();

            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtTempPath.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }
        }
        private void btnReportPathIn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();

            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtReportPathIn.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }
        }
        private void btnReportPathOut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();

            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtReportPathOut.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }
        }
        private void btnDynamicPathIn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();

            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtDynamicPathIn.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }
        }
        private void btnDynamicPathOut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();

            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtDynamicPathOut.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }
        }
        private void btnCustReport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();

            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtCustReportPathIn.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }
        }
        private void btnSlipsPathIn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();

            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtSlipsPathIn.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }
        }

        private void btnSlipsPathOut_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();

            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtSlipsPathOut.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }
        }

        private void frmOption_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            string mstrRegPath = ISM.CUser.Remote.mstrRegPath;


            //Static.RegisterSave(RegPath, "MainOptions", "LockTimeOut", txtLockTimeOut.Text);
            //Static.RegisterSave(mstrRegPath, "Login", "CheckInterval", txtCheckInterval.Text);
            //Static.RegisterSave(mstrRegPath, "Login", "WaitTimeout", txtWaitTimeout.Text);

            //Static.RegisterSave(RegPath, "MainOptions", "TempPath", txtTempPath.Text);
            //Static.RegisterSave(RegPath, "MainOptions", "ReportPathIn", txtReportPathIn.Text);
            //Static.RegisterSave(RegPath, "MainOptions", "ReportPathOut", txtReportPathOut.Text);
            //Static.RegisterSave(RegPath, "MainOptions", "DynamicPathIn", txtDynamicPathIn.Text);
            //Static.RegisterSave(RegPath, "MainOptions", "DynamicPathOut", txtDynamicPathOut.Text);
            //Static.RegisterSave(RegPath, "MainOptions", "SlipsPathIn", txtSlipsPathIn.Text);
            //Static.RegisterSave(RegPath, "MainOptions", "SlipsPathOut", txtSlipsPathOut.Text);

            //Static.RegisterSave(RegPath, "MainOptions", "TerminalSkin", Static.ToStr(cboStyle.EditValue));
            //Static.RegisterSave(RegPath, "MainOptions", "WindowType", Static.ToStr(cboWindowType.EditValue));

            _core.CacheSet(this.Name+"_LockTimeOut",txtLockTimeOut.EditValue);
            _core.CacheSet(this.Name+"_CheckInterval",txtCheckInterval.EditValue);
            _core.CacheSet(this.Name+"_WaitTimeout",txtWaitTimeout.EditValue);
            _core.CacheSet(this.Name+"_TempPath",txtTempPath.EditValue);
            _core.CacheSet(this.Name+"_ReportPathIn",txtReportPathIn.EditValue);
            _core.CacheSet(this.Name+"_ReportPathOut",txtReportPathOut.EditValue);
            _core.CacheSet(this.Name + "_CustReportPathIn", txtCustReportPathIn.EditValue);
            _core.CacheSet(this.Name + "_DynamicPathIn", txtSlipsPathIn.EditValue);
            _core.CacheSet(this.Name + "_DynamicPathOut", txtSlipsPathOut.EditValue);
            _core.CacheSet(this.Name+"_SlipsPathIn",txtSlipsPathIn.EditValue);
            _core.CacheSet(this.Name+"_SlipsPathOut",txtSlipsPathOut.EditValue);
            _core.CacheSet(this.Name+"_TerminalSkin",cboStyle.EditValue);
            _core.CacheSet(this.Name + "_WindowType", cboWindowType.EditValue);
            _core.RemoteObject.CheckInterval = Static.ToInt(txtCheckInterval.Text);
            _core.RemoteObject.WaitTimeout = Static.ToInt(txtWaitTimeout.Text);
            _core.CacheSave();
            _core.TempPath = txtTempPath.Text;
            _core.ReportPathIn = txtReportPathIn.Text;
            _core.ReportPathOut = txtReportPathOut.Text;
            _core.DynamicPathIn = txtDynamicPathIn.Text;
            _core.DynamicPathOut = txtDynamicPathOut.Text;
            _core.SlipsPathIn = txtSlipsPathIn.Text;
            _core.CustReportPathIn = txtCustReportPathIn.Text;
            _core.SlipsPathOut = txtSlipsPathOut.Text;
            _core.TerminalSkin = Static.ToStr(cboStyle.EditValue);
            _core.WindowType = Static.ToStr(cboWindowType.EditValue);

            defaultLookAndFeel1.LookAndFeel.SetSkinStyle(GetSkinName(Static.ToInt(cboStyle.EditValue)));

            this.Close();
        }
        private string GetSkinName(int SkinNumber)
        {
            /*
Caramel - Бор саарал - 0
Black - Гүн хар - 1
Blue - Гүн цэнхэр - 2
DevExpress Style - Саарал - 3
Money Twins - Цэнхэр - 4
Lilian - Ягаан саарал - 5
DevExpress Dark Style - Хар - 6
*/
            string strstyle = "Caramel";

            switch (Static.ToInt(cboStyle.EditValue))
            {
                case 0: strstyle = "Caramel"; break;
                case 1: strstyle = "Black"; break;
                case 2: strstyle = "Blue"; break;
                case 3: strstyle = "DevExpress Style"; break;
                case 4: strstyle = "Money Twins"; break;
                case 5: strstyle = "Lilian"; break;
                case 6: strstyle = "DevExpress Dark Style"; break;
            }
            return strstyle;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
