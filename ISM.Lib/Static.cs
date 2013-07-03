using System;
using System.IO;
using System.IO.Compression;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Microsoft.Win32;  // RegisterKey

using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography; //Encrypt descrypt
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace ISM.Lib
{
    static public class Static
    {
        #region Kernel Extern Functions

        #region Struct of SHGetFileInfo function
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        #endregion

        #region Constants

        public const uint FILE_ATTRIBUTE_ARCHIVE = 0x20;
        public const uint FILE_ATTRIBUTE_COMPRESSED = 0x800;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
        public const uint FILE_ATTRIBUTE_HIDDEN = 0x2;
        public const uint FILE_ATTRIBUTE_NORMAL = 0x0;
        public const uint FILE_ATTRIBUTE_READONLY = 0x1;
        public const uint FILE_ATTRIBUTE_SYSTEM = 0x4;
        public const uint SHGFI_ATTRIBUTES = 0x800;
        public const uint SHGFI_DISPLAYNAME = 0x200;
        public const uint SHGFI_EXETYPE = 0x2000;
        public const uint SHGFI_ICONLOCATION = 0x1000;
        public const uint SHGFI_LINKOVERLAY = 0x8000;
        public const uint SHGFI_OPENICON = 0x2;
        public const uint SHGFI_PIDL = 0x8;
        public const uint SHGFI_SELECTED = 0x10000;
        public const uint SHGFI_SHELLICONSIZE = 0x4;
        public const uint SHGFI_SYSICONINDEX = 0x4000;
        public const uint SHGFI_TYPENAME = 0x400;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        public const uint ILD_TRANSPARENT = 0x1; //display transparent
        public const uint BASIC_SHGFI_FLAGS = SHGFI_TYPENAME |
                                        SHGFI_SHELLICONSIZE |
                                        SHGFI_SYSICONINDEX |
                                        SHGFI_DISPLAYNAME |
                                        SHGFI_EXETYPE;
        public const uint MAX_PATH = 260;

        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;    // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1;    // 'Small icon
        public const uint SHGFI_EXTRALARGE = 0x2;    // These images are the Shell standard extra-large icon size. This is typically 48x48, but the size can be customized by the user.
        public const uint SHGFI_SYSSMALL = 0x3;    // These images are the size specified by GetSystemMetrics called with SM_CXSMICON and GetSystemMetrics called with SM_CYSMICON.
        public const uint SHGFI_JUMBO = 0x4;    // Windows Vista and later. The image is normally 256x256 pixels.
  
        #endregion

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(System.IntPtr hIcon);

        #endregion

        #region Static Varibales

        static private string _workingfolder = "C:";
        static public string WorkingFolder
        { get { return _workingfolder; } }

        #endregion

        #region Constractor
        static Static()
        {
            string s = Assembly.GetExecutingAssembly().Location;
            int i = s.LastIndexOf(@"\");
            if (i > 0) _workingfolder = s.Substring(0, i);
        }
        #endregion

        #region Convertion

        static public object ConvToType(Type totype, object value)
        {
            if (value == null) return null;
            object ret = null;
            switch (totype.Name)
            {
                case "Byte":
                    ret = (byte)ToByte(value);
                    break;
                case "Int16":
                    ret = (Int16)ToInt(value);
                    break;
                case "Int32":
                    ret = ToInt(value);
                    break;
                case "Int64":
                    ret = ToLong(value);
                    break;
                case "Float":
                    ret = ToFloat(value);
                    break;
                case "Double":
                    ret = ToDouble(value);
                    break;
                case "Decimal":
                    ret = ToDecimal(value);
                    break;
                case "UInt16":
                    ret = (UInt16)ToInt(value);
                    break;
                case "UInt32":
                    ret = (UInt32)ToInt(value);
                    break;
                case "UInt64":
                    ret = (UInt64)ToLong(value);
                    break;
                case "DateTime":
                    ret = ToDateTime(value);
                    break;
                case "String":
                    ret = ToStr(value);
                    break;
                default:
                    ret = null;
                    break;
            }
            return ret;
        }

        static public bool ToBool(object pObj)
        {
            if (pObj is bool) return (bool)pObj;
            bool result;
            string s = Convert.ToString(pObj);
            if (s == "1") return true;
            bool parsed = bool.TryParse(s, out result);
            return result;
        }
        static public char ToChar(object pobj)
        {
            if (Convert.IsDBNull(pobj) || pobj == null)
                return Convert.ToChar(" ");
            else
                return Convert.ToChar(pobj);
        }
        static public string ToStr(object pobj)
        {
            string ret = "";
            if (pobj != null && !Convert.IsDBNull(pobj))
            {
                ret = Convert.ToString(pobj);
            }
            return ret;
        }

        static public byte ToByte(object pobj)
        {
            byte result = (byte)ToDouble(pobj);
            return result;
        }
        static public int ToInt(object pobj)
        {
            int result = (int)ToDouble(pobj);
            return result;
        }
        static public short ToShort(object pobj)
        {
            if (pobj is short) return (short)pobj;
            short result;
            bool parsed = short.TryParse(Convert.ToString(pobj), System.Globalization.NumberStyles.Number, null, out result);
            return result;
        }
        static public float ToFloat(object pobj)
        {
            if (pobj is float) return (float)pobj;
            float result;
            bool parsed = float.TryParse(Convert.ToString(pobj), System.Globalization.NumberStyles.Float, null, out result);
            return result;
        }
        static public Single ToSingle(object pobj)
        {
            if (pobj is Single) return (Single)pobj;
            Single result;
            bool parsed = Single.TryParse(Convert.ToString(pobj), System.Globalization.NumberStyles.Float, null, out result);
            return result;
        }
        static public double ToDouble(object pobj)
        {
            if (pobj is double) return (double)pobj;
            double result;
            bool parsed;

            if (pobj is bool) result = Convert.ToDouble(pobj);
            else parsed = double.TryParse(Convert.ToString(pobj), System.Globalization.NumberStyles.Float, null, out result);
            return result;
        }
        static public long ToLong(object pobj)
        {
            if (pobj is long) return (long)pobj;
            long result;
            bool parsed;

            if (pobj is bool) result = Convert.ToInt64(pobj);
            else parsed = long.TryParse(Convert.ToString(pobj), System.Globalization.NumberStyles.Any, null, out result);
            return result;
        }
        static public ulong ToULong(object pobj)
        {
            if (pobj is ulong) return (ulong)pobj;
            ulong result;
            bool parsed;

            if (pobj is bool) result = Convert.ToUInt64(pobj);
            else parsed = ulong.TryParse(Convert.ToString(pobj), System.Globalization.NumberStyles.Any, null, out result);
            return result;
        }
        static public decimal ToDecimal(object pobj)
        {
            if (pobj is decimal) return (decimal)pobj;
            decimal result;
            bool parsed = decimal.TryParse(Convert.ToString(pobj), System.Globalization.NumberStyles.Any
                , null //Thread.CurrentThread.CurrentCulture.NumberFormat
                , out result);
            return result;
        }

        static public DateTime ToDate(object pobj)
        {
            if (pobj is DateTime) return (DateTime)pobj;

            DateTime result;
            bool parsed = DateTime.TryParseExact(Convert.ToString(pobj)
                , new string[] { "G", "yyyyMMdd HH:mm:ss", "yyyy/M/d HH:mm:ss", "yyyy-M-d HH:mm:ss", "yyyy.M.d HH:mm:ss"
                , "yyyyMMdd", "yyyy/M/d", "yyyy-M-d", "yyyy.M.d"}
                , null, System.Globalization.DateTimeStyles.None, out result);

            return result;
        }
        static public DateTime ToDateTime(object pobj)
        {
            if (pobj is DateTime) return (DateTime)pobj;
            DateTime result;
            bool parsed = DateTime.TryParseExact(Convert.ToString(pobj)
                , new string[] { "G", "yyyyMMdd HH:mm:ss", "yyyy/M/d HH:mm:ss", "yyyy-M-d HH:mm:ss", "yyyy.M.d HH:mm:ss"
                , "yyyyMMdd", "yyyy/M/d", "yyyy-M-d", "yyyy.M.d"}
                , null, System.Globalization.DateTimeStyles.None, out result);

            return result;
        }

        static public string ToDateStr(object pobj)
        {
            return ToDate(pobj).ToString("yyyy.MM.dd");
        }
        static public string ToDateTimeStr(object pobj)
        {
            return ToDateTime(pobj).ToString("yyyy.MM.dd HH:mm:ss");
        }

        #endregion

        #region String Related

        static public string SubStr(string source, int startindex, int len)
        {
            string ret = "";
            if (source != null && startindex >= 0)
            {
                int maxlen = source.Length;
                int l = (maxlen >= startindex + len) ? len : maxlen - startindex;
                ret = source.Substring(startindex, l);
            }
            return ret;
        }
        static public string SubStr(string source, int startindex)
        {
            string ret = "";
            if (source != null && startindex >= 0)
            {
                int maxlen = source.Length;
                if (maxlen > startindex)
                    ret = source.Substring(startindex, maxlen - startindex);
            }
            return ret;
        }

        #endregion

        #region Matching Regular Expression

        static public string ToMask(string mask, Hashtable values)
        {
            #region Collect specific characters
            StringBuilder sb = new StringBuilder();
            if (values != null)
            {
                foreach (string s in values.Keys)
                {
                    sb.Append(s);
                }
            }
            #endregion
            #region Collect pattern characters
            string pattern = @"\[([" + sb.ToString() + @"]{1})(\d+)\]";
            #endregion
            #region Matching and replacing
            MatchCollection matches = Regex.Matches(mask, pattern, RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                string key = m.Groups[1].Value;
                int len = Convert.ToInt32(m.Groups[2].Value);
                object val = values[key];
                if (val != null)
                {
                    string str = ToStr(val);
                    if (str.Length > len)
                    {
                        str = str.Substring(str.Length - len);
                    }
                    else
                    {
                        str = str.PadLeft(len, '0');
                    }
                    mask = mask.Replace(m.Value, str);
                }
            }
            #endregion
            return mask;
        }

        #endregion

        #region Crypto

        static public string Encrypt(string toHash)
        {
            byte[] inArr;
            byte[] outArr;

            inArr = System.Text.Encoding.Default.GetBytes(toHash); // ANSI
            SHA1Managed hash = new SHA1Managed();
            outArr = hash.ComputeHash(inArr);

            return Convert.ToBase64String(outArr);
        }

        #endregion

        #region System

        static public void RegisterSave(string path, string mod, string key, object val)
        {
            RegisterSave(path + @"\" + mod, key, val);
        }
        static public object RegisterGet(string path, string mod, string key, object defaultvalue)
        {
            return RegisterGet(path + @"\" + mod, key, defaultvalue);
        }
        static public void RegisterSave(string mod, string key, object val)
        {
            try
            {
                string user = Environment.UserDomainName + @"\" + Environment.UserName;

                System.Security.AccessControl.RegistryAccessRule regRule = new System.Security.AccessControl.RegistryAccessRule(
                    user
                    , System.Security.AccessControl.RegistryRights.FullControl
                    , System.Security.AccessControl.AccessControlType.Allow
                );
                RegistryKey rkServer = Registry.LocalMachine.CreateSubKey(
                    "Software\\" + mod
                    , RegistryKeyPermissionCheck.ReadWriteSubTree
                );
                System.Security.AccessControl.RegistrySecurity regSecurity = rkServer.GetAccessControl();
                regSecurity.AddAccessRule(regRule);
                rkServer.SetAccessControl(regSecurity);

                string subkey = null;
                string valkey = null;
                int index = key.LastIndexOf(@"\");
                if (index >= 0)
                {
                    subkey = key.Substring(0, index);
                    valkey = key.Substring(index + 1);

                    RegistryKey rkValue = rkServer.CreateSubKey(subkey, RegistryKeyPermissionCheck.ReadWriteSubTree, regSecurity);
                    System.Security.AccessControl.RegistrySecurity regValSecurity = rkValue.GetAccessControl();
                    regValSecurity.AddAccessRule(regRule);
                    rkValue.SetAccessControl(regValSecurity);
                    rkValue.SetValue(valkey, val);
                }
                else
                {
                    rkServer.SetValue(key, val);
                }
                rkServer.Close();
            }
            catch (Exception)
            { }
        }
        static public object RegisterGet(string mod, string key, object defaultvalue)
        {
            object ret = defaultvalue;
            try
            {
                string user = Environment.UserDomainName + @"\" + Environment.UserName;

                System.Security.AccessControl.RegistryAccessRule regRule = new System.Security.AccessControl.RegistryAccessRule(
                    user
                    , System.Security.AccessControl.RegistryRights.FullControl
                    , System.Security.AccessControl.AccessControlType.Allow
                );
                RegistryKey rkServer = Registry.LocalMachine.CreateSubKey(
                    "Software\\" + mod
                    , RegistryKeyPermissionCheck.ReadWriteSubTree
                );
                System.Security.AccessControl.RegistrySecurity regSecurity = rkServer.GetAccessControl();
                regSecurity.AddAccessRule(regRule);
                rkServer.SetAccessControl(regSecurity);

                string subkey = null;
                string valkey = null;
                int index = key.LastIndexOf(@"\");
                if (index >= 0)
                {
                    subkey = key.Substring(0, index);
                    valkey = key.Substring(index + 1);

                    RegistryKey rkValue = rkServer.CreateSubKey(subkey, RegistryKeyPermissionCheck.ReadWriteSubTree, regSecurity);
                    System.Security.AccessControl.RegistrySecurity regValSecurity = rkValue.GetAccessControl();
                    regValSecurity.AddAccessRule(regRule);
                    rkValue.SetAccessControl(regValSecurity);
                    ret = rkValue.GetValue(valkey, defaultvalue);
                }
                else
                {
                    ret = rkServer.GetValue(key, defaultvalue);
                }
                rkServer.Close();
            }
            catch (Exception)
            { }

            return ret;
        }

        static public string NetworkGetIp(string pstrComputerName)
        {
            string getIPAddressReturn = null;

            try
            {
                System.Net.IPHostEntry HEServer = new System.Net.IPHostEntry();
                //HEServer = System.Net.Dns.GetHostEntry(pstrComputerName);
                HEServer = System.Net.Dns.Resolve(pstrComputerName);
                getIPAddressReturn = HEServer.AddressList[0].ToString();
                HEServer = null;
            }
            catch { }
            return getIPAddressReturn;
        }
        static public string NetworkGetNic()
        {
            try
            {
                NetworkInterface[] ni = NetworkInterface.GetAllNetworkInterfaces();
                PhysicalAddress pa = ni[0].GetPhysicalAddress();

                return pa.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static string NetworkGetNic(string RemoteHostName)
        {
            try
            {
                byte[] ab = new byte[6];
                int len = ab.Length;

                System.Net.IPHostEntry Tempaddr = null;

                //Tempaddr = (System.Net.IPHostEntry)Dns.GetHostEntry(RemoteHostName);
                Tempaddr = (System.Net.IPHostEntry)Dns.Resolve(RemoteHostName);

                //int Address = (int)Tempaddr.AddressList[0].Address;
                int Address = (int)Tempaddr.AddressList[0].Address;
                int r = SendARP(Address, 0, ab, ref len);

                string mac = BitConverter.ToString(ab, 0, 6);

                return mac;
            }
            catch (Exception)
            {
                return "";
            }
        }

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);

        #endregion

        #region Marshaling
        static public byte[] MarshalingToByte(object any)
        {
            int size = Marshal.SizeOf(any);
            IntPtr buffer = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(any, buffer, false);

            byte[] data = new byte[size];

            Marshal.Copy(buffer, data, 0, size);
            Marshal.FreeHGlobal(buffer);

            return data;
        }
        static public object MarshalingToStruct(byte[] data, System.Type any)
        {
            int size = Marshal.SizeOf(any);
            if (size > data.Length)
                return null;

            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.Copy(data, 0, buffer, size);

            object retobj = Marshal.PtrToStructure(buffer, any);
            Marshal.FreeHGlobal(buffer);

            return retobj;
        }
        #endregion

        #region File
        static public void CheckFileFolder(string filename)
        {
            string folder = Path.GetDirectoryName(filename);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(Path.GetDirectoryName(filename));
        }
        static public string GetFileName(string fullname)
        {
            if (string.IsNullOrEmpty(fullname)) return null;
            string filename;
            int i = fullname.LastIndexOf("\\");
            if (i > 0)
            {
                filename = fullname.Substring(i + 1);
            }
            else
            {
                filename = fullname;
            }
            return filename;
        }
        static public string GetFileSmallName(string fullname)
        {
            System.IO.FileInfo file = new FileInfo(fullname);
            return file.Name;
        }
        static public void WriteToLogFile(string s)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(_workingfolder);
                sb.Append("\\embserver_");
                sb.Append(DateTime.Today.ToString("yyyyMMdd"));
                sb.Append(".log");

                StreamWriter sw = new StreamWriter(sb.ToString(), true);
                sw.WriteLine(DateTime.Now.ToString("HH.mm.ss fff: ") + s);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
            }
        }
        static public void WriteToLogFile(string filename, string s)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("{0}\\{1}_{2}.log", _workingfolder, filename, DateTime.Today.ToString("yyyyMMdd")));

                StreamWriter sw = new StreamWriter(sb.ToString(), true);
                sw.WriteLine(DateTime.Now.ToString("HH.mm.ss fff: ") + s);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
            }
        }
        #endregion

        #region Image Related

        static public byte[] ImageToByte(Image img)
        {
            byte[] bytes = null;
            if (img != null)
            {
                using (Bitmap bmp = new System.Drawing.Bitmap(img))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Jpeg);
                        bytes = ms.ToArray();
                    }
                }
            }
            return bytes;
        }
        static public Image ImageFromByte(byte[] bytes)
        {
            Image img = null;
            if (bytes != null && bytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    img = Image.FromStream(ms, true);
                }
            }
            return img;
        }
        static public Image ImageFromStream(Stream stream)
        {
            Image img = null;
            if (stream != null && stream.Length > 0)
            {
                img = Image.FromStream(stream, true);
            }
            return img;
        }

        static public Image ImageResize(Image img, int width, int height)
        {
            Image imgSrc = img;
            Bitmap bmpSrc = new Bitmap(imgSrc);

            Bitmap bmpDest = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmpDest);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            RectangleF rectSrc = new RectangleF(0, 0, bmpSrc.Width, bmpSrc.Height);
            RectangleF rectDest = new RectangleF(0, 0, bmpDest.Width, bmpDest.Height);
            g.DrawImage(bmpSrc, rectDest, rectSrc, GraphicsUnit.Pixel);

            return bmpDest;
        }
        static public byte[] ImageResize(byte[] img, int width, int height)
        {
            Image imgSrc = ImageFromByte(img);
            Image imgDest = ImageResize(imgSrc, width, height);
            return ImageToByte(imgDest);
        }

        static public Image ImageGetThumbnail(string path)
        {
            Image image = null;
            try
            {
                FileStream fs = File.OpenRead(path);
                Image img = null;
                img = Image.FromStream(fs, true);
                if (img != null) image = img.GetThumbnailImage(100, 100, null, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return image;
        }

        static public Image ImageFileExt(string filename)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImg;

            hImg = SHGetFileInfo(filename, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo)
                , SHGFI_ICON | SHGFI_JUMBO | SHGFI_USEFILEATTRIBUTES | SHGFI_SYSICONINDEX);
            Bitmap bmp = Bitmap.FromHicon(shinfo.hIcon);
            DestroyIcon(shinfo.hIcon);

            return bmp;
        }

        #endregion

        #region Invoke

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dll"></param>
        /// <param name="cls"></param>
        /// <param name="fnc"></param>
        /// <param name="p"></param>
        /// <returns>
        /// 0-Ok
        /// 1-DLL file not found.
        /// 2-Class not found in specified DLL file.
        /// 3-Method not found in specified class.
        /// 4-Call invoke function error.
        /// </returns>
        static public int Invoke(string dll, string cls, string fnc, object[] p)
        {
            int ret = 0;
            try
            {
                Assembly asm = null;
                Type type = null;
                object obj = null;
                MethodInfo mi = null;

                #region Load DLL file
                try
                {
                    asm = Assembly.LoadFrom(Path.Combine(_workingfolder, dll));
                }
                catch
                {
                    ret = 1;
                    goto OnExit;
                }
                #endregion

                #region Create class instance
                try
                {
                    type = asm.GetType(cls, true, true);
                    obj = Activator.CreateInstance(type);
                }
                catch
                {
                    ret = 2;
                    goto OnExit;
                }
                #endregion

                #region Read method info

                mi = obj.GetType().GetMethod(fnc, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (mi.IsStatic)
                {
                    type.InvokeMember(fnc, BindingFlags.InvokeMethod, null, type, new object[] { p });
                }
                else
                {
                    mi.Invoke(obj, new object[] { p });
                }
                #endregion
            }
            catch (Exception ex)
            {
                ret = 4;
                throw ex;
            }
        OnExit:
            return ret;
        }
        static public object Invoke(string cls, string fnc, object[] p)
        {
            object ret = null;
            try
            {
                Assembly asm = null;
                Type type = null;
                object obj = null;
                MethodInfo mi = null;

                #region Load DLL file
                asm = Assembly.GetCallingAssembly();
                #endregion

                #region Create class instance
                type = asm.GetType(cls, true, true);
                obj = Activator.CreateInstance(type);
                #endregion

                #region Read method info

                mi = obj.GetType().GetMethod(fnc, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (mi.IsStatic)
                {
                    type.InvokeMember(fnc, BindingFlags.InvokeMethod, null, type, new object[] { p });
                }
                else
                {
                    mi.Invoke(obj, new object[] { p });
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        static public object Invoke(object sender, string fnc, object[] p)
        {
            object ret = null;
            try
            {
                #region Read method info
                MethodInfo mi = sender.GetType().GetMethod(fnc, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                ret = mi.Invoke(sender, new object[] { p });
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        #endregion

        #region Compression
        static public byte[] Compress(byte[] source)
        {
            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                DeflateStream def = new DeflateStream(ms, CompressionMode.Compress);
                def.Write(source, 0, source.Length);
                def.Flush();
                bytes = ms.ToArray();
                def.Close();
            }
            return bytes;
        }
        static public byte[] Compress(MemoryStream stream)
        {
            return Compress(stream.ToArray());
        }
        static public byte[] Decompress(byte[] source)
        {
            byte[] bytes = null;
            byte[] buffer = new byte[1024];
            using (MemoryStream ms = new MemoryStream(source))
            {
                using (MemoryStream os = new MemoryStream())
                {
                    DeflateStream def = new DeflateStream(ms, CompressionMode.Decompress);
                    def.CopyTo(os);
                    def.CopyTo(os);
                    bytes = os.ToArray();
                    def.Dispose();
                }
            }
            return bytes;
        }
        static public byte[] Decompress(MemoryStream stream)
        {
            return Decompress(stream.ToArray());
        }
        #endregion

        #region INI File

        //static public 

        #endregion

        #region Thumbnail Images

        // GDI plus functions
        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int GdipGetPropertyItem(IntPtr image, int propid, int size, IntPtr buffer);

        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int GdipGetPropertyItemSize(IntPtr image, int propid, out int size);

        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int GdipLoadImageFromFile(string filename, out IntPtr image);

        [DllImport("gdiplus.dll", EntryPoint = "GdipDisposeImage", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int GdipDisposeImage(IntPtr image);


        // EXIT tag value for thumbnail data. Value specified by EXIF standard
        private static int THUMBNAIL_DATA = 0x501B;

        /// <summary>
        /// Reads the thumbnail in the given image. If no thumbnail is found, returns null
        /// </summary>
        public static Image ReadThumb(string imagePath)
        {
            const int GDI_ERR_PROP_NOT_FOUND = 19;	// Property not found error
            const int GDI_ERR_OUT_OF_MEMORY = 3;

            IntPtr hImage = IntPtr.Zero;
            IntPtr buffer = IntPtr.Zero;	// Holds the thumbnail data
            int ret;
            ret = GdipLoadImageFromFile(imagePath, out hImage);

            try
            {
                if (ret != 0)
                    throw createException(ret);

                int propSize;
                ret = GdipGetPropertyItemSize(hImage, THUMBNAIL_DATA, out propSize);
                // Image has no thumbnail data in it. Return null
                if (ret == GDI_ERR_PROP_NOT_FOUND)
                    return null;
                if (ret != 0)
                    throw createException(ret);


                // Allocate a buffer in memory
                buffer = Marshal.AllocHGlobal(propSize);
                if (buffer == IntPtr.Zero)
                    throw createException(GDI_ERR_OUT_OF_MEMORY);

                ret = GdipGetPropertyItem(hImage, THUMBNAIL_DATA, propSize, buffer);
                if (ret != 0)
                    throw createException(ret);

                // buffer has the thumbnail data. Now we have to convert it to
                // an Image
                return convertFromMemory(buffer);
            }

            finally
            {
                // Free the buffer
                if (buffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(buffer);

                GdipDisposeImage(hImage);
            }
        }

        /// <summary>
        /// Generates an exception depending on the GDI+ error codes (I removed some error
        /// codes)
        /// </summary>
        private static Exception createException(int gdipErrorCode)
        {
            switch (gdipErrorCode)
            {
                case 1:
                    return new ExternalException("Gdiplus Generic Error", -2147467259);
                case 2:
                    return new ArgumentException("Gdiplus Invalid Parameter");
                case 3:
                    return new OutOfMemoryException("Gdiplus Out Of Memory");
                case 4:
                    return new InvalidOperationException("Gdiplus Object Busy");
                case 5:
                    return new OutOfMemoryException("Gdiplus Insufficient Buffer");
                case 7:
                    return new ExternalException("Gdiplus Generic Error", -2147467259);
                case 8:
                    return new InvalidOperationException("Gdiplus Wrong State");
                case 9:
                    return new ExternalException("Gdiplus Aborted", -2147467260);
                case 10:
                    return new FileNotFoundException("Gdiplus File Not Found");
                case 11:
                    return new OverflowException("Gdiplus Over flow");
                case 12:
                    return new ExternalException("Gdiplus Access Denied", -2147024891);
                case 13:
                    return new ArgumentException("Gdiplus Unknown Image Format");
                case 18:
                    return new ExternalException("Gdiplus Not Initialized", -2147467259);
                case 20:
                    return new ArgumentException("Gdiplus Property Not Supported Error");
            }

            return new ExternalException("Gdiplus Unknown Error", -2147418113);
        }



        /// <summary>
        /// Converts the IntPtr buffer to a property item and then converts its 
        /// value to a Drawing.Image item
        /// </summary>
        private static Image convertFromMemory(IntPtr thumbData)
        {
            propertyItemInternal prop =
                (propertyItemInternal)Marshal.PtrToStructure
                (thumbData, typeof(propertyItemInternal));

            // The image data is in the form of a byte array. Write all 
            // the bytes to a stream and create a new image from that stream
            byte[] imageBytes = prop.Value;
            MemoryStream stream = new MemoryStream(imageBytes.Length);
            stream.Write(imageBytes, 0, imageBytes.Length);

            return Image.FromStream(stream);
        }

        /// <summary>
        /// Used in Marshal.PtrToStructure().
        /// We need this dummy class because Imaging.PropertyItem is not a "blittable"
        /// class and Marshal.PtrToStructure only accepted blittable classes.
        /// (It's not blitable because it uses a byte[] array and that's not a blittable
        /// type. See MSDN for a definition of Blittable.)
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class propertyItemInternal
        {
            public int id = 0;
            public int len = 0;
            public short type = 0;
            public IntPtr value = IntPtr.Zero;

            public byte[] Value
            {
                get
                {
                    byte[] bytes = new byte[(uint)len];
                    Marshal.Copy(value, bytes, 0, len);
                    return bytes;
                }
            }
        }


        #endregion

        #region JSON

        public static string JsonValue(object obj)
        {
            StringBuilder sb = new StringBuilder();

            if (obj == null || obj is DBNull)
                sb.Append("null");
            else if (obj is string || obj is char)
            {
                sb.Append('"');
                sb.Append((string)obj);
                sb.Append('"');
            }
            else if (obj is bool)
                sb.Append(((bool)obj) ? "true" : "false"); // conform to standard

            else if (
                obj is int || obj is long || obj is double ||
                obj is decimal || obj is float ||
                obj is byte || obj is short ||
                obj is sbyte || obj is ushort ||
                obj is uint || obj is ulong
            )
                sb.Append(((IConvertible)obj).ToString(NumberFormatInfo.InvariantInfo));
            else if (obj is DateTime)
            {
                // datetime format standard : yyyy-MM-dd HH:mm:ss
                DateTime dt = (DateTime)obj;
                //if (UseUTCDateTime) dt = dateTime.ToUniversalTime();

                sb.Append("\"");
                sb.Append(dt.Year.ToString("0000", NumberFormatInfo.InvariantInfo));
                sb.Append("-");
                sb.Append(dt.Month.ToString("00", NumberFormatInfo.InvariantInfo));
                sb.Append("-");
                sb.Append(dt.Day.ToString("00", NumberFormatInfo.InvariantInfo));
                sb.Append(" ");
                sb.Append(dt.Hour.ToString("00", NumberFormatInfo.InvariantInfo));
                sb.Append(":");
                sb.Append(dt.Minute.ToString("00", NumberFormatInfo.InvariantInfo));
                sb.Append(":");
                sb.Append(dt.Second.ToString("00", NumberFormatInfo.InvariantInfo));

                //if (UseUTCDateTime) sb.Append("Z");

                sb.Append("\"");
            }
            return sb.ToString();
        }
        public static string JsonFromDataTable(DataTable src)
        {
            if (src == null) return "";
            StringBuilder sb = new StringBuilder();
            bool isfirstrow = true;
                int rowindex = 0;

            sb.Append("{\"rows\":[");
            DataColumnCollection cols = src.Columns;
            foreach (DataRow row in src.Rows)
            {
                if (!isfirstrow) sb.Append(",");
                isfirstrow = false;

                sb.Append("{\"id\":");
                sb.Append(rowindex);
                sb.Append(",\"cell\":[");

                bool isfirstfield = true;
                foreach (DataColumn column in cols)
                {
                    if (!isfirstfield) sb.Append(',');
                    sb.Append(JsonValue(row[column]));
                    isfirstfield = false;
                }
                sb.Append("]}");
            }
            sb.Append("]}");
            return sb.ToString();
        }
        public static string JsonFromDataSet(DataSet src)
        {
            if (src == null) return "";
            StringBuilder sb = new StringBuilder();
            bool isfirst = true;

            sb.Append('{');
            foreach (DataTable table in src.Tables)
            {
                if (!isfirst) sb.Append(',');
                sb.Append('"');
                sb.Append(table.TableName);
                sb.Append("\":");
                sb.Append(JsonFromDataTable(table));
                isfirst = false;
            }
            sb.Append('}');
            return sb.ToString();
        }

        #endregion
    }

}
