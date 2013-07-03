using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISM.Template
{
    public partial class FormDictProgress : Form
    {
        private string _title;
        private string _taskname;

        public FormDictProgress()
        {
            InitializeComponent();

            this.TopMost = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        public void SetProgress(string title, string taskname)
        {
            _title = title;
            _taskname = taskname;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Close form when form opened and close is called
            if (FormUtility._lck_progress_open == 1 && FormUtility._lck_progress_close == 1)
            {
                FormUtility._lck_progress_open = 0;
                FormUtility._lck_progress_close = 0;

                this.Close();
                return;
            }
            #endregion

            lblTitle.Text = _title;
            lblProgress.Text = _taskname;
            this.Refresh();
            Application.DoEvents();
        }

        private void FormDictProgress_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 100;
            timer1.Start();
        }
    }

}
