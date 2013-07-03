using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EServ.Data;
using EServ.Interface;
using EServ.Shared;
using System.IO;
using System.Collections;

namespace IPos.TerminalUpdater
{
    public class TerminalUpdater:IModule
    {
        int index = 0;
        string dllname = "";
        Hashtable senddll = new Hashtable();
        ArrayList arraylist = new ArrayList();
        public override Result Invoke(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Result res = new Result();
            try
            {
                switch (ri.FunctionNo)
                {
                    case 100003:
                        res = Txn100003(ci, ri, db, ref lg);
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
        }
        public Result Txn100003(ClientInfo ci, RequestInfo ri, DbConnections db, ref Log lg)
        {
            Hashtable thash = new Hashtable();
            Hashtable dochash = new Hashtable();
            Hashtable rephash = new Hashtable();
            Hashtable slipshash = new Hashtable();
            thash = (Hashtable)ri.ReceivedParam[0];
            dochash = (Hashtable)ri.ReceivedParam[1];
            rephash = (Hashtable)ri.ReceivedParam[2];
            slipshash = (Hashtable)ri.ReceivedParam[3];
            Result res = new Result();
            try
            {
                string[] types = IPos.Core.SystemProp.UpdateExtention.Split(',');
                #region[Terminal self update]
                if (File.Exists(IPos.Core.SystemProp.PathTerminal + @"\InfoPos.TerminalUpdater.exe"))
                {
                    FileInfo fileinfo = new FileInfo(IPos.Core.SystemProp.PathTerminal + @"\InfoPos.TerminalUpdater.exe");
                    DateTime date = (DateTime)thash["InfoPos.TerminalUpdater.exe"];
                    if (!Modified(thash,fileinfo))
                    {
                        res = FileToByteArray(IPos.Core.SystemProp.PathTerminal + @"\InfoPos.TerminalUpdater.exe", fileinfo.Name, fileinfo.LastWriteTime);
                        res.Param = new object[] { senddll };
                        res.ResultNo = 9110135;
                        res.ResultDesc = "Терминал шинэчлэгч өөрийгөө шинэчлэж байна.";
                        return res;
                    }
                }
                #endregion
                #region[Терминал бусад файл]
                if (Directory.Exists(IPos.Core.SystemProp.PathTerminal))
                {
                foreach (string type in types)
                {
                    string[] sfilename = Directory.GetFiles(IPos.Core.SystemProp.PathTerminal, type);
                    foreach (string filename in sfilename)
                    {
                        FileInfo fileinfo = new FileInfo(filename);
                        if (fileinfo.Name != "InfoPos.TerminalUpdater.exe" && fileinfo.Name != "InfoPos.UpdaterCopy.exe")
                        {
                            if (thash.ContainsKey(fileinfo.Name))
                            {                  
                                if (!Modified(thash, fileinfo))
                                {
                                    res = FileToByteArray(filename, fileinfo.Name, fileinfo.LastWriteTime);
                                    if (res.ResultNo != 0)
                                    {
                                        return res;
                                    }
                                }
                            }
                            else
                            {
                                res = FileToByteArray(filename, fileinfo.Name, fileinfo.LastWriteTime);
                                if (res.ResultNo != 0)
                                {
                                    return res;
                                }
                            }
                        }
                    }
                }
                }
                else
                {
                    res.ResultNo = 9110130;
                    res.ResultDesc = "Сервэр дээрх терминал байрлаж буй хавтасны тохиргоо буруу байна.";
                    return res;
                }
                #endregion
                #region[Doc загвар файл]
                if(Directory.Exists(IPos.Core.SystemProp.PathDynamicDoc))
                {  
                    foreach (string type in types)
                    {
                        string[] sfilename = Directory.GetFiles(IPos.Core.SystemProp.PathDynamicDoc, type);
                        foreach (string filename in sfilename)
                        {
                            FileInfo fileinfo = new FileInfo(filename);
                            if (fileinfo.Name != "InfoPos.TerminalUpdater.exe" && fileinfo.Name != "InfoPos.UpdaterCopy.exe")
                            {
                                if (dochash.ContainsKey(fileinfo.Name))
                                {
                                    if (!Modified(dochash, fileinfo))
                                    {
                                        res = FileToByteArray(filename, fileinfo.Name, fileinfo.LastWriteTime);
                                        if (res.ResultNo != 0)
                                        {
                                            return res;
                                        }
                                    }
                                }
                                else
                                {
                                    res = FileToByteArray(filename, fileinfo.Name, fileinfo.LastWriteTime);
                                    if (res.ResultNo != 0)
                                    {
                                        return res;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    res.ResultNo = 9110131;
                    res.ResultDesc = "Сервэр дээрх документ загвар байрлаж буй хавтасны тохиргоо буруу байна.";
                    return res;
                }
                #endregion
                #region[Report файл]
                if (Directory.Exists(IPos.Core.SystemProp.PathDynamicRpt))
                {
                    foreach (string type in types)
                    {
                        string[] sfilename = Directory.GetFiles(IPos.Core.SystemProp.PathDynamicRpt, type);
                        foreach (string filename in sfilename)
                        {
                            FileInfo fileinfo = new FileInfo(filename);
                            if (fileinfo.Name != "InfoPos.TerminalUpdater.exe" && fileinfo.Name != "InfoPos.UpdaterCopy.exe")
                            {
                                if (rephash.ContainsKey(fileinfo.Name))
                                {
                                    if (!Modified(rephash, fileinfo))
                                    {
                                        res = FileToByteArray(filename, fileinfo.Name, fileinfo.LastWriteTime);
                                        if (res.ResultNo != 0)
                                        {
                                            return res;
                                        }
                                    }
                                }
                                else
                                {
                                    res = FileToByteArray(filename, fileinfo.Name, fileinfo.LastWriteTime);
                                    if (res.ResultNo != 0)
                                    {
                                        return res;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    res.ResultNo = 9110132;
                    res.ResultDesc = "Сервэр дээрх динамик тайлан байрлаж буй хавтасны тохиргоо буруу байна.";
                    return res;
                }
                #endregion
                #region[Slips файл]
                if (Directory.Exists(IPos.Core.SystemProp.PathSlips))
                {
                    foreach (string type in types)
                    {
                        string[] sfilename = Directory.GetFiles(IPos.Core.SystemProp.PathSlips, type);
                        foreach (string filename in sfilename)
                        {
                            FileInfo fileinfo = new FileInfo(filename);
                            if (fileinfo.Name != "InfoPos.TerminalUpdater.exe" && fileinfo.Name != "InfoPos.UpdaterCopy.exe")
                            {
                                if (slipshash.ContainsKey(fileinfo.Name))
                                {
                                    if (!Modified(slipshash, fileinfo))
                                    {
                                        res = FileToByteArray(filename, fileinfo.Name, fileinfo.LastWriteTime);
                                        if (res.ResultNo != 0)
                                        {
                                            return res;
                                        }
                                    }
                                }
                                else
                                {
                                    res = FileToByteArray(filename, fileinfo.Name, fileinfo.LastWriteTime);
                                    if (res.ResultNo != 0)
                                    {
                                        return res;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    res.ResultNo = 9110133;
                    res.ResultDesc="Сервэр дээрх slips тайлан байрлаж буй хавтасны тохиргоо буруу байна.";
                    return res;
                }
                #endregion
                res.Param = new object[] { senddll };
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 911;
                res.ResultDesc = ex.Message;
                return res;
            }
        }
        #region[Function]
        //File аа буферлэж лист уруу хийж буй хэсэг
        public Result FileToByteArray(string _FileName,string name,DateTime modifieddate)
        {
            byte[] _Buffer = null;
            Result res = new Result();
            try
            {
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                FileInfo fileinfo=new FileInfo(_FileName);
                ArrayList array=new ArrayList();
                array.Add(name);
                array.Add(_Buffer);
                dllname = dllname + fileinfo.LastWriteTime.Year + "." + fileinfo.LastWriteTime.Month + "." + fileinfo.LastWriteTime.Day + " " + fileinfo.LastWriteTime.Hour + ":" + fileinfo.LastWriteTime.Minute + ":" + fileinfo.LastWriteTime.Second;
                array.Add(dllname);
                dllname = "";
                if (fileinfo.DirectoryName == IPos.Core.SystemProp.PathTerminal) { array.Add(0); }
                if (fileinfo.DirectoryName == IPos.Core.SystemProp.PathDynamicRpt) { array.Add(1); }
                if (fileinfo.DirectoryName == IPos.Core.SystemProp.PathDynamicDoc) { array.Add(2); }
                if (fileinfo.DirectoryName == IPos.Core.SystemProp.PathSlips) { array.Add(3); }
                senddll.Add(index, array);
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
                index++;
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 9110134;
                res.ResultDesc = "Сервэрээс файл татахад алдаа гарлаа.  " + ex.Message;
                return res;
            }
            return res;
        }
        //Файлын хугацааг шалгаж буй хэсэг
        public bool Modified(Hashtable thash,FileInfo fileinfo)
        {
            DateTime datetime = (DateTime)thash[fileinfo.Name];
            if (datetime.Year >= fileinfo.LastWriteTime.Year)
            {
                if (datetime.Year == fileinfo.LastWriteTime.Year)
                {
                    if (datetime.Month >= fileinfo.LastWriteTime.Month)
                    {
                        if (datetime.Month == fileinfo.LastWriteTime.Month)
                        {
                            if (datetime.Day >= fileinfo.LastWriteTime.Day)
                            {
                                if (datetime.Day == fileinfo.LastWriteTime.Day)
                                {
                                    if (datetime.Hour >= fileinfo.LastWriteTime.Hour)
                                    {
                                        if (datetime.Hour == fileinfo.LastWriteTime.Hour)
                                        {
                                            if (datetime.Minute >= fileinfo.LastWriteTime.Minute)
                                            {
                                                if (datetime.Minute >= fileinfo.LastWriteTime.Minute)
                                                {
                                                    if (datetime.Second < fileinfo.LastWriteTime.Second)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    { return true; }
                                                }
                                                else
                                                { return false; }
                                            }
                                            else
                                            { return false; }
                                        }
                                        else { return true; }
                                    }
                                    else
                                    { return false; }
                                }
                                else
                                { return true; }
                            }
                            else { return false; }
                        }
                        else { return true; }
                    }
                    else { return false; }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
