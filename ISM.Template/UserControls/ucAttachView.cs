using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

using EServ.Shared;

namespace ISM.Template.UserControls
{
    public partial class ucAttachView : UserControl
    {
        #region Enums

        public enum enumShowType
        {
            All = -1,
            Images = 0,
            Documents = 1,
            Others = 2
        }

        #endregion
        #region Constants

        const int CONST_FILEID = 103;

        #endregion
        #region Custom Properties

        private CUser.Remote _remote;
        [DefaultValue(null), Browsable(false)]
        public CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }

        private int _typecode = 0;
        [DefaultValue(0), Browsable(true)]
        public int TypeCode
        {
            get { return _typecode; }
            set { _typecode = value; }
        }
        private string _typeid = "";
        [DefaultValue(""), Browsable(true)]
        public string TypeId
        {
            get { return _typeid; }
            set { _typeid = value; }
        }
        private int _attachtype = 0;
        [DefaultValue(0), Browsable(true)]
        public int AttachType
        {
            get { return _attachtype; }
            set { _attachtype = value; }
        }

        private int _tableprivselect = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivSelect
        {
            get { return _tableprivselect; }
            set { _tableprivselect = value; }
        }

        private int _tableprivupdate = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivUpdate
        {
            get { return _tableprivupdate; }
            set { _tableprivupdate = value; }
        }

        private int _tableprivremove = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivRemove
        {
            get { return _tableprivremove; }
            set { _tableprivremove = value; }
        }

        #endregion
        #region Custom Events

        public delegate void delegateEventDataChanged();
        public event delegateEventDataChanged EventDataChanged;
        public void OnEventDataChanged()
        {
            if (EventDataChanged != null) EventDataChanged();
        }

        #endregion
        #region Constractor
        public ucAttachView()
        {
            InitializeComponent();

            galleryControl1.Gallery.ItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(Gallery_ItemClick);
            gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView1_FocusedRowChanged);

            gridControl1.Dock = DockStyle.Fill;
            galleryControl1.Dock = DockStyle.Fill;

            gridControl1.Visible = false;
        }
        #endregion
        #region Control Events

        private void navAll_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowGallary(enumShowType.All);
        }
        private void navImage_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowGallary(enumShowType.Images);
        }
        private void navDoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowGallary(enumShowType.Documents);
        }
        private void navOther_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ShowGallary(enumShowType.Others);
        }

        private void navFileView_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenFile(0);
        }
        private void navFileAdd_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Result r = UploadFile();
            if (r != null && r.ResultNo == 0)
            {
                ShowGallary(enumShowType.All);
            }
        }
        private void navFileRemove_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ulong attachid = ISM.Lib.Static.ToULong(lblAttachId.Text);
            string filename = lblFilename.Text;
            Result r = DeleteFile(attachid, filename);
        }
        private void navFileDownload_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ulong attachid = ISM.Lib.Static.ToULong(lblAttachId.Text);
            Result r = DownloadFile(attachid);
        }

        private void radioGroup1_Properties_SelectedIndexChanged(object sender, EventArgs e)
        {
            galleryControl1.Visible = (radioGroup1.SelectedIndex == 0);
            gridControl1.Visible = !galleryControl1.Visible;
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            lblFilename.Text = "...";
            lblAttachId.Text = "...";
            lblAttachSize.Text = "...";
            lblAttachDate.Text = "...";
            lblUserNo.Text = "...";
            lblAttachDesc.Text = "...";

            if (e.FocusedRowHandle >= 0)
            {
                if (gridView1.RowCount > 0)
                {
                    string filename = ISM.Lib.Static.ToStr(gridView1.GetFocusedRowCellValue("FILENAME"));
                    ulong attachid = ISM.Lib.Static.ToULong(gridView1.GetFocusedRowCellValue("ATTACHID"));
                    long attachsize = ISM.Lib.Static.ToLong(gridView1.GetFocusedRowCellValue("ATTACHSIZE"));
                    DateTime attachdate = ISM.Lib.Static.ToDate(gridView1.GetFocusedRowCellValue("ATTACHDATE"));
                    string attachdesc = ISM.Lib.Static.ToStr(gridView1.GetFocusedRowCellValue("DESCRIPTION"));
                    int userno = ISM.Lib.Static.ToInt(gridView1.GetFocusedRowCellValue("USERNO"));

                    lblFilename.Text = filename;
                    lblAttachId.Text = Convert.ToString(attachid);
                    lblAttachSize.Text = string.Format("{0, 8:#,##0.0} KB", attachsize / 1024);
                    lblAttachDate.Text = ISM.Lib.Static.ToDateStr(attachdate);
                    lblUserNo.Text = Convert.ToString(userno);
                    lblAttachDesc.Text = attachdesc;
                }
            }
        }
        private void Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            lblFilename.Text = "...";
            lblAttachId.Text = "...";
            lblAttachSize.Text = "...";
            lblAttachDate.Text = "...";
            lblUserNo.Text = "...";
            lblAttachDesc.Text = "...";

            DevExpress.XtraBars.Ribbon.GalleryItemGroup group = galleryControl1.Gallery.Groups[0];
            if (group.Items.Count > 0)
            {
                if (e.Item != null && e.Item.Tag != null)
                {
                    object[] info = (object[])e.Item.Tag;

                    //filename, attachid, attachsize, attachtype, attachdate, attachdesc, userno
                    string filename = ISM.Lib.Static.ToStr(info[0]);
                    ulong attachid = ISM.Lib.Static.ToULong(info[1]);
                    long attachsize = ISM.Lib.Static.ToLong(info[2]);
                    int attachtype = ISM.Lib.Static.ToInt(info[3]);
                    DateTime attachdate = ISM.Lib.Static.ToDate(info[4]);
                    string attachdesc = ISM.Lib.Static.ToStr(info[5]);
                    int userno = ISM.Lib.Static.ToInt(info[6]);

                    lblFilename.Text = filename;
                    lblAttachId.Text = Convert.ToString(attachid);
                    lblAttachSize.Text = string.Format("{0, 8:#,##0.0} KB", attachsize / 1024);
                    lblAttachDate.Text = ISM.Lib.Static.ToDateStr(attachdate);
                    lblUserNo.Text = Convert.ToString(userno);
                    lblAttachDesc.Text = attachdesc;
                }
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            OpenFile(0);
        }
        private void galleryControlGallery1_ItemDoubleClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            OpenFile(0);
        }

        #endregion
        #region Server Functions

        internal Result GetAttachTable(int typecode, string typeid, enumShowType attachtype)
        {
            Result r = null;
            if (_remote != null)
            {
                #region Preparing Calling Parameters
                object[] param = new object[] { typecode, typeid, (int)attachtype };
                #endregion
                #region Call Server Function
                r = _remote.Connection.Call(
                    _remote.User.UserNo
                    , CONST_FILEID
                    , 103101
                    , _tableprivselect
                    , param
                    );
                #endregion
            }
            return r;
        }
        internal Result GetAttachTable(enumShowType attachtype)
        {
            return GetAttachTable(_typecode, _typeid, attachtype );
        }
        internal Result GetAttachImages()
        {
            Result r = null;
            if (_remote != null)
            {
                #region Preparing Calling Parameters
                object[] param = new object[] { _typecode, _typeid };
                #endregion
                #region Call Server Function
                r = _remote.Connection.Call(
                    _remote.User.UserNo
                    , CONST_FILEID
                    , 103102
                    , _tableprivselect
                    , param
                    );
                #endregion
            }
            return r;
        }

        public Result OpenFile(ulong attachid)
        {
            if (attachid == 0) attachid = ISM.Lib.Static.ToULong(lblAttachId.Text);

            Result r = AttachUtility.GetFile(_remote, _tableprivselect, attachid, "");
            if (r.ResultNo == 0 && r.AffectedRows > 0)
            {
                string filename = ISM.Lib.Static.ToStr(r.Param[0]);
                // Shell execute
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = filename;
                process.Start();
            }
            return r;
        }
        public Result UploadFile()
        {
            FormAttachViewFileAdd frm = new FormAttachViewFileAdd(_remote, _typecode, _typeid, _tableprivupdate);
            frm.ShowDialog();
            if (frm.Result != null)
            {
                string s = "";
                if (frm.Result.ResultNo == 0)
                {
                    ulong attachid = ISM.Lib.Static.ToULong(frm.Result.Param[0]);
                    s = string.Format("Файл амжилттай хадгаллаа.\r\nХавсралт файлын дугаар: {0}", attachid);
                }
                else
                {
                    s = string.Format("{0}: {1}", frm.Result.ResultNo, frm.Result.ResultDesc);
                }
                MessageBox.Show(s, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return frm.Result;
        }
        public Result DownloadFile(ulong attachid)
        {
            Result r = null;
            if (_remote != null)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Хавсралт файлыг өөрийн локал хавтаст татаж авах.";
                dialog.SelectedPath = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
                DialogResult dr = dialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    r = AttachUtility.GetFile(_remote, TablePrivSelect, attachid, dialog.SelectedPath);
                    string s = "";
                    if (r.ResultNo == 0)
                    {
                        string filename = Convert.ToString(r.Param[0]);
                        s = string.Format("Файл амжилттай хадгаллаа.\r\nХавсралт файлын зам: {0}", filename);
                    }
                    else
                    {
                        s = string.Format("{0}: {1}", r.ResultNo, r.ResultDesc);
                    }
                    MessageBox.Show(s, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return r;
        }
        public Result DeleteFile(ulong attachid, string filename)
        {
            Result r = null;
            if (_remote != null)
            {
                string msg = string.Format("Та дараах хавсралт файлыг устгахдаа итгэлтэй байна уу?\r\nХавсралт файл: {0}[{1}]",filename, attachid);
                DialogResult dr = MessageBox.Show(msg, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    r = AttachUtility.Delete(_remote, _tableprivremove, attachid);
                    string s = "";
                    if (r.ResultNo == 0)
                    {
                        s = string.Format("Файл амжилттай устгагдлаа.\r\nХавсралт файл: {0}[{1}]", filename, attachid);
                    }
                    else
                    {
                        s = string.Format("{0}: {1}", r.ResultNo, r.ResultDesc);
                    }
                    MessageBox.Show(s, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return r;
        }

        #endregion
        #region Gallary Functions

        internal void SetColumnCaption()
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Файлын нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Огноо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Төрөл");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Хэрэглэгч");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Дугаар", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Хэмжээ");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Тайлбар");
        }
        internal void InsertGallary(string filename, ulong attachid, long attachsize, int attachtype, DateTime attachdate, string attachdesc, int userno, Image image)
        {
            DevExpress.XtraBars.Ribbon.GalleryItemGroup group = galleryControl1.Gallery.Groups[0];
            DevExpress.XtraBars.Ribbon.GalleryItem item = null;

            item = new DevExpress.XtraBars.Ribbon.GalleryItem();
            group.Items.Add(item);

            item.Caption = filename;
            item.Image = image;
            item.Tag = new object[] { filename, attachid, attachsize, attachtype, attachdate, attachdesc, userno };
        }
        public void ShowGallary(enumShowType type)
        {
            Result r = null;

            if (type == 0) r = GetAttachImages();
            else r = GetAttachTable(type);

            if (r.ResultNo == 0)
            {
                gridControl1.DataSource = null;
                gridControl1.DataSource = r.Data.Tables[0];
                FormUtility.SetFormatGrid(ref gridView1, false);
                SetColumnCaption();

                #region Clear Gallary List

                galleryControl1.SuspendLayout();
                DevExpress.XtraBars.Ribbon.GalleryItemGroup group = galleryControl1.Gallery.Groups[0];
                group.Items.Clear();

                #endregion
                #region Insert Gallary List

                foreach (DataRow row in r.Data.Tables[0].Rows)
                {
                    string filename = Convert.ToString(row["filename"]);
                    DateTime attachdate = ISM.Lib.Static.ToDate(row["attachdate"]);
                    int attachtype = ISM.Lib.Static.ToInt(row["attachtype"]);
                    int userno = ISM.Lib.Static.ToInt(row["userno"]);
                    ulong attachid = ISM.Lib.Static.ToULong(row["attachid"]);
                    long attachsize = ISM.Lib.Static.ToLong(row["attachsize"]);
                    string attachdesc = Convert.ToString(row["description"]);

                    Image image = null;
                    object blob = null;
                    if (type == 0) blob = row["attachblob"];
                    if (blob != null && blob != DBNull.Value ) image = ISM.Lib.Static.ImageFromByte((byte[])blob);
                    else image = ISM.Lib.Static.ImageResize(ISM.Lib.Static.ImageFileExt(filename), 64, 64);

                    InsertGallary(filename, attachid, attachsize, (int)type, attachdate, attachdesc, userno, image);
                }
                galleryControl1.ResumeLayout();
                galleryControl1.Refresh();
                
                #endregion
            }
        }

        #endregion
    }
}
