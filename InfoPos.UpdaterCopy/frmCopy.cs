using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Reflection;
using Microsoft.Win32;

namespace InfoPos.UpdaterCopy
{
    public partial class frmCopy : Form
    {
        //Файлын нэр болон хугацааг дамжуулж авч буй хувьсагч
        string[] namedate;
        Timer timer = new Timer();
        int count = 0;
        public frmCopy(string _updatefiles)
        {
            try
            {
                timer.Interval = 5000;
                timer.Tick +=new EventHandler(timer_Tick);
                InitializeComponent();
                _updatefiles = File.ReadAllText(_updatefiles);
                namedate = _updatefiles.Split(' ');
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmCopy_Load(object sender, EventArgs e)
        {
            mmolog.Text = "Түр хүлээнэ үү...";
            timer.Enabled = true;

        }

        void timer_Tick(object sender, EventArgs e)
        {
            count++;
            if (count == 2)
            {
                System.Threading.Thread t;
                t = new System.Threading.Thread(MoveProcess);
                t.Start();
            }
        }
        private void MoveProcess()
        {
            try
            {
                int j = 0;
                string bath = "";
                string temp = @Convert.ToString(XMLCacheGet("frmOption_TempPath", "")) + "\\temp\\";
                progressBarControl1.Properties.Maximum = namedate.Length/4;
                string dllname = "";
                mmolog.Text = "";
                for (int i = 0; i < namedate.Length - 3; i = i + 4)
                {
                    dllname = SSTCryptographer.Decrypt(namedate[i], "SampleKey");
                    string date = namedate[i + 1] + " " + namedate[i + 2];
                    dateTimePicker1.Text = date;
                    switch (Convert.ToInt16(namedate[i + 3]))
                    {
                        case 0: bath = @Convert.ToString(XMLCacheGet("frmOption_TempPath", "")) + "\\"; break;
                        case 1: bath = @Convert.ToString(XMLCacheGet("frmOption_ReportPathIn", "")) + "\\"; break;
                        case 2: bath = @Convert.ToString(XMLCacheGet("frmOption_DynamicPathIn", "")) + "\\"; break;
                        case 3: bath = @Convert.ToString(XMLCacheGet("frmOption_SlipsPathIn", "")) + "\\"; break;
                    }
                    j++;

                    if (File.Exists(bath + dllname))
                    {
                        File.Delete(bath + dllname);
                    }
                    progressBarControl1.EditValue = j;
                    mmolog.Text = mmolog.Text + "\r\n" + Convert.ToString(j) + ". " + dllname + ".........." + date;
                    
                    File.SetLastWriteTime(temp + dllname, dateTimePicker1.Value);
                    File.Move(temp + dllname, bath + dllname);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static public object XMLCacheGet(string key, object defaultvalue = null)
        {
            string _workingfolder = "";
            string s = Assembly.GetExecutingAssembly().Location;
            int i = s.LastIndexOf(@"\");
            if (i > 0)
            {
                _workingfolder = s.Substring(0, i);
            }
            string _xmlcachename = string.Format(@"{0}\Data\Settings.xml", _workingfolder);
            object _xmlcache = XMLCacheOpen(_xmlcachename);
            object ret = null;
            try
            {
                if (_xmlcache != null)
                {
                    Hashtable h = (Hashtable)_xmlcache;
                    ret = h[key];
                    if (ret == null) ret = defaultvalue;
                }
            }
            catch
            { }
            return ret;
        }
        static public object XMLCacheOpen(string filename, bool clean = false)
        {
            Hashtable values = null;
            try
            {
                values = new Hashtable(1024);
                if (!clean && File.Exists(filename))
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.ReadXml(filename);
                        if (dt.Columns.Contains("key") && dt.Columns.Contains("value"))
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                values[row["key"]] = row["value"];
                            }
                        }
                    }
                }
            }
            catch
            { }
            return values;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}