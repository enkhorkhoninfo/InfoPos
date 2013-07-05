using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EServ.Shared;
using InfoPos.Core;
using ISM.Template;

namespace HeavenPro.Admin
{
    public class Main
    {
        private Core _core;
     
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
            HeavenPro.UserProp frm = new HeavenPro.UserProp(_core, userid);
            frm.MdiParent = _core.MainForm;
            frm.Show();

        }
        public void CallPassOptions(object[] param)
        {
            _core = (Core.Core)param[0];
            HeavenPro.Admin.PassOption frm = new HeavenPro.Admin.PassOption(_core);
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
                HeavenPro.Admin.UserGroupProp frm = new HeavenPro.Admin.UserGroupProp(_core, GroupID);
                frm.MdiParent = _core.MainForm;
                frm.Show();
        }        
        public void CallLogDetail(object[] param)
        {
            _core = (Core.Core)param[0];
            object[] obj = new object[1];
            long pLogId = 1;
            try
            {
                pLogId = Static.ToLong(param[1]);
                obj[0] = (DataRow)param[2];
            }
            catch { pLogId = 1; obj = null; }
            HeavenPro.Admin.LogDetail frm = new HeavenPro.Admin.LogDetail(_core, pLogId,obj);

            frm.MdiParent = _core.MainForm;
            frm.Show();
         
        }
    }
}