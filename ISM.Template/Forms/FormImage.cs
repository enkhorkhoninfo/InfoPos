using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WIA;

namespace ISM.Template
{
    public partial class FormImage : DevExpress.XtraEditors.XtraForm
    {
        int width, height, fwidth, fheight;

        #region [ External Constants ]

        private int hWnd;
        const int WM_CAP_START = 1024;
        const int WS_CHILD = 1073741824;
        const int WS_VISIBLE = 268435456;
        const int WM_CAP_DRIVER_CONNECT = (WM_CAP_START + 10);
        const int WM_CAP_EDIT_COPY = (WM_CAP_START + 30);
        const int WM_CAP_SET_SCALE = (WM_CAP_START + 53);
        const int WM_CAP_SET_PREVIEWRATE = (WM_CAP_START + 52);
        const int WM_CAP_SET_PREVIEW = (WM_CAP_START + 50);
        const int SWP_NOMOVE = 2;
        const int SWP_NOZORDER = 4;
        const int HWND_BOTTOM = 1;

        #endregion

        #region [ External Methods ]

        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(
        short wDriverIndex, string lpszName,
        int cbName, string lpszVer, int cbVer);

        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern int capCreateCaptureWindowA(
        string lpszWindowName, int dwStyle, int x, int y,
        int nWidth, short nHeight, int hWnd, int nID);

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SendMessageA")]
        static extern int SendMessage(
        int hwnd, int Msg, int wParam,
        [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetWindowPos")]
        static extern int SetWindowPos(
        int hwnd, int hWndInsertAfter, int x, int y,
        int cx, int cy, int wFlags);

        [System.Runtime.InteropServices.DllImport("user32")]
        static extern bool DestroyWindow(int hndw);

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
                    btnFile.Image = _resource.GetImage("image_folder");
                    btnCam.Image = _resource.GetImage("image_camer");
                    btnScan.Image = _resource.GetImage("image_scan");
                    btnOK.Image = _resource.GetImage("image_ok");
                    btnSave.Image = _resource.GetImage("image_save");
                    btnTake.Image = _resource.GetImage("image_snapshot");

                    btnRotLeft.Image = _resource.GetImage("image_rotateright");
                    btnRotRight.Image = _resource.GetImage("image_rotateleft");
                }
            }
        }

        private System.Drawing.Image _image = null;
        public System.Drawing.Image ImageObject
        {
            get { return _image; }
            set { _image = value; }
        }

        private string _filename = "";
        public string FileName
        {
            get { return _filename; }
        }

        #endregion

        #region Constractor

        public FormImage()
        {
            InitializeComponent();

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            trkZoom.EditValueChanged += new EventHandler(trkZoom_EditValueChanged);
            trkZoom.ValueChanged += new EventHandler(trkZoom_ValueChanged);
        }

        #endregion

        private void StopCamera()
        {
            btnCam.Enabled = true;
            btnTake.Enabled = false;

            try
            {
                if (hWnd != 0)
                {
                    DestroyWindow(hWnd);
                    hWnd = 0;
                }
            }
            catch
            { }
        }

        #region file-с уншина

        private void file(PictureEdit pBox)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "JPEG files (*.jpg)|*.jpg";
            openfile.Filter += "|BITMAP files (*.bmp)|*.bmp";
            openfile.Filter += "|GIF files (*.gif)|*.gif";

            Bitmap BMP;
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                StopCamera();
                string fname = openfile.FileName;
                BMP = new System.Drawing.Bitmap(fname);
                pBox.Image = null;
                pBox.Image = BMP;

                _filename = fname;
            }
        }
        #endregion

        #region camer-с уншина
        private void camer(PictureEdit pBox)
        {
            hWnd = capCreateCaptureWindowA("0", WS_VISIBLE | WS_CHILD, 0, 0, 0, 0, pBox.Handle.ToInt32(), 0);
            if (SendMessage(hWnd, WM_CAP_DRIVER_CONNECT, 0, 0) != 0)
            {
                SendMessage(hWnd, WM_CAP_SET_SCALE, 1, 0);
                SendMessage(hWnd, WM_CAP_SET_PREVIEWRATE, 30, 0);
                SendMessage(hWnd, WM_CAP_SET_PREVIEW, 1, 0);
                SetWindowPos(hWnd, HWND_BOTTOM, 0, 0, pBox.Width, pBox.Height, SWP_NOMOVE | SWP_NOZORDER);

                btnCam.Enabled = false;
                btnTake.Enabled = true;
            }
            else
            {
                StopCamera();
            }
        }
        private bool take(PictureEdit pBox)
        {
            bool success = false;
            try
            {
                IDataObject data;
                System.Drawing.Image bmap;
                SendMessage(hWnd, WM_CAP_EDIT_COPY, 0, 0);

                data = Clipboard.GetDataObject();
                if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                {
                    bmap = ((System.Drawing.Image)(data.GetData(typeof(System.Drawing.Bitmap))));
                    pBox.Image = bmap;
                    _filename = string.Format("Camera_{0}.jpg", DateTime.Now.ToString("yyyyMMdd-hhmmss-fff"));
                }
                success = true;
            }
            catch
            { }
            StopCamera();
            return success;
        }
        #endregion

        #region Scanner-с уншина
        private void scan(PictureEdit pBox)
        {
            WIA.ImageFile img = null;
            bool success = Scan(ref img);

            if (success)
            {
                string fname = Path.GetTempPath() + DateTime.Now.ToString("yyyyMMdd-hhmmss-fff") + ".png";
                if (SaveImageToPNGFile(img, fname))
                {
                    StopCamera();
                    Bitmap bmp = new Bitmap(fname);
                    pBox.Image = null;
                    pBox.Image = bmp;
                    _filename = fname;
                }
            }
        }
        #endregion

        #region Туслах функц

        void ImageCenter()
        {
            int x, y;
            x = scrZoom.Width / 2 - picMain.Width / 2;
            y = scrZoom.Height / 2 - picMain.Height / 2;
            picMain.Location = new Point(x, y);
        }

        void trkZoom_ValueChanged(object sender, EventArgs e)
        {
            ImageCenter();
        }
        void trkZoom_EditValueChanged(object sender, EventArgs e)
        {
            scrZoom.VerticalScroll.Value = 0;
            scrZoom.HorizontalScroll.Value = 0;
            double n, p;
            n = trkZoom.Value;

            if (n > 25)
            {
                p = (n - 25) * 0.1;
                ImageCenter();
                picMain.Width = width + Convert.ToInt32(width * p);
                picMain.Height = height + Convert.ToInt32(height * p);
                ImageCenter();
            }
            if (n < 25)
            {
                p = (25 - n) * 0.05;
                ImageCenter();
                picMain.Width = width - Convert.ToInt32(width * p);
                picMain.Height = height - Convert.ToInt32(height * p);
                ImageCenter();
            }
            if (n == 25)
            {
                ImageCenter();
                picMain.Width = width;
                picMain.Height = height;
                ImageCenter();
            }
        }

        private bool saveImage(PictureEdit pBox)
        {
            bool success = false;

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "Jpg|*.jpg|Bmp|*.bmp|Gif|*.gif|Png|*.gif";
            if (pBox.Image != null)
            {
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap bmp = new Bitmap(pBox.Image);
                        switch (savefile.FilterIndex)
                        {
                            case 1: bmp.Save(savefile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                            case 2: bmp.Save(savefile.FileName, System.Drawing.Imaging.ImageFormat.Bmp); break;
                            case 3: bmp.Save(savefile.FileName, System.Drawing.Imaging.ImageFormat.Gif); break;
                            case 4: bmp.Save(savefile.FileName, System.Drawing.Imaging.ImageFormat.Png); break;
                        }
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
            }
            else
            {
                MessageBox.Show("Зураг оруулаагүй байна .", "Info");
            }
            return success;
        }

        public bool Scan(ref ImageFile image)
        {
            bool success = false;
            try
            {
                WIA.CommonDialog dialog = new WIA.CommonDialog();
                image = dialog.ShowAcquireImage(
                    WiaDeviceType.ScannerDeviceType,
                    WiaImageIntent.ColorIntent,
                    WiaImageBias.MaximizeQuality,
                    WIA.FormatID.wiaFormatJPEG, false, false, false);
                success = true;
            }
            catch
            {}
            return success;
        }

        private bool SaveImageToPNGFile(ImageFile image, string fileName)
        {
            bool success = false;
            try
            {
                if (image != null)
                {
                    ImageProcess imgProcess = new ImageProcess();
                    object convertFilter = "Convert";
                    string convertFilterID = imgProcess.FilterInfos.get_Item(ref convertFilter).FilterID;
                    imgProcess.Filters.Add(convertFilterID, 0);
                    SetWIAProperty(imgProcess.Filters[imgProcess.Filters.Count].Properties, "FormatID", WIA.FormatID.wiaFormatPNG);
                    image = imgProcess.Apply(image);
                    image.SaveFile(fileName);
                    return true;
                }
            }
            catch
            { }
            return success;
        }

        private bool SetWIAProperty(IProperties properties, object propName, object propValue)
        {
            bool success = false;
            try
            {
                Property prop = properties.get_Item(ref propName);
                prop.set_Value(ref propValue);
                success = true;
            }
            catch
            { }
            return success;
        }

        #endregion

        #region Control Events

        private void FormImage_Load(object sender, EventArgs e)
        {
            ImageCenter();
            width = picMain.Width;
            height = picMain.Height;
            fwidth = this.Width;
            fheight = this.Height;

            picMain.Image = _image;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            file(picMain);
        }
        private void btnScan_Click(object sender, EventArgs e)
        {
            scan(picMain);
        }
        private void btnTake_Click(object sender, EventArgs e)
        {
            take(picMain);
        }
        private void btnCam_Click(object sender, EventArgs e)
        {
            camer(picMain);
        }
        private void btnZoom_Click_1(object sender, EventArgs e)
        {
                //FormZoom frm = new FormZoom();
                //frm.picMain.Width = picMain.Width;
                //frm.picMain.Height = picMain.Height;
                //frm.picMain.Image = picMain.Image;
                //frm.Show();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveImage(picMain);
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = (picMain.Image == null ? DialogResult.Cancel : DialogResult.OK);
            if (picMain.Image != null)
            {
                _image = (System.Drawing.Image)picMain.Image.Clone();
            }
            this.Close();
        }
        private void btnRotLeft_Click(object sender, EventArgs e)
        {
            if (picMain.Image != null)
            {
                picMain.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                picMain.Refresh();
            }
        }
        private void btnRotRight_Click(object sender, EventArgs e)
        {
            if (picMain.Image != null)
            {
                picMain.Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                picMain.Refresh();
            }
        }

        #endregion
    }
}