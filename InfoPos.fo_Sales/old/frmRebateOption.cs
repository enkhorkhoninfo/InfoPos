using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;


using EServ.Shared;
namespace InfoPos.fo_cash
{
    public partial class frmRebateOption : DevExpress.XtraEditors.XtraForm
    {
        private string _rebateindex = "";
        public string rebateindex
        {
            get { return _rebateindex; }
        }
        bool isselected = false;
        /// <summary>
        /// Хөнгөлөлт сонгох
        /// </summary>
        /// <param name="param">SALESDB,LOYALDB,POINT,REBATEID,LOYALID,POINTID</param>
        /// <param name="rebateID">Хөнгөлөлтийн дугаар:Cонголт индекс [OUTPUT]</param>
        public frmRebateOption(object[] param)
        {
            InitializeComponent();
            
            string productname = "";
            int pindex = 0;
            DevExpress.XtraEditors.Controls.RadioGroupItem rgi=new DevExpress.XtraEditors.Controls.RadioGroupItem();
            foreach (object[] obj in param)
            {
                decimal SALESPRICE = 0;
                decimal TOTALPRICE = 0;           
                
                DataTable dtSalesDB=(DataTable)obj[0];
                DataTable dt=(DataTable)obj[1];
                foreach(DataRow dr in dtSalesDB.Rows)
                {
                    TOTALPRICE = TOTALPRICE + Static.ToDecimal(dr["PRICE"]) * Static.ToDecimal(dr["QUANTITY"]);
                    SALESPRICE = SALESPRICE + Static.ToDecimal(dr["SALESPRICE"]) * Static.ToDecimal(dr["QUANTITY"]);
                }
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            productname = productname + "," + dr["PRODNO"];
                        }
                    }
                    else
                    {
                        productname = "Байхгүй";
                    }
                }
                else
                {
                    productname = "Байхгүй";
                }
                rgi.Description = string.Format("{0} [Хөн.Дүн: {1}][Урам.Бар: {2}][Оноо: {3}]", obj[3], TOTALPRICE - SALESPRICE, productname, obj[2]);
                //rgi.Value = string.Format("{0}:{1}", 1, pindex);
                rgi.Value = string.Format("{0}", pindex);
                radioGroup1.Properties.Items.Add(rgi);
                pindex += 1;
            }
            radioGroup1.SelectedIndex = 0;
        }


        private void frmRebateOption_Load(object sender, EventArgs e)
        {

        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _rebateindex = Static.ToStr(radioGroup1.EditValue);
            isselected = true;
        }
    }
}