using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ISM.Template.UserControls
{
    public partial class ucDynamicStepItem : UserControl
    {
        #region Events

        public delegate void delegateEventMouseDoubleClick(object sender, int index, int id);
        public event delegateEventMouseDoubleClick EventMouseDoubleClick;
        public void OnEventMouseDoubleClick()
        {
            if (EventMouseDoubleClick != null)
                EventMouseDoubleClick(this, itemindex, itemid);
        }

        #endregion
        #region User Properties
        int itemindex = 0;
        public int ItemIndex
        {
            get { return itemindex; }
            set { itemindex = value; }
        }
        
        int itemid = 0;
        public int ItemId
        {
            get { return itemid; }
            set { itemid = value; }
        }

        int typecode = 0;
        public int TypeCode
        {
            get { return typecode; }
            set { typecode = value; }
        }

        ulong typeid = 0;
        public ulong TypeId
        {
            get { return typeid; }
            set { typeid = value; }
        }

        int stepid = 0;
        public int StepId
        {
            get { return stepid; }
            set { stepid = value; }
        }

        int stepitemid = 0;
        public int StepItemId
        {
            get { return stepitemid; }
            set { stepitemid = value; }
        }

        int owner = 0;
        public int Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        string ownername = "";
        public string OwnerName
        {
            get { return ownername; }
            set { ownername = value; }
        }

        double performance = 0;
        public double Performance
        {
            get { return performance; }
            set { performance = value; }
        }

        DateTime started;
        public DateTime Started
        {
            get { return started; }
            set { started = value; }
        }
        DateTime finished;
        public DateTime Finished
        {
            get { return finished; }
            set { finished = value; }
        }

        int status = 0;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Title
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }
        public string Description
        {
            get { return lblDescription.Text; }
            set { lblDescription.Text = value; }
        }
        #endregion
        #region Constractor
        public ucDynamicStepItem()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            lblName.MouseDoubleClick += new MouseEventHandler(lblName_MouseDoubleClick);
            lblDescription.MouseDoubleClick += new MouseEventHandler(lblDescription_MouseDoubleClick);
        }
        void lblDescription_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnEventMouseDoubleClick();
        }
        void lblName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnEventMouseDoubleClick();
        }
        #endregion
        #region Control Properties

        int lineWidth = 2;
        [Category("Bar"), Description("Gets and sets the edge lines width.")]
        public int LineWidth
        {
            get { return lineWidth; }
            set { lineWidth = value; }
        }

        Color bodyColor = Color.FromArgb(233, 28, 33);
        [Category("Bar"), Description("Gets and sets edge surface color.")]
        public Color BodyColor
        {
            get { return bodyColor; }
            set { bodyColor = value; }
        }

        int alphaColor = 220;
        [Category("Bar"), Description("Gets and sets alpha color of transparent. It takes a values between 0..255.")]
        public int AlphaColor
        {
            get { return alphaColor; }
            set { alphaColor = value; }
        }

        bool lightGlint = false;
        [Category("Bar"), Description("Gets and sets surface glint of light.")]
        public bool LightGlint
        {
            get { return lightGlint; }
            set { lightGlint = value; }
        }

        int thickSize = 10;
        [Category("Bar"), Description("Thickness size of the trangle edge.")]
        public int ThickSize
        {
            get { return thickSize; }
            set { thickSize = value; }
        }

        #endregion
        #region Control Functions

        public static Color Lightness(Color color, float factor)
        {
            if (factor == 0) return color;
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;
            if (factor < 0)
            {
                factor = 1 + factor;
                red *= factor;
                green *= factor;
                blue *= factor;
            }
            else
            {
                red = (255 - red) * factor + red;
                green = (255 - green) * factor + green;
                blue = (255 - blue) * factor + blue;
            }
            return System.Drawing.Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
        private static Brush BrushCylinderSurface(Color color, Rectangle rect, bool glint)
        {
            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Colors = new Color[] { 
                Lightness(color, -0.3F), 
                color,
                (glint ? Lightness(color, 0.23F) : color),
                (glint ? Lightness(color, 0.23F) : color),
                color,
                color,
                Lightness(color, -0.1F),
                Lightness(color, -0.3F)
            };
            colorBlend.Positions = new float[] { 0F, 0.18F, 0.28F, 0.36F, 0.49F, 0.65F, 0.89F, 1F };
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.White, LinearGradientMode.Vertical);
            brush.InterpolationColors = colorBlend;
            return brush;
        }
        private static void DrawTrendBar(Control ctl, Graphics graph, Color color, int alpha, int linewidth, int x, int y, int w, int h, int z, bool glint = false)
        {
            if (z < 0) z = 0;
            if (z > w) z = w;

            Color c = Color.FromArgb(alpha, color.R, color.G, color.B);
            Pen p = new Pen(Lightness(c, -0.4F), linewidth);

            #region Draw Body

            PointF[] trendbar = new PointF[]{ 
                new PointF(x,y)
                ,new PointF(x+w-z,y)
                ,new PointF(x+w,y+h/2)
                ,new PointF(x+w-z,y+h)
                ,new PointF(x,y+h)
                ,new PointF(x,y)
            };

            Rectangle r = new Rectangle(x, y, w, h);
            Brush b = BrushCylinderSurface(c, r, glint);
            graph.FillPolygon(b, trendbar);
            graph.DrawPolygon(p, trendbar);
            p.Dispose();
            b.Dispose();

            #endregion
        }

        #endregion
        #region Override Events

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int x = this.ClientRectangle.X + lineWidth / 2;
            int y = this.ClientRectangle.Y + lineWidth / 2;
            int w = this.ClientRectangle.Width - lineWidth;
            int h = this.ClientRectangle.Height - lineWidth;

            DrawTrendBar(this, e.Graphics, bodyColor, alphaColor, lineWidth
                , x
                , y
                , w
                , h
                , thickSize
                , lightGlint);
        }

        #endregion
    }
}
