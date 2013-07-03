using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO.Ports;

namespace InfoPos
{
    public partial class frmSettings : DevExpress.XtraEditors.XtraForm
    {
        //Core.Core _core;

        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Core.CacheSet("Connection_Ip", txtIp.EditValue);
                Program.Core.CacheSet("Connection_Port", numPort.EditValue);
                Program.Core.CacheSet("Connection_PosNo", numPosNo.EditValue);
                
                Program.Core.CacheSet("BillPrinterName", txtBillPrinterName.EditValue);
                Program.Core.CacheSet("BillPrinterPort", cboBillPrinterPort.EditValue);
                Program.Core.CacheSet("BillPrinterType", rdoBillPrinterType.EditValue);
                Program.Core.CacheSet("BillPrinterRate", numBillPrinterRate.EditValue);

                Program.Core.CacheSet("LiftPrinterName", txtLiftPrinterName.EditValue);
                Program.Core.CacheSet("LiftPrinterPort", cboLiftPrinterPort.EditValue);
                Program.Core.CacheSet("LiftPrinterType", rdoLiftPrinterType.EditValue);
                Program.Core.CacheSet("LiftPrinterRate", numLiftPrinterRate.EditValue);
                
                Program.Core.CacheSet("PriceBoardPort", cboPriceBoardPort.EditValue);
                
                
                Program.Core.CacheSave();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            try
            {
                cboBillPrinterPort.Properties.DataSource = SerialPort.GetPortNames();
                cboBillPrinterPort.Properties.ValueMember = "Column";

                cboLiftPrinterPort.Properties.DataSource = SerialPort.GetPortNames();
                cboLiftPrinterPort.Properties.ValueMember = "Column";

                cboPriceBoardPort.Properties.DataSource = SerialPort.GetPortNames();
                cboPriceBoardPort.Properties.ValueMember = "Column";

                txtIp.EditValue = Program.Core.CacheGetStr("Connection_Ip");
                numPort.EditValue = Program.Core.CacheGetInt("Connection_Port");
                numPosNo.EditValue = Program.Core.CacheGetStr("Connection_PosNo");

                txtBillPrinterName.EditValue = Program.Core.CacheGetStr("BillPrinterName");
                cboBillPrinterPort.EditValue = Program.Core.CacheGetStr("BillPrinterPort", "COM1");
                rdoBillPrinterType.EditValue = Program.Core.CacheGetInt("BillPrinterType", 0);
                numBillPrinterRate.EditValue = Program.Core.CacheGetInt("BillPrinterRate", 9600);

                txtLiftPrinterName.EditValue = Program.Core.CacheGetStr("LiftPrinterName");
                cboLiftPrinterPort.EditValue = Program.Core.CacheGetStr("LiftPrinterPort", "COM1");
                rdoLiftPrinterType.EditValue = Program.Core.CacheGetInt("LiftPrinterType", 0);
                numLiftPrinterRate.EditValue = Program.Core.CacheGetInt("LiftPrinterRate", 9600);

                cboPriceBoardPort.EditValue = Program.Core.CacheGetStr("PriceBoardPort", "COM2");

                if (Program.Core.Resource != null)
                {
                    btnSave.Image = Program.Core.Resource.GetImage("navigate_save");
                    btnCancel.Image = Program.Core.Resource.GetImage("image_exit");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnIPPOS_Click(object sender, EventArgs e)
        {
            GCIPPOSF.CTxn t = new GCIPPOSF.CTxn();
            t.InitAll();
            t.Settings();
        }

        private void btnBillChoose_Click(object sender, EventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBillPrinterName.EditValue = dlg.PrinterSettings.PrinterName;
            }
        }

        private void btnLiftChoose_Click(object sender, EventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtLiftPrinterName.EditValue = dlg.PrinterSettings.PrinterName;
            }
        }
    }
}
