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

using EServ.Shared;
using ISM.Touch;
using InfoPos.Core;

namespace InfoPos.Reg
{
    public partial class frmReg : DevExpress.XtraEditors.XtraForm, ISM.Touch.ITouchCall
    {
        // Харилцагч холбоход дараах эх үүсвэрээс авна:
        // 1. Харилцагч сонгох
        // 2. Гэрээ сонгох
        // 3. Захиалга сонгох

        #region Internal events

        public event Core.Tag.delegateEventOnCard EventOnCard;

        #endregion
        #region Internal variables

        TouchKeyboard _kb = null;
        InfoPos.Core.Core _core = null;
        InfoPos.Resource _resource = null;

        decimal _pledgecustno = 0;  //pledge owner
        string _pledgeno = null;
        int _doctype = 0;
        string _docno = null;
        string _custname = null;
        string _contactno = null;
        string _memo = null;
        int _status = 0;
        DateTime _udate;
        int _userno = 0;

        DataTable _rents = null;
        DataTable _custs = null;

        string _serialno = null;
        string _tagno = null;
        string _batchno = null;

        private string _layoutfilename = "";

        RepositoryItemCheckEdit ri_checkbox = null;

        #endregion
        #region Menu functions

        public void Init(string buttonkey, TouchLinkItem item, Form parent, object param, ref bool cancel)
        {
            try
            {
                _core = (InfoPos.Core.Core)param;
                _resource = _core.Resource;
                _kb = new TouchKeyboard();
                if (_core.IsTouch == true)
                    _kb.Enable = true;
                else
                    _kb.Enable = false;

                //this.ucPledgeSearch1.Core = _core;
                //this.ucPledgeSearch1.Remote = _core.RemoteObject;
                //this.ucPledgeSearch1.Resource = _resource;
                //this.ucPledgeSearch1.TouchKeyboard = _kb;

                //this.ucPledgeList1.Core = _core;
                //this.ucPledgeList1.Remote = _core.RemoteObject;
                //this.ucPledgeList1.Resource = _resource;
                //this.ucPledgeList1.TouchKeyboard = _kb;

                //this.ucRentList1.Core = _core;
                //this.ucRentList1.Remote = _core.RemoteObject;
                //this.ucRentList1.Resource = _resource;
                //this.ucRentList1.TouchKeyboard = _kb;

                //this.ucCustSearch1.Core = _core;
                //this.ucCustSearch1.Remote = _core.RemoteObject;
                //this.ucCustSearch1.Resource = _core.Resource;
                //this.ucCustSearch1.TouchKeyboard = _kb;

                this.MdiParent = parent;
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Call(string buttonkey, TouchLinkItem item, ref bool cancel)
        {
            try
            {
                switch (buttonkey)
                {
                    case "fo_reg_search":
                        SubMenu_Search();
                        break;
                    case "fo_reg_searchtag":
                        SubMenu_Tag();
                        break;
                    case "fo_reg_add":
                        SubMenu_Receive();
                        break;
                    case "fo_reg_del":
                        SubMenu_Deliver();
                        break;
                    case "fo_reg_edit":
                        SubMenu_Edit();
                        break;

                    case "fo_reg_taglinkset":
                        SubMenu_TagLinkSet();
                        break;
                    case "fo_reg_taglinkdel":
                        SubMenu_TagLinkDel();
                        break;

                    case "fo_reg_custadd":
                        SubMenu_CustomerAdd();
                        break;
                    case "fo_reg_custdel":
                        SubMenu_CustomerDel();
                        break;
                    case "fo_reg_fine":
                        SubMenu_Fine();
                        break;

                    case "call_tagreader":
                        string tagno = item.Text;
                        Core.TagEventData tagdata = (TagEventData)item.Tag;
                        if (EventOnCard != null)
                        {
                            EventOnCard(tagdata);
                        }
                        else
                        {
                            SubMenu_TagReader(tagno);
                        }
                        break;
                    case "fo_reg_exit":
                        this.Close();
                        item.IsClose = 1;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SubMenu_Search()
        {
            frmRegSearch frm = new frmRegSearch(_core);
            DialogResult res = frm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (frm.CurrentRow != null)
                {
                    _pledgeno = Static.ToStr(frm.CurrentRow["pledgeno"]);
                    frm.Dispose();

                    ReadRecord(_pledgeno);
                }
            }
        }
        public void SubMenu_Tag()
        {
            Panels.frmTagReader frm = new Panels.frmTagReader(_core);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(frm.SerialNo))
                {
                    string serialno = frm.SerialNo;
                    frm.Dispose();

                    ReadRecordByTag(serialno);
                }
            }
        }
        public void SubMenu_Receive()
        {
            frmCreate frm = new frmCreate(_core, null);
            DialogResult res = frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _pledgeno = frm.PledgeNo;
                frm.Dispose();

                ReadRecord(_pledgeno);
            }
        }
        public void SubMenu_Deliver()
        {
            PledgeDel(_pledgeno);

            //Result res = null;
            //if (tabMain.SelectedTabPageIndex != 1)
            //{
            //    res = Msg.Get(EnumMessage.CUSTOMER_NOT_SELECTED);
            //}
            //else
            //{
            //    res = ucPledgeList1.Deliver();
            //}
        }
        public void SubMenu_Edit()
        {
            frmCreate frm = new frmCreate(_core, null);
            Result res = frm.Read(_pledgeno);
            if (res != null && res.ResultNo == 0)
            {
                DialogResult dres = frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    _pledgeno = frm.PledgeNo;
                    frm.Dispose();

                    ReadRecord(_pledgeno);
                }
            }
        }
        public void SubMenu_TagLinkSet()
        {
            Result res = null;
            #region Барьцааны мэдээлэл сонгогдсон эсэхийг шалгах

            if (string.IsNullOrEmpty(_pledgeno))
            {
                res = new Result(9, "Барьцааны мэдээлэл сонгогдоогүй байна.");
                ISM.Template.FormUtility.ValidateQuery(res);
                return;
            }

            #endregion
            #region Жагсаалтанд бгаа бүх таггүй харилцагчаар гүйнэ

            InfoPos.Core.frmTagReader frm = new frmTagReader(_core, "", true);
            EventOnCard += frm.EventOnCard;

            gridView2.MoveFirst();
            DataRow r = gridView2.GetFocusedDataRow();
            decimal custno = 0;
            string custname = null;
            string custtag = null;

            while (r != null)
            {
                #region Сонгогдсон мөрөөс харилцагчийн дугаарыг авах

                custno = Static.ToDecimal(r["CUSTNO"]);
                custname = Static.ToStr(r["CUSTNAME"]);
                custtag = Static.ToStr(r["SERIALNO"]);

                #endregion
                if (string.IsNullOrEmpty(custtag))
                {
                    #region Таг форм дуудаж сериал дугаар авах

                OnRestart:
                    frm.SetCaption(string.Format("[{0}] үйлчлүүлэгч дээр холбох тагийг уншуулна уу.", custname));
                    DialogResult dlg = frm.ShowDialog();
                    if (dlg == System.Windows.Forms.DialogResult.Cancel) goto OnExit;
                    if (dlg == System.Windows.Forms.DialogResult.Ignore) goto OnNext;
                    string serialno = frm.SerialNo;

                    #endregion
                    #region Таг холбох үйлдэл хийх
                    res = TagLinkAdd(custno, serialno);
                    if (res != null && res.ResultNo != 0)
                    {
                        //Таг холбох үед алдаа гарвал дахин таг уншуулах
                        Alert(res, "Таг холбох");
                        goto OnRestart;
                    }
                    #endregion
                }
            OnNext:
                #region Дараагийн бичлэг руу гүйх
                if (!gridView2.IsLastRow)
                {
                    gridView2.MoveNext();
                    r = gridView2.GetFocusedDataRow();
                }
                else
                {
                    break;
                }
                #endregion
            }
            #endregion
        OnExit:
            EventOnCard -= frm.EventOnCard;
            frm.Dispose();
        }
        public void SubMenu_TagLinkDel()
        {
            Result res = null;

            InfoPos.Core.frmTagReader frm = new frmTagReader(_core, "");
            EventOnCard += frm.EventOnCard;

            #region Барьцааны мэдээлэл сонгогдсон эсэхийг шалгах

            if (string.IsNullOrEmpty(_pledgeno))
            {
                res = new Result(9, "Барьцааны мэдээлэл сонгогдоогүй байна.");
                ISM.Template.FormUtility.ValidateQuery(res);
                return;
            }

            #endregion
            #region Салгах таг байгаа эсэхийг шалгах

            DataRow[] rows = null;
            if (_custs != null)
            {
                rows = _custs.Select(string.Format("SERIALNO<>''"));
                if (rows != null && rows.Length <= 0)
                {
                    res = new Result(9, "Холбоотой таг алга байна.");
                    Alert(res, "Таг салгах");
                    goto OnExit;
                }
            }

            #endregion

            #region Таг форм дуудаж сериал дугаар авах

        OnRestart:
            frm.SetCaption(string.Format("Салгах тагийг уншуулна уу."));
            DialogResult dlg = frm.ShowDialog();
            if (dlg != System.Windows.Forms.DialogResult.OK) goto OnExit;
            string serialno = frm.SerialNo;

            #endregion

            #region Уншигдсан таг дугаараар бичлэгийг олох

            rows = _custs.Select(string.Format("SERIALNO='{0}'", serialno));
            if (rows != null && rows.Length <= 0)
            {
                res = new Result(9, string.Format("[{0}] дугаартай таг алга байна.", serialno));
                Alert(res, "Таг салгах");
                goto OnRestart;
            }

            decimal custno = Static.ToDecimal(rows[0]["CUSTNO"]);
            //serialno = Static.ToStr(rows[0]["SERIALNO"]);

            res = TagLinkDel(custno, serialno);
            if (res != null && res.ResultNo != 0)
            {
                Alert(res, "Таг салгах");
                goto OnRestart;
            }

            rows = _custs.Select(string.Format("SERIALNO<>''"));
            if (rows != null && rows.Length > 0) goto OnRestart;
            
            #endregion

        OnExit:
            if (frm != null)
            {
                EventOnCard -= frm.EventOnCard;
                frm.Dispose();
            }
        }

        public void SubMenu_CustomerAdd()
        {
            //InfoPos.fo_Customer.frmCustomer frm = new fo_Customer.frmCustomer(_core, "", "", "", "", "", "");
            //frm.ShowDialog();

            InfoPos.fo_Customer.frmCustSearch frm = new fo_Customer.frmCustSearch(_core);
            DialogResult res = frm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (frm.Selected)
                {
                    CustomerAdd(_pledgeno, _pledgecustno, frm.CustNo);
                }
            }
            frm.Dispose();
        }
        public void SubMenu_CustomerDel()
        {
            decimal custno = 0;
            try
            {
                DataRow r = gridView2.GetFocusedDataRow();
                if (r != null)
                {
                    custno = Static.ToDecimal(r["custno"]);
                }
                CustomerDel(_pledgeno, _pledgecustno, custno);
            }
            catch (Exception ex)
            {
            }
        }
        public void SubMenu_Fine()
        {
            try
            {
                DataRow r = gridView1.GetFocusedDataRow();
                if (r != null)
                {
                    int fined = Static.ToInt(r["FINED"]);
                    string salesno = Static.ToStr(r["SALESNO"]);
                    decimal custno =Static.ToDecimal(r["CUSTNO"]);
                    string prodno =Static.ToStr(r["PRODNO"]);
                    int itemno = Static.ToInt(r["ITEMNO"]);

                    string prodname = Static.ToStr(r["NAME"]);
                    string rentstatus = Static.ToStr(r["RENTSTATUS"]);
                    string damagenote = Static.ToStr(r["DAMAGENOTE"]);
                    decimal rentminutes = Static.ToDecimal(r["RENTMINUTES"]);

                    if (fined == 1)
                    {
                        frmRegFine frm = new frmRegFine(_core, salesno, custno, prodno, 0, itemno, prodname, rentstatus, damagenote, rentminutes);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        DialogResult res = frm.ShowDialog();
                        if (res == System.Windows.Forms.DialogResult.OK)
                        {
                            ReadRecord(_pledgeno);
                        }
                    }
                    else
                    {
                        if (_core != null)
                            _core.AlertShow("Торгууль төлөх", "Төлөх торгууль алга байна.", 1);
                    }
                }
                
            }
            catch (Exception ex)
            {
            }
        }
        
        private void SubMenu_TagReader(string serialno)
        {
            _serialno = serialno;
            Result res = ReadRecordByTag(serialno);
            //ISM.Template.FormUtility.ValidateQuery(res);
        }

        #endregion
        #region Business functions

        public string GetDocTypeName(int doctype)
        {
            string name = doctype.ToString();

            DataTable dt = null;
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject, "PLEDGETYPE", 601003, ref dt);
            if (res != null && res.ResultNo == 0)
            {
                DataRow[] rows = dt.Select(string.Format("TYPEID={0}", doctype));
                if (rows != null && rows.Length > 0)
                {
                    name = Static.ToStr(rows[0]["TYPENAME"]);
                }
            }
            return name;
        }

        public Result ReadRecord(string pledgeno)
        {
            Result res = null;
            try
            {
                #region Validation
                #endregion
                #region Prepare parameters
                object[] param = new object[] { pledgeno };
                #endregion
                #region Call server
                if (_core != null && _core.RemoteObject != null)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601004, 601003, param);
                    if (res.ResultNo != 0) goto OnExit;

                    #region Pledge main details

                    DataTable d1 = res.Data.Tables[0];

                    if (d1.Rows.Count > 0)
                    {
                        _pledgecustno = Static.ToDecimal(d1.Rows[0]["custno"]);
                        _doctype = Static.ToInt(d1.Rows[0]["doctype"]);
                        _docno = Static.ToStr(d1.Rows[0]["docno"]);
                        _custname = Static.ToStr(d1.Rows[0]["custname"]);
                        _contactno = Static.ToStr(d1.Rows[0]["contact"]);
                        _memo = Static.ToStr(d1.Rows[0]["memo"]);
                        _status = Static.ToInt(d1.Rows[0]["status"]);

                        _core.MainForm_HeaderSet(0, "Барьцаа №", pledgeno);
                        _core.MainForm_HeaderSet(1, "Төрөл", GetDocTypeName(_doctype));
                        _core.MainForm_HeaderSet(2, "Дугаар", _docno);
                        _core.MainForm_HeaderSet(3, "Овог нэр", _custname);
                        _core.MainForm_HeaderSet(4, "Утас", _contactno);

                        if (_status == 0)
                        {
                            _userno = Static.ToInt(d1.Rows[0]["holduser"]);
                            _udate = Static.ToDateTime(d1.Rows[0]["holddate"]);

                            txtStatus.ForeColor = Color.Red;
                            txtStatus.EditValue = string.Format("БАРЬЦААЛСАН ({0})", _udate.ToString("MM/dd HH:mm"));
                        }
                        else
                        {
                            _userno = Static.ToInt(d1.Rows[0]["unholduser"]);
                            _udate = Static.ToDateTime(d1.Rows[0]["unholddate"]);

                            txtStatus.ForeColor = Color.Green;
                            txtStatus.EditValue = string.Format("ЧӨЛӨӨЛСӨН ({0})", _udate.ToString("MM/dd HH:mm"));
                        }

                        DataTable dtUser = null;
                        ISM.Template.DictUtility.Get(_core.RemoteObject, "USERS", 601003, ref dtUser);

                        cboUser.EditValue = _userno;
                        ISM.Template.FormUtility.LookUpEdit_SetList(ref cboUser, dtUser, "USERNO", "USERFNAME");
                    }

                    #endregion
                    #region Pledge customers

                    _custs = res.Data.Tables[1];
                    BuildCustList(_custs);

                    #endregion
                    #region Pledge rent items

                    _rents = res.Data.Tables[2];
                    BuildRentList(_rents, 0);

                    #endregion

                }
                else
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(601901, ex.ToString());
            }
        OnExit:
            //ISM.Template.FormUtility.ValidateQuery(res);
            Alert(res, "Барьцаа хайх");
            return res;
        }
        public Result ReadRecordByTag(string tagno)
        {
            Result res = null;
            try
            {
                #region Validation
                if (string.IsNullOrEmpty(tagno))
                {
                    res = new Result(6010031, "Тагны дугаар буруу байна!");
                    goto OnExit;
                }
                #endregion
                #region Prepare parameters
                object[] param = new object[] { tagno };
                #endregion
                #region Call server
                if (_core != null && _core.RemoteObject != null)
                {
                    res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601007, 601003, param);
                    if (res.ResultNo != 0) goto OnExit;

                    if (res.AffectedRows > 0)
                    {
                        DataTable dt = res.Data.Tables[0];
                        _pledgeno = Static.ToStr(dt.Rows[0]["pledgeno"]);
                        res = ReadRecord(_pledgeno);
                    }
                    else
                    {
                        res = new Result(601004, string.Format("[{0}] тагын дугаар дээр барьцаа бүртгэгдээгүй байна!", tagno));
                    }

                }
                else
                {
                    res = new Result(601900, "Internal Error: Remote object not set.");
                }
                #endregion
            }
            catch (Exception ex)
            {
                res = new Result(601901, ex.ToString());
            }
        OnExit:
            //ISM.Template.FormUtility.ValidateQuery(res);
            Alert(res, "Тагаар хайх");
            return res;
        }

        public void BuildClear()
        {
            #region Pledge main details

            _pledgeno = null;
            _pledgecustno = 0;
            _doctype = 0;
            _docno = "";
            _custname = "";
            _contactno = "";
            _memo = "";
            _status = 0;

            _userno = 0;
            _udate = DateTime.MinValue;

            _core.MainForm_HeaderSet(0, "Барьцаа №", "");
            _core.MainForm_HeaderSet(1, "Төрөл", "");
            _core.MainForm_HeaderSet(2, "Дугаар", "");
            _core.MainForm_HeaderSet(3, "Овог нэр", "");
            _core.MainForm_HeaderSet(4, "Утас", "");

            txtStatus.EditValue = null;
            cboUser.EditValue = null;

            #endregion
            #region Pledge customers

            BuildCustList(null);

            #endregion
            #region Pledge rent items

            BuildRentList(null, 0);

            #endregion
        }
        public void BuildGridFormat(DevExpress.XtraGrid.Views.Grid.GridView gridView1)
        {
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            //gridView1.OptionsView.ShowColumnHeaders = false;
            //gridView1.OptionsView.ShowVertLines = false;

            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            gridView1.OptionsView.ColumnAutoWidth = false;
            //gridView1.OptionsView.ShowAutoFilterRow = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10.0F);

            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            gridView1.RowHeight = 28;
        }
        public void BuildCustList(DataTable dt)
        {
            gridControl2.DataSource = null;

            if (dt == null || dt.Rows.Count <= 0) return;

            gridControl2.DataSource = dt;

            #region  Add picture column
            
            //gridControl2.RepositoryItems.Clear();
            //RepositoryItemPictureEdit ri = new RepositoryItemPictureEdit();
            //gridControl1.RepositoryItems.Add(ri);
            
            //DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
            //col.VisibleIndex = gridView1.Columns.Count;
            //col.Caption = "Үйлдэл";
            //col.FieldName = string.Format("PIC");
            //col.ColumnEdit = ri;
            //col.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            //col.OptionsColumn.ReadOnly = true;
            //col.Width = 32;

            //gridView2.Columns.Add(col);

            #endregion
            #region Column formatting

            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 0, "Харилцагчийн №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 1, "Регистр", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 2, "Нэр, овог", false);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 3, "Таг", false);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView2, 4, "Хариуцагч", true);

            gridView2.Columns[2].Width = (int)(gridControl2.Width * 0.68);
            gridView2.Columns[3].Width = (int)(gridControl2.Width * 0.3);
            gridView2.RowHeight = 28;

            #endregion
        }
        public void BuildRentList(DataTable dt, decimal custno)
        {
            gridControl1.DataSource = null;

            if (dt == null || dt.Rows.Count <= 0) return;

            //var query = from row in dt.AsEnumerable()
            //            where row.Field<decimal>("CUSTNO") == custno
            //            orderby row.Field<string>("CUSTNAME")
            //            select row;
            //tmp = query.CopyToDataTable();
            ISM.Template.FormUtility.GridLayoutGet(gridView1, dt, _layoutfilename);
            #region Add picture column

            //gridControl1.RepositoryItems.Clear();
            //RepositoryItemButtonEdit ri = new RepositoryItemButtonEdit();
            //gridControl1.RepositoryItems.Add(ri);

            //DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
            //col.VisibleIndex = gridView1.Columns.Count;
            //col.Caption = "Төлбөр";
            //col.FieldName = string.Format("PIC");
            //col.ColumnEdit = ri;
            //col.UnboundType = DevExpress.Data.UnboundColumnType.String;
            //col.OptionsColumn.ReadOnly = true;
            //col.Width = 32;
            //gridView1.Columns.Add(col);

            #endregion
            #region Column formatting

            gridView1.Columns[6].ColumnEdit = ri_checkbox;

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 0, "Барьцаа №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 1, "Харилцагч №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 2, "Үйлчлүүлэгч");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 3, "Дэс №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 4, "Хэрэгслийн №", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 5, "Хэрэгслийн нэр");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 6, "Торгууль");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 7, "Илүү цаг (мин)");
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 8, "Төлөв");

            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 9, "SalesNo", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 10, "DamageNote", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView1, 11, "RentStatus", true);

            //gridView1.BestFitColumns();

            #endregion
        }
        public void CustomerAdd(string pledgeno, decimal pledgecustno, decimal custno)
        {
            Result res = null;
            #region Validation
            if (pledgecustno == 0)
            {
                res = new Result(601001, "Барьцаа үүсгэсэн үндсэн харилцагч сонгогдоогүй байна!");
                goto OnExit;
            }
            if (custno == 0)
            {
                res = new Result(601002, "Барьцаанд хамрагдах харилцагч сонгогдоогүй байна!");
                goto OnExit;
            }
            #endregion
            #region Prepare parameters
            object[] param = new object[] { pledgeno, pledgecustno, custno };
            #endregion
            #region Call server
            if (_core != null && _core.RemoteObject != null)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601005, 601005, param);
                if (res.ResultNo != 0) goto OnExit;

                res = ReadRecord(_pledgeno);
            }
            else
            {
                res = new Result(99999, "Internal Error: Remote object not set.");
            }
            #endregion
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }
        public void CustomerDel(string pledgeno, decimal pledgecustno, decimal custno)
        {
            Result res = null;
            #region Validation
            if (pledgecustno == 0)
            {
                res = new Result(6010061, "Барьцаа үүсгэсэн үндсэн харилцагч сонгогдоогүй байна!");
                goto OnExit;
            }
            if (custno == 0)
            {
                res = new Result(6010062, "Барьцаанд хамрагдсан харилцагч сонгогдоогүй байна!");
                goto OnExit;
            }
            #endregion
            #region Prepare parameters
            object[] param = new object[] { pledgeno, pledgecustno, custno };
            #endregion
            #region Call server
            if (_core != null && _core.RemoteObject != null)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601006, 601006, param);
                if (res.ResultNo != 0) goto OnExit;

                res = ReadRecord(_pledgeno);
            }
            else
            {
                res = new Result(99999, "Internal Error: Remote object not set.");
            }
            #endregion
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
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

                //res = ReadRecord(_pledgeno);
                DataRow[] rows =  _custs.Select (string.Format("CUSTNO='{0}'",custno));
                if (rows!=null && rows.Length >0) rows[0]["SERIALNO"] = serialno;
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
        public Result TagLinkDel(decimal custno, string serialno)
        {
            Result res = null;
            #region Validation
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
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601009, 601009, param);
                if (res.ResultNo != 0) goto OnExit;

                //res = ReadRecord(_pledgeno);
                DataRow[] rows =  _custs.Select (string.Format("CUSTNO='{0}'",custno));
                if (rows!=null && rows.Length >0) rows[0]["SERIALNO"] = "";
            }
            else
            {
                res = new Result(99999, "Internal Error: Remote object not set.");
            }
            #endregion
        OnExit:
            //ISM.Template.FormUtility.ValidateQuery(res);
            //Alert(res, "Таг салгах");
            return res;
        }

        public void PledgeDel(string pledgeno)
        {
            Result res = null;
            #region Validation1 - Барьцаа сонгогдсон эсэх
            if (string.IsNullOrEmpty(pledgeno))
            {
                res = new Result(6010101, "Барьцааны бүртгэл сонгогдоогүй байна!");
                goto OnExit;
            }
            #endregion
            #region Validation2 - Борлуулалтын төлбөр төлөгдсөн эсэх

            
            
            #endregion
            #region Validation3 - Түрээсийн хэрэгслүүд олгогдсон эсэх

            if (_rents != null)
            {
                foreach (DataRow r in _rents.Rows)
                {
                    int status = Static.ToInt(r["STATUS"]); //rentstatus
                    int fined = Static.ToInt(r["FINED"]); //rentstatus
                    if (status == 1 || fined == 1)
                    {
                        res = new Result(6010103, "Түрээсийн хэрэгслийг бүрэн хүлээж аваагүй эсвэл төлбөр дутуу байна!");
                        goto OnExit;
                    }
                }
            }

            #endregion
            #region Confirmation

            string confirm = string.Format("[{0}] дугаартай барьцааг чөлөөлөхдөө итгэлтэй байна уу?", _docno);
            if (!ISM.Template.FormUtility.ValidateConfirm(confirm))
            {
                goto OnExit;
            }

            #endregion
            #region Prepare parameters
            object[] param = new object[] { pledgeno };
            #endregion
            #region Call server
            if (_core != null && _core.RemoteObject != null)
            {
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 601, 601010, 601010, param);
                if (res.ResultNo != 0) goto OnExit;

                BuildClear();
            }
            else
            {
                res = new Result(99999, "Internal Error: Remote object not set.");
            }
            #endregion
        OnExit:
            ISM.Template.FormUtility.ValidateQuery(res);
        }

        public void Alert(Result res, string caption)
        {
            if (res != null && res.ResultNo != 0)
            {
                if (_core!=null)
                {
                    _core.AlertShow(caption, res.ResultDesc);
                }
            }
        }
        
        #endregion
        #region Control Events

        public frmReg()
        {
            InitializeComponent();

            BuildGridFormat(gridView1);
            BuildGridFormat(gridView2);
            _layoutfilename = string.Format(@"{0}\Data\Layout_{1}.xml", Static.WorkingFolder, this.GetType().Name);

            ri_checkbox = new RepositoryItemCheckEdit();
            ri_checkbox.AllowGrayed = false;
            ri_checkbox.ValueChecked = (decimal)1;
            ri_checkbox.ValueUnchecked = (decimal)0;
            ri_checkbox.ValueGrayed = (decimal)2;
            
            //ri_checkbox.QueryCheckStateByValue += ri_checkbox_QueryCheckStateByValue;
            gridControl1.RepositoryItems.Add(ri_checkbox);

            this.gridView1.RowStyle += gridView1_RowStyle;

            //gridView2.FocusedRowChanged += gridView2_FocusedRowChanged;
            this.FormClosing += frmRegMain_FormClosing;
        }

        private void ri_checkbox_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            
        }

        private void frmRegMain_Load(object sender, EventArgs e)
        {
            _core.MainForm_HeaderClear();
            _core.MainForm_HeaderSet(0, "Барьцаа №", "");
            _core.MainForm_HeaderSet(1, "Төрөл", "");
            _core.MainForm_HeaderSet(2, "Дугаар", "");
            _core.MainForm_HeaderSet(3, "Овог нэр", "");
            _core.MainForm_HeaderSet(4, "Утас", "");

            txtStatus.EditValue = "";
            cboUser.EditValue = null;
        }

        void frmRegMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ISM.Template.FormUtility.GridLayoutSave(gridView1, _layoutfilename);
        }
        void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //DataRow r = gridView2.GetDataRow(e.FocusedRowHandle);
            //if (r != null)
            //{
            //    decimal custno = Static.ToDecimal(r["custno"]);
            //    BuildRentList(_rents, custno);
            //}
        }
        void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                DataRow r = gridView1.GetDataRow(e.RowHandle);
                if (r != null)
                {
                    int fined = Static.ToInt(r["FINED"]);
                    int rentstatus = Static.ToInt(r["STATUS"]);

                    if (rentstatus >= 3)
                    {
                        e.Appearance.BackColor = Color.LimeGreen;
                        e.Appearance.BackColor2 = Color.White;
                        e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
                    }
                    else if (fined == 1)
                    {
                        e.Appearance.BackColor = Color.Tomato;
                        e.Appearance.BackColor2 = Color.White;
                        e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
                    }
                    else if (rentstatus == 1)
                    {
                        e.Appearance.BackColor = Color.Goldenrod;
                        e.Appearance.BackColor2 = Color.White;
                        e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.White;
                        e.Appearance.BackColor2 = Color.White;
                        e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
                    }
                }
            }
            catch
            { }
        }
        #endregion
    }
}
