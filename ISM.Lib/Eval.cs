using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ISM.Lib
{
    public class Eval
    {
        static public object Run(string FunctionName, string SourceBody, DataTable SourceValues, object ThisObject, string[] ReferenceNames, object[] ReferenceObjects)
        {
            object ret = null;
            try
            {
                if (string.IsNullOrEmpty(FunctionName)) FunctionName = "Macro";

                #region Preparing source code and values

                int count = 0;
                if (SourceValues != null) count = SourceValues.Columns.Count;
                object[] param = new object[count];

                StringBuilder sb = new StringBuilder();
                sb.Append("Function ");
                sb.Append(FunctionName);
                sb.Append("(");

                if (SourceValues != null)
                {
                    for (int i = 0; i < SourceValues.Columns.Count; i++)
                    {
                        DataColumn col = SourceValues.Columns[i];
                        if (i > 0) sb.Append(",");
                        sb.Append(SourceValues.Columns[i].ColumnName);

                        if (SourceValues.Rows.Count > 0) param[i] = SourceValues.Rows[0][i];
                    }
                }

                sb.AppendLine(")");
                sb.AppendLine(SourceBody);
                sb.Append("End Function");

                #endregion

                #region Creating Script Instance
                                
                MSScriptControl.ScriptControlClass scriptcontrol = new MSScriptControl.ScriptControlClass();
                scriptcontrol.AllowUI = true;
                scriptcontrol.Language = "VBScript";
                scriptcontrol.Timeout = 300000;

                #endregion

                #region Referencing Additional Objects

                if (ThisObject != null)
                    scriptcontrol.AddObject("This", ThisObject, false);

                if (ReferenceObjects != null)
                {
                    string referencename = "";
                    for (int i = 0; i < ReferenceObjects.Length; i++)
                    {
                        if (ReferenceObjects[i] != null)
                        {
                            if (ReferenceNames != null && i < ReferenceNames.Length) referencename = ReferenceNames[i];
                            else referencename = ReferenceObjects[i].GetType().Name;

                            scriptcontrol.AddObject(referencename, ReferenceObjects[i], false);
                        }
                    }
                }

                #endregion

                #region Run Dynamic Function

                scriptcontrol.AddCode(sb.ToString());
                ret = scriptcontrol.Run(FunctionName, param);

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        static public object Run(string SourceBody, DataTable SourceValues, object ThisObject, string[] ReferenceNames, object[] ReferenceObjects)
        {
            return Run("", SourceBody, SourceValues, ThisObject, ReferenceNames, ReferenceObjects);
        }
        static public object Run(string SourceBody, DataTable SourceValues, object ThisObject)
        {
            return Run(SourceBody, SourceValues, ThisObject, null, null);
        }
        static public object Run(string SourceBody, DataTable SourceValues)
        {
            return Run(SourceBody, SourceValues, null, null, null);
        }
        static public object Run(string SourceBody)
        {
            return Run(SourceBody, null, null, null, null);
        }
    }
}
