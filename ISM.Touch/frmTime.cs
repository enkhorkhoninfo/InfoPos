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
    public partial class frmTime : Form
    {
        #region Internal Variables
        internal Control editControl = null;
        internal CalcEdit edit = null;
        DateTime _time;
        #endregion
        #region Internal Functions
        internal int ToInt(object value)
        {
            int ret = 0;
            if (value != null && value != DBNull.Value)
            {
                string s = Convert.ToString(value) + " ";
                s = s.Replace(",", "");
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
                        if (!string.IsNullOrEmpty(numHour.Text)
                            && !string.IsNullOrEmpty(numMinute.Text)
                            && !string.IsNullOrEmpty(numSecond.Text)
                            )
                        {
                            if (Convert.ToInt32(numHour.EditValue) == 24)
                            {
                                numHour.EditValue = 0;
                            }
                            DateTime date = new DateTime(1, 1, 1,
                                Convert.ToInt32(numHour.EditValue)
                                , Convert.ToInt32(numMinute.EditValue)
                                , Convert.ToInt32(numSecond.EditValue));
                            
                            if (this.editControl is DateEdit)
                            {
                                DateEdit tbase = (DateEdit)this.editControl;
                                tbase.EditValue = date;
                            }
                            else if (this.editControl is TextEdit)
                            {
                                TextEdit tbase = (TextEdit)this.editControl;
                                tbase.EditValue = date.ToString("H:mm:ss");
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
        public frmTime(Control control, DateTime time)
        {
            InitializeComponent();

            #region Key events

            this.Load += new EventHandler(frmKeyboard_Load);
            this.Deactivate += new EventHandler(frmNumpad_Deactivate);
            _time = time;
            numHour.EditValueChanging += new ChangingEventHandler(numYear_EditValueChanging);
            numHour.EditValueChanged += new EventHandler(numYear_EditValueChanged);

            numMinute.EditValueChanging += new ChangingEventHandler(numMonth_EditValueChanging);
            numMinute.EditValueChanged += new EventHandler(numMonth_EditValueChanged);

            numSecond.EditValueChanging += new ChangingEventHandler(numSecond_EditValueChanging);

            numHour.GotFocus += new EventHandler(numYear_GotFocus);
            numMinute.GotFocus += new EventHandler(numMonth_GotFocus);
            numSecond.GotFocus += new EventHandler(numDay_GotFocus);

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
            edit = numHour;
        }
        #endregion
        #region Properties
        public DateTime Value
        {
            get
            {
                DateTime date = DateTime.Now;
                if (!string.IsNullOrEmpty(numHour.Text)
                    && !string.IsNullOrEmpty(numMinute.Text)
                    && !string.IsNullOrEmpty(numSecond.Text)
                    )
                {
                    date = new DateTime(
                        Convert.ToInt32(numHour.EditValue)
                        , Convert.ToInt32(numMinute.EditValue)
                        , Convert.ToInt32(numSecond.EditValue));
                }
                return date;

            }
            set
            {
                numHour.EditValue = value.Hour;
                numMinute.EditValue = value.Minute;
                numSecond.EditValue = value.Second;
            }
        }
        #endregion

        #region Events
        void frmKeyboard_Load(object sender, EventArgs e)
        {
            try
            {
                //numHour.Properties.Mask.EditMask = "###";
                //numHour.Properties.Mask.UseMaskAsDisplayFormat = true;
                numHour.EnterMoveNextControl = true;

                //numMinute.Properties.Mask.EditMask = "###";
                //numMinute.Properties.Mask.UseMaskAsDisplayFormat = true;
                numMinute.EnterMoveNextControl = true;

                //numSecond.Properties.Mask.EditMask = "###";
                //numSecond.Properties.Mask.UseMaskAsDisplayFormat = true;
                numSecond.EnterMoveNextControl = true;

                if (this.editControl == null) return;
                if (!string.IsNullOrEmpty(this.editControl.Text))
                {
                    numHour.EditValue = _time.Hour;
                    numMinute.EditValue = _time.Minute.ToString();
                    numSecond.EditValue = _time.Second.ToString();

                    numHour.Show();
                    numHour.Select();
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
            int value = Convert.ToInt32(numHour.EditValue);
            /***************************************
             * 24 цагаас эх болцол дараагын талбаруу шилжинэ
             ***************************************/
            if (value >= 24)
            {
                numHour.EditValue = 24;
                numMinute.Select();
                numMinute.SelectAll();
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

            if (value == 6) e.Cancel = true;
            if (value == 7) e.Cancel = true;
            if (value == 8) e.Cancel = true;
            if (value == 9) e.Cancel = true;
            if (value.ToString().Length > 2)
            {
                numMinute.EditValue = 59;
                numSecond.Select();
                numSecond.SelectAll();
            }
        }
        void numMonth_EditValueChanged(object sender, EventArgs e)
        {
            /***************************************
            * Минут утга бүрдвэл шууд дараагийн талбар.
            ***************************************/
            if (Convert.ToInt32(numMinute.EditValue) >= 59)
            {
                numMinute.EditValue = 59;
                numSecond.Select();
                numSecond.SelectAll();
            }
        }

        void numDay_GotFocus(object sender, EventArgs e)
        {
            edit = (CalcEdit)sender;
        }
        void numSecond_EditValueChanging(object sender, ChangingEventArgs e)
        {
            int value = ToInt(e.NewValue);
            /***************************************
             * Секундын утга 59 өөс их байх ёсгүй.
             ***************************************/

            if (value == 6) e.Cancel = true;
            if (value == 7) e.Cancel = true;
            if (value == 8) e.Cancel = true;
            if (value == 9) e.Cancel = true;
            if (value.ToString().Length > 2 || value > 59)
            {
                numSecond.EditValue = 59;
                numHour.Select();
                numHour.SelectAll();
            }
        }
        private void numSecond_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numSecond.EditValue) > 59)
            {
                numSecond.EditValue = 59;
                numHour.Select();
                numHour.SelectAll();
            }
        }
        #endregion

        private void numHour_Click(object sender, EventArgs e)
        {
            numHour.SelectAll();
        }

        private void numMinute_Click(object sender, EventArgs e)
        {
            numMinute.SelectAll();
        }

        private void numSecond_Click(object sender, EventArgs e)
        {
            numSecond.SelectAll();
        }
    }
}
        
