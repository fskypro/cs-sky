using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
using System.Globalization;
using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;

using XTreme.XTJson;
using XTreme.XTPList;

namespace Test
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			FileStream fs = new FileStream("c:\\test.json", FileMode.Open);
			BinaryReader br = new BinaryReader(fs, Encoding.UTF8);
			byte[] bs = br.ReadBytes((int)fs.Length);
			string s = Encoding.UTF8.GetString(bs);
			XTJsonDict r = XTJson.Explain(s);
			
		}

		private string m_v;
		public string V
		{
			get { return m_v; }
		}

		private void Form1_Click(object sender, EventArgs e)
		{
			CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

			// 2.ICodeComplier
			ICodeCompiler objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

			// 3.CompilerParameters
			CompilerParameters objCompilerParameters = new CompilerParameters();
			objCompilerParameters.ReferencedAssemblies.Add("System.dll");
			objCompilerParameters.GenerateExecutable = false;
			objCompilerParameters.GenerateInMemory = true;

			// 4.CompilerResults
			CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, GenerateCode());

			if (cr.Errors.HasErrors)
			{
				Console.WriteLine("编译错误：");
				foreach (CompilerError err in cr.Errors)
				{
					Console.WriteLine(err.ErrorText);
				}
			}
			else
			{
				// 通过反射，调用HelloWorld的实例
				Assembly objAssembly = cr.CompiledAssembly;
				object bb = objAssembly.CreateInstance("AA.BB");
				this.propertyGrid1.SelectedObject = bb;
			}
		}

		static string GenerateCode()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("using System;");
			sb.Append(Environment.NewLine);
			sb.Append("namespace AA");
			sb.Append(Environment.NewLine);
			sb.Append("{");
			sb.Append(Environment.NewLine);
			sb.Append("    public class BB");
			sb.Append(Environment.NewLine);
			sb.Append("    {");
			sb.Append(Environment.NewLine);
			sb.Append("        public string OutPut");
			sb.Append(Environment.NewLine);
			sb.Append("        {");
			sb.Append(Environment.NewLine);
			sb.Append("             get{ return \"Hello world!\";}");
			sb.Append(Environment.NewLine);
			sb.Append("        }");
			sb.Append(Environment.NewLine);
			sb.Append("    }");
			sb.Append(Environment.NewLine);
			sb.Append("}");

			string code = sb.ToString();

			return code;
		}
   }
}
