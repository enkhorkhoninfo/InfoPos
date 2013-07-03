using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using EServ.Data;
using EServ.Shared;
using EServ.Interface;
using System.IO;

namespace IPos.Reports
{
    public class Reports : IModule
    {
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            DateTime First = DateTime.Now;
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 240000: //Динамик тайлангийн гүйлгээний жагсаалт
                        res = Txn240000(ci, ri, db, ref lg);
                        break;
                    case 249998:
                        res = Txn249998(ci, ri, db, ref lg);
                        break;
                    case 249999: // борлуулалт хайх
                        res = Txn249999(ci, ri, db, ref lg);
                        break;
                    default:
                        res = new Result(1000, "Unknown transation.");
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
                    ISM.Lib.Static.WriteToLogFile("IPos.Reports", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : 0 \r\n ResultDescription : OK \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
                else
                    ISM.Lib.Static.WriteToLogFile("IPos.Reports", "<<Start : " + Static.ToStr(First) + ">>\r\n UserNo : " + Static.ToStr(ri.UserNo) + "\r\n FunctionNo : " + ri.FunctionNo.ToString() + "\r\n Description : " + Static.ToStr(lg.item.Desc) + "\r\n ResultNo : " + res.ResultNo.ToString() + " \r\n ResultDescription : " + res.ResultDesc + " \r\n <<End : " + Static.ToStr(Last) + ">>\r\n");
            }
        }
        public Result Txn240000(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)       //Динамик тайлангийн гүйлгээний жагсаалт 
        {
            Result res = new Result();
            int pUserNo = Static.ToInt(ri.ReceivedParam[0]);
            DateTime pTxnDate = Static.ToDate(ri.ReceivedParam[1]);

           

            object[] nobj = new object[2];

            //  object[] nobj1=ri.ReceivedParam[];

            nobj = (object[])ri.ReceivedParam[2];
           // object[] pParam=ri.ReceivedParam;
            try
            {


                //object pParam = ri.ReceivedParam[2];
                //object pParam1 = ri.ReceivedParam[3];


               
                res = IPos.DB.Main.DB214000(db, ri.PageIndex, ri.PageRows, pUserNo, pTxnDate,nobj);
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
              lg.item.Desc = "Динамик тайлангийн жагсаалт авах";

              if (res.ResultNo == 0)
              {
                  lg.item.Desc = "Динамик тайлангийн жагсаалт авах";
                  object[] FieldName = new object[2];
                  FieldName[0] = "TRANCODE";
                  FieldName[1] = "NAME";

                  for (int i = 0; i < FieldName.Length; i++)
                  {

                      if (nobj[i] !=null)
                      {
                          lg.AddDetail("TXN", FieldName[i].ToString(), "Динамик тайлангийн жагсаалт авах", Static.ToStr(nobj[i]));
                        

                      }
                  }
              }
            }
        }
        public Result Txn249999(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                object[] param = new object[1];
                string ViewName = "";
                Hashtable colindex = new Hashtable();
                string query = "";
                if (Static.ToInt(ri.ReceivedParam[1]) == 0)
                {
                    Excel.Application xlsm = new Excel.Application();
                    Excel.Workbook xlsWorkBook = xlsm.Workbooks.Open(@Static.ToStr(ri.ReceivedParam[0]), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    Excel.Worksheet xlsWorkSheet = xlsWorkBook.Worksheets[1];
                    ViewName = xlsWorkSheet.Name;
                    for (int i = 1; i <= xlsWorkSheet.UsedRange.Columns.Count; i++)
                    {
                        if (!colindex.ContainsKey(xlsWorkSheet.Cells[1, i].Value.ToString()))
                        {
                            colindex.Add(xlsWorkSheet.Cells[1, i].Value.ToString(), i);
                        }
                        else
                        {
                            colindex.Clear();
                            res.ResultNo = 9110146;
                            res.ResultDesc = string.Format("Тайлангын загвар алдаатай байна. {0} талбар өмнө нь үүссэн байна.", xlsWorkSheet.Cells[1, i].Value);
                            return res;
                        }
                    }
                    res = IPos.DB.Main.DB202351(db, Static.ToStr(ViewName));
                    if (res.ResultNo != 0)
                    {
                        colindex.Clear();
                        return res;
                    }
                    object[] obj = new object[2];
                    obj[0] = colindex;
                    obj[1]=ViewName;
                    res.Param = obj;
                }
                else
                {
                    int rowindex = 2;
                    Excel.Application xlsm = new Excel.Application();
                    Excel.Workbook xlsWorkBook = xlsm.Workbooks.Open(@Static.ToStr(ri.ReceivedParam[0]), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    Excel.Worksheet xlsWorkSheet = xlsWorkBook.Worksheets[1];
                    ViewName = xlsWorkSheet.Name;
                    StringBuilder sb = new StringBuilder();
                    object[] prop = (object[])ri.ReceivedParam[2];
                    bool isfirst = false;
                    bool iswhere = false;
                    if (prop.Count() > 0)
                    {
                        for (int i = 0; i < prop.Count() / 3; i = i + 3)
                        {
                            if (prop[i + 2] != null && prop[i + 2] != "")
                            {
                                if (isfirst == false)
                                {
                                    if (Static.ToStr(prop[i + 1]).ToUpper() == "LIKE")
                                    {
                                        sb.Append(Static.ToStr(prop[i]));
                                        sb.Append(string.Format(" {0} ", Static.ToStr(prop[i + 1])));
                                        sb.Append(string.Format("'{0}%'", Static.ToStr(prop[i + 2])));
                                        isfirst = true;
                                    }
                                    else
                                    {
                                        sb.Append(Static.ToStr(prop[i]));
                                        sb.Append(string.Format(" {0} ", Static.ToStr(prop[i + 1])));
                                        sb.Append(Static.ToStr(prop[i + 2]));
                                        isfirst = true;
                                    }
                                }
                                else
                                {
                                    if (Static.ToStr(prop[i + 1]).ToUpper() == "LIKE")
                                    {
                                        sb.Append(" and ");
                                        sb.Append(Static.ToStr(prop[i]));
                                        sb.Append(string.Format(" {0} ", Static.ToStr(prop[i + 1])));
                                        sb.Append(string.Format("'{0}%'", Static.ToStr(prop[i + 2])));
                                        isfirst = true;
                                    }
                                    else
                                    {
                                        sb.Append(" and ");
                                        sb.Append(Static.ToStr(prop[i]));
                                        sb.Append(string.Format(" {0} ", Static.ToStr(prop[i + 1])));
                                        sb.Append(Static.ToStr(prop[i + 2]));
                                        iswhere = true;
                                    }
                                }
                            }
                        }
                        if (isfirst == true || iswhere == true)
                        {
                            query = string.Format("select * from {0} where {1}", ViewName, sb.ToString());
                        }
                        else
                        {
                            query = string.Format("select * from {0}", ViewName);
                        }
                    }
                    else
                    {
                        query = string.Format("select * from {0}", ViewName);
                    }
                    res = db.ExecuteQuery("core", query, enumCommandType.SELECT, "Txn249999", null);
                    if (res.ResultNo != 0) return res;

                    colindex = (Hashtable)ri.ReceivedParam[3];
                    foreach (DataRow row in res.Data.Tables[0].Rows)
                    {
                        foreach (DictionaryEntry fieldname in colindex)
                        {
                            xlsWorkSheet.Cells[rowindex, Convert.ToInt32(fieldname.Value)].Value = row[fieldname.Key.ToString()];
                        }
                        rowindex++;
                    }

                    string temp = Path.GetTempPath();
                    string filepath = string.Format(@temp+@"rep{0}.xlsm", DateTime.Now.Ticks);
                    xlsWorkBook.SaveCopyAs(filepath);

                    byte[] _Buffer = null;
                    System.IO.FileStream _FileStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                    long _TotalBytes = new System.IO.FileInfo(@Static.ToStr(ri.ReceivedParam[0])).Length;
                    _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                    param[0] = _Buffer;
                    _FileStream.Dispose();
                    _FileStream.Close();
                    _BinaryReader.Dispose();
                    _BinaryReader.Close();
                    colindex.Clear();
                    Result result = new Result();
                    result.Param = param;
                    return result;
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
                lg.item.Desc = "Excel тайлангийн жагсаалт авах";

            }
        }
        public Result Txn249998(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            string[] filepath = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportFullName", typeof(string));
            dt.Columns.Add("ReportName", typeof(string));
            DataSet ds = new DataSet();
            try
            {
                if (IPos.Core.SystemProp.PathDynamicRpt != "")
                {
                    filepath = Directory.GetFiles(IPos.Core.SystemProp.PathDynamicRpt, "*.xls");
                    FileInfo fileinfo = null;
                    foreach (string name in filepath)
                    {
                        fileinfo = new FileInfo(name);
                        DataRow dr = dt.NewRow();
                        dr["ReportFullName"] = name;
                        dr["ReportName"] = fileinfo.Name;
                        dt.Rows.Add(dr);
                    }
                    dt.TableName = "REPORTSPATH";
                    ds.Tables.Add(dt.Copy());
                    res.Data = ds;
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
                lg.item.Desc = "Excel тайлангийн жагсаалт авах";
            }
        }
    }
}