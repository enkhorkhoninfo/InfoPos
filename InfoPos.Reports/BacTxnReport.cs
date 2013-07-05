using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISM.Template;
using ISM.Lib;
//using ISM.CGenerator;
using ISM.Report;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace InfoPos.Reports
{
    public partial class BacTxnReport : Form
    {   
        Core.Core _core;
        #region [ Constructor Function ]
        public BacTxnReport(object[] obj)
        {
            
            InitializeComponent();
            _core = (Core.Core)obj[0];
            if (obj[1] != null)
            {
                txtReportPriv.Text = obj[1].ToString();
            }
            if (obj[2] != null)
            {
               txtReportName.Text = obj[2].ToString();
            }
        }
        #endregion
        #region [ Тайлан харах ]
        public EServ.Shared.Result ViewReport(Core.Core  pCore, int TxnCode, string Type)
        {
            EServ.Shared.Result res = new EServ.Shared.Result();
            try
            {
                ISM.Report.Generator gen = new ISM.Report.Generator(_core);
                gen.ReportPath = pCore.ReportPathIn;
                gen.ReportOutPath = pCore.ReportPathOut;
                gen.UserNo = pCore.RemoteObject.User.UserNo;
                gen.Client = pCore.RemoteObject.Connection;
                string error = "";
                string outfilename = "";
        
              
                int success = gen.GetReport( TxnCode,Type, ref outfilename, ref error);
            
                if (success != 0)
                {
                    MessageBox.Show(string.Format("Result = {0} Error = {1}", success, error));
                    return new EServ.Shared.Result(1, "");
                }
                if (!File.Exists(outfilename))
                {
                    MessageBox.Show(string.Format("Ini file not found.", outfilename));
                    return new EServ.Shared.Result(1, "");
                }
                ISM.Template.Globals.ShellOpenFile(outfilename);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message;
                return res;
            }
           

        }
        private void btnView1_Click(object sender, EventArgs e)
        {
        
                EServ.Shared.Result res = new EServ.Shared.Result();
         
           
                ISM.Report.Generator gen = new ISM.Report.Generator(_core);
                gen.ReportPath = _core.ReportPathIn;
                gen.ReportOutPath = _core.ReportPathOut;
                gen.UserNo = _core.RemoteObject.User.UserNo;
                gen.Client = _core.RemoteObject.Connection;

                //string suc = gen.DynamicRead("ini", txtReportPriv.Text);
                //if (suc != "") { MessageBox.Show(suc); return; }
                //string succ = gen.DynamicRead("rpt", txtReportPriv.Text);
                //if (succ != "") { MessageBox.Show(succ); return; }   

                gen.ReportFormTitle = "Тайлангийн параметер";

                string error = "";
                string outfilename = "";
                int success = gen.GetReport(Convert.ToInt32(txtReportPriv.Text), cboOutType.Text, ref outfilename, ref error);

                    

              
                if (success != 0)
                {
                    if (success == 24) {
                    //    this.Close();
                   
                    
                    }
                    if (success == 30) {
                        MessageBox.Show("Report ini file not found.");
                    
                    }
                    if (success == 10) {
                        MessageBox.Show("Ini file not found.");
                    
                    }
                    if (success == 20)
                    {
                        MessageBox.Show("Empty report file name.");
                        //MessageBox.Show("wergt");

                    }
                    if (success == 21)
                    {
                        MessageBox.Show("Empty data source.");

                    }
                    if (success == 22)
                    {
                        MessageBox.Show("Not found specified report file");

                    }
                    if (success == 23)
                    {
                        MessageBox.Show("Export error.");

                    }
                    if (success == 29)
                    {
                        MessageBox.Show("Other error.");

                    }
           //    MessageBox.Show(string.Format("Result = {0} Error = {1}", success, error));
            //   return;
            }


                if (!File.Exists(outfilename))
                {
                    
                   // MessageBox.Show(string.Format("Output file does not exist.\r\n{0}", outfilename));


                    return
;
                }
                   
            
               
                ISM.Template.Globals.ShellOpenFile(outfilename);
        
            }
        //    catch (Exception ex) { this.Close(); }
        //}
   
         


        #endregion
        #region [ Button --- зураг оруулах ]
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void Function()
        {
            //if (_core.Resource != null)
            {
                //btnView1.Image = _core.Resource.GetImage("image_zoom");
                //simpleButton1.Image = _core.Resource.GetImage("navigate_cancel");
            }
        }
        private void BacTxnReport_Load(object sender, EventArgs e)
        {
            Function();
        }
        #endregion

        private void BacTxnReport_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
