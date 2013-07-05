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
    public partial class FormObjectSubObject : Form
    {
        #region [ Variables ]
        Core.Core _core;
        int _objectid = 0;
        string appname = "", formname = "";
        int objid = 0;
        Form FormName = null;
        #endregion
        #region [ Init ]
        public FormObjectSubObject(Core.Core core, int _pobjectid)
        {
            InitializeComponent();
            _core = core;
            _objectid = _pobjectid;
        }
        private void FormObject_Load(object sender, EventArgs e)
        {
            appname = _core.ApplicationName;
            formname = "List." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            Init();
            LoadDataObject();
        }
        private void Init()
        {
            InitEvents();
            InitToggles();
            InitData();
        }
        private void InitEvents()
        {
            ucObject.EventSave += new ucTogglePanel.delegateEventSave(ucObject_EventSave);
            ucObject.EventDelete += new ucTogglePanel.delegateEventDelete(ucObject_EventDelete);
            ucObject.EventExit += new ucTogglePanel.delegateEventExit(ucObject_EventExit);
            ucObject.EventEdit+=new ucTogglePanel.delegateEventEdit(ucObject_EventEdit);
            ucObject.EventAdd+=new ucTogglePanel.delegateEventAdd(ucObject_EventAdd);
            ucObject.EventReject+=new ucTogglePanel.delegateEventReject(ucObject_EventReject);
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
                ucObject.GridView = gvwSubObject;
                ISM.Template.FormUtility.SetFormatGrid(ref gvwSubObject, false);
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
                ucObject.FieldLinkAdd("txtObjectID", 0, "ObjectID", "", true, false, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region [ Main ]
        private void LoadDataObject()
        {
            RefreshObjectData();
        }
        void RefreshObjectData()
        {
            Result res = new Result();
            try
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140231, 140231, new object[] { _objectid });
                if (res.AffectedRows != 0)
                {
                    if (res.ResultNo == 0 || res.ResultNo == 9110014)
                    {
                        ucObject.DataSource = res.Data;
                        ucObject.FieldLinkSetValues();
                        SetObjectData();
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
            gvwSubObject.Columns[0].Caption = "Зүйлийн дугаар";
            gvwSubObject.Columns[1].Caption = "Зүйлийн нэр";
            FormUtility.RestoreStateGrid(appname, formname, ref gvwSubObject);
        }
        void SaveObjectData(bool isnew, ref bool cancel)
        {
            Result res = new Result();
            object[] obj = new object[2];
            string msg = "";
            try
            {
                obj[0] = _objectid;
                obj[1] = Static.ToInt(txtObjectID.EditValue);
                if (_objectid != Static.ToInt(txtObjectID.EditValue))
                {
                    if (!isnew)
                    {
                        DataRow dr = gvwSubObject.GetFocusedDataRow();
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140234, 140234, new object[] { _objectid, txtObjectID.EditValue, dr["objectid"] });
                        msg = "Амжилттай засварлалаа";
                    }
                    else
                    {
                        res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140233, 140233, obj);
                        msg = "Амжилттай нэмлээ";
                    }
                    btnObjectID.Enabled = false;
                    if (res.ResultNo == 0)
                    {
                        RefreshObjectData();
                        MessageBox.Show(msg);
                    }
                    else
                    {
                        MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                        cancel = true;
                    }
                }
                else
                    MessageBox.Show("Зүйлийн зүйл дээр өөрөө өөрийгөө сонгосон байна");
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
        #region[Event]
        void ucObject_EventEdit(ref bool cancel)
        {
            btnObjectID.Enabled = true;
        }
        void ucObject_EventReject()
        {
            btnObjectID.Enabled = false;
        }
        void ucObject_EventAdd(ref bool cancel)
        {
            btnObjectID.Enabled = true;
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

                int _subobjectid = Static.ToInt(txtObjectID.Text);

                if (_objectid != 0 && _subobjectid != 0)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140235, 140235, new object[] { _objectid, _subobjectid });

                    if (res.AffectedRows != 0)
                    {
                        if (res.ResultNo == 0)
                        {
                            RefreshObjectData();
                            MessageBox.Show("Амжилттай устгагдлаа");
                        }
                        else
                        {
                            MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[FormEvent]
        private void FormObjectSubObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void FormObjectSubObject_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gvwSubObject);

        }
        private void btnObjectID_Click_1(object sender, EventArgs e)
        {
            //objid = Static.ToInt(txtObjectID.EditValue);
            //InfoPos.List.Object frm1 = new InfoPos.List.Object(_core);
            //frm1.ucObject.Browsable = true;

            //DialogResult res = frm1.ShowDialog();
            //if ((res == System.Windows.Forms.DialogResult.OK))
            //{
            //    if (Static.ToInt(frm1.ucObject.SelectedRow["OBJECTID"]) != _objectid)
            //        txtObjectID.Text = Static.ToStr(frm1.ucObject.SelectedRow["OBJECTID"]);
            //    else
            //        MessageBox.Show("Зүйлийн зүйл дээр өөрөө өөрийгөө сонгосон байна");
            //}
        }
        #endregion
    }
}
