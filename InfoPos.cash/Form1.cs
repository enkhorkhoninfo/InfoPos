using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ISM.Touch;

namespace InfoPos.cash
{
    public partial class Form1 : Form, ISM.Touch.ITouchCall
    {
        void TouchButtonsInit()
        {
            touchButtonGroup1.Add("", "prod1", 1, 1, "prod1", "", null);
            touchButtonGroup1.Add("", "prod2", 1, 2, "prod2", "", null);
            touchButtonGroup1.Add("", "prod3", 1, 3, "prod3", "", null);
            touchButtonGroup1.Add("", "prod4", 1, 4, "prod4", "", null);

            touchButtonGroup1.Add("", "prod5", 2, 1, "prod5", "", null);
            touchButtonGroup1.Add("", "prod6", 2, 2, "PACK6", "", null);
            touchButtonGroup1.Add("", "prod7", 2, 3, "prod7", "", null);
            touchButtonGroup1.Add("", "prod8", 2, 3, "prod8", "", null);

            touchButtonGroup1.Add("prod6", "prod61", 1, 1, "prod61", "", null);
            touchButtonGroup1.Add("prod6", "prod62", 1, 2, "prod62", "", null);
            touchButtonGroup1.Add("prod6", "prod63", 1, 3, "prod63", "", null);
        }

        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            MessageBox.Show("Rent Form1: Init. key=" + buttonkey);
            this.MdiParent = parent;
            this.Show();
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            MessageBox.Show(string.Format("Rent Form1: Call. r={0} c={1} key={2}", item.Row, item.Col, buttonkey));
        }

        public Form1()
        {
            InitializeComponent();
            touchButtonGroup1.EventKeyDown += new ISM.Touch.TouchButtonGroup.delegateKeyDown(touchButtonGroup1_EventKeyDown);
            TouchButtonsInit();
            touchButtonGroup1.Init(20, 20, 2);
        }
        void touchButtonGroup1_EventKeyDown(Control sender, MouseEventArgs e, TouchLinkItem item, ref bool cancel)
        {
            MessageBox.Show(string.Format("Rent Form1: Button clicked. r={0} c={1} key={2}", item.Row, item.Col, sender.Name));
        }
    }
}
