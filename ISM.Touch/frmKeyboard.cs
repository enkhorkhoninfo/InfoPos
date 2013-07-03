using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ISM.Touch
{
    public partial class frmKeyboard : Form
    {
        internal string[] keyEng = new string[] { 
        "1","2","3","4","5","6","7","8","9","0","-","="
        ,"Q","W","E","R","T","Y","U","I","O","P","[","]"
        ,"A","S","D","F","G","H","J","K","L",";","'"
        ,"Z","X","C","V","B","N","M",",",".","/"
        ,"@","%"," "
        };
        internal string[] keyCyr = new string[] { 
        "1","2","3","4","5","6","7","8","9","0","Е","Щ"
        ,"Ф","Ц","У","Ж","Э","Н","Г","Ш","Ү","З","К","Ъ"
        ,"Й","Ы","Б","Ө","А","Х","Р","О","Л","Д","П"
        ,"Я","Ч","Ё","С","М","И","Т","Ь","В","Ю"
        ,"@","%"," "
        };

        internal Control editControl = null;

        #region Properties
        public string Value
        {
            get { return txtView.Text; }
            set { txtView.EditValue = value; }
        }
        #endregion


        internal void KeyDownProcess(object sender)
        {
            Control btn = (Control)sender;
            string cmd = Convert.ToString(btn.Tag);
            string key = btn.Text;

            switch (cmd)
            {
                case "Lang":
                    LangSwitch();
                    break;
                case "Done":
                    if (this.editControl != null)
                    {
                        this.editControl.Text = txtView.Text;
                        if (this.editControl is TextBoxBase)
                        {
                            TextBoxBase tbase = (TextBoxBase)this.editControl;
                            tbase.Select(tbase.Text.Length, 0);
                        }
                        else if (this.editControl is TextEdit)
                        {
                            TextEdit tbase = (TextEdit)this.editControl;
                            tbase.Select(tbase.Text.Length, 0);
                        }
                    }
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    break;
                case "Exit":
                    this.Close();
                    break;
                case "Back":
                    if (txtView.SelectionLength > 0)
                    {
                        txtView.SelectedText = "";
                    }
                    else if (txtView.SelectionStart > 0)
                    {
                        txtView.SelectionStart--;
                        txtView.SelectionLength = 1;
                        txtView.SelectedText = "";
                    }
                    break;
                case "Space":
                    key = " ";
                    txtView.SelectedText = key;
                    break;
                default:

                    if (this.editControl != null)
                    {
                        this.editControl.Text = txtView.Text;
                        if (this.editControl is TextBoxBase)
                        {
                            TextBoxBase tbase = (TextBoxBase)this.editControl;
                            tbase.SelectedText = key;
                        }
                        else if (this.editControl is TextEdit)
                        {
                            TextEdit tbase = (TextEdit)this.editControl;
                            tbase.SelectedText = key;
                        }
                    }


                    
                    txtView.SelectedText = key;
                    break;
            }
            txtView.Focus();
        }
        internal void LangChange(bool eng)
        {
            
            this.SuspendLayout();
            panelKey.SuspendLayout();
            
            for (int i = 1; i <= 47; i++)
            {
                string name = string.Format("btnKey{0}",i);
                panelKey.Controls[name].Text = eng ? keyEng[i-1] : keyCyr[i-1];
            }
            btnKeyLang.Text = eng ? "MN" : "EN";

            panelKey.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        internal void LangSwitch()
        {
            LangChange(btnKeyLang.Text != "MN");
        }

        public frmKeyboard(Control control)
        {
            InitializeComponent();

            #region Key events

            this.Load += new EventHandler(frmKeyboard_Load);
            this.Deactivate += new EventHandler(frmKeyboard_Deactivate);

            this.btnKeySpace.Click += new EventHandler(btnKey_Click);
            this.btnKeyBack.Click += new EventHandler(btnKey_Click);
            this.btnKeyLang.Click += new EventHandler(btnKey_Click);
            this.btnKeyDone.Click += new EventHandler(btnKey_Click);
            this.btnKeyExit.Click += new EventHandler(btnKey_Click);

            for (int i = 1; i <= 47; i++)
            {
                string name = string.Format("btnKey{0}",i);
                panelKey.Controls[name].Click += new EventHandler(btnKey_Click);
            }

            #endregion
            
            this.editControl = control;
            LangChange(true);
        }

        void frmKeyboard_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            { }
        }
        void frmKeyboard_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.editControl == null) return;
                if (!string.IsNullOrEmpty(this.editControl.Text))
                {
                    //txtView.Focus();
                    txtView.SelectedText = this.editControl.Text;
                    txtView.Select(txtView.Text.Length, 0);
                }
            }
            catch
            { }
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            KeyDownProcess(sender);
        }

        private void frmKeyboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.editControl != null)
                {
                    this.editControl.Text = txtView.Text;
                    if (this.editControl is TextBoxBase)
                    {
                        TextBoxBase tbase = (TextBoxBase)this.editControl;
                        tbase.Select(tbase.Text.Length, 0);
                    }
                    else if (this.editControl is TextEdit)
                    {
                        TextEdit tbase = (TextEdit)this.editControl;
                        tbase.Select(tbase.Text.Length, 0);
                    }
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
