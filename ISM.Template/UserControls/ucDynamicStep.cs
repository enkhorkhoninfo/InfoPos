using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;

using EServ.Shared;

namespace ISM.Template.UserControls
{
    public partial class ucDynamicStep : UserControl
    {
        #region Internal Variables
        List<ucDynamicStepItem> items = new List<ucDynamicStepItem>();
        Color _currentcolor = Color.Yellow;
        #endregion
        #region Control Properties

        int margin = 3;
        public int BarMargin
        {
            get { return margin; }
            set { margin = value; }
        }

        int barwidth = 170;
        public int BarWidth
        {
            get { return barwidth; }
            set
            {
                barwidth = value;
                Draw();
            }
        }

        int barheight = 70;
        public int BarHeight
        {
            get { return barheight; }
            set
            {
                barheight = value;
            }
        }

        int barminwidth = 100;
        public int BarMinWidth
        {
            get { return barminwidth; }
            set
            {
                barminwidth = value;
                Draw();
            }
        }

        bool barautosize = false;
        public bool BarAutoSize
        {
            get { return barautosize; }
            set
            {
                barautosize = value;
                Draw();
            }
        }

        int spacesize = 5;
        public int BarSpaceSize
        {
            get { return spacesize; }
            set
            {
                spacesize = value > 20 ? 20 : value;
                Draw();
            }
        }

        bool barsmallsize = false;
        public bool BarSmallSize
        {
            get { return checkEdit1.Checked; }
            set { checkEdit1.Checked = value; }
        }

        #endregion
        #region Business Properties

        private CUser.Remote _remote = null;
        [DefaultValue(null), Browsable(false)]
        public CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }
        private Resource _resource = null;
        [DefaultValue(null), Browsable(false)]
        public Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        private int _typecode = 0;
        [DefaultValue(0), Browsable(true)]
        public int TypeCode
        {
            get { return _typecode; }
            set { _typecode = value; }
        }

        private ulong _typeid = 0;
        [DefaultValue(0), Browsable(true)]
        public ulong TypeId
        {
            get { return _typeid; }
            set { _typeid = value; }
        }

        int _stepid = 0;
        public int StepId
        {
            get { return _stepid; }
            set { _stepid = value; }
        }

        private int _tableprivselect = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivSelect
        {
            get { return _tableprivselect; }
            set { _tableprivselect = value; }
        }

        private int _tableprivupdate = 0;
        [DefaultValue(0), Browsable(true)]
        public int TablePrivUpdate
        {
            get { return _tableprivupdate; }
            set { _tableprivupdate = value; }
        }

        private int _currentworkindex = -1;
        public int CurrentWorkIndex
        {
            get { return _currentworkindex; }
        }
        private int _currentworkid = -1;
        public int CurrentWorkId
        {
            get { return _currentworkid; }
        }

        #endregion
        #region Constractor
        public ucDynamicStep()
        {
            InitializeComponent();
            this.Resize += new EventHandler(ucDynamicStep_Resize);
        }
        #endregion
        #region Control Events
        private void ucDynamicStep_Load(object sender, EventArgs e)
        {
        }
        void ucDynamicStep_Resize(object sender, EventArgs e)
        {
            Draw();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Lighting(null);
        }
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            this.Height = checkEdit1.Checked ? 30 : barheight;
        }

        void si_EventMouseDoubleClick(object sender, int index, int id)
        {
            //MessageBox.Show(string.Format("{0} {1}", index, id));

            if (sender == null) return;
            ucDynamicStepItem si = (ucDynamicStepItem)sender;

            ISM.Template.FormDynamicStep frm = new FormDynamicStep(
                _remote
                ,_resource
                , _tableprivselect
                , _typecode
                , _typeid
                , si.StepId
                , si.StepItemId
                , si.Owner
                , si.OwnerName
                , si.Performance
                , si.Started
                , si.Finished
                , si.Status);
            frm.Text = string.Format("Бүртгэлийн явц");
            DialogResult dlg = frm.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                Read();
            }
        }

        #endregion
        #region Thread Functions
        float _lightingfactor = 0.0F;
        bool _lighting = true;
        private void Lighting(object state)
        {
            if (_currentworkindex < 0 || _currentworkindex >= items.Count) return;
            try
            {
                ucDynamicStepItem si = items[_currentworkindex];
                if (si != null)
                {
                    if (_lighting)
                    {
                        if (_lightingfactor >= 0.5F) _lighting = false;
                        else _lightingfactor += 0.05F;
                    }
                    else
                    {
                        if (_lightingfactor <= -0.4F) _lighting = true;
                        else _lightingfactor -= 0.05F;
                    }
                    Color c = ucDynamicStepItem.Lightness(_currentcolor, _lightingfactor);
                    si.BodyColor = c;
                    si.Refresh();
                }
            }
            catch
            { }
        }
        #endregion
        #region Server Functions
        public Result Read()
        {
            timer1.Enabled = false;
            _currentworkindex = -1;
            _currentworkid = -1;
            _currentcolor = Color.Yellow;

            Result r = null;
            #region Calling to Server
            r = _remote.Connection.Call(_remote.User.UserNo
                , 108, 108301, _tableprivselect
                , new object[] { _typecode, _typeid, _stepid });
            if (r.ResultNo != 0 || r.Data.Tables.Count <= 0) return r;
            #endregion

            DataTable dt = r.Data.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                #region Field Values
                int itemid = ISM.Lib.Static.ToInt(dt.Rows[i]["stepitemid"]);
                string itemname = ISM.Lib.Static.ToStr(dt.Rows[i]["name"]);
                string itemdesc = ISM.Lib.Static.ToStr(dt.Rows[i]["note"]);
                int owner = ISM.Lib.Static.ToInt(dt.Rows[i]["owner"]);
                string userfname = ISM.Lib.Static.ToStr(dt.Rows[i]["userfname"]);
                string userlname = ISM.Lib.Static.ToStr(dt.Rows[i]["userlname"]);
                string position = ISM.Lib.Static.ToStr(dt.Rows[i]["position"]);
                double performance = ISM.Lib.Static.ToDouble(dt.Rows[i]["performance"]);
                DateTime started = ISM.Lib.Static.ToDate(dt.Rows[i]["started"]);
                DateTime finished = ISM.Lib.Static.ToDate(dt.Rows[i]["finished"]);
                int status = ISM.Lib.Static.ToInt(dt.Rows[i]["status"]);
                #endregion
                #region Checking item exists
                ucDynamicStepItem si = null;
                if (items.Count < i + 1)
                {
                    si = Add(itemid, itemname, itemdesc, Color.OrangeRed);
                }
                else
                {
                    si = items[i];
                }
                #endregion
                #region Collecting Informations
                if (!string.IsNullOrEmpty(userlname))
                {
                    userfname = string.Format("{0}.{1}", userlname.Substring(0, 1), userfname);
                }
                if (!string.IsNullOrEmpty(position))
                {
                    position = string.Format("{0}, ", position);
                }
                #endregion
                #region Checking Performance Color

                string desc = "";
                if (performance == 100)
                {
                    si.BodyColor = Color.YellowGreen;
                    desc = string.Format("{0}, {1}{2}%\r\nЭхэлсэн {3}", userfname, position, performance, started.ToString("yyyy.MM.dd"));
                }
                else if (started > DateTime.MinValue)
                {
                    si.BodyColor = Color.Yellow;
                    desc = string.Format("{0}, {1}{2}%\r\nЭхэлсэн {3}", userfname, position, performance, started.ToString("yyyy.MM.dd"));
                    if (_currentworkindex < 0)
                    {
                        _currentworkindex = i;
                        _currentworkid = itemid;
                        _currentcolor = si.BodyColor;
                    }
                }
                else
                {
                    si.BodyColor = Color.OrangeRed;
                    desc = "Ажил эхлээгүй...";
                    if (_currentworkindex < 0)
                    {
                        _currentworkindex = i;
                        _currentworkid = itemid;
                        _currentcolor = si.BodyColor;
                    }
                }

                #endregion
                #region Setting Values to Control

                si.TypeCode = _typecode;
                si.TypeId = _typeid;
                si.StepId = _stepid;
                si.StepItemId = itemid;
                si.Owner = owner;
                si.OwnerName = userfname;
                si.Performance = performance;
                si.Started = started;
                si.Finished = finished;
                si.Status = status;

                si.ItemIndex = i;
                si.Title = string.Format("{0}. {1}", i + 1, itemname);
                si.Description = desc;
                si.lblName.ToolTipTitle = si.Title;
                si.lblName.ToolTip = string.Format("{0}\r\n{1}", itemdesc, desc);
                si.lblName.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
                si.Refresh();
                #endregion
            }

            timer1.Enabled = true;

            return r;
        }
        #endregion
        #region Methods

        private void Draw()
        {
            int c = items.Count;
            if (c > 0)
            {
                this.SuspendLayout();

                int w = (((this.ClientSize.Width - checkEdit1.Width) - margin * 2) - (c - 1) * spacesize) / c;
                if (w < barminwidth) w = barminwidth;
                int h = this.ClientSize.Height > margin * 2 ? this.ClientSize.Height - margin * 2 : this.ClientSize.Height;
                
                for (int i = 0; i < items.Count; i++)
                {
                    ucDynamicStepItem si = items[i];
                    si.Left = margin + i * spacesize + i * w;
                    si.Width = w;
                    si.Top = margin;
                    si.Height = h;
                }

                this.ResumeLayout();
            }
        }
        public ucDynamicStepItem Add(int itemid, string title, string desc, Color barcolor)
        {
            #region Create Instance
            ucDynamicStepItem si = new ucDynamicStepItem();
            si.ItemId = itemid;
            si.Title = title;
            si.Description = desc;
            si.BodyColor = barcolor;
            si.EventMouseDoubleClick += new ucDynamicStepItem.delegateEventMouseDoubleClick(si_EventMouseDoubleClick);
            #endregion

            items.Add(si);
            Draw();

            this.Controls.Add(si);

            return si;
        }

        public void Remove(ucDynamicStepItem item)
        {
            if (item != null)
            {
                item.EventMouseDoubleClick -= new ucDynamicStepItem.delegateEventMouseDoubleClick(si_EventMouseDoubleClick);
                this.Controls.Remove(item);
                items.Remove(item);
                item.Dispose();
            }
        }
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                ucDynamicStepItem si = items[index];
                si.EventMouseDoubleClick -= new ucDynamicStepItem.delegateEventMouseDoubleClick(si_EventMouseDoubleClick);
                this.Controls.Remove(si);
                items.RemoveAt(index);
                si.Dispose();
            }
        }
        #endregion
    }
}
