// ------------------------------------------------------------------
// Description : 参数元素基类
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
	internal class XTArgParser : XTFormulaTokenParser
	{
		private const string PTN = @"(?:^{0}(?=[^A-z_0-9]))|(?:^{0}\s*$)";

		private string m_name;
		private Regex m_re;

		public XTArgParser(string name)
		{
			this.m_name = name;
			this.m_re = new Regex(string.Format(PTN, name));
		}

		public override XTFormulaToken Parse(XTFormulaParser parser, HashSet<string> argNames)
		{
			Match m = parser.NextRegx(this.m_re);
			if (m == null) return null;
			argNames.Add(this.m_name);
			return new XTArgToken(this.m_name);
		}
	}
}
