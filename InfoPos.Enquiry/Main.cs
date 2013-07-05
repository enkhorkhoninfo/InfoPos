using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using EServ.Shared;
using ISM.Template;

namespace InfoPos.Enquiry
{
    public class Main
    {
        private Core.Core _core;
        public void CallFAEnquiry(object[] param)                       //Үндсэн хөрөнгийн дэлгэрэнгүй лавлагаа
        {
            CallCallEnquiry(param, "FA");
        }
        public void CallInvEnquiry(object[] param)                      //Бараа материалын дэлгэрэнгүй лавлагаа
        {
            CallCallEnquiry(param, "INV");
        }
        public void CallCallEnquiry(object[] param, string Enq_Type)    //Дэлгэрэнгүй лавлагаа
        {
            string ID = "";
            DataRow DR = null;
            try
            {
                _core = (Core.Core)param[0];
                if (param[1] != null)
                {
                    DR = (DataRow)param[1];
                }
                if (param[2] != null)
                {
                    ID = Static.ToStr(param[2]);
                }
                InfoPos.Enquiry.Enquiry enq = new InfoPos.Enquiry.Enquiry(_core, Enq_Type, ID, DR, param);
                enq.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CallCustomerEnquiry(object[] param)                 //Харилцагчийн лавлагаа дуудах
        {
            _core = (Core.Core)param[0];
            object[] obj = new object[1];
            long CustomerID = 1;
            try
            {
                CustomerID = Static.ToLong(param[1]);
                obj[0] = (DataRow)param[2];
            }
            catch { CustomerID = 1; obj = null; }
            InfoPos.Enquiry.CustomerEnquiry CusEnq = new InfoPos.Enquiry.CustomerEnquiry(_core, CustomerID, obj);
            CusEnq.ShowDialog();
        }
        public void CallCustContactEnquiry(object[] param)              //Холбоо барисан харилцагчийн лавлагаа дуудах
        {
            _core = (Core.Core)param[0];
            object[] obj = new object[1];
            long CustomerID = 1;
            try
            {
                CustomerID = Static.ToLong(param[1]);
                obj[0] = (DataRow)param[2];
            }
            catch { CustomerID = 1; obj = null; }
            InfoPos.Enquiry.CustContactEnquiry CusEnq = new InfoPos.Enquiry.CustContactEnquiry(_core, CustomerID, obj);
            CusEnq.ShowDialog();
        }
    }
}
