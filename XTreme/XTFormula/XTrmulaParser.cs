// ------------------------------------------------------------------
// Description : 公式解释器
// Author      : hyw
// Email       : atnostyle@gmail.com
// Date        : 2014.07.23
// Histories   :
// ------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XTreme.XTFormula
{
	internal sealed class XTFormulaParser
	{
		#region 静态构造
		static private XTFormulaTokenParser[] sm_tokenParsers;		// 解释器

		static XTFormulaParser()
		{
			sm_tokenParsers = new XTFormulaTokenParser[]{
				new XTOperatorParser(),								// 双目运算符	
				new XTNumericParser(),								// 值类型
				new XTFunctionParser(),								// 函数
				new XTScopeParser()};								// 括号
		}

		#endregion

		private readonly List<XTFormulaTokenParser> m_tokenParsers;	// 自定义参数解释器
		private readonly string m_formula;							// 总公式
		private int m_point;										// 扫描指针

		internal XTFormulaParser(string formula, string[] argNames)
		{
			m_tokenParsers = new List<XTFormulaTokenParser>();
			m_tokenParsers.AddRange(sm_tokenParsers);
			foreach (string argName in argNames)
				m_tokenParsers.Add(new XTArgParser(argName));

			this.m_formula = formula;
			this.m_point = 0;
		}

		// ----------------------------------------------------------
		// properties
		// ----------------------------------------------------------
		internal string Formula
		{
			get { return this.m_formula; }
		}

		// ----------------------------------------------------------
		// internal
		// ----------------------------------------------------------
		#region 遍历公式函数
		// 获取当前指针指向的字符
		internal int CurrChar()
		{
			char chr;
			while (this.m_point < this.m_formula.Length)
			{
				chr = this.m_formula[this.m_point];
				if (chr == ' ')
					++this.m_point;
				else
					return chr;
			}
			return -1;
		}

		// 获取当前指针指向的字符，并下移指针
		internal int NextChar()
		{
			char chr;
			while (this.m_point < this.m_formula.Length)
			{
				chr = this.m_formula[this.m_point++];
				if (chr != ' ') return chr;
			}
			return -1;
		}

		// 获取正则指向串，并下移指针
		internal Match NextRegx(Regex re)
		{
			if (this.CurrChar() <= 0) return null;
			string tail = this.m_formula.Remove(0, this.m_point);
			Match match = re.Match(tail);
			if (!match.Success) return null;
			this.m_point += match.Length;
			return match;
		}

		// 获取下一个字符串，并下移指针
		// fixNexts 如果不等于 null，则限制字符串后必须是集合中指定的字符
		// allowAsEnd 指出是否允许指定字符串为公式结束
		internal bool NextString(string str, HashSet<char> fixNexts=null, bool allowAsEnd=false)
		{
			if (this.CurrChar() < 0) return false;
			string leave = this.m_formula.Substring(this.m_point);
			if (!leave.StartsWith(str)) return false;
			if (fixNexts == null)							// 对字符串的下一个字符没限制
			{
				this.m_point += str.Length;
				return true;
			}

			leave = leave.Remove(0, str.Length);
			if (leave.Length == 0)							// 公式结束
			{
				if (!allowAsEnd) return false;
				this.m_point += str.Length;
				return true;
			}
			if (fixNexts == null)							// 对下一个字符没限制
			{
				return true;
			}
			if (fixNexts.Contains(leave[0]))				// 字符串的下一个字符在指定字符集内
			{
				this.m_point += str.Length;
				return true;
			}
			return false;
		}

		#endregion

		// -------------------------------------------
		// 抛出公式错误异常
		internal void RaiseFormulaException()
		{
			throw new XTFormulaErrorException(this.m_formula, this.m_point);
		}

		// 解释嵌套表达式
		private XTFormula Parse(HashSet<string> argNames, XTFormula formula)
		{
			XTFormulaToken token = null;
			while (this.CurrChar() > 0)
			{
				token = null;
				foreach (XTFormulaTokenParser parser in m_tokenParsers)
				{
					token = parser.Parse(this, argNames);
					if (token == null) continue;
					formula.AddToken(this, token);
					break;
				}
				if (token == null)
					break;
			}
			if (!formula.IsVaild)
				this.RaiseFormulaException();
			return formula;
		}

		// 嵌套公式使用
		public XTFormula InnerParse(HashSet<string> argNames)
		{
			return this.Parse(argNames, new XTFormula(this.m_formula));
		}

		// 顶层公式使用
		public XTFormula RootParse()
		{
			HashSet<string> argNames = new HashSet<string>();
			XTFormula formula = new XTFormula(this.m_formula, argNames);
			return this.Parse(argNames, formula);
		}
	}
}
