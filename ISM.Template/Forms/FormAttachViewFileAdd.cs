using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;
using System.IO;

namespace ISM.Template
{
    public partial class FormAttachViewFileAdd : Form
    {
        #region Local Variables
        private ISM.CUser.Remote _remote = null;
        private int _typecode = 0;
        private string _typeid = "";
        int _privupdate = 0;
        #endregion

        #region Properties
        private Result _result = null;
        public Result Result
        {
            get { return _result; }
        }
        #endregion

        #region Constractor
        public FormAttachViewFileAdd(ISM.CUser.Remote remote, int typecode, string typeid, int privupdate)
        {
            InitializeComponent();

            _remote = remote;
            _typecode = typecode;
            _typeid = typeid;

            _privupdate = privupdate;
        }
        #endregion
        #region Control Events
        private void FormAttachViewFileAdd_Load(object sender, EventArgs e)
        {
            if (_remote != null)
            {
                numUserNo.Value = _remote.User.UserNo;
                txtUserName.Text = _remote.User.UserFName;
                dteAttached.EditValue = DateTime.Now;
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofile = new OpenFileDialog();
            ofile.Title = "Файл сонгох";
            DialogResult d = ofile.ShowDialog();
            if (d != DialogResult.OK) return;

            txtFileName.Text = ofile.FileName;

            #region Check File Type
            int p = ofile.FileName.LastIndexOf('.');
            if (p > 0)
            {
                string ext = ofile.FileName.Substring(p + 1);
                switch (ext.ToLower())
                {
                    case "jpg":
                    case "jpeg":
                    case "jpe":
                    case "bmp":
                    case "png":
                    case "gif":
                        radType.EditValue = 0;
                        break;
                    case "doc":
                    case "docx":
                    case "xls":
                    case "xlsx":
                    case "ppt":
                    case "pptx":
                    case "rtf":
                    case "txt":
                        radType.EditValue = 1;
                        break;
                    default:
                        radType.EditValue = 2;
                        break;
                }
            }
            #endregion
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region Validating
            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageBox.Show(string.Format("Файл сонгогдоогүй байна!"));
                return;
            }
            #endregion

            int attachtype = ISM.Lib.Static.ToInt(radType.EditValue);
            _result = AttachUtility.SaveFromFile(_remote, _privupdate, 0, attachtype, txtFileName.Text, txtDesc.Text, _typecode, _typeid);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _result = new Result(1, "Operation is cancelled.");
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
