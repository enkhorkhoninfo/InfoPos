using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class FormObject : Form
    {
        #region [ Variables ]
        Core.Core _core;
        int _objectid = 0;
        int TablePrivSelect = 200126, TablePrivUpdate = 200129, PrivNo = 200126;
        int rowhandle = 0;
        string appname = "", formname = "";
        Form FormName = null;
        object[] OldValue;
        #endregion
        #region[Байгуулагч функц]
        public FormObject(Core.Core core, int Pobjectid)
        {
            InitializeComponent();
            _objectid = Pobjectid;
            _core = core;
            Init();
            ucObject.FieldLinkSetSaveState();
        }
        #endregion   
        #region [ Init ]
        private void Init()
        {
            InitEvents();
            InitCombos();
            InitToggles();
            InitData();
        }
        private void InitEvents()
        {
            ucObject.EventSave += new ucTogglePanel.delegateEventSave(ucObject_EventSave);
            ucObject.EventDelete += new ucTogglePanel.delegateEventDelete(ucObject_EventDelete);
            ucObject.EventExit += new ucTogglePanel.delegateEventExit(ucObject_EventExit);
            ucObject.EventAdd += new ucTogglePanel.delegateEventAdd(ucObject_EventAdd);
            ucObject.EventEdit += new ucTogglePanel.delegateEventEdit(ucObject_EventEdit);
        }
        void ucObject_EventEdit(ref bool cancel)
        {
            object[] Value = { txtObjectID.EditValue,cboClassID.EditValue,txtName.EditValue,txtName2.EditValue,txtOrderNo.EditValue};
            OldValue = Value;
        }
        void ucObject_EventAdd(ref bool cancel)
        {
            //AllBtnEnable();
            //txtCustomerName.Text = "";
            //cboStatus.Text = "0";
        }
        private void InitCombos()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";

            DictUtility.PrivNo = PrivNo;

            string[] names = new string[] { "OBJECTCLASS" };
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

            DT = (DataTable)Tables[0];
            if (DT == null)
            {
                msg = "Dictionary-д OBJECTCLASS оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboClassID, DT, "CLASSID", "NAME");
            }

            if (msg != "")
                MessageBox.Show(msg);
        }
        private void InitToggles()
        {
            try
            {
                ucObject.ToggleShowDelete = true;
                ucObject.ToggleShowEdit = true;
                ucObject.ToggleShowExit = true;
                ucObject.ToggleShowNew = true;
                ucObject.ToggleShowReject = true;
                ucObject.ToggleShowSave = true;

                ucObject.Resource = _core.Resource;
                ucObject.FieldContainer = pnlObject.Controls;
                ucObject.GridView = gvwObject;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitData()
        {
            try
            {
                ucObject.FieldLinkAdd("txtObjectID", 0, "ObjectID", "", true, true, false);
                ucObject.FieldLinkAdd("cboClassID", 0, "ClassID", "", true, false);
                ucObject.FieldLinkAdd("txtName", 0, "Name", "", true, false);
                ucObject.FieldLinkAdd("txtName2", 0, "Name2", "", false, false);
                ucObject.FieldLinkAdd("txtOrderNo", 0, "OrderNo", "", false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region [ Main ]
        private void LoadDataObject(int pobjectid)
        {
            RefreshObjectData(pobjectid);
        }
        void ucObject_EventExit(bool editing, ref bool cancel)
        {
            this.Close();
        }
        void ucObject_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;

            if (ucObject.FieldValidate(ref err, ref cont) == true)
            {
                SaveObjectData(isnew, ref cancel);
            }
            else
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
        }
        void ucObject_EventDelete()
        {
            Result res = new Result();

            try
            {
                DialogResult d = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == System.Windows.Forms.DialogResult.No) return;

                int objectid = Static.ToInt(txtObjectID.Text);

                if (objectid != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140195, 140195, new object[] { objectid });

                    if (res.ResultNo == 0)
                    {
                        RefreshObjectData(_objectid);
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        gvwObject.FocusedRowHandle = rowhandle - 1;
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
        void SetObjectData()
        {
            gvwObject.Columns[0].Caption = "Зүйлийн дугаар";
            gvwObject.Columns[1].Caption = "Зүйлийн төрөл";
            gvwObject.Columns[2].Caption = "Нэр";
            gvwObject.Columns[3].Caption = "Нэр2";
            gvwObject.Columns[4].Caption = "Эрэмбэ";

            gvwObject.Columns[0].OptionsColumn.AllowEdit = false;
            gvwObject.Columns[1].OptionsColumn.AllowEdit = false;
            gvwObject.Columns[2].OptionsColumn.AllowEdit = false;
            gvwObject.Columns[3].OptionsColumn.AllowEdit = false;
            gvwObject.Columns[4].OptionsColumn.AllowEdit = false;
            FormUtility.RestoreStateGrid(appname, formname, ref gvwObject);
        }
        void RefreshObjectData(int pobjectid)
        {
            rowhandle = gvwObject.FocusedRowHandle;
            Result res = new Result();
            try
            {
                object[] obj = new object[5];
                if (pobjectid == 0) obj = null;
                else
                    obj[0] = pobjectid;

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140191, 140191, obj);
                if (res.AffectedRows != 0)
                {
                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        ucObject.DataSource = res.Data;
                        SetObjectData();
                        gvwObject.FocusedRowHandle = rowhandle - 1;
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
        void SaveObjectData(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            object[] obj = new object[5];
            object[] FieldName = {"OBJECTID","CLASSID","NAME","NAME2","ORDERNO"};
            string msg = "";
            try
            {
                obj[0] = Static.ToInt(txtObjectID.EditValue);   //ObjectID
                obj[1] = Static.ToInt(cboClassID.EditValue);    //ClassID
                obj[2] = Static.ToStr(txtName.EditValue);       //Name
                obj[3] = Static.ToStr(txtName2.EditValue);      //Name2
                obj[4] = Static.ToInt(txtOrderNo.EditValue);    //OrderNo

                if (isnew)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140193, 140193, new object[]{obj,FieldName});
                    msg = "Амжилттай нэмлээ .";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140194, 140194, new object[] {obj,OldValue,FieldName});
                    msg = "Амжилттай засварлалаа .";
                }

                if (res.ResultNo == 0)
                {
                    RefreshObjectData(_objectid);
                    MessageBox.Show(msg);
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                    cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cancel = true;
            }
        }
        private void gvwObject_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucObject.FieldLinkSetValues();
        }
        #endregion
        #region[BTN]
        private void btnSubObject_Click(object sender, EventArgs e)
        {
            DataRow DR = gvwObject.GetDataRow(gvwObject.FocusedRowHandle);

            if (DR != null)
            {
                int objectid = Static.ToInt(DR["OBJECTID"]);
                InfoPos.Parameter.FormObjectSubObject frm = new InfoPos.Parameter.FormObjectSubObject(_core, objectid);
                frm.ShowDialog();
            }
        }
        private void btnItem_Click(object sender, EventArgs e)
        {
            DataRow DR = gvwObject.GetDataRow(gvwObject.FocusedRowHandle);

            if (DR != null)
            {
                int objectid = Static.ToInt(DR["OBJECTID"]);
                InfoPos.Parameter.FormObjectShowItems frm = new InfoPos.Parameter.FormObjectShowItems(_core, objectid);
                frm.ShowDialog();
            }
        }
        #endregion
        #region[FormEvent]
        private void FormObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormObject_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwObject);
        }
        private void FormObject_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            LoadDataObject(_objectid);
        }
        #endregion

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
