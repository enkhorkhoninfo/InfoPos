using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

using DevExpress.XtraEditors;

using ISM.Template;
using EServ.Shared;

namespace InfoPos.Parameter
{
    public partial class frmDynReport : DevExpress.XtraEditors.XtraForm
    {
        #region[Variables]
        InfoPos.Core.Core _core;
        CodeNamespace mynamespace;
        CodeTypeDeclaration myclass;
        CodeCompileUnit myassembly;
        string path = "";
        string ininame = "";
        string rptname = "";
        #endregion
        #region[Constructure]
        public frmDynReport(InfoPos.Core.Core core)
        {
            InitializeComponent();
            _core = core;
            if (_core.Resource != null)
            {
                btnChoose.Image = _core.Resource.GetImage("button_find");
                btnChoose1.Image = _core.Resource.GetImage("button_find");
                btnCreate.Image = _core.Resource.GetImage("object_save");
                btnExit.Image = _core.Resource.GetImage("image_exit");
            }
            FormUtility.LookUpEdit_SetList(ref cbotype, 0, "Динамик тайлан");
            FormUtility.LookUpEdit_SetList(ref cbotype, 1, "Slips тайлан");
            cbotype.ItemIndex = 0;
            cbotype.Select();
        }
        private void frmDynReport_Load(object sender, EventArgs e)
        {
            Result res = new Result();
            txtFilePath.Text = _core.ReportPathIn;
            object[] obj = new object[3];
            obj[0] = _core.RemoteObject.User.UserNo;
            obj[1] = _core.TxnDate;
            obj[2] = null;
            res = _core.RemoteObject.Connection.Call(_core.RemoteObject.User.UserNo, 202, 140421, 140421, null);
            if (res.ResultNo == 0)
            {
                cboTxnCode.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                cboTxnCode.Properties.NullText = string.Empty;
                cboTxnCode.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                cboTxnCode.Properties.CharacterCasing = CharacterCasing.Upper;
                cboTxnCode.Properties.DataSource = res.Data.Tables[0];
                cboTxnCode.Properties.CaseSensitiveSearch = false;
                cboTxnCode.Properties.ShowHeader = false;
                cboTxnCode.Properties.ForceInitialize();
                cboTxnCode.Properties.PopulateColumns();
                cboTxnCode.Properties.ValueMember = "TRANCODE";
                cboTxnCode.Properties.DisplayMember = "NAME";
            }
            else
            {
                MessageBox.Show(res.ResultNo + " : " + res.ResultDesc);
            }
        }
        #endregion
        #region[Function]
        public void CreateNamespace(string name)
        {
            //Үүсгэж байгаа dll ийн namespace- н нэр-г тодорхойлох
            mynamespace = new CodeNamespace(name);
        }
        public void CreateImports()
        {
            mynamespace.Imports.Add(new CodeNamespaceImport("System"));
            mynamespace.Imports.Add(new CodeNamespaceImport("System.Drawing"));
            mynamespace.Imports.Add(new CodeNamespaceImport("System.Windows.Forms"));
        }
        public void CreateClass()
        {
            //Классаа үүсгэж байна
            myclass = new CodeTypeDeclaration("Resource");
            CompilerParameters cp = new CompilerParameters();

            //Классын функцийн base ийг тодорхойлж өгч байна.
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;
            constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("\"Resources\""));
            myclass.Members.Add(constructor);

            //Классаа ISM.Template.Resource-с удамшуулж байна.
            myclass.BaseTypes.Add("ISM.Template.Resource");
            myclass.IsClass = true;
            myclass.Attributes = MemberAttributes.Public;
            mynamespace.Types.Add(myclass);
        }
        public void CreateEntryPoint()
        {
            //Main функц үүсгэж байгаа хэсэг
            CodeEntryPointMethod mymain = new CodeEntryPointMethod();
            mymain.Name = "Main";

            //Функцийн атрибутыг тодорхойлж буй хэсэг
            mymain.Attributes = MemberAttributes.Public | MemberAttributes.Static;

            //Үүсгэсэн myClass даа Main функцээ тодорхойлж байна
            myclass.Members.Add(mymain);
        }
        public void SaveAssembly(string ininame, string rptname)
        {
            myassembly = new CodeCompileUnit();

            //namespace-ээ assembly -д оруулж байна
            myassembly.Namespaces.Add(mynamespace);

            //Ашиглагдах dll ээ оруулж ирж байна.(using...)
            CompilerParameters comparam = new CompilerParameters(new string[] { "mscorlib.dll", "ISM.Template.dll" });
            comparam.ReferencedAssemblies.Add("System.dll");
            comparam.ReferencedAssemblies.Add("System.Drawing.dll");
            comparam.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            comparam.ReferencedAssemblies.Add("ISM.Template.dll");

            comparam.GenerateInMemory = false;
            path = txtFilePath.EditValue.ToString() + "\\" + ininame;
            if (File.Exists(path))
            {
                comparam.EmbeddedResources.Add(path);
                path = "";
                path = txtFilePath.EditValue.ToString() + "\\" + rptname;
                if (File.Exists(path))
                {
                    comparam.EmbeddedResources.Add(path);
                }
                else
                {
                    MessageBox.Show(rptname + " файл олдсонгүй");
                    return;
                }
            }
            else
            {
                MessageBox.Show(ininame + " файл олдсонгүй");
                return;
            }

            comparam.GenerateExecutable = true;

            comparam.MainClass = "rep" + cboTxnCode.EditValue.ToString() + ".Resource";
            //Үүсгэх газрыг зааж өгч байна.
            comparam.OutputAssembly = _core.ReportPathIn + "\\rep" + cboTxnCode.EditValue.ToString() + ".dll";
            Microsoft.CSharp.CSharpCodeProvider ccp = new Microsoft.CSharp.CSharpCodeProvider();
            ICodeCompiler icc = ccp.CreateCompiler();

            //Ямар нэгэн алдаа гарвал мессэжбокс дээр хэвлэх үйлдэл явагдаж байна.
            CompilerResults compres = icc.CompileAssemblyFromDom(comparam, myassembly);

            if (compres == null || compres.Errors.Count > 0)
            {
                string error = "";
                for (int i = 0; i < compres.Errors.Count; i++)
                {
                    error = error + "\r\n" + compres.Errors[i].ToString();
                }
                MessageBox.Show(error);
                return;
            }
            else
            {
                MessageBox.Show("Амжилттай үүслээ.");
            }
        }   //DynamicReport
        public void SaveAssembly(string rptname)
        {
            myassembly = new CodeCompileUnit();

            //namespace-ээ assembly -д оруулж байна
            myassembly.Namespaces.Add(mynamespace);

            //Ашиглагдах dll ээ оруулж ирж байна.(using...)
            CompilerParameters comparam = new CompilerParameters(new string[] { "mscorlib.dll", "ISM.Template.dll" });
            comparam.ReferencedAssemblies.Add("System.dll");
            comparam.ReferencedAssemblies.Add("System.Drawing.dll");
            comparam.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            comparam.ReferencedAssemblies.Add("ISM.Template.dll");

            comparam.GenerateInMemory = false;
            comparam.EmbeddedResources.Add(rptname);
            comparam.GenerateExecutable = true;
            comparam.MainClass = txtSlipsname.Text + ".Resource";
            //Үүсгэх газрыг зааж өгч байна.
            comparam.OutputAssembly = _core.SlipsPathIn + "\\" + txtSlipsname.Text + ".dll";
            Microsoft.CSharp.CSharpCodeProvider ccp = new Microsoft.CSharp.CSharpCodeProvider();
            ICodeCompiler icc = ccp.CreateCompiler();

            //Ямар нэгэн алдаа гарвал мессэжбокс дээр хэвлэх үйлдэл явагдаж байна.
            CompilerResults compres = icc.CompileAssemblyFromDom(comparam, myassembly);

            if (compres == null || compres.Errors.Count > 0)
            {
                string error = "";
                for (int i = 0; i < compres.Errors.Count; i++)
                {
                    error = error + "\r\n" + compres.Errors[i].ToString();
                }
                MessageBox.Show(error);
                return;
            }
            else
            {
                MessageBox.Show("Амжилттай үүслээ.");
            }
        }                   //Slips
        #endregion
        private void cbotype_EditValueChanged(object sender, EventArgs e)
        {
            if (Static.ToInt(cbotype.EditValue) == 0)
            {
                txtFilePath.Text = _core.ReportPathIn;
                Slips.Visible = false;
            }
            if (Static.ToInt(cbotype.EditValue) == 1)
            {
                txtPath.Text = _core.SlipsPathIn;
                Slips.Visible = true;
            }
        }
        #region[BTN]
        private void btnChoose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FDialog = new FolderBrowserDialog();
            if (FDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtFilePath.Text = FDialog.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
                }
            }    
        }
        private void btnChoose1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.Title = "Сонгох";
            opendialog.Filter = "Report File[*.rpt]|*.rpt";
            try
            {
                if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtPath.EditValue = opendialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хавтсыг уншиж чадсангүй : " + ex.Message);
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (Static.ToInt(cbotype.EditValue) == 0)
            {
                if (Directory.Exists(_core.ReportPathIn))
                {
                    if (cboTxnCode.EditValue == null) { MessageBox.Show("Гүйлгээний код сонгоно уу."); cboTxnCode.Select(); return; }
                    if (txtFilePath.Text == "") { MessageBox.Show("Тайлангын *.rpt, *.ini файл байрлаж буй хавтсыг оруулна уу"); txtFilePath.Select(); return; }
                    CreateNamespace("rep" + cboTxnCode.EditValue.ToString());
                    CreateImports();
                    CreateClass();
                    CreateEntryPoint();
                    SaveAssembly("rep" + cboTxnCode.EditValue.ToString() + ".ini", "rep" + cboTxnCode.EditValue.ToString() + ".rpt");
                }
                else
                {
                    MessageBox.Show("Тайлан байрлаж буй хавтас буруу байна.");
                }
            }
            else
            {
                if (Directory.Exists(_core.SlipsPathIn))
                {
                    if (txtSlipsname.Text == "") { MessageBox.Show("Slips тайлангийн нэр оруулна уу."); txtSlipsname.Select(); return; }
                    if (txtPath.Text == "") { MessageBox.Show("*.rpt файл сонгоно уу."); txtPath.Select(); return; }
                    CreateNamespace(txtSlipsname.Text);
                    CreateImports();
                    CreateClass();
                    CreateEntryPoint();
                    SaveAssembly(txtPath.Text);
                }
                else
                {
                    MessageBox.Show("Slips загварын байрлаж буй хавтас буруу байна.");
                }
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}