using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace ISM.Touch
{
    public partial class frmNumpad : Form
    {
        #region Internal Variables
        internal Control editControl = null;
        internal CalcEdit edit = null;
        #endregion
        #region Internal Functions
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
                        if (!string.IsNullOrEmpty(numView.Text))
                        {
                            this.editControl.Text = Convert.ToString(numView.EditValue);
                            if (this.editControl is CalcEdit)
                            {
                                CalcEdit tbase = (CalcEdit)this.editControl;
                                tbase.EditValue = numView.EditValue;
                            }
                            else if (this.editControl is TextEdit)
                            {
                                TextEdit tbase = (TextEdit)this.editControl;
                                tbase.Text = numView.Text;
                            }
                        }
                    }
                    this.Value = numView.EditValue == null ? 0 : Convert.ToDecimal(numView.EditValue);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();

                    break;
                case "Exit":
                    this.Close();
                    break;
                case "Clear":
                    numView.Text = "";
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
        public frmNumpad(Control control)
        {
            InitializeComponent();

            #region Key events

            this.Load += new EventHandler(frmKeyboard_Load);
            this.Deactivate += new EventHandler(frmNumpad_Deactivate);

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
            edit = numView;
        }
        #endregion
        #region Properties
        public decimal Value
        {
            get { return numView.Value; }
            set { numView.EditValue = value; }
        }
        #endregion
        #region Events
        void frmKeyboard_Load(object sender, EventArgs e)
        {
            try
            {
                numView.Properties.Mask.EditMask = "###,###,###,##0.##";
                numView.Properties.Mask.UseMaskAsDisplayFormat = true;
                numView.EnterMoveNextControl = true;

                if (this.editControl == null) return;
                if (!string.IsNullOrEmpty(this.editControl.Text))
                {
                    decimal value = 0;
                    if (this.editControl is CalcEdit)
                    {
                        value = (Decimal)((CalcEdit)this.editControl).EditValue;
                    }
                    numView.Show();
                    numView.Text = this.editControl.Text;
                    numView.Select();
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
        private void btnKey_MouseDown(object sender, MouseEventArgs e)
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
        #endregion
    }
}
