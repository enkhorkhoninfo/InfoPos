using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

using EServ.Shared;
using ISM.Template;

namespace InfoPos.Schedule
{
    public partial class frmDay : DevExpress.XtraEditors.XtraForm
    {
        Core.Core _core;
        int _type = 0;
        DateTime _date;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="core">Core</param>
        /// <param name="row">Календар дээрх мөр</param>
        /// <param name="date">Хэдний өдөр болох</param>
        /// <param name="type">Төрөл(нэмэх,засах)</param>
        public frmDay(Core.Core core, DataRow row, DateTime date, int type, DataTable Data)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
                btnClose.Image = _core.Resource.GetImage("image_exit");
            }
            InitCombo();
                imgcbDayWheType.Properties.SmallImages = frmCalendar.image;
                imgcbNightWheType.Properties.SmallImages = frmCalendar.image;
                int i = 0;
                foreach (DataRow dr in Data.Rows)
                {
                    i++;
                    imgcbDayWheType.Properties.Items.Add(new ImageComboBoxItem(Static.ToStr(dr["DESCRIPTION"]), Static.ToInt(dr["WEATHERID"]), i));
                    imgcbNightWheType.Properties.Items.Add(new ImageComboBoxItem(Static.ToStr(dr["DESCRIPTION"]), Static.ToInt(dr["WEATHERID"]), i));
                }
            _type = type;
            dteStartDay.EditValue = date.ToString(string.Format("yyyy.M.{0}", row["DayDATE"]));
            dteEndDate.EditValue = dteStartDay.EditValue;

            if (_type == 1)
            {
                dteStartDay.Properties.ReadOnly = true;
                FormUtility.LookUpEdit_SetValue(ref cboDayType, Static.ToInt(row["DayTYPE"]));
                imgcbDayWheType.EditValue = Static.ToInt(row["DayWeatherType"]);
                numDayTemp.EditValue = row["DayTemperature"];
                imgcbNightWheType.EditValue = row["NightWeatherType"];
                numNightTemp.EditValue = row["NightTemperature"];
            }
        }
        private void InitCombo()
        {
            Result res = new Result();
            ArrayList Tables = new ArrayList();
            DataTable DT = null;
            string msg = "";

            DictUtility.PrivNo = 140206;

            string[] names = new string[] { "PADAYTYPE"};
            res = DictUtility.Get(_core.RemoteObject, names, ref Tables);

            DT = (DataTable)Tables[0];
            if (DT == null)
            {
                msg = "Dictionary-д PADAYTYPE оруулаагүй байна-" + res.ResultDesc;
            }
            else
            {
                FormUtility.LookUpEdit_SetList(ref cboDayType, DT, "DAYTYPE", "DESCRIPTION");
            }
            if (msg != "")
                MessageBox.Show(msg);
            cboDayType.ItemIndex = 0;
        }
        private bool Validate()
        {
            string controlalert="Доорх талбаруудын утгыг бүрэн оруулна уу";
            if(dteStartDay.EditValue==null)
                controlalert=controlalert+"\r\n"+"Эхлэх огноо оруулна уу.";
            if(dteEndDate.EditValue==null)
                controlalert=controlalert+"\r\n"+"Дуусах огноо оруулна уу.";
            if(cboDayType.EditValue==null)
                controlalert=controlalert+"\r\n"+"Өдрийн төрөл сонгоно уу.";
            if(numDayTemp.EditValue==null)
                controlalert=controlalert+"\r\n"+"Өдрийн хэм оруулна уу.";
            if(imgcbDayWheType.EditValue==null)
                controlalert=controlalert+"\r\n"+"Өдрийн цаг агаарын төрөл сонгоно уу.";
            if(numDayTemp.EditValue==null)
                controlalert=controlalert+"\r\n"+"Шөнийн хэм оруулна уу.";
            if(imgcbDayWheType.EditValue==null)
                controlalert=controlalert+"\r\n"+"Шөнийн цаг агаарын төрөл сонгоно уу.";
            if(controlalert!="Доорх талбаруудын утгыг бүрэн оруулна уу")
            {
                MessageBox.Show(this, controlalert, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dteStartDay.DateTime <= dteEndDate.DateTime)
            {
                if (dteEndDate.DateTime > dteStartDay.DateTime.AddDays(DateTime.DaysInMonth(_date.Year, _date.Month)))
                {
                    MessageBox.Show(this, "Эхлэх дуусах огнооны интервал хэтэрсэн байна.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
                }
            }
            else
            {
                MessageBox.Show(this, "Эхлэх дуусах огноо алдаатай байна.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (_type == 0)
                {
                    Result res = new Result();
                    object[] obj = new object[7];
                    obj[0] = Convert.ToDateTime(dteStartDay.EditValue);
                    obj[1] = Convert.ToDateTime(dteEndDate.EditValue);
                    obj[2] = cboDayType.EditValue;
                    obj[3] = numDayTemp.EditValue;
                    obj[4] = imgcbDayWheType.EditValue;
                    obj[5] = numNightTemp.EditValue;
                    obj[6] = imgcbNightWheType.EditValue;

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 228, 140208, 140208, obj);
                    if (res.ResultNo == 0 || res.ResultNo == 9110039)
                    {
                        if (res.ResultDesc != "")
                            MessageBox.Show(this, "Доорх огноо дээр өмнө нь бичлэг үүссэн байна." + res.ResultDesc, "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            this.Close();
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                    }
                }
                else
                {
                    Result res = new Result();
                    object[] obj = new object[7];
                    obj[0] = Convert.ToDateTime(dteStartDay.EditValue);
                    obj[1] = Convert.ToDateTime(dteEndDate.EditValue);
                    obj[2] = cboDayType.EditValue;
                    obj[3] = numDayTemp.EditValue;
                    obj[4] = imgcbDayWheType.EditValue;
                    obj[5] = numNightTemp.EditValue;
                    obj[6] = imgcbNightWheType.EditValue;

                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 228, 140209, 140209, obj);
                    if (res.ResultNo == 0)
                    {
                        if (res.ResultDesc != "")
                            MessageBox.Show(this, "Доорх огноо дээр өмнө нь бичлэг үүсээгүй тул засах боломжгүй." + res.ResultDesc, "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            this.Close();
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
    }
}