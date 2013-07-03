using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HeavenPro.Image
{
    public partial class FormZoom : DevExpress.XtraEditors.XtraForm
    {
        #region Private Variables

        int width, height, fwidth, fheight;

        #endregion

        #region Properties

        private ISM.Template.Resource _resource = null;
        public ISM.Template.Resource Resource
        {
            get { return _resource; }
            set
            {
                _resource = value;
                if (_resource != null)
                {
                    btnRotLeft.Image = _resource.GetImage("image_rotateright");
                    btnRotRight.Image = _resource.GetImage("image_rotateleft");
                }
            }
        }

        #endregion

        #region Constractor

        public FormZoom()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        void ImageCenter() 
        {
            int x, y;
            x = ZoomHelp.Width / 2-zoomPicture.Width/2;
            y = ZoomHelp.Height / 2 - zoomPicture.Height / 2;
            zoomPicture.Location = new Point(x,y);
        }

        #endregion

        #region Control Events

        private void FormZoom_Load(object sender, EventArgs e)
        {
            ImageCenter();
            width = zoomPicture.Width;
            height = zoomPicture.Height;
            fwidth = this.Width;
            fheight = this.Height;
        }
        private void FormZoom_Resize(object sender, EventArgs e)
        {
            ImageCenter();
        }

        private void zoomTrack_EditValueChanged(object sender, EventArgs e)
        {
            ZoomHelp.VerticalScroll.Value = 0;
            ZoomHelp.HorizontalScroll.Value = 0;
            double n, p;
            n = zoomTrack.Value;

            if (n > 25)
            {
                p = (n - 25) * 0.1;
                ImageCenter();
                zoomPicture.Width = width + Convert.ToInt32(width * p);
                zoomPicture.Height = height + Convert.ToInt32(height * p);
                ImageCenter();
            }
            if (n < 25)
            {
                p = (25 - n) * 0.05;
                ImageCenter();
                zoomPicture.Width = width - Convert.ToInt32(width * p);
                zoomPicture.Height = height - Convert.ToInt32(height * p);
                ImageCenter();
            }
            if (n == 25)
            {
                ImageCenter();
                zoomPicture.Width = width;
                zoomPicture.Height = height;
                ImageCenter();
            }
        }
        private void zoomTrack_ValueChanged(object sender, EventArgs e)
        {
            ImageCenter();
        }
        private void RotLeft_Click(object sender, EventArgs e)
        {
            if (zoomPicture.Image != null)
            {
                zoomPicture.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                zoomPicture.Refresh();
            }
            else 
            {
                MessageBox.Show("Зураг оруулаагүй  байна .");
            }
        }
        private void RotRight_Click(object sender, EventArgs e)
        {
            if (zoomPicture.Image != null)
            {
                zoomPicture.Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                zoomPicture.Refresh();
            }
            else
            {
                MessageBox.Show("Зураг оруулаагүй  байна .");
            }
        }
        private void ZoomHelp_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}