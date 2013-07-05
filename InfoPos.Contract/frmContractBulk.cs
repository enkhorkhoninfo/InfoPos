using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using EServ.Shared;
using NativeExcel;
using ISM.Template;

namespace InfoPos.Contract
{
    public partial class frmContractBulk : Form
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        DataTable _DT;
        string _layout = "";
        #endregion

        #region[ Init ]
        public frmContractBulk(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            _layout = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
        }
        private void frmContractBulk_Load(object sender, EventArgs e)
        {
            gvwContractMain.OptionsBehavior.ReadOnly = true;
            gvwContractMain.OptionsBehavior.Editable = false;
            gvwContractMain.OptionsCustomization.AllowGroup = false;
            gvwContractMain.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gvwContractMain.OptionsView.ColumnAutoWidth = false;
            gvwContractMain.OptionsView.ShowIndicator = false;
            gvwContractMain.OptionsView.RowAutoHeight = true;
            gvwContractMain.Appearance.Row.Font = new Font("Tahoma", 10.0F);

            initCombo();

            _layout = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);
        }
        private void frmContractBulk_FormClosed(object sender, FormClosedEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gvwContractMain, _layout);
        }
        private void initCombo()
        {
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 0, "Гэрээний үндсэн мэдээлэл");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 1, "Гэрээнд хамаарагдах бүтээгдэхүүнүүд");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 2, "Гэрээний төлбөрийн төрөл ба дансны бүртгэл");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboType, 3, "Гэрээний дүнг элэгдүүлэх хуваарь");

            ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboType, 0);
        }
        #endregion
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFile.Title = "Ханшийн файл сонгох";
            OpenFile.FileName = "";
            OpenFile.Filter = "Excel Worksheets|*.xls";
            OpenFile.ShowDialog();

            if (OpenFile.FileName != "")
            {
                txtFileName.Text = OpenFile.FileName;
                Browse(txtFileName.Text);
            }
        }
        public void Browse(string strFileName)
        {
            try
            {
                if (!File.Exists(strFileName))
                {
                    MessageBox.Show("Файл олдсонгүй!", "Файл татах", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                IWorkbook book = Factory.OpenWorkbook(strFileName);
                if (book != null)
                {
                    _DT = book.Worksheets[1].UsedRange.GetDataTable(false, true);
                    grdContractMain.DataSource = null;
                    grdContractMain.DataSource = _DT;
                    switch (Static.ToInt(cboType.EditValue))
                    {
                        case 0 : setDataMain(); break;
                        case 1 : setDataProd(); break;
                        case 2 : setDataAcnt(); break;
                        case 3: setDataSchedule(); break;
                    }
                    
                }
                else
                    MessageBox.Show("Файл уншихад алдаа гарлаа");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void setDataMain()
        {
            gvwContractMain.Columns[0].Caption = "Гэрээний төрөл";
            gvwContractMain.Columns[0].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[1].Caption = "Харилцагчийн дугаар";
            gvwContractMain.Columns[1].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[2].Caption = "Гэрээ хүчин төгөлдөр болох өдөр";
            gvwContractMain.Columns[2].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[3].Caption = "Гэрээ үйлчлэх цаг";
            gvwContractMain.Columns[3].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[4].Caption = "Гэрээ дуусах өдөр";
            gvwContractMain.Columns[4].OptionsColumn.AllowEdit = false;

            gvwContractMain.Columns[5].Caption = "Гэрээ дуусах цаг";
            gvwContractMain.Columns[5].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[6].Caption = "Гэрээний үнийн дүн";
            gvwContractMain.Columns[6].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[7].Caption = "Валют";
            gvwContractMain.Columns[7].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[8].Caption = "Балансын төрөл";
            gvwContractMain.Columns[8].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[9].Caption = "Гэрээнд хамрагдах үйчлүүлэгчийн тоо";
            gvwContractMain.Columns[9].OptionsColumn.AllowEdit = false;

            gvwContractMain.Columns[10].Caption = "Гэрээний дүнг элэгдүүлэх давтамж";
            gvwContractMain.Columns[10].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[11].Caption = "Гэрээний дүнг элэгдүүлэх дүн.";
            gvwContractMain.Columns[11].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[12].Caption = "Хариуцсан хэрэглэгч";
            gvwContractMain.Columns[12].OptionsColumn.AllowEdit = false;
        }
        private void setDataProd()
        {
            gvwContractMain.Columns[0].Caption = "Гэрээний дугаар";
            gvwContractMain.Columns[0].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[1].Caption = "Бүтээгдэхүүний дугаар";
            gvwContractMain.Columns[1].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[2].Caption = "Бүтээгдэхүүний төрөл";
            gvwContractMain.Columns[2].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[3].Caption = "Үнэ";
            gvwContractMain.Columns[3].OptionsColumn.AllowEdit = false;
        }
        private void setDataAcnt()
        {
            gvwContractMain.Columns[0].Caption = "Гэрээний дугаар";
            gvwContractMain.Columns[0].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[1].Caption = "Төлбөрийн төрлийн дугаар";
            gvwContractMain.Columns[1].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[2].Caption = "Уг т.төрлөөр төлбөр төлөх дансны дугаар";
            gvwContractMain.Columns[2].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[3].Caption = "Дансны нэр";
            gvwContractMain.Columns[3].OptionsColumn.AllowEdit = false;
        }
        private void setDataSchedule()
        {
            gvwContractMain.Columns[0].Caption = "Гэрээний дугаар";
            gvwContractMain.Columns[0].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[1].Caption = "Элэгдүүлэх огноо";
            gvwContractMain.Columns[1].OptionsColumn.AllowEdit = false;
            gvwContractMain.Columns[2].Caption = "Элэгдүүлэх дүн";
            gvwContractMain.Columns[2].OptionsColumn.AllowEdit = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable _DT = (DataTable)grdContractMain.DataSource;
            Result r = new Result();
            string pPrefix = "";
            try
            {
                pPrefix = txtAutoNumPrefix.Text.Trim();
                if (_DT != null)
                {
                    r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 207, 130022, 130022, new object[] { cboType.EditValue, _DT, _core.RemoteObject.User.UserNo, _core.TxnDate, txtRowNo.EditValue, pPrefix });
                    if (r.ResultNo == 0)
                    {
                        MessageBox.Show("Амжилтай нэмэгдлээ");
                    }
                    else
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
