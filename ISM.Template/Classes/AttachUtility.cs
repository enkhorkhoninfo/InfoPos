using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using EServ.Shared;

namespace ISM.Template
{
    public class AttachUtility
    {
        static public Result Get(CUser.Remote remote, int PrivNo, ulong AttachId)
        {
            Result res = null;
            try
            {
                object[] param = new object[] { AttachId };
                res = remote.Connection.Call(
                    remote.User.UserNo
                    , 103
                    , 103001
                    , PrivNo
                    , param
                    );
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        static public Result GetImage(CUser.Remote remote, int PrivNo, ulong AttachId, ref Image image)
        {
            Result res = null;
            try
            {
                res = Get(remote, PrivNo, AttachId);
                if (res.ResultNo == 0 && res.AffectedRows > 0)
                {
                    image = Static.ImageFromByte((byte[])res.Data.Tables[0].Rows[0]["attachblob"]);
                    res.Param = new object[] { res.Data.Tables[0].Rows[0]["filename"] };
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        static public Result GetFile(CUser.Remote remote, int PrivNo, ulong AttachId, ref Stream stream)
        {
            Result res = null;
            string filename = "";
            try
            {
                res = Get(remote, PrivNo, AttachId);
                if (res.ResultNo == 0 && res.AffectedRows > 0)
                {
                    byte[] bytes = (byte[])res.Data.Tables[0].Rows[0]["attachblob"];
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                    filename = ISM.Lib.Static.ToStr(res.Data.Tables[0].Rows[0]["filename"]);
                }
                res.Param = new object[] { filename };
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        static public Result GetFile(CUser.Remote remote, int PrivNo, ulong AttachId, string outfolder)
        {
            Result res = null;
            MemoryStream ms = new MemoryStream();
            Stream stream = ms;
            res = GetFile(remote, PrivNo, AttachId, ref stream);
            if (res.ResultNo == 0 && res.AffectedRows > 0)
            {
                string filename = ISM.Lib.Static.ToStr(res.Param[0]);
                if (string.IsNullOrEmpty(filename))
                {
                    filename = "NONAME";
                }
                else
                {
                    int i = filename.LastIndexOf("\\");
                    if (i > 0) filename = filename.Substring(i + 1);
                }
                if (string.IsNullOrEmpty(outfolder))
                {
                    outfolder = System.IO.Path.GetTempPath();
                }

                filename = string.Format(@"{0}\{1}", outfolder, filename);
                FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                fs.Write(ms.ToArray(), 0, (int)ms.Length);
                fs.Flush();
                fs.Close();

                res.Param = new object[] { filename };
            }
            ms.Close();

            return res;
        }
        static public Result GetBytes(CUser.Remote remote, int PrivNo, ulong AttachId, ref byte[] bytes)
        {
            Result res = null;
            try
            {
                res = Get(remote, PrivNo, AttachId);
                if (res.ResultNo == 0 && res.AffectedRows > 0)
                {
                    bytes = (byte[])res.Data.Tables[0].Rows[0]["attachblob"];
                    res.Param = new object[] { res.Data.Tables[0].Rows[0]["filename"] };
                }
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }

        static public Result Save(CUser.Remote remote, int PrivNo, ulong AttachId, int AttachType, string FileName, string FileDesc, byte[] Blob, int LinkTypeCode, string LinkTypeId)
        {
            Result res = null;
            try
            {

                object[] param = new object[] { 
                                AttachId
                                , AttachType
                                , Blob
                                , LinkTypeCode
                                , LinkTypeId
                                , ISM.Lib.Static.GetFileSmallName(FileName)
                                , FileDesc
                            };
                res = remote.Connection.Call(remote.User.UserNo, 103, 103002, PrivNo, param);
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        static public Result Save(CUser.Remote remote, int PrivNo, ulong AttachId, int AttachType, string FileName, byte[] Blob, int LinkTypeCode, string LinkTypeId)
        {
            return Save(remote, PrivNo, AttachId, AttachType, FileName, "", Blob, LinkTypeCode, LinkTypeId);
        }
        
        static public Result SaveImage(CUser.Remote remote, int PrivNo, ulong AttachId, string FileName, Image AttachImage, int LinkTypeCode, string LinkTypeId)
        {
            return Save(remote, PrivNo, AttachId, 0, FileName, "", Static.ImageToByte(AttachImage), LinkTypeCode, LinkTypeId);
        }
        static public Result SaveImage(CUser.Remote remote, int PrivNo, ulong AttachId, string FileName, string FileDesc, Image AttachImage, int LinkTypeCode, string LinkTypeId)
        {
            return Save(remote, PrivNo, AttachId, 0, FileName, FileDesc, Static.ImageToByte(AttachImage), LinkTypeCode, LinkTypeId);
        }
        
        static public Result SaveImage(CUser.Remote remote, int PrivNo, ulong AttachId, string FileName, byte[] AttachData, int LinkTypeCode, string LinkTypeId)
        {
            return Save(remote, PrivNo, AttachId, 0, FileName, AttachData, LinkTypeCode, LinkTypeId);
        }
        static public Result SaveImage(CUser.Remote remote, int PrivNo, ulong AttachId, string FileName,string FileDesc, byte[] AttachData, int LinkTypeCode, string LinkTypeId)
        {
            return Save(remote, PrivNo, AttachId, 0, FileName, FileDesc, AttachData, LinkTypeCode, LinkTypeId);
        }
        
        static public Result SaveFromFile(CUser.Remote remote, int PrivNo, ulong AttachId, int AttachType, string FileName, string FileDesc, int LinkTypeCode, string LinkTypeId)
        {
            Result res = null;
            byte[] Blob = null;
            try
            {
                res = FileRead(FileName, ref Blob);
                if (res.ResultNo != 0) return res;

                FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Blob = new byte[stream.Length];
                stream.Read(Blob, 0, Blob.Length);
                stream.Close();
                FileInfo file = new FileInfo(FileName);
                res = Save(remote, PrivNo, AttachId, AttachType, file.Name, FileDesc, Blob, LinkTypeCode, LinkTypeId);
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
        static public Result SaveFromFile(CUser.Remote remote, int PrivNo, ulong AttachId, int AttachType, string FileName, int LinkTypeCode, string LinkTypeId)
        {
            return SaveFromFile(remote, PrivNo, AttachId, AttachType, FileName, "", LinkTypeCode, LinkTypeId);
        }

        static public Result Delete(CUser.Remote remote, int PrivNo, ulong AttachId)
        {
            Result res = null;
            try
            {
                object[] param = new object[] { AttachId };
                res = remote.Connection.Call(remote.User.UserNo, 103, 103003, PrivNo, param);
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }

        static public Result FileRead(string filename, ref byte[] data)
        {
            Result res = new Result();
            try
            {
                if (string.IsNullOrEmpty(filename))
                {
                    res = new Result(9, "Файлын нэр тодорхойгүй байна.");
                    return res;
                }
                if (!File.Exists(filename))
                {
                    res = new Result(9, string.Format("Файл дараах хавтаснаас олдсонгүй.\r\n{0}", filename));
                    return res;
                }

                FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
            }
            catch (IOException io)
            {
                res = new Result(9, string.Format("Файл нээлттэй, эсвэл нээхэд алдаа гарлаа. \r\n{0}", filename));
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
            return res;
        }
    }
}
