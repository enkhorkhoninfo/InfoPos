using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;

using EServ.Shared;
using System.Collections;
using ISM.Template;

namespace InfoPos.Schedule
{
    public partial class frmSchedul : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        Core.Core _core;
        int ProdType = 1;
        string _prodno = "";
        int PrivNo = 500004;
        ISM.Touch.TouchKeyboard _touchkeyboard = null;
        #endregion               
        #region[Contructure]
        public frmSchedul(Core.Core core, string Unit, int Duration, string OrderNo, long GroupNo, string ProdNo,int Count,string custname)
        {
            InitializeComponent();
            ucSchedule1.core = core;
            btnRefresh.Image = core.Resource.GetImage("navigate_refresh");

            _prodno = ProdNo;
            ucSchedule1.ProdType = 1; 
            ucSchedule1.ProdNo = ProdNo;
            ucSchedule1.LineNumber = Count;
            ucSchedule1.Duration = Duration;
            ucSchedule1.Unit = Unit;
            ucSchedule1.OrderNo = OrderNo;
            ucSchedule1.CustName = custname;
            dteToday.DateTime = core.TxnDate;
            ucSchedule1.RefreshData(1, ProdNo, dteToday.DateTime);

            ucSchedule1.StatusAdd(0, "Захиалга хийж байгаа", Color.NavajoWhite);
            ucSchedule1.StatusAdd(1, "Захиалсан", Color.DeepSkyBlue);
            ucSchedule1.StatusAdd(2, "Захиалга баталгаажсан", Color.DeepPink);
            ucSchedule1.StatusAdd(3, "Цуцлагдсан", Color.OrangeRed);
            ucSchedule1.StatusAdd(4, "Хаагдсан", Color.GreenYellow);
        }
        #endregion
        #region[Event]
        private void frmSchedul_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (dteEnd.DateTime.Date < dteStart.DateTime.Date)
            {
                MessageBox.Show("Эхлэх дуусах хугацаа алдаатай байна.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dteEnd.DateTime.DayOfYear - dteStart.DateTime.DayOfYear > 30)
            {
                MessageBox.Show("Эхлэх дуусах хугацааны интервал их байна.(<31)", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dteToday.EditValue != null)
            {
                if (dteStart.EditValue == null && dteEnd.EditValue == null)
                {
                    ucSchedule1.RefreshData(ProdType, _prodno, dteToday.DateTime);
                }
            }
            if (dteToday.EditValue != null && dteStart.EditValue != null && dteEnd.EditValue != null)
                ucSchedule1.RefreshData(ProdType, _prodno, dteToday.DateTime, dteStart.DateTime, dteEnd.DateTime);
        }
        #endregion
        private void frmSchedul_Load(object sender, EventArgs e)
        {
            string msg = "";
            ArrayList Tables = new ArrayList();
            string[] names = { "SERVMAINSHEDULE" };
            DictUtility.PrivNo = PrivNo;
            Result res = DictUtility.Get(_core.RemoteObject, names, ref Tables);
            DataTable dt = (DataTable)Tables[0];
            if (dt == null)
            {
                msg = "Dictionary-д хуваарьтай үйлчилгээний мэдээлэл оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboService, dt, "SERVID", "NAME", "", new int[]{});
                cboService.ItemIndex = 0;
            }

            dteToday.DateTime = DateTime.Now;
            dteStart.DateTime = DateTime.Now;
            dteEnd.DateTime = DateTime.Now;
        }
    }
}
