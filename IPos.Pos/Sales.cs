using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using System.Data;

using IPos.DB;
using IPos.Core;

namespace IPos.Pos
{
    public class Sales : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                switch (ri.FunctionNo)
                {
                    case 605001: 	             //Борлуулалтын жагсаалт хайлт
                        res = Txn605001(ci, ri, db, ref lg);
                        break;
                    case 605002: 	             //Борлуулалт хадгалах
                        res = Txn605002(ci, ri, db, ref lg);
                        break;
                    case 605003: 	             //Борлуулалт дахь үйлчлүүлэгчид ба түрээсийн хэрэгслүүд
                        res = Txn605003(ci, ri, db, ref lg);
                        break;
                    case 605004: 	             //Борлуулалт дахь бараа үйлчилгээний жагсаалт
                        res = Txn605004(ci, ri, db, ref lg);
                        break;
                    case 605005: 	             //Багц доторх бараа үйлчилгээний жагсаалт
                        res = Txn605005(ci, ri, db, ref lg);
                        break;
                    case 605006: 	             //Тухайн бараа үйлчилгээнд дагалдах хэрэгслүүд
                        res = Txn605006(ci, ri, db, ref lg);
                        break;
                    case 605007: 	             //Тагын дугаараар борлуулалтын дугаар буцаах
                        res = Txn605007(ci, ri, db, ref lg);
                        break;
                    case 605008: 	             //Тухайн AreaCode дээрх барааны жагсаалт
                        res = Txn605008(ci, ri, db, ref lg);
                        break;

                    case 605009: 	             //Хэрэглэгчийн төлбөрийн төрлийн жагсаалт
                        res = Txn605009(ci, ri, db, ref lg);
                        break;
                    case 605010: 	             //Төлбөрийн үлдэгдэл, төрлийн жагсаалт
                        res = Txn605010(ci, ri, db, ref lg);
                        break;
                    case 605011: 	             //Төлбөрийн гүйлгээ багцаар хийх
                        res = Txn605011(ci, ri, db, ref lg);
                        break;
                    case 605012: 	             //Төлбөрийн гүйлгээ болон борлуулалтын гүйлгээг хамтад хийх
                        res = Txn605012(ci, ri, db, ref lg);
                        break;
                    case 605013: 	             //Борлуулалтын бараа үйлчилгээний жагсаалт
                        res = Txn605013(ci, ri, db, ref lg);
                        break;
                    case 605014: 	             //Төлбөрийн төлсөн, буцаасан дүнг базсан жагсаалт
                        res = Txn605014(ci, ri, db, ref lg);
                        break;
                    case 605015: 	             //Борлуулалтыг бүхэлд нь буцаах
                        res = Txn605015(ci, ri, db, ref lg);
                        break;
                    case 605016: 	             //Борлуулалт доторх бараа үйлчилгээний буцаалт
                        res = Txn605016(ci, ri, db, ref lg);
                        break;
                    case 605017: 	             //Төлбөрийн буцаалт
                        res = Txn605017(ci, ri, db, ref lg);
                        break;

                    case 605018: 	             //Хэрэгслийг өөр үйлчлүүлэгч рүү шилжүүлэх
                        res = Txn605018(ci, ri, db, ref lg);
                        break;

                    case 605019:                // НӨАТ солих эрх шалгах
                        res = Txn605019(ci, ri, db, ref lg);
                        break;

                    case 605020: 	             //Төлбөрийн гүйлгээ болон сунгалтын гүйлгээг хамтад хийх
                        res = Txn605020(ci, ri, db, ref lg);
                        break;
                    case 605021: 	             //Төлбөрийн гүйлгээ болон засварын гүйлгээг хамтад хийх
                        res = Txn605021(ci, ri, db, ref lg);
                        break;

                    case 605030: 	             //Хөнгөлөлтийн файлуудыг нэгтгэж илгээх
                        res = Txn605030(ci, ri, db, ref lg);
                        break;

                    case 605040: 	             //Борлуулалтын товч мэдээ
                        res = Txn605040(ci, ri, db, ref lg);
                        break;
                    case 605041: 	             //Борлуулалтын дэлгэрэнгүй мэдээ
                        res = Txn605041(ci, ri, db, ref lg);
                        break;
                    case 605042: 	             //Төлбөрийн дэлгэрэнгүй мэдээ
                        res = Txn605042(ci, ri, db, ref lg);
                        break;

                    case 605050: 	             //Тасалбараар хэвлэх үйлчилгээний жагсаалт
                        res = Txn605050(ci, ri, db, ref lg);
                        break;

                    default:
                        res = new Result(9110009, "Функц тодорхойлогдоогүй байна");
                        break;
                }
                return res;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Функц дуудахад алдаа гарлаа.\r\n" + ex.Message);
            }
            return res;
        }

        #region [ Business functions ]

        public string GetActionId()
        {
            DateTime now = DateTime.Now;
            byte[] bytes = BitConverter.GetBytes(now.Ticks);
            return Convert.ToBase64String(bytes);
        }
        public Result GetPosStatus(DbConnections db, string posno, int userno, string ip, string mac)
        {
            Result res = null;

            string sql = @"select p.posno,p.posname,p.posdesc,p.posip,p.posmac,p.postype
,p.shiftno,p.shiftuserno,p.status,p.trandate
from posterminal p where p.posno=:1";

            object[] param = new object[] { posno };
            res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "CheckPosStatus", param);
            if (res != null && res.ResultNo != 0) goto OnExit;
            if (res.AffectedRows <= 0)
            {
                res = new Result(6020011, string.Format("[{0}] дугаартай ПОС бүртгэлд байхгүй байна.", posno));
                goto OnExit;
            }

            /******************************************
             * 1. Борлуулалт хийж бга хэрэглэгч нь ээлж эхлүүлсэн
             *    хэрэглэгч мөн эсэхийг шалгана.
             * 2. Ээлжийн төлөв нь нээлттэй бга эсэх.
             ******************************************/
            DataTable dt = res.Data.Tables[0];
            if (dt.Rows.Count <= 0)
            {
                res = new Result(6020011, "Ээлж эхлээгүй байна.");
                goto OnExit;
            }
            int shiftno = Static.ToInt(dt.Rows[0]["SHIFTNO"]);
            int shiftuserno = Static.ToInt(dt.Rows[0]["SHIFTUSERNO"]);
            int shiftstatus = Static.ToInt(dt.Rows[0]["STATUS"]);

            if (userno != shiftuserno)
            {
                res = new Result(6020012, string.Format("[{0}] дугаартай хэрэглэгч ээлж эхлүүлсэн байна.", shiftuserno));
                goto OnExit;
            }
            if (shiftstatus != 1)
            {
                res = new Result(6020013, string.Format("Ээлж эхлүүлээгүй байна."));
                goto OnExit;
            }
            res.Param = new object[] { shiftno };
        OnExit:
            return res;
        }
        public Result GetPaymentTypes(DbConnections db, int userno, string posno)
        {
            Result res = null;
            try
            {
                #region хэрэглэгчийн боломжит төлбөрийн төрлүүдийн жагсаалт авах

                string sql = @"select /*+ first_rows(1) */
pt.typeid,pt.name,pt.suspaccount,0 paid,pt.contracttype,pt.paymentflag,pt.contractcheck
from papaytype pt
inner join papaytypeuser pu on pu.typeid=pt.typeid
inner join pospaytype pos on pos.paytypeid=pt.typeid and pos.posno=:2
where pu.userno=:1
order by pt.orderno";
                object[] param = new object[] { userno, posno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "PaymentTypes", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        }    //Төлбөрийн төрлийн жагсаалт
        public Result GetSalesInv(DbConnections db, string salesno, int rentflag /*0,1,2,  8 бол 1&2, 9 бол бүгд*/)
        {
            /**********************************************************
             * Тухайн борлуулалтанд байгаа Түрэсийн хэрэгслүүдийн жагсаалт.
             * Багц болон Үйлчилгээн дотор дагалдсан бүгдийг гаргана.
             * Гарсан жагсаалт нь дандаа бараа бх ёстой.
             **********************************************************/
            /**********************************************************
             * Хэрэв бүтээгдэхүүний төрөл нь багц бол доторх бараагаар задлах. 
             * Үүний тулд өмнө оруулсан SalesProd тэйблийн бүтээгдэхүүнүүдийг PackItem
             * тэйбэлтэй жойн хийж задалж авна.
             * 
             * Хэрэв бүтээгдэхүүний төрөл үйлчилгээ бол доторх дагалдах
             * хэрэгслүүдийг бас задалж авна. Үүний тулд ServInventory тэйбэлтэй
             * холбож гаргана.
             **********************************************************/

            Result res = null;
            try
            {
                #region Тухай борлуулалтад орсон бүх түрээсийн хэрэгслүүд

                string sql = @"select a.salesno,a.custno,a.prodno,a.prodtype
,max(decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname)) custname
,max(i.name) prodname,sum(a.qty) qty,max(i.rentflag) rentflag
,max(a.servtime) servtime,max(servid) servid
from (select ss.salesno,ss.custno
    ,decode(ss.prodtype,1,i.invid,ss.prodno) prodno
    ,decode(ss.prodtype,1,0,ss.prodtype) prodtype
    ,decode(ss.prodtype,1,ss.qty,ss.qty) qty
    ,decode(ss.prodtype,1,sm.servicetime,0) servtime
    ,decode(ss.prodtype,1,ss.prodno,null) servid
from (select s.salesno,s.custno
    ,decode(s.prodtype,2,p.prodid,s.prodno) prodno
    ,decode(s.prodtype,2,p.prodtype,s.prodtype) prodtype
    ,decode(s.prodtype,2,p.count*s.qty,s.qty) qty
    from salesprod s
    left join packitem p on p.packid=s.prodno and s.prodtype=2
    where s.salesno=:1 and s.prodtype in (1,2)
) ss
left join servinventory i on i.servid=ss.prodno and ss.prodtype=1
left join servmain sm on sm.servid=ss.prodno and ss.prodtype=1
where ss.prodtype=0 or i.invid is not null
) a
left join invmain i on i.invid=a.prodno
left join customer c on c.customerno=a.custno
where a.prodtype=0 {0}
group by a.salesno,a.custno,a.prodno,a.prodtype
order by 1,2,3";

                object[] param = null;
                string filter = "";
                switch (rentflag)
                {
                    case 0:
                    case 1:
                    case 2:
                        param = new object[] { salesno, rentflag };
                        filter = "and i.rentflag=:2";
                        break;
                    case 8:
                        param = new object[] { salesno };
                        filter = "and i.rentflag>0";
                        break;
                    default:
                        param = new object[] { salesno };
                        break;
                }

                sql = string.Format(sql, filter);
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "RentalItems", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        } // Борлуулалт дахь түрээсийн хэрэгслийн жагсаалт
        public Result GetSalesInv(DbConnections db, string salesno, string prodno, int prodtype)
        {
            /**********************************************************
             * Тухайн борлуулалтанд байгаа Түрэсийн хэрэгслүүдийн жагсаалт.
             * Багц болон Үйлчилгээн дотор дагалдсан бүгдийг гаргана.
             * Гарсан жагсаалт нь дандаа бараа бх ёстой.
             **********************************************************/
            /**********************************************************
             * Хэрэв бүтээгдэхүүний төрөл нь багц бол доторх бараагаар задлах. 
             * Үүний тулд өмнө оруулсан SalesProd тэйблийн бүтээгдэхүүнүүдийг PackItem
             * тэйбэлтэй жойн хийж задалж авна.
             * 
             * Хэрэв бүтээгдэхүүний төрөл үйлчилгээ бол доторх дагалдах
             * хэрэгслүүдийг бас задалж авна. Үүний тулд ServInventory тэйбэлтэй
             * холбож гаргана.
             **********************************************************/

            Result res = null;
            try
            {
                #region Тухай борлуулалтад орсон бүх түрээсийн хэрэгслүүд

                string sql = @"select a.salesno,a.custno,a.prodno,a.prodtype
,max(decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname)) custname
,max(i.name) prodname,sum(a.qty) qty,max(i.rentflag) rentflag
from (select ss.salesno,ss.custno
    ,decode(ss.prodtype,1,i.invid,ss.prodno) prodno
    ,decode(ss.prodtype,1,0,ss.prodtype) prodtype
    ,decode(ss.prodtype,1,ss.qty,ss.qty) qty
from (select s.salesno,s.custno
    ,decode(s.prodtype,2,p.prodid,s.prodno) prodno
    ,decode(s.prodtype,2,p.prodtype,s.prodtype) prodtype
    ,decode(s.prodtype,2,p.count*s.qty,s.qty) qty
    from salesprod s
    left join packitem p on p.packid=s.prodno and s.prodtype=2
    where s.salesno=:1 and s.prodno=:2 and s.prodtype=:3
) ss
left join servinventory i on i.servid=ss.prodno and ss.prodtype=1
where ss.prodtype=0 or i.invid is not null
) a
left join invmain i on i.invid=a.prodno
left join customer c on c.customerno=a.custno
where a.prodtype=0
group by a.salesno,a.custno,a.prodno,a.prodtype
order by 1,2,3";

                object[] param = new object[] { salesno, prodno, prodtype };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "RentalItems", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        } // Борлуулалт дахь түрээсийн хэрэгслийн жагсаалт
        public Result GetPaymentBalance(DbConnections db, string salesno)
        {
            Result res = null;
            try
            {
                #region төлбөрийн дутуу үлдсэн нийт дүнг авах

                string sql = @"
select s.salesno,sp.salesamount
,sp.discountprod+sp.discountsales discount
,sp.salesamount-sp.discountprod-sp.discountsales totalamount
,s.vat,st.paid
from sales s,
(
    select sum(salesamount) salesamount
    ,sum(discountprod + (baseprice-price)) discountprod
    ,sum(discountsales) discountsales
    from salesprod
    where salesno=:1
) sp,
(
    select nvl(sum(amount),0) paid 
    from salestxn 
    where salesno=:1
) st
where s.salesno=:1";
                object[] param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "GetPaymentBalance", param);
                //if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                //lg.item.Desc = "Төлбөрийн үлдэгдэл, төрлийн жагсаалт";
            }
        //OnExit:
            return res;
        }

        public Result SalesAction(DbConnections db, DateTime trandate, int userno, string posno, string area, string ip, string mac, ref int shiftno)
        {
            Result res = null;

            #region Посын ээлж эхлүүлсэн хэрэглэгч таарч бга эсэхийг шалгах

            res = GetPosStatus(db, posno, userno, ip, mac);
            if (res.ResultNo != 0) goto OnExit;
            shiftno = (int)res.Param[0];

            #endregion

            string actionid = GetActionId();
            string sql = @"insert into salesaction (actionid,trandate,postdate,shiftno,cashierno,posno,areacode,ip,mac) values (:1,:2,:3,:4,:5,:6,:7,:8,:9)";
            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn605002", actionid, trandate, DateTime.Now,shiftno,userno,posno,area,ip,mac);
            if (res != null && res.ResultNo != 0) goto OnExit;
            res.ResultDesc = actionid;

            OnExit:
            return res;
        }
        public Result SalesNew(DbConnections db, string actionid, object[] param)
        {
            #region Comment!

            /*******************************************************************
             * 
             * Шинээр борлуулалтын бичлэг оруулах.
             * Sales тэйблийн бичлэгт байгаа дараах утгуудыг шинэчилнэ. 
             * Үүнд:
             *      SalesAmount   = Борлуулалтын нийт дүн * (1 - VAT%)
             *      Discount      = Барааны хөнгөлөлтийн нийт дүн.
             *      DiscountSales = Борлуулалтаас тооцох хөнгөлөлтийн дүн.
             *      TotalAmount   = SalesAmount - Discount - DiscountSales
             *      VAT           = TotalAmount * VAT%
             * 
             * ХӨНГӨЛӨЛТ ТООЦОХ
             * Дараах байдлаар тооцно:
             * 
             *  - Шууд гэрээний хүрээнд борлуулалт хийдсэн бол гэрээнээс НӨАТ тооцох эсэхийг авах. 
             *    Талбар: ContractMain.Vat
             *    
             *  - Үйлчлүүлэгч бол гишүүн эсэхийг шалгах ба гишүүний бүртгэлээс НӨАТ тооцох эсэхийг авах ???
             *  
             *******************************************************************/

            #endregion

            Result res = null;
            string sql = null;
            try
            {
                #region Prepare parameters

                //DateTime _trandate = Static.ToDate(param[0]);
                //string _posno = Static.ToStr(param[1]);
                //string _areacode = Static.ToStr(param[2]);

                string _salesno = Static.ToStr(param[3]);
                decimal _custo = Static.ToDecimal(param[4]);
                string _contractno = Static.ToStr(param[5]);
                string _orderno = Static.ToStr(param[6]);
                decimal _discount = Static.ToDecimal(param[7]);
                decimal _vatamount = Static.ToInt(param[8]); // 0-No VAT, 1-VAT үүнийг гэрээний бүртгэлээс давхар авна.
                string _pledgeno = Static.ToStr(param[9]);
                DataTable _cart = (DataTable)param[10];

                string _corrsalesno = _salesno;

                //decimal vat = isvat == 1 ? SystemProp.VAT : 0;

                #endregion
                #region Посын ээлж эхлүүлсэн хэрэглэгч таарч бга эсэхийг шалгах

                //res = CheckPosStatus(db, _posno, userno, ip, mac);
                //if (res.ResultNo != 0) goto OnExit;

                //int shiftno = (int)res.Param[0];

                #endregion
                #region Get contract status and balance

                bool iscontract = false;
                int contractbalancemethod = 0;
                int contractbalancetype = 0;
                decimal contractbalance = 0;

                /***********************************************
                 * Тухайн гэрээ буюу бүртгэлийн үлдэгдэл
                 * борлуулалт хийж байгаа дүнд хүрэлцэж байна уу үгүй юу гэж шалгаж байна.
                 * Шалгахдаа гэрээний балансын төрлийг шалгаж байгаа. 0 - Улайхгүй, 1 - Улайна
                 ***********************************************/

                if (!string.IsNullOrEmpty(_contractno))
                {
                    sql = @"select balancetype,balance,vat from contractmain where contractno=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605002", _contractno);
                    if (res != null && res.ResultNo != 0) goto OnExit;
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res = new Result(22222, string.Format("[{0}] Гэрээний мэдээлэл олдсонгүй", _contractno));
                        goto OnExit;
                    }

                    iscontract = true;
                    //isvat = Static.ToInt(res.Data.Tables[0].Rows[0]["vat"]);
                    contractbalancemethod = Static.ToInt(res.Data.Tables[0].Rows[0]["balancemethod"]);
                    contractbalancetype = Static.ToInt(res.Data.Tables[0].Rows[0]["balancetype"]);
                    contractbalance = Static.ToDecimal(res.Data.Tables[0].Rows[0]["balance"]);
                }
                #endregion
                #region Get autonumeric for salesno

                IPos.Core.AutoNumEnum autonum = new AutoNumEnum();
                autonum.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 5, "", autonum);
                if (res != null && res.ResultNo != 0) goto OnExit;

                _salesno = res.ResultDesc;
                if (string.IsNullOrEmpty(_salesno))
                {
                    res = new Result(9110068, "Борлуулалтын дугаар авахад алдаа гарлаа.");
                    goto OnExit;
                }

                #endregion

                #region Insert into SalesAction

                //res = SalesAction(db, _trandate, shiftno, userno, _posno, _areacode, ip, mac);
                //if (res != null && res.ResultNo != 0) goto OnExit;
                //string actionid = res.ResultDesc;

                #endregion
                #region Insert into SalesProd

                sql = @"insert into salesprod 
(salesno,custno,prodno,prodtype,price,qty,baseprice,salesamount,discountprod
,discountsales,salestype,flag,packid,subtype,actionid,posting)
values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,0)";
                param = new object[16];
                foreach (DataRow r in _cart.Rows)
                {
                    int salestype = Static.ToInt(r["SALETYPE"]);
                    string contractno = Static.ToStr(r["CONTRACTNO"]);
                    if (!string.IsNullOrEmpty(contractno)) salestype = 3;

                    res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn605002"
                        , _salesno
                        , r["CUSTNO"]
                        , r["PRODNO"]
                        , r["PRODTYPE"]
                        , r["SALESPRICE"]
                        , r["QTY"]
                        , r["PRICE"]
                        , r["SALESAMOUNT"]
                        , r["DISCOUNTPROD"]
                        , r["DISCOUNTSALES"]
                        , salestype    //saletype: 0-борлуулалт, 1-Бонус бараа, 2-Оноо, 3-Гэрээний бараа
                        , 'N'     //flag=N: Normal transaction
                        , r["PACKID"]
                        , r["SUBTYPE"]
                        , actionid
                        );
                    if (res != null && res.ResultNo != 0) goto OnExit;
                }

                #endregion
                #region Insert into SalesRent

                res = GetSalesInv(db, _salesno, 8 /*select only rental items*/);
                if (res != null && res.ResultNo != 0) goto OnExit;
                DataTable dtTools = res.Data.Tables[0];

                sql = @"insert into salesrent (salesno,custno,prodno,prodtype,itemno,rentstatus,servicetime,serviceid) values (:1,:2,:3,:4,:5,:6,:7,:8)";

                int itemno = 0;
                //param = new object[7];
                foreach (DataRow r in dtTools.Rows)
                {
                    /************************************************
                     * Тоо ширхэг тус бүрээр бичлэг болгож оруулна.
                     * 5ш гэвэл 5н бичлэг болгоно. ItemNo = 1..5
                     ************************************************/
                    int qty = Static.ToInt(r["QTY"]);
                    for (int i = 1; i <= qty; i++)
                    {
                        res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn605002"
                            , _salesno
                            , r["CUSTNO"]
                            , r["PRODNO"]
                            , r["PRODTYPE"]
                            , ++itemno
                            , 0
                            , r["SERVTIME"]
                            , r["SERVID"]
                            );
                        if (res != null && res.ResultNo != 0) goto OnExit;
                    }
                }

                #endregion
                #region Insert Services with tagtime into SalesRent

                sql = @"select ss.salesno,ss.custno,sm.servid prodno,ss.prodtype,ss.qty,sm.tagtime
from (select s.salesno,s.custno
    ,decode(s.prodtype,2,p.prodid,s.prodno) prodno
    ,decode(s.prodtype,2,p.prodtype,s.prodtype) prodtype
    ,decode(s.prodtype,2,p.count*s.qty,s.qty) qty
    from salesprod s
    left join packitem p on p.packid=s.prodno and s.prodtype=2
    where s.salesno=:1 and s.prodtype in (1,2)
) ss
left join servmain sm on sm.servid=ss.prodno and ss.prodtype=1
where (ss.prodtype=1 and sm.tagtime>0)
";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605002", _salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                dtTools = res.Data.Tables[0];

                sql = @"insert into salesrent (salesno,custno,prodno,prodtype,itemno,rentstatus,tagchargetime) values (:1,:2,:3,:4,:5,:6,:7)";

                foreach (DataRow r in dtTools.Rows)
                {
                    /************************************************
                     * Тоо ширхэг тус бүрээр бичлэг болгож оруулна.
                     * 5ш гэвэл 5н бичлэг болгоно. ItemNo = 1..5
                     ************************************************/
                    int qty = Static.ToInt(r["QTY"]);
                    for (int i = 1; i <= qty; i++)
                    {
                        res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn605002"
                            , _salesno
                            , r["CUSTNO"]
                            , r["PRODNO"]
                            , r["PRODTYPE"]
                            , ++itemno
                            , 0
                            , r["TAGTIME"]
                            );
                        if (res != null && res.ResultNo != 0) goto OnExit;
                    }
                }

                #endregion
                #region Insert into Sales

                sql = @"insert into sales(salesno,custno,vat,curcode,contractno,orderno,pledgeno,corrsalesno,flag) values(:1,:2,:3,:4,:5,:6,:7,:8,'N')";
                param = new object[] { _salesno, _custo, _vatamount, "MNT", _contractno, _orderno, _pledgeno, _corrsalesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "SalesNew", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region Check contract balance
                if (iscontract && contractbalancemethod != 0)
                {
                    decimal totalamount = 0;
                    if (contractbalancemethod == 1)
                        totalamount = _cart.AsEnumerable().Where(x => Static.ToStr(x["CONTRACTNO"]) != "").Sum(x =>
                            -Static.ToDecimal(x["DISCOUNTPROD"])
                            - Static.ToDecimal(x["DISCOUNTSALES"])
                            - (Static.ToDecimal(x["SALESPRICE"]) - Static.ToDecimal(x["UNITPRICE"])) /*DISCOUNT PACK*/
                        );
                    else if (contractbalancemethod == 2)
                        totalamount = _cart.AsEnumerable().Sum(x =>
                            Static.ToDecimal(x["SALESAMOUNT"])
                            - Static.ToDecimal(x["DISCOUNTPROD"])
                            - Static.ToDecimal(x["DISCOUNTSALES"])
                            - (Static.ToDecimal(x["SALESPRICE"]) - Static.ToDecimal(x["UNITPRICE"])) /*DISCOUNT PACK*/
                        );

                    if (totalamount < 0) totalamount = 0;

                    #region Гэрээн үлдэгдэл хүрэлцэж бгааг шалгах
                    if (contractbalance < totalamount && contractbalancetype == 0)
                    {
                        res = new Result(22223
                            , string.Format("[{0}] Гэрээний үлдэгдэл хүрэлцэхгүй байна. Үлдэгдэл = {1}", _contractno, contractbalance));
                        goto OnExit;
                    }
                    else
                    {
                        #region Гэрээний үлдэгдлийг борлуулалтын дүнгээр бууруулах
                        sql = "update contractmain set balance=balance-:2 where contractno=:1 and balancetype=1";
                        res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn605002", _contractno, totalamount);
                        if (res != null && res.ResultNo != 0)
                        {
                            res = new Result(22224
                                , string.Format("[{0}] Гэрээний үлдэгдэлийг бууруулахад алдаа гарлаа.\r\n{1}", _contractno, res.ResultDesc));
                            goto OnExit;
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                // Борлуулалтын дугаараа буцаана.
                res.ResultDesc = _salesno;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
        OnExit:
            return res;
        }
        public Result SalesExtend(DbConnections db, string actionid, object[] param)
        {
            #region Comment!

            /*******************************************************************
             * 
             * Шинээр борлуулалтын бичлэг оруулах.
             * Sales тэйблийн бичлэгт байгаа дараах утгуудыг шинэчилнэ. 
             * Үүнд:
             *      SalesAmount   = Борлуулалтын нийт дүн * (1 - VAT%)
             *      Discount      = Барааны хөнгөлөлтийн нийт дүн.
             *      DiscountSales = Борлуулалтаас тооцох хөнгөлөлтийн дүн.
             *      TotalAmount   = SalesAmount - Discount - DiscountSales
             *      VAT           = TotalAmount * VAT%
             * 
             * ХӨНГӨЛӨЛТ ТООЦОХ
             * Дараах байдлаар тооцно:
             * 
             *  - Шууд гэрээний хүрээнд борлуулалт хийдсэн бол гэрээнээс НӨАТ тооцох эсэхийг авах. 
             *    Талбар: ContractMain.Vat
             *    
             *  - Үйлчлүүлэгч бол гишүүн эсэхийг шалгах ба гишүүний бүртгэлээс НӨАТ тооцох эсэхийг авах ???
             *  
             *******************************************************************/

            #endregion

            Result res = null;
            string sql = null;
            try
            {
                #region Prepare parameters

                //DateTime _trandate = Static.ToDate(param[0]);
                //string _posno = Static.ToStr(param[1]);
                //string _areacode = Static.ToStr(param[2]);

                string _salesno = Static.ToStr(param[3]);
                decimal _custo = Static.ToDecimal(param[4]);
                string _contractno = Static.ToStr(param[5]);
                string _orderno = Static.ToStr(param[6]);
                decimal _discount = Static.ToDecimal(param[7]);
                decimal _vatamount = Static.ToInt(param[8]); // 0-No VAT, 1-VAT үүнийг гэрээний бүртгэлээс давхар авна.
                string _pledgeno = Static.ToStr(param[9]);
                DataTable _cart = (DataTable)param[10];

                //decimal vat = isvat == 1 ? SystemProp.VAT : 0;

                #endregion
                #region Посын ээлж эхлүүлсэн хэрэглэгч таарч бга эсэхийг шалгах

                //res = CheckPosStatus(db, _posno, userno, ip, mac);
                //if (res.ResultNo != 0) goto OnExit;

                //int shiftno = (int)res.Param[0];

                #endregion
                #region Get contract status and balance

                bool iscontract = false;
                int contractbalancetype = 0;
                int contractbalancemethod = 0;
                decimal contractbalance = 0;

                /***********************************************
                 * Тухайн гэрээ буюу бүртгэлийн үлдэгдэл
                 * борлуулалт хийж байгаа дүнд хүрэлцэж байна уу үгүй юу гэж шалгаж байна.
                 * Шалгахдаа гэрээний балансын төрлийг шалгаж байгаа. 0 - Улайхгүй, 1 - Улайна
                 ***********************************************/

                if (!string.IsNullOrEmpty(_contractno))
                {
                    sql = @"select balancemethod,balancetype,balance,vat from contractmain where contractno=:1";
                    res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605002", _contractno);
                    if (res != null && res.ResultNo != 0) goto OnExit;
                    if (res.Data.Tables[0].Rows.Count == 0)
                    {
                        res = new Result(22222, string.Format("[{0}] Гэрээний мэдээлэл олдсонгүй", _contractno));
                        goto OnExit;
                    }

                    iscontract = true;
                    //isvat = Static.ToInt(res.Data.Tables[0].Rows[0]["vat"]);
                    contractbalancemethod = Static.ToInt(res.Data.Tables[0].Rows[0]["balancemethod"]);
                    contractbalancetype = Static.ToInt(res.Data.Tables[0].Rows[0]["balancetype"]);
                    contractbalance = Static.ToDecimal(res.Data.Tables[0].Rows[0]["balance"]);
                }
                #endregion
                #region Extend Operation

                #region SQL бэлдэх
                sql = @"update salesprod set qty=qty+:5
,salesamount=price*(qty+:5),discountprod=discountprod+discountprod/qty*:5
where salesno=:1 and custno=:2 and prodno=:3 and prodtype=:4";

                string sql2 = @"update salesrent set servicetime=servicetime+:4
where salesno=:1 and custno=:2 and serviceid=:3";
                string sql2b = @"update salesrent set tagchargetime=tagchargetime+:4
where salesno=:1 and custno=:2 and prodno=:3 and prodtype=1";

                string sql3 = @"select tagtime,servicetime from servmain where servid=:1";

                string sql4 = @"update sales set vat=vat+:2 where salesno=:1";

                #endregion

                foreach (DataRow r in _cart.Rows)
                {
                    int prodtype = Static.ToInt(r["PRODTYPE"]);
                    decimal extend = Static.ToDecimal(r["EXTEND"]);
                    int tagtime = 0;
                    int servtime = 0;

                    if (extend > 0)
                    {
                        #region Үйлчилгээний бүртгэлээс TagTime, ServiceTime олох
                        res = db.ExecuteQuery("core", sql3, enumCommandType.SELECT, "Extend", r["PRODNO"]);
                        if (res != null && res.ResultNo != 0) goto OnExit;
                        
                        //if (res.AffectedRows <= 0)
                        //{
                        //    res = new Result(6050201, string.Format("[{0}] дугаартай үйлчилгээ бүртгэгдээгүй байна.", r["PRODNO"]));
                        //    goto OnExit;
                        //}
                        if (prodtype == 1 /*хэрэв үйлчилгээ бол*/)
                        {
                            DataTable dt = res.Data.Tables[0];
                            tagtime = Static.ToInt(dt.Rows[0]["TAGTIME"]);
                            servtime = Static.ToInt(dt.Rows[0]["SERVICETIME"]);
                        }

                        #endregion
                        #region SalesProd тэйбэлд тоо ширхэгийг ахиулах
                        res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Extend"
                            , _salesno
                            , r["CUSTNO"]
                            , r["PRODNO"]
                            , prodtype
                            , extend
                            );
                        if (res != null && res.ResultNo != 0) goto OnExit;
                        #endregion
                        #region SalesRent тэйбэлд ServiceTime хугацааг сунгах
                        if (prodtype == 1 /*is service*/)
                        {
                            res = db.ExecuteQuery("core", sql2, enumCommandType.UPDATE, "Extend"
                                , _salesno
                                , r["CUSTNO"]
                                , r["PRODNO"]
                                , servtime * extend
                                );
                            if (res != null && res.ResultNo != 0) goto OnExit;
                        }
                        #endregion
                        #region SalesRent тэйбэлд TagTime хугацааг сунгах
                        if (prodtype == 1 /*is service*/)
                        {
                            res = db.ExecuteQuery("core", sql2b, enumCommandType.UPDATE, "Extend"
                                , _salesno
                                , r["CUSTNO"]
                                , r["PRODNO"]
                                , tagtime * extend
                                );
                            if (res != null && res.ResultNo != 0) goto OnExit;
                        }
                        #endregion
                    }
                }

                #region Sales тэйбэлд НӨАТ дүнг шинэчлэх

                res = db.ExecuteQuery("core", sql4, enumCommandType.UPDATE, "Extend"
                    , _salesno
                    , _vatamount
                    );
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion


                #endregion
                #region Check contract balance
                //дараа нь оруулая, мартваа!
                #endregion

                // Борлуулалтын дугаараа буцаана.
                res.ResultDesc = _salesno;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
        OnExit:
            return res;
        }
        public Result SalesCorrection(DbConnections db, string actionid, string corrsalesno, object[] param)
        {
            #region Comment!

            /*******************************************************************
             * 
             * Борлуулалтын бичлэг засварлах.
             *  
             * 1. Өмнөх борлуулалтыг бүхэлд буцаана. Flag = C
             * 2. Шинэ борлуулалт хийнэ. Sales.CorrSalesNo талбар дээр хуучин
             *    борлуулалтын дугаарыг өгнө.
             *******************************************************************/

            #endregion

            Result res = null;
            string sql = null;
            try
            {
                #region Prepare parameters

                DateTime _trandate = Static.ToDate(param[0]);
                string _posno = Static.ToStr(param[1]);
                string _areacode = Static.ToStr(param[2]);

                /*********************************************************
                 * Param[3] дээр ориг борлуулалтын дугаар орж ирнэ.
                 * Уг борлуулалтыг бүхэлд нь буцааж шинэ борлуулалт хийнэ.
                 *********************************************************/
                string _salesno = Static.ToStr(param[3]);

                #endregion

                #region Өмнөх борлуулалтыг бүхэлд нь буцаах

                sql = "update sales set flag='C' where salesno=:1";
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "SalesCorrection", _salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
        OnExit:
            return res;
        }
        public Result SalesPayment(DbConnections db, string actionid, string salesno, object[] param)
        {
            #region Comment!
            /****************************************************************************************
             * PARAMETER DESCRIPTION:
             * 
             * salesno, paymentno, paymenttype, paymentflag, amount, txnflag, contractno, areacode, posno
             * Дээрх бүтэц бүхий олон бичлэг терминалаас ирнэ.
             * param = Object[ object[], object[], ...  ] гэсэн байдлаар.
             * 
             * 
             * TABLE DESCRIPTION:
             * 
             * PaPayType.PaymentFlag    = 0 бэлэн гүйлгээний төрөл
             *                          = 1 картын гүйлгээний төрөл
             *                          = 9 бусад гүйлгээний төрөл, гэрээ, төлбөрийн даалгавар, ваучир гм.
             *                          
             * ContractMain.BalanceType = 0 улайхгүй
             *                          = 1 улайж болно
             * PaContractType.Method    = 0 үлдэгдэл нь борлуулалтаар буурна
             *                          = 1 үлдэгдэл нь авто буурна
             *                          = 2 үлдэгдэл хөтлөхгүй
             ****************************************************************************************/
            #endregion
            Result res = null;
            string sql = null;
            try
            {
                #region Prepare parameters

                object[] rows = param;

                #endregion
                #region Validation

                if (rows == null)
                {
                    res = new Result(6050111, "Төлбөрийн мэдээлэл буруу байна.");
                    goto OnExit;
                }

                #endregion

                #region Шинэ төлбөрийн гүйлгээний дугаар авах

                IPos.Core.AutoNumEnum enums = new IPos.Core.AutoNumEnum();
                enums.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 9, "", enums);
                if (res != null && res.ResultNo != 0) return res;

                //string paymentno = string.Format("{0}-{1}", ri.ReceivedParam[2], res.ResultDesc);
                string paymentno = string.Format("{0}", res.ResultDesc);

                #endregion

                for (int i = 0; i < rows.Length; i++)
                {
                    #region Төлбөрийн гүйлгээний утгуудаа авах

                    object[] row = (object[])rows[i];

                    if (string.IsNullOrEmpty(salesno))
                        salesno = Static.ToStr(row[0]);

                    //string paymentno = Static.ToStr(row[1]);  //шинэ борлуулалт дээр үүнийг ашиглахгүй.
                    string paymenttype = Static.ToStr(row[2]);
                    int paymentflag = Static.ToInt(row[3]);
                    decimal amount = Static.ToDecimal(row[4]);
                    string txnflag = Static.ToStr(row[5]);
                    string regno = Static.ToStr(row[6]);
                    //string areacode = Static.ToStr(row[7]);
                    //string posno = Static.ToStr(row[8]);
                    string detail = Static.ToStr(row[9]);

                    #endregion

                    if (amount != 0)
                    {
                        #region POSNO дугаараар зөвшөөрөгдөх төлбөрийн хэрэгслийг шалгах
                        #endregion
                        #region PaymentType төлбөрийн төрлийг тодорхойлох

                        /**************************************************************************
                     * Хэрэв PaymentType нь хоосон бол PaymentFlag аар төрлийн кодыг олно.
                     * Бэлэн, Картын төлбөрийн үед л ингэж хоосон ирэх боломжтой.
                     **************************************************************************/
                        if (string.IsNullOrEmpty(paymenttype))
                        {
                            sql = "select typeid,paymentflag,contracttype,contractcheck from papaytype where paymentflag=:1 and rownum=1";
                            param = new object[] { paymentflag };
                        }
                        else
                        {
                            sql = "select typeid,paymentflag,contracttype,contractcheck from papaytype where typeid=:1";
                            param = new object[] { paymenttype };
                        }
                        res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605011", param);
                        if (res != null && res.ResultNo != 0) goto OnExit;
                        if (res.AffectedRows <= 0)
                        {
                            res = new Result(6050112, "Бэлэн эсвэл Картын төлбөрийн төрөл тодорхойгүй байна.");
                            goto OnExit;
                        }

                        DataTable dt = res.Data.Tables[0];
                        paymenttype = Static.ToStr(dt.Rows[0]["TYPEID"]);
                        string contracttype = Static.ToStr(dt.Rows[0]["CONTRACTTYPE"]);
                        int contractcheck = Static.ToInt(dt.Rows[0]["CONTRACTCHECK"]);

                        #endregion

                        /**************************************************************************
                     * Гэрээний боловсруулалтын хэсэг.
                     * Бэлэн болон Картын төлбөрийн төрлөөс бусад үед хийгдэнэ.
                     * contractcheck - Гэрээний бүртгэлийг шалгах эсэх
                     **************************************************************************/
                        if (paymentflag > 1 && contractcheck > 0)
                        {
                            #region Гэрээний бүртгэлийн дугаарыг шалгах
                            if (string.IsNullOrEmpty(regno))
                            {
                                res = new Result(6050113, "Төлбөрийн хэрэгслийн бүртгэлийн дугаараа оруулна уу.");
                                goto OnExit;
                            }

                            #region Prepare sql
                            sql = @"select cm.contracttype
,cm.validstartdate,cm.validstarttime
,cm.validenddate,cm.validendtime
,cm.balance,cm.balancetype,cm.curcode,cm.status,ct.method
from contractmain cm
left join pacontracttype ct on ct.contracttype=cm.contracttype 
where cm.contractno=:1
and cm.contracttype=(select contracttype from papaytype where typeid=:2 and rownum=1)";
                            #endregion
                            param = new object[] { regno, paymenttype };
                            res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605011", param);
                            if (res != null && res.ResultNo != 0) goto OnExit;
                            if (res.AffectedRows <= 0)
                            {
                                res = new Result(6050113, string.Format("[{0}] Дугаартай төлбөрийн хэрэгсэл бүртгэгдээгүй байна.", regno));
                                goto OnExit;
                            }

                            dt = res.Data.Tables[0];
                            DataRow r = dt.Rows[0];
                            #endregion
                            #region Гэрээний төлөв шалгах
                            if (Static.ToInt(r["STATUS"]) == 0)
                            {
                                res = new Result(6050113, string.Format("[{0}] Төлбөрийн хэрэгсэл идэвхгүй байна.", regno));
                                goto OnExit;
                            }
                            #endregion
                            #region Гэрээний хүчинтэй хугацаа шалгах
                            if (Static.ToDate(r["VALIDSTARTDATE"]) > DateTime.Now || Static.ToDate(r["VALIDENDDATE"]) < DateTime.Now)
                            {
                                res = new Result(6050113, string.Format("[{0}] Төлбөрийн хэрэгслийн хүчинтэй хугацаа таарахгүй байна.", regno));
                                goto OnExit;
                            }
                            #endregion
                            #region Гэрээний үлдэгдлийг хөдөлгөх
                            if (Static.ToInt(r["METHOD"]) == 0)
                            {
                                decimal balance = Static.ToDecimal(r["BALANCE"]);
                                int balancetype = Static.ToInt(r["BALANCETYPE"]);
                                if (balance < amount && balancetype == 0 /*улайхгүй*/)
                                {
                                    res = new Result(6050114, string.Format("[{0}] Төлбөрийн хэрэгслийн үлдэгдэл хүрэлцэхгүй байна.", regno));
                                    goto OnExit;
                                }

                                sql = "update contractmain set balance=balance-:2 where contractno=:1";
                                param = new object[] { regno, amount };
                                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn605011", param);
                                if (res != null && res.ResultNo != 0) goto OnExit;
                            }
                            #endregion
                        }

                        /**************************************************************************
                         * Төлбөрийн гүйлгээний бичилт хийх.
                         **************************************************************************/
                        #region Төлбөрийн гүйлгээ бичих

                        sql = @"insert into salestxn (salesno,paymentno,paymenttype,sourceno,amount,flag,actionid,detail)
values(:1,:2,:3,:4,:5,:6,:7,:8)";
                        param = new object[] { salesno, paymentno, paymenttype, regno, amount, txnflag, actionid, detail };
                        res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "Txn605011", param);
                        if (res != null && res.ResultNo != 0) goto OnExit;

                        #endregion
                    }
                }
                res.ResultDesc = paymentno;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        }
        public Result RefundSales(DbConnections db, string actionid, string salesno)
        {
            /**************************************************************
             * 
             * 
             **************************************************************/
            Result res = null;
            string newsalesno = null;
            string newpaymentno = null;
            try
            {
                #region Prepare parameters

                #endregion
                #region Посын ээлж эхлүүлсэн хэрэглэгч таарч бга эсэхийг шалгах

                //res = CheckPosStatus(db, posno, userno, ip, mac);
                //if (res.ResultNo != 0) goto OnExit;

                //int shiftno = (int)res.Param[0];

                #endregion
                #region Борлуулалтын үндсэн мэдээлэл авах

//                string sql = @"select custno,vat,curcode,contractno,orderno 
//,(select sum(salesamount - (discountprod+price-salesprice)*qty - discountsales) totalamount 
//from salesprod where salesno=:1) totalamount
//from sales where salesno=:1";
//                object[] param = new object[] { salesno };
//                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "RefundSales", param);
//                if (res != null && res.ResultNo != 0) goto OnExit;

//                if (res.AffectedRows <= 0)
//                {
//                    res = new Result(6050141, "[{0}] дугаартай борлуулалт олдсонгүй.");
//                    goto OnExit;
//                }

//                DataTable dt = res.Data.Tables[0];
//                decimal custno = Static.ToDecimal(dt.Rows[0]["CUSTNO"]);
//                decimal vat = Static.ToDecimal(dt.Rows[0]["VAT"]);
//                string curcode = Static.ToStr(dt.Rows[0]["CURCODE"]);
//                string contractno = Static.ToStr(dt.Rows[0]["CONTRACTNO"]);
//                string orderno = Static.ToStr(dt.Rows[0]["ORDERNO"]);
//                decimal totalamount = Static.ToDecimal(dt.Rows[0]["TOTALAMOUNT"]);

                #endregion
                #region Get autonumeric for salesno

                //IPos.Core.AutoNumEnum autonum = new AutoNumEnum();
                //autonum.Y = Static.ToStr(Static.ToDate(DateTime.Now).Year);
                //res = IPos.Core.SystemProp.gAutoNum.GetNextNumber(db, 5, autonum);
                //if (res != null && res.ResultNo != 0) goto OnExit;

                //newsalesno = res.ResultDesc;
                //if (string.IsNullOrEmpty(newsalesno))
                //{
                //    res = new Result(9110068, "Борлуулалтын дугаар авахад алдаа гарлаа.");
                //    goto OnExit;
                //}

                #endregion

                //db.BeginTransaction("core", "");

                #region SalesProd - Борлуулсан барааны жагсаалтыг бөөнд нь буцаах

                #region SQL
                string sql = @"insert into salesprod 
(salesno,custno,prodno,prodtype,price,qty,baseprice,salesamount
,discountprod,discountsales,salestype,flag,packid,subtype,actionid)
select :1,custno,prodno,prodtype,price
,-sum(qty) qty,max(baseprice) baseprice
,-max(salesamount) salesamount
,-max(discountprod) discountprod
,-max(discountsales) discountsales
,max(salestype) saletype
,'R' flag
,max(packid) packid
,max(subtype) subtype
,:2 actionid
from salesprod 
where salesno=:1
group by custno,prodno,prodtype,price
having sum(qty)>0
";

                #endregion
                object[] param = new object[] { salesno, actionid };
                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "RefundSales", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region SalesTxn - Төлбөрийн гүйлгээнүүдийг бөөнд нь буцаах

                sql = @"select paymenttype,sourceno,sum(amount) amount
from salestxn s
where s.salesno=:1
group by paymenttype,sourceno
having sum(amount)>0";
                param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "RefundSales", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    DataTable dt = res.Data.Tables[0];
                    ArrayList rows = new ArrayList();
                    foreach (DataRow r in dt.Rows)
                    {
                        param = new object[]
                        {
                            salesno /*salesno*/
                            ,0 /*paymentno*/
                            , r["PAYMENTTYPE"]
                            ,0 /*paymentflag*/
                            ,-1* Static.ToDecimal (r["AMOUNT"])
                            ,'R' /*flag=R refund*/
                            ,r["SOURCENO"]
                            ,actionid
                        };
                        rows.Add(param);
                    }
                    res = SalesPayment(db, actionid, salesno, rows.ToArray());
                    if (res != null && res.ResultNo != 0) goto OnExit;
                    newpaymentno = res.ResultDesc;
                }

                #endregion
                #region SalesRent - Түрээсийн хэрэгслүүдийг буцсан төлөвт оруулах

                
                #endregion
                #region Sales - Буцаалтын гүйлгээний үндсэн бичилт үүсгэх

//                sql = @"
//insert into sales(trandate,salesno,custno
//,vat,curcode,posno,cashierno,ip,mac
//,status,postdate,areacode,contractno,orderno,shiftno)
//values(:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15)";

//                param = new object[]{
//                      SystemProp.TxnDate, newsalesno, custno
//                      , vat, "MNT", posno, userno, ip, mac
//                      , 2 /*Буцаалтын гүйлгээний флаг*/
//                      , DateTime.Now, areacode, contractno, orderno, shiftno 
//                };

//                res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "RefundSales", param);
//                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
                #region Sales - Эх гүйлгээг буцаагдсан төлөвт оруулах

                sql = @"update sales set flag='R' where salesno=:1";
                param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "RefundSales", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion

                res.Param = new object[] { newsalesno, newpaymentno };
                res.Data = null;

            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
        OnExit:
            return res;
        }    //Борлуулалтын бичилт буцаах
        public Result RefundProd(DbConnections db, string actionid, string salesno, decimal custno, string prodno, int prodtype, decimal price, decimal qty)
        {
            Result res = null;
            #region SalesProd - Үндсэн бараа үйлчилгээний бичлэгийг Flag='C' төлөвт оруулах

//            #region SQL
//            string sql = @"update salesprod set flag='C'
//where salesno=:1 and custno=:2 and prodno=:3 and prodtype=:4 and price=:5
//and flag='N' and rownum=1";

//            #endregion
//            object[] param = new object[] { salesno, custno, prodno, prodtype, price };
//            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "RefundSales", param);
//            if (res != null && res.ResultNo != 0) goto OnExit;

            #endregion
            #region SalesProd - Борлуулсан барааны жагсаалтыг бөөнд нь буцаах

            #region SQL
            string sql = @"insert into salesprod
(salesno,custno,prodno,prodtype,price,qty,baseprice,salesamount
,discountprod,discountsales,salestype,flag,packid,subtype,actionid)

select salesno,custno,prodno,prodtype,price,:6 qty,baseprice
,:6*price salesamount
,discountprod
,discountsales
,salestype
,'R' flag
,packid
,subtype
,:7 actionid
from salesprod 
where salesno=:1 and custno=:2 and prodno=:3 and prodtype=:4 and price=:5
and flag='N' and rownum=1
";

            #endregion
            object[] param = new object[] { salesno,custno,prodno,prodtype, price, -qty, actionid };
            res = db.ExecuteQuery("core", sql, enumCommandType.INSERT, "RefundSales", param);
            if (res != null && res.ResultNo != 0) goto OnExit;

            #endregion
            
            #region SalesRent

            #endregion
            
        OnExit:
            return res;
        }

        public Result Txn605001(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters
                object[] param = ri.ReceivedParam;

                string salesno = Static.ToStr(param[0]);
                string custno = Static.ToStr(param[1]);
                string regno = Static.ToStr(param[2]);
                string custname1 = Static.ToStr(param[3]);
                string custname2 = Static.ToStr(param[4]);
                string corpname = Static.ToStr(param[5]);
                DateTime trandate = Static.ToDate(param[6]);
                bool chkoriginal = Static.ToBool(param[7]);
                bool chkrefund = Static.ToBool(param[8]);

                string serialno = Static.ToStr(param[9]);
                string tagframeno = Static.ToStr(param[10]);

                bool chkcorrection = param.Length >= 11 ? Static.ToBool(param[11]) : false;

                #endregion
                #region Prepare query
                string sql = @"
select a.salesno, b.custno, c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,b.salesamount,b.discountprod+b.discountsales discount
,a.vat
,(b.salesamount-b.discountprod-b.discountsales) + case when a.vat>0 then 0 else a.vat end totalamount
,(select nvl(sum(amount),0) paid from salestxn where salesno=a.salesno) paid
,sa.posno,sa.postdate,a.contractno,a.orderno,sa.trandate,sa.shiftno,sa.cashierno
,decode(u.userfname,null,'',substr(u.userfname,1,1)||'.')||u.userlname username
,cid.serialno,cid.tagframeno,a.pledgeno, a.flag,a.corrsalesno
from sales a
left join (
select sp.salesno,sp.custno,sp.actionid
,sum(salesamount) salesamount
,sum(discountprod + (baseprice-price)) discountprod
,sum(discountsales) discountsales
from salesprod sp
group by sp.salesno,sp.custno,sp.actionid
) b on a.salesno=b.salesno
left join customer c on b.custno=c.customerno
left join salesaction sa on sa.actionid=b.actionid
left join hpuser u on u.userno=sa.cashierno
left join customeriddevice cid on cid.custno=b.custno
{0}
order by 1 desc, 4
";

                #endregion
                #region Prepare filter text

                StringBuilder sb = new StringBuilder();
                ArrayList values = new ArrayList();

                if (Static.ToDate(trandate) != DateTime.MinValue)
                {
                    values.Add(trandate);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("sa.trandate=:{0}", values.Count);
                }
                if (!string.IsNullOrEmpty(salesno))
                {
                    values.Add(salesno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("a.salesno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(custno))
                {
                    values.Add(custno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("b.custno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(custname1))
                {
                    values.Add(custname1);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.firstname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(custname2))
                {
                    values.Add(custname2);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.lastname like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(corpname))
                {
                    values.Add(corpname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.corporatename like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(regno))
                {
                    values.Add(corpname);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("c.registerno like :{0}||'%'", values.Count);
                }

                if (!string.IsNullOrEmpty(serialno))
                {
                    values.Add(serialno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("cid.serialno like :{0}||'%'", values.Count);
                }
                if (!string.IsNullOrEmpty(tagframeno))
                {
                    values.Add(tagframeno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("cid.tagframeno like :{0}||'%'", values.Count);
                }

                if (!string.IsNullOrEmpty(tagframeno))
                {
                    values.Add(tagframeno);
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("cid.tagframeno like :{0}||'%'", values.Count);
                }

                string flags = "";
                if (chkoriginal) flags += ",'N'";
                if (chkrefund) flags += ",'R'";
                if (chkcorrection) flags += ",'C'";

                if (flags != "")
                {
                    if (sb.Length > 0) sb.Append(" and ");
                    sb.AppendFormat("a.flag in (' '{0})", flags);
                }

                if (sb.Length > 0) sb.Insert(0, " where ");
                sql = string.Format(sql, sb.ToString());

                #endregion
                #region Execute query
                res = db.ExecuteQuery("core", sql, "Txn605001", ri.PageIndex, ri.PageRows, values.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Борлуулалтын хайлт жагсаалт
        public Result Txn605002(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            //db.BeginTransaction("core", "");
            ////res = SalesAction(db, 
            //res = SalesNew(db, ri.UserNo, ci.ClientIp, ci.ClientId, ri.ReceivedParam);
            //if (res.ResultNo == 0) db.Commit("core");
            //else db.RollBack("core");

            return res;
        }    //!!! хоосон бн. Шинэ борлуулалтын бичлэг
        public Result Txn605003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                #region Prepare parameters

                string salesno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Борлуулалтад хамрагдах үйлчлүүлэгчид

                string sql = @"select s.custno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,s.qty,cid.serialno,c.registerno,cid.tagframeno
from (select custno,count(custno) qty from salesrent where salesno=:1 group by custno) s
left join customer c on c.customerno=s.custno
left join customeriddevice cid on cid.custno=s.custno
order by 2";
                object[] param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605003", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "CUST";
                ds.Tables.Add(dt.Copy());

                res.Data.Dispose();

                #endregion
                #region Борлуулалтад түрээсийн хэрэгслүүд

                sql = @"select sr.custno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,sr.prodno
,decode(sr.prodtype,0,i.name,sm.name||' ['||sr.tagchargetime||'цаг]') prodname
,sr.itemno,sr.rentstatus
,decode(sr.prodtype,1,'ТАГ'
  ,decode(sr.rentstatus,0,'ОЛГООГҮЙ',1,'ОЛГОСОН',2,'АВСАН','ТОДОРХОЙГҮЙ')
) statusname
,sr.prodtype,sr.tagchargetime
from salesrent sr
left join customer c on c.customerno=sr.custno
left join invmain i on i.invid=sr.prodno and sr.prodtype=0
left join servmain sm on sm.servid=sr.prodno and sr.prodtype=1
where sr.salesno=:1
order by 1,4";

                param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605003", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "INV";
                ds.Tables.Add(dt.Copy());

                res.Data.Dispose();

                #endregion

                res.Data = ds;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:

            return res;
        }    //Борлуулалт дахь үйлчлүүлэгчид ба түрээсийн хэрэгслүүд
        public Result Txn605004(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string salesno = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Борлуулалтын бараа үйлчилгээний жагсаалт

                string sql = @"select s.custno,c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,s.prodno,s.prodtype,s.qty,s.baseprice,s.price,s.salesamount
,s.discountprod,s.discountsales,s.salestype,s.packid,s.subtype
,decode(s.prodtype,0,i.name,1,m.name,2,p.name) prodname
,sm.contractno
from salesprod s
left join sales sm on sm.salesno=s.salesno
left join customer c on c.customerno=s.custno
left join invmain i on i.invid=s.prodno and s.prodtype=0
left join servmain m on m.servid=s.prodno and s.prodtype=1
left join packmain p on p.packid=s.prodno and s.prodtype=2
where s.salesno=:1
order by 3,7";
                object[] param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605004", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                //EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        }    //Борлуулалт дахь бараа үйлчилгээний жагсаалт
        public Result Txn605005(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string packid = Static.ToStr(ri.ReceivedParam[0]);

                #endregion
                #region Борлуулалтын бараа үйлчилгээний жагсаалт

                string sql = @"select p.packid,p.prodid prodno,p.prodtype,p.count qty,p.price,p.optional
,decode(p.prodtype,0,i.name,1,s.name,'') prodname
,decode(p.prodtype,0,i.invtype,1,s.servtype,'') subtype
from packitem p
left join invmain i on i.invid=p.prodid and p.prodtype=0
left join servmain s on s.servid=p.prodid and p.prodtype=1
where p.packid=:1";
                object[] param = new object[] { packid };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605005", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                //EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        }    //Багц доторх бараа үйлчилгээний жагсаалт
        public Result Txn605006(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                string salesno = Static.ToStr(ri.ReceivedParam[0]);

                #region төлбөрийн дутуу үлдсэн нийт дүнг авах

                res = GetPaymentBalance(db, salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                lg.item.Desc = "Борлуулалтын төлбөрийн дүн";
            }
        OnExit:
            return res;
        }    //Борлуулалтын төлбөрийн дүн
        public Result Txn605007(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string tagno = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Төлбөрийн гүйлгээний жагсаалт

                string sql = @"select a.salesno, a.custno, c.registerno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,b.salesamount,b.discountprod+b.discountsales discount
,b.salesamount-b.discountprod-b.discountsales totalamount,a.vat
,(select nvl(sum(amount),0) paid from salestxn where salesno=a.salesno) paid
,b.posno,b.postdate,a.contractno,a.orderno,b.trandate
from sales a
left join customer c on a.custno=c.customerno
inner join (
select salesno
,sum(salesamount) salesamount
,sum(discountprod + (baseprice-price)) discountprod
,sum(discountsales) discountsales
,min(sa.posno) posno
,min(sa.postdate) postdate
,min(sa.trandate) trandate
from salesprod sp
left join salesaction sa on sa.actionid=sp.actionid
where sp.custno = (select custno from customeriddevice where serialno=:1)
group by salesno
) b on a.salesno=b.salesno";
                object[] param = new object[] { tagno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605007", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                //EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        }    //Тагын дугаараар борлуулалтын дугаар буцаах
        public Result Txn605008(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string posno = Static.ToStr(ri.ReceivedParam[1]);
                string areacode = Static.ToStr(ri.ReceivedParam[2]);

                #endregion
                #region Тухайн AreaCode дээрх барааны жагсаалт

                string sql = @"select a.parentid, a.itemid, a.itemtype
,case 
when a.itemtype=0 then (select name from invmain where invid=a.itemid)
when a.itemtype=1 then (select name from servmain where servid=a.itemid)
when a.itemtype=2 then (select name from packmain where packid=a.itemid)
when a.itemtype=3 then (select name from prodtreedesc where itemid=a.itemid)
end itemname
,case 
when a.itemtype=0 then (select picture from invmain where invid=a.itemid)
when a.itemtype=1 then (select picture from servmain where servid=a.itemid)
end itempicture
from prodtree a
inner join workarealink w on w.areacode=:1 and w.id=a.itemid and w.producttype=a.itemtype
order by 3 desc,4
";
                object[] param = new object[] { areacode };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605008", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
        OnExit:
            return res;
        }    //Тухайн AreaCode дээрх барааны жагсаалт

        public Result Txn605009(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                int userno = Static.ToInt(ri.ReceivedParam[1]);
                string posno = Static.ToStr(ri.ReceivedParam[2]);
                string areacode = Static.ToStr(ri.ReceivedParam[3]);

                #endregion
                #region хэрэглэгчийн боломжит төлбөрийн төрлүүдийн жагсаалт авах
                
                res = GetPaymentTypes(db, ri.UserNo, posno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн жагсаалт";
            }
        OnExit:
            return res;
        }    //Хэрэглэгчийн төлбөрийн төрлийн жагсаалт
        public Result Txn605010(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string salesno = Static.ToStr(ri.ReceivedParam[1]);
                int userno = Static.ToInt(ri.ReceivedParam[2]);
                string posno = Static.ToStr(ri.ReceivedParam[3]);
                string areacode = Static.ToStr(ri.ReceivedParam[4]);

                #region төлбөрийн дутуу үлдсэн нийт дүнг авах

                res = GetPaymentBalance(db, salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0].Copy();
                dt.TableName = "TotalAmounts";
                ds.Tables.Add(dt);
                res.Data.Dispose();

                #endregion
                #region хэрэглэгчийн боломжит төлбөрийн төрлүүдийн жагсаалт авах

                res = GetPaymentTypes(db, ri.UserNo, posno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0].Copy();
                dt.TableName = "PaymentTypes";
                ds.Tables.Add(dt);
                res.Data.Dispose();

                #endregion
                res.Data = ds;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн үлдэгдэл, төрлийн жагсаалт";
            }
        OnExit:
            return res;
        }    //Төлбөрийн үлдэгдэл, төрлийн жагсаалт
        public Result Txn605011(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;

            object[] paramSales = (object[])ri.ReceivedParam[0];
            object[] paramPayment = (object[])ri.ReceivedParam[1];

            int shiftno = 0;
            string actionid = null;

            db.BeginTransaction("core", "");

            DateTime trandate = Static.ToDate(paramSales[0]);
            string posno = Static.ToStr(paramSales[1]);
            string areacode = Static.ToStr(paramSales[2]);
            string salesno = Static.ToStr(paramSales[3]);

            res = SalesAction(db, SystemProp.TxnDate, ri.UserNo, posno, areacode, ci.ClientIp, ci.ClientId, ref shiftno);
            if (res != null && res.ResultNo != 0) goto OnExit;
            actionid = res.ResultDesc;

            res = SalesPayment(db, actionid, salesno, paramPayment);
            if (res.ResultNo == 0) db.Commit("core");
            else db.RollBack("core");

            OnExit:
            return res;
        }    //Төлбөрийн гүйлгээ багцаар хийх
        public Result Txn605012(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;

            object[] paramSales = (object[])ri.ReceivedParam[0];
            object[] paramPayment = (object[])ri.ReceivedParam[1];

            int shiftno = 0;
            string actionid = null;
            string paymentno = null;

            db.BeginTransaction("core", "");

            DateTime trandate = Static.ToDate(paramSales[0]);
            string posno = Static.ToStr(paramSales[1]);
            string areacode = Static.ToStr(paramSales[2]);
            string salesno = Static.ToStr(paramSales[3]);

            res = SalesAction(db, SystemProp.TxnDate, ri.UserNo, posno, areacode, ci.ClientIp, ci.ClientId, ref shiftno);
            if (res != null && res.ResultNo != 0) goto OnExit;
            actionid = res.ResultDesc;

            res = SalesNew(db, actionid, paramSales);
            if (res != null && res.ResultNo != 0) goto OnExit;
            salesno = res.ResultDesc;

            res = SalesPayment(db, actionid, salesno, paramPayment);
            if (res != null && res.ResultNo != 0) goto OnExit;
            paymentno = res.ResultDesc;

            res.Param = new object[] { salesno, paymentno };
            res.ResultDesc = null;
            res.Data = null;

        OnExit:
            if (res.ResultNo == 0) db.Commit("core");
            else db.RollBack("core");

            return res;
        }    //Төлбөрийн гүйлгээ болон борлуулалтын гүйлгээг хамтад хийх

        public Result Txn605013(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string salesno = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Төлбөрийн гүйлгээний жагсаалт

                string sql = @"select s.salesno,sa.postdate,s.paymentno,s.paymenttype,p.name paymentname
,s.amount,s.flag,decode(s.flag,'N','ОРЛОГО','E','ХАРИУЛТ','R','БУЦААЛТ') flagname,u.userfname,s.sourceno,p.paymentflag
from salestxn s
left join salesaction sa on sa.actionid=s.actionid
left join papaytype p on p.typeid=s.paymenttype
left join hpuser u on u.userno=sa.cashierno
where s.salesno=:1
order by 2 desc";
                object[] param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605013", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                //EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        }    //Төлбөрийн гүйлгээний жагсаалт
        public Result Txn605014(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string salesno = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Төлбөрийн гүйлгээний жагсаалт

                string sql = @"select s.paymenttype,s.sourceno registerno
,decode(s.sourceno,null,pt.name,pt.name||' ['||s.sourceno||']') name
,s.amount
from (
    select paymenttype,sourceno,sum(amount) amount
    from salestxn
    where salesno=:1
    group by paymenttype,sourceno
) s
left join papaytype pt on pt.typeid=s.paymenttype
order by pt.orderno,3";
                object[] param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605014", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                //EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
        OnExit:
            return res;
        }    //Төлбөрийн төлсөн, буцаасан дүнг базсан жагсаалт. Refund хийхэд дуудна.

        public Result Txn605015(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            /**************************************************
             * Борлуулалтыг бүхэлд нь буцаах
             * 
             * 1. Sales тэйбэлд шинээр борлуулалтын бичилт үүснэ.
             * 2. SalesProd дээр бараа бүрийг буцаасан бичилт Flag=1, RefTrandate, RefSalesNo үүснэ.
             * 3. SalesTxn дээр төлбөрийн буцаалтын гүйлгээ төлбөрийн төрөл бүр дээр үүснэ.
             * 4. SalesRent дээрх түрээсийн хэрэгсэл бүрийн Status=9 буцсан төлөвтэй болгоно.
             * 
             **************************************************/
            Result res = null;
            DataTable dt = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string salesno = Static.ToStr(ri.ReceivedParam[1]);
                string posno = Static.ToStr(ri.ReceivedParam[2]);
                string areacode = Static.ToStr(ri.ReceivedParam[3]);

                string actionid = null;
                int shiftno = 0;

                #endregion
                #region Шалгалт1 - Борлуулалтын төлбөр бүрэн төлөгдсөн эсэх

                res = GetPaymentBalance(db, salesno);
                if (res.ResultNo != 0) goto OnExit;

                if (res.AffectedRows > 0)
                {
                    dt = res.Data.Tables[0];
                    decimal ta = Static.ToDecimal(dt.Rows[0]["TOTALAMOUNT"]);
                    decimal pa = Static.ToDecimal(dt.Rows[0]["PAID"]);

                    if (ta > 0 && ta > pa)
                    {
                        res = new Result(6010102, "Борлуулалтын төлбөр бүрэн төлөгдөөгүй байна.");
                        goto OnExit;
                    }
                }

                #endregion
                #region Шалгалт2 - Түрээсийн торгууль төлөгдсөн эсэх

                string sql = @"select * from (
    select sr.custno,sr.salesno,sr.rentstatus status,sr.itemno,sr.prodno,im.name
    ,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
    ,case when nvl(sr.rentstatus,0)=2 and
       ((nvl(sr.servicetime,0)>0 and round((sr.rentendtime-sr.rentstarttime)*(24*60),2)-nvl(sr.servicetime,0)>0)
       or sr.damagetype is not null)
     then 1 else 0 end fined
    ,case when nvl(sr.servicetime,0)>0 and (sr.rentendtime-sr.rentstarttime)*(24*60)-nvl(sr.servicetime,0)>0 then
     round((sr.rentendtime-sr.rentstarttime)*(24*60)-nvl(sr.servicetime,0),2) else 0 end rentminutes
    from salesrent sr
    left join customeriddevice d on d.custno=sr.custno
    left join invmain im on im.invid=sr.prodno
    left join customer c on c.customerno=sr.custno
    where sr.salesno=:1
) a where a.fined=1 or a.status=1";

                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn601015", salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    res = new Result(6010103, "Түрээсийн хэрэгсэлийг бүрэн хүлээж аваагүй эсвэл торгууль төлөгдөөгүй байна.");
                    goto OnExit;
                }

                #endregion
                
                db.BeginTransaction("core", "Txn605015");

                res = SalesAction(db, trandate, ri.UserNo, posno, areacode, ci.ClientIp, ci.ClientId, ref shiftno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                actionid = res.ResultDesc;

                res = RefundSales(db, actionid, salesno);
                if (res != null && res.ResultNo != 0) goto OnExit;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                if (res != null && res.ResultNo == 0) db.Commit("core");
                else db.RollBack("core");
            }
        OnExit:
            return res;
        }    //Борлуулалтыг бүхэлд нь буцаалт
        public Result Txn605016(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            /**************************************************
             * Борлуулалтын бараа үйлчилгээг буцаах
             * 
             * 1. Sales тэйбэл дээр борлуулалтын шинэ бичилт үүснэ.
             * 2. SalesProd дээр буцаасан барааны гүйлгээ шинээр үүснэ.
             * 3. SalesTxn дээр буцаасан төлбөрийн төрлөөр бичилт үүснэ.
             * 4. SalesRent дээр буцаасан бараа үйлчилгээтэй холбоотой түрээсийн хэрэгслийг RentStatus=9 төлөвтэй болгоно.
             * 
             **************************************************/
            Result res = null;
            DataTable dt = null;
            try
            {
                #region Prepare parameters

                object[] param = (object[])ri.ReceivedParam;

                DateTime trandate = Static.ToDate(param[0]);
                string salesno = Static.ToStr(param[1]);
                string posno = Static.ToStr(param[2]);
                string areacode = Static.ToStr(param[3]);

                decimal custno = Static.ToDecimal(param[4]);
                string prodno = Static.ToStr(param[5]);
                int prodtype = Static.ToInt(param[6]);
                decimal price = Static.ToDecimal(param[7]);
                decimal qty = Static.ToDecimal(param[8]);

                object[] paramPayment = (object[])param[9];

                string actionid = null;
                string newpaymentno = null;

                #endregion
                #region Буцаах үлдэгдэл хүрэлцээтэй байгаа эсэхийг шалгах

                string sql = @"select sum(qty) qty from salesprod
where salesno=:1 and custno=:2 and prodno=:3 and prodtype=:4 and price=:5";
                param = new object[] { salesno, custno, prodno, prodtype, price };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605016", param);
                if (res != null && res.ResultNo != 0) goto OnExit;
                if (res.AffectedRows > 0)
                {
                    dt = res.Data.Tables[0];
                    decimal remain = Static.ToDecimal(dt.Rows[0]["QTY"]);
                    if (remain < qty)
                    {
                        res = new Result(6050161, string.Format("Буцаалт хийх үлдэгдэл хүрэлцээгүй байна."));
                        goto OnExit;
                    }
                }

                #endregion

                db.BeginTransaction("core", "Txn605015");

                #region SalesAction
                int shiftno = 0;

                res = SalesAction(db, trandate, ri.UserNo, posno, areacode, ci.ClientIp, ci.ClientId, ref shiftno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                actionid = res.ResultDesc;
                #endregion
                #region SalesTxn - Төлбөрийн гүйлгээнүүдийг бөөнд нь буцаах

                res = SalesPayment(db, actionid, salesno, paramPayment);
                if (res != null && res.ResultNo != 0) goto OnExit;
                newpaymentno = res.ResultDesc;

                #endregion
                #region SalesProd - Буцаалтын борлуулалтын бичилт хийх
                res = RefundProd(db, actionid, salesno, custno, prodno, prodtype, price, qty);
                #endregion

                res.Param = new object[] { 0, newpaymentno };
                res.Data = null;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                //EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
            finally
            {
                if (res != null && res.ResultNo == 0) db.Commit("core");
                else db.RollBack("core");
            }
        OnExit:
            return res;
        }    //Борлуулалт доторх бараа буцаалт
        public Result Txn605017(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            string newpaymentno = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string salesno = Static.ToStr(ri.ReceivedParam[1]);
                string posno = Static.ToStr(ri.ReceivedParam[2]);
                string areacode = Static.ToStr(ri.ReceivedParam[3]);

                /****************************************************
                 * Талбарууд:
                 * paymenttype,registerno,name,amount,refund
                 ****************************************************/
                object[] refundtxn = (object[])ri.ReceivedParam[4];

                string actionid = null;
                int shiftno = 0;

                #endregion
                #region Sales тэйблээс буцаалтын борлуулалт эсэхийг шалгах

                //string sql = @"select status from sales where trandate=:1 and salesno=:2 and status=1";
                //object[] param = new object[] { trandate, salesno };
                //res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605015", param);
                //if (res != null && res.ResultNo != 0) goto OnExit;
                //if (res.AffectedRows > 0)
                //{
                //    res = new Result(6050151, string.Format("[{0}] дугаартай борлуулалт нь буцаалтын гүйлгээ байна.", salesno));
                //    goto OnExit;
                //}

                #endregion
                #region Борлуулсан бараанаас түрээс олгогдсон эсэхийг шалгах
                

                #endregion

                db.BeginTransaction("core", "Txn605015");

                res = SalesAction(db, trandate, ri.UserNo, posno, areacode, ci.ClientIp, ci.ClientId, ref shiftno);
                if (res != null && res.ResultNo != 0) goto OnExit;
                actionid = res.ResultDesc;

                #region SalesTxn - Төлбөрийн гүйлгээнүүдийг бөөнд нь буцаах

                res = SalesPayment(db, actionid, salesno, refundtxn);
                if (res != null && res.ResultNo != 0) goto OnExit;
                newpaymentno = res.ResultDesc;

                #endregion

                res.Param = new object[] { "", newpaymentno };
                res.Data = null;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            finally
            {
                if (res != null && res.ResultNo == 0) db.Commit("core");
                else db.RollBack("core");
            }
        OnExit:
            return res;
        }    //Төлбөрийн буцаалт
        
        public Result Txn605018(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string salesno = Static.ToStr(ri.ReceivedParam[1]);
                string posno = Static.ToStr(ri.ReceivedParam[2]);
                string areacode = Static.ToStr(ri.ReceivedParam[3]);

                string prodno = Static.ToStr(ri.ReceivedParam[4]);
                int prodtype = Static.ToInt(ri.ReceivedParam[5]);
                int itemno = Static.ToInt(ri.ReceivedParam[6]);
                decimal custno = Static.ToDecimal(ri.ReceivedParam[7]);

                #endregion
                #region хэрэглэгчийн боломжит төлбөрийн төрлүүдийн жагсаалт авах

                string sql = "update salesrent set custno=:5 where salesno=:1 and prodno=:2 and prodtype=:3 and itemno=:4";
                res = db.ExecuteQuery("core", sql, enumCommandType.UPDATE, "Txn605018", salesno, prodno, prodtype, itemno, custno);
                if (res != null && res.ResultNo != 0) goto OnExit;

                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
            finally
            {
                lg.item.Desc = "Төлбөрийн төрлийн жагсаалт";
            }
        OnExit:
            return res;
        }    //Хэрэгсэлийг өөр үйлчлүүлэгч рүү шилжүүлэх
        public Result Txn605019(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            return res;
        }    //НӨАТ өөрчлөх эрх байгаа эсхийг шалгах
        public Result Txn605020(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;

            object[] paramSales = (object[])ri.ReceivedParam[0];
            object[] paramPayment = (object[])ri.ReceivedParam[1];

            int shiftno = 0;
            string actionid = null;
            string paymentno = null;

            db.BeginTransaction("core", "");

            DateTime trandate = Static.ToDate(paramSales[0]);
            string posno = Static.ToStr(paramSales[1]);
            string areacode = Static.ToStr(paramSales[2]);
            string salesno = Static.ToStr(paramSales[3]);

            res = SalesAction(db, SystemProp.TxnDate, ri.UserNo, posno, areacode, ci.ClientIp, ci.ClientId, ref shiftno);
            if (res != null && res.ResultNo != 0) goto OnExit;
            actionid = res.ResultDesc;

            res = SalesExtend(db, actionid, paramSales);
            if (res != null && res.ResultNo != 0) goto OnExit;
            salesno = res.ResultDesc;

            res = SalesPayment(db, actionid, salesno, paramPayment);
            if (res != null && res.ResultNo != 0) goto OnExit;
            paymentno = res.ResultDesc;

            res.Param = new object[] { salesno, paymentno };
            res.ResultDesc = null;
            res.Data = null;

        OnExit:
            if (res.ResultNo == 0) db.Commit("core");
            else db.RollBack("core");

            return res;
        }    //Төлбөрийн гүйлгээ болон Сунгалтын гүйлгээг хамтад хийх
        public Result Txn605021(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;

            object[] paramSales = (object[])ri.ReceivedParam[0];
            object[] paramPayment = (object[])ri.ReceivedParam[1];

            int shiftno = 0;
            string actionid = null;
            string paymentno = null;

            db.BeginTransaction("core", "");

            DateTime trandate = Static.ToDate(paramSales[0]);
            string posno = Static.ToStr(paramSales[1]);
            string areacode = Static.ToStr(paramSales[2]);
            string salesno = Static.ToStr(paramSales[3]);

            res = SalesAction(db, trandate, ri.UserNo, posno, areacode, ci.ClientIp, ci.ClientId, ref shiftno);
            if (res != null && res.ResultNo != 0) goto OnExit;
            actionid = res.ResultDesc;

            res = SalesNew(db, actionid, paramSales);
            if (res != null && res.ResultNo != 0) goto OnExit;
            salesno = res.ResultDesc;

            res = SalesCorrection(db, actionid, "", paramSales);
            if (res != null && res.ResultNo != 0) goto OnExit;

            res = SalesPayment(db, actionid, salesno, paramPayment);
            if (res != null && res.ResultNo != 0) goto OnExit;
            paymentno = res.ResultDesc;

            res.Param = new object[] { salesno, paymentno };
            res.ResultDesc = null;
            res.Data = null;

        OnExit:
            if (res.ResultNo == 0) db.Commit("core");
            else db.RollBack("core");

            return res;
        }    //Төлбөрийн гүйлгээ болон Засварын гүйлгээг хамтад хийх
                
        public Result Txn605030(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);
                string areacode = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Файлуудыг уншиж нэгтгэх

                string discount_folder = string.Format ("{0}\\Discount", Static.WorkingFolder);
                string file_prefix = string.Format("{0}_*.txt", areacode);
                string[] files = System.IO.Directory.GetFiles(discount_folder, file_prefix, SearchOption.AllDirectories);

                ArrayList data = new ArrayList();
                foreach (string file in files)
                {
                    data.Add(File.ReadAllText(file, Encoding.UTF8));
                }

                #endregion

                res = new Result();
                res.Param = data.ToArray();
            }
            catch (Exception ex)
            {
                res = new Result(605030, "Тооцоолол хийхэд алдаа гарлаа. " + ex.Message);
                //EServ.Shared.Static.WriteToLogFile("Error.log", ex.ToString());
            }
            finally
            {
                lg.item.Desc = "Хөнгөлөлтийн мэдээлэл авах";
            }
        //OnExit:
            return res;
        }    //Хөнгөлөлтийн файлуудыг нэгтгэж илгээх
        
        public Result Txn605040(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                string posno = Static.ToStr(ri.ReceivedParam[0]);
                string areacode = Static.ToStr(ri.ReceivedParam[1]);

                #endregion
                #region Prepare query
                string sql = @"
select 
nvl(sp.trandate,st.trandate) trandate
,nvl(sp.salesno,st.salesno) salesno
,s.custno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,decode(sp.flag,'N','БОРЛ','R','БУЦ','E','ХАРИУЛТ') flag
,sp.salesamount,sp.discount,sp.amount
,sp.amount*s.vat/100 vat
,st.totalpaid,st.cash,st.card,st.other
,decode(s.corrsalesno,null,decode(s.flag,'C','ЗАСАГДСАН', 'ХЭВИЙН'),'ЗАССАН '||s.corrsalesno) corr  
from (
    select sa1.trandate,sp1.salesno,sp1.flag
    ,sum(sp1.price*sp1.qty) salesamount
    ,sum(sp1.price*sp1.discountprod + sp1.discountsales) discount
    ,sum(sp1.qty*(sp1.price-sp1.discountprod)-sp1.discountsales) amount
    from salesprod sp1
    left join salesaction sa1 on sa1.actionid=sp1.actionid
    where sa1.trandate=:1 and sa1.cashierno=:2
    group by sa1.trandate,sp1.salesno,sp1.flag
) sp
full join
(
    select sa1.trandate,st1.salesno,st1.flag
    ,sum(st1.amount) totalpaid
    ,sum(decode(pt.paymentflag,0,st1.amount,0)) cash
    ,sum(decode(pt.paymentflag,1,st1.amount,0)) card
    ,sum(decode(pt.paymentflag,0,0,1,0,st1.amount)) other
    from salestxn st1
    left join salesaction sa1 on sa1.actionid=st1.actionid
    left join papaytype pt on pt.typeid=st1.paymenttype
    where sa1.trandate=:1 and sa1.cashierno=:2
    group by sa1.trandate,st1.salesno,st1.flag
) st on /*st.trandate=sp.trandate and*/ st.salesno=sp.salesno
left join sales s on s.salesno=nvl(sp.salesno,st.salesno)
left join customer c on s.custno=c.customerno
order by 1,2
";

                #endregion
                #region Execute query
                object[] param = new object[] { SystemProp.TxnDate, ri.UserNo };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605040", param);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Борлуулалтын товч мэдээ. Trandate, Userno
        public Result Txn605041(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string posno = Static.ToStr(ri.ReceivedParam[1]);
                string areacode = Static.ToStr(ri.ReceivedParam[2]);

                #endregion
                #region Prepare query
                string sql = @"
select sa.trandate,sa.postdate,sa.shiftno,sa.cashierno,sa.posno,sa.areacode
,sp.salesno,sp.custno
,decode(c.classcode,1,c.corporatename,c.lastname||', '||c.firstname) custname
,sp.prodno,sp.prodtype
,decode(sp.prodtype,0,im.name,1,sm.name,pm.name) prodname
,sp.price,sp.baseprice,sp.qty,sp.salesamount
,sp.discountprod,sp.discountsales,sp.salestype
,decode(s.flag,null,sp.flag,'N',sp.flag,s.flag) flag
,sp.subtype,s.corrsalesno
from salesprod sp
inner join salesaction sa on sa.actionid=sp.actionid
left join sales s on s.salesno=sp.salesno
left join customer c on c.customerno=sp.custno
left join invmain im on im.invid=sp.prodno and sp.prodtype=0
left join servmain sm on sm.servid=sp.prodno and sp.prodtype=1
left join packmain pm on pm.packid=sp.prodno and sp.prodtype=2
where sa.trandate=:1 and sa.cashierno=:2
order by salesno
";

                #endregion
                #region Execute query
                object[] param = new object[] { trandate, ri.UserNo };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605041", param);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Борлуулалтын товч мэдээ. Trandate, Userno
        public Result Txn605042(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            try
            {
                #region Prepare parameters

                DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                string posno = Static.ToStr(ri.ReceivedParam[1]);
                string areacode = Static.ToStr(ri.ReceivedParam[2]);

                #endregion
                #region Prepare query
                string sql = @"
select sa.trandate,sa.postdate,sa.shiftno,sa.cashierno,sa.posno,sa.areacode
,sp.salesno,s.custno,decode(c.classcode,1,c.corporatename,c.lastname||', '||c.firstname) custname
,sp.paymentno,pt.name paymentname,sp.sourceno,sp.amount
,decode(s.flag,null,sp.flag,'N',sp.flag,s.flag) flag
from salestxn sp
inner join salesaction sa on sa.actionid=sp.actionid
left join sales s on s.salesno=sp.salesno
left join customer c on c.customerno=s.custno
left join papaytype pt on pt.typeid=sp.paymenttype
where sa.trandate=:1 and sa.cashierno=:2
order by salesno
";

                #endregion
                #region Execute query
                object[] param = new object[] { trandate, ri.UserNo };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605042", param);
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            return res;
        }    //Борлуулалтын товч мэдээ. Trandate, Userno

        public Result Txn605050(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = null;
            DataSet ds = new DataSet();
            DataTable dt = null;
            try
            {
                #region Prepare parameters

                //DateTime trandate = Static.ToDate(ri.ReceivedParam[0]);
                //string posno = Static.ToStr(ri.ReceivedParam[1]);
                //string areacode = Static.ToStr(ri.ReceivedParam[2]);
                string salesno = Static.ToStr(ri.ReceivedParam[0]);

                #endregion

                #region Prepare query - Tickets list
                string sql = @"
select a.salesno,s.custno,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,a.prodno,sm.name prodname,a.qty,servicetime
from (
select s.salesno,s.custno
,decode(s.prodtype,2,p.prodid,s.prodno) prodno
,decode(s.prodtype,2,p.prodtype,s.prodtype) prodtype
,decode(s.prodtype,2,p.count*s.qty,s.qty) qty
from salesprod s
left join packitem p on p.packid=s.prodno and s.prodtype=2
where s.salesno=:1 and s.prodtype in (1,2)
) a
left join servmain sm on sm.servid=a.prodno
left join sales s on s.salesno=a.salesno
left join customer c on c.customerno=s.custno
where a.prodtype=1 and printertype=1";

                #endregion
                #region Execute query - Tickets list
                object[] param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605050", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "Tickets";
                ds.Tables.Add(dt.Copy());

                #endregion

                #region Prepare query - Sales total amounts
                sql = @"
select s.salesno,s.vat,sp.sa,sp.dp,sp.ds,st.paid
,sp.trandate,sp.postdate,sp.shiftno,sp.posno,sp.cashierno,pos.posname
,decode(u.userfname,null,null,substr(u.userfname,1,1)||'.') ||u.userlname username
from sales s
left join (
  select salesno,sum(salesamount) sa,sum(qty*(baseprice-price)+discountprod) dp,sum(discountsales) ds
  ,min(sa.trandate) trandate,min(sa.postdate) postdate,min(sa.shiftno) shiftno,min(cashierno) cashierno,min(posno) posno
  from salesprod sp
  left join salesaction sa on sa.actionid=sp.actionid
  group by salesno  
) sp on sp.salesno=s.salesno
left join (
  select salesno,sum(amount) paid
  from salestxn group by salesno
) st on st.salesno=s.salesno
left join hpuser u on u.userno=sp.cashierno
left join posterminal pos on pos.posno=sp.posno
where s.salesno=:1";

                #endregion
                #region Execute query - Sales total amounts
                param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605050", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "Totals";
                ds.Tables.Add(dt.Copy());

                #endregion

                #region Prepare query - Sales product list
                sql = @"
select s.salesno,s.custno,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,s.prodno,s.prodtype,decode(s.prodtype,2,pm.name,1,sm.name,im.name) prodname,s.qty,s.price,s.salesamount
from salesprod s
left join invmain im on im.invid=s.prodno and s.prodtype=0
left join servmain sm on sm.servid=s.prodno and s.prodtype=1
left join packmain pm on pm.packid=s.prodno and s.prodtype=2
left join customer c on c.customerno=s.custno
where s.salesno=:1";

                #endregion
                #region Execute query - Sales product list
                param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605050", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "Products";
                ds.Tables.Add(dt.Copy());

                #endregion

                #region Prepare query - Payment list
                sql = @"
select st.paymentno,st.paymenttype
,pt.name paymentname,pt.paymentflag
,st.amount,st.sourceno,st.detail,st.flag
from salestxn st
left join papaytype pt on pt.typeid=st.paymenttype
where st.salesno=:1";

                #endregion
                #region Execute query - Payment list
                param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605050", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "Payments";
                ds.Tables.Add(dt.Copy());

                #endregion

                #region Prepare query - Tag list
                sql = @"
select s.custno
,decode(c.classcode,1,c.corporatename, c.lastname||' '||c.firstname) custname
,s.qty,cid.serialno,c.registerno,cid.tagframeno
from (select custno,count(custno) qty from salesrent where salesno=:1 group by custno) s
left join customer c on c.customerno=s.custno
left join customeriddevice cid on cid.custno=s.custno
order by 2";

                #endregion
                #region Execute query - Tag list
                param = new object[] { salesno };
                res = db.ExecuteQuery("core", sql, enumCommandType.SELECT, "Txn605050", param);
                if (res != null && res.ResultNo != 0) goto OnExit;

                dt = res.Data.Tables[0];
                dt.TableName = "Tags";
                ds.Tables.Add(dt.Copy());

                #endregion

                res.Data = ds;
            }
            catch (Exception ex)
            {
                res = new Result(9110002, "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message);
            }
            OnExit:
            return res;
        }    //Тасалбараар хэвлэх үйлчилгээний жагсаалт


        #endregion
    }
}
