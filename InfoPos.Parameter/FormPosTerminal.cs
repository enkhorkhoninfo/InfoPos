using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormPosTerminal : ISM.Template.frmTempProp
    {
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        int PrivNo = 111114;

        public FormPosTerminal(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            this.Resource = core.Resource;
            Init();
            InitCombo();
            this.FieldLinkSetSaveState();
        }
        private void Init() 
        {
            this.EventRefresh += new delegateEventRefresh(FormPosTerminal_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormPosTerminal_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormPosTerminal_EventSave);
            this.EventDelete += new delegateEventDelete(FormPosTerminal_EventDelete);
            this.EventEdit += new delegateEventEdit(FormPosTerminal_EventEdit);
            this.EventRowChanged += new delegateEventRowChanged(FormPosTerminal_EventRowChanged);

            this.FieldLinkAdd("txtPOSNo", "POSNo", "", true , true);
            this.FieldLinkAdd("txtPOSName", "POSName", "", false, false);
            this.FieldLinkAdd("txtPOSDesc", "POSDesc", "", false, false);
            this.FieldLinkAdd("txtPOSIP", "POSIP", "", false, false);
            this.FieldLinkAdd("txtPOSMAC", "POSMAC", "", false, false);
            this.FieldLinkAdd("cboPOSType", "POSType", "", true, false);
            this.FieldLinkSetValues();
        }

        void FormPosTerminal_EventRowChanged(int rowno)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            
        }
        void FormPosTerminal_EventEdit(ref bool cancel)
        {
            object[] Value = { txtPOSNo.EditValue, txtPOSName.EditValue, txtPOSDesc.EditValue, txtPOSIP.EditValue, txtPOSMAC.EditValue, cboPOSType.EditValue };
            OldValue = Value;
        }
        void FormPosTerminal_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140080, 140080, new object[] { txtPOSNo.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void FormPosTerminal_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            if (!FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
                return;
            }
            Result r;
            try
            {
                object[] NewValue = { Static.ToStr(txtPOSNo.EditValue), 
                                      Static.ToStr(txtPOSName.EditValue), 
                                      Static.ToStr(txtPOSDesc.EditValue),
                                      Static.ToStr(txtPOSIP.EditValue),
                                      Static.ToStr(txtPOSMAC.EditValue),
                                      Static.ToStr(cboPOSType.EditValue)
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140079, 140079, new object[] { NewValue, OldValue, FieldName });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140078, 140078, new object[] { NewValue, FieldName });
                    MessageBox.Show("Амжилттай нэмлээ .");
                }
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formName, ref gridView1);
        }
        void FormPosTerminal_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "ПОС ын дугаар ");
            this.FieldLinkSetColumnCaption(1, "ПОС ын нэр ");
            this.FieldLinkSetColumnCaption(2, "ПОС ын тайлбар");
            this.FieldLinkSetColumnCaption(3, "ПОС ын IP");
            this.FieldLinkSetColumnCaption(4, "ПОС ын MAC");
            this.FieldLinkSetColumnCaption(5, "ПОС ын төрөл");
            appname = _core.ApplicationName;
            //formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, "Parameter." + this.Name, ref gridView1);
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }
        private void InitCombo()
        {
            try
            {
                Result res = new Result();
                ArrayList Tables = new ArrayList();
                DataTable DT = null;
                string msg = "";

                DictUtility.PrivNo = PrivNo;
                string[] name = new string[] { "PayType" };
                res = DictUtility.Get(_core.RemoteObject, name, ref Tables);

                DT = (DataTable)Tables[0];
                if (DT == null)
                {
                    msg = "Dictionary-д PayType оруулаагүй байна-" + res.ResultDesc;
                    MessageBox.Show(msg);
                }
                else
                {
                    FormUtility.LookUpEdit_SetList(ref cboPayTypeID, DT, "typeid", "NAME");
                }

                FormUtility.LookUpEdit_SetList(ref cboPOSType, "I", "Бараа");
                FormUtility.LookUpEdit_SetList(ref cboPOSType, "S", "Үйлчилгээ");
                FormUtility.LookUpEdit_SetList(ref cboPOSType, "IS", "Бараа болон үйлчилгээ");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Өгөгдлийн баазаас Dictionary олдсонгүй.");
            }
        }
        void FormPosTerminal_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 140076, 140076, null);
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
                    switch (btn)
                    {
                        case 0: gridView1.FocusedRowHandle = rowhandle; break;
                        case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            cboPOSType.EditValue= "I";
        }

        #region [ PayType ]
        private void btnPayTypeAdd_Click(object sender, EventArgs e)
        {
            Result res = null;

            if (Static.ToStr(cboPayTypeID.EditValue) != "")
            {
                object[] obj = {    
                                   Static.ToStr(txtPOSNo.EditValue),
                                   Static.ToStr(cboPayTypeID.EditValue)
                               };
                try
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 111115, 111115, new object[] { obj });

                    if (res.ResultNo == 0)
                    {
                        RefreshPayType(Static.ToStr(txtPOSNo.EditValue));
                        MessageBox.Show("Амжилттай нэмлээ");
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Төлбөрийн төрлөө сонгоно уу");
            }
        }
        void RefreshPayType(string pPosNo)
        {
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, 111114, 111114, new object[] { pPosNo });
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                }
                else
                {
                    grdPayType.DataSource = r.Data.Tables[0];
                    SetPayTypeData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetPayTypeData()
        {
            try
            {
                gvwPayType.Columns[0].Caption = "PosNo";
                gvwPayType.Columns[0].OptionsColumn.AllowEdit = false;
                gvwPayType.Columns[1].Caption = "Төлбөрийн төрлийн дугаар";
                gvwPayType.Columns[1].OptionsColumn.AllowEdit = false;
                gvwPayType.Columns[2].Caption = "Төлбөрийн төрлийн нэр";
                gvwPayType.Columns[2].OptionsColumn.AllowEdit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPayTypeDelete_Click(object sender, EventArgs e)
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                DataRow dr = gvwPayType.GetFocusedDataRow();
                string pPOSNo = Static.ToStr(dr["POSNo"]);
                string pPayTypeID = Static.ToStr(dr["PayTypeID"]);

                if (pPOSNo != "" && pPayTypeID != "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 111117, 111117, new object[] { pPOSNo, pPayTypeID });

                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилттай устгагдлаа");
                        RefreshPayType(Static.ToStr(txtPOSNo.EditValue));
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPayTypeRefresh_Click(object sender, EventArgs e)
        {
            RefreshPayType(Static.ToStr(txtPOSNo.EditValue));
        }
        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            switch (e.PageIndex)
            {
                case 1:
                    RefreshPayType(Static.ToStr(txtPOSNo.EditValue));
                    break;
            }
        }
        #endregion

    }
}