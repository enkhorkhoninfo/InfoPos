using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Shared;

namespace InfoPos.List
{
    public class List
    {
        private  Core.Core _core;
        public List()
        {
        }
        public void CallSaleList(object[] param)                //Борлуулалын жагсаалт дуудах
        {
            _core = (Core.Core)param[0];
            InfoPos.List.SalesSearch frm = new InfoPos.List.SalesSearch(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallCustomerList(object[] param)                //Харилцагчийн жагсаалт дуудах
        {
            _core = (Core.Core)param[0];
            InfoPos.List.CustomerList frm = new InfoPos.List.CustomerList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallUserList(object[] param)                    //Хэрэглэгчийн жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.UserList frm = new InfoPos.List.UserList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallUserGroupList(object[] param)               //Хэрэглэгчийн бүлгийн жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.GroupUserList frm = new InfoPos.List.GroupUserList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallInventoryList(object[] param)               //Бараа материалын жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.InventoryList frm = new InfoPos.List.InventoryList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallServiceList(object[] param)                 //Үйлчилгээний жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.ServiceList frm = new InfoPos.List.ServiceList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallDocTemplate(object[] param)                 //Тайлангийн документ жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.ReportList frm = new InfoPos.List.ReportList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallTxnYNoteList(object[] param)                //Ерөнхий дэвтрийн гүйлгээний жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.TxnYNote TxnYnoteList = new TxnYNote(_core);
            TxnYnoteList.MdiParent = _core.MainForm;
            TxnYnoteList.Show();
        }
        public void CallEmployeeList(object[] param)                //Ажилчдын жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.EmployeeList EmpList = new EmployeeList(_core);
            EmpList.MdiParent = _core.MainForm;
            EmpList.Show();
        }
        public void CallDocTemplateList(object[] param)             //Гүйлгээ журналын лавлагаа дуудах
        {
            _core = (Core.Core)param[0];
            InfoPos.List.DocTemplate doc = new InfoPos.List.DocTemplate(_core);
            doc.MdiParent = _core.MainForm;
            doc.Show();
        }
        public void CallAccountTxnList(object[] param)              //Дансны гүйлгээний жагсаалт
        {
            _core = (Core.Core)param[0];
            int pType = Static.ToInt(param[1]);
            long pID = Static.ToLong(param[2]);
          //  object[] pParam = (object[])(param[4]);
           

           InfoPos.List.AccountTxnList Acc = new InfoPos.List.AccountTxnList(_core, pType, pID, null,null);
            //Acc.MdiParent = _core.MainForm;
            Acc.ShowDialog();
        }
        public void CallTxnList(object[] param)                     //Гүйлгээний кодуудын жагсаалт
        {
            _core = (Core.Core)param[0];

            InfoPos.List.TxnList tx = new InfoPos.List.TxnList(_core);
            tx.MdiParent = _core.MainForm;
            tx.Show();
        }
        public void CallTxnEntryList(object[] param)                //Гүйлгээний оролтын кодуудын жагсаалт
        {
            _core = (Core.Core)param[0];

            InfoPos.List.TxnEntry txnEnt = new InfoPos.List.TxnEntry(_core);
            txnEnt.MdiParent = _core.MainForm;
            txnEnt.Show();
        }
        public void CallCurRateHistory(object[] param)              //Байгууллагын зүйлийн төрлийн жагсаалт
        {
            _core = (Core.Core)param[0];

            InfoPos.List.CurRateHistory ComObjList = new InfoPos.List.CurRateHistory(_core);
            ComObjList.MdiParent = _core.MainForm;
            ComObjList.Show();
        }
        public void CallLogList(object[] param)                     //Логийн бүртгэлийн жагсаалт
        {
            _core = (Core.Core)param[0];
            InfoPos.List.LogList Log = new InfoPos.List.LogList(_core);
            Log.MdiParent = _core.MainForm;
            Log.Show();

        }
        public void CallContractList(object[] param)                //Гэрээний жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.ContractList frm = new InfoPos.List.ContractList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallPackMainList(object[] param)                //Бүтээгдэхүүний багцын бүртгэлийн жагсаалт
        {
            _core = (Core.Core)param[0];

            InfoPos.List.PackMainList frm = new InfoPos.List.PackMainList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallOrderList(object[] param)                   //Захиалгын жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.OrderList frm = new InfoPos.List.OrderList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallIssueProjectList(object[] param)            //Төслийн жагсаалт
        {
            _core = (Core.Core)param[0];
            InfoPos.List.IssueProject iss = new InfoPos.List.IssueProject(_core);
            iss.MdiParent = _core.MainForm;
            iss.Show();

        }
        public void CallProjectIssueList(object[] param)            //Төслийн жагсаалт
        {
            _core = (Core.Core)param[0];
            InfoPos.List.ProjectIssue iss = new InfoPos.List.ProjectIssue(_core);
            iss.MdiParent = _core.MainForm;
            iss.Show();

        }
        public void CallIssueList(object[] param)            //Асуудлын жагсаалт
        {
            _core = (Core.Core)param[0];
            InfoPos.List.Issue i1 = new InfoPos.List.Issue(_core);
            i1.MdiParent = _core.MainForm;
            i1.Show();

        }
        public void CallIssueContactList(object[] param)            //Холбоо барьсан харилцагчийн асуудлын жагсаалт
        {
            _core = (Core.Core)param[0];
            InfoPos.List.IssueContact i11 = new InfoPos.List.IssueContact(_core);
            i11.MdiParent = _core.MainForm;
            i11.Show();

        }
        public void CallContactList(object[] param)              //Холбоо барьсан харилцагчийн талаарах жагсаалт мэдээлэл авах
        {
            _core = (Core.Core)param[0];
            InfoPos.List.ContactList ob = new InfoPos.List.ContactList(_core);
            ob.MdiParent = _core.MainForm;
            ob.Show();
        }
        public void CallProdPriceHistList(object[] param)              //Үнийн түүхиин жагсаалт
        {
            _core = (Core.Core)param[0];
            InfoPos.List.ProdPriceHist ob = new InfoPos.List.ProdPriceHist(_core);
            ob.MdiParent = _core.MainForm;
            ob.Show();
        }
        public void CallSalesMoreBOList(object[] param)              //Боруулалтын дэлгэрэнгүй
        {
            _core = (Core.Core)param[0];
            InfoPos.List.SalesMoreBO ob = new InfoPos.List.SalesMoreBO(_core);
            ob.MdiParent = _core.MainForm;
            ob.Show();
        }
        public void CallSalesProductBOList(object[] param)              //Борлуулсан бараа болон үйлчилгээний жагсаалт BO
        {
            _core = (Core.Core)param[0];
            InfoPos.List.SalesProductBO ob = new InfoPos.List.SalesProductBO(_core);
            ob.MdiParent = _core.MainForm;
            ob.Show();
        }
        public void CallSalesPayBOList(object[] param)              //Төлбөрийн жагсаалт BO
        {
            _core = (Core.Core)param[0];
            InfoPos.List.SalesPayBO ob = new InfoPos.List.SalesPayBO(_core);
            ob.MdiParent = _core.MainForm;
            ob.Show();
        }
        public void CallPreSaleList(object[] param)                   //УБ жагсаалт дуудах
        {
            _core = (Core.Core)param[0];

            InfoPos.List.PreSaleList frm = new InfoPos.List.PreSaleList(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
    }
}
