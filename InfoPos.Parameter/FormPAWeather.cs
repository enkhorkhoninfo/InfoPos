using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

using EServ.Shared;
using ISM.Template;
using System.Drawing.Imaging;
namespace InfoPos.Parameter
{
    public partial class FormPAWeather : ISM.Template.frmTempProp
    {
        #region[Хувьсагчууд]
        InfoPos.Core.Core _core = null;
        int rowhandle = 0;
        object[] OldValue;
        object[] FieldName;
        int btn = 0;
        string appname = "", formName = "";
        Form FormName = null;
        String data = "";
        #endregion[]
        public FormPAWeather(Core.Core core)
        {
            try
            {
                InitializeComponent();
                _core = core;
                Init();
                this.Resource = _core.Resource;
                this.FieldLinkSetSaveState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region[Init]
        private void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormPAWeather_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormPAWeather_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormPAWeather_EventSave);
                this.EventEdit += new delegateEventEdit(FormPAWeather_EventEdit);
                this.EventDelete += new delegateEventDelete(FormPAWeather_EventDelete);
                this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);

                this.FieldLinkAdd("txtWeatherId", "WEATHERID", "", true, true);
                this.FieldLinkAdd("txtName", "Name", "", true, false);
                this.FieldLinkAdd("txtName2", "Name2", "", false, false);
                this.FieldLinkAdd("txtDescription", "Description", "", false, false);
                this.FieldLinkAdd("txtOrderNo", "OrderNo", "", false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion[]
        #region[Үзэгдлүүд]
        void FormPAWeather_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140145, 140145, new object[] { txtWeatherId.EditValue });
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
        void FormPAWeather_EventEdit(ref bool cancel)
        {
            try
            {
                data = Static.ToStr(txtIcon.Image);
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);

                object[] Value = { txtWeatherId.EditValue, txtName.EditValue, txtName2.EditValue, txtDescription.EditValue, encodedData, txtOrderNo.EditValue };
                OldValue = Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormPAWeather_EventSave(bool isnew, ref bool cancel)
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
                object[] NewValue = { 
                                        txtWeatherId.EditValue, txtName.EditValue, txtName2.EditValue, txtDescription.EditValue, ToBase64(txtIcon.Image,System.Drawing.Imaging.ImageFormat.Gif), txtOrderNo.EditValue
                                    };
                if (!isnew)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140144, 140144, new object[] { NewValue, OldValue, FieldName });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else { MessageBox.Show("Амжилттай засварлалаа."); }
                    
                }
                else
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140143, 140143, new object[] { NewValue, FieldName });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        cancel = true;
                    }
                    else { MessageBox.Show("Амжилттай нэмлээ ."); }
                    
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formName, ref gridView1);
        }
        void FormPAWeather_EventRefreshAfter()
        {
            try
            {
                this.FieldLinkSetColumnCaption(0, "Цаг агаарын төрлийн код");
                this.FieldLinkSetColumnCaption(1, "Нэр");
                this.FieldLinkSetColumnCaption(2, "Нэр2");
                this.FieldLinkSetColumnCaption(3, "Тайлбар");
                this.FieldLinkSetColumnCaption(4, "Зураг");
                this.FieldLinkSetColumnCaption(5, "Эрэмбэ");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void FormPAWeather_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140141, 140141, null);
                if (r.ResultNo == 0)
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
                    else
                    {
                        //if (r.Data != null)
                        //{
                        //    if (r.AffectedRows > 0)
                        //    {
                        //        byte[] a = (byte[])r.Data.Tables[0].Rows[0][0];
                        //        txtIcon.Image = Static.ImageFromByte(a);
                        //    }
                        //    else
                        //        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        //    return;
                        //}

                        
                    }

                }            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ISM.Template.FormImage img = new FormImage();
            img.Resource = _core.Resource;
            img.ShowDialog();
            if (img.DialogResult == System.Windows.Forms.DialogResult.OK)
                txtIcon.Image = img.ImageObject;
        }
        #endregion[]                
        #region[Form]
        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        private void FormPAWeather_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
        }
        public void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            txtIcon.Image = Base64ToImage(Static.ToStr(dr["ICON"]));
        }
        public string ToBase64(Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                //Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        #endregion[]

        private void FormPAWeather_Load(object sender, EventArgs e)
        {

        }
    }
}