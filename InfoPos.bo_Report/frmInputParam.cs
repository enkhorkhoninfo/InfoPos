using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using EServ.Shared;
using System.Collections;

namespace InfoPos.bo_Reports
{
    public partial class frmInputParam : DevExpress.XtraEditors.XtraForm
    {
        Core.Core _core;
        string TemplatePath = "";
        Hashtable colindex = new Hashtable();
        #region[Properties]
        private DataTable _ReportData = null;
        public DataTable ReportData
        {
            get { return _ReportData; }
            set { _ReportData = value; }
        }
        #endregion
        public frmInputParam(Core.Core core,string templatepath)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnView.Image = _core.Resource.GetImage("object_search");
                btnExit.Image = _core.Resource.GetImage("image_exit");
            }
            TemplatePath = templatepath;
            RefreshParamData(TemplatePath);
        }

        private void frmInputParam_Load(object sender, EventArgs e)
        {

        }
        void RefreshParamData(string TemplatePath)
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 503, 249999, 249999, new object[] { TemplatePath, 0 });
                if (res.ResultNo == 0)
                {
                    colindex = (Hashtable)res.Param[0];
                    ucExcelReports.ItemRemoveAll();
                    foreach (DataRow row in res.Data.Tables[0].Rows)
                    {
                        switch (Static.ToInt(row["PARAMTYPE"]))
                        {
                            case 0:
                                ucExcelReports.ItemAdd(row["PARAMID"].ToString(), row["PARAMNAME"].ToString(), row["FIELDNAME"].ToString(), ISM.Template.DynamicParameterType.Decimal, 0, row["DEFAULTVALUE"],false, "", string.Format("{0}:{1}", row["PARAMDESC"], row["CONDITION"]));
                                break;
                            case 1:
                                ucExcelReports.ItemAdd(row["PARAMID"].ToString(), row["PARAMNAME"].ToString(), row["FIELDNAME"].ToString(), "", ISM.Template.DynamicParameterType.Text, 0, "", false, "", string.Format("{0}:{1}", row["PARAMDESC"], row["CONDITION"]), "", false, "", "", 0);
                                break;
                            case 2:
                                ucExcelReports.ItemAdd(row["PARAMID"].ToString(), row["PARAMNAME"].ToString(), row["FIELDNAME"].ToString(), ISM.Template.DynamicParameterType.Date, 0, row["DEFAULTVALUE"], false, "", string.Format("{0}:{1}", row["PARAMDESC"], row["CONDITION"]));
                                break;
                            case 3:
                                ucExcelReports.ItemAdd(row["PARAMID"].ToString(), row["PARAMNAME"].ToString(), row["FIELDNAME"].ToString(), ISM.Template.DynamicParameterType.DateTime, 0, row["DEFAULTVALUE"], false, "", string.Format("{0}:{1}", row["PARAMDESC"], row["CONDITION"]));
                                break;
                            case 4:
                                ucExcelReports.ItemAdd(row["PARAMID"].ToString(), row["PARAMNAME"].ToString(), row["FIELDNAME"].ToString(), "", ISM.Template.DynamicParameterType.List, 0, row["DEFAULTVALUE"], false, "", string.Format("{0}:{1}", row["PARAMDESC"], row["CONDITION"]), row["DICID"].ToString(), true, row["DICVALUEFIELD"].ToString(), row["DICNAMEFIELD"].ToString(), 0);
                                break;
                        }
                    }
                    ucExcelReports.ItemListRefresh();
                    if (_core != null)
                    {
                        ucExcelReports.ItemSetListFromDictionary(_core.RemoteObject);

                    }
                    ucExcelReports.ItemListRefreshValues();
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                object[] param = new object[ucExcelReports.ItemGetList().Count * 3];
                int index = 0;
                foreach (ISM.Template.DynamicParameterItem pi in ucExcelReports.ItemGetList())
                {
                    param[index] = pi.Name2;
                    index++;
                    string[] condition = pi.Description.Split(':');
                    param[index] = condition[1];
                    index++;
                    param[index] = pi.Value;
                    index++;
                }
                Result res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 503, 249999, 249999, new object[] { TemplatePath, 1, param, colindex });
                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    FileStream fs = null;
                    byte[] bytes = (byte[])res.Param[0];
                    string path = string.Format("{0}\\rep{1}.xlsm", _core.ReportPathOut, DateTime.Now.Ticks);
                    fs = File.Create(path);
                    fs.Write(bytes, 0, Static.ToInt(bytes.Length));
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                    System.Diagnostics.Process.Start(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}