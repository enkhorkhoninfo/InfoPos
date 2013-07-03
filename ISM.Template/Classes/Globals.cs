using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;

namespace ISM.Template
{
    public enum DynamicParameterType
    {
        #region Types
        Text = 0,
        Decimal = 1,
        Date = 2,
        DateTime = 3,
        File = 4,
        Folder = 5,
        Picture = 6,
        Font = 7,
        Color = 8,
        List = 9,
        Register = 10
        #endregion
    }
    public class DynamicParameterItem
    {
        #region Variables

        public string Id;
        public string Name;
        public string Name2;
        public object Value;
        public DynamicParameterType ValueType;
        public int ValueLength;
        public object ValueDefault;
        public bool Mandatory;
        public string EditMask;
        public string Description;
        public string DictId;
        public bool DictEditable;
        public string DictValueField;
        public string DictDescField;
        public int Orderno;

        public EditorRow Row = null;
        public ulong AttachId; // Used for Picture, File object
        public byte[] AttachData; // Used for Picture, File object
        public bool Editing = false;

        #endregion

        #region Constractors

        public DynamicParameterItem()
        {
        }

        public DynamicParameterItem(string Id, string Name, string Name2, object Value, DynamicParameterType ValueType, int ValueLength, object ValueDefault
            , bool Mandatory, string EditMask, string Description, string DictId, bool DictEditable, string DictValueField, string DictDescField, int Orderno)
        {
            #region Setting Values
            this.Id = Id;
            this.Name = Name;
            this.Name2 = Name2;
            this.Value = Value;
            this.ValueType = ValueType;
            this.ValueLength = ValueLength;
            this.ValueDefault = ValueDefault;
            this.Mandatory = Mandatory;
            this.EditMask = EditMask;
            this.Description = Description;
            this.DictId = DictId;
            this.DictEditable = DictEditable;
            this.DictValueField = DictValueField;
            this.DictDescField = DictDescField;
            this.Orderno = Orderno;
            #endregion
        }
        public DynamicParameterItem(string Id, string Name, object Value, DynamicParameterType ValueType, int ValueLength, object ValueDefault
            , bool Mandatory, string EditMask, string Description)
            : this(Id, Name, null, Value, ValueType, ValueLength, ValueDefault, Mandatory, EditMask, Description, null, true, null, null, 0)
        {
        }
        public DynamicParameterItem(string Id, string Name, object Value, DynamicParameterType ValueType, bool Mandatory, string EditMask, string Description)
            : this(Id, Name, null, Value, ValueType, 4000, null, Mandatory, EditMask, Description, null, true, null, null, 0)
        {
        }
        public DynamicParameterItem(string Id, string Name, object Value, DynamicParameterType ValueType, int ValueLength, bool Mandatory)
            : this(Id, Name, null, Value, ValueType, ValueLength, null, Mandatory, null, null, null, true, null, null, 0)
        {
        }
        public DynamicParameterItem(string Id, string Name, object Value, DynamicParameterType ValueType, bool Mandatory)
            : this(Id, Name, null, Value, ValueType, 4000, null, Mandatory, null, null, null, true, null, null, 0)
        {
        }

        #endregion
    }

    public class Globals
    {
        static public string GetFileName(string fullname)
        {
            string fname = fullname;
            int pos = fname.LastIndexOf('\\');
            if (pos > 0) fname = fname.Substring(pos, fname.Length - pos);
            return fname;
        }
        static public void ShellOpenFile(string fullname, byte[] buffer)
        {
            string fname = GetFileName(fullname);
            fname = Path.GetTempPath() + "\\" + fname;
            FileStream stream = new FileStream(fname, FileMode.Create, FileAccess.Write);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            stream.Close();

            Process p = new Process();
            p.StartInfo.FileName = fname;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
        }
        static public void ShellOpenFile(string fullname)
        {
            Process p = new Process();
            p.StartInfo.FileName = fullname;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
        }
    }
}
