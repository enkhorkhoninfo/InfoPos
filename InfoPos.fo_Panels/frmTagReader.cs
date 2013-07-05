using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Tag = Sit;
using System.IO.Ports;

namespace InfoPos.Panels
{
    public partial class frmTagReader : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;
        #region Public Properties

        //private string _custno = null;
        //public string CustNo
        //{
        //    get { return _custno; }
        //}
        private string _serialno = null;
        public string SerialNo
        {
            get { return _serialno; }
        }

        #endregion
        #region Constractors and Control Events
        public frmTagReader(InfoPos.Core.Core core, string caption = "")
        {
            InitializeComponent();
            //this.FormClosing += new FormClosingEventHandler(frmTagReader_FormClosing);
            lblCaption.Text = caption;
            _core = core;
        }
        private void frmTagReader_Load(object sender, EventArgs e)
        {
            try
            {
                //if (_core.Tag.tagreader != null)
                //{
                //    _core.Tag.tagreader.OnCardRead -= new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                //    _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                //    _core.Tag.tagreader.lastSelectedCardID = "";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnChoose_Click(object sender, EventArgs e)
        {
            // Таг уншигчийг ажилд оруулах, унших
            // _core.TagReader.Open();

            Choose(txtSerialNo.Text);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        #endregion
        #region Methods

        private DialogResult Choose(string value = null)
        {
            DialogResult res = System.Windows.Forms.DialogResult.None;

            if (string.IsNullOrEmpty(value))
            {
                ISM.Template.FormUtility.ValidateConfirm("Мэдээлэл хоосон байна!");
            }
            else
            {
                txtSerialNo.Text = value;
                _serialno = value; // тагаас уншсан утгаа энд хийнэ.                                              
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }

            return res;
        }

        #endregion
                
        public void EventOnCard(string tagno, ArrayList data)
        {
            try
            {
                Choose(tagno);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
    }
}