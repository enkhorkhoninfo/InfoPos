using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EServ.Shared;
using InfoPos.Admin;
using InfoPos.Core;
using ISM.Template;

namespace InfoPos.Admin
{
    public class Main
    {
        private Core.Core _core;
     
        public Main()
        {
        }
        public void CallUserProp(object[] param)
        {
            _core = (Core.Core)param[0];

            int userid = 0;

            try
            {
                userid = Static.ToInt(param[1]);
            }
            catch { userid = 0; }
            InfoPos.UserProp frm = new InfoPos.UserProp(_core, userid);
            frm.MdiParent = _core.MainForm;
            frm.Show();

        }
        public void CallPassOptions(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Admin.PassOption frm = new InfoPos.Admin.PassOption(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallGroupProp(object[] param)
        {
            _core = (Core.Core)param[0];
            long GroupID = 0;

            try
            {
                GroupID = Static.ToInt(param[1]);
            }
            catch { GroupID = 0; }
                InfoPos.Admin.UserGroupProp frm = new InfoPos.Admin.UserGroupProp(_core, GroupID);
                frm.MdiParent = _core.MainForm;
                frm.Show();
        }        
        public void CallLogDetail(object[] param)
        {            

            object[] obj = new object[1];

            long pLogId = Static.ToLong(param[1]);
            
            try
            {
                _core  = (Core.Core)param[0];            
                obj[0] = (DataRow)param[2];
            }

            catch { pLogId = 1; obj = null; }
            InfoPos.Admin.LogDetail frm = new InfoPos.Admin.LogDetail(_core, pLogId,obj);

            frm.MdiParent = _core.MainForm;
            frm.Show();
         
        }
    }
}