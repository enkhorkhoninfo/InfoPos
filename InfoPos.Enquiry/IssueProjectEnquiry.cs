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
    public partial class IssueProjectEnquiry : Form
    {
        private Core.Core _core;
        DataRow DR;

         public IssueProjectEnquiry(Core.Core core): this(core, null)
        {

        }
         public IssueProjectEnquiry(Core.Core core, Object[] obj)
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
                ISM.Template.FormUtility.SetFormatGrid(ref gvwIssueProject, false);
                gvwIssueProject.GroupPanelText = "Бүлэглэх баганаа оруулна уу";
                //gvwReInsurer.BestFitMaxRowCount = 50;

                DT.Rows.Add("Дэд төрлийн дугаар", Static.ToStr(DR["ProjectID"]));
                DT.Rows.Add("Төлөв", Static.ToStr(DR["status"]));
                DT.Rows.Add("Нэр", Static.ToStr(DR["name"]));
                DT.Rows.Add("Нэр 2", Static.ToStr(DR["name2"]));
                DT.Rows.Add("Товч нэр", Static.ToStr(DR["ShortName"]));
                DT.Rows.Add("Товч нэр 2", Static.ToStr(DR["ShortName2"]));
                DT.Rows.Add("Төслийг хариуцагч", Static.ToStr(DR["owneruser"]));
                DT.Rows.Add("Төслийн эхлэх огноо", Static.ToStr(DR["startdate"]));
                DT.Rows.Add("Төслийн дуусах огноо", Static.ToStr(DR["enddate"]));
                DT.Rows.Add("Төслийн төрлийн ID", Static.ToStr(DR["projecttypeid"]));
                DT.Rows.Add("Үүсгэсэн хэрэглэгчийн дугаар", Static.ToStr(DR["createuserno"]));
                DT.Rows.Add("Төслийг үүсгэсэн огноо", Static.ToStr(DR["createdate"]));
                DT.Rows.Add("Мэдэгдэлийн схем ID", Static.ToStr(DR["NotifySchemaID"]));
                DT.Rows.Add("Эрхийн схем ID", Static.ToStr(DR["PermSchemaID"]));

  
                grdIssueProject.DataSource = null;
                grdIssueProject.DataSource = DT;
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
                btnexit.Image = _core.Resource.GetImage("navigate_cancel");
            }
        }
        #endregion

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IssueProjectEnquiry_Load(object sender, EventArgs e)
        {
            gvwIssueProject.OptionsView.ColumnAutoWidth = false;
            gvwIssueProject.BestFitColumns();
        }

        private void IssueProjectEnquiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnexit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
