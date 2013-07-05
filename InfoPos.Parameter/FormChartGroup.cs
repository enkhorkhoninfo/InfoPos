using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;
using DevExpress.XtraEditors;

namespace HeavenPro.Parameter
{
    public partial class FormChartGroup :ISM.Template.frmTempProp
    {
        object[] OldValue;
        object[] FieldName;

        int SelectTxnCode = 140121;
        int AddTxnCode = 140123;
        int EditTxnCode = 140124;
        int DeleteTxnCode=140125;

        HeavenPro.Core.Core _core = null;

        public FormChartGroup(HeavenPro.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();
            this.Resource = _core.resource;

            if (_core.RemoteObject.User.UserNo != 0) this.FieldLinkSetEditState();
            else this.FieldLinkSetNewState();
        }

        private void Init() 
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormChartGroup_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormChartGroup_EventRefreshAfter);
                this.EventAdd += new delegateEventAdd(FormChartGroup_EventAdd);
                this.EventSave += new delegateEventSave(FormChartGroup_EventSave);
                this.EventEdit += new delegateEventEdit(FormChartGroup_EventEdit);
                this.EventDelete += new delegateEventDelete(FormChartGroup_EventDelete);

                this.FieldLinkAdd("numGroupNo", "GroupNo", "", true, true);
                this.FieldLinkAdd("txtName", "Name", "", true, false);
                this.FieldLinkAdd("txtName2", "Name2", "", false, false);
                this.FieldLinkAdd("cboType", "Type", "", true, false);
                this.FieldLinkAdd("cboCloseType", "CloseType", "", true, false);
                this.FieldLinkAdd("numOrderNo", "OrderNo", "", false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InitCombo() 
        {
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 1, "Active");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 2, "Passive");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 3, "Өөрийн хөрөнгө");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 4, "Орлого");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 5, "Зарлага");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 6, "Балансын гадуурх");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 7, "Систем");

            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboCloseType, 0, "үлдэгдэлтэй хаагдана");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboCloseType, 1, "тэг үлдэгдэлтэй хаагдана");
        }

        #region[Event]

        void FormChartGroup_EventSave(bool isnew)
        {
            Result r;
            try
            {
                if (!isnew)
                {
                    object[] NewValue = {numGroupNo.Text,txtName.Text,txtName2.Text,cboType.Text,cboCloseType.Text,numOrderNo.Text};
                    r = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 202, EditTxnCode, EditTxnCode , new object[] { NewValue, OldValue, FieldName });
                }
                else
                {
                    r = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 202, AddTxnCode, AddTxnCode, new object[] { numGroupNo.Text, txtName.Text, txtName2.Text, cboType.Text, cboCloseType.Text, numOrderNo.Text });
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void FormChartGroup_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 202, DeleteTxnCode, DeleteTxnCode, new object[] { numGroupNo.Text });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void FormChartGroup_EventEdit()
        {
            object[] Value = { numGroupNo.Text, txtName.Text, txtName2.Text, cboType.Text, cboCloseType.Text, numOrderNo.Text };
            OldValue = Value;
        }

        void FormChartGroup_EventAdd()
        {

        }

        void FormChartGroup_EventRefreshAfter()
        {
            this.FieldLinkSetColumnCaption(0, "Төрөлийн дугаар");
            this.FieldLinkSetColumnCaption(1, "Төрөлийн нэр");
            this.FieldLinkSetColumnCaption(2, "", true);
            this.FieldLinkSetColumnCaption(3, "", true);
            this.FieldLinkSetColumnCaption(4, "", true);
            this.FieldLinkSetColumnCaption(5, "", true);
        }

        void FormChartGroup_EventRefresh(ref DataTable dt)
        {
            try
            {
                Result r = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 202, SelectTxnCode, SelectTxnCode, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
                else
                {
                    dt = r.Data.Tables[0];
                    int index = 0;
                    object[] Value = new object[dt.Columns.Count];
                    foreach (DataColumn col in dt.Columns)
                    {
                        Value[index] = col.ColumnName;
                        index++;
                    }
                    FieldName = Value;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void FormChartGroup_Load(object sender, EventArgs e)
        {
            object[] Value = { numGroupNo.Text, txtName.Text, txtName2.Text, cboType.Text, cboCloseType.Text, numOrderNo.Text };
            OldValue = Value;
        }

    }
}