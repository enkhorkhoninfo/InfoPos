using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using EServ.Shared;

namespace InfoPos.Panels
{
    public partial class frmRentDeliver : DevExpress.XtraEditors.XtraForm
    {
        InfoPos.Core.Core _core = null;
        ISM.Touch.TouchKeyboard _touchkeyboard = null;

        string _salesno = null;
        int _itemno = 0;
        string _invcode = null;
        string _invname = null;
        int _userno = 0;
        int _userstate = 0;

        #region Constructor

        public frmRentDeliver(InfoPos.Core.Core core, ISM.Touch.TouchKeyboard kb, string salesno, int itemno, string invcode, string invname,int userno,int userstate)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmRentDeliver_FormClosing);

            this.ucSearchList1.KeyFieldIndex = 1;
            this.ucSearchList1.EventChoose += new ucSearchList.delegateEventChoose(ucSearchList1_EventChoose);
            this.ucSearchList1.EventDataSourceChanged += new ucSearchList.delegateEventDataSourceChanged(ucSearchList1_EventDataSourceChanged);

            _core = core;
            _touchkeyboard = kb;

            _salesno = salesno;
            _itemno = itemno;
            _invcode = invcode;
            _invname = invname;
            _userno = userno;
            _userstate = userstate;
            ucSearchList1.TouchKeyboard = kb;
        }

        void ucSearchList1_EventDataSourceChanged(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            ISM.Template.FormUtility.Column_SetCaption(ref gridView, 0, "Барааны код", true);
            ISM.Template.FormUtility.Column_SetCaption(ref gridView, 1, "Серийн дугаар");
        }

        void ucSearchList1_EventChoose(DataRow currentrow)
        {
            
        }

        void frmRentDeliver_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //_core.BarCoder.Close();
            }
            catch
            { }
        }

        #endregion

        #region Control Events

        private void frmRentDeliver_Load(object sender, EventArgs e)
        {
            txtInvName.EditValue = _invname;

            InitTable();
            ucSearchList1.SelectedControl();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            ServerCall();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region User Functions

        public void InitTable()
        {
            DataTable dt = new DataTable();
            Result res = ISM.Template.DictUtility.Get(_core.RemoteObject.Connection, _core.RemoteObject.User.UserNo, "INVSERIES", 500101, ref dt);
            if (ISM.Template.FormUtility.ValidateQuery(res))
            {
                try
                {
                    var query = from row in dt.AsEnumerable()
                                where row.Field<string>("INVID") == _invcode
                                orderby row.Field<string>("INVID")
                                select row;
                    if (query != null)
                    {
                        if (query.Count() != 0)
                        {
                            ucSearchList1.DataSource = query.CopyToDataTable();
                            dt.Columns[0].Caption = "Барааны код";
                            dt.Columns[1].Caption = "Серийн дугаар";
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public string BarcodeRead()
        {
            return ""; // _core.BarCoder.Read();
        }
        public void ServerCall()
        {
            Result res = null;

            #region Prepare parameters
            if (ucSearchList1.CurrentRow != null)
            {
                string barcode = Static.ToStr(ucSearchList1.CurrentRow["barcode"]);
                object[] param = new object[]
                { 
                    _salesno
                    , _invcode
                    , _itemno
                    , barcode
                    , DateTime.Now //_core.TxnDate -энд утга орж ирэхгүй бн, шалгах!
                    , 1 //1-Олгосон, 2-Хүлээн авсан ,9 Эвдэрсэн.
                    , _userno
                    , _userstate
                };
            #endregion
                res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo
                    , 500, 500101, 500101, param);

                if (ISM.Template.FormUtility.ValidateQuery(res))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
        }

        #endregion
    
        private void frmRentDeliver_MouseMove(object sender, MouseEventArgs e)
        {
            ucSearchList1.SelectedControl();
        }
        private void frmRentDeliver_MouseClick(object sender, MouseEventArgs e)
        {
            ucSearchList1.SelectedControl();
        }

    }
}