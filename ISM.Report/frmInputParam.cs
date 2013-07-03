using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISM.Report
{
    public partial class frmInputParam : Form
    {
        List<ParameterItem> parameters = null;
        EServ.Client client = null;
        int userno = 0;
        int privno = 0;

        #region Constractor
        public frmInputParam(EServ.Client client, int userno, int privno, List<ParameterItem> parameters)
        {
            InitializeComponent();
            this.client = client;
            this.userno = userno;
            this.privno = privno;
            this.parameters = parameters;
        }
        #endregion
        #region Control Events
        private void frmInputParam_Load(object sender, EventArgs e)
        {
            

            foreach (ParameterItem pi in parameters)
            {
                ISM.Template.DynamicParameterItem dpi = ucParameterPanel1.ItemAdd(pi.Id, pi.Name, pi.Name, ""
                    , string.IsNullOrEmpty(pi.DictId) ? GetItemType(pi.ValueType) : Template.DynamicParameterType.List
                    , pi.ValueLength, ISM.Lib.Static.ToStr(pi.Value), pi.Mandatory, pi.EditMask, pi.Desc
                    , pi.DictId, false, pi.DictValueField, pi.DictNameField, 0);
            }
            ISM.Template.DictUtility.PrivNo = privno;
            ucParameterPanel1.ItemListRefresh();
            ucParameterPanel1.ItemListRefreshDefaultValues();
            ucParameterPanel1.ItemSetListFromDictionary(client, userno);

         
        }
        private void frmInputParam_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK) return;
            try
            {
                foreach (ParameterItem pi in parameters)
                {
                    object value = ucParameterPanel1.ItemGetValue(pi.Id);
                    if (value != null) pi.Value = value;
                }
            }
            catch
            {
            }
        }
        private void btnView_Click(object sender, EventArgs e)
        {

            
            List<ISM.Template.DynamicParameterItem> list = ucParameterPanel1.ItemCheckMandatory();
           
            if (list.Count > 0)
            {  
                int i=1;
                StringBuilder sb = new StringBuilder();
               
                foreach (ISM.Template.DynamicParameterItem pi in list)
                {
                  //  object value = ucParameterPanel1.ItemGetValue(pi.Id);

                    if (sb.Length > 0) sb.AppendLine();
                    
                        sb.AppendFormat("{0}. {1}", i++, pi.Name);
                    
                    MessageBox.Show(string.Format("Утга оруулна уу.\r\n{0}", sb.ToString()));
                    return;
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
           this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion
        #region Internal Methods
        private Template.DynamicParameterType GetItemType(Type type)
        {
            Template.DynamicParameterType dtype = Template.DynamicParameterType.Text;
            switch (type.Name)
            {
                case "String":
                    dtype = Template.DynamicParameterType.Text;
                    break;
                case "DateTime":
                    dtype = Template.DynamicParameterType.DateTime;
                    break;
                default:
                    dtype = Template.DynamicParameterType.Decimal;
                    break;
            }
            return dtype;
        }
        #endregion

        private void frmInputParam_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.F12)
            {
                List<ISM.Template.DynamicParameterItem> list = ucParameterPanel1.ItemCheckMandatory();

                if (list.Count > 0)
                {
                    int i = 1;
                    StringBuilder sb = new StringBuilder();

                    foreach (ISM.Template.DynamicParameterItem pi in list)
                    {
                        //  object value = ucParameterPanel1.ItemGetValue(pi.Id);

                        if (sb.Length > 0) sb.AppendLine();

                        sb.AppendFormat("{0}. {1}", i++, pi.Name);

                        MessageBox.Show(string.Format("Утга оруулна уу.\r\n{0}", sb.ToString()));
                        return;
                    }
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

            }


        }

        private void frmInputParam_Click(object sender, EventArgs e)
        {   


            //HeavenPro.Core.Core core = new HeavenPro.Core.Core();
            //frmInputParam.ActiveForm.MdiParent = core.MainForm;
            //frmInputParam.ActiveForm.MdiParent.Show();
            //core.MainForm.MdiParent = this;
            //this.MdiParent = core.MainForm;
          //  core.MainForm.Show();


      //      this.Show();


        }

    }
}
