using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;

using ISM.Touch;
using EServ.Shared;
namespace InfoPos.fo_Customer
{
    public partial class frmCustomer : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        ISM.Touch.TouchKeyboard _kb;
        string customerid;
        string _layoutfilename;
        object[] OldValue;

        int classcode = 0;
        DataTable _dtCustType = null;

        #endregion

        #region[Constructure]
        public frmCustomer(InfoPos.Core.Core core,string CustomerNo,string ClassCode, string Name1, string Name2, string CorporateName, string RegisterNo)
        {
            InitializeComponent();
            _core = core;            
            Inviduale();
            if (CustomerNo == "")
            {
                Inviduale();
                btnRetail.ForeColor = Color.OrangeRed;
                txtName1.EditValue = Name1;
                txtName2.EditValue = Name2;
                txtCorpName.EditValue = CorporateName;
                txtRegisterNo.EditValue = RegisterNo;
            }
            if (_core.Resource != null)
            {
                btnSave.Image = _core.Resource.GetImage("object_save");
                btnExit.Image = _core.Resource.GetImage("image_exit");
                btnRetail.Image = _core.Resource.GetImage("issue_assign");
                btnCorprate.Image = _core.Resource.GetImage("menu_office");
            }
            _kb = new TouchKeyboard();
        }
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            _kb.Enable = true;
            _kb.AddToKeyboard(txtName1);
            _kb.AddToKeyboard(txtName2);
            _kb.AddToKeyboard(txtRegisterNo);
            _kb.AddToKeyboard(txtMobileNo);
            _kb.AddToKeyboard(txtFootSize);
            _kb.AddToKeyboard(txtHeight);
            _kb.AddToKeyboard(txtCorpName);

            /****************************************
             * Dictionary Table:
             * TypeCode
             * ClassCode
             * Name
             ****************************************/
            ISM.Template.DictUtility.Get(_core.RemoteObject, "CUSTOMERTYPECODE", ref _dtCustType);

            btnRetail_Click(null, null);
        }
        void Inviduale()
        {
            DataTable dt = null;
            if (_dtCustType != null)
            {
                var query = _dtCustType.AsEnumerable().Where(x => Static.ToInt(x["CLASSCODE"]) == 0).Select(x => x);
                if (query != null && query.Count()>0)
                {
                    dt = query.CopyToDataTable();
                }
            }
            ISM.Template.FormUtility.LookUpEdit_SetList(ref lueCustType, dt, "TYPECODE", "NAME");
        }
        void Corporate()
        {
            DataTable dt = null;
            var query = _dtCustType.AsEnumerable().Where(x => Static.ToInt(x["CLASSCODE"]) == 1).Select(x => x);
            if (query != null && query.Count() > 0)
            {
                dt = query.CopyToDataTable();
            }
            ISM.Template.FormUtility.LookUpEdit_SetList(ref lueCustType, dt, "TYPECODE", "NAME");
        }
        #endregion

        #region[BTN]
        private void btnRetail_Click(object sender, EventArgs e)
        {
            if (txtCustNo.EditValue == null || txtCustNo.Text == "")
            {
                classcode = 0;
                pnlDetails.PanelVisibility = SplitPanelVisibility.Panel2;

                btnCorprate.ForeColor = Color.Black;
                btnRetail.ForeColor = Color.OrangeRed;
                Inviduale();
            }
        }
        private void btnCorprate_Click(object sender, EventArgs e)
        {
            if (txtCustNo.EditValue == null || txtCustNo.Text == "")
            {
                classcode = 1;
                pnlDetails.PanelVisibility = SplitPanelVisibility.Panel1;

                btnCorprate.ForeColor = Color.OrangeRed;
                btnRetail.ForeColor = Color.Black;
                Corporate();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validate() == "Дараах талбаруудыг гүйцэт бөглөнө үү")
            {
                Save();
            }
            else MessageBox.Show(Validate(), "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region[Function]
        private string Validate()
        {
            string msg = "Дараах талбаруудыг гүйцэт бөглөнө үү";
            if (btnRetail.ForeColor == Color.OrangeRed)
            {
                if (txtName2.EditValue == null || txtName2.Text == "")
                {
                    msg = msg + "\r\nХариалцагчийн нэр оруулна уу.";
                }
            }
            else
            {
                if (txtCorpName.EditValue == null || txtCorpName.Text == "")
                {
                    msg = msg + "\r\nБайгууллагын нэр оруулна уу.";
                }
            }
            if (btnRetail.ForeColor == Color.OrangeRed)
            {
                if (txtRegisterNo.EditValue == null || txtRegisterNo.Text == "")
                {
                    msg = msg + "\r\nРегистер оруулна уу.";
                }
            }
            else
            {
                if (txtRegisterNo.EditValue == null || txtRegisterNo.Text == "")
                {
                    msg = msg + "\r\nУлсын бүртгэлийн дугаар оруулна уу.";
                }
            }
            return msg;
        }
        private void Save()
        {
            Result res = new Result();
            object[] obj = new object[15];
            string msg = "";
            try
            {
                #region[Утга]
                obj[0] = txtCustNo.EditValue;                               //CustomerNo
                obj[1] = classcode;                                             //ClassCode
                obj[2] = classcode==0 ? txtName1.EditValue : "";                            //FirstName
                obj[3] = classcode==0 ? txtName2.EditValue : "";                            //LastName
                obj[4] = classcode==1 ? txtCorpName.EditValue : "";                         //CorporateName
                obj[5] = txtRegisterNo.EditValue;                          //RegisterNo
                obj[6] = rdgSex.EditValue;
                obj[7] = txtMobileNo.EditValue;                            //Mobile
                if (txtCustNo.EditValue == null || txtCustNo.Text == "")
                {
                    obj[8] = dteCreateDate.EditValue = _core.TxnDate;                  //CreateTime
                    obj[9] = txtCreateUser.EditValue = _core.RemoteObject.User.UserNo; //CreateUser
                }
                else
                {
                    obj[8] = dteCreateDate.EditValue;                  //CreateTime
                    obj[9] = txtCreateUser.EditValue; //CreateUser
                }

                obj[10] = Static.ToDecimal(txtHeight.EditValue);          //Foot
                obj[11] = Static.ToDecimal(txtFootSize.EditValue);          //Foot
                obj[12] = Static.ToInt(lueCustType.EditValue);
                obj[13] = txtContractNo.EditValue;
                obj[14] = 0;

                #endregion
                object[] FieldName = { "CustomerNo", "ClassCode", "FirstName"
                                       , "LastName", "CorporateName", "RegisterNo"
                                       , "Sex", "Mobile", "CreateDate"
                                       , "CreateUser", "Height", "Foot"
                                       , "TypeCode", "MemberContractNo", "MemberType"};

                if (txtCustNo.EditValue == null || txtCustNo.Text == "")
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120002, 120002, new object[] { obj, FieldName, 1 });
                    msg = "Амжилттай нэмлээ .";
                }
                else
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 205, 120003, 120003, new object[] { obj, OldValue, FieldName, 1 });
                    msg = "Амжилттай засварлалаа .";
                }
                if (res.ResultNo == 0)
                {
                    if (txtCustNo.EditValue == null || txtCustNo.Text == "")
                    txtCustNo.EditValue = Static.ToStr(res.Param[0]);
                    this.Close();
                    MessageBox.Show(msg);
                }
                else
                {
                    MessageBox.Show(Static.ToStr(res.ResultNo) + " " + res.ResultDesc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }



    }
}