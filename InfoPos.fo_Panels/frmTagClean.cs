using System;
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
    public partial class frmTagClean : DevExpress.XtraEditors.XtraForm
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
        private int _type = 0;
        /// <summary>
        /// 0 - Таг цэвэрлэх
        /// 1 - Таг дээр бичих
        /// 2 - Тагаас мэдээлэл унших
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private DateTime _startdate;
        public DateTime StartDate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }

        private DateTime _enddate;
        public DateTime EndDate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }


        #endregion
        #region Constractors
        public frmTagClean(InfoPos.Core.Core core)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmTagReader_FormClosing);
            _core = core;
        }
        #endregion
        #region Control Events
        void frmTagReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void frmTagReader_Load(object sender, EventArgs e)
        {
            try
            {
                if (_core.Tag.tagreader != null)
                {
                    _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                    _core.Tag.tagreader.lastSelectedCardID = "";
                }
                else
                    MessageBox.Show("Таг уншигч холбогдоогүй байна.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void tagreader_OnCardRead(object sender, Tag.onCardEventArgs e)
        {
            try
            {
                _core.Tag.tagreader.OnCardRead -= new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                string cardid = e.cardID;
                if (!cardid.Equals(""))
                {
                    if (!cardid.Equals("47FF"))
                    {
                        txtSerialNo.EditValue = e.cardID;
                        //Таг цэвэрлэх
                        if (_type == 0)
                        {
                            if (_core.Tag.Reader_ClearData(e.cardID))
                            {
                                InfoPos.fo_panels.frmMessage frm = new fo_panels.frmMessage(InfoPos.fo_panels.frmMessage.enumMessage.CLEANCORRECT);
                                frm.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                InfoPos.fo_panels.frmMessage frm = new fo_panels.frmMessage(InfoPos.fo_panels.frmMessage.enumMessage.CLEANERROR);
                                frm.ShowDialog();
                                _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                                _core.Tag.tagreader.lastSelectedCardID = "";
                            }
                        }
                        else
                        {
                            //Таг дээр мэдээлэл бичих
                            if (_type == 1)
                            {
                                if (_core.Tag.Reader_WriteData(e.cardID, _startdate, _enddate))
                                {
                                    InfoPos.fo_panels.frmMessage frm = new fo_panels.frmMessage(InfoPos.fo_panels.frmMessage.enumMessage.WRITECORRECT);
                                    frm.ShowDialog();
                                    this.Close();
                                }
                                else
                                {
                                    InfoPos.fo_panels.frmMessage frm = new fo_panels.frmMessage(InfoPos.fo_panels.frmMessage.enumMessage.WRITEERROR);
                                    frm.ShowDialog();
                                }
                                _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                                _core.Tag.tagreader.lastSelectedCardID = "";
                            }
                        }
                    }
                    else
                    {
                        InfoPos.fo_panels.frmMessage frm = new fo_panels.frmMessage(InfoPos.fo_panels.frmMessage.enumMessage.REPEATREAD);
                        frm.ShowDialog();
                        _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                        _core.Tag.tagreader.lastSelectedCardID = "";
                    }
                }
                else
                {
                    InfoPos.fo_panels.frmMessage frm = new fo_panels.frmMessage(InfoPos.fo_panels.frmMessage.enumMessage.REPEATREAD);
                    frm.ShowDialog();
                    _core.Tag.tagreader.OnCardRead += new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
                    _core.Tag.tagreader.lastSelectedCardID = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmTagClean_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_core.Tag.tagreader != null)
                _core.Tag.tagreader.OnCardRead -= new Sit.Reader.CardEventHandler(tagreader_OnCardRead);
        }
    }
}