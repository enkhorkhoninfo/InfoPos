using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EServ.Shared;

namespace ISM.Template
{
    public partial class FormDynamicStep : Form
    {
        #region Internal Variables
        CUser.Remote _remote = null;
        Resource _resource = null;
        int _typecode = 0;
        ulong _typeid = 0;
        int _stepid = 0;
        int _stepitemid = 0;
        int _privno = 0;

        int _owner = 0;
        string _ownername = "";
        double _performance = 0;
        DateTime _started;
        DateTime _finished;
        int _status = 0;
        #endregion
        #region Constractor
        public FormDynamicStep(CUser.Remote remote, Resource resource, int privno, int typecode, ulong typeid, int stepid, int stepitemid, int owner, string ownername, double perf, DateTime started, DateTime finished, int status)
        {
            InitializeComponent();

            _remote = remote;
            _resource = resource;

            _privno = privno;
            _typecode = typecode;
            _typeid = typeid;
            _stepid = stepid;
            _stepitemid = stepitemid;

            _owner = owner;
            _ownername = ownername;
            _performance = perf;
            _started = started;
            _finished = finished;
            _status = status;
        }
        #endregion
        #region Control Events
        private void FormDynamicStep_Load(object sender, EventArgs e)
        {
            if (_resource != null)
            {
                btnClear.Image = _resource.GetImage("navigate_refresh");
                btnEnter.Image = _resource.GetImage("navigate_edit");
            }

            txtStepId.Text = ISM.Lib.Static.ToStr(_stepitemid);
            numPerformance.Value = ISM.Lib.Static.ToDecimal(_performance);
            if (_started != DateTime.MinValue)
            {
                txtOwner.Text = string.Format("{0} - {1}", _owner, _ownername);
                txtStarted.Text = _started.ToString("yyyy.MM.dd");
                txtFinished.Text = _finished != DateTime.MinValue ? _finished.ToString("yyyy.MM.dd") : "";
                if (_performance == 100.0D)
                {
                    if (_finished > DateTime.MinValue)
                        txtDays.Text = _finished.Subtract(_started).TotalDays.ToString("#,##0");
                }
                else
                {
                    if (_started > DateTime.MinValue)
                        txtDays.Text = DateTime.Now.Subtract(_started).TotalDays.ToString("#,##0");
                }
            }

            DataTable dt = null;
            Result r = ISM.Template.DictUtility.Get(_remote, "USERS", _privno, ref dt);
            if (r.ResultNo == 0)
            {
                ISM.Template.FormUtility.LookUpEdit_SetList(ref lueNewOwner, dt, "USERNO", "USERLNAME");
            }
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            #region Validation
                        
            int newowner = ISM.Lib.Static.ToInt(lueNewOwner.EditValue);
            if (_owner == 0)
            {
                _owner = _remote.User.UserNo;
                _started = DateTime.Now;
            }
            if (numProgress.Value == 0 && newowner == 0 && string.IsNullOrEmpty(txtComment.Text))
            {
                MessageBox.Show(string.Format("Өгөгдөл оруулаагүй байна!"));
                return;
            }
            if (numProgress.Value != 0 && _owner != _remote.User.UserNo)
            {
                MessageBox.Show(string.Format("Хариуцагчаас бусад хүн ахиц оруулах боломжгүй!"));
                return;
            }
            if (newowner != 0 && _owner != _remote.User.UserNo)
            {
                MessageBox.Show(string.Format("Хариуцагчаас бусад хүн хариуцагч оруулах боломжгүй!"));
                return;
            }
            double d = _performance + ISM.Lib.Static.ToDouble(numProgress.Value);
            if (d > 100.0D)
            {
                MessageBox.Show(string.Format("Нийт гүйцэтгэл 100% утгаас их байна!"));
                return;
            }
            if (d < 0.0D)
            {
                MessageBox.Show(string.Format("Нийт гүйцэтгэл тэгээс бага утгатай байна!"));
                return;
            }

            #endregion
            Result r = Save();
            if (r != null && r.ResultNo != 0)
            {
                MessageBox.Show(string.Format("Мэдээлэл оруулахад алдаа гарлаа.\r\nАлдааны дугаар: {0}\r\n{1}", r.ResultNo, r.ResultDesc));
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

            /*
            txtComment.Text = "";
            numProgress.Value = 0;
            lueNewOwner.EditValue = null;

            ReadHistory();
             */
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            numProgress.Value = 0;
            lueNewOwner.EditValue = null;
            txtComment.Text = "";
        }
        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            if (e.PageIndex == 1 && !readhistory)
            {
                ReadHistory();
                readhistory = true;
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            object obj = gridView1.GetFocusedRowCellValue("NOTE");
            if (obj != null)
                txtHistComment.Text = Convert.ToString(obj);
        }
        #endregion
        #region Server Functions
        bool readhistory = false;
        public Result ReadHistory()
        {
            Result r = null;

            object[] param = new object[] { _typecode, _typeid, _stepitemid };
            r = _remote.Connection.Call(_remote.User.UserNo, 108, 108401, _privno, param);
            if (r.ResultNo == 0)
            {
                gridControl1.DataSource = r.Data.Tables[0];
                gridView1.PopulateColumns();
                FormatGrid();
            }
            return r;
        }
        public Result Save()
        {
            Result r = null;

            int newowner = ISM.Lib.Static.ToInt(lueNewOwner.EditValue);
            double progress = ISM.Lib.Static.ToDouble(numProgress.Value);
            double d = _performance + progress;

            object[] param = new object[]{
                 _typecode
                , _typeid
                , _stepid
                , _stepitemid
                , _owner
                , d
                , _started
                , d >= 100.0D ? DateTime.Today : DateTime.MinValue
                , d >= 100.0D ? 1 : 0

                , DateTime.Today
                , progress
                , txtComment.Text
                , newowner
            };
            
            r = _remote.Connection.Call(_remote.User.UserNo, 108, 108303, _privno, param);

            return r;
        }
        private void FormatGrid()
        {
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Id", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Хариуцагч", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Эхэлсэн", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Хэрэглэгч");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Огноо");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Ахиц%");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Гүйцэтгэл%");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Тайлбар");

            DataTable dt = null;
            Result r = ISM.Template.DictUtility.Get(_remote, "USERS", _privno, ref dt);
            if (r.ResultNo == 0)
            {
                ISM.Template.FormUtility.Column_SetList(ref gridView1, 3, dt, "USERNO", "USERFNAME");
            }
            ISM.Template.FormUtility.SetFormatGrid(ref gridView1, false);

            gridView1.Columns[4].DisplayFormat.FormatString = "G"; //"yyyy.MM.dd HH:mm:ss";
            gridView1.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        }
        #endregion
    }
}
