using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace InfoPos.Panels
{
    public partial class ucNumpad : UserControl
    {
        public Control EditControl = null;
        public bool EnableKey = true;
        public bool EnableActiveControl = true;

        #region Constructor and Events

        public ucNumpad()
        {
            try
            {
                InitializeComponent();
            }
            catch
            { }
        }
        private void ucNumpad_Load(object sender, EventArgs e)
        {

        }

        private void lbl0_Click(object sender, EventArgs e)
        {
            SetControlValue("0");
        }
        private void lbl1_Click(object sender, EventArgs e)
        {
            SetControlValue("1");
        }
        private void lbl2_Click(object sender, EventArgs e)
        {
            SetControlValue("2");
        }
        private void lbl3_Click(object sender, EventArgs e)
        {
            SetControlValue("3");
        }
        private void lbl4_Click(object sender, EventArgs e)
        {
            SetControlValue("4");
        }
        private void lbl5_Click(object sender, EventArgs e)
        {
            SetControlValue("5");
        }
        private void lbl6_Click(object sender, EventArgs e)
        {
            SetControlValue("6");
        }
        private void lbl7_Click(object sender, EventArgs e)
        {
            SetControlValue("7");
        }
        private void lbl8_Click(object sender, EventArgs e)
        {
            SetControlValue("8");
        }
        private void lbl9_Click(object sender, EventArgs e)
        {
            SetControlValue("9");
        }

        private void lblDot_Click(object sender, EventArgs e)
        {
            SetControlValue(".");
        }
        private void lblBack_Click(object sender, EventArgs e)
        {
            SetControlValue("Back");
        }
        private void lblCE_Click(object sender, EventArgs e)
        {
            SetControlValue("CE");
        }

        private void lblExtra1_Click(object sender, EventArgs e)
        {
            EventClickExtraButtonFire(1);
        }
        private void lblExtra2_Click(object sender, EventArgs e)
        {
            EventClickExtraButtonFire(2);
        }
        private void lblExtra3_Click(object sender, EventArgs e)
        {
            EventClickExtraButtonFire(3);
        }

        #endregion
        #region Custom Events

        public delegate void DelegateEvenClickExtraButton(int num);
        public event DelegateEvenClickExtraButton EventClickExtraButton;
        public void EventClickExtraButtonFire(int num)
        {
            if (EventClickExtraButton != null) EventClickExtraButton(num);
        }

        #endregion
        #region Custom Properties

        public bool ExtraShow1
        {
            get { return lblExtra1.Visible; }
            set { lblExtra1.Visible = value; }
        }
        public bool ExtraShow2
        {
            get { return lblExtra2.Visible; }
            set { lblExtra2.Visible = value; }
        }
        public bool ExtraShow3
        {
            get { return lblExtra3.Visible; }
            set { lblExtra3.Visible = value; }
        }

        public string ExtraText1
        {
            get { return lblExtra1.Text; }
            set { lblExtra1.Text = value; }
        }
        public string ExtraText2
        {
            get { return lblExtra2.Text; }
            set { lblExtra2.Text = value; }
        }
        public string ExtraText3
        {
            get { return lblExtra3.Text; }
            set { lblExtra3.Text = value; }
        }

        #endregion
        private Form GetParentForm()
        {
            Form form = null;
            Control parent = this;
            while (parent != null)
            {
                if (parent is Form)
                {
                    form = (Form)parent;
                    break;
                }
                else
                {
                    parent = parent.Parent;
                }
            }
            return form;
        }
        public void SetControlValue(string digit)
        {
            Control ctrl = EditControl;

            #region Validation
            if (!EnableKey) return;
            if (EnableActiveControl)
            {
                Form frm = GetParentForm();
                if (frm != null)
                {
                    ctrl = frm.ActiveControl;
                    if (ctrl.Parent is BaseEdit) ctrl = ctrl.Parent;
                    if (ctrl != EditControl)
                    {
                        if (!(ctrl is BaseEdit) || ((ctrl is BaseEdit) && ((BaseEdit)ctrl).Properties.ReadOnly))
                        {
                            ctrl = EditControl; // Контрол олдоогүй Дефаулт контролоо авах
                            if (ctrl!= null) ((BaseEdit)ctrl).EditValue = null;
                        }
                    }
                }
            }
            if (ctrl == null) return;
            if (!(ctrl is BaseEdit)) return;
            #endregion
            #region Send key
            BaseEdit be = (BaseEdit)(ctrl);
            char keychar = ' ';
            switch (digit)
            {
                case "Back": keychar = '\x8'; break;
                case "CE": be.EditValue = null; break;
                default: keychar = digit[0]; break;
            }
            KeyPressEventArgs keyarg = new KeyPressEventArgs(keychar);
            be.Focus();
            be.SendKey(be, keyarg);
            #endregion
        }
    }
}
