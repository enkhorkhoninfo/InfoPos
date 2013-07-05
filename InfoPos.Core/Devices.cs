using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Management;
using System.IO;

namespace InfoPos.Core
{
    public class Printer
    {
        PrintDialog p = new PrintDialog();

        private System.IO.Ports.SerialPort sp = null;

        public Printer()
        {
            PrintDialog p = new PrintDialog();
            sp = new System.IO.Ports.SerialPort();
        }

        public bool Init(string portname, int rate, int databits, System.IO.Ports.StopBits stopbits, System.IO.Ports.Parity parity)
        {
            if (sp == null) return false;
            bool success = false;
            try
            {
                sp.PortName = portname;
                sp.BaudRate = rate;
                sp.DataBits = databits;
                sp.StopBits = stopbits;
                sp.Parity = parity;
                sp.Handshake = System.IO.Ports.Handshake.None;
                sp.Encoding = Encoding.UTF8;
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public string Open()
        {
            string ret = "";
            if (sp == null) return "Принтер үүсээгүй байна.";
            try
            {
                if (string.IsNullOrEmpty(sp.PortName))
                {
                    ret = "Сериал порт тохируулагдаагүй байна.";
                    goto OnExit;
                }

                if (!sp.IsOpen) sp.Close();

                sp.Open();
                if (!sp.IsOpen)
                {
                    ret = string.Format("Сериал порт нээгдэж чадсангүй.\r\nПорт = {0}", sp.PortName);
                    goto OnExit;
                }
            }
            catch (Exception ex)
            {
                ret = string.Format ("{0}\r\nПорт = {1}", ex.Message, sp.PortName);
            }
            OnExit:
            return ret;
        }
        public bool Close()
        {
            bool success = false;
            try
            {
                if (sp.IsOpen) sp.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public bool Print(string text)
        {
            string mn = "фцужэнгшүзкъйыбөахролдпячёсмитьвюещ ФЦУЖЭНГШҮЗКЪЙЫБӨАХРОЛДПЯЧЁСМИТЬВЮЕЩ";
            string us = "ftujeng$uzkiiïbøahroldpyĉësmitivue$ ftujeng$uzkiiïbøahroldpyĉësmitivue$";
            //string us = "ftujeng$uzkiiiboahroldpycesmitivue$ ftujeng$uzkiiiboahroldpycesmitivue$";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    sb.Append(" ");
                else
                {
                    int index = mn.IndexOf(text[i]);
                    if (index >= 0) sb.Append(us[index]);
                    else sb.Append(text[i]);
                }
            }
            bool success = false;
            try
            {
                if (sp.IsOpen)
                {
                    sp.WriteLine(sb.ToString());
                    success = true;
                }
                else success = false;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
    }
    public class PriceBoard
    {
        private System.IO.Ports.SerialPort sp = null;

        public PriceBoard()
        {
            sp = new System.IO.Ports.SerialPort();
        }

        public bool Init(string portname, int rate, int databits, System.IO.Ports.StopBits stopbits, System.IO.Ports.Parity parity)
        {
            if (sp == null) return false;
            bool success = false;
            try
            {
                sp.PortName = portname;
                sp.BaudRate = rate;
                sp.DataBits = databits;
                sp.StopBits = stopbits;
                sp.Parity = parity;
                sp.Handshake = System.IO.Ports.Handshake.None;
                sp.Encoding = Encoding.UTF8;

                sp.Open();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public bool Open()
        {
            if (sp == null) return false;
            bool success = false;
            try
            {
                if (!sp.IsOpen) sp.Open();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public bool Close()
        {
            bool success = false;
            try
            {
                if (sp.IsOpen) sp.Close();
                success = true;
            }
            catch (Exception ex)
            {
            }
            return success;
        }
        public bool Print(string text)
        {
            bool success = false;
            try
            {
                if (sp.IsOpen)
                {
                    sp.Write(text);
                    success = true;
                }
            }
            catch (Exception ex)
            {
            }
            return success;
        }
    }
    public class Tag
    {
        #region Event
        public delegate void delegateEventOnCard(TagEventData tagdata);
        public event delegateEventOnCard EventOnCard;
        #endregion
        #region Properties

        private int _eventactivateseconds = 3;
        public int EventActivateSeconds
        {
            get { return _eventactivateseconds; }
            set { _eventactivateseconds = value; }
        }

        private bool _isopenned = false;
        public bool IsOpenned
        {
            get { return _isopenned; }
        }

        private bool _eventon = false;
        public bool EventOn
        {
            get { return _eventon; }
            set
            {
                if (value)
                {
                    if (tagreader != null && !_eventon) tagreader.OnCardRead += tagreader_OnCardRead;
                }
                else
                {
                    if (tagreader != null) tagreader.OnCardRead -= tagreader_OnCardRead;
                }
                _eventon = value;
            }
        }

        private string _errormsg = null;
        public string ErrorMessage
        {
            get { return _errormsg; }
        }

        #endregion
        #region Variables
        public Sit.Reader tagreader;
        ArrayList datalist = new ArrayList();
        System.Threading.Timer _timer = null;
        DateTime _lastevent = DateTime.MinValue;
        #endregion
        #region Constructor
        public Tag()
        {
            try
            {
                InitReader();
                _timer = new System.Threading.Timer(new TimerCallback(Timer_Call), null, 0, 500);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Таг төхөөрөмж эхлүүлэх.\r\n"+ex.ToString());
            }
        }
        #endregion
        #region Functions
        public int InitReader()
        {
            string portno = GetReaderPort();
            if (portno != "")
            {
                tagreader = new Sit.Reader(portno);
                tagreader.lastSelectedCardID = null;

                _isopenned = tagreader.InitReader();
                _lastevent = DateTime.Now;
                EventOn = true;
                return 1;
            }
            else { return 0; }
        }
        private void Timer_Call(object state)
        {
            TimeSpan ts = DateTime.Now.Subtract(_lastevent);
            if (ts.TotalSeconds >= _eventactivateseconds)
            {
                if (tagreader != null)
                {
                    tagreader.lastSelectedCardID = "";
                    EventOn = true;
                }
            }
        }

        private string GetReaderPort()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                if (queryObj["Caption"].ToString().Contains("(COM"))
                {
                    string[] data = queryObj["Caption"].ToString().Split(new char[] { '(', ')' });
                    if ("USB-SERIAL CH340".Equals(data[0].Trim()))
                        return data[1].Trim();
                }
            }
            return "";
        }
        public bool Reader_WriteData(string _TagNo, DateTime StartDate, DateTime EndDate)
        {
            bool success = false;
            try
            {
                if (tagreader == null) return false;

                byte sector_1 = 1;
                byte[] data_1 = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0 };

                byte sector_2 = 2;
                byte[] data_2 = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0 };

                byte sector_3 = 3;
                byte[] data_3 = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0 };

                if (!tagreader.SetData(_TagNo, sector_1, data_1, StartDate, EndDate)) goto OnExit;
                if (!tagreader.SetData(_TagNo, sector_2, data_2, StartDate, EndDate)) goto OnExit;
                if (!tagreader.SetData(_TagNo, sector_3, data_3, StartDate, EndDate)) goto OnExit;

                success = true;
            }
            catch (Exception ex)
            {
                _errormsg = ex.ToString();
                return false;
            }
        OnExit:
            _errormsg = success ? "" : tagreader.ErrorMessage;
            return success;
        }
        public TagEventData Reader_GetData(string CardID)
        {
            TagEventData data = new TagEventData();
            try
            {
                if (tagreader == null) return data;

                #region Sector1
                
                DateTime start_1;
                DateTime end_1;
                byte[] data_1 = new byte[16];
                byte sector_1 = 1;
                byte result_1 = tagreader.GetData(CardID, sector_1, data_1, out start_1, out end_1);

                data.errormsg = tagreader.ErrorMessage;
                data.readdate1 = start_1.AddYears(2000);
                data.readdate2 = end_1.AddYears(2000);
                data.readtagno = CardID;
                data.readstatus = result_1; //0-Success, 1-Partial read, 2-Duplicate read, 3-Error
                
                //datalist.Add(result_1);
                //datalist.Add(start_1.AddYears(2000).ToString("yyyy-MM-dd HH:mm:ss"));
                //datalist.Add(end_1.AddYears(2000).ToString("yyyy-MM-dd HH:mm:ss"));
                //datalist.Add(string.Join(",", data_1.Select(x => x.ToString()).ToArray()));
                #endregion
                #region Sector2
                //DateTime start_2;
                //DateTime end_2;
                //byte[] data_2 = new byte[16];
                //byte sector_2 = 2;
                //byte result_2 = tagreader.GetData(CardID, sector_2, data_2, out start_2, out end_2);
                //datalist.Add(result_2);
                //datalist.Add(start_2.AddYears(2000).ToString("yyyy-MM-dd HH:mm:ss"));
                //datalist.Add(end_2.AddYears(2000).ToString("yyyy-MM-dd HH:mm:ss"));
                //datalist.Add(string.Join(",", data_1.Select(x => x.ToString()).ToArray()));
                #endregion
                #region Sector3
                //DateTime start_3;
                //DateTime end_3;
                //byte[] data_3 = new byte[16];
                //byte sector_3 = 3;
                //byte result_3 = tagreader.GetData(CardID, sector_3, data_3, out start_3, out end_3);
                //datalist.Add(result_3);
                //datalist.Add(start_3.AddYears(2000).ToString("yyyy-MM-dd HH:mm:ss"));
                //datalist.Add(end_3.AddYears(2000).ToString("yyyy-MM-dd HH:mm:ss"));
                //datalist.Add(string.Join(",", data_3.Select(x => x.ToString()).ToArray()));
                #endregion

                //return datalist;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //return datalist;
            return data;
        }
        public bool Reader_ClearData(string _TagNo)
        {
            bool success = false;
            DateTime date = new DateTime(2000, 1, 1);
            try
            {
                if (tagreader == null) return false;

                byte[] data = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                if (!tagreader.SetData(_TagNo, 1, data, date, date)) goto OnExit;
                if (!tagreader.SetData(_TagNo, 2, data, date, date)) goto OnExit;
                if (!tagreader.SetData(_TagNo, 3, data, date, date)) goto OnExit;
                success = true;
            }
            catch (Exception ex)
            {
                _errormsg = ex.ToString();
                return false;
            }
            OnExit:
            _errormsg = success ? "" : tagreader.ErrorMessage;
            return success;
        }

        private void tagreader_OnCardRead(object sender, Sit.onCardEventArgs e)
        {
            /************************************************************
             * Эвэнт дуудагдах үед дахин дуудагдахгүйгээр эвэнтийг
             * зогсоосон төлөвт оруулах
             ************************************************************/
            EventOn = false;
            _lastevent = DateTime.Now;
            try
            {
                if (EventOnCard != null)
                {
                    if (!string.IsNullOrEmpty(e.cardID))
                    {
                        TagEventData data = new TagEventData();
                        data.readtagno = e.cardID;
                        data.readstatus = 0;
                        EventOnCard(data);
                    }
                }

                while (!_eventon)
                {
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            { }
        }

        #endregion
    }

    public class TagEventData
    {
        public string writetagno = null;
        public DateTime writedate1 = DateTime.MinValue;
        public DateTime writedate2 = DateTime.MinValue;
        public int writestatus = 3;
        
        public string readtagno = null;
        public DateTime readdate1 = DateTime.MinValue;
        public DateTime readdate2 = DateTime.MinValue;
        public int readstatus = 3;

        public string errormsg = null;
    }
}
