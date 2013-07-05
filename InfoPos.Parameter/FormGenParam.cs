using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormGenParam : DevExpress.XtraEditors.XtraForm
    {
        Core.Core _core;
        int SelectTxnCode = 140146;
        int AddTxnCode = 140148;
        int EditTxnCode = 140149;
        int DeleteTxnCode = 140150;
        DataTable DT = new DataTable();
        bool ResCheck=false;

        public FormGenParam(Core.Core core)
        {
            InitializeComponent();
            _core = core;
        }

        private void FormGenParam_Load(object sender, EventArgs e)
        {
            RefreshData(SelectTxnCode);
            ucGenParam.Resource = _core.Resource;

            Form FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(_core.ApplicationName, ref FormName);
        }

        void RefreshData(int SelectTxnCode)
        {
            Result res = new Result();
            try
            {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, SelectTxnCode, SelectTxnCode,null);
                    if (res.ResultNo == 0)
                    {
                        ucGenParam.ItemRemoveAll();                       
                        foreach (DataRow row in res.Data.Tables[0].Rows) 
                        {
                            switch(Static.ToInt(row[6]))
                            {
                                case 0:
                                    ucGenParam.ItemAdd(row[0].ToString(), row[2].ToString(), row[3].ToString(), ISM.Template.DynamicParameterType.Decimal,0, 0, false, row[8].ToString(), row[4].ToString());
                                    break;
                                case 1:
                                    ucGenParam.ItemAdd(row[0].ToString(), row[2].ToString(), row[3].ToString(), ISM.Template.DynamicParameterType.Text, Static.ToInt(row[7]), 0, false, "", row[4].ToString());
                                    break;
                                case 2:
                                    ucGenParam.ItemAdd(row[0].ToString(), row[2].ToString(), row[3].ToString(), ISM.Template.DynamicParameterType.Date, false, "", row[4].ToString());
                                    break;
                            }
                        }
                        ucGenParam.ItemListRefresh();
                        if (_core != null)
                        {
                            ucGenParam.ItemSetListFromDictionary(_core.RemoteObject);
                            
                        }
                        ucGenParam.ItemListRefreshValues();

                        DT = res.Data.Tables[0];
                        ResCheck = true;
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
        
        void EditData(int EditTxnCode) 
        {
            if (ResCheck)
            {
                object[] value = ucGenParam.ItemGetValueList();
                for(int i=0;i<DT.Rows.Count;i++)
                {
                    DT.Rows[i][3] =value[i];
                }

                Result res = new Result();
                try
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, EditTxnCode, EditTxnCode, new object[]{DT});
                    if (res.ResultNo == 0)
                    {
                        RefreshData(SelectTxnCode);
                        MessageBox.Show("Амжилттай засварлалаа.");
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
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditData(EditTxnCode);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormGenParam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FormGenParam_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }

    }
}