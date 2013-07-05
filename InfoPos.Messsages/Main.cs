using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EServ.Shared;

namespace InfoPos.Messages
{
    public class Main
    {
        private Core.Core _core;
        public Main()
        { 
        }
        public void CallSendMessage(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Messages.frmSend frm = new InfoPos.Messages.frmSend(_core, 0, "", 0);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallMessage(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Messages.frmMail frm = new InfoPos.Messages.frmMail(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
    }
}
