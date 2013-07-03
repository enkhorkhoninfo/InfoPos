using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using EServ.Data;
using EServ.Shared;
using EServ.Interface;

using System.Net.Mail;
using ISM.DB;
using IPos.Core;
namespace Ipos.admin
{
    public class Admin : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime First = DateTime.Now;
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 110100: 	            //Хэрэглэгчийн жагсаалт авах
                        res = Txn110100(ci, ri, db, ref lg);
                        break;
                    case 110101:            	//Хэрэглэгчийн мэдээлэл авах
                        res = Txn110101(ci, ri, db, ref lg);
                        break;
                    case 110102:            	//Хэрэглэгч нэмэх
                        res = Txn110102(ci, ri, db, ref lg);
                        break;
                    case 110103:	            //Хэрэглэгч устгах
                        res = Txn110103(ci, ri, db, ref lg);
                        break;
                    case 110104:	            //Хэрэглэгч засварлах
                        res = Txn110104(ci, ri, db, ref lg);
                        break;
                    case 110105: 	            //Хэрэглэгчийн бүлгийн жагсаалт авах
                        res = Txn110105(ci, ri, db, ref lg);
                        break;
                    case 110106:            	//Хэрэглэгчийн бүлгийн мэдээлэл авах
                        res = Txn110106(ci, ri, db, ref lg);
                        break;
                    case 110107:            	//Хэрэглэгчийн бүлэг нэмэх
                        res = Txn110107(ci, ri, db, ref lg);
                        break;
                    case 110108:	            //Хэрэглэгчийн бүлэг устгах
                        res = Txn110108(ci, ri, db, ref lg);
                        break;
                    case 110109:	            //Хэрэглэгчийн бүлэг засварлах
                        res = Txn110109(ci, ri, db, ref lg);
                        break;
                    case 110110:	            //Хэрэглэгчийн бүлгийн гүйлгээний жагсаалт
                        res = Txn110110(ci, ri, db, ref lg);
                        break;
                    case 110111:	            //Хэрэглэгчийн зураг засварлах
                        res = Txn110111(ci, ri, db, ref lg);
                        break;
                    case 110112:	            //Хэрэглэгчийн зураг авах
                        res = Txn110112(ci, ri, db, ref lg);
                        break;
                    case 110113:                //Нууц үгийн бүртгэлийн мэдээлэл авах
                        res = Txn110113(ci, ri, db, ref lg);
                        break;
                    case 110114:                //Нууц үгийн бүртгэлийг засварлах
                        res = Txn110114(ci, ri, db, ref lg);
                        break;
                    case 110201:                //Логийн дэлгэрэнгүй мэдээлэл авах
                        res = Txn110201(ci, ri, db, ref lg);
                        break;
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                DateTime Last = DateTime.Now;
                if (res.ResultNo == 0)
                ISM.Lib.Static.WriteToLogFile("IPos.Admin", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("IPos.Admin", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
            }
        }
        #region [ Business functions ]
        #region [ Үндсэн хэрэглэгч ]
        public Result Txn110100(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            try
            {
                res = DBIO.DB203001(db, pagenumber, pagecount, null);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн жагсаалт авах";

            }
        }
        public Result Txn110101(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();
            int userno = 0;
            userno = Static.ToInt(ri.ReceivedParam[0]);
            try
            {
                ret.ResultNo = 9110002;

                res = DBIO.DB203002(db, userno);     // Хэрэглэгчийн дэлгэрэнгүй мэдээлэл
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "Users";
                DataSet ds = new DataSet();
                ds.Tables.Add(res.Data.Tables[0].Copy());

                res = DBIO.DB203003(db, userno);     // Хэрэглэгчийн сонгогдсон болон сонгогдоогүй эрхүүд
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "UserPriv";
                ds.Tables.Add(res.Data.Tables[0].Copy());

                res = DBIO.DB203004(db, userno);     // Хэрэглэгчийн supervisor хэрэглэгч нар
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "UserSup";
                ds.Tables.Add(res.Data.Tables[0].Copy());

                ret.Data = ds;
                ret.ResultNo = 0;
                return ret;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн мэдээлэл авах";
                object[] newvalue = new object[2];
                newvalue[0] = userno;
                newvalue[1] = res.ResultNo;
                lg.AddDetail("HPUSER", newvalue[0].ToString(), "Хэрэглэгчийн мэдээлэл авах", newvalue[1].ToString());
            }
        }
        public Result Txn110102(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            object[] value = (object[])ri.ReceivedParam[0];
            object[] obj = new object[19];
            string DbIdErrorDesc = "Системд ийм дугаартай хэрэглэгч бүртгэгдсэн байна.";

            try
            {
                obj[0] = value[0]; // UserNo
                obj[1] = value[1]; //UserFname
                obj[2] = value[2]; //UserLname
                obj[3] = value[3]; //UserFname2
                obj[4] = value[4]; //UserLname2
                obj[5] = value[5]; //RegisterNo
                obj[6] = value[6]; //Position
                obj[7] = value[7]; //Status
                obj[8] = value[8]; //BranchNo
                obj[9] = value[9]; //UserLevel
                obj[10] = value[10]; //UPassword
                obj[11] = value[11]; //UserType
                obj[12] = value[12]; //Email
                obj[13] = value[13]; //Mobile
                obj[14] = value[14]; //Logintype
                obj[15] = value[15]; //wrongcount
                obj[16] = value[16]; //Pass Change date
                obj[17] = value[17]; //Agentcorp
                obj[18] = value[18]; //AgentBranch
                res = DBIO.DB203006(db, obj);
                Result PassPolicy = new Result();
                PassPolicy = DBIO.DB216001(db);
                DataRow _dr;
                _dr = PassPolicy.Data.Tables[0].Rows[0];
                if (Static.ToStr(_dr["ISSENDMAIL"]) == Static.ToStr(1))
                {
                    if (PassPolicy.ResultNo == 0)
                    {

                        _dr = PassPolicy.Data.Tables[0].Rows[0];
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(Static.ToStr(_dr["FROMUSER"]));
                        msg.To.Add(new MailAddress(Static.ToStr(obj[12]).ToLower()));
                        msg.Subject = obj[2] + " Таны нэвтрэх нэр, нууц үг";
                        object[] upassword = (object[])ri.ReceivedParam[2];
                        for (int i = 0; i < upassword.Length; i++)
                        {
                            msg.Body = "Таны нэвтрэх нэр, нууц үг\r\n\r\n\tХэрэглэгчийн дугаар: " + obj[0] + "\r\n\tНууц үг: " + upassword[i];
                        }
                        SmtpClient smtp = new SmtpClient(Static.ToStr(_dr["SERVERNAME"]));
                        smtp.Credentials = new System.Net.NetworkCredential((Static.ToStr(_dr["MAILUSERNAME"])), (Static.ToStr(_dr["MAILUSERPASS"])));
                        smtp.Port = Static.ToInt(_dr["SERVERPORT"]);
                        smtp.Send(msg);
                    }
                }
                if (res.ResultNo == 9110039)
                {
                    res.ResultNo = 9110040;
                    res.ResultDesc = DbIdErrorDesc;
                    return res;
                }

                if (value[19] != null)
                {
                    DataTable priv = (DataTable)value[19];
                    res = DBIO.DB203009(db, Static.ToInt(value[0]), priv);
                }
                if (value[20] != null)
                {
                    DataTable sup = (DataTable)value[20];
                    res = DBIO.DB203010(db, Static.ToInt(value[0]), sup);
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгч нэмэх";
                object[] fieldValue = (object[])ri.ReceivedParam[1];
                object[] newValue = (object[])ri.ReceivedParam[0];
                for (int i = 0; i < fieldValue.Length; i++)
                {
                    lg.AddDetail("HPUSER", Static.ToStr(fieldValue[i]), "Хэрэглэгч нэмэх", Static.ToStr(newValue[i]));
                }
            }
        }
        public Result Txn110103(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();            
            try
            {
                res = DBIO.DB203008(db, Static.ToInt(ri.ReceivedParam[0]));
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгч устгах";
            }
        }
        public Result Txn110104(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg )
        {
            Result res = new Result();
                        
            object[] value = (object[])ri.ReceivedParam[0];
            object[] obj = new object[19];
            int userno = Static.ToInt(ri.ReceivedParam[5]);
            long olpassword = Static.ToLong(ri.ReceivedParam[3]);
            long password = Static.ToLong(ri.ReceivedParam[4]);


                Result PassPolicy = new Result();
                PassPolicy = DBIO.DB216001(db);
                DataRow _dr;
                _dr = PassPolicy.Data.Tables[0].Rows[0];
                
                Result pres = new Result();
                pres = DBIO.DB203002(db, userno);
                DataRow _drow;
                _drow = pres.Data.Tables[0].Rows[0];
                decimal pass = Static.ToDecimal(_drow["UPASSWORD"]);


                if (Static.ToLong(olpassword) != Static.ToLong(password))
                {

                    if (Static.ToStr(_dr["ISSENDMAIL"]) == Static.ToStr(1))
                    {
                        if (PassPolicy.ResultNo == 0)
                        {

                            _dr = PassPolicy.Data.Tables[0].Rows[0];
                            MailMessage msg = new MailMessage();
                            msg.From = new MailAddress(Static.ToStr(_dr["FROMUSER"]));
                            msg.To.Add(new MailAddress(Static.ToStr(obj[12]).ToLower()));
                            msg.Subject = obj[2] + " Таны нэвтрэх нэр, нууц үг";
                            object[] uspassword = (object[])ri.ReceivedParam[4];
                            for (int i = 0; i < uspassword.Length; i++)
                            {
                                msg.Body = "Таны нэвтрэх нэр, нууц үг\r\n\r\n\tХэрэглэгчийн дугаар: " + obj[0] + "\r\n\tНууц үг: " + uspassword[i];
                            }
                            SmtpClient smtp = new SmtpClient(Static.ToStr(_dr["SERVERNAME"]));
                            smtp.Credentials = new System.Net.NetworkCredential((Static.ToStr(_dr["MAILUSERNAME"])), (Static.ToStr(_dr["MAILUSERPASS"])));
                            smtp.Port = Static.ToInt(_dr["SERVERPORT"]);
                            smtp.Send(msg);
                        }
                    }

                }              

            try
            {
                obj[0] = value[0]; // UserNo
                obj[1] = value[1]; //UserFname
                obj[2] = value[2]; //UserLname
                obj[3] = value[3]; //UserFname2
                obj[4] = value[4]; //UserLname2
                obj[5] = value[5]; //RegisterNo
                obj[6] = value[6]; //Position
                obj[7] = value[7]; //Status
                obj[8] = value[8]; //BranchNo
                obj[9] = value[9]; //UserLevel
                obj[10] = value[10]; //UPassword
                obj[11] = value[11]; //UserType
                obj[12] = value[12]; //Email
                obj[13] = value[13]; //Mobile
                obj[14] = value[14]; //Logintype
                obj[15] = value[15]; // WrongCount
                obj[16] = value[16]; //PassChangeDate
                obj[17] = value[17]; // AgentCorp
                obj[18] = value[18]; //AgentBranch

                if (value[19] != null)
                {
                    DataTable priv = (DataTable)value[19];
                    res = DBIO.DB203009(db, Static.ToInt(value[0]), priv);
                    if (res.ResultNo != 0)
                    {
                        return res;
                    }
                }
                if (value[20] != null)
                {
                    DataTable sup = (DataTable)value[20];
                    res = DBIO.DB203010(db, Static.ToInt(value[0]), sup);
                    if (res.ResultNo != 0)
                    {
                        return res;
                    }
                }
                res = DBIO.DB203007(db, obj);
                EServ.Shared.Static.WriteToLogFile("Error.log", "DB203007" + res.ResultNo.ToString() + res.ResultDesc);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн мэдээлэл засварлах";
                object[] fieldValue = (object[])ri.ReceivedParam[1];
                object[] newValue = (object[])ri.ReceivedParam[0];
                object[] oldValue = (object[])ri.ReceivedParam[2];
                for (int i = 0; i < fieldValue.Length; i++)
                {
                    if (oldValue[i] != newValue[i])
                    {
                        lg.AddDetail("HPUSER", Static.ToStr(fieldValue[i]), Static.ToStr(oldValue[i]), Static.ToStr(newValue[i]));
                    }
                }
            }
        }
        public Result Txn110111(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();

            try
            {

                byte[] image = (byte[])ri.ReceivedParam[1];
                res = DBIO.DB203011(db, Static.ToInt(ri.ReceivedParam[0]), image);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн мэдээлэл засварлах";
            }
        }
        public Result Txn110112(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                int userno = Static.ToInt(ri.ReceivedParam[0]);
                res = DBIO.DB203005(db, userno);     // Хэрэглэгчийн зураг

                EServ.Shared.Static.WriteToLogFile("Error.log", "Txn110112 userno=" + userno.ToString() + " " + res.ResultNo.ToString());

                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "UserPhoto";
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн зурагын мэдээлэл засварлах";
            }
        }
        public Result Txn110113(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB216001(db);     // Нууц үгийн бүртгэлийн мэдээлэл авах                
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                lg.item.Desc = "Нууц үгийн бүртгэлийн мэдээлэл авах";
            }
        }
        public Result Txn110114(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB216002(db, (object[])ri.ReceivedParam[0]);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110001;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", " " + res.ResultNo.ToString());
                return res;
            }
            finally
            {
                lg.item.Desc = "Нууц үгийн бүртгэлийг засварлах";
                object[] fieldValue = (object[])ri.ReceivedParam[2];
                object[] oldValue = (object[])ri.ReceivedParam[1];
                object[] newValue = (object[])ri.ReceivedParam[0];

                for (int i = 0; i < fieldValue.Length; i++)
                {
                    if (oldValue[i].ToString() != newValue[i].ToString())
                    {
                        lg.AddDetail("PASSPOLICY", fieldValue[i].ToString(), oldValue[i].ToString(), newValue[i].ToString());
                    }
                }
            }
        }
        public Result Txn110201(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();


            long pLogID = Static.ToLong(ri.ReceivedParam[0]);
            try
            {

                res = DBIO.DB203201(db, pLogID);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
                return res;
            }
            finally
            {
                //lg.item.Desc = "Даатгалын гэрээний Гэрээ хийсэн бүтээгдэхүүний лавлагаа";
            }

            //Result res = new Result();
            //int pLogID = Static.ToInt(ri.ReceivedParam[1]);
            //try
            //{
            //    res = IPos.DB.Main.DB203201(db, pLogID); // Логийн дэлгэрэнгүй мэдээлэл авах
            //    return res;
            //}
            //catch (Exception ex)
            //{
            //    res.ResultNo = 9110002;
            //    res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;
            //    EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);
            //    return res;
            //}
            //finally
            //{
            //  //  lg.item.Desc = "Логийн дэлгэрэнгүй мэдээлэл авах";
            //}
        }
        #endregion
        #region [ Хэрэглэгчийн бүлэг ]
        public Result Txn110105(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int pagenumber = 1;
            int pagecount = 1;
            try
            {
                res = DBIO.DB203012(db, pagenumber, pagecount, ri.ReceivedParam);  //Хэрэглэгчийн бүлгийн жагсаалт авах
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн бүлгийн жагсаалт авах";
            }
        }
        public Result Txn110106(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            Result ret = new Result();
            int groupid = 0;
            try
            {
                groupid = Static.ToInt(ri.ReceivedParam[0]);
                ret.ResultNo = 9110002;

                res = DBIO.DB203013(db, groupid);     // Хэрэглэгчийн бүлэгийн дэлгэрэнгүй мэдээлэл
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "Group";
                DataSet ds = new DataSet();
                ds.Tables.Add(res.Data.Tables[0].Copy());

                ret.Data = ds;
                ret.ResultNo = 0;
                return ret;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн бүлэгийн мэдээлэл авах";
            }
        }
        public Result Txn110107(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                res = DBIO.DB203015(db, (object[])ri.ReceivedParam[0]);           // Хэрэглэгчийн бүлэг нэмэх
                if (res.ResultNo != 0)
                    return res;

                DataTable txn = (DataTable)ri.ReceivedParam[2];
                res = DBIO.DB203018(db, Static.ToInt(ri.ReceivedParam[0]), txn);
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн бүлэг нэмэх";
                object[] fieldValue = (object[])ri.ReceivedParam[1];
                object[] newValue = (object[])ri.ReceivedParam[0];

                for (int i = 0; i < fieldValue.Length; i++)
                {
                    lg.AddDetail("TXNGROUP", fieldValue[i].ToString(), "Хэрэглэгчийн бүлэг нэмэх", newValue[i].ToString());
                }
            }
        }
        public Result Txn110108(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int groupid = Static.ToInt(ri.ReceivedParam[0]);
            try
            {

                res = DBIO.DB203017(db, groupid);   // Хэрэглэгчийн бүлэг устгах
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн бүлэг устгах";
                //object[] fielvalue = (object[])ri.ReceivedParam[1];
                object[] oldvalue = (object[])ri.ReceivedParam[1];
                for (int i = 0; i < oldvalue.Length; i++)
                {
                    if (groupid != 0)
                    {
                        lg.AddDetail("TXNGROUP", oldvalue[i].ToString(), "Хэрэглэгчийн бүлэг устгах", groupid.ToString());
                    }
                }
            }
        }
        public Result Txn110109(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {

                res = DBIO.DB203016(db, (object[])ri.ReceivedParam[0]);  // Хэрэглэгчийн бүлэг засварлах
                if (res.ResultNo != 0)
                    return res;

                object[] idgroup = (object[])ri.ReceivedParam[0];
                res = DBIO.DB203018(db, Static.ToInt(idgroup[0]), (DataTable)ri.ReceivedParam[3]);

                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн бүлэгийн мэдээлэл засварлах";
                object[] fieldValue = (object[])ri.ReceivedParam[1];
                object[] oldValue = (object[])ri.ReceivedParam[2];
                object[] newValue = (object[])ri.ReceivedParam[0];

                for (int i = 0; i < fieldValue.Length; i++)
                {
                    if (oldValue[i] != null)
                    {
                        lg.AddDetail("TXNGROUP", fieldValue[i].ToString(), oldValue[i].ToString(), newValue[i].ToString());
                    }
                }
            }
        }
        public Result Txn110110(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            int groupid = 0;

            try
            {
                groupid = Static.ToInt(ri.ReceivedParam[0]); // GroupID
                res = DBIO.DB203014(db, groupid);     // Хэрэглэгчийн бүлэгийн сонгогдсон болон сонгогдоогүй гүйлгээнүүд
                if (res.ResultNo != 0)
                    return res;
                res.Data.Tables[0].TableName = "GroupTxn";
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110002;
                res.ResultDesc = "Програм руу нэвтрэхэд алдаа гарлаа" + ex.Message;

                EServ.Shared.Static.WriteToLogFile("Error.log", ex.Message + ex.Source + ex.StackTrace);

                return res;
            }
            finally
            {
                lg.item.Desc = "Хэрэглэгчийн бүлэгийн гүйлгээний жагсаалт";
            }
        }
        #endregion
        #endregion
    }
}
