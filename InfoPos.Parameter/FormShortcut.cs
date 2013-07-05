using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using ISM.Template;

namespace InfoPos.Parameter
{
    public partial class FormShortcut : ISM.Template.frmTempProp
    {
        #region[Variables]
        int rowhandle = 0;
        int btn = 0;
        object[] OldValue;
        object[] NewValue;
        object[] FieldName;
        InfoPos.Core.Core _core = null;
        string appname = "", formname = "";
        Form FormName = null;
        FunctionParam fp = new FunctionParam();
        int SelectTxn = 140321, EditTxn = 140324, SaveTxn = 140323, DeleteTxn = 140325, SelTxn = 140322;
        string shortcut = "";
        bool up = true;
        int keys = 0, keys1 = 0, keys2 = 0;
        #endregion
        #region[Байгуулагч функц]
        public FormShortcut(Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
        }
        #endregion
        #region[Init Function]
        private void Init()
        {
            try
            {
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormShortcut_EventRefreshAfter);
                this.EventRefresh += new delegateEventRefresh(FormShortcut_EventRefresh);
                this.EventSave += new delegateEventSave(FormShortcut_EventSave);
                this.EventEdit += new delegateEventEdit(FormShortcut_EventEdit);
                this.EventDelete += new delegateEventDelete(FormShortcut_EventDelete);
                this.EventAddAfter+=new delegateEventAddAfter(FormShortcut_EventAddAfter);

                this.FieldLinkAdd("numID", "ID", "", true, true);
                this.FieldLinkAdd("txtKey", "Name", "", true, false);
                this.FieldLinkAdd("cboIDValue", "IDVALUE", "", true, false);
                this.FieldLinkAdd("txtIDValue", "IDVALUE", "",true, false);
                this.FieldLinkAdd("mmeNote", "DESCRIPTION", "", false, false);
                this.FieldLinkAdd("cboShortCtType", "TYPE", "", true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitCombo()
        {
            FormUtility.LookUpEdit_SetList(ref cboShortCtType, 1, "Цонх");
            FormUtility.LookUpEdit_SetList(ref cboShortCtType, 2, "Текст");
            fp.SetCombo(_core, "ScreenCode","TRANCODE","NAME",SelectTxn, cboIDValue, "", null);
        }
        #endregion
        #region[Event]
        void FormShortcut_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, SelectTxn, SelectTxn, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultNo.ToString());
                }
                else
                {
                    dt = r.Data.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        keys = Static.ToInt(dr["KEYS"]);
                        keys1 = Static.ToInt("KEYS1");
                        keys2 = Static.ToInt("KEYS2");
                    }
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
        void FormShortcut_EventSave(bool isnew, ref bool cancel)
        {
            Result r;
            string err = "";
            Control cont = null;
            txtKey.ToolTipTitle = "Товчнууд болон хослолууд оруулна уу .";
            if (Static.ToInt(cboShortCtType.EditValue) == 1)
            {
                txtIDValue.EditValue = 0;
            }
            else
            {
                cboIDValue.EditValue = 0;
            }
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
                    object[] NewValue = new object[8];
                    NewValue[0]=Static.ToInt(numID.EditValue);
                    NewValue[1]=Static.ToInt(keys);
                    NewValue[2] = Static.ToInt(keys1);
                    NewValue[3] = Static.ToInt(keys2);
                    NewValue[4]=Static.ToStr(txtKey.EditValue);
                    if (Static.ToInt(cboShortCtType.EditValue) == 2)
                    {
                        NewValue[5] = Static.ToStr(txtIDValue.EditValue);
                    }
                    else 
                    {
                        NewValue[5] = Static.ToStr(cboIDValue.EditValue);
                    }
                    NewValue[6]=Static.ToStr(mmeNote.EditValue);
                    NewValue[7]=Static.ToInt(cboShortCtType.EditValue);
                    object[] FieldName={"ID", "KEYS", "KEYS1", "KEYS2", "NAME", "IDVALUE", "DESCRIPTION", "TYPE"};
                    if (!isnew)
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, EditTxn, EditTxn, new object[] { NewValue, OldValue, FieldName });
                         if (r.ResultNo == 0)
                         {
                             MessageBox.Show("Амжилттай засварлалаа .");
                         }
                         else
                         {
                             MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                         }
                    }

                    else
                    {
                        r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, SaveTxn, SaveTxn, new object[] { NewValue, FieldName });
                        if (r.ResultNo == 0)
                        {
                            MessageBox.Show("Амжилттай нэмлээ .");
                        }
                        else
                        {
                            MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void FormShortcut_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToInt(numID.EditValue),keys,keys1,keys2, Static.ToStr(txtKey.EditValue), Static.ToStr(txtIDValue.EditValue), Static.ToStr(mmeNote.EditValue), Static.ToInt(cboShortCtType.EditValue) };
            OldValue = Value;
        }
        void FormShortcut_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    int ID = Static.ToInt(numID.EditValue);
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, DeleteTxn, DeleteTxn, new object[] { ID });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
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
        void FormShortcut_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
            this.FieldLinkSetColumnCaption(1, "Түлхүүр", true);
            this.FieldLinkSetColumnCaption(2, "Түлхүүр1", true);
            this.FieldLinkSetColumnCaption(3, "Түлхүүр2", true);
            this.FieldLinkSetColumnCaption(0, "ID дугаар");
            this.FieldLinkSetColumnCaption(4, "Товчнууд болон хослол");
            this.FieldLinkSetColumnCaption(5, "Талбарын утга");
            this.FieldLinkSetColumnCaption(6, "Тайлбар");
            this.FieldLinkSetColumnCaption(7, "Төрлийн дугаар", true);
            this.FieldLinkSetColumnCaption(8, "Төрөл");
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }
        void FormShortcut_EventAddAfter()
        {
            cboShortCtType.ItemIndex = 0;
            cboIDValue.ItemIndex = 0;
        }
        #endregion
        #region[FormEvent]
        private void FormShortcut_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void cboShortCtType_EditValueChanged(object sender, EventArgs e)
        {
            if (Static.ToInt(cboShortCtType.EditValue) == 1)
            {
                cboIDValue.Visible = true;
                txtIDValue.Visible = false;
                txtIDValue.EditValue = "";
            }
            else
            {
                txtIDValue.Visible = true;
                cboIDValue.Visible = false;
                cboIDValue.ItemIndex = 0;
            }
        }
        private void txtKey_KeyDown_1(object sender, KeyEventArgs e)
        {
            int[] fletter = { 112, 113, 114, 115, 116, 117, 118, 119, 120, 122, 123 };
            for (int i = 0; i < fletter.Length; i++)
            {
                if (e.KeyValue == fletter[i])
                {
                    txtKey.Text = e.KeyCode.ToString();
                    keys = e.KeyValue;
                    keys1 = 0;
                    keys2 = 0;
                    break;
                }
            }
            int[] letter = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 112, 113, 114, 116, 117, 118, 119, 120, 122, 123 };
            for (int i = 0; i < letter.Length; i++)
            {
                if (e.KeyValue == letter[i] && e.Modifiers == Keys.Control)
                {
                    txtKey.Text = "Ctrl+" + e.KeyCode.ToString();
                    keys = 131072;
                    keys1 = e.KeyValue;
                    keys2 = 0;
                    break;
                }
                if (e.KeyValue == letter[i] && e.Modifiers == Keys.Alt)
                {
                    txtKey.Text = "Alt+" + e.KeyCode.ToString();
                    keys = 262144;
                    keys1 = e.KeyValue;
                    keys2 = 0;
                    break;
                }
                if (e.KeyValue == letter[i] && e.Modifiers == Keys.Shift)
                {
                    txtKey.Text = "Shift+" + e.KeyCode.ToString();
                    keys = 65536;
                    keys1 = e.KeyValue;
                    keys2 = 0;
                    break;
                }
                if (e.KeyValue == letter[i] && e.Modifiers == (Keys.Control | Keys.Shift))
                {
                    txtKey.Text = "Ctrl+" + "Shift+" + e.KeyCode.ToString();
                    keys = 131072;
                    keys1 = 65536;
                    keys2 = e.KeyValue;
                    break;
                }
                if (e.KeyValue == letter[i] && e.Modifiers == (Keys.Control | Keys.Alt))
                {
                    txtKey.Text = "Ctrl+" + "Alt+" + e.KeyCode.ToString();
                    keys = 131072;
                    keys1 = 262144;
                    keys2 = e.KeyValue;
                    break;
                }
                if (e.KeyValue == letter[i] && e.Modifiers == (Keys.Alt | Keys.Shift))
                {
                    txtKey.Text = "Alt+" + "Shift+" + e.KeyCode.ToString();
                    keys = 262144;
                    keys1 = 65536;
                    keys2 = e.KeyValue;
                    break;
                }
            }
        }
        private void txtKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void FormShortcut_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
        #endregion
    }
}