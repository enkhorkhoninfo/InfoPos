using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using EServ.Shared;
namespace InfoPos.Panels
{
    public partial class ucSaleCustomer : DevExpress.XtraEditors.XtraUserControl
    {
        #region[Properties]
        private InfoPos.Core.Core _core = null;
        public InfoPos.Core.Core Core
        {
            get { return _core; }
            set
            {
                if (value != null)
                {
                    _core = value;
                    if (_remote == null) _remote = _core.RemoteObject;
                    if (_resource == null) _resource = _core.Resource;
                }
            }
        }
        Hashtable _allreadycustomer = new Hashtable();
        private ISM.CUser.Remote _remote = null;
        public ISM.CUser.Remote Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }

        private ISM.Template.Resource _resource = null;
        public ISM.Template.Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }
        #endregion
        #region[Variables]
        DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
        long customerno = 0;
        int itemindex = 0;
        #endregion
        #region[Constracture]
        public ucSaleCustomer()
        {
            try
            {
                InitializeComponent();
                galleryControl1.Gallery.Groups.Add(galleryItemGroup1);
                galleryControl1.Gallery.ShowGroupCaption = false;
                galleryControl1.Gallery.ItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(Gallery_ItemClick);
                galleryControl1.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
                galleryControl1.Gallery.ImageSize = new Size(24, 24);
                galleryControl1.Gallery.ShowItemText = true;
                galleryControl1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
                galleryControl1.Gallery.ItemCheckedChanged += new DevExpress.XtraBars.Ribbon.GalleryItemEventHandler(Gallery_ItemCheckedChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
            }
        }
        private void ucSaleCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                if (Resource != null)
                {
                    image.AddImage(Resource.GetImage("dashboard_user"));
                    image.AddImage(Resource.GetImage("image_woman"));
                    image.AddImage(Resource.GetImage("menu_office"));
                    btnDelete.Image = Resource.GetImage("edit-not-validated-icon");
                    btnEdit.Image = Resource.GetImage("edit-validated-icon");
                }
                galleryControl1.Gallery.Images = image;
                DataTable dt = new DataTable();
                dt.Columns.Add("SALESNO", typeof(string));
                dt.Columns.Add("CUSTOMERNO", typeof(long));
                dt.Columns.Add("CLASSCODE", typeof(int));
                dt.Columns.Add("SEX", typeof(int));
                dt.Columns.Add("REGISTERNO", typeof(string));
                dt.Columns.Add("CUSTNAME", typeof(string));
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} : {1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion
        #region[Events]
        void Gallery_ItemCheckedChanged(object sender, DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e)
        {
            customerno = Static.ToLong(e.Item.Caption);
            OnItemClick(Static.ToLong(e.Item.Caption), e.Item.Hint);
        }
        void Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            customerno = Static.ToLong(e.Item.Caption);
            OnItemClick(Static.ToLong(e.Item.Caption), e.Item.Hint);
        }
        #endregion
        #region[User Function]
        public Result RefreshData(string BatchNo)
        {
            Result res = new Result();
            object[] obj = new object[6];
            galleryItemGroup1.Items.Clear();
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 501, 600006, 600006, new object[] { BatchNo });
            if (res.ResultNo == 0)
            {
                DataTable dt = res.Data.Tables[0];
                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //CustomerNo, classcode,CustName,sex,registerno
                        DevExpress.XtraBars.Ribbon.GalleryItem galleryItem1 = new DevExpress.XtraBars.Ribbon.GalleryItem();
                        gridControl1.DataSource = dt;
                        if (!_allreadycustomer.ContainsKey(Static.ToStr(dr["CUSTOMERNO"])))
                        {
                            galleryItem1.Description = Static.ToStr(dr["CUSTNAME"]);
                            galleryItem1.Caption = Static.ToStr(dr["CUSTOMERNO"]);
                            galleryItem1.Hint = Static.ToStr(dr["SALESNO"]);
                            if (Static.ToStr(dr["CLASSCODE"]) == "0")
                            {
                                if (Static.ToInt(dr["SEX"]) == 0)
                                    galleryItem1.ImageIndex = 0;
                                else
                                    galleryItem1.ImageIndex = 1;
                            }
                            else
                            {
                                galleryItem1.ImageIndex = 2;
                            }
                            galleryItemGroup1.Items.Add(galleryItem1);
                            _allreadycustomer.Add(Static.ToStr(dr["CUSTOMERNO"]), 0);
                        }
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// Мөр нэмэх
        /// </summary>
        /// <param name="SalesNo">Борлуулалтын дугаар</param>
        /// <param name="CustomerNo">Харилцагчийн дугаар</param>
        /// <param name="ClassCode">Харилцагчийн төрөл 0-Иргэн, 1-Байгууллага</param>
        /// <param name="Sex">Хүйс 0-Эр, 1-Эм</param>
        /// <param name="RegisgerNo">Регистер №</param>
        /// <param name=CustName">Харилцагчийн нэр</param>
        /// <returns></returns>
        public Result AddRow(string SalesNo, string CustomerNo, int ClassCode, int Sex, string RegisterNo, string CustName)
        {
            Result res = new Result();
            try
            {
                if (!_allreadycustomer.ContainsKey(Static.ToStr(CustomerNo)))
                {
                    //DataTable dt = (DataTable)gridControl1.DataSource;
                    //DataRow dr = dt.NewRow();
                    //dr["SALESNO"] = SalesNo;
                    //dr["CUSTOMERNO"] = CustomerNo;
                    //dr["CLASSCODE"] = ClassCode;
                    //dr["SEX"] = Sex;
                    //dr["REGISTERNO"] = RegisterNo;
                    //dr["CUSTNAME"] = CustName;
                    //dt.Rows.Add(dr);
                    //gridControl1.DataSource = dt;
                    res.ResultDesc = "Амжилттай нэмлээ";
                    DevExpress.XtraBars.Ribbon.GalleryItem galleryItem1 = new DevExpress.XtraBars.Ribbon.GalleryItem();
                    //galleryItem1.Description = Static.ToStr(dr["CUSTNAME"]);
                    //galleryItem1.Caption = Static.ToStr(dr["CUSTOMERNO"]);
                    //galleryItem1.Hint = Static.ToStr(dr["SALESNO"]);
                    //if (Static.ToStr(dr["CLASSCODE"]) == "0")
                    //{
                    //    if (Static.ToInt(dr["SEX"]) == 0)
                    //        galleryItem1.ImageIndex = 0;
                    //    else
                    //        galleryItem1.ImageIndex = 1;
                    //}
                    galleryItem1.Description = CustName;
                    galleryItem1.Caption = Static.ToStr(CustomerNo);
                    galleryItem1.Hint = "";
                    
                    if (ClassCode == 0)
                    {
                        if (Sex == 0)
                            galleryItem1.ImageIndex = 0;
                        else
                            galleryItem1.ImageIndex = 1;
                    }
                    else
                    {
                        galleryItem1.ImageIndex = 2;
                    }
                    galleryItemGroup1.Items.Add(galleryItem1);
                    _allreadycustomer.Add(Static.ToStr(CustomerNo), 0);
                }
                else
                {
                    res.ResultNo = 1;
                    res.ResultDesc = "Энэ харилцагч сонгогдсон байна.";
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message + " : " + ex.StackTrace;
                return res;
            }
        }       
        //public Result DeleteRow(long CustomerNo)
        //{
        //    Result res = new Result();
        //    try
        //    {
        //        DataTable dt = (DataTable)gridControl1.DataSource;
        //        int i = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if (Static.ToLong(dr["CUSTOMERNO"]) == CustomerNo)
        //            {
        //                gridView1.DeleteRow(i);
        //                break;
        //            }
        //            i++;
        //        }


        //        dt = (DataTable)gridControl1.DataSource;
        //        res.ResultDesc = "Амжилттай хаслаа";
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ResultNo = 1;
        //        res.ResultDesc = ex.Message + " : " + ex.StackTrace;
        //        return res;
        //    }
        //}
        public Result EditRow(long CustomerNo, string CustName, int Sex,int classcode)
        {
            Result res = new Result();
            try
            {
                if (!_allreadycustomer.ContainsKey(Static.ToStr(CustomerNo)))
                {
                    foreach (DevExpress.XtraBars.Ribbon.GalleryItem gitem in galleryItemGroup1.Items)
                    {
                        if (Static.ToLong(gitem.Caption) == customerno)
                        {

                            gitem.Caption = Static.ToStr(CustomerNo);
                            gitem.Description = CustName;
                            if (classcode == 0)
                            {
                                if (Sex == 0)
                                    gitem.ImageIndex = 0;
                                else
                                    gitem.ImageIndex = 1;
                            }
                            else
                            {
                                gitem.ImageIndex = 2;
                            }
                            break;
                        }
                    }
                    DataTable dt = (DataTable)gridControl1.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Static.ToLong(dr["CUSTOMERNO"]) == customerno)
                        {
                            dr["CUSTOMERNO"] = CustomerNo;
                            dr["SEX"] = Sex;
                            dr["CLASSCODE"] = classcode;
                            dr["CUSTNAME"] = CustName;
                        }
                    }
                    _allreadycustomer.Remove(Static.ToStr(customerno));
                    _allreadycustomer.Add(Static.ToStr(CustomerNo), 0);
                    customerno = CustomerNo;
                }
                else
                {
                    res.ResultNo = 1;
                    res.ResultDesc = "Энэ харилцагч сонгогдсон байна.";
                }
                
                return res;
            }
            catch (Exception ex)
            {
                res.ResultNo = 1;
                res.ResultDesc = ex.Message;
                return res;
            }
        }
        public void SelectItem(string salesno)
        {
            foreach (DevExpress.XtraBars.Ribbon.GalleryItem gi in galleryItemGroup1.Items)
            {
                if (gi.Hint == salesno)
                    gi.Checked = true;
            }
        }
        public void SelectItem(long customerno)
        {
            foreach (DevExpress.XtraBars.Ribbon.GalleryItem gi in galleryItemGroup1.Items)
            {
                if (gi.Caption == customerno.ToString())
                    gi.Checked = true;
            }
        }
        public void ClearAll()
        {
            galleryControl1 = null;
        }
        public int ItemCount()
        {
            return _allreadycustomer.Count;
        }

        /// <summary>
        /// Товчнуудын харагдац
        /// </summary>
        /// <param name="value">true - Харагдана, false - харагдахгүй</param>
        public void BtnsVisible(bool value)
        {
            pnlControls.Visible = value;
        }
        #endregion
        #region[Custom Events]
        public delegate void delegateEventOnItemClick(long CustomerNo, string SalesNo);
        public event delegateEventOnItemClick EventOnItemClick;
        public void OnItemClick(long CustomerNo,string SalesNo)
        {
            try
            {
                if (EventOnItemClick != null) EventOnItemClick(CustomerNo, SalesNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }

        public delegate void delegateEventOnBtnEdit(long CustomerNo);
        public event delegateEventOnBtnEdit EventOnBtnEdit;
        public void OnBtnEdit(long CustomerNo)
        {
            try
            {
                if (EventOnBtnEdit != null)
                {
                    EventOnBtnEdit(CustomerNo);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }

        public delegate void delegateEventOnBtnDelete(long CustomerNo);
        public event delegateEventOnBtnDelete EventOnBtnDelete;
        public void OnBtnDelete(long CustomerNo)
        {
            try
            {
                if (EventOnBtnDelete != null) EventOnBtnDelete(CustomerNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace + ex.StackTrace);
            }
        }

        #endregion

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_allreadycustomer.Count != 0)
                OnBtnEdit(customerno);
            else _core.AlertShow("Мэдээлэл","Харилцагч сонгогдоогүй байна.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_allreadycustomer.Count != 0)
            {
                foreach (DevExpress.XtraBars.Ribbon.GalleryItem gitem in galleryItemGroup1.Items)
                {
                    if (Static.ToLong(gitem.Caption) == customerno)
                    {
                        _allreadycustomer.Remove(gitem.Caption);
                        galleryItemGroup1.Items.Remove(gitem);
                        break;
                    }
                }
                OnBtnDelete(customerno);
            }
            else _core.AlertShow("Мэдээлэл","Харилцагч сонгогдоогүй байна.");
        }
    }
}
