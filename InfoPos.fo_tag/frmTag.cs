using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

using EServ.Shared;
using ISM.Touch;
using InfoPos.Core;

namespace infopos.fo_tag
{
    public partial class frmTag : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        #region Internal variables

        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;

        frmTagReader _frmTag = null;

        #endregion
        #region Constructor and events

        public frmTag()
        {
            InitializeComponent();
        }
        private void frmTag_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Menu functions

        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;
                _kb = new TouchKeyboard();
                _kb.Enable = _core.IsTouch;
                
                this.MdiParent = parent;
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            try
            {
                switch (buttonkey)
                {
                    case "tag_clear":
                        SubMenu_Clear();
                        break;
                    case "tag_write":
                        SubMenu_Write();
                        break;
                    case "tag_recycle":
                        SubMenu_Recycle();
                        break;
                    case "tag_exit":
                        this.Close();
                        item.IsClose = 1;
                        break;

                    case "call_tagreader":
                        string tagno = item.Text;
                        TagEventData tagdata = (TagEventData)item.Tag;

                        //Alert(string.Format("Сериал: {0} Төлөв: {1}", tagdata.readtagno, tagdata.readstatus), "Таг уншигдлаа.");

                        if (_frmTag == null)
                        {
                            SubMenu_TagReader(tagdata);
                        }
                        else
                        {
                            _frmTag.EventOnCard(tagdata);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SubMenu_TagReader(TagEventData tagdata)
        {
            Result res = null;
            try
            {
                tagdata = _core.Tag.Reader_GetData(tagdata.readtagno);
                txtTagNo.EditValue = tagdata.readtagno;
                dteStart.EditValue = tagdata.readdate1;
                dteEnd.EditValue = tagdata.readdate2;

                object[] param = new object[] { tagdata.readtagno };
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 606, 606003, 606003, param);
                if (res != null && res.ResultNo == 0)
                {
                    if (res.AffectedRows > 0)
                    {
                        DataTable dt = res.Data.Tables[0];
                        txtCustNo.EditValue = dt.Rows[0]["CUSTNO"];
                        txtCustName.EditValue = dt.Rows[0]["CUSTNAME"];
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            Alert(res, "Таг унших");
        }
        public void SubMenu_Clear()
        {
            try
            {
                #region Таг уншигч залгагдсан эсэхийг шалгах

                if (!_core.Tag.IsOpenned)
                {
                    Alert(string.Format("Таг уншигч холбогдоогүй байна!")
                            , "Таг цэвэрлэх"
                            , 2);
                    return;
                }

                #endregion

                using (_frmTag = new frmTagReader(_core, ""))
                {
                    _frmTag.SetCaption(string.Format("Цэвэрлэх тагийг уншуулна уу."));
                    DialogResult dlg = _frmTag.ShowDialog();
                    if (dlg == System.Windows.Forms.DialogResult.Cancel) return;

                    string serialno = _frmTag.SerialNo;
                    bool success = _core.Tag.Reader_ClearData(serialno);
                    if (!success)
                    {
                        Alert(string.Format("Таг цэвэрлэхэд алдаа гарлаа, дахин уншуулна уу!\r\n{0}", _core.Tag.ErrorMessage)
                            , "Таг цэвэрлэх"
                            , 2);
                    }
                }
            }
            catch (Exception ex)
            { }
            _frmTag = null;
        }
        public void SubMenu_Write()
        {
            try
            {
                #region Таг уншигч залгагдсан эсэхийг шалгах

                if (!_core.Tag.IsOpenned)
                {
                    Alert(string.Format("Таг уншигч холбогдоогүй байна!")
                            , "Таг цэнэглэх"
                            , 2);
                    return;
                }

                #endregion

                using (_frmTag = new frmTagReader(_core, ""))
                {
                    _frmTag.SetCaption(string.Format("Бичих тагийг уншуулна уу."));
                    DialogResult dlg = _frmTag.ShowDialog();
                    if (dlg == System.Windows.Forms.DialogResult.Cancel) return;

                    string serialno = _frmTag.SerialNo;
                    bool success = _core.Tag.Reader_WriteData(serialno, Static.ToDateTime(dteStart.EditValue), Static.ToDateTime(dteEnd.EditValue));
                    if (!success)
                    {
                        Alert(string.Format("Таг цэнэглэхэд алдаа гарлаа, дахин уншуулна уу!\r\n{0}", _core.Tag.ErrorMessage)
                        , "Таг цэнэглэх"
                        , 2);
                    }
                }
            }
            catch (Exception ex)
            { }
            _frmTag = null;
        }
        public void SubMenu_Recycle()
        {
            txtCustNo.EditValue = null;
            txtCustName.EditValue = null;
            txtTagNo.EditValue = null;
            dteStart.EditValue = null;
            dteEnd.EditValue = null;
        }

        #endregion

        #region Functions

        public void Alert(Result res, string caption)
        {
            if (res != null && res.ResultNo != 0)
            {
                if (_core != null)
                {
                    _core.AlertShow(caption, res.ResultDesc, 2);
                }
            }
        }
        public void Alert(string text, string caption, int image=0)
        {
            if (_core != null)
            {
                _core.AlertShow(caption, text, image);
            }
        }
        #endregion

        private void btnSysTime1_Click(object sender, EventArgs e)
        {
            dteStart.EditValue = DateTime.Now;
        }

        private void btnSysTime2_Click(object sender, EventArgs e)
        {
            dteEnd.EditValue = DateTime.Now;
        }
    }
}
