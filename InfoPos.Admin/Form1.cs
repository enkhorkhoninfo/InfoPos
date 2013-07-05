using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraBars;
using HeavenPro;

namespace WindowsFormsApplication1
{
    public partial class frmMain : Form
    {
        MenuFile mfile = null;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            mfile = new MenuFile();
            mfile.menus = new List<HeavenPro.MenuItem>();
            mfile.images = new ArrayList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mfile.menus.Clear();
            for (int i = 0; i < 3; i++)
            {
                HeavenPro.MenuItem mnu = new HeavenPro.MenuItem();
                mnu.Id = i;
                mnu.Name = "Menu" + i;
                mnu.Caption = "Menu" + i;
                mfile.menus.Add(mnu);

                for (int j = 0; j < 3; j++)
                {
                    HeavenPro.MenuSubItem sub = mnu.AddItem(-1
                        , string.Format ("Id_{0}_{1}",i,j)
                        , "Sub menu " + j, "", false, "HeavenPro.Cust.dll", "frmCustNew", 0, 0);
                    if (i == 2 && j == 2)
                    {
                        sub.AddItem("Subiin sub menu " + j, "", false, "HeavenPro.Insurance.dll", "frmInsurance", 0, 0);
                    }
                }
            }

            HeavenPro.Menu.SaveToFile("menu.txt", mfile);            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            mfile = HeavenPro.Menu.ReadFromFile("menu.txt");            
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            HeavenPro.Menu.SetToControl(mfile, this.barMenu);
        }

        private void barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {
            HeavenPro.Menu.MenuClick(sender, e);
        }

        private void barManager1_ItemPress(object sender, ItemClickEventArgs e)
        {
            //this.Text = string.Format("ItemPress: id={0} name={1}", e.Item.Id, e.Item.Name);
        }
    }
}

