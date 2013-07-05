using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using EServ.Shared;
using System.Text;

namespace InfoPos.Parameter
{
    class FunctionParam
    {
        public void SetCombo(Core.Core _core, string ID, string FieldValue, string FieldName, int PrivNo, DevExpress.XtraEditors.LookUpEdit LEdit, string Filter, int[] Hide)
        {
            Result res = new Result();
            DataTable DT = null;
            string msg = "";

            ISM.Template.DictUtility.PrivNo = PrivNo;

            res = ISM.Template.DictUtility.Get(_core.RemoteObject, ID, ref DT);

            if (res.ResultNo == 0)
            {
                ISM.Template.FormUtility.LookUpEdit_SetList(ref LEdit, DT, FieldValue, FieldName, Filter, Hide);
            }
            else
            {
                msg = "Dictionary-д оруулаагүй байна-" + res.ResultDesc;
                MessageBox.Show(msg);
            }
        }
        public void SetCombo(Core.Core _core, string ID, string FieldValue, int PrivNo, DevExpress.XtraEditors.LookUpEdit LEdit, string Filter, int[] Hide)
        {
            Result res = new Result();
            DataTable DT = null;
            string msg = "";

            ISM.Template.DictUtility.PrivNo = PrivNo;

            res = ISM.Template.DictUtility.Get(_core.RemoteObject, ID, ref DT);

            if (res.ResultNo == 0)
            {
                ISM.Template.FormUtility.LookUpEdit_SetList(ref LEdit, DT, FieldValue, Filter, Hide);
            }
            else
            {
                msg = "Dictionary-д оруулаагүй байна-" + res.ResultDesc;
                MessageBox.Show(msg);
            }
        }
        public void SetComboSub(Core.Core _core, string ID, string FieldValue, string FieldName, int PrivNo, DevExpress.XtraEditors.LookUpEdit LEdit, string Filter, int[] Hide)
        {
            DataTable DT = null;
            string msg = "";
            Result res = new Result();
            ISM.Template.DictUtility.PrivNo = PrivNo;

            res = ISM.Template.DictUtility.Get(_core.RemoteObject, ID, ref DT);
            if (res.ResultNo == 0)
            {
                ISM.Template.FormUtility.LookUpEdit_SetList(ref LEdit, DT, FieldValue, FieldName, Filter, Hide);
            }
            else
                msg = "Dictionary-д оруулаагүй байна-" + res.ResultDesc;
        }
    }
}
