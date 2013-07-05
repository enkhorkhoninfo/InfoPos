using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;
using DevExpress.XtraEditors;
using ISM.Template;
using System.Text.RegularExpressions;
using DevExpress.XtraBars.Alerter;

namespace InfoPos.Parameter
{
    public partial class FormAutoNum : ISM.Template.frmTempProp
    {  
        #region[Хувьсагч]
        object[] OldValue;
        object[] FieldName;
        FunctionParam fp = new FunctionParam();
        int i = 0;
        int SelectTxnCode = 140031;
        int AddTxnCode = 140033;
        int EditTxnCode = 140034;
        int DeleteTxnCode = 140035;
        int btn = 0;
        int rowhandle = 0;
        InfoPos.Core.Core _core = null;
        bool check = true;
        string appname = "", formname = "";
        Form FormName = null;
       #endregion
        #region[Байгуулагч функц]
        public FormAutoNum(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            Init();
            InitCombo();
            this.Resource = _core.Resource;
            this.FieldLinkSetSaveState();
            if (_core.Resource != null)
            {
                btncheck.Image = _core.Resource.GetImage("menu_check");
            }
        }
        #endregion
        #region[InitFunction]
        private void Init()
        {
            try
            {
                this.EventRefresh += new delegateEventRefresh(FormAutoNum_EventRefresh);
                this.EventRefreshAfter += new delegateEventRefreshAfter(FormAutoNum_EventRefreshAfter);
                this.EventSave += new delegateEventSave(FormAutoNum_EventSave);
                this.EventEdit += new delegateEventEdit(FormAutoNum_EventEdit);
                this.EventDelete += new delegateEventDelete(FormAutoNum_EventDelete);

                this.FieldLinkAdd("cboType", "ID", "", true, true);
                this.FieldLinkAdd("txtName", "Name", "", true, false);
                this.FieldLinkAdd("txtName2", "Name2", "", false, false);
                this.FieldLinkAdd("txtMask", "Mask", "", true, false);
                this.FieldLinkAdd("txtKey", "Key", "", true, false, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void InitCombo()
        {
            FormUtility.LookUpEdit_SetList(ref cboType, "3", "Гэрээний дугаарлалт");
            FormUtility.LookUpEdit_SetList(ref cboType, "4", "Захиалгын дугаарлалт");
            FormUtility.LookUpEdit_SetList(ref cboType, "5", "Борлуулалтын дугаарлалт");
            FormUtility.LookUpEdit_SetList(ref cboType, "6", "Борлуулалтын багцын дугаарлалт ");         
            FormUtility.LookUpEdit_SetList(ref cboType, "8", "Барьцааны дугаарлалт");
            FormUtility.LookUpEdit_SetList(ref cboType, "9", "Төлбөрийн дугаарлалт");
            FormUtility.LookUpEdit_SetList(ref cboType, "12", "Харилцагчийн дугаарлалт");            
            FormUtility.LookUpEdit_SetList(ref cboType, "17", "Асуудлын дугаарлалт");
            FormUtility.LookUpEdit_SetList(ref cboType, "18", "Төслийн дугаарлалт");
            FormUtility.LookUpEdit_SetList(ref cboType, "19", "Гэрээний дугаарлалт (XLS)");
        }
        #endregion
        #region[Event
        void FormAutoNum_EventSave(bool isnew, ref bool cancel)
        {
            string err = "";
            Control cont = null;
            
            if (!FieldValidate(ref err, ref cont))
            {
                MessageBox.Show(err);
                cont.Select();
                cancel = true;
            }
            else
            {
                Result r;
                try
                {
                    object[] NewValue = { Static.ToInt(cboType.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToStr(txtMask.EditValue), Static.ToStr(txtKey.EditValue) };
                    if (!isnew)
                    {
                        if (txtKey.Text != "" && check == true)
                        {
                            r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, EditTxnCode, EditTxnCode, new object[] { NewValue, OldValue, FieldName });
                            MessageBox.Show("Амжилттай засварлалаа .");
                        }
                    }
                    else
                    {
                        if (txtKey.Text != "" && check==true)
                        {
                            r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, AddTxnCode, AddTxnCode, new object[] {NewValue,FieldName });
                            if (r.ResultNo == 0) MessageBox.Show("Амжилттай нэмлээ .");
                            else
                            {
                                MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                                cancel = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        void FormAutoNum_EventDelete()
        {
            DialogResult DR = MessageBox.Show("Бичлэгийг утсгахдаа итгэлтэй байна уу?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == System.Windows.Forms.DialogResult.No) return;
            else
            {
                try
                {
                    Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, DeleteTxnCode, DeleteTxnCode, new object[] { cboType.EditValue });
                    if (r.ResultNo != 0)
                    {
                        MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Амжилттай устгагдлаа .");
                        btn = 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void FormAutoNum_EventEdit(ref bool cancel)
        {
            object[] Value = { Static.ToInt(cboType.EditValue), Static.ToStr(txtName.EditValue), Static.ToStr(txtName2.EditValue), Static.ToStr(txtMask.EditValue), Static.ToStr(txtKey.EditValue) };
            OldValue = Value;
        }
        void FormAutoNum_EventRefreshAfter()
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            this.FieldLinkSetColumnCaption(0, "Дугаарлалтын төрөл");
            this.FieldLinkSetColumnCaption(1, "Нэр");
            this.FieldLinkSetColumnCaption(2, "Нэр 2");
            this.FieldLinkSetColumnCaption(3, "Маскны утга");
            this.FieldLinkSetColumnCaption(4, "Түлхүүр үг");
            appname = _core.ApplicationName;
            formname = "Parameter." + this.Name;
            FormName = this;
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridView1);
            switch (btn)
            {
                case 0: gridView1.FocusedRowHandle = rowhandle; break;
                case 1: gridView1.FocusedRowHandle = rowhandle - 1; break;
            }
            btn = 0;
        }
        void FormAutoNum_EventRefresh(ref DataTable dt)
        {
            rowhandle = gridView1.FocusedRowHandle;
            try
            {
                Result r = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 2021, SelectTxnCode, SelectTxnCode, null);
                if (r.ResultNo != 0)
                {
                    MessageBox.Show(r.ResultNo.ToString() + " " + r.ResultDesc);
                    return;
                }
                else
                {
                    dt = r.Data.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region[Түлхүүр үг авах буюу маск шалгах]
        private void btncheck_Click(object sender, EventArgs e)
        {
            mmError.Text = null;
            if (txtMask.Text == "")
            {
                MessageBox.Show("Маскны утга оруулна уу .");
            }
            else
            {
                int count = 0, p = 0, oldend, start = 0, j = 0, i = 0, sum = 0;
                bool f = false, h = false;
                string re = "", b = "", c = "", d = "";
                string a = txtMask.Text;
                try
                {
                    int k = a.Length;
                    #region[Давталт]
                    for (i = 0; i <= k - 1; i++)
                    {
                        if (a[i] == ']')
                        {
                            f = true;
                            h = true;
                            oldend = i + 1;
                            count = k - oldend;
                            b = a.Substring(j, oldend);
                            #region[Давталт2]
                            for (int l = 0; l <= b.Length - 1; l++)
                            {
                                if (b[l] == '[')
                                {
                                    start = l + 1;
                                }
                                if (b[l] == ']')
                                {
                                    c = b.Substring(start, 1);
                                    re = a.Substring(start, 4);
                                    p = Convert.ToInt16(b.Substring(start + 1, 2));
                                    sum = sum + p + (start - 1);
                                    string q = re.Substring(3, 1);
                                    if (q != "]")
                                    {
                                        txtKey.Text = null;
                                        mmError.Text=" Үсгийн ард 2 оронтой тоо байх ёстой.";
                                        break;
                                    }
                                    switch (c)
                                    {
                                        case "U": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "A": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "B": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "C": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "G": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "P": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "S": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "Y": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "M": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        case "R": d = d + c; a = a.Substring(oldend, a.Length - oldend); k = a.Length; i = 0; break;
                                        default: mmError.Text = c + " үсгийн формат буруу байна."; d = null; l = b.Length; i = k; break;
                                    }
                                }
                            }
                            #endregion
                        }
                        else f = false;
                    }
                    #endregion
                    if (h == false)
                    {
                        mmError.Text = "Маскны формат буруу байна [...] хаалт алга байна";
                    }
                    int countA = 0;
                    int countB = 0;
                    for (int l = 0; l <= a.Length - 1; l++)
                    {
                        if (a[l] != '[') countA++;
                        if (a[l] != ']') countB++;
                    }
                    if (countA == countB)
                    {
                        f = true;
                    }
                    else
                    {
                        f = false;
                    }
                    if (f != true)
                    {
                        d = null;
                        mmError.Text="[...] хаалтны алдаатай байна .";
                    }
                    else
                    {
                        txtKey.Text = d;
                        txtKey.EditValue = d;
                    }
                    sum = sum + count;
                    if (Static.ToInt(cboType.EditValue) == 1 || Static.ToInt(cboType.EditValue) == 2)
                    {
                        if (sum > 20)
                        {
                            txtKey.Text = null;
                            mmError.Text = "Дугаарын урт 20-c бага байх ёстой таных " + sum + " байна .";
                            sum = 0;
                            p = 0;
                            start = 0;
                        }
                    }
                    else
                    {
                        if (sum > 16)
                        {
                            txtKey.Text = null;
                            mmError.Text = "Дугаарын урт 16-c бага байх ёстой таных " + sum + " байна .";
                            sum = 0;
                            p = 0;
                            start = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtKey.Text = null;
                    mmError.Text="[...] хаалтан дотор үсэг 2 оронтой тоо байх ёстой .";
                }
                check = true;
            }
        }
        #endregion
        #region[FormEvent]
        private void FormAutoNum_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUtility.SaveStateForm(appname, ref FormName);
            FormUtility.SaveStateGrid(appname, formname, ref gridView1);
        }
        private void FormAutoNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void txtMask_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }
        private void txtMask_KeyUp(object sender, KeyEventArgs e)
        {
            //Маскийн утгыг өөрчилсөн тохиолдолд 
            //түлхүүр үгийг хүчингүй болгож арилгах
            txtKey.Text = null;
            check = false;
        }
        private void mmError_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        private void cboType_EditValueChanged(object sender, EventArgs e)
        {
            //3	-	Гэрээний дугаарлалт
            //4	-	Захиалгын дугаарлалт
            //5	-	Борлуулалтын дугаарлалт
            //8	-	Барьцааны дугаарлалт
            //9	-	Төлбөрийн дугаарлалт
            //12	-	Харилцагчийн дугаарлалт
            int AutoNumType = Static.ToInt(cboType.EditValue);
            switch (AutoNumType)
            {
                case 3:
                case 19:
                    lblNote.Text = "Маскны үсгийн формат: Y - Жил, C - Валют код, P - Гишүүний төрөл, A - Санамсаргүй тоо, U - Хэрэглэгчийн утга, S - Дэс дугаар \n Маскны тооны урт: Нийт тооны урт 20-с бага байх ёстой.\nЖишээ нь: [Y04][P01][S06]";
                    break;
                case 4:
                    lblNote.Text = "Маскны үсгийн формат: Y - Жил, C - Валют код, S - Дэс дугаар \n Маскны тооны урт: Нийт тооны урт 20-с бага байх ёстой.\nЖишээ нь: [Y04][C01][S06]";
                    break;
                case 5:
                    lblNote.Text = "Маскны үсгийн формат: Y - Жил, S - Дэс дугаар \n Маскны тооны урт: Нийт тооны урт 20-с бага байх ёстой.\nЖишээ нь: [Y04][S06]";
                    break;
                case 8:
                    lblNote.Text = "Маскны үсгийн формат: Y - Жил, S - Дэс дугаар \n Маскны тооны урт: Нийт тооны урт 20-с бага байх ёстой.\nЖишээ нь: [Y04][S06]";
                    break;
                case 9:
                    lblNote.Text = "Маскны үсгийн формат: Y - Жил, S - Дэс дугаар \n Маскны тооны урт: Нийт тооны урт 20-с бага байх ёстой.\nЖишээ нь: [Y04][S06]";
                    break;
                case 12:
                    lblNote.Text = "Маскны үсгийн формат: B - Салбар, Y - Жил, C - Харилцагч ангилал, P - Гишүүний төрөл, G - Харилцагчийн төрөл, S - Дэс дугаар \n Маскны тооны урт: Нийт тооны урт 20-с бага байх ёстой.\nЖишээ нь: [B02][Y04][C01][S06]";
                    break;
            }
            //if (Static.ToInt(cboType.EditValue) == 1 || Static.ToInt(cboType.EditValue) == 2)
            //{
            //    lblNote.Text = "Маскны үсгийн формат: B, C, G, P, S ,Y ,M, R\n Маскны тооны урт: Нийт тооны урт 20-с бага байх ёстой.\nЖишээ нь: 1N3[B01]13[G03]0[C01]12A";
            //}
            //else
            //{
            //    lblNote.Text = "Маскны үсгийн формат: B, C, G, P, S ,Y ,M ,R\n Маскны тооны урт: Нийт тооны урт 16-с бага байх ёстой.\nЖишээ нь: 1N3[B01]13[G03]0[C01]12A";
            //}
        }

        private void FormAutoNum_Load(object sender, EventArgs e)
        {
            FormUtility.RestoreStateForm(appname, ref FormName);
            FormUtility.RestoreStateGrid(appname, formname, ref gridControl1);
        }
    }
}