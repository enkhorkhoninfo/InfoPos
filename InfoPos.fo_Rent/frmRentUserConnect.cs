using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;

namespace InfoPos.Rent
{
    public partial class frmRentUserConnect : DevExpress.XtraEditors.XtraForm
    {
        Core.Core _core = null;
        Hashtable _hash = new Hashtable();

        #region[ Properties ]
        string _username = "";
        public string UserName
        {
            get { return _username; }
        }
        int _userno = 0;
        public int UserNo
        {
            get { return _userno; }
        }
        #endregion
        #region Constructor and events

        public frmRentUserConnect(Core.Core core, int userno)
        {
            InitializeComponent();
            _core = core;
            _userno = userno;
        }
        public frmRentUserConnect(Core.Core core)
            : this(core, 0)
        {
        }
        private void frmRentUserConnect_Load(object sender, EventArgs e)
        {
            if (_userno != 0)
            {
                numUserNo.EditValue = _userno;
                txtPass.Select();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Result res = null;

            #region Validation

            int userno = Static.ToInt(numUserNo.EditValue);
            string userpwd = Static.ToStr(txtPass.EditValue);

            if (userno == 0)
            {
                res = new Result(1000, "Хэрэглэгчийн дугаар буруу байна.");
                goto OnExit;
            }
            if (userpwd == "")
            {
                res = new Result(1001, "Хэрэглэгчийн нууц үг буруу байна.");
                goto OnExit;
            }

            #endregion

            #region Кэйшлэсэн мэдээллээс хайх

            string[] data = (string[])_hash[userno];
            if (data != null)
            {
                if (data[1] != Static.Encrypt(userpwd))
                {
                    res = new Result(1001, "Хэрэглэгчийн нууц үг буруу байна.");
                    goto OnExit;
                }
                _username = data[0];
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                return;
            }

            #endregion

            #region Нэвтрэх хүсэлт илгээх

            object[] param = new object[] { numUserNo.EditValue, txtPass.EditValue };
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 606, 606005, 110000, param);
            if (res != null && res.ResultNo != 0) goto OnExit;

            #endregion
            #region Хэрэглэгчийн мэдээлийг хадгалах
            DataTable dt = res.Data.Tables[0];
            _username = Static.ToStr(dt.Rows[0]["USERNAME"]);
            _userno = Static.ToInt(numUserNo.EditValue);
            #endregion
            #region Нэвтэрсэн нууц үгийг кэйшлэх

            string pwd = Static.ToStr(dt.Rows[0]["UPASSWORD"]);
            _hash[_userno] = new string[] { _username, pwd };

            #endregion

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}