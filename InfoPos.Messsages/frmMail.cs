using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

using EServ.Shared;
using ISM.Template;

namespace InfoPos.Messages
{
    public partial class frmMail : DevExpress.XtraEditors.XtraForm
    {
        #region[Varibles]
        Core.Core _core;
        bool page = false;
        bool load = false;
        bool loadsent = false;
        string appname = "", formname = "";
        Form FormName = null;

        #endregion
        #region[Байгуулагч функц]
        public frmMail(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnSend.Image = _core.Resource.GetImage("menu_newmail");
                btnReply.Image = _core.Resource.GetImage("image_replymail");
                btnForward.Image = _core.Resource.GetImage("image_forwardmail");
                btnDelete.Image = _core.Resource.GetImage("image_deletemail");
                btnRefresh.Image = _core.Resource.GetImage("navigate_refresh");
                imagecol.AddImage(_core.Resource.GetImage("dashboard_user"));
                imagecol.AddImage(_core.Resource.GetImage("dashboard_read"));
                imagecol.AddImage(_core.Resource.GetImage("dashboard_unread"));
                appname = _core.ApplicationName;
                formname = "Parameter." + this.Name;
                FormName = this;
                FormUtility.RestoreStateForm(appname, ref FormName);
            }
            RefreshInbox();
        }
        #endregion
        #region[Refresh]
        void RefreshInbox()
        {
            Result res = new Result();
            load = true;
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 226, 110120, 110120, new object[] { _core.RemoteObject.User.UserNo, 0 });
            if (res.ResultNo == 0)
            {
                grdInbox.DataSource = res.Data.Tables[0];
                SetInbox();
            }
            else
            {
                MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
            }
        }
        void RefreshSent()
        {
            loadsent = true;
            Result res = new Result();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 226, 110121, 110121, new object[] { _core.RemoteObject.User.UserNo, 1 });
            if (res.ResultNo == 0)
            {
                grdSent.DataSource = res.Data.Tables[0];
                SetSent();
            }
            else
            {
                MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
            }
        }
        #endregion
        #region[Focus]
        private void gvwSent_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwSent.GetFocusedDataRow();
            if (dr != null)
            {
                lblfrom.Text = Static.ToStr(dr["TOUSERNO"]);
                lblsent.Text = Static.ToStr(dr["POSTDATE"]);
                lblto.Text = Static.ToStr(dr["FROMUSERNO"]);
                mmoDescription.EditValue = Static.ToStr(dr["DESCRIPTION"]);
            }
        }
        private void gvwInbox_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gvwInbox.GetFocusedDataRow();
            if (dr != null)
            {
                lblfrom.Text = Static.ToStr(dr["FROMUSERNO"]);
                lblsent.Text = Static.ToStr(dr["POSTDATE"]);
                lblto.Text = Static.ToStr(dr["TOUSERNO"]);
                mmoDescription.EditValue = Static.ToStr(dr["DESCRIPTION"]);
            }
        }
        #endregion
        #region[Set]
        void SetInbox()
        {
            gvwInbox.Columns[0].Caption = "Төлөв";
            gvwInbox.Columns[0].ColumnEdit = imagecombo;
            gvwInbox.Columns[1].Caption = "Мессэжний дугаар";
            gvwInbox.Columns[1].Visible = false;
            gvwInbox.Columns[2].Caption = "Хүлээн авсан хэрэглэгч";
            gvwInbox.Columns[3].Caption = "Илгээсэн хэрэглэгч";
            gvwInbox.Columns[4].Caption = "Мэдээлэл";
            gvwInbox.Columns[5].Caption = "Програмын илгээсэн огноо";
            gvwInbox.Columns[5].Visible = false;
            gvwInbox.Columns[6].Caption = "Хүлээн авсан огноо";
            gvwInbox.Columns[8].Caption = "Илгээсэн хэрэглэгчийн дугаар";
            gvwInbox.Columns[8].Visible = false;
            gvwInbox.Columns[7].Caption = "Хүлээн авсан хэрэглэгчийн дугаар";
            gvwInbox.Columns[7].Visible = false;
            gvwInbox.Columns[6].GroupIndex = 0;
            gvwInbox.Columns[6].GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateRange;
            gvwInbox.Columns[6].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            gvwInbox.Columns[0].OptionsColumn.AllowEdit = false;
            gvwInbox.Columns[1].OptionsColumn.AllowEdit = false;
            gvwInbox.Columns[2].OptionsColumn.AllowEdit = false;
            gvwInbox.Columns[3].OptionsColumn.AllowEdit = false;
            gvwInbox.Columns[4].OptionsColumn.AllowEdit = false;
            gvwInbox.Columns[5].OptionsColumn.AllowEdit = false;
            gvwInbox.Columns[6].OptionsColumn.AllowEdit = false;
            gvwInbox.Columns[7].OptionsColumn.AllowEdit = false;
            gvwInbox.Columns[8].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwInbox);
            gvwInbox.FocusedRowHandle = 0;


        }
       
        void SetSent()
        {
            gvwSent.Columns[0].Caption = "Төлөв";
            gvwSent.Columns[0].ColumnEdit = imagecombo;
            gvwSent.Columns[1].Caption = "Мессэжний дугаар";
            gvwSent.Columns[1].Visible = false;
            gvwSent.Columns[2].Caption = "Илгээсэн хэрэглэгч";
            gvwSent.Columns[3].Caption = "Хүлээн авсан хэрэглэгч";
            gvwSent.Columns[4].Caption = "Мэдээлэл";
            gvwSent.Columns[5].Caption = "Програмын илгээсэн огноо";
            gvwSent.Columns[5].Visible = false;
            gvwSent.Columns[6].Caption = "Илгээсэн огноо";
            gvwSent.Columns[7].Caption = "Илгээсэн хэрэглэгчийн дугаар";
            gvwSent.Columns[7].Visible = false;
            gvwSent.Columns[8].Caption = "Хүлээн авсан хэрэглэгчийн дугаар";
            gvwSent.Columns[8].Visible = false;
            gvwSent.Columns[6].GroupIndex = 0;
            gvwSent.Columns[6].GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateRange;
            gvwSent.Columns[6].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gvwSent.Columns[0].OptionsColumn.AllowEdit = false;
            gvwSent.Columns[1].OptionsColumn.AllowEdit = false;
            gvwSent.Columns[2].OptionsColumn.AllowEdit = false;
            gvwSent.Columns[3].OptionsColumn.AllowEdit = false;
            gvwSent.Columns[4].OptionsColumn.AllowEdit = false;
            gvwSent.Columns[5].OptionsColumn.AllowEdit = false;
            gvwSent.Columns[6].OptionsColumn.AllowEdit = false;
            gvwSent.Columns[7].OptionsColumn.AllowEdit = false;
            gvwSent.Columns[8].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwSent);
            gvwSent.FocusedRowHandle = 0;
        }
        #endregion
        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                page = false;
                if (!load)
                {
                    RefreshInbox();
                }
            }
            else
            {
                page = true;
                if (!loadsent)
                {
                    RefreshSent();
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            frmSend frm = new frmSend(_core,0,"",0);
            frm.ShowDialog();
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            DataRow dr;
            if(page==false)
            {
                dr=gvwInbox.GetFocusedDataRow();
            }
            else
            {
                dr=gvwSent.GetFocusedDataRow();
            }
            frmSend frm = new frmSend(_core, Static.ToInt(dr["FROMUSERNO1"]), Static.ToStr(dr["DESCRIPTION"]),1);
            frm.ShowDialog();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {           
            DataRow dr;
            if(page==false)
            {
                dr=gvwInbox.GetFocusedDataRow();
            }
            else
            {
                dr=gvwSent.GetFocusedDataRow();
            }
            frmSend frm = new frmSend(_core, 0, Static.ToStr(dr["DESCRIPTION"]), 2);
            frm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            try
            {
                if (MessageBox.Show(this, "Бичлэгийг устгахдаа итгэлтэй байна уу.", "Анхааруулга", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Result res = new Result();
                    int userno = 0;
                    int type = 0;
                    DataRow dr;
                    if (page == false)
                    {
                        dr = gvwInbox.GetFocusedDataRow();
                        if (dr != null)
                        {
                            userno = Static.ToInt(dr["TOUSERNO1"]);
                            type = 0;
                        }
                    }
                    else
                    {
                        dr = gvwSent.GetFocusedDataRow();
                        if (dr != null)
                        {
                            userno = Static.ToInt(dr["FROMUSERNO1"]);
                            type = 1;
                        }
                    }
                    if (dr != null)
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 226, 110119, 110119, new object[] { Static.ToLong(dr["MSGID"]), userno, type });
                        if (res.ResultNo == 0)
                        {
                            MessageBox.Show("Амжилттай устгагдлаа");
                            if (page == false)
                            {
                                RefreshInbox();
                            }
                            else
                            {
                                RefreshSent();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gvwInbox_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Result res = new Result();
            try
            {
                DataRow dr = gvwInbox.GetFocusedDataRow();
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 226, 110122, 110122, new object[] { Static.ToLong(dr["MSGID"]) });
                if (res.ResultNo != 0)
                {
                    MessageBox.Show(res.ResultNo + " " + res.ResultDesc);
                }
                else
                {
                    dr["ISREAD"] = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshInbox();
            RefreshSent();
        }

        private void frmMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwInbox);
            FormUtility.SaveStateGrid(appname, formname, ref gvwSent);
        }

        private void frmMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.Delete) Delete();
        }
    }
}