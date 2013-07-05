using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;
using ISM.Template;
using InfoPos.Messages;

namespace InfoPos.Parameter
{
    public partial class FaPosition : ISM.Template.frmTempProp
    {
        #region [ Хувьсагчууд ]
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        #endregion
        #region [ Байгуулагч функц ]
        public FaPosition(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region [ Init ]
        private void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FaPosition_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FaPosition_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FaPosition_EventSave);
                this.EventEdit += new delegateEventEdit(FaPosition_EventEdit);
                this.EventDelete += new delegateEventDelete(FaPosition_EventDelete);

                this.FieldLinkAdd("numTypeCode", "TypeCode", "", true, true);
                this.FieldLinkAdd("txtPosition", "Position", "", true, false);
                this.FieldLinkAdd("numOrderNo", "OrderNo", "", true, false);

                this.Resource = _core.Resource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region [ Events ]
        void FaPosition_EventDelete()
        {
            DialogResult DR = MessageBox.Show(MSG.Messages(_core.Lang,10007), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140245, 140245, new object[] { Static.ToInt(numTypeCode.EditValue) });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                    else
                    {
                        MessageBox.Show(MSG.Messages(_core.Lang, 10003));
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void FaPosition_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToInt(numTypeCode.EditValue), Static.ToStr(txtPosition.EditValue), Static.ToInt(numOrderNo.EditValue) };
            OldValue = Value;
        }
        void FaPosition_EventSave(bool isnew, ref bool cancel)
        {
            Result r;
            string err = "";
            Control cont = null;
            if (!FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
            else
            {
                try
                {
                    if (!isnew)
                    {
                        object[] NewValue = { Static.ToInt(numTypeCode.EditValue), Static.ToStr(txtPosition.EditValue), Static.ToInt(numOrderNo.EditValue) };
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140244, 140244, new object[] { NewValue, OldValue, FieldName });
                        MessageBox.Show(MSG.Messages(_core.Lang, 10001));
                    }

                    else
                    {
                        object[] NewValue = { Static.ToInt(numTypeCode.EditValue), Static.ToStr(txtPosition.EditValue), Static.ToInt(numOrderNo.EditValue) };
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140243, 140243, new object[] {NewValue,FieldName});
                        MessageBox.Show(MSG.Messages(_core.Lang, 10000));
                    }
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            Form FormName = this;
            ISM.Template.FormUtility.SaveStateForm(_core.ApplicationName, ref FormName);
        }
        void FaPosition_EventRefreshAfter()
        {
            this.FieldLinkSetColumnCaption(0, "Байрлалын дугаар");
            this.FieldLinkSetColumnCaption(1, "Байршил");
            this.FieldLinkSetColumnCaption(2, "Жагсаалтын эрэмбэ");
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle-1; break;
            }
            btn = 0;
        }
        void FaPosition_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140241, 140241, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
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
                    switch (btn)
                    {
                        case 0: gridView1.FocusedRowHandle = rowhandle; break;
                        case 1: gridView1.FocusedRowHandle = rowhandle-1; break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region [FormEvent]
        private void FaPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FaPosition_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridControl1);
        }
        #endregion
        
    }
}
