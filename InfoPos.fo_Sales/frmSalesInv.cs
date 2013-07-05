using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars.Ribbon;

using ISM.Touch;
using InfoPos.Core;
using EServ.Shared;

namespace InfoPos.sales
{
    public partial class frmSalesInv : DevExpress.XtraEditors.XtraForm
    {
        #region Internal variables
        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;

        string _salesno = null;
        decimal _custno = 0;  //pledge owner
        string _custname = null;
        decimal _selected_custno = 0;
        string _selected_custname = null;

        string _layoutfilename = "";

        DataTable _dtCust = null;
        DataTable _dtRent = null;
        InfoPos.Core.frmTagReader _frmTag = null;

        #endregion
        #region Constructor and events
        public frmSalesInv(InfoPos.Core.Core core, string salesno)
        {
            InitializeComponent();
            this.FormClosing += frmSalesInv_FormClosing;
            this.galleryCust.Gallery.ItemCheckedChanged += Gallery_ItemCheckedChanged;

            _core = core;
            if (_core != null) _resource = _core.Resource;

            _salesno = salesno;
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);

            #region Gallary Formatting

            galleryCust.Dock = DockStyle.Fill;
            galleryCust.Gallery.AllowFilter = false;
            galleryCust.Gallery.AllowHoverImages = true;
            galleryCust.Gallery.AllowMarqueeSelection = true;
            galleryCust.Gallery.AutoFitColumns = false;
            galleryCust.Gallery.CheckDrawMode = DevExpress.XtraBars.Ribbon.Gallery.CheckDrawMode.ImageAndText;
            galleryCust.Gallery.ColumnCount = 4;
            galleryCust.Gallery.DrawImageBackground = false;
            galleryCust.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
            galleryCust.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.MiddleRight;
            galleryCust.Gallery.ItemImageLocation = DevExpress.Utils.Locations.Left;
            galleryCust.Gallery.ShowGroupCaption = true;
            galleryCust.Gallery.ShowItemText = true;
            galleryCust.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
            //galleryCust.LookAndFeel.UseDefaultLookAndFeel = false;

            #endregion
            #region Grid1 formatting

            gridControl1.Dock = DockStyle.Fill;
            gridView1.GroupFormat = "{1}";

            //gridView1.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gridView1.Columns[5].DisplayFormat.FormatString = "#,##0.00";
            gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsCustomization.AllowColumnMoving = false;
            gridView1.OptionsCustomization.AllowRowSizing = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            gridView1.OptionsSelection.UseIndicatorForSelection = false;

            gridView1.RowHeight = 28;

            #endregion
        }
        void frmSalesInv_Load(object sender, EventArgs e)
        {
            if (_core != null && _core.Resource != null)
            {
                btnCustAdd.Image = _core.Resource.GetImage("frontmenu_customer_add");
                btnCustRem.Image = _core.Resource.GetImage("frontmenu_customer_remove");
                btnMove.Image = _core.Resource.GetImage("frontmenu_sales_exchange");
                btnTag.Image = _core.Resource.GetImage("frontmenu_tag");
                btnExit.Image = _core.Resource.GetImage("frontmenu_exit");
            }

            InitSalesInv(_salesno);
        }
        void frmSalesInv_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }

        void Gallery_ItemCheckedChanged(object sender, GalleryItemEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                _selected_custno = Static.ToDecimal(e.Item.Tag);
                _selected_custname = e.Item.Caption;
            }
        }

        private void btnCustAdd_Click(object sender, EventArgs e)
        {
            SubMenu_CustomerAdd();
        }
        private void btnCustRem_Click(object sender, EventArgs e)
        {
            SubMenu_CustomerDel();
        }
        private void btnMove_Click(object sender, EventArgs e)
        {
            MoveRent();
        }
        private void btnTag_Click(object sender, EventArgs e)
        {
            TagWrite();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        #region Business functions

        public void SubMenu_CustomerAdd()
        {
            InfoPos.fo_Customer.frmIndividualSearch frm = new fo_Customer.frmIndividualSearch(_core);
            frm.StartPosition = FormStartPosition.CenterScreen;
            DialogResult res = frm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (frm.Selected)
                {
                    CustomerAdd(frm.CustNo, frm.CustName, frm.CustReg);
                }
            }
            frm.Dispose();
        }
        public void SubMenu_CustomerDel()
        {
            try
            {
                CustomerDel(_selected_custno);
            }
            catch (Exception ex)
            {
            }
        }

        public void CustomerAdd(decimal custno, string custname, string register)
        {
            #region Get group
            GalleryItemGroup group = null;
            if (galleryCust.Gallery.Groups.Count > 0)
            {
                group = galleryCust.Gallery.Groups[0];
            }
            else
            {
                group = new GalleryItemGroup();
                galleryCust.Gallery.Groups.Add(group);
            }
            #endregion
            #region Check item is exists
            GalleryItem item = null;
            for (int i = 0; i < group.Items.Count; i++)
            {
                item = group.Items[i];
                if (item.Tag != null)
                {
                    decimal d = Static.ToDecimal(item.Tag);
                    if (custno == d)
                    {
                        return; // already exists
                    }
                }
            }
            #endregion
            #region Add new item
            item = new GalleryItem();
            item.Caption = custname;
            item.Description = register;
            item.Tag = custno;

            group.Items.Add(item);
            #endregion
            if (_custno <= 0)
            {
                _custno = _selected_custno = custno;
                _custname = _selected_custname = custname;
            }
        }
        public void CustomerDel(decimal custno)
        {
            #region Get group
            GalleryItemGroup group = null;
            if (galleryCust.Gallery.Groups.Count > 0)
            {
                group = galleryCust.Gallery.Groups[0];
            }
            else
            {
                group = new GalleryItemGroup();
            }
            #endregion
            #region Check item is exists
            for (int i = 0; i < group.Items.Count; i++)
            {
                GalleryItem item = group.Items[0];
                if (item.Tag != null)
                {
                    decimal d = Static.ToDecimal(item.Tag);
                    if (custno == d)
                    {
                        group.Items.Remove(item);
                    }
                }
            }
            #endregion
        }
        public void CustomerUpdate(decimal custno, string custname, string register)
        {
            #region Get group
            GalleryItemGroup group = null;
            if (galleryCust.Gallery.Groups.Count > 0)
            {
                group = galleryCust.Gallery.Groups[0];
            }
            else
            {
                group = new GalleryItemGroup();
                galleryCust.Gallery.Groups.Add(group);
            }
            #endregion
            #region Check item is exists
            GalleryItem item = null;
            for (int i = 0; i < group.Items.Count; i++)
            {
                item = group.Items[i];
                if (item.Tag != null)
                {
                    decimal d = Static.ToDecimal(item.Tag);
                    if (custno == d)
                    {
                        item.Caption = custname;
                        item.Description = register;
                        return;
                    }
                }
            }
            #endregion
        }

        public void InitSalesInv(string salesno)
        {
            Result res = null;
            try
            {
                #region Validation
                #endregion
                #region Prepare parameters
                object[] param = new object[] { salesno };
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605003, 605001, param);
                if (res.ResultNo != 0) goto OnExit;

                _dtCust = res.Data.Tables[0];
                _dtRent = res.Data.Tables[1];
                
                #endregion
                #region Build customers gallery control
                foreach (DataRow r in _dtCust.Rows)
                {
                    string custreg = Static.ToStr(r["registerno"]);
                    string custname = Static.ToStr(r["custname"]);
                    string serialno = Static.ToStr(r["serialno"]) + ":" + Static.ToStr(r["tagframeno"]);
                    decimal custno = Static.ToDecimal(r["custno"]);
                    CustomerAdd(custno, custname, serialno);
                }
                #endregion
                #region Build rent grid

                ISM.Template.FormUtility.GridLayoutGet(gridView1, _dtRent, _layoutfilename);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "custno", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Үйлчлүүлэгч");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "prodno", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Хэрэгсэл");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "itemno", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "status", true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Төлөв");
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "prodtype",true);
                ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "tagchargetime", true);

                gridView1.Columns[1].GroupIndex = 0;

                #endregion
            }
            catch (Exception ex)
            {
            }
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }

        public void MoveRent()
        {
            Result res = null;

            // Хэрэгсэл сонгогдоогүй бол шууд гарах.
            DataRow r = gridView1.GetFocusedDataRow();
            if (r == null) return;

            // Шилжүүлэх үйлчлүүлэгч сонгогдоогүй бол шууд гарах.
            if (_selected_custno <= 0) return;

            decimal src_custno = Static.ToDecimal(r["CUSTNO"]);
            decimal src_custname = Static.ToDecimal(r["CUSTNAME"]);

            // Өөрлүүгээ шилжүүлэх боломжгүй шууд гарах.
            if (_selected_custno == src_custno) return;

            string prodno = Static.ToStr(r["PRODNO"]);
            string prodname = Static.ToStr(r["PRODNAME"]);
            int prodtype = Static.ToInt(r["PRODTYPE"]);
            int itemno = Static.ToInt(r["ITEMNO"]);

            string confirm = string.Format("[{0}] Хэрэгслийг [{1}] үйлчлүүлэгч рүү шилжүүлэх үү?", prodname, _selected_custname);
            if (ISM.Template.FormUtility.ValidateConfirm(confirm))
            {
                #region Prepare parameters
                object[] param = new object[] { _core.TxnDate, _salesno, _core.POSNo, _core.AreaCode, prodno, prodtype, itemno, _selected_custno };
                #endregion
                #region Call server
                if (_core == null || _core.RemoteObject == null)
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                    goto OnExit;
                }
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 605, 605018, 605018, param);
                if (res.ResultNo != 0) goto OnExit;
                #endregion

                r["CUSTNO"] = _selected_custno;
                r["CUSTNAME"] = _selected_custname;

                r.Table.AcceptChanges();
                gridView1.RefreshData();
            }

        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        public void TagWrite()
        {
            Result res = null;
            #region Борлуулалтын мэдээлэл сонгогдсон эсэхийг шалгах

            if (string.IsNullOrEmpty(_salesno))
            {
                res = new Result(9, "Борлуулалт сонгогдоогүй байна.");
                ISM.Template.FormUtility.ValidateQuery(res);
                return;
            }

            #endregion
            #region Таг уншигч залгагдсан эсэхийг шалгах
            
            if (!_core.Tag.IsOpenned)
            {
                res = new Result(9, "Таг уншигч холбогдоогүй байна!");
                ISM.Template.FormUtility.ValidateQuery(res);
                return;
            }

            #endregion
            #region Жагсаалтанд бгаа бүх таггүй харилцагчаар гүйнэ

            _frmTag = new frmTagReader(_core, "", true);
            //EventOnCard += _frmTag.EventOnCard;

            decimal custno = 0;
            string custname = null;
            string custtag = null;

            foreach (DataRow r in _dtCust.Rows)
            {
                #region Сонгогдсон мөрөөс харилцагчийн дугаарыг авах

                custno = Static.ToDecimal(r["CUSTNO"]);
                custname = Static.ToStr(r["CUSTNAME"]);
                custtag = Static.ToStr(r["SERIALNO"]);

                #endregion

                #region Таг дээр цэнэглэх цагийн мэдээллийг нэгтгэж авах

                /*************************************************
                 * Хэрэв нэг харилцагч дээр хэд хэдэн Таг цэнэглэх
                 * цагтай үйлчилгээнүүд бол бүх цагуудыг нэмж нэг
                 * бүхэл цаг бичнэ. 2013.02.16 Амараа
                 *************************************************/
                int totaltime = _dtRent.AsEnumerable().Sum(x => Static.ToDecimal(x["CUSTNO"]) == custno ? Static.ToInt(x["TAGCHARGETIME"]) : 0);

                #endregion

                /*************************************************
                 * Өмнө нь таг холбосон бол яг холбосон тагыг нь 
                 * шалгаж, тэр таг нь уншигдах үед бичих хэрэгтэй 
                 * болох байх даа ?! Тодруулах шаардлагатай юм бн. 2013.02.16 Амараа
                 *************************************************/
                //if (string.IsNullOrEmpty(custtag))

                #region Таг форм дуудаж сериал дугаар авах

            OnRestart:

                _frmTag.SetCaption(string.Format("[{0}] үйлчлүүлэгч дээр холбох тагийг уншуулна уу.", custname));
                DialogResult dlg = _frmTag.ShowDialog();
                if (dlg == System.Windows.Forms.DialogResult.Cancel) goto OnExit;
                if (dlg == System.Windows.Forms.DialogResult.Ignore) goto OnNext;
                string serialno = _frmTag.SerialNo;
                
                #endregion
                #region Хэрэв өмнө нь таг холбосон бол яг тэр таг уншигдсан эсэхийг шалгана.
                if (!string.IsNullOrEmpty(custtag) && custtag != serialno && totaltime > 0)
                {
                    Alert(string.Format("[{0}] Харилцагч дээр холбоотой байгаа [{1}] тагийг уншуулна уу!", custname, serialno), "Таг цэнэглэх");
                    goto OnRestart;
                }
                #endregion
                #region Таг дээр бичих

                if (totaltime > 0)
                {
                    bool success = _core.Tag.Reader_WriteData(serialno, DateTime.Now, DateTime.Now.AddHours(totaltime));
                    if (!success)
                    {
                        Alert(string.Format("Таг дээр бичилт хийгдэж чадсангүй, дахин уншуулна уу!\r\n{0}", _core.Tag.ErrorMessage)
                            , "Таг цэнэглэх"
                            , 2
                            );
                        goto OnRestart;
                    }
                }
                #endregion
                #region Таг холбох үйлдэл хийх
                res = TagLinkAdd(custno, serialno);
                if (res != null && res.ResultNo != 0)
                {
                    Alert(res, "Таг холбох");
                    goto OnRestart;
                }

                if (res.Param != null && res.Param.Length > 0)
                {
                    string tagframeno = Static.ToStr(res.Param[0]);
                    serialno = string.Format("{0}:{1}", serialno, tagframeno);
                }
                CustomerUpdate(custno, custname, serialno);

                #endregion

            OnNext:
                ;
            }
            #endregion
        OnExit:
            _core.Tag.EventOn = true;
            _frmTag.Dispose();
            _frmTag = null;
        }
        public Result TagLinkAdd(decimal custno, string serialno)
        {
            Result res = null;
            #region Validation
            if (string.IsNullOrEmpty(serialno))
            {
                res = new Result(601001, "Тагийн дугаар буруу байна!");
                goto OnExit;
            }
            if (custno == 0)
            {
                res = new Result(601002, "Үйлчлүүлэгч сонгогдоогүй байна!");
                goto OnExit;
            }
            #endregion
            #region Prepare parameters
            object[] param = new object[] { custno, serialno };
            #endregion
            #region Call server
            if (_core != null && _core.RemoteObject != null)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601008, 601008, param);
                if (res.ResultNo != 0) goto OnExit;
            }
            else
            {
                res = new Result(99999, "Internal Error: Remote object not set.");
            }
            #endregion
        OnExit:
            return res;
            //ISM.Template.FormUtility.ValidateQuery(res);
            //Alert(res, "Таг холбох");
        }

        public void BillPrint()
        {
            Result res = null;
            try
            {
                #region Validation
                if (string.IsNullOrEmpty(_salesno))
                {
                    res = new Result(9, "Борлуулалт сонгоогдоогүй байна.");
                    goto OnExit;
                }
                #endregion

                frmBillShow frm = new frmBillShow(_core);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.PrepareBillContents(_salesno);
                //frm.BillTagShow(_salesno, _dtRent, _dtCust);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                res = new Result(9, ex.ToString());
            }
        OnExit:
            Alert(res, "Билл хэвлэх");
            //ISM.Template.FormUtility.ValidateQuery(res, success_text);
        }

        public void Alert(Result res, string caption)
        {
            if (res != null && res.ResultNo != 0)
            {
                if (_core != null)
                {
                    _core.AlertShow(caption, res.ResultDesc, 2);
                }
            }
        }
        public void Alert(string text, string caption, int image = 0)
        {
            if (_core != null)
            {
                _core.AlertShow(caption, text, image);
            }
        }

        public void EventOnCard(Core.TagEventData tagdata)
        {
            if (_frmTag!=null) _frmTag.EventOnCard(tagdata);
        }

        #endregion
    }
}
