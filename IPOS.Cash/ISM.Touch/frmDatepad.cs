using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace ISM.Touch
{
    public partial class frmDatepad : Form
    {
        #region Internal Variables
        internal Control editControl = null;
        internal CalcEdit edit = null;
        #endregion
        #region Internal Functions
        internal int ToInt(object value)
        {
            int ret = 0;
            if (value != null && value != DBNull.Value)
            {
                string s = Convert.ToString(value)+" ";
                s=s.Replace(",","");
                s = s.Replace(". ", "");

                int.TryParse(s, out ret);
            }
            return ret;
        }
        internal void KeyDownProcess(object sender)
        {
            KeyPressEventArgs keyarg;

            LabelControl btn = (LabelControl)sender;
            string cmd = Convert.ToString(btn.Tag);
            string key = btn.Text;
                        
            switch (cmd)
            {
                case "Done":
                    if (this.editControl != null)
                    {
                        if (!string.IsNullOrEmpty(numYear.Text)
                            && !string.IsNullOrEmpty(numMonth.Text)
                            && !string.IsNullOrEmpty(numDay.Text)
                            )
                        {
                            DateTime date = new DateTime(
                                Convert.ToInt32(numYear.EditValue)
                                , Convert.ToInt32(numMonth.EditValue)
                                , Convert.ToInt32(numDay.EditValue));

                            if (this.editControl is DateEdit)
                            {
                                DateEdit tbase = (DateEdit)this.editControl;
                                tbase.EditValue = date;
                            }
                            else if (this.editControl is TextEdit)
                            {
                                TextEdit tbase = (TextEdit)this.editControl;
                                tbase.EditValue = date.ToString("yyyy/MM/dd");
                            }

                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                    }
                    break;
                case "Exit":
                    this.Close();
                    break;
                case "Clear":
                    edit.Text = "";
                    break;
                case "Back":
                    keyarg = new KeyPressEventArgs('\x8');
                    edit.SendKey(edit, keyarg);
                    break;
                default:
                    keyarg = new KeyPressEventArgs(key[0]);
                    edit.SendKey(edit, keyarg);
                    break;
            }
        }
        #endregion
        #region Constructor
        public frmDatepad(Control control)
        {
            InitializeComponent();

            #region Key events

            this.Load += new EventHandler(frmKeyboard_Load);
            this.Deactivate += new EventHandler(frmNumpad_Deactivate);

            numYear.EditValueChanging += new ChangingEventHandler(numYear_EditValueChanging);
            numYear.EditValueChanged += new EventHandler(numYear_EditValueChanged);

            numMonth.EditValueChanging += new ChangingEventHandler(numMonth_EditValueChanging);
            numMonth.EditValueChanged += new EventHandler(numMonth_EditValueChanged);

            numDay.EditValueChanging += new ChangingEventHandler(numDay_EditValueChanging);

            numYear.GotFocus += new EventHandler(numYear_GotFocus);
            numMonth.GotFocus += new EventHandler(numMonth_GotFocus);
            numDay.GotFocus += new EventHandler(numDay_GotFocus);

            this.btnKey0.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey1.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey2.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey3.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey4.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey5.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey6.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey7.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey8.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKey9.MouseDown += new MouseEventHandler(btnKey_MouseDown);

            this.btnKeyBack.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKeyClear.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKeyDot.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKeyComma.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKeyDone.MouseDown += new MouseEventHandler(btnKey_MouseDown);
            this.btnKeyExit.MouseDown += new MouseEventHandler(btnKey_MouseDown);

            #endregion

            this.editControl = control;
            edit = numYear;
        }
        #endregion
        #region Properties
        public DateTime Value
        {
            get
            {
                DateTime date = DateTime.Today;
                if (!string.IsNullOrEmpty(numYear.Text)
                    && !string.IsNullOrEmpty(numMonth.Text)
                    && !string.IsNullOrEmpty(numDay.Text)
                    )
                {
                    date = new DateTime(
                        Convert.ToInt32(numYear.EditValue)
                        , Convert.ToInt32(numMonth.EditValue)
                        , Convert.ToInt32(numDay.EditValue));
                }
                return date;

            }
            set
            {
                numYear.EditValue = value.Year;
                numMonth.EditValue = value.Month;
                numDay.EditValue = value.Day;
            }
        }
        #endregion

        #region Events
        void frmKeyboard_Load(object sender, EventArgs e)
        {
            try
            {
                numYear.Properties.Mask.EditMask = "####";
                numYear.Properties.Mask.UseMaskAsDisplayFormat = true;
                numYear.EnterMoveNextControl = true;

                numMonth.Properties.Mask.EditMask = "##";
                numMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
                numMonth.EnterMoveNextControl = true;

                numDay.Properties.Mask.EditMask = "##";
                numDay.Properties.Mask.UseMaskAsDisplayFormat = true;
                numDay.EnterMoveNextControl = true;

                if (this.editControl == null) return;
                if (!string.IsNullOrEmpty(this.editControl.Text))
                {
                    DateTime date = DateTime.Today;
                    if (this.editControl is DateEdit)
                    {
                        date = (DateTime)((DateEdit)this.editControl).EditValue;
                    }
                    numYear.EditValue = date.Year;
                    numMonth.EditValue = date.Month;
                    numDay.EditValue = date.Day;

                    numYear.Show();
                    numYear.Select();
                }
            }
            catch
            { }
        }
        void frmNumpad_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            { }
        }

        void btnKey_MouseDown(object sender, MouseEventArgs e)
        {
            LabelControl control = (LabelControl)sender;
            control.BackColor = Color.FromKnownColor(KnownColor.Control);
            control.ForeColor = Color.Black;
            control.Refresh();

            KeyDownProcess(sender);
            System.Threading.Thread.Sleep(100);

            control.BackColor = Color.FromKnownColor(KnownColor.ControlDarkDark);
            control.ForeColor = Color.White;
            control.Refresh();
        }

        void numYear_GotFocus(object sender, EventArgs e)
        {
            edit = (CalcEdit)sender;
        }
        void numYear_EditValueChanging(object sender, ChangingEventArgs e)
        {
        }
        void numYear_EditValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(numYear.EditValue);
            /***************************************
             * 4н оронтой болбол шууд дараагийн талбар.
             ***************************************/
            if (value >= 999)
            {
                numMonth.Select();
                numMonth.SelectAll();
            }
        }

        void numMonth_GotFocus(object sender, EventArgs e)
        {
            edit = (CalcEdit)sender;
        }
        void numMonth_EditValueChanging(object sender, ChangingEventArgs e)
        {
            int value = ToInt(e.NewValue);

            /***************************************
             * Сарын утга 12 аас их байх ёсгүй.
             ***************************************/
            if (value > 12) e.Cancel = true;
        }
        void numMonth_EditValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(numMonth.EditValue);
            /***************************************
            * Сарын утга бүрдвэл шууд дараагийн талбар.
            ***************************************/
            if (value > 1 && value <= 12)
            {
                int year = Convert.ToInt32(numYear.EditValue);
                int day = Convert.ToInt32(numDay.EditValue);

                int max = DateTime.DaysInMonth(year, value);
                if (day > max) numDay.EditValue = max;
                numDay.Select();
                numDay.SelectAll();
            }
        }

        void numDay_GotFocus(object sender, EventArgs e)
        {
            edit = (CalcEdit)sender;
        }
        void numDay_EditValueChanging(object sender, ChangingEventArgs e)
        {
            int year = Convert.ToInt32(numYear.EditValue);
            int month = Convert.ToInt32(numMonth.EditValue);
            int value = ToInt(e.NewValue);

            /***************************************
             * Өдрийн утга 31 аас их байх ёсгүй.
             ***************************************/
            int max = DateTime.DaysInMonth(year, month);
            if (value > max) e.Cancel = true;

        }
        #endregion
    }
}
