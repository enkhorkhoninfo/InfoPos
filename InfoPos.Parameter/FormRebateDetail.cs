using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormRebateDetail : ISM.Template.frmTempProp
    {
        #region[Variable]
        Core.Core _core;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        #endregion[]
        #region[Construction]
        public FormRebateDetail(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion[]
        #region[Init]
        private void Init()
        {
            this.EventRefresh += new delegateEventRefresh(FormRebateDetail_EventRefresh);
            this.EventRefreshAfter += new delegateEventRefreshAfter(FormRebateDetail_EventRefreshAfter);
            this.EventSave += new delegateEventSave(FormRebateDetail_EventSave);
            this.EventEdit += new delegateEventEdit(FormRebateDetail_EventEdit);
            this.EventDelete += new delegateEventDelete(FormRebateDetail_EventDelete);

            this.FieldLinkAdd("numMasterID", "MasterID", "", true, true);
            this.FieldLinkAdd("cboProdType", "ProdType", "", true, false);
            this.FieldLinkAdd("cboProdNo", "ProdNo", "", false, false);
            this.FieldLinkAdd("cboCalcType", "CalcType", "", true, false);
            this.FieldLinkAdd("numCalcAmount", "CalcAmount", "", false, false);


            FormUtility.LookUpEdit_SetList(ref cboProdType, 0, "Бараа");
            FormUtility.LookUpEdit_SetList(ref cboProdType, 1, "Үйлчилгээ");


            FormUtility.LookUpEdit_SetList(ref cboCalcType, 0, "Дүн");
            FormUtility.LookUpEdit_SetList(ref cboCalcType, 1, "Хувь");

        }        
         
        #endregion[]
        #region[Event]
        void FormRebateDetail_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140345, 140345, new object[] { numMasterID.EditValue });
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
        void FormRebateDetail_EventEdit(ref bool cancel)
        {
            try
            {
                object[] Value = { numMasterID.EditValue, cboProdType.EditValue, cboProdNo.EditValue, cboCalcType.EditValue, numCalcAmount.EditValue };
                OldValue = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormRebateDetail_EventSave(bool isnew, ref bool cancel)
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


                object[] NewValue = { numMasterID.EditValue, cboProdType.EditValue, cboProdNo.EditValue, cboCalcType.EditValue, numCalcAmount.EditValue };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140344, 140344, new object[] { NewValue });
                    MessageBox.Show("Амжилттай засварлалаа.");
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140343, 140343, new object[] { NewValue });
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
        }
        void FormRebateDetail_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "ID дугаар ");
            this.FieldLinkSetColumnCaption(1, "Хөнгөлөлт, Оноо, Урамшуулал эсэх");
            this.FieldLinkSetColumnCaption(2, "Нэр");
            this.FieldLinkSetColumnCaption(3, "Нэр 2");
            this.FieldLinkSetColumnCaption(4, "Жагсаалтын эрэмбэ");
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
        void FormRebateDetail_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140341, 140341, null);
                if (r.ResultNo != 0)
                {
                    if (r.Data == null)
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
        #endregion[]
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Static.ToInt(cboProdType.EditValue) == 0)
            {
                InfoPos.List.InventoryList frm = new InfoPos.List.InventoryList(_core);

                frm.ucInventoryList.Browsable = true;

                DialogResult res = frm.ShowDialog();
                if ((res == System.Windows.Forms.DialogResult.OK))
                {
                    cboProdNo.Text = Static.ToStr(frm.ucInventoryList.SelectedRow["INVID"]);
                }
            }
            else if (Static.ToInt(cboProdType.EditValue) == 1)
            {
                InfoPos.List.ServiceList frm = new InfoPos.List.ServiceList(_core);

                frm.ucServiceList.Browsable = true;

                DialogResult res = frm.ShowDialog();
                if ((res == System.Windows.Forms.DialogResult.OK))
                {
                    cboProdNo.Text = Static.ToStr(frm.ucServiceList.SelectedRow["SERVID"]);
                }
            }            
        }
    }
}