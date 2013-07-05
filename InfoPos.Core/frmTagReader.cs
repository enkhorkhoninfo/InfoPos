using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Tag = Sit;
//using System.IO.Ports;

namespace InfoPos.Core
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

        TagEventData _tagdata = null;
        public TagEventData TagData
        {
            get { return _tagdata; }
        }

        #endregion
        #region Constractors and Control Events
        public frmTagReader(InfoPos.Core.Core core, string caption, bool showskip = false)
        {
            InitializeComponent();
            //this.FormClosing += new FormClosingEventHandler(frmTagReader_FormClosing);
            btnSkip.Visible = showskip;
            lblCaption.Text = caption;
            _core = core;

            this.KeyPreview = true;
            this.KeyDown += frmTagReader_KeyDown;
        }

        void frmTagReader_KeyDown(object sender, KeyEventArgs e)
        {
            this.Text = "Form key preview: " + txtSerialNo.IsEditorActive.ToString();
            if (!txtSerialNo.IsEditorActive)
            {
                txtSerialNo.SelectAll();
                txtSerialNo.SelectedText = new string((char)e.KeyValue, 1);
                txtSerialNo.Select();
            }
        }
        private void frmTagReader_Load(object sender, EventArgs e)
        {
            try
            {
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
        public void SetCaption(string caption, string tagno=null)
        {
            lblCaption.Text = caption;
            txtSerialNo.EditValue = tagno;
        }

        #endregion
                
        public void EventOnCard(TagEventData tagdata)
        {
            try
            {
                _tagdata = tagdata;
                if (tagdata != null) Choose(tagdata.readtagno);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.Close();
        }        
    }
}