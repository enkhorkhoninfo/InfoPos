using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ISM.Touch
{
    public partial class TouchButtonGroup : UserControl
    {
        #region Static
        static string _workingfolder = "";
        #endregion
        #region Events

        public delegate void delegateKeyDown(Control sender, MouseEventArgs e, TouchLinkItem item, ref bool cancel);
        public event delegateKeyDown EventKeyDown;

        #endregion
        #region Internal Fields

        Hashtable _links = null;
        string _currentkey = "ROOT";
        ArrayList _path = new ArrayList();

        private int cols = 5;
        private int rows = 2;
        private int space = 4;

        #endregion
        #region Properties

        private Form parentMDI;
        public Form ParentMDI
        {
            get { return parentMDI; }
            set { parentMDI = value; }
        }

        private Color buttonForeColor = System.Drawing.SystemColors.ButtonFace;
        public Color BurronForeColor
        {
            get { return buttonForeColor; }
            set { buttonForeColor = value; }
        }

        private Color buttonBackColor = System.Drawing.SystemColors.ControlDarkDark;
        public Color BurronBackColor
        {
            get { return buttonBackColor; }
            set { buttonBackColor = value; }
        }

        private Font buttonTrackFont = new Font("Tahoma", 10.0f);
        public Font ButtonTrackFont
        {
            get { return buttonTrackFont; }
            set
            {
                if (value != null)
                {
                    labelControl1.Font = value;
                    buttonTrackFont = value;
                }
            }
        }

        private int buttonTrackHeigth = 26;
        public int ButtonTrackHeigth
        {
            get { return buttonTrackHeigth; }
            set
            {
                flowPanel.Height = value;
                buttonTrackHeigth = value;
                Arrange();
            }
        }

        private string buttonTrackText = "";
        public string ButtonTrackText
        {
            get { return buttonTrackText; }
            set
            {
                labelControl1.Text = value;
                buttonTrackText = value;
            }
        }

        private bool buttonTrackVisible = true;
        public bool ButtonTrackVisible
        {
            get { return buttonTrackVisible; }
            set
            {
                flowPanel.Visible = value;
                buttonTrackVisible = value;
                Arrange();
            }
        }
        #endregion
        #region User Functions

        public void RemoveAll()
        {
            foreach (Control control in this.Controls)
            {
                if (control is LabelControl)
                {
                    LabelControl btnKey = (LabelControl)control;
                    if (btnKey.Name.StartsWith("btnR"))
                    {
                        this.Controls.Remove(control);
                        btnKey.Dispose();
                    }
                }
            }
        }
        public void Init(int max_row, int max_col, int margin)
        {
            cols = max_col;
            rows = max_row;
            space = margin;

            this.SuspendLayout();
            this.Clear();

            int cw = this.Width - (cols + 1) * space;
            int ch = Math.Abs(this.Height - (buttonTrackVisible ? flowPanel.Height : 0) - (rows + 1) * space);

            int bw = cw / cols;
            int bh = ch / rows;

            for (int r = 1; r <= rows; r++)
                for (int c = 1; c <= cols; c++)
                {
                    #region Calculate position of control
                    int x = c * space + (c - 1) * bw;
                    int y = r * space + (r - 1) * bh + (buttonTrackVisible ? flowPanel.Height : 0);
                    #endregion
                    #region Create and add key control
                    LabelControl btnKey = new LabelControl();
                    btnKey.Appearance.BackColor = buttonBackColor;
                    btnKey.Appearance.Font = this.Font; // new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnKey.Appearance.ForeColor = buttonForeColor;
                    
                    btnKey.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    btnKey.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
                    btnKey.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                    btnKey.Appearance.ImageAlign = ContentAlignment.TopCenter;

                    btnKey.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                    btnKey.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;

                    btnKey.Name = string.Format("btnR{0}C{1}", r, c);
                    btnKey.Location = new Point(x, y);
                    btnKey.Width = bw;
                    btnKey.Height = bh;
                    btnKey.Tag = new int[] { r, c };

                    btnKey.MouseDown -= new MouseEventHandler(btnKey_MouseDown);
                    btnKey.MouseDown += new MouseEventHandler(btnKey_MouseDown);

                    this.Controls.Add(btnKey);
                    #endregion
                }

            Arrange();

            this.ResumeLayout(false);
        }
        public void Arrange()
        {
            if (this.Controls.Count <= 0) return;
            
            this.SuspendLayout();
            
            int cw = this.Width - (cols + 1) * space;
            int ch = Math.Abs(this.Height - (buttonTrackVisible ? flowPanel.Height : 0) - (rows + 1) * space);

            int bw = cw / cols;
            int bh = ch / rows;

            for (int r = 1; r <= rows; r++)
                for (int c = 1; c <= cols; c++)
                {
                    int x = c * space + (c - 1) * bw;
                    int y = r * space + (r - 1) * bh + (buttonTrackVisible ? flowPanel.Height : 0);
                    string key = string.Format("btnR{0}C{1}", r, c);
                    LabelControl btnKey = (LabelControl)this.Controls[key];
                    if (btnKey != null)
                    {
                        btnKey.Location = new Point(x, y);
                        btnKey.Width = bw;
                        btnKey.Height = bh;
                    }
                }

            this.ResumeLayout();
        }
        public void Clear()
        {
            for (int r = 1; r <= rows; r++)
                for (int c = 1; c <= cols; c++)
                {
                    string key = string.Format("btnR{0}C{1}", r, c);
                    LabelControl btnKey = (LabelControl)this.Controls[key];
                    if (btnKey != null)
                    {
                        btnKey.Text = "";
                        btnKey.Appearance.Image = null;
                    }
                }
        }

        internal TouchLinkItem Get(int row, int col)
        {
            TouchLinkItem ret = null;
            foreach (TouchLinkItem item in _links.Values)
            {
                if (item != null && item.parentkey == _currentkey && item.row == row && item.col == col)
                {
                    ret = item;
                    break;
                }
            }
            return ret;
        }
        public bool Add(string parentkey, string key, int row, int col, string text, string desc = null, Bitmap bitmap = null, string dll = null, string cls = null)
        {
            if (_links.ContainsKey(key)) return false;

            TouchLinkItem link = new TouchLinkItem();
            link.parentkey = (string.IsNullOrEmpty(parentkey) ? "ROOT" : parentkey);
            link.key = key;
            link.row = row;
            link.col = col;
            link.text = text;
            link.description = desc;
            link.bitmap = bitmap;
            link.dll = dll;
            link.cls = cls;
            _links[key] = link;

            return true;
        }
        public bool Set(int row, int col, string text, Bitmap bitmap)
        {
            bool success = false;
            string key = string.Format("btnR{0}C{1}", row, col);
            LabelControl btnKey = (LabelControl)this.Controls[key];
            if (btnKey != null)
            {
                btnKey.Text = text;
                Image oldimage = btnKey.Appearance.Image;
                btnKey.Appearance.Image = bitmap;

                success = true;
                //oldimage.Dispose();
                //oldimage = null;
            }
            return success;
        }
        public bool Remove(string key)
        {
            bool success = false;
            TouchLinkItem item = (TouchLinkItem)_links[key];
            if (item != null)
            {
                _links.Remove(key);
                if (item.bitmap != null)
                {
                    item.bitmap.Dispose();
                }
                success = true;
            }
            return success;
        }
        internal List<TouchLinkItem> GetChildren(string parentkey)
        {
            List<TouchLinkItem> list = new List<TouchLinkItem>();

            foreach (TouchLinkItem item in _links.Values)
            {
                if (item != null && item.parentkey.ToLower() == parentkey.ToLower())
                {
                    list.Add(item);
                }
            }

            return list;
        }

        #endregion
        #region Internal Functions

        internal ArrayList GetPath(string key)
        {
            ArrayList list = new ArrayList();
            TouchLinkItem item = (TouchLinkItem)_links[key];
            while (item != null)
            {
                list.Insert(0, new string[] { item.parentkey, item.text });
                item = (TouchLinkItem)_links[item.parentkey];
            }
            return list;
        }
        internal bool ButtonsDrawPath(string key)
        {
            bool success = false;
            int i = 0;

            ArrayList list = GetPath(key);
            if (list.Count > 0)
            {
                for (i = 0; i < list.Count; i++)
                {
                    string[] item = (string[])list[i];
                    string linklabel_key = string.Format("lnk{0}", i);
                    #region Find link label by index
                    LabelControl linklabel = (LabelControl)flowPanel.Controls[linklabel_key];
                    #endregion
                    #region Create link label control when not found
                    if (linklabel == null)
                    {
                        linklabel = new LabelControl();
                        linklabel.Name = string.Format("lnk{0}", i);
                        linklabel.AutoSize = true;
                        linklabel.LineLocation = LineLocation.Right;
                        linklabel.LineOrientation = LabelLineOrientation.Vertical;
                        linklabel.LineVisible = true;
                        linklabel.Font = buttonTrackFont;

                        linklabel.Click += new EventHandler(linklabel_Click);
                        flowPanel.Controls.Add(linklabel);
                    }
                    #endregion
                    linklabel.Text = string.Format("{0}", item[1]);
                    linklabel.Tag = item[0];
                }
            }
            for (i = list.Count; i < flowPanel.Controls.Count; i++)
            {
                flowPanel.Controls.RemoveByKey(string.Format("lnk{0}", i));
            }

            return success;
        }

        void linklabel_Click(object sender, EventArgs e)
        {
            LabelControl linklabel = (LabelControl)sender;
            if (linklabel.Tag != null)
            {
                ButtonsDrawChildren(Convert.ToString(linklabel.Tag));
            }
        }
        public bool ButtonsDrawChildren(string parentkey)
        {
            bool success = false;
            if (!string.IsNullOrEmpty(parentkey))
            {
                List<TouchLinkItem> list = GetChildren(parentkey);
                if (list.Count > 0)
                {
                    Clear();
                    foreach (TouchLinkItem tmp in list)
                    {
                        Set(tmp.row, tmp.col, tmp.text, tmp.bitmap);
                    }
                    _currentkey = parentkey;

                    ButtonsDrawPath(parentkey);

                    success = true;
                }
            }
            return success;
        }
        internal void ButtonsCalc(int row, int col)
        {
            TouchLinkItem item = Get(row, col);
            if (item == null) return;

            bool success = ButtonsDrawChildren(item.key);
            if (success) _currentkey = item.key;
        }
        internal object CreateInstance(string dll, string cls, out int result)
        {
            Assembly asm = null;
            Type type = null;
            object obj = null;
            int ret = 0;
            try
            {
                #region Load DLL file
                try
                {
                    asm = Assembly.LoadFrom(Path.Combine(_workingfolder, dll));
                }
                catch
                {
                    ret = 1;
                    goto OnExit;
                }
                #endregion

                #region Create class instance
                try
                {
                    type = asm.GetType(cls, true, true);
                    obj = Activator.CreateInstance(type);
                }
                catch
                {
                    ret = 2;
                    goto OnExit;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ret = 4;
                //throw ex;
            }
        OnExit:
            result = ret;
            return obj;
        }

        #endregion
        #region Constructor
        static TouchButtonGroup()
        {
            string s = Assembly.GetExecutingAssembly().Location;
            int i = s.LastIndexOf(@"\");
            if (i > 0) _workingfolder = s.Substring(0, i);
        }
        public TouchButtonGroup()
        {
            InitializeComponent();
            this.Resize += new EventHandler(ucButtonGroup_Resize);
            this.FontChanged += new EventHandler(TouchButtonGroup_FontChanged);
            _links = new Hashtable();
        }

        ~TouchButtonGroup()
        {
        }
        #endregion
        #region Control Events
        private void ucButtonGroup_Load(object sender, EventArgs e)
        {
            ButtonsDrawChildren("ROOT");
        }
        private void ucButtonGroup_Resize(object sender, EventArgs e)
        {
            flowPanel.Width = this.Width;
            Arrange();
        }
        void TouchButtonGroup_FontChanged(object sender, EventArgs e)
        {
            //labelControl1.Font = this.Font;
        }
        private void btnKey_MouseDown(object sender, MouseEventArgs e)
        {
            #region Button pressed state
            LabelControl control = (LabelControl)sender;
            control.BackColor = this.buttonForeColor; //Color.FromKnownColor(KnownColor.Control);
            control.ForeColor = this.buttonBackColor; //Color.Black;
            control.Refresh();
            #endregion

            #region Find link item

            int[] values = (int[])control.Tag;
            int row = values[0];
            int col = values[1];

            TouchLinkItem item = Get(row, col);

            #endregion

            #region Raise an event
            bool cancel = false;
            if (EventKeyDown != null)
            {
                try
                {
                    EventKeyDown(control, e, item, ref cancel);
                }
                catch
                { }
            }
            //else
            {
                System.Threading.Thread.Sleep(100);
            }
            #endregion

            #region Trip to next button

            if (!cancel)
            {
                ButtonsCalc(row, col);
            }
            else
            {
                ButtonsDrawChildren("ROOT");
            }

            #endregion

            #region Button unpressed state

            control.BackColor = this.buttonBackColor; // Color.FromKnownColor(KnownColor.ControlDarkDark);
            control.ForeColor = this.buttonForeColor; // Color.White;
            control.Refresh();

            #endregion
        }
        #endregion
    }

    public class TouchLinkItem
    {
        #region Properties

        public object Tag;

        protected internal string parentkey;
        public string parentKey
        {
            get { return parentkey; }
        }

        protected internal string key;
        public string Key
        {
            get { return key; }
        }

        protected internal int row;
        public int Row
        {
            get { return row; }
        }

        protected internal int col;
        public int Col
        {
            get { return col; }
        }

        protected internal string text;
        public string Text
        {
            get { return text; }
        }

        protected internal string description;
        public string Description
        {
            get { return description; }
        }

        protected internal Bitmap bitmap;
        public Bitmap Bitmap
        {
            get { return bitmap; }
        }

        protected internal string dll;
        public string Dll
        {
            get { return dll; }
        }

        protected internal string cls;
        public string Cls
        {
            get { return cls; }
        }
        protected internal int isclose = 0;
        public int IsClose
        {
            get { return isclose; }
            set { isclose = value; }
        }

        #endregion
    }
}
