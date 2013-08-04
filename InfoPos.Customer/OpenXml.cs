using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EServ.Shared;
using System.Collections;

using ISM.Template;

namespace InfoPos.Customer
{
    public partial class OpenXml : Form
    {
        #region[variables]
        InfoPos.Core.Core _core;
        object _xmlcache = null;
        string filename = "";
        #endregion

        #region[Functions]
        public OpenXml(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
        }
        #endregion
        private void OpenXml_Load(object sender, EventArgs e)
        {

        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    txtFileName.EditValue = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            filename = Static.ToStr(txtFileName.EditValue);

            grdXML.DataSource = null;
            DataTable dt = new DataTable();

            try
            {
                if (File.Exists(filename))
                {
                    _xmlcache = ISM.Lib.XML.XMLCacheOpen(filename);
                    dt = ISM.Lib.XML.XMLCacheGetDT(_xmlcache);

                    if (dt != null)
                        grdXML.DataSource = dt;
                    else
                        MessageBox.Show("DataTable is null");
                    //_xmlcache

                    //dt.ReadXml(filename);
                    //grdXML.DataSource = dt;

                    //DataTable tabl = new DataTable("mytable");
                    //tabl.Columns.Add(new DataColumn("id", typeof(int)));
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    DataRow row = tabl.NewRow();
                    //    row["id"] = i;
                    //    tabl.Rows.Add(row);
                    //}
                    //tabl.WriteXml("c://f.xml", XmlWriteMode.WriteSchema);
                    //DataTable newt = new DataTable();
                    //newt.ReadXml("c://f.xml");

                }
                else
                    MessageBox.Show("Файлаа сонгоно уу !!!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            string filename = Static.ToStr(txtFileName.EditValue);

            DataTable tabl = new DataTable("PERSONAL");

            try
            {
                if (File.Exists(filename))
                {
                    tabl.Columns.Add(new DataColumn("RegisterNo", typeof(string)));
                    tabl.Columns.Add(new DataColumn("value", typeof(string)));

                    //tabl.Columns.Add(new DataColumn("FirstName", typeof(string)));
                    //tabl.Columns.Add(new DataColumn("LastName", typeof(string)));
                    //tabl.Columns.Add(new DataColumn("MiddleName", typeof(string)));
                    //tabl.Columns.Add(new DataColumn("Sex", typeof(int)));
                    //tabl.Columns.Add(new DataColumn("BirthDay", typeof(DateTime)));
                    //tabl.Columns.Add(new DataColumn("Email", typeof(string)));
                    //tabl.Columns.Add(new DataColumn("Mobile", typeof(string)));
                    //tabl.Columns.Add(new DataColumn("Company", typeof(string)));
                    //tabl.Columns.Add(new DataColumn("CountryCode", typeof(int)));
                    //tabl.Columns.Add(new DataColumn("Height", typeof(float)));
                    //tabl.Columns.Add(new DataColumn("FootSize", typeof(float)));

                    for (int i = 0; i < 1000000; i++)
                    {
                        DataRow row = tabl.NewRow();
                        row["RegisterNo"] = "ЗМ" + i;
                        row["value"] = ("Болд" + i) + "||" + ("Бат" + i) + "||" + ("ББ" + i) + "||" + "1" + "||" + Static.ToDate(DateTime.Now) +
                             "||" + "test@test.com" + "||" + "9999" + "||" + "SW" + "||" + (i + 1) + "||" + (i + 2) + "||" + (i + 3);

                        //row["FirstName"] = "Болд" + i;
                        //row["LastName"] = "Бат" + i;
                        //row["MiddleName"] = "ББ" + i;
                        //row["Sex"] = 1;
                        //row["BirthDay"] = Static.ToDate(DateTime.Now);
                        //row["Email"] = "";
                        //row["Mobile"] = "9999";
                        //row["Company"] = "ЗМ";
                        //row["CountryCode"] = i;
                        //row["Height"] = i;
                        //row["FootSize"] = i;

                        tabl.Rows.Add(row);
                    }
                    tabl.WriteXml(filename, XmlWriteMode.WriteSchema);
                    DataTable newt = new DataTable();
                    newt.ReadXml(filename);
                }
                else
                    MessageBox.Show("Файлаа сонгоно уу !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnMemory_Click(object sender, EventArgs e)
        {
            //Hashtable ht = new Hashtable();
            //ht.Add("001", "Zara Ali");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            object obj = ISM.Lib.XML.XMLCacheSet(_xmlcache, Static.ToStr(txtRegisterNo.EditValue).ToUpper(), txtvalue.EditValue);
            //ISM.Lib.XML.XMLCacheSave(filename, _xmlcache);
            MessageBox.Show("Хашруу амжилттай хадгаллаа");
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string getstr = ISM.Lib.XML.XMLCacheGetStr(_xmlcache, Static.ToStr(txtRegisterNo.EditValue).ToUpper());
            MessageBox.Show(getstr);
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            ISM.Lib.XML.XMLCacheSave(filename, _xmlcache);
            MessageBox.Show("Амжилттай хадгаллаа");
        }
    }
}
