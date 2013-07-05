using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace InfoPos.Issue
{
    public class Main
    {
        private Core.Core _core;
        public Main()
        {
        }

        public void CallIssue(object[] param)
        {
            _core = (Core.Core)param[0];
            long customerno=Convert.ToInt64(param[1]);
            DataRow dr = (DataRow)param[2];
            InfoPos.Issue.FormMain frm = new InfoPos.Issue.FormMain(_core, customerno,dr);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallIssueTrack(object[] param)
        {
            _core = (Core.Core)param[0];
            long projectid = Convert.ToInt64(param[1]);
            DataRow dr = (DataRow)param[2];
            InfoPos.Issue.FormIssueTracking frm = new InfoPos.Issue.FormIssueTracking(_core, projectid, dr);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
    }
}
