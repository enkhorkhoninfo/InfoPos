using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Vsa;
using System.Runtime.InteropServices;

namespace ISM.Lib
{
    public class Vsa
    {
        static public object RunVsa(string FunctionName, string SourceBody, DataTable SourceValues, object ThisObject, string[] ReferenceNames, object[] ReferenceObjects)
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
                if (SourceValues != null)
                {
                    for (int i = 0; i < SourceValues.Columns.Count; i++)
                    {
                        DataColumn col = SourceValues.Columns[i];
                        if (i > 0) sb.Append(",");
                        sb.Append(SourceValues.Columns[i].ColumnName);
                        sb.Append(" As Object");

                        if (SourceValues.Rows.Count > 0) param[i] = SourceValues.Rows[0][i];
                    }
                }

                string body = string.Format(@"Option Strict Off
imports System
imports System.Diagnostics
module Script
    Function {0}({1}) as Object
    {2}
    end Function
end module
", FunctionName, sb.ToString(), SourceBody);

                #endregion

                #region Creating Script Instance

                ScriptingHost vsa = new ScriptingHost();
                vsa.FunctionName = FunctionName;
                vsa.SetScript(body);

                #endregion

                #region Referencing Additional Objects

                if (ThisObject != null)
                {
                    string loadingassemble = Assembly.GetAssembly(ThisObject.GetType()).Location;
                    vsa.AddReference(loadingassemble);
                    vsa.AddGlobalObject("This", ThisObject);
                }
                if (ReferenceObjects != null)
                {
                    string referencename = "";
                    for (int i = 0; i < ReferenceObjects.Length; i++)
                    {
                        if (ReferenceObjects[i] != null)
                        {
                            if (ReferenceNames != null && i < ReferenceNames.Length) referencename = ReferenceNames[i];
                            else referencename = ReferenceObjects[i].GetType().Name;

                            vsa.AddReference(referencename);
                        }
                    }
                }

                #endregion

                #region Run Dynamic Function

                ret = vsa.Run(FunctionName, param);

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }
        static public object RunVsa(string SourceBody, DataTable SourceValues, object ThisObject, string[] ReferenceNames, object[] ReferenceObjects)
        {
            return RunVsa("", SourceBody, SourceValues, ThisObject, ReferenceNames, ReferenceObjects);
        }
        static public object RunVsa(string SourceBody, DataTable SourceValues, object ThisObject)
        {
            return RunVsa(SourceBody, SourceValues, ThisObject, null, null);
        }
        static public object RunVsa(string SourceBody, DataTable SourceValues)
        {
            return RunVsa(SourceBody, SourceValues, null, null, null);
        }
        static public object RunVsa(string SourceBody)
        {
            return RunVsa(SourceBody, null, null, null, null);
        }
    }
    public class ScriptingHost : IVsaSite, IDisposable
    {
        #region Private Fields

        private IVsaEngine _engine = null;
        private string _scriptText = "";

        private Hashtable _globalObs = new Hashtable();
        private List<ReferenceItem> _references = new List<ReferenceItem>();
        private List<GlobalObItem> _globalObItems = new List<GlobalObItem>();
        private ExternalException _exception = null;

        private static int _instanceID = 0;
        private object _lockOb = new object();

        private bool _disposed = false;
        private string _workingDirectory;
        private bool _isInitialized = false;

        #endregion
        #region Constructors and Finalizer

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ScriptingHost()
        {
            string s = Assembly.GetExecutingAssembly().Location;
            int i = s.LastIndexOf(@"\");
            if (i > 0) _workingDirectory = s.Substring(0, i);
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~ScriptingHost()
        {
            Dispose(false);
        }

        #endregion
        #region Public Properties

        /// <summary>
        /// Gets or sets the working directory where referenced DLLs are located.
        /// </summary>
        /// <remarks>This only has meaning for VBScript. It's not used in JScript.</remarks>
        public string WorkingDirectory
        {
            get
            {
                return _workingDirectory;
            }
            set
            {
                if (IsCompiled)
                {
                    throw new InvalidOperationException("You can't set the working directory after a script has been compiled.");
                }
                if (!Directory.Exists(value))
                {
                    throw new DirectoryNotFoundException("The working directory does not exist.");
                }
                _workingDirectory = value;
            }
        }

        /// <summary>
        /// Gets a unique ID for the script.
        /// </summary>
        public int NewInstanceID
        {
            get
            {
                lock (_lockOb)
                {
                    return ++_instanceID;
                }
            }
        }

        /// <summary>
        /// Whether or not the script has been compiled.
        /// </summary>
        /// <remarks>
        /// For multiple executions of the same script, there's no need to recompile.
        /// </remarks>
        public bool IsCompiled
        {
            get
            {
                if (_engine == null)
                {
                    return false;
                }
                return _engine.IsCompiled;
            }
        }

        /// <summary>
        /// Whether or not the script is currently executing.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (_engine == null)
                {
                    return false;
                }

                return _engine.IsRunning;
            }
        }

        private string functionname = "Macro";
        public string FunctionName
        {
            get { return functionname; }
            set { functionname = value; }
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// Loads the script from a file.
        /// </summary>
        /// <param name="filename">File to open.</param>
        public void LoadScriptFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Unable to find the script file.", filename);
            }

            string ext = Path.GetExtension(filename);
            if (ext == ".vb" || ext == ".vbe" || ext == ".vbs")
            {
                //language = ScriptingLanguage.VBScript;
            }
            using (StreamReader sr = new StreamReader(filename))
            {
                string scriptText = sr.ReadToEnd();
                SetScript(scriptText);
            }
        }

        /// <summary>
        /// Sets the script from a string
        /// </summary>
        /// <param name="scriptText">Script text</param>
        /// <param name="language">Script language</param>
        public void SetScript(string scriptText)
        {
            _isInitialized = false;
            // Make sure the last allocated engine is stopped.
            if (_engine != null)
            {
                _engine.Close();
            }

            _scriptText = scriptText;

            // Initialize globale vars
            _globalObs.Clear();

            _engine = new Microsoft.VisualBasic.Vsa.VsaEngine();

            // Initialize Engine
            _engine.RootMoniker = "nScriptHost://VSAScript/Instance" + NewInstanceID.ToString();
            _engine.Site = this;
            _engine.InitNew();
            _engine.RootNamespace = "__Script__";

            _references.Clear();
            _globalObItems.Clear();

            AddDefaultReferences();

            // Set the script code.
            IVsaCodeItem item = _engine.Items.CreateItem("Code", VsaItemType.Code, VsaItemFlag.None) as IVsaCodeItem;
            item.SourceText = _scriptText;
        }


        /// <summary>
        /// Adds a reference from the GAC.
        /// </summary>
        /// <param name="assemblyName">Name of assembly.</param>
        public void AddReference(string assemblyName)
        {
            AddReference("", assemblyName);
        }

        /// <summary>
        /// Adds a reference from a file path.
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="assemblyName">Name of assembly file</param>
        public void AddReference(string path, string assemblyName)
        {
            ReferenceItem refItem = new ReferenceItem(path, assemblyName);
            _references.Add(refItem);
        }

        /// <summary>
        /// Makes a managed object available to scripts.
        /// </summary>
        /// <param name="variableName">Name of globale variable to assign to object.</param>
        /// <param name="globalOb">The object.</param>
        public void AddGlobalObject(string variableName, object globalOb)
        {
            GlobalObItem item = new GlobalObItem(variableName, globalOb);
            _globalObItems.Add(item);
        }

        /// <summary>
        /// Compiles the script
        /// </summary>
        /// <returns><b>true</b> if compilation was successful, otherwise <b>false</b>.</returns>
        public bool Compile()
        {
            if (!_isInitialized)
            {
                InitializeEngine();
            }
            _exception = null;
            bool success = _engine.Compile();
            if (!success) throw _exception;

            return success;
        }

        /// <summary>
        /// Compiles (if necessary) the script and executes it.
        /// </summary>
        public object Run(string funcname, object[] param)
        {
            if (IsRunning)
            {
                return null;
            }
            if (!IsCompiled)
            {
                bool success = Compile();
            }
            return InternalRun(funcname, param);
        }
        public object Run()
        {
            return Run(functionname, null);
        }

        #endregion
        #region IVsaSite Members

        /// <summary>
        ///  Returns the Event Source
        /// </summary>
        /// <remarks>
        /// We don't handle this yet. I think what we'll need to do is process this through
        /// an event handler.
        /// </remarks>
        /// <param name="itemName"></param>
        /// <param name="eventSourceName"></param>
        /// <returns></returns>
        public object GetEventSourceInstance(string itemName, string eventSourceName)
        {
            Debug.WriteLine("GetEventSourceInstance for " + itemName + "  eventSource - " + eventSourceName);
            return null;
        }

        /// <summary>
        /// When the engine calls back the IVsaSite to ask for a global item, return the instance if we've cached it previously
        /// </summary>
        /// <param name="name">The name of the global item to which an object instance is requested</param>
        /// <returns></returns>
        public object GetGlobalInstance(string name)
        {
            Debug.WriteLine("GetGlobalInstance for " + name);
            if (_globalObs.ContainsKey(name))
            {
                return _globalObs[name];
            }
            Debug.WriteLine("Unable to find Global Instance of " + name.ToString());
            return null;
        }

        /// <summary>
        /// Notifications about events generated by the engine.
        /// </summary>
        /// <param name="notify"></param>
        /// <param name="info"></param>
        public void Notify(string notify, object info)
        {
            Debug.WriteLine("Notify: " + notify + " - info Object Type: " + info.GetType().Name);
        }

        /// <summary>
        /// Called when there's a compiler error.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool OnCompilerError(IVsaError error)
        {
            _exception = new ExternalException(error.Description, error.Number);
            _exception.Source = error.LineText;

            return true;
        }

        /// <summary>
        /// We don't deal with compile states..
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="debugInfo"></param>
        public void GetCompiledState(out byte[] pe, out byte[] debugInfo)
        {
            pe = null;
            debugInfo = null;
        }

        #endregion
        #region IDisposable Members

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                    if (_engine != null)
                    {
                        _engine.Close();
                    }
                }

                _disposed = true;
            }
        }

        #endregion
        #region Private Methods
        
        private void InitializeEngine()
        {
            _isInitialized = true;
            // Add our global objects
            foreach (GlobalObItem item in _globalObItems)
            {
                InternalAddGlobalObject(item.VariableName, item.GlobalOb);
            }

            _engine.SetOption("ApplicationBase", _workingDirectory);

            // Add reference items
            foreach (ReferenceItem item in _references)
            {
                // VB uses ApplicationBase, not a path.
                InternalAddReference("", item.AssemblyName);
            }
        }

        /// <summary>
        /// Adds the reference item to the engine.
        /// </summary>
        /// <param name="path">Path to reference.</param>
        /// <param name="assemblyName">AssemblyName of reference.</param>
        private void InternalAddReference(string path, string assemblyName)
        {
            if (path.Trim().Length > 0 && !path.EndsWith(@"\"))
            {
                path += @"\";
            }
            path += assemblyName;
            Debug.Assert(_engine != null, "You cannot add references until a script has been set or loaded.");
            IVsaReferenceItem item = _engine.Items.CreateItem(path, VsaItemType.Reference, VsaItemFlag.None) as IVsaReferenceItem;
            item.AssemblyName = assemblyName;

        }

        /// <summary>
        /// Adds a global objec to the engine
        /// </summary>
        /// <param name="variableName">Name of variable.</param>
        /// <param name="globalOb">Object to add to Engine</param>
        private void InternalAddGlobalObject(string variableName, object globalOb)
        {
            Debug.Assert(_engine != null, "You cannot add global objects until a script has been set or loaded.");
            lock (_globalObs.SyncRoot)
            {
                if (!_globalObs.ContainsKey(variableName))
                    _globalObs.Add(variableName, globalOb);
            }

            IVsaGlobalItem item = _engine.Items.CreateItem(variableName, VsaItemType.AppGlobal, VsaItemFlag.None) as IVsaGlobalItem;
            item.TypeString = globalOb.GetType().FullName;
            //item.TypeString = globalOb.GetType().Name;
            //item.ExposeMembers = true;
        }
        
        /// <summary>
        /// Adds the default references.
        /// </summary>
        private void AddDefaultReferences()
        {
            AddReference("mscorlib.dll");
            AddReference("system.dll");
            AddReference("system.drawing.dll");
            AddReference("system.data.dll");
            AddReference("system.windows.forms.dll");
        }


        private object InternalRun(string funcname, object[] param)
        {
            _engine.Run();
            object ret = _engine.Assembly.GetType("__Script__.Script").GetMethod(funcname).Invoke(null, param);
            return ret;
        }

        #endregion
        #region Private Classes

        private class ReferenceItem
        {
            public string Path;
            public string AssemblyName;

            public ReferenceItem(string path, string assemblyName)
            {
                Path = path;
                AssemblyName = assemblyName;
            }
        }

        private class GlobalObItem
        {
            public string VariableName;
            public object GlobalOb;

            public GlobalObItem(string variableName, object globalOb)
            {
                VariableName = variableName;
                GlobalOb = globalOb;
            }
        }

        #endregion
    }
}
