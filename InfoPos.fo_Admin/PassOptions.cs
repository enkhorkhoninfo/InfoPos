using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;

using ISM.Template;

namespace HeavenPro.Admin
{
    public partial class PassOption : Form
    {
        #region[variables]
        InfoPos.Core.Core _core;
        DataRow _dr = null;
        string appname = "", formname = "";
        Form FormName = null;
        object[] oldValue = new object[14];
        object[] newValue = new object[14];
        object[] fieldValue = new object[14];
        #endregion

        #region[Functions]
        public PassOption(HeavenPro.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            InitCombo();
            lod();
        }
        void InitCombo()
        {
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboMaskType, 0, "Тогтмол");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboMaskType, 1, "Тогтмол биш");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboMaskCreate, 0, "Маскны дагуу гараар үүсгэх");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboMaskCreate, 1, "Маскны дагуу Random-оор үүсгэх");
            ISM.Template.FormUtility.LookUpEdit_SetList(ref cboMaskCreate, 2, "Default нууц үгээр");                      
        }
        void lod()
        {
            Result res = new Result();
            res = _core.moRemote.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110113, 110113, null);
            try
            {
                if (res.ResultNo == 0)
                {
                    _dr = res.Data.Tables[0].Rows[0];
                    ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboMaskType, Static.ToInt(_dr["MASKTYPE"]));
                    ISM.Template.FormUtility.LookUpEdit_SetValue(ref cboMaskCreate, Static.ToStr(_dr["CREATETYPE"]));
                    txtMaskValue.EditValue = Static.ToStr(_dr["MASKVALUE"]);
                    txtMaskDesc.EditValue = Static.ToStr(_dr["MASKDESCRIPTION"]);
                    txtMaskDft.EditValue = Static.ToStr(_dr["DEFAULTPASS"]);
                    numDuration.EditValue = Static.ToStr(_dr["VALIDDAY"]);
                    numErrorValue.EditValue = Static.ToStr(_dr["WRONGCOUNT"]);
                    numHistory.EditValue = Static.ToStr(_dr["HISTORYCOUNT"]);
                    txtServername.EditValue = Static.ToStr(_dr["SERVERNAME"]);
                    txtServerport.EditValue = Static.ToStr(_dr["SERVERPORT"]);
                    txtMailuname.EditValue = Static.ToStr(_dr["MAILUSERNAME"]);
                    txtMailpass.EditValue = Static.ToStr(_dr["MAILUSERPASS"]);
                    txtFromuser.EditValue = Static.ToStr(_dr["FROMUSER"]);
                    checkBox1.Text = Static.ToStr(_dr["isSendMail"]);
                    if (Static.ToStr(checkBox1.Text) == Static.ToStr(1))
                    {
                        checkBox1.Text = "Илгээнэ";
                        checkBox1.Checked = true;
                        txtServername.Properties.ReadOnly = false;
                        txtServerport.Properties.ReadOnly = false;
                        txtFromuser.Properties.ReadOnly = false;
                        txtMailpass.Properties.ReadOnly = false;
                        txtMailuname.Properties.ReadOnly = false;
                    }
                    else
                    {
                        checkBox1.Text = "Илгээхгүй";
                        txtServername.Properties.ReadOnly = true;
                        txtServerport.Properties.ReadOnly = true;
                        txtFromuser.Properties.ReadOnly = true;
                        txtMailpass.Properties.ReadOnly = true;
                        txtMailuname.Properties.ReadOnly = true;
                    }
                }
                oldValue[0] = cboMaskType.EditValue;
                oldValue[1] = txtMaskValue.EditValue;
                oldValue[2] = txtMaskDesc.EditValue;
                oldValue[3] = txtMaskDft.EditValue;
                oldValue[4] = cboMaskCreate.EditValue;
                oldValue[5] = numDuration.EditValue;
                oldValue[6] = numErrorValue.EditValue;
                oldValue[7] = numHistory.EditValue;
                oldValue[8] = txtServername.EditValue;
                oldValue[9] = txtServerport.EditValue;
                oldValue[10] = txtMailuname.EditValue;
                oldValue[11] = txtMailpass.EditValue;
                oldValue[12] = txtFromuser.EditValue;
                oldValue[13] = checkBox1.Checked ? 1 : 0;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PassOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
        }
        string CreatePassWord(string Mask)
        {
            string abc = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string num = "0123456789";
            string other = "*$-+?_&=!%{}/";
            string PW = "";

            object[] obj = Word(Mask);
            string[] Value = (string[])obj[0];
            int[] Length = (int[])obj[1];
            int n = (int)obj[2];
            Random rand = new Random();
            int RandVal = new int();
            for (int i = 0; i < n; i++)
            {
                switch (Value[i])
                {
                    case "a-z":
                        for (int j = 1; j <= Length[i]; j++)
                        {
                            RandVal = rand.Next(0, 25);
                            PW = PW + abc[RandVal];
                        }
                        break;
                    case "a-Z":
                        for (int j = 1; j <= Length[i]; j++)
                        {
                            RandVal = rand.Next(0, 51);
                            PW = PW + abc[RandVal];
                        }
                        break;
                    case "A-Z":
                        for (int j = 1; j <= Length[i]; j++)
                        {
                            RandVal = rand.Next(26, 51);
                            PW = PW + abc[RandVal];
                        }
                        break;
                    case "0-9":
                        for (int j = 1; j <= Length[i]; j++)
                        {
                            RandVal = rand.Next(0, 9);
                            PW = PW + num[RandVal];
                        }
                        break;
                    case ".+":
                        for (int j = 1; j <= Length[i]; j++)
                        {
                            RandVal = rand.Next(0, 12);
                            PW = PW + other[RandVal];
                        }
                        break;
                }

            }


            return PW;
        }
        object[] Word(string Mask)
        {
            int Count = 0;
            for (int i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] == '[') Count++;
            }

            string[] Value = new string[Count];
            int[] Length = new int[Count];
            int j = 0;

            for (int i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] == '[')
                {
                    if (Mask[i + 4] == ']')
                    {
                        Value[j] = Mask.Substring(i + 1, 3);
                        int k = i + 6;
                        string Num = "";
                        for (; ; )
                        {
                            if (Mask[k] == '}')
                            {
                                break;
                            }
                            else
                            {
                                Num = Num + Mask[k].ToString();
                                k++;
                            }
                        }
                        Length[j] = Convert.ToInt16(Num);
                    }
                    if (Mask.Length - 1 == '}')
                    {
                        string k = Static.ToStr(Mask.Length - 1);

                        if (Mask.Length - 1 == 's')
                        {
                            MessageBox.Show("true");
                        }
                    }
                    if (Mask[i + 3] == ']')
                    {
                        Value[j] = Mask.Substring(i + 1, 2);
                        int k = i + 5;
                        string Num = "";
                        for (; ; )
                        {
                            if (Mask[k] == '}')
                            {
                                break;
                            }
                            else
                            {
                                Num = Num + Mask[k].ToString();
                                k++;
                            }
                        }
                        Length[j] = Convert.ToInt16(Num);
                    }
                    j++;
                }
            }
            object[] obj = { Value, Length, j };
            return obj;
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(6, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(4, false));
            return builder.ToString();
        }
        #endregion

        #region[Event]
        private void btnClose_Click(object sender, EventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            this.Close();
        }
        private void Save_Click(object sender, EventArgs e)
        {           
            try
            {
                String msg;
                                            
                    if (Static.ToInt(cboMaskCreate.EditValue) == 1 )
                    {
                        txtMaskDft.EditValue = CreatePassWord(txtMaskValue.Text);                        
                    }
                    newValue[0] = Static.ToInt(cboMaskType.EditValue);
                    newValue[1] = Static.ToStr(txtMaskValue.EditValue);
                    newValue[2] = Static.ToStr(txtMaskDesc.EditValue);
                    newValue[3] = Static.ToStr(txtMaskDft.EditValue);
                    newValue[4] = Static.ToStr(cboMaskCreate.EditValue);
                    newValue[5] = Static.ToInt(numDuration.EditValue);
                    newValue[6] = Static.ToInt(numErrorValue.EditValue);
                    newValue[7] = Static.ToInt(numHistory.EditValue);
                    newValue[8] = Static.ToStr(txtServername.EditValue);
                    newValue[9] = Static.ToStr(txtServerport.EditValue);
                    newValue[10] = Static.ToStr(txtMailuname.EditValue);
                    newValue[11] = Static.ToStr(txtMailpass.EditValue);
                    newValue[12] = Static.ToStr(txtFromuser.EditValue);
                    newValue[13] = Static.ToInt(checkBox1.Checked ? 1 : 0);

                    fieldValue[0] = "MASKTYPE";
                    fieldValue[1] = "MASKVALUE";
                    fieldValue[2] = "MASKDESCRIPTION";
                    fieldValue[3] = "DEFAULTPASS";
                    fieldValue[4] = "CREATETYPE";
                    fieldValue[5] = "VALIDDAY";
                    fieldValue[6] = "WRONGCOUNT";
                    fieldValue[7] = "HISTORYCOUNT";
                    fieldValue[8] = "SERVERNAME";
                    fieldValue[9] = "SERVERPORT";
                    fieldValue[10] = "MAILUSERNAME";
                    fieldValue[11] = "MAILUSERPASS";
                    fieldValue[12] = "FROMUSER";
                    fieldValue[13] = "ISSENDMAIL";

                    Result res = new Result();
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110114, 110114, new[] { newValue, oldValue, fieldValue });
                    msg = "Амжилттай хадгалагдлаа.";
                    if (res.ResultNo == 0)
                    {
                        MessageBox.Show(msg);
                    }
                    else
                    {
                        MessageBox.Show(res.ResultNo + ":" + res.ResultDesc);
                    }
                }
                
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PassOption_Load(object sender, EventArgs e)
        {
            this.Show();
            appname = _core.ApplicationName;
            formname = "Admin." + this.Name;

            FormName = this;
            ISM.Template.FormUtility.RestoreStateForm(appname, ref FormName);
            lod();

            if (_core.resource != null)
            {
                Save.Image = _core.resource.GetImage("navigate_edit");
                btnClose.Image = _core.resource.GetImage("navigate_cancel");
            }
        }
        private void PassOption_KeyDown(object sender, KeyEventArgs e)
        {
            ISM.Template.FormUtility.SaveStateForm(appname, ref FormName);
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
        private void cboMaskCreate_EditValueChanged(object sender, EventArgs e)
        {
            if (Static.ToInt(cboMaskCreate.ItemIndex) == 1)
            {
                txtMaskDft.Properties.ReadOnly = true;
                Result re = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 203, 110113, 110113, null);
                DataRow DR = re.Data.Tables[0].Rows[0];
                txtMaskDft.EditValue = Static.ToStr(DR["DEFAULTPASS"]);
            }
            else
            {
                txtMaskDft.Properties.ReadOnly = false;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.Text = "Илгээнэ";
                txtServername.Properties.ReadOnly = false;
                txtServerport.Properties.ReadOnly = false;
                txtFromuser.Properties.ReadOnly = false;
                txtMailpass.Properties.ReadOnly = false;
                txtMailuname.Properties.ReadOnly = false;
            }
            else
            {
                checkBox1.Text = "Илгээхгүй";
                txtServername.Properties.ReadOnly = true;
                txtServerport.Properties.ReadOnly = true;
                txtFromuser.Properties.ReadOnly = true;
                txtMailpass.Properties.ReadOnly = true;
                txtMailuname.Properties.ReadOnly = true;
            }
        }
        #endregion[]
    }
}
