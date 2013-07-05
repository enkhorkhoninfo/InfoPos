using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace InfoPos.fo_panels
{
    public partial class frmMessage : DevExpress.XtraEditors.XtraForm
    {
        public frmMessage(enumMessage msgno)
        {
            InitializeComponent();
            SetText(msgno);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        public enum enumMessage
        {
            REPEATREAD=10000,
            READCORRECT=10001,
            WRITECORRECT=10002,
            CLEANCORRECT=10003,
            WRITEERROR=10004,
            READERROR=10005,
            CLEANERROR=10006
        }
       public void SetText(enumMessage msgno)
       {
           switch(msgno)
           {
               case enumMessage.WRITECORRECT: labelControl1.Text = "Амжилттай бичигдлээ"; break;
               case enumMessage.READCORRECT: labelControl1.Text = "Амжилттай уншигдлаа"; break;
               case enumMessage.CLEANCORRECT: labelControl1.Text = "Амжилттай цэвэрлэгдлээ"; break;
               case enumMessage.WRITEERROR: labelControl1.Text = "Мэдээлэл бичихэд алдаа гарлаа.\r\nДахин оролдоно уу."; break;
               case enumMessage.CLEANERROR: labelControl1.Text = "Мэдээлэл цэвэрлэхэд алдаа гарлаа.\r\nДахин оролдоно уу."; break;
               case enumMessage.READERROR: labelControl1.Text = "Мэдээлэл уншихад алдаа гарлаа.\r\nДахин оролдоно уу."; break;
               case enumMessage.REPEATREAD: labelControl1.Text = "Тагыг дахин уншуулна уу."; break;

           }
       }
    }
}