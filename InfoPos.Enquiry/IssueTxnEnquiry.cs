using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;

namespace InfoPos.Enquiry
{

    public partial class IssueTxnEnquiry : Form
    {
     #region [ Parameter ]
        private Core.Core _core;
        private long _JrNo = 0 ;
        string appname = "", formname = "";
        #endregion
        #region [ Constructor Function ]
        public IssueTxnEnquiry(Core.Core core): this(core,0)
        {

        }

        public IssueTxnEnquiry(Core.Core core, long pJrNo)
        {
            InitializeComponent();
            _core = core;
            _JrNo = pJrNo;
            Init(_JrNo);
        }
        #endregion
        #region [ Үндсэн мэдээлэл ]
        public void Init(long pJrNo)
        {
            try
            {
                Result res = new Result();
                object[] obj = new object[1];
                obj[0] = Static.ToLong(pJrNo);

                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 223, 310112, 310112, new object[] { pJrNo });

                if (res.ResultNo != 0) { MessageBox.Show(res.ResultDesc); return; }
                if (res.Data == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0] == null) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }
                if (res.Data.Tables[0].Rows.Count == 0) { MessageBox.Show("Мэдээлэл олдсонгүй"); return; }

//                select i.jrno,i.issueid,i.txndate,i.postdate,i.userno, substr(h1.userfname, 0, 1)||'.'||h1.userlname as UserName,  i.actiontypeid,a.name as actiontypename, i.subject,i.description,i.status,decode(i.status,0,'Хэвийн гүйлгээ',1,'Хаах гүйлгээ') as statusname,
//i.resolutiontypeid,b.name as resolutiontypename, i.trackid,c.name as trackname, i.assigneeuser,substr(h1.userfname, 0, 1)||'.'||h1.userlname as assigneeuserName, i.nextpurpose,i.nextdate
                
                DataTable DT = new DataTable();
                DT.Columns.Add("Талбар", Type.GetType("System.String"));
                DT.Columns.Add("Утга", Type.GetType("System.String"));

                Function();
                ISM.Template.FormUtility.SetFormatGrid(ref gvwIssueTxn, false);
                gvwIssueTxn.GroupPanelText = "Бүлэглэх баганаа оруулна уу";

                DT.Rows.Add("Журналын дугаар", Static.ToStr(res.Data.Tables[0].Rows[0]["jrno"]));
                DT.Rows.Add("Асуудлын дугаар", Static.ToStr(res.Data.Tables[0].Rows[0]["issueid"]));
                DT.Rows.Add("Гүйлгээ оруулсан огноо", Static.ToStr(res.Data.Tables[0].Rows[0]["txndate"]));
                DT.Rows.Add("Илгээсэн огноо", Static.ToStr(res.Data.Tables[0].Rows[0]["postdate"]));
                DT.Rows.Add("Хэрэглэгчийн дугаар", Static.ToStr(res.Data.Tables[0].Rows[0]["userno"]));
                DT.Rows.Add("Хэрэглэгчийн нэр", Static.ToStr(res.Data.Tables[0].Rows[0]["UserName"]));
                DT.Rows.Add("Асуудлын төрлийн дугаар", Static.ToStr(res.Data.Tables[0].Rows[0]["actiontypeid"]));
                DT.Rows.Add("Асуудлын төрлийн нэр", Static.ToStr(res.Data.Tables[0].Rows[0]["actiontypename"]));
                DT.Rows.Add("Шалтгаан", Static.ToStr(res.Data.Tables[0].Rows[0]["subject"]));
                DT.Rows.Add("Тайлбар", Static.ToStr(res.Data.Tables[0].Rows[0]["description"]));
                DT.Rows.Add("Төлөв", Static.ToStr(res.Data.Tables[0].Rows[0]["status"]));
                DT.Rows.Add("Төлвийн нэр", Static.ToStr(res.Data.Tables[0].Rows[0]["statusname"]));
                DT.Rows.Add("Шийдвэрлэсэн төлвийн дугаар", Static.ToStr(res.Data.Tables[0].Rows[0]["resolutiontypeid"]));
                DT.Rows.Add("Шийдвэрлэсэн төлвийн нэр", Static.ToStr(res.Data.Tables[0].Rows[0]["resolutiontypename"]));
                DT.Rows.Add("Шатлалын дугаар", Static.ToStr(res.Data.Tables[0].Rows[0]["trackid"]));
                DT.Rows.Add("Шатлалын нэр", Static.ToStr(res.Data.Tables[0].Rows[0]["trackname"]));
                DT.Rows.Add("Хариуцсан хэрэглэгч", Static.ToStr(res.Data.Tables[0].Rows[0]["assigneeuser"]));
                DT.Rows.Add("Хариуцсан хэрэглэгчийн нэр", Static.ToStr(res.Data.Tables[0].Rows[0]["assigneeuserName"]));
                DT.Rows.Add("Дараачийн зорилго", Static.ToStr(res.Data.Tables[0].Rows[0]["nextpurpose"]));
                DT.Rows.Add("Дараагийн он сар өдөр", Static.ToStr(res.Data.Tables[0].Rows[0]["nextdate"]));

                //ISM.Template.FormUtility.RestoreStateGrid(appname, formname, ref gvwIssueTxn);
                grdIssueTxn.DataSource = null;
                grdIssueTxn.DataSource = DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Үндсэн мэдээллийг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }
        #endregion
        #region [ Button ]

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Function()
        {
            if (_core.Resource != null)
            {
                btnclose.Image = _core.Resource.GetImage("navigate_cancel");
            }
        }
        #endregion

        private void IssueTxnEnquiry_Load(object sender, EventArgs e)
        {
            gvwIssueTxn.OptionsView.ColumnAutoWidth = false;
            gvwIssueTxn.BestFitColumns();
        }

        private void btnclose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IssueTxnEnquiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
