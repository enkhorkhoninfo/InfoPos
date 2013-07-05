using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISM.CUser;
using EServ.Shared;

namespace InfoPos.Enquiry
{
    public partial class IssueContactEnquiry : Form
    {
        private Core.Core _core;
        DataRow DR;

        public IssueContactEnquiry(Core.Core core): this(core, null)
        {

        }

        public IssueContactEnquiry(Core.Core core, Object[] obj)
        {
            InitializeComponent();
            _core = core;
            DR = (DataRow)obj[0];
            Init(DR);
        }

        #region [ Үндсэн мэдээлэл ]
        public void Init(DataRow DR)
        {
            try
            {
                DataTable DT = new DataTable();
                DT.Columns.Add("Талбар", Type.GetType("System.String"));
                DT.Columns.Add("Утга", Type.GetType("System.String"));

                Function();
                ISM.Template.FormUtility.SetFormatGrid(ref gvwIssueContact, false);
                gvwIssueContact.GroupPanelText = "Бүлэглэх баганаа оруулна уу";
                //gvwReInsurer.BestFitMaxRowCount = 50;

                DT.Rows.Add("Харилцагчийн дугаар", Static.ToStr(DR["custno"]));
                DT.Rows.Add("Асуудлын дугаар", Static.ToStr(DR["issueid"]));
                DT.Rows.Add("Төслийн дугаар", Static.ToStr(DR["projectid"]));
                DT.Rows.Add("Төслийн нэр", Static.ToStr(DR["prjectname"]));
                DT.Rows.Add("Дэд төрлийн дугаар", Static.ToStr(DR["PROJECTCOMPID"]));
                DT.Rows.Add("Дэд төрлийн нэр", Static.ToStr(DR["projectcopname"]));
                DT.Rows.Add("Асуудлын төрлийн дугаар", Static.ToStr(DR["IssueTypeID"]));
                DT.Rows.Add("Асуудлын төрлийн нэр", Static.ToStr(DR["issuetypeidname"]));
                DT.Rows.Add("Асуудлын эрэмбэ ID", Static.ToStr(DR["IssuePriorID"]));
                DT.Rows.Add("Асуудлын эрэмбийн нэр", Static.ToStr(DR["issuepriorname"]));
                DT.Rows.Add("Төлөв", Static.ToStr(DR["Status"]));
                DT.Rows.Add("Төлвийн нэр", Static.ToStr(DR["statusname"]));
                DT.Rows.Add("Асуудал хаагдсан төлөв", Static.ToStr(DR["ResolutionStatus"]));
                DT.Rows.Add("Аль шатлал дээр явж байгаа эсэх", Static.ToStr(DR["TrackID"]));
                DT.Rows.Add("Аль шатлал дээр явж байгаа эсэхийн нэр", Static.ToStr(DR["trackname"]));
                DT.Rows.Add("Үүсгэсэн хэрэглэгчийн дугаар", Static.ToStr(DR["CreateUser"]));
                DT.Rows.Add("Үүсгэсэн хэрэглэгчийн нэр", Static.ToStr(DR["CreateUserName"]));
                DT.Rows.Add("Асуудал үүсгэсэн огноо, цаг минут", Static.ToStr(DR["Createdate"]));
                DT.Rows.Add("Сүүлд өөрчлөлт орсон огноо, цаг минут", Static.ToStr(DR["UpdateDate"]));
                DT.Rows.Add("Асуудал дуусгана огноо, цаг минут", Static.ToStr(DR["duedate"]));
                DT.Rows.Add("Яг одоо хариуцаж байгаа хэрэглэгч", Static.ToStr(DR["AssigneeUser"]));
                DT.Rows.Add("Яг одоо хариуцаж байгаа хэрэглэгчийн нэр", Static.ToStr(DR["AssigneeUserName"]));
                DT.Rows.Add("Асуудал хаагдсан огноо, цаг минут", Static.ToStr(DR["resolutiondate"]));
                DT.Rows.Add("Асуудал хаагдсан хэрэглэгч", Static.ToStr(DR["resolutionuser"]));
                DT.Rows.Add("Асуудал хаагдсан хэрэглэгчийн нэр", Static.ToStr(DR["resolutionuserName"]));


                grdIssueContact.DataSource = null;
                grdIssueContact.DataSource = DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Үндсэн мэдээллийг дахин ачааллах үед алдаа гарлаа" + ex.Message);
            }
        }

        void Function()
        {
            if (_core.Resource != null)
            {
                btnexitb.Image = _core.Resource.GetImage("navigate_cancel");
            }
        }
        #endregion

        private void IssueContactEnquiry_Load(object sender, EventArgs e)
        {
            gvwIssueContact.OptionsView.ColumnAutoWidth = false;
            gvwIssueContact.BestFitColumns();
        }

        private void btnexit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
